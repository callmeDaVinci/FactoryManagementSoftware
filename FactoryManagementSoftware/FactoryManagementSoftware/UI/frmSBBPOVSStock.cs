using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Collections.Generic;
using System.Diagnostics;

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
        facStockDAL dalStock = new facStockDAL();

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
        readonly string header_QtyPerPkt = "QTY PER PKT";
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
        readonly string header_Stock = "STOCK";

        readonly string text_FillBySystem = "FILL BY SYSTEM";
        readonly string text_SelectCol = "SELECT COLUMN";
        readonly string text_FillIn = "Fill In";
        readonly string text_FillAll = "Fill All";
        readonly string text_FillAllByCustomer = "Fill All By Customer";
        readonly string text_JumpToFirstItem = "Jump To First Item";
        readonly string text_JumpToNextItem = "Jump To Next Item";
        readonly string text_JumpToLastItem = "Jump To Last Item";
        readonly string text_Reset = "Reset";
        readonly string text_ResetAll = "Reset All";
        readonly string text_ResetAllByCustomer = "Reset All By Customer";
        readonly string text_NewTrip = "New Trip";

        readonly string text_OpenDO = "OPEN D/O";
        readonly string text_AddDO = "Add D/O";

        readonly string text_Selected = " SELECTED";
        
        readonly string text_Unit = "UNIT";
        readonly string text_ProductSortBy = "PRODUCT SORT BY";
        readonly string text_POSortBy = "P/O SORT BY";

        readonly string text_Bag = "BAGs";
        readonly string text_Pkt = "PKTs";
        readonly string text_Pcs = "PCs";
        readonly string text_Size = "SIZE";
        readonly string text_Type = "TYPE";
        readonly string text_Customer = "CUSTOMER";
        readonly string text_ReceivedDate = "RECEIVED DATE";
       // readonly string text_PriorityLvl = "PRIORITY LEVEL";

        readonly string text_AssemblyNeeded = "ASSEMBLY NEEDED !!!";
        
        readonly string text_DeliveryStatus_ToOpen = "TO OPEN";
        readonly string text_DeliveryStatus_Opened = "OPENED";
        readonly string text_DeliveryStatus_OpenedWithDiff = "OPENED WITH DIFF";
        readonly string text_DeliveryStatus_ToDeliveryMoreThanOrder = "TO DELIVERY MORE THAN ORDER";

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

        private DataTable DB_PO_ACTIVE;
        private DataTable DB_DO_ALL;
        private DataTable dt_Product;
        private DataTable DB_ITEM_ALL;
        private DataTable DB_DO_ACTIVE;
        private DataTable dt_Mat;
        private DataTable dt_ToUpdate;

        DataTable DB_STOCK_ALL;
        static public DataTable DB_JOIN_ALL;

        private bool Loaded = false;
        private bool dataChanged = false;
        private bool stopAfterBalUpdate = false;
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
            
            dt.Columns.Add(header_Stock, typeof(int));
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
            dt.Columns.Add(header_QtyPerPkt, typeof(int));
            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_BalAfterDelivery, typeof(string));
            dt.Columns.Add(header_BalAfterDeliveryInPcs, typeof(int));
            return dt;
        }


        private const int WM_SETREDRAW = 0x000B;

        public static void Suspend(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        public static void Resume(Control control)
        {
            // Create a C "true" boolean as an IntPtr
            IntPtr wparam = new IntPtr(1);
            Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgResumeUpdate);

            control.Invalidate();
        }

        private void AdjustDGVRowAlignment(DataGridView dgv)
        {
            string preItem = "";

   
            dgv.SuspendLayout();

            if (dgv.RowCount > 1)
            {
                dgv.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                dgv.Rows[0].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                dgv.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                dgv.Rows[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                dgv.Rows[1].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                dgv.Rows[1].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                dgv.Rows[1].Frozen = true;
            }

            Font style_Italic_7 = new Font("Segoe UI", 7F, FontStyle.Italic);
            Font style_Italic_6 = new Font("Segoe UI", 6F, FontStyle.Italic);
            Font style_Regular_8 = new Font("Segoe UI", 8F, FontStyle.Regular);
            Font style_Bold_10 = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font style_Regular_10 = new Font("Segoe UI", 10F, FontStyle.Regular);

            #region datagridview looping

            for (int i = 4; i < dgv.RowCount; i += 3)
            {
                if ((i + 2) % 3 == 0)
                {
                    dgv.Rows[i - 2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    dgv.Rows[i - 2].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    dgv.Rows[i - 2].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);

                    dgv.Rows[i - 2].Cells[header_BalAfterDelivery].Style.Font = style_Italic_6;

                    //dgv.Rows[i - 2].InheritedStyle.Font = style_Italic;
                    dgv.Rows[i - 2].DefaultCellStyle.Font = style_Italic_7;
                    dgv.Rows[i - 2].Cells[header_Type].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                    dgv.Rows[i - 2].Cells[header_StockString].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                    DataGridViewRow row = dgv.Rows[i];
                    row.MinimumHeight = 2;
                    dgv.Rows[i].Height = 2;//adjust
                    //dgv.Rows[i].Visible = false;
                    string currentItem;

                    if (cmbProductSortBy.Text == text_Size)//144ms
                    {
                        currentItem = dgv.Rows[i - 1].Cells[header_SizeString].Value.ToString();
                    }
                    else
                    {
                        currentItem = dgv.Rows[i - 1].Cells[header_Type].Value.ToString();
                    }

                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv.Rows[i].InheritedStyle.BackColor = Color.LightGray;

                    if (preItem == "")
                    {
                        preItem = currentItem;

                    }
                    else if (preItem != currentItem)
                    {
                        preItem = currentItem;
                        dgv.Rows[i - 3].DefaultCellStyle.BackColor = Color.Black;
                        //dgv.Rows[i - 3].InheritedStyle.BackColor = Color.Black;
                        ////dgv.Rows[i - 3].Height = 40;
                    }


                    //dgv.Rows[i - 1].DefaultCellStyle.ForeColor = Color.Black;
                    //dgv.Rows[i - 1].DefaultCellStyle.Font = style_Regular_8;

                    dgv.Rows[i - 1].InheritedStyle.ForeColor = Color.Black;
                    dgv.Rows[i - 1].InheritedStyle.Font = style_Regular_8;

                    dgv.Rows[i - 1].Cells[header_BalAfterDelivery].Style.Font = style_Regular_10;//142ms
                    
                     

                    dgv.Rows[i - 1].Cells[header_Type].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Rows[i - 1].Cells[header_StockString].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Rows[i - 1].Cells[header_BalAfterDelivery].InheritedStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    //dgv.Rows[i - 1].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Red;

                }//576ms//558ms//567ms//419ms//422ms//145ms
              

            }
            #endregion
            dgv.ResumeLayout();

        }

        private void DgvUIEdit(DataGridView dgv, int priorityColNum)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           
            dgv.Columns[header_Code].Visible = false;
            dgv.Columns[header_Size].Visible = false;
            dgv.Columns[header_Unit].Visible = false;
            dgv.Columns[header_StockPcs].Visible = false;
            dgv.Columns[header_StockBag].Visible = false;
            dgv.Columns[header_QtyPerBag].Visible = false;
            dgv.Columns[header_QtyPerPkt].Visible = false;
            dgv.Columns[header_BalAfterDeliveryInPcs].Visible = false;
            dgv.Columns[header_BalAfterDelivery].Frozen = true;

            dgv.Columns[header_Code].DefaultCellStyle.ForeColor = Color.Gray;
            dgv.Columns[header_Code].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            //dgv.Columns[header_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dgv.Columns[header_StockString].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            dgv.Rows[0].Height = 70;

            dgv.Columns[header_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[header_Index].Width = 35;

            dgv.Columns[header_StockString].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[header_StockString].Width = 150;

            dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[header_Type].Width = 135;

            dgv.Columns[header_BalAfterDelivery].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[header_BalAfterDelivery].Width = 170;
            dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.BackColor = Color.WhiteSmoke;
          
            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_StockString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

                dgv.Rows[0].Cells[colName].Style.WrapMode = DataGridViewTriState.True;
                dgv.Rows[1].Cells[colName].Style.WrapMode = DataGridViewTriState.True;


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
            cmb.SelectedIndex = 1;
        }

        private void LoadPOSortByCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(text_POSortBy);
            dt.Rows.Add(text_Customer);

            dt.Rows.Add(text_ReceivedDate);
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

        private void Old_LoadPOList()
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


               // dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC" ;
                dt_Product.DefaultView.Sort = dalSPP.SizeWeight + " ASC," + dalSPP.SizeWeight + "1 ASC";
                //dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";

                dt_Product = dt_Product.DefaultView.ToTable();

                if (cmbProductSortBy.Text == text_Type)
                {
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }
                //^^19ms
                DB_ITEM_ALL = dalItem.Select();//40ms
                DB_PO_ACTIVE = dalSPP.ActivePOSelect();//48ms
                DB_DO_ALL = dalSPP.ActiveDOWithInfoSelect();//487ms

                //dt_POList = dalSPP.POSelect();
                //dt_DOList = dalSPP.DOWithInfoSelect();

                DB_STOCK_ALL = dalStock.Select();//41ms
                DB_JOIN_ALL = dalJoin.SelectAll();//8ms

                if (cmbPOSortBy.Text == text_ReceivedDate)
                {
                    DB_PO_ACTIVE.DefaultView.Sort = dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.CustTblCode + " ASC," + dalSPP.PriorityLevel + " ASC";
                }
                else if (cmbPOSortBy.Text == text_Customer)
                {
                    DB_PO_ACTIVE.DefaultView.Sort = dalSPP.ShortName + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.PriorityLevel + " ASC";
                }
                else
                {
                    DB_PO_ACTIVE.DefaultView.Sort = dalSPP.PriorityLevel + " ASC," + dalSPP.ShortName + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC";
                }

                //^^2831ms
                DB_PO_ACTIVE = DB_PO_ACTIVE.DefaultView.ToTable();

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
                    int bagStock = pcsStock / (qtyPerBag > 0 ? qtyPerBag : 1);
                    int balStock = pcsStock % (qtyPerBag > 0 ? qtyPerBag : 1);

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
                    string stdPackingString = qtyPerPacket + "/PKT, " + qtyPerBag + "/BAG";


                    if (typeName == text.Type_PolyORing)
                    {
                        stdPackingString = qtyPerPacket + "/PKT";
                    }

                    planning_row[header_Type] = itemCode;
                    planning_row[header_StockString] = stockString;
                    planning_row[header_BalAfterDelivery] = stdPackingString;
                    planning_row[header_QtyPerPkt] = qtyPerPacket;
                    dt_Planning.Rows.Add(planning_row);

                    //stockString = bagStock + " BAGS (" + pcsStock + ")";


                    stockString = bagStock + " BAGS";

                    if (typeName == text.Type_PolyORing)
                    {
                        stockString = bagStock + " PKTS";
                    }

                    planning_row = dt_Planning.NewRow();

                    planning_row[header_Index] = index;

                    planning_row[header_Size] = size;
                    planning_row[header_Unit] = sizeUnit;
                    planning_row[header_SizeString] = sizeString;

                    planning_row[header_Type] = typeName;
                    planning_row[header_Code] = itemCode;
                    planning_row[header_StockPcs] = pcsStock;
                    planning_row[header_StockBag] = bagStock;
                    planning_row[header_QtyPerBag] = qtyPerBag;
                    planning_row[header_QtyPerPkt] = qtyPerPacket;
                    planning_row[header_BalAfterDelivery] = stockString;
                    planning_row[header_BalAfterDeliveryInPcs] = pcsStock;

                    if (balStock > 0)
                    {
                        string nl = Environment.NewLine;
                        stockString += nl + " + " + balStock + " PCS";
                    }

                   

                    planning_row[header_StockString] = stockString;

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

                string EditUnit = cmbEditUnit.Text;

                //start: code to improve speed

                DataTable dt_DO_Short = DB_DO_ALL.Clone();

                foreach (DataRow row in DB_PO_ACTIVE.Rows)
                {
                    string poTblCode = row[dalSPP.TableCode].ToString();

                    foreach (DataRow rowDO in DB_DO_ALL.Rows)
                    {
                        bool isDORemoved = bool.TryParse(rowDO[dalSPP.IsRemoved].ToString(), out isDORemoved) ? isDORemoved : false;
                        bool isDODelivered = bool.TryParse(rowDO[dalSPP.IsDelivered].ToString(), out isDODelivered) ? isDODelivered : false;
                        string DO_POTblCode = rowDO[dalSPP.POTableCode].ToString();

                        if (!isDORemoved && !isDODelivered && DO_POTblCode == poTblCode)
                        {
                            //get to delivery qty
                            dt_DO_Short.Rows.Add(rowDO.ItemArray);
                        }
                    }

                }

                #region old Code to improve speed
                foreach (DataRow row in DB_PO_ACTIVE.Rows)
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                    bool isFreeze = bool.TryParse(row[dalSPP.Freeze].ToString(), out isFreeze) ? isFreeze : false;
                    bool custOwnDO = bool.TryParse(row[dalSPP.CustOwnDO].ToString(), out custOwnDO) ? custOwnDO : false;


                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                    int toDeliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out toDeliveryQty) ? toDeliveryQty : 0;
                    string poTblCode = row[dalSPP.TableCode].ToString();

                    int poCustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out poCustTblCode) ? poCustTblCode : 0;
                    bool usingBillingAddress = bool.TryParse(row[dalSPP.ShippingSameAsBilling].ToString(), out usingBillingAddress) ? usingBillingAddress : false;
                    bool gotBalanceOrder = false;

                    if (!isRemoved && !isFreeze && deliveredQty < orderQty)
                    {
                        int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;
                        string poItemCode = row[dalSPP.ItemCode].ToString();
                        

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

                                    if (EditUnit == text_Pcs)
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

                                    foreach (DataRow rowDO in dt_DO_Short.Rows)
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
                                    string POData = "";


                                    if ((orderQty - deliveredQty) % divideBy > 0)
                                    {
                                        POData += "★ ";//★⍟☆⋆✪✫❂🌟#%★
                                        gotBalanceOrder = true;
                                    }

                                    POData += " " + DO_ToDeliveryQty / divideBy + "/" + planningDelivered / divideBy + "/" + planningOrder / divideBy + " ";

                                    if ((orderQty - deliveredQty) % divideBy > 0)
                                    {
                                        POData += "★";//★⍟☆⋆✪✫❂🌟#%★

                                    }
                                    dt_Planning.Rows[i - 1][colName] = POData;

                                    string nl = Environment.NewLine;


                                    string POInfo = "";


                                    string DateString = PODate.ToShortDateString();

                                    if (custOwnDO)
                                    {
                                        if(gotBalanceOrder)
                                        {
                                            POInfo = " " + ShortName + nl + nl + "★✪ (" + PONo + ") " + nl + " " + DateString + " ";

                                        }
                                        else
                                        {
                                            POInfo = " " + ShortName + nl + nl + " ✪ (" + PONo + ") " + nl + " " + DateString + " ";

                                        }
                                    }
                                    else if (gotBalanceOrder)
                                    {
                                        POInfo = " " + ShortName + nl + nl + "★ (" + PONo + ") " + nl + " " + DateString + " ";

                                    }
                                    else
                                    {
                                        POInfo = " " + ShortName + nl + nl + " (" + PONo + ") " + nl + " " + DateString + " ";
                                    }

                                    dt_Planning.Rows[0][colName] = POInfo;
                                    dt_Planning.Rows[0][colCustIDName] = poCustTblCode;
                                   
                                    ShortName = row[dalSPP.ShortName].ToString();


                                    // string POShippingInfo = " " + PODate.ToShortDateString() + " ";
                                    string POShippingInfo = "";

                                    string ShippingShortName = row[dalSPP.ShippingShortName].ToString();
                                    string ShippingState = row[dalSPP.AddressState].ToString().ToUpper();
                                    string ShippingTransporter = row[dalSPP.ShippingTransporter].ToString();

                                    
                                    POShippingInfo += nl ;

                                    if (ShippingShortName != ShortName && !usingBillingAddress)
                                        POShippingInfo +=  " " + ShippingShortName;
                                    else
                                        POShippingInfo +=  nl;



                                    if (ShippingState != "SELANGOR" && ShippingState != "KL" && ShippingState != "KUALA LUMPUR" )
                                    {
                                        POShippingInfo += " (" + ShippingState + ")";
                                    }

                                    POShippingInfo += " " + nl + ShippingTransporter + " ";
                                    dt_Planning.Rows[1][colName] = POShippingInfo;
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
                //ennd: code to improve speed

                #endregion
                
                dgvList.SuspendLayout();

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvList.ColumnHeadersVisible = false;

                dgvList.DataSource = dt_Planning;

               

                DgvUIEdit(dgvList, colNum);

                AdjustDGVRowAlignment(dgvList);

                AdjustToDeliveryBackColor();

                UpdateBalAfterDelivery();

                dgvList.ResumeLayout();

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

                dgvList.ColumnHeadersVisible = true;


              
                dgvList.ClearSelection();

                //^^872ms
                Cursor = Cursors.Arrow;

                frmLoading.CloseForm();//1122ms//986ms
            }
          
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


                // dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC" ;
                dt_Product.DefaultView.Sort = dalSPP.SizeWeight + " ASC," + dalSPP.SizeWeight + "1 ASC";
                //dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";

                dt_Product = dt_Product.DefaultView.ToTable();

                if (cmbProductSortBy.Text == text_Type)
                {
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }

                DB_ITEM_ALL = dalItem.Select();
                DB_PO_ACTIVE = dalSPP.ActivePOSelect();
                DB_DO_ALL = dalSPP.DOSelect();//124ms
                //dt_DOList = dalSPP.ActiveDOWithInfoSelect();//487ms


                //dt_POList = dalSPP.POSelect();
                //dt_DOList = dalSPP.DOWithInfoSelect();

                DB_STOCK_ALL = dalStock.Select();
                DB_JOIN_ALL = dalJoin.SelectAll();

                if (cmbPOSortBy.Text == text_ReceivedDate)
                {
                    DB_PO_ACTIVE.DefaultView.Sort = dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.CustTblCode + " ASC," + dalSPP.PriorityLevel + " ASC";
                }
                else if (cmbPOSortBy.Text == text_Customer)
                {
                    DB_PO_ACTIVE.DefaultView.Sort = dalSPP.ShortName + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.PriorityLevel + " ASC";
                }
                else
                {
                    DB_PO_ACTIVE.DefaultView.Sort = dalSPP.PriorityLevel + " ASC," + dalSPP.ShortName + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC";
                }

                //^^2831ms
                DB_PO_ACTIVE = DB_PO_ACTIVE.DefaultView.ToTable();

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
                    int bagStock = pcsStock / (qtyPerBag > 0 ? qtyPerBag : 1);
                    int balStock = pcsStock % (qtyPerBag > 0 ? qtyPerBag : 1);

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
                    string stdPackingString = qtyPerPacket + "/PKT, " + qtyPerBag + "/BAG";


                    if (typeName == text.Type_PolyORing)
                    {
                        stdPackingString = qtyPerPacket + "/PKT";
                    }

                    planning_row[header_Type] = itemCode;
                    planning_row[header_StockString] = stockString;
                    planning_row[header_BalAfterDelivery] = stdPackingString;
                    planning_row[header_QtyPerPkt] = qtyPerPacket;
                    dt_Planning.Rows.Add(planning_row);

                    //stockString = bagStock + " BAGS (" + pcsStock + ")";


                    stockString = bagStock + " BAGS";

                    if (typeName == text.Type_PolyORing)
                    {
                        stockString = bagStock + " PKTS";
                    }

                    planning_row = dt_Planning.NewRow();

                    planning_row[header_Index] = index;

                    planning_row[header_Size] = size;
                    planning_row[header_Unit] = sizeUnit;
                    planning_row[header_SizeString] = sizeString;

                    planning_row[header_Type] = typeName;
                    planning_row[header_Code] = itemCode;
                    planning_row[header_StockPcs] = pcsStock;
                    planning_row[header_StockBag] = bagStock;
                    planning_row[header_QtyPerBag] = qtyPerBag;
                    planning_row[header_QtyPerPkt] = qtyPerPacket;
                    planning_row[header_BalAfterDelivery] = stockString;
                    planning_row[header_BalAfterDeliveryInPcs] = pcsStock;

                    if (balStock > 0)
                    {
                        string nl = Environment.NewLine;
                        stockString += nl + " + " + balStock + " PCS";
                    }



                    planning_row[header_StockString] = stockString;

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

                string EditUnit = cmbEditUnit.Text;

                //start: code to improve speed

                DB_DO_ACTIVE = DB_DO_ALL.Clone();

                // Create a dictionary for the DO list.
                Dictionary<string, List<DataRow>> doDict = new Dictionary<string, List<DataRow>>();

                foreach (DataRow rowDO in DB_DO_ALL.Rows)
                {
                    bool isDORemoved = bool.TryParse(rowDO[dalSPP.IsRemoved].ToString(), out isDORemoved) ? isDORemoved : false;
                    bool isDODelivered = bool.TryParse(rowDO[dalSPP.IsDelivered].ToString(), out isDODelivered) ? isDODelivered : false;
                    string DO_POTblCode = rowDO[dalSPP.POTableCode].ToString();

                    if (!isDORemoved && !isDODelivered)
                    {
                        if (!doDict.ContainsKey(DO_POTblCode))
                        {
                            doDict[DO_POTblCode] = new List<DataRow>();
                        }
                        doDict[DO_POTblCode].Add(rowDO);
                    }
                }

                // Now, iterate through the PO list and check against the dictionary.
                foreach (DataRow row in DB_PO_ACTIVE.Rows)
                {
                    string poTblCode = row[dalSPP.TableCode].ToString();
                    if (doDict.ContainsKey(poTblCode))
                    {
                        foreach (DataRow matchingRowDO in doDict[poTblCode])
                        {
                            DB_DO_ACTIVE.Rows.Add(matchingRowDO.ItemArray);
                        }
                    }
                }


                #region old Code to improve speed
                foreach (DataRow row in DB_PO_ACTIVE.Rows)
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                    bool isFreeze = bool.TryParse(row[dalSPP.Freeze].ToString(), out isFreeze) ? isFreeze : false;
                    bool custOwnDO = bool.TryParse(row[dalSPP.CustOwnDO].ToString(), out custOwnDO) ? custOwnDO : false;


                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                    int toDeliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out toDeliveryQty) ? toDeliveryQty : 0;
                    string poTblCode = row[dalSPP.TableCode].ToString();
                    int priorityLevel = int.TryParse(row[dalSPP.PriorityLevel].ToString(), out priorityLevel) ? priorityLevel : -1;

                    DateTime TargetDeliveryDate = DateTime.TryParse(row[dalSPP.TargetDeliveryDate].ToString(),out TargetDeliveryDate)? TargetDeliveryDate: DateTime.MaxValue;

                    int poCustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out poCustTblCode) ? poCustTblCode : 0;
                    bool usingBillingAddress = bool.TryParse(row[dalSPP.ShippingSameAsBilling].ToString(), out usingBillingAddress) ? usingBillingAddress : false;
                    bool gotBalanceOrder = false;

                    if (!isRemoved && !isFreeze && deliveredQty < orderQty)
                    {
                        int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;
                        string poItemCode = row[dalSPP.ItemCode].ToString();


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

                                    if (EditUnit == text_Pcs)
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

                                    foreach (DataRow rowDO in DB_DO_ACTIVE.Rows)
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
                                    string POData = "";


                                    if ((orderQty - deliveredQty) % divideBy > 0)
                                    {
                                        POData += "★ ";//★⍟☆⋆✪✫❂🌟#%★
                                        gotBalanceOrder = true;
                                    }

                                    POData += " " + DO_ToDeliveryQty / divideBy + "/" + planningDelivered / divideBy + "/" + planningOrder / divideBy + " ";

                                    if ((orderQty - deliveredQty) % divideBy > 0)
                                    {
                                        POData += "★";//★⍟☆⋆✪✫❂🌟#%★

                                    }
                                    dt_Planning.Rows[i - 1][colName] = POData;

                                    string nl = Environment.NewLine;


                                    string POInfo = "";


                                    string DateString = PODate.ToShortDateString();

                                    if (custOwnDO)
                                    {
                                        if (gotBalanceOrder)
                                        {
                                            POInfo = " " + ShortName + nl + nl + "★✪ (" + PONo + ") " + nl + " " + DateString + " ";

                                        }
                                        else
                                        {
                                            POInfo = " " + ShortName + nl + nl + " ✪ (" + PONo + ") " + nl + " " + DateString + " ";

                                        }
                                    }
                                    else if (gotBalanceOrder)
                                    {
                                        POInfo = " " + ShortName + nl + nl + "★ (" + PONo + ") " + nl + " " + DateString + " ";

                                    }
                                    else
                                    {
                                        POInfo = " " + ShortName + nl + nl + " (" + PONo + ") " + nl + " " + DateString + " ";
                                    }


                                    if(priorityLevel > 0)
                                    {
                                        POInfo = "!!! " + POInfo;

                                        POInfo = POInfo.Replace(DateString + " ", "PO:"+DateString + "   Del.:" + TargetDeliveryDate.ToShortDateString() + "");
                                    }

                                    dt_Planning.Rows[0][colName] = POInfo;
                                    dt_Planning.Rows[0][colCustIDName] = poCustTblCode;

                                    ShortName = row[dalSPP.ShortName].ToString();


                                    // string POShippingInfo = " " + PODate.ToShortDateString() + " ";
                                    string POShippingInfo = "";

                                    string ShippingShortName = row[dalSPP.ShippingShortName].ToString();
                                    string ShippingState = row[dalSPP.AddressState].ToString().ToUpper();
                                    string ShippingTransporter = row[dalSPP.ShippingTransporter].ToString();


                                    POShippingInfo += nl;

                                    if (ShippingShortName != ShortName && !usingBillingAddress)
                                        POShippingInfo += " " + ShippingShortName;
                                    else
                                        POShippingInfo += nl;



                                    if (ShippingState != "SELANGOR" && ShippingState != "KL" && ShippingState != "KUALA LUMPUR")
                                    {
                                        POShippingInfo += " (" + ShippingState + ")";
                                    }

                                    POShippingInfo += " " + nl + ShippingTransporter + " ";
                                    dt_Planning.Rows[1][colName] = POShippingInfo;
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
                //ennd: code to improve speed

                #endregion

                #region DGV Setting

                //^^ 375ms
                //vv 847ms
                dgvList.SuspendLayout();

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvList.ColumnHeadersVisible = false;

                dgvList.DataSource = dt_Planning;



                DgvUIEdit(dgvList, colNum);//222ms

                AdjustDGVRowAlignment(dgvList);//292ms

                AdjustToDeliveryBackColor();//141ms

                UpdateBalAfterDelivery();//194ms

                dgvList.ResumeLayout();

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

                dgvList.ColumnHeadersVisible = true;



                dgvList.ClearSelection();

                #endregion

  
                Cursor = Cursors.Arrow;

                frmLoading.CloseForm();
            }

        }
        private void AdjustToDeliveryBackColor()
        {
            DataGridView dgv = dgvList;
            Font bold_12 = new Font("Segoe UI", 12F, FontStyle.Bold);
            Color color_ToOpen = Color.FromArgb(253, 203, 110);//yellow
            Color color_Opened = Color.FromArgb(0, 184, 148);//green
            Color color_OpenedWithDiff = Color.FromArgb(52, 160, 225);//blue
            Color color_ToDeliveryMoreThanOrder = Color.FromArgb(255, 118, 117);//red

            for (int j = 5; j < dgv.ColumnCount; j++)
            {
                int colIndex = j;

                string cellValue = dgv.Rows[0].Cells[colIndex].Value.ToString();

                if (cellValue.Contains("!!!"))
                {
                    dgv.Rows[0].Cells[j].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[1].Cells[j].Style.BackColor = Color.FromArgb(251, 255, 147);

                }

              
            }

            for (int i = 2; i < dgv.RowCount; i++)
            {
                for (int j = 5; j < dgv.ColumnCount; j++)
                {
                    int rowIndex = i;
                    int colIndex = j;

                    
                    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
                    string colName = dgv.Columns[colIndex].Name;

                    if (colName.Contains(header_Priority) && int.TryParse(cellValue, out int x))
                    {
                        dgv.Rows[i].Cells[j].Style.Font = bold_12;
                        //dgv.Rows[i].Cells[j].InheritedStyle.Font = bold_12;
                       
                    }

                    // change to delivery qty fore color
                    if (cellValue == text_DeliveryStatus_ToOpen)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = color_ToOpen;
                    }
                    else if (cellValue == text_DeliveryStatus_Opened)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = color_Opened;
                    }
                    else if (cellValue == text_DeliveryStatus_OpenedWithDiff)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = color_OpenedWithDiff;
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
                                string POData = "";
                                bool gotBal = false;

                                if ((orderQty - deliveredQty) % pcsPerBag > 0)
                                {
                                    POData += "★ ";//★⍟☆⋆✪✫❂🌟#%★
                                    gotBal = true;
                                }

                                

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

                                //string POData = " " + DOToDeliveryQtyInPcs + "/" + deliveredQty + "/" + orderQty + " ";

                                POData += " " + DOToDeliveryQtyInPcs + "/" + deliveredQty + "/" + orderQty + " ";

                                if (gotBal)
                                {
                                    POData += "★";//★⍟☆⋆✪✫❂🌟#%★
                                }




                                //string POData = " " + ShortName + " (" + PONo + "): " + DOToDeliveryQtyInPcs + "/" + deliveredQty + "/" + orderQty + " ";

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
                UpdateBalAfterDelivery();
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
                string itemCode = row[header_ItemCode].ToString();

                string parentCode = itemCode;

                if (itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C")
                {
                    parentCode = GetChildCode(dtJoin, row[header_ItemCode].ToString());
                }

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
                            alert_row[header_Stock] = readyStock;
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
                string balString = row[header_BalAfterDeliveryInPcs].ToString();
                //string balString = row[header_BalAfterDelivery].ToString().Replace(" BAGS", "");


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
                        int toDelivery = int.TryParse(dt.Rows[rowIndex - 1][colName].ToString(), out toDelivery) ? toDelivery : -1;
                        
                        //if (colName.Contains(header_Priority) && toDelivery >= 0)
                        //{
                        //    TotalToDeliveryQty += toDelivery;
                        //}

                        if (colName.Contains(header_PriorityToDeliveryPlanningInPCS) && toDelivery >= 0)
                        {
                            TotalToDeliveryQty += toDelivery;
                        }

                    }

                    int divideBy = pcsPerBag;
                    

                    if (cmbEditUnit.Text == text_Pcs)
                    {
                        divideBy = 1;
                    }

                    int bal_Pcs = stockInPcs - TotalToDeliveryQty * 1;

                    int balAfterDelivery = bal_Pcs / pcsPerBag;

                    string balAFterDelivery = dt.Rows[rowIndex][header_BalAfterDelivery].ToString();

                    if(balAFterDelivery.Contains("PKTS"))
                    {
                        dt.Rows[rowIndex][header_BalAfterDelivery] = balAfterDelivery + " PKTS";

                    }
                    else
                    {
                        dt.Rows[rowIndex][header_BalAfterDelivery] = balAfterDelivery + " BAGS";

                    }

                    int balPcsAfterDelivery = bal_Pcs % pcsPerBag;

                    if(balPcsAfterDelivery != 0)
                    {
                        if (balAFterDelivery.Contains("PKTS"))
                        {
                            dt.Rows[rowIndex][header_BalAfterDelivery] = balAfterDelivery + " PKTS + " + balPcsAfterDelivery + " PCS";

                        }
                        else
                        {
                            dt.Rows[rowIndex][header_BalAfterDelivery] = balAfterDelivery + " BAGS + " + balPcsAfterDelivery + " PCS";

                        }

                        

                    }

                    dt.Rows[rowIndex][header_BalAfterDeliveryInPcs] = bal_Pcs;

                    int test = bal_Pcs / pcsPerBag;

                    if (stockInPcs < 0)
                    {
                        dgvList.Rows[rowIndex].Cells[header_Stock].Style.ForeColor = Color.Red;
                        dgvList.Rows[rowIndex - 1].Cells[header_Stock].Style.ForeColor = Color.Red;

                    }
                   
                    if (bal_Pcs < 0)
                    {
                        if (dgvList.Rows[rowIndex].Cells[header_BalAfterDelivery].InheritedStyle.ForeColor == Color.Black)
                        {
                            toAssemblyProductQty++;
                        }

                        dgvList.Rows[rowIndex].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Red;
                        TotalAssemblyNeededInBags += bal_Pcs / pcsPerBag;
                        TotalAssemblyNeededInPcs += bal_Pcs;
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

            int balInPcsBalance = balInPcs % pcsPerBag;

            if(balInPcsBalance != 0)
            {
                dt.Rows[row][header_BalAfterDelivery] = balInPcs / pcsPerBag + " BAGS + "+balInPcsBalance+" PCS";

            }


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
                
                if(int.TryParse(cellValue, out int x) && x != oldToDeliveryQty)
                {
                    

                    //get priority level
                    string colName = dgv.Columns[colIndex].Name;

                    string priorityLvl = colName.Replace(header_Priority, "");

                    //get DO to delivery qty
                    string DOToDeliveryQtyInPcs = dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();
                    string POTblCode = dgv.Rows[rowIndex - 1].Cells[header_PriorityTblCode + priorityLvl].Value.ToString();

                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                    int pcsPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out pcsPerBag) ? pcsPerBag : 1;

                    int newToDeliveryQty = int.TryParse(cellValue, out newToDeliveryQty) ? newToDeliveryQty : 0;
                    int orderQty = int.TryParse(dgv.Rows[rowIndex - 1].Cells[header_PriorityOrder + priorityLvl].Value.ToString(), out orderQty) ? orderQty : 0;
                    int deliveredQty = int.TryParse(dgv.Rows[rowIndex - 1].Cells[header_PriorityDelivered + priorityLvl].Value.ToString(), out deliveredQty) ? deliveredQty : 0;

                    int toDeliveryQty = newToDeliveryQty * pcsPerBag;

                    if(cmbEditUnit.Text == text_Pcs)
                    {
                        toDeliveryQty = newToDeliveryQty;
                    }

                    if (toDeliveryQty + deliveredQty <= orderQty)
                    {
                        dataChanged = true;
                        btnUpdate.Visible = true;
                        //update balance
                        UpdateBalAfterDelivery();

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
                    else
                    {
                        MessageBox.Show("Delivery + delivered quantity cannot more than ordered quantity.","        WARNING",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgv.Rows[rowIndex].Cells[colIndex].Value = oldToDeliveryQty;
                    }

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

        private void JumpToNextItem(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            int currentRowIndex = dgv.CurrentCell.RowIndex;
            int currentColIndex = dgv.CurrentCell.ColumnIndex;
            int jumpToRowIndex = -1;
            string headerName = dgv.Columns[currentColIndex].Name;
            string priorityLevel = headerName.Replace(header_Priority, "");


            for (int i = currentRowIndex + 1; i < dt.Rows.Count; i++)
            {
                int orderQty = int.TryParse(dt.Rows[i][header_PriorityOrder + priorityLevel].ToString(), out orderQty) ? orderQty : 0;

                if (orderQty > 0)
                {
                    jumpToRowIndex = i;
                    break;

                }
            }

            if (jumpToRowIndex != -1 && jumpToRowIndex >= currentRowIndex && jumpToRowIndex + 1< dt.Rows.Count)
            {
                dgv.CurrentCell = dgv[currentColIndex, jumpToRowIndex + 1];
            }
            else
            {
                MessageBox.Show("No next item found under this PO.");
            }

            dgv.ResumeLayout();
        }
        private void JumpToLastItem(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            int currentRowIndex = dgv.CurrentCell.RowIndex;
            int currentColIndex = dgv.CurrentCell.ColumnIndex;
            string headerName = dgv.Columns[currentColIndex].Name;
            string priorityLevel = headerName.Replace(header_Priority, "");

            int lastRowIndex = currentRowIndex;

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                int orderQty = int.TryParse(dt.Rows[i][header_PriorityOrder + priorityLevel].ToString(), out orderQty) ? orderQty : 0;

                if (orderQty > 0)
                {
                    lastRowIndex = i;
                    break;

                }
            }

            if (lastRowIndex > -1)
            {
                dgv.CurrentCell = dgv[currentColIndex, lastRowIndex + 1];
            }

            dgv.ResumeLayout();
        }

        private void JumpToFirstItem(DataGridView dgv)
        {
            dgv.SuspendLayout();
            DataTable dt = (DataTable)dgv.DataSource;

            int currentRowIndex = dgv.CurrentCell.RowIndex;
            int currentColIndex = dgv.CurrentCell.ColumnIndex;
            string headerName = dgv.Columns[currentColIndex].Name;
            string priorityLevel = headerName.Replace(header_Priority, "");

            int FirstRowIndex = -1;

            for (int i = 0 ; i < dt.Rows.Count; i++)
            {
                int orderQty = int.TryParse(dt.Rows[i][header_PriorityOrder + priorityLevel].ToString(), out orderQty) ? orderQty : 0;

                if (orderQty > 0)
                {
                    FirstRowIndex = i;
                    break;

                }
            }

            if (FirstRowIndex > -1)
            {
                dgv.CurrentCell = dgv[currentColIndex, FirstRowIndex + 1];
            }

            dgv.ResumeLayout();
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

                                int stillNeedInPcs = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = stillNeedInPcs;

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeedInPcs.ToString())
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

                                int stillNeedInPcs = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = stillNeedInPcs;

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeedInPcs.ToString())
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

                                    int stillNeedInPcs = stillNeed;

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                    dt.Rows[i][searchingColumns] = stillNeed;

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                    //get DO to delivery qty
                                    string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                                    dgv.Rows[i - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = stillNeedInPcs;


                                    //change delivery status color
                                    if (DOToDeliveryQtyInPcs != "0")
                                    {
                                        if (DOToDeliveryQtyInPcs == stillNeedInPcs.ToString())
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
                                    dgv.Rows[i - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = 0;

                                    stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                    //get DO to delivery qty
                                    string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                                    if (DOToDeliveryQtyInPcs != "0")
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);

                                        //if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                        //{
                                        //    deliveryStatus = text_DeliveryStatus_Opened;
                                        //    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                        //}
                                        //else
                                        //{
                                        //    deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        //    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                        //}
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
            int totalPkts = 0;
            int totalPcs = 0;
            int totalBalPcs = 0;

            int total_Oring_Pkts = 0;
            int total_Oring_Pcs = 0;

            int totalCellSelected = 0;
            int totalColSelected = 0;

            //dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;
                int pcsPerPacket = int.TryParse(dt.Rows[i][header_QtyPerPkt].ToString(), out pcsPerPacket) ? pcsPerPacket : -1;

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

                            string itemCode = dt.Rows[i][header_Code].ToString();

                            if (toDeliverQty > -1)
                            {
                                totalCellSelected++;
                            }

                            int toDeliverPcs = int.TryParse(dt.Rows[i - 1][header_PriorityToDeliveryPlanningInPCS + priorityLvl.ToString()].ToString(), out toDeliverPcs) ? toDeliverPcs : 0;

                            if (toDeliverQty > 0 || toDeliverPcs > 0)
                            {
                                if(itemCode.Contains("CFPOR"))
                                {
                                    total_Oring_Pkts += cmbEditUnit.Text != text_Bag ? toDeliverQty / pcsPerBag : toDeliverQty;
                                    total_Oring_Pcs += toDeliverPcs;
                                }
                                else
                                {
                                    totalBag += cmbEditUnit.Text != text_Bag ? toDeliverQty / pcsPerBag : toDeliverQty;
                                    totalPcs += toDeliverPcs;
                                }
                              
                          

                                if (pcsPerPacket > 0)
                                {
                                    if (itemCode.Contains("CFPOR"))
                                    {
                                        total_Oring_Pkts += toDeliverPcs % pcsPerBag / pcsPerPacket;
                                        total_Oring_Pcs += toDeliverPcs % pcsPerPacket;
                                    }
                                    else
                                    {
                                        totalPkts += toDeliverPcs % pcsPerBag / pcsPerPacket;
                                        totalBalPcs += toDeliverPcs % pcsPerPacket;
                                    }

                                    

                                }
                                else
                                {
                                    if (itemCode.Contains("CFPOR"))
                                    {
                                        total_Oring_Pcs += toDeliverPcs % pcsPerBag;

                                    }
                                    else
                                    {
                                        totalBalPcs += toDeliverPcs % pcsPerBag;

                                    }


                                }
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

            string totalSelected_Text = totalCellSelected + " Cells";

            if (dgv.SelectionMode == DataGridViewSelectionMode.FullColumnSelect)
            {
                totalSelected = totalColSelected;
                selectedType = "column";
            }

            if(totalSelected > 1)
            {
                selectedType += "s";
            }

            totalSelected_Text =  totalSelected + " " + selectedType ;
            string str_Bag = " Bags";
            string str_Pkt = " Packets";
            string str_Pcs = " Pcs";
            string str_BalPcs = " Bal. Pcs";

            string str_Oring_Pkt = " Oring Packets";
            string str_Oring_Pcs = " Oring Pcs";

            if (totalBag <= 1)
            {
                str_Bag = " Bag";
            }

            if (totalPkts <= 1)
            {
                str_Pkt = " Packet";
            }

            if (totalPcs <= 1)
            {
                str_Pcs = " Piece";
            }

            if (totalBalPcs <= 1)
            {
                str_BalPcs = " Piece";
            }

            if (total_Oring_Pkts <= 1)
            {
                str_Oring_Pkt = " Oring Packet";
            }

            if (total_Oring_Pcs <= 1)
            {
                str_Oring_Pcs = " Oring Piece";
            }

            if (totalPkts > 0)
            {
                if(totalBalPcs > 0)
                {
                    lblTotalBag.Text = totalBag + str_Bag +" + " + totalPkts + str_Pkt + " + " + totalBalPcs + str_BalPcs + " or " + totalPcs + str_Pcs ;

                }
                else
                {
                    lblTotalBag.Text = totalBag + str_Bag + " + " + totalPkts + str_Pkt + " or " + totalPcs + str_Pcs;

                }

            }
            else
            {
                if (totalBalPcs > 0)
                {
                    lblTotalBag.Text = totalBag + str_Bag + " + " + totalBalPcs + str_BalPcs + " or " + totalPcs + str_Pcs;

                }
                else
                {
                    lblTotalBag.Text = totalBag + str_Bag + " or " + totalPcs + str_Pcs;


                }
            }

            if (total_Oring_Pkts > 0)
            {
                if (total_Oring_Pcs > 0)
                {
                    lblTotalBag.Text += " + " + total_Oring_Pkts + str_Oring_Pkt + " or " + total_Oring_Pcs + str_Oring_Pcs;

                }
                else
                {
                    lblTotalBag.Text += " + " + total_Oring_Pkts + str_Oring_Pkt;

                }

            }
            else
            {
                if (total_Oring_Pcs > 0)
                {
                    lblTotalBag.Text += " + " + total_Oring_Pcs + str_Oring_Pcs;

                }
             
            }
            lblTotalBag.Text += " ("+totalSelected_Text+ text_Selected+")";

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
            stopAfterBalUpdate = true;
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

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = 0;


                                // dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110);

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);

                                    //if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    //{
                                    //    deliveryStatus = text_DeliveryStatus_Opened;
                                    //    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    //}
                                    //else
                                    //{
                                    //    deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                    //    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    //}
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
            stopAfterBalUpdate = false;
        }

        private void ResetAll(DataGridView dgv)
        {
            stopAfterBalUpdate = true;
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
                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = 0;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);

                                    //if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    //{
                                    //    deliveryStatus = text_DeliveryStatus_Opened;
                                    //    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    //}
                                    //else
                                    //{
                                    //    deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                    //    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    //}
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
            stopAfterBalUpdate = false;
        }

        private void btnFillALL_Click(object sender, EventArgs e)
        {
            stopAfterBalUpdate = true;
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
            stopAfterBalUpdate = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgv.ResumeLayout();
            Cursor = Cursors.Arrow;
        }

        private bool dataUpdate()
        {
            bool updated = false;

            DialogResult dialogResult = MessageBox.Show("Confirm to save/update ?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                bool dataUpdated = true;

                if (dt_ToUpdate != null)
                {
                    uSpp.Updated_By = MainDashboard.USER_ID;
                    uSpp.Updated_Date = DateTime.Now;

                    foreach (DataRow row in dt_ToUpdate.Rows)
                    {
                        uSpp.Table_Code = int.TryParse(row[header_POTblCode].ToString(), out int i) ? i : 0;
                        uSpp.To_delivery_qty = int.TryParse(row[header_ToDeliveryPCSQty].ToString(), out i) ? i : 0;

                        dataUpdated = dalSPP.POToDeliveryDataUpdate(uSpp);
                        if (!dataUpdated)
                        {
                            MessageBox.Show("Failed to update to delivery qty.\nPO Table Code: " + uSpp.Table_Code + "\nTo Delivery Qty: " + uSpp.To_delivery_qty);
                        }
                    }

                    if (dataUpdated)
                    {
                        MessageBox.Show("Data updated!");

                        updated = true;
                    }

                    dataChanged = false;
                    btnUpdate.Visible = false;
                }
            }

            return updated;

        }

           

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataUpdate();   
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
                //string poNumber = dgv.Columns[col].HeaderText.Replace("P/O_", "");

                //dgv.Rows[0].Cells[header_ColSelected + poNumber].Value = 1;
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
            bool OktoNextStep = true;

            if(btnUpdate.Visible)
            {
                OktoNextStep = dataUpdate();
            }

            if(OktoNextStep)
            {
                var POData = GetPOData(dgv);

                DataTable dt = POData.Item1;
                DataTable dt_POItem = POData.Item2;

                if (dt.Rows.Count > 0)
                {
                    frmSBBPOList frm = new frmSBBPOList(dt, dt_POItem, false)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    frm.ShowDialog();

                    if(frmSBBPOList.DOOpened)
                    {
                        LoadPOList();
                    }

                }
                else
                {
                    MessageBox.Show("Valid delivery data not found!\n\n>To delivery qty cannot be 0\n>Check if D/O have been opened");
                }
            }
           
        }


        private Tuple<DataTable, DataTable> GetPOData(DataGridView dgv)
        {
            DataTable dt = NewPOTable();
            DataTable dty_POItem = NewPOItemTable();

            DataTable dt_Source = (DataTable) dgv.DataSource;

            for(int col = 0; col < dt_Source.Columns.Count; col++)
            {
                bool colSelected = dgv.Columns[col].Selected;

                if (colSelected)
                {
                    string colName = dt_Source.Columns[col].ColumnName;

                    string level = colName.Replace(header_Priority, "");

                    DataRow newRow =null;

                    for (int row = 1; row < dt_Source.Rows.Count; row++)
                    {
                        int toDeliveryBag = int.TryParse(dt_Source.Rows[row][header_Priority + level].ToString(), out toDeliveryBag) ? toDeliveryBag : -1;
                        int toDeliveryQty = int.TryParse(dt_Source.Rows[row - 1][header_PriorityToDeliveryPlanningInPCS + level].ToString(), out toDeliveryQty) ? toDeliveryQty : -1;

                        int DOToDeliveryQty = int.TryParse(dt_Source.Rows[row - 1][header_PriorityToDeliveryQty + level].ToString(), out DOToDeliveryQty) ? DOToDeliveryQty : -1;
                        int orderQty = int.TryParse(dt_Source.Rows[row - 1][header_PriorityOrder + level].ToString(), out orderQty) ? orderQty : -1;

                        //&& toDeliveryQtyInPcs != DOToDeliveryQty
                        int toAddQty = toDeliveryQty - DOToDeliveryQty;

                        if (toAddQty > 0 )
                        {
                            newRow = dt.NewRow();

                            //string ponostring = dt_Source.Rows[row - 1][header_PriorityPONo + level].ToString();

                            newRow[header_PONoString] = dt_Source.Rows[row - 1][header_PriorityPONo + level];
                            newRow[header_POCode] = dt_Source.Rows[row - 1][header_PriorityPOCode + level];
                            newRow[header_PODate] = dt_Source.Rows[row - 1][header_PriorityPODate + level];
                            newRow[header_Customer] = dt_Source.Rows[row - 1][header_PriorityCustShortName + level];
                            newRow[header_CustomerCode] = dt_Source.Rows[row - 1][header_PriorityCustCode + level];

                            //dt.Rows.Add(newRow);

                            DataRow newItemRow = dty_POItem.NewRow();

                            newItemRow[header_POTblCode] = dt_Source.Rows[row - 1][header_PriorityTblCode + level];
                            newItemRow[header_ToDeliveryPCSQty] = toAddQty;

                            dty_POItem.Rows.Add(newItemRow);

                        }
                    }

                    if(newRow != null)
                    {
                        dt.Rows.Add(newRow);
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

                //dgv.CurrentCell = dgv.Rows[1].Cells[col];

                dgv.Rows[1].Cells[col].Selected = true;

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

        private int CheckTotalSelectedColumn(DataGridView dgv)
        {
            int selectedColumn = 0;

            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                if (dgv.Columns[col].Selected)
                {
                    selectedColumn++;
                }


            }

            return selectedColumn;
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


            if(dgv.CurrentCell.ColumnIndex > 11)
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

                    if(CheckTotalSelectedColumn(dgv) == 1)
                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    dgv.Focus();

                    
                    string headerName = dgv.Columns[e.ColumnIndex].Name;
                    int colIndex = e.ColumnIndex;
                    bool validCelltoShowMenu = false;

                    if (colIndex <= 11)
                    {
                        //get itemCode
                        string itemCode_1 = dgv.Rows[e.RowIndex].Cells[header_Code].Value.ToString();
                        string itemCode_2 = "";

                        string itemCode = itemCode_1;
                        //string stdPacking = dgv.Rows[e.RowIndex].Cells[header_QtyPerBag].Value.ToString();
                        int GoodsQtyPerBag = int.TryParse(dgv.Rows[e.RowIndex].Cells[header_QtyPerBag].Value.ToString(), out int i) ? i : 0;
                        int GoodsReadyStockPcsQty = int.TryParse(dgv.Rows[e.RowIndex].Cells[header_StockPcs].Value.ToString(), out i) ? i : 0;

                        int nextRowIndex = e.RowIndex + 1;

                        if(nextRowIndex > 0)
                        {
                            itemCode_2 = dgv.Rows[nextRowIndex].Cells[header_Code].Value.ToString();
                           
                        }

                        if(string.IsNullOrEmpty(itemCode_1))
                        {
                            itemCode = itemCode_2;
                            GoodsQtyPerBag = int.TryParse(dgv.Rows[nextRowIndex].Cells[header_QtyPerBag].Value.ToString(), out  i) ? i : 0;
                            GoodsReadyStockPcsQty = int.TryParse(dgv.Rows[nextRowIndex].Cells[header_StockPcs].Value.ToString(), out i) ? i : 0;
                        }

                        if (string.IsNullOrEmpty(itemCode_1) && string.IsNullOrEmpty(itemCode_2))
                        {

                        }
                        else
                        {
                            validCelltoShowMenu = true;

                            //search joint group and find Body itemCode
                            DataTable dt = tool.getJoinDataTableFromDataTable(DB_JOIN_ALL, itemCode);

                            string headerParentCode = "PARENT CODE";
                            string headerChildCode = "CHILD CODE";
                            string headerJoinQty = "JOIN QTY";
                            string headerJoinMax = "JOIN MAX";
                            string headerJoinMin = "JOIN MIN";
                            string headerMainCarton = "MAIN CARTON";

                            int joinMax = 0;
                            int joinMin = 0;
                            int joinQty = 0;

                            string bodyCode = "";
                            bool isOring = false;

                            string equalItemCode = "";
                            //f(itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C" && itemCode[10].ToString() != "9")
                            if (itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C" && itemCode[10].ToString() != "9" && itemCode[10].ToString() != "1")
                            {
                                foreach (DataRow row in dalJoin.SelectWithChildCat().Rows)
                                {
                                    if (itemCode == row[dalJoin.ParentCode].ToString())
                                    {
                                        string childCode = row[dalJoin.ChildCode].ToString();

                                        if (childCode.Substring(0, 2) == "CF")
                                        {
                                            equalItemCode = childCode;

                                            break;
                                        }
                                    }
                                }
                            }

                            foreach (DataRow row in dt.Rows)
                            {
                                string ParentCode = row[headerParentCode].ToString();
                                string ChildCode = row[headerChildCode].ToString();
                              
                                if(ParentCode.Contains("CFPOR"))
                                {
                                    isOring = true;
                                }

                                if (ParentCode.Equals(itemCode) || ParentCode.Equals(equalItemCode))
                                {
                                    foreach(DataRow itemRow in DB_ITEM_ALL.Rows)
                                    {
                                        string searching_Code = itemRow[dalItem.ItemCode].ToString();
                                        string itemCategory = itemRow[dalItem.CategoryTblCode].ToString();
                                        string itemType = itemRow[dalItem.TypeTblCode].ToString();
                                        
                                        if(isOring)
                                        {
                                            if (searching_Code.Equals(ChildCode) && itemType.Equals("5"))
                                            {
                                                bodyCode = ChildCode;

                                                joinMax = int.TryParse(row[headerJoinMax].ToString(), out i) ? i : 0;
                                                joinMin = int.TryParse(row[headerJoinMin].ToString(), out i) ? i : 0;
                                                joinQty = int.TryParse(row[headerJoinQty].ToString(), out i) ? i : 0;

                                                break;
                                            }
                                        }
                                        else if (searching_Code.Equals(ChildCode) && itemCategory.Equals("2"))
                                        {
                                            bodyCode = ChildCode;

                                            joinMax = int.TryParse(row[headerJoinMax].ToString(), out i) ? i : 0;
                                            joinMin = int.TryParse(row[headerJoinMin].ToString(), out i) ? i : 0;
                                            joinQty = int.TryParse(row[headerJoinQty].ToString(), out i) ? i : 0;

                                            break;

                                        }

                                     
                                    }
                                }

                                if(!string.IsNullOrEmpty(bodyCode))
                                {
                                    break;
                                }
                            }

                            int SemenyihBodyStock = 0;
                            int BinaBodyStock = 0;
                            
                            //get body item stock qty: Semenyih & Bina
                            foreach(DataRow row in DB_STOCK_ALL.Rows)
                            {
                                string StockLocation = row["fac_name"].ToString();
                                string stockItem = row["item_code"].ToString();

                                if(stockItem.Equals(bodyCode))
                                {
                                    if(StockLocation.Equals(text.Factory_Semenyih))
                                    {

                                        string stock = row["stock_qty"].ToString();
                                        decimal decimalStock;
                                        if (decimal.TryParse(stock, out decimalStock))
                                        {
                                            SemenyihBodyStock = (int)decimalStock;  // Convert decimal to integer
                                        }
                                        else
                                        {
                                            SemenyihBodyStock = 0;  // Handle parsing failure
                                        }

                                    }
                                    else if(StockLocation.Equals(text.Factory_Bina))
                                    {
                                        BinaBodyStock = int.TryParse(row["stock_qty"].ToString(), out i) ? i : 0;

                                        string stock = row["stock_qty"].ToString();
                                        decimal decimalStock;
                                        if (decimal.TryParse(stock, out decimalStock))
                                        {
                                            BinaBodyStock = (int)decimalStock;  // Convert decimal to integer
                                        }
                                        else
                                        {
                                            BinaBodyStock = 0;  // Handle parsing failure
                                        }

                                    }

                                }


                            }

                            //get total possible bag qty: body stock / (std qty/bag) / joint qty

                            //show data: Semenyih & Bina
                            string unit = "Bags";

                            if(isOring)
                            {
                                unit = "Packets";
                            }

                            string bodyStockString = "BODY STOCK (" + bodyCode +")";
                            my_menu.Items.Add(bodyStockString).Name = bodyStockString;

                            string divideLine = "========================";
                            my_menu.Items.Add(divideLine).Name = divideLine;

                            int SemenyihBodyStock_Bag = GoodsQtyPerBag > 0? SemenyihBodyStock/GoodsQtyPerBag : 0;
                            int BinaBodyStock_Bag = GoodsQtyPerBag > 0 ? BinaBodyStock / GoodsQtyPerBag : 0;

                            dt = tool.getJoinDataTableFromDataTable(DB_JOIN_ALL, bodyCode);
                            itemBLL bodyItem = new itemBLL();

                            bodyItem.packaging_code = "CTR 003";
                            bodyItem.packaging_name = "CONTAINER WHITE";
                            bodyItem.standard_packing_qty = 0;

                            foreach (DataRow row in dt.Rows)
                            {
                                string ParentCode = row[headerParentCode].ToString();

                                if (ParentCode.Equals(bodyCode))
                                {
                                    string ChildCode = row[headerChildCode].ToString();

                                    string ChildCat = tool.getItemCatFromDataTable(DB_ITEM_ALL, ChildCode);

                                    if(ChildCat == text.Cat_Carton)
                                    {
                                        bool mainCarton = bool.TryParse(row[headerMainCarton].ToString(), out mainCarton) ? mainCarton : false;

                                        if(mainCarton)
                                        {
                                            bodyItem.packaging_code = ChildCode;
                                            bodyItem.packaging_name = tool.getItemNameFromDataTable(DB_ITEM_ALL, ChildCode);

                                            bodyItem.standard_packing_qty = int.TryParse(row[headerJoinMax].ToString(), out i) ? i : 0;
                                            break;

                                        }
                                        else
                                        {
                                            bodyItem.packaging_code = ChildCode;
                                            bodyItem.packaging_name = tool.getItemNameFromDataTable(DB_ITEM_ALL, ChildCode);
                                            bodyItem.standard_packing_qty = int.TryParse(row[headerJoinMax].ToString(), out i) ? i : 0;
                                        }

                                    }

                                  
                                }

                            }

                            bodyItem.item_code = bodyCode;
                            bodyItem.item_name = tool.getItemNameFromDataTable(DB_ITEM_ALL, bodyCode);
                            bodyItem.item_ready_stock = SemenyihBodyStock;
                            

                            itemBLL goodsItem = new itemBLL();

                            goodsItem.item_code = itemCode;
                            goodsItem.item_name = tool.getItemNameFromDataTable(DB_ITEM_ALL, itemCode);
                            goodsItem.item_ready_stock = GoodsReadyStockPcsQty;
                            goodsItem.standard_packing_qty = GoodsQtyPerBag;

                            frmSBBBodyCalculation frm = new frmSBBBodyCalculation(bodyItem, goodsItem, 1);

                            frm.StartPosition = FormStartPosition.CenterScreen;

                            frm.Show();

                            if(frmSBBBodyCalculation.PACKING_INFO_CHANGED)
                            {
                                DB_JOIN_ALL = dalJoin.SelectAll();
                            }

                            //string SemenyihStockString = "SEMENYIH: " + SemenyihBodyStock + " ( " + SemenyihBodyStock_Bag + " " +unit+")";
                            //string BinaStockString     = "BINA         : " + BinaBodyStock + " ( " + BinaBodyStock_Bag + " " + unit + ")";

                            //my_menu.Items.Add(SemenyihStockString).Name = SemenyihStockString;
                            //my_menu.Items.Add(BinaStockString).Name = BinaStockString;

                            validCelltoShowMenu = false;
                        }

                    }
                    else
                    {
                        validCelltoShowMenu = true;

                        my_menu.Items.Add(text_Reset).Name = text_Reset;
                        my_menu.Items.Add(text_ResetAll).Name = text_ResetAll;
                        my_menu.Items.Add(text_ResetAllByCustomer).Name = text_ResetAllByCustomer;
                        my_menu.Items.Add(text_FillIn).Name = text_FillIn;
                        my_menu.Items.Add(text_FillAll).Name = text_FillAll;
                        my_menu.Items.Add(text_FillAllByCustomer).Name = text_FillAllByCustomer;

                        if(headerName.Contains(header_Priority))
                        {
                            my_menu.Items.Add(text_JumpToNextItem).Name = text_JumpToNextItem;
                            my_menu.Items.Add(text_JumpToLastItem).Name = text_JumpToLastItem;
                            my_menu.Items.Add(text_JumpToFirstItem).Name = text_JumpToFirstItem;

                        }

                        if (dgv.SelectionMode != DataGridViewSelectionMode.CellSelect)
                        {
                            my_menu.Items.Add(text_AddDO).Name = text_AddDO;
                        }
                    }
                
                    //my_menu.Items.Add(text_NewTrip).Name = text_NewTrip;

                    if(validCelltoShowMenu)
                    {
                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(DOList_ItemClicked);
                    }
                    

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
                stopAfterBalUpdate = true;
                if (rowIndex >= 0 && (ClickedItem.Equals(text_AddDO) || ClickedItem.Equals(text_JumpToLastItem) || ClickedItem.Equals(text_JumpToFirstItem) || ClickedItem.Equals(text_JumpToNextItem) || ClickedItem.Equals(text_ResetAll) || ClickedItem.Equals(text_ResetAllByCustomer)  || ClickedItem.Equals(text_FillAll) || ClickedItem.Equals(text_FillAllByCustomer) || CheckIfValidCellSelected()))
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
                    else if (ClickedItem.Equals(text_AddDO))
                    {
                        OpenDO(dgv);
                    }
                    else if (ClickedItem.Equals(text_JumpToNextItem))
                    {
                        JumpToNextItem(dgv);
                    }
                    else if (ClickedItem.Equals(text_JumpToLastItem))
                    {
                        JumpToLastItem(dgv);
                    }
                    else if (ClickedItem.Equals(text_JumpToFirstItem))
                    {
                        JumpToFirstItem(dgv);
                    }
                    CalculateTotalBag(dgv);
                }
                stopAfterBalUpdate = false;


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

        private void dgvList_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;
        }

        private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(!stopAfterBalUpdate)
            UpdateBalAfterDelivery();
        }
    }
}
