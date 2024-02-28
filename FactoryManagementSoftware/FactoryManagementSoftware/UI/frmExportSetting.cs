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

        static public bool allInOne = false;

        static public bool newFormat = false;

        static public bool openFileAfterExport = false;
        static public bool printFileAfterExport = false;
        static public bool printPreview = false;

        private void frmExportSetting_Load(object sender, EventArgs e)
        {
            DOFormatUpdate();
        }

        private void btnCancelDOMode_Click(object sender, EventArgs e)
        {
            settingApplied = false;
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            DODate = dtpDODate.Value;

            newFormat = cbNewFormat.Checked;
            allInOne = cbSeparate.Checked;
            openFileAfterExport = cbOpenFile.Checked;
            printFileAfterExport = cbPrintFile.Checked;
            printPreview = cbPrintPreview.Checked;
            settingApplied = true;

            //if (cbSeparate.Checked)
            //{
            //    allInOne = false;
            //}
            //else
            //{
            //    allInOne = true;
            //}

            //if (cbOpenFile.Checked)
            //{
            //    openFileAfterExport = true;
            //}
            //else
            //{
            //    openFileAfterExport = false;
            //}

            //if (cbPrintFile.Checked)
            //{
            //    printFileAfterExport = true;
            //}
            //else
            //{
            //    printFileAfterExport = false;
            //}

            //if (cbPrintPreview.Checked)
            //{
            //    printPreview = true;
            //}
            //else
            //{
            //    printPreview = false;
            //}

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

        private void cbSeparate_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSeparate.Checked)
            {
                cbAllInOne.Checked = false;
            }
            else
            {
                cbAllInOne.Checked = true;
            }
        }

        private void cbAllInOne_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllInOne.Checked)
            {
                cbSeparate.Checked = false;
            }
            else
            {
                cbSeparate.Checked = true;
            }
        }

        private void DOFormatUpdate()
        {
            // Define the start date for comparison (1st December 2023)
            DateTime startDate = new DateTime(2023, 12, 1);

            // Check if the selected date is on or after the start date
            if (dtpDODate.Value.Date >= startDate)
            {
                cbNewFormat.Checked= true;
            }
            else
            {
                //cbOldFormat.Checked = true;
            }
        }

        private void dtpDODate_ValueChanged(object sender, EventArgs e)
        {
            DOFormatUpdate();
        }

        private void cbOldFormat_CheckedChanged(object sender, EventArgs e)
        {
            //if(cbOldFormat.Checked)
            //{
            //    cbNewFormat.Checked = false;
            //}
        }

        private void cbNewFormat_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbNewFormat.Checked)
            //{
            //    cbOldFormat.Checked = false;
            //}
        }
    }
}
