using Syncfusion.XlsIO.Implementation.XmlSerialization;
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
    public partial class frmForecastEditRecord : Form
    {
        public frmForecastEditRecord()
        {
            InitializeComponent();
        }

        private bool ALL_EDIT_HISTORY_MODE = false;

        public frmForecastEditRecord(DataTable dt)
        {
            InitializeComponent();
            ALL_EDIT_HISTORY_MODE = true;

            lblPart.Text = "";

            tool.DoubleBuffered(dgvForecastRecord, true);
            dgvForecastRecord.DataSource = dt;

            DgvForecastReportUIEdit(dgvForecastRecord);

            dgvForecastRecord.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        public frmForecastEditRecord(DataTable dt, string itemCode)
        {
            InitializeComponent();
            ALL_EDIT_HISTORY_MODE = false;

            lblPart.Text =new Tool().getItemName(itemCode) + " (" + itemCode + ")";

            tool.DoubleBuffered(dgvForecastRecord, true);
            dgvForecastRecord.DataSource = dt;

            DgvForecastReportUIEdit(dgvForecastRecord);

           

            dgvForecastRecord.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        Text text = new Text();
        Tool tool = new Tool();

        private void DgvForecastReportUIEdit(DataGridView dgv)
        {
            if (dgv != null)
            {
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //dgv.Columns[text.Header_ID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_ID].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_Description].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_OldValue].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                dgv.Columns[text.Header_Month].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[text.Header_NewValue].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        private void BoldLatestForecast(DataGridView dgv)
        {

            if(dgv != null && !ALL_EDIT_HISTORY_MODE)
            {
                bool DateBolded = false;
                DateTime previousMonth = DateTime.MaxValue;


                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DateTime UpdatedDate = DateTime.TryParse(row.Cells[text.Header_Date].Value.ToString(), out UpdatedDate) ? UpdatedDate : DateTime.MaxValue;
                    DateTime ForecastMonth = DateTime.TryParse(row.Cells[text.Header_Month].Value.ToString(), out ForecastMonth) ? ForecastMonth : DateTime.MaxValue;

                    int rowindex = row.Index;

                    if(previousMonth != ForecastMonth)
                    {
                        foreach (DataGridViewRow row2 in dgv.Rows)
                        {
                            DateTime UpdatedDate2 = DateTime.TryParse(row2.Cells[text.Header_Date].Value.ToString(), out UpdatedDate2) ? UpdatedDate2 : DateTime.MaxValue;
                            DateTime ForecastMonth2 = DateTime.TryParse(row2.Cells[text.Header_Month].Value.ToString(), out ForecastMonth2) ? ForecastMonth2 : DateTime.MaxValue;

                            if (ForecastMonth2 == ForecastMonth && UpdatedDate2 > UpdatedDate)
                            {
                                rowindex = row2.Index;

                                UpdatedDate = UpdatedDate2;
                            }
                        }

                        dgv.Rows[rowindex].Cells[text.Header_NewValue].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                        dgv.Rows[rowindex].Cells[text.Header_Month].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                        previousMonth = ForecastMonth;
                    }
                    


                }
            }
        }

        private void dgvForecastRecord_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            BoldLatestForecast(dgvForecastRecord);

        }
    }
}
