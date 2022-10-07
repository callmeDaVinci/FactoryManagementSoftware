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

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderAlert_NEW : Form
    {
        public frmOrderAlert_NEW()
        {
            InitializeComponent();

            ResetPage();

            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
        }

        #region variable declare

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
        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerMat = "MATERIAL";
        readonly string headerCode = "CODE";
        readonly string headerName = "NAME";
        readonly string headerMB = "MB";
        readonly string headerMBRate = "MB RATE";
        readonly string headerWeight = "WEIGHT";
        readonly string headerWastage = "WASTAGE RATE";
        //private string readyStockHeaderText = "READY STOCK";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerZeroCostStock = "ZERO COST STOCK";
        private string headerBalanceZero = "FORECAST BAL 0";
        private string headerBalanceOne = "FORECAST BAL 1";
        private string headerBalanceTwo = "FORECAST BAL 2";
        private string headerBalanceThree = "FORECAST BAL 3";
        private string headerBalanceFour = "FORECAST BALANCE 4";
        readonly string headerPendingOrder = "PENDING ORDER";

        readonly string headerForecast1 = "FORECAST 1";
        readonly string headerForecast2 = "FORECAST 2";
        readonly string headerForecast3 = "FORECAST 3";
        readonly string headerForecast4 = "FORECAST 4";

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";
        private string header_Forecast = "FORECAST";
        private string header_Delivered = "DELIVERED";
        private string header_TotalOut = "TOTAL";
        private string header_Stock = "STOCK";
        private string header_Bal = "BAL.";
        private string header_Unit = "UNIT";
        private string header_ParentIndex = "PARENT #";

        private string Unit_KG = "KG";
        private string Unit_PCS = "PCS";

        //private string text.str_Forecast = " FORECAST";
        //private string string_StillNeed = " STILL NEED";
        //private string text.str_Delivered = " DELIVERED";
        //private string text.str_EstBalance = " EST. BAL.";

        private string ForecastType_DeductStock = " (FORECAST-STOCK)";
        private string ForecastType_FORECAST = " FORECAST";


        readonly string reportType_Delivered = "DELIVERED";
        readonly string reportType_ReadyStock = "READY STOCK";
        readonly string reportType_Forecast = "FORECAST";

        readonly string reportTitle_ActualUsed = "MATERIAL USED REPORT";
        readonly string reportTitle_Forecast = "MATERIAL USED REPORT";
        readonly string reportTitle_ReadyStock = "MATERIAL USED REPORT";

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

        custDAL dalCust = new custDAL();
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

        private DataTable dt_MatUsed_Forecast;
        private DataTable dt_MatStock;
        private DataTable dt_Mat;
        private DataTable dt_PMMA;
        private DataTable dt_DeliveredData;
        private DataTable dt_ItemForecast;
        bool loaded = false;
        #endregion

        #region UI Design

        private DataTable NewOrderRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerDateRequired, typeof(DateTime));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerCat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerPONO, typeof(int));
            dt.Columns.Add(headerOrdered, typeof(float));
            dt.Columns.Add(headerPending, typeof(float));
            dt.Columns.Add(headerReceived, typeof(float));
            dt.Columns.Add(headerUnit, typeof(string));
            dt.Columns.Add(headerStatus, typeof(string));

            return dt;
        }

        private DataTable NewOrderAlertSummaryTable()
        {
            DataTable dt = new DataTable();

            header_Forecast = cmbMonthFrom.Text + "/" + cmbYearFrom.Text + text.str_Forecast;
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(header_Forecast, typeof(float));

            return dt;
        }

        private DataTable Product_SummaryDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_IndexMarking, typeof(int));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(header_ParentIndex, typeof(int));
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

            // headerOutQty = cmbMonth.Text + "/" + cmbYear.Text + " FORECAST";

            string month = null;

            int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
            int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

            int yearStart = Convert.ToInt32(cmbYearFrom.Text);
            int yearEnd = Convert.ToInt32(cmbYearTo.Text);

            //check date
            DateTime dateStart = new DateTime(yearStart, monthStart, 1);
            DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

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

                month = j + "/" + i;

                dt.Columns.Add(month + text.str_Forecast, typeof(float));
                dt.Columns.Add(month + text.str_Delivered, typeof(float));
                dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
                dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));
            }

            dt.Columns.Add(text.Header_PendingOrder, typeof(string));

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

            #region Get Start Date & End Date

            int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
            int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

            int yearStart = Convert.ToInt32(cmbYearFrom.Text);
            int yearEnd = Convert.ToInt32(cmbYearTo.Text);

            DateTime dateStart = new DateTime(yearStart, monthStart, 1);
            DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

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

            #endregion

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

                string month = j + "/" + i;
                dt.Columns.Add(month + text.str_Forecast, typeof(float));
                dt.Columns.Add(month + text.str_Delivered, typeof(float));
                dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
                dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));
                dt.Columns.Add(month + text.str_EstBalance, typeof(float));
            }

            dt.Columns.Add(text.Header_PendingOrder, typeof(string));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            return dt;
        }

        private DataTable NewMatUsedTable()
        {
            DataTable dt = new DataTable();

            header_Forecast = cmbMonthFrom.Text + "/" + cmbYearFrom.Text + text.str_Forecast;

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

            string month = null;

            int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
            int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

            int yearStart = Convert.ToInt32(cmbYearFrom.Text);
            int yearEnd = Convert.ToInt32(cmbYearTo.Text);

    
            //check date
            DateTime dateStart = new DateTime(yearStart, monthStart, 1);
            DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

            if(dateStart > dateEnd)
            {
                int tmp;

                tmp = yearStart;
                yearStart = yearEnd;
                yearEnd = tmp;

                tmp = monthStart;
                monthStart = monthEnd;
                monthEnd = tmp;
            }

            header_Forecast = monthStart + "/" + yearStart + text.str_Forecast;

            for (int i = yearStart; i <= yearEnd; i++)
            {
                if (yearStart == yearEnd)
                {
                    for (int j = monthStart; j <= monthEnd; j++)
                    {
                        month = j + "/" + i + text.str_Forecast;
                        dt.Columns.Add(month, typeof(float));
                       
                    }
                }
                else
                {
                    if (i == yearStart)
                    {
                        for (int j = monthStart; j <= 12; j++)
                        {
                            month = j + "/" + i + text.str_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                    else if (i == yearEnd)
                    {
                        for (int j = 1; j <= monthEnd; j++)
                        {
                            month = j + "/" + i + text.str_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            month = j + "/" + i + text.str_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                }
            }
            #endregion

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

                        if(colName.Contains(text.str_Forecast) || colName.Contains(text.str_Delivered))
                        {
                            dgv.Columns[colName].Visible = false;
                        }
                        else if(colName.Contains(text.str_EstBalance))
                        {
                            dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }

                dgv.Columns[text.Header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_PendingOrder].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_ReadyStock].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dgv.Columns[text.Header_PendingOrder].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

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

        #region Load Data/Validation

        private void ResetPage()
        {
            tool.DoubleBuffered(dgvOrder, true);
            tool.DoubleBuffered(dgvAlertSummary, true);

            PanelUISetting(true, true);

            CMBReset();
            DateReset();

            dt_MatUsed_Forecast = NewMatUsedTable();
            dt_DeliveredData = NewOrderAlertSummaryTable();
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

            tool.LoadCustomerAndAllToComboBox(cmbCustomer);
            LoadItemTypeToCMB(cmbItemType);
            cmbCustomer.Text = tool.getCustName(1);
           //cmbCustomer.Enabled = false;
        }

        private void DateReset()
        {
            loadMonthDataToCMB(cmbMonthFrom);
            loadYearDataToCMB(cmbYearFrom);

            loadMonthDataToCMB(cmbMonthTo);
            loadYearDataToCMB(cmbYearTo);

            cmbMonthFrom.Text = DateTime.Now.Month.ToString();
            cmbYearFrom.Text = DateTime.Now.Year.ToString();

            cmbMonthTo.Text = DateTime.Now.AddMonths(3).Month.ToString();
            cmbYearTo.Text = DateTime.Now.AddMonths(3).Year.ToString();
        }

        private void MainFilterOnly(bool showMainFilterOnly)
        {
            if (showMainFilterOnly)
            {
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);//row 2
                btnFilter.Text = textMoreFilters;
            }
            else
            {
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 110f);//row 2
                btnFilter.Text = textHideFilters;
            }
        }
        private void PanelUISetting(bool ShowOrderRecord,bool ShowOrderAlert)
        {
            btnFilter.Text = textMoreFilters;

            if (ShowOrderRecord && ShowOrderAlert)
            {
                btnShowOrHideOrderAlert.Text = BTN_HIDE_MATERIAL_FORECAST;
                btnOrderAlertOnly.Text = BTN_MATERIAL_FORECAST_ONLY;

                //order record & alert
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Absolute, 75f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Percent, 50f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 30f);
                tlpMain.RowStyles[5] = new RowStyle(SizeType.Percent, 50f);

                dgvOrder.Visible = true;
                dgvAlertSummary.Visible = true;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 130f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 135f);
            }
            else if (ShowOrderRecord && !ShowOrderAlert)
            {
                btnShowOrHideOrderAlert.Text = BTN_SHOW_MATERIAL_FOREACST;
                btnOrderAlertOnly.Text = BTN_MATERIAL_FORECAST_ONLY;

                //order record 
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Absolute, 75f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[5] = new RowStyle(SizeType.Percent, 0f);

                dgvOrder.Visible = true;
                dgvAlertSummary.Visible = false;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
            }
            else if (!ShowOrderRecord && ShowOrderAlert)
            {
                btnShowOrHideOrderAlert.Text = BTN_HIDE_MATERIAL_FORECAST;
                btnOrderAlertOnly.Text = BTN_SHOW_ORDER_RECORD;

                //order alert 
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 30f);
                tlpMain.RowStyles[5] = new RowStyle(SizeType.Percent, 100f);

                dgvOrder.Visible = false;
                dgvAlertSummary.Visible = true;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 130f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 135f);
            }
            else
            {
                btnShowOrHideOrderAlert.Text = BTN_SHOW_MATERIAL_FOREACST;

                //order alert 
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[5] = new RowStyle(SizeType.Percent, 100f);

                dgvOrder.Visible = false;
                dgvAlertSummary.Visible = false;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
            }
        }
        private void frmMaterialUsedReport_NEW_Load(object sender, EventArgs e)
        {
            loaded = false;

            PanelUISetting(true, false);

            tool.DoubleBuffered(dgvOrder, true);
            tool.DoubleBuffered(dgvAlertSummary, true);

            cmbStatusSearch.SelectedIndex = 1;

            resetForm();

           
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

        private void getStartandEndDate()
        {
            if (cmbCustomer.SelectedIndex != -1)
            {
                int month_FROM = int.TryParse(cmbMonthFrom.Text, out month_FROM) ? month_FROM : DateTime.Now.Month;
                int year_FROM = int.TryParse(cmbYearFrom.Text, out year_FROM) ? year_FROM : DateTime.Now.Year;

                int month_TO = int.TryParse(cmbMonthTo.Text, out month_TO) ? month_TO : DateTime.Now.Month;
                int year_TO = int.TryParse(cmbYearTo.Text, out year_TO) ? year_TO : DateTime.Now.Year;

                if (cmbCustomer.Text.Equals(tool.getCustName(1)))
                {
                    dtpFrom.Value = tool.GetPMMAStartDate(month_FROM, year_FROM);
                    dtpTo.Value = tool.GetPMMAEndDate(month_TO, year_TO);
                }
                else
                {
                    dtpFrom.Value = new DateTime(year_FROM, month_FROM, 1);
                    dtpTo.Value = new DateTime(year_TO, month_TO, DateTime.DaysInMonth(year_TO, month_TO));
                }
            }
        }

        private bool ifCustomerItem(string itemCode, DataTable dt_Join, DataTable dt_CustItem)
        {
            bool isCustomerItem = false;

            foreach (DataRow row in dt_CustItem.Rows)
            {
                if (itemCode == row[dalItem.ItemCode].ToString())
                {
                    return true;
                }
            }

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    string parentCode = join["parent_code"].ToString();
                    isCustomerItem = ifCustomerItem(parentCode, dt_Join, dt_CustItem);
                }
            }

            return isCustomerItem;
        }

        private float DeliveredToCustomerQty(string itemCode, DateTime dateFrom, DateTime dateTo)
        {
            float DeliveredQty = 0;

            DT_PART_TRANSFER.AcceptChanges();

            bool itemFound = false;

            foreach (DataRow trfHistRow in DT_PART_TRANSFER.Rows)
            {
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();
                DateTime trfDate = DateTime.TryParse(trfHistRow[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                bool dateMatched = trfDate >= dateFrom;
                dateMatched &= trfDate <= dateTo;

                if(itemFound && itemCode != trfHistRow[dalTrfHist.TrfItemCode].ToString())
                {
                    break;
                }

                if (itemCode == trfHistRow[dalTrfHist.TrfItemCode].ToString() && result == text.Passed && dateMatched)
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

        private float DeliveredToCustomerQty(string customer, string itemCode, DataTable dt_Join, DataTable dt_ToCustomer)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;

            DataTable dt_Cust = dalCust.FullSelect();

            row_Delivered = dt_DeliveredData.NewRow();

            if (cmbItemType.Text.Equals(reportType_Delivered))
            {
                foreach (DataRow trfHistRow in dt_ToCustomer.Rows)
                {
                    dbItemCode = trfHistRow[dalTrfHist.TrfItemCode].ToString();
                    string result = trfHistRow[dalTrfHist.TrfResult].ToString();

                    string trf_To = trfHistRow[dalTrfHist.TrfTo].ToString();

                    bool matched = itemCode == dbItemCode && result == "Passed" && tool.IfCustomer(dt_Cust, trf_To);

                    if (!customer.Equals("All"))
                    {
                        matched = matched && trf_To.ToUpper().Equals(customer.ToUpper());
                    }

                    //get single out
                    if (matched)
                    {
                        dbItemName = trfHistRow[dalItem.ItemName].ToString();
                        int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                        DeliveredQty += qty;

                    }

                }
            }

            else if (cmbItemType.Text.Equals(reportType_ReadyStock))
            {
                DeliveredQty = tool.getStockQtyFromDataTable(dt_Item, itemCode);
                dbItemName = tool.getItemNameFromDataTable(dt_Item, itemCode);
            }

            else if (cmbItemType.Text.Equals(reportType_Forecast))
            {
                DeliveredQty = tool.getItemForecast(dt_ItemForecast, itemCode, Convert.ToInt32(cmbYearFrom.Text), Convert.ToInt32(cmbMonthFrom.Text));
                dbItemName = tool.getItemNameFromDataTable(dt_Item, itemCode);

               if(cbDeductUsedStock.Checked)
                {
                    float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);

                    DeliveredQty -= readyStock;
                }

                if (DeliveredQty < 0)
                    DeliveredQty = 0;


            }


            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[header_Forecast] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * DeliveredToCustomerQty(customer, parentCode, dt_Join, dt_ToCustomer);
                }
            }

            return DeliveredQty;
        }

        private float GetForecastQty(string customer, string itemCode, DataTable dt_Join, DataTable dt_ToCustomer, string Month, string year)
        {
            float DeliveredQty = 0;
            string dbItemName = null;

            DataRow row_Delivered;

            DataTable dt_Cust = dalCust.FullSelect();

            row_Delivered = dt_DeliveredData.NewRow();

            DeliveredQty = tool.getItemForecast(dt_ItemForecast, itemCode, Convert.ToInt32(year), Convert.ToInt32(Month));
            dbItemName = tool.getItemNameFromDataTable(dt_Item, itemCode);

            if (cbDeductUsedStock.Checked)
            {
                float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);

                DeliveredQty -= readyStock;
            }

            if (DeliveredQty < 0)
                DeliveredQty = 0;

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[header_Forecast] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * GetForecastQty(customer, parentCode, dt_Join, dt_ToCustomer, Month, year);
                }
            }

            return DeliveredQty;
        }

        #region NEW Method

        private void New_PrintForecastSummary()
        {
            dgvAlertSummary.DataSource = null;

            if (DT_MATERIAL_FORECAST_SUMMARY != null)
            {
                string Material_Type = cmbItemType.Text;

                DataTable dt_MaterialList = DT_MATERIAL_FORECAST_SUMMARY.Clone();

                if(Material_Type.ToUpper() != text.Cmb_All)
                {
                    foreach (DataRow row in DT_MATERIAL_FORECAST_SUMMARY.Rows)
                    {
                        string matType = row[text.Header_Type].ToString();
                        string stock = row[text.Header_ReadyStock].ToString();

                        if (matType == Material_Type && stock != "-1")
                        {
                            dt_MaterialList.ImportRow(row);
                        }
                    }
                }
                

                if (dt_MaterialList.Rows.Count > 0)
                {

                    dt_MaterialList.DefaultView.Sort = text.Header_PartName + " ASC";
                    dt_MaterialList = dt_MaterialList.DefaultView.ToTable();

                    dt_MaterialList = NEW_RearrangeIndex(dt_MaterialList);

                    dgvAlertSummary.DataSource = dt_MaterialList;
                    dgvUIEdit(dgvAlertSummary);
                    dgvAlertSummary.ClearSelection();
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

                            if (cbZeroStockType.Checked && itemType != text.Cat_Part)
                            {
                                Child_Stock = tool.getPMMAQtyFromDataTable(dt_Item, itemCode);
                            }
                           
                            newRow[text.Header_Index] = childIndex;
                            newRow[header_ParentIndex] = parentIndex;
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

                            if (cbZeroCostOnly.Checked)
                            {
                                partWeight = float.TryParse(item[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                runnerWeight = float.TryParse(item[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                                if (partWeight <= 0)
                                {
                                    partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                    runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                                }

                            }
                            else
                            {
                                partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                            }

                            float itemWeight = partWeight + runnerWeight;

                            float wastage = item[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(item[dalItem.ItemWastage]);

                            newRow[text.Header_PartWeight_G] = partWeight;
                            newRow[text.Header_RunnerWeight_G] = runnerWeight;
                            newRow[text.Header_WastageAllowed_Percentage] = wastage;
                            newRow[text.Header_Unit] = text.Unit_Piece;

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
                                newRow[header_ParentIndex] = childIndex;
                                newRow[text.Header_GroupLevel] = groupLevel + 2;
                                newRow[text.Header_Type] = text.Cat_RawMat;
                                newRow[text.Header_PartCode] = RawMaterial;
                                newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, RawMaterial);

                                if(RawMaterial == "ABS 450Y MH1")
                                {
                                    float TEST = 0;
                                }

                                if (cbZeroStockType.Checked)
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);
                                }
                                else
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, RawMaterial), 2);
                                }

                                newRow[text.Header_PendingOrder] = (float)Math.Round((double)tool.getOrderQtyFromDataTable(dt_Item, RawMaterial), 2);
                                newRow[text.Header_Unit] = text.Unit_KG;

                                DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                                ChildQtyCalculation(subChildIndex - 1);

                            }

                            if (!string.IsNullOrEmpty(ColorMaterial))
                            {
                                newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                                newRow[text.Header_Index] = subChildIndex++;
                                newRow[header_ParentIndex] = childIndex;
                                newRow[text.Header_GroupLevel] = groupLevel + 2;
                                newRow[text.Header_Type] = tool.getCatNameFromDataTable(dt_Item, ColorMaterial);
                                newRow[text.Header_PartCode] = ColorMaterial;
                                newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, ColorMaterial);

                                if (cbZeroStockType.Checked)
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                }
                                else
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                }

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

        private float getItemForecast(string itemCode, int year, int month)
        {
            float forecast = -1;

            dt_ItemForecast.AcceptChanges();

            foreach (DataRow row in dt_ItemForecast.Rows)
            {
                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                bool itemMatch = code == itemCode && yearData == year && monthData == month;

                if (itemMatch)
                {
                    forecast =  float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i) ? i : -1;
                    row.Delete();
                    break;
                }
            }

            dt_ItemForecast.AcceptChanges();

            return forecast;
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
                        string parentIndex = row[header_ParentIndex].ToString();

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

        private void NEW_GetSummaryForecastMatUsedData()
        {
            #region data setting 

            frmLoading.ShowLoadingScreen();

            dgvAlertSummary.DataSource = null;

            dt_DeliveredData = Product_SummaryDataTable();

            string custName = cmbCustomer.Text;

            string from = dtpFrom.Value.ToString("yyyy/MM/dd");
            string to = dtpTo.Value.ToString("yyyy/MM/dd");

            DataTable dt_PartTrfHist;
            DataTable dt_CustProduct;


            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();

            //get all item info
            dt_Item = dalItem.Select();

            bool PMMACustomer = false;

            if (custName.Equals("All"))
            {
                dt_CustProduct = dalItemCust.Select();
                dt_PartTrfHist = dalTrfHist.rangeItemToAllCustomerSearch(from, to);
                dt_ItemForecast = dalItemForecast.Select();
            }
            else
            {
                dt_CustProduct = dalItemCust.custSearch(custName);
                dt_PartTrfHist = dalTrfHist.rangeItemToCustomerSearch(custName, from, to);
                dt_ItemForecast = dalItemForecast.Select(tool.getCustID(custName).ToString());

                PMMACustomer = cmbCustomer.Text.Equals(tool.getCustName(1));

            }

            DT_PART_TRANSFER = dt_PartTrfHist.Copy();

            dt_ItemForecast.DefaultView.Sort = dalItemForecast.ItemCode + " ASC, " + dalItemForecast.ForecastYear + " DESC, " + dalItemForecast.ForecastMonth + " DESC";
            dt_ItemForecast = dt_ItemForecast.DefaultView.ToTable();

            DT_PART_TRANSFER.DefaultView.Sort = dalTrfHist.TrfItemCode + " ASC, " + dalTrfHist.TrfDate + " ASC";
            DT_PART_TRANSFER = DT_PART_TRANSFER.DefaultView.ToTable();


            if (!cbShowTerminatedItem.Checked)
            {
                dt_CustProduct = RemoveTerminatedProduct(dt_CustProduct);
            }

            #endregion 

            //generate product forecast summary (with delivered data)
            if (dt_CustProduct != null)
            {
                #region data setting 2

                dt_CustProduct.DefaultView.Sort = dalItem.ItemCode + " ASC";
                dt_CustProduct = dt_CustProduct.DefaultView.ToTable();

                int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

                int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                int yearEnd = Convert.ToInt32(cmbYearTo.Text);

                DateTime dateStart = new DateTime(yearStart, monthStart, 1);
                DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

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

                    string ProductCode = ProductRow[dalItem.ItemCode].ToString();
                    string ProductName = ProductRow[dalItem.ItemName].ToString();
                    float Ready_Stock = float.TryParse(ProductRow[dalItem.ItemStock].ToString(), out Ready_Stock) ? Ready_Stock : 0;
                    float Pending_Order = float.TryParse(ProductRow[dalItem.ItemOrd].ToString(), out Pending_Order) ? Pending_Order : 0;
                    float Color_Rate = float.TryParse(ProductRow[dalItem.ItemMBRate].ToString(), out Color_Rate) ? Color_Rate : 0;
                    float Forecast = 0;
                    float Delivered = 0;
                    float Required = 0;
                    float Insufficient = 0;

                    newRow[text.Header_Index] = index;
                    newRow[header_ParentIndex] = 0;
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
                            Forecast = getItemForecast(ProductCode, i, j);

                            Forecast = Forecast < 0 ? 0 : Forecast;

                            if (cbDeductDeliveredQty.Checked)
                            {
                                Delivered = DeliveredToCustomerQty(ProductCode, dateFrom, dateTo);
                                newRow[MonthlyDelivered] = Delivered;
                            }

                            Required = Forecast - Delivered;

                            Required = Required < 0 ? 0 : Required;

                            Insufficient = Ready_Stock - Required;

                            Insufficient = Insufficient > 0 ? 0 : Insufficient;

                            newRow[MonthlyForecast] = Forecast;
                            newRow[MonthlyRequired] = Required;
                            newRow[MonthlyInsufficient] = Insufficient;
                        }
                    }

                    #region add item weight info

                    float partWeight = 0;
                    float runnerWeight = 0;

                    if (cbZeroCostOnly.Checked)
                    {
                        partWeight = float.TryParse(ProductRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                        runnerWeight = float.TryParse(ProductRow[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                        if (partWeight <= 0)
                        {
                            partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                            runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                        }

                    }
                    else
                    {
                        partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                        runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                    }

                    float itemWeight = partWeight + runnerWeight;

                    float wastage = ProductRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(ProductRow[dalItem.ItemWastage]);

                    newRow[text.Header_PartWeight_G] = partWeight;
                    newRow[text.Header_RunnerWeight_G] = runnerWeight;
                    newRow[text.Header_WastageAllowed_Percentage] = wastage;
                    newRow[text.Header_Unit] = text.Unit_Piece;

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
                        newRow[header_ParentIndex] = index;
                        newRow[text.Header_GroupLevel] = 2;
                        newRow[text.Header_Type] = text.Cat_RawMat;
                        newRow[text.Header_PartCode] = RawMaterial;
                        newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, RawMaterial);


                        if (cbZeroStockType.Checked)
                        {
                            newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);
                        }
                        else
                        {
                            newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, RawMaterial), 2);
                        }

                        newRow[text.Header_PendingOrder] = (float)Math.Round((double)tool.getOrderQtyFromDataTable(dt_Item, RawMaterial), 2);

                        newRow[text.Header_Unit] = text.Unit_KG;

                        DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                        ChildQtyCalculation(childIndex-1);
                    }

                    if (!string.IsNullOrEmpty(ColorMaterial))
                    {
                        newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                        newRow[text.Header_Index] = childIndex++;
                        newRow[header_ParentIndex] = index;
                        newRow[text.Header_GroupLevel] = 2;
                        newRow[text.Header_Type] = tool.getCatNameFromDataTable(dt_Item, ColorMaterial);
                        newRow[text.Header_PartCode] = ColorMaterial;
                        newRow[text.Header_PartName] = tool.getItemNameFromDataTable(dt_Item, ColorMaterial);

                        if (cbZeroStockType.Checked)
                        {
                            newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);
                        }
                        else
                        {
                            newRow[text.Header_ReadyStock] = (float)Math.Round((double)tool.getStockQtyFromDataTable(dt_Item, ColorMaterial), 2);
                        }

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

                //stock deduct
                if(cbDeductUsedStock.Checked)
                {
                    StockDeductCalculation();
                    DT_PRODUCT_FORECAST_SUMMARY.DefaultView.Sort = text.Header_IndexMarking + " ASC";
                    DT_PRODUCT_FORECAST_SUMMARY = DT_PRODUCT_FORECAST_SUMMARY.DefaultView.ToTable();

                }

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

        private void StockDeductCalculation()//Level 1 only
        {
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                float balStock = 0;
                string Last_PartCode = null;
                bool LevelFound = false;

                DT_PRODUCT_FORECAST_SUMMARY.DefaultView.Sort = text.Header_Type + " ASC," + text.Header_PartCode + " ASC," + text.Header_GroupLevel + " ASC";
                DT_PRODUCT_FORECAST_SUMMARY = DT_PRODUCT_FORECAST_SUMMARY.DefaultView.ToTable();
                int Level_Checking = 1;

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
                            balStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out float i) ? i : 0;

                        }

                        if (Level == Level_Checking && Last_PartCode == itemCode)
                        {
                            LevelFound = true;

                            foreach (DataColumn col in DT_PRODUCT_FORECAST_SUMMARY.Columns)
                            {
                                string colName = col.ColumnName;

                                if (colName.Contains(text.str_RequiredQty))
                                {
                                    float Required = float.TryParse(row[colName].ToString(), out float j) ? j : 0;
                                    float Insufficient = 0;

                                    balStock -= Required;

                                    if (balStock < 0)
                                    {
                                        Insufficient = (float) balStock;
                                        balStock = 0;
                                    }
                                    else
                                    {
                                        Insufficient = 0;
                                    }

                                    row[colName.Replace(text.str_RequiredQty, text.str_InsufficientQty)] = Insufficient;
                                    row[text.Header_BalStock] = balStock;
                                }
                            }

                          
                        }
                    }

                }

               

                if(LevelFound)
                {
                    StockDeductCalculation(Level_Checking + 1);
                }
            }
        }

        private void StockDeductCalculation(int Level_Checking)// Level 2 above
        {
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                float balStock = 0;
                string Last_PartCode = null;
                bool LevelFound = false;

    
                foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                {
                    string itemType = row[text.Header_Type].ToString();
                    string indexMarking = row[text.Header_Index].ToString();

                  
                    if (true)//itemType == text.Cat_Part
                    {
                        int Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out Level) ? Level : 0;
                        string itemCode = row[text.Header_PartCode].ToString();


                        if (Last_PartCode != itemCode)
                        {
                            Last_PartCode = itemCode;

                            float ReadyStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out ReadyStock) ? ReadyStock : 0;


                            //get lastest bal stock (from lower level)
                            balStock = GetLatestBalStock(Level_Checking, itemCode, ReadyStock);
                            //balStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out float i) ? i : 0;

                            balStock = balStock < 0 ? 0 : balStock;
                        }

                        if (Level == Level_Checking && Last_PartCode == itemCode)
                        {
                            LevelFound = true;

                            foreach (DataColumn col in DT_PRODUCT_FORECAST_SUMMARY.Columns)
                            {
                                string colName = col.ColumnName;

                                if (colName.Contains(text.str_InsufficientQty))
                                {
                                    DataRow ParentRow = GetParentDatarow(row[text.Header_ParentIndex].ToString());

                                    float Required = float.TryParse(ParentRow[colName].ToString(), out float j) ? j * -1 : 0;

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

                                    //if (itemType == text.Cat_Part)
                                    //    row[colName] = Required;

                                    row[colName.Replace(text.str_InsufficientQty, text.str_RequiredQty)] = Required;
                                    row[colName] = Insufficient;
                                    row[text.Header_BalStock] = balStock;
                                }

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

                            if (colName.Contains(text.str_InsufficientQty))
                            {
                                string EstBalance_ColName = colName.Replace(text.str_InsufficientQty, text.str_EstBalance);
                                float Insufficient = qty + previous_qty;

                                
                                readyStock = (float)decimal.Round((decimal) (readyStock + Insufficient), 3);


                                newRow[EstBalance_ColName] = readyStock;
                            }
                           
                        }

                    }
                }

                DT_MATERIAL_FORECAST_SUMMARY.Rows.Add(newRow);
            }
        }

        #endregion

        private DataTable NEW_RearrangeIndex(DataTable dataTable)
        {
            int index = 1;

            if(dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    row[text.Header_Index] = index++;
                }
            }

            return dataTable;
        }

        private void frmOrderAlert_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewOrdFormOpen = false;
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
            loaded = false;

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            loadOrderRecord();

            txtOrdSearch.Clear();

            loaded = true;
            lblUpdatedTime2.Text = DateTime.Now.ToString();
            Cursor = Cursors.Arrow; // change cursor to normal type
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

        private void loadOrderRecord()
        {
            loaded = false;

            DataTable dtOrder = NewOrderRecordTable();
            DataRow dtOrder_row;
            DataTable dt_itemInfo = dalItem.Select();

            DataTable dt_OrderAction = dalOrderAction.Select();
            dt_OrderAction.DefaultView.Sort = "ord_id DESC";
            dt_OrderAction = dt_OrderAction.DefaultView.ToTable();

            string keywords = txtOrdSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
               
                DataTable dt;

                if (cbCodeNameSearch.Checked)
                {
                    dt = dalOrd.Search(keywords);
                }
                else if (cbPOSearch.Checked)
                {
                    cmbStatusSearch.Text = text.Cmb_All;
                    dt = dalOrd.PONOSearch(keywords);
                }
                else
                {
                    cmbStatusSearch.Text = text.Cmb_All;
                    dt = dalOrd.IDSearch(keywords);
                }

                string statusSearch = cmbStatusSearch.Text;
                dt.DefaultView.Sort = "ord_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                foreach (DataRow ord in sortedDt.Rows)
                {
                    string ordStatus = ord["ord_status"].ToString();

                    if (statusSearch.Equals("ALL") || ordStatus.Equals(statusSearch) || statusSearch.Contains(ordStatus))
                    {

                        int orderID = Convert.ToInt32(ord["ord_id"].ToString());

                        if (orderID > 8)
                        {
                            dtOrder_row = dtOrder.NewRow();

                            string itemCode = ord["ord_item_code"].ToString();
                            string ordID = ord["ord_id"].ToString();
                           

                            int ordPONo = ord["ord_po_no"] == DBNull.Value ? -1 : Convert.ToInt32(ord["ord_po_no"].ToString());


                            if (ordPONo <= 0 && (ordStatus == status_Received || ordStatus == status_Pending))
                            {
                                ordPONo = GetPONoFromActionRecord(ordID, dt_OrderAction);

                                //update PO NO to DB
                                uOrd.ord_id = int.TryParse(ordID, out int i) ? i : -1;
                                uOrd.ord_po_no = ordPONo;
                                uOrd.ord_updated_date = DateTime.Now;
                                uOrd.ord_updated_by = MainDashboard.USER_ID;

                                if (!dalOrd.POUpdate(uOrd))
                                {
                                    MessageBox.Show("Failed to update PO No to Order!");

                                }
                            }

                            dtOrder_row[headerID] = ordID;
                            //lblDebug.Text = "before date assign";

                            DateTime requiredDate = DateTime.ParseExact(Convert.ToDateTime(ord["ord_required_date"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                            //lblDebug.Text = "After requiredDateAssign";
                            dtOrder_row[headerDateRequired] = requiredDate;
                            //lblDebug.Text = "After date assign";
                            dtOrder_row[headerType] = ord["ord_type"].ToString();
                            dtOrder_row[headerCat] = tool.getCatNameFromDataTable(dt_itemInfo, itemCode);
                            dtOrder_row[headerCode] = itemCode;
                            dtOrder_row[headerName] = ord["item_name"].ToString();
                            dtOrder_row[headerPONO] = ordPONo;
                            dtOrder_row[headerOrdered] = ord["ord_qty"].ToString();
                            dtOrder_row[headerPending] = ord["ord_pending"].ToString();
                            dtOrder_row[headerReceived] = ord["ord_received"].ToString();
                            dtOrder_row[headerUnit] = ord["ord_unit"].ToString();
                            dtOrder_row[headerStatus] = ordStatus;

                            dtOrder.Rows.Add(dtOrder_row);
                        }
                    }
                }
            }

            dgvOrder.DataSource = null;

            if (dtOrder.Rows.Count > 0)
            {
                dtOrder.DefaultView.Sort = "ID DESC";
                dgvOrder.DataSource = dtOrder;
                dgvOrderUIEdit(dgvOrder);
                dgvOrder.ClearSelection();
            }

            loaded = true;
        }

        private void loadOrderRecordFromStatusSearch()
        {
            loaded = false;

            DataTable dtOrder = NewOrderRecordTable();
            DataRow dtOrder_row;
            DataTable dt_itemInfo = dalItem.Select();

            DataTable dt_OrderAction = dalOrderAction.Select();
            dt_OrderAction.DefaultView.Sort = "ord_id DESC";
            dt_OrderAction = dt_OrderAction.DefaultView.ToTable();

            string keywords = txtOrdSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {

                DataTable dt;

                if (cbCodeNameSearch.Checked)
                {
                    dt = dalOrd.Search(keywords);
                }
                else if (cbPOSearch.Checked)
                {
                    dt = dalOrd.PONOSearch(keywords);
                }
                else
                {
                    dt = dalOrd.IDSearch(keywords);
                }

                string statusSearch = cmbStatusSearch.Text;
                dt.DefaultView.Sort = "ord_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                foreach (DataRow ord in sortedDt.Rows)
                {
                    string ordStatus = ord["ord_status"].ToString();

                    if (statusSearch.Equals("ALL") || ordStatus.Equals(statusSearch) || statusSearch.Contains(ordStatus))
                    {

                        int orderID = Convert.ToInt32(ord["ord_id"].ToString());

                        if (orderID > 8)
                        {
                            dtOrder_row = dtOrder.NewRow();

                            string itemCode = ord["ord_item_code"].ToString();
                            string ordID = ord["ord_id"].ToString();


                            int ordPONo = ord["ord_po_no"] == DBNull.Value ? -1 : Convert.ToInt32(ord["ord_po_no"].ToString());


                            if (ordPONo <= 0 && (ordStatus == status_Received || ordStatus == status_Pending))
                            {
                                ordPONo = GetPONoFromActionRecord(ordID, dt_OrderAction);

                                //update PO NO to DB
                                uOrd.ord_id = int.TryParse(ordID, out int i) ? i : -1;
                                uOrd.ord_po_no = ordPONo;
                                uOrd.ord_updated_date = DateTime.Now;
                                uOrd.ord_updated_by = MainDashboard.USER_ID;

                                if (!dalOrd.POUpdate(uOrd))
                                {
                                    MessageBox.Show("Failed to update PO No to Order!");

                                }
                            }

                            dtOrder_row[headerID] = ordID;
                            //lblDebug.Text = "before date assign";

                            DateTime requiredDate = DateTime.ParseExact(Convert.ToDateTime(ord["ord_required_date"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                            //lblDebug.Text = "After requiredDateAssign";
                            dtOrder_row[headerDateRequired] = requiredDate;
                            //lblDebug.Text = "After date assign";
                            dtOrder_row[headerType] = ord["ord_type"].ToString();
                            dtOrder_row[headerCat] = tool.getCatNameFromDataTable(dt_itemInfo, itemCode);
                            dtOrder_row[headerCode] = itemCode;
                            dtOrder_row[headerName] = ord["item_name"].ToString();
                            dtOrder_row[headerPONO] = ordPONo;
                            dtOrder_row[headerOrdered] = ord["ord_qty"].ToString();
                            dtOrder_row[headerPending] = ord["ord_pending"].ToString();
                            dtOrder_row[headerReceived] = ord["ord_received"].ToString();
                            dtOrder_row[headerUnit] = ord["ord_unit"].ToString();
                            dtOrder_row[headerStatus] = ordStatus;

                            dtOrder.Rows.Add(dtOrder_row);
                        }
                    }
                }
            }

            dgvOrder.DataSource = null;

            if (dtOrder.Rows.Count > 0)
            {
                dtOrder.DefaultView.Sort = "ID DESC";
                dgvOrder.DataSource = dtOrder;
                dgvOrderUIEdit(dgvOrder);
                dgvOrder.ClearSelection();
            }

            loaded = true;
        }

        private void refreshOrderRecord(int orderID)
        {
            //dgvOrderAlert.Rows.Clear();
            loadOrderRecord();
            dgvOrder.ClearSelection();
            if (orderID != -1)
            {
                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    if (Convert.ToInt32(row.Cells[headerID].Value.ToString()).Equals(orderID))
                    {
                        int rowIndex = row.Index;
                        row.Selected = true;
                        dgvOrder.FirstDisplayedScrollingRowIndex = rowIndex;

                        break;
                    }
                }
            }
        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "OrderReport(" + cmbStatusSearch.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void copyAlltoClipboard()
        {
            dgvOrder.SelectAll();
            DataObject dataObj = dgvOrder.GetClipboardContent();
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

        #region UI Action

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                MainFilterOnly(false);

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                MainFilterOnly(true);

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cust = cmbCustomer.Text;
            dgvAlertSummary.DataSource = null;

            if (cmbCustomer.SelectedIndex != -1)
            {
                getStartandEndDate();

                if (cust.Equals(tool.getCustName(1)))
                {
                    lblChangeDate.Visible = true;
                    cbZeroCostOnly.Checked = true;

                }
                else
                {
                    lblChangeDate.Visible = false;
                    cbZeroCostOnly.Checked = false;
                }
            }
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reportType = cmbItemType.Text;
            dt_DeliveredData = NewOrderAlertSummaryTable();
            dgvAlertSummary.DataSource = null;
        }

        private void lblChangeDate_Click(object sender, EventArgs e)
        {
            lblChangeDate.ForeColor = Color.Purple;
            frmPMMADateEdit frm = new frmPMMADateEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            lblChangeDate.ForeColor = Color.Blue;

            if (frmPMMADateEdit.dateChanged)
            {
                getStartandEndDate();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else if (cmbItemType.SelectedIndex <= -1)
            {
                MessageBox.Show("Please select a item type.");
            }
            else
            {
                MainFilterOnly(true);
                NEW_GetSummaryForecastMatUsedData();
            }
        }
      
        #endregion

        private void cmbMonthFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                int yearStart = Convert.ToInt32(cmbYearFrom.Text);

                DateTime dateStart = new DateTime(yearStart, monthStart, 1);

                cmbMonthTo.Text = dateStart.AddMonths(3).Month.ToString();
                cmbYearTo.Text = dateStart.AddMonths(3).Year.ToString();
            }
           
        }

        private void btnShowOrHideOrderAlert_Click(object sender, EventArgs e)
        {
            if(btnShowOrHideOrderAlert.Text.Equals(BTN_HIDE_MATERIAL_FORECAST))
            {
                if (btnOrderAlertOnly.Text.Equals(BTN_MATERIAL_FORECAST_ONLY))
                {
                    PanelUISetting(true, false);
                }
                else
                {
                    PanelUISetting(false, false);
                }
            }
            else
            {

                if (btnOrderAlertOnly.Text.Equals(BTN_MATERIAL_FORECAST_ONLY))
                {
                    PanelUISetting(true, true);
                }
                else
                {
                    PanelUISetting(false, true);
                }
            }
        }

        private void btnOrderAlertOnly_Click(object sender, EventArgs e)
        {
            string text = btnOrderAlertOnly.Text;

            if (text == BTN_MATERIAL_FORECAST_ONLY)
            {
                PanelUISetting(false, true);
            }
            else if (text == BTN_SHOW_ORDER_RECORD)
            {
                if (btnShowOrHideOrderAlert.Text.Equals(BTN_SHOW_MATERIAL_FOREACST))
                {
                    PanelUISetting(true, false);
                }
                else
                {
                    PanelUISetting(true, true);
                }
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void cmbMonthTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStartandEndDate();
        }

        private void cmbYearFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStartandEndDate();
        }

        private void cmbYearTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStartandEndDate();
        }

        private void cbZeroCostOnly_CheckedChanged(object sender, EventArgs e)
        {
            dgvAlertSummary.DataSource = null;

            if(cbZeroCostOnly.Checked)
            {
                cbZeroStockType.Checked = true;
                cbZeroStockType.Enabled = true;

            }
            else
            {
                cbZeroStockType.Checked = false;
                cbZeroStockType.Enabled = false;
            }
        }

        private void cbZeroStockType_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvAlertSummary_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                string itemCode = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();

                int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

                int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                int yearEnd = Convert.ToInt32(cmbYearTo.Text);

                //check date
                DateTime dateStart = new DateTime(yearStart, monthStart, 1);
                DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

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

                frmOrderAlertDetail_NEW frm = new frmOrderAlertDetail_NEW(DT_PRODUCT_FORECAST_SUMMARY, itemCode, dateStart, dateEnd);

                frm.StartPosition = FormStartPosition.CenterScreen;

                frm.ShowDialog();

            }
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

        #region order status change

        private void orderRequest(int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //get data from datagridview
            // item category, item code, item name, order id, 
            string id = "";
            string type = "";
            string cat = "";
            string itemCode = "";
            string itemName = "";
            string qty = "";
            string unit = "";
            string date = "";
            DataTable dt = dalOrd.Select(orderID);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow ord in dt.Rows)
                {
                    id = ord["ord_id"].ToString();
                    cat = ord["item_cat"].ToString();
                    itemCode = ord["ord_item_code"].ToString();
                    itemName = ord["item_name"].ToString();
                    type = ord["ord_type"].ToString();
                    qty = ord["ord_qty"].ToString();
                    unit = ord["ord_unit"].ToString();
                    date = Convert.ToDateTime(ord["ord_required_date"].ToString()).ToString("dd/MM/yyyy");

                }
            }

            frmOrderRequest frm = new frmOrderRequest(id, cat, itemCode, itemName, qty, unit, date, type);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//order request

            if (frmOrderRequest.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                frmOrderRequest.orderSuccess = false;
                resetForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }

            refreshOrderRecord(orderID);

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderApprove(int rowIndex, int orderID)
        {
            try
            {
                if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                {
                    string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
                    string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
                    //string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
                    DateTime requiredDate = Convert.ToDateTime(dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value);
                    string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
                    string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
                    string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();
                    int po_no = dgvOrder.Rows[rowIndex].Cells[headerPONO].Value == DBNull.Value ? -1 : Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerPONO].Value.ToString());


                    //approve form
                    frmOrderApprove frm = new frmOrderApprove(orderID.ToString(), requiredDate, itemName, itemCode, qty, unit, type, po_no);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                    if (frmOrderApprove.orderApproved)//if order approved from approve form, then change order status from requesting to pending
                    {
                        dalItem.orderAdd(itemCode, frmOrderApprove.FINAL_ORDER_QTY);//add order qty to item
                        refreshOrderRecord(orderID);
                        orderApproved = false;
                    }
                }
                else
                {
                    MessageBox.Show("Action denied. Please contact admin for this action.");
                    tool.historyRecord(text.System, "Action denied. Please contact admin for this action.(frmOrder)", DateTime.Now, MainDashboard.USER_ID);

                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                refreshOrderRecord(orderID);
            }

        }

        private void orderEdit(int rowIndex, int orderID)
        {
            try
            {
                if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                {
                    string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
                    string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
                    //string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
                    DateTime requiredDate = Convert.ToDateTime(dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value);
                    string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
                    string received = dgvOrder.Rows[rowIndex].Cells[headerReceived].Value.ToString();
                    string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
                    string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();
                    int po_no = dgvOrder.Rows[rowIndex].Cells[headerPONO].Value == DBNull.Value ? -1 : Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerPONO].Value.ToString());


                    //approve form
                    frmOrderApprove frm = new frmOrderApprove(orderID.ToString(), requiredDate, itemName, itemCode, qty, unit, type, po_no, received);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                    if (frmOrderApprove.orderApproved)//if order approved from approve form, then change order status from requesting to pending
                    {
                        float final_Order_Qty = float.TryParse(frmOrderApprove.FINAL_ORDER_QTY, out float i) ? i : 0;
                        float Qty = float.TryParse(qty, out  i) ? i : 0;

                        dalItem.orderAdd(itemCode, final_Order_Qty - Qty);//add order qty to item
                        refreshOrderRecord(orderID);
                        orderApproved = false;
                    }
                }
                else
                {
                    MessageBox.Show("Action denied. Please contact admin for this action.");
                    tool.historyRecord(text.System, "Action denied. Please contact admin for this action.(frmOrder)", DateTime.Now, MainDashboard.USER_ID);

                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                refreshOrderRecord(orderID);
            }

        }

        private void orderReceive(int rowIndex, int orderID)
        {
            //get data from datagridview
            string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
            string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
            string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
            string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
            string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
            float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
            string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

            frmOrderReceive frm = new frmOrderReceive(orderID, itemCode, itemName, Convert.ToSingle(qty), Convert.ToSingle(received), unit, type);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//stock in

            refreshOrderRecord(orderID);

        }

        private void orderCancel(int rowIndex, int orderID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //DialogResult dialogResult = MessageBox.Show("Are you sure want to cancel this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
            string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
            //string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
            DateTime requiredDate = Convert.ToDateTime(dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value);
            string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
            string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
            float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
            string pending = dgvOrder.Rows[rowIndex].Cells[headerPending].Value.ToString();
            string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

            if (received > 0)//if have received record under this order ,then need to return this item from stock before cancel this order
            {
                DialogResult dialogResult = MessageBox.Show(@"There is an existing received record under this order. Please undo this record before canceling.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.OK)
                {
                    frmOrderActionHistory frm = new frmOrderActionHistory(orderID);

                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//return item from stock
                }
            }
            else
            {
                frmOrderCancel frm = new frmOrderCancel();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmOrderCancel.orderCancelled)
                {
                    uOrd.ord_id = orderID;
                    DateTime date = requiredDate;//DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null)
                    uOrd.ord_required_date = date;
                    uOrd.ord_qty = Convert.ToSingle(qty);
                    uOrd.ord_pending = 0;
                    uOrd.ord_received = 0;
                    uOrd.ord_updated_date = DateTime.Now;
                    uOrd.ord_updated_by = MainDashboard.USER_ID;
                    uOrd.ord_item_code = itemCode;
                    uOrd.ord_note = frmOrderCancel.CancelledReason;
                    uOrd.ord_unit = unit;
                    uOrd.ord_status = status_Cancelled;

                    uOrd.ord_type = type;

                    if (dalOrd.Update(uOrd))
                    {
                        if (!presentStatus.Equals(status_Requesting))
                        {
                            dalItem.orderSubtract(itemCode, pending); //subtract order qty
                        }

                        dalOrderAction.orderCancel(orderID, -1, note);
                    }
                }

            }
            refreshOrderRecord(orderID);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderComplete(int rowIndex, int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
            string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
            //string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
            DateTime requiredDate = Convert.ToDateTime(dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value);

            string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
            string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
            float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
            string pending = dgvOrder.Rows[rowIndex].Cells[headerPending].Value.ToString();
            string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();
            int po_no = dgvOrder.Rows[rowIndex].Cells[headerPONO].Value == DBNull.Value ? -1 : Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerPONO].Value.ToString());

            frmOrderComplete frm = new frmOrderComplete();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frmOrderComplete.orderCompleted)
            {
                uOrd.ord_id = orderID;
                DateTime date = requiredDate;
                uOrd.ord_required_date = date;
                uOrd.ord_qty = Convert.ToSingle(qty);
                uOrd.ord_pending = 0;
                uOrd.ord_po_no = po_no;
                uOrd.ord_received = received;
                uOrd.ord_updated_date = DateTime.Now;
                uOrd.ord_updated_by = MainDashboard.USER_ID;
                uOrd.ord_item_code = itemCode;
                uOrd.ord_note = frmOrderComplete.NOTE;
                uOrd.ord_unit = unit;
                uOrd.ord_status = status_Received;
                uOrd.ord_type = type;

                if (dalOrd.Update(uOrd))
                {
                    float pendingQty = float.TryParse(pending, out pendingQty) ? pendingQty : 0;

                    if (pendingQty > 0)
                    {
                        dalItem.orderSubtract(itemCode, pending); //subtract order qty
                    }


                    dalOrderAction.orderClose(orderID, note);
                }

                refreshOrderRecord(orderID);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderIncomplete(int rowIndex, int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to reopen this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {

                string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
                string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
                //string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
                DateTime requiredDate = Convert.ToDateTime(dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value);

                string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
                string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
                float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
                string pending = dgvOrder.Rows[rowIndex].Cells[headerPending].Value.ToString();
                string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

                uOrd.ord_id = orderID;
                DateTime date = requiredDate;
                uOrd.ord_required_date = date;
                uOrd.ord_qty = Convert.ToSingle(qty);
                uOrd.ord_pending = uOrd.ord_qty - received;
                uOrd.ord_received = received;
                uOrd.ord_updated_date = DateTime.Now;
                uOrd.ord_updated_by = MainDashboard.USER_ID;
                uOrd.ord_item_code = itemCode;
                uOrd.ord_note = "";
                uOrd.ord_unit = unit;
                uOrd.ord_status = status_Pending;
                uOrd.ord_type = type;

                if (dalOrd.Update(uOrd))
                {
                    dalItem.orderAdd(itemCode, uOrd.ord_qty - received); //subtract order qty

                    dalOrderAction.orderReOpen(orderID, "");
                }

                refreshOrderRecord(orderID);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void orderFollowUpAction(int orderID)
        {
            frmOrderFollowUpAction frm = new frmOrderFollowUpAction(orderID);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            refreshOrderRecord(orderID);
        }

        #endregion

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvOrder;
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int orderID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);
            string presentStatus = dgv.Rows[rowIndex].Cells[headerStatus].Value.ToString();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();
            if (itemClicked.Equals("Request"))
            {
                orderRequest(orderID);
            }
            else if (itemClicked.Equals("Approve"))
            {
                orderApprove(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Edit"))
            {
                orderEdit(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Cancel"))
            {
                orderCancel(rowIndex, orderID, presentStatus);
            }
            else if (itemClicked.Equals("Receive"))
            {
                orderReceive(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Complete"))
            {
                orderComplete(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Incomplete"))
            {
                orderIncomplete(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Follow Up/ Action"))
            {
                orderFollowUpAction(orderID);
            }
          
            lblUpdatedTime.Text = DateTime.Now.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void MaterialForecast_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvAlertSummary;
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.Str_MoreDetail))
            {
                if (rowIndex >= 0)
                {
                    string itemCode = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();

                    int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                    int monthEnd = Convert.ToInt32(cmbMonthTo.Text);

                    int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    int yearEnd = Convert.ToInt32(cmbYearTo.Text);

                    //check date
                    DateTime dateStart = new DateTime(yearStart, monthStart, 1);
                    DateTime dateEnd = new DateTime(yearEnd, monthEnd, 1);

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

                    frmOrderAlertDetail_NEW frm = new frmOrderAlertDetail_NEW(DT_PRODUCT_FORECAST_SUMMARY, itemCode, dateStart, dateEnd);

                    frm.StartPosition = FormStartPosition.CenterScreen;

                    frm.ShowDialog();

                }
            }
            else if (itemClicked.Equals(text.Str_OrderRequest))
            {
                if (rowIndex >= 0)
                {
                    string itemCode = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();
                    string itemName = dgvAlertSummary.Rows[rowIndex].Cells[text.Header_PartName].Value.ToString();

                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                    frmOrderRequest frm = new frmOrderRequest(tool.getItemCat(itemCode), itemCode, itemName, cbZeroCostOnly.Checked);

                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//create new order

                    if (frmOrderRequest.orderSuccess)
                    {
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        resetForm();
                        frmOrderRequest.orderSuccess = false;
                        Cursor = Cursors.Arrow; // change cursor to normal type
                    }
                    Cursor = Cursors.Arrow; // change cursor to normal type

                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void cmbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
                loadOrderRecordFromStatusSearch();
        }

        private void cbCodeNameSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCodeNameSearch.Checked)
            {
                cbPOSearch.Checked = false;
                cbOrderIDSearch.Checked = false;
                if (loaded)
                    loadOrderRecord();
            }
        }

        private void cbPOSearch_CheckedChanged(object sender, EventArgs e)
        {

            if (cbPOSearch.Checked)
            {
                cbCodeNameSearch.Checked = false;
                cbOrderIDSearch.Checked = false;
                if (loaded)
                    loadOrderRecord();
            }
        }

        private void cbOrderIDSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOrderIDSearch.Checked)
            {
                cbCodeNameSearch.Checked = false;
                cbPOSearch.Checked = false;
                if (loaded)
                    loadOrderRecord();
            }
        }

        private void txtOrdSearch_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            frmOrderRequest frm = new frmOrderRequest();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//create new order

            if (frmOrderRequest.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                resetForm();
                frmOrderRequest.orderSuccess = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {

                dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();

                string path = @"D:\StockAssistant\Document\OrderReport";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;

                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard();
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Name = cmbStatusSearch.Text;

                    #region Save data to Sheet

                    //Header and Footer setup
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&16 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 ORDERING RECORD";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&16 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                    //Page setup




                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;
                    xlWorkSheet.PageSetup.CenterHorizontally = true;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.4 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 1.4 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;


                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                    xlexcel.PrintCommunication = true;
                    xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;
                    // Paste clipboard results to worksheet range
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    //content edit
                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;
                    tRange.Font.Size = 11;
                    tRange.Font.Name = "Calibri";
                    tRange.EntireColumn.AutoFit();
                    tRange.EntireRow.AutoFit();
                    tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                    #endregion

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlexcel.DisplayAlerts = true;

                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvOrder.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                dgvOrder.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void dgvOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgvOrder.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                int orderID = Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerID].Value);

                DataTable dt = dalOrderAction.Select(orderID);
                if (dt.Rows.Count > 0)
                {
                    frmOrderActionHistory frm = new frmOrderActionHistory(orderID);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit
                }
                else
                {
                    MessageBox.Show("No action record under this order yet.");
                }
            }

            if (receivedReturn)//if order approved from approve form, then change order status from requesting to pending
            {
                refreshOrderRecord(selectedOrderID);
                receivedReturn = false;
            }

            lblUpdatedTime.Text = DateTime.Now.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvOrder;
            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerOrdered)
            {
                dgv.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(232, 244, 66);
            }
            else if (dgv.Columns[col].Name == headerPending)
            {
                dgv.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(66, 191, 244);
            }
            else if (dgv.Columns[col].Name == headerReceived)
            {
                dgv.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(66, 244, 161);
            }
            else if (dgv.Columns[col].Name == headerStatus)
            {
                string value = dgv.Rows[row].Cells[headerStatus].Value.ToString();
                Color foreColor = dgvOrder.DefaultCellStyle.ForeColor;

                if (value.Equals(status_Requesting))
                {
                    foreColor = Color.FromArgb(244, 170, 66);
                }
                else if (value.Equals(status_Cancelled))
                {
                    foreColor = Color.FromArgb(234, 67, 53);
                }
                else if (value.Equals(status_Pending))
                {
                    foreColor = Color.FromArgb(66, 133, 244);
                }
                else if (value.Equals(status_Received))
                {
                    foreColor = Color.FromArgb(52, 168, 83);
                }
                dgv.Rows[row].Cells[headerStatus].Style.ForeColor = foreColor;
            }

            dgv.ResumeLayout();
        }

        private void dgvOrder_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)//&& userPermission >= MainDashboard.ACTION_LVL_TWO
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvOrder.CurrentCell = dgvOrder.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvOrder.Rows[e.RowIndex].Selected = true;
                dgvOrder.Focus();
                int rowIndex = dgvOrder.CurrentCell.RowIndex;

                try
                {
                    string result = dgvOrder.Rows[rowIndex].Cells[headerStatus].Value.ToString();

                    if (result.Equals(status_Requesting))
                    {
                        if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                        {
                            my_menu.Items.Add("Approve").Name = "Approve";
                        }

                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }
                    else if (result.Equals(status_Cancelled))
                    {
                        my_menu.Items.Add("Request").Name = "Request";
                    }
                    else if (result.Equals(status_Pending))
                    {
                        my_menu.Items.Add("Receive").Name = "Receive";

                        if (userPermission >= MainDashboard.ACTION_LVL_TWO)
                        {
                            my_menu.Items.Add("Edit").Name = "Edit";
                            my_menu.Items.Add("Complete").Name = "Complete";
                        }
                        my_menu.Items.Add("Follow Up/ Action").Name = "Follow Up/ Action";
                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }
                    else if (result.Equals(status_Received))
                    {
                        if (userPermission >= MainDashboard.ACTION_LVL_TWO)
                        {
                            my_menu.Items.Add("Incomplete").Name = "Incomplete";
                        }

                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvOrder_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvOrder.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvOrder.ClearSelection();
            }
        }

        private void dgvOrder_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrder.CurrentCell != null)
                selectedOrderID = Convert.ToInt32(dgvOrder.Rows[dgvOrder.CurrentCell.RowIndex].Cells[headerID].Value);
        }

        private void dgvOrder_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
        }

        private void frmOrderAlert_NEW_Shown(object sender, EventArgs e)
        {
            loaded = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            timer1.Stop();

            if (loaded)
                loadOrderRecord();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
    }
}
