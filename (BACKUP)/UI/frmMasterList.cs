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
    public partial class frmMasterList : Form
    {
        public frmMasterList()
        {
            InitializeComponent();
        }

        public frmMasterList(DataTable dt)
        {
            InitializeComponent();

            dgvList.DataSource = dt;

            DgvUIEdit(dgvList);
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        private void frmMasterList_Shown(object sender, EventArgs e)
        {
            dgvList.Rows[dgvList.Rows.Count - 1].Cells[dgvList.Columns.Count - 1].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

            dgvList.Columns[dgvList.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dgvList.ClearSelection();
        }
    }
}
