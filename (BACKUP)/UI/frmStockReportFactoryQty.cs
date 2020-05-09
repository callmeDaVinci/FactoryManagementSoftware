using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockReportFactoryQty : Form
    {
        public frmStockReportFactoryQty()
        {
            InitializeComponent();
        }

        facStockDAL dalStock = new facStockDAL();

        private void listPaint(DataGridView dgv)
        {
            bool rowColorChange = true;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = SystemColors.Control;
                    rowColorChange = false;
                }
                else
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }
            }
            dgv.ClearSelection();
        }

        private void loadStockList(string itemCode)
        {
            DataTable dt = dalStock.Select(itemCode);

            dgvFactoryStock.Rows.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    int n = dgvFactoryStock.Rows.Add();
                    dgvFactoryStock.Rows[n].Cells["fac_name"].Value = stock["fac_name"].ToString();
                    dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");

                }

            }
            else
            {
                int n = dgvFactoryStock.Rows.Add();
                dgvFactoryStock.Rows[n].Cells["fac_name"].Value = "null";
                dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = "null";

            }
            listPaint(dgvFactoryStock);

        }

        private void frmStockReportFactoryQty_Load(object sender, EventArgs e)
        {
           // loadStockList(frmStockReport.itemCode);
        }
    }
}
