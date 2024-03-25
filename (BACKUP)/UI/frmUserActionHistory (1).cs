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
        readonly string past3Days = "3";
        readonly string pastWeek = "7";
        readonly string pastMonth = "30";
        readonly string pastYear = "365";
        readonly string All = "ALL";

        public frmUserActionHistory()
        {
            InitializeComponent();
         
        }

        public frmUserActionHistory(DataTable dt)
        {
            InitializeComponent();
           
            //AddColumns(dgvUserHistory);
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

            DataTable tempDT = new DataTable();

            tempDT.Columns.Add("ID", typeof(string));
            tempDT.Columns.Add("Date", typeof(string));
            tempDT.Columns.Add("User", typeof(string));
            tempDT.Columns.Add("Action", typeof(string));
            tempDT.Columns.Add("Detail", typeof(string));



            //foreach (DataRow user in dt.Rows)
            //{
            //    DataRow row = tempDT.NewRow();
            //    row["ID"] = user[dalHistory.HistoryID].ToString();
            //    row["Date"] = user[dalHistory.HistoryDate].ToString();
            //    row["User"] = dalUser.getUsername(Convert.ToInt32(user[dalHistory.HistoryBy]));
            //    row["Action"] = user[dalHistory.HistoryAction].ToString();
            //    row["Detail"] = user[dalHistory.HistoryDetail].ToString();
            //    tempDT.Rows.Add(row);

            //    //int n = dgv.Rows.Add();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryID].Value = user[dalHistory.HistoryID].ToString();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryDate].Value = user[dalHistory.HistoryDate].ToString();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryBy].Value = dalUser.getUsername(Convert.ToInt32(user[dalHistory.HistoryBy]));
            //    //dgv.Rows[n].Cells[dalHistory.HistoryAction].Value = user[dalHistory.HistoryAction].ToString();
            //    //dgv.Rows[n].Cells[dalHistory.HistoryDetail].Value = user[dalHistory.HistoryDetail].ToString();
            //}

            dgv.DataSource = dt;
            tool.listPaint(dgv);
            dgvUIEdit(dgv);
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

       

        private void frmUserActionHistory_Load(object sender, EventArgs e)
        {

        }
    }
}
