using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanningVer2dot1 : Form
    {
        #region Variable Declare
        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        Text text = new Text();
        matPlanDAL dalmatPlan = new matPlanDAL();
        PlanningBLL uPlanning = new PlanningBLL();
        planningDAL dalPlanning = new planningDAL();
        MacDAL dalMac = new MacDAL();

        habitDAL dalHabit = new habitDAL();
        habitBLL uHabit = new habitBLL();

        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();

        planningActionDAL dalPlanAction = new planningActionDAL();
        itemForecastDAL dalItemForecast = new itemForecastDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();

        dataTrfBLL uData = new dataTrfBLL();

        private DataTable DT_ITEM;
        private DataTable DT_MOULD_ITEM;
        private DataTable DT_MACHINE_SCHEDULE;
        private DataTable DT_ACTIVE_JOB;
        private DataTable DT_ITEM_LIST;

        private bool CMB_CODE_READY = false;
        private bool PRO_INFO_LOADING = false;
        private bool MATERIAL_STOCK_ENOUGH = true;

        readonly private string BTN_INFO_SAVE_CONFIRM_MOULD = "INFO SAVE & MOULD CONFIRM";
        readonly private string BTN_CONFIRM_MOULD = "MOULD CONFIRM";

        private int rowIndexFromMouseDown = -1;
        private int rowIndexOfItemUnderMouseToDrop = -1;

        private int MIN_SHOT = 0;

        private bool PRO_INFO_CHANGE = false;

        private string CALL_WITH_ITEM_CODE = null;
        private string CALL_WITH_JOB_NO = null;
        private int CALL_WITH_TARGET_QTY = -1;

        private bool JOB_ADDING_MODE = true;
        private bool JOB_EDITING_MODE = false;
        private string EDITING_JOB_NO = null;
        private int CURRENT_STEP = 1;

        #endregion



        public frmPlanningVer2dot1()
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();

            JOB_ADDING_MODE = true;
            JOB_EDITING_MODE = false;

            EDITING_JOB_RUNNING = false;

        }

        public frmPlanningVer2dot1(string itemCode)
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
            CALL_WITH_ITEM_CODE = itemCode;

            JOB_ADDING_MODE = true;
            JOB_EDITING_MODE = false;

            EDITING_JOB_RUNNING = false;

        }

        private DataTable DT_JOBEDITING_MAC_SCHEDULE;
        private DataRow DT_JOBEDITING_ROW;
        private int EDITING_ROW_INDEX = -1;

        public frmPlanningVer2dot1(DataTable dt_MacSchedule, int rowIndex)
        {
            InitializeComponent();
            InitialSetting();

            LoadDB();

            CALL_WITH_ITEM_CODE = "";
            DT_JOBEDITING_MAC_SCHEDULE = dt_MacSchedule;
            EDITING_ROW_INDEX = rowIndex;
            DT_JOBEDITING_ROW = DT_JOBEDITING_MAC_SCHEDULE.Rows[EDITING_ROW_INDEX];

            EDITING_JOB_NO = DT_JOBEDITING_ROW[text.Header_JobNo].ToString();

            JOB_ADDING_MODE = false;
            JOB_EDITING_MODE = true;
            EDITING_JOB_RUNNING = false;

        }


        public frmPlanningVer2dot1(string reference, bool jobEdit)
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
            
            if(jobEdit)
            {
                CALL_WITH_JOB_NO = reference;
                CALL_WITH_ITEM_CODE = "";

                JOB_ADDING_MODE = false;
                JOB_EDITING_MODE = true;
                EDITING_JOB_RUNNING = false;

            }
            else
            {
                CALL_WITH_ITEM_CODE = reference;
                CALL_WITH_JOB_NO = "";

                JOB_ADDING_MODE = true;
                JOB_EDITING_MODE = false;
                EDITING_JOB_RUNNING = false;

            }
        }

        public frmPlanningVer2dot1(string itemCode, int targetQty)
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();

            CALL_WITH_ITEM_CODE = itemCode;
            CALL_WITH_TARGET_QTY = targetQty;

            JOB_ADDING_MODE = true;
            JOB_EDITING_MODE = false;
            EDITING_JOB_RUNNING = false;

        }

        private void AutoLoadPageWithItemCode()
        {
            AddItemToList(CALL_WITH_ITEM_CODE);

            if(dgvItemList?.Rows.Count > 0 && CALL_WITH_TARGET_QTY > 0)
            {
                foreach(DataGridViewRow row in dgvItemList.Rows)
                {
                    string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();

                    if(itemCode == CALL_WITH_ITEM_CODE)
                    {
                        row.Cells[text.Header_TargetQty].Value = CALL_WITH_TARGET_QTY;

                        break;
                    }
                }
            }
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Fac, typeof(string));
            dt.Columns.Add(text.Header_MacName, typeof(string));
            dt.Columns.Add(text.Header_Status, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_RawMat_1, typeof(string));
            dt.Columns.Add(text.Header_ColorMat, typeof(string));
            dt.Columns.Add(text.Header_Color, typeof(string));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            dt.Columns.Add(text.Header_Ori_Status, typeof(string));
            dt.Columns.Add(text.Header_Ori_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_Ori_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_JobNo, typeof(int));
            dt.Columns.Add(text.Header_FacID, typeof(string));
            dt.Columns.Add(text.Header_MacID, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ColorMatCode, typeof(string));

            dt.Columns.Add(text.Header_FamilyWithJobNo, typeof(int));
            dt.Columns.Add(text.Header_ProductionDay, typeof(int));
            dt.Columns.Add(text.Header_ProductionHour, typeof(double));
            dt.Columns.Add(text.Header_ProductionHourPerDay, typeof(int));


            return dt;
        }
        private DataTable NewItemList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_JobNo, typeof(string));
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ProTon, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(string));
            dt.Columns.Add(text.Header_ProPwShot, typeof(string));
            dt.Columns.Add(text.Header_ProRwShot, typeof(string));
            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_AutoQtyAdjustment, typeof(int));
            dt.Columns.Add(text.Header_Job_Purpose, typeof(string));
            dt.Columns.Add(text.Header_FamilyWithJobNo, typeof(string));

            return dt;
        }

        private DataTable NewRawMaterialList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Selection, typeof(bool));

            dt.Columns.Add(text.Header_Index, typeof(int));

            dt.Columns.Add(text.Header_ItemDescription, typeof(string));

            dt.Columns.Add(text.Header_ItemCode, typeof(string));

            dt.Columns.Add(text.Header_ItemName, typeof(string));

            dt.Columns.Add(text.Header_Ratio, typeof(double));

            dt.Columns.Add(text.Header_RoundUp_ToBag, typeof(bool));

            dt.Columns.Add(text.Header_KGPERBAG, typeof(decimal));

            dt.Columns.Add(text.Header_Qty_Required_KG, typeof(decimal));

            dt.Columns.Add(text.Header_Qty_Required_Bag, typeof(decimal));

            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private DataTable NewStockCheckTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Type, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));
            dt.Columns.Add(text.Header_ReservedForOtherJobs, typeof(float));
            dt.Columns.Add(text.Header_RequiredForCurrentJob, typeof(float));
            dt.Columns.Add(text.Header_BalStock, typeof(float));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            return dt;

        }
       

        #region UI/UX

        private void InitialSetting()
        {

            Size = new Size(1500, 930);

            CURRENT_STEP = 1;

            StepUIUpdate(CURRENT_STEP, false);

            MIN_SHOT = 0;

            tool.DoubleBuffered(dgvMacSchedule, true);

            CALL_WITH_ITEM_CODE = null;
            CALL_WITH_TARGET_QTY = -1;

            BLL_JOB_SUMMARY = new PlanningBLL();
            DT_SUMMARY_ITEM = null;
            DT_SUMMARY_RAW = null;
            DT_SUMMARY_STOCKCHECK = null;
            DT_SUMMARY_MAC_SCHEDULE = null;
        }

        private DataTable DT_SUMMARY_ITEM = null;
        private DataTable DT_SUMMARY_RAW = null;
        private DataTable DT_SUMMARY_STOCKCHECK = null;
        private DataTable DT_SUMMARY_MAC_SCHEDULE = null;
        private PlanningBLL BLL_JOB_SUMMARY = new PlanningBLL();
        private facDAL dalFac = new facDAL();
        private string COLOR_MAT_CODE = "";

        private bool Validation(int currentStep)
        {
           
            if(currentStep == 1)
            {
                //item, raw mat, color mat, pro. info, recycle mat
                if (dgvItemList?.Rows.Count > 0)
                {
                    DT_SUMMARY_ITEM = (DataTable)dgvItemList.DataSource;

                    foreach (DataRow row in DT_SUMMARY_ITEM.Rows)
                    {
                        string purpose = row[text.Header_Job_Purpose].ToString();

                        if (string.IsNullOrEmpty(purpose))
                        {
                            MessageBox.Show("One of the items 'Job Purpose' was not found.\nPlease right-click on the Item List to set a job purpose for each item");

                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Item not found.\nPlease add at least one(1) item before adding the job.");

                    return false;
                }

                if (dgvRawMatList?.Rows.Count > 0)
                {
                    DT_SUMMARY_RAW = (DataTable)dgvRawMatList.DataSource;

                    bool selected = false;

                    foreach (DataRow row in DT_SUMMARY_RAW.Rows)
                    {
                        bool selection = bool.TryParse(row[text.Header_Selection].ToString(), out selection) ? selection : false;

                        if (selection)
                        {
                            selected = true;
                            break;
                        }
                    }

                    if (!selected)
                    {
                        MessageBox.Show("Please select at least one(1) raw material before adding the job.");

                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Raw material not found.\nPlease add and select at least one(1) raw material before adding the job.");

                    return false;
                }

                int totalCavity = int.TryParse(lblMouldCavity.Text, out totalCavity) ? totalCavity : 0;

                if (totalCavity == 0)
                {
                    MessageBox.Show("Mould Cavity cannot be 0.");

                    return false;
                }

                int proCycleTime = int.TryParse(txtMouldCycleTime.Text, out proCycleTime) ? proCycleTime : 0;

                if (proCycleTime == 0)
                {
                    MessageBox.Show("Mould Cycle Time cannot be 0.");

                    return false;
                }

                double totalPWPerShot = double.TryParse(lblMouldPWPerShot.Text, out totalPWPerShot) ? totalPWPerShot : 0;
                double totalRWPerShot = double.TryParse(txtMouldRWPerShot.Text, out totalRWPerShot) ? totalRWPerShot : 0;

                if (totalPWPerShot + totalRWPerShot == 0)
                {
                    MessageBox.Show("Total weight per shot cannot be 0.");

                    return false;
                }

                int proDay = int.TryParse(lblDaysNeeded.Text, out proDay) ? proDay : 0;
                double proBalHrs = double.TryParse(lblbalHours.Text, out proBalHrs) ? proBalHrs : 0;
                int hrsPerDay = int.TryParse(txtHrsPerDay.Text, out hrsPerDay) ? hrsPerDay : 0;

                int maxShot = int.TryParse(lblMaxShot.Text, out maxShot) ? maxShot : 0;

                if (maxShot == 0)
                {
                    MessageBox.Show("Mould Max Shot cannot be 0.");

                    return false;
                }


                double totalRaw_kg = double.TryParse(lblRawMat.Text, out totalRaw_kg) ? totalRaw_kg : 0;
                double totalColor_kg = double.TryParse(lblColorMat.Text, out totalColor_kg) ? totalColor_kg : 0;
                double totalRecycle_kg = double.TryParse(lblRecycleMat.Text, out totalRecycle_kg) ? totalRecycle_kg : 0;

                if (totalRaw_kg + totalColor_kg + totalRecycle_kg == 0)
                {
                    MessageBox.Show("Material cannot be 0.");

                    return false;
                }


                BLL_JOB_SUMMARY.plan_mould_code = lblMouldCode.Text;
                BLL_JOB_SUMMARY.plan_mould_ton = lblMouldTon.Text;
                BLL_JOB_SUMMARY.plan_cavity = totalCavity.ToString();
                BLL_JOB_SUMMARY.plan_ct = proCycleTime.ToString();
                BLL_JOB_SUMMARY.plan_pw_shot = totalPWPerShot.ToString();
                BLL_JOB_SUMMARY.plan_pw = totalPWPerShot.ToString();
                BLL_JOB_SUMMARY.plan_rw_shot = totalRWPerShot.ToString();
                BLL_JOB_SUMMARY.plan_rw = totalRWPerShot.ToString();
                BLL_JOB_SUMMARY.production_day = proDay.ToString();
                BLL_JOB_SUMMARY.production_hour = proBalHrs.ToString();
                BLL_JOB_SUMMARY.production_hour_per_day = hrsPerDay.ToString();
                BLL_JOB_SUMMARY.production_max_shot = maxShot.ToString();
                BLL_JOB_SUMMARY.raw_material_qty_kg = totalRaw_kg.ToString();
                BLL_JOB_SUMMARY.raw_material_qty = totalRaw_kg.ToString();
                BLL_JOB_SUMMARY.color_material_qty_kg = totalColor_kg.ToString();
                BLL_JOB_SUMMARY.color_material_qty = totalColor_kg.ToString();
                BLL_JOB_SUMMARY.recycle_material_qty_kg = totalRecycle_kg.ToString();
                BLL_JOB_SUMMARY.material_recycle_use = totalRecycle_kg.ToString();
                BLL_JOB_SUMMARY.color_material_code = COLOR_MAT_CODE;
                BLL_JOB_SUMMARY.part_color = txtItemColor.Text;
                BLL_JOB_SUMMARY.color_material_usage = txtColorMatUsage.Text;
                BLL_JOB_SUMMARY.use_recycle = cbRecycleExtraMode.Checked || cbRecycleSaveMode.Checked;


            }

            else if(currentStep == 2)
            {
                if (dgvStockCheck?.Rows.Count > 0)
                {
                    DT_SUMMARY_STOCKCHECK = (DataTable)dgvStockCheck.DataSource;

                    //check if stock insufficent
                    if (!MATERIAL_STOCK_ENOUGH)
                    {
                        DialogResult dialogResult = MessageBox.Show("The stock of materials is insufficient.\nAre you sure you want to proceed to machine selection?", "Message",
                                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult != DialogResult.Yes)
                        {
                            return false;
                        }
                    }

                }
              

            }

            else if(currentStep == 3)
            {
                if (dgvMacSchedule?.Rows.Count > 0)
                {
                    DT_SUMMARY_MAC_SCHEDULE = (DataTable)dgvMacSchedule.DataSource;
                }


                int macID = 0;

                if (cmbMac.SelectedIndex > -1)
                {
                    DataRowView drv = (DataRowView)cmbMac.SelectedItem;
                    macID = int.TryParse(drv[dalMac.MacID].ToString(), out macID) ? macID : 0;
                }

                if (macID == 0)
                {
                    MessageBox.Show("Please select a machine for this job.");
                    errorProvider1.SetError(lblMachineSelectionID, "Please select a machine for this job.");
                    return false;
                }


                int facID = 0;
                string facName = cmbMacLocation.Text;

                if (cmbMacLocation.SelectedIndex > -1)
                {
                    DataRowView drv = (DataRowView)cmbMacLocation.SelectedItem;
                    facID = int.TryParse(drv[dalFac.FacID].ToString(), out facID) ? facID : 0;
                }

                DateTime proStart = dtpStartDate.Value;
                DateTime proEnd = dtpEstimateEndDate.Value;

                BLL_JOB_SUMMARY.production_start_date = proStart.Date;
                BLL_JOB_SUMMARY.production_end_date = proEnd.Date;
                BLL_JOB_SUMMARY.machine_id = macID;
                BLL_JOB_SUMMARY.machine_name = facName;
                BLL_JOB_SUMMARY.machine_location = facID;

                //check if date collision
                if (DATE_COLLISION_FOUND)
                {
                    MessageBox.Show("New Draft Job date conflict detected.\nPlease adjust the dates to prevent overlap before adding the job.");

                    return false;
                }
            }

            return true;
        }

        static public bool dataSaved = false;
        planningActionDAL dalPlanningAction = new planningActionDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        matPlanDAL dalMatPlan = new matPlanDAL();

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            int smallColumnWidth = 60;

            if (dgv == dgvItemList)
            {
                dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_TargetQty].HeaderCell.Style.BackColor = Color.FromArgb(255, 153, 153);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;

                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[text.Header_Job_Purpose].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[text.Header_AutoQtyAdjustment].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_Job_Purpose].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                //dgv.Columns[text.Header_MouldCode].Width = smallColumnWidth;
                dgv.Columns[text.Header_Cavity].Width = smallColumnWidth;
                //dgv.Columns[text.Header_AutoQtyAdjustment].Width = smallColumnWidth;
                //dgv.Columns[text.Header_MouldSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ItemSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_MouldCode].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;

                dgv.Columns[text.Header_JobNo].Visible = false;
                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_MouldCode].Visible = false;

                //dgv.Columns[text.Header_Cavity].Visible = false;
                dgv.Columns[text.Header_ProTon].Visible = false;
                dgv.Columns[text.Header_ProCT].Visible = false;
                //dgv.Columns[text.Header_ProPwShot].Visible = false;
                dgv.Columns[text.Header_ProRwShot].Visible = false;
                dgv.Columns[text.Header_FamilyWithJobNo].Visible = false;

                //int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

                //if (itemListRowHeight < 150)
                //{
                //    tlpItemSelection.RowStyles[1].SizeType = SizeType.Absolute;
                //    tlpItemSelection.RowStyles[1].Height = itemListRowHeight;


                //    tlpLeftPanel.RowStyles[2].SizeType = SizeType.Absolute;
                //    tlpLeftPanel.RowStyles[2].Height = itemListRowHeight + 157;
                //}
            }
            else if (dgv == dgvRawMatList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_Qty_Required_Bag].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_Qty_Required_KG].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_KGPERBAG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_Selection].Width = smallColumnWidth;
                dgv.Columns[text.Header_RoundUp_ToBag].Width = smallColumnWidth;
                dgv.Columns[text.Header_Index].Width = smallColumnWidth;
                dgv.Columns[text.Header_Qty_Required_KG].Width = smallColumnWidth;
                dgv.Columns[text.Header_KGPERBAG].Width = smallColumnWidth;
                dgv.Columns[text.Header_Qty_Required_Bag].Width = smallColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_Remark].Visible = false;

                //int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

                //if (itemListRowHeight < 192)
                //{
                //    tlpMatSelection.RowStyles[1].SizeType = SizeType.Absolute;
                //    tlpMatSelection.RowStyles[1].Height = itemListRowHeight;
                //}

            }
            else if (dgv == dgvStockCheck)
            {
                dgv.Columns[text.Header_Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                dgv.Columns[text.Header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_Type].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgv.Columns[text.Header_Index].Width = smallColumnWidth;
                //int columnWidth = 60;

                //dgv.Columns[text.Header_ReadyStock].Width = columnWidth;
                //dgv.Columns[text.Header_ReservedForOtherJobs].Width = columnWidth;
                //dgv.Columns[text.Header_RequiredForCurrentJob].Width = columnWidth;
                //dgv.Columns[text.Header_BalStock].Width = columnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
            }
            else if (dgv == dgvMacSchedule)
            {
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_RawMat_1].MinimumWidth = 100;
                dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ColorMat].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[text.Header_ColorMat].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.Columns[text.Header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                dgv.Columns[text.Header_Status].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.Columns[text.Header_MacName].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);


                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgv.Columns[text.Header_DateStart].MinimumWidth = 120;
                dgv.Columns[text.Header_EstDateEnd].MinimumWidth = 120;


                dgv.Columns[text.Header_Fac].Width = smallColumnWidth - 10;
                dgv.Columns[text.Header_MacName].Width = smallColumnWidth - 10;
                dgv.Columns[text.Header_Status].Width = smallColumnWidth;
                //dgv.Columns[text.Header_DateStart].Width = smallColumnWidth + 5;
                //dgv.Columns[text.Header_EstDateEnd].Width = smallColumnWidth + 5;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_MacID].Visible = false;
                dgv.Columns[text.Header_FacID].Visible = false;
                dgv.Columns[text.Header_Ori_Status].Visible = false;
                dgv.Columns[text.Header_Ori_DateStart].Visible = false;
                dgv.Columns[text.Header_Ori_EstDateEnd].Visible = false;
                //dgv.Columns[text.Header_Remark].Visible = false;
                dgv.Columns[text.Header_ColorMatCode].Visible = false;
                dgv.Columns[text.Header_JobNo].Visible = false;

                dgv.Columns[text.Header_FamilyWithJobNo].Visible = false;
                dgv.Columns[text.Header_ProductionDay].Visible = false;
                dgv.Columns[text.Header_ProductionHour].Visible = false;
                dgv.Columns[text.Header_ProductionHourPerDay].Visible = false;

            }
        }

        #endregion

        #region Data Loading

        private void IndexReset(DataGridView dgv)
        {
            int index = 1;

            if (dgv?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgv.DataSource;

                if (dt.Columns.Contains(text.Header_Index))
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        row[text.Header_Index] = index++;
                    }
                }
                else
                {
                    MessageBox.Show("Index column not found , cannot reset index.");
                }
            }


        }

        private void LoadSingleMaterialList()
        {
            lblRawMat.Text = "0";
            lblColorMat.Text = "0";
            lblRecycleMat.Text = "0";
            lblColorMatDescription.Text = "";
            COLOR_MAT_CODE = "";

            lblMouldTon.Text = "";
            lblMouldCode.Text = "";
            txtItemColor.Text = "";
            txtColorMatUsage.Text = "0";
            

            if (dgvItemList?.Rows.Count > 0)
            {
                string itemCode = dgvItemList.Rows[0].Cells[text.Header_ItemCode].Value.ToString();

                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }

                DataTable dt_RawMat = NewRawMaterialList();

                int rawIndex = 1;

                foreach (DataRow row in DT_ITEM.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {

                      

                        string rawMaterial = row[dalItem.ItemMaterial].ToString();

                        var newRawRow = dt_RawMat.NewRow();

                        newRawRow[text.Header_Selection] = true;
                        newRawRow[text.Header_Index] = rawIndex++;
                        newRawRow[text.Header_ItemDescription] = rawMaterial;
                        newRawRow[text.Header_ItemCode] = rawMaterial;
                        newRawRow[text.Header_KGPERBAG] = 25;

                        dt_RawMat.Rows.Add(newRawRow);

                     
                        dgvRawMatList.DataSource = dt_RawMat;
                        RawMatRatioAdjustment();
                        dgvUIEdit(dgvRawMatList);
                        RawMatListCellFormatting(dgvRawMatList);
                        dgvRawMatList.ClearSelection();


                        //Load Color Material Info
                        string colorMaterial = row[dalItem.ItemMBatch].ToString();
                        string colorName = row[dalItem.ItemColor].ToString();
                        decimal colorRate = decimal.TryParse(row[dalItem.ItemMBRate].ToString(), out colorRate) ? colorRate : 0;

                        if (colorRate < 1)
                        {
                            colorRate *= 100;
                        }

                        colorRate = decimal.Round(colorRate, 0);

                        lblColorMatDescription.Text = tool.getItemNameFromDataTable(DT_ITEM, colorMaterial);
                        COLOR_MAT_CODE = colorMaterial;

                        txtItemColor.Text = colorName;
                        txtColorMatUsage.Text = colorRate.ToString();

                        break;

                    }
                }

                if(dgvRawMatList.Rows.Count == 1)
                {
                    dgvRawMatList.Rows[0].Cells[text.Header_RoundUp_ToBag].Value = true;
                }
            }
            else
            {
                dgvRawMatList.DataSource = null;
                //dgvColorMatList.DataSource = null;

            }
        }

        private void LoadSingleMaterialList(string itemCode)
        {
            if (DT_ITEM == null)
            {
                DT_ITEM = dalItem.Select();
            }

            DataTable dt_RawMat = NewRawMaterialList();

            int rawIndex = 1;

            foreach (DataRow row in DT_ITEM.Rows)
            {
                if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                {

                    string rawMaterial = row[dalItem.ItemMaterial].ToString();

                    var newRawRow = dt_RawMat.NewRow();

                    newRawRow[text.Header_Selection] = true;
                    newRawRow[text.Header_Index] = rawIndex++;
                    newRawRow[text.Header_ItemDescription] = rawMaterial;
                    newRawRow[text.Header_ItemCode] = rawMaterial;
                    newRawRow[text.Header_KGPERBAG] = 25;

                    dt_RawMat.Rows.Add(newRawRow);


                    dgvRawMatList.DataSource = dt_RawMat;
                    RawMatRatioAdjustment();

                    dgvUIEdit(dgvRawMatList);
                    RawMatListCellFormatting(dgvRawMatList);
                    dgvRawMatList.ClearSelection();

                    //Load Color Material Info
                    string colorMaterial = row[dalItem.ItemMBatch].ToString();
                    string colorName = row[dalItem.ItemColor].ToString();
                    decimal colorRate = decimal.TryParse(row[dalItem.ItemMBRate].ToString(), out colorRate) ? colorRate : 0;

                    if (colorRate < 1)
                    {
                        colorRate *= 100;
                    }

                    colorRate = decimal.Round(colorRate, 0);

                    lblColorMatDescription.Text = tool.getItemNameFromDataTable(DT_ITEM, colorMaterial);
                    COLOR_MAT_CODE = colorMaterial;
                    txtItemColor.Text = colorName;
                    txtColorMatUsage.Text = colorRate.ToString();


                    break;

                }
            }

            if (dgvRawMatList.Rows.Count == 1)
            {
                dgvRawMatList.Rows[0].Cells[text.Header_RoundUp_ToBag].Value = true;
            }
        }

        private void LoadMaterialFormulaList(string itemCode)
        {
            if (DT_ITEM == null)
            {
                DT_ITEM = dalItem.Select();
            }

            DataTable dt_RawMat = NewRawMaterialList();

            DataTable dt_MatFormula = dalItem.ItemMaterialFormulaSelect();

            int rawIndex = 1;
            string rawMaterialCode = "";

            foreach (DataRow row in DT_ITEM.Rows)
            {
                if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                {

                    string rawMaterial = row[dalItem.ItemMaterial].ToString();
                    rawMaterialCode = rawMaterial;

                    var newRawRow = dt_RawMat.NewRow();

                    newRawRow[text.Header_Selection] = true;
                    newRawRow[text.Header_Index] = rawIndex++;
                    newRawRow[text.Header_ItemDescription] = rawMaterial;
                    newRawRow[text.Header_ItemCode] = rawMaterial;
                    newRawRow[text.Header_KGPERBAG] = 25;

                    dt_RawMat.Rows.Add(newRawRow);


                    

                    //Load Color Material Info
                    string colorMaterial = row[dalItem.ItemMBatch].ToString();
                    string colorName = row[dalItem.ItemColor].ToString();
                    decimal colorRate = decimal.TryParse(row[dalItem.ItemMBRate].ToString(), out colorRate) ? colorRate : 0;

                    if (colorRate < 1)
                    {
                        colorRate *= 100;
                    }

                    colorRate = decimal.Round(colorRate, 0);

                    lblColorMatDescription.Text = tool.getItemNameFromDataTable(DT_ITEM, colorMaterial);
                    COLOR_MAT_CODE = colorMaterial;
                    txtItemColor.Text = colorName;
                    txtColorMatUsage.Text = colorRate.ToString();


                    break;

                }
            }


            //get material formula data
            foreach (DataRow row in dt_MatFormula.Rows)
            {
                if(itemCode == row[dalItem.ItemCode].ToString())
                {
                    if(rawMaterialCode != row[dalItem.MatCode].ToString())
                    {
                        var newRawRow = dt_RawMat.NewRow();

                        newRawRow[text.Header_Selection] = true;
                        newRawRow[text.Header_Index] = rawIndex++;
                        newRawRow[text.Header_ItemDescription] = row[dalItem.MatCode].ToString();
                        newRawRow[text.Header_ItemCode] = row[dalItem.MatCode].ToString();
                        newRawRow[text.Header_KGPERBAG] = string.IsNullOrEmpty(row[dalItem.KGPerBag].ToString())? "25" : row[dalItem.KGPerBag].ToString();
                        newRawRow[text.Header_Ratio] = row[dalItem.MatRatio].ToString();

                        dt_RawMat.Rows.Add(newRawRow);
                    }
                    else
                    {
                        foreach(DataRow rowRaw in dt_RawMat.Rows)
                        {
                            if (rawMaterialCode == rowRaw[text.Header_ItemCode].ToString())
                            {
                                rowRaw[text.Header_KGPERBAG] = string.IsNullOrEmpty(row[dalItem.KGPerBag].ToString()) ? "25" : row[dalItem.KGPerBag].ToString();
                                rowRaw[text.Header_Ratio] = row[dalItem.MatRatio].ToString();
                            }
                        }
                    }
                }
            }

            dgvRawMatList.DataSource = dt_RawMat;
            RawMatRatioAdjustment();

            dgvUIEdit(dgvRawMatList);
            RawMatListCellFormatting(dgvRawMatList);
            dgvRawMatList.ClearSelection();

            if (dgvRawMatList.Rows.Count >= 1)
            {
                dgvRawMatList.Rows[0].Cells[text.Header_RoundUp_ToBag].Value = true;
            }
        }

        private void LoadEditingMaterialList()
        {
            MATERIAL_DATA_UPDATING = true;

            DataTable dt_RawMat = NewRawMaterialList();

            string rawMaterial = DT_JOBEDITING_ROW[text.Header_RawMat_1].ToString();

            int index = 1;
            if(!string.IsNullOrEmpty(rawMaterial))
            {
                DataRow newRawRow = dt_RawMat.NewRow();

                newRawRow[text.Header_Selection] = true;
                newRawRow[text.Header_Index] = index++;
                newRawRow[text.Header_ItemDescription] = DT_JOBEDITING_ROW[text.Header_RawMat_1];
                newRawRow[text.Header_ItemCode] = DT_JOBEDITING_ROW[text.Header_RawMat_1];

                //double ratio = double.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_1_Ratio].ToString(), out double d) ? d : 100;

                newRawRow[text.Header_Ratio] = DT_JOBEDITING_ROW[text.Header_RawMat_1_Ratio];
                newRawRow[text.Header_RoundUp_ToBag] = false;
                newRawRow[text.Header_Qty_Required_KG] = decimal.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_KG_1].ToString(), out decimal d)? d : 0;
                newRawRow[text.Header_Qty_Required_Bag] = decimal.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_Bag_1].ToString(), out  d) ? d : 0;

                int bag = int.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_Bag_1].ToString(), out bag) ? bag : 0;
                double kg = double.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_KG_1].ToString(), out kg) ? kg : 0;

                int kgPerBag = 25;

                if (bag > 0)
                {
                    kgPerBag = (int)kg / bag;
                }

                newRawRow[text.Header_KGPERBAG] = kgPerBag;

                dt_RawMat.Rows.Add(newRawRow);
            }

            rawMaterial = DT_JOBEDITING_ROW[text.Header_RawMat_2].ToString();

            if (!string.IsNullOrEmpty(rawMaterial))
            {
                DataRow newRawRow = dt_RawMat.NewRow();

                newRawRow[text.Header_Selection] = true;
                newRawRow[text.Header_Index] = index++;
                newRawRow[text.Header_ItemDescription] = DT_JOBEDITING_ROW[text.Header_RawMat_2];
                newRawRow[text.Header_ItemCode] = DT_JOBEDITING_ROW[text.Header_RawMat_2];

                newRawRow[text.Header_Ratio] = DT_JOBEDITING_ROW[text.Header_RawMat_2_Ratio];
                newRawRow[text.Header_RoundUp_ToBag] = false;
                newRawRow[text.Header_Qty_Required_KG] = decimal.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_KG_2].ToString(), out decimal d) ? d : 0;
                newRawRow[text.Header_Qty_Required_Bag] = decimal.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_Bag_2].ToString(), out d) ? d : 0;

                int bag = int.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_Bag_2].ToString(), out bag) ? bag : 0;
                double kg = double.TryParse(DT_JOBEDITING_ROW[text.Header_RawMat_KG_2].ToString(), out kg) ? kg : 0;

                int kgPerBag = 25;

                if (bag > 0)
                {
                    kgPerBag = (int)kg / bag;
                }

                newRawRow[text.Header_KGPERBAG] = kgPerBag;

                dt_RawMat.Rows.Add(newRawRow);
            }

            dgvRawMatList.DataSource = dt_RawMat;
            //RawMatRatioAdjustment();

            dgvUIEdit(dgvRawMatList);
            RawMatListCellFormatting(dgvRawMatList);
            dgvRawMatList.ClearSelection();

            //Load Color Material Info
            string colorMaterial = DT_JOBEDITING_ROW[text.Header_ColorMatCode].ToString();
            string colorName = DT_JOBEDITING_ROW[text.Header_Color].ToString();
            decimal colorRate = decimal.TryParse(DT_JOBEDITING_ROW[text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;

            if (colorRate < 1)
            {
                colorRate *= 100;
            }

            colorRate = decimal.Round(colorRate, 0);

            lblColorMatDescription.Text = tool.getItemNameFromDataTable(DT_ITEM, colorMaterial);
            COLOR_MAT_CODE = colorMaterial;
            txtItemColor.Text = colorName;
            txtColorMatUsage.Text = colorRate.ToString();

            if (dgvRawMatList.Rows.Count == 1)
            {
                dgvRawMatList.Rows[0].Cells[text.Header_RoundUp_ToBag].Value = true;
            }

            MATERIAL_DATA_UPDATING = false;

        }

        private readonly string DayLabelTitle = "Day";

        #region Calculation
        private void FromMaxShotToTimeNeededCalculation()
        {
            int cycleTime_PerShot_sec = int.TryParse(txtMouldCycleTime.Text, out cycleTime_PerShot_sec) ? cycleTime_PerShot_sec : 0;
            int MaxShots = int.TryParse(lblMaxShot.Text, out MaxShots) ? MaxShots : 0;

            int Max_CycleTime_sec = MaxShots * cycleTime_PerShot_sec;

            lblProSec.Text = "(" + Max_CycleTime_sec + "s)";

            double workingHrsPerday = double.TryParse(txtHrsPerDay.Text, out workingHrsPerday) ? workingHrsPerday : 22;

            int totalDays = (int)(Max_CycleTime_sec / 60 / 60 / workingHrsPerday);

            double balHrs = (double)Max_CycleTime_sec / 60 / 60 - (totalDays * workingHrsPerday);

            lblDays.Text = DayLabelTitle + (totalDays > 1 ? "s" : "") + " (" + workingHrsPerday + "hrs)";

            lblDaysNeeded.Text = totalDays.ToString();
            lblbalHours.Text = balHrs.ToString("0.##");

        }

        private bool MAX_SHOT_CALCULATING = false;

        private void CalculateMaxShot()
        {
            if(MAX_SHOT_CALCULATING)
            {
                return;
            }

            MAX_SHOT_CALCULATING = true;

            #region Step 1: get Min Shots

            //min shots calculate from user's target qty (minShot = target qty/ mould cavity), done at other stage and save it into MIN_SHOT

            if(MIN_SHOT <= 0)
            {
                refreshCavityMatchedQty();
            }

            int minShots = MIN_SHOT;

            if(minShots < 0)
            {
                minShots = 0;
            }

            #endregion

            #region Step 2: get Min Material Needed

            //part weight per shot in gram
            double Mould_PW_Shot_g = double.TryParse(lblMouldPWPerShot.Text, out Mould_PW_Shot_g) ? Mould_PW_Shot_g : 0;

            //runner weight per shot in gram
            double Mould_RW_Shot_g = double.TryParse(txtMouldRWPerShot.Text, out Mould_RW_Shot_g) ? Mould_RW_Shot_g : 0;

            //total material weight needed per shot
            double Mould_Total_Weight_Shot_g = Mould_PW_Shot_g + Mould_RW_Shot_g;

            // new material wastage percentage
            double newMatWastage = double.TryParse(txtMatWastage.Text, out newMatWastage) ? newMatWastage : 0;

            //if user key in 1%, we convert to 0.01, else if user key in 0.01, then we direct use 0.01 to do calculation
            newMatWastage = newMatWastage < 1 ? newMatWastage : newMatWastage / 100;

            //CASE 1 , WASTAGE = 0, RECYCLE OFF ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            newMatWastage = 0;

            //calculate min material needed base on user's target min shots, and add wastage material to ensure production have enough material to met the min shots target
            double minMaterialNeeded_g = CalculateMinMaterialNeeded_Gram(minShots, Mould_Total_Weight_Shot_g, newMatWastage);

            #endregion

            #region Step 3: get Max Material Needed & Max Shots

            int maxShots = minShots;

            bool RawMat_RoundUpToBag = false;
            double MaterialperBag_KG = 25;
            double MainRawRatio = 100;

            int selectedRawCount = 0;

            if (dgvRawMatList?.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dgvRawMatList.Rows)
                {
                    bool selection = bool.TryParse(row.Cells[text.Header_Selection].Value.ToString(), out selection) ? selection : false;
                    bool roundUpToBag = bool.TryParse(row.Cells[text.Header_RoundUp_ToBag].Value.ToString(), out roundUpToBag) ? roundUpToBag : false;
                        

                    if (selection && roundUpToBag)
                    {
                        selectedRawCount++;

                        RawMat_RoundUpToBag = true;

                        MainRawRatio = double.TryParse(row.Cells[text.Header_Ratio].Value.ToString(), out MainRawRatio) ? MainRawRatio : 100;

                        MaterialperBag_KG = double.TryParse(row.Cells[text.Header_KGPERBAG].Value.ToString(), out MaterialperBag_KG) ? MaterialperBag_KG : 25;

                        break;
                    }
                }
            }

            MainRawRatio = MainRawRatio >= 1 ? MainRawRatio / 100 : MainRawRatio;

            if (MainRawRatio == 0)
            {
                if (selectedRawCount == 1)
                {
                    MainRawRatio = 100;
                }
                else
                {
                    MainRawRatio = 50;
                }
            }

            double maxMaterialNeeded_g = minMaterialNeeded_g;

            //DataTable dt_RawMat = (DataTable)dgvRawMatList.DataSource;

            //if (dt_RawMat?.Rows.Count > 0 && dt_RawMat.Columns.Contains(text.Header_KGPERBAG))
            //{
            //    MaterialperBag_KG = double.TryParse(dt_RawMat.Rows[0][text.Header_KGPERBAG].ToString(), out MaterialperBag_KG) ? MaterialperBag_KG : 25;
            //}

            double colorRatio_percentage = 0;

            colorRatio_percentage = double.TryParse(txtColorMatUsage.Text, out colorRatio_percentage) ? colorRatio_percentage : 0;

            colorRatio_percentage = colorRatio_percentage < 1 ? colorRatio_percentage : colorRatio_percentage / 100;

            // rawMat + rawMat * ColorRatio = minMaterialNeeded
            // rawMat (1+ColorRatio) = minMaterialNeeded
            // rawMat = minMaterialNeeded / (1+ColorRatio)
            // R = T / (1 + P)
            double new_RawMat_g = minMaterialNeeded_g / (1 + colorRatio_percentage);
            double new_ColorMat_g = new_RawMat_g * colorRatio_percentage;

            //if(new_RawMat_g + new_ColorMat_g != minMaterialNeeded_g)
            //{
            //    MessageBox.Show("Min New Raw Mat Calculation Error:\nnew_RawMat_g + new_ColorMat_g != minMaterialNeeded_g");
            //}

            int extraShot_fromRoundUp = 0;

            if (RawMat_RoundUpToBag)
            {
                double new_RawMat_g_With_Ratio = Math.Ceiling(new_RawMat_g * MainRawRatio / 1000 / MaterialperBag_KG) * MaterialperBag_KG * 1000; 

                //Round Up Raw Mat to nearlest max bag qty
                new_RawMat_g = new_RawMat_g_With_Ratio / MainRawRatio;

                //new_RawMat_g = Math.Ceiling(new_RawMat_g / 1000 / MaterialperBag_KG) * MaterialperBag_KG * 1000;

                new_ColorMat_g = new_RawMat_g * colorRatio_percentage;

                maxMaterialNeeded_g = new_RawMat_g + new_ColorMat_g;

                //calculate extra shot can get from the round up material qty, need to apply wastage, since the minMaterialNeeded already deduct wastage,
                //so at this stage only deduct the wastage from the extra material.
                extraShot_fromRoundUp = (int)Math.Floor((maxMaterialNeeded_g - minMaterialNeeded_g) * (1 - newMatWastage) / Mould_Total_Weight_Shot_g);

                maxShots = minShots + extraShot_fromRoundUp;
            }



            //CASE 1, SET RECYCLE = OFF /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //bool RunnerRecycle = cbRecycleExtraMode.Checked;
            bool RunnerRecycle = false;

            double recycleWastage = 0;
            recycleWastage = recycleWastage < 1 ? recycleWastage : recycleWastage / 100;

            int total_ExtraShotsFromRecycle = 0;
            double total_RecycleMat_g = 0;

            if (RunnerRecycle)
            {
                // total runner material from max shots
                double totalRunner_g = Mould_RW_Shot_g * maxShots;
                double effectiveRunner_g = totalRunner_g;

                total_RecycleMat_g += effectiveRunner_g;

                // loop until the recyclable runner is less than the total weight required for a single shot
                while (effectiveRunner_g / Mould_Total_Weight_Shot_g >= 1)
                {
                    // calculate runner that can be recycled after considering wastage
                    double recyclableRunner_g = effectiveRunner_g * (1 - recycleWastage);

                    //total_RecycleMat_g += recyclableRunner_g;

                    // calculate extra shots from the recyclable runner material
                    // consider both part weight and runner weight for each shot when calculating extra shots
                    int extraShotsFromRecycle = (int)Math.Floor(recyclableRunner_g / Mould_Total_Weight_Shot_g);

                    total_ExtraShotsFromRecycle += extraShotsFromRecycle;

                    // the effective runner for the next iteration is the runner generated from the extra shots
                    effectiveRunner_g = Mould_RW_Shot_g * extraShotsFromRecycle;

                    total_RecycleMat_g += effectiveRunner_g;
                }

                // add the extra shots from recycle to the max shots
                maxShots += total_ExtraShotsFromRecycle;

            }

            #endregion

            #region Step 4: Production Time Calculation

            int cycleTime_PerShot_sec = int.TryParse(txtMouldCycleTime.Text, out cycleTime_PerShot_sec) ? cycleTime_PerShot_sec : 0;
            int cavity_PerShot_pcs = int.TryParse(lblMouldCavity.Text, out cavity_PerShot_pcs) ? cavity_PerShot_pcs : 0;

            int Max_CycleTime_sec = maxShots * cycleTime_PerShot_sec;

            double pro_TotalHours = Max_CycleTime_sec / 3600;

            double workingHrsPerday = double.TryParse(txtHrsPerDay.Text, out workingHrsPerday) ? workingHrsPerday : 22;

            int totalDays = (int)(pro_TotalHours / workingHrsPerday);

            double balHrs = (double)pro_TotalHours - (totalDays * workingHrsPerday);

            #endregion

            #region Step 5: Fill In Data

            //Production Time
            lblProSec.Text = "(" + Max_CycleTime_sec + "s)";
            lblDays.Text = DayLabelTitle + (totalDays > 1 ? "s" : "") + " (" + workingHrsPerday + "hrs)";
            lblDaysNeeded.Text = totalDays.ToString();
            lblbalHours.Text = balHrs.ToString("0.##");

            //Max Capacity
            int Max_Qty_pcs = maxShots * cavity_PerShot_pcs;
            lblMaxShot.Text = maxShots.ToString();
            lblMaxQty.Text = Max_Qty_pcs.ToString();

            //Material Summary
            lblRawMat.Text = (new_RawMat_g / 1000).ToString("0.##");
            lblColorMat.Text = (new_ColorMat_g / 1000).ToString("0.##");
            lblRecycleMat.Text = (total_RecycleMat_g / 1000).ToString("0.##");

            if (dgvRawMatList?.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dgvRawMatList.Rows)
                {
                    bool selection = bool.TryParse(row.Cells[text.Header_Selection].Value.ToString(), out selection) ? selection : false;
                    
                    if (selection)
                    {
                        double ratio = double.TryParse(row.Cells[text.Header_Ratio].Value.ToString(), out ratio) ? ratio : 0;

                        ratio = ratio < 1 ? ratio : ratio / 100;

                        double kgPerBag = double.TryParse(row.Cells[text.Header_KGPERBAG].Value.ToString(), out kgPerBag) ? kgPerBag : 0;


                        double result = new_RawMat_g * ratio / 1000;

                        // Check if the result has decimal places
                        if (result % 1 != 0)
                        {
                            // If it does, round to 2 decimal places
                            row.Cells[text.Header_Qty_Required_KG].Value = (decimal) Math.Round(result, 2);

                            //try
                            //{
                            //    row.Cells[text.Header_Qty_Required_KG].Value = (decimal)Math.Round(result, 2);
                            //}
                            //catch (OverflowException ex)
                            //{
                            //    MessageBox.Show($"An overflow exception occurred: {ex.Message}" + $"\n\nResult value: {result}");
                            //}
                        }
                        else
                        {
                            // If not, keep the original value
                            row.Cells[text.Header_Qty_Required_KG].Value = (decimal) result;
                        }

                        result = result / kgPerBag;

                        // Check if the result has decimal places
                        if (result % 1 != 0)
                        {
                            // If it does, round to 2 decimal places
                            row.Cells[text.Header_Qty_Required_Bag].Value = (decimal) Math.Round(result, 2);
                        }
                        else
                        {
                            // If not, keep the original value
                            row.Cells[text.Header_Qty_Required_Bag].Value = (decimal) result;
                        }

                       // row.Cells[text.Header_Qty_Required_Bag].Value = (int)(new_RawMat_g * ratio / 1000 / kgPerBag);
                    }
                }
               
               
            }

            if (dgvItemList?.Rows.Count > 0)
            {
                
                foreach(DataGridViewRow row in dgvItemList.Rows)
                {
                    int itemCavity = int.TryParse(row.Cells[text.Header_Cavity].Value.ToString(), out itemCavity)? itemCavity : 0;

                    int maxQty = maxShots * itemCavity;

                    row.Cells[text.Header_AutoQtyAdjustment].Value = maxQty;

                  
                }

                if(dgvItemList.Rows.Count > 1)
                {
                    lblMaxQty.Text = "Multiple items have been found.\nPlease check the Max Qty in the Item List";
                    lblMaxQty.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                    dgvItemList.Columns[text.Header_AutoQtyAdjustment].Visible = true;

                }
                else
                {
                    lblMaxQty.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
                    dgvItemList.Columns[text.Header_AutoQtyAdjustment].Visible = false;

                }
            }

            #endregion

            MAX_SHOT_CALCULATING = false;

        }


        private bool PRO_TIME_CALCULATING = false;

        private void CalculateProductionTime()
        {
            if (PRO_TIME_CALCULATING)
            {
                return;
            }

            PRO_TIME_CALCULATING = true;

            #region Step 1: Production Time Calculation

            int cycleTime_PerShot_sec = int.TryParse(txtMouldCycleTime.Text, out cycleTime_PerShot_sec) ? cycleTime_PerShot_sec : 0;
            int cavity_PerShot_pcs = int.TryParse(lblMouldCavity.Text, out cavity_PerShot_pcs) ? cavity_PerShot_pcs : 0;

            int maxShots = int.TryParse(lblMaxShot.Text, out maxShots) ? maxShots : 0;

            int Max_CycleTime_sec = maxShots * cycleTime_PerShot_sec;

            double pro_TotalHours = Max_CycleTime_sec / 3600;

            double workingHrsPerday = double.TryParse(txtHrsPerDay.Text, out workingHrsPerday) ? workingHrsPerday : 22;

            int totalDays = (int)(pro_TotalHours / workingHrsPerday);

            double balHrs = (double)pro_TotalHours - (totalDays * workingHrsPerday);

            #endregion

            #region Step 2: Fill In Data

            //Production Time
            lblProSec.Text = "(" + Max_CycleTime_sec + "s)";

            lblDays.Text = DayLabelTitle + (totalDays > 1 ? "s" : "") + " (" + workingHrsPerday + "hrs)";
            lblDaysNeeded.Text = totalDays.ToString();
            lblbalHours.Text = balHrs.ToString("0.##");
            #endregion

            PRO_TIME_CALCULATING = false;

        }

        private double CalculateMinMaterialNeeded_Gram(int minShot, double totalWeightPerShot, double wastage)
        {
            return (double) minShot * totalWeightPerShot * (1 + wastage);
        }

        #endregion

        private void AddItemToList(string itemCode)
        {
            PRO_INFO_LOADING = true;

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }


                int index = 1;

                if (dgvItemList?.Rows.Count > 0)
                {
                    DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                    //get last index
                    index = int.TryParse(dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_Index].Value.ToString(), out index) ? index++ : 1;
                }
                else
                {
                    DT_ITEM_LIST = NewItemList();
                }


                bool itemFound = false;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    if (itemCode == row[text.Header_ItemCode].ToString())
                    {
                        itemFound = true;
                        int rowIndex = DT_ITEM_LIST.Rows.IndexOf(row);

                        MessageBox.Show("Same item code found in the list (row: " + rowIndex + ").");
                        return;
                    }
                }

                if (!itemFound)
                    foreach (DataRow row in DT_ITEM.Rows)
                    {
                        if (itemCode == row[dalItem.ItemCode].ToString())
                        {
                            DT_ITEM_LIST.Rows.Add(CreateNewItemRow(row, index++));

                            itemFound = true;

                            break;
                        }
                    }

                if (itemFound)
                {
                    dgvItemList.DataSource = DT_ITEM_LIST;
                    IndexReset(dgvItemList);
                    dgvUIEdit(dgvItemList);
                    ItemListCellFormatting(dgvItemList);

                    LoadSummary_ProductionInfo();

                    dgvReadOnlyModeUpdate(dgvItemList, dgvItemList.Rows.Count - 1, dgvItemList.Columns[text.Header_TargetQty].Index);
                }
                else
                {
                    DT_ITEM = dalItem.Select();
                    AddItemToList(itemCode);
                }
            }

            PRO_INFO_LOADING = false;
            LoadMaterialFormulaList(itemCode);

            string ItemListTitle = "Item List";

            if (dgvItemList?.Rows.Count > 0)
            {
                int itemCount = dgvItemList.Rows.Count;

                if (itemCount > 1)
                {
                    ItemListTitle += " (" + itemCount + " Items)";
                }
                else
                {
                    ItemListTitle += " (" + itemCount + " Item)";

                }
            }

            lblItemListTitle.Text = ItemListTitle;
            //dgvItemList.FirstDisplayedScrollingRowIndex = 0;

        }

        private void AddItemToList(string itemCode, string jobPurpose, int targetQty)
        {
            PRO_INFO_LOADING = true;

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }

                int index = 1;

                if (dgvItemList?.Rows.Count > 0)
                {
                    DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                    //get last index
                    index = int.TryParse(dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_Index].Value.ToString(), out index) ? index++ : 1;
                }
                else
                {
                    DT_ITEM_LIST = NewItemList();
                }


                bool itemFound = false;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    if (itemCode == row[text.Header_ItemCode].ToString())
                    {
                        itemFound = true;
                        int rowIndex = DT_ITEM_LIST.Rows.IndexOf(row);

                        MessageBox.Show("Same item code found in the list (row: " + rowIndex + ").");
                        return;
                    }
                }

                if (!itemFound)
                    foreach (DataRow row in DT_ITEM.Rows)
                    {
                        if (itemCode == row[dalItem.ItemCode].ToString())
                        {
                            DT_ITEM_LIST.Rows.Add(CreateNewItemRow(row, index++, jobPurpose,targetQty));

                            itemFound = true;

                            break;
                        }
                    }

                if (itemFound)
                {
                    dgvItemList.DataSource = DT_ITEM_LIST;
                    IndexReset(dgvItemList);
                    dgvUIEdit(dgvItemList);
                    ItemListCellFormatting(dgvItemList);

                    LoadSummary_ProductionInfo();

                    dgvReadOnlyModeUpdate(dgvItemList, dgvItemList.Rows.Count - 1, dgvItemList.Columns[text.Header_TargetQty].Index);
                }
                else
                {
                    DT_ITEM = dalItem.Select();
                    AddItemToList(itemCode);
                }
            }

            PRO_INFO_LOADING = false;

            LoadMaterialFormulaList(itemCode);

            string ItemListTitle = "Item List";

            if (dgvItemList?.Rows.Count > 0)
            {
                int itemCount = dgvItemList.Rows.Count;

                if (itemCount > 1)
                {
                    ItemListTitle += " (" + itemCount + " Items)";
                }
                else
                {
                    ItemListTitle += " (" + itemCount + " Item)";

                }
            }

            lblItemListTitle.Text = ItemListTitle;
            //dgvItemList.FirstDisplayedScrollingRowIndex = 0;
            refreshCavityMatchedQty();

            dgvItemList.ClearSelection();
        }

        private Tuple<int, int> CalculateRawMaterialRatios(int numberOfRawMaterials)
        {
            if (numberOfRawMaterials <= 0)
            {
                return new Tuple<int, int>(0, 0);
            }

            // Calculate the average ratio
            int avgRatio = 100 / numberOfRawMaterials;

            // The first raw material will receive the balance after subtracting the remaining ratio
            int firstRatio = 100 - (avgRatio * (numberOfRawMaterials - 1));

            return new Tuple<int, int>(firstRatio, avgRatio);
        }


        private void RawMatRatioAdjustment()
        {
            if(dgvRawMatList?.Rows?.Count > 0) 
            {
                int selectedRowCount = 0;

                int totalPercentage = 0;

                foreach (DataGridViewRow row in dgvRawMatList.Rows)
                {
                    bool selection = bool.TryParse(row.Cells[text.Header_Selection].Value.ToString(), out selection) ? selection : false;

                    if (selection)
                    {
                        selectedRowCount++;
                        totalPercentage += int.TryParse(row.Cells[text.Header_Ratio].Value.ToString(), out int ratio) ? ratio : 0;
                    }
                }

                if(selectedRowCount == 0)
                {
                    MessageBox.Show("Need to select at least one Raw Material.");
                    dgvRawMatList.Rows[0].Cells[text.Header_Selection].Value = true;
                    selectedRowCount = 1;
                }
                else if (selectedRowCount == 1)
                {
                    foreach (DataGridViewRow row in dgvRawMatList.Rows)
                    {
                        bool selection = bool.TryParse(row.Cells[text.Header_Selection].Value.ToString(), out selection) ? selection : false;

                        if (selection)
                        {
                            row.Cells[text.Header_RoundUp_ToBag].Value = true;
                        }
                    }
                }

                int avgRatio = 0;
                int firstRatio = 0;

                if(totalPercentage != 100)
                {
                    if (selectedRowCount > 0)
                    {
                        avgRatio = 100 / selectedRowCount;

                        firstRatio = 100 - (avgRatio * (selectedRowCount - 1));
                    }


                    bool firstselectedRowDataUpdated = false;

                    for (int i = 0; i < dgvRawMatList.Rows.Count; i++)
                    {
                        bool selection = bool.TryParse(dgvRawMatList.Rows[i].Cells[text.Header_Selection].Value.ToString(), out selection) ? selection : false;

                        if (selection)
                        {
                            if (!firstselectedRowDataUpdated)
                            {
                                dgvRawMatList.Rows[i].Cells[text.Header_Ratio].Value = firstRatio;

                                firstselectedRowDataUpdated = true;
                            }
                            else
                            {
                                dgvRawMatList.Rows[i].Cells[text.Header_Ratio].Value = avgRatio;
                            }
                        }
                        else
                        {
                            dgvRawMatList.Rows[i].Cells[text.Header_Ratio].Value = 0;
                        }
                    }
                }
              

                CalculateMaxShot();
            }
          

       
        }

        private bool RAW_MAT_RATIO_UPDATING = false;

        private void RawMatRatioAdjustment_Manual(int currentRowIndex)
        {
            if(RAW_MAT_RATIO_UPDATING)
            {
                return;
            }

            RAW_MAT_RATIO_UPDATING = true;

            DataGridView dgv = dgvRawMatList;

            if (dgv?.Rows?.Count > 0)
            {
                int totalRowCount = dgv.RowCount;

                double currentRow_Ratio = double.TryParse(dgv.Rows[currentRowIndex].Cells[text.Header_Ratio].Value.ToString(), out currentRow_Ratio) ? currentRow_Ratio : 100;


                if (totalRowCount == 1)
                {
                    if(currentRow_Ratio != 100)
                    {
                        MessageBox.Show("If there is only 1 type of raw material, the ratio is fixed at 100.");
                        dgv.Rows[currentRowIndex].Cells[text.Header_Ratio].Value = 100;
                    }
                    RAW_MAT_RATIO_UPDATING = false;

                    return;

                }


                foreach (DataGridViewRow row in dgvRawMatList.Rows)
                {
                    bool selection = bool.TryParse(row.Cells[text.Header_Selection].Value.ToString(), out selection) ? selection : false;

                    if (selection && currentRowIndex != dgvRawMatList.Rows.IndexOf(row))
                    {
                        row.Cells[text.Header_Ratio].Value = 100 - currentRow_Ratio;

                    }
                }


                CalculateMaxShot();
            }

            RAW_MAT_RATIO_UPDATING = false;


        }
        private void AddRawToList(string rawCode)
        {
            PRO_INFO_LOADING = true;

            if (!string.IsNullOrEmpty(rawCode))
            {
                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }

                int index = 1;

                DataTable dt_RawList = NewRawMaterialList();

                bool itemFound = false;

                if (dgvRawMatList?.Rows.Count > 0)
                {
                    dt_RawList = (DataTable)dgvRawMatList.DataSource;

                    //get last index
                    index = int.TryParse(dgvRawMatList.Rows[dgvRawMatList.Rows.Count - 1].Cells[text.Header_Index].Value.ToString(), out index) ? index+1 : 1;
                   
                    foreach (DataRow row in dt_RawList.Rows)
                    {
                        if (rawCode == row[text.Header_ItemCode].ToString())
                        {
                            itemFound = true;
                            int rowIndex = dt_RawList.Rows.IndexOf(row);

                            MessageBox.Show("Same item code found in the list (row: " + rowIndex + ").");
                            return;
                        }
                    }
                }

                if (!itemFound)
                {
                    var newRawRow = dt_RawList.NewRow();

                    newRawRow[text.Header_Selection] = true;
                    newRawRow[text.Header_Index] = index;
                    newRawRow[text.Header_ItemDescription] = rawCode;
                    newRawRow[text.Header_ItemCode] = rawCode;
                    newRawRow[text.Header_ItemName] = tool.getItemNameFromDataTable(DT_ITEM,rawCode);
                    newRawRow[text.Header_KGPERBAG] = 25;

                    dt_RawList.Rows.Add(newRawRow);
                    dgvRawMatList.DataSource = dt_RawList;
                    RawMatRatioAdjustment();
                    dgvUIEdit(dgvRawMatList);
                    RawMatListCellFormatting(dgvRawMatList);
                    dgvRawMatList.ClearSelection();
                }
            }

            PRO_INFO_LOADING = false;
            dgvRawMatList.ClearSelection();

            CalculateMaxShot();
        }
        
           
        private void ChangeColorMat(string ColorCode)
        {
            PRO_INFO_LOADING = true;

            if (!string.IsNullOrEmpty(ColorCode))
            {
                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }

                COLOR_MAT_CODE = ColorCode;
                lblColorMatDescription.Text = tool.getItemNameFromDataTable(DT_ITEM, ColorCode);
            }

            PRO_INFO_LOADING = false;

            CalculateMaxShot();
        }
        private void dgvClearSelection()
        {
            dgvItemList.ClearSelection();
            dgvRawMatList.ClearSelection();
            dgvStockCheck.ClearSelection();
            dgvMacSchedule.ClearSelection();
        }
        private void RemoveItemFromList(DataGridView dgv, int rowIndex)
        {
            if(JOB_EDITING_MODE)
            {
                MessageBox.Show("Cannot remove item in Job Editing Mode. You should cancel this item's job through the Machine Schedule instead.");
            }
            else
            {
                if (rowIndex >= 0 && rowIndex < dgv.Rows.Count) // Ensure the index is within the valid range
                {

                    dgv.Rows.RemoveAt(rowIndex);
                    LoadSummary_ProductionInfo();
                    IndexReset(dgvItemList);

                    LoadSingleMaterialList();
                    CalculateMaxShot();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("rowIndex", "The row index provided is out of range.");
                }
            }
          
        }

        private void RemoveRawFromList(DataGridView dgv, int rowIndex)
        {
            if (JOB_EDITING_MODE)
            {
                MessageBox.Show("You cannot change or remove material in Job Editing Mode.\nIf you wish to modify the material, cancel this Job through the Machine Schedule, then add a new job with the desired material.");

            }
            else
            {
                if (rowIndex >= 0 && rowIndex < dgv.Rows.Count) // Ensure the index is within the valid range
                {
                    dgv.Rows.RemoveAt(rowIndex);

                    IndexReset(dgvRawMatList);

                    DataTable dt_RawMat = (DataTable)dgvRawMatList.DataSource;

                    dgvRawMatList.DataSource = dt_RawMat;
                    RawMatRatioAdjustment();

                    CalculateMaxShot();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("rowIndex", "The row index provided is out of range.");
                }
            }
           
        }
        private string JOB_PURPOSE = "";

        private void EditJobPurpose(DataGridView dgv, int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count) // Ensure the index is within the valid range
            {
                JOB_PURPOSE = "";
                //check if item column exist

                //get item code
                string itemCode = dgv.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();
                string itemName = dgv.Rows[rowIndex].Cells[text.Header_ItemName].Value.ToString();

                //open purpose page
                frmJobPurposeEdit frm = new frmJobPurposeEdit(itemCode, itemName);

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                frm.ShowDialog();


                if (frmJobPurposeEdit.JOB_PURPOSE != "")
                {
                    JOB_PURPOSE = frmJobPurposeEdit.JOB_PURPOSE;

                    dgv.Rows[rowIndex].Cells[text.Header_Job_Purpose].Value = JOB_PURPOSE;
                    dgv.Rows[rowIndex].Cells[text.Header_Job_Purpose].Style.BackColor = Color.White;
                }

                dgv.ClearSelection();
            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "The row index provided is out of range.");
            }
        }

        private DataRow CreateNewItemRow(DataRow row, int index)
        {
            DataRow newRow = DT_ITEM_LIST.NewRow();

            string itemCode = row[dalItem.ItemCode].ToString();

            string itemCodePresent = row[dalItem.ItemCodePresent].ToString();

            itemCodePresent = string.IsNullOrEmpty(itemCodePresent) ? itemCode : itemCodePresent;

            string itemName = row[dalItem.ItemName].ToString();

            string itemDescription = itemName + "\n(" + itemCodePresent + ")";

            double pwPerShot = double.TryParse(row[dalItem.ItemProPWShot].ToString(), out pwPerShot) ? pwPerShot : 0;
            double rwPerShot = double.TryParse(row[dalItem.ItemProRWShot].ToString(), out rwPerShot) ? rwPerShot : 0;

            newRow[text.Header_Index] = index;
            newRow[text.Header_ItemDescription] = itemDescription;
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_ItemName] = itemName;
            newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
            newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
            newRow[text.Header_ProPwShot] = pwPerShot.ToString("#.##");
            newRow[text.Header_ProRwShot] = rwPerShot.ToString("#.##");

            if(DT_ITEM_LIST.Rows.Count > 0 )
            {
                newRow[text.Header_Job_Purpose] = DT_ITEM_LIST.Rows[0][text.Header_Job_Purpose].ToString();
            }
           
            return newRow;
        }

        private DataRow CreateNewItemRow(DataRow row, int index, string jobPurpose, int targetQty)
        {
            DataRow newRow = DT_ITEM_LIST.NewRow();

            string itemCodePresent = row[dalItem.ItemCodePresent].ToString();
            string itemCode = row[dalItem.ItemCode].ToString();
            itemCodePresent = string.IsNullOrEmpty(itemCodePresent) ? itemCode : itemCodePresent;

            string itemName = row[dalItem.ItemName].ToString();

            string itemDescription = itemName + "\n(" + itemCodePresent + ")";

            newRow[text.Header_Index] = index;
            newRow[text.Header_ItemDescription] = itemDescription;
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_ItemName] = itemName;

            LoadSingleMaterialList();

            double pwPerShot = double.TryParse(row[dalItem.ItemProPWShot].ToString(), out double x) ? x : 0;
            double rwPerShot = double.TryParse(row[dalItem.ItemProRWShot].ToString(), out  x) ? x : 0;

            newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
            newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
            newRow[text.Header_ProPwShot] = pwPerShot.ToString("0.##");
            newRow[text.Header_ProRwShot] = rwPerShot.ToString("0.##");

            newRow[text.Header_TargetQty] = targetQty;

            if (!string.IsNullOrEmpty(jobPurpose))
            {
                newRow[text.Header_Job_Purpose] = jobPurpose;
            }
            else if (DT_ITEM_LIST.Rows.Count > 0)
            {
                newRow[text.Header_Job_Purpose] = DT_ITEM_LIST.Rows[0][text.Header_Job_Purpose].ToString();
            }

            lblMouldCode.Text = "";
            lblMouldTon.Text = row[dalItem.ItemProTon].ToString();


            return newRow;
        }
        private void LoadSummary_ProductionInfo()
        {
            ClearProductionInfo();

            if (dgvItemList?.Rows.Count > 0)
            {
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                int totalCavity = 0;
                int totalShot = 0;
                decimal cycleTime = 0;

                decimal partWeightPerShot = 0;
                decimal runnerWeightPerShot = 0;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    totalCavity += int.TryParse(row[text.Header_Cavity].ToString(), out int cavity) ? cavity : 0;
                    partWeightPerShot += decimal.TryParse(row[text.Header_ProPwShot].ToString(), out decimal pwPerShot) ? pwPerShot : 0;

                    decimal rwPerShot = decimal.TryParse(row[text.Header_ProRwShot].ToString(), out rwPerShot) ? rwPerShot : 0;

                    if (rwPerShot > runnerWeightPerShot)
                    {
                        runnerWeightPerShot = rwPerShot;
                    }


                    decimal ct = decimal.TryParse(row[text.Header_ProCT].ToString(), out ct) ? ct : 0;

                    if (ct > cycleTime)
                    {
                        cycleTime = ct;
                    }

                    int targetQty = int.TryParse(row[text.Header_AutoQtyAdjustment].ToString(), out targetQty) ? targetQty : 0;

                    if (cavity > 0)
                    {
                        int shotQty = targetQty / cavity;

                        if (shotQty > totalShot)
                        {
                            totalShot = shotQty;
                        }
                    }
                }

                lblMouldCavity.Text = totalCavity.ToString();
                txtMouldCycleTime.Text = cycleTime.ToString("0");
                lblMouldPWPerShot.Text = partWeightPerShot.ToString("0.##");
                txtMouldRWPerShot.Text = runnerWeightPerShot.ToString("0.##");
                MIN_SHOT = totalShot;

                CalculateMaxShot();

            }

        }

       
        private void ClearProductionInfo()
        {
            lblMouldCavity.Text = "";
            txtMouldCycleTime.Text = "";
            lblMouldPWPerShot.Text = "";
            txtMouldRWPerShot.Text = "";
            MIN_SHOT = 0;
            CalculateMaxShot();

        }

        private double TwoDecimalPlace(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private DataTable ConsolidateStockCheckTable(DataTable originalTable)
        {
            var resultTable = NewStockCheckTable();

            // Group original DataTable by ItemCode
            var groupedData = originalTable.AsEnumerable()
                .GroupBy(r => r.Field<string>(text.Header_ItemCode));

            // Loop through grouped data
            foreach (var group in groupedData)
            {
                // Calculate RequiredForCurrentJob and BalStock for each group
                float totalRequiredForCurrentJob = group.Sum(r => r.Field<float>(text.Header_RequiredForCurrentJob));
                float readyStock = group.First().Field<float>(text.Header_ReadyStock);
                float reservedForOtherJobs = group.First().Field<float>(text.Header_ReservedForOtherJobs);
                float balStock = readyStock - reservedForOtherJobs - totalRequiredForCurrentJob;

                // Create new row for resultTable and add values
                DataRow newRow = resultTable.NewRow();
                newRow[text.Header_Index] = group.First().Field<int>(text.Header_Index);
                newRow[text.Header_Type] = group.First().Field<string>(text.Header_Type);
                newRow[text.Header_ItemDescription] = group.First().Field<string>(text.Header_ItemDescription);
                newRow[text.Header_ItemCode] = group.Key;
                newRow[text.Header_ItemName] = group.First().Field<string>(text.Header_ItemName);
                newRow[text.Header_ReadyStock] = TwoDecimalPlace(readyStock);
                newRow[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(reservedForOtherJobs);
                newRow[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalRequiredForCurrentJob);
                newRow[text.Header_BalStock] = TwoDecimalPlace(balStock);
                newRow[text.Header_Unit] = group.First().Field<string>(text.Header_Unit);

                if (balStock < 0)
                {
                    MATERIAL_STOCK_ENOUGH = false;
                }


                resultTable.Rows.Add(newRow);
            }

            return resultTable;
        }

        private void LoadMatCheckList()
        {
            frmLoading.ShowLoadingScreen();

            DataTable dt_MAT = NewStockCheckTable();
            DataTable dt = dalmatPlan.Select();
            int index = 1;
            float currentStock, planningUsed, availableQty, totalMaterial = 0, StockBal = 0;

            MATERIAL_STOCK_ENOUGH = true;

            dgvStockCheck.DataSource = dt_MAT;

            if (dgvItemList?.Rows.Count > 0)
            {
                //add raw material to list
                if (dgvRawMatList?.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvRawMatList.Rows)
                    {
                        string matCode = row.Cells[text.Header_ItemCode].Value.ToString();

                        if(!string.IsNullOrEmpty(tool.getItemNameFromDataTable(DT_ITEM, matCode)))
                        {
                            string matDescription = row.Cells[text.Header_ItemDescription].Value.ToString();

                            var matSummary = tool.loadMaterialPlanningSummary(matCode);
                            int planCounter = matSummary.Item1;
                            planningUsed = matSummary.Item2;
                            float TotalMatUsed = matSummary.Item3;
                            float TotalMatToUse = matSummary.Item4;
                            currentStock = matSummary.Item5;
                            bool rawType = matSummary.Item6;
                            bool colorType = matSummary.Item7;

                            availableQty = currentStock - TotalMatToUse;

                            totalMaterial = float.TryParse(row.Cells[text.Header_Qty_Required_KG].Value.ToString(), out totalMaterial) ? totalMaterial : 0;

                            StockBal = availableQty - totalMaterial;

                            DataRow row_dtMat = dt_MAT.NewRow();

                            row_dtMat[text.Header_Index] = index;


                            row_dtMat[text.Header_Type] = text.Cat_RawMat;
                            row_dtMat[text.Header_ItemCode] = matCode;
                            row_dtMat[text.Header_ItemDescription] = matDescription;

                            row_dtMat[text.Header_ReadyStock] = TwoDecimalPlace(currentStock);
                            row_dtMat[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(TotalMatToUse);

                            row_dtMat[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalMaterial);
                            row_dtMat[text.Header_BalStock] = TwoDecimalPlace(StockBal);
                            row_dtMat[text.Header_Unit] = text.Unit_KG;

                            dt_MAT.Rows.Add(row_dtMat);
                            index++;
                        }
                       
                    }
                }

                if(!string.IsNullOrEmpty(COLOR_MAT_CODE) && !string.IsNullOrEmpty(tool.getItemNameFromDataTable(DT_ITEM, COLOR_MAT_CODE)))
                {
                    string matCode = COLOR_MAT_CODE;

                    string matDescription = lblColorMatDescription.Text;

                    var matSummary = tool.loadMaterialPlanningSummary(matCode);
                    int planCounter = matSummary.Item1;
                    planningUsed = matSummary.Item2;
                    float TotalMatUsed = matSummary.Item3;
                    float TotalMatToUse = matSummary.Item4;
                    currentStock = matSummary.Item5;
                    bool rawType = matSummary.Item6;
                    bool colorType = matSummary.Item7;

                    availableQty = currentStock - TotalMatToUse;

                    totalMaterial = float.TryParse(lblColorMat.Text, out totalMaterial) ? totalMaterial : 0;

                    StockBal = availableQty - totalMaterial;

                    DataRow row_dtMat = dt_MAT.NewRow();

                    row_dtMat[text.Header_Index] = index;

                    row_dtMat[text.Header_Type] = tool.getItemCatFromDataTable(DT_ITEM, matCode);
                    row_dtMat[text.Header_ItemCode] = matCode;
                    row_dtMat[text.Header_ItemDescription] = matDescription;

                    row_dtMat[text.Header_ReadyStock] = TwoDecimalPlace(currentStock);
                    row_dtMat[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(TotalMatToUse);

                    row_dtMat[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalMaterial);
                    row_dtMat[text.Header_BalStock] = TwoDecimalPlace(StockBal);
                    row_dtMat[text.Header_Unit] = text.Unit_KG;

                    dt_MAT.Rows.Add(row_dtMat);
                    index++;
                }
                

                foreach (DataGridViewRow row in dgvItemList.Rows)
                {
                    string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();
                    string itemDescription = row.Cells[text.Header_ItemDescription].Value.ToString();

                    //add child material
                    if (!string.IsNullOrEmpty(itemCode))
                    {
                        //check if got child
                        if (tool.ifGotChildIncludedPackaging(itemCode))
                        {
                            //loop child list
                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            string childCode;
                            float targetQty = float.TryParse(row.Cells[text.Header_AutoQtyAdjustment].Value.ToString(), out targetQty) ? targetQty : 0;
                            float joinQty = 0;

                            foreach (DataRow JoinRow in dtJoin.Rows)
                            {
                                childCode = JoinRow[dalJoin.JoinChild].ToString();

                                //joinQty = row[dalJoin.JoinQty] == DBNull.Value? 0 : Convert.ToSingle(row[dalJoin.JoinQty]);

                                joinQty = float.TryParse(JoinRow[dalJoin.JoinQty].ToString(), out float i) ? i : 1;

                                currentStock = dalItem.getStockQty(childCode);

                                ////////////////////////////////////////////////////////////////////////////////////////////////////
                                //waiting to get planning used qty from database
                                planningUsed = tool.matPlanGetQty(dt, childCode);
                                availableQty = currentStock - planningUsed;

                                #region Max/Min

                                float childQty = targetQty;

                                int joinMax = int.TryParse(JoinRow[dalJoin.JoinMax].ToString(), out int j) ? j : 1;
                                int JoinMin = int.TryParse(JoinRow[dalJoin.JoinMin].ToString(), out int k) ? k : 1;

                                joinMax = joinMax <= 0 ? 1 : joinMax;
                                JoinMin = JoinMin <= 0 ? 1 : JoinMin;

                                int ParentQty = Convert.ToInt32(targetQty);

                                int fullQty = ParentQty / joinMax;

                                int notFullQty = ParentQty % joinMax;

                                childQty = fullQty * joinQty;

                                if (notFullQty >= JoinMin)
                                {
                                    childQty += joinQty;
                                }

                                #endregion

                                //calculate total material need
                                totalMaterial = childQty;
                                //totalMaterial = joinQty * targetQty;

                                StockBal = availableQty - totalMaterial;

                                if (StockBal < 0)
                                {
                                    MATERIAL_STOCK_ENOUGH = false;
                                }

                                DataRow row_dtMat = dt_MAT.NewRow();

                                row_dtMat[text.Header_Index] = index;

                                string childName = tool.getItemNameFromDataTable(DT_ITEM, childCode);
                                string childDescription = childName + " (" + childCode + ")";

                                row_dtMat[text.Header_Type] = tool.getCatNameFromDataTable(DT_ITEM, childCode);
                                row_dtMat[text.Header_ItemCode] = childCode;
                                row_dtMat[text.Header_ItemName] = childName;
                                row_dtMat[text.Header_ItemDescription] = childDescription;

                                row_dtMat[text.Header_ReadyStock] = TwoDecimalPlace(currentStock);
                                row_dtMat[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(planningUsed);

                                row_dtMat[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalMaterial);

                                row_dtMat[text.Header_BalStock] = TwoDecimalPlace(StockBal);
                                row_dtMat[text.Header_Unit] = text.Unit_Piece;

                                dt_MAT.Rows.Add(row_dtMat);
                                index++;
                            }

                        }
                    }
                }

            }

            //add datatable to datagridview if got data
            if (dt_MAT.Rows.Count > 0)
            {
                dt_MAT = ConsolidateStockCheckTable(dt_MAT);
                dgvStockCheck.DataSource = dt_MAT;
                dgvUIEdit(dgvStockCheck);
                StockCheckListCellFormatting(dgvStockCheck);
                dgvStockCheck.ClearSelection();

               
            }
            else
            {
                MessageBox.Show("No data for material check list.\n Please check the material for this planning or continue process to next step.");
            }

            frmLoading.CloseForm();
        }

        private void LoadMatCheckList_EditingMode()
        {
            frmLoading.ShowLoadingScreen();

            DataTable dt_MAT = NewStockCheckTable();
            DataTable dt = dalmatPlan.Select();
            int index = 1;
            float currentStock, planningUsed, availableQty, totalMaterial = 0, StockBal = 0;

            MATERIAL_STOCK_ENOUGH = true;

            dgvStockCheck.DataSource = dt_MAT;

            if (dgvItemList?.Rows.Count > 0)
            {
                //add raw material to list
                if (dgvRawMatList?.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvRawMatList.Rows)
                    {
                        string matCode = row.Cells[text.Header_ItemCode].Value.ToString();

                        if (!string.IsNullOrEmpty(tool.getItemNameFromDataTable(DT_ITEM, matCode)))
                        {
                            string matDescription = row.Cells[text.Header_ItemDescription].Value.ToString();

                            var matSummary = tool.loadMaterialPlanningSummary(matCode, LIST_EDITING_JOB_NO);
                            int planCounter = matSummary.Item1;
                            planningUsed = matSummary.Item2;
                            float TotalMatUsed = matSummary.Item3;
                            float TotalMatToUse = matSummary.Item4;
                            currentStock = matSummary.Item5;
                            bool rawType = matSummary.Item6;
                            bool colorType = matSummary.Item7;

                            availableQty = currentStock - TotalMatToUse;

                            totalMaterial = float.TryParse(row.Cells[text.Header_Qty_Required_KG].Value.ToString(), out totalMaterial) ? totalMaterial : 0;

                            StockBal = availableQty - totalMaterial;

                            DataRow row_dtMat = dt_MAT.NewRow();

                            row_dtMat[text.Header_Index] = index;


                            row_dtMat[text.Header_Type] = text.Cat_RawMat;
                            row_dtMat[text.Header_ItemCode] = matCode;
                            row_dtMat[text.Header_ItemDescription] = matDescription;

                            row_dtMat[text.Header_ReadyStock] = TwoDecimalPlace(currentStock);
                            row_dtMat[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(TotalMatToUse);

                            row_dtMat[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalMaterial);
                            row_dtMat[text.Header_BalStock] = TwoDecimalPlace(StockBal);
                            row_dtMat[text.Header_Unit] = text.Unit_KG;

                            dt_MAT.Rows.Add(row_dtMat);
                            index++;
                        }

                    }
                }

                if (!string.IsNullOrEmpty(COLOR_MAT_CODE) && !string.IsNullOrEmpty(tool.getItemNameFromDataTable(DT_ITEM, COLOR_MAT_CODE)))
                {
                    string matCode = COLOR_MAT_CODE;

                    string matDescription = lblColorMatDescription.Text;

                    var matSummary = tool.loadMaterialPlanningSummary(matCode, LIST_EDITING_JOB_NO);
                    int planCounter = matSummary.Item1;
                    planningUsed = matSummary.Item2;
                    float TotalMatUsed = matSummary.Item3;
                    float TotalMatToUse = matSummary.Item4;
                    currentStock = matSummary.Item5;
                    bool rawType = matSummary.Item6;
                    bool colorType = matSummary.Item7;

                    availableQty = currentStock - TotalMatToUse;

                    totalMaterial = float.TryParse(lblColorMat.Text, out totalMaterial) ? totalMaterial : 0;

                    StockBal = availableQty - totalMaterial;

                    DataRow row_dtMat = dt_MAT.NewRow();

                    row_dtMat[text.Header_Index] = index;

                    row_dtMat[text.Header_Type] = tool.getItemCatFromDataTable(DT_ITEM, matCode);
                    row_dtMat[text.Header_ItemCode] = matCode;
                    row_dtMat[text.Header_ItemDescription] = matDescription;

                    row_dtMat[text.Header_ReadyStock] = TwoDecimalPlace(currentStock);
                    row_dtMat[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(TotalMatToUse);

                    row_dtMat[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalMaterial);
                    row_dtMat[text.Header_BalStock] = TwoDecimalPlace(StockBal);
                    row_dtMat[text.Header_Unit] = text.Unit_KG;

                    dt_MAT.Rows.Add(row_dtMat);
                    index++;
                }


                foreach (DataGridViewRow row in dgvItemList.Rows)
                {
                    string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();
                    string itemDescription = row.Cells[text.Header_ItemDescription].Value.ToString();

                    //add child material
                    if (!string.IsNullOrEmpty(itemCode))
                    {
                        //check if got child
                        if (tool.ifGotChildIncludedPackaging(itemCode))
                        {
                            //loop child list
                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            string childCode;
                            float targetQty = float.TryParse(row.Cells[text.Header_AutoQtyAdjustment].Value.ToString(), out targetQty) ? targetQty : 0;
                            float joinQty = 0;

                            foreach (DataRow JoinRow in dtJoin.Rows)
                            {
                                childCode = JoinRow[dalJoin.JoinChild].ToString();

                                //joinQty = row[dalJoin.JoinQty] == DBNull.Value? 0 : Convert.ToSingle(row[dalJoin.JoinQty]);

                                joinQty = float.TryParse(JoinRow[dalJoin.JoinQty].ToString(), out float i) ? i : 1;

                                currentStock = dalItem.getStockQty(childCode);

                                ////////////////////////////////////////////////////////////////////////////////////////////////////
                                //waiting to get planning used qty from database
                                planningUsed = tool.matPlanGetQty(dt, childCode);
                                availableQty = currentStock - planningUsed;

                                #region Max/Min

                                float childQty = targetQty;

                                int joinMax = int.TryParse(JoinRow[dalJoin.JoinMax].ToString(), out int j) ? j : 1;
                                int JoinMin = int.TryParse(JoinRow[dalJoin.JoinMin].ToString(), out int k) ? k : 1;

                                joinMax = joinMax <= 0 ? 1 : joinMax;
                                JoinMin = JoinMin <= 0 ? 1 : JoinMin;

                                int ParentQty = Convert.ToInt32(targetQty);

                                int fullQty = ParentQty / joinMax;

                                int notFullQty = ParentQty % joinMax;

                                childQty = fullQty * joinQty;

                                if (notFullQty >= JoinMin)
                                {
                                    childQty += joinQty;
                                }

                                #endregion

                                //calculate total material need
                                totalMaterial = childQty;
                                //totalMaterial = joinQty * targetQty;

                                StockBal = availableQty - totalMaterial;

                                if (StockBal < 0)
                                {
                                    MATERIAL_STOCK_ENOUGH = false;
                                }

                                DataRow row_dtMat = dt_MAT.NewRow();

                                row_dtMat[text.Header_Index] = index;

                                string childName = tool.getItemNameFromDataTable(DT_ITEM, childCode);
                                string childDescription = childName + " (" + childCode + ")";

                                row_dtMat[text.Header_Type] = tool.getCatNameFromDataTable(DT_ITEM, childCode);
                                row_dtMat[text.Header_ItemCode] = childCode;
                                row_dtMat[text.Header_ItemName] = childName;
                                row_dtMat[text.Header_ItemDescription] = childDescription;

                                row_dtMat[text.Header_ReadyStock] = TwoDecimalPlace(currentStock);
                                row_dtMat[text.Header_ReservedForOtherJobs] = TwoDecimalPlace(planningUsed);

                                row_dtMat[text.Header_RequiredForCurrentJob] = TwoDecimalPlace(totalMaterial);

                                row_dtMat[text.Header_BalStock] = TwoDecimalPlace(StockBal);
                                row_dtMat[text.Header_Unit] = text.Unit_Piece;

                                dt_MAT.Rows.Add(row_dtMat);
                                index++;
                            }

                        }
                    }
                }

            }

            //add datatable to datagridview if got data
            if (dt_MAT.Rows.Count > 0)
            {
                dt_MAT = ConsolidateStockCheckTable(dt_MAT);
                dgvStockCheck.DataSource = dt_MAT;
                dgvUIEdit(dgvStockCheck);
                StockCheckListCellFormatting(dgvStockCheck);
                dgvStockCheck.ClearSelection();


            }
            else
            {
                MessageBox.Show("No data for material check list.\n Please check the material for this planning or continue process to next step.");
            }

            frmLoading.CloseForm();
        }

        private void LoadDB()
        {
            DT_ITEM = dalItem.Select();
            DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
            DT_MACHINE_SCHEDULE = dalPlan.SelectCompletedOrRunningPlan();
            DT_ACTIVE_JOB = dalPlan.SelectActivePlanning();

            DT_ACTIVE_JOB.DefaultView.Sort = dalMac.MacLocationName + " ASC," + dalMac.MacID + " ASC," + dalPlan.productionStartDate + " ASC";
            DT_ACTIVE_JOB = DT_ACTIVE_JOB.DefaultView.ToTable();

        }

        #endregion


        private void AddItem()
        {
            DataTable dt_itemList = null;

            if (dgvItemList?.Rows.Count > 0)
            {
                dt_itemList = (DataTable)dgvItemList.DataSource;
            }

            frmItemSearch frm = new frmItemSearch(dt_itemList, text.Cat_Part,true);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();

            frmLoading.CloseForm();

            if (frmItemSearch.ITEM_CODE_SELECTED != "")
            {
                AddItemToList(frmItemSearch.ITEM_CODE_SELECTED, frmItemSearch.JOB_PURPOSE, frmItemSearch.JOB_TARGET_QTY);
            }
        }

        private void AddRawItem()
        {
            DataTable dt_RawList = null;

            if (dgvRawMatList?.Rows.Count > 0)
            {
                dt_RawList = (DataTable)dgvRawMatList.DataSource;
            }

            frmItemSearch frm = new frmItemSearch(dt_RawList, text.Cat_RawMat, false);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();

            frmLoading.CloseForm();

            if (frmItemSearch.ITEM_CODE_SELECTED != "")
            {
                AddRawToList(frmItemSearch.ITEM_CODE_SELECTED);
            }

        }

        private void ChangeColorItem()
        {
            DataTable dt = NewRawMaterialList();

            DataRow newRow = dt.NewRow();

            newRow[text.Header_ItemCode] = COLOR_MAT_CODE;

            dt.Rows.Add(newRow);

            frmItemSearch frm = new frmItemSearch(dt, text.Cat_MB, false);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();

            frmLoading.CloseForm();

            if (frmItemSearch.ITEM_CODE_SELECTED != "")
            {
                ChangeColorMat(frmItemSearch.ITEM_CODE_SELECTED);
            }

        }

        private List<string> LIST_EDITING_JOB_NO = new List<string>();
        private void LoadEdtingItemList()
        {
            PRO_INFO_LOADING = true;

            string itemCode = DT_JOBEDITING_ROW[text.Header_ItemCode].ToString();

            if (!string.IsNullOrEmpty(itemCode))
            {
               

                int index = 1;
                if (dgvItemList?.Rows.Count > 0)
                {
                    DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                    //get last index
                    index = int.TryParse(dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_Index].Value.ToString(), out index) ? index++ : 1;
                }
                else
                {
                    DT_ITEM_LIST = NewItemList();
                }

                bool itemFound = false;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    if (itemCode == row[text.Header_ItemCode].ToString())
                    {
                        itemFound = true;
                        int rowIndex = DT_ITEM_LIST.Rows.IndexOf(row);

                        MessageBox.Show("Same item code found in the list (row: " + rowIndex + ").");
                        return;
                    }
                }

                if (!itemFound)
                {
                    DataRow newRow = DT_ITEM_LIST.NewRow();

                    string itemCode_Present = DT_JOBEDITING_ROW[text.Header_ItemCode_Present].ToString();
                    itemCode_Present = string.IsNullOrEmpty(itemCode_Present) ? itemCode : itemCode_Present;

                    string itemName = DT_JOBEDITING_ROW[text.Header_ItemName].ToString();

                    string itemDescription = itemName + "\n(" + itemCode_Present + ")";

                    newRow[text.Header_Index] = index;
                    newRow[text.Header_ItemDescription] = itemDescription;
                    newRow[text.Header_ItemCode] = itemCode;
                    newRow[text.Header_ItemName] = itemName;
                    newRow[text.Header_Cavity] = DT_JOBEDITING_ROW[text.Header_Cavity];
                    newRow[text.Header_ProCT] = DT_JOBEDITING_ROW[text.Header_ProCT];
                    newRow[text.Header_ProPwShot] = DT_JOBEDITING_ROW[text.Header_ProPwShot];
                    newRow[text.Header_ProRwShot] = DT_JOBEDITING_ROW[text.Header_ProRwShot];
                    newRow[text.Header_Job_Purpose] = DT_JOBEDITING_ROW[text.Header_Job_Purpose];
                    newRow[text.Header_TargetQty] = DT_JOBEDITING_ROW[text.Header_TargetQty];
                    newRow[text.Header_JobNo] = DT_JOBEDITING_ROW[text.Header_JobNo];
                    newRow[text.Header_FamilyWithJobNo] = DT_JOBEDITING_ROW[text.Header_FamilyWithJobNo];

                    DT_ITEM_LIST.Rows.Add(newRow);

                    LIST_EDITING_JOB_NO.Add(EDITING_JOB_NO);

                }

                string editingItemFamilyWith = DT_JOBEDITING_ROW[text.Header_FamilyWithJobNo].ToString();
                //family mould check
                if (editingItemFamilyWith  != "-1")
                {
                    string editingItemJobNo = DT_JOBEDITING_ROW[text.Header_JobNo].ToString();

                    foreach (DataRow row in DT_JOBEDITING_MAC_SCHEDULE.Rows)
                    {
                        string jobNo = row[text.Header_JobNo].ToString();
                        string familyWith = row[text.Header_FamilyWithJobNo].ToString();

                        if(familyWith == editingItemFamilyWith && jobNo != editingItemJobNo)
                        {
                            DataRow newRow = DT_ITEM_LIST.NewRow();

                            string FamilyItemCode = row[text.Header_ItemCode].ToString();
                            string itemCode_Present = row[text.Header_ItemCode_Present].ToString();
                            itemCode_Present = string.IsNullOrEmpty(itemCode_Present) ? FamilyItemCode : itemCode_Present;

                            string itemName = row[text.Header_ItemName].ToString();

                            string itemDescription = itemName + "\n(" + itemCode_Present + ")";

                            newRow[text.Header_Index] = index;
                            newRow[text.Header_ItemDescription] = itemDescription;
                            newRow[text.Header_ItemCode] = FamilyItemCode;
                            newRow[text.Header_ItemName] = itemName;
                            newRow[text.Header_Cavity] = row[text.Header_Cavity];
                            newRow[text.Header_ProCT] = row[text.Header_ProCT];
                            newRow[text.Header_ProPwShot] = row[text.Header_ProPwShot];
                            newRow[text.Header_ProRwShot] = row[text.Header_ProRwShot];
                            newRow[text.Header_Job_Purpose] = row[text.Header_Job_Purpose];
                            newRow[text.Header_TargetQty] = row[text.Header_TargetQty];
                            newRow[text.Header_JobNo] = row[text.Header_JobNo];
                            newRow[text.Header_FamilyWithJobNo] = row[text.Header_FamilyWithJobNo];

                            DT_ITEM_LIST.Rows.Add(newRow);

                            LIST_EDITING_JOB_NO.Add(row[text.Header_JobNo].ToString());

                        }
                    }
                }

                dgvItemList.DataSource = DT_ITEM_LIST;
                IndexReset(dgvItemList);
                dgvUIEdit(dgvItemList);
                ItemListCellFormatting(dgvItemList);

                LoadEditingMaterialList();

                LoadSummary_ProductionInfo();


            }

            PRO_INFO_LOADING = false;

            string ItemListTitle = "Item List";

            if (dgvItemList?.Rows.Count > 0)
            {
                int itemCount = dgvItemList.Rows.Count;

                if (itemCount > 1)
                {
                    ItemListTitle += " (" + itemCount + " Items)";
                }
                else
                {
                    ItemListTitle += " (" + itemCount + " Item)";

                }
            }

            lblItemListTitle.Text = ItemListTitle;
            //dgvItemList.FirstDisplayedScrollingRowIndex = 0;
            refreshCavityMatchedQty();

            dgvItemList.ClearSelection();

        }

        private void LoadEditingData()
        {
            if (DT_JOBEDITING_MAC_SCHEDULE?.Rows.Count > 0)
            {
                LoadEdtingItemList();
            }
        }

        private void frmPlanningNEWV2_Shown(object sender, EventArgs e)
        {
            //load item search page
            if (CALL_WITH_ITEM_CODE != null && JOB_ADDING_MODE)
            {
                AutoLoadPageWithItemCode();
                //MaterialStockCheckMode();
                //machineSelectionMode();
            }
            else if(JOB_EDITING_MODE)
            {
                if(DT_JOBEDITING_MAC_SCHEDULE?.Rows.Count > 0)
                {
                    LoadEditingData();
                }
                else
                {
                    MessageBox.Show("Data not found!");
                }
            }
            else
            {
                AddItem();
            }
        }

        private void ItemListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    string itemDescription = row[text.Header_ItemDescription].ToString();
                    string jobPurpose = row[text.Header_Job_Purpose].ToString();


                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Cavity].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[rowIndex].Cells[text.Header_ProPwShot].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[rowIndex].Cells[text.Header_TargetQty].Style.BackColor = Color.FromArgb(254, 241, 154); 
                    dgv.Rows[rowIndex].Cells[text.Header_AutoQtyAdjustment].Style.BackColor = Color.FromArgb(255, 153, 153);
                    dgv.Rows[rowIndex].Cells[text.Header_AutoQtyAdjustment].Style.ForeColor = Color.FromArgb(64, 64, 64);
                    dgv.Rows[rowIndex].Cells[text.Header_AutoQtyAdjustment].Style.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

                    if (string.IsNullOrEmpty(jobPurpose))
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_Job_Purpose].Style.BackColor = Color.Red;
                    }

                }

                dgv.ResumeLayout();
            }

        }
        private void RawMatListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Selection].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[rowIndex].Cells[text.Header_KGPERBAG].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[rowIndex].Cells[text.Header_Ratio].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[rowIndex].Cells[text.Header_RoundUp_ToBag].Style.BackColor = Color.FromArgb(251, 255, 147);
                    dgv.Rows[rowIndex].Cells[text.Header_Remark].Style.BackColor = Color.FromArgb(251, 255, 147);
                }

                dgv.ResumeLayout();
            }

        }
        private void StockCheckListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;


                    foreach (DataColumn col in dt.Columns)
                    {
                        string colName = col.ColumnName;


                        bool isNumColumn = colName == text.Header_ReadyStock;
                        isNumColumn |= colName == text.Header_ReservedForOtherJobs;
                        isNumColumn |= colName == text.Header_RequiredForCurrentJob;
                        isNumColumn |= colName == text.Header_BalStock;

                        if (isNumColumn)
                        {
                            int colIndex = dgv.Columns[colName].Index;

                            decimal num = decimal.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out num) ? num : 0;

                            if (num < 0)
                            {
                                dgv.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                dgv.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.Black;
                            }
                        }
                    }


                }

                dgv.ResumeLayout();
            }
        }
        private void MacScheduleListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    string macID = row[text.Header_MacID].ToString();
                    string status = row[text.Header_Status].ToString();
                    string remark = row[text.Header_Remark].ToString();

                    dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
                    dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);

                    if (remark.Contains("Family Mould"))
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.BackColor = Color.Yellow;

                    }

                    if (string.IsNullOrEmpty(macID))
                    {
                        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(70,70,70);
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(245, 247, 255);
                        dgv.Rows[rowIndex].Height = 20;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.WhiteSmoke;
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.WhiteSmoke;

                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(50, 50, 50);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(50, 50, 50);

                       

                        // Check if macID is used before and bold the font
                        int macID_INT = int.TryParse(macID, out macID_INT) ? macID_INT : 0;
                        if (MacHistoryList.Contains(macID_INT))
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_MacName].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                            //dgv.Rows[rowIndex].Cells[text.Header_MacName].Style.ForeColor = Color.Blue;

                        }
                    }

                    if (status == text.planning_status_running)
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = Color.LightGreen;
                        //dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 6F, FontStyle.Regular);


                    }
                    else if (status == text.planning_status_draft)
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = Color.FromArgb(251, 255, 147);
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.FromArgb(251, 255, 147);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.FromArgb(251, 255, 147);

                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(50, 50, 50);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(50, 50, 50);

                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                    }
                    else if (status == text.planning_status_new_draft)
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(251, 255, 147);
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.FromArgb(251, 255, 147);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.FromArgb(251, 255, 147);

                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(50, 50, 50);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(50, 50, 50);

                        //dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = Color.FromArgb(251, 255, 147);
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                    }
                    else if (status == text.planning_status_edting)
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 241, 154);
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.FromArgb(254, 241, 154);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.FromArgb(254, 241, 154);
                        //dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = Color.FromArgb(251, 255, 147);

                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(50, 50, 50);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(50, 50, 50);

                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                    }
                    else if (status == text.planning_status_idle)
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.WhiteSmoke;
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.WhiteSmoke;

                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(50, 50, 50);
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(50, 50, 50);

                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.ForeColor = Color.Red;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = Color.LightGray;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                    }
                    else if (status == text.planning_status_to_update)
                    {
      
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.ForeColor = Color.Black;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                    }

                }

                CheckMachineProductionDateCollision();
                dgv.ResumeLayout();
            }

        }

        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
          
        }

        private void dgvItemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dgvReadOnlyModeUpdate(DataGridView dgv, int rowIndex, int colIndex)
        {
            listReadOnlyModeChanging = true;

            if(dgv == dgvItemList)
            {
                if (rowIndex >= 0 && rowIndex < dgv.Rows.Count && colIndex >= 0 && colIndex < dgv.Columns.Count)
                {
                    string colName = dgv.Columns[colIndex].Name;

                    bool readOnlyMode = true;

                    if (colName.Equals(text.Header_TargetQty) || colName.Equals(text.Header_Cavity) || colName.Equals(text.Header_ProPwShot))
                    {
                        readOnlyMode = false;
                    }

                    if (readOnlyMode)
                    {
                        dgv.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView
                                             // Change back to full row selection mode for any other column.
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                        dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                    }
                    else
                    {
                        // Change to cell selection mode.
                        dgv.ReadOnly = false; // Temporarily allow user interaction

                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                        dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                    }
                }
            }

            else if (dgv == dgvRawMatList)
            {
                if (rowIndex >= 0 && rowIndex < dgv.Rows.Count && colIndex >= 0 && colIndex < dgv.Columns.Count)
                {
                    string colName = dgv.Columns[colIndex].Name;

                    bool readOnlyMode = true;

                    if (colName.Equals(text.Header_KGPERBAG) || colName.Equals(text.Header_Selection) || colName.Equals(text.Header_Ratio) || colName.Equals(text.Header_RoundUp_ToBag) || colName.Equals(text.Header_Remark))
                    {
                        readOnlyMode = false;
                    }

                    if (readOnlyMode)
                    {
                        dgv.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView
                                             // Change back to full row selection mode for any other column.
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                        dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                    }
                    else
                    {
                        // Change to cell selection mode.
                        dgv.ReadOnly = false; // Temporarily allow user interaction

                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                        dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                    }
                }
            }
            listReadOnlyModeChanging = false;

        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!listReadOnlyModeChanging)
                dgvReadOnlyModeUpdate(dgvItemList, e.RowIndex, e.ColumnIndex);
        }
        private bool userInitiatedChange = true;

        private bool autoMaxQtyUpdating = false;

        private void refreshCavityMatchedQty()
        {
            if (dgvItemList?.Rows.Count > 0 && !autoMaxQtyUpdating)
            {
                autoMaxQtyUpdating = true;
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                decimal totalShot = 0;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    decimal targetQty = decimal.TryParse(row[text.Header_TargetQty].ToString(), out targetQty) ? targetQty : 0;
                    decimal cavity = decimal.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                    decimal cavityMatchedQty = 0;

                    if (cavity > 0)
                    {
                        cavityMatchedQty = Math.Ceiling(targetQty / cavity);

                        if (cavityMatchedQty > totalShot)
                        {
                            totalShot = cavityMatchedQty;
                        }
                        cavityMatchedQty *= cavity;
                    }

                    row[text.Header_AutoQtyAdjustment] = (int)cavityMatchedQty;
                }

                MIN_SHOT = (int)totalShot;

                CalculateMaxShot();

                autoMaxQtyUpdating = false;
            }
        }

        private void updateItemMaxQty()
        {
            autoMaxQtyUpdating = true;

            if (dgvItemList?.Rows.Count > 0)
            {
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                int maxShot = int.TryParse(lblMaxShot.Text, out maxShot) ? maxShot : 0;


                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    int cavity = int.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;

                    int maxQty = cavity * maxShot;

                    row[text.Header_AutoQtyAdjustment] = maxQty;


                }
            }

            autoMaxQtyUpdating = false;

        }



        private void StepUIUpdate(int step,bool Continue)
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
            lblStep4.Font = normalStepFont;

            tlpJobPlanningStep.Visible = false;
            btnCancel.Visible = false;
            btnAddAsDraft.Visible = false;
            btnJobPublish.Visible = false;

            tlpJobPlanningStep.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0);
            tlpJobPlanningStep.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
            tlpJobPlanningStep.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);
            tlpJobPlanningStep.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
            tlpJobPlanningStep.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);
            tlpJobPlanningStep.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0);

            tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);//Previous
            tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);//Continue
            tlpButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);//Cancel
            tlpButton.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0);//Draft
            tlpButton.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0);//Publish

          

            Color FormBackColor = Color.FromArgb(147, 168, 255);//147, 168, 255 //245, 247, 255
            Color CircleLabelProcessingColor = Color.White;
            Color lblStepTextForeColor = Color.Black;
            Color CurrentStepColor = Color.FromArgb(254, 241, 154); 
            Color circleLabelCompletedBackColor = Color.FromArgb(153, 255, 204);

            string tickText = "✓";
            panelStatusUp1.BackColor = FormBackColor;
            panelStatusUp2.BackColor = FormBackColor;
            panelStatusUp3.BackColor = FormBackColor;
            panelStatusUp4.BackColor = FormBackColor;

            circleLabelStep1.CircleBackColor = CircleLabelProcessingColor;
            circleLabelStep2.CircleBackColor = CircleLabelProcessingColor;
            circleLabelStep3.CircleBackColor = CircleLabelProcessingColor;
            circleLabelStep4.CircleBackColor = CircleLabelProcessingColor;

            circleLabelStep1.Text = "1";
            circleLabelStep2.Text = "2";
            circleLabelStep3.Text = "3";
            circleLabelStep4.Text = "4";

            lblStep1.ForeColor = lblStepTextForeColor;
            lblStep2.ForeColor = lblStepTextForeColor;
            lblStep3.ForeColor = lblStepTextForeColor;
            lblStep4.ForeColor = lblStepTextForeColor;

            #endregion

            switch (step)
            {
                #region Case 1: Item , Mould, Raw Mat, Color Mat, Max Shot, Time Required
                case 1:

                    panelStatusUp1.BackColor = CurrentStepColor;
                    lblStep1.Font = CurrentStepFont;

                    tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 200);//Continue

                    tlpJobPlanningStep.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                    tlpJobPlanningStep.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 20);
                    tlpJobPlanningStep.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 350);
                    tlpJobPlanningStep.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0);

                    btnPreviousStep.Visible = false;
                    btnContinue.Visible = true;

                    break;
                #endregion

                #region Case 2: Stock Check
                case 2:

                    circleLabelStep1.Text = tickText;
                    circleLabelStep1.CircleBackColor = circleLabelCompletedBackColor;

                    panelStatusUp2.BackColor = CurrentStepColor;
                    lblStep2.Font = CurrentStepFont;


                    tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200);//Previous
                    tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 200);//Continue

                    btnPreviousStep.Visible = true;
                    btnContinue.Visible = true;

                    tlpJobPlanningStep.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 100);
                    tlpJobPlanningStep.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0);

                    if(Continue)
                    MaterialStockCheckMode();

                    break;
                #endregion

                #region Case 3: Machine Schedule
                case 3:

                    circleLabelStep1.Text = tickText;
                    circleLabelStep2.Text = tickText;
                    circleLabelStep1.CircleBackColor = circleLabelCompletedBackColor;
                    circleLabelStep2.CircleBackColor = circleLabelCompletedBackColor;

                    panelStatusUp3.BackColor = CurrentStepColor;
                    lblStep3.Font = CurrentStepFont;


                    tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200);//Previous
                    tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 200);//Continue

                    btnPreviousStep.Visible = true;
                    btnContinue.Visible = true;

                    tlpJobPlanningStep.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 100);
                    tlpJobPlanningStep.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0);

                    tlpMachineScheduleList.Visible = true;

                    if (Continue)
                        machineSelectionMode();

                    break;
                #endregion

                #region Case 4: Summary
                case 4:

                    tlpMachineScheduleList.Visible = false;

                    circleLabelStep1.Text = tickText;
                    circleLabelStep2.Text = tickText;
                    circleLabelStep3.Text = tickText;
                    circleLabelStep1.CircleBackColor = circleLabelCompletedBackColor;
                    circleLabelStep2.CircleBackColor = circleLabelCompletedBackColor;
                    circleLabelStep3.CircleBackColor = circleLabelCompletedBackColor;


                    panelStatusUp4.BackColor = CurrentStepColor;
                    lblStep4.Font = CurrentStepFont;


                    tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 200);//Previous
                    //tlpButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 150);//Cancel
                    tlpButton.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 200);//Draft
                    tlpButton.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 200);//Publish

                    btnContinue.Visible = false;

                    btnCancel.Visible = true;
                    btnAddAsDraft.Visible = true;
                    btnJobPublish.Visible = true;

                    tlpJobPlanningStep.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);
                    tlpJobPlanningStep.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 100);

                    if(JOB_EDITING_MODE)
                    {
                        btnAddAsDraft.Text = "Update as Draft";
                        btnJobPublish.Text = "Job Update";
                    }

                    LoadSummaryData();

                    break;
                    #endregion
            }

            System.Threading.Thread.Sleep(100);

            tlpJobPlanningStep.Visible = true;

        }

        private void dgvItemList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (!PRO_INFO_LOADING && (e.ColumnIndex == dgvItemList.Columns[text.Header_Cavity].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_ProPwShot].Index))
            {

            }

            PRO_INFO_LOADING = true;

            if (e.ColumnIndex != dgvItemList.Columns[text.Header_TargetQty].Index &&
                e.ColumnIndex != dgvItemList.Columns[text.Header_Cavity].Index &&
                e.ColumnIndex != dgvItemList.Columns[text.Header_ProPwShot].Index)
                return;

            // Ignore any changes that occur programmatically to avoid infinite loops
            if (!userInitiatedChange) return;

            try
            {
                // Temporarily disable changes from triggering this event
                userInitiatedChange = false;

                DataGridViewRow currentRow = dgvItemList.Rows[e.RowIndex];


                if (e.ColumnIndex == dgvItemList.Columns[text.Header_TargetQty].Index)
                {
                    refreshCavityMatchedQty();
                }
                else if (e.ColumnIndex == dgvItemList.Columns[text.Header_Cavity].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_TargetQty].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_ProPwShot].Index)
                {
                    LoadSummary_ProductionInfo();
                }



            }
            finally
            {
                // Allow changes to trigger this event again
                userInitiatedChange = true;
            }

            PRO_INFO_LOADING = false;

        }

        private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void CannotEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void FloatOnly_KeyPress(object sender, KeyPressEventArgs e)
        {

            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (tb != null)
            {
                // If it's not a number, not a control key (like backspace), and it's not the first decimal point
                if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || tb.Text.Contains('.')))
                {
                    e.Handled = true;
                }
            }

            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgvMouldList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dgvMouldList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvItemList;

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();


                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string currentHeader = dgv.Columns[colIndex].Name;

                try
                {
                    my_menu.Items.Add(text.RemoveItem).Name = text.RemoveItem;
                    my_menu.Items.Add(text.JobPurpose).Name = text.JobPurpose;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvItemList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;

            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.RemoveItem))
            {
                RemoveItemFromList(dgv, rowIndex);
            }
            else if (itemClicked.Equals(text.JobPurpose))
            {
                EditJobPurpose(dgv, rowIndex);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void my_menu_RawItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvRawMatList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;

            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.RemoveItem))
            {
                RemoveRawFromList(dgv, rowIndex);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }
        private void tableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void tableLayoutPanel12_MouseClick(object sender, MouseEventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private bool updateMouldItem(itemBLL u)
        {
            // Your update logic here
            return dalItem.MouldItemUpdate(u);
        }

        bool listReadOnlyModeChanging = false;

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;

                if (dgvItemList.SelectedCells.Count > 0)  // checking if any cell is selected
                {
                    int rowIndex = dgvItemList.SelectedCells[0].RowIndex; // Get the row index of first selected cell
                    int columnIndex = dgvItemList.SelectedCells[0].ColumnIndex; // Get the column index of first selected cell

                    dgvReadOnlyModeUpdate(dgvItemList, rowIndex, columnIndex);
                }

                listReadOnlyModeChanging = false;
            }
        }

        private void SaveItemProductionInfo(string messageToUser)
        {
            DialogResult dialogResult = MessageBox.Show(messageToUser, "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                itemBLL uItem = new itemBLL();

                if (dgvItemList?.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)dgvItemList.DataSource;

                    foreach (DataRow row in dt.Rows)
                    {
                        string itemCode = row[text.Header_ItemCode].ToString();

                        if (!string.IsNullOrEmpty(itemCode))
                        {
                            uItem.item_code = itemCode;
                            uItem.item_cavity = int.TryParse(lblMouldCavity.Text, out int i) ? i : 0;
                            uItem.item_pro_ct_to = int.TryParse(txtMouldCycleTime.Text, out i) ? i : 0;
                            uItem.item_pro_pw_shot = int.TryParse(lblMouldPWPerShot.Text, out i) ? i : 0;
                            uItem.item_pro_rw_shot = int.TryParse(txtMouldRWPerShot.Text, out i) ? i : 0;
                            uItem.item_updtd_date = DateTime.Now;
                            uItem.item_updtd_by = MainDashboard.USER_ID;

                            if (dalItem.updateItemProductionInfoAndHistoryRecord(uItem))
                            {
                                MessageBox.Show("Data saved!");
                                PRO_INFO_CHANGE = false;

                                break;
                            }
                        }
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type


            }
        }
        private void btnItemInfoSave_Click(object sender, EventArgs e)
        {
            //check total row in item list
            if (dgvItemList?.Rows.Count > 0)
            {
                if (dgvItemList.Rows.Count < 2)
                {
                    //one item only, save item info
                    SaveItemProductionInfo("Confirm: Save the data?");
                }
                else
                {
                    //save mould_item data
                }
            }
        }


        private void cbRunnerRecycle_CheckedChanged(object sender, EventArgs e)
        {
            CalculateMaxShot();
        }

        private void cbRoundUpToBag_CheckedChanged(object sender, EventArgs e)
        {
            CalculateMaxShot();
        }

        private void txtMatWastage_TextChanged(object sender, EventArgs e)
        {
            CalculateMaxShot();
        }

        private void txtHrsPerDay_TextChanged(object sender, EventArgs e)
        {
            FromMaxShotToTimeNeededCalculation();
        }

        private void txtHrsPerDay_Leave(object sender, EventArgs e)
        {

        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void MaterialStockCheckMode()
        {
            int maxShot = int.TryParse(lblMaxShot.Text, out maxShot) ? maxShot : 0;

            if (maxShot > 0)
            {
                if(JOB_EDITING_MODE)
                {
                    LoadMatCheckList_EditingMode();

                }
                else
                {
                    LoadMatCheckList();

                }
            }
            else
            {
                MessageBox.Show("Please set a target quantity for production before proceeding with a stock check action.");
            }
        }


        private void btnChangeColorMat_Click(object sender, EventArgs e)
        {
            if (JOB_EDITING_MODE)
            {
                MessageBox.Show("You cannot change or remove material in Job Editing Mode.\nIf you wish to modify the material, cancel this Job through the Machine Schedule, then add a new job with the desired material.");
            }
            else
                ChangeColorItem();
        }

       

        private void txtMaxShot_TextChanged(object sender, EventArgs e)
        {
            updateItemMaxQty();
        }

        private List<int> MacHistoryList = new List<int>();
        private string GetMachineHistory(string _ItemCode)
        {
            string MachineHistory = " Machine History : ";

            foreach (DataRow row in DT_MACHINE_SCHEDULE.Rows)
            {
                string itemCode = row[dalPlanning.partCode].ToString();

                if (itemCode == _ItemCode)
                {
                    int macID = int.TryParse(row[dalPlanning.machineID].ToString(), out macID) ? macID : 0;

                    MacHistoryList.Add(macID);
                }
            }

            List<int> noDupes = MacHistoryList.Distinct().ToList();

            noDupes.Sort();

            foreach (int i in noDupes)
            {
                var value = noDupes[noDupes.Count - 1];

                if (i == value)
                {
                    MachineHistory += i + " ;";

                }
                else
                {
                    MachineHistory += i + " , ";

                }
            }

            return MachineHistory;

        }

        private void LoadMachineHistoryRemark()
        {
            string remark = "";

            if (dgvItemList?.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvItemList.Rows)
                {
                    string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();
                    remark += GetMachineHistory(itemCode);
                }
            }



            txtMachineSelectionRemark.Text = remark;
        }

        private List<Tuple<int, string, int, string>> MachineList = new List<Tuple<int, string, int, string>>();

        private void loadMachine(ComboBox cmb)
        {
            string macSelected = "";

            if (cmbMac.SelectedItem != null)
            {
                macSelected = cmbMac.Text;
            }

            DataRowView drv = (DataRowView)cmbMacLocation.SelectedItem;

            String valueOfItem = drv["fac_name"].ToString();

            string fac = valueOfItem;

            DataTable dt_Mac = dalMac.Select();

            DataTable dt = new DataTable();
            dt.Columns.Add(dalMac.MacID, typeof(int));
            dt.Columns.Add(dalMac.MacName, typeof(string));

            if (string.IsNullOrEmpty(fac) || fac.ToUpper().Equals(text.Cmb_All.ToUpper()))
            {
                //load All active machine
                facDAL dalFac = new facDAL();

                DataTable dt_Fac = (DataTable)cmbMacLocation.DataSource;

                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocationName].ToString();

                    foreach (DataRow facRow in dt_Fac.Rows)
                    {
                        string facName = facRow[dalFac.FacName].ToString();
                        string facID = facRow[dalFac.FacID].ToString();

                        if (macLocation == facName)
                        {
                            dt.Rows.Add(row[dalMac.MacID], row[dalMac.MacName]);

                            Tuple<int, string, int, string> machine = new Tuple<int, string, int, string>(Convert.ToInt32(facID), facName, Convert.ToInt32(row[dalMac.MacID]), row[dalMac.MacName].ToString());
                            MachineList.Add(machine);
                        }
                    }
                }

            }
            else
            {
                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocationName].ToString();

                    if (macLocation == fac)
                    {
                        dt.Rows.Add(row[dalMac.MacID], row[dalMac.MacName]);

                    }
                }
            }

            DataTable distinctTable = dt.DefaultView.ToTable(true, dalMac.MacID, dalMac.MacName);
            distinctTable.DefaultView.Sort = dalMac.MacID + " ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = dalMac.MacName;

            if (macSelected == "")
            {
                cmb.SelectedIndex = -1;
            }
            else
            {
                cmb.Text = macSelected;

            }
        }

        private void StartDateInitial()
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);

            bool ifTomorrowSunday = tomorrow.DayOfWeek == DayOfWeek.Sunday;

            if (ifTomorrowSunday && !cbIncludeSunday.Checked)
            {
                tomorrow = tomorrow.AddDays(1);
            }

            dtpStartDate.Value = tomorrow;

        }

        private bool ProDateChanging = false;

        private void EstEndDateUpdate()
        {
            if(!ProDateChanging)
            {
                ProDateChanging = true;
                DateTime Start = dtpStartDate.Value;

                //int day = int.TryParse(txtDaysNeeded.Text, out day) ? day : 0;

                //double balHours = double.TryParse(txtbalHours.Text, out balHours) ? balHours : 0;

                //if (balHours > 0)
                //{
                //    day++;
                //}
                //if (day < 2)
                //{
                //    day = 0;
                //}
                //else
                //{
                //    day--;
                //}

                //if (!cbIncludeSunday.Checked)
                //{
                //    for (var date = Start; date <= Start.AddDays(day); date = date.AddDays(1))
                //    {
                //        if (date.DayOfWeek == DayOfWeek.Sunday)
                //        {
                //            day++;
                //        }
                //    }
                //}

                //dtpEstimateEndDate.Value = Start.AddDays(day);

                dtpEstimateEndDate.Value = CalculateNewDraftEndDate(Start);

                NewDraftProductionDateUpdate();

                ProDateChanging = false;
            }
        }
        private void MachineSelectionSettingInitial()
        {
            tool.loadProductionFactory(cmbMacLocation);

            cmbMac.SelectedIndex = -1;

            LoadMachineHistoryRemark();

            StartDateInitial();

            if(JOB_EDITING_MODE)
            {
                ChangeCMBLocationAndMachine(DT_JOBEDITING_ROW[text.Header_FacID].ToString(), DT_JOBEDITING_ROW[text.Header_MacID].ToString());
                
            }
        }

        private bool DATE_COLLISION_FOUND = false;
        private bool CheckMachineProductionDateCollision()
        {
    
            string currentMacID = null;
            string LastFamilyWith = null;
            DateTime LastRow_EndDate = DateTime.MaxValue;

            bool DateCollisionFound = false;

            foreach (DataGridViewRow row in dgvMacSchedule.Rows)
            {
                string MacID = row.Cells[text.Header_MacID].Value.ToString();
                string FamilyWith = row.Cells[text.Header_FamilyWithJobNo].Value.ToString();
                string status   = row.Cells[text.Header_Status].Value.ToString();

                DateTime Date_Start = DateTime.TryParse(row.Cells[text.Header_DateStart].Value.ToString(), out Date_Start) ? Date_Start : DateTime.MaxValue;
                DateTime Date_End = DateTime.TryParse(row.Cells[text.Header_EstDateEnd].Value.ToString(), out Date_End) ? Date_End : DateTime.MaxValue;

                if (!string.IsNullOrEmpty(MacID))
                {
                    if (currentMacID != MacID)
                    {
                        currentMacID = MacID;
                        LastRow_EndDate = Date_End;
                    }
                    else
                    {
                        if(Date_Start < LastRow_EndDate && LastRow_EndDate != DateTime.MaxValue && (LastFamilyWith != FamilyWith || FamilyWith == "-1" || string.IsNullOrEmpty(FamilyWith)))
                        {
                            row.Cells[text.Header_DateStart].Style.BackColor = Color.Red;

                            row.Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);

                            row.Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(254, 241, 154);

                            DateCollisionFound = true;

                            
                            if (Date_End < LastRow_EndDate)
                            {
                                row.Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(254, 241, 154);

                                row.Cells[text.Header_EstDateEnd].Style.BackColor = Color.Red;

                                row.Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);

                                if (status == text.planning_status_new_draft || status == text.planning_status_edting)
                                {
                                    DATE_COLLISION_FOUND = true;
                                }
                            }
                        }
                        else
                        {
                            LastRow_EndDate = Date_End;
                        }
                     
                    }

                    if (Date_End < Date_Start && Date_Start != DateTime.MaxValue)
                    {
                        row.Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(254, 241, 154);
                        row.Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(254, 241, 154);

                        row.Cells[text.Header_DateStart].Style.BackColor = Color.Red;
                        row.Cells[text.Header_EstDateEnd].Style.BackColor = Color.Red;

                        row.Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);
                        row.Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);

                        DateCollisionFound = true;

                     
                    }
                

                   
                    LastFamilyWith = FamilyWith;
                }
               
            }

            DATE_COLLISION_FOUND = DateCollisionFound;

            if(dgvMacSchedule?.Rows.Count > 0)
            {
                btnAdjustCollisionDateBySystem.Visible = DateCollisionFound;

            }
            else
            {
                btnAdjustCollisionDateBySystem.Visible = false;

            }

            adjustMachineScheduleButtonLayout(DateCollisionFound);

            return DateCollisionFound;
        }

        private void adjustMachineScheduleButtonLayout(bool Show)
        {
            if(Show)
            {
                tlpMachineScheduleList.RowStyles[5] = new RowStyle(SizeType.Absolute, 50);
                tlpMachineScheduleList.RowStyles[6] = new RowStyle(SizeType.Absolute, 10);
            }
            else
            {
                tlpMachineScheduleList.RowStyles[5] = new RowStyle(SizeType.Absolute, 0);
                tlpMachineScheduleList.RowStyles[6] = new RowStyle(SizeType.Absolute, 0);
            }
        }
        bool EDITING_JOB_RUNNING = false;

        private void LoadMachineSchedule()
        {
            MachineSelectionDataLoading = true;

            if (DT_ITEM?.Rows.Count <= 0)
            {
                DT_ITEM = dalItem.Select();
            }

            DataTable dt_Schedule = NewScheduleTable();

            DataRow row_Schedule;
            bool match = true;
            string previousLocation = null;
            string previousMachine = null;

            foreach (DataRow row in DT_ACTIVE_JOB.Rows)
            {
                match = true;
                
                string jobNo = row[dalPlanning.jobNo].ToString();
                string status = row[dalPlanning.planStatus].ToString();
                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);
                string factory = row[dalMac.MacLocationName].ToString();
                string factoryID = row[dalMac.MacLocationName].ToString();
                string machineID = row[dalMac.MacID].ToString();
                string machineName = row[dalMac.MacName].ToString();
                int familyWith = int.TryParse(row[dalPlan.familyWith].ToString(), out familyWith) ? familyWith : -1;

                #region Filtering

                //status filtering
                if (!status.Equals(text.planning_status_pending) && !status.Equals(text.planning_status_running) && !status.Equals(text.planning_status_draft))
                {
                    match = false;
                }

                #endregion

                if (match)
                {
                    if (previousLocation == null)
                    {
                        previousLocation = factory;
                    }
                    else if (!previousLocation.Equals(factory))
                    {
                        //row_Schedule = dt_Schedule.NewRow();
                        //dt_Schedule.Rows.Add(row_Schedule);
                        previousLocation = factory;
                    }

                    if (previousMachine == null)
                    {
                        previousMachine = machineID;
                    }
                    else if (!previousMachine.Equals(machineID))
                    {
                        //row_Schedule = dt_Schedule.NewRow();
                        //dt_Schedule.Rows.Add(row_Schedule);

                        previousMachine = machineID;
                    }
                    else
                    {
                        //factory = "";
                        //machineName = "";
                    }

                    row_Schedule = dt_Schedule.NewRow();

                    if(JOB_EDITING_MODE && LIST_EDITING_JOB_NO.Contains(jobNo))
                    {
                        if(status == text.planning_status_running)
                        {
                            EDITING_JOB_RUNNING = true;
                        }
                        status = text.planning_status_edting;
                    }

                    row_Schedule[text.Header_Status] = status;
                    row_Schedule[text.Header_Ori_Status] = status;
                    row_Schedule[text.Header_JobNo] = jobNo;
                    row_Schedule[text.Header_DateStart] = start;
                    row_Schedule[text.Header_EstDateEnd] = end;
                    row_Schedule[text.Header_Ori_DateStart] = start;
                    row_Schedule[text.Header_Ori_EstDateEnd] = end;

                    row_Schedule[text.Header_Fac] = factory;
                    row_Schedule[text.Header_FacID] = factoryID;
                    row_Schedule[text.Header_MacID] = machineID;
                    row_Schedule[text.Header_MacName] = machineName;

                    if (familyWith > -1)
                    {
                        row_Schedule[text.Header_FamilyWithJobNo] = familyWith;
                        row_Schedule[text.Header_Remark] = "Family Mould";
                    }

                    string itemCode = row[dalItem.ItemCode].ToString();
                    string itemName = row[dalItem.ItemName].ToString();

                    row_Schedule[text.Header_ItemDescription] = itemName + "\n(" + itemCode + ")";
                    row_Schedule[text.Header_ItemName] = itemName;
                    row_Schedule[text.Header_ItemCode] = itemCode;

                    row_Schedule[text.Header_RawMat_1] = row[dalPlanning.materialCode];

                    string colorMatCode = row[dalPlanning.colorMaterialCode].ToString();
                    string colorMatName = tool.getItemNameFromDataTable(DT_ITEM, colorMatCode);

                    colorMatName = string.IsNullOrEmpty(colorMatName) ? colorMatCode : colorMatName;

                    //row_Schedule[headerColor] = row[dalItem.ItemColor];
                    row_Schedule[text.Header_ColorMatCode] = colorMatCode;
                    row_Schedule[text.Header_ColorMat] = colorMatName;
                    row_Schedule[text.Header_Color] = row[dalItem.ItemColor];

                    //pro time info
                    row_Schedule[text.Header_ProductionDay] = row[dalPlan.productionDay];
                    row_Schedule[text.Header_ProductionHour] = row[dalPlan.productionHour];
                    row_Schedule[text.Header_ProductionHourPerDay] = row[dalPlan.productionHourPerDay];

                    dt_Schedule.Rows.Add(row_Schedule);
                }

            }

            dgvMacSchedule.DataSource = null;

            //Idle machine adding
            foreach (var machine in MachineList)
            {

                // Check if the machine id from MachineList is not found in dt_Schedule.
                if (!dt_Schedule.AsEnumerable().Any(row => !row.IsNull(text.Header_MacID) && row.Field<int>(text.Header_MacID) == machine.Item3))
                {
                    // Add a new row for this machine.
                    DataRow idleRow = dt_Schedule.NewRow();

                    // Populate the necessary fields for this row.
                    idleRow[text.Header_Status] = text.planning_status_idle;
                    idleRow[text.Header_FacID] = machine.Item1;
                    idleRow[text.Header_Fac] = machine.Item2;
                    idleRow[text.Header_MacID] = machine.Item3;
                    idleRow[text.Header_MacName] = machine.Item4;

                    // Add the row to the DataTable.
                    dt_Schedule.Rows.Add(idleRow);
                }
            }

            dt_Schedule.DefaultView.Sort = text.Header_FacID + " ASC," + text.Header_MacID + " ASC," + text.Header_DateStart + " ASC";
            dt_Schedule = dt_Schedule.DefaultView.ToTable();

            if (dt_Schedule.Rows.Count > 0)
            {
                DataTable dt_Schedule_After_Idle = dt_Schedule.Clone();

                previousLocation = null;
                previousMachine = null;

                foreach (DataRow row in dt_Schedule.Rows)
                {
                    string factoryID = row[text.Header_FacID].ToString();
                    string machineID = row[text.Header_MacID].ToString();

                    if (previousLocation == null)
                    {
                        previousLocation = factoryID;
                    }
                    else if (!previousLocation.Equals(factoryID))
                    {
                        dt_Schedule_After_Idle.Rows.Add(dt_Schedule_After_Idle.NewRow());
                        previousLocation = factoryID;
                    }

                    if (previousMachine == null)
                    {
                        previousMachine = machineID;
                    }
                    else if (!previousMachine.Equals(machineID))
                    {
                        dt_Schedule_After_Idle.Rows.Add(dt_Schedule_After_Idle.NewRow());
                        previousMachine = machineID;
                    }
                    else
                    {
                        //row[text.Header_Fac] = "";
                        //row[text.Header_MacName] = "";
                    }

                    // Create new DataRow and copy content from 'row'
                    DataRow newRow = dt_Schedule_After_Idle.NewRow();
                    newRow.ItemArray = row.ItemArray; // Copy data
                    dt_Schedule_After_Idle.Rows.Add(newRow);
                }



                dgvMacSchedule.DataSource = dt_Schedule_After_Idle;
                dgvUIEdit(dgvMacSchedule);
                MacScheduleListCellFormatting(dgvMacSchedule);
                dgvMacSchedule.ClearSelection();
            }

            MachineSelectionDataLoading = false;

        }

        private bool MachineSelectionDataLoading = false;

        public void dgvMacScheduleRowFirstDisplay(string reference)
        {
            DataGridView dgv = dgvMacSchedule;

            if (dgv.Rows.Count > 0 && dgv.Columns.Contains(text.Header_JobNo))
            {
                //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //dgv.ClearSelection();


                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string data = dgv.Rows[i].Cells[text.Header_JobNo].Value.ToString();

                    if (data == reference)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = i;

                        break;
                    }
                }
            }
        }

        private void machineSelectionMode()
        {
            errorProvider1.Clear();

            if (!MachineSelectionDataLoading)
            {
                frmLoading.ShowLoadingScreen();

                MachineSelectionDataLoading = true;

                int maxShot = int.TryParse(lblMaxShot.Text, out maxShot) ? maxShot : 0;

                if (maxShot > 0)
                {
                    MachineSelectionSettingInitial();
                    LoadMachineSchedule();

                    if(JOB_EDITING_MODE)
                    {
                        dtpStartDate.Value = DateTime.TryParse(DT_JOBEDITING_ROW[text.Header_DateStart].ToString(), out DateTime start) ? start : dtpStartDate.Value;

                        dgvMacScheduleRowFirstDisplay(EDITING_JOB_NO);
                    }
                }
                else
                {
                    MessageBox.Show("Please set a target quantity for production before proceeding with a stock check action.");
                }
                MachineSelectionDataLoading = false;

                frmLoading.CloseForm();

            }

        }

        private void lblLoadProductionHistory_Click(object sender, EventArgs e)
        {
            if (dgvItemList?.Rows.Count == 1)
            {
                frmLoading.ShowLoadingScreen();

                frmMachineSchedule frm = new frmMachineSchedule(dgvItemList.Rows[0].Cells[text.Header_ItemCode].Value.ToString());

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                frm.Size = new Size(1600, 930);
                frm.Show();

                frmLoading.CloseForm();
            }

        }

        bool ableLoadData = true;
        private void cmbMacLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMacLocation.Text != null && ableLoadData)
            {
                ableLoadData = false;

                loadMachine(cmbMac);

                ableLoadData = true;
            }

        }

        private bool MacScheduleRowSelectionChanging = false;

        private void SelectRowByMacId()
        {
            if (cmbMac.SelectedItem == null || dgvMacSchedule?.Rows.Count <= 0 || MacScheduleRowSelectionChanging)
                return;

            MacScheduleRowSelectionChanging = true;

            DataRowView drv = (DataRowView)cmbMac.SelectedItem;
            string selectedMacId = drv[dalMac.MacID].ToString();

            for (int i = 0; i < dgvMacSchedule.Rows.Count; i++)
            {
                if (dgvMacSchedule.Rows[i].Cells[text.Header_MacID].Value.ToString() == selectedMacId)
                {
                    dgvMacSchedule.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }

            MacScheduleRowSelectionChanging = false;

        }
        private void AddNewDraftToSchedule()
        {
            if (cmbMac.SelectedItem == null || dgvMacSchedule?.Rows.Count <= 0)
                return;

            DataTable dt_MacSchedule = (DataTable)dgvMacSchedule.DataSource;
            string facID = "";
            string facName = "";

            //remove from old row index
            for (int i = dt_MacSchedule.Rows.Count - 1; i >= 0; i--)
            {
                bool editingMode =  dt_MacSchedule.Rows[i][text.Header_Status].ToString() == text.planning_status_edting;

                if (dt_MacSchedule.Rows[i][text.Header_Status].ToString() == text.planning_status_new_draft || editingMode)
                {
                    // remove this row from dgvMacSchedule
                    dt_MacSchedule.Rows.RemoveAt(i);
                }
            }

            DataRowView drv = (DataRowView)cmbMac.SelectedItem;
            string selectedMacId = drv[dalMac.MacID].ToString();
            string selectedMacName = drv[dalMac.MacName].ToString();

            int rowToInsert = -1;
            bool sameMacIDFound = false;

            //search new row to insert
            for (int i = 0; i < dt_MacSchedule.Rows.Count; i++)
            {
                if (dt_MacSchedule.Rows[i][text.Header_MacID].ToString() == selectedMacId)
                {
                    sameMacIDFound = true;
                    facID = dt_MacSchedule.Rows[i][text.Header_FacID].ToString();
                    facName = dt_MacSchedule.Rows[i][text.Header_Fac].ToString();

                    //insert to last row under same mac ID
                    for (int j = i + 1; j < dt_MacSchedule.Rows.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dt_MacSchedule.Rows[j][text.Header_MacID].ToString()))
                        {
                            rowToInsert = j;


                            break;
                        }
                    }
                    break;
                }
            }

            //insert to new row index
            if (sameMacIDFound)
            {
                DateTime start = dtpStartDate.Value.Date;
                DateTime end = dtpEstimateEndDate.Value.Date;

                string itemColor = "";
                string RawMaterial = "";
                string colorMaterial = "";

                if (dgvRawMatList?.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvRawMatList.Rows)
                    {
                        if (string.IsNullOrEmpty(RawMaterial))
                        {
                            RawMaterial = row.Cells[text.Header_ItemDescription].Value.ToString();
                        }
                        else
                        {
                            RawMaterial += " + " + row.Cells[text.Header_ItemDescription].Value.ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(COLOR_MAT_CODE))
                {
                    colorMaterial = COLOR_MAT_CODE;
                    itemColor = txtItemColor.Text;
                }

                if (dgvItemList?.RowCount > 0)
                {
                    foreach (DataGridViewRow row in dgvItemList.Rows)
                    {
                        DataRow newRow = dt_MacSchedule.NewRow();
                        newRow[text.Header_Status] = text.planning_status_new_draft;

                        if(JOB_EDITING_MODE)
                        {
                            newRow[text.Header_Status] = text.planning_status_edting;
                        }

                        newRow[text.Header_ItemDescription] = row.Cells[text.Header_ItemDescription].Value.ToString();
                        newRow[text.Header_RawMat_1] = RawMaterial;
                        newRow[text.Header_ColorMat] = colorMaterial;
                        newRow[text.Header_Color] = itemColor;
                        newRow[text.Header_DateStart] = start;
                        newRow[text.Header_EstDateEnd] = end;
                        newRow[text.Header_FacID] = facID;
                        newRow[text.Header_Fac] = facName;
                        newRow[text.Header_MacID] = selectedMacId;
                        newRow[text.Header_Mac] = selectedMacName;

                        if (dgvItemList.Rows.Count > 1)
                            newRow[text.Header_FamilyWithJobNo] = 0;

                        if (rowToInsert > -1)
                        {
                            // insert new row at dgvMacShedule (datagridview) row index : rowToInsert
                            dt_MacSchedule.Rows.InsertAt(newRow, rowToInsert);

                            rowToInsert++;
                        }
                        else
                        {
                            // add new row to dgvMacShedule (datagridview)
                            dt_MacSchedule.Rows.Add(newRow);
                        }
                    }
                }


            }

            dgvMacSchedule.DataSource = dt_MacSchedule;
            MacScheduleListCellFormatting(dgvMacSchedule);
        }

        private void cmbMacID_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (!BLOCK_CMB_AUTO_UPDATING)
            {
                CLEAR_SELECTION_AFTER_DROP = false;
                if (cmbMacLocation.Text != null && ableLoadData)
                {

                    if (cmbMac.SelectedItem != null)
                    {
                        DataRowView drv = (DataRowView)cmbMac.SelectedItem;
                        cmbMacLocation.Text = tool.getFactoryNameFromMachineID(drv[dalMac.MacID].ToString());
                    }

                }
                AddNewDraftToSchedule();
                SelectRowByMacId();

                dgvMacSchedule.ClearSelection();
            }

        }

        private bool NEWDRAFT_DATE_UPDATING = false;

        private void NewDraftProductionDateUpdate()
        {
            if(dgvMacSchedule?.Rows.Count > 0 && !NEWDRAFT_DATE_UPDATING)
            {
                NEWDRAFT_DATE_UPDATING = true;

                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                DateTime start = dtpStartDate.Value.Date;
                DateTime end = dtpEstimateEndDate.Value.Date;

                foreach (DataRow row in dt.Rows)
                {
                    string status = row[text.Header_Status].ToString();

                    if(status == text.planning_status_new_draft || status == text.planning_status_edting)
                    {
                        row[text.Header_DateStart] = start;
                        row[text.Header_EstDateEnd] = end;
                    }
                }

                MacScheduleListCellFormatting(dgvMacSchedule);

                NEWDRAFT_DATE_UPDATING = false;
            }


        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            EstEndDateUpdate();
        }

        private void label15_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now;
        }

        private void cbIncludeSunday_CheckedChanged(object sender, EventArgs e)
        {
            EstEndDateUpdate();
        }

        bool BLOCK_CMB_AUTO_UPDATING = false;
        private void ChangeCMBLocationAndMachine()
        {
            if(MAC_SCHEDULE_ROW_TO_DROP != null)
            {
                BLOCK_CMB_AUTO_UPDATING = true;

                string fac = MAC_SCHEDULE_ROW_TO_DROP[text.Header_FacID].ToString();
                string mac = MAC_SCHEDULE_ROW_TO_DROP[text.Header_MacID].ToString();

                if(!string.IsNullOrEmpty(fac) && !string.IsNullOrEmpty(mac))
                {
                    int machineId = Convert.ToInt32(mac);
                    var machine = MachineList.FirstOrDefault(t => t.Item3 == machineId);

                    string machineName = machine?.Item4;

                    cmbMacLocation.Text = fac;
                    cmbMac.Text = machineName;

                }

                BLOCK_CMB_AUTO_UPDATING = false;
            }
        }

        private void ChangeCMBLocationAndMachine(string fac, string mac)
        {
            if (!string.IsNullOrEmpty(fac) && !string.IsNullOrEmpty(mac))
            {
                BLOCK_CMB_AUTO_UPDATING = true;

                int machineId = Convert.ToInt32(mac);
                var machine = MachineList.FirstOrDefault(t => t.Item3 == machineId);

                string machineName = machine?.Item4;

                cmbMacLocation.Text = fac;
                cmbMac.Text = machineName;

                BLOCK_CMB_AUTO_UPDATING = false;
            }

        }
        #region Drag & Drop

        private DataRow MAC_SCHEDULE_ROW_TO_DROP;
        private string OLD_MACHINE_ID = null;

        private void dgvMacShedule_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo = dgvMacSchedule.HitTest(e.X, e.Y);
            CLEAR_SELECTION_AFTER_DROP = false;

            if (hitTestInfo.RowIndex != -1)
            {
                dgvMacSchedule.Rows[hitTestInfo.RowIndex].Selected = true;
                //lblMachineSelectionRemark.Text = hitTestInfo.RowIndex.ToString();

                string status = dgvMacSchedule.Rows[hitTestInfo.RowIndex].Cells[text.Header_Status].Value.ToString();

                if ((status == text.planning_status_new_draft || status == text.planning_status_edting) && !EDITING_JOB_RUNNING)
                {
                    OLD_MACHINE_ID = dgvMacSchedule.Rows[hitTestInfo.RowIndex].Cells[text.Header_MacID].Value.ToString();

                    rowIndexFromMouseDown = hitTestInfo.RowIndex;
                    DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                    MAC_SCHEDULE_ROW_TO_DROP = dt.NewRow();
                    MAC_SCHEDULE_ROW_TO_DROP.ItemArray = dt.Rows[rowIndexFromMouseDown].ItemArray;

                    dgvMacSchedule.Rows[rowIndexFromMouseDown].Selected = true;

                    dgvMacSchedule.DoDragDrop(dgvMacSchedule.Rows[rowIndexFromMouseDown], DragDropEffects.Move);

                    CLEAR_SELECTION_AFTER_DROP = true;

                    //ChangeCMBLocationAndMachine();
                }
            }
        }

        private void dgvMacSchedule_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                e.Effect = DragDropEffects.Move;

            }
        }

        private void dgvMacSchedule_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dgvMacSchedule.PointToClient(new Point(e.X, e.Y));
            int rowIndexToDrop = dgvMacSchedule.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndexToDrop != -1 && e.Effect == DragDropEffects.Move)
            {
                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                DataRow draggedRow = dt.Rows[rowIndexFromMouseDown];
                string Drop_familyWithID = draggedRow[text.Header_FamilyWithJobNo].ToString();

                dt.Rows.RemoveAt(rowIndexFromMouseDown);

                string newFacID = "";
                string newMacID = "";
                string newFacName = "";
                string newMacName = "";

                int NEWrowIndexToDrop = 0;

                if (rowIndexFromMouseDown> rowIndexToDrop)
                {
                    NEWrowIndexToDrop = 1;
                }
                for(int i = rowIndexToDrop - 1 + NEWrowIndexToDrop; i >= 0; i--)
                {
                    string facID = dt.Rows[i][text.Header_FacID].ToString();
                    string facName = dt.Rows[i][text.Header_Fac].ToString();
                    string macID = dt.Rows[i][text.Header_MacID].ToString();
                    string macName = dt.Rows[i][text.Header_Mac].ToString();

                    if (!string.IsNullOrEmpty(facID) && !string.IsNullOrEmpty(macID))
                    {
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_FacID] = facID;
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_Fac] = facName;
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_MacID] = Convert.ToInt32(macID);
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_Mac] = macName;


                        newFacID = facID;
                        newMacID = macID;
                        newFacName = facName;
                        newMacName = macName;

                        NEWrowIndexToDrop = i;
                        break;
                    }
                   
                }

                
                
                rowIndexToDrop = NEWrowIndexToDrop + 1;

                //lblStartDate.Text = rowIndexFromMouseDown.ToString();
                //lblEndDate.Text = rowIndexToDrop.ToString(); 

                //check if Family Mould Row, if family mould = row++ to next non family mould
                //To-DO: 

                string familyWithID = null;
                int rowIndexOffSet = 0;

                foreach(DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    int familyWith = int.TryParse(row[text.Header_FamilyWithJobNo].ToString(), out familyWith)? familyWith : 0;
                    string itemDescription = row[text.Header_ItemDescription].ToString();
                    string jobNo = row[text.Header_JobNo].ToString();

                    if (rowIndex == rowIndexToDrop && familyWith > 0)
                    {
                        familyWithID = familyWith.ToString();

                        rowIndexOffSet++;
                    }
                    else if(familyWithID == familyWith.ToString() && familyWith > 0 && familyWithID != null)
                    {
                        rowIndexOffSet++;
                    }
                }

                int dtRowCount = dt.Rows.Count;

                rowIndexToDrop += rowIndexOffSet;

                dt.Rows.InsertAt(MAC_SCHEDULE_ROW_TO_DROP, rowIndexToDrop);


                if (!string.IsNullOrEmpty(Drop_familyWithID) && Drop_familyWithID != "-1")
                {
                    List<DataRow> rowsToMove = new List<DataRow>();
                    List<DataRow> rowsToDelect = new List<DataRow>();

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = dt.Rows[i];
                        string rowFamilyWithID = row[text.Header_FamilyWithJobNo].ToString();
                        if (rowFamilyWithID == Drop_familyWithID && i != rowIndexToDrop)
                        {
                            DataRow cloneRow = dt.NewRow();
                            cloneRow.ItemArray = row.ItemArray;
                            rowsToMove.Add(cloneRow);

                            rowsToDelect.Add(row);
                        }
                    }

                    rowIndexToDrop++;

                    foreach (DataRow row in rowsToDelect)
                    {
                        dt.Rows.Remove(row);

                        rowIndexToDrop --;
                    }

                    // Insert rows at the new position.
                    foreach (DataRow row in rowsToMove)
                    {

                        row[text.Header_FacID] = newFacID;
                        row[text.Header_MacID] = Convert.ToInt32(newMacID);

                        row[text.Header_Fac] = newFacName;
                        row[text.Header_Mac] = newMacName;

                        dt.Rows.InsertAt(row, rowIndexToDrop);
                        rowIndexToDrop++;
                    }
                }




                ChangeCMBLocationAndMachine(newFacID, newMacID);


                //if (rowIndexToDrop > dt.Rows.Count)
                //{
                //    dt.Rows.Add(MAC_SCHEDULE_ROW_TO_DROP);

                //}
                //else
                //{
                //    dt.Rows.InsertAt(MAC_SCHEDULE_ROW_TO_DROP, rowIndexToDrop);

                //}

                if (OLD_MACHINE_ID != null)
                ResetOldMachineJobDate(OLD_MACHINE_ID);

                MacScheduleListCellFormatting(dgvMacSchedule);
                rowIndexOfItemUnderMouseToDrop = rowIndexToDrop;

            }
        }

        private void ResetOldMachineJobDate(string machineID)
        {
            if(dgvMacSchedule?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                foreach(DataRow row in dt.Rows)
                {
                    string status = row[text.Header_Status].ToString();


                    if (row[text.Header_MacID].ToString() == machineID && status != text.planning_status_new_draft && status != text.planning_status_edting)
                    {
                        row[text.Header_DateStart] = row[text.Header_Ori_DateStart];
                        row[text.Header_EstDateEnd] = row[text.Header_Ori_EstDateEnd];
                        row[text.Header_Status] = row[text.Header_Ori_Status];
                    }
                }
            }
        }

        bool CLEAR_SELECTION_AFTER_DROP = false;
        private void dgvMacSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(CLEAR_SELECTION_AFTER_DROP)
            {
                dgvMacSchedule.ClearSelection();

                if (rowIndexOfItemUnderMouseToDrop > -1 && rowIndexOfItemUnderMouseToDrop < dgvMacSchedule.Rows.Count)
                    dgvMacSchedule.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;

               
            }
        }

        #endregion

        private void dtpEstimateEndDate_ValueChanged(object sender, EventArgs e)
        {
            NewDraftProductionDateUpdate();
        }

        #region Date Adjustment
        private int TotalSundaysBetween(DateTime startDate, DateTime endDate)
        {
            int totalSundays = 0;

            if(endDate < startDate)
            {
                DateTime tmp = startDate;
                startDate = endDate;
                endDate = tmp;
            }

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    totalSundays++;
                }
            }

            return totalSundays;
        }

        private int TotalDaysBetween(DateTime startDate, DateTime endDate)
        {
            int totalDays = 0;

            if (endDate < startDate)
            {
                DateTime tmp = startDate;
                startDate = endDate;
                endDate = tmp;
            }

            TimeSpan span = endDate - startDate;

            return (int)span.TotalDays + 1;// adding 1 to make it inclusive
        }

        private DateTime CalculateEndDate(DateTime startDate, int totalDays, bool includeSunday)
        {
            DateTime endDate = startDate;
            int dayCount = 0;

            totalDays = totalDays < 0 ? 0 : totalDays;

            while (dayCount < totalDays)
            {
                endDate = endDate.AddDays(1);

                // If the day is a Sunday and Sundays should not be included, skip it.
                if (endDate.DayOfWeek == DayOfWeek.Sunday && !includeSunday)
                    continue;

                dayCount++;
            }

            return endDate;
        }

        private DateTime CalculateNewDraftEndDate(DateTime Start)
        {
            int day = int.TryParse(lblDaysNeeded.Text, out day) ? day : 0;

            double balHours = double.TryParse(lblbalHours.Text, out balHours) ? balHours : 0;

            if (balHours > 0)
            {
                day++;
            }
            if (day < 2)
            {
                day = 0;
            }
            else
            {
                day--;
            }

            if (!cbIncludeSunday.Checked)
            {
                for (var date = Start; date <= Start.AddDays(day); date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        day++;
                    }
                }
            }

            DateTime End  = Start.AddDays(day);

            return End;
        }

        private void AutoAdjustCollisionDate()
        {
            if(dgvMacSchedule?.Rows.Count > 0)
            {
                string currentMacID = null;
                string LastFamilyWith = null;
                DateTime LastRow_EndDate = DateTime.MaxValue;

                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    string MacID = row[text.Header_MacID].ToString();
                    string FamilyWith = row[text.Header_FamilyWithJobNo].ToString();
                    string status = row[text.Header_Status].ToString();

                  

                    bool startError = false;
                    bool endError = false;

                    DateTime Date_Start = DateTime.TryParse(row[text.Header_DateStart].ToString(), out Date_Start) ? Date_Start : DateTime.MaxValue;
                    DateTime Date_End = DateTime.TryParse(row[text.Header_EstDateEnd].ToString(), out Date_End) ? Date_End : DateTime.MaxValue;

                    if (!string.IsNullOrEmpty(MacID))
                    {
                        if (currentMacID != MacID)
                        {
                            currentMacID = MacID;
                            LastRow_EndDate = Date_End;
                        }
                        else
                        {
                            if (Date_Start < LastRow_EndDate && LastRow_EndDate != DateTime.MaxValue && (LastFamilyWith != FamilyWith || FamilyWith == "-1" ||string.IsNullOrEmpty(FamilyWith)))
                            {
                                startError = true;
                            }
                            else
                            {
                                LastRow_EndDate = Date_End;
                            }

                        }

                        if (Date_End < Date_Start && Date_Start != DateTime.MaxValue)
                        {
                            endError = true;
                          
                        }

                        if( startError || endError)
                        {
                            if (status != text.planning_status_new_draft && status != text.planning_status_edting)
                            {
                                int proDay = int.TryParse(row[text.Header_ProductionDay].ToString(), out proDay) ? proDay : 0;
                                int proHourPerDay = int.TryParse(row[text.Header_ProductionHourPerDay].ToString(), out proHourPerDay) ? proHourPerDay : 0;
                                double proBalHour = double.TryParse(row[text.Header_ProductionHour].ToString(), out proBalHour) ? proBalHour : 0;

                                bool includedSunday = cbIncludeSunday.Checked;
                                int actualProDay = proDay + (proBalHour > 0 ? 1 : 0);

                                int daysBetween = TotalDaysBetween(Date_Start, Date_End);
                                int SundaysBetween = TotalSundaysBetween(Date_Start, Date_End);

                                if (SundaysBetween > 0)
                                {
                                    if (daysBetween - SundaysBetween == actualProDay)
                                    {
                                        includedSunday = false;
                                    }
                                    else if (daysBetween == actualProDay)
                                    {
                                        includedSunday = true;
                                    }
                                }

                                if (startError)
                                {
                                    Date_Start = CalculateEndDate(LastRow_EndDate, 1, includedSunday);
                                }


                                Date_End = CalculateEndDate(Date_Start, actualProDay - 1, includedSunday);


                                row[text.Header_Status] = text.planning_status_to_update;
                            }
                              
                            else
                            {
                                ProDateChanging = true;

                                if (startError)
                                {
                                    Date_Start = CalculateEndDate(LastRow_EndDate, 1, cbIncludeSunday.Checked);
                                }

                                Date_End = CalculateNewDraftEndDate(Date_Start);

                                dtpStartDate.Value = Date_Start;
                                dtpEstimateEndDate.Value = Date_End;

                                ProDateChanging = false;
                            }

                            row[text.Header_DateStart] = Date_Start;
                            row[text.Header_EstDateEnd] = Date_End;

                            LastRow_EndDate = Date_End;
                        }

                        LastFamilyWith = FamilyWith;
                    }

                }

                MacScheduleListCellFormatting(dgvMacSchedule);
                DATE_COLLISION_FOUND = false;
                btnAdjustCollisionDateBySystem.Visible = false;
                adjustMachineScheduleButtonLayout(false);
            }
           else
            {
                MessageBox.Show("Data not found.");
            }
        }

       

        #endregion


        private void tlpMachineSelection_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

      

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if(Validation(CURRENT_STEP))
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

                StepUIUpdate(CURRENT_STEP, true);
            }
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

            StepUIUpdate(CURRENT_STEP,false);
        }

        private void btnAdjustCollisionDateBySystem_Click(object sender, EventArgs e)
        {
            AutoAdjustCollisionDate();
        }

        private void btnJobPublish_Click(object sender, EventArgs e)
        {
            string message = "Confirm to publish this job?";

            if (JOB_EDITING_MODE)
            {
                message = "Confirm to update and publish this job?";
            }

            DialogResult dialogResult = MessageBox.Show(message, "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (JOB_EDITING_MODE)
                {
                    JobEditingUpdate(false);
                }
                else
                {
                    JobAdd(false);

                }
            }
        }

        private void tableLayoutPanel59_Click(object sender, EventArgs e)
        {
            dgvClearSelection();
        }

        private void tableLayoutPanel36_Click(object sender, EventArgs e)
        {
            dgvClearSelection();
        }

        private void tlpJobPlanningStep_Click(object sender, EventArgs e)
        {
            dgvClearSelection();
        }

        private void tableLayoutPanel11_Click(object sender, EventArgs e)
        {
            dgvClearSelection();
        }

        private void ColorUsageChanged()
        {
            double totalRawMat_KG = double.TryParse(lblRawMat.Text, out totalRawMat_KG)? totalRawMat_KG : 0;

            double color_usage = double.TryParse(txtColorMatUsage.Text, out color_usage) ? color_usage : 0;

            color_usage = color_usage >= 1 ? color_usage / 100 : color_usage;

            lblColorMat.Text = (totalRawMat_KG * color_usage).ToString(); 

        }
        private void txtColorMatUsage_TextChanged(object sender, EventArgs e)
        {
            if(!MATERIAL_DATA_UPDATING)
            {
                //ColorUsageChanged();
                CalculateMaxShot();
            }
           
        }

        private void txtCycleTime_TextChanged(object sender, EventArgs e)
        {
            CalculateProductionTime();
        }

        private void btnAddAsDraft_Click(object sender, EventArgs e)
        {
            string message = "Do you confirm to add this job as a Draft?";

            if (JOB_EDITING_MODE)
            {
                message = "Do you confirm to update this job as a Draft?";
            }
                
            DialogResult dialogResult = MessageBox.Show(message, "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if(JOB_EDITING_MODE)
                {
                    JobEditingUpdate(true);
                }
                else
                {
                    JobAdd(true);

                }
            }
        }

        static public bool JOB_ADDED = false;
        static public bool JOB_UPDATED = false;
        static public bool JOB_EDITING_UPDATED = false;
        static public int JOB_ADDED_COUNT = 0;
        static public int JOB_EDITING_UPDATED_COUNT = 0;
        static public int JOB_UPDATED_COUNT = 0;
        static public string NEW_JOB_NO = "";

        private void JobEditingUpdate(bool UpdateAsDraft)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            JOB_EDITING_UPDATED_COUNT = 0;
            JOB_EDITING_UPDATED = false;

            try
            {
                //save planning data
                DateTime date = DateTime.Now;
                BLL_JOB_SUMMARY.plan_note = txtRemark.Text;

                if (UpdateAsDraft)
                {
                    BLL_JOB_SUMMARY.plan_status = text.planning_status_draft;
                }
                else
                {
                    BLL_JOB_SUMMARY.plan_status = text.planning_status_pending;
                }

                BLL_JOB_SUMMARY.plan_added_date = date;
                BLL_JOB_SUMMARY.plan_added_by = MainDashboard.USER_ID;

                BLL_JOB_SUMMARY.plan_updated_date = date;
                BLL_JOB_SUMMARY.plan_updated_by = MainDashboard.USER_ID;

                int familyWith = -1;

                if (DT_SUMMARY_ITEM?.Rows.Count > 0)
                {
                    bool familyMould = DT_SUMMARY_ITEM.Rows.Count > 1;

                    foreach (DataRow row in DT_SUMMARY_ITEM.Rows)
                    {
                        string itemDescription = row[text.Header_ItemDescription].ToString();
                        string itemCode = row[text.Header_ItemCode].ToString();
                        string jobNo = row[text.Header_JobNo].ToString();

                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(jobNo))
                        {
                            BLL_JOB_SUMMARY.plan_id = int.TryParse(jobNo, out int i) ? i : 0;

                            BLL_JOB_SUMMARY.plan_pw = row[text.Header_ProPwShot].ToString();

                            BLL_JOB_SUMMARY.plan_cavity = row[text.Header_Cavity].ToString();

                            BLL_JOB_SUMMARY.part_code = row[text.Header_ItemCode].ToString();

                            BLL_JOB_SUMMARY.production_purpose = row[text.Header_Job_Purpose].ToString();

                            BLL_JOB_SUMMARY.production_target_qty = row[text.Header_TargetQty].ToString();

                            BLL_JOB_SUMMARY.part_name = row[text.Header_ItemName].ToString();

                            int cavity = int.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                            int maxShot = int.TryParse(lblSummaryMouldMaxShot.Text, out maxShot) ? maxShot : 0;

                            BLL_JOB_SUMMARY.production_able_produce_qty = (maxShot * cavity).ToString();

                            BLL_JOB_SUMMARY.family_with = int.TryParse(row[text.Header_FamilyWithJobNo].ToString(), out int familyNo) ? familyNo : -1;

                            if(familyNo > -1)
                            {
                                familyWith = familyNo;
                            }
                            else
                            {
                                BLL_JOB_SUMMARY.family_with = familyWith;
                            }

                            JOB_EDITING_UPDATED = dalPlanningAction.planningUpdate(BLL_JOB_SUMMARY);

                            if (familyMould)
                            {
                                if (familyNo == -1)
                                {
                                    PlanningBLL updateFamily = new PlanningBLL();
                                    updateFamily.plan_id = int.TryParse(jobNo, out i) ? i : 0;
                                    updateFamily.plan_note = txtRemark.Text + text.planning_Family_mould_Remark;
                                    updateFamily.family_with = familyWith;
                                    updateFamily.plan_updated_date = date;
                                    updateFamily.plan_updated_by = MainDashboard.USER_ID;

                                    dalPlanningAction.planningFamilyWithChange(updateFamily, "-1");
                                }

                            }

                            if (JOB_EDITING_UPDATED)
                            {
                                JOB_EDITING_UPDATED_COUNT++;

                                tool.historyRecord(text.System, "Edited Job Updated (ID:" + jobNo + ")", date, MainDashboard.USER_ID);
                            }
                            else
                            {
                                MessageBox.Show("Failed to update Edited Job data");
                                tool.historyRecord(text.System, "Failed to update Edited Job data", date, MainDashboard.USER_ID);
                            }

                        }
                    }

                   
                }
            }

            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                MessageBox.Show(JOB_EDITING_UPDATED_COUNT + " Edited Job(s) Updated.");

                JobUpdate();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void JobAdd(bool NEWDraftJob)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            JOB_ADDED_COUNT = 0;
            JOB_ADDED = false;
            NEW_JOB_NO = "";

            try
            {
                               
                //save planning data
                DateTime date = DateTime.Now;
                BLL_JOB_SUMMARY.plan_note = txtRemark.Text;

                if (NEWDraftJob)
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
                            BLL_JOB_SUMMARY.plan_pw = row[text.Header_ProPwShot].ToString();

                            BLL_JOB_SUMMARY.plan_cavity = row[text.Header_Cavity].ToString();

                            BLL_JOB_SUMMARY.part_code = row[text.Header_ItemCode].ToString();

                            BLL_JOB_SUMMARY.production_purpose = row[text.Header_Job_Purpose].ToString();

                            BLL_JOB_SUMMARY.production_target_qty = row[text.Header_TargetQty].ToString();

                            BLL_JOB_SUMMARY.part_name = row[text.Header_ItemName].ToString();

                            int cavity = int.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                            int maxShot = int.TryParse(lblSummaryMouldMaxShot.Text, out maxShot) ? maxShot : 0;

                            BLL_JOB_SUMMARY.production_able_produce_qty = (maxShot * cavity).ToString();


                            BLL_JOB_SUMMARY.family_with = familyWith;

                            if (familyMould)
                            {
                                if (familyWith == -1)
                                {
                                    jobID = dalPlanningAction.NewplanningAdd(BLL_JOB_SUMMARY);



                                    //update latest family with
                                    familyWith = jobID;

                                    PlanningBLL updateFamily = new PlanningBLL();
                                    updateFamily.plan_id = familyWith;
                                    updateFamily.plan_note = txtRemark.Text + text.planning_Family_mould_Remark;
                                    updateFamily.family_with = familyWith;
                                    updateFamily.plan_updated_date = date;
                                    updateFamily.plan_updated_by = MainDashboard.USER_ID;

                                    dalPlanningAction.planningFamilyWithChange(updateFamily, "-1");
                                }
                                else
                                {
                                    BLL_JOB_SUMMARY.plan_note = txtRemark.Text + text.planning_Family_mould_Remark;

                                    jobID = dalPlanningAction.NewplanningAdd(BLL_JOB_SUMMARY);


                                }


                            }
                            else
                            {
                                jobID = dalPlanningAction.NewplanningAdd(BLL_JOB_SUMMARY);

                            }

                            if (jobID > 0)
                            {
                                tool.historyRecord(text.System, "New Job Added (ID:" + jobID + ")", date, MainDashboard.USER_ID);
                                JOB_ADDED = true;
                                JOB_ADDED_COUNT++;

                                if(string.IsNullOrEmpty(NEW_JOB_NO))
                                {
                                    NEW_JOB_NO = jobID.ToString();
                                }

                            }
                            else
                            {
                                JOB_ADDED = false;
                            }

                        }
                    }
                }


                if (JOB_ADDED)
                {
                    //save material plan to use qty
                    if (DT_SUMMARY_STOCKCHECK.Rows.Count > 0)
                    {
                        //get planID for this planning

                        if (jobID > 0)
                        {
                            if (familyWith > 0)
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
                    MessageBox.Show("Failed to save Job data");
                    tool.historyRecord(text.System, "Failed to save Job data", date, MainDashboard.USER_ID);
                }
            }

            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                MessageBox.Show(JOB_ADDED_COUNT + " Job(s) Added.");

                JobUpdate();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void JobUpdate()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            JOB_UPDATED = false;
            JOB_UPDATED_COUNT = 0;

            DateTime dateNow = DateTime.Now;

            try
            {
                if (dgvMacSchedule?.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                    PlanningBLL uPlanning = new PlanningBLL();
                    uPlanning.plan_updated_date = dateNow;
                    uPlanning.plan_updated_by = MainDashboard.USER_ID;

                    foreach (DataRow row in dt.Rows)
                    {
                        int jobNo = int.TryParse(row[text.Header_JobNo].ToString(), out jobNo) ? jobNo : -1;
                        string status = row[text.Header_Status].ToString();

                        if (jobNo > -1 && status == text.planning_status_to_update)
                        {
                            string OriStatus = row[text.Header_Ori_Status].ToString();
                            string remark = row[text.Header_Remark].ToString();

                            DateTime newStart = DateTime.TryParse(row[text.Header_DateStart].ToString(), out newStart) ? newStart : DateTime.MaxValue;
                            DateTime newEnd = DateTime.TryParse(row[text.Header_EstDateEnd].ToString(), out newEnd) ? newEnd : DateTime.MaxValue;

                            DateTime oriStart = DateTime.TryParse(row[text.Header_Ori_DateStart].ToString(), out oriStart) ? oriStart : DateTime.MaxValue;
                            DateTime oriEnd = DateTime.TryParse(row[text.Header_Ori_EstDateEnd].ToString(), out oriEnd) ? oriEnd : DateTime.MaxValue;

                            int familyWith = int.TryParse(row[text.Header_FamilyWithJobNo].ToString(), out familyWith) ? familyWith : -1;

                            uPlanning.plan_id = jobNo;
                            uPlanning.plan_status = OriStatus;
                            uPlanning.production_start_date = newStart;
                            uPlanning.production_end_date = newEnd;
                            uPlanning.family_with = familyWith;
                            uPlanning.plan_note = remark;

                            JOB_UPDATED = dalPlanningAction.planningStatusAndScheduleChange(uPlanning, OriStatus, oriStart, oriEnd, familyWith);

                            if(JOB_UPDATED)
                            {
                                JOB_UPDATED_COUNT++;
                                tool.historyRecord(text.System, "Job Updated", dateNow, MainDashboard.USER_ID);
                            }
                            else
                            {
                                MessageBox.Show("Failed to update Job data");
                                tool.historyRecord(text.System, "Failed to update job data", dateNow, MainDashboard.USER_ID);
                            }

                        }
                    }
                }


               
            }

            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                if(JOB_UPDATED_COUNT > 0)
                {
                    MessageBox.Show(JOB_UPDATED_COUNT + " Other Job(s) Updated.");
                }

                Close();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        private void LoadSummaryData()
        {
            #region Load Mould & Item Info

            int itemCount = 0;
            int maxShot = int.TryParse(BLL_JOB_SUMMARY.production_max_shot, out maxShot) ? maxShot : 0;

            lblDescriptionItem1.Text = "";
            lblPWPerShotItem1.Text = "";
            lblCavityItem1.Text = "";
            lblTargetQtyItem1.Text = "";
            lblMaxQtyItem1.Text = "";
            lblJobPurposeItem1.Text = "";

            lblDescriptionItem2.Text = "";
            lblPWPerShotItem2.Text = "";
            lblCavityItem2.Text = "";
            lblTargetQtyItem2.Text = "";
            lblMaxQtyItem2.Text = "";
            lblJobPurposeItem2.Text = "";
            lblSummaryItemIndex2.Text = "";

            lblDescriptionItem3.Text = "";
            lblPWPerShotItem3.Text = "";
            lblCavityItem3.Text = "";
            lblTargetQtyItem3.Text = "";
            lblMaxQtyItem3.Text = "";
            lblJobPurposeItem3.Text = "";
            lblSummaryItemIndex3.Text = "";

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

                if (itemCount == 2)
                {
                    lblDescriptionItem2.Text = itemDescription;
                    lblPWPerShotItem2.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem2.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem2.Text = row[text.Header_TargetQty].ToString() + " pcs";
                    lblMaxQtyItem2.Text = maxQty + " pcs";
                    lblJobPurposeItem2.Text = row[text.Header_Job_Purpose].ToString();
                    lblSummaryItemIndex2.Text = "2";

                }
                else if (itemCount == 3)
                {
                    lblDescriptionItem3.Text = itemDescription;
                    lblPWPerShotItem3.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem3.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem3.Text = row[text.Header_TargetQty].ToString() + " pcs";
                    lblMaxQtyItem3.Text = maxQty + " pcs";
                    lblJobPurposeItem3.Text = row[text.Header_Job_Purpose].ToString();
                    lblSummaryItemIndex3.Text = "3";

                }

                else if (itemCount == 1)
                {
                    lblDescriptionItem1.Text = itemDescription;
                    lblPWPerShotItem1.Text = row[text.Header_ProPwShot].ToString() + " g";
                    lblCavityItem1.Text = row[text.Header_Cavity].ToString() + " pcs";
                    lblTargetQtyItem1.Text = row[text.Header_TargetQty].ToString() + " pcs";
                    lblMaxQtyItem1.Text = maxQty + " pcs";
                    lblJobPurposeItem1.Text = row[text.Header_Job_Purpose].ToString();
                }
            }

            if(itemCount == 2)
            {
                tlpSummaryItemList.RowStyles[4] = new RowStyle(SizeType.Percent, 100);
                tlpSummaryItemList.RowStyles[5] = new RowStyle(SizeType.Absolute, 0);
                tlpSummaryItemList.RowStyles[6] = new RowStyle(SizeType.Absolute, 0);

                tlpSummayMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 195);

            }
            else if(itemCount == 3) 
            {
                tlpSummaryItemList.RowStyles[4] = new RowStyle(SizeType.Absolute, 50);
                tlpSummaryItemList.RowStyles[5] = new RowStyle(SizeType.Absolute, 10);
                tlpSummaryItemList.RowStyles[6] = new RowStyle(SizeType.Percent, 100);

                tlpSummayMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 255);
            }
            else
            {
                tlpSummaryItemList.RowStyles[2] = new RowStyle(SizeType.Percent, 100);
                tlpSummaryItemList.RowStyles[3] = new RowStyle(SizeType.Absolute, 0);
                tlpSummaryItemList.RowStyles[4] = new RowStyle(SizeType.Absolute, 0);
                tlpSummaryItemList.RowStyles[5] = new RowStyle(SizeType.Absolute, 0);
                tlpSummaryItemList.RowStyles[6] = new RowStyle(SizeType.Absolute, 0);

                tlpSummayMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 135);
            }

            lblSummaryMouldCode.Text = BLL_JOB_SUMMARY.plan_mould_code;
            lblSummaryMouldTon.Text = BLL_JOB_SUMMARY.plan_mould_ton;

            lblSummaryCycleTime.Text = BLL_JOB_SUMMARY.plan_ct + " s";
            lblSummaryMouldTotalCavity.Text = BLL_JOB_SUMMARY.plan_cavity + " pcs";
            lblSummaryMouldTotalPWPerShot.Text = BLL_JOB_SUMMARY.plan_pw_shot + " g";
            lblSummaryMouldTotalRWPerShot.Text = BLL_JOB_SUMMARY.plan_rw_shot + " g";
            lblSummaryMouldMaxShot.Text = BLL_JOB_SUMMARY.production_max_shot;

            #endregion

            #region Load Material Info


            //if (BLL_JOB_SUMMARY.raw_round_up_to_bag)
            //{
            //    lblMaterialSetting1.Text = "✅ Raw Round-Up To Bag";

            //    if (BLL_JOB_SUMMARY.use_recycle)
            //    {
            //        lblMaterialSetting2.Text = "✅ Recycle Runner Mat.";
            //    }
            //}
            //else if (BLL_JOB_SUMMARY.use_recycle)
            //{
            //    lblMaterialSetting1.Text = "✅ Recycle Runner Mat.";
            //}

            string rawMaterialDescription = "";
            string colorMaterialDescription = "";
            string rawRatio = "";

            int rawMatCount = 1;

            BLL_JOB_SUMMARY.material_code_2 = "";
            BLL_JOB_SUMMARY.material_bag_qty_2 = "";
            BLL_JOB_SUMMARY.raw_material_qty_2 = "";
            BLL_JOB_SUMMARY.raw_mat_ratio_2 = "";

            foreach (DataRow row in DT_SUMMARY_RAW.Rows)
            {
                if (rawMaterialDescription == "")
                {
                    rawMaterialDescription = row[text.Header_ItemDescription].ToString();
                }
                else
                {
                    rawMaterialDescription += "(" + rawRatio + " %)";

                    rawMaterialDescription += " + " + row[text.Header_ItemDescription].ToString();
                    rawMaterialDescription += "(" + rawRatio + " %)";
                }

                if (rawMatCount == 1)
                {
                    BLL_JOB_SUMMARY.material_code = row[text.Header_ItemCode].ToString();
                    BLL_JOB_SUMMARY.material_bag_kg = row[text.Header_KGPERBAG].ToString();
                    BLL_JOB_SUMMARY.material_bag_qty = row[text.Header_Qty_Required_Bag].ToString();
                    BLL_JOB_SUMMARY.raw_material_qty = row[text.Header_Qty_Required_KG].ToString();
                    BLL_JOB_SUMMARY.raw_mat_ratio_1 = row[text.Header_Ratio].ToString();

                    rawMatCount++;
                }
                else
                {
                    BLL_JOB_SUMMARY.material_code_2 = row[text.Header_ItemCode].ToString();
                    BLL_JOB_SUMMARY.material_bag_kg = row[text.Header_KGPERBAG].ToString();
                    BLL_JOB_SUMMARY.material_bag_qty_2 = row[text.Header_Qty_Required_Bag].ToString();
                    BLL_JOB_SUMMARY.raw_material_qty_2 = row[text.Header_Qty_Required_KG].ToString();
                    BLL_JOB_SUMMARY.raw_mat_ratio_2 = row[text.Header_Ratio].ToString();
                }

                

                rawRatio = row[text.Header_Ratio].ToString();
            }

            lblRawDescription.Text = rawMaterialDescription;

            lblColorTitle.Text = "COLOR (" + BLL_JOB_SUMMARY.part_color + ")";
            colorMaterialDescription = tool.getItemName(BLL_JOB_SUMMARY.color_material_code);
            lblColorDescription.Text = colorMaterialDescription;

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

            lblMacLocation.Text = BLL_JOB_SUMMARY.machine_name.ToString();
            lblMachineID.Text = BLL_JOB_SUMMARY.machine_id.ToString();

            #endregion

            #region Load Remark

            txtRemark.Text = "";

            string remark_StockCheckResult = "";

            if(DT_SUMMARY_STOCKCHECK == null)
            {
                DT_SUMMARY_STOCKCHECK = NewStockCheckTable();
            }
            foreach (DataRow row in DT_SUMMARY_STOCKCHECK.Rows)
            {
                double balStock = double.TryParse(row[text.Header_BalStock].ToString(), out balStock) ? balStock : 0;

                if (balStock < 0)
                {
                    string itemDescription = row[text.Header_ItemDescription].ToString();

                    remark_StockCheckResult += "[" + itemDescription + "   " + balStock + "] ";
                }
            }

            txtRemark.Text = remark_StockCheckResult;

            #endregion

        }

        private void tableLayoutPanel27_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddRawMat_Click(object sender, EventArgs e)
        {
            if (JOB_EDITING_MODE)
            {
                MessageBox.Show("You cannot change or remove material in Job Editing Mode.\nIf you wish to modify the material, cancel this Job through the Machine Schedule, then add a new job with the desired material.");
            }
            else
            {
                if (dgvRawMatList?.Rows.Count > 1)
                {
                    MessageBox.Show("The mixing of raw materials is limited to only two types.\nYou can remove one type of raw material and then add it again.");
                }
                else
                {
                    AddRawItem();

                }
            }
           
        }

        private void dgvRawMatList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvRawMatList;

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();


                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string currentHeader = dgv.Columns[colIndex].Name;

                try
                {
                    my_menu.Items.Add(text.RemoveItem).Name = text.RemoveItem;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_RawItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtMouldRWPerShot_TextChanged(object sender, EventArgs e)
        {
            CalculateMaxShot();
        }

        private void dgvRawMatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!listReadOnlyModeChanging)
                dgvReadOnlyModeUpdate(dgvRawMatList, e.RowIndex, e.ColumnIndex);
        }

        private void dgvRawMatList_SelectionChanged(object sender, EventArgs e)
        {
            if (!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;

                if (dgvRawMatList.SelectedCells.Count > 0)  // checking if any cell is selected
                {
                    int rowIndex = dgvRawMatList.SelectedCells[0].RowIndex; // Get the row index of first selected cell
                    int columnIndex = dgvRawMatList.SelectedCells[0].ColumnIndex; // Get the column index of first selected cell

                    dgvReadOnlyModeUpdate(dgvRawMatList, rowIndex, columnIndex);
                }

                listReadOnlyModeChanging = false;
            }
        }

        private void dgvRawMatList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
            string colName = dgvRawMatList.Columns[e.ColumnIndex].Name;

            if (colName == text.Header_RoundUp_ToBag)
            {
                dgvRawMatList.Enabled = false;
                dgvRawMatList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                foreach (DataGridViewRow row in dgvRawMatList.Rows)
                {
                    if (e.RowIndex != row.Index)
                    {
                        row.Cells[text.Header_RoundUp_ToBag].Value = false;
                    }
                    
                    CalculateMaxShot();
                }
                dgvRawMatList.Enabled = true;

            }
            else if (colName == text.Header_Selection)
            {
                dgvRawMatList.Enabled = false;
                dgvRawMatList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                RawMatRatioAdjustment();
                dgvRawMatList.Enabled = true;
            }


        }

        private void dgvRawMatList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(!MATERIAL_DATA_UPDATING)
            {
                string colName = dgvRawMatList.Columns[e.ColumnIndex].Name;

                if (colName == text.Header_RoundUp_ToBag)
                {
                    dgvRawMatList.Enabled = false;

                    CalculateMaxShot();
                    dgvRawMatList.Enabled = true;

                }
                else if (colName == text.Header_Selection)
                {
                    dgvRawMatList.Enabled = false;

                    bool selection = bool.TryParse(dgvRawMatList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out selection) ? selection : false;

                    if (!selection)
                    {
                        dgvRawMatList.Rows[e.RowIndex].Cells[text.Header_RoundUp_ToBag].Value = false;
                    }

                    RawMatRatioAdjustment();
                    dgvRawMatList.Enabled = true;

                }
            }
          

        }

        private void dgvRawMatList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            int colIndex = dgvRawMatList.CurrentCell.ColumnIndex;

            if (dgvRawMatList.IsCurrentCellDirty && dgvRawMatList.Columns[colIndex].Name != text.Header_KGPERBAG && dgvRawMatList.Columns[colIndex].Name != text.Header_Ratio && dgvRawMatList.Columns[colIndex].Name != text.Header_Remark)
            {
                // Commit the checkbox change immediately
                dgvRawMatList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private bool MATERIAL_DATA_UPDATING = false;
        private void dgvRawMatList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(!MATERIAL_DATA_UPDATING)
            {
                string colName = dgvRawMatList.Columns[e.ColumnIndex].Name;

                if (colName == text.Header_KGPERBAG)
                {
                    CalculateMaxShot();
                }
                else if (colName == text.Header_Ratio)
                {
                    RawMatRatioAdjustment_Manual(e.RowIndex);
                }
            }
           
        }


        private void ColumnMeter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }
        private void dgvRawMatList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnMeter_KeyPress);

            int col = dgvRawMatList.CurrentCell.ColumnIndex;
            int row = dgvRawMatList.CurrentCell.RowIndex;
            string colName = dgvRawMatList.Columns[col].Name;

            if (colName.Contains(text.Header_Ratio))
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnMeter_KeyPress);
                }
            }
        }

        private void lblMouldPWPerShot_Click(object sender, EventArgs e)
        {

        }

        private void gunaGradientButton11_Click(object sender, EventArgs e)
        {
            if (JOB_EDITING_MODE)
            {
                MessageBox.Show("You cannot change or edit mould info in Job Editing Mode.\nIf you wish to modify the mould info, cancel this Job through the Machine Schedule, then add a new job with the desired Mould.");
            }
            else
            {

            }
        }

        private void txtHrsPerDay_Leave_1(object sender, EventArgs e)
        {
            string habitData = txtHrsPerDay.Text;

            //check old data
            string oldHabitData = "";

            DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

            foreach (DataRow row in dt.Rows)
            {
                oldHabitData = row[dalHabit.HabitData].ToString();
            }

            if (oldHabitData != habitData)
            {
                //save habit
                uHabit.belong_to = text.habit_belongTo_PlanningPage;
                uHabit.habit_name = text.habit_planning_HourPerDay;
                uHabit.habit_data = habitData;
                uHabit.added_date = DateTime.Now;
                uHabit.added_by = MainDashboard.USER_ID;

                dalHabit.HabitInsertAndHistoryRecord(uHabit);

                //message to ask if want to change current running and pending plan date follow new hour per day
                if (MessageBox.Show("Total working hour per day changed!\nDo you want to update all the current running or pending plan as well?", "Message",
                                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                        //get machine data
                        DataTable dt_plan = dalPlan.Select();
                        int previousMacID = -1, previousFamilyWith = -1;
                        DateTime previousStart = DateTime.Today;
                        DateTime previousEnd = DateTime.Today;

                        foreach (DataRow row in dt_plan.Rows)
                        {
                            string status = row[dalPlan.planStatus].ToString();

                            if (status != text.planning_status_cancelled && status != text.planning_status_completed)
                            {
                                int currentMacID = Convert.ToInt32(row[dalPlan.machineID]);
                                int currentFamilyWith = Convert.ToInt32(row[dalPlan.familyWith]);
                                int oldProDay = Convert.ToInt32(row[dalPlan.productionDay]);
                                float oldProHour = Convert.ToSingle(row[dalPlan.productionHour]);
                                float oldProHourPerDay = Convert.ToSingle(row[dalPlan.productionHourPerDay]);

                                float totalHour = oldProHourPerDay * oldProDay + oldProHour;

                                DateTime oldStart = Convert.ToDateTime(row[dalPlan.productionStartDate]);
                                DateTime oldEnd = Convert.ToDateTime(row[dalPlan.productionEndDate]);

                                int newProDay = 0;
                                float newProHour = 0, newProHourPerDay = 0;

                                if (habitData != oldProHourPerDay.ToString())
                                {
                                    newProHourPerDay = Convert.ToSingle(habitData);

                                    if (status == text.planning_status_running)
                                    {


                                        if (newProHourPerDay != 0)
                                        {
                                            newProDay = Convert.ToInt32(Math.Floor(totalHour / Convert.ToSingle(newProHourPerDay)));
                                            newProHour = totalHour - Convert.ToSingle(newProDay * newProHourPerDay);
                                        }
                                        else
                                        {
                                            newProDay = oldProDay;
                                            newProHour = oldProHour;
                                            newProHourPerDay = oldProHourPerDay;
                                        }

                                        //get total day left 
                                        DateTime today = DateTime.Today;
                                        int proDayLeft = 0;

                                        if (today <= oldEnd)
                                        {
                                            proDayLeft = tool.getNumberOfDayBetweenTwoDate(today, oldEnd, false);
                                        }


                                        int totalProDay = newProDay;

                                        if (newProHour > 0)
                                        {
                                            totalProDay++;
                                        }

                                        //change start & end date
                                        DateTime newStart = DateTime.Today;

                                        DateTime newEnd = new DateTime();

                                        if (proDayLeft <= 0)
                                        {
                                            newStart = oldStart;
                                            newEnd = oldEnd;
                                        }
                                        else
                                        {
                                            float totalHourLeft = oldProHourPerDay * proDayLeft;

                                            int totalProDayLeft = 0;
                                            float totalProHourLeft = 0;

                                            if (newProHourPerDay != 0)
                                            {
                                                totalProDayLeft = Convert.ToInt32(Math.Floor(totalHourLeft / Convert.ToSingle(newProHourPerDay)));
                                                totalProHourLeft = totalHourLeft - Convert.ToSingle(totalProDayLeft * newProHourPerDay);

                                                if (totalProHourLeft > 0)
                                                {
                                                    totalProDayLeft++;
                                                }

                                                totalProDay = totalProDayLeft;
                                            }

                                            newEnd = tool.EstimateEndDate(newStart, totalProDay, false);
                                        }

                                        if (tool.checkIfSunday(newEnd))
                                        {
                                            newEnd.AddDays(1);
                                        }

                                        if (previousMacID == -1)
                                        {
                                            previousMacID = currentMacID;
                                            //previousStart = previousStart;
                                        }
                                        else if (previousMacID == currentMacID)
                                        {
                                            if (previousFamilyWith == currentFamilyWith && previousFamilyWith != -1)
                                            {
                                                newStart = oldStart;
                                            }
                                            else
                                            {
                                                newStart = previousEnd.AddDays(1);

                                                if (tool.checkIfSunday(newStart))
                                                {
                                                    newStart.AddDays(1);
                                                }
                                            }

                                            newEnd = tool.EstimateEndDate(newStart, totalProDay, false);

                                            if (tool.checkIfSunday(newEnd))
                                            {
                                                newEnd.AddDays(1);
                                            }

                                        }
                                        else
                                        {
                                            previousMacID = currentMacID;
                                        }

                                        if (previousFamilyWith == currentFamilyWith && previousFamilyWith != -1)
                                        {
                                            if (previousEnd < newEnd)
                                            {
                                                previousEnd = newEnd;
                                            }
                                        }
                                        else
                                        {
                                            previousEnd = newEnd;
                                        }

                                        if (newProHour < 0)
                                        {
                                            newProHour = 0;
                                        }

                                        previousStart = oldStart;
                                        previousFamilyWith = currentFamilyWith;
                                        //change production day & hour data
                                        uPlan.plan_id = Convert.ToInt32(row[dalPlan.jobNo]);
                                        uPlan.production_hour = newProHour.ToString();
                                        uPlan.production_day = newProDay.ToString();
                                        uPlan.production_hour_per_day = newProHourPerDay.ToString();
                                        uPlan.production_start_date = oldStart.Date;
                                        uPlan.production_end_date = newEnd.Date;
                                        uPlan.plan_updated_date = DateTime.Now;
                                        uPlan.plan_updated_by = MainDashboard.USER_ID;

                                        //update to db
                                        dalPlanAction.planningScheduleAndProDayChange(uPlan, oldProDay.ToString(), oldProHour.ToString(), oldProHourPerDay.ToString(), oldStart.Date.ToString(), newEnd.Date.ToString());
                                    }
                                    else if (status == text.planning_status_pending)
                                    {

                                        if (newProHourPerDay != 0)
                                        {
                                            newProDay = Convert.ToInt32(Math.Floor(totalHour / Convert.ToSingle(newProHourPerDay)));
                                            newProHour = totalHour - Convert.ToSingle(newProDay * newProHourPerDay);
                                        }
                                        else
                                        {
                                            newProDay = oldProDay;
                                            newProHour = oldProHour;
                                            newProHourPerDay = oldProHourPerDay;
                                        }

                                        int totalProDay = newProDay;

                                        if (newProHour > 0)
                                        {
                                            totalProDay++;
                                        }

                                        //change start & end date
                                        DateTime newStart = Convert.ToDateTime(row[dalPlan.productionStartDate]);

                                        DateTime newEnd = tool.EstimateEndDate(newStart, totalProDay, false);



                                        if (tool.checkIfSunday(newEnd))
                                        {
                                            newEnd.AddDays(1);
                                        }



                                        if (previousMacID == -1)
                                        {
                                            currentMacID = previousMacID;

                                        }
                                        else if (previousMacID == currentMacID)
                                        {
                                            if (previousFamilyWith == currentFamilyWith && previousFamilyWith != -1)
                                            {
                                                newStart = previousStart;
                                            }
                                            else
                                            {
                                                newStart = previousEnd.AddDays(1);

                                                if (tool.checkIfSunday(newStart))
                                                {
                                                    newStart.AddDays(1);
                                                }
                                            }


                                            newEnd = tool.EstimateEndDate(newStart, totalProDay, false);

                                            if (tool.checkIfSunday(newEnd))
                                            {
                                                newEnd.AddDays(1);
                                            }

                                        }
                                        else
                                        {
                                            currentMacID = previousMacID;

                                        }

                                        if (previousFamilyWith == currentFamilyWith && previousFamilyWith != -1)
                                        {
                                            if (previousEnd < newEnd)
                                            {
                                                previousEnd = newEnd;
                                            }
                                        }
                                        else
                                        {
                                            previousEnd = newEnd;
                                        }

                                        previousFamilyWith = currentFamilyWith;
                                        previousStart = newStart;

                                        //change production day & hour data
                                        uPlan.plan_id = Convert.ToInt32(row[dalPlan.jobNo]);
                                        uPlan.production_hour = newProHour.ToString();
                                        uPlan.production_day = newProDay.ToString();
                                        uPlan.production_hour_per_day = newProHourPerDay.ToString();
                                        uPlan.production_start_date = newStart.Date;
                                        uPlan.production_end_date = newEnd.Date;
                                        uPlan.plan_updated_date = DateTime.Now;
                                        uPlan.plan_updated_by = MainDashboard.USER_ID;

                                        //update to db
                                        dalPlanAction.planningScheduleAndProDayChange(uPlan, oldProDay.ToString(), oldProHour.ToString(), oldProHourPerDay.ToString(), newStart.Date.ToString(), newEnd.Date.ToString());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tool.saveToTextAndMessageToUser(ex);
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow; // change cursor to normal type
                       
                    }


                }
            }
        }

        private void frmPlanningVer2dot1_Load(object sender, EventArgs e)
        {

        }

        private void btnItemInfoSave_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the item info?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            if (dialogResult == DialogResult.Yes)
            {
                itemBLL uItem = new itemBLL();

                if(dgvItemList?.Rows.Count > 0)
                {
                    DataGridView dgv = dgvItemList;

                    uItem.item_updtd_date = DateTime.Now;
                    uItem.item_updtd_by = MainDashboard.USER_ID;

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();

                        if(!string.IsNullOrEmpty(itemCode))
                        {
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                            uItem.item_code = itemCode;
                            int ton = int.TryParse(lblMouldTon.Text, out ton) ? ton : 0;
                            int cavity = int.TryParse(row.Cells[text.Header_Cavity].Value.ToString(), out cavity) ? cavity : 0;
                            int cycleTime = int.TryParse(txtMouldCycleTime.Text, out cycleTime) ? cycleTime : 0;
                            float pwPerShot = float.TryParse(row.Cells[text.Header_ProPwShot].Value.ToString(), out pwPerShot) ? pwPerShot : 0;
                            float rwPerShot = float.TryParse(txtMouldRWPerShot.Text, out rwPerShot) ? rwPerShot : 0;

                            //uItem.item_quo_ton = string.IsNullOrEmpty(txtQuoTon.Text) ? 0 : Convert.ToInt32(txtQuoTon.Text);
                            
                            uItem.item_pro_ton = ton;
                            uItem.item_cavity = cavity;
                            //uItem.item_quo_ct = string.IsNullOrEmpty(txtQuoCT.Text) ? 0 : Convert.ToInt32(txtQuoCT.Text);
                            uItem.item_pro_ct_to = cycleTime;
                           
                            uItem.item_pro_pw_shot = pwPerShot;
                            uItem.item_pro_rw_shot = rwPerShot;
                           
                            if (dalItem.updateAndHistoryRecord(uItem))
                            {
                                MessageBox.Show(itemCode + "'s info updated!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Item Code found!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Item not found!");

                }


                DT_ITEM = dalItem.Select();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void btnRawMatSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update raw material info for this item?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                itemBLL uItem = new itemBLL();

                if(dgvRawMatList == null || dgvRawMatList.Rows.Count <= 0)
                {
                    MessageBox.Show("Raw material data not found!");
                    Cursor = Cursors.Arrow; // change cursor to normal type

                    return;
                }

                if (dgvItemList?.Rows.Count > 0)
                {
                    DataGridView dgv = dgvItemList;

                    uItem.item_updtd_date = DateTime.Now;
                    uItem.item_updtd_by = MainDashboard.USER_ID;
                    uItem.added_date = uItem.item_updtd_date;
                    uItem.added_by = MainDashboard.USER_ID;

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();

                        if (!string.IsNullOrEmpty(itemCode))
                        {
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                            uItem.item_code = itemCode;

                            string rawMat = dgvRawMatList.Rows[0].Cells[text.Header_ItemCode].Value.ToString();

                            if (!string.IsNullOrEmpty(rawMat))
                            {
                                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                uItem.item_code = itemCode;
                                uItem.item_material = rawMat;

                                if (dalItem.rawMatUpdateAndHistoryRecord(uItem))
                                {
                                    MessageBox.Show(itemCode + "'s raw material info updated!");

                                    dalItem.ItemMaterialFormulaRemove(uItem);

                                    //save item material formula
                                    foreach (DataGridViewRow rowMat in dgvRawMatList.Rows)
                                    {
                                        uItem.mat_code = rowMat.Cells[text.Header_ItemCode].Value.ToString();
                                        uItem.mat_ratio = float.TryParse(rowMat.Cells[text.Header_Ratio].Value.ToString(), out float f) ? f : 0;
                                        uItem.kg_per_bag = float.TryParse(rowMat.Cells[text.Header_KGPERBAG].Value.ToString(), out f) ? f : 25;
                                        uItem.mat_formula_group = 1;

                                        dalItem.ItemMaterialFormulaInsert(uItem);

                                    }
                                }
                             
                            }
                            else
                            {
                                MessageBox.Show("Raw material data not found!");
                                Cursor = Cursors.Arrow; // change cursor to normal type

                                return;
                            }

                           
                        }
                        else
                        {
                            MessageBox.Show("Invalid Item Code found!");
                        }
                    }


                    
                }
                else
                {
                    MessageBox.Show("Item not found!");

                }

                DT_ITEM = dalItem.Select();

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void btnColorMatSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update color material info for this item?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {

                itemBLL uItem = new itemBLL();

                if (dgvItemList?.Rows.Count > 0)
                {
                    DataGridView dgv = dgvItemList;

                    uItem.item_updtd_date = DateTime.Now;
                    uItem.item_updtd_by = MainDashboard.USER_ID;

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();

                        if (!string.IsNullOrEmpty(itemCode))
                        {
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                            string colorMat = COLOR_MAT_CODE;

                            if(!string.IsNullOrEmpty(COLOR_MAT_CODE))
                            {
                                string color = txtItemColor.Text;
                                float colorUsage = float.TryParse(txtColorMatUsage.Text, out colorUsage) ? colorUsage : 0;

                                colorUsage = colorUsage >= 1 ? colorUsage / 100 : colorUsage;

                                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                uItem.item_code = itemCode;
                                uItem.item_mb = colorMat;
                                uItem.item_mb_rate = colorUsage;
                                uItem.item_color = color;

                                if (dalItem.colorMatUpdateAndHistoryRecord(uItem))
                                {
                                    MessageBox.Show(itemCode + "'s color material info updated!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Color Material Code not found!");
                            }
                          
                        }
                        else
                        {
                            MessageBox.Show("Invalid Item Code found!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Item not found!");

                }

                DT_ITEM = dalItem.Select();

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }
    }
}
