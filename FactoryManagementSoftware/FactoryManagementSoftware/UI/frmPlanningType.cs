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
    public partial class frmPlanningType : Form
    {
        public frmPlanningType()
        {
            InitializeComponent();
        }

        private void frmPlanningType_Load(object sender, EventArgs e)
        {

        }

        private void btnMoldingMachine_Click(object sender, EventArgs e)
        {
            frmSBBProductionPlanning frm = new frmSBBProductionPlanning
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            Close();

        }

        private void btnAssemblyLine_Click(object sender, EventArgs e)
        {
            frmSBBAssemblyPlanning frm = new frmSBBAssemblyPlanning
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            Close();
        }
    }
}
