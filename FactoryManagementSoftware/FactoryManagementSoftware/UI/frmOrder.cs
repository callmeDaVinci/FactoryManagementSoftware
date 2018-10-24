using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
        }

        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        #region Load or Reset Form

        private void frmOrder_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ordFormOpen = false;
        }

        private void loadOrderRecord()
        {
            DataTable dt = dalOrd.Select();
            dgvOrd.Rows.Clear();
            ((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;
            foreach (DataRow ord in dt.Rows)
            {
                int n = dgvOrd.Rows.Add();



                dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
                dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
                dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
                dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
                dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
                dgvOrd.Rows[n].Cells["ord_forecast_date"].Value = Convert.ToDateTime(ord["ord_forecast_date"]).ToString("dd/MM/yyyy"); ;
                dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
                dgvOrd.Rows[n].Cells["ord_added_by"].Value = ord["ord_added_by"].ToString();
                dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();

                //((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).DataSource = getOrderStatusTable();
                //((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).DisplayMember = "Status";




            }
        }

        private void resetForm()
        {
            //select item category list from item category database
            DataTable dtItemCat = dalItemCat.Select();
            //remove repeating name in item_cat_name
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            //sort the data according item_cat_name
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            //set combobox datasource from table
            cmbItemCat.DataSource = distinctTable;
            //show item_cat_name data from table only
            cmbItemCat.DisplayMember = "item_cat_name";

            loadOrderRecord();
        }

        private DataTable getOrderStatusTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status", typeof(string));
            dt.Rows.Add("Requesting");
            dt.Rows.Add("Cancelled");
            dt.Rows.Add("Approved");
            dt.Rows.Add("Received");


            return dt;
        }

        #endregion

        #region selected/text changed

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
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

        private void cmbItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = cmbItemCat.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                //show item based on keywords
                DataTable dt = dalItem.catSearch(keywords);
                //remove repeating name in item_name
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                //sort the data according item_name
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbItemName.DataSource = distinctTable;
                cmbItemName.DisplayMember = "item_name";
                cmbItemName.ValueMember = "item_name";

                if (string.IsNullOrEmpty(cmbItemName.Text))
                {
                    cmbItemCode.DataSource = null;
                }
            }
            else
            {
                cmbItemName.DataSource = null;
            }
        }

        private void cmbItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void cmbQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
        }


        #endregion

        #region data validation

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbItemName.Text))
            {

                errorProvider1.SetError(cmbItemName, "Transfer item name required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbItemCode.Text))
            {

                errorProvider2.SetError(cmbItemCode, "Transfer item code required");
                result = false;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {

                errorProvider3.SetError(txtQty, "Transfer quantity required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbQtyUnit.Text))
            {

                errorProvider4.SetError(cmbQtyUnit, "Quantity unit required");
                result = false;
            }

            return result;
        }

        private void getDataFromUser()
        {
            uOrd.ord_item_code = cmbItemCode.Text;
            uOrd.ord_qty = Convert.ToInt32(txtQty.Text);
            uOrd.ord_forecast_date = dtpForecastDate.Value.Date;
            uOrd.ord_note = txtNote.Text;
            uOrd.ord_added_date = DateTime.Now;
            uOrd.ord_added_by = 0;
        }
        #endregion

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Validation();
        }
    }
}
