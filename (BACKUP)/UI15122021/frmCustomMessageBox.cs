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
    public partial class frmCustomMessageBox : Form
    {
        static public bool openNewPlan = false;
        public frmCustomMessageBox()
        {
            InitializeComponent();
        }

        public frmCustomMessageBox(string message, string btnTop, string btnBtm)
        {
            InitializeComponent();

            lblMessage.Text = message;
            btnContinuePlanning.Text = btnTop;
            btnBackToSchedule.Text = btnBtm;
        }

        private void frmCustomMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void btnContinuePlanning_Click(object sender, EventArgs e)
        {
            openNewPlan = true;
            Close();
        }

        private void btnBackToSchedule_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
