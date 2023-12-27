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

namespace FactoryManagementSoftware.UI
{
    public partial class frmDOEditing : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();

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
        private void dgvUIEdit(DataGridView dgv)
        {
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            //int smallColumnWidth = 60;

            //if (dgv == dgvItemList)
            //{
            //    dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            //    dgv.Columns[text.Header_TargetQty].HeaderCell.Style.BackColor = Color.FromArgb(255, 153, 153);

            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            //    dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //    dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;

            //    dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    dgv.Columns[text.Header_Job_Purpose].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //    dgv.Columns[text.Header_AutoQtyAdjustment].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //    dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //    dgv.Columns[text.Header_Job_Purpose].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            //    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            //    //dgv.Columns[text.Header_MouldCode].Width = smallColumnWidth;
            //    dgv.Columns[text.Header_Cavity].Width = smallColumnWidth;
            //    //dgv.Columns[text.Header_AutoQtyAdjustment].Width = smallColumnWidth;
            //    //dgv.Columns[text.Header_MouldSelection].Width = ProductionInfoColumnWidth;
            //    //dgv.Columns[text.Header_ItemSelection].Width = ProductionInfoColumnWidth;
            //    //dgv.Columns[text.Header_MouldCode].Width = ProductionInfoColumnWidth;
            //    //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;

            //    dgv.Columns[text.Header_JobNo].Visible = false;
            //    dgv.Columns[text.Header_ItemCode].Visible = false;
            //    dgv.Columns[text.Header_ItemName].Visible = false;
            //    dgv.Columns[text.Header_MouldCode].Visible = false;

            //    //dgv.Columns[text.Header_Cavity].Visible = false;
            //    dgv.Columns[text.Header_ProTon].Visible = false;
            //    dgv.Columns[text.Header_ProCT].Visible = false;
            //    //dgv.Columns[text.Header_ProPwShot].Visible = false;
            //    dgv.Columns[text.Header_ProRwShot].Visible = false;
            //    dgv.Columns[text.Header_FamilyWithJobNo].Visible = false;

            //    //int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

            //    //if (itemListRowHeight < 150)
            //    //{
            //    //    tlpItemSelection.RowStyles[1].SizeType = SizeType.Absolute;
            //    //    tlpItemSelection.RowStyles[1].Height = itemListRowHeight;


            //    //    tlpLeftPanel.RowStyles[2].SizeType = SizeType.Absolute;
            //    //    tlpLeftPanel.RowStyles[2].Height = itemListRowHeight + 157;
            //    //}
            //}
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

        #region Page Control

        private int CURRENT_STEP = 1;
        private bool DO_EDITING_MODE = false;

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
                else if (CURRENT_STEP > 4)
                {
                    CURRENT_STEP = 4;
                }

                StepUIUpdates(CURRENT_STEP, true);
            }
        }

        private void btnJobPublish_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #endregion

        #region Data Logic

        private void LoadSummaryData()
        {

        }

        private bool Validation(int currentStep)
        {
            return true;
        }

        private void cmbFromBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string branch = cmbFromBranch.Text;

            if(cmbDOType.Text == "Internal Transfer Note")
            {
                if (branch == text.Factory_OUG)
                {
                    cmbToDeliveryLocation.Text = text.Factory_Semenyih;
                }
                else if(branch == text.Factory_Semenyih)
                {
                    cmbToDeliveryLocation.Text = text.Factory_OUG;
                }
            }
        }




        #endregion

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

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
