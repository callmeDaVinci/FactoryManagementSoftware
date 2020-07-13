using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmShowDataTable : Form
    {
        public frmShowDataTable()
        {
            InitializeComponent();
        }

        public frmShowDataTable(DataTable dt)
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            dgvList.DataSource = dt;
           
        }

        Tool tool = new Tool();
        private void frmShowDataTable_Load(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }
    }
}
