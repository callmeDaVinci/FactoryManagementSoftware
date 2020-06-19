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
        }

        public frmDeliveryDate(DataTable dt)
        {
            InitializeComponent();

            dt_Delivered = dt;
        }

        private DataTable dt_Delivered;
        static public bool transferred = false;


        private void btnCancel_Click(object sender, EventArgs e)
        {
            transferred = false;
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            frmInOutEdit frm = new frmInOutEdit(dt_Delivered, dtpDate.Value.Date);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(frmInOutEdit.TrfSuccess)
            {
                transferred = true;
            }

            Close();
        }
    }
}
