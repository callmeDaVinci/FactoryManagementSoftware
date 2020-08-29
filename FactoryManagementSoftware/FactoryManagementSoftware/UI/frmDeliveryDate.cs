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
    public partial class frmDeliveryDate : Form
    {
        public frmDeliveryDate()
        {
            InitializeComponent();
            dateEditOnly = true;
        }

        public frmDeliveryDate(DataTable dt)
        {
            InitializeComponent();

            dt_Delivered = dt;
        }

      
        public frmDeliveryDate(string DateType)
        {

            InitializeComponent();
            lblDateType.Text = DateType;
            dateEditOnly = true;
        }

        private DataTable dt_Delivered;
        static public bool transferred = false;
        private bool dateEditOnly = false;
        private bool dateClear = false;

        static public DateTime selectedDate = DateTime.MaxValue;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            transferred = false;
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            if(dateEditOnly)
            {
                selectedDate = dtpDate.Value;

                if(dateClear)
                {
                    selectedDate = DateTimePicker.MinimumDateTime;
                }
            }
            else
            {
                frmInOutEdit frm = new frmInOutEdit(dt_Delivered, dtpDate.Value.Date);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit

                if (frmInOutEdit.TrfSuccess)
                {
                    transferred = true;
                    
                }
            }

            Close();
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            dtpDate.Value = DateTimePicker.MinimumDateTime;
            dateClear = true;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDate.Value == DateTimePicker.MinimumDateTime)
            {
                dtpDate.Value = DateTime.Now; // This is required in order to show current month/year when user reopens the date popup.
                dtpDate.Format = DateTimePickerFormat.Custom;
                dtpDate.CustomFormat = " ";
            }
            else
            {
                dtpDate.Format = DateTimePickerFormat.Custom;
                dtpDate.CustomFormat = "ddMMMMyy";
                dateClear = false;
            }
        }
    }
}
