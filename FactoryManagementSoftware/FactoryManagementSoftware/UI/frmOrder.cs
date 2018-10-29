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

        #region variable declare
        private string presentValue = "";
        private int selectedOrderID = -1;
        static public string selectedOrderQty = "";
        static public string selectedItemCode = "";
        static public bool receivedStockIn = false;
        static public bool receivedStockOut = false;
        #endregion

        #region create class object (database)
        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        #endregion

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
            dt.DefaultView.Sort = "ord_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();
            dgvOrd.Rows.Clear();
            ((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;

            foreach (DataRow ord in sortedDt.Rows)
            {
                int n = dgvOrd.Rows.Add();
                dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
                dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
                dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
                dgvOrd.Rows[n].Cells["item_ord"].Value = ord["item_ord"].ToString();    
                dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
                dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
                dgvOrd.Rows[n].Cells["ord_forecast_date"].Value = Convert.ToDateTime(ord["ord_forecast_date"]).ToString("dd/MM/yyyy"); ;
                dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
                dgvOrd.Rows[n].Cells["ord_added_by"].Value = ord["ord_added_by"].ToString();
                dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();
            }
        }

        private void refreshOrderRecord(int orderID)
        {
            loadOrderRecord();

            dgvOrd.ClearSelection();
            if (selectedOrderID != -1)
            {
                foreach (DataGridViewRow row in dgvOrd.Rows)
                {
                    if (Convert.ToInt32(row.Cells["ord_id"].Value.ToString()).Equals(orderID))
                    {
                        row.Selected = true;
                        break;
                    }
                }
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
            txtQty.Clear();
            cmbQtyUnit.SelectedIndex = -1;
            cmbItemCat.SelectedIndex = -1;
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
            uOrd.ord_unit = cmbQtyUnit.Text;
            uOrd.ord_status = "Requesting";
            uOrd.ord_added_date = DateTime.Now;
            uOrd.ord_added_by = 0;
        }
        #endregion

        #region data Insert/Update/Search

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    getDataFromUser();

                    //Inserting Data into Database
                    bool success = dalOrd.Insert(uOrd);
                    //If the data is successfully inserted then the value of success will be true else false
                    if (success == true)
                    {
                        //Data Successfully Inserted
                        //MessageBox.Show("Transfer record successfully created");

                        //stockInandOut();
                        resetForm();

                    }
                    else
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new order record");
                    }

                }
            }
        }

        private void dgvOrd_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combobox = e.Control as ComboBox;

            if (combobox != null)
            {
                combobox.SelectedIndexChanged -= new EventHandler(SelectedIndexChanged);

                combobox.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);

                e.CellStyle.BackColor = this.dgvOrd.DefaultCellStyle.BackColor;
            }
        }

        private void cancelReceive(string selectedItem)
        {
            frmReceiveCancel frm = new frmReceiveCancel();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//stock out

            if (receivedStockOut)
            {
                if (!dalItem.updateTotalStock(selectedItemCode))
                {
                    MessageBox.Show("Failed to update total stock");
                }
                else
                {
                    if (selectedItem.Equals("Approved"))
                    {
                        orderAdd(selectedItemCode, selectedOrderQty);
                    }

                    dalOrd.Update(uOrd);
                }
                receivedStockOut = false;
            }

                

            
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string selectedItem = combo.SelectedItem.ToString();
            if (!presentValue.Equals(selectedItem))//if selected text change(before!=after)
            {
                uOrd.ord_id = selectedOrderID;
                uOrd.ord_status = selectedItem;

                if (selectedItem.Equals("Received"))
                {
                    frmReceiveConfirm frm = new frmReceiveConfirm();
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (presentValue.Equals("Requesting") || presentValue.Equals("Cancelled"))
                    {
                        MessageBox.Show("This order not approve yet!");
                        refreshOrderRecord(selectedOrderID);
                    }
                    else if (presentValue.Equals("Approved"))
                    {
                        frm.ShowDialog();//stock in

                        if(receivedStockIn)
                        {
                            if(!dalItem.updateTotalStock(selectedItemCode))
                            {
                                MessageBox.Show("Failed to update total stock");
                            }
                            else
                            {
                                dalOrd.Update(uOrd);
                                orderSubtract(selectedItemCode, selectedOrderQty);
                            }
                            receivedStockIn = false;
                            
                        }
                                        
                        refreshOrderRecord(selectedOrderID);
                    }
                    
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to check the order status?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        switch (selectedItem)
                        {
                            case "Cancelled":
                                if (presentValue.Equals("Approved"))
                                {
                                    orderSubtract(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    //dialog stock out
                                    cancelReceive(selectedItem);
                                }
                                break;

                            case "Approved":
                                if (presentValue.Equals("Requesting"))
                                {
                                    orderAdd(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Cancelled"))
                                {
                                    orderAdd(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Received"))
                                {
                      
                                    cancelReceive(selectedItem);
                                    refreshOrderRecord(selectedOrderID);

                                }
                                break;

                            case "Requesting":
                                if (presentValue.Equals("Approved"))
                                {
                                    orderSubtract(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    cancelReceive(selectedItem);
                                }
                                break;

                            default:
                                break;
                        }
                        refreshOrderRecord(selectedOrderID);
                    }
                }
            }

        }

        private void dgvOrd_CellEnter(object sender, DataGridViewCellEventArgs e)//activate combobox on first click
        {

            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                int rowIndex = e.RowIndex;
                presentValue = dgvOrd.Rows[rowIndex].Cells["ord_status"].Value.ToString();
                selectedOrderID = Convert.ToInt32(dgvOrd.Rows[rowIndex].Cells["ord_id"].Value.ToString());
                selectedItemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
                selectedOrderQty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();

                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;

                //MessageBox.Show(presentValue);

            }
        }

        private void txtOrdSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtOrdSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                DataTable dt = dalOrd.Search(keywords);
                dt.DefaultView.Sort = "ord_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();
                dgvOrd.Rows.Clear();
                ((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;

                foreach (DataRow ord in sortedDt.Rows)
                {
                    int n = dgvOrd.Rows.Add();
                    dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
                    dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
                    dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
                    dgvOrd.Rows[n].Cells["item_ord"].Value = ord["item_ord"].ToString();
                    dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
                    dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
                    dgvOrd.Rows[n].Cells["ord_forecast_date"].Value = Convert.ToDateTime(ord["ord_forecast_date"]).ToString("dd/MM/yyyy"); ;
                    dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
                    dgvOrd.Rows[n].Cells["ord_added_by"].Value = ord["ord_added_by"].ToString();
                    dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();
                }

            }
            else
            {
                //show all item from the database
                loadOrderRecord();
            }
        }

        private void orderAdd(string itemCode, string ordQty)
        {
            bool success = dalItem.orderAdd(itemCode, ordQty);//Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to updated item");//failed to update user
            }
        }

        private void orderSubtract(string itemCode, string ordQty)
        {
            bool success = dalItem.orderSubtract(itemCode, ordQty); //Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to updated item");//failed to update user
            }
        }

        private void stockAdd(string itemCode, string stockQty)
        {
            bool success = dalItem.stockAdd(itemCode, stockQty);//Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to add item stock qty");//failed to update user
            }
        }

        private void stockSubtract(string itemCode, string stockQty)
        {
            bool success = dalItem.stockSubtract(itemCode, stockQty); //Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to subtract item stock qty ");//failed to update user
            }
        }

        #endregion


    }
}
