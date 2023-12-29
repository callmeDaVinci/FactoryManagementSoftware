using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Linq;
using System.Threading;
using static Syncfusion.XlsIO.Parser.Biff_Records.PivotTable.PivotViewItemRecord;
using System.Web.UI.Design;
using Syncfusion.XlsIO.Implementation.XmlSerialization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDOEditing : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        itemDAL dalItem = new itemDAL();


        public frmDOEditing()
        {
            InitializeComponent();
            InitialSetting();
        }

        #region UI/UX
        private void InitialSetting()
        {
            CURRENT_STEP = 1;
            StepUIUpdates(CURRENT_STEP, false);
        }

        #region Appearance

        DataTable DT_ITEM_LIST;

        private DataTable NewItemList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_Qty, typeof(decimal));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            return dt;
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            //int smallColumnWidth = 60;

            if (dgv == dgvDOItemList)
            {
                dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemCode].MinimumWidth = 100;


                // Enable wrap mode for the header description
                dgv.Columns[text.Header_Description].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_Description].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_Description].MinimumWidth = 100;

                dgv.Columns[text.Header_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }



            //else if (dgv == dgvRawMatList)
            //{
            //    //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
            //    dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //    dgv.Columns[text.Header_Qty_Required_Bag].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_Qty_Required_KG].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_KGPERBAG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            //    dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            //    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            //    dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    dgv.Columns[text.Header_Selection].Width = smallColumnWidth;
            //    dgv.Columns[text.Header_RoundUp_ToBag].Width = smallColumnWidth;
            //    dgv.Columns[text.Header_Index].Width = smallColumnWidth;
            //    dgv.Columns[text.Header_Qty_Required_KG].Width = smallColumnWidth;
            //    dgv.Columns[text.Header_KGPERBAG].Width = smallColumnWidth;
            //    dgv.Columns[text.Header_Qty_Required_Bag].Width = smallColumnWidth;

            //    dgv.Columns[text.Header_ItemCode].Visible = false;
            //    dgv.Columns[text.Header_ItemName].Visible = false;
            //    dgv.Columns[text.Header_Remark].Visible = false;

            //    //int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

            //    //if (itemListRowHeight < 192)
            //    //{
            //    //    tlpMatSelection.RowStyles[1].SizeType = SizeType.Absolute;
            //    //    tlpMatSelection.RowStyles[1].Height = itemListRowHeight;
            //    //}

            //}
            //else if (dgv == dgvStockCheck)
            //{
            //    dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
            //    dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //    dgv.Columns[text.Header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            //    dgv.Columns[text.Header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_Type].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            //    //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            //    dgv.Columns[text.Header_Index].Width = smallColumnWidth;
            //    //int columnWidth = 60;

            //    //dgv.Columns[text.Header_ReadyStock].Width = columnWidth;
            //    //dgv.Columns[text.Header_ReservedForOtherJobs].Width = columnWidth;
            //    //dgv.Columns[text.Header_RequiredForCurrentJob].Width = columnWidth;
            //    //dgv.Columns[text.Header_BalStock].Width = columnWidth;

            //    dgv.Columns[text.Header_ItemCode].Visible = false;
            //    dgv.Columns[text.Header_ItemName].Visible = false;
            //}
            //else if (dgv == dgvMacSchedule)
            //{
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
            //    dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //    dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            //    dgv.Columns[text.Header_RawMat_1].MinimumWidth = 100;
            //    dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //    dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ColorMat].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //    dgv.Columns[text.Header_ColorMat].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //    dgv.Columns[text.Header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //    dgv.Columns[text.Header_Status].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //    dgv.Columns[text.Header_MacName].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);


            //    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            //    dgv.Columns[text.Header_DateStart].MinimumWidth = 120;
            //    dgv.Columns[text.Header_EstDateEnd].MinimumWidth = 120;


            //    dgv.Columns[text.Header_Fac].Width = smallColumnWidth - 10;
            //    dgv.Columns[text.Header_MacName].Width = smallColumnWidth - 10;
            //    dgv.Columns[text.Header_Status].Width = smallColumnWidth;
            //    //dgv.Columns[text.Header_DateStart].Width = smallColumnWidth + 5;
            //    //dgv.Columns[text.Header_EstDateEnd].Width = smallColumnWidth + 5;

            //    dgv.Columns[text.Header_ItemCode].Visible = false;
            //    dgv.Columns[text.Header_ItemName].Visible = false;
            //    dgv.Columns[text.Header_MacID].Visible = false;
            //    dgv.Columns[text.Header_FacID].Visible = false;
            //    dgv.Columns[text.Header_Ori_Status].Visible = false;
            //    dgv.Columns[text.Header_Ori_DateStart].Visible = false;
            //    dgv.Columns[text.Header_Ori_EstDateEnd].Visible = false;
            //    //dgv.Columns[text.Header_Remark].Visible = false;
            //    dgv.Columns[text.Header_ColorMatCode].Visible = false;
            //    dgv.Columns[text.Header_JobNo].Visible = false;

            //    dgv.Columns[text.Header_FamilyWithJobNo].Visible = false;
            //    dgv.Columns[text.Header_ProductionDay].Visible = false;
            //    dgv.Columns[text.Header_ProductionHour].Visible = false;
            //    dgv.Columns[text.Header_ProductionHourPerDay].Visible = false;

            //}
        }

        #endregion

        #region Event Handlers

        private string ITEM_SEARCH_DEFAULT_TEXT = "Search (Item Name/Code)";
        private string ITEM_CUSTOM_DEFAULT_TEXT = "Fill in Item's Description";
        private bool SEARCH_ICON_UPDATING = false;
        private int CURRENT_STEP = 1;
        private bool DO_EDITING_MODE = false;


        private void cmbFromBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string branch = cmbFromBranch.Text;

            if (cmbDOType.Text == "Internal Transfer Note")
            {
                if (branch == text.Factory_OUG)
                {
                    cmbToDeliveryLocation.Text = text.Factory_Semenyih;
                }
                else if (branch == text.Factory_Semenyih)
                {
                    cmbToDeliveryLocation.Text = text.Factory_OUG;
                }
            }
        }

        private void assignDOItemListtoDGV()
        {
            if (DT_ITEM_LIST == null)
            {
                DT_ITEM_LIST = NewItemList();


            }

            dgvDOItemList.DataSource = DT_ITEM_LIST;
            dgvUIEdit(dgvDOItemList);
        }

        private void StepUIUpdates(int step, bool Continue)
        {
            #region setting

            if (step < 1)
            {
                step = 1;
                CURRENT_STEP = 1;
            }

            Font CurrentStepFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font normalStepFont = new Font("Segoe UI", 8F, FontStyle.Regular);

            lblStep1.Font = normalStepFont;
            lblStep2.Font = normalStepFont;
            lblStep3.Font = normalStepFont;

            tlpStepPanel.Visible = false;
            btnCancel.Visible = false;
            btnAddAsDraft.Visible = false;
            btnJobPublish.Visible = false;

            tlpStepPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0);//D/O Settings
            tlpStepPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);//Item List
            tlpStepPanel.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);//Final Reivew


            tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);//Previous
            tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);//Continue
            tlpButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);//Cancel
            tlpButton.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0);//Draft
            tlpButton.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0);//Add D/O



            Color FormBackColor = Color.FromArgb(147, 168, 255);//147, 168, 255 //245, 247, 255
            Color CircleLabelProcessingColor = Color.White;
            Color lblStepTextForeColor = Color.Black;
            Color CurrentStepColor = Color.FromArgb(254, 241, 154);
            Color circleLabelCompletedBackColor = Color.FromArgb(153, 255, 204);

            string tickText = "✓";
  
            circleLabelStep1.CircleBackColor = CircleLabelProcessingColor;
            circleLabelStep2.CircleBackColor = CircleLabelProcessingColor;
            circleLabelStep3.CircleBackColor = CircleLabelProcessingColor;

            circleLabelStep1.Text = "1";
            circleLabelStep2.Text = "2";
            circleLabelStep3.Text = "3";

            lblStep1.ForeColor = lblStepTextForeColor;
            lblStep2.ForeColor = lblStepTextForeColor;
            lblStep3.ForeColor = lblStepTextForeColor;

            tlpStepPanel.ColumnStyles[step - 1] = new ColumnStyle(SizeType.Percent, 100);

            #endregion

            switch (step)
            {
                #region Case 1: D/O Settings
                case 1:

                    circleLabelStep1.CircleBackColor = CurrentStepColor;
                    lblStep1.Font = CurrentStepFont;

                    tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 200);//Continue
                  
                    btnPreviousStep.Visible = false;
                    btnContinue.Visible = true;

                    break;
                #endregion

                #region Case 2: Item List
                case 2:

                    circleLabelStep1.Text = tickText;
                    circleLabelStep1.CircleBackColor = circleLabelCompletedBackColor;

                    circleLabelStep2.CircleBackColor = CurrentStepColor;

                    lblStep2.Font = CurrentStepFont;

                    tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200);//Previous
                    tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 200);//Continue

                    btnPreviousStep.Visible = true;
                    btnContinue.Visible = true;
                    InitialNameTextBox();

                    assignDOItemListtoDGV();

                    if (Continue)
                    {
                        //MaterialStockCheckMode(); 
                        //change to Item List Mode();
                    }

                    break;

                #endregion

                #region Case 3: Summary
                case 3:

                    circleLabelStep1.Text = tickText;
                    circleLabelStep2.Text = tickText;
                    circleLabelStep1.CircleBackColor = circleLabelCompletedBackColor;
                    circleLabelStep2.CircleBackColor = circleLabelCompletedBackColor;

                    circleLabelStep3.CircleBackColor = CurrentStepColor;

                    lblStep3.Font = CurrentStepFont;

                    tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200);//Previous
                    //tlpButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 150);//Cancel
                    tlpButton.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 200);//Draft
                    tlpButton.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 200);//Add D/O

                    btnContinue.Visible = false;

                    btnCancel.Visible = true;
                    btnAddAsDraft.Visible = true;
                    btnJobPublish.Visible = true;

                    if (DO_EDITING_MODE)
                    {
                        btnAddAsDraft.Text = "Update as Draft";
                        btnJobPublish.Text = "D/O Update";
                    }

                    LoadSummaryData();

                    break;
                    #endregion
            }

            System.Threading.Thread.Sleep(100);

            tlpStepPanel.Visible = true;

        }

        private void btnPreviousStep_Click(object sender, EventArgs e)
        {
            CURRENT_STEP--;

            if (CURRENT_STEP < 1)
            {
                CURRENT_STEP = 1;
            }
            else if (CURRENT_STEP > 4)
            {
                CURRENT_STEP = 4;
            }

            StepUIUpdates(CURRENT_STEP, false);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (Validation(CURRENT_STEP))
            {
                CURRENT_STEP++;

                if (CURRENT_STEP < 1)
                {
                    CURRENT_STEP = 1;
                }
                else if (CURRENT_STEP > 3)
                {
                    CURRENT_STEP = 3;
                }

                StepUIUpdates(CURRENT_STEP, true);
            }
        }

        private void btnJobPublish_Click(object sender, EventArgs e)
        {

        }

        private void cmbToDeliveryLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DeliveryLocation = cmbToDeliveryLocation.Text;

            if (cmbDOType.Text == "Internal Transfer Note")
            {
                if (DeliveryLocation == text.Factory_OUG)
                {
                    cmbFromBranch.Text = text.Factory_Semenyih;
                }
                else if (DeliveryLocation == text.Factory_Semenyih)
                {
                    cmbFromBranch.Text = text.Factory_OUG;
                }
            }
        }

        private void gunaTextBox12_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!SEARCH_ICON_UPDATING)
            {
                SEARCH_ICON_UPDATING = true;

                cbItemCustom.Checked = !cbItemSearch.Checked;

                UpdatePreviewDescription();
                itemCodePreviewUpdate();

                SEARCH_ICON_UPDATING = false;

                searchTypeUpdate();

            }


        }

        private void cbItemCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (!SEARCH_ICON_UPDATING)
            {
                SEARCH_ICON_UPDATING = true;

                cbItemSearch.Checked = !cbItemCustom.Checked;
                UpdatePreviewDescription();
                itemCodePreviewUpdate();

                SEARCH_ICON_UPDATING = false;

                searchTypeUpdate();
            }

        }

        private void searchTypeUpdate()
        {
            if (cbItemCustom.Checked)
            {
                btnSearch.Image = Properties.Resources.icons8_pencil_100;
            }
            else
            {
                btnSearch.Image = Properties.Resources.icons8_search_500;
            }

            if (txtItemDescription.Text.Length <=0 || txtItemDescription.Text == ITEM_SEARCH_DEFAULT_TEXT || txtItemDescription.Text == ITEM_CUSTOM_DEFAULT_TEXT)
            {
                ItemDescriptionTextSetDefault();
            }
        }

        private void txtItemDescription_Enter(object sender, EventArgs e)
        {
            if (cbItemSearch.Checked)
            {
                if (txtItemDescription.Text == ITEM_SEARCH_DEFAULT_TEXT)
                {
                    txtItemDescription.Text = "";
                    txtItemDescription.ForeColor = SystemColors.WindowText;
                }
            }
            else
            {
                if (txtItemDescription.Text == ITEM_CUSTOM_DEFAULT_TEXT)
                {
                    txtItemDescription.Text = "";
                    txtItemDescription.ForeColor = SystemColors.WindowText;
                }
            }

        }

        private void ItemDescriptionTextSetDefault()
        {
            if (cbItemSearch.Checked)
            {
                txtItemDescription.Text = ITEM_SEARCH_DEFAULT_TEXT;
            }
            else
            {
                txtItemDescription.Text = ITEM_CUSTOM_DEFAULT_TEXT;
            }

            txtItemDescription.ForeColor = SystemColors.GrayText;
        }

        private void txtItemDescription_Leave(object sender, EventArgs e)
        {

        }

        private void txtNumericWithDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, control characters, and a single decimal point
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || (sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true; // Ignore the character
            }
        }

        private void totalQtyCalculation()
        {
            // Parse the numeric values from the TextBoxes. If parsing fails, default to 0.
            decimal.TryParse(txtQtyPerBox.Text, out decimal qtyPerBox);
            decimal.TryParse(txtBoxQty.Text, out decimal boxQty);
            decimal.TryParse(txtBalanceQty.Text, out decimal balanceQty);

            // Calculate and assign the result
            txtTotalQty.Text = (qtyPerBox * boxQty + balanceQty).ToString();
        }

        private void txtQtyPerBox_TextChanged(object sender, EventArgs e)
        {
            totalQtyCalculation();
        }

        private void txtBoxQty_TextChanged(object sender, EventArgs e)
        {
            totalQtyCalculation();

        }

        private void txtBalanceQty_TextChanged(object sender, EventArgs e)
        {
            totalQtyCalculation();

        }

        private bool IsTotalQtyCorrect()
        {
            // Parse the numeric values from the TextBoxes. If parsing fails, default to 0.
            decimal.TryParse(txtQtyPerBox.Text, out decimal qtyPerBox);
            decimal.TryParse(txtBoxQty.Text, out decimal boxQty);
            decimal.TryParse(txtBalanceQty.Text, out decimal balanceQty);

            // Calculate the expected total
            decimal expectedTotal = qtyPerBox * boxQty + balanceQty;

            // Check if the calculated total matches the value in txtTotalQty
            return txtTotalQty.Text == expectedTotal.ToString();
        }

        private string GetPackagingInfo()
        {
            // Parse the numeric values. If parsing fails, default to 0.
            decimal.TryParse(txtQtyPerBox.Text, out decimal qtyPerBox);
            decimal.TryParse(txtBoxQty.Text, out decimal boxQty);
            decimal.TryParse(txtBalanceQty.Text, out decimal balanceQty);

            // Get the box unit, default to "ctn" if empty
            string boxUnit = string.IsNullOrEmpty(txtBoxUnit.Text) ? "ctn" : txtBoxUnit.Text;
            string pcsUnit = string.IsNullOrEmpty(txtTotalQtyUnit.Text) ? "pcs" : txtTotalQtyUnit.Text;

            // Construct the packaging info string
            string packagingInfo = $"{qtyPerBox} {pcsUnit} x {boxQty} {boxUnit}";

            // Add balance part if balanceQty is not zero
            if (balanceQty > 0)
            {
                packagingInfo += $" + bal. {balanceQty}";
            }

            return packagingInfo;
        }

        private void totalQtyPreviewUpdate()
        {
            // Parse the numeric values. If parsing fails, default to 0.
            decimal.TryParse(txtTotalQty.Text, out decimal totalQty);
            
            string qtyUnit = string.IsNullOrEmpty(txtTotalQtyUnit.Text) ? "pcs" : txtTotalQtyUnit.Text;

            // Construct the packaging info string
            string totalQtyInfo = $"{totalQty} {qtyUnit}";

            txtQtyPreview.Text = totalQtyInfo;
        }

        private void itemCodePreviewUpdate()
        {
            if(!string.IsNullOrEmpty(txtItemDescription.Text))
            {
                string CodePreview = "custom";

                if (cbItemSearch.Checked)
                {
                    if (txtItemDescription.Text == ITEM_SEARCH_DEFAULT_TEXT)
                    {
                        CodePreview = "INVALID";
                    }
                    else
                    {
                        CodePreview = ITEM_CODE;
                    }
                }


                txtItemCodePreview.Text = CodePreview;
            }
           
        }

        private void UpdatePreviewDescription()
        {
            string previewDescription = "";

            //get category info & item name
            if(cbItemSearch.Checked && !string.IsNullOrEmpty(txtItemDescription.Text))
            {
               

                if (txtItemDescription.Text == ITEM_SEARCH_DEFAULT_TEXT )
                {
                    previewDescription += "[PLEASE SEARCH A ITEM]";
                }
                else
                {
                    if (!cbDescriptionIncludeCategory.Checked)
                    {
                        previewDescription += txtItemDescription.Text.Replace($"[{ITEM_CATEGORY}] ", "");
                    }
                    else
                    {
                        previewDescription += txtItemDescription.Text;
                    }

                   
                }
            }
            else
            {
                if (txtItemDescription.Text == ITEM_CUSTOM_DEFAULT_TEXT)
                {
                    previewDescription += "[PLEASE FILL IN ITEM'S DESCRIPTION]";
                }
                else
                {
                    previewDescription += txtItemDescription.Text;
                }
            }

            //get packaging info
            if (txtTotalQty.Text != "0" && !string.IsNullOrEmpty(txtTotalQty.Text) &&  cbDescriptionIncludePackaging.Checked && IsTotalQtyCorrect())
            {
                previewDescription += " : " + GetPackagingInfo();
            }

            //get remark
            if(cbDescriptionIncludeRemark.Checked &&  !string.IsNullOrEmpty(txtRemark.Text))
            {
                previewDescription += " " + txtRemark.Text;
            }

            txtDescriptionPreview.Text = previewDescription;

        }

        private void txtItemDescription_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();
            itemCodePreviewUpdate();
        }

        private void txtTotalQty_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();
            totalQtyPreviewUpdate();
        }

        private void cbDescriptionIncludeCategory_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();
        }

        private void cbDescriptionIncludePackaging_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();

        }

        private void cbDescriptionIncludeRemark_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();

        }

        #endregion

        #region Logic

        private void LoadSummaryData()
        {

        }

        private bool Validation(int currentStep)
        {
            return true;
        }


        private void AddItem(string itemCode, string description, decimal quantity, string unit)
        {
            // Create a new row in the DataTable
            DataRow newRow = DT_ITEM_LIST.NewRow();

            // Assign values to each column in the DataRow
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_Description] = description;
            newRow[text.Header_Qty] = quantity;
            newRow[text.Header_Unit] = unit;

            // Add the new row to the DataTable
            DT_ITEM_LIST.Rows.Add(newRow);

            if (dgvDOItemList.DataSource != DT_ITEM_LIST)
            {
                dgvDOItemList.DataSource = DT_ITEM_LIST;
            }
        }


        #endregion

        #endregion

        #endregion

        #region Data Access

        private DataTable DT_ITEM_SOURCE;

        private string header_MasterCode = "MasterCode";

        private void InitialNameTextBox()
        {
            DT_ITEM_SOURCE = dalItem.Select();

            DT_ITEM_SOURCE.Columns.Add(header_MasterCode, typeof(string));

            var masterCodes = new List<string>();

            foreach (DataRow row in DT_ITEM_SOURCE.Rows)
            {
                var category = row[dalItem.ItemCat].ToString();
                var itemName = row[dalItem.ItemName].ToString();
                var itemCode = row[dalItem.ItemCode].ToString();

                var masterCode = $"[{category}] {itemName}";

                if (itemName != itemCode)
                {
                    masterCode += $" ({itemCode})";
                }

                row[header_MasterCode] = masterCode;
                masterCodes.Add(masterCode);
            }

            txtItemDescription.Values = masterCodes.ToArray();

            //DT_ITEM_SOURCE = dalItem.Select();

            //DT_ITEM_SOURCE.Columns.Add(header_MasterCode, typeof(string));

            //string[] stringArray = new string[DT_ITEM_SOURCE.Rows.Count];

            //for (int i = 0; i < DT_ITEM_SOURCE.Rows.Count; i++)
            //{
            //    string category = DT_ITEM_SOURCE.Rows[i][dalItem.ItemCat].ToString();
            //    string itemName = DT_ITEM_SOURCE.Rows[i][dalItem.ItemName].ToString();
            //    string itemCode = DT_ITEM_SOURCE.Rows[i][dalItem.ItemCode].ToString();

            //    string masterCode = "[" + category + "] " + itemName;

            //    if(itemName != itemCode)
            //    {
            //        masterCode += " (" + itemCode + ")";
            //    }
            //    DT_ITEM_SOURCE.Rows[i][header_MasterCode] = masterCode;

            //    stringArray[i] = masterCode;
            //}

            //txtItemDescription.Values = stringArray;
        }


        #endregion

        private string ITEM_CODE = "ITEM CODE";
        private string ITEM_CATEGORY = "CATEGORY";

        private void txtItemDescription_TextChanged_1(object sender, EventArgs e)
        {
            string keywords = txtItemDescription.Text;

            if (!string.IsNullOrEmpty(keywords) && cbItemSearch.Checked)
            {
                foreach (DataRow row in DT_ITEM_SOURCE.Rows)
                {
                    ITEM_CATEGORY = row[dalItem.ItemCat].ToString();

                    if(keywords == row[header_MasterCode].ToString())
                    {
                        ITEM_CATEGORY = row[dalItem.ItemCat].ToString();
                        ITEM_CODE = row[dalItem.ItemCode].ToString();

                        break;
                    }
                }
            }
            else
            {
                ITEM_CODE = "ITEM CODE";
                ITEM_CATEGORY = "CATEGORY";
            }

            UpdatePreviewDescription();
            itemCodePreviewUpdate();
        }

        private void btnNewJob_Click(object sender, EventArgs e)
        {
            AddItem(ITEM_CODE,txtDescriptionPreview.Text,decimal.Parse(txtTotalQty.Text),txtTotalQtyUnit.Text);
        }
    }
}
