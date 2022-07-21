using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;


namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBMouldEdit : Form
    {
        #region DATA SETTING

        Tool tool = new Tool();
        Text text = new Text();

        bool MOULD_EDITMODE = false;
        bool PAGE_LOADED = false;
        SBBDataBLL uData = new SBBDataBLL();
        SBBDataDAL dalSBB = new SBBDataDAL();

        private string MOULD_EDIT_MODE_ADD = "MOULD DATA (ADD MODE)";
        private string MOULD_EDIT_MODE_UPDATE = "MOULD DATA (EDIT MODE)";
        private string MOULD_BUTTON_ADD = "ADD";
        private string MOULD_BUTTON_EDIT = "UPDATE";

        private DataTable DT_MOULD_DATA;
        #endregion

        public frmSBBMouldEdit()
        {
            InitializeComponent();
            LoadMouldNameToCMB();
            ShowOrHideItemGroupEditUI(false);
        }

        public frmSBBMouldEdit(bool EditMode)
        {
            InitializeComponent();

            MOULD_EDITMODE = EditMode;

            EditModeUIChange(EditMode);
            ShowOrHideItemGroupEditUI(false);

            DT_MOULD_DATA = dalSBB.MouldWithoudRemovedDataSelect();
            LoadMouldNameToCMB();
        }

        private void LoadMouldNameToCMB()
        {
            cmbMouldCode.DataSource = null;

            if(DT_MOULD_DATA != null && DT_MOULD_DATA.Rows.Count > 0)
            {
                DataTable MouldNameData = DT_MOULD_DATA.DefaultView.ToTable(true, dalSBB.MouldName);
                MouldNameData.DefaultView.Sort = dalSBB.MouldName + " ASC";

                MouldNameData.AcceptChanges();

                cmbMouldName.DataSource = MouldNameData;
                cmbMouldName.DisplayMember = dalSBB.MouldName;
                cmbMouldName.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Mould Data not Found!");
            }
        }

        private void LoadMouldCodeToCMB()
        {
            cmbMouldCode.DataSource = null;

            if (DT_MOULD_DATA != null && DT_MOULD_DATA.Rows.Count > 0)
            {
                DataTable dt = dalSBB.MouldKeyWordsSelect(cmbMouldName.Text);

                DataTable MouldCodeData = dt.DefaultView.ToTable(true, dalSBB.MouldCode);
                MouldCodeData.DefaultView.Sort = dalSBB.MouldCode + " ASC";

                MouldCodeData.AcceptChanges();

                cmbMouldCode.DataSource = MouldCodeData;
                cmbMouldCode.DisplayMember = dalSBB.MouldCode;
                //cmbMouldCode.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Mould Data not Found!");
            }
        }

        private void EditModeUIChange(bool EditMode)
        {
            if(EditMode)
            {
                cmbMouldName.Visible = true;
                cmbMouldCode.Visible = true;
                txtMouldName.Visible = false;
                txtMouldCode.Visible = false;

                btnEditMould.Text = MOULD_BUTTON_EDIT;

                lblMouldEditMode.Text = MOULD_EDIT_MODE_UPDATE;
                tlpMouldNameAndCode.RowStyles[0] = new RowStyle(SizeType.Absolute, 30f);
                tlpMouldNameAndCode.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);
            }
            else
            {

                cmbMouldName.Visible = false;
                cmbMouldCode.Visible = false;
                txtMouldName.Visible = true;
                txtMouldCode.Visible = true;

                lblMouldEditMode.Text = MOULD_EDIT_MODE_ADD;
                btnEditMould.Text = MOULD_BUTTON_ADD;

                tlpMouldNameAndCode.RowStyles[1] = new RowStyle(SizeType.Absolute, 30f);
                tlpMouldNameAndCode.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
                
            }
        }


        private void NotEditable_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void OnlyNumericAndDot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void ResetItemGroupEditField()
        {
            txtGroupCode.Clear();
            cmbItemName.SelectedIndex = -1;
            cmbItemCode.SelectedIndex = -1;
            txtItemCavity.Clear();
            txtGroupCT.Clear();
            txtGroupPW.Clear();
            txtGroupRW.Clear();
        }

        private void ShowOrHideItemGroupEditUI(bool show)
        {
            ResetItemGroupEditField();

            if (show)
            {
                //button
                btnAddItem.Visible = false;
                btnEditItem.Visible = false;

                btnEditGroupItem.Visible = true;
                btnRemoveGroupItem.Visible = true;
                btnCancelEditGroupItem.Visible = true;

                //show/hide
                tlpItemGroup.RowStyles[0] = new RowStyle(SizeType.Absolute, 25f);
                tlpItemGroup.RowStyles[1] = new RowStyle(SizeType.Absolute, 35f);
                tlpItemGroup.RowStyles[2] = new RowStyle(SizeType.Absolute, 200f);
                tlpItemGroup.RowStyles[3] = new RowStyle(SizeType.Absolute, 35f);
                tlpItemGroup.RowStyles[4] = new RowStyle(SizeType.Absolute, 35f);
                //tlpItemGroup.RowStyles[5] = new RowStyle(SizeType.Absolute, 35f);
                //tlpItemGroup.RowStyles[6] = new RowStyle(SizeType.Percent, 100f);
            }
            else
            {
                btnAddItem.Visible = true;
                btnEditItem.Visible = true;

                btnEditGroupItem.Visible = false;
                btnRemoveGroupItem.Visible = false;
                btnCancelEditGroupItem.Visible = false;
                //show/hide
                tlpItemGroup.RowStyles[0] = new RowStyle(SizeType.Absolute, 0f);
                tlpItemGroup.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
                tlpItemGroup.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                tlpItemGroup.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpItemGroup.RowStyles[4] = new RowStyle(SizeType.Absolute, 0f);
                //tlpItemGroup.RowStyles[5] = new RowStyle(SizeType.Absolute, 35f);
                //tlpItemGroup.RowStyles[6] = new RowStyle(SizeType.Percent, 100f);
            }

        }

        private void AddGroupItemDataToField()
        {
            //TO-DO:
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            ShowOrHideItemGroupEditUI(true);
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            ShowOrHideItemGroupEditUI(true);
            AddGroupItemDataToField();
        }

        private void frmSBBMould_Load(object sender, EventArgs e)
        {
            PAGE_LOADED = true;
        }

        private void frmSBBMould_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MouldFormOpen = false;
        }

        private void btnCancelEditGroupItem_Click(object sender, EventArgs e)
        {
            ShowOrHideItemGroupEditUI(false);
        }

   
        private void ClearError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            errorProvider8.Clear();
        }

        private void ClearMouldDataField()
        {
            PAGE_LOADED = false;
            cmbMouldName.SelectedIndex = -1;
            cmbMouldCode.SelectedIndex = -1;
            txtMouldName.Clear();
            txtMouldCode.Clear();
            txtTon.Clear();
            txtCavity.Clear();
            txtHeight.Clear();
            txtWidth.Clear();
            txtLength.Clear();
            PAGE_LOADED = true;

        }

        private bool MouldDataValidation()
        {
            bool Result = true;

            ClearError();

            if ((string.IsNullOrEmpty(txtMouldName.Text) && !MOULD_EDITMODE) || (string.IsNullOrEmpty(cmbMouldName.Text) && MOULD_EDITMODE))
            {
                Result = false;
                errorProvider1.SetError(lblMouldName, "Mould Name Required");
            }

            if ((string.IsNullOrEmpty(txtMouldCode.Text) && !MOULD_EDITMODE) || (string.IsNullOrEmpty(cmbMouldCode.Text) && MOULD_EDITMODE))
            {
                Result = false;
                errorProvider2.SetError(lblMouldCode, "Mould Code Required");
            }
            else if(!MOULD_EDITMODE)
            {
                //check if code duplicate
                DataTable dt = dalSBB.MouldSelect();

                if(dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        string MouldCode = row[dalSBB.MouldCode].ToString();

                        if(MouldCode == txtMouldCode.Text.ToUpper())
                        {
                            MessageBox.Show("Mould Code existed in database!");
                            errorProvider2.SetError(lblMouldCode, "Mould Code existed in database!");
                            Result = false;
                        }
                    }
                }

            }

            if (string.IsNullOrEmpty(txtTon.Text))
            {
                Result = false;
                errorProvider3.SetError(lblTon, "Mould Ton Required");
            }

            if (string.IsNullOrEmpty(txtCavity.Text))
            {
                Result = false;
                errorProvider4.SetError(lblCavity, "Mould Cavity Required");
            }

            if (string.IsNullOrEmpty(txtWidth.Text))
            {
                Result = false;
                errorProvider5.SetError(lblWidth, "Mould Width Required");
            }

            if (string.IsNullOrEmpty(txtHeight.Text))
            {
                Result = false;
                errorProvider6.SetError(lblHeight, "Mould Height Required");
            }

            if (string.IsNullOrEmpty(txtLength.Text))
            {
                Result = false;
                errorProvider7.SetError(lblLength, "Mould Length Required");
            }

            return Result;
        }

        private void MouldDataUpdate()
        {
            bool DataUpdateResult = false;

            uData.Updated_By = MainDashboard.USER_ID;
            uData.Updated_Date = DateTime.Now;
            uData.Mould_startdate = DateTime.Now;

            uData.Mould_ton = int.TryParse(txtTon.Text, out int x) ? x : -1;
            uData.Mould_Total_cavity = int.TryParse(txtCavity.Text, out x) ? x : -1;
            uData.Mould_width = float.TryParse(txtWidth.Text, out float y) ? y : -1;
            uData.Mould_height = float.TryParse(txtHeight.Text, out y) ? y : -1;
            uData.Mould_length = float.TryParse(txtLength.Text, out y) ? y : -1;

            if (MOULD_EDITMODE)
            {
                uData.Mould_name = cmbMouldName.Text.ToUpper();
                uData.Mould_code = cmbMouldCode.Text.ToUpper();

                foreach(DataRow row in DT_MOULD_DATA.Rows)
                {
                    if(cmbMouldCode.Text.ToUpper() == row[dalSBB.MouldCode].ToString())
                    {
                        uData.Table_Code = int.TryParse(row[dalSBB.TableCode].ToString(), out int i) ? i : -1;

                    }
                }

                if(uData.Table_Code > 0)
                DataUpdateResult = dalSBB.MouldUpdate(uData);

                else
                {
                    MessageBox.Show("Table Code not found!");
                }
            }
            else
            {
                uData.Mould_name = txtMouldName.Text.ToUpper();
                uData.Mould_code = txtMouldCode.Text.ToUpper();

                DataUpdateResult = dalSBB.InsertMould(uData);
            }

            if (!DataUpdateResult)
            {
                MessageBox.Show("Failed to insert/update Mould data!");
            }
            else
            {
                MessageBox.Show("Mould data inserted/updated!");
                ClearMouldDataField();
                DT_MOULD_DATA = dalSBB.MouldWithoudRemovedDataSelect();

            }
        }

        private void btnEditMould_Click(object sender, EventArgs e)
        {
            if(MouldDataValidation())
            {
                DialogResult dialogResult = MessageBox.Show("Confirm to insert new mould data ?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    DataTable dt_ItemGroup = (DataTable)dgvItemGroup.DataSource;

                    if(dt_ItemGroup == null || dt_ItemGroup.Rows.Count <= 0)
                    {
                        dialogResult = MessageBox.Show("Item Group not found.\nConfirm to insert/edit Mould data without item group listing?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            MouldDataUpdate();
                        }
                    }
                    else
                    {
                        MouldDataUpdate();
                    }
                   
                }
            }
        }

        private void cmbMouldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MouldName = cmbMouldName.Text;

            if(!string.IsNullOrEmpty(MouldName) && PAGE_LOADED)
            {
                LoadMouldCodeToCMB();
            }
        }

        private void LoadMouldData()
        {
            string MouldCode = cmbMouldCode.Text;

            foreach(DataRow row in DT_MOULD_DATA.Rows)
            {
                if (MouldCode == row[dalSBB.MouldCode].ToString())
                {
                    txtTon.Text = row[dalSBB.MouldTon].ToString();
                    txtCavity.Text = row[dalSBB.MouldTotalCavity].ToString();
                    txtWidth.Text = row[dalSBB.MouldWidth].ToString();
                    txtHeight.Text = row[dalSBB.MouldHeight].ToString();
                    txtLength.Text = row[dalSBB.MouldLength].ToString();
                }
            }
        }

        private void cmbMouldCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PAGE_LOADED && MOULD_EDITMODE)
            {
                LoadMouldData();

            }
        }
    }
}
