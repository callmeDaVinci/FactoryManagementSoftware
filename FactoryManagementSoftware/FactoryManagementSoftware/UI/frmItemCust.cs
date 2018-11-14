using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemCust : Form
    {
        public frmItemCust()
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

        private void loadItemNameList()
        {
            //show item based on keywords
            DataTable dt = dalItem.Select();
            //remove repeating name in item_name
            DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
            //sort the data according item_name
            distinctTable.DefaultView.Sort = "item_name ASC";
            cmbItemName.DataSource = distinctTable;
            cmbItemName.DisplayMember = "item_name";
            cmbItemName.ValueMember = "item_name";

            cmbItemName.SelectedIndex = -1;
        }

        private void loadItemCodeList()
        {
            //get keyword from text box
            string keywords = cmbItemName.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                //show item code at itemcode combobox based on keywords
                DataTable dt = dalItem.Search(keywords);
                cmbItemCode.DataSource = dt;
                cmbItemCode.DisplayMember = "item_code";
                cmbItemCode.ValueMember = "item_code";
            }
            else
            {
                cmbItemCode.DataSource = null;
            }
        }

        private void loadItemCustomerList()
        {
            DataTable dt = dalItemCust.Select();
            dt.DefaultView.Sort = "cust_name DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();
            dgvItemCust.Rows.Clear();
    
            foreach (DataRow itemCust in sortedDt.Rows)
            {
                int n = dgvItemCust.Rows.Add();
                dgvItemCust.Rows[n].Cells["cust_name"].Value = itemCust["cust_name"].ToString();
                dgvItemCust.Rows[n].Cells["item_code"].Value = itemCust["item_code"].ToString();
                dgvItemCust.Rows[n].Cells["item_name"].Value = itemCust["item_name"].ToString();
                dgvItemCust.Rows[n].Cells["item_cust_added_date"].Value = itemCust["item_cust_added_date"].ToString();
                dgvItemCust.Rows[n].Cells["item_cust_added_by"].Value = itemCust["item_cust_added_by"].ToString();
            }
        }

        private void frmItemCust_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void frmItemCust_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.itemCustFormOpen = false;
        }

        private void refreshList()
        {

            loadCustomerList();
            loadItemNameList();
            loadItemCustomerList();
        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            loadItemCodeList();
        }

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void cmbItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
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

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbCust.Text))
            {

                errorProvider1.SetError(cmbCust, "Customer name required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbItemName.Text))
            {

                errorProvider2.SetError(cmbItemName, "Item name required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbItemCode.Text))
            {

                errorProvider3.SetError(cmbItemCode, "Item code required");
                result = false;
            }

            return result;
        }

        private bool IfExists(string itemCode, string custName)
        {

            DataTable dt = dalItemCust.existsSearch(itemCode, getCustID(custName));

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if(!IfExists(cmbItemCode.Text, cmbCust.Text))
                    {
                        uItemCust.cust_id = Convert.ToInt32(getCustID(cmbCust.Text));
                        uItemCust.item_code = cmbItemCode.Text;
                        uItemCust.item_cust_added_date = DateTime.Now;
                        uItemCust.item_cust_added_by = 0;
                        uItemCust.forecast_one = 0;
                        uItemCust.forecast_two = 0;
                        uItemCust.forecast_three = 0;
                        uItemCust.forecast_current_month = DateTime.Now.ToString("MMMM"); 
               

                        bool success = dalItemCust.Insert(uItemCust);
                       
                        if (success == true)
                        {
                            refreshList();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new item_cust record");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data already exist.");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
          
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (IfExists(cmbItemCode.Text, cmbCust.Text))
                {
                    uItemCust.cust_id = Convert.ToInt32(getCustID(cmbCust.Text));
                    uItemCust.item_code = cmbItemCode.Text;
                    uItemCust.item_cust_added_date = DateTime.Now;
                    uItemCust.item_cust_added_by = 0;

                    bool success = dalItemCust.Delete(uItemCust);

                    if (success == true)
                    {
                        cmbCust.SelectedIndex = -1;
                        cmbItemName.SelectedIndex = -1;
                        cmbItemCode.SelectedIndex = -1;
                        MessageBox.Show("Record delete successfully");
                        refreshList();
                    }
                    else
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to delete");
                    } 
                }
                else
                {
                    MessageBox.Show("Data not exist.");
                }

            }
        }

        private void dgvItemCust_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            cmbCust.Text = dgvItemCust.Rows[rowIndex].Cells["cust_name"].Value.ToString();
            cmbItemName.Text = dgvItemCust.Rows[rowIndex].Cells["item_name"].Value.ToString();
            cmbItemCode.Text = dgvItemCust.Rows[rowIndex].Cells["item_code"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = txtSearch.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                if(cmbSearch.Text.Equals("Customer"))
                {
                   
                    DataTable dt = dalItemCust.custSearch(keywords);

                    dgvItemCust.Rows.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgvItemCust.Rows.Add();
                        dgvItemCust.Rows[n].Cells["cust_name"].Value = item["cust_name"].ToString();
                        dgvItemCust.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                        dgvItemCust.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvItemCust.Rows[n].Cells["item_cust_added_date"].Value = item["item_cust_added_date"].ToString();
                        dgvItemCust.Rows[n].Cells["item_cust_added_by"].Value = item["item_cust_added_by"].ToString();
                    }
                }
                else if (cmbSearch.Text.Equals("Item Name"))
                {

                    DataTable dt = dalItemCust.itemSearch(keywords);

                    dgvItemCust.Rows.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgvItemCust.Rows.Add();
                        dgvItemCust.Rows[n].Cells["cust_name"].Value = item["cust_name"].ToString();
                        dgvItemCust.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                        dgvItemCust.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvItemCust.Rows[n].Cells["item_cust_added_date"].Value = item["item_cust_added_date"].ToString();
                        dgvItemCust.Rows[n].Cells["item_cust_added_by"].Value = item["item_cust_added_by"].ToString();
                    }
                }
                else if (cmbSearch.Text.Equals("Item Code"))
                {

                    DataTable dt = dalItemCust.itemCodeSearch(keywords);

                    dgvItemCust.Rows.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgvItemCust.Rows.Add();
                        dgvItemCust.Rows[n].Cells["cust_name"].Value = item["cust_name"].ToString();
                        dgvItemCust.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                        dgvItemCust.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvItemCust.Rows[n].Cells["item_cust_added_date"].Value = item["item_cust_added_date"].ToString();
                        dgvItemCust.Rows[n].Cells["item_cust_added_by"].Value = item["item_cust_added_by"].ToString();
                    }
                }
                else
                {
                    loadItemCustomerList();
                }
                

            }
            else
            {
                loadItemCustomerList();
            }
        }
    }
}
