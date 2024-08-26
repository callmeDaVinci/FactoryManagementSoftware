using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Linq;
using System.Threading;
using Syncfusion.XlsIO.Parser.Biff_Records;
using Guna.UI.WinForms;
using Accord.Statistics.Distributions.Univariate;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSbbStdPackingSettings : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
     

        public frmSbbStdPackingSettings()
        {
            InitializeComponent();
            InitialSetting();
        }

   
        private void InitialSetting()
        {

          
        }

        private bool DATA_SAVED = false;

        private void frmStockCountListEditing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        #endregion

      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
                

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
           
            
        }

        private void txtUnitConversionRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && txtPcsPerBag.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
     
        private void txtCountUnit_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtUnitConversionRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSystemUnit_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
