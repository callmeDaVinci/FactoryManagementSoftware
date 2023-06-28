﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Math.Distances;
using Accord.Statistics.Moving;
using Accord.Statistics.Running;
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
        private int CALL_WITH_TARGET_QTY = -1;
        #endregion

        public frmPlanningVer2dot1()
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
        }

        public frmPlanningVer2dot1(string itemCode)
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
            CALL_WITH_ITEM_CODE = itemCode;
        }

        public frmPlanningVer2dot1(string itemCode, int targetQty)
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();

            CALL_WITH_ITEM_CODE = itemCode;
            CALL_WITH_TARGET_QTY = targetQty;
        }

        private void frmPlanning_Load(object sender, EventArgs e)
        {
           
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
            dt.Columns.Add(text.Header_RawMat, typeof(string));
            dt.Columns.Add(text.Header_ColorMat, typeof(string));
            dt.Columns.Add(text.Header_Color, typeof(string));
            dt.Columns.Add(text.Header_Remark, typeof(string));

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

            return dt;
        }

        private DataTable NewRawMaterialList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_ItemSelection, typeof(bool));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_BAG, typeof(int));

            dt.Columns.Add(text.Header_KGPERBAG, typeof(decimal));
            dt.Columns.Add(text.Header_KG, typeof(decimal));

            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private DataTable NewColorMaterialList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_ItemSelection, typeof(bool));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_Color, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Percentage, typeof(decimal));
            dt.Columns.Add(text.Header_KG, typeof(decimal));
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

            return dt;

        }
        private DataTable NewMatSummaryList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_Qty, typeof(string));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }
        #region UI/UX

        private void InitialSetting()
        {
            ShowBtnItemSave(false);
            StepsUIUpdate(1);
            MIN_SHOT = 0;

            //hide setting
            ShowOrHideSetting(labelButton_HideSettings);
            RightPanelInitialSetting(0);

            tool.DoubleBuffered(dgvMacSchedule, true);

            CALL_WITH_ITEM_CODE = null;
            CALL_WITH_TARGET_QTY = -1;
    }

        private void ShowOrHideSetting(string LabelButton)
        {
            if (LabelButton == labelButton_ShowSettings)
            {
                tlpLeftPanel.RowStyles[0] = new RowStyle(SizeType.Absolute, 200f);

                tlpSettings.RowStyles[0] = new RowStyle(SizeType.Absolute, 30f);
                tlpSettings.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);


                lblSettingsShowOrHide.Text = labelButton_HideSettings;
            }
            else
            {
                tlpLeftPanel.RowStyles[0] = new RowStyle(SizeType.Absolute, 50f);

                tlpSettings.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                tlpSettings.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);

                lblSettingsShowOrHide.Text = labelButton_ShowSettings;

            }
        }

        private void RightPanelInitialSetting(int step)
        {
            if (step == 0) //panel initial 
            {
                btnStockCheck.Visible = true;
                btnMachineSelection.Visible = true;

                lblStockCheckStatus.Text = "";

                btnEditItemGroup.Visible = false;
                btnMatStockCheck2.Visible = false;

                tlpStockCheckTitle.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);

                tlpRightPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 50f);
                tlpRightPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 50f);

                tlpStockCheckPanel.RowStyles[0] = new RowStyle(SizeType.Absolute, 31f);
                tlpStockCheckPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);
                tlpStockCheckPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 0f);

                tlpMachineSelection.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                tlpMachineSelection.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
                tlpMachineSelection.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);

                dgvStockCheck.DataSource = null;
                dgvMacSchedule.DataSource = null;

            }
            else if (step == 1) //show stock check data
            {
                btnStockCheck.Visible = false;

                btnMachineSelection.Visible = true;

                btnEditItemGroup.Visible = true;
                btnMatStockCheck2.Visible = false;
                tlpStockCheckTitle.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);

                tlpRightPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 50f);
                tlpRightPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 50f);

                tlpStockCheckPanel.RowStyles[0] = new RowStyle(SizeType.Absolute, 31f);
                tlpStockCheckPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);
                tlpStockCheckPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 100f);


                tlpMachineSelection.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                tlpMachineSelection.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
                tlpMachineSelection.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);

                dgvMacSchedule.DataSource = null;
            }
            else if (step == 2) //show machine selection
            {
                btnEditItemGroup.Visible = false;
                btnMatStockCheck2.Visible = true;
                tlpStockCheckTitle.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 150f);

                btnStockCheck.Visible = false;
                btnMachineSelection.Visible = false;

                tlpRightPanel.RowStyles[0] = new RowStyle(SizeType.Absolute, 50f);
                tlpRightPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 100f);

                tlpStockCheckPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                tlpStockCheckPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);
                tlpStockCheckPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 0f);

                tlpMachineSelection.RowStyles[0] = new RowStyle(SizeType.Percent, 0f);
                tlpMachineSelection.RowStyles[1] = new RowStyle(SizeType.Absolute, 150f);
                tlpMachineSelection.RowStyles[2] = new RowStyle(SizeType.Percent, 100f);
            }
        }

        private void NextToMachineSelection()
        {
            btnNextToMachineSelection.Visible = false;

            tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
            tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);
        }

        private void BackToPartInfo()
        {
            //btnNextToMachineSelection.Visible = true;

            //tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            //tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            int smallColumnWidth = 60;

            if (dgv == dgvItemList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_TargetQty].HeaderCell.Style.BackColor = Color.FromArgb(255, 153, 153);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgv.Columns[text.Header_MouldCode].Width = smallColumnWidth;
                dgv.Columns[text.Header_Cavity].Width = smallColumnWidth;
                //dgv.Columns[text.Header_MouldSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ItemSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_MouldCode].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                //dgv.Columns[text.Header_Cavity].Visible = false;
                dgv.Columns[text.Header_ProTon].Visible = false;
                dgv.Columns[text.Header_ProCT].Visible = false;
                //dgv.Columns[text.Header_ProPwShot].Visible = false;
                dgv.Columns[text.Header_ProRwShot].Visible = false;

                int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

                if (itemListRowHeight < 150)
                {
                    tlpItemSelection.RowStyles[1].SizeType = SizeType.Absolute;
                    tlpItemSelection.RowStyles[1].Height = itemListRowHeight;


                    tlpLeftPanel.RowStyles[2].SizeType = SizeType.Absolute;
                    tlpLeftPanel.RowStyles[2].Height = itemListRowHeight + 157;
                }
            }
            else if (dgv == dgvRawMatList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_BAG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_KG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_KGPERBAG].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;



                dgv.Columns[text.Header_ItemSelection].Width = smallColumnWidth;
                dgv.Columns[text.Header_Index].Width = smallColumnWidth;
                dgv.Columns[text.Header_KG].Width = smallColumnWidth;
                dgv.Columns[text.Header_KGPERBAG].Width = smallColumnWidth;
                dgv.Columns[text.Header_BAG].Width = smallColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

                if (itemListRowHeight < 192)
                {
                    tlpMatSelection.RowStyles[1].SizeType = SizeType.Absolute;
                    tlpMatSelection.RowStyles[1].Height = itemListRowHeight;
                }

            }
            else if (dgv == dgvColorMatList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_Percentage].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_KG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_Color].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                int columnWidth = 60;

                dgv.Columns[text.Header_ItemSelection].Width = columnWidth;
                dgv.Columns[text.Header_Index].Width = columnWidth;
                dgv.Columns[text.Header_KG].Width = columnWidth;
                dgv.Columns[text.Header_Percentage].Width = columnWidth;
                dgv.Columns[text.Header_Color].Width = columnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                int itemListRowHeight = dgv.ColumnHeadersHeight + dgv.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

                if (itemListRowHeight < 192)
                {
                    tlpMatSelection.RowStyles[3].SizeType = SizeType.Absolute;
                    tlpMatSelection.RowStyles[3].Height = itemListRowHeight;

                }

            }
            else if (dgv == dgvStockCheck)
            {
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_Type].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

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
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_RawMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_RawMat].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                dgv.Columns[text.Header_RawMat].MinimumWidth = 100;
                dgv.Columns[text.Header_RawMat].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Bold);
                dgv.Columns[text.Header_ColorMat].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[text.Header_ColorMat].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.Columns[text.Header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                dgv.Columns[text.Header_Status].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.Columns[text.Header_MacName].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);


                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;


                dgv.Columns[text.Header_Fac].Width = smallColumnWidth - 10;
                dgv.Columns[text.Header_MacName].Width = smallColumnWidth - 10;
                dgv.Columns[text.Header_Status].Width = smallColumnWidth;
                dgv.Columns[text.Header_DateStart].Width = smallColumnWidth + 5;
                dgv.Columns[text.Header_EstDateEnd].Width = smallColumnWidth + 5;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_MacID].Visible = false;
                dgv.Columns[text.Header_FacID].Visible = false;
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

        private void loadRawMaterialList()
        {

        }

        private void LoadSingleMaterialList()
        {
            if (dgvItemList?.Rows.Count > 0)
            {
                string itemCode = dgvItemList.Rows[0].Cells[text.Header_ItemCode].Value.ToString();

                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }

                DataTable dt_RawMat = NewRawMaterialList();
                DataTable dt_ColorMat = NewColorMaterialList();

                int rawIndex = 1;
                int colorIndex = 1;

                foreach (DataRow row in DT_ITEM.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {

                        string rawMaterial = row[dalItem.ItemMaterial].ToString();

                        var newRawRow = dt_RawMat.NewRow();

                        newRawRow[text.Header_ItemSelection] = true;
                        newRawRow[text.Header_Index] = rawIndex++;
                        newRawRow[text.Header_ItemDescription] = rawMaterial;
                        newRawRow[text.Header_ItemCode] = rawMaterial;
                        newRawRow[text.Header_KGPERBAG] = 25;

                        dt_RawMat.Rows.Add(newRawRow);

                        dgvRawMatList.DataSource = dt_RawMat;
                        dgvUIEdit(dgvRawMatList);
                        RawMatListCellFormatting(dgvRawMatList);
                        dgvRawMatList.ClearSelection();

                        string colorMaterial = row[dalItem.ItemMBatch].ToString();
                        string colorName = row[dalItem.ItemColor].ToString();
                        decimal colorRate = decimal.TryParse(row[dalItem.ItemMBRate].ToString(), out colorRate) ? colorRate : 0;

                        if (colorRate < 1)
                        {
                            colorRate *= 100;
                        }

                        colorRate = decimal.Round(colorRate, 0);

                        var newColorRow = dt_ColorMat.NewRow();
                        newColorRow[text.Header_ItemSelection] = true;
                        newColorRow[text.Header_Index] = colorIndex++;
                        newColorRow[text.Header_ItemDescription] = colorMaterial;
                        newColorRow[text.Header_ItemCode] = colorMaterial;
                        newColorRow[text.Header_Color] = colorName;
                        newColorRow[text.Header_Percentage] = colorRate;

                        dt_ColorMat.Rows.Add(newColorRow);
                        dgvColorMatList.DataSource = dt_ColorMat;
                        dgvUIEdit(dgvColorMatList);
                        ColorMatListCellFormatting(dgvColorMatList);
                        dgvColorMatList.ClearSelection();


                        break;

                    }
                }
            }
            else
            {
                dgvRawMatList.DataSource = null;
                dgvColorMatList.DataSource = null;
                dgvMaterialSummary.DataSource = null;

            }
        }

        private void LoadSingleMaterialList(string itemCode)
        {
            if (DT_ITEM == null)
            {
                DT_ITEM = dalItem.Select();
            }

            DataTable dt_RawMat = NewRawMaterialList();
            DataTable dt_ColorMat = NewColorMaterialList();

            int rawIndex = 1;
            int colorIndex = 1;

            foreach (DataRow row in DT_ITEM.Rows)
            {
                if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                {

                    string rawMaterial = row[dalItem.ItemMaterial].ToString();

                    var newRawRow = dt_RawMat.NewRow();

                    newRawRow[text.Header_ItemSelection] = true;
                    newRawRow[text.Header_Index] = rawIndex++;
                    newRawRow[text.Header_ItemDescription] = rawMaterial;
                    newRawRow[text.Header_ItemCode] = rawMaterial;
                    newRawRow[text.Header_KGPERBAG] = 25;

                    dt_RawMat.Rows.Add(newRawRow);

                    dgvRawMatList.DataSource = dt_RawMat;
                    dgvUIEdit(dgvRawMatList);
                    RawMatListCellFormatting(dgvRawMatList);
                    dgvRawMatList.ClearSelection();

                    string colorMaterial = row[dalItem.ItemMBatch].ToString();
                    string colorName = row[dalItem.ItemColor].ToString();
                    decimal colorRate = decimal.TryParse(row[dalItem.ItemMBRate].ToString(), out colorRate) ? colorRate : 0;

                    if (colorRate < 1)
                    {
                        colorRate *= 100;
                    }

                    colorRate = decimal.Round(colorRate, 0);

                    var newColorRow = dt_ColorMat.NewRow();
                    newColorRow[text.Header_ItemSelection] = true;
                    newColorRow[text.Header_Index] = colorIndex++;
                    newColorRow[text.Header_ItemDescription] = colorMaterial;
                    newColorRow[text.Header_ItemCode] = colorMaterial;
                    newColorRow[text.Header_Color] = colorName;
                    newColorRow[text.Header_Percentage] = colorRate;

                    dt_ColorMat.Rows.Add(newColorRow);
                    dgvColorMatList.DataSource = dt_ColorMat;
                    dgvUIEdit(dgvColorMatList);
                    ColorMatListCellFormatting(dgvColorMatList);
                    dgvColorMatList.ClearSelection();


                    break;

                }
            }
        }

        private readonly string TimePanelTitle = "2. TIME REQUIRED";
        private readonly string DayLabelTitle = "Day";

        private void ClearPannelSummaryInfo()
        {
            gbTime.Text = TimePanelTitle;
            lblDays.Text = DayLabelTitle;
            txtDaysNeeded.Text = "0";
            txtbalHours.Text = "0";

            txtMaxQty.Text = "0";
            txtMaxShot.Text = "0";

            dgvMaterialSummary.DataSource = null;

        }

        #region Calculation
        private void FromMaxShotToTimeNeededCalculation()
        {
            int cycleTime_PerShot_sec = int.TryParse(txtCycleTime.Text, out cycleTime_PerShot_sec) ? cycleTime_PerShot_sec : 0;
            int MaxShots = int.TryParse(txtMaxShot.Text, out MaxShots) ? MaxShots : 0;

            int Max_CycleTime_sec = MaxShots * cycleTime_PerShot_sec;

            gbTime.Text = TimePanelTitle + " (" + Max_CycleTime_sec + " s )";

            double workingHrsPerday = double.TryParse(txtHrsPerDay.Text, out workingHrsPerday) ? workingHrsPerday : 22;

            int totalDays = (int)(Max_CycleTime_sec / 60 / 60 / workingHrsPerday);

            double balHrs = (double)Max_CycleTime_sec / 60 / 60 - (totalDays * workingHrsPerday);

            lblDays.Text = DayLabelTitle + (totalDays > 1 ? "s" : "") + " (" + workingHrsPerday + "hrs)";

            txtDaysNeeded.Text = totalDays.ToString();
            txtbalHours.Text = balHrs.ToString("0.##");

        }

        private void FromMinShotToTotalMaterialCalculation()
        {
            if (MIN_SHOT > 0)
            {
                #region info from user

                int minShots = MIN_SHOT;

                bool RunnerRecycle = cbRunnerRecycle.Checked;
                bool RawMat_RoundUpToBag = cbRoundUpToBag.Checked;

                int cycleTime_PerShot_sec = int.TryParse(txtCycleTime.Text, out cycleTime_PerShot_sec) ? cycleTime_PerShot_sec : 0;
                int cavity_PerShot_pcs = int.TryParse(txtCavity.Text, out cavity_PerShot_pcs) ? cavity_PerShot_pcs : 0;

                double PW_Shot_g = double.TryParse(txtPWPerShot.Text, out PW_Shot_g) ? PW_Shot_g : 0;
                double RW_Shot_g = double.TryParse(txtRWPerShot.Text, out RW_Shot_g) ? RW_Shot_g : 0;

                double TotalWeight_Shot_g = PW_Shot_g + RW_Shot_g;

                double colorRatio_percentage = 0;

                DataTable dt_ColorMat = (DataTable)dgvColorMatList.DataSource;

                if (dt_ColorMat?.Rows.Count > 0 && dt_ColorMat.Columns.Contains(text.Header_Percentage))
                {
                    colorRatio_percentage = double.TryParse(dt_ColorMat.Rows[0][text.Header_Percentage].ToString(), out colorRatio_percentage) ? colorRatio_percentage : 0;
                }

                double newMatWastage = double.TryParse(txtMatWastage.Text, out newMatWastage) ? newMatWastage : 0;

                double recycleWastage = 0;

                double MaterialperBag_KG = 25;

                DataTable dt_RawMat = (DataTable)dgvRawMatList.DataSource;

                if (dt_RawMat?.Rows.Count > 0 && dt_RawMat.Columns.Contains(text.Header_KGPERBAG))
                {
                    MaterialperBag_KG = double.TryParse(dt_RawMat.Rows[0][text.Header_KGPERBAG].ToString(), out MaterialperBag_KG) ? MaterialperBag_KG : 25;

                }

                colorRatio_percentage = colorRatio_percentage >= 1 ? colorRatio_percentage /= 100 : colorRatio_percentage;
                newMatWastage = newMatWastage >= 1 ? newMatWastage /= 100 : newMatWastage;
                recycleWastage = recycleWastage >= 1 ? recycleWastage /= 100 : recycleWastage;

                #endregion

                #region Calculation

                //TO:DO Calculate the below result

                double TotalNewRawMat_g = 0;
                double TotalNewColorMat_g = 0;

                double TotalMinMatNeeded_g = 0;
                double TotalMaxMatProNeeded_g = 0;

                int maxShots = minShots;

                double TotalRecycleMat = 0;
                int extraShotGetFromRecycle = 0;

                if (RunnerRecycle)
                {
                    TotalRecycleMat = (minShots - 1) * RW_Shot_g * (1 - recycleWastage);

                    extraShotGetFromRecycle = (int)Math.Floor(TotalRecycleMat / TotalWeight_Shot_g);
                }

                TotalMinMatNeeded_g = minShots * TotalWeight_Shot_g;

                double TotalNewMat = TotalMinMatNeeded_g - TotalRecycleMat;

                TotalNewRawMat_g = TotalNewMat / (1 + colorRatio_percentage);
                TotalNewColorMat_g = TotalNewRawMat_g * colorRatio_percentage;

                if (TotalNewMat > (TotalNewRawMat_g + TotalNewColorMat_g))
                {
                    TotalNewRawMat_g = TotalNewMat - TotalNewColorMat_g;
                }
                else
                {
                    TotalNewMat = TotalNewRawMat_g + TotalNewColorMat_g;

                }

                TotalMaxMatProNeeded_g = TotalNewMat + TotalRecycleMat;

                maxShots = (int)Math.Floor(TotalMaxMatProNeeded_g / TotalWeight_Shot_g);

                double Prepare_RawMat_g = TotalNewRawMat_g * (1 + newMatWastage);
                double Prepare_ColorMat_g = Prepare_RawMat_g * colorRatio_percentage;

                if (RawMat_RoundUpToBag)
                {
                    double RawMatRoundUpQty = Math.Ceiling(Prepare_RawMat_g / 1000 / MaterialperBag_KG) * MaterialperBag_KG * 1000;

                    RawMatRoundUpQty = RawMatRoundUpQty - Prepare_RawMat_g;

                    Prepare_RawMat_g += RawMatRoundUpQty;

                    Prepare_ColorMat_g = Prepare_RawMat_g * colorRatio_percentage;

                    double RoundUp_Pro_RawMat = RawMatRoundUpQty * (1 - newMatWastage) + TotalNewRawMat_g;

                    double RoundUp_Pro_ColorMat = RoundUp_Pro_RawMat * colorRatio_percentage;


                    double Total_RoundUp_Pro_New_Mat = RoundUp_Pro_RawMat + RoundUp_Pro_ColorMat;

                    maxShots += (int)Math.Floor(RawMatRoundUpQty * (1 - newMatWastage) / TotalWeight_Shot_g);

                    if (RunnerRecycle)
                    {
                        TotalRecycleMat = (maxShots - 1) * RW_Shot_g * (1 - recycleWastage);

                        extraShotGetFromRecycle = (int)Math.Floor(TotalRecycleMat / TotalWeight_Shot_g);

                        maxShots += extraShotGetFromRecycle;
                    }

                    TotalMaxMatProNeeded_g = Total_RoundUp_Pro_New_Mat + TotalRecycleMat;
                }


                if (maxShots < minShots)
                {
                    MessageBox.Show(" MaxShot < MinShot");
                }

                #endregion

                #region Fill in data

                txtRawMat.Text = (Prepare_RawMat_g / 1000).ToString();
                txtColorMat.Text = (Prepare_ColorMat_g / 1000).ToString();
                txtRecycleMat.Text = (TotalRecycleMat / 1000).ToString();

                if (dt_RawMat?.Rows.Count > 0)
                {
                    double kgPerBag = double.TryParse(dt_RawMat.Rows[0][text.Header_KGPERBAG].ToString(), out kgPerBag) ? kgPerBag : 0;

                    dt_RawMat.Rows[0][text.Header_KG] = Prepare_RawMat_g / 1000;
                    dt_RawMat.Rows[0][text.Header_BAG] = (int)(Prepare_RawMat_g / 1000 / kgPerBag);

                }

                if (dt_ColorMat?.Rows.Count > 0 && dt_ColorMat.Columns.Contains(text.Header_KG))
                {
                    dt_ColorMat.Rows[0][text.Header_KG] = Prepare_ColorMat_g / 1000;
                }

                int Max_CycleTime_sec = maxShots * cycleTime_PerShot_sec;
                gbTime.Text = TimePanelTitle + " (" + Max_CycleTime_sec + " s )";

                double workingHrsPerday = double.TryParse(txtHrsPerDay.Text, out workingHrsPerday) ? workingHrsPerday : 22;


                int totalDays = (int)(Max_CycleTime_sec / 60 / 60 / workingHrsPerday);

                double balHrs = (double)Max_CycleTime_sec / 60 / 60 - (totalDays * workingHrsPerday);

                lblDays.Text = DayLabelTitle + (totalDays > 1 ? "s" : "") + " (" + workingHrsPerday + "hrs)";

                txtDaysNeeded.Text = totalDays.ToString();
                txtbalHours.Text = balHrs.ToString("0.##");

                int Max_Qty_pcs = maxShots * cavity_PerShot_pcs;
                txtMaxQty.Text = Max_Qty_pcs.ToString();
                txtMaxShot.Text = maxShots.ToString();

                //dgvMaterialSummary.DataSource = null;

                #endregion
            }
            else
            {
                //reset field
                ClearPannelSummaryInfo();

            }


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
            LoadSingleMaterialList(itemCode);

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
            RightPanelInitialSetting(0);
            //dgvItemList.FirstDisplayedScrollingRowIndex = 0;

        }

        private void RemoveItemFromList(DataGridView dgv, int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count) // Ensure the index is within the valid range
            {

                dgv.Rows.RemoveAt(rowIndex);
                LoadSummary_ProductionInfo();
                IndexReset(dgvItemList);

                LoadSingleMaterialList();
                FromMinShotToTotalMaterialCalculation();
            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "The row index provided is out of range.");
            }
        }

        private DataRow CreateNewItemRow(DataRow row, int index)
        {
            DataRow newRow = DT_ITEM_LIST.NewRow();

            string itemCode = row[dalItem.ItemCodePresent].ToString();
            itemCode = string.IsNullOrEmpty(itemCode) ? row[dalItem.ItemCode].ToString() : itemCode;

            string itemName = row[dalItem.ItemName].ToString();

            string itemDescription = itemName + "\n(" + itemCode + ")";

            newRow[text.Header_Index] = index;
            newRow[text.Header_ItemDescription] = itemDescription;
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_ItemName] = itemName;
            newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
            newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
            newRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
            newRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

           
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

                txtCavity.Text = totalCavity.ToString();
                txtCycleTime.Text = cycleTime.ToString("0");
                txtPWPerShot.Text = partWeightPerShot.ToString("0.##");
                txtRWPerShot.Text = runnerWeightPerShot.ToString("0.##");
                MIN_SHOT = totalShot;

                FromMinShotToTotalMaterialCalculation();

            }

        }
        private void LoadProductionInfoToField(DataRow[] existingRows)
        {
            txtCavity.Text = existingRows[0][dalItem.MouldCavity].ToString();
            txtCycleTime.Text = existingRows[0][dalItem.MouldCT].ToString();
            txtPWPerShot.Text = existingRows[0][dalItem.ItemPWShot].ToString();
            txtRWPerShot.Text = existingRows[0][dalItem.ItemRWShot].ToString();


        }

        private void ClearProductionInfo()
        {
            txtCavity.Text = "";
            txtCycleTime.Text = "";
            txtPWPerShot.Text = "";
            txtRWPerShot.Text = "";
            MIN_SHOT = 0;
        }

        private double TwoDecimalPlace(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private void LoadMatCheckList()
        {
            lblStockCheckStatus.Text = "";

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

                        totalMaterial = float.TryParse(row.Cells[text.Header_KG].Value.ToString(), out totalMaterial) ? totalMaterial : 0;

                        StockBal = availableQty - totalMaterial;

                        DataRow row_dtMat = dt_MAT.NewRow();

                        row_dtMat[text.Header_Index] = index;


                        row_dtMat[text.Header_Type] = text.Cat_RawMat;
                        row_dtMat[text.Header_ItemCode] = matCode;
                        row_dtMat[text.Header_ItemDescription] = matDescription;

                        row_dtMat[text.Header_ReadyStock] = currentStock;
                        row_dtMat[text.Header_ReservedForOtherJobs] = TotalMatToUse;

                        row_dtMat[text.Header_RequiredForCurrentJob] = totalMaterial;
                        row_dtMat[text.Header_BalStock] = StockBal;

                        dt_MAT.Rows.Add(row_dtMat);
                        index++;
                    }
                }

                //add color material to list
                if (dgvColorMatList?.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvColorMatList.Rows)
                    {
                        string matCode = row.Cells[text.Header_ItemCode].Value.ToString();
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

                        totalMaterial = float.TryParse(row.Cells[text.Header_KG].Value.ToString(), out totalMaterial) ? totalMaterial : 0;

                        StockBal = availableQty - totalMaterial;

                        DataRow row_dtMat = dt_MAT.NewRow();

                        row_dtMat[text.Header_Index] = index;

                        row_dtMat[text.Header_Type] = tool.getItemCatFromDataTable(DT_ITEM, matCode);
                        row_dtMat[text.Header_ItemCode] = matCode;
                        row_dtMat[text.Header_ItemDescription] = matDescription;

                        row_dtMat[text.Header_ReadyStock] = currentStock;
                        row_dtMat[text.Header_ReservedForOtherJobs] = TotalMatToUse;

                        row_dtMat[text.Header_RequiredForCurrentJob] = totalMaterial;
                        row_dtMat[text.Header_BalStock] = StockBal;

                        dt_MAT.Rows.Add(row_dtMat);
                        index++;
                    }
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
                dgvStockCheck.DataSource = dt_MAT;
                dgvUIEdit(dgvStockCheck);
                StockCheckListCellFormatting(dgvStockCheck);
                dgvStockCheck.ClearSelection();

                if (MATERIAL_STOCK_ENOUGH)
                {
                    lblStockCheckStatus.Text = "✔";
                    lblStockCheckStatus.ForeColor = Color.Green;

                }
                else
                {
                    lblStockCheckStatus.Text = "❌";
                    lblStockCheckStatus.ForeColor = Color.Red;
                }
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

            DT_ACTIVE_JOB.DefaultView.Sort = dalMac.MacLocation + " ASC," + dalMac.MacID + " ASC," + dalPlan.productionStartDate + " ASC";
            DT_ACTIVE_JOB = DT_ACTIVE_JOB.DefaultView.ToTable();

        }

        #endregion

        private void btnNextToMachineSelection_Click(object sender, EventArgs e)
        {
            NextToMachineSelection();
        }

        private void AddItem()
        {
            DataTable dt = dalItem.CatSearch(text.Cat_Part);

            List<string> itemsToRemove = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                string itemName = row[dalItem.ItemName].ToString();

                if (itemName.ToUpper().Contains(text.Terminated.ToUpper()))
                {
                    string itemCode = row[dalItem.ItemCode].ToString();

                    // Add to list of items to remove
                    itemsToRemove.Add(itemCode);
                }
            }

            if (dgvItemList?.Rows.Count > 0)
            {
                DataTable dt_itemList = (DataTable)dgvItemList.DataSource;

                foreach (DataRow row in dt_itemList.Rows)
                {
                    string itemCode = row[text.Header_ItemCode].ToString();
                    itemsToRemove.Add(itemCode);
                }
            }

            if (itemsToRemove.Count > 0)
            {
                // Only call AcceptChanges once, improving performance
                dt.AcceptChanges();

                // Use LINQ to efficiently filter out the rows
                var rows = dt.AsEnumerable()
                    .Where(r => !itemsToRemove.Contains(r.Field<string>(dalItem.ItemCode)));

                // Convert filtered enumerable back to DataTable
                dt = rows.CopyToDataTable();
            }

            frmItemSearch frm = new frmItemSearch(dt, text.Cat_Part);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();

            frmLoading.CloseForm();

            if (frmItemSearch.ITEM_CODE_SELECTED != "")
            {
                AddItemToList(frmItemSearch.ITEM_CODE_SELECTED);
            }
        }
        private void frmPlanningNEWV2_Shown(object sender, EventArgs e)
        {
            //load item search page
            if (CALL_WITH_ITEM_CODE != null)
            {
                AutoLoadPageWithItemCode();
                MaterialStockCheckMode();
                machineSelectionMode();
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


                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Cavity].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_ProPwShot].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_TargetQty].Style.BackColor = SystemColors.Info;

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

                    if (remark.Contains("Family Mould"))
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.BackColor = Color.Yellow;

                    }
                    if (string.IsNullOrEmpty(macID))
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(245, 247, 255);
                        dgv.Rows[rowIndex].Height = 20;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

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

                    }
                    else if (status == text.planning_status_draft)
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = SystemColors.Info;
                    }
                    else if (status == text.planning_status_new_draft)
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = SystemColors.Info;
                        //dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = SystemColors.Info;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                    }
                    else if (status == text.planning_status_idle)
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.ForeColor = Color.Red;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.ForeColor = Color.Black;
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
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_BAG].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_KGPERBAG].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_Remark].Style.BackColor = SystemColors.Info;
                }

                dgv.ResumeLayout();
            }

        }

        private void ColorMatListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Percentage].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_Color].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_Remark].Style.BackColor = SystemColors.Info;
                }

                dgv.ResumeLayout();
            }

        }
        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //DataGridView dgv = dgvItemList;

            //if (dgv.Columns[colIndex].Name == text.Header_ItemDescription && dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //{
            //    string itemDescription = dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString();

            //    if(string.IsNullOrEmpty(itemDescription))
            //    {
            //        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Black;
            //        //dgv.Rows[rowIndex].Height = 20;
            //    }
            //    else
            //    {
            //        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            //        //dgv.Rows[rowIndex].Height = 50;

            //        dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            //        dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);
            //    }

            //}


        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewRow row in dgvItemList.Rows)
            //{
            //    //DataGridViewCheckBoxCell checkBoxCell = row.Cells[text.Header_MouldSelection] as DataGridViewCheckBoxCell;
            //    //if (row.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //    //{
            //    //    checkBoxCell.ReadOnly = false;  // allow checkbox to be checked/unchecked
            //    //}
            //    //else
            //    //{
            //    //    checkBoxCell.ReadOnly = true;  // disable checkbox
            //    //    //checkBoxCell.Style.BackColor = Color.DimGray;  // indicate disabled state
            //    //}
            //}
        }

        private void dgvItemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //    if (e.ColumnIndex == dgvItemList.Columns[text.Header_MouldSelection].Index &&
            //e.RowIndex >= 0 &&
            //dgvItemList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item) == false)
            //    {
            //        e.PaintBackground(e.CellBounds, true);
            //        e.Handled = true;
            //    }

            //    if (e.ColumnIndex == dgvItemList.Columns[text.Header_ItemSelection].Index &&
            //e.RowIndex >= 0 &&
            //string.IsNullOrEmpty(dgvItemList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString()))
            //    {
            //        e.PaintBackground(e.CellBounds, true);
            //        e.Handled = true;
            //    }
        }

        private void dgvReadOnlyModeUpdate(DataGridView dgv, int rowIndex, int colIndex)
        {
            listReadOnlyModeChanging = true;

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

            listReadOnlyModeChanging = false;

        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!listReadOnlyModeChanging)
                dgvReadOnlyModeUpdate(dgvItemList, e.RowIndex, e.ColumnIndex);
        }
        private bool userInitiatedChange = true;

        private void refreshCavityMatchedQty()
        {
            if (dgvItemList?.Rows.Count > 0)
            {
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

                FromMinShotToTotalMaterialCalculation();

            }
        }

        private void StepsUIUpdate(int step)
        {
            //// reset all fonts to normal
            //ResetFonts();

            //Font focusFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            //// update the font for the current step
            //switch (step)
            //{
            //    case 1:
            //        gbItem.Font = focusFont;
            //        break;
            //    case 2:
            //        gbRawColorMat.Font = focusFont;
            //        break;
            //    case 3:
            //        gbTime.Font = focusFont;
            //        break;
            //    case 4:
            //        gbStockCheck.Font = focusFont;
            //        break;
            //    case 5:
            //        gbMachineSchedule.Font = focusFont;
            //        break;
            //}
        }

        private void ResetFonts()
        {
            Font normalFont = new Font("Segoe UI", 6F, FontStyle.Bold);

            gbItem.Font = normalFont;
            gbRawColorMat.Font = normalFont;
            gbTime.Font = normalFont;
            gbStockCheck.Font = normalFont;
            gbMachineSchedule.Font = normalFont;
        }

        private void dgvItemList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (!PRO_INFO_LOADING && (e.ColumnIndex == dgvItemList.Columns[text.Header_Cavity].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_ProPwShot].Index))
            {
                ShowBtnItemSave(true);
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
            //// If the clicked column is "Mould Selection" or "Item Selection".
            //if (e.ColumnIndex == dgvItemList.Columns[text.Header_ItemSelection].Index)
            //{
            //    // Commit the edit immediately for the checkbox cell.
            //    dgvItemList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
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

        private void tableLayoutPanel26_MouseClick(object sender, MouseEventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private void ShowBtnItemSave(bool show)
        {
        }
        private void ProductionInfo_TextChanged(object sender, EventArgs e)
        {
            if (!PRO_INFO_LOADING)
            {
                ShowBtnItemSave(true);
            }
        }

        private bool UpsertUniqueMouldItemData(itemBLL u)
        {
            bool isSuccess = false;

            // Search for a matching row in DT_MOULD_LIST
            DataRow[] existingRows = DT_MOULD_ITEM.Select($"mould_code = '{u.mould_code}' AND item_code = '{u.item_code}' AND combination_code = '{u.combination_code}'");

            if (existingRows.Length > 0)
            {
                // If a match was found, call the update function
                u.tbl_code = (int)existingRows[0]["tbl_code"];
                isSuccess = updateMouldItem(u);
            }
            else
            {
                // If no match was found, call the insert function
                u.combination_code = 0; // Set combination_code as 0 for unique item info
                isSuccess = insertMouldItem(u);
            }

            return isSuccess;
        }

        private bool InsertNewCombinationGroup(itemBLL u, List<Tuple<string, string>> selectedItems)
        {
            foreach (var item in selectedItems)
            {
                // Item2 of the tuple is itemCode
                u.item_code = item.Item2;

                // Call the method to insert the item
                if (!insertMouldItem(u))
                {
                    MessageBox.Show($"Failed to insert item with item code: {u.item_code}.", "Insertion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }



        private bool updateMouldItem(itemBLL u)
        {
            // Your update logic here
            return dalItem.MouldItemUpdate(u);
        }

        private bool insertMouldItem(itemBLL u)
        {
            // Your insert logic here
            return dalItem.MouldItemInsert(u);

        }

        private void SaveProductionInfo()
        {
            //DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();

            //DataTable dt_MouldList = (DataTable) dgvItemList.DataSource;

            //List<Tuple<string, string>> selectedItems = new List<Tuple<string, string>>();

            //foreach (DataRow row in dt_MouldList.Rows)
            //{
            //    bool itemSelected = bool.TryParse(row[text.Header_ItemSelection].ToString(), out bool parsedSelection) ? parsedSelection : false;

            //    if (itemSelected)
            //    {
            //        string itemCode = row.Field<string>(text.Header_ItemCode);
            //        string itemDescription = row[text.Header_ItemDescription].ToString();
            //        string mouldCode = row[text.Header_MouldCode].ToString();

            //        uItem.mould_code = mouldCode;
            //        uItem.item_code = itemCode;

            //        if (!itemDescription.Contains(text.All_Item))
            //            selectedItems.Add(new Tuple<string, string>(mouldCode, itemCode));
            //    }
            //}

            //if (selectedItems.Select(x => x.Item1).Distinct().Count() > 1 && selectedItems.Count > 1)
            //{
            //    throw new Exception("All selected items must have the same mould code");
            //}

            ////update unique production info data
            //uItem.updated_date = DateTime.Now;
            //uItem.updated_by = MainDashboard.USER_ID;
            //uItem.item_updtd_date = uItem.updated_date;
            //uItem.item_updtd_by = uItem.updated_by;
            //uItem.item_added_date = uItem.updated_date;
            //uItem.item_added_by = uItem.updated_by;
            //uItem.removed = false;

            //uItem.mould_cavity = int.TryParse(txtCavity.Text, out int y) ? y : 0;
            //uItem.mould_ct = int.TryParse(txtCycleTime.Text, out y) ? y : 0;
            //uItem.item_pw_shot = float.TryParse(txtPWPerShot.Text, out float k) ? k : 0;
            //uItem.item_rw_shot = float.TryParse(txtRWPerShot.Text, out k) ? k : 0;

            //uItem.MouldDefaultSelection = true;
            //uItem.combination_code = 0;

            //if (selectedItems.Count == 1)
            //{
            //    UpsertUniqueMouldItemData(uItem);

            //}
            //else if (selectedItems.Count > 1)
            //{
            //    bool foundCombination = false;

            //    foreach (var combination in DT_MOULD_ITEM.AsEnumerable()
            //                                                        .Where(r => r.Field<string>(dalItem.MouldCode) == uItem.mould_code && r.Field<int>(dalItem.CombinationCode) != 0)
            //                                                        .GroupBy(r => r.Field<int>(dalItem.CombinationCode)))
            //    {
            //        var itemsInCombination = combination.Select(r => r.Field<string>(dalItem.ItemCode)).ToArray();
            //        var selectedItemCodes = selectedItems.Select(i => i.Item2).ToArray();

            //        if (!selectedItemCodes.Except(itemsInCombination).Any() && !itemsInCombination.Except(selectedItemCodes).Any())
            //        {
            //            uItem.combination_code = combination.Key;
            //            foundCombination = true;
            //            dalItem.MouldItemProInfoUpdateByCombinationCode(uItem);
            //            break;
            //        }
            //    }

            //    if (!foundCombination)
            //    {
            //        // If we haven't found a combination, generate a new combination code
            //        var maxCombinationCode = DT_MOULD_ITEM.AsEnumerable()
            //                                              .Where(r => r.Field<string>("mould_code") == uItem.mould_code)
            //                                              .Max(r => r.Field<int>("combination_code"));

            //        uItem.combination_code = maxCombinationCode + 1;

            //        // Call insert method here
            //        InsertNewCombinationGroup(uItem, selectedItems);
            //    }

            //}
            //else
            //{
            //    ClearProductionInfo();

            //}

            //DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
        }

        private void Step2_MaterialSetting()
        {
            dgvItemList.ClearSelection();

            StepsUIUpdate(2);
            if (dgvItemList?.Rows.Count == 1)
            {
                string itemCode = dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_ItemCode].Value.ToString();

                LoadSingleMaterialList(itemCode);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AddItem();

        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            AddItem();
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
                            uItem.item_cavity = int.TryParse(txtCavity.Text, out int i) ? i : 0;
                            uItem.item_pro_ct_to = int.TryParse(txtCycleTime.Text, out i) ? i : 0;
                            uItem.item_pro_pw_shot = int.TryParse(txtPWPerShot.Text, out i) ? i : 0;
                            uItem.item_pro_rw_shot = int.TryParse(txtRWPerShot.Text, out i) ? i : 0;
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

        private readonly string labelButton_ShowSettings = "Show Settings";
        private readonly string labelButton_HideSettings = "Hide Settings";
        private void label15_Click(object sender, EventArgs e)
        {
            ShowOrHideSetting(lblSettingsShowOrHide.Text);

        }

        private void cbRunnerRecycle_CheckedChanged(object sender, EventArgs e)
        {
            FromMinShotToTotalMaterialCalculation();
        }

        private void cbRoundUpToBag_CheckedChanged(object sender, EventArgs e)
        {
            FromMinShotToTotalMaterialCalculation();
        }

        private void txtMatWastage_TextChanged(object sender, EventArgs e)
        {
            FromMinShotToTotalMaterialCalculation();
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
            int maxShot = int.TryParse(txtMaxShot.Text, out maxShot) ? maxShot : 0;

            if (maxShot > 0)
            {
                LoadMatCheckList();
                RightPanelInitialSetting(1);
            }
            else
            {
                MessageBox.Show("Please set a target quantity for production before proceeding with a stock check action.");
            }
        }

        private void btnStockCheck_Click(object sender, EventArgs e)
        {
            MaterialStockCheckMode();
        }

        private void btnAddColorMat_Click(object sender, EventArgs e)
        {

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


        private void txtMaxShot_TextChanged(object sender, EventArgs e)
        {
            RightPanelInitialSetting(0);
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
                    string macLocation = row[dalMac.MacLocation].ToString();

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
                    string macLocation = row[dalMac.MacLocation].ToString();

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

        private void EstEndDateUpdate()
        {
            DateTime Start = dtpStartDate.Value;

            int day = int.TryParse(txtDaysNeeded.Text, out day) ? day : 0;

            double balHours = double.TryParse(txtbalHours.Text, out balHours) ? balHours : 0;

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

            dtpEstimateEndDate.Value = Start.AddDays(day);

        }
        private void MachineSelectionSettingInitial()
        {
            tool.loadProductionFactory(cmbMacLocation);

            cmbMac.SelectedIndex = -1;

            LoadMachineHistoryRemark();

            StartDateInitial();

        }

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

            //string factorySearching = cmbMacLocation.Text;
            //string machineSearching = "";

            //if(cmbMac.SelectedIndex > -1)
            //{
            //    DataRowView drv = (DataRowView)cmbMac.SelectedItem;
            //    machineSearching = drv[dalMac.MacID].ToString();
            //}

            foreach (DataRow row in DT_ACTIVE_JOB.Rows)
            {
                match = true;

                string status = row[dalPlanning.planStatus].ToString();
                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);
                string factory = row[dalMac.MacLocation].ToString();
                string factoryID = row[dalMac.MacLocation].ToString();
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

                    row_Schedule[text.Header_Status] = status;
                    row_Schedule[text.Header_JobNo] = row[dalPlanning.planID];
                    row_Schedule[text.Header_DateStart] = start;
                    row_Schedule[text.Header_EstDateEnd] = end;

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

                    row_Schedule[text.Header_ItemDescription] = itemName + " (" + itemCode + ")";
                    row_Schedule[text.Header_ItemName] = itemName;
                    row_Schedule[text.Header_ItemCode] = itemCode;

                    row_Schedule[text.Header_RawMat] = row[dalPlanning.materialCode];

                    string colorMatCode = row[dalPlanning.colorMaterialCode].ToString();
                    string colorMatName = tool.getItemNameFromDataTable(DT_ITEM, colorMatCode);

                    colorMatName = string.IsNullOrEmpty(colorMatName) ? colorMatCode : colorMatName;

                    //row_Schedule[headerColor] = row[dalItem.ItemColor];
                    row_Schedule[text.Header_ColorMatCode] = colorMatCode;
                    row_Schedule[text.Header_ColorMat] = colorMatName;
                    row_Schedule[text.Header_Color] = row[dalItem.ItemColor];

                    dt_Schedule.Rows.Add(row_Schedule);
                }

            }

            dgvMacSchedule.DataSource = null;

            //To - Do
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
                    idleRow[text.Header_FacID] = machine.Item2;
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
                        row[text.Header_Fac] = "";
                        row[text.Header_MacName] = "";
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

        private void machineSelectionMode()
        {
            int maxShot = int.TryParse(txtMaxShot.Text, out maxShot) ? maxShot : 0;

            if (maxShot > 0)
            {
                if (!MATERIAL_STOCK_ENOUGH)
                {
                    DialogResult dialogResult = MessageBox.Show("The stock of materials is insufficient.\nAre you sure you want to proceed to machine selection?", "Message",
                                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (string.IsNullOrEmpty(lblStockCheckStatus.Text))
                {
                    MessageBox.Show("Please perform a Material Stock Check action first before proceeding to Machine Selection");

                    return;
                }

                RightPanelInitialSetting(2);
                MachineSelectionSettingInitial();
                LoadMachineSchedule();
            }
            else
            {
                MessageBox.Show("Please set a target quantity for production before proceeding with a stock check action.");
            }
        }
        private void btnMachineSelection_Click(object sender, EventArgs e)
        {
            CLEAR_SELECTION_AFTER_DROP = false;
            machineSelectionMode();
        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {

        }

        private void btnMatStockCheck2_Click(object sender, EventArgs e)
        {
            int maxShot = int.TryParse(txtMaxShot.Text, out maxShot) ? maxShot : 0;

            if (maxShot > 0)
            {
                LoadMatCheckList();
                RightPanelInitialSetting(1);
            }
            else
            {
                MessageBox.Show("Please set a target quantity for production before proceeding with a stock check action.");
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
                frm.Size = new Size(1700, 800);
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



            for (int i = dt_MacSchedule.Rows.Count - 1; i >= 0; i--)
            {
                if (dt_MacSchedule.Rows[i][text.Header_Status].ToString() == text.planning_status_new_draft)
                {
                    // remove this row from dgvMacSchedule
                    dt_MacSchedule.Rows.RemoveAt(i);
                }
            }

            DataRowView drv = (DataRowView)cmbMac.SelectedItem;
            string selectedMacId = drv[dalMac.MacID].ToString();

            int rowToInsert = -1;
            bool sameMacIDFound = false;

            for (int i = 0; i < dt_MacSchedule.Rows.Count; i++)
            {
                if (dt_MacSchedule.Rows[i][text.Header_MacID].ToString() == selectedMacId)
                {
                    sameMacIDFound = true;
                    facID = dt_MacSchedule.Rows[i][text.Header_FacID].ToString();

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

                if (dgvColorMatList?.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvColorMatList.Rows)
                    {
                        if (string.IsNullOrEmpty(colorMaterial))
                        {
                            colorMaterial = row.Cells[text.Header_ItemDescription].Value.ToString();
                            itemColor = row.Cells[text.Header_Color].Value.ToString();
                        }
                        else
                        {
                            colorMaterial += " + " + row.Cells[text.Header_ItemDescription].Value.ToString();
                            itemColor = row.Cells[text.Header_Color].Value.ToString();
                        }
                    }
                }

                if (dgvItemList?.RowCount > 0)
                {
                    foreach (DataGridViewRow row in dgvItemList.Rows)
                    {
                        DataRow newRow = dt_MacSchedule.NewRow();
                        newRow[text.Header_Status] = text.planning_status_new_draft;
                        newRow[text.Header_ItemDescription] = row.Cells[text.Header_ItemDescription].Value.ToString();
                        newRow[text.Header_RawMat] = RawMaterial;
                        newRow[text.Header_ColorMat] = colorMaterial;
                        newRow[text.Header_Color] = itemColor;
                        newRow[text.Header_DateStart] = start;
                        newRow[text.Header_EstDateEnd] = end;
                        newRow[text.Header_FacID] = facID;
                        newRow[text.Header_MacID] = selectedMacId;

                        if (rowToInsert > -1)
                        {
                            // insert new row at dgvMacShedule (datagridview) row index : rowToInsert
                            dt_MacSchedule.Rows.InsertAt(newRow, rowToInsert);
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

                int machineId = Convert.ToInt32(mac); 
                var machine = MachineList.FirstOrDefault(t => t.Item3 == machineId);

                string machineName = machine?.Item4;

                cmbMacLocation.Text = fac;
                cmbMac.Text = machineName;

                BLOCK_CMB_AUTO_UPDATING = false;
            }
        }
        private DataRow MAC_SCHEDULE_ROW_TO_DROP;
        private void dgvMacShedule_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo = dgvMacSchedule.HitTest(e.X, e.Y);
            CLEAR_SELECTION_AFTER_DROP = false;

            if (hitTestInfo.RowIndex != -1)
            {
                dgvMacSchedule.Rows[hitTestInfo.RowIndex].Selected = true;
                lblMachineSelectionRemark.Text = hitTestInfo.RowIndex.ToString();

                string status = dgvMacSchedule.Rows[hitTestInfo.RowIndex].Cells[text.Header_Status].Value.ToString();

                if (status == text.planning_status_new_draft)
                {
                    rowIndexFromMouseDown = hitTestInfo.RowIndex;
                    DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                    MAC_SCHEDULE_ROW_TO_DROP = dt.NewRow();
                    MAC_SCHEDULE_ROW_TO_DROP.ItemArray = dt.Rows[rowIndexFromMouseDown].ItemArray;

                    dgvMacSchedule.Rows[rowIndexFromMouseDown].Selected = true;

                    dgvMacSchedule.DoDragDrop(dgvMacSchedule.Rows[rowIndexFromMouseDown], DragDropEffects.Move);

                    CLEAR_SELECTION_AFTER_DROP = true;

                    ChangeCMBLocationAndMachine();
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

                dt.Rows.RemoveAt(rowIndexFromMouseDown);


               
                int NEWrowIndexToDrop = 0;
               

                if(rowIndexFromMouseDown> rowIndexToDrop)
                {
                    NEWrowIndexToDrop = 1;
                }
                for(int i = rowIndexToDrop - 1 + NEWrowIndexToDrop; i >= 0; i--)
                {
                    string facID = dt.Rows[i][text.Header_FacID].ToString();
                    string macID = dt.Rows[i][text.Header_MacID].ToString();

                    if(!string.IsNullOrEmpty(facID) && !string.IsNullOrEmpty(macID))
                    {
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_FacID] = facID;
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_MacID] = Convert.ToInt32(macID);

                        NEWrowIndexToDrop = i;
                        break;
                    }
                   
                }

                
                rowIndexToDrop = NEWrowIndexToDrop + 1;

                lblStartDate.Text = rowIndexFromMouseDown.ToString();
                lblEndDate.Text = rowIndexToDrop.ToString(); 

                dt.Rows.InsertAt(MAC_SCHEDULE_ROW_TO_DROP, rowIndexToDrop);

                MacScheduleListCellFormatting(dgvMacSchedule);
                rowIndexOfItemUnderMouseToDrop = rowIndexToDrop;


            }
        }

        bool CLEAR_SELECTION_AFTER_DROP = false;
        private void dgvMacSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(CLEAR_SELECTION_AFTER_DROP)
            {
                dgvMacSchedule.ClearSelection();

                if (rowIndexOfItemUnderMouseToDrop > -1)
                    dgvMacSchedule.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;

               
            }
        }

       
    }
}
