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
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections.Generic;

using Range = Microsoft.Office.Interop.Excel.Range;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;

using System.Configuration;
using Label = System.Windows.Forms.Label;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOEMStockCount : Form
    {
        public frmOEMStockCount()
        {
            InitializeComponent();

            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            tool.DoubleBuffered(dgvOEMItemStockCountList, true);

            if (cbMainCustomerOnly.Checked)
            {
                tool.loadMainCustomerAndAllToComboBox(cmbCustomer);
            }
            else
            {
                tool.loadCustomerAndALLWithoutOtherToComboBox(cmbCustomer);
            }

            if (myconnstrng == text.DB_Semenyih)
            {
                //cmbCustomer.Text = "SP OUG";
            }

            cmbCustomer.SelectedIndex = 0;
            InitializeFilterData();
        }

        #region variable declare

        static public string myconnstrng;

        enum ColorSet
        {
            Gold = 0,
            //Cyan,
            //Aquamarine,
            //Orange,
            //Yellow,
            ////MediumSpringGreen,
            //Lavender,
            //LightGray,
            //LightBlue
            //Bisque,
            //Chartreuse,
            //CornflowerBlue,
            //MediumTurquoise,
            //MediumAquamarine,
            //LightSalmon,
            //DeepSkyBlue,
            //SkyBlue,
            //Tan,
            //Thistle,
        }

        private List<int> Row_Index_Found;

        int CURRENT_ROW_JUMP = -1;
        private string CELL_EDITING_OLD_VALUE = "";
        private string CELL_EDITING_NEW_VALUE = "";

        private bool CELL_VALUE_CHANGED = false;

        private string textRowFound = " row(s) found";

        private string textSearchFilter = "SEARCH FILTER";
        private string textHideFilter = "HIDE FILTER";


        readonly string headerParentColor = "SPECIAL TYPE";
        readonly string headerBackColor = "BACK COLOR";
        readonly string headerForecastType = "FORECAST TYPE";
        readonly string headerBalType = "BAL TYPE";
        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerRowReference = "Repeated Row";

        readonly string headerItemType = "Item Type";
        readonly string headerPartName = "Description";
        readonly string headerPartCode = "Code";
        
        readonly string headerReadyStock = "Ready Stock";

        readonly string headerStdPackingString = "Std. Packing";
        readonly string headerStdPacking_Type = "Std. Packing Type";
        readonly string headerStdPacking_Qty = "Std. Packing Qty";

        readonly string headerFullPackingQty = "Full Packing Qty";
        readonly string headerBalance = "Balance (pcs)";
        readonly string headerTotalAcutalStock = "Total Actual Stock";
        readonly string headerStockDiff = "Stock Diff.";
        readonly string headerRemark = "Remark";


        readonly Color AssemblyColor = Color.Blue;
        readonly Color InsertMouldingColor = Color.Green;
        readonly Color ProductionAndAssemblyColor = Color.Purple;
        readonly Color InspectionColor = Color.Peru;

        readonly string AssemblyMarking = "Assembly Set";
        readonly string InspectionMarking = "Inspection";
        readonly string InsertMoldingMarking = "Insert Molding";
        readonly string AssemblyAfterProductionMarking = "Assembly After Pro.";

        readonly string AssemblyMarking_Color = "Blue";
        readonly string InspectionMarking_Color = "Peru";
        readonly string InjectionMarking_Color = "Green";
        readonly string AssemblyAfterProductionMarking_Color = "Purple";

        readonly string typeSingle = "SINGLE";
        readonly string typeParent= "PARENT";
        readonly string typeChild = "CHILD";

        readonly string forecastType_Forecast = "FORECAST";
        readonly string forecastType_Needed = "NEEDED";

        readonly string ToDoType_ToAssembly = "(2) TO ASSEMBLY";
        readonly string ToDoType_ToProduce = "(1) TO PRODUCE";
        readonly string ToDoType_ToOrder = "(3) TO ORDER";


        readonly string balType_Total = "TOTAL";
        readonly string balType_Repeated = "REPEATED";
        readonly string balType_Unique = "UNIQUE";

        readonly string sortBy_Stock = "Stock (Finished Goods)";
        readonly string sortBy_Estimate = "Estimate";
        string sortBy_Forecast1 = "Forecast/Needed";
        string sortBy_Forecast2 = "Forecast/Needed";
        string sortBy_Forecast3 = "Forecast/Needed";

        readonly string sortBy_Out = "Delivered Out";
        readonly string sortBy_OutStd = "Out Standing";
        string sortBy_Bal1 = "Balance";
        string sortBy_Bal2 = "Balance";

        readonly string ReportType_Full = "FORECAST REPORT";
        readonly string ReportType_Summary = "SUMMARY REPORT";

        private bool loaded = false;
        private bool custChanging = false;

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();
        planningDAL dalPlanning = new planningDAL();
        dataTrfBLL uData = new dataTrfBLL();
        habitDAL dalHabit = new habitDAL();
        habitBLL uHabit = new habitBLL();
        facStockDAL dalStock = new facStockDAL();
        Tool tool = new Tool();

        Text text = new Text();

        DataTable DT_JOIN = new DataTable();
        DataTable DT_ITEM = new DataTable();
        DataTable dtMasterData = new DataTable();
        DataTable dt_SummaryList = new DataTable();
        DataTable DT_MACHINE_SCHEDULE = new DataTable();
        DataTable DT_MACHINE_SCHEDULE_COPY = new DataTable();
        DataTable DT_PMMA_DATE = new DataTable();
        DataTable DT_ITEM_CUST = new DataTable();

        DataTable DT_FORECAST_REPORT = new DataTable();

        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
        DataTable dt_ProRecord = new DataTable();

        DateTime PMMA_START_DATE = DateTime.MaxValue;
        DateTime PMMA_END_DATE = DateTime.MaxValue;
        #endregion

        #region UI Design
        private DataTable NewForecastReportTable()
        {
           
            DataTable dt = new DataTable();

            dt.Columns.Add(headerBackColor, typeof(string));
            dt.Columns.Add(headerBalType, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerRowReference, typeof(string));
            dt.Columns.Add(headerItemType, typeof(string));
            dt.Columns.Add(headerIndex, typeof(float));

            if(cmbCustomer.Text.Equals(text.Cmb_All))
            {
                dt.Columns.Add(text.Header_Customer, typeof(string));
            }

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));

            dt.Columns.Add(headerReadyStock, typeof(decimal));
            dt.Columns.Add(headerStdPackingString, typeof(string));
            dt.Columns.Add(headerStdPacking_Type, typeof(string));
            dt.Columns.Add(headerStdPacking_Qty, typeof(int));
            dt.Columns.Add(headerFullPackingQty, typeof(int));
            dt.Columns.Add(headerBalance, typeof(decimal));
            dt.Columns.Add(headerTotalAcutalStock, typeof(decimal));
            dt.Columns.Add(headerStockDiff, typeof(decimal));

            dt.Columns.Add(headerRemark, typeof(string));

            return dt;
        }

        private void DgvForecastReportUIEdit(DataGridView dgv)
        {
            if (dgv != null)
            {
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (dgv.Columns.Contains(text.Header_Customer))
                {
                    dgv.Columns[text.Header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[text.Header_Customer].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                }

                dgv.Columns[headerPartCode].Frozen = true;

                // Configure stock check mode (always on)
                dgv.ReadOnly = false;
                dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

                // Show stock check columns
                dgv.Columns[headerStdPacking_Type].Visible = false;  // Keep hidden
                dgv.Columns[headerStdPackingString].Visible = false;  // Keep hidden
                dgv.Columns[headerStdPacking_Qty].Visible = true;
                dgv.Columns[headerFullPackingQty].Visible = true;
                dgv.Columns[headerBalance].Visible = true;
                dgv.Columns[headerTotalAcutalStock].Visible = true;
                dgv.Columns[headerStockDiff].Visible = true;
                dgv.Columns[headerRemark].Visible = true;

                // Make only specific columns editable
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Name == headerStdPacking_Qty ||
                        col.Name == headerFullPackingQty ||
                        col.Name == headerBalance ||
                        col.Name == headerRemark)
                    {
                        col.ReadOnly = false;
                    }
                    else
                    {
                        col.ReadOnly = true;
                    }
                }

                // Color coding
                dgv.Columns[headerReadyStock].HeaderCell.Style.BackColor = Color.Pink;
                dgv.Columns[headerReadyStock].DefaultCellStyle.BackColor = Color.Pink;

                // Stock check column styling
                if (dgv.Columns.Contains(headerTotalAcutalStock))
                {
                    dgv.Columns[headerTotalAcutalStock].HeaderCell.Style.BackColor = Color.LightBlue;
                    dgv.Columns[headerTotalAcutalStock].DefaultCellStyle.BackColor = Color.LightBlue;
                    dgv.Columns[headerTotalAcutalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgv.Columns.Contains(headerStockDiff))
                {
                    dgv.Columns[headerStockDiff].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgv.Columns.Contains(headerStdPacking_Qty))
                {
                    dgv.Columns[headerStdPacking_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[headerStdPacking_Qty].HeaderCell.Style.BackColor = Color.LightYellow;
                }

                if (dgv.Columns.Contains(headerFullPackingQty))
                {
                    dgv.Columns[headerFullPackingQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[headerFullPackingQty].HeaderCell.Style.BackColor = Color.LightYellow;
                }

                if (dgv.Columns.Contains(headerBalance))
                {
                    dgv.Columns[headerBalance].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[headerBalance].HeaderCell.Style.BackColor = Color.LightYellow;
                }

                

                dgv.Columns[headerRowReference].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[headerPartCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[headerPartName].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


                // Add bold border effect for Full Packing Qty
                dgv.Columns[headerFullPackingQty].DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                dgv.Columns[headerBalance].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);


                // Add alternating row colors for better readability
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                dgv.DefaultCellStyle.BackColor = Color.White;

                // Center align the numeric editable columns
                dgv.Columns[headerStdPacking_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerFullPackingQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[headerBalance].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.EnableHeadersVisualStyles = false;
            }
        }

        private void ColorData()
        {
            DataTable dt = (DataTable)dgvOEMItemStockCountList.DataSource;
            DataGridView dgv = dgvOEMItemStockCountList;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            Font _ParentFont = new Font(dgvOEMItemStockCountList.Font, FontStyle.Underline);

            //Font _BalFont = new Font(dgvOEMItemStockCountList.Font, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold);
            //Font _NeededFont = new Font(dgvOEMItemStockCountList.Font, FontStyle.Italic);

            if(dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string parentColor = dt.Rows[i][headerParentColor].ToString();
                    string backColor = dt.Rows[i][headerBackColor].ToString();
                    string balType = dt.Rows[i][headerBalType].ToString();

                    //change parent color
                    if(cbSpecialTypeColorMode.Checked)
                    {
                        if (parentColor.Equals(AssemblyMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = AssemblyColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                        else if (parentColor.Equals(InsertMoldingMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InsertMouldingColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                        else if (parentColor.Equals(AssemblyAfterProductionMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = ProductionAndAssemblyColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                        else if (parentColor.Equals(InspectionMarking))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InspectionColor;
                            dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;
                        }
                    }
                 

                    if (!string.IsNullOrEmpty(backColor) && cbRepeatedColorMode.Checked)
                    {
                        if (balType.Equals(balType_Total))
                        {
                            dgv.Rows[i].Cells[headerPartName].Style.BackColor = Color.FromName(backColor);
                            dgv.Rows[i].Cells[headerPartCode].Style.BackColor = Color.FromName(backColor);
                          
                        }
                        else
                        {
                            Color repeatedRow = Color.LightGray;

                            dgv.Rows[i].Cells[headerPartName].Style.BackColor = repeatedRow;
                            dgv.Rows[i].Cells[headerPartCode].Style.BackColor = repeatedRow;

                        }
                    }
                }
            }
        }

        private void dgvOEMItemStockCountList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvOEMItemStockCountList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            // Color the Full Packing Qty column
            if (dgv.Columns[col].Name == headerFullPackingQty)
            {
                e.CellStyle.BackColor = Color.LightYellow;
                e.CellStyle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            }

            if (dgv.Columns[col].Name == headerPartCode)
            {
                string partCode = dgv.Rows[row].Cells[headerPartCode].Value.ToString();

                if (string.IsNullOrEmpty(partCode))
                {
                    dgv.Rows[row].Height = 4;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                    // Apply alternating row colors here
                    dgv.Rows[row].DefaultCellStyle.BackColor = (row % 2 == 0) ? Color.White : Color.FromArgb(245, 245, 245);
                }
            }
        }

        #endregion

        #region UI Function

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvOEMItemStockCountList.SuspendLayout();

            string text = btnFilter.Text;

            if (text == textSearchFilter)
            {
                tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 120f);

                dgvOEMItemStockCountList.ResumeLayout();

                btnFilter.Text = textHideFilter;
            }
            else if (text == textHideFilter)
            {
                tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);

                dgvOEMItemStockCountList.ResumeLayout();

                btnFilter.Text = textSearchFilter;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cust = cmbCustomer.Text;
            custChanging = true;
            dgvOEMItemStockCountList.DataSource = null;

            txtItemSearch.Text = "Search";
            txtItemSearch.ForeColor = SystemColors.GrayText;
            ItemSearchUIReset();

            if (cmbCustomer.SelectedIndex != -1)
            {
                LoadItemPurchaseByCustomer();
            }

            custChanging = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvOEMItemStockCountList.SelectionMode = DataGridViewSelectionMode.CellSelect;

            DT_ITEM = dalItem.Select();

            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                FullForecastReport();

                if (!(txtItemSearch.Text.Length == 0 || txtItemSearch.Text == text.Search_DefaultText))
                {
                    lblSearchClear.Visible = true;
                    ItemSearch();
                }

            }
        }

        private void ItemSearchUIReset()
        {
            CURRENT_ROW_JUMP = -1;
            lblSearchInfo.Text = "";
            Row_Index_Found = new List<int>();

            lblResultNo.Text = "";
            //lblSearchClear.Visible = false;
            btnPreviousSearchResult.Enabled = false;
            btnNextSearchResult.Enabled = false;
        }
        private void ItemSearch()
        {

            ItemSearchUIReset();
            string Searching_Text = txtItemSearch.Text.ToUpper();

            if(Searching_Text != "SEARCH" )
            {
                DataGridView dgv = dgvOEMItemStockCountList;

                DataTable dgv_List = (DataTable)dgv.DataSource;

                int itemFoundCount = 0;

                //search data and row jump
                if (dgv_List != null && !string.IsNullOrEmpty(Searching_Text))
                    foreach (DataRow row in dgv_List.Rows)
                    {
                        //dt_Row[headerPartCode] = uData.part_code;
                        //dt_Row[headerPartName] = uData.part_name;
                        string itemCode = row[headerPartCode].ToString().ToUpper();
                        string itemName = row[headerPartName].ToString().ToUpper();

                        if (itemCode.ToUpper().Contains(Searching_Text) || itemName.ToUpper().Contains(Searching_Text))
                        {
                            int rowIndex = dgv_List.Rows.IndexOf(row);

                            Row_Index_Found.Add(rowIndex);
                        }

                    }

                //remove duplicate data
                Row_Index_Found = Row_Index_Found.Distinct().ToList();

                itemFoundCount = Row_Index_Found.Count;

                lblSearchInfo.Text = itemFoundCount + textRowFound;

                if (itemFoundCount > 0)
                {
                    JumpToNextRow();
                    btnPreviousSearchResult.Enabled = false;
                    btnNextSearchResult.Enabled = true;
                }

            }

        }
        private int FindParentIndex(int rowIndexSearching)
        {
            int parentIndex = rowIndexSearching;

            DataTable dt = (DataTable)dgvOEMItemStockCountList.DataSource;

            string itemType = dt.Rows[rowIndexSearching][headerType].ToString();

            if(dt != null && itemType != typeSingle && itemType != typeParent)
            {
                for (int i = rowIndexSearching; i >= 0; i--)
                {
                    itemType = dt.Rows[i][headerType].ToString();

                    if (itemType == typeParent)
                    {
                        return i;
                    }
                }
            }
            return parentIndex;
        }
        private void JumpToNextRow()
        {
            var last = Row_Index_Found.Last();

            foreach(var i in Row_Index_Found)
            {
                if(i > CURRENT_ROW_JUMP)
                {
                    CURRENT_ROW_JUMP = i;


                    dgvOEMItemStockCountList.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);

                    dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                    dgvOEMItemStockCountList.CurrentCell = dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];


                    btnPreviousSearchResult.Enabled = true;

                    //check if last row
                    btnNextSearchResult.Enabled = !(i == last);

                    lblResultNo.Text = "#" + (Row_Index_Found.IndexOf(i)+1).ToString();

                    return;
                }
            }

            if(CURRENT_ROW_JUMP != -1)
            {
                dgvOEMItemStockCountList.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                dgvOEMItemStockCountList.CurrentCell = dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];

            }
        }

        private void BackToPreviousRow()
        {
            var first = Row_Index_Found.First();

            if(Row_Index_Found.Count - 1 >= 0)
                for (int i = Row_Index_Found.Count - 1 ; i >= 0 ; i--)
                {
                    if (Row_Index_Found[i] < CURRENT_ROW_JUMP)
                    {
                        CURRENT_ROW_JUMP = Row_Index_Found[i];
                        dgvOEMItemStockCountList.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                        dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                        dgvOEMItemStockCountList.CurrentCell = dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];

                        //check if first row
                        btnPreviousSearchResult.Enabled = !(CURRENT_ROW_JUMP == first);

                        btnNextSearchResult.Enabled = true;

                        lblResultNo.Text = "#" + (i+1).ToString();

                        return;
                    }
               
                }


            if (CURRENT_ROW_JUMP != -1)
            {
                dgvOEMItemStockCountList.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                dgvOEMItemStockCountList.CurrentCell = dgvOEMItemStockCountList.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];
            }
        }

        private void FullForecastReport()
        {

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvOEMItemStockCountList;

            DT_FORECAST_REPORT = FullDetailForecastData();//9562ms-->3464ms (16/3) -> 2537ms (17/3)

            dgv.DataSource = DT_FORECAST_REPORT;


            if (dgv.DataSource != null)
            {
                ColorData();//720ms (16/3)

                dgvOEMItemStockCountList.Columns.Remove(headerItemType);

                if (cbSpecialTypeColorMode.Checked)
                    dgvOEMItemStockCountList.Columns.Remove(headerParentColor);

                dgvOEMItemStockCountList.Columns.Remove(headerType);
                dgvOEMItemStockCountList.Columns.Remove(headerBackColor);
                dgvOEMItemStockCountList.Columns.Remove(headerBalType);

                DgvForecastReportUIEdit(dgvOEMItemStockCountList); //973ms (16/3)

             

                dgvOEMItemStockCountList.ResumeLayout();

                dgvOEMItemStockCountList.ClearSelection();

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
            }

            frmLoading.CloseForm();
        }

        private void ShowDetailForecastReport()
        {
            frmLoading.ShowLoadingScreen();
            DataGridView dgv = dgvOEMItemStockCountList;
            dgv.DataSource = SearchForecastData();
     
            if (dgv.DataSource != null)
            {
                ColorData();
                DgvForecastReportUIEdit(dgvOEMItemStockCountList);
                dgvOEMItemStockCountList.Columns.Remove(headerItemType);
                if (cbSpecialTypeColorMode.Checked)
                    dgvOEMItemStockCountList.Columns.Remove(headerParentColor);
                dgvOEMItemStockCountList.Columns.Remove(headerType);
                dgvOEMItemStockCountList.Columns.Remove(headerBackColor);
                dgvOEMItemStockCountList.Columns.Remove(headerBalType);
                dgvOEMItemStockCountList.ClearSelection();
                dgvOEMItemStockCountList.ResumeLayout();
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }
            frmLoading.CloseForm();//568ms,515ms,531ms
        }


        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (txtItemSearch.Text.Length == 0 || txtItemSearch.Text == text.Search_DefaultText)
            {
                lblSearchClear.Visible = false;
                ItemSearchUIReset();
            }
            else
            {
                lblSearchClear.Visible = true;
                ItemSearch();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        #endregion

        #region Loading Data

        private void frmForecastReport_NEW_Load(object sender, EventArgs e)
        {

            DT_MACHINE_SCHEDULE = dalPlanning.SelectCompletedOrRunningPlan();
            DT_PMMA_DATE = dalPmmaDate.Select();
            DT_JOIN = dalJoin.SelectAllWithChildCat();
         
            //default all customer item list setting
            DataTable dt_Item_Cust = dalItemCust.SelectAllExcludedOTHER();

            //filter out terminated item
            if (!cbIncludeTerminated.Checked && DT_ITEM_CUST != null)
                dt_Item_Cust = RemoveTerminatedItem(dt_Item_Cust);

       
            dgvOEMItemStockCountList.SuspendLayout();

            tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
            tlpForecastReport.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);

            dgvOEMItemStockCountList.ResumeLayout();

            btnFilter.Text = textSearchFilter;

            cmbCustomer.Focus();
            loaded = true;

            string cust = cmbCustomer.Text;
            custChanging = true;

            
            custChanging = false;

        }



        private void InitializeFilterData()
        {
            loaded = false;

            LoadSortByComboBoxData();

            loaded = true;
        }

        private void LoadSortByComboBoxData()
        {
           
           
        }

        
    

        private Tuple<int,bool, double> EstimateNextOrderAndCheckIfStillActive(DataTable DT_PMMA_DATE, DataTable dt_trfToCustomer, string _ItemCode, string _Customer)
        {
            int estimateOrder = 0;
            bool NonActiveItem = true;

            int selectedCurentMonth = DateTime.Now.Month;

            int preMonth = 0, preYear = 0, dividedQty = 0;
            double singleOutQty = 0, totalTrfOutQty = 0;

            double deliveredQtyThisMonth = 0;

            int monthNow = DateTime.Now.Month;
            int yearNow = DateTime.Now.Year;

            int inactiveMonthsThreshold = int.TryParse(txtInactiveMonthsThreshold.Text, out int i) ? i : 0;

            int MonthAgo = DateTime.Now.AddMonths(inactiveMonthsThreshold * -1).Month;
            int YearAgo = DateTime.Now.AddMonths(inactiveMonthsThreshold * -1).Year;
            DateTime ActiveMinmumDate = new DateTime(Convert.ToInt32(YearAgo), Convert.ToInt32(MonthAgo), 1);


            foreach (DataRow row in dt_trfToCustomer.Rows)
            {
                string trfResult = row[dalTrfHist.TrfResult].ToString();
                string itemCode = row[dalTrfHist.TrfItemCode].ToString();
                string DB_Customer = row[dalTrfHist.TrfTo].ToString();

                if (trfResult == "Passed" && _ItemCode == itemCode && DB_Customer == _Customer)
                {

                    double trfQty = double.TryParse(row[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;
                    DateTime trfDate = DateTime.TryParse(row[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                    int month = 0;
                    int year = 0;

                    if (trfDate == DateTime.MaxValue)
                    {
                        continue;
                    }
                    else
                    {
                        if (trfDate.Month == monthNow && trfDate.Year == yearNow)
                        {
                            deliveredQtyThisMonth += trfQty;
                        }
                        else
                        {
                            totalTrfOutQty += trfQty;
                        }

                        month = trfDate.Month;
                        year = trfDate.Year;

                        DateTime TransferDate = new DateTime(Convert.ToInt32(year),Convert.ToInt32(month),1);

                        if (TransferDate >= ActiveMinmumDate)
                        {
                            NonActiveItem = false;
                        }

                        if (preMonth != 0 && preYear != 0 && preMonth == month && preYear == year)
                        {
                            singleOutQty += trfQty;
                        }
                        else
                        {
                            singleOutQty = trfQty;
                            preMonth = month;
                            preYear = year;

                            if (month != monthNow || year != yearNow)
                            {
                                dividedQty++;
                            }

                        }

                    }
                }
            }

            if (totalTrfOutQty == 0 || dividedQty <= 2)
            {
                estimateOrder = 0;
            }
            else
            {
                estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty / 100, 0) * 100;

                if (estimateOrder == 0)
                    estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty, 0);
            }

            return Tuple.Create(estimateOrder, NonActiveItem, deliveredQtyThisMonth);
        }
        private DataTable RemoveTerminatedItem(DataTable dt)
        {
            //"(TERMINATED)"
            DataTable dt_NEW = dt.Copy();

            if(dt_NEW != null)
            {
                dt_NEW.AcceptChanges();
                foreach (DataRow row in dt_NEW.Rows)
                {
                    // If this row is offensive then
                    string itemName = row[dalItem.ItemName].ToString();

                    if (itemName.Contains(text.Cat_Terminated))
                    {
                        row.Delete();
                    }

                }
                dt_NEW.AcceptChanges();
            }
           
            return dt_NEW;

        }

        private DataTable RemoveOtherFromTransferRecord(DataTable dt)
        {
            //"(TERMINATED)"
            DataTable dt_NEW = dt.Copy();

            dt_NEW.AcceptChanges();
            foreach (DataRow row in dt_NEW.Rows)
            {
                // If this row is offensive then
                string Customer = row[dalTrfHist.TrfTo].ToString();

                if (Customer.ToUpper().Contains("OTHER"))
                {
                    row.Delete();
                }

            }
            dt_NEW.AcceptChanges();

            return dt_NEW;

        }

        private void LoadItemPurchaseByCustomer()
        {
            string customerName = cmbCustomer.Text;

            DT_ITEM_CUST = new DataTable();

            if (customerName == text.Cmb_All)
            {
                DT_ITEM_CUST = dalItemCust.SelectAllExcludedOTHER();
            }
            else if (!string.IsNullOrEmpty(customerName)) 
            {
                DT_ITEM_CUST = dalItemCust.custSearch(customerName);
            }


            if (!cbIncludeTerminated.Checked && DT_ITEM_CUST != null)
                DT_ITEM_CUST = RemoveTerminatedItem(DT_ITEM_CUST);

            DT_ITEM_CUST.Columns.Add(text.Header_GotNotPackagingChild);
          

            if (DT_ITEM_CUST != null)
            {
                DT_JOIN = DT_JOIN != null && DT_JOIN.Rows.Count > 0 ? DT_JOIN: dalJoin.SelectAllWithChildCat();
                DT_ITEM = DT_ITEM != null && DT_ITEM.Rows.Count > 0 ? DT_ITEM : dalItem.Select();


                foreach (DataRow row in DT_ITEM_CUST.Rows)
                {
                    string itemCode = row[dalItem.ItemCode].ToString();

                    bool gotNotPackagingChild = tool.ifGotNotPackagingChild(itemCode, DT_JOIN, DT_ITEM);
                    row[text.Header_GotNotPackagingChild] = gotNotPackagingChild;

                }
            }
        }


        private DataTable FullDetailForecastData()
        {
            #region Setting

            Cursor = Cursors.WaitCursor;

            lblForecastType.Text = ReportType_Full;

            btnFullReport.Enabled = false;
            btnExcel.Enabled = false;
            btnExcelAll.Enabled = false;

            DataGridView dgv = dgvOEMItemStockCountList;

            dgvOEMItemStockCountList.SuspendLayout();
           
            dgvOEMItemStockCountList.DataSource = null;

            DataTable dt_Data = NewForecastReportTable();

            DataRow dt_Row;

            var getForecastPeriod = getMonthYearPeriod();

            DataTable dt_ItemForecast = dalItemForecast.SelectWithRange(getForecastPeriod.Item1, getForecastPeriod.Item2, getForecastPeriod.Item3, getForecastPeriod.Item4);

            DateTime DateToday = DateTime.Now;
            int year = DateToday.Year - 1;
            DateTime LastYear = DateTime.Parse("1/1/" + year);

            string deliveredHistory_From = LastYear.ToString("yyyy/MM/dd");
            string deliveredHistory_To = DateToday.AddMonths(1).ToString("yyyy/MM/dd");

            DataTable dt_TrfHist = dalTrfHist.ItemDeliveredRecordSearch(deliveredHistory_From, deliveredHistory_To);

            dt_TrfHist = RemoveOtherFromTransferRecord(dt_TrfHist);

            DataTable DT_PMMA_DATE = dalPmmaDate.Select();
            
            int index = 1;

            #endregion

            #region Load Data

            #region load single part
            //308ms 
            foreach (DataRow row in DT_ITEM_CUST.Rows)
            {

                string itemSearch = row[dalItem.ItemCode].ToString();

                bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

                if (!gotNotPackagingChild)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();
                    uData.customer_name = row[dalItemCust.CustName].ToString();
                    uData.cust_id = row[dalItemCust.CustID].ToString();

                    var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, uData.customer_name);

                    uData.estimate = estimate.Item1;
                    bool NonActiveItem = estimate.Item2;
                    uData.deliveredOut = (float)estimate.Item3;

                    if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NonActiveItem && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                    {
                        uData.item_remark = row[dalItem.ItemRemark].ToString();

                        int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                        int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                     
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);
                    

                        dt_Row = dt_Data.NewRow();


                       
                        dt_Row[headerIndex] = uData.index;

                        if(dt_Data.Columns.Contains(text.Header_Customer))
                        dt_Row[text.Header_Customer] = uData.customer_name;

                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                    
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;

                        decimal readyStockDecimal = Math.Round((decimal)uData.ready_stock, 2);
                        dt_Row[headerReadyStock] = readyStockDecimal % 1 == 0 ? (decimal)(int)readyStockDecimal : readyStockDecimal;


                        dt_Data.Rows.Add(dt_Row);
                        index++;
                    }
                }

            }

            #endregion
            //^^^3930ms -> 1497ms -> 1000ms (17/3)

            #region load assembly part

            foreach (DataRow row in DT_ITEM_CUST.Rows)
            {

                string itemSearch = row[dalItem.ItemCode].ToString();

                int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);
                bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

                //check if got child part also
                if ((assembly == 1 || production == 1) && gotNotPackagingChild)//tool.ifGotChild2(uData.part_code, dt_Join)
                {
                    uData.cust_id = row[dalItemCust.CustID].ToString();
                    uData.customer_name = row[dalItemCust.CustName].ToString();
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, uData.cust_id, uData.part_code, 1, 2, 3);
                    uData.forecast1 = forecastData.Item1;
                    uData.forecast2 = forecastData.Item2;
                    uData.forecast3 = forecastData.Item3;

                    var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, uData.customer_name);
                    uData.estimate = estimate.Item1;
                    bool NonActiveItem = estimate.Item2;

                    uData.deliveredOut = (float) estimate.Item3;

                    if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NonActiveItem && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                    {
                        uData.item_remark = row[dalItem.ItemRemark].ToString();
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                      
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uData.index;

                        if (dt_Data.Columns.Contains(text.Header_Customer))
                            dt_Row[text.Header_Customer] = uData.customer_name;

                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerBalType] = balType_Unique;
                    
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;

                        decimal readyStockDecimal = Math.Round((decimal)uData.ready_stock, 2);
                        dt_Row[headerReadyStock] = readyStockDecimal % 1 == 0 ? (decimal)(int)readyStockDecimal : readyStockDecimal;


                        if (assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = InsertMoldingMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
                        }

                        if (uData.part_code.Substring(0, 3) == text.Inspection_Pass)
                        {
                            dt_Row[headerParentColor] = InspectionMarking;
                        }


                        dt_Data.Rows.Add(dt_Data.NewRow());
                        dt_Data.Rows.Add(dt_Row);
                        index++;

                        LoadChild(dt_Data, uData, 0.1m);
                    }
                }
            }

            #endregion
            //2654ms -> 1320ms (16/3) -> 895ms (17/3)

            #endregion

            if (dt_Data.Rows.Count > 0)
            {
                dt_Data = CalRepeatedData(dt_Data);
                dt_Data = ItemSearch(dt_Data);

                //dtMasterData = Sorting(dt_Data);
                dtMasterData = dt_Data;
            }
            else
            {
                dtMasterData = null;
            }

            btnFullReport.Enabled = true;
            btnExcel.Enabled = true;
            btnExcelAll.Enabled = true;

            Cursor = Cursors.Arrow;

            return dtMasterData;

        }

        private DataTable CalRepeatedData(DataTable dt)
        {
           
            bool colorChange = false;
            int colorOrder = 0;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
               
                string rowReference = "(" + dt.Rows[i][headerIndex].ToString() + ")";
                int repeatedCount = 1;

                string firstItem = dt.Rows[i][headerPartCode].ToString();
               
                string firstParentColor = dt.Rows[i][headerParentColor].ToString();

              

                if (!string.IsNullOrEmpty(firstItem) )
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();
                        string index = dt.Rows[j][headerIndex].ToString();

                        if (nextItem.Equals(firstItem))
                        {

                            repeatedCount++;

                            rowReference += " (" + dt.Rows[j][headerIndex].ToString() + ")";


                            dt.Rows[i][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBalType] = balType_Repeated;
                            colorChange = true;
                        }
                    }



                    if (colorChange)
                    {
                      
                        dt.Rows[i][headerBalType] = balType_Total;
                        dt.Rows[i][headerRowReference] = rowReference;
                        colorOrder++;

                        if (colorOrder > Enum.GetValues(typeof(ColorSet)).Cast<int>().Max())
                        {
                            colorOrder = 0;

                        }
                        colorChange = false;
                    }

                }
            }

            dt = RepeatedRowReferenceRemark(dt);

            return dt;


        }

        private DataTable SearchForecastData()
        {
            #region pre setting

            Cursor = Cursors.WaitCursor;

            lblForecastType.Text = ReportType_Full;

            btnFullReport.Enabled = false;
         
            btnExcel.Enabled = false;
            btnExcelAll.Enabled = false;

            DataGridView dgv = dgvOEMItemStockCountList;

         
            dgvOEMItemStockCountList.SuspendLayout();
           
         
            dgvOEMItemStockCountList.DataSource = null;


            string customer = cmbCustomer.Text;
            DataTable dt_Data = NewForecastReportTable();

            #endregion

            if (!string.IsNullOrEmpty(customer))//29ms
            {
                DataRow dt_Row;

                DataTable dt_Item_Cust = dalItemCust.custSearch(customer);

                //filter out terminated item
                if (!cbIncludeTerminated.Checked)
                    dt_Item_Cust = RemoveTerminatedItem(dt_Item_Cust);

                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(customer).ToString());

                //DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerSearch(customer);
                
                //speed test 1 start ------------------------------------------------------------------------------------
                DateTime DateToday = DateTime.Now;

                int year = DateToday.Year - 1;

                DateTime LastYear = DateTime.Parse("1/1/" + year);

                string From = LastYear.ToString("yyyy/MM/dd");
                string To = DateToday.AddMonths(1).ToString("yyyy/MM/dd");

                DataTable dt_TrfHist = dalTrfHist.ItemDeliveredRecordSearch(customer, From, To);

                //DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerTransferDataOnlySearch(PMMA, from, to);

                DataTable DT_PMMA_DATE = dalPmmaDate.Select();//437ms

                //DataTable dt_Estimate = dalTrfHist.ItemToCustomerAllTimeSearch(cmbCustomer.Text);

                //dt_MacSchedule = dalPlanning.Select();

                //speed test 1 end-----------------------------------------------------------------------------------------132ms
                //DT_JOIN = dalJoin.SelectAll();
                //DT_ITEM = dalItem.Select();
                //dt_ProRecord = dalProRecord.SelectWithItemInfo();
                //1516ms

                int index = 1;

                dt_Item_Cust.DefaultView.Sort = "item_name ASC";

                dt_Item_Cust = dt_Item_Cust.DefaultView.ToTable();

                dt_Item_Cust.Columns.Add(text.Header_GotNotPackagingChild);
                //^^^502ms^^^

                #region load single part
                

                foreach (DataRow row in dt_Item_Cust.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    uData.item_remark = row[dalItem.ItemRemark].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);
                   
                    bool gotNotPackagingChild = tool.ifGotNotPackagingChild(uData.part_code, DT_JOIN, DT_ITEM);

                    row[text.Header_GotNotPackagingChild] = gotNotPackagingChild;



                    if (!gotNotPackagingChild)//assembly == 0 && production == 0
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);
                       
                        var forecastData = GetThreeMonthsForecastQty(dt_ItemForecast, uData.part_code, 1, 2, 3);
                        uData.forecast1 = forecastData.Item1;
                        uData.forecast2 = forecastData.Item2;
                        uData.forecast3 = forecastData.Item3;

                        var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, customer);

                        uData.estimate = estimate.Item1;
                        bool NoDeliveredPast6Months = estimate.Item2;

                       
                       



                        dt_Row = dt_Data.NewRow();


                       
                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                    
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;

                        decimal readyStockDecimal = Math.Round((decimal)uData.ready_stock, 2);
                        dt_Row[headerReadyStock] = readyStockDecimal % 1 == 0 ? (decimal)(int)readyStockDecimal : readyStockDecimal;


                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NoDeliveredPast6Months && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                        {
                            dt_Data.Rows.Add(dt_Row);
                            index++;
                        }

                    }
                   
                }

                #endregion
                //^^^755ms,793ms^^^

                #region load assembly part

                foreach (DataRow row in dt_Item_Cust.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();
                    uData.item_remark = row[dalItem.ItemRemark].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

                    //bool gotNotPackagingChild = tool.ifGotNotPackagingChild(uData.part_code, dt_Join, dt_Item);

                    //check if got child part also
                    if ((assembly == 1 || production == 1) && gotNotPackagingChild)//tool.ifGotChild2(uData.part_code, dt_Join)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                       
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);


                        var forecastData = GetThreeMonthsForecastQty(dt_ItemForecast, uData.part_code, 1, 2, 3);
                        uData.forecast1 = forecastData.Item1;
                        uData.forecast2 = forecastData.Item2;
                        uData.forecast3 = forecastData.Item3;

                        var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, customer);

                        uData.estimate = estimate.Item1;
                        bool NoDeliveredPast6Months = estimate.Item2;

                        dt_Row = dt_Data.NewRow();

                        
                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerBalType] = balType_Unique;
                       
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                     

                    
                        if (assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = InsertMoldingMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
                        }

                        if (uData.part_code.Substring(0, 3) == text.Inspection_Pass)
                        {
                            dt_Row[headerParentColor] = InspectionMarking;
                        }


                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NoDeliveredPast6Months && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
                        {
                            dt_Data.Rows.Add(dt_Data.NewRow());
                            dt_Data.Rows.Add(dt_Row);
                            index++;

                            //load child
                            LoadChild(dt_Data, uData, 0.1m);
                        }



                    }
                }

                #endregion
                //2061ms,1295ms

            }

            if (dt_Data.Rows.Count > 0)
            {

                dt_Data = ItemSearch(dt_Data);

                dtMasterData = dt_Data;
                //dtMasterData = Sorting(dt_Data);

            }
            else
            {
                dtMasterData = null;
            }

          
            
            btnFullReport.Enabled = true;
            btnExcel.Enabled = true;
            btnExcelAll.Enabled = true;

            Cursor = Cursors.Arrow;

            return dtMasterData;

        }

        //private DataTable Sorting(DataTable dt)
        //{
        //    string keywords = cmbSoryBy.Text;
        //    DataTable dt_Copy;

        //    bool dataInserted = false;

        //    bool singleToParent = false;
        //    bool spaceAdded = false;

        //    if (cmbSoryBy.SelectedIndex == -1 || keywords.Equals("") || string.IsNullOrEmpty(keywords))
        //    {
        //        dt_Copy = dt.Copy();
        //    }
        //    else
        //    {
        //        dt_Copy = dt.Clone();

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            dataInserted = false;

        //            double sortingTarget_1 = double.TryParse(dt.Rows[i][keywords].ToString(), out sortingTarget_1) ? sortingTarget_1 : 0;
        //            string itemType_1 = dt.Rows[i][headerType].ToString();

        //            string itemCode = dt.Rows[i][headerPartCode].ToString();

        //            //if(itemCode.Equals("V96LAR000"))
        //            //{
        //            //    float test = 0;
        //            //}
        //            #region add divider seperate single item and group item

        //            if (itemType_1.Equals(typeParent) && !spaceAdded)
        //            {
        //                singleToParent = true;
        //            }

        //            if (singleToParent && !spaceAdded)
        //            {
        //                //add space
        //                dt_Copy.Rows.Add(dt_Copy.NewRow());
        //                spaceAdded = true;
        //            }

        //            #endregion
                    
        //            if (itemType_1.Equals(typeSingle) || itemType_1.Equals(typeParent))
        //            {
        //                for (int j = 0; j < dt_Copy.Rows.Count; j++)
        //                {
        //                    string itemType_2 = dt_Copy.Rows[j][headerType].ToString();

        //                    if (itemType_1.Equals(itemType_2))
        //                    {
        //                        double sortingTarget_2 = double.TryParse(dt_Copy.Rows[j][keywords].ToString(), out sortingTarget_2) ? sortingTarget_2 : 0;

        //                        bool waitingToInsert = cbDescending.Checked ? sortingTarget_1 > sortingTarget_2 : sortingTarget_1 < sortingTarget_2;

        //                        //if (cbDescending.Checked)
        //                        //{
        //                        //    waitingToInsert = sortingTarget_1 > sortingTarget_2;
        //                        //}
        //                        //else
        //                        //{
        //                        //    waitingToInsert = sortingTarget_1 < sortingTarget_2;
        //                        //}

        //                        if(waitingToInsert)
        //                        {
        //                            if (itemType_1.Equals(typeSingle))
        //                            {
        //                                //insert
        //                                DataRow insertingRow = dt_Copy.NewRow();

        //                                insertingRow.ItemArray = dt.Rows[i].ItemArray;

        //                                dt_Copy.Rows.InsertAt(insertingRow, j);
        //                                dataInserted = true;
        //                                break;
        //                            }

        //                            else
        //                            {
        //                                //insert
        //                                DataRow insertingRow = dt_Copy.NewRow();

        //                                insertingRow.ItemArray = dt.Rows[i].ItemArray;

        //                                dt_Copy.Rows.InsertAt(insertingRow, j);

        //                                int tempIndex = j;

        //                                for (int k = i + 1; k < dt.Rows.Count; k++)
        //                                {
        //                                    tempIndex++;
        //                                    string _type = dt.Rows[k][headerType].ToString();

        //                                    if (_type.Equals(typeParent))
        //                                    {
        //                                        //dt_Copy.Rows.Add(dt_Copy.NewRow());
        //                                        break;
        //                                    }
        //                                    else
        //                                    {
        //                                        insertingRow = dt_Copy.NewRow();

        //                                        insertingRow.ItemArray = dt.Rows[k].ItemArray;

        //                                        dt_Copy.Rows.InsertAt(insertingRow, tempIndex);
        //                                    }
        //                                }


        //                                dataInserted = true;
        //                                break;
        //                            }
        //                        }
                               
        //                    }
                            

                          
        //                }

        //                if (!dataInserted)
        //                {
        //                    //add data
        //                    if (itemType_1.Equals(typeSingle))
        //                    {
        //                        dt_Copy.ImportRow(dt.Rows[i]);
        //                    }

        //                    else if (itemType_1.Equals(typeParent))
        //                    {
        //                        dt_Copy.ImportRow(dt.Rows[i]);

        //                        for (int k = i + 1; k < dt.Rows.Count; k++)
        //                        {
        //                            string _type = dt.Rows[k][headerType].ToString();

        //                            if (_type.Equals(typeParent))
        //                            {
        //                                //dt_Copy.Rows.Add(dt_Copy.NewRow());
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                dt_Copy.ImportRow(dt.Rows[k]);

        //                            }
        //                        }


        //                    }

        //                }
        //            }
        //        }
        //    }

        //    //remove last row if it is empty
        //    if (dt_Copy.Rows.Count > 0)
        //    {
        //        string itemCode = dt_Copy.Rows[dt_Copy.Rows.Count - 1][headerPartCode].ToString();

        //        if (string.IsNullOrEmpty(itemCode))
        //        {
        //            dt_Copy.Rows.Remove(dt_Copy.Rows[dt_Copy.Rows.Count - 1]);
        //        }
        //        dt_Copy.AcceptChanges();
        //    }

        //    return dt_Copy;

        //}

        private DataTable ItemSearch(DataTable dt)
        {
            string keywords = txtItemSearch.Text;
            DataTable dt_Copy;
            int parentIndex = -1;

            if (string.IsNullOrEmpty(keywords) || keywords.Equals("Search"))
            {
                dt_Copy = dt.Copy();
            }
            else
            {
                dt_Copy = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string itemName = dt.Rows[i][headerPartName].ToString();
                    string itemCode = dt.Rows[i][headerPartCode].ToString();
                    string itemType = dt.Rows[i][headerType].ToString();

                    bool itemMatch = itemName.Contains(keywords.ToUpper()) || itemCode.Contains(keywords.ToUpper());

                    if (itemMatch && dt.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (itemType.Equals(typeSingle))
                        {
                            dt_Copy.ImportRow(dt.Rows[i]);
                            //dt.Rows.Remove(dt.Rows[i]);
                            dt.Rows[i][headerType] = DBNull.Value;

                            dt_Copy.Rows.Add(dt_Copy.NewRow());
                        }
                        else if (itemType.Equals(typeParent))
                        {
                            parentIndex = i;

                            dt_Copy.ImportRow(dt.Rows[i]);
                            //dt.Rows.Remove(dt.Rows[i]);
                            dt.Rows[i][headerType] = DBNull.Value;

                            for (int j = i + 1; j < dt.Rows.Count; j++)
                            {
                                string _type = dt.Rows[j][headerType].ToString();

                                if (_type.Equals(typeParent))
                                {
                                    break;
                                }
                                else if (dt.Rows[j].RowState != DataRowState.Deleted)
                                {
                                    dt_Copy.ImportRow(dt.Rows[j]);
                                    //dt.Rows.Remove(dt.Rows[j]);
                                    dt.Rows[j][headerType] = DBNull.Value;
                                }
                            }
                        }

                        else if (itemType.Equals(typeChild))
                        {
                            for (int j = i; j >= 0; j--)
                            {
                                string _type = dt.Rows[j][headerType].ToString();

                                if (_type.Equals(typeParent))
                                {
                                    parentIndex = j;
                                    break;
                                }
                            }

                            if (parentIndex >= 0)
                            {
                                dt_Copy.ImportRow(dt.Rows[parentIndex]);
                                //dt.Rows.Remove(dt.Rows[parentIndex]);
                                dt.Rows[parentIndex][headerType] = DBNull.Value;
                                for (int k = parentIndex + 1; k < dt.Rows.Count; k++)
                                {
                                    string _type = dt.Rows[k][headerType].ToString();

                                    if (_type.Equals(typeParent))
                                    {
                                        break;
                                    }
                                    else if (dt.Rows[k].RowState != DataRowState.Deleted)
                                    {
                                        dt_Copy.ImportRow(dt.Rows[k]);
                                        //dt.Rows.Remove(dt.Rows[k]);
                                        dt.Rows[k][headerType] = DBNull.Value;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Parent not found. (ItemSearch method error)");
                            }

                        }
                    }
                }
            } 

            if (dt_Copy.Rows.Count > 0)
            {
                string itemCode = dt_Copy.Rows[dt_Copy.Rows.Count - 1][headerPartCode].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    dt_Copy.Rows.Remove(dt_Copy.Rows[dt_Copy.Rows.Count - 1]);
                }
                dt_Copy.AcceptChanges();
            }

            return dt_Copy;

        }

        private DataTable RepeatedRowReferenceRemark(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                string firstItem = dt.Rows[i][headerPartCode].ToString();
                string rowReference = dt.Rows[i][headerRowReference].ToString();

                if (!string.IsNullOrEmpty(firstItem))
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();

                        if (firstItem.Equals(nextItem))
                        {
                            dt.Rows[j][headerRowReference] = rowReference;
                        }
                    }
                }
            }

            return dt;


        }

       
        private void LoadChild(DataTable dt_Data, dataTrfBLL uParentData, decimal subIndex)
        {
            DataRow dt_Row;
            dataTrfBLL uChildData = new dataTrfBLL();

            decimal index = (decimal) uParentData.index + subIndex;

            foreach (DataRow row in DT_JOIN.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if (parentCode.Equals(uParentData.part_code))
                {
                    string childCode = row[dalJoin.JoinChild].ToString();

                  

                    DataRow row_Item = tool.getDataRowFromDataTable(DT_ITEM, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part) || row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_SubMat);

                    if (itemMatch)
                    {
                        float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float i) ? Convert.ToSingle(row[dalJoin.JoinQty].ToString()) : 1;

                        dt_Row = dt_Data.NewRow();

                        uChildData.part_code = row_Item[dalItem.ItemCode].ToString();

                        uChildData.index = (double)index;
                        uChildData.item_remark = row_Item[dalItem.ItemRemark].ToString();
                        uChildData.part_name = row_Item[dalItem.ItemName].ToString();
                      
                       
                        uChildData.ready_stock = row_Item[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemStock]);

                        float stock = uChildData.ready_stock;

                        if (stock % 1 > 0)
                        {
                            stock = (float)Math.Round(stock * 100f) / 100f;

                            uChildData.ready_stock = stock;
                        }


                    

                        uChildData.forecast1 = uChildData.forecast1 < 0 ? uChildData.forecast1 * -1 : uChildData.forecast1;
                        uChildData.forecast2 = uChildData.forecast2 < 0 ? uChildData.forecast2 * -1 : uChildData.forecast2;
                        uChildData.forecast3 = uChildData.forecast3 < 0 ? uChildData.forecast3 * -1 : uChildData.forecast3;

                     



                        dt_Row = dt_Data.NewRow();



                      
                        dt_Row[headerIndex] = uChildData.index;
                        dt_Row[headerType] = typeChild;
                       
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row_Item[dalItem.ItemCat].ToString();
                        dt_Row[headerPartCode] = uChildData.part_code;
                        dt_Row[headerPartName] = CountZeros(subIndex.ToString()) + uChildData.part_name;
                      

                        decimal readyStockDecimal = Math.Round((decimal)uChildData.ready_stock, 2);
                        dt_Row[headerReadyStock] = readyStockDecimal % 1 == 0 ? (decimal)(int)readyStockDecimal : readyStockDecimal;


                        int assembly = row_Item[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemAssemblyCheck]);
                        int production = row_Item[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemProductionCheck]);

                        bool gotChild = tool.ifGotNotPackagingChild(uChildData.part_code, DT_JOIN, DT_ITEM);

                        if (assembly == 1 && production == 0 && gotChild)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = InsertMoldingMarking;
                        }
                        else if (assembly == 1 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
                        }

                        dt_Data.Rows.Add(dt_Row);

                        index += subIndex;

                        //check if got child part also
                        if (gotChild)
                        {
                            LoadChild(dt_Data, uChildData, subIndex / 10);
                        }
                    }

                }

            }
        }

        private string CountZeros(string input)
        {
            int count = 0;

            foreach (char c in input)
            {
                if (c == '0')
                {
                    count++;
                }
            }

            if(count >= 3)
            {
                var checkoint = 1;
            }

            return count > 0 ? new string('-', count) + "> " : "";
        }


        private Tuple<float,float,float> GetThreeMonthsForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum_1, int forecastNum_2, int forecastNum_3)
        {
            string monthString = DateTime.Now.ToString("MMMM");

            int month_1 = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year_1 = DateTime.Now.Year;

            month_1 += forecastNum_1 - 1;

            if (month_1 > 12)
            {
                month_1 -= 12;
                year_1++;
            }

            int month_2 = month_1 + 1;
            int year_2 = year_1;

            if (month_2 > 12)
            {
                month_2 -= 12;
                year_2++;
            }

            int month_3 = month_2 + 1;
            int year_3 = year_2;

            if (month_3 > 12)
            {
                month_3 -= 12;
                year_3++;
            }

            var forecastData = tool.getItemForecast(dt_ItemForecast, itemCode,  year_1,  month_1,  year_2,  month_2,  year_3,  month_3);

            return Tuple.Create(forecastData.Item1, forecastData.Item2, forecastData.Item3);
        }

        private Tuple<float, float, float> GetCustomerThreeMonthsForecastQty(DataTable dt_ItemForecast, string Customer,  string itemCode, int forecastNum_1, int forecastNum_2, int forecastNum_3)
        {
            string monthString = DateTime.Now.ToString("MMMM");


            int month_1 = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year_1 = DateTime.Now.Year;

            month_1 += forecastNum_1 - 1;

            if (month_1 > 12)
            {
                month_1 -= 12;
                year_1++;
            }

            int month_2 = month_1 + 1;
            int year_2 = year_1;

            if (month_2 > 12)
            {
                month_2 -= 12;
                year_2++;
            }

            int month_3 = month_2 + 1;
            int year_3 = year_2;

            if (month_3 > 12)
            {
                month_3 -= 12;
                year_3++;
            }

            var forecastData = tool.getCustomerItemForecast(dt_ItemForecast, Customer, itemCode, year_1, month_1, year_2, month_2, year_3, month_3);

            return Tuple.Create(forecastData.Item1, forecastData.Item2, forecastData.Item3);
        }
      
        private Tuple <int,int,int,int> getMonthYearPeriod()
        {
            int year = DateTime.Now.Year;

            int monthStart = 1;
            int yearStart = year;
            int monthEnd = 1;
            int yearEnd = year;

            string monthString = DateTime.Now.ToString("MMMM");

            monthStart = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;

            monthString = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).AddMonths(2).Month);

            monthEnd = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;

            if (monthEnd < monthStart)
            {
                yearEnd++;

            }

            return Tuple.Create(monthStart,yearStart,monthEnd,yearEnd);
        }

        #endregion

        private void EditNextLineRemarkForExcelExport(DataGridView dgv, bool removeNextLine)
        {
            if(dgv.Columns.Contains(text.Header_Remark))
            {

                DataTable dt = (DataTable)dgv.DataSource;
                
                foreach(DataRow row in dt.Rows)
                {
                    string remark = row[text.Header_Remark].ToString();

                    if(removeNextLine)
                    {
                        remark = remark.Replace("\n", "_");
                    }
                    else
                    {
                        remark = remark.Replace("_", "\n");

                    }

                    row[text.Header_Remark] = remark;

                }
            }
        }

        #region export to excel

        static void OpenCSVWithExcel(string path)
        {
            var ExcelApp = new Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);

            ExcelApp.Visible = true;
        }


        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;

            fileName = "OEM ITEM LIST (" + cmbCustomer.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                if(dgvOEMItemStockCountList.DataSource == null)
                {
                    MessageBox.Show("No data.");
                }
                else
                {
                    try
                    {
                        dgvOEMItemStockCountList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        if(dgvOEMItemStockCountList.Columns.Contains(headerRowReference))
                            dgvOEMItemStockCountList.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        

                        #region Excel 


                        Cursor = Cursors.WaitCursor;

                        btnExcel.Enabled = false;

                        btnFullReport.Enabled = false;

                        SaveFileDialog sfd = new SaveFileDialog();
                        string path = @"D:\StockAssistant\Document\OEM ITEM LIST";
                        Directory.CreateDirectory(path);
                        sfd.InitialDirectory = path;
                        sfd.Filter = "Excel Documents (*.xls)|*.xls";
                        sfd.FileName = setFileName();

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                            EditNextLineRemarkForExcelExport(dgvOEMItemStockCountList, true);

                            // Copy DataGridView results to clipboard
                            copyAlltoClipboard();

                            object misValue = Missing.Value;
                            Excel.Application xlexcel = new Excel.Application
                            {
                                DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                            };

                            frmLoading.ShowLoadingScreen();

                            Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                            Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                            xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"); ;
                            xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") OEM ITEM LIST";
                            xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                      
                            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                            xlWorkSheet.PageSetup.Zoom = false;

                            xlWorkSheet.PageSetup.FitToPagesWide = 1;
                            xlWorkSheet.PageSetup.FitToPagesTall = false;

                            double pointToCMRate = 0.035;
                            xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                            xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                            xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                            xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                            xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                            xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                            // Paste clipboard results to worksheet range
                            Range CR = (Range)xlWorkSheet.Cells[1, 1];
                            CR.Select();
                            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                            Range tRange = xlWorkSheet.UsedRange;
                            tRange.Font.Size = 8;
                            tRange.RowHeight = 15;
                            tRange.Font.Name = "Calibri";
                            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                            tRange.Borders.Weight = XlBorderWeight.xlThin;

                            DataGridView dgv = dgvOEMItemStockCountList;

                            //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                            Range FirstRow = xlWorkSheet.get_Range("a1:u1").Cells;
                            FirstRow.WrapText = true;
                            FirstRow.Font.Size = 6;
                            FirstRow.Font.Name = "Calibri";
                            FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                            FirstRow.RowHeight = 25;
                            FirstRow.Interior.Color = Color.WhiteSmoke;



                            DataTable dt = (DataTable)dgv.DataSource;

                            int Index = dgv.Columns[headerIndex].Index;
                          
                            int customerIndex = -1;

                            if (cmbCustomer.Text == text.Cmb_All)
                            {
                                customerIndex = dgv.Columns[text.Header_Customer].Index;
                                xlWorkSheet.Cells[1, customerIndex + 1].ColumnWidth = 10;
                            }

                            int nameIndex = dgv.Columns[headerPartName].Index;
                            int codeIndex = dgv.Columns[headerPartCode].Index;

                            int stockIndex = dgv.Columns[headerReadyStock].Index;
                       
                            
                            tRange.EntireColumn.AutoFit();

                            xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[stockIndex].InheritedStyle.BackColor);
                          
                            for (int i = 0; i <= dtMasterData.Rows.Count - 1; i++)
                            {
                                Range rangeIndex = (Range)xlWorkSheet.Cells[i + 2, Index + 1];

                                Range rangeName = (Range)xlWorkSheet.Cells[i + 2, nameIndex + 1];
                                Range rangeCode = (Range)xlWorkSheet.Cells[i + 2, codeIndex + 1];

                                Range rangeStock = (Range)xlWorkSheet.Cells[i + 2, stockIndex + 1];
                                
                                Range rangeRow = (Range)xlWorkSheet.Rows[i + 2];
                                //rangeRow.Rows.RowHeight = 40;
                                rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                                rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                          

                                string itemCode = dgv.Rows[i].Cells[headerPartCode].Value.ToString();

                                //color empty space
                                if (string.IsNullOrEmpty(itemCode))
                                {
                                    rangeRow.Rows.RowHeight = 2;
                                    Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[i + 2, 1], xlWorkSheet.Cells[i + 2, xlWorkSheet.UsedRange.Columns.Count]];
                                    //Range rng2 = xlWorkSheet.Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, xlWorkSheet.UsedRange.Columns.Count]);
                                    //Range changeRowBackColor = xlWorkSheet.get_Range(i+2, last);
                                    rng.Interior.Color = Color.Black;
                                }
                                else
                                {
                                    rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.ForeColor);
                                    rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.ForeColor);

                                    rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.BackColor);
                                    rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.BackColor);

                                    rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[stockIndex].InheritedStyle.BackColor);
                                  
                                }

                                string parentColor = dt.Rows[i][headerParentColor].ToString();
                                string type = dt.Rows[i][headerType].ToString();
                                string balType = dt.Rows[i][headerBalType].ToString();

                                //change parent color
                                if (parentColor.Equals(AssemblyMarking))
                                {
                                    rangeName.Font.Underline = true;
                                    rangeCode.Font.Underline = true;
                                }
                                else if (parentColor.Equals(InsertMoldingMarking))
                                {
                                    rangeName.Font.Underline = true;
                                    rangeCode.Font.Underline = true;
                                }
                                else if (parentColor.Equals(AssemblyAfterProductionMarking))
                                {
                                    rangeName.Font.Underline = true;
                                    rangeCode.Font.Underline = true;
                                }

                            }

                            // Save the excel file under the captured location from the SaveFileDialog
                            xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                            xlexcel.DisplayAlerts = true;
                            xlWorkBook.Close(true, misValue, misValue);
                            xlexcel.Quit();

                            releaseObject(xlWorkSheet);
                            releaseObject(xlWorkBook);
                            releaseObject(xlexcel);

                            // Clear Clipboard and DataGridView selection
                            Clipboard.Clear();
                            dgvOEMItemStockCountList.ClearSelection();

                            // Open the newly saved excel file
                            //if (File.Exists(sfd.FileName))
                            //    System.Diagnostics.Process.Start(sfd.FileName);
                            OpenCSVWithExcel(sfd.FileName);

                            EditNextLineRemarkForExcelExport(dgvOEMItemStockCountList, false);

                        }

                        #endregion


                    }
                    catch (Exception ex)
                    {
                        tool.saveToTextAndMessageToUser(ex);
                    }
                    finally
                    {
                        frmLoading.CloseForm();

                        dgvOEMItemStockCountList.SelectionMode = DataGridViewSelectionMode.CellSelect;

                        if (dgvOEMItemStockCountList.Columns.Contains(headerRowReference))
                            dgvOEMItemStockCountList.Columns[headerRowReference].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                      

                        btnExcel.Enabled = true;
                        btnFullReport.Enabled = true;


                        Cursor = Cursors.Arrow; // change cursor to normal type
                    }
                }
               
            }
          
        }

        private void copyAlltoClipboard()
        {
            dgvOEMItemStockCountList.SelectAll();
            DataObject dataObj = dgvOEMItemStockCountList.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnExcelAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\OEM ITEM LIST";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "OEM ITEM LIST(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;
                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    insertAllDataToSheet(path, sfd.FileName);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvOEMItemStockCountList.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void insertAllDataToSheet(string path, string fileName)
        {
            string custName = tool.getCustName(1);

            int totalCust = cmbCustomer.Items.Count;

            Excel.Application excelApp = new Excel.Application
            {
                Visible = true
            };

            Workbook g_Workbook = excelApp.Workbooks.Open(
               path,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing);

            object misValue = Missing.Value;

            //frmLoading.ShowLoadingScreen();
            for (int i = 0; i <= cmbCustomer.Items.Count - 1; i++)
            {
                cmbCustomer.SelectedIndex = i;
                ShowDetailForecastReport();

                string cust = cmbCustomer.Text;

                DataTable dt = NewForecastReportTable();

                if (dgvOEMItemStockCountList.DataSource != null)
                {
                    dt = (DataTable)dgvOEMItemStockCountList.DataSource;
                }

                if (dt.Rows.Count > 0)//if datagridview have data
                {
                    Worksheet xlWorkSheet = null;

                    int count = g_Workbook.Worksheets.Count;

                    xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                            g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                    xlWorkSheet.Name = cmbCustomer.Text;

                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") OEM ITEM LIST";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";


                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;
                    
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                    // Paste clipboard results to worksheet range
                    copyAlltoClipboard();
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);



                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Font.Size = 8;
                    tRange.RowHeight = 15;
                    tRange.Font.Name = "Calibri";
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;

                    DataGridView dgv = dgvOEMItemStockCountList;


                    //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                    Range FirstRow = xlWorkSheet.get_Range("a1:o1").Cells;
                    FirstRow.WrapText = true;
                    FirstRow.Font.Size = 6;
                    FirstRow.Font.Name = "Calibri";
                    FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    FirstRow.RowHeight = 25;
                    FirstRow.Interior.Color = Color.WhiteSmoke;



                    //DataTable dt = (DataTable)dgv.DataSource;

                    int Index = dgv.Columns[headerIndex].Index;
                

                    int nameIndex = dgv.Columns[headerPartName].Index;
                    int codeIndex = dgv.Columns[headerPartCode].Index;

                    int stockIndex = dgv.Columns[headerReadyStock].Index;
                 



                  
                    tRange.EntireColumn.AutoFit();

                    xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[stockIndex].InheritedStyle.BackColor);
                

                    for (int j = 0; j <= dtMasterData.Rows.Count - 1; j++)
                    {
                        Range rangeIndex = (Range)xlWorkSheet.Cells[j + 2, Index + 1];
                        
                        Range rangeName = (Range)xlWorkSheet.Cells[j + 2, nameIndex + 1];
                        Range rangeCode = (Range)xlWorkSheet.Cells[j + 2, codeIndex + 1];

                        Range rangeStock = (Range)xlWorkSheet.Cells[j + 2, stockIndex + 1];
                       

                        Range rangeRow = (Range)xlWorkSheet.Rows[j + 2];
                        //rangeRow.Rows.RowHeight = 40;
                        rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                        rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                       

                        string itemCode = dgv.Rows[j].Cells[headerPartCode].Value.ToString();

                        //color empty space
                        if (string.IsNullOrEmpty(itemCode))
                        {
                            rangeRow.Rows.RowHeight = 2;
                            Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[j + 2, 1], xlWorkSheet.Cells[j + 2, xlWorkSheet.UsedRange.Columns.Count]];
                          
                            rng.Interior.Color = Color.Black;
                        }
                        else
                        {
                            rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.ForeColor);
                            rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.ForeColor);

                            rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.BackColor);
                            rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.BackColor);

                            rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[stockIndex].InheritedStyle.BackColor);
                          
                        }

                        string parentColor = dt.Rows[j][headerParentColor].ToString();
                        string type = dt.Rows[j][headerType].ToString();
                        string balType = dt.Rows[j][headerBalType].ToString();

                        //change parent color
                        if (parentColor.Equals(AssemblyMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }
                        else if (parentColor.Equals(InsertMoldingMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }
                        else if (parentColor.Equals(AssemblyAfterProductionMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }

                       

                    }
                   

                    releaseObject(xlWorkSheet);
                    Clipboard.Clear();
                    dgvOEMItemStockCountList.ClearSelection();
                }

            }

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        private void frmForecastReport_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_NEW_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbRemoveForecastInvalidItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvOEMItemStockCountList.DataSource = null;

            if(cbRemoveNoOrderItem.Checked)
            {
                cbRemoveNoDeliveredItem.Checked = true;
            }

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbIncludeTerminated_CheckedChanged(object sender, EventArgs e)
        {
            dgvOEMItemStockCountList.DataSource = null;

        }

        private void cbMainCustomerOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                if (cbMainCustomerOnly.Checked)
                {
                    tool.loadMainCustomerAndAllToComboBox(cmbCustomer);

                }
                else
                {
                    tool.loadCustomerAndALLWithoutOtherToComboBox(cmbCustomer);

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            JumpToNextRow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackToPreviousRow();
        }

        private void txtNameSearch_Enter(object sender, EventArgs e)
        {
            if (txtItemSearch.Text == text.Search_DefaultText)
            {
                txtItemSearch.Text = "";
                txtItemSearch.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNameSearch_Leave(object sender, EventArgs e)
        {
            if (txtItemSearch.Text.Length == 0)
            {
                txtItemSearch.Text = text.Search_DefaultText;
                txtItemSearch.ForeColor = SystemColors.GrayText;

                ItemSearchUIReset();

            }
        }

        private void dgvOEMItemStockCountList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvOEMItemStockCountList;
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 )
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;
                string currentHeader = dgv.Columns[colIndex].Name;
               
                string repeatedRowReference = dgv.Rows[rowIndex].Cells[headerRowReference].Value.ToString();

                try
                {
                    if(currentHeader == headerReadyStock)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[headerPartCode].Value.ToString());
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(repeatedRowReference))
                        {
                            List<string> parentProducts = GetParentProductsFromReferences(dgv, repeatedRowReference);
                            if (parentProducts.Count > 0)
                            {
                                my_menu.Items.Add("").Name = ""; // Separator
                                my_menu.Items.Add("--- Parent Products Using This Child Part ---").Enabled = false;

                                foreach (string parentInfo in parentProducts)
                                {
                                    my_menu.Items.Add(parentInfo).Name = parentInfo;
                                }
                            }
                        }

                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                        contextMenuStrip1 = my_menu;
                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    }
                    


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private List<string> GetParentProductsFromReferences(DataGridView dgv, string repeatedRowReference)
        {
            List<string> parentProducts = new List<string>();

            try
            {
                // Parse the reference string like "(45) (58.22) (59.22)"
                var matches = System.Text.RegularExpressions.Regex.Matches(repeatedRowReference, @"\(([^)]+)\)");

                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    string reference = match.Groups[1].Value;

                    // Check if it's a decimal (contains a dot)
                    if (reference.Contains("."))
                    {
                        // Extract the integer part (before the decimal point)
                        string[] parts = reference.Split('.');
                        if (parts.Length > 0 && int.TryParse(parts[0], out int parentIndex))
                        {
                            // Find the row with this index in the DataGridView
                            string parentProductInfo = FindParentProductByIndex(dgv, parentIndex);
                            if (!string.IsNullOrEmpty(parentProductInfo) && !parentProducts.Contains(parentProductInfo))
                            {
                                parentProducts.Add(parentProductInfo);
                            }
                        }
                    }
                    // Skip integer references like "45" as they are direct delivery products
                }
            }
            catch (Exception ex)
            {
                // Log error if needed, but don't show to user
                System.Diagnostics.Debug.WriteLine($"Error parsing repeated row reference: {ex.Message}");
            }

            return parentProducts;
        }

        // NEW METHOD: Find parent product by index
        private string FindParentProductByIndex(DataGridView dgv, int targetIndex)
        {
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Get the index from the first column (assuming it's the index column)
                    if (row.Cells[0].Value != null)
                    {
                        string indexValue = row.Cells[headerIndex].Value.ToString();

                        // Check if this row's index matches our target
                        if (int.TryParse(indexValue, out int currentIndex) && currentIndex == targetIndex)
                        {
                            // Get the part name and part code for this parent product
                            string partName = row.Cells[headerPartName]?.Value?.ToString() ?? "";
                            string partCode = row.Cells[headerPartCode]?.Value?.ToString() ?? "";

                            if (!string.IsNullOrEmpty(partName) || !string.IsNullOrEmpty(partCode))
                            {
                                return $"Parent: {partName} ({partCode})";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error finding parent product by index: {ex.Message}");
            }

            return string.Empty;
        }

        private void ShowStockLocation(string itemCode)
        {
            ContextMenuStrip my_menu = new ContextMenuStrip();

            my_menu.Items.Add(itemCode + "     " + tool.getItemNameFromDataTable(DT_ITEM, itemCode)).Name = itemCode;
            my_menu.Items.Add("").Name = itemCode;

            DataTable dt = dalStock.Select(itemCode);

            float TotalStock = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    string fac = stock["fac_name"].ToString();
                    float qty = float.TryParse(stock["stock_qty"].ToString(), out float x) ? x : 0;

                    TotalStock += qty;

                    if (qty > 0)
                    {
                        int length = fac.Length;
                        if ((10 - length) >= 0)
                        {
                            for (int i = 1; i <= (10 - length); i++)
                            {
                                fac += " ";
                            }
                        }

                        my_menu.Items.Add(fac + " " + qty.ToString("0.##")).Name = fac;

                    }

                }


                my_menu.Items.Add("---------------------").Name = "Line1";
                my_menu.Items.Add("Total" + "       " + TotalStock.ToString("0.##")).Name = "Total";
                my_menu.Items.Add("---------------------").Name = "Line2";
            }
            else
            {
                string fac = "null";
                string qty = "null";

                my_menu.Items.Add(fac + "   " + qty).Name = fac;
            }

            my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvOEMItemStockCountList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            string itemCode = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[headerPartCode].Value.ToString();
            string customer = cmbCustomer.Text;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

           if (itemClicked.Equals(text.StockLocation))
            {
                ShowStockLocation(itemCode);
            }
            else if (itemClicked.StartsWith("Parent:"))
            {
                HandleParentProductClick(itemClicked);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void HandleParentProductClick(string parentInfo)
        {
            try
            {
                // Extract name and code from the parent info string
                // Format: "Parent: CURVE WAND SET (V0KPBW100)"
                string[] parts = parentInfo.Split('(');
                if (parts.Length >= 2)
                {
                    string nameWithPrefix = parts[0].Trim(); // "Parent: CURVE WAND SET "
                    string name = nameWithPrefix.Replace("Parent:", "").Trim(); // "CURVE WAND SET"
                    string codeWithBracket = parts[1].Trim(); // "V0KPBW100)"
                    string code = codeWithBracket.Replace(")", "").Trim(); // "V0KPBW100"

                    // Create custom dialog with Code/Name buttons
                    Form customDialog = new Form()
                    {
                        Width = 400,
                        Height = 230,  // Increased height for better spacing
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "Copy Parent Product Info",
                        StartPosition = FormStartPosition.CenterScreen,
                        MaximizeBox = false,
                        MinimizeBox = false,
                        BackColor = SystemColors.Control
                    };

                    System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label()
                    {
                        Left = 20,
                        Top = 20,
                        Width = 350,
                        Height = 80,  // Increased height for text
                        Text = $"What would you like to copy to clipboard?\n\nName: {name}\nCode: {code}",
                        Font = new Font("Segoe UI", 9),
                        TextAlign = ContentAlignment.TopLeft
                    };

                    System.Windows.Forms.Button codeButton = new System.Windows.Forms.Button()
                    {
                        Text = "Code",
                        Left = 70,     // Adjusted position
                        Width = 90,    // Slightly wider
                        Height = 35,   // Taller button
                        Top = 120,     // Moved down
                        DialogResult = DialogResult.Yes,
                        Font = new Font("Segoe UI", 9),
                        FlatStyle = FlatStyle.Standard,
                        UseVisualStyleBackColor = true
                    };

                    System.Windows.Forms.Button nameButton = new System.Windows.Forms.Button()
                    {
                        Text = "Name",
                        Left = 170,    // Centered
                        Width = 90,    // Slightly wider
                        Height = 35,   // Taller button
                        Top = 120,     // Moved down
                        DialogResult = DialogResult.No,
                        Font = new Font("Segoe UI", 9),
                        FlatStyle = FlatStyle.Standard,
                        UseVisualStyleBackColor = true
                    };

                    System.Windows.Forms.Button cancelButton = new System.Windows.Forms.Button()
                    {
                        Text = "Cancel",
                        Left = 270,    // Adjusted position
                        Width = 90,    // Slightly wider
                        Height = 35,   // Taller button
                        Top = 120,     // Moved down
                        DialogResult = DialogResult.Cancel,
                        Font = new Font("Segoe UI", 9),
                        FlatStyle = FlatStyle.Standard,
                        UseVisualStyleBackColor = true
                    };

                    customDialog.Controls.Add(textLabel);
                    customDialog.Controls.Add(codeButton);
                    customDialog.Controls.Add(nameButton);
                    customDialog.Controls.Add(cancelButton);

                    DialogResult result = customDialog.ShowDialog();

                    string copiedText = "";

                    if (result == DialogResult.Yes) // Code button
                    {
                        // Copy code
                        Clipboard.SetText(code);
                        copiedText = $"'{code}' copied to clipboard!";
                    }
                    else if (result == DialogResult.No) // Name button
                    {
                        // Copy name
                        Clipboard.SetText(name);
                        copiedText = $"'{name}' copied to clipboard!";
                    }
                    // If Cancel, do nothing

                    if (!string.IsNullOrEmpty(copiedText))
                    {
                        MessageBox.Show(copiedText, "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    customDialog.Dispose(); // Clean up
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing parent product info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void cbRemoveNoDeliveredItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvOEMItemStockCountList.DataSource = null;

            if (cbRemoveNoDeliveredItem.Checked)
            {
                cbRemoveNoOrderItem.Checked = true;
            }

        }
       


        private void lblForecastType_Click(object sender, EventArgs e)
        {
            dgvOEMItemStockCountList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            txtItemSearch.Text = text.Search_DefaultText;
            txtItemSearch.ForeColor = SystemColors.GrayText;
            ItemSearchUIReset();

            lblSearchClear.Visible = false;
            btnFullReport.Focus();
        }


        private void frmForecastReport_NEW_Shown(object sender, EventArgs e)
        {
            if (myconnstrng == text.DB_Semenyih)
            {
                cmbCustomer.Text = "ALL";
            }
        }


        private bool hasStockChanges = false;
        private HashSet<string> changedItemCodes = new HashSet<string>();

        private void dgvOEMItemStockCountList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridView dgv = dgvOEMItemStockCountList;
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            string itemCode = dgv.Rows[e.RowIndex].Cells[headerPartCode].Value?.ToString();

            if (string.IsNullOrEmpty(itemCode)) return;

            try
            {
                if (columnName == headerStdPacking_Qty ||
                    columnName == headerFullPackingQty ||
                    columnName == headerBalance ||
                    columnName == headerRemark)
                {
                    // Mark as changed
                    hasStockChanges = true;
                    changedItemCodes.Add(itemCode);

                    // Get the new value
                    var newValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                    // Update all rows with the same item code
                    int updatedCount = UpdateDuplicateItems(itemCode, columnName, newValue, e.RowIndex);

                    // Recalculate for current row
                    CalculateTotalActualStock(e.RowIndex);
                    CalculateStockDifference(e.RowIndex);

                    // Recalculate for all duplicate rows
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        string rowItemCode = row.Cells[headerPartCode].Value?.ToString();
                        if (rowItemCode == itemCode && row.Index != e.RowIndex)
                        {
                            CalculateTotalActualStock(row.Index);
                            CalculateStockDifference(row.Index);
                        }
                    }

                    // Show simple message if duplicates were updated
                    if (updatedCount > 0)
                    {
                        ShowBriefMessage($"Updated {updatedCount + 1} items", 1500);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in calculation: {ex.Message}");
            }
        }

        // Add method to update duplicate items
        private int UpdateDuplicateItems(string itemCode, string columnName, object newValue, int excludeRowIndex)
        {
            DataGridView dgv = dgvOEMItemStockCountList;
            int updatedCount = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Index == excludeRowIndex) continue; // Skip the row being edited

                string rowItemCode = row.Cells[headerPartCode].Value?.ToString();
                if (rowItemCode == itemCode)
                {
                    // Update the same column for duplicate items
                    row.Cells[columnName].Value = newValue;
                    updatedCount++;
                }
            }

            return updatedCount;
        }

        // Add method to show brief message
        private void ShowBriefMessage(string message, int durationMs)
        {
            // Create a temporary label to show the message
            Label tempLabel = new Label()
            {
                Text = message,
                BackColor = Color.LightYellow,
                ForeColor = Color.DarkBlue,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Padding = new Padding(8, 4, 8, 4)
            };

            // Position it near the currently edited cell
            DataGridViewCell currentCell = dgvOEMItemStockCountList.CurrentCell;
            if (currentCell != null)
            {
                Rectangle cellRect = dgvOEMItemStockCountList.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                Point gridLocation = dgvOEMItemStockCountList.Location;
                Point location = new Point(gridLocation.X + cellRect.Right + 5, gridLocation.Y + cellRect.Top);
                tempLabel.Location = location;
            }
            else
            {
                // Fallback to default position if no current cell
                Point location = new Point(dgvOEMItemStockCountList.Location.X + 10, dgvOEMItemStockCountList.Location.Y + 10);
                tempLabel.Location = location;
            }

            // Add to form
            this.Controls.Add(tempLabel);
            tempLabel.BringToFront();

            // Create timer to hide the message
            Timer hideTimer = new Timer();
            hideTimer.Interval = durationMs;
            hideTimer.Tick += (s, e) =>
            {
                this.Controls.Remove(tempLabel);
                tempLabel.Dispose();
                hideTimer.Stop();
                hideTimer.Dispose();
            };
            hideTimer.Start();
        }

        // Add calculation methods
        private void CalculateTotalActualStock(int rowIndex)
        {
            DataGridView dgv = dgvOEMItemStockCountList;

            try
            {
                // Get values from editable columns
                int stdPackingQty = 0;
                int fullPackingQty = 0;
                decimal balance = 0;

                var stdPackingValue = dgv.Rows[rowIndex].Cells[headerStdPacking_Qty].Value;
                var fullPackingValue = dgv.Rows[rowIndex].Cells[headerFullPackingQty].Value;
                var balanceValue = dgv.Rows[rowIndex].Cells[headerBalance].Value;

                // Check if any required values are null or empty
                bool hasStdPacking = stdPackingValue != null && !string.IsNullOrEmpty(stdPackingValue.ToString()) && int.TryParse(stdPackingValue.ToString(), out stdPackingQty);
                bool hasFullPacking = fullPackingValue != null && !string.IsNullOrEmpty(fullPackingValue.ToString()) && int.TryParse(fullPackingValue.ToString(), out fullPackingQty);
                bool hasBalance = balanceValue != null && !string.IsNullOrEmpty(balanceValue.ToString()) && decimal.TryParse(balanceValue.ToString(), out balance);

                // Only calculate if we have meaningful data for multiplication (std packing AND full packing)
                // OR if we have balance data
                if ((!hasStdPacking || !hasFullPacking) && !hasBalance)
                {
                    // Don't calculate if missing required multiplication values and no balance
                    dgv.Rows[rowIndex].Cells[headerTotalAcutalStock].Value = DBNull.Value;
                    return;
                }

                // Calculate total: (Standard Packing Qty * Full Packing Qty) + Balance
                decimal totalActualStock = (stdPackingQty * fullPackingQty) + balance;

                // Format to 2 decimals, remove trailing zeros
                totalActualStock = Math.Round(totalActualStock, 2);
                if (totalActualStock % 1 == 0)
                    dgv.Rows[rowIndex].Cells[headerTotalAcutalStock].Value = (int)totalActualStock;
                else
                    dgv.Rows[rowIndex].Cells[headerTotalAcutalStock].Value = totalActualStock;
            }
            catch (Exception ex)
            {
                dgv.Rows[rowIndex].Cells[headerTotalAcutalStock].Value = DBNull.Value;
            }
        }

        private void CalculateStockDifference(int rowIndex)
        {
            DataGridView dgv = dgvOEMItemStockCountList;

            try
            {
                // Get system stock and actual stock
                decimal systemStock = 0;
                decimal actualStock = 0;

                var systemStockValue = dgv.Rows[rowIndex].Cells[headerReadyStock].Value;
                var actualStockValue = dgv.Rows[rowIndex].Cells[headerTotalAcutalStock].Value;

                bool hasSystemStock = systemStockValue != null && !string.IsNullOrEmpty(systemStockValue.ToString()) && decimal.TryParse(systemStockValue.ToString(), out systemStock);
                bool hasActualStock = actualStockValue != null && actualStockValue != DBNull.Value && !string.IsNullOrEmpty(actualStockValue.ToString()) && decimal.TryParse(actualStockValue.ToString(), out actualStock);

                // Only calculate difference if we have actual stock calculated
                if (!hasActualStock)
                {
                    // No actual stock calculated - clear the difference field
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Value = DBNull.Value;
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Style.BackColor = Color.White;
                    return;
                }

                // Calculate difference (Actual - System)
                decimal stockDiff = actualStock - systemStock;

                // Format and set value
                stockDiff = Math.Round(stockDiff, 2);
                if (stockDiff % 1 == 0)
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Value = (int)stockDiff;
                else
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Value = stockDiff;

                // Color code the difference
                if (stockDiff > 0)
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Style.BackColor = Color.LightGreen; // Surplus
                else if (stockDiff < 0)
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Style.BackColor = Color.LightCoral; // Shortage
                else
                    dgv.Rows[rowIndex].Cells[headerStockDiff].Style.BackColor = Color.White; // Match

            }
            catch (Exception ex)
            {
                dgv.Rows[rowIndex].Cells[headerStockDiff].Value = DBNull.Value;
                dgv.Rows[rowIndex].Cells[headerStockDiff].Style.BackColor = Color.White;
            }
        }

        // Add cell validation
        private void dgvOEMItemStockCountList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dgvOEMItemStockCountList.Columns[e.ColumnIndex].Name;
            string newValue = e.FormattedValue.ToString();

            // Validate integer columns
            if (columnName == headerStdPacking_Qty || columnName == headerFullPackingQty)
            {
                if (!string.IsNullOrEmpty(newValue) && !int.TryParse(newValue, out _))
                {
                    e.Cancel = true;
                    MessageBox.Show($"{columnName} must be a whole number.");
                }
            }

            // Validate decimal columns
            if (columnName == headerBalance)
            {
                if (!string.IsNullOrEmpty(newValue) && !decimal.TryParse(newValue, out _))
                {
                    e.Cancel = true;
                    MessageBox.Show($"{columnName} must be a valid number.");
                }
            }
        }

        // Add save functionality
        private bool SaveStockCheckChanges()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Here you would implement the actual database save logic
                // This is a placeholder showing the structure

                foreach (DataGridViewRow row in dgvOEMItemStockCountList.Rows)
                {
                    string itemCode = row.Cells[headerPartCode].Value?.ToString();
                    if (string.IsNullOrEmpty(itemCode) || !changedItemCodes.Contains(itemCode)) continue;

                    var stdPackingQty = row.Cells[headerStdPacking_Qty].Value;
                    var fullPackingQty = row.Cells[headerFullPackingQty].Value;
                    var balance = row.Cells[headerBalance].Value;
                    var totalActualStock = row.Cells[headerTotalAcutalStock].Value;
                    var remark = row.Cells[headerRemark].Value?.ToString();

                    // TODO: Implement database update logic here
                    // Example:
                    // dalStock.UpdateStockCount(itemCode, Convert.ToDecimal(totalActualStock), remark);
                }

                MessageBox.Show($"Stock check changes saved successfully for {changedItemCodes.Count} items.");

                hasStockChanges = false;
                changedItemCodes.Clear();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving stock check changes: {ex.Message}");
                return false;
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        
    }

}

