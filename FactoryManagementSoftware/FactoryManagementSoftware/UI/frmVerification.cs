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
    public partial class frmVerification : Form
    {
        public frmVerification()
        {
            InitializeComponent();
        }

        public frmVerification(string text)
        {
            InitializeComponent();
            PASSWORD = text;
        }

        private string PASSWORD = "";
        static public bool PASSWORD_MATCHED = false;

        private void btnSwitchToMatUsed_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text == PASSWORD)
            {
                lblWarning.Visible = false;
                PASSWORD_MATCHED = true;
                Close();
            }
            else
            {
                lblWarning.Visible = true;
                lblWarning.Text = "WRONG PASSWORD!";
            }
        }
    }
}
