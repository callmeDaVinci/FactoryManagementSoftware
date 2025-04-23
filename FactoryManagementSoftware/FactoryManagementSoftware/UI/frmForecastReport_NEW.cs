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
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections.Generic;
using static Accord.Math.FourierTransform;
using Syncfusion.XlsIO.Parser.Biff_Records;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using Range = Microsoft.Office.Interop.Excel.Range;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Configuration;
using iTextSharp.text.pdf;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport_NEW : Form
    {
        public frmForecastReport_NEW()
        {
            InitializeComponent();

            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            //tool.GetCustItemWithForecast(1, 1, 2020, 3);
            tool.DoubleBuffered(dgvForecastReport, true);

            if (cbMainCustomerOnly.Checked)
            {
                tool.loadMainCustomerAndAllToComboBox(cmbCustomer);
                
            }
            else
            {
                tool.loadCustomerAndALLWithoutOtherToComboBox(cmbCustomer);

            }

            if (myconnstrng == text.DB_Semenyih)
            {
                cmbCustomer.Text = "SP OUG";
            }

            cmbCustomer.SelectedIndex = 0;
            InitializeFilterData();
        }

        #region variable declare

        static public string myconnstrng;

        enum ColorSet
        {
            Gold = 0,
            //Cyan,
            //Aquamarine,
            //Orange,
            //Yellow,
            ////MediumSpringGreen,
            //Lavender,
            //LightGray,
            //LightBlue
            //Bisque,
            //Chartreuse,
            //CornflowerBlue,
            //MediumTurquoise,
            //MediumAquamarine,
            //LightSalmon,
            //DeepSkyBlue,
            //SkyBlue,
            //Tan,
            //Thistle,
        }

        //enum ColorSet
        //{
        //    Gold = 0,
        //    Cyan,
        //    Aquamarine,
        //    Orange,
        //    Yellow,
        //    //MediumSpringGreen,
        //    Lavender,
        //    LightGray,
        //    LightBlue
        //    //Bisque,
        //    //Chartreuse,
        //    //CornflowerBlue,
        //    //MediumTurquoise,
        //    //MediumAquamarine,
        //    //LightSalmon,
        //    //DeepSkyBlue,
        //    //SkyBlue,
        //    //Tan,
        //    //Thistle,
        //}
        private List<int> Row_Index_Found;

        int CURRENT_ROW_JUMP = -1;
        private string CELL_EDITING_OLD_VALUE = "";
        private string CELL_EDITING_NEW_VALUE = "";

        private bool CELL_VALUE_CHANGED = false;

        private string textRowFound = " row(s) found";

        private string textSearchFilter = "SEARCH FILTER";
        private string textHideFilter = "HIDE FILTER";

        private string textHideProPlanningFilter = "HIDE PRO PLANNING FILTER";
        private string textShoweProPlanningFilter = "PRO PLANNING FILTER";


        readonly string headerToDo = "TO DO";
        readonly string headerParentColor = "SPECIAL TYPE";
        readonly string headerBackColor = "BACK COLOR";
        readonly string headerForecastType = "FORECAST TYPE";
        readonly string headerBalType = "BAL TYPE";
        readonly string headerToProduce = "TO PRODUCE";
        readonly string headerProduced = "PRODUCED*";
        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerRowReference = "REPEATED ROW";
        readonly string headerItemRemark = "REMARK";

        readonly string headerItemType = "ITEM TYPE";
        readonly string headerRawMat = "RAW MATERIAL";
        readonly string headerPartName = "PART NAME";
        readonly string headerPartCode = "PART CODE";
        readonly string headerColorMat = "COLOR MATERIAL";
        readonly string headerPartWeight = "PART WEIGHT/G (RUNNER)";
        readonly string headerPlannedQty = "PLANNED QTY";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerEstimate = "ESTIMATE";
        string headerForecast1 = "FCST/ NEEDED";
        string headerForecast2 = "FCST/ NEEDED";
        string headerForecast3 = "FCST/ NEEDED";
        string headerForecast4 = "FCST/ NEEDED";

        readonly string headerOut = "OUT";
        readonly string headerOutStd = "OUTSTD";
        string headerBal1 = "BAL";
        string headerBal2 = "BAL";
        string headerBal3 = "BAL";
        string headerBal4 = "BAL";

        readonly string headerQuoTon = "QUO TON";
        readonly string headerProTon = "PRO TON";
        readonly string headerCavity = "CAVITY";
        readonly string headerQuoCT = "QUO CT";
        readonly string headerProCT = "PRO CT";
        readonly string headerPWPerShot = "PW/SHOT";
        readonly string headerRWPerShot = "RW/SHOT";
        readonly string headerTotalOut = "TOTAL OUT/2021";
        readonly string headerMonthlyOut = "MONTHLY OUT";

        readonly Color AssemblyColor = Color.Blue;
        readonly Color InsertMouldingColor = Color.Green;
        readonly Color ProductionAndAssemblyColor = Color.Purple;
        readonly Color InspectionColor = Color.Peru;

        readonly string AssemblyMarking = "Assembly Set";
        readonly string InspectionMarking = "Inspection";
        readonly string InsertMoldingMarking = "Insert Molding";
        readonly string AssemblyAfterProductionMarking = "Assembly After Pro.";

        readonly string AssemblyMarking_Color = "Blue";
        readonly string InspectionMarking_Color = "Peru";
        readonly string InjectionMarking_Color = "Green";
        readonly string AssemblyAfterProductionMarking_Color = "Purple";

        readonly string typeSingle = "SINGLE";
        readonly string typeParent= "PARENT";
        readonly string typeChild = "CHILD";

        readonly string forecastType_Forecast = "FORECAST";
        readonly string forecastType_Needed = "NEEDED";

        readonly string ToDoType_ToAssembly = "(2) TO ASSEMBLY";
        readonly string ToDoType_ToProduce = "(1) TO PRODUCE";
        readonly string ToDoType_ToOrder = "(3) TO ORDER";


        readonly string balType_Total = "TOTAL";
        readonly string balType_Repeated = "REPEATED";
        readonly string balType_Unique = "UNIQUE";

        readonly string sortBy_Stock = "Stock (Finished Goods)";
        readonly string sortBy_Estimate = "Estimate";
        string sortBy_Forecast1 = "Forecast/Needed";
        string sortBy_Forecast2 = "Forecast/Needed";
        string sortBy_Forecast3 = "Forecast/Needed";

        readonly string sortBy_Out = "Delivered Out";
        readonly string sortBy_OutStd = "Out Standing";
        string sortBy_Bal1 = "Balance";
        string sortBy_Bal2 = "Balance";

        readonly string ReportType_Full = "FORECAST REPORT";
        readonly string ReportType_Summary = "SUMMARY REPORT";

        private bool loaded = false;
        private bool custChanging = false;
        private bool SummaryMode = false;

        private float alertLevel = 0;

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();
        planningDAL dalPlanning = new planningDAL();
        dataTrfBLL uData = new dataTrfBLL();
        habitDAL dalHabit = new habitDAL();
        habitBLL uHabit = new habitBLL();
        facStockDAL dalStock = new facStockDAL();
        Tool tool = new Tool();

        Text text = new Text();

        DataTable DT_JOIN = new DataTable();
        DataTable DT_ITEM = new DataTable();
        DataTable dtMasterData = new DataTable();
        DataTable dt_SummaryList = new DataTable();
        DataTable DT_MACHINE_SCHEDULE = new DataTable();
        DataTable DT_MACHINE_SCHEDULE_COPY = new DataTable();
        DataTable DT_PMMA_DATE = new DataTable();
        DataTable DT_ITEM_CUST = new DataTable();

        DataTable DT_FORECAST_REPORT = new DataTable();

        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
        DataTable dt_ProRecord = new DataTable();

        DateTime PMMA_START_DATE = DateTime.MaxValue;
        DateTime PMMA_END_DATE = DateTime.MaxValue;
        #endregion

        #region UI Design

        private DataTable NewForecastReportTable()
        {
            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";
            headerForecast4 = "FCST/ NEEDED";

            headerBal1 = "BAL";
            headerBal2 = "BAL";
            headerBal3 = "BAL";
            DataTable dt = new DataTable();

            dt.Columns.Add(headerBackColor, typeof(string));
            dt.Columns.Add(headerForecastType, typeof(string));
            dt.Columns.Add(headerBalType, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerRowReference, typeof(string));
            dt.Columns.Add(headerItemType, typeof(string));
            dt.Columns.Add(headerIndex, typeof(float));

            if(cmbCustomer.Text.Equals(text.Cmb_All))
            {
                dt.Columns.Add(text.Header_Customer, typeof(string));
            }

            dt.Columns.Add(headerRawMat, typeof(string));
            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerColorMat, typeof(string));
            dt.Columns.Add(text.Header_ColorMatCode, typeof(string));
            dt.Columns.Add(headerPartWeight, typeof(string));

            if(cbIncludeProInfo.Checked)
            {
                dt.Columns.Add(headerQuoTon, typeof(string));
                dt.Columns.Add(headerProTon, typeof(string));
                dt.Columns.Add(headerCavity, typeof(string));
                dt.Columns.Add(headerQuoCT, typeof(string));
                dt.Columns.Add(headerProCT, typeof(string));
                dt.Columns.Add(headerPWPerShot, typeof(string));
                dt.Columns.Add(headerRWPerShot, typeof(string));
                dt.Columns.Add(headerTotalOut, typeof(string));
                dt.Columns.Add(headerMonthlyOut, typeof(string));
            }

            //dt.Columns.Add(headerPlannedQty, typeof(float));
            dt.Columns.Add(headerProduced, typeof(float));
            dt.Columns.Add(headerToProduce, typeof(float));
           
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerEstimate, typeof(float));


            string monthFrom = cmbForecastFrom.Text;

            string monthName = string.IsNullOrEmpty(monthFrom) || cmbForecastFrom.SelectedIndex == -1 ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) : monthFrom;
            int monthINT = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;

            for (int i = 1; i <= 4; i++)
            {
                if(i == 1)
                {
                    headerForecast1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast1;
                    headerBal1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal1;
                }
               
                else if(i == 2)
                {
                    headerForecast2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast2;
                    headerBal2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal2;
                }

                else if (i == 3)
                {
                    headerForecast3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast3;
                    headerBal3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal3;

                }
                else if (i == 4)
                {
                    headerForecast4 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast4;
                    headerBal4 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal4;

                }
                monthINT++;

                if(monthINT > 12)
                {
                    monthINT -= 12;

                }

                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            }

            dt.Columns.Add(headerForecast1, typeof(float));
            dt.Columns.Add(headerOut, typeof(float));
            dt.Columns.Add(headerOutStd, typeof(float));
            dt.Columns.Add(headerBal1, typeof(float));
            dt.Columns.Add(headerForecast2, typeof(float));
            dt.Columns.Add(headerBal2, typeof(float));
            dt.Columns.Add(headerForecast3, typeof(float));
            dt.Columns.Add(headerBal3, typeof(float));

            dt.Columns.Add(headerItemRemark, typeof(string));

            return dt;
        }

        private void DgvForecastReportUIEdit(DataGridView dgv)
        {
            if(dgv != null)
            {
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (dgv.Columns.Contains(headerPartWeight))
                    dgv.Columns[headerPartWeight].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //dgv.Columns[headerPlannedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerToProduce].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (dgv.Columns.Contains(headerPartWeight))
                    dgv.Columns[headerEstimate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgv.Columns[headerForecast1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerOut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerOutStd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerBal1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerForecast2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerBal2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerForecast3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerBal3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[headerItemRemark].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                if (dgv.Columns.Contains(text.Header_Customer))
                {
                    dgv.Columns[text.Header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[text.Header_Customer].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                }

                dgv.Columns[headerPartCode].Frozen = true;
                dgv.Columns[text.Header_ColorMatCode].Visible = false;

                dgv.Columns[headerBal1].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerBal1].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[headerBal1].HeaderCell.Style.ForeColor = Color.Red;

                dgv.Columns[headerForecast2].HeaderCell.Style.BackColor = Color.PeachPuff;
                dgv.Columns[headerForecast2].DefaultCellStyle.BackColor = Color.PeachPuff;

                dgv.Columns[headerBal2].HeaderCell.Style.BackColor = Color.PeachPuff;
                dgv.Columns[headerBal2].DefaultCellStyle.BackColor = Color.PeachPuff;
                dgv.Columns[headerBal2].HeaderCell.Style.ForeColor = Color.Red;

                dgv.Columns[headerForecast3].HeaderCell.Style.BackColor = Color.Aquamarine;
                dgv.Columns[headerForecast3].DefaultCellStyle.BackColor = Color.Aquamarine;

                dgv.Columns[headerBal3].HeaderCell.Style.BackColor = Color.Aquamarine;
                dgv.Columns[headerBal3].DefaultCellStyle.BackColor = Color.Aquamarine;
                dgv.Columns[headerBal3].HeaderCell.Style.ForeColor = Color.Red;

                dgv.Columns[headerReadyStock].HeaderCell.Style.BackColor = Color.Pink;
                dgv.Columns[headerReadyStock].DefaultCellStyle.BackColor = Color.Pink;

                dgv.Columns[headerEstimate].HeaderCell.Style.BackColor = Color.LightCyan;
                dgv.Columns[headerEstimate].DefaultCellStyle.BackColor = Color.LightCyan;

                dgv.Columns[headerForecast1].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerForecast1].DefaultCellStyle.BackColor = Color.LightYellow;

                dgv.Columns[headerOut].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerOut].DefaultCellStyle.BackColor = Color.LightYellow;

                dgv.Columns[headerOutStd].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerOutStd].DefaultCellStyle.BackColor = Color.LightYellow;

                dgv.Columns[headerRowReference].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[headerPartCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[headerPartName].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[headerRawMat].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                dgv.Columns[headerColorMat].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

               
                dgv.Columns[headerPartWeight].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                //dgv.Columns[headerProduced].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[headerToProduce].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[headerEstimate].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);


                dgv.Columns[headerBal1].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                dgv.Columns[headerBal1].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[headerBal2].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                dgv.Columns[headerBal2].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[headerBal3].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                dgv.Columns[headerBal3].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                //dgv.Columns[headerParentColor].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);

                dgv.Columns[headerProduced].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Strikeout);

                dgv.Columns[headerItemRemark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                //dgv.Columns[headerPartWeight].Visible = false;

                dgv.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[headerItemRemark].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[headerItemRemark].MinimumWidth = 200;
                dgv.EnableHeadersVisualStyles = false;
            }
          
        }

        private void DgvForecastSummaryReportUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgv.Columns.Contains(headerPartWeight))
            {
                //dgv.Columns[headerPartWeight].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerPartWeight].Visible = false;

            }

            //dgv.Columns[headerPlannedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[headerToProduce].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[headerProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;



            dgv.Columns[headerPartCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerRawMat].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[headerToDo].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            dgv.Columns[headerItemRemark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);


            dgv.Columns[headerForecast1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerOut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerOutStd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBal1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerForecast2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBal2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerForecast3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[headerPartCode].Frozen = true;

            if (dgv.Columns.Contains(headerBal3))
            {
                dgv.Columns[headerBal3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgv.Columns[headerBal3].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                dgv.Columns[headerBal3].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[headerReadyStock].HeaderCell.Style.BackColor = Color.Aquamarine;
                dgv.Columns[headerReadyStock].DefaultCellStyle.BackColor = Color.Aquamarine;

                dgv.Columns[headerBal1].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerBal1].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[headerBal1].HeaderCell.Style.ForeColor = Color.Red;

                dgv.Columns[headerBal2].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerBal2].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[headerBal2].HeaderCell.Style.ForeColor = Color.Red;

                dgv.Columns[headerBal3].HeaderCell.Style.BackColor = Color.LightYellow;
                dgv.Columns[headerBal3].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[headerBal3].HeaderCell.Style.ForeColor = Color.Red;

                if (dgv.Columns.Contains(headerBal4))
                {
                    dgv.Columns[headerBal4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgv.Columns[headerBal4].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                    dgv.Columns[headerBal4].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                    dgv.Columns[headerBal4].HeaderCell.Style.BackColor = Color.LightYellow;
                    dgv.Columns[headerBal4].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgv.Columns[headerBal4].HeaderCell.Style.ForeColor = Color.Red;

                    if (dgv.Columns.Contains(headerForecast4))
                        dgv.Columns[headerForecast4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
            }
         

            dgv.Columns[headerBal1].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
            dgv.Columns[headerBal1].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

            dgv.Columns[headerBal2].HeaderCell.Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
            dgv.Columns[headerBal2].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

            dgv.Columns[headerProduced].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Strikeout);
            dgv.Columns[headerProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[headerItemRemark].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[headerItemRemark].MinimumWidth = 200;

            dgv.EnableHeadersVisualStyles = false;
        }

        private void ColorData()
        {
            DataTable dt = (DataTable)dgvForecastReport.DataSource;
            DataGridView dgv = dgvForecastReport;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            Font _ParentFont = new Font(dgvForecastReport.Font, FontStyle.Underline);

            //Font _BalFont = new Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold);
            //Font _NeededFont = new Font(dgvForecastReport.Font, FontStyle.Italic);

            if(dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string parentColor = dt.Rows[i][headerParentColor].ToString();
                    string backColor = dt.Rows[i][headerBackColor].ToString();
                    string balType = dt.Rows[i][headerBalType].ToString();

                    //change parent color
                    if(cbSpecialTypeColorMode.Checked)
                    {
                        if (parentColor.Equals(AssemblyMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = AssemblyColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                        else if (parentColor.Equals(InsertMoldingMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InsertMouldingColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                        else if (parentColor.Equals(AssemblyAfterProductionMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = ProductionAndAssemblyColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                        else if (parentColor.Equals(InspectionMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InspectionColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                    }
                 

                    if (!string.IsNullOrEmpty(backColor) && cbRepeatedColorMode.Checked)
                    {
                        if (balType.Equals(balType_Total))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerPartCode].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerForecast1].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerForecast2].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerForecast3].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerOut].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerOutStd].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerBal1].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerBal2].Style.BackColor = Color.FromName(backColor);

                            if (dgv.Columns.Contains(headerBal3))
                            {
                                dgv.Rows[i].Cells[headerBal3].Style.BackColor = Color.FromName(backColor);
                            }
                        }
                        else
                        {
                            Color repeatedRow = Color.LightGray;

                            dgv.Rows[i].Cells[headerPartName].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerPartCode].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerForecast1].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerForecast2].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerForecast3].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerOut].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerOutStd].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerBal1].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerBal2].Style.BackColor = repeatedRow;

                            if (dgv.Columns.Contains(headerBal3))
                            {
                                dgv.Rows[i].Cells[headerBal3].Style.BackColor = repeatedRow;
                            }
                        }
                    }
                }
            }
        }

        private void ColorSummaryData()
        {
            DataTable dt = (DataTable)dgvForecastReport.DataSource;
            DataGridView dgv = dgvForecastReport;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            Font _ParentFont = dgvForecastReport.Font;
            Font _BalFont = new Font(dgvForecastReport.Font, FontStyle.Italic | FontStyle.Bold);
            Font _NeededFont = new Font(dgvForecastReport.Font, FontStyle.Italic);

            //Font _ParentFont = new Font(dgvForecastReport.Font, FontStyle.Underline);
            //Font _BalFont = new Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold);
            //Font _NeededFont = new Font(dgvForecastReport.Font, FontStyle.Italic);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parentColor = dt.Rows[i][headerParentColor].ToString();
                //string backColor = dt.Rows[i][headerBackColor].ToString();
                string type = dt.Rows[i][headerType].ToString();
                string balType = dt.Rows[i][headerBalType].ToString();
                string ToDoType = dt.Rows[i][headerToDo].ToString();

                //change parent color
                if (parentColor.Equals(AssemblyMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = Color.FromName(parentColor);
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = AssemblyColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }
                else if (parentColor.Equals(InsertMoldingMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InsertMouldingColor;
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = InsertMouldingColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }
                else if (parentColor.Equals(AssemblyAfterProductionMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = ProductionAndAssemblyColor;
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = ProductionAndAssemblyColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }
                else if (parentColor.Equals(InspectionMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InspectionColor;
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = InspectionColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }

                if(ToDoType.Equals(ToDoType_ToProduce))
                {
                    dgv.Rows[i].Cells[headerToDo].Style.BackColor = Color.Gold;
                }
                else if (ToDoType.Equals(ToDoType_ToAssembly))
                {
                    dgv.Rows[i].Cells[headerToDo].Style.BackColor = Color.LightBlue;
                }
                else if (ToDoType.Equals(ToDoType_ToOrder))
                {
                    dgv.Rows[i].Cells[headerToDo].Style.BackColor = Color.Lavender;
                }

                dgv.Rows[i].Cells[headerReadyStock].Style.BackColor = Color.Aquamarine;
                dgv.Rows[i].Cells[headerBal1].Style.BackColor = Color.LightYellow;
                dgv.Rows[i].Cells[headerBal2].Style.BackColor = Color.LightYellow;
                dgv.Rows[i].Cells[headerBal3].Style.BackColor = Color.LightYellow;

                if(dgv.Columns.Contains(headerBal4))
                dgv.Rows[i].Cells[headerBal4].Style.BackColor = Color.LightYellow;

                if (type.Equals(typeChild))
                {
                    dgv.Rows[i].Cells[headerForecast1].Style.Font = _NeededFont;
                    dgv.Rows[i].Cells[headerForecast2].Style.Font = _NeededFont;
                    dgv.Rows[i].Cells[headerForecast3].Style.Font = _NeededFont;

                    if (dgv.Columns.Contains(headerForecast4))
                    {
                        dgv.Rows[i].Cells[headerForecast4].Style.Font = _NeededFont;

                    }
                }

                if (balType.Equals(balType_Total))
                {
                    dgv.Rows[i].Cells[headerBal1].Style.Font = _BalFont;
                    dgv.Rows[i].Cells[headerBal2].Style.Font = _BalFont;

                    if (dgv.Columns.Contains(headerBal3))
                    {
                        dgv.Rows[i].Cells[headerBal3].Style.Font = _BalFont;
                    }

                    if (dgv.Columns.Contains(headerBal4))
                    {
                        dgv.Rows[i].Cells[headerBal4].Style.Font = _BalFont;

                    }

                }
            }

        }

        private void dgvForecastReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvForecastReport;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerPartCode)
            {
                string partCode = dgv.Rows[row].Cells[headerPartCode].Value.ToString();

                if (string.IsNullOrEmpty(partCode))
                {
                    dgv.Rows[row].Height = 4;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
                }
            }
            else if (dgv.Columns[col].Name == headerBal1)
            {
                float bal = dgv.Rows[row].Cells[headerBal1].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal1].Value);

                if (bal < alertLevel)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
            else if (dgv.Columns[col].Name == headerBal2)
            {
                float bal = dgv.Rows[row].Cells[headerBal2].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal2].Value);

                if (bal < alertLevel)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
            else if (dgv.Columns[col].Name == headerBal3)
            {
                float bal = dgv.Rows[row].Cells[headerBal3].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal3].Value);

                if (bal < alertLevel)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
            else if (dgv.Columns[col].Name == headerBal4)
            {
                float bal = dgv.Rows[row].Cells[headerBal4].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal4].Value);

                if (bal < alertLevel)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
            else if (dgv.Columns[col].Name == headerForecast1 || dgv.Columns[col].Name == headerForecast2 || dgv.Columns[col].Name == headerForecast3)
            {
                float forecast = dgv.Rows[row].Cells[col].Value == DBNull.Value ? -1 : 0;

                if (forecast == -1)
                {
                    //dgv.Rows[row].Cells[col].Value = "NO FORECAST/ORDER";
                    dgv.Rows[row].Cells[col].Style.BackColor = Color.Gray;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.BackColor = Color.White;
                }
            }
        }

        #endregion

        #region UI Function

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();

            string text = btnFilter.Text;

            if (text == textSearchFilter)
            {
                tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 220f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textHideFilter;
            }
            else if (text == textHideFilter)
            {
                tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textSearchFilter;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void ShowProductionPlanningFilter(bool ShowFilter)
        {
            dgvForecastReport.SuspendLayout();

            if (ShowFilter)
            {
                tlpForecastReport.RowStyles[3] = new RowStyle(SizeType.Absolute, 150f);

                dgvForecastReport.ResumeLayout();

                lblProductionPlanningMode.Text = textHideProPlanningFilter;
            }
            else
            {
                tlpForecastReport.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);

                dgvForecastReport.ResumeLayout();

                lblProductionPlanningMode.Text = textShoweProPlanningFilter;
            }
        }

        private void LoadSummaryBalMonth(ComboBox cmb)
        {
            cmb.DataSource = null;

            string monthFrom = cmbForecastFrom.Text;
          
            DataTable dt = new DataTable();
            dt.Columns.Add("Summary Bal Month Sorting");

            dt.Rows.Add(cmbForecastFrom.Text);
            dt.Rows.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).AddMonths(1).Month));
            dt.Rows.Add(cmbForecastTo.Text);

            cmb.DataSource = dt;
            cmb.DisplayMember = "Summary Bal Month Sorting";
            cmb.SelectedIndex = 2;
        }

        private void cmbForecastFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbForecastFrom.SelectedIndex != -1)
            {
                dgvForecastReport.DataSource = null;
                cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).AddMonths(2).Month);
                getStartandEndDate();
                LoadSortByComboBoxData();

                LoadSummaryBalMonth(cmbSummaryMonthBalSort);
            }
        }

        private void lblIncludeSubMat_Click(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            if (cbWithSubMat.Checked)
            {
                cbWithSubMat.Checked = false;
            }
            else
            {
                cbWithSubMat.Checked = true;
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cust = cmbCustomer.Text;
            custChanging = true;
            dgvForecastReport.DataSource = null;

            txtItemSearch.Text = "Search";
            txtItemSearch.ForeColor = SystemColors.GrayText;
            ItemSearchUIReset();

            if (cmbCustomer.SelectedIndex != -1)
            {
                getStartandEndDate();

                if (cust.Equals(tool.getCustName(1)) || cmbCustomer.Text.Equals(text.Cmb_All))
                {
                    lblPMMAChangeDate.Visible = true;
                }
                else
                {
                    lblPMMAChangeDate.Visible = false;
                }

                LoadItemPurchaseByCustomer();
            }

            custChanging = false;
            lblProductionPlanningMode.Visible = false;

        }

        private void lblChangeDate_Click(object sender, EventArgs e)
        {
            lblPMMAChangeDate.ForeColor = Color.Purple;
            frmPMMADateEdit frm = new frmPMMADateEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            lblPMMAChangeDate.ForeColor = Color.Blue;

            if (frmPMMADateEdit.dateChanged)
            {
                getStartandEndDate();
            }
        }

        private void cmbForecastTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbForecastFrom.SelectedIndex != -1 && cmbForecastTo.SelectedIndex != -1 && loaded)
            {
                int monthFrom = DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).Month;
                int monthTo = DateTime.ParseExact(cmbForecastTo.Text, "MMMM", CultureInfo.CurrentCulture).Month;

                monthFrom += 2;

                if (monthFrom > 12)
                {
                    monthFrom -= 12;
                }

                if (monthFrom != monthTo)
                {
                    MessageBox.Show("Total forecast months must be exactly 3!\nSystem will correct it after this message.");

                    cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthFrom);
                }
            }
        }

        private void lblCurrentMonth_Click(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            if (cmbForecastFrom.SelectedIndex != -1)
            {
                cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvForecastReport.SelectionMode = DataGridViewSelectionMode.CellSelect;
            ProPlanningSearchReset();

            DT_ITEM = dalItem.Select();

            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                FullForecastReport();

                if (!(txtItemSearch.Text.Length == 0 || txtItemSearch.Text == text.Search_DefaultText))
                {
                    lblSearchClear.Visible = true;
                    ItemSearch();
                }

                if(dgvForecastReport.DataSource != null)
                lblProductionPlanningMode.Visible = true;
            }
        }

        private void ItemSearchUIReset()
        {
            CURRENT_ROW_JUMP = -1;
            lblSearchInfo.Text = "";
            Row_Index_Found = new List<int>();

            lblResultNo.Text = "";
            //lblSearchClear.Visible = false;
            btnPreviousSearchResult.Enabled = false;
            btnNextSearchResult.Enabled = false;
        }
        private void ItemSearch()
        {

            ItemSearchUIReset();
            string Searching_Text = txtItemSearch.Text.ToUpper();

            if(Searching_Text != "SEARCH" )
            {
                DataGridView dgv = dgvForecastReport;

                DataTable dgv_List = (DataTable)dgv.DataSource;

                int itemFoundCount = 0;

                //search data and row jump
                if (dgv_List != null && !string.IsNullOrEmpty(Searching_Text))
                    foreach (DataRow row in dgv_List.Rows)
                    {
                        //dt_Row[headerPartCode] = uData.part_code;
                        //dt_Row[headerPartName] = uData.part_name;
                        string itemCode = row[headerPartCode].ToString().ToUpper();
                        string itemName = row[headerPartName].ToString().ToUpper();

                        if (itemCode.ToUpper().Contains(Searching_Text) || itemName.ToUpper().Contains(Searching_Text))
                        {
                            int rowIndex = dgv_List.Rows.IndexOf(row);

                            Row_Index_Found.Add(rowIndex);
                        }

                    }

                //remove duplicate data
                Row_Index_Found = Row_Index_Found.Distinct().ToList();

                itemFoundCount = Row_Index_Found.Count;

                lblSearchInfo.Text = itemFoundCount + textRowFound;

                if (itemFoundCount > 0)
                {
                    JumpToNextRow();
                    btnPreviousSearchResult.Enabled = false;
                    btnNextSearchResult.Enabled = true;
                }

            }

        }

        private int FindParentIndex(int rowIndexSearching)
        {
            int parentIndex = rowIndexSearching;

            DataTable dt = (DataTable)dgvForecastReport.DataSource;

            string itemType = dt.Rows[rowIndexSearching][headerType].ToString();

            if(dt != null && itemType != typeSingle && itemType != typeParent)
            {
                for (int i = rowIndexSearching; i >= 0; i--)
                {
                    itemType = dt.Rows[i][headerType].ToString();

                    if (itemType == typeParent)
                    {
                        return i;
                    }
                }
            }

            return parentIndex;
        }
        private void JumpToNextRow()
        {
            var last = Row_Index_Found.Last();

            foreach(var i in Row_Index_Found)
            {
                if(i > CURRENT_ROW_JUMP)
                {
                    CURRENT_ROW_JUMP = i;


                    dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);

                    dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                    dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];


                    btnPreviousSearchResult.Enabled = true;

                    //check if last row
                    btnNextSearchResult.Enabled = !(i == last);

                    lblResultNo.Text = "#" + (Row_Index_Found.IndexOf(i)+1).ToString();

                    return;
                }
            }

            if(CURRENT_ROW_JUMP != -1)
            {
                dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];

            }
        }

        private void BackToPreviousRow()
        {
            var first = Row_Index_Found.First();

            if(Row_Index_Found.Count - 1 >= 0)
                for (int i = Row_Index_Found.Count - 1 ; i >= 0 ; i--)
                {
                    if (Row_Index_Found[i] < CURRENT_ROW_JUMP)
                    {
                        CURRENT_ROW_JUMP = Row_Index_Found[i];
                        dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                        dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                        dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];

                        //check if first row
                        btnPreviousSearchResult.Enabled = !(CURRENT_ROW_JUMP == first);

                        btnNextSearchResult.Enabled = true;

                        lblResultNo.Text = "#" + (i+1).ToString();

                        return;
                    }
               
                }


            if (CURRENT_ROW_JUMP != -1)
            {
                dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];
            }
        }

        private void ShowAllCustomerForecastReport()
        {
            SummaryMode = false;

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvForecastReport;

            dgv.DataSource = FullDetailForecastData();//9562ms-->3464ms (16/3) -> 2537ms (17/3)
           

            if (dgv.DataSource != null)
            {
                ColorData();//720ms (16/3)

                dgvForecastReport.Columns.Remove(headerItemType);

                if (cbSpecialTypeColorMode.Checked)
                    dgvForecastReport.Columns.Remove(headerParentColor);

                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);

                DgvForecastReportUIEdit(dgvForecastReport); //973ms (16/3)

                //dgvForecastReport.Columns.Remove(headerItemType);

                //if (cbSpecialTypeColorMode.Checked)
                //    dgvForecastReport.Columns.Remove(headerParentColor);

                //dgvForecastReport.Columns.Remove(headerType);
                //dgvForecastReport.Columns.Remove(headerBackColor);
                //dgvForecastReport.Columns.Remove(headerBalType);
                //dgvForecastReport.Columns.Remove(headerForecastType);

                dgvForecastReport.ResumeLayout();

                dgvForecastReport.ClearSelection();

                 dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

               dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
            }

            frmLoading.CloseForm();
        }

        private void FullForecastReport()
        {
            SummaryMode = false;

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvForecastReport;

            DT_FORECAST_REPORT = FullDetailForecastData();//9562ms-->3464ms (16/3) -> 2537ms (17/3)

            dgv.DataSource = DT_FORECAST_REPORT;


            if (dgv.DataSource != null)
            {
                ColorData();//720ms (16/3)

                dgvForecastReport.Columns.Remove(headerItemType);

                if (cbSpecialTypeColorMode.Checked)
                    dgvForecastReport.Columns.Remove(headerParentColor);

                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);

                DgvForecastReportUIEdit(dgvForecastReport); //973ms (16/3)

                dgvForecastReport.ResumeLayout();

                dgvForecastReport.ClearSelection();

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
            }

            frmLoading.CloseForm();
        }

        //private void NewFullForecastReport()
        //{
        //    SummaryMode = false;

        //    frmLoading.ShowLoadingScreen();

        //    DataGridView dgv = dgvForecastReport;

        //    //DT_FORECAST_REPORT = NewFullDetailForecastData();//9562ms-->3464ms (16/3) -> 2537ms (17/3)
        //    DT_FORECAST_REPORT = NewFullDetailForecastData(txtItemSearch.Text);
        //    dgv.DataSource = DT_FORECAST_REPORT;


        //    if (dgv.DataSource != null)
        //    {
        //        ColorData();//720ms (16/3)

        //        dgvForecastReport.Columns.Remove(headerItemType);

        //        if (cbSpecialTypeColorMode.Checked)
        //            dgvForecastReport.Columns.Remove(headerParentColor);

        //        dgvForecastReport.Columns.Remove(headerType);
        //        dgvForecastReport.Columns.Remove(headerBackColor);
        //        dgvForecastReport.Columns.Remove(headerBalType);
        //        dgvForecastReport.Columns.Remove(headerForecastType);

        //        DgvForecastReportUIEdit(dgvForecastReport); //973ms (16/3)

        //        dgvForecastReport.ResumeLayout();

        //        dgvForecastReport.ClearSelection();

        //        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

        //        dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
        //    }

        //    frmLoading.CloseForm();
        //}

        private void ShowDetailForecastReport()
        {
            SummaryMode = false;
            frmLoading.ShowLoadingScreen();
            DataGridView dgv = dgvForecastReport;
            dgv.DataSource = SearchForecastData();
     
            if (dgv.DataSource != null)
            {
                ColorData();
                DgvForecastReportUIEdit(dgvForecastReport);
                dgvForecastReport.Columns.Remove(headerItemType);
                if (cbSpecialTypeColorMode.Checked)
                    dgvForecastReport.Columns.Remove(headerParentColor);
                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);
                dgvForecastReport.ClearSelection();
                dgvForecastReport.ResumeLayout();
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }
            frmLoading.CloseForm();//568ms,515ms,531ms
        }

        private void ShowSummaryForecastReport()
        {
            SummaryMode = true;

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvForecastReport;

            //if (cmbCustomer.Text == text.Cmb_All)
            //     dt = SearchAllCustomerForecastData();
            //else
            //{
            //    dt = SearchForecastData();//3804

            //}

            DT_FORECAST_REPORT = FullDetailForecastData();

            dt_SummaryList = LoadSummaryList(DT_FORECAST_REPORT);

            DT_FORECAST_REPORT = dt_SummaryList;

            #region Load Data To Datagridview

            dgvForecastReport.DataSource = dt_SummaryList;

            dgvForecastReport.Columns.Remove(headerRowReference);
            //dgvForecastReport.Columns.Remove(headerColorMat);
            dgvForecastReport.Columns.Remove(headerPartWeight);

            ColorSummaryData();//2075

            //delete column
            dgvForecastReport.Columns.Remove(headerItemType);
            dgvForecastReport.Columns.Remove(headerParentColor);
            dgvForecastReport.Columns.Remove(headerType);
            dgvForecastReport.Columns.Remove(headerBackColor);
            dgvForecastReport.Columns.Remove(headerBalType);
            dgvForecastReport.Columns.Remove(headerForecastType);

            DgvForecastSummaryReportUIEdit(dgvForecastReport);

            #endregion


            if (dgv.DataSource != null)
            {
                //ColorData();

                //DgvForecastReportUIEdit(dgvForecastReport);

                //dgvForecastReport.Columns.Remove(headerItemType);

                if (cbSpecialTypeColorMode.Checked && dgv.Columns.Contains(headerParentColor))
                    dgvForecastReport.Columns.Remove(headerParentColor);


                dgvForecastReport.ClearSelection();
                dgvForecastReport.ResumeLayout();

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }

            frmLoading.CloseForm();

        }


        private void dtpOutFrom_ValueChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cmbSoryBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void txtAlertLevel_TextChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void dtpOutTo_ValueChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cbWithSubMat_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cbDescending_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            cmbSoryBy.SelectedIndex = -1;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //if (cmbCustomer.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please select a customer.");
            //}
            //else
            //{
            //    LoadForecastData();
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (txtItemSearch.Text.Length == 0 || txtItemSearch.Text == text.Search_DefaultText)
            {
                lblSearchClear.Visible = false;
                ItemSearchUIReset();
            }
            else
            {
                lblSearchClear.Visible = true;
                ItemSearch();
            }

            //dgvForecastReport.DataSource = null;

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        #endregion

        #region Loading Data

        private void frmForecastReport_NEW_Load(object sender, EventArgs e)
        {
            

            DT_MACHINE_SCHEDULE = dalPlanning.SelectCompletedOrRunningPlan();
            DT_PMMA_DATE = dalPmmaDate.Select();
            DT_JOIN = dalJoin.SelectAllWithChildCat();
            //DT_JOIN = dalJoin.SelectAll();
            //DT_ITEM = dalItem.Select();
            
            //default all customer item list setting

            DataTable dt_Item_Cust = dalItemCust.SelectAllExcludedOTHER();

            //filter out terminated item
            if (!cbIncludeTerminated.Checked && DT_ITEM_CUST != null)
                dt_Item_Cust = RemoveTerminatedItem(dt_Item_Cust);

            //dt_MacSchedule = dalPlanning.SelectOrderByItem();

            //482ms
            //dt_ProRecord = dalProRecord.SelectWithItemInfoSortByItem();
            //5248ms

            //DataTable dt = dalPlanning.SelectCompletedOrRunningPlan();

            dgvForecastReport.SuspendLayout();

            tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
            tlpForecastReport.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);

            dgvForecastReport.ResumeLayout();

            btnFilter.Text = textSearchFilter;

            cmbCustomer.Focus();
            loaded = true;

            string cust = cmbCustomer.Text;
            custChanging = true;

            if (cmbCustomer.SelectedIndex != -1)
            {
                getStartandEndDate();

                if (cust.Equals(tool.getCustName(1)) || cmbCustomer.Text.Equals(text.Cmb_All))
                {
                    lblPMMAChangeDate.Visible = true;
                }
                else
                {
                    lblPMMAChangeDate.Visible = false;
                }
            }
            custChanging = false;

            ifNewForeacstUpdatesFound();

        }

        private void ifNewForeacstUpdatesFound()
        {
            if(ForecastEditRecordChecked())
            {
                //new updates found
                lblForecastHistoryNotification.Text = "(NEW) Foreacst Edit Record";
                lblForecastHistoryNotification.BackColor = Color.Yellow;
                lblForecastHistoryNotification.ForeColor = Color.Red;
            }
            else
            {
                lblForecastHistoryNotification.Text = "Foreacst Edit Record";
                lblForecastHistoryNotification.BackColor = Color.Transparent;
                lblForecastHistoryNotification.ForeColor = Color.Black;

            }
        }

        private void ifNewForeacstUpdatesFound(bool newData)
        {
            if (newData)
            {
                //new updates found
                lblForecastHistoryNotification.Text = "(NEW) Foreacst Edit Record";
                lblForecastHistoryNotification.BackColor = Color.Yellow;
                lblForecastHistoryNotification.ForeColor = Color.Red;

            }
            else
            {
                lblForecastHistoryNotification.Text = "Foreacst Edit Record";
                lblForecastHistoryNotification.BackColor = Color.Transparent;
                lblForecastHistoryNotification.ForeColor = Color.Black;

            }
        }

        private void InitializeFilterData()
        {
            loaded = false;

            cmbForecastFrom.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                     .MonthNames.Take(12).ToList();

            cmbForecastTo.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                    .MonthNames.Take(12).ToList();

            cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(2).Month);

            LoadSortByComboBoxData();

            loaded = true;
        }

        private void LoadSortByComboBoxData()
        {
            ComboBox cmb = cmbSoryBy;
            cmb.DataSource = null;

            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";

            headerBal1 = "BAL";
            headerBal2 = "BAL";
            headerBal3 = "BAL";

            sortBy_Forecast1 = "Forecast/Needed";
            sortBy_Forecast2 = "Forecast/Needed";
            sortBy_Forecast3 = "Forecast/Needed";

            sortBy_Bal1 = "Balance";
            sortBy_Bal2 = "Balance";

            string monthFrom = cmbForecastFrom.Text;

            string monthName = string.IsNullOrEmpty(monthFrom) || cmbForecastFrom.SelectedIndex == -1 ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) : monthFrom;
            int monthINT = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;

            for (int i = 1; i <= 3; i++)
            {
                if (i == 1)
                {
                    headerForecast1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast1;
                    headerBal1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal1;
                }

                else if (i == 2)
                {
                    headerForecast2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast2;
                    headerBal2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal2;
                }

                else if (i == 3)
                {
                    headerForecast3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast3;
                    headerBal3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal3;

                }

                monthINT++;

                if (monthINT > 12)
                {
                    monthINT -= 12;

                }

                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            }

            DataTable dt = new DataTable();
            dt.Columns.Add("SORT BY");
          
            dt.Rows.Add(headerBal1);
            dt.Rows.Add(headerBal2);
            dt.Rows.Add(headerBal3);
            dt.Rows.Add("");
            dt.Rows.Add(headerForecast1);
            dt.Rows.Add(headerForecast2);
            dt.Rows.Add(headerForecast3);
            dt.Rows.Add("");
            dt.Rows.Add(headerReadyStock);
            dt.Rows.Add(headerEstimate);
            dt.Rows.Add("");
            dt.Rows.Add(headerOut);
            dt.Rows.Add(headerOutStd);

            cmb.DataSource = dt;
            cmb.DisplayMember = "SORT BY";
            cmb.SelectedIndex = -1;
        }

        
       

        private int CalculateEstimateOrder(DataTable DT_PMMA_DATE, DataTable dt_trfToCustomer, string _ItemCode, string _Customer)
        {
            int estimateOrder = 0;

            int preMonth = 0, preYear = 0, dividedQty = 0;
            double singleOutQty = 0, totalTrfOutQty = 0;

            int monthNow = DateTime.Now.Month;
            int yearNow = DateTime.Now.Year;


            if(_ItemCode == "V51KM4100")
            {
                float checkpoint = 1;
            }

            foreach (DataRow row in dt_trfToCustomer.Rows)
            {
                string trfResult = row[dalTrfHist.TrfResult].ToString();
                string itemCode = row[dalTrfHist.TrfItemCode].ToString();

                if (trfResult == "Passed" && _ItemCode == itemCode)
                {

                    double trfQty = double.TryParse(row[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;
                    DateTime trfDate = DateTime.TryParse(row[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                    int month = 0;
                    int year = 0;

                    if (trfDate == DateTime.MaxValue)
                    {
                        continue;
                    }
                    else
                    {
                        if (_Customer == "PMMA")
                        {
                            //day = trfDate.Day;
                            //month = trfDate.Month;
                            //year = trfDate.Year;

                            DateTime pmmaDate = tool.GetPMMAMonthAndYear(trfDate, DT_PMMA_DATE);

                            if (pmmaDate != DateTime.MaxValue)
                            {
                                month = pmmaDate.Month;
                                year = pmmaDate.Year;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            month = trfDate.Month;
                            year = trfDate.Year;
                        }

                        if (month != monthNow || year != yearNow)
                        {
                            totalTrfOutQty += trfQty;

                        }
                        
                        if (preMonth != 0 && preYear != 0 && preMonth == month && preYear == year)
                        {
                            singleOutQty += trfQty;
                        }
                        else
                        {
                            singleOutQty = trfQty;
                            preMonth = month;
                            preYear = year;

                            if (month != monthNow || year != yearNow)
                            {
                                dividedQty ++;
                            }
                            
                        }

                    }
                }
            }

            if(totalTrfOutQty == 0 || dividedQty == 0)
            {
                estimateOrder = 0;
            }
            else
            {
                estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty/100, 0) * 100;

                if(estimateOrder == 0)
                estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty, 0) ;
            }

            return estimateOrder;
        }

        private Tuple<int,bool, double> EstimateNextOrderAndCheckIfStillActive(DataTable DT_PMMA_DATE, DataTable dt_trfToCustomer, string _ItemCode, string _Customer)
        {
            int estimateOrder = 0;
            bool NonActiveItem = true;

            int selectedCurentMonth = DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).Month;

            int preMonth = 0, preYear = 0, dividedQty = 0;
            double singleOutQty = 0, totalTrfOutQty = 0;

            double deliveredQtyThisMonth = 0;

            int monthNow = DateTime.Now.Month;
            int yearNow = DateTime.Now.Year;

            int inactiveMonthsThreshold = int.TryParse(txtInactiveMonthsThreshold.Text, out int i) ? i : 0;

            int MonthAgo = DateTime.Now.AddMonths(inactiveMonthsThreshold * -1).Month;
            int YearAgo = DateTime.Now.AddMonths(inactiveMonthsThreshold * -1).Year;
            DateTime ActiveMinmumDate = new DateTime(Convert.ToInt32(YearAgo), Convert.ToInt32(MonthAgo), 1);


            foreach (DataRow row in dt_trfToCustomer.Rows)
            {
                string trfResult = row[dalTrfHist.TrfResult].ToString();
                string itemCode = row[dalTrfHist.TrfItemCode].ToString();
                string DB_Customer = row[dalTrfHist.TrfTo].ToString();

                if (trfResult == "Passed" && _ItemCode == itemCode && DB_Customer == _Customer)
                {

                    double trfQty = double.TryParse(row[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;
                    DateTime trfDate = DateTime.TryParse(row[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                    int month = 0;
                    int year = 0;

                    if (trfDate == DateTime.MaxValue)
                    {
                        continue;
                    }
                    else
                    {
                        if (_Customer == "PMMA")
                        {
                            if (trfDate >= dtpPMMAOutFrom.Value && trfDate <= dtpPMMAOutTo.Value)
                            {
                                deliveredQtyThisMonth += trfQty;
                            }
                            else
                            {
                                totalTrfOutQty += trfQty;
                            }

                            DateTime pmmaDate = tool.GetPMMAMonthAndYear(trfDate, DT_PMMA_DATE);

                            if (pmmaDate != DateTime.MaxValue)
                            {
                                month = pmmaDate.Month;
                                year = pmmaDate.Year;

                                //if (month == selectedCurentMonth && year == yearNow)
                                //{
                                //    deliveredQtyThisMonth += trfQty;
                                //}
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (trfDate >= dtpOutFrom.Value && trfDate <= dtpOutTo.Value)
                            {
                                deliveredQtyThisMonth += trfQty;
                            }
                            else
                            {
                                totalTrfOutQty += trfQty;
                            }

                            month = trfDate.Month;
                            year = trfDate.Year;
                        }


                        DateTime TransferDate = new DateTime(Convert.ToInt32(year),Convert.ToInt32(month),1);

                        if (TransferDate >= ActiveMinmumDate)
                        {
                            NonActiveItem = false;
                        }

                        if (preMonth != 0 && preYear != 0 && preMonth == month && preYear == year)
                        {
                            singleOutQty += trfQty;
                        }
                        else
                        {
                            singleOutQty = trfQty;
                            preMonth = month;
                            preYear = year;

                            if (month != monthNow || year != yearNow)
                            {
                                dividedQty++;
                            }

                        }

                    }
                }
            }

            if (totalTrfOutQty == 0 || dividedQty <= 2)
            {
                estimateOrder = 0;
            }
            else
            {
                estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty / 100, 0) * 100;

                if (estimateOrder == 0)
                    estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty, 0);
            }

            return Tuple.Create(estimateOrder, NonActiveItem, deliveredQtyThisMonth);
        }

        private Tuple<int, int> GetProduceQty(string _ItemCode, DataTable dt_MacSechedule)
        {
            int toProduce = 0, produced = 0;
           
            //DateTime start = dtpPMMAOutFrom.Value;
            //DateTime end = dtpPMMAOutTo.Value;

            DateTime now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);

            bool itemFound = false;

           //DataRow[] rowSchedule = dt_MacSechedule.Select(dalPlanning.partCode + " = '"+ _ItemCode + "'");

            foreach (DataRow row in dt_MacSechedule.Rows)
            {
                string itemCode = row[dalPlanning.partCode].ToString();

                if (itemCode == _ItemCode)
                {
                    itemFound = true;
                    DateTime produceStart = DateTime.TryParse(row[dalPlanning.productionStartDate].ToString(), out produceStart) ? produceStart : DateTime.MaxValue;

                    string status = row[dalPlanning.planStatus].ToString();

                    int TargetQty = int.TryParse(row[dalPlanning.targetQty].ToString(), out TargetQty) ? TargetQty : 0;

                    int producedQty = int.TryParse(row[dalPlanning.planProduced].ToString(), out producedQty) ? producedQty : 0;

                    int ToProduceQty = TargetQty - producedQty;

                    if (ToProduceQty < 0)
                    {
                        ToProduceQty = 0;
                    }

                    if (produceStart >= start && produceStart <= end && status == text.planning_status_completed)
                    {
                        produced += producedQty;
                    }

                    if (status == text.planning_status_running || status == text.planning_status_pending)
                    {
                        toProduce += ToProduceQty;
                        produced += producedQty;

                    }
                }

                else if (itemFound)
                {
                    break;
                }

            }


            return Tuple.Create(toProduce, produced);
        }

        private string GetMachineHistory(string _ItemCode)
        {
            string MachineHistory = " Mac : ";

            List<string> listMacName = new List<string>();

            DT_MACHINE_SCHEDULE_COPY.AcceptChanges();

            foreach (DataRow row in DT_MACHINE_SCHEDULE_COPY.Rows)
            {
                string itemCode = row[dalPlanning.partCode].ToString();

                if (itemCode == _ItemCode)
                {
                    string macName = row[dalPlanning.machineName].ToString();
                    listMacName.Add(macName);
                    row.Delete();
                }
            }

            DT_MACHINE_SCHEDULE_COPY.AcceptChanges();

            List<string> noDupes = listMacName.Distinct().ToList();

            for (int i = 0; i < noDupes.Count; i++)
            {
                MachineHistory += noDupes[i];
                MachineHistory += (i == noDupes.Count - 1) ? ";" : ", ";
            }

            return MachineHistory;
        }


        private DataTable RemoveTerminatedItem(DataTable dt)
        {
            //"(TERMINATED)"
            DataTable dt_NEW = dt.Copy();

            if(dt_NEW != null)
            {
                dt_NEW.AcceptChanges();
                foreach (DataRow row in dt_NEW.Rows)
                {
                    // If this row is offensive then
                    string itemName = row[dalItem.ItemName].ToString();

                    if (itemName.Contains(text.Cat_Terminated))
                    {
                        row.Delete();
                    }

                }
                dt_NEW.AcceptChanges();
            }
           
            return dt_NEW;

        }

        private DataTable RemoveOtherFromTransferRecord(DataTable dt)
        {
            //"(TERMINATED)"
            DataTable dt_NEW = dt.Copy();

            dt_NEW.AcceptChanges();
            foreach (DataRow row in dt_NEW.Rows)
            {
                // If this row is offensive then
                string Customer = row[dalTrfHist.TrfTo].ToString();

                if (Customer.ToUpper().Contains("OTHER"))
                {
                    row.Delete();
                }

            }
            dt_NEW.AcceptChanges();

            return dt_NEW;

        }

        private DataTable LoadSummaryList(DataTable dt)
        {
            DataTable dt_Copy = dt.Copy();
            dt_Copy.Columns.Add(headerToDo, typeof(string)).SetOrdinal(0);

            #region Filter Row

            if (dt_Copy.Rows.Count > 0)
            {
                for (int i = 0; i <= dt_Copy.Rows.Count - 1; i++)
                {

                    bool ToRemoveMatched = false;

                    string balType = dt_Copy.Rows[i][headerBalType].ToString();
                    string itemType = dt_Copy.Rows[i][headerItemType].ToString();
                    string ParentOrChild = dt_Copy.Rows[i][headerType].ToString();

                    string RawMaterial = dt_Copy.Rows[i][headerRawMat].ToString();
                    decimal bal_1 = decimal.TryParse(dt_Copy.Rows[i][headerBal1].ToString(), out decimal d) ? d : 0;
                    decimal bal_2 = decimal.TryParse(dt_Copy.Rows[i][headerBal2].ToString(), out d) ? d : 0;
                    decimal bal_3 = decimal.TryParse(dt_Copy.Rows[i][headerBal3].ToString(), out d) ? d : 0;

                    string ToDOType = "";

                    if (balType != balType_Unique && balType != balType_Total)
                    {
                        ToRemoveMatched = true;
                    }

                    if (cbShowInsufficientOnly.Checked && bal_1 >= 0 && bal_2 >= 0 && bal_3 >= 0)
                    {
                        ToRemoveMatched = true;
                    }

                    if (!string.IsNullOrEmpty(RawMaterial))
                    {
                        ToDOType = ToDoType_ToProduce;

                        if (!cbShowToProduceItem.Checked)
                        {
                            ToRemoveMatched = true;
                        }
                    }
                    else if (ParentOrChild.Equals(typeParent) || itemType.Equals(text.Cat_Part))
                    {
                        ToDOType = ToDoType_ToAssembly;

                        if (!cbShowToAssemblyItem.Checked)
                        {
                            ToRemoveMatched = true;
                        }
                    }
                    else if (itemType.Equals(text.Cat_SubMat))
                    {
                        ToDOType = ToDoType_ToOrder;

                        if (!cbShowToOrderItem.Checked)
                        {
                            ToRemoveMatched = true;
                        }
                    }



                    if (ToRemoveMatched)
                        dt_Copy.Rows[i].Delete();
                    else
                        dt_Copy.Rows[i][headerToDo] = ToDOType;


                }

                dt_Copy.AcceptChanges();
            }

            #endregion

            #region Sorting

            string SortString = "";

            if (cbSortByToDOType.Checked)
            {
                SortString = headerToDo + " ASC";
            }

            if (cbSortByBalance.Checked)
            {
                string MonthSelected = cmbSummaryMonthBalSort.Text;

                string headerToSort = headerBal3 + " ASC, " + headerBal1 + " ASC, " + headerBal2 + " ASC";

                if (MonthSelected == cmbForecastFrom.Text)
                {
                    headerToSort = headerBal1 + " ASC, " + headerBal2 + " ASC, " + headerBal3 + " ASC";
                }
                else if(MonthSelected != cmbForecastTo.Text)
                {
                    headerToSort = headerBal2 + " ASC, " + headerBal1 + " ASC, " + headerBal3 + " ASC";

                }

                if (string.IsNullOrEmpty(SortString))
                {

                    SortString = headerToSort;
                }
                else
                {
                    SortString += ", " + headerToSort;
                }
            }

            if (!string.IsNullOrEmpty(SortString))
            {
                dt_Copy.DefaultView.Sort = SortString;

                dt_Copy = dt_Copy.DefaultView.ToTable();
            }

            #endregion

            return dt_Copy;
        }

        private void LoadItemPurchaseByCustomer()
        {
            string customerName = cmbCustomer.Text;

            DT_ITEM_CUST = new DataTable();

            if (customerName == text.Cmb_All)
            {
                DT_ITEM_CUST = dalItemCust.SelectAllExcludedOTHER();
            }
            else if (!string.IsNullOrEmpty(customerName)) 
            {
                DT_ITEM_CUST = dalItemCust.custSearch(customerName);
            }


            if (!cbIncludeTerminated.Checked && DT_ITEM_CUST != null)
                DT_ITEM_CUST = RemoveTerminatedItem(DT_ITEM_CUST);

            DT_ITEM_CUST.Columns.Add(text.Header_GotNotPackagingChild);
            //DT_ITEM_CUST.Columns.Add(text.Header_Forecast_1);
            //DT_ITEM_CUST.Columns.Add(text.Header_Forecast_2);
            //DT_ITEM_CUST.Columns.Add(text.Header_Forecast_3);

            if (DT_ITEM_CUST != null)
            {
                DT_JOIN = DT_JOIN != null && DT_JOIN.Rows.Count > 0 ? DT_JOIN: dalJoin.SelectAllWithChildCat();
                DT_ITEM = DT_ITEM != null && DT_ITEM.Rows.Count > 0 ? DT_ITEM : dalItem.Select();

                //var getForecastPeriod = getMonthYearPeriod();
                //DataTable dt_ItemForecast = dalItemForecast.SelectWithRange(getForecastPeriod.Item1, getForecastPeriod.Item2, getForecastPeriod.Item3, getForecastPeriod.Item4);


                foreach (DataRow row in DT_ITEM_CUST.Rows)
                {
                    string itemCode = row[dalItem.ItemCode].ToString();
                    //string custID = row[dalItemCust.CustID].ToString();

                    bool gotNotPackagingChild = tool.ifGotNotPackagingChild(itemCode, DT_JOIN, DT_ITEM);
                    row[text.Header_GotNotPackagingChild] = gotNotPackagingChild;

                    //var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, custID, itemCode, 1, 2, 3);
                    //row[text.Header_Forecast_1] = forecastData.Item1;
                    //row[text.Header_Forecast_2] = forecastData.Item2;
                    //row[text.Header_Forecast_3] = forecastData.Item3;

                }
            }
        }


        private DataTable DT_SEMENYIH_STOCK;

        private decimal deductSemenyihStock(string itemCode)
        {
            if(DT_SEMENYIH_STOCK == null || DT_SEMENYIH_STOCK.Rows.Count <= 0)
            {
                DT_SEMENYIH_STOCK = dalStock.SelectFactoryStock("10");//Semenyih Fac id(10) in OUG DB
            }

            decimal TotalStock = 0;

            if (DT_SEMENYIH_STOCK.Rows.Count > 0)
            {
                foreach (DataRow stock in DT_SEMENYIH_STOCK.Rows)
                {
                    if(itemCode == stock["stock_item_code"].ToString())
                    {
                        decimal qty = decimal.TryParse(stock["stock_qty"].ToString(), out decimal x) ? x : 0;

                        TotalStock += qty;

                        break;
                    }
                }
            }
            return TotalStock;
        }

        private DataTable FullDetailForecastData()
        {
            #region Setting

            Cursor = Cursors.WaitCursor;

            lblForecastType.Text = ReportType_Full;

            btnFullReport.Enabled = false;
            btnSummary.Enabled = false;
            btnRefresh.Enabled = false;
            btnExcel.Enabled = false;
            btnExcelAll.Enabled = false;

            DataGridView dgv = dgvForecastReport;

            alertLevel = float.TryParse(txtAlertLevel.Text, out alertLevel) ? alertLevel : 0;
            dgvForecastReport.SuspendLayout();
            lblLastUpdated.Visible = true;
            lblUpdatedTime.Visible = true;

            lblUpdatedTime.Text = DateTime.Now.ToString();
            dgvForecastReport.DataSource = null;

            DataTable dt_Data = NewForecastReportTable();

            DataRow dt_Row;

            var getForecastPeriod = getMonthYearPeriod();

            DataTable dt_ItemForecast = dalItemForecast.SelectWithRange(getForecastPeriod.Item1, getForecastPeriod.Item2, getForecastPeriod.Item3, getForecastPeriod.Item4);

            DateTime DateToday = DateTime.Now;
            int year = DateToday.Year - 1;
            DateTime LastYear = DateTime.Parse("1/1/" + year);

            string deliveredHistory_From = LastYear.ToString("yyyy/MM/dd");
            string deliveredHistory_To = DateToday.AddMonths(1).ToString("yyyy/MM/dd");

            DataTable dt_TrfHist = dalTrfHist.ItemDeliveredRecordSearch(deliveredHistory_From, deliveredHistory_To);

            dt_TrfHist = RemoveOtherFromTransferRecord(dt_TrfHist);

            DataTable DT_PMMA_DATE = dalPmmaDate.Select();
            
            int index = 1;

            #endregion

            #region Load Data

            #region load single part
            //308ms 
            foreach (DataRow row in DT_ITEM_CUST.Rows)
            {

                string itemSearch = row[dalItem.ItemCode].ToString();

                if(itemSearch == "A0LK160R0")
                {
                    var checkpoint = 1;
                }

                bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

                if (!gotNotPackagingChild)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();
                    uData.customer_name = row[dalItemCust.CustName].ToString();
                    uData.cust_id = row[dalItemCust.CustID].ToString();

                    var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, uData.cust_id, uData.part_code, 1, 2, 3);
                    uData.forecast1 = forecastData.Item1;
                    uData.forecast2 = forecastData.Item2;
                    uData.forecast3 = forecastData.Item3;

                    var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, uData.customer_name);

                    uData.estimate = estimate.Item1;
                    bool NonActiveItem = estimate.Item2;
                    uData.deliveredOut = (float)estimate.Item3;

                    if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NonActiveItem && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                    {
                        uData.item_remark = row[dalItem.ItemRemark].ToString();

                        int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                        int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);

                        //uData.pw_per_shot = (float) Decimal.Round((decimal) uData.pw_per_shot, 2);

                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);

                        //uData.rw_per_shot = (float) Decimal.Round((decimal) uData.rw_per_shot, 2);

                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        decimal SemenyihStock = deductSemenyihStock(uData.part_code);
              

                        //uData.ready_stock = uData.ready_stock - (float) SemenyihStock;
                        uData.ready_stock = uData.ready_stock;


                        if (myconnstrng == text.DB_Semenyih)
                        {
                            uData.ready_stock = (float)SemenyihStock;
                        }

                        var result = GetProduceQty(uData.part_code, DT_MACHINE_SCHEDULE);
                        uData.toProduce = result.Item1;
                        uData.Produced = result.Item2;

                        #region Balance Calculation

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (uData.forecast1 == -1)
                        {
                            uData.outStd = 0;
                        }
                        else if (uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            uData.bal2 = uData.bal1 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }

                        uData.bal3 = uData.bal2 - uData.forecast3;

                        if (uData.forecast3 == -1)
                        {
                            uData.bal3 = uData.bal2 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast3 > -1)
                        {
                            uData.bal3 = uData.bal2 - uData.forecast3;
                        }

                        #endregion

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        if (uData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uData.toProduce;
                        }

                        if (uData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uData.Produced;
                        }

                        dt_Row[headerItemRemark] = uData.item_remark;
                        dt_Row[headerIndex] = uData.index;

                        if(dt_Data.Columns.Contains(text.Header_Customer))
                        dt_Row[text.Header_Customer] = uData.customer_name;

                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[text.Header_ColorMatCode] = row[dalItem.ItemMBatch].ToString();

                        dt_Row[headerPartWeight] = (uData.pw_per_shot / uData.cavity).ToString("0.##") + " (" + (uData.rw_per_shot / uData.cavity).ToString("0.##") + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;
                        dt_Row[headerBal3] = uData.bal3;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        dt_Data.Rows.Add(dt_Row);
                        index++;
                    }
                }

            }

            #endregion
            //^^^3930ms -> 1497ms -> 1000ms (17/3)

            #region load assembly part

            foreach (DataRow row in DT_ITEM_CUST.Rows)
            {

                string itemSearch = row[dalItem.ItemCode].ToString();

                if (itemSearch == "A0LK160R0")
                {
                    var checkpoint = 1;
                }

                int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);
                bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

                //check if got child part also
                if ((assembly == 1 || production == 1) && gotNotPackagingChild)//tool.ifGotChild2(uData.part_code, dt_Join)
                {
                    uData.cust_id = row[dalItemCust.CustID].ToString();
                    uData.customer_name = row[dalItemCust.CustName].ToString();
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, uData.cust_id, uData.part_code, 1, 2, 3);
                    uData.forecast1 = forecastData.Item1;
                    uData.forecast2 = forecastData.Item2;
                    uData.forecast3 = forecastData.Item3;

                    var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, uData.customer_name);
                    uData.estimate = estimate.Item1;
                    bool NonActiveItem = estimate.Item2;

                    uData.deliveredOut = (float) estimate.Item3;

                    if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NonActiveItem && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                    {
                        uData.item_remark = row[dalItem.ItemRemark].ToString();
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        decimal SemenyihStock = deductSemenyihStock(uData.part_code);
                        //uData.ready_stock = uData.ready_stock - (float)SemenyihStock;

                        if (myconnstrng == text.DB_Semenyih)
                        {
                            uData.ready_stock = (float)SemenyihStock;
                        }

                        var result = GetProduceQty(uData.part_code, DT_MACHINE_SCHEDULE);
                        uData.toProduce = result.Item1;
                        uData.Produced = result.Item2;

                        #region Balance Calculation

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (uData.forecast1 == -1)
                        {
                            uData.outStd = 0;

                        }
                        else if (uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            uData.bal2 = uData.bal1 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }

                        uData.bal3 = uData.bal2 - uData.forecast3;

                        if (uData.forecast3 == -1)
                        {
                            uData.bal3 = uData.bal2 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast3 > -1)
                        {
                            uData.bal3 = uData.bal2 - uData.forecast3;
                        }

                        #endregion

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        if (uData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uData.toProduce;
                        }

                        if (uData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uData.Produced;
                        }

                        dt_Row[headerItemRemark] = uData.item_remark;
                        dt_Row[headerIndex] = uData.index;

                        if (dt_Data.Columns.Contains(text.Header_Customer))
                            dt_Row[text.Header_Customer] = uData.customer_name;

                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[text.Header_ColorMatCode] = row[dalItem.ItemMBatch].ToString();
                        dt_Row[headerPartWeight] = (uData.pw_per_shot / uData.cavity).ToString("0.##") + " (" + (uData.rw_per_shot / uData.cavity).ToString("0.##") + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;
                        dt_Row[headerBal3] = uData.bal3;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        if (assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = InsertMoldingMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
                        }

                        if (uData.part_code.Substring(0, 3) == text.Inspection_Pass)
                        {
                            dt_Row[headerParentColor] = InspectionMarking;
                        }


                        dt_Data.Rows.Add(dt_Data.NewRow());
                        dt_Data.Rows.Add(dt_Row);
                        index++;

                        LoadChild(dt_Data, uData, 0.1m);
                    }
                }
            }

            #endregion
            //2654ms -> 1320ms (16/3) -> 895ms (17/3)

            #endregion

            if (dt_Data.Rows.Count > 0)
            {
                dt_Data = CalAllCustomerRepeatedData(dt_Data);

                dt_Data = ItemSearch(dt_Data);

                dtMasterData = Sorting(dt_Data);

                if (cbIncludeProInfo.Checked)
                {
                    foreach (DataRow row in dtMasterData.Rows)
                    {
                        string itemCode = row[headerPartCode].ToString();

                        foreach (DataRow itemRow in DT_ITEM.Rows)
                        {
                            if (itemCode.Equals(itemRow[dalItem.ItemCode].ToString()))
                            {
                                row[headerQuoTon] = itemRow[dalItem.ItemQuoTon].ToString();
                                row[headerProTon] = itemRow[dalItem.ItemProTon].ToString();
                                row[headerCavity] = itemRow[dalItem.ItemCavity].ToString();
                                row[headerQuoCT] = itemRow[dalItem.ItemQuoCT].ToString();
                                row[headerProCT] = itemRow[dalItem.ItemProCTTo].ToString();
                                row[headerPWPerShot] = itemRow[dalItem.ItemProPWShot].ToString();
                                row[headerRWPerShot] = itemRow[dalItem.ItemProRWShot].ToString();

                                break;

                                //row[headerTotalOut] = uData.color_mat;
                                //row[headerMonthlyOut] = uData.color_mat;
                            }
                        }
                    }
                }

                if (cbShowProDayNeeded.Checked)
                {
                    DT_MACHINE_SCHEDULE_COPY = DT_MACHINE_SCHEDULE.Copy();

                    DT_MACHINE_SCHEDULE_COPY.DefaultView.Sort = dalPlanning.machineID + " ASC";
                    DT_MACHINE_SCHEDULE_COPY = DT_MACHINE_SCHEDULE_COPY.DefaultView.ToTable();

                    foreach (DataRow row in dtMasterData.Rows)
                    {
                        string itemCode = row[headerPartCode].ToString();
                        string balType = row[headerBalType].ToString();
                        float bal3 = float.TryParse(row[headerBal3].ToString(), out bal3) ? bal3 : 0;

                        string rawMat = row[headerRawMat].ToString();

                        if ((balType == balType_Total || balType == balType_Unique) && bal3 < 0 && !string.IsNullOrEmpty(rawMat))
                        {
                            foreach (DataRow itemRow in DT_ITEM.Rows)
                            {
                                if (itemCode.Equals(itemRow[dalItem.ItemCode].ToString()))
                                {

                                    int Cavity = int.TryParse(itemRow[dalItem.ItemCavity].ToString(), out Cavity) ? Cavity : 0;

                                    float proCT = float.TryParse(itemRow[dalItem.ItemProCTTo].ToString(), out proCT) ? proCT : 0;

                                    if (proCT <= 0)
                                    {
                                        proCT = float.TryParse(itemRow[dalItem.ItemQuoCT].ToString(), out proCT) ? proCT : 0;
                                    }


                                    if (proCT > 0 && Cavity > 0)
                                    {
                                        int totalShot = (int)Math.Ceiling(bal3 * -1 / Cavity);

                                        float totalSec = totalShot * proCT;

                                        //int totalDay = (int)Math.Truncate(totalSec / 3600 / 22);

                                        //calculate total hours need
                                        float totalHrs = Convert.ToSingle(totalSec) / 3600;

                                        int hoursPerDay = 22;

                                        int totalDay = 0;

                                        if (hoursPerDay != 0)
                                        {
                                            totalDay = Convert.ToInt32(Math.Truncate(totalHrs / Convert.ToSingle(hoursPerDay)));
                                        }

                                        string proDaysNeededInfo = "ERROR";

                                        if (totalDay > 0)
                                            proDaysNeededInfo = totalDay.ToString() + " Day (" + hoursPerDay + " hrs)";

                                        float balHrs = (totalHrs - Convert.ToSingle(totalDay * hoursPerDay));

                                        if (balHrs > 0)
                                        {
                                            if (totalDay > 0)
                                            {
                                                proDaysNeededInfo += " + ";
                                                proDaysNeededInfo += balHrs.ToString("0.##") + " hrs";
                                            }
                                            else
                                            {
                                                proDaysNeededInfo = balHrs.ToString("0.##") + " hrs";

                                            }

                                        }

                                        int totalProducedQty = totalShot * Cavity;

                                        proDaysNeededInfo += "\n" + " to Produce " + totalProducedQty + " pcs; ";

                                        string macHistory = "";

                                        if (cbIncludeMacRecord.Checked)
                                        {
                                            macHistory = GetMachineHistory(itemCode);

                                        }

                                        row[headerItemRemark] = proDaysNeededInfo + "\n" + macHistory;



                                    }
                                    else
                                    {
                                        string macHistory = "";

                                        if (cbIncludeMacRecord.Checked)
                                        {
                                            macHistory = GetMachineHistory(itemCode);

                                        }


                                        row[headerItemRemark] = "ERROR: CT=" + proCT + " , Cavity=" + Cavity + "\n" + macHistory;
                                    }

                                    break;

                                    //row[headerTotalOut] = uData.color_mat;
                                    //row[headerMonthlyOut] = uData.color_mat;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                dtMasterData = null;
            }



            btnFullReport.Enabled = true;
            btnSummary.Enabled = true;
            btnRefresh.Enabled = true;
            btnExcel.Enabled = true;
            btnExcelAll.Enabled = true;

            Cursor = Cursors.Arrow;

            return dtMasterData;

        }

    
        private DataTable SearchForecastData()
        {
            #region pre setting

            Cursor = Cursors.WaitCursor;

            lblForecastType.Text = ReportType_Full;

            btnFullReport.Enabled = false;
            btnSummary.Enabled = false;
            btnRefresh.Enabled = false;
            btnExcel.Enabled = false;
            btnExcelAll.Enabled = false;

            DataGridView dgv = dgvForecastReport;

            alertLevel = float.TryParse(txtAlertLevel.Text, out alertLevel) ? alertLevel : 0;
            dgvForecastReport.SuspendLayout();
            lblLastUpdated.Visible = true;
            lblUpdatedTime.Visible = true;
            //lblNote.Visible = true;

            lblUpdatedTime.Text = DateTime.Now.ToString();
            dgvForecastReport.DataSource = null;


            string customer = cmbCustomer.Text;
            DataTable dt_Data = NewForecastReportTable();

            #endregion

            if (!string.IsNullOrEmpty(customer))//29ms
            {
                DataRow dt_Row;

                DataTable dt_Item_Cust = dalItemCust.custSearch(customer);

                //filter out terminated item
                if (!cbIncludeTerminated.Checked)
                    dt_Item_Cust = RemoveTerminatedItem(dt_Item_Cust);

                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(customer).ToString());

                //DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerSearch(customer);
                
                //speed test 1 start ------------------------------------------------------------------------------------
                DateTime DateToday = DateTime.Now;

                int year = DateToday.Year - 1;

                DateTime LastYear = DateTime.Parse("1/1/" + year);

                string From = LastYear.ToString("yyyy/MM/dd");
                string To = DateToday.AddMonths(1).ToString("yyyy/MM/dd");

                DataTable dt_TrfHist = dalTrfHist.ItemDeliveredRecordSearch(customer, From, To);

                //DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerTransferDataOnlySearch(PMMA, from, to);

                DataTable DT_PMMA_DATE = dalPmmaDate.Select();//437ms

                //DataTable dt_Estimate = dalTrfHist.ItemToCustomerAllTimeSearch(cmbCustomer.Text);

                //dt_MacSchedule = dalPlanning.Select();

                //speed test 1 end-----------------------------------------------------------------------------------------132ms
                //DT_JOIN = dalJoin.SelectAll();
                //DT_ITEM = dalItem.Select();
                //dt_ProRecord = dalProRecord.SelectWithItemInfo();
                //1516ms

                int index = 1;

                dt_Item_Cust.DefaultView.Sort = "item_name ASC";

                dt_Item_Cust = dt_Item_Cust.DefaultView.ToTable();

                dt_Item_Cust.Columns.Add(text.Header_GotNotPackagingChild);
                //^^^502ms^^^

                #region load single part
                

                foreach (DataRow row in dt_Item_Cust.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    uData.item_remark = row[dalItem.ItemRemark].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);
                   
                    bool gotNotPackagingChild = tool.ifGotNotPackagingChild(uData.part_code, DT_JOIN, DT_ITEM);

                    row[text.Header_GotNotPackagingChild] = gotNotPackagingChild;


                    if(uData.part_code == "V51KM4100")
                    {
                        float checkpoint = 1;
                    }

                    if (!gotNotPackagingChild)//assembly == 0 && production == 0
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        decimal SemenyihStock = deductSemenyihStock(uData.part_code);
                        //uData.ready_stock = uData.ready_stock - (float)SemenyihStock;

                        if (myconnstrng == text.DB_Semenyih)
                        {
                            uData.ready_stock = (float)SemenyihStock;
                        }

                        var result = GetProduceQty(uData.part_code, DT_MACHINE_SCHEDULE);
                        uData.toProduce = result.Item1;
                        uData.Produced = result.Item2;
                    
                        var forecastData = GetThreeMonthsForecastQty(dt_ItemForecast, uData.part_code, 1, 2, 3);
                        uData.forecast1 = forecastData.Item1;
                        uData.forecast2 = forecastData.Item2;
                        uData.forecast3 = forecastData.Item3;

                        var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, customer);

                        uData.estimate = estimate.Item1;
                        bool NoDeliveredPast6Months = estimate.Item2;

                        if (GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, DT_PMMA_DATE) == 0)
                        {
                            uData.estimate = 0;
                        }

                        uData.deliveredOut = GetMaxOut(uData.part_code, customer, 0, dt_TrfHist, DT_PMMA_DATE);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;


                        if (uData.forecast1 == -1)
                        {
                            //uData.outStd = uData.estimate - uData.deliveredOut;

                            uData.outStd = 0;

                            //if (customer.Equals("PMMA"))
                            //{
                            //    uData.outStd = 0;
                            //}
                            //else
                            //{
                            //    uData.outStd = uData.estimate - uData.deliveredOut;
                            //}
                        }
                        else if (uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            uData.bal2 = uData.bal1 - (cbDeductEstimate.Checked ? uData.estimate : 0);
                          
                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }

                        uData.bal3 = uData.bal2 - uData.forecast3;

                        if (uData.forecast3 == -1)
                        {
                            uData.bal3 = uData.bal2 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast3 > -1)
                        {
                            uData.bal3 = uData.bal2 - uData.forecast3;
                        }



                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }


                        dt_Row = dt_Data.NewRow();

                        if (uData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uData.toProduce;
                        }

                        if (uData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uData.Produced;
                        }

                        dt_Row[headerItemRemark] = uData.item_remark;
                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[text.Header_ColorMatCode] = row[dalItem.ItemMBatch].ToString();
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;
                        dt_Row[headerBal3] = uData.bal3;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NoDeliveredPast6Months && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                        {
                            dt_Data.Rows.Add(dt_Row);
                            index++;
                        }

                    }
                   
                }

                #endregion
                //^^^755ms,793ms^^^

                #region load assembly part

                foreach (DataRow row in dt_Item_Cust.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();
                    uData.item_remark = row[dalItem.ItemRemark].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

                    //bool gotNotPackagingChild = tool.ifGotNotPackagingChild(uData.part_code, dt_Join, dt_Item);

                    //check if got child part also
                    if ((assembly == 1 || production == 1) && gotNotPackagingChild)//tool.ifGotChild2(uData.part_code, dt_Join)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        decimal SemenyihStock = deductSemenyihStock(uData.part_code);
                        //uData.ready_stock = uData.ready_stock - (float)SemenyihStock;

                        if (myconnstrng == text.DB_Semenyih)
                        {
                            uData.ready_stock = (float)SemenyihStock;
                        }

                        var result = GetProduceQty(uData.part_code, DT_MACHINE_SCHEDULE);
                        uData.toProduce = result.Item1;
                        uData.Produced = result.Item2;

                        var forecastData = GetThreeMonthsForecastQty(dt_ItemForecast, uData.part_code, 1, 2, 3);
                        uData.forecast1 = forecastData.Item1;
                        uData.forecast2 = forecastData.Item2;
                        uData.forecast3 = forecastData.Item3;

                        var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, customer);

                        uData.estimate = estimate.Item1;
                        bool NoDeliveredPast6Months = estimate.Item2;

                      

                        //uData.estimate = GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, DT_PMMA_DATE);
                        //uData.estimate = CalculateEstimateOrder(DT_PMMA_DATE, dt_TrfHist, uData.part_code, customer);

                      

                        if (GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, DT_PMMA_DATE) == 0)
                        {
                            uData.estimate = 0;
                        }

                        uData.deliveredOut = GetMaxOut(uData.part_code, customer, 0, dt_TrfHist, DT_PMMA_DATE);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (uData.forecast1 == -1)
                        {
                            uData.outStd = 0;

                        }
                        else if (uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            uData.bal2 = uData.bal1 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }

                        uData.bal3 = uData.bal2 - uData.forecast3;

                        if (uData.forecast3 == -1)
                        {
                            uData.bal3 = uData.bal2 - (cbDeductEstimate.Checked ? uData.estimate : 0);

                        }
                        else if (uData.forecast3 > -1)
                        {
                            uData.bal3 = uData.bal2 - uData.forecast3;
                        }

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        if (uData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uData.toProduce;
                        }

                        if (uData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uData.Produced;
                        }

                        dt_Row[headerItemRemark] = uData.item_remark;
                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[text.Header_ColorMatCode] = row[dalItem.ItemMBatch].ToString();
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;
                        dt_Row[headerBal3] = uData.bal3;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        if (assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = InsertMoldingMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
                        }

                        if (uData.part_code.Substring(0, 3) == text.Inspection_Pass)
                        {
                            dt_Row[headerParentColor] = InspectionMarking;
                        }


                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NoDeliveredPast6Months && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                        {
                            dt_Data.Rows.Add(dt_Data.NewRow());
                            dt_Data.Rows.Add(dt_Row);
                            index++;

                            if(index == 55)
                            {
                                var checkpoint = 1;
                            }

                            //load child
                            LoadChild(dt_Data, uData, 0.1m);
                        }



                    }
                }

                #endregion
                //2061ms,1295ms

            }

            if (dt_Data.Rows.Count > 0)
            {
                dt_Data = CalRepeatedData(dt_Data);

                dt_Data = ItemSearch(dt_Data);

                dtMasterData = Sorting(dt_Data);

                if (cbIncludeProInfo.Checked)
                {
                    foreach (DataRow row in dtMasterData.Rows)
                    {
                        string itemCode = row[headerPartCode].ToString();

                        foreach (DataRow itemRow in DT_ITEM.Rows)
                        {
                            if (itemCode.Equals(itemRow[dalItem.ItemCode].ToString()))
                            {
                                row[headerQuoTon] = itemRow[dalItem.ItemQuoTon].ToString();
                                row[headerProTon] = itemRow[dalItem.ItemProTon].ToString();
                                row[headerCavity] = itemRow[dalItem.ItemCavity].ToString();
                                row[headerQuoCT] = itemRow[dalItem.ItemQuoCT].ToString();
                                row[headerProCT] = itemRow[dalItem.ItemProCTTo].ToString();
                                row[headerPWPerShot] = itemRow[dalItem.ItemProPWShot].ToString();
                                row[headerRWPerShot] = itemRow[dalItem.ItemProRWShot].ToString();

                                break;

                                //row[headerTotalOut] = uData.color_mat;
                                //row[headerMonthlyOut] = uData.color_mat;
                            }
                        }
                    }
                }

                if (cbShowProDayNeeded.Checked)
                {
                    DT_MACHINE_SCHEDULE_COPY = DT_MACHINE_SCHEDULE.Copy();

                    DT_MACHINE_SCHEDULE_COPY.DefaultView.Sort = dalPlanning.machineID + " ASC";
                    DT_MACHINE_SCHEDULE_COPY = DT_MACHINE_SCHEDULE_COPY.DefaultView.ToTable();

                    foreach (DataRow row in dtMasterData.Rows)
                    {
                        string itemCode = row[headerPartCode].ToString();
                        string balType = row[headerBalType].ToString();
                        float bal3 = float.TryParse(row[headerBal3].ToString(), out bal3) ? bal3 : 0;

                        string rawMat = row[headerRawMat].ToString();

                        if ((balType == balType_Total || balType == balType_Unique) && bal3 < 0 && !string.IsNullOrEmpty(rawMat))
                        {
                            foreach (DataRow itemRow in DT_ITEM.Rows)
                            {
                                if (itemCode.Equals(itemRow[dalItem.ItemCode].ToString()))
                                {

                                    int Cavity = int.TryParse(itemRow[dalItem.ItemCavity].ToString(), out Cavity) ? Cavity : 0;

                                    float proCT = float.TryParse(itemRow[dalItem.ItemProCTTo].ToString(), out proCT) ? proCT : 0;

                                    if(proCT <= 0)
                                    {
                                        proCT = float.TryParse(itemRow[dalItem.ItemQuoCT].ToString(), out proCT) ? proCT : 0;
                                    }


                                    if(proCT > 0 && Cavity > 0)
                                    {
                                        int totalShot = (int) Math.Ceiling(bal3 * -1 / Cavity);

                                        float totalSec = totalShot * proCT;

                                        //int totalDay = (int)Math.Truncate(totalSec / 3600 / 22);

                                        //calculate total hours need
                                        float totalHrs = Convert.ToSingle(totalSec) / 3600;

                                        int hoursPerDay = 22;

                                        int totalDay = 0;

                                        if (hoursPerDay != 0)
                                        {
                                            totalDay = Convert.ToInt32(Math.Truncate(totalHrs / Convert.ToSingle(hoursPerDay)));
                                        }

                                        string proDaysNeededInfo = "ERROR";

                                        if (totalDay > 0)
                                        proDaysNeededInfo = totalDay.ToString() + " Day (" +hoursPerDay + " hrs)";

                                        float balHrs = (totalHrs - Convert.ToSingle(totalDay * hoursPerDay));

                                        if(balHrs > 0)
                                        {
                                            if (totalDay > 0)
                                            {
                                                proDaysNeededInfo += " + ";
                                                proDaysNeededInfo += balHrs.ToString("0.##") + " hrs";
                                            }
                                            else
                                            {
                                                proDaysNeededInfo = balHrs.ToString("0.##") + " hrs";

                                            }

                                        }

                                        int totalProducedQty = totalShot * Cavity;

                                        proDaysNeededInfo += "\n" + " to Produce " + totalProducedQty + " pcs; ";

                                        string macHistory = "";

                                        if(cbIncludeMacRecord.Checked)
                                        {
                                            macHistory = GetMachineHistory(itemCode);

                                        }

                                        row[headerItemRemark] = proDaysNeededInfo + "\n" + macHistory;



                                    }
                                    else
                                    {
                                        string macHistory = "";

                                        if (cbIncludeMacRecord.Checked)
                                        {
                                            macHistory = GetMachineHistory(itemCode);

                                        }


                                        row[headerItemRemark] = "ERROR: CT=" + proCT + " , Cavity=" + Cavity + "\n" + macHistory;
                                    }

                                    break;

                                    //row[headerTotalOut] = uData.color_mat;
                                    //row[headerMonthlyOut] = uData.color_mat;
                                }
                            }
                        }
                      
                    }
                }
            }
            else
            {
                dtMasterData = null;
            }

          
            
            btnFullReport.Enabled = true;
            btnSummary.Enabled = true;
            btnRefresh.Enabled = true;
            btnExcel.Enabled = true;
            btnExcelAll.Enabled = true;

            Cursor = Cursors.Arrow;

            return dtMasterData;

        }

        private DataTable Sorting(DataTable dt)
        {
            string keywords = cmbSoryBy.Text;
            DataTable dt_Copy;

            bool dataInserted = false;

            bool singleToParent = false;
            bool spaceAdded = false;

            if (cmbSoryBy.SelectedIndex == -1 || keywords.Equals("") || string.IsNullOrEmpty(keywords))
            {
                dt_Copy = dt.Copy();
            }
            else
            {
                dt_Copy = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataInserted = false;

                    double sortingTarget_1 = double.TryParse(dt.Rows[i][keywords].ToString(), out sortingTarget_1) ? sortingTarget_1 : 0;
                    string itemType_1 = dt.Rows[i][headerType].ToString();

                    string itemCode = dt.Rows[i][headerPartCode].ToString();

                    //if(itemCode.Equals("V96LAR000"))
                    //{
                    //    float test = 0;
                    //}
                    #region add divider seperate single item and group item

                    if (itemType_1.Equals(typeParent) && !spaceAdded)
                    {
                        singleToParent = true;
                    }

                    if (singleToParent && !spaceAdded)
                    {
                        //add space
                        dt_Copy.Rows.Add(dt_Copy.NewRow());
                        spaceAdded = true;
                    }

                    #endregion
                    
                    if (itemType_1.Equals(typeSingle) || itemType_1.Equals(typeParent))
                    {
                        for (int j = 0; j < dt_Copy.Rows.Count; j++)
                        {
                            string itemType_2 = dt_Copy.Rows[j][headerType].ToString();

                            if (itemType_1.Equals(itemType_2))
                            {
                                double sortingTarget_2 = double.TryParse(dt_Copy.Rows[j][keywords].ToString(), out sortingTarget_2) ? sortingTarget_2 : 0;

                                bool waitingToInsert = cbDescending.Checked ? sortingTarget_1 > sortingTarget_2 : sortingTarget_1 < sortingTarget_2;

                                //if (cbDescending.Checked)
                                //{
                                //    waitingToInsert = sortingTarget_1 > sortingTarget_2;
                                //}
                                //else
                                //{
                                //    waitingToInsert = sortingTarget_1 < sortingTarget_2;
                                //}

                                if(waitingToInsert)
                                {
                                    if (itemType_1.Equals(typeSingle))
                                    {
                                        //insert
                                        DataRow insertingRow = dt_Copy.NewRow();

                                        insertingRow.ItemArray = dt.Rows[i].ItemArray;

                                        dt_Copy.Rows.InsertAt(insertingRow, j);
                                        dataInserted = true;
                                        break;
                                    }

                                    else
                                    {
                                        //insert
                                        DataRow insertingRow = dt_Copy.NewRow();

                                        insertingRow.ItemArray = dt.Rows[i].ItemArray;

                                        dt_Copy.Rows.InsertAt(insertingRow, j);

                                        int tempIndex = j;

                                        for (int k = i + 1; k < dt.Rows.Count; k++)
                                        {
                                            tempIndex++;
                                            string _type = dt.Rows[k][headerType].ToString();

                                            if (_type.Equals(typeParent))
                                            {
                                                //dt_Copy.Rows.Add(dt_Copy.NewRow());
                                                break;
                                            }
                                            else
                                            {
                                                insertingRow = dt_Copy.NewRow();

                                                insertingRow.ItemArray = dt.Rows[k].ItemArray;

                                                dt_Copy.Rows.InsertAt(insertingRow, tempIndex);
                                            }
                                        }


                                        dataInserted = true;
                                        break;
                                    }
                                }
                               
                            }
                            

                          
                        }

                        if (!dataInserted)
                        {
                            //add data
                            if (itemType_1.Equals(typeSingle))
                            {
                                dt_Copy.ImportRow(dt.Rows[i]);
                            }

                            else if (itemType_1.Equals(typeParent))
                            {
                                dt_Copy.ImportRow(dt.Rows[i]);

                                for (int k = i + 1; k < dt.Rows.Count; k++)
                                {
                                    string _type = dt.Rows[k][headerType].ToString();

                                    if (_type.Equals(typeParent))
                                    {
                                        //dt_Copy.Rows.Add(dt_Copy.NewRow());
                                        break;
                                    }
                                    else
                                    {
                                        dt_Copy.ImportRow(dt.Rows[k]);

                                    }
                                }


                            }

                        }
                    }
                }
            }

            //remove last row if it is empty
            if (dt_Copy.Rows.Count > 0)
            {
                string itemCode = dt_Copy.Rows[dt_Copy.Rows.Count - 1][headerPartCode].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    dt_Copy.Rows.Remove(dt_Copy.Rows[dt_Copy.Rows.Count - 1]);
                }
                dt_Copy.AcceptChanges();
            }

            return dt_Copy;

        }

        private DataTable ItemSearch(DataTable dt)
        {
            string keywords = txtItemSearch.Text;
            DataTable dt_Copy;
            int parentIndex = -1;

            if (string.IsNullOrEmpty(keywords) || keywords.Equals("Search"))
            {
                dt_Copy = dt.Copy();
            }
            else
            {
                dt_Copy = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string itemName = dt.Rows[i][headerPartName].ToString();
                    string itemCode = dt.Rows[i][headerPartCode].ToString();
                    string itemType = dt.Rows[i][headerType].ToString();

                    bool itemMatch = itemName.Contains(keywords.ToUpper()) || itemCode.Contains(keywords.ToUpper());

                    if (itemMatch && dt.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (itemType.Equals(typeSingle))
                        {
                            dt_Copy.ImportRow(dt.Rows[i]);
                            //dt.Rows.Remove(dt.Rows[i]);
                            dt.Rows[i][headerType] = DBNull.Value;

                            dt_Copy.Rows.Add(dt_Copy.NewRow());
                        }
                        else if (itemType.Equals(typeParent))
                        {
                            parentIndex = i;

                            dt_Copy.ImportRow(dt.Rows[i]);
                            //dt.Rows.Remove(dt.Rows[i]);
                            dt.Rows[i][headerType] = DBNull.Value;

                            for (int j = i + 1; j < dt.Rows.Count; j++)
                            {
                                string _type = dt.Rows[j][headerType].ToString();

                                if (_type.Equals(typeParent))
                                {
                                    break;
                                }
                                else if (dt.Rows[j].RowState != DataRowState.Deleted)
                                {
                                    dt_Copy.ImportRow(dt.Rows[j]);
                                    //dt.Rows.Remove(dt.Rows[j]);
                                    dt.Rows[j][headerType] = DBNull.Value;
                                }
                            }
                        }

                        else if (itemType.Equals(typeChild))
                        {
                            for (int j = i; j >= 0; j--)
                            {
                                string _type = dt.Rows[j][headerType].ToString();

                                if (_type.Equals(typeParent))
                                {
                                    parentIndex = j;
                                    break;
                                }
                            }

                            if (parentIndex >= 0)
                            {
                                dt_Copy.ImportRow(dt.Rows[parentIndex]);
                                //dt.Rows.Remove(dt.Rows[parentIndex]);
                                dt.Rows[parentIndex][headerType] = DBNull.Value;
                                for (int k = parentIndex + 1; k < dt.Rows.Count; k++)
                                {
                                    string _type = dt.Rows[k][headerType].ToString();

                                    if (_type.Equals(typeParent))
                                    {
                                        break;
                                    }
                                    else if (dt.Rows[k].RowState != DataRowState.Deleted)
                                    {
                                        dt_Copy.ImportRow(dt.Rows[k]);
                                        //dt.Rows.Remove(dt.Rows[k]);
                                        dt.Rows[k][headerType] = DBNull.Value;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Parent not found. (ItemSearch method error)");
                            }

                        }
                    }
                }
            } 

            if (dt_Copy.Rows.Count > 0)
            {
                string itemCode = dt_Copy.Rows[dt_Copy.Rows.Count - 1][headerPartCode].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    dt_Copy.Rows.Remove(dt_Copy.Rows[dt_Copy.Rows.Count - 1]);
                }
                dt_Copy.AcceptChanges();
            }

            return dt_Copy;

        }

        private DataTable RepeatedRowReferenceRemark(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                string firstItem = dt.Rows[i][headerPartCode].ToString();
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                string rowReference = dt.Rows[i][headerRowReference].ToString();

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();

                        if (firstItem.Equals(nextItem))
                        {
                            dt.Rows[j][headerRowReference] = rowReference;
                        }
                    }
                }
            }

            return dt;


        }

        private DataTable CalAllCustomerRepeatedData(DataTable dt)
        {
            double totalNeeded1 = 0;
            double totalNeeded2 = 0;
            double totalNeeded3 = 0;

            bool colorChange = false;
            int colorOrder = 0;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                totalNeeded1 = 0;
                totalNeeded2 = 0;
                totalNeeded3 = 0;
                string rowReference = "(" + dt.Rows[i][headerIndex].ToString() + ")";
                int repeatedCount = 1;

                //string firstItemCustomer = "Single Customer Mode";

                //if (dt.Columns.Contains(text.Header_Customer))
                //{
                //    firstItemCustomer = dt.Rows[i][text.Header_Customer].ToString();
                //}
                    
                string firstItem = dt.Rows[i][headerPartCode].ToString();

                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                double firstBal3 = double.TryParse(dt.Rows[i][headerBal3].ToString(), out firstBal3) ? firstBal3 : -0.001;

                //string firstParentColor = dt.Rows[i][headerParentColor].ToString();

                string type_FirstItem = dt.Rows[i][headerType].ToString();

                if (!(type_FirstItem.Equals(typeSingle) || type_FirstItem.Equals(typeParent)))
                {
                    dt.Rows[i][headerForecastType] = forecastType_Needed;
                }
                else
                {
                    dt.Rows[i][headerForecastType] = forecastType_Forecast;
                }

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        //string nextItemCustomer = dt.Rows[j][text.Header_Customer].ToString();
                        string nextItem = dt.Rows[j][headerPartCode].ToString();
                        //string index = dt.Rows[j][headerIndex].ToString();



                        if (nextItem.Equals(firstItem))
                        {
                            repeatedCount++;

                            rowReference += " (" + dt.Rows[j][headerIndex].ToString() + ")";

                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerForecast1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerForecast2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerForecast3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                            double nextOutQty = double.TryParse(dt.Rows[j][headerOut].ToString(), out nextOutQty) ? nextOutQty : 0;
                            double nextOutStanding = double.TryParse(dt.Rows[j][headerOutStd].ToString(), out nextOutStanding) ? nextOutStanding : 0;
                            double nextEstimate = double.TryParse(dt.Rows[j][headerEstimate].ToString(), out nextEstimate) ? nextEstimate : 0;


                            nextOutStanding = nextOutStanding > 0 ? nextOutStanding : 0;
                            nextNeededQty1 = nextNeededQty1 > 0 ? nextNeededQty1 : 0;
                            nextNeededQty2 = nextNeededQty2 > 0 ? nextNeededQty2 : 0;
                            nextNeededQty3 = nextNeededQty3 > 0 ? nextNeededQty3 : 0;

                            if(nextOutStanding > 0 || nextOutQty >= nextNeededQty1)
                                nextNeededQty1 = nextOutStanding;

                            if(nextEstimate > 0)
                            {
                                nextNeededQty2 = nextNeededQty2 > 0 ? nextNeededQty2 : nextEstimate;
                                nextNeededQty3 = nextNeededQty3 > 0 ? nextNeededQty3 : nextEstimate;
                            }

                            string type_NextItem = dt.Rows[j][headerType].ToString();

                            if (!type_NextItem.Equals(typeParent))
                            {
                                dt.Rows[j][headerBal1] = DBNull.Value;
                                dt.Rows[j][headerBal2] = DBNull.Value;
                                dt.Rows[j][headerBal3] = DBNull.Value;
                            }
                            else
                            {
                                double nextItem_Stock = double.TryParse(dt.Rows[j][headerReadyStock].ToString(), out nextItem_Stock) ? nextItem_Stock : 0;

                                nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                                nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                                nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;


                                nextNeededQty3 -= nextNeededQty2;
                                nextNeededQty2 -= nextNeededQty1;
                                nextNeededQty1 -= nextItem_Stock;

                                nextNeededQty1 = nextNeededQty1 < 0 ? nextNeededQty1 * -1 : 0;
                                nextNeededQty2 = nextNeededQty2 < 0 ? nextNeededQty2 * -1 : 0;
                                nextNeededQty3 = nextNeededQty3 < 0 ? nextNeededQty3 * -1 : 0;

                            }



                            totalNeeded1 += nextNeededQty1;
                            totalNeeded2 += nextNeededQty2;
                            totalNeeded3 += nextNeededQty3;

                            dt.Rows[i][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBalType] = balType_Repeated;
                            colorChange = true;
                        }
                    }



                    if (colorChange)
                    {
                        if (totalNeeded1 < 0)
                        {
                            totalNeeded1 = 0;
                        }

                        if (totalNeeded2 < 0)
                        {
                            totalNeeded2 = 0;
                        }

                        if (totalNeeded3 < 0)
                        {
                            totalNeeded3 = 0;
                        }

                        dt.Rows[i][headerBal1] = firstBal1 - totalNeeded1;
                        dt.Rows[i][headerBal2] = firstBal2 - totalNeeded1 - totalNeeded2;
                        dt.Rows[i][headerBal3] = firstBal3 - totalNeeded1 - totalNeeded2 - totalNeeded3;

                        dt.Rows[i][headerBalType] = balType_Total;
                        dt.Rows[i][headerRowReference] = rowReference;
                        colorOrder++;

                        if (colorOrder > Enum.GetValues(typeof(ColorSet)).Cast<int>().Max())
                        {
                            colorOrder = 0;

                        }
                        colorChange = false;
                    }



                }
            }

            dt = RepeatedRowReferenceRemark(dt);

            return AllCustomerChildBalChecking(dt);


        }

        private DataTable AllCustomerChildBalChecking(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                double forecast1 = double.TryParse(dt.Rows[i][headerForecast1].ToString(), out forecast1) ? forecast1 : 0;
                double forecast2 = double.TryParse(dt.Rows[i][headerForecast2].ToString(), out forecast2) ? forecast2 : 0;
                double forecast3 = double.TryParse(dt.Rows[i][headerForecast3].ToString(), out forecast3) ? forecast3 : 0;

                if (forecast1 < 0)
                {
                    dt.Rows[i][headerForecast1] = DBNull.Value;
                }

                if (forecast2 < 0)
                {
                    dt.Rows[i][headerForecast2] = DBNull.Value;
                }

                if (forecast3 < 0)
                {
                    dt.Rows[i][headerForecast3] = DBNull.Value;
                }

                double totalNeeded1 = 0;
                double totalNeeded2 = 0;
                double totalNeeded3 = 0;

                string itemCode = dt.Rows[i][headerPartCode].ToString();
                string itemType = dt.Rows[i][headerType].ToString();
                string balType = dt.Rows[i][headerBalType].ToString();
                string ForecastType = dt.Rows[i][headerForecastType].ToString();
                double itemStock = double.TryParse(dt.Rows[i][headerReadyStock].ToString(), out itemStock) ? itemStock : 0;

                //loop and find "TOTAL"  and not parent item
                if (itemType != typeParent && !ForecastType.Equals(forecastType_Forecast) && balType.Equals(balType_Total))
                {
                    bool itemBalToChange = false;

                    //load Joint Data Table and find Parent(s)
                    foreach (DataRow row in DT_JOIN.Rows)
                    {
                        string childCode = row[dalJoin.JoinChild].ToString();

                        if (childCode.Equals(itemCode))
                        {

                            if (childCode == "A41K150K0")
                            {
                                var checkpoint = 0;
                            }

                            string parentCode = row[dalJoin.JoinParent].ToString();

                            float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float f) ? f : 1;

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string seachingItemCode = dt.Rows[j][headerPartCode].ToString();
                                string seachingbalType = dt.Rows[j][headerBalType].ToString();
                                string seachingItemType = dt.Rows[j][headerType].ToString();


                                //Find Parent Item With "Total"
                                if (seachingItemCode.Equals(parentCode) && (seachingItemType.Equals(typeParent) || ((seachingbalType.Equals(balType_Total) || seachingbalType.Equals(balType_Unique)) && seachingItemType.Equals(typeChild))))
                                {
                                    double nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                                    double nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                                    double nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                                    //Sum Up balance, and calculate total needed for Child (if balance>=0, needed for child =0; if balance <0, needed for child = bal*-1)
                                    totalNeeded1 += nextNeededQty1 < 0 ? nextNeededQty1 * -1 * joinQty : 0;
                                    totalNeeded2 += nextNeededQty2 < 0 ? nextNeededQty2 * -1 * joinQty : 0;
                                    totalNeeded3 += nextNeededQty3 < 0 ? nextNeededQty3 * -1 * joinQty : 0;

                                    itemBalToChange = true;
                                    break;
                                }

                            }


                        }
                    }

                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string seachingItemCode = dt.Rows[j][headerPartCode].ToString();
                        string seachingbalType = dt.Rows[j][headerBalType].ToString();
                        string seachingItemType = dt.Rows[j][headerType].ToString();

                        //Find Parent Item With "Total"
                        if (seachingItemCode.Equals(itemCode) && seachingItemType.Equals(typeParent) && !seachingbalType.Equals(balType_Total))
                        {
                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                            //Sum Up balance, and calculate total needed for Child (if balance>=0, needed for child =0; if balance <0, needed for child = bal*-1)
                            totalNeeded1 += nextNeededQty1 < 0 ? nextNeededQty1 * -1 : 0;
                            totalNeeded2 += nextNeededQty2 < 0 ? nextNeededQty2 * -1 : 0;
                            totalNeeded3 += nextNeededQty3 < 0 ? nextNeededQty3 * -1 : 0;

                            itemBalToChange = true;
                        }

                    }

                    if (itemBalToChange)
                    {
                        if (totalNeeded1 % 1 > 0)
                        {
                            totalNeeded1 = (float)Math.Round(totalNeeded1 * 100f) / 100f;
                        }

                        if (totalNeeded2 % 1 > 0)
                        {
                            totalNeeded2 = (float)Math.Round(totalNeeded2 * 100f) / 100f;

                        }

                        if (totalNeeded3 % 1 > 0)
                        {
                            totalNeeded3 = (float)Math.Round(totalNeeded3 * 100f) / 100f;

                        }


                        dt.Rows[i][headerBal1] = itemStock - totalNeeded1;
                        dt.Rows[i][headerBal2] = itemStock - totalNeeded2;
                        dt.Rows[i][headerBal3] = itemStock - totalNeeded3;
                    }

                }




            }

            return dt;
        }

        private DataTable CalRepeatedData(DataTable dt)
        {
            double totalNeeded1 = 0;
            double totalNeeded2 = 0;
            double totalNeeded3 = 0;

            bool colorChange = false;
            int colorOrder = 0;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                totalNeeded1 = 0;
                totalNeeded2 = 0;
                totalNeeded3 = 0;
                string rowReference = "(" + dt.Rows[i][headerIndex].ToString() + ")";
                int repeatedCount = 1;

                string firstItem = dt.Rows[i][headerPartCode].ToString();
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                double firstBal3 = double.TryParse(dt.Rows[i][headerBal3].ToString(), out firstBal3) ? firstBal3 : -0.001;

                string firstParentColor = dt.Rows[i][headerParentColor].ToString();

                string type_FirstItem = dt.Rows[i][headerType].ToString();

                if (!(type_FirstItem.Equals(typeSingle) || type_FirstItem.Equals(typeParent)))
                {
                    dt.Rows[i][headerForecastType] = forecastType_Needed;
                }
                else
                {
                    dt.Rows[i][headerForecastType] = forecastType_Forecast;
                }

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();
                        string index = dt.Rows[j][headerIndex].ToString();

                       

                        if (nextItem.Equals(firstItem))
                        {
                            if (index == "54")
                            {
                                var checkpoint = 1;
                            }

                            repeatedCount++;

                            rowReference += " (" + dt.Rows[j][headerIndex].ToString() +")";

                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerForecast1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerForecast2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerForecast3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                            nextNeededQty1 = nextNeededQty1 > 0 ? nextNeededQty1 : 0;
                            nextNeededQty2 = nextNeededQty2 > 0 ? nextNeededQty2 : 0;
                            nextNeededQty3 = nextNeededQty3 > 0 ? nextNeededQty3 : 0;

                            string type_NextItem = dt.Rows[j][headerType].ToString();

                            if (!type_NextItem.Equals(typeParent))
                            {
                                dt.Rows[j][headerBal1] = DBNull.Value;
                                dt.Rows[j][headerBal2] = DBNull.Value;
                                dt.Rows[j][headerBal3] = DBNull.Value;
                            }
                            else
                            {
                                double nextItem_Stock = double.TryParse(dt.Rows[j][headerReadyStock].ToString(), out nextItem_Stock) ? nextItem_Stock : 0;

                                nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                                nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                                nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;


                                nextNeededQty3 -= nextNeededQty2;
                                nextNeededQty2 -= nextNeededQty1;
                                nextNeededQty1 -= nextItem_Stock;

                                nextNeededQty1 = nextNeededQty1 < 0 ? nextNeededQty1 * -1 : 0;
                                nextNeededQty2 = nextNeededQty2 < 0 ? nextNeededQty2 * -1 : 0;
                                nextNeededQty3 = nextNeededQty3 < 0 ? nextNeededQty3 * -1 : 0;

                            }

                           

                            totalNeeded1 += nextNeededQty1;
                            totalNeeded2 += nextNeededQty2;
                            totalNeeded3 += nextNeededQty3;

                            dt.Rows[i][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBalType] = balType_Repeated;
                            colorChange = true;
                        }
                    }



                    if (colorChange)
                    {
                        if(totalNeeded1 < 0)
                        {
                            totalNeeded1 = 0;
                        }

                        if (totalNeeded2 < 0)
                        {
                            totalNeeded2 = 0;
                        }

                        if (totalNeeded3 < 0)
                        {
                            totalNeeded3 = 0;
                        }

                        dt.Rows[i][headerBal1] = firstBal1 - totalNeeded1;
                        dt.Rows[i][headerBal2] = firstBal2 - totalNeeded1 - totalNeeded2;
                        dt.Rows[i][headerBal3] = firstBal3 - totalNeeded1 - totalNeeded2 - totalNeeded3;

                        dt.Rows[i][headerBalType] = balType_Total;
                        dt.Rows[i][headerRowReference] = rowReference;
                        colorOrder++;

                        if (colorOrder > Enum.GetValues(typeof(ColorSet)).Cast<int>().Max())
                        {
                            colorOrder = 0;

                        }
                        colorChange = false;
                    }



                }
            }

            dt = RepeatedRowReferenceRemark(dt);

            return ChildBalChecking(dt);


        }

        private DataTable ChildBalChecking(DataTable dt)
        {
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                double forecast1 = double.TryParse(dt.Rows[i][headerForecast1].ToString(), out forecast1) ? forecast1 : 0;
                double forecast2 = double.TryParse(dt.Rows[i][headerForecast2].ToString(), out forecast2) ? forecast2 : 0;
                double forecast3 = double.TryParse(dt.Rows[i][headerForecast3].ToString(), out forecast3) ? forecast3 : 0;

                if(forecast1 < 0)
                {
                    dt.Rows[i][headerForecast1] = DBNull.Value;
                }

                if (forecast2 < 0)
                {
                    dt.Rows[i][headerForecast2] = DBNull.Value;
                }

                if (forecast3 < 0)
                {
                    dt.Rows[i][headerForecast3] = DBNull.Value;
                }

                double totalNeeded1 = 0;
                double totalNeeded2 = 0;
                double totalNeeded3 = 0;

                string itemCode = dt.Rows[i][headerPartCode].ToString();
                string itemType = dt.Rows[i][headerType].ToString();
                string balType = dt.Rows[i][headerBalType].ToString();
                string ForecastType = dt.Rows[i][headerForecastType].ToString();
                double itemStock = double.TryParse(dt.Rows[i][headerReadyStock].ToString(), out itemStock) ? itemStock : 0;

                //loop and find "TOTAL"  and not parent item
                if (itemType != typeParent &&  !ForecastType.Equals(forecastType_Forecast) && balType.Equals(balType_Total))
                {
                    bool itemBalToChange = false;

                    //load Joint Data Table and find Parent(s)
                    foreach (DataRow row in DT_JOIN.Rows)
                    {
                        string childCode = row[dalJoin.JoinChild].ToString();

                        if(childCode.Equals(itemCode))
                        {

                           if(childCode == "C84KXQ100")
                            {
                                var checkpoint = 0;
                            }

                            string parentCode = row[dalJoin.JoinParent].ToString();

                            float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out  float f) ? f : 1;

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string seachingItemCode = dt.Rows[j][headerPartCode].ToString();
                                string seachingbalType = dt.Rows[j][headerBalType].ToString();
                                string seachingItemType = dt.Rows[j][headerType].ToString();


                                //Find Parent Item With "Total"
                                if (seachingItemCode.Equals(parentCode) && (seachingItemType.Equals(typeParent) || ((seachingbalType.Equals(balType_Total) || seachingbalType.Equals(balType_Unique)) && seachingItemType.Equals(typeChild))))
                                {
                                    double nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                                    double nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                                    double nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                                    //Sum Up balance, and calculate total needed for Child (if balance>=0, needed for child =0; if balance <0, needed for child = bal*-1)
                                    totalNeeded1 += nextNeededQty1 < 0 ? nextNeededQty1 * -1 * joinQty : 0;
                                    totalNeeded2 += nextNeededQty2 < 0 ? nextNeededQty2 * -1 * joinQty : 0;
                                    totalNeeded3 += nextNeededQty3 < 0 ? nextNeededQty3 * -1 * joinQty : 0;

                                    itemBalToChange = true;
                                    break;
                                }

                            }


                        }
                    }

                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string seachingItemCode = dt.Rows[j][headerPartCode].ToString();
                        string seachingbalType = dt.Rows[j][headerBalType].ToString();
                        string seachingItemType = dt.Rows[j][headerType].ToString();

                        //Find Parent Item With "Total"
                        if (seachingItemCode.Equals(itemCode) && seachingItemType.Equals(typeParent) && !seachingbalType.Equals(balType_Total))
                        {
                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                            //Sum Up balance, and calculate total needed for Child (if balance>=0, needed for child =0; if balance <0, needed for child = bal*-1)
                            totalNeeded1 += nextNeededQty1 < 0 ? nextNeededQty1 * -1  : 0;
                            totalNeeded2 += nextNeededQty2 < 0 ? nextNeededQty2 * -1  : 0;
                            totalNeeded3 += nextNeededQty3 < 0 ? nextNeededQty3 * -1  : 0;

                            itemBalToChange = true;
                        }

                    }

                    if (itemBalToChange)
                    {
                        if(totalNeeded1 % 1 > 0)
                        {
                            totalNeeded1 = (float)Math.Round(totalNeeded1 * 100f) / 100f;
                        }

                        if (totalNeeded2 % 1 > 0)
                        {
                            totalNeeded2 = (float)Math.Round(totalNeeded2 * 100f) / 100f;

                        }

                        if (totalNeeded3 % 1 > 0)
                        {
                            totalNeeded3 = (float)Math.Round(totalNeeded3 * 100f) / 100f;

                        }


                        dt.Rows[i][headerBal1] = itemStock - totalNeeded1;
                        dt.Rows[i][headerBal2] = itemStock - totalNeeded2;
                        dt.Rows[i][headerBal3] = itemStock - totalNeeded3;
                    }
                    
                }




            }

            return dt;
        }

        private void LoadChild(DataTable dt_Data, dataTrfBLL uParentData, decimal subIndex)
        {
            DataRow dt_Row;
            dataTrfBLL uChildData = new dataTrfBLL();

            decimal index = (decimal) uParentData.index + subIndex;

            foreach (DataRow row in DT_JOIN.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if (parentCode.Equals(uParentData.part_code))
                {
                    string childCode = row[dalJoin.JoinChild].ToString();

                    if(childCode == "V01CAR000")
                    {
                        var checkpoint = 1;
                    }

                    DataRow row_Item = tool.getDataRowFromDataTable(DT_ITEM, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part) || row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_SubMat);

                    if (itemMatch)
                    {
                        float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float i) ? Convert.ToSingle(row[dalJoin.JoinQty].ToString()) : 1;

                        dt_Row = dt_Data.NewRow();

                        uChildData.part_code = row_Item[dalItem.ItemCode].ToString();

                        uChildData.index = (double)index;
                        uChildData.item_remark = row_Item[dalItem.ItemRemark].ToString();
                        uChildData.part_name = row_Item[dalItem.ItemName].ToString();
                        uChildData.color_mat = row_Item[dalItem.ItemMBatch].ToString();
                        uChildData.color = row_Item[dalItem.ItemColor].ToString();
                        uChildData.raw_mat = row_Item[dalItem.ItemMaterial].ToString();
                        uChildData.pw_per_shot = row_Item[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProPWShot]);
                        uChildData.rw_per_shot = row_Item[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProRWShot]);
                        uChildData.cavity = row_Item[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row_Item[dalItem.ItemCavity]);
                        uChildData.cavity = uChildData.cavity == 0 ? 1 : uChildData.cavity;
                        uChildData.ready_stock = row_Item[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemStock]);

                        decimal SemenyihStock = deductSemenyihStock(childCode);
                        //uChildData.ready_stock = uChildData.ready_stock - (float)SemenyihStock;

                        float stock = uChildData.ready_stock;

                        if (stock % 1 > 0)
                        {
                            stock = (float)Math.Round(stock * 100f) / 100f;

                            uChildData.ready_stock = stock;
                        }


                        if (uParentData.bal1 >= 0)
                        {
                            uChildData.forecast1 = 0;
                        }
                        else
                        {
                            uChildData.forecast1 = uParentData.bal1 * -1 * joinQty;
                        }

                        if (uParentData.bal2 >= 0)
                        {
                            uChildData.forecast2 = 0;

                            if (uParentData.forecast3 <= -1)
                            {
                                if (uParentData.bal2 - uParentData.estimate < 0)
                                {
                                    uChildData.forecast3 = (uParentData.bal2 - uParentData.estimate) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = 0;
                                }
                            }
                            else
                            {
                                if (uParentData.bal2 - uParentData.forecast3 < 0)
                                {
                                    uChildData.forecast3 = (uParentData.bal2 - uParentData.forecast3) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = 0;
                                }
                            }
                        }
                        else
                        {
                            if (uParentData.bal1 < 0)
                            {
                                if (uParentData.forecast2 <= -1)
                                {
                                    uChildData.forecast2 = uParentData.estimate * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast2 = uParentData.forecast2 * joinQty;
                                }
                               
                            }
                            else
                            {
                                uChildData.forecast2 = uParentData.bal2 * -1 * joinQty;
                            }
                        }

                        if (uParentData.bal3 >= 0)
                        {
                            uChildData.forecast3 = 0;
                        }
                        else
                        {
                            if (uParentData.bal2 < 0)
                            {
                                if (uParentData.forecast3 <= -1)
                                {
                                    uChildData.forecast3 = uParentData.estimate * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = uParentData.forecast3 * joinQty;
                                }

                            }
                            else
                            {
                                uChildData.forecast3 = uParentData.bal3 * -1 * joinQty;
                            }
                        }

                        uChildData.forecast1 = uChildData.forecast1 < 0 ? uChildData.forecast1 * -1 : uChildData.forecast1;
                        uChildData.forecast2 = uChildData.forecast2 < 0 ? uChildData.forecast2 * -1 : uChildData.forecast2;
                        uChildData.forecast3 = uChildData.forecast3 < 0 ? uChildData.forecast3 * -1 : uChildData.forecast3;

                        uChildData.bal1 = uChildData.ready_stock - uChildData.forecast1;

                        uChildData.bal2 = uChildData.bal1 - uChildData.forecast2;

                        uChildData.bal3 = uChildData.bal2 - uChildData.forecast3;


                        if (!uChildData.color.Equals(uChildData.color_mat) && !string.IsNullOrEmpty(uChildData.color_mat))
                        {
                            uChildData.color_mat += " (" + uChildData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        var result = GetProduceQty(uChildData.part_code, DT_MACHINE_SCHEDULE);
                        uChildData.toProduce = result.Item1;
                        uChildData.Produced = result.Item2;

                        if (uChildData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uChildData.toProduce;
                        }

                        if (uChildData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uChildData.Produced;
                        }

                        dt_Row[headerItemRemark] = uChildData.item_remark;

                        dt_Row[headerIndex] = uChildData.index;
                        dt_Row[headerType] = typeChild;
                        dt_Row[headerRawMat] = uChildData.raw_mat;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row_Item[dalItem.ItemCat].ToString();
                        dt_Row[headerPartCode] = uChildData.part_code;
                        dt_Row[headerPartName] = CountZeros(subIndex.ToString()) + uChildData.part_name;
                        dt_Row[headerColorMat] = uChildData.color_mat;
                        dt_Row[text.Header_ColorMatCode] = row_Item[dalItem.ItemMBatch].ToString();
                        dt_Row[headerPartWeight] = (uChildData.pw_per_shot / uChildData.cavity).ToString("0.##") + " (" + (uChildData.rw_per_shot / uChildData.cavity).ToString("0.##") + ")";


                        dt_Row[headerReadyStock] = uChildData.ready_stock;
                      
                        dt_Row[headerForecast1] = uChildData.forecast1 % 1 > 0 ? (float)Math.Round(uChildData.forecast1 * 100f) / 100f : uChildData.forecast1;
                        dt_Row[headerForecast2] = uChildData.forecast2 % 1 > 0 ? (float)Math.Round(uChildData.forecast2 * 100f) / 100f : uChildData.forecast2;
                        dt_Row[headerForecast3] = uChildData.forecast3 % 1 > 0 ? (float)Math.Round(uChildData.forecast3 * 100f) / 100f : uChildData.forecast3;

                        dt_Row[headerBal1] = uChildData.bal1;
                        dt_Row[headerBal2] = uChildData.bal2;
                        dt_Row[headerBal3] = uChildData.bal3;

                        int assembly = row_Item[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemAssemblyCheck]);
                        int production = row_Item[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemProductionCheck]);

                        bool gotChild = tool.ifGotNotPackagingChild(uChildData.part_code, DT_JOIN, DT_ITEM);

                        if (assembly == 1 && production == 0 && gotChild)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = InsertMoldingMarking;
                        }
                        else if (assembly == 1 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
                        }

                        dt_Data.Rows.Add(dt_Row);

                        index += subIndex;

                        //check if got child part also
                        if (gotChild)
                        {
                            LoadChild(dt_Data, uChildData, subIndex / 10);
                        }
                    }

                }

            }
        }

        private string CountZeros(string input)
        {
            int count = 0;

            foreach (char c in input)
            {
                if (c == '0')
                {
                    count++;
                }
            }

            if(count >= 3)
            {
                var checkoint = 1;
            }

            return count > 0 ? new string('-', count) + "> " : "";
        }


        private Tuple<float,float,float> GetThreeMonthsForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum_1, int forecastNum_2, int forecastNum_3)
        {
            string monthString = cmbForecastFrom.Text;

            int month_1 = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year_1 = DateTime.Now.Year;

            month_1 += forecastNum_1 - 1;

            if (month_1 > 12)
            {
                month_1 -= 12;
                year_1++;
            }

            int month_2 = month_1 + 1;
            int year_2 = year_1;

            if (month_2 > 12)
            {
                month_2 -= 12;
                year_2++;
            }

            int month_3 = month_2 + 1;
            int year_3 = year_2;

            if (month_3 > 12)
            {
                month_3 -= 12;
                year_3++;
            }

            var forecastData = tool.getItemForecast(dt_ItemForecast, itemCode,  year_1,  month_1,  year_2,  month_2,  year_3,  month_3);

            return Tuple.Create(forecastData.Item1, forecastData.Item2, forecastData.Item3);
        }

        private Tuple<float, float, float> GetCustomerThreeMonthsForecastQty(DataTable dt_ItemForecast, string Customer,  string itemCode, int forecastNum_1, int forecastNum_2, int forecastNum_3)
        {
            string monthString = cmbForecastFrom.Text;

            int month_1 = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year_1 = DateTime.Now.Year;

            month_1 += forecastNum_1 - 1;

            if (month_1 > 12)
            {
                month_1 -= 12;
                year_1++;
            }

            int month_2 = month_1 + 1;
            int year_2 = year_1;

            if (month_2 > 12)
            {
                month_2 -= 12;
                year_2++;
            }

            int month_3 = month_2 + 1;
            int year_3 = year_2;

            if (month_3 > 12)
            {
                month_3 -= 12;
                year_3++;
            }

            var forecastData = tool.getCustomerItemForecast(dt_ItemForecast, Customer, itemCode, year_1, month_1, year_2, month_2, year_3, month_3);

            return Tuple.Create(forecastData.Item1, forecastData.Item2, forecastData.Item3);
        }
        private float GetForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum)
        {
            string monthString = cmbForecastFrom.Text;

            int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year = DateTime.Now.Year;

            //if (month > DateTime.Now.Month)
            //{
            //    year = DateTime.Now.Year - 1;
            //}
          
            month += forecastNum - 1;

            if (month > 12)
            {
                month -= 12;
                year++;

            }
            return tool.getItemForecast(dt_ItemForecast, itemCode, year, month);
        }

        private float GetMaxOut(string itemCode, string customer, int pastMonthQty, DataTable dt_TrfHist, DataTable DT_PMMA_DATE)
        {
            float MaxOut = 0, tmp = 0;
            bool includeCurrentMonth = false;

            if (uData.part_code == "R 120 141 020 38")
            {
                var checkpoint = 1;
            }

            if (customer.Equals("PMMA"))
            {
                if (pastMonthQty <= 0)
                {
                    DateTime start = dtpPMMAOutFrom.Value;
                    DateTime end = dtpPMMAOutTo.Value;

                    foreach (DataRow row in dt_TrfHist.Rows)
                    {
                        string DB_Customer = row[dalTrfHist.TrfTo].ToString();

                        string item = row[dalTrfHist.TrfItemCode].ToString();

                        DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                        if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode) && DB_Customer.Equals(customer))
                        {
                            
                            //string cust = row[dalTrfHist.TrfTo].ToString();


                            if (trfDate >= start && trfDate <= end)
                            {
                                MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                            }
                        }
                        else if (myconnstrng == text.DB_Semenyih && row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                        {
                            if(DB_Customer.Equals("OUG") || DB_Customer.Equals("SP OUG") || DB_Customer.Equals("DAIKIN") || DB_Customer.Equals("PYROCELL") || DB_Customer.Equals("PMMA"))
                            {
                                if (trfDate >= start && trfDate <= end)
                                {
                                    MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }


                    }
                }
                else
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    if (DateTime.Today > tool.GetPMMAEndDate(currentMonth, currentYear))
                    {
                        includeCurrentMonth = true;
                    }

                    for (int i = 0; i < pastMonthQty; i++)
                    {
                        tmp = 0;

                        if (!(i == 0 && includeCurrentMonth))
                        {
                            if (currentMonth == 1)
                            {
                                currentMonth = 12;
                                currentYear--;
                            }
                            else
                            {
                                currentMonth--;
                            }

                        }

                        DateTime start = tool.GetPMMAStartDate(currentMonth, currentYear, DT_PMMA_DATE);
                        DateTime end = tool.GetPMMAEndDate(currentMonth, currentYear, DT_PMMA_DATE);

                        //dtpOutFrom.Value = new DateTime(year, month, 1);
                        //dtpOutTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                        foreach (DataRow row in dt_TrfHist.Rows)
                        {
                            string DB_Customer = row[dalTrfHist.TrfTo].ToString();
                            DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                            string item = row[dalTrfHist.TrfItemCode].ToString();
                            if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode) && DB_Customer.Equals(customer))
                            {
                                

                                if (trfDate >= start && trfDate <= end)
                                {
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }

                            }
                            else if (myconnstrng == text.DB_Semenyih && row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                            {
                                if (DB_Customer.Equals("OUG") || DB_Customer.Equals("SP OUG") || DB_Customer.Equals("DAIKIN") || DB_Customer.Equals("PYROCELL") || DB_Customer.Equals("PMMA"))
                                {
                                    if (trfDate >= start && trfDate <= end)
                                    {
                                        tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                    }
                                }
                            }

                        }

                        //get maxout
                        if (MaxOut < tmp)
                        {
                            MaxOut = tmp;
                        }


                    }
                }
            }
            else
            {
                if (pastMonthQty <= 0)
                {
                    DateTime start = dtpOutFrom.Value;
                    DateTime end = dtpOutTo.Value;

                    foreach (DataRow row in dt_TrfHist.Rows)
                    {
                        string item = row[dalTrfHist.TrfItemCode].ToString();
                        string DB_Customer = row[dalTrfHist.TrfTo].ToString();
                        DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                        if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode) && DB_Customer.Equals(customer))
                        {

                            if (trfDate >= start && trfDate <= end)
                            {
                                MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                            }
                        }
                        else if (myconnstrng == text.DB_Semenyih && row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                        {
                            if (DB_Customer.Equals("OUG") || DB_Customer.Equals("SP OUG") || DB_Customer.Equals("DAIKIN") || DB_Customer.Equals("PYROCELL") || DB_Customer.Equals("PMMA"))
                            {
                                if (trfDate >= start && trfDate <= end)
                                {
                                    MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    for (int i = 0; i < pastMonthQty; i++)
                    {
                        tmp = 0;

                        if (currentMonth == 1)
                        {
                            currentMonth = 12;
                            currentYear--;
                        }
                        else
                        {
                            currentMonth--;
                        }

                        DateTime start = new DateTime(currentYear, currentMonth, 1);
                        DateTime end = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));

                        foreach (DataRow row in dt_TrfHist.Rows)
                        {
                            string item = row[dalTrfHist.TrfItemCode].ToString();
                            DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                            if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                            {

                                if (trfDate >= start && trfDate <= end)
                                {
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                            else if (myconnstrng == text.DB_Semenyih && row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                            {
                                if (trfDate >= start && trfDate <= end)
                                {
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }

                        //get maxout
                        if (MaxOut < tmp)
                        {
                            MaxOut = tmp;
                        }
                    }
                }
            }

          

            return MaxOut;
        }

        private float Get6MonthsMaxOut(string itemCode, string customer)
        {
            float MaxOut = 0, tmp = 0;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            for (int i = 0; i < 6; i++)
            {
                tmp = 0;

                if (currentMonth == 1)
                {
                    currentMonth = 12;
                    currentYear--;
                }
                else
                {
                    currentMonth--;
                }

                //get transfer out record
                DataTable dt = dalTrfHist.ItemToCustomerDateSearch(customer, currentMonth.ToString(), currentYear.ToString(), itemCode);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow outRecord in dt.Rows)
                    {
                        if (outRecord["trf_result"].ToString().Equals("Passed"))
                        {
                            tmp += Convert.ToSingle(outRecord["trf_hist_qty"]);
                        }
                    }
                }

                //get maxout
                if (MaxOut < tmp)
                {
                    MaxOut = tmp;
                }


            }
            return MaxOut;
        }

        private void SetPMMADate()
        {
            int year = DateTime.Now.Year;

            if (cmbForecastFrom.SelectedIndex != -1 && cmbCustomer.SelectedIndex != -1)
            {
                string monthString = cmbForecastFrom.Text;

                int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;

                PMMA_START_DATE = tool.GetPMMAStartDate(month, year);
                PMMA_END_DATE = tool.GetPMMAEndDate(month, year);
            }
        }

        private void getStartandEndDate()
        {
            int year = DateTime.Now.Year;

            if (loaded && cmbForecastFrom.SelectedIndex != -1 && cmbCustomer.SelectedIndex != -1)
            {
                string monthString = cmbForecastFrom.Text;

                int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;


                #region new

                if (cmbCustomer.Text.Equals(tool.getCustName(1)) || cmbCustomer.Text.Equals(text.Cmb_All))
                {
                    DateTime outTo = tool.GetPMMAEndDate(month, year);

                    if (DateTime.Today > outTo )
                    {
                        month++;

                        if (month > 12)
                        {
                            month -= 12;
                            year++;
                        }

                        //cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    }
                    else
                    {
                        PMMA_START_DATE = tool.GetPMMAStartDate(month, year);
                        PMMA_END_DATE = tool.GetPMMAEndDate(month, year);

                        dtpPMMAOutFrom.Value = PMMA_START_DATE;
                        dtpPMMAOutTo.Value = PMMA_END_DATE;

                        dtpPMMAOutFrom.Enabled = true;
                        dtpPMMAOutTo.Enabled = true;

                        dtpOutFrom.Enabled = false;
                        dtpOutTo.Enabled = false;
                    }
                }


                if (!cmbCustomer.Text.Equals(tool.getCustName(1)))
                {
                    //check if habit exist
                    string oldOutPeriod_From = "";
                    string oldOutPeriod_To = "";

                    string habitName_1 = text.habit_ForecastReport_OutFrom + "(" + monthString + year + ")";
                    string habitName_2 = text.habit_ForecastReport_OutTo + "(" + monthString + year + ")";

                    DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_ForecastReport, habitName_1, habitName_2);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[dalHabit.HabitName].ToString() == habitName_1)
                        {
                            oldOutPeriod_From = row[dalHabit.HabitData].ToString();

                        }
                        else if (row[dalHabit.HabitName].ToString() == habitName_2)
                        {
                            oldOutPeriod_To = row[dalHabit.HabitData].ToString();
                        }
                    }

                    dtpOutFrom.Value = DateTime.TryParse(oldOutPeriod_From, out DateTime outFrom) ? outFrom : new DateTime(year, month, 1);
                    dtpOutTo.Value = DateTime.TryParse(oldOutPeriod_To, out DateTime outTo) ? outTo : new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    //if (string.IsNullOrEmpty(oldOutPeriod_From))
                    //{
                    //    dtpOutFrom.Value = new DateTime(year, month, 1);
                    //}
                    //else
                    //{
                        

                    //}

                    //if (string.IsNullOrEmpty(oldOutPeriod_To))
                    //{
                    //    dtpOutTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    //}
                    //else
                    //{

                    //}

                    dtpOutFrom.Enabled = true;
                    dtpOutTo.Enabled = true;

                    if (cmbCustomer.Text.Equals(text.Cmb_All))
                    {
                        dtpPMMAOutFrom.Enabled = true;
                        dtpPMMAOutTo.Enabled = true;
                    }
                    else
                    {
                        dtpPMMAOutFrom.Enabled = false;
                        dtpPMMAOutTo.Enabled = false;
                    }
                }

                #endregion

                #region old

                //if (cmbCustomer.Text.Equals(tool.getCustName(1)) || cmbCustomer.Text.Equals(text.Cmb_All))
                //{
                //    DateTime outTo = tool.GetPMMAEndDate(month, year);

                //    if (DateTime.Today > outTo && custChanging)
                //    {
                //        month++;

                //        if (month > 12)
                //        {
                //            month -= 12;
                //            year++;
                //        }

                //        cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                //    }
                //    else
                //    {
                //        PMMA_START_DATE = tool.GetPMMAStartDate(month, year);
                //        PMMA_END_DATE = outTo;

                //        if (!cmbCustomer.Text.Equals(text.Cmb_All))
                //        {
                //            dtpPMMAOutFrom.Value = PMMA_START_DATE;
                //            dtpPMMAOutTo.Value = PMMA_END_DATE;
                //        }
                //    }
                //}

                //if (!cmbCustomer.Text.Equals(tool.getCustName(1)))
                //{
                //    dtpPMMAOutFrom.Value = new DateTime(year, month, 1);
                //    dtpPMMAOutTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                //}

                #endregion


            }
        }

        private Tuple <int,int,int,int> getMonthYearPeriod()
        {
            int year = DateTime.Now.Year;

            int monthStart = 1;
            int yearStart = year;
            int monthEnd = 1;
            int yearEnd = year;

            if (cmbForecastFrom.SelectedIndex != -1)
            {
                string monthString = cmbForecastFrom.Text;

                monthStart = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;

                monthString = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).AddMonths(2).Month);

                monthEnd = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;

            }

            if (monthEnd < monthStart)
            {
                yearEnd++;

            }

            return Tuple.Create(monthStart,yearStart,monthEnd,yearEnd);
        }

        #endregion

        private void EditNextLineRemarkForExcelExport(DataGridView dgv, bool removeNextLine)
        {
            if(dgv.Columns.Contains(text.Header_Remark))
            {

                DataTable dt = (DataTable)dgv.DataSource;
                
                foreach(DataRow row in dt.Rows)
                {
                    string remark = row[text.Header_Remark].ToString();

                    if(removeNextLine)
                    {
                        remark = remark.Replace("\n", "_");
                    }
                    else
                    {
                        remark = remark.Replace("_", "\n");

                    }

                    row[text.Header_Remark] = remark;

                }
            }
        }

        #region export to excel

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

            if(SummaryMode)
            {
                fileName = "ForecastSummaryReport(" + cmbCustomer.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            }
            else
            {
                fileName = "ForecastReport(" + cmbCustomer.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            }

            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                if(dgvForecastReport.DataSource == null)
                {
                    MessageBox.Show("No data.");
                }
                else
                {
                    try
                    {
                        dgvForecastReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        if(dgvForecastReport.Columns.Contains(headerRowReference))
                            dgvForecastReport.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        if (dgvForecastReport.Columns.Contains(headerItemRemark))
                            dgvForecastReport.Columns[headerItemRemark].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        if (SummaryMode)
                        {
                            #region Summary Excel

                            Cursor = Cursors.WaitCursor;

                            btnExcel.Enabled = false;

                            btnFullReport.Enabled = false;
                            btnRefresh.Enabled = false;



                            SaveFileDialog sfd = new SaveFileDialog();
                            string path = @"D:\StockAssistant\Document\ForecastReport";
                            Directory.CreateDirectory(path);
                            sfd.InitialDirectory = path;
                            sfd.Filter = "Excel Documents (*.xls)|*.xls";
                            sfd.FileName = setFileName();

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                                EditNextLineRemarkForExcelExport(dgvForecastReport, true);
                                // Copy DataGridView results to clipboard
                                copyAlltoClipboard();

                                object misValue = Missing.Value;
                                Excel.Application xlexcel = new Excel.Application
                                {
                                    DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                                };

                                frmLoading.ShowLoadingScreen();

                                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"); ;
                                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") FORECAST SUMMARY REPORT";
                                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                                xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + ", PMMA OUT PEROID FROM:" + dtpPMMAOutFrom.Text + " TO:" + dtpPMMAOutTo.Text + ", OTHER CUSTOMER OUT PEROID FROM:" + dtpOutFrom.Text + " TO:" + dtpOutTo.Text;
                                //xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&8 *Produced: was produced within this month.\n-1: No data found in database.";


                                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                                xlWorkSheet.PageSetup.Zoom = false;

                                xlWorkSheet.PageSetup.FitToPagesWide = 1;
                                xlWorkSheet.PageSetup.FitToPagesTall = false;

                                double pointToCMRate = 0.035;
                                xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                                xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                                xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                                xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                                xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                                xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                                xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                                // Paste clipboard results to worksheet range
                                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                                CR.Select();
                                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                                Range tRange = xlWorkSheet.UsedRange;
                                tRange.Font.Size = 8;
                                tRange.RowHeight = 15;
                                tRange.Font.Name = "Calibri";
                                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                                tRange.Borders.Weight = XlBorderWeight.xlThin;

                                DataGridView dgv = dgvForecastReport;


                                //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];

                                Range FirstRow = xlWorkSheet.get_Range("a1:s1").Cells;
                                FirstRow.WrapText = true;
                                FirstRow.Font.Size = 6;
                                FirstRow.Font.Name = "Calibri";
                                FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                                FirstRow.RowHeight = 25;
                                FirstRow.Interior.Color = Color.WhiteSmoke;



                                DataTable dt = (DataTable)dgv.DataSource;

                              
                                int bal3Index = dgv.Columns[headerBal3].Index;
                                int ToDOIndex = dgv.Columns[headerToDo].Index;

                                int Index = dgv.Columns[headerIndex].Index;
                               
                                int bal1Index = dgv.Columns[headerBal1].Index;
                                int bal2Index = dgv.Columns[headerBal2].Index;

                                int nameIndex = dgv.Columns[headerPartName].Index;
                                int codeIndex = dgv.Columns[headerPartCode].Index;

                                int stockIndex = dgv.Columns[headerReadyStock].Index;

                                int forecast1Index = dgv.Columns[headerForecast1].Index;
                                int forecast2Index = dgv.Columns[headerForecast2].Index;
                                int forecast3Index = dgv.Columns[headerForecast3].Index;

                                int outIndex = dgv.Columns[headerOut].Index;
                                int outStdIndex = dgv.Columns[headerOutStd].Index;


                                xlWorkSheet.Cells[1, forecast1Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, forecast2Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, forecast3Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, bal1Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, bal2Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, bal3Index + 1].ColumnWidth = 8;
                                tRange.EntireColumn.AutoFit();

                                xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[stockIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, outIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[outIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, outStdIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[outStdIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, forecast1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast1Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, forecast2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast2Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, forecast3Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast3Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal1Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal2Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal3Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal3Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal1Index + 1].Font.Color = Color.Red;
                                xlWorkSheet.Cells[1, bal2Index + 1].Font.Color = Color.Red;
                                xlWorkSheet.Cells[1, bal3Index + 1].Font.Color = Color.Red;
                                xlWorkSheet.Cells[1, bal1Index + 1].Font.Bold = true;
                                xlWorkSheet.Cells[1, bal2Index + 1].Font.Bold = true;
                                xlWorkSheet.Cells[1, bal3Index + 1].Font.Bold = true;

                                string preToDOType = "";

                                for (int i = 0; i <= dt_SummaryList.Rows.Count - 1; i++)
                                {
                                    Range rangeIndex = (Range)xlWorkSheet.Cells[i + 2, Index + 1];

                                    Range rangeName = (Range)xlWorkSheet.Cells[i + 2, nameIndex + 1];
                                    Range rangeCode = (Range)xlWorkSheet.Cells[i + 2, codeIndex + 1];

                                    Range rangeStock = (Range)xlWorkSheet.Cells[i + 2, stockIndex + 1];

                                    Range rangeForecast1 = (Range)xlWorkSheet.Cells[i + 2, forecast1Index + 1];
                                    Range rangeForecast2 = (Range)xlWorkSheet.Cells[i + 2, forecast2Index + 1];
                                    Range rangeForecast3 = (Range)xlWorkSheet.Cells[i + 2, forecast3Index + 1];

                                    Range rangeOut = (Range)xlWorkSheet.Cells[i + 2, outIndex + 1];
                                    Range rangeOutStd = (Range)xlWorkSheet.Cells[i + 2, outStdIndex + 1];

                                    Range rangeRow = (Range)xlWorkSheet.Rows[i + 2];
                                    //rangeRow.Rows.RowHeight = 40;
                                    rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                                    rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                                    //color bal font
                                    Range rangeBal1 = (Range)xlWorkSheet.Cells[i + 2, bal1Index + 1];
                                    rangeBal1.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal1Index].InheritedStyle.ForeColor);

                                    Range rangeBal2 = (Range)xlWorkSheet.Cells[i + 2, bal2Index + 1];
                                    rangeBal2.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal2Index].InheritedStyle.ForeColor);

                                    Range rangeBal3 = (Range)xlWorkSheet.Cells[i + 2, bal3Index + 1];
                                    Range rangeToDO = (Range)xlWorkSheet.Cells[i + 2, ToDOIndex + 1];

                                    rangeBal3.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal3Index].InheritedStyle.ForeColor);

                                    string ToDOString = dt_SummaryList.Rows[i][headerToDo].ToString();

                                    if(preToDOType == "")
                                    {
                                        preToDOType = ToDOString;
                                    }
                                    else if(preToDOType != ToDOString)
                                    {
                                        preToDOType = ToDOString;

                                        Range rangeBoldLineBelow = xlWorkSheet.get_Range("a" +(i+2).ToString() + ":s" + (i + 2).ToString()).Cells;

                                        rangeBoldLineBelow.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThick;
                                    }


                                    if (ToDOString.Equals(ToDoType_ToAssembly))
                                    {
                                        rangeToDO.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);
                                    }
                                    else if (ToDOString.Equals(ToDoType_ToProduce))
                                    {
                                        rangeToDO.Interior.Color = ColorTranslator.ToOle(Color.Gold);
                                    }
                                    else if (ToDOString.Equals(ToDoType_ToOrder))
                                    {
                                        rangeToDO.Interior.Color = ColorTranslator.ToOle(Color.Lavender);
                                    }

                                    string itemCode = dgv.Rows[i].Cells[headerPartCode].Value.ToString();

                                    //color empty space
                                    if (string.IsNullOrEmpty(itemCode))
                                    {
                                        rangeRow.Rows.RowHeight = 2;
                                        Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[i + 2, 1], xlWorkSheet.Cells[i + 2, xlWorkSheet.UsedRange.Columns.Count]];
                                        rng.Interior.Color = Color.Black;
                                    }
                                    else
                                    {
                                        rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.ForeColor);
                                        rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.ForeColor);

                                        rangeBal1.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
                                        rangeBal2.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
                                        rangeBal3.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
                                        rangeStock.Interior.Color = ColorTranslator.ToOle(Color.Aquamarine);
                                    }

                                    string parentColor = dt.Rows[i][headerParentColor].ToString();
                                    string type = dt.Rows[i][headerType].ToString();
                                    string balType = dt.Rows[i][headerBalType].ToString();

                                    //change parent color
                                    if (parentColor.Equals(AssemblyMarking))
                                    {
                                        rangeName.Font.Underline = true;
                                        rangeCode.Font.Underline = true;
                                    }
                                    else if (parentColor.Equals(InsertMoldingMarking))
                                    {
                                        rangeName.Font.Underline = true;
                                        rangeCode.Font.Underline = true;
                                    }
                                    else if (parentColor.Equals(AssemblyAfterProductionMarking))
                                    {
                                        rangeName.Font.Underline = true;
                                        rangeCode.Font.Underline = true;
                                    }

                                    if (type.Equals(typeChild))
                                    {
                                        rangeForecast1.Font.Italic = true;
                                        rangeForecast2.Font.Italic = true;
                                        rangeForecast3.Font.Italic = true;
                                    }

                                    if (balType.Equals(balType_Total))
                                    {
                                        rangeBal1.Font.Bold = true;
                                        rangeBal1.Font.Italic = true;
                                        rangeBal1.Font.Underline = true;

                                        rangeBal2.Font.Bold = true;
                                        rangeBal2.Font.Italic = true;
                                        rangeBal2.Font.Underline = true;

                                        rangeBal3.Font.Bold = true;
                                        rangeBal3.Font.Italic = true;
                                        rangeBal3.Font.Underline = true;
                                    }
                                    else
                                    {
                                        rangeBal1.Font.Bold = true;
                                        rangeBal2.Font.Bold = true;
                                        rangeBal3.Font.Bold = true;

                                    }

                                }

                                // Save the excel file under the captured location from the SaveFileDialog
                                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                xlexcel.DisplayAlerts = true;
                                xlWorkBook.Close(true, misValue, misValue);
                                xlexcel.Quit();

                                releaseObject(xlWorkSheet);
                                releaseObject(xlWorkBook);
                                releaseObject(xlexcel);

                                // Clear Clipboard and DataGridView selection
                                Clipboard.Clear();
                                dgvForecastReport.ClearSelection();

                                // Open the newly saved excel file
                                //if (File.Exists(sfd.FileName))
                                //    System.Diagnostics.Process.Start(sfd.FileName);
                                OpenCSVWithExcel(sfd.FileName);

                                EditNextLineRemarkForExcelExport(dgvForecastReport, false);
                            }

                            #endregion
                        }
                        else
                        {
                            #region Excel 


                            Cursor = Cursors.WaitCursor;

                            btnExcel.Enabled = false;

                            btnFullReport.Enabled = false;
                            btnRefresh.Enabled = false;


                            SaveFileDialog sfd = new SaveFileDialog();
                            string path = @"D:\StockAssistant\Document\ForecastReport";
                            Directory.CreateDirectory(path);
                            sfd.InitialDirectory = path;
                            sfd.Filter = "Excel Documents (*.xls)|*.xls";
                            sfd.FileName = setFileName();

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                                EditNextLineRemarkForExcelExport(dgvForecastReport, true);

                                // Copy DataGridView results to clipboard
                                copyAlltoClipboard();

                                object misValue = Missing.Value;
                                Excel.Application xlexcel = new Excel.Application
                                {
                                    DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                                };

                                frmLoading.ShowLoadingScreen();

                                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"); ;
                                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") FORECAST REPORT";
                                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                                xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + ", PMMA OUT PEROID FROM:" + dtpPMMAOutFrom.Text + " TO:" + dtpPMMAOutTo.Text + ", OTHER CUSTOMER OUT PEROID FROM:" + dtpOutFrom.Text + " TO:" + dtpOutTo.Text;
                                //xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&8 *Produced: was produced within this month.\n-1: No data found in database.";


                                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                                xlWorkSheet.PageSetup.Zoom = false;

                                xlWorkSheet.PageSetup.FitToPagesWide = 1;
                                xlWorkSheet.PageSetup.FitToPagesTall = false;

                                double pointToCMRate = 0.035;
                                xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                                xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                                xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                                xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                                xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                                xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                                xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                                // Paste clipboard results to worksheet range
                                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                                CR.Select();
                                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                                Range tRange = xlWorkSheet.UsedRange;
                                tRange.Font.Size = 8;
                                tRange.RowHeight = 15;
                                tRange.Font.Name = "Calibri";
                                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                                tRange.Borders.Weight = XlBorderWeight.xlThin;

                                DataGridView dgv = dgvForecastReport;

                                int forecast3Index = dgv.Columns[headerForecast3].Index;

                                //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                                Range FirstRow = xlWorkSheet.get_Range("a1:u1").Cells;
                                FirstRow.WrapText = true;
                                FirstRow.Font.Size = 6;
                                FirstRow.Font.Name = "Calibri";
                                FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                                FirstRow.RowHeight = 25;
                                FirstRow.Interior.Color = Color.WhiteSmoke;



                                DataTable dt = (DataTable)dgv.DataSource;

                                int Index = dgv.Columns[headerIndex].Index;
                                int weightIndex = dgv.Columns[headerPartWeight].Index;

                                int customerIndex = -1;

                                if(cmbCustomer.Text == text.Cmb_All)
                                {
                                    customerIndex = dgv.Columns[text.Header_Customer].Index;
                                    xlWorkSheet.Cells[1, customerIndex + 1].ColumnWidth = 10;
                                }

                                int bal1Index = dgv.Columns[headerBal1].Index;
                                int bal2Index = dgv.Columns[headerBal2].Index;
                                int bal3Index = dgv.Columns[headerBal3].Index;

                                int nameIndex = dgv.Columns[headerPartName].Index;
                                int codeIndex = dgv.Columns[headerPartCode].Index;

                                int stockIndex = dgv.Columns[headerReadyStock].Index;
                                int estimateIndex = dgv.Columns[headerEstimate].Index;

                                int forecast1Index = dgv.Columns[headerForecast1].Index;
                                int forecast2Index = dgv.Columns[headerForecast2].Index;


                                int outIndex = dgv.Columns[headerOut].Index;
                                int outStdIndex = dgv.Columns[headerOutStd].Index;

                                xlWorkSheet.Cells[1, weightIndex + 1].ColumnWidth = 10;
                                xlWorkSheet.Cells[1, forecast1Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, forecast2Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, forecast3Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, bal1Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, bal2Index + 1].ColumnWidth = 8;
                                xlWorkSheet.Cells[1, bal3Index + 1].ColumnWidth = 8;
                                tRange.EntireColumn.AutoFit();

                                xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[stockIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, estimateIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[estimateIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, outIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[outIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, outStdIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[outStdIndex].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, forecast1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast1Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, forecast2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast2Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal1Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal2Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal3Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal3Index].InheritedStyle.BackColor);
                                xlWorkSheet.Cells[1, bal1Index + 1].Font.Color = Color.Red;
                                xlWorkSheet.Cells[1, bal2Index + 1].Font.Color = Color.Red;
                                xlWorkSheet.Cells[1, bal3Index + 1].Font.Color = Color.Red;
                                xlWorkSheet.Cells[1, bal1Index + 1].Font.Bold = true;
                                xlWorkSheet.Cells[1, bal2Index + 1].Font.Bold = true;
                                xlWorkSheet.Cells[1, bal3Index + 1].Font.Bold = true;

                                for (int i = 0; i <= dtMasterData.Rows.Count - 1; i++)
                                {
                                    Range rangeIndex = (Range)xlWorkSheet.Cells[i + 2, Index + 1];
                                    Range rangeWeight = (Range)xlWorkSheet.Cells[i + 2, weightIndex + 1];

                                    Range rangeName = (Range)xlWorkSheet.Cells[i + 2, nameIndex + 1];
                                    Range rangeCode = (Range)xlWorkSheet.Cells[i + 2, codeIndex + 1];

                                    Range rangeStock = (Range)xlWorkSheet.Cells[i + 2, stockIndex + 1];
                                    Range rangeEstimate = (Range)xlWorkSheet.Cells[i + 2, estimateIndex + 1];

                                    Range rangeForecast1 = (Range)xlWorkSheet.Cells[i + 2, forecast1Index + 1];
                                    Range rangeForecast2 = (Range)xlWorkSheet.Cells[i + 2, forecast2Index + 1];
                                    Range rangeForecast3 = (Range)xlWorkSheet.Cells[i + 2, forecast3Index + 1];

                                    Range rangeOut = (Range)xlWorkSheet.Cells[i + 2, outIndex + 1];
                                    Range rangeOutStd = (Range)xlWorkSheet.Cells[i + 2, outStdIndex + 1];

                                    Range rangeRow = (Range)xlWorkSheet.Rows[i + 2];
                                    //rangeRow.Rows.RowHeight = 40;
                                    rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                                    rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                                    rangeWeight.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                                    //color bal font
                                    Range rangeBal1 = (Range)xlWorkSheet.Cells[i + 2, bal1Index + 1];
                                    rangeBal1.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal1Index].InheritedStyle.ForeColor);

                                    Range rangeBal2 = (Range)xlWorkSheet.Cells[i + 2, bal2Index + 1];
                                    rangeBal2.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal2Index].InheritedStyle.ForeColor);

                                    Range rangeBal3 = (Range)xlWorkSheet.Cells[i + 2, bal3Index + 1];
                                    rangeBal3.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal3Index].InheritedStyle.ForeColor);

                                    string itemCode = dgv.Rows[i].Cells[headerPartCode].Value.ToString();

                                    //color empty space
                                    if (string.IsNullOrEmpty(itemCode))
                                    {
                                        rangeRow.Rows.RowHeight = 2;
                                        Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[i + 2, 1], xlWorkSheet.Cells[i + 2, xlWorkSheet.UsedRange.Columns.Count]];
                                        //Range rng2 = xlWorkSheet.Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, xlWorkSheet.UsedRange.Columns.Count]);
                                        //Range changeRowBackColor = xlWorkSheet.get_Range(i+2, last);
                                        rng.Interior.Color = Color.Black;
                                    }
                                    else
                                    {
                                        rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.ForeColor);
                                        rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.ForeColor);

                                        rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.BackColor);
                                        rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.BackColor);

                                        rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[stockIndex].InheritedStyle.BackColor);
                                        rangeEstimate.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[estimateIndex].InheritedStyle.BackColor);

                                        rangeForecast1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[forecast1Index].InheritedStyle.BackColor);
                                        rangeForecast2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[forecast2Index].InheritedStyle.BackColor);
                                        rangeForecast3.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[forecast3Index].InheritedStyle.BackColor);

                                        rangeBal1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal1Index].InheritedStyle.BackColor);
                                        rangeBal2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal2Index].InheritedStyle.BackColor);
                                        rangeBal3.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal3Index].InheritedStyle.BackColor);

                                        rangeOut.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[outIndex].InheritedStyle.BackColor);
                                        rangeOutStd.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[outStdIndex].InheritedStyle.BackColor);
                                    }

                                    string parentColor = dt.Rows[i][headerParentColor].ToString();
                                    string type = dt.Rows[i][headerType].ToString();
                                    string balType = dt.Rows[i][headerBalType].ToString();

                                    //change parent color
                                    if (parentColor.Equals(AssemblyMarking))
                                    {
                                        rangeName.Font.Underline = true;
                                        rangeCode.Font.Underline = true;
                                    }
                                    else if (parentColor.Equals(InsertMoldingMarking))
                                    {
                                        rangeName.Font.Underline = true;
                                        rangeCode.Font.Underline = true;
                                    }
                                    else if (parentColor.Equals(AssemblyAfterProductionMarking))
                                    {
                                        rangeName.Font.Underline = true;
                                        rangeCode.Font.Underline = true;
                                    }

                                    if (type.Equals(typeChild))
                                    {
                                        rangeForecast1.Font.Italic = true;
                                        rangeForecast2.Font.Italic = true;
                                        rangeForecast3.Font.Italic = true;
                                    }

                                    if (balType.Equals(balType_Total))
                                    {
                                        rangeBal1.Font.Bold = true;
                                        rangeBal1.Font.Italic = true;
                                        rangeBal1.Font.Underline = true;

                                        rangeBal2.Font.Bold = true;
                                        rangeBal2.Font.Italic = true;
                                        rangeBal2.Font.Underline = true;

                                        rangeBal3.Font.Bold = true;
                                        rangeBal3.Font.Italic = true;
                                        rangeBal3.Font.Underline = true;
                                        //dgv.Rows[i].Cells[headerBal1].Style.Font = _BalFont;
                                        //dgv.Rows[i].Cells[headerBal2].Style.Font = _BalFont;
                                    }
                                    else
                                    {
                                        rangeBal1.Font.Bold = true;
                                        rangeBal2.Font.Bold = true;
                                        rangeBal3.Font.Bold = true;
                                    }

                                }

                                // Save the excel file under the captured location from the SaveFileDialog
                                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                xlexcel.DisplayAlerts = true;
                                xlWorkBook.Close(true, misValue, misValue);
                                xlexcel.Quit();

                                releaseObject(xlWorkSheet);
                                releaseObject(xlWorkBook);
                                releaseObject(xlexcel);

                                // Clear Clipboard and DataGridView selection
                                Clipboard.Clear();
                                dgvForecastReport.ClearSelection();

                                // Open the newly saved excel file
                                //if (File.Exists(sfd.FileName))
                                //    System.Diagnostics.Process.Start(sfd.FileName);
                                OpenCSVWithExcel(sfd.FileName);

                                EditNextLineRemarkForExcelExport(dgvForecastReport, false);

                            }

                            #endregion
                        }


                    }
                    catch (Exception ex)
                    {
                        tool.saveToTextAndMessageToUser(ex);
                    }
                    finally
                    {
                        frmLoading.CloseForm();

                        dgvForecastReport.SelectionMode = DataGridViewSelectionMode.CellSelect;

                        if (dgvForecastReport.Columns.Contains(headerRowReference))
                            dgvForecastReport.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                        if (dgvForecastReport.Columns.Contains(headerItemRemark))
                            dgvForecastReport.Columns[headerItemRemark].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                        btnExcel.Enabled = true;
                        btnFullReport.Enabled = true;
                        btnRefresh.Enabled = true;


                        Cursor = Cursors.Arrow; // change cursor to normal type
                    }
                }
               
            }
          
        }

        private void copyAlltoClipboard()
        {
            dgvForecastReport.SelectAll();
            DataObject dataObj = dgvForecastReport.GetClipboardContent();
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

        private void btnExcelAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\ForecastReport";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "ForecastReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

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
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    insertAllDataToSheet(path, sfd.FileName);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvForecastReport.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void insertAllDataToSheet(string path, string fileName)
        {
            string custName = tool.getCustName(1);

            int totalCust = cmbCustomer.Items.Count;

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

            //frmLoading.ShowLoadingScreen();
            for (int i = 0; i <= cmbCustomer.Items.Count - 1; i++)
            {
                cmbCustomer.SelectedIndex = i;
                ShowDetailForecastReport();

                string cust = cmbCustomer.Text;

                DataTable dt = NewForecastReportTable();

                if (dgvForecastReport.DataSource != null)
                {
                    dt = (DataTable)dgvForecastReport.DataSource;
                }

                if (dt.Rows.Count > 0)//if datagridview have data
                {
                    Worksheet xlWorkSheet = null;

                    int count = g_Workbook.Worksheets.Count;

                    xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                            g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                    xlWorkSheet.Name = cmbCustomer.Text;

                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") READY STOCK VERSUS FORECAST";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + ", PMMA OUT PEROID FROM:" + dtpPMMAOutFrom.Text + " TO:" + dtpPMMAOutTo.Text + ", OTHER CUSTOMER OUT PEROID FROM:" + dtpOutFrom.Text + " TO:" + dtpOutTo.Text;
                    xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&8 *Produced: was produced within this month.\n-1: No data found in database.";


                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;
                    
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                    // Paste clipboard results to worksheet range
                    copyAlltoClipboard();
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);



                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Font.Size = 8;
                    tRange.RowHeight = 15;
                    tRange.Font.Name = "Calibri";
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;

                    DataGridView dgv = dgvForecastReport;

                    int forecast3Index = dgv.Columns[headerForecast3].Index;

                    //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                    Range FirstRow = xlWorkSheet.get_Range("a1:o1").Cells;
                    FirstRow.WrapText = true;
                    FirstRow.Font.Size = 6;
                    FirstRow.Font.Name = "Calibri";
                    FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    FirstRow.RowHeight = 25;
                    FirstRow.Interior.Color = Color.WhiteSmoke;



                    //DataTable dt = (DataTable)dgv.DataSource;

                    int Index = dgv.Columns[headerIndex].Index;
                    int weightIndex = dgv.Columns[headerPartWeight].Index;

                    int bal1Index = dgv.Columns[headerBal1].Index;
                    int bal2Index = dgv.Columns[headerBal2].Index;

                    int nameIndex = dgv.Columns[headerPartName].Index;
                    int codeIndex = dgv.Columns[headerPartCode].Index;

                    int stockIndex = dgv.Columns[headerReadyStock].Index;
                    int estimateIndex = dgv.Columns[headerEstimate].Index;

                    int forecast1Index = dgv.Columns[headerForecast1].Index;
                    int forecast2Index = dgv.Columns[headerForecast2].Index;


                    int outIndex = dgv.Columns[headerOut].Index;
                    int outStdIndex = dgv.Columns[headerOutStd].Index;

                    xlWorkSheet.Cells[1, weightIndex + 1].ColumnWidth = 10;
                    xlWorkSheet.Cells[1, forecast1Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, forecast2Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, forecast3Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, bal1Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, bal2Index + 1].ColumnWidth = 8;
                    tRange.EntireColumn.AutoFit();

                    xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[stockIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, estimateIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[estimateIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, outIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[outIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, outStdIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[outStdIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, forecast1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[forecast1Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, forecast2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[forecast2Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, bal1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[bal1Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, bal2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[bal2Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, bal1Index + 1].Font.Color = Color.Red;
                    xlWorkSheet.Cells[1, bal2Index + 1].Font.Color = Color.Red;
                    xlWorkSheet.Cells[1, bal1Index + 1].Font.Bold = true;
                    xlWorkSheet.Cells[1, bal2Index + 1].Font.Bold = true;

                    for (int j = 0; j <= dtMasterData.Rows.Count - 1; j++)
                    {
                        Range rangeIndex = (Range)xlWorkSheet.Cells[j + 2, Index + 1];
                        Range rangeWeight = (Range)xlWorkSheet.Cells[j + 2, weightIndex + 1];

                        Range rangeName = (Range)xlWorkSheet.Cells[j + 2, nameIndex + 1];
                        Range rangeCode = (Range)xlWorkSheet.Cells[j + 2, codeIndex + 1];

                        Range rangeStock = (Range)xlWorkSheet.Cells[j + 2, stockIndex + 1];
                        Range rangeEstimate = (Range)xlWorkSheet.Cells[j + 2, estimateIndex + 1];

                        Range rangeForecast1 = (Range)xlWorkSheet.Cells[j + 2, forecast1Index + 1];
                        Range rangeForecast2 = (Range)xlWorkSheet.Cells[j + 2, forecast2Index + 1];
                        Range rangeForecast3 = (Range)xlWorkSheet.Cells[j + 2, forecast3Index + 1];

                        Range rangeOut = (Range)xlWorkSheet.Cells[j + 2, outIndex + 1];
                        Range rangeOutStd = (Range)xlWorkSheet.Cells[j + 2, outStdIndex + 1];

                        Range rangeRow = (Range)xlWorkSheet.Rows[j + 2];
                        //rangeRow.Rows.RowHeight = 40;
                        rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                        rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                        rangeWeight.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                        //color bal font
                        Range rangeBal1 = (Range)xlWorkSheet.Cells[j + 2, bal1Index + 1];
                        rangeBal1.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal1Index].InheritedStyle.ForeColor);

                        Range rangeBal2 = (Range)xlWorkSheet.Cells[j + 2, bal2Index + 1];
                        rangeBal2.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal2Index].InheritedStyle.ForeColor);

                        string itemCode = dgv.Rows[j].Cells[headerPartCode].Value.ToString();

                        //color empty space
                        if (string.IsNullOrEmpty(itemCode))
                        {
                            rangeRow.Rows.RowHeight = 2;
                            Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[j + 2, 1], xlWorkSheet.Cells[j + 2, xlWorkSheet.UsedRange.Columns.Count]];
                            //Range rng2 = xlWorkSheet.Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, xlWorkSheet.UsedRange.Columns.Count]);
                            //Range changeRowBackColor = xlWorkSheet.get_Range(i+2, last);
                            rng.Interior.Color = Color.Black;
                        }
                        else
                        {
                            rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.ForeColor);
                            rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.ForeColor);

                            rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.BackColor);
                            rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.BackColor);

                            rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[stockIndex].InheritedStyle.BackColor);
                            rangeEstimate.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[estimateIndex].InheritedStyle.BackColor);

                            rangeForecast1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast1Index].InheritedStyle.BackColor);
                            rangeForecast2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast2Index].InheritedStyle.BackColor);
                            rangeForecast3.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast3Index].InheritedStyle.BackColor);

                            rangeBal1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal1Index].InheritedStyle.BackColor);
                            rangeBal2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal2Index].InheritedStyle.BackColor);

                            rangeOut.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[outIndex].InheritedStyle.BackColor);
                            rangeOutStd.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[outStdIndex].InheritedStyle.BackColor);
                        }

                        string parentColor = dt.Rows[j][headerParentColor].ToString();
                        string type = dt.Rows[j][headerType].ToString();
                        string balType = dt.Rows[j][headerBalType].ToString();

                        //change parent color
                        if (parentColor.Equals(AssemblyMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }
                        else if (parentColor.Equals(InsertMoldingMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }
                        else if (parentColor.Equals(AssemblyAfterProductionMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }

                        if (type.Equals(typeChild))
                        {
                            rangeForecast1.Font.Italic = true;
                            rangeForecast2.Font.Italic = true;
                            rangeForecast3.Font.Italic = true;
                        }

                        if (balType.Equals(balType_Total))
                        {
                            rangeBal1.Font.Bold = true;
                            rangeBal1.Font.Italic = true;
                            rangeBal1.Font.Underline = true;

                            rangeBal2.Font.Bold = true;
                            rangeBal2.Font.Italic = true;
                            rangeBal2.Font.Underline = true;
                            //dgv.Rows[i].Cells[headerBal1].Style.Font = _BalFont;
                            //dgv.Rows[i].Cells[headerBal2].Style.Font = _BalFont;
                        }
                        else
                        {
                            rangeBal1.Font.Bold = true;
                            rangeBal2.Font.Bold = true;
                        }

                    }
                   

                    releaseObject(xlWorkSheet);
                    Clipboard.Clear();
                    dgvForecastReport.ClearSelection();
                }

            }

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        private void frmForecastReport_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_NEW_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cmbCustomer.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Please select a customer.");
            //    }
            //    else
            //    {
            //        LoadForecastData();
            //    }
            //}
        }

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cmbCustomer.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Please select a customer.");
            //    }
            //    else
            //    {
            //        LoadForecastData();
            //    }
            //}
        }

        private void txtNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cmbCustomer.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Please select a customer.");
            //    }
            //    else
            //    {
            //        LoadForecastData();
            //    }
            //}
        }

        private void cbRemoveForecastInvalidItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

            if(cbRemoveNoOrderItem.Checked)
            {
                cbRemoveNoDeliveredItem.Checked = true;
            }

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbShowSummary_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            dtMasterData = null;
            dt_SummaryList = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                txtItemSearch.Text = text.Search_DefaultText;
                txtItemSearch.ForeColor = SystemColors.GrayText;
                ItemSearchUIReset();

                ProPlanningSearchReset();
                DT_ITEM = dalItem.Select();
                ShowSummaryForecastReport();
                if (dgvForecastReport.DataSource != null)
                    lblProductionPlanningMode.Visible = true;
            }
        }

        private void cbShowInsufficientOnly_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

        }

        private void cbSortByToDOType_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

        }

        private void cbSortByBalance_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

        }

        private void cbShowToProduceItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

            if(!cbShowToProduceItem.Checked)
            {
                if(!cbShowToOrderItem.Checked && !cbShowToAssemblyItem.Checked)
                {
                    cbShowToProduceItem.Checked = true;

                    MessageBox.Show("At least 1 Summary Type to be selected.");
                }
            }

        }

        private void cbShowToAssemblyItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

            if (!cbShowToAssemblyItem.Checked)
            {
                if (!cbShowToProduceItem.Checked && !cbShowToOrderItem.Checked)
                {
                    cbShowToAssemblyItem.Checked = true;

                    MessageBox.Show("At least 1 Summary Type to be selected.");
                }
            }
        }

        private void cbShowToOrderItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

            if (!cbShowToOrderItem.Checked)
            {
                if (!cbShowToProduceItem.Checked && !cbShowToAssemblyItem.Checked)
                {
                    cbShowToOrderItem.Checked = true;

                    MessageBox.Show("At least 1 Summary Type to be selected.");
                }
            }

        }

        private void cbShowRawMaterial_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

        }

        private void cbIncludeTerminated_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

        }

        private void cbIncludeProInfo_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cbMainCustomerOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                if (cbMainCustomerOnly.Checked)
                {
                    tool.loadMainCustomerAndAllToComboBox(cmbCustomer);
                    getStartandEndDate();

                }
                else
                {
                    tool.loadCustomerAndALLWithoutOtherToComboBox(cmbCustomer);

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            JumpToNextRow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackToPreviousRow();
        }

        private void txtNameSearch_Enter(object sender, EventArgs e)
        {
            if (txtItemSearch.Text == text.Search_DefaultText)
            {
                txtItemSearch.Text = "";
                txtItemSearch.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNameSearch_Leave(object sender, EventArgs e)
        {
            if (txtItemSearch.Text.Length == 0)
            {
                txtItemSearch.Text = text.Search_DefaultText;
                txtItemSearch.ForeColor = SystemColors.GrayText;

                ItemSearchUIReset();

            }
        }

        private void dgvForecastReport_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvForecastReport;
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 )
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string currentHeader = dgv.Columns[colIndex].Name;

                try
                {
                    if(currentHeader == headerReadyStock)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[headerPartCode].Value.ToString());
                    }
                    else if (currentHeader == headerRawMat)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[headerRawMat].Value.ToString());
                    }
                    else if (currentHeader == headerColorMat)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[text.Header_ColorMatCode].Value.ToString());
                    }
                    else
                    {
                        my_menu.Items.Add(text.DeliveredSummary).Name = text.DeliveredSummary;
                        my_menu.Items.Add(text.ProductionHistory).Name = text.ProductionHistory;
                        my_menu.Items.Add(text.ForecastRecord).Name = text.ForecastRecord;
                        my_menu.Items.Add(text.JobPlanning).Name = text.JobPlanning;
                        //my_menu.Items.Add(text.StockLocation).Name = text.StockLocation;

                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                        contextMenuStrip1 = my_menu;
                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    }
                    


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void ShowStockLocation(string itemCode)
        {
            ContextMenuStrip my_menu = new ContextMenuStrip();

         

            my_menu.Items.Add(itemCode + "     " + tool.getItemNameFromDataTable(DT_ITEM, itemCode)).Name = itemCode;
            my_menu.Items.Add("").Name = itemCode;

            DataTable dt = dalStock.Select(itemCode);

            float TotalStock = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    string fac = stock["fac_name"].ToString();
                    float qty = float.TryParse(stock["stock_qty"].ToString(), out float x) ? x : 0;

                    TotalStock += qty;

                    if (qty > 0)
                    {
                        int length = fac.Length;
                        if ((10 - length) >= 0)
                        {
                            for (int i = 1; i <= (10 - length); i++)
                            {
                                fac += " ";
                            }
                        }

                        my_menu.Items.Add(fac + " " + qty.ToString("0.##")).Name = fac;

                    }

                }


                my_menu.Items.Add("---------------------").Name = "Line1";
                my_menu.Items.Add("Total" + "       " + TotalStock.ToString("0.##")).Name = "Total";
                my_menu.Items.Add("---------------------").Name = "Line2";
            }
            else
            {
                string fac = "null";
                string qty = "null";

                my_menu.Items.Add(fac + "   " + qty).Name = fac;
            }

            my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvForecastReport;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            string itemCode = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[headerPartCode].Value.ToString();
            string customer = cmbCustomer.Text;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.DeliveredSummary))
            {
                frmLoading.ShowLoadingScreen();

                if(cmbCustomer.Text == text.Cmb_All)
                {
                    customer = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_Customer].Value.ToString();

                }

                frmInOutReport_NEW frm = new frmInOutReport_NEW(itemCode, customer);

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                frm.Size = new Size(1700, 450);
                frm.Show();

                frmLoading.CloseForm();
            }
            else if (itemClicked.Equals(text.ProductionHistory))
            {
                frmLoading.ShowLoadingScreen();

                frmMachineSchedule frm = new frmMachineSchedule(itemCode);

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                frm.Size = new Size(1700, 800);
                frm.Show();

                frmLoading.CloseForm();
            }
            else if(itemClicked.Equals(text.ForecastRecord))
            {
                if(cmbCustomer.Text == text.Cmb_All && dgv.Columns.Contains(text.Header_Customer))
                {
                    customer = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_Customer].Value.ToString();
                }

                ForecastEditRecord(customer, itemCode);
            }
            else if (itemClicked.Equals(text.JobPlanning))
            {

                int targetQty = int.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[headerBal3].Value.ToString(), out targetQty)? targetQty : 1;

                targetQty = targetQty < 0 ? targetQty * -1 : targetQty;
                
                frmJobAdding frm = new frmJobAdding(itemCode, targetQty);

                frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.WindowState = FormWindowState.Maximized;
                //frm.Size = new Size(1700, 450);
                frm.ShowDialog();

            }
            else if (itemClicked.Equals(text.StockLocation))
            {
                ShowStockLocation(itemCode);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private string GetItemCode(string input)
        {
            //int lastOpenParenthesis = input.LastIndexOf('(');
            //int lastCloseParenthesis = input.LastIndexOf(')');

            //if (lastOpenParenthesis == -1 || lastCloseParenthesis == -1 || lastCloseParenthesis < lastOpenParenthesis)
            //{
            //    return "Invalid input"; // Or throw an exception based on your error handling policy
            //}

            //return input.Substring(lastOpenParenthesis + 1, lastCloseParenthesis - lastOpenParenthesis - 1).Trim();

            int openParenthesisCount = 0;
            int closeParenthesisIndex = -1;

            // Start from the end of the string
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == ')')
                {
                    if (openParenthesisCount == 0)
                    {
                        closeParenthesisIndex = i;
                    }
                    openParenthesisCount++;
                }
                else if (input[i] == '(')
                {
                    openParenthesisCount--;
                    if (openParenthesisCount == 0)
                    {
                        return input.Substring(i + 1, closeParenthesisIndex - i - 1).Trim();
                    }
                }
            }

            return "Invalid input"; // Or throw an exception based on your error handling policy
        }

        public void ForecastEditRecord(string customer, string itemcode)
        {
            historyDAL dalHistory = new historyDAL();

            DataTable DB_History = dalHistory.ForecastEditHistorySelect();

            string itemName = tool.getItemNameFromDataTable(DT_ITEM, itemcode);

            if (DB_History != null)
            {
                //DataTable dt = DB_History.Clone();

                DataTable dt = new DataTable();

                dt.Columns.Add(text.Header_ID, typeof(string));
                dt.Columns.Add(text.Header_Date, typeof(DateTime));
                dt.Columns.Add(text.Header_Description, typeof(string));
                dt.Columns.Add(text.Header_EditedBy, typeof(string));
                dt.Columns.Add(text.Header_Customer, typeof(string));
                dt.Columns.Add(text.Header_Month, typeof(string));
                dt.Columns.Add(text.Header_OldValue, typeof(string));
                dt.Columns.Add(text.Header_NewValue, typeof(string));

                
                int monthFrom_INT = int.TryParse(cmbForecastFrom.Text, out monthFrom_INT) ? monthFrom_INT : 0;

                monthFrom_INT = DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).Month;

                int monthTo_INT = int.TryParse(cmbForecastTo.Text, out monthTo_INT) ? monthTo_INT : 0;

                monthTo_INT = DateTime.ParseExact(cmbForecastTo.Text, "MMMM", CultureInfo.CurrentCulture).Month;

                int yearFrom_INT = DateTime.Now.Year;

                int yearTo_INT = monthTo_INT < monthFrom_INT ? yearFrom_INT+1 : yearFrom_INT;

                DateTime Start = DateTime.Parse(1 + cmbForecastFrom.Text + yearFrom_INT);
                DateTime End = DateTime.Parse(1 + cmbForecastTo.Text + yearTo_INT);

                foreach (DataRow row in DB_History.Rows)
                {
                    string historyDetail = row[dalHistory.HistoryDetail].ToString();

                    if(historyDetail.Contains("PLASTIC CAP/PLASTIC GROUT VENT(SAFETY)") && historyDetail.Contains(customer))
                    {
                        var checkpoint = 1;
                    }

                    //get month and year and customer data
                    string historyCustomer = "";
                    string historyItem = "";
                    string historyMonthAndYear = "";
                    string historyMonth = "";
                    string historyYear = "";

                    string value = "";
                    string oldValue = "";
                    string newValue = "";

                    bool gettingCustomerInfo = false;
                    bool gettingMonthAndYearInfo = false;
                    bool gettingItemInfo = false;
                    bool gettingValueInfo = false;

                    DateTime HistoryDate = DateTime.MaxValue;


                    for (int i = 0; i < historyDetail.Length; i++)
                    {
                        if (gettingValueInfo)
                        {
                            value += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == ":")
                        {
                            gettingItemInfo = false;
                            gettingValueInfo = true;
                        }

                        if (gettingItemInfo)
                        {
                            historyItem += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "]")
                        {
                            gettingMonthAndYearInfo = false;
                            gettingItemInfo = true;
                        }

                        if (gettingMonthAndYearInfo)
                        {
                            historyMonthAndYear += historyDetail[i].ToString();
                        }

                        if (gettingCustomerInfo && historyDetail[i].ToString() == "_")
                        {
                            gettingCustomerInfo = false;
                            gettingMonthAndYearInfo = true;
                        }

                        if (gettingCustomerInfo)
                        {
                            historyCustomer += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "[")
                        {
                            gettingCustomerInfo = true;
                        }
                    }

                    if (historyMonthAndYear.Length > 4)
                    {
                        for (int i = historyMonthAndYear.Length - 4; i < historyMonthAndYear.Length; i++)
                        {
                            historyYear += historyMonthAndYear[i].ToString();
                        }

                        historyMonth = historyMonthAndYear.Replace(historyYear, "");
                        HistoryDate = DateTime.TryParse(1.ToString() + "/" + historyMonth + "/" + historyYear, out DateTime test) ? test : DateTime.MaxValue;

                    }

                
                    //string historyItemNameRemoved = historyItem.Replace(itemName,"");
                    //string historyItemNameRemoved = GetItemCode(historyItem);
                    
                    string historyItemCode = GetItemCode(historyItem);

                    //if (historyItemNameRemoved.Replace(" ","") =="()" && itemcode == itemName)
                    //{
                    //    historyItemCode = itemcode;
                    //}
                    //else
                    //{
                    //    bool gettingCodeInfo = false;
                    //    for (int i = 0; i < historyItemNameRemoved.Length - 1; i++)
                    //    {
                    //        if (gettingCodeInfo)
                    //        {
                    //            historyItemCode += historyItemNameRemoved[i].ToString();
                    //        }

                    //        if (historyItemNameRemoved[i].ToString() == "(")
                    //        {
                    //            gettingCodeInfo = true;
                    //        }

                    //    }
                    //}


                    //date inspection
                    bool dataMatched = true;

                    //dataMatched = historyItem.Contains(itemcode) ? dataMatched : false;

                    dataMatched = historyItemCode == itemcode ? dataMatched : false;

                    dataMatched = historyCustomer == customer ? dataMatched : false;


                    dataMatched = HistoryDate >= Start && HistoryDate <= End ? dataMatched : false;

                    dataMatched = HistoryDate == DateTime.MaxValue ? false : dataMatched;


                    if (dataMatched)
                    {
                        DataRow newRow = dt.NewRow();

                        newRow[text.Header_ID] = row[dalHistory.HistoryID].ToString();
                        newRow[text.Header_Date] = DateTime.TryParse(row[dalHistory.HistoryDate].ToString(), out DateTime Date) ? Date : DateTime.MaxValue;

                        string Desciption = "";
                        string HistoryAction = row[dalHistory.HistoryAction].ToString();

                        if (HistoryAction.Contains(text.ForecastEdit))
                        {
                            Desciption = text.DataUpdated;
                        }
                        else if (HistoryAction.Contains(text.ForecastInsert))
                        {
                            Desciption = text.DataAdded;
                        }

                        newRow[text.Header_Description] = Desciption;
                        newRow[text.Header_EditedBy] = new userDAL().getUsername(int.TryParse(row[dalHistory.HistoryBy].ToString(), out int userId) ? userId : 0);
                        newRow[text.Header_Customer] = historyCustomer;
                        newRow[text.Header_Month] = HistoryDate.ToString("yyyy/MM");

                        value = value.Replace(" ", "");

                        if (value.Contains("->"))
                        {
                            bool newValueFound = false;

                            for (int i = 0; i < value.Length; i++)
                            {
                                if (newValueFound)
                                {
                                    newValue += value[i].ToString();
                                }
                                else if (value[i].ToString() != ("-") && value[i].ToString() != (">"))
                                {
                                    oldValue += value[i].ToString();

                                }

                                if (value[i].ToString().Equals(">"))
                                {
                                    newValueFound = true;
                                }


                            }
                        }
                        else
                        {
                            newValue = value;
                            oldValue = "NA";
                        }


                        newRow[text.Header_OldValue] = oldValue;
                        newRow[text.Header_NewValue] = newValue;

                        dt.Rows.Add(newRow);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.Sort = text.Header_Month + " ASC," + text.Header_Date + " DESC";
                    dt = dt.DefaultView.ToTable();

                    DataTable dt_ForecastEditRecord = dt.Clone();

                    string previousMonth = "";

                    foreach (DataRow row in dt.Rows)
                    {
                        string month = row[text.Header_Month].ToString();

                        DateTime Month_DateTime = DateTime.TryParse(month, out Month_DateTime) ? Month_DateTime : DateTime.MaxValue;

                        row[text.Header_Month] = Month_DateTime.ToString("MMM-yy");

                        if (!string.IsNullOrEmpty(previousMonth) && previousMonth != month)
                        {
                            //insert empty row
                            dt_ForecastEditRecord.Rows.Add(dt_ForecastEditRecord.NewRow());
                        }


                        dt_ForecastEditRecord.Rows.Add(row.ItemArray);
                        previousMonth = month;

                    }

                    frmForecastEditRecord frm = new frmForecastEditRecord(dt_ForecastEditRecord, itemcode);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("History not found.");
                }
            }
            else
            {
                MessageBox.Show("History not found.");
            }
        }

        private int LAST_CHECKED_ID = -1;

        private bool ForecastEditRecordChecked()
        {
            LAST_CHECKED_ID = -1;

            FORECAST_HISTORY_NEW = false;

            DB_FOREACST_HISTORY = dalHistory.ForecastEditHistorySelect();//393

            DB_FOREACST_HISTORY.DefaultView.Sort = dalHistory.HistoryID + " DESC";
            DB_FOREACST_HISTORY = DB_FOREACST_HISTORY.DefaultView.ToTable();

            string habitName_1 = text.habit_ForecastReport_ForecastHistoryChecked ;
            int userID = MainDashboard.USER_ID;

            DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_ForecastReport, habitName_1, userID);

            if(dt == null || dt.Rows.Count <= 0)
            {
                FORECAST_HISTORY_NEW = true;
                return true;
            }
            else
            {
                //get habit data INT
                int habit_History_ID = int.TryParse(dt.Rows[0][dalHabit.HabitData].ToString(), out int i) ? i : -1;

                if(habit_History_ID > 0)
                {
                    if(DB_FOREACST_HISTORY?.Rows.Count > 0)
                    {
                        //get lastest forecast edit history id
                        int forecast_History_ID = int.TryParse(DB_FOREACST_HISTORY.Rows[0][dalHistory.HistoryID].ToString(), out i) ? i : -1;

                        if(forecast_History_ID > habit_History_ID)
                        {
                            FORECAST_HISTORY_NEW = true;

                            LAST_CHECKED_ID = habit_History_ID;

                            return true;
                        }
                    }
                    else
                    {
                        FORECAST_HISTORY_NEW = true;
                        return true;
                    }
                }
                else
                {
                    FORECAST_HISTORY_NEW = true;
                    return true;

                }
            }


            return false;

        }

        private bool FORECAST_HISTORY_NEW = false;


        private DataTable DT_FOREACST_EDIT_RECORD;
        private DataTable DB_FOREACST_HISTORY;
        historyDAL dalHistory = new historyDAL();

        public void ForecastEditRecord()
        {
            frmLoading.ShowLoadingScreen();

            if(DB_FOREACST_HISTORY == null)
            {
                DB_FOREACST_HISTORY = dalHistory.ForecastEditHistorySelect();//393

                DB_FOREACST_HISTORY.DefaultView.Sort = dalHistory.HistoryID + " DESC";
                DB_FOREACST_HISTORY = DB_FOREACST_HISTORY.DefaultView.ToTable();
            }

            if (DB_FOREACST_HISTORY != null && DB_FOREACST_HISTORY.Rows.Count > 0)
            {
                int latestHistoryID = int.TryParse(DB_FOREACST_HISTORY.Rows[0][dalHistory.HistoryID].ToString(), out int x) ? x : -1;

                //DataTable dt = DB_History.Clone();

                DataTable dt = new DataTable();

                dt.Columns.Add(text.Header_ID, typeof(string));
                dt.Columns.Add(text.Header_Date, typeof(DateTime));
                dt.Columns.Add(text.Header_Description, typeof(string));
                dt.Columns.Add(text.Header_EditedBy, typeof(string));
                dt.Columns.Add(text.Header_ItemNameAndCode, typeof(string));
                dt.Columns.Add(text.Header_Customer, typeof(string));
                dt.Columns.Add(text.Header_Month, typeof(string));
                dt.Columns.Add(text.Header_OldValue, typeof(string));
                dt.Columns.Add(text.Header_NewValue, typeof(string));


                int monthFrom_INT = int.TryParse(cmbForecastFrom.Text, out monthFrom_INT) ? monthFrom_INT : 0;
                int monthTo_INT = int.TryParse(cmbForecastTo.Text, out monthTo_INT) ? monthTo_INT : 0;

                int yearFrom_INT = DateTime.Now.Year;

                int yearTo_INT = monthTo_INT < monthFrom_INT ? yearFrom_INT++ : yearFrom_INT;

                DateTime Start = DateTime.Parse(1 + cmbForecastFrom.Text + yearFrom_INT);
                DateTime End = DateTime.Parse(1 + cmbForecastTo.Text + yearTo_INT);

                foreach (DataRow row in DB_FOREACST_HISTORY.Rows)
                {
                    #region Filter

                    string historyDetail = row[dalHistory.HistoryDetail].ToString();

                    //get month and year and customer data
                    string historyCustomer = "";
                    string historyItem = "";
                    string historyMonthAndYear = "";
                    string historyMonth = "";
                    string historyYear = "";

                    string value = "";
                    string oldValue = "";
                    string newValue = "";

                    bool gettingCustomerInfo = false;
                    bool gettingMonthAndYearInfo = false;
                    bool gettingItemInfo = false;
                    bool gettingValueInfo = false;

                    DateTime HistoryDate = DateTime.MaxValue;


                    for (int i = 0; i < historyDetail.Length; i++)
                    {
                        if (gettingValueInfo)
                        {
                            value += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == ":")
                        {
                            gettingItemInfo = false;
                            gettingValueInfo = true;
                        }

                        if (gettingItemInfo)
                        {
                            historyItem += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "]")
                        {
                            gettingMonthAndYearInfo = false;
                            gettingItemInfo = true;
                        }

                        if (gettingMonthAndYearInfo)
                        {
                            historyMonthAndYear += historyDetail[i].ToString();
                        }

                        if (gettingCustomerInfo && historyDetail[i].ToString() == "_")
                        {
                            gettingCustomerInfo = false;
                            gettingMonthAndYearInfo = true;
                        }

                        if (gettingCustomerInfo)
                        {
                            historyCustomer += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "[")
                        {
                            gettingCustomerInfo = true;
                        }
                    }

                    if (historyMonthAndYear.Length > 4)
                    {
                        for (int i = historyMonthAndYear.Length - 4; i < historyMonthAndYear.Length; i++)
                        {
                            historyYear += historyMonthAndYear[i].ToString();
                        }

                        historyMonth = historyMonthAndYear.Replace(historyYear, "");
                        HistoryDate = DateTime.TryParse(1.ToString() + "/" + historyMonth + "/" + historyYear, out DateTime test) ? test : DateTime.MaxValue;

                    }


                    //date inspection
                    bool dataMatched = true;
                   
                    dataMatched = HistoryDate >= Start ? dataMatched : false;

                    dataMatched = HistoryDate == DateTime.MaxValue ? false : dataMatched;

                    #endregion

                    #region Insert Data

                    if (dataMatched)
                    {
                        DataRow newRow = dt.NewRow();

                        newRow[text.Header_ID] = row[dalHistory.HistoryID].ToString();
                        newRow[text.Header_Date] = DateTime.TryParse(row[dalHistory.HistoryDate].ToString(), out DateTime Date) ? Date : DateTime.MaxValue;

                        string Desciption = "";
                        string HistoryAction = row[dalHistory.HistoryAction].ToString();

                        if (HistoryAction.Contains(text.ForecastEdit))
                        {
                            Desciption = text.DataUpdated;
                        }
                        else if (HistoryAction.Contains(text.ForecastInsert))
                        {
                            Desciption = text.DataAdded;
                        }

                        newRow[text.Header_Description] = Desciption;
                        newRow[text.Header_EditedBy] = new userDAL().getUsername(int.TryParse(row[dalHistory.HistoryBy].ToString(), out int userId) ? userId : 0);
                        newRow[text.Header_Customer] = historyCustomer;
                        newRow[text.Header_ItemNameAndCode] = historyItem;
                        newRow[text.Header_Month] = HistoryDate.ToString("MM/yyyy");

                        value = value.Replace(" ", "");

                        if (value.Contains("->"))
                        {
                            bool newValueFound = false;

                            for (int i = 0; i < value.Length; i++)
                            {
                                if (newValueFound)
                                {
                                    newValue += value[i].ToString();
                                }
                                else if (value[i].ToString() != ("-") && value[i].ToString() != (">"))
                                {
                                    oldValue += value[i].ToString();

                                }

                                if (value[i].ToString().Equals(">"))
                                {
                                    newValueFound = true;
                                }


                            }
                        }
                        else
                        {
                            newValue = value;
                            oldValue = "NA";
                        }


                        newRow[text.Header_OldValue] = oldValue;
                        newRow[text.Header_NewValue] = newValue;

                        dt.Rows.Add(newRow);
                    }

                    #endregion

                }
                //2228
                if (dt.Rows.Count > 0)
                {
                    //dt.DefaultView.Sort = text.Header_Month + " ASC," + text.Header_Date + " DESC";

                    dt.DefaultView.Sort = text.Header_Date + " DESC," + text.Header_ItemNameAndCode + " ASC," + text.Header_Month + " ASC" ;
                    dt = dt.DefaultView.ToTable();

                    //DataTable dt_ForecastEditRecord = dt.Copy();

                    DT_FOREACST_EDIT_RECORD = dt.Clone();

                    string previousMonth = "";

                    foreach (DataRow row in dt.Rows)
                    {
                        string month = row[text.Header_Month].ToString();

                        DateTime Month_DateTime = DateTime.TryParse(month, out Month_DateTime) ? Month_DateTime : DateTime.MaxValue;

                        row[text.Header_Month] = Month_DateTime.ToString("MMM-yy");

                        if (!string.IsNullOrEmpty(previousMonth) && previousMonth != month)
                        {
                            //insert empty row
                            //dt_ForecastEditRecord.Rows.Add(dt_ForecastEditRecord.NewRow());
                        }


                        DT_FOREACST_EDIT_RECORD.Rows.Add(row.ItemArray);
                        previousMonth = month;

                    }

                    DT_FOREACST_EDIT_RECORD = RemoveDuplicateRows(DT_FOREACST_EDIT_RECORD);
                    frmLoading.CloseForm();
                    frmForecastEditRecord frm = new frmForecastEditRecord(DT_FOREACST_EDIT_RECORD, LAST_CHECKED_ID);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.Size = new Size(1100, 800);
                    frm.ShowDialog();

                    ForecastHistoryCheckedHabitUpdate(latestHistoryID);

                    ifNewForeacstUpdatesFound(false);
                }
                else
                {
                    frmLoading.CloseForm();
                    MessageBox.Show("History not found.");
                }
            }
            else
            {
                frmLoading.CloseForm();
                MessageBox.Show("History not found.");
            }

         
        }

        private DataTable RemoveDuplicateRows(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row1 = dt.Rows[i];
                for (int j = i + 1; j < dt.Rows.Count; j++)
                {
                    DataRow row2 = dt.Rows[j];
                    if (row1[text.Header_ItemNameAndCode].Equals(row2[text.Header_ItemNameAndCode]) && row1[text.Header_Customer].Equals(row2[text.Header_Customer]) && row1[text.Header_Month].Equals(row2[text.Header_Month]))
                    {
                        DateTime date1 = Convert.ToDateTime(row1[text.Header_Date]);
                        DateTime date2 = Convert.ToDateTime(row2[text.Header_Date]);

                        // Remove the older row
                        if (DateTime.Compare(date1, date2) > 0)
                        {
                            dt.Rows.RemoveAt(j);
                            j--; // Decrement index to account for removed row
                        }
                        else
                        {
                            dt.Rows.RemoveAt(i);
                            i--; // Decrement index to account for removed row
                            break; // Exit inner loop as the current outer row has been removed
                        }
                    }
                }
            }

            return dt;
        }

        private void cbRemoveNoDeliveredItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;

            if (cbRemoveNoDeliveredItem.Checked)
            {
                cbRemoveNoOrderItem.Checked = true;
            }

        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void dgvForecastReport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //try
            //{
            //    CELL_VALUE_CHANGED = false;
            //    CELL_EDITING_OLD_VALUE = "";

            //    DataGridView dgv = dgvForecastReport;

            //    e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            //    e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

            //    int colIndex = dgv.CurrentCell.ColumnIndex;
            //    int rowIndex = dgv.CurrentCell.RowIndex;

            //    if (dgv.Columns[colIndex].Name.Contains(headerItemRemark)) //Desired Column
            //    {
            //        CELL_EDITING_OLD_VALUE = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

            //        TextBox tb = e.Control as TextBox;

            //        if (tb != null)
            //        {
            //            tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            //        }
            //    }
            //    else
            //    {
            //        TextBox tb = e.Control as TextBox;

            //        if (tb != null)
            //        {
            //            tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
        }

        private void dgvForecastReport_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (CELL_VALUE_CHANGED)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Do you want to save changes?", "Message",
            //                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //        string itemCode = dgvForecastReport.Rows[e.RowIndex].Cells[headerPartCode].Value.ToString();

            //        itemBLL uItem = new itemBLL();

            //        uItem.item_code = itemCode;
            //        uItem.item_remark = CELL_EDITING_NEW_VALUE;
            //        uItem.item_updtd_date = DateTime.Now;
            //        uItem.item_updtd_by = MainDashboard.USER_ID;


            //        if (!dalItem.ItemRemarkUpdate(uItem))
            //        {
            //            MessageBox.Show("Failed to update item remark!");
            //        }

            //        CELL_EDITING_OLD_VALUE = "";
            //        CELL_EDITING_NEW_VALUE = "";

            //        Cursor = Cursors.Arrow; // change cursor to normal type

            //    }
            //}
        }

        private void dgvForecastReport_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //CELL_VALUE_CHANGED = true;

            //DataGridView dgv = dgvForecastReport;

            //CELL_EDITING_NEW_VALUE = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void lblForecastType_Click(object sender, EventArgs e)
        {
            dgvForecastReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //if (dgvForecastReport.Columns.Contains(headerRowReference))
            //    dgvForecastReport.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            //if (dgvForecastReport.Columns.Contains(headerItemRemark))
            //    dgvForecastReport.Columns[headerItemRemark].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            txtItemSearch.Text = text.Search_DefaultText;
            txtItemSearch.ForeColor = SystemColors.GrayText;
            ItemSearchUIReset();

            lblSearchClear.Visible = false;
            btnFullReport.Focus();
        }

        private void dgvForecastReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbSummaryMonthBalSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt_SummaryList = (DataTable)dgvForecastReport.DataSource;
            
            if(dt_SummaryList != null && loaded)
            {
                frmLoading.ShowLoadingScreen();

                #region Sorting

                string SortString = "";

                if (cbSortByToDOType.Checked)
                {
                    SortString = headerToDo + " ASC";
                }

                if (cbSortByBalance.Checked)
                {
                    string MonthSelected = cmbSummaryMonthBalSort.Text;

                    string headerToSort = headerBal3 + " ASC, " + headerBal1 + " ASC, " + headerBal2 + " ASC";

                    if (MonthSelected == cmbForecastFrom.Text)
                    {
                        headerToSort = headerBal1 + " ASC, " + headerBal2 + " ASC, " + headerBal3 + " ASC";
                    }
                    else if (MonthSelected != cmbForecastTo.Text)
                    {
                        headerToSort = headerBal2 + " ASC, " + headerBal1 + " ASC, " + headerBal3 + " ASC";

                    }

                    if (string.IsNullOrEmpty(SortString))
                    {

                        SortString = headerToSort;
                    }
                    else
                    {
                        SortString += ", " + headerToSort;
                    }
                }

                if (!string.IsNullOrEmpty(SortString))
                {
                    dt_SummaryList.DefaultView.Sort = SortString;

                    dt_SummaryList = dt_SummaryList.DefaultView.ToTable();
                }


                dgvForecastReport.DataSource = dt_SummaryList;

                #endregion


                #region Load Data To Datagridview

                dgvForecastReport.DataSource = dt_SummaryList;

                dgvForecastReport.Columns.Remove(headerRowReference);
                dgvForecastReport.Columns.Remove(headerColorMat);
                dgvForecastReport.Columns.Remove(headerPartWeight);

                ColorSummaryData();//2075

                //delete column
                dgvForecastReport.Columns.Remove(headerItemType);
                dgvForecastReport.Columns.Remove(headerParentColor);
                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);

                DgvForecastSummaryReportUIEdit(dgvForecastReport);

                #endregion

                if (dgvForecastReport.DataSource != null)
                {
                    //ColorData();

                    //DgvForecastReportUIEdit(dgvForecastReport);

                    //dgvForecastReport.Columns.Remove(headerItemType);

                    if (cbSpecialTypeColorMode.Checked && dgvForecastReport.Columns.Contains(headerParentColor))
                        dgvForecastReport.Columns.Remove(headerParentColor);


                    dgvForecastReport.ClearSelection();
                    dgvForecastReport.ResumeLayout();

                    dgvForecastReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    dgvForecastReport.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                }

                frmLoading.CloseForm();

            }


        }

        private void lblProductionPlanningMode_Click(object sender, EventArgs e)
        {
            ShowProductionPlanningFilter(lblProductionPlanningMode.Text.Equals(textShoweProPlanningFilter));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProPlanningSearchReset()
        {
            txtMacSearching.Text = "";
            txtMacSearching.Text = "";
            txtColorMatSearching.Text = "";
        }

        private void btnProPlanningFilterApply_Click(object sender, EventArgs e)
        {
            ProPlanningDataFilter();
        }

        private bool CheckForCommonElements(string str1, string str2)
        {
            str1 = str1.Replace("，",",");
            str2 = str2.Replace("，",",");

            string[] elements1 = str1.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] elements2 = str2.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            HashSet<string> set1 = new HashSet<string>(elements1);
            HashSet<string> set2 = new HashSet<string>(elements2);

            return set1.Intersect(set2).Any();
        }

        private string ExtractMachineInfo(string remark)
        {
            remark = remark.Replace(" ", "");
            remark = remark.ToUpper();

            string keyword = "MAC:";

            string MacInfo = "";

            int index = remark.IndexOf(keyword);

            if (index != -1)
            {
                MacInfo =  remark.Substring(index + keyword.Length);

                MacInfo = MacInfo.Replace(";", "");

            }

            return MacInfo;
        }


        private void ProPlanningDataFilter()
        {

            if(DT_FORECAST_REPORT.Rows.Count > 0)
            {

            }
            else
            {
                DT_FORECAST_REPORT = (DataTable)dgvForecastReport.DataSource;

            }


            DataTable dt_ProPlanningFilteredData = DT_FORECAST_REPORT.Clone();

            //get machine filter data
            string machineSearching = txtMacSearching.Text;
            string rawMatSearching = txtRawMatSearching.Text;
            string colorMatSearching = txtColorMatSearching.Text;


            foreach (DataRow row in DT_FORECAST_REPORT.Rows)
            {
                bool dataMatched = true;

                string remark = row[text.Header_Remark].ToString();
                string itemName = row[headerPartName].ToString();
                string rawMat = row[headerRawMat].ToString();
                string colorMatInfo = row[headerColorMat].ToString();

                string macInfo = ExtractMachineInfo(remark);

                bool hasCommonElements = CheckForCommonElements(machineSearching, macInfo);

                if(!hasCommonElements && !string.IsNullOrEmpty(machineSearching))
                {
                    dataMatched = false;
                }

                if (!rawMat.ToUpper().Contains(rawMatSearching.ToUpper()) && !string.IsNullOrEmpty(rawMatSearching))
                {
                    dataMatched = false;
                }

                if (!colorMatInfo.ToUpper().Contains(colorMatSearching.ToUpper()) && !string.IsNullOrEmpty(colorMatSearching))
                {
                    dataMatched = false;
                }

                if (dataMatched)
                {
                    //MessageBox.Show(itemName + " The two strings have common elements.");

                    dt_ProPlanningFilteredData.Rows.Add(row.ItemArray);

                }
            }

            DataGridView dgv = dgvForecastReport;

            dgv.DataSource = null;

            dgv.DataSource = dt_ProPlanningFilteredData;

            if (dgv.DataSource != null)
            {
                ColorData();

                DgvForecastReportUIEdit(dgvForecastReport);

                dgvForecastReport.Columns.Remove(headerItemType);

                if (cbSpecialTypeColorMode.Checked)
                    dgvForecastReport.Columns.Remove(headerParentColor);

                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);
                dgvForecastReport.ClearSelection();
                dgvForecastReport.ResumeLayout();
                
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }

            //get raw material filter data

            //get color material filter data
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            txtMacSearching.Text = "";
        }

        private void label15_Click(object sender, EventArgs e)
        {
            txtRawMatSearching.Text = "";

        }

        private void label18_Click(object sender, EventArgs e)
        {
            txtColorMatSearching.Text = "";

        }

        private void ForecastHistoryCheckedHabitUpdate(int latestHistoryID)
        {
            if(latestHistoryID > 0)
            {
                string habitData_1 = text.habit_ForecastReport_ForecastHistoryChecked;
                string habitData_2 = latestHistoryID.ToString();
                //save habit
                uHabit.belong_to = text.habit_belongTo_ForecastReport;
                uHabit.habit_name = habitData_1;
                uHabit.habit_data = habitData_2;
                uHabit.added_date = DateTime.Now;
                uHabit.added_by = MainDashboard.USER_ID;

                dalHabit.HabitInsertAndHistoryRecordWithUserID(uHabit);
            }
        }

        private void OtherCustomerOutPeriodSave()
        {
            if(loaded && OK_TO_CHECK_DATE_HABIT)
            {

                string habitData_1 = dtpOutFrom.Value.ToString();
                string habitData_2 = dtpOutTo.Value.ToString();

                string monthString = cmbForecastFrom.Text;

                int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
                int year = DateTime.Now.Year;

                //check old data
                string oldOutPeriod_From = "";
                string oldOutPeriod_To = "";

                string habitName_1 = text.habit_ForecastReport_OutFrom + "(" + monthString + year + ")";
                string habitName_2 = text.habit_ForecastReport_OutTo + "(" + monthString + year + ")";

                DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_ForecastReport, habitName_1, habitName_2);


                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalHabit.HabitName].ToString() == habitName_1)
                    {
                        oldOutPeriod_From = row[dalHabit.HabitData].ToString();

                    }
                    else if (row[dalHabit.HabitName].ToString() == habitName_2)
                    {
                        oldOutPeriod_To = row[dalHabit.HabitData].ToString();
                    }
                }
                bool success = true;

                if (oldOutPeriod_From != habitData_1 || oldOutPeriod_To != habitData_2)
                {

                    string DateFrom = dtpOutFrom.Value.ToString("dd MMM yyyy");
                    string DateTo = dtpOutTo.Value.ToString("dd MMM yyyy");

                    string message = $"Do you want to set the out period from {DateFrom} to {DateTo} as the default?";

                    DialogResult dialogResult = MessageBox.Show(message, "Message",
                                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (oldOutPeriod_From != habitData_1)
                        {
                            //save habit
                            uHabit.belong_to = text.habit_belongTo_ForecastReport;
                            uHabit.habit_name = habitName_1;
                            uHabit.habit_data = habitData_1;
                            uHabit.added_date = DateTime.Now;
                            uHabit.added_by = MainDashboard.USER_ID;

                            success = dalHabit.HabitInsertAndHistoryRecord(uHabit);
                        }

                        if (oldOutPeriod_To != habitData_2)
                        {
                            //save habit
                            uHabit.belong_to = text.habit_belongTo_ForecastReport;
                            uHabit.habit_name = habitName_2;
                            uHabit.habit_data = habitData_2;
                            uHabit.added_date = DateTime.Now;
                            uHabit.added_by = MainDashboard.USER_ID;

                            success = dalHabit.HabitInsertAndHistoryRecord(uHabit);
                        }
                    }
                }
                   
                    

               

               
            }
        }

        private void dtpOutFrom_ValueChanged_1(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            OtherCustomerOutPeriodSave();

        }

        private void dtpOutTo_ValueChanged_1(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            OtherCustomerOutPeriodSave();

        }

        bool OK_TO_CHECK_DATE_HABIT = false;

        private void frmForecastReport_NEW_Shown(object sender, EventArgs e)
        {
            OK_TO_CHECK_DATE_HABIT = true;
            if (myconnstrng == text.DB_Semenyih)
            {
                cmbCustomer.Text = "ALL";
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            ForecastEditRecord();
        }
    }

}

