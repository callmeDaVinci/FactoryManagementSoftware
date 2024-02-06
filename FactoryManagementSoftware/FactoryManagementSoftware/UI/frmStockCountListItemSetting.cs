using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Linq;
using System.Threading;
using Syncfusion.XlsIO.Parser.Biff_Records;
using Guna.UI.WinForms;
using Accord.Statistics.Distributions.Univariate;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockCountListItemSetting : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        facDAL dalFac = new facDAL();
        stockCountListDAL dalStockCountList = new stockCountListDAL();
        stockCountListBLL uStockCountList = new stockCountListBLL();

        stockCountListItemDAL dalStockCountListItem = new stockCountListItemDAL();
        stockCountListItemBLL uStockCountListItem = new stockCountListItemBLL();

        itemDAL dalItem = new itemDAL();
        DataTable DT_STOCK_COUNT_LIST;
        DataTable DT_STOCK_COUNT_LIST_ITEM;
        DataTable DT_ACTIVE_FACTORY_LIST;
        private DataTable DT_ITEM_SOURCE;

        private string BUTTON_INSERT_TEXT = "Add Item";
        private string BUTTON_UPDATE_TEXT = "Update Item";
        private bool DATA_SAVED = false;
        private string ITEM_SEARCH_DEFAULT_TEXT = "Search (Item Name/Code)";
        private int LIST_TBL_CODE = -1;
        private int ITEM_TBL_CODE = -1;


        public frmStockCountListItemSetting()
        {
            InitializeComponent();
            InitialSetting();
        }

        public frmStockCountListItemSetting(int tableCode)
        {
            InitializeComponent();

            LIST_TBL_CODE = tableCode;

            InitialSetting();
        }

        public frmStockCountListItemSetting(int listTblCode, int itemTblCode)
        {
            InitializeComponent();

            LIST_TBL_CODE = listTblCode;
            ITEM_TBL_CODE = itemTblCode;

            InitialSetting();

        }
        private void InitialSetting()
        {

            InitialNameTextBox();
            LoadStockCountListData();
            LoadActiveFactoryListData();
            LoadStockCountListItemData();

            InitialLocationComboBox(DT_ACTIVE_FACTORY_LIST);
            InitialLocationFromAndInComboBox();
            InsertButtonTextChange();

            if (ITEM_TBL_CODE > 0)
            {
                LoadExistingData();
            }
        }

        private string header_MasterCode = "MasterCode";

        private void InitialNameTextBox()
        {
            DT_ITEM_SOURCE = dalItem.Select();

            DT_ITEM_SOURCE.Columns.Add(header_MasterCode, typeof(string));

            var masterCodes = new List<string>();

            foreach (DataRow row in DT_ITEM_SOURCE.Rows)
            {
                var category = row[dalItem.ItemCat].ToString();
                var itemName = row[dalItem.ItemName].ToString();
                var itemCode = row[dalItem.ItemCode].ToString();

                var masterCode = $"[{category}] {itemName}";

                if (itemName != itemCode)
                {
                    masterCode += $" ({itemCode})";
                }

                row[header_MasterCode] = masterCode;
                masterCodes.Add(masterCode);
            }

            txtItem.Values = masterCodes.ToArray();
        }

        trfCatDAL daltrfCat = new trfCatDAL();
        private void InitialLocationFromAndInComboBox()
        {
           
            DataTable dtlocationCat = daltrfCat.Select();
            dtlocationCat.DefaultView.Sort = "trf_cat_name ASC";
            dtlocationCat = dtlocationCat.DefaultView.ToTable();

            DataTable fromCatTable = dtlocationCat.Copy();
            cmbDefaultInCat.DataSource = fromCatTable;
            cmbDefaultInCat.DisplayMember = "trf_cat_name";
            cmbDefaultInCat.ValueMember = "trf_cat_id";
            cmbDefaultInCat.SelectedIndex = -1;

            DataTable toCatTable = dtlocationCat.Copy();
            cmbDefaultOutCat.DataSource = toCatTable;
            cmbDefaultOutCat.DisplayMember = "trf_cat_name";
            cmbDefaultOutCat.ValueMember = "trf_cat_id";
            cmbDefaultOutCat.SelectedIndex = -1;
        }

        private void FieldReset()
        {
            txtItem.Text = ITEM_SEARCH_DEFAULT_TEXT;
            cmbStockLocation.SelectedIndex = -1;
            cmbDefaultInCat.SelectedIndex = -1;
            cmbDefaultOutCat.SelectedIndex = -1;
            txtCountUnit.Text = "pcs";
            txtUnitConversionRate.Text = "1";
            txtSystemUnit.Text = "pcs";
            ITEM_CODE = "ITEM CODE";
            ITEM_CATEGORY = "CATEGORY";
        }

        private void InitialLocationComboBox(DataTable dt)
        {
            cmbStockLocation.DataSource = dt;
            cmbStockLocation.DisplayMember = dalFac.FacName;
            cmbStockLocation.ValueMember = dalFac.FacID;

            //get default location from list table
            if(DT_STOCK_COUNT_LIST?.Rows.Count > 0)
            {
                foreach(DataRow row in DT_STOCK_COUNT_LIST.Rows)
                {
                    string tblCode = row[dalStockCountList.TblCode].ToString();

                    if(tblCode == LIST_TBL_CODE.ToString())
                    {
                        string facTblCode = row[dalStockCountList.DefaultFactoryTblCode].ToString();

                        //to-do:
                        bool foundMatch = false;
                        for (int i = 0; i < cmbStockLocation.Items.Count; i++)
                        {
                            DataRowView drv = (DataRowView)cmbStockLocation.Items[i];
                            if (drv[dalFac.FacID].ToString() == facTblCode)
                            {
                                cmbStockLocation.SelectedIndex = i;
                                foundMatch = true;
                                break; // Exit the loop once the match is found
                            }
                        }

                        if (!foundMatch)
                        {
                            // Handle the case where no matching value is found
                            // For example, you could set the selected index to -1 or to a default value
                            cmbStockLocation.SelectedIndex = -1;
                        }
                    }
                }
            }
            else
            {
                cmbStockLocation.SelectedIndex = -1;
            }
        }

        private void LoadStockCountListData()
        {
            DT_STOCK_COUNT_LIST = dalStockCountList.SelectAll();
        }

        private void LoadActiveFactoryListData()
        {
            DT_ACTIVE_FACTORY_LIST = dalFac.NewSelectDESC();
        }

        private void LoadStockCountListItemData()
        {
            DT_STOCK_COUNT_LIST_ITEM = dalStockCountListItem.SelectListItem(LIST_TBL_CODE);
        }


        private void LoadExistingData()
        {
           if(DT_STOCK_COUNT_LIST_ITEM?.Rows.Count > 0 && ITEM_TBL_CODE > 0)
            {
                foreach(DataRow row in DT_STOCK_COUNT_LIST_ITEM.Rows)
                {
                    string tblCode = row[dalStockCountList.TblCode].ToString();

                    if(tblCode == ITEM_TBL_CODE.ToString())
                    {
                        txtCountUnit.Text = row[dalStockCountListItem.CountUnit].ToString();
                        txtUnitConversionRate.Text = row[dalStockCountListItem.UnitConversionRate].ToString();
                        txtCountUnit.Text = row[dalStockCountList.ListDescription].ToString();

                        string facID = row[dalStockCountList.DefaultFactoryTblCode].ToString();

                        foreach(DataRow rowFac in DT_ACTIVE_FACTORY_LIST.Rows)
                        {
                            if(facID == rowFac[dalFac.FacID].ToString())
                            {
                                cmbStockLocation.Text = rowFac[dalFac.FacName].ToString();

                                break;
                            }    
                        }

                        break;
                    }
                }
            }

        }

        private bool Validation()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            bool result = true;

            if (string.IsNullOrEmpty(ITEM_CODE) || ITEM_CODE == "ITEM CODE")
            {
                errorProvider1.SetError(lblItem, "Item Invalid");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbStockLocation.Text))
            {
                errorProvider2.SetError(lblStockLocation, "Stock Location Required");
                result = false;
            }

            //if (DT_STOCK_COUNT_LIST?.Rows.Count > 0)
            //{
            //    string newListName = txtCountUnit.Text.ToUpper().Replace(" ", "");
            //    foreach (DataRow row in DT_STOCK_COUNT_LIST.Rows)
            //    {
            //        string listName = row[dalStockCountList.ListDescription].ToString().ToUpper().Replace(" ", "");
            //        string tblCode = row[dalStockCountList.TblCode].ToString();

            //        //if (listName == newListName)
            //        //{
            //        //    if((TBL_CODE != -1 && tblCode != TBL_CODE.ToString()) || TBL_CODE == -1)
            //        //    {
            //        //        lblListNameErrorMessage.Text = "Name already taken, please choose a different one.";
            //        //        errorProvider1.SetError(lblListName, "Name already taken, please choose a different one.");
            //        //        lblListNameErrorMessage.Visible = true;
            //        //        result = false;
            //        //        break;
            //        //    }
            //        //}

            //    }
            //}

            return result;
        }

        private void ListItemInsert()
        {
            dalStockCountListItem.CreateTable();

            if (cmbStockLocation.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockLocation.SelectedItem;

                int facID = int.TryParse(selectedRow[dalFac.FacID].ToString(), out int i) ? i : 3;
                uStockCountListItem.factory_tbl_code = facID;
            }

            if (cmbDefaultOutCat.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbDefaultOutCat.SelectedItem;

                int facID = int.TryParse(selectedRow["trf_cat_id"].ToString(), out int i) ? i : 3;
                uStockCountListItem.default_out_cat_tbl_code = facID;
            }

            if (cmbDefaultInCat.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbDefaultInCat.SelectedItem;

                int facID = int.TryParse(selectedRow["trf_cat_id"].ToString(), out int i) ? i : 3;
                uStockCountListItem.default_int_cat_tbl_code = facID;
            }


            uStockCountListItem.list_tbl_code = LIST_TBL_CODE;
            uStockCountListItem.item_code = ITEM_CODE;

            uStockCountListItem.count_unit = string.IsNullOrEmpty(txtCountUnit.Text) ? "pcs" : txtCountUnit.Text;
            uStockCountListItem.unit_conversion_rate = double.TryParse(txtUnitConversionRate.Text,out double d)?d : 1;

            uStockCountListItem.remark = "";
            uStockCountListItem.isRemoved = false;
            uStockCountListItem.updated_date = DateTime.Now;
            uStockCountListItem.updated_by = MainDashboard.USER_ID;
            uStockCountListItem.tbl_code = 1;


            if (!dalStockCountListItem.Insert(uStockCountListItem))
            {
                MessageBox.Show("Failed to insert new item to list!");
            }
            else
            {
                MessageBox.Show("New item added to list!\nYou can continue to add another new item.");

                //reset field
                FieldReset();

                DATA_SAVED = true;
            }
        }

        private void ListItemUpdate()
        {
            if (cmbStockLocation.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockLocation.SelectedItem;

                int facID = int.TryParse(selectedRow[dalFac.FacID].ToString(), out int i) ? i : 3;
                uStockCountListItem.factory_tbl_code = facID;
            }

            if (cmbDefaultOutCat.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbDefaultOutCat.SelectedItem;

                int facID = int.TryParse(selectedRow["trf_cat_id"].ToString(), out int i) ? i : 3;
                uStockCountListItem.default_out_cat_tbl_code = facID;
            }

            if (cmbDefaultInCat.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbDefaultInCat.SelectedItem;

                int facID = int.TryParse(selectedRow["trf_cat_id"].ToString(), out int i) ? i : 3;
                uStockCountListItem.default_int_cat_tbl_code = facID;
            }


            uStockCountListItem.list_tbl_code = LIST_TBL_CODE;
            uStockCountListItem.item_code = ITEM_CODE;

            uStockCountListItem.count_unit = string.IsNullOrEmpty(txtCountUnit.Text) ? "pcs" : txtCountUnit.Text;

            uStockCountListItem.unit_conversion_rate = double.TryParse(txtUnitConversionRate.Text, out double d) ? d : 1;

            uStockCountListItem.remark = "";
            uStockCountListItem.isRemoved = false;
            uStockCountListItem.updated_date = DateTime.Now;
            uStockCountListItem.updated_by = MainDashboard.USER_ID;
            uStockCountListItem.tbl_code = ITEM_TBL_CODE;




            if (!dalStockCountListItem.Update(uStockCountListItem))
            {
                MessageBox.Show("Failed to update item!");
            }
            else
            {
                MessageBox.Show("Item updated to list!");

                DATA_SAVED = true;
                Close();

            }
        }
        private void frmStockCountListEditing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        #endregion

      
        private void InsertButtonTextChange()
        {
            if (ITEM_TBL_CODE == -1)
            {
                btnInsert.Text = BUTTON_INSERT_TEXT;
            }
            else
            {
                btnInsert.Text = BUTTON_UPDATE_TEXT;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
                

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                if(ITEM_TBL_CODE == -1)
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm adding new item to list?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        ListItemInsert();
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm to make changes? ", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        ListItemUpdate();
                    }
                }
               
            }
            
        }

        private void txtListName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void cmbStockLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        private void txtItemDescription_Enter(object sender, EventArgs e)
        {
            if (txtItem.Text == ITEM_SEARCH_DEFAULT_TEXT)
            {
                txtItem.Text = "";
                txtItem.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtItemDescription_Leave(object sender, EventArgs e)
        {
            if (txtItem.Text.Length == 0 || string.IsNullOrEmpty(txtItem.Text.Replace(" ", "")))
            {
                txtItem.Text = ITEM_SEARCH_DEFAULT_TEXT;
                txtItem.ForeColor = SystemColors.GrayText;
                txtItem.Font = new Font(txtItem.Font, FontStyle.Italic);
            }
        }

        private string ITEM_CODE = "ITEM CODE";
        private string ITEM_CATEGORY = "CATEGORY";

        private void GetItemUnit(string itemCategory)
        {
            string SystemUnit = "pcs";
            string CountUnit = "";
            string UnitConversionRate = "";

            if (itemCategory.ToUpper() == text.Cat_RawMat.ToUpper())
            {
                SystemUnit = "kg";
                CountUnit = "bag";
                UnitConversionRate = "25";
            }

            txtCountUnit.Text = CountUnit;
            txtSystemUnit.Text = SystemUnit;

            if(string.IsNullOrEmpty(txtUnitConversionRate.Text))
                txtUnitConversionRate.Text = UnitConversionRate;

        }

        private void AutoSelectFromOrToLocation(string itemCategory)
        {
            if (itemCategory.ToUpper() == text.Cat_RawMat.ToUpper())
            {
                //text.Production; text.Assembly;
                cmbDefaultOutCat.Text = text.Production;
            }
        }

        joinDAL dalItemGroup = new joinDAL();
        private string LoadStdPacking(string itemCode)
        {
            DataTable DB_Join = dalItemGroup.loadChildList(itemCode);

            foreach (DataRow row in DB_Join.Rows)
            {
                string max = row[dalItemGroup.JoinMax].ToString();
                bool isMainCarton = bool.TryParse(row[dalItemGroup.JoinMainCarton].ToString(), out isMainCarton) ? isMainCarton : false;

                if (isMainCarton)
                {
                    return max;
                }
            }

            return "";
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtItem.Text;

            if (!string.IsNullOrEmpty(keywords) && keywords != ITEM_SEARCH_DEFAULT_TEXT)
            {
                foreach (DataRow row in DT_ITEM_SOURCE.Rows)
                {

                    if (keywords == row[header_MasterCode].ToString())
                    {
                        ITEM_CATEGORY = row[dalItem.ItemCat].ToString();
                        ITEM_CODE = row[dalItem.ItemCode].ToString();

                        //get item std packing : main carton = true, max parent qty
                        txtUnitConversionRate.Text = LoadStdPacking(ITEM_CODE);

                        GetItemUnit(ITEM_CATEGORY);
                        AutoSelectFromOrToLocation(ITEM_CATEGORY);

                        DATA_SAVED = false;
                        break;
                    }
                }
            }
            else
            {
                ITEM_CODE = "ITEM CODE";
                ITEM_CATEGORY = "CATEGORY";
            }
        }

        private void txtUnitConversionRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && txtUnitConversionRate.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private bool UNIT_CONVERSION_EXAMPLE_LOADING = false;

        private void RefreshUnitConversionExample()
        {
            if(!UNIT_CONVERSION_EXAMPLE_LOADING)
            {
                UNIT_CONVERSION_EXAMPLE_LOADING = true;

                string conversionExample = "Example: ";

                conversionExample += "1 " + txtCountUnit.Text + " = " + txtUnitConversionRate.Text + txtSystemUnit.Text;

                lblUnitExample.Text = conversionExample;

                UNIT_CONVERSION_EXAMPLE_LOADING = false;
            }
        }
        private void txtCountUnit_TextChanged(object sender, EventArgs e)
        {
            RefreshUnitConversionExample();
        }

        private void txtUnitConversionRate_TextChanged(object sender, EventArgs e)
        {
            RefreshUnitConversionExample();

        }

        private void txtSystemUnit_TextChanged(object sender, EventArgs e)
        {
            RefreshUnitConversionExample();

        }
    }
}
