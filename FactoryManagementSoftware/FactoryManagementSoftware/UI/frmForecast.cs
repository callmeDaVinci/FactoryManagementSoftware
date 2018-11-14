using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecast : Form
    {
        public frmForecast()
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

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

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

        private void loadForecastList()
        {
            //get keyword from text box
            string keywords = cmbCust.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItemCust.custSearch(keywords);
                if(dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }

                dgvForecast.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvForecast.Rows.Add();
                    dgvForecast.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvForecast.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_one"].Value = item["forecast_one"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_two"].Value = item["forecast_two"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_three"].Value = item["forecast_three"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_updtd_date"].Value = item["forecast_updated_date"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_updtd_by"].Value = item["forecast_updated_by"].ToString();
                }
            }
            else
            {
                dgvForecast.DataSource = null;
            }
        }

        private void searchForecastList()
        {
            //get keyword from text box
            string keywords = txtSearch.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItemCust.nameSearch(keywords, cmbCust.Text);

                dgvForecast.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvForecast.Rows.Add();
                    dgvForecast.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvForecast.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();

                    dgvForecast.Rows[n].Cells["forecast_one"].Value = item["forecast_one"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_two"].Value = item["forecast_two"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_three"].Value = item["forecast_three"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_updtd_date"].Value = item["forecast_updated_date"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_updtd_by"].Value = item["forecast_updated_by"].ToString();
                       
                }              
            }
            else
            {
                loadForecastList();
            }
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

        private bool IfExists(string itemCode, string custName)
        {
          
            DataTable dt = dalItemCust.existsSearch(itemCode, getCustID(custName));

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void frmForecast_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastInputFormOpen = false;
        }

        private void cmbForecast1_SelectedIndexChanged(object sender, EventArgs e)
        {  
            var index1 = cmbForecast1.SelectedIndex;
            var index2 = index1;
            var index3 = index1;

            if(index1 == 11)
            {
                index2 = 0;
            }
            else
            {
                index2 = index1 + 1;
            }
            cmbForecast2.SelectedIndex = index2;

            if (index2 == 11)
            {
                index3 = 0;
            }
            else
            {
                index3 = index2 + 1;
            }
            cmbForecast3.SelectedIndex = index3;


        }

        private void cmbForecast2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index2 = cmbForecast2.SelectedIndex;
            var index1 = index2;
            var index3 = index2;

            if (index2 == 0)
            {
                index1 = 11;
            }
            else
            {
                index1 = index2 - 1;
            }
            cmbForecast1.SelectedIndex = index1;

            if (index2 == 11)
            {
                index3 = 0;
            }
            else
            {
                index3 = index2 + 1;
            }
            cmbForecast3.SelectedIndex = index3;
        }

        private void cmbForecast3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index3 = cmbForecast3.SelectedIndex;
            var index1 = index3;
            var index2 = index3;

            if (index3 == 0)
            {
                index2 = 11;
            }
            else
            {
                index2 = index3 - 1;
            }
            cmbForecast2.SelectedIndex = index2;

            if (index2 == 0)
            {
                index1 = 11;
            }
            else
            {
                index1 = index2 - 1;
            }
            cmbForecast1.SelectedIndex = index1;
        }

        private void frmForecast_Load(object sender, EventArgs e)
        {
            loadCustomerList();
            cmbCust.SelectedIndex = -1;

            var currentMonth = Convert.ToInt32(DateTime.Now.Month.ToString("00"));


            cmbForecast1.SelectedIndex = currentMonth - 1;
        }

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvForecast_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var datagridview = sender as DataGridView;

            int rowIndex = e.RowIndex;
            dgvForecast.Rows[rowIndex].Cells["forecast_updtd_date"].Value = DateTime.Now;
            dgvForecast.Rows[rowIndex].Cells["forecast_updtd_by"].Value = 1;

            uItemCust.item_code = dgvForecast.Rows[rowIndex].Cells["item_code"].Value.ToString();
            uItemCust.cust_id = Convert.ToInt32(getCustID(cmbCust.Text));

            uItemCust.forecast_one = Convert.ToSingle(dgvForecast.Rows[rowIndex].Cells["forecast_one"].Value.ToString());
            uItemCust.forecast_two = Convert.ToSingle(dgvForecast.Rows[rowIndex].Cells["forecast_two"].Value.ToString());
            uItemCust.forecast_three = Convert.ToSingle(dgvForecast.Rows[rowIndex].Cells["forecast_three"].Value.ToString());
            uItemCust.forecast_updated_date = Convert.ToDateTime(dgvForecast.Rows[rowIndex].Cells["forecast_updtd_date"].Value);
            uItemCust.forecast_current_month = cmbForecast1.Text;
            uItemCust.forecast_updated_by = 1;

            if (IfExists(uItemCust.item_code, cmbCust.Text))
            {
                bool success = dalItemCust.Update(uItemCust);

                if (!success)
                {
                    MessageBox.Show("Failed to updated forecast");
                }     
            }
            else
            {             
                bool success = dalItemCust.Insert(uItemCust);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    MessageBox.Show("Failed to add new forecast");
                }
            }

        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvForecast_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);

            if (dgvForecast.CurrentCell.ColumnIndex == 2 || dgvForecast.CurrentCell.ColumnIndex == 3 || dgvForecast.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            loadForecastList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchForecastList();
        }
    }
}
