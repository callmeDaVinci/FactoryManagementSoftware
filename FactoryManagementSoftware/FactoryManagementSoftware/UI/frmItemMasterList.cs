﻿using System;
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
        Tool tool = new Tool();
        Text text = new Text();

        itemDAL dalItem = new itemDAL();

        readonly string text_ShowFilter = "SHOW FILTER";
        readonly string text_HideFilter = "HIDE FILTER";

        readonly string MODE_GENERAL_INFO = "GENERAL INFO";
        readonly string MODE_GROUP = "ITEM GROUP";
        readonly string MODE_TRASACTION = "TRASACTION RECORD";
        readonly string MODE_PRODUCTION = "PRODUCTION RECORD";
        readonly string MODE_STOCK_LOCATION = "STOCK LOCATION";
        readonly string MODE_CUSTOMER = "CUSTOMER";

        private DataTable DB_ITEM_LIST;

        public frmItemMasterList()
        {
            InitializeComponent();
        }

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
           // dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_ItemName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[text.Header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

        private void LoadItemList()
        {
            if(DB_ITEM_LIST == null || DB_ITEM_LIST.Rows.Count < 0)
            {
                LoadDBItemList();
            }

            DataTable dt_Item = NewItemTable();
            DataRow newRow;

            int index = 1;

            foreach(DataRow row in DB_ITEM_LIST.Rows)
            {
                string itemCat = row[dalItem.ItemCat].ToString();
                string itemCode = row[dalItem.ItemCode].ToString();
                string itemName = row[dalItem.ItemName].ToString();
                float stockBalance = float.TryParse(row[dalItem.ItemStock].ToString(), out stockBalance)? stockBalance : 0;

                string Status = "";

                bool FilterPassed = true;

                bool TerminatedItem = itemCode.ToUpper().Contains("TERMINATED");
                TerminatedItem |= itemCode.ToUpper().Contains("REMOVED");
                TerminatedItem |= itemCode.ToUpper().Contains("CANCEL");
                TerminatedItem |= itemName.ToUpper().Contains("TERMINATED");
                TerminatedItem |= itemName.ToUpper().Contains("CANCEL");
                TerminatedItem |= itemName.ToUpper().Contains("REMOVED");

                if (cbHideTerminatedItem.Checked && TerminatedItem)
                {
                    FilterPassed = false;
                }

                if(!itemCat.Equals(cmbCategory.Text))
                {
                    FilterPassed = false;
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
            dgvItemList.ClearSelection();
            dgvMoreInfo.DataSource = null;

        }

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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowFilter();
        }

        private void ShowFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowFilter(bool ShowFilter)
        {
            if (ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpBase.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

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
            }
          
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            frmItemEdit_NEW frm = new frmItemEdit_NEW();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

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

        private void cbHideTerminatedItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvItemList.DataSource = null;

        }

        private void LoadDBItemList()
        {
            DB_ITEM_LIST = dalItem.Select();

            DB_ITEM_LIST.DefaultView.Sort = dalItem.ItemName + " ASC, " + dalItem.ItemCode + " ASC";
            DB_ITEM_LIST = DB_ITEM_LIST.DefaultView.ToTable();
        }
        private void LoadGeneralInfo()
        {
            dgvMoreInfo.DataSource = null;

            if(DB_ITEM_LIST == null || DB_ITEM_LIST.Rows.Count < 0 )
            {
                LoadDBItemList();
            }

            int rowIndex = dgvItemList.CurrentCell.RowIndex;
            string itemCode = dgvItemList.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();

            if(!string.IsNullOrEmpty(itemCode) && rowIndex > -1)
            {
                DataTable dt_MoreInfo = NewItemGeneralInfoTable();
                DataRow newRow;

                int index = 1;

                foreach(DataRow row in DB_ITEM_LIST.Rows)
                {
                    if(row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        int DBRowIndex = DB_ITEM_LIST.Rows.IndexOf(row);

                        foreach (DataColumn col in DB_ITEM_LIST.Columns)
                        {
                            string Data = col.ColumnName;
                            string Description = DB_ITEM_LIST.Rows[DBRowIndex][Data].ToString();

                            newRow = dt_MoreInfo.NewRow();

                            newRow[text.Header_Index] = index++;
                            newRow[text.Header_Data] = Data;
                            newRow[text.Header_DataName] = Data;
                            newRow[text.Header_Description] = Description;

                            dt_MoreInfo.Rows.Add(newRow);

                        }

                        break;
                    }
                  
                }
               

                dgvMoreInfo.DataSource = dt_MoreInfo;

                dgvMoreInfo.ClearSelection();
            }

        }

        private void MoreInfoModeStation()
        {
            string CurrentMode = lblMoreInfo.Text;

            if(CurrentMode.Contains(MODE_GENERAL_INFO))
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

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvItemList.DataSource != null)
            {
                MoreInfoModeStation();
            }
        }
    }
}
