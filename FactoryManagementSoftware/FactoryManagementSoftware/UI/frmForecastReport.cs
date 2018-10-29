using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport : Form
    {
        public frmForecastReport()
        {
            InitializeComponent();
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

        private void loadForecastList()
        {
            //get keyword from text box
            string keywords = cmbCust.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItemCust.custSearch(keywords);

                dgvForecastReport.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvForecastReport.Rows.Add();
                    dgvForecastReport.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvForecastReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();

                    DataTable dt2 = dalForecast.existsSearch(item["item_code"].ToString(), getCustID(keywords));

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow forecast in dt2.Rows)
                        {
                            dgvForecastReport.Rows[n].Cells["forecast_one"].Value = forecast["forecast_one"].ToString();
                            dgvForecastReport.Rows[n].Cells["forecast_two"].Value = forecast["forecast_two"].ToString();
                            dgvForecastReport.Rows[n].Cells["forecast_three"].Value = forecast["forecast_three"].ToString();            
                        }
                    }
                    else
                    {
                        dgvForecastReport.Rows[n].Cells["forecast_one"].Value = 0;
                        dgvForecastReport.Rows[n].Cells["forecast_two"].Value = 0;
                        dgvForecastReport.Rows[n].Cells["forecast_three"].Value = 0;
                    }

                    dgvForecastReport.Rows[n].Cells["stock_qty"].Value = dalItem.getStockQty(item["item_code"].ToString()).ToString();


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
            loadForecastList();
        }
    }
}
