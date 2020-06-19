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
    public partial class frmSPP : Form
    {
        public frmSPP()
        {
            InitializeComponent();
        }

        private void frmSPP_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.SPPFormOpen = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSPPInventory frm = new frmSPPInventory
            {
                StartPosition = FormStartPosition.CenterScreen
            };

           
            frm.ShowDialog();
            
        }

        private void frmSPP_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmSPPDataSetting frm = new frmSPPDataSetting
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Control ctrl = (Control)sender;
            //ctrl.BackColor = Color.Yellow;
            frmSPPCalculation frm = new frmSPPCalculation
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmSPPPOList frm = new frmSPPPOList
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmSPPNewPO frm = new frmSPPNewPO
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmSPPDOList frm = new frmSPPDOList
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void WorkInProgressMessage()
        {
            MessageBox.Show("work in progress...");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }
    }
}
