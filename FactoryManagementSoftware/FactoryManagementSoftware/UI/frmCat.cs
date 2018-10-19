using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmCat : Form
    {
        public frmCat()
        {
            InitializeComponent();
        }

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        #region Load or Reset Form

        private void frmCat_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.catFormOpen = false;
        }

        private void frmCat_Load(object sender, EventArgs e)
        {
            resetForm("");
        }

        private void loadData()
        {
            //load Item Category List to data grid view
            DataTable dtItemCat = dalItemCat.Select();
            dgvItemCat.Rows.Clear();
            foreach (DataRow item in dtItemCat.Rows)
            {
                int n = dgvItemCat.Rows.Add();
                dgvItemCat.Rows[n].Cells["item_cat_id"].Value = item["item_cat_id"].ToString();
                dgvItemCat.Rows[n].Cells["item_cat_name"].Value = item["item_cat_name"].ToString();
            }

            //load tranfer category list to data grid view
            DataTable dttrfCat = daltrfCat.Select();
            dgvTrfCat.Rows.Clear();
            foreach (DataRow item in dttrfCat.Rows)
            {
                int n = dgvTrfCat.Rows.Add();
                dgvTrfCat.Rows[n].Cells["trf_cat_id"].Value = item["trf_cat_id"].ToString();
                dgvTrfCat.Rows[n].Cells["trf_cat_name"].Value = item["trf_cat_name"].ToString();
            }

        }

        private void resetForm(string checkingSite)
        {
            switch (checkingSite)
            {
                case "Item":
                    //reset item category site
                    txtItemCatID.Clear();
                    txtItemCat.Clear();
                    txtItemCatSearch.Clear();
                    errorProvider1.Clear();
                    btnItemCatInsert.Text = "ADD";
                    btnItemCatDelete.Hide();
                    this.ActiveControl = txtItemCat;

                    break;

                case "Transfer":
                    //reset transfer category site
                    txtTrfCatID.Clear();
                    txtTrfCat.Clear();
                    txtTrfCatSearch.Clear();
                    errorProvider2.Clear();
                    btnTrfCatInsert.Text = "ADD";
                    btnTrfCatDelete.Hide();
                    this.ActiveControl = txtTrfCat;

                    break;

                case "":

                    //reset all
                    txtItemCatID.Clear();
                    txtItemCat.Clear();
                    btnItemCatInsert.Text = "ADD";
                    btnItemCatDelete.Hide();

                    txtTrfCatID.Clear();
                    txtTrfCat.Clear();
                    btnTrfCatInsert.Text = "ADD";
                    btnTrfCatDelete.Hide();

                    break;
                default:
                    MessageBox.Show("out of option");
                    break;

            }

            //load data to data grid view
            loadData();
            
        }




        #endregion

        #region Data Grid View

        private void dgvItemCat_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of particular row
            int rowIndex = e.RowIndex;
            txtItemCatID.Text = dgvItemCat.Rows[rowIndex].Cells["item_cat_id"].Value.ToString();
            txtItemCat.Text = dgvItemCat.Rows[rowIndex].Cells["item_cat_name"].Value.ToString();

            //change the button text and get delete button show
            btnItemCatInsert.Text = "UPDATE";
            btnItemCatDelete.Show();
        }

        private void dgvTrfCat_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of particular row
            int rowIndex = e.RowIndex;
            txtTrfCatID.Text = dgvTrfCat.Rows[rowIndex].Cells["trf_cat_id"].Value.ToString();
            txtTrfCat.Text = dgvTrfCat.Rows[rowIndex].Cells["trf_cat_name"].Value.ToString();

            //change the button text and get delete button show
            btnTrfCatInsert.Text = "UPDATE";
            btnTrfCatDelete.Show();
        }



        #endregion

        #region data validation

        private bool Validation(string checkingSite)
        {
            bool result = false;
            
            switch(checkingSite)
            {
                case "Item":

                    if (string.IsNullOrEmpty(txtItemCat.Text))
                    {

                        errorProvider1.SetError(txtItemCat, "Category name Required");
                    }
                    else
                    {
                        result = true;
                    }

                    break;

                case "Transfer":

                    if (string.IsNullOrEmpty(txtTrfCatID.Text))
                    {

                        errorProvider2.SetError(txtTrfCat, "Category name Required");
                    }
                    else
                    {
                        result = true;
                    }

                    break;
                default:
                    MessageBox.Show("out of option");
                    break;

            }
           

            return result;

        }

        private bool IfExists(String checkingSite)
        {
            bool result = false;

            switch(checkingSite)
            {
                case "Item":

                    if (string.IsNullOrEmpty(txtItemCatID.Text))
                    {
                        result = false;
                    }
                    else
                    {
                        DataTable dt = dalItemCat.idSearch(txtItemCatID.Text);
                        if (dt.Rows.Count > 0)
                            result = true;
                        else
                            result = false;
                    }
                    break;

                case "Transfer":
                    if (string.IsNullOrEmpty(txtTrfCatID.Text))
                    {
                        result = false;
                    }
                    else
                    {
                        DataTable dt = daltrfCat.idSearch(txtTrfCatID.Text);
                        if (dt.Rows.Count > 0)
                            result = true;
                        else
                            result = false;
                    }
                    break;

                default:
                    MessageBox.Show("out of option");
                    break;
                    
            }

            return result;

        }

        private void txtItemCat_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtTrfCat_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        #endregion

        #region Function: Insert/Delete/Reset/Search

        private void btnItemCatInsert_Click(object sender, EventArgs e)
        {

            if (Validation("Item"))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (IfExists("Item"))
                    {

                        //Update data
                        uItemCat.item_cat_id = Convert.ToInt32(txtItemCatID.Text);
                        uItemCat.item_cat_name = txtItemCat.Text;

                        //Updating data into database
                        bool success = dalItemCat.Update(uItemCat);

                        //if data is updated successfully then the value = true else false
                        if (success == true)
                        {
                            //data updated successfully
                            MessageBox.Show("Item category successfully updated ");
                            resetForm("Item");
                        }
                        else
                        {
                            //failed to update user
                            MessageBox.Show("Failed to updated item category");
                        }
                    }
                    else
                    {
                        //Add data
                        uItemCat.item_cat_name = txtItemCat.Text;

                        //Inserting Data into Database
                        bool success = dalItemCat.Insert(uItemCat);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Item category successfully created");
                            resetForm("Item");
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new item category");
                        }
                    }
                }
            }
        }

        private void btnItemCatDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                uItemCat.item_cat_id = Convert.ToInt32(txtItemCatID.Text);

                bool success = dalItemCat.Delete(uItemCat);

                if (success == true)
                {
                    //item deleted successfully
                    MessageBox.Show("Item category deleted successfully");
                    resetForm("Item");
                }
                else
                {
                    //Failed to delete item
                    MessageBox.Show("Failed to delete item category");
                }
            }
        }

        private void btnItemCatReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to reset?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                resetForm("Item");
            }
        }

        private void txtItemCatSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = txtItemCatSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dalItemCat.Search(keywords);
                //dgvItem.DataSource = dt;
                dgvItemCat.Rows.Clear();
                foreach (DataRow fac in dt.Rows)
                {
                    int n = dgvItemCat.Rows.Add();
                    dgvItemCat.Rows[n].Cells["item_cat_id"].Value = fac["item_cat_id"].ToString();
                    dgvItemCat.Rows[n].Cells["item_cat_name"].Value = fac["item_cat_name"].ToString();
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
