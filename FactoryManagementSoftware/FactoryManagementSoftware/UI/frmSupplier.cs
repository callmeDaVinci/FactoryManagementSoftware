using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSupplier : Form
    {
        public frmSupplier()
        {
            InitializeComponent();
        }

        custSupplierBLL uSupplier = new custSupplierBLL();
        custSupplierDAL dalSupplier = new custSupplierDAL();

        #region Load or Reset Form


        private void frmCust_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.supplierFormOpen = false;
        }

        private void frmCust_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void loadData()
        {
            DataTable dt = dalSupplier.SupplierSelectAll();

            dgvSupplier.DataSource = null;

            dgvSupplier.DataSource = dt;

            //foreach (DataRow item in dt.Rows)
            //{
            //    int n = dgvCust.Rows.Add();
            //    dgvCust.Rows[n].Cells["dgvcCustID"].Value = item["cust_id"].ToString();
            //    dgvCust.Rows[n].Cells["dgvcCustName"].Value = item["cust_name"].ToString();
            //}
        }

        
        private void ReloadData()
        {
            txtSupplierID.Clear();
            txtSupplierName.Clear();
            btnAdd.Text = "Add";
            btnDelete.Hide();
            loadData();
            this.ActiveControl = txtSupplierName;
        }

        #endregion

        #region Data Grid View

        private void dgvCust_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of particular row
            int rowIndex = e.RowIndex;

            if(rowIndex >= 0)
            {
                txtSupplierID.Text = dgvSupplier.Rows[rowIndex].Cells[dalSupplier.SupplierID].Value.ToString();
                txtSupplierName.Text = dgvSupplier.Rows[rowIndex].Cells[dalSupplier.SupplierName].Value.ToString();

                btnAdd.Text = "Update";
                btnDelete.Show();
            }
          

        }

        #endregion

        #region data validation

        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtSupplierName.Text))
            {

                errorProvider2.SetError(txtSupplierName, "Supplier name Required");
            }
            else
            {
                result = true;
            }

            return result;

        }

        private bool IfSupplierExists(String supplierID)
        {
            if (string.IsNullOrEmpty(txtSupplierID.Text))
            {
                return false;
            }
            else
            {
                DataTable dt = dalSupplier.supplierIDSearch(supplierID);
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }

        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
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
                    if (IfSupplierExists(txtSupplierID.Text))
                    {

                        //Update data
                        uSupplier.supplier_id = Convert.ToInt32(txtSupplierID.Text);
                        uSupplier.supplier_name = txtSupplierName.Text;
                        uSupplier.updated_date = DateTime.Now;
                        uSupplier.updated_by = MainDashboard.USER_ID;
                        uSupplier.isRemoved = false;

                        //Updating data into database
                        bool success = dalSupplier.SupplierUpdate(uSupplier);

                        //if data is updated successfully then the value = true else false
                        if (success)
                        {
                            //data updated successfully
                            MessageBox.Show("Supplier's data updated successfully.");
                            ReloadData();
                        }
                        else
                        {
                            //failed to update user
                            MessageBox.Show("Failed to update supplier.");
                        }
                    }
                    else
                    {
                        //Add data
                        uSupplier.supplier_name = txtSupplierName.Text;
                        uSupplier.added_date = DateTime.Now;
                        uSupplier.added_by = MainDashboard.USER_ID;

                        //Inserting Data into Database
                        bool success = dalSupplier.InsertSupplier(uSupplier);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Supplier created!");
                            ReloadData();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new supplier.");
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
                uSupplier.supplier_id = Convert.ToInt32(txtSupplierID.Text);
                uSupplier.supplier_name = txtSupplierName.Text;
                uSupplier.updated_date = DateTime.Now;
                uSupplier.updated_by = MainDashboard.USER_ID;
                uSupplier.isRemoved = true;

                bool success = dalSupplier.SupplierUpdate(uSupplier);

                if (success)
                {
                    //item deleted successfully
                    MessageBox.Show("supplier deleted successfully");
                    ReloadData();
                }
                else
                {
                    //Failed to delete item
                    MessageBox.Show("Failed to delete supplier");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to reload?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                ReloadData();
            }
        }

        private void txtCustSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = txtSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dalSupplier.supplierSearch(keywords);
                //dgvItem.DataSource = dt;
                dgvSupplier.DataSource = null;
                dgvSupplier.DataSource = dt;
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
