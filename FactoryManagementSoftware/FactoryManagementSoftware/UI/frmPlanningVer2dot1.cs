using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.ReportingServices.Interfaces;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using Syncfusion.XlsIO.Parser.Biff_Records;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanningVer2dot1 : Form
    {
        #region Variable Declare
        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();
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

        private DataTable DT_ITEM;
        private DataTable DT_MOULD_ITEM;

        private DataTable DT_ITEM_LIST;

        bool CMB_CODE_READY = false;
        bool PRO_INFO_LOADING = false;

        readonly private string BTN_INFO_SAVE_CONFIRM_MOULD = "INFO SAVE & MOULD CONFIRM";
        readonly private string BTN_CONFIRM_MOULD = "MOULD CONFIRM";

        private bool PRO_INFO_CHANGE = false;
        #endregion

        #region LastInventoryZeroDate

        public new string Name { get; set; }
       

        public static DateTime CalculateZeroInventoryDate(DataTable dt, string customerName)
        {
            var customerRows = dt.Select($"Customer = '{customerName}'")
         .OrderBy(row => row["ReceiveDate"]).ToList();

            DateTime zeroInventoryDate = (DateTime)customerRows[0]["ReceiveDate"];
            int remainingInventory = (int)customerRows[0]["Quantity"];

            for (int i = 1; i < customerRows.Count; i++)
            {
                DateTime nextReceiveDate = (DateTime)customerRows[i]["ReceiveDate"];
                int daysDifference = (nextReceiveDate - zeroInventoryDate).Days;

                if (remainingInventory - daysDifference <= 0)
                {
                    zeroInventoryDate = nextReceiveDate;
                    remainingInventory = (int)customerRows[i]["Quantity"];
                }
                else
                {
                    remainingInventory = remainingInventory - daysDifference + (int)customerRows[i]["Quantity"];
                    zeroInventoryDate = zeroInventoryDate.AddDays(daysDifference);
                }
            }

            zeroInventoryDate = zeroInventoryDate.AddDays(remainingInventory - 1);

            return zeroInventoryDate;
        }


        private void Testing()
        {
            DataTable dt = new DataTable("PurchaseData");

            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("ReceiveDate", typeof(DateTime));

            dt.Rows.Add("Customer1", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer1", 10, new DateTime(2023, 5, 5));
            dt.Rows.Add("Customer1", 10, new DateTime(2023, 6, 1));

            dt.Rows.Add("Customer2", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer2", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer2", 10, new DateTime(2023, 6, 1));

            dt.Rows.Add("Customer3", 10, new DateTime(2023, 5, 1));

            dt.Rows.Add("Customer4", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer4", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer4", 10, new DateTime(2023, 5, 15));


            string toFollowUpDate = CalculateZeroInventoryDate(dt, "Customer1").ToString("dd/MMM/yyyy");

            string toFollowUpDate2 = CalculateZeroInventoryDate(dt, "Customer2").ToString("dd/MMM/yyyy");
            string toFollowUpDate3 = CalculateZeroInventoryDate(dt, "Customer3").ToString("dd/MMM/yyyy");
            string toFollowUpDate4 = CalculateZeroInventoryDate(dt, "Customer4").ToString("dd/MMM/yyyy");


        }
        #endregion


        public frmPlanningVer2dot1()
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
        }

        private void frmPlanning_Load(object sender, EventArgs e)
        {
            
        }

        private DataTable NewItemList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ProTon, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(string));
            dt.Columns.Add(text.Header_ProPwShot, typeof(string));
            dt.Columns.Add(text.Header_ProRwShot, typeof(string));
            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_CavityMatchedQty, typeof(int));

            return dt;
        }

        #region UI/UX

        private void InitialSetting()
        {
            //BackToPartInfo();
        }

        private void btnSaveProductionInfoAndMouldConfirmMode(bool active)
        {

        }

        private void NextToMachineSelection()
        {
            btnNextToMachineSelection.Visible = false;

            tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
            tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);
        }

        private void BackToPartInfo()
        {
            //btnNextToMachineSelection.Visible = true;

            //tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            //tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.Columns[text.Header_TargetQty].HeaderCell.Style.BackColor = Color.FromArgb(255, 153, 153);

            if (dgv == dgvItemList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;


                dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                //dgv.Columns[text.Header_MouldSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ItemSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_MouldCode].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                //dgv.Columns[text.Header_Cavity].Visible = false;
                dgv.Columns[text.Header_ProTon].Visible = false;
                dgv.Columns[text.Header_ProCT].Visible = false;
                //dgv.Columns[text.Header_ProPwShot].Visible = false;
                dgv.Columns[text.Header_ProRwShot].Visible = false;


                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


        }

        #endregion

        #region Data Loading

        private void IndexReset(DataGridView dgv)
        {
            if(dgv?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgv.DataSource;

                if(dt.Columns.Contains(text.Header_Index))
                {
                    int index = 1;
                    foreach(DataRow row in dt.Rows)
                    {
                        row[text.Header_Index] = index++;
                    }
                }
                else
                {
                    MessageBox.Show("Index column not found , cannot reset index.");
                }
            }
        }

        private void AddItemToList(string itemCode)
        {
            if (!string.IsNullOrEmpty(itemCode))
            {
                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }


                int index = 1;

                if (dgvItemList?.Rows.Count > 0)
                {
                    DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                    //get last index
                    index = int.TryParse(dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_Index].Value.ToString(), out index)? index++ : 1 ;
                }
                else
                {
                    DT_ITEM_LIST = NewItemList();
                }

                
                bool itemFound = false;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    if (itemCode == row[text.Header_ItemCode].ToString())
                    {
                        itemFound = true;
                        int rowIndex = DT_ITEM_LIST.Rows.IndexOf(row);

                        MessageBox.Show("Same item code found in the list (row: " + rowIndex + ").");
                        return;
                    }
                }

                if (!itemFound)
                    foreach (DataRow row in DT_ITEM.Rows)
                    {
                        if (itemCode == row[dalItem.ItemCode].ToString())
                        {
                            DT_ITEM_LIST.Rows.Add(CreateNewItemRow(row, index++));

                            itemFound = true;

                            break;
                        }
                    }

                if(itemFound)
                {
                    dgvItemList.DataSource = DT_ITEM_LIST;
                    IndexReset(dgvItemList);
                    dgvUIEdit(dgvItemList);
                    ItemListCellFormatting(dgvItemList);

                    LoadSummary_ProductionInfo();
                }
                else
                {
                    DT_ITEM = dalItem.Select();
                    AddItemToList(itemCode);
                }
            }
        }

        private void RemoveItemFromList(DataGridView dgv ,int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count) // Ensure the index is within the valid range
            {
                dgv.Rows.RemoveAt(rowIndex);
                LoadSummary_ProductionInfo();
                IndexReset(dgvItemList);

            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "The row index provided is out of range.");
            }
        }
        private DataRow CreateNewItemRow(DataRow row, int index)
        {
            DataRow newRow = DT_ITEM_LIST.NewRow();

            string itemCode = row[dalItem.ItemCodePresent].ToString();
            itemCode = string.IsNullOrEmpty(itemCode) ? row[dalItem.ItemCode].ToString() : itemCode;

            string itemName = row[dalItem.ItemName].ToString();

            string itemDescription = itemName + "\n(" + itemCode + ")";

            newRow[text.Header_Index] = index;
            newRow[text.Header_ItemDescription] = itemDescription;
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_ItemName] = itemName;
            newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
            newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
            newRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
            newRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

            return newRow;
        }

        private void LoadSummary_ProductionInfo()
        {
            ClearProductionInfo();

            if(dgvItemList?.Rows.Count > 0)
            {
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                int totalCavity = 0;
                int totalShot = 0;
                decimal cycleTime = 0;

                decimal partWeightPerShot = 0;
                decimal runnerWeightPerShot = 0;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    totalCavity += int.TryParse(row[text.Header_Cavity].ToString(), out int cavity) ? cavity : 0;
                    partWeightPerShot += decimal.TryParse(row[text.Header_ProPwShot].ToString(), out decimal pwPerShot) ? pwPerShot : 0;

                    decimal rwPerShot = decimal.TryParse(row[text.Header_ProRwShot].ToString(), out rwPerShot) ? rwPerShot : 0;

                    if (rwPerShot > runnerWeightPerShot)
                    {
                        runnerWeightPerShot = rwPerShot;
                    }


                    decimal ct = decimal.TryParse(row[text.Header_ProCT].ToString(), out ct)? ct : 0;

                    if(ct > cycleTime)
                    {
                        cycleTime = ct;
                    }

                    int targetQty = int.TryParse(row[text.Header_CavityMatchedQty].ToString(), out targetQty) ? targetQty : 0;

                    if(cavity > 0)
                    {
                        int shotQty = targetQty / cavity;

                        if(shotQty > totalShot)
                        {
                            totalShot = shotQty;
                        }
                    }
                }

                txtCavity.Text = totalCavity.ToString();
                txtCycleTime.Text = cycleTime.ToString("0");
                txtPWPerShot.Text = partWeightPerShot.ToString("0.##");
                txtRWPerShot.Text = runnerWeightPerShot.ToString("0.##");
                txtTotalShot.Text = totalShot.ToString();

            }

        }


        private void LoadProductionInfoToField(DataRow[] existingRows)
        {
            txtCavity.Text = existingRows[0][dalItem.MouldCavity].ToString();
            txtCycleTime.Text = existingRows[0][dalItem.MouldCT].ToString();
            txtPWPerShot.Text = existingRows[0][dalItem.ItemPWShot].ToString();
            txtRWPerShot.Text = existingRows[0][dalItem.ItemRWShot].ToString();

            btnItemConfirm.Visible = true;

        }

        private void ClearProductionInfo()
        {
            txtCavity.Text = "";
            txtCycleTime.Text = "";
            txtPWPerShot.Text = "";
            txtRWPerShot.Text = "";
            txtTotalShot.Text = "";
        }

        private void LoadDB()
        {
            DT_ITEM = dalItem.Select();
            DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
        }

        #endregion

        private void btnNextToMachineSelection_Click(object sender, EventArgs e)
        {
            NextToMachineSelection();
        }

        private void AddItem()
        {
            DataTable dt = dalItem.CatSearch(text.Cat_Part);

            List<string> itemsToRemove = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                string itemName = row[dalItem.ItemName].ToString();

                if (itemName.ToUpper().Contains(text.Terminated.ToUpper()))
                {
                    string itemCode = row[dalItem.ItemCode].ToString();

                    // Add to list of items to remove
                    itemsToRemove.Add(itemCode);
                }
            }

            if(dgvItemList?.Rows.Count > 0)
            {
                DataTable dt_itemList = (DataTable)dgvItemList.DataSource;

                foreach(DataRow row in dt_itemList.Rows)
                {
                    string itemCode = row[text.Header_ItemCode].ToString();
                    itemsToRemove.Add(itemCode);
                }
            }

            if (itemsToRemove.Count > 0)
            {
                // Only call AcceptChanges once, improving performance
                dt.AcceptChanges();

                // Use LINQ to efficiently filter out the rows
                var rows = dt.AsEnumerable()
                    .Where(r => !itemsToRemove.Contains(r.Field<string>(dalItem.ItemCode)));

                // Convert filtered enumerable back to DataTable
                dt = rows.CopyToDataTable();
            }

            frmItemSearch frm = new frmItemSearch(dt, text.Cat_Part);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();

            frmLoading.CloseForm();

            if (frmItemSearch.ITEM_CODE_SELECTED != "")
            {
                AddItemToList(frmItemSearch.ITEM_CODE_SELECTED);
            }
        }
        private void frmPlanningNEWV2_Shown(object sender, EventArgs e)
        {
            //load item search page
            AddItem();
        }

        private void ItemListCellFormatting(DataGridView dgv)
        {
            if(dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    string itemDescription = row[text.Header_ItemDescription].ToString();

                   
                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Cavity].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_ProPwShot].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_TargetQty].Style.BackColor = SystemColors.Info;

                }

                dgv.ResumeLayout();
            }
          
        }

        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //DataGridView dgv = dgvItemList;

            //if (dgv.Columns[colIndex].Name == text.Header_ItemDescription && dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //{
            //    string itemDescription = dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString();

            //    if(string.IsNullOrEmpty(itemDescription))
            //    {
            //        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Black;
            //        //dgv.Rows[rowIndex].Height = 20;
            //    }
            //    else
            //    {
            //        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            //        //dgv.Rows[rowIndex].Height = 50;

            //        dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            //        dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);
            //    }
               
            //}
           
           
        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            btnItemConfirm.Visible = dgvItemList?.Rows.Count > 0;
            //foreach (DataGridViewRow row in dgvItemList.Rows)
            //{
            //    //DataGridViewCheckBoxCell checkBoxCell = row.Cells[text.Header_MouldSelection] as DataGridViewCheckBoxCell;
            //    //if (row.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //    //{
            //    //    checkBoxCell.ReadOnly = false;  // allow checkbox to be checked/unchecked
            //    //}
            //    //else
            //    //{
            //    //    checkBoxCell.ReadOnly = true;  // disable checkbox
            //    //    //checkBoxCell.Style.BackColor = Color.DimGray;  // indicate disabled state
            //    //}
            //}
        }

        private void dgvItemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        //    if (e.ColumnIndex == dgvItemList.Columns[text.Header_MouldSelection].Index &&
        //e.RowIndex >= 0 &&
        //dgvItemList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item) == false)
        //    {
        //        e.PaintBackground(e.CellBounds, true);
        //        e.Handled = true;
        //    }

        //    if (e.ColumnIndex == dgvItemList.Columns[text.Header_ItemSelection].Index &&
        //e.RowIndex >= 0 &&
        //string.IsNullOrEmpty(dgvItemList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString()))
        //    {
        //        e.PaintBackground(e.CellBounds, true);
        //        e.Handled = true;
        //    }
        }

        private void dgvReadOnlyModeUpdate(DataGridView dgv, int rowIndex, int colIndex)
        {
            listReadOnlyModeChanging = true;

            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count && colIndex >= 0 && colIndex < dgv.Columns.Count)
            {
                string colName = dgv.Columns[colIndex].Name;

                bool readOnlyMode = true;

                if (colName.Equals(text.Header_TargetQty) || colName.Equals(text.Header_Cavity) || colName.Equals(text.Header_ProPwShot))
                {
                    readOnlyMode = false;
                }

                if (readOnlyMode)
                {
                    dgv.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView
                    // Change back to full row selection mode for any other column.
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                    dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                }
                else
                {
                    // Change to cell selection mode.
                    dgv.ReadOnly = false; // Temporarily allow user interaction

                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                    dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                }
            }

            listReadOnlyModeChanging = false;

        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!listReadOnlyModeChanging)
                dgvReadOnlyModeUpdate(dgvItemList,e.RowIndex, e.ColumnIndex);
        }
        private bool userInitiatedChange = true;

        private void refreshCavityMatchedQty()
        {
            if(dgvItemList?.Rows.Count > 0)
            {
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                decimal totalShot = 0;

                foreach(DataRow row in DT_ITEM_LIST.Rows)
                {
                    decimal targetQty = decimal.TryParse(row[text.Header_TargetQty].ToString(), out targetQty) ? targetQty : 0;
                    decimal cavity = decimal.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                    decimal cavityMatchedQty = 0;

                    if(cavity > 0)
                    {
                        cavityMatchedQty = Math.Ceiling(targetQty / cavity);

                        if(cavityMatchedQty > totalShot)
                        {
                            totalShot = cavityMatchedQty;
                        }
                        cavityMatchedQty *= cavity;
                    }


                    row[text.Header_CavityMatchedQty] = (int) cavityMatchedQty;
                }

                txtTotalShot.Text = totalShot.ToString("0");
            }
        }

        private void dgvItemList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PRO_INFO_LOADING = true;


            // Ignore changes in any column other than 'MouldSelection' and 'ItemSelection'
            if (e.ColumnIndex != dgvItemList.Columns[text.Header_TargetQty].Index &&
                e.ColumnIndex != dgvItemList.Columns[text.Header_Cavity].Index &&
                e.ColumnIndex != dgvItemList.Columns[text.Header_ProPwShot].Index)
                return;

            // Ignore any changes that occur programmatically to avoid infinite loops
            if (!userInitiatedChange) return;

            try
            {
                // Temporarily disable changes from triggering this event
                userInitiatedChange = false;

                DataGridViewRow currentRow = dgvItemList.Rows[e.RowIndex];


                if (e.ColumnIndex == dgvItemList.Columns[text.Header_TargetQty].Index)
                {
                    refreshCavityMatchedQty();
                }
                else if (e.ColumnIndex == dgvItemList.Columns[text.Header_Cavity].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_TargetQty].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_ProPwShot].Index)
                {
                    LoadSummary_ProductionInfo();
                }
              
              
            }
            finally
            {
                // Allow changes to trigger this event again
                userInitiatedChange = true;
            }

            PRO_INFO_LOADING = false;

        }

        private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void CannotEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void FloatOnly_KeyPress(object sender, KeyPressEventArgs e)
        {

            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (tb != null)
            {
                // If it's not a number, not a control key (like backspace), and it's not the first decimal point
                if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || tb.Text.Contains('.')))
                {
                    e.Handled = true;
                }
            }

            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgvMouldList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //// If the clicked column is "Mould Selection" or "Item Selection".
            //if (e.ColumnIndex == dgvItemList.Columns[text.Header_ItemSelection].Index)
            //{
            //    // Commit the edit immediately for the checkbox cell.
            //    dgvItemList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private void dgvMouldList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvItemList;

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
                    my_menu.Items.Add(text.RemoveItem).Name = text.RemoveItem;

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
            DataGridView dgv = dgvItemList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
         
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.RemoveItem))
            {
                RemoveItemFromList(dgv, rowIndex);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void tableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void tableLayoutPanel12_MouseClick(object sender, MouseEventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private void tableLayoutPanel26_MouseClick(object sender, MouseEventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private void ProductionInfo_TextChanged(object sender, EventArgs e)
        {
            if(!PRO_INFO_LOADING)
            {
                btnSaveProductionInfoAndMouldConfirmMode(true);
            }
        }

        private bool UpsertUniqueMouldItemData(itemBLL u)
        {
            bool isSuccess = false;

            // Search for a matching row in DT_MOULD_LIST
            DataRow[] existingRows = DT_MOULD_ITEM.Select($"mould_code = '{u.mould_code}' AND item_code = '{u.item_code}' AND combination_code = '{u.combination_code}'");

            if (existingRows.Length > 0)
            {
                // If a match was found, call the update function
                u.tbl_code = (int)existingRows[0]["tbl_code"];
                isSuccess = updateMouldItem(u);
            }
            else
            {
                // If no match was found, call the insert function
                u.combination_code = 0; // Set combination_code as 0 for unique item info
                isSuccess = insertMouldItem(u);
            }

            return isSuccess;
        }

        private bool InsertNewCombinationGroup(itemBLL u, List<Tuple<string, string>> selectedItems)
        {
            foreach (var item in selectedItems)
            {
                // Item2 of the tuple is itemCode
                u.item_code = item.Item2;

                // Call the method to insert the item
                if (!insertMouldItem(u))
                {
                    MessageBox.Show($"Failed to insert item with item code: {u.item_code}.", "Insertion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }



        private bool updateMouldItem(itemBLL u)
        {
            // Your update logic here
            return dalItem.MouldItemUpdate(u);
        }

        private bool insertMouldItem(itemBLL u)
        {
            // Your insert logic here
            return dalItem.MouldItemInsert(u);

        }

        private void SaveProductionInfo()
        {
            //DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();

            //DataTable dt_MouldList = (DataTable) dgvItemList.DataSource;

            //List<Tuple<string, string>> selectedItems = new List<Tuple<string, string>>();

            //foreach (DataRow row in dt_MouldList.Rows)
            //{
            //    bool itemSelected = bool.TryParse(row[text.Header_ItemSelection].ToString(), out bool parsedSelection) ? parsedSelection : false;

            //    if (itemSelected)
            //    {
            //        string itemCode = row.Field<string>(text.Header_ItemCode);
            //        string itemDescription = row[text.Header_ItemDescription].ToString();
            //        string mouldCode = row[text.Header_MouldCode].ToString();

            //        uItem.mould_code = mouldCode;
            //        uItem.item_code = itemCode;

            //        if (!itemDescription.Contains(text.All_Item))
            //            selectedItems.Add(new Tuple<string, string>(mouldCode, itemCode));
            //    }
            //}

            //if (selectedItems.Select(x => x.Item1).Distinct().Count() > 1 && selectedItems.Count > 1)
            //{
            //    throw new Exception("All selected items must have the same mould code");
            //}

            ////update unique production info data
            //uItem.updated_date = DateTime.Now;
            //uItem.updated_by = MainDashboard.USER_ID;
            //uItem.item_updtd_date = uItem.updated_date;
            //uItem.item_updtd_by = uItem.updated_by;
            //uItem.item_added_date = uItem.updated_date;
            //uItem.item_added_by = uItem.updated_by;
            //uItem.removed = false;

            //uItem.mould_cavity = int.TryParse(txtCavity.Text, out int y) ? y : 0;
            //uItem.mould_ct = int.TryParse(txtCycleTime.Text, out y) ? y : 0;
            //uItem.item_pw_shot = float.TryParse(txtPWPerShot.Text, out float k) ? k : 0;
            //uItem.item_rw_shot = float.TryParse(txtRWPerShot.Text, out k) ? k : 0;

            //uItem.MouldDefaultSelection = true;
            //uItem.combination_code = 0;

            //if (selectedItems.Count == 1)
            //{
            //    UpsertUniqueMouldItemData(uItem);

            //}
            //else if (selectedItems.Count > 1)
            //{
            //    bool foundCombination = false;

            //    foreach (var combination in DT_MOULD_ITEM.AsEnumerable()
            //                                                        .Where(r => r.Field<string>(dalItem.MouldCode) == uItem.mould_code && r.Field<int>(dalItem.CombinationCode) != 0)
            //                                                        .GroupBy(r => r.Field<int>(dalItem.CombinationCode)))
            //    {
            //        var itemsInCombination = combination.Select(r => r.Field<string>(dalItem.ItemCode)).ToArray();
            //        var selectedItemCodes = selectedItems.Select(i => i.Item2).ToArray();

            //        if (!selectedItemCodes.Except(itemsInCombination).Any() && !itemsInCombination.Except(selectedItemCodes).Any())
            //        {
            //            uItem.combination_code = combination.Key;
            //            foundCombination = true;
            //            dalItem.MouldItemProInfoUpdateByCombinationCode(uItem);
            //            break;
            //        }
            //    }

            //    if (!foundCombination)
            //    {
            //        // If we haven't found a combination, generate a new combination code
            //        var maxCombinationCode = DT_MOULD_ITEM.AsEnumerable()
            //                                              .Where(r => r.Field<string>("mould_code") == uItem.mould_code)
            //                                              .Max(r => r.Field<int>("combination_code"));

            //        uItem.combination_code = maxCombinationCode + 1;

            //        // Call insert method here
            //        InsertNewCombinationGroup(uItem, selectedItems);
            //    }

            //}
            //else
            //{
            //    ClearProductionInfo();

            //}

            //DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
        }

        private void btnItemConfirmed_Click(object sender, EventArgs e)
        {
            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;

            if(totalShot <= 0)
            {
                MessageBox.Show("Total shot cannot be 0, please change the Target Qty in the Item List.");
            }
            else
            {

            }

            //if(btnItemConfirm.Text == BTN_INFO_SAVE_CONFIRM_MOULD)
            //{
            //    //SAVE PRODUCTION INFO
            //    //SEARCH IF SELECTED ITEMS AND COMBINATION CODE EXIST
            //    //IF NOT, CREATE A NEW COMBINATION CODE
            //    SaveProductionInfo();

            //}

            //btnSaveProductionInfoAndMouldConfirmMode(false);
            //btnItemConfirm.Visible = false;

            ////MOULD Selected
            ////step 2: monthly balance estimation, production history (included machine no)
            //MessageBox.Show("Step 2");

            ////step 3: process to production requirement steps
            ////raw material
            ////color material
            ////
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AddItem();

        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            AddItem();
        }

        bool listReadOnlyModeChanging = false;

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;

                if (dgvItemList.SelectedCells.Count > 0)  // checking if any cell is selected
                {
                    int rowIndex = dgvItemList.SelectedCells[0].RowIndex; // Get the row index of first selected cell
                    int columnIndex = dgvItemList.SelectedCells[0].ColumnIndex; // Get the column index of first selected cell

                    dgvReadOnlyModeUpdate(dgvItemList, rowIndex, columnIndex);
                }

                listReadOnlyModeChanging = false;
            }
        }
    }
}
