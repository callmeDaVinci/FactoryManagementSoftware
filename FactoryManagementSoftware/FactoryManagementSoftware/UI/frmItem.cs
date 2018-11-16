using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware
{
    public partial class frmItem : Form
    {
        
        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        materialDAL dalMaterial = new materialDAL();
        materialBLL uMaterial = new materialBLL();

        itemCatDAL dALItemCat = new itemCatDAL();

        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();

        private bool formLoaded = false;
        private int currentRowIndex;
        static public string currentItemCode;
        static public string currentItemName;
        static public string currentItemColor;
        static public string currentItemCat;
        static public string currentItemPartWeight;
        static public string currentItemRunnerWeight;
        static public string currentMaterial;
        static public string currentMB;

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
            loadItemCategoryData();
            resetForm();
            cmbCat.SelectedIndex = -1;
            formLoaded = true;
        }

        private void loadItemData()
        {  
            DataTable dt = dalItem.catSearch(cmbCat.Text);
            dgvItem.Rows.Clear();
            
                foreach (DataRow item in dt.Rows)
                {
                    float partf = 0;
                    float runnerf = 0;
                    int n = dgvItem.Rows.Add();

                    if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
                    {
                        partf = Convert.ToSingle(item["item_part_weight"]);
                    }

                    if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
                    {
                        runnerf = Convert.ToSingle(item["item_runner_weight"]);
                    }

                    dgvItem.Rows[n].Cells["Category"].Value = item["item_cat"].ToString();
                    dgvItem.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                    dgvItem.Rows[n].Cells["item_part_weight"].Value = partf.ToString("0.00");
                    dgvItem.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");
                    dgvItem.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                    dgvItem.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                    dgvItem.Rows[n].Cells["item_material"].Value = dalItem.getMaterialName(item["item_material"].ToString());
                    dgvItem.Rows[n].Cells["item_mb"].Value = dalItem.getMBName(item["item_mb"].ToString());
                    dgvItem.Rows[n].Cells["item_mc"].Value = item["item_mc"].ToString();
                    dgvItem.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                    dgvItem.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
                }
                listPaint(dgvItem);
          
            if(dt.Rows.Count <= 0 && formLoaded)
            {
                MessageBox.Show("no data under this record");
            }        
        }

        private bool IfProductsExists(String productCode)
        {
            DataTable dt;
           
                dt = dalItem.codeSearch(productCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void loadMaterialData()
        {
            string category = cmbCat.Text;
            DataTable dt = dalMaterial.catSearch(category);

            dgvItem.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvItem.Rows.Add();
                dgvItem.Rows[n].Cells["Category"].Value = item["material_cat"].ToString();
                dgvItem.Rows[n].Cells["dgvcItemCode"].Value = item["material_code"].ToString();
                dgvItem.Rows[n].Cells["dgvcItemName"].Value = item["material_name"].ToString();

                uItem.item_code = item["material_code"].ToString();
                uItem.item_name = item["material_name"].ToString();
                uItem.item_cat = item["material_cat"].ToString();

                uItem.item_color = "";
                uItem.item_material = "";
                uItem.item_mb = "";

                uItem.item_added_date = DateTime.Now;
                uItem.item_added_by = -1;

                if(!IfProductsExists(uItem.item_code))
                {
                    dalItem.Insert(uItem);
                   
                }
                
            }
            listPaint(dgvItem);
         
            if (dt.Rows.Count <= 0 && formLoaded)
            {
                MessageBox.Show("no data under this record");
            }
            
        }

        private void loadData()
        {
            loadItemData();
            //if (!string.IsNullOrEmpty(cmbCat.Text))
            //{
            //    if (cmbCat.Text.Equals("Part"))
            //    {
                    
            //    }
            //    else
            //    {
            //        loadMaterialData();
            //    }
            //}
        }

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dALItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");

            distinctTable.DefaultView.Sort = "item_cat_name ASC";    
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";

        }

        private void resetForm()
        {           
            currentRowIndex = -1;
            currentItemCode = null;
            currentItemName = null;
            currentItemCat = null;
            currentItemColor = null;
            currentItemPartWeight = null;
            currentItemRunnerWeight = null;
            currentMaterial = null;
            currentMB = null;
            loadData();
        }

        #endregion

        #region Function: Insert/Delete/Reset/Search

        private void dgvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRowIndex = e.RowIndex;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmItemEdit frm = new frmItemEdit();
            currentItemCat = cmbCat.Text;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            resetForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (dgvItem.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if(cmbCat.Text.Equals("Part"))
                    {
                        uItem.item_code = dgvItem.Rows[currentRowIndex].Cells["dgvcItemCode"].Value.ToString(); 

                        bool success = dalItem.Delete(uItem);

                        if (success == true)
                        {
                            //item deleted successfully
                            MessageBox.Show("Item deleted successfully");
                            uJoin.join_parent_code = uItem.item_code;
                            uJoin.join_child_code = uItem.item_code;
                            dalJoin.itemDelete(uJoin);
                            resetForm();
                        }
                        else
                        {
                            //Failed to delete item
                            MessageBox.Show("Failed to delete item");
                        }
                    }

                    else
                    {
                        uMaterial.material_code = dgvItem.Rows[currentRowIndex].Cells["dgvcItemCode"].Value.ToString(); ;

                        bool success = dalMaterial.Delete(uMaterial);

                        if (success)
                        {
                            //item deleted successfully
                            MessageBox.Show("Material deleted successfully");
                            uItem.item_code = dgvItem.Rows[currentRowIndex].Cells["dgvcItemCode"].Value.ToString();

                            bool success2 = dalItem.Delete(uItem);

                            if (success2 == true)
                            {
                                //item deleted successfully
                                MessageBox.Show("Item deleted successfully");
                            }
                            else
                            {
                                //Failed to delete item
                                MessageBox.Show("Failed to delete item");
                            }
                            resetForm();
                        }
                        else
                        {
                            //Failed to delete item
                            MessageBox.Show("Failed to delete material");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a data");
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRowIndex = e.RowIndex;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count > 0)
            {
                if (cmbCat.Text.Equals("Part"))
                {
                    currentItemCat = dgvItem.Rows[currentRowIndex].Cells["Category"].Value.ToString();
                    currentItemCode = dgvItem.Rows[currentRowIndex].Cells["dgvcItemCode"].Value.ToString();
                    currentItemName = dgvItem.Rows[currentRowIndex].Cells["dgvcItemName"].Value.ToString();
                    currentItemColor = dgvItem.Rows[currentRowIndex].Cells["item_color"].Value.ToString();
                    currentItemPartWeight = Convert.ToSingle(dgvItem.Rows[currentRowIndex].Cells["item_part_weight"].Value).ToString("0.00");
                    currentItemRunnerWeight = Convert.ToSingle(dgvItem.Rows[currentRowIndex].Cells["item_runner_weight"].Value).ToString("0.00");
                    currentMaterial = dgvItem.Rows[currentRowIndex].Cells["item_material"].Value.ToString();
                    currentMB = dgvItem.Rows[currentRowIndex].Cells["item_mb"].Value.ToString();

                    frmItemEdit frm = new frmItemEdit();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit

                    resetForm(); 
                }
                else
                {
                    currentItemCat = dgvItem.Rows[currentRowIndex].Cells["Category"].Value.ToString();
                    currentItemCode = dgvItem.Rows[currentRowIndex].Cells["dgvcItemCode"].Value.ToString();
                    currentItemName = dgvItem.Rows[currentRowIndex].Cells["dgvcItemName"].Value.ToString();
                   
                    frmItemEdit frm = new frmItemEdit();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit

                    resetForm();
                }
            }
            else
            {
                MessageBox.Show("Please select a data");
            }

        }

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt;
            string keywords = txtItemSearch.Text;
            string category = cmbCat.Text;
            

           
            if (keywords != null && category != null)
            {
                if (cmbCat.Text.Equals("ALL"))
                {
                    dt = dalItem.Search(keywords);
                }
                else
                {
                    dt = dalItem.catItemSearch(keywords, category);
                }

                dgvItem.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    float partf = 0;
                    float runnerf = 0;
                    int n = dgvItem.Rows.Add();
                    if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
                    {
                        partf = Convert.ToSingle(item["item_part_weight"]);
                    }
                    if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
                    {
                        partf = Convert.ToSingle(item["item_runner_weight"]);
                    }

                    dgvItem.Rows[n].Cells["Category"].Value = item["item_cat"].ToString();
                    dgvItem.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                    dgvItem.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                    dgvItem.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                    dgvItem.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                    dgvItem.Rows[n].Cells["item_mb"].Value = item["item_mb"].ToString();
                    dgvItem.Rows[n].Cells["item_mc"].Value = item["item_mc"].ToString();
                    dgvItem.Rows[n].Cells["item_part_weight"].Value =partf.ToString("0.00");
                    dgvItem.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");
                    dgvItem.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                    dgvItem.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
                }

            }
            else
            {
                //show all item from the database
                loadData();
            }

            bool rowColorChange = true;
            foreach (DataGridViewRow row in dgvItem.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgvItem.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
                    rowColorChange = false;
                }
                else
                {
                    dgvItem.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }
            }
            dgvItem.ClearSelection();
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (formLoaded)
            {
                loadData();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        #endregion

        private bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        private void listPaint(DataGridView dgv)
        {
            bool rowColorChange = true;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = SystemColors.Control;
                    rowColorChange = false;
                }
                else
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }

                string itemCode = "";
                if (dgv == dgvItem)
                {
                    itemCode = dgv.Rows[n].Cells["dgvcItemCode"].Value.ToString();
                    float qty = 0;

                    if (dgv.Rows[n].Cells["dgvcQty"] != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells["dgvcQty"].Value.ToString(), out (qty));
                    }

                    if (ifGotChild(itemCode))
                    {
                        dgv.Rows[n].Cells["dgvcItemCode"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells["dgvcItemName"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
                    }
                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells["dgvcQty"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                }
            }


            dgv.ClearSelection();
        }

        private void dgvItem_Sorted(object sender, EventArgs e)
        {
            listPaint(dgvItem);
        }
    }

}
