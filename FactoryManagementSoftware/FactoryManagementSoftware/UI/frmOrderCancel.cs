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
    public partial class frmOrderCancel : Form
    {
        public frmOrderCancel()
        {
            InitializeComponent();
            orderCancelled = false;
            CancelledReason = "";
        }

        static public bool orderCancelled = false;
        static public string  CancelledReason = "";

        private void btnReset_Click(object sender, EventArgs e)
        {
            frmOrder.cancel = false;
            Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrder.note = txtNote.Text;
            frmOrder.cancel = true;

            frmOrderAlert_NEW.note = txtNote.Text;
            frmOrderAlert_NEW.cancel = true;

            orderCancelled = true;
            Close();
        }
    }
}
