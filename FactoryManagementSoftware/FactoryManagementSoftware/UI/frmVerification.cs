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
            PASSWORD_MATCHED = false;

        }

        public frmVerification(string text)
        {
            InitializeComponent();
            PASSWORD = text;
            PASSWORD_MATCHED = false;
        }

        private string PASSWORD = "";
        static public bool PASSWORD_MATCHED = false;

        private void btnSwitchToMatUsed_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            PASSWORD_MATCHED = false;
            if (txtPassword.Text == PASSWORD)
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

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCheck_Click(sender, e);
            }
        }
    }
}
