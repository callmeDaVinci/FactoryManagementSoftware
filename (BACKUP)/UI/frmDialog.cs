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
    public partial class frmDialog : Form
    {
        static public bool InsType = false;

        public frmDialog()
        {
            InitializeComponent();
        }

        private void btnIns_Click(object sender, EventArgs e)
        {
            InsType = true;
            Close();
        }

        private void btnTube_Click(object sender, EventArgs e)
        {
            InsType = false;
            Close();
        }
    }
}
