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
using Org.BouncyCastle.Asn1.Pkcs;

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

        static public bool DATA_UPDATED = false;

        private string ITEM_CODE;
        private string ITEM_NAME;
        SBBDataDAL dalData = new SBBDataDAL();
        SBBDataBLL uData = new SBBDataBLL();

        public frmSbbStdPackingSettings(string itemName, string itemCode)
        {
            InitializeComponent();

            ITEM_CODE = itemCode;
            ITEM_NAME = itemName;

            InitialSetting(ITEM_NAME,ITEM_CODE);
        }

        private void InitialSetting()
        {

          
        }

        private int TABLE_CODE = 0;

        private void InitialSetting(string itemName, string itemCode)
        {
            DATA_UPDATED = false;
            txtItemName.Text = itemName;
            txtItemCode.Text = itemCode;

            txtItemName.Enabled = false;
            txtItemCode.Enabled = false;
            
            //int qtyPerBag = tool.GetQtyPerBag(dt, itemCode);
            var stdPacking = tool.GetItemStdPacking(dalData.StdPackingSelect(), itemCode);

            txtPcsPerBag.Text = stdPacking.Item1.ToString();
            txtPcsPerPacket.Text = stdPacking.Item2.ToString();
            txtPcsPerContainer.Text = stdPacking.Item3.ToString();
            TABLE_CODE = stdPacking.Item4;

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
                    DATA_SAVED = true;
                    DATA_UPDATED = false;
                    Close();
                }


            }
        }

        private bool StdPackingInsert()
        {
            uData.Updated_Date = DateTime.Now;
            uData.Updated_By = MainDashboard.USER_ID;

            uData.Max_Lvl = int.TryParse(txtPcsPerBag.Text, out int data) ? data : 0;
            uData.Qty_Per_Packet = int.TryParse(txtPcsPerPacket.Text, out data) ? data : 0;
            uData.Qty_Per_Bag = int.TryParse(txtPcsPerBag.Text, out data) ? data : 0;
            uData.Qty_Per_Container = int.TryParse(txtPcsPerContainer.Text, out data) ? data : 0;
            uData.Item_code = ITEM_CODE;
           
            return dalData.InsertStdPacking(uData);
        }


        private bool StdPackingUpdate()
        {
            uData.Table_Code = TABLE_CODE;
            uData.Updated_Date = DateTime.Now;
            uData.Updated_By = MainDashboard.USER_ID;

            uData.Max_Lvl = int.TryParse(txtPcsPerBag.Text, out int data) ? data : 0;
            uData.Qty_Per_Packet = int.TryParse(txtPcsPerPacket.Text, out data) ? data : 0;
            uData.Qty_Per_Bag = int.TryParse(txtPcsPerBag.Text, out data) ? data : 0;
            uData.Qty_Per_Container = int.TryParse(txtPcsPerContainer.Text, out data) ? data : 0;
            uData.Item_code = ITEM_CODE;



            return dalData.StdPackingUpdate(uData);
        }

        private bool Validation()
        {
            bool Result = true;


            return Result;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool result = false;

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Confirm to update?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    if (TABLE_CODE <= 0)
                    {
                        //check again DB table , if same ItemCode found ,then process to update, else go to insert
                        var stdPacking = tool.GetItemStdPacking(dalData.StdPackingSelect(), ITEM_CODE);
                        TABLE_CODE = stdPacking.Item4;

                        if (TABLE_CODE <= 0)
                        {
                            result = StdPackingInsert();

                        }
                        else
                        {
                            result = StdPackingUpdate();
                        }

                    }
                    else
                    {
                        result = StdPackingUpdate();

                    }
                    if (!result)
                    {
                        MessageBox.Show("Failed to update standard packing data!");
                    }
                    else
                    {
                        DATA_SAVED = true;
                        DATA_UPDATED = true;

                        Close();

                    }
                }
            }
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
