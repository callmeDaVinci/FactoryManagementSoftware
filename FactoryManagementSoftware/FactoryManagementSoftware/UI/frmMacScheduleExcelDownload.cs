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
    public partial class frmMacScheduleExcelDownload : Form
    {
        public frmMacScheduleExcelDownload()
        {
            InitializeComponent();
        }

        static public bool settingApplied = false;
        static public bool RequestingMachineSchedule = false;
        static public bool RequestingStocktake_Part = false;
        static public bool RequestingStocktake_RawMat = false;
        static public bool RequestingStocktake_ColorMat = false;

        static public DateTime DODate;

        static public bool allInOne = false;

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

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if(!StocktakeListFailedToProcess())
            {
                settingApplied = true;

                RequestingMachineSchedule = cbMacSchedule.Checked;
                RequestingStocktake_Part = cbSTTypePart.Checked;
                RequestingStocktake_RawMat = cbSTTypeRawMat.Checked;
                RequestingStocktake_ColorMat = cbSTTypeColorMat.Checked;

                Close();
            }
        }

        private void cbSTTypeRawMat_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSTTypeRawMat.Checked)
            {
                StocktakeListFailedToProcess();
            }
        }

        private void cbPrintFile_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbMacSchedule_CheckedChanged(object sender, EventArgs e)
        {
            cbStockTakeList.Checked = !cbMacSchedule.Checked;
        }

        private void cbSTTypePart_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSTTypePart.Checked)
            {
                StocktakeListFailedToProcess();
            }
          
        }

        private bool StocktakeListFailedToProcess()
        {
            bool Failed = !cbSTTypePart.Checked && !cbSTTypeRawMat.Checked && !cbSTTypeColorMat.Checked && cbStockTakeList.Checked;

            if (Failed)
            {
                MessageBox.Show("Please select at least one Item Type!");
            }

            return Failed;

        }

        private void cbSTTypeColorMat_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbSTTypeColorMat.Checked)
            {
                StocktakeListFailedToProcess();
            }
        }

        private void cbStockTakeList_CheckedChanged(object sender, EventArgs e)
        {
            cbMacSchedule.Checked = !cbStockTakeList.Checked;

            cbSTTypePart.Checked = cbStockTakeList.Checked;
            cbSTTypeRawMat.Checked = false;
            cbSTTypeColorMat.Checked = cbStockTakeList.Checked;

            cbSTTypePart.Enabled = cbStockTakeList.Checked;
            cbSTTypeRawMat.Enabled = cbStockTakeList.Checked;
            cbSTTypeColorMat.Enabled = cbStockTakeList.Checked;
        }
    }
}
