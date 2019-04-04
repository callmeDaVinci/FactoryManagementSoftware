using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport : Form
    {
        public frmForecastReport()
        {
            InitializeComponent();
        }

        #region Variable Declare

        enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        
        enum color
        {
            Gold = 0,
            Aquamarine,
            Bisque,
            Chartreuse,
            CornflowerBlue,
            Yellow,
            Cyan,
            MediumTurquoise,
            LightGray,
           // Magenta,
            MediumAquamarine,
            LightSalmon,
            Lavender,
            MediumSpringGreen,
            DeepSkyBlue,
            Orange,
            //Orchid,
            //Seashell,
            SkyBlue,
            Tan,
            Thistle,
            LightBlue
        }

        private int indexNo = 1;
        //private int alphbet = 65;
        private bool gotData = false;
        private int colorOrder = 0;
        private string colorName = "Gold";
        private int redAlertLevel = 0;
        private string singlePartEndIndex = "0";
        readonly string IndexColName = "NO";
        readonly string MatTypeColName = "MATERIAL TYPE";
        readonly string NameColName = "PART NAME";
        readonly string CodeColName = "PART CODE";
        readonly string ColorColName = "COLOR";
        readonly string MCColName = "MC";
        readonly string PartWeightColName = "PRO PW(g)";
        readonly string RunnerWeightColName = "PRO RW(g)";
        readonly string MBColName = "MBatch CODE";
        readonly string StockColName = "READY STOCK";
        readonly string EstimateColName = "ESTIMATE";
        readonly string Forecast1ColName = "F/CAST 1";
        readonly string Forecast2ColName = "F/CAST 2";
        readonly string Forecast3ColName = "F/CAST 3";
        readonly string OutColName = "OUT";
        readonly string OsantColName = "OSANT";
        readonly string Shot1ColName = "SHOT 1";
        readonly string Shot2ColName = "SHOT 2";

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        #endregion

        void StartWork()
        {
            // Start BackGround Worker Thread 
            bgWorker.RunWorkerAsync();
        }

        void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //NOTE : DONT play with the UI thread here...
            // Do Whatever work you are doing and for which you need to show    progress bar
            //CopyLotsOfFiles() // This is the function which is being run in the background
            e.Result = true;// Tell that you are done
        }

        void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Access Main UI Thread here
            progressBar1.Value = e.ProgressPercentage;
        }

        #region create class object (database)

        custBLL uCust = new custBLL();
        custDAL dalCust = new custDAL();

        facBLL uFac = new facBLL();
        facDAL dalFac = new facDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        itemCustBLL uItemCust = new itemCustBLL();
        itemCustDAL dalItemCust = new itemCustDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        forecastBLL uForecast = new forecastBLL();
        forecastDAL dalForecast = new forecastDAL();

        trfHistDAL dalTrfHist = new trfHistDAL();

        userDAL dalUser = new userDAL();

        Tool tool = new Tool();
        Text text = new Text();
        #endregion  

        #region Form Load/Close

        private void frmForecastReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_Load(object sender, EventArgs e)
        {
            tool.loadCustomerToComboBox(cmbCust);
            tool.DoubleBuffered(dgvForecastReport, true);
        }

        #endregion

        #region Data Checking/Get Data

        private int getMonthValue(string keyword)
        {
            
            int month = 0;
            if (string.IsNullOrEmpty(keyword))
            {
                month =  Convert.ToInt32(DateTime.Now.Month.ToString());
            }
            else
            {
                month = (int)(Month)Enum.Parse(typeof(Month), keyword);
            }
            

            return month;
        }

        private int getNextMonth(string keyword)
        {
           
            int month = getMonthValue(keyword);
            int nextMonth = 0;

            if(month == 12)
            {
                nextMonth = 1;
            }
            else
            {
                nextMonth = month + 1;
            }
            return nextMonth;
        }

        private int getNextNextMonth(string keyword)
        {

            int nextMonth = getNextMonth(keyword);
            int nextNextMonth = 0;

            if (nextMonth == 12)
            {
                nextNextMonth = 1;
            }
            else
            {
                nextNextMonth = nextMonth + 1;
            }
            return nextNextMonth;
        }

        private string getShortMonth(string month, int forecast)
        {
            string shortMonth = "";
            int monthValue = 0;
            switch (forecast)
            {
                case 1:
                    monthValue = getMonthValue(month);
                    break;
                case 2:
                    monthValue = getNextMonth(month);
                    break;
                case 3:
                    monthValue = getNextNextMonth(month);
                    break;
                default:
                    monthValue = 0;
                    break;
            }

            switch (monthValue)
            {
                case 1:
                    shortMonth = "Jan";
                    break;
                case 2:
                    shortMonth = "Feb";
                    break;
                case 3:
                    shortMonth = "Mar";
                    break;
                case 4:
                    shortMonth = "Apr";
                    break;
                case 5:
                    shortMonth = "May";
                    break;
                case 6:
                    shortMonth = "Jun";
                    break;
                case 7:
                    shortMonth = "Jul";
                    break;
                case 8:
                    shortMonth = "Aug";
                    break;
                case 9:
                    shortMonth = "Sep";
                    break;
                case 10:
                    shortMonth = "Oct";
                    break;
                case 11:
                    shortMonth = "Nov";
                    break;
                case 12:
                    shortMonth = "Dec";
                    break;
            }
            
            
            return shortMonth;
        }

        private string getCustID(string custName)
        {
            string custID = "";

            DataTable dtCust = dalCust.nameSearch(custName);

            foreach (DataRow Cust in dtCust.Rows)
            {
                custID = Cust["cust_id"].ToString();
            }
            return custID;
        }
    
        private bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

                return result;
        }

        #endregion

        #region UI Edit

        private void createPMMADGV()
        {
            DataGridView dgv = dgvForecastReport;

            if(dgv != null)
            {
                dgv.Columns.Clear();
            }
            

            //create column
            tool.AddTextBoxColumns(dgv, IndexColName, IndexColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, MatTypeColName, MatTypeColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, NameColName, NameColName, Fill);
            tool.AddTextBoxColumns(dgv, CodeColName, CodeColName, Fill);
            tool.AddTextBoxColumns(dgv, ColorColName, ColorColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, MCColName, MCColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, PartWeightColName, PartWeightColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, RunnerWeightColName, RunnerWeightColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, MBColName, MBColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, StockColName, StockColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Forecast1ColName, Forecast1ColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, OutColName, OutColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, OsantColName, OsantColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Shot1ColName, Shot1ColName, DisplayedCells);

            tool.AddTextBoxColumns(dgv, Forecast2ColName, Forecast2ColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Shot2ColName, Shot2ColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Forecast3ColName, Forecast3ColName, DisplayedCells);

            //alignment setting
            dgv.Columns[MCColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[PartWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[RunnerWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[MBColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[StockColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[Forecast1ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[OutColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[OsantColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[Shot1ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //UI setting

            dgv.Columns[MCColName].DefaultCellStyle.BackColor = Color.AliceBlue;

            dgv.Columns[MCColName].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            dgv.Columns[MCColName].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
            dgv.Columns[MCColName].Width = 40;

            dgv.Columns[PartWeightColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[PartWeightColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[RunnerWeightColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[RunnerWeightColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[StockColName].HeaderCell.Style.BackColor = Color.Pink;
            dgv.Columns[StockColName].DefaultCellStyle.BackColor = Color.Pink;
            dgv.Columns[StockColName].HeaderCell.Style.ForeColor = Color.Blue;

            dgv.Columns[Forecast1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[Forecast1ColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[OutColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[OutColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[OsantColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[OsantColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[Shot1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[Shot1ColName].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[Shot1ColName].HeaderCell.Style.ForeColor = Color.Red;
            dgv.Columns[StockColName].MinimumWidth = 70;

            dgv.Columns[Forecast2ColName].HeaderCell.Style.BackColor = Color.LightCyan;
            dgv.Columns[Forecast2ColName].DefaultCellStyle.BackColor = Color.LightCyan;

            dgv.Columns[Shot2ColName].HeaderCell.Style.BackColor = Color.LightCyan;
            dgv.Columns[Shot2ColName].DefaultCellStyle.BackColor = Color.LightCyan;
            dgv.Columns[Shot2ColName].HeaderCell.Style.ForeColor = Color.Red;

            dgv.Columns[Forecast3ColName].HeaderCell.Style.BackColor = Color.PeachPuff;
            dgv.Columns[Forecast3ColName].DefaultCellStyle.BackColor = Color.PeachPuff;
        }

        private void createDGV()
        {
            DataGridView dgv = dgvForecastReport;

            dgv.Columns.Clear();

            tool.AddTextBoxColumns(dgv, IndexColName, IndexColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, MatTypeColName, MatTypeColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, NameColName, NameColName, Fill);
            tool.AddTextBoxColumns(dgv, CodeColName, CodeColName, Fill);
            tool.AddTextBoxColumns(dgv, ColorColName, ColorColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, MCColName, MCColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, PartWeightColName, PartWeightColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, RunnerWeightColName, RunnerWeightColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, MBColName, MBColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, StockColName, StockColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, EstimateColName, EstimateColName, DisplayedCells);
            //tool.AddTextBoxColumns(dgv, Forecast1ColName, Forecast1ColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, OutColName, OutColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, OsantColName, OsantColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Shot1ColName, Shot1ColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Shot2ColName, Shot2ColName, DisplayedCells);

            dgv.Columns[MCColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[PartWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[RunnerWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[MBColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[StockColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[EstimateColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[Forecast1ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[OutColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[OsantColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[Shot1ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[Shot2ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //UI setting
            dgv.Columns[MCColName].DefaultCellStyle.BackColor = Color.AliceBlue;

            dgv.Columns[MCColName].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            dgv.Columns[MCColName].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
            dgv.Columns[MCColName].Width = 40;

            dgv.Columns[PartWeightColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[PartWeightColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[RunnerWeightColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[RunnerWeightColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[StockColName].HeaderCell.Style.BackColor = Color.Pink;
            dgv.Columns[StockColName].DefaultCellStyle.BackColor = Color.Pink;
            dgv.Columns[StockColName].HeaderCell.Style.ForeColor = Color.Blue;
            dgv.Columns[StockColName].MinimumWidth = 70;

            dgv.Columns[EstimateColName].HeaderCell.Style.BackColor = Color.LightCyan;
            dgv.Columns[EstimateColName].DefaultCellStyle.BackColor = Color.LightCyan;

            //dgv.Columns[Forecast1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            //dgv.Columns[Forecast1ColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[OutColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[OutColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[OsantColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[OsantColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[Shot1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[Shot1ColName].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[Shot1ColName].HeaderCell.Style.ForeColor = Color.Red;

            dgv.Columns[Shot2ColName].HeaderCell.Style.BackColor = Color.LightGreen;
            dgv.Columns[Shot2ColName].DefaultCellStyle.BackColor = Color.LightGreen;
            dgv.Columns[Shot2ColName].HeaderCell.Style.ForeColor = Color.Red;
        }

        private void UIDesign()
        {
            //dgvForecastReport2.Columns["index"].HeaderCell.Style.BackColor = Color.Red;
            //dgvForecastReport2.Columns["index"].DefaultCellStyle.BackColor = Color.Red;
            //dgvForecastReport2.Columns["mc_ton"].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            //dgvForecastReport2.Columns["mc_ton"].Width = 40;
            //dgvForecastReport2.Columns["item_part_weight"].HeaderCell.Style.BackColor = Color.LightYellow;
            //dgvForecastReport2.Columns["item_runner_weight"].HeaderCell.Style.BackColor = Color.LightYellow;
            //dgvForecastReport2.Columns[Forecast1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            //dgvForecastReport2.Columns[OutColName].HeaderCell.Style.BackColor = Color.LightYellow;
            //dgvForecastReport2.Columns["oSant"].HeaderCell.Style.BackColor = Color.LightYellow;

            //dgvForecastReport2.Columns[Shot1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            //dgvForecastReport2.Columns[Shot1ColName].HeaderCell.Style.ForeColor = Color.Red;

            //dgvForecastReport2.Columns[Forecast2ColName].HeaderCell.Style.BackColor = Color.LightCyan;
            //dgvForecastReport2.Columns[Shot2ColName].HeaderCell.Style.BackColor = Color.LightCyan;
            //dgvForecastReport2.Columns[Shot2ColName].HeaderCell.Style.ForeColor = Color.Red;

            //dgvForecastReport2.Columns["forecast_Three"].HeaderCell.Style.BackColor = Color.PeachPuff;
          

            //dgvForecastReport2.Columns[Shot2ColName].HeaderCell.Style.ForeColor = Color.Red;

            //dgvForecastReport2.Columns["stock_qty"].HeaderCell.Style.BackColor = Color.Pink;
            //dgvForecastReport2.Columns["stock_qty"].HeaderCell.Style.ForeColor = Color.Blue;
        }

        private void emptyRowBackColorToBlack(int dgvType)
        {
            float shotOne = redAlertLevel;
            float shotTwo = redAlertLevel;

            string colorName = "Black";
            foreach (DataGridViewRow row in dgvForecastReport.Rows)
            {
                if(row.Cells[Shot1ColName].Value != null)
                {
                    shotOne = Convert.ToSingle(row.Cells[Shot1ColName].Value);
                }
                
                if(shotOne < redAlertLevel)
                {
                    row.Cells[Shot1ColName].Style.ForeColor = Color.Red;
                }
                else if(row.Cells[MBColName].Value != null)
                {
                    //row.Cells[Shot1ColName].Style.ForeColor = Color.Black;
                }

                if(dgvType == 1)
                {
                    if (row.Cells[Shot2ColName].Value != null)
                    {
                        shotTwo = Convert.ToSingle(row.Cells[Shot2ColName].Value);
                    }

                    if (shotTwo < redAlertLevel)
                    {
                        row.Cells[Shot2ColName].Style.ForeColor = Color.Red;
                    }
                    else if (row.Cells[MBColName].Value != null)
                    {
                        //row.Cells[Shot2ColName].Style.ForeColor = Color.Black;
                    }
                }

                if (row.Cells[NameColName].Value == null)
                {
                    row.Height = 3;
                    row.DefaultCellStyle.BackColor = Color.FromName(colorName);
                }
            }
        }

        private void changeBackColor(DataGridViewRow row, int rowIndex, bool type, int dgvType)//if type = true, change previous data back color, else change the new data
        {
            if (type)
            {
                string test = row.Cells[Forecast1ColName].Style.BackColor.ToKnownColor().ToString();

                if (test.Equals("0"))
                {
                    row.Cells[CodeColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells[NameColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells[Forecast1ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells[OutColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells[OsantColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                    if (Convert.ToSingle(row.Cells[Shot1ColName].Value) < 0)
                    {
                        row.Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                    }
                    else
                    {
                        row.Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                    }

                  

                    if (dgvType == 1)
                    {
                        row.Cells[Forecast2ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                        row.Cells[Forecast3ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                        if (Convert.ToSingle(row.Cells[Shot2ColName].Value) < 0)
                        {
                            row.Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                        }
                        else
                        {
                            row.Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                        }
                    }
                    
                }
                else
                {
                    colorName = test;
                    //MessageBox.Show(row.Cells[Forecast1ColName].Style.BackColor.ToString());
                }
            }
            else
            {
                dgvForecastReport.Rows[rowIndex].Cells[CodeColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells[NameColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells[Forecast1ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells[OutColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells[OsantColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                if (Convert.ToSingle(dgvForecastReport.Rows[rowIndex].Cells[Shot1ColName].Value) < 0)
                {
                    dgvForecastReport.Rows[rowIndex].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                }
                else
                {
                    dgvForecastReport.Rows[rowIndex].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                }

                //if(dgvType == 1)
                //{
                //    dgvForecastReport.Rows[rowIndex].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                //    dgvForecastReport.Rows[rowIndex].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                //    if (Convert.ToSingle(dgvForecastReport.Rows[rowIndex].Cells[Shot2ColName].Value) < 0)
                //    {
                //        dgvForecastReport.Rows[rowIndex].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                //    }
                //    else
                //    {
                //        dgvForecastReport.Rows[rowIndex].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                //    }
                //}

                //if (colorOrder == 2)
                //{
                //    colorOrder = 0;
                //}
                //else
                //{
                //    colorOrder++;
                //}
                //colorName = ((color)colorOrder).ToString();
            }
        }
        #endregion

        #region Forecast Data Processing

        private float get6MonthsMaxOut(string itemCode, string customer)
        {
            float MaxOut = 0, tmp = 0;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            for(int i = 0; i < 6; i++)
            {
                tmp = 0;
                
                if(currentMonth == 1)
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

        private void refreshData(int dgvType)
        {
            bool result = dalForecast.Delete();//database forecast table reset

            if (!result)
            {
                MessageBox.Show("Failed to reset forecast data");
            }
            else
            {
                colorOrder = 0;
                insertForecastData();

                string sort = cmbSort.Text;
                string order = cmbOrder.Text;

              
                int type = 0;

                if (cmbType.SelectedIndex > 0)
                {
                    type = cmbType.SelectedIndex;
                }

                DataTable dt = dalForecast.Select(sort, order);//load sorted data from forecast table
                loadForecastList(dt, type, dgvType); 
            }
        }

        private DataTable RemoveMouldData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i][dalItem.ItemCat].ToString().Equals("Mould"))
                    {
                        dt.Rows[i].Delete();
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private void insertForecastData()
        {
            string custName = cmbCust.Text;
            int dgvType = -1;
            if (custName.Equals(tool.getCustName(1)))//PMMA
            {
                dgvType = 1;
            }
            else
            {
                dgvType = 2;
            }

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);//load customer's item list

                dt = RemoveMouldData(dt);

                if (dt.Rows.Count < 1)
                {   
                    //MessageBox.Show("no data under this record.");
                }
                else
                {
                    float outStock = 0;//item stock out qty(in current month)
                    float forecastOne = 0;//current month forecast number
                    float forecastTwo = 0;//next month forecast number
                    float forecastThree = 0;//next next month forecast number
                    float readyStock = 0;//item ready stock
                    float outSant = 0;//forecast - outStock
                    float shotOne = 0;//still left how many item after current month
                    float shotTwo = 0;//still left how many item after next month
                    string itemCode = "";
                    string month = "";
                    int forecastIndex = 1;
                    string start = dtpStart.Value.ToString("yyyy/MM/dd");
                    string end = dtpEnd.Value.ToString("yyyy/MM/dd");

                    foreach (DataRow item in dt.Rows)//go into customer's item list
                    {
                        itemCode = item["item_code"].ToString();

                        if(dgvType == 1)
                        {
                            forecastOne = Convert.ToSingle(item["forecast_one"]);
                            forecastTwo = Convert.ToSingle(item["forecast_two"]);
                        }
                        else if(dgvType == 2)
                        {
                            forecastOne = get6MonthsMaxOut(itemCode, custName);
                            forecastTwo = forecastOne;
                        }
                        
                        forecastThree = Convert.ToSingle(item["forecast_three"]);
                        month = item["forecast_current_month"].ToString();

                        //DataTable dt3 = daltrfHist.outSearch(cmbCust.Text, getMonthValue(month), itemCode);//load item out to customer record in current month
                        DataTable dt3 = daltrfHist.rangeItemToCustomerSearch(cmbCust.Text, start, end, itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {
                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                //To-Do: need to check if from factory

                                if(outRecord["trf_result"].ToString().Equals("Passed")) 
                                {
                                    outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
                                }
                                
                            }
                        }

                        readyStock = Convert.ToSingle(dalItem.getStockQty(itemCode));

                        outSant = forecastOne - outStock;

                        if (outSant > 0)
                        {
                            shotOne = readyStock - outSant;
                        }
                        else
                        {
                            shotOne = readyStock;
                        }

                        shotTwo = shotOne - forecastTwo;

                        uForecast.forecast_no = forecastIndex.ToString();
                        uForecast.item_code = itemCode;
                        uForecast.forecast_ready_stock = readyStock;
                        uForecast.forecast_current_month = month;
                        uForecast.forecast_one = forecastOne;
                        uForecast.forecast_two = forecastTwo;
                        uForecast.forecast_three = forecastThree;
                        uForecast.forecast_out_stock = outStock;
                        uForecast.forecast_osant = outSant;
                        uForecast.forecast_shot_one = shotOne;
                        uForecast.forecast_shot_two = shotTwo;
                        uForecast.forecast_updtd_date = DateTime.Now;
                        uForecast.forecast_updtd_by = MainDashboard.USER_ID;

                        bool result = dalForecast.Insert(uForecast);
                        if (!result)
                        {
                            MessageBox.Show("failed to insert forecast data");
                            return;
                        }
                        else
                        {
                            forecastIndex++;
                        }
                    }
                }
            }
            else
            {
                dgvForecastReport.DataSource = null;
            }
            dgvForecastReport.ClearSelection();
        }

        private bool ifRepeat(string itemCode, int n, string fcast1, string fcast2, string fcast3, string outStock, float shot1, float shot2, int dgvType)
        {
            bool result = false;

            foreach (DataGridViewRow row in dgvForecastReport.Rows)
            {
                if (row.Cells[CodeColName].Value == null)
                {
                    row.Cells[CodeColName].Value = "";
                }

                if (row.Cells[CodeColName].Value.ToString().Equals(itemCode) && row.Index < n)
                {
                    float headFcast1 = 0;
                    float headOutStock = Convert.ToSingle(row.Cells[OutColName].Value);
                    float headShot1 = Convert.ToSingle(row.Cells[Shot1ColName].Value);

                    float childFcast1 = Convert.ToSingle(fcast1);
                    float childFcast2 = Convert.ToSingle(fcast2);
                    float childFcast3 = Convert.ToSingle(fcast3);
                    float childOutStock = Convert.ToSingle(outStock);

                    if (shot1 < 0)
                    {
                        headShot1 += shot1;
                    }

                    if(dgvType == 1)
                    {
                        headFcast1 = Convert.ToSingle(row.Cells[Forecast1ColName].Value);
                        float headFcast2 = Convert.ToSingle(row.Cells[Forecast2ColName].Value);
                        float headFcast3 = Convert.ToSingle(row.Cells[Forecast3ColName].Value);
                        float headShot2 = Convert.ToSingle(row.Cells[Shot2ColName].Value);
                        if (shot2 < 0)
                        {
                            headShot2 += shot2;
                        }
                        headFcast2 += childFcast2;
                        headFcast3 += childFcast3;

                        row.Cells[Forecast2ColName].Value = headFcast2.ToString();
                        row.Cells[Forecast3ColName].Value = headFcast3.ToString();
                        row.Cells[Shot2ColName].Value = headShot2.ToString();
                    }
                    else
                    {
                        headFcast1 = Convert.ToSingle(row.Cells[EstimateColName].Value);
                    }
                   
                    headFcast1 += childFcast1;
                    
                    headOutStock += childOutStock;

                    float headOutSant = headFcast1 - headOutStock;

                    row.Cells[Forecast1ColName].Value = headFcast1.ToString();
                    
                    row.Cells[OutColName].Value = headOutStock.ToString();
                    row.Cells[Shot1ColName].Value = headShot1.ToString();
                    
                    row.Cells[OsantColName].Value = headOutSant.ToString();

                    string checkColor = row.Cells[Forecast1ColName].Style.BackColor.ToKnownColor().ToString();

                    if(checkColor.Equals("0"))
                    {
                        colorName = ((color)colorOrder).ToString();

                        if (colorOrder == 18)
                        {
                            colorOrder = 0;
                        }
                        else
                        {
                            colorOrder++;
                        }
                    }
                    else
                    {
                        colorName = row.Cells[Forecast1ColName].Style.BackColor.ToString();
                    }

                    changeBackColor(row, 0, true, dgvType);
                    result = true;
                   
                    

                    return true;
                }               
            }

            return result;
        }

        private void checkChild(string itemCode, string month, string forecastOne, string forecastTwo, string forecastThree, float shotOne, float shotTwo, float outStock,string no, int dgvType)
        {
            int alphbetTest = 65;
            string parentItemCode = itemCode;
            DataGridView dgv = dgvForecastReport;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    float partf = 0;
                    float runnerf = 0;
                    float childShotOne = 0;
                    float childShotTwo = 0;
                    
                    DataTable dtItem = dalItem.codeSearch(Join["join_child_code"].ToString());

                    if (dtItem.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtItem.Rows)
                        {
                            itemCode = item["item_code"].ToString();

                            bool subMatChecking = false;

                            if(cbSubMat.Checked)
                            {
                                subMatChecking = true;
                            }
                            else
                            {
                                if(dalItem.getCatName(itemCode).Equals("Part"))
                                {
                                    subMatChecking = true;
                                }
                                else
                                {
                                    subMatChecking = false;
                                }
                            }
                            if(subMatChecking)//dalItem.getCatName(itemCode).Equals("Part")
                            {
                                int n = dgvForecastReport.Rows.Add();
                                dgv.Rows[n].Cells[CodeColName].Value = Join["join_child_code"].ToString();
                             
                                if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
                                {
                                    partf = Convert.ToSingle(item["item_part_weight"]);
                                }
                                if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
                                {
                                    runnerf = Convert.ToSingle(item["item_runner_weight"]);
                                }
  
                                dgv.Rows[n].Cells[IndexColName].Value = no + "(" + (char)alphbetTest + ")";
                                dgv.Rows[n].Cells[CodeColName].Value = itemCode;
                                dgv.Rows[n].Cells[NameColName].Value = item["item_name"].ToString();
                                dgv.Rows[n].Cells[MatTypeColName].Value = dalItem.getMaterialName(item["item_material"].ToString());
                                dgv.Rows[n].Cells[MBColName].Value = item["item_mb"].ToString();
                                dgv.Rows[n].Cells[ColorColName].Value = item["item_color"].ToString();
                                dgv.Rows[n].Cells[PartWeightColName].Value = partf.ToString("0.00");
                                dgv.Rows[n].Cells[RunnerWeightColName].Value = runnerf.ToString("0.00");

                                if (ifRepeat(itemCode, n, forecastOne, forecastTwo, forecastThree, outStock.ToString(), shotOne, shotTwo, dgvType))
                                {
                                    changeBackColor(null, n, false, dgvType);
                                }
                                else
                                {

                                    dgv.Rows[n].Cells[OutColName].Value = outStock.ToString();

                                    float readyStock = Convert.ToSingle(dalItem.getStockQty(itemCode));
                                    dgv.Rows[n].Cells[StockColName].Style.Font = new System.Drawing.Font(dgv.Font, FontStyle.Regular);
                                    dgv.Rows[n].Cells[StockColName].Value = readyStock.ToString();

                                    float oSant = Convert.ToSingle(forecastOne) - outStock;
                                    dgv.Rows[n].Cells[OsantColName].Value = oSant.ToString();

                                    if (shotOne >= 0)
                                    {
                                        childShotOne = readyStock;
                                    }
                                    else
                                    {
                                        childShotOne = readyStock + shotOne;
                                    }

                                    if (childShotOne < 0)
                                    {
                                        dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = dgv.Rows[n].Cells[Shot1ColName].Style.BackColor };

                                    }
                                    else
                                    {
                                        dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = dgv.Rows[n].Cells[Shot1ColName].Style.BackColor };

                                    }

                                    dgv.Rows[n].Cells[Shot1ColName].Value = childShotOne.ToString();

                                    if (shotTwo >= 0)
                                    {
                                        childShotTwo = childShotOne;
                                    }
                                    else if (shotOne > 0)
                                    {
                                        childShotTwo = childShotOne + shotTwo;
                                    }
                                    else
                                    {
                                        childShotTwo = childShotOne - Convert.ToSingle(forecastTwo);
                                    }

                                    if (childShotTwo < 0)
                                    {
                                        dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = dgv.Rows[n].Cells[Shot2ColName].Style.BackColor };

                                    }
                                    else
                                    {
                                        dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = dgv.Rows[n].Cells[Shot2ColName].Style.BackColor };

                                    }

                                    dgv.Rows[n].Cells[Shot2ColName].Value = childShotTwo.ToString();

                                    if (dgvType == 1)
                                    {
                                        dgv.Rows[n].Cells[Forecast1ColName].Value = forecastOne;
                                        dgv.Rows[n].Cells[Forecast2ColName].Value = forecastTwo;
                                        dgv.Rows[n].Cells[Forecast3ColName].Value = forecastThree;
                                    }
                                }

                                if (ifGotChild(itemCode))
                                {
                                    if(dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                                    {
                                        dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                        dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                    }
                                    else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                                    {
                                        dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                        dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                    }
                                    else if (dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))
                                    {
                                        dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                        dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                    }


                                    checkChild(itemCode, month, forecastOne, forecastTwo, forecastThree, childShotOne, childShotTwo, Convert.ToSingle(outStock), no + "(" + (char)alphbetTest + ")", dgvType);
                                }

                                alphbetTest++;
                            }
                            
                        }
                        
                    }

                    
                }
            }
        }

        private void loadForecastList(DataTable dataTable, int type, int dgvType)
        {
            if(dgvType == 1)
            {
                createPMMADGV();
            }
            else
            {
                createDGV();
            }
            
            DataGridView dgv = dgvForecastReport;
            DataTable dt = dataTable;
            

            if (dt.Rows.Count <= 0)
            {
                //MessageBox.Show("no data under this record.");
                gotData = false;
            }
            else
            {
                if(cmbSort.SelectedIndex <= 0)
                {
                    dt.DefaultView.Sort = "item_name ASC";
                    dt = dt.DefaultView.ToTable();
                }

                dgv.Rows.Clear();
                gotData = true;
                //load single data
                indexNo = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item["item_code"].ToString();

                    if (!ifGotChild(itemCode) && type != 2)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[IndexColName].Value = indexNo.ToString();
                        singlePartEndIndex = indexNo.ToString();
                        indexNo++;
                        dgv.Rows[n].Cells[CodeColName].Value = itemCode;
                        dgv.Rows[n].Cells[NameColName].Value = item["item_name"].ToString();
                        dgv.Rows[n].Cells[ColorColName].Value = item["item_color"].ToString();
                        dgv.Rows[n].Cells[MatTypeColName].Value = dalItem.getMaterialName(item["item_material"].ToString());
                        dgv.Rows[n].Cells[MBColName].Value = item["item_mb"].ToString();
                        dgv.Rows[n].Cells[PartWeightColName].Value = Convert.ToSingle(item["item_part_weight"]).ToString("0.00");
                        dgv.Rows[n].Cells[RunnerWeightColName].Value = Convert.ToSingle(item["item_runner_weight"]).ToString("0.00"); 

                        string month = item["forecast_current_month"].ToString();

                        dgv.Rows[n].Cells[OutColName].Value = item["forecast_out_stock"].ToString();
                        dgv.Rows[n].Cells[StockColName].Value = item["forecast_ready_stock"].ToString();
                        dgv.Rows[n].Cells[OsantColName].Value = item["forecast_osant"].ToString();

                        float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
                        float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);

                        dgv.Rows[n].Cells[Shot1ColName].Value = shotOne.ToString();
                        dgv.Rows[n].Cells[Shot2ColName].Value = shotTwo.ToString();

                        dgv.Columns[Shot1ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                        dgv.Columns[Shot2ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 2);

                        if (shotOne < redAlertLevel)
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }
                        
                        if (shotTwo < redAlertLevel)
                        {
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                        {
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }

                        if (dgvType == 1)
                        {
                            dgv.Columns[Forecast1ColName].HeaderText = "F/cast " + getShortMonth(month, 1);
                            dgv.Columns[Forecast2ColName].HeaderText = "F/cast " + getShortMonth(month, 2);
                            dgv.Columns[Forecast3ColName].HeaderText = "F/cast " + getShortMonth(month, 3);

                            dgv.Rows[n].Cells[Forecast1ColName].Value = item["forecast_one"].ToString();
                            dgv.Rows[n].Cells[Forecast2ColName].Value = item["forecast_two"].ToString();
                            dgv.Rows[n].Cells[Forecast3ColName].Value = item["forecast_three"].ToString();  
                        } 
                        else
                        {
                            //show estimate number
                            dgv.Rows[n].Cells[EstimateColName].Value = get6MonthsMaxOut(itemCode, cmbCust.Text);
                        }
                    }
                }

                //if (type != 1)
                //{
                //    int n = dgv.Rows.Add();
                //}

                //load parent and child data
                indexNo = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item["item_code"].ToString();

                    if (ifGotChild(itemCode) && type != 1)
                    {
                        int n = dgv.Rows.Add();

                        float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
                        float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);
                        string forecastOne = item["forecast_one"].ToString();
                        string forecastTwo = item["forecast_two"].ToString();
                        string forecastThree = item["forecast_three"].ToString();
                        string month = item["forecast_current_month"].ToString();
                        string outStock = item["forecast_out_stock"].ToString();

                        dgv.Rows[n].Cells[IndexColName].Value = indexNo.ToString();

                        if(dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                            dgv.Rows[n].Cells[StockColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[OutColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[OsantColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };


                        }
                        else if (dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                            dgv.Rows[n].Cells[StockColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[OutColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[OsantColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        }
                        else
                        {
                            dgv.Rows[n].Cells[MatTypeColName].Value = dalItem.getMaterialName(item["item_material"].ToString());
                            dgv.Rows[n].Cells[MBColName].Value = item["item_mb"].ToString();
                            dgv.Rows[n].Cells[PartWeightColName].Value = Convert.ToSingle(item["item_part_weight"]).ToString("0.00");
                            dgv.Rows[n].Cells[RunnerWeightColName].Value = Convert.ToSingle(item["item_runner_weight"]).ToString("0.00");
                            dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells[StockColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[OutColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[OsantColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                        }

                        dgv.Rows[n].Cells[IndexColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                        dgv.Rows[n].Cells[CodeColName].Value = itemCode;
                        dgv.Rows[n].Cells[NameColName].Value = item["item_name"].ToString();
                        dgv.Rows[n].Cells[ColorColName].Value = item["item_color"].ToString();

                        dgv.Rows[n].Cells[StockColName].Value = item["forecast_ready_stock"].ToString();


                        dgv.Rows[n].Cells[OutColName].Value = outStock;

                        dgv.Rows[n].Cells[OsantColName].Value = item["forecast_osant"].ToString();

                        dgv.Columns[Shot1ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                        dgv.Rows[n].Cells[Shot1ColName].Value = item["forecast_shot_one"].ToString();

                        if (shotOne < 0)
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        }
                        else
                        {
                            if (dalItem.checkIfAssembly(itemCode))//assembly part show blue color, else show green color
                            {
                                dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                            }
                            else
                            {
                                dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                            }
                        }

                        dgv.Columns[Shot2ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 2);

                        if (shotTwo < 0)
                        {
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        }
                        else
                        {
                            if (dalItem.checkIfAssembly(itemCode))//assembly part show blue color, else show green color
                            {
                                dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                            }
                            else
                            {
                                dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                            }
                        }
                        dgv.Rows[n].Cells[Shot2ColName].Value = item["forecast_shot_two"].ToString();


                        if (dgvType == 1)
                        {
                            dgv.Columns[Forecast1ColName].HeaderText = "F/cast " + getShortMonth(month, 1);
                            dgv.Columns[Forecast2ColName].HeaderText = "F/cast " + getShortMonth(month, 2);
                            dgv.Columns[Forecast3ColName].HeaderText = "F/cast " + getShortMonth(month, 3);

                            if (dalItem.checkIfAssembly(itemCode))
                            {
                                dgv.Rows[n].Cells[Forecast1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                                dgv.Rows[n].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                                dgv.Rows[n].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            }
                            else
                            {
                                dgv.Rows[n].Cells[Forecast1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                                dgv.Rows[n].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                                dgv.Rows[n].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            }

                            dgv.Rows[n].Cells[Forecast1ColName].Value = forecastOne;
                            dgv.Rows[n].Cells[Forecast2ColName].Value = forecastTwo;
                            dgv.Rows[n].Cells[Forecast3ColName].Value = forecastThree;
                        }
                        else
                        {
                            dgv.Rows[n].Cells[EstimateColName].Value = get6MonthsMaxOut(itemCode,cmbCust.Text);
                        }

                        checkChild(itemCode, month, forecastOne, forecastTwo, forecastThree, shotOne, shotTwo, Convert.ToSingle(outStock),indexNo.ToString(), dgvType);
                        indexNo++;
                        n = dgv.Rows.Add();
                    }
                    //alphbet = 65;
                }
            }
            dgv.ClearSelection();
        }

        private void searchForecastList()
        {
            int type = 0;
            string sort = cmbSort.Text;
            string order = cmbOrder.Text;
            string keywords = txtSearch.Text;
            DataTable dt;
            

            if (cmbType.SelectedIndex > 0)
            {
                type = cmbType.SelectedIndex;
            }

            if(string.IsNullOrEmpty(keywords))
            {
                dt = dalForecast.Select(sort, order);
            }
            else
            {
                dt = dalForecast.testSearch(keywords);
                dt = RemoveDuplicates(dt);
            }

            string custName = tool.getCustName(1);
            int dgvType = 0;
            if (cmbCust.Text.Equals(custName))//PMMA
            {
                dgvType = 1;
            }
            else
            {
                dgvType = 2;
            }

            loadForecastList(dt, type, dgvType);
        }

        /* To eliminate Duplicate rows */
        private DataTable RemoveDuplicates(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        break;
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (dt.Rows[i]["item_code"].ToString() == dt.Rows[j]["item_code"].ToString())
                        {
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();
                
            }
            return dt;
        }

        #endregion

        #region Function: Check forecast data/Search

        private void showForecastData()
        {
            gotData = false;
            string custName = tool.getCustName(1);
            int type = 0;
            if (cmbCust.Text.Equals(custName))//PMMA
            {
                type = 1;
            }
            else
            {
                type = 2;
            }

            colorOrder = 0;
            btnCheck.Enabled = false;
            refreshData(type);
            emptyRowBackColorToBlack(type);
            btnCheck.Enabled = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //dgvForecastReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            foreach (DataGridViewColumn c in dgvForecastReport.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            ((ISupportInitialize)dgvForecastReport).BeginInit();
            showForecastData();
            ((ISupportInitialize)dgvForecastReport).EndInit();


            foreach (DataGridViewColumn dgvc in dgvForecastReport.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvForecastReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Cursor = Cursors.Arrow; // change cursor to normal type



            //try
            //{
            //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //    showForecastData();

            //    foreach (DataGridViewColumn dgvc in dgvForecastReport.Columns)
            //    {
            //        dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            //    }

            //    Cursor = Cursors.Arrow; // change cursor to normal type
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        #endregion

        #region Selected Index Changed/ Mouse Click

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(cmbSort.Text) || cmbSort.SelectedIndex <= 0)
            {
                cmbOrder.DataSource = null;
            }
            else
            {
                string asc = "Ascending";
                string desc = "Descending";
                DataTable dt = new DataTable();
                dt.Columns.Add("order");
                dt.Rows.Add(asc);
                dt.Rows.Add(desc);
                cmbOrder.DataSource = dt;
                cmbOrder.DisplayMember = "order";
                //cmbOrder.SelectedIndex = 0;
            }
            
        }

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;

            string custName = tool.getCustName(1);

            if(cmbCust.Text.Equals(custName))//PMMA
            {
                //create daikin dgv
                //dgvForecastReport2.Hide();
                createPMMADGV();
                dgvForecastReport.Show();
            }
            else
            {
                //create normal dgv
                //dgvForecastReport2.Hide();
                createDGV();
                dgvForecastReport.Show();
            }
        }

        private void frmForecastReport_Click(object sender, EventArgs e)
        {
            dgvForecastReport.ClearSelection();
        }

        private void txtAlertLevel_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlertLevel.Text))
            {
                redAlertLevel = 0;
            }
            else
            {
                redAlertLevel = Convert.ToInt32(txtAlertLevel.Text);
            }
        }

        private void txtAlertLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgvForecastReport_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvForecastReport.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvForecastReport.ClearSelection();
            }
        }

        #endregion

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
            fileName = "ForecastReport(" + cmbCust.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            bgWorker.DoWork += BackgroundWorkerDoWork;
            bgWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            try
            {
                int type = 0;
                string custName = tool.getCustName(1);
                if (cmbCust.Text.Equals(custName))//PMMA
                {
                    type = 0;
                }
                else
                {
                    type = 2;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                string path = @"D:\StockAssistant\Document\ForecastReport";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Visible = true;
                    bgWorker.ReportProgress(10);
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard();

                    object misValue = System.Reflection.Missing.Value;
                    Excel.Application xlexcel = new Excel.Application();

                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) +" OUT FROM:"+dtpStart.Text+" TO:"+dtpEnd.Text;

                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;

                    xlWorkSheet.PageSetup.LeftMargin = 0.8;
                    xlWorkSheet.PageSetup.RightMargin = 0.8;
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;
                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
                    //xlWorkSheet.PageSetup.TopMargin = 1.6;


                    // Paste clipboard results to worksheet range
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    Range rng;
                    if (type == 2)
                    {
                        rng = xlWorkSheet.get_Range("A:P").Cells;
                    }
                    else
                    {
                        rng = xlWorkSheet.get_Range("A:Q").Cells;
                    }

                    rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    rng.RowHeight = 16;
                    rng.EntireColumn.AutoFit();

                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Font.Size = 12;
                    tRange.Font.Name = "Calibri";
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;

                    //top row 
                    Range topRow;
                    if (type == 2)
                    {
                        topRow = xlWorkSheet.get_Range("a1:p1").Cells;
                    }
                    else
                    {
                        topRow = xlWorkSheet.get_Range("a1:q1").Cells;
                    }
                    topRow.RowHeight = 25;

                    topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    topRow.BorderAround2(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                    //first column: index 
                    Range indexCol = xlWorkSheet.Columns[1];
                    indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;

                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                    for (int i = 0; i <= dgvForecastReport.RowCount - 2; i++)
                    {
                        for (int j = 0; j <= dgvForecastReport.ColumnCount - 1; j++)
                        {
                            Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);

                                header = (Range)xlWorkSheet.Cells[1, 10];
                                header.Font.Color = Color.Blue;

                                header = (Range)xlWorkSheet.Cells[1, 14];
                                header.Font.Color = Color.Red;

                                header = (Range)xlWorkSheet.Cells[1, 16];
                                header.Font.Color = Color.Red;


                            }

                            if (dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                            {
                                range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                if (i == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                }
                            }
                            else if (dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
                            {
                                range.Rows.RowHeight = 3;
                                range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);
                                if (i == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);
                                }
                            }
                            else
                            {
                                range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);

                                if (i == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);
                                }
                            }
                            range.Font.Color = dgvForecastReport.Rows[i].Cells[j].Style.ForeColor;
                            if (dgvForecastReport.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 2, 2];
                                header.Font.Underline = true;

                                header = (Range)xlWorkSheet.Cells[i + 2, 3];
                                header.Font.Underline = true;
                            }

                        }

                        Int32 percentage = ((i+1) * 100) / (dgvForecastReport.RowCount - 2);
                        if(percentage >= 100)
                        {
                            percentage = 100;
                        }
                        bgWorker.ReportProgress(percentage);
                    }

                    bgWorker.ReportProgress(100);
                    System.Threading.Thread.Sleep(1000);

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
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                progressBar1.Visible = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void test(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\ForecastReport";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

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

                    insertOneDataToSheet(path, sfd.FileName);
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

        private void btnExportAllToExcel_Click(object sender, EventArgs e)
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

        private void insertOneDataToSheet(string path, string fileName)
        {
            string custName = tool.getCustName(1);
            string sheetNameAdjust = " Non-Assembly";
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

            for (int i = 1; i <= 2; i++)
            {
                cmbType.SelectedIndex = i;
                showForecastData();

                if (gotData)//if datagridview have data
                {
                    Worksheet xlWorkSheet = null;

                    int count = g_Workbook.Worksheets.Count;

                    xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                            g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                    xlWorkSheet.Name = cmbCust.Text+sheetNameAdjust;

                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " OUT FROM:" + dtpStart.Text + " TO:" + dtpEnd.Text;

                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;

                    xlWorkSheet.PageSetup.LeftMargin = 0.8;
                    xlWorkSheet.PageSetup.RightMargin = 0.8;
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;
                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
                    //xlWorkSheet.PageSetup.TopMargin = 1.6;


                    // Paste clipboard results to worksheet range
                    copyAlltoClipboard();
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    Range rng;
                    if (cmbCust.Text.Equals(custName))
                    {
                        rng = xlWorkSheet.get_Range("A:Q").Cells;
                    }
                    else
                    {
                        rng = xlWorkSheet.get_Range("A:P").Cells;
                    }


                    rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    rng.RowHeight = 16;
                    rng.EntireColumn.AutoFit();

                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;

                    //top row 
                    Range topRow;
                    if (cmbCust.Text.Equals(custName))
                    {
                        topRow = xlWorkSheet.get_Range("a1:q1").Cells;
                    }
                    else
                    {
                        topRow = xlWorkSheet.get_Range("a1:p1").Cells;
                    }
                    topRow.RowHeight = 25;

                    topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    topRow.BorderAround2(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                    //first column: index 
                    Range indexCol = xlWorkSheet.Columns[1];
                    indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;

                    for (int a = 0; a <= dgvForecastReport.RowCount - 2; a++)
                    {
                        for (int j = 0; j <= dgvForecastReport.ColumnCount - 1; j++)
                        {
                            Range range = (Range)xlWorkSheet.Cells[a + 2, j + 1];

                            if (a == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);

                                header = (Range)xlWorkSheet.Cells[1, 10];
                                header.Font.Color = Color.Blue;

                                header = (Range)xlWorkSheet.Cells[1, 14];
                                header.Font.Color = Color.Red;

                                header = (Range)xlWorkSheet.Cells[1, 16];
                                header.Font.Color = Color.Red;


                            }

                            if (dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                            {
                                range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                if (a == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                }
                            }
                            else if (dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor == Color.Black)
                            {
                                range.Rows.RowHeight = 3;
                                range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
                                if (a == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
                                }
                            }
                            else
                            {
                                range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);

                                if (a == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
                                }
                            }
                            range.Font.Color = dgvForecastReport.Rows[a].Cells[j].Style.ForeColor;
                            if (dgvForecastReport.Rows[a].Cells[j].Style.ForeColor == Color.Blue)
                            {
                                Range header = (Range)xlWorkSheet.Cells[a + 2, 2];
                                header.Font.Underline = true;

                                header = (Range)xlWorkSheet.Cells[a + 2, 3];
                                header.Font.Underline = true;
                            }

                        }
                    }

                    sheetNameAdjust = " Assembly & Other";
                    releaseObject(xlWorkSheet);
                    Clipboard.Clear();
                    dgvForecastReport.ClearSelection();
                }

            }
            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void insertAllDataToSheet(string path, string fileName)
        {
            string custName = tool.getCustName(1);
           
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

            for (int i = 0; i <= cmbCust.Items.Count - 1; i++)
            {
                cmbCust.SelectedIndex = i;
                showForecastData();
                // MessageBox.Show(cmbCust.Text + " rowcount:" + dgvForecastReport.RowCount + " columncount:" + dgvForecastReport.ColumnCount);
                if (gotData)//if datagridview have data
                {
                    Worksheet xlWorkSheet = null;

                    int count = g_Workbook.Worksheets.Count;

                    xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                            g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                    xlWorkSheet.Name = cmbCust.Text;

                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " OUT FROM:" + dtpStart.Text + " TO:" + dtpEnd.Text;

                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;

                    xlWorkSheet.PageSetup.LeftMargin = 0.8;
                    xlWorkSheet.PageSetup.RightMargin = 0.8;
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;
                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
                    //xlWorkSheet.PageSetup.TopMargin = 1.6;


                    // Paste clipboard results to worksheet range
                    copyAlltoClipboard();
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    Range rng;
                    if (cmbCust.Text.Equals(custName))
                    {
                        rng = xlWorkSheet.get_Range("A:Q").Cells;
                    }
                    else
                    {
                        rng = xlWorkSheet.get_Range("A:P").Cells;
                    }


                    rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    rng.RowHeight = 16;
                    rng.EntireColumn.AutoFit();

                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;

                    //top row 
                    Range topRow;
                    if (cmbCust.Text.Equals(custName))
                    {
                        topRow = xlWorkSheet.get_Range("a1:q1").Cells;
                    }
                    else
                    {
                        topRow = xlWorkSheet.get_Range("a1:p1").Cells;
                    }
                    topRow.RowHeight = 25;

                    topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    topRow.BorderAround2(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                    //first column: index 
                    Range indexCol = xlWorkSheet.Columns[1];
                    indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;

                    for (int a = 0; a <= dgvForecastReport.RowCount - 2; a++)
                    {
                        for (int j = 0; j <= dgvForecastReport.ColumnCount - 1; j++)
                        {
                            Range range = (Range)xlWorkSheet.Cells[a + 2, j + 1];

                            if (a == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);

                                header = (Range)xlWorkSheet.Cells[1, 10];
                                header.Font.Color = Color.Blue;

                                header = (Range)xlWorkSheet.Cells[1, 14];
                                header.Font.Color = Color.Red;

                                header = (Range)xlWorkSheet.Cells[1, 16];
                                header.Font.Color = Color.Red;


                            }

                            if (dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                            {
                                range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                if (a == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                }
                            }
                            else if (dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor == Color.Black)
                            {
                                range.Rows.RowHeight = 3;
                                range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
                                if (a == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
                                }
                            }
                            else
                            {
                                range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);

                                if (a == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
                                }
                            }
                            range.Font.Color = dgvForecastReport.Rows[a].Cells[j].Style.ForeColor;
                            if (dgvForecastReport.Rows[a].Cells[j].Style.ForeColor == Color.Blue)
                            {
                                Range header = (Range)xlWorkSheet.Cells[a + 2, 2];
                                header.Font.Underline = true;

                                header = (Range)xlWorkSheet.Cells[a + 2, 3];
                                header.Font.Underline = true;
                            }

                        }
                    }

                    releaseObject(xlWorkSheet);
                    Clipboard.Clear();
                    dgvForecastReport.ClearSelection();
                }

            }
            //g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            Cursor = Cursors.Arrow; // change cursor to normal type
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

        #endregion

        #region backup export one list to excel
        //private string setFileName()
        //{
        //    string fileName = "Test.xls";

        //    DateTime currentDate = DateTime.Now;
        //    fileName = "ForecastReport(" + cmbCust.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
        //    return fileName;
        //}

        //private void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    int type = 0;
        //    string custName = tool.getCustName(1);
        //    if (cmbCust.Text.Equals(custName))//PMMA
        //    {
        //        type = 0;
        //    }
        //    else
        //    {
        //        type = 2;
        //    }

        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Filter = "Excel Documents (*.xls)|*.xls";
        //    sfd.FileName = setFileName();
        //    if (sfd.ShowDialog() == DialogResult.OK)
        //    {
        //        tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

        //        // Copy DataGridView results to clipboard
        //        copyAlltoClipboard();

        //        object misValue = System.Reflection.Missing.Value;
        //        Excel.Application xlexcel = new Excel.Application();

        //        xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
        //        Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
        //        Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //        xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
        //        xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";
        //        xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
        //        xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

        //        xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
        //        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
        //        xlWorkSheet.PageSetup.Zoom = false;

        //        xlWorkSheet.PageSetup.LeftMargin = 0.8;
        //        xlWorkSheet.PageSetup.RightMargin = 0.8;
        //        xlWorkSheet.PageSetup.FitToPagesWide = 1;
        //        xlWorkSheet.PageSetup.FitToPagesTall = false;
        //        xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
        //        //xlWorkSheet.PageSetup.TopMargin = 1.6;


        //        // Paste clipboard results to worksheet range
        //        Range CR = (Range)xlWorkSheet.Cells[1, 1];
        //        CR.Select();
        //        xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

        //        Range rng;
        //        if (type == 2)
        //        {
        //            rng = xlWorkSheet.get_Range("A:P").Cells;
        //        }
        //        else
        //        {
        //            rng = xlWorkSheet.get_Range("A:Q").Cells;
        //        }

        //        rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
        //        rng.RowHeight = 16;
        //        rng.EntireColumn.AutoFit();

        //        Range tRange = xlWorkSheet.UsedRange;
        //        tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //        tRange.Borders.Weight = XlBorderWeight.xlThin;

        //        //top row 
        //        Range topRow;
        //        if (type == 2)
        //        {
        //            topRow = xlWorkSheet.get_Range("a1:p1").Cells;
        //        }
        //        else
        //        {
        //            topRow = xlWorkSheet.get_Range("a1:q1").Cells;
        //        }
        //        topRow.RowHeight = 25;

        //        topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
        //        topRow.BorderAround2(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

        //        //first column: index 
        //        Range indexCol = xlWorkSheet.Columns[1];
        //        indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;

        //        for (int i = 0; i <= dgvForecastReport.RowCount - 2; i++)
        //        {
        //            for (int j = 0; j <= dgvForecastReport.ColumnCount - 1; j++)
        //            {
        //                Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

        //                if (i == 0)
        //                {
        //                    Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);

        //                    header = (Range)xlWorkSheet.Cells[1, 10];
        //                    header.Font.Color = Color.Blue;

        //                    header = (Range)xlWorkSheet.Cells[1, 14];
        //                    header.Font.Color = Color.Red;

        //                    header = (Range)xlWorkSheet.Cells[1, 16];
        //                    header.Font.Color = Color.Red;


        //                }

        //                if (dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
        //                {
        //                    range.Interior.Color = ColorTranslator.ToOle(Color.White);
        //                    if (i == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(Color.White);
        //                    }
        //                }
        //                else if (dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
        //                {
        //                    range.Rows.RowHeight = 3;
        //                    range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);
        //                    if (i == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);
        //                    }
        //                }
        //                else
        //                {
        //                    range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);

        //                    if (i == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[i].Cells[j].InheritedStyle.BackColor);
        //                    }
        //                }
        //                range.Font.Color = dgvForecastReport.Rows[i].Cells[j].Style.ForeColor;
        //                if (dgvForecastReport.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
        //                {
        //                    Range header = (Range)xlWorkSheet.Cells[i + 2, 2];
        //                    header.Font.Underline = true;

        //                    header = (Range)xlWorkSheet.Cells[i + 2, 3];
        //                    header.Font.Underline = true;
        //                }

        //            }
        //        }

        //        // Save the excel file under the captured location from the SaveFileDialog
        //        xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //        xlexcel.DisplayAlerts = true;
        //        xlWorkBook.Close(true, misValue, misValue);
        //        xlexcel.Quit();

        //        releaseObject(xlWorkSheet);
        //        releaseObject(xlWorkBook);
        //        releaseObject(xlexcel);

        //        // Clear Clipboard and DataGridView selection
        //        Clipboard.Clear();
        //        dgvForecastReport.ClearSelection();

        //        // Open the newly saved excel file
        //        if (File.Exists(sfd.FileName))
        //            System.Diagnostics.Process.Start(sfd.FileName);
        //    }
        //}

        //private void btnExportAllToExcel_Click(object sender, EventArgs e)
        //{

        //    SaveFileDialog sfd = new SaveFileDialog();

        //    sfd.Filter = "Excel Documents (*.xls)|*.xls";
        //    sfd.FileName = "ForecastReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

        //    if (sfd.ShowDialog() == DialogResult.OK)
        //    {
        //        tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
        //        string path = Path.GetFullPath(sfd.FileName);
        //        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
        //        object misValue = System.Reflection.Missing.Value;
        //        Excel.Application xlexcel = new Excel.Application
        //        {
        //            PrintCommunication = false,
        //            ScreenUpdating = false,
        //            DisplayAlerts = false // Without this you will get two confirm overwrite prompts
        //        };
        //        Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

        //        //Save the excel file under the captured location from the SaveFileDialog
        //        xlWorkBook.SaveAs(sfd.FileName,
        //            XlFileFormat.xlWorkbookNormal,
        //            misValue, misValue, misValue, misValue,
        //            XlSaveAsAccessMode.xlExclusive,
        //            misValue, misValue, misValue, misValue, misValue);

        //        insertDataToSheet(path, sfd.FileName);
        //        xlexcel.DisplayAlerts = true;
        //        xlWorkBook.Close(true, misValue, misValue);
        //        xlexcel.Quit();

        //        releaseObject(xlWorkBook);
        //        releaseObject(xlexcel);

        //        // Clear Clipboard and DataGridView selection
        //        Clipboard.Clear();
        //        dgvForecastReport.ClearSelection();
        //    }
        //}

        //private void insertDataToSheet(string path, string fileName)
        //{
        //    string custName = tool.getCustName(1);
        //    int type = 0;
        //    if (cmbCust.Text.Equals(custName))//PMMA
        //    {
        //        type = 0;
        //    }
        //    else
        //    {
        //        type = 2;
        //    }

        //    Excel.Application excelApp = new Excel.Application
        //    {
        //        Visible = true
        //    };

        //    Workbook g_Workbook = excelApp.Workbooks.Open(
        //       path,
        //       Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //       Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //       Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //       Type.Missing, Type.Missing);

        //    object misValue = System.Reflection.Missing.Value;

        //    for (int i = 0; i <= cmbCust.Items.Count - 1; i++)
        //    {
        //        cmbCust.SelectedIndex = i;
        //        showForecastData();
        //        // MessageBox.Show(cmbCust.Text + " rowcount:" + dgvForecastReport.RowCount + " columncount:" + dgvForecastReport.ColumnCount);
        //        if (gotData)//if datagridview have data
        //        {
        //            Worksheet xlWorkSheet = null;

        //            int count = g_Workbook.Worksheets.Count;

        //            xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
        //                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

        //            xlWorkSheet.Name = cmbCust.Text;

        //            xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
        //            xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";
        //            xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
        //            xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

        //            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
        //            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
        //            xlWorkSheet.PageSetup.Zoom = false;

        //            xlWorkSheet.PageSetup.LeftMargin = 0.8;
        //            xlWorkSheet.PageSetup.RightMargin = 0.8;
        //            xlWorkSheet.PageSetup.FitToPagesWide = 1;
        //            xlWorkSheet.PageSetup.FitToPagesTall = false;
        //            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
        //            //xlWorkSheet.PageSetup.TopMargin = 1.6;


        //            // Paste clipboard results to worksheet range
        //            copyAlltoClipboard();
        //            xlWorkSheet.Select();
        //            Range CR = (Range)xlWorkSheet.Cells[1, 1];
        //            CR.Select();
        //            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

        //            Range rng;
        //            if (cmbCust.Text.Equals(custName))
        //            {
        //                rng = xlWorkSheet.get_Range("A:Q").Cells;
        //            }
        //            else
        //            {
        //                rng = xlWorkSheet.get_Range("A:P").Cells;
        //            }


        //            rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
        //            rng.RowHeight = 16;
        //            rng.EntireColumn.AutoFit();

        //            Range tRange = xlWorkSheet.UsedRange;
        //            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //            tRange.Borders.Weight = XlBorderWeight.xlThin;

        //            //top row 
        //            Range topRow;
        //            if (cmbCust.Text.Equals(custName))
        //            {
        //                topRow = xlWorkSheet.get_Range("a1:q1").Cells;
        //            }
        //            else
        //            {
        //                topRow = xlWorkSheet.get_Range("a1:p1").Cells;
        //            }
        //            topRow.RowHeight = 25;

        //            topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //            topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
        //            topRow.BorderAround2(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

        //            //first column: index 
        //            Range indexCol = xlWorkSheet.Columns[1];
        //            indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //            indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;

        //            for (int a = 0; a <= dgvForecastReport.RowCount - 2; a++)
        //            {
        //                for (int j = 0; j <= dgvForecastReport.ColumnCount - 1; j++)
        //                {
        //                    Range range = (Range)xlWorkSheet.Cells[a + 2, j + 1];

        //                    if (a == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);

        //                        header = (Range)xlWorkSheet.Cells[1, 10];
        //                        header.Font.Color = Color.Blue;

        //                        header = (Range)xlWorkSheet.Cells[1, 14];
        //                        header.Font.Color = Color.Red;

        //                        header = (Range)xlWorkSheet.Cells[1, 16];
        //                        header.Font.Color = Color.Red;


        //                    }

        //                    if (dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
        //                    {
        //                        range.Interior.Color = ColorTranslator.ToOle(Color.White);
        //                        if (a == 0)
        //                        {
        //                            Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
        //                            header.Interior.Color = ColorTranslator.ToOle(Color.White);
        //                        }
        //                    }
        //                    else if (dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor == Color.Black)
        //                    {
        //                        range.Rows.RowHeight = 3;
        //                        range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
        //                        if (a == 0)
        //                        {
        //                            Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
        //                            header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);

        //                        if (a == 0)
        //                        {
        //                            Range header = (Range)xlWorkSheet.Cells[a + 1, j + 1];
        //                            header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport.Rows[a].Cells[j].InheritedStyle.BackColor);
        //                        }
        //                    }
        //                    range.Font.Color = dgvForecastReport.Rows[a].Cells[j].Style.ForeColor;
        //                    if (dgvForecastReport.Rows[a].Cells[j].Style.ForeColor == Color.Blue)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[a + 2, 2];
        //                        header.Font.Underline = true;

        //                        header = (Range)xlWorkSheet.Cells[a + 2, 3];
        //                        header.Font.Underline = true;
        //                    }

        //                }
        //            }

        //            releaseObject(xlWorkSheet);
        //            Clipboard.Clear();
        //            dgvForecastReport.ClearSelection();
        //        }

        //    }
        //    //g_Workbook.Worksheets.Item[1].Delete();
        //    g_Workbook.Save();
        //    releaseObject(g_Workbook);
        //    Cursor = Cursors.Arrow; // change cursor to normal type
        //}

        //private void copyAlltoClipboard()
        //{
        //    dgvForecastReport.SelectAll();
        //    DataObject dataObj = dgvForecastReport.GetClipboardContent();
        //    if (dataObj != null)
        //        Clipboard.SetDataObject(dataObj);
        //}

        //private void releaseObject(object obj)
        //{
        //    try
        //    {
        //        Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        obj = null;
        //        MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                colorOrder = 0;
                searchForecastList();

                int dgvType = 0;
                string custName = tool.getCustName(1);

                if (cmbCust.Text.Equals(custName))//PMMA
                {
                    dgvType = 1;
                }
                else
                {
                    dgvType = 2;
                }

                emptyRowBackColorToBlack(dgvType);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }
    }
}

