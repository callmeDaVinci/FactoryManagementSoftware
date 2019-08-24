﻿using System;
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
        readonly string changeDate = "Change Date";
        readonly string headerToRun = "TO RUN";

        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        itemDAL dalItem = new itemDAL();
        Text text = new Text();
        Tool tool = new Tool();

        bool blink = false;
        bool dataChange = false;
        bool loaded = false;
        bool ableLoadDate = true;
        bool ableLoadSchedule = true;
        bool RunAction = false;
        static public bool applied = false;

        //static public DateTime start;
        //static public DateTime end;

        private int currentSelectedRow = -1;
        #endregion

        public frmMachineScheduleAdjustFromMain()
        {
            InitializeComponent();
        }

        public frmMachineScheduleAdjustFromMain(PlanningBLL u, bool RunActionMake)
        {
            InitializeComponent();
            uPlanning = u;
            RunAction = RunActionMake;
            if(RunActionMake)
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

            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////

            dgv.Columns[headerPlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStartDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerEndDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTargetQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        #endregion

        #region Load Data

        private void loadScheduleData()
        {
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

                foreach (DataRow row in dt.Rows)
                {
                    match = true;
                    int planID = Convert.ToInt32(row[dalPlanning.planID]);

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

                        if(planID == uPlanning.plan_id && RunAction)
                        {
                            row_Schedule[headerStatus] = headerToRun;
                        }
                        else
                        {
                            row_Schedule[headerStatus] = row[dalPlanning.planStatus];
                        }

                        dt_Schedule.Rows.Add(row_Schedule);
                    }

                }

                dgvMac.DataSource = null;

                if (dt_Schedule.Rows.Count > 0)
                {
                    dgvMac.DataSource = dt_Schedule;
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

        private void button2_Click(object sender, EventArgs e)
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
                else if (value.Equals(text.planning_status_running) || value.Equals(headerToRun))
                {
                    backColor = Color.FromArgb(0, 184, 148);
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
                        if(i < row)
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


            if (value.Equals(text.planning_status_cancelled) || value.Equals(text.planning_status_completed))
            {
                dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
                dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
            }

            #region old way invalid date checking (invalid = color red)
            ///////////////////////////////////////
            //else if (dgv.Columns[col].Name == headerEndDate && !value.Equals(text.planning_status_cancelled) && !value.Equals(text.planning_status_completed))
            //{
            //    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
            //    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;

            //    if (row + 1 < dgv.Rows.Count)
            //    {
            //        DateTime start = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
            //        DateTime end = Convert.ToDateTime(dgv.Rows[row].Cells[headerEndDate].Value);

            //        int nextIndex = row + 1;

            //        string nextStatus = dgv.Rows[nextIndex].Cells[headerStatus].Value.ToString();

            //        for (int i = row + 1; i < dgv.Rows.Count; i++)
            //        {
            //            nextStatus = dgv.Rows[i].Cells[headerStatus].Value.ToString();

            //            if(!nextStatus.Equals(text.planning_status_cancelled) && !nextStatus.Equals(text.planning_status_completed))
            //            {
            //                break;
            //            }
            //            else
            //            {
            //                nextIndex++;
            //            }
            //        }

            //        if (!nextStatus.Equals(text.planning_status_cancelled) && !nextStatus.Equals(text.planning_status_completed))
            //        {
            //            DateTime secondStart = Convert.ToDateTime(dgv.Rows[nextIndex].Cells[headerStartDate].Value);
            //            DateTime secondEnd = Convert.ToDateTime(dgv.Rows[nextIndex].Cells[headerEndDate].Value);

            //            if (!tool.ifProductionDateAvailable(start, end, secondStart, secondEnd))
            //            {
            //                dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Red;
            //                dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Red;

            //                dgv.Rows[row + 1].Cells[headerStartDate].Style.ForeColor = Color.Red;
            //                dgv.Rows[row + 1].Cells[headerEndDate].Style.ForeColor = Color.Red;
            //            }
            //        }


            //    }
            //    else if (row == dgv.Rows.Count - 1 && row != 0)
            //    {
            //        int lastIndex = row - 1;

            //        string lastStatus = dgv.Rows[row -1].Cells[headerStatus].Value.ToString();

            //        for (int i = row - 1; i >= 0; i--)
            //        {
            //            lastStatus = dgv.Rows[i].Cells[headerStatus].Value.ToString();
            //            lastIndex = i;

            //            if (!lastStatus.Equals(text.planning_status_cancelled) && !lastStatus.Equals(text.planning_status_completed))
            //            {
            //                break;
            //            }
            //        }

            //        DateTime start = Convert.ToDateTime(dgv.Rows[lastIndex].Cells[headerStartDate].Value);
            //        DateTime end = Convert.ToDateTime(dgv.Rows[lastIndex].Cells[headerEndDate].Value);

            //        DateTime secondStart = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
            //        DateTime secondEnd = Convert.ToDateTime(dgv.Rows[row].Cells[headerEndDate].Value);

            //        if (!tool.ifProductionDateAvailable(start, end, secondStart, secondEnd))
            //        {
            //            dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Red;
            //            dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Red;
            //        }
            //    }
            //}

            //if(value.Equals(text.planning_status_cancelled) || value.Equals(text.planning_status_completed))
            //{
            //    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
            //    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
            //}
            /////////////////////////////////////////
            /////
            #endregion
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
            lblWarning.Text = warning;
            lblWarning.Visible = true;
            while (blink)
            {
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
           

            if (row != -1 && row != dgv.RowCount -1)
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

                    DataTable dt = (DataTable)dgv.DataSource;

                    dt.Rows.RemoveAt(row);

                    int position = row + 1;

                    //int proDayRequired = tool.getNumberOfDayBetweenTwoDate(u.production_start_date.Date, u.production_end_date.Date, includeSunday);

                    //DateTime start = (DateTime)dgv.Rows[row].Cells[headerEndDate].Value;

                    //start = start.Date.AddDays(1);

                    //if (!includeSunday && tool.checkIfSunday(start))
                    //{
                    //    start = start.Date.AddDays(1);
                    //}

                    //DateTime end = tool.EstimateEndDate(start, proDayRequired, includeSunday);

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

                    dt.Rows.InsertAt(dr, position);



                    dgv.ClearSelection();
                    dgvMac.Rows[position].Selected = true;
                    dgvMac.Focus();
                    currentSelectedRow = position;
                    dataChange = true;
                }
                
            }
          
        }

        private void autoAdjustDate()
        {
            bool includeSunday = false;

            if (cbIncludeSunday.Checked)
            {
                includeSunday = true;
            }

            DataTable dt = (DataTable)dgvMac.DataSource;

            DataGridView dgv = dgvMac;

            DateTime start = Convert.ToDateTime(dgv.Rows[0].Cells[headerEndDate].Value).Date; 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(i == 0)
                {
                    start = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value).Date;
                }
                else
                {
                    string status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

                    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
                    {
                        DateTime oldStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value).Date;
                        DateTime oldEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value).Date;

                        int proDayRequired = tool.getNumberOfDayBetweenTwoDate(oldStart, oldEnd, includeSunday);

                        start = start.AddDays(1);

                        if (!includeSunday && tool.checkIfSunday(start))
                        {
                            start = start.AddDays(1);
                        }
                        dgv.Rows[i].Cells[headerStartDate].Value = start;

                        start = tool.EstimateEndDate(start, proDayRequired, includeSunday);
                        dgv.Rows[i].Cells[headerEndDate].Value = start;

                    }
                }
            }
        }

        private void btnAutoAdjust_Click(object sender, EventArgs e)
        {
            autoAdjustDate();
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
            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerPlanID].Value);

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
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_running;
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

                bool includeSunday = false;
                if (cbIncludeSunday.Checked)
                {
                    includeSunday = true;
                }

                int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);

                frmChangeDate frm = new frmChangeDate(start, end, proDayRequired, includeSunday);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmChangeDate.dateSaved)
                {
                    dataChange = true;
                    dgv.Rows[rowIndex].Cells[headerStartDate].Value = frmChangeDate.start.Date;
                    dgv.Rows[rowIndex].Cells[headerEndDate].Value = frmChangeDate.end.Date;
                    ableLoadDate = false;
                    dtpStartDate.Value = frmChangeDate.start.Date;
                    dtpEstimateEndDate.Value = frmChangeDate.end.Date;
                    ableLoadDate = true;
                }
            }


        }

        private void btnApply_Click(object sender, EventArgs e)
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
                            }
                            else
                            {
                                uPlanning.plan_status = row[headerStatus].ToString();
                            }

                            uPlanning.production_start_date = Convert.ToDateTime(row[headerStartDate]).Date;
                            uPlanning.production_end_date = Convert.ToDateTime(row[headerEndDate]).Date;

                            success = dalPlanning.statusAndDateUpdate(uPlanning);
                        }
                    }

                    if (success)
                    {
                        applied = true;


                        MessageBox.Show("Change successfully!");
                        Close();
                    }
                    else
                    {
                        Blink("Unable to update data!");
                    }

                }

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

            if(currentSelectedRow != -1)
            {
                row = currentSelectedRow;
            }

            if (row > 0)
            {
                int upPlanID = (int)dgv.Rows[row -1].Cells[headerPlanID].Value;
                string upStatus = dgv.Rows[row - 1].Cells[headerStatus].Value.ToString();

                if (upStatus.Equals(text.planning_status_running) || upStatus.Equals(headerToRun))
                {
                    DialogResult dialogResult;
                    if (upStatus.Equals(text.planning_status_running))
                    {
                        dialogResult = MessageBox.Show("PLAN " + upPlanID + " is RUNNING now, you want to switch it to PENDING?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    else
                    {
                        dialogResult = MessageBox.Show("PLAN " + upPlanID + " is planning to run, you want to switch it to PENDING?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    if (dialogResult == DialogResult.Yes)
                    {
                        abletoChangeData = true;
                        dgv.Rows[row - 1].Cells[headerStatus].Value = text.planning_status_pending;
                    }
                    else
                    {
                        abletoChangeData = false;
                    }
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

                    dt.Rows.InsertAt(dr, position);



                    //autoAdjustDate();
                    dgv.ClearSelection();
                    dgvMac.Rows[position].Selected = true;
                    dgvMac.Focus();

                    currentSelectedRow = position;

                    dataChange = true;
                }

            }
        }

        private void dgvMac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvMac_SelectionChanged(object sender, EventArgs e)
        {

            currentSelectedRow = dgvMac.CurrentRow.Index;
        }

        private void changeRunningPlanData()
        {
            if(ableLoadDate)
            {
                DataTable dt = (DataTable)dgvMac.DataSource;

                for(int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);
                    if(planID == uPlanning.plan_id)
                    {
                        dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                        dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                
                    }
                }
            }

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            ableLoadDate = false;
            DateTime start = dtpStartDate.Value.Date;

            bool includeSunday = false;

            if(cbIncludeSunday.Checked)
            {
                includeSunday = true;
            }
            int proDayRequired = tool.getNumberOfDayBetweenTwoDate(uPlanning.production_start_date,uPlanning.production_end_date,includeSunday);

            DateTime end = tool.EstimateEndDate(start, proDayRequired, includeSunday);

            dtpEstimateEndDate.Value = end;

            if(RunAction)
            {
                ableLoadDate = true;
                changeRunningPlanData();
            }

            if(dtpStartDate.Value.Date != uPlanning.production_start_date.Date)
            {
                dataChange = true;
            }
        }

        private void dtpEstimateEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (RunAction)
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
    }
}
