using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMachineScheduleAdjustFromMain : Form
    {

        #region Variable/ Object

        readonly string headerPlanID = "PLAN ID";
        readonly string headerStartDate = "START";
        readonly string headerEndDate = "ESTIMATE END";
        readonly string headerPartName = "NAME";
        readonly string headerPartCode = "CODE";
        readonly string headerProductionPurpose = "PRODUCTION FOR";
        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerStatus = "STATUS";
        readonly string headerFamilyWith= "FAMILY WITH";
        readonly string headerRemark = "REMARK";
        readonly string changeDate = "Change Date";
        readonly string familyWith = "Family With";
        readonly string resetFamilyWith = "Reset Family With";
        readonly string headerToRun = "TO RUN";
        readonly string ToAdd = "TO ADD";
        readonly string ToEdit = "TO EDIT";

        private string MacID = "";
        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        planningActionDAL dalPlanningAction = new planningActionDAL();

        itemDAL dalItem = new itemDAL();
        Text text = new Text();
        Tool tool = new Tool();

        int formClose = -1;
        bool blink = false;
        bool dataChange = false;
        bool loaded = false;
        bool ableLoadDate = true;
        bool ableLoadSchedule = true;
        bool ableChangeDTP = true;
        bool RunAction = false;
        bool AddAction = false;
        static public bool applied = false;

        static public DateTime start;
        static public DateTime end;

        static public int ToAddPlanFamilyWith = -1;
        private int currentSelectedRow = -1;
        private int selectedRow = -1;
        #endregion

        public frmMachineScheduleAdjustFromMain()
        {
            InitializeComponent();
        }

        //call from main schedule
        public frmMachineScheduleAdjustFromMain(PlanningBLL u, bool RunActionMake)
        {
            InitializeComponent();
            uPlanning = u;
            MacID = u.machine_id.ToString();
            RunAction = RunActionMake;

            lblStartDate.Text = "TO RUN START DATE";
            lblEndDate.Text = "TO RUN END DATE";

            if (RunActionMake)
            {
                dtpStartDate.Value = u.production_start_date;
                dtpEstimateEndDate.Value = u.production_end_date;
            }
            else
            {
                simpleScheduleEditUI();
                headerToRun = text.planning_status_running;
            }
        }

        //call from planning apply
        public frmMachineScheduleAdjustFromMain(PlanningBLL u)
        {
            InitializeComponent();
            uPlanning = u;
            MacID = u.machine_id.ToString();
            AddAction = true;

            lblStartDate.Text = "TO ADD START DATE";
            lblEndDate.Text = "TO ADD END DATE";

            dtpStartDate.Value = u.production_start_date;
            dtpEstimateEndDate.Value = u.production_end_date;

            start = dtpStartDate.Value.Date;
            end = dtpEstimateEndDate.Value.Date;
        }

        #region UI Setting

        private void simpleScheduleEditUI()
        {
            dtpStartDate.Visible = false;
            dtpEstimateEndDate.Visible = false;
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            cbIncludeSunday.Visible = false;


            lblMachineSchedule.Location = new Point(lblMachineSchedule.Left, lblMachineSchedule.Top - 80);
            lblWarning.Location = new Point(lblWarning.Left, lblWarning.Top - 80);

            dgvMac.Location = new Point(dgvMac.Left, dgvMac.Top - 80);

            btnAutoAdjust.Location = new Point(btnAutoAdjust.Left, btnAutoAdjust.Top - 80);
            btnMoveUp.Location = new Point(btnMoveUp.Left, btnMoveUp.Top - 80);
            btnMoveDown.Location = new Point(btnMoveDown.Left, btnMoveDown.Top - 80);
            btnRefresh.Location = new Point(btnRefresh.Left, btnRefresh.Top - 80);
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerPlanID, typeof(int));
            dt.Columns.Add(headerStatus, typeof(string));
            dt.Columns.Add(headerStartDate, typeof(DateTime));
            dt.Columns.Add(headerEndDate, typeof(DateTime));

            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerTargetQty, typeof(int));
            dt.Columns.Add(headerProductionPurpose, typeof(string));
            dt.Columns.Add(headerFamilyWith, typeof(int));
            dt.Columns.Add(headerRemark, typeof(string));
            return dt;
        }

        private void dgvScheduleUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerPlanID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStartDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerEndDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerProductionPurpose].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTargetQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStatus].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFamilyWith].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerRemark].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////

            dgv.Columns[headerPlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStartDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerEndDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTargetQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFamilyWith].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerRemark].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        #endregion

        #region Load Data

        private void loadScheduleData()
        {
            blink = false;
            if (ableLoadSchedule)
            {
                dtpStartDate.Value = uPlanning.production_start_date;
                dtpEstimateEndDate.Value = uPlanning.production_end_date;

                dataChange = false;

                string macID = uPlanning.machine_id.ToString();

                DataTable dt = dalPlanning.macIDSearch(macID);

                DataTable dt_Schedule = NewScheduleTable();
                DataRow row_Schedule;
                bool match = true;
                int referFamilyWith = tool.getFamilyWithData(uPlanning.plan_id);
                foreach (DataRow row in dt.Rows)
                {
                    match = true;
                    int planID = Convert.ToInt32(row[dalPlanning.jobNo]);
                    int familyWith = Convert.ToInt32(row[dalPlanning.familyWith]);
                    #region Status Filtering

                    string status = row[dalPlanning.planStatus].ToString();

                    if (string.IsNullOrEmpty(status))
                    {
                        match = false;
                    }

                    if (status.Equals(text.planning_status_cancelled))
                    {
                        match = false;
                    }

                    if (status.Equals(text.planning_status_completed))
                    {
                        match = false;
                    }

                    #endregion

                    if (match)
                    {
                        row_Schedule = dt_Schedule.NewRow();

                        row_Schedule[headerPlanID] = planID;
                        row_Schedule[headerStartDate] = row[dalPlanning.productionStartDate];
                        row_Schedule[headerEndDate] = row[dalPlanning.productionEndDate];

                        row_Schedule[headerPartName] = row[dalItem.ItemName];
                        row_Schedule[headerPartCode] = row[dalItem.ItemCode];
                        row_Schedule[headerProductionPurpose] = row[dalPlanning.productionPurpose];

                        row_Schedule[headerTargetQty] = row[dalPlanning.targetQty];
                        row_Schedule[headerStatus] = row[dalPlanning.planStatus];

                        if ((planID == uPlanning.plan_id || referFamilyWith == familyWith )&& RunAction)
                        {

                            if(planID == uPlanning.plan_id)
                            {
                                row_Schedule[headerStatus] = headerToRun;
                            }
                            else if (referFamilyWith != -1)
                            {
                                row_Schedule[headerStatus] = headerToRun;
                            }
                        }

                        row_Schedule[headerFamilyWith] = row[dalPlanning.familyWith];
                        row_Schedule[headerRemark] = row[dalPlanning.planNote];
                        dt_Schedule.Rows.Add(row_Schedule);
                    }

                }

                if(AddAction)
                {
                    row_Schedule = dt_Schedule.NewRow();

                    row_Schedule[headerPlanID] = -1;
                    row_Schedule[headerStartDate] = dtpStartDate.Value.Date;
                    row_Schedule[headerEndDate] = dtpEstimateEndDate.Value.Date;

                    row_Schedule[headerPartName] = uPlanning.part_name;
                    row_Schedule[headerPartCode] = uPlanning.part_code;
                    row_Schedule[headerProductionPurpose] = uPlanning.production_purpose;

                    row_Schedule[headerTargetQty] = uPlanning.production_target_qty;
                    row_Schedule[headerStatus] = ToAdd;

                    row_Schedule[headerFamilyWith] = uPlanning.family_with;
                    row_Schedule[headerRemark] = uPlanning.plan_remark;
                    dt_Schedule.Rows.Add(row_Schedule);
                }

                dgvMac.DataSource = null;

                if (dt_Schedule.Rows.Count > 0)
                {
                    DataTable dtSorted = new DataTable();
                    dt_Schedule.DefaultView.Sort = headerStartDate+" asc,"+headerEndDate+" asc";
                    dtSorted = dt_Schedule.DefaultView.ToTable();

                    dgvMac.DataSource = dtSorted;
                    dgvScheduleUIEdit(dgvMac);
                    dgvMac.ClearSelection();
                    currentSelectedRow = -1;
                }
            }

        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMachineScheduleAdjustFromMain_Load(object sender, EventArgs e)
        {
            loaded = true;
            loadScheduleData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadScheduleData();
        }

        private void dgvMac_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMac;
            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            string value = dgv.Rows[row].Cells[headerStatus].Value.ToString();

            if (dgv.Columns[col].Name == headerStatus)
            {
                
                Color backColor = dgv.DefaultCellStyle.BackColor;

                if (value.Equals(text.planning_status_cancelled))
                {
                    backColor = Color.White;
                }
                else if (value.Equals(text.planning_status_completed))
                {
                    backColor = Color.White;
                }
                else if (value.Equals(text.planning_status_delayed))
                {
                    backColor = Color.FromArgb(255, 255, 128);
                }
                else if (value.Equals(text.planning_status_pending))
                {
                    backColor = Color.FromArgb(52, 160, 225);
                }
                else if (value.Equals(text.planning_status_running) || value.Equals(headerToRun) )
                {
                    backColor = Color.FromArgb(0, 184, 148);
                }
                else if (value.Equals(ToAdd) || value.Equals(ToEdit))
                {
                    backColor = Color.FromArgb(253, 203, 110);
                }
                else if (value.Equals(text.planning_status_warning))
                {
                    backColor = Color.FromArgb(255, 118, 117);
                }
                dgv.Rows[row].Cells[headerStatus].Style.BackColor = backColor;

                if (backColor != dgv.DefaultCellStyle.BackColor)
                {
                    dgv.Rows[row].Cells[headerStatus].Style.ForeColor = Color.Black;

                }
                else
                {
                    dgv.Rows[row].Cells[headerStatus].Style.ForeColor = Color.White;
                }

            }

            else if (dgv.Columns[col].Name == headerEndDate && !value.Equals(text.planning_status_cancelled) && !value.Equals(text.planning_status_completed))
            {
                dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
                dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;

                for (int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    string status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

                    if (status.Equals(text.planning_status_cancelled) || status.Equals(text.planning_status_completed))
                    {
                        dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
                        dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
                    }
                    else if(i != row)
                    {
                        int firstFamilyWith = -1;
                        int secondFamilyWith = -1;

                        if (i >= 0 && dgv.Rows[i].Cells[headerFamilyWith].Value != DBNull.Value)
                        {
                            firstFamilyWith = Convert.ToInt32(dgv.Rows[i].Cells[headerFamilyWith].Value);
                        }

                         
                        if (row >= 0 && dgv.Rows[row].Cells[headerFamilyWith].Value != DBNull.Value)
                        {
                            secondFamilyWith = Convert.ToInt32(dgv.Rows[row].Cells[headerFamilyWith].Value);
                        }

                        if(firstFamilyWith != secondFamilyWith || (firstFamilyWith == -1 || secondFamilyWith == -1))
                        {
                            if (i < row)
                            {
                                DateTime start = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
                                DateTime end = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value);

                                DateTime secondStart = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
                                DateTime secondEnd = Convert.ToDateTime(dgv.Rows[row].Cells[headerEndDate].Value);

                                if (!tool.ifProductionDateAvailable(start, end, secondStart, secondEnd))
                                {
                                    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Red;
                                    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Red;
                                    break;
                                }

                            }
                            else
                            {
                                DateTime start = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
                                DateTime end = Convert.ToDateTime(dgv.Rows[row].Cells[headerEndDate].Value);

                                DateTime secondStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
                                DateTime secondEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value);

                                if (!tool.ifProductionDateAvailable(start, end, secondStart, secondEnd))
                                {
                                    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Red;
                                    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Red;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (value.Equals(text.planning_status_cancelled) || value.Equals(text.planning_status_completed))
            {
                dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
                dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
            }
        }

        private bool Validation()
        {
            bool result = true;

            if (!checkAllDate())
            {
                result = false;
                blink = true;
                errorProvider1.SetError(lblMachineSchedule, "Production date clash with other plan");
                Blink("One of the production date clash with other plan!");
            }

            return result;

        }

        private bool checkAllDate()
        {
            blink = false;
            bool result = true;
            DataTable dt = (DataTable)dgvMac.DataSource;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                if (dgvMac.Rows[i].Cells[headerStartDate].Style.ForeColor == Color.Red)
                {
                    result = false;
                }
            }

            return result;
        }

        private async void Blink(string warning)
        {
            while (blink)
            {
                lblWarning.Text = warning;
                lblWarning.Visible = true;
                await Task.Delay(500);
                lblWarning.BackColor = lblWarning.BackColor == Color.Red ? Color.White : Color.Red;
            }
            lblWarning.Visible = false;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            blink = false;
            errorProvider1.Clear();

            bool abletoChangeData = true;

            DataGridView dgv = dgvMac;
            lblMachineSchedule.Focus();
            dgv.Focus();
            bool includeSunday = false;
            if(cbIncludeSunday.Checked)
            {
                includeSunday = true;
            }

            int row = dgv.CurrentRow.Index;

            if(currentSelectedRow != -1)
            {
                row = currentSelectedRow;
            }
           

            if (row >= 0 && row != dgv.RowCount -1)
            {
                PlanningBLL u = new PlanningBLL();

                u.plan_id = (int)dgv.Rows[row].Cells[headerPlanID].Value;
                u.plan_status = (string)dgv.Rows[row].Cells[headerStatus].Value;

                if(u.plan_status.Equals(text.planning_status_running) || u.plan_status.Equals(headerToRun))
                {
                    DialogResult dialogResult;
                    if (u.plan_status.Equals(text.planning_status_running))
                    {
                        dialogResult = MessageBox.Show("PLAN " + u.plan_id + " is RUNNING now, you want to switch it to PENDING?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    else
                    {
                        dialogResult = MessageBox.Show("PLAN " + u.plan_id + " is planning to run, you want to switch it to PENDING?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }

                    if (dialogResult == DialogResult.Yes)
                    {
                        abletoChangeData = true;
                        u.plan_status = text.planning_status_pending;
                    }
                    else
                    {
                        abletoChangeData = false;
                    }
                }

                if(abletoChangeData)
                {
                    u.production_start_date = (DateTime)dgv.Rows[row].Cells[headerStartDate].Value;
                    u.production_end_date = (DateTime)dgv.Rows[row].Cells[headerEndDate].Value;
                    u.part_code = (string)dgv.Rows[row].Cells[headerPartCode].Value;
                    u.part_name = (string)dgv.Rows[row].Cells[headerPartName].Value;
                    u.production_purpose = (string)dgv.Rows[row].Cells[headerProductionPurpose].Value;
                    u.production_target_qty = dgv.Rows[row].Cells[headerTargetQty].Value.ToString();
                    u.family_with = Convert.ToInt32(dgv.Rows[row].Cells[headerFamilyWith].Value);
                    u.plan_remark = dgv.Rows[row].Cells[headerRemark].Value.ToString();
                    DataTable dt = (DataTable)dgv.DataSource;

                    dt.Rows.RemoveAt(row);

                    int position = row + 1;

                    DataRow dr;
                    dr = dt.NewRow();

                    dr[headerPlanID] = u.plan_id;
                    dr[headerStartDate] = u.production_start_date;
                    dr[headerEndDate] = u.production_end_date;
                    dr[headerPartName] = u.part_name;
                    dr[headerPartCode] = u.part_code;
                    dr[headerProductionPurpose] = u.production_purpose;
                    dr[headerTargetQty] = u.production_target_qty;
                    dr[headerStatus] = u.plan_status;
                    dr[headerFamilyWith] = u.family_with;
                    dr[headerRemark] = u.plan_remark;

                    dt.Rows.InsertAt(dr, position);



                    dgv.ClearSelection();
                    dgvMac.Rows[position].Selected = true;
                    dgvMac.Focus();
                    currentSelectedRow = position;
                    dataChange = true;
                }
                
            }
          
            else if(row == 0)
            {
                blink = true;
                Blink("Please select a row.");
            }
        }

        private DateTime addOneDay(DateTime day, bool includeSunday)
        {
            day = day.AddDays(1);

            if (!includeSunday && tool.checkIfSunday(day))
            {
                day = day.AddDays(1);
            }
            return day.Date;
        }

        private DataTable newAutoAdjustDate()
        {
            bool includeSunday = false;

            if (cbIncludeSunday.Checked)
            {
                includeSunday = true;
            }

            DataTable dt = (DataTable)dgvMac.DataSource;
            DataTable new_dt = NewScheduleTable();
            DataGridView dgv = dgvMac;

            DateTime previousRowStart = new DateTime();
            DateTime previousRowEnd = new DateTime();
            int previousRowFamilyWith = -1;

            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    string status = row[headerStatus].ToString();
                    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
                    {
                        previousRowFamilyWith = Convert.ToInt32(row[headerFamilyWith]);

                        if (previousRowFamilyWith != -1)
                        {
                            DateTime earliestStart;
                            DateTime latestEnd;

                            if (previousRowEnd == default(DateTime))
                            {
                                previousRowStart = Convert.ToDateTime(row[headerStartDate]).Date;
                                previousRowEnd = Convert.ToDateTime(row[headerEndDate]).Date;
                                earliestStart = previousRowStart;
                            }
                            else
                            {
                                earliestStart = addOneDay(previousRowEnd, includeSunday);
                            }

                            latestEnd = Convert.ToDateTime(row[headerEndDate]).Date;

                            foreach (DataRow row2 in dt.Rows)
                            {
                                if (row2.RowState != DataRowState.Deleted)
                                {
                                    int familyWith = Convert.ToInt32(row2[headerFamilyWith]);
                                    int PlanID = Convert.ToInt32(row2[headerPlanID]);
                                    if (familyWith == previousRowFamilyWith)
                                    {
                                        DateTime start = Convert.ToDateTime(row2[headerStartDate]).Date;
                                        DateTime end = Convert.ToDateTime(row2[headerEndDate]).Date;

                                        //int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);
                                        int proDayRequired = tool.getNumberOfProDay(PlanID);

                                        row2[headerStartDate] = earliestStart.Date;

                                        end = tool.EstimateEndDate(earliestStart.Date, proDayRequired, includeSunday);

                                        row2[headerEndDate] = end;

                                        if (latestEnd < end)
                                        {
                                            latestEnd = end;
                                        }

                                        DataRow newRow = new_dt.NewRow();
                                        newRow = row2;
                                        new_dt.ImportRow(newRow);
                                        previousRowEnd = latestEnd;
                                        row2.Delete();
                                    }
                                }
                                 
                            }
                        }
                        else
                        {
                            DateTime start = Convert.ToDateTime(row[headerStartDate]).Date;
                            DateTime end = Convert.ToDateTime(row[headerEndDate]).Date;
                            int PlanID = Convert.ToInt32(row[headerPlanID]);

                            //get data from db///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);
                            int proDayRequired = tool.getNumberOfProDay(PlanID);

                            if (previousRowEnd == default(DateTime))
                            {
                                previousRowEnd = Convert.ToDateTime(row[headerStartDate]).Date;
                            }
                            else
                            {
                                previousRowEnd = addOneDay(previousRowEnd, includeSunday);
                                
                            }

                            row[headerStartDate] = previousRowEnd.Date;

                            previousRowEnd = tool.EstimateEndDate(previousRowEnd, proDayRequired, includeSunday);

                            row[headerEndDate] = previousRowEnd.Date;

                            DataRow newRow = new_dt.NewRow();
                            newRow = row;
                            new_dt.ImportRow(newRow);
                            row.Delete();
                        }
                    }
                    
                    else
                    {
                        DataRow newRow = new_dt.NewRow();
                        newRow = row;
                        new_dt.ImportRow(newRow);
                        row.Delete();
                    }
                }
                
            }

            return new_dt;
        }

        private void btnAutoAdjust_Click(object sender, EventArgs e)
        {
            //autoAdjustDate();
            blink = false;

            dgvMac.Focus();
            int row = currentSelectedRow;

            DataTable dt = newAutoAdjustDate();
            //DataTable dtSource = (DataTable)dgvMac.DataSource;

            dgvMac.DataSource = dt;

            if (AddAction)
            {
                for (int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);

                    if (planID == uPlanning.plan_id)
                    {
                        ableChangeDTP = false;
                        dtpStartDate.Value = Convert.ToDateTime(dgvMac.Rows[i].Cells[headerStartDate].Value);
                        dtpEstimateEndDate.Value = Convert.ToDateTime(dgvMac.Rows[i].Cells[headerEndDate].Value);
                        ableChangeDTP = true;
                    }
                }
            }

            if(row > 0)
            {
                dgvMac.Rows[row].Selected = true;
                currentSelectedRow = row;
            }
            else
            {
                dgvMac.ClearSelection();
            }
        }

        private void dgvMac_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip my_menu = new ContextMenuStrip();
            errorProvider1.Clear();
            blink = false;
            //int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {

                dgvMac.CurrentCell = dgvMac.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvMac.Rows[e.RowIndex].Selected = true;
                dgvMac.Focus();
                int rowIndex = dgvMac.CurrentCell.RowIndex;

                try
                {
                    string result = dgvMac.Rows[rowIndex].Cells[headerStatus].Value.ToString();

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
                    else if (result.Equals(text.planning_status_running) || result.Equals(headerToRun))
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

                    my_menu.Items.Add(changeDate).Name = changeDate;
                    my_menu.Items.Add(familyWith).Name = familyWith;
                    my_menu.Items.Add(resetFamilyWith).Name = resetFamilyWith;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            DataGridView dgv = dgvMac;
            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
            

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.planning_status_pending))
            {
                dataChange = true;
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_pending;
            }
            else if (itemClicked.Equals(text.planning_status_running))
            {
                dataChange = true;
                if(RunAction)
                {
                    dgv.Rows[rowIndex].Cells[headerStatus].Value = headerToRun;
                }
                else
                {
                    dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_running;
                }
            
            }
            else if (itemClicked.Equals(text.planning_status_completed))
            {
                dataChange = true;
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_completed;
            }
            else if (itemClicked.Equals(text.planning_status_cancelled))
            {
                dataChange = true;
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_cancelled;
            }
            else if (itemClicked.Equals(changeDate))
            {
                DateTime start = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerStartDate].Value);
                DateTime end = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerEndDate].Value);
                int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerPlanID].Value);

                bool includeSunday = false;
                if (cbIncludeSunday.Checked)
                {
                    includeSunday = true;
                }

                //int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);
                int proDayRequired = tool.getNumberOfProDay(planID);

                frmChangeDate frm = new frmChangeDate(start, end, proDayRequired, includeSunday);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmChangeDate.dateSaved)
                {
                    dataChange = true;
                    dgv.Rows[rowIndex].Cells[headerStartDate].Value = frmChangeDate.start.Date;
                    dgv.Rows[rowIndex].Cells[headerEndDate].Value = frmChangeDate.end.Date;
                    //ableLoadDate = false;
                    //dtpStartDate.Value = frmChangeDate.start.Date;
                    //dtpEstimateEndDate.Value = frmChangeDate.end.Date;
                    //ableLoadDate = true;
                }
            }

            else if (itemClicked.Equals(familyWith))
            {
                //record current plan id
                int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerPlanID].Value);
                DataTable dt = (DataTable)dgvMac.DataSource;

                //open new form, show schedule exclude current plan id 
                frmJustMachineSchedule frm = new frmJustMachineSchedule(dt, planID);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                
                //if save (return plan id), do following:
                if(frmJustMachineSchedule.planID != -1)
                {
                    dataChange = true;
                    int familyWithPlanID = -1;
                    int selectedRowIndex = frmJustMachineSchedule.currentSelectedRow;

                    int selectedFamilyWithData = Convert.ToInt32(dgvMac.Rows[selectedRowIndex].Cells[headerFamilyWith].Value);

                    if(selectedFamilyWithData <= 0)
                    {
                        familyWithPlanID = frmJustMachineSchedule.planID;
                    }
                    else
                    {
                        familyWithPlanID = selectedFamilyWithData;
                    }

                    //set both "family_with" to plan id from the selected row
                    dgvMac.Rows[selectedRowIndex].Cells[headerFamilyWith].Value = familyWithPlanID;
                    dgvMac.Rows[rowIndex].Cells[headerFamilyWith].Value = familyWithPlanID;

                    // set current plan start date same with selected row(if selected row not running)
                    string selectedStatus = dgvMac.Rows[selectedRowIndex].Cells[headerStatus].Value.ToString();

                    if(!selectedStatus.Equals(text.planning_status_running))
                    {
                        DateTime selectedStart = Convert.ToDateTime(dgvMac.Rows[selectedRowIndex].Cells[headerStartDate].Value);

                        DateTime start = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerStartDate].Value);
                        DateTime end = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerEndDate].Value);

                        bool includeSunday = false;
                        if (cbIncludeSunday.Checked)
                        {
                            includeSunday = true;
                        }

                        //int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);
                        int proDayRequired = tool.getNumberOfProDay(planID);

                        dgvMac.Rows[rowIndex].Cells[headerStartDate].Value = selectedStart.ToShortDateString();

                        end = tool.EstimateEndDate(selectedStart, proDayRequired, includeSunday);
                        dgvMac.Rows[rowIndex].Cells[headerEndDate].Value = end;

                        string selectedRemark = dgv.Rows[selectedRowIndex].Cells[headerRemark].Value.ToString();
                        string Remark = dgv.Rows[rowIndex].Cells[headerRemark].Value.ToString();

                        selectedRemark = selectedRemark.Replace(text.planning_Family_mould_Remark, "");
                        Remark = Remark.Replace(text.planning_Family_mould_Remark, "");

                        dgv.Rows[selectedRowIndex].Cells[headerRemark].Value = text.planning_Family_mould_Remark + "" + selectedRemark;
                        dgv.Rows[rowIndex].Cells[headerRemark].Value = text.planning_Family_mould_Remark + "" + Remark;

                        ableLoadDate = false;
                        dtpStartDate.Value = selectedStart;
                        dtpEstimateEndDate.Value = end;
                        ableLoadDate = true;

                    }

                   

                }


            }

            else if (itemClicked.Equals(resetFamilyWith))
            {
                if (true)
                {
                    dataChange = true;
                    int familyWith = Convert.ToInt32(dgvMac.Rows[rowIndex].Cells[headerFamilyWith].Value);
                    int planID = Convert.ToInt32(dgvMac.Rows[rowIndex].Cells[headerPlanID].Value);

                    if(familyWith != -1)
                    {
                        dgvMac.Rows[rowIndex].Cells[headerFamilyWith].Value = -1;

                        string Remark = dgv.Rows[rowIndex].Cells[headerRemark].Value.ToString();

                        Remark = Remark.Replace(text.planning_Family_mould_Remark, "");

                        dgv.Rows[rowIndex].Cells[headerRemark].Value = Remark;

                        if (familyWith == planID)
                        {
                            if (!backgroundWorker2.IsBusy)
                            {
                                backgroundWorker2.RunWorkerAsync(planID);
                            }
                        }
                    }
                }

            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
                start = dtpStartDate.Value.Date;
                end = dtpEstimateEndDate.Value.Date;
            }

            while(formClose == -1)
            {

            }

            if(formClose == 1)
            {
                Close();
            }
            else
            {
                formClose = -1;
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            blink = false;
            errorProvider1.Clear();

            bool abletoChangeData = true;

            DataGridView dgv = dgvMac;
            dgvMac.Focus();

            int row = dgv.CurrentRow.Index;

         
            if (currentSelectedRow != -1)
            {
                row = currentSelectedRow;
            }

            if (row > 0)
            {
                int upPlanID = (int)dgv.Rows[row - 1].Cells[headerPlanID].Value;
                string upStatus = dgv.Rows[row - 1].Cells[headerStatus].Value.ToString();
                string downStatus = dgv.Rows[row].Cells[headerStatus].Value.ToString();

                if ((upStatus.Equals(text.planning_status_running) || upStatus.Equals(headerToRun)) && upStatus != downStatus)
                {
                    //DialogResult dialogResult;
                    if (upStatus.Equals(text.planning_status_running))
                    {
                        blink = true;
                        Blink("PLAN " + upPlanID + " is RUNNING now!");
                        abletoChangeData = false;
                       // dialogResult = MessageBox.Show("PLAN " + upPlanID + " is RUNNING now, you want to switch it to PENDING?", "Message",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    else
                    {
                        blink = true;
                        Blink("PLAN " + upPlanID + " is planning to run!");
                        abletoChangeData = false;
                        //dialogResult = MessageBox.Show("PLAN " + upPlanID + " is planning to run, you want to switch it to PENDING?", "Message",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }

                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    abletoChangeData = true;
                    //    dgv.Rows[row - 1].Cells[headerStatus].Value = text.planning_status_pending;
                    //}
                    //else
                    //{
                    //    abletoChangeData = false;
                    //}
                }
                if (abletoChangeData)
                {
                    PlanningBLL u = new PlanningBLL();

                    u.plan_id = (int)dgv.Rows[row].Cells[headerPlanID].Value;
                    u.plan_status = (string)dgv.Rows[row].Cells[headerStatus].Value;
                    u.production_start_date = (DateTime)dgv.Rows[row].Cells[headerStartDate].Value;
                    u.production_end_date = (DateTime)dgv.Rows[row].Cells[headerEndDate].Value;
                    u.part_code = (string)dgv.Rows[row].Cells[headerPartCode].Value;
                    u.part_name = (string)dgv.Rows[row].Cells[headerPartName].Value;
                    u.production_purpose = (string)dgv.Rows[row].Cells[headerProductionPurpose].Value;
                    u.production_target_qty = dgv.Rows[row].Cells[headerTargetQty].Value.ToString();
                    u.family_with = Convert.ToInt32(dgv.Rows[row].Cells[headerFamilyWith].Value);
                    u.plan_remark = dgv.Rows[row].Cells[headerRemark].Value.ToString();

                    DataTable dt = (DataTable)dgv.DataSource;

                    dt.Rows.RemoveAt(row);

                    int position = row - 1;


                    DataRow dr;
                    dr = dt.NewRow();

                    dr[headerPlanID] = u.plan_id;
                    dr[headerStartDate] = u.production_start_date;
                    dr[headerEndDate] = u.production_end_date;
                    dr[headerPartName] = u.part_name;
                    dr[headerPartCode] = u.part_code;
                    dr[headerProductionPurpose] = u.production_purpose;
                    dr[headerTargetQty] = u.production_target_qty;
                    dr[headerStatus] = u.plan_status;
                    dr[headerFamilyWith] = u.family_with;
                    dr[headerRemark] = u.plan_remark;

                    dt.Rows.InsertAt(dr, position);

                    //autoAdjustDate();
                    dgv.ClearSelection();
                    dgvMac.Rows[position].Selected = true;
                    dgvMac.Focus();

                    currentSelectedRow = position;

                    dataChange = true;
                }

            }

            else if (row == 0)
            {
                blink = true;
                Blink("Please select a row.");
            }
        }

        private void dgvMac_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvMac.CurrentRow != null)
            {
                currentSelectedRow = dgvMac.CurrentRow.Index;
                selectedRow = dgvMac.CurrentRow.Index;
            }

            //if (dgvMac.CurrentCell != null)
            //selectedRow = dgvMac.CurrentCell.RowIndex;
        }

        private void changeRunningPlanData()
        {
            int referFamilyWithData = tool.getFamilyWithData(uPlanning.plan_id);

            bool includedSunday = false;

            if(cbIncludeSunday.Checked)
            {
                includedSunday = true;
            }
            if(ableLoadDate)
            {
                DataTable dt = (DataTable)dgvMac.DataSource;

                for(int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);
                    int familyWith = Convert.ToInt32(dgvMac.Rows[i].Cells[headerFamilyWith].Value);

                    if (planID == uPlanning.plan_id)
                    {
                        dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                        dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                
                    }
                    else if(familyWith == referFamilyWithData)
                    {
                        DateTime start = Convert.ToDateTime(dgvMac.Rows[i].Cells[headerStartDate].Value).Date;
                        DateTime end = Convert.ToDateTime(dgvMac.Rows[i].Cells[headerEndDate].Value).Date;

                        //int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includedSunday);

                        int proDayRequired = tool.getNumberOfProDay(planID);

                        dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                        dgvMac.Rows[i].Cells[headerEndDate].Value = tool.EstimateEndDate(dtpStartDate.Value.Date,proDayRequired,includedSunday);
                    }
                    
                }
            }

        }

        private void changeAddingPlanData()
        {
            if (ableLoadDate)
            {
                DataTable dt = (DataTable)dgvMac.DataSource;

                for (int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);

                    if (planID == uPlanning.plan_id)
                    {
                        dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                        dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                    }
                }
            }

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            blink = false;
            if (ableChangeDTP)
            {
                ableLoadDate = false;
                DateTime start = dtpStartDate.Value.Date;

                bool includeSunday = false;

                if (cbIncludeSunday.Checked)
                {
                    includeSunday = true;
                }
                int proDayRequired = tool.getNumberOfDayBetweenTwoDate(uPlanning.production_start_date, uPlanning.production_end_date, includeSunday);
                //int proDayRequired = tool.getNumberOfProDay(planID);

                DateTime end = tool.EstimateEndDate(start, proDayRequired, includeSunday);

                dtpEstimateEndDate.Value = end;

                if (RunAction)
                {
                    ableLoadDate = true;
                    changeRunningPlanData();
                }

                else if (AddAction)
                {
                    ableLoadDate = true;
                    changeAddingPlanData();
                }

                if (dtpStartDate.Value.Date != uPlanning.production_start_date.Date)
                {
                    dataChange = true;
                }
            }
        }

        private void dtpEstimateEndDate_ValueChanged(object sender, EventArgs e)
        {
            blink = false;
            if ((RunAction || AddAction) && ableChangeDTP)
            {
                DataTable dt = (DataTable)dgvMac.DataSource;

                for (int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    int PlanID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);

                    if (PlanID == uPlanning.plan_id)
                    {
                        dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                    }
                }
            }

            if (dtpEstimateEndDate.Value.Date != uPlanning.production_end_date.Date)
            {
                dataChange = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Validation())
            {
                if (MessageBox.Show("Are you sure you want to apply planning with these dates?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //update planning data
                    bool success = true;

                    DataTable dt = (DataTable)dgvMac.DataSource;

                    PlanningBLL uPlanning = new PlanningBLL();

                    foreach (DataRow row in dt.Rows)
                    {
                        int planID = Convert.ToInt32(row[headerPlanID]);

                        if (planID != -1)
                        {
                            uPlanning.plan_id = planID;



                            if (row[headerStatus].ToString().Equals(headerToRun))
                            {
                                uPlanning.plan_status = text.planning_status_running;
                                uPlanning.recording = true;
                            }
                            else
                            {
                                uPlanning.plan_status = row[headerStatus].ToString();
                            }

                            uPlanning.production_start_date = Convert.ToDateTime(row[headerStartDate]).Date;
                            uPlanning.production_end_date = Convert.ToDateTime(row[headerEndDate]).Date;
                            uPlanning.family_with = Convert.ToInt32(row[headerFamilyWith]);
                            uPlanning.plan_remark = row[headerRemark].ToString();
                            uPlanning.plan_updated_date = DateTime.Now;
                            uPlanning.plan_updated_by = MainDashboard.USER_ID;
                            

                            DataTable dt_Mac = dalPlanning.macIDSearch(MacID);
                            DataRow oldData = tool.getDataRowFromDataTableByPlanID(dt_Mac, row[headerPlanID].ToString());

                            DateTime oldStart = Convert.ToDateTime(oldData[dalPlanning.productionStartDate]);
                            DateTime oldEnd = Convert.ToDateTime(oldData[dalPlanning.productionEndDate]);
                            string oldStatus = oldData[dalPlanning.planStatus].ToString();
                            int oldFamilyWith = Convert.ToInt32(oldData[dalPlanning.familyWith]);
                            success = dalPlanningAction.planningStatusAndScheduleChange(uPlanning, oldStatus, oldStart, oldEnd, oldFamilyWith);

                            //success = dalPlanning.statusAndDateUpdate(uPlanning);
                        }
                        else
                        {
                            ToAddPlanFamilyWith = Convert.ToInt32(row[headerFamilyWith]);
                        }
                    }

                    if (success)
                    {
                        applied = true;


                        MessageBox.Show("Change successfully!");
                        formClose = 1;
                    }
                    else
                    {
                        Blink("Unable to update data!");
                        formClose = 0;
                    }

                }
                else
                {
                    formClose = 0;
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            int planID = (int)e.Argument;

            int newFamilyWith = -1;

            DataTable dt = (DataTable)dgvMac.DataSource;

            foreach(DataRow row in dt.Rows)
            {
                int oldFamilyWith = Convert.ToInt32(row[headerFamilyWith]);

                if(oldFamilyWith == planID)
                {
                    if(newFamilyWith == -1)
                    {
                        newFamilyWith = Convert.ToInt32(row[headerPlanID]);
                    }

                    row[headerFamilyWith] = newFamilyWith;
                }
            }

        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
     
        }
    }
}
