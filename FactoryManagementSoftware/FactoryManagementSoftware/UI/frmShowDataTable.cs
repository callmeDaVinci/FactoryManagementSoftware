using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmShowDataTable : Form
    {
        public frmShowDataTable()
        {
            InitializeComponent();
        }

        public frmShowDataTable(DataTable dt)
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            dgvList.DataSource = dt;
            DgvUIEdit(dgvList);
        }

        readonly string header_BalAfter = "BAL. AFTER";
        Tool tool = new Tool();

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

          
            dgv.Columns[header_BalAfter].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
           
        }

        private void frmShowDataTable_Load(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if(dgv.Columns[col].Name == header_BalAfter)
            {
                int balanceAfter = int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out balanceAfter) ? balanceAfter : 0;

                if(balanceAfter < 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
        }
    }
}
