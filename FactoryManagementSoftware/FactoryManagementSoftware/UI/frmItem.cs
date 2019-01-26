using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware
{
    public partial class frmItem : Form
    {
        #region create class object (database)

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        materialDAL dalMaterial = new materialDAL();
        materialBLL uMaterial = new materialBLL();

        itemCatDAL dALItemCat = new itemCatDAL();

        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();

        Tool tool = new Tool();

        #endregion

        #region variable declare

        private bool formLoaded = false;
        private bool join = false;
        private string itemParentCode = "parentTest";
        private string itemChildCode = "childTest";
        private string parentCust = null;
        private int currentRowIndex;
        static public string currentItemCode;
        static public string currentItemName;
        static public string currentItemColor;
        static public string currentItemCat;
        static public string currentItemPartWeight;
        static public string currentItemRunnerWeight;
        static public string currentMaterial;
        static public string currentMB;

       
        readonly string indexColName = "No.";

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;
        DataGridViewAutoSizeColumnMode DisplayedCellsExceptHeader = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        int minWidth = 60;
        int offSet = 20;

        #endregion

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
            formLoaded = true;
            dgvItemList.ClearSelection();
            loadFastAddData();
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

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dALItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");

            distinctTable.DefaultView.Sort = "item_cat_name ASC";    
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";
            cmbCat.SelectedIndex = -1;
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

            if (cmbCat.Text.Equals("Part"))
            {
                AddPartListColumns(dgvItemList);
                LoadPartList(dgvItemList);
            }
            else
            {
                AddMaterialListColumns(dgvItemList);
                LoadMaterialList(dgvItemList);
            }
        }

        private void loadFastAddData()
        {
            tool.loadRAWMaterialToComboBox(cmbMat);
            tool.loadMasterBatchToComboBox(cmbMB);
            tool.loadCustomerToComboBox(cmbCust);
        }
        #endregion

        #region Function: Insert/Delete/Reset/Search

        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRowIndex = e.RowIndex;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmItemEdit frm = new frmItemEdit(cmbCat.Text);
            currentItemCat = cmbCat.Text;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            resetForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            DataGridView dgv = dgvItemList;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (dgv.SelectedRows.Count > 0)
            {
                int n = dgv.CurrentCell.RowIndex;
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if(cmbCat.Text.Equals("Part"))
                    {
                        uItem.item_code = dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString(); 

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
                        uMaterial.material_code = dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString(); ;

                        bool success = dalMaterial.Delete(uMaterial);

                        if (success)
                        {
                            //item deleted successfully
                            MessageBox.Show("Material deleted successfully");
                            uItem.item_code = dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString();

                            //to-do 
                            //update item material to null for all the item using same material
                            //DataTable dtItem = dalItem.itemMaterialSearch(uItem.item_code);

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
            DataGridView dgv = dgvItemList;
            if (dgv.SelectedRows.Count > 0)
            {
                int n = dgv.CurrentCell.RowIndex;
                
                uItem.item_code = dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString();
                DataTable dt = dalItem.codeSearch(uItem.item_code);

                foreach (DataRow item in dt.Rows)
                {
                    uItem.item_cat = item[dalItem.ItemCat].ToString();

                    uItem.item_name = item[dalItem.ItemName].ToString();

                    uItem.item_material = item[dalItem.ItemMaterial].ToString();
                    uItem.item_mb = item[dalItem.ItemMBatch].ToString();
                    uItem.item_color = item[dalItem.ItemColor].ToString();

                    uItem.item_quo_ton = tool.Int_TryParse(item[dalItem.ItemQuoTon].ToString());
   
                    uItem.item_best_ton = tool.Int_TryParse(item[dalItem.ItemBestTon].ToString());
                    uItem.item_pro_ton = tool.Int_TryParse(item[dalItem.ItemProTon].ToString());

                    uItem.item_quo_ct = tool.Int_TryParse(item[dalItem.ItemQuoCT].ToString());
                    uItem.item_pro_ct_from = tool.Int_TryParse(item[dalItem.ItemProCTFrom].ToString());
                    uItem.item_pro_ct_to = tool.Int_TryParse(item[dalItem.ItemProCTTo].ToString());
                    uItem.item_capacity = tool.Int_TryParse(item[dalItem.ItemCapacity].ToString());

                    uItem.item_quo_pw_pcs = tool.Float_TryParse(item[dalItem.ItemQuoPWPcs].ToString());
                    uItem.item_quo_rw_pcs = tool.Float_TryParse(item[dalItem.ItemQuoRWPcs].ToString());
                    uItem.item_pro_pw_pcs = tool.Float_TryParse(item[dalItem.ItemProPWPcs].ToString());
                    uItem.item_pro_rw_pcs = tool.Float_TryParse(item[dalItem.ItemProRWPcs].ToString()); ;

                    uItem.item_pro_pw_shot = tool.Float_TryParse(item[dalItem.ItemProPWShot].ToString());
                    uItem.item_pro_rw_shot = tool.Float_TryParse(item[dalItem.ItemProRWShot].ToString());
                    uItem.item_pro_cooling = tool.Int_TryParse(item[dalItem.ItemProCooling].ToString());
                    uItem.item_wastage_allowed = tool.Float_TryParse(item[dalItem.ItemWastage].ToString());
                }

                frmItemEdit frm = new frmItemEdit(uItem);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit
                resetForm(); 
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
            DataGridView dgv = dgvItemList;

           
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

                dgv.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    float partf = 0;
                    float runnerf = 0;
                    int n = dgv.Rows.Add();
                    if (!string.IsNullOrEmpty(item["item_part_weight"].ToString()))
                    {
                        partf = Convert.ToSingle(item["item_part_weight"]);
                    }
                    if (!string.IsNullOrEmpty(item["item_runner_weight"].ToString()))
                    {
                        partf = Convert.ToSingle(item["item_runner_weight"]);
                    }

                    dgv.Rows[n].Cells["Category"].Value = item["item_cat"].ToString();
                    dgv.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                    dgv.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                    dgv.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                    dgv.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                    dgv.Rows[n].Cells["item_mb"].Value = item["item_mb"].ToString();
                    dgv.Rows[n].Cells["item_mc"].Value = item["item_mc"].ToString();
                    dgv.Rows[n].Cells["item_part_weight"].Value =partf.ToString("0.00");
                    dgv.Rows[n].Cells["item_runner_weight"].Value = runnerf.ToString("0.00");
                    dgv.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                    dgv.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
                }

            }
            else
            {
                //show all item from the database
                //loadItemData(dgvItemList);
            }

            bool rowColorChange = true;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
                    rowColorChange = false;
                }
                else
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }
            }
            dgv.ClearSelection();
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (formLoaded)
            {
                if(cmbCat.Text.Equals("Part"))
                {
                    AddPartListColumns(dgvItemList);
                    LoadPartList(dgvItemList);
                }
                else
                {
                    AddMaterialListColumns(dgvItemList);
                    LoadMaterialList(dgvItemList);
                }
            }
            dgvItemList.ClearSelection();
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
            }
            dgv.ClearSelection();
        }

        private void LoadPartList(DataGridView dgv)
        {
            int index = 1;
            DataTable dt = dalItem.catSearch(cmbCat.Text);
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();

            dgv.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                float partf = 0;
                float runnerf = 0;
                int n = dgv.Rows.Add();

                if (!string.IsNullOrEmpty(item[dalItem.ItemProPWPcs].ToString()))
                {
                    partf = Convert.ToSingle(item[dalItem.ItemProPWPcs]);
                }

                if (!string.IsNullOrEmpty(item[dalItem.ItemProRWPcs].ToString()))
                {
                    runnerf = Convert.ToSingle(item[dalItem.ItemProRWPcs]);
                }
                dgv.Rows[n].Cells[indexColName].Value = index;
                dgv.Rows[n].Cells[dalItem.ItemMaterial].Value = item[dalItem.ItemMaterial].ToString();
                dgv.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgv.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                dgv.Rows[n].Cells[dalItem.ItemColor].Value = item[dalItem.ItemColor].ToString();
                dgv.Rows[n].Cells[dalItem.ItemMBatch].Value = item[dalItem.ItemMBatch].ToString();
                dgv.Rows[n].Cells[dalItem.ItemQuoTon].Value = item[dalItem.ItemQuoTon].ToString();
                dgv.Rows[n].Cells[dalItem.ItemBestTon].Value = item[dalItem.ItemBestTon].ToString();
                dgv.Rows[n].Cells[dalItem.ItemProTon].Value = item[dalItem.ItemProTon].ToString();
                dgv.Rows[n].Cells[dalItem.ItemQuoCT].Value = item[dalItem.ItemQuoCT].ToString();
                dgv.Rows[n].Cells["ProCT"].Value = item[dalItem.ItemProCTFrom].ToString()+" - " + item[dalItem.ItemProCTTo].ToString();
                dgv.Rows[n].Cells["ItemWeight(g)"].Value = (partf+runnerf).ToString();
                dgv.Rows[n].Cells[dalItem.ItemQuoPWPcs].Value = item[dalItem.ItemQuoPWPcs].ToString();
                dgv.Rows[n].Cells[dalItem.ItemQuoRWPcs].Value = item[dalItem.ItemQuoRWPcs].ToString();
                dgv.Rows[n].Cells[dalItem.ItemProPWShot].Value = item[dalItem.ItemProPWShot].ToString();
                dgv.Rows[n].Cells[dalItem.ItemProRWShot].Value = item[dalItem.ItemProRWShot].ToString();
                dgv.Rows[n].Cells[dalItem.ItemCapacity].Value = item[dalItem.ItemCapacity].ToString();
                dgv.Rows[n].Cells[dalItem.ItemProCooling].Value = item[dalItem.ItemProCooling].ToString();
                dgv.Rows[n].Cells[dalItem.ItemProPWPcs].Value = partf.ToString("0.00");
                dgv.Rows[n].Cells[dalItem.ItemProRWPcs].Value = runnerf.ToString("0.00");
                index++;
            }
            tool.listPaint(dgv);

            if (dt.Rows.Count <= 0 && formLoaded)
            {
                MessageBox.Show("no data under this record");
            }
        }

        private void LoadMaterialList(DataGridView dgv)
        {
            DataTable dt = dalItem.catSearch(cmbCat.Text);
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();
            dgv.Rows.Clear();

            int index = 1;
            foreach (DataRow item in dt.Rows)
            {
                int n = dgv.Rows.Add();

                dgv.Rows[n].Cells[indexColName].Value = index;
                dgv.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgv.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                index++;
            }
            tool.listPaint(dgv);

            if (dt.Rows.Count <= 0 && formLoaded)
            {
                MessageBox.Show("no data under this record");
            }
        }

        private void AddPartListColumns(DataGridView dgv)
        {
            dgv.Columns.Clear();
            tool.AddTextBoxColumns(dgv, indexColName, indexColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Material Type", dalItem.ItemMaterial, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Part Name", dalItem.ItemName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Part Code", dalItem.ItemCode, Fill);
            tool.AddTextBoxColumns(dgv, "Color", dalItem.ItemColor, DisplayedCellsExceptHeader,minWidth);
            tool.AddTextBoxColumns(dgv, "Mb Code", dalItem.ItemMBatch, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Quo Ton", dalItem.ItemQuoTon, DisplayedCellsExceptHeader, minWidth);
            tool.AddTextBoxColumns(dgv, "Best Ton", dalItem.ItemBestTon, DisplayedCellsExceptHeader, minWidth);
            tool.AddTextBoxColumns(dgv, "Pro Ton", dalItem.ItemProTon, DisplayedCellsExceptHeader, minWidth);
            tool.AddTextBoxColumns(dgv, "Quo CT", dalItem.ItemQuoCT, DisplayedCellsExceptHeader, minWidth);
            tool.AddTextBoxColumns(dgv, "Pro CT", "ProCT", DisplayedCellsExceptHeader, minWidth);
            tool.AddTextBoxColumns(dgv, "Item Weight(g)", "ItemWeight(g)", DisplayedCellsExceptHeader, minWidth+offSet+5);
            tool.AddTextBoxColumns(dgv, "Quo PW(pcs)", dalItem.ItemQuoPWPcs, DisplayedCellsExceptHeader, minWidth + offSet);
            tool.AddTextBoxColumns(dgv, "Quo RW(pcs)", dalItem.ItemQuoRWPcs, DisplayedCellsExceptHeader, minWidth + offSet);
            tool.AddTextBoxColumns(dgv, "Pro PW(pcs)", dalItem.ItemProPWPcs, DisplayedCellsExceptHeader, minWidth + offSet);
            tool.AddTextBoxColumns(dgv, "Pro RW(pcs)", dalItem.ItemProRWPcs, DisplayedCellsExceptHeader, minWidth + offSet);
            tool.AddTextBoxColumns(dgv, "Pro PW(shot)", dalItem.ItemProPWShot, DisplayedCellsExceptHeader, minWidth + offSet);
            tool.AddTextBoxColumns(dgv, "Pro RW(shot)", dalItem.ItemProRWShot, DisplayedCellsExceptHeader, minWidth + offSet);
            tool.AddTextBoxColumns(dgv, "C", dalItem.ItemCapacity, DisplayedCellsExceptHeader, 30);
            tool.AddTextBoxColumns(dgv, "Pro CL", dalItem.ItemProCooling, DisplayedCellsExceptHeader, minWidth);
        }

        private void AddMaterialListColumns(DataGridView dgv)
        {
            dgv.Columns.Clear();
            tool.AddTextBoxColumns(dgv, indexColName, indexColName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Material Name", dalItem.ItemName, Fill);
            tool.AddTextBoxColumns(dgv, "Material Code", dalItem.ItemCode, Fill);
        }

        private void frmItem_Click(object sender, EventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private void dgvItemList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(sender, e);
        }

        private void updateItem()
        {
            //Update data
            itemBLL u = new itemBLL();
            u.item_cat = cmbCat.Text;

            if (!join)
            {
                itemParentCode = txtItemCode.Text;
                parentCust = cmbCust.Text;
            }
            else
            {
                itemChildCode = txtItemCode.Text;
            }

            u.item_code = txtItemCode.Text;
            u.item_name = txtItemName.Text;

            u.item_material = cmbMat.Text;
            u.item_mb = cmbMB.Text;
            u.item_color = txtColor.Text;

            u.item_quo_ton = 0;
            u.item_best_ton = 0;
            u.item_pro_ton = 0;

            u.item_quo_ct = 0;
            u.item_pro_ct_from = 0;
            u.item_pro_ct_to = 0;
            u.item_capacity = tool.Int_TryParse(txtCapacity.Text);

            u.item_quo_pw_pcs = tool.Float_TryParse(txtQuoPWpcs.Text);
            u.item_quo_rw_pcs = tool.Float_TryParse(txtQuoRWpcs.Text);
            u.item_pro_pw_pcs = tool.Float_TryParse(txtPWpcs.Text);
            u.item_pro_rw_pcs = tool.Float_TryParse(txtRWpcs.Text);

            u.item_pro_pw_shot = tool.Float_TryParse(txtPWshot.Text);
            u.item_pro_rw_shot = tool.Float_TryParse(txtRWshot.Text);
            u.item_pro_cooling = 0;
            u.item_wastage_allowed = 0.05f;

            if (cbAssembly.Checked)
            {
                u.item_assembly = 1;
            }
            else
            {
                u.item_assembly = 0;
            }

            u.item_updtd_date = DateTime.Now;
            u.item_updtd_by = MainDashboard.USER_ID;
            //Updating data into database
            //bool success = dalItem.Update(u);
            bool success = dalItem.NewUpdate(u);
            //if data is updated successfully then the value = true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Item successfully updated ");
                if (!join)
                {
                    pairCustomer();
                }
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
            itemBLL u = new itemBLL();
            u.item_cat = cmbCat.Text;

            if(!join)
            {
                itemParentCode = txtItemCode.Text;
                parentCust = cmbCust.Text;
            }
            else
            {
                itemChildCode = txtItemCode.Text;
            }
            
            u.item_code = txtItemCode.Text;
            u.item_name = txtItemName.Text;

            u.item_material = cmbMat.Text;
            u.item_mb = cmbMB.Text;
            u.item_color = txtColor.Text;

            u.item_quo_ton = 0;
            u.item_best_ton = 0;
            u.item_pro_ton = 0;

            u.item_quo_ct = 0;
            u.item_pro_ct_from = 0;
            u.item_pro_ct_to = 0;
            u.item_capacity = tool.Int_TryParse(txtCapacity.Text);

            u.item_quo_pw_pcs = tool.Float_TryParse(txtQuoPWpcs.Text); 
            u.item_quo_rw_pcs = tool.Float_TryParse(txtQuoRWpcs.Text);
            u.item_pro_pw_pcs = tool.Float_TryParse(txtPWpcs.Text);
            u.item_pro_rw_pcs = tool.Float_TryParse(txtRWpcs.Text);

            u.item_pro_pw_shot = tool.Float_TryParse(txtPWshot.Text);
            u.item_pro_rw_shot = tool.Float_TryParse(txtRWshot.Text);
            u.item_pro_cooling = 0;
            u.item_wastage_allowed = 0.05f;

            if (cbAssembly.Checked)
            {
                u.item_assembly = 1;
            }
            else
            {
                u.item_assembly = 0;
            }

            u.item_added_date = DateTime.Now;
            u.item_added_by = MainDashboard.USER_ID;

            //Inserting Data into Database
            bool success = dalItem.NewInsert(u);
            //If the data is successfully inserted then the value of success will be true else false
            if (success)
            {
                //Data Successfully Inserted
                //MessageBox.Show("Item successfully created");
                if(!join)
                {
                    pairCustomer();
                }
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
                //MessageBox.Show("Material successfully updated ");
                updateItem();
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
                //MessageBox.Show("Material successfully created");
                insertItem();
            }
            else
            {
                //Failed to insert data
                dalMaterial.Delete(uMaterial);
                MessageBox.Show("Failed to add new material");
            }
        }

        private void clearFastAddData()
        {
            txtItemCode.Clear();
            txtItemName.Clear();
            txtColor.Clear();
            txtQuoPWpcs.Clear();
            txtQuoRWpcs.Clear();
            txtPWpcs.Clear();
            txtRWpcs.Clear();
            txtPWshot.Clear();
            txtRWshot.Clear();
            txtCapacity.Clear();
            cmbMat.SelectedIndex = -1;
            cmbMB.SelectedIndex = -1;
        }

        private bool IfExists(string itemCode, string custName)
        {
            itemCustDAL dalItemCust = new itemCustDAL();
            DataTable dt = dalItemCust.existsSearch(itemCode, tool.getCustID(custName).ToString());

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void pairCustomer()
        {
            itemCustBLL uItemCust = new itemCustBLL();
            itemCustDAL dalItemCust = new itemCustDAL();

            string cust = cmbCust.Text;

            if(cmbCat.Text.Equals("Part") && !string.IsNullOrEmpty(cust))
            {
                if (!IfExists(txtItemCode.Text, cmbCust.Text))
                {
                    uItemCust.cust_id = Convert.ToInt32(tool.getCustID(cmbCust.Text));
                    uItemCust.item_code = txtItemCode.Text;
                    uItemCust.item_cust_added_date = DateTime.Now;
                    uItemCust.item_cust_added_by = MainDashboard.USER_ID;
                    uItemCust.forecast_one = 0;
                    uItemCust.forecast_two = 0;
                    uItemCust.forecast_three = 0;
                    uItemCust.forecast_current_month = DateTime.Now.ToString("MMMM");

                    bool success = dalItemCust.Insert(uItemCust);

                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new item_cust record");
                    }
                }
                else
                {
                    MessageBox.Show("Data already exist.");
                }
            }
            
        }

        private void joinItem()
        {
            if (IfChildProductsExists(itemParentCode, txtItemCode.Text))
            {
                MessageBox.Show("record already exist");
            }
            else
            {
                uJoin.join_parent_code = itemParentCode;

                uJoin.join_child_code = txtItemCode.Text;

                uJoin.join_added_date = DateTime.Now;
                uJoin.join_added_by = -1;

                if (!string.IsNullOrEmpty(txtJoinQty.Text))
                {
                    uJoin.join_qty = Convert.ToInt32(txtJoinQty.Text);
                }
                else
                {
                    uJoin.join_qty = 1;
                }

                bool success = dalJoin.Insert(uJoin);
                //If the data is successfully inserted then the value of success will be true else false
                if (success == true)
                {
                    //Data Successfully Inserted
                    MessageBox.Show("Join successfully created");
                }
                else
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to add new join");
                }
            }
        }

        private bool IfChildProductsExists(string parentCode, string childCode)
        {
            DataTable dt = dalJoin.existCheck(parentCode, childCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnFastAdd_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (IfProductsExists(txtItemCode.Text))
            {
                if (cmbCat.Text.Equals("Part"))
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

            if(join)
            {
                joinItem();
            }

            clearFastAddData();
            resetForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtPWpcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRWpcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtPWshot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRWshot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cbMB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMB.Checked)
            {
                cbPigment.Checked = false;
                tool.loadMasterBatchToComboBox(cmbMB);
            }
        }

        private void cbPigment_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPigment.Checked)
            {
                cbMB.Checked = false;
                tool.loadPigmentToComboBox(cmbMB);
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (cmbCat.Text.Equals("Part") || cmbCat.Text.Equals("Sub Material")) 
            {
                if (join)
                {
                    join = false;
                    btnJoin.Text = "ADD JOIN";
                    //lblParentCode.Hide();
                    lblJoinQty.Hide();
                    txtJoinQty.Hide();

                    lblParentCode.Text = "(PARENT CODE: " + itemParentCode + " )" + join;
                    lblParentCode.Show();
                }
                else
                {
                    if(string.IsNullOrEmpty(parentCust))
                    {
                        MessageBox.Show("Joining Item must pair with a customer. Please pair a customer for your previous added item("+itemParentCode+" "+tool.getItemName(itemParentCode) +").");
                    }
                    else
                    {
                        join = true;
                        lblJoinQty.Show();
                        txtJoinQty.Show();
                        btnJoin.Text = "JOINING";
                        lblParentCode.Text = "(PARENT CODE: " + itemParentCode + " )" + join;
                        lblParentCode.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Only part item or sub material can join together.");
            }
            
        }
    }

}
