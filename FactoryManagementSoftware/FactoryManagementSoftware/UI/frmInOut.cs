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

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        materialBLL uMaterial = new materialBLL();
        materialDAL dalMaterial = new materialDAL();

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

        private void loadStockList(string itemCode)
        {
            DataTable dt = dalStock.Select(itemCode);

            dgvFactoryStock.Rows.Clear();
            foreach (DataRow stock in dt.Rows)
            {
                int n = dgvFactoryStock.Rows.Add();
                dgvFactoryStock.Rows[n].Cells["fac_name"].Value = stock["fac_name"].ToString();
                dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");

            }
            dgvFactoryStock.ClearSelection();
        }

        private void loadItemList()
        {
            //get keyword from text box
            string keywords = txtItemSearch.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                //show item based on keywords
                DataTable dt = dalItem.Search(keywords);

                dgvItem.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvItem.Rows.Add();
                    dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                    dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                    dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                    dgvItem.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(item["item_qty"]).ToString("0.00");
                    dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
                }

            }
            else
            {
                //show all item from the database
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
            }
        }

        private void loadTransferList()
        {
            DataTable dt;
            //get keyword from text box
            string keywords = txtItemSearch.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                //show tranfer records based on keywords
                dt = daltrfHist.Search(keywords);
                dt.DefaultView.Sort = "trf_hist_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();
                //dgvItem.DataSource = dt;
                dgvTrf.Rows.Clear();
                foreach (DataRow trf in sortedDt.Rows)
                {
                    int n = dgvTrf.Rows.Add();
                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["trf_hist_item_name"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
                }

            }
            else
            {
                //show all transfer records from the database
                dt = daltrfHist.Select();
                dt.DefaultView.Sort = "trf_hist_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();
                dgvTrf.Rows.Clear();
                foreach (DataRow trf in sortedDt.Rows)
                {
                    int n = dgvTrf.Rows.Add();
                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["trf_hist_item_name"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
                }
            }


        }

        public void refreshList(string itemCode)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (!string.IsNullOrEmpty(itemCode))
            {
                loadStockList(itemCode);
                calTotalStock(itemCode);
            }
            
            loadItemList();
            loadTransferList();
            txtTrfQty.Clear();
            
            //Hightlight selected item code in item list
            String searchValue = cmbTrfItemCode.Text;

            if(!string.IsNullOrEmpty(searchValue))
            {
                dgvItem.ClearSelection();
                foreach (DataGridViewRow row in dgvItem.Rows)
                {
                    if (row.Cells["item_code"].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void resetForm()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            loadItemList();
            loadTransferList();

            //select item category list from item category database
            DataTable dtItemCat = dalItemCat.Select();
            //remove repeating name in item_cat_name
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            //sort the data according item_cat_name
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            //set combobox datasource from table
            cmbTrfItemCat.DataSource = distinctTable;
            //show item_cat_name data from table only
            cmbTrfItemCat.DisplayMember = "item_cat_name";

            //select category list from category database
            DataTable dtTrfCatFrm = daltrfCat.Select();
            //remove repeating name in trf_cat_name
            DataTable distinctTable3 = dtTrfCatFrm.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according trf_cat_name
            distinctTable3.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbTrfFromCategory.DataSource = distinctTable3;
            //show trf_cat_name data from table only
            cmbTrfFromCategory.DisplayMember = "trf_cat_name";


            //select category list  from category database
            DataTable dtTrfCatTo = daltrfCat.Select();
            //remove repeating name in trf_cat_name
            DataTable distinctTable4 = dtTrfCatTo.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according trf_cat_name
            distinctTable4.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbTrfToCategory.DataSource = distinctTable4;
            //show trf_cat_name data from table only
            cmbTrfToCategory.DisplayMember = "trf_cat_name";

            //set unit combo box empty
            cmbTrfQtyUnit.SelectedIndex = -1;

            //clear input and list
            txtTrfQty.Clear();
            txtTrfNote.Clear();
            dgvFactoryStock.Rows.Clear();
            dgvTotal.Rows.Clear();

            dgvItem.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void unitDataSource()
        {
            string itemCat = cmbTrfItemCat.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("item_unit");

            if (itemCat.Equals("RAW Material") || itemCat.Equals("Master Batch") || itemCat.Equals("Pigment"))
            {
                dt.Clear();
                dt.Rows.Add("kg");
                dt.Rows.Add("g");
                
            }
            else if(itemCat.Equals("Part") || itemCat.Equals("Carton"))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                cmbTrfQtyUnit.DataSource = dt;
            }
            else if(!string.IsNullOrEmpty(itemCat))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                dt.Rows.Add("kg");
                dt.Rows.Add("g");
                
            }
            else
            {
                dt = null;
            }

            cmbTrfQtyUnit.DataSource = dt;
            cmbTrfQtyUnit.DisplayMember = "item_unit";
            cmbTrfQtyUnit.SelectedIndex = -1;

        }

        #endregion
                
        #region item list double click

        private void dgvItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            int rowIndex = e.RowIndex;
            if(rowIndex != -1)
            {
                cmbTrfItemCat.Text = dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
                cmbTrfItemName.Text = dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
                cmbTrfItemCode.Text = dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();

                refreshList(cmbTrfItemCode.Text);
            }
           
        }

        #endregion

        #region text/index/slection Change

        //refresh list when selected item code change
        private void cmbTrfItemCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {
                refreshList(cmbTrfItemCode.Text);
            }
            else
            {
                dgvFactoryStock.Rows.Clear();
            }

        }

        //refresh list and item code source from combo box when selected item name change
        private void cmbTrfItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            //get keyword from text box
            string keywords = cmbTrfItemName.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {          
                //show item code at itemcode combobox based on keywords
                DataTable dt = dalItem.Search(keywords);
                cmbTrfItemCode.DataSource = dt;
                cmbTrfItemCode.DisplayMember = "item_code";
                cmbTrfItemCode.ValueMember = "item_code";
            }
            else
            {
                cmbTrfItemCode.DataSource = null;
            }

            // if selected item code change, then refresh list, else clear stock list
            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {
                refreshList(cmbTrfItemCode.Text);
            }
            else
            {
                dgvFactoryStock.Rows.Clear();
            }
        }

        //refresh list, item name and item code source from combo box when selected item name change
        private void cmbTrfItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = cmbTrfItemCat.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                //show item based on keywords
                DataTable dt = dalItem.catSearch(keywords);
                //remove repeating name in item_name
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                //sort the data according item_name
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbTrfItemName.DataSource = distinctTable;
                cmbTrfItemName.DisplayMember = "item_name";
                cmbTrfItemName.ValueMember = "item_name";

                unitDataSource();

                if(string.IsNullOrEmpty(cmbTrfItemName.Text))
                {
                    cmbTrfItemCode.DataSource = null;
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
                    cmbTrfFrom.DataSource = null;
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
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            loadItemList();
            loadTransferList();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion
       
        #region data validation

        private void txtTrfQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

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

        private float getQty(string itemCode, string factoryName)
        {
            float qty = 0;
            if(IfExists(itemCode, factoryName))
            {
                DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

                qty = Convert.ToSingle(dt.Rows[0]["stock_qty"].ToString());
                //MessageBox.Show("get qty= "+qty);
            }
            else
            {
                qty = 0;
            }

            return qty;
        }

        private string getFactoryID(string factoryName)
        {
            string factoryID = "";

            DataTable dtFac = dalFac.nameSearch(factoryName);

            foreach (DataRow fac in dtFac.Rows)
            {
                factoryID = fac["fac_id"].ToString();
            }
            return factoryID;
        }

        private bool IfExists(string itemCode, string factoryName)
        {

            DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

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
            if(ifGotChild())
            {
                cmbTrfQtyUnit.Text = "set";
            }
          
            
        }

        private void cmbTrfQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
        }

        private float checkQty(string keyword)
        {
            float qty = 0;

            if(string.IsNullOrEmpty(keyword))
            {
                qty = 0;
            }
            else
            {
               qty = Convert.ToSingle(keyword);
            }
            
            string unit = cmbTrfQtyUnit.Text;

            if(unit.Equals("g"))
            {
                qty = qty / 1000;
            }
            else if (string.IsNullOrEmpty(unit))
            {
                qty = 0;
            }

            return qty;
        }

        private string checkUnit(string keyword)
        {
            string unit = keyword;
            if (unit.Equals("g"))
            {
                unit = "kg";
            }
            return unit;
        }

        private bool ifGotChild()
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(cmbTrfItemCode.Text);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }
                return result;
        }

        private void checkChild(string itemCode,string qtyOut)
        {
            string factory = cmbTrfTo.Text;
            string childItemCode = "";
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0 && cmbTrfToCategory.Text == "Factory" && cmbTrfFromCategory.Text == "Assembly")
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    childItemCode = Join["join_child_code"].ToString();
                    uStock.stock_item_code = childItemCode;
                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(factory));
                    uStock.stock_qty = getQty(childItemCode, factory) - Convert.ToSingle(qtyOut);
                    uStock.stock_updtd_date = DateTime.Now;
                    uStock.stock_updtd_by = 0;
                    
                    //Stock Out
                    if (IfExists(childItemCode,factory))
                    {
                        bool success = dalStock.Update(uStock);
       
                        if (success)
                        {                              
                            float totalStock = 0;
                            totalStock = dalItem.getStockQty(childItemCode) - Convert.ToSingle(qtyOut);
                            //Update data
                            uItem.item_code = childItemCode;
                            uItem.item_qty = totalStock;
                            uItem.item_updtd_date = DateTime.Now;
                            uItem.item_updtd_by = 0;
                            cmbTrfQtyUnit.SelectedIndex = -1;

                            //Updating data into database
                            bool success2 = dalItem.qtyUpdate(uItem);

                            //if data is updated successfully then the value = true else false
                            if (!success2)
                            {
                                MessageBox.Show("Failed to updated child item stock qty");
                            }
                                                            
                        }
                        else
                        {
                            //failed to update user
                            MessageBox.Show("Failed to updated stock");
                        }
                    }
                    else
                    {      
                        //Inserting Data into Database
                        bool success = dalStock.Insert(uStock);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            //MessageBox.Show("Stock successfully created");
                            cmbTrfQtyUnit.SelectedIndex = -1;
                            float totalStock = 0;
                            totalStock = dalItem.getStockQty(childItemCode) - Convert.ToSingle(qtyOut);
                            //Update data
                            uItem.item_code = childItemCode;
                            uItem.item_qty = totalStock;
                            uItem.item_updtd_date = DateTime.Now;
                            uItem.item_updtd_by = 0;

                            //Updating data into database
                            bool success2 = dalItem.qtyUpdate(uItem);

                            //if data is updated successfully then the value = true else false
                            if (!success2)
                            {
                                MessageBox.Show("Failed to updated child item stock qty");
                            }
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new stock");
                        }
                    }
                  
                }
                
            }
            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {
                refreshList(cmbTrfItemCode.Text);
            }
        }
        #endregion

        #region Function: Insert/Reset

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string locationFrom = "";
            string locationTo = "";

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if(!string.IsNullOrEmpty(cmbTrfFrom.Text))
                    {
                        locationFrom = cmbTrfFrom.Text;
                    }
                    else
                    {
                        locationFrom = cmbTrfFromCategory.Text;
                    }

                    if (!string.IsNullOrEmpty(cmbTrfTo.Text))
                    {
                        locationTo = cmbTrfTo.Text;
                    }
                    else
                    {
                        locationTo = cmbTrfToCategory.Text;
                    }

                    utrfHist.trf_hist_item_code = cmbTrfItemCode.Text;
                    utrfHist.trf_hist_item_name = cmbTrfItemName.Text;
                    utrfHist.trf_hist_from = locationFrom;
                    utrfHist.trf_hist_to = locationTo;
                    utrfHist.trf_hist_qty = checkQty(txtTrfQty.Text);
                    utrfHist.trf_hist_unit = checkUnit(cmbTrfQtyUnit.Text);
                    utrfHist.trf_hist_trf_date = dtpTrfDate.Value.Date;
                    utrfHist.trf_hist_note = txtTrfNote.Text;
                    utrfHist.trf_hist_added_date = DateTime.Now;
                    utrfHist.trf_hist_added_by = 0;

                    //Inserting Data into Database
                    bool success = daltrfHist.Insert(utrfHist);
                    //If the data is successfully inserted then the value of success will be true else false
                    if (success == true)
                    {
                        //Data Successfully Inserted
                        //MessageBox.Show("Transfer record successfully created");
                        
                        stockInandOut();
                        cmbTrfQtyUnit.SelectedIndex = -1;

                        if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
                        {
                            refreshList(cmbTrfItemCode.Text);
                        }


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
            //Stock Out
            if (cmbTrfFromCategory.Text == "Factory")
            {
                if (IfExists(cmbTrfItemCode.Text, cmbTrfFrom.Text))
                {
                    //MessageBox.Show(cmbTrfItemCode.Text + " " + cmbTrfFrom.Text + " data exists");
                    //Update data
                    uStock.stock_item_code = cmbTrfItemCode.Text;
                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfFrom.Text));
                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfFrom.Text) - checkQty(txtTrfQty.Text);
                    uStock.stock_updtd_date = DateTime.Now;
                    uStock.stock_updtd_by = 0;

                    //MessageBox.Show("After calculate qty: " + uStock.stock_qty);

                    //Updating data into database
                    bool success = dalStock.Update(uStock);

                    //if data is updated successfully then the value = true else false
                    if (success == true)
                    {
                        //data updated successfully
                        //MessageBox.Show("Stock successfully updated ");
                        cmbTrfQtyUnit.SelectedIndex = -1;
                       
                    }
                    else
                    {
                        //failed to update user
                        MessageBox.Show("Failed to updated stock");
                    }

                }

                else
                {
                    //MessageBox.Show("data not exist");
                //Add data
                    uStock.stock_item_code = cmbTrfItemCode.Text;
                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfFrom.Text));
                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfFrom.Text) - checkQty(txtTrfQty.Text);
                    uStock.stock_updtd_date = DateTime.Now;
                    uStock.stock_updtd_by = 0;

                    //Inserting Data into Database
                    bool success = dalStock.Insert(uStock);
                    //If the data is successfully inserted then the value of success will be true else false
                    if (success == true)
                    {
                        //Data Successfully Inserted
                        //MessageBox.Show("Stock successfully created");
                        cmbTrfQtyUnit.SelectedIndex = -1;
                    }
                    else
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new stock");
                    }
                } 
            }

            //Stock In
            if (cmbTrfToCategory.Text == "Factory")
            {
                if (IfExists(cmbTrfItemCode.Text, cmbTrfTo.Text))
                {
                    string qtyOut = txtTrfQty.Text;
                    //MessageBox.Show(cmbTrfItemCode.Text + " " + cmbTrfTo.Text + " data exists");
                    //Update data
                    uStock.stock_item_code = cmbTrfItemCode.Text;
                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfTo.Text));
                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfTo.Text) + checkQty(qtyOut);
                    uStock.stock_updtd_date = DateTime.Now;
                    uStock.stock_updtd_by = 0;

                    //MessageBox.Show("After calculate qty: " + uStock.stock_qty);

                    //Updating data into database
                    bool success = dalStock.Update(uStock);

                    //if data is updated successfully then the value = true else false
                    if (success == true)
                    {
                        //data updated successfully
                        //MessageBox.Show("Stock successfully updated ");
                        checkChild(cmbTrfItemCode.Text, qtyOut);
                        if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
                        {
                            refreshList(cmbTrfItemCode.Text);
                        }
                        
                    }
                    else
                    {
                        //failed to update user
                        MessageBox.Show("Failed to updated stock");
                    }

                }

                else
                {
                    //MessageBox.Show("data not exist");
                    //Add data

                    uStock.stock_item_code = cmbTrfItemCode.Text;
                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfTo.Text));
                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfTo.Text) + checkQty(txtTrfQty.Text);
                    uStock.stock_updtd_date = DateTime.Now;
                    uStock.stock_updtd_by = 0;

                    //Inserting Data into Database
                    bool success = dalStock.Insert(uStock);
                    //If the data is successfully inserted then the value of success will be true else false
                    if (success == true)
                    {
                        //Data Successfully Inserted
                        //MessageBox.Show("Stock successfully created");
                        checkChild(cmbTrfItemCode.Text, txtTrfQty.Text);
                        if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
                        {
                            refreshList(cmbTrfItemCode.Text);
                        }
                        

                    }
                    else
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new stock");
                    }
                }
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
            dgvTotal.Rows[0].Cells["Total"].Value = totalStock.ToString("0.00");
            dgvTotal.ClearSelection();

            //Update data
            uItem.item_code = cmbTrfItemCode.Text;
            uItem.item_name = cmbTrfItemName.Text;
            uItem.item_qty = totalStock;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;

            //Updating data into database
            bool success = dalItem.qtyUpdate(uItem);

            //if data is updated successfully then the value = true else false
            if (success == true)
            {
                //data updated successfully
                //MessageBox.Show("Item successfully updateddfsfsd ");
               
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated item");
            }
        }

        #endregion

        private void dgvItem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex != -1)
            {
                cmbTrfItemCat.Text = dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
                cmbTrfItemName.Text = dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
                cmbTrfItemCode.Text = dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();

                refreshList(cmbTrfItemCode.Text);
            }
        }
    }
}

//code backup
//using FactoryManagementSoftware.BLL;
//using FactoryManagementSoftware.DAL;
//using System;
//using System.Data;
//using System.Windows.Forms;

//namespace FactoryManagementSoftware.UI
//{
//    public partial class frmInOut : Form
//    {
//        public frmInOut()
//        {
//            InitializeComponent();
//        }

//        #region create class object (database)

//        custBLL uCust = new custBLL();
//        custDAL dalCust = new custDAL();

//        facBLL uFac = new facBLL();
//        facDAL dalFac = new facDAL();

//        itemBLL uItem = new itemBLL();
//        itemDAL dalItem = new itemDAL();

//        itemCatBLL uItemCat = new itemCatBLL();
//        itemCatDAL dalItemCat = new itemCatDAL();

//        trfCatBLL utrfCat = new trfCatBLL();
//        trfCatDAL daltrfCat = new trfCatDAL();

//        trfHistBLL utrfHist = new trfHistBLL();
//        trfHistDAL daltrfHist = new trfHistDAL();

//        stockBLL uStock = new stockBLL();
//        stockDAL dalStock = new stockDAL();

//        joinBLL uJoin = new joinBLL();
//        joinDAL dalJoin = new joinDAL();

//        materialBLL uMaterial = new materialBLL();
//        materialDAL dalMaterial = new materialDAL();

//        #endregion

//        #region Load or Reset Form

//        private void frmInOut_FormClosed(object sender, FormClosedEventArgs e)
//        {
//            MainDashboard.inOutFormOpen = false;
//        }

//        private void frmInOut_Load(object sender, EventArgs e)
//        {
//            resetForm();
//        }

//        private void loadStockList(string itemCode)
//        {
//            DataTable dt = dalStock.Select(itemCode);

//            dgvFactoryStock.Rows.Clear();
//            foreach (DataRow stock in dt.Rows)
//            {
//                int n = dgvFactoryStock.Rows.Add();
//                dgvFactoryStock.Rows[n].Cells["fac_name"].Value = stock["fac_name"].ToString();
//                dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");

//            }
//            dgvFactoryStock.ClearSelection();
//        }

//        private void loadItemList()
//        {
//            //get keyword from text box
//            string keywords = txtItemSearch.Text;

//            //check if the keywords has value or not
//            if (!string.IsNullOrEmpty(keywords))
//            {
//                //show item based on keywords
//                DataTable dt = dalItem.Search(keywords);

//                dgvItem.Rows.Clear();
//                foreach (DataRow item in dt.Rows)
//                {
//                    int n = dgvItem.Rows.Add();
//                    dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
//                    dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
//                    dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
//                    dgvItem.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(item["item_qty"]).ToString("0.00");
//                    dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
//                }

//            }
//            else
//            {
//                //show all item from the database
//                DataTable dtItem = dalItem.Select();
//                dgvItem.Rows.Clear();
//                foreach (DataRow item in dtItem.Rows)
//                {
//                    int n = dgvItem.Rows.Add();
//                    dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
//                    dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
//                    dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
//                    dgvItem.Rows[n].Cells["item_qty"].Value = item["item_qty"].ToString();
//                    dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
//                }
//            }
//        }

//        private void loadTransferList()
//        {
//            DataTable dt;
//            //get keyword from text box
//            string keywords = txtItemSearch.Text;

//            //check if the keywords has value or not
//            if (!string.IsNullOrEmpty(keywords))
//            {
//                //show tranfer records based on keywords
//                dt = daltrfHist.Search(keywords);
//                dt.DefaultView.Sort = "trf_hist_added_date DESC";
//                DataTable sortedDt = dt.DefaultView.ToTable();
//                //dgvItem.DataSource = dt;
//                dgvTrf.Rows.Clear();
//                foreach (DataRow trf in sortedDt.Rows)
//                {
//                    int n = dgvTrf.Rows.Add();
//                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
//                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["trf_hist_item_name"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
//                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
//                }

//            }
//            else
//            {
//                //show all transfer records from the database
//                dt = daltrfHist.Select();
//                dt.DefaultView.Sort = "trf_hist_added_date DESC";
//                DataTable sortedDt = dt.DefaultView.ToTable();
//                dgvTrf.Rows.Clear();
//                foreach (DataRow trf in sortedDt.Rows)
//                {
//                    int n = dgvTrf.Rows.Add();
//                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
//                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["trf_hist_item_name"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
//                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
//                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
//                }
//            }


//        }

//        public void refreshList(string itemCode)
//        {
//            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
//            if (!string.IsNullOrEmpty(itemCode))
//            {
//                loadStockList(itemCode);
//                calTotalStock(itemCode);
//            }

//            loadItemList();
//            loadTransferList();
//            txtTrfQty.Clear();

//            //Hightlight selected item code in item list
//            String searchValue = cmbTrfItemCode.Text;

//            if (!string.IsNullOrEmpty(searchValue))
//            {
//                dgvItem.ClearSelection();
//                foreach (DataGridViewRow row in dgvItem.Rows)
//                {
//                    if (row.Cells["item_code"].Value.ToString().Equals(searchValue))
//                    {
//                        row.Selected = true;
//                        break;
//                    }
//                }
//            }
//            Cursor = Cursors.Arrow; // change cursor to normal type
//        }

//        private void resetForm()
//        {
//            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

//            loadItemList();
//            loadTransferList();

//            //select item category list from item category database
//            DataTable dtItemCat = dalItemCat.Select();
//            //remove repeating name in item_cat_name
//            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
//            //sort the data according item_cat_name
//            distinctTable.DefaultView.Sort = "item_cat_name ASC";
//            //set combobox datasource from table
//            cmbTrfItemCat.DataSource = distinctTable;
//            //show item_cat_name data from table only
//            cmbTrfItemCat.DisplayMember = "item_cat_name";

//            //select category list from category database
//            DataTable dtTrfCatFrm = daltrfCat.Select();
//            //remove repeating name in trf_cat_name
//            DataTable distinctTable3 = dtTrfCatFrm.DefaultView.ToTable(true, "trf_cat_name");
//            //sort the data according trf_cat_name
//            distinctTable3.DefaultView.Sort = "trf_cat_name ASC";
//            //set combobox datasource from table
//            cmbTrfFromCategory.DataSource = distinctTable3;
//            //show trf_cat_name data from table only
//            cmbTrfFromCategory.DisplayMember = "trf_cat_name";


//            //select category list  from category database
//            DataTable dtTrfCatTo = daltrfCat.Select();
//            //remove repeating name in trf_cat_name
//            DataTable distinctTable4 = dtTrfCatTo.DefaultView.ToTable(true, "trf_cat_name");
//            //sort the data according trf_cat_name
//            distinctTable4.DefaultView.Sort = "trf_cat_name ASC";
//            //set combobox datasource from table
//            cmbTrfToCategory.DataSource = distinctTable4;
//            //show trf_cat_name data from table only
//            cmbTrfToCategory.DisplayMember = "trf_cat_name";

//            //set unit combo box empty
//            cmbTrfQtyUnit.SelectedIndex = -1;

//            //clear input and list
//            txtTrfQty.Clear();
//            txtTrfNote.Clear();
//            dgvFactoryStock.Rows.Clear();
//            dgvTotal.Rows.Clear();

//            dgvItem.ClearSelection();
//            Cursor = Cursors.Arrow; // change cursor to normal type
//        }

//        private void unitDataSource()
//        {
//            string itemCat = cmbTrfItemCat.Text;
//            DataTable dt = new DataTable();
//            dt.Columns.Add("item_unit");

//            if (itemCat.Equals("RAW Material") || itemCat.Equals("Master Batch") || itemCat.Equals("Pigment"))
//            {
//                dt.Clear();
//                dt.Rows.Add("kg");
//                dt.Rows.Add("g");

//            }
//            else if (itemCat.Equals("Part") || itemCat.Equals("Carton"))
//            {
//                dt.Clear();
//                dt.Rows.Add("set");
//                dt.Rows.Add("piece");
//                cmbTrfQtyUnit.DataSource = dt;
//            }
//            else if (!string.IsNullOrEmpty(itemCat))
//            {
//                dt.Clear();
//                dt.Rows.Add("set");
//                dt.Rows.Add("piece");
//                dt.Rows.Add("kg");
//                dt.Rows.Add("g");

//            }
//            else
//            {
//                dt = null;
//            }

//            cmbTrfQtyUnit.DataSource = dt;
//            cmbTrfQtyUnit.DisplayMember = "item_unit";
//            cmbTrfQtyUnit.SelectedIndex = -1;

//        }

//        #endregion

//        #region item list double click

//        private void dgvItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
//        {

//            int rowIndex = e.RowIndex;
//            if (rowIndex != -1)
//            {
//                cmbTrfItemCat.Text = dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
//                cmbTrfItemName.Text = dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
//                cmbTrfItemCode.Text = dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();

//                refreshList(cmbTrfItemCode.Text);
//            }

//        }

//        #endregion

//        #region text/index/slection Change

//        //refresh list when selected item code change
//        private void cmbTrfItemCode_SelectionChangeCommitted(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
//            {
//                refreshList(cmbTrfItemCode.Text);
//            }
//            else
//            {
//                dgvFactoryStock.Rows.Clear();
//            }

//        }

//        //refresh list and item code source from combo box when selected item name change
//        private void cmbTrfItemName_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            errorProvider2.Clear();
//            //get keyword from text box
//            string keywords = cmbTrfItemName.Text;

//            //check if the keywords has value or not
//            if (!string.IsNullOrEmpty(keywords))
//            {
//                //show item code at itemcode combobox based on keywords
//                DataTable dt = dalItem.Search(keywords);
//                cmbTrfItemCode.DataSource = dt;
//                cmbTrfItemCode.DisplayMember = "item_code";
//                cmbTrfItemCode.ValueMember = "item_code";
//            }
//            else
//            {
//                cmbTrfItemCode.DataSource = null;
//            }

//            // if selected item code change, then refresh list, else clear stock list
//            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
//            {
//                refreshList(cmbTrfItemCode.Text);
//            }
//            else
//            {
//                dgvFactoryStock.Rows.Clear();
//            }
//        }

//        //refresh list, item name and item code source from combo box when selected item name change
//        private void cmbTrfItemCat_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            //get keyword from text box
//            string keywords = cmbTrfItemCat.Text;

//            //check if the keywords has value or not
//            if (!string.IsNullOrEmpty(keywords))
//            {
//                //show item based on keywords
//                DataTable dt = dalItem.catSearch(keywords);
//                //remove repeating name in item_name
//                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
//                //sort the data according item_name
//                distinctTable.DefaultView.Sort = "item_name ASC";
//                cmbTrfItemName.DataSource = distinctTable;
//                cmbTrfItemName.DisplayMember = "item_name";
//                cmbTrfItemName.ValueMember = "item_name";

//                unitDataSource();

//                if (string.IsNullOrEmpty(cmbTrfItemName.Text))
//                {
//                    cmbTrfItemCode.DataSource = null;
//                }
//            }
//            else
//            {
//                cmbTrfItemName.DataSource = null;
//            }
//        }

//        private void cmbTrfItemName_TextChanged(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(cmbTrfItemName.Text))
//            {
//                cmbTrfItemCode.DataSource = null;
//            }
//        }

//        private void cmbTrfFromCategory_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            switch (cmbTrfFromCategory.Text)
//            {
//                case "Factory":

//                    DataTable dt = dalFac.Select();
//                    DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
//                    distinctTable.DefaultView.Sort = "fac_name ASC";
//                    cmbTrfFrom.DataSource = distinctTable;
//                    cmbTrfFrom.DisplayMember = "fac_name";

//                    break;

//                case "Customer":

//                    DataTable dt2 = dalCust.Select();
//                    DataTable distinctTable2 = dt2.DefaultView.ToTable(true, "cust_name");
//                    distinctTable2.DefaultView.Sort = "cust_name ASC";
//                    cmbTrfFrom.DataSource = distinctTable2;
//                    cmbTrfFrom.DisplayMember = "cust_name";

//                    break;

//                case "Supplier":

//                    cmbTrfFrom.DataSource = null;

//                    break;

//                case "Other":

//                    cmbTrfFrom.DataSource = null;

//                    break;

//                default:
//                    cmbTrfFrom.DataSource = null;
//                    break;
//            }
//        }

//        private void cmbTrfToCategory_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            switch (cmbTrfToCategory.Text)
//            {
//                case "Factory":

//                    DataTable dt = dalFac.Select();
//                    DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
//                    distinctTable.DefaultView.Sort = "fac_name ASC";
//                    cmbTrfTo.DataSource = distinctTable;
//                    cmbTrfTo.DisplayMember = "fac_name";

//                    break;

//                case "Customer":

//                    DataTable dt2 = dalCust.Select();
//                    DataTable distinctTable2 = dt2.DefaultView.ToTable(true, "cust_name");
//                    distinctTable2.DefaultView.Sort = "cust_name ASC";
//                    cmbTrfTo.DataSource = distinctTable2;
//                    cmbTrfTo.DisplayMember = "cust_name";

//                    break;

//                case "Supplier":

//                    cmbTrfTo.DataSource = null;

//                    break;

//                case "Other":

//                    cmbTrfTo.DataSource = null;

//                    break;

//                default:
//                    break;
//            }
//        }

//        private void textBox1_TextChanged(object sender, EventArgs e)
//        {
//            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

//            loadItemList();
//            loadTransferList();

//            Cursor = Cursors.Arrow; // change cursor to normal type
//        }

//        #endregion

//        #region data validation

//        private void txtTrfQty_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
//            {
//                e.Handled = true;
//            }
//        }

//        private bool Validation()
//        {
//            bool result = true;
//            if (string.IsNullOrEmpty(txtTrfQty.Text))
//            {

//                errorProvider1.SetError(txtTrfQty, "Transfer quantity required");
//                result = false;
//            }

//            if (string.IsNullOrEmpty(cmbTrfItemName.Text))
//            {

//                errorProvider2.SetError(cmbTrfItemName, "Transfer item name required");
//                result = false;
//            }

//            if (string.IsNullOrEmpty(cmbTrfItemCode.Text))
//            {

//                errorProvider3.SetError(cmbTrfItemCode, "Transfer item code required");
//                result = false;
//            }

//            if (string.IsNullOrEmpty(cmbTrfQtyUnit.Text))
//            {

//                errorProvider4.SetError(cmbTrfQtyUnit, "Quantity unit required");
//                result = false;
//            }


//            return result;

//        }

//        private float getQty(string itemCode, string factoryName)
//        {
//            float qty = 0;
//            if (IfExists(itemCode, factoryName))
//            {
//                DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

//                qty = Convert.ToSingle(dt.Rows[0]["stock_qty"].ToString());
//                //MessageBox.Show("get qty= "+qty);
//            }
//            else
//            {
//                qty = 0;
//            }

//            return qty;
//        }

//        private string getFactoryID(string factoryName)
//        {
//            string factoryID = "";

//            DataTable dtFac = dalFac.nameSearch(factoryName);

//            foreach (DataRow fac in dtFac.Rows)
//            {
//                factoryID = fac["fac_id"].ToString();
//            }
//            return factoryID;
//        }

//        private bool IfExists(string itemCode, string factoryName)
//        {

//            DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

//            if (dt.Rows.Count > 0)
//                return true;
//            else
//                return false;
//        }

//        private void txtTrfQty_TextChanged(object sender, EventArgs e)
//        {
//            errorProvider1.Clear();
//        }

//        private void cmbTrfItemCode_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            errorProvider3.Clear();
//            if (ifGotChild())
//            {
//                cmbTrfQtyUnit.Text = "set";
//            }


//        }

//        private void cmbTrfQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            errorProvider4.Clear();
//        }

//        private float checkQty(string keyword)
//        {
//            float qty = 0;

//            if (string.IsNullOrEmpty(keyword))
//            {
//                qty = 0;
//            }
//            else
//            {
//                qty = Convert.ToSingle(keyword);
//            }

//            string unit = cmbTrfQtyUnit.Text;

//            if (unit.Equals("g"))
//            {
//                qty = qty / 1000;
//            }
//            else if (string.IsNullOrEmpty(unit))
//            {
//                qty = 0;
//            }

//            return qty;
//        }

//        private string checkUnit(string keyword)
//        {
//            string unit = keyword;
//            if (unit.Equals("g"))
//            {
//                unit = "kg";
//            }
//            return unit;
//        }

//        private bool ifGotChild()
//        {
//            bool result = false;
//            DataTable dtJoin = dalJoin.parentCheck(cmbTrfItemCode.Text);
//            if (dtJoin.Rows.Count > 0)
//            {
//                result = true;
//            }
//            return result;
//        }

//        private void checkChild(string itemCode, string qtyOut)
//        {
//            string factory = cmbTrfTo.Text;
//            string childItemCode = "";
//            DataTable dtJoin = dalJoin.parentCheck(itemCode);
//            if (dtJoin.Rows.Count > 0 && cmbTrfToCategory.Text == "Factory" && cmbTrfFromCategory.Text == "Assembly")
//            {
//                foreach (DataRow Join in dtJoin.Rows)
//                {
//                    childItemCode = Join["join_child_code"].ToString();
//                    uStock.stock_item_code = childItemCode;
//                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(factory));
//                    uStock.stock_qty = getQty(childItemCode, factory) - Convert.ToSingle(qtyOut);
//                    uStock.stock_updtd_date = DateTime.Now;
//                    uStock.stock_updtd_by = 0;

//                    //Stock Out
//                    if (IfExists(childItemCode, factory))
//                    {
//                        bool success = dalStock.Update(uStock);

//                        if (success)
//                        {
//                            float totalStock = 0;
//                            totalStock = dalItem.getStockQty(childItemCode) - Convert.ToSingle(qtyOut);
//                            //Update data
//                            uItem.item_code = childItemCode;
//                            uItem.item_qty = totalStock;
//                            uItem.item_updtd_date = DateTime.Now;
//                            uItem.item_updtd_by = 0;
//                            cmbTrfQtyUnit.SelectedIndex = -1;

//                            //Updating data into database
//                            bool success2 = dalItem.qtyUpdate(uItem);

//                            //if data is updated successfully then the value = true else false
//                            if (!success2)
//                            {
//                                MessageBox.Show("Failed to updated child item stock qty");
//                            }

//                        }
//                        else
//                        {
//                            //failed to update user
//                            MessageBox.Show("Failed to updated stock");
//                        }
//                    }
//                    else
//                    {
//                        //Inserting Data into Database
//                        bool success = dalStock.Insert(uStock);
//                        //If the data is successfully inserted then the value of success will be true else false
//                        if (success == true)
//                        {
//                            //Data Successfully Inserted
//                            //MessageBox.Show("Stock successfully created");
//                            cmbTrfQtyUnit.SelectedIndex = -1;
//                            float totalStock = 0;
//                            totalStock = dalItem.getStockQty(childItemCode) - Convert.ToSingle(qtyOut);
//                            //Update data
//                            uItem.item_code = childItemCode;
//                            uItem.item_qty = totalStock;
//                            uItem.item_updtd_date = DateTime.Now;
//                            uItem.item_updtd_by = 0;

//                            //Updating data into database
//                            bool success2 = dalItem.qtyUpdate(uItem);

//                            //if data is updated successfully then the value = true else false
//                            if (!success2)
//                            {
//                                MessageBox.Show("Failed to updated child item stock qty");
//                            }
//                        }
//                        else
//                        {
//                            //Failed to insert data
//                            MessageBox.Show("Failed to add new stock");
//                        }
//                    }

//                }

//            }
//            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
//            {
//                refreshList(cmbTrfItemCode.Text);
//            }
//        }
//        #endregion

//        #region Function: Insert/Reset

//        private void btnTransfer_Click(object sender, EventArgs e)
//        {
//            string locationFrom = "";
//            string locationTo = "";

//            if (Validation())
//            {
//                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                if (dialogResult == DialogResult.Yes)
//                {
//                    if (!string.IsNullOrEmpty(cmbTrfFrom.Text))
//                    {
//                        locationFrom = cmbTrfFrom.Text;
//                    }
//                    else
//                    {
//                        locationFrom = cmbTrfFromCategory.Text;
//                    }

//                    if (!string.IsNullOrEmpty(cmbTrfTo.Text))
//                    {
//                        locationTo = cmbTrfTo.Text;
//                    }
//                    else
//                    {
//                        locationTo = cmbTrfToCategory.Text;
//                    }

//                    utrfHist.trf_hist_item_code = cmbTrfItemCode.Text;
//                    utrfHist.trf_hist_item_name = cmbTrfItemName.Text;
//                    utrfHist.trf_hist_from = locationFrom;
//                    utrfHist.trf_hist_to = locationTo;
//                    utrfHist.trf_hist_qty = checkQty(txtTrfQty.Text);
//                    utrfHist.trf_hist_unit = checkUnit(cmbTrfQtyUnit.Text);
//                    utrfHist.trf_hist_trf_date = dtpTrfDate.Value.Date;
//                    utrfHist.trf_hist_note = txtTrfNote.Text;
//                    utrfHist.trf_hist_added_date = DateTime.Now;
//                    utrfHist.trf_hist_added_by = 0;

//                    //Inserting Data into Database
//                    bool success = daltrfHist.Insert(utrfHist);
//                    //If the data is successfully inserted then the value of success will be true else false
//                    if (success == true)
//                    {
//                        //Data Successfully Inserted
//                        //MessageBox.Show("Transfer record successfully created");

//                        stockInandOut();
//                        cmbTrfQtyUnit.SelectedIndex = -1;

//                        if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
//                        {
//                            refreshList(cmbTrfItemCode.Text);
//                        }


//                    }
//                    else
//                    {
//                        //Failed to insert data
//                        MessageBox.Show("Failed to add new transfer record");
//                    }

//                }
//            }
//        }

//        private void btnReset_Click(object sender, EventArgs e)
//        {
//            DialogResult dialogResult = MessageBox.Show("Are you sure want to reset?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (dialogResult == DialogResult.Yes)
//            {
//                resetForm();
//            }
//        }

//        #endregion

//        #region Calculate Stock  

//        private void stockInandOut()
//        {
//            //Stock Out
//            if (cmbTrfFromCategory.Text == "Factory")
//            {
//                if (IfExists(cmbTrfItemCode.Text, cmbTrfFrom.Text))
//                {
//                    //MessageBox.Show(cmbTrfItemCode.Text + " " + cmbTrfFrom.Text + " data exists");
//                    //Update data
//                    uStock.stock_item_code = cmbTrfItemCode.Text;
//                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfFrom.Text));
//                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfFrom.Text) - checkQty(txtTrfQty.Text);
//                    uStock.stock_updtd_date = DateTime.Now;
//                    uStock.stock_updtd_by = 0;

//                    //MessageBox.Show("After calculate qty: " + uStock.stock_qty);

//                    //Updating data into database
//                    bool success = dalStock.Update(uStock);

//                    //if data is updated successfully then the value = true else false
//                    if (success == true)
//                    {
//                        //data updated successfully
//                        //MessageBox.Show("Stock successfully updated ");
//                        cmbTrfQtyUnit.SelectedIndex = -1;

//                    }
//                    else
//                    {
//                        //failed to update user
//                        MessageBox.Show("Failed to updated stock");
//                    }

//                }

//                else
//                {
//                    //MessageBox.Show("data not exist");
//                    //Add data
//                    uStock.stock_item_code = cmbTrfItemCode.Text;
//                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfFrom.Text));
//                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfFrom.Text) - checkQty(txtTrfQty.Text);
//                    uStock.stock_updtd_date = DateTime.Now;
//                    uStock.stock_updtd_by = 0;

//                    //Inserting Data into Database
//                    bool success = dalStock.Insert(uStock);
//                    //If the data is successfully inserted then the value of success will be true else false
//                    if (success == true)
//                    {
//                        //Data Successfully Inserted
//                        //MessageBox.Show("Stock successfully created");
//                        cmbTrfQtyUnit.SelectedIndex = -1;
//                    }
//                    else
//                    {
//                        //Failed to insert data
//                        MessageBox.Show("Failed to add new stock");
//                    }
//                }
//            }

//            //Stock In
//            if (cmbTrfToCategory.Text == "Factory")
//            {
//                if (IfExists(cmbTrfItemCode.Text, cmbTrfTo.Text))
//                {
//                    string qtyOut = txtTrfQty.Text;
//                    //MessageBox.Show(cmbTrfItemCode.Text + " " + cmbTrfTo.Text + " data exists");
//                    //Update data
//                    uStock.stock_item_code = cmbTrfItemCode.Text;
//                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfTo.Text));
//                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfTo.Text) + checkQty(qtyOut);
//                    uStock.stock_updtd_date = DateTime.Now;
//                    uStock.stock_updtd_by = 0;

//                    //MessageBox.Show("After calculate qty: " + uStock.stock_qty);

//                    //Updating data into database
//                    bool success = dalStock.Update(uStock);

//                    //if data is updated successfully then the value = true else false
//                    if (success == true)
//                    {
//                        //data updated successfully
//                        //MessageBox.Show("Stock successfully updated ");
//                        checkChild(cmbTrfItemCode.Text, qtyOut);
//                        if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
//                        {
//                            refreshList(cmbTrfItemCode.Text);
//                        }

//                    }
//                    else
//                    {
//                        //failed to update user
//                        MessageBox.Show("Failed to updated stock");
//                    }

//                }

//                else
//                {
//                    //MessageBox.Show("data not exist");
//                    //Add data

//                    uStock.stock_item_code = cmbTrfItemCode.Text;
//                    uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTrfTo.Text));
//                    uStock.stock_qty = getQty(cmbTrfItemCode.Text, cmbTrfTo.Text) + checkQty(txtTrfQty.Text);
//                    uStock.stock_updtd_date = DateTime.Now;
//                    uStock.stock_updtd_by = 0;

//                    //Inserting Data into Database
//                    bool success = dalStock.Insert(uStock);
//                    //If the data is successfully inserted then the value of success will be true else false
//                    if (success == true)
//                    {
//                        //Data Successfully Inserted
//                        //MessageBox.Show("Stock successfully created");
//                        checkChild(cmbTrfItemCode.Text, txtTrfQty.Text);
//                        if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
//                        {
//                            refreshList(cmbTrfItemCode.Text);
//                        }


//                    }
//                    else
//                    {
//                        //Failed to insert data
//                        MessageBox.Show("Failed to add new stock");
//                    }
//                }
//            }




//        }

//        private void calTotalStock(string itemCode)
//        {
//            float totalStock = 0;

//            DataTable dtStock = dalStock.Select(itemCode);

//            foreach (DataRow stock in dtStock.Rows)
//            {
//                totalStock += Convert.ToSingle(stock["stock_qty"].ToString());
//            }

//            dgvTotal.Rows.Clear();
//            dgvTotal.Rows.Add();
//            dgvTotal.Rows[0].Cells["Total"].Value = totalStock.ToString("0.00");
//            dgvTotal.ClearSelection();

//            //Update data
//            uItem.item_code = cmbTrfItemCode.Text;
//            uItem.item_name = cmbTrfItemName.Text;
//            uItem.item_qty = totalStock;
//            uItem.item_updtd_date = DateTime.Now;
//            uItem.item_updtd_by = 0;

//            //Updating data into database
//            bool success = dalItem.qtyUpdate(uItem);

//            //if data is updated successfully then the value = true else false
//            if (success == true)
//            {
//                //data updated successfully
//                //MessageBox.Show("Item successfully updateddfsfsd ");

//            }
//            else
//            {
//                //failed to update user
//                MessageBox.Show("Failed to updated item");
//            }
//        }

//        #endregion

//        private void dgvItem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            int rowIndex = e.RowIndex;
//            if (rowIndex != -1)
//            {
//                cmbTrfItemCat.Text = dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
//                cmbTrfItemName.Text = dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
//                cmbTrfItemCode.Text = dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();

//                refreshList(cmbTrfItemCode.Text);
//            }
//        }
//    }
//} //code backup

