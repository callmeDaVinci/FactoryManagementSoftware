using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Syncfusion.XlsIO.Implementation.XmlSerialization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDataTableDisplay : Form
    {
        public frmDataTableDisplay()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvList, true);
        }
        public frmDataTableDisplay(DataTable dt)
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvList, true);

            DT_TO_DISPLAY = dt;

        }

        private DataTable DT_TO_DISPLAY;
        Tool tool = new Tool();
        Text text = new Text();

        private void frmDataTableDisplay_Load(object sender, System.EventArgs e)
        {
            if(DT_TO_DISPLAY != null)
            {
                dgvList.DataSource = DT_TO_DISPLAY;
                DgvUIEdit(dgvList);
                dgvList.ClearSelection();

            }
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgv.Columns[text.Header_NetAmount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_Remark].Visible = false;
            dgv.Columns["SHIP TO"].Visible = false;
            dgv.Columns["CUSTOMER CODE"].Visible = false;
           

        }

        private void frmDataTableDisplay_Shown(object sender, System.EventArgs e)
        {
            if (DT_TO_DISPLAY != null)
            {
                dgvList.DataSource = DT_TO_DISPLAY;
                DgvUIEdit(dgvList);
                dgvList.ClearSelection();

            }
        }
    }
}
