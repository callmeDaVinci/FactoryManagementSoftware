using System;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class MainDashboard : Form
    {
        static public bool itemFormOpen = false;
        static public bool facFormOpen = false;
        static public bool custFormOpen = false;
        static public bool inOutFormOpen = false;
        static public bool catFormOpen = false;

        public MainDashboard()
        {
            InitializeComponent();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!itemFormOpen)
            {
                frmItem item = new frmItem();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                //item.WindowState = FormWindowState.Maximized;
                item.Show();
                itemFormOpen = true;
            }
        }

        private void facToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!facFormOpen)
            {
                frmFac fac = new frmFac();
                fac.MdiParent = this;
                fac.StartPosition = FormStartPosition.CenterScreen;
                //item.WindowState = FormWindowState.Maximized;
                fac.Show();

                facFormOpen = true;
            }
        }

        private void custToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!custFormOpen)
            {
                frmCust item = new frmCust();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                //item.WindowState = FormWindowState.Maximized;
                item.Show();
                custFormOpen = true;
            }
        }

        private void inOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!inOutFormOpen)
            {
                frmInOut inOut = new frmInOut();
                inOut.MdiParent = this;
                inOut.StartPosition = FormStartPosition.CenterScreen;
                //item.WindowState = FormWindowState.Maximized;
                inOut.Show();
                inOutFormOpen = true; 
            }
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!catFormOpen)
            {
                frmCat cat = new frmCat();
                cat.MdiParent = this;
                cat.StartPosition = FormStartPosition.CenterScreen;
                cat.Show();
                catFormOpen = true;
            }
        }
    }
}
