using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;


namespace FactoryManagementSoftware.UI
{
    public partial class frmItemMasterList : Form
    {
        #region Form Called

        public frmItemMasterList()
        {
            InitializeComponent();

            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
        }

        #endregion

        #region Object & Variable Declare

        private int userPermission = -1;

        Tool tool = new Tool();
        Text text = new Text();

        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();

        userDAL dalUser = new userDAL();

        readonly string text_ShowFilter = "SHOW FILTER";
        readonly string text_HideFilter = "HIDE FILTER";

        readonly string MODE_GENERAL_INFO = "GENERAL INFO";
        readonly string MODE_GROUP = "ITEM GROUP";
        readonly string MODE_TRASACTION = "TRASACTION RECORD";
        readonly string MODE_PRODUCTION = "PRODUCTION RECORD";
        readonly string MODE_STOCK_LOCATION = "STOCK LOCATION";
        readonly string MODE_CUSTOMER = "CUSTOMER";

        private string CURRENT_SELECTED_ITEMCODE = "";

        private DataTable DB_ITEM_LIST;

        #endregion

        #region Initial/Reset

        private void PageReset()
        {
            ShowFilter(true);
            ShowSubListButton(false);
            tool.loadItemCategoryDataToComboBox(cmbCategory);

            cmbCategory.Text = text.Cat_Part;

            tool.LoadCustomerAndAllToComboBox(cmbCust);


        }

        private void dgvItemListUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_ItemName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[text.Header_ItemCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[text.Header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void dgvMoreInfoEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            // dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (lblMoreInfo.Text.Contains(MODE_GENERAL_INFO))
            {
                dgv.Columns[text.Header_Data].Visible = false;

                dgv.Columns[text.Header_DataName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_DataName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(string));
            dt.Columns.Add(text.Header_Status, typeof(string));

            return dt;
        }

        private DataTable NewItemGeneralInfoTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Data, typeof(string));
            dt.Columns.Add(text.Header_DataName, typeof(string));
            dt.Columns.Add(text.Header_Description, typeof(string));

            return dt;
        }

        #endregion

        #region Load Data 

        private DataTable SubListDataFilter(DataTable dt)
        {
            DataTable dt_SubList = dt.Clone();

            foreach(DataRow row in dt_SubList.Rows)
            {
                string data = row[text.Header_DataName].ToString();

                if (data.Equals(dalItem.ItemCode))
                {
                    row[text.Header_DataName] = text.Header_ItemCode;
                }
                else if (data.Equals(dalItem.ItemCode))
                {
                    row[text.Header_DataName] = text.Header_ItemCode;
                }

            }

            return dt_SubList;
        }

        private void LoadItemList()
        {
            Cursor = Cursors.WaitCursor;
           
            if (DB_ITEM_LIST == null || DB_ITEM_LIST.Rows.Count < 0)
            {
                LoadDBItemList();
            }

            DataTable dt_Item = NewItemTable();
            DataRow newRow;

            int index = 1;
            string SearchText = txtSearch.Text.ToUpper();

            foreach(DataRow row in DB_ITEM_LIST.Rows)
            {
                string itemCat = row[dalItem.ItemCat].ToString();
                string itemCode = row[dalItem.ItemCode].ToString();
                string itemName = row[dalItem.ItemName].ToString();
                float stockBalance = float.TryParse(row[dalItem.ItemStock].ToString(), out stockBalance)? stockBalance : 0;

                bool isRemoved = bool.TryParse(row[dalItem.ItemisRemoved].ToString(), out isRemoved) ? isRemoved : false;

                string Status = "";

                bool FilterPassed = true;

                bool TerminatedItem = itemCode.ToUpper().Contains("TERMINATED");
                TerminatedItem |= itemCode.ToUpper().Contains("REMOVED");
                TerminatedItem |= itemCode.ToUpper().Contains("CANCEL");
                TerminatedItem |= itemName.ToUpper().Contains("TERMINATED");
                TerminatedItem |= itemName.ToUpper().Contains("CANCEL");
                TerminatedItem |= itemName.ToUpper().Contains("REMOVED");

                if (cbHideTerminatedItem.Checked && (TerminatedItem || isRemoved))
                {
                    FilterPassed = false;
                }

                if(!itemCat.Equals(cmbCategory.Text))
                {
                    FilterPassed = false;
                }

                if(!string.IsNullOrEmpty(SearchText))
                {
                    if (!itemCode.Contains(SearchText) && !itemName.Contains(SearchText))
                    {
                        FilterPassed = false;
                    }
                }
               

                if(FilterPassed)
                {
                    newRow = dt_Item.NewRow();

                    newRow[text.Header_Index] = index++;
                    newRow[text.Header_ItemCode] = itemCode;
                    newRow[text.Header_ItemName] = itemName;
                    newRow[text.Header_BalStock] = stockBalance.ToString("0.##");
                    newRow[text.Header_Status] = Status;

                    dt_Item.Rows.Add(newRow);
                }
            }

            dgvItemList.DataSource = dt_Item;
            dgvItemListUIEdit(dgvItemList);
            ResetMoreInfoList();

            CURRENT_SELECTED_ITEMCODE = "";

            Cursor = Cursors.Arrow;
        }

        private void RemoveTerminatedItem()
        {
            Cursor = Cursors.WaitCursor;

            LoadDBItemList();

            DataTable dt_Item = (DataTable)dgvItemList.DataSource;

            int index = 1;

            if(dt_Item != null)
            {
                dt_Item.AcceptChanges();
                foreach (DataRow row in dt_Item.Rows)
                {
                    string itemName = row[text.Header_ItemName].ToString();

                    bool TerminatedItem = itemName.ToUpper().Contains("TERMINATED");
                    TerminatedItem |= itemName.ToUpper().Contains("CANCEL");
                    TerminatedItem |= itemName.ToUpper().Contains("REMOVED");

                    if (cbHideTerminatedItem.Checked && TerminatedItem)
                    {
                        row.Delete();
                    }
                    else
                    {
                        row[text.Header_Index] = index++;
                    }

                }
                dt_Item.AcceptChanges();

            }


            ResetMoreInfoList();

            CURRENT_SELECTED_ITEMCODE = "";

            Cursor = Cursors.Arrow;
        }

        private void LoadDBItemList()
        {
            DB_ITEM_LIST = dalItem.Select();

            DB_ITEM_LIST.DefaultView.Sort = dalItem.ItemName + " ASC, " + dalItem.ItemCode + " ASC";
            DB_ITEM_LIST = DB_ITEM_LIST.DefaultView.ToTable();
        }

        private void UpdateRowToDBItemList(DataTable dt)
        {
            if(dt != null && dt.Rows.Count > 0)
            {
                DataColumnCollection columns = DB_ITEM_LIST.Columns;

                string itemCode = dt.Rows[0][dalItem.ItemCode].ToString();

                foreach(DataRow row in DB_ITEM_LIST.Rows)
                {
                    if(itemCode.Equals(row[dalItem.ItemCode].ToString()))
                    {
                        foreach(DataColumn col in dt.Columns)
                        {
                            string colName = col.ColumnName;
                            
                            if (columns.Contains(colName))
                            {
                                row[colName] = dt.Rows[0][colName];
                            }

                        }
                    }
                }
            }
        }

        private string GeneralInfoDataNameChange(string data)
        {
            string dataName = "";

            if (data.Equals(dalItem.ItemCat))
            {
                dataName = text.Header_Category;
            }
            else if (data.Equals(dalItem.ItemMaterial))
            {
                dataName = text.Header_Mat;
            }
            else if (data.Equals(dalItem.ItemName))
            {
                dataName = text.Header_ItemName;
            }
            else if (data.Equals(dalItem.ItemCode))
            {
                dataName = text.Header_ItemCode;
            }
            else if (data.Equals(dalItem.ItemColor))
            {
                dataName = text.Header_Color;
            }
            else if (data.Equals(dalItem.ItemProTon))
            {
                dataName = text.Header_ProTon;
            }
            else if (data.Equals(dalItem.ItemMBRate))
            {
                dataName = text.Header_ColorRate;
            }
            else if (data.Equals(dalItem.ItemProCTTo))
            {
                dataName = text.Header_ProCT;
            }
            else if (data.Equals(dalItem.ItemProPWPcs))
            {
                dataName = text.Header_ProPwPcs;
            }
            else if (data.Equals(dalItem.ItemProRWPcs))
            {
                dataName = text.Header_ProRwPcs;
            }
            else if (data.Equals(dalItem.ItemProPWShot))
            {
                dataName = text.Header_ProPwShot;
            }
            else if (data.Equals(dalItem.ItemProRWShot))
            {
                dataName = text.Header_ProRwShot;
            }
            else if (data.Equals(dalItem.ItemCavity))
            {
                dataName = text.Header_Cavity;
            }
            else if (data.Equals(dalItem.ItemProCooling))
            {
                dataName = text.Header_Cooling;
            }
            else if (data.Equals(dalItem.ItemWastage))
            {
                dataName = text.Header_WastageAllowed_Percentage;
            }
            else if (data.Equals(dalItem.ItemAddDate))
            {
                dataName = text.Header_AddedDate;
            }
            else if (data.Equals(dalItem.ItemAddBy))
            {
                dataName = text.Header_AddedBy;
            }
            else if (data.Equals(dalItem.ItemUpdateDate))
            {
                dataName = text.Header_UpdatedDate;
            }
            else if (data.Equals(dalItem.ItemUpdateBy))
            {
                dataName = text.Header_UpdatedBy;
            }
            else if (data.Equals(dalItem.ItemQuoTon))
            {
                dataName = text.Header_QuoTon;
            }
            else if (data.Equals(dalItem.ItemQuoCT))
            {
                dataName = text.Header_QuoCT;
            }
            else if (data.Equals(dalItem.ItemQuoPWPcs))
            {
                dataName = text.Header_QuoPwPcs;
            }
            else if (data.Equals(dalItem.ItemQuoRWPcs))
            {
                dataName = text.Header_QuoRwPcs;
            }
            return dataName;
        }

        private bool IfQuotationData(string data)
        {
            if (data.Equals(dalItem.ItemQuoTon))
            {
                return true;
            }
            else if (data.Equals(dalItem.ItemQuoCT))
            {
                return true;

            }
            else if (data.Equals(dalItem.ItemQuoPWPcs))
            {
                return true;
            }
            else if (data.Equals(dalItem.ItemQuoRWPcs))
            {
                return true;
            }

            return false;
        }
        private void LoadGeneralInfo()
        {
            dgvMoreInfo.DataSource = null;

            if (DB_ITEM_LIST == null || DB_ITEM_LIST.Rows.Count < 0)
            {
                LoadDBItemList();
            }

            int rowIndex = dgvItemList.CurrentCell.RowIndex;
            string itemCode = dgvItemList.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();

            CURRENT_SELECTED_ITEMCODE = itemCode;

            if (!string.IsNullOrEmpty(itemCode) && rowIndex > -1)
            {
                DataTable dt_MoreInfo = NewItemGeneralInfoTable();
                DataRow newRow;

                int index = 1;

                foreach (DataRow row in DB_ITEM_LIST.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        int DBRowIndex = DB_ITEM_LIST.Rows.IndexOf(row);

                        foreach (DataColumn col in DB_ITEM_LIST.Columns)
                        {
                            string Data = col.ColumnName;
                            string Description = DB_ITEM_LIST.Rows[DBRowIndex][Data].ToString();

                            string DataName = GeneralInfoDataNameChange(Data);
                            bool FilterPassed = true;

                            if(string.IsNullOrEmpty(DataName))
                            {
                                FilterPassed = false;
                            }

                            if (IfQuotationData(Data) && !cbShowQuotationItem.Checked)
                            {
                                FilterPassed = false;
                            }

                            if (FilterPassed)
                            {
                                if (Data.Equals(dalItem.ItemMBRate))
                                {
                                    Description = float.TryParse(Description, out float i) ? (i * 100).ToString() : 0.ToString();
                                }

                                if (Data.Equals(dalItem.ItemWastage))
                                {
                                    Description = float.TryParse(Description, out float i) ? (i * 100).ToString() : 0.ToString();
                                }

                                if (Data.Equals(dalItem.ItemAddBy))
                                {
                                    int userID = int.TryParse(Description, out int i) ? i : 0;

                                    Description = dalUser.getUsername(userID);
                                }

                                if (Data.Equals(dalItem.ItemUpdateBy))
                                {
                                    int userID = int.TryParse(Description, out int i) ? i : 0;

                                    Description = dalUser.getUsername(userID);
                                }

                                newRow = dt_MoreInfo.NewRow();

                                newRow[text.Header_Index] = index++;
                                newRow[text.Header_Data] = Data;
                                newRow[text.Header_DataName] = DataName;
                                newRow[text.Header_Description] = Description;

                                dt_MoreInfo.Rows.Add(newRow);
                            }
                        }

                        break;
                    }

                }


                dgvMoreInfo.DataSource = dt_MoreInfo;
                dgvMoreInfoEdit(dgvMoreInfo);
                dgvMoreInfo.ClearSelection();

                if(dt_MoreInfo.Rows.Count >= 0)
                {
                    ShowSubListButton(true);
                }
                else
                {
                    ShowSubListButton(false);

                }
            }

        }

        private void LoadGeneralInfo(DataTable dt)
        {
            dgvMoreInfo.DataSource = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                UpdateRowToDBItemList(dt);

                DataTable dt_MoreInfo = NewItemGeneralInfoTable();
                DataRow newRow;

                int index = 1;

                foreach (DataColumn col in dt.Columns)
                {
                    string Data = col.ColumnName;
                    string Description = dt.Rows[0][Data].ToString();

                    string DataName = GeneralInfoDataNameChange(Data);
                    bool FilterPassed = true;

                    if (string.IsNullOrEmpty(DataName))
                    {
                        FilterPassed = false;
                    }

                    if (IfQuotationData(Data) && !cbShowQuotationItem.Checked)
                    {
                        FilterPassed = false;
                    }

                    if (FilterPassed)
                    {
                        if (Data.Equals(dalItem.ItemMBRate))
                        {
                            Description = float.TryParse(Description, out float i) ? (i * 100).ToString() : 0.ToString();
                        }

                        if (Data.Equals(dalItem.ItemWastage))
                        {
                            Description = float.TryParse(Description, out float i) ? (i * 100).ToString() : 0.ToString();
                        }

                        if (Data.Equals(dalItem.ItemAddBy))
                        {
                            int userID = int.TryParse(Description, out int i) ? i : 0;

                            Description = dalUser.getUsername(userID);
                        }

                        if (Data.Equals(dalItem.ItemUpdateBy))
                        {
                            int userID = int.TryParse(Description, out int i) ? i : 0;

                            Description = dalUser.getUsername(userID);
                        }

                        newRow = dt_MoreInfo.NewRow();

                        newRow[text.Header_Index] = index++;
                        newRow[text.Header_Data] = Data;
                        newRow[text.Header_DataName] = DataName;
                        newRow[text.Header_Description] = Description;

                        dt_MoreInfo.Rows.Add(newRow);
                    }
                }

                dgvMoreInfo.DataSource = dt_MoreInfo;
                dgvMoreInfoEdit(dgvMoreInfo);
                dgvMoreInfo.ClearSelection();

                if (dt_MoreInfo.Rows.Count > 0)
                {
                    ShowSubListButton(true);
                }
                else
                {
                    ShowSubListButton(false);

                }
            }
            else
            {
                MessageBox.Show("DATATABLE FROM EDIT PAGE INVALID!");
            }
        }

        private void MoreInfoModeStation()
        {
            string CurrentMode = lblMoreInfo.Text;

            if (CurrentMode.Contains(MODE_GENERAL_INFO))
            {
                LoadGeneralInfo();
            }
            else if (CurrentMode.Contains(MODE_GROUP))
            {

            }
            else if (CurrentMode.Contains(MODE_TRASACTION))
            {

            }
            else if (CurrentMode.Contains(MODE_PRODUCTION))
            {

            }
            else if (CurrentMode.Contains(MODE_STOCK_LOCATION))
            {

            }
            else if (CurrentMode.Contains(MODE_CUSTOMER))
            {

            }
        }

        private void ItemUpdatedReturn(string ItemCode)
        {
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                if (Convert.ToInt32(row.Cells[text.Header_ItemCode].Value.ToString()).Equals(ItemCode))
                {
                    int rowIndex = row.Index;
                    row.Selected = true;
                    dgvItemList.FirstDisplayedScrollingRowIndex = rowIndex;

                    break;
                }
            }
        }

        private DataTable GetSelectedItemInfo()
        {
            if (DB_ITEM_LIST == null || DB_ITEM_LIST.Rows.Count < 0)
            {
                LoadDBItemList();
            }

            DataTable dt = DB_ITEM_LIST.Clone();

            if(string.IsNullOrEmpty(CURRENT_SELECTED_ITEMCODE))
            {
                MessageBox.Show("SELECTED ITEM CODE INVALID!");
            }
            else
            {
                foreach (DataRow row in DB_ITEM_LIST.Rows)
                {
                    if(CURRENT_SELECTED_ITEMCODE.Equals(row[dalItem.ItemCode].ToString()))
                    {
                        dt.ImportRow(row);
                        break;

                    }
                }
            }
           


            return dt;
        }

        private bool ItemTermination(string itemCode, string itemName)
        {
            bool result = false;
            uItem.item_code = itemCode;
            uItem.item_name = "(" + text.Terminated.ToUpper() + ")" + itemName;

            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            result = dalItem.ItemNameUpdate(uItem);
            if (!result)
            {
                MessageBox.Show("Failed to terminated item");
            }

            return result;
        }

        private bool ItemActivation(string itemCode, string itemName)
        {
            bool result = false;

            if (itemName.Contains("(" + text.Terminated.ToUpper() + ")"))
            {
                itemName = itemName.Replace("(" + text.Terminated.ToUpper() + ")", "");

                uItem.item_code = itemCode;
                uItem.item_name = itemName;

                uItem.item_updtd_date = DateTime.Now;
                uItem.item_updtd_by = MainDashboard.USER_ID;

                result = dalItem.ItemNameUpdate(uItem);

                if (!result)
                {
                    MessageBox.Show("Failed to activate item");
                }

            }
            else
            {
                MessageBox.Show("Not terminated item!");
            }

            return result;
        }

        private void Item_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                             //MessageBox.Show(e.ClickedItem.Name.ToString());

                DataGridView dgv = dgvItemList;

                int rowIndex = dgvItemList.CurrentCell.RowIndex;

                string itemCode = dgv.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();
                string itemName = dgv.Rows[rowIndex].Cells[text.Header_ItemName].Value.ToString();

                string clickedItem = e.ClickedItem.Name.ToString();

                if (dgv.SelectedRows.Count >= 0 && rowIndex >= 0)
                {
                    if (clickedItem.Equals(text.Terminated))
                    {
                        if (ItemTermination(itemCode, itemName))
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_ItemName].Value = "(" + text.Terminated.ToUpper() + ")" + itemName;
                            timer2.Stop();
                            timer2.Start();
                        }
                    }
                    else if (clickedItem.Equals(text.Activate))
                    {
                        if (ItemActivation(itemCode, itemName))
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_ItemName].Value = itemName.Replace("(" + text.Terminated.ToUpper() + ")", "");
                        }
                    }
                   

                }

                //listPaintAndKeepSelected(dgvItem);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }
        #endregion

        #region Form Action

        private void frmItemMasterList_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewItemListFormOpen = false;
        }

        private void frmItemMasterList_Load(object sender, EventArgs e)
        {
            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvMoreInfo, true);
            PageReset();
        }

        private void lblFilter_Click(object sender, EventArgs e)
        {
            ShowFilter();
        }

        private void ShowFilter()
        {
            string filterText = lblFilter.Text;

            if (filterText == text_ShowFilter)
            {
                lblFilter.Text = text_HideFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 150f);
            }
            else
            {
                lblFilter.Text = text_ShowFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowFilter(bool ShowFilter)
        {
            if (ShowFilter)
            {
                lblFilter.Text = text_HideFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 150f);
            }
            else
            {
                lblFilter.Text = text_ShowFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowSubListButton(bool toShow)
        {
            if (toShow)
            {
                tlpMoreInfo.RowStyles[1] = new RowStyle(SizeType.Absolute, 35f);
            }
            else
            {
                tlpMoreInfo.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void lblClearCMBCategory_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = -1;
        }

        private void lblClearCMBCust_Click(object sender, EventArgs e)
        {
            cmbCust.SelectedIndex = -1;
        }

        private void ResetMoreInfoList()
        {
            dgvItemList.ClearSelection();
            dgvMoreInfo.DataSource = null;
            ShowSubListButton(false);
        }

        private void cbShowQuotationItem_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowQuotationItem.Checked)
            {
                frmVerification frm = new frmVerification(text.PW_TopManagement);

                frm.ShowDialog();

                if (!frmVerification.PASSWORD_MATCHED)
                {
                    cbShowQuotationItem.Checked = false;
                }
                else
                {
                    ResetMoreInfoList();
                }
            }
          
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            frmItemEdit_NEW frm = new frmItemEdit_NEW();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            LoadItemList();

        }

        private void btnFilter_Click_1(object sender, EventArgs e)
        {
            ShowFilter();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCategory.Text != text.Cat_Part)
            {
                cmbCust.SelectedIndex = -1;
                cmbCust.Enabled = false;
            }
            else
            {
                cmbCust.Enabled = true;
                cmbCust.Text = "ALL";
            }
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            frmLoading.ShowLoadingScreen();

            Cursor = Cursors.WaitCursor;

            LoadItemList();

            Cursor = Cursors.Arrow;

            frmLoading.CloseForm();

        }

        private void ClearDGV()
        {
            dgvItemList.DataSource = null;
            dgvMoreInfo.DataSource = null;
            CURRENT_SELECTED_ITEMCODE = "";

            ShowSubListButton(false);
        }

        private void cbHideTerminatedItem_CheckedChanged(object sender, EventArgs e)
        {
            ClearDGV();

        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvItemList.DataSource != null)
            {
                MoreInfoModeStation();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dgvItemList.DataSource != null)
            {
                timer1.Stop();
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadItemList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblMoreInfo.Text.Contains(MODE_GENERAL_INFO))
            {
                DataTable dt = GetSelectedItemInfo();

                if(dt.Rows.Count > 1)
                {
                    MessageBox.Show("MULTIPLE ITEM ROW FOUND!");
                }
                frmItemEdit_NEW frm = new frmItemEdit_NEW(dt);

                frm.ShowDialog();

                if(frmItemEdit_NEW.DATA_SAVED)
                {
                    //CHANGE DB_ITEM_LIST DATA, CHANGE SUB LIST TABLE
                    LoadGeneralInfo(frmItemEdit_NEW.DT_DATA_SAVED);

                }
            }
        }

        private void dgvItemList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
            //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //    DataGridView dgv = dgvItemList;

            //    //handle the row selection on right click
            //    if (e.Button == MouseButtons.Right && userPermission >= MainDashboard.ACTION_LVL_TWO)
            //    {
            //        ContextMenuStrip my_menu = new ContextMenuStrip();

            //        dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            //        // Can leave these here - doesn't hurt
            //        dgv.Rows[e.RowIndex].Selected = true;
            //        dgv.Focus();
            //        int rowIndex = dgv.CurrentCell.RowIndex;

            //        string itemCode = dgv.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();
            //        string itemName = dgv.Rows[rowIndex].Cells[text.Header_ItemName].Value.ToString();

            //        if (itemName.Contains(text.Terminated) || itemName.Contains(text.Terminated.ToUpper()) || itemName.Contains("CANCEL"))
            //        {
            //            my_menu.Items.Add(text.Activate).Name = text.Activate;
            //        }
            //        else
            //        {
            //            my_menu.Items.Add(text.Terminated).Name = text.Terminated;
            //        }


            //        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

            //        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(Item_my_menu_ItemClicked);
            //    }

            //    Cursor = Cursors.Arrow; // change cursor to normal type
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            LoadDBItemList();
            RemoveTerminatedItem();
        }

        #endregion


    }
}
