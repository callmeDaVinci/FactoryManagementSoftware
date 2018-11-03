using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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
            int month = (int)(Month)Enum.Parse(typeof(Month), keyword);

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

        private void UIDesign()
        {
            dgvForecastReport.Columns["mc_ton"].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
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

        private void searchForecastList()
        {
            string keywords = cmbCust.Text;
            string itemName = txtSearch.Text;
            if (!string.IsNullOrEmpty(keywords) && !string.IsNullOrEmpty(itemName))
            {
                DataTable dt = dalItemCust.forecastSearch(keywords,itemName);
                float outStock = 0;
                float forecastOne = 0;
                float forecastTwo = 0;
                dgvForecastReport.Rows.Clear();
                UIDesign();
                foreach (DataRow item in dt.Rows)
                {

                    float f = 0;
                    if (!string.IsNullOrEmpty(item["item_weight"].ToString()))
                    {
                        f = Convert.ToSingle(item["item_weight"]);
                    }

                    int n = dgvForecastReport.Rows.Add();

                    dgvForecastReport.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_weight"].Value = f.ToString("0.00");

                    DataTable dt2 = dalForecast.existsSearch(item["item_code"].ToString(), getCustID(keywords));
                    forecastOne = 0;
                    forecastTwo = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow forecast in dt2.Rows)
                        {
                            forecastOne = Convert.ToSingle(forecast["forecast_one"]);
                            forecastTwo = Convert.ToSingle(forecast["forecast_two"]);
                            string month = forecast["forecast_current_month"].ToString();
                            dgvForecastReport.Columns["forecast_one"].HeaderText = "F/cast " + getShortMonth(month, 1);
                            dgvForecastReport.Columns["forecast_two"].HeaderText = "F/cast " + getShortMonth(month, 2);
                            dgvForecastReport.Columns["forecast_three"].HeaderText = "F/cast " + getShortMonth(month, 3);
                            dgvForecastReport.Columns["shotOne"].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                            dgvForecastReport.Columns["shotTwo"].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
                            dgvForecastReport.Rows[n].Cells["forecast_one"].Value = forecastOne.ToString();
                            dgvForecastReport.Rows[n].Cells["forecast_two"].Value = forecastTwo.ToString();
                            dgvForecastReport.Rows[n].Cells["forecast_three"].Value = forecast["forecast_three"].ToString();

                            DataTable dt3 = daltrfHist.outSearch(cmbCust.Text, getMonthValue(month), item["item_code"].ToString());
                            outStock = 0;
                            if (dt3.Rows.Count > 0)
                            {
                                foreach (DataRow outRecord in dt3.Rows)
                                {
                                    outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
                                }
                            }
                            dgvForecastReport.Rows[n].Cells["outStock"].Value = outStock.ToString();
                        }
                    }
                    else
                    {
                        dgvForecastReport.Rows[n].Cells["forecast_one"].Value = 0;
                        dgvForecastReport.Rows[n].Cells["forecast_two"].Value = 0;
                        dgvForecastReport.Rows[n].Cells["forecast_three"].Value = 0;
                    }
                    float readyStock = Convert.ToSingle(dalItem.getStockQty(item["item_code"].ToString()));
                    dgvForecastReport.Rows[n].Cells["stock_qty"].Value = readyStock.ToString();
                    float oSant = forecastOne - outStock;
                    dgvForecastReport.Rows[n].Cells["oSant"].Value = oSant.ToString();

                    float shotOne = 0;
                    if (oSant > 0)
                    {
                        shotOne = readyStock - oSant;
                    }
                    else
                    {
                        shotOne = readyStock;
                    }
                    dgvForecastReport.Rows[n].Cells["shotOne"].Value = shotOne.ToString();

                    float shotTwo = 0;
                    shotTwo = shotOne - forecastTwo;
                    dgvForecastReport.Rows[n].Cells["shotTwo"].Value = shotTwo.ToString();
                }
            }
            else
            {
                dgvForecastReport.DataSource = null;
            }
        }

        private void loadForecastList()
        {
            //get keyword from text box
            string keywords = cmbCust.Text;
            //bool rowColorChange = true;
            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                
                DataTable dt = dalItemCust.custSearch(keywords);

                if(dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                float outStock = 0;
                float forecastOne = 0;
                float forecastTwo = 0;
                dgvForecastReport.Rows.Clear();

                UIDesign();

                foreach (DataRow item in dt.Rows)
                {
                    
                    float partf = 0;
                    float runnerf = 0;
                    if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
                    {
                        partf = Convert.ToSingle(item["item_part_weight"]);
                    }
                    if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
                    {
                        runnerf = Convert.ToSingle(item["item_runner_weight"]);
                    }

                    int n = dgvForecastReport.Rows.Add();

                    //if (rowColorChange)
                    //{
                    //    dgvForecastReport.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
                    //    rowColorChange = false;
                    //}
                    //else
                    //{
                    //    dgvForecastReport.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    //    rowColorChange = true;
                    //}

                    dgvForecastReport.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_part_weight"].Value = partf.ToString("0.00");
                    dgvForecastReport.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");

                    DataTable dt2 = dalForecast.existsSearch(item["item_code"].ToString(), getCustID(keywords));
                    forecastOne = 0;
                    forecastTwo = 0;
                    if (dt2.Rows.Count > 0)
                    {

                        
                        foreach (DataRow forecast in dt2.Rows)
                        {
                            forecastOne = Convert.ToSingle(forecast["forecast_one"]);
                            forecastTwo = Convert.ToSingle(forecast["forecast_two"]);
                            string month = forecast["forecast_current_month"].ToString();
                            dgvForecastReport.Columns["forecast_one"].HeaderText = "F/cast "+getShortMonth(month,1);
                            dgvForecastReport.Columns["forecast_two"].HeaderText = "F/cast " + getShortMonth(month, 2);
                            dgvForecastReport.Columns["forecast_three"].HeaderText = "F/cast " + getShortMonth(month, 3);
                            dgvForecastReport.Columns["shotOne"].HeaderText = "SHOT FOR " + getShortMonth(month, 1);
                            dgvForecastReport.Columns["shotTwo"].HeaderText = "SHOT FOR " + getShortMonth(month, 2);
                            dgvForecastReport.Rows[n].Cells["forecast_one"].Value = forecastOne.ToString();
                            dgvForecastReport.Rows[n].Cells["forecast_two"].Value = forecastTwo.ToString();
                            dgvForecastReport.Rows[n].Cells["forecast_three"].Value = forecast["forecast_three"].ToString();

                            DataTable dt3 = daltrfHist.outSearch(cmbCust.Text,getMonthValue(month),item["item_code"].ToString());
                            outStock = 0;
                            if(dt3.Rows.Count > 0)
                            {
                                foreach (DataRow outRecord in dt3.Rows)
                                {
                                    outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
                                }
                            }
                            dgvForecastReport.Rows[n].Cells["outStock"].Value = outStock.ToString();
                           
                        }
                    }
                    else
                    {
                        dgvForecastReport.Rows[n].Cells["forecast_one"].Value = 0;
                        dgvForecastReport.Rows[n].Cells["forecast_two"].Value = 0;
                        dgvForecastReport.Rows[n].Cells["forecast_three"].Value = 0;
                    }
                    float readyStock = Convert.ToSingle(dalItem.getStockQty(item["item_code"].ToString()));
                    dgvForecastReport.Rows[n].Cells["stock_qty"].Value = readyStock.ToString();
                    float oSant = forecastOne - outStock ;
                    dgvForecastReport.Rows[n].Cells["oSant"].Value = oSant.ToString();

                    float shotOne = 0;
                    if(oSant > 0)
                    {
                        shotOne = readyStock - oSant;
                    }
                    else
                    {
                        shotOne = readyStock;
                    }
                    dgvForecastReport.Rows[n].Cells["shotOne"].Value = shotOne.ToString();

                    float shotTwo = 0;
                    shotTwo = shotOne - forecastTwo;
                    dgvForecastReport.Rows[n].Cells["shotTwo"].Value = shotTwo.ToString();
                }
            }
            else
            {
                dgvForecastReport.DataSource = null;
            }


        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string custName = cmbCust.Text;
            int custID = Convert.ToInt32(getCustID(custName));

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            btnCheck.Enabled = false;
            loadForecastList();
            Cursor = Cursors.Arrow; // change cursor to normal type
            btnCheck.Enabled = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtSearch.Text))
            {
                searchForecastList();
            }
            else
            {
                loadForecastList();
            }
        }
    }
}
