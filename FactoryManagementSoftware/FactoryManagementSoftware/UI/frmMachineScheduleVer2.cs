using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using System.Linq;
using System.Configuration;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

using TextBox = System.Windows.Forms.TextBox;
using Task = System.Threading.Tasks.Task;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;
using Range = Microsoft.Office.Interop.Excel.Range;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;
using System.Collections.Generic;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMachineScheduleVer2 : Form
    {
        public frmMachineScheduleVer2()
        {
            InitializeComponent();
            InitializeData();

           

            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(filterHide);
        }

        public frmMachineScheduleVer2(string itemCode)
        {
            InitializeComponent();
            InitializeData();

            btnNewJob.Hide();
            //btnMatList.Hide();
            btnExcel.Hide();

            txtSearch.Text = itemCode;

            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(filterHide);

            ItemProductionHistoryChecking = true;
        }

        public frmMachineScheduleVer2(bool _fromDailyJobRecord)
        {
            
            InitializeComponent();
            InitializeData();
            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnNewJob.Hide();
            //btnExcel.Hide();
            //btnMatList.Hide();

            
            fromDailyRecord = _fromDailyJobRecord;
            btnExcel.Text = "ADD ITEM";
            btnExcel.Width = 180;
            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300);
            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(filterHide);
        }

        public frmMachineScheduleVer2(string action, int data_1, string data_2)
        {

            InitializeComponent();
            InitializeData();

            btnNewJob.Hide();
            //btnExcel.Hide();
            //btnMatList.Hide();

            btnExcel.Text = action;
            btnExcel.Width = 180;
            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300);
            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(false);
            buttionAction = action;

            if(action == text.DailyAction_ChangePlan)
            {
                ChangePlanToAction = true;

                ITEM_CODE = data_2;
                PLAN_ID = data_1;

                cbCompleted.Checked = true;
                cbCancelled.Checked = true;
                cbSearchByJobNo.Checked = true;
                cbItem.Checked = false;
            }

        }

        #region Variable/ object setting

        userDAL dalUser = new userDAL();

        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        planningActionDAL dalPlanningAction = new planningActionDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        MacDAL dalMac = new MacDAL();
        facDAL dalFac = new facDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();

        habitDAL dalHabit = new habitDAL();

        private int PLAN_ID = -1;
        private string ITEM_CODE = "";
        private string buttionAction = "";
        int userPermission = -1;
     
        private string startDateChecking = null;

        private string CELL_EDITING_OLD_VALUE = "";
        private string CELL_EDITING_NEW_VALUE = "";

        private bool CELL_VALUE_CHANGED = false;

        private bool loaded = false;
        private bool ableLoadData = true;
        private bool filterHide = true;
        private bool fromDailyRecord = false;
        private bool ItemProductionHistoryChecking = false;
        private bool ChangePlanToAction = false;

        private bool requestingStocktake_Part;
        private bool requestingStocktake_RawMat;
        private bool requestingStocktake_ColorMat;

        private DataTable DT_STOCKTAKE_PART;
        private DataTable DT_STOCKTAKE_RAWMAT;
        private DataTable DT_STOCKTAKE_COLORMAT;

        #endregion

        #region UI Setting

        private void HideFilter(bool hide)
        {
            if(hide)
            {
                btnFilter.Text = "Show Filter";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                //tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 85f);
                //tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Percent, 83f);
            }
            else
            {
                btnFilter.Text = "Hide Filter";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Absolute, 120f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Absolute, 45f);
                //tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 62f);
            }
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Fac, typeof(string));
            dt.Columns.Add(text.Header_Mac, typeof(string));
            dt.Columns.Add(text.Header_Status, typeof(string));
            dt.Columns.Add(text.Header_JobNo, typeof(int));
            dt.Columns.Add(text.Header_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ItemNameAndCode, typeof(string));
            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_MaxOutput, typeof(int));
            dt.Columns.Add(text.Header_ProducedQty, typeof(int));
            dt.Columns.Add(text.Header_RawMat_String, typeof(string));
            dt.Columns.Add(text.Header_RawMat_Qty, typeof(string));
            dt.Columns.Add(text.Header_Color, typeof(string));
            dt.Columns.Add(text.Header_ColorMat, typeof(string));
            dt.Columns.Add(text.Header_ColorRate, typeof(int));
            dt.Columns.Add(text.Header_ColorMat_KG, typeof(double));
            dt.Columns.Add(text.Header_Remark, typeof(string));
            dt.Columns.Add(text.Header_Job_Purpose, typeof(string));

            dt.Columns.Add(text.Header_FacID, typeof(int));
            dt.Columns.Add(text.Header_MacID, typeof(int));
            dt.Columns.Add(text.Header_Ori_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_Ori_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_ProductionDay, typeof(int));
            dt.Columns.Add(text.Header_ProductionHourPerDay, typeof(double));
            dt.Columns.Add(text.Header_ProductionHour, typeof(double));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(int));
            dt.Columns.Add(text.Header_Cavity, typeof(int));
            dt.Columns.Add(text.Header_ProPwShot, typeof(double));
            dt.Columns.Add(text.Header_ProRwShot, typeof(double));
            dt.Columns.Add(text.Header_RawMat_1, typeof(string));
            dt.Columns.Add(text.Header_RawMat_2, typeof(string));
            dt.Columns.Add(text.Header_RawMat_1_Ratio, typeof(double));
            dt.Columns.Add(text.Header_RawMat_2_Ratio, typeof(double));

            dt.Columns.Add(text.Header_RawMat_Bag_1, typeof(int));
            dt.Columns.Add(text.Header_RawMat_Bag_2, typeof(int));
            dt.Columns.Add(text.Header_RawMat_KG_1, typeof(double));
            dt.Columns.Add(text.Header_RawMat_KG_2, typeof(double));

            dt.Columns.Add(text.Header_Recycle_Qty, typeof(int));
            dt.Columns.Add(text.Header_ColorMatCode, typeof(string));

            return dt;
        }

        private DataTable NewStocktakeTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Fac, typeof(string));

            dt.Columns.Add(text.Header_ItemDescription, typeof(string));

            dt.Columns.Add(text.Header_Stocktake, typeof(string));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private void dgvScheduleUIEdit(DataGridView dgv)
        {

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
         
            dgv.Columns[text.Header_ColorMatCode].Visible = false;
            dgv.Columns[text.Header_RawMat_1].Visible = false;
            dgv.Columns[text.Header_RawMat_2].Visible = false;
            dgv.Columns[text.Header_ItemCode].Visible = false;
            dgv.Columns[text.Header_ItemName].Visible = false;
            dgv.Columns[text.Header_Recycle_Qty].Visible = false;
            dgv.Columns[text.Header_FacID].Visible = false;
            dgv.Columns[text.Header_MacID].Visible = false;
            dgv.Columns[text.Header_Ori_DateStart].Visible = false;
            dgv.Columns[text.Header_Ori_EstDateEnd].Visible = false;
            dgv.Columns[text.Header_ProductionDay].Visible = false;
            dgv.Columns[text.Header_ProductionHourPerDay].Visible = false;
            dgv.Columns[text.Header_ProductionHour].Visible = false;
            dgv.Columns[text.Header_ItemName].Visible = false;
            dgv.Columns[text.Header_ItemCode].Visible = false;
            dgv.Columns[text.Header_ProCT].Visible = false;
            dgv.Columns[text.Header_Cavity].Visible = false;
            dgv.Columns[text.Header_ProPwShot].Visible = false;
            dgv.Columns[text.Header_ProRwShot].Visible = false;
            dgv.Columns[text.Header_RawMat_1].Visible = false;
            dgv.Columns[text.Header_RawMat_2].Visible = false;
            dgv.Columns[text.Header_RawMat_Bag_1].Visible = false;
            dgv.Columns[text.Header_RawMat_Bag_2].Visible = false;
            dgv.Columns[text.Header_RawMat_KG_1].Visible = false;
            dgv.Columns[text.Header_RawMat_KG_2].Visible = false;
            dgv.Columns[text.Header_Recycle_Qty].Visible = false;
            dgv.Columns[text.Header_ColorMatCode].Visible = false;
            dgv.Columns[text.Header_RawMat_1_Ratio].Visible = false;
            dgv.Columns[text.Header_RawMat_2_Ratio].Visible = false;
            dgv.Columns[text.Header_ColorRate].Visible = false;


            Color normalColor = Color.Black;

            Color importantColor = Color.OrangeRed;

            dgv.Columns[text.Header_Mac].DefaultCellStyle.ForeColor = importantColor;

            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[text.Header_ItemNameAndCode].MinimumWidth = 180;
            dgv.Columns[text.Header_ItemNameAndCode].Frozen = true;



            //dgv.Columns[text.Header_RawMat_String].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgv.Columns[text.Header_RawMat_String].MinimumWidth = 100;
            dgv.Columns[text.Header_RawMat_String].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[text.Header_TargetQty].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[text.Header_ProducedQty].DefaultCellStyle.ForeColor = Color.Green;

            dgv.Columns[text.Header_RawMat_Qty].DefaultCellStyle.ForeColor = importantColor;

            dgv.Columns[text.Header_ColorMat_KG].DefaultCellStyle.ForeColor = importantColor;

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

            dgv.Columns[text.Header_Job_Purpose].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            dgv.Columns[text.Header_Mac].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_TargetQty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_RawMat_Qty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_ProducedQty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_ColorMat_KG].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            dgv.Columns[text.Header_Remark].MinimumWidth = 100;
            dgv.Columns[text.Header_Job_Purpose].MinimumWidth = 120;
            dgv.Columns[text.Header_Fac].MinimumWidth = 70;


            dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        #endregion

        #region Load Data

        private void InitializeData()
        {
            tool.loadProductionFactory(cmbMacLocation);
            loadMachine(cmbMac);
            ResetData();

            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_FOUR)
            {
                btnNewJob.Show();
                cbDraft.Checked = true;
                cbDraft.Enabled = true;
            }
            else
            {
                btnNewJob.Hide();
                cbDraft.Checked = false;
                cbDraft.Enabled = false;
            }
        }

        private List<Tuple<int, string, int, string>> MachineList = new List<Tuple<int, string, int, string>>();
        private void loadMachine(ComboBox cmb)
        {
            string macSelected = "";

            if (cmbMac.SelectedItem != null)
            {
                macSelected = cmbMac.Text;
            }

            DataRowView drv = (DataRowView)cmbMacLocation.SelectedItem;

            String valueOfItem = drv["fac_name"].ToString();

            string fac = valueOfItem;

            DataTable dt_Mac = dalMac.Select();

            DataTable dt = new DataTable();
            dt.Columns.Add(dalMac.MacID, typeof(int));
            dt.Columns.Add(dalMac.MacName, typeof(string));

            if (string.IsNullOrEmpty(fac) || fac.ToUpper().Equals(text.Cmb_All.ToUpper()))
            {
                //load All active machine
                facDAL dalFac = new facDAL();

                DataTable dt_Fac = (DataTable)cmbMacLocation.DataSource;

                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocationName].ToString();

                    foreach (DataRow facRow in dt_Fac.Rows)
                    {
                        string facName = facRow[dalFac.FacName].ToString();
                        string facID = facRow[dalFac.FacID].ToString();

                        if (macLocation == facName)
                        {
                            dt.Rows.Add(row[dalMac.MacID], row[dalMac.MacName]);

                            Tuple<int, string, int, string> machine = new Tuple<int, string, int, string>(Convert.ToInt32(facID), facName, Convert.ToInt32(row[dalMac.MacID]), row[dalMac.MacName].ToString());
                            MachineList.Add(machine);
                        }
                    }
                }

            }
            else
            {
                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocationName].ToString();

                    if (macLocation == fac)
                    {
                        dt.Rows.Add(row[dalMac.MacID], row[dalMac.MacName]);

                    }
                }
            }

            DataTable distinctTable = dt.DefaultView.ToTable(true, dalMac.MacID, dalMac.MacName);
            distinctTable.DefaultView.Sort = dalMac.MacID + " ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = dalMac.MacName;

            if (macSelected == "")
            {
                cmb.SelectedIndex = -1;
            }
            else
            {
                cmb.Text = macSelected;

            }
        }

        private DataTable specialDataSort(DataTable dt)
        {
            DataTable sortedDt = dt.Clone();
            string Fac = null;
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    string newFac = row[dalMac.MacLocationName].ToString();
                    if (Fac != newFac)
                    {
                        foreach (DataRow row2 in dt.Rows)
                        {
                            if (row2.RowState != DataRowState.Deleted)
                            {
                                string FacSearch = row2[dalMac.MacLocationName].ToString();

                                if (Fac == FacSearch)
                                {
                                    DataRow newRow2 = sortedDt.NewRow();
                                    newRow2 = row2;
                                    sortedDt.ImportRow(newRow2);
                                    row2.Delete();
                                }
                            }


                        }
                    }

                    DataRow newRow = sortedDt.NewRow();
                    newRow = row;
                    sortedDt.ImportRow(newRow);
                    row.Delete();
                    Fac = newFac;
                }

            }

            return sortedDt;
        }
        private void MatSummaryReset()
        {
            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            MatSummaryColoring(dgvMacSchedule,"",  true, true);

        }

        DataTable DT_ITEM;

        private void New_LoadMacSchedule()
        {
            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            if(DT_ITEM == null || DT_ITEM.Rows.Count <= 0) 
            {
                DT_ITEM = dalItem.Select();
            }

            #region Search Filtering

            DataTable dt_JobPlan;
            DateTime startEarliest = DateTime.MinValue, endLatest = DateTime.MinValue;

            string Keywords = txtSearch.Text;

            bool GetCompletedPlanOnly = !cbDraft.Checked && !cbPending.Checked && !cbRunning.Checked && !cbWarning.Checked && !cbCancelled.Checked && cbCompleted.Checked;


            if (ChangePlanToAction)
            {
                txtSearch.Text = ITEM_CODE;

                Keywords = ITEM_CODE;

                cbItem.Checked = true;
                cbSearchByJobNo.Checked = false;
                cbPending.Checked = true;
                cbCancelled.Checked = true;
                cbCompleted.Checked = true;
                cbWarning.Checked = false;
                cbRunning.Checked = true;
            }

            if (!string.IsNullOrEmpty(Keywords))
            {
                if (cbItem.Checked)
                {
                    //search item
                    dt_JobPlan = dalPlanning.itemSearch(Keywords);
                }
                else
                {
                    //search id
                    dt_JobPlan = dalPlanning.idSearch(Keywords);
                }
            }
            else
            {
                dt_JobPlan = dalPlanning.Select();

            }

            dt_JobPlan = specialDataSort(dt_JobPlan);

            #endregion

            DataTable dt_Schedule = NewScheduleTable();

            DataRow row_Schedule;

            bool match = true;

            string previousLocationID = null;
            string previousMachineID = null;

            bool OneFactorySeaching = false;

            if (!cmbMacLocation.Text.Equals("All") && !string.IsNullOrEmpty(cmbMacLocation.Text) && string.IsNullOrEmpty(cmbMac.Text))
            {
                OneFactorySeaching = true;
            }

            foreach (DataRow row in dt_JobPlan.Rows)
            {
                match = true;

                #region Status Filtering

                string status = row[dalPlanning.planStatus].ToString();

                if (!(cbSearchByJobNo.Checked && !string.IsNullOrEmpty(Keywords)))
                {
                    if (string.IsNullOrEmpty(status))
                    {
                        match = false;
                        continue;
                    }

                    if (!cbPending.Checked && status.Equals(text.planning_status_pending))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbDraft.Checked && status.Equals(text.planning_status_draft))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbRunning.Checked && status.Equals(text.planning_status_running))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbWarning.Checked && status.Equals(text.planning_status_warning))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbCancelled.Checked && status.Equals(text.planning_status_cancelled))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbCompleted.Checked && status.Equals(text.planning_status_completed))
                    {
                        match = false;
                        continue;

                    }
                }


                #endregion

                #region Period Filtering

                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                DateTime from = dtpFrom.Value;
                DateTime to = dtpTo.Value;

                if (!(cbSearchByJobNo.Checked && !string.IsNullOrEmpty(Keywords)))
                {
                    if (!GetCompletedPlanOnly && !((start.Date >= from.Date && start.Date <= to.Date) || (end.Date >= from.Date && end.Date <= to.Date)))
                    {
                        match = false;
                        continue;

                    }
                    else if (GetCompletedPlanOnly && !(end.Date >= from.Date && end.Date <= to.Date))
                    {
                        match = false;
                        continue;

                    }
                }

                #endregion

                #region Location Filtering

                string machineIDFilter = "";
                string machineNameFilter = cmbMac.Text;
                string factoryNameFilter = cmbMacLocation.Text;
                string factoryIDFilter = "";

                if (cmbMacLocation.SelectedIndex > -1)
                {
                    DataRowView drv_Fac = (DataRowView)cmbMacLocation.SelectedItem;

                    factoryIDFilter = drv_Fac[dalFac.FacID].ToString();
                }

                if (cmbMac.SelectedIndex > -1)
                {
                    DataRowView drv = (DataRowView)cmbMac.SelectedItem;
                    machineIDFilter = drv[dalMac.MacID].ToString();
                }

                string factoryName = row[dalMac.MacLocationName].ToString();
                string factoryID = row[dalMac.MacLocationID].ToString();

                string machineID = row[dalMac.MacID].ToString();
                string machineName = row[dalMac.MacName].ToString();

                if (!factoryNameFilter.Equals("All") && !string.IsNullOrEmpty(factoryNameFilter))
                {
                    if (!factoryName.Equals(factoryNameFilter))
                    {
                        match = false;
                        continue;

                    }

                    if (!string.IsNullOrEmpty(machineIDFilter))
                    {
                        if (!machineID.Equals(machineIDFilter))
                        {
                            match = false;
                            continue;

                        }
                    }
                }

                #endregion

                if (match)
                {
                    #region Data Checking 

                    string factoryDivider = "";
                    string machineDivider = "";

                    if (startEarliest == DateTime.MinValue)
                    {
                        startEarliest = start;
                    }
                    else if (start < startEarliest)
                    {
                        startEarliest = start;
                    }

                    if (endLatest == DateTime.MinValue)
                    {
                        endLatest = end;
                    }
                    else if (endLatest < end)
                    {
                        endLatest = end;
                    }

                    if (previousLocationID == null)
                    {
                        previousLocationID = factoryID;
                    }
                    else if (!previousLocationID.Equals(factoryID))
                    {
                        factoryDivider = "factoryDivider";
                        previousLocationID = factoryID;
                    }

                    if (previousMachineID == null)
                    {
                        previousMachineID = machineID;
                    }
                    else if (!previousMachineID.Equals(machineID))
                    {
                        //if (OneFactorySeaching)
                        //{
                        //    dt_Schedule.Rows.Add(dt_Schedule.NewRow());
                        //}

                        machineDivider = "machineDivider";
                        previousMachineID = machineID;
                    }

                    if(!string.IsNullOrEmpty(machineDivider) || !string.IsNullOrEmpty(factoryDivider))
                    {
                        row_Schedule = dt_Schedule.NewRow();
                        row_Schedule[text.Header_ItemNameAndCode] = factoryDivider + machineDivider;
                        dt_Schedule.Rows.Add(row_Schedule);
                    }
                   

                    #endregion

                    #region Load to table 
                    row_Schedule = dt_Schedule.NewRow();

                    row_Schedule[text.Header_MouldCode] = row[dalPlanning.planMouldCode];
                    row_Schedule[text.Header_JobNo] = row[dalPlanning.jobNo];
                    row_Schedule[text.Header_DateStart] = row[dalPlanning.productionStartDate];
                    row_Schedule[text.Header_EstDateEnd] = row[dalPlanning.productionEndDate];
                    row_Schedule[text.Header_Ori_DateStart] = row[dalPlanning.productionStartDate];
                    row_Schedule[text.Header_Ori_EstDateEnd] = row[dalPlanning.productionEndDate];
                    row_Schedule[text.Header_Fac] = factoryName;
                    row_Schedule[text.Header_FacID] = int.TryParse(factoryID, out int x)?x : 0;
                    row_Schedule[text.Header_Mac] = machineName;
                    row_Schedule[text.Header_MacID] = int.TryParse(machineID, out  x) ? x : 0;

                    string itemName = row[dalItem.ItemName].ToString();
                    string itemCode = row[dalItem.ItemCode].ToString();
                    string itemCodePresent = row[dalItem.ItemCodePresent].ToString();

                    if (string.IsNullOrEmpty(itemCodePresent))
                    {
                        itemCodePresent = itemCode;
                    }


                    string itemNameAndCodeString = itemName + " (" + itemCodePresent + ")";
                    if(itemName == itemCodePresent)
                    {
                        itemNameAndCodeString = itemName;
                    }

                    row_Schedule[text.Header_ItemName] = itemName;
                    row_Schedule[text.Header_ItemCode] = itemCode;
                    row_Schedule[text.Header_ItemNameAndCode] = itemNameAndCodeString;
                    int cycleTime = int.TryParse(row[dalPlanning.planCT].ToString(), out x) ? x : 0;
                    if(cycleTime == 0)
                    {
                        cycleTime = int.TryParse(row[dalItem.ItemProCTTo].ToString(), out x) ? x : 0;
                    }
                    row_Schedule[text.Header_ProCT] = cycleTime;
                    row_Schedule[text.Header_Job_Purpose] = row[dalPlanning.productionPurpose];

                    row_Schedule[text.Header_TargetQty] = row[dalPlanning.targetQty];
                    row_Schedule[text.Header_MaxOutput] = row[dalPlanning.ableQty];

                    if (row[dalPlanning.planProduced].ToString() != "0")
                        row_Schedule[text.Header_ProducedQty] = row[dalPlanning.planProduced];

                    string rawMat_1 = row[dalPlanning.materialCode].ToString();
                    string rawMat_2 = row[dalPlanning.materialCode2].ToString();

                    double rawMat_1_ratio = double.TryParse(row[dalPlanning.rawMatRatio_1].ToString(), out rawMat_1_ratio) ? rawMat_1_ratio : 0;
                    double rawMat_2_ratio = double.TryParse(row[dalPlanning.rawMatRatio_2].ToString(), out rawMat_2_ratio) ? rawMat_2_ratio : 0;

                    string rawMix = "";

                    rawMat_1_ratio = rawMat_1_ratio < 1 ? rawMat_1_ratio * 100 : rawMat_1_ratio;
                    rawMat_2_ratio = rawMat_2_ratio < 1 ? rawMat_2_ratio * 100 : rawMat_2_ratio;

                    if (!string.IsNullOrEmpty(rawMat_1) && !string.IsNullOrEmpty(rawMat_2))
                    {
                        rawMix = rawMat_1 + "(" + rawMat_1_ratio +"%)" + " + " + rawMat_2 + "(" + rawMat_2_ratio + "%)";
                    }
                    else
                    {
                        rawMix = rawMat_1;
                    }

                    row_Schedule[text.Header_RawMat_1] = rawMat_1;
                    row_Schedule[text.Header_RawMat_2] = rawMat_2;
                    row_Schedule[text.Header_RawMat_String] = rawMix;

                    int rawBag_1 = int.TryParse(row[dalPlanning.materialBagQty_1].ToString(), out rawBag_1) ? rawBag_1 : 0;
                    int rawBag_2 = int.TryParse(row[dalPlanning.materialBagQty_2].ToString(), out rawBag_2) ? rawBag_2 : 0;

                    double rawKG_1 = double.TryParse(row[dalPlanning.rawMaterialQty_1].ToString(), out rawKG_1) ? rawKG_1 : 0;
                    double rawKG_2 = double.TryParse(row[dalPlanning.rawMaterialQty_2].ToString(), out rawKG_2) ? rawKG_2 : 0;

                    if(rawKG_1 == 0 && rawBag_1 > 0)
                    {
                        rawKG_1 = rawBag_1 * 25;
                    }

                    if (rawKG_2 == 0 && rawBag_2 > 0)
                    {
                        rawKG_2 = rawBag_2 * 25;

                    }
                    string rawMatQty = rawKG_1 + " KG (" + rawBag_1 + " bags)";

                    
                    if(rawKG_2> 0)
                    {
                        double totalRawKG = rawKG_1 + rawKG_2;

                        rawMatQty = totalRawKG + " KG (" + rawKG_1 + " kg + " + rawKG_2+ " kg)";
                    }

                    row_Schedule[text.Header_RawMat_Bag_1] = rawBag_1;
                    row_Schedule[text.Header_RawMat_Bag_2] = rawBag_2;
                    row_Schedule[text.Header_RawMat_KG_1] = rawKG_1;
                    row_Schedule[text.Header_RawMat_KG_2] = rawKG_2;

                    row_Schedule[text.Header_RawMat_Qty] = rawMatQty;

                    row_Schedule[text.Header_Recycle_Qty] = row[dalPlanning.materialRecycleUse];

                    string colorMatCode = row[dalPlanning.colorMaterialCode].ToString();
                    string colorMatName = tool.getItemNameFromDataTable(DT_ITEM, colorMatCode);

                    colorMatName = string.IsNullOrEmpty(colorMatName) ? colorMatCode : colorMatName;

                    //row_Schedule[headerColor] = row[dalItem.ItemColor];
                    row_Schedule[text.Header_Color] = row[dalItem.ItemColor];
                    row_Schedule[text.Header_ColorMatCode] = colorMatCode;
                    row_Schedule[text.Header_ColorMat] = colorMatName;
                    row_Schedule[text.Header_ColorMat_KG] = row[dalPlanning.colorMaterialQty];
                    row_Schedule[text.Header_ColorRate] = row[dalPlanning.colorMaterialUsage];

                    row_Schedule[text.Header_Remark] = row[dalPlanning.planNote];
                    row_Schedule[text.Header_Status] = row[dalPlanning.planStatus];

                    row_Schedule[text.Header_ProductionDay] = row[dalPlanning.productionDay];
                    row_Schedule[text.Header_ProductionHourPerDay] = row[dalPlanning.productionHourPerDay];
                    row_Schedule[text.Header_ProductionHour] = row[dalPlanning.productionHour];
                    row_Schedule[text.Header_Cavity] = row[dalPlanning.planCavity];
                    row_Schedule[text.Header_ProPwShot] = row[dalPlanning.planPW];
                    row_Schedule[text.Header_ProRwShot] = row[dalPlanning.planRW];

                    dt_Schedule.Rows.Add(row_Schedule);

                    #endregion
                }

            }

            dgvMacSchedule.DataSource = null;

            if (dt_Schedule.Rows.Count > 0)
            {
                if (!GetCompletedPlanOnly)
                {
                    if (startEarliest != DateTime.MinValue)
                    {
                        dtpFrom.Value = startEarliest;
                    }

                    if (endLatest != DateTime.MinValue)
                    {
                        dtpTo.Value = endLatest;
                    }
                }

                //Idle Search
                dgvMacSchedule.DataSource = dt_Schedule;
                dgvScheduleUIEdit(dgvMacSchedule);
                MacScheduleListCellFormatting(dgvMacSchedule);
                dgvMacSchedule.ClearSelection();
            }
        }

        private void loadMaterialSummary(string Material)
        {
            float TotalMatPlannedToUse = 0;
            float TotalMatUsed = 0;
            float TotalMatToUse = 0;
            float stock = 0;

            bool rawType = false;
            bool colorType = false;

            int planCounter = 0;
            DataTable dt;
         

            var matSummary = tool.loadMaterialPlanningSummary(Material);
            planCounter = matSummary.Item1;
            TotalMatPlannedToUse = matSummary.Item2;
            TotalMatUsed = matSummary.Item3;
            TotalMatToUse = matSummary.Item4;
            stock = matSummary.Item5;
            rawType = matSummary.Item6;
            colorType = matSummary.Item7;

            lblTotalPlannedToUse.Text = "Total Planned ( " + planCounter +" ) To Use : " + TotalMatPlannedToUse.ToString("0.###") + " kg  ( " + (TotalMatPlannedToUse/25).ToString("0.###") + " bag(s)  )";
            lblTotalUsed.Text = "Total Mat. Used : " + TotalMatUsed.ToString("0.###") + " kg  ( " + (TotalMatUsed / 25).ToString("0.###") + " bag(s)  )";
            lblTotalToUse.Text = "Total Mat. To Use : " + TotalMatToUse.ToString("0.###") + " kg  ( " + (TotalMatToUse / 25).ToString("0.###") + " bag(s)  )";

            //get stock qty

            lblMatStock.Text = "Stock : " + stock.ToString("0.###") + " kg  ( " + (stock / 25).ToString("0.###") + " bag(s)  )";

            if(stock < TotalMatToUse)
            {
                lblMatStock.ForeColor = Color.Red;
            }
            else
            {
                lblMatStock.ForeColor = Color.Green;

            }
            MatSummaryColoring(dgvMacSchedule,Material, rawType, colorType);
        }

        private void MatSummaryColoring(DataGridView dgv , string materialCode, bool rawMode, bool colorMode)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;

            if(rawMode && colorMode)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    if (string.IsNullOrEmpty(row[text.Header_ItemCode].ToString()))
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.FromArgb(70,70,70);
                        dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.FromArgb(70,70,70);
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.Gainsboro;
                        dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.Gainsboro;
                    }
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    string matCode_DGV = "";
                    string headerName = "";

                    if (rawMode)
                    {
                        headerName = text.Header_RawMat_1;

                    }
                    else if (colorMode)
                    {
                        headerName = text.Header_ColorMatCode;
                    }

                    matCode_DGV = row[headerName].ToString();

                    if (rawMode)
                    {
                        headerName = text.Header_RawMat_String;
                    }
                    else if (colorMode)
                    {
                        headerName = text.Header_ColorMat;
                    }

                    if (matCode_DGV == materialCode)
                    {
                        dgv.Rows[rowIndex].Cells[headerName].Style.BackColor = Color.OrangeRed;

                        if (rawMode)
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.Gainsboro;
                        }
                        else if (colorMode)
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.Gainsboro;

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(row[text.Header_ItemCode].ToString()))
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.FromArgb(70,70,70);
                            dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.FromArgb(70,70,70);
                        }
                        else
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.Gainsboro;
                            dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
            }
            

            dgv.ResumeLayout();
        }


        private bool checkIfFamilyMould(int planID, int planIDtoCompare)
        {
            string familyID_1 = "";
            string familyID_2 = "";
            bool result = false;

            DataTable dt_Plan = dalPlanning.Select();

            foreach(DataRow row in dt_Plan.Rows)
            {
                string planID_DB = row[dalPlanning.jobNo].ToString();

                if(planID_DB == planID.ToString())
                {
                    familyID_1 = row[dalPlanning.familyWith].ToString();
                }

                if (planID_DB == planIDtoCompare.ToString())
                {
                    familyID_2 = row[dalPlanning.familyWith].ToString();
                }
            }

            if(familyID_1 != "" && familyID_1 != "-1" && familyID_1 == familyID_2)
            {
                result = true;
            }

            return result;
        }


        private void frmMachineSchedule_Load(object sender, EventArgs e)
        {
            dgvMacSchedule.ClearSelection();

            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            loaded = true;
            //loadMachine();
            cmbMacLocation.SelectedIndex = 0;

            if (ItemProductionHistoryChecking)
            {
                cbCompleted.Checked = true;
                cbPending.Checked = true;

                dtpFrom.Value = new DateTime(2019, 01, 01);
                dtpTo.Value = DateTime.Today;
            }

            New_LoadMacSchedule();

            if(fromDailyRecord)
            {
                dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            else
            {
                dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

            }

           

        }

        #endregion

        #region Button Click to Plan Page

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            frmPlanningNEW frm = new frmPlanningNEW();

            // frmPlanning frm = new frmPlanning();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            New_LoadMacSchedule();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";
            string title = "MouldChangePlan";
            DateTime currentDate = DateTime.Now;

            fileName = title + "_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (fromDailyRecord && MessageBox.Show("Are you sure you want to add this item to the list?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = false;
                foreach(DataGridViewRow row in dgvMacSchedule.SelectedRows)
                {
                    string planID = row.Cells[text.Header_JobNo].Value.ToString();
                    int planID_INT = -1;

                    if(!string.IsNullOrEmpty(planID) && int.TryParse(planID, out planID_INT))
                    {
                        uPlanning.plan_id = planID_INT;
                        uPlanning.recording = true;

                        success = dalPlanning.RecordingUpdate(uPlanning);
                    }
                }

                if(success)
                MessageBox.Show("Item(s) added!");
                Close();
            }

            else if (buttionAction == text.DailyAction_ChangePlan && MessageBox.Show("Confirm to change plan ID?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                foreach (DataGridViewRow row in dgvMacSchedule.SelectedRows)
                {
                    string planID = row.Cells[text.Header_JobNo].Value.ToString();
                    string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();

                    int planID_INT = -1;

                    if (!string.IsNullOrEmpty(planID) && int.TryParse(planID, out planID_INT))
                    {
                       if(planID_INT != -1 && itemCode == ITEM_CODE && planID_INT != PLAN_ID)
                        {
                            ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
                            ProductionRecordBLL uProRecord = new ProductionRecordBLL();

                            uProRecord.new_plan_id = planID_INT;
                            uProRecord.old_plan_id = PLAN_ID;
                            uProRecord.updated_date = DateTime.Now;
                            uProRecord.updated_by = MainDashboard.USER_ID;

                            if(dalProRecord.ChangePlanID(uProRecord))
                            {
                                //change transfer record plan id remark
                                ChangeTransferRecordProductionPlanIDRemark(PLAN_ID.ToString(), planID_INT.ToString());

                                MessageBox.Show("Plan ID Changed!");

                                Close();
                            }

                        }
                       else if(planID_INT == PLAN_ID)
                       {
                            MessageBox.Show("Cannot select plan with same ID !");
                        }
                        else if(itemCode != ITEM_CODE)
                        {
                            MessageBox.Show("Item not match!");
                        }
                    }
                }
                
            }
            else
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                frmMacScheduleExcelDownload frm = new frmMacScheduleExcelDownload();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                Cursor = Cursors.Arrow; // change cursor to normal type

                if(frmMacScheduleExcelDownload.settingApplied)
                {
                    bool requestingMacSchedule = frmMacScheduleExcelDownload.RequestingMachineSchedule;

                    if(requestingMacSchedule)
                    {
                        try
                        {
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type


                            SaveFileDialog sfd = new SaveFileDialog();
                            string path = @"D:\StockAssistant\Document\MouldChangeReport";
                            Directory.CreateDirectory(path);
                            sfd.InitialDirectory = path;
                            sfd.Filter = "Excel Documents (*.xls)|*.xls";
                            sfd.FileName = setFileName();

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                                // Copy DataGridView results to clipboard
                                copyAlltoClipboard();
                                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                object misValue = System.Reflection.Missing.Value;
                                Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                                xlexcel.PrintCommunication = false;
                                xlexcel.ScreenUpdating = false;
                                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                                xlexcel.Calculation = XlCalculation.xlCalculationManual;
                                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                                DateTime dateNow = DateTime.Now;
                                xlWorkSheet.Name = "MOULD CHANGE PLAN ";


                                #region Save data to Sheet

                                string title = "MOULD CHANGE PLAN " + dateNow.ToString("dd/MM/yyyy");

                                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 " + title;

                                string date = dateNow.ToString("dd/MM/yyyy hh:mm:ss");
                                //Header and Footer setup
                                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dateNow.ToString("dd/MM/yyyy");
                                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                                xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " At " + date;

                                //Page setup
                                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;


                                xlWorkSheet.PageSetup.Zoom = false;
                                xlWorkSheet.PageSetup.CenterHorizontally = true;

                                xlWorkSheet.PageSetup.FitToPagesWide = 1;
                                xlWorkSheet.PageSetup.FitToPagesTall = false;

                                double pointToCMRate = 0.035;
                                xlWorkSheet.PageSetup.TopMargin = 1.5 / pointToCMRate;
                                xlWorkSheet.PageSetup.BottomMargin = 1.0 / pointToCMRate;
                                xlWorkSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                                xlWorkSheet.PageSetup.FooterMargin = 1.0 / pointToCMRate;
                                xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                                xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                                xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                                xlexcel.PrintCommunication = true;
                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;
                                // Paste clipboard results to worksheet range
                                xlWorkSheet.Select();
                                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                                CR.Select();
                                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                                //content edit
                                Range tRange = xlWorkSheet.UsedRange;
                                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                                tRange.Borders.Weight = XlBorderWeight.xlMedium;
                                tRange.Font.Size = 12;
                                tRange.Font.Name = "Calibri";
                                tRange.EntireColumn.AutoFit();
                                tRange.EntireRow.AutoFit();

                                tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                                #endregion

                                DataTable dt_test = (DataTable)dgvMacSchedule.DataSource;

                                //int row = dgvSchedule.RowCount - 1;
                                //int col = dgvSchedule.ColumnCount - 1;

                                //MessageBox.Show("row: " + row + " col: " + col);
                                if (true)//cmbSubType.Text.Equals("PMMA")
                                {
                                    for (int i = 0; i <= dgvMacSchedule.RowCount - 1; i++)
                                    {
                                        for (int j = 0; j <= dgvMacSchedule.ColumnCount - 1; j++)
                                        {
                                            Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];
                                            range.Rows.RowHeight = 30;
                                            range.Font.Color = ColorTranslator.ToOle(dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.ForeColor);

                                            if (j == 3 || j == 6 || j == 7 || j == 9 || j == 12 || j == 15 || j == 11 || j == 14)
                                            {
                                                range.Cells.Font.Bold = true;
                                                range.Font.Size = 16;
                                                range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                            }

                                            if (j == 4 || j == 5)
                                            {
                                                range.ColumnWidth = 10;
                                            }

                                            if (j == 6 || j == 7 || j == 11 || j == 14 || j == 16)
                                            {
                                                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                                range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                            }
                                            else
                                            {
                                                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                            }

                                            if (dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Gainsboro)
                                            {
                                                range.Interior.Color = Color.White;
                                                range.Rows.RowHeight = 40;
                                            }
                                            else if (dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.FromArgb(70,70,70))
                                            {
                                                range.Rows.RowHeight = 4;
                                                range.Interior.Color = Color.Black;
                                            }
                                            else
                                            {
                                                range.Interior.Color = ColorTranslator.ToOle(dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.BackColor);
                                                range.Rows.RowHeight = 40;
                                            }
                                        }
                                    }
                                }

                                //Range header2 = (Range)xlWorkSheet.Cells[1, 1];
                                //header2.Interior.Color = Color.Gold;

                                //Save the excel file under the captured location from the SaveFileDialog
                                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                                    misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                xlexcel.DisplayAlerts = true;

                                xlWorkBook.Close(true, misValue, misValue);
                                xlexcel.Quit();

                                releaseObject(xlWorkSheet);
                                releaseObject(xlWorkBook);
                                releaseObject(xlexcel);

                                // Clear Clipboard and DataGridView selection
                                Clipboard.Clear();
                                dgvMacSchedule.ClearSelection();

                                // Open the newly saved excel file
                                if (File.Exists(sfd.FileName))
                                    System.Diagnostics.Process.Start(sfd.FileName);
                            }

                            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

                            Cursor = Cursors.Arrow; // change cursor to normal type
                        }
                        catch (Exception ex)
                        {
                            tool.saveToTextAndMessageToUser(ex);
                            Cursor = Cursors.Arrow; // change cursor to normal type
                        }


                    }
                    else//export Stocktake List
                    {
                        requestingStocktake_Part = frmMacScheduleExcelDownload.RequestingStocktake_Part;
                        requestingStocktake_RawMat = frmMacScheduleExcelDownload.RequestingStocktake_RawMat;
                        requestingStocktake_ColorMat = frmMacScheduleExcelDownload.RequestingStocktake_ColorMat;


                        DataTable dt_Main = (DataTable)dgvMacSchedule.DataSource;
                        GetStocktakeData(dt_Main);


                        //export to excel
                        NewExcel();

                    }

                }
               
            }

            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect ;


        }

        private void GetStocktakeData(DataTable dt_Main)
        {
            DT_STOCKTAKE_PART = NewStocktakeTable();
            DT_STOCKTAKE_RAWMAT = NewStocktakeTable();
            DT_STOCKTAKE_COLORMAT = NewStocktakeTable();

            if(dt_Main != null)
            {
                foreach (DataRow row in dt_Main.Rows)
                {
                    string ItemName = row[text.Header_ItemName].ToString();
                    string ItemCode = row[text.Header_ItemCode].ToString();
                    string RawMat = row[text.Header_RawMat_1].ToString();
                    string ColorMat = row[text.Header_ColorMatCode].ToString();

                    string Fac = row[text.Header_Fac].ToString();

                    //add Part to Table
                    if (requestingStocktake_Part && !string.IsNullOrEmpty(ItemName) && !string.IsNullOrEmpty(ItemCode))
                    {
                        bool itemDuplicated = false;

                        foreach (DataRow partRow in DT_STOCKTAKE_PART.Rows)
                        {
                            if ((ItemName + " (" + ItemCode + ")").Equals(partRow[text.Header_ItemDescription].ToString()) && Fac.Equals(partRow[text.Header_Fac].ToString()))
                            {
                                itemDuplicated = true;
                                break;
                            }
                        }

                        if (!itemDuplicated)
                        {
                            DataRow newPartRow = DT_STOCKTAKE_PART.NewRow();

                            newPartRow[text.Header_Fac] = Fac;
                            newPartRow[text.Header_ItemDescription] = ItemName + "\n" + " (" + ItemCode + ")";

                            DT_STOCKTAKE_PART.Rows.Add(newPartRow);
                        }

                      

                    }

                    //TPE TSL H50 + H80 = TPE TSL H80 & TPE TSL H50
                    if (requestingStocktake_RawMat && !string.IsNullOrEmpty(RawMat))
                    {
                        bool itemDuplicated = false;

                        if (RawMat.Equals("TPE TSL H50 + H80"))
                        {
                            foreach (DataRow RawMatRaw in DT_STOCKTAKE_RAWMAT.Rows)
                            {
                                if ("TPE TSL H80".Equals(RawMatRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(RawMatRaw[text.Header_Fac].ToString()))
                                {
                                    itemDuplicated = true;
                                    break;
                                }
                            }

                            if (!itemDuplicated)
                            {
                                DataRow newRawMatRow = DT_STOCKTAKE_RAWMAT.NewRow();

                                newRawMatRow[text.Header_Fac] = Fac;
                                newRawMatRow[text.Header_ItemDescription] = "TPE TSL H80";

                                DT_STOCKTAKE_RAWMAT.Rows.Add(newRawMatRow);
                                itemDuplicated = false;
                            }


                            foreach (DataRow RawMatRaw in DT_STOCKTAKE_RAWMAT.Rows)
                            {
                                if ("TPE TSL H50".Equals(RawMatRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(RawMatRaw[text.Header_Fac].ToString()))
                                {
                                    itemDuplicated = true;
                                    break;
                                }
                            }

                            if (!itemDuplicated)
                            {
                                DataRow newRawMatRow = DT_STOCKTAKE_RAWMAT.NewRow();

                                newRawMatRow[text.Header_Fac] = Fac;
                                newRawMatRow[text.Header_ItemDescription] = "TPE TSL H50";

                                DT_STOCKTAKE_RAWMAT.Rows.Add(newRawMatRow);
                                itemDuplicated = false;
                            }
                           
                        }
                        else
                        {
                            foreach (DataRow RawMatRaw in DT_STOCKTAKE_RAWMAT.Rows)
                            {
                                if (RawMat.Equals(RawMatRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(RawMatRaw[text.Header_Fac].ToString()))
                                {
                                    itemDuplicated = true;
                                    break;
                                }
                            }

                            if (!itemDuplicated)
                            {
                                DataRow newRawMatRow = DT_STOCKTAKE_RAWMAT.NewRow();

                                newRawMatRow[text.Header_Fac] = Fac;
                                newRawMatRow[text.Header_ItemDescription] = RawMat;

                                DT_STOCKTAKE_RAWMAT.Rows.Add(newRawMatRow);
                            }

                            
                        }
                    }

                    //color filter Out: NATURAL, COMPOUND
                    if (requestingStocktake_ColorMat && !string.IsNullOrEmpty(ColorMat) && ColorMat != "NATURAL" && ColorMat != "COMPOUND")
                    {
                        bool itemDuplicated = false;

                        foreach(DataRow colorRaw in DT_STOCKTAKE_COLORMAT.Rows)
                        {
                            if (ColorMat.Equals(colorRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(colorRaw[text.Header_Fac].ToString()))
                            {
                                itemDuplicated = true;
                                break;
                            }
                        }

                        if(!itemDuplicated)
                        {
                            DataRow newColorMatRow = DT_STOCKTAKE_COLORMAT.NewRow();

                            newColorMatRow[text.Header_Fac] = Fac;
                            newColorMatRow[text.Header_ItemDescription] = ColorMat;

                            DT_STOCKTAKE_COLORMAT.Rows.Add(newColorMatRow);
                        }
                     
                    }
                }

                DT_STOCKTAKE_PART.DefaultView.Sort = text.Header_Fac + " ASC," + text.Header_ItemDescription + " ASC";
                DT_STOCKTAKE_PART = DT_STOCKTAKE_PART.DefaultView.ToTable();

                DT_STOCKTAKE_RAWMAT.DefaultView.Sort = text.Header_Fac + " ASC," + text.Header_ItemDescription + " ASC";
                DT_STOCKTAKE_RAWMAT = DT_STOCKTAKE_RAWMAT.DefaultView.ToTable();

                DT_STOCKTAKE_COLORMAT.DefaultView.Sort = text.Header_Fac + " ASC," + text.Header_ItemDescription + " ASC";
                DT_STOCKTAKE_COLORMAT = DT_STOCKTAKE_COLORMAT.DefaultView.ToTable();

                DT_STOCKTAKE_PART = tool.NEW_RearrangeIndex(DT_STOCKTAKE_PART, text.Header_Index);

                DT_STOCKTAKE_RAWMAT = tool.NEW_RearrangeIndex(DT_STOCKTAKE_RAWMAT, text.Header_Index);

                DT_STOCKTAKE_COLORMAT = tool.NEW_RearrangeIndex(DT_STOCKTAKE_COLORMAT, text.Header_Index);



            }
        }

        private void ExcelPageSetup(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = 1;
            xlWorkSheet.PageSetup.FitToPagesWide = 1;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            //xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

            xlWorkSheet.PageSetup.PrintArea = "a1:g25";


        }

        private void ExcelMergeandAlign(Worksheet xlWorkSheet, string RangeString, XlHAlign hl, XlVAlign va)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.Merge();

            range.HorizontalAlignment = hl;
            range.VerticalAlignment = va;
        }

        private void ExcelColumnWidth(Worksheet xlWorkSheet, string RangeString, double colWidth)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.ColumnWidth = colWidth;
        }

        private void ExcelRowHeight(Worksheet xlWorkSheet, string RangeString, double rowHeight)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.RowHeight = rowHeight;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, string value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;

            if(area == "a2:g2")
            DataInsertArea.Characters[34, 44].Font.Bold = 1;
        }

        private void NewInitialDOFormat(Worksheet xlWorkSheet)
        {
            #region Sheet Setting

            string Area_Sheet = "a1:g25";
            string Area_FormCode = "A1:C1";
            string Area_SheetTitle = "a2:g2";
            string Area_StockCountQty= "d3:e3";
            string Area_PageNo= "g1:g1";

            string Area_Data = "a4:g23";

            string Area_CountedInfo = "a25:c25";
            string Area_CountedBy = "a25:b25";
            string Area_CountedDate = "c25:c25";

            string Area_VerifiedInfo = "e25:g25";
            string Area_VerifiedBy = "e25:e25";
            string Area_VerifiedDate = "f25:g25";
            string Area_Signature = "a25:g25";

            string Row_FormCode = "a1";
            string Row_SheetTitle = "a2";
            string Row_Header = "a3";
            string Row_Data = "a4:g23";
            string Row_SpaceBetweenDataAndSignature = "a24";
            string Row_Signature = "a25";

           
            string Col_Index = "a1";
            string Col_Location = "b1";
            string Col_ItemDescription = "c1";
            string Col_StockCountQty_1 = "d1";
            string Col_StockCountQty_2 = "e1";
            string Col_SystemQty = "f1";
            string Col_Diffenrence = "g1";



            XlHAlign H_alignLeft = XlHAlign.xlHAlignLeft;
            XlHAlign H_alignRight = XlHAlign.xlHAlignRight;
            XlHAlign H_alignCenter = XlHAlign.xlHAlignCenter;

            XlVAlign V_alignTop = XlVAlign.xlVAlignTop;
            XlVAlign V_alignBottom = XlVAlign.xlVAlignBottom;
            XlVAlign V_alignCenter = XlVAlign.xlVAlignCenter;

            ExcelMergeandAlign(xlWorkSheet, "a3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "b3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "c3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "f3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "g3", H_alignCenter, V_alignCenter);


            ExcelMergeandAlign(xlWorkSheet, Area_FormCode, H_alignLeft, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, Area_PageNo, H_alignRight, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, Area_SheetTitle, H_alignCenter, V_alignCenter);

            ExcelMergeandAlign(xlWorkSheet, Area_StockCountQty, H_alignCenter, V_alignCenter);

            ExcelMergeandAlign(xlWorkSheet, Area_CountedBy, H_alignLeft, V_alignTop);
            ExcelMergeandAlign(xlWorkSheet, Area_CountedDate, H_alignCenter, V_alignTop);

            ExcelMergeandAlign(xlWorkSheet, Area_VerifiedBy, H_alignLeft, V_alignTop);
            ExcelMergeandAlign(xlWorkSheet, Area_VerifiedDate, H_alignCenter, V_alignTop);

            ExcelRowHeight(xlWorkSheet, Row_FormCode, 19.6);
            ExcelRowHeight(xlWorkSheet, Row_SheetTitle, 31.6);
            ExcelRowHeight(xlWorkSheet, Row_Header, 30.4);
            ExcelRowHeight(xlWorkSheet, Row_Data, 31);
            ExcelRowHeight(xlWorkSheet, Row_SpaceBetweenDataAndSignature, 12.4);
            ExcelRowHeight(xlWorkSheet, Row_Signature, 73.6);

            ExcelColumnWidth(xlWorkSheet, Col_Index, 5.2);
            ExcelColumnWidth(xlWorkSheet, Col_Location, 9.63);
            ExcelColumnWidth(xlWorkSheet, Col_ItemDescription, 33.5);
            ExcelColumnWidth(xlWorkSheet, Col_StockCountQty_1, 30.38);
            ExcelColumnWidth(xlWorkSheet, Col_StockCountQty_2, 17.38);
            ExcelColumnWidth(xlWorkSheet, Col_SystemQty, 11.4);
            ExcelColumnWidth(xlWorkSheet, Col_Diffenrence, 10.2);

            Range SheetFormat = xlWorkSheet.get_Range(Area_Sheet).Cells;
            SheetFormat.Interior.Color = Color.White;
            SheetFormat.Font.Name = "Segoe UI";
            SheetFormat.Font.Size = 9;
            //SheetFormat.Font.Color = Color.DarkBlue;
            SheetFormat.Font.Color = Color.Black;
            xlWorkSheet.PageSetup.PrintArea = Area_Sheet;

            Color GrayLineColor = Color.DarkGray;

           


            SheetFormat = xlWorkSheet.get_Range(Area_CountedInfo).Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;


            SheetFormat = xlWorkSheet.get_Range(Area_VerifiedInfo).Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;


            SheetFormat = xlWorkSheet.get_Range(Area_SheetTitle).Cells;
            SheetFormat.Value = "PHYSICAL STOCK COUNT SHEET";
            SheetFormat.Font.Size = 12;

            SheetFormat = xlWorkSheet.get_Range(Area_FormCode).Cells;
            SheetFormat.Value = "FORM CODE : WHSC00101";
            SheetFormat.Font.Size = 8;
            SheetFormat.Font.Italic = true;

            SheetFormat = xlWorkSheet.get_Range("a3").Cells;
            SheetFormat.Value = "#";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("b3").Cells;
            SheetFormat.Value = "Location";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("c3").Cells;
            SheetFormat.Value = "Item Description";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("d3").Cells;
            SheetFormat.Value = "Stock Count Qty";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("f3").Cells;
            SheetFormat.Value = "System Qty";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("g3").Cells;
            SheetFormat.Value = "Difference";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_CountedBy).Cells;
            SheetFormat.Value = "Counted By";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_CountedDate).Cells;
            SheetFormat.Value = "Date";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_VerifiedBy).Cells;
            SheetFormat.Value = "Verified By";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_VerifiedDate).Cells;
            SheetFormat.Value = "Date";
            SheetFormat.Font.Bold = true;


            #endregion

            #region Item List

            int TotalRows = 20;
            int itemRowOffset = 3;

            string indexColStart = "a";
            string indexColEnd = ":a";

            string LocationColStart = "b";
            string LocationColEnd = ":b";

            string ItemDescriptionColStart = "c";
            string ItemDescriptionColEnd = ":c";

            string StockCountQtyColStart = "d";
            string StockCountQtyColEnd = ":e";

            string SystemQtyColStart = "f";
            string SystemQtyColEnd = ":f";

            string DifferenceColStart = "g";
            string DifferenceColEnd = ":g";


            for (int i = 0; i <= TotalRows; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = LocationColStart + (itemRowOffset + i).ToString() + LocationColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = ItemDescriptionColStart + (itemRowOffset + i).ToString() + ItemDescriptionColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignLeft, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;


                area = StockCountQtyColStart + (itemRowOffset + i).ToString() + StockCountQtyColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = SystemQtyColStart + (itemRowOffset + i).ToString() + SystemQtyColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = DifferenceColStart + (itemRowOffset + i).ToString() + DifferenceColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

            }

            SheetFormat = xlWorkSheet.get_Range("a3:g3").Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;

            SheetFormat = xlWorkSheet.get_Range("a3:g23").Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;

            //CellStyle style = cell.Style;
            //style.ShrinkToFit = true;

            SheetFormat.ShrinkToFit = true;

            SheetFormat = xlWorkSheet.get_Range("c4:c23").Cells;
            SheetFormat.WrapText = true;

            #endregion
        }

        private void NewExcel()
        {
            #region export excel

            if (requestingStocktake_Part || requestingStocktake_RawMat || requestingStocktake_ColorMat)
            {
                #region Excel Saving Setting

                string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

                string path = @"D:\StockAssistant\Document\OUG Stock Count";

                Directory.CreateDirectory(path);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = path;

                DateTime dateFrom = dtpFrom.Value;
                DateTime dateTo = dtpTo.Value;

                string title = "StockCountSheet_ProductionEnd (" + dateFrom.Date.ToString("ddMM") + "~" + dateTo.Date.ToString("ddMM") + ")";
                DateTime currentDate = DateTime.Now;

                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = title + "_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

                #endregion

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    #region Excel Setting

                    frmLoading.ShowLoadingScreen();
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;

                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };

                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.StandardFont = "Segoe UI";
                    xlexcel.StandardFontSize = 9;

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    xlexcel.PrintCommunication = false;

                    int pageNo = 1;
                    int sheetNo = 0;
                    int rowNo = 0;

                    Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;
                    //xlWorkSheet.PageSetup.PrintArea

                    xlexcel.PrintCommunication = true;
                    ExcelPageSetup(xlWorkSheet);

                    xlexcel.PrintCommunication = true;

                    xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;


                    NewInitialDOFormat(xlWorkSheet);

                    #endregion

                    sheetNo = 0;
                    rowNo = 0;

                    int rowStart = 4;
                    int rowEnd = 23;
                    int rowMax = rowEnd - rowStart + 1;

                    string col_Index = "a";
                    string col_Location = "b";
                    string col_ItemDescription = "c";
                    string colStart_StockCountQty = "d";
                    string colEnd_StockCountQty = "e";
                    string col_SystemQty = "f";
                    string col_Difference = "g";

                    string Area_PageNo = "g1:g1";

                    string areaPageData = "t10:w10";

                    string Area_SheetTitle = "a2:g2";
                    int dataRowInsertedCount = 0;


                    string FormTitle = "STOCK COUNT SHEET_PRODUCTION END ("  + dateFrom.Date.ToString("dd/MM") + "~" + dateTo.Date.ToString("dd/MM") + " )_";


                    //check if data can combine in one sheet
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //8467ms

                    if (requestingStocktake_Part)
                    {
                        sheetNo++;

                        pageNo = 1;
                        xlWorkSheet.Name = text.Cat_Part;
                        int index = 1;

                        InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());

                        InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_Part);


                        foreach (DataRow row in DT_STOCKTAKE_PART.Rows)
                        {
                            string fac = row[text.Header_Fac].ToString();
                            string itemDescription = row[text.Header_ItemDescription].ToString();

                            InsertToSheet(xlWorkSheet, col_Index + rowStart + ":" + col_Index + rowStart, index.ToString());
                            InsertToSheet(xlWorkSheet, col_Location + rowStart + ":" + col_Location + rowStart, fac);
                            InsertToSheet(xlWorkSheet, col_ItemDescription + rowStart + ":" + col_ItemDescription + rowStart, itemDescription);

                            rowStart++;
                            index++;
                            dataRowInsertedCount++;

                            if(dataRowInsertedCount == rowMax)
                            {
                                //reset and create new sheet
                                sheetNo++;

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                xlexcel.PrintCommunication = true;
                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;

                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                NewInitialDOFormat(xlWorkSheet);

                                pageNo++;
                                xlWorkSheet.Name = text.Cat_Part + " (" + pageNo + ")";
                                rowStart = 4;
                                dataRowInsertedCount = 0;

                                InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_Part);
                                InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());

                            }


                        }
                    }

                    if (requestingStocktake_RawMat)
                    {
                        sheetNo++;
                        rowStart = 4;
                        dataRowInsertedCount = 0;

                        if (sheetNo > 1)
                        {
                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                            xlexcel.PrintCommunication = true;
                            ExcelPageSetup(xlWorkSheet);

                            xlexcel.PrintCommunication = true;

                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            NewInitialDOFormat(xlWorkSheet);
                        }

                        pageNo = 1;
                        xlWorkSheet.Name = text.Cat_RawMat;
                        int index = 1;
                        InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                        InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_RawMat);


                        foreach (DataRow row in DT_STOCKTAKE_RAWMAT.Rows)
                        {
                            string fac = row[text.Header_Fac].ToString();
                            string itemDescription = row[text.Header_ItemDescription].ToString();

                            InsertToSheet(xlWorkSheet, col_Index + rowStart + ":" + col_Index + rowStart, index.ToString());
                            InsertToSheet(xlWorkSheet, col_Location + rowStart + ":" + col_Location + rowStart, fac);
                            InsertToSheet(xlWorkSheet, col_ItemDescription + rowStart + ":" + col_ItemDescription + rowStart, itemDescription);

                            rowStart++;
                            index++;
                            dataRowInsertedCount++;

                            if (dataRowInsertedCount == rowMax)
                            {
                                //reset and create new sheet
                                sheetNo++;

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                xlexcel.PrintCommunication = true;
                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;

                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                NewInitialDOFormat(xlWorkSheet);

                                pageNo++;
                                xlWorkSheet.Name = text.Cat_RawMat + " (" + pageNo + ")";
                                rowStart = 4;
                                dataRowInsertedCount = 0;
                                InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                                InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_RawMat);


                            }


                        }
                    }

                    if (requestingStocktake_ColorMat)
                    {
                        sheetNo++;
                        rowStart = 4;
                        dataRowInsertedCount = 0;

                        if (sheetNo > 1)
                        {
                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                            xlexcel.PrintCommunication = true;
                            ExcelPageSetup(xlWorkSheet);

                            xlexcel.PrintCommunication = true;

                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            NewInitialDOFormat(xlWorkSheet);
                        }

                        pageNo = 1;
                        xlWorkSheet.Name = text.Cat_ColorMat;
                        int index = 1;
                        InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                        InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_ColorMat);

                        foreach (DataRow row in DT_STOCKTAKE_COLORMAT.Rows)
                        {
                            string fac = row[text.Header_Fac].ToString();
                            string itemDescription = row[text.Header_ItemDescription].ToString();

                            InsertToSheet(xlWorkSheet, col_Index + rowStart + ":" + col_Index + rowStart, index.ToString());
                            InsertToSheet(xlWorkSheet, col_Location + rowStart + ":" + col_Location + rowStart, fac);
                            InsertToSheet(xlWorkSheet, col_ItemDescription + rowStart + ":" + col_ItemDescription + rowStart, itemDescription);

                            rowStart++;
                            index++;
                            dataRowInsertedCount++;

                            if (dataRowInsertedCount == rowMax)
                            {
                                //reset and create new sheet
                                sheetNo++;

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                xlexcel.PrintCommunication = true;
                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;

                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                NewInitialDOFormat(xlWorkSheet);

                                pageNo++;
                                xlWorkSheet.Name = text.Cat_ColorMat + " (" + pageNo + ")";
                                rowStart = 4;
                                dataRowInsertedCount = 0;
                                InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                                InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_ColorMat);

                            }


                        }


                    }



                    if (sheetNo > 0)
                    {
                        xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                       misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    }

                    frmLoading.CloseForm();
                    Focus();

                    #region open saved file
                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                    {
                        Excel.Application excel = new Excel.Application();
                        excel.Visible = true;
                        excel.DisplayAlerts = false;
                        Workbook wb = excel.Workbooks.Open(sfd.FileName, ReadOnly: false, Notify: false);
                        excel.DisplayAlerts = true;
                    }


                    xlexcel.DisplayAlerts = true;

                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);
                    #endregion
                }

            }
            #endregion
        }

        private void ChangeTransferRecordProductionPlanIDRemark(string planID, string newPlanID)
        {
            trfHistBLL uTrfHist = new trfHistBLL();
            uTrfHist.trf_hist_updated_by = MainDashboard.USER_ID;
            uTrfHist.trf_hist_updated_date = DateTime.Now;

            trfHistDAL dalTrf = new trfHistDAL();

            DataTable dt_Trf = dalTrf.SelectAll();
            dt_Trf.DefaultView.Sort = dalTrf.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();

            int trfTableCode = -1;

            foreach (DataRow row in dt_Trf.Rows)
            {
                DateTime trfDate = Convert.ToDateTime(row[dalTrf.TrfDate].ToString());

                string productionInfo = row[dalTrf.TrfNote].ToString();

                string _planIDFound = "";
                string balanceClearType = "";
                bool startCopy = false;
                bool IDCopied = false;
                bool ShiftCopied = false;
                bool StopIDCopy = false;

                for (int i = 0; i < productionInfo.Length; i++)
                {
                    if (productionInfo[i].ToString() == "P")
                    {
                        startCopy = true;
                    }
                    else if (ShiftCopied && (productionInfo[i].ToString() == "O" || productionInfo[i].ToString() == "B"))
                    {
                        startCopy = true;
                    }
                    else if (ShiftCopied && productionInfo[i].ToString() == "]")
                    {
                        startCopy = false;
                        StopIDCopy = true;
                    }

                    if (startCopy)
                    {

                        if (char.IsDigit(productionInfo[i]) && !StopIDCopy)
                        {
                            IDCopied = true;
                            _planIDFound += productionInfo[i];
                        }
                        else if (IDCopied && i + 1 < productionInfo.Length)
                        {
                            IDCopied = false;
                            ShiftCopied = true;
                            startCopy = false;
                        }
                        else if (ShiftCopied)
                        {
                            balanceClearType += productionInfo[i];
                        }

                    }
                }

                if (planID == _planIDFound)
                {
                    trfTableCode = int.TryParse(row[dalTrf.TrfID].ToString(), out trfTableCode) ? trfTableCode : -1;

                    if (trfTableCode != -1)
                    {
                        uTrfHist.trf_hist_note = productionInfo.Replace(planID, newPlanID);
                        uTrfHist.trf_hist_id = trfTableCode;

                        if (!dalTrf.NoteUpdate(uTrfHist))
                        {
                            MessageBox.Show("Failed to update note!\nTable Code: "+ trfTableCode);
                        }
                    }


                }

            }

        }

        private void copyAlltoClipboard()
        {
            dgvMacSchedule.SelectAll();
            DataObject dataObj = dgvMacSchedule.GetClipboardContent();
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

        #endregion

        #region Filter Data

        private void cbItem_CheckedChanged(object sender, EventArgs e)
        {
            if (cbItem.Checked)
            {
                cbSearchByJobNo.Checked = false;
            }
        }

        private void cbPlanningID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchByJobNo.Checked)
            {
                cbItem.Checked = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            DateTime DateFrom = dtpFrom.Value;

            bool GetCompletedPlanOnly = !cbPending.Checked && !cbRunning.Checked && !cbWarning.Checked && !cbCancelled.Checked && cbCompleted.Checked;
            bool isMonday = DateFrom.DayOfWeek == DayOfWeek.Monday;

            if(GetCompletedPlanOnly && isMonday)
            {
                //set date End on Saturday
                dtpTo.Value = DateFrom.AddDays(5);
            }


        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMacLocation.Text != null && loaded && ableLoadData)
            {
                ableLoadData = false;

                //loadMachine();
                loadMachine(cmbMac);

                ableLoadData = true;
            }
        }

        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMac.SelectedItem != null && ableLoadData)
            {
                ableLoadData = false;

                DataRowView drv = (DataRowView)cmbMac.SelectedItem;
                ableLoadData = true;

                cmbMacLocation.Text = tool.getFactoryNameFromMachineID(drv[dalMac.MacID].ToString());
            }
        }

        private void ResetStatusToDefault()
        {
            cbPending.Checked = true;
            cbRunning.Checked = true;
            cbWarning.Checked = true;
            cbCompleted.Checked = false;
            cbCancelled.Checked = false;
        }

        private void ResetData()
        {
            cmbMacLocation.SelectedIndex = 0;
            txtSearch.Clear();
            cbItem.Checked = true;
            ResetStatusToDefault();

            DateTime todayDate = DateTime.Today;
            dtpFrom.Value = todayDate.AddDays(-180);
            dtpTo.Value = todayDate.AddDays(180);
            ActiveControl = dgvMacSchedule;

        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            ResetData();

            dgvMacSchedule.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        public void StartForm()
        {
            System.Windows.Forms.Application.Run(new frmLoading());
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                frmLoading.ShowLoadingScreen();
                New_LoadMacSchedule();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                frmLoading.CloseForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchByJobNo.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region form close
        private void frmMachineSchedule_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ProductionFormOpen = false;
        }
        #endregion


        private Color GetColorSetFromPlanStatus(string PlanStatus,DataGridView dgv)
        {
            Color ColorSet = dgv.DefaultCellStyle.BackColor;

            if (PlanStatus.Equals(text.planning_status_cancelled))
            {
                ColorSet = Color.White;
            }
            else if (PlanStatus.Equals(text.planning_status_completed))
            {
                ColorSet = Color.Gainsboro;
            }
            else if (PlanStatus.Equals(text.planning_status_delayed))
            {
                ColorSet = Color.FromArgb(255, 255, 128);
            }
            else if (PlanStatus.Equals(text.planning_status_pending))
            {
                ColorSet = Color.Gainsboro;
            }
            else if (PlanStatus.Equals(text.planning_status_running))
            {
                ColorSet = Color.FromArgb(0, 184, 148);
            }
            else if (PlanStatus.Equals(text.planning_status_warning))
            {
                ColorSet = Color.LightGreen;
            }
            else if (PlanStatus.Equals(text.planning_status_draft))
            {
                ColorSet = Color.FromArgb(254, 241, 154);
            }
            else if (PlanStatus.Equals(""))
            {
                ColorSet = Color.FromArgb(70,70,70);
            }

            return ColorSet;
        }

        private void MacScheduleListCellFormatting(DataGridView dgv)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;

            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                string PlanStatus = row[text.Header_Status].ToString();

                Color ColorSet = GetColorSetFromPlanStatus(PlanStatus, dgv);

                dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = ColorSet;
                dgv.Rows[rowIndex].Cells[text.Header_Status].Style.ForeColor = Color.Black;

                if (PlanStatus == "")
                {
                    string divider = row[text.Header_ItemNameAndCode].ToString();
                    row[text.Header_ItemNameAndCode] = "";

                    if (divider.Contains("factoryDivider"))
                    {
                        dgv.Rows[rowIndex].Height = 60;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Height = 6;
                    }
                  
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(70,70,70); //70,70,70

                    dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.FromArgb(70,70,70);
                    dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.FromArgb(70,70,70);

                    //if (!cmbMacLocation.Text.Equals("All") && !string.IsNullOrEmpty(cmbMacLocation.Text) && string.IsNullOrEmpty(cmbMac.Text))
                    //{
                    //    dgv.Rows[rowIndex].Height = 10;
                    //}

                }
                else
                {
                   

                    // dgv.Rows[rowIndex].Height = 50;
                    dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.Gainsboro;
                    dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.Gainsboro;
                }


                if (!PlanStatus.Equals(text.planning_status_cancelled) && !PlanStatus.Equals(text.planning_status_completed) && PlanStatus != "")
                {
                    DateTime startDate = Convert.ToDateTime(row[text.Header_DateStart].ToString());

                    int macID = Convert.ToInt32(row[text.Header_Mac].ToString());

                    if (rowIndex + 1 <= dgv.Rows.Count - 1)
                    {
                        for (int i = rowIndex + 1; i < dgv.Rows.Count; i++)
                        {
                            string otherStatus = dgv.Rows[i].Cells[text.Header_Status].Value.ToString();

                            if (!otherStatus.Equals(text.planning_status_cancelled) && !otherStatus.Equals(text.planning_status_completed) && otherStatus != "")
                            {
                                DateTime otherStart = Convert.ToDateTime(dgv.Rows[i].Cells[text.Header_DateStart].Value);
                                int otherMacID = Convert.ToInt32(dgv.Rows[i].Cells[text.Header_Mac].Value);
                                if (startDate == otherStart && macID == otherMacID)
                                {
                                    int planID = Convert.ToInt32(row[text.Header_JobNo].ToString());
                                    int otherPlanID = Convert.ToInt32(dgv.Rows[i].Cells[text.Header_JobNo].Value);

                                    if (checkIfFamilyMould(planID, otherPlanID))
                                    {
                                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.Yellow;
                                        dgv.Rows[i].Cells[text.Header_DateStart].Style.BackColor = Color.Yellow;
                                    }
                                }
                            }

                        }
                    }
                }
            }


            dgv.ResumeLayout();
        }

        #region plan status change

        private bool planRunning(int rowIndex, string presentStatus)
        {
            bool statusChanged = false;

            uPlanning.plan_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_JobNo].Value;
            uPlanning.machine_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Mac].Value;
            uPlanning.production_start_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value;
            uPlanning.production_end_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value;
            uPlanning.recording = true;

            if (tool.ifProductionDateAvailable(uPlanning.machine_id.ToString()))
            {
                frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, true);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmMachineScheduleAdjustFromMain.applied)
                {
                    //loadScheduleData();

                    statusChanged = true;
                    //inactive mat plan

                    uMatPlan.plan_id = uPlanning.plan_id;
                    uMatPlan.active = true;
                    uMatPlan.updated_date = DateTime.Now;
                    uMatPlan.updated_by = MainDashboard.USER_ID;

                    dalMatPlan.ActiveUpdate(uMatPlan);

                    if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                    {
                        //get habit data
                        float oldHourPerDay = 0;
                        float newHourPerDay = 0;

                        DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                        foreach (DataRow row in dt.Rows)
                        {
                            newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                        }

                        //get this plan hour per day data
                        DataTable dt_plan = dalPlanning.idSearch(uPlanning.plan_id.ToString());

                        foreach (DataRow row in dt_plan.Rows)
                        {
                            oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                            //compare
                            if (oldHourPerDay != newHourPerDay)
                            {
                                int oldProDay = 0, newProDay = 0;
                                float oldProHour = 0, newProHour = 0;

                                oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                                oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                                float totalHour = oldProDay * oldHourPerDay + oldProHour;

                                newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                                newProHour = totalHour - newProDay * newHourPerDay;

                                if (newProHour < 0)
                                {
                                    newProHour = 0;
                                }

                                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                                //change this plan data
                                //update
                                //change production day & hour data
                                uPlanning.plan_id = uPlanning.plan_id;
                                uPlanning.production_hour = newProHour.ToString();
                                uPlanning.production_day = newProDay.ToString();
                                uPlanning.production_hour_per_day = newHourPerDay.ToString();
                                uPlanning.production_start_date = start.Date;
                                uPlanning.production_end_date = end.Date;
                                uPlanning.plan_updated_date = DateTime.Now;
                                uPlanning.plan_updated_by = MainDashboard.USER_ID;
                                uPlanning.recording = true;

                                //update to db
                                dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                            }
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("Machine "+ uPlanning.machine_id + " is running now, please stop it before start a new plan.");
            }
            
            

            Cursor = Cursors.Arrow; // change cursor to normal type
            return statusChanged;
        }

        private void planNoteUpdate(int planID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            uPlanning.plan_id = planID;
            uPlanning.plan_remark = CELL_EDITING_NEW_VALUE;
            uPlanning.plan_updated_date = DateTime.Now;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            dalPlanningAction.planningRemarkChange(uPlanning, CELL_EDITING_OLD_VALUE);

            CELL_EDITING_OLD_VALUE = "";
            CELL_EDITING_NEW_VALUE = "";

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private bool planPending(int planID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = planID;
            uPlanning.plan_status = text.planning_status_pending;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;
            //uPlanning.recording = false;

            //bool success = dalPlanning.statusUpdate(uPlanning);
            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = planID;
                uMatPlan.active = true;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);

                if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                {
                    //get habit data
                    float oldHourPerDay = 0;
                    float newHourPerDay = 0;

                    DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                    foreach (DataRow row in dt.Rows)
                    {
                        newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                    }

                    //get this plan hour per day data
                    DataTable dt_plan = dalPlanning.idSearch(planID.ToString());

                    foreach (DataRow row in dt_plan.Rows)
                    {
                        oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                        //compare
                        if (oldHourPerDay != newHourPerDay)
                        {
                            int oldProDay = 0, newProDay = 0;
                            float oldProHour = 0, newProHour = 0;

                            oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                            oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                            float totalHour = oldProDay * oldHourPerDay + oldProHour;

                            newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                            newProHour = totalHour - newProDay * newHourPerDay;

                            if (newProHour < 0)
                            {
                                newProHour = 0;
                            }

                            DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                            DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                            //change this plan data
                            //update
                            //change production day & hour data
                            uPlanning.plan_id = planID;
                            uPlanning.production_hour = newProHour.ToString();
                            uPlanning.production_day = newProDay.ToString();
                            uPlanning.production_hour_per_day = newHourPerDay.ToString();
                            uPlanning.production_start_date = start.Date;
                            uPlanning.production_end_date = end.Date;
                            uPlanning.plan_updated_date = DateTime.Now;
                            uPlanning.plan_updated_by = MainDashboard.USER_ID;

                            //update to db
                            dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                        }
                    }
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }

        private bool planComplete(int planID, string presentStatus)
        {
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            uPlanning.plan_id = planID;
            uPlanning.plan_status = text.planning_status_completed;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = planID;
                uMatPlan.active = false;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);
            }
            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }

        private bool planCancel(int planID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = planID;
            uPlanning.plan_status = text.planning_status_cancelled;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            uPlanning.recording = false;

            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = planID;
                uMatPlan.active = false;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);
            }

            
            Cursor = Cursors.Arrow; // change cursor to normal type
            return statusChanged;
        }

        private void CalculationMaterialSummary(string material)
        {
            loadMaterialSummary(material);
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            dgvMacSchedule.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            DataGridView dgv = dgvMacSchedule;
            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
            int colIndex = dgv.CurrentCell.ColumnIndex;

            int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[text.Header_JobNo].Value);

            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[text.Header_Mac].Value);

            string presentStatus = dgv.Rows[rowIndex].Cells[text.Header_Status].Value.ToString();

            string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.planning_status_pending))
            {
                if (MessageBox.Show("Are you sure you want to switch this plan to PENDING status?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (planPending(planID, presentStatus))
                    {
                        DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                        if (cbPending.Checked)
                        {
                            dt.Rows[rowIndex][text.Header_Status] = text.planning_status_pending;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(rowIndex);
                            dt.AcceptChanges();
                        }
                    }
                }
                    
            }
            else if (itemClicked.Equals(text.planning_status_running))
            {              
                if (planRunning(rowIndex, presentStatus))
                {
                    DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                    if (cbRunning.Checked)
                    {
                        dt.Rows[rowIndex][text.Header_Status] = text.planning_status_running;
                    }
                    else
                    {
                        dt.Rows.RemoveAt(rowIndex);
                        dt.AcceptChanges();
                    }
                }
            }
            else if (itemClicked.Equals(text.planning_status_completed))
            {
                if (MessageBox.Show("Are you sure you want to switch this plan to COMPLETED status?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (planComplete(planID, presentStatus))
                    {
                        DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                        if (cbCompleted.Checked)
                        {
                            dt.Rows[rowIndex][text.Header_Status] = text.planning_status_completed;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(rowIndex);
                            dt.AcceptChanges();
                        }

                        var w = new Form() { Size = new Size(0, 0) };
                        Task.Delay(TimeSpan.FromSeconds(3))
                            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                        MessageBox.Show(w,  "PLAN " + planID + " completed!", "SYSTEM");
                    }
                }
            }
            else if (itemClicked.Equals(text.planning_status_cancelled))
            {
                if (MessageBox.Show("Are you sure you want to CANCEL this plan?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(planCancel(planID, presentStatus))
                    {
                        DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                        if (cbCancelled.Checked)
                        {
                            dt.Rows[rowIndex][text.Header_Status] = text.planning_status_cancelled;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(rowIndex);
                            dt.AcceptChanges();
                        }

                        var w = new Form() { Size = new Size(0, 0) };
                        Task.Delay(TimeSpan.FromSeconds(3))
                            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                        MessageBox.Show(w, "PLAN "+planID+" cancelled!", "SYSTEM");
                    }
                }
                
            }
            else if (itemClicked.Equals("Edit Schedule"))
            {
                editSchedule(rowIndex);
            }
            //loadScheduleData();
            //dgvSchedule.ClearSelection();

            MacScheduleListCellFormatting(dgvMacSchedule);

            if (itemClicked.Equals(text.planning_Material_Summary))
            {
                string headerName = dgv.Columns[colIndex].Name;

                if(headerName == text.Header_ColorMat)
                {
                    cellValue = dgv.Rows[rowIndex].Cells[text.Header_ColorMatCode].Value.ToString();

                }

                CalculationMaterialSummary(cellValue);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgvMacSchedule.ResumeLayout();
        }

        private void editSchedule(int rowIndex)
        {
            uPlanning.plan_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_JobNo].Value;
            uPlanning.machine_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Mac].Value;
            uPlanning.production_start_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value;
            uPlanning.production_end_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value;

            frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(frmMachineScheduleAdjustFromMain.applied)
            {
                New_LoadMacSchedule();
                dgvMacSchedule.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void dgvSchedule_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvMacSchedule.CurrentCell = dgvMacSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvMacSchedule.Rows[e.RowIndex].Selected = true;
                dgvMacSchedule.Focus();
                int rowIndex = dgvMacSchedule.CurrentCell.RowIndex;
                int colIndex = dgvMacSchedule.CurrentCell.ColumnIndex;

                string currentHeader = dgvMacSchedule.Columns[colIndex].Name;


                try
                {
                    string result = dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Status].Value.ToString();

                    if (result.Equals(text.planning_status_pending))
                    {
                        my_menu.Items.Add("Run").Name = text.planning_status_running;
                        my_menu.Items.Add("Cancel").Name = text.planning_status_cancelled;
                        my_menu.Items.Add("Complete").Name = text.planning_status_completed;                        
                    }
                    else if (result.Equals(text.planning_status_cancelled))
                    {

                        my_menu.Items.Add("Run").Name = text.planning_status_running;
                        my_menu.Items.Add("Pending").Name = text.planning_status_pending;

                        
                    }
                    else if (result.Equals(text.planning_status_running))
                    {
                        my_menu.Items.Add("Pending").Name = text.planning_status_pending;
                        my_menu.Items.Add("Cancel").Name = text.planning_status_cancelled;
                        my_menu.Items.Add("Complete").Name = text.planning_status_completed;
                    }
                    else if (result.Equals(text.planning_status_completed))
                    {
                        my_menu.Items.Add("Run").Name = text.planning_status_running;
                        my_menu.Items.Add("Pending").Name = text.planning_status_pending;
                    }

                    my_menu.Items.Add("Edit Schedule").Name = "Edit Schedule";

                    if(currentHeader.Equals(text.Header_RawMat_String) || currentHeader.Equals(text.Header_ColorMat))
                    my_menu.Items.Add(text.planning_Material_Summary).Name = text.planning_Material_Summary;
                    
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

        #endregion

        #region plan action history check
        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                string planID = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_JobNo].Value.ToString();
                int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
                //handle the row selection on right click
               
                    //MessageBox.Show("Plan ID: "+planID);
                    if (planID != null && userPermission >= MainDashboard.ACTION_LVL_THREE)
                {
                    frmPlanningActionHistory frm = new frmPlanningActionHistory(Convert.ToInt32(planID));
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (!frmPlanningActionHistory.noData)
                    {
                        frm.ShowDialog();
                    }
                }
            
            } 
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Thread t = null;
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                t = new Thread(new ThreadStart(StartForm));
                New_LoadMacSchedule();
            }
            catch (ThreadAbortException)
            {
                // ignore it
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                t.Abort();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (filterHide)
            {
                filterHide = false;
            }
            else
            {
                filterHide = true;
            }

            HideFilter(filterHide);
        }

        private void cbCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if(cbCompleted.Checked)
            {
                cbPending.Checked = false;
                cbRunning.Checked = false;
                cbWarning.Checked = false;
                cbCancelled.Checked = false;
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void dgvSchedule_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                CELL_VALUE_CHANGED = false;
                CELL_EDITING_OLD_VALUE = "";

                DataGridView dgv = dgvMacSchedule;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (dgv.Columns[colIndex].Name.Contains(text.Header_Remark)) //Desired Column
                {
                    CELL_EDITING_OLD_VALUE = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvSchedule_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if(CELL_VALUE_CHANGED)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int planID = int.TryParse(dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_JobNo].Value.ToString(), out planID) ? planID : 0;

                    planNoteUpdate(planID);
                }
            }
        }

        private void dgvSchedule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CELL_VALUE_CHANGED = true;

            DataGridView dgv = dgvMacSchedule;

            CELL_EDITING_NEW_VALUE = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

           
        }

        private void label6_Click(object sender, EventArgs e)
        {
            dtpTo.Value = DateTime.Now;
        }

        private void dgvSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string headerName = dgvMacSchedule.Columns[e.ColumnIndex].Name;

                if(headerName == text.Header_RawMat_String || headerName == text.Header_ColorMat || headerName == text.Header_ColorMatCode)
                {
                    string cellValue = dgvMacSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    if (headerName == text.Header_RawMat_String)
                    {
                        cellValue = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_RawMat_1].Value.ToString();

                        string rawMat2 = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_RawMat_2].Value.ToString();
                      
                    }

                    if (headerName == text.Header_ColorMat)
                    {
                        cellValue = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_ColorMatCode].Value.ToString();
                    }

                    if(!string.IsNullOrEmpty(cellValue))
                        CalculationMaterialSummary(cellValue);
                    else
                    {
                        MatSummaryReset();

                    }
                }
                else
                {
                    //reset
                    MatSummaryReset();
                }
            

            }
        }
    }
}
