using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmTimeNeededSetting : Form
    {
        public frmTimeNeededSetting()
        {
            InitializeComponent();
        }

        public frmTimeNeededSetting(string _pcsPerManHour, string _manpower, string _hoursPerDay)
        {
            InitializeComponent();

            pcsPerManHour = _pcsPerManHour;
            manpower = _manpower;
            hoursPerDay = _hoursPerDay;
        }

        static public string pcsPerManHour = "";
        static public string manpower = "";
        static public string hoursPerDay = "";
        static public bool dataSaved = false;

        private void LoadData()
        {
            txtPcsPerManHour.Text = pcsPerManHour;
            txtManpower.Text = manpower;
            txtHoursPerDay.Text = hoursPerDay;
        }

        private bool IfDataChanged()
        {
            bool dataChanged = false;

            if(txtPcsPerManHour.Text != pcsPerManHour)
            {
                dataChanged = true;
            }

            if (txtManpower.Text != manpower)
            {
                dataChanged = true;
            }

            if (txtHoursPerDay.Text != hoursPerDay)
            {
                dataChanged = true;
            }

            return dataChanged;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (IfDataChanged())
            {
                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    dataSaved = false;
                    Close();
                }
                
            }
            else
            {
                Close();
            }
        }

        private void frmTimeNeededSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IfDataChanged())
            {
                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
          
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            

            if(IfDataChanged())
            {
                pcsPerManHour = txtPcsPerManHour.Text;
                manpower = txtManpower.Text;
                hoursPerDay = txtHoursPerDay.Text;

                dataSaved = true;
            }

            MessageBox.Show("Data saved!");

            Close();
        }

        private void frmTimeNeededSetting_Shown(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
