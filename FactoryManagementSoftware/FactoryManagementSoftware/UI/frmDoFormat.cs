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
using System.Configuration;
using Syncfusion.XlsIO;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDOFormat : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        itemDAL dalItem = new itemDAL();
        doFormatDAL dalDoFormat = new doFormatDAL();
        companyDAL dalCompany = new companyDAL();
        addressBookDAL dalAddressBook = new addressBookDAL();
        doInternalDAL dalInternalDO = new doInternalDAL();
        doInternalItemDAL dalInternalDOItem = new doInternalItemDAL();

        AddressBookBLL uBillingAddress = new AddressBookBLL();
        AddressBookBLL uShippingAddress = new AddressBookBLL();
        AddressBookBLL uLetterHeadAddress = new AddressBookBLL();
        internalDOBLL uInternalDO = new internalDOBLL();
        internalDOItemBLL uInternalDOItem = new internalDOItemBLL();

        private string ITEM_CODE = "ITEM CODE";
        private string ITEM_CATEGORY = "CATEGORY";
        bool DO_ITEM_EDIT_MODE = false;
        bool DO_EDITING_MODE = false;
        private int EDITING_INDEX = -1;
        private string BUTTON_ADDTOLIST_TEXT = "Add to List";
        private string BUTTON_UPDATETOLIST_TEXT = "Update to List";

        private bool DATA_SAVED = false;
        public frmDOFormat()
        {
            InitializeComponent();
            InitialSetting();
            DO_ITEM_EDIT_MODE = false;
            DO_EDITING_MODE = false;
        }


        #endregion

        #region UI/UX
        private void InitialSetting()
        {
            
        }

        #region Appearance

        DataTable DT_ITEM_LIST;

        private DataTable NewItemList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_TableCode, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_Qty, typeof(decimal));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            dt.Columns.Add(text.Header_SearchMode, typeof(bool));
            dt.Columns.Add(text.Header_ItemName, typeof(string));

            dt.Columns.Add(text.Header_TotalQty, typeof(string));
            dt.Columns.Add(text.Header_TotalQtyUnit, typeof(string));
            dt.Columns.Add(text.Header_QtyPerBox, typeof(string));
            dt.Columns.Add(text.Header_BoxQty, typeof(string));
            dt.Columns.Add(text.Header_BoxUnit, typeof(string));
            dt.Columns.Add(text.Header_Balance, typeof(decimal));

            dt.Columns.Add(text.Header_Remark, typeof(string));

            dt.Columns.Add(text.Header_DescriptionIncludeCategory, typeof(bool));
            dt.Columns.Add(text.Header_DescriptionIncludePackaging, typeof(bool));
            dt.Columns.Add(text.Header_DescriptionIncludeRemark, typeof(bool));
            dt.Columns.Add(text.Header_ToRemove, typeof(bool));

            return dt;
        }


        private void functiontoTestGibhub()
        {
            DataTable dt = new DataTable();

            var testing123 = 13;

        }

        private void dgvUIEdit(DataGridView dgv)
        {
            var test = 0;

            //if (dgv == dgvDOItemList)
            //{
            //    dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            //    dgv.Columns[text.Header_ItemCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_ItemCode].MinimumWidth = 100;


            //    dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_Description].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_Description].MinimumWidth = 100;
            //    dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //    dgv.Columns[text.Header_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //    dgv.Columns[text.Header_TableCode].Visible = false;
            //    dgv.Columns[text.Header_SearchMode].Visible = false;
            //    dgv.Columns[text.Header_ItemName].Visible = false;
            //    dgv.Columns[text.Header_TotalQty].Visible = false;
            //    dgv.Columns[text.Header_TotalQtyUnit].Visible = false;
            //    dgv.Columns[text.Header_QtyPerBox].Visible = false;
            //    dgv.Columns[text.Header_BoxQty].Visible = false;
            //    dgv.Columns[text.Header_BoxUnit].Visible = false;
            //    dgv.Columns[text.Header_Balance].Visible = false;
            //    dgv.Columns[text.Header_Remark].Visible = false;
            //    dgv.Columns[text.Header_Balance].Visible = false;
            //    dgv.Columns[text.Header_DescriptionIncludeCategory].Visible = false;
            //    dgv.Columns[text.Header_DescriptionIncludePackaging].Visible = false;
            //    dgv.Columns[text.Header_DescriptionIncludeRemark].Visible = false;
            //    dgv.Columns[text.Header_ToRemove].Visible = false;

            //}
            //else if (dgv == dgvPreviewItemList)
            //{
            //    dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            //    dgv.Columns[text.Header_ItemCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_ItemCode].MinimumWidth = 100;


            //    dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_Description].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_Description].MinimumWidth = 100;
            //    dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //    dgv.Columns[text.Header_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //    dgv.Columns[text.Header_TableCode].Visible = false;
            //    dgv.Columns[text.Header_SearchMode].Visible = false;
            //    dgv.Columns[text.Header_ItemName].Visible = false;
            //    dgv.Columns[text.Header_TotalQty].Visible = false;
            //    dgv.Columns[text.Header_TotalQtyUnit].Visible = false;
            //    dgv.Columns[text.Header_QtyPerBox].Visible = false;
            //    dgv.Columns[text.Header_BoxQty].Visible = false;
            //    dgv.Columns[text.Header_BoxUnit].Visible = false;
            //    dgv.Columns[text.Header_Balance].Visible = false;
            //    dgv.Columns[text.Header_Remark].Visible = false;
            //    dgv.Columns[text.Header_Balance].Visible = false;
            //    dgv.Columns[text.Header_DescriptionIncludeCategory].Visible = false;
            //    dgv.Columns[text.Header_DescriptionIncludePackaging].Visible = false;
            //    dgv.Columns[text.Header_DescriptionIncludeRemark].Visible = false;
            //    dgv.Columns[text.Header_ToRemove].Visible = false;

            //}
        }

        #endregion




        #endregion


        #region Event Handlers

      
        private void btnJobPublish_Click(object sender, EventArgs e)
        {
            
        }


        private void frmDOEditing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved D/O data. Leave without saving? ", "Message",
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

     


        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
     

      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved D/O data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
               

            }
        }


        private void frmDOEditing_Load(object sender, EventArgs e)
        {
            
           
        }



        private void tableLayoutPanel28_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }


     

        private void txtNextRunningNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnSaveRunningNumber_Click(object sender, EventArgs e)
        {
          
        }


        private void frmDOEditing_Shown(object sender, EventArgs e)
        {
            

        }

    }
}
