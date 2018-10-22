using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOut : Form
    { 
        public frmInOut()
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

        #endregion

        #region Load or Reset Form

        private void frmInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.inOutFormOpen = false;
        }

        private void frmInOut_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void loadData()
        {
            DataTable dtItem = dalItem.Select();
            dgvItem.Rows.Clear();
            foreach (DataRow item in dtItem.Rows)
            {
                int n = dgvItem.Rows.Add();
                dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvItem.Rows[n].Cells["item_qty"].Value = item["item_qty"].ToString();
                dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
            }

            //show user based on keywords
            DataTable dt2 = daltrfHist.Select();
            //dgvItem.DataSource = dt;
            dgvTrf.Rows.Clear();
            foreach (DataRow item in dt2.Rows)
            {
                int n = dgvTrf.Rows.Add();
                dgvTrf.Rows[n].Cells["trf_hist_id"].Value = item["trf_hist_id"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = item["trf_hist_added_date"].ToString();

                //dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = item["trf_hist_trf_date"].ToString();

                dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(item["trf_hist_trf_date"]).ToString("dd/MM/yyyy");


                dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = item["trf_hist_item_code"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = item["trf_hist_item_name"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_from"].Value = item["trf_hist_from"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_to"].Value = item["trf_hist_to"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = item["trf_hist_qty"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = item["trf_hist_unit"].ToString();
                dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = item["trf_hist_added_by"].ToString();

            }


           
            //dgvFactoryStock.DataSource = dtStock;
        }

        private void loadStockData(string itemCode)
        {
            DataTable dtStock = dalStock.Select(itemCode);

            dgvFactoryStock.Rows.Clear();
            foreach (DataRow stock in dtStock.Rows)
            {
                int n = dgvFactoryStock.Rows.Add();
                dgvFactoryStock.Rows[n].Cells["fac_name"].Value = stock["fac_name"].ToString();
                dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = stock["stock_qty"].ToString();

            }
            dgvFactoryStock.ClearSelection();
        }

        private void resetForm()
        {
            loadData();

            //select item data from item category database
            DataTable dtItemCat = dalItemCat.Select();
            //remove repeating name in item_name
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            //sort the data according item_name
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            //set combobox datasource from table
            cmbTrfItemCat.DataSource = distinctTable;
            //show item_name data from table only
            cmbTrfItemCat.DisplayMember = "item_cat_name";

            //select item data from item database
            DataTable dtTrfCatFrm = daltrfCat.Select();
            //remove repeating name in item_name
            DataTable distinctTable3 = dtTrfCatFrm.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according item_name
            distinctTable3.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbTrfFromCategory.DataSource = distinctTable3;
            //show item_name data from table only
            cmbTrfFromCategory.DisplayMember = "trf_cat_name";


            //select item data from item database
            DataTable dtTrfCatTo = daltrfCat.Select();
            //remove repeating name in item_name
            DataTable distinctTable4 = dtTrfCatTo.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according item_name
            distinctTable4.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbTrfToCategory.DataSource = distinctTable4;
            //show item_name data from table only
            cmbTrfToCategory.DisplayMember = "trf_cat_name";

            cmbTrfQtyUnit.SelectedIndex = -1;

            txtTrfQty.Clear();
            txtTrfNote.Clear();

            dgvFactoryStock.Rows.Clear();
            dgvTotal.Rows.Clear();


        }

        #endregion

        #region Data Grid View

        private void dgvItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            cmbTrfItemCat.Text = dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
            cmbTrfItemName.Text = dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
            cmbTrfItemCode.Text = dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();

            loadStockData(cmbTrfItemCode.Text);
            calTotalStock(cmbTrfItemCode.Text);
        }

        #endregion

        #region Combobox Datasource Change

        private void cmbTrfItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            //get keyword from text box
            string keywords = cmbTrfItemName.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                
                //show user based on keywords
                DataTable dt = dalItem.Search(keywords);
                cmbTrfItemCode.DataSource = dt;
                cmbTrfItemCode.DisplayMember = "item_code";
                cmbTrfItemCode.ValueMember = "item_code";
                

            }
            else
            {
                cmbTrfItemCode.DataSource = null;
            }
        }

        private void cmbTrfItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = cmbTrfItemCat.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dalItem.catSearch(keywords);
                //remove repeating name in item_name
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                //sort the data according item_name
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbTrfItemName.DataSource = distinctTable;
                cmbTrfItemName.DisplayMember = "item_name";
                cmbTrfItemName.ValueMember = "item_name";

                if(string.IsNullOrEmpty(cmbTrfItemName.Text))
                {
                    cmbTrfItemCode.DataSource = null;
                   // MessageBox.Show("No Item Found under this Category");
                }

            }
            else
            {
                cmbTrfItemName.DataSource = null;
            }
        }

        private void cmbTrfItemName_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmbTrfItemName.Text))
            {
                cmbTrfItemCode.DataSource = null;
            }
        }

        private void cmbTrfFromCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbTrfFromCategory.Text)
            {
                case "Factory":

                    DataTable dt = dalFac.Select();
                    DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
                    distinctTable.DefaultView.Sort = "fac_name ASC";
                    cmbTrfFrom.DataSource = distinctTable;
                    cmbTrfFrom.DisplayMember = "fac_name";

                    break;

                case "Customer":

                    DataTable dt2 = dalCust.Select();
                    DataTable distinctTable2 = dt2.DefaultView.ToTable(true, "cust_name");
                    distinctTable2.DefaultView.Sort = "cust_name ASC";
                    cmbTrfFrom.DataSource = distinctTable2;
                    cmbTrfFrom.DisplayMember = "cust_name";

                    break;

                case "Supplier":

                    cmbTrfFrom.DataSource = null;

                    break;

                case "Other":

                    cmbTrfFrom.DataSource = null;

                    break;

                default:
                    break;
            }
        }

        private void cmbTrfToCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTrfToCategory.Text)
            {
                case "Factory":

                    DataTable dt = dalFac.Select();
                    DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
                    distinctTable.DefaultView.Sort = "fac_name ASC";
                    cmbTrfTo.DataSource = distinctTable;
                    cmbTrfTo.DisplayMember = "fac_name";

                    break;

                case "Customer":

                    DataTable dt2 = dalCust.Select();
                    DataTable distinctTable2 = dt2.DefaultView.ToTable(true, "cust_name");
                    distinctTable2.DefaultView.Sort = "cust_name ASC";
                    cmbTrfTo.DataSource = distinctTable2;
                    cmbTrfTo.DisplayMember = "cust_name";

                    break;

                case "Supplier":

                    cmbTrfTo.DataSource = null;

                    break;

                case "Other":

                    cmbTrfTo.DataSource = null;

                    break;

                default:
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = txtItemSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dalItem.Search(keywords);
                //dgvItem.DataSource = dt;
                dgvItem.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvItem.Rows.Add();
                    dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                    dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvItem.Rows[n].Cells["item_qty"].Value = item["item_qty"].ToString();
                    dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
                }

                //show user based on keywords
                DataTable dt2 = daltrfHist.Search(keywords);
                //dgvItem.DataSource = dt;
                dgvTrf.Rows.Clear();
                foreach (DataRow item in dt2.Rows)
                {
                    int n = dgvTrf.Rows.Add();
                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = item["trf_hist_id"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = item["trf_hist_added_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = item["trf_hist_trf_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = item["trf_hist_item_code"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = item["trf_hist_item_name"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = item["trf_hist_from"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = item["trf_hist_to"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = item["trf_hist_qty"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = item["trf_hist_unit"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = item["trf_hist_added_by"].ToString();


                }

            }
            else
            {
                //show all item from the database
                loadData();
            }


        }

        #endregion

        #region data validation

        private bool Validation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(txtTrfQty.Text))
            {

                errorProvider1.SetError(txtTrfQty, "Transfer quantity required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbTrfItemName.Text))
            {

                errorProvider2.SetError(cmbTrfItemName, "Transfer item name required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {

                errorProvider3.SetError(cmbTrfItemCode, "Transfer item code required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbTrfQtyUnit.Text))
            {

                errorProvider4.SetError(cmbTrfQtyUnit, "Quantity unit required");
                result = false;
            }
            

            return result;

        }

        private bool IfExists(string itemCode, string factoryID)
        {
            DataTable dt = dalStock.Search(itemCode, factoryID);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void txtTrfQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void cmbTrfItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void cmbTrfQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
        }

        #endregion

        #region Function: Insert/Reset

        private void btnTransfer_Click(object sender, EventArgs e)
        {

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    //Add data
                    utrfHist.trf_hist_item_code = cmbTrfItemCode.Text;
                    utrfHist.trf_hist_item_name = cmbTrfItemName.Text;
                    utrfHist.trf_hist_from = cmbTrfFrom.Text;
                    utrfHist.trf_hist_to = cmbTrfTo.Text;
                    utrfHist.trf_hist_qty = Convert.ToSingle(txtTrfQty.Text);
                    utrfHist.trf_hist_unit = cmbTrfQtyUnit.Text;
                    utrfHist.trf_hist_trf_date = dtpTrfDate.Value.Date;
                    utrfHist.trf_hist_note = txtTrfNote.Text;
                    utrfHist.trf_hist_added_date = DateTime.Now;
                    utrfHist.trf_hist_added_by = 0;

                    string factoryID = "";

                    //Inserting Data into Database
                    bool success = daltrfHist.Insert(utrfHist);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Transfer record successfully created");

                        DataTable dt = dalFac.nameSearch(cmbTrfFrom.Text);
                        
                        foreach (DataRow fac in dt.Rows)
                        {
                            factoryID = fac["fac_id"].ToString();
                        }

                        if (IfExists(cmbTrfItemCode.Text, factoryID))
                        {
                            MessageBox.Show(cmbTrfItemCode.Text + " " + cmbTrfFrom.Text +" data exists");
                        }
                        else
                        {
                            MessageBox.Show("data not exist");
                        }
                            stockInandOut();
                            resetForm();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new transfer record");
                        }
                    
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to reset?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                resetForm();
            }
        }

        #endregion

        #region Calculate Stock  

        private void stockInandOut()
        {
            if (cmbTrfFromCategory.Text == "Factory")
            {
                MessageBox.Show("Stock Out from Factory");

            }

            if (cmbTrfToCategory.Text == "Factory")
            {
                MessageBox.Show("Stock In to Factory");
            }
        }

        private void calTotalStock(string itemCode)
        {
            float totalStock = 0;

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                totalStock += Convert.ToSingle(stock["stock_qty"].ToString());
            }

            dgvTotal.Rows.Clear();
            dgvTotal.Rows.Add();
            dgvTotal.Rows[0].Cells["Total"].Value = totalStock.ToString();
            dgvTotal.ClearSelection();
            //MessageBox.Show("Total Stock = " + totalStock);
        }



        #endregion

        private void txtTrfQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
