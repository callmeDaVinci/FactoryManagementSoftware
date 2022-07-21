using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmFac : Form
    {
        public frmFac()
        {
            InitializeComponent();
        }

        facBLL uFac = new facBLL();
        facDAL dalFac = new facDAL();

        #region Load or Reset Form

        private void frmFac_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.facFormOpen = false;
        }

        private void frmFac_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void loadData()
        {
            DataTable dt = dalFac.SelectDESC();
            dgvFac.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvFac.Rows.Add();
                dgvFac.Rows[n].Cells["dgvcFacID"].Value = item["fac_id"].ToString();
                dgvFac.Rows[n].Cells["dgvcFacName"].Value = item["fac_name"].ToString();
            }
        }

        private void resetForm()
        {
            txtFacID.Clear();
            txtFacName.Clear();
            btnInsert.Text = "ADD";
            btnDelete.Hide();
            loadData();
            this.ActiveControl = txtFacName;
        }

        #endregion

        #region Data Grid View

        private void dgvFac_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of particular row
            int rowIndex = e.RowIndex;
            txtFacID.Text = dgvFac.Rows[rowIndex].Cells["dgvcFacID"].Value.ToString();
            txtFacName.Text = dgvFac.Rows[rowIndex].Cells["dgvcFacName"].Value.ToString();

            btnInsert.Text = "UPDATE";
            btnDelete.Show();

        }

        #endregion

        #region data validation

        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtFacName.Text))
            {

                errorProvider2.SetError(txtFacName, "Factory name Required");
            }        
            else
            {
                result = true;
            }

            return result;

        }

        private bool IfFacExists(String facID)
        {
            if(string.IsNullOrEmpty(txtFacID.Text))
            {
                return false;
            }
            else
            {
                DataTable dt = dalFac.idSearch(facID);
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            
        }

        private void txtFacName_TextChanged(object sender, EventArgs e)
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
                    if (IfFacExists(txtFacID.Text))
                    {

                        //Update data
                        uFac.fac_id = Convert.ToInt32(txtFacID.Text);
                        uFac.fac_name = txtFacName.Text;
                        uFac.fac_updtd_date = DateTime.Now;
                        uFac.fac_updtd_by = 0;

                        //Updating data into database
                        bool success = dalFac.Update(uFac);

                        //if data is updated successfully then the value = true else false
                        if (success == true)
                        {
                            //data updated successfully
                            MessageBox.Show("Factory successfully updated ");
                            resetForm();
                        }
                        else
                        {
                            //failed to update user
                            MessageBox.Show("Failed to updated factory");
                        }
                    }
                    else
                    {
                        //Add data
                        uFac.fac_name = txtFacName.Text;
                        uFac.fac_added_date = DateTime.Now;
                        uFac.fac_added_by = 1;

                        //Inserting Data into Database
                        bool success = dalFac.Insert(uFac);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Factory successfully created");
                            resetForm();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new factory");
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
                uFac.fac_id = Convert.ToInt32(txtFacID.Text);

                bool success = dalFac.Delete(uFac);

                if (success == true)
                {
                    //item deleted successfully
                    MessageBox.Show("Factory deleted successfully");
                    resetForm();
                }
                else
                {
                    //Failed to delete item
                    MessageBox.Show("Failed to delete factory");
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

        private void txtFacSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = txtFacSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dalFac.Search(keywords);
                //dgvItem.DataSource = dt;
                dgvFac.Rows.Clear();
                foreach (DataRow fac in dt.Rows)
                {
                    int n = dgvFac.Rows.Add();
                    dgvFac.Rows[n].Cells["dgvcFacID"].Value = fac["fac_id"].ToString();
                    dgvFac.Rows[n].Cells["dgvcFacName"].Value = fac["fac_name"].ToString();
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
