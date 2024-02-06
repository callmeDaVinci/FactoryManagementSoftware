using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using Font = System.Drawing.Font;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Data.Entity.Core.Metadata.Edm;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockCountManagement : Form
    {
       
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        facDAL dalFac = new facDAL();
        stockCountListDAL dalStockCountList = new stockCountListDAL();
        stockCountListBLL uStockCountList = new stockCountListBLL();

        stockCountListItemDAL dalStockCountListItem = new stockCountListItemDAL();

        DataTable DT_STOCK_COUNT_LIST;

        private bool INITIAL_SETTING_LOADED = false;

        private bool DATA_SAVED = true;

        public frmStockCountManagement()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvStockCountList, true);

            InitialSetting();

        }
      
        private void InitialSetting()
        {

            LoadStockCountListData();
            InitialStockCountListComboBox(DT_STOCK_COUNT_LIST);
         
        }

        private void InitialStockCountListComboBox(DataTable dt)
        {
            cmbStockCountList.DataSource = null;

            if(dt?.Rows.Count > 0)
            {
                cmbStockCountList.DataSource = dt;
                cmbStockCountList.DisplayMember = dalStockCountList.ListDescription;
                cmbStockCountList.ValueMember = dalStockCountList.TblCode;
                cmbStockCountList.SelectedIndex = -1;
            }
        }

        private void LoadStockCountListData()
        {
            DT_STOCK_COUNT_LIST = dalStockCountList.SelectAll();
        }


        private void LoadStockCountList()
        {
            //if (DT_STOCK_COUNT_LIST?.Rows.Count > 0 && TBL_CODE > 0)
            //{
            //    foreach (DataRow row in DT_STOCK_COUNT_LIST.Rows)
            //    {
            //        string tblCode = row[dalStockCountList.TblCode].ToString();

            //        if (tblCode == TBL_CODE.ToString())
            //        {
            //            txtListName.Text = row[dalStockCountList.ListDescription].ToString();

            //            string facID = row[dalStockCountList.DefaultFactoryTblCode].ToString();

            //            foreach (DataRow rowFac in DT_ACTIVE_FACTORY_LIST.Rows)
            //            {
            //                if (facID == rowFac[dalFac.FacID].ToString())
            //                {
            //                    cmbStockLocation.Text = rowFac[dalFac.FacName].ToString();

            //                    break;
            //                }
            //            }

            //            break;
            //        }
            //    }
            //}

        }

        private bool Validation()
        {
            //lblListNameErrorMessage.Visible = false;
            //errorProvider1.Clear();
            //errorProvider2.Clear();
            bool result = true;

            //if (string.IsNullOrEmpty(txtListName.Text))
            //{
            //    errorProvider1.SetError(lblListName, "List Name Required");
            //    result = false;
            //}

            //if (string.IsNullOrEmpty(cmbStockLocation.Text))
            //{
            //    errorProvider2.SetError(lblStockLocation, "Stock Location Required");
            //    result = false;
            //}

            //if (DT_STOCK_COUNT_LIST?.Rows.Count > 0)
            //{
            //    string newListName = txtListName.Text.ToUpper().Replace(" ", "");
            //    foreach (DataRow row in DT_STOCK_COUNT_LIST.Rows)
            //    {
            //        string listName = row[dalStockCountList.ListDescription].ToString().ToUpper().Replace(" ", "");
            //        string tblCode = row[dalStockCountList.TblCode].ToString();

            //        if (listName == newListName)
            //        {
            //            if ((TBL_CODE != -1 && tblCode != TBL_CODE.ToString()) || TBL_CODE == -1)
            //            {
            //                lblListNameErrorMessage.Text = "Name already taken, please choose a different one.";
            //                lblListNameErrorMessage.Visible = true;
            //                result = false;
            //                break;
            //            }
            //        }

            //    }
            //}

            return result;
        }
      
        private void frmStockCountManagement_FormClosing(object sender, FormClosingEventArgs e)
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


        private DataTable NewStockCountList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_TableCode, typeof(int));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));

            dt.Columns.Add(text.Header_StockLocation_TblCode, typeof(int));
            dt.Columns.Add(text.Header_StockLocation, typeof(string));

            dt.Columns.Add(text.Header_SystemStock, typeof(double));

            dt.Columns.Add(text.Header_InFrom_TblCode, typeof(int));
            dt.Columns.Add(text.Header_OutTo_TblCode, typeof(int));

            dt.Columns.Add(text.Header_OutTo, typeof(string));
            dt.Columns.Add(text.Header_InFrom, typeof(string));

            dt.Columns.Add(text.Header_StockCount, typeof(double));
            dt.Columns.Add(text.Header_CountUnit, typeof(string));

            dt.Columns.Add(text.Header_UnitConversionRate, typeof(double));
            dt.Columns.Add(text.Header_TotalQty, typeof(double));
            dt.Columns.Add(text.Header_Unit, typeof(string));
            dt.Columns.Add(text.Header_ActionPreview, typeof(string));
            dt.Columns.Add(text.Header_Selection, typeof(bool));

            return dt;

        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dgv == dgvStockCountList)
            {
                //dgv.Columns[header_PODate].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                //dgv.Columns[header_PONo].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                dgv.Columns[text.Header_StockLocation].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_SystemStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_StockCount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_CountUnit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_UnitConversionRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_TotalQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_TableCode].Visible = false;
                dgv.Columns[text.Header_StockLocation_TblCode].Visible = false;
                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_InFrom_TblCode].Visible = false;
                dgv.Columns[text.Header_OutTo_TblCode].Visible = false;
            }
           

        }

        private void AddNewList_Click(object sender, EventArgs e)
        {
            frmStockCountListSetting frm = new frmStockCountListSetting();

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.ShowDialog();

            LoadStockCountListData();
            InitialStockCountListComboBox(DT_STOCK_COUNT_LIST);
        }

        private void cmbStockCountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            lblEditList.Visible = cmbStockCountList.SelectedIndex > -1;

            btnAddItem.Visible = cmbStockCountList.SelectedIndex > -1;

            LoadStockCountListItem();
        }

        private void lblEditList_Click(object sender, EventArgs e)
        {
            // Get the DataRowView for the selected item
            DataRowView selectedRow = (DataRowView)cmbStockCountList.SelectedItem;

            int tblCode = int.TryParse(selectedRow[dalStockCountList.TblCode].ToString(), out int i) ? i : -1;

            frmStockCountListSetting frm = new frmStockCountListSetting(tblCode);

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.ShowDialog();

            LoadStockCountListData();
            InitialStockCountListComboBox(DT_STOCK_COUNT_LIST);
        }

        trfCatDAL daltrfCat = new trfCatDAL();
        itemDAL dalItem = new itemDAL();
        facStockDAL dalFacStock = new facStockDAL();

        DataTable DT_LOCATION_CATEGORY;
        DataTable DT_ITEM;
        DataTable DT_FAC_STOCK;

        private void LoadLocationCategoryData()
        {
            DataTable dtlocationCat = daltrfCat.Select();
            dtlocationCat.DefaultView.Sort = "trf_cat_name ASC";
            DT_LOCATION_CATEGORY = dtlocationCat.DefaultView.ToTable();

            //DataTable fromCatTable = dtlocationCat.Copy();
            //cmbDefaultInCat.DataSource = fromCatTable;
            //cmbDefaultInCat.DisplayMember = "trf_cat_name";
            //cmbDefaultInCat.ValueMember = "trf_cat_id";
            //cmbDefaultInCat.SelectedIndex = -1;
        }

        private void LoadItemData()
        {
            DT_ITEM = dalItem.Select();
        }

        private void LoadFacStockData()
        {
            DT_FAC_STOCK = dalFacStock.Select();
        }

        private void LoadStockCountListItem()
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data found. confirm to load data? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
            }

            if(!INITIAL_SETTING_LOADED)
            {
                return;
            }

            if (cmbStockCountList.SelectedIndex != -1)
            {
                if(DT_LOCATION_CATEGORY == null || DT_LOCATION_CATEGORY.Rows.Count == 0)
                {
                    LoadLocationCategoryData();
                }

                if (DT_ITEM == null || DT_ITEM.Rows.Count == 0)
                {
                    LoadItemData();
                }

                if (DT_FAC_STOCK == null || DT_FAC_STOCK.Rows.Count == 0)
                {
                    LoadFacStockData();
                }

                DataTable dt_List_Item;

                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockCountList.SelectedItem;
                int listTblCode = int.TryParse(selectedRow[dalStockCountList.TblCode].ToString(), out int i) ? i : -1;

                DataTable DB_StockCountListItem = dalStockCountListItem.SelectListItem(listTblCode);
                int index = 0;

                if (dgvStockCountList?.Rows.Count > 0)
                {
                    // Existing IDs in dgvStockCountList
                    HashSet<string> existingIDs = new HashSet<string>();
                    foreach (DataGridViewRow dgvRow in dgvStockCountList.Rows)
                    {
                        if (dgvRow.Cells[text.Header_TableCode]?.Value != null)
                            existingIDs.Add(dgvRow.Cells[text.Header_TableCode].Value.ToString());

                        index = int.TryParse(dgvRow.Cells[text.Header_Index].Value.ToString(), out i) ? i : 0;
                    }

                    dt_List_Item = (DataTable)dgvStockCountList.DataSource;

                    foreach (DataRow dtRow in DB_StockCountListItem.Rows)
                    {
                        index++;
                        DataRow newRow = dt_List_Item.NewRow();

                        newRow[text.Header_TableCode] = int.TryParse(dtRow[dalStockCountListItem.TblCode].ToString(), out i) ? i : -1;
                        newRow[text.Header_Index] = index;

                        string itemCode = dtRow[dalStockCountListItem.ItemCode].ToString();
                        string itemName = "";
                        string itemUnit = "pcs";
                        string itemCat = "";

                        foreach (DataRow row in DT_ITEM.Rows)
                        {
                            if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                            {
                                itemName = row[dalItem.ItemName].ToString();
                                itemUnit = row[dalItem.ItemUnit].ToString();
                                itemCat = row[dalItem.ItemCat].ToString();

                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(itemUnit))
                        {
                            if (itemCat == text.Cat_RawMat || itemCat == text.Cat_MB || itemCat == text.Cat_Pigment)
                            {
                                itemUnit = text.Unit_KG;
                            }
                            else
                            {
                                itemUnit = "pcs";
                            }
                        }

                        string itemDescription = itemName + " (" + itemCode + ")";

                        if (itemCode.ToUpper().Replace(" ", "") == itemName.ToUpper().Replace(" ", ""))
                        {
                            itemDescription = itemName;
                        }

                        int stockFacID = int.TryParse(dtRow[dalStockCountListItem.FactoryTblCode].ToString(), out i) ? i : -1;
                        string facName = tool.getFactoryName(DT_FAC_STOCK, stockFacID.ToString());
                        double stock = tool.getFacStock(DT_FAC_STOCK, itemCode, stockFacID.ToString());

                        newRow[text.Header_ItemCode] = itemCode;
                        newRow[text.Header_ItemName] = itemName;
                        newRow[text.Header_ItemDescription] = itemDescription;
                        newRow[text.Header_StockLocation] = facName;
                        newRow[text.Header_StockLocation_TblCode] = stockFacID;
                        newRow[text.Header_SystemStock] = stock;

                        int OutToTblCode = int.TryParse(dtRow[dalStockCountListItem.DefaultOutCatTblCode].ToString(), out i) ? i : -1;
                        int InFromTblCode = int.TryParse(dtRow[dalStockCountListItem.DefaultInCatTblCode].ToString(), out i) ? i : -1;

                        string OutToLocation = "";
                        string InFromLocation = "";

                        foreach (DataRow row in DT_LOCATION_CATEGORY.Rows)
                        {
                            if (row["trf_cat_id"].ToString().Equals(OutToTblCode))
                            {
                                OutToLocation = row["trf_cat_name"].ToString();

                            }

                            if (row["trf_cat_id"].ToString().Equals(InFromTblCode))
                            {
                                InFromLocation = row["trf_cat_name"].ToString();
                            }
                        }

                        newRow[text.Header_OutTo_TblCode] = OutToTblCode;
                        newRow[text.Header_InFrom_TblCode] = InFromTblCode;
                        newRow[text.Header_OutTo] = OutToLocation;
                        newRow[text.Header_InFrom] = InFromLocation;

                        newRow[text.Header_CountUnit] = dtRow[dalStockCountListItem.CountUnit].ToString();

                        double unitConversionRate = double.TryParse(dtRow[dalStockCountListItem.UnitConversionRate].ToString(), out double d) ? d : 1;

                        newRow[text.Header_UnitConversionRate] = unitConversionRate;

                        newRow[text.Header_Unit] = itemUnit;
                        newRow[text.Header_Selection] = false;


                        dt_List_Item.Rows.Add(newRow);
                    }
                }
                else
                {
                    dt_List_Item = NewStockCountList();

                    foreach (DataRow dtRow in DB_StockCountListItem.Rows)
                    {
                        index++;
                        DataRow newRow = dt_List_Item.NewRow();

                        newRow[text.Header_TableCode] = int.TryParse(dtRow[dalStockCountListItem.TblCode].ToString(), out i) ? i : -1;
                        newRow[text.Header_Index] = index;

                        string itemCode = dtRow[dalStockCountListItem.ItemCode].ToString();
                        string itemName = "";
                        string itemUnit = "pcs";
                        string itemCat = "";

                        foreach (DataRow row in DT_ITEM.Rows)
                        {
                            if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                            {
                                itemName = row[dalItem.ItemName].ToString();
                                itemUnit = row[dalItem.ItemUnit].ToString();
                                itemCat = row[dalItem.ItemCat].ToString();

                                break;
                            }
                        }

                        if(string.IsNullOrEmpty(itemUnit))
                        {
                            if(itemCat == text.Cat_RawMat || itemCat == text.Cat_MB || itemCat == text.Cat_Pigment)
                            {
                                itemUnit = text.Unit_KG;
                            }
                            else
                            {
                                itemUnit = "pcs";
                            }
                        }

                        string itemDescription = itemName + " (" + itemCode + ")";

                        if (itemCode.ToUpper().Replace(" ", "") == itemName.ToUpper().Replace(" ", ""))
                        {
                            itemDescription = itemName;
                        }

                        int stockFacID = int.TryParse(dtRow[dalStockCountListItem.FactoryTblCode].ToString(), out i) ? i : -1;
                        string facName = tool.getFactoryName(DT_FAC_STOCK, stockFacID.ToString());
                        double stock = tool.getFacStock(DT_FAC_STOCK, itemCode, stockFacID.ToString());

                        newRow[text.Header_ItemCode] = itemCode;
                        newRow[text.Header_ItemName] = itemName;
                        newRow[text.Header_ItemDescription] = itemDescription;
                        newRow[text.Header_StockLocation] = facName;
                        newRow[text.Header_StockLocation_TblCode] = stockFacID;
                        newRow[text.Header_SystemStock] = stock;

                        int OutToTblCode = int.TryParse(dtRow[dalStockCountListItem.DefaultOutCatTblCode].ToString(), out i) ? i : -1;
                        int InFromTblCode = int.TryParse(dtRow[dalStockCountListItem.DefaultInCatTblCode].ToString(), out i) ? i : -1;

                        string OutToLocation = "";
                        string InFromLocation = "";

                        foreach (DataRow row in DT_LOCATION_CATEGORY.Rows)
                        {
                            string trfCatID = row["trf_cat_id"].ToString();

                            if (trfCatID == OutToTblCode.ToString())
                            {
                                OutToLocation = row["trf_cat_name"].ToString();
                               
                            }

                            if (trfCatID== InFromTblCode.ToString())
                            {
                                InFromLocation = row["trf_cat_name"].ToString();
                            }
                        }

                        newRow[text.Header_OutTo_TblCode] = OutToTblCode;
                        newRow[text.Header_InFrom_TblCode] = InFromTblCode;
                        newRow[text.Header_OutTo] = OutToLocation;
                        newRow[text.Header_InFrom] = InFromLocation;

                        newRow[text.Header_CountUnit] = dtRow[dalStockCountListItem.CountUnit].ToString();

                        double unitConversionRate = double.TryParse(dtRow[dalStockCountListItem.UnitConversionRate].ToString(), out double d) ? d : 1;

                        newRow[text.Header_UnitConversionRate] = unitConversionRate;

                        newRow[text.Header_Unit] = itemUnit;
                        newRow[text.Header_Selection] = false;


                        dt_List_Item.Rows.Add(newRow);
                    }

                    if(dt_List_Item?.Rows.Count > 0)
                    {
                        dgvStockCountList.DataSource = dt_List_Item;
                        DgvUIEdit(dgvStockCountList);
                    }
                  

                }

                dgvStockCountList.ClearSelection();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //if (cmbStockLocation.SelectedIndex != -1)
            //{
            //    // Get the DataRowView for the selected item
            //    DataRowView selectedRow = (DataRowView)cmbStockLocation.SelectedItem;

            //    int facID = int.TryParse(selectedRow[dalFac.FacID].ToString(), out int i) ? i : 3;
            //    uStockCountList.default_factory_tbl_code = facID;
            //}

            if (cmbStockCountList.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockCountList.SelectedItem;

                int tblCode = int.TryParse(selectedRow[dalStockCountList.TblCode].ToString(), out int i) ? i : -1;

                frmStockCountListItemSetting frm = new frmStockCountListItemSetting(tblCode);

                frm.StartPosition = FormStartPosition.CenterScreen;

                frm.ShowDialog();

                LoadStockCountListItem();
            }
            else
            {
                MessageBox.Show("No List Type Found!");

               

                errorProvider1.SetError(lblStockCountList, "List Invalid");
            }
                
               
        }

        private void frmStockCountManagement_Shown(object sender, EventArgs e)
        {
            INITIAL_SETTING_LOADED = true;
        }
    }
}
