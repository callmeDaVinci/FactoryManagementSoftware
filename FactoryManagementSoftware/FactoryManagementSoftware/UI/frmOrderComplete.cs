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
    public partial class frmOrderComplete : Form
    {
        public frmOrderComplete()
        {
            frmOrder.orderCompleted = false;

            InitializeComponent();
            orderCompleted = false;
        }

        static public bool orderCompleted = false;
        static public string NOTE = "";
        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrder.note = txtNote.Text;
            frmOrderAlert_NEW.note = txtNote.Text;

            frmOrder.orderCompleted = true;
            frmOrderAlert_NEW.orderCompleted = true;

            NOTE = txtNote.Text;

            orderCompleted = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmOrder.orderCompleted = true;

            Close();
        }
    }
}
