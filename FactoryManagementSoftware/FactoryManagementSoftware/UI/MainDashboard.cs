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
    public partial class MainDashboard : Form
    {
        static public bool itemFormOpen = false;
        static public bool facFormOpen = false;

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

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!facFormOpen)
            {
                frmFac item = new frmFac();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                //item.WindowState = FormWindowState.Maximized;
                item.Show();
                facFormOpen = true;
            }
        }
    }
}
