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

        itemCatDAL dalItemCat = new itemCatDAL();

        materialBLL uMaterial = new materialBLL();
        materialDAL dalMaterial = new materialDAL();

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";
            cmbCat.SelectedIndex = -1;
        }

        private void loadMaterialTypeData()
        {
            DataTable dt = dalMaterial.catSearch("RAW Material");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            cmbMaterialType.DataSource = distinctTable;
            cmbMaterialType.DisplayMember = "material_code";
            cmbMaterialType.SelectedIndex = -1;
        }

        private void loadMasterBatchData()
        {
            DataTable dt = dalMaterial.catSearch("Master Batch");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            cmbMasterBatch.DataSource = distinctTable;
            cmbMasterBatch.DisplayMember = "material_code";
            cmbMasterBatch.SelectedIndex = -1;
        }

        private void getData()
        {
            if(!string.IsNullOrEmpty(frmItem.currentItemCode))
            {
                cmbCat.Text = frmItem.currentItemCat;
                txtItemCode.Text = frmItem.currentItemCode;
                txtItemCode.Enabled = false;
                txtItemName.Text = frmItem.currentItemName;
                txtColor.Text = frmItem.currentItemColor;
                txtPartWeight.Text = frmItem.currentItemPartWeight;
                txtRunnerWeight.Text = frmItem.currentItemRunnerWeight;
                cmbMaterialType.Text = frmItem.currentMaterial;
                cmbMasterBatch.Text = frmItem.currentMB;

            }
            else
            {
                cmbCat.Text = frmItem.currentItemCat;
                txtItemCode.Enabled = true;
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
            DataTable dt;
            if(cmbCat.Text.Equals("Part"))
            {
                dt = dalItem.Search(productCode);
            }
            else
            {
                dt = dalMaterial.codeSearch(productCode);
            }
            

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void updateItem()
        {
            //Update data
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

        private void insertItem()
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

        private void updateMaterial()
        {
            //Update data
            uMaterial.material_cat = cmbCat.Text;
            uMaterial.material_code = txtItemCode.Text;
            uMaterial.material_name = txtItemName.Text;

            bool success = dalMaterial.Update(uMaterial);
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Material successfully updated ");
                this.Close();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated material");
            }
        }

        private void insertMaterial()
        {
            //Add data
            uMaterial.material_cat = cmbCat.Text;
            uMaterial.material_code = txtItemCode.Text;
            uMaterial.material_name = txtItemName.Text;
            bool success = dalMaterial.Insert(uMaterial);
            //If the data is successfully inserted then the value of success will be true else false
            if (success == true)
            {
                //Data Successfully Inserted
                MessageBox.Show("Material successfully created");
                this.Close();
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new material");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (IfProductsExists(txtItemCode.Text))
                    {
                        if(cmbCat.Text.Equals("Part"))
                        {
                            updateItem();
                        }
                        else
                        {
                            updateMaterial();
                        }   
                    }
                    else
                    {
                        if (cmbCat.Text.Equals("Part"))
                        {
                            insertItem();
                        }
                        else
                        {
                            insertMaterial();
                        }
                    }
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
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

            if(cmbCat.Text.Equals("RAW Material") || cmbCat.Text.Equals("Master Batch"))
            {
                txtItemName.Text = txtItemCode.Text;
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
