using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Runtime.InteropServices;

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
            Feburary,
            March,
            Apirl,
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
            LightGreen,
            DeepSkyBlue,
            Lavender,
            LightBlue,
            LightCyan,
            LightPink,
            Orange,
            Pink,
            RoyalBlue,
            Silver,
            SteelBlue,
            Tomato
        }

        private int indexNo = 1;
        private int alphbet = 65;

        private int colorOrder = 0;
        private string colorName = "Black";
        private int redAlertLevel = 0;

        readonly string IndexColName = "NO";
        readonly string MatTypeColName = "MATERIAL TYPE";
        readonly string NameColName = "PART NAME";
        readonly string CodeColName = "PART CODE";
        readonly string ColorColName = "COLOR";
        readonly string MCColName = "MC";
        readonly string PartWeightColName = "PART WEIGHT(g)";
        readonly string RunnerWeightColName = "RUNNER WEIGHT(g)";
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

        #endregion

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

            dgv.Columns.Clear();

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
            tool.AddTextBoxColumns(dgv, Forecast1ColName, Forecast1ColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, OutColName, OutColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, OsantColName, OsantColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, Shot1ColName, Shot1ColName, DisplayedCells);

            dgv.Columns[MCColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[PartWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[RunnerWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[MBColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[StockColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[EstimateColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            dgv.Columns[StockColName].MinimumWidth = 70;

            dgv.Columns[EstimateColName].HeaderCell.Style.BackColor = Color.LightCyan;
            dgv.Columns[EstimateColName].DefaultCellStyle.BackColor = Color.LightCyan;

            dgv.Columns[Forecast1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[Forecast1ColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[OutColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[OutColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[OsantColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[OsantColName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[Shot1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[Shot1ColName].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[Shot1ColName].HeaderCell.Style.ForeColor = Color.Red;
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
                    row.Cells[Shot1ColName].Style.ForeColor = Color.Black;
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
                        row.Cells[Shot2ColName].Style.ForeColor = Color.Black;
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

                    if(dgvType == 1)
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

                if(dgvType == 1)
                {
                    dgvForecastReport.Rows[rowIndex].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    dgvForecastReport.Rows[rowIndex].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                    if (Convert.ToSingle(dgvForecastReport.Rows[rowIndex].Cells[Shot2ColName].Value) < 0)
                    {
                        dgvForecastReport.Rows[rowIndex].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                    }
                    else
                    {
                        dgvForecastReport.Rows[rowIndex].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                    }
                }
            }
        }
        #endregion

        #region Export to PDF

        private void exportgridtopdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);

            //Add header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);
            }

            //Add datarow
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PdfPCell cellSet = null;
                    string cellValue = "";

                    if (cell.Value != null)
                    {
                        cellValue = cell.Value.ToString();

                    }

                    cellSet = new PdfPCell(new Phrase(cellValue, text));

                    if (cell.Style.BackColor.Name.ToString().Equals("0"))
                    {
                        cellSet.BackgroundColor = new BaseColor(Color.White);
                    }
                    else
                    {
                        cellSet.BackgroundColor = new BaseColor(cell.Style.BackColor);

                    }

                    pdftable.AddCell(cellSet);
                }
            }

            var savefiledialog = new SaveFileDialog();
            savefiledialog.FileName = filename;
            savefiledialog.DefaultExt = ".pdf";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialog.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exportgridtopdf(dgvForecastReport, "forecast");
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

        private void insertForecastData()
        {
            string custName = cmbCust.Text;

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);//load customer's item list

                if (dt.Rows.Count < 1)
                {   
                    MessageBox.Show("no data under this record.");
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

                    foreach (DataRow item in dt.Rows)//go into customer's item list
                    {
                        itemCode = item["item_code"].ToString();
                        forecastOne = Convert.ToSingle(item["forecast_one"]);
                        forecastTwo = Convert.ToSingle(item["forecast_two"]);
                        forecastThree = Convert.ToSingle(item["forecast_three"]);
                        month = item["forecast_current_month"].ToString();

                        DataTable dt3 = daltrfHist.outSearch(cmbCust.Text, getMonthValue(month), itemCode);//load item out to customer record in current month

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
                    float headFcast1 = Convert.ToSingle(row.Cells[Forecast1ColName].Value);
                   
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
                   
                    headFcast1 += childFcast1;
                    
                    headOutStock += childOutStock;

                    float headOutSant = headFcast1 - headOutStock;

                    row.Cells[Forecast1ColName].Value = headFcast1.ToString();
                    
                    row.Cells[OutColName].Value = headOutStock.ToString();
                    row.Cells[Shot1ColName].Value = headShot1.ToString();
                    
                    row.Cells[OsantColName].Value = headOutSant.ToString();

                    colorName = ((color)colorOrder).ToString();
                    changeBackColor(row, 0, true, dgvType);
                    result = true;
                    if (result)
                    {

                        if (colorOrder == 12)
                        {
                            colorOrder = 0;
                        }
                        else
                        {
                            colorOrder++;
                        }

                        return true;
                    }
                }
            }
            return result;
        }

        private void checkChild(string itemCode, string month, string forecastOne, string forecastTwo, string forecastThree, float shotOne, float shotTwo, float outStock,int no, int dgvType)
        {
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
                    int n = dgvForecastReport.Rows.Add();

                    dgv.Rows[n].Cells[CodeColName].Value = Join["join_child_code"].ToString();

                    DataTable dtItem = dalItem.codeSearch(Join["join_child_code"].ToString());

                    if (dtItem.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtItem.Rows)
                        {
                            itemCode = item["item_code"].ToString();

                            if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
                            {
                                partf = Convert.ToSingle(item["item_part_weight"]);
                            }
                            if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
                            {
                                runnerf = Convert.ToSingle(item["item_runner_weight"]);
                            }

                            if(ifGotChild(itemCode))
                            {
                                dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }
                            dgv.Rows[n].Cells[IndexColName].Value = no.ToString()+"("+(char)alphbet+")";
                            dgv.Rows[n].Cells[CodeColName].Value = itemCode;
                            dgv.Rows[n].Cells[NameColName].Value = item["item_name"].ToString();
                            dgv.Rows[n].Cells[MatTypeColName].Value = item["item_material"].ToString();
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
                                dgv.Rows[n].Cells[Forecast1ColName].Value = forecastOne;
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

                                if (dgvType == 1)
                                {
                                    dgv.Rows[n].Cells[Forecast2ColName].Value = forecastTwo;
                                    dgv.Rows[n].Cells[Forecast3ColName].Value = forecastThree;

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
                                }
                            }
                            alphbet++;
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
                MessageBox.Show("no data under this record.");
            }
            else
            {
                dgv.Rows.Clear();

                //load single data
                indexNo = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item["item_code"].ToString();

                    if (!ifGotChild(itemCode) && type != 2)
                    {
                        int n = dgv.Rows.Add();

                        dgv.Rows[n].Cells[IndexColName].Value = indexNo.ToString();
                        indexNo++;
                        dgv.Rows[n].Cells[CodeColName].Value = itemCode;
                        dgv.Rows[n].Cells[NameColName].Value = item["item_name"].ToString();
                        dgv.Rows[n].Cells[ColorColName].Value = item["item_color"].ToString();
                        dgv.Rows[n].Cells[MatTypeColName].Value = item["item_material"].ToString();
                        dgv.Rows[n].Cells[MBColName].Value = item["item_mb"].ToString();
                        dgv.Rows[n].Cells[PartWeightColName].Value = Convert.ToSingle(item["item_part_weight"]).ToString("0.00");
                        dgv.Rows[n].Cells[RunnerWeightColName].Value = Convert.ToSingle(item["item_runner_weight"]).ToString("0.00"); 

                        string month = item["forecast_current_month"].ToString();

                        dgv.Columns[Forecast1ColName].HeaderText = "F/cast " + getShortMonth(month, 1);
                        dgv.Columns[Shot1ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                        
                        dgv.Rows[n].Cells[Forecast1ColName].Value = item["forecast_one"].ToString();
                        
                        dgv.Rows[n].Cells[OutColName].Value = item["forecast_out_stock"].ToString();
                        dgv.Rows[n].Cells[StockColName].Value = item["forecast_ready_stock"].ToString();
                        dgv.Rows[n].Cells[OsantColName].Value = item["forecast_osant"].ToString();

                        float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
                        dgv.Rows[n].Cells[Shot1ColName].Value = shotOne.ToString();

                        if (shotOne < redAlertLevel)
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }

                        if (dgvType == 1)
                        {
                            dgv.Columns[Forecast2ColName].HeaderText = "F/cast " + getShortMonth(month, 2);
                            dgv.Columns[Shot2ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
                            dgv.Columns[Forecast3ColName].HeaderText = "F/cast " + getShortMonth(month, 3);
                            dgv.Rows[n].Cells[Forecast2ColName].Value = item["forecast_two"].ToString();
                            dgv.Rows[n].Cells[Forecast3ColName].Value = item["forecast_three"].ToString();

                            float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);
                            if (shotTwo < redAlertLevel)
                            {
                                dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                            }
                            else
                            {
                                dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                            }

                            dgv.Rows[n].Cells[Shot2ColName].Value = shotTwo.ToString();
                        } 
                        else
                        {
                            //show estimate number
                            dgv.Rows[n].Cells[EstimateColName].Value = get6MonthsMaxOut(itemCode, cmbCust.Text);
                        }
                    }
                }

                if (type != 1)
                {
                    int n = dgv.Rows.Add();
                }

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
                        dgv.Rows[n].Cells[IndexColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                        dgv.Rows[n].Cells[CodeColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[n].Cells[NameColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[n].Cells[CodeColName].Value = itemCode;
                        dgv.Rows[n].Cells[NameColName].Value = item["item_name"].ToString();
                        dgv.Rows[n].Cells[ColorColName].Value = item["item_color"].ToString();

                        dgv.Rows[n].Cells[StockColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        dgv.Rows[n].Cells[StockColName].Value = item["forecast_ready_stock"].ToString();

                        dgv.Columns[Forecast1ColName].HeaderText = "F/cast " + getShortMonth(month, 1);
                        dgv.Rows[n].Cells[Forecast1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        dgv.Rows[n].Cells[Forecast1ColName].Value = forecastOne;

                        dgv.Rows[n].Cells[OutColName].Value = outStock;
                        dgv.Rows[n].Cells[OutColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                        dgv.Rows[n].Cells[OsantColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        dgv.Rows[n].Cells[OsantColName].Value = item["forecast_osant"].ToString();

                        dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        dgv.Columns[Shot1ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                        dgv.Rows[n].Cells[Shot1ColName].Value = item["forecast_shot_one"].ToString();

                        if (shotOne < 0)
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        }
                        else
                        {
                            dgv.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                        }

                        if(dgvType == 1)
                        {
                            dgv.Columns[Forecast2ColName].HeaderText = "F/cast " + getShortMonth(month, 2);
                            dgv.Columns[Forecast3ColName].HeaderText = "F/cast " + getShortMonth(month, 3);
                            dgv.Columns[Shot2ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
                            dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };

                            dgv.Rows[n].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            dgv.Rows[n].Cells[Forecast2ColName].Value = forecastTwo;
                            dgv.Rows[n].Cells[Forecast3ColName].Value = forecastThree;

                            if (shotTwo < 0)
                            {
                                dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            }
                            else
                            {
                                dgv.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Bold) };
                            }
                            dgv.Rows[n].Cells[Shot2ColName].Value = item["forecast_shot_two"].ToString();
                        }
                        else
                        {
                            dgv.Rows[n].Cells[EstimateColName].Value = get6MonthsMaxOut(itemCode,cmbCust.Text);
                        }

                        checkChild(itemCode, month, forecastOne, forecastTwo, forecastThree, shotOne, shotTwo, Convert.ToSingle(outStock),indexNo, dgvType);
                        indexNo++;
                        n = dgv.Rows.Add();
                    }
                    alphbet = 65;
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string custName = tool.getCustName(1);
            int type = 0;
            if (cmbCust.Text.Equals(custName))//PMMA
            {
                createPMMADGV();
                dgvForecastReport.Show();
                type = 1;
            }
            else
            {
                createDGV();
                dgvForecastReport.Show();
                type = 2;
            }

            colorOrder = 0;
            btnCheck.Enabled = false;
            refreshData(type);
            emptyRowBackColorToBlack(type);
            btnCheck.Enabled = true;

            foreach (DataGridViewColumn dgvc in dgvForecastReport.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
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

        #endregion

        #region Selected Index Changed/ Mouse Click

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(cmbSort.Text))
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
                dt.Rows.Add(desc);               cmbOrder.DataSource = dt;
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

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "ForecastReport(" + cmbCust.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                string centerHeader = xlWorkSheet.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                centerHeader = "&\"Calibri,Bold\"&20 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";

                string LeftHeader = xlWorkSheet.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                LeftHeader = "&\"Calibri,Bold\"&20 "+ DateTime.Now.Date.ToString("dd/MM/yyyy");

                string RightHeader = xlWorkSheet.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                RightHeader = "&\"Calibri,Bold\"&20 PG -&P";

                xlWorkSheet.PageSetup.LeftHeader = LeftHeader;
                xlWorkSheet.PageSetup.CenterHeader = centerHeader;
                xlWorkSheet.PageSetup.RightHeader = RightHeader;
                xlWorkSheet.PageSetup.CenterFooter = "footer here hahahahahaha";
                
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

                Range rng = xlWorkSheet.get_Range("A:Q").Cells;
                rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
                rng.RowHeight = 16;
                rng.EntireColumn.AutoFit();

                Range tRange = xlWorkSheet.UsedRange;
                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                tRange.Borders.Weight = XlBorderWeight.xlThin;

                //top row 
                Range topRow = xlWorkSheet.get_Range("a1:q1").Cells;
                topRow.RowHeight = 25;
                
                

                topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                topRow.BorderAround2(Type.Missing,XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                //first column: index 
                Range indexCol = xlWorkSheet.Columns[1];
                indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;






                for (int i = 0; i <= dgvForecastReport.RowCount - 2; i++)
                {
                    for (int j = 0; j <= dgvForecastReport.ColumnCount -1; j++)
                    {
                        Range range = (Range)xlWorkSheet.Cells[i+2, j+1];
                        
                        if(i == 0)
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
                        if(dgvForecastReport.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
                        {
                            Range header = (Range)xlWorkSheet.Cells[i + 2, 2];
                            header.Font.Underline = true;

                            header = (Range)xlWorkSheet.Cells[i + 2, 3];
                            header.Font.Underline = true;
                        }

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
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
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

        #endregion

        #region backup
        //#region Variable Declare
        //enum Month
        //{
        //    January = 1,
        //    Feburary,
        //    March,
        //    Apirl,
        //    May,
        //    June,
        //    July,
        //    August,
        //    September,
        //    October,
        //    November,
        //    December
        //}

        //enum color
        //{
        //    Gold = 0,
        //    LightGreen,
        //    DeepSkyBlue,
        //    Lavender,
        //    LightBlue,
        //    LightCyan,
        //    LightPink,
        //    Orange,
        //    Pink,
        //    RoyalBlue,
        //    Silver,
        //    SteelBlue,
        //    Tomato
        //}

        //private int indexNo = 1;
        //private int alphbet = 65;

        //private int colorOrder = 0;
        //private string colorName = "Black";
        //private int redAlertLevel = 0;

        //readonly string IndexColName = "NO";
        //readonly string MatTypeColName = "MATERIAL TYPE";
        //readonly string NameColName = "PART NAME";
        //readonly string CodeColName = "PART CODE";
        //readonly string ColorColName = "COLOR";
        //readonly string MCColName = "MC";
        //readonly string PartWeightColName = "PART WEIGHT(g)";
        //readonly string RunnerWeightColName = "RUNNER WEIGHT(g)";
        //readonly string MBColName = "MBatch CODE";
        //readonly string StockColName = "READY STOCK";
        //readonly string EstimateColName = "ESTIMATE";
        //readonly string Forecast1ColName = "F/CAST 1";
        //readonly string Forecast2ColName = "F/CAST 2";
        //readonly string Forecast3ColName = "F/CAST 3";
        //readonly string OutColName = "OUT";
        //readonly string OsantColName = "OSANT";
        //readonly string Shot1ColName = "SHOT 1";
        //readonly string Shot2ColName = "SHOT 2";

        //DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        //DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //DataGridViewAutoSizeColumnMode DisplayedCellsExceptHeader = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        //#endregion

        //#region create class object (database)

        //custBLL uCust = new custBLL();
        //custDAL dalCust = new custDAL();

        //facBLL uFac = new facBLL();
        //facDAL dalFac = new facDAL();

        //itemBLL uItem = new itemBLL();
        //itemDAL dalItem = new itemDAL();

        //itemCatBLL uItemCat = new itemCatBLL();
        //itemCatDAL dalItemCat = new itemCatDAL();

        //trfCatBLL utrfCat = new trfCatBLL();
        //trfCatDAL daltrfCat = new trfCatDAL();

        //trfHistBLL utrfHist = new trfHistBLL();
        //trfHistDAL daltrfHist = new trfHistDAL();

        //facStockBLL uStock = new facStockBLL();
        //facStockDAL dalStock = new facStockDAL();

        //itemCustBLL uItemCust = new itemCustBLL();
        //itemCustDAL dalItemCust = new itemCustDAL();

        //joinBLL uJoin = new joinBLL();
        //joinDAL dalJoin = new joinDAL();

        //forecastBLL uForecast = new forecastBLL();
        //forecastDAL dalForecast = new forecastDAL();

        //Tool tool = new Tool();
        //#endregion  

        //#region Form Load/Close

        //private void frmForecastReport_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    MainDashboard.forecastReportInputFormOpen = false;
        //}

        //private void frmForecastReport_Load(object sender, EventArgs e)
        //{
        //    tool.loadCustomerToComboBox(cmbCust);
        //    UIDesign();
        //}

        //#endregion

        //#region Data Checking/Get Data

        //private int getMonthValue(string keyword)
        //{

        //    int month = 0;
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        month = Convert.ToInt32(DateTime.Now.Month.ToString());
        //    }
        //    else
        //    {
        //        month = (int)(Month)Enum.Parse(typeof(Month), keyword);
        //    }


        //    return month;
        //}

        //private int getNextMonth(string keyword)
        //{

        //    int month = getMonthValue(keyword);
        //    int nextMonth = 0;

        //    if (month == 12)
        //    {
        //        nextMonth = 1;
        //    }
        //    else
        //    {
        //        nextMonth = month + 1;
        //    }
        //    return nextMonth;
        //}

        //private int getNextNextMonth(string keyword)
        //{

        //    int nextMonth = getNextMonth(keyword);
        //    int nextNextMonth = 0;

        //    if (nextMonth == 12)
        //    {
        //        nextNextMonth = 1;
        //    }
        //    else
        //    {
        //        nextNextMonth = nextMonth + 1;
        //    }
        //    return nextNextMonth;
        //}

        //private string getShortMonth(string month, int forecast)
        //{
        //    string shortMonth = "";
        //    int monthValue = 0;
        //    switch (forecast)
        //    {
        //        case 1:
        //            monthValue = getMonthValue(month);
        //            break;
        //        case 2:
        //            monthValue = getNextMonth(month);
        //            break;
        //        case 3:
        //            monthValue = getNextNextMonth(month);
        //            break;
        //        default:
        //            monthValue = 0;
        //            break;
        //    }

        //    switch (monthValue)
        //    {
        //        case 1:
        //            shortMonth = "Jan";
        //            break;
        //        case 2:
        //            shortMonth = "Feb";
        //            break;
        //        case 3:
        //            shortMonth = "Mar";
        //            break;
        //        case 4:
        //            shortMonth = "Apr";
        //            break;
        //        case 5:
        //            shortMonth = "May";
        //            break;
        //        case 6:
        //            shortMonth = "Jun";
        //            break;
        //        case 7:
        //            shortMonth = "Jul";
        //            break;
        //        case 8:
        //            shortMonth = "Aug";
        //            break;
        //        case 9:
        //            shortMonth = "Sep";
        //            break;
        //        case 10:
        //            shortMonth = "Oct";
        //            break;
        //        case 11:
        //            shortMonth = "Nov";
        //            break;
        //        case 12:
        //            shortMonth = "Dec";
        //            break;
        //    }


        //    return shortMonth;
        //}

        //private string getCustID(string custName)
        //{
        //    string custID = "";

        //    DataTable dtCust = dalCust.nameSearch(custName);

        //    foreach (DataRow Cust in dtCust.Rows)
        //    {
        //        custID = Cust["cust_id"].ToString();
        //    }
        //    return custID;
        //}

        //private bool ifGotChild(string itemCode)
        //{
        //    bool result = false;
        //    DataTable dtJoin = dalJoin.parentCheck(itemCode);
        //    if (dtJoin.Rows.Count > 0)
        //    {
        //        result = true;
        //    }

        //    return result;
        //}

        //#endregion

        //#region UI Edit

        //private void UIDesign()
        //{
        //    dgvForecastReport2.Columns["mc_ton"].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
        //    dgvForecastReport2.Columns["mc_ton"].Width = 40;
        //    dgvForecastReport2.Columns["item_part_weight"].HeaderCell.Style.BackColor = Color.LightYellow;
        //    dgvForecastReport2.Columns["item_runner_weight"].HeaderCell.Style.BackColor = Color.LightYellow;
        //    dgvForecastReport2.Columns[Forecast1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
        //    dgvForecastReport2.Columns[OutColName].HeaderCell.Style.BackColor = Color.LightYellow;
        //    dgvForecastReport2.Columns["oSant"].HeaderCell.Style.BackColor = Color.LightYellow;

        //    dgvForecastReport2.Columns[Shot1ColName].HeaderCell.Style.BackColor = Color.LightYellow;
        //    dgvForecastReport2.Columns[Shot1ColName].HeaderCell.Style.ForeColor = Color.Red;

        //    dgvForecastReport2.Columns[Forecast2ColName].HeaderCell.Style.BackColor = Color.LightCyan;
        //    dgvForecastReport2.Columns[Shot2ColName].HeaderCell.Style.BackColor = Color.LightCyan;
        //    dgvForecastReport2.Columns[Shot2ColName].HeaderCell.Style.ForeColor = Color.Red;

        //    dgvForecastReport2.Columns["forecast_Three"].HeaderCell.Style.BackColor = Color.PeachPuff;


        //    dgvForecastReport2.Columns[Shot2ColName].HeaderCell.Style.ForeColor = Color.Red;

        //    dgvForecastReport2.Columns["stock_qty"].HeaderCell.Style.BackColor = Color.Pink;
        //    dgvForecastReport2.Columns["stock_qty"].HeaderCell.Style.ForeColor = Color.Blue;
        //}

        //private void emptyRowBackColorToBlack()
        //{
        //    float shotOne = redAlertLevel;
        //    float shotTwo = redAlertLevel;

        //    string colorName = "Black";
        //    foreach (DataGridViewRow row in dgvForecastReport2.Rows)
        //    {
        //        if (row.Cells[Shot1ColName].Value != null)
        //        {
        //            shotOne = Convert.ToSingle(row.Cells[Shot1ColName].Value);
        //        }

        //        if (shotOne < redAlertLevel)
        //        {
        //            row.Cells[Shot1ColName].Style.ForeColor = Color.Red;
        //        }
        //        else if (row.Cells[MBColName].Value != null)
        //        {
        //            row.Cells[Shot1ColName].Style.ForeColor = Color.Black;
        //        }

        //        if (row.Cells[Shot2ColName].Value != null)
        //        {
        //            shotTwo = Convert.ToSingle(row.Cells[Shot2ColName].Value);
        //        }

        //        if (shotTwo < redAlertLevel)
        //        {
        //            row.Cells[Shot2ColName].Style.ForeColor = Color.Red;
        //        }
        //        else if (row.Cells[MBColName].Value != null)
        //        {
        //            row.Cells[Shot2ColName].Style.ForeColor = Color.Black;
        //        }

        //        if (row.Cells["item_name"].Value == null)
        //        {
        //            row.Height = 3;
        //            row.DefaultCellStyle.BackColor = Color.FromName(colorName);
        //        }
        //    }
        //}

        //private void changeBackColor(DataGridViewRow row, int rowIndex, bool type)//if type = true, change previous data back color, else change the new data
        //{
        //    if (type)
        //    {
        //        string test = row.Cells[Forecast1ColName].Style.BackColor.ToKnownColor().ToString();

        //        if (test.Equals("0"))
        //        {
        //            row.Cells["item_code"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //            row.Cells["item_name"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //            row.Cells[Forecast1ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //            row.Cells[Forecast2ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //            row.Cells[Forecast3ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //            row.Cells[OutColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //            row.Cells["oSant"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

        //            if (Convert.ToSingle(row.Cells[Shot1ColName].Value) < 0)
        //            {
        //                row.Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
        //            }
        //            else
        //            {
        //                row.Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
        //            }

        //            if (Convert.ToSingle(row.Cells[Shot2ColName].Value) < 0)
        //            {
        //                row.Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
        //            }
        //            else
        //            {
        //                row.Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
        //            }
        //        }
        //        else
        //        {
        //            colorName = test;
        //        }
        //    }
        //    else
        //    {
        //        dgvForecastReport2.Rows[rowIndex].Cells["item_code"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //        dgvForecastReport2.Rows[rowIndex].Cells["item_name"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //        dgvForecastReport2.Rows[rowIndex].Cells[Forecast1ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //        dgvForecastReport2.Rows[rowIndex].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //        dgvForecastReport2.Rows[rowIndex].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //        dgvForecastReport2.Rows[rowIndex].Cells[OutColName].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
        //        dgvForecastReport2.Rows[rowIndex].Cells["oSant"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

        //        if (Convert.ToSingle(dgvForecastReport2.Rows[rowIndex].Cells[Shot1ColName].Value) < 0)
        //        {
        //            dgvForecastReport2.Rows[rowIndex].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
        //        }
        //        else
        //        {
        //            dgvForecastReport2.Rows[rowIndex].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
        //        }

        //        if (Convert.ToSingle(dgvForecastReport2.Rows[rowIndex].Cells[Shot2ColName].Value) < 0)
        //        {
        //            dgvForecastReport2.Rows[rowIndex].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
        //        }
        //        else
        //        {
        //            dgvForecastReport2.Rows[rowIndex].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
        //        }

        //    }
        //}

        //#endregion

        //#region Export to PDF

        //private void exportgridtopdf(DataGridView dgw, string filename)
        //{
        //    BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
        //    PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
        //    pdftable.DefaultCell.Padding = 3;
        //    pdftable.WidthPercentage = 100;
        //    pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
        //    pdftable.DefaultCell.BorderWidth = 1;

        //    iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);

        //    //Add header
        //    foreach (DataGridViewColumn column in dgw.Columns)
        //    {
        //        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
        //        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
        //        pdftable.AddCell(cell);
        //    }

        //    //Add datarow
        //    foreach (DataGridViewRow row in dgw.Rows)
        //    {
        //        foreach (DataGridViewCell cell in row.Cells)
        //        {
        //            PdfPCell cellSet = null;
        //            string cellValue = "";

        //            if (cell.Value != null)
        //            {
        //                cellValue = cell.Value.ToString();

        //            }

        //            cellSet = new PdfPCell(new Phrase(cellValue, text));

        //            if (cell.Style.BackColor.Name.ToString().Equals("0"))
        //            {
        //                cellSet.BackgroundColor = new BaseColor(Color.White);
        //            }
        //            else
        //            {
        //                cellSet.BackgroundColor = new BaseColor(cell.Style.BackColor);

        //            }

        //            pdftable.AddCell(cellSet);
        //        }
        //    }

        //    var savefiledialog = new SaveFileDialog();
        //    savefiledialog.FileName = filename;
        //    savefiledialog.DefaultExt = ".pdf";
        //    if (savefiledialog.ShowDialog() == DialogResult.OK)
        //    {
        //        using (FileStream stream = new FileStream(savefiledialog.FileName, FileMode.Create))
        //        {
        //            Document pdfdoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
        //            PdfWriter.GetInstance(pdfdoc, stream);
        //            pdfdoc.Open();
        //            pdfdoc.Add(pdftable);
        //            pdfdoc.Close();
        //            stream.Close();
        //        }
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    exportgridtopdf(dgvForecastReport2, "forecast");
        //}

        //#endregion

        //#region Forecast Data Processing

        //private void refreshData()
        //{
        //    bool result = dalForecast.Delete();//database forecast table reset

        //    if (!result)
        //    {
        //        MessageBox.Show("Failed to reset forecast data");
        //    }
        //    else
        //    {
        //        colorOrder = 0;
        //        insertForecastData();

        //        string sort = cmbSort.Text;
        //        string order = cmbOrder.Text;
        //        int type = 0;

        //        if (cmbType.SelectedIndex > 0)
        //        {
        //            type = cmbType.SelectedIndex;
        //        }

        //        DataTable dt = dalForecast.Select(sort, order);
        //        loadForecastList(dt, type);
        //    }
        //}

        //private void insertForecastData()
        //{
        //    string custName = cmbCust.Text;

        //    if (!string.IsNullOrEmpty(custName))
        //    {
        //        DataTable dt = dalItemCust.custSearch(custName);//load customer's item list

        //        if (dt.Rows.Count < 1)
        //        {
        //            MessageBox.Show("no data under this record.");
        //        }
        //        else
        //        {
        //            float outStock = 0;//item stock out qty(in current month)
        //            float forecastOne = 0;//current month forecast number
        //            float forecastTwo = 0;//next month forecast number
        //            float forecastThree = 0;//next next month forecast number
        //            float readyStock = 0;//item ready stock
        //            float outSant = 0;//forecast - outStock
        //            float shotOne = 0;//still left how many item after current month
        //            float shotTwo = 0;//still left how many item after next month
        //            string itemCode = "";
        //            string month = "";
        //            int forecastIndex = 1;

        //            foreach (DataRow item in dt.Rows)//go into customer's item list
        //            {
        //                itemCode = item["item_code"].ToString();
        //                forecastOne = Convert.ToSingle(item[Forecast1ColName]);
        //                forecastTwo = Convert.ToSingle(item[Forecast2ColName]);
        //                forecastThree = Convert.ToSingle(item[Forecast3ColName]);
        //                month = item["forecast_current_month"].ToString();

        //                DataTable dt3 = daltrfHist.outSearch(cmbCust.Text, getMonthValue(month), itemCode);//load item out to customer record in current month

        //                outStock = 0;

        //                if (dt3.Rows.Count > 0)
        //                {
        //                    foreach (DataRow outRecord in dt3.Rows)
        //                    {
        //                        //To-Do: need to check if from factory

        //                        if (outRecord["trf_result"].ToString().Equals("Passed"))
        //                        {
        //                            outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
        //                        }

        //                    }
        //                }

        //                readyStock = Convert.ToSingle(dalItem.getStockQty(itemCode));

        //                outSant = forecastOne - outStock;

        //                if (outSant > 0)
        //                {
        //                    shotOne = readyStock - outSant;
        //                }
        //                else
        //                {
        //                    shotOne = readyStock;
        //                }

        //                shotTwo = shotOne - forecastTwo;

        //                uForecast.forecast_no = forecastIndex.ToString();
        //                uForecast.item_code = itemCode;
        //                uForecast.forecast_ready_stock = readyStock;
        //                uForecast.forecast_current_month = month;
        //                uForecast.forecast_one = forecastOne;
        //                uForecast.forecast_two = forecastTwo;
        //                uForecast.forecast_three = forecastThree;
        //                uForecast.forecast_out_stock = outStock;
        //                uForecast.forecast_osant = outSant;
        //                uForecast.forecast_shot_one = shotOne;
        //                uForecast.forecast_shot_two = shotTwo;
        //                uForecast.forecast_updtd_date = DateTime.Now;
        //                uForecast.forecast_updtd_by = -1;

        //                bool result = dalForecast.Insert(uForecast);
        //                if (!result)
        //                {
        //                    MessageBox.Show("failed to insert forecast data");
        //                    return;
        //                }
        //                else
        //                {
        //                    forecastIndex++;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dgvForecastReport2.DataSource = null;
        //    }
        //    dgvForecastReport2.ClearSelection();
        //}//generate forecast data 

        //private bool ifRepeat(string itemCode, int n, string fcast1, string fcast2, string fcast3, string outStock, float shot1, float shot2)
        //{
        //    bool result = false;

        //    foreach (DataGridViewRow row in dgvForecastReport2.Rows)
        //    {
        //        if (row.Cells["item_code"].Value == null)
        //        {
        //            row.Cells["item_code"].Value = "";
        //        }
        //        if (row.Cells["item_code"].Value.ToString().Equals(itemCode) && row.Index < n)
        //        {
        //            float headFcast1 = Convert.ToSingle(row.Cells[Forecast1ColName].Value);
        //            float headFcast2 = Convert.ToSingle(row.Cells[Forecast2ColName].Value);
        //            float headFcast3 = Convert.ToSingle(row.Cells[Forecast3ColName].Value);
        //            float headOutStock = Convert.ToSingle(row.Cells[OutColName].Value);
        //            float headShot1 = Convert.ToSingle(row.Cells[Shot1ColName].Value);
        //            float headShot2 = Convert.ToSingle(row.Cells[Shot2ColName].Value);

        //            float childFcast1 = Convert.ToSingle(fcast1);
        //            float childFcast2 = Convert.ToSingle(fcast2);
        //            float childFcast3 = Convert.ToSingle(fcast3);
        //            float childOutStock = Convert.ToSingle(outStock);

        //            if (shot1 < 0)
        //            {
        //                headShot1 += shot1;
        //            }

        //            if (shot2 < 0)
        //            {
        //                headShot2 += shot2;
        //            }

        //            headFcast1 += childFcast1;
        //            headFcast2 += childFcast2;
        //            headFcast3 += childFcast3;
        //            headOutStock += childOutStock;

        //            float headOutSant = headFcast1 - headOutStock;

        //            row.Cells[Forecast1ColName].Value = headFcast1.ToString();
        //            row.Cells[Forecast2ColName].Value = headFcast2.ToString();
        //            row.Cells[Forecast3ColName].Value = headFcast3.ToString();
        //            row.Cells[OutColName].Value = headOutStock.ToString();
        //            row.Cells[Shot1ColName].Value = headShot1.ToString();
        //            row.Cells[Shot2ColName].Value = headShot2.ToString();
        //            row.Cells["oSant"].Value = headOutSant.ToString();

        //            colorName = ((color)colorOrder).ToString();
        //            changeBackColor(row, 0, true);
        //            result = true;
        //            if (result)
        //            {

        //                if (colorOrder == 12)
        //                {
        //                    colorOrder = 0;
        //                }
        //                else
        //                {
        //                    colorOrder++;
        //                }

        //                return true;
        //            }
        //        }
        //    }
        //    return result;
        //}

        //private void checkChild(string itemCode, string month, string forecastOne, string forecastTwo, string forecastThree, float shotOne, float shotTwo, float outStock, int no)
        //{
        //    string parentItemCode = itemCode;
        //    DataTable dtJoin = dalJoin.parentCheck(itemCode);
        //    if (dtJoin.Rows.Count > 0)
        //    {
        //        foreach (DataRow Join in dtJoin.Rows)
        //        {
        //            float partf = 0;
        //            float runnerf = 0;
        //            float childShotOne = 0;
        //            float childShotTwo = 0;
        //            int n = dgvForecastReport2.Rows.Add();

        //            dgvForecastReport2.Rows[n].Cells["item_code"].Value = Join["join_child_code"].ToString();

        //            DataTable dtItem = dalItem.codeSearch(Join["join_child_code"].ToString());

        //            if (dtItem.Rows.Count > 0)
        //            {
        //                foreach (DataRow item in dtItem.Rows)
        //                {
        //                    itemCode = item["item_code"].ToString();

        //                    if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
        //                    {
        //                        partf = Convert.ToSingle(item["item_part_weight"]);
        //                    }
        //                    if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
        //                    {
        //                        runnerf = Convert.ToSingle(item["item_runner_weight"]);
        //                    }

        //                    if (ifGotChild(itemCode))
        //                    {
        //                        dgvForecastReport2.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Underline | FontStyle.Bold) };
        //                        dgvForecastReport2.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Underline | FontStyle.Bold) };
        //                    }
        //                    dgvForecastReport2.Rows[n].Cells["index"].Value = no.ToString() + "(" + (char)alphbet + ")";
        //                    dgvForecastReport2.Rows[n].Cells["item_code"].Value = itemCode;
        //                    dgvForecastReport2.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
        //                    dgvForecastReport2.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
        //                    dgvForecastReport2.Rows[n].Cells[MBColName].Value = item["item_mb"].ToString();
        //                    dgvForecastReport2.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
        //                    dgvForecastReport2.Rows[n].Cells["item_part_weight"].Value = partf.ToString("0.00");
        //                    dgvForecastReport2.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");

        //                    if (ifRepeat(itemCode, n, forecastOne, forecastTwo, forecastThree, outStock.ToString(), shotOne, shotTwo))
        //                    {
        //                        changeBackColor(null, n, false);
        //                    }
        //                    else
        //                    {
        //                        dgvForecastReport2.Rows[n].Cells[Forecast1ColName].Value = forecastOne;
        //                        dgvForecastReport2.Rows[n].Cells[Forecast2ColName].Value = forecastTwo;
        //                        dgvForecastReport2.Rows[n].Cells[Forecast3ColName].Value = forecastThree;
        //                        dgvForecastReport2.Rows[n].Cells[OutColName].Value = outStock.ToString();

        //                        float readyStock = Convert.ToSingle(dalItem.getStockQty(itemCode));
        //                        dgvForecastReport2.Rows[n].Cells["stock_qty"].Style.Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Regular);
        //                        dgvForecastReport2.Rows[n].Cells["stock_qty"].Value = readyStock.ToString();

        //                        float oSant = Convert.ToSingle(forecastOne) - outStock;
        //                        dgvForecastReport2.Rows[n].Cells["oSant"].Value = oSant.ToString();

        //                        if (shotOne >= 0)
        //                        {
        //                            childShotOne = readyStock;
        //                        }
        //                        else
        //                        {
        //                            childShotOne = readyStock + shotOne;
        //                        }

        //                        if (childShotOne < 0)
        //                        {
        //                            dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style.BackColor };

        //                        }
        //                        else
        //                        {
        //                            dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style.BackColor };

        //                        }

        //                        dgvForecastReport2.Rows[n].Cells[Shot1ColName].Value = childShotOne.ToString();

        //                        if (shotTwo >= 0)
        //                        {
        //                            childShotTwo = childShotOne;
        //                        }
        //                        else if (shotOne > 0)
        //                        {
        //                            childShotTwo = childShotOne + shotTwo;
        //                        }
        //                        else
        //                        {
        //                            childShotTwo = childShotOne - Convert.ToSingle(forecastTwo);
        //                        }

        //                        if (childShotTwo < 0)
        //                        {
        //                            dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style.BackColor };

        //                        }
        //                        else
        //                        {
        //                            dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style.BackColor };

        //                        }

        //                        dgvForecastReport2.Rows[n].Cells[Shot2ColName].Value = childShotTwo.ToString();
        //                    }
        //                    alphbet++;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void loadForecastList(DataTable dataTable, int type)
        //{
        //    DataTable dt = dataTable;

        //    if (dt.Rows.Count <= 0)
        //    {
        //        MessageBox.Show("no data under this record.");
        //    }
        //    else
        //    {
        //        dgvForecastReport2.Rows.Clear();

        //        UIDesign();

        //        //load single data
        //        indexNo = 1;
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            string itemCode = item["item_code"].ToString();

        //            if (!ifGotChild(itemCode) && type != 2)
        //            {
        //                int n = dgvForecastReport2.Rows.Add();

        //                dgvForecastReport2.Rows[n].Cells["index"].Value = indexNo.ToString();
        //                indexNo++;
        //                dgvForecastReport2.Rows[n].Cells["item_code"].Value = itemCode;
        //                dgvForecastReport2.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
        //                dgvForecastReport2.Rows[n].Cells[MBColName].Value = item["item_mb"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["item_part_weight"].Value = Convert.ToSingle(item["item_part_weight"]).ToString("0.00");
        //                dgvForecastReport2.Rows[n].Cells["item_runner_weight"].Value = Convert.ToSingle(item["item_runner_weight"]).ToString("0.00");

        //                string month = item["forecast_current_month"].ToString();

        //                dgvForecastReport2.Columns[Forecast1ColName].HeaderText = "F/cast " + getShortMonth(month, 1);
        //                dgvForecastReport2.Columns[Forecast2ColName].HeaderText = "F/cast " + getShortMonth(month, 2);
        //                dgvForecastReport2.Columns[Forecast3ColName].HeaderText = "F/cast " + getShortMonth(month, 3);
        //                dgvForecastReport2.Columns[Shot1ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
        //                dgvForecastReport2.Columns[Shot2ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 2);

        //                dgvForecastReport2.Rows[n].Cells[Forecast1ColName].Value = item[Forecast1ColName].ToString();
        //                dgvForecastReport2.Rows[n].Cells[Forecast2ColName].Value = item[Forecast2ColName].ToString();
        //                dgvForecastReport2.Rows[n].Cells[Forecast3ColName].Value = item[Forecast3ColName].ToString();

        //                dgvForecastReport2.Rows[n].Cells[OutColName].Value = item["forecast_out_stock"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["stock_qty"].Value = item["forecast_ready_stock"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["oSant"].Value = item["forecast_osant"].ToString();

        //                float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
        //                float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);

        //                if (shotOne < redAlertLevel)
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
        //                }
        //                else
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
        //                }



        //                if (shotTwo < redAlertLevel)
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
        //                }
        //                else
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
        //                }

        //                dgvForecastReport2.Rows[n].Cells[Shot1ColName].Value = shotOne.ToString();
        //                dgvForecastReport2.Rows[n].Cells[Shot2ColName].Value = shotTwo.ToString();
        //            }

        //        }

        //        if (type != 1)
        //        {
        //            int n = dgvForecastReport2.Rows.Add();
        //        }

        //        //load parent and child data
        //        indexNo = 1;
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            string itemCode = item["item_code"].ToString();

        //            if (ifGotChild(itemCode) && type != 1)
        //            {
        //                int n = dgvForecastReport2.Rows.Add();

        //                float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
        //                float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);
        //                string forecastOne = item[Forecast1ColName].ToString();
        //                string forecastTwo = item[Forecast2ColName].ToString();
        //                string forecastThree = item[Forecast3ColName].ToString();
        //                string month = item["forecast_current_month"].ToString();
        //                string outStock = item["forecast_out_stock"].ToString();

        //                dgvForecastReport2.Rows[n].Cells["index"].Value = indexNo.ToString();
        //                dgvForecastReport2.Rows[n].Cells["index"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };

        //                dgvForecastReport2.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Underline | FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Underline | FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells["item_code"].Value = itemCode;
        //                dgvForecastReport2.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
        //                dgvForecastReport2.Columns[Forecast1ColName].HeaderText = "F/cast " + getShortMonth(month, 1);
        //                dgvForecastReport2.Columns[Forecast2ColName].HeaderText = "F/cast " + getShortMonth(month, 2);
        //                dgvForecastReport2.Columns[Forecast3ColName].HeaderText = "F/cast " + getShortMonth(month, 3);
        //                dgvForecastReport2.Columns[Shot1ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
        //                dgvForecastReport2.Columns[Shot2ColName].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
        //                dgvForecastReport2.Rows[n].Cells[Forecast1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells[Forecast2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells[Forecast3ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells[OutColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells["stock_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells["oSant"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };

        //                dgvForecastReport2.Rows[n].Cells[Forecast1ColName].Value = forecastOne;
        //                dgvForecastReport2.Rows[n].Cells[Forecast2ColName].Value = forecastTwo;
        //                dgvForecastReport2.Rows[n].Cells[Forecast3ColName].Value = forecastThree;

        //                dgvForecastReport2.Rows[n].Cells[OutColName].Value = outStock;
        //                dgvForecastReport2.Rows[n].Cells["stock_qty"].Value = item["forecast_ready_stock"].ToString();
        //                dgvForecastReport2.Rows[n].Cells["oSant"].Value = item["forecast_osant"].ToString();

        //                if (shotOne < 0)
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                }
        //                else
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot1ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                }

        //                if (shotTwo < 0)
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                }
        //                else
        //                {
        //                    dgvForecastReport2.Rows[n].Cells[Shot2ColName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport2.Font, FontStyle.Bold) };
        //                }

        //                dgvForecastReport2.Rows[n].Cells[Shot1ColName].Value = item["forecast_shot_one"].ToString();
        //                dgvForecastReport2.Rows[n].Cells[Shot2ColName].Value = item["forecast_shot_two"].ToString();
        //                checkChild(itemCode, month, forecastOne, forecastTwo, forecastThree, shotOne, shotTwo, Convert.ToSingle(outStock), indexNo);
        //                indexNo++;
        //                n = dgvForecastReport2.Rows.Add();
        //            }

        //            alphbet = 65;
        //        }
        //    }


        //    dgvForecastReport2.ClearSelection();
        //}

        //private void searchForecastList()
        //{
        //    int type = 0;
        //    string sort = cmbSort.Text;
        //    string order = cmbOrder.Text;
        //    string keywords = txtSearch.Text;
        //    DataTable dt;


        //    if (cmbType.SelectedIndex > 0)
        //    {
        //        type = cmbType.SelectedIndex;
        //    }

        //    if (string.IsNullOrEmpty(keywords))
        //    {
        //        dt = dalForecast.Select(sort, order);
        //    }
        //    else
        //    {
        //        dt = dalForecast.testSearch(keywords);
        //        dt = RemoveDuplicates(dt);
        //    }

        //    loadForecastList(dt, type);
        //}

        ///* To eliminate Duplicate rows */
        //private DataTable RemoveDuplicates(DataTable dt)
        //{

        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        //        {
        //            if (i == 0)
        //            {
        //                break;
        //            }
        //            for (int j = i - 1; j >= 0; j--)
        //            {
        //                if (dt.Rows[i]["item_code"].ToString() == dt.Rows[j]["item_code"].ToString())
        //                {
        //                    dt.Rows[i].Delete();
        //                    break;
        //                }
        //            }
        //        }
        //        dt.AcceptChanges();

        //    }
        //    return dt;
        //}

        //#endregion

        //#region Function: Check forecast data/Search

        //private void btnCheck_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

        //    colorOrder = 0;
        //    btnCheck.Enabled = false;
        //    refreshData();
        //    emptyRowBackColorToBlack();
        //    btnCheck.Enabled = true;

        //    foreach (DataGridViewColumn dgvc in dgvForecastReport2.Columns)
        //    {
        //        dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }

        //    Cursor = Cursors.Arrow; // change cursor to normal type
        //}

        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

        //    colorOrder = 0;
        //    searchForecastList();
        //    emptyRowBackColorToBlack();
        //    Cursor = Cursors.Arrow; // change cursor to normal type
        //}

        //#endregion

        //#region Selected Index Changed/ Mouse Click

        //private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (string.IsNullOrEmpty(cmbSort.Text))
        //    {
        //        cmbOrder.DataSource = null;
        //    }
        //    else
        //    {
        //        string asc = "Ascending";
        //        string desc = "Descending";
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("order");
        //        dt.Rows.Add(asc);
        //        dt.Rows.Add(desc);
        //        cmbOrder.DataSource = dt;
        //        cmbOrder.DisplayMember = "order";
        //        //cmbOrder.SelectedIndex = 0;
        //    }

        //}

        //private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbType.SelectedIndex = 0;

        //    string custName = tool.getCustName(3);

        //    if (cmbCust.Text.Equals(custName))
        //    {
        //        //create daikin dgv
        //        dgvForecastReport2.Hide();
        //        createDaiKinDGV();
        //        dgvDaikin.Show();
        //    }
        //    else
        //    {
        //        //create normal dgv
        //        dgvDaikin.Hide();
        //        dgvForecastReport2.Show();
        //    }
        //}

        //private void createDaiKinDGV()
        //{
        //    DataGridView dgv = dgvDaikin;

        //    dgv.Columns.Clear();

        //    tool.AddTextBoxColumns(dgv, IndexColName, IndexColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, MatTypeColName, MatTypeColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, NameColName, NameColName, Fill);
        //    tool.AddTextBoxColumns(dgv, CodeColName, CodeColName, Fill);
        //    tool.AddTextBoxColumns(dgv, ColorColName, ColorColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, MCColName, MCColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, PartWeightColName, PartWeightColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, RunnerWeightColName, RunnerWeightColName, DisplayedCellsExceptHeader);
        //    tool.AddTextBoxColumns(dgv, MBColName, MBColName, DisplayedCellsExceptHeader);
        //    tool.AddTextBoxColumns(dgv, StockColName, StockColName, DisplayedCellsExceptHeader);
        //    tool.AddTextBoxColumns(dgv, EstimateColName, EstimateColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, Forecast1ColName, Forecast1ColName, DisplayedCells);
        //    tool.AddTextBoxColumns(dgv, OutColName, OutColName, DisplayedCellsExceptHeader);
        //    tool.AddTextBoxColumns(dgv, OsantColName, OsantColName, DisplayedCellsExceptHeader);
        //    tool.AddTextBoxColumns(dgv, Shot1ColName, Shot1ColName, DisplayedCellsExceptHeader);

        //    dgv.Columns[MCColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[PartWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[RunnerWeightColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[MBColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[StockColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[EstimateColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[Forecast1ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[OutColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[OsantColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgv.Columns[Shot1ColName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //}

        //private void frmForecastReport_Click(object sender, EventArgs e)
        //{
        //    dgvForecastReport2.ClearSelection();
        //}

        //#endregion

        //#region export to excel

        //private string setFileName()
        //{
        //    string fileName = "Test.xls";

        //    DateTime currentDate = DateTime.Now.Date;
        //    fileName = "ForecastReport(" + cmbCust.Text + ")_" + currentDate.ToString("ddMMyyyy") + ".xls";
        //    return fileName;
        //}

        //private void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Filter = "Excel Documents (*.xls)|*.xls";
        //    sfd.FileName = setFileName();
        //    if (sfd.ShowDialog() == DialogResult.OK)
        //    {
        //        // Copy DataGridView results to clipboard
        //        copyAlltoClipboard();

        //        object misValue = System.Reflection.Missing.Value;
        //        Excel.Application xlexcel = new Excel.Application();

        //        xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
        //        Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
        //        Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //        string centerHeader = xlWorkSheet.PageSetup.CenterHeader;
        //        //"Arial Unicode MS" is font name, "18" is font size
        //        centerHeader = "&\"Calibri,Bold\"&20 (" + cmbCust.Text + ") READY STOCK VERSUS FORECAST";

        //        string LeftHeader = xlWorkSheet.PageSetup.CenterHeader;
        //        //"Arial Unicode MS" is font name, "18" is font size
        //        LeftHeader = "&\"Calibri,Bold\"&20 " + DateTime.Now.Date.ToString("dd/MM/yyyy");

        //        string RightHeader = xlWorkSheet.PageSetup.CenterHeader;
        //        //"Arial Unicode MS" is font name, "18" is font size
        //        RightHeader = "&\"Calibri,Bold\"&20 PG -&P";

        //        xlWorkSheet.PageSetup.LeftHeader = LeftHeader;
        //        xlWorkSheet.PageSetup.CenterHeader = centerHeader;
        //        xlWorkSheet.PageSetup.RightHeader = RightHeader;
        //        xlWorkSheet.PageSetup.CenterFooter = "footer here hahahahahaha";

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

        //        Range rng = xlWorkSheet.get_Range("A:Q").Cells;
        //        rng.VerticalAlignment = XlHAlign.xlHAlignCenter;
        //        rng.RowHeight = 16;
        //        rng.EntireColumn.AutoFit();

        //        Range tRange = xlWorkSheet.UsedRange;
        //        tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //        tRange.Borders.Weight = XlBorderWeight.xlThin;

        //        //top row 
        //        Range topRow = xlWorkSheet.get_Range("a1:q1").Cells;
        //        topRow.RowHeight = 25;



        //        topRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        topRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
        //        topRow.BorderAround2(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);

        //        //first column: index 
        //        Range indexCol = xlWorkSheet.Columns[1];
        //        indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;






        //        for (int i = 0; i <= dgvForecastReport2.RowCount - 2; i++)
        //        {
        //            for (int j = 0; j <= dgvForecastReport2.ColumnCount - 1; j++)
        //            {
        //                Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

        //                if (i == 0)
        //                {
        //                    Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                    header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor);

        //                    header = (Range)xlWorkSheet.Cells[1, 10];
        //                    header.Font.Color = Color.Blue;

        //                    header = (Range)xlWorkSheet.Cells[1, 14];
        //                    header.Font.Color = Color.Red;

        //                    header = (Range)xlWorkSheet.Cells[1, 16];
        //                    header.Font.Color = Color.Red;


        //                }

        //                if (dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
        //                {
        //                    range.Interior.Color = ColorTranslator.ToOle(Color.White);
        //                    if (i == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(Color.White);
        //                    }
        //                }
        //                else if (dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
        //                {
        //                    range.Rows.RowHeight = 3;
        //                    range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor);
        //                    if (i == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor);
        //                    }
        //                }
        //                else
        //                {
        //                    range.Interior.Color = ColorTranslator.ToOle(dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor);

        //                    if (i == 0)
        //                    {
        //                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
        //                        header.Interior.Color = ColorTranslator.ToOle(dgvForecastReport2.Rows[i].Cells[j].InheritedStyle.BackColor);
        //                    }
        //                }
        //                range.Font.Color = dgvForecastReport2.Rows[i].Cells[j].Style.ForeColor;
        //                if (dgvForecastReport2.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
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
        //        dgvForecastReport2.ClearSelection();

        //        // Open the newly saved excel file
        //        if (File.Exists(sfd.FileName))
        //            System.Diagnostics.Process.Start(sfd.FileName);
        //    }
        //}

        //private void copyAlltoClipboard()
        //{
        //    dgvForecastReport2.SelectAll();
        //    DataObject dataObj = dgvForecastReport2.GetClipboardContent();
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

        //#endregion

        #endregion

    }
}
