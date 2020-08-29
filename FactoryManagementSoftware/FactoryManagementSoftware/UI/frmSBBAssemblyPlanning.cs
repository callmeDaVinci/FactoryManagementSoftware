using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBAssemblyPlanning : Form
    {
        public frmSBBAssemblyPlanning()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvMatList, true);
        }

        public frmSBBAssemblyPlanning(DataTable dt)
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvMatList, true);

            tool.loadFactory(cmbSiteLocation);
            cmbSiteLocation.Text = text.Factory_Store;
            //cmbSiteLocation.SelectedIndex = -1;

            LoadItemListFromPlanner(dt);
            //dt_FromPlanner = dt;
            
        }

        #region Object and Variable declare

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();
        planningDAL dalPlan = new planningDAL();
        facStockDAL dalStock = new facStockDAL();
        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();

        DataTable dt_MatStock;
        DataTable dt_MatListForEachItem;
        private int oldTargetBag = 0;

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE INT";
        readonly string header_Unit = "UNIT";
        readonly string header_ItemString = "ITEM";
        readonly string header_Type = "TYPE";
        readonly string header_Code = "CODE";
        readonly string header_StockPcs = "STOCK (PCS)";
        readonly string header_StockBag = "STOCK (BAG)";
        readonly string header_StockString = "STOCK";
        readonly string header_QtyPerBag = "QTY PER BAG";
        readonly string header_TargetBag = "TARGET (BAG)";
        readonly string header_TargetPcs = "TARGET (PCS)";
        readonly string header_MaxAvailability = "MAX (PCS)";
        //readonly string header_OriMaxAvailability = "ORI MAX (PCS)";

        readonly string header_BalAfterDeliveryInPcs = "BAL. AFTER DELIVERY IN PCS";

        //readonly string header_OrigRequiredQty = "ORIG REQUIRED QTY";

        readonly string header_TotalStock = "TOTAL STOCK";
        readonly string header_ParentCode = "PARENT CODE";
        readonly string header_RequiredQty = "REQUIRED QTY";
        readonly string header_RequiredBag = "REQUIRED BAG";
        readonly string header_SiteBalance = "SITE BAL.";
        readonly string header_JoinQty = "JOIN QTY";
        readonly string header_ToDeliverBag = "TO DELIVER (BAG)";
        readonly string header_ToDeliverPCS = "TO DELIVER (PCS)";
        //readonly string header_MinCheck = "MIN CHECK";
        readonly string header_DeliverFrom = "FROM";

        readonly string headerType = "TYPE";

        readonly string unit_Bag = "BAG";
        readonly string unit_BagBushGrip = "BAG(BUSH & GRIP)";
        readonly string unit_Container = "CTR";
        readonly string unit_ContainerBushGrip = "CTR(BUSH & GRIP)";
        readonly string unit_HeaderName = "PACKAGING UNIT";


        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanID = "PLAN";
        readonly string headerFac = "FAC.";
        readonly string headerStart = "START";
        readonly string headerEnd = "END";
        readonly string headerMac = "MAC.";
        readonly string headerFrom = "FROM";
        readonly string headerTransferPending = "TRANSFER PENDING (KG/PIECE)";
        readonly string headerItem = "PLAN FOR";
        readonly string headerPlanToUse = "PLAN TO USE QTY";
        readonly string headerStatus = "STATUS";
        readonly string headerBalance = "BALANCE";
        readonly string headerTransfering = "TRANSFERING";
        readonly string headerUnuse = "UNUSE";

        readonly string headerStillNeed = "STILL NEED (KG/PIECE)";
        readonly string headerStock = "FAC. STOCK (KG/PIECE)";
        readonly string headerTransferred = "TRANSFERRED (KG/PIECE)";

        readonly string text_OutOfStock = "OUT OF STOCK";
        private bool Loaded = false;
        private bool ItemEditChanged = false;

        #endregion

        private DataTable NewMatStockTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerFac, typeof(string));
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerStock, typeof(float));
            dt.Columns.Add(headerBalance, typeof(float));
            dt.Columns.Add(headerTransfering, typeof(float));
            dt.Columns.Add(headerUnuse, typeof(float));

            return dt;
        }

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_ItemString, typeof(string));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));

            dt.Columns.Add(header_QtyPerBag, typeof(int));

            dt.Columns.Add(header_StockPcs, typeof(int));
            dt.Columns.Add(header_StockBag, typeof(int));
            dt.Columns.Add(header_StockString, typeof(string));

            dt.Columns.Add(header_RequiredBag, typeof(int));
            dt.Columns.Add(header_TargetBag, typeof(int));
            dt.Columns.Add(header_TargetPcs, typeof(int));
            dt.Columns.Add(header_MaxAvailability, typeof(int));

            return dt;
        }

        private DataTable NewMaterialTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_ParentCode, typeof(string));
            dt.Columns.Add(header_ItemString, typeof(string));
            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_JoinQty, typeof(int));
            

            dt.Columns.Add(header_TotalStock, typeof(int));
            dt.Columns.Add(header_SiteBalance, typeof(int));

            dt.Columns.Add(header_RequiredQty, typeof(int));

            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_ToDeliverBag, typeof(int));
            dt.Columns.Add(header_ToDeliverPCS, typeof(int));

            dt.Columns.Add(unit_HeaderName, typeof(string));
            dt.Columns.Add(header_DeliverFrom, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            

            if (dgv == dgvItemList)
            {
                dgv.Columns[header_TargetBag].DefaultCellStyle.BackColor = SystemColors.Info;
                dgv.Columns[header_Code].Visible = false;
                dgv.Columns[header_Size].Visible = false;
                dgv.Columns[header_Unit].Visible = false;
                dgv.Columns[header_Type].Visible = false;
                dgv.Columns[header_StockPcs].Visible = false;
                dgv.Columns[header_StockBag].Visible = false;
                dgv.Columns[header_StockString].Visible = false;
                dgv.Columns[header_RequiredBag].Visible = false;

                dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                dgv.Columns[header_TargetBag].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

                dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_QtyPerBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_RequiredBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TargetBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TargetPcs].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_MaxAvailability].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else if(dgv == dgvMatList)
            {
                dgv.Columns[header_QtyPerBag].DefaultCellStyle.BackColor = SystemColors.Info;

                dgv.Columns[header_Code].Visible = false;
                dgv.Columns[header_Size].Visible = false;
                dgv.Columns[header_Unit].Visible = false;
                dgv.Columns[header_Type].Visible = false;
                dgv.Columns[header_ParentCode].Visible = false;
                dgv.Columns[header_JoinQty].Visible = false;

                if(!cbStockInclude.Checked)
                {
                    dgv.Columns[header_SiteBalance].Visible = false;
                }
                else
                {
                    dgv.Columns[header_SiteBalance].Visible = true;
                }

                dgv.Columns[header_DeliverFrom].MinimumWidth = 150;

                dgv.Columns[header_DeliverFrom].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                dgv.Columns[header_ToDeliverBag].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

                dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TotalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ToDeliverPCS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_QtyPerBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_RequiredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_SiteBalance].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ToDeliverBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


        }

        private void LoadItemListFromPlanner(DataTable dt)
        {
            Loaded = false;
            DataTable dt_Item = NewItemTable();

            foreach(DataRow row in dt.Rows)
            {
                int BalAfterDelivery = int.TryParse(row[header_BalAfterDeliveryInPcs].ToString(), out BalAfterDelivery) ? BalAfterDelivery : 0;
                int qtyPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                if (BalAfterDelivery < 0)
                {
                    DataRow new_Row = dt_Item.NewRow();

                    string size = row[header_Size].ToString();
                    string unit = row[header_Unit].ToString();
                    string type = row[header_Type].ToString();
                    string code = row[header_Code].ToString();

                    new_Row[header_Index] = row[header_Index];
                    new_Row[header_Size] = row[header_Size];
                    new_Row[header_Unit] = row[header_Unit];
                    new_Row[header_Type] = row[header_Type];
                    new_Row[header_Code] = row[header_Code];

                    new_Row[header_ItemString] = size + " " + unit + " " + type;

                    new_Row[header_QtyPerBag] = row[header_QtyPerBag];

                    new_Row[header_StockPcs] = row[header_StockPcs];
                    new_Row[header_StockBag] = row[header_StockBag];
                    new_Row[header_StockString] = row[header_StockString];

                    new_Row[header_RequiredBag] = BalAfterDelivery / qtyPerBag * -1;
                    new_Row[header_TargetBag] = BalAfterDelivery/qtyPerBag * -1;
                    new_Row[header_TargetPcs] = BalAfterDelivery * -1;
                    new_Row[header_MaxAvailability] = BalAfterDelivery * -1;

                    dt_Item.Rows.Add(new_Row);
                }
               

            }

            dgvItemList.DataSource = dt_Item;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();

            LoadMatPartList();
            //LoadMatPartListWithTargetPcs();
            Loaded = true;
        }

        private string GetChildCode(DataTable dt, string parentCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (parentCode == row[dalJoin.ParentCode].ToString())
                {
                    string childCode = row[dalJoin.ChildCode].ToString();

                    if (childCode.Substring(0, 2) == "CF")
                    {
                        parentCode = childCode;

                        break;
                    }
                }

            }

            return parentCode;
        }

        private string GetFacName(string s)
        {
            string rtn = null;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    rtn += s[i];
                }
                else
                {
                    return rtn;
                }
            }
            return rtn;
        }

        private void AddComboBoxToDGVCell()
        {
            DataGridView dgv = dgvMatList;

            DataTable dt_RequiredList = (DataTable)dgv.DataSource;

            DataTable dt_stock = dalStock.Select();

            dt_MatStock = NewMatStockTable();

            DataRow row_MatStock;

            string previousMat = null;
            string Factory = cmbSiteLocation.Text;

            foreach (DataRow row in dt_RequiredList.Rows)
            {
                int toDeliverBag = int.TryParse(row[header_ToDeliverBag].ToString(), out toDeliverBag) ? toDeliverBag : -2;
                if (toDeliverBag > -2)
                {
                    string MatCode = row[header_Code].ToString();
                    int stdPacking = int.TryParse(row[header_QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;
                    int siteBal = int.TryParse(row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;

                    if (previousMat != MatCode)
                    {
                        previousMat = MatCode;

                        //get fac data
                        foreach (DataRow facStock in dt_stock.Rows)
                        {
                            string ItemCode = facStock[dalItem.ItemCode].ToString();

                            if (ItemCode == MatCode)
                            {
                                row_MatStock = dt_MatStock.NewRow();
                                row_MatStock[headerMatCode] = MatCode;
                                row_MatStock[headerFac] = facStock["fac_name"].ToString();
                                row_MatStock[headerStock] = facStock["stock_qty"].ToString();
                                row_MatStock[headerStillNeed] = 0;
                                row_MatStock[headerBalance] = facStock["stock_qty"].ToString();
                                row_MatStock[headerTransfering] = 0;
                                row_MatStock[headerUnuse] = facStock["stock_qty"].ToString();

                                dt_MatStock.Rows.Add(row_MatStock);
                            }
                        }

                    }

                    foreach (DataRow matStock in dt_MatStock.Rows)
                    {
                        if (MatCode == matStock[headerMatCode].ToString() && Factory == matStock[headerFac].ToString())
                        {
                            matStock[headerStillNeed] = toDeliverBag;

                            int balance = siteBal - toDeliverBag;

                            if (balance < 0)
                            {
                                balance = 0;
                            }

                            matStock[headerBalance] = balance;
                            matStock[headerTransfering] = 0;
                            matStock[headerUnuse] = balance;
                        }
                    }
                }
            }

          

            foreach (DataGridViewRow row in dgv.Rows)
            {

                int toDeliver = int.TryParse(row.Cells[header_ToDeliverBag].Value.ToString(), out toDeliver) ? toDeliver : 0;
                int toDeliverPcs = int.TryParse(row.Cells[header_ToDeliverPCS].Value.ToString(), out toDeliverPcs) ? toDeliverPcs : 0;

                if (toDeliver > 0)
                {
                    DataGridViewComboBoxCell ComboBoxCellUnit = new DataGridViewComboBoxCell();

                    ComboBoxCellUnit.Items.Add(unit_Bag);
                    ComboBoxCellUnit.Items.Add(unit_BagBushGrip);
                    ComboBoxCellUnit.Items.Add(unit_Container);
                    ComboBoxCellUnit.Items.Add(unit_ContainerBushGrip);
                    
                    ComboBoxCellUnit.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    
                    row.Cells[unit_HeaderName] = ComboBoxCellUnit;
                    row.Cells[unit_HeaderName].Value = unit_Bag;

                    string MatCode = row.Cells[header_Code].Value.ToString();

                    DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();

                    string locationFrom = "";
                    string selectedLocation = "";

                    foreach (DataRow matStock in dt_MatStock.Rows)
                    {
                        string ItemCode = matStock[headerMatCode].ToString();
                        string FacName = matStock[headerFac].ToString();
                        string Balance = matStock[headerBalance].ToString();

                        if (ItemCode == MatCode && FacName != Factory && Balance != "0")
                        {
                            locationFrom = FacName + " (BAL.: " + Balance + ")";
                            ComboBoxCell.Items.Add(locationFrom);


                            int BalInt = int.TryParse(Balance, out BalInt) ? BalInt : 0;

                            if (BalInt >= toDeliverPcs)
                            {
                                selectedLocation = locationFrom;
                            }
                        }
                    }

                    ComboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                    if (ComboBoxCell.Items.Count <= 0)
                    {
                        ComboBoxCell.Items.Add(text_OutOfStock);
                        row.Cells[header_DeliverFrom] = ComboBoxCell;
                    }
                    else
                    {
                        row.Cells[header_DeliverFrom] = ComboBoxCell;
                    }

                    if (selectedLocation != "")
                    {
                        row.Cells[header_DeliverFrom].Value = selectedLocation;
                        row.Cells[header_ToDeliverPCS].Style.ForeColor = Color.Black;
                        row.Cells[header_ToDeliverBag].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.Cells[header_ToDeliverBag].Style.ForeColor = Color.Red;
                        row.Cells[header_ToDeliverPCS].Style.ForeColor = Color.Red;
                    }

                }
                else
                {
                    row.Cells[unit_HeaderName].Value = DBNull.Value;
                    row.Cells[header_DeliverFrom].Value = DBNull.Value;
                }
            }
        }

        private DataTable RemoveRowIfRequiredQtyEqualZero(DataTable dt)
        {
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                int requiredQty = int.TryParse(row[header_RequiredQty].ToString(), out requiredQty) ? requiredQty : 0;

                if (requiredQty == 0)
                {
                    row.Delete();

                }
            }

            dt.AcceptChanges();

            return dt;
        }

        private DataTable ClearRequiredQty(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                row[header_RequiredQty] = 0;
            }

            return dt;
        }

        private DataTable MergeSameMaterial(DataTable dt_Mat)
        {
            DataTable dt = dt_Mat.Clone();

            dt_Mat.DefaultView.Sort = header_Code + " ASC";
            dt_Mat = dt_Mat.DefaultView.ToTable();

            string preChildCode = "";

            foreach (DataRow row in dt_Mat.Rows)
            {
                string childCode = row[header_Code].ToString();
                int newRequiredQty = int.TryParse(row[header_RequiredQty].ToString(), out newRequiredQty) ? newRequiredQty : 0;

                if (childCode != preChildCode)
                {
                    dt.ImportRow(row);
                    preChildCode = childCode;
                }
                else
                {
                    foreach(DataRow dt_row in dt.Rows)
                    {
                        if (dt_row[header_Code].ToString() == childCode)
                        {
                            int oldRequiredQty = int.TryParse(dt_row[header_RequiredQty].ToString(), out oldRequiredQty) ? oldRequiredQty : 0;
                            int siteBal = int.TryParse(dt_row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;
                            int qtyperBag = int.TryParse(dt_row[header_QtyPerBag].ToString(), out qtyperBag) ? qtyperBag : 0;

                            int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - newRequiredQty - oldRequiredQty;

                            //check if site have enough balance, if so no need to deliver
                            bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                            bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyperBag > 0 ? qtyperBag : 1));

                            bagToDeliver = qtyperBag > 0 ? bagToDeliver : -1;

                            dt_row[header_RequiredQty] = newRequiredQty + oldRequiredQty;

                            dt_row[header_ToDeliverBag] = bagToDeliver;

                            int pcsTodeliver = 0;

                            if (bagToDeliver > 0)
                            {
                                pcsTodeliver = bagToDeliver * qtyperBag;
                            }

                            dt_row[header_ToDeliverPCS] = pcsTodeliver;

                            break;
                        }
                    }
                }
           
            }

            return dt;
        }

        private void LoadMatPartList()
        {
            #region Pre Setting: datatable and variable

            DataTable dt_Fac = dalStock.Select();
            DataTable dtJoin = dalJoin.SelectWithSBBInfo();
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;
            DataTable dt_Mat = NewMaterialTable();

            if (dgvMatList.Rows.Count > 0)
            {
                dt_Mat = (DataTable)dgvMatList.DataSource;

                //clear required qty
                dt_Mat = ClearRequiredQty(dt_Mat);
            }

            string preChildCode = "";
            int index = 1;

            #endregion

            //Load Material List
            foreach (DataRow row in dt_Item.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_Code].ToString());
                string GoodsCode = row[header_Code].ToString();

                int targetPcs = int.TryParse(row[header_TargetPcs].ToString(), out targetPcs) ? targetPcs : 0;

                row[header_MaxAvailability] = targetPcs;

                foreach (DataRow rowJoin in dtJoin.Rows)
                {
                    if (rowJoin[dalJoin.ParentCode].ToString() == parentCode && preChildCode != rowJoin[dalJoin.ChildCode].ToString())
                    {
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

                        preChildCode = childCode;

                        int qtyPerBag = int.TryParse(rowJoin[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int numerator = int.TryParse(rowJoin[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                        int denominator = int.TryParse(rowJoin[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                        string sizeUnit = rowJoin[dalSPP.SizeUnit].ToString().ToUpper();
                        string typeName = rowJoin[dalSPP.TypeName].ToString();

                        string sizeString = "";
                        int size = 1;

                        if (denominator == 1)
                        {
                            size = numerator;
                            sizeString = numerator + " " + sizeUnit;

                        }
                        else
                        {
                            size = numerator / denominator;
                            sizeString = numerator + "/" + denominator + " " + sizeUnit;
                        }

                        int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                        int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                        int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                        int child_StillNeed = 0;

                        joinMax = joinMax <= 0 ? 1 : joinMax;
                        joinMin = joinMin <= 0 ? 1 : joinMin;

                        child_StillNeed = targetPcs / joinMax * joinQty;

                        child_StillNeed = targetPcs % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;


                        bool itemFound = false;

                        foreach (DataRow rowMat in dt_Mat.Rows)
                        {
                            if (rowMat[header_Code].ToString() == childCode && rowMat[header_ParentCode].ToString() == GoodsCode)
                            {
                                int oldRequiredQty = int.TryParse(rowMat[header_RequiredQty].ToString(), out oldRequiredQty) ? oldRequiredQty : 0;

                                int siteBal = 0;
                                int totalStock = 0;

                                foreach (DataRow stock in dt_Fac.Rows)
                                {
                                    if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                    }

                                    if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                    }
                                }

                                if (cbStockInclude.Checked)
                                {
                                    totalStock += siteBal;
                                }
                                else
                                {
                                    siteBal = 0;
                                }

                                int qtyperBag_2 = int.TryParse(rowMat[header_QtyPerBag].ToString(), out qtyperBag_2) ? qtyperBag_2 : 0;

                                int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed - oldRequiredQty;

                                //check if site have enough balance, if so no need to deliver
                                bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                                bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyperBag_2 > 0 ? qtyperBag_2 : 1));

                                bagToDeliver = qtyperBag_2 > 0 ? bagToDeliver : -1;

                                if (!itemFound)
                                {
                                    rowMat[header_RequiredQty] = child_StillNeed + oldRequiredQty;

                                    rowMat[header_ToDeliverBag] = bagToDeliver;

                                    rowMat[header_SiteBalance] = siteBal;
                                    rowMat[header_TotalStock] = totalStock;

                                    int pcsTodeliver = 0;

                                    if (bagToDeliver > 0)
                                    {
                                        pcsTodeliver = bagToDeliver * qtyperBag_2;
                                    }

                                    rowMat[header_ToDeliverPCS] = pcsTodeliver;
                                }

                                itemFound = true;

                                break;
                            }
                        }

                        if (!itemFound)
                        {
                            int siteBal = 0;
                            int totalStock = 0;

                            foreach (DataRow stock in dt_Fac.Rows)
                            {
                                if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                {
                                    siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                }

                                if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                {
                                    totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                }
                            }

                            if (cbStockInclude.Checked)
                            {
                                totalStock += siteBal;
                            }
                            else
                            {
                                siteBal = 0;
                            }

                            DataRow new_Row = dt_Mat.NewRow();

                            int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed;

                            //check if site have enough balance, if so no need to deliver
                            bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                            bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyPerBag > 0 ? qtyPerBag : 1));

                            bagToDeliver = qtyPerBag > 0 ? bagToDeliver : -1;

                            int pcsTodeliver = 0;

                            if (bagToDeliver > 0)
                            {
                                pcsTodeliver = bagToDeliver * qtyPerBag;
                            }

                            new_Row[header_Index] = index++;
                            new_Row[header_ParentCode] = GoodsCode;
                            new_Row[header_ItemString] = childName;
                            new_Row[header_Size] = size;
                            new_Row[header_Unit] = sizeUnit;
                            new_Row[header_Type] = typeName;
                            new_Row[header_Code] = childCode;
                            new_Row[header_TotalStock] = totalStock;
                            new_Row[header_JoinQty] = joinQty;
                            new_Row[header_QtyPerBag] = qtyPerBag;
                            new_Row[header_RequiredQty] = child_StillNeed;
                            new_Row[header_SiteBalance] = siteBal;
                            new_Row[header_ToDeliverBag] = bagToDeliver;
                            new_Row[header_ToDeliverPCS] = pcsTodeliver;
                            dt_Mat.Rows.Add(new_Row);
                        }

                    }
                }
            }

            //remove row if required qty = 0
            dt_Mat = RemoveRowIfRequiredQtyEqualZero(dt_Mat);

            if(cbMaxAvailability.Checked)
            {
                //update item max qty
                UpdateItemMaxAvailability(dt_Mat);

                //update material list: change required qty & to deliver bag only
                dt_Mat = UpdateMatPartList(dt_Mat);
            }


            //calculate Min availability for item
            UpdateItemMinAvailability(dt_Mat);

            dgvItemList.Update();
            dgvItemList.Refresh();

            dt_MatListForEachItem = dt_Mat;

            //Merge same material in dt_Mat
            dt_Mat = MergeSameMaterial(dt_Mat);

            int indexReset = 1;

            foreach (DataGridViewRow row in dgvMatList.Rows)
            {
                row.Cells[header_Index].Value = indexReset++;
            }

            dgvMatList.DataSource = dt_Mat;
            DgvUIEdit(dgvMatList);
            dgvMatList.ReadOnly = false;
            dgvMatList.SelectionMode = DataGridViewSelectionMode.CellSelect;

            AddComboBoxToDGVCell();

            dgvMatList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            dgvMatList.ClearSelection();
        }

        private DataTable UpdateMatPartList(DataTable dt_Mat)
        {
            #region Pre Setting: datatable and variable

            DataTable dt_Fac = dalStock.Select();
            DataTable dtJoin = dalJoin.SelectWithSBBInfo();
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;

            dt_Mat = ClearRequiredQty(dt_Mat);

            string preChildCode = "";
            int index = 1;

            #endregion

            //Load Material List
            foreach (DataRow row in dt_Item.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_Code].ToString());
                string GoodsCode = row[header_Code].ToString();
                int targetPcs = int.TryParse(row[header_MaxAvailability].ToString(), out targetPcs) ? targetPcs : 0;

                if(targetPcs == 0)
                {
                    targetPcs = int.TryParse(row[header_TargetPcs].ToString(), out targetPcs) ? targetPcs : 0;
                }

                if(targetPcs != 0)
                {
                    foreach (DataRow rowJoin in dtJoin.Rows)
                    {
                        if (rowJoin[dalJoin.ParentCode].ToString() == parentCode && preChildCode != rowJoin[dalJoin.ChildCode].ToString())
                        {
                            string childCode = rowJoin[dalJoin.ChildCode].ToString();
                            string childName = rowJoin[dalJoin.ChildName].ToString();

                            preChildCode = childCode;

                            int qtyPerBag = int.TryParse(rowJoin[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                            int numerator = int.TryParse(rowJoin[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                            int denominator = int.TryParse(rowJoin[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                            string sizeUnit = rowJoin[dalSPP.SizeUnit].ToString().ToUpper();
                            string typeName = rowJoin[dalSPP.TypeName].ToString();

                            string sizeString = "";
                            int size = 1;

                            if (denominator == 1)
                            {
                                size = numerator;
                                sizeString = numerator + " " + sizeUnit;

                            }
                            else
                            {
                                size = numerator / denominator;
                                sizeString = numerator + "/" + denominator + " " + sizeUnit;
                            }

                            int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                            int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                            int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                            int child_StillNeed = 0;

                            joinMax = joinMax <= 0 ? 1 : joinMax;
                            joinMin = joinMin <= 0 ? 1 : joinMin;

                            child_StillNeed = targetPcs / joinMax * joinQty;

                            child_StillNeed = targetPcs % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;


                            bool itemFound = false;

                            foreach (DataRow rowMat in dt_Mat.Rows)
                            {
                                if (rowMat[header_Code].ToString() == childCode && rowMat[header_ParentCode].ToString() == GoodsCode)
                                {
                                    int oldRequiredQty = int.TryParse(rowMat[header_RequiredQty].ToString(), out oldRequiredQty) ? oldRequiredQty : 0;

                                    int siteBal = 0;
                                    int totalStock = 0;

                                    foreach (DataRow stock in dt_Fac.Rows)
                                    {
                                        if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                        {
                                            siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                        }

                                        if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                        {
                                            totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                        }
                                    }

                                    if (cbStockInclude.Checked)
                                    {
                                        totalStock += siteBal;
                                    }
                                    else
                                    {
                                        siteBal = 0;
                                    }

                                    int qtyperBag_2 = int.TryParse(rowMat[header_QtyPerBag].ToString(), out qtyperBag_2) ? qtyperBag_2 : 0;

                                    int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed - oldRequiredQty;

                                    //check if site have enough balance, if so no need to deliver
                                    bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                                    bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyperBag_2 > 0 ? qtyperBag_2 : 1));

                                    bagToDeliver = qtyperBag_2 > 0 ? bagToDeliver : -1;

                                    if (!itemFound)
                                    {
                                        if(child_StillNeed + oldRequiredQty > 0)
                                        {
                                            rowMat[header_RequiredQty] = child_StillNeed + oldRequiredQty;

                                            rowMat[header_ToDeliverBag] = bagToDeliver;

                                            rowMat[header_SiteBalance] = siteBal;
                                            rowMat[header_TotalStock] = totalStock;

                                            int pcsTodeliver = 0;

                                            if (bagToDeliver > 0)
                                            {
                                                pcsTodeliver = bagToDeliver * qtyperBag_2;
                                            }

                                            rowMat[header_ToDeliverPCS] = pcsTodeliver;
                                        }
                                       
                                    }

                                    itemFound = true;

                                    break;
                                }
                            }

                            if (!itemFound)
                            {
                                int siteBal = 0;
                                int totalStock = 0;

                                foreach (DataRow stock in dt_Fac.Rows)
                                {
                                    if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                    }

                                    if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                    }
                                }

                                if (cbStockInclude.Checked)
                                {
                                    totalStock += siteBal;
                                }
                                else
                                {
                                    siteBal = 0;
                                }

                                DataRow new_Row = dt_Mat.NewRow();

                                int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed;

                                //check if site have enough balance, if so no need to deliver
                                bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                                bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyPerBag > 0 ? qtyPerBag : 1));

                                bagToDeliver = qtyPerBag > 0 ? bagToDeliver : -1;

                                int pcsTodeliver = 0;

                                if (bagToDeliver > 0)
                                {
                                    pcsTodeliver = bagToDeliver * qtyPerBag;
                                }

                                new_Row[header_Index] = index++;
                                new_Row[header_ParentCode] = GoodsCode;
                                new_Row[header_ItemString] = childName;
                                new_Row[header_Size] = size;
                                new_Row[header_Unit] = sizeUnit;
                                new_Row[header_Type] = typeName;
                                new_Row[header_Code] = childCode;
                                new_Row[header_JoinQty] = joinQty;
                                new_Row[header_QtyPerBag] = qtyPerBag;
                                new_Row[header_RequiredQty] = child_StillNeed;
                                new_Row[header_SiteBalance] = siteBal;
                                new_Row[header_TotalStock] = totalStock;
                                new_Row[header_ToDeliverBag] = bagToDeliver;
                                new_Row[header_ToDeliverPCS] = pcsTodeliver;
                                dt_Mat.Rows.Add(new_Row);
                            }

                        }
                    }
                }

            }

            return dt_Mat;
        }

        private void UpdateItemMaxAvailability(DataTable dt_Mat)
        {
            foreach (DataRow row in dt_Mat.Rows)
            {
                string mat_Type = row[header_Type].ToString();
                string parentCode = row[header_ParentCode].ToString();

                for (int i = 0; i < dgvItemList.Rows.Count; i++)
                {
                    string item_Type = dgvItemList.Rows[i].Cells[header_Type].Value.ToString();
                    string itemCode = dgvItemList.Rows[i].Cells[header_Code].Value.ToString();

                    if (mat_Type == item_Type && itemCode == parentCode)
                    {
                        int qtyPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int bagToDeliver = int.TryParse(row[header_ToDeliverBag].ToString(), out bagToDeliver) ? bagToDeliver : 0;

                        int joinQty = int.TryParse(row[header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                        int pcsToDeliver = qtyPerBag * bagToDeliver / joinQty;
                        int requiredQty = int.TryParse(row[header_RequiredQty].ToString(), out requiredQty) ? requiredQty : 0;

                        if (pcsToDeliver < requiredQty && cbStockInclude.Checked)
                        {
                            int siteBal = int.TryParse(row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;

                            if (siteBal >= requiredQty)
                            {
                                pcsToDeliver = requiredQty / joinQty;
                            }
                            else
                            {
                                pcsToDeliver = (siteBal + qtyPerBag * bagToDeliver) / joinQty;
                            }
                        }



                        //int targetPcs = int.TryParse(dgvItemList.Rows[i].Cells[header_TargetPcs].Value.ToString(), out targetPcs) ? targetPcs : 0;

                        dgvItemList.Rows[i].Cells[header_MaxAvailability].Value = pcsToDeliver;
 

                        break;
                    }
                }
            }

        }

        private void UpdateItemMinAvailability(DataTable dt_Mat)
        {
            for (int i = 0; i < dgvItemList.Rows.Count; i++)
            {
                string item_Type = dgvItemList.Rows[i].Cells[header_Type].Value.ToString();
                string itemCode = dgvItemList.Rows[i].Cells[header_Code].Value.ToString();
                int maxQty = int.TryParse(dgvItemList.Rows[i].Cells[header_MaxAvailability].Value.ToString(), out maxQty) ? maxQty : 0;

                foreach (DataRow row in dt_Mat.Rows)
                {
                    string parentCode = row[header_ParentCode].ToString();

                    if (itemCode == parentCode)
                    {
                        int joinQty = int.TryParse(row[header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                        int qtyPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int bagToDeliver = int.TryParse(row[header_ToDeliverBag].ToString(), out bagToDeliver) ? bagToDeliver : 0;

                        int pcsToDeliver = qtyPerBag * bagToDeliver / joinQty;
                        int requiredQty = int.TryParse(row[header_RequiredQty].ToString(), out requiredQty) ? requiredQty : 0;

                        if (pcsToDeliver < requiredQty && cbStockInclude.Checked)
                        {
                            int siteBal = int.TryParse(row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;
                           
                            if (siteBal >= requiredQty)
                            {
                                pcsToDeliver = requiredQty / joinQty;
                            }
                            else
                            {
                                pcsToDeliver = (siteBal + qtyPerBag * bagToDeliver) / joinQty;
                            }
                        }
                      

                        if (pcsToDeliver < maxQty)
                        {
                            maxQty = pcsToDeliver;
                            dgvItemList.Rows[i].Cells[header_MaxAvailability].Value = maxQty;
                        }
                    }
                }
            }
        }

        private void frmSBBAssemblyPlanning_Load(object sender, EventArgs e)
        {
            tlpMaterialList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            //if(dt_FromPlanner != null)
            //LoadItemListFromPlanner(dt_FromPlanner);

            AddComboBoxToDGVCell();
            dgvItemList.ClearSelection();
            dgvMatList.ClearSelection();
            Loaded = true;
        }

        private void cmbSiteLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                //update site balance
                LoadMatPartList();
            }
        }

        private void dgvItemList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = dgvItemList;

            if (e.RowIndex != -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                if(dgv.Columns[col].Name == header_TargetBag)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = false;
                }
                else
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = true;
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;


        }
        private void dgvItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvItemList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                int targetBag = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out targetBag) ? targetBag : -1;

                if (dgv.Columns[colIndex].Name.Contains(header_TargetBag) ) //Desired Column
                {
                    oldTargetBag = targetBag;

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvItemList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvItemList;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            int targetBag = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out targetBag) ? targetBag : -1;

            if (rowIndex > -1 && targetBag != -1 && targetBag != oldTargetBag)
            {
                oldTargetBag = 0;
                ItemEditChanged = true;

                int qtyPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out qtyPerBag) ? qtyPerBag : 0;

                dgv.Rows[rowIndex].Cells[header_TargetPcs].Value = targetBag * qtyPerBag;
                dgv.Rows[rowIndex].Cells[header_MaxAvailability].Value = targetBag * qtyPerBag;

                //update material list
                LoadMatPartList();

                ItemEditChanged = false;

            }

        }

        private void dgvMatList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatList;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;

            bool validClick = row != -1 && col != -1; //Make sure the clicked row/column is valid.

            if (validClick)
            {
                if (dgv.Rows[row].Cells[col] is DataGridViewComboBoxCell)
                {
                    dgv.BeginEdit(true);
                    ((ComboBox)dgv.EditingControl).DroppedDown = true;
                }
                //else if (dgv.Columns[col].Name == headerPrepare)
                //{
                //    dgv.BeginEdit(true);
                //}
            }
        }

        private string GetFacName(DataTable dt_Fac, string location)
        {
            foreach(DataRow row in dt_Fac.Rows)
            {
                string facName = row["fac_name"].ToString();

                if(location.Contains(facName))
                {
                    return facName;
                }
            }

            return "";
        }
        private void btnText_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvMatList.DataSource;
            DataTable dt_MaterialList = dt.Copy();
            DataTable dt_Fac = dalFac.Select();

            dt_MaterialList.DefaultView.Sort = header_DeliverFrom + " ASC";
            dt_MaterialList = dt_MaterialList.DefaultView.ToTable();



            string assemblySite = cmbSiteLocation.Text;
            string materialPrepareList = "";
            string materialPrepareListForWhatsApp = "Assembly Items:\n\n";
            string preDeliverFrom = "";
            
            foreach(DataRow row in dt_MaterialList.Rows)
            {
                int toDeliver = int.TryParse(row[header_ToDeliverBag].ToString(), out toDeliver) ? toDeliver : 0;
                string from = GetFacName(dt_Fac, row[header_DeliverFrom].ToString());
                string toDeliverUnit = row[unit_HeaderName].ToString();

                toDeliverUnit = toDeliverUnit.ToLower();

                if (toDeliver > 0 && !string.IsNullOrEmpty(from))
                {
                    string item = row[header_ItemString].ToString();
                    
                    string stdPacking = row[header_QtyPerBag].ToString();

                    item = item.Replace("CF ", "");
                    item = item.ToLower();

                    if(preDeliverFrom != from)
                    {
                        
                        if (preDeliverFrom == "")
                        {
                            materialPrepareList += "From " + from + " To " + assemblySite;
                            materialPrepareListForWhatsApp += "_From " + from + " To " + assemblySite + "_";
                        }
                        else
                        {
                            materialPrepareList += "\n\n" + "From " + from + " To " + assemblySite;
                            materialPrepareListForWhatsApp += "\n\n" + "_From " + from + " To " + assemblySite + "_";
                        }

                        preDeliverFrom = from;

                    }

                    if(cbWithStdPacking.Checked)
                    {
                        materialPrepareList += "\n" + item + "   " + toDeliver + " " + toDeliverUnit + " (x" + stdPacking + ")";
                        materialPrepareListForWhatsApp += "\n" + item + "   *" + toDeliver + "* " + " " + toDeliverUnit + " (x" + stdPacking + ")";
                    }
                    else
                    {
                        materialPrepareList += "\n" + item + "   " + toDeliver + " " + toDeliverUnit ;
                        materialPrepareListForWhatsApp += "\n" + item + "   *" + toDeliver + "* " + " " + toDeliverUnit ;
                    }
                    
                }
            }

            MessageBox.Show(materialPrepareList + "\n\n Text has been copied to clipboard.");
            Clipboard.SetText(materialPrepareListForWhatsApp);
        }

        private void dgvMatList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvMatList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                //int targetBag = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out targetBag) ? targetBag : -1;

                if (dgv.Columns[colIndex].Name.Contains(header_QtyPerBag)) //Desired Column
                {
                    //oldTargetBag = targetBag;

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvMatList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvMatList;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (rowIndex > -1 && dgv.Columns[colIndex].Name == header_QtyPerBag)
            {
                //int qtyPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out qtyPerBag) ? qtyPerBag : 0;

                //int siteBal = int.TryParse(dgv.Rows[rowIndex].Cells[header_SiteBalance].Value.ToString(), out siteBal) ? siteBal : 0;

                //int requiredQty = int.TryParse(dgv.Rows[rowIndex].Cells[header_OrigRequiredQty].Value.ToString(), out requiredQty) ? requiredQty : 0;

                //int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - requiredQty;

                //string childCode = dgv.Rows[rowIndex].Cells[header_Code].Value.ToString();

                ////check if site have enough balance, if so no need to deliver
                //bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                //bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyPerBag > 0 ? qtyPerBag : 1));

                //bagToDeliver = qtyPerBag > 0 ? bagToDeliver : -1;

                //dgv.Rows[rowIndex].Cells[header_ToDeliverBag].Value = bagToDeliver;
                //dgv.Rows[rowIndex].Cells[header_ToDeliverPCS].Value = bagToDeliver * qtyPerBag;


                //UpdateQtyPerBagForMatDetailList(childCode, qtyPerBag);

                LoadMatPartList();
            }
        }

        private void UpdateQtyPerBagForMatDetailList(string childCode,int qtyPerBag)
        {
            foreach(DataRow row in dt_MatListForEachItem.Rows)
            {
                if(row[header_Code].ToString() == childCode)
                {
                    row[header_QtyPerBag] = qtyPerBag;
                }
            }
        }

        private void btnAddNewDO_Click(object sender, EventArgs e)
        {
            frmNewSBBAddItem frm = new frmNewSBBAddItem()
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            string selectedItemCode = frmNewSBBAddItem.itemCode;

            DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(text.SPP_BrandName);

            DataTable dt_Item = (DataTable)dgvItemList.DataSource;


            bool itemFound = false;
            foreach(DataRow row in dt_Item.Rows)
            {
                if(row[header_Code].ToString() == selectedItemCode)
                {
                    itemFound = true;
                    break;
                }
            }

            if(!itemFound)
            {
                foreach (DataRow row in dt_Product.Rows)
                {

                    string itemCode = row[dalSPP.ItemCode].ToString();

                    if (itemCode == selectedItemCode)
                    {
                        int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                        int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                        int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;
                        int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                        int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                        string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                        string typeName = row[dalSPP.TypeName].ToString();

                        int pcsStock = readyStock;
                        int bagStock = pcsStock / qtyPerBag;


                        string sizeString = "";
                        int size = 1;
                        if (denominator == 1)
                        {
                            size = numerator;
                            sizeString = numerator + " " + sizeUnit;

                        }
                        else
                        {
                            size = numerator / denominator;
                            sizeString = numerator + "/" + denominator + " " + sizeUnit;
                        }
                        string stockString = pcsStock + " PCS";

                        DataRow new_Row = dt_Item.NewRow();

                        new_Row[header_Size] = size;
                        new_Row[header_Unit] = sizeUnit;
                        new_Row[header_Type] = typeName;
                        new_Row[header_Code] = itemCode;

                        new_Row[header_ItemString] = size + " " + sizeUnit + " " + typeName;

                        new_Row[header_QtyPerBag] = qtyPerBag;

                        new_Row[header_StockPcs] = pcsStock;
                        new_Row[header_StockBag] = bagStock;
                        new_Row[header_StockString] = stockString;

                        new_Row[header_RequiredBag] = 0;
                        new_Row[header_TargetBag] = 0;
                        new_Row[header_TargetPcs] = 0;

                        dt_Item.Rows.Add(new_Row);

                        break;
                    }

                }

                tool.AddIndexNumberToDGV(dgvItemList,header_Index);
            }
           else
            {
                MessageBox.Show("Item already added!");
            }


        }

        private void cbMaxAvailability_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatPartList();
        }

        private void dgvMatList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbStockInclude_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatPartList();
        }

        private int GetBalanceFromLocationString(string str)
        {
            string bal = "";

            bool startConvert = false;
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i].ToString() == "(")
                {
                    startConvert = true;
                }

                if(startConvert && int.TryParse(str[i].ToString(), out int x))
                {
                    bal += str[i].ToString();
                }
            }

            int balInt = int.TryParse(bal, out balInt) ? balInt : 0;
            return balInt;
        }

        private void dgvMatList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if(Loaded && dgv.Columns[col].Name == header_DeliverFrom)
            {
                // My combobox column is the second one so I hard coded a 1, flavor to taste
               //DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgv.Rows[row].Cells[col];

                int toDeliverPcs = int.TryParse(dgv.Rows[row].Cells[header_ToDeliverPCS].Value.ToString(), out toDeliverPcs) ? toDeliverPcs : 0;

                //string cbValue = cb.Value.ToString();
                string cbValue = dgv.CurrentCell.EditedFormattedValue.ToString();
                //get balance
                int bal = GetBalanceFromLocationString(cbValue);

                if(bal < toDeliverPcs)
                {
                    dgv.Rows[row].Cells[header_ToDeliverPCS].Style.ForeColor = Color.Red;
                    dgv.Rows[row].Cells[header_ToDeliverBag].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[header_ToDeliverPCS].Style.ForeColor = Color.Black;
                    dgv.Rows[row].Cells[header_ToDeliverBag].Style.ForeColor = Color.Black;
                }
            }
            
        }
    }
}
