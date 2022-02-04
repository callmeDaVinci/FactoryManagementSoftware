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

        private bool fromSBBDeliveredPage = false;

        public frmChangeDate()
        {
            InitializeComponent();
        }

        public frmChangeDate(DateTime from, DateTime to)
        {
            InitializeComponent();
            fromSBBDeliveredPage = true;
            start = from;
            end = to;

            dtpStartDate.Value = start;
            dtpEstimateEndDate.Value = end;
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
            if(fromSBBDeliveredPage)
            {
                int day = dtpStartDate.Value.Day;

               
                if(day == 23)
                    dtpEstimateEndDate.Value = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.AddMonths(1).Month, 22);
                else
                    dtpEstimateEndDate.Value = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, DateTime.DaysInMonth(dtpStartDate.Value.Year, dtpStartDate.Value.Month));

            }
            else
            {
                dtpEstimateEndDate.Value = tool.EstimateEndDate(dtpStartDate.Value.Date, proDayRequired, includeSunday);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            start = dtpStartDate.Value.Date;
            end = dtpEstimateEndDate.Value.Date;

            dateSaved = true;

            if(fromSBBDeliveredPage)
            {
                frmSBB.MonthlyDateStart = start;
                frmSBB.MonthlyDateEnd = end;
            }

            Close();
        }
    }
}
