using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOUGPOList : Form
    {
        public frmOUGPOList()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvPOList, true);
            tool.DoubleBuffered(dgvItemList, true);


            btnFilter.Text = text_HideFilter;
            dt_POList = dalSPP.POSelect();
            LoadCustomerList();

        }

        #region variable/object declare

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

        Tool tool = new Tool();
        Text text = new Text();

        private readonly string text_100Percentage = "100%";

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string text_SelectPO = "PLEASE SELECT P/O";
        readonly string text_AddDo = "ADD D/O";
        readonly string text_NextStep = "NEXT STEP";
        readonly string text_SelectAll = "SELECT ALL";
        readonly string text_AddNewPO = "+ NEW P/O";
        readonly string text_POList = "P/O LIST";
        readonly string text_POItemList = "P/O ITEM LIST";
        readonly string text_CombineDO = "COMBINE D/O";
        readonly string text_DOUpdate = "D/O UPDATE";
        readonly string text_Cancel = "CANCEL";
        readonly string text_CombineConfirm = "COMBINE CONFIRM";

        readonly string text_FullyDelivered = "Fully Delivered";
        readonly string text_Freeze = "Freeze";
        readonly string text_Unfreeze = "Unfreeze";
        readonly string text_UndoDelivered = "Undo Delivered";
        readonly string text_EditDelivered = "Edit Delivered Qty";
        readonly string text_StopEdit = "Stop Edit";

        readonly string text_DOList = "D/O TO ADD";
        readonly string text_EditDOList = "D/O TO EDIT";
        readonly string text_DOItemList = "D/O ITEM LIST";

        readonly string text_AvailableStock = "AVAILABLE";
        readonly string text_InsufficientStock = "INSUFFICIENT";

        readonly string text_DB = "DB";
        readonly string text_ToAdd = "TO ADD";
        readonly string text_ToEdit = "TO EDIT";
        readonly string text_ToUpdate = "TO UPDATE";

        readonly string header_POTblCode = "P/O TBL CODE";
        readonly string header_POCode = "P/O CODE";
        readonly string header_PONoString = "P/O NO";
        readonly string header_PODate = "P/O RECEIVED DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_Progress = "PROGRESS";
        readonly string header_Selected = "SELECTED";
        readonly string header_DataMode = "DATA MODE";
        readonly string header_Freeze = "Freeze";

        readonly string header_Index = "#";
        readonly string header_Size_1 = "SIZE 1";
        readonly string header_Size_2 = "SIZE 2";

        readonly string header_SizeString = "SIZE";
        readonly string header_Unit_1 = "UNIT 1";
        readonly string header_Unit_2 = "UNIT 2";
        readonly string header_Type = "TYPE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_OrderQty = "ORDER QTY(PCS)";
        readonly string header_OrderQtyString = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY(PCS)";
        readonly string header_DeliveredQtyString = "DELIVERED QTY";
        readonly string header_Note = "NOTE";
        readonly string header_StockCheck = "STOCK CHECK";
        readonly string header_DONo = "D/O #";
        readonly string header_DOTblCode = "D/O TBL CODE";
        readonly string header_DONoString = "D/O NO";
        readonly string header_CombinedCode = "COMBINED CODE";
        readonly string header_Stock = "STOCK QTY";
        readonly string header_StockString = "STOCK";
        readonly string header_DeliveryPCS = "DELIVERY PCS";
        readonly string header_DeliveryBAG = "DELIVERY BAG";
        readonly string header_DeliveryQTY = "DELIVERY QTY";
        readonly string header_Balance = "BAL No";
        readonly string header_StdPacking = "StdPacking";
        readonly string header_BalanceString = "BAL.";
        readonly string header_ToDeliveryPCSQty = "TO DELIVERY PCS QTY";

        private DataTable dt_POList;
        private DataTable dt_DOItemList_1;
        private DataTable dt_DOEditList;
        private DataTable dt_DOItemOriginalList;
        private DataTable dt_PlannerPO;
        private DataTable dt_PlannerPOItem;
        private bool addingDOMode = false;
        private bool ableToNextStep = false;
        private bool addingDOStep1 = false;
        private bool POMode = true;
        private bool addingDOStep2 = false;
        private bool combineDO = false;
        private bool combiningDO = false;
        private bool EditMode = false;
        private bool DeliveredQtyEditMode = false;
        private bool callFromDOPage = false;
        private bool callFromPlanner = false;

        static public bool DOOpened = false;
        private int selectedDO = 0;
        private int oldData = 0;
        private int adjustRow = -1;
        private int adjustCol = -1;
        private int TotalToDeliveryBag = 0;

        
        #endregion

        private DataTable NewExcelTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_Size_1, typeof(string));
            dt.Columns.Add(header_Unit_1, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Freeze, typeof(bool));
            dt.Columns.Add(header_PODate, typeof(DateTime));

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));



            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_Progress, typeof(string));

            dt.Columns.Add(header_DONoString, typeof(string));

            return dt;
        }

        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_StockCheck, typeof(string));
            dt.Columns.Add(header_CombinedCode, typeof(string));
            return dt;
        }

        private DataTable NewPOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_Size_1, typeof(string));
            dt.Columns.Add(header_Unit_1, typeof(string));
            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_DeliveredQtyString, typeof(string));
            dt.Columns.Add(header_OrderQtyString, typeof(string));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private DataTable NewDOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DataMode, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size_1, typeof(string));
            dt.Columns.Add(header_Unit_1, typeof(string));
            dt.Columns.Add(header_Size_2, typeof(string));
            dt.Columns.Add(header_Unit_2, typeof(string));
            dt.Columns.Add(header_SizeString, typeof(string));

            dt.Columns.Add(header_Stock, typeof(int));
            dt.Columns.Add(header_StockString, typeof(string));
            dt.Columns.Add(header_POCode, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));
            dt.Columns.Add(header_Balance, typeof(int));
            dt.Columns.Add(header_BalanceString, typeof(string));
            dt.Columns.Add(header_Note, typeof(string));
            dt.Columns.Add(header_StdPacking, typeof(int));
            return dt;
        }

        private void HideAddedDO(DataGridView dgv)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            foreach(DataRow row in dt.Rows)
            {
                string do_string = row[header_DONoString].ToString();

                if(int.TryParse(do_string, out int i))
                {
                    //dgv.Rows[dt.Rows.IndexOf(row)].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgv.DataSource];
                    currencyManager1.SuspendBinding();
                    dgv.Rows[dt.Rows.IndexOf(row)].Visible = false;
                    currencyManager1.ResumeBinding();

                   
                }
            }
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;

            if (dgv == dgvPOList)
            {
                dgv.Columns[header_PODate].DefaultCellStyle.ForeColor = Color.Gray;
                //dgv.Columns[header_DONo].DefaultCellStyle.ForeColor = Color.Gray;
                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Progress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
                dgv.Columns[header_Freeze].Visible = false;
                
            }
            else if (dgv == dgvItemList)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (POMode || addingDOStep1)
                {

                    dgv.Columns[header_POTblCode].Visible = false;
                    dgv.Columns[header_POCode].Visible = false;
                    dgv.Columns[header_DeliveredQty].Visible = false;
                    dgv.Columns[header_OrderQty].Visible = false;
                    dgv.Columns[header_Size_1].Visible = false;
                    dgv.Columns[header_Unit_1].Visible = false;

                    
                    dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Unit_1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    dgv.Columns[header_DeliveredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_OrderQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else if (addingDOStep2 || EditMode)
                {
                    HideAddedDO(dgv);
                    dgv.Columns[header_DONo].Visible = false;
                    dgv.Columns[header_Stock].Visible = false;
                    dgv.Columns[header_DeliveryPCS].Visible = false;
                    dgv.Columns[header_DeliveryBAG].Visible = false;
                    dgv.Columns[header_POCode].Visible = false;
                    dgv.Columns[header_Note].Visible = false;
                    dgv.Columns[header_StdPacking].Visible = false;
                    dgv.Columns[header_Balance].Visible = false;
                    dgv.Columns[header_DataMode].Visible = false;
                    dgv.Columns[header_POTblCode].Visible = false;
                    dgv.Columns[header_DOTblCode].Visible = false;
                    dgv.Columns[header_Size_1].Visible = false;
                    dgv.Columns[header_Unit_1].Visible = false;
                    dgv.Columns[header_Size_2].Visible = false;
                    dgv.Columns[header_Unit_2].Visible = false;

                    //dgv.Columns[header_DeliveredQty].Visible = false;
                    //dgv.Columns[header_OrderQty].Visible = false;
                    dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[header_BalanceString].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    dgv.Columns[header_StockString].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    dgv.Columns[header_DONoString].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    //dgv.Columns[header_StockString].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    dgv.Columns[header_DeliveryQTY].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    //dgv.Columns[header_Size].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    //dgv.Columns[header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                    dgv.Columns[header_DONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                   // dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[header_Unit_1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_DeliveryPCS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_DeliveredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_DeliveryBAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                //else if (addingDOStep1)
                //{
                //    dgv.Columns[header_DeliveredQty].Visible = false;
                //    dgv.Columns[header_OrderQty].Visible = false;
                //    dgv.Columns[header_POTblCode].Visible = false;
                //    dgv.Columns[header_POCode].Visible = false;

                //    dgv.Columns[header_Size].Visible = false;
                //    dgv.Columns[header_Unit].Visible = false;
                //}
                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }

        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowFilter(bool showMoreFilter)
        {
            if (showMoreFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowDOList()
        {
            lblMainList.Text = text_DOList;
            lblSubList.Text = text_DOItemList;

            if (EditMode)
            {
                lblMainList.Text = text_EditDOList;
            }

            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 45);
        }

        private void ShowPOListUI()
        {
            lblMainList.Text = text_POList;
            lblSubList.Text = text_POItemList;


            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 45);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void frmSPPPOList_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();
            ShowPOListUI();
            LoadPOList();

            lblSubList.Text = text_POItemList;
        }

        private void LoadCustomerList()
        {
            DataTable dt = dalSPP.CustomerWithoutRemovedDataSelect();

            DataTable locationTable = dt.DefaultView.ToTable(true, dalSPP.FullName);

            DataRow dr;
            dr = locationTable.NewRow();
            dr[dalSPP.FullName] = "ALL";

            locationTable.Rows.InsertAt(dr, 0);

            locationTable.DefaultView.Sort = dalSPP.FullName + " ASC";

            cmbCustomer.DataSource = locationTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;

        }

        private DataTable AddSpaceToList(DataTable dt)
        {
            string preType = null;
            DataTable dt_Copy = dt.Copy();

            foreach (DataRow row in dt.Rows)
            {
                string type = row[header_Type].ToString();

                if (preType == null)
                {
                    preType = type;
                }
                else if (preType != type)
                {
                    preType = type;
                    dt.Rows.Add();
                    dt.AcceptChanges();
                }
            }

            return dt;
        }

        private string GetDONo(DataTable dt_DO, string poNo, string custFullName)
        {
            string DONo = "";

            string preDoNo = null;
            foreach (DataRow row in dt_DO.Rows)
            {
                string currentDO = row[dalSPP.DONo].ToString();
                string PONo = row[dalSPP.PONo].ToString();
                string custFullName_DB = row[dalSPP.FullName].ToString();
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (poNo == PONo && !isRemoved && custFullName == custFullName_DB)
                {
                    if (preDoNo == null || preDoNo != currentDO)
                    {

                        if (preDoNo != null)
                        {
                            DONo += "/";
                        }

                        DONo += Convert.ToInt16(currentDO).ToString("D6");
                        preDoNo = currentDO;
                    }

                }
            }

            return DONo;
        }

        public Tuple<int, int> GetPendingPOQty()
        {
            int pendingPOQty = 0;
            int pendingCustQty = 0;

            DataTable dt = NewPOTable();
            DataTable dt_DOList = dalSPP.DOWithInfoSelect();

            DataRow dt_row;

            int poCode = -1;
            int prePOCode = -1;
            int orderQty = 0;
            int deliveredQty = 0;
            int CustTblCode = -1;
            int progress = -1;
            bool dataMatched = true;
            string PONo = null, ShortName = null, FullName = null;
            string customer = null;

            DateTime PODate = DateTime.MaxValue;

            foreach (DataRow row in dt_POList.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                

                if (!isRemoved)
                {
                    poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;

                    if (prePOCode == -1)
                    {
                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();
                        FullName = row[dalSPP.FullName].ToString();

                        CustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                    else if (prePOCode == poCode)
                    {
                        int temp = 0;
                        orderQty += int.TryParse(row[dalSPP.POQty].ToString(), out temp) ? temp : 0;
                        deliveredQty += int.TryParse(row[dalSPP.DeliveredQty].ToString(), out temp) ? temp : 0;
                    }
                    else if (prePOCode != poCode)
                    {
                        progress = Convert.ToInt32((float)deliveredQty / orderQty * 100);
                        dataMatched = true;

                        #region PO TYPE

                        if (progress < 100 && cbInProgressPO.Checked)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else if (progress >= 100 && cbCompletedPO.Checked)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        #region Customer Filter

                        customer = cmbCustomer.Text;
                        //string sppCustomer = row[dalSPP.FullName].ToString();
                        if (string.IsNullOrEmpty(customer) || customer == "ALL")
                        {
                            dataMatched &= true;
                        }
                        else if (FullName == customer)
                        {
                            dataMatched &= true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        if (dataMatched)
                        {
                            dt_row = dt.NewRow();

                            dt_row[header_POCode] = prePOCode;
                            dt_row[header_PONoString] = PONo;
                            dt_row[header_PODate] = PODate;
                            dt_row[header_Customer] = ShortName;
                            dt_row[header_CustomerCode] = CustTblCode;
                            dt_row[header_Progress] = progress + "%";
                            dt_row[header_DONoString] = GetDONo(dt_DOList, PONo, FullName);
                            dt.Rows.Add(dt_row);
                        }

                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();
                        FullName = row[dalSPP.FullName].ToString();
                        CustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                }

            }

            progress = Convert.ToInt32((float)deliveredQty / orderQty * 100);
            dataMatched = true;

            #region PO TYPE

            if (progress < 100)
            {
                dataMatched = dataMatched && true;
            }
            else
            {
                dataMatched = false;
            }

            #endregion

            if (dataMatched)
            {
                dt_row = dt.NewRow();

                dt_row[header_POCode] = prePOCode;
                dt_row[header_PONoString] = PONo;
                dt_row[header_PODate] = PODate;
                dt_row[header_Customer] = ShortName;
                dt_row[header_CustomerCode] = CustTblCode;
                dt_row[header_Progress] = progress + "%";
                dt_row[header_DONoString] = GetDONo(dt_DOList, PONo, FullName);
                dt.Rows.Add(dt_row);
            }

            string previousCustCode = "";

            dt.DefaultView.Sort = header_CustomerCode+" ASC";
            dt = dt.DefaultView.ToTable();

            foreach(DataRow row in dt.Rows)
            {
                pendingPOQty++;
                string custCode = row[header_CustomerCode].ToString();
                if (previousCustCode == "")
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
                else if(previousCustCode != custCode)
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
            }

            return Tuple.Create(pendingPOQty, pendingCustQty);
        }

        private void LoadPOList()
        {
            btnEdit.Visible = false;
            dgvItemList.DataSource = null;

            DataTable dt = NewPOTable();
            DataTable dt_DOList = dalSPP.DOWithInfoSelect();

            DataRow dt_row;

            int poCode = -1;
            int prePOCode = -1;
            int orderQty = 0;
            int deliveredQty = 0;
            int CustTblCode = -1;
            int progress = -1;
            bool dataMatched = true;
            string PONo = null, ShortName = null, FullName = null;
            string customer = null;
            bool isFreeze = false;
            DateTime PODate = DateTime.MaxValue;

            foreach (DataRow row in dt_POList.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;

                    if (prePOCode == -1)
                    {
                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                        isFreeze = bool.TryParse(row[dalSPP.Freeze].ToString(), out isFreeze) ? isFreeze : false;

                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();
                        FullName = row[dalSPP.FullName].ToString();

                        CustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                    else if (prePOCode == poCode)
                    {
                        int temp = 0;
                        orderQty += int.TryParse(row[dalSPP.POQty].ToString(), out temp) ? temp : 0;
                        deliveredQty += int.TryParse(row[dalSPP.DeliveredQty].ToString(), out temp) ? temp : 0;
                    }
                    else if (prePOCode != poCode)
                    {
                        progress = (int)((float)deliveredQty / orderQty * 100);
                        dataMatched = true;

                        #region PO TYPE

                        if (progress < 100 && cbInProgressPO.Checked)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else if (progress >= 100 && cbCompletedPO.Checked)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        #region Customer Filter

                        customer = cmbCustomer.Text;
                        //string sppCustomer = row[dalSPP.FullName].ToString();
                        if (string.IsNullOrEmpty(customer) || customer == "ALL")
                        {
                            dataMatched &= true;
                        }
                        else if (FullName == customer)
                        {
                            dataMatched &= true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        if (dataMatched)
                        {
                            dt_row = dt.NewRow();

                            //int test =(int)((float)deliveredQty / orderQty * 100);

                            dt_row[header_Freeze] = isFreeze;
                            dt_row[header_POCode] = prePOCode;
                            dt_row[header_PONoString] = PONo;
                            dt_row[header_PODate] = PODate;
                            dt_row[header_Customer] = ShortName;
                            dt_row[header_CustomerCode] = CustTblCode;
                            dt_row[header_Progress] = progress + "%";
                            dt_row[header_DONoString] = GetDONo(dt_DOList, PONo, FullName);
                            dt.Rows.Add(dt_row);
                        }

                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                        isFreeze = bool.TryParse(row[dalSPP.Freeze].ToString(), out isFreeze) ? isFreeze : false;
                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();
                        FullName = row[dalSPP.FullName].ToString();
                        CustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                }

            }

            progress = (int)((float)deliveredQty / orderQty * 100);
            dataMatched = true;

            #region PO TYPE

            if (progress < 100 && cbInProgressPO.Checked)
            {
                dataMatched = dataMatched && true;
            }
            else if (progress >= 100 && cbCompletedPO.Checked)
            {
                dataMatched = dataMatched && true;
            }
            else
            {
                dataMatched = false;
            }

            #endregion

            #region Customer Filter

            customer = cmbCustomer.Text;

            if (string.IsNullOrEmpty(customer) || customer == "ALL")
            {
                dataMatched = dataMatched && true;
            }
            else if (FullName == customer)
            {
                dataMatched = dataMatched && true;
            }
            else
            {
                dataMatched = false;
            }

            #endregion

            if (dataMatched)
            {
                dt_row = dt.NewRow();

                dt_row[header_Freeze] = isFreeze;
                dt_row[header_POCode] = prePOCode;
                dt_row[header_PONoString] = PONo;
                dt_row[header_PODate] = PODate;
                dt_row[header_Customer] = ShortName;
                dt_row[header_CustomerCode] = CustTblCode;
                dt_row[header_Progress] = progress + "%";
                dt_row[header_DONoString] = GetDONo(dt_DOList, PONo, FullName);
                dt.Rows.Add(dt_row);
            }


            dgvPOList.DataSource = dt;
            DgvUIEdit(dgvPOList);
            dgvPOList.ClearSelection();

        }

        private void btnAddNewPO_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSBBNewPO frm = new frmSBBNewPO
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();
            dt_POList = dalSPP.POSelect();
            LoadPOList();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadPOList();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvItemList.DataSource = null;
        }

        private void cbInProgressPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvItemList.DataSource = null;
        }

        private void cbCompletedPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvItemList.DataSource = null;
        }

        private void dgvPOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvPOList;

            if (rowIndex >= 0)
            {
                btnEdit.Visible = true;
                string code = dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvItemList.DataSource = null;
                btnEdit.Visible = false;
            }
        }

        private void ShowPOItem(string poCode)
        {
            DataTable dt = dalSPP.POSelectWithSizeAndType();

            DataTable dt_POItemList = NewPOItemTable();
            DataRow dt_Row;
            int index = 1;
            int totalBag = 0;
            string preType = null;

            foreach (DataRow row in dt.Rows)
            {
                if (poCode == row[dalSPP.POCode].ToString())
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                    if(!isRemoved)
                    {
                        int deliveredQty = row[dalSPP.DeliveredQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.DeliveredQty].ToString());
                        int poQty = row[dalSPP.POQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.POQty].ToString());
                        int qtyPerBag = row[dalSPP.QtyPerBag] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.QtyPerBag].ToString());

                        int poBag = 0;

                        if (poQty > 0 && qtyPerBag > 0)
                        {
                            poBag = poQty / qtyPerBag;
                        }

                        totalBag += poBag;

                        string type = row[dalSPP.TypeName].ToString();

                        if (preType == null)
                        {
                            preType = type;
                        }
                        else if (preType != type)
                        {
                            preType = type;
                            dt_Row = dt_POItemList.NewRow();
                            dt_POItemList.Rows.Add(dt_Row);
                        }

                        int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                        int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                        string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                        int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                        int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                        string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                        string sizeString = "";
                        string sizeString_1 = "";
                        string sizeString_2 = "";

                        int size_1 = 1;
                        int size_2 = 1;

                        if (denominator == 1)
                        {
                            size_1 = numerator;
                            sizeString_1 = numerator.ToString();

                        }
                        else
                        {
                            size_1 = numerator / denominator;
                            sizeString_1 = numerator + "/" + denominator;
                        }

                        if (numerator_2 > 0)
                        {
                            if (denominator_2 == 1)
                            {
                                sizeString_2 += numerator_2;
                                size_2 = numerator_2;

                            }
                            else
                            {
                                size_2 = numerator_2 / denominator_2;

                                if (numerator_2 == 3 && denominator_2 == 2)
                                {
                                    sizeString_2 += "1 1" + "/" + denominator_2;
                                }
                                else
                                {
                                    sizeString_2 += numerator_2 + "/" + denominator_2;
                                }
                               

                               
                            }
                        }

                        if(size_1 >= size_2)
                        {
                            if(sizeString_2 != "")
                            sizeString = sizeString_1 + " x " + sizeString_2;

                            else
                            {
                                sizeString = sizeString_1;
                            }
                        }
                        else
                        {

                            if (sizeString_2 != "")
                                sizeString = sizeString_2 + " x " + sizeString_1;

                            else
                            {
                                sizeString = sizeString_2;
                            }
                        }

                        dt_Row = dt_POItemList.NewRow();
                        dt_Row[header_Index] = index;
                        dt_Row[header_POTblCode] = row[dalSPP.TableCode];
                        dt_Row[header_POCode] = row[dalSPP.POCode];
                        dt_Row[header_Size_1] = row[dalSPP.SizeNumerator];
                        dt_Row[header_Unit_1] = row[dalSPP.SizeUnit].ToString().ToUpper();

                        dt_Row[header_SizeString] = sizeString;
                        dt_Row[header_Type] = type;
                        dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = deliveredQty;
                        dt_Row[header_OrderQty] = row[dalSPP.POQty];


                        dt_Row[header_DeliveredQtyString] = deliveredQty + " (" + (deliveredQty / qtyPerBag).ToString() + " bags)";

                        if(deliveredQty % qtyPerBag != 0)
                        {
                            dt_Row[header_DeliveredQtyString] = deliveredQty + " (" + (deliveredQty / qtyPerBag).ToString() + " bags + " + (deliveredQty % qtyPerBag).ToString() + " pcs)";
                        }

                        int balancePcs = poQty % qtyPerBag;

                        string deliveryQtyString = row[dalSPP.POQty].ToString() + " (" + poBag + " bags)";

                        if (balancePcs != 0)
                        {
                            deliveryQtyString = row[dalSPP.POQty].ToString() + " (" + poBag + " bags + " + balancePcs + " pcs)";
                        }

                        dt_Row[header_OrderQtyString] = deliveryQtyString;


                        string note = row[dalSPP.PONote].ToString();

                        note = note.Replace("(" + poBag + " BAGS)", string.Empty);
                        dt_Row[header_Note] = note;

                        dt_POItemList.Rows.Add(dt_Row);
                        index++;
                    }

                    
                }
            }

            //dt_POItemList = AddSpaceToList(dt_POItemList);

            lblSubList.Text = text_POItemList + " ( Total Bags: " + totalBag + ")";
            dgvItemList.DataSource = dt_POItemList;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();
        }

        private void EditPO()
        {
            //get item info
            DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = dgvPOList.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();

                frmSBBNewPO frm = new frmSBBNewPO(code)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSBBNewPO.poEdited || frmSBBNewPO.poRemoved)
                {
                    btnEdit.Visible = false;
                    dt_POList = dalSPP.POSelect();
                    LoadPOList();
                }

            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private DataTable GetDOItem(DataTable dt_DBSource, string poCode, int DONo, string customerName)
        {
            DataTable dt_DOItemList = NewDOItemTable();
            DataRow dt_Row;

            string preType = null;
            foreach (DataRow row in dt_DBSource.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                //int doNumber = int.TryParse(row[dalSPP.DONo].ToString(), out doNumber) ? doNumber : -1;

                string DONo_DB = DONo.ToString();

                if (EditMode)
                {
                    DONo_DB = row[dalSPP.DONo].ToString();
                }

                if (poCode == row[dalSPP.POCode].ToString() && !isRemoved && DONo_DB == DONo.ToString())
                {
                    string type = row[dalSPP.TypeName].ToString();

                    if (preType == null)
                    {
                        preType = type;
                    }
                    else if (preType != type)
                    {
                        preType = type;
                        //dt_Row = dt_DOItemList.NewRow();
                        //dt_DOItemList.Rows.Add(dt_Row);
                    }

                    int stockQty = int.TryParse(row[dalItem.ItemStock].ToString(), out stockQty) ? stockQty : 0;
                    int deliveryQty = int.TryParse(row[dalSPP.POQty].ToString(), out deliveryQty) ? deliveryQty : 0;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;


                    if (EditMode)
                    {
                        deliveryQty = int.TryParse(row[dalSPP.DOToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;
                    }
                    else if(callFromPlanner)
                    {
                        deliveryQty = 0;

                        string poTblCode = row[dalSPP.TableCode].ToString();

                        //loop po item list from "PO VS Stock"
                        foreach (DataRow itemRow in dt_PlannerPOItem.Rows)
                        {
                            //search item by po table code
                            if(poTblCode == itemRow[header_POTblCode].ToString())
                            {
                                //get to delivery qty in pcs
                                int itemToDeliveryPcs = int.TryParse(itemRow[header_ToDeliveryPCSQty].ToString(), out itemToDeliveryPcs) ? itemToDeliveryPcs : 0;
                                deliveryQty = itemToDeliveryPcs;
                                break;
                            }

                        }

                    }

                    int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                    int deliveryBag = deliveryQty / qtyPerBag;

                   
                    if (deliveredQty != orderQty && deliveryQty != 0)
                    {
                        dt_Row = dt_DOItemList.NewRow();


                        if (EditMode)
                        {
                            //string test 
                            dt_Row[header_POTblCode] = row[dalSPP.POTableCode];
                            //dt_Row[header_POTblCode] = row[dalSPP.TableCode];
                        }
                        else
                        {
                            dt_Row[header_POTblCode] = row[dalSPP.TableCode];

                            if (!callFromPlanner)
                            {
                                deliveryQty -= deliveredQty;//auto set delivery qty by system
                            }

                        }


                        deliveryBag = deliveryQty / qtyPerBag;


                        dt_Row[header_DataMode] = text_ToAdd;


                        #region SizeData

                        int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                        int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                        string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                        int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                        int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                        string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                        string sizeString = "";
                        string sizeString_1 = "";
                        string sizeString_2 = "";

                        int size_1 = 1;
                        int size_2 = 1;

                        if (denominator == 1)
                        {
                            size_1 = numerator;
                            sizeString_1 = numerator.ToString();

                        }
                        else
                        {
                            size_1 = numerator / denominator;
                            sizeString_1 = numerator + "/" + denominator;
                        }

                        if (numerator_2 > 0)
                        {
                            if (denominator_2 == 1)
                            {
                                sizeString_2 += numerator_2;
                                size_2 = numerator_2;

                            }
                            else
                            {
                                size_2 = numerator_2 / denominator_2;

                                if (numerator_2 == 3 && denominator_2 == 2)
                                {
                                    sizeString_2 += "1 1" + "/" + denominator_2;
                                }
                                else
                                {
                                    sizeString_2 += numerator_2 + "/" + denominator_2;
                                }



                            }
                        }

                        if (size_1 >= size_2)
                        {
                            if (sizeString_2 != "")
                                sizeString = sizeString_1 + " x " + sizeString_2;

                            else
                            {
                                sizeString = sizeString_1;
                            }
                        }
                        else
                        {

                            if (sizeString_2 != "")
                                sizeString = sizeString_2 + " x " + sizeString_1;

                            else
                            {
                                sizeString = sizeString_2;
                            }
                        }

                        #endregion

                        dt_Row[header_Size_1] = sizeString_1;
                        dt_Row[header_Unit_1] = row[dalSPP.SizeUnit].ToString().ToUpper();

                        dt_Row[header_Size_2] = sizeString_2;
                        dt_Row[header_Unit_2] = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                        dt_Row[header_SizeString] = sizeString;

                        dt_Row[header_Type] = type;
                        dt_Row[header_Stock] = row[dalItem.ItemStock];

                        dt_Row[header_StockString] = row[dalItem.ItemStock] + " (" + stockQty / qtyPerBag + "bags)";

                        int stockBalancePcs = stockQty % qtyPerBag;

                        if(stockBalancePcs != 0)
                        {
                            dt_Row[header_StockString] = row[dalItem.ItemStock] + " (" + stockQty / qtyPerBag + "bags + " + stockBalancePcs + " pcs)";
                        }
                        

                        dt_Row[header_POCode] = row[dalSPP.POCode];
                        dt_Row[header_DONo] = DONo;
                        dt_Row[header_DONoString] = DONo.ToString("D6") + "-NEW";

                        if (EditMode)
                        {
                            dt_Row[header_DOTblCode] = row[dalSPP.TableCode];
                            dt_Row[header_DataMode] = text_ToEdit;
                            dt_Row[header_DONoString] = DONo.ToString("D6") + "-EDIT";
                        }

                        dt_Row[header_Customer] = customerName;
                        dt_Row[header_DeliveredQty] = deliveredQty;
                        dt_Row[header_DeliveryPCS] = deliveryQty;
                        dt_Row[header_DeliveryQTY] = deliveryQty + " (" + deliveryBag + "bags)";

                        int deliveryBalancePcs = deliveryQty % qtyPerBag;

                        if(deliveryBalancePcs != 0)
                        {
                            dt_Row[header_DeliveryQTY] = deliveryQty + " (" + deliveryBag + "bags + "+ deliveryBalancePcs + " pcs)";

                        }

                        dt_Row[header_DeliveryBAG] = deliveryBag;
                        dt_Row[header_StdPacking] = qtyPerBag;
                        dt_DOItemList.Rows.Add(dt_Row);
                    }

                    TotalToDeliveryBag += deliveryBag;
                }
            }

            return dt_DOItemList;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditPO();
        }

        private void dgvPOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (!addingDOMode && POMode)
            //    EditPO();

            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvPOList;

            if (rowIndex >= 0 && dgv.SelectedRows.Count > 0)
            {
                string tblName = dalSPP.POTableName;
                string poCode = dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString();

                frmActionLog frm = new frmActionLog(tblName, poCode)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();
            }
        }

        private bool ifPOSelected()
        {
            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (dt.Columns.Contains(header_Selected))
            {
                if(callFromPlanner)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string poNo = row[header_PONoString].ToString();

                        foreach (DataRow rowPlanner in dt_PlannerPO.Rows)
                        {
                            if (poNo == rowPlanner[header_PONoString].ToString())
                            {
                                row[header_Selected] = true;
                            }
                        }


                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;
                    string poNo = row[header_PONoString].ToString();

                    if (selected)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void dgvPOList_SelectionChanged(object sender, EventArgs e)
        {
            if (POMode)
            {
                btnEdit.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
            }

            DataGridView dgv = dgvPOList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
            }


            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvItemList.DataSource = null;
                btnEdit.Visible = false;
            }

        }

        private void CombineCustomerData()
        {
            DataGridView dgv = dgvPOList;

            DataTable dt_TargetPO = (DataTable)dgv.DataSource;
            DataTable dt_DBSource = dalSPP.POSelectWithSizeAndType();

            dt_TargetPO.DefaultView.Sort = header_CustomerCode + " ASC, " + header_PODate + " DESC," + header_POCode + " DESC";
            dt_TargetPO = dt_TargetPO.DefaultView.ToTable();

            dt_DBSource.DefaultView.Sort = dalSPP.CustTblCode + " ASC, " + dalSPP.PODate + " DESC, " + dalSPP.POCode + " DESC";
            dt_DBSource = dt_DBSource.DefaultView.ToTable();

            int preCustomerCode = -1;

            DataTable dt_Excel = NewExcelTable();
            DataRow dt_Row;

            foreach (DataRow row in dt_TargetPO.Rows)
            {
                int poCode = Convert.ToInt32(row[header_POCode].ToString());

                foreach (DataRow db in dt_DBSource.Rows)
                {
                    int dbCode = Convert.ToInt32(db[dalSPP.POCode].ToString());

                    if (dbCode == poCode)
                    {
                        int customerCode = Convert.ToInt32(db[dalSPP.CustTblCode].ToString());
                        string customerShortName = db[dalSPP.ShortName].ToString();

                        if (preCustomerCode == customerCode)
                        {
                            //combine data in same sheet

                        }
                        else
                        {

                            if (preCustomerCode != -1)
                            {
                                dt_Row = dt_Excel.NewRow();
                                dt_Excel.Rows.Add(dt_Row);
                            }

                            preCustomerCode = customerCode;
                            //save data in new sheet
                        }

                        dt_Row = dt_Excel.NewRow();

                        dt_Row[header_POCode] = db[dalSPP.POCode];
                        dt_Row[header_PODate] = db[dalSPP.PODate];
                        dt_Row[header_Size_1] = db[dalSPP.SizeNumerator];
                        dt_Row[header_Unit_1] = db[dalSPP.SizeUnit] + " " + customerShortName;
                        dt_Row[header_Type] = db[dalSPP.TypeName];
                        dt_Row[header_ItemCode] = db[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = db[dalSPP.DeliveredQty];
                        dt_Row[header_OrderQty] = db[dalSPP.POQty];
                        dt_Row[header_Note] = db[dalSPP.PONote];

                        dt_Excel.Rows.Add(dt_Row);

                    }
                }
            }
            dgvItemList.DataSource = null;
            dgvItemList.DataSource = dt_Excel;
            dgvItemList.ClearSelection();
        }


        static void OpenCSVWithExcel(string path)
        {
            var ExcelApp = new Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);

            ExcelApp.Visible = true;
        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "SPP PO REPORT_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void copyDGVtoClipboard(DataGridView dgv)
        {
            dgv.SelectAll();
            DataObject dataObj = dgv.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\SPP PO Report";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xlsx";
                sfd.FileName = "SPPPOReport_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;
                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    //xlWorkBook.SaveAs(sfd.FileName,
                    //    XlFileFormat.xlWorkbookDefault,
                    //    misValue, misValue, misValue, misValue,
                    //    XlSaveAsAccessMode.xlExclusive,
                    //    misValue, misValue, misValue, misValue, misValue);

                    //xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                    //       misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                    xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, Missing.Value,
                    Missing.Value, false, false, XlSaveAsAccessMode.xlExclusive,
                   misValue, misValue,
                    Missing.Value, Missing.Value, Missing.Value);

                    InsertAllDataToSheet(path, sfd.FileName);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    LoadPOList();
                    dgvPOList.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void InsertAllDataToSheet(string path, string fileName)
        {
            Excel.Application excelApp = new Excel.Application
            {
                Visible = true
            };

            Workbook g_Workbook = excelApp.Workbooks.Open(
               path,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing);

            object misValue = Missing.Value;


            DataGridView dgv = dgvPOList;

            DataTable dt_TargetPO = (DataTable)dgv.DataSource;
            DataTable dt_DBSource = dalSPP.POSelectWithSizeAndType();

            dt_TargetPO.DefaultView.Sort = header_CustomerCode + " ASC, " + header_PODate + " DESC," + header_POCode + " DESC";
            dt_TargetPO = dt_TargetPO.DefaultView.ToTable();

            dt_DBSource.DefaultView.Sort = dalSPP.CustTblCode + " ASC, " + dalSPP.PODate + " DESC, " + dalSPP.POCode + " DESC";
            dt_DBSource = dt_DBSource.DefaultView.ToTable();

            int preCustomerCode = -1;
            string customerShortName = "";
            DataTable dt_Excel = NewExcelTable();
            DataRow dt_Row;

            int preCode = -1;

            foreach (DataRow row in dt_TargetPO.Rows)
            {
                int poCode = Convert.ToInt32(row[header_POCode].ToString());

                foreach (DataRow db in dt_DBSource.Rows)
                {
                    int dbCode = Convert.ToInt32(db[dalSPP.POCode].ToString());

                    if (dbCode == poCode)
                    {
                        int customerCode = Convert.ToInt32(db[dalSPP.CustTblCode].ToString());


                        if (preCustomerCode == customerCode)
                        {
                            //combine data in same sheet
                            if (preCode != dbCode)
                            {
                                dt_Excel.Rows.Add(dt_Excel.NewRow());
                                preCode = dbCode;
                            }

                            //if(preCode == -1)
                            //{
                            //    preCode = dbCode;
                            //}
                            //else if(preCode != dbCode)
                            //{
                            //    dt_Excel.Rows.Add(dt_Excel.NewRow());
                            //    preCode = dbCode;
                            //}
                        }
                        else
                        {
                            if (preCustomerCode != -1)
                            {
                                dgvItemList.DataSource = null;
                                dgvItemList.DataSource = dt_Excel;
                                dgvItemList.ClearSelection();

                                MoveUniqueDataToSheet(g_Workbook, customerShortName);

                                dgvItemList.DataSource = null;
                                dt_Excel = NewExcelTable();
                                preCode = -1;

                            }

                            preCode = dbCode;
                            preCustomerCode = customerCode;
                            //save data in new sheet
                        }


                        customerShortName = db[dalSPP.ShortName].ToString();
                        dt_Row = dt_Excel.NewRow();

                        dt_Row[header_PONoString] = db[dalSPP.PONo];
                        dt_Row[header_PODate] = Convert.ToDateTime(db[dalSPP.PODate]).Date;
                        dt_Row[header_Size_1] = db[dalSPP.SizeNumerator];
                        dt_Row[header_Unit_1] = db[dalSPP.SizeUnit];
                        dt_Row[header_Type] = db[dalSPP.TypeName];
                        dt_Row[header_ItemCode] = db[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = db[dalSPP.DeliveredQty];
                        dt_Row[header_OrderQty] = db[dalSPP.POQty];
                        dt_Row[header_Note] = db[dalSPP.PONote];

                        dt_Excel.Rows.Add(dt_Row);


                    }
                }
            }

            if (dt_Excel.Rows.Count > 0)
            {
                dgvItemList.DataSource = null;
                dgvItemList.DataSource = dt_Excel;
                dgvItemList.ClearSelection();

                MoveUniqueDataToSheet(g_Workbook, customerShortName);

                dgvItemList.DataSource = null;
                dt_Excel = NewExcelTable();
            }


            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void MoveUniqueDataToSheet(Workbook g_Workbook, string CustomerName)
        {
            DataGridView dgv = dgvItemList;

            #region move data to sheet

            Worksheet xlWorkSheet = null;

            int count = g_Workbook.Worksheets.Count;

            xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

            xlWorkSheet.Name = CustomerName;

            xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&10 SPP PO REPORT: " + CustomerName;
            xlWorkSheet.PageSetup.RightHeader = "&\"Courier New\"&8 PG -&P";
            xlWorkSheet.PageSetup.CenterFooter = "&\"Courier New\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

            xlWorkSheet.PageSetup.CenterHorizontally = true;
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;

            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



            // Paste clipboard results to worksheet range
            copyDGVtoClipboard(dgvItemList);

            xlWorkSheet.Select();
            Range CR = (Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            #region old format setting

            Range tRange = xlWorkSheet.UsedRange;
            tRange.Font.Size = 11;
            tRange.RowHeight = 30;
            tRange.VerticalAlignment = XlHAlign.xlHAlignCenter;
            tRange.Font.Name = "Courier New";

            tRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            tRange.Borders.Weight = XlBorderWeight.xlThin;

            Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
            //Range FirstRow = xlWorkSheet.get_Range("a1:i1").Cells;
            FirstRow.WrapText = true;
            FirstRow.Font.Size = 8;

            FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
            FirstRow.RowHeight = 20;
            FirstRow.Interior.Color = Color.WhiteSmoke;
            FirstRow.Borders.LineStyle = XlLineStyle.xlContinuous;
            FirstRow.Borders.Weight = XlBorderWeight.xlThin;

            tRange.EntireColumn.AutoFit();


            DataTable dt = (DataTable)dgvItemList.DataSource;

            int PONOIndex = dgv.Columns[header_PONoString].Index;
            int PODateIndex = dgv.Columns[header_PODate].Index;
            int SizeIndex = dgv.Columns[header_Size_1].Index;
            int UnitIndex = dgv.Columns[header_Unit_1].Index;
            int TypeIndex = dgv.Columns[header_Type].Index;
            int CodeIndex = dgv.Columns[header_ItemCode].Index;
            int DeliveredQtyIndex = dgv.Columns[header_DeliveredQty].Index;
            int OrderQtyIndex = dgv.Columns[header_OrderQty].Index;
            int NoteIndex = dgv.Columns[header_Note].Index;

            xlWorkSheet.Cells[1, PONOIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, PODateIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, CodeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, DeliveredQtyIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, OrderQtyIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, NoteIndex + 1].ColumnWidth = 10;

            xlWorkSheet.Cells[1, PONOIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, PODateIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[1, SizeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, UnitIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, TypeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, CodeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, DeliveredQtyIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, OrderQtyIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, NoteIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;

            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                Range rangePODate = (Range)xlWorkSheet.Cells[j + 2, PODateIndex + 1];
                rangePODate.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeType = (Range)xlWorkSheet.Cells[j + 2, TypeIndex + 1];

                Range rangeDelievered = (Range)xlWorkSheet.Cells[j + 2, DeliveredQtyIndex + 1];
                rangeDelievered.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeNote = (Range)xlWorkSheet.Cells[j + 2, NoteIndex + 1];


                Color color = Color.Black;
                rangePODate.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangePODate.Borders[XlBordersIndex.xlEdgeLeft].Color = color;



                rangeType.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeType.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeDelievered.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeDelievered.Borders[XlBordersIndex.xlEdgeLeft].Color = color;


                rangeNote.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeNote.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

            }

            #endregion

            releaseObject(xlWorkSheet);
            Clipboard.Clear();

            dgvItemList.ClearSelection();
            #endregion
        }

        private bool IfCustomerDuplicated(DataTable dt)
        {
            dt.DefaultView.Sort = header_CustomerCode + " ASC";
            dt = dt.DefaultView.ToTable();

            string preCustomer = null;

            foreach (DataRow row in dt.Rows)
            {
                string customer = row[header_CustomerCode].ToString();

                if (preCustomer == null || preCustomer != customer)
                {
                    preCustomer = customer;
                }
                else if (preCustomer == customer)
                {
                    return true;
                }
            }

            return false;
        }

        private void POListMode()
        {
            dgvItemList.ReadOnly = true;

            dgvItemList.DataSource = null;

            addingDOStep1 = false;
            addingDOStep2 = false;
            POMode = true;
            dgvPOList.ClearSelection();

            DeliveredQtyEditMode = false;
            btnRefresh.Visible = true;

            dgvItemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnExcel.Visible = true;
            
            btnRefresh.Visible = true;
            btnAddNewPO.Visible = true;
            btnFilter.Visible = true;

            lblMainList.Text = text_POList;
            lblSubList.Text = text_POItemList;
            btnAddNewPO.Text = text_AddNewPO;

            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (dt != null && dt.Columns.Contains(header_Selected))
            {
                dt.Columns.Remove(header_Selected);
            }


            LoadPOList();

            dgvPOList.ClearSelection();
            ShowPOListUI();

        }

        private DataTable AddSelectedColumn(DataTable dt)
        {
            if (!dt.Columns.Contains(header_Selected))
            {
                DataColumn dc = new DataColumn(header_Selected, typeof(bool));

                dt.Columns.Add(dc);

            }

            return dt;
        }

        private void DeliveryQtyEditMode()
        {
            //DataGridView dgv = dgvItemList;

            //if (cbEditInBagUnit.Checked || cbEditInPcsUnit.Checked)
            //{
            //    string headerName = "";
            //    if (cbEditInBagUnit.Checked)
            //    {
            //        headerName = header_DeliveryBAG;
            //        dgv.Columns[header_DeliveryBAG].Visible = true;
            //        dgv.Columns[header_DeliveryPCS].Visible = false;
            //    }
            //    else if (cbEditInPcsUnit.Checked)
            //    {
            //        headerName = header_DeliveryPCS;
            //        dgv.Columns[header_DeliveryPCS].Visible = true;
            //        dgv.Columns[header_DeliveryBAG].Visible = false;
            //    }

            //    if (dgv.DataSource != null)
            //    {
            //        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //        dgv.ReadOnly = false;
            //        dgv.Columns[header_DeliveryQTY].Visible = false;

            //        DataTable dt_DOList = (DataTable)dgv.DataSource;

            //        foreach(DataRow row in dt_DOList.Rows)
            //        {
            //            string do_String = row[header_DONoString].ToString();

            //            int rowIndex = dt_DOList.Rows.IndexOf(row);

            //            if(int.TryParse(do_String, out int i))
            //            {
            //                dgv.Rows[rowIndex].Cells[headerName].Style.BackColor = Color.White;
            //                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            //            }
            //            else if (!string.IsNullOrEmpty(do_String))
            //            {
            //                dgv.Rows[rowIndex].Cells[headerName].Style.BackColor = SystemColors.Info;
            //            }

            //            //if(string.IsNullOrEmpty(do_String))
            //            //{
            //            //    dgv.Rows[rowIndex].Cells[headerName].Style.BackColor = Color.White;
            //            //}
            //        }

                    
            //    }
            //    else
            //    {
            //        MessageBox.Show("No data.");
            //        cbEditInBagUnit.Checked = false;
            //        cbEditInPcsUnit.Checked = false;
            //        dgv.Columns[header_DeliveryBAG].Visible = false;
            //        dgv.Columns[header_DeliveryPCS].Visible = false;
            //        dgv.Columns[header_DeliveryQTY].Visible = true;
            //        dgv.Columns[header_DeliveryQTY].DefaultCellStyle.BackColor = Color.White;
            //    }


            //}
            //else
            //{
            //    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    dgv.ReadOnly = true;

            //    if (dgv.DataSource != null)
            //    {
            //        dgv.Columns[header_DeliveryBAG].Visible = false;
            //        dgv.Columns[header_DeliveryPCS].Visible = false;
            //        dgv.Columns[header_DeliveryQTY].Visible = true;
            //        dgv.Columns[header_DeliveryQTY].DefaultCellStyle.BackColor = Color.White;
            //    }

            //}
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

                int currentColumn = dgv.CurrentCell.ColumnIndex;
                int currentRow = dgv.CurrentCell.RowIndex;

                if (dgv.Columns[currentColumn].Name == header_DeliveredQty && dgv.CurrentCell.Value != DBNull.Value) //Desired Column
                {
                    oldData = int.TryParse(dgv.CurrentCell.Value.ToString(), out oldData) ? oldData : 0;

                    if (e.Control is System.Windows.Forms.TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                        dgv.CurrentCell = dgv.Rows[currentRow].Cells[currentColumn];
                        dgv.BeginEdit(true);
                    }
                }
                else
                {

                    if (e.Control is System.Windows.Forms.TextBox tb)
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
            DataGridView dgv = dgvItemList;

            int currentColumn = dgv.CurrentCell.ColumnIndex;
            int currentRow = dgv.CurrentCell.RowIndex;

            int newData = int.TryParse(dgv.Rows[currentRow].Cells[currentColumn].Value.ToString(), out newData) ? newData : 0;

            //get data
            if (oldData != newData)
            {
                if (POMode)
                {
                    UpdateDeliveredQty(currentRow);
                }
               


            }
        }

        private void UpdateBalance(int currentRow)
        {
            DataGridView dgv = dgvItemList;

            int balance = int.TryParse(dgv.Rows[currentRow].Cells[header_Balance].Value.ToString(), out balance) ? balance : 0;
            int stdPacking = int.TryParse(dgv.Rows[currentRow].Cells[header_StdPacking].Value.ToString(), out stdPacking) ? stdPacking : 0;

            string type = dgv.Rows[currentRow].Cells[header_Type].Value.ToString();
            string size = dgv.Rows[currentRow].Cells[header_Size_1].Value.ToString();

          

            for (int i = currentRow; i < dgv.Rows.Count; i++)
            {
                string loopType = dgv.Rows[i].Cells[header_Type].Value.ToString();
                string loopSize = dgv.Rows[i].Cells[header_Size_1].Value.ToString();

                if (loopType == type && loopSize == size)
                {
                    int pcs = int.TryParse(dgv.Rows[i].Cells[header_DeliveryPCS].Value.ToString(), out pcs) ? pcs : 0;

                    balance -= pcs;

                    dgv.Rows[i].Cells[header_Balance].Value = balance;
                    dgv.Rows[i].Cells[header_BalanceString].Value = balance + " (" + balance / stdPacking + "bags)";

                    if(balance % stdPacking != 0)
                    {
                        dgv.Rows[i].Cells[header_BalanceString].Value = balance + " (" + balance / stdPacking + "bags + "+ balance % stdPacking+" pcs)";

                    }

                    if (balance < 0)
                    {
                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Black;
                    }

                }
                else
                {
                    break;
                }

            }
        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (adjustRow != -1 && adjustCol != -1)
            {

                int row = adjustRow;
                int col = adjustCol;
                adjustRow = -1;
                adjustCol = -1;
                dgvItemList.ClearSelection();
                dgvItemList.Rows[row].Cells[col].Selected = true;

                dgvItemList.CurrentCell = dgvItemList.Rows[row].Cells[col];
                //dgvItemList.BeginEdit(true);

            }
        }


        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (addingDOStep2 || EditMode)
            //{
            //    bool twoEmptyInaRow = false;

            //    DataGridView dgv = dgvItemList;
            //    dgv.SuspendLayout();

            //    for (int i = 0; i < dgv.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < dgv.Columns.Count; j++)
            //        {
            //            string colName = dgv.Columns[j].Name;

            //            if (dgv.Columns[j].Name == header_Balance)
            //            {
            //                if (dgv.Rows[i].Cells[j].Value == DBNull.Value)
            //                {
            //                    if (twoEmptyInaRow)
            //                    {
            //                        dgv.Rows[i].Height = 3;
            //                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Black;
            //                        dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.Black;
            //                    }
            //                    else
            //                    {
            //                        dgv.Rows[i].Height = 25;
            //                        twoEmptyInaRow = true;
            //                    }


            //                }
            //                else
            //                {
            //                    twoEmptyInaRow = false;
            //                    dgv.Rows[i].Height = 50;

            //                    int bal = int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out bal) ? bal : 0;

            //                    if (bal < 0)
            //                    {
            //                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Red;
            //                    }
            //                    else
            //                    {
            //                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Black;
            //                    }
            //                }

            //            }

            //            else if (dgv.Columns[j].Name == header_DataMode)
            //            {
            //                if (dgv.Rows[i].Cells[j].Value != DBNull.Value)
            //                {
            //                    string text = dgv.Rows[i].Cells[j].Value.ToString();
            //                    if (dgv.Rows[i].Cells[j].Value.ToString() != text_DB)
            //                    {
            //                        dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.FromArgb(253, 203, 110);
            //                    }
            //                    else
            //                    {
            //                        dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.White;
            //                    }
            //                }
            //                else
            //                {
            //                    dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.White;
            //                }

            //            }
            //        }
            //    }

            //    dgv.ResumeLayout();
            //}
        }

        private void UpdateTotalToDeliveryBag()
        {
            DataTable dt = (DataTable)dgvItemList.DataSource;
            TotalToDeliveryBag = 0;
            foreach (DataRow row in dt.Rows)
            {
                string dataMode = row[header_DataMode].ToString();

                if (dataMode == text_ToAdd || dataMode == text_ToEdit)
                {
                    int toDeliveryBag = int.TryParse(row[header_DeliveryBAG].ToString(), out toDeliveryBag) ? toDeliveryBag : 0;
                    TotalToDeliveryBag += toDeliveryBag;
                }
            }

            lblSubList.Text = text_DOItemList + " ( Total Bags: " + TotalToDeliveryBag + ")";
        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (POMode)
            {
                DataGridView dgv = dgvItemList;
                dgv.SuspendLayout();

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string deliveredQty = dgv.Rows[i].Cells[header_DeliveredQty].Value.ToString();
                    string orderQty = dgv.Rows[i].Cells[header_OrderQty].Value.ToString();

                    if (deliveredQty == orderQty)
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                dgv.ResumeLayout();
            }
        }

        private void dgvPOItem_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvItemList;


                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && POMode && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();


                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.Focus();
                    int rowIndex = dgv.CurrentCell.RowIndex;

                    if (dgv.Rows[rowIndex].Cells[header_Type].Value != DBNull.Value)
                    {
                        my_menu.Items.Add(text_FullyDelivered).Name = text_FullyDelivered;

                        if (DeliveredQtyEditMode)
                        {
                            my_menu.Items.Add(text_StopEdit).Name = text_StopEdit;
                        }
                        else
                        {
                            my_menu.Items.Add(text_EditDelivered).Name = text_EditDelivered;
                        }


                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(ItemList_ItemClicked);
                    }

                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        //undo/redo function
        private void ItemList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvItemList;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();

                    if (ClickedItem.Equals(text_FullyDelivered))
                    {
                        string orderedQty = dgv.Rows[rowIndex].Cells[header_OrderQty].Value.ToString();
                        dgv.Rows[rowIndex].Cells[header_DeliveredQty].Value = orderedQty;
                        UpdateDeliveredQty(rowIndex);
                    }
                    else if (ClickedItem.Equals(text_EditDelivered))
                    {
                        DeliveredQtyEditMode = true;
                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.ReadOnly = false;
                        dgv.Columns[header_DeliveredQty].DefaultCellStyle.BackColor = SystemColors.Info;
                        //dgv.Rows[rowIndex].Cells[header_DeliveredQty].Value = orderedQty;



                    }
                    else if (ClickedItem.Equals(text_StopEdit))
                    {
                        DeliveredQtyEditMode = false;
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.ReadOnly = true;
                        dgv.Columns[header_DeliveredQty].DefaultCellStyle.BackColor = Color.White;
                    }
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void POList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvPOList;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();
                    if (ClickedItem.Equals(text_FullyDelivered))
                    {
                        FullyDelivered();
                    }
                    else if (ClickedItem.Equals(text_UndoDelivered))
                    {
                        UndoDelivered();
                    }
                    else if (ClickedItem.Equals(text_Freeze))
                    {
                        FreezePO(true, rowIndex);
                    }
                    else if (ClickedItem.Equals(text_Unfreeze))
                    {
                        FreezePO(false, rowIndex);
                    }
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void FreezePO(bool toFreeze, int rowIndex)
        {
            DataGridView dgv = dgvPOList;

            DataTable dt = (DataTable)dgv.DataSource;

            int poCode = int.TryParse(dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString(), out poCode)? poCode : -1;

            if(poCode > 0)
            {
                uSpp.PO_code = poCode;
                uSpp.Freeze = toFreeze;
                uSpp.Updated_Date = DateTime.Now;
                uSpp.Updated_By = MainDashboard.USER_ID;

                if (!dalSPP.POFreezeUpdate(uSpp))
                {
                    MessageBox.Show("Failed to freeze/unfreeze po!");

                }
                else
                {
                    
                    dgv.Rows[rowIndex].Cells[header_Freeze].Value = toFreeze;

                    if (toFreeze)
                    {
                        //MessageBox.Show("PO freeze successfully!");
                        dgv.Rows[rowIndex].Cells[header_Customer].Style.ForeColor = Color.FromArgb(52, 160, 225);
                    }
                    else
                    {
                       // MessageBox.Show("PO unfreeze successfully!");
                        dgv.Rows[rowIndex].Cells[header_Customer].Style.ForeColor = Color.Black;
                    }
                    dgv.ClearSelection();
                }
            }
            else
            {
                MessageBox.Show("po code invalid");
            }
           
           
        }

        private void UndoDelivered()
        {
            DataGridView dgv = dgvItemList;

            DataTable dt = (DataTable)dgv.DataSource;

            int poTblCode = -1;
            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;

            bool success = true;

            foreach (DataRow row in dt.Rows)
            {
                poTblCode = int.TryParse(row[header_POTblCode].ToString(), out poTblCode) ? poTblCode : -1;

                if (poTblCode >= 0)
                {
                    row[header_DeliveredQty] = 0;
                    uSpp.Table_Code = poTblCode;
                    uSpp.Delivered_qty = 0;
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = updatedBy;

                    success = dalSPP.PODeliveredDataUpdate(uSpp);
                    if (!success)
                    {
                        MessageBox.Show("Failed to update delivered qty!");

                    }

                }
            }
            //dgvItemList.DataSource = dt;
            UpdateProgress(dt);
        }

        private void FullyDelivered()
        {
            DataGridView dgv = dgvItemList;

            DataTable dt = (DataTable)dgv.DataSource;

            int poTblCode = -1;
            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;

            bool success = true;

            foreach (DataRow row in dt.Rows)
            {
                poTblCode = int.TryParse(row[header_POTblCode].ToString(), out poTblCode) ? poTblCode : -1;

                if (poTblCode >= 0)
                {
                    int newData = int.TryParse(row[header_OrderQty].ToString(), out newData) ? newData : 0;
                    row[header_DeliveredQty] = newData;
                    uSpp.Table_Code = poTblCode;
                    uSpp.Delivered_qty = newData;
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = updatedBy;

                    success = dalSPP.PODeliveredDataUpdate(uSpp);
                    if (!success)
                    {
                        MessageBox.Show("Failed to update delivered qty!");

                    }

                }
            }
            //dgvItemList.DataSource = dt;
            UpdateProgress(dt);
        }

        private void UpdateDeliveredQty(int rowIndex)
        {
            DataGridView dgv = dgvItemList;

            int poTblCode = int.TryParse(dgv.Rows[rowIndex].Cells[header_POTblCode].Value.ToString(), out poTblCode) ? poTblCode : -1;

            if (poTblCode >= 0)
            {
                DateTime updatedDate = DateTime.Now;
                int updatedBy = MainDashboard.USER_ID;
                int newData = int.TryParse(dgv.Rows[rowIndex].Cells[header_DeliveredQty].Value.ToString(), out newData) ? newData : 0;
                uSpp.Table_Code = poTblCode;
                uSpp.Delivered_qty = newData;
                uSpp.Updated_Date = updatedDate;
                uSpp.Updated_By = updatedBy;

                if (!dalSPP.PODeliveredDataUpdate(uSpp))
                {
                    MessageBox.Show("Failed to update delivered qty!");

                }
                else
                {
                    UpdateProgress();
                }
            }

        }

        private void UpdateProgress()
        {
            if (POMode)
            {
                DataTable dt = (DataTable)dgvItemList.DataSource;

                int poCode = -1;
                int progress = 0;
                int deliveredQty = 0;
                int orderQty = 0;

                foreach (DataRow row in dt.Rows)
                {
                    poCode = int.TryParse(row[header_POCode].ToString(), out poCode) ? poCode : -1;

                    if (poCode != -1)
                    {
                        deliveredQty += int.TryParse(row[header_DeliveredQty].ToString(), out int temp) ? temp : 0;
                        orderQty += int.TryParse(row[header_OrderQty].ToString(), out temp) ? temp : 0;
                    }
                }

                progress = Convert.ToInt32((float)deliveredQty / orderQty * 100);

                dt = (DataTable)dgvPOList.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    int pocode = int.TryParse(row[header_POCode].ToString(), out pocode) ? pocode : -1;
                    if (poCode == pocode)
                    {
                        row[header_Progress] = progress + "%";
                    }
                }
            }

        }

        private void UpdateProgress(DataTable dt)
        {
            if (POMode)
            {

                int poCode = -1;
                int progress = 0;
                int deliveredQty = 0;
                int orderQty = 0;

                foreach (DataRow row in dt.Rows)
                {
                    poCode = int.TryParse(row[header_POCode].ToString(), out poCode) ? poCode : -1;

                    if (poCode != -1)
                    {
                        deliveredQty += int.TryParse(row[header_DeliveredQty].ToString(), out int temp) ? temp : 0;
                        orderQty += int.TryParse(row[header_OrderQty].ToString(), out temp) ? temp : 0;
                    }
                }

                progress = Convert.ToInt32((float)deliveredQty / orderQty * 100);

                dt = (DataTable)dgvPOList.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    int pocode = int.TryParse(row[header_POCode].ToString(), out pocode) ? pocode : -1;
                    if (poCode == pocode)
                    {
                        row[header_Progress] = progress + "%";
                    }
                }
            }

        }

        private void dgvPOList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvPOList;


                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && POMode && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();

                    dgv.Focus();

                    if (dgv.SelectedRows.Count > 0)
                    {
                        //int colIndex = dgv.CurrentCell.ColumnIndex;
                        dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        // Can leave these here - doesn't hurt
                        dgv.Rows[e.RowIndex].Selected = true;
                        
                        int rowIndex = dgv.CurrentCell.RowIndex;

                        if (dgv.Rows[rowIndex].Cells[header_PONoString].Value != DBNull.Value)
                        {
                            btnEdit.Visible = true;
                            string code = dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString();
                            ShowPOItem(code);

                            string progress = dgv.Rows[rowIndex].Cells[header_Progress].Value.ToString();

                            if (progress == text_100Percentage)
                            {
                                //my_menu.Items.Add(text_UndoDelivered).Name = text_UndoDelivered;
                            }
                            else
                            {
                                my_menu.Items.Add(text_FullyDelivered).Name = text_FullyDelivered;
                                my_menu.Items.Add(text_Freeze).Name = text_Freeze;
                                my_menu.Items.Add(text_Unfreeze).Name = text_Unfreeze;
                            }


                            my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                            my_menu.ItemClicked += new ToolStripItemClickedEventHandler(POList_ItemClicked);
                        }
                    }
                   

                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvPOList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        private void dgvPOList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (POMode)
            {
                DataGridView dgv = dgvPOList;
                int rowIndex = e.RowIndex;
                int colIndex = e.ColumnIndex;

                if (dgv.Columns[colIndex].Name == header_Customer)
                {
                    bool isFreeze = bool.TryParse(dgv.Rows[rowIndex].Cells[header_Freeze].Value.ToString(), out isFreeze) ? isFreeze : false;

                    if (isFreeze)
                    {
                        dgv.Rows[rowIndex].Cells[header_Customer].Style.ForeColor = Color.FromArgb(52, 160, 225);
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[header_Customer].Style.ForeColor = Color.Black;
                    }
                }
            }

        }

        private void frmSPPPOList_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.OUGPOFormOpen = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPOList();
            lblSubList.Text = text_POItemList;
        }
    }
}
