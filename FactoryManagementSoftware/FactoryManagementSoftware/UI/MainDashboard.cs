using System;
using System.Drawing;
using System.Linq;
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
        static public bool ordFormOpen = false;
        static public bool dataFormOpen = false;
        static public bool itemCustFormOpen = false;
        static public bool forecastInputFormOpen = false;
        static public bool forecastReportInputFormOpen = false;
        static public bool joinFormOpen = false;
        static public bool MaterialUsedReportFormOpen = false;
        static public bool stockReportFormOpen = false;

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
                item.WindowState = FormWindowState.Maximized;
                item.Show();
                itemFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmItem>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmItem>().First().BringToFront();
                }
            }
        }

        private void facToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!facFormOpen)
            {
                frmFac fac = new frmFac();
                fac.MdiParent = this;
                fac.StartPosition = FormStartPosition.CenterScreen;
                fac.WindowState = FormWindowState.Maximized;
                fac.Show();

                facFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmFac>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmFac>().First().BringToFront();
                }
            }
        }

        private void custToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!custFormOpen)
            {
                frmCust item = new frmCust();
                item.MdiParent = this;
                item.StartPosition = FormStartPosition.CenterScreen;
                item.WindowState = FormWindowState.Maximized;
                item.Show();
                custFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmCust>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmCust>().First().BringToFront();
                }
            }
        }

        private void inOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!inOutFormOpen)
            {
                frmInOut inOut = new frmInOut();
                inOut.MdiParent = this;
                inOut.StartPosition = FormStartPosition.CenterScreen;
                inOut.WindowState = FormWindowState.Maximized;
                inOut.Show();
                inOutFormOpen = true; 
            }
            else
            {
                if (Application.OpenForms.OfType<frmInOut>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmInOut>().First().BringToFront();
                }
            }
        }


        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!catFormOpen)
            {
                frmCat cat = new frmCat();
                cat.MdiParent = this;
                cat.StartPosition = FormStartPosition.CenterScreen;
                cat.WindowState = FormWindowState.Maximized;
                cat.Show();
                catFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmCat>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmCat>().First().BringToFront();
                }
            }
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dataFormOpen)
            {
                frmData data = new frmData();
                data.MdiParent = this;
                data.StartPosition = FormStartPosition.CenterScreen;
                data.WindowState = FormWindowState.Maximized;
                data.Show();
                dataFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmData>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmData>().First().BringToFront();
                }
            }
        }

        private void itemCustToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!itemCustFormOpen)
            {
                frmItemCust frm = new frmItemCust();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                itemCustFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmItemCust>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmItemCust>().First().BringToFront();
                }
            }
        }

        private void dataInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!forecastInputFormOpen)
            {
                frmForecast frm = new frmForecast();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                forecastInputFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmForecast>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmForecast>().First().BringToFront();
                }
            }
        }

        private void forecastReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!forecastReportInputFormOpen)
            {
                frmForecastReport frm = new frmForecastReport();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                forecastReportInputFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmForecastReport>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmForecastReport>().First().BringToFront();
                }
            }

        }

        private void itemJoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!joinFormOpen)
            {
                frmJoin frm = new frmJoin();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                joinFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmJoin>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmJoin>().First().BringToFront();
                }
            }
        }

        private void MainDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void orderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!ordFormOpen)
            {
                frmOrder ord = new frmOrder();
                ord.MdiParent = this;
                ord.StartPosition = FormStartPosition.CenterScreen;
                ord.WindowState = FormWindowState.Maximized;
                ord.Show();
                ordFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrder>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrder>().First().BringToFront();
                }
            }
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!inOutFormOpen)
            {
                frmInOut inOut = new frmInOut();
                inOut.MdiParent = this;
                inOut.StartPosition = FormStartPosition.CenterScreen;
                inOut.WindowState = FormWindowState.Maximized;
                inOut.Show();
                inOutFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmInOut>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmInOut>().First().BringToFront();
                }
            }
        }

        private void materialUsedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MaterialUsedReportFormOpen)
            {
                frmMaterialUsedReport frm = new frmMaterialUsedReport();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                MaterialUsedReportFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmMaterialUsedReport>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmMaterialUsedReport>().First().BringToFront();
                }
            }
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!stockReportFormOpen)
            {
                frmStockReport frm = new frmStockReport();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                stockReportFormOpen = true;
            }
            else
            {
                if (Application.OpenForms.OfType<frmStockReport>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmStockReport>().First().BringToFront();
                }
            }
        }
    }
}
