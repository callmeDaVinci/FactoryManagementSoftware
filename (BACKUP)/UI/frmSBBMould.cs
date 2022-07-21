using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;


namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBMould : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        public frmSBBMould()
        {
            InitializeComponent();
        }

        private void frmSBBMould_Load(object sender, EventArgs e)
        {

        }

        private void frmSBBMould_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MouldFormOpen = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmSBBMouldEdit frm = new frmSBBMouldEdit(true)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSBBMouldEdit frm = new frmSBBMouldEdit(false)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

        }
    }
}
