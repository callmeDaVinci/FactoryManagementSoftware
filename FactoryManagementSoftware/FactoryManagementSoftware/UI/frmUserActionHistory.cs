using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmUserActionHistory : Form
    {
        public frmUserActionHistory()
        {
            InitializeComponent();
        }

        public frmUserActionHistory(DataTable dt)
        {
            InitializeComponent();
            AddColumns(dgvUserHistory);
            LoadUserHistoryList(dt);
            dgvUserHistory.ClearSelection();
        }

        Tool tool = new Tool();
        historyDAL dalHistory = new historyDAL();
        userDAL dalUser = new userDAL();

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

        private void LoadUserHistoryList(DataTable dt)
        {
            DataGridView dgv = dgvUserHistory;
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
    }
}
