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

//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockCountManagementVer3 : Form
    {
        public frmStockCountManagementVer3()
        {
            InitializeComponent();

            myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            //tool.GetCustItemWithForecast(1, 1, 2020, 3);
            tool.DoubleBuffered(dgvForecastReport, true);

            if (cbMainCustomerOnly.Checked)
            {
                tool.loadMainCustomerAndAllToComboBox(cmbCustomer);
                
            }
            else
            {
                tool.loadCustomerAndALLWithoutOtherToComboBox(cmbCustomer);

            }

            cmbCustomer.SelectedIndex = 0;
        }

        #region variable declare

        static public string myconnstrng;

        enum ColorSet
        {
            Gold = 0,
           
        }

        private List<int> Row_Index_Found;

        int CURRENT_ROW_JUMP = -1;
        private string CELL_EDITING_OLD_VALUE = "";
        private string CELL_EDITING_NEW_VALUE = "";

        private bool CELL_VALUE_CHANGED = false;

        private string textRowFound = " row(s) found";

        private string textSearchFilter = "SEARCH FILTER";
        private string textHideFilter = "HIDE FILTER";

        private string textHideProPlanningFilter = "HIDE PRO PLANNING FILTER";
        private string textShoweProPlanningFilter = "PRO PLANNING FILTER";


        readonly string headerToDo = "TO DO";
        readonly string headerParentColor = "SPECIAL TYPE";
        readonly string headerBackColor = "BACK COLOR";
        readonly string headerForecastType = "FORECAST TYPE";
        readonly string headerBalType = "BAL TYPE";
        readonly string headerToProduce = "TO PRODUCE";
        readonly string headerProduced = "PRODUCED*";
        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerRowReference = "REPEATED ROW";
        readonly string headerItemRemark = "REMARK";

        readonly string headerItemType = "ITEM TYPE";
        readonly string headerRawMat = "RAW MATERIAL";
        readonly string headerPartName = "PART NAME";
        readonly string headerPartCode = "PART CODE";
        readonly string headerColorMat = "COLOR MATERIAL";
        readonly string headerPartWeight = "PART WEIGHT/G (RUNNER)";
        readonly string headerPlannedQty = "PLANNED QTY";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerEstimate = "ESTIMATE";
        string headerForecast1 = "FCST/ NEEDED";
        string headerForecast2 = "FCST/ NEEDED";
        string headerForecast3 = "FCST/ NEEDED";
        string headerForecast4 = "FCST/ NEEDED";

        readonly string headerOut = "OUT";
        readonly string headerOutStd = "OUTSTD";
        string headerBal1 = "BAL";
        string headerBal2 = "BAL";
        string headerBal3 = "BAL";
        string headerBal4 = "BAL";
        string headerProdInfo = "Prod. Info";
        string headerPurchaseInfo = "Purchase Info";

        readonly string headerQuoTon = "QUO TON";
        readonly string headerProTon = "PRO TON";
        readonly string headerCavity = "CAVITY";
        readonly string headerQuoCT = "QUO CT";
        readonly string headerProCT = "PRO CT";
        readonly string headerPWPerShot = "PW/SHOT";
        readonly string headerRWPerShot = "RW/SHOT";
        readonly string headerTotalOut = "TOTAL OUT/2021";
        readonly string headerMonthlyOut = "MONTHLY OUT";

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
        private bool SummaryMode = false;

        private float alertLevel = 0;

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
            dt.Columns.Add(headerForecastType, typeof(string));
            dt.Columns.Add(headerBalType, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerItemType, typeof(string)); 
            dt.Columns.Add(headerIndex, typeof(float));

            if(cmbCustomer.Text.Equals(text.Cmb_All))
            {
                dt.Columns.Add(text.Header_Customer, typeof(string));
            }

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerReadyStock, typeof(float));

            return dt;
        }

        private void DgvForecastReportUIEdit(DataGridView dgv)
        {
            if(dgv != null)
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
              
                dgv.Columns[headerReadyStock].HeaderCell.Style.BackColor = Color.Pink;
                dgv.Columns[headerReadyStock].DefaultCellStyle.BackColor = Color.Pink;

                dgv.Columns[headerPartCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[headerPartName].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
              
                dgv.EnableHeadersVisualStyles = false;
            }
          
        }
        private void ColorData()
        {
            DataTable dt = (DataTable)dgvForecastReport.DataSource;
            DataGridView dgv = dgvForecastReport;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            Font _ParentFont = new Font(dgvForecastReport.Font, FontStyle.Underline);

            if(dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string parentColor = dt.Rows[i][headerParentColor].ToString();
                    string backColor = dt.Rows[i][headerBackColor].ToString();
                    string balType = dt.Rows[i][headerBalType].ToString();
                }
            }
        }

        private void dgvForecastReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvForecastReport;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

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
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        #endregion

        #region UI Function

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();

            string text = btnFilter.Text;

            if (text == textSearchFilter)
            {
                tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 220f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textHideFilter;
            }
            else if (text == textHideFilter)
            {
                tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);

                dgvForecastReport.ResumeLayout();

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
            dgvForecastReport.DataSource = null;

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
            dgvForecastReport.SelectionMode = DataGridViewSelectionMode.CellSelect;

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
                DataGridView dgv = dgvForecastReport;

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

            DataTable dt = (DataTable)dgvForecastReport.DataSource;

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


                    dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);

                    dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                    dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];


                    btnPreviousSearchResult.Enabled = true;

                    //check if last row
                    btnNextSearchResult.Enabled = !(i == last);

                    lblResultNo.Text = "#" + (Row_Index_Found.IndexOf(i)+1).ToString();

                    return;
                }
            }

            if(CURRENT_ROW_JUMP != -1)
            {
                dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];

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
                        dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                        dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                        dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];

                        //check if first row
                        btnPreviousSearchResult.Enabled = !(CURRENT_ROW_JUMP == first);

                        btnNextSearchResult.Enabled = true;

                        lblResultNo.Text = "#" + (i+1).ToString();

                        return;
                    }
               
                }


            if (CURRENT_ROW_JUMP != -1)
            {
                dgvForecastReport.FirstDisplayedScrollingRowIndex = FindParentIndex(CURRENT_ROW_JUMP);
                dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode].Selected = true;
                dgvForecastReport.CurrentCell = dgvForecastReport.Rows[CURRENT_ROW_JUMP].Cells[headerPartCode];
            }
        }

        private void ShowAllCustomerForecastReport()
        {
            SummaryMode = false;

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvForecastReport;

            dgv.DataSource = LoadCustomerItemList();//9562ms-->3464ms (16/3) -> 2537ms (17/3)
           

            if (dgv.DataSource != null)
            {
                ColorData();//720ms (16/3)

                dgvForecastReport.Columns.Remove(headerItemType);

                //if (cbSpecialTypeColorMode.Checked)
                //    dgvForecastReport.Columns.Remove(headerParentColor);

                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);

                DgvForecastReportUIEdit(dgvForecastReport); //973ms (16/3)

                //dgvForecastReport.Columns.Remove(headerItemType);

                //if (cbSpecialTypeColorMode.Checked)
                //    dgvForecastReport.Columns.Remove(headerParentColor);

                //dgvForecastReport.Columns.Remove(headerType);
                //dgvForecastReport.Columns.Remove(headerBackColor);
                //dgvForecastReport.Columns.Remove(headerBalType);
                //dgvForecastReport.Columns.Remove(headerForecastType);

                dgvForecastReport.ResumeLayout();

                dgvForecastReport.ClearSelection();

                 dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

               dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
            }

            frmLoading.CloseForm();
        }

        private void FullForecastReport()
        {
            SummaryMode = false;

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvForecastReport;

            DT_FORECAST_REPORT = LoadCustomerItemList();//9562ms-->3464ms (16/3) -> 2537ms (17/3)

            dgv.DataSource = DT_FORECAST_REPORT;


            if (dgv.DataSource != null)
            {
                ColorData();//720ms (16/3)

                //dgvForecastReport.Columns.Remove(headerItemType);


                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);

                DgvForecastReportUIEdit(dgvForecastReport); //973ms (16/3)

                dgvForecastReport.ResumeLayout();

                dgvForecastReport.ClearSelection();

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
            }

            frmLoading.CloseForm();
        }

        //private void NewFullForecastReport()
        //{
        //    SummaryMode = false;

        //    frmLoading.ShowLoadingScreen();

        //    DataGridView dgv = dgvForecastReport;

        //    //DT_FORECAST_REPORT = NewFullDetailForecastData();//9562ms-->3464ms (16/3) -> 2537ms (17/3)
        //    DT_FORECAST_REPORT = NewFullDetailForecastData(txtItemSearch.Text);
        //    dgv.DataSource = DT_FORECAST_REPORT;


        //    if (dgv.DataSource != null)
        //    {
        //        ColorData();//720ms (16/3)

        //        dgvForecastReport.Columns.Remove(headerItemType);

        //        if (cbSpecialTypeColorMode.Checked)
        //            dgvForecastReport.Columns.Remove(headerParentColor);

        //        dgvForecastReport.Columns.Remove(headerType);
        //        dgvForecastReport.Columns.Remove(headerBackColor);
        //        dgvForecastReport.Columns.Remove(headerBalType);
        //        dgvForecastReport.Columns.Remove(headerForecastType);

        //        DgvForecastReportUIEdit(dgvForecastReport); //973ms (16/3)

        //        dgvForecastReport.ResumeLayout();

        //        dgvForecastReport.ClearSelection();

        //        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //693ms (16/3)

        //        dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable); //781 ms (16/3)
        //    }

        //    frmLoading.CloseForm();
        //}

        private void ShowDetailForecastReport()
        {
            SummaryMode = false;
            frmLoading.ShowLoadingScreen();
            DataGridView dgv = dgvForecastReport;
            dgv.DataSource = SearchForecastData();
     
            if (dgv.DataSource != null)
            {
                ColorData();
                DgvForecastReportUIEdit(dgvForecastReport);
                dgvForecastReport.Columns.Remove(headerItemType);

                
                dgvForecastReport.Columns.Remove(headerType);
                dgvForecastReport.Columns.Remove(headerBackColor);
                dgvForecastReport.Columns.Remove(headerBalType);
                dgvForecastReport.Columns.Remove(headerForecastType);
                dgvForecastReport.ClearSelection();
                dgvForecastReport.ResumeLayout();
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

            //dgvForecastReport.DataSource = null;

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        #endregion

        #region Loading Data

        private void frmForecastReport_NEW_Load(object sender, EventArgs e)
        {
            

            DT_MACHINE_SCHEDULE = dalPlanning.SelectCompletedOrRunningPlan();
            DT_PMMA_DATE = dalPmmaDate.Select();
            DT_JOIN = dalJoin.SelectAllWithChildCat();
         

            DataTable dt_Item_Cust = dalItemCust.SelectAllExcludedOTHER();

            dgvForecastReport.SuspendLayout();

            tlpForecastReport.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
            tlpForecastReport.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);

            dgvForecastReport.ResumeLayout();

            btnFilter.Text = textSearchFilter;

            cmbCustomer.Focus();
            loaded = true;

            string cust = cmbCustomer.Text;
            custChanging = true;
          
            custChanging = false;

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

            DT_ITEM_CUST = RemoveTerminatedItem(DT_ITEM_CUST);

            DT_ITEM_CUST.Columns.Add(text.Header_GotNotPackagingChild);
            //DT_ITEM_CUST.Columns.Add(text.Header_Forecast_1);
            //DT_ITEM_CUST.Columns.Add(text.Header_Forecast_2);
            //DT_ITEM_CUST.Columns.Add(text.Header_Forecast_3);

            if (DT_ITEM_CUST != null)
            {
                DT_JOIN = DT_JOIN != null && DT_JOIN.Rows.Count > 0 ? DT_JOIN : dalJoin.SelectAllWithChildCat();
                DT_ITEM = DT_ITEM != null && DT_ITEM.Rows.Count > 0 ? DT_ITEM : dalItem.Select();

                //var getForecastPeriod = getMonthYearPeriod();
                //DataTable dt_ItemForecast = dalItemForecast.SelectWithRange(getForecastPeriod.Item1, getForecastPeriod.Item2, getForecastPeriod.Item3, getForecastPeriod.Item4);


                foreach (DataRow row in DT_ITEM_CUST.Rows)
                {
                    string itemCode = row[dalItem.ItemCode].ToString();
                    //string custID = row[dalItemCust.CustID].ToString();

                    bool gotNotPackagingChild = tool.ifGotNotPackagingChild(itemCode, DT_JOIN, DT_ITEM);
                    row[text.Header_GotNotPackagingChild] = gotNotPackagingChild;

                    //var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, custID, itemCode, 1, 2, 3);
                    //row[text.Header_Forecast_1] = forecastData.Item1;
                    //row[text.Header_Forecast_2] = forecastData.Item2;
                    //row[text.Header_Forecast_3] = forecastData.Item3;

                }
            }
        }

        private DataTable LoadCustomerItemList()
        {
            #region Setting

            Cursor = Cursors.WaitCursor;

            btnFullReport.Enabled = false;

            DataGridView dgv = dgvForecastReport;

            dgvForecastReport.SuspendLayout();
            dgvForecastReport.DataSource = null;

            DataTable dt_Data = NewForecastReportTable();

            DataRow dt_Row;
            

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

                    if (true)
                    {
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

                        dt_Row[headerReadyStock] = uData.ready_stock;

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

                    if (true)
                    {
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
                        dt_Row[headerReadyStock] = uData.ready_stock;

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
              

                dt_Data = ItemSearch(dt_Data);

                dtMasterData = dt_Data;
            }
            else
            {
                dtMasterData = null;
            }



            btnFullReport.Enabled = true;
     
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

        //                        if (waitingToInsert)
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
        private DataTable SearchForecastData()
        {
            #region pre setting

            Cursor = Cursors.WaitCursor;


            btnFullReport.Enabled = false;
           

            DataGridView dgv = dgvForecastReport;

            dgvForecastReport.SuspendLayout();
            dgvForecastReport.DataSource = null;

            string customer = cmbCustomer.Text;
            DataTable dt_Data = NewForecastReportTable();

            #endregion

            if (!string.IsNullOrEmpty(customer))//29ms
            {
                DataRow dt_Row;

                DataTable dt_Item_Cust = dalItemCust.custSearch(customer);

                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(customer).ToString());

                int index = 1;

                dt_Item_Cust.DefaultView.Sort = "item_name ASC";

                dt_Item_Cust = dt_Item_Cust.DefaultView.ToTable();

                dt_Item_Cust.Columns.Add(text.Header_GotNotPackagingChild);
                //^^^502ms^^^

                #region load single part
                

                foreach (DataRow row in dt_Item_Cust.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);
                   
                    bool gotNotPackagingChild = tool.ifGotNotPackagingChild(uData.part_code, DT_JOIN, DT_ITEM);

                    row[text.Header_GotNotPackagingChild] = gotNotPackagingChild;

                    if (!gotNotPackagingChild)//assembly == 0 && production == 0
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        dt_Row = dt_Data.NewRow();
                       
                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                       
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerReadyStock] = uData.ready_stock;

                        dt_Data.Rows.Add(dt_Row);
                        index++;

                    }
                   
                }

                #endregion
                //^^^755ms,793ms^^^

                #region load assembly part

                foreach (DataRow row in dt_Item_Cust.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;


                    //check if got child part also
                    if ((assembly == 1 || production == 1) && gotNotPackagingChild)//tool.ifGotChild2(uData.part_code, dt_Join)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                       
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);


                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerReadyStock] = uData.ready_stock;

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

                        if (index == 55)
                        {
                            var checkpoint = 1;
                        }

                        //load child
                        LoadChild(dt_Data, uData, 0.1m);
                    }
                }

                #endregion
                //2061ms,1295ms

            }

            if (dt_Data.Rows.Count > 0)
            {

                dt_Data = ItemSearch(dt_Data);

                //dtMasterData = Sorting(dt_Data);

            }
            else
            {
                dtMasterData = null;
            }

          
            
            btnFullReport.Enabled = true;
           

            Cursor = Cursors.Arrow;

            return dtMasterData;

        }

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
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                string rowReference = dt.Rows[i][headerRowReference].ToString();

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
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

                    if(childCode == "V01CAR000")
                    {
                        var checkpoint = 1;
                    }

                    DataRow row_Item = tool.getDataRowFromDataTable(DT_ITEM, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part) || row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_SubMat);

                    if (itemMatch)
                    {
                        float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float i) ? Convert.ToSingle(row[dalJoin.JoinQty].ToString()) : 1;

                        dt_Row = dt_Data.NewRow();

                        uChildData.part_code = row_Item[dalItem.ItemCode].ToString();

                        uChildData.index = (double)index;
                    
                        uChildData.part_name = row_Item[dalItem.ItemName].ToString();
                     
                        uChildData.ready_stock = row_Item[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemStock]);

                        float stock = uChildData.ready_stock;

                        if (stock % 1 > 0)
                        {
                            stock = (float)Math.Round(stock * 100f) / 100f;

                            uChildData.ready_stock = stock;
                        }


                     
                        dt_Row = dt_Data.NewRow();

                      
                    

                      
                        dt_Row[headerIndex] = uChildData.index;
                        dt_Row[headerType] = typeChild;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row_Item[dalItem.ItemCat].ToString();
                        dt_Row[headerPartCode] = uChildData.part_code;
                        dt_Row[headerPartName] = CountZeros(subIndex.ToString()) + uChildData.part_name;

                        dt_Row[headerReadyStock] = uChildData.ready_stock;

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

        #endregion

        private void frmForecastReport_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_NEW_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cmbCustomer.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Please select a customer.");
            //    }
            //    else
            //    {
            //        LoadForecastData();
            //    }
            //}
        }

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cmbCustomer.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Please select a customer.");
            //    }
            //    else
            //    {
            //        LoadForecastData();
            //    }
            //}
        }

        private void txtNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (cmbCustomer.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Please select a customer.");
            //    }
            //    else
            //    {
            //        LoadForecastData();
            //    }
            //}
        }


        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbShowSummary_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            dtMasterData = null;
            dt_SummaryList = null;

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

        private void dgvForecastReport_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvForecastReport;
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
                string prodINfo = dgv.Rows[rowIndex].Cells[headerProdInfo].Value.ToString();
                string purchaseINfo = dgv.Rows[rowIndex].Cells[headerPurchaseInfo].Value.ToString();

                try
                {
                    if(currentHeader == headerReadyStock)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[headerPartCode].Value.ToString());
                    }
                    else if (currentHeader == headerRawMat)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[headerRawMat].Value.ToString());
                    }
                    else if (currentHeader == headerColorMat)
                    {
                        ShowStockLocation(dgv.Rows[rowIndex].Cells[text.Header_ColorMatCode].Value.ToString());
                    }
                    else
                    {
                        my_menu.Items.Add(text.DeliveredSummary).Name = text.DeliveredSummary;
                        my_menu.Items.Add(text.ProductionHistory).Name = text.ProductionHistory;
                        my_menu.Items.Add(text.ForecastRecord).Name = text.ForecastRecord;
                        my_menu.Items.Add(text.JobPlanning).Name = text.JobPlanning;

                        if(!string.IsNullOrEmpty(prodINfo))
                        {
                            my_menu.Items.Add("").Name = "";
                            my_menu.Items.Add(prodINfo).Name = prodINfo;
                        }

                        if (!string.IsNullOrEmpty(purchaseINfo))
                        {
                            my_menu.Items.Add("").Name = "";
                            my_menu.Items.Add(purchaseINfo).Name = purchaseINfo;
                        }

                        //my_menu.Items.Add(text.StockLocation).Name = text.StockLocation;

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
        }



        private void dgvForecastReport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           
        }

        private void dgvForecastReport_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvForecastReport_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            txtItemSearch.Text = text.Search_DefaultText;
            txtItemSearch.ForeColor = SystemColors.GrayText;
            ItemSearchUIReset();

            lblSearchClear.Visible = false;
            btnFullReport.Focus();
        }

        private void dgvForecastReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      

        private void frmForecastReport_NEW_Shown(object sender, EventArgs e)
        {
          
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }
    }

}

