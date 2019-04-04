using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmHistory : Form
    {
        public frmHistory()
        {
            InitializeComponent();

            //AddColumns(dgvHistory);

            DataTable dt = dalHistory.Select();
            LoadHistoryList(dt);
            dgvHistory.ClearSelection();
        }

        Tool tool = new Tool();
        userDAL dalUser = new userDAL();
        userBLL uUser = new userBLL();

        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();

        private DateTime updatedTime;

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;


        private void AddColumns(DataGridView dgv)
        {
            dgv.Columns.Clear();
            tool.AddTextBoxColumns(dgv, "ID", dalHistory.HistoryID, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Date", dalHistory.HistoryDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "User", dalHistory.HistoryBy, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Action", dalHistory.HistoryAction, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Detail", dalHistory.HistoryDetail, Fill);
        }

        private void LoadHistoryList(DataTable dt)
        {
            DataGridView dgv = dgvHistory;
            //dgv.Rows.Clear();

            //foreach (DataRow user in dt.Rows)
            //{
            //    //int n = dgv.Rows.Add();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryID].Value = user[dalHistory.HistoryID].ToString();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryDate].Value = user[dalHistory.HistoryDate].ToString();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryBy].Value = dalUser.getUsername(Convert.ToInt32(user[dalHistory.HistoryBy]));
            //    //dgv.Rows[n].Cells[dalHistory.HistoryAction].Value = user[dalHistory.HistoryAction].ToString();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryDetail].Value = user[dalHistory.HistoryDetail].ToString();
            //}

           

            dgv.DataSource = dt;
            dgvUIEdit(dgv);
            tool.listPaint(dgv);

            //foreach (DataGridViewRow row in dgv.Rows)
            //{
            //    //currQty += row.Cells["qty"].Value;
            //    ////More code here
            //    //int n = row.Index;
            //    //int userID = Convert.ToInt32(dgv.Rows[row.Index].Cells[dalHistory.HistoryBy].Value.ToString());
            //    //dgv.Rows[row.Index].Cells[dalHistory.HistoryAction].Value += "[" + dalUser.getUsername(userID) + "]";
            //}

            dataUpdatedTime();
        }

        private void dataUpdatedTime()
        {
            updatedTime = DateTime.Now;
            lblUpdatedTime.Text = updatedTime.ToString();
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.Columns[dalHistory.HistoryID].HeaderText = "ID";
            dgv.Columns[dalHistory.HistoryDate].HeaderText = "Date";
            dgv.Columns[dalHistory.HistoryBy].HeaderText = "User";
            dgv.Columns[dalHistory.HistoryAction].HeaderText = "Action";
            dgv.Columns[dalHistory.HistoryDetail].HeaderText = "Detail";

            dgv.Columns[dalHistory.HistoryID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalHistory.HistoryDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalHistory.HistoryBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalHistory.HistoryBy].Visible = false;
            dgv.Columns[dalHistory.HistoryAction].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalHistory.HistoryDetail].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void frmHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.historyFormOpen = false;
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            dgvHistory.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = dalHistory.Select();
            LoadHistoryList(dt);
            dgvHistory.ClearSelection();
        }
    }
}
