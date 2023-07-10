using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmJobSummary : Form
    {
        #region Variable Declare


        Text text = new Text();
        Tool tool = new Tool();

        private DataTable DT_SUMMARY_ITEM = null;
        private DataTable DT_SUMMARY_RAW = null;
        private DataTable DT_SUMMARY_COLOR = null;
        private DataTable DT_SUMMARY_STOCKCHECK = null;
        private DataTable DT_SUMMARY_MAC_SCHEDULE = null;
        private PlanningBLL BLL_JOB_SUMMARY = new PlanningBLL();
        private planningActionDAL dalPlanningAction = new planningActionDAL();

        private matPlanBLL uMatPlan = new matPlanBLL();
        private matPlanDAL dalMatPlan = new matPlanDAL();

        static public bool JOB_ADDED = false;
        static public int JOB_ADDED_COUNT = 0;

        #endregion

        public frmJobSummary(DataTable dt_item, DataTable dt_Raw, DataTable dt_Color, DataTable dt_StockCheck, DataTable dt_MacSchedule, PlanningBLL jobSummary)
        {
            InitializeComponent();
            JOB_ADDED_COUNT = 0;
            JOB_ADDED = false;
            DT_SUMMARY_ITEM = dt_item;
          
            DT_SUMMARY_RAW = dt_Raw;
          
            DT_SUMMARY_COLOR = dt_Color;
          
            DT_SUMMARY_STOCKCHECK = dt_StockCheck;
          
            DT_SUMMARY_MAC_SCHEDULE = dt_MacSchedule;
          
            BLL_JOB_SUMMARY = jobSummary;

            LoadSummaryData();
        }

        private void LoadSummaryData()
        {
            #region Load Mould & Item Info

            int itemCount = 0;
            int maxShot = int.TryParse(BLL_JOB_SUMMARY.production_max_shot, out maxShot) ? maxShot : 0;

            foreach (DataRow row in DT_SUMMARY_ITEM.Rows)
            {
                string itemDescription = row[text.Header_ItemDescription].ToString();
                string itemCode = row[text.Header_ItemCode].ToString();

                int cavity = int.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;

                int maxQty = maxShot * cavity;

                if (!string.IsNullOrEmpty(itemCode))
                {
                    itemCount++;
                }

                if(itemCount == 2)
                {
                    lblDescriptionItem2.Text = itemDescription;
                    lblPWPerShotItem2.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem2.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem2.Text = row[text.Header_TargetQty].ToString() + " pcs"; 
                    lblMaxQtyItem2.Text = maxQty + " pcs"; 
                    lblJobPurposeItem2.Text = row[text.Header_Job_Purpose].ToString(); 
                }
                else if (itemCount == 3)
                {
                    lblDescriptionItem3.Text = itemDescription;
                    lblPWPerShotItem3.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem3.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem3.Text = row[text.Header_TargetQty].ToString() + " pcs";
                    lblMaxQtyItem3.Text = maxQty + " pcs";
                    lblJobPurposeItem3.Text = row[text.Header_Job_Purpose].ToString();
                }
                else if (itemCount == 4)
                {
                    lblDescriptionItem4.Text = itemDescription;
                    lblPWPerShotItem4.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem4.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem4.Text = row[text.Header_TargetQty].ToString() + " pcs";
                    lblMaxQtyItem4.Text = maxQty + " pcs";
                    lblJobPurposeItem4.Text = row[text.Header_Job_Purpose].ToString();
                }
                else if(itemCount == 1)
                {
                    lblDescriptionItem1.Text = itemDescription; 
                    lblPWPerShotItem1.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem1.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem1.Text = row[text.Header_TargetQty].ToString() + " pcs";
                    lblMaxQtyItem1.Text = maxQty + " pcs";
                    lblJobPurposeItem1.Text = row[text.Header_Job_Purpose].ToString();
                }
            }

            ItemPanelUISetting(itemCount);

            lblMouldCode.Text = BLL_JOB_SUMMARY.plan_mould_code;
            lblMouldTon.Text = BLL_JOB_SUMMARY.plan_mould_ton;
          
            lblCycleTime.Text = BLL_JOB_SUMMARY.plan_ct + " s";
            lblMouldTotalCavity.Text = BLL_JOB_SUMMARY.plan_cavity + " pcs";
            lblMouldTotalPWPerShot.Text = BLL_JOB_SUMMARY.plan_pw_shot + " g";
            lblMouldTotalRWPerShot.Text = BLL_JOB_SUMMARY.plan_rw_shot + " g";
            lblMouldMaxShot.Text = BLL_JOB_SUMMARY.production_max_shot;

            #endregion

            #region Load Material Info

            lblMaterialSetting1.Text = "";
            lblMaterialSetting2.Text = "";

            if (BLL_JOB_SUMMARY.raw_round_up_to_bag)
            {
                lblMaterialSetting1.Text = "✅ Raw Round-Up To Bag";

                if (BLL_JOB_SUMMARY.use_recycle)
                {
                    lblMaterialSetting2.Text = "✅ Recycle Runner Mat.";
                }
            }
            else if (BLL_JOB_SUMMARY.use_recycle)
            {
                lblMaterialSetting1.Text = "✅ Recycle Runner Mat.";
            }

            string rawMaterialDescription = "";
            string colorMaterialDescription = "";
            string rawRatio = "";

            foreach (DataRow row in DT_SUMMARY_RAW.Rows)
            {
                if(rawMaterialDescription == "")
                {
                    rawMaterialDescription = row[text.Header_ItemDescription].ToString();
                }
                else
                {
                    rawMaterialDescription += "(" + rawRatio + " %)";

                    rawMaterialDescription +=  " + " + row[text.Header_ItemDescription].ToString();
                    rawMaterialDescription += "(" + rawRatio + " %)";
                }



                BLL_JOB_SUMMARY.material_code = row[text.Header_ItemCode].ToString();
                BLL_JOB_SUMMARY.material_bag_kg = row[text.Header_KGPERBAG].ToString();
                BLL_JOB_SUMMARY.material_bag_qty = row[text.Header_BAG].ToString();
                BLL_JOB_SUMMARY.raw_material_qty = row[text.Header_KG].ToString();

                rawRatio = row[text.Header_Ratio].ToString();
            }

            lblRawDescription.Text = rawMaterialDescription;

            lblColorTitle.Text = "COLOR (" + DT_SUMMARY_COLOR.Rows[0][text.Header_Color].ToString() + ")";
            colorMaterialDescription = DT_SUMMARY_COLOR.Rows[0][text.Header_ItemDescription].ToString();
            lblColorDescription.Text = colorMaterialDescription;

            BLL_JOB_SUMMARY.color_material_code = DT_SUMMARY_COLOR.Rows[0][text.Header_ItemCode].ToString();
            BLL_JOB_SUMMARY.color_material_usage = DT_SUMMARY_COLOR.Rows[0][text.Header_Percentage].ToString();
            BLL_JOB_SUMMARY.color_material_qty = DT_SUMMARY_COLOR.Rows[0][text.Header_KG].ToString();
            BLL_JOB_SUMMARY.part_color = DT_SUMMARY_COLOR.Rows[0][text.Header_Color].ToString();

            lblTotalRawKG.Text = BLL_JOB_SUMMARY.raw_material_qty_kg + " KG";
            lblTotalColorKG.Text = BLL_JOB_SUMMARY.color_material_qty_kg + " KG";

            if (BLL_JOB_SUMMARY.use_recycle)
            {
                lblTotalRecycleMatKG.Text = BLL_JOB_SUMMARY.recycle_material_qty_kg + " KG";

            }
            else
            {
                lblTotalRecycleMatKG.Text = "0";

            }

            #endregion

            #region Load Pro. Time

            lblProDay.Text = BLL_JOB_SUMMARY.production_day;
            lblProBalHrs.Text = BLL_JOB_SUMMARY.production_hour;
            lblProDateTitle.Text = "DAY (" + BLL_JOB_SUMMARY.production_hour_per_day + "hrs)";

            lblStart.Text = BLL_JOB_SUMMARY.production_start_date.ToShortDateString();
            lblEstEnd.Text = BLL_JOB_SUMMARY.production_end_date.ToShortDateString();

            #endregion

            #region Load Machine ID and Location

            lblMacLocation.Text = BLL_JOB_SUMMARY.machine_location_string.ToString();
            lblMachineID.Text = BLL_JOB_SUMMARY.machine_id.ToString();

            #endregion

            #region Load Remark

            txtRemark.Text = "";

            string remark_StockCheckResult = "";

            foreach(DataRow row in DT_SUMMARY_STOCKCHECK.Rows)
            {
                double balStock = double.TryParse(row[text.Header_BalStock].ToString(), out balStock)? balStock: 0;

                if(balStock < 0)
                {
                    string itemDescription = row[text.Header_ItemDescription].ToString();

                    remark_StockCheckResult += "[" + itemDescription + "   " + balStock + "] ";
                }
            }

            txtRemark.Text = remark_StockCheckResult;
        
            #endregion

        }

        private void ItemPanelUISetting(int itemCount)
        {
            Color labelTitleColor = Color.DimGray;
            Color labelInfoColor = Color.FromArgb(64, 64, 64);
            Color DividerColor = Color.Gainsboro;
            Color TransparentColor = Color.Transparent;
            Size = new Size(814, 900);

            if (itemCount == 1)
            {
                //item 2 trasparent
                panelDividerUpItem2.BackColor = TransparentColor;
                panelDividerMidItem2.BackColor = TransparentColor;
                panelDividerbtmItem2.BackColor = TransparentColor;

                lblTitleItem2.ForeColor = TransparentColor;
                lblTitleDescriptionItem2.ForeColor = TransparentColor;
                lblTitlePWShotItem2.ForeColor = TransparentColor;
                lblTitleCavityItem2.ForeColor = TransparentColor;
                lblTitleTargetItem2.ForeColor = TransparentColor;
                lblTitleMaxQtyItem2.ForeColor = TransparentColor;
                lblTitlePurposeItem2.ForeColor = TransparentColor;

                lblDescriptionItem2.ForeColor = TransparentColor;
                lblPWPerShotItem2.ForeColor = TransparentColor;
                lblCavityItem2.ForeColor = TransparentColor;
                lblTargetQtyItem2.ForeColor = TransparentColor;
                lblMaxQtyItem2.ForeColor = TransparentColor;
                lblJobPurposeItem2.ForeColor = TransparentColor;


                //item 3 trasparent
                panelDividerUpItem3.BackColor = TransparentColor;
                panelDividerMidItem3.BackColor = TransparentColor;
                panelDividerbtmItem3.BackColor = TransparentColor;
                lblTitleItem3.ForeColor = TransparentColor;
                lblTitleDescriptionItem3.ForeColor = TransparentColor;
                lblDescriptionItem3.ForeColor = TransparentColor;
                lblTitlePWShotItem3.ForeColor = TransparentColor;
                lblPWPerShotItem3.ForeColor = TransparentColor;
                lblTitleCavityItem3.ForeColor = TransparentColor;
                lblCavityItem3.ForeColor = TransparentColor;
                lblTitleTargetItem3.ForeColor = TransparentColor;
                lblTargetQtyItem3.ForeColor = TransparentColor;
                lblTitleMaxQtyItem3.ForeColor = TransparentColor;
                lblMaxQtyItem3.ForeColor = TransparentColor;
                lblTitlePurposeItem3.ForeColor = TransparentColor;
                lblJobPurposeItem3.ForeColor = TransparentColor;

                //item 4 trasparent
                panelDividerUpItem4.BackColor = TransparentColor;
                panelDividerMidItem4.BackColor = TransparentColor;
                panelDividerbtmItem4.BackColor = TransparentColor;
                lblTitleItem4.ForeColor = TransparentColor;
                lblTitleDescriptionItem4.ForeColor = TransparentColor;
                lblDescriptionItem4.ForeColor = TransparentColor;
                lblTitlePWShotItem4.ForeColor = TransparentColor;
                lblPWPerShotItem4.ForeColor = TransparentColor;
                lblTitleCavityItem4.ForeColor = TransparentColor;
                lblCavityItem4.ForeColor = TransparentColor;
                lblTitleTargetItem4.ForeColor = TransparentColor;
                lblTargetQtyItem4.ForeColor = TransparentColor;
                lblTitleMaxQtyItem4.ForeColor = TransparentColor;
                lblMaxQtyItem4.ForeColor = TransparentColor;
                lblTitlePurposeItem4.ForeColor = TransparentColor;
                lblJobPurposeItem4.ForeColor = TransparentColor;
            }
            if (itemCount >= 2)
            {
                panelDividerUpItem2.BackColor = DividerColor;
                panelDividerMidItem2.BackColor = DividerColor;
                panelDividerbtmItem2.BackColor = DividerColor;

                lblTitleItem2.ForeColor = Color.Black ;
                lblTitleDescriptionItem2.ForeColor = labelTitleColor;
                lblTitlePWShotItem2.ForeColor = labelTitleColor;
                lblTitleCavityItem2.ForeColor = labelTitleColor;
                lblTitleTargetItem2.ForeColor = labelTitleColor;
                lblTitleMaxQtyItem2.ForeColor = labelTitleColor;
                lblTitlePurposeItem2.ForeColor = labelTitleColor;

                lblDescriptionItem2.ForeColor = labelInfoColor;
                lblPWPerShotItem2.ForeColor = labelInfoColor;
                lblCavityItem2.ForeColor = labelInfoColor;
                lblTargetQtyItem2.ForeColor = labelInfoColor;
                lblMaxQtyItem2.ForeColor = labelInfoColor;
                lblJobPurposeItem2.ForeColor = labelInfoColor;
            }
            if (itemCount >= 3)
            {
                Size = new Size(1046, 900);
                panelDividerUpItem3.BackColor = DividerColor;
                panelDividerMidItem3.BackColor = DividerColor;
                panelDividerbtmItem3.BackColor = DividerColor;

                lblTitleItem3.ForeColor = Color.Black; 
                lblTitleDescriptionItem3.ForeColor = labelTitleColor;
                lblTitlePWShotItem3.ForeColor = labelTitleColor;
                lblTitleCavityItem3.ForeColor = labelTitleColor;
                lblTitleTargetItem3.ForeColor = labelTitleColor;
                lblTitleMaxQtyItem3.ForeColor = labelTitleColor;
                lblTitlePurposeItem3.ForeColor = labelTitleColor;

                lblDescriptionItem3.ForeColor = labelInfoColor;
                lblPWPerShotItem3.ForeColor = labelInfoColor;
                lblCavityItem3.ForeColor = labelInfoColor;
                lblTargetQtyItem3.ForeColor = labelInfoColor;
                lblMaxQtyItem3.ForeColor = labelInfoColor;
                lblJobPurposeItem3.ForeColor = labelInfoColor;

            }
            if (itemCount >= 4)
            {
                Size = new Size(1317, 900);
                panelDividerUpItem4.BackColor = DividerColor;
                panelDividerMidItem4.BackColor = DividerColor;
                panelDividerbtmItem4.BackColor = DividerColor;

                lblTitleItem4.ForeColor = Color.Black;
                lblTitleDescriptionItem4.ForeColor = labelTitleColor;
                lblTitlePWShotItem4.ForeColor = labelTitleColor;
                lblTitleCavityItem4.ForeColor = labelTitleColor;
                lblTitleTargetItem4.ForeColor = labelTitleColor;
                lblTitleMaxQtyItem4.ForeColor = labelTitleColor;
                lblTitlePurposeItem4.ForeColor = labelTitleColor;

                lblDescriptionItem4.ForeColor = labelInfoColor;
                lblPWPerShotItem4.ForeColor = labelInfoColor;
                lblCavityItem4.ForeColor = labelInfoColor;
                lblTargetQtyItem4.ForeColor = labelInfoColor;
                lblMaxQtyItem4.ForeColor = labelInfoColor;
                lblJobPurposeItem4.ForeColor = labelInfoColor;
            }
           
        }

        private void JobAdd(bool NEWDraftJob)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            try
            {
                //save planning data
                DateTime date = DateTime.Now;
                BLL_JOB_SUMMARY.plan_remark = txtRemark.Text;

                if(NEWDraftJob)
                {
                    BLL_JOB_SUMMARY.plan_status = text.planning_status_draft;
                }
                else
                {
                    BLL_JOB_SUMMARY.plan_status = text.planning_status_pending;
                }

                BLL_JOB_SUMMARY.plan_added_date = date;
                BLL_JOB_SUMMARY.plan_added_by = MainDashboard.USER_ID;

                int jobID = 0;
                int familyWith = -1;

                if (DT_SUMMARY_ITEM?.Rows.Count > 0)
                {
                    bool familyMould = DT_SUMMARY_ITEM.Rows.Count > 1;

                    foreach (DataRow row in DT_SUMMARY_ITEM.Rows)
                    {

                        string itemDescription = row[text.Header_ItemDescription].ToString();
                        string itemCode = row[text.Header_ItemCode].ToString();

                        if (!string.IsNullOrEmpty(itemCode))
                        {
                            BLL_JOB_SUMMARY.plan_item_pw = row[text.Header_ProPwShot].ToString();

                            BLL_JOB_SUMMARY.plan_item_cavity = row[text.Header_Cavity].ToString();

                            BLL_JOB_SUMMARY.part_code = row[text.Header_ItemCode].ToString();

                            BLL_JOB_SUMMARY.production_purpose = row[text.Header_Job_Purpose].ToString();

                            BLL_JOB_SUMMARY.production_target_qty = row[text.Header_TargetQty].ToString();

                            BLL_JOB_SUMMARY.part_name = row[text.Header_ItemName].ToString();

                            int cavity = int.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                            int maxShot = int.TryParse(lblMouldMaxShot.Text, out maxShot) ? maxShot : 0;

                            BLL_JOB_SUMMARY.production_able_produce_qty = (maxShot * cavity).ToString();


                            BLL_JOB_SUMMARY.family_with = familyWith;

                            if(familyMould)
                            {
                                if(familyWith == -1)
                                {
                                    jobID = dalPlanningAction.NewplanningAdd(BLL_JOB_SUMMARY);

                                    
                                   
                                    //update latest family with
                                    familyWith = jobID;

                                    PlanningBLL updateFamily = new PlanningBLL();
                                    updateFamily.plan_id = familyWith;
                                    updateFamily.plan_remark = txtRemark.Text + text.planning_Family_mould_Remark;
                                    updateFamily.family_with = familyWith;
                                    updateFamily.plan_updated_date = date;
                                    updateFamily.plan_updated_by = MainDashboard.USER_ID;

                                    dalPlanningAction.planningFamilyWithChange(updateFamily, "-1");
                                }
                                else
                                {
                                    BLL_JOB_SUMMARY.plan_remark = txtRemark.Text + text.planning_Family_mould_Remark;

                                    jobID = dalPlanningAction.NewplanningAdd(BLL_JOB_SUMMARY);

                                  
                                }

                               
                            }
                            else
                            {
                                jobID = dalPlanningAction.NewplanningAdd(BLL_JOB_SUMMARY);

                            }

                            if (jobID > 0)
                            {
                                JOB_ADDED = true;
                                JOB_ADDED_COUNT++;
                            }
                            else
                            {
                                JOB_ADDED = false;
                            }

                        }
                    }
                }
              

                if(JOB_ADDED)
                {
                    tool.historyRecord(text.System, "Planning Data Saved", date, MainDashboard.USER_ID);

                    //save material plan to use qty
                    if (DT_SUMMARY_STOCKCHECK.Rows.Count > 0)
                    {
                        //get planID for this planning

                        if (jobID > 0)
                        {
                            if(familyWith > 0)
                            {
                                jobID = familyWith;
                            }

                            foreach (DataRow row in DT_SUMMARY_STOCKCHECK.Rows)
                            {
                                string matCode = row[text.Header_ItemCode].ToString();
                                float planToUse = float.TryParse(row[text.Header_RequiredForCurrentJob].ToString(), out planToUse) ? planToUse : 0;

                                uMatPlan.mat_code = matCode;
                                uMatPlan.plan_id = jobID;
                                uMatPlan.plan_to_use = planToUse;
                                uMatPlan.mat_used = 0;
                                uMatPlan.active = true;
                                uMatPlan.mat_note = "";
                                uMatPlan.updated_date = date;
                                uMatPlan.updated_by = MainDashboard.USER_ID;

                                if (!dalMatPlan.Insert(uMatPlan))
                                {
                                    //MessageBox.Show("Failed to insert material plan data.");
                                    tool.historyRecord(text.System, "Failed to insert material plan data.", date, MainDashboard.USER_ID);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Job ID not found! Cannot save material plan data.");
                            tool.historyRecord(text.System, "Job ID not found! Cannot save material plan data.", date, MainDashboard.USER_ID);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Failed to save planning data");
                    tool.historyRecord(text.System, "Failed to save planning data", date, MainDashboard.USER_ID);
                }
            }
           
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                MessageBox.Show(JOB_ADDED_COUNT + " Job(s) Added.");
                Close();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnJobAdd_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Confirm to add this job?", "Message",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                JobAdd(false);
            }
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you confirm to add this job as a Draft?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                JobAdd(true);
            }
        }
    }
}
