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
    public partial class frmMacScheduleAdjust : Form
    {

        #region Variable/ Object

        readonly string headerID = "PLAN ID";
        readonly string headerStartDate = "START";
        readonly string headerEndDate = "ESTIMATE END";
        readonly string headerPartName = "NAME";
        readonly string headerPartCode = "CODE";
        readonly string headerProductionPurpose = "PRODUCTION FOR";
        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerStatus = "STATUS";
        readonly string changeDate = "Change Date";
        readonly string toAdd = "TO ADD";

        planningDAL dalPlanning = new planningDAL();
        itemDAL dalItem = new itemDAL();
        Text text = new Text();
        Tool tool = new Tool();

        bool blink = false;
        bool insertChange = false;
        bool loaded = false;
        bool ableLoadSchedule = true;
        static public bool applied = false;

        static public DateTime start;
        static public DateTime end;

        private int insertedRow = -1;
        private int PlanIDFromMain = -1;

        #endregion

        public frmMacScheduleAdjust()
        {
            InitializeComponent();
            tool.loadMacIDToComboBox(cmbMacID);
        }

        public frmMacScheduleAdjust(string macID,PlanningBLL u, DateTime start, DateTime end)
        {
            InitializeComponent();
            tool.loadMacIDToComboBox(cmbMacID);

            float hours = Convert.ToSingle(u.production_hour);

            int day = Convert.ToInt32(u.production_day);
            
            cmbMacID.Text = macID;
            txtDayRequired.Text = u.production_day;
            txtHourRequired.Text = u.production_hour;

            if(string.IsNullOrEmpty(u.production_day))
            {
                txtDayRequired.Text = "10";
            }

            if (string.IsNullOrEmpty(u.production_hour))
            {
                txtHourRequired.Text = "0";
            }

            if (!string.IsNullOrEmpty(u.part_code))
            {
                txtPartCode.Text = u.part_code;
            }

            if (!string.IsNullOrEmpty(u.part_name))
            {
                txtPartName.Text = u.part_name;
            }

            dtpStartDate.Value = start;
            dtpEstimateEndDate.Value = end;
        }

        public frmMacScheduleAdjust(string macID, PlanningBLL u, DateTime start, DateTime end, int planID)
        {
            InitializeComponent();
            tool.loadMacIDToComboBox(cmbMacID);

            PlanIDFromMain = planID;

            float hours = Convert.ToSingle(u.production_hour);

            int day = Convert.ToInt32(u.production_day);

            cmbMacID.Text = macID;
            txtDayRequired.Text = u.production_day;
            txtHourRequired.Text = u.production_hour;

            if (string.IsNullOrEmpty(u.production_day))
            {
                txtDayRequired.Text = "10";
            }

            if (string.IsNullOrEmpty(u.production_hour))
            {
                txtHourRequired.Text = "0";
            }

            if (!string.IsNullOrEmpty(u.part_code))
            {
                txtPartCode.Text = u.part_code;
            }

            if (!string.IsNullOrEmpty(u.part_name))
            {
                txtPartName.Text = u.part_name;
            }

            dtpStartDate.Value = start;
            dtpEstimateEndDate.Value = end;
        }

        #region UI Setting

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
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
            dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStartDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerEndDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerProductionPurpose].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTargetQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStatus].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////

            dgv.Columns[headerID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStartDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerEndDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTargetQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          
        }

        #endregion

        #region Load Data

        private void loadScheduleData()
        {
            if(ableLoadSchedule)
            {
                insertChange = false;
                string macID = cmbMacID.Text;

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

                    #region from Main Filter

                    if(PlanIDFromMain != -1)
                    {
                        if(planID == PlanIDFromMain)
                        {
                            match = false;
                        }
                    }

                    #endregion

                    if (match)
                    {
                        row_Schedule = dt_Schedule.NewRow();

                        row_Schedule[headerID] = planID;
                        row_Schedule[headerStartDate] = row[dalPlanning.productionStartDate];
                        row_Schedule[headerEndDate] = row[dalPlanning.productionEndDate];

                        row_Schedule[headerPartName] = row[dalItem.ItemName];
                        row_Schedule[headerPartCode] = row[dalItem.ItemCode];
                        row_Schedule[headerProductionPurpose] = row[dalPlanning.productionPurpose];

                        row_Schedule[headerTargetQty] = row[dalPlanning.targetQty];

                        row_Schedule[headerStatus] = row[dalPlanning.planStatus];

                        dt_Schedule.Rows.Add(row_Schedule);
                    }

                }

                dgvMac.DataSource = null;

                if (dt_Schedule.Rows.Count > 0)
                {
                    dgvMac.DataSource = dt_Schedule;
                    dgvScheduleUIEdit(dgvMac);
                    dgvMac.ClearSelection();
                    insertedRow = -1;
                }
            }
        }

        #endregion

        private void frmMacScheduleAdjust_Load(object sender, EventArgs e)
        {
            estimateEndDate();
            loadScheduleData();
            loaded = true;
        }

        private void cmbMacID_SelectedIndexChanged(object sender, EventArgs e)
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
                else if (value.Equals(text.planning_status_running) || value.Equals(toAdd))
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
                    else if (i != row)
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


            if (value.Equals(text.planning_status_cancelled) || value.Equals(text.planning_status_completed))
            {
                dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
                dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
            }



            dgv.ResumeLayout();
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

        private void btnInsertAbove_Click(object sender, EventArgs e)
        {
            blink = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
           
            DataGridView dgv = dgvMac;

            bool includeSunday = false;

            if (cbIncludeSunday.Checked)
            {
                includeSunday = true;
            }

            if (dgv.SelectedRows.Count > 0 && insertedRow != dgv.CurrentCell.RowIndex)
            {
                //change

                DataTable dt = (DataTable)dgvMac.DataSource;

                if (insertedRow != -1)
                {
                    dt.Rows.RemoveAt(insertedRow);
                }

                int position = dgv.CurrentCell.RowIndex;

                #region Check if Running
                bool approveInsert = true;
                string status = dgv.Rows[position].Cells[headerStatus].Value.ToString();
                string planID = dgv.Rows[position].Cells[headerID].Value.ToString();
                if (status.Equals(text.planning_status_running))
                {
                    DialogResult dialogResult = MessageBox.Show("PLAN "+planID+" is RUNNING now, you want to switch it to PENDING?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        approveInsert = true;
                        dgv.Rows[position].Cells[headerStatus].Value = text.planning_status_pending;
                    }
                    else
                    {
                        approveInsert = false;

                    }
                }
                #endregion

                if (approveInsert)
                {
                    DateTime start = dtpStartDate.Value;
                    DateTime backwardStartDate = dtpEstimateEndDate.Value;

                    #region auto adjust date
                    //if (position != 0)
                    //{
                    //    string macStatus;

                    //    for (int i = dgv.CurrentCell.RowIndex-1; i >= 0; i--)
                    //    {
                    //        macStatus = dgvMac.Rows[i].Cells[headerStatus].Value.ToString();

                    //        if (macStatus != text.planning_status_cancelled && macStatus != text.planning_status_completed)
                    //        {
                    //            start = Convert.ToDateTime(dgvMac.Rows[i].Cells[headerEndDate].Value).AddDays(1);
                    //            break;
                    //        }
                    //    }

                    //    //start = Convert.ToDateTime(dgvMac.Rows[dgv.CurrentCell.RowIndex - 1].Cells[headerEndDate].Value).AddDays(1);

                    //}

                    //if (!includeSunday && tool.checkIfSunday(start.Date))
                    //{
                    //    start = start.AddDays(1);
                    //}

                    //ableLoadSchedule = false;
                    //dtpStartDate.Value = start.Date;
                    //ableLoadSchedule = true;

                    //DateTime backwardStartDate = dtpEstimateEndDate.Value;

                    //for (int i = position; i < dgv.Rows.Count; i++)
                    //{
                    //    status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

                    //    if (status != text.planning_status_completed && status != text.planning_status_cancelled)
                    //    {
                    //        DateTime beforeStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
                    //        DateTime beforeEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value);
                    //        int proRequiredDay = tool.getNumberOfDayBetweenTwoDate(beforeStart, beforeEnd, includeSunday);

                    //        backwardStartDate = backwardStartDate.AddDays(1);
                    //        if (tool.checkIfSunday(backwardStartDate) && !includeSunday)
                    //        {
                    //            backwardStartDate = backwardStartDate.AddDays(1);
                    //        }

                    //        dgv.Rows[i].Cells[headerStartDate].Value = backwardStartDate;
                    //        backwardStartDate = tool.EstimateEndDate(backwardStartDate, proRequiredDay, includeSunday);
                    //        dgv.Rows[i].Cells[headerEndDate].Value = backwardStartDate;
                    //    }

                    //}

                    #endregion

                    DataRow dr;
                    dr = dt.NewRow();

                    dr[headerID] = -1;
                    dr[headerStartDate] = dtpStartDate.Value.Date;
                    dr[headerEndDate] = dtpEstimateEndDate.Value.Date;
                    dr[headerPartName] = txtPartName.Text;
                    dr[headerPartCode] = txtPartCode.Text;
                    dr[headerStatus] = toAdd;

                    dt.Rows.InsertAt(dr, position);

                    insertedRow = position;

                    dgvMac.Rows[position].Selected = true;
                    dgvMac.Focus();
                    insertChange = true;
                }
            }
            else
            {
                blink = true;
                if(insertedRow == dgv.CurrentCell.RowIndex)
                {
                    Blink("Please select other row!");
                }
                else
                {
                    Blink("Please select a row!");
                }
                
            }
            
        }

        private void btnInsertBelow_Click(object sender, EventArgs e)
        {
            blink = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            DataGridView dgv = dgvMac;

            if (dgv.SelectedRows.Count > 0 && insertedRow != dgv.CurrentCell.RowIndex)
            {
                DataTable dt = (DataTable)dgvMac.DataSource;

                if (insertedRow != -1)
                {
                    dt.Rows.RemoveAt(insertedRow);
                }

                //insert row below
                int position = dgv.CurrentCell.RowIndex;
                position += 1;
                DateTime start = dtpStartDate.Value;
                //string macStatus;
                DateTime backwardStartDate = dtpEstimateEndDate.Value;

                #region auto adjust date
                //for (int i = dgv.CurrentCell.RowIndex; i >= 0; i--)
                //{
                //    macStatus = dgvMac.Rows[i].Cells[headerStatus].Value.ToString();

                //    if(macStatus != text.planning_status_cancelled && macStatus != text.planning_status_completed)
                //    {
                //        start = Convert.ToDateTime(dgvMac.Rows[i].Cells[headerEndDate].Value).AddDays(1);
                //        break;
                //    }
                //}


                //if (!includeSunday && tool.checkIfSunday(start.Date))
                //{
                //    start = start.AddDays(1);
                //}

                //ableLoadSchedule = false;
                //dtpStartDate.Value = start.Date;
                //ableLoadSchedule = true;

                //DateTime backwardStartDate = dtpEstimateEndDate.Value;

                //for(int i = position; i < dgv.Rows.Count; i++)
                //{
                //    string status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

                //    if(status != text.planning_status_completed && status != text.planning_status_cancelled)
                //    {
                //        DateTime beforeStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
                //        DateTime beforeEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value);
                //        int proRequiredDay = tool.getNumberOfDayBetweenTwoDate(beforeStart, beforeEnd, includeSunday);

                //        backwardStartDate = backwardStartDate.AddDays(1);
                //        if (tool.checkIfSunday(backwardStartDate) && !includeSunday)
                //        {
                //            backwardStartDate = backwardStartDate.AddDays(1);
                //        }

                //        dgv.Rows[i].Cells[headerStartDate].Value = backwardStartDate;
                //        backwardStartDate = tool.EstimateEndDate(backwardStartDate, proRequiredDay, includeSunday);
                //        dgv.Rows[i].Cells[headerEndDate].Value = backwardStartDate;
                //    }

                //}
                #endregion



                DataRow dr;
                dr = dt.NewRow();

                dr[headerID] = -1;
                dr[headerStartDate] = dtpStartDate.Value;
                dr[headerEndDate] = dtpEstimateEndDate.Value;

                
                dr[headerPartName] = txtPartName.Text;
                dr[headerPartCode] = txtPartCode.Text;
                //dr[headerProductionPurpose] = row[dalPlanning.productionPurpose];

                //dr[headerTargetQty] = row[dalPlanning.targetQty];

                dr[headerStatus] = toAdd;

                //dr.Rows.Add(row_Schedule);
               
                dt.Rows.InsertAt(dr, position);

                

                insertedRow = position;

                dgvMac.Rows[position].Selected = true;
                dgvMac.Focus();

                insertChange = true;
            }

            else
            {
                blink = true;
                if (insertedRow == dgv.CurrentCell.RowIndex)
                {
                    Blink("Please select other row!");
                }
                else
                {
                    Blink("Please select a row!");
                }
            }
        }

        private bool checkDateAvailable()
        {
            bool available = false;
            if (!string.IsNullOrEmpty(cmbMacID.Text) && loaded)
            {
                DateTime start = dtpStartDate.Value;
                DateTime end = dtpEstimateEndDate.Value;

                errorProvider1.Clear();
                errorProvider2.Clear();

                available = tool.ifProductionDateAvailable(cmbMacID.Text, start, end);

                if (!available)
                {
                    //MessageBox.Show("date fail");
                    errorProvider1.SetError(lblStartDate, "Production date clash with other plan");
                    errorProvider2.SetError(lblEndDate, "Production date clash with other plan");
                    //MessageBox.Show("One of the production date clash with other plan!");
                    Blink("One of the production date clash with other plan!");
                    
                }
            }

            return available;
        }

        private bool Validation()
        {
            bool result = true;

            if (!checkAllDate())
            {
                result = false;
                errorProvider3.SetError(lblMachineSchedule, "Production date clash with other plan");
                Blink("One of the production date clash with other plan!");
            }

            return result;

        }

        void bwApply_DoWork(object sender, DoWorkEventArgs e)
        {
            
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
                if (i == 0)
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            dgvMac.ClearSelection();
            blink = true;

            if (!insertChange)
            {
                if (checkDateAvailable())
                {
                    if (MessageBox.Show("Are you sure you want to apply planning with these dates?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        applied = true;
                        start = dtpStartDate.Value;
                        end = dtpEstimateEndDate.Value;

                        MessageBox.Show("Change successfully!");
                        Close();
                    }
                }
            }
            else
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

                        foreach(DataRow row in dt.Rows)
                        {
                            int planID = Convert.ToInt32(row[headerID]);

                            if(planID != -1)
                            {
                                uPlanning.plan_id = planID;
                                uPlanning.plan_status = row[headerStatus].ToString();
                                uPlanning.production_start_date = Convert.ToDateTime(row[headerStartDate]).Date;
                                uPlanning.production_end_date = Convert.ToDateTime(row[headerEndDate]).Date;

                                success = dalPlanning.statusAndDateUpdate(uPlanning);
                            }
                        }

                        if(success)
                        {
                            applied = true;
                            start = dtpStartDate.Value;
                            end = dtpEstimateEndDate.Value;

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
            

                
        }

        private void dgvMac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dgvMac.CurrentCell.RowIndex;

            blink = false;

        }

        private void estimateEndDate()
        {
            float hours = 0;

            int day = 1;
            int proDayRequired = 1;

            if (float.TryParse(txtHourRequired.Text, out hours) && int.TryParse(txtDayRequired.Text, out day))
            {

                if (hours > 0)
                {
                    proDayRequired = day + 1;
                }
                else
                {
                    proDayRequired = day;
                }
            }

            bool includedSunday = false;
            if (cbIncludeSunday.Checked)
            {
                includedSunday = true;
            }

            dtpEstimateEndDate.Value = tool.EstimateEndDate(dtpStartDate.Value, proDayRequired, includedSunday);
        }

        private DateTime estimateEndDate(DateTime start)
        {
            float hours = 0;

            int day = 1;
            int proDayRequired = 1;

            if (float.TryParse(txtHourRequired.Text, out hours) && int.TryParse(txtDayRequired.Text, out day))
            {

                if (hours > 0)
                {
                    proDayRequired = day + 1;
                }
                else
                {
                    proDayRequired = day;
                }
            }

            bool includedSunday = false;
            if (cbIncludeSunday.Checked)
            {
                includedSunday = true;
            }

            return tool.EstimateEndDate(start, proDayRequired, includedSunday);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            blink = false;
            estimateEndDate();
            //loadScheduleData();

            DataTable dt = (DataTable)dgvMac.DataSource;

            for(int i = 0; i < dgvMac.RowCount; i++)
            {
                int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerID].Value);

                if (planID == -1)
                {
                    dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                    dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                }
            }
                
            

            errorProvider3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadScheduleData();
        }

        private void cbIncludeSunday_CheckedChanged(object sender, EventArgs e)
        {
            estimateEndDate();
            DataTable dt = (DataTable)dgvMac.DataSource;

            for (int i = 0; i < dgvMac.RowCount; i++)
            {
                int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerID].Value);

                if (planID == -1)
                {
                    dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                    dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                }
            }
            //loadScheduleData();
        }

        private void dtpEstimateEndDate_ValueChanged(object sender, EventArgs e)
        {
            blink = false;
            //loadScheduleData();
            DataTable dt = (DataTable)dgvMac.DataSource;

            for (int i = 0; i < dgvMac.RowCount; i++)
            {
                int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerID].Value);

                if (planID == -1)
                {
                    dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                    dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                }
            }
            errorProvider3.Clear();
            //bool includeSunday = false;

            //if(cbIncludeSunday.Checked)
            //{
            //    includeSunday = true;
            //}

            //lblTotalDay.Text = tool.getNumberOfDayBetweenTwoDate(dtpStartDate.Value,dtpEstimateEndDate.Value,includeSunday).ToString();
        }

        private void dgvMac_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip my_menu = new ContextMenuStrip();
            errorProvider3.Clear();
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

                    if(e.RowIndex != insertedRow)
                    {
                        my_menu.Items.Add(changeDate).Name = changeDate;
                    }

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
            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.planning_status_pending))
            {
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_pending;
            }
            else if (itemClicked.Equals(text.planning_status_running))
            {
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_running;
            }
            else if (itemClicked.Equals(text.planning_status_completed))
            {
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_completed;
            }
            else if (itemClicked.Equals(text.planning_status_cancelled))
            {
                dgv.Rows[rowIndex].Cells[headerStatus].Value = text.planning_status_cancelled;
            }
            else if (itemClicked.Equals(changeDate))
            {
                DateTime start = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerStartDate].Value);
                DateTime end = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerEndDate].Value);

                bool includeSunday = false;
                if(cbIncludeSunday.Checked)
                {
                    includeSunday = true;
                }

                int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);

                frmChangeDate frm = new frmChangeDate(start, end, proDayRequired, includeSunday);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if(frmChangeDate.dateSaved)
                {
                    dgv.Rows[rowIndex].Cells[headerStartDate].Value = frmChangeDate.start.Date;
                    dgv.Rows[rowIndex].Cells[headerEndDate].Value = frmChangeDate.end.Date;
                }
            }
            //lblUpdatedTime.Text = DateTime.Now.ToString();


        }

        private bool checkAllDate()
        {
            bool result = true;
            DataTable dt = (DataTable)dgvMac.DataSource;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                if(dgvMac.Rows[i].Cells[headerStartDate].Style.ForeColor == Color.Red)
                {
                    result = false;
                }
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAutoAdjust_Click(object sender, EventArgs e)
        {
            autoAdjustDate();
        }
    }
}
