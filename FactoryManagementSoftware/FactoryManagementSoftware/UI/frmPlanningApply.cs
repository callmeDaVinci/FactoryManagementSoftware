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
    public partial class frmPlanningApply : Form
    {

        #region declare object/ variable

        Tool tool = new Tool();
        Text text = new Text();
        MacDAL dalMac = new MacDAL();
        MacBLL uMac = new MacBLL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();

        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        itemDAL dalItem = new itemDAL();

        userDAL dalUser = new userDAL();

        readonly string headerID = "ID";
        readonly string headerName = "NAME";
        readonly string headerTon = "TON";
        readonly string headerLocation = "LOCATION";
        readonly string headerRunning = "RUNNING";
        readonly string headerEstimateEnd = "ESTIMATE END";
        readonly string headerNext = "NEXT";
        readonly string headerNextStart = "NEXT PLANNED START";
        readonly string headerNextEnd = "NEXT ESTIMATE END";

        private int proDayRequired = 0;

        static public bool dataSaved = false;
        private bool loaded = false;
        private DataTable dt_mat;

        #endregion

        public frmPlanningApply()
        {
            InitializeComponent();
            loadMacData();
            tool.loadMacIDToComboBox(cmbID);
        }

        public frmPlanningApply(PlanningBLL u, DataTable dt)
        {
            InitializeComponent();
            uPlanning = u;
            dt_mat = dt;
            loadMacData();
            //loadMacIDToComboBox(cmbID);
            tool.loadMacIDToComboBox(cmbID);
            //cmbID.SelectedIndex = -1;
            dtpStartDate.Value = DateTime.Today.AddDays(1);

            Summary(u);

            float hours = Convert.ToSingle(u.production_hour);

            int day = Convert.ToInt32(u.production_day);

            if(hours > 0)
            {
                proDayRequired = day + 1;
            }
            else
            {
                proDayRequired = day;
            }

            EstimateEndDate();
            //dtpStartDate.CalendarTitleForeColor = Color.Red;
        }

        private void EstimateEndDate()
        {
            DateTime estimateEndDate = dtpStartDate.Value;

            for(int i = 1; i < proDayRequired; i++)
            {
                estimateEndDate = estimateEndDate.AddDays(1);

                if(!cbIncludeSunday.Checked)
                {
                    DayOfWeek day = estimateEndDate.DayOfWeek;
                    if (day == DayOfWeek.Sunday)
                    {
                        estimateEndDate = estimateEndDate.AddDays(1);
                    }
                }
            }

            dtpEstimateEndDate.Value = estimateEndDate;
            checkDateAvailable();
        }

        private bool checkDateAvailable()
        {
            bool available = false;
            if (!string.IsNullOrEmpty(cmbID.Text) && loaded)
            {
                DateTime start = dtpStartDate.Value;
                DateTime end  = dtpEstimateEndDate.Value;

                errorProvider2.Clear();
                errorProvider3.Clear();

                available = tool.ifProductionDateAvailable(cmbID.Text, start, end);

                if (!available)
                {
                    //MessageBox.Show("date fail");
                    errorProvider2.SetError(lblStartDate, "Production date clash with other plan");
                    errorProvider3.SetError(lblEndDate, "Production date clash with other plan");
                }
            }

            return available;
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbID.Text))
            {
                result = false;
                errorProvider1.SetError(cmbID, "Machine ID Required");
            }

            result = checkDateAvailable();

            return result;
        }

        private void Summary(PlanningBLL u)
        {
            lblPartName.Text = u.part_name;
            lblPartCode.Text = u.part_code;
            lblQuoTon.Text = u.quo_ton;
            lblCycleTime.Text = u.cycle_time + " SEC";
            lblProductionFor.Text = u.production_purpose;

            lblMaterial.Text = u.material_code;
            lblBagQty.Text = u.material_bag_qty + " x " + u.material_bag_kg + " KG";
            lblRecycleUse.Text = u.material_recycle_use + " KG";

            lblColor.Text = u.part_color;
            lblColorMaterial.Text = u.color_material_code;
            lblUsage.Text = u.color_material_usage + " %";
            lblColorMatQty.Text = u.color_material_qty + " KG";

            lblTargetQty.Text = u.production_target_qty;
            lblAbleToProduce.Text = u.production_able_produce_qty;
            lblProductionDay.Text = u.production_day + " DAY(" + u.production_hour_per_day+") + "+u.production_hour + " HRS";
        }

        private DataTable NewMacTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerTon, typeof(int));
            dt.Columns.Add(headerLocation, typeof(string));
            dt.Columns.Add(headerRunning, typeof(string));
            dt.Columns.Add(headerEstimateEnd, typeof(DateTime));
            dt.Columns.Add(headerNext, typeof(string));
            dt.Columns.Add(headerNextStart, typeof(DateTime));
            dt.Columns.Add(headerNextEnd, typeof(DateTime));

            return dt;
        }

        private void dgvMacUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerTon].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerLocation].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerRunning].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerEstimateEnd].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerNext].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerNextStart].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerNextEnd].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerEstimateEnd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerNextStart].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerNextEnd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void loadMacData()
        {
            DataTable dt = dalMac.Select();

            DataTable dt_Mac = NewMacTable();
            DataRow row_Mac;

            DataTable dt_Plan = dalPlanning.Select();

            foreach (DataRow row in dt.Rows)
            {
                row_Mac = dt_Mac.NewRow();

                row_Mac[headerID] = row[dalMac.MacID];
                row_Mac[headerName] = row[dalMac.MacName];
                row_Mac[headerTon] = row[dalMac.MacTon];
                row_Mac[headerLocation] = row[dalMac.MacLocation];

                string runningItem = "", nextItem = "";
                DateTime runningEnd = new DateTime(), nextStart = new DateTime(), nextEnd = new DateTime();

                dt_Plan.AcceptChanges();
                foreach (DataRow plan in dt_Plan.Rows)
                {
                    if(row[dalMac.MacID].Equals(plan[dalPlanning.machineID]))
                    {
                        if(plan[dalPlanning.planStatus].Equals(text.planning_status_running))
                        {
                            runningItem = "(" + plan[dalPlanning.partCode].ToString() + ") " + plan[dalItem.ItemName].ToString();
                            runningEnd = Convert.ToDateTime(plan[dalPlanning.productionEndDate]);

                        }
                        else if(plan[dalPlanning.planStatus].Equals(text.planning_status_pending))
                        {
                            if(nextStart != default(DateTime))
                            {
                                DateTime temp = Convert.ToDateTime(plan[dalPlanning.productionStartDate]);

                                if(temp < nextStart)
                                {
                                    nextItem = "(" + plan[dalPlanning.partCode].ToString() + ") " + plan[dalItem.ItemName].ToString();
                                    nextStart = Convert.ToDateTime(plan[dalPlanning.productionStartDate]);
                                    nextEnd = Convert.ToDateTime(plan[dalPlanning.productionEndDate]);
                                }
                            }
                            else
                            {
                                nextItem = "(" + plan[dalPlanning.partCode].ToString() + ") " + plan[dalItem.ItemName].ToString();
                                nextStart = Convert.ToDateTime(plan[dalPlanning.productionStartDate]);
                                nextEnd = Convert.ToDateTime(plan[dalPlanning.productionEndDate]);
                            }
                        }

                        plan.Delete();
                    }
                    
                }
                dt_Plan.AcceptChanges();

                row_Mac[headerRunning] = runningItem;
                row_Mac[headerRunning] = runningItem;
                row_Mac[headerEstimateEnd] = runningEnd;

                row_Mac[headerNext] = nextItem;
                row_Mac[headerNextStart] = nextStart;
                row_Mac[headerNextEnd] = nextEnd;

                dt_Mac.Rows.Add(row_Mac);
            }

            dgvMac.DataSource = null;
            if (dt_Mac.Rows.Count > 0)
            {
                dgvMac.DataSource = dt_Mac;
                dgvMacUIEdit(dgvMac);
                dgvMac.ClearSelection();
            }
        }

        private void frmPlanningApply_Load(object sender, EventArgs e)
        {
            dgvMac.ClearSelection();
            loaded = true;
        }

        public void loadMacIDToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMac.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
        }

        private void dgvMac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

            if(row != -1)
            {
                cmbID.Text = dgvMac.Rows[row].Cells[headerID].Value.ToString();

            }
        }

        private void cmbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            string keyword = cmbID.Text;

            if(!string.IsNullOrEmpty(keyword))
            {
                dgvMac.ClearSelection();


                for(int i = 0; i < dgvMac.Rows.Count; i++)
                {
                    string id = dgvMac.Rows[i].Cells[headerID].Value.ToString();
                    if(id.Equals(keyword))
                    {
                        dgvMac.Rows[i].Selected = true;
                        break;
                    }
                }
            }

            checkDateAvailable();
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            EstimateEndDate();
        }

        private void cbIncludeSunday_CheckedChanged(object sender, EventArgs e)
        {
            EstimateEndDate();
        }

        public void StartForm()
        {
            Application.Run(new frmLoading());
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

            Thread t = null;

            try
            {
                
                if (Validation())
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to process this planning?", "Message",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        t = new Thread(new ThreadStart(StartForm));
                        //save planning data
                        DateTime date = DateTime.Now;
                        string note = txtNote.Text;

                        uPlanning.plan_status = text.planning_status_pending;
                        uPlanning.plan_note = note;
                        uPlanning.machine_id = Convert.ToInt32(cmbID.Text);
                        uPlanning.production_start_date = dtpStartDate.Value;
                        uPlanning.production_end_date = dtpEstimateEndDate.Value;
                        uPlanning.plan_added_date = date;
                        uPlanning.plan_added_by = MainDashboard.USER_ID;


                        bool success = false;
                        success = dalPlanning.Insert(uPlanning);


                        if (!success)
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to save Machine data");
                            tool.historyRecord(text.System, "Failed to save planning data", date, MainDashboard.USER_ID);
                        }
                        else
                        {
                            dataSaved = true;
                            tool.historyRecord(text.System, "Save Planning Data", date, MainDashboard.USER_ID);

                            //save material plan to use qty

                            DataTable dt = dalMatPlan.Select();

                            if(dt_mat.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt_mat.Rows)
                                {
                                    string matCode = row[frmPlanning.headerCode].ToString();
                                    float planToUse = row[frmPlanning.headerQtyNeedForThisPlanning] == null ? 0 : Convert.ToSingle(row[frmPlanning.headerQtyNeedForThisPlanning]);

                                    tool.matPlanAddQty(dt, matCode, planToUse);
                                    ////check if exist
                                    //bool dataExist = false;

                                    //foreach (DataRow row2 in dt.Rows)
                                    //{
                                    //    string mat_code = row2[dalMatPlan.MatCode].ToString();

                                    //    if(mat_code.Equals(matCode))
                                    //    {
                                    //        dataExist = true;
                                    //        float temp = row2[dalMatPlan.PlanToUse] == null ? 0 : Convert.ToSingle(row2[dalMatPlan.PlanToUse]);
                                    //        planToUse += temp;
                                    //    }
                                    //}

                                    //bool successSave = false;
                                    //uMatPlan.mat_code = matCode;
                                    //uMatPlan.plan_to_use = planToUse;

                                    //if (dataExist)
                                    //{
                                    //    //update
                                    //    successSave = dalMatPlan.Update(uMatPlan);
                                    //}
                                    //else
                                    //{
                                    //    //insert
                                    //    successSave = dalMatPlan.Insert(uMatPlan);
                                    //}

                                    //if (!successSave)
                                    //{
                                    //    //Failed to insert data
                                    //    MessageBox.Show("Failed to save Material to use data");
                                    //    tool.historyRecord(text.System, "Failed to save Material to use data", date, MainDashboard.USER_ID);
                                    //}
                                }
                            }

                            Close();
                        }

                    }
                }

            }
            catch (ThreadAbortException)
            {
                // ignore it
                Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                if(t != null)
                t.Abort();
            }

           
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvMac_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMac;

            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            int R, G, B;
            double brightness = 0.3;

            if (dgv.Columns[col].Name == headerRunning)
            {
                string value = dgv.Rows[row].Cells[headerRunning].Value.ToString();
                

                if (!string.IsNullOrEmpty(value))
                {
                    R = 0;
                    G = 184;
                    B = 148;

                    R = tool.min(255, R * (1 + brightness));
                    G = tool.min(255, G * (1 + brightness));
                    B = tool.min(255, B * (1 + brightness));

                    dgv.Rows[row].Cells[headerRunning].Style.BackColor = Color.FromArgb(R, G, B);
                    dgv.Rows[row].Cells[headerEstimateEnd].Style.BackColor = Color.FromArgb(R, G, B);
                }
                else
                {
                    R = 255;
                    G = 255;
                    B = 255;
                    dgv.Rows[row].Cells[headerRunning].Style.BackColor = Color.FromArgb(R, G, B);
                    dgv.Rows[row].Cells[headerEstimateEnd].Style.BackColor = Color.FromArgb(R, G, B);
                }
            }

            else if (dgv.Columns[col].Name == headerNext)
            {
                string value = dgv.Rows[row].Cells[headerNext].Value.ToString();


                if (!string.IsNullOrEmpty(value))
                {
                    R = 52;
                    G = 160;
                    B = 225;

                    R = tool.min(255, R * (1 + brightness));
                    G = tool.min(255, G * (1 + brightness));
                    B = tool.min(255, B * (1 + brightness));

                    dgv.Rows[row].Cells[headerNext].Style.BackColor = Color.FromArgb(R, G, B);
                    dgv.Rows[row].Cells[headerNextStart].Style.BackColor = Color.FromArgb(R, G, B);
                    dgv.Rows[row].Cells[headerNextEnd].Style.BackColor = Color.FromArgb(R, G, B);
                }
                else
                {
                    R = 255;
                    G = 255;
                    B = 255;

                    dgv.Rows[row].Cells[headerNext].Style.BackColor = Color.FromArgb(R, G, B);
                    dgv.Rows[row].Cells[headerNextStart].Style.BackColor = Color.FromArgb(R, G, B);
                    dgv.Rows[row].Cells[headerNextEnd].Style.BackColor = Color.FromArgb(R, G, B);
                }
            }

            dgv.ResumeLayout();
           
        }

        private void dtpEstimateEndDate_ValueChanged(object sender, EventArgs e)
        {
            checkDateAvailable();
        }

        private void btnSetBySystem_Click(object sender, EventArgs e)
        {
            string macID = cmbID.Text;

            if (string.IsNullOrEmpty(macID))
            {
                errorProvider1.SetError(cmbID, "Machine ID Required");
            }
            else
            {
                bool includeSunday = false;

                if(cbIncludeSunday.Checked)
                {
                    includeSunday = true;
                }
                if(cbToLast.Checked)
                {
                    dtpStartDate.Value = tool.lastAvailableDate(macID, includeSunday);
                }
                else
                {
                    dtpStartDate.Value = tool.earlierAvailableDate(macID, includeSunday ,uPlanning);
                }
            }
        }

        private void cbToLast_CheckedChanged(object sender, EventArgs e)
        {
            if(cbToLast.Checked)
            {
                cbEarlier.Checked = false;
            }
        }

        private void cbEarlier_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEarlier.Checked)
            {
                cbToLast.Checked = false;
            }
        }

        private void dgvMac_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvMac.CurrentCell = dgvMac.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvMac.Rows[e.RowIndex].Selected = true;
                dgvMac.Focus();
                int rowIndex = dgvMac.CurrentCell.RowIndex;

                try
                {
                    my_menu.Items.Add("Edit Schedule").Name = "Edit Schedule";

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow;
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvMac;
            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals("Edit Schedule"))
            {
                checkSchedule(macID);
            }

            dgvMac.Rows[rowIndex].Selected = true;
            dgvMac.Focus();
            //lblUpdatedTime.Text = DateTime.Now.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void checkSchedule(int macID)
        {
            int row = dgvMac.CurrentRow.Index;

            string itemName = lblPartName.Text, itemCode = lblPartCode.Text;
            DateTime start = dtpStartDate.Value;
            DateTime end = dtpEstimateEndDate.Value;

            frmMacScheduleAdjust frm = new frmMacScheduleAdjust(macID.ToString(),uPlanning,start,end);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(frmMacScheduleAdjust.applied)
            {
                dtpStartDate.Value = frmMacScheduleAdjust.start;
                dtpEstimateEndDate.Value = frmMacScheduleAdjust.end;
            }

            loadMacData();

            
        }
    }
}
