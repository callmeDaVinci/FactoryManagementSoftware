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
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrder.note = txtNote.Text;
            frmOrder.orderCompleted = true;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmOrder.orderCompleted = true;

            Close();
        }
    }
}
