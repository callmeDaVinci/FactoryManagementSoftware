using System;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Windows.Forms;
using System.Data;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPMMADateEdit : Form
    {
        public frmPMMADateEdit()
        {
            InitializeComponent();

            InitializeDate();
        }

        static public bool dateChanged = false;
        static public bool Edited = false;
        private int oldYear = DateTime.Now.Year;
        readonly string startDateString = "StartDate";
        readonly string endDateString = "EndDate";

        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        pmmaDateBLL uPmmaDate = new pmmaDateBLL();

        Tool tool = new Tool();
        Text text = new Text();

        DataTable dt_PmmaDate;

        #region INITIALIZE/ LOAD DATA/ SAVE DATA

        private void InitializeDate()
        {
            dtpYear.Format = DateTimePickerFormat.Custom;
            dtpYear.CustomFormat = "yyyy";
            dtpYear.ShowUpDown = true;// to prevent the calendar from being displayed

            dt_PmmaDate = dalPmmaDate.Select();
            LoadDate();
            Edited = false;
        }

        private void LoadDate()
        {
            int year = dtpYear.Value.Year;

            dtpJanStart.Value = GetStartDate(1,year,dt_PmmaDate);
            dtpJanEnd.Value = GetEndDate(1, year, dt_PmmaDate);

            dtpFebStart.Value = GetStartDate(2, year, dt_PmmaDate);
            dtpFebEnd.Value = GetEndDate(2, year, dt_PmmaDate);

            dtpMarStart.Value = GetStartDate(3, year, dt_PmmaDate);
            dtpMarEnd.Value = GetEndDate(3, year, dt_PmmaDate);

            dtpAprStart.Value = GetStartDate(4, year, dt_PmmaDate);
            dtpAprEnd.Value = GetEndDate(4, year, dt_PmmaDate);

            dtpMayStart.Value = GetStartDate(5, year, dt_PmmaDate);
            dtpMayEnd.Value = GetEndDate(5, year, dt_PmmaDate);

            dtpJunStart.Value = GetStartDate(6, year, dt_PmmaDate);
            dtpJunEnd.Value = GetEndDate(6, year, dt_PmmaDate);

            dtpJulStart.Value = GetStartDate(7, year, dt_PmmaDate);
            dtpJulEnd.Value = GetEndDate(7, year, dt_PmmaDate);

            dtpAugStart.Value = GetStartDate(8, year, dt_PmmaDate);
            dtpAugEnd.Value = GetEndDate(8, year, dt_PmmaDate);

            dtpSepStart.Value = GetStartDate(9, year, dt_PmmaDate);
            dtpSepEnd.Value = GetEndDate(9, year, dt_PmmaDate);

            dtpOctStart.Value = GetStartDate(10, year, dt_PmmaDate);
            dtpOctEnd.Value = GetEndDate(10, year, dt_PmmaDate);

            dtpNovStart.Value = GetStartDate(11, year, dt_PmmaDate);
            dtpNovEnd.Value = GetEndDate(11, year, dt_PmmaDate);

            dtpDecStart.Value = GetStartDate(12, year, dt_PmmaDate);
            dtpDecEnd.Value = GetEndDate(12, year, dt_PmmaDate);

            Edited = false;
        }

        private DateTime GetStartDate(int month, int year, DataTable dt)
        {
            DateTime date = new DateTime(year, month, 1);

            bool recordFound = false;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateStart]);

                        recordFound = true;
                        break;
                    }
                }

                if(!recordFound)
                {
                    //find last year December date

                    int lastYear = year - 1;
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[dalPmmaDate.dateYear].ToString() == lastYear.ToString() && row[dalPmmaDate.dateMonth].ToString() == 12.ToString())
                        {
                            date = Convert.ToDateTime(row[dalPmmaDate.dateEnd]).AddDays(1);

                            recordFound = true;
                            break;
                        }
                    }
                }
            }

            return date;
        }

        private DateTime GetEndDate(int month, int year, DataTable dt)
        {
            var lastDayOfMonth = DateTime.DaysInMonth(year, month);

            DateTime date = new DateTime(year, month, lastDayOfMonth);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateEnd]);
                    }
                }
            }

            return date;
        }

        private void saveDate()
        {
            bool success = false;

            int year = dtpYear.Value.Year;
            int month;
            DateTime startDate, endDate;

            #region SAVE EACH MONTH START & END DATE

            month = 1;
            startDate = dtpJanStart.Value;
            endDate = dtpJanEnd.Value;
            success = saveDate(year,month,startDate,endDate);

            month = 2;
            startDate = dtpFebStart.Value;
            endDate = dtpFebEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 3;
            startDate = dtpMarStart.Value;
            endDate = dtpMarEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 4;
            startDate = dtpAprStart.Value;
            endDate = dtpAprEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 5;
            startDate = dtpMayStart.Value;
            endDate = dtpMayEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 6;
            startDate = dtpJunStart.Value;
            endDate = dtpJunEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 7;
            startDate = dtpJulStart.Value;
            endDate = dtpJulEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 8;
            startDate = dtpAugStart.Value;
            endDate = dtpAugEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 9;
            startDate = dtpSepStart.Value;
            endDate = dtpSepEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 10;
            startDate = dtpOctStart.Value;
            endDate = dtpOctEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 11;
            startDate = dtpNovStart.Value;
            endDate = dtpNovEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            month = 12;
            startDate = dtpDecStart.Value;
            endDate = dtpDecEnd.Value;
            success = saveDate(year, month, startDate, endDate);

            #endregion

            dt_PmmaDate = dalPmmaDate.Select();

           

            if(success)
            {
                MessageBox.Show("The data was successfully updated.");

                if(Edited)
                {
                    dateChanged = true;
                }
            }
            Edited = false;
        }

        private DataTable checkIfExist(int year, int month)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(startDateString);
            dt.Columns.Add(endDateString);

            DataRow dr = dt.NewRow();
            foreach (DataRow row in dt_PmmaDate.Rows)
            {
                if(Convert.ToInt16(row[dalPmmaDate.dateYear]) == year && Convert.ToInt16(row[dalPmmaDate.dateMonth]) == month)
                {
                    dr[startDateString] = row[dalPmmaDate.dateStart];
                    dr[endDateString] = row[dalPmmaDate.dateEnd];
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        private bool saveDate(int year, int month, DateTime startDate, DateTime endDate)
        {
            bool success = true;
            DataTable dt = checkIfExist(year,month);
            if(dt.Rows.Count > 0)
            {
                DateTime oldStartDate = startDate, oldEndDate = endDate;

                foreach(DataRow row in dt.Rows)
                {
                    oldStartDate = Convert.ToDateTime(row[startDateString]);
                    oldEndDate = Convert.ToDateTime(row[endDateString]);
                }

                if(oldStartDate != startDate || oldEndDate != endDate)
                {
                    //update data
                    uPmmaDate.year = year;
                    uPmmaDate.month = month;
                    uPmmaDate.date_start = startDate;
                    uPmmaDate.date_end = endDate;
                    uPmmaDate.updated_by = MainDashboard.USER_ID;
                    uPmmaDate.updated_date = DateTime.Now;

                    success = dalPmmaDate.Update(uPmmaDate);

                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to update date data");
                        tool.historyRecord(text.System, "Failed to update date data (pmmaDateEdit)", uPmmaDate.updated_date, MainDashboard.USER_ID);
                    }
                    else
                    {
                        if (oldStartDate != startDate && oldEndDate != endDate)
                        {
                            tool.historyRecord("PMMA Date Updated", "(" + year + "/" + month + ")" + "Start Date: "+oldStartDate.ToShortDateString()+" --> "+startDate.ToShortDateString() + ", End Date:" + oldEndDate.ToShortDateString() + " --> " + endDate.ToShortDateString(), uPmmaDate.updated_date, MainDashboard.USER_ID);
                        }
                        else if (oldStartDate != startDate)
                        {
                            tool.historyRecord("PMMA Date Updated", "(" + year + "/" + month + ")" + "Start Date: " + oldStartDate.ToShortDateString() + " --> " + startDate.ToShortDateString(), uPmmaDate.updated_date, MainDashboard.USER_ID);
                        }
                        else if (oldEndDate != endDate)
                        {
                            tool.historyRecord("PMMA Date Updated", "(" + year + "/" + month + ")" + "End Date: " + oldEndDate.ToShortDateString() + " --> " + endDate.ToShortDateString(), uPmmaDate.updated_date, MainDashboard.USER_ID);
                        }
                    }
                }
            }
            else
            {
                uPmmaDate.year = year;
                uPmmaDate.month = month;
                uPmmaDate.date_start = startDate;
                uPmmaDate.date_end = endDate;
                uPmmaDate.updated_by = MainDashboard.USER_ID;
                uPmmaDate.updated_date = DateTime.Now;

                success = dalPmmaDate.Insert(uPmmaDate);

                if (!success)
                {
                    //Failed to insert data
                    
                    MessageBox.Show("Failed to add new date data");
                    tool.historyRecord(text.System, "Failed to add new date data (pmmaDateEdit)", uPmmaDate.updated_date, MainDashboard.USER_ID);
                }
                else
                {
                    tool.historyRecord("PMMA Date Add", "(" + year + "/" + month + ")" + "Start Date: "+ startDate.ToShortDateString() + ", End Date:" + endDate.ToShortDateString(), uPmmaDate.updated_date, MainDashboard.USER_ID);
                }
            }

            return success;
        }

        #endregion

        #region BUTTON CLICK

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to save these data?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dialogResult == DialogResult.Yes)
            {
                saveDate();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(Edited)
            {
                DialogResult dialogResult = MessageBox.Show("You will lose all unsaved work.\n\nAre you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
           
        }

        #endregion

        #region PROTECT UNSAVE DATA

        #region dtp value changed

        private void dtpJanStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

           
        }

        private void dtpJanEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpJanEnd.Value.AddDays(1);
            dtpFebStart.Value = NextStartDate;
        }

        private void dtpFebStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpFebEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
            DateTime NextStartDate = dtpFebEnd.Value.AddDays(1);
            dtpMarStart.Value = NextStartDate;
        }

        private void dtpMarStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;


        }

        private void dtpMarEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpMarEnd.Value.AddDays(1);
            dtpAprStart.Value = NextStartDate;
        }

        private void dtpAprStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpAprEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpAprEnd.Value.AddDays(1);
            dtpMayStart.Value = NextStartDate;
        }

        private void dtpMayStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpMayEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpMayEnd.Value.AddDays(1);
            dtpJunStart.Value = NextStartDate;
        }

        private void dtpJunStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJunEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpJunEnd.Value.AddDays(1);
            dtpJulStart.Value = NextStartDate;
        }

        private void dtpJulStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJulEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpJulEnd.Value.AddDays(1);
            dtpAugStart.Value = NextStartDate;
        }

        private void dtpAugStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpAugEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpAugEnd.Value.AddDays(1);
            dtpSepStart.Value = NextStartDate;
        }

        private void dtpSepStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpSepEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpSepEnd.Value.AddDays(1);
            dtpOctStart.Value = NextStartDate;
        }

        private void dtpOctStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpOctEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpOctEnd.Value.AddDays(1);
            dtpNovStart.Value = NextStartDate;
        }

        private void dtpNovStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpNovEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;

            DateTime NextStartDate = dtpNovEnd.Value.AddDays(1);
            dtpDecStart.Value = NextStartDate;
        }

        private void dtpDecStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpDecEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        #endregion

        private void frmPMMADateEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Edited)
            {
                DialogResult dialogResult = MessageBox.Show("You will lose all unsaved work.\n\nAre you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    Edited = false;
                    Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
        }

        private void dtpYear_ValueChanged(object sender, EventArgs e)
        {
            if (Edited)
            {
                DialogResult dialogResult = MessageBox.Show("You will lose all unsaved work.\n\nAre you sure you want to change the year?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    oldYear = dtpYear.Value.Year;
                    LoadDate();
                    
                }
                else
                {
                    dtpYear.Value = new DateTime(oldYear, 1, 1);
                }
            }   
            else
            {
                oldYear = dtpYear.Value.Year;
                LoadDate();
            }
        }

        #endregion

        private void frmPMMADateEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
