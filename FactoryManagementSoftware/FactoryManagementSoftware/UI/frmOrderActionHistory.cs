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
    public partial class frmOrderActionHistory : Form
    {
        public frmOrderActionHistory(DataTable dt)
        {
            InitializeComponent();
            loadActionHistory(dt);
            dgvAction.ClearSelection();
        }

        private void loadActionHistory(DataTable dt)
        {
            dt.DefaultView.Sort = "order_action_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            foreach (DataRow action in sortedDt.Rows)
            {
                int n = dgvAction.Rows.Add();
                dgvAction.Rows[n].Cells["order_id"].Value = action["ord_id"].ToString();
                dgvAction.Rows[n].Cells["date"].Value = action["added_date"].ToString();
                dgvAction.Rows[n].Cells["by"].Value = action["added_by"].ToString();
                dgvAction.Rows[n].Cells["action"].Value = action["action"].ToString();
                dgvAction.Rows[n].Cells["note"].Value = action["note"].ToString();
            }
            listPaint(dgvAction);
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ClearSelection();
        }

        private void dgvAction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                dgvAction.ClearSelection();
            }
        }

        private void dgvAction_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvAction.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvAction.ClearSelection();
            }
        }

        private void frmOrderActionHistory_Load(object sender, EventArgs e)
        {
            dgvAction.ClearSelection();
        }
    }
}
