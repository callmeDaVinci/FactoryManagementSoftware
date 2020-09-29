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
using System.Globalization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBPOVSStock : Form
    {
        public frmSBBPOVSStock()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            LoadEditUnitCMB(cmbEditUnit);
            LoadProductSortByCMB(cmbProductSortBy);
            LoadPOSortByCMB(cmbPOSortBy);

        }

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

        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE INT";
        readonly string header_Unit = "UNIT";
        readonly string header_SizeString = "SIZE";
        readonly string header_Type = "TYPE";
        readonly string header_Code = "CODE";
        readonly string header_StockPcs = "STOCK (PCS)";
        readonly string header_StockBag = "STOCK (BAG)";
        readonly string header_StockString = "STOCK";
        readonly string header_QtyPerBag = "QTY PER BAG";
        readonly string header_BalAfterDelivery = "BAL. AFTER DELIVERY";
        readonly string header_BalAfterDeliveryInPcs = "BAL. AFTER DELIVERY IN PCS";
       

        readonly string header_Priority = "P/O_";
        readonly string header_PriorityDelivered = "PRIORITY DELIVERED_";
        readonly string header_PriorityOrder = "PRIORITY ORDER_";
        readonly string header_PriorityTblCode = "PRIORITY TABLE CODE_";
        readonly string header_PriorityPOCode = "PRIORITY PO CODE_";
        readonly string header_PriorityPONo = "PRIORITY PO NO_";
        readonly string header_PriorityCustShortName = "PRIORITY CUST NAME_";
        readonly string header_PriorityToDeliveryStatus = "PRIORITY DELIVERY STATUS_";
        readonly string header_PriorityToDeliveryQty = "PRIORITY TO DELIVERY QTY_";
        readonly string header_PriorityToDeliveryPlanningInPCS = "PRIORITY TO DELIVERY PLANNING IN PCS_";
        readonly string header_ColSelected = "COL SELECTED_";
        readonly string header_PriorityPODate = "PRIORITY PO DATE_";
        readonly string header_PriorityCustCode = "PRIORITY CUST CODE_";

        readonly string header_POTblCode = "P/O TBL CODE";
        readonly string header_ToDeliveryPCSQty = "TO DELIVERY PCS QTY";
        readonly string header_ItemCode = "CODE";
        readonly string header_ItemName = "NAME";
        readonly string header_BalAfter = "BAL. AFTER";

        readonly string text_FillBySystem = "FILL BY SYSTEM";
        readonly string text_SelectCol = "SELECT COLUMN";
        readonly string text_FillIn = "Fill In";
        readonly string text_FillAll = "Fill All";
        readonly string text_FillAllByCustomer = "Fill All By Customer";
        readonly string text_Reset = "Reset";
        readonly string text_ResetAll = "Reset All";
        readonly string text_ResetAllByCustomer = "Reset All By Customer";
        readonly string text_NewTrip = "New Trip";

        readonly string text_OpenDO = "OPEN D/O";
        readonly string text_AddDO = "ADD D/O";

        readonly string text_Selected = " SELECTED";
        
        readonly string text_Unit = "UNIT";
        readonly string text_ProductSortBy = "PRODUCT SORT BY";
        readonly string text_POSortBy = "P/O SORT BY";

        readonly string text_Bag = "BAG";
        readonly string text_Pcs = "PCS";
        readonly string text_Size = "SIZE";
        readonly string text_Type = "TYPE";
        readonly string text_Customer = "CUSTOMER";
        readonly string text_ReceivedDate = "RECEIVED DATE";
       // readonly string text_PriorityLvl = "PRIORITY LEVEL";

        readonly string text_AssemblyNeeded = "ASSEMBLY NEEDED !!!";
        
        readonly string text_DeliveryStatus_ToOpen = "TO OPEN";
        readonly string text_DeliveryStatus_Opened = "OPENED";
        readonly string text_DeliveryStatus_OpenedWithDiff = "OPENED WITH DIFF";


        readonly string header_POCode = "P/O CODE";
        readonly string header_PONo = "P/O NO";
        readonly string header_PODate = "P/O RECEIVED DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_DeliveredDate = "DELIVERED";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_DONo = "D/O #";
        readonly string header_DONoString = "D/O NO";
        readonly string header_DataType = "DATA TYPE";

      
       
        readonly string header_PONoString = "P/O NO";

        private DataTable dt_POList;
        private DataTable dt_DOList;
        private DataTable dt_Product;
        private DataTable dt_Item;
        private DataTable dt_Mat;
        private DataTable dt_ToUpdate;

        private bool Loaded = false;
        private bool dataChanged = false;
        private int oldToDeliveryQty = 0;

        private int toAssemblyProductQty = 0;

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PODate, typeof(DateTime));

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));

            return dt;
        }

        private DataTable NewPOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_ToDeliveryPCSQty, typeof(int));
           
            return dt;
        }

        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DataType, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));

            return dt;
        }

        private DataTable NewToUpdateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(string));
            dt.Columns.Add(header_ToDeliveryPCSQty, typeof(int));

            return dt;
        }

        private DataTable NewStockAlertTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));
            dt.Columns.Add(header_BalAfter, typeof(int));
        
            return dt;
        }

        private DataTable NewPlanningTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_SizeString, typeof(string));

            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_StockPcs, typeof(int));
            dt.Columns.Add(header_StockBag, typeof(int));
            dt.Columns.Add(header_StockString, typeof(string));
            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_BalAfterDelivery, typeof(string));
            dt.Columns.Add(header_BalAfterDeliveryInPcs, typeof(int));
            return dt;
        }

       
        private void AdjustDGVRowAlignment(DataGridView dgv)
        {
            string preItem = "";

            if(dgv.RowCount > 1)
            {
                dgv.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                dgv.Rows[0].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                dgv.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                dgv.Rows[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                dgv.Rows[1].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                dgv.Rows[1].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            }

            for(int i = 4; i < dgv.RowCount; i += 3)
            {
                //int test = (i + 1) % 3;

                

                if ((i + 2) % 3 == 0 )
                {
                    dgv.Rows[i - 2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    dgv.Rows[i - 2].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                    dgv.Rows[i - 2].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                    dgv.Rows[i - 2].Cells[header_Type].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                    dgv.Rows[i - 2].Cells[header_StockString].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                    DataGridViewRow row = dgv.Rows[i];
                    row.MinimumHeight = 2;
                    dgv.Rows[i].Height = 2;

                    string currentItem;

                    if (cmbProductSortBy.Text == text_Size)
                    {
                        currentItem = dgv.Rows[i - 1].Cells[header_SizeString].Value.ToString();
                    }
                    else
                    {
                        currentItem = dgv.Rows[i - 1].Cells[header_Type].Value.ToString();
                    }

                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;

                    if (preItem == "")
                    {
                        preItem = currentItem;
                        
                    }
                    else if(preItem != currentItem)
                    {
                        preItem = currentItem;
                        dgv.Rows[i - 3].DefaultCellStyle.BackColor = Color.Black;
                        //dgv.Rows[i - 3].Height = 40;
                    }
                  

                    dgv.Rows[i - 1].DefaultCellStyle.ForeColor = Color.Black;
                    dgv.Rows[i - 1].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

                    dgv.Rows[i - 1].Cells[header_BalAfterDelivery].InheritedStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

                    dgv.Rows[i - 1].Cells[header_Type].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Rows[i - 1].Cells[header_StockString].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Rows[i - 1].Cells[header_BalAfterDelivery].InheritedStyle.Alignment = DataGridViewContentAlignment.TopCenter;

                }
            }

           
        }

        private void DgvUIEdit(DataGridView dgv, int priorityColNum)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_Code].Visible = false;
            dgv.Columns[header_Size].Visible = false;
            dgv.Columns[header_Unit].Visible = false;
            dgv.Columns[header_StockPcs].Visible = false;
            dgv.Columns[header_StockBag].Visible = false;
            dgv.Columns[header_QtyPerBag].Visible = false;
            dgv.Columns[header_BalAfterDeliveryInPcs].Visible = false;
            dgv.Columns[header_BalAfterDelivery].Frozen = true;

            dgv.Columns[header_Code].DefaultCellStyle.ForeColor = Color.Gray;
            dgv.Columns[header_Code].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_StockString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            for (int i = 1; i <= priorityColNum; i++)
            {
                string colName = header_Priority + i;
                string colDeliveredName = header_PriorityDelivered + i;
                string colOrderName = header_PriorityOrder + i;
                string colTblCodeName = header_PriorityTblCode + i;
                string colPOCodeName = header_PriorityPOCode + i;
                string colPONoName = header_PriorityPONo + i;
                string colCustShortName = header_PriorityCustShortName + i;
                string colDeliveryStatusName = header_PriorityToDeliveryStatus + i;
                string colToDeliveryQtyName = header_PriorityToDeliveryQty + i;
                string colPlanningToDeliveryQtyName = header_PriorityToDeliveryPlanningInPCS + i;
                string colSelectedName = header_ColSelected + i;
                string colCustCodeName = header_PriorityCustCode + i;
                string colPODateName = header_PriorityPODate + i;

                dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[colName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                dgv.Columns[colDeliveredName].Visible = false;
                dgv.Columns[colOrderName].Visible = false;
                dgv.Columns[colCustShortName].Visible = false;
                dgv.Columns[colTblCodeName].Visible = false;
                dgv.Columns[colPOCodeName].Visible = false;
                dgv.Columns[colPONoName].Visible = false;
                dgv.Columns[colDeliveryStatusName].Visible = false;
                dgv.Columns[colToDeliveryQtyName].Visible = false;
                dgv.Columns[colPlanningToDeliveryQtyName].Visible = false;
                dgv.Columns[colSelectedName].Visible = false;
                dgv.Columns[colCustCodeName].Visible = false;
                dgv.Columns[colPODateName].Visible = false;

            }
        }

        private void AddToUpdateData(string POTblCode, int ToDeliveryQty)
        {
            if(dt_ToUpdate == null)
            {
                dt_ToUpdate = NewToUpdateTable();
            }

            bool dataExist = false;

            foreach(DataRow row in dt_ToUpdate.Rows)
            {
                if(POTblCode == row[header_POTblCode].ToString())
                {
                    dataExist = true;
                    row[header_ToDeliveryPCSQty] = ToDeliveryQty;
                }
            }

            if(!dataExist)
            {
                DataRow newRow = dt_ToUpdate.NewRow();

                newRow[header_POTblCode] = POTblCode;
                newRow[header_ToDeliveryPCSQty] = ToDeliveryQty;

                dt_ToUpdate.Rows.Add(newRow);
            }

        }

        private void LoadEditUnitCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(text_Unit);

            dt.Rows.Add(text_Bag);
            dt.Rows.Add(text_Pcs);

            cmb.DataSource = dt;
            cmb.DisplayMember = text_Unit;
        }

        private void LoadProductSortByCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(text_ProductSortBy);

            dt.Rows.Add(text_Size);
            dt.Rows.Add(text_Type);
            

            cmb.DataSource = dt;
            cmb.DisplayMember = text_ProductSortBy;
        }

        private void LoadPOSortByCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(text_POSortBy);

            dt.Rows.Add(text_ReceivedDate);
            dt.Rows.Add(text_Customer);
            //dt.Rows.Add(text_PriorityLvl);

            cmb.DataSource = dt;
            cmb.DisplayMember = text_POSortBy;
        }

        private void ResetPage()
        {
            dt_ToUpdate = null;
            toAssemblyProductQty = 0;
            btnUpdate.Visible = false;
            dataChanged = false;

            btnOpenDO.Text = text_AddDO;
            btnOpenDO.Enabled = true;

            cbFillALL.Checked = false;
            cbFillALL.Text = text_FillAll;

        }

        private void LoadPOList()
        {
            DialogResult dialogResult;

            if (dataChanged)
            {
                dialogResult = MessageBox.Show("Reload data will lose all the unsaved changes.\nAre you sure you want to reload data?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                dialogResult = DialogResult.Yes;
            }

            if (dialogResult == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                frmLoading.ShowLoadingScreen();

                ResetPage();

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                #region Load data from database

                DataTable dt_Planning = NewPlanningTable();

                DataRow newRow = dt_Planning.NewRow();
                dt_Planning.Rows.Add(newRow);
                newRow = dt_Planning.NewRow();
                dt_Planning.Rows.Add(newRow);

                string itemCust = text.SPP_BrandName;
                dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);


                dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC";
                dt_Product = dt_Product.DefaultView.ToTable();

                if (cmbProductSortBy.Text == text_Type)
                {
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }

                dt_Item = dalItem.Select();
                dt_POList = dalSPP.POSelect();
                dt_DOList = dalSPP.DOWithInfoSelect();

                if (cmbPOSortBy.Text == text_ReceivedDate)
                {
                    dt_POList.DefaultView.Sort = dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.CustomerTableCode + " ASC," + dalSPP.PriorityLevel + " ASC";
                }
                else if (cmbPOSortBy.Text == text_Customer)
                {
                    dt_POList.DefaultView.Sort = dalSPP.ShortName + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.PriorityLevel + " ASC";
                }
                else
                {
                    dt_POList.DefaultView.Sort = dalSPP.PriorityLevel + " ASC," + dalSPP.ShortName + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC";
                }


                dt_POList = dt_POList.DefaultView.ToTable();

                int index = 1;
                #endregion

                #region load product & stock

                foreach (DataRow row in dt_Product.Rows)
                {
                    string itemCode = row[dalSPP.ItemCode].ToString();
                    string itemName = row[dalSPP.ItemName].ToString();
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


                    DataRow planning_row = dt_Planning.NewRow();

                    string stockString = pcsStock + " PCS";

                    planning_row[header_Type] = itemCode;
                    planning_row[header_StockString] = stockString;

                    dt_Planning.Rows.Add(planning_row);


                    //stockString = bagStock + " BAGS (" + pcsStock + ")";
                    stockString = bagStock + " BAGS";

                    planning_row = dt_Planning.NewRow();

                    planning_row[header_Index] = index;

                    planning_row[header_Size] = size;
                    planning_row[header_Unit] = sizeUnit;
                    planning_row[header_SizeString] = sizeString;

                    planning_row[header_Type] = typeName;
                    planning_row[header_Code] = itemCode;
                    planning_row[header_StockPcs] = pcsStock;
                    planning_row[header_StockBag] = bagStock;
                    planning_row[header_StockString] = stockString;
                    planning_row[header_QtyPerBag] = qtyPerBag;
                    planning_row[header_BalAfterDelivery] = stockString;
                    planning_row[header_BalAfterDeliveryInPcs] = pcsStock;

                    dt_Planning.Rows.Add(planning_row);
                    index++;

                    dt_Planning.Rows.Add(dt_Planning.NewRow());
                }

                #endregion

                #region load PO data

                int prePOCode = -2;
                string PONo = "", ShortName = "";
                DateTime PODate = DateTime.MaxValue;
                int colNum = 0;
                string colName = "";
                string colDeliveredName = "";
                string colOrderName = "";
                string colTblCodeName = "";
                string colPOCodeName = "";
                string colPONoName = "";
                string colCustShortName = "";
                string colDeliveryStatusName = "";
                string colToDeliveryQtyName = "";
                string colPlanningToDeliveryQtyName = "";
                string colSelectedName = "";
                string colCustIDName = "";
                string colPODateName = "";

                foreach (DataRow row in dt_POList.Rows)
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                    bool isFreeze = bool.TryParse(row[dalSPP.Freeze].ToString(), out isFreeze) ? isFreeze : false;

                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                    int toDeliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out toDeliveryQty) ? toDeliveryQty : 0;
                    string poTblCode = row[dalSPP.TableCode].ToString();

                    int poCustTblCode = int.TryParse(row[dalSPP.CustomerTableCode].ToString(), out poCustTblCode) ? poCustTblCode : 0;

                    //PODate = DateTime.TryParse(row[dalSPP.PODate].ToString(), out PODate) ? PODate : 0;

                    //if(poTblCode == "727")
                    // {
                    //     float test = 0;
                    // }

                    if (!isRemoved && !isFreeze && deliveredQty < orderQty)
                    {
                        int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;
                        string poItemCode = row[dalSPP.ItemCode].ToString();

                        //if (poCode.ToString() == "122")
                        //{
                        //    float trest = 0;
                        //}

                        if (prePOCode != poCode)
                        {
                            #region add column to table planning

                            colNum++;
                            colName = header_Priority + colNum;
                            dt_Planning.Columns.Add(colName, typeof(string));

                            colDeliveryStatusName = header_PriorityToDeliveryStatus + colNum;
                            dt_Planning.Columns.Add(colDeliveryStatusName, typeof(string));

                            colDeliveredName = header_PriorityDelivered + colNum;
                            dt_Planning.Columns.Add(colDeliveredName, typeof(int));

                            colOrderName = header_PriorityOrder + colNum;
                            dt_Planning.Columns.Add(colOrderName, typeof(int));

                            colTblCodeName = header_PriorityTblCode + colNum;
                            dt_Planning.Columns.Add(colTblCodeName, typeof(string));

                            colPOCodeName = header_PriorityPOCode + colNum;
                            dt_Planning.Columns.Add(colPOCodeName, typeof(string));

                            colPONoName = header_PriorityPONo + colNum;
                            dt_Planning.Columns.Add(colPONoName, typeof(string));

                            colCustShortName = header_PriorityCustShortName + colNum;
                            dt_Planning.Columns.Add(colCustShortName, typeof(string));

                            colToDeliveryQtyName = header_PriorityToDeliveryQty + colNum;
                            dt_Planning.Columns.Add(colToDeliveryQtyName, typeof(int));

                            colPlanningToDeliveryQtyName = header_PriorityToDeliveryPlanningInPCS + colNum;
                            dt_Planning.Columns.Add(colPlanningToDeliveryQtyName, typeof(int));

                            colSelectedName = header_ColSelected + colNum;
                            dt_Planning.Columns.Add(colSelectedName, typeof(bool));

                            colCustIDName = header_PriorityCustCode + colNum;
                            dt_Planning.Columns.Add(colCustIDName, typeof(int));

                            colPODateName = header_PriorityPODate + colNum;
                            dt_Planning.Columns.Add(colPODateName, typeof(DateTime));

                            #endregion

                            prePOCode = poCode;

                            PONo = row[dalSPP.PONo].ToString();

                            PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                            ShortName = row[dalSPP.ShortName].ToString();

                        }

                        //add data
                        for (int i = 0; i < dt_Planning.Rows.Count; i++)
                        {
                            string planningCode = dt_Planning.Rows[i][header_Code].ToString();
                            int pcsPerBag = int.TryParse(dt_Planning.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : 1;

                            if (planningCode == poItemCode && i - 1 >= 0)
                            {
                                DataColumnCollection columns = dt_Planning.Columns;

                                if (columns.Contains(colName) && columns.Contains(colDeliveredName) && columns.Contains(colOrderName))
                                {
                                    int planningDelivered = int.TryParse(dt_Planning.Rows[i - 1][colDeliveredName].ToString(), out planningDelivered) ? planningDelivered : 0;
                                    int planningOrder = int.TryParse(dt_Planning.Rows[i - 1][colOrderName].ToString(), out planningOrder) ? planningOrder : 0;
                                    //int planningToDelivery = int.TryParse(dt_Planning.Rows[i][colOrderName].ToString(), out planningToDelivery) ? planningToDelivery : 0;

                                    planningDelivered += deliveredQty;
                                    planningOrder += orderQty;

                                    int divideBy = pcsPerBag;

                                    if (cmbEditUnit.Text == text_Pcs)
                                    {
                                        divideBy = 1;
                                    }

                                    //planningToDelivery =  toDeliveryQty;

                                    //planningDelivered /= pcsPerBag;
                                    //planningOrder /= pcsPerBag;


                                    dt_Planning.Rows[i - 1][colDeliveredName] = planningDelivered;
                                    dt_Planning.Rows[i - 1][colOrderName] = planningOrder;

                                    dt_Planning.Rows[i - 1][colTblCodeName] = poTblCode;
                                    dt_Planning.Rows[i - 1][colPOCodeName] = poCode;
                                    dt_Planning.Rows[i - 1][colPONoName] = PONo;
                                    dt_Planning.Rows[i - 1][colCustShortName] = ShortName;
                                    dt_Planning.Rows[i - 1][colCustIDName] = poCustTblCode;
                                    dt_Planning.Rows[i - 1][colPODateName] = PODate;

                                    //get DO to delivery qty from tbl_DO, and compare to planning toDelivery
                                    //input delivery status
                                    int DO_ToDeliveryQty = 0;
                                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                                    foreach (DataRow rowDO in dt_DOList.Rows)
                                    {
                                        bool isDORemoved = bool.TryParse(rowDO[dalSPP.IsRemoved].ToString(), out isDORemoved) ? isDORemoved : false;
                                        bool isDODelivered = bool.TryParse(rowDO[dalSPP.IsDelivered].ToString(), out isDODelivered) ? isDODelivered : false;
                                        string DO_POTblCode = rowDO[dalSPP.POTableCode].ToString();

                                        if (!isDORemoved && !isDODelivered && DO_POTblCode == poTblCode)
                                        {
                                            //get to delivery qty
                                            DO_ToDeliveryQty += int.TryParse(rowDO[dalSPP.ToDeliveryQty].ToString(), out DO_ToDeliveryQty) ? DO_ToDeliveryQty : 0;

                                        }
                                    }

                                    if (DO_ToDeliveryQty != 0)
                                    {
                                        if (DO_ToDeliveryQty == toDeliveryQty)
                                        {
                                            deliveryStatus = text_DeliveryStatus_Opened;
                                        }
                                        else
                                        {
                                            deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        }
                                    }

                                    dt_Planning.Rows[i][colName] = toDeliveryQty / divideBy;
                                    dt_Planning.Rows[i - 1][colPlanningToDeliveryQtyName] = toDeliveryQty;
                                    dt_Planning.Rows[i - 1][colDeliveryStatusName] = deliveryStatus;
                                    dt_Planning.Rows[i - 1][colToDeliveryQtyName] = DO_ToDeliveryQty;

                                    //string POData = " " + ShortName + " (" + PONo + "): " + DO_ToDeliveryQty / divideBy + "/" + planningDelivered / divideBy + "/" + planningOrder / divideBy + " ";

                                    string POData = " " + DO_ToDeliveryQty / divideBy + "/" + planningDelivered / divideBy + "/" + planningOrder / divideBy + " ";

                                    dt_Planning.Rows[i - 1][colName] = POData;

                                    dt_Planning.Rows[0][colName] = " " + ShortName + " (" + PONo + ") ";
                                    dt_Planning.Rows[0][colCustIDName] = poCustTblCode;
                                    dt_Planning.Rows[1][colName] = " "+PODate.ToShortDateString()+" ";

                                }
                                else
                                {
                                    MessageBox.Show("Columns not exist!");
                                }

                                break;
                            }
                        }


                    }

                }

                #endregion

                dgvList.SuspendLayout();

               

                dgvList.DataSource = dt_Planning;

                DgvUIEdit(dgvList, colNum);

                AdjustDGVRowAlignment(dgvList);//150ms

                AdjustToDeliveryBackColor();//905ms

                UpdateBalAfterDelivery();//441ms

                dgvList.ResumeLayout();//115ms

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;//327ms

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

                dgvList.ClearSelection();

                //298ms
                Cursor = Cursors.Arrow;

                frmLoading.CloseForm();
            }
          
        }

        private void AdjustToDeliveryBackColor()
        {
            DataGridView dgv = dgvList;

            for (int i = 2; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    int rowIndex = i;
                    int colIndex = j;

                    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
                    string colName = dgv.Columns[colIndex].Name;

                    if (colName.Contains(header_Priority) && int.TryParse(cellValue, out int x))
                    {
                        dgv.Rows[i].Cells[j].Style.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    }

                    // change to delivery qty fore color
                    if (cellValue == text_DeliveryStatus_ToOpen)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = Color.FromArgb(253, 203, 110);
                    }
                    else if (cellValue == text_DeliveryStatus_Opened)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = Color.FromArgb(0, 184, 148);
                    }
                    else if (cellValue == text_DeliveryStatus_OpenedWithDiff)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = Color.FromArgb(52, 160, 225);
                    }
                }
            }


        }

        private void frmSBBDeliveryPlanning_Load(object sender, EventArgs e)
        {

            LoadPOList();

            Loaded = true;
            BringToFront();
            Activate();
        }

        private void SwitchUnit()
        {
            Cursor = Cursors.WaitCursor;

            DataGridView dgv = dgvList;

            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        if (colName == searchingColumns)
                        {
                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;
                            int DOToDeliveryQtyInPcs = int.TryParse(dt.Rows[i - 1][header_PriorityToDeliveryQty + priorityLvl.ToString()].ToString(), out DOToDeliveryQtyInPcs) ? DOToDeliveryQtyInPcs : 0;

                            int planToDeliveryQty = int.TryParse(dt.Rows[i - 1][header_PriorityToDeliveryPlanningInPCS + priorityLvl].ToString(), out planToDeliveryQty) ? planToDeliveryQty : 0;
 
                            string PONo = dt.Rows[i - 1][header_PriorityPONo + priorityLvl].ToString();
                            string ShortName = dt.Rows[i - 1][header_PriorityCustShortName + priorityLvl].ToString();

                            if(!string.IsNullOrEmpty(PONo))
                            {
                                if (cmbEditUnit.Text == text_Bag)
                                {
                                    //pcs switch to bag
                                    DOToDeliveryQtyInPcs /= pcsPerBag;
                                    deliveredQty /= pcsPerBag;
                                    orderQty /= pcsPerBag;
                                    planToDeliveryQty /= pcsPerBag;
                                }
                                else
                                {
                                    //bag switch to pcs
                                    //planToDeliveryQty *= pcsPerBag;
                                }

                                string POData = " " + ShortName + " (" + PONo + "): " + DOToDeliveryQtyInPcs + "/" + deliveredQty + "/" + orderQty + " ";

                                dt.Rows[i - 1][searchingColumns] = POData;
                                dt.Rows[i][searchingColumns] = planToDeliveryQty;
                               
                            }

                            priorityLvl++;
                        }
                    }
                }
            }

            Cursor = Cursors.Arrow;


        }

        private void cmbEditUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                SwitchUnit();
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
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

        private void dgvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                int toDeliveryQty = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out toDeliveryQty) ? toDeliveryQty : -1;

                if (dgv.Columns[colIndex].Name.Contains(header_Priority) && toDeliveryQty >= 0) //Desired Column
                {
                    oldToDeliveryQty = toDeliveryQty;

                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
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

        private DataTable LoadMatPartList(DataTable dt_Product)
        {
            DataTable dt_MatPart = NewStockAlertTable();
            DataTable dt_Plan = dalPlan.Select();

            int index = 1;
            DataTable dtJoin = dalJoin.Select();

            foreach (DataRow row in dt_Product.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_ItemCode].ToString());

                int stillNeed = int.TryParse(row[header_BalAfter].ToString(), out stillNeed) ? stillNeed : 0;
             

                stillNeed = stillNeed > 0 ? 0 : stillNeed * -1;
             
                foreach (DataRow rowJoin in dtJoin.Rows)
                {
                    if (rowJoin[dalJoin.ParentCode].ToString() == parentCode)
                    {
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

                        int readyStock = int.TryParse(rowJoin[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                        int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                        int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                        int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                        int child_StillNeed = 0;
                     
                        joinMax = joinMax <= 0 ? 1 : joinMax;
                        joinMin = joinMin <= 0 ? 1 : joinMin;

                        child_StillNeed = stillNeed / joinMax * joinQty;

                        child_StillNeed = stillNeed % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;

                        int bal = readyStock - child_StillNeed;

                        bool childInserted = false;

                        foreach (DataRow mat_row in dt_MatPart.Rows)
                        {
                            if (childCode == mat_row[header_ItemCode].ToString())
                            {
                                childInserted = true;

                                int previousBal = int.TryParse(mat_row[header_BalAfter].ToString(), out previousBal) ? previousBal : 0;

                                bal = previousBal - child_StillNeed;

                                mat_row[header_BalAfter] = bal;
                              
                                break;
                            }
                        }

                        if (!childInserted)//!childInserted
                        {
                            DataRow alert_row = dt_MatPart.NewRow();

                            alert_row[header_ItemCode] = childCode;
                            alert_row[header_ItemName] = childName;
                            alert_row[header_BalAfter] = bal;

                            dt_MatPart.Rows.Add(alert_row);
                            index++;
                        }


                    }
                }
            }

            return dt_MatPart;
        }

        private void CheckIfProductionNeeded()
        {
            bool productionNeeded = false;

            DataTable dt = (DataTable)dgvList.DataSource;

            DataTable dt_Product = NewStockAlertTable();

            //get to assembly product list
            foreach (DataRow row in dt.Rows)
            {
                int bal = int.TryParse(row[header_BalAfterDeliveryInPcs].ToString(), out bal) ? bal : 0;

                if (bal < 0)
                {
                    DataRow newRow = dt_Product.NewRow();

                    newRow[header_ItemCode] = row[header_Code].ToString();
                    newRow[header_BalAfter] = bal;

                    dt_Product.Rows.Add(newRow);
                }
            }

             dt_Mat = LoadMatPartList(dt_Product);

            foreach(DataRow row in dt_Mat.Rows)
            {
                int bal = int.TryParse(row[header_BalAfter].ToString(), out bal) ? bal : 0;

                if(bal < 0)
                {
                    productionNeeded = true;
                    break;
                }
            }
            
            if(productionNeeded)
            {
                lblProductionAlert.Visible = true;
            }
            else
            {
                lblProductionAlert.Visible = false;
            }

        }

        private void CheckIfAssemblyNeeded(DataTable dt)
        {
            bool assemblyNeeded = false;
            foreach(DataRow row in dt.Rows)
            {
                string balString = row[header_BalAfterDelivery].ToString().Replace(" BAGS", "");
                int bal = int.TryParse(balString, out bal) ? bal : 0;

                if(bal < 0)
                {
                    assemblyNeeded = true;
                    break;
                }
            }

            if(assemblyNeeded)
            {
                lblAssemblyNeeded.Text = toAssemblyProductQty + " PRODUCT(S) " + text_AssemblyNeeded;
                lblAssemblyNeeded.Visible = true;
            }
            else
            {
                lblAssemblyNeeded.Visible = false;
            }

            CheckIfProductionNeeded();
        }

        private void UpdateBalAfterDelivery()
        {
            DataTable dt = (DataTable)dgvList.DataSource;

            int rowIndex = 0;
            int TotalAssemblyNeededInBags = 0;
            int TotalAssemblyNeededInPcs = 0;

            foreach (DataRow row in dt.Rows)
            {
                int pcsPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : 0;
                int stockInPcs = int.TryParse(row[header_StockPcs].ToString(), out stockInPcs) ? stockInPcs :0;
                
                if(pcsPerBag > 0)
                {
                    int TotalToDeliveryQty = 0;

                    foreach (DataColumn col in dt.Columns)
                    {
                        string colName = col.ColumnName;
                        int toDelivery = int.TryParse(dt.Rows[rowIndex][colName].ToString(), out toDelivery) ? toDelivery : -1;

                        if (colName.Contains(header_Priority) && toDelivery >= 0)
                        {
                            TotalToDeliveryQty += toDelivery;
                        }
                    }

                    int divideBy = pcsPerBag;

                    if (cmbEditUnit.Text == text_Pcs)
                    {
                        divideBy = 1;
                    }

                    int balInPcs = stockInPcs - TotalToDeliveryQty * divideBy;

                    int balAfterDelivery = balInPcs / pcsPerBag;

                    dt.Rows[rowIndex][header_BalAfterDelivery] = balAfterDelivery + " BAGS";
                    dt.Rows[rowIndex][header_BalAfterDeliveryInPcs] = balInPcs;

                    int test = balInPcs / pcsPerBag;

                    if (balInPcs < 0)
                    {
                        if (dgvList.Rows[rowIndex].Cells[header_BalAfterDelivery].InheritedStyle.ForeColor == Color.Black)
                        {
                            toAssemblyProductQty++;
                        }

                        dgvList.Rows[rowIndex].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Red;
                        TotalAssemblyNeededInBags += balInPcs / pcsPerBag;
                        TotalAssemblyNeededInPcs += balInPcs;
                    }
                    else
                    {
                        if (dgvList.Rows[rowIndex].Cells[header_BalAfterDelivery].InheritedStyle.ForeColor == Color.Red)
                        {
                            toAssemblyProductQty--;
                        }

                        dgvList.Rows[rowIndex].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Black;

                    }
                }

                rowIndex++;

            }

           

            CheckIfAssemblyNeeded(dt);
            lblAssemblyNeeded.Text += " (" + TotalAssemblyNeededInBags + " bags / " + TotalAssemblyNeededInPcs + " pcs)";
        }

        private void UpdateBalAfterDelivery(int row)
        {
            DataTable dt = (DataTable)dgvList.DataSource;

            int pcsPerBag = int.TryParse(dt.Rows[row][header_QtyPerBag].ToString(), out pcsPerBag)? pcsPerBag : 1;
            int stockInPcs = int.TryParse(dt.Rows[row][header_StockPcs].ToString(), out stockInPcs) ? stockInPcs : 0;

            int TotalToDeliveryQty = 0;
            int TotalAssemblyNeededInBags = 0;
            int TotalAssemblyNeededInPcs = 0;

            foreach (DataColumn col in dt.Columns)
            {
                string colName = col.ColumnName;
                int toDelivery = int.TryParse(dt.Rows[row][colName].ToString(), out toDelivery) ? toDelivery : -1;

                if(colName.Contains(header_Priority) && toDelivery >= 0)
                {
                    TotalToDeliveryQty += toDelivery;
                }
            }

            int divideBy = pcsPerBag;

            if(cmbEditUnit.Text == text_Pcs)
            {
                divideBy = 1;
            }

            int balInPcs = stockInPcs - TotalToDeliveryQty * divideBy;

            dt.Rows[row][header_BalAfterDelivery] = balInPcs/pcsPerBag + " BAGS";
            dt.Rows[row][header_BalAfterDeliveryInPcs] = balInPcs;


            if (balInPcs < 0)
            {
                //Color test = dgvList.Rows[row].Cells[header_BalAfterDelivery].InheritedStyle.ForeColor;

                if (dgvList.Rows[row].Cells[header_BalAfterDelivery].InheritedStyle.ForeColor == Color.Black)
                {
                    toAssemblyProductQty ++;
                }

                dgvList.Rows[row].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Red;
                TotalAssemblyNeededInBags += balInPcs / pcsPerBag;
                TotalAssemblyNeededInPcs += balInPcs;

            }
            else
            {
                if(dgvList.Rows[row].Cells[header_BalAfterDelivery].InheritedStyle.ForeColor == Color.Red)
                {
                    toAssemblyProductQty --;
                }

                dgvList.Rows[row].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Black;

            }

            CheckIfAssemblyNeeded(dt);

            lblAssemblyNeeded.Text += " (" + TotalAssemblyNeededInBags+" bags / "+ TotalAssemblyNeededInPcs + " pcs)";
        }

        private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvList;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if(rowIndex - 1 >= 0)
            {
                string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                if(int.TryParse(cellValue, out int x))
                {
                    dataChanged = true;
                    btnUpdate.Visible = true;

                    //update balance
                    UpdateBalAfterDelivery();
                    //UpdateBalAfterDelivery(rowIndex);
                    //get priority level
                    string colName = dgv.Columns[colIndex].Name;

                    string priorityLvl = colName.Replace(header_Priority, "");

                    //get DO to delivery qty
                    string DOToDeliveryQtyInPcs = dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();
                    string POTblCode = dgv.Rows[rowIndex - 1].Cells[header_PriorityTblCode + priorityLvl].Value.ToString();

                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                    int pcsPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out pcsPerBag) ? pcsPerBag : 1;

                    int newToDeliveryQty = int.TryParse(cellValue, out newToDeliveryQty) ? newToDeliveryQty : 0;


                    
                    if (cmbEditUnit.Text == text_Bag)
                    {
                        newToDeliveryQty *= pcsPerBag;
                    }

                    dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = newToDeliveryQty;

                    //change delivery status color
                    if (int.TryParse(DOToDeliveryQtyInPcs, out int i))
                    {
                        AddToUpdateData(POTblCode, newToDeliveryQty);

                        if (DOToDeliveryQtyInPcs != "0")
                        {
                            if (DOToDeliveryQtyInPcs == newToDeliveryQty.ToString())
                            {
                                deliveryStatus = text_DeliveryStatus_Opened;
                                dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(0, 184, 148);
                            }
                            else
                            {
                                deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(52, 160, 225);
                                //dgv.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.White;
                            }
                        }
                        else
                        {
                            dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(253, 203, 110);

                        }
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                    }

                    dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;
                    //MessageBox.Show("Different");
                }
            }
           
            
        }

        private void AutoFillInAll(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        if (colName == searchingColumns)
                        {
                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;

                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;

                                AddToUpdateData(POTblCode, stillNeed);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                            }

                            priorityLvl++;
                        }
                    }


                }



            }
        }

        private void AutoFill(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        bool colSelected = false;

                        if (colName == searchingColumns)
                        colSelected = bool.TryParse(dt.Rows[0][header_ColSelected + priorityLvl.ToString()].ToString(), out colSelected) ? colSelected : false;

                        if (colName == searchingColumns && colSelected)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;
                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;

                                AddToUpdateData(POTblCode, stillNeed);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                            }
                           

                            
                        }

                        if (colName == searchingColumns)
                        priorityLvl++;
                    }

                   
                }



            }
        }

        private void NewAutoFill(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        bool colSelected = false;

                        if (colName == searchingColumns)
                            colSelected = dgv.Rows[i].Cells[j].Selected;

                        if (colName == searchingColumns && colSelected)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;

                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;
                                
                                AddToUpdateData(POTblCode, stillNeed);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                            }



                        }

                        if (colName == searchingColumns)
                            priorityLvl++;


                    }


                }



            }

            if(dataChanged)
            {
                UpdateBalAfterDelivery();
            }
        }

        private void NewAutoFillAll(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        if (colName == searchingColumns)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;

                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;

                                AddToUpdateData(POTblCode, stillNeed);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                            }



                        }

                        if (colName == searchingColumns)
                            priorityLvl++;


                    }


                }



            }

            if (dataChanged)
            {
                UpdateBalAfterDelivery();
            }
        }

        private void NewAutoFillAllByCustomer(DataGridView dgv,int colIndex)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            

            string colName = dgv.Columns[colIndex].Name;
            int custID = 0;

            if (colName.Contains(header_Priority))
            {
                string priorityLevel = colName.Replace(header_Priority, "");
                custID = int.TryParse(dt.Rows[0][header_PriorityCustCode + priorityLevel].ToString(), out custID) ? custID : -1;
            }

            if(custID > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                    if (pcsPerBag != -1)
                    {
                        int priorityLvl = 1;

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            colName = dgv.Columns[j].Name;

                            string searchingColumns = header_Priority + priorityLvl.ToString();

                            int loopingCustID = -1;

                            if(dt.Columns.Contains(header_PriorityCustCode + priorityLvl))
                            {
                                loopingCustID = int.TryParse(dt.Rows[0][header_PriorityCustCode + priorityLvl].ToString(), out loopingCustID) ? loopingCustID : -1;
                            }
                            

                            if (colName == searchingColumns && loopingCustID == custID)
                            {
                                dgv.Columns[j].Selected = true;

                                string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                                //get order, delivered, pcs
                                int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                                int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                                //calculate still need
                                int stillNeed = orderQty - deliveredQty;

                                if (stillNeed > 0)
                                {
                                    dataChanged = true;
                                    btnUpdate.Visible = true;

                                    AddToUpdateData(POTblCode, stillNeed);

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                    dt.Rows[i][searchingColumns] = stillNeed;

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                    //get DO to delivery qty
                                    string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                                    //change delivery status color
                                    if (DOToDeliveryQtyInPcs != "0")
                                    {
                                        if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                        {
                                            deliveryStatus = text_DeliveryStatus_Opened;
                                            dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                        }
                                        else
                                        {
                                            deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                            dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                        }
                                    }
                                    else
                                    {
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                    }

                                    dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                                }



                            }

                            if (colName == searchingColumns)
                                priorityLvl++;
                        }
                    }
                }
            }
            

            if (dataChanged)
            {
                UpdateBalAfterDelivery();
            }
        }

        private void ResetAllByCustomer(DataGridView dgv, int colIndex)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;


            string colName = dgv.Columns[colIndex].Name;
            int custID = 0;

            if (colName.Contains(header_Priority))
            {
                string priorityLevel = colName.Replace(header_Priority, "");
                custID = int.TryParse(dt.Rows[0][header_PriorityCustCode + priorityLevel].ToString(), out custID) ? custID : -1;
            }

            if(custID > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                    if (pcsPerBag != -1)
                    {
                        int priorityLvl = 1;

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            colName = dgv.Columns[j].Name;

                            string searchingColumns = header_Priority + priorityLvl.ToString();

                            int loopingCustID = -1;

                            if (dt.Columns.Contains(header_PriorityCustCode + priorityLvl))
                            {
                                loopingCustID = int.TryParse(dt.Rows[0][header_PriorityCustCode + priorityLvl].ToString(), out loopingCustID) ? loopingCustID : -1;
                            }


                            if (colName == searchingColumns && loopingCustID == custID)
                            {
                                dgv.Columns[j].Selected = true;

                                string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                                //get order, delivered, pcs
                                int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                                int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                                //calculate still need
                                int stillNeed = orderQty - deliveredQty;

                                if (stillNeed > 0)
                                {
                                    dataChanged = true;
                                    btnUpdate.Visible = true;

                                    AddToUpdateData(POTblCode, 0);

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                    dt.Rows[i][searchingColumns] = 0;

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                    //get DO to delivery qty
                                    string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                                    if (DOToDeliveryQtyInPcs != "0")
                                    {
                                        if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                        {
                                            deliveryStatus = text_DeliveryStatus_Opened;
                                            dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                        }
                                        else
                                        {
                                            deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                            dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                        }
                                    }
                                    else
                                    {
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                    }

                                    dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;


                                }

                            }


                            if (colName == searchingColumns)
                                priorityLvl++;
                        }


                    }



                }
            }

           

            if (dataChanged)
            {
                UpdateBalAfterDelivery();
            }
        }

        private void CalculateTotalBag(DataGridView dgv)
        {
    
            DataTable dt = (DataTable)dgv.DataSource;

            int totalBag = 0;
            int totalPcs = 0;

            int totalCellSelected = 0;
            int totalColSelected = 0;

            //dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        bool cellSelected = false;


                        if (colName == searchingColumns)
                            cellSelected = dgv.Rows[i].Cells[j].Selected;

                        if (colName == searchingColumns && cellSelected)
                        {

                            

                            int toDeliverQty = int.TryParse(dt.Rows[i][searchingColumns].ToString(), out toDeliverQty) ? toDeliverQty : -1;

                            
                            if(toDeliverQty > -1)
                            {
                                totalCellSelected++;
                            }

                            if (toDeliverQty > 0)
                            {
                                

                                totalBag += cmbEditUnit.Text != text_Bag ? toDeliverQty / pcsPerBag : toDeliverQty;
                                totalPcs += cmbEditUnit.Text != text_Bag ? toDeliverQty : toDeliverQty * pcsPerBag;

                            }



                        }

                        if (colName == searchingColumns)
                            priorityLvl++;


                    }


                }



            }

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string colName = dgv.Columns[j].Name;
                bool colSelected = dgv.Columns[j].Selected;

                if (colName.Contains(header_Priority) && colSelected)
                {
                    totalColSelected++;

                }

            }

            int totalSelected = totalCellSelected;

            string selectedType = "cell";

            string totalSelected_Text = " (" + totalCellSelected + " Cells)";

            if (dgv.SelectionMode == DataGridViewSelectionMode.FullColumnSelect)
            {
                totalSelected = totalColSelected;
                selectedType = "column";
            }

            if(totalSelected > 1)
            {
                selectedType += "s";
            }

            totalSelected_Text = " (" + totalSelected + " " + selectedType + ")";

            lblTotalBag.Text = totalBag + " BAG(s) / " + totalPcs +" PCS "+ text_Selected + totalSelected_Text;

        }

        private void NewTrip(DataGridView dgv)
        {
            //dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;

            bool result = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        bool colSelected = false;

                        if (colName == searchingColumns)
                            colSelected = dgv.Rows[i].Cells[j].Selected;

                        if (colName == searchingColumns && colSelected)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            ////get order, delivered, pcs
                            //int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            //int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int toDeliveredQty = int.TryParse(dt.Rows[i][searchingColumns].ToString(), out toDeliveredQty) ? toDeliveredQty : 0;

                            if (toDeliveredQty > 0)
                            {
                                toDeliveredQty = cmbEditUnit.Text == text_Bag ? toDeliveredQty * pcsPerBag : toDeliveredQty;

                                //get customer id
                                //get route tbl code
                                string CustTblCode = dt.Rows[i - 1][header_PriorityCustCode + priorityLvl.ToString()].ToString();

                                DataTable dt_Cust = dalSPP.CustomerWithoutRemovedDataSelect();

                                int routeTblCode = 0;

                                foreach (DataRow custRow in dt_Cust.Rows)
                                {
                                    if (CustTblCode == custRow[dalSPP.TableCode].ToString())
                                    {
                                        routeTblCode = int.TryParse(custRow[dalSPP.RouteTblCode].ToString(), out routeTblCode)? routeTblCode : 0;
                                        break;
                                    }
                                }


                                uSpp.Route_tbl_code = routeTblCode;
                                uSpp.PO_tbl_code = Convert.ToInt32(POTblCode);
                                uSpp.Delivery_status = text.Delivery_Processing;
                                uSpp.Deliver_pcs = toDeliveredQty;

                                if(!dalSPP.InsertDelivery(uSpp))
                                {
                                    MessageBox.Show("Failed to insert new trip data to database!");
                                    break;
                                }
                                else
                                {
                                    result = true;
                                }
                               

                            }



                        }

                        if (colName == searchingColumns)
                            priorityLvl++;


                    }


                }



            }

         
            if(result)
            {
                MessageBox.Show("New trip added!");
            }
        }

        private void NewTripTest(DataGridView dgv)
        {
            //dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;

            DataTable dt_Trip = dalSPP.DeliveryOWithoutRemovedDataSelect();

            DataView dv = dt_Trip.DefaultView;
            dv.Sort = dalSPP.PlanningNo + " desc";
            DataTable sortedDT = dv.ToTable();

            int newPlanningNo = 1;

            if(sortedDT.Rows.Count > 0)
            newPlanningNo = int.TryParse(sortedDT.Rows[0][dalSPP.PlanningNo].ToString(), out newPlanningNo)? newPlanningNo + 1 : 1;

            bool result = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        bool colSelected = false;

                        if (colName == searchingColumns)
                            colSelected = dgv.Rows[i].Cells[j].Selected;

                        if (colName == searchingColumns && colSelected)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            ////get order, delivered, pcs
                            //int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            //int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int toDeliveredQty = int.TryParse(dt.Rows[i][searchingColumns].ToString(), out toDeliveredQty) ? toDeliveredQty : 0;

                            if (toDeliveredQty > 0)
                            {
                                toDeliveredQty = cmbEditUnit.Text == text_Bag ? toDeliveredQty * pcsPerBag : toDeliveredQty;

                                //get customer id
                                //get route tbl code
                                string CustTblCode = dt.Rows[i - 1][header_PriorityCustCode + priorityLvl.ToString()].ToString();

                                DataTable dt_Cust = dalSPP.CustomerWithoutRemovedDataSelect();

                                int routeTblCode = 0;

                                foreach (DataRow custRow in dt_Cust.Rows)
                                {
                                    if (CustTblCode == custRow[dalSPP.TableCode].ToString())
                                    {
                                        routeTblCode = int.TryParse(custRow[dalSPP.RouteTblCode].ToString(), out routeTblCode) ? routeTblCode : 0;
                                        break;
                                    }
                                }


                                uSpp.Route_tbl_code = routeTblCode;
                                uSpp.Planning_no = newPlanningNo;

                                uSpp.PO_tbl_code = Convert.ToInt32(POTblCode);
                                uSpp.Delivery_status = text.Delivery_Processing;
                                uSpp.Deliver_pcs = toDeliveredQty;

                                if (!dalSPP.InsertDelivery(uSpp))
                                {
                                    MessageBox.Show("Failed to insert new trip data to database!");
                                    break;
                                }
                                else
                                {
                                    result = true;
                                }


                            }



                        }

                        if (colName == searchingColumns)
                            priorityLvl++;


                    }


                }



            }


            if (result)
            {
                MessageBox.Show("New trip added!");
            }
        }

        private void Reset(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        bool colSelected = false;

                        if (colName == searchingColumns)
                            colSelected = dgv.Rows[i].Cells[j].Selected;

                        if (colName == searchingColumns && colSelected)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;

                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;

                                AddToUpdateData(POTblCode, 0);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = 0;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                               // dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                            }

                        }


                        if (colName == searchingColumns)
                            priorityLvl++;
                    }


                }



            }

            if (dataChanged)
            {
                UpdateBalAfterDelivery();
            }
        }

        private void ResetAll(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        if (colName == searchingColumns)
                        {
                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;

                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;

                                AddToUpdateData(POTblCode, 0);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = 0;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;


                            }

                        }


                        if (colName == searchingColumns)
                            priorityLvl++;
                    }


                }



            }

            if (dataChanged)
            {
                UpdateBalAfterDelivery();
            }
        }

        private void btnFillALL_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DataGridView dgv = dgvList;

            lblFillSettingReset.Visible = true;

            string fillStatus = btnFillALL.Text;

            if(fillStatus == text_FillBySystem)
            {
                if(cbFillALL.Checked)
                {
                    AutoFillInAll(dgv);
                }
                else
                {
                    btnFillALL.Text = text_SelectCol;
                    btnFillALL.Enabled = false;

                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    foreach (DataGridViewColumn c in dgv.Columns)
                    {
                        c.SortMode = DataGridViewColumnSortMode.NotSortable;
                        c.Selected = false;
                    }

                    //dgv.columnsor
                    dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                }
            }
            else if(fillStatus == text_FillIn)
            {

                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv.EnableHeadersVisualStyles = true;

                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.Automatic;
                    c.Selected = false;
                }

                if (cbFillALL.Checked)
                {
                    AutoFillInAll(dgv);
                }
                else
                {
                    AutoFill(dgv);
                }
                

                DeselectedAllColumn(dgv);
                btnFillALL.Text = text_FillBySystem;

                lblFillSettingReset.Visible = false;

            }
            
            UpdateBalAfterDelivery();

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgv.ResumeLayout();
            Cursor = Cursors.Arrow;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool dataUpdated = true;

            if(dt_ToUpdate != null)
            { 
                uSpp.Updated_By = MainDashboard.USER_ID;
                uSpp.Updated_Date = DateTime.Now;

                foreach (DataRow row in dt_ToUpdate.Rows)
                {
                    uSpp.Table_Code = int.TryParse(row[header_POTblCode].ToString(), out int i) ? i : 0;
                    uSpp.To_delivery_qty = int.TryParse(row[header_ToDeliveryPCSQty].ToString(), out  i) ? i : 0;

                    dataUpdated = dalSPP.POToDeliveryDataUpdate(uSpp);
                    if (!dataUpdated)
                    {
                        MessageBox.Show("Failed to update to delivery qty.\nPO Table Code: "+ uSpp.Table_Code +"\nTo Delivery Qty: "+ uSpp.To_delivery_qty);
                    }
                }
             
                if(dataUpdated)
                {
                    MessageBox.Show("All changed data updated!");
                }

                dataChanged = false;
                btnUpdate.Visible = false;
            }
          
            
        }

        private void frmSBBDeliveryPlanning_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dataChanged)
            {
                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                   
                }
                else
                {
                    e.Cancel = true;
                }
                  
            }
            
        }

        private void lblProductionAlert_Click(object sender, EventArgs e)
        {
            if(dt_Mat != null)
            {

                dt_Mat.DefaultView.Sort = header_BalAfter + " ASC";
                dt_Mat = dt_Mat.DefaultView.ToTable();

                frmShowDataTable frm = new frmShowDataTable(dt_Mat)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("no data");
            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbProductSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                LoadPOList();
            }
        }

        private void cmbPOSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Loaded)
            {
                LoadPOList();
            }
        }

        private void dgvList_DragDrop(object sender, DragEventArgs e)
        {
            
           
        }

        private void dgvList_DragEnter(object sender, DragEventArgs e)
        {
  
        }

        private void dgvList_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
           
           // MessageBox.Show("Drag Drop" + e.Column.Index);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (Loaded)
            {
                LoadPOList();
            }
        }

       
        private bool IfColSelected(DataGridView dgv)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            for(int col = 0; col < dt.Columns.Count; col++)
            {
                bool ifSelected = bool.TryParse(dt.Rows[0][col].ToString(), out ifSelected) ? ifSelected : false;

                if(ifSelected)
                {
                    return true;
                }
            }

            return false;
        }


        private void DeselectedAllColumn(DataGridView dgv)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            for (int col = 0; col < dt.Columns.Count; col++)
            {
                bool ifSelected = bool.TryParse(dt.Rows[0][col].ToString(), out ifSelected) ? ifSelected : false;

                if (ifSelected)
                {
                    dt.Rows[0][col] = false;

                    string colName = dgv.Columns[col].Name;

                    string level = colName.Replace(header_ColSelected, "");

                    dgv.Columns[header_Priority + level].HeaderCell.Style.BackColor = Color.White;
                    dgv.Columns[header_Priority + level].HeaderCell.Style.ForeColor = Color.Black;
                }
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvList;

            if (e.RowIndex != -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv.Rows[row].Cells[col].Selected = true;
            }

            //CalculateTotalBag(dgv);


            string fillStatus = btnFillALL.Text;

            int colIndex = e.ColumnIndex;

            if((fillStatus == text_SelectCol || fillStatus == text_FillIn) && dgvList.SelectedColumns.Count > 0)
            {
                dgv.EnableHeadersVisualStyles = false;

                string colName = dgv.Columns[colIndex].Name;

                if(colName.Contains(header_Priority))
                {
                    string priorityLevel = colName.Replace(header_Priority, "");

                    bool selected = bool.TryParse(dgv.Rows[0].Cells[header_ColSelected + priorityLevel].Value.ToString(), out selected) ? selected : false;

                    if (!selected)
                    {
                        dgv.Rows[0].Cells[header_ColSelected + priorityLevel].Value = true;
                        dgv.Columns[colIndex].HeaderCell.Style.BackColor = SystemColors.Highlight;
                        dgv.Columns[colIndex].HeaderCell.Style.ForeColor = Color.White;
                      
                    }
                    else 
                    {
                        dgv.Rows[0].Cells[header_ColSelected + priorityLevel].Value = false;
                        dgv.Columns[colIndex].HeaderCell.Style.BackColor = Color.White;
                        dgv.Columns[colIndex].HeaderCell.Style.ForeColor = Color.Black;
                    }


                }

                dgv.Columns[colIndex].Selected = false;

                if (IfColSelected(dgv))
                {
                    btnFillALL.Text = text_FillIn;
                    btnFillALL.Enabled = true;
                }
                else
                {
                    btnFillALL.Text = text_SelectCol;
                    btnFillALL.Enabled = false;
                }

            }


            string DOStatus = btnOpenDO.Text;

            if ((DOStatus == text_SelectCol || DOStatus == text_OpenDO) && dgvList.SelectedColumns.Count > 0)
            {
                dgv.EnableHeadersVisualStyles = false;

                string colName = dgv.Columns[colIndex].Name;

                if (colName.Contains(header_Priority))
                {
                    string priorityLevel = colName.Replace(header_Priority, "");

                    bool selected = bool.TryParse(dgv.Rows[0].Cells[header_ColSelected + priorityLevel].Value.ToString(), out selected) ? selected : false;

                    if (!selected)
                    {
                        dgv.Rows[0].Cells[header_ColSelected + priorityLevel].Value = true;
                        dgv.Columns[colIndex].HeaderCell.Style.BackColor = SystemColors.Highlight;
                        dgv.Columns[colIndex].HeaderCell.Style.ForeColor = Color.White;

                    }
                    else
                    {
                        dgv.Rows[0].Cells[header_ColSelected + priorityLevel].Value = false;
                        dgv.Columns[colIndex].HeaderCell.Style.BackColor = Color.White;
                        dgv.Columns[colIndex].HeaderCell.Style.ForeColor = Color.Black;
                    }


                }

                dgv.Columns[colIndex].Selected = false;

                if (IfColSelected(dgv))
                {
                    btnOpenDO.Text = text_OpenDO;
                    btnOpenDO.Enabled = true;
                }
                else
                {
                    btnOpenDO.Text = text_SelectCol;
                    btnOpenDO.Enabled = false;
                }

            }

            //dgv.EnableHeadersVisualStyles = true;
        }

        private void OpenDO(DataGridView dgv)
        {
            var POData = GetPOData(dgv);

            DataTable dt = POData.Item1;
            DataTable dt_POItem = POData.Item2;
         
            if(dt.Rows.Count > 0)
            {
                frmSPPPOList frm = new frmSPPPOList(dt, dt_POItem, false)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();


            }
            else
            {
                MessageBox.Show("Valid delivery data not found!\n\n>To delivery qty cannot be 0\n>Check if D/O have been opened");
            }
        }


        private Tuple<DataTable, DataTable> GetPOData(DataGridView dgv)
        {
            DataTable dt = NewPOTable();
            DataTable dty_POItem = NewPOItemTable();

            DataTable dt_Source = (DataTable) dgv.DataSource;

            for(int col = 0; col < dt_Source.Columns.Count; col++)
            {
                bool colSelected = bool.TryParse(dt_Source.Rows[0][col].ToString(), out colSelected) ? colSelected : false;

                if(colSelected)
                {
                    string colName = dt_Source.Columns[col].ColumnName;

                    string level = colName.Replace(header_ColSelected, "");


                    for(int row = 1; row < dt_Source.Rows.Count; row++)
                    {
                        int toDeliveryQty = int.TryParse(dt_Source.Rows[row][header_Priority + level].ToString(), out toDeliveryQty) ? toDeliveryQty : -1;
                        int toDeliveryQtyInPcs = int.TryParse(dt_Source.Rows[row - 1][header_PriorityToDeliveryPlanningInPCS + level].ToString(), out toDeliveryQtyInPcs) ? toDeliveryQtyInPcs : -1;
                        int DOToDeliveryQty = int.TryParse(dt_Source.Rows[row - 1][header_PriorityToDeliveryQty + level].ToString(), out DOToDeliveryQty) ? DOToDeliveryQty : -1;
                        //&& toDeliveryQtyInPcs != DOToDeliveryQty

                        if (toDeliveryQty > 0 )
                        {
                            DataRow newRow = dt.NewRow();

                            newRow[header_PONoString] = dt_Source.Rows[row - 1][header_PriorityPONo + level];
                            newRow[header_POCode] = dt_Source.Rows[row - 1][header_PriorityPOCode + level];
                            newRow[header_PODate] = dt_Source.Rows[row - 1][header_PriorityPODate + level];
                            newRow[header_Customer] = dt_Source.Rows[row - 1][header_PriorityCustShortName + level];
                            newRow[header_CustomerCode] = dt_Source.Rows[row - 1][header_PriorityCustCode + level];

                            dt.Rows.Add(newRow);

                            DataRow newItemRow = dty_POItem.NewRow();

                            newItemRow[header_POTblCode] = dt_Source.Rows[row - 1][header_PriorityTblCode + level];
                            newItemRow[header_ToDeliveryPCSQty] = toDeliveryQtyInPcs;

                            dty_POItem.Rows.Add(newItemRow);

                        }
                    }
                }
            }

            return Tuple.Create(dt, dty_POItem);
        }

        private void btnOpenDO_Click(object sender, EventArgs e)
        {
            if(dataChanged)
            {
                MessageBox.Show("Please save the data before process to D/O open.");
            }
            else
            {
                DataGridView dgv = dgvList;

                string buttonStatus = btnOpenDO.Text;

                if (buttonStatus == text_AddDO)
                {
                    btnOpenDO.Text = text_SelectCol;
                    btnOpenDO.Enabled = false;

                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    foreach (DataGridViewColumn c in dgv.Columns)
                    {
                        c.SortMode = DataGridViewColumnSortMode.NotSortable;
                        c.Selected = false;
                    }

                    //dgv.columnsor
                    dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                }
                else if (buttonStatus == text_OpenDO)
                {

                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.EnableHeadersVisualStyles = true;

                    foreach (DataGridViewColumn c in dgv.Columns)
                    {
                        c.SortMode = DataGridViewColumnSortMode.Automatic;
                        c.Selected = false;
                    }

                    //open do
                    OpenDO(dgv);

                    DeselectedAllColumn(dgv);
                    btnOpenDO.Text = text_AddDO;

                    LoadPOList();
                }

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
          
        }

        private void cbFillALL_CheckedChanged(object sender, EventArgs e)
        {
            if(btnFillALL.Text == text_SelectCol && cbFillALL.Checked)
            {
                btnFillALL.Text = text_FillIn;
                btnFillALL.Enabled = true;
            }

            if (btnFillALL.Text == text_FillIn && !cbFillALL.Checked)
            {
                btnFillALL.Text = text_SelectCol;
                btnFillALL.Enabled = false;
            }

            
        }

        private void lblFillSettingReset_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvList;

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.EnableHeadersVisualStyles = true;

            foreach (DataGridViewColumn c in dgv.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.Automatic;
                c.Selected = false;
            }

            cbFillALL.Checked = false;

            btnFillALL.Enabled = true;

            DeselectedAllColumn(dgv);
            btnFillALL.Text = text_FillBySystem;
        }

        private void dgvList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                DataGridView dgv = dgvList;
                int col = e.ColumnIndex;

                dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                dgv.Columns[col].Selected = true;

                CalculateTotalBag(dgv);
            }
        }

        private void dgvList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("cell clicked!");
        }

        private void dgvList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show("cell clicked!");
        }

        private bool CheckIfWithinSelectedCell(int rowIndex, int colIndex)
        {
            int minSelectedCol = -1, maxSelectedCol = -1, minSelectedRow = -1, maxSelectedRow = -1;
            DataGridView dgv = dgvList;

            if(dgv.CurrentCell != null)
            {
                

                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        if (dgv.Rows[row].Cells[col].Selected)
                        {
                            if (minSelectedCol == -1 || minSelectedCol > col)
                            {
                                minSelectedCol = col;
                            }

                            if (maxSelectedCol == -1 || maxSelectedCol < col)
                            {
                                maxSelectedCol = col;
                            }

                            if (minSelectedRow == -1 || minSelectedRow > row)
                            {
                                minSelectedRow = row;
                            }

                            if (maxSelectedRow == -1 || maxSelectedRow < row)
                            {
                                maxSelectedRow = row;
                            }
                        }

                        
                    }

                 
                }


                if(rowIndex <= maxSelectedRow && rowIndex >= minSelectedRow && colIndex <= maxSelectedCol && colIndex >= minSelectedCol)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfValidCellSelected()
        {
            DataGridView dgv = dgvList;

            if (dgv.CurrentCell != null)
            {

                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        if (dgv.Rows[row].Cells[col].Selected)
                        {
                            string colName = dgv.Columns[col].Name;

                            if(colName.Contains(header_Priority))
                            {
                                if(int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out int i))
                                {
                                    return true;
                                }
                            }
                        }


                    }


                }

            }


            MessageBox.Show("Please select a valid cell!");
            return false;
        }

        private void dgvList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvList;

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();


                    if(!CheckIfWithinSelectedCell(e.RowIndex, e.ColumnIndex))
                    {
                        dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        //Can leave these here -doesn't hurt
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                  
                    dgv.Focus();

                    my_menu.Items.Add(text_Reset).Name = text_Reset;
                    my_menu.Items.Add(text_ResetAll).Name = text_ResetAll;
                    my_menu.Items.Add(text_ResetAllByCustomer).Name = text_ResetAllByCustomer;
                    my_menu.Items.Add(text_FillIn).Name = text_FillIn;
                    my_menu.Items.Add(text_FillAll).Name = text_FillAll;
                    my_menu.Items.Add(text_FillAllByCustomer).Name = text_FillAllByCustomer;
                    //my_menu.Items.Add(text_NewTrip).Name = text_NewTrip;
                    

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(DOList_ItemClicked);

                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void DOList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvList;

                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string ClickedItem = e.ClickedItem.Name.ToString();

                if (rowIndex >= 0 && (ClickedItem.Equals(text_ResetAll) || ClickedItem.Equals(text_ResetAllByCustomer)  || ClickedItem.Equals(text_FillAll) || ClickedItem.Equals(text_FillAllByCustomer) || CheckIfValidCellSelected()))
                {
                   
                    if (ClickedItem.Equals(text_Reset))
                    {
                        Reset(dgv);
                    }
                    else if (ClickedItem.Equals(text_ResetAll))
                    {
                        ResetAll(dgv);
                    }
                    else if (ClickedItem.Equals(text_ResetAllByCustomer))
                    {
                        ResetAllByCustomer(dgv, colIndex);
                    }
                    else if (ClickedItem.Equals(text_FillIn))
                    {
                        NewAutoFill(dgv);
                    }
                    else if (ClickedItem.Equals(text_FillAll))
                    {
                        NewAutoFillAll(dgv);
                    }
                    else if (ClickedItem.Equals(text_FillAllByCustomer))
                    {
                        NewAutoFillAllByCustomer(dgv, colIndex);                      
                    }
                    else if (ClickedItem.Equals(text_NewTrip))
                    {
                        NewTripTest(dgv);
                    }

                    CalculateTotalBag(dgv);
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvList_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
           // MessageBox.Show("Cell selected");
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Cell selected");
        }

        private void dgvList_MultiSelectChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Cell selected");
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //MessageBox.Show("Cell selected");
        }

        private void dgvList_MouseClick(object sender, MouseEventArgs e)
        {
            CalculateTotalBag(dgvList);
        }

        private DataTable GetToAssemblyList()
        {
            DataTable dt_Assembly = NewPlanningTable();

           

            DataTable dt = (DataTable)dgvList.DataSource;

            int index = 1;
            foreach(DataRow row in dt.Rows)
            {
                int BalAfterDelivery = int.TryParse(row[header_BalAfterDeliveryInPcs].ToString(), out BalAfterDelivery) ? BalAfterDelivery : 0;

                if(BalAfterDelivery < 0)
                {
                    DataRow new_Row = dt_Assembly.NewRow();
                    new_Row[header_Index] = index++;
                    new_Row[header_Size] = row[header_Size];
                    new_Row[header_Unit] = row[header_Unit];
                    new_Row[header_SizeString] = row[header_SizeString];
                    new_Row[header_Type] = row[header_Type];
                    new_Row[header_Code] = row[header_Code];
                    new_Row[header_QtyPerBag] = row[header_QtyPerBag];
                    new_Row[header_StockPcs] = row[header_StockPcs];
                    new_Row[header_StockBag] = row[header_StockBag];
                    new_Row[header_StockString] = row[header_StockString];
                    new_Row[header_BalAfterDelivery] = row[header_BalAfterDelivery];
                    new_Row[header_BalAfterDeliveryInPcs] = row[header_BalAfterDeliveryInPcs];

                    dt_Assembly.Rows.Add(new_Row);
                }
            }
            

            return dt_Assembly;
        }

        private void lblAssemblyNeeded_Click(object sender, EventArgs e)
        {

            frmSBBAssemblyPlanning frm = new frmSBBAssemblyPlanning(GetToAssemblyList())
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();
        }
    }
}
