using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;


namespace FactoryManagementSoftware.UI
{
    public partial class frmActionLog : Form
    {
        public frmActionLog()
        {
            InitializeComponent();
        }

        public frmActionLog(string tblName, string dataID)
        {
            InitializeComponent();
            dt_Log = dalHistory.LogSearch(tblName, dataID);
            LoadLogData();
        }

        historyDAL dalHistory = new historyDAL();
        readonly string header_ID = "ID";
        readonly string header_Date = "DATE";
        readonly string header_Action = "ACTION";
        readonly string header_Detail = "DETAIL";

        private DataTable dt_Log;

        private DataTable NewLogTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_ID, typeof(string));
            dt.Columns.Add(header_Date, typeof(DateTime));
            //dt.Columns.Add(header_User, typeof(string));
            dt.Columns.Add(header_Action, typeof(string));
            dt.Columns.Add(header_Detail, typeof(string));
            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[header_Detail].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_PONo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_DONo].Visible = false;
            //dgv.Columns[header_POCode].Visible = false;
            //dgv.Columns[header_CustomerCode].Visible = false;

        }

        private void LoadLogData()
        {
            DataTable dt = NewLogTable();

            if(dt_Log != null)
            {
                foreach(DataRow row in dt_Log.Rows)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[header_ID] = row[dalHistory.HistoryID];
                    newRow[header_Date] = row[dalHistory.HistoryDate];
                    newRow[header_Action] = row[dalHistory.HistoryAction];
                    newRow[header_Detail] = row[dalHistory.HistoryDetail];

                    dt.Rows.Add(newRow);
                }

                dgvLog.DataSource = dt;
                DgvUIEdit(dgvLog);
                dgvLog.ClearSelection();

            }
        }

        private void frmActionLog_Load(object sender, EventArgs e)
        {
            dgvLog.ClearSelection();
        }
    }
}
