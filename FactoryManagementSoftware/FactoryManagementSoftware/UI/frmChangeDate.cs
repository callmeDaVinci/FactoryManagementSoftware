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
        Text text = new Text();

        static public DateTime start;
        static public DateTime end;
        static public bool dateChanged = false;
        private bool includeSunday = false;

        private bool MAC_SCHEDULE_JOB_DATE_EDIT = false;

        private int proDayRequired = 0;

        private bool fromSBBDeliveredPage = false;
        private bool FORM_LOADED = false;

        static public bool JOB_RUNNING_DATE_CONFIRMED = false;

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

        private int PRO_DAY = 0;
        private int PRO_HRS_PER_DAY = 0;
        private double PRO_BAL_HRS = 0;

        private int TOTAL_PRO_DAY = 0;

        public frmChangeDate(DataRow row_Job_editing)
        {
            InitializeComponent();
            dateChanged = false;

          
            MAC_SCHEDULE_JOB_DATE_EDIT = true;
            JOB_RUNNING_DATE_CONFIRMED = false;

            start = DateTime.TryParse(row_Job_editing[text.Header_DateStart].ToString(), out start) ? start.Date : DateTime.Now.Date;
            end = DateTime.TryParse(row_Job_editing[text.Header_EstDateEnd].ToString(), out end) ? end.Date : DateTime.Now.Date;

            PRO_DAY = int.TryParse(row_Job_editing[text.Header_ProductionDay].ToString(), out PRO_DAY) ? PRO_DAY : 0;
            PRO_HRS_PER_DAY = int.TryParse(row_Job_editing[text.Header_ProductionHourPerDay].ToString(), out PRO_HRS_PER_DAY) ? PRO_HRS_PER_DAY : 0;
            PRO_BAL_HRS = double.TryParse(row_Job_editing[text.Header_ProductionHour].ToString(), out PRO_BAL_HRS) ? PRO_BAL_HRS : 0;

            TOTAL_PRO_DAY = PRO_DAY + (PRO_BAL_HRS > 0 ? 1 : 0);

            int daysBetween = tool.TotalDaysBetween(start, end);
            int SundaysBetween = tool.TotalSundaysBetween(start, end);

            if (SundaysBetween > 0)
            {
                if (daysBetween - SundaysBetween == TOTAL_PRO_DAY)
                {
                    cbSundayInclude.Checked = false;
                }
                else if (daysBetween == TOTAL_PRO_DAY)
                {
                    cbSundayInclude.Checked = true;
                }
            }
           
            dtpStartDate.Value = start;
            dtpEstimateEndDate.Value = end;
        }

        public frmChangeDate(DateTime from , DateTime to , int day, bool sunday)
        {
            InitializeComponent();

            includeSunday = sunday;
            cbSundayInclude.Checked = sunday;

            start = from;
            end = to;

            dtpStartDate.Value = start;
            dtpEstimateEndDate.Value = end;

            proDayRequired = day;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateChanged = false;

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
            else if(MAC_SCHEDULE_JOB_DATE_EDIT)
            {
                ProductionEstimateEndDateCalculation();
            }
            else
            {
                dtpEstimateEndDate.Value = tool.EstimateEndDate(dtpStartDate.Value.Date, proDayRequired, includeSunday);
            }
        }



        private void ProductionEstimateEndDateCalculation()
        {
            if(FORM_LOADED)
            {
                dtpEstimateEndDate.Value = tool.CalculateEndDate(dtpStartDate.Value.Date, TOTAL_PRO_DAY - 1, cbSundayInclude.Checked);
            }
        }


        private void btnCheck_Click(object sender, EventArgs e)
        {
            if(MAC_SCHEDULE_JOB_DATE_EDIT)
            {
                JOB_RUNNING_DATE_CONFIRMED = true;

                DateTime Date_Start = dtpStartDate.Value.Date;
                DateTime Date_End = dtpEstimateEndDate.Value.Date;

                if(Date_Start != start.Date || Date_End != end.Date)
                {
                    dateChanged = true;

                    start = dtpStartDate.Value.Date;
                    end = dtpEstimateEndDate.Value.Date;

                }
            }
            else
            {
                start = dtpStartDate.Value.Date;
                end = dtpEstimateEndDate.Value.Date;

                dateChanged = true;

                if (fromSBBDeliveredPage)
                {
                    frmSBBVer2.MonthlyDateStart = start;
                    frmSBBVer2.MonthlyDateEnd = end;
                }
            }

            Close();
        }

        private void cbSundayInclude_CheckedChanged(object sender, EventArgs e)
        {
            if(MAC_SCHEDULE_JOB_DATE_EDIT)
            {
                ProductionEstimateEndDateCalculation();
            }
        }

        private void frmChangeDate_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmChangeDate_Load(object sender, EventArgs e)
        {
            FORM_LOADED = true;
        }
    }
}
