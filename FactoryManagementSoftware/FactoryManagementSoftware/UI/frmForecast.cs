using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecast : Form
    {
        string oldMonth;
        string oldForecast, newForecast;
        string forecastNum;
        bool loaded = false;
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

        userDAL dalUser = new userDAL();

        Tool tool = new Tool();
        Text text = new Text();

        #endregion

        #region load/search 

        private void loadForecastList()
        {
            //get keyword from text box
            string keywords = cmbCust.Text;
            int index = 1;
            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItemCust.custSearch(keywords);

                dt.DefaultView.Sort = "item_name ASC";
                dt = dt.DefaultView.ToTable();

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }

                dgvForecast.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvForecast.Rows.Add();
                    dgvForecast.Rows[n].Cells["NO"].Value = index;
                    dgvForecast.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvForecast.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_one"].Value = item["forecast_one"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_two"].Value = item["forecast_two"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_three"].Value = item["forecast_three"].ToString();
                    dgvForecast.Rows[n].Cells["forecast_updtd_date"].Value = item["forecast_updated_date"].ToString();
                    index++;
                    if (int.TryParse(item["forecast_updated_by"].ToString(), out int test))
                    {
                        if (Convert.ToInt32(item["forecast_updated_by"]) <= 0)
                        {
                            dgvForecast.Rows[n].Cells["forecast_updtd_by"].Value = "ADMIN";
                        }
                        else
                        {
                            dgvForecast.Rows[n].Cells["forecast_updtd_by"].Value = dalUser.getUsername(Convert.ToInt32(item["forecast_updated_by"]));
                        } 
                    }

                    string currentMonth = item["forecast_current_month"]?.ToString();

                    resetCurrentMonth(item["item_code"].ToString(),n,currentMonth);
                }
            }
            else
            {
                dgvForecast.DataSource = null;
            }
            tool.listPaintGreyHeader(dgvForecast);
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

                    if(Convert.ToInt32(item["forecast_updated_by"]) <= 0)
                    {
                        dgvForecast.Rows[n].Cells["forecast_updtd_by"].Value = "ADMIN";
                    }
                    else
                    {
                        dgvForecast.Rows[n].Cells["forecast_updtd_by"].Value = dalUser.getUsername(Convert.ToInt32(item["forecast_updated_by"]));
                    }     
                }              
            }
            else
            {
                loadForecastList();
            }
        }

        private void frmForecast_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastInputFormOpen = false;
        }

        private void frmForecast_Load(object sender, EventArgs e)
        {
            tool.loadCustomerToComboBox(cmbCust);
            cmbCust.SelectedIndex = -1;

            var currentMonth = Convert.ToInt32(DateTime.Now.Month.ToString("00"));


            cmbForecast1.SelectedIndex = currentMonth - 1;
            oldMonth = cmbForecast1.Text;
            loaded = true;
        }

        #endregion

        #region selected index/text changed

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
            string newMonth = cmbForecast1.Text;
            

            if(loaded)
            tool.historyRecord(text.ForecastEdit, text.getForecastMonthEditString(oldMonth, newMonth), DateTime.Now, MainDashboard.USER_ID);

            oldMonth = newMonth;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchForecastList();
        }

        #endregion 


        private void dgvForecast_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var datagridview = sender as DataGridView;

            int rowIndex = e.RowIndex;
            dgvForecast.Rows[rowIndex].Cells["forecast_updtd_date"].Value = DateTime.Now;
            dgvForecast.Rows[rowIndex].Cells["forecast_updtd_by"].Value = dalUser.getUsername(MainDashboard.USER_ID);

            uItemCust.item_code = dgvForecast.Rows[rowIndex].Cells["item_code"].Value.ToString();
            uItemCust.cust_id = tool.getCustID(cmbCust.Text);

            uItemCust.forecast_one = Convert.ToSingle(dgvForecast.Rows[rowIndex].Cells["forecast_one"].Value.ToString());
            uItemCust.forecast_two = Convert.ToSingle(dgvForecast.Rows[rowIndex].Cells["forecast_two"].Value.ToString());
            uItemCust.forecast_three = Convert.ToSingle(dgvForecast.Rows[rowIndex].Cells["forecast_three"].Value.ToString());
            uItemCust.forecast_updated_date = Convert.ToDateTime(dgvForecast.Rows[rowIndex].Cells["forecast_updtd_date"].Value);
            uItemCust.forecast_current_month = cmbForecast1.Text;
            uItemCust.forecast_updated_by = MainDashboard.USER_ID;

            if(Convert.ToInt32(forecastNum) == 1)
            {
                newForecast = uItemCust.forecast_one.ToString();
            }
            else if (Convert.ToInt32(forecastNum) == 2)
            {
                newForecast = uItemCust.forecast_two.ToString();
            }
            else
            {
                newForecast = uItemCust.forecast_three.ToString();
            }
             
            if (tool.IfExists(uItemCust.item_code, cmbCust.Text))
            {
                bool success = dalItemCust.Update(uItemCust);

                if (!success)
                {
                    MessageBox.Show("Failed to updated forecast");
                    tool.historyRecord(text.System, "Failed to updated forecast(frmForecast)", DateTime.Now, MainDashboard.USER_ID);
                }    
                else
                {
                    tool.historyRecord(text.ForecastEdit,text.getForecastEditString(cmbCust.Text,forecastNum, uItemCust.item_code,oldForecast,newForecast),DateTime.Now,MainDashboard.USER_ID);
                }
            }
            else
            {             
                bool success = dalItemCust.Insert(uItemCust);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    MessageBox.Show("Failed to add new forecast");
                    tool.historyRecord(text.System, "Failed to add new forecast(frmForecast)", DateTime.Now, MainDashboard.USER_ID);

                }
            }
        }

        private void resetCurrentMonth(string itemCode, int rowIndex, string currentMonth)
        {         
            uItemCust.item_code = itemCode;
            uItemCust.cust_id = tool.getCustID(cmbCust.Text);

            if (!currentMonth.Equals(cmbForecast1.Text))
            {
                uItemCust.forecast_updated_date = DateTime.Now;
                uItemCust.forecast_current_month = cmbForecast1.Text;
                uItemCust.forecast_updated_by = MainDashboard.USER_ID;
                bool success = dalItemCust.monthUpdate(uItemCust);

                if (!success)
                {
                    MessageBox.Show("Failed to updated forecast");
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
                forecastNum = (dgvForecast.CurrentCell.ColumnIndex - 1).ToString();
                oldForecast = dgvForecast.CurrentCell.Value.ToString();

                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        #region button

        private void btnCheck_Click(object sender, EventArgs e)
        {
            loadForecastList();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!MainDashboard.forecastReportInputFormOpen)
            {
                frmForecastReport frm = new frmForecastReport();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                MainDashboard.forecastReportInputFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmForecastReport>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmForecastReport>().First().BringToFront();
                }
            }
        }

        #endregion
    }
}
