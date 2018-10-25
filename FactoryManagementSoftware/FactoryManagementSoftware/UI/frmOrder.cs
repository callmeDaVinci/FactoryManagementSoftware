using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrder : Form
    {
        private string presentValue = "";
        private int selectedOrderID;

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

        private void refreshOrderRecord(int orderID)
        {
            loadOrderRecord();
            
                dgvOrd.ClearSelection();
                foreach (DataGridViewRow row in dgvOrd.Rows)
                {
                    if (Convert.ToInt32(row.Cells["ord_id"].Value.ToString()).Equals(orderID))
                    {
                        row.Selected = true;
                        break;
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

            if(combobox != null)
            {
                combobox.SelectedIndexChanged -= new EventHandler(SelectedIndexChanged);
               
                combobox.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);

                e.CellStyle.BackColor = this.dgvOrd.DefaultCellStyle.BackColor;
            }
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string selectedItem = combo.SelectedItem.ToString();
            if (!presentValue.Equals(selectedItem))
            {

                //MessageBox.Show("select changed: "+selectedItem + " ID:" +selectedOrderID);
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //Update data
                    uOrd.ord_id = selectedOrderID;
                    uOrd.ord_status = selectedItem;


                    //Updating data into database
                    bool success = dalOrd.Update(uOrd);

                    //if data is updated successfully then the value = true else false
                    if (success == true)
                    {
                        //data updated successfully
                        // MessageBox.Show("Order successfully updated ");
                        refreshOrderRecord(selectedOrderID);
                    }
                    else
                    {
                        //failed to update user
                        MessageBox.Show("Failed to updated order record");
                    }
                }
                    

            }
            


        }

        //activate combobox on first click
        private void dgvOrd_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                int rowIndex = e.RowIndex;
                presentValue = dgvOrd.Rows[rowIndex].Cells["ord_status"].Value.ToString();
                selectedOrderID = Convert.ToInt32(dgvOrd.Rows[rowIndex].Cells["ord_id"].Value.ToString());
                
                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;
               
                //MessageBox.Show(presentValue);

            }
        }
    }
}
