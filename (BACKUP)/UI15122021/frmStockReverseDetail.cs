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
    public partial class frmStockReverseDetail : Form
    {
        public frmStockReverseDetail()
        {
            InitializeComponent();
        }

        facDAL dalFac = new facDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();

        private DataTable dt_Fac;

        public frmStockReverseDetail(DataTable dt, string itemCode, float readyStock, string startDate, string endDate)
        {
            InitializeComponent();
            dt_Fac = dalFac.Select();
            dgvTrfHist.DataSource = dt;

            lblReadyStock.Text = readyStock.ToString();
            lblTrfHist.Text = "TRANSFER HISTORY: "+ startDate + "  -->  "+ endDate;
            calculateTotalInOut(dt, readyStock);

        }

        private void calculateTotalInOut(DataTable dt, float readyStock)
        {
            float totalIn = 0, totalOut = 0, reverseQty = 0;

            foreach (DataRow row in dt.Rows)
            {
                string from = row[dalTrfHist.TrfFrom].ToString();
                string to = row[dalTrfHist.TrfTo].ToString();

                if (tool.IfFactoryExists(dt_Fac, from) && tool.IfFactoryExists(dt_Fac, to))
                {
                    //from factory to factory

                    //if (row[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    //{
                    //    float trfQty = row[dalTrfHist.TrfQty] == null ? 0 : Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                    //    float oldQty = row[from] == null ? 0 : Convert.ToSingle(row[from].ToString());
                    //    float newQty = oldQty + trfQty;

                    //    row[from] = newQty;

                    //    oldQty = row[to] == null ? 0 : Convert.ToSingle(row[to].ToString());
                    //    newQty = oldQty - trfQty;

                    //    row[to] = newQty;
                    //}

                    //if (row[headerRepeat] != null)
                    //{
                    //    int i = Convert.ToInt16(row[headerRepeat]);

                    //    if (i == 1)
                    //    {
                    //        //row.Delete();
                    //    }
                    //}

                }

                else if (tool.IfFactoryExists(dt_Fac, from) && !tool.IfFactoryExists(dt_Fac, to))
                {
                    //from factory to non factory : OUT

                    if (row[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        float trfQty = row[dalTrfHist.TrfQty] == null ? 0 : Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                        totalOut += trfQty;
                    }

                }
                else if (!tool.IfFactoryExists(dt_Fac, from) && tool.IfFactoryExists(dt_Fac, to))
                {
                    //from non factory to factory : IN

                    if (row[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        float trfQty = row[dalTrfHist.TrfQty] == null ? 0 : Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                        totalIn += trfQty;
                    }
                }
            }

            lblTotalIn.Text = totalIn.ToString();
            lblTotalOut.Text = totalOut.ToString();

            lblCalculate.Text = readyStock + " - " + totalIn + " + " + totalOut + " = ";

            reverseQty = readyStock - totalIn + totalOut;

            lblReverseStock.Text = reverseQty.ToString();
        }

        private void dgvTrfHist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int row = e.RowIndex;

            string from = dgvTrfHist.Rows[row].Cells[dalTrfHist.TrfFrom].Value.ToString();
            string to = dgvTrfHist.Rows[row].Cells[dalTrfHist.TrfTo].Value.ToString();

            if (tool.IfFactoryExists(dt_Fac, from) && !tool.IfFactoryExists(dt_Fac, to))
            {
                //from factory to non factory : OUT
                
                if (dgvTrfHist.Rows[row].Cells[dalTrfHist.TrfResult].Value.ToString().Equals("Passed"))
                {
                    //red color
                    dgvTrfHist.Rows[row].Cells[dalTrfHist.TrfQty].Style.BackColor = Color.Red;
                    dgvTrfHist.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
                    dgvTrfHist.Rows[row].DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
                }
                else
                {
                    dgvTrfHist.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                    dgvTrfHist.Rows[row].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
                }

            }
            else if (!tool.IfFactoryExists(dt_Fac, from) && tool.IfFactoryExists(dt_Fac, to))
            {
                //from non factory to factory : IN

                if (dgvTrfHist.Rows[row].Cells[dalTrfHist.TrfResult].Value.ToString().Equals("Passed"))
                {
                    //green color
                    dgvTrfHist.Rows[row].Cells[dalTrfHist.TrfQty].Style.BackColor = Color.FromArgb(0, 192, 0);
                    dgvTrfHist.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
                    dgvTrfHist.Rows[row].DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
                }
                else
                {
                    dgvTrfHist.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                    dgvTrfHist.Rows[row].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
                }
            }

            dgvTrfHist.ClearSelection();
        }
    }
}
