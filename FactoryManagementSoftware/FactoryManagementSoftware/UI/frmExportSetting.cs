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
    public partial class frmExportSetting : Form
    {
        public frmExportSetting()
        {
            InitializeComponent();
        }

        static public bool settingApplied = false;
        static public DateTime DODate;
        static public bool openFileAfterExport = false;
        static public bool printFileAfterExport = false;
        static public bool printPreview = false;

        private void frmExportSetting_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelDOMode_Click(object sender, EventArgs e)
        {
            settingApplied = false;
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            DODate = dtpDODate.Value;
            
            if(cbOpenFile.Checked)
            {
                openFileAfterExport = true;
            }
            else
            {
                openFileAfterExport = false;
            }

            if (cbPrintFile.Checked)
            {
                printFileAfterExport = true;
            }
            else
            {
                printFileAfterExport = false;
            }

            if (cbPrintPreview.Checked)
            {
                printPreview = true;
            }
            else
            {
                printPreview = false;
            }

            settingApplied = true;
            Close();
        }

        private void cbPrintPreview_CheckedChanged(object sender, EventArgs e)
        {
            if(cbPrintPreview.Checked)
            {
                cbPrintFile.Checked = true;
            }
        }

        private void cbPrintFile_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrintFile.Checked)
            {
                cbPrintPreview.Checked = true;
            }
            else
            {
                cbPrintPreview.Checked = false;
            }
        }
    }
}
