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

        Tool tool = new Tool();
        #endregion  

        #region Form Load/Close

        private void frmForecastReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_Load(object sender, EventArgs e)
        {
            tool.loadCustomerToComboBox(cmbCust);
            UIDesign();
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

        private void UIDesign()
        {
            dgvForecastReport.Columns["mc_ton"].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            dgvForecastReport.Columns["mc_ton"].Width = 40;
            dgvForecastReport.Columns["item_part_weight"].HeaderCell.Style.BackColor = Color.LightYellow;
            dgvForecastReport.Columns["item_runner_weight"].HeaderCell.Style.BackColor = Color.LightYellow;
            dgvForecastReport.Columns["forecast_one"].HeaderCell.Style.BackColor = Color.LightYellow;
            dgvForecastReport.Columns["outStock"].HeaderCell.Style.BackColor = Color.LightYellow;
            dgvForecastReport.Columns["oSant"].HeaderCell.Style.BackColor = Color.LightYellow;

            dgvForecastReport.Columns["shotOne"].HeaderCell.Style.BackColor = Color.LightYellow;
            dgvForecastReport.Columns["shotOne"].HeaderCell.Style.ForeColor = Color.Red;

            dgvForecastReport.Columns["forecast_two"].HeaderCell.Style.BackColor = Color.LightCyan;
            dgvForecastReport.Columns["shotTwo"].HeaderCell.Style.BackColor = Color.LightCyan;
            dgvForecastReport.Columns["shotTwo"].HeaderCell.Style.ForeColor = Color.Red;

            dgvForecastReport.Columns["forecast_Three"].HeaderCell.Style.BackColor = Color.PeachPuff;
          

            dgvForecastReport.Columns["shotTwo"].HeaderCell.Style.ForeColor = Color.Red;

            dgvForecastReport.Columns["stock_qty"].HeaderCell.Style.BackColor = Color.Pink;
            dgvForecastReport.Columns["stock_qty"].HeaderCell.Style.ForeColor = Color.Blue;
        }

        private void emptyRowBackColorToBlack()
        {
            float shotOne = redAlertLevel;
            float shotTwo = redAlertLevel;

            string colorName = "Black";
            foreach (DataGridViewRow row in dgvForecastReport.Rows)
            {
                if(row.Cells["shotOne"].Value != null)
                {
                    shotOne = Convert.ToSingle(row.Cells["shotOne"].Value);
                }
                
                if(shotOne < redAlertLevel)
                {
                    row.Cells["shotOne"].Style.ForeColor = Color.Red;
                }
                else if(row.Cells["item_batch"].Value != null)
                {
                    row.Cells["shotOne"].Style.ForeColor = Color.Black;
                }

                if (row.Cells["shotTwo"].Value != null)
                {
                    shotTwo = Convert.ToSingle(row.Cells["shotTwo"].Value);
                }

                if (shotTwo < redAlertLevel)
                {
                    row.Cells["shotTwo"].Style.ForeColor = Color.Red;
                }
                else if(row.Cells["item_batch"].Value != null)
                {
                    row.Cells["shotTwo"].Style.ForeColor = Color.Black;
                }

                if (row.Cells["item_name"].Value == null)
                {
                    row.Height = 3;
                    row.DefaultCellStyle.BackColor = Color.FromName(colorName);
                }
            }
        }

        private void changeBackColor(DataGridViewRow row, int rowIndex, bool type)//if type = true, change previous data back color, else change the new data
        {
            if (type)
            {
                string test = row.Cells["forecast_one"].Style.BackColor.ToKnownColor().ToString();

                if (test.Equals("0"))
                {
                    row.Cells["item_code"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells["item_name"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells["forecast_one"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells["forecast_two"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells["forecast_three"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells["outStock"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                    row.Cells["oSant"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                    if (Convert.ToSingle(row.Cells["shotOne"].Value) < 0)
                    {
                        row.Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                    }
                    else
                    {
                        row.Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                    }

                    if (Convert.ToSingle(row.Cells["shotTwo"].Value) < 0)
                    {
                        row.Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                    }
                    else
                    {
                        row.Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                    }
                }
                else
                {
                    colorName = test;
                }
            }
            else
            {
                dgvForecastReport.Rows[rowIndex].Cells["item_code"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells["item_name"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells["forecast_one"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells["forecast_two"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells["forecast_three"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells["outStock"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };
                dgvForecastReport.Rows[rowIndex].Cells["oSant"].Style = new DataGridViewCellStyle { BackColor = Color.FromName(colorName) };

                if (Convert.ToSingle(dgvForecastReport.Rows[rowIndex].Cells["shotOne"].Value) < 0)
                {
                    dgvForecastReport.Rows[rowIndex].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                }
                else
                {
                    dgvForecastReport.Rows[rowIndex].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
                }

                if (Convert.ToSingle(dgvForecastReport.Rows[rowIndex].Cells["shotTwo"].Value) < 0)
                {
                    dgvForecastReport.Rows[rowIndex].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.FromName(colorName) };
                }
                else
                {
                    dgvForecastReport.Rows[rowIndex].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = Color.FromName(colorName) };
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

        private void refreshData()
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

                DataTable dt = dalForecast.Select(sort, order);
                loadForecastList(dt, type); 
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

                                outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
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
                        uForecast.forecast_updtd_by = -1;

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
        }//generate forecast data 

        private bool ifRepeat(string itemCode, int n, string fcast1, string fcast2, string fcast3, string outStock, float shot1, float shot2)
        {
            bool result = false;

            foreach (DataGridViewRow row in dgvForecastReport.Rows)
            {
                if (row.Cells["item_code"].Value == null)
                {
                    row.Cells["item_code"].Value = "";
                }
                if (row.Cells["item_code"].Value.ToString().Equals(itemCode) && row.Index < n)
                {
                    float headFcast1 = Convert.ToSingle(row.Cells["forecast_one"].Value);
                    float headFcast2 = Convert.ToSingle(row.Cells["forecast_two"].Value);
                    float headFcast3 = Convert.ToSingle(row.Cells["forecast_three"].Value);
                    float headOutStock = Convert.ToSingle(row.Cells["outStock"].Value);
                    float headShot1 = Convert.ToSingle(row.Cells["shotOne"].Value);
                    float headShot2 = Convert.ToSingle(row.Cells["shotTwo"].Value);

                    float childFcast1 = Convert.ToSingle(fcast1);
                    float childFcast2 = Convert.ToSingle(fcast2);
                    float childFcast3 = Convert.ToSingle(fcast3);
                    float childOutStock = Convert.ToSingle(outStock);

                    if (shot1 < 0)
                    {
                        headShot1 += shot1;
                    }

                    if (shot2 < 0)
                    {
                        headShot2 += shot2;
                    }

                    headFcast1 += childFcast1;
                    headFcast2 += childFcast2;
                    headFcast3 += childFcast3;
                    headOutStock += childOutStock;

                    float headOutSant = headFcast1 - headOutStock;

                    row.Cells["forecast_one"].Value = headFcast1.ToString();
                    row.Cells["forecast_two"].Value = headFcast2.ToString();
                    row.Cells["forecast_three"].Value = headFcast3.ToString();
                    row.Cells["outStock"].Value = headOutStock.ToString();
                    row.Cells["shotOne"].Value = headShot1.ToString();
                    row.Cells["shotTwo"].Value = headShot2.ToString();
                    row.Cells["oSant"].Value = headOutSant.ToString();

                    colorName = ((color)colorOrder).ToString();
                    changeBackColor(row, 0, true);
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

        private void checkChild(string itemCode, string month, string forecastOne, string forecastTwo, string forecastThree, float shotOne, float shotTwo, float outStock,int no)
        {
            string parentItemCode = itemCode;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    float partf = 0;
                    float runnerf = 0;
                    float childShotOne = shotOne;
                    float childShotTwo = shotTwo;
                    int n = dgvForecastReport.Rows.Add();

                    dgvForecastReport.Rows[n].Cells["item_code"].Value = Join["join_child_code"].ToString();

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
                                dgvForecastReport.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgvForecastReport.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Bold) };
                            }
                            dgvForecastReport.Rows[n].Cells["index"].Value = no.ToString()+"("+(char)alphbet+")";
                            dgvForecastReport.Rows[n].Cells["item_code"].Value = itemCode;
                            dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_batch"].Value = item["item_mb"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_part_weight"].Value = partf.ToString("0.00");
                            dgvForecastReport.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");

                            if (ifRepeat(itemCode, n, forecastOne, forecastTwo, forecastThree, outStock.ToString(), shotOne, shotTwo))
                            {
                                changeBackColor(null, n, false);
                            }
                            else
                            {
                                dgvForecastReport.Rows[n].Cells["forecast_one"].Value = forecastOne;
                                dgvForecastReport.Rows[n].Cells["forecast_two"].Value = forecastTwo;
                                dgvForecastReport.Rows[n].Cells["forecast_three"].Value = forecastThree;
                                dgvForecastReport.Rows[n].Cells["outStock"].Value = outStock.ToString();

                                float readyStock = Convert.ToSingle(dalItem.getStockQty(itemCode));
                                dgvForecastReport.Rows[n].Cells["stock_qty"].Style.Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Regular);

                                dgvForecastReport.Rows[n].Cells["stock_qty"].Value = readyStock.ToString();

                                float oSant = Convert.ToSingle(forecastOne) - outStock;
                                dgvForecastReport.Rows[n].Cells["oSant"].Value = oSant.ToString();


                                if (childShotOne >= 0)
                                {
                                    childShotOne = readyStock;
                                    dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = dgvForecastReport.Rows[n].Cells["shotOne"].Style.BackColor };

                                }
                                else
                                {
                                    childShotOne += readyStock;
                                    dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = dgvForecastReport.Rows[n].Cells["shotOne"].Style.BackColor };

                                }

                                dgvForecastReport.Rows[n].Cells["shotOne"].Value = childShotOne.ToString();

                                if (childShotTwo >= 0)
                                {
                                    childShotTwo = childShotOne;
                                    dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, BackColor = dgvForecastReport.Rows[n].Cells["shotTwo"].Style.BackColor };

                                }
                                else
                                {
                                    childShotTwo += childShotOne;
                                    dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = dgvForecastReport.Rows[n].Cells["shotTwo"].Style.BackColor };

                                }

                                dgvForecastReport.Rows[n].Cells["shotTwo"].Value = childShotTwo.ToString();
                            }
                            alphbet++;
                        }
                    }
                }
            }
        }

        private void loadForecastList(DataTable dataTable, int type)
        {
            DataTable dt = dataTable;

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                dgvForecastReport.Rows.Clear();

                UIDesign();

                //load single data
                indexNo = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item["item_code"].ToString();

                    if (!ifGotChild(itemCode) && type != 2)
                    {
                        int n = dgvForecastReport.Rows.Add();

                        dgvForecastReport.Rows[n].Cells["index"].Value = indexNo.ToString();
                        indexNo++;
                        dgvForecastReport.Rows[n].Cells["item_code"].Value = itemCode;
                        dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                        dgvForecastReport.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                        dgvForecastReport.Rows[n].Cells["item_batch"].Value = item["item_mb"].ToString();
                        dgvForecastReport.Rows[n].Cells["item_part_weight"].Value = Convert.ToSingle(item["item_part_weight"]).ToString("0.00");
                        dgvForecastReport.Rows[n].Cells["item_runner_weight"].Value = Convert.ToSingle(item["item_runner_weight"]).ToString("0.00"); 

                        string month = item["forecast_current_month"].ToString();

                        dgvForecastReport.Columns["forecast_one"].HeaderText = "F/cast " + getShortMonth(month, 1);
                        dgvForecastReport.Columns["forecast_two"].HeaderText = "F/cast " + getShortMonth(month, 2);
                        dgvForecastReport.Columns["forecast_three"].HeaderText = "F/cast " + getShortMonth(month, 3);
                        dgvForecastReport.Columns["shotOne"].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                        dgvForecastReport.Columns["shotTwo"].HeaderText = "SHOT FOR " + getShortMonth(month, 2);

                        dgvForecastReport.Rows[n].Cells["forecast_one"].Value = item["forecast_one"].ToString();
                        dgvForecastReport.Rows[n].Cells["forecast_two"].Value = item["forecast_two"].ToString();
                        dgvForecastReport.Rows[n].Cells["forecast_three"].Value = item["forecast_three"].ToString();

                        dgvForecastReport.Rows[n].Cells["outStock"].Value = item["forecast_out_stock"].ToString();
                        dgvForecastReport.Rows[n].Cells["stock_qty"].Value = item["forecast_ready_stock"].ToString();
                        dgvForecastReport.Rows[n].Cells["oSant"].Value = item["forecast_osant"].ToString();

                        float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
                        float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);

                        if (shotOne < redAlertLevel)
                        {
                            dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                        {
                            dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }



                        if (shotTwo < redAlertLevel)
                        {
                            dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                        {
                            dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }

                        dgvForecastReport.Rows[n].Cells["shotOne"].Value = shotOne.ToString();
                        dgvForecastReport.Rows[n].Cells["shotTwo"].Value = shotTwo.ToString();
                    }
                    
                }

                if (type != 1)
                {
                    int n = dgvForecastReport.Rows.Add();
                }

                //load parent and child data
                indexNo = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item["item_code"].ToString();

                    if (ifGotChild(itemCode) && type != 1)
                    {
                        int n = dgvForecastReport.Rows.Add();

                        float shotOne = Convert.ToSingle(item["forecast_shot_one"]);
                        float shotTwo = Convert.ToSingle(item["forecast_shot_two"]);
                        string forecastOne = item["forecast_one"].ToString();
                        string forecastTwo = item["forecast_two"].ToString();
                        string forecastThree = item["forecast_three"].ToString();
                        string month = item["forecast_current_month"].ToString();
                        string outStock = item["forecast_out_stock"].ToString();

                        dgvForecastReport.Rows[n].Cells["index"].Value = indexNo.ToString();
                        dgvForecastReport.Rows[n].Cells["index"].Style = new DataGridViewCellStyle { ForeColor = Color.Black, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };

                        dgvForecastReport.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["item_code"].Value = itemCode;
                        dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                        dgvForecastReport.Columns["forecast_one"].HeaderText = "F/cast " + getShortMonth(month, 1);
                        dgvForecastReport.Columns["forecast_two"].HeaderText = "F/cast " + getShortMonth(month, 2);
                        dgvForecastReport.Columns["forecast_three"].HeaderText = "F/cast " + getShortMonth(month, 3);
                        dgvForecastReport.Columns["shotOne"].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                        dgvForecastReport.Columns["shotTwo"].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
                        dgvForecastReport.Rows[n].Cells["forecast_one"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["forecast_two"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["forecast_three"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["outStock"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["stock_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["oSant"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };

                        dgvForecastReport.Rows[n].Cells["forecast_one"].Value = forecastOne;
                        dgvForecastReport.Rows[n].Cells["forecast_two"].Value = forecastTwo;
                        dgvForecastReport.Rows[n].Cells["forecast_three"].Value = forecastThree;

                        dgvForecastReport.Rows[n].Cells["outStock"].Value = outStock;
                        dgvForecastReport.Rows[n].Cells["stock_qty"].Value = item["forecast_ready_stock"].ToString();
                        dgvForecastReport.Rows[n].Cells["oSant"].Value = item["forecast_osant"].ToString();

                        if (shotOne < 0)
                        {
                            dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        }
                        else
                        {
                            dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        }

                        if (shotTwo < 0)
                        {
                            dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        }
                        else
                        {
                            dgvForecastReport.Rows[n].Cells["shotTwo"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgvForecastReport.Font, FontStyle.Bold) };
                        }

                        dgvForecastReport.Rows[n].Cells["shotOne"].Value = item["forecast_shot_one"].ToString();
                        dgvForecastReport.Rows[n].Cells["shotTwo"].Value = item["forecast_shot_two"].ToString();
                        checkChild(itemCode, month, forecastOne, forecastTwo, forecastThree, shotOne, shotTwo, Convert.ToSingle(outStock),indexNo);
                        indexNo++;
                        n = dgvForecastReport.Rows.Add();
                    }
                   
                    alphbet = 65;
                }
            }


            dgvForecastReport.ClearSelection();
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
            
            loadForecastList(dt, type);
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

            colorOrder = 0;
            btnCheck.Enabled = false;
            refreshData();
            emptyRowBackColorToBlack();
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
            emptyRowBackColorToBlack();
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
                dt.Rows.Add(desc);
                cmbOrder.DataSource = dt;
                cmbOrder.DisplayMember = "order";
                //cmbOrder.SelectedIndex = 0;
            }
            
        }

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;
        }

        private void frmForecastReport_Click(object sender, EventArgs e)
        {
            dgvForecastReport.ClearSelection();
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now.Date;
            fileName = "ForecastReport(" + cmbCust.Text + ")_" + currentDate.ToString("ddMMyyyy") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
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

    }
}
