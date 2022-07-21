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
    public partial class frmJustMachineSchedule : Form
    {
        public frmJustMachineSchedule()
        {
            InitializeComponent();
        }

        public frmJustMachineSchedule(DataTable dt, int planID)
        {
            InitializeComponent();
            loadScheduleData(dt, planID);
            lblMessage.Text = "Please select a row to family with:";
        }

        #region Variable/ Object

        readonly string headerPlanID = "PLAN ID";
        readonly string headerStartDate = "START";
        readonly string headerEndDate = "ESTIMATE END";
        readonly string headerPartName = "NAME";
        readonly string headerPartCode = "CODE";
        readonly string headerProductionPurpose = "PRODUCTION FOR";
        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerStatus = "STATUS";
        readonly string headerFamilyWith = "FAMILY WITH";
        readonly string headerRemark = "REMARK";
        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        planningActionDAL dalPlanningAction = new planningActionDAL();

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

        static public int currentSelectedRow = -1;
        static public int planID = -1;
        #endregion

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

        private void loadScheduleData(DataTable dt, int selectedPlanID)
        {
            DataTable dt_Schedule = NewScheduleTable();
            DataRow row_Schedule;
            bool match = true;

            foreach (DataRow row in dt.Rows)
            {
                match = true;
                int planID = Convert.ToInt32(row[headerPlanID]);

                if(planID == selectedPlanID)
                {
                    match = false;
                }

                #region Status Filtering

                //string status = row[dalPlanning.planStatus].ToString();

                //if (string.IsNullOrEmpty(status))
                //{
                //    match = false;
                //}

                //if (status.Equals(text.planning_status_cancelled))
                //{
                //    match = false;
                //}

                //if (status.Equals(text.planning_status_completed))
                //{
                //    match = false;
                //}

                #endregion

                if (match)
                {
                    row_Schedule = dt_Schedule.NewRow();

                    row_Schedule[headerPlanID] = planID;
                    row_Schedule[headerStartDate] = row[headerStartDate];
                    row_Schedule[headerEndDate] = row[headerEndDate];

                    row_Schedule[headerPartName] = row[headerPartName];
                    row_Schedule[headerPartCode] = row[headerPartCode];
                    row_Schedule[headerProductionPurpose] = row[headerProductionPurpose];

                    row_Schedule[headerTargetQty] = row[headerTargetQty];
                    row_Schedule[headerStatus] = row[headerStatus];
                    row_Schedule[headerFamilyWith] = row[headerFamilyWith];

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

        #endregion

        private void dgvMac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            int selectedRow = e.RowIndex;
            currentSelectedRow = selectedRow;

            //MessageBox.Show(selectedRow.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
                Close();
            }
        }

        private void frmJustMachineSchedule_Load(object sender, EventArgs e)
        {
            dgvMac.ClearSelection();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (currentSelectedRow < 0 || currentSelectedRow >= dgvMac.Rows.Count)
            {
                errorProvider1.SetError(lblMessage, "");
            }
            else
            {
                planID = Convert.ToInt32(dgvMac.Rows[currentSelectedRow].Cells[headerPlanID].Value);
                MessageBox.Show("Successful family with plan " + planID);
                
            }
        }
    }
}
