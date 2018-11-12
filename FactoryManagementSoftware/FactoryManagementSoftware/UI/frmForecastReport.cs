using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport : Form
    {
        public frmForecastReport()
        {
            InitializeComponent();
        }

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

        private int colorOrder = 0;
        private string colorName = "Black";

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

        stockBLL uStock = new stockBLL();
        stockDAL dalStock = new stockDAL();

        itemCustBLL uItemCust = new itemCustBLL();
        itemCustDAL dalItemCust = new itemCustDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        forecastBLL uForecast = new forecastBLL();
        forecastDAL dalForecast = new forecastDAL();

        #endregion

        private void loadCustomerList()
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmbCust.DataSource = distinctTable;
            cmbCust.DisplayMember = "cust_name";
            cmbCust.SelectedIndex = -1;
        }

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
            dgvForecastReport.Columns["dateRequired"].HeaderCell.Style.ForeColor = Color.Red;

            dgvForecastReport.Columns["shotTwo"].HeaderCell.Style.ForeColor = Color.Red;

            dgvForecastReport.Columns["stock_qty"].HeaderCell.Style.BackColor = Color.Pink;
            dgvForecastReport.Columns["stock_qty"].HeaderCell.Style.ForeColor = Color.Blue;
        }

        private void emptyRowBackColorToBlack()
        {
            string colorName = "Black";
            foreach (DataGridViewRow row in dgvForecastReport.Rows)
            {
                if (row.Cells["item_name"].Value == null)
                {
                    row.Height = 3;
                    row.Cells["item_material"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["item_code"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["item_name"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["item_color"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["item_batch"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["dateRequired"].Style.BackColor = Color.FromName(colorName);

                    row.Cells["mc_ton"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["item_part_weight"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["item_runner_weight"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["forecast_one"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["outStock"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["oSant"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["shotOne"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["forecast_two"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["shotTwo"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["forecast_Three"].Style.BackColor = Color.FromName(colorName);
                    row.Cells["stock_qty"].Style.BackColor = Color.FromName(colorName);
                }
            }
        }

        private void frmForecastReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_Load(object sender, EventArgs e)
        {
            loadCustomerList();
            UIDesign();
        }

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

        private void searchForecastList()
        {
            //string keywords = cmbCust.Text;
            //string itemName = txtSearch.Text;
            //if (!string.IsNullOrEmpty(keywords) && !string.IsNullOrEmpty(itemName))
            //{
            //    DataTable dt = dalItemCust.forecastSearch(keywords,itemName);
            //    float outStock = 0;
            //    float forecastOne = 0;
            //    float forecastTwo = 0;
            //    dgvForecastReport.Rows.Clear();
            //    UIDesign();
            //    foreach (DataRow item in dt.Rows)
            //    {

            //        float f = 0;
            //        //if (!string.IsNullOrEmpty(item["item_weight"].ToString()))
            //        //{
            //        //    f = Convert.ToSingle(item["item_weight"]);
            //        //}

            //        int n = dgvForecastReport.Rows.Add();

            //        dgvForecastReport.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
            //        dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
            //        dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
            //        //dgvForecastReport.Rows[n].Cells["item_weight"].Value = f.ToString("0.00");

            //        DataTable dt2 = dalForecast.existsSearch(item["item_code"].ToString(), getCustID(keywords));
            //        forecastOne = 0;
            //        forecastTwo = 0;
            //        if (dt2.Rows.Count > 0)
            //        {
            //            foreach (DataRow forecast in dt2.Rows)
            //            {
            //                forecastOne = Convert.ToSingle(forecast["forecast_one"]);
            //                forecastTwo = Convert.ToSingle(forecast["forecast_two"]);
            //                string month = forecast["forecast_current_month"].ToString();
            //                dgvForecastReport.Columns["forecast_one"].HeaderText = "F/cast " + getShortMonth(month, 1);
            //                dgvForecastReport.Columns["forecast_two"].HeaderText = "F/cast " + getShortMonth(month, 2);
            //                dgvForecastReport.Columns["forecast_three"].HeaderText = "F/cast " + getShortMonth(month, 3);
            //                dgvForecastReport.Columns["shotOne"].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
            //                dgvForecastReport.Columns["shotTwo"].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
            //                dgvForecastReport.Rows[n].Cells["forecast_one"].Value = forecastOne.ToString();
            //                dgvForecastReport.Rows[n].Cells["forecast_two"].Value = forecastTwo.ToString();
            //                dgvForecastReport.Rows[n].Cells["forecast_three"].Value = forecast["forecast_three"].ToString();

            //                DataTable dt3 = daltrfHist.outSearch(cmbCust.Text, getMonthValue(month), item["item_code"].ToString());
            //                outStock = 0;
            //                if (dt3.Rows.Count > 0)
            //                {
            //                    foreach (DataRow outRecord in dt3.Rows)
            //                    {
            //                        outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
            //                    }
            //                }
            //                dgvForecastReport.Rows[n].Cells["outStock"].Value = outStock.ToString();
            //            }
            //        }
            //        else
            //        {
            //            dgvForecastReport.Rows[n].Cells["forecast_one"].Value = 0;
            //            dgvForecastReport.Rows[n].Cells["forecast_two"].Value = 0;
            //            dgvForecastReport.Rows[n].Cells["forecast_three"].Value = 0;
            //        }
            //        float readyStock = Convert.ToSingle(dalItem.getStockQty(item["item_code"].ToString()));
            //        dgvForecastReport.Rows[n].Cells["stock_qty"].Value = readyStock.ToString();
            //        float oSant = forecastOne - outStock;
            //        dgvForecastReport.Rows[n].Cells["oSant"].Value = oSant.ToString();

            //        float shotOne = 0;
            //        if (oSant > 0)
            //        {
            //            shotOne = readyStock - oSant;
            //        }
            //        else
            //        {
            //            shotOne = readyStock;
            //        }
            //        dgvForecastReport.Rows[n].Cells["shotOne"].Value = shotOne.ToString();

            //        float shotTwo = 0;
            //        shotTwo = shotOne - forecastTwo;
            //        dgvForecastReport.Rows[n].Cells["shotTwo"].Value = shotTwo.ToString();
            //    }
            //}
            //else
            //{
            //    dgvForecastReport.DataSource = null;
            //}
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

        private void changeBackColor(DataGridViewRow row, int rowIndex, bool type)//if type = true, change previous data back color, else change the new data
        {
            if(type)
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

                    if(Convert.ToSingle(row.Cells["shotOne"].Value) <0)
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

        private bool ifRepeat(string itemCode,int n,string fcast1, string fcast2, string fcast3, string outStock, float shot1, float shot2)
        {
            bool result = false;

            foreach (DataGridViewRow row in dgvForecastReport.Rows)
            {
                if(row.Cells["item_code"].Value == null)
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

                    if(shot1 < 0)
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
                    changeBackColor(row,0,true);
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

        private void checkChild(string itemCode, string month, string forecastOne, string forecastTwo, string forecastThree, float shotOne, float shotTwo, float outStock)
        {
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

                    if(dtItem.Rows.Count > 0)
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

                            dgvForecastReport.Rows[n].Cells["item_code"].Value = itemCode;
                            dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_batch"].Value = item["item_mb"].ToString();
                            dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();                            
                            dgvForecastReport.Rows[n].Cells["item_part_weight"].Value = partf.ToString("0.00");
                            dgvForecastReport.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");

                            if (ifRepeat(itemCode,n,forecastOne,forecastTwo,forecastThree,outStock.ToString(),shotOne,shotTwo))
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
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item["item_code"].ToString();

                    if (!ifGotChild(itemCode) && type != 2)
                    {
                        int n = dgvForecastReport.Rows.Add();

                    dgvForecastReport.Rows[n].Cells["item_code"].Value = itemCode;
                    dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_batch"].Value = item["item_mb"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_part_weight"].Value = item["item_part_weight"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_runner_weight"].Value = item["item_runner_weight"].ToString();

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
                        
                    if (shotOne < 0)
                    {
                        dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                    else
                    {
                        dgvForecastReport.Rows[n].Cells["shotOne"].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                    }

                        

                    if (shotTwo < 0)
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

                if(type != 1)
                {
                    int n = dgvForecastReport.Rows.Add();
                }

                //load parent and child data
            
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
                        checkChild(itemCode, month, forecastOne, forecastTwo, forecastThree, shotOne, shotTwo, Convert.ToSingle(outStock));

                        n = dgvForecastReport.Rows.Add();
                    }
                }
            }
            
           
            dgvForecastReport.ClearSelection();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            colorOrder = 0;
            btnCheck.Enabled = false;
            refreshData();
            emptyRowBackColorToBlack();
            btnCheck.Enabled = true;

            Cursor = Cursors.Arrow; // change cursor to normal type
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //if(!string.IsNullOrEmpty(txtSearch.Text))
            //{
            //    searchForecastList();
            //}
            //else
            //{
            //    loadForecastList();
            //}
        }

        private void exportgridtopdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 8,iTextSharp.text.Font.NORMAL);

            //Add header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240,240,240);
                pdftable.AddCell(cell);
            }

            //Add datarow
            foreach(DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PdfPCell cellSet = null;
                    string cellValue = "";

                    if(cell.Value != null)
                    {
                        cellValue = cell.Value.ToString();
                        
                    }

                    cellSet = new PdfPCell(new Phrase(cellValue,text));

                    if(cell.Style.BackColor.Name.ToString().Equals("0"))
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
            if(savefiledialog.ShowDialog() == DialogResult.OK)
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

        private void insertForecastData()
        {
            string custName = cmbCust.Text;

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    float outStock = 0;
                    float forecastOne = 0;
                    float forecastTwo = 0;
                    float forecastThree = 0;
                    float readyStock = 0;
                    float outSant = 0;
                    float shotOne = 0;
                    float shotTwo = 0;
                    string itemCode = "";
                    string month = "";
                   // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();
                        forecastOne = Convert.ToSingle(item["forecast_one"]);
                        forecastTwo = Convert.ToSingle(item["forecast_two"]);
                        forecastThree = Convert.ToSingle(item["forecast_three"]);
                        month = item["forecast_current_month"].ToString();

                        DataTable dt3 = daltrfHist.outSearch(cmbCust.Text, getMonthValue(month), itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
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
                        if(!result)
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

        private void refreshData()
        {
            bool result = dalForecast.Delete();

            if(!result)
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

                if(cmbType.SelectedIndex > 0)
                {
                    type = cmbType.SelectedIndex;
                }

                DataTable dt = dalForecast.Select(sort,order);
                loadForecastList(dt,type);
            }
        }
    }
}
