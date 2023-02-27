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
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using TextBox = System.Windows.Forms.TextBox;
using Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;
using Range = Microsoft.Office.Interop.Excel.Range;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;
using System.Drawing.Imaging;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMachineSchedule : Form
    {
        public frmMachineSchedule()
        {
            InitializeComponent();
            InitializeData();

            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_TWO)
            {
                btnPlan.Show();
            }
            else
            {
                btnPlan.Hide();
            }

            tool.DoubleBuffered(dgvSchedule, true);
            HideFilter(filterHide);
        }

        public frmMachineSchedule(string itemCode)
        {
            InitializeComponent();
            InitializeData();

            btnPlan.Hide();
            btnMatList.Hide();
            btnExcel.Hide();

            txtSearch.Text = itemCode;

            tool.DoubleBuffered(dgvSchedule, true);
            HideFilter(filterHide);

            ItemProductionHistoryChecking = true;
        }

        public frmMachineSchedule(bool _fromDailyJobRecord)
        {
            
            InitializeComponent();
            InitializeData();
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnPlan.Hide();
            //btnExcel.Hide();
            btnMatList.Hide();

            
            fromDailyRecord = _fromDailyJobRecord;
            btnExcel.Text = "ADD ITEM";
            btnExcel.Width = 180;
            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300);
            tool.DoubleBuffered(dgvSchedule, true);
            HideFilter(filterHide);
        }

        public frmMachineSchedule(string action, int data_1, string data_2)
        {

            InitializeComponent();
            InitializeData();

            btnPlan.Hide();
            //btnExcel.Hide();
            btnMatList.Hide();

            btnExcel.Text = action;
            btnExcel.Width = 180;
            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300);
            tool.DoubleBuffered(dgvSchedule, true);
            HideFilter(false);
            buttionAction = action;

            if(action == text.DailyAction_ChangePlan)
            {
                ChangePlanToAction = true;

                ITEM_CODE = data_2;
                PLAN_ID = data_1;

                cbCompleted.Checked = true;
                cbCancelled.Checked = true;
                cbPlanningID.Checked = true;
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
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();

        habitDAL dalHabit = new habitDAL();

        private int PLAN_ID = -1;
        private string ITEM_CODE = "";
        private string buttionAction = "";
        int userPermission = -1;
        readonly string headerID = "PLAN";
        readonly string headerStartDate = "START";
        readonly string headerEndDate = "ESTIMATE END";
        //readonly string headerProductionDay = "PRODUCTION DAY";
        readonly string headerFactory = "FAC.";
        readonly string headerMachine = "MAC.";
        readonly string headerPartName = "NAME";
        readonly string headerPartCode = "CODE";
        readonly string headerPartCycleTime = "CYCLE TIME";

        readonly string headerProductionPurpose = "PRODUCTION FOR";
        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerAbleProduceQty = "ABLE PRODUCE";
        readonly string headerProducedQty = "PRODUCED QTY";

        readonly string headerMaterial = "MATERIAL";
        readonly string headerMaterialBag = "BAG";
        readonly string headerRecycle = "RECYCLE (KG)";
        
        //readonly string headerColor = "COLOR";
        readonly string headerColorMaterial = "COLOR MATERIAL";
        //readonly string headerColorMaterialUsage = "USAGE %";
        readonly string headerColorMaterialQty = "QTY (KG)";

        readonly string headerNote = "REMARK";

        readonly string headerStatus = "STATUS";

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
                btnFilter.Text = "SHOW FILTER";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                //tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 85f);
                //tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Percent, 83f);
            }
            else
            {
                btnFilter.Text = "HIDE FILTER";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Absolute, 120f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Absolute, 45f);
                //tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 62f);
            }
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerStatus, typeof(string));
            
            dt.Columns.Add(headerFactory, typeof(string));
            dt.Columns.Add(headerMachine, typeof(int));
            dt.Columns.Add(headerStartDate, typeof(DateTime));
            dt.Columns.Add(headerEndDate, typeof(DateTime));
            //dt.Columns.Add(headerProductionDay, typeof(string));

            
            
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerPartCycleTime, typeof(int));
            
            dt.Columns.Add(headerTargetQty, typeof(int));
            dt.Columns.Add(headerAbleProduceQty, typeof(int));
            dt.Columns.Add(headerProducedQty, typeof(int));

            dt.Columns.Add(headerMaterial, typeof(string));
            dt.Columns.Add(headerMaterialBag, typeof(int));
            dt.Columns.Add(headerRecycle, typeof(int));

            //dt.Columns.Add(headerColor, typeof(string));
            dt.Columns.Add(headerColorMaterial, typeof(string));
            //dt.Columns.Add(headerColorMaterialUsage, typeof(int));
            dt.Columns.Add(headerColorMaterialQty, typeof(float));

           
            dt.Columns.Add(headerNote, typeof(string));
            dt.Columns.Add(headerProductionPurpose, typeof(string));


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
            //dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerPartCode].Frozen = true;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerStartDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerEndDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.Columns[headerFactory].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerMachine].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerPartCycleTime].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerProductionPurpose].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerTargetQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerAbleProduceQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.Columns[headerMaterial].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerMaterialBag].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerRecycle].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.Columns[headerColorMaterial].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerColorMaterialQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.Columns[headerNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerStatus].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////

            //dgv.Columns[headerPartCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[headerMaterial].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[headerColorMaterial].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[headerNote].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dgv.Columns[headerID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStartDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerEndDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFactory].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMachine].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTargetQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerAbleProduceQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerProducedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerRecycle].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMaterialBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerColorMaterialQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPartCycleTime].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //forecolor///////////////////////////////////////////////////////////////////////////////////////////////////

            Color normalColor = Color.Black;
            

            dgv.Columns[headerStartDate].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerEndDate].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerProductionDay].DefaultCellStyle.ForeColor = Color.White;

            dgv.Columns[headerFactory].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerID].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerID].Visible = false;

            //int colorR = 192;
            //int colorG = 0;
            //int colorB= 0;

            ////int colorR = 255;
            ////int colorG = 215;
            ////int colorB = 0;
            //192, 0, 0
            Color importantColor = Color.OrangeRed;
            //importantColor = Color.FromArgb(192, 0, 0);
            dgv.Columns[headerMachine].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerPartName].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerPartCode].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerPartCycleTime].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerProductionPurpose].DefaultCellStyle.ForeColor = normalColor;

            dgv.Columns[headerTargetQty].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerAbleProduceQty].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerProducedQty].DefaultCellStyle.ForeColor = importantColor;


            dgv.Columns[headerMaterial].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerMaterialBag].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerRecycle].DefaultCellStyle.ForeColor = normalColor;

            //dgv.Columns[headerColor].DefaultCellStyle.ForeColor = Color.White;
            dgv.Columns[headerColorMaterial].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerColorMaterialUsage].DefaultCellStyle.ForeColor = Color.White;
            dgv.Columns[headerColorMaterialQty].DefaultCellStyle.ForeColor = importantColor;


            //dgv.Columns[headerStatus].DefaultCellStyle.ForeColor = Color.FromArgb(52, 160, 225);
            dgv.Columns[headerNote].DefaultCellStyle.ForeColor = Color.FromArgb(192, 0, 0);

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Courier New", 6F, FontStyle.Regular);

            //dgv.Columns[headerID].DefaultCellStyle.Font = new Font("Courier New", 6F, FontStyle.Regular);
            //dgv.Columns[headerFactory].DefaultCellStyle.Font = new Font("Courier New", 6F, FontStyle.Regular);
           // dgv.Columns[headerFactory].DefaultCellStyle.Font = new Font("Courier New", 6F, FontStyle.Regular);

            //dgv.Columns[headerStatus].DefaultCellStyle.Font = new Font("Courier New", 10F, FontStyle.Bold);

            dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        #endregion

        #region Load Data

        private void InitializeData()
        {
            tool.loadOUGProductionFactory(cmbFactory);
            ResetData();
        }

        private void loadMachine_NEW()
        {
            if (cmbFactory.SelectedIndex != -1)
            {
                string fac = cmbFactory.Text;

                if (string.IsNullOrEmpty(fac))
                {
                    tool.loadMacIDToComboBox(cmbMachine);
                    cmbMachine.SelectedIndex = -1;
                }
                else if (fac.Equals("All"))
                {
                    tool.loadMacIDToComboBox(cmbMachine);
                    cmbMachine.SelectedIndex = -1;
                }
                else
                {
                    tool.loadMacIDByFactoryToComboBox(cmbMachine, fac);
                }
            }
        }

        private void loadMachine()
        {
            if(cmbFactory.SelectedIndex != -1)
            {
                string fac = cmbFactory.Text;

                if (string.IsNullOrEmpty(fac))
                {
                    tool.loadMacIDToComboBox(cmbMachine);
                    cmbMachine.SelectedIndex = -1;
                }
                else if (fac.Equals("All"))
                {
                    tool.loadMacIDToComboBox(cmbMachine);
                    cmbMachine.SelectedIndex = -1;
                }
                else
                {
                    tool.loadMacIDByFactoryToComboBox(cmbMachine, fac);
                }
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
                    string newFac = row[dalMac.MacLocation].ToString();
                    if (Fac != newFac)
                    {
                        foreach (DataRow row2 in dt.Rows)
                        {
                            if (row2.RowState != DataRowState.Deleted)
                            {
                                string FacSearch = row2[dalMac.MacLocation].ToString();

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

            MatSummaryColoring(dgvSchedule,"",  true, true);

        }
        private void loadScheduleData()
        {
            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            #region Search Filtering
            DataTable dt;
            DateTime startEarliest = DateTime.MinValue, endLatest = DateTime.MinValue;

            string Keywords = txtSearch.Text;
            bool GetCompletedPlanOnly = !cbPending.Checked && !cbRunning.Checked && !cbWarning.Checked && !cbCancelled.Checked && cbCompleted.Checked;


            if (ChangePlanToAction)
            {
                txtSearch.Text = ITEM_CODE;

                Keywords = ITEM_CODE;

                cbItem.Checked = true;
                cbPlanningID.Checked = false;
                cbPending.Checked = true;
                cbCancelled.Checked = true;
                cbCompleted.Checked = true;
                cbWarning.Checked = false;
                cbRunning.Checked = true;
            }

            if (!string.IsNullOrEmpty(Keywords))
            {
                if(cbItem.Checked)
                {
                    //search item
                    dt = dalPlanning.itemSearch(Keywords);
                }
                else
                {
                    //search id
                    dt = dalPlanning.idSearch(Keywords);
                }
            }
            else
            {
                dt = dalPlanning.Select();

            }

            dt = specialDataSort(dt);

            #endregion

            DataTable dt_Schedule = NewScheduleTable();
            DataRow row_Schedule;
            bool match = true;
            string previousLocation = null;
            string previousMachine = null;

            bool OneFactorySeaching = false;

            if (!cmbFactory.Text.Equals("All") && !string.IsNullOrEmpty(cmbFactory.Text) && string.IsNullOrEmpty(cmbMachine.Text))
            {
                OneFactorySeaching = true;
            }
                
            foreach (DataRow row in dt.Rows)
            {
                match = true;

                #region Status Filtering

                string status = row[dalPlanning.planStatus].ToString();

                if(string.IsNullOrEmpty(status))
                {
                    match = false;
                }

                if(!cbPending.Checked &&status.Equals(text.planning_status_pending))
                {
                    match = false;
                }

                if (!cbRunning.Checked && status.Equals(text.planning_status_running))
                {
                    match = false;
                }

                if (!cbWarning.Checked && status.Equals(text.planning_status_warning))
                {
                    match = false;
                }

                if (!cbCancelled.Checked && status.Equals(text.planning_status_cancelled))
                {
                    match = false;
                }

                if (!cbCompleted.Checked && status.Equals(text.planning_status_completed))
                {
                    match = false;
                }

                #endregion

                #region Period Filtering

                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                DateTime from = dtpFrom.Value;
                DateTime to = dtpTo.Value;

                if(!GetCompletedPlanOnly && !((start.Date >= from.Date && start.Date <= to.Date) || (end.Date >= from.Date && end.Date <= to.Date)))
                {
                    match = false;
                }
                else if(GetCompletedPlanOnly && !(end.Date >= from.Date && end.Date <= to.Date))
                {
                    match = false;
                }
                #endregion

                #region Location Filtering

                string factoryFilter = cmbFactory.Text;
                string machineFilter = cmbMachine.Text;

                string factory = row[dalMac.MacLocation].ToString();
                string machine = row[dalMac.MacID].ToString();

                if (!factoryFilter.Equals("All") && !string.IsNullOrEmpty(factoryFilter))
                {
                    if(!factory.Equals(factoryFilter))
                    {
                        match = false;
                    }

                    if(!string.IsNullOrEmpty(machineFilter))
                    {
                        if (!machine.Equals(machineFilter))
                        {
                            match = false;
                        }
                    }
                }

                #endregion

                if (match)
                {
                    if(startEarliest == DateTime.MinValue)
                    {
                        startEarliest = start;
                    }
                    else if(start < startEarliest)
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

                    if (previousLocation == null)
                    {
                        previousLocation = row[dalMac.MacLocation].ToString();
                    }
                    else if(!previousLocation.Equals(row[dalMac.MacLocation].ToString()))
                    {
                        row_Schedule = dt_Schedule.NewRow();
                        dt_Schedule.Rows.Add(row_Schedule);
                        previousLocation = row[dalMac.MacLocation].ToString();
                    }

                    if (previousMachine == null)
                    {
                        previousMachine = row[dalMac.MacID].ToString();
                    }
                    else if (!previousMachine.Equals(row[dalMac.MacID].ToString()))
                    {
                        if(OneFactorySeaching)
                        {
                            row_Schedule = dt_Schedule.NewRow();
                            dt_Schedule.Rows.Add(row_Schedule);
                        }
                       
                        previousMachine = row[dalMac.MacID].ToString();
                    }

                    row_Schedule = dt_Schedule.NewRow();

                    row_Schedule[headerID] = row[dalPlanning.planID];
                    row_Schedule[headerStartDate] = row[dalPlanning.productionStartDate];
                    row_Schedule[headerEndDate] = row[dalPlanning.productionEndDate];

                    row_Schedule[headerFactory] = row[dalMac.MacLocation];
                    row_Schedule[headerMachine] = row[dalMac.MacID];

                    row_Schedule[headerPartName] = row[dalItem.ItemName];
                    row_Schedule[headerPartCode] = row[dalItem.ItemCode];

                    float ct = row[dalPlanning.planCT] == DBNull.Value ? row[dalItem.ItemProCTTo] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProCTTo]) : Convert.ToSingle(row[dalPlanning.planCT]);
                    row_Schedule[headerPartCycleTime] = ct;
                    row_Schedule[headerProductionPurpose] = row[dalPlanning.productionPurpose];

                    row_Schedule[headerTargetQty] = row[dalPlanning.targetQty];
                    row_Schedule[headerAbleProduceQty] = row[dalPlanning.ableQty];

                    if(row[dalPlanning.planProduced].ToString() != "0")
                    row_Schedule[headerProducedQty] = row[dalPlanning.planProduced];

                    row_Schedule[headerMaterial] = row[dalPlanning.materialCode];
                    row_Schedule[headerMaterialBag] = row[dalPlanning.materialBagQty];
                    row_Schedule[headerRecycle] = row[dalPlanning.materialRecycleUse];

                    //row_Schedule[headerColor] = row[dalItem.ItemColor];
                    row_Schedule[headerColorMaterial] = row[dalPlanning.colorMaterialCode];
                    row_Schedule[headerColorMaterialQty] = row[dalPlanning.colorMaterialQty];

                    row_Schedule[headerNote] = row[dalPlanning.planNote];
                    row_Schedule[headerStatus] = row[dalPlanning.planStatus];

                    dt_Schedule.Rows.Add(row_Schedule);
                }
               
            }

            dgvSchedule.DataSource = null;
           

            if (dt_Schedule.Rows.Count > 0)
            {
                if(!GetCompletedPlanOnly)
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

                dgvSchedule.DataSource = dt_Schedule;
                dgvScheduleUIEdit(dgvSchedule);
                ListCellFormatting(dgvSchedule);
                dgvSchedule.ClearSelection();
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

            #region Hide
            DataTable dt;

            //dt = dalPlanning.SelectActivePlanning();
            //if (dt != null)
            //    foreach(DataRow row in dt.Rows)
            //    {
            //        string rawMat = row[dalPlanning.materialCode].ToString();
            //        string colorMat = row[dalPlanning.colorMaterialCode].ToString();
            //        float matQty = 0;
            //        float ableProduce = float.TryParse(row[dalPlanning.ableQty].ToString(), out float i) ? i : 0;
            //        float ProducedQty = float.TryParse(row[dalPlanning.planProduced].ToString(), out i) ? i : 0;

            //        if (Material.Equals(rawMat))
            //        {
            //            float MatBag = float.TryParse(row[dalPlanning.materialBagQty].ToString(), out i) ? i : 0;
            //            matQty = MatBag * 25;

                       

            //            float toProduceQty = ableProduce - ProducedQty;

            //            if(toProduceQty < 0)
            //            {
            //                toProduceQty = 0;
            //            }

            //            float matused_perPcs = matQty / ableProduce;

            //            float matUsed = matused_perPcs * ProducedQty;
            //            float matToUse = matused_perPcs * toProduceQty;

            //            TotalMatUsed += matUsed;
            //            TotalMatToUse += matToUse;

            //            rawType = true;

            //            planCounter++;

            //        }
            //        else if(Material.Equals(colorMat))
            //        {
            //            matQty = float.TryParse(row[dalPlanning.colorMaterialQty].ToString(), out  i) ? i : 0;


            //            float toProduceQty = ableProduce - ProducedQty;

            //            if (toProduceQty < 0)
            //            {
            //                toProduceQty = 0;
            //            }

            //            float matused_perPcs = matQty / ableProduce;

            //            float matUsed = matused_perPcs * ProducedQty;
            //            float matToUse = matused_perPcs * toProduceQty;

            //            TotalMatUsed += matUsed;
            //            TotalMatToUse += matToUse;

            //            colorType = true;

            //            planCounter++;

            //        }


            //        TotalMatPlannedToUse += matQty;

            //    }

            #endregion

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

            if(stock < TotalMatPlannedToUse)
            {
                lblMatStock.ForeColor = Color.Red;
            }
            else
            {
                lblMatStock.ForeColor = Color.Green;

            }
            MatSummaryColoring(dgvSchedule,Material, rawType, colorType);
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

                    if (string.IsNullOrEmpty(row[headerPartCode].ToString()))
                    {
                        dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.FromArgb(64, 64, 64);
                        dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.FromArgb(64, 64, 64);
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.Gainsboro;
                        dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.Gainsboro;
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
                        headerName = headerMaterial;

                    }
                    else if (colorMode)
                    {
                        headerName = headerColorMaterial;
                    }

                    matCode_DGV = row[headerName].ToString();

                    if (matCode_DGV == materialCode)
                    {
                        dgv.Rows[rowIndex].Cells[headerName].Style.BackColor = Color.OrangeRed;

                        if (rawMode)
                        {
                            dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.Gainsboro;
                        }
                        else if (colorMode)
                        {
                            dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.Gainsboro;

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(row[headerPartCode].ToString()))
                        {
                            dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.FromArgb(64, 64, 64);
                            dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.FromArgb(64, 64, 64);
                        }
                        else
                        {
                            dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.Gainsboro;
                            dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.Gainsboro;
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
                string planID_DB = row[dalPlanning.planID].ToString();

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
            dgvSchedule.ClearSelection();

            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            loaded = true;
            loadMachine();
            cmbFactory.SelectedIndex = 0;

            if (ItemProductionHistoryChecking)
            {
                cbCompleted.Checked = true;
                cbPending.Checked = true;

                dtpFrom.Value = new DateTime(2019, 01, 01);
                dtpTo.Value = DateTime.Today;
            }

            loadScheduleData();

            if(fromDailyRecord)
            {
                dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            else
            {
                dgvSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

            }

           

        }

        #endregion

        #region Button Click to Plan Page

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            frmNewPlanning frm = new frmNewPlanning();

            // frmPlanning frm = new frmPlanning();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            loadScheduleData();

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
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (fromDailyRecord && MessageBox.Show("Are you sure you want to add this item to the list?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = false;
                foreach(DataGridViewRow row in dgvSchedule.SelectedRows)
                {
                    string planID = row.Cells[headerID].Value.ToString();
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
                foreach (DataGridViewRow row in dgvSchedule.SelectedRows)
                {
                    string planID = row.Cells[headerID].Value.ToString();
                    string itemCode = row.Cells[headerPartCode].Value.ToString();

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

                                DataTable dt_test = (DataTable)dgvSchedule.DataSource;

                                //int row = dgvSchedule.RowCount - 1;
                                //int col = dgvSchedule.ColumnCount - 1;

                                //MessageBox.Show("row: " + row + " col: " + col);
                                if (true)//cmbSubType.Text.Equals("PMMA")
                                {
                                    for (int i = 0; i <= dgvSchedule.RowCount - 1; i++)
                                    {
                                        for (int j = 0; j <= dgvSchedule.ColumnCount - 1; j++)
                                        {
                                            Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];
                                            range.Rows.RowHeight = 30;
                                            range.Font.Color = ColorTranslator.ToOle(dgvSchedule.Rows[i].Cells[j].InheritedStyle.ForeColor);

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

                                            if (dgvSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Gainsboro)
                                            {
                                                range.Interior.Color = Color.White;
                                                range.Rows.RowHeight = 40;
                                            }
                                            else if (dgvSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.FromArgb(64,64,64))
                                            {
                                                range.Rows.RowHeight = 4;
                                                range.Interior.Color = Color.Black;
                                            }
                                            else
                                            {
                                                range.Interior.Color = ColorTranslator.ToOle(dgvSchedule.Rows[i].Cells[j].InheritedStyle.BackColor);
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
                                dgvSchedule.ClearSelection();

                                // Open the newly saved excel file
                                if (File.Exists(sfd.FileName))
                                    System.Diagnostics.Process.Start(sfd.FileName);
                            }

                            dgvSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

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


                        DataTable dt_Main = (DataTable)dgvSchedule.DataSource;
                        GetStocktakeData(dt_Main);


                        //export to excel
                        NewExcel();

                    }

                }
               
            }

            dgvSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect ;


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
                    string ItemName = row[headerPartName].ToString();
                    string ItemCode = row[headerPartCode].ToString();
                    string RawMat = row[headerMaterial].ToString();
                    string ColorMat = row[headerColorMaterial].ToString();

                    string Fac = row[headerFactory].ToString();

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
            dgvSchedule.SelectAll();
            DataObject dataObj = dgvSchedule.GetClipboardContent();
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
                cbPlanningID.Checked = false;
            }
        }

        private void cbPlanningID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlanningID.Checked)
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
            if (cmbFactory.Text != null && loaded && ableLoadData)
            {
                ableLoadData = false;

                loadMachine();
                ableLoadData = true;
            }
        }

        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMachine.Text != null && ableLoadData)
            {
                ableLoadData = false;
                cmbFactory.Text = tool.getFactoryNameFromMachineID(cmbMachine.Text);
                ableLoadData = true;
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
            cmbFactory.SelectedIndex = 0;
            txtSearch.Clear();
            cbItem.Checked = true;
            ResetStatusToDefault();

            DateTime todayDate = DateTime.Today;
            dtpFrom.Value = todayDate.AddDays(-180);
            dtpTo.Value = todayDate.AddDays(180);
            ActiveControl = dgvSchedule;

        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            ResetData();

            dgvSchedule.ClearSelection();
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
                loadScheduleData();
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
            if (cbPlanningID.Checked)
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

        #region material list check

        private void btnMatList_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            frmMatPlanningList frm = new frmMatPlanningList();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        #endregion

        #region datagridview cell formatting

        private void dgvSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           // DataGridView dgv = dgvSchedule;
           // dgv.SuspendLayout();

           // int row = e.RowIndex;
           // int col = e.ColumnIndex;

           //if (dgv.Columns[col].Name == headerStartDate)
           // {
           //     string value = dgv.Rows[row].Cells[headerStatus].Value.ToString();
           //     Color ColorSet = dgv.DefaultCellStyle.BackColor;

           //     if (value.Equals(text.planning_status_cancelled))
           //     {
           //         ColorSet = Color.White;
           //     }
           //     else if (value.Equals(text.planning_status_completed))
           //     {
           //         ColorSet = Color.Gainsboro;
           //     }
           //     else if (value.Equals(text.planning_status_delayed))
           //     {
           //         ColorSet = Color.FromArgb(255, 255, 128);
           //     }
           //     else if (value.Equals(text.planning_status_pending))
           //     {
           //         ColorSet = Color.Gainsboro;
           //     }
           //     else if (value.Equals(text.planning_status_running))
           //     {
           //         ColorSet = Color.FromArgb(0, 184, 148);
           //     }
           //     else if (value.Equals(text.planning_status_warning))
           //     {
           //         ColorSet = Color.LightGreen;
           //     }
           //     else if (value.Equals(""))
           //     {
           //         ColorSet = Color.FromArgb(64, 64, 64);
           //     }

           //     dgv.Rows[row].Cells[headerStatus].Style.BackColor = ColorSet;
           //     dgv.Rows[row].Cells[headerStatus].Style.ForeColor = Color.Black;

           //     if (value == "")
           //     {
           //         dgv.Rows[row].Height = 12;
           //         dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
           //     }
           //     else
           //     {
           //         dgv.Rows[row].Height = 50;
           //     }

           //     string status = dgv.Rows[row].Cells[headerStatus].Value.ToString();

           //     if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed) && status != "")
           //     {
           //         DataTable dt = (DataTable)dgv.DataSource;
           //         DateTime startDate = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
           //         int macID = Convert.ToInt32(dgv.Rows[row].Cells[headerMachine].Value);

           //         if(row+1 <= dgv.Rows.Count - 1)
           //         {
           //             for (int i = row + 1; i < dgv.Rows.Count; i++)
           //             {
           //                 string otherStatus = dgv.Rows[i].Cells[headerStatus].Value.ToString();
           //                 if (!otherStatus.Equals(text.planning_status_cancelled) && !otherStatus.Equals(text.planning_status_completed) && otherStatus != "")
           //                 {
           //                     DateTime otherStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
           //                     int otherMacID = Convert.ToInt32(dgv.Rows[i].Cells[headerMachine].Value);
           //                     if (startDate == otherStart && macID == otherMacID)
           //                     {
           //                         int planID = Convert.ToInt32(dgv.Rows[row].Cells[headerID].Value);
           //                         int otherPlanID = Convert.ToInt32(dgv.Rows[i].Cells[headerID].Value);

           //                         if(checkIfFamilyMould(planID,otherPlanID))
           //                         {
           //                             dgv.Rows[row].Cells[headerStartDate].Style.BackColor = Color.Yellow;
           //                             dgv.Rows[i].Cells[headerStartDate].Style.BackColor = Color.Yellow;
           //                         }
                                   

           //                     }
           //                 }
                                
           //             }
           //         }
                    




           //         //dgv.Rows[row].Cells[headerStartDate].Style.BackColor = Color.Gainsboro;

           //     }
           // }

           // dgv.ResumeLayout();
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
            else if (PlanStatus.Equals(""))
            {
                ColorSet = Color.FromArgb(64, 64, 64);
            }

            return ColorSet;
        }

        private void ListCellFormatting(DataGridView dgv)
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

                string PlanStatus = row[headerStatus].ToString();

                Color ColorSet = GetColorSetFromPlanStatus(PlanStatus, dgv);

                dgv.Rows[rowIndex].Cells[headerStatus].Style.BackColor = ColorSet;
                dgv.Rows[rowIndex].Cells[headerStatus].Style.ForeColor = Color.Black;

                if (PlanStatus == "")
                {
                   // dgv.Rows[rowIndex].Height = 12;
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);

                    if (!cmbFactory.Text.Equals("All") && !string.IsNullOrEmpty(cmbFactory.Text) && string.IsNullOrEmpty(cmbMachine.Text))
                    {
                        dgv.Rows[rowIndex].Height = 10;
                    }

                    dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.FromArgb(64, 64, 64);
                    dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.FromArgb(64, 64, 64);

                }
                else
                {
                    // dgv.Rows[rowIndex].Height = 50;
                    dgv.Rows[rowIndex].Cells[headerMaterial].Style.BackColor = Color.Gainsboro;
                    dgv.Rows[rowIndex].Cells[headerColorMaterial].Style.BackColor = Color.Gainsboro;
                }


                if (!PlanStatus.Equals(text.planning_status_cancelled) && !PlanStatus.Equals(text.planning_status_completed) && PlanStatus != "")
                {
                    DateTime startDate = Convert.ToDateTime(row[headerStartDate].ToString());

                    int macID = Convert.ToInt32(row[headerMachine].ToString());

                    if (rowIndex + 1 <= dgv.Rows.Count - 1)
                    {
                        for (int i = rowIndex + 1; i < dgv.Rows.Count; i++)
                        {
                            string otherStatus = dgv.Rows[i].Cells[headerStatus].Value.ToString();

                            if (!otherStatus.Equals(text.planning_status_cancelled) && !otherStatus.Equals(text.planning_status_completed) && otherStatus != "")
                            {
                                DateTime otherStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
                                int otherMacID = Convert.ToInt32(dgv.Rows[i].Cells[headerMachine].Value);
                                if (startDate == otherStart && macID == otherMacID)
                                {
                                    int planID = Convert.ToInt32(row[headerID].ToString());
                                    int otherPlanID = Convert.ToInt32(dgv.Rows[i].Cells[headerID].Value);

                                    if (checkIfFamilyMould(planID, otherPlanID))
                                    {
                                        dgv.Rows[rowIndex].Cells[headerStartDate].Style.BackColor = Color.Yellow;
                                        dgv.Rows[i].Cells[headerStartDate].Style.BackColor = Color.Yellow;
                                    }
                                }
                            }

                        }
                    }
                }
            }

            dgv.ResumeLayout();
        }

        private void oldCellFormatting()
        {
            //DataGridView dgv = dgvSchedule;
            //dgv.SuspendLayout();

            //int row = e.RowIndex;
            //int col = e.ColumnIndex;

            //if (dgv.Columns[col].Name == headerStatus)
            //{
            //    string value = dgv.Rows[row].Cells[headerStatus].Value.ToString();
            //    Color ColorSet = dgv.DefaultCellStyle.BackColor;

            //    if (value.Equals(text.planning_status_cancelled))
            //    {
            //        ColorSet = Color.White;
            //    }
            //    else if (value.Equals(text.planning_status_completed))
            //    {
            //        ColorSet = Color.Gainsboro;
            //    }
            //    else if (value.Equals(text.planning_status_delayed))
            //    {
            //        ColorSet = Color.FromArgb(255, 255, 128);
            //    }
            //    else if (value.Equals(text.planning_status_pending))
            //    {
            //        ColorSet = Color.Gainsboro;
            //    }
            //    else if (value.Equals(text.planning_status_running))
            //    {
            //        ColorSet = Color.FromArgb(0, 184, 148);
            //    }
            //    else if (value.Equals(text.planning_status_warning))
            //    {
            //        ColorSet = Color.LightGreen;
            //    }
            //    else if (value.Equals(""))
            //    {
            //        ColorSet = Color.FromArgb(64, 64, 64);
            //    }

            //    dgv.Rows[row].Cells[headerStatus].Style.BackColor = ColorSet;
            //    dgv.Rows[row].Cells[headerStatus].Style.ForeColor = Color.Black;

            //    if (value == "")
            //    {
            //        dgv.Rows[row].Height = 12;
            //        dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            //    }
            //    else
            //    {
            //        dgv.Rows[row].Height = 50;
            //    }
            //}

            //else if (dgv.Columns[col].Name == headerStartDate)
            //{
            //    string status = dgv.Rows[row].Cells[headerStatus].Value.ToString();

            //    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed) && status != "")
            //    {
            //        DataTable dt = (DataTable)dgv.DataSource;
            //        DateTime startDate = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
            //        int macID = Convert.ToInt32(dgv.Rows[row].Cells[headerMachine].Value);

            //        if (row + 1 <= dgv.Rows.Count - 1)
            //        {
            //            for (int i = row + 1; i < dgv.Rows.Count; i++)
            //            {
            //                string otherStatus = dgv.Rows[i].Cells[headerStatus].Value.ToString();
            //                if (!otherStatus.Equals(text.planning_status_cancelled) && !otherStatus.Equals(text.planning_status_completed) && otherStatus != "")
            //                {
            //                    DateTime otherStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
            //                    int otherMacID = Convert.ToInt32(dgv.Rows[i].Cells[headerMachine].Value);
            //                    if (startDate == otherStart && macID == otherMacID)
            //                    {
            //                        int planID = Convert.ToInt32(dgv.Rows[row].Cells[headerID].Value);
            //                        int otherPlanID = Convert.ToInt32(dgv.Rows[i].Cells[headerID].Value);

            //                        if (checkIfFamilyMould(planID, otherPlanID))
            //                        {
            //                            dgv.Rows[row].Cells[headerStartDate].Style.BackColor = Color.Yellow;
            //                            dgv.Rows[i].Cells[headerStartDate].Style.BackColor = Color.Yellow;
            //                        }


            //                    }
            //                }

            //            }
            //        }





            //        //dgv.Rows[row].Cells[headerStartDate].Style.BackColor = Color.Gainsboro;

            //    }
            //}

            //dgv.ResumeLayout();
        }

        #region plan status change

        private bool planRunning(int rowIndex, string presentStatus)
        {
            bool statusChanged = false;

            uPlanning.plan_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerID].Value;
            uPlanning.machine_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerMachine].Value;
            uPlanning.production_start_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerStartDate].Value;
            uPlanning.production_end_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerEndDate].Value;
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
            dgvSchedule.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            DataGridView dgv = dgvSchedule;
            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
            int colIndex = dgv.CurrentCell.ColumnIndex;

            int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);

            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerMachine].Value);

            string presentStatus = dgv.Rows[rowIndex].Cells[headerStatus].Value.ToString();

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
                        DataTable dt = (DataTable)dgvSchedule.DataSource;
                        if (cbPending.Checked)
                        {
                            dt.Rows[rowIndex][headerStatus] = text.planning_status_pending;
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
                    DataTable dt = (DataTable)dgvSchedule.DataSource;
                    if (cbRunning.Checked)
                    {
                        dt.Rows[rowIndex][headerStatus] = text.planning_status_running;
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
                        DataTable dt = (DataTable)dgvSchedule.DataSource;
                        if (cbCompleted.Checked)
                        {
                            dt.Rows[rowIndex][headerStatus] = text.planning_status_completed;
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
                        DataTable dt = (DataTable)dgvSchedule.DataSource;
                        if (cbCancelled.Checked)
                        {
                            dt.Rows[rowIndex][headerStatus] = text.planning_status_cancelled;
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

            ListCellFormatting(dgvSchedule);

            if (itemClicked.Equals(text.planning_Material_Summary))
            {
                CalculationMaterialSummary(cellValue);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgvSchedule.ResumeLayout();
        }

        private void editSchedule(int rowIndex)
        {
            uPlanning.plan_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerID].Value;
            uPlanning.machine_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerMachine].Value;
            uPlanning.production_start_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerStartDate].Value;
            uPlanning.production_end_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerEndDate].Value;

            frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(frmMachineScheduleAdjustFromMain.applied)
            {
                loadScheduleData();
                dgvSchedule.FirstDisplayedScrollingRowIndex = rowIndex;
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
                dgvSchedule.CurrentCell = dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvSchedule.Rows[e.RowIndex].Selected = true;
                dgvSchedule.Focus();
                int rowIndex = dgvSchedule.CurrentCell.RowIndex;
                int colIndex = dgvSchedule.CurrentCell.ColumnIndex;

                string currentHeader = dgvSchedule.Columns[colIndex].Name;


                try
                {
                    string result = dgvSchedule.Rows[rowIndex].Cells[headerStatus].Value.ToString();

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

                    if(currentHeader.Equals(headerMaterial) || currentHeader.Equals(headerColorMaterial))
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
                string planID = dgvSchedule.Rows[e.RowIndex].Cells[headerID].Value.ToString();
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
                loadScheduleData();
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

                DataGridView dgv = dgvSchedule;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (dgv.Columns[colIndex].Name.Contains(headerNote)) //Desired Column
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
                    int planID = int.TryParse(dgvSchedule.Rows[e.RowIndex].Cells[headerID].Value.ToString(), out planID) ? planID : 0;

                    planNoteUpdate(planID);
                }
            }
        }

        private void dgvSchedule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CELL_VALUE_CHANGED = true;

            DataGridView dgv = dgvSchedule;

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
                string headerName = dgvSchedule.Columns[e.ColumnIndex].Name;

                if(headerName == headerMaterial || headerName == headerColorMaterial)
                {
                    string cellValue = dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

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
