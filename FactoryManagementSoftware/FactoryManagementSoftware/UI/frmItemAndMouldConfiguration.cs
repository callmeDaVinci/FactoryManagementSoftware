using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Syncfusion.XlsIO.Implementation.XmlSerialization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemAndMouldConfiguration : Form
    {
        #region Variable Declare
        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        Text text = new Text();
        matPlanDAL dalmatPlan = new matPlanDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        habitDAL dalHabit = new habitDAL();
        habitBLL uHabit = new habitBLL();

        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();

        planningActionDAL dalPlanAction = new planningActionDAL();
        itemForecastDAL dalItemForecast = new itemForecastDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();

        dataTrfBLL uData = new dataTrfBLL();

        DataTable DT_ITEM;
        private readonly string ITEM_CODE;
        bool dataChanged = false;
        bool MouldListEditMode = false;
        #endregion

        public frmItemAndMouldConfiguration()
        {
            InitializeComponent();
            MouldListEditMode = false;

        }

        public frmItemAndMouldConfiguration(string itemCode, int editMode)
        {
            InitializeComponent();

            ITEM_CODE = itemCode;
            DT_ITEM = dalItem.Select();
            MouldListEditMode = false;
            //edit mode
            //0 : add new mould data from null
            //1 : mould data found , want to add new mould data or edit existing mould data
            if (editMode == 0)
            {
                //UI Change : show suggestion List
                ShowSuggestionList(true);
                //Load suggestion List
                LoadSuggestionMouldList(itemCode);
            }
            else
            {
                //UI change : hide suggestion List
                ShowSuggestionList(false);

            }

        }

        #region Data Loading/Checking/Save

        private bool SaveMouldData()
        {
            bool dataSaved = false;

            if(dgvMouldList?.Rows.Count > 0)
            {
                DataTable dt_MouldList = (DataTable)dgvMouldList.DataSource;

                dt_MouldList.DefaultView.Sort = text.Header_MouldCode + " ASC";
                dt_MouldList.DefaultView.ToTable();


            }
            else
            {
                MessageBox.Show("No mould data found!");
            }

            return dataSaved;
        }

        private bool HasSelectedRow(DataGridView dgv)
        {
            return dgv?.SelectedRows.Count > 0;
        }

        private void MoveSuggestMouldToMouldList(string mouldCode)
        {
            bool MoveRightSuccess = false;
                
            if (dgvMouldList?.SelectedRows.Count <= 0)
            {
                foreach (DataRow row in DT_ITEM.Rows)
                {
                    if (ITEM_CODE == row[dalItem.ItemCode].ToString())
                    {
                        DataTable dt_MouldList = NewMouldListTable();

                        DataRow newRow = dt_MouldList.NewRow();

                        string itemName = row[dalItem.ItemName].ToString();
                        string itemDesription = itemName + " (" + ITEM_CODE + ")";

                        newRow[text.Header_MouldCode] = mouldCode;
                        newRow[text.Header_ProTon] = row[dalItem.ItemProTon].ToString();//item_best_ton
                        newRow[text.Header_ItemDescription] = itemDesription;
                        newRow[text.Header_ItemCode] = ITEM_CODE;
                        newRow[text.Header_ItemName] = itemName;
                        newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
                        newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
                        newRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
                        newRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

                        dt_MouldList.Rows.Add(newRow);

                        //add mould all item production mode info

                        newRow = dt_MouldList.NewRow();


                        newRow[text.Header_MouldCode] = mouldCode;
                        newRow[text.Header_ProTon] = row[dalItem.ItemProTon].ToString();
                        newRow[text.Header_ItemDescription] = text.All_Item;
                        newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
                        newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
                        newRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
                        newRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

                        dt_MouldList.Rows.Add(newRow);

                        dgvMouldList.DataSource = dt_MouldList;

                        MoveRightSuccess = true;

                        break;
                    }
                }
            }
            else
            {
                //check if mould existing in mould list
                //MoveRightSuccess = true;

            }


            if (MoveRightSuccess)
            {
                // 1)if family member found in production records, ask user want to auto add in or not
                // 2)UI Change, base on mould code
                dgvUIEdit(dgvMouldList);
                AfterMoveRightUIUpdate(mouldCode);
            }
        }

        private void MoveMouldListToSuggestMould(string mouldCode)
        {

            if (MessageBox.Show("Are you sure you want to move this mould " + mouldCode + " to the suggestion list?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)//MessageBox.Show("Are you sure you want to move this mould "+mouldCode+" to the suggestion list?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes
            {
                dgvSuggestionList.Focus();
                DataTable dt_SuggestionList;

                if (dgvSuggestionList?.RowCount <= 0)
                {
                    dt_SuggestionList = NewSuggestionMouldListTable();

                }
                else
                {
                    dt_SuggestionList = (DataTable)dgvSuggestionList.DataSource;

                }

                DataRow newRow = dt_SuggestionList.NewRow();

                newRow[text.Header_MouldCode] = mouldCode;
                newRow[text.Header_MouldDescription] = tool.getItemNameFromDataTable(DT_ITEM, mouldCode);

                dt_SuggestionList.Rows.Add(newRow);


                dgvSuggestionList.DataSource = dt_SuggestionList;
                dgvUIEdit(dgvSuggestionList);
                AfterMoveLeftUIUpdate(mouldCode);

            }

            
        }
        private void LoadSuggestionMouldList(string itemCode)
        {
            dgvSuggestionList.DataSource = null;

            if(!string.IsNullOrEmpty(itemCode))
            {
                string itemName = tool.getItemName(itemCode).ToUpper().Replace(" ","");

                DataTable dt_SuggestionMouldList = NewSuggestionMouldListTable();

                DataTable DB_Item = dalItem.CatSearch(text.Cat_Mould);

                foreach(DataRow row in DB_Item.Rows)
                {
                    string mouldName = row[dalItem.ItemName].ToString();

                    if(mouldName.ToUpper().Replace(" ", "").Contains(itemName) || mouldName.ToUpper().Replace(" ", "").Contains(itemCode.ToUpper().Replace(" ", "")))
                    {
                        DataRow newRow = dt_SuggestionMouldList.NewRow();

                        newRow[text.Header_MouldCode] = row[dalItem.ItemCode].ToString();
                        newRow[text.Header_MouldDescription] = mouldName;

                        dt_SuggestionMouldList.Rows.Add(newRow);

                    }
                }

                dgvSuggestionList.DataSource = dt_SuggestionMouldList;
                dgvUIEdit(dgvSuggestionList);
                dgvSuggestionList.ClearSelection();
            }
        }

        #endregion

        #region UI/UX

        private void ClearAllListSelectionIfAny()
        {
            if(HasSelectedRow(dgvMouldList))
            {
                dgvMouldList.ClearSelection();

            }

            if (HasSelectedRow(dgvSuggestionList))
            {
                dgvSuggestionList.ClearSelection();

            }
        }

        private void AfterMoveRightUIUpdate(string mouldCode)
        {
            //remove from suggestion list
            DeleteSelectedRows(dgvSuggestionList, mouldCode);

            //get rows selected, also update all item qty
            UpdateMouldListAllItemCountAndSelectRows(dgvMouldList, mouldCode);

            //show save button
            ShowSaveButton(true);
        }

        private void AfterMoveLeftUIUpdate(string mouldCode)
        {
            //remove from suggestion list
            DeleteSelectedRows(dgvMouldList, mouldCode);

            //get rows selected, also update all item qty
            SelectRowsBasedOnMouldCode(dgvSuggestionList, mouldCode);

            ShowSaveButton(dataChanged);

            if(dgvMouldList.Rows.Count <= 0)
            {
                MouldListEditMode = false;
            }
        }

        private void MouldListEditModeChangeColumnColor()
        {
            DataGridView dgv = dgvMouldList;

            if (MouldListEditMode && dgvMouldList?.Rows.Count > 0)
            {

                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.BackColor = SystemColors.Info;
            }
            else
            {
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.BackColor = Color.White ;

            }
        }

        public void DeleteSelectedRows(DataGridView dgv, string mouldCode)
        {
            for (int i = dgv.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dgv.Rows[i];
                if (row.IsNewRow) continue; // Skip the new row at the end of the grid.

                // Replace "YourConditionHere" with the condition you want to use to delete rows.
                if (row.Cells[text.Header_MouldCode].Value.ToString() == mouldCode)
                {
                    dgv.Rows.RemoveAt(i);
                }
            }
        }


        public void SelectRowsBasedOnMouldCode(DataGridView dgv, string mouldCode)
        {
            dgv.ClearSelection(); // Clear any existing selection.

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow row = dgv.Rows[i];
                if (row.IsNewRow) continue; // Skip the new row at the end of the grid.

                // Replace "YourConditionHere" with the condition you want to use to select rows.
                if (row.Cells[text.Header_MouldCode].Value.ToString() == mouldCode)
                {
                    row.Selected = true;
                }
            }
        }

        public void UpdateMouldListAllItemCountAndSelectRows(DataGridView dgv, string mouldCode)
        {
            dgv.ClearSelection(); // Clear any existing selection.
            int AllItemRowIndex = -1;
            int itemCount = 0;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow row = dgv.Rows[i];
                if (row.IsNewRow) continue; // Skip the new row at the end of the grid.

                // Replace "YourConditionHere" with the condition you want to use to select rows.
                if (row.Cells[text.Header_MouldCode].Value.ToString() == mouldCode)
                {
                    string itemDescription = row.Cells[text.Header_ItemDescription].Value.ToString();

                    if(itemDescription.Contains(text.All_Item))
                    {
                        AllItemRowIndex = row.Index;
                    }
                    else
                    {
                        itemCount++;
                    }
                    row.Selected = true;
                }
            }

            if(AllItemRowIndex > -1)
            {
                string AllItemItemDesciption = text.All_Item;

                if(itemCount > 1)
                {
                    AllItemItemDesciption += "S (" + itemCount + ")"; 
                }
                else
                {
                    AllItemItemDesciption += " (" + itemCount + ")";

                }

                dgv.Rows[AllItemRowIndex].Cells[text.Header_ItemDescription].Value = AllItemItemDesciption;
                dgv.Rows[AllItemRowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
                dgv.Rows[AllItemRowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);

            }

        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            if (dgv == dgvSuggestionList)
            {
                //dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ////dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dgv.Columns[headerUpdatedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv.Columns[headerUpdatedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_MouldDescription].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            }
           
            else if(dgv == dgvMouldList)
            {
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 200;


                dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                //int ProductionInfoColumnWidth = 50;

                //dgv.Columns[text.Header_Cavity].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProCT].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProPwShot].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProRwShot].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;


                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
            }


        }

        private DataTable NewMouldListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ProTon, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(string));
            dt.Columns.Add(text.Header_ProPwShot, typeof(string));
            dt.Columns.Add(text.Header_ProRwShot, typeof(string));

            return dt;
        }

        private DataTable NewSuggestionMouldListTable()
        {
            DataTable dt = new DataTable();
          
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_MouldDescription, typeof(string));

            return dt;
        }

        private void ShowSuggestionList(bool show)
        {
            if(show)
            {
                tlpMain.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 35f);
                tlpMain.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 30f);
                tlpMain.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 65f);
            }
            else
            {
                tlpMain.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMain.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMain.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 100f);
            }
            
        }

        private void ShowSaveButton(bool show)
        {
            btnSave.Visible = show;

            if (show)
            {
                tlpButton.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100f);
                tlpButton.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 5f);
                tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 100f);

            }
            else
            {
                tlpButton.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100f);
                tlpButton.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 5f);
                tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
            }

        }

        private void EnableMoveRightButton(bool Enable)
        {
            btnMoveRight.Enabled= Enable;
        }

        private void EnableMoveLeftButton(bool Enable)
        {
            btnMoveLeft.Enabled = Enable;

            

        }

        #endregion
      
        #region Form Action
        private void frmItemAndMouldConfiguration_Load(object sender, EventArgs e)
        {
            if(dgvSuggestionList.DataSource != null)
            {
                dgvSuggestionList.ClearSelection();
            }

            btnMoveLeft.Enabled = false;
            btnMoveRight.Enabled = false;

            ShowSaveButton(false);

        }

        private void dgvSuggestionList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    btnMoveRight.Enabled = true;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(btnSave.Visible)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes that will be lost if you close this page. Are you sure you want to proceed?", "Unsaved Changes Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
           

        }

       
        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if(HasSelectedRow(dgvSuggestionList))
            {
                int rowIndex = dgvSuggestionList.CurrentCell.RowIndex;

                MoveSuggestMouldToMouldList(dgvSuggestionList.Rows[rowIndex].Cells[text.Header_MouldCode].Value.ToString());
            }
           
        }


        private void frmItemAndMouldConfiguration_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();
        }

        private void tableLayoutPanel10_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tableLayoutPanel2_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tableLayoutPanel25_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tlpButton_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void dgvSuggestionList_SelectionChanged(object sender, EventArgs e)
        {
            EnableMoveRightButton(HasSelectedRow(dgvSuggestionList));
        }

        private void dgvMouldList_SelectionChanged(object sender, EventArgs e)
        {
            EnableMoveLeftButton(HasSelectedRow(dgvMouldList));
        }

        private void dgvMouldList_MouseClick(object sender, MouseEventArgs e)
        {
            var hti = dgvMouldList.HitTest(e.X, e.Y);

            if (hti.Type == DataGridViewHitTestType.None || hti.Type == DataGridViewHitTestType.ColumnHeader)
            {
                dgvMouldList.ClearSelection();
            }
        }

        private void dgvSuggestionList_MouseClick(object sender, MouseEventArgs e)
        {
            var hti = dgvSuggestionList.HitTest(e.X, e.Y);

            if (hti.Type == DataGridViewHitTestType.None || hti.Type == DataGridViewHitTestType.ColumnHeader)
            {
                dgvSuggestionList.ClearSelection();
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (HasSelectedRow(dgvMouldList))
            {
                int rowIndex = dgvMouldList.CurrentCell.RowIndex;

                MoveMouldListToSuggestMould(dgvMouldList.Rows[rowIndex].Cells[text.Header_MouldCode].Value.ToString());

                btnMoveLeft.OnHoverBaseColor1 = Color.White;
                btnMoveLeft.OnHoverBaseColor2 = Color.White;
                btnMoveLeft.OnHoverBorderColor = Color.Black;

               

            }
        }

        private void btnMoveLeft_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnMoveLeft_MouseEnter(object sender, EventArgs e)
        {
            if(btnMoveLeft.Enabled)
            {
                btnMoveLeft.OnHoverBaseColor1 = Color.FromArgb(155, 145, 221);
                btnMoveLeft.OnHoverBaseColor2 = Color.FromArgb(255, 85, 255);
                btnMoveLeft.OnHoverBorderColor = Color.Black;
            }
        }

        private void dgvMouldList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMouldList;
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string currentHeader = dgv.Columns[colIndex].Name;

                try
                {
                    if(!MouldListEditMode)
                    {
                        my_menu.Items.Add(text.EditMode_Open).Name = text.EditMode_Open;

                    }
                    else
                    {
                        my_menu.Items.Add(text.EditMode_Close).Name = text.EditMode_Close;

                    }

                    my_menu.Items.Add(text.AddingItem).Name = text.AddingItem;
                    my_menu.Items.Add(text.AddingNewMould).Name = text.AddingNewMould;
                    my_menu.Items.Add(text.AddingExistingMould).Name = text.AddingExistingMould;
                    my_menu.Items.Add(text.AutoSearchFamilyItem).Name = text.AutoSearchFamilyItem;


                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvMouldList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            //string itemCode = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_PartCode].Value.ToString();
            //string jobNo = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();
            //string sheetNo = "";

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.EditMode_Open))
            {
                MouldListEditMode = true;

                MouldListEditModeChangeColumnColor();
            }
            else if (itemClicked.Equals(text.EditMode_Close))
            {
                MouldListEditMode = false;

                MouldListEditModeChangeColumnColor();
            }
            else if (itemClicked.Equals(text.AddingItem))
            {
                //frmJobSheetViewMode frm = new frmJobSheetViewMode(jobNo, sheetNo, itemCode);

                //frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.WindowState = FormWindowState.Normal;
                //frm.Size = new Size(1650, 900);
                //frm.ShowDialog();

                //frmLoading.CloseForm();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        #endregion
    }
}
