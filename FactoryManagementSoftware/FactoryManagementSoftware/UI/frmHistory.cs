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

            AddColumns(dgvHistory);

            DataTable dt = dalHistory.Select();
            LoadHistoryList(dt);
            dgvHistory.ClearSelection();
        }

        Tool tool = new Tool();
        userDAL dalUser = new userDAL();
        userBLL uUser = new userBLL();

        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;


        private void AddColumns(DataGridView dgv)
        {
            dgv.Columns.Clear();
            tool.AddTextBoxColumns(dgv, "ID", dalHistory.HistoryID, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Date", dalHistory.HistoryDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "User", dalHistory.HistoryBy, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Action", dalHistory.HistoryAction, Fill);
            tool.AddTextBoxColumns(dgv, "Detail", dalHistory.HistoryDetail, Fill);
        }

        private void LoadHistoryList(DataTable dt)
        {
            DataGridView dgv = dgvHistory;
            dgv.Rows.Clear();

            foreach (DataRow user in dt.Rows)
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[dalHistory.HistoryID].Value = user[dalHistory.HistoryID].ToString();
                dgv.Rows[n].Cells[dalHistory.HistoryDate].Value = user[dalHistory.HistoryDate].ToString();
                dgv.Rows[n].Cells[dalHistory.HistoryBy].Value = dalUser.getUsername(Convert.ToInt32(user[dalHistory.HistoryBy]));
                dgv.Rows[n].Cells[dalHistory.HistoryAction].Value = user[dalHistory.HistoryAction].ToString();
                dgv.Rows[n].Cells[dalHistory.HistoryDetail].Value = user[dalHistory.HistoryDetail].ToString();
            }
            tool.listPaint(dgv);
        }

        private void frmHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.historyFormOpen = false;
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            dgvHistory.ClearSelection();
        }
    }
}
