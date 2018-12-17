using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmChildStockOutRecord : Form
    {
        public frmChildStockOutRecord()
        {
            InitializeComponent();
        }

        childTrfHistDAL dalChildTrf = new childTrfHistDAL();
        
        private void frmChildStockOutRecord_Load(object sender, EventArgs e)
        {
            int index = frmInOut.editingIndexNo;
            loadData(index);
        }

        private void loadData(int index)
        {
            DataTable dt = dalChildTrf.indexSearch(index);

            dgvData.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvData.Rows.Add();
                dgvData.Rows[n].Cells["child_trf_hist_id"].Value = item["child_trf_hist_id"].ToString();
                dgvData.Rows[n].Cells["child_trf_hist_code"].Value = item["child_trf_hist_code"].ToString();
                dgvData.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvData.Rows[n].Cells["child_trf_hist_from"].Value = item["child_trf_hist_from"].ToString();
                dgvData.Rows[n].Cells["child_trf_hist_to"].Value = item["child_trf_hist_to"].ToString();
                dgvData.Rows[n].Cells["child_trf_hist_qty"].Value = item["child_trf_hist_qty"].ToString();
                dgvData.Rows[n].Cells["child_trf_hist_unit"].Value = item["child_trf_hist_unit"].ToString();
                dgvData.Rows[n].Cells["child_trf_hist_result"].Value = item["child_trf_hist_result"].ToString();

                if (item["child_trf_hist_result"].ToString().Equals("Undo"))
                {
                    dgvData.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
                    dgvData.Rows[n].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Strikeout);
                }
            }
            listPaint(dgvData);
        }

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
    }
}
