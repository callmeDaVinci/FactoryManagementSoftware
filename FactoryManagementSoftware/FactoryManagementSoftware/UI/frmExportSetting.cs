using FactoryManagementSoftware.Module;
using Syncfusion.XlsIO;
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
            PDF_TYPE = true;
            EXCEL_TYPE= false;
        }

        static public bool settingApplied = false;
        static public DateTime DODate;

        static public bool allInOne = false;

        static public bool newFormat = true;

        static public bool openFileAfterExport = false;
        static public bool printFileAfterExport = false;
        static public bool printPreview = false;
        static public bool EXCEL_TYPE = false;
        static public bool PDF_TYPE = true;
        Text text = new Text();

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

            EXCEL_TYPE = cbExcel.Checked;
            PDF_TYPE = !cbExcel.Checked;
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
                //cbExcel.Checked= true;
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

        private bool FORMAT_TYPE_UPDATING = false;

        private void cbExcel_CheckedChanged(object sender, EventArgs e)
        {
            if(!FORMAT_TYPE_UPDATING)
            {
                FORMAT_TYPE_UPDATING = true;

                if (cbExcel.Checked)
                {
                    //password
                    frmVerification frm = new frmVerification(text.PW_Level_1)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };


                    frm.ShowDialog();

                    if (!frmVerification.PASSWORD_MATCHED)
                    {
                        cbExcel.Checked = false;
                    }
                }

                cbPDF.Checked = !cbExcel.Checked;

                cbSeparate.Checked = cbExcel.Checked;
                cbAllInOne.Checked = false;

                EXCEL_TYPE = cbExcel.Checked;
                PDF_TYPE= !cbExcel.Checked;

                FORMAT_TYPE_UPDATING = false;

                cbSeparate.Visible= cbExcel.Checked;
                cbAllInOne.Visible= cbExcel.Checked;
            }
        }

        private void cbPDF_CheckedChanged(object sender, EventArgs e)
        {
            if (!FORMAT_TYPE_UPDATING)
            {
                FORMAT_TYPE_UPDATING = true;
                cbExcel.Checked = !cbPDF.Checked;

                if (cbExcel.Checked)
                {
                    //password
                    frmVerification frm = new frmVerification(text.PW_Level_1)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };


                    frm.ShowDialog();

                    if (!frmVerification.PASSWORD_MATCHED)
                    {
                        cbExcel.Checked = false;
                        cbPDF.Checked = true;
                    }
                }


                EXCEL_TYPE = cbExcel.Checked;
                PDF_TYPE = !cbExcel.Checked;

                cbSeparate.Checked = cbExcel.Checked;
                cbAllInOne.Checked = false;

                cbSeparate.Visible = cbExcel.Checked;
                cbAllInOne.Visible = cbExcel.Checked;

                FORMAT_TYPE_UPDATING = false;
            }
        }
    }
}
