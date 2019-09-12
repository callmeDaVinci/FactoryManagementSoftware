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
    public partial class frmPlanningActionHistory : Form
    {
        planningActionDAL dalPlanningAction = new planningActionDAL();
        userDAL dalUser = new userDAL();

        readonly string headerActionID = "ACTION ID";
        readonly string headerPlanningID = "PLAN ID";
        readonly string headerAddedDate = "ADDED DATE";
        readonly string headerAddedBy = "ADDED BY";
        readonly string headerAction = "ACTION";
        readonly string headerDetail = "DETAIL";
        readonly string headerFrom = "FROM";
        readonly string headerTo = "TO";
        readonly string headerNote = "NOTE";
        static public bool noData = false;
        public frmPlanningActionHistory(int PlanID)
        {
            InitializeComponent();

            DataTable dt = dalPlanningAction.SelectByPlanningID(PlanID);


            if(dt.Rows.Count > 0)
            {
                LoadPlanningActionHistory(dt);
            }
            else
            {
                MessageBox.Show("NO ACTION DATA EXIST!");
                noData = true;
            }

        }

        #region UI Setting

        private DataTable NewHistoryTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerActionID, typeof(int));
            dt.Columns.Add(headerPlanningID, typeof(int));
            dt.Columns.Add(headerAddedDate, typeof(DateTime));
            dt.Columns.Add(headerAddedBy, typeof(string));

            dt.Columns.Add(headerAction, typeof(string));
            dt.Columns.Add(headerDetail, typeof(string));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(headerTo, typeof(string));
            dt.Columns.Add(headerNote, typeof(string));

            return dt;
        }

        private void dgvHistoryUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerActionID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPlanningID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAddedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAddedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAction].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerDetail].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFrom].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////

            dgv.Columns[headerActionID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPlanningID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFrom].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerDetail].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        #endregion

        #region Load Data

        private void LoadPlanningActionHistory(DataTable dt)
        {
            DataTable dt_History = NewHistoryTable();
            DataRow row_History;
            foreach (DataRow row in dt.Rows)
            {
                row_History = dt_History.NewRow();
                int userID = -1;
                if (row[dalPlanningAction.ActionAddedBy] != null)
                {
                    userID = Convert.ToInt32(row[dalPlanningAction.ActionAddedBy]);
                }

                string userName = dalUser.getUsername(userID);

                row_History[headerActionID] = row[dalPlanningAction.ActionID];
                row_History[headerPlanningID] = row[dalPlanningAction.PlanID];
                row_History[headerAddedDate] = row[dalPlanningAction.ActionAddedDate];
                row_History[headerAddedBy] = userName;
                row_History[headerAction] = row[dalPlanningAction.Action];
                row_History[headerDetail] = row[dalPlanningAction.ActionDetail];
                row_History[headerFrom] = row[dalPlanningAction.ActionFrom];
                row_History[headerTo] = row[dalPlanningAction.ActionTo];
                row_History[headerNote] = row[dalPlanningAction.ActioNote];

                dt_History.Rows.Add(row_History);
            }

            dgvActionHistory.DataSource = null;

            if (dt_History.Rows.Count > 0)
            {
                dgvActionHistory.DataSource = dt_History;
                dgvHistoryUIEdit(dgvActionHistory);
                dgvActionHistory.ClearSelection();
            }
        }

        #endregion

        private void frmPlanningActionHistory_Load(object sender, EventArgs e)
        {
            dgvActionHistory.ClearSelection();
            if(noData)
            Close();
        }
    }
}
