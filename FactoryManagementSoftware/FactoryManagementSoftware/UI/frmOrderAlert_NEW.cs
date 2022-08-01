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

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderAlert_NEW : Form
    {
        public frmOrderAlert_NEW()
        {
            InitializeComponent();

            ResetPage();
        }

        #region variable declare

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";
        private string headerOutQty = "DELIVERED";
        private string header_TotalOut = "TOTAL";
        private string header_Stock = "STOCK";
        private string header_Bal = "BAL.";
        private string header_Unit = "UNIT";
        private string Unit_KG = "KG";
        private string Unit_PCS = "PCS";
        private string header_Forecast = " FORECAST";
        private string ForecastType_DeductStock = " (FORECAST-STOCK)";
        private string ForecastType_FORECAST = " FORECAST";


        readonly string reportType_Delivered = "DELIVERED";
        readonly string reportType_ReadyStock = "READY STOCK";
        readonly string reportType_Forecast = "FORECAST";

        readonly string reportTitle_ActualUsed = "MATERIAL USED REPORT";
        readonly string reportTitle_Forecast = "MATERIAL USED REPORT";
        readonly string reportTitle_ReadyStock = "MATERIAL USED REPORT";

        readonly string BTN_SHOW_ORDER_ALERT = "SHOW ORDER ALERT";
        readonly string BTN_HIDE_ORDER_ALERT = "HIDE ORDER ALERT";
        readonly string BTN_ORDER_ALERT_ONLY = "ORDER ALERT ONLY";
        readonly string BTN_SHOW_ORDER_RECORD = "SHOW ORDER RECORD";

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

        private DataTable dt_MatUsed_Forecast;
        private DataTable dt_MatStock;
        private DataTable dt_Mat;
        private DataTable dt_PMMA;
        private DataTable dt_DeliveredData;
        private DataTable dt_ItemForecast;
        bool loaded = false;
        #endregion

        #region UI Design

        private DataTable NewOrderAlertSummaryTable()
        {
            DataTable dt = new DataTable();

            headerOutQty = cmbMonthFrom.Text + "/" + cmbYearFrom.Text + header_Forecast;
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(headerOutQty, typeof(float));

            return dt;
        }

        private DataTable SummaryDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));

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

            headerOutQty = monthStart + "/" + yearStart + header_Forecast;

            for (int i = yearStart; i <= yearEnd; i++)
            {
                if (yearStart == yearEnd)
                {
                    for (int j = monthStart; j <= monthEnd; j++)
                    {
                        month = j + "/" + i + header_Forecast;
                        dt.Columns.Add(month, typeof(float));

                    }
                }
                else
                {
                    if (i == yearStart)
                    {
                        for (int j = monthStart; j <= 12; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                    else if (i == yearEnd)
                    {
                        for (int j = 1; j <= monthEnd; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                }
            }

            return dt;
        }

        private DataTable NewMatUsedTable()
        {
            DataTable dt = new DataTable();

            headerOutQty = cmbMonthFrom.Text + "/" + cmbYearFrom.Text + header_Forecast;

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MatType, typeof(string));
            dt.Columns.Add(text.Header_MatCode, typeof(string));
            dt.Columns.Add(text.Header_MatName, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_Parent, typeof(string));
            dt.Columns.Add(headerOutQty, typeof(int));
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

            headerOutQty = monthStart + "/" + yearStart + header_Forecast;

            for (int i = yearStart; i <= yearEnd; i++)
            {
                if (yearStart == yearEnd)
                {
                    for (int j = monthStart; j <= monthEnd; j++)
                    {
                        month = j + "/" + i + header_Forecast;
                        dt.Columns.Add(month, typeof(float));
                       
                    }
                }
                else
                {
                    if (i == yearStart)
                    {
                        for (int j = monthStart; j <= 12; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                    else if (i == yearEnd)
                    {
                        for (int j = 1; j <= monthEnd; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dt.Columns.Add(month, typeof(float));
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            month = j + "/" + i + header_Forecast;
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

        private void dgvMatUsedUIEdit(DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[text.Header_Parent].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[text.Header_MatType].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.Columns[text.Header_MatCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

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

            for (int i = yearStart; i <= yearEnd; i++)
            {
                if (yearStart == yearEnd)
                {
                    for (int j = monthStart; j <= monthEnd; j++)
                    {
                        month = j + "/" + i + header_Forecast;
                        dgv.Columns[month].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    if (i == yearStart)
                    {
                        for (int j = monthStart; j <= 12; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dgv.Columns[month].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        }
                    }
                    else if (i == yearEnd)
                    {
                        for (int j = 1; j <= monthEnd; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dgv.Columns[month].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        }
                    }
                    else
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            month = j + "/" + i + header_Forecast;
                            dgv.Columns[month].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        }
                    }
                }
            }

            dgv.Columns[text.Header_PartName].Visible = false;
            dgv.Columns[text.Header_PartCode].Visible = false;
            dgv.Columns[text.Header_Parent].Visible = false;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].Visible = false;
            dgv.Columns[text.Header_Wastage].Visible = false;
            dgv.Columns[text.Header_MaterialUsedWithWastage].Visible = false;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].Visible = false;
            dgv.Columns[text.Header_ItemWeight_G].Visible = false;


            dgv.Columns[header_TotalOut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_TotalOut].DefaultHeaderCellType
            dgv.Columns[header_TotalOut].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Bal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Bal].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[text.Header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_PartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MatName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Parent].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerOutQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_ItemWeight_G].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Wastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MaterialUsedWithWastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerOutQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ItemWeight_G].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_Wastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_MaterialUsedWithWastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_MatName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[text.Header_MatName].Frozen = true;
            
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
        }

        private void dgvMatUsedReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView dgv = dgvAlertSummary;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == text.Header_MatCode)
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
            else if (dgv.Columns[col].Name == header_Bal)
            {
                float bal = float.TryParse(dgv.Rows[row].Cells[header_Bal].Value.ToString(), out float x) ? x : 0;

                if (bal < 0)
                {
                    dgv.Rows[row].Cells[header_Bal].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[header_Bal].Style.ForeColor = Color.Black;

                }
            }
            dgv.ResumeLayout();
        }

        private void dgvMatUsedReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvMatUsedUIEdit(dgvAlertSummary);
            dgvAlertSummary.AutoResizeColumns();
        }

        #endregion

        #region Load Data/Validation

        private void ResetPage()
        {
            tool.DoubleBuffered(dgvOrderRecord, true);
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
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                btnFilter.Text = textMoreFilters;
            }
            else
            {
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 110f);
                btnFilter.Text = textHideFilters;
            }
        }
        private void PanelUISetting(bool ShowOrderRecord,bool ShowOrderAlert)
        {
            btnFilter.Text = textMoreFilters;

            if (ShowOrderRecord && ShowOrderAlert)
            {
                btnShowOrHideOrderAlert.Text = BTN_HIDE_ORDER_ALERT;
                btnOrderAlertOnly.Text = BTN_ORDER_ALERT_ONLY;

                //order record & alert
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Percent, 50f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 30f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Percent, 50f);

                dgvOrderRecord.Visible = true;
                dgvAlertSummary.Visible = true;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 180f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 130f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 135f);
            }
            else if (ShowOrderRecord && !ShowOrderAlert)
            {
                btnShowOrHideOrderAlert.Text = BTN_SHOW_ORDER_ALERT;
                btnOrderAlertOnly.Text = BTN_ORDER_ALERT_ONLY;

                //order record 
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Percent, 0f);

                dgvOrderRecord.Visible = true;
                dgvAlertSummary.Visible = false;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 180f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
            }
            else if (!ShowOrderRecord && ShowOrderAlert)
            {
                btnShowOrHideOrderAlert.Text = BTN_HIDE_ORDER_ALERT;
                btnOrderAlertOnly.Text = BTN_SHOW_ORDER_RECORD;

                //order alert 
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Percent, 0f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 30f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Percent, 100f);

                dgvOrderRecord.Visible = false;
                dgvAlertSummary.Visible = true;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 180f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 130f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 135f);
            }
            else
            {
                btnShowOrHideOrderAlert.Text = BTN_SHOW_ORDER_ALERT;

                //order alert 
                tlpMain.RowStyles[0] = new RowStyle(SizeType.Percent, 0f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 65f);
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Percent, 100f);

                dgvOrderRecord.Visible = false;
                dgvAlertSummary.Visible = false;

                //main filter
                tlpMainFIlter.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMainFIlter.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 180f);
                tlpMainFIlter.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpMainFIlter.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
            }
        }
        private void frmMaterialUsedReport_NEW_Load(object sender, EventArgs e)
        {
            loaded = true;
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

        private float DeliveredToCustomerQty(string itemCode, DataTable dt_Join, DataTable dt_ToCustomer)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;
            row_Delivered = dt_DeliveredData.NewRow();

            foreach (DataRow trfHistRow in dt_ToCustomer.Rows)
            {
                dbItemCode = trfHistRow[dalTrfHist.TrfItemCode].ToString();
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();
                //get single out
                if (itemCode == dbItemCode && result == "Passed")
                {
                    dbItemName = trfHistRow[dalItem.ItemName].ToString();
                    int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                    DeliveredQty += qty;
                }

            }

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * DeliveredToCustomerQty(parentCode, dt_Join, dt_ToCustomer);
                }
            }

            return DeliveredQty;

        }

        private float ReadyStock(string customer, string itemCode, DataTable dt_Join, DataTable dt_ToCustomer)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;

            DataTable dt_Cust = dalCust.FullSelect();

            row_Delivered = dt_DeliveredData.NewRow();

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

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
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

               if(cbForecastDeductStock.Checked)
                {
                    float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);

                    DeliveredQty -= readyStock;
                }

                if (DeliveredQty < 0)
                    DeliveredQty = 0;


            }


            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
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

            DeliveredQty = tool.getItemForecast(dt_ItemForecast, itemCode, Convert.ToInt32(cmbYearFrom.Text), Convert.ToInt32(cmbMonthFrom.Text));
            dbItemName = tool.getItemNameFromDataTable(dt_Item, itemCode);

            if (cbForecastDeductStock.Checked)
            {
                float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);

                DeliveredQty -= readyStock;
            }

            if (DeliveredQty < 0)
                DeliveredQty = 0;

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
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

        private bool ifZeroCostMat(string matCode, DataTable dt_ZeroCostMat)
        {
            foreach (DataRow row in dt_ZeroCostMat.Rows)
            {
                if (matCode == row[dalMat.MatCode].ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private Tuple<DateTime,DateTime> GetStartAndEndDate()
        {
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

            dateStart = new DateTime(yearStart, monthStart, 1);
            dateEnd = new DateTime(yearEnd, monthEnd, 1);

            return Tuple.Create(dateStart, dateEnd);
        }

        private void NEW_LoadSummaryForecastMatUsedData()
        {
            frmLoading.ShowLoadingScreen();

            dgvAlertSummary.DataSource = null;

            #region Datatable Data

            dt_DeliveredData = SummaryDataTable();

            string custName = cmbCustomer.Text;

            string from = dtpFrom.Value.ToString("yyyy/MM/dd");
            string to = dtpTo.Value.ToString("yyyy/MM/dd");

            DataTable dt_PartTrfHist;
            DataTable dt_CustItem;

            if (custName.Equals("All"))
            {
                dt_CustItem = dalItemCust.Select();
                dt_PartTrfHist = dalTrfHist.rangeItemToAllCustomerSearch(from, to);
                dt_ItemForecast = dalItemForecast.Select();
            }
            else
            {
                dt_CustItem = dalItemCust.custSearch(custName);
                dt_PartTrfHist = dalTrfHist.rangeItemToCustomerSearch(custName, from, to);
                dt_ItemForecast = dalItemForecast.Select(tool.getCustID(custName).ToString());
            }

            if (cbZeroCostOnly.Checked)
            {
                dt_Mat = dalMat.SelectZeroCostMaterial();
            }
            else
            {
                dt_Mat = dalMat.Select();
            }

            //create datatable
            dt_MatUsed_Forecast = SummaryForecastMatUsedTable();
            DataRow row_DGVSouce;

            //get all item info
            dt_Item = dalItem.Select();

            //get all part list
            DataTable dt_Part = dalItem.catSelect(text.Cat_Part);
            DataTable dt_PartForChecking = dt_Part.Copy();

            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();
            DataTable dt_JoinforChecking = dt_Join.Copy();

            #endregion

            #region variable

            int index = 1;
            int dgvRowIndex = 0;
            string previousMatCode = null;
            float totalMatUsed = 0;
            bool printed = false;

            #endregion

            //foreach material list
            foreach (DataRow matRow in dt_Mat.Rows)
            {
                string matCode = matRow[dalMat.MatCode].ToString();
                string matName = matRow[dalMat.MatName].ToString();
                string matCat = matRow[dalMat.MatCat].ToString();

                string ItemTypeSelected = cmbItemType.Text;

                bool itemCatMatched = ItemTypeSelected == matCat;
                //itemCatMatched |= ItemTypeSelected == text.Cat_MB;
                //itemCatMatched |= ItemTypeSelected == text.Cat_Pigment;
                //itemCatMatched |= ItemTypeSelected == text.Cat_SubMat;
                //itemCatMatched |= ItemTypeSelected == text.Cat_Carton;
                itemCatMatched |= ItemTypeSelected.ToUpper() == "ALL";

                if (itemCatMatched)
                {
                    if (previousMatCode == null)
                    {
                        previousMatCode = matCode;
                        totalMatUsed = 0;
                    }

                    else if (previousMatCode != matCode && printed)
                    {
                        string printedMatCode = dt_MatUsed_Forecast.Rows[dgvRowIndex - 1][text.Header_MatCode].ToString();

                        if (dgvRowIndex - 1 >= 0 && !string.IsNullOrEmpty(printedMatCode))
                        {
                            dt_MatUsed_Forecast.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);

                            //add spacing
                            //row_DGVSouce = dt_MatUsed.NewRow();
                            //dt_MatUsed.Rows.Add(row_DGVSouce);
                            totalMatUsed = 0;
                            previousMatCode = matCode;
                            //dgvRowIndex++;
                        }

                    }

                    if (ItemTypeSelected == text.Cat_SubMat)
                    {
                        //foreach all join list (for sub material)
                        foreach (DataRow joinRow in dt_Join.Rows)
                        {
                            if (joinRow.RowState != DataRowState.Deleted)
                            {
                                string childCode = joinRow["child_code"].ToString();

                                if (childCode == matCode)
                                {
                                    string parentCode = joinRow["parent_code"].ToString();

                                    if (ifCustomerItem(parentCode, dt_JoinforChecking, dt_CustItem))
                                    {
                                        string parentName = joinRow["parent_name"].ToString();

                                        if(parentName.Equals("DISCH.GRILLE HOR.BLADE SUPPORT"))
                                        {
                                            float TEST = 0;
                                        }
                                        //Get delivered out data
                                        dt_DeliveredData.Rows.Clear();
                                        float reference_Value = GetForecastQty(custName, parentCode, dt_JoinforChecking, dt_PartTrfHist, "11", "2021");

                                        #region get weight data
                                        float partWeight = 0;
                                        float runnerWeight = 0;

                                        if (cbZeroCostOnly.Checked)
                                        {
                                            partWeight = float.TryParse(joinRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(joinRow[dalItem.ItemQuoRWPcs].ToString(), out partWeight) ? partWeight : 0;
                                        }
                                        else
                                        {
                                            partWeight = float.TryParse(joinRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(joinRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                        }

                                        float itemWeight = partWeight + runnerWeight;
                                        #endregion

                                        float wastage = Convert.ToSingle(matRow[dalItem.ItemWastage]); 

                                        float wastageAdd = reference_Value * wastage;
                                        int MatUsedWithWastage = (int)Math.Ceiling(reference_Value + wastageAdd);
                                        totalMatUsed += MatUsedWithWastage;

                                        #region New Test: with parent code/name
                                        foreach (DataRow row in dt_DeliveredData.Rows)
                                        {
                                            string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();
                                            string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();

                                            float qty = Convert.ToSingle(row[headerOutQty]);
                                            //string parent = DeliveredName + "(" + DeliveredCode + ")";
                                            string parent = DeliveredCode;

                                            if ((DeliveredName != null && DeliveredCode != null) || (reference_Value == 0 && parentCode == DeliveredCode))
                                            {

                                                if (DeliveredCode == parentCode)
                                                {
                                                    parent = "";
                                                }

                                                wastageAdd = qty * wastage;
                                                MatUsedWithWastage = (int)Math.Ceiling(qty + wastageAdd);

                                                row_DGVSouce = dt_MatUsed_Forecast.NewRow();
                                                row_DGVSouce[text.Header_MatType] = matCat;
                                                row_DGVSouce[text.Header_Index] = index;
                                                row_DGVSouce[text.Header_MatCode] = matCode;
                                                row_DGVSouce[text.Header_MatName] = matName;
                                                row_DGVSouce[text.Header_PartName] = parentName;
                                                row_DGVSouce[text.Header_PartCode] = parentCode;
                                                row_DGVSouce[text.Header_Parent] = parent;
                                                row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                                row_DGVSouce[text.Header_Wastage] = wastage;

                                                row_DGVSouce[headerOutQty] = Math.Round(qty, 2);
                                                row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = qty;
                                                row_DGVSouce[text.Header_MaterialUsedWithWastage] = MatUsedWithWastage;

                                                dt_MatUsed_Forecast.Rows.Add(row_DGVSouce);
                                                dgvRowIndex++;
                                                index++;
                                                printed = true;
                                            }
                                        }
                                        //joinRow.Delete();

                                        #endregion



                                    }
                                }
                            }
                        }
                        dt_Join.AcceptChanges();
                    }
                    else
                    {
                        //foreach all part list(for raw material)
                        foreach (DataRow partRow in dt_Part.Rows)
                        {
                            if (partRow.RowState != DataRowState.Deleted)
                            {
                                string itemMatCode = partRow[dalItem.ItemMaterial].ToString();

                                if (matCat != text.Cat_RawMat)
                                {
                                    itemMatCode = partRow[dalItem.ItemMBatch].ToString();
                                }

                                if (itemMatCode == matCode)
                                {
                                    string itemCode = partRow[dalItem.ItemCode].ToString();


                                    if (ifCustomerItem(itemCode, dt_JoinforChecking, dt_CustItem))
                                    {
                                        string itemName = partRow[dalItem.ItemName].ToString();

                                        float partWeight = 0;
                                        float runnerWeight = 0;

                                        if (cbZeroCostOnly.Checked)
                                        {
                                            partWeight = float.TryParse(partRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(partRow[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                                            if (partWeight <= 0)
                                            {
                                                partWeight = float.TryParse(partRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                                runnerWeight = float.TryParse(partRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                                            }

                                        }
                                        else
                                        {
                                            partWeight = float.TryParse(partRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(partRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                                        }

                                        float itemWeight = partWeight + runnerWeight;

                                        float wastage = partRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(partRow[dalItem.ItemWastage]);

                                        //Get delivered out data
                                        dt_DeliveredData.Rows.Clear();
                                        float deliveredQty = GetForecastQty(custName, itemCode, dt_JoinforChecking, dt_PartTrfHist, "11", "2021");

                                        float matUsedInKG = deliveredQty * itemWeight / 1000;
                                        totalMatUsed += (float)Math.Round(matUsedInKG + matUsedInKG * wastage, 2);


                                        #region New Test: with parent code/name
                                        foreach (DataRow row in dt_DeliveredData.Rows)
                                        {
                                            string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();

                                            //if(itemCode == "V99CJP0M0")
                                            //{
                                            //    float TEST = 0;
                                            //}
                                            string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();
                                            float qty = Convert.ToSingle(row[headerOutQty]);
                                            string parent = DeliveredCode;
                                            // string parent = DeliveredName + "(" + DeliveredCode + ")";

                                            if ((DeliveredName != null && DeliveredCode != null) || (deliveredQty == 0 && itemCode == DeliveredCode))
                                            {
                                                row_DGVSouce = dt_MatUsed_Forecast.NewRow();

                                                if (DeliveredCode == itemCode)
                                                {
                                                    parent = "";
                                                }

                                                matUsedInKG = qty * itemWeight / 1000;

                                                row_DGVSouce[text.Header_Index] = index;
                                                row_DGVSouce[text.Header_MatType] = matCat;
                                                row_DGVSouce[text.Header_MatCode] = matCode;
                                                row_DGVSouce[text.Header_MatName] = matName;
                                                row_DGVSouce[text.Header_PartName] = itemName;
                                                row_DGVSouce[text.Header_PartCode] = itemCode;
                                                row_DGVSouce[text.Header_Parent] = parent;
                                                row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                                row_DGVSouce[text.Header_Wastage] = wastage;
                                                row_DGVSouce[headerOutQty] = Math.Round(qty, 2);
                                                row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = Math.Round(matUsedInKG, 2);
                                                row_DGVSouce[text.Header_MaterialUsedWithWastage] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

                                                dt_MatUsed_Forecast.Rows.Add(row_DGVSouce);
                                                index++;
                                                dgvRowIndex++;
                                                printed = true;
                                            }
                                        }

                                        //partRow.Delete();
                                        #endregion
                                    }
                                }

                                if (!ifZeroCostMat(itemMatCode, dt_Mat))
                                {
                                    //partRow.Delete();
                                }
                            }
                        }
                        //dt_Part.AcceptChanges();
                    }
                }

            }

            //last row 
            string printedMatCode2 = dt_MatUsed_Forecast.Rows[dgvRowIndex - 1][text.Header_MatCode].ToString();
            if (dgvRowIndex - 1 >= 0 && !string.IsNullOrEmpty(printedMatCode2))
            {
                dt_MatUsed_Forecast.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);
            }




            if (dt_MatUsed_Forecast.Rows.Count > 0)
            {
                PrintForecastSummary(dt_MatUsed_Forecast);
            }
            else
            {
                dgvAlertSummary.DataSource = null;
            }

            frmLoading.CloseForm();
        }

        private void PrintForecastSummary(DataTable dt_MatUsed)
        {
            //float OrderedQty = 0;
            string month = null;

            //float OrderedQty = tool.getItemForecast(dt_ItemForecast, partCode, Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.Text));

            #region Get  Date Data
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

            #endregion

            foreach (DataRow row in dt_MatUsed.Rows)
            {
                string partCode = row[text.Header_PartCode].ToString();
                string matType = row[text.Header_MatType].ToString();

                string matName = row[text.Header_MatName].ToString();


                float itemWeight = float.TryParse(row[text.Header_ItemWeight_G].ToString(), out float x) ? x : 0;
                float wastage = float.TryParse(row[text.Header_Wastage].ToString(), out x) ? x : 0;
                float joinQty = 1;

                string itemCode = row[text.Header_Parent].ToString();

                float TotalOrderedQty = 0;
                float OrderedQty = 0;
                float matUsedInKG = 0;

                if (string.IsNullOrEmpty(itemCode))
                {
                    itemCode = partCode;
                    joinQty = 1;
                }
                else
                {
                    foreach (DataRow join in dt_Join.Rows)
                    {
                        if (partCode == join["child_code"].ToString() && itemCode == join["parent_code"].ToString())
                        {
                            joinQty = Convert.ToSingle(join["join_qty"]);
                            break;
                        }
                    }
                }

                for (int i = yearStart; i <= yearEnd; i++)
                {
                    if (yearStart == yearEnd)
                    {
                        for (int j = monthStart; j <= monthEnd; j++)
                        {
                            month = j + "/" + i + header_Forecast;

                            if(dt_MatUsed.Columns.Contains(month))
                            {
                                if (cbForecastDeductStock.Checked)
                                {
                                    float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);
                                    OrderedQty = joinQty * (tool.getItemForecast(dt_ItemForecast, itemCode, i, j) - readyStock);
                                }
                                else
                                {
                                    OrderedQty = joinQty * tool.getItemForecast(dt_ItemForecast, itemCode, i, j);

                                }

                                OrderedQty = OrderedQty <= -1 ? 0 : OrderedQty;
                                TotalOrderedQty += OrderedQty;

                                //calculate material used
                                matUsedInKG = OrderedQty * itemWeight / 1000;

                                if(matType.Equals(text.Cat_SubMat) || matType.Equals(text.Cat_Carton) || matType.Equals(text.Cat_Packaging))
                                {
                                    row[month] = Math.Round(OrderedQty + OrderedQty * wastage, 0);
                                }
                                else
                                {
                                    row[month] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

                                }
                            }
                        }
                    }
                    else
                    {
                        if (i == yearStart)
                        {
                            for (int j = monthStart; j <= 12; j++)
                            {
                                month = j + "/" + i + header_Forecast;
                                if (dt_MatUsed.Columns.Contains(month))
                                {
                                    if (cbForecastDeductStock.Checked)
                                    {
                                        float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);
                                        OrderedQty = joinQty * (tool.getItemForecast(dt_ItemForecast, itemCode, i, j) - readyStock);
                                    }
                                    else
                                    {
                                        OrderedQty = joinQty * tool.getItemForecast(dt_ItemForecast, itemCode, i, j);

                                    }

                                    OrderedQty = OrderedQty <= -1 ? 0 : OrderedQty;
                                    TotalOrderedQty += OrderedQty;

                                    //calculate material used
                                    matUsedInKG = OrderedQty * itemWeight / 1000;

                                    if (matType.Equals(text.Cat_SubMat))
                                    {
                                        row[month] = Math.Round(OrderedQty + OrderedQty * wastage, 0);
                                    }
                                    else
                                    {
                                        row[month] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);
                                    }
                                }
                            }
                        }
                        else if (i == yearEnd)
                        {
                            for (int j = 1; j <= monthEnd; j++)
                            {
                                month = j + "/" + i + header_Forecast;
                                if (dt_MatUsed.Columns.Contains(month))
                                {
                                    if (cbForecastDeductStock.Checked)
                                    {
                                        float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);
                                        OrderedQty = joinQty * (tool.getItemForecast(dt_ItemForecast, itemCode, i, j) - readyStock);
                                    }
                                    else
                                    {
                                        OrderedQty = joinQty * tool.getItemForecast(dt_ItemForecast, itemCode, i, j);

                                    }

                                    OrderedQty = OrderedQty <= -1 ? 0 : OrderedQty;
                                    TotalOrderedQty += OrderedQty;

                                    //calculate material used
                                    matUsedInKG = OrderedQty * itemWeight / 1000;

                                    if (matType.Equals(text.Cat_SubMat))
                                    {
                                        row[month] = Math.Round(OrderedQty + OrderedQty * wastage, 0);
                                    }
                                    else
                                    {
                                        row[month] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                month = j + "/" + i + header_Forecast;
                                if (dt_MatUsed.Columns.Contains(month))
                                {
                                    if (cbForecastDeductStock.Checked)
                                    {
                                        float readyStock = tool.getStockQtyFromDataTable(dt_Item, itemCode);
                                        OrderedQty = joinQty * (tool.getItemForecast(dt_ItemForecast, itemCode, i, j) - readyStock);
                                    }
                                    else
                                    {
                                        OrderedQty = joinQty * tool.getItemForecast(dt_ItemForecast, itemCode, i, j);

                                    }

                                    OrderedQty = OrderedQty <= -1 ? 0 : OrderedQty;
                                    TotalOrderedQty += OrderedQty;

                                    //calculate material used
                                    matUsedInKG = OrderedQty * itemWeight / 1000;

                                    if (matType.Equals(text.Cat_SubMat))
                                    {
                                        row[month] = Math.Round(OrderedQty + OrderedQty * wastage, 0);
                                    }
                                    else
                                    {
                                        row[month] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);
                                    }
                                }
                            }
                        }
                    }
                }


                //calculate material used
                matUsedInKG = TotalOrderedQty * itemWeight / 1000;
                row[text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

            }

            if (dt_MatUsed.Rows.Count > 0)
            {
                dt_MatUsed = RemoveDuplicateRows(dt_MatUsed, text.Header_MatCode);

                dt_MatUsed = RearrangeIndex(dt_MatUsed);


                dgvAlertSummary.DataSource = dt_MatUsed;
                dgvAlertSummary.ClearSelection();
            }
            else
            {
                dgvAlertSummary.DataSource = null;
            }

        }

        private DataTable RearrangeIndex(DataTable dataTable)
        {
            int index = 1;

            #region Get  Date Data
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

            #endregion
            DataTable dt_Item = dalItem.Select();


            string ForecastColName = "";
            foreach (DataRow row in dataTable.Rows)
            {
                row[text.Header_Index] = index++;

                string matType = row[text.Header_MatType].ToString();
                string matCode = row[text.Header_MatCode].ToString();

                float Total = 0;

                for (int i = yearStart; i <= yearEnd; i++)
                {
                    if (yearStart == yearEnd)
                    {
                        for (int j = monthStart; j <= monthEnd; j++)
                        {
                            ForecastColName = j + "/" + i + header_Forecast;

                            if (dataTable.Columns.Contains(ForecastColName))
                            {
                                Total += float.TryParse(row[ForecastColName].ToString(), out float x) ? x : 0;

                            }
                        }
                    }
                    else
                    {
                        if (i == yearStart)
                        {
                            for (int j = monthStart; j <= 12; j++)
                            {
                                ForecastColName = j + "/" + i + header_Forecast;

                                if (dataTable.Columns.Contains(ForecastColName))
                                {
                                    Total += float.TryParse(row[ForecastColName].ToString(), out float x) ? x : 0;
                                }
                            }
                        }
                        else if (i == yearEnd)
                        {
                            for (int j = 1; j <= monthEnd; j++)
                            {
                                ForecastColName = j + "/" + i + header_Forecast;

                                if (dataTable.Columns.Contains(ForecastColName))
                                {
                                    Total += float.TryParse(row[ForecastColName].ToString(), out float x) ? x : 0;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                ForecastColName = j + "/" + i + header_Forecast;

                                if (dataTable.Columns.Contains(ForecastColName))
                                {
                                    Total += float.TryParse(row[ForecastColName].ToString(), out float x) ? x : 0;
                                }
                            }
                        }
                    }
                }
                float readyStock = 0;

                if (cbZeroStockType.Checked)
                {
                    readyStock = tool.getPMMAQtyFromDataTable(dt_Item, matCode);
                }
                else
                {
                    readyStock = tool.getStockQtyFromDataTable(dt_Item, matCode);
                }

                if (matType.Equals(text.Cat_SubMat))
                {
                    row[header_TotalOut] = Math.Round(Total, 0);
                    row[header_Stock] = Math.Round(readyStock, 0);
                    row[header_Bal] = Math.Round(readyStock - Total, 0);

                    row[header_Unit] = Unit_PCS;

                }
                else
                {
                    row[header_TotalOut] = Math.Round(Total, 2);
                    row[header_Stock] = Math.Round(readyStock, 2);
                    row[header_Bal] = Math.Round(readyStock - Total,2);

                    row[header_Unit] = Unit_KG;

                }
            }

            return dataTable;
        }

        private DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            #region Get  Date Data
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

            #endregion

            string ForecastColName = "";
            var result = dTable.AsEnumerable()
            .GroupBy(r => new
            {
                Version = r.Field<String>(colName)
                //Col1 = r.Field<float>("10" + "/" + "2021" + " FORECAST")
                //Col2 = r.Field<String>("11" + "/" + "2021" + " FORECAST")
            })
            .Select(g =>
            {
                var row = g.First();

                for (int i = yearStart; i <= yearEnd; i++)
                {
                    if (yearStart == yearEnd)
                    {
                        for (int j = monthStart; j <= monthEnd; j++)
                        {
                            ForecastColName = j + "/" + i + header_Forecast;

                            if (dTable.Columns.Contains(ForecastColName))
                            {
                                row.SetField(ForecastColName, Math.Round(g.Sum(r => r.Field<float>(ForecastColName)), 2));
                               
                            }
                        }
                    }
                    else
                    {
                        if (i == yearStart)
                        {
                            for (int j = monthStart; j <= 12; j++)
                            {
                                ForecastColName = j + "/" + i + header_Forecast;

                                if (dTable.Columns.Contains(ForecastColName))
                                {
                                    //row.SetField(ForecastColName, g.Sum(r => r.Field<float>(ForecastColName)));
                                    row.SetField(ForecastColName, Math.Round(g.Sum(r => r.Field<float>(ForecastColName)), 2));
                                }
                            }
                        }
                        else if (i == yearEnd)
                        {
                            for (int j = 1; j <= monthEnd; j++)
                            {
                                ForecastColName = j + "/" + i + header_Forecast;

                                if (dTable.Columns.Contains(ForecastColName))
                                {
                                    //row.SetField(ForecastColName, g.Sum(r => r.Field<float>(ForecastColName)));
                                    row.SetField(ForecastColName, Math.Round(g.Sum(r => r.Field<float>(ForecastColName)), 2));
                                }
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                ForecastColName = j + "/" + i + header_Forecast;

                                if (dTable.Columns.Contains(ForecastColName))
                                {
                                    //row.SetField(ForecastColName, g.Sum(r => r.Field<float>(ForecastColName)));
                                    row.SetField(ForecastColName, Math.Round(g.Sum(r => r.Field<float>(ForecastColName)), 2));
                                }
                            }
                        }
                    }
                }

                //row.SetField("10" + "/" + "2021" + " FORECAST", g.Sum(r => r.Field<float>("10" + "/" + "2021" + " FORECAST")));
                //row.SetField("11" + "/" + "2021" + " FORECAST", g.Sum(r => r.Field<float>("11" + "/" + "2021" + " FORECAST")));
                //row.SetField("12" + "/" + "2021" + " FORECAST", g.Sum(r => r.Field<float>("12" + "/" + "2021" + " FORECAST")));
                return row;
            }).CopyToDataTable();


            //Hashtable hTable = new Hashtable();
            //ArrayList duplicateList = new ArrayList();

            ////Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            ////And add duplicate item value in arraylist.
            //foreach (DataRow drow in dTable.Rows)
            //{
            //    if (hTable.Contains(drow[colName]))
            //        duplicateList.Add(drow);
            //    else
            //        hTable.Add(drow[colName], string.Empty);
            //}

            ////Removing a list of duplicate items from datatable.
            //foreach (DataRow dRow in duplicateList)
            //    dTable.Rows.Remove(dRow);

            ////Datatable which contains unique records will be return as output.
            return result;
        }

        private void frmOrderAlert_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewOrdFormOpen = false;
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
                NEW_LoadSummaryForecastMatUsedData();
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

        private void tlpFilter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbForecastDeduteStock_CheckedChanged(object sender, EventArgs e)
        {
            if(cbForecastDeductStock.Checked)
            {
                header_Forecast = ForecastType_DeductStock;
            }
            else
            {
                header_Forecast = ForecastType_FORECAST;

            }
        }

        private void cbStockType_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnShowOrHideOrderAlert_Click(object sender, EventArgs e)
        {
            if(btnShowOrHideOrderAlert.Text.Equals(BTN_HIDE_ORDER_ALERT))
            {
                if (btnOrderAlertOnly.Text.Equals(BTN_ORDER_ALERT_ONLY))
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

                if (btnOrderAlertOnly.Text.Equals(BTN_ORDER_ALERT_ONLY))
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

            if (text == BTN_ORDER_ALERT_ONLY)
            {
                PanelUISetting(false, true);
            }
            else if (text == BTN_SHOW_ORDER_RECORD)
            {
                if (btnShowOrHideOrderAlert.Text.Equals(BTN_SHOW_ORDER_ALERT))
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
        }

        private void cbZeroStockType_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
