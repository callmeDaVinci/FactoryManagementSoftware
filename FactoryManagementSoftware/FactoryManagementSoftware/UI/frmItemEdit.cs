using System;
using System.Data;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemEdit : Form
    {
        public frmItemEdit()
        {
            InitializeComponent();
        }

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatDAL dALItemCat = new itemCatDAL();

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dALItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";
            cmbCat.SelectedIndex = -1;
        }

        private void loadMaterialTypeData()
        {
            DataTable dtItemCat = dalItem.catSearch("RAW Material");
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_code");
            distinctTable.DefaultView.Sort = "item_code ASC";
            cmbMaterialType.DataSource = distinctTable;
            cmbMaterialType.DisplayMember = "item_code";
            cmbMaterialType.SelectedIndex = -1;
        }

        private void loadMasterBatchData()
        {
            DataTable dtItemCat = dalItem.catSearch("Master Batch");
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_code");
            distinctTable.DefaultView.Sort = "item_code ASC";
            cmbMasterBatch.DataSource = distinctTable;
            cmbMasterBatch.DisplayMember = "item_code";
            cmbMasterBatch.SelectedIndex = -1;
        }

        private void getData()
        {
            if(!string.IsNullOrEmpty(frmItem.currentItemCat))
            {
                cmbCat.Text = frmItem.currentItemCat;
                txtItemCode.Text = frmItem.currentItemCode;
                txtItemName.Text = frmItem.currentItemName;
                txtColor.Text = frmItem.currentItemColor;
                txtPartWeight.Text = frmItem.currentItemPartWeight;
                txtRunnerWeight.Text = frmItem.currentItemRunnerWeight;
            }
        }

        private void frmItemEdit_Load(object sender, EventArgs e)
        {
            loadItemCategoryData();
            loadMaterialTypeData();
            loadMasterBatchData();
            getData();
        }

        private bool Validation()
        {

            if (string.IsNullOrEmpty(cmbCat.Text))
            {

                errorProvider3.SetError(cmbCat, "Item Category Required");
            }

            if (string.IsNullOrEmpty(txtItemCode.Text))
            {

                errorProvider1.SetError(txtItemCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtItemName.Text))
            {

                errorProvider2.SetError(txtItemName, "Item Name Required");
            }

            bool result = false;
            if (!string.IsNullOrEmpty(txtItemCode.Text) && !string.IsNullOrEmpty(txtItemName.Text) && !string.IsNullOrEmpty(cmbCat.Text))
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

        private void btnSave_Click(object sender, EventArgs e)
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
                        uItem.item_cat = cmbCat.Text;
                        uItem.item_color = txtColor.Text;
                        uItem.item_material = cmbMaterialType.Text;
                        uItem.item_mb = cmbMasterBatch.Text;
                        if(!string.IsNullOrEmpty(txtMcTon.Text))
                        {
                            uItem.item_mc = Convert.ToInt32(txtMcTon.Text);
                        }
                        else
                        {
                            uItem.item_mc = 0;
                        }
                        
                        if (!string.IsNullOrEmpty(txtPartWeight.Text))
                        {
                            uItem.item_part_weight = Convert.ToSingle(txtPartWeight.Text);
                        }
                        else
                        {
                            uItem.item_part_weight = 0;
                        }

                        if (!string.IsNullOrEmpty(txtRunnerWeight.Text))
                        {
                            uItem.item_runner_weight = Convert.ToSingle(txtRunnerWeight.Text);
                        }
                        else
                        {
                            uItem.item_runner_weight = 0;
                        }

                        uItem.item_updtd_date = DateTime.Now;
                        uItem.item_updtd_by = 0;

                        //Updating data into database
                        bool success = dalItem.Update(uItem);

                        //if data is updated successfully then the value = true else false
                        if (success == true)
                        {
                            //data updated successfully
                            MessageBox.Show("Item successfully updated ");
                            this.Close();

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
                        uItem.item_cat = cmbCat.Text;
                        uItem.item_color = txtColor.Text;
                        uItem.item_material = cmbMaterialType.Text;
                        uItem.item_mb = cmbMasterBatch.Text;
                        if (!string.IsNullOrEmpty(txtMcTon.Text))
                        {
                            uItem.item_mc = Convert.ToInt32(txtMcTon.Text);
                        }
                        else
                        {
                            uItem.item_mc = 0;
                        }
                        if (!string.IsNullOrEmpty(txtPartWeight.Text))
                        {
                            uItem.item_part_weight = Convert.ToSingle(txtPartWeight.Text);
                        }
                        else
                        {
                            uItem.item_part_weight = 0;
                        }

                        if (!string.IsNullOrEmpty(txtRunnerWeight.Text))
                        {
                            uItem.item_runner_weight = Convert.ToSingle(txtRunnerWeight.Text);
                        }
                        else
                        {
                            uItem.item_runner_weight = 0;
                        }

                        uItem.item_added_date = DateTime.Now;
                        uItem.item_added_by = -1;

                        //Inserting Data into Database
                        bool success = dalItem.Insert(uItem);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Item successfully created");
                            this.Close();
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

        private void txtPartWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRunnerWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCat.Text))
            {
                errorProvider3.Clear();
            }
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtItemCode.Text))
            {
                errorProvider1.Clear();
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                errorProvider2.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMcTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
