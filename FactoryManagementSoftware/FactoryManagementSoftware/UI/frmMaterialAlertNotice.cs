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
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using Microsoft.Office.Interop.Word;
using Syncfusion.XlsIO.Parser.Biff_Records;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;
using Microsoft.Office.Core;
using System.Runtime.Remoting.Messaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using Range = Microsoft.Office.Interop.Excel.Range;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using iTextSharp.text;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMaterialAlertNotice : Form
    {
        public frmMaterialAlertNotice()
        {
            InitializeComponent();

            CURRENT_MONTH = DateTime.Now.Month;
            CURRENT_YEAR = DateTime.Now.Year;

            getStartandEndDate();
            NEGATIVE_BAL_FOUND = false;
            //GetSummaryForecastMatUsedData();
        }

        #region variable declare

        private int CURRENT_MONTH;
        private int CURRENT_YEAR;

        readonly string status_Received = "RECEIVED";
        readonly string status_Requesting = "REQUESTING";
        readonly string status_Pending = "PENDING";
        readonly string status_Cancelled = "CANCELLED";
        readonly string headerPONO = "P/O NO";
        readonly string headerID = "ID";
        readonly string headerDateRequired = "DATE REQUIRED";
        readonly string headerCat = "CATEGORY";
        readonly string headerOrdered = "ORDERED";
        readonly string headerPending = "PENDING / OVER";
        readonly string headerReceived = "RECEIVED";
        readonly string headerUnit = "UNIT";
        readonly string headerStatus = "STATUS";
        readonly string headerType = "TYPE";
        readonly string headerCode = "CODE";
        readonly string headerName = "NAME";

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";
        private string header_Forecast = "FORECAST";
        private string header_TotalOut = "TOTAL";
        private string header_Stock = "STOCK";
        private string header_Bal = "BAL.";
        private string header_Unit = "UNIT";

        readonly string BTN_SHOW_MATERIAL_FOREACST = "SHOW MATERIAL FORECAST";
        readonly string BTN_HIDE_MATERIAL_FORECAST = "HIDE MATERIAL FORECAST";
        readonly string BTN_MATERIAL_FORECAST_ONLY = "MATERIAL FORECAST ONLY";
        readonly string BTN_SHOW_ORDER_RECORD = "SHOW ORDER RECORD";

        private int selectedOrderID = -1;
        static public string finalOrderNumber;
        static public string receivedNumber;
        static public string note;
        static public bool cancel;
        static public bool receivedReturn = false;
        static public bool orderApproved = false;
        static public bool zeroCost = false;
        static public bool orderCompleted = false;
        private int userPermission = -1;

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();
        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();

        custSupplierDAL dalCust = new custSupplierDAL();
        materialDAL dalMat = new materialDAL();
        dataTrfBLL uData = new dataTrfBLL();

        Tool tool = new Tool();

        Text text = new Text();

        DataTable dt_Join = new DataTable();
        DataTable dt_Item = new DataTable();
        DataTable dt_OrginalData = new DataTable();
        DataTable DT_PART_TRANSFER = new DataTable();

        private DataTable DT_PRODUCT_FORECAST_SUMMARY;
        private DataTable DT_MATERIAL_FORECAST_SUMMARY;
        private DataTable DT_STOCK_LIST;
        private DataTable dt_ItemForecast;

        // private DataTable dt_MatUsed_Forecast;
        //private DataTable dt_DeliveredData;
        bool loaded = false;
        #endregion

        #region UI Design

        private DataTable Product_SummaryDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Customer, typeof(string));
            dt.Columns.Add(text.Header_IndexMarking, typeof(int));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ParentIndex, typeof(int));
            dt.Columns.Add(text.Header_GroupLevel, typeof(int));
            dt.Columns.Add(text.Header_Type, typeof(string));
            dt.Columns.Add(text.Header_PartWeight_G, typeof(float));
            dt.Columns.Add(text.Header_RunnerWeight_G, typeof(float));
            dt.Columns.Add(text.Header_WastageAllowed_Percentage, typeof(float));
            dt.Columns.Add(text.Header_ColorRate, typeof(float));
            dt.Columns.Add(text.Header_JoinQty, typeof(float));
            dt.Columns.Add(text.Header_JoinMax, typeof(float));
            dt.Columns.Add(text.Header_JoinMin, typeof(float));
            dt.Columns.Add(text.Header_JoinWastage, typeof(float));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));
            dt.Columns.Add(text.Header_Unit, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(float));

  


            string month = CURRENT_MONTH + "/" + CURRENT_YEAR;

                dt.Columns.Add(month + text.str_Forecast, typeof(float));
                dt.Columns.Add(month + text.str_Delivered, typeof(float));
                dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
                dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));
                dt.Columns.Add(month + text.str_EstBalance, typeof(float));

            dt.Columns.Add(text.Header_PendingOrder, typeof(string));
            dt.Columns.Add(text.Header_PendingOrder_ZeroCost, typeof(string));
            dt.Columns.Add(text.Header_PendingOrder_Purchase, typeof(string));
            dt.Columns.Add(text.Header_Order_Requesting, typeof(string));

            return dt;
        }

        private DataTable Material_SummaryDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Type, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));

            

           

                string month = CURRENT_MONTH + "/" + CURRENT_YEAR;

                dt.Columns.Add(month + text.str_Forecast, typeof(float));
                dt.Columns.Add(month + text.str_Delivered, typeof(float));
                dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
                dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));
                dt.Columns.Add(month + text.str_EstBalance, typeof(float));
            

            dt.Columns.Add(text.Header_PendingOrder, typeof(string));
            dt.Columns.Add(text.Header_PendingOrder_ZeroCost, typeof(string));
            dt.Columns.Add(text.Header_PendingOrder_Purchase, typeof(string));
            dt.Columns.Add(text.Header_Order_Requesting, typeof(string));

            dt.Columns.Add(text.Header_Unit, typeof(string));

            return dt;
        }

        private DataTable New_StockList_DataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(float));


            string month = CURRENT_MONTH + "/" + CURRENT_YEAR;
            dt.Columns.Add(month + text.str_EstBalance, typeof(float));
                
            
            return dt;
        }

        private DataTable NewMatUsedTable()
        {
            DataTable dt = new DataTable();

            header_Forecast = CURRENT_MONTH + "/" + CURRENT_YEAR + text.str_Forecast;

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MatType, typeof(string));
            dt.Columns.Add(text.Header_MatCode, typeof(string));
            dt.Columns.Add(text.Header_MatName, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_Parent, typeof(string));
            dt.Columns.Add(header_Forecast, typeof(int));
            dt.Columns.Add(text.Header_ItemWeight_G, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsed_KG_Piece, typeof(float));
            dt.Columns.Add(text.Header_Wastage, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsedWithWastage, typeof(float));
            dt.Columns.Add(text.Header_TotalMaterialUsed_KG_Piece, typeof(float));


            return dt;
        }

        private DataTable SummaryForecastMatUsedTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MatType, typeof(string));
            dt.Columns.Add(text.Header_MatCode, typeof(string));
            dt.Columns.Add(text.Header_MatName, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_Parent, typeof(string));

            #region add Month Column
            // headerOutQty = cmbMonth.Text + "/" + cmbYear.Text + " FORECAST";



            #endregion

            string month = CURRENT_MONTH + "/" + CURRENT_YEAR;

            dt.Columns.Add(month + text.str_Forecast, typeof(float));

            dt.Columns.Add(text.Header_ItemWeight_G, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsed_KG_Piece, typeof(float));
            dt.Columns.Add(text.Header_Wastage, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsedWithWastage, typeof(float));
            dt.Columns.Add(text.Header_TotalMaterialUsed_KG_Piece, typeof(float));
            dt.Columns.Add(header_TotalOut, typeof(float));
            dt.Columns.Add(header_Stock, typeof(float));
            dt.Columns.Add(header_Bal, typeof(float));
            dt.Columns.Add(header_Unit, typeof(string));


            return dt;
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            if(dgv == dgvAlertSummary)
            {
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_PartCode].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                dgv.Columns[text.Header_Type].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.Columns[text.Header_ReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (DT_MATERIAL_FORECAST_SUMMARY != null)
                {
                    foreach(DataColumn col in DT_MATERIAL_FORECAST_SUMMARY.Columns)
                    {
                        string colName = col.ColumnName;

                        if(colName.Contains(text.str_Forecast) || colName.Contains(text.str_Delivered) || colName.Contains(text.str_RequiredQty) || colName.Contains(text.str_InsufficientQty) )
                        {
                            dgv.Columns[colName].Visible = false;
                        }
                        else if(colName.Contains(text.str_EstBalance))
                        {
                            dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }

                dgv.Columns[text.Header_PendingOrder].Visible = false;

                dgv.Columns[text.Header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //dgv.Columns[text.Header_PendingOrder].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_PendingOrder_Purchase].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_PendingOrder_ZeroCost].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_Order_Requesting].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_ReadyStock].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                //dgv.Columns[text.Header_PendingOrder].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                dgv.Columns[text.Header_PendingOrder_Purchase].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic | FontStyle.Bold);

                dgv.Columns[text.Header_PendingOrder_ZeroCost].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                dgv.Columns[text.Header_Order_Requesting].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[text.Header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_PartCode].Frozen = true;

                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            }
            
        }

        private void dgvMatUsedReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView dgv = dgvAlertSummary;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            string colName = dgv.Columns[col].Name;

            if (colName == text.Header_MatCode)
            {
                string matCode = dgv.Rows[row].Cells[text.Header_MatCode].Value.ToString();

                if (matCode == "")
                {
                    dgv.Rows[row].Height = 3;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[row].Height = 50;
                }
            }
            else if (colName.Contains(text.str_EstBalance))
            {
                float bal = float.TryParse(dgv.Rows[row].Cells[colName].Value.ToString(), out float x) ? x : 0;

                if (bal < 0)
                {
                    dgv.Rows[row].Cells[colName].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[colName].Style.ForeColor = Color.Black;

                }
            }
            dgv.ResumeLayout();
        }

        private void dgvMatUsedReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //dgvMatUsedUIEdit(dgvAlertSummary);
            //dgvAlertSummary.AutoResizeColumns();
        }

        #endregion


        //private bool ifCustomerItem(string itemCode, DataTable dt_Join, DataTable dt_CustItem)
        //{
        //    bool isCustomerItem = false;

        //    foreach (DataRow row in dt_CustItem.Rows)
        //    {
        //        if (itemCode == row[dalItem.ItemCode].ToString())
        //        {
        //            return true;
        //        }
        //    }

        //    foreach (DataRow join in dt_Join.Rows)
        //    {
        //        if (itemCode == join["child_code"].ToString())
        //        {
        //            string parentCode = join["parent_code"].ToString();
        //            isCustomerItem = ifCustomerItem(parentCode, dt_Join, dt_CustItem);
        //        }
        //    }

        //    return isCustomerItem;
        //}


        #region NEW Method
        private DataTable NEW_RearrangeIndex(DataTable dataTable)
        {
            int index = 1;

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    row[text.Header_Index] = index++;
                }
            }

            return dataTable;
        }
        private float DeliveredToCustomerQty(string custID, string itemCode, DateTime dateFrom, DateTime dateTo)
        {
            float DeliveredQty = 0;

            DT_PART_TRANSFER.AcceptChanges();

            bool itemFound = false;

            foreach (DataRow trfHistRow in DT_PART_TRANSFER.Rows)
            {
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();
                string custID_DB = trfHistRow[dalItemCust.CustID].ToString();

                DateTime trfDate = DateTime.TryParse(trfHistRow[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                bool dateMatched = trfDate >= dateFrom;
                dateMatched &= trfDate <= dateTo;

                if (itemFound && itemCode != trfHistRow[dalTrfHist.TrfItemCode].ToString())
                {
                    break;
                }

                if (custID_DB == custID && itemCode == trfHistRow[dalTrfHist.TrfItemCode].ToString() && result == text.Passed && dateMatched)
                {
                    itemFound = true;

                    int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                    DeliveredQty += qty;
                    trfHistRow.Delete();
                }
            }

            DT_PART_TRANSFER.AcceptChanges();

            return DeliveredQty;
        }

        private bool NEGATIVE_BAL_FOUND = false;

        private void New_PrintForecastSummary()
        {
            dgvAlertSummary.DataSource = null;

            if (DT_MATERIAL_FORECAST_SUMMARY != null)
            {
                //string Material_Type = cmbItemType.Text;

                DataTable dt_MaterialList = DT_MATERIAL_FORECAST_SUMMARY.Clone();

                string monthbalanceheader = CURRENT_MONTH + "/" + CURRENT_YEAR + text.str_EstBalance;

                foreach (DataRow row in DT_MATERIAL_FORECAST_SUMMARY.Rows)
                {
                    string matType = row[text.Header_Type].ToString();
                    string stock = row[text.Header_ReadyStock].ToString();
                    string itemCode = row[text.Header_PartCode].ToString();
                   
                    float balance = float.TryParse(row[monthbalanceheader].ToString(), out balance)? balance : 0;

                    if (!string.IsNullOrEmpty(matType) && matType  != text.Cat_Part && stock != "-1" && itemCode != "XTW4+10EEJ" && balance < 0)
                    {
                        dt_MaterialList.ImportRow(row);

                        NEGATIVE_BAL_FOUND = true;
                    }
                }

                if (dt_MaterialList.Rows.Count > 0 && NEGATIVE_BAL_FOUND)
                {

                    DataTable DB_PendingOrder = dalOrd.PendingOrderSelect();
                    DataTable DB_RequestingOrder = dalOrd.RequestingOrderSelect();

                    foreach (DataRow row in dt_MaterialList.Rows)
                    {
                        //update pending order qty

                        string itemCode = row[text.Header_PartCode].ToString();

                        float pendingOrder_ZeroCost = tool.GetZeroCostPendingOrder(DB_PendingOrder, itemCode);
                        float pendingOrder_Purchase = tool.GetPurchasePendingOrder(DB_PendingOrder, itemCode);

                        float totalOrder = pendingOrder_ZeroCost + pendingOrder_Purchase;

                        row[text.Header_PendingOrder_ZeroCost] = pendingOrder_ZeroCost;
                        row[text.Header_PendingOrder_Purchase] = pendingOrder_Purchase;

                        //float order_Requesting = tool.GetRequestingOrder(DB_PendingOrder, itemCode);
                        row[text.Header_Order_Requesting] = tool.GetRequestingOrder(DB_RequestingOrder, itemCode);


                        row[text.Header_PendingOrder] = pendingOrder_ZeroCost;

                        //if (true)//cbZeroStockType.Checked
                        //{
                        //    row[text.Header_PendingOrder] = pendingOrder_ZeroCost;
                        //}
                        //else
                        //{
                        //    row[text.Header_PendingOrder] = pendingOrder_Purchase;
                        //}


                        itemBLL uItem = new itemBLL();

                        uItem.item_code = itemCode;
                        uItem.item_updtd_date = DateTime.Now;
                        uItem.item_updtd_by = MainDashboard.USER_ID;
                        uItem.item_ord = totalOrder;

                        //Updating data into database
                       dalItem.ordUpdate(uItem);

                    }

                    dt_MaterialList.DefaultView.Sort = monthbalanceheader + " ASC";
                    dt_MaterialList = dt_MaterialList.DefaultView.ToTable();

                    dt_MaterialList = NEW_RearrangeIndex(dt_MaterialList);

                    this.BackColor = Color.Red;

                    dgvAlertSummary.BackgroundColor = Color.Yellow; 
                    dgvAlertSummary.DataSource = dt_MaterialList;
                    dgvUIEdit(dgvAlertSummary);

                }
                else
                {
                    Close();
                }
               
            }


            

        }

        private DataTable RemoveTerminatedProduct(DataTable dt_Product)
        {
            if(dt_Product != null)
            {
                dt_Product.AcceptChanges();
                foreach (DataRow row in dt_Product.Rows)
                {
                    string itemName = row[dalItem.ItemName].ToString();

                    if(itemName.ToUpper().Contains(text.Terminated.ToUpper()))
                    {
                        //remove item
                        row.Delete();
                    }
                }
                dt_Product.AcceptChanges();

            }
            return dt_Product;
        }

        private void GetChildFromGroup(DataTable dt_Join, DataTable dt_Item, string parentCode, int parentIndex, int groupLevel, int childIndex)
        {
            //search child

            foreach (DataRow join in dt_Join.Rows)
            {
                if (parentCode == join[dalJoin.ParentCode].ToString())
                {
                    string childCode = join[dalJoin.ChildCode].ToString();

                    float joinQty = float.TryParse(join[dalJoin.JoinQty].ToString(), out float i) ? i : 1;
                    float joinMax = float.TryParse(join[dalJoin.JoinMax].ToString(), out i) ? i : 1;
                    float joinMin = float.TryParse(join[dalJoin.JoinMin].ToString(), out i) ? i : 1;
                    float joinWastage = float.TryParse(join[dalJoin.JoinWastage].ToString(), out i) ? i : 1;

                    foreach (DataRow item in dt_Item.Rows)
                    {
                        if(childCode == item[dalItem.ItemCode].ToString())
                        {
                            #region add child to table

                            DataRow newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                            string itemCode = item[dalItem.ItemCode].ToString();
                            string itemName = item[dalItem.ItemName].ToString();
                            string itemType = item[dalItem.ItemCat].ToString();
                            float Child_Stock = float.TryParse(item[dalItem.ItemStock].ToString(), out Child_Stock) ? Child_Stock : 0;
                            float pendingOrder = float.TryParse(item[dalItem.ItemOrd].ToString(), out pendingOrder) ? pendingOrder : 0;
                            float Color_Rate = float.TryParse(item[dalItem.ItemMBRate].ToString(), out Color_Rate) ? Color_Rate : 0;
                            string itemUnit = item[dalItem.ItemUnit].ToString();


                            if (itemType != text.Cat_Part)//cbZeroStockType.Checked && 
                            {
                                Child_Stock = tool.getPMMAQtyFromDataTable(dt_Item, itemCode);
                            }
                           
                            newRow[text.Header_Index] = childIndex;
                            newRow[text.Header_ParentIndex] = parentIndex;
                            newRow[text.Header_GroupLevel] = groupLevel + 1;
                            newRow[text.Header_Type] = itemType;
                            newRow[text.Header_PartCode] = itemCode;
                            newRow[text.Header_PartName] = itemName;
                            newRow[text.Header_JoinQty] = joinQty;
                            newRow[text.Header_JoinMax] = joinMax;
                            newRow[text.Header_JoinMin] = joinMin;
                            newRow[text.Header_ReadyStock] = Child_Stock;
                            newRow[text.Header_PendingOrder] = pendingOrder;
                            newRow[text.Header_ColorRate] = Color_Rate;

                            #region add item weight info

                            float partWeight = 0;
                            float runnerWeight = 0;

                            partWeight = float.TryParse(item[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                            runnerWeight = float.TryParse(item[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                            if (partWeight <= 0)
                            {
                                partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                            }

                            //if (cbZeroCostOnly.Checked)
                            //{
                            //    partWeight = float.TryParse(item[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                            //    runnerWeight = float.TryParse(item[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                            //    if (partWeight <= 0)
                            //    {
                            //        partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                            //        runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                            //    }

                            //}
                            //else
                            //{
                            //    partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                            //    runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                            //}

                            if (partWeight <= 0)
                            {
                                float Cavity = float.TryParse(item[dalItem.ItemCavity].ToString(), out Cavity) ? Cavity : 1;

                                if (Cavity > 0)
                                {
                                    partWeight = (float.TryParse(item[dalItem.ItemProPWShot].ToString(), out partWeight) ? partWeight : 0) / Cavity;
                                    runnerWeight = (float.TryParse(item[dalItem.ItemProRWShot].ToString(), out runnerWeight) ? runnerWeight : 0) / Cavity;
                                }
                            }

                            float itemWeight = partWeight + runnerWeight;

                            float wastage = item[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(item[dalItem.ItemWastage]);

                            newRow[text.Header_PartWeight_G] = partWeight;
                            newRow[text.Header_RunnerWeight_G] = runnerWeight;
                            newRow[text.Header_WastageAllowed_Percentage] = wastage;
                            newRow[text.Header_Unit] = itemUnit;

                            #endregion

                            DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                            ChildQtyCalculation(childIndex);

                            #endregion

                            int subChildIndex = childIndex * 100 + 1;

                            #region add material

                            string RawMaterial = item[dalItem.ItemMaterial].ToString();
                            string ColorMaterial = item[dalItem.ItemMBatch].ToString();

                            if (!string.IsNullOrEmpty(RawMaterial))
                            {
                                newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                                newRow[text.Header_Index] = subChildIndex++;
                                newRow[text.Header_ParentIndex] = childIndex;
                                newRow[text.Header_GroupLevel] = groupLevel + 2;
                                newRow[text.Header_Type] = text.Cat_RawMat;
                                newRow[text.Header_PartCode] = RawMaterial;
                                newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, RawMaterial);

                               

                                newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);


                                //if (cbZeroStockType.Checked)
                                //{
                                //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);
                                //}
                                //else
                                //{
                                //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, RawMaterial), 2);
                                //}

                                newRow[text.Header_PendingOrder] = (float)Math.Round((double)tool.getOrderQtyFromDataTable(dt_Item, RawMaterial), 2);
                                newRow[text.Header_Unit] = text.Unit_KG;

                                DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                                ChildQtyCalculation(subChildIndex - 1);

                            }

                            if (!string.IsNullOrEmpty(ColorMaterial))
                            {
                                newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                                newRow[text.Header_Index] = subChildIndex++;
                                newRow[text.Header_ParentIndex] = childIndex;
                                newRow[text.Header_GroupLevel] = groupLevel + 2;
                                newRow[text.Header_Type] = tool.getCatNameFromDataTable(dt_Item, ColorMaterial);
                                newRow[text.Header_PartCode] = ColorMaterial;
                                newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, ColorMaterial);

                                newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);

                                //if (cbZeroStockType.Checked)
                                //{
                                //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                //}
                                //else
                                //{
                                //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                //}

                                newRow[text.Header_PendingOrder] = (float)Math.Round((double)tool.getOrderQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                newRow[text.Header_Unit] = text.Unit_KG;

                                DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                                ChildQtyCalculation(subChildIndex - 1);


                            }

                            #endregion

                            GetChildFromGroup(dt_Join, dt_Item, childCode, childIndex, groupLevel + 1, subChildIndex);

                            childIndex++;

                            break;
                        }

                        
                    }

                }
            }

        }

        private Tuple<float,string> getItemForecast(string itemCode, int year, int month)
        {
            float forecast = -1;
            string custID = "";

            dt_ItemForecast.AcceptChanges();

            foreach (DataRow row in dt_ItemForecast.Rows)
            {
                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                bool itemMatch = code == itemCode && yearData == year && monthData == month;
                custID = row[dalItemForecast.CustID].ToString();

                if (itemMatch)
                {
                    forecast =  float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i) ? i : -1;
                    row.Delete();
                    break;
                }
            }

            dt_ItemForecast.AcceptChanges();

            return Tuple.Create(forecast,custID);
        }

        private void ForecastDataFilter(int yearFrom, int yearTo)
        {
            dt_ItemForecast.AcceptChanges();

            foreach (DataRow row in dt_ItemForecast.Rows)
            {
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());

                if (yearData < yearFrom || yearData > yearTo)
                {
                    row.Delete();
                }
            }

            dt_ItemForecast.AcceptChanges();
        }

        private void ChildQtyCalculation(int childIndex)
        {
            if(DT_PRODUCT_FORECAST_SUMMARY != null)
                foreach(DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                {
                    if(childIndex.ToString() == row[text.Header_Index].ToString())
                    {
                        string parentIndex = row[text.Header_ParentIndex].ToString();

                        float Ready_Stock = float.TryParse(row[text.Header_ReadyStock].ToString(), out Ready_Stock) ? Ready_Stock : 0;

                        for(int i = 0; i < DT_PRODUCT_FORECAST_SUMMARY.Rows.Count; i++)
                        {
                            if (DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_Index].ToString() == parentIndex)
                            {
                                for (int j = 0; j < DT_PRODUCT_FORECAST_SUMMARY.Columns.Count; j++)
                                {
                                    string colName = DT_PRODUCT_FORECAST_SUMMARY.Columns[j].ColumnName;

                                    bool colFound = colName.Contains(text.str_Forecast);
                                    colFound |= colName.Contains(text.str_Delivered);
                                    colFound |= colName.Contains(text.str_RequiredQty);
                                    colFound |= colName.Contains(text.str_InsufficientQty);

                                    if (colFound)
                                    {
                                        float parentQty = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][j].ToString(), out parentQty) ? parentQty : 0;
                                        float wastage = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_WastageAllowed_Percentage].ToString(), out wastage) ? wastage : 0;

                                        string childType = row[text.Header_Type].ToString();

                                        float childQty;

                                        if (childType == text.Cat_RawMat || childType == text.Cat_MB || childType == text.Cat_Pigment)
                                        {
                                            float partWeight = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                                            float runnerWeight = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;
                                            float colorRate = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;

                                            float itemWeight = (partWeight + runnerWeight)/1000;

                                            childQty = (float) decimal.Round((decimal)( parentQty * itemWeight * (1 + wastage)), 3);

                                            if (childType == text.Cat_MB || childType == text.Cat_Pigment)
                                            {
                                                childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * colorRate * (1 + wastage)), 3);
                                            }
                                        }
                                        else
                                        {
                                            float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                                            float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 0;
                                            float joinWastage = float.TryParse(row[text.Header_JoinWastage].ToString(), out joinWastage) ? joinWastage : 0;

                                            joinMax = joinMax <= 0 ? 1 : joinMax;

                                            if(childType == text.Cat_Part)
                                            {
                                                childQty = parentQty / joinMax * joinQty;

                                            }
                                            else
                                            {
                                                childQty = (float)Math.Ceiling( parentQty / joinMax * joinQty * (1 + joinWastage));

                                            }
                                        }

                                        row[colName] = childQty;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }
            
                }
        }

        private void GetSummaryForecastMatUsedData()
        {
            #region data setting 

            frmLoading.ShowLoadingScreen();


            DT_PRODUCT_FORECAST_SUMMARY = null;
            DT_MATERIAL_FORECAST_SUMMARY = null;
            dt_ItemForecast = null;
            DT_PART_TRANSFER = null;
            DT_STOCK_LIST = null;

            dgvAlertSummary.DataSource = null;

            //dt_DeliveredData = Product_SummaryDataTable();

           // string custName = cmbCustomer.Text;

            string from = DATE_FROM.ToString("yyyy/MM/dd");
            string to = DATE_TO.ToString("yyyy/MM/dd");

            DataTable dt_PartTrfHist;
            DataTable dt_CustProduct;


            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();

            //get all item info
            dt_Item = dalItem.Select();

            bool PMMACustomer = true;

            dt_CustProduct = dalItemCust.custSearch(tool.getCustName(1));
            dt_PartTrfHist = dalTrfHist.rangeItemToCustomerSearch(tool.getCustName(1), from, to);
            dt_ItemForecast = dalItemForecast.Select(tool.getCustID(tool.getCustName(1)).ToString());

            //PMMACustomer = cmbCustomer.Text.Equals(tool.getCustName(1));


            DT_PART_TRANSFER = dt_PartTrfHist.Copy();

            dt_ItemForecast.DefaultView.Sort = dalItemForecast.ItemCode + " ASC, " + dalItemForecast.ForecastYear + " DESC, " + dalItemForecast.ForecastMonth + " DESC";
            dt_ItemForecast = dt_ItemForecast.DefaultView.ToTable();

            DT_PART_TRANSFER.DefaultView.Sort = dalTrfHist.TrfItemCode + " ASC, " + dalTrfHist.TrfDate + " ASC";
            DT_PART_TRANSFER = DT_PART_TRANSFER.DefaultView.ToTable();

            dt_CustProduct = RemoveTerminatedProduct(dt_CustProduct);

            //if (!cbShowTerminatedItem.Checked)
            //{
            //    dt_CustProduct = RemoveTerminatedProduct(dt_CustProduct);
            //}

            #endregion 

            //generate product forecast summary (with delivered data)
            if (dt_CustProduct != null)
            {
                #region data setting 2

                dt_CustProduct.DefaultView.Sort = dalItem.ItemCode + " ASC";
                dt_CustProduct = dt_CustProduct.DefaultView.ToTable();

                int monthStart = CURRENT_MONTH;
                int monthEnd = CURRENT_MONTH;

                int yearStart = CURRENT_YEAR;
                int yearEnd = CURRENT_YEAR;

                DateTime dateStart = DATE_FROM;
                DateTime dateEnd = DATE_TO;

                if (dateStart > dateEnd)
                {
                    int tmp;

                    tmp = yearStart;
                    yearStart = yearEnd;
                    yearEnd = tmp;

                    tmp = monthStart;
                    monthStart = monthEnd;
                    monthEnd = tmp;
                }

                string MonthlyForecast;
                string MonthlyDelivered;
                string MonthlyRequired;
                string MonthlyInsufficient;
                string MonthlyBalance;

                DataTable dt_PMMA_Date = dalPmmaDate.Select();

                DT_PRODUCT_FORECAST_SUMMARY = Product_SummaryDataTable();

                if (PMMACustomer)
                {
                    dateStart = tool.GetPMMAStartDate(monthStart, yearStart, dt_PMMA_Date);
                    dateEnd = tool.GetPMMAEndDate(monthEnd, yearEnd, dt_PMMA_Date);
                }
                else
                {
                    dateStart = new DateTime(yearStart, monthStart, 1);
                    dateEnd = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd));
                }

                ForecastDataFilter(yearStart, yearEnd);

                #endregion

                int index = 1;

                foreach (DataRow ProductRow in dt_CustProduct.Rows)
                {
                    DataRow newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                    string CustName = ProductRow[dalItemCust.CustName].ToString();
                    string ProductCode = ProductRow[dalItem.ItemCode].ToString();
                    string ProductName = ProductRow[dalItem.ItemName].ToString();
                    string itemUnit = ProductRow[dalItem.ItemUnit].ToString();
                    float Ready_Stock = float.TryParse(ProductRow[dalItem.ItemStock].ToString(), out Ready_Stock) ? Ready_Stock : 0;
                    float Pending_Order = float.TryParse(ProductRow[dalItem.ItemOrd].ToString(), out Pending_Order) ? Pending_Order : 0;
                    float Color_Rate = float.TryParse(ProductRow[dalItem.ItemMBRate].ToString(), out Color_Rate) ? Color_Rate : 0;
                    float Forecast = 0;
                    float Delivered = 0;
                    float Required = 0;
                    float Insufficient = 0;

                    newRow[text.Header_Customer] = CustName;
                    newRow[text.Header_Index] = index;
                    newRow[text.Header_ParentIndex] = 0;
                    newRow[text.Header_GroupLevel] = 1;
                    newRow[text.Header_Type] = ProductRow[dalItem.ItemCat].ToString();
                    newRow[text.Header_PartCode] = ProductCode;
                    newRow[text.Header_PartName] = ProductName;
                    newRow[text.Header_ReadyStock] = Ready_Stock;
                    newRow[text.Header_PendingOrder] = Pending_Order;
                    newRow[text.Header_ColorRate] = Color_Rate;

                    for (var date = dateStart; date <= dateEnd; date = date.AddMonths(1))
                    {
                        var i = date.Year;
                        var j = date.Month;

                        if (date == dateStart && j < monthStart)
                        {
                            date = date.AddMonths(1);
                            i = date.Year;
                            j = date.Month;
                            date = new DateTime(i, j, 1);
                        }

                        MonthlyForecast = j + "/" + i + text.str_Forecast;
                        MonthlyDelivered = j + "/" + i + text.str_Delivered;
                        MonthlyRequired = j + "/" + i + text.str_RequiredQty;
                        MonthlyInsufficient = j + "/" + i + text.str_InsufficientQty;
                        MonthlyBalance = j + "/" + i + text.str_EstBalance;

                        DateTime dateFrom, dateTo;

                        if (PMMACustomer)
                        {
                            dateFrom = tool.GetPMMAStartDate(j, i, dt_PMMA_Date);
                            dateTo = tool.GetPMMAEndDate(j, i, dt_PMMA_Date);
                        }
                        else
                        {
                            dateFrom = new DateTime(i, j, 1);
                            dateTo = new DateTime(i, j, DateTime.DaysInMonth(i, j));
                        }

                        bool colFound = DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyForecast);
                        colFound &= DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyDelivered);
                        colFound &= DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyRequired);
                        colFound &= DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyInsufficient);

                        if (colFound)
                        {
                            var result = getItemForecast(ProductCode, i, j);

                            Forecast = result.Item1;
                            string custID = result.Item2;

                            Forecast = Forecast < 0 ? 0 : Forecast;

                            Delivered = DeliveredToCustomerQty(custID, ProductCode, dateFrom, dateTo);
                            newRow[MonthlyDelivered] = Delivered;

                            //if (cbDeductDeliveredQty.Checked)
                            //{
                            //    Delivered = DeliveredToCustomerQty(custID, ProductCode, dateFrom, dateTo);
                            //    newRow[MonthlyDelivered] = Delivered;
                            //}

                            Required = Forecast - Delivered;

                            Required = Required < 0 ? 0 : Required;

                            Insufficient = Ready_Stock - Required;

                            

                            newRow[MonthlyForecast] = Forecast;
                            newRow[MonthlyRequired] = Required;

                            newRow[MonthlyBalance] = Insufficient;

                            Insufficient = Insufficient > 0 ? 0 : Insufficient;
                            newRow[MonthlyInsufficient] = Insufficient;

                        }
                    }

                    #region add item weight info

                    float partWeight = 0;
                    float runnerWeight = 0;

                    partWeight = float.TryParse(ProductRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                    runnerWeight = float.TryParse(ProductRow[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                    if (partWeight <= 0)
                    {
                        partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                        runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                    }

                    //if (cbZeroCostOnly.Checked)
                    //{
                    //    partWeight = float.TryParse(ProductRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                    //    runnerWeight = float.TryParse(ProductRow[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                    //    if (partWeight <= 0)
                    //    {
                    //        partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                    //        runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                    //    }

                    //}
                    //else
                    //{
                    //    partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                    //    runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                    //}

                    if (partWeight <= 0)
                    {
                        float Cavity = float.TryParse(ProductRow[dalItem.ItemCavity].ToString(), out Cavity) ? Cavity : 1;

                        if(Cavity > 0)
                        {
                            partWeight = (float.TryParse(ProductRow[dalItem.ItemProPWShot].ToString(), out partWeight) ? partWeight : 0) / Cavity;
                            runnerWeight = (float.TryParse(ProductRow[dalItem.ItemProRWShot].ToString(), out runnerWeight) ? runnerWeight : 0) / Cavity;
                        }
                       
                    }

                    float itemWeight = partWeight + runnerWeight;

                    float wastage = ProductRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(ProductRow[dalItem.ItemWastage]);

                    newRow[text.Header_PartWeight_G] = partWeight;
                    newRow[text.Header_RunnerWeight_G] = runnerWeight;
                    newRow[text.Header_WastageAllowed_Percentage] = wastage;
                    newRow[text.Header_Unit] = itemUnit;

                    #endregion

                    DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                    #region add material

                    string RawMaterial = ProductRow[dalItem.ItemMaterial].ToString();
                    string ColorMaterial = ProductRow[dalItem.ItemMBatch].ToString();

               

                    int childIndex = index *1000 + 1;

                    if(!string.IsNullOrEmpty(RawMaterial))
                    {
                        newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                        newRow[text.Header_Index] = childIndex++;
                        newRow[text.Header_ParentIndex] = index;
                        newRow[text.Header_GroupLevel] = 2;
                        newRow[text.Header_Type] = text.Cat_RawMat;
                        newRow[text.Header_PartCode] = RawMaterial;
                        newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, RawMaterial);

                        newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);

                        //if (cbZeroStockType.Checked)
                        //{
                        //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);
                        //}
                        //else
                        //{
                        //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, RawMaterial), 2);
                        //}

                        newRow[text.Header_PendingOrder] = (float)Math.Round((double)tool.getOrderQtyFromDataTable(dt_Item, RawMaterial), 2);

                        newRow[text.Header_Unit] = text.Unit_KG;

                        DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                        ChildQtyCalculation(childIndex-1);
                    }

                    if (!string.IsNullOrEmpty(ColorMaterial))
                    {
                        newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                        newRow[text.Header_Index] = childIndex++;
                        newRow[text.Header_ParentIndex] = index;
                        newRow[text.Header_GroupLevel] = 2;
                        newRow[text.Header_Type] = tool.getCatNameFromDataTable(dt_Item, ColorMaterial);
                        newRow[text.Header_PartCode] = ColorMaterial;
                        newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, ColorMaterial);

                        newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);

                        //if (cbZeroStockType.Checked)
                        //{
                        //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);
                        //}
                        //else
                        //{
                        //    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, ColorMaterial), 2);
                        //}

                        newRow[text.Header_PendingOrder] = (float)Math.Round((double)tool.getOrderQtyFromDataTable(dt_Item, ColorMaterial), 2);
                        newRow[text.Header_Unit] = text.Unit_KG;


                        DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);
                        ChildQtyCalculation(childIndex - 1);

                    }

                    #endregion

                    #region add child group

                    GetChildFromGroup(dt_Join, dt_Item, ProductCode, index,1, childIndex);

                    #endregion

                    index++;
                }

                //index marking
                DT_PRODUCT_FORECAST_SUMMARY = IndexMarking(DT_PRODUCT_FORECAST_SUMMARY);


                ////stock deduct
                //if(cbDeductUsedStock.Checked)//1873ms
                //{
                //    StockDeductCalculation();
                //    DT_PRODUCT_FORECAST_SUMMARY.DefaultView.Sort = text.Header_IndexMarking + " ASC";
                //    DT_PRODUCT_FORECAST_SUMMARY = DT_PRODUCT_FORECAST_SUMMARY.DefaultView.ToTable();

                //}

                //material & child Item merge & balance calculation
                ChildItemMergeAndBalCalculation();

                New_PrintForecastSummary();
            }

            frmLoading.CloseForm();
        }

        private DataTable IndexMarking(DataTable dt)
        {
            if(dt != null && dt.Columns.Contains(text.Header_IndexMarking))
            {
                int indexMarking = 1;

                foreach(DataRow row in dt.Rows)
                {
                    row[text.Header_IndexMarking] = indexMarking++;
                }
            }

            return dt;
        }

        private float GetLatestBalStock(int CurrentLevel, string itemCode, float BalStock)
        {

            if(DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                if(CurrentLevel > 1)
                {
                    CurrentLevel --;
                }
                
                bool LevelItemFound = false;

                foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                {
                    string looping_ItemCode = row[text.Header_PartCode].ToString();
                    int looping_Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out int i) ? i : 0;

                    if(looping_Level == CurrentLevel && looping_ItemCode == itemCode)
                    {
                        LevelItemFound = true;
                        float StockTmp = float.TryParse(row[text.Header_BalStock].ToString(), out StockTmp) ? StockTmp : 0;

                        if( StockTmp < BalStock)
                        {
                            BalStock = StockTmp;
                        }

                    }
                }

                if(!LevelItemFound && CurrentLevel > 1)
                {
                    BalStock = GetLatestBalStock(CurrentLevel - 1, itemCode, BalStock);
                }
                else if(!LevelItemFound && CurrentLevel == 1)
                {
                    foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                    {
                        if (row[text.Header_PartCode].ToString() == itemCode)
                        {
                            BalStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out BalStock) ? BalStock : 0;

                            break;
                        }
                    }
                }
            }

            return BalStock;

        }

        private float GetLatestBalStock(string itemCode)
        {
            float balStock = 0;

            if(DT_STOCK_LIST != null)
            {
                
                foreach (DataRow row in DT_STOCK_LIST.Rows)
                {
                    if(itemCode == row[text.Header_PartCode].ToString())
                    {
                        return float.TryParse(row[text.Header_BalStock].ToString(), out balStock) ? balStock : 0;
                    }
                }

                //if item not found in stock list
                if (DT_PRODUCT_FORECAST_SUMMARY != null)
                    foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                    {
                        if (itemCode == row[text.Header_PartCode].ToString())
                        {
                            balStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out balStock) ? balStock : 0;
                            balStock = balStock == -1 ? 0 : balStock;

                            UpdateLatestBalStock(itemCode, balStock);
                            break;
                        }
                    }
            }

            return balStock;
        }

        private void UpdateLatestBalStock(string itemCode, float balStock)
        {
            if (DT_STOCK_LIST == null)
            {
                DT_STOCK_LIST = New_StockList_DataTable();
            }


            foreach (DataRow row in DT_STOCK_LIST.Rows)
            {
                if(itemCode == row[text.Header_PartCode].ToString())
                {
                    row[text.Header_BalStock] = balStock;
                    return;
                }
            }

            //if data not found
            DataRow NewRow = DT_STOCK_LIST.NewRow();

            NewRow[text.Header_PartCode] = itemCode;
            NewRow[text.Header_BalStock] = balStock;
            DT_STOCK_LIST.Rows.Add(NewRow);

        }

        private void UpdateMonthy(string itemCode, float balStock, string Month)
        {
            if (DT_STOCK_LIST == null)
            {
                DT_STOCK_LIST = New_StockList_DataTable();
            }


            foreach (DataRow row in DT_STOCK_LIST.Rows)
            {
                if (itemCode == row[text.Header_PartCode].ToString())
                {
                    row[text.Header_BalStock] = balStock;


                    row[Month + text.str_EstBalance] = balStock;

                    return;
                }
            }

            //if data not found
            DataRow NewRow = DT_STOCK_LIST.NewRow();

            NewRow[text.Header_PartCode] = itemCode;
            NewRow[text.Header_BalStock] = balStock;
            NewRow[Month + text.str_EstBalance] = balStock;
            DT_STOCK_LIST.Rows.Add(NewRow);

        }
        private void StockDeductCalculation()//Level 1 only
        {
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {

                if(DT_STOCK_LIST == null)
                {
                    DT_STOCK_LIST = New_StockList_DataTable();
                }

                float Bal_Stock = 0;
                string Last_PartCode = null;

                DT_PRODUCT_FORECAST_SUMMARY.DefaultView.Sort = text.Header_Type + " ASC," + text.Header_PartCode + " ASC," + text.Header_GroupLevel + " ASC";
                DT_PRODUCT_FORECAST_SUMMARY = DT_PRODUCT_FORECAST_SUMMARY.DefaultView.ToTable();
                int Level_Checking = 1;

                foreach(DataColumn col in DT_PRODUCT_FORECAST_SUMMARY.Columns)
                {
                    string ColName = col.ColumnName;

                    if(ColName.Contains(text.str_Forecast))
                    {
                        foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                        {
                            string itemType = row[text.Header_Type].ToString();

                            if (itemType == text.Cat_Part)
                            {
                                int Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out Level) ? Level : 0;
                                string itemCode = row[text.Header_PartCode].ToString();

                                if (Last_PartCode != itemCode)
                                {
                                    Last_PartCode = itemCode;
                                    Bal_Stock = GetLatestBalStock(itemCode);
                                }

                                if (Level == Level_Checking && Last_PartCode == itemCode)
                                {
                                    float Required = float.TryParse(row[ColName.Replace(text.str_Forecast, text.str_RequiredQty)].ToString(), out float j) ? j : 0;
                                    float Insufficient = 0;

                                    Bal_Stock -= Required;

                                    row[ColName.Replace(text.str_Forecast, text.str_EstBalance)] = Bal_Stock;

                                    if (Bal_Stock < 0)
                                    {
                                        Insufficient = (float)Bal_Stock;
                                        Bal_Stock = 0;
                                    }
                                    else
                                    {
                                        Insufficient = 0;
                                    }

                                    row[ColName.Replace(text.str_Forecast, text.str_InsufficientQty)] = Insufficient;

                                    

                                    row[text.Header_BalStock] = Bal_Stock;

                                    string Month = ColName.Replace(text.str_Forecast, "");
                                    UpdateLatestBalStock(itemCode, Bal_Stock);
                                }
                            }

                        }
                    }

                }

                //Level 2 and above
                StockDeductCalculation(Level_Checking + 1);
            }
        }

        private void StockDeductCalculation(int Level_Checking)// Level 2 above
        {
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                float balStock = 0;
                string Last_PartCode = null;
                bool LevelFound = false;

                foreach (DataColumn col in DT_PRODUCT_FORECAST_SUMMARY.Columns)
                {
                    string ColName = col.ColumnName;

                    if (ColName.Contains(text.str_Forecast))
                    {
                        foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                        {
                            string itemType = row[text.Header_Type].ToString();
                            string indexMarking = row[text.Header_Index].ToString();
                            int Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out Level) ? Level : 0;
                            string itemCode = row[text.Header_PartCode].ToString();

                            if (Last_PartCode != itemCode)
                            {
                                Last_PartCode = itemCode;

                                balStock = GetLatestBalStock(itemCode);
                            }

                            if (Level == Level_Checking && Last_PartCode == itemCode)
                            {
                                LevelFound = true;

                                DataRow ParentRow = GetParentDatarow(row[text.Header_ParentIndex].ToString());

                                float Required = float.TryParse(ParentRow[ColName.Replace(text.str_Forecast, text.str_InsufficientQty)].ToString(), out float j) ? j * -1 : 0;

                                if (itemType == text.Cat_RawMat || itemType == text.Cat_MB || itemType == text.Cat_Pigment)
                                {
                                    float partWeight = float.TryParse(ParentRow[text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                                    float runnerWeight = float.TryParse(ParentRow[text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;
                                    float colorRate = float.TryParse(ParentRow[text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;
                                    float wastage = float.TryParse(ParentRow[text.Header_WastageAllowed_Percentage].ToString(), out wastage) ? wastage : 0;

                                    float itemWeight = (partWeight + runnerWeight) / 1000;

                                    if (itemType == text.Cat_MB || itemType == text.Cat_Pigment)
                                    {
                                        Required = (float)decimal.Round((decimal)(Required * itemWeight * colorRate * (1 + wastage)), 3);
                                    }
                                    else
                                    {
                                        Required = (float)decimal.Round((decimal)(Required * itemWeight * (1 + wastage)), 3);

                                    }
                                }
                                else
                                {
                                    float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                                    float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 0;
                                    float joinWastage = float.TryParse(row[text.Header_JoinWastage].ToString(), out joinWastage) ? joinWastage : 0;

                                    joinMax = joinMax <= 0 ? 1 : joinMax;

                                    if (itemType == text.Cat_Part)
                                    {
                                        Required = Required / joinMax * joinQty;

                                    }
                                    else
                                    {
                                        Required = (float)Math.Ceiling(Required / joinMax * joinQty * (1 + joinWastage));

                                    }
                                }


                                balStock -= Required;

                                row[ColName.Replace(text.str_Forecast, text.str_EstBalance)] = balStock;

                                float Insufficient = 0;


                                if (balStock < 0)
                                {
                                    Insufficient = (float)balStock;
                                    balStock = 0;
                                }
                                else
                                {
                                    Insufficient = 0;
                                }

                                string Month = ColName.Replace(text.str_Forecast, "");
                                UpdateLatestBalStock(itemCode, balStock);

                                row[ColName.Replace(text.str_Forecast, text.str_RequiredQty)] = Required;
                                row[ColName.Replace(text.str_Forecast, text.str_InsufficientQty)] = Insufficient;

                                row[text.Header_BalStock] = balStock;


                            }

                        }
                    }

                }

               

                if (LevelFound)
                {
                    StockDeductCalculation(Level_Checking + 1);
                }
            }
        }

        private DataRow GetParentDatarow(string ParentIndex)
        {

            foreach(DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
            {
                if(ParentIndex == row[text.Header_Index].ToString())
                {
                    return row;
                }
            }
            return null;
        }

        private void ChildItemMergeAndBalCalculation()
        {
            DT_MATERIAL_FORECAST_SUMMARY = Material_SummaryDataTable();

            if(DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                DataTable dt_Product = DT_PRODUCT_FORECAST_SUMMARY.Copy();

                dt_Product.DefaultView.Sort = text.Header_PartCode + " ASC";
                dt_Product = dt_Product.DefaultView.ToTable();

                string previousItemCode = null;

                DataRow newRow = DT_MATERIAL_FORECAST_SUMMARY.NewRow();

                int index = 1;

                foreach (DataRow productRow in dt_Product.Rows)
                {
                    string itemCode = productRow[text.Header_PartCode].ToString();
                    string itemName = productRow[text.Header_PartName].ToString();
                    string itemType = productRow[text.Header_Type].ToString();
                    string itemUnit = productRow[text.Header_Unit].ToString();

                    float readyStock = float.TryParse(productRow[text.Header_ReadyStock].ToString(), out readyStock) ? readyStock : 0;
                    float pendingOrder = float.TryParse(productRow[text.Header_PendingOrder].ToString(), out pendingOrder) ? pendingOrder : 0;

                    if (previousItemCode != itemCode)
                    {
                        if (previousItemCode != null)//next code found, save previous code's data to table
                        {
                            DT_MATERIAL_FORECAST_SUMMARY.Rows.Add(newRow);
                        }

                        previousItemCode = itemCode;

                        newRow = DT_MATERIAL_FORECAST_SUMMARY.NewRow();

                        newRow[text.Header_Index] = index++;
                        newRow[text.Header_Type] = itemType;
                        newRow[text.Header_ReadyStock] = readyStock;
                        newRow[text.Header_PartCode] = itemCode;
                        newRow[text.Header_PartName] = itemName;
                        newRow[text.Header_Unit] = itemUnit;
                        newRow[text.Header_PendingOrder] = pendingOrder;

                    }

                    foreach(DataColumn productCol in dt_Product.Columns)
                    {
                        string colName = productCol.ColumnName;
                        bool colFound = colName.Contains(text.str_Forecast);
                        colFound |= colName.Contains(text.str_Delivered);
                        colFound |= colName.Contains(text.str_RequiredQty);
                        colFound |= colName.Contains(text.str_InsufficientQty);

                        if (colFound)
                        {
                            float qty = float.TryParse(productRow[colName].ToString(), out qty) ? qty : 0;

                            float previous_qty = float.TryParse(newRow[colName].ToString(), out previous_qty) ? previous_qty : 0;

                            newRow[colName] = (float)decimal.Round((decimal)(qty + previous_qty), 3);

                            if (colName.Contains(text.str_RequiredQty))
                            {
                                string EstBalance_ColName = colName.Replace(text.str_RequiredQty, text.str_EstBalance);
                                float Required = qty + previous_qty;

                                
                                readyStock = (float)decimal.Round((decimal) (readyStock - Required), 3);


                                newRow[EstBalance_ColName] = readyStock;
                            }
                           
                        }

                    }
                }

                DT_MATERIAL_FORECAST_SUMMARY.Rows.Add(newRow);
            }
        }

        #endregion

      
        #region Old Order Page method

        private int GetPONoFromActionRecord(string orderID, DataTable dt_OrderAction)
        {
            int PoNo = -1;
            int actionID = 0;
            bool orderFound = false;

            foreach (DataRow row in dt_OrderAction.Rows)
            {
                if (orderID == row["ord_id"].ToString())
                {
                    orderFound = true;

                    string action = row["action"].ToString();
                    string actionDetail = row["action_detail"].ToString();
                    string to = row["action_to"].ToString();

                    if (action == "EDIT" && actionDetail == "P/O NO")
                    {
                        if (PoNo != -1)
                        {
                            int NewactionID = int.TryParse(row["order_action_id"].ToString(), out int i) ? i : 0;

                            if (NewactionID > actionID)
                            {
                                actionID = NewactionID;

                                PoNo = int.TryParse(to, out i) ? i : -1;
                            }
                        }
                        else
                        {
                            actionID = int.TryParse(row["order_action_id"].ToString(), out int i) ? i : actionID;
                            PoNo = int.TryParse(to, out i) ? i : -1;


                        }
                    }


                }
                else if (orderFound)
                {
                    return PoNo;
                }

            }

            return PoNo;

        }

        private void resetForm()
        {
            loaded = true;
        }

        private void dgvOrderUIEdit(DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


            //dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[text.Header_PartCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            //dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerCat].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerDateRequired].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[headerOrdered].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerPending].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerReceived].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerUnit].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerStatus].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[headerPONO].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            dgv.Columns[headerID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPONO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerOrdered].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPending].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerReceived].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerDateRequired].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerType].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerCat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerUnit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerOrdered].DefaultCellStyle.Format = "0.###";
            dgv.Columns[headerPending].DefaultCellStyle.Format = "0.###";
            dgv.Columns[headerReceived].DefaultCellStyle.Format = "0.###";



            dgv.Columns[headerCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[headerName].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[headerType].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerDateRequired].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            dgv.Columns[headerPONO].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.Columns[headerCat].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerUnit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerStatus].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
        }

        private float LoadPendingReceiveOrder(string keywords)
        {
            float pendingOrderQty = 0;

            if (keywords != null)
            {
                DataTable dt;

                dt = new ordDAL().PendingOrderSelect(keywords);

                foreach (DataRow ord in dt.Rows)
                {
                    string ordStatus = ord["ord_status"].ToString();
                    string itemCode = ord["ord_item_code"].ToString();

                    if (ordStatus.Equals(status_Pending) && itemCode == keywords)
                    {
                        float ordQty = float.TryParse(ord["ord_qty"].ToString(), out ordQty) ? ordQty : 0;
                        float ordReceived = float.TryParse(ord["ord_received"].ToString(), out ordReceived) ? ordReceived : 0;

                        float ordPending = ordQty - ordReceived;

                        ordPending = ordPending < 0 ? 0 : ordPending;

                        pendingOrderQty += ordPending;
                    }
                }
            }
            return pendingOrderQty;
        }



        #endregion

        #region UI Action

        private void frmOrderAlert_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MainDashboard.NewOrdFormOpen = false;
        }

        private void ResetPage()
        {
            tool.DoubleBuffered(dgvAlertSummary, true);

            CMBReset();
            DateReset();

            //dt_MatUsed_Forecast = NewMatUsedTable();
            //dt_DeliveredData = NewOrderAlertSummaryTable();
        }

        private void LoadItemTypeToCMB(ComboBox cmb)
        {
            //text.Cat_RawMat || matCat == text.Cat_MB || matCat == text.Cat_Pigment || matCat == text.Cat_SubMat

            DataTable dt = new DataTable();
            dt.Columns.Add(text.Header_Category, typeof(string));

            dt.Rows.Add(text.Cat_RawMat);
            dt.Rows.Add(text.Cat_MB);
            dt.Rows.Add(text.Cat_Pigment);
            dt.Rows.Add(text.Cat_SubMat);
            dt.Rows.Add(text.Cat_Carton);

            cmb.DataSource = dt;
            cmb.DisplayMember = text.Header_Category;
            cmb.SelectedIndex = -1;
        }

        private void CMBReset()
        {
            // tool.LoadMaterialAndAllToComboBox(cmbItemType);

            //tool.LoadCustomerAndAllToComboBox(cmbCustomer);
            //LoadItemTypeToCMB(cmbItemType);
            //cmbCustomer.Text = tool.getCustName(1);
            //cmbCustomer.Enabled = false;
        }

        private void DateReset()
        {
            //loadMonthDataToCMB(cmbMonthFrom);
            //loadYearDataToCMB(cmbYearFrom);

            //loadMonthDataToCMB(cmbMonthTo);
            //loadYearDataToCMB(cmbYearTo);

            //cmbMonthFrom.Text = DateTime.Now.Month.ToString();
            //cmbYearFrom.Text = DateTime.Now.Year.ToString();

            //cmbMonthTo.Text = DateTime.Now.AddMonths(3).Month.ToString();
            //cmbYearTo.Text = DateTime.Now.AddMonths(3).Year.ToString();
        }

        private void MainFilterOnly(bool showMainFilterOnly)
        {
            //if (showMainFilterOnly)
            //{
            //    tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);//row 2
            //    btnFilter.Text = textMoreFilters;
            //}
            //else
            //{
            //    tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 110f);//row 2
            //    btnFilter.Text = textHideFilters;
            //}
        }
        private void frmMaterialUsedReport_NEW_Load(object sender, EventArgs e)
        {
          



            



        }

        private void loadMonthDataToCMB(ComboBox cmb)
        {
            DataTable dt_month = new DataTable();

            dt_month.Columns.Add("month");

            for (int i = 1; i <= 12; i++)
            {
                dt_month.Rows.Add(i);
            }

            cmb.DataSource = dt_month;
            cmb.DisplayMember = "month";
        }

        private void loadYearDataToCMB(ComboBox cmb)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add("year");

            for (int i = 2018; i <= 2018 + 100; i++)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = "year";
        }

        private DateTime DATE_FROM;
        private DateTime DATE_TO;
        private void getStartandEndDate()
        {
            
            DATE_FROM = tool.GetPMMAStartDate(CURRENT_MONTH, CURRENT_YEAR);
            DATE_TO = tool.GetPMMAEndDate(CURRENT_MONTH, CURRENT_YEAR);
        }

      
    

      

      
        #endregion

    

        private void dgvAlertSummary_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int rowIndex = e.RowIndex;

            //if (rowIndex >= 0)
            //{
            //    string itemCode = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();

            //    int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
            //    int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

            //    int yearStart = Convert.ToInt32(cmbYearFrom.Text);
            //    int yearEnd = Convert.ToInt32(cmbYearTo.Text);

            //    //check date
            //    DateTime dateStart = new DateTime(yearStart, monthStart, 1);
            //    DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

            //    if (dateStart > dateEnd)
            //    {
            //        int tmp;

            //        tmp = yearStart;
            //        yearStart = yearEnd;
            //        yearEnd = tmp;

            //        tmp = monthStart;
            //        monthStart = monthEnd;
            //        monthEnd = tmp;
            //    }

            //    frmOrderAlertDetail_NEW frm = new frmOrderAlertDetail_NEW(DT_PRODUCT_FORECAST_SUMMARY, DT_MATERIAL_FORECAST_SUMMARY ,itemCode, dateStart, dateEnd,cbDeductUsedStock.Checked);

            //    frm.StartPosition = FormStartPosition.CenterScreen;

            //    frm.ShowDialog();

            //}
        }

        private void dgvAlertSummary_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void dgvAlertSummary_Sorted(object sender, EventArgs e)
        {
            int index = 1;
            foreach(DataGridViewRow row in dgvAlertSummary.Rows)
            {
                row.Cells[text.Header_Index].Value = index.ToString();

                index++;
            }
        }

        private void dgvAlertSummary_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                DataGridView dgv = dgvAlertSummary;

                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;


                try
                {

                    my_menu.Items.Add(text.Str_MoreDetail).Name = text.Str_MoreDetail;
                    my_menu.Items.Add(text.Str_OrderRequest).Name = text.Str_OrderRequest;
                    my_menu.Items.Add(text.Str_OrderRecordSearch).Name = text.Str_OrderRecordSearch;

                    contextMenuStrip1 = my_menu;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(MaterialForecast_my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

     

        private void MaterialForecast_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //DataGridView dgv = dgvAlertSummary;
            //string itemClicked = e.ClickedItem.Name.ToString();
            //int rowIndex = dgv.CurrentCell.RowIndex;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //contextMenuStrip1.Hide();

            //if (itemClicked.Equals(text.Str_MoreDetail))
            //{
            //    if (rowIndex >= 0)
            //    {
            //        string itemCode = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();

            //        int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
            //        int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

            //        int yearStart = Convert.ToInt32(cmbYearFrom.Text);
            //        int yearEnd = Convert.ToInt32(cmbYearTo.Text);

            //        //check date
            //        DateTime dateStart = new DateTime(yearStart, monthStart, 1);
            //        DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

            //        if (dateStart > dateEnd)
            //        {
            //            int tmp;

            //            tmp = yearStart;
            //            yearStart = yearEnd;
            //            yearEnd = tmp;

            //            tmp = monthStart;
            //            monthStart = monthEnd;
            //            monthEnd = tmp;
            //        }

            //        frmOrderAlertDetail_NEW frm = new frmOrderAlertDetail_NEW(DT_PRODUCT_FORECAST_SUMMARY, DT_MATERIAL_FORECAST_SUMMARY, itemCode, dateStart, dateEnd,cbDeductUsedStock.Checked);

            //        frm.StartPosition = FormStartPosition.CenterScreen;

            //        frm.ShowDialog();

            //    }
            //}
            //else if (itemClicked.Equals(text.Str_OrderRequest))
            //{
            //    if (rowIndex >= 0)
            //    {
            //        string itemCode = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();
            //        string itemName = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartName].Value.ToString();

            //        Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //        frmOrderRequest frm = new frmOrderRequest(tool.getItemCat(itemCode), itemCode, itemName, cbZeroCostOnly.Checked);

            //        frm.StartPosition = FormStartPosition.CenterScreen;
            //        frm.ShowDialog();//create new order

            //        if (frmOrderRequest.orderSuccess)
            //        {
            //            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //            resetForm();

            //            dgvAlertSummary.Rows[rowIndex].Cells[text.Header_Order_Requesting].Value = tool.GetRequestingOrder(dalOrd.RequestingOrderSelect(), itemCode);

            //            frmOrderRequest.orderSuccess = false;
            //            Cursor = Cursors.Arrow; // change cursor to normal type
            //        }
            //        Cursor = Cursors.Arrow; // change cursor to normal type

            //    }
            //}
          
            //Cursor = Cursors.Arrow; // change cursor to normal type
            
        }

     

        private void frmOrderAlert_NEW_Shown(object sender, EventArgs e)
        {
            dgvAlertSummary.ClearSelection();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

    }
}


