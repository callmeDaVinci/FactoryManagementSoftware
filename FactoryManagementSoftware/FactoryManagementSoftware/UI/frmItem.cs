﻿using FactoryManagementSoftware.BLL;
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
        private int currentRowIndex;
        static public string currentItemCode;
        static public string currentItemName;
        static public string currentItemColor;
        static public string currentItemCat;
        static public string currentItemPartWeight;
        static public string currentItemRunnerWeight;
        static public string currentMaterial;
        static public string currentMB;

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
            AddPartListColumns(dgvItemList);
            dgvItemList.ClearSelection();
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
            //loadItemData(dgvItemList);
            LoadPartList(dgvItemList);
        }

        #endregion

        #region Function: Insert/Delete/Reset/Search

        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

                    uItem.item_material = dalItem.getMaterialName(item[dalItem.ItemMaterial].ToString());
                    uItem.item_mb = dalItem.getMBName(item[dalItem.ItemMBatch].ToString());
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
                //loadItemData();
                LoadPartList(dgvItemList);
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
            DataTable dt = dalItem.catSearch(cmbCat.Text);
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

                dgv.Rows[n].Cells[dalItem.ItemMaterial].Value = dalItem.getMaterialName(item[dalItem.ItemMaterial].ToString());
                dgv.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgv.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                dgv.Rows[n].Cells[dalItem.ItemColor].Value = item[dalItem.ItemColor].ToString();
                dgv.Rows[n].Cells[dalItem.ItemMBatch].Value = dalItem.getMBName(item[dalItem.ItemMBatch].ToString());
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
            tool.AddTextBoxColumns(dgv, "Material Type", dalItem.ItemMaterial, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Part Name", dalItem.ItemName, Fill);
            tool.AddTextBoxColumns(dgv, "Part Number", dalItem.ItemCode, Fill);
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
           
        }

        private void frmItem_Click(object sender, EventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private void dgvItemList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(sender, e);
        }
    }

}
