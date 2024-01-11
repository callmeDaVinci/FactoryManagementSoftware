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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
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

            dt.Columns.Add(text.Header_SearchMode, typeof(bool));
            dt.Columns.Add(text.Header_ItemName, typeof(string));

            dt.Columns.Add(text.Header_TotalQty, typeof(string));
            dt.Columns.Add(text.Header_TotalQtyUnit, typeof(string));
            dt.Columns.Add(text.Header_QtyPerBox, typeof(string));
            dt.Columns.Add(text.Header_BoxQty, typeof(string));
            dt.Columns.Add(text.Header_BoxUnit, typeof(string));
            dt.Columns.Add(text.Header_Balance, typeof(string));

            dt.Columns.Add(text.Header_Remark, typeof(string));

            dt.Columns.Add(text.Header_DescriptionIncludeCategory, typeof(bool));
            dt.Columns.Add(text.Header_DescriptionIncludePackaging, typeof(bool));
            dt.Columns.Add(text.Header_DescriptionIncludeRemark, typeof(bool));


            return dt;
        }

        private void dgvUIEdit(DataGridView dgv)
        {

            if (dgv == dgvDOItemList)
            {
                dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemCode].MinimumWidth = 100;


                dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_Description].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_Description].MinimumWidth = 100;
                dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgv.Columns[text.Header_SearchMode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_TotalQty].Visible = false;
                dgv.Columns[text.Header_TotalQtyUnit].Visible = false;
                dgv.Columns[text.Header_QtyPerBox].Visible = false;
                dgv.Columns[text.Header_BoxQty].Visible = false;
                dgv.Columns[text.Header_BoxUnit].Visible = false;
                dgv.Columns[text.Header_Balance].Visible = false;
                dgv.Columns[text.Header_Remark].Visible = false;
                dgv.Columns[text.Header_Balance].Visible = false;
                dgv.Columns[text.Header_DescriptionIncludeCategory].Visible = false;
                dgv.Columns[text.Header_DescriptionIncludePackaging].Visible = false;
                dgv.Columns[text.Header_DescriptionIncludeRemark].Visible = false;

            }
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
            tlpItemListSetting.Visible = false;
            tlpDOPreview.Visible = false;
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

                    this.Size = new Size(1366, 750);
                    this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
);

                    circleLabelStep1.CircleBackColor = CurrentStepColor;
                    lblStep1.Font = CurrentStepFont;

                    tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 200);//Continue
                  
                    btnPreviousStep.Visible = false;
                    btnContinue.Visible = true;

                    break;
                #endregion

                #region Case 2: Item List
                case 2:


                    this.Size = new Size(1366, 750);
                    this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
);



                    DO_ITEM_EDIT_MODE = false;
                    EDITING_INDEX = -1;
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


                    tlpItemListSetting.Visible = true;

                    break;

                #endregion

                #region Case 3: Summary
                case 3:


                    this.Size = new Size(900, 1000);
                    this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
);

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

                    tlpDOPreview.Visible = true;

                    break;
                    #endregion
            }

            //System.Threading.Thread.Sleep(100);

            tlpStepPanel.Visible = true;

        }

        private void btnPreviousStep_Click(object sender, EventArgs e)
        {
            //frmLoading.ShowLoadingScreen();
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


            //System.Threading.Thread.Sleep(500);

            //frmLoading.CloseForm();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (Validation(CURRENT_STEP))
            {
                //frmLoading.ShowLoadingScreen();
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

                //System.Threading.Thread.Sleep(500);

                //frmLoading.CloseForm();
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
                InitialNameTextBox();

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
                InitialNameTextBox();

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
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void totalQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && txtTotalQty.Text.Contains('.'))
            {
                e.Handled = true;
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

        private void ItemFieldReset()
        {
            txtItemDescription.Text = null;
            ITEM_CODE = "";
            txtBoxQty.Text = "";
            txtBalanceQty.Text = "";
            txtQtyPerBox.Text = "";
            txtTotalQty.Text = "";
            txtRemark.Text = "";
            txtItemCodePreview.Text = "";
            txtQtyPreview.Text = "";

            btnCancelItemEdit.Visible = false;
            btnAddItem.Text = BUTTON_ADDTOLIST_TEXT;
        }

        private bool ItemFieldInspection()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();

            if (string.IsNullOrEmpty(txtItemDescription.Text) || txtItemDescription.Text == ITEM_SEARCH_DEFAULT_TEXT || txtItemDescription.Text == ITEM_CUSTOM_DEFAULT_TEXT)
            {
                errorProvider1.SetError(cbItemCustom, "Description Required");
                return false;
            }

            if (string.IsNullOrEmpty(txtTotalQty.Text) || txtTotalQty.Text == "0")
            { 
                errorProvider2.SetError(lblTotalQty, "Total Qty Required");
                return false;
            }


            return true;
        }

        private void AddItem()
        {
            if(ItemFieldInspection())
            {
                decimal TotalQty = decimal.TryParse(txtTotalQty.Text, out TotalQty) ? TotalQty : -1;

                if (TotalQty <= -1)
                {
                    errorProvider2.SetError(lblTotalQty, "Invalid!");
                    return;
                }

                // Create a new row in the DataTable
                DataRow newRow = DT_ITEM_LIST.NewRow();

                // Assign values to each column in the DataRow
                newRow[text.Header_ItemCode] = txtItemCodePreview.Text;
                newRow[text.Header_Description] = txtDescriptionPreview.Text;
                newRow[text.Header_Qty] = TotalQty;
                newRow[text.Header_Unit] = txtTotalQtyUnit.Text;

                newRow[text.Header_SearchMode] = cbItemSearch.Checked;
                newRow[text.Header_ItemName] = txtItemDescription.Text;

                newRow[text.Header_TotalQty] = txtTotalQty.Text;
                newRow[text.Header_TotalQtyUnit] = txtTotalQtyUnit.Text;
                newRow[text.Header_QtyPerBox] = txtQtyPerBox.Text;
                newRow[text.Header_BoxQty] = txtBoxQty.Text;
                newRow[text.Header_BoxUnit] = txtBoxUnit.Text;
                newRow[text.Header_Balance] = txtBalanceQty.Text;

                newRow[text.Header_Remark] = txtRemark.Text;

                newRow[text.Header_DescriptionIncludeCategory] = cbDescriptionIncludeCategory.Checked;
                newRow[text.Header_DescriptionIncludePackaging] = cbDescriptionIncludePackaging.Checked;
                newRow[text.Header_DescriptionIncludeRemark] = cbDescriptionIncludeRemark.Checked;



                int index = 1;

                if (DT_ITEM_LIST.Rows.Count > 0)
                {
                    index = int.TryParse(DT_ITEM_LIST.Rows[DT_ITEM_LIST.Rows.Count - 1][text.Header_Index].ToString(), out int i) ? i + 1 : index;
                }

                newRow[text.Header_Index] = index;


                // Add the new row to the DataTable
                DT_ITEM_LIST.Rows.Add(newRow);

                //if (dgvDOItemList.DataSource != DT_ITEM_LIST)
                //{
                //    dgvDOItemList.DataSource = DT_ITEM_LIST;
                //}
                dgvDOItemList.DataSource = null;

                dgvDOItemList.DataSource = DT_ITEM_LIST;

                ItemFieldReset();

                dgvUIEdit(dgvDOItemList);
                dgvDOItemList.ClearSelection();
            }
           
        }

        private void UpdateItem(int rowIndex)
        {
            if (ItemFieldInspection())
            {
                decimal TotalQty = decimal.TryParse(txtTotalQty.Text, out TotalQty) ? TotalQty : -1;

                if (TotalQty <= -1)
                {
                    errorProvider2.SetError(lblTotalQty, "Invalid!");
                    return;
                }

                if (rowIndex < 0 || rowIndex >= DT_ITEM_LIST.Rows.Count)
                {
                    MessageBox.Show("Invalid row index.");
                    return;
                }

                DataRow rowToUpdate = DT_ITEM_LIST.Rows[rowIndex];

                // Assign updated values to the row
                rowToUpdate[text.Header_ItemCode] = txtItemCodePreview.Text;
                rowToUpdate[text.Header_Description] = txtDescriptionPreview.Text;
                rowToUpdate[text.Header_Qty] = TotalQty;
                rowToUpdate[text.Header_Unit] = txtTotalQtyUnit.Text;

                rowToUpdate[text.Header_SearchMode] = cbItemSearch.Checked;
                rowToUpdate[text.Header_ItemName] = txtItemDescription.Text;

                rowToUpdate[text.Header_TotalQty] = txtTotalQty.Text;
                rowToUpdate[text.Header_TotalQtyUnit] = txtTotalQtyUnit.Text;
                rowToUpdate[text.Header_QtyPerBox] = txtQtyPerBox.Text;
                rowToUpdate[text.Header_BoxQty] = txtBoxQty.Text;
                rowToUpdate[text.Header_BoxUnit] = txtBoxUnit.Text;
                rowToUpdate[text.Header_Balance] = txtBalanceQty.Text;

                rowToUpdate[text.Header_Remark] = txtRemark.Text;

                rowToUpdate[text.Header_DescriptionIncludeCategory] = cbDescriptionIncludeCategory.Checked;
                rowToUpdate[text.Header_DescriptionIncludePackaging] = cbDescriptionIncludePackaging.Checked;
                rowToUpdate[text.Header_DescriptionIncludeRemark] = cbDescriptionIncludeRemark.Checked;

                dgvDOItemList.DataSource = null;
                dgvDOItemList.DataSource = DT_ITEM_LIST;

                ItemFieldReset();
                dgvUIEdit(dgvDOItemList);
                dgvDOItemList.ClearSelection();
            }
        }

        private void GetDataFromList(int rowIndex)
        {
            DataTable dt = (DataTable)dgvDOItemList.DataSource;

            cbItemSearch.Checked = bool.TryParse(dt.Rows[rowIndex][text.Header_SearchMode].ToString(), out bool search) ? search : true;
            txtItemDescription.Text = dt.Rows[rowIndex][text.Header_ItemName].ToString();

            ITEM_CODE = dt.Rows[rowIndex][text.Header_ItemCode].ToString();
            txtItemCodePreview.Text = dt.Rows[rowIndex][text.Header_ItemCode].ToString();


            txtTotalQty.Text = dt.Rows[rowIndex][text.Header_TotalQty].ToString();
            txtTotalQtyUnit.Text = dt.Rows[rowIndex][text.Header_TotalQtyUnit].ToString();
            txtQtyPerBox.Text = dt.Rows[rowIndex][text.Header_QtyPerBox].ToString();
            txtBoxQty.Text = dt.Rows[rowIndex][text.Header_BoxQty].ToString();
            txtBoxUnit.Text = dt.Rows[rowIndex][text.Header_BoxUnit].ToString();
            txtBalanceQty.Text = dt.Rows[rowIndex][text.Header_Balance].ToString();

            txtRemark.Text = dt.Rows[rowIndex][text.Header_Remark].ToString();

            cbDescriptionIncludeCategory.Checked = bool.TryParse(dt.Rows[rowIndex][text.Header_DescriptionIncludeCategory].ToString(), out bool includeCategory) ? includeCategory : false;
            cbDescriptionIncludePackaging.Checked = bool.TryParse(dt.Rows[rowIndex][text.Header_DescriptionIncludePackaging].ToString(), out bool includePackaging) ? includePackaging : false;
            cbDescriptionIncludeRemark.Checked = bool.TryParse(dt.Rows[rowIndex][text.Header_DescriptionIncludeRemark].ToString(), out bool includeRemark) ? includeRemark : false;

            txtDescriptionPreview.Text = dt.Rows[rowIndex][text.Header_Description].ToString();

            DO_ITEM_EDIT_MODE = true;
            btnAddItem.Text = BUTTON_UPDATETOLIST_TEXT;
            btnCancelItemEdit.Visible = true;

        }



        #endregion

        #endregion

        #endregion

        #region Data Access

        private DataTable DT_ITEM_SOURCE;

        private string header_MasterCode = "MasterCode";

        private void InitialNameTextBox()
        {
            if(cbItemSearch.Checked)
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
            }
            else
            {
                txtItemDescription.Values = null;
            }
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

        bool DO_ITEM_EDIT_MODE = false;
        private int EDITING_INDEX = -1;
        private string BUTTON_ADDTOLIST_TEXT = "Add to List";
        private string BUTTON_UPDATETOLIST_TEXT = "Update to List";

        private void btnNewJob_Click(object sender, EventArgs e)
        {
            if(!DO_ITEM_EDIT_MODE)
            {
                AddItem();

            }
            else
            {
                //UPDATE ITEM
                if (MessageBox.Show("Confirm to make changes?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    UpdateItem(EDITING_INDEX);
                }
            }
        }

        private void txtQtyPerBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && txtQtyPerBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void txtBoxQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
           
        }

        private void txtBalanceQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && txtBalanceQty.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void btnItemRemove_Click(object sender, EventArgs e)
        {
            if (dgvDOItemList.Rows.Count == 0)
            {
                MessageBox.Show("There are no rows in the Item List.");
                return;
            }


            if (dgvDOItemList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to remove.");
                return;
            }

            int rowIndex = dgvDOItemList.SelectedRows[0].Index;
            if (MessageBox.Show("Are you sure you want to remove this item?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dgvDOItemList.Rows.RemoveAt(rowIndex);

                for (int i = rowIndex; i < dgvDOItemList.Rows.Count; i++)
                {
                    dgvDOItemList.Rows[i].Cells[text.Header_Index].Value = i + 1;
                }
            }
        }

        
        private void btnItemEdit_Click(object sender, EventArgs e)
        {
            if (dgvDOItemList.Rows.Count == 0)
            {
                MessageBox.Show("There are no rows in the Item List.");
                return;
            }


            if (dgvDOItemList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.");
                return;
            }
            EDITING_INDEX = dgvDOItemList.SelectedRows[0].Index;
            GetDataFromList(EDITING_INDEX);
        }

        private void btnCancelItemEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this editing?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ItemFieldReset();
                dgvDOItemList.ClearSelection();
            }
        }

        private bool DATA_SAVED = false;

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

        private void tableLayoutPanel27_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTotalQtyUnit_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewDescription();
            totalQtyPreviewUpdate();
        }
    }
}
