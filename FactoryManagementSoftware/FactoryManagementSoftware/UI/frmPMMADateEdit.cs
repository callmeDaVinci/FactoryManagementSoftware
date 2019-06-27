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

        private bool Edited = false;
        private bool yearEdited = false;
        private int oldYear = DateTime.Now.Year;
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();

        private void InitializeDate()
        {

            dtpYear.Format = DateTimePickerFormat.Custom;
            dtpYear.CustomFormat = "yyyy";
            dtpYear.ShowUpDown = true;// to prevent the calendar from being displayed

            DataTable dt = dalPmmaDate.Select();
            LoadDate(dt);
            Edited = false;
            //var dummyYear = 2019;
            //var dummyMonth = DateTime.Now.Month;
            //dtpDate.Value = new DateTime(dummyYear, dummyMonth, 1);
        }

        private void LoadDate()
        {
            int year = dtpYear.Value.Year;

            dtpJanStart.Value = new DateTime(year, 1, 1);
            dtpJanEnd.Value = new DateTime(year, 1, DateTime.DaysInMonth(year, 1));

            dtpFebStart.Value = new DateTime(year, 2, 1);
            dtpFebEnd.Value = new DateTime(year, 2, DateTime.DaysInMonth(year, 2));

            dtpMarStart.Value = new DateTime(year, 3, 1);
            dtpMarEnd.Value = new DateTime(year, 3, DateTime.DaysInMonth(year, 3));

            dtpAprStart.Value = new DateTime(year, 4, 1);
            dtpAprEnd.Value = new DateTime(year, 4, DateTime.DaysInMonth(year, 4));

            dtpMayStart.Value = new DateTime(year, 5, 1);
            dtpMayEnd.Value = new DateTime(year, 5, DateTime.DaysInMonth(year, 5));

            dtpJunStart.Value = new DateTime(year, 6, 1);
            dtpJunEnd.Value = new DateTime(year, 6, DateTime.DaysInMonth(year, 6));

            dtpJulStart.Value = new DateTime(year, 7, 1);
            dtpJulEnd.Value = new DateTime(year, 7, DateTime.DaysInMonth(year, 7));

            dtpAugStart.Value = new DateTime(year, 8, 1);
            dtpAugEnd.Value = new DateTime(year, 8, DateTime.DaysInMonth(year, 8));

            dtpSepStart.Value = new DateTime(year, 9, 1);
            dtpSepEnd.Value = new DateTime(year, 9, DateTime.DaysInMonth(year, 9));

            dtpOctStart.Value = new DateTime(year, 10, 1);
            dtpOctEnd.Value = new DateTime(year, 10, DateTime.DaysInMonth(year, 10));

            dtpNovStart.Value = new DateTime(year, 11, 1);
            dtpNovEnd.Value = new DateTime(year, 11, DateTime.DaysInMonth(year, 11));

            dtpDecStart.Value = new DateTime(year, 12, 1);
            dtpDecEnd.Value = new DateTime(year, 12, DateTime.DaysInMonth(year, 12));

            Edited = false;
        }

        private void LoadDate(DataTable dt)
        {
            int year = dtpYear.Value.Year;

            dtpJanStart.Value = new DateTime(year, 1, 1);
            dtpJanEnd.Value = new DateTime(year, 1, DateTime.DaysInMonth(year, 1));

            dtpFebStart.Value = new DateTime(year, 2, 1);
            dtpFebEnd.Value = new DateTime(year, 2, DateTime.DaysInMonth(year, 2));

            dtpMarStart.Value = new DateTime(year, 3, 1);
            dtpMarEnd.Value = new DateTime(year, 3, DateTime.DaysInMonth(year, 3));

            dtpAprStart.Value = new DateTime(year, 4, 1);
            dtpAprEnd.Value = new DateTime(year, 4, DateTime.DaysInMonth(year, 4));

            dtpMayStart.Value = new DateTime(year, 5, 1);
            dtpMayEnd.Value = new DateTime(year, 5, DateTime.DaysInMonth(year, 5));

            dtpJunStart.Value = new DateTime(year, 6, 1);
            dtpJunEnd.Value = new DateTime(year, 6, DateTime.DaysInMonth(year, 6));

            dtpJulStart.Value = new DateTime(year, 7, 1);
            dtpJulEnd.Value = new DateTime(year, 7, DateTime.DaysInMonth(year, 7));

            dtpAugStart.Value = new DateTime(year, 8, 1);
            dtpAugEnd.Value = new DateTime(year, 8, DateTime.DaysInMonth(year, 8));

            dtpSepStart.Value = new DateTime(year, 9, 1);
            dtpSepEnd.Value = new DateTime(year, 9, DateTime.DaysInMonth(year, 9));

            dtpOctStart.Value = new DateTime(year, 10, 1);
            dtpOctEnd.Value = new DateTime(year, 10, DateTime.DaysInMonth(year, 10));

            dtpNovStart.Value = new DateTime(year, 11, 1);
            dtpNovEnd.Value = new DateTime(year, 11, DateTime.DaysInMonth(year, 11));

            dtpDecStart.Value = new DateTime(year, 12, 1);
            dtpDecEnd.Value = new DateTime(year, 12, DateTime.DaysInMonth(year, 12));


        }

        private DateTime GetStartDate(int month,int year,DataTable dt)
        {
            DateTime date = new DateTime(year, month, 1);

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if(row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateStart]);
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

        private void frmPMMADateEdit_Load(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
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

        #region dtp value changed

        private void dtpJanStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJanEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpFebStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpFebEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpMarStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpMarEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpAprStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpAprEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpMayStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpMayEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJunStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJunEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJulStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpJulEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpAugStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpAugEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpSepStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpSepEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpOctStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpOctEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpNovStart_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
        }

        private void dtpNovEnd_ValueChanged(object sender, EventArgs e)
        {
            Edited = true;
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
    }
}
