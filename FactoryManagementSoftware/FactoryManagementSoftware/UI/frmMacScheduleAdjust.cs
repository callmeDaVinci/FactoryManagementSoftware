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

        readonly string headerPlanID = "PLAN ID";
        readonly string headerStartDate = "START";
        readonly string headerEndDate = "ESTIMATE END";
        readonly string headerPartName = "NAME";
        readonly string headerPartCode = "CODE";
        readonly string headerProductionPurpose = "PRODUCTION FOR";
        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerStatus = "STATUS";
        readonly string changeDate = "Change Date";
        readonly string toAdd = "TO ADD";
        readonly string headerFamilyWith = "FAMILY WITH";
        readonly string headerRemark = "REMARK";
        readonly string familyWithThisPlan = "Family With This Plan";

        planningDAL dalPlanning = new planningDAL();
        planningActionDAL dalPlanningAction = new planningActionDAL();
        itemDAL dalItem = new itemDAL();
        Text text = new Text();
        Tool tool = new Tool();

        bool blink = false;
        bool insertChange = false;
        bool loaded = false;
        bool ableLoadSchedule = true;
        static public bool applied = false;
        bool stopStartValueFunction = false;
        static public DateTime start;
        static public DateTime end;
        static public int ToRunPlanFamilyWith = -1;
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
            if(ableLoadSchedule)
            {
                ToRunPlanFamilyWith = -1;
                insertChange = false;
                string macID = cmbMacID.Text;

                DataTable dt = dalPlanning.macIDSearch(macID);
                DataTable dt_Schedule = NewScheduleTable();
                DataRow row_Schedule;
                bool match = true;

                foreach (DataRow row in dt.Rows)
                {
                    match = true;
                    int planID = Convert.ToInt32(row[dalPlanning.jobNo]);

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

                        row_Schedule[headerPlanID] = planID;
                        row_Schedule[headerStartDate] = row[dalPlanning.productionStartDate];
                        row_Schedule[headerEndDate] = row[dalPlanning.productionEndDate];

                        row_Schedule[headerPartName] = row[dalItem.ItemName];
                        row_Schedule[headerPartCode] = row[dalItem.ItemCode];
                        row_Schedule[headerProductionPurpose] = row[dalPlanning.productionPurpose];

                        row_Schedule[headerTargetQty] = row[dalPlanning.targetQty];

                        row_Schedule[headerStatus] = row[dalPlanning.planStatus];
                        row_Schedule[headerFamilyWith] = row[dalPlanning.familyWith];
                        row_Schedule[headerRemark] = row[dalPlanning.planNote];
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

                        if (firstFamilyWith != secondFamilyWith || firstFamilyWith == -1)
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

            #region old way checking date available
            //else if (dgv.Columns[col].Name == headerEndDate && !value.Equals(text.planning_status_cancelled) && !value.Equals(text.planning_status_completed))
            //{
            //    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
            //    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;

            //    for (int i = 0; i < dgvMac.Rows.Count; i++)
            //    {
            //        string status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

            //        if (status.Equals(text.planning_status_cancelled) || status.Equals(text.planning_status_completed))
            //        {
            //            dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
            //            dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
            //        }
            //        else if (i != row)
            //        {
            //            if (i < row)
            //            {
            //                DateTime start = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
            //                DateTime end = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value);

            //                DateTime secondStart = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
            //                DateTime secondEnd = Convert.ToDateTime(dgv.Rows[row].Cells[headerEndDate].Value);

            //                if (!tool.ifProductionDateAvailable(start, end, secondStart, secondEnd))
            //                {
            //                    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Red;
            //                    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Red;
            //                    break;
            //                }

            //            }
            //            else
            //            {
            //                DateTime start = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
            //                DateTime end = Convert.ToDateTime(dgv.Rows[row].Cells[headerEndDate].Value);

            //                DateTime secondStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
            //                DateTime secondEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value);

            //                if (!tool.ifProductionDateAvailable(start, end, secondStart, secondEnd))
            //                {
            //                    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Red;
            //                    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Red;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}


            //if (value.Equals(text.planning_status_cancelled) || value.Equals(text.planning_status_completed))
            //{
            //    dgv.Rows[row].Cells[headerStartDate].Style.ForeColor = Color.Black;
            //    dgv.Rows[row].Cells[headerEndDate].Style.ForeColor = Color.Black;
            //}
            #endregion


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

                                    if (familyWith == previousRowFamilyWith)
                                    {
                                        DateTime start = Convert.ToDateTime(row2[headerStartDate]).Date;
                                        DateTime end = Convert.ToDateTime(row2[headerEndDate]).Date;

                                        int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);


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
                            if (previousRowEnd == default(DateTime))
                            {
                                previousRowEnd = Convert.ToDateTime(row[headerStartDate]).Date;
                            }

                            DateTime start = Convert.ToDateTime(row[headerStartDate]).Date;
                            DateTime end = Convert.ToDateTime(row[headerEndDate]).Date;

                            int proDayRequired = tool.getNumberOfDayBetweenTwoDate(start, end, includeSunday);

                            previousRowEnd = addOneDay(previousRowEnd, includeSunday);

                            row[headerStartDate] = previousRowEnd.Date;

                            previousRowEnd = tool.EstimateEndDate(previousRowEnd, proDayRequired, includeSunday);

                            row[headerEndDate] = previousRowEnd.Date;

                            DataRow newRow = new_dt.NewRow();
                            newRow = row;
                            new_dt.ImportRow(newRow);
                            row.Delete();
                        }
                    }


                }

            }

            return new_dt;
        }

        private void btnInsertAbove_Click(object sender, EventArgs e)
        {
            blink = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
           
            DataGridView dgv = dgvMac;

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
                string planID = dgv.Rows[position].Cells[headerPlanID].Value.ToString();
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

                    DataRow dr;
                    dr = dt.NewRow();

                    dr[headerPlanID] = -1;
                    dr[headerStartDate] = dtpStartDate.Value.Date;
                    dr[headerEndDate] = dtpEstimateEndDate.Value.Date;
                    dr[headerPartName] = txtPartName.Text;
                    dr[headerPartCode] = txtPartCode.Text;
                    dr[headerStatus] = toAdd;

                    if (ToRunPlanFamilyWith != -1)
                    {
                        dr[headerRemark] = text.planning_Family_mould_Remark;
                    }
                    dr[headerFamilyWith] = ToRunPlanFamilyWith;

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

        private void InsertBelow()
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

                DataRow dr;
                dr = dt.NewRow();

                dr[headerPlanID] = -1;
                dr[headerStartDate] = dtpStartDate.Value;
                dr[headerEndDate] = dtpEstimateEndDate.Value;


                dr[headerPartName] = txtPartName.Text;
                dr[headerPartCode] = txtPartCode.Text;
                dr[headerStatus] = toAdd;

                if (ToRunPlanFamilyWith != -1)
                {
                    dr[headerRemark] = text.planning_Family_mould_Remark;
                }
                dr[headerFamilyWith] = ToRunPlanFamilyWith;

                dt.Rows.InsertAt(dr, position);



                insertedRow = position;

                dgvMac.Rows[position].Selected = true;
                dgvMac.Focus();

                insertChange = true;

                autoAdjustDate();

                DataTable sortDt = newAutoAdjustDate();

                DataTable dtSource = (DataTable)dgvMac.DataSource;

                dgvMac.DataSource = sortDt;
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

        private void btnInsertBelow_Click(object sender, EventArgs e)
        {
            InsertBelow();
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

            DateTime previousRowStart = new DateTime();
            DateTime previousRowEnd = new DateTime();
            if (dt.Rows.Count > 0)
            {
                previousRowStart = Convert.ToDateTime(dgv.Rows[0].Cells[headerStartDate].Value).Date;
                previousRowEnd = Convert.ToDateTime(dgv.Rows[0].Cells[headerEndDate].Value).Date;

            }

            int previousRowFamilyWith = -1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int planID = Convert.ToInt32(dgv.Rows[i].Cells[headerPlanID].Value);
                int familyWith = Convert.ToInt32(dgv.Rows[i].Cells[headerFamilyWith].Value);
                if (i == 0)
                {
                    previousRowStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value).Date;
                    previousRowEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value).Date;
                    previousRowFamilyWith = Convert.ToInt32(dgv.Rows[i].Cells[headerFamilyWith].Value);

                    if (planID == -1)
                    {
                        if(familyWith == -1)
                        {
                            blink = false;
                            stopStartValueFunction = true;
                            dtpStartDate.Value = previousRowStart;
                            dtpEstimateEndDate.Value = previousRowEnd;
                            stopStartValueFunction = false;
                        }
                        
                    }
                }
                else
                {
                    string status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

                    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
                    {
                        DateTime oldStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value).Date;
                        DateTime oldEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value).Date;


                        int proDayRequired = tool.getNumberOfDayBetweenTwoDate(oldStart, oldEnd, includeSunday);

                        previousRowEnd = addOneDay(previousRowEnd, includeSunday);

                        dgv.Rows[i].Cells[headerStartDate].Value = previousRowEnd;

                        DateTime currentRowEnd = tool.EstimateEndDate(previousRowEnd, proDayRequired, includeSunday);
                        dgv.Rows[i].Cells[headerEndDate].Value = currentRowEnd;

                        if (planID == -1)
                        {
                            if (familyWith == -1)
                            {
                                blink = false;
                                stopStartValueFunction = true;
                                dtpStartDate.Value = previousRowEnd;
                                dtpEstimateEndDate.Value = currentRowEnd;
                                stopStartValueFunction = false;
                            }
                                
                        }
                    }
                }
            }
        }


        #region old auto adjust date
        //private void autoAdjustDate()
        //{
        //    bool includeSunday = false;

        //    if (cbIncludeSunday.Checked)
        //    {
        //        includeSunday = true;
        //    }

        //    DataTable dt = (DataTable)dgvMac.DataSource;

        //    DataGridView dgv = dgvMac;

        //    DateTime start = Convert.ToDateTime(dgv.Rows[0].Cells[headerEndDate].Value).Date;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (i == 0)
        //        {
        //            start = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value).Date;
        //        }
        //        else
        //        {
        //            string status = dgv.Rows[i].Cells[headerStatus].Value.ToString();

        //            if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
        //            {
        //                DateTime oldStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value).Date;
        //                DateTime oldEnd = Convert.ToDateTime(dgv.Rows[i].Cells[headerEndDate].Value).Date;

        //                int proDayRequired = tool.getNumberOfDayBetweenTwoDate(oldStart, oldEnd, includeSunday);

        //                start = start.AddDays(1);

        //                if (!includeSunday && tool.checkIfSunday(start))
        //                {
        //                    start = start.AddDays(1);
        //                }
        //                dgv.Rows[i].Cells[headerStartDate].Value = start;

        //                start = tool.EstimateEndDate(start, proDayRequired, includeSunday);
        //                dgv.Rows[i].Cells[headerEndDate].Value = start;

        //            }
        //        }
        //    }
        //}
        #endregion

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
                            int planID = Convert.ToInt32(row[headerPlanID]);

                            if(planID != -1)
                            {
                                uPlanning.plan_id = planID;
                                uPlanning.plan_status = row[headerStatus].ToString();
                                uPlanning.production_start_date = Convert.ToDateTime(row[headerStartDate]).Date;
                                uPlanning.production_end_date = Convert.ToDateTime(row[headerEndDate]).Date;
                                uPlanning.plan_updated_date = DateTime.Now;
                                uPlanning.plan_updated_by = MainDashboard.USER_ID;

                                DataTable dt_Mac = dalPlanning.macIDSearch(cmbMacID.Text.ToString());
                                DataRow oldData = tool.getDataRowFromDataTableByPlanID(dt_Mac,planID.ToString());

                                DateTime oldStart = Convert.ToDateTime(oldData[dalPlanning.productionStartDate]);
                                DateTime oldEnd = Convert.ToDateTime(oldData[dalPlanning.productionEndDate]);

                                int oldFamilyWith = Convert.ToInt32(oldData[dalPlanning.familyWith]);
                                string oldNote = oldData[dalPlanning.planNote].ToString();

                                uPlanning.family_with = oldFamilyWith;
                                uPlanning.plan_note = oldNote;

                                string oldStatus = oldData[dalPlanning.planStatus].ToString();
                                success = dalPlanningAction.planningStatusAndScheduleChange(uPlanning,oldStatus,oldStart,oldEnd, oldFamilyWith);
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
            if(!stopStartValueFunction)
            {
                blink = false;
                estimateEndDate();

                DataTable dt = (DataTable)dgvMac.DataSource;

                for (int i = 0; i < dgvMac.RowCount; i++)
                {
                    int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);

                    if (planID == -1)
                    {
                        dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                        dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                    }
                }



                errorProvider3.Clear();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadScheduleData();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cbIncludeSunday_CheckedChanged(object sender, EventArgs e)
        {
            estimateEndDate();
            DataTable dt = (DataTable)dgvMac.DataSource;

            for (int i = 0; i < dgvMac.RowCount; i++)
            {
                int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);

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
            if(!stopStartValueFunction)
            {
                blink = false;
                DataTable dt = (DataTable)dgvMac.DataSource;

                for (int i = 0; i < dgvMac.RowCount; i++)
                {
                    int planID = Convert.ToInt32(dgvMac.Rows[i].Cells[headerPlanID].Value);

                    if (planID == -1)
                    {
                        dgvMac.Rows[i].Cells[headerStartDate].Value = dtpStartDate.Value.Date;
                        dgvMac.Rows[i].Cells[headerEndDate].Value = dtpEstimateEndDate.Value.Date;
                    }
                }
                errorProvider3.Clear();
            }
           

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
                        my_menu.Items.Add(familyWithThisPlan).Name = familyWithThisPlan;
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
            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerPlanID].Value);

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

            else if (itemClicked.Equals(familyWithThisPlan))
            {
                //get familyWith data of selected plan
                int selectedFamilyWith = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerFamilyWith].Value);
                DateTime start = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerStartDate].Value);

                dtpStartDate.Value = start.Date;
                //DateTime selectedFamilyWith = Convert.ToDateTime(dgv.Rows[rowIndex].Cells[headerFamilyWith].Value);

                //if familyWith = -1, get selected plan id
                if (selectedFamilyWith == -1)
                {
                    //set selected plan and to add plan 's familyWith = selected plan id
                    selectedFamilyWith = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerPlanID].Value);
                    dgv.Rows[rowIndex].Cells[headerFamilyWith].Value = selectedFamilyWith;
                    dgv.Rows[rowIndex].Cells[headerRemark].Value = text.planning_Family_mould_Remark;
                    insertChange = true;
                }
                ToRunPlanFamilyWith = selectedFamilyWith;

                InsertBelow();
            }


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

            DataTable dt = newAutoAdjustDate();

            DataTable dtSource = (DataTable)dgvMac.DataSource;

            DataView dv = dtSource.DefaultView;
            dv.Sort = "START ASC,ESTIMATE END ASC ";
            DataTable sortedDT = dv.ToTable();

            dgvMac.DataSource = sortedDT;
        }
    }
}
