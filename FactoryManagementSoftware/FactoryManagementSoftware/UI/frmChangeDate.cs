using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmChangeDate : Form
    {
        Tool tool = new Tool();

        static public DateTime start;
        static public DateTime end;
        static public bool dateSaved = false;
        private bool includeSunday = false;

        private int proDayRequired = 0;
        public frmChangeDate()
        {
            InitializeComponent();
        }

        public frmChangeDate(DateTime from , DateTime to , int day, bool sunday)
        {
            InitializeComponent();

            includeSunday = sunday;
            start = from;
            end = to;

            dtpStartDate.Value = start;
            dtpEstimateEndDate.Value = end;

            proDayRequired = day;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEstimateEndDate.Value = tool.EstimateEndDate(dtpStartDate.Value.Date, proDayRequired, includeSunday);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            start = dtpStartDate.Value.Date;
            end = dtpEstimateEndDate.Value.Date;
            dateSaved = true;
            Close();
        }
    }
}
