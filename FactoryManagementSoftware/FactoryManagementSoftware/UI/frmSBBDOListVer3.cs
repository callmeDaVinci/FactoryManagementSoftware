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
using FactoryManagementSoftware.Properties;
using System.Configuration;
using System.Collections.Generic;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBDOListVer3 : Form
    {
        public frmSBBDOListVer3()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvDOList, true);
            tool.DoubleBuffered(dgvDOItemList, true);
            DATABASE_MONTHS_AGO_NUMBER = 1;
            btnFilter.Text = text_HideFilter;
            cbInvoiceMode.Checked = false;

            LoadCustomerList();

            int userID = MainDashboard.USER_ID;
            
            if(userID == -1)
            {
                userID = 1;
            }

            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            
            if (userPermission < 4)
            {
                gbAdvanceSettings.Visible = false;

                tlpFilter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
            }

        }

        #region variable/object declare

        private int userPermission = 0;

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
        trfHistBLL uTrfHist = new trfHistBLL();
        trfHistDAL dalTrf = new trfHistDAL();

        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "Show Filter"; //SHOW FILTER...
        readonly string text_HideFilter = "Hide Filter";//HIDE FILTER
        readonly string text_CompleteDO = "Complete D/O";
        readonly string text_InCompleteDO = "D/O Incomplete";
        readonly string text_ChangeDeliveredDate = "Change Delivered Date";
        readonly string text_IssueEInvoice = "Issue E-Invoice";
        readonly string text_UndoRemove = "Undo Remove";
        readonly string text_ChangeDONumber = "Change D/O Number";
        readonly string text_SelectDO = "Select D/O";
        readonly string text_Export= "Export";
        readonly string text_CancelSelectingMode = "Cancel";
        readonly string text_RemoveDO = "Remove D/O";
        readonly string text_Select = "Select";
        readonly string text_DOItemList = "D/O ITEM LIST";
        readonly string text_MasterList = "Master List";

        readonly string header_POCode = "P/O CODE";
        readonly string header_PONo = "P/O NO";
        readonly string header_PODate = "P/O RECEIVED DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_DeliveredDate = "DELIVERED";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_ShipTo = "SHIP TO";
        readonly string header_Progress = "PROGRESS";
        readonly string header_Selected = "SELECTED";
        readonly string header_DataMode = "DATA MODE";

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE 1";
        readonly string header_Unit = "UNIT";
        readonly string header_SizeString = "SIZE";
        readonly string header_Type = "TYPE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_Item = "ITEM";
        readonly string header_OrderQty = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY";
        readonly string header_TotalDeliveryBag = "NOTE";
        readonly string header_StockCheck = "STOCK CHECK";
        readonly string header_DONo = "D/O #";
        readonly string header_Amount = "Amount (RM)";
        readonly string header_DONoString = "D/O NO";
        readonly string header_DataType = "DATA TYPE";

        readonly string header_POTblCode = "P/O TABLE CODE";
        readonly string header_DOTblCode = "D/O TABLE CODE";
        readonly string header_CombinedCode = "COMBINED CODE";
        readonly string header_Stock = "STOCK QTY";
        readonly string header_StockString = "STOCK";
        readonly string header_DeliveryPCS = "DELIVERY PCS";
        readonly string header_DeliveryBAG = "DELIVERY BAG";
        readonly string header_Remark = "REMARK";
        readonly string header_DeliveryQTY = "DELIVERY QTY";
        readonly string header_Balance = "BAL No";
        readonly string header_StdPacking = "StdPacking";
        readonly string header_BalanceString = "BAL.";
        readonly string header_TrfTableCode = "TRANSFER ID";

        readonly string header_UnitPrice = "UNIT PRICE";
        readonly string header_DISCOUNT = "DISCOUNT";
        readonly string header_TotalAmount = "TOTAL AMOUNT";

        readonly string headerIndex = "#";
        readonly string headerPlanID = "PLAN ID";
        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerItem = "PLAN FOR";

        readonly string headerCollectBag = "COLLECT BAG";
        readonly string headerDeliverBag = "DELIVER BAG";
        readonly string headerFrom = "FROM";
        readonly string headerTo = "TO";
        readonly string headerReceivedBy = "RECEIVED BY";
        readonly string headerCheck = "CHECK";
        readonly string headerQty = "QTY (KG/PIECE)";

        readonly string DataType_Removed = "REMOVED";
        readonly string DataType_InProgress = "IN PROGRESS";
        readonly string DataType_Delivered = "DELIVERED";

        readonly string Text_MultiPOCode = "MULTI";
        private DataTable DB_DO_INFO;
        private DataTable DB_TRANSFER_RECORD;
        private DataTable dt_DOItemList_1;

        private string ShowingDO = "";
        private int DATABASE_MONTHS_AGO_NUMBER = 6;

        //private bool addingDOMode = false;
        //private bool ableToNextStep = false;
        private bool DOSelectingMode = false;
        private bool FormLoaded = false;
        //private bool POMode = true;
        //private bool addingDOStep2 = false;
        //private bool combineDO = false;
        //private bool combiningDO = false;

        //private int selectedDO = 0;
        //private int oldData = 0;
        //private int adjustRow = -1;
        //private int adjustCol = -1;
        #endregion

        #region data table 

        private DataTable NewDeliverTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(headerTo, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerPlanID, typeof(int));
            dt.Columns.Add(headerItem, typeof(string));
            dt.Columns.Add(headerQty, typeof(float));
            dt.Columns.Add(headerDeliverBag, typeof(string));
            dt.Columns.Add(headerReceivedBy, typeof(string));
            dt.Columns.Add(headerCheck, typeof(bool));

            return dt;

        }

        private DataTable NewCompleteTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DOTblCode, typeof(string));
            dt.Columns.Add(header_DONo, typeof(string));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveryPCS, typeof(string));
            dt.Columns.Add(header_DeliveryBAG, typeof(string));
            dt.Columns.Add(header_PONo, typeof(string));
            dt.Columns.Add(header_POTblCode, typeof(int));
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
            dt.Columns.Add(header_ShipTo, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));

            if(DOSelectingMode)
            {
                dt.Columns.Add(header_Selected, typeof(bool));
            }
            return dt;
        }


        private DataTable NewCustomerSalesTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(string));
            dt.Columns.Add(text.Header_CustomerCode, typeof(string));
            dt.Columns.Add(text.Header_Customer, typeof(string));
            dt.Columns.Add(text.Header_DOQty, typeof(string));
            dt.Columns.Add(text.Header_TotalSales, typeof(decimal));

            return dt;
        }

        private DataTable NewProductSalesTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_Qty, typeof(string));
            dt.Columns.Add(text.Header_TotalSales, typeof(decimal));

            return dt;
        }


        private DataTable NewDOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_TrfTableCode, typeof(int));
            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));

            dt.Columns.Add(header_ItemCode, typeof(string));

            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));

            dt.Columns.Add(header_StdPacking, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            return dt;
        }

        private DataTable NewSalesReportTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DataType, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_ShipTo, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));

            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_TrfTableCode, typeof(int));
            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));

            dt.Columns.Add(header_ItemCode, typeof(string));

            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));

            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_UnitPrice, typeof(decimal));
            dt.Columns.Add(text.Header_DiscountRate, typeof(decimal));
            dt.Columns.Add(text.Header_NetAmount, typeof(decimal));

            dt.Columns.Add(header_StdPacking, typeof(int));
            
            return dt;
        }

        private DataTable NewCustomerSalesReportTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_CustomerCode, typeof(string));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_ShipTo, typeof(string));
            dt.Columns.Add(text.Header_NetAmount, typeof(decimal));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private DataTable NewProductSalesReportTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(text.Header_NetAmount, typeof(decimal));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private DataTable NewDOItemTableWithMultiPO()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_TrfTableCode, typeof(int));
            dt.Columns.Add(header_Index, typeof(int));

           

            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));

            dt.Columns.Add(header_ItemCode, typeof(string));

            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));

            dt.Columns.Add(header_StdPacking, typeof(int));

            dt.Columns.Add(header_PONo, typeof(string));
            return dt;
        }

        private DataTable NewInvoiceDetailTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_TrfTableCode, typeof(int));
            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_StdPacking, typeof(int));

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));
            dt.Columns.Add(text.Header_Unit, typeof(string));
            dt.Columns.Add(text.Header_UnitPrice, typeof(decimal));
            dt.Columns.Add(text.Header_DiscountRate, typeof(decimal));
            dt.Columns.Add(text.Header_NetAmount, typeof(decimal));


            return dt;
        }
        private DataTable NewMasterTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(headerDeliverBag, typeof(int));
            dt.Columns.Add(header_StdPacking, typeof(int));
            return dt;
        }

        #endregion

        #region UI

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dgv == dgvDOList)
            {
                dgv.Columns[header_PODate].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[header_PONo].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                if (dgv.Columns.Contains(header_Amount))
                {
                    dgv.Columns[header_Amount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                }

                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PONo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_ShipTo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_DONo].Visible = false;
                dgv.Columns[header_DataType].Visible = false;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
            }
            else if(dgv == dgvDOItemList)
            {
                
                if(!cbInvoiceMode.Checked)
                {
                    dgv.Columns[header_DeliveryPCS].Visible = false;
                }
                else
                {
                    dgv.Columns[header_ItemCode].Visible = false;
                    dgv.Columns[header_SizeString].Visible = false;
                    dgv.Columns[header_Type].Visible = false;

                }

                dgv.Columns[header_DeliveryBAG].Visible = false;

                dgv.Columns[header_StdPacking].Visible = false;
                dgv.Columns[header_POTblCode].Visible = false;
                dgv.Columns[header_DOTblCode].Visible = false;
                dgv.Columns[header_Size].Visible = false;
                dgv.Columns[header_Unit].Visible = false;
                dgv.Columns[header_TrfTableCode].Visible = false;

                //dgv.Columns[header_DeliveryQTY].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                if(dgv.Columns.Contains(header_PONo))
                {
                    dgv.Columns[header_PONo].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                }
                //dgv.Columns[header_SizeString].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
               // dgv.Columns[header_DeliveryPCS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv.Columns[header_DeliveryBAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }

        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 250f);
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

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 250f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {

            ITEM_LIST_LOADING = true;
            GRAND_TOTAL_AMOUNT = 0;

            ShowOrHideFilter();
            ResetDBDOInfoList();
            LoadDOList();

            
            ITEM_LIST_LOADING = false;

        }

        private void dgvDOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(FormLoaded)
            {
                SELECTION_CHANGE_MODE = false;
                // DataTable dt = (DataTable)dgvPOList.DataSource;
                int rowIndex = e.RowIndex;

                DATA_LOADED = false;

                LoadItemList(rowIndex);

                DATA_LOADED = true;
            }
          
        }


        private bool ITEM_LIST_LOADING = false;

        private void LoadItemList(int rowIndex)
        {
            if(ITEM_LIST_LOADING)
            {
                return;
            }

            ITEM_LIST_LOADING = true;
            DataGridView dgv = dgvDOList;

            if (rowIndex >= 0)
            {
                string dataType = dgv.Rows[rowIndex].Cells[header_DataType].Value.ToString();

                if (dataType == DataType_InProgress)
                {
                    btnCancel.Visible = true;
                }
                else if(!DOSelectingMode)
                    btnCancel.Visible = false;

                string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();

                if (ShowingDO == "" || ShowingDO != code)
                {
                    if (cbInvoiceMode.Checked)
                    {
                        ShowInvoiceDetails(code);
                    }
                    else
                    {
                        if (dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString() == Text_MultiPOCode)
                        {
                            NEWShowDOItemWithMultiPO(code);
                            ShowingDO = code;
                        }
                        else
                        {
                            NewShowDOItem(code);
                            ShowingDO = code;

                        }
                    }

                }



                if (DOSelectingMode)
                {
                    if(!SELECTION_CHANGE_MODE)
                    {
                        bool selected = bool.TryParse(dgv.Rows[rowIndex].Cells[header_Selected].Value.ToString(), out selected) ? selected : false;

                        if (!selected)
                        {
                            dgv.Rows[rowIndex].Cells[header_Selected].Value = true;

                            btnExport.Text = text_Export;
                            btnExport.Enabled = true;
                        }
                        else
                        {
                            dgv.Rows[rowIndex].Cells[header_Selected].Value = false;

                            if (!ifDOSelected())
                            {
                                btnExport.Text = text_SelectDO;
                                btnExport.Enabled = false;
                            }
                            else
                            {
                                btnExport.Text = text_Export;
                                btnExport.Enabled = true;
                            }
                        }
                    }
                   
                }
                else
                {
                    if (dataType == DataType_InProgress)
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;
                }
            }
            else
            {
                dgvDOItemList.DataSource = null;
                btnEdit.Visible = false;
                btnCancel.Visible = false;
            }

            ITEM_LIST_LOADING = false;

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ifDOSelected()
        {
            DataTable dt = (DataTable)dgvDOList.DataSource;

            if (dt.Columns.Contains(header_Selected))
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void DOSelectingUI()
        {
            dgvDOItemList.DataSource = null;

            DOSelectingMode = true;

            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnCancel.Text = text_CancelSelectingMode;
            btnCancel.Visible = true;
            lblSelectAll.Visible = true;
            DataTable dt = (DataTable)dgvDOList.DataSource;

            //To-do:
            //check dgvDOList selected row index
            
            List<int> selectedRowIndices = new List<int>();

            foreach (DataGridViewRow row in dgvDOList.SelectedRows)
            {
                selectedRowIndices.Add(row.Index);
            }

            if (!dt.Columns.Contains(header_Selected))
            {
                DataColumn dc = new DataColumn(header_Selected, typeof(bool));

                dt.Columns.Add(dc);

                int slectedrow = 0;

                foreach (int rowIndex in selectedRowIndices)
                {
                    if (rowIndex < dt.Rows.Count)
                    {
                        dt.Rows[rowIndex][header_Selected] = true;
                        slectedrow++;
                    }
                }

                if(slectedrow > 0)
                {
                    btnExport.Enabled = true;
                    btnExport.Text = text_Export;
                }
                else
                {
                    btnExport.Enabled = false;
                    btnExport.Text = text_SelectDO;
                }
                



            }
            else if (ifDOSelected())
            {
                btnExport.Enabled = true;
                btnExport.Text = text_Export;
            }
            else
            {
                btnExport.Enabled = false;
                btnExport.Text = text_SelectDO;
            }


        }
        #endregion

  

        private void LoadCustomerList()
        {
            DataTable dt = dalSPP.CustomerWithoutRemovedDataSelect();

            DataTable locationTable = dt.DefaultView.ToTable(true, dalSPP.FullName);

            locationTable.DefaultView.Sort = dalSPP.FullName + " ASC";
            locationTable = locationTable.DefaultView.ToTable();


            DataRow dr;
            dr = locationTable.NewRow();
            dr[dalSPP.FullName] = "ALL";

            locationTable.Rows.InsertAt(dr, 0);

            cmbCustomer.DataSource = locationTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;
        }

        private void frmSPPDOList_Load(object sender, EventArgs e)
        {
           

        }
        private void DBDOListFilter()
        {
            DataTable dt = DB_DO_INFO.Clone();

            DateTime dateStart = dtpRangeStart.Value;
            DateTime dateEnd = dtpRangeEnd.Value;
            DateTime dateToInspect = DateTime.MaxValue ;

            foreach (DataRow row in DB_DO_INFO.Rows)
            {
                int doCode;
                int.TryParse(row[dalSPP.DONo].ToString(), out doCode);

                bool isRemoved;
                bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved);

                bool isDelivered;
                bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered);

                bool dataMatched = true;

                #region DO TYPE

                if (!isDelivered && cbInProgress.Checked)
                {
                    //dataMatched = dataMatched && true;
                }
                else if (isDelivered && cbCompleted.Checked)
                {
                    //dataMatched = dataMatched && true;
                }
                else
                {
                    dataMatched = false;
                }

                if (isRemoved && cbRemoved.Checked)
                {
                    //dataMatched = true;
                }
                else if (isRemoved)
                {
                    dataMatched = false;
                }

                #endregion

                #region Customer Filter

                string customer = cmbCustomer.Text;

                if (string.IsNullOrEmpty(customer) || customer == "ALL")
                {
                    //dataMatched = dataMatched && true;
                }
                else if (row[dalSPP.FullName].ToString() == customer)
                {
                    //dataMatched = dataMatched && true;
                }
                else
                {
                    dataMatched = false;
                }

                #endregion

                #region Date Range Filter



                if (dataMatched)
                {

                    if(cbDeliveredDate.Checked)
                    {
                        string trfID = row[dalSPP.TrfTableCode].ToString();

                        if (isDelivered && !string.IsNullOrEmpty(trfID))
                        {
                            if(DB_DO_INFO.Columns.Contains(dalTrfHist.TrfDate))
                            {
                                dateToInspect =  Convert.ToDateTime(row[dalTrfHist.TrfDate].ToString()).Date;
                            }
                            else
                            {
                                dateToInspect = tool.GetTransferDate(trfID, DB_TRANSFER_RECORD);

                            }

                            int month = dateToInspect.Month;

                            if (month == 4)
                            {
                                int checkpoint = 1;
                            }

                            if (dateToInspect >= dateStart && dateToInspect <= dateEnd)
                            {

                            }
                            else
                            {
                                dataMatched = false;
                            }
                        }
                        else
                        {
                            dataMatched = false;

                        }
                    }
                    else if (cbPODate.Checked)
                    {
                        dateToInspect = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        if (dateToInspect >= dateStart && dateToInspect <= dateEnd)
                        {

                        }
                        else
                        {
                            dataMatched = false;
                        }
                    }
                    else if (cbDODate.Checked)
                    {
                        dateToInspect = Convert.ToDateTime(row[dalSPP.DODate]).Date;

                        if (dateToInspect >= dateStart && dateToInspect <= dateEnd)
                        {

                        }
                        else
                        {
                            dataMatched = false;
                        }
                    }
                }
                    
                #endregion

                if (dataMatched)
                {
                    int month = dateToInspect.Month;

                    if(month == 4)
                    {
                        int checkpoint = 1;
                    }
                    //to-do
                    //add row to dt
                    dt.Rows.Add(row.ItemArray);
                }
                else
                {
                    int month = dateToInspect.Month;

                    if (month == 4)
                    {
                        int checkpoint = 1;
                    }
                }

            }

            //to-do
            //DB_DO_INFO = dt;
            DB_DO_INFO = dt.Copy();
        }


        // This approach formats the DISPLAY only, not the underlying data
     
        private void FormatAllDONumbersInGrid()
        {
            DataGridView dgv = dgvDOList;
            DataTable dt = (DataTable)dgv.DataSource;

            if (dt != null && dt.Columns.Contains(header_DONoString))
            {
                foreach (DataRow row in dt.Rows)
                {
                    // Get the integer DO number from header_DONo column
                    if (row[header_DONo] != null && int.TryParse(row[header_DONo].ToString(), out int doNoInt))
                    {
                        // Format it and put in the header_DONoString column
                        row[header_DONoString] = tool.FormatDONumber(doNoInt);
                    }
                }
            }
        }

        private bool DATA_LOADED = false;
        private void LoadDOList()
        {
            lblInvoiceMode.Visible = cbInvoiceMode.Checked;
            lblMainList.Text = "D/O List";
            DATA_LOADED = false;
            btnEdit.Visible = false;
            btnCancel.Visible = false;
            dgvDOItemList.DataSource = null;
            ShowingDO = "";


            DataTable dt = NewDOTable();
            DataRow dt_row;

            int preDOCode = -999999;

            string prePONo = "";

            foreach (DataRow row in DB_DO_INFO.Rows)
            {
                int doCode = int.TryParse(row[dalSPP.DONo].ToString(), out doCode) ? doCode : -999999;

                if ((preDOCode == -999999 || preDOCode != doCode) && row[dalSPP.DONo] != DBNull.Value)
                {
                    int deliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                    if(deliveryQty > 0)
                    {
                        bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                        bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;
                        string trfID = row[dalSPP.TrfTableCode].ToString();

                        bool dataMatched = true;
                        string dataType = "";

                        #region DO TYPE

                        if (!isDelivered && cbInProgress.Checked)
                        {
                            dataMatched = dataMatched && true;
                            dataType = DataType_InProgress;
                        }
                        else if (isDelivered && cbCompleted.Checked)
                        {
                            dataMatched = dataMatched && true;
                            dataType = DataType_Delivered;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        if (isRemoved && cbRemoved.Checked)
                        {
                            dataMatched = true;
                            dataType = DataType_Removed;
                        }
                        else if (isRemoved)
                        {
                            dataMatched = false;
                        }

                        #endregion

                        #region Customer Filter

                        string customer = cmbCustomer.Text;

                        if (string.IsNullOrEmpty(customer) || customer == "ALL")
                        {
                            dataMatched = dataMatched && true;
                        }
                        else if (row[dalSPP.FullName].ToString() == customer)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        #region Date Filter

                        //if (!isDelivered && cbInProgress.Checked)
                        //{
                        //    dataMatched = dataMatched && true;
                        //    dataType = DataType_InProgress;
                        //}

                        #endregion

                        if (dataMatched)
                        {
                            prePONo = row[dalSPP.PONo].ToString();
                            preDOCode = doCode;
                            dt_row = dt.NewRow();

                            string shipTo = row[dalSPP.ShippingShortName].ToString();
                            string transporter = row[dalSPP.ShippingTransporter].ToString();

                            if(!string.IsNullOrEmpty(transporter))
                            {
                                shipTo = shipTo + " (" + transporter + ")";
                            }

                            dt_row[header_DataType] = dataType;
                            dt_row[header_DONo] = doCode;
                            dt_row[header_DONoString] = doCode.ToString("D6");
                            dt_row[header_PONo] = row[dalSPP.PONo];
                            dt_row[header_POCode] = row[dalSPP.POCode];
                            dt_row[header_PODate] = Convert.ToDateTime(row[dalSPP.PODate]).Date;
                            dt_row[header_Customer] = row[dalSPP.ShortName];
                            dt_row[header_ShipTo] = shipTo;
                            dt_row[header_CustomerCode] = row[dalSPP.CustTblCode];

                            if (isDelivered && !string.IsNullOrEmpty(trfID))
                            {
                                dt_row[header_DeliveredDate] = tool.GetTransferDate(trfID);
                            }

                            dt.Rows.Add(dt_row);
                        }
                    }
                }

                if(doCode != 999999 && preDOCode == doCode)
                {
                    string PONo = row[dalSPP.PONo].ToString();

                    if (prePONo != "" && PONo != prePONo)
                    {
                        dt.Rows[dt.Rows.Count - 1][header_PONo] = Text_MultiPOCode;

                    }
                }
            }


            dt.DefaultView.Sort = header_DONo + " DESC";
            dt = dt.DefaultView.ToTable();

            
            dgvDOList.DataSource = dt;

            FormatAllDONumbersInGrid();

            if (cbInvoiceMode.Checked)
            {
                ShowTotalInvoiceAmountInDOList();
            }

            DgvUIEdit(dgvDOList);
            dgvDOList.ClearSelection();
            //lblSubList.Text = text_DOItemList;


            Focus();

            DATA_LOADED = true;


        }

        private void ShowDOItem(string doCode)
        {
            if (FormLoaded)
            {
                frmLoading.ShowLoadingScreen();

                Cursor = Cursors.WaitCursor;
                //dgvItemList.DataSource = null;
                DataTable dt = dalSPP.DOWithInfoSelect();//570

                dt.DefaultView.Sort = dalSPP.DONo + " ASC," + dalSPP.TypeName + " ASC," + dalSPP.SizeWeight + " ASC," + dalSPP.SizeWeight + "1 ASC";
                dt = dt.DefaultView.ToTable();
               
                DataTable dt_DOItemList = NewDOItemTable();

                DataRow dt_Row;
                int index = 1;
                string preType = null;
                int totalBag = 0;

                foreach (DataRow row in dt.Rows)
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                    if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                    {
                        int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                        string type = row[dalSPP.TypeName].ToString();

                        if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                        {
                            if (preType == null)
                            {
                                preType = type;
                            }
                            else if (preType != type)
                            {
                                preType = type;
                            }

                            dt_Row = dt_DOItemList.NewRow();

                            int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                            int bag = deliveryQty / stdPacking;



                            int DOTableCode = int.TryParse(row[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                            int POTableCode = int.TryParse(row[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                            int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                            dt_Row[header_POTblCode] = POTableCode;
                            dt_Row[header_DOTblCode] = DOTableCode;

                            if (TrfTableCode != -1)
                            {
                                dt_Row[header_TrfTableCode] = TrfTableCode;
                            }


                            if (deliveryQty > 0)
                            {
                                #region Get Size Data

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

                                totalBag += bag;
                                dt_Row[header_Index] = index;
                                dt_Row[header_PONo] = row[dalSPP.PONo];
                                dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                                dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                                dt_Row[header_SizeString] = sizeString;
                                dt_Row[header_Type] = type;
                                dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                                dt_Row[header_StdPacking] = stdPacking;
                                dt_Row[header_DeliveryPCS] = deliveryQty;
                                dt_Row[header_DeliveryBAG] = bag;


                                int balancePcs = deliveryQty % stdPacking;
                                string deliveryQtyString = deliveryQty + " (" + bag + "bags)";

                                if (balancePcs != 0)
                                {
                                    deliveryQtyString = deliveryQty + " (" + bag + "bags + " + balancePcs + " pcs )";
                                }

                                dt_Row[header_DeliveryQTY] = deliveryQtyString;

                                dt_DOItemList.Rows.Add(dt_Row);
                                index++;
                            }

                        }
                    }
                }
                //216
                if(!cbInvoiceMode.Checked)
                {
                    lblSubList.Text = text_DOItemList + " (Total Bags: " + totalBag + ")";

                }
                //dt_POItemList = AddSpaceToList(dt_POItemList);
                dgvDOItemList.DataSource = dt_DOItemList;
                DgvUIEdit(dgvDOItemList);
                dgvDOItemList.ClearSelection();

                Cursor = Cursors.Arrow;

                frmLoading.CloseForm();
                Focus();

            }
        }

        private void ReloadDatabase()
        {
            //DB_DO_INFO = dalSPP.DOWithInfoSelect();

            if(cbInvoiceMode.Checked)
            {
            
                DB_DO_INFO = dalSPP.CompletedDOWithInfoSelectByMonth(DATABASE_MONTHS_AGO_NUMBER);
            }
            else
            {
                DB_DO_INFO = dalSPP.DOWithInfoSelectByMonth(DATABASE_MONTHS_AGO_NUMBER);

            }
            DB_TRANSFER_RECORD = dalTrfHist.SelectByMonth(DATABASE_MONTHS_AGO_NUMBER);
            //DB_TRANSFER_RECORD = dalTrfHist.Select();

        }

        private void ResetDBDOInfoList()
        {
            frmLoading.ShowLoadingScreen();
           
            ReloadDatabase();
            DBDOListFilter();

            DB_DO_INFO.DefaultView.Sort = dalSPP.DONo + " ASC," + dalSPP.TypeName + " ASC," + dalSPP.SizeWeight + " ASC," + dalSPP.SizeWeight + "1 ASC";
            DB_DO_INFO = DB_DO_INFO.DefaultView.ToTable();

            frmLoading.CloseForm();

        }

        private void NewShowDOItem(string doCode)
        {
            if (FormLoaded)
            {
                //frmLoading.ShowLoadingScreen();

                Cursor = Cursors.WaitCursor;
                //dgvItemList.DataSource = null;


               

                DataTable dt_DOItemList = NewDOItemTable();

                DataRow dt_Row;
                int index = 1;
                string preType = null;
                int totalBag = 0;

                foreach (DataRow row in DB_DO_INFO.Rows)
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                    if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                    {
                        int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                        string type = row[dalSPP.TypeName].ToString();

                        if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                        {
                            if (preType == null)
                            {
                                preType = type;
                            }
                            else if (preType != type)
                            {
                                preType = type;
                            }

                            dt_Row = dt_DOItemList.NewRow();

                            int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                            int bag = deliveryQty / stdPacking;



                            int DOTableCode = int.TryParse(row[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                            int POTableCode = int.TryParse(row[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                            int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                            dt_Row[header_POTblCode] = POTableCode;
                            dt_Row[header_DOTblCode] = DOTableCode;

                            if (TrfTableCode != -1)
                            {
                                dt_Row[header_TrfTableCode] = TrfTableCode;
                            }


                            if (deliveryQty > 0)
                            {
                                #region Get Size Data

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

                                totalBag += bag;
                                dt_Row[header_Index] = index;
                                dt_Row[header_PONo] = row[dalSPP.PONo];
                                dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                                dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                                dt_Row[header_SizeString] = sizeString;
                                dt_Row[header_Type] = type;
                                dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                                dt_Row[header_StdPacking] = stdPacking;
                                dt_Row[header_DeliveryPCS] = deliveryQty;
                                dt_Row[header_DeliveryBAG] = bag;


                                int balancePcs = deliveryQty % stdPacking;
                                string deliveryQtyString = deliveryQty + " (" + bag + "bags)";

                                if (balancePcs != 0)
                                {
                                    deliveryQtyString = deliveryQty + " (" + bag + "bags + " + balancePcs + " pcs )";
                                }

                                dt_Row[header_DeliveryQTY] = deliveryQtyString;

                                dt_DOItemList.Rows.Add(dt_Row);
                                index++;
                            }

                        }
                    }
                }
                //216
                if(!cbInvoiceMode.Checked)
                {
                    lblSubList.Text = text_DOItemList + " (Total Bags: " + totalBag + ")";

                }
                //dt_POItemList = AddSpaceToList(dt_POItemList);
                dgvDOItemList.DataSource = dt_DOItemList;
                DgvUIEdit(dgvDOItemList);
                dgvDOItemList.ClearSelection();

                Cursor = Cursors.Arrow;

                //frmLoading.CloseForm();
                Focus();

            }
        }

        private void ShowDOItemWithMultiPO(string doCode)
        {
            frmLoading.ShowLoadingScreen();

            Cursor = Cursors.WaitCursor;
            

            DataTable dt = dalSPP.DOWithInfoSelect();

            dt.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
            dt = dt.DefaultView.ToTable();

            DataTable dt_DOItemList = NewDOItemTableWithMultiPO();

            DataRow dt_Row;
            int index = 1;
            string preType = null;
            int totalBag = 0;

            foreach (DataRow row in dt.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                {
                    int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                    {
                        if (preType == null)
                        {
                            preType = type;
                        }
                        else if (preType != type)
                        {
                            preType = type;
                        }

                        dt_Row = dt_DOItemList.NewRow();

                        int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                        int bag = deliveryQty / stdPacking;



                        int DOTableCode = int.TryParse(row[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                        int POTableCode = int.TryParse(row[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                        int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                        dt_Row[header_POTblCode] = POTableCode;
                        dt_Row[header_DOTblCode] = DOTableCode;

                        if (TrfTableCode != -1)
                        {
                            dt_Row[header_TrfTableCode] = TrfTableCode;
                        }


                        if (deliveryQty > 0)
                        {
                            #region Get Size Data
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

                            totalBag += bag;
                            dt_Row[header_Index] = index;
                            dt_Row[header_PONo] = row[dalSPP.PONo];
                            dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                            dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                            dt_Row[header_SizeString] = sizeString;
                            dt_Row[header_Type] = type;
                            dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                            dt_Row[header_StdPacking] = stdPacking;
                            dt_Row[header_DeliveryPCS] = deliveryQty;
                            dt_Row[header_DeliveryBAG] = bag;


                            int balancePcs = deliveryQty % stdPacking;
                            string deliveryQtyString = deliveryQty + " (" + bag + "bags)";

                            if (balancePcs != 0)
                            {
                                deliveryQtyString = deliveryQty + " (" + bag + "bags + " + balancePcs + " pcs )";
                            }

                            dt_Row[header_DeliveryQTY] = deliveryQtyString;

                            dt_DOItemList.Rows.Add(dt_Row);
                            index++;
                        }

                    }
                }
            }

            if (!cbInvoiceMode.Checked)
            {
                lblSubList.Text = text_DOItemList + " (Total Bags: " + totalBag + ")";

            }
            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvDOItemList.DataSource = dt_DOItemList;
            DgvUIEdit(dgvDOItemList);
            dgvDOItemList.ClearSelection();
            Cursor = Cursors.Arrow;


            frmLoading.CloseForm();
            Focus();

        }

        private void NEWShowDOItemWithMultiPO(string doCode)
        {
            //frmLoading.ShowLoadingScreen();

            Cursor = Cursors.WaitCursor;


            DB_DO_INFO.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
            DB_DO_INFO = DB_DO_INFO.DefaultView.ToTable();
            //749MS
            DataTable dt_DOItemList = NewDOItemTableWithMultiPO();

            DataRow dt_Row;
            int index = 1;
            string preType = null;
            int totalBag = 0;

            foreach (DataRow row in DB_DO_INFO.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                {
                    int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                    {
                        if (preType == null)
                        {
                            preType = type;
                        }
                        else if (preType != type)
                        {
                            preType = type;
                        }

                        dt_Row = dt_DOItemList.NewRow();

                        int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                        int bag = deliveryQty / stdPacking;



                        int DOTableCode = int.TryParse(row[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                        int POTableCode = int.TryParse(row[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                        int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                        dt_Row[header_POTblCode] = POTableCode;
                        dt_Row[header_DOTblCode] = DOTableCode;

                        if (TrfTableCode != -1)
                        {
                            dt_Row[header_TrfTableCode] = TrfTableCode;
                        }


                        if (deliveryQty > 0)
                        {
                            #region Get Size Data
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

                            totalBag += bag;
                            dt_Row[header_Index] = index;
                            dt_Row[header_PONo] = row[dalSPP.PONo];
                            dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                            dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                            dt_Row[header_SizeString] = sizeString;
                            dt_Row[header_Type] = type;
                            dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                            dt_Row[header_StdPacking] = stdPacking;
                            dt_Row[header_DeliveryPCS] = deliveryQty;
                            dt_Row[header_DeliveryBAG] = bag;


                            int balancePcs = deliveryQty % stdPacking;
                            string deliveryQtyString = deliveryQty + " (" + bag + "bags)";

                            if (balancePcs != 0)
                            {
                                deliveryQtyString = deliveryQty + " (" + bag + "bags + " + balancePcs + " pcs )";
                            }

                            dt_Row[header_DeliveryQTY] = deliveryQtyString;

                            dt_DOItemList.Rows.Add(dt_Row);
                            index++;
                        }

                    }
                }
            }

            if (!cbInvoiceMode.Checked)
            {
                lblSubList.Text = text_DOItemList + " (Total Bags: " + totalBag + ")";

            }
            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvDOItemList.DataSource = dt_DOItemList;
            DgvUIEdit(dgvDOItemList);
            dgvDOItemList.ClearSelection();
            Cursor = Cursors.Arrow;


            //frmLoading.CloseForm();
            Focus();

        }

        private decimal GRAND_TOTAL_AMOUNT = 0;

        private void ShowTotalInvoiceAmountInDOList()
        {
            if (cbInvoiceMode.Checked)
            {
                //frmLoading.ShowLoadingScreen();

                Cursor = Cursors.WaitCursor;

                GRAND_TOTAL_AMOUNT = 0;

                //749MS
                DataTable dt = (DataTable)dgvDOList.DataSource;

                if(!dt.Columns.Contains(header_Amount))
                {
                    dt.Columns.Add(header_Amount, typeof(decimal));
                }

                foreach (DataRow doRow in dt.Rows)
                {
                    string doCode = doRow[header_DONo].ToString();

                    decimal totalAmount = 0;

                    foreach (DataRow row in DB_DO_INFO.Rows)
                    {
                        bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                        if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                        {
                            int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                            string type = row[dalSPP.TypeName].ToString();

                            if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                            {
                                decimal unitPrice = decimal.TryParse(row[dalSPP.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;

                                decimal DiscRate = decimal.TryParse(row[dalSPP.DiscountRate].ToString(), out DiscRate) ? DiscRate : 0;

                                //decimal netAmount = deliveryQty * unitPrice * (100 - DiscRate) / 100;
                                decimal netAmount = Math.Round(deliveryQty * unitPrice * (100 - DiscRate) / 100, 2);
                                totalAmount += netAmount;

                            }
                        }
                    }


                    decimal roundedTotalAmount = Math.Round(totalAmount, 2);

                    doRow[header_Amount] = roundedTotalAmount;

                    GRAND_TOTAL_AMOUNT += roundedTotalAmount;
                }


                //lblMainList.Text = "D/O List";
                //lblMainList.Text += " - Total Amount(RM): " + GRAND_TOTAL_AMOUNT;

                //lblGrandTotalAmount.Text = GRAND_TOTAL_AMOUNT.ToString();
                //lblGrandTotalAmount.Visible = true;

                Cursor = Cursors.Arrow;


            }
        }

        private void ShowInvoiceDetails(string doCode)
        {
            //frmLoading.ShowLoadingScreen();

            Cursor = Cursors.WaitCursor;

            DB_DO_INFO.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
            DB_DO_INFO = DB_DO_INFO.DefaultView.ToTable();
            //749MS
            DataTable dt_DOItemList = NewInvoiceDetailTable();

            DataRow dt_Row;
            int index = 1;
            string preType = null;
            int totalBag = 0;

            decimal totalAmount = 0;

            foreach (DataRow row in DB_DO_INFO.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                {
                    int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                    {
                        if (preType == null)
                        {
                            preType = type;
                        }
                        else if (preType != type)
                        {
                            preType = type;
                        }

                        dt_Row = dt_DOItemList.NewRow();

                        int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                        int bag = deliveryQty / stdPacking;



                        int DOTableCode = int.TryParse(row[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                        int POTableCode = int.TryParse(row[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                        int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                        dt_Row[header_POTblCode] = POTableCode;
                        dt_Row[header_DOTblCode] = DOTableCode;

                        if (TrfTableCode != -1)
                        {
                            dt_Row[header_TrfTableCode] = TrfTableCode;
                        }


                        if (deliveryQty > 0)
                        {
                            #region Get Size Data
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

                            totalBag += bag;
                            dt_Row[header_Index] = index;
                            dt_Row[header_PONo] = row[dalSPP.PONo];
                            dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                            dt_Row[header_Unit] = "PCS";
                            dt_Row[header_SizeString] = sizeString;
                            dt_Row[header_Type] = type;
                            dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                            dt_Row[header_StdPacking] = stdPacking;
                            //dt_Row[header_DeliveryPCS] = deliveryQty;
                            //dt_Row[header_DeliveryBAG] = bag;

                            dt_Row[text.Header_Description] = sizeString + " " + type;

                            dt_Row[header_DeliveryQTY] = deliveryQty;
                            dt_Row[text.Header_Unit] = "PCS";

                            decimal unitPrice = decimal.TryParse(row[dalSPP.UnitPrice].ToString(), out unitPrice)? unitPrice : 0;
                            dt_Row[text.Header_UnitPrice] = unitPrice;

                            decimal DiscRate = decimal.TryParse(row[dalSPP.DiscountRate].ToString(), out DiscRate) ? DiscRate : 0;

                            dt_Row[text.Header_DiscountRate] = DiscRate;

                            //decimal netAmount = deliveryQty * unitPrice * (100 - DiscRate) / 100;
                            decimal netAmount = Math.Round(deliveryQty * unitPrice * (100 - DiscRate) / 100, 2);
                            dt_Row[text.Header_NetAmount] = netAmount;

                            totalAmount += netAmount;

                            //dt.Columns.Add(text.Header_Description, typeof(string));


                            //int balancePcs = deliveryQty % stdPacking;
                            //string deliveryQtyString = deliveryQty + " (" + bag + "bags)";

                            //if (balancePcs != 0)
                            //{
                            //    deliveryQtyString = deliveryQty + " (" + bag + "bags + " + balancePcs + " pcs )";
                            //}


                            dt_DOItemList.Rows.Add(dt_Row);
                            index++;
                        }

                    }
                }
            }

            decimal roundedTotalAmount = Math.Round(totalAmount, 2);

            lblSubList.Text = "INVOICE TOTAL(RM): " + roundedTotalAmount;
            lblSubList.Visible = true;


            if(!cbInvoiceMode.Checked)
            {
                lblSubList.Text = text_DOItemList + " (Total Bags: " + totalBag + ")";

            }
            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvDOItemList.DataSource = dt_DOItemList;
            DgvUIEdit(dgvDOItemList);
            dgvDOItemList.ClearSelection();
            Cursor = Cursors.Arrow;


            //frmLoading.CloseForm();
            Focus();

        }

        private int TOTAL_DO_SELECTED_COUNT = 0;
        private decimal TotalAmountRowSelected()
        {
            decimal totalAmount = 0;
            TOTAL_DO_SELECTED_COUNT = 0;

            DataTable dt = (DataTable)dgvDOList.DataSource;

            if (dt.Columns.Contains(header_Amount))
            {
                foreach (DataGridViewRow dgvRow in dgvDOList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        TOTAL_DO_SELECTED_COUNT++;
                        DataRow row = ((DataRowView)dgvRow.DataBoundItem).Row;
                        totalAmount += decimal.TryParse(row[header_Amount].ToString(), out decimal x) ? x : 0;
                    }
                }
            }

            return totalAmount;
        }


        private bool SELECTION_CHANGE_MODE = false;
        private void dgvDOList_SelectionChanged(object sender, EventArgs e)
        {
            if (DATA_LOADED && FormLoaded && dgvDOList.SelectedRows.Count > 0)
            {
                SELECTION_CHANGE_MODE = true;
                int rowIndex = dgvDOList.SelectedRows[0].Index;
                LoadItemList(rowIndex);

                if(cbInvoiceMode.Checked)
                {
                    lblMainList.Text = "D/O List";

                    decimal totalAmount = TotalAmountRowSelected();

                    if(totalAmount > 0)
                        lblMainList.Text += " - ("+ TOTAL_DO_SELECTED_COUNT+ " D/O Selected) Total Amount(RM): " + totalAmount;
                }
            }

        }

        private void RemoveDO(int DONo, string customer)
        {
            uSpp.IsRemoved = true;
            uSpp.DO_no = DONo;

            int userID = MainDashboard.USER_ID;
            DateTime dateNow = DateTime.Now;
            uSpp.Updated_Date = dateNow;
            uSpp.Updated_By = userID;

            if (dalSPP.DORemove(uSpp))
            {
                MessageBox.Show("DO Removed!");
                tool.historyRecord(text.DO_Removed, text.GetDONumberAndCustomer(DONo, customer), dateNow, userID, dalSPP.DOTableName, DONo);
            }
            else
            {
                MessageBox.Show("Unable to delete DO!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvDOList;

            int rowIndex = dgv.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                DataTable dt = NewDOTable();

                DataRow dt_Row = dt.NewRow();

                dt_Row[header_DONo] = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString());
                dt_Row[header_DONoString] = dgv.Rows[rowIndex].Cells[header_DONoString].Value.ToString();
                dt_Row[header_PONo] = dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString();
                dt_Row[header_POCode] = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString());
                dt_Row[header_PODate] = dgv.Rows[rowIndex].Cells[header_PODate].Value.ToString();
                dt_Row[header_Customer] = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
                dt_Row[header_CustomerCode] = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString());

                dt.Rows.Add(dt_Row);

                frmSBBPOList frm = new frmSBBPOList(dt, true)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                //4664
                frm.ShowDialog();

                ResetDBDOInfoList();
                LoadDOList();
            }

        }

        private void DOListUI()
        {
            DataGridView dgv = dgvDOList;
            DataTable dt = (DataTable)dgv.DataSource;

            if (DOSelectingMode)
            {
                if (dt != null && dt.Columns.Contains(header_Selected))
                {
                    dt.Columns.Remove(header_Selected);
                }

                dgvDOItemList.DataSource = null;

                DOSelectingMode = true;

                btnCancel.Visible = false;// btnAddNewDO.Visible = true;

                btnEdit.Visible = false;
                btnCancel.Text = text_RemoveDO;
                btnCancel.Visible = false;
                btnExport.Enabled = true;
                btnExport.Text = text_Select;
                lblSelectAll.Visible = false;

                dgvDOList.ClearSelection();

                DOSelectingMode = false;
            }
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvDOList;

            if(DOSelectingMode)
            {
                DOListUI();
                lblMainList.Text = "D/O List";
            }
            else
            {
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    int DONo = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString());
                    string Customer = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();

                    //DialogResult dialogResult = MessageBox.Show("Are you sure you want to REMOVE DO: " + DONo.ToString("D6") + " ?", "Message",
                                                              //MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    string formattedDO = tool.FormatDONumber(DONo);
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to REMOVE DO: " + formattedDO + " ?", "Message",
                                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        RemoveDO(DONo, Customer);
                        ResetDBDOInfoList();
                        LoadDOList();
                    }
                }
            }
          
        }

        private void btnAddNewDO_Click(object sender, EventArgs e)
        {
            //frmSBBPOList frm = new frmSBBPOList(true)
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};


            //frm.ShowDialog();
            //ResetDBDOInfoList();
            //LoadDOList();
        }

        private void dgvDOList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(FormLoaded)
            {
                try
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    DataGridView dgv = dgvDOList;
                    //dgvItemList.DataSource = null;

                    //handle the row selection on right click
                    if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                    {
                        ContextMenuStrip my_menu = new ContextMenuStrip();


                        dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        // Can leave these here - doesn't hurt
                        dgv.Rows[e.RowIndex].Selected = true;
                        dgv.Focus();
                        int rowIndex = dgv.CurrentCell.RowIndex;

                        string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();

                        if (ShowingDO == "" || ShowingDO != code)
                        {
                            if (dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString() == Text_MultiPOCode)
                            {
                                NEWShowDOItemWithMultiPO(code);
                                ShowingDO = code;
                            }
                            else
                            {
                                NewShowDOItem(code);
                                ShowingDO = code;

                            }
                        }

                        //if (dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString() == Text_MultiPOCode)
                        //{
                        //    ShowDOItemWithMultiPO(code);
                        //}
                        //else
                        //{
                        //    ShowDOItem(code);
                        //}
                        //ShowDOItem(code);

                        string dataType = dgv.Rows[rowIndex].Cells[header_DataType].Value.ToString();

                        string showingText = "";

                        bool ableToChangeDONo = false;
                        if (dataType == DataType_Removed)
                        {
                            showingText = text_UndoRemove;
                        }
                        else if (dataType == DataType_Delivered)
                        {
                            my_menu.Items.Add(text_ChangeDeliveredDate).Name = text_ChangeDeliveredDate;
                            showingText = text_InCompleteDO;
                            ableToChangeDONo = true;
                        }
                        else if (dataType == DataType_InProgress)
                        {
                            showingText = text_CompleteDO;
                            ableToChangeDONo = true;
                        }

                        my_menu.Items.Add(showingText).Name = showingText;

                        if (ableToChangeDONo)
                        {
                            my_menu.Items.Add(text_ChangeDONumber).Name = text_ChangeDONumber;
                        }

                        my_menu.Items.Add(text_MasterList).Name = text_MasterList;

                        if(MainDashboard.USER_ACCESS_LEVEL >= 4)
                        {
                            my_menu.Items.Add(text_IssueEInvoice).Name = text_IssueEInvoice;
                        }


                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(DOList_ItemClicked);

                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
            }//1
           
        }

        private void GetMasterList()
        {
            DataGridView dgv = dgvDOList;

            DataTable dt_DO = dalSPP.DOWithInfoSelect();

            dt_DO.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
            dt_DO = dt_DO.DefaultView.ToTable();

            DataTable dt_DOItemList = NewMasterTable();

            DataRow dt_Row;
            int index = 1;
            string preType = null;
            int totalBag = 0;

            //DataTable dt = new DataTable();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if(row.Selected)
                {
                    string doCode = row.Cells[header_DONo].Value.ToString();
                    int doNo = int.TryParse(doCode, out doNo) ? doNo : -1;

                    foreach (DataRow row2 in dt_DO.Rows)
                    {
                        bool isRemoved = bool.TryParse(row2[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                        if (doCode == row2[dalSPP.DONo].ToString() && row2[dalSPP.DONo] != DBNull.Value && !isRemoved)
                        {
                            int deliveryQty = row2[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row2[dalSPP.ToDeliveryQty].ToString());

                            string type = row2[dalSPP.TypeName].ToString();

                            if (!string.IsNullOrEmpty(type) && (deliveryQty > 0))
                            {
                                if (preType == null)
                                {
                                    preType = type;
                                }
                                else if (preType != type)
                                {
                                    preType = type;
                                    dt_Row = dt_DOItemList.NewRow();
                                    dt_DOItemList.Rows.Add(dt_Row);
                                }

                                dt_Row = dt_DOItemList.NewRow();

                                int stdPacking = int.TryParse(row2[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                                int bag = deliveryQty / stdPacking;

                                int DOTableCode = int.TryParse(row2[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                                int POTableCode = int.TryParse(row2[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                                int TrfTableCode = int.TryParse(row2[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                                dt_Row[header_DOTblCode] = DOTableCode;
                                dt_Row[header_DONo] = doNo;
                                //dt_Row[header_DONoString] = doNo.ToString("D6");
                                dt_Row[header_DONoString] = tool.FormatDONumber(doNo);


                                if (deliveryQty > 0)
                                {
                                    totalBag += bag;
                                    dt_Row[header_Index] = index;
                                    dt_Row[header_Size] = row2[dalSPP.SizeNumerator];
                                    dt_Row[header_Unit] = row2[dalSPP.SizeUnit].ToString().ToUpper();
                                    dt_Row[header_Type] = type;
                                    dt_Row[header_ItemCode] = row2[dalSPP.ItemCode];
                                    dt_Row[header_StdPacking] = stdPacking;
                                    dt_Row[header_DeliveryPCS] = deliveryQty;
                                    dt_Row[headerDeliverBag] = bag;
                                    dt_DOItemList.Rows.Add(dt_Row);
                                    index++;
                                }

                            }
                        }
                    }
                }
            }

            dt_DOItemList = FilterMasterList(dt_DOItemList);

            if (dt_DOItemList.Rows.Count > 0)
            {

                frmMasterList frm = new frmMasterList(dt_DOItemList)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();

            }
           
        }

        private DataTable FilterMasterList(DataTable dt_MasterList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Item, typeof(string));
            dt.Columns.Add(header_DeliveryQTY, typeof(int));
            dt.Columns.Add(header_Remark, typeof(string));

            dt_MasterList.DefaultView.Sort = header_Unit + " ASC,"  + header_Size + " ASC," + header_Type + " ASC";
            dt_MasterList = dt_MasterList.DefaultView.ToTable();

            string preType = "", preUnit = "", preSize = "";

            int totalBag = 0, totalPCS = 0, subTotalPcs = 0, subTotalBag = 0, subTotalBalancePcs = 0;

            DataRow newRow = null;

            foreach(DataRow row in dt_MasterList.Rows)
            {
                string type = row[header_Type].ToString();
                string unit = row[header_Unit].ToString();
                string size = row[header_Size].ToString();

                int deliveryPcs = int.TryParse(row[header_DeliveryPCS].ToString(), out deliveryPcs) ? deliveryPcs : 0;
                int stdPacking = int.TryParse(row[header_StdPacking].ToString(), out stdPacking) ? stdPacking : -1;
                int balancePcs = 0;

                int deliveryBag = deliveryPcs / stdPacking;

                if(deliveryBag < 0)
                {
                    deliveryBag = 0;
                }

                if(deliveryPcs % stdPacking != 0)
                {
                    balancePcs = deliveryPcs - deliveryBag * stdPacking;
                }

                bool sameItemWithPrevious = type == preType && unit == preUnit && size == preSize;

                if(sameItemWithPrevious)
                {
                    totalBag += deliveryBag;
                    subTotalPcs += deliveryPcs;
                    subTotalBag += deliveryBag;
                    subTotalBalancePcs += balancePcs;
                    totalPCS += balancePcs;
                    if (newRow != null)
                    {
                        newRow[header_DeliveryQTY] = subTotalPcs;

                        string deliveryNote = subTotalBag + " BAG(S)";

                        if(subTotalBalancePcs > 0)
                        {
                            deliveryNote += " + " + subTotalBalancePcs+ " PCS";
                        }

                        newRow[header_Remark] = deliveryNote;
                    }
                }
                else
                {
                    preType = type;
                    preUnit = unit;
                    preSize = size;

                    totalBag += deliveryBag;
                    subTotalPcs = deliveryPcs;
                    subTotalBag = deliveryBag;
                    subTotalBalancePcs = balancePcs;
                    totalPCS += balancePcs;
                    if (newRow != null)
                    {
                        dt.Rows.Add(newRow);
                    }

                    newRow = dt.NewRow();

                    string item = size + unit + " " + type;
                    newRow[header_Item] = item;
                    newRow[header_DeliveryQTY] = subTotalPcs;

                    string deliveryNote = subTotalBag + " BAG(S)";

                    if (subTotalBalancePcs > 0)
                    {
                        deliveryNote += " + " + subTotalBalancePcs + " PCS";
                    }

                    newRow[header_Remark] = deliveryNote;
                }

            }

            if (newRow != null)
            {
                dt.Rows.Add(newRow);
            }

            if(dt.Rows.Count > 0)
            {
                dt.Rows.Add(dt.NewRow());
            }

            newRow = dt.NewRow();

            string deliveryTotNote = "TOT. "+totalBag + " BAG(S)";

            if (totalPCS > 0)
            {
                deliveryTotNote += " + " + totalPCS + " PCS";
            }


            newRow[header_Remark] = deliveryTotNote;
            dt.Rows.Add(newRow);

            return dt;


        }

        private DataTable DT_SALES_REPORT;
        private DataTable DT_CUSTOMER_SALES_REPORT;
        private DataTable DT_PRODUCT_SALES_REPORT;

        private void GenerateSalesMasterList()
        {
            #region export

            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                DT_SALES_REPORT = NewSalesReportTable();
                DT_CUSTOMER_SALES_REPORT = NewCustomerSalesReportTable();

                DataRow salesRow;
                DataRow customerSalesRow;
                DataRow productSalesRow;

                dt_DOList.DefaultView.Sort = header_DONo + " ASC";
                dt_DOList = dt_DOList.DefaultView.ToTable();

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                //DataTable dt_DOInfo = DB_DO_INFO.Copy();
                //dt_DOInfo.DefaultView.Sort = dalSPP.PONo + " ASC";
                //dt_DOInfo = dt_DOInfo.DefaultView.ToTable();

                foreach (DataRow rowDOList in dt_DOList.Rows)
                {
                    bool selected = bool.TryParse(rowDOList[header_Selected].ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        #region getting basic info

                        string deliveredDate = rowDOList[header_DeliveredDate].ToString();

                        DateTime DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate;
                        string DODate_String = DODate.ToString("dd/MM/yyyy");

                        string customerName = rowDOList[header_Customer].ToString();
                        string customerCode = rowDOList[header_CustomerCode].ToString();

                        string POTableCode = rowDOList[header_POCode].ToString();
                        string PONo = rowDOList[header_PONo].ToString();
                        string DONo = rowDOList[header_DONo].ToString();
                        string DONoString = rowDOList[header_DONoString].ToString();

                        #endregion

                        #region Excel Settings

                        string billingName = null;
                        string billingAddress_1 = null;
                        string billingAddress_2 = null;
                        string billingAddress_3 = null;
                        string billingPostalCode = null;
                        string billingCity = null;
                        string billingState = null;
                        string billingContact = null;

                        string shippingName = null;
                        string shippingAddress_1 = null;
                        string shippingAddress_2 = null;
                        string shippingAddress_3 = null;
                        string shippingPostalCode = null;
                        string shippingCity = null;
                        string shippingState = null;
                        string shippingContact = null;

                        int indexNo = 1;
                        string previousPO = "";

                        decimal totalAmount = 0;

                        #endregion
                        //^^2585ms->540ms->242ms
                        foreach (DataRow rowDBInfo in DB_DO_INFO.Rows)
                        {
                            if (DONo == rowDBInfo[dalSPP.DONo].ToString() && rowDBInfo[dalSPP.DONo] != DBNull.Value)
                            {
                                int deliveryQty = rowDBInfo[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(rowDBInfo[dalSPP.ToDeliveryQty].ToString());

                                if (deliveryQty > 0)
                                {
                                    #region Billing/Shipping Address

                                    bool useBillingAddress = bool.TryParse(rowDBInfo[dalSPP.DefaultShippingAddress].ToString(), out useBillingAddress) ? useBillingAddress : false;

                                    billingName = rowDBInfo[dalSPP.FullName].ToString();
                                    billingAddress_1 = rowDBInfo[dalSPP.Address1 + "1"].ToString();
                                    billingAddress_2 = rowDBInfo[dalSPP.Address2 + "1"].ToString();
                                    billingAddress_3 = rowDBInfo[dalSPP.Address3 + "1"].ToString();

                                    billingPostalCode = rowDBInfo[dalSPP.AddressPostalCode + "1"].ToString();
                                    billingCity = rowDBInfo[dalSPP.AddressCity + "1"].ToString();
                                    billingState = rowDBInfo[dalSPP.AddressState + "1"].ToString();
                                    billingContact = rowDBInfo[dalSPP.Phone1 + "1"].ToString();


                                    if (useBillingAddress)
                                    {
                                        

                                        shippingName = billingName;
                                        shippingAddress_1 = billingAddress_1;
                                        shippingAddress_2 = billingAddress_2;
                                        shippingAddress_3 = billingAddress_3;

                                        shippingPostalCode = billingPostalCode;
                                        shippingCity = billingCity;
                                        shippingState = billingState;
                                        shippingContact = billingContact;
                                    }
                                    else
                                    {
                                        shippingName = rowDBInfo[dalSPP.ShippingFullName].ToString();


                                        shippingAddress_1 = rowDBInfo[dalSPP.Address1].ToString();
                                        shippingAddress_2 = rowDBInfo[dalSPP.Address2].ToString();
                                        shippingAddress_3 = rowDBInfo[dalSPP.Address3].ToString();

                                        shippingPostalCode = rowDBInfo[dalSPP.AddressPostalCode].ToString();
                                        shippingCity = rowDBInfo[dalSPP.AddressCity].ToString();
                                        shippingState = rowDBInfo[dalSPP.AddressState].ToString();
                                        shippingContact = rowDBInfo[dalSPP.Phone1].ToString();
                                    }
                                    #endregion

                                    #region Getting item Info/Size

                                    string type = rowDBInfo[dalSPP.TypeName].ToString();

                                    string size = rowDBInfo[dalSPP.SizeNumerator].ToString();
                                    string itemCode = rowDBInfo[dalSPP.ItemCode].ToString();

                                    int numerator = int.TryParse(rowDBInfo[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                                    int denominator = int.TryParse(rowDBInfo[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                                    string sizeUnit = rowDBInfo[dalSPP.SizeUnit].ToString().ToUpper();

                                    if (sizeUnit.ToUpper() == "IN")
                                    {
                                        sizeUnit = "\"";
                                    }

                                    int numerator_2 = int.TryParse(rowDBInfo[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                                    int denominator_2 = int.TryParse(rowDBInfo[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                                    string sizeUnit_2 = rowDBInfo[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                                    if (sizeUnit_2.ToUpper() == "IN")
                                    {
                                        sizeUnit_2 = "\"";
                                    }

                                    string sizeString_WithUnit = "";
                                    string sizeString_1_WithUnit = "";
                                    string sizeString_2_WithUnit = "";

                                    string sizeString_1 = "";
                                    string sizeString_2 = "";

                                    int size_1 = 1;
                                    int size_2 = 1;

                                    if (denominator == 1)
                                    {
                                        size_1 = numerator;
                                        sizeString_1_WithUnit = numerator + " " + sizeUnit.ToUpper();
                                        sizeString_1 = numerator.ToString();

                                    }
                                    else
                                    {
                                        size_1 = numerator / denominator;
                                        sizeString_1_WithUnit = numerator + "/" + denominator + " " + sizeUnit.ToUpper();
                                        sizeString_1 = numerator + "/" + denominator;
                                    }

                                    if (numerator_2 > 0)
                                    {
                                        if (denominator_2 == 1)
                                        {
                                            sizeString_2_WithUnit += numerator_2 + " " + sizeUnit_2.ToUpper();
                                            sizeString_2 += " " + numerator_2.ToString();
                                            size_2 = numerator_2;

                                        }
                                        else
                                        {
                                            size_2 = numerator_2 / denominator_2;

                                            if (numerator_2 == 3 && denominator_2 == 2)
                                            {
                                                sizeString_2_WithUnit += "1 1" + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                sizeString_2 += " " + "1 1" + "/" + denominator_2;
                                            }
                                            else
                                            {
                                                sizeString_2_WithUnit += numerator_2 + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                sizeString_2 += " " + numerator_2 + "/" + denominator_2;
                                            }



                                        }
                                    }

                                    if (size_1 >= size_2)
                                    {
                                        if (sizeString_2_WithUnit != "")
                                        {
                                            sizeString_WithUnit = sizeString_1_WithUnit + " x " + sizeString_2_WithUnit;
                                        }


                                        else
                                        {
                                            sizeString_WithUnit = sizeString_1_WithUnit;
                                        }
                                    }
                                    else
                                    {

                                        if (sizeString_2_WithUnit != "")
                                            sizeString_WithUnit = sizeString_2_WithUnit + " x " + sizeString_1_WithUnit;

                                        else
                                        {
                                            sizeString_WithUnit = sizeString_2_WithUnit;
                                        }
                                    }

                                    #endregion

                                    #region Checking PO No

                                    string currentPONo = rowDBInfo[dalSPP.PONo].ToString();


                                    if (previousPO != currentPONo)
                                    {
                                        if (previousPO != "")
                                        {
                                            //rowNo++;
                                        }

                                        previousPO = currentPONo;
                                    }

                                    #endregion

                                    #region Data Insert

                                    //product decription
                                    string type_ShortName = GetTypeShortName(type);
                                    string newItemCode = text.SBB_BrandName + type_ShortName + " " + sizeString_1 + sizeString_2;
                                    string ProductDescription = "(" + newItemCode + ") " + sizeString_WithUnit + " " + type;
                                    //RowToInsert = DescriptionColStart + (itemRowOffset + rowNo).ToString() + DescriptionColEnd + (itemRowOffset + rowNo).ToString();
                                    //InsertToSheet(xlWorkSheet, RowToInsert, ProductDescription);

                                    ////delivery qty
                                    //RowToInsert = qtyColStart + (itemRowOffset + rowNo).ToString() + qtyColEnd + (itemRowOffset + rowNo).ToString();
                                    //InsertToSheet(xlWorkSheet, RowToInsert, deliveryQty);

                                    ////unit
                                    //RowToInsert = uomColStart + (itemRowOffset + rowNo).ToString() + uomColEnd + (itemRowOffset + rowNo).ToString();
                                    //InsertToSheet(xlWorkSheet, RowToInsert, "PCS");

                                    //unit price
                                    decimal unitPrice = decimal.TryParse(rowDBInfo[dalSPP.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                   // RowToInsert = "q" + (itemRowOffset + rowNo).ToString() + ":r" + (itemRowOffset + rowNo).ToString();
                                    //InsertToSheet(xlWorkSheet, RowToInsert, unitPrice.ToString("N3"));

                                    //discount rate
                                    decimal DiscRate = decimal.TryParse(rowDBInfo[dalSPP.DiscountRate].ToString(), out DiscRate) ? DiscRate : 0;
                                    //RowToInsert = "s" + (itemRowOffset + rowNo).ToString() + ":t" + (itemRowOffset + rowNo).ToString();
                                    //InsertToSheet(xlWorkSheet, RowToInsert, DiscRate.ToString("N3"));

                                    //net amount
                                    //decimal netAmount = deliveryQty * unitPrice * (100 - DiscRate) / 100;
                                    decimal netAmount = Math.Round(deliveryQty * unitPrice * (100 - DiscRate) / 100, 2);

                                    //RowToInsert = "u" + (itemRowOffset + rowNo).ToString() + ":w" + (itemRowOffset + rowNo).ToString();
                                    //InsertToSheet(xlWorkSheet, RowToInsert, netAmount.ToString("N3"));

                                    //total amount
                                    totalAmount += netAmount;
                                    //InsertToSheet(xlWorkSheet, areaTotalAmountText, ConvertAmountToWords(totalAmount));
                                    //InsertToSheet(xlWorkSheet, areaTotalAmount, totalAmount.ToString("N2"));

                                    //salesRow = DT_SALES_REPORT.NewRow();
                                    //salesRow[header_PONo] = currentPONo;
                                    //salesRow[header_ShipTo] = shippingName;
                                    //salesRow[header_DeliveredDate] = deliveredDate;
                                    //salesRow[header_Customer] = customerName;
                                    //salesRow[header_DONoString] = DONoString;
                                    //salesRow[header_ItemCode] = itemCode;
                                    //salesRow[header_Type] = type_ShortName;
                                    //salesRow[header_DeliveryPCS] = deliveryQty;
                                    //salesRow[header_DeliveryQTY] = deliveryQty;
                                    //salesRow[text.Header_Description] = ProductDescription;
                                    //salesRow[text.Header_UnitPrice] = unitPrice;
                                    //salesRow[text.Header_DiscountRate] = DiscRate;
                                    //salesRow[text.Header_NetAmount] = netAmount;

                                    //CustomerSalesReportUpdate(salesRow);

                                    #endregion
                                }
                            }
                        }

                        ////total amount
                        //totalAmount += netAmount;
                        //InsertToSheet(xlWorkSheet, areaTotalAmountText, ConvertAmountToWords(totalAmount));
                        //InsertToSheet(xlWorkSheet, areaTotalAmount, totalAmount.ToString("N2"));
                        totalAmount = Math.Round(totalAmount, 2);

                        customerSalesRow = DT_CUSTOMER_SALES_REPORT.NewRow();

                        customerSalesRow[header_CustomerCode] = customerCode;
                        customerSalesRow[header_Customer] = customerName;
                        //customerSalesRow[header_ShipTo] = shippingName;
                        customerSalesRow[text.Header_NetAmount] = totalAmount;
                        customerSalesRow[text.Header_Remark] = "INV"+DONoString;
                        
                        CustomerSalesReportUpdate(customerSalesRow);
                    }
                }
            }
        }
            
        private void CustomerSalesReportUpdate(DataRow newRow)
        {
            if(DT_CUSTOMER_SALES_REPORT == null)
            {
                DT_CUSTOMER_SALES_REPORT = NewCustomerSalesReportTable();
            }

            foreach(DataRow row in DT_CUSTOMER_SALES_REPORT.Rows)
            {
                string oldCustomerCode = row[header_CustomerCode].ToString();
                string newCustomerCode = newRow[header_CustomerCode].ToString();

                if (oldCustomerCode == newCustomerCode)
                {
                    decimal amount = decimal.TryParse(row[text.Header_NetAmount].ToString(), out amount) ? amount : 0;
                    decimal newAmountToAdd = decimal.TryParse(newRow[text.Header_NetAmount].ToString(), out newAmountToAdd) ? newAmountToAdd : 0;

                    string oldRemark = row[text.Header_Remark].ToString();
                    string newRemark = newRow[text.Header_Remark].ToString();

                    if(!string.IsNullOrEmpty(oldRemark))
                    {
                        newRemark = newRemark + ";" + oldRemark;
                    }
                    row[text.Header_NetAmount] = amount + newAmountToAdd;
                    row[text.Header_Remark] = amount + newRemark;

                    return;
                }
            }

            DT_CUSTOMER_SALES_REPORT.Rows.Add(newRow);
           
        }

        private void CustomerSalesReportRanking()
        {
            if (DT_CUSTOMER_SALES_REPORT != null)
            {
                DT_CUSTOMER_SALES_REPORT.DefaultView.Sort = text.Header_NetAmount + " DESC";
                DT_CUSTOMER_SALES_REPORT = DT_CUSTOMER_SALES_REPORT.DefaultView.ToTable();

                int index = 1;
                foreach(DataRow row in DT_CUSTOMER_SALES_REPORT.Rows)
                {
                    row[text.Header_Index] = index++;
                }
            }
        }
                
        private void DOList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvDOList;

            int rowIndex = dgv.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                string ClickedItem = e.ClickedItem.Name.ToString();
                
                if (ClickedItem.Equals(text_CompleteDO))
                {
                    CompleteDO(rowIndex);
                }
                else if (ClickedItem.Equals(text_ChangeDeliveredDate))
                {
                    DOChangeDeliveredDate(rowIndex);
                }
                else if (ClickedItem.Equals(text_InCompleteDO))
                {
                    IncompleteDO(rowIndex);
                    //MessageBox.Show("incomplete do");
                }
                else if (ClickedItem.Equals(text_UndoRemove))
                {
                    DOUndoRemove(rowIndex);
                    //MessageBox.Show("undo remove");
                }
                else if (ClickedItem.Equals(text_ChangeDONumber))
                {
                    ChangeDONumber(rowIndex);
                    //MessageBox.Show("Change D/O Number");
                }
                else if (ClickedItem.Equals(text_MasterList))
                {
                    GetMasterList();
                }
                else if (ClickedItem.Equals(text_IssueEInvoice))
                {
                    GenerateInvoice();
                }
            }



            Cursor = Cursors.Arrow; // change cursor to normal type

            //try
            //{
            //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //    DataGridView dgv = dgvDOList;

            //    int rowIndex = dgv.CurrentCell.RowIndex;

            //    if (rowIndex >= 0)
            //    {
            //        string ClickedItem = e.ClickedItem.Name.ToString();

            //        if (ClickedItem.Equals(text_CompleteDO))
            //        {
            //            CompleteDO(rowIndex);
            //        }
            //        else if (ClickedItem.Equals(text_InCompleteDO))
            //        {
            //            IncompleteDO(rowIndex);
            //            //MessageBox.Show("incomplete do");
            //        }
            //        else if (ClickedItem.Equals(text_UndoRemove))
            //        {
            //            DOUndoRemove(rowIndex);
            //            //MessageBox.Show("undo remove");
            //        }
            //        else if (ClickedItem.Equals(text_ChangeDONumber))
            //        {
            //            ChangeDONumber(rowIndex);
            //            //MessageBox.Show("Change D/O Number");
            //        }
            //        else if (ClickedItem.Equals(text_MasterList))
            //        {
            //            GetMasterList();
            //        }
            //    }



            //    Cursor = Cursors.Arrow; // change cursor to normal type
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
        }

        private void DOChangeDeliveredDate(int rowIndex)
        {

            bool dateChanged = true;

            DataTable dt_Item = (DataTable)dgvDOItemList.DataSource;
            DataGridView dgv = dgvDOList;
            
            
            //get delivered date
            DateTime deliveredDate = DateTime.TryParse(dgv.Rows[rowIndex].Cells[header_DeliveredDate].Value.ToString(), out deliveredDate)? deliveredDate : DateTime.MaxValue;

            if(deliveredDate != DateTime.MaxValue)
            {
                //search trf hist by delivered date
                DataTable dt_TrfHist = dalTrfHist.rangeTrfSearchWithItemInfo(deliveredDate.ToString("yyyy/MM/dd"), deliveredDate.ToString("yyyy/MM/dd"));

                bool itemList_InspectionPass = false;

                //self checking, make sure item list not empty, each item got transfer table code
                foreach (DataRow row in dt_Item.Rows)
                {
                    string TrfTableCode = row[header_TrfTableCode].ToString();

                    foreach (DataRow rowTrf in dt_TrfHist.Rows)
                    {
                        string trfID = rowTrf[dalTrfHist.TrfID].ToString();

                        if (trfID.Equals(TrfTableCode))
                        {
                            itemList_InspectionPass = true;
                            break;
                        }
                        else
                        {
                            itemList_InspectionPass = false;

                        }
                    }
                }

                if(itemList_InspectionPass)
                {
                    frmDeliveryDate frm = new frmDeliveryDate(deliveredDate)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    frm.ShowDialog();

                    if (frmDeliveryDate.selectedDate != DateTimePicker.MinimumDateTime && frmDeliveryDate.selectedDate != DateTime.MaxValue)
                    {
                        uTrfHist.trf_hist_updated_by = MainDashboard.USER_ID;
                        uTrfHist.trf_hist_updated_date = DateTime.Now;
                        uTrfHist.trf_hist_trf_date = frmDeliveryDate.selectedDate;

                        foreach (DataRow row in dt_Item.Rows)
                        {
                            int TrfTableCode = int.TryParse(row[header_TrfTableCode].ToString(), out TrfTableCode)? TrfTableCode : -1;

                            if(TrfTableCode != -1)
                            {
                                //update delivered date
                                uTrfHist.trf_hist_id = TrfTableCode;

                                if (!dalTrf.DeliveredDateUpdate(uTrfHist))
                                {
                                    MessageBox.Show("Failed to change deliverd date!\nTable Code: " + TrfTableCode.ToString());
                                    dateChanged = false;

                                }

                            }
                            else
                            {
                                MessageBox.Show("Transfer ID invalid!");
                                dateChanged = false;

                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Trf Table Code not found!\nPlease contact Jun.");
                    dateChanged = false;

                }


            }
            else
            {
                MessageBox.Show("Invalid delivered date!");
                dateChanged = false;
            }
           
            if(dateChanged)
            {
                dgv.Rows[rowIndex].Cells[header_DeliveredDate].Value = frmDeliveryDate.selectedDate.Date;
            }
        }

        private void CompleteDO(int rowIndex)
        {
            DataTable dt_Item = (DataTable)dgvDOItemList.DataSource;
            DataGridView dgv = dgvDOList;


            string doCode = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string doCodeString = dgv.Rows[rowIndex].Cells[header_DONoString].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            DataTable dt = NewCompleteTable();
            DataRow dt_Row;


            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string itemCode = row[header_ItemCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();
                string PONo = row[header_PONo].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    dt_Row = dt.NewRow();

                    dt_Row[header_DOTblCode] = doTableCode;
                    dt_Row[header_DONoString] = tool.FormatDONumber(Convert.ToInt32(doCode));
                    dt_Row[header_DONo] = doCode;
                    dt_Row[header_Customer] = customerShortName;
                    dt_Row[header_CustomerCode] = customerCode;

                    dt_Row[header_ItemCode] = itemCode;
                    dt_Row[header_DeliveryPCS] = PcsQty;
                    dt_Row[header_DeliveryBAG] = BagQty;

                    dt_Row[header_PONo] = PONo;
                    dt_Row[header_POTblCode] = row[header_POTblCode].ToString();
                    dt.Rows.Add(dt_Row);
                }


            }

            frmDeliveryDate frm = new frmDeliveryDate(dt)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();//Item Edit

            if (frmDeliveryDate.transferred)
            {
                MessageBox.Show("Transfer success!");

                ClearToDeliveryQtyAfterDelivered(dt);
                ResetDBDOInfoList();
                LoadDOList();

                tool.historyRecord(text.DO_Delivered, text.GetDONumberAndCustomer(Convert.ToInt32(doCode), customerShortName), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(doCode));
            }
            else
            {
                MessageBox.Show("Transfer cancelled!");
            }
        }

        private bool ClearToDeliveryQtyAfterDelivered(DataTable dt)
        {
            bool dataUpdated = true;

            uSpp.Updated_By = MainDashboard.USER_ID;
            uSpp.Updated_Date = DateTime.Now;

            foreach (DataRow row in dt.Rows)
            {
                uSpp.Table_Code = int.TryParse(row[header_POTblCode].ToString(), out int i) ? i : 0;

                uSpp.To_delivery_qty = 0;

                dataUpdated = dalSPP.POToDeliveryDataUpdate(uSpp);

                if (!dataUpdated)
                {
                    MessageBox.Show("Failed to update to delivery qty.\nPO Table Code: " + uSpp.Table_Code + "\nTo Delivery Qty: " + uSpp.To_delivery_qty);
                }
            }

           
            return dataUpdated;
        }

        private bool UpdateDOandPO(DataTable dt_DO, DataTable dt_PO, string trfItemCode, int trfID, string trfQty)
        {
            bool success = false;
            //DateTime updatedDate = DateTime.Now;
            //int userID = MainDashboard.USER_ID;

            //uSpp.Updated_Date = updatedDate;
            //uSpp.Updated_By = userID;

            //if (!string.IsNullOrEmpty(trfItemCode) && dt_DO.Rows.Count > 0)
            //{
            //    //GET D/O TABLE CODE BY ITEM CODE SEARCHING
            //    foreach (DataRow row in dt_DOItem.Rows)
            //    {
            //        //GET TRANSFER QTY
            //        string itemCode = row[header_ItemCode].ToString();
            //        string pcsQty = row[header_DeliveryPCS].ToString();

            //        if (trfItemCode == itemCode && pcsQty == trfQty)
            //        {
            //            string DOTableCode = row[header_DOTblCode].ToString();

            //            //update D/O
            //            uData.Table_Code = Convert.ToInt32(DOTableCode);
            //            uData.IsDelivered = true;
            //            uData.Trf_tbl_code = trfID;

            //            success = dalData.DODelivered(uData);

            //            if (!success)
            //            {
            //                MessageBox.Show("Failed to update D/O\nTransfer ID: " + trfID);
            //                return success;
            //            }

            //            //SEARCHING AT D/O DATABASE TABLE
            //            foreach (DataRow rowDODatabase in dt_DO.Rows)
            //            {
            //                string DB_DOTableCode = rowDODatabase[dalData.TableCode].ToString();

            //                if (DB_DOTableCode == DOTableCode)
            //                {
            //                    //GET P/O TABLE CODE
            //                    string POTableCode = rowDODatabase[dalData.POTableCode].ToString();

            //                    //get P/O delivered qty
            //                    foreach (DataRow rowPODatabase in dt_PO.Rows)
            //                    {
            //                        string DB_POTableCode = rowPODatabase[dalData.TableCode].ToString();

            //                        if (POTableCode == DB_POTableCode)
            //                        {
            //                            int deliveredQty = int.TryParse(rowPODatabase[dalData.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

            //                            deliveredQty += int.TryParse(trfQty, out int i) ? i : 0;

            //                            //update P/O

            //                            uData.Table_Code = Convert.ToInt32(DB_POTableCode);
            //                            uData.Delivered_qty = deliveredQty;

            //                            success = dalData.PODeliveredDataUpdate(uData);

            //                            if (!success)
            //                            {
            //                                MessageBox.Show("Failed to update P/O\nP/O Table Code: " + DB_POTableCode);

            //                            }

            //                            return success;
            //                        }
            //                    }

            //                }
            //            }

            //        }
            //    }
            //}


            return success;
        }

        private void CompleteDOWithoutTransfer(int rowIndex)
        {
            DataTable dt_Item = (DataTable)dgvDOItemList.DataSource;
            DataGridView dgv = dgvDOList;

            string doCode = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            DataTable dt_DODataBase = dalSPP.DOSelect();
            DataTable dt_PODataBase = dalSPP.POSelect();

            bool result = true;
            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string itemCode = row[header_ItemCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

               if( !UpdateDOandPO(dt_DODataBase, dt_PODataBase, itemCode,-1, PcsQty))
                {
                    result = false;
                    MessageBox.Show("Failed to transfer!");
                }
            }

            if (result)
            {
                MessageBox.Show("transfer success");
                ResetDBDOInfoList();
                LoadDOList();
                tool.historyRecord(text.DO_Delivered, text.GetDONumberAndCustomer(Convert.ToInt32(doCode), customerShortName), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(doCode));
            }
        }

        private int GetPODeliveredQty(DataTable dt_PO, string poTableCode)
        {
            int deliveredQty = 0;

            foreach(DataRow row in dt_PO.Rows)
            {
                if(poTableCode == row[dalSPP.TableCode].ToString())
                {
                    deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                }
            }

            return deliveredQty;
        }

        private void IncompleteDO(int rowIndex)
        {
            bool success = true;
            DataTable dt_Item = (DataTable)dgvDOItemList.DataSource;
            DataGridView dgv = dgvDOList;

            string doCode = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string poNoString = dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            //get po data from DB by po no string
            DataTable dt_POData;

            if (poNoString.Equals(Text_MultiPOCode))
            {
                dt_POData = dalSPP.POSelect();

            }
            else
            {
                dt_POData  = dalSPP.POSelect(poNoString);

            }

            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;

            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string poTableCode = row[header_POTblCode].ToString();
                string trfTableCode = row[header_TrfTableCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();

                string itemCode = row[header_ItemCode].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = updatedBy;

                    #region  update isDelivered to false to DB by do table code

                    uSpp.Table_Code = Convert.ToInt32(doTableCode);
                    uSpp.IsDelivered = false;
                    uSpp.Trf_tbl_code = Convert.ToInt32(trfTableCode);

                    success = dalSPP.DODelivered(uSpp);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update D/O\nTransfer ID: " + trfTableCode);
                    }
                    #endregion

                    #region update delivered qty to DB by po table code

                    //get delivered qty by po table code
                    int deliveredQty = GetPODeliveredQty(dt_POData, poTableCode);

                    //dedute delivered qty by to delivery qty
                    int toDeliveryPcs = int.TryParse(PcsQty, out toDeliveryPcs) ? toDeliveryPcs : 0;

                    deliveredQty -= toDeliveryPcs;
                    uSpp.Table_Code = Convert.ToInt32(poTableCode);
                    uSpp.Delivered_qty = deliveredQty;
              

                    success = dalSPP.PODeliveredDataUpdate(uSpp);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update delivered qty!");

                    }

                    #endregion

                    tool.UndoTransferRecord(Convert.ToInt32(trfTableCode));

                }


            }

            //message
            if (success)
            {
                //MessageBox.Show("D/O: " + Convert.ToInt32(doCode).ToString("D6") + " Incomplete Successful!");

                string formattedDO = tool.FormatDONumber(Convert.ToInt32(doCode));
                MessageBox.Show("D/O: " + formattedDO + " Incomplete Successful!");

                dgv.Rows[rowIndex].Cells[header_DataType].Value = DataType_InProgress;
            }
          

            //change row color
            ColorDONoByStatus();

            //record history
            tool.historyRecord(text.DO_Incomplete, text.GetDONumberAndCustomer(Convert.ToInt32(doCode), customerShortName), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(doCode));


            //LoadDOList();
        }

        private void DOUndoRemove(int rowIndex)
        {
            bool success = true;
            
            DataGridView dgv = dgvDOList;

            string doNo = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string poNoString = dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            DataTable dt_Item = (DataTable)dgvDOItemList.DataSource;

            string newDONo = doNo;
            if(tool.IfDONoExist(doNo))
            {
                newDONo = tool.GetNewDONo().ToString();
            }


            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;


            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string poTableCode = row[header_POTblCode].ToString();
                string trfTableCode = row[header_TrfTableCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();

                string itemCode = row[header_ItemCode].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = updatedBy;

                    #region  update isRemoved to false and DONo to DB by do table code

                    uSpp.Table_Code = Convert.ToInt32(doTableCode);
                    uSpp.IsRemoved = false;
                    uSpp.DO_no = Convert.ToInt32(newDONo);

                    success = dalSPP.DORemovedStatusAndDoNoUpdate(uSpp);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update D/O's number or isRemoved status!");
                    }
                    #endregion
                }


            }

            //message
            if(success)
            {
                //MessageBox.Show("D/O: " + Convert.ToInt32(newDONo).ToString("D6") + " remove undo successful!");

                string formattedDO = tool.FormatDONumber(Convert.ToInt32(newDONo));
                MessageBox.Show("D/O: " + formattedDO + " remove undo successful!");
                dgv.Rows[rowIndex].Cells[header_DataType].Value = DataType_InProgress;
            }

            //change row color
            ColorDONoByStatus();

            //record history
            tool.historyRecord(text.DO_UndoRemove, text.GetDONumberAndCustomer(Convert.ToInt32(newDONo), customerShortName), updatedDate, updatedBy, dalSPP.DOTableName, Convert.ToInt32(newDONo));
        }

        private void ChangeDONumber(int rowIndex)
        {
            bool success = true;
            DataTable dt_Item = (DataTable)dgvDOItemList.DataSource;
            DataGridView dgv = dgvDOList;
            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;

            uSpp.Updated_Date = updatedDate;
            uSpp.Updated_By = updatedBy;

            string oldDONo = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();

            frmDONumberChange frm = new frmDONumberChange(oldDONo)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            string newDONo = frmDONumberChange.NewDONumber;

            if(frmDONumberChange.numberSelected)
            {
                #region Update Removed D/O

                //To-Do: 
                //check if removed do have same newDONo
                if (tool.IfDONoExistInRemovedDO(newDONo))
                {
                    //if have, generate new do no for removed do
                    int newRemovedDONo = tool.GetNewRemovedDONo();

                    //change removed do old no to new do no in do table
                    uSpp.DO_no = Convert.ToInt32(newRemovedDONo);
                    success = dalSPP.DONoChange(uSpp, newDONo);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update new Removed D/O's number!");
                    }

                    //change removed do old no to new do no in history table
                    uHistory.page_name = dalSPP.DOTableName;
                    uHistory.data_id = newRemovedDONo;

                    success = dalHistory.ChangeDataID(uHistory, newDONo);

                    //removed d/o change number
                    tool.historyRecord(text.DO_ChangeDONumber, text.ChangeDONumber(Convert.ToInt32(newRemovedDONo), "", newDONo, newRemovedDONo.ToString()), updatedDate, updatedBy, dalSPP.DOTableName, Convert.ToInt32(newRemovedDONo));
                }

                #endregion

                #region change to new D/O no.

                if (!string.IsNullOrEmpty(newDONo) && newDONo != "")
                {
                    uSpp.DO_no = Convert.ToInt32(newDONo);

                    success = dalSPP.DONoChange(uSpp, oldDONo);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update new D/O's number!");
                    }
                    ResetDBDOInfoList();
                    LoadDOList();

                    //record history
                    tool.historyRecord(text.DO_ChangeDONumber, text.ChangeDONumber(Convert.ToInt32(newDONo), customerShortName, oldDONo, newDONo), updatedDate, updatedBy, dalSPP.DOTableName, Convert.ToInt32(newDONo));
                }
                else
                {
                    MessageBox.Show("Error! New DO No is null or invalid data.");
                }

                #endregion

                #region  change old data id to new data id in history record

                uHistory.page_name = dalSPP.DOTableName;
                uHistory.data_id = Convert.ToInt32(newDONo);

                success = dalHistory.ChangeDataID(uHistory, oldDONo);

                #endregion
            }
        }


        #endregion

        #region export to excel

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if(btnExport.Text == text_Export)
            {
                OpenExportSettings();
            }
            else
            {
                DATA_LOADED = false;

                DOSelectingUI();
            }

        }

        private void NewExcelPageSetup(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = 1;
            //xlWorkSheet.PageSetup.FitToPagesWide = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            //xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

            xlWorkSheet.PageSetup.PrintArea = "a1:w45";


        }

        private void OldExcelPageSetup(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;
            xlWorkSheet.PageSetup.FitToPagesWide = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            //xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

            xlWorkSheet.PageSetup.PrintArea = "a1:w46";



        }
        private void ExcelRowHeight(Worksheet xlWorkSheet, string RangeString, double rowHeight)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.RowHeight = rowHeight;
        }

        private void ExcelColumnWidth(Worksheet xlWorkSheet, string RangeString, double colWidth)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.ColumnWidth = colWidth;
        }

        private void ExcelMergeandAlign(Worksheet xlWorkSheet, string RangeString, XlHAlign hl, XlVAlign va)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.Merge();

            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignCenter;

            range.Font.Size = 9;
            range.Font.Name = "Cambria";
        }

        #region Excel marking

        XlHAlign alignLeft = XlHAlign.xlHAlignLeft;
        XlHAlign alignRight = XlHAlign.xlHAlignRight;
        XlVAlign alignVCenter = XlVAlign.xlVAlignCenter;

        Color BorderColor = Color.Black;

        int itemRowOffset = 16;
        int maxRow = 20;

        string sheetWidth = "a1:w1";
        string wholeSheetArea = "a1:w45";

        #region Letterhead: Company Info

        string rowCompanyNameCN = "a1:w1";
        string rowCompanyNameEN = "a2:w2";
        string rowSyarikatNo = "a3:w3";
        string rowCompanyAddress = "a4:w4";
        string rowSSTRegNo = "a5:w5";

        string rowDocumentVersionControl = "a5:w5";

        string areaTransporterTitle = "q14:w14";

        string areaOwnDORemark = "b38:o38";
        #endregion

        #region DO Info

        string areaDOInfo = "a6:w15";
        string rowDoInfoStart = "a6:w6";
        string areaBillTo = "a6:g6";
        string areaDeliverTo = "i6:o6";
        string areaSbbCustomerCopy = "q6:w6";

        string rowDeliveryOrder = "a7:w7";

        string areaBillName = "a7:g7";
        string areaBillLine1 = "a8:g8";
        string areaBillLine2 = "a9:g9";
        string areaBillLine3 = "a10:g10";
        string areaBillLine4 = "a11:g11";
        string areaBillLine5 = "a12:g12";
        string areaBillTel = "a13:g13";
        string areaDeliveryName = "i7:o7";
        string areaDeliveryLine1 = "i8:o8";
        string areaDeliveryLine2 = "i9:o9";
        string areaDeliveryLine3 = "i10:o10";
        string areaDeliveryLine4 = "i11:o11";
        string areaDeliveryLine5 = "i12:o12";
        string areaDeliveryTel = "i13:o13";
        string areaTotalAmountText = "a38:p38";
        string areaTotalAmount = "u38:w38";

        string areaDONo = "q8:s8";
        string areaDate = "q9:s9";
        string areaPONo = "q10:s10";
        string areaPage = "q11:s11";

        string areaDONoData = "t8:w8";
        string areaDateData = "t9:w9";
        string areaPONoData = "t10:w10";
        string areaPageData = "t11:w11";
        string areaTermData = "t14:w14";

        string areaTransportCompany1Title = "q13:s13";
        string areaTransportCompany2Title = "q14:s14";
        string areaTransporterName = "t13:w15";

        string rowDOInfoLastRow = "a15:w15";

        #endregion

        #region Item List

        string rowListTitle = "a16:w16";
        string areaItemList = "a17:w36";

        string areaIndex = "a16:a16";
        string areaPO = "b16:e16";
        string areaItem = "b16:e16";
        string areaDescription = "f16:s16";
        string areaQuantity = "t16:u16";
        string areaUOM = "v16:w16";

        string itemListColStart = "a";
        string itemListColEnd = ":w";

        string indexColStart = "a";
        string indexColEnd = ":a";

        string ItemColStart = "b";
        string ItemColEnd = ":e";

        string DescriptionColStart = "f";
        string DescriptionColEnd = ":s";

        string qtyColStart = "t";
        string qtyColEnd = ":u";

        string uomColStart = "v";
        string uomColEnd = ":w";

        string poColStart = "b";
        string poColEnd = ":e";

        string areaRemarkInDO = "a37:r37";

        string areaTotalData = "s37:w37";

        string rowItemListLastRow = "a37:w37";

        string pcsColStart = "p";
        string pcsColEnd = ":q";

        string pcsUnitColStart = "r";
        string pcsUnitColEnd = ":r";

        string remarkColStart = "t";

        string remarkColEnd = ":w";
        #endregion

        #region Sign & Chop

        string areaSafetySign = "a39:m45";
        string areaSafetyCompanyName = "a39:m39";
        string areaIssuedBy = "b45:d45";
        string areaPreparedBy = "f45:h45";
        string areaDriver = "j45:l45";
        string areaGoodsCheckedandReceivedBy = "p39:w39";
        string areaCustomerChopandSign = "p45:w45";

        #endregion


        #endregion

        private void InitialInvoiceFormat(Worksheet xlWorkSheet, bool MultiPo)
        {
            if (MultiPo)
            {
                DescriptionColStart = "e";
                areaDescription = "e17:l17";
            }
            else
            {
                DescriptionColStart = "b";
                areaDescription = "b17:l17";
            }


            #region Page Setting 

            Range DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 9;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;

            ExcelColumnWidth(xlWorkSheet, sheetWidth, 3.2);
            ExcelColumnWidth(xlWorkSheet, "r1:r1", 4.1);

            #endregion

            #region LOGO

            string tempPath = Path.GetTempFileName();
            Resources.safetyblacklogo.Save(tempPath + "safetyblacklogo.png");
            string filePath = tempPath + "safetyblacklogo.png";

            //var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo-black.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 2, 88f, 49f);

            //tempPath = Path.GetTempFileName();
            //Resources.SBB_DELIVERY_ORDER_LOGO.Save(tempPath + "sbbdeliveryorderwithrectacgle.png");
            //filePath = tempPath + "sbbdeliveryorderwithrectacgle.png";
            //xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 370, 100, 164.28f, 28.95f);


            tempPath = Path.GetTempFileName();
            Resources.iso_logo_black.Save(tempPath + "iso-logo-black.png");
            filePath = tempPath + "iso-logo-black.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 460, 4, 68f, 55f);

            #endregion

            #region LETTER HEAD

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameCN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 21.60;
            DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 22;
            DOFormat.Font.Name = text.Font_Type_KaiTi;

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameEN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 18.60;
            DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(rowSyarikatNo).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 15.60;
            DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(rowCompanyAddress).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 22.20;
            DOFormat.Value = text.Company_Semenyih_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(rowSSTRegNo).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 15.6;
            DOFormat.Value = text.Company_SST_Reg_No;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(rowDocumentVersionControl).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.SBB_Invoice_Version_Control;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(rowDocumentVersionControl).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            #endregion

            #region DO Info

            ExcelRowHeight(xlWorkSheet, areaDOInfo, 13.2);
            ExcelRowHeight(xlWorkSheet, rowDoInfoStart, 24.6);

            DOFormat = xlWorkSheet.get_Range(areaBillTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "BILL TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            ExcelMergeandAlign(xlWorkSheet, areaBillLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillTel, alignLeft, alignVCenter);

            DOFormat = xlWorkSheet.get_Range(areaDeliverTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVER TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryTel, alignLeft, alignVCenter);

            DOFormat = xlWorkSheet.get_Range("q7:w8").Cells;
            DOFormat.Merge();
            DOFormat.Value = "INVOICE";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 28;
            DOFormat.Font.Name = "Arial Black";

            ExcelRowHeight(xlWorkSheet, "q7:q7", 24.6);
            ExcelRowHeight(xlWorkSheet, "q8:q8", 34.8);

            DOFormat = xlWorkSheet.get_Range(areaBillName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDeliveryName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDate).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your P/O No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPage).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Page :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("q12:s12").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Our D/O No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("q13:s13").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Salesman :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("q14:s14").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Terms :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "00345";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDateData).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "PO01232";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t12:w12").Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t13:w13").Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            DOFormat.Value = "Y.S.Ong";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTermData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            DOFormat.Value = "";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range(areaPageData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "1    of   3";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            #endregion
            //^^807ms
            #region Item List

            int itemListFontSize = 9;


            DOFormat = xlWorkSheet.get_Range("A17:A37").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range("M17:N37").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range("O17:P37").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range("Q17:R37").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range("S17:T37").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range("U17:W38").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            if (MultiPo)
            {
                itemListFontSize = 10;

                DOFormat = xlWorkSheet.get_Range(areaPO).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;
                DOFormat.Value = "YOUR P/O NO.";

                DOFormat = xlWorkSheet.get_Range("B17:D37").Cells;
                DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

                DOFormat = xlWorkSheet.get_Range("E17:L37").Cells;
                DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            }
            else
            {
                DOFormat = xlWorkSheet.get_Range("B17:L37").Cells;
                DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
                DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;
            }


            //item list title
            DOFormat = xlWorkSheet.get_Range(rowListTitle).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat.RowHeight = 21.60;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.HorizontalAlignment = alignLeft;
            DOFormat.VerticalAlignment = alignVCenter;

            //item list content
            DOFormat = xlWorkSheet.get_Range(areaItemList).Cells;
            DOFormat.RowHeight = 18.6;
            DOFormat.Font.Size = itemListFontSize;
            DOFormat.Font.Name = "Cambria";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(areaIndex).Cells;
            DOFormat.Merge();
            DOFormat.Value = "#";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(areaDescription).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";

            DOFormat = xlWorkSheet.get_Range(areaQuantity).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Value = "QTY";

            DOFormat = xlWorkSheet.get_Range(areaUOM).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignVCenter;
            DOFormat.Value = "UOM";

            DOFormat = xlWorkSheet.get_Range("q17:r17").Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignVCenter;
            DOFormat.Value = "U.Price (RM)";

            DOFormat = xlWorkSheet.get_Range("s17:t17").Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignVCenter;
            DOFormat.Value = "DISC. (%)";

            DOFormat = xlWorkSheet.get_Range("u17:w17").Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignVCenter;
            DOFormat.Value = "Net Amount (RM)";

            for (int i = 1; i <= maxRow; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
                DOFormat.Font.Size = 9;

                area = DescriptionColStart + (itemRowOffset + i).ToString() + DescriptionColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = MultiPo? 8: 9;
                //DOFormat.Value = "25 MM EQUAL ELBOW";

                area = qtyColStart + (itemRowOffset + i).ToString() + qtyColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
                //DOFormat.Value = "1200";
                DOFormat.Font.Size = 10;

                area = uomColStart + (itemRowOffset + i).ToString() + uomColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 10;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                area = "q" + (itemRowOffset + i).ToString() + ":r" + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.NumberFormat = "@";
                DOFormat.Merge();
                DOFormat.Font.Size = 10;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;

                area = "s" + (itemRowOffset + i).ToString() + ":t" + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.NumberFormat = "@";
                DOFormat.Merge();
                DOFormat.Font.Size = 10;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;

                area = "u" + (itemRowOffset + i).ToString() + ":w" + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.NumberFormat = "@";
                DOFormat.Merge();
                DOFormat.Font.Size = 10;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;

                if (MultiPo)
                {
                    area = poColStart + (itemRowOffset + i).ToString() + poColEnd + (itemRowOffset + i).ToString();
                    DOFormat = xlWorkSheet.get_Range(area).Cells;
                    DOFormat.Merge();
                    DOFormat.Font.Size = 7;

                    //DOFormat.Value = "23232323";
                }


            }
           

            DOFormat = xlWorkSheet.get_Range(rowItemListLastRow).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;
            //DOFormat.RowHeight = 26.40;
            //DOFormat.Font.Size = 9;
            //DOFormat.Font.Name = "Cambria";

            #endregion
            //^^2111ms
            #region Sign And Chop
            ExcelRowHeight(xlWorkSheet, "a38:w38", 22.2);
            //DOFormat = xlWorkSheet.get_Range(areaSafetySign).Cells;
            //DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            //DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            //DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            //DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range("Q38:T38").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;
            DOFormat.Merge();
            DOFormat.Value = "TOTAL (RM)";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTotalAmountText).Cells;
            DOFormat.Merge();

            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.WrapText = true;


            DOFormat = xlWorkSheet.get_Range(areaTotalAmount).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";

            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;

            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("a40:a40").Cells;
            DOFormat.Value = "Notes:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range("b40:b40").Cells;
            DOFormat.NumberFormat = "@";
            DOFormat.Value = "1.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("b42:b42").Cells;
            DOFormat.NumberFormat = "@";
            DOFormat.Value = "2.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("b43:b43").Cells;
            DOFormat.NumberFormat = "@";
            DOFormat.Value = "3.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("c40:p40").Cells;
            DOFormat.Merge();
            DOFormat.Value = "All cheques should be crossed and made payable to";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("c41:p41").Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("c42:p42").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Interest at the rate of 1% per month will be charges on overdue amount.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("c43:f43").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Our bank account no.:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("g43:p43").Cells;
            DOFormat.Merge();
            DOFormat.Value = "5142 3545 3454 (Malayan Banking Berhad)";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("a45:w45").Cells;
            DOFormat.Merge();
            DOFormat.Value = "'THIS IS A COMPUTER GENERATED INVOICE. NO SIGNATURE IS REQUIRED. ";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

         

            #endregion
            //^^285ms
        }

        private void InitialInvoiceFormat_LOGO(Worksheet xlWorkSheet)
        {

            #region LETTER HEAD

            Range DOFormat = xlWorkSheet.get_Range(rowCompanyNameCN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 21.60;
            DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 22;
            DOFormat.Font.Name = text.Font_Type_KaiTi;


            #endregion

        }

        private bool IS_NON_BILLING_DO = false;

        private bool IsNonBillingDO(string doNo)
        {
            if (string.IsNullOrEmpty(doNo))
                return false;

            string upperDoNo = doNo.ToUpper();

            // Check if DO number contains "NB" indicating non-billing
            return upperDoNo.Contains("NB");
        }

        private void InitialSPSMYDOFormat(Worksheet xlWorkSheet, bool MultiPo)
        {
            if (MultiPo)
            {
                ItemColStart = "f";
                ItemColEnd = ":i";
                DescriptionColStart = "j";

                areaItem = "f16:i16";
                areaDescription = "j16:s16";

            }
            else
            {
                ItemColStart = "b";
                ItemColEnd = ":e";

                DescriptionColStart = "f";

                areaItem = "b16:e16";
                areaDescription = "f16:s16";

            }

            #region Page Setting 

            Range DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 9;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;

            ExcelColumnWidth(xlWorkSheet, sheetWidth, 3.2);

            #endregion

            #region LOGO

            string tempPath = Path.GetTempFileName();
            Resources.safetyblacklogo.Save(tempPath + "safetyblacklogo.png");
            string filePath = tempPath + "safetyblacklogo.png";

            //var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo-black.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 2, 88f, 49f);


            if(IS_NON_BILLING_DO)
            {
                tempPath = Path.GetTempFileName();
                Resources.SBB_NON_BILLING_DELIVERY_ORDER_LOGO.Save(tempPath + "sbbnonbillingdeliveryorderwithrectacgle.png");
                filePath = tempPath + "sbbnonbillingdeliveryorderwithrectacgle.png";
                xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 370, 100, 164.28f, 28.95f);
            }
            else
            {
                tempPath = Path.GetTempFileName();
                Resources.SBB_DELIVERY_ORDER_LOGO.Save(tempPath + "sbbdeliveryorderwithrectacgle.png");
                filePath = tempPath + "sbbdeliveryorderwithrectacgle.png";
                xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 370, 100, 164.28f, 28.95f);
            }

           


            tempPath = Path.GetTempFileName();
            Resources.iso_logo_black.Save(tempPath + "iso-logo-black.png");
            filePath = tempPath + "iso-logo-black.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 460, 4, 68f, 55f);

            #endregion

            #region LETTER HEAD

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameCN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 21.60;
            DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 22;
            DOFormat.Font.Name = text.Font_Type_KaiTi;

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameEN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 18.60;
            DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(rowSyarikatNo).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 15.60;
            DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(rowCompanyAddress).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 22.20;
            DOFormat.Value = text.Company_Semenyih_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
           
            DOFormat = xlWorkSheet.get_Range(rowDocumentVersionControl).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.SBB_DO_Version_Control;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(rowDocumentVersionControl).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            #endregion

            #region DO Info

            ExcelRowHeight(xlWorkSheet, areaDOInfo, 13.2);
            ExcelRowHeight(xlWorkSheet, rowDoInfoStart, 24.6);

            DOFormat = xlWorkSheet.get_Range(areaBillTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "BILL TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            ExcelMergeandAlign(xlWorkSheet, areaBillLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillTel, alignLeft, alignVCenter);

            DOFormat = xlWorkSheet.get_Range(areaDeliverTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVER TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryTel, alignLeft, alignVCenter);

            DOFormat = xlWorkSheet.get_Range(areaSbbCustomerCopy).Cells;
            DOFormat.Merge();
            DOFormat.Value = "SP COPY  /  CUSTOMER COPY";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Arial Black";

            ExcelRowHeight(xlWorkSheet, rowDeliveryOrder, 37.8);

            DOFormat = xlWorkSheet.get_Range(areaBillName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDeliveryName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDate).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your P/O No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPage).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Page :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "00345";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDateData).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "PO01232";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
       

            DOFormat = xlWorkSheet.get_Range(areaPageData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "1    of   3";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTransportCompany1Title).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "Transport :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTransportCompany2Title).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "Company  ";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTransporterName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 14;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

          

            #endregion

            #region Item List

            int itemListFontSize = 9;

            if (MultiPo)
            {
                itemListFontSize = 9;

                DOFormat = xlWorkSheet.get_Range(areaPO).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;

                DOFormat.Value = "YOUR P/O NO.";
                areaItem = "f16:h16";
                areaDescription = "j16:r16";
            }

            //item list title
            DOFormat = xlWorkSheet.get_Range(rowListTitle).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat.RowHeight = 19.80;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.HorizontalAlignment = alignLeft;
            DOFormat.VerticalAlignment = alignVCenter;

            //item list content
            DOFormat = xlWorkSheet.get_Range(areaItemList).Cells;
            DOFormat.RowHeight = 18;
            DOFormat.Font.Size = itemListFontSize;
            DOFormat.Font.Name = "Cambria";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(areaIndex).Cells;
            DOFormat.Merge();
            DOFormat.Value = "#";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(areaItem).Cells;
            DOFormat.Merge();
            DOFormat.Value = "ITEM";

            DOFormat = xlWorkSheet.get_Range(areaDescription).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";

            DOFormat = xlWorkSheet.get_Range(areaQuantity).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignRight;
            DOFormat.Value = "QUANTITY";

            DOFormat = xlWorkSheet.get_Range(areaUOM).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignVCenter;
            DOFormat.Value = "UOM";

            for (int i = 1; i <= maxRow; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
                DOFormat.Font.Size = 9;

                area = ItemColStart + (itemRowOffset + i).ToString() + ItemColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;
                //DOFormat.Value = "SBBEE 32";

                area = DescriptionColStart + (itemRowOffset + i).ToString() + DescriptionColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = itemListFontSize;

                //DOFormat.Value = "25 MM EQUAL ELBOW";

                area = qtyColStart + (itemRowOffset + i).ToString() + qtyColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                //DOFormat.Value = "1200";
                DOFormat.Font.Size = 9;

                area = uomColStart + (itemRowOffset + i).ToString() + uomColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 9;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                if (MultiPo)
                {
                    area = poColStart + (itemRowOffset + i).ToString() + poColEnd + (itemRowOffset + i).ToString();
                    DOFormat = xlWorkSheet.get_Range(area).Cells;
                    DOFormat.Merge();
                    DOFormat.Font.Size = 8;

                    //DOFormat.Value = "23232323";
                }



            }

            DOFormat = xlWorkSheet.get_Range(areaRemarkInDO).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 16;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.ShrinkToFit = true;

            //Total 
            DOFormat = xlWorkSheet.get_Range(areaTotalData).Cells;
            DOFormat.Merge();
            DOFormat.Font.Bold = true;
            DOFormat.Font.Italic = true;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(rowItemListLastRow).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;
            DOFormat.RowHeight = 26.40;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            #endregion

            #region Sign And Chop
           
            DOFormat = xlWorkSheet.get_Range(areaSafetySign).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range(areaSafetyCompanyName).Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaIssuedBy).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Issued By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range(areaPreparedBy).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Prepared By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;

            DOFormat = xlWorkSheet.get_Range(areaDriver).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Driver / Plate No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;


            DOFormat = xlWorkSheet.get_Range(areaGoodsCheckedandReceivedBy).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Goods checked and received by";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaCustomerChopandSign).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Customer's Chop & Signature";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = BorderColor;

            #endregion

        }

        private void OldInitialDOFormat(Worksheet xlWorkSheet, bool MultiPo)
        {
            if (MultiPo)
            {
                DescriptionColStart = "e";
                DescriptionColEnd = ":k";

                pcsColStart = "l";
                pcsColEnd = ":m";

                pcsUnitColStart = "n";
                pcsUnitColEnd = ":n";

                remarkColStart = "p";
                remarkColEnd = ":s";
            }
            else
            {
                DescriptionColStart = "e";
                DescriptionColEnd = ":o";

                pcsColStart = "p";
                pcsColEnd = ":q";

                pcsUnitColStart = "r";
                pcsUnitColEnd = ":r";

                remarkColStart = "t";
                remarkColEnd = ":w";

            }



            #region sheet size adjustment

            string sheetWidth = "a1:w1";
            string wholeSheetArea = "a1:w46";

            //Company Info
            string rowCompanyNameCN = "a1:w1";
            string rowCompanyNameEN = "a2:w2";
            string rowSyarikatNo = "a3:w3";
            string rowAddress1 = "a4:w4";
            string rowAddress2 = "a5:w5";
            string HeadOffice = "a4:e4";
            string SemenyihBranch = "a5:e5";
            string HeadOfficeAddress = "f4:w4";
            string SemenyihAddress = "f5:w5";
            string documentVersionControl = "a6:w6";

            //DO Info
            string areaBillTo = "a7:g7";
            string areaDeliveryTo = "i7:o7";
            string rowDeliveryOrder = "a7:w7";
            string areaDeliveryOrder = "q7:w7";
            string areaDOInfo = "a7:w14";

            string areaDONo = "q8:s8";
            string areaDate = "q9:s9";
            string areaPONo = "q10:s10";
            string areaPage = "q11:s11";



            XlHAlign alignLeft = XlHAlign.xlHAlignLeft;
            XlHAlign alignRight = XlHAlign.xlHAlignRight;
            XlVAlign alignVCenter = XlVAlign.xlVAlignCenter;

            ExcelMergeandAlign(xlWorkSheet, areaBillLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillTel, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, documentVersionControl, alignRight, alignVCenter);

            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryTel, alignLeft, alignVCenter);

            string rowDOInfoLastRow = "a16:w16";

            ExcelMergeandAlign(xlWorkSheet, areaTransporterTitle, alignLeft, alignVCenter);

            string rowListTitle = "a17:w17";
            string areaIndex = "a17:a17";
            string areaItem = "b17:d17";
            string areaDescription = "e17:o17";
            string areaQuantity = "p17:r17";
            string areaRemark = "t17:w17";
            string areaPO = "t17:w17";

            ExcelColumnWidth(xlWorkSheet, sheetWidth, 3);

            Range DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 9;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;



            #endregion

            #region LOGO

            string tempPath = Path.GetTempFileName();
            Resources.safetyblacklogo.Save(tempPath + "safetyblacklogo.png");
            string filePath = tempPath + "safetyblacklogo.png";

            //var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo-black.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 2, 88f, 49f);

            #endregion

            #region LETTER HEAD

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameCN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 21.60;
            DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 22;
            DOFormat.Font.Name = text.Font_Type_KaiTi;

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameEN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 18.60;
            DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(rowSyarikatNo).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 15.60;
            DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            ExcelRowHeight(xlWorkSheet, rowAddress1, 22.20);
            ExcelRowHeight(xlWorkSheet, rowAddress2, 27.60);

            DOFormat = xlWorkSheet.get_Range(HeadOffice).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Head Office:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(SemenyihBranch).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Semenyih Branch:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(HeadOfficeAddress).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_OUG_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(SemenyihAddress).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_Semenyih_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(documentVersionControl).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.SBB_DO_Version_Control_Old;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            Color color = Color.Black;
            DOFormat = xlWorkSheet.get_Range(documentVersionControl).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            #endregion

            #region DO Info

            ExcelRowHeight(xlWorkSheet, rowDeliveryOrder, 37.8);
            ExcelRowHeight(xlWorkSheet, areaDOInfo, 13.2);
            ExcelRowHeight(xlWorkSheet, rowDOInfoLastRow, 27);
            ExcelRowHeight(xlWorkSheet, rowDOInfoLastRow, 27);
            ExcelRowHeight(xlWorkSheet, "a42:w42", 0);

            DOFormat = xlWorkSheet.get_Range(areaBillTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "BILL TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDeliveryTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVER TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range(areaDeliveryOrder).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY ORDER";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 15;
            DOFormat.Font.Name = "Arial Black";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaBillName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDeliveryName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDate).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaPONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your P/O No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaPage).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Page :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "00345";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDateData).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "PO01232";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range(areaPageData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "1    of   3";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTransporterName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 16;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;


            DOFormat = xlWorkSheet.get_Range(areaRemarkInDO).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 16;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.ShrinkToFit = true;

            #endregion

            #region Item List

            int itemListFontSize = 9;

            if (MultiPo)
            {
                itemListFontSize = 8;

                DOFormat = xlWorkSheet.get_Range(areaPO).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;

                DOFormat.Value = "YOUR P/O NO.";


                areaDescription = "e17:k17";
                areaQuantity = "l17:n17";
                areaRemark = "p17:s17";


            }

            //item list title
            DOFormat = xlWorkSheet.get_Range(rowListTitle).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;
            DOFormat.RowHeight = 19.80;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.HorizontalAlignment = alignLeft;
            DOFormat.VerticalAlignment = alignVCenter;

            //item list content
            DOFormat = xlWorkSheet.get_Range("a18:w37").Cells;
            DOFormat.RowHeight = 18;
            DOFormat.Font.Size = itemListFontSize;
            DOFormat.Font.Name = "Cambria";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;


            DOFormat = xlWorkSheet.get_Range(areaIndex).Cells;
            DOFormat.Merge();
            DOFormat.Value = "#";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(areaItem).Cells;
            DOFormat.Merge();
            DOFormat.Value = "ITEM";

            DOFormat = xlWorkSheet.get_Range(areaDescription).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";

            DOFormat = xlWorkSheet.get_Range(areaQuantity).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignRight;
            DOFormat.Value = "QUANTITY";

            DOFormat = xlWorkSheet.get_Range(areaRemark).Cells;
            DOFormat.Merge();
            DOFormat.Value = "REMARK";

            for (int i = 1; i <= maxRow; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;


                //DOFormat.Value = i;

                area = ItemColStart + (itemRowOffset + i).ToString() + ItemColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;
                //DOFormat.Value = "SBBEE 32";

                area = DescriptionColStart + (itemRowOffset + i).ToString() + DescriptionColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "25 MM EQUAL ELBOW";

                area = pcsColStart + (itemRowOffset + i).ToString() + pcsColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                //DOFormat.Value = "1200";

                area = pcsUnitColStart + (itemRowOffset + i).ToString() + pcsUnitColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                // DOFormat.Value = "PCS";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;

                area = remarkColStart + (itemRowOffset + i).ToString() + remarkColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "3 BAGS";

                if (MultiPo)
                {
                    area = poColStart + (itemRowOffset + i).ToString() + poColEnd + (itemRowOffset + i).ToString();
                    DOFormat = xlWorkSheet.get_Range(area).Cells;
                    DOFormat.Merge();
                    //DOFormat.Value = "23232323";
                }



            }

            DOFormat = xlWorkSheet.get_Range(areaRemarkInDO).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 16;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.ShrinkToFit = true;

            //Total 
            DOFormat = xlWorkSheet.get_Range("a38:w38").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;
            //DOFormat.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;

            DOFormat.RowHeight = 26.40;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            //DOFormat.Borders.LineStyle = XlLineStyle.xlDashDot;

            DOFormat = xlWorkSheet.get_Range(areaTotalData).Cells;
            DOFormat.Merge();
            DOFormat.Font.Bold = true;
            DOFormat.Font.Italic = true;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            string ownDORemark = "b38:o38";
            DOFormat = xlWorkSheet.get_Range(ownDORemark).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaRemarkInDO).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 16;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.ShrinkToFit = true;

            #endregion

            #region Sign And Chop

            DOFormat = xlWorkSheet.get_Range("a40:j46").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a40:j40").Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("p40:w40").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Goods checked and received by";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("b46:d46").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Issued By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("g46:i46").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Lorry";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("p46:w46").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Customer's Chop & Signature";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            #endregion

            ExcelRowHeight(xlWorkSheet, rowDeliveryOrder, 37.8);

        }
        private void RowBorder(Worksheet xlWorkSheet, string area)
        {
            Range range = xlWorkSheet.get_Range(area).Cells;
            range.Borders[XlBordersIndex.xlEdgeBottom].Color = Color.Silver;
            range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, string value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
          
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, double value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
        }

        private string GetTypeShortName(string type)
        {
           
            string type_ShortName = "";

            if (type == text.Type_SprayJet)
            {
                type_ShortName = text.SprayJet_Short;
            }
            else if (type == text.Type_SprayJetSmall)
            {
                type_ShortName = text.SprayJetSmall_Short;
            }
            else if (type == text.Type_SprayJetBig)
            {
                type_ShortName = text.SprayJetBig_Short;
            }
            else if (type == text.Type_Sprinkler)
            {
                type_ShortName = text.Sprinkler_Short;
            }
            else if (type == text.Type_EqualElbow)
            {
                type_ShortName = text.EqualElbow_Short;
            }
            else if (type == text.Type_EqualSocket)
            {
                type_ShortName = text.EqualSocket_Short;
            }
            else if (type == text.Type_EqualTee)
            {
                type_ShortName = text.EqualTee_Short;
            }
            else if (type == text.Type_ReducingElbow)
            {
                type_ShortName = text.ReducingElbow_Short;
            }
            else if (type == text.Type_ReducingSocket)
            {
                type_ShortName = text.ReducingSocket_Short;
            }
            else if (type == text.Type_ReducingTee)
            {
                type_ShortName = text.ReducingTee_Short;
            }
            else if (type == text.Type_MTA)
            {
                type_ShortName = text.MTA_Short;
            }
            else if (type == text.Type_FTA)
            {
                type_ShortName = text.FTA_Short;
            }
            else if (type == text.Type_MaleElbow)
            {
                type_ShortName = text.MaleElbow_Short;
            }
            else if (type == text.Type_MaleTee)
            {
                type_ShortName = text.MaleTee_Short;
            }
            else if (type == text.Type_FemaleElbow)
            {
                type_ShortName = text.FemaleElbow_Short;
            }
            else if (type == text.Type_FemaleTee)
            {
                type_ShortName = text.FemaleTee_Short;
            }
            else if (type == text.Type_EndCap)
            {
                type_ShortName = text.EndCap_Short;
            }
            else if (type == text.Type_PolyORing)
            {
                type_ShortName = text.PolyORing_Short;
            }
            else if (type == text.Type_ClampSaddle)
            {
                type_ShortName = text.ClampSaddle_Short;
            }
            else if (type == text.Type_PolyNipple)
            {
                type_ShortName = text.PolyNipple_Short;
            }
            else if (type == text.Type_PolyReducingBush)
            {
                type_ShortName = text.PolyReducingBush_Short;
            }

            return type_ShortName;
        }

        private void ExportSBBDO()
        {

            #region Excel marking

            alignLeft = XlHAlign.xlHAlignLeft;
            alignRight = XlHAlign.xlHAlignRight;
            alignVCenter = XlVAlign.xlVAlignCenter;

            Color BorderColor = Color.Black;

             itemRowOffset = 16;
             maxRow = 20;

             sheetWidth = "a1:w1";
             wholeSheetArea = "a1:w45";

            #region Letterhead: Company Info

             rowCompanyNameCN = "a1:w1";
             rowCompanyNameEN = "a2:w2";
             rowSyarikatNo = "a3:w3";
             rowCompanyAddress = "a4:w4";

             rowDocumentVersionControl = "a5:w5";

             areaTransporterTitle = "q14:w14";

             areaOwnDORemark = "b38:o38";
            #endregion

            #region DO Info

             areaDOInfo = "a6:w15";
             rowDoInfoStart = "a6:w6";
             areaBillTo = "a6:g6";
             areaDeliverTo = "i6:o6";
             areaSbbCustomerCopy = "q6:w6";

             rowDeliveryOrder = "a7:w7";

             areaBillName = "a7:g7";
             areaBillLine1 = "a8:g8";
             areaBillLine2 = "a9:g9";
             areaBillLine3 = "a10:g10";
             areaBillLine4 = "a11:g11";
             areaBillLine5 = "a12:g12";
             areaBillTel = "a13:g13";
             areaDeliveryName = "i7:o7";
             areaDeliveryLine1 = "i8:o8";
             areaDeliveryLine2 = "i9:o9";
             areaDeliveryLine3 = "i10:o10";
             areaDeliveryLine4 = "i11:o11";
             areaDeliveryLine5 = "i12:o12";
             areaDeliveryTel = "i13:o13";

             areaDONo = "q8:s8";
             areaDate = "q9:s9";
             areaPONo = "q10:s10";
             areaPage = "q11:s11";

             areaDONoData = "t8:w8";
             areaDateData = "t9:w9";
             areaPONoData = "t10:w10";
             areaPageData = "t11:w11";

             areaTransportCompany1Title = "q13:s13";
             areaTransportCompany2Title = "q14:s14";
             areaTransporterName = "t13:w15";

             rowDOInfoLastRow = "a15:w15";

            #endregion

            #region Item List

             rowListTitle = "a16:w16";
             areaItemList = "a17:w36";

             areaIndex = "a16:a16";
             areaPO = "b16:e16";
             areaItem = "b16:e16";
             areaDescription = "f16:s16";
             areaQuantity = "t16:u16";
             areaUOM = "v16:w16";

             itemListColStart = "a";
             itemListColEnd = ":w";

             indexColStart = "a";
             indexColEnd = ":a";

             ItemColStart = "b";
             ItemColEnd = ":e";

             DescriptionColStart = "f";
             DescriptionColEnd = ":s";

             qtyColStart = "t";
             qtyColEnd = ":u";

             uomColStart = "v";
             uomColEnd = ":w";

             poColStart = "b";
             poColEnd = ":e";

             areaRemarkInDO = "a37:r37";

             areaTotalData = "s37:w37";

             rowItemListLastRow = "a37:w37";

             pcsColStart = "p";
             pcsColEnd = ":q";

             pcsUnitColStart = "r";
             pcsUnitColEnd = ":r";

             remarkColStart = "t";

             remarkColEnd = ":w";
            #endregion

            #region Sign & Chop

             areaSafetySign = "a39:m45";
             areaSafetyCompanyName = "a39:m39";
             areaIssuedBy = "b45:d45";
             areaPreparedBy = "f45:h45";
             areaDriver = "j45:l45";
             areaGoodsCheckedandReceivedBy = "p39:w39";
             areaCustomerChopandSign = "p45:w45";

            #endregion


            #endregion

            DateTime DODate = frmExportSetting.DODate;
            bool openFile = frmExportSetting.openFileAfterExport;
            bool printFile = frmExportSetting.printFileAfterExport;
            bool printPreview = frmExportSetting.printPreview;
            string DODate_String = DODate.ToString("dd/MM/yyyy");

            #region export excel

            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DataTable dt_PO = dalSPP.DOWithInfoSelect();
                dt_PO.DefaultView.Sort = dalSPP.PONo + " ASC," + dalSPP.TypeName + " ASC," + dalSPP.SizeWeight + " ASC," + dalSPP.SizeWeight + "1 ASC";
                dt_PO = dt_PO.DefaultView.ToTable();

                string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

                string path = @"D:\StockAssistant\Document\SBB DO Report";

                if (myconnstrng == text.DB_Semenyih)
                {
                    string folderName = DODate.Year.ToString() + DODate.Month.ToString("00");
                    string year = DODate.Year.ToString();

                    path = @"\\ADMIN001\Admin Server\( OFFICE )\(1.DO)\" + year + @"\" + folderName;
                }

                Directory.CreateDirectory(path);

                Cursor = Cursors.Arrow; // change cursor to normal type

                foreach (DataRow row in dt_DOList.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        string deliveredDate = row[header_DeliveredDate].ToString();

                        DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate;

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.InitialDirectory = path;

                        string customerName = row[header_Customer].ToString();
                        string POTableCode = row[header_POCode].ToString();
                        string PONo = row[header_PONo].ToString();
                        string DONo = row[header_DONo].ToString();
                        string DONoString = row[header_DONoString].ToString();

                        IS_NON_BILLING_DO = IsNonBillingDO(DONoString);

                        sfd.Filter = "Excel Documents (*.xls)|*.xls";
                        sfd.FileName = DONoString + ".xls";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            frmLoading.ShowLoadingScreen();
                            tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                            // Copy DataGridView results to clipboard
                            copyAlltoClipboard();
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                            object misValue = Missing.Value;

                            Excel.Application xlexcel = new Excel.Application
                            {
                                PrintCommunication = false,
                                ScreenUpdating = false,
                                DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                            };

                            Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);


                            xlexcel.StandardFont = "Calibri";
                            xlexcel.StandardFontSize = 12;

                            xlexcel.Calculation = XlCalculation.xlCalculationManual;
                            xlexcel.PrintCommunication = false;

                            int sheetNo = 0;
                            int pageNo = 1;
                            int rowNo = 0;

                            Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;

                            if (sheetNo > 0)
                            {
                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                            }

                            sheetNo++;


                            xlWorkSheet.Name = customerName + "_" + DONoString;

                            var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                            int printerIndex = 0;

                            foreach (String s in printers)
                            {
                                if (s.Equals("RICOH MP C3503"))
                                {
                                    break;
                                }
                                printerIndex++;
                            }

                            //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, printers[printerIndex], Type.Missing, Type.Missing, Type.Missing);

                            xlexcel.PrintCommunication = true;
                            NewExcelPageSetup(xlWorkSheet);
                            //192.168.1.111:53000
                            //xlexcel.ActivePrinter = "\\\\192.168.1.111:53000";
                            xlexcel.PrintCommunication = true;

                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            InitialSPSMYDOFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);

                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);
                            InsertToSheet(xlWorkSheet, areaDONoData, DONoString);

                            bool ownDORemark = tool.checkIfUsingOwnDOFrmDT(dt_PO, PONo);
                            string RemarkInDO = "";

                            if (PONo != Text_MultiPOCode)
                            {
                                if (ownDORemark)
                                {
                                    //InsertToSheet(xlWorkSheet, areaOwnDORemark, text.DO_CustOwnDO);
                                    InsertToSheet(xlWorkSheet, areaPONoData, "*" + PONo);
                                    RemarkInDO = RemarkInDO + (string.IsNullOrEmpty(RemarkInDO) ? "" : "; ") + text.DO_CustOwnDO;
                                }
                                else
                                {
                                    InsertToSheet(xlWorkSheet, areaPONoData, PONo);
                                }

                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaPONoData, "REFER BELOW");
                            }


                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());


                            string billingName = null;
                            string billingAddress_1 = null;
                            string billingAddress_2 = null;
                            string billingAddress_3 = null;
                            string billingPostalCode = null;
                            string billingCity = null;
                            string billingState = null;
                            string billingContact = null;

                            string shippingName = null;
                            string shippingAddress_1 = null;
                            string shippingAddress_2 = null;
                            string shippingAddress_3 = null;
                            string shippingPostalCode = null;
                            string shippingCity = null;
                            string shippingState = null;
                            string shippingContact = null;
                            string transporterName = "";
                            int totalBag = 0;
                            int totalBox= 0;
                            int totalOringBag = 0;
                            int totalSprinklerPkt = 0;
                            int totalBalPcs = 0;

                            int indexNo = 1;
                            string previousPO = "";


                            foreach (DataRow row2 in dt_PO.Rows)
                            {
                                if (DONo == row2[dalSPP.DONo].ToString() && row2[dalSPP.DONo] != DBNull.Value)
                                {
                                    int deliveryQty = row2[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row2[dalSPP.ToDeliveryQty].ToString());

                                    //string shippingTransporter = 
                                    if (!string.IsNullOrEmpty(row2[dalSPP.ShippingTransporter].ToString()))
                                    {
                                        transporterName = row2[dalSPP.ShippingTransporter].ToString();
                                    }

                                    string currentPONo = row2[dalSPP.PONo].ToString();

                                    bool useBillingAddress = bool.TryParse(row2[dalSPP.DefaultShippingAddress].ToString(), out useBillingAddress) ? useBillingAddress : false;
                                    bool useOwnDO = bool.TryParse(row2[dalSPP.CustOwnDO].ToString(), out useOwnDO) ? useOwnDO : false;

                                    ///////////     ADD BILLING ADDRESS HERE   //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                    billingName = row2[dalSPP.FullName].ToString();
                                    billingAddress_1 = row2[dalSPP.Address1 + "1"].ToString();
                                    billingAddress_2 = row2[dalSPP.Address2 + "1"].ToString();
                                    billingAddress_3 = row2[dalSPP.Address3 + "1"].ToString();

                                    billingPostalCode = row2[dalSPP.AddressPostalCode + "1"].ToString();
                                    billingCity = row2[dalSPP.AddressCity + "1"].ToString();
                                    billingState = row2[dalSPP.AddressState + "1"].ToString();
                                    billingContact = row2[dalSPP.Phone1 + "1"].ToString();
                                    RemarkInDO = row2[dalSPP.RemarkInDO].ToString();

                                    if (ownDORemark)
                                    {
                                        RemarkInDO = RemarkInDO + (string.IsNullOrEmpty(RemarkInDO) ? "" : "; ") + text.DO_CustOwnDO;
                                    }

                                    if (useBillingAddress)
                                    {
                                        shippingName = billingName;
                                        shippingAddress_1 = billingAddress_1;
                                        shippingAddress_2 = billingAddress_2;
                                        shippingAddress_3 = billingAddress_3;

                                        shippingPostalCode = billingPostalCode;
                                        shippingCity = billingCity;
                                        shippingState = billingState;
                                        shippingContact = billingContact;
                                    }
                                    else
                                    {
                                        shippingName = row2[dalSPP.ShippingFullName].ToString();
                                        shippingAddress_1 = row2[dalSPP.Address1].ToString();
                                        shippingAddress_2 = row2[dalSPP.Address2].ToString();
                                        shippingAddress_3 = row2[dalSPP.Address3].ToString();

                                        shippingPostalCode = row2[dalSPP.AddressPostalCode].ToString();
                                        shippingCity = row2[dalSPP.AddressCity].ToString();
                                        shippingState = row2[dalSPP.AddressState].ToString();
                                        shippingContact = row2[dalSPP.Phone1].ToString();
                                    }


                                    if (deliveryQty > 0)
                                    {
                                        string type = row2[dalSPP.TypeName].ToString();
                                        int stdPacking = int.TryParse(row2[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;
                                        int bag = deliveryQty / stdPacking;
                                        string withS = "S";

                                        if(bag <= 1)
                                        {
                                            withS = "";
                                        }

                                        //if(type == text.Type_PolyORing)
                                        //{
                                        //    bag = 0;
                                        //}

                                        string size = row2[dalSPP.SizeNumerator].ToString();
                                        string unit = row2[dalSPP.SizeUnit].ToString().ToUpper();
                                        string itemCode = row2[dalSPP.ItemCode].ToString();

                                        string remark = "";

                                        if (itemCode.Contains("CFPOR"))
                                        {
                                            totalOringBag += bag;

                                            remark = bag + " PACKET" + withS;

                                        }
                                        else
                                        {
                                            remark = bag + " BAG" + withS;

                                        }
                                        int balancePcs = deliveryQty % stdPacking;

                                        if (balancePcs > 0)
                                        {
                                            totalBalPcs += balancePcs;
                                            remark += " + " + balancePcs + " PCS";
                                        }

                                        if (itemCode.Contains("SP323"))
                                        {
                                            totalBalPcs -= balancePcs;

                                            if (bag > 1)
                                            {
                                                remark = bag + " BOXES";
                                            }
                                            else if (bag == 1)
                                            {
                                                remark = bag + " BOX";
                                            }

                                            totalBag -= bag;
                                            totalBox += bag;

                                            if (balancePcs > 0)
                                            {
                                                int pktQty = balancePcs / 40;

                                                totalSprinklerPkt += pktQty;

                                                string pktUnit = " PKT";

                                                if(pktQty > 1)
                                                {
                                                    pktUnit += "S";
                                                }

                                                if (remark != "")
                                                {
                                                    remark += " + " + pktQty + pktUnit;

                                                }
                                                else
                                                {
                                                    remark += pktQty + pktUnit;

                                                }

                                                if(balancePcs % 40 > 0)
                                                {
                                                    if (remark != "")
                                                    {
                                                        remark += " + " + balancePcs % 40 + " PCS";

                                                    }
                                                    else
                                                    {
                                                        remark += balancePcs % 40 + " PCS";

                                                    }

                                                    totalBalPcs += balancePcs % 40;

                                                }
                                            }

                                        }
                                      

                                        #region Getting item Size

                                        int numerator = int.TryParse(row2[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                                        int denominator = int.TryParse(row2[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                                        string sizeUnit = row2[dalSPP.SizeUnit].ToString().ToUpper();

                                        if (sizeUnit.ToUpper() == "IN")
                                        {
                                            sizeUnit = "\"";
                                        }

                                        int numerator_2 = int.TryParse(row2[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                                        int denominator_2 = int.TryParse(row2[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                                        string sizeUnit_2 = row2[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                                        if (sizeUnit_2.ToUpper() == "IN")
                                        {
                                            sizeUnit_2 = "\"";
                                        }

                                        string sizeString_WithUnit = "";
                                        string sizeString_1_WithUnit = "";
                                        string sizeString_2_WithUnit = "";

                                        string sizeString_1 = "";
                                        string sizeString_2 = "";

                                        int size_1 = 1;
                                        int size_2 = 1;

                                        if (denominator == 1)
                                        {
                                            size_1 = numerator;
                                            sizeString_1_WithUnit = numerator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator.ToString();

                                        }
                                        else
                                        {
                                            size_1 = numerator / denominator;
                                            sizeString_1_WithUnit = numerator + "/" + denominator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator + "/" + denominator;
                                        }

                                        if (numerator_2 > 0)
                                        {
                                            if (denominator_2 == 1)
                                            {
                                                sizeString_2_WithUnit += numerator_2 + " " + sizeUnit_2.ToUpper();
                                                sizeString_2 += " " + numerator_2.ToString();
                                                size_2 = numerator_2;

                                            }
                                            else
                                            {
                                                size_2 = numerator_2 / denominator_2;

                                                if (numerator_2 == 3 && denominator_2 == 2)
                                                {
                                                    sizeString_2_WithUnit += "1 1" + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + "1 1" + "/" + denominator_2;
                                                }
                                                else
                                                {
                                                    sizeString_2_WithUnit += numerator_2 + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + numerator_2 + "/" + denominator_2;
                                                }



                                            }
                                        }

                                        if (size_1 >= size_2)
                                        {
                                            if (sizeString_2_WithUnit != "")
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit + " x " + sizeString_2_WithUnit;
                                            }


                                            else
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit;
                                            }
                                        }
                                        else
                                        {

                                            if (sizeString_2_WithUnit != "")
                                                sizeString_WithUnit = sizeString_2_WithUnit + " x " + sizeString_1_WithUnit;

                                            else
                                            {
                                                sizeString_WithUnit = sizeString_2_WithUnit;
                                            }
                                        }

                                        #endregion

                                        totalBag += bag;

                                        if (previousPO == "")
                                        {
                                            previousPO = currentPONo;
                                        }
                                        else if (previousPO != currentPONo)
                                        {
                                            previousPO = currentPONo;

                                            rowNo++;
                                            //if (rowNo + 1 < maxRow)
                                            //{

                                            //}
                                        }

                                        //check current row vs max row
                                        if (rowNo + 1 > maxRow)
                                        {
                                            rowNo = 1;
                                            pageNo++;
                                            //create new sheet
                                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                            if (pageNo > 1)
                                                xlWorkSheet.Name = customerName + "_" + DONoString + "_" + pageNo;

                                            NewExcelPageSetup(xlWorkSheet);

                                            InitialSPSMYDOFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);

                                            InsertToSheet(xlWorkSheet, areaDONoData, DONoString);

                                            if (PONo != Text_MultiPOCode)
                                            {
                                                if (useOwnDO)
                                                {
                                                    InsertToSheet(xlWorkSheet, areaPONoData, "*" + PONo);
                                                    //InsertToSheet(xlWorkSheet, areaOwnDORemark, text.DO_CustOwnDO);

                                                    //RemarkInDO = RemarkInDO + (string.IsNullOrEmpty(RemarkInDO) ? "" : "; ") + text.DO_CustOwnDO;
                                                }
                                                else
                                                {
                                                    InsertToSheet(xlWorkSheet, areaPONoData, PONo);

                                                }

                                            }
                                            else
                                            {
                                                InsertToSheet(xlWorkSheet, areaPONoData, "REFER BELOW");
                                            }

                                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);

                                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());

                                            Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString];

                                            InsertToSheet(previousSheet, areaPageData, 1 + " of " + pageNo);


                                            if (string.IsNullOrEmpty(billingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine4, billingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine5, billingState);
                                            }

                                            if (string.IsNullOrEmpty(shippingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                            }

                                            InsertToSheet(previousSheet, areaBillName, billingName);
                                            InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                            InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                            InsertToSheet(previousSheet, areaBillTel, billingContact);

                                            InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                            InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                            InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                            InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);
                                            InsertToSheet(previousSheet, areaRemarkInDO, RemarkInDO);

                                            if (!string.IsNullOrEmpty(transporterName))
                                            {
                                                InsertToSheet(previousSheet, areaTransportCompany1Title, "TRANSPORTER:");
                                                InsertToSheet(previousSheet, areaTransporterName, transporterName);
                                            }

                                            //change total page number in previous sheet(s)
                                            for (int i = 2; i < pageNo; i++)
                                            {
                                                previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString + "_" + i];

                                                InsertToSheet(previousSheet, areaPageData, i + " of " + pageNo);


                                                if (string.IsNullOrEmpty(billingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine5, billingState);
                                                }

                                                if (string.IsNullOrEmpty(shippingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                                }


                                                InsertToSheet(previousSheet, areaBillName, billingName);
                                                InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                                InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                                InsertToSheet(previousSheet, areaBillTel, billingContact);

                                                InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                                InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                                InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                                InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);
                                                InsertToSheet(previousSheet, areaRemarkInDO, RemarkInDO);

                                                if (!string.IsNullOrEmpty(transporterName))
                                                {
                                                    InsertToSheet(previousSheet, areaTransportCompany1Title, "TRANSPORTER:");
                                                    InsertToSheet(previousSheet, areaTransporterName, transporterName);
                                                }
                                            }

                                            sheetNo++;

                                        }
                                        else
                                        {
                                            rowNo++;
                                        }
                                        //insert data

                                        string RowToInsert = itemListColStart + (itemRowOffset + rowNo).ToString() + itemListColEnd + (itemRowOffset + rowNo).ToString();

                                        RowBorder(xlWorkSheet, RowToInsert);

                                        RowToInsert = indexColStart + (itemRowOffset + rowNo).ToString() + indexColEnd + (itemRowOffset + rowNo).ToString();

                                        InsertToSheet(xlWorkSheet, RowToInsert, indexNo);

                                        indexNo++;

                                        RowToInsert = ItemColStart + (itemRowOffset + rowNo).ToString() + ItemColEnd + (itemRowOffset + rowNo).ToString();

                                        string type_ShortName = GetTypeShortName(type);

                                        string newItemCode = text.SBB_BrandName + type_ShortName + " " + sizeString_1 + sizeString_2;

                                        InsertToSheet(xlWorkSheet, RowToInsert, newItemCode);

                                        RowToInsert = DescriptionColStart + (itemRowOffset + rowNo).ToString() + DescriptionColEnd + (itemRowOffset + rowNo).ToString();

                                        InsertToSheet(xlWorkSheet, RowToInsert, sizeString_WithUnit + " " + type + " - " + remark);

                                        RowToInsert = qtyColStart + (itemRowOffset + rowNo).ToString() + qtyColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, deliveryQty);

                                        RowToInsert = uomColStart + (itemRowOffset + rowNo).ToString() + uomColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, "PCS");


                                        if (PONo == Text_MultiPOCode)
                                        {
                                            RowToInsert = poColStart + (itemRowOffset + rowNo).ToString() + poColEnd + (itemRowOffset + rowNo).ToString();

                                            if (useOwnDO)
                                            {
                                                InsertToSheet(xlWorkSheet, RowToInsert, "*" + currentPONo);
                                                //InsertToSheet(xlWorkSheet, areaRemarkInDO, text.DO_CustOwnDO);

                                                RemarkInDO = RemarkInDO + (string.IsNullOrEmpty(RemarkInDO)? "" : "; ") + text.DO_CustOwnDO;
                                            }
                                            else
                                            {
                                                InsertToSheet(xlWorkSheet, RowToInsert, currentPONo);

                                            }

                                        }

                                    }

                                }
                            }

                            string totalRemark = "";

                            if (totalBag - totalOringBag <= 0 && totalOringBag > 0) 
                            {
                                if(totalOringBag > 1)
                                {
                                    //withS = "S";
                                }

                                totalRemark = "TOT. " + totalOringBag + " O'Ring PKT" + (totalOringBag > 1 ? "S" : "");
                            }
                            else if (totalBalPcs > 0)
                            {
                                if (totalOringBag > 0)
                                {
                   

                                    totalBag -= totalOringBag;
                                    totalRemark = "TOT. " + totalBag + " BAG"+ (totalBag > 1 ? "S" : "") + " + " + totalBalPcs + " PCS + " + totalOringBag + " O'Ring PKT" + (totalOringBag>1? "S" : "");
                                }
                                else
                                {
                                    totalRemark = "TOT. " + totalBag + " BAG" + (totalBag > 1 ? "S" : "") + " + " + totalBalPcs + " PCS";
                                }

                            }
                            else if (totalBag > 0)
                            {
                                if (totalOringBag > 0)
                                {
                                    totalBag -= totalOringBag;
                                    totalRemark = "TOT. " + totalBag + " BAG" + (totalBag > 1 ? "S" : "") +" + "+ totalOringBag + " O'Ring PKT" + (totalOringBag > 1 ? "S" : "");
                                }
                                else
                                {
                                    totalRemark = "TOT. " + totalBag + " BAG" + (totalBag > 1 ? "S" : "");
                                }
                            }

                            string sprinklerTotalQtyRemark = "";

                            if(totalBox > 0)
                            {
                                sprinklerTotalQtyRemark =  totalBox + " BOX" + (totalBox > 1 ? "ES" : "");
                            }

                            if (totalSprinklerPkt > 0)
                            {
                                if(string.IsNullOrEmpty(sprinklerTotalQtyRemark))
                                {
                                    sprinklerTotalQtyRemark += totalSprinklerPkt + " SP PKT" + (totalSprinklerPkt > 1 ? "S" : "");
                                }
                                else
                                {
                                    sprinklerTotalQtyRemark += " + " + totalSprinklerPkt + " SP PKT" + (totalSprinklerPkt > 1 ? "S" : "");
                                }
                            }

                            if (!string.IsNullOrEmpty(sprinklerTotalQtyRemark))
                            {
                                if (string.IsNullOrEmpty(totalRemark) )
                                {
                                    totalRemark  = "TOT. " + sprinklerTotalQtyRemark;
                                }
                                else
                                { 
                                    totalRemark +=  " + " + sprinklerTotalQtyRemark ;


                                }
                            }

                            //totalRemark = "";
                            if (!string.IsNullOrEmpty(totalRemark))
                            {
                                InsertToSheet(xlWorkSheet, areaTotalData, totalRemark);
                            }

                            if (string.IsNullOrEmpty(billingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingAddress_3);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine5, billingState);
                            }

                            if (string.IsNullOrEmpty(shippingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingAddress_3);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine5, shippingState);

                            }

                            InsertToSheet(xlWorkSheet, areaBillName, billingName);
                            InsertToSheet(xlWorkSheet, areaBillLine1, billingAddress_1);
                            InsertToSheet(xlWorkSheet, areaBillLine2, billingAddress_2);

                            InsertToSheet(xlWorkSheet, areaBillTel, billingContact);

                            InsertToSheet(xlWorkSheet, areaDeliveryName, shippingName);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine1, shippingAddress_1);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine2, shippingAddress_2);

                            InsertToSheet(xlWorkSheet, areaDeliveryTel, shippingContact);
                            InsertToSheet(xlWorkSheet, areaRemarkInDO, RemarkInDO);

                            if (!string.IsNullOrEmpty(transporterName))
                            {
                                InsertToSheet(xlWorkSheet, areaTransportCompany1Title, "Transport :");
                                InsertToSheet(xlWorkSheet, areaTransportCompany2Title, "Company  ");
                                InsertToSheet(xlWorkSheet, areaTransporterName, transporterName);
                            }

                            tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));

                            pageNo = 1;
                            rowNo = 0;

                            if (sheetNo > 0)
                            {
                                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                               misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                                //xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "D:\\x.pdf");

                                //System.Diagnostics.Process.Start("D:\\x.pdf");
                            }

                            // Clear Clipboard and DataGridView selection
                            Clipboard.Clear();
                            dgv.ClearSelection();

                            frmLoading.CloseForm();
                            Focus();

                            // Open the newly saved excel file
                            if (File.Exists(sfd.FileName))
                            {

                                if (openFile)
                                {
                                    Excel.Application excel = new Excel.Application();
                                    excel.Visible = true;
                                    excel.DisplayAlerts = false;
                                    Workbook wb = excel.Workbooks.Open(sfd.FileName, ReadOnly: false, Notify: false);
                                    excel.DisplayAlerts = true;

                                    //wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "D:\\x.pdf");

                                    //System.Diagnostics.Process.Start(sfd.FileName);
                                }
                                else
                                {
                                    MessageBox.Show("D/O export successful!");
                                }

                                if (printFile)
                                {
                                    xlexcel.Visible = printPreview;
                                    xlWorkBook.PrintOut(Preview: printPreview, Collate: true);

                                    //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, printPreview, Type.Missing, Type.Missing, true, Type.Missing);
                                }

                            }

                            xlexcel.DisplayAlerts = true;

                            xlWorkBook.Close(true, misValue, misValue);
                            xlexcel.Quit();

                            releaseObject(xlWorkSheet);

                            releaseObject(xlWorkBook);
                            releaseObject(xlexcel);
                        }

                    }
                }

            }
            #endregion
        }


        #region Export Invoice

        private void oldExportSBBInvoice()
        {

            #region Excel marking

            alignLeft = XlHAlign.xlHAlignLeft;
            alignRight = XlHAlign.xlHAlignRight;
            alignVCenter = XlVAlign.xlVAlignCenter;

            Color BorderColor = Color.Black;

            itemRowOffset = 17;
            maxRow = 20;

            sheetWidth = "a1:w1";
            wholeSheetArea = "a1:w45";

            #region Letterhead: Company Info

            rowCompanyNameCN = "a1:w1";
            rowCompanyNameEN = "a2:w2";
            rowSyarikatNo = "a3:w3";
            rowCompanyAddress = "a4:w4";
            rowSSTRegNo = "a5:w5";
            rowDocumentVersionControl = "a6:w6";

            #endregion

            #region Invoice Info

            areaDOInfo = "a7:w16";
            rowDoInfoStart = "a7:w7";
            areaBillTo = "a7:g7";
            areaDeliverTo = "i7:o7";

            rowDeliveryOrder = "q7:w8";

            areaBillName = "a8:g8";
            areaBillLine1 = "a9:g9";
            areaBillLine2 = "a10:g10";
            areaBillLine3 = "a11:g11";
            areaBillLine4 = "a12:g12";
            areaBillLine5 = "a13:g13";
            areaBillTel = "a14:g14";

            areaDeliveryName = "i8:o8";
            areaDeliveryLine1 = "i9:o9";
            areaDeliveryLine2 = "i10:o10";
            areaDeliveryLine3 = "i11:o11";
            areaDeliveryLine4 = "i12:o12";
            areaDeliveryLine5 = "i13:o13";
            areaDeliveryTel = "i14:o14";

            areaDONo = "q9:s9";
            areaDate = "q10:s10";
            areaPONo = "q11:s11";
            areaPage = "q15:s15";

            areaDONoData = "t9:w9";
            areaDateData = "t10:w10";
            areaPONoData = "t11:w11";
            areaPageData = "t15:w15";

            #endregion

            #region Item List

            rowListTitle = "a17:w17";
            areaItemList = "a18:w37";

            areaIndex = "a17:a17";
            areaPO = "b17:d17";
            areaDescription = "e17:l17";
            areaQuantity = "m17:n17";
            areaUOM = "o17:p17";

            itemListColStart = "a";
            itemListColEnd = ":w";

            indexColStart = "a";
            indexColEnd = ":a";

            DescriptionColStart = "e";
            DescriptionColEnd = ":l";

            qtyColStart = "m";
            qtyColEnd = ":n";

            uomColStart = "o";
            uomColEnd = ":p";

            poColStart = "b";
            poColEnd = ":d";

            rowItemListLastRow = "a37:w37";
          
            #endregion

            #region Sign & Chop

            areaSafetySign = "a39:m45";
            areaSafetyCompanyName = "a39:m39";
            areaIssuedBy = "b45:d45";
            areaPreparedBy = "f45:h45";
            areaDriver = "j45:l45";
            areaGoodsCheckedandReceivedBy = "p39:w39";
            areaCustomerChopandSign = "p45:w45";

            #endregion


            #endregion

            #region Export Settings

            DateTime DODate = frmExportSetting.DODate;
            bool openFile = frmExportSetting.openFileAfterExport;
            bool printFile = frmExportSetting.printFileAfterExport;
            bool printPreview = frmExportSetting.printPreview;
            bool EXCEL_TYPE = frmExportSetting.EXCEL_TYPE;
            bool PDF_TYPE = frmExportSetting.PDF_TYPE;
            bool Single_File = frmExportSetting.SINGLE_FILE_ONLY;

            string DODate_String = DODate.ToString("dd/MM/yyyy");

            #endregion

            #region export

            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                dt_DOList.DefaultView.Sort = header_DONo + " ASC";
                dt_DOList = dt_DOList.DefaultView.ToTable();

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
               
                string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

                string path = @"D:\StockAssistant\Document\SBB Invoice";

                if (myconnstrng == text.DB_Semenyih)
                {
                    //string folderName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");

                    path = @"\\ADMIN001\Admin Server\( ACCOUNT & FINANCE )\(SBB INVOICE)\";
                }

                Directory.CreateDirectory(path);

                Cursor = Cursors.Arrow; // change cursor to normal type

                foreach (DataRow rowDOList in dt_DOList.Rows)
                {
                    bool selected = bool.TryParse(rowDOList[header_Selected].ToString(), out selected) ? selected : false;

                    //export selected DO only
                    if (selected)
                    {
                        #region getting basic info
                        string deliveredDate = rowDOList[header_DeliveredDate].ToString();

                        DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate;
                        DODate_String = DODate.ToString("dd/MM/yyyy");

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.InitialDirectory = path;

                        string customerName = rowDOList[header_Customer].ToString();
                        string POTableCode = rowDOList[header_POCode].ToString();
                        string PONo = rowDOList[header_PONo].ToString();
                        string DONo = rowDOList[header_DONo].ToString();
                        string DONoString = rowDOList[header_DONoString].ToString();

                        // Check if this is a non-billing DO
                        IS_NON_BILLING_DO = IsNonBillingDO(DONo);

                        sfd.Filter = "Excel Documents (*.xls)|*.xls";
                        sfd.FileName = "INV_"+ DONoString + ".xls";
                        #endregion

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            #region Excel Settings

                            frmLoading.ShowLoadingScreen();
                            tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                            // Copy DataGridView results to clipboard
                            copyAlltoClipboard();
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                            object misValue = Missing.Value;

                            Excel.Application xlexcel = new Excel.Application
                            {
                                PrintCommunication = false,
                                ScreenUpdating = false,
                                DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                            };

                            Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);


                            xlexcel.StandardFont = "Calibri";
                            xlexcel.StandardFontSize = 12;

                            xlexcel.Calculation = XlCalculation.xlCalculationManual;
                            xlexcel.PrintCommunication = false;

                            int sheetNo = 0;
                            int pageNo = 1;
                            int rowNo = 0;

                            Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;

                            if (sheetNo > 0)
                            {
                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                            }

                            sheetNo++;


                            xlWorkSheet.Name = customerName + "_" + DONoString;

                            var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                            int printerIndex = 0;

                            foreach (String s in printers)
                            {
                                if (s.Equals("RICOH MP C3503"))
                                {
                                    break;
                                }
                                printerIndex++;
                            }

                            //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, printers[printerIndex], Type.Missing, Type.Missing, Type.Missing);

                            xlexcel.PrintCommunication = true;
                            NewExcelPageSetup(xlWorkSheet);
                            //192.168.1.111:53000
                            //xlexcel.ActivePrinter = "\\\\192.168.1.111:53000";
                            xlexcel.PrintCommunication = true;

                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            InitialInvoiceFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);

                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);
                            //InsertToSheet(xlWorkSheet, areaDONoData, DONoString);
                            InsertToSheet(xlWorkSheet, areaDONoData, "INV" + DONoString);
                            InsertToSheet(xlWorkSheet, "t12:w12", DONoString);

                            //to do: check if more than one PO
                            InsertToSheet(xlWorkSheet, areaPONoData, PONo == Text_MultiPOCode ? "REFER BELOW" : PONo);
                            //InsertToSheet(xlWorkSheet, areaPONoData, "REFER BELOW");

                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());


                            string billingName = null;
                            string billingAddress_1 = null;
                            string billingAddress_2 = null;
                            string billingAddress_3 = null;
                            string billingPostalCode = null;
                            string billingCity = null;
                            string billingState = null;
                            string billingContact = null;

                            string shippingName = null;
                            string shippingAddress_1 = null;
                            string shippingAddress_2 = null;
                            string shippingAddress_3 = null;
                            string shippingPostalCode = null;
                            string shippingCity = null;
                            string shippingState = null;
                            string shippingContact = null;
                            string transporterName = "";
                            int totalBag = 0;
                            int totalBox = 0;
                            int totalOringBag = 0;
                            int totalSprinklerPkt = 0;
                            int totalBalPcs = 0;

                            int indexNo = 1;
                            string previousPO = "";

                            decimal totalAmount = 0;

                            #endregion

                            foreach (DataRow rowDBInfo in DB_DO_INFO.Rows)
                            {
                                if (DONo == rowDBInfo[dalSPP.DONo].ToString() && rowDBInfo[dalSPP.DONo] != DBNull.Value)
                                {
                                    int deliveryQty = rowDBInfo[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(rowDBInfo[dalSPP.ToDeliveryQty].ToString());
                                  
                                    if (deliveryQty > 0)
                                    {
                                        #region Billing/Shipping Address

                                        bool useBillingAddress = bool.TryParse(rowDBInfo[dalSPP.DefaultShippingAddress].ToString(), out useBillingAddress) ? useBillingAddress : false;

                                        billingName = rowDBInfo[dalSPP.FullName].ToString();
                                        billingAddress_1 = rowDBInfo[dalSPP.Address1 + "1"].ToString();
                                        billingAddress_2 = rowDBInfo[dalSPP.Address2 + "1"].ToString();
                                        billingAddress_3 = rowDBInfo[dalSPP.Address3 + "1"].ToString();

                                        billingPostalCode = rowDBInfo[dalSPP.AddressPostalCode + "1"].ToString();
                                        billingCity = rowDBInfo[dalSPP.AddressCity + "1"].ToString();
                                        billingState = rowDBInfo[dalSPP.AddressState + "1"].ToString();
                                        billingContact = rowDBInfo[dalSPP.Phone1 + "1"].ToString();


                                        if (useBillingAddress)
                                        {
                                            shippingName = billingName;
                                            shippingAddress_1 = billingAddress_1;
                                            shippingAddress_2 = billingAddress_2;
                                            shippingAddress_3 = billingAddress_3;

                                            shippingPostalCode = billingPostalCode;
                                            shippingCity = billingCity;
                                            shippingState = billingState;
                                            shippingContact = billingContact;
                                        }
                                        else
                                        {
                                            shippingName = rowDBInfo[dalSPP.ShippingFullName].ToString();
                                            shippingAddress_1 = rowDBInfo[dalSPP.Address1].ToString();
                                            shippingAddress_2 = rowDBInfo[dalSPP.Address2].ToString();
                                            shippingAddress_3 = rowDBInfo[dalSPP.Address3].ToString();

                                            shippingPostalCode = rowDBInfo[dalSPP.AddressPostalCode].ToString();
                                            shippingCity = rowDBInfo[dalSPP.AddressCity].ToString();
                                            shippingState = rowDBInfo[dalSPP.AddressState].ToString();
                                            shippingContact = rowDBInfo[dalSPP.Phone1].ToString();
                                        }
                                        #endregion

                                        #region Getting item Info/Size

                                        string type = rowDBInfo[dalSPP.TypeName].ToString();

                                        string size = rowDBInfo[dalSPP.SizeNumerator].ToString();
                                        string itemCode = rowDBInfo[dalSPP.ItemCode].ToString();

                                        int numerator = int.TryParse(rowDBInfo[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                                        int denominator = int.TryParse(rowDBInfo[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                                        string sizeUnit = rowDBInfo[dalSPP.SizeUnit].ToString().ToUpper();

                                        if (sizeUnit.ToUpper() == "IN")
                                        {
                                            sizeUnit = "\"";
                                        }

                                        int numerator_2 = int.TryParse(rowDBInfo[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                                        int denominator_2 = int.TryParse(rowDBInfo[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                                        string sizeUnit_2 = rowDBInfo[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                                        if (sizeUnit_2.ToUpper() == "IN")
                                        {
                                            sizeUnit_2 = "\"";
                                        }

                                        string sizeString_WithUnit = "";
                                        string sizeString_1_WithUnit = "";
                                        string sizeString_2_WithUnit = "";

                                        string sizeString_1 = "";
                                        string sizeString_2 = "";

                                        int size_1 = 1;
                                        int size_2 = 1;

                                        if (denominator == 1)
                                        {
                                            size_1 = numerator;
                                            sizeString_1_WithUnit = numerator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator.ToString();

                                        }
                                        else
                                        {
                                            size_1 = numerator / denominator;
                                            sizeString_1_WithUnit = numerator + "/" + denominator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator + "/" + denominator;
                                        }

                                        if (numerator_2 > 0)
                                        {
                                            if (denominator_2 == 1)
                                            {
                                                sizeString_2_WithUnit += numerator_2 + " " + sizeUnit_2.ToUpper();
                                                sizeString_2 += " " + numerator_2.ToString();
                                                size_2 = numerator_2;

                                            }
                                            else
                                            {
                                                size_2 = numerator_2 / denominator_2;

                                                if (numerator_2 == 3 && denominator_2 == 2)
                                                {
                                                    sizeString_2_WithUnit += "1 1" + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + "1 1" + "/" + denominator_2;
                                                }
                                                else
                                                {
                                                    sizeString_2_WithUnit += numerator_2 + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + numerator_2 + "/" + denominator_2;
                                                }



                                            }
                                        }

                                        if (size_1 >= size_2)
                                        {
                                            if (sizeString_2_WithUnit != "")
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit + " x " + sizeString_2_WithUnit;
                                            }


                                            else
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit;
                                            }
                                        }
                                        else
                                        {

                                            if (sizeString_2_WithUnit != "")
                                                sizeString_WithUnit = sizeString_2_WithUnit + " x " + sizeString_1_WithUnit;

                                            else
                                            {
                                                sizeString_WithUnit = sizeString_2_WithUnit;
                                            }
                                        }

                                        #endregion

                                        #region Checking PO No

                                        string currentPONo = rowDBInfo[dalSPP.PONo].ToString();
                                       
                                        if (previousPO != currentPONo)
                                        {
                                            if (previousPO != "")
                                            {
                                                //rowNo++;
                                            }

                                            previousPO = currentPONo;
                                        }

                                        #endregion

                                        #region checking if need to open new page

                                        if (rowNo + 1 > maxRow)
                                        {
                                            #region clear previous page total amount and notes

                                            Range DOFormat = xlWorkSheet.get_Range("a38:w45").Cells;
                                            ExcelMergeandAlign(xlWorkSheet, "a38:w45", alignLeft, XlVAlign.xlVAlignTop);
                                            DOFormat.Merge();
                                            DOFormat.Value = "\nNext Page";
                                            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
                                            DOFormat.Font.Size = 10;
                                            DOFormat.Font.Name = "Cambria";
                                            DOFormat.Font.Bold = false;
                                            DOFormat.Font.Italic = true;

                                            #endregion

                                            rowNo = 1;

                                            pageNo++;
                                            //create new sheet
                                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                            if (pageNo > 1)
                                                xlWorkSheet.Name = customerName + "_" + DONoString + "_" + pageNo;

                                            NewExcelPageSetup(xlWorkSheet);

                                            InitialInvoiceFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);

                                            InsertToSheet(xlWorkSheet, areaDONoData, "INV" + DONoString);
                                            InsertToSheet(xlWorkSheet, "t12:w12", DONoString);

                                            InsertToSheet(xlWorkSheet, areaPONoData, PONo == Text_MultiPOCode ? "REFER BELOW" : PONo);
                                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);

                                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());

                                            Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString];

                                            InsertToSheet(previousSheet, areaPageData, 1 + " of " + pageNo);


                                            if (string.IsNullOrEmpty(billingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine4, billingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine5, billingState);
                                            }

                                            if (string.IsNullOrEmpty(shippingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                            }

                                            InsertToSheet(previousSheet, areaBillName, billingName);
                                            InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                            InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                            InsertToSheet(previousSheet, areaBillTel, billingContact);

                                            InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                            InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                            InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                            InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);

                                            //change total page number in previous sheet(s)
                                            for (int i = 2; i < pageNo; i++)
                                            {
                                                previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString + "_" + i];

                                                InsertToSheet(previousSheet, areaPageData, i + " of " + pageNo);


                                                if (string.IsNullOrEmpty(billingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine5, billingState);
                                                }

                                                if (string.IsNullOrEmpty(shippingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                                }


                                                InsertToSheet(previousSheet, areaBillName, billingName);
                                                InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                                InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                                InsertToSheet(previousSheet, areaBillTel, billingContact);

                                                InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                                InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                                InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                                InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);
                                           
                                            }

                                            sheetNo++;

                                        }
                                        else
                                        {
                                            rowNo++;
                                        }

                                        #endregion

                                        #region Data Insert

                                        //index
                                        string RowToInsert = itemListColStart + (itemRowOffset + rowNo).ToString() + itemListColEnd + (itemRowOffset + rowNo).ToString();
                                        RowToInsert = indexColStart + (itemRowOffset + rowNo).ToString() + indexColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, indexNo);
                                        indexNo++;

                                        //po data
                                        if (PONo == Text_MultiPOCode)
                                        {
                                            RowToInsert = poColStart + (itemRowOffset + rowNo).ToString() + poColEnd + (itemRowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, RowToInsert, currentPONo);
                                        }


                                        //product decription
                                        string type_ShortName = GetTypeShortName(type);
                                        string newItemCode = text.SBB_BrandName + type_ShortName + " " + sizeString_1 + sizeString_2;
                                        string ProductDescription = "("+newItemCode + ") " + sizeString_WithUnit + " " + type;
                                        RowToInsert = DescriptionColStart + (itemRowOffset + rowNo).ToString() + DescriptionColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, ProductDescription);

                                        //delivery qty
                                        RowToInsert = qtyColStart + (itemRowOffset + rowNo).ToString() + qtyColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, deliveryQty);

                                        //unit
                                        RowToInsert = uomColStart + (itemRowOffset + rowNo).ToString() + uomColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, "PCS");

                                        //unit price
                                        decimal unitPrice = decimal.TryParse(rowDBInfo[dalSPP.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                        RowToInsert = "q" + (itemRowOffset + rowNo).ToString() + ":r" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, unitPrice.ToString("N2"));

                                        //discount rate
                                        decimal DiscRate = decimal.TryParse(rowDBInfo[dalSPP.DiscountRate].ToString(), out DiscRate) ? DiscRate : 0;
                                        RowToInsert = "s" + (itemRowOffset + rowNo).ToString() + ":t" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, DiscRate.ToString("N2"));

                                        //net amount
                                        //decimal netAmount = deliveryQty * unitPrice * (100 - DiscRate) / 100;
                                        decimal netAmount = Math.Round(deliveryQty * unitPrice * (100 - DiscRate) / 100, 2);

                                        RowToInsert = "u" + (itemRowOffset + rowNo).ToString() + ":w" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, netAmount.ToString("N2"));

                                        //total amount
                                        totalAmount += Math.Round(netAmount, 2);
                                        InsertToSheet(xlWorkSheet, areaTotalAmountText, ConvertAmountToWords(totalAmount));
                                        InsertToSheet(xlWorkSheet, areaTotalAmount, totalAmount.ToString("N2"));

                                        #endregion
                                    }

                                }
                            }

                            #region insert Billing/Shipping Address
                            if (string.IsNullOrEmpty(billingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingAddress_3);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine5, billingState);
                            }

                            if (string.IsNullOrEmpty(shippingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingAddress_3);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine5, shippingState);

                            }

                            InsertToSheet(xlWorkSheet, areaBillName, billingName);
                            InsertToSheet(xlWorkSheet, areaBillLine1, billingAddress_1);
                            InsertToSheet(xlWorkSheet, areaBillLine2, billingAddress_2);

                            InsertToSheet(xlWorkSheet, areaBillTel, billingContact);

                            InsertToSheet(xlWorkSheet, areaDeliveryName, shippingName);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine1, shippingAddress_1);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine2, shippingAddress_2);

                            InsertToSheet(xlWorkSheet, areaDeliveryTel, shippingContact);
                            #endregion

                            tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));

                            pageNo = 1;
                            rowNo = 0;

                            #region Excel/PDF Save and open
                            if (sheetNo > 0)
                            {
                                if (EXCEL_TYPE)
                                {
                                    //Export to EXCEL
                                    xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                                   misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                }
                                else if (PDF_TYPE)
                                {
                                    // Export to PDF
                                    // Determine the PDF file path (assuming you want to save the PDF in the same location as the Excel file)
                                    string pdfFilePath = Path.ChangeExtension(sfd.FileName, ".pdf");
                                    xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFilePath,
                                                                   XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                                }


                                if (File.Exists(sfd.FileName))
                                {
                                    if (openFile)
                                    {
                                        //Excel.Application excel = new Excel.Application();
                                        //excel.Visible = true;
                                        //excel.DisplayAlerts = false;
                                        //Workbook wb = excel.Workbooks.Open(sfd.FileName, ReadOnly: false, Notify: false);
                                        //excel.DisplayAlerts = true;

                                        ////wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "D:\\x.pdf");

                                        System.Diagnostics.Process.Start(sfd.FileName);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invoice export successful!");
                                    }

                                    //if (printFile)
                                    //{
                                    //    xlexcel.Visible = printPreview;
                                    //    xlWorkBook.PrintOut(Preview: printPreview, Collate: true);

                                    //    //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, printPreview, Type.Missing, Type.Missing, true, Type.Missing);
                                    //}

                                }

                            }

                            Clipboard.Clear();
                            dgv.ClearSelection();

                            frmLoading.CloseForm();
                            Focus();

                            if (EXCEL_TYPE)
                            {
                                xlexcel.DisplayAlerts = true;
                            }

                            xlWorkBook.Close(false, misValue, misValue);

                            xlexcel.Quit();

                            releaseObject(xlWorkSheet);

                            releaseObject(xlWorkBook);
                            releaseObject(xlexcel);

                            #endregion

                            #region old save and open settings
                            /*
                            if (sheetNo > 0)
                            {
                                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                               misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                                //xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "D:\\x.pdf");

                                //System.Diagnostics.Process.Start("D:\\x.pdf");
                            }

                            // Clear Clipboard and DataGridView selection
                            Clipboard.Clear();
                            dgv.ClearSelection();

                            frmLoading.CloseForm();
                            Focus();

                            // Open the newly saved excel file
                            if (File.Exists(sfd.FileName))
                            {

                                if (openFile)
                                {
                                    Excel.Application excel = new Excel.Application();
                                    excel.Visible = true;
                                    excel.DisplayAlerts = false;
                                    Workbook wb = excel.Workbooks.Open(sfd.FileName, ReadOnly: false, Notify: false);
                                    excel.DisplayAlerts = true;

                                    //wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "D:\\x.pdf");

                                    //System.Diagnostics.Process.Start(sfd.FileName);
                                }
                                else
                                {
                                    MessageBox.Show("D/O export successful!");
                                }

                                if (printFile)
                                {
                                    xlexcel.Visible = printPreview;
                                    xlWorkBook.PrintOut(Preview: printPreview, Collate: true);

                                    //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, printPreview, Type.Missing, Type.Missing, true, Type.Missing);
                                }

                            }

                            xlexcel.DisplayAlerts = true;

                            xlWorkBook.Close(true, misValue, misValue);
                            xlexcel.Quit();

                            releaseObject(xlWorkSheet);

                            releaseObject(xlWorkBook);
                            releaseObject(xlexcel);*/
                            #endregion
                        }

                    }
                }

            }
            #endregion
        }

        public static string GetTerms(string customerCode)
        {
            string terms_60Days = "60 Days";
            string terms_90Days = "90 Days";
            string terms_120Days = "120 Days";
            string terms_CashOnDelivery = "C.O.D.";
            var termsTable = new Dictionary<string, string>
            {
                { "47", terms_90Days }, // AIK MENG
                { "51", terms_60Days }, // ALIKE
                { "37", terms_CashOnDelivery }, // BANDWELL
                { "17", terms_90Days }, // BENTON
                { "21", terms_120Days }, // BLUE
                { "23", terms_90Days }, // CHIN XIANG
                { "2", terms_120Days }, // CL
                { "3", terms_120Days }, // DELTA
                { "4", terms_120Days }, // DYSON
                { "22", terms_120Days }, // ENG
                { "36", terms_120Days }, // HOCK LEE
                { "41", terms_CashOnDelivery }, // HF HENG FATT
                { "14", terms_60Days }, // INN
                { "32", terms_120Days }, // KC KWONG
                { "13", terms_120Days }, // LIANHONG
                { "44", terms_90Days }, // NAN YA
                { "18", terms_120Days }, // OEL
                { "53", terms_CashOnDelivery }, // PERDASA
                { "31", terms_60Days }, // PIPELINE
                { "5", terms_120Days }, // ROWY
                { "46", terms_CashOnDelivery }, // SIGMA
                { "38", terms_90Days }, // SIN SENG
                { "52", terms_CashOnDelivery }, // SINAR CERAH
                { "27", terms_90Days }, // SUNWAY
                { "20", terms_120Days }, // WEI HUA
                { "54", terms_CashOnDelivery }, // Sanifix
                { "25", terms_120Days } // WS
            };

            if (termsTable.TryGetValue(customerCode, out string terms))
            {
                return terms;
            }
            else
            {
                return "";
            }
        }



        private void ExportSBBInvoice_SingleFile()
        {
            #region Excel marking

            alignLeft = XlHAlign.xlHAlignLeft;
            alignRight = XlHAlign.xlHAlignRight;
            alignVCenter = XlVAlign.xlVAlignCenter;

            Color BorderColor = Color.Black;

            itemRowOffset = 17;
            maxRow = 20;

            sheetWidth = "a1:w1";
            wholeSheetArea = "a1:w45";

            #region Letterhead: Company Info

            rowCompanyNameCN = "a1:w1";
            rowCompanyNameEN = "a2:w2";
            rowSyarikatNo = "a3:w3";
            rowCompanyAddress = "a4:w4";
            rowSSTRegNo = "a5:w5";
            rowDocumentVersionControl = "a6:w6";

            #endregion

            #region Invoice Info

            areaDOInfo = "a7:w16";
            rowDoInfoStart = "a7:w7";
            areaBillTo = "a7:g7";
            areaDeliverTo = "i7:o7";

            rowDeliveryOrder = "q7:w8";

            areaBillName = "a8:g8";
            areaBillLine1 = "a9:g9";
            areaBillLine2 = "a10:g10";
            areaBillLine3 = "a11:g11";
            areaBillLine4 = "a12:g12";
            areaBillLine5 = "a13:g13";
            areaBillTel = "a14:g14";

            areaDeliveryName = "i8:o8";
            areaDeliveryLine1 = "i9:o9";
            areaDeliveryLine2 = "i10:o10";
            areaDeliveryLine3 = "i11:o11";
            areaDeliveryLine4 = "i12:o12";
            areaDeliveryLine5 = "i13:o13";
            areaDeliveryTel = "i14:o14";

            areaDONo = "q9:s9";
            areaDate = "q10:s10";
            areaPONo = "q11:s11";
            areaPage = "q15:s15";

            areaDONoData = "t9:w9";
            areaDateData = "t10:w10";
            areaPONoData = "t11:w11";
            areaPageData = "t15:w15";

            #endregion

            #region Item List

            rowListTitle = "a17:w17";
            areaItemList = "a18:w37";

            areaIndex = "a17:a17";
            areaPO = "b17:d17";
            areaDescription = "e17:l17";
            areaQuantity = "m17:n17";
            areaUOM = "o17:p17";

            itemListColStart = "a";
            itemListColEnd = ":w";

            indexColStart = "a";
            indexColEnd = ":a";

            DescriptionColStart = "e";
            DescriptionColEnd = ":l";

            qtyColStart = "m";
            qtyColEnd = ":n";

            uomColStart = "o";
            uomColEnd = ":p";

            poColStart = "b";
            poColEnd = ":d";

            rowItemListLastRow = "a37:w37";

            #endregion

            #region Sign & Chop

            areaSafetySign = "a39:m45";
            areaSafetyCompanyName = "a39:m39";
            areaIssuedBy = "b45:d45";
            areaPreparedBy = "f45:h45";
            areaDriver = "j45:l45";
            areaGoodsCheckedandReceivedBy = "p39:w39";
            areaCustomerChopandSign = "p45:w45";

            #endregion


            #endregion

            #region Export Settings

            DateTime DODate = frmExportSetting.DODate;
            bool openFile = frmExportSetting.openFileAfterExport;
            bool printFile = frmExportSetting.printFileAfterExport;
            bool printPreview = frmExportSetting.printPreview;
            bool EXCEL_TYPE = frmExportSetting.EXCEL_TYPE;
            bool PDF_TYPE = frmExportSetting.PDF_TYPE;
            string DODate_String = DODate.ToString("dd/MM/yyyy");

            #endregion

            #region export

            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;
           

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                #region Pre-Settings

                dt_DOList.DefaultView.Sort = header_DONo + " ASC";
                dt_DOList = dt_DOList.DefaultView.ToTable();

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

                string path = @"D:\StockAssistant\Document\SBB Invoice";

                if (myconnstrng == text.DB_Semenyih)
                {
                    //string folderName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");

                    path = @"\\ADMIN001\Admin Server\( ACCOUNT & FINANCE )\(SBB INVOICE)\";
                }

                Directory.CreateDirectory(path);

                Cursor = Cursors.Arrow; // change cursor to normal type
                object misValue = Missing.Value;

                string firstInvoiceFileName = "";
                string lastInvoiceFileName = "";
                Workbook xlWorkBook = null;
                Excel.Application xlexcel = null;

                xlexcel = new Excel.Application
                {
                    PrintCommunication = false,
                    ScreenUpdating = false,
                    DisplayAlerts = false
                };
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlexcel.StandardFont = "Calibri";
                xlexcel.StandardFontSize = 12;
                xlexcel.Calculation = XlCalculation.xlCalculationManual;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = path;

                int sheetNo = 0;
                int pageNo = 1;
                int rowNo = 0;
                int invoiceCount = 0;

                Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;


                DateTime fromDate = DateTime.MaxValue;
                DateTime toDate = DateTime.MinValue;

                #endregion
                //^^1000ms

                #region List selected check and file name setting
                foreach (DataRow rowDOList in dt_DOList.Rows)
                {
                    bool selected = bool.TryParse(rowDOList[header_Selected].ToString(), out selected) ? selected : false;
                
                    if(selected)
                    {
                        string deliveredDate = rowDOList[header_DeliveredDate].ToString();

                        DateTime Date = DateTime.TryParse(deliveredDate, out DateTime test) ? test : DateTime.MinValue;


                        if (fromDate > Date)
                        {
                            fromDate = Date;
                        }
                        
                        if(Date > toDate)
                        {
                            toDate = Date;
                        }

                        string DONoString = rowDOList[header_DONoString].ToString();
                        invoiceCount++;
                        if (string.IsNullOrEmpty(firstInvoiceFileName))
                        {
                            firstInvoiceFileName = "INV_" + DONoString;
                        }
                        else
                        {
                            lastInvoiceFileName = "INV_" + DONoString;
                        }
                        //string PONo = rowDOList[header_PONo].ToString();
                        //if (PONo == Text_MultiPOCode)
                        //{
                        //    MultiPOFound = true;
                        //}
                    }
                }

                string finalFileName = firstInvoiceFileName + " - " + lastInvoiceFileName;

                if (firstInvoiceFileName == lastInvoiceFileName || string.IsNullOrEmpty(lastInvoiceFileName))
                {
                    finalFileName = firstInvoiceFileName;
                }

                if(invoiceCount > 1)
                    finalFileName += " (" + invoiceCount + ")";

                string fullPath = Path.Combine(path, finalFileName);

                
                if(fromDate == DateTime.MaxValue)
                {
                    fromDate = toDate;
                }

                if(fromDate > toDate)
                {
                    DateTime dateTmp = fromDate;
                    
                    toDate = dateTmp;
                }

                string MonthFrom = fromDate.ToString("MMMyy");
                string MonthTo = toDate.ToString("MMMyy");

                string Month = MonthFrom;

                if (MonthFrom != MonthTo)
                {
                    Month = MonthFrom + "-" + MonthTo;
                }

                if(PDF_TYPE)
                {
                    sfd.Filter = "Pdf Documents (*.pdf)|*.pdf";
                    sfd.FileName = "SBB " + Month + " " + finalFileName + ".pdf";
                }
                else
                {
                    sfd.Filter = "Pdf Documents (*.xls)|*.xls";
                    sfd.FileName = "SBB " + Month + " " + finalFileName + ".xls";
                }

                #endregion
                //^^786ms

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    DataTable dt_DOInfo = DB_DO_INFO.Copy();
                    dt_DOInfo.DefaultView.Sort = dalSPP.PONo + " ASC";
                    dt_DOInfo = dt_DOInfo.DefaultView.ToTable();

                    if(cbInvoiceSortByCustomer.Checked)
                    {
                        dt_DOList.DefaultView.Sort = header_Customer + " ASC," + header_DONo + " ASC";
                        dt_DOList = dt_DOList.DefaultView.ToTable();
                    }

                    foreach (DataRow rowDOList in dt_DOList.Rows)
                    {
                        bool selected = bool.TryParse(rowDOList[header_Selected].ToString(), out selected) ? selected : false;

                        //export selected DO only
                        if (selected)
                        {
                            #region getting basic info

                            string deliveredDate = rowDOList[header_DeliveredDate].ToString();

                            DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate;
                            DODate_String = DODate.ToString("dd/MM/yyyy");

                            string customerName = rowDOList[header_Customer].ToString();
                            string POTableCode = rowDOList[header_POCode].ToString();
                            string PONo = rowDOList[header_PONo].ToString();
                            string DONo = rowDOList[header_DONo].ToString();
                            string DONoString = rowDOList[header_DONoString].ToString();

                            string customerCode = rowDOList[header_CustomerCode].ToString();

                            //DataRow customerRow = dt_CustomerSalesReport.NewRow();

                            #endregion

                            #region Excel Settings

                            frmLoading.ShowLoadingScreen();

                            if (sheetNo > 0)
                            {
                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                            }
                            sheetNo++;
                            xlWorkSheet.Name = "INV_" + DONoString;

                            var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                            int printerIndex = 0;

                            foreach (String s in printers)
                            {
                                if (s.Equals("RICOH MP C3503"))
                                {
                                    break;
                                }
                                printerIndex++;
                            }

                            xlexcel.PrintCommunication = true;
                            NewExcelPageSetup(xlWorkSheet);//346ms
                            xlexcel.PrintCommunication = true;
                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;
                          

                            //if(PONo == Text_MultiPOCode)
                            //{
                            //    CopyFormat(initialSheet_MultiPO, xlWorkSheet);
                            //}
                            //else
                            //{
                            //    CopyFormat(initialSheet_SinglePO, xlWorkSheet);
                            //}

                            //InitialInvoiceFormat_LOGO(xlWorkSheet);

                            InitialInvoiceFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);//2194ms

                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);
                            InsertToSheet(xlWorkSheet, areaDONoData, "INV" + DONoString);
                            InsertToSheet(xlWorkSheet, "t12:w12", DONoString);

                            //to do: check if more than one PO
                            InsertToSheet(xlWorkSheet, areaPONoData, PONo == Text_MultiPOCode ? "REFER BELOW" : PONo);
                            //InsertToSheet(xlWorkSheet, areaPONoData, "REFER BELOW");

                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());

                            InsertToSheet(xlWorkSheet, areaTermData, GetTerms(customerCode));


                            string billingName = null;
                            string billingAddress_1 = null;
                            string billingAddress_2 = null;
                            string billingAddress_3 = null;
                            string billingPostalCode = null;
                            string billingCity = null;
                            string billingState = null;
                            string billingContact = null;

                            string shippingName = null;
                            string shippingAddress_1 = null;
                            string shippingAddress_2 = null;
                            string shippingAddress_3 = null;
                            string shippingPostalCode = null;
                            string shippingCity = null;
                            string shippingState = null;
                            string shippingContact = null;

                            int indexNo = 1;
                            string previousPO = "";

                            decimal totalAmount = 0;

                            #endregion
                            //^^2585ms->540ms->242ms

                            foreach (DataRow rowDBInfo in dt_DOInfo.Rows)
                            {
                                if (DONo == rowDBInfo[dalSPP.DONo].ToString() && rowDBInfo[dalSPP.DONo] != DBNull.Value)
                                {
                                    int deliveryQty = rowDBInfo[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(rowDBInfo[dalSPP.ToDeliveryQty].ToString());

                                    if (deliveryQty > 0)
                                    {
                                        #region Billing/Shipping Address

                                        bool useBillingAddress = bool.TryParse(rowDBInfo[dalSPP.DefaultShippingAddress].ToString(), out useBillingAddress) ? useBillingAddress : false;

                                        billingName = rowDBInfo[dalSPP.FullName].ToString();
                                        billingAddress_1 = rowDBInfo[dalSPP.Address1 + "1"].ToString();
                                        billingAddress_2 = rowDBInfo[dalSPP.Address2 + "1"].ToString();
                                        billingAddress_3 = rowDBInfo[dalSPP.Address3 + "1"].ToString();

                                        billingPostalCode = rowDBInfo[dalSPP.AddressPostalCode + "1"].ToString();
                                        billingCity = rowDBInfo[dalSPP.AddressCity + "1"].ToString();
                                        billingState = rowDBInfo[dalSPP.AddressState + "1"].ToString();
                                        billingContact = rowDBInfo[dalSPP.Phone1 + "1"].ToString();


                                        if (useBillingAddress)
                                        {
                                            shippingName = billingName;
                                            shippingAddress_1 = billingAddress_1;
                                            shippingAddress_2 = billingAddress_2;
                                            shippingAddress_3 = billingAddress_3;

                                            shippingPostalCode = billingPostalCode;
                                            shippingCity = billingCity;
                                            shippingState = billingState;
                                            shippingContact = billingContact;
                                        }
                                        else
                                        {
                                            shippingName = rowDBInfo[dalSPP.ShippingFullName].ToString();
                                            shippingAddress_1 = rowDBInfo[dalSPP.Address1].ToString();
                                            shippingAddress_2 = rowDBInfo[dalSPP.Address2].ToString();
                                            shippingAddress_3 = rowDBInfo[dalSPP.Address3].ToString();

                                            shippingPostalCode = rowDBInfo[dalSPP.AddressPostalCode].ToString();
                                            shippingCity = rowDBInfo[dalSPP.AddressCity].ToString();
                                            shippingState = rowDBInfo[dalSPP.AddressState].ToString();
                                            shippingContact = rowDBInfo[dalSPP.Phone1].ToString();
                                        }
                                        #endregion

                                        #region Getting item Info/Size

                                        string type = rowDBInfo[dalSPP.TypeName].ToString();

                                        string size = rowDBInfo[dalSPP.SizeNumerator].ToString();
                                        string itemCode = rowDBInfo[dalSPP.ItemCode].ToString();

                                        int numerator = int.TryParse(rowDBInfo[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                                        int denominator = int.TryParse(rowDBInfo[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                                        string sizeUnit = rowDBInfo[dalSPP.SizeUnit].ToString().ToUpper();

                                        if (sizeUnit.ToUpper() == "IN")
                                        {
                                            sizeUnit = "\"";
                                        }

                                        int numerator_2 = int.TryParse(rowDBInfo[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                                        int denominator_2 = int.TryParse(rowDBInfo[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                                        string sizeUnit_2 = rowDBInfo[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                                        if (sizeUnit_2.ToUpper() == "IN")
                                        {
                                            sizeUnit_2 = "\"";
                                        }

                                        string sizeString_WithUnit = "";
                                        string sizeString_1_WithUnit = "";
                                        string sizeString_2_WithUnit = "";

                                        string sizeString_1 = "";
                                        string sizeString_2 = "";

                                        int size_1 = 1;
                                        int size_2 = 1;

                                        if (denominator == 1)
                                        {
                                            size_1 = numerator;
                                            sizeString_1_WithUnit = numerator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator.ToString();

                                        }
                                        else
                                        {
                                            size_1 = numerator / denominator;
                                            sizeString_1_WithUnit = numerator + "/" + denominator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator + "/" + denominator;
                                        }

                                        if (numerator_2 > 0)
                                        {
                                            if (denominator_2 == 1)
                                            {
                                                sizeString_2_WithUnit += numerator_2 + " " + sizeUnit_2.ToUpper();
                                                sizeString_2 += " " + numerator_2.ToString();
                                                size_2 = numerator_2;

                                            }
                                            else
                                            {
                                                size_2 = numerator_2 / denominator_2;

                                                if (numerator_2 == 3 && denominator_2 == 2)
                                                {
                                                    sizeString_2_WithUnit += "1 1" + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + "1 1" + "/" + denominator_2;
                                                }
                                                else
                                                {
                                                    sizeString_2_WithUnit += numerator_2 + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + numerator_2 + "/" + denominator_2;
                                                }



                                            }
                                        }

                                        if (size_1 >= size_2)
                                        {
                                            if (sizeString_2_WithUnit != "")
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit + " x " + sizeString_2_WithUnit;
                                            }


                                            else
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit;
                                            }
                                        }
                                        else
                                        {

                                            if (sizeString_2_WithUnit != "")
                                                sizeString_WithUnit = sizeString_2_WithUnit + " x " + sizeString_1_WithUnit;

                                            else
                                            {
                                                sizeString_WithUnit = sizeString_2_WithUnit;
                                            }
                                        }

                                        #endregion

                                        #region Checking PO No

                                        string currentPONo = rowDBInfo[dalSPP.PONo].ToString();

                                        if (previousPO != currentPONo)
                                        {
                                            if (previousPO != "")
                                            {
                                                //rowNo++;
                                            }

                                            previousPO = currentPONo;
                                        }

                                        #endregion

                                        //check if item list full 
                                        if (rowNo + 1 > maxRow)
                                        {
                                            #region clear previous page total amount and notes

                                            Range DOFormat = xlWorkSheet.get_Range("a38:w45").Cells;
                                            ExcelMergeandAlign(xlWorkSheet, "a38:w45", alignLeft, XlVAlign.xlVAlignTop);
                                            DOFormat.Merge();
                                            DOFormat.Value = "\nNext Page";
                                            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
                                            DOFormat.Font.Size = 10;
                                            DOFormat.Font.Name = "Cambria";
                                            DOFormat.Font.Bold = false;
                                            DOFormat.Font.Italic = true;

                                            #endregion

                                            rowNo = 1;

                                            pageNo++;
                                            //create new sheet
                                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                            if (pageNo > 1)
                                                xlWorkSheet.Name = "INV_" + DONoString + "_" + pageNo;

                                            NewExcelPageSetup(xlWorkSheet);

                                            //if (PONo == Text_MultiPOCode)
                                            //{
                                            //    CopyFormat(initialSheet_MultiPO, xlWorkSheet);
                                            //}
                                            //else
                                            //{
                                            //    CopyFormat(initialSheet_SinglePO, xlWorkSheet);
                                            //}

                                            //InitialInvoiceFormat_LOGO(xlWorkSheet);

                                            InitialInvoiceFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);

                                            InsertToSheet(xlWorkSheet, areaDONoData, "INV" + DONoString);
                                            InsertToSheet(xlWorkSheet, "t12:w12", DONoString);

                                            InsertToSheet(xlWorkSheet, areaPONoData, PONo == Text_MultiPOCode ? "REFER BELOW" : PONo);
                                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);

                                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());
                                            InsertToSheet(xlWorkSheet, areaTermData, GetTerms(customerCode));

                                            Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets["INV_" + DONoString];

                                            InsertToSheet(previousSheet, areaPageData, 1 + " of " + pageNo);


                                            if (string.IsNullOrEmpty(billingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine4, billingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine5, billingState);
                                            }

                                            if (string.IsNullOrEmpty(shippingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                            }

                                            InsertToSheet(previousSheet, areaBillName, billingName);
                                            InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                            InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                            InsertToSheet(previousSheet, areaBillTel, billingContact);

                                            InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                            InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                            InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                            InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);

                                            //change total page number in previous sheet(s)
                                            for (int i = 2; i < pageNo; i++)
                                            {
                                                previousSheet = (Worksheet)xlWorkBook.Worksheets["INV_" + DONoString + "_" + i];
                                                //Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets["INV_" + DONoString];

                                                InsertToSheet(previousSheet, areaPageData, i + " of " + pageNo);


                                                if (string.IsNullOrEmpty(billingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine5, billingState);
                                                }

                                                if (string.IsNullOrEmpty(shippingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                                }


                                                InsertToSheet(previousSheet, areaBillName, billingName);
                                                InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                                InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                                InsertToSheet(previousSheet, areaBillTel, billingContact);

                                                InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                                InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                                InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                                InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);

                                            }

                                            sheetNo++;

                                        }
                                        else
                                        {
                                            rowNo++;
                                        }

                                        #region Data Insert

                                        //index
                                        string RowToInsert = itemListColStart + (itemRowOffset + rowNo).ToString() + itemListColEnd + (itemRowOffset + rowNo).ToString();
                                        RowToInsert = indexColStart + (itemRowOffset + rowNo).ToString() + indexColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, indexNo);
                                        indexNo++;

                                        //po data
                                        if (PONo == Text_MultiPOCode)
                                        {
                                            RowToInsert = poColStart + (itemRowOffset + rowNo).ToString() + poColEnd + (itemRowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, RowToInsert, currentPONo);
                                        }


                                        //product decription
                                        string type_ShortName = GetTypeShortName(type);
                                        string newItemCode = text.SBB_BrandName + type_ShortName + " " + sizeString_1 + sizeString_2;
                                        string ProductDescription = "(" + newItemCode + ") " + sizeString_WithUnit + " " + type;
                                        RowToInsert = DescriptionColStart + (itemRowOffset + rowNo).ToString() + DescriptionColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, ProductDescription);

                                        //delivery qty
                                        RowToInsert = qtyColStart + (itemRowOffset + rowNo).ToString() + qtyColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, deliveryQty);

                                        //unit
                                        RowToInsert = uomColStart + (itemRowOffset + rowNo).ToString() + uomColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, "PCS");

                                        //unit price
                                        decimal unitPrice = decimal.TryParse(rowDBInfo[dalSPP.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                        RowToInsert = "q" + (itemRowOffset + rowNo).ToString() + ":r" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, unitPrice.ToString("N2"));

                                        //discount rate
                                        decimal DiscRate = decimal.TryParse(rowDBInfo[dalSPP.DiscountRate].ToString(), out DiscRate) ? DiscRate : 0;
                                        RowToInsert = "s" + (itemRowOffset + rowNo).ToString() + ":t" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, DiscRate.ToString("N2"));

                                        //net amount
                                        //decimal netAmount = deliveryQty * unitPrice * (100 - DiscRate) / 100;
                                        decimal netAmount = Math.Round(deliveryQty * unitPrice * (100 - DiscRate) / 100, 2);

                                        RowToInsert = "u" + (itemRowOffset + rowNo).ToString() + ":w" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, netAmount.ToString("N2"));

                                        //total amount
                                        totalAmount += netAmount;

                                        InsertToSheet(xlWorkSheet, areaTotalAmountText, ConvertAmountToWords(Math.Round(totalAmount, 2)));
                                        InsertToSheet(xlWorkSheet, areaTotalAmount, totalAmount.ToString("N2"));

                                        #endregion
                                    }

                                }
                            }

                            #region insert Billing/Shipping Address
                            if (string.IsNullOrEmpty(billingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingAddress_3);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine5, billingState);
                            }

                            if (string.IsNullOrEmpty(shippingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingAddress_3);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine5, shippingState);

                            }

                            InsertToSheet(xlWorkSheet, areaBillName, billingName);
                            InsertToSheet(xlWorkSheet, areaBillLine1, billingAddress_1);
                            InsertToSheet(xlWorkSheet, areaBillLine2, billingAddress_2);

                            InsertToSheet(xlWorkSheet, areaBillTel, billingContact);

                            InsertToSheet(xlWorkSheet, areaDeliveryName, shippingName);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine1, shippingAddress_1);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine2, shippingAddress_2);

                            InsertToSheet(xlWorkSheet, areaDeliveryTel, shippingContact);
                            #endregion
                            //^^264ms

                            pageNo = 1;
                            rowNo = 0;
                        }
                    }

                    #region Open File

                    if (sheetNo > 0)
                    {
                        //tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));

                        #region Excel/PDF Save and open

                        if (EXCEL_TYPE)
                        {
                            //Export to EXCEL
                            xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                           misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        }
                        else if (PDF_TYPE)
                        {
                            // Export to PDF
                            // Determine the PDF file path (assuming you want to save the PDF in the same location as the Excel file)
                            string pdfFilePath = Path.ChangeExtension(sfd.FileName, ".pdf");
                            xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFilePath,
                                                           XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                        }


                        if (File.Exists(sfd.FileName))
                        {
                            if (openFile)
                            {

                                System.Diagnostics.Process.Start(sfd.FileName);
                            }
                            else
                            {
                                MessageBox.Show("Invoice export successful!");
                            }
                        }

                        Clipboard.Clear();
                        dgv.ClearSelection();

                        frmLoading.CloseForm();
                        Focus();

                        if (EXCEL_TYPE)
                        {
                            xlexcel.DisplayAlerts = true;
                        }

                        xlWorkBook.Close(false, misValue, misValue);

                        xlexcel.Quit();

                        releaseObject(xlWorkSheet);

                        releaseObject(xlWorkBook);
                        releaseObject(xlexcel);

                        #endregion

                    }


                    #endregion
                }

            }


            DOListUI();
            lblMainList.Text = "D/O List";

            dt_DOList.DefaultView.Sort = header_DONo + " DESC";
            dt_DOList = dt_DOList.DefaultView.ToTable();

            #endregion
        }


        private void Old_ExportSBBInvoice_SingleFile()
        {
            #region Excel marking

            alignLeft = XlHAlign.xlHAlignLeft;
            alignRight = XlHAlign.xlHAlignRight;
            alignVCenter = XlVAlign.xlVAlignCenter;

            Color BorderColor = Color.Black;

            itemRowOffset = 17;
            maxRow = 20;

            sheetWidth = "a1:w1";
            wholeSheetArea = "a1:w45";

            #region Letterhead: Company Info

            rowCompanyNameCN = "a1:w1";
            rowCompanyNameEN = "a2:w2";
            rowSyarikatNo = "a3:w3";
            rowCompanyAddress = "a4:w4";
            rowSSTRegNo = "a5:w5";
            rowDocumentVersionControl = "a6:w6";

            #endregion

            #region Invoice Info

            areaDOInfo = "a7:w16";
            rowDoInfoStart = "a7:w7";
            areaBillTo = "a7:g7";
            areaDeliverTo = "i7:o7";

            rowDeliveryOrder = "q7:w8";

            areaBillName = "a8:g8";
            areaBillLine1 = "a9:g9";
            areaBillLine2 = "a10:g10";
            areaBillLine3 = "a11:g11";
            areaBillLine4 = "a12:g12";
            areaBillLine5 = "a13:g13";
            areaBillTel = "a14:g14";

            areaDeliveryName = "i8:o8";
            areaDeliveryLine1 = "i9:o9";
            areaDeliveryLine2 = "i10:o10";
            areaDeliveryLine3 = "i11:o11";
            areaDeliveryLine4 = "i12:o12";
            areaDeliveryLine5 = "i13:o13";
            areaDeliveryTel = "i14:o14";

            areaDONo = "q9:s9";
            areaDate = "q10:s10";
            areaPONo = "q11:s11";
            areaPage = "q15:s15";

            areaDONoData = "t9:w9";
            areaDateData = "t10:w10";
            areaPONoData = "t11:w11";
            areaPageData = "t15:w15";

            #endregion

            #region Item List

            rowListTitle = "a17:w17";
            areaItemList = "a18:w37";

            areaIndex = "a17:a17";
            areaPO = "b17:d17";
            areaDescription = "e17:l17";
            areaQuantity = "m17:n17";
            areaUOM = "o17:p17";

            itemListColStart = "a";
            itemListColEnd = ":w";

            indexColStart = "a";
            indexColEnd = ":a";

            DescriptionColStart = "e";
            DescriptionColEnd = ":l";

            qtyColStart = "m";
            qtyColEnd = ":n";

            uomColStart = "o";
            uomColEnd = ":p";

            poColStart = "b";
            poColEnd = ":d";

            rowItemListLastRow = "a37:w37";

            #endregion

            #region Sign & Chop

            areaSafetySign = "a39:m45";
            areaSafetyCompanyName = "a39:m39";
            areaIssuedBy = "b45:d45";
            areaPreparedBy = "f45:h45";
            areaDriver = "j45:l45";
            areaGoodsCheckedandReceivedBy = "p39:w39";
            areaCustomerChopandSign = "p45:w45";

            #endregion


            #endregion

            #region Export Settings

            DateTime DODate = frmExportSetting.DODate;
            bool openFile = frmExportSetting.openFileAfterExport;
            bool printFile = frmExportSetting.printFileAfterExport;
            bool printPreview = frmExportSetting.printPreview;
            bool EXCEL_TYPE = frmExportSetting.EXCEL_TYPE;
            bool PDF_TYPE = frmExportSetting.PDF_TYPE;
            string DODate_String = DODate.ToString("dd/MM/yyyy");

            #endregion

            #region export

            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                #region Pre-Settings

                dt_DOList.DefaultView.Sort = header_DONo + " ASC";
                dt_DOList = dt_DOList.DefaultView.ToTable();

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

                string path = @"D:\StockAssistant\Document\SBB Invoice";

                if (myconnstrng == text.DB_Semenyih)
                {
                    //string folderName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");

                    path = @"\\ADMIN001\Admin Server\( ACCOUNT & FINANCE )\(SBB INVOICE)\";
                }

                Directory.CreateDirectory(path);

                Cursor = Cursors.Arrow; // change cursor to normal type
                object misValue = Missing.Value;

                string firstInvoiceFileName = "";
                string lastInvoiceFileName = "";
                Workbook xlWorkBook = null;
                Excel.Application xlexcel = null;

                xlexcel = new Excel.Application
                {
                    PrintCommunication = false,
                    ScreenUpdating = false,
                    DisplayAlerts = false
                };
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlexcel.StandardFont = "Calibri";
                xlexcel.StandardFontSize = 12;
                xlexcel.Calculation = XlCalculation.xlCalculationManual;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = path;

                int sheetNo = 0;
                int pageNo = 1;
                int rowNo = 0;
                int invoiceCount = 0;

                Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;


                DateTime fromDate = DateTime.MaxValue;
                DateTime toDate = DateTime.MinValue;

                #endregion
                //^^1000ms

                #region List selected check and file name setting
                foreach (DataRow rowDOList in dt_DOList.Rows)
                {
                    bool selected = bool.TryParse(rowDOList[header_Selected].ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        string deliveredDate = rowDOList[header_DeliveredDate].ToString();

                        DateTime Date = DateTime.TryParse(deliveredDate, out DateTime test) ? test : DateTime.MinValue;


                        if (fromDate > Date)
                        {
                            fromDate = Date;
                        }

                        if (Date > toDate)
                        {
                            toDate = Date;
                        }

                        string DONoString = rowDOList[header_DONoString].ToString();
                        invoiceCount++;
                        if (string.IsNullOrEmpty(firstInvoiceFileName))
                        {
                            firstInvoiceFileName = "INV_" + DONoString;
                        }
                        else
                        {
                            lastInvoiceFileName = "INV_" + DONoString;
                        }
                        //string PONo = rowDOList[header_PONo].ToString();
                        //if (PONo == Text_MultiPOCode)
                        //{
                        //    MultiPOFound = true;
                        //}
                    }
                }

                string finalFileName = firstInvoiceFileName + " - " + lastInvoiceFileName;

                if (firstInvoiceFileName == lastInvoiceFileName || string.IsNullOrEmpty(lastInvoiceFileName))
                {
                    finalFileName = firstInvoiceFileName;
                }

                if (invoiceCount > 1)
                    finalFileName += " (" + invoiceCount + ")";

                string fullPath = Path.Combine(path, finalFileName);


                if (fromDate == DateTime.MaxValue)
                {
                    fromDate = toDate;
                }

                if (fromDate > toDate)
                {
                    DateTime dateTmp = fromDate;

                    toDate = dateTmp;
                }

                string MonthFrom = fromDate.ToString("MMMyy");
                string MonthTo = toDate.ToString("MMMyy");

                string Month = MonthFrom;

                if (MonthFrom != MonthTo)
                {
                    Month = MonthFrom + "-" + MonthTo;
                }

                if (PDF_TYPE)
                {
                    sfd.Filter = "Pdf Documents (*.pdf)|*.pdf";
                    sfd.FileName = "SBB " + Month + " " + finalFileName + ".pdf";
                }
                else
                {
                    sfd.Filter = "Pdf Documents (*.xls)|*.xls";
                    sfd.FileName = "SBB " + Month + " " + finalFileName + ".xls";
                }

                #endregion
                //^^786ms

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    DataTable dt_DOInfo = DB_DO_INFO.Copy();
                    dt_DOInfo.DefaultView.Sort = dalSPP.PONo + " ASC";
                    dt_DOInfo = dt_DOInfo.DefaultView.ToTable();

                    foreach (DataRow rowDOList in dt_DOList.Rows)
                    {
                        bool selected = bool.TryParse(rowDOList[header_Selected].ToString(), out selected) ? selected : false;

                        //export selected DO only
                        if (selected)
                        {
                            #region getting basic info

                            string deliveredDate = rowDOList[header_DeliveredDate].ToString();

                            DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate;
                            DODate_String = DODate.ToString("dd/MM/yyyy");

                            string customerName = rowDOList[header_Customer].ToString();
                            string POTableCode = rowDOList[header_POCode].ToString();
                            string PONo = rowDOList[header_PONo].ToString();
                            string DONo = rowDOList[header_DONo].ToString();
                            string DONoString = rowDOList[header_DONoString].ToString();

                            string customerCode = rowDOList[header_CustomerCode].ToString();

                            #endregion

                            #region Excel Settings

                            frmLoading.ShowLoadingScreen();

                            if (sheetNo > 0)
                            {
                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                            }
                            sheetNo++;
                            xlWorkSheet.Name = "INV_" + DONoString;

                            var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

                            int printerIndex = 0;

                            foreach (String s in printers)
                            {
                                if (s.Equals("RICOH MP C3503"))
                                {
                                    break;
                                }
                                printerIndex++;
                            }

                            xlexcel.PrintCommunication = true;
                            NewExcelPageSetup(xlWorkSheet);//346ms
                            xlexcel.PrintCommunication = true;
                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            //if(PONo == Text_MultiPOCode)
                            //{
                            //    CopyFormat(initialSheet_MultiPO, xlWorkSheet);
                            //}
                            //else
                            //{
                            //    CopyFormat(initialSheet_SinglePO, xlWorkSheet);
                            //}

                            //InitialInvoiceFormat_LOGO(xlWorkSheet);

                            InitialInvoiceFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);//2194ms

                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);
                            InsertToSheet(xlWorkSheet, areaDONoData, "INV" + DONoString);
                            InsertToSheet(xlWorkSheet, "t12:w12", DONoString);

                            //to do: check if more than one PO
                            InsertToSheet(xlWorkSheet, areaPONoData, PONo == Text_MultiPOCode ? "REFER BELOW" : PONo);
                            //InsertToSheet(xlWorkSheet, areaPONoData, "REFER BELOW");

                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());

                            InsertToSheet(xlWorkSheet, areaTermData, GetTerms(customerCode));


                            string billingName = null;
                            string billingAddress_1 = null;
                            string billingAddress_2 = null;
                            string billingAddress_3 = null;
                            string billingPostalCode = null;
                            string billingCity = null;
                            string billingState = null;
                            string billingContact = null;

                            string shippingName = null;
                            string shippingAddress_1 = null;
                            string shippingAddress_2 = null;
                            string shippingAddress_3 = null;
                            string shippingPostalCode = null;
                            string shippingCity = null;
                            string shippingState = null;
                            string shippingContact = null;

                            int indexNo = 1;
                            string previousPO = "";

                            decimal totalAmount = 0;

                            #endregion
                            //^^2585ms->540ms->242ms

                            foreach (DataRow rowDBInfo in dt_DOInfo.Rows)
                            {
                                if (DONo == rowDBInfo[dalSPP.DONo].ToString() && rowDBInfo[dalSPP.DONo] != DBNull.Value)
                                {
                                    int deliveryQty = rowDBInfo[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(rowDBInfo[dalSPP.ToDeliveryQty].ToString());

                                    if (deliveryQty > 0)
                                    {
                                        #region Billing/Shipping Address

                                        bool useBillingAddress = bool.TryParse(rowDBInfo[dalSPP.DefaultShippingAddress].ToString(), out useBillingAddress) ? useBillingAddress : false;

                                        billingName = rowDBInfo[dalSPP.FullName].ToString();
                                        billingAddress_1 = rowDBInfo[dalSPP.Address1 + "1"].ToString();
                                        billingAddress_2 = rowDBInfo[dalSPP.Address2 + "1"].ToString();
                                        billingAddress_3 = rowDBInfo[dalSPP.Address3 + "1"].ToString();

                                        billingPostalCode = rowDBInfo[dalSPP.AddressPostalCode + "1"].ToString();
                                        billingCity = rowDBInfo[dalSPP.AddressCity + "1"].ToString();
                                        billingState = rowDBInfo[dalSPP.AddressState + "1"].ToString();
                                        billingContact = rowDBInfo[dalSPP.Phone1 + "1"].ToString();


                                        if (useBillingAddress)
                                        {
                                            shippingName = billingName;
                                            shippingAddress_1 = billingAddress_1;
                                            shippingAddress_2 = billingAddress_2;
                                            shippingAddress_3 = billingAddress_3;

                                            shippingPostalCode = billingPostalCode;
                                            shippingCity = billingCity;
                                            shippingState = billingState;
                                            shippingContact = billingContact;
                                        }
                                        else
                                        {
                                            shippingName = rowDBInfo[dalSPP.ShippingFullName].ToString();
                                            shippingAddress_1 = rowDBInfo[dalSPP.Address1].ToString();
                                            shippingAddress_2 = rowDBInfo[dalSPP.Address2].ToString();
                                            shippingAddress_3 = rowDBInfo[dalSPP.Address3].ToString();

                                            shippingPostalCode = rowDBInfo[dalSPP.AddressPostalCode].ToString();
                                            shippingCity = rowDBInfo[dalSPP.AddressCity].ToString();
                                            shippingState = rowDBInfo[dalSPP.AddressState].ToString();
                                            shippingContact = rowDBInfo[dalSPP.Phone1].ToString();
                                        }
                                        #endregion

                                        #region Getting item Info/Size

                                        string type = rowDBInfo[dalSPP.TypeName].ToString();

                                        string size = rowDBInfo[dalSPP.SizeNumerator].ToString();
                                        string itemCode = rowDBInfo[dalSPP.ItemCode].ToString();

                                        int numerator = int.TryParse(rowDBInfo[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                                        int denominator = int.TryParse(rowDBInfo[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                                        string sizeUnit = rowDBInfo[dalSPP.SizeUnit].ToString().ToUpper();

                                        if (sizeUnit.ToUpper() == "IN")
                                        {
                                            sizeUnit = "\"";
                                        }

                                        int numerator_2 = int.TryParse(rowDBInfo[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                                        int denominator_2 = int.TryParse(rowDBInfo[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                                        string sizeUnit_2 = rowDBInfo[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                                        if (sizeUnit_2.ToUpper() == "IN")
                                        {
                                            sizeUnit_2 = "\"";
                                        }

                                        string sizeString_WithUnit = "";
                                        string sizeString_1_WithUnit = "";
                                        string sizeString_2_WithUnit = "";

                                        string sizeString_1 = "";
                                        string sizeString_2 = "";

                                        int size_1 = 1;
                                        int size_2 = 1;

                                        if (denominator == 1)
                                        {
                                            size_1 = numerator;
                                            sizeString_1_WithUnit = numerator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator.ToString();

                                        }
                                        else
                                        {
                                            size_1 = numerator / denominator;
                                            sizeString_1_WithUnit = numerator + "/" + denominator + " " + sizeUnit.ToUpper();
                                            sizeString_1 = numerator + "/" + denominator;
                                        }

                                        if (numerator_2 > 0)
                                        {
                                            if (denominator_2 == 1)
                                            {
                                                sizeString_2_WithUnit += numerator_2 + " " + sizeUnit_2.ToUpper();
                                                sizeString_2 += " " + numerator_2.ToString();
                                                size_2 = numerator_2;

                                            }
                                            else
                                            {
                                                size_2 = numerator_2 / denominator_2;

                                                if (numerator_2 == 3 && denominator_2 == 2)
                                                {
                                                    sizeString_2_WithUnit += "1 1" + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + "1 1" + "/" + denominator_2;
                                                }
                                                else
                                                {
                                                    sizeString_2_WithUnit += numerator_2 + "/" + denominator_2 + " " + sizeUnit_2.ToUpper();
                                                    sizeString_2 += " " + numerator_2 + "/" + denominator_2;
                                                }



                                            }
                                        }

                                        if (size_1 >= size_2)
                                        {
                                            if (sizeString_2_WithUnit != "")
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit + " x " + sizeString_2_WithUnit;
                                            }


                                            else
                                            {
                                                sizeString_WithUnit = sizeString_1_WithUnit;
                                            }
                                        }
                                        else
                                        {

                                            if (sizeString_2_WithUnit != "")
                                                sizeString_WithUnit = sizeString_2_WithUnit + " x " + sizeString_1_WithUnit;

                                            else
                                            {
                                                sizeString_WithUnit = sizeString_2_WithUnit;
                                            }
                                        }

                                        #endregion

                                        #region Checking PO No

                                        string currentPONo = rowDBInfo[dalSPP.PONo].ToString();

                                        if (previousPO != currentPONo)
                                        {
                                            if (previousPO != "")
                                            {
                                                //rowNo++;
                                            }

                                            previousPO = currentPONo;
                                        }

                                        #endregion

                                        //check if item list full 
                                        if (rowNo + 1 > maxRow)
                                        {
                                            #region clear previous page total amount and notes

                                            Range DOFormat = xlWorkSheet.get_Range("a38:w45").Cells;
                                            ExcelMergeandAlign(xlWorkSheet, "a38:w45", alignLeft, XlVAlign.xlVAlignTop);
                                            DOFormat.Merge();
                                            DOFormat.Value = "\nNext Page";
                                            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
                                            DOFormat.Font.Size = 10;
                                            DOFormat.Font.Name = "Cambria";
                                            DOFormat.Font.Bold = false;
                                            DOFormat.Font.Italic = true;

                                            #endregion

                                            rowNo = 1;

                                            pageNo++;
                                            //create new sheet
                                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                            if (pageNo > 1)
                                                xlWorkSheet.Name = "INV_" + DONoString + "_" + pageNo;

                                            NewExcelPageSetup(xlWorkSheet);

                                            //if (PONo == Text_MultiPOCode)
                                            //{
                                            //    CopyFormat(initialSheet_MultiPO, xlWorkSheet);
                                            //}
                                            //else
                                            //{
                                            //    CopyFormat(initialSheet_SinglePO, xlWorkSheet);
                                            //}

                                            //InitialInvoiceFormat_LOGO(xlWorkSheet);

                                            InitialInvoiceFormat(xlWorkSheet, PONo == Text_MultiPOCode ? true : false);

                                            InsertToSheet(xlWorkSheet, areaDONoData, "INV" + DONoString);
                                            InsertToSheet(xlWorkSheet, "t12:w12", DONoString);

                                            InsertToSheet(xlWorkSheet, areaPONoData, PONo == Text_MultiPOCode ? "REFER BELOW" : PONo);
                                            InsertToSheet(xlWorkSheet, areaPageData, pageNo + " of " + pageNo);

                                            InsertToSheet(xlWorkSheet, areaDateData, DODate.ToOADate());
                                            InsertToSheet(xlWorkSheet, areaTermData, GetTerms(customerCode));

                                            Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets["INV_" + DONoString];

                                            InsertToSheet(previousSheet, areaPageData, 1 + " of " + pageNo);


                                            if (string.IsNullOrEmpty(billingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine4, billingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                InsertToSheet(previousSheet, areaBillLine5, billingState);
                                            }

                                            if (string.IsNullOrEmpty(shippingAddress_3))
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                            }
                                            else
                                            {
                                                InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                            }

                                            InsertToSheet(previousSheet, areaBillName, billingName);
                                            InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                            InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                            InsertToSheet(previousSheet, areaBillTel, billingContact);

                                            InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                            InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                            InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                            InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);

                                            //change total page number in previous sheet(s)
                                            for (int i = 2; i < pageNo; i++)
                                            {
                                                previousSheet = (Worksheet)xlWorkBook.Worksheets["INV_" + DONoString + "_" + i];
                                                //Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets["INV_" + DONoString];

                                                InsertToSheet(previousSheet, areaPageData, i + " of " + pageNo);


                                                if (string.IsNullOrEmpty(billingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaBillLine3, billingAddress_3);
                                                    InsertToSheet(previousSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                                    InsertToSheet(previousSheet, areaBillLine5, billingState);
                                                }

                                                if (string.IsNullOrEmpty(shippingAddress_3))
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingState);
                                                }
                                                else
                                                {
                                                    InsertToSheet(previousSheet, areaDeliveryLine3, shippingAddress_3);
                                                    InsertToSheet(previousSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, areaDeliveryLine5, shippingState);

                                                }


                                                InsertToSheet(previousSheet, areaBillName, billingName);
                                                InsertToSheet(previousSheet, areaBillLine1, billingAddress_1);
                                                InsertToSheet(previousSheet, areaBillLine2, billingAddress_2);
                                                InsertToSheet(previousSheet, areaBillTel, billingContact);

                                                InsertToSheet(previousSheet, areaDeliveryName, shippingName);
                                                InsertToSheet(previousSheet, areaDeliveryLine1, shippingAddress_1);
                                                InsertToSheet(previousSheet, areaDeliveryLine2, shippingAddress_2);
                                                InsertToSheet(previousSheet, areaDeliveryTel, shippingContact);

                                            }

                                            sheetNo++;

                                        }
                                        else
                                        {
                                            rowNo++;
                                        }

                                        #region Data Insert

                                        //index
                                        string RowToInsert = itemListColStart + (itemRowOffset + rowNo).ToString() + itemListColEnd + (itemRowOffset + rowNo).ToString();
                                        RowToInsert = indexColStart + (itemRowOffset + rowNo).ToString() + indexColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, indexNo);
                                        indexNo++;

                                        //po data
                                        if (PONo == Text_MultiPOCode)
                                        {
                                            RowToInsert = poColStart + (itemRowOffset + rowNo).ToString() + poColEnd + (itemRowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, RowToInsert, currentPONo);
                                        }


                                        //product decription
                                        string type_ShortName = GetTypeShortName(type);
                                        string newItemCode = text.SBB_BrandName + type_ShortName + " " + sizeString_1 + sizeString_2;
                                        string ProductDescription = "(" + newItemCode + ") " + sizeString_WithUnit + " " + type;
                                        RowToInsert = DescriptionColStart + (itemRowOffset + rowNo).ToString() + DescriptionColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, ProductDescription);

                                        //delivery qty
                                        RowToInsert = qtyColStart + (itemRowOffset + rowNo).ToString() + qtyColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, deliveryQty);

                                        //unit
                                        RowToInsert = uomColStart + (itemRowOffset + rowNo).ToString() + uomColEnd + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, "PCS");

                                        //unit price
                                        decimal unitPrice = decimal.TryParse(rowDBInfo[dalSPP.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                        RowToInsert = "q" + (itemRowOffset + rowNo).ToString() + ":r" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, unitPrice.ToString("N2"));

                                        //discount rate
                                        decimal DiscRate = decimal.TryParse(rowDBInfo[dalSPP.DiscountRate].ToString(), out DiscRate) ? DiscRate : 0;
                                        RowToInsert = "s" + (itemRowOffset + rowNo).ToString() + ":t" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, DiscRate.ToString("N2"));

                                        //net amount
                                        //decimal netAmount = deliveryQty * unitPrice * (100 - DiscRate) / 100;
                                        decimal netAmount = Math.Round(deliveryQty * unitPrice * (100 - DiscRate) / 100, 2);
                                        RowToInsert = "u" + (itemRowOffset + rowNo).ToString() + ":w" + (itemRowOffset + rowNo).ToString();
                                        InsertToSheet(xlWorkSheet, RowToInsert, netAmount.ToString("N2"));

                                        //total amount
                                        totalAmount += netAmount;

                                        InsertToSheet(xlWorkSheet, areaTotalAmountText, ConvertAmountToWords(Math.Round(totalAmount, 2)));
                                        InsertToSheet(xlWorkSheet, areaTotalAmount, totalAmount.ToString("N2"));

                                        #endregion
                                    }

                                }
                            }

                            #region insert Billing/Shipping Address
                            if (string.IsNullOrEmpty(billingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaBillLine3, billingAddress_3);
                                InsertToSheet(xlWorkSheet, areaBillLine4, billingPostalCode + " " + billingCity);
                                InsertToSheet(xlWorkSheet, areaBillLine5, billingState);
                            }

                            if (string.IsNullOrEmpty(shippingAddress_3))
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingState);
                            }
                            else
                            {
                                InsertToSheet(xlWorkSheet, areaDeliveryLine3, shippingAddress_3);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine4, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, areaDeliveryLine5, shippingState);

                            }

                            InsertToSheet(xlWorkSheet, areaBillName, billingName);
                            InsertToSheet(xlWorkSheet, areaBillLine1, billingAddress_1);
                            InsertToSheet(xlWorkSheet, areaBillLine2, billingAddress_2);

                            InsertToSheet(xlWorkSheet, areaBillTel, billingContact);

                            InsertToSheet(xlWorkSheet, areaDeliveryName, shippingName);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine1, shippingAddress_1);
                            InsertToSheet(xlWorkSheet, areaDeliveryLine2, shippingAddress_2);

                            InsertToSheet(xlWorkSheet, areaDeliveryTel, shippingContact);
                            #endregion
                            //^^264ms

                            pageNo = 1;
                            rowNo = 0;
                        }
                    }

                    #region Open File

                    if (sheetNo > 0)
                    {
                        //tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));

                        #region Excel/PDF Save and open

                        if (EXCEL_TYPE)
                        {
                            //Export to EXCEL
                            xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                           misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        }
                        else if (PDF_TYPE)
                        {
                            // Export to PDF
                            // Determine the PDF file path (assuming you want to save the PDF in the same location as the Excel file)
                            string pdfFilePath = Path.ChangeExtension(sfd.FileName, ".pdf");
                            xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFilePath,
                                                           XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                        }


                        if (File.Exists(sfd.FileName))
                        {
                            if (openFile)
                            {

                                System.Diagnostics.Process.Start(sfd.FileName);
                            }
                            else
                            {
                                MessageBox.Show("Invoice export successful!");
                            }
                        }

                        Clipboard.Clear();
                        dgv.ClearSelection();

                        frmLoading.CloseForm();
                        Focus();

                        if (EXCEL_TYPE)
                        {
                            xlexcel.DisplayAlerts = true;
                        }

                        xlWorkBook.Close(false, misValue, misValue);

                        xlexcel.Quit();

                        releaseObject(xlWorkSheet);

                        releaseObject(xlWorkBook);
                        releaseObject(xlexcel);

                        #endregion

                    }


                    #endregion
                }

            }


            DOListUI();
            lblMainList.Text = "D/O List";

            dt_DOList.DefaultView.Sort = header_DONo + " DESC";
            dt_DOList = dt_DOList.DefaultView.ToTable();

            #endregion
        }


        private string ConvertAmountToWords(decimal amount)
        {
            string[] unitsMap = { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            string[] tensMap = { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            string NumberToWords(int number)
            {
                if (number == 0)
                    return "";
                if (number < 20)
                    return unitsMap[number];
                if (number < 100)
                    return tensMap[number / 10] + (number % 10 > 0 ? " " + NumberToWords(number % 10) : "");
                if (number < 1000)
                    return unitsMap[number / 100] + " HUNDRED" + (number % 100 > 0 ? " " + NumberToWords(number % 100) : "");
                if (number < 1000000)
                    return NumberToWords(number / 1000) + " THOUSAND " + (number % 1000 > 0 ? " " + NumberToWords(number % 1000) : "");
                if (number < 1000000000)
                    return NumberToWords(number / 1000000) + " MILLION " + (number % 1000000 > 0 ? " " + NumberToWords(number % 1000000) : "");
                return NumberToWords(number / 1000000000) + " BILLION " + (number % 1000000000 > 0 ? " " + NumberToWords(number % 1000000000) : "");
            }

            if (amount == 0)
                return "RINGGIT MALAYSIA: ZERO ONLY";

            int ringgit = (int)amount;
            int cents = (int)((amount - ringgit) * 100);

            string ringgitWords = NumberToWords(ringgit).Trim();
            string centsWords = cents > 0 ? " AND CENTS " + NumberToWords(cents).Trim() : "";

            return "RINGGIT MALAYSIA: " + ringgitWords + centsWords + " ONLY";
        }


        #endregion

        private void SetDateTimePickersToPreviousMonth()
        {
            DateTime today = dtpRangeStart.Value;

            DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfPreviousMonth = firstDayOfCurrentMonth.AddDays(-1);
            DateTime firstDayOfPreviousMonth = new DateTime(lastDayOfPreviousMonth.Year, lastDayOfPreviousMonth.Month, 1);

            dtpRangeStart.Value = firstDayOfPreviousMonth;
            dtpRangeEnd.Value = lastDayOfPreviousMonth;
        }

        private void SetDateTimePickersToNextMonth()
        {
            DateTime today = dtpRangeStart.Value;

            DateTime firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
            DateTime lastDayOfNextMonth = firstDayOfNextMonth.AddMonths(1).AddDays(-1);

            dtpRangeStart.Value = firstDayOfNextMonth;
            dtpRangeEnd.Value = lastDayOfNextMonth;
        }

        private bool ISSUE_E_INVOICE_MODE = false;

        private void GenerateInvoice()
        {
            //password
            frmVerification frmPW = new frmVerification(text.PW_Level_1)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frmPW.ShowDialog();

            if (frmVerification.PASSWORD_MATCHED)
            {
                ISSUE_E_INVOICE_MODE = true;
               
                if(btnExport.Text != text_Export)
                {
                    DATA_LOADED = false;

                    DOSelectingUI();
                }
            }

           
        }

        private void OpenExportSettings()
        {

            if (cbInvoiceMode.Checked || ISSUE_E_INVOICE_MODE)
            {
                ISSUE_E_INVOICE_MODE = false;

                //show export setting
                frmExportSetting frm = new frmExportSetting(true)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                frm.ShowDialog();

                //get return data
                if (frmExportSetting.settingApplied)
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                    ExportSBBInvoice_SingleFile();

                }

                Cursor = Cursors.Arrow;

                DOListUI();

                DATA_LOADED = false;

            }
            else
            {
                //show export setting
                frmExportSetting frm = new frmExportSetting(false)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                frm.ShowDialog();

                //get return data
                if (frmExportSetting.settingApplied)
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                    ExportSBBDO();

                }

                Cursor = Cursors.Arrow;

                DOListUI();

                DATA_LOADED = false;
            }
           
        }

        private void copyAlltoClipboard()
        {
            //dgvNewStock.Columns.Remove(headerParentColor);
            //dgvNewStock.Columns.Remove(headerRepeat);
            dgvDOList.SelectAll();
            //dgvNewStock.sele
            DataObject dataObj = dgvDOList.GetClipboardContent();
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

        #endregion

        private void dgvDOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvDOList;

            if (rowIndex >= 0 && dgv.SelectedRows.Count > 0)
            {
                string tblName = dalSPP.DOTableName;
                string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();

                frmActionLog frm = new frmActionLog(tblName, code)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();
            }

        }

        private void ColorDONoByStatus()
        {
            DataGridView dgv = dgvDOList;
            dgv.SuspendLayout();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Columns[j].Name == header_DataType)
                    {
                        if (dgv.Rows[i].Cells[j].Value.ToString() == DataType_Delivered)
                        {
                            dgv.Rows[i].Cells[header_DONoString].Style.ForeColor = Color.Green;
                        }
                        else if (dgv.Rows[i].Cells[j].Value.ToString() == DataType_Removed)
                        {
                            dgv.Rows[i].Cells[header_DONoString].Style.ForeColor = Color.Red;

                        }
                        else
                        {
                            dgv.Rows[i].Cells[header_DONoString].Style.ForeColor = Color.Black;

                        }
                    }
                }
            }

            dgv.ResumeLayout();

            dgv.ClearSelection();
        }

        private void dgvDOList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorDONoByStatus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowFilter(false);
            ResetDBDOInfoList();
            LoadDOList();
        }

        private void frmSPPDOList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void frmSBBDOListVer2_Shown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            frmLoading.ShowLoadingScreen();
            ShowOrHideFilter();

            cmbCustomer.Text = "ALL";

            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-15);

            DateTime lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));


            dtpRangeStart.Value = firstDay;
            dtpRangeEnd.Value = lastDay;

            cbInvoiceMode.Checked = false;

            ResetDBDOInfoList();//3304ms
            LoadDOList();//65ms
            FormLoaded = true;

            //int id =  dalTrfHist.GetLastInsertedID();

            //MessageBox.Show(id.ToString());
            Cursor = Cursors.Arrow;
            frmLoading.CloseForm();
        }

        private void frmSBBDOListVer2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
           
          
        }

        private void dgvDOItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool DATE_RANGE_TYPE_CHANGING = false;

        private void cbDeliveredDate_CheckedChanged(object sender, EventArgs e)
        {
            if(!DATE_RANGE_TYPE_CHANGING)
            {
                DATE_RANGE_TYPE_CHANGING = true;
                if (cbDeliveredDate.Checked)
                {
                    cbPODate.Checked = !cbDeliveredDate.Checked;
                    cbDODate.Checked = !cbDeliveredDate.Checked;
                }
                else
                {
                    //MessageBox.Show("Please select ");
                    cbDeliveredDate.Checked = true;
                }

                DATE_RANGE_TYPE_CHANGING = false;

            }

        }

        private void cbDODate_CheckedChanged(object sender, EventArgs e)
        {
            if (!DATE_RANGE_TYPE_CHANGING)
            {
                DATE_RANGE_TYPE_CHANGING = true;

                if (cbDODate.Checked)
                {
                    cbPODate.Checked = !cbDODate.Checked;
                    cbDeliveredDate.Checked = !cbDODate.Checked;
                }
                else
                {
                    cbPODate.Checked = false;
                    cbDeliveredDate.Checked = true;
                }
                DATE_RANGE_TYPE_CHANGING = false;

            }
        }

        private void cbPODate_CheckedChanged(object sender, EventArgs e)
        {
            if (!DATE_RANGE_TYPE_CHANGING)
            {
                DATE_RANGE_TYPE_CHANGING = true;

                if (cbPODate.Checked)
                {
                    cbDODate.Checked = !cbPODate.Checked;
                    cbDeliveredDate.Checked = !cbPODate.Checked;
                }
                else
                {
                    cbDODate.Checked = false;
                    cbDeliveredDate.Checked = true;
                }
                DATE_RANGE_TYPE_CHANGING = false;

            }
        }

        private void cbInvoiceMode_CheckedChanged(object sender, EventArgs e)
        {
            cbInvoiceSortByCustomer.Checked = false;
            cbCustomerSummaryList.Checked = false;

            cbInvoiceSortByCustomer.Visible = cbInvoiceMode.Checked;
            //cbCustomerSummaryList.Visible = cbInvoiceMode.Checked;

            if (!cbInvoiceMode.Checked)
            {
                lblSubList.Text = text_DOItemList;

                cbInProgress.Enabled = true;
                cbRemoved.Enabled = true;
                cbCompleted.Enabled = true;

                cbDeliveredDate.Enabled = true;
                cbDODate.Enabled = true;
                cbPODate.Enabled = true;
            }
            else
            {
                cbDeliveredDate.Checked = true;
                cbInProgress.Checked = false;
                cbCompleted.Checked = true;
                cbRemoved.Checked = false;

                cbDeliveredDate.Enabled = false;
                cbDODate.Enabled = false;
                cbPODate.Enabled = false;

                cbInProgress.Enabled = false;
                cbRemoved.Enabled = false;
                cbCompleted.Enabled = false;

                DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
                dtpRangeStart.Value = firstDay;
                dtpRangeEnd.Value = lastDay;

            }
        }

        private void lblGrandTotalAmount_Click(object sender, EventArgs e)
        {
            //lblGrandTotalAmount.Text = 0.ToString();
            //GRAND_TOTAL_AMOUNT = 0;
        }

     

        private void UpdateDatabaseMonthsAgoNumber()
        {
            int orimonthsagodata = DATABASE_MONTHS_AGO_NUMBER;
            DateTime startDate = dtpRangeStart.Value;
            DateTime endDate = dtpRangeEnd.Value;

            int monthsDifference = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month + 1;

            DATABASE_MONTHS_AGO_NUMBER = Math.Max(6, monthsDifference);

            //if(DATABASE_MONTHS_AGO_NUMBER != orimonthsagodata)
            //{
            //    TO_RELOAD_DATA = true;
            //}
        }
        private void dtpRangeStart_ValueChanged(object sender, EventArgs e)
        {
            UpdateDatabaseMonthsAgoNumber();
        }

        private void lblSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvDOList.DataSource;

            if(dt.Columns.Contains(header_Selected))
            {
                foreach(DataRow row in dt.Rows)
                {
                    row[header_Selected] = true;

                }

                if(ifDOSelected())
                {
                    btnExport.Text = text_Export;
                    btnExport.Enabled = true;
                }
               
            }
           
           
        }

        private void dtpRangeEnd_ValueChanged(object sender, EventArgs e)
        {
            UpdateDatabaseMonthsAgoNumber();

        }

        private void lblPrevMonth_Click(object sender, EventArgs e)
        {
            SetDateTimePickersToPreviousMonth();
        }

        private void lblNextMonth_Click(object sender, EventArgs e)
        {
            SetDateTimePickersToNextMonth();
        }

        private void lblMainList_Click(object sender, EventArgs e)
        {
            if(userPermission >= 4)
            {
                GenerateSalesMasterList();
                CustomerSalesReportRanking();

                frmMasterList frm = new frmMasterList(DT_CUSTOMER_SALES_REPORT)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();
            }
        }

        private void dgvDOList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if this is the DO number column
            if (dgvDOList.Columns[e.ColumnIndex].Name == header_DONo && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int doNoInt))
                {
                    // Format for display without changing underlying data
                    e.Value = tool.FormatDONumber(doNoInt);
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
