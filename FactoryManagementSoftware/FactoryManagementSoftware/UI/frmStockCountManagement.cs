using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using FactoryManagementSoftware.Properties;
using System.Configuration;
using System.Collections.Generic;

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

        private void LoadStockCountListItem()
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. confirm to load data? ", "Message",
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
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockCountList.SelectedItem;
                int listTblCode = int.TryParse(selectedRow[dalStockCountList.TblCode].ToString(), out int i) ? i : -1;

                DataTable dt = dalStockCountListItem.SelectListItem(listTblCode);

                if (dgvStockCountList?.Rows.Count > 0)
                {
                    // Existing IDs in dgvStockCountList
                    HashSet<string> existingIDs = new HashSet<string>();
                    foreach (DataGridViewRow dgvRow in dgvStockCountList.Rows)
                    {
                        if (dgvRow.Cells[dalStockCountListItem.TblCode]?.Value != null)
                            existingIDs.Add(dgvRow.Cells[dalStockCountListItem.TblCode].Value.ToString());
                    }

                    // Add rows from dt if not found in existingIDs
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        if (!existingIDs.Contains(dtRow[dalStockCountListItem.TblCode].ToString()))
                        {
                            // Convert DataRow to DataGridViewRow or create a new row and add to dgvStockCountList
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dgvStockCountList); // Create cells for the new row

                            for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                            {
                                newRow.Cells[colIndex].Value = dtRow[colIndex];
                            }

                            dgvStockCountList.Rows.Add(newRow);
                        }
                    }
                }
                else
                {
                    dgvStockCountList.DataSource = dt;
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
