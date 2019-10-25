using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport_NEW : Form
    {
        public frmForecastReport_NEW()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvForecastReport, true);
          

            tool.loadCustomerWithoutOtherToComboBox(cmbCustomer);

            InitializeFilterData();
        }

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";
        private bool loaded = false;

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        Tool tool = new Tool();

        Text text = new Text();
        private void InitializeFilterData()
        {
            loaded = false;

            cmbForecastFrom.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                     .MonthNames.Take(12).ToList();

            cmbForecastTo.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                    .MonthNames.Take(12).ToList();

            cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month); 
            cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(2).Month);
            loaded = true;
        }

        private void tlpForecast_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();
            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 175f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void frmForecastReport_NEW_Load(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();

            tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvForecastReport.ResumeLayout();

            btnFilter.Text = textMoreFilters;


            loaded = true;
        }

        private void cmbForecastFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbForecastFrom.SelectedIndex != -1)
            cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).AddMonths(2).Month);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if(cbWithSubMat.Checked)
            {
                cbWithSubMat.Checked = false;
            }
            else
            {
                cbWithSubMat.Checked = true;
            }
        }
    }

}

