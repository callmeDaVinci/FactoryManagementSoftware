using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmFactoryTrfRecord : Form
    {
        public frmFactoryTrfRecord()
        {
            InitializeComponent();
        }

        trfHistDAL daltrfHist = new trfHistDAL();

        private void frmFactoryTrfRecord_Load(object sender, EventArgs e)
        {
            loadTransferList();
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

        private void loadTransferList()
        {
            string itemCode = frmInOut.itemCode;
            string facName = frmInOut.facName;
            DataTable dt = daltrfHist.facSearch(itemCode,facName);

            dgvTrf.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "trf_hist_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                foreach (DataRow trf in sortedDt.Rows)
                {
                    int n = dgvTrf.Rows.Add();
                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["item_name"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_note"].Value = trf["trf_hist_note"].ToString();
                    dgvTrf.Rows[n].Cells["trf_result"].Value = trf["trf_result"].ToString();
                }
                listPaint(dgvTrf);
            }
        }
    }
}
