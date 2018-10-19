using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware
{
    public partial class frmItem : Form
    {
        
        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        #region Load or Reset Form

        public frmItem()
        {
            InitializeComponent();
        }

        private void frmItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.itemFormOpen = false;
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void loadData()
        {
            DataTable dt = dalItem.Select();
            dgvItem.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvItem.Rows.Add();
                dgvItem.Rows[n].Cells["Category"].Value = item["item_cat"].ToString();
                dgvItem.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                dgvItem.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                dgvItem.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                dgvItem.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
            }
        }

        private void resetForm()
        {
            txtItemCode.ReadOnly = false;
            txtItemCode.Clear();
            txtItemName.Clear();
            cmbItemCategory.SelectedIndex = -1;
            txtItemCode.Focus();
            btnInsert.Text = "ADD";
            btnDelete.Hide();
            loadData();
        }

        #endregion

        #region Data Grid View
        private void dgvItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of particular row
            int rowIndex = e.RowIndex;
            txtItemCode.Text = dgvItem.Rows[rowIndex].Cells["dgvcItemCode"].Value.ToString();
            txtItemName.Text = dgvItem.Rows[rowIndex].Cells["dgvcItemName"].Value.ToString();

            btnInsert.Text = "UPDATE";
            btnDelete.Show();
            txtItemCode.ReadOnly = true;

        }

        #endregion

        #region data validation

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {

                errorProvider1.SetError(txtItemCode, "Item Code Required");
            }
            if (string.IsNullOrEmpty(txtItemName.Text))
            {

                errorProvider2.SetError(txtItemName, "Item Name Required");
            }

            bool result = false;
            if (!string.IsNullOrEmpty(txtItemCode.Text) && !string.IsNullOrEmpty(txtItemName.Text))
            {
                result = true;
            }

            return result;

        }

        private bool IfProductsExists(String productCode)
        {
            DataTable dt = dalItem.Search(productCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        #endregion

        #region Function: Insert/Delete/Reset/Search

        private void btnInsert_Click(object sender, EventArgs e)
        {

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (IfProductsExists(txtItemCode.Text))
                    {

                        //Update data
                        uItem.item_code = txtItemCode.Text;
                        uItem.item_name = txtItemName.Text;
                        uItem.item_updtd_date = DateTime.Now;
                        uItem.item_updtd_by = 0;

                        //Updating data into database
                        bool success = dalItem.Update(uItem);

                        //if data is updated successfully then the value = true else false
                        if (success == true)
                        {
                            //data updated successfully
                            MessageBox.Show("item successfully updated ");
                            resetForm();
                        }
                        else
                        {
                            //failed to update user
                            MessageBox.Show("Failed to updated item");
                        }
                    }
                    else
                    {
                        //Add data
                        uItem.item_code = txtItemCode.Text;
                        uItem.item_name = txtItemName.Text;
                        uItem.item_added_date = DateTime.Now;
                        uItem.item_added_by = -1;

                        //Inserting Data into Database
                        bool success = dalItem.Insert(uItem);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Item successfully created");
                            resetForm();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new item");
                        }
                    }
                } 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    uItem.item_code = txtItemCode.Text;

                    bool success = dalItem.Delete(uItem);

                    if (success == true)
                    {
                        //item deleted successfully
                        MessageBox.Show("Item deleted successfully");
                        resetForm();
                    }
                    else
                    {
                        //Failed to delete item
                        MessageBox.Show("Failed to delete item");
                    }
                }       
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to reset?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dialogResult == DialogResult.Yes)
            {
                resetForm();
            }
        }

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
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
                    dgvItem.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                    dgvItem.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                    dgvItem.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                    dgvItem.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
                }

            }
            else
            {
                //show all item from the database
                loadData();
            }
        }

        #endregion
    }
}
