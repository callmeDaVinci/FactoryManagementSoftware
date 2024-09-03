using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using System.Linq;
using System.Configuration;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

using TextBox = System.Windows.Forms.TextBox;
using Task = System.Threading.Tasks.Task;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;
using Range = Microsoft.Office.Interop.Excel.Range;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMachineScheduleVer2 : Form
    {
        public frmMachineScheduleVer2()
        {
            InitializeComponent();
            InitializeData();

            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(filterHide);
        }

        public frmMachineScheduleVer2(string itemCode)
        {
            InitializeComponent();
            InitializeData();

            btnNewJob.Hide();
            //btnMatList.Hide();
            btnExcel.Hide();

            txtSearch.Text = itemCode;

            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(filterHide);

            ItemProductionHistoryChecking = true;
        }

        public frmMachineScheduleVer2(bool _fromDailyJobRecord)
        {

            InitializeComponent();
            InitializeData();
            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnNewJob.Hide();
            //btnExcel.Hide();
            //btnMatList.Hide();


            fromDailyRecord = _fromDailyJobRecord;
            btnExcel.Text = "ADD ITEM";
            btnExcel.Width = 180;
            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300);
            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(filterHide);
        }

        public frmMachineScheduleVer2(string action, int data_1, string data_2)
        {

            InitializeComponent();
            InitializeData();

            btnNewJob.Hide();
            //btnExcel.Hide();
            //btnMatList.Hide();

            btnExcel.Text = action;
            btnExcel.Width = 180;
            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 300);
            tool.DoubleBuffered(dgvMacSchedule, true);
            HideFilter(false);
            buttionAction = action;

            if (action == text.DailyAction_ChangePlan)
            {
                ChangePlanToAction = true;

                ITEM_CODE = data_2;
                PLAN_ID = data_1;

                cbCompleted.Checked = true;
                cbCancelled.Checked = true;
                cbSearchByJobNo.Checked = true;
                cbItem.Checked = false;
            }

        }

        #region Variable/ object setting

        userDAL dalUser = new userDAL();

        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        planningActionDAL dalPlanningAction = new planningActionDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        MacDAL dalMac = new MacDAL();
        facDAL dalFac = new facDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();

        habitDAL dalHabit = new habitDAL();

        private int PLAN_ID = -1;
        private string ITEM_CODE = "";
        private string buttionAction = "";
        int userPermission = -1;

        private string startDateChecking = null;

        private string CELL_EDITING_OLD_VALUE = "";
        private string CELL_EDITING_NEW_VALUE = "";

        private bool CELL_VALUE_CHANGED = false;

        private bool loaded = false;
        private bool ableLoadData = true;
        private bool filterHide = true;
        private bool fromDailyRecord = false;
        private bool ItemProductionHistoryChecking = false;
        private bool ChangePlanToAction = false;

        private bool requestingStocktake_Part;
        private bool requestingStocktake_RawMat;
        private bool requestingStocktake_ColorMat;

        private DataTable DT_STOCKTAKE_PART;
        private DataTable DT_STOCKTAKE_RAWMAT;
        private DataTable DT_STOCKTAKE_COLORMAT;

        #endregion

        #region UI Setting

        private void HideFilter(bool hide)
        {
            if (hide)
            {
                btnFilter.Text = "Show Filter";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                //tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 85f);
                //tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Percent, 83f);
            }
            else
            {
                btnFilter.Text = "Hide Filter";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Absolute, 120f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Absolute, 45f);
                //tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 62f);
            }
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Fac, typeof(string));
            dt.Columns.Add(text.Header_Mac, typeof(string));
            dt.Columns.Add(text.Header_Status, typeof(string));
            dt.Columns.Add(text.Header_JobNo, typeof(int));
            dt.Columns.Add(text.Header_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ItemNameAndCode, typeof(string));
            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_MaxOutput, typeof(int));
            dt.Columns.Add(text.Header_ProducedQty, typeof(int));
            dt.Columns.Add(text.Header_RawMat_String, typeof(string));
            dt.Columns.Add(text.Header_RawMat_Qty, typeof(string));
            dt.Columns.Add(text.Header_Color, typeof(string));
            dt.Columns.Add(text.Header_ColorMat, typeof(string));
            dt.Columns.Add(text.Header_ColorRate, typeof(int));
            dt.Columns.Add(text.Header_ColorMat_KG, typeof(double));
            dt.Columns.Add(text.Header_Remark, typeof(string));
            dt.Columns.Add(text.Header_Job_Purpose, typeof(string));

            dt.Columns.Add(text.Header_FacID, typeof(int));
            dt.Columns.Add(text.Header_MacID, typeof(int));
            dt.Columns.Add(text.Header_Ori_Status, typeof(string));
            dt.Columns.Add(text.Header_Ori_Machine_Name, typeof(string));
            dt.Columns.Add(text.Header_Ori_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_Ori_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_ProductionDay, typeof(int));
            dt.Columns.Add(text.Header_ProductionHourPerDay, typeof(double));
            dt.Columns.Add(text.Header_ProductionHour, typeof(double));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemCode_Present, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(int));
            dt.Columns.Add(text.Header_Cavity, typeof(int));
            dt.Columns.Add(text.Header_ProPwShot, typeof(double));
            dt.Columns.Add(text.Header_ProRwShot, typeof(double));

            dt.Columns.Add(text.Header_RawMat_1, typeof(string));
            dt.Columns.Add(text.Header_RawMat_Bag_1, typeof(int));
            dt.Columns.Add(text.Header_RawMat_KG_1, typeof(double));
            dt.Columns.Add(text.Header_RawMat_1_Ratio, typeof(double));

            dt.Columns.Add(text.Header_RawMat_2, typeof(string));
            dt.Columns.Add(text.Header_RawMat_Bag_2, typeof(int));
            dt.Columns.Add(text.Header_RawMat_KG_2, typeof(double));
            dt.Columns.Add(text.Header_RawMat_2_Ratio, typeof(double));

            dt.Columns.Add(text.Header_Recycle_Qty, typeof(int));
            dt.Columns.Add(text.Header_ColorMatCode, typeof(string));

            dt.Columns.Add(text.Header_FamilyWithJobNo, typeof(string));

            return dt;
        }

        private DataTable NewStocktakeTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Fac, typeof(string));

            dt.Columns.Add(text.Header_ItemDescription, typeof(string));

            dt.Columns.Add(text.Header_Stocktake, typeof(string));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private void dgvUIEdit(DataGridView dgv)
        {

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgv.Columns[text.Header_ColorMatCode].Visible = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(93, 127, 236);

            dgv.Columns[text.Header_ItemCode].Visible = false;
            dgv.Columns[text.Header_ItemCode_Present].Visible = false;
            dgv.Columns[text.Header_ItemName].Visible = false;
            dgv.Columns[text.Header_Recycle_Qty].Visible = false;
            dgv.Columns[text.Header_FacID].Visible = false;
            dgv.Columns[text.Header_MacID].Visible = false;
            dgv.Columns[text.Header_Ori_DateStart].Visible = false;
            dgv.Columns[text.Header_Ori_EstDateEnd].Visible = false;
            dgv.Columns[text.Header_ProductionDay].Visible = false;
            dgv.Columns[text.Header_ProductionHourPerDay].Visible = false;
            dgv.Columns[text.Header_ProductionHour].Visible = false;
            dgv.Columns[text.Header_ProCT].Visible = false;

            dgv.Columns[text.Header_Recycle_Qty].Visible = false;
            dgv.Columns[text.Header_ColorMatCode].Visible = false;

            dgv.Columns[text.Header_Cavity].Visible = false;

            dgv.Columns[text.Header_ProPwShot].Visible = false;
            dgv.Columns[text.Header_ProRwShot].Visible = false;

            dgv.Columns[text.Header_RawMat_1].Visible = false;
            dgv.Columns[text.Header_RawMat_2].Visible = false;
            dgv.Columns[text.Header_RawMat_Bag_1].Visible = false;
            dgv.Columns[text.Header_RawMat_Bag_2].Visible = false;
            dgv.Columns[text.Header_RawMat_KG_1].Visible = false;
            dgv.Columns[text.Header_RawMat_KG_2].Visible = false;
            dgv.Columns[text.Header_RawMat_1_Ratio].Visible = false;
            dgv.Columns[text.Header_RawMat_2_Ratio].Visible = false;
            dgv.Columns[text.Header_Ori_Status].Visible = false;
            dgv.Columns[text.Header_Ori_Machine_Name].Visible = false;

           // dgv.Columns[text.Header_ColorRate].Visible = false;
            dgv.Columns[text.Header_FamilyWithJobNo].Visible = false;


            Color normalColor = Color.Black;

            Color importantColor = Color.OrangeRed;
            //Color importantColor = Color.FromArgb(96, 127, 255);

            dgv.Columns[text.Header_Mac].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[text.Header_JobNo].DefaultCellStyle.ForeColor = importantColor;

            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[text.Header_ItemNameAndCode].MinimumWidth = 180;
            dgv.Columns[text.Header_ItemNameAndCode].Frozen = true;


            dgv.Columns[text.Header_Remark].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_Job_Purpose].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dgv.Columns[text.Header_RawMat_String].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgv.Columns[text.Header_RawMat_String].MinimumWidth = 100;
            dgv.Columns[text.Header_RawMat_String].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[text.Header_TargetQty].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[text.Header_ProducedQty].DefaultCellStyle.ForeColor = Color.Green;

            dgv.Columns[text.Header_RawMat_Qty].DefaultCellStyle.ForeColor = importantColor;

            dgv.Columns[text.Header_ColorMat_KG].DefaultCellStyle.ForeColor = importantColor;

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[text.Header_Job_Purpose].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            dgv.Columns[text.Header_Mac].DefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_TargetQty].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dgv.Columns[text.Header_RawMat_Qty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_JobNo].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[text.Header_ProducedQty].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dgv.Columns[text.Header_ColorMat_KG].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            dgv.Columns[text.Header_Remark].MinimumWidth = 100;
            dgv.Columns[text.Header_Job_Purpose].MinimumWidth = 120;
            dgv.Columns[text.Header_Fac].MinimumWidth = 70;
            dgv.Columns[text.Header_Mac].MinimumWidth = 60;


            dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        #endregion

        #region Load Data

        private bool DATE_COLLISION_FOUND = false;

        private void adjustAdjustCollisionDateButtonLayout()
        {
            
            if (DATE_COLLISION_FOUND)
            {
                btnAdjustCollisionDateBySystem.Text = "Adjust Collision Date";
                btnAdjustCollisionDateBySystem.BaseColor1 = Color.FromArgb(255, 192, 159);
                btnAdjustCollisionDateBySystem.BaseColor2 = Color.FromArgb(255, 192, 159);
                btnAdjustCollisionDateBySystem.Visible = true;

                tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Absolute, 10);
                tlpMainSchedule.RowStyles[6] = new RowStyle(SizeType.Absolute, 56);
            }
            else
            {
                if(DATA_TO_UPDATE)
                {
                    btnAdjustCollisionDateBySystem.Text = "Job Update";
                    btnAdjustCollisionDateBySystem.BaseColor1 = Color.FromArgb(216, 236, 201);
                    btnAdjustCollisionDateBySystem.BaseColor2 = Color.FromArgb(216, 236, 201);

                    btnAdjustCollisionDateBySystem.Visible = true;
                    tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Absolute, 10);
                    tlpMainSchedule.RowStyles[6] = new RowStyle(SizeType.Absolute, 56);
                }
                else
                {
                    btnAdjustCollisionDateBySystem.Visible = false;

                    tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Absolute, 0);
                    tlpMainSchedule.RowStyles[6] = new RowStyle(SizeType.Absolute, 0);
                }
               
            }
        }

        private void InitializeData()
        {
            tool.loadProductionFactory(cmbMacLocation);
            loadMachine(cmbMac);
            ResetData();
            adjustAdjustCollisionDateButtonLayout();
            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_FOUR)
            {
                btnNewJob.Show();
                cbDraft.Checked = true;
                cbDraft.Enabled = true;
            }
            else
            {
                btnNewJob.Hide();
                cbDraft.Checked = false;
                cbDraft.Enabled = false;
            }
        }

        private List<Tuple<int, string, int, string>> MachineList = new List<Tuple<int, string, int, string>>();

        private void loadMachine(ComboBox cmb)
        {
            string macSelected = "";
            MachineList.Clear();

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
            DataTable dt_Fac = (DataTable)cmbMacLocation.DataSource;


            if (string.IsNullOrEmpty(fac) || fac.ToUpper().Equals(text.Cmb_All.ToUpper()))
            {
                //load All active machine
                facDAL dalFac = new facDAL();

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

                    foreach (DataRow facRow in dt_Fac.Rows)
                    {
                        string facName = facRow[dalFac.FacName].ToString();
                        string facID = facRow[dalFac.FacID].ToString();

                        if (macLocation == fac && fac == facName)
                        {
                            dt.Rows.Add(row[dalMac.MacID], row[dalMac.MacName]);

                            Tuple<int, string, int, string> machine = new Tuple<int, string, int, string>(Convert.ToInt32(facID), facName, Convert.ToInt32(row[dalMac.MacID]), row[dalMac.MacName].ToString());
                            MachineList.Add(machine);
                        }
                    }


                    //if (macLocation == fac)
                    //{
                    //    dt.Rows.Add(row[dalMac.MacID], row[dalMac.MacName]);

                    //}
                }
            }


            DataTable distinctTable = dt.DefaultView.ToTable(true, dalMac.MacID, dalMac.MacName);
            distinctTable.DefaultView.Sort = dalMac.MacName; // Assuming MacName is the column name
            distinctTable.DefaultView.ApplyDefaultSort = false;

            List<DataRow> sortedRows = new List<DataRow>(distinctTable.Select());
            sortedRows.Sort((row1, row2) => new AlphanumericComparer().Compare(row1[dalMac.MacName].ToString(), row2[dalMac.MacName].ToString()));

            // Create a new DataTable with the sorted rows
            DataTable sortedTable = distinctTable.Clone();

            foreach (DataRow row in sortedRows)
            {
                sortedTable.ImportRow(row);
            }

            // If you want to replace the original table with the sorted one
            distinctTable = sortedTable;

            cmb.DataSource = sortedTable;
            cmb.DisplayMember = dalMac.MacName;

            if (macSelected == "" || fac.ToUpper().Equals(text.Cmb_All.ToUpper()))
            {
                cmb.SelectedIndex = -1;
            }
            else
            {
                cmb.Text = macSelected;

            }
        }

        private DataTable specialDataSort(DataTable dt)
        {
            // Clone the original DataTable schema
            DataTable sortedDt = dt.Clone();

            // Accept changes to the original DataTable
            dt.AcceptChanges();

            // Convert DataTable to Enumerable
            var rows = dt.AsEnumerable();

            // Group by MacLocationName and then sort within each group by MacName
            var grouped = rows.GroupBy(row => row.Field<string>(dalMac.MacLocationName))
                              .OrderBy(g => int.TryParse(g.Key, out int loc) ? loc : int.MaxValue);

            foreach (var group in grouped)
            {
                var sortedGroup = group.OrderBy(row => int.TryParse(row.Field<string>(dalMac.MacName), out int macName) ? macName : int.MaxValue)
                                        .ThenBy(row => row.Field<string>(dalMac.MacName), new NaturalStringComparer());

                foreach (var row in sortedGroup)
                {
                    DataRow newRow = sortedDt.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    sortedDt.Rows.Add(newRow);
                }
            }

            return sortedDt;
        }

        public class NaturalStringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                // Regex that separates the string into groups of digits and non-digits
                var xParts = Regex.Split(x, @"(\d+)");
                var yParts = Regex.Split(y, @"(\d+)");

                for (int i = 0; i < Math.Min(xParts.Length, yParts.Length); i++)
                {
                    // If both parts are numeric, compare as integers
                    if (int.TryParse(xParts[i], out int xInt) && int.TryParse(yParts[i], out int yInt))
                    {
                        int numCompare = xInt.CompareTo(yInt);
                        if (numCompare != 0)
                        {
                            return numCompare;
                        }
                    }
                    else // Else compare as strings
                    {
                        int stringCompare = string.Compare(xParts[i], yParts[i], StringComparison.Ordinal);
                        if (stringCompare != 0)
                        {
                            return stringCompare;
                        }
                    }
                }

                // If we get here, the common parts of both strings are equal,
                // so whichever string is longer should be considered "larger."
                return x.Length.CompareTo(y.Length);
            }
        }


        private DataTable Old_specialDataSort(DataTable dt)
        {
            DataTable sortedDt = dt.Clone();
            string Fac = null;
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    string newFac = row[dalMac.MacLocationName].ToString();
                    if (Fac != newFac)
                    {
                        foreach (DataRow row2 in dt.Rows)
                        {
                            if (row2.RowState != DataRowState.Deleted)
                            {
                                string FacSearch = row2[dalMac.MacLocationName].ToString();

                                if (Fac == FacSearch)
                                {
                                    DataRow newRow2 = sortedDt.NewRow();
                                    newRow2 = row2;
                                    sortedDt.ImportRow(newRow2);
                                    row2.Delete();
                                }
                            }


                        }
                    }

                    DataRow newRow = sortedDt.NewRow();
                    newRow = row;
                    sortedDt.ImportRow(newRow);
                    row.Delete();
                    Fac = newFac;
                }

            }

            return sortedDt;
        }
        private void MatSummaryReset()
        {
            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            MatSummaryColoring(dgvMacSchedule, "", true, true);

        }

        DataTable DT_ITEM;

       
        public static int CompareMachines(string mac1, string mac2)
        {
            // Handle numeric machines
            if (int.TryParse(mac1, out var num1) && int.TryParse(mac2, out var num2))
            {
                if (num1 > num2) return -1;
                if (num1 < num2) return 1;
                return 0;
            }

            // Handle alphanumeric machines
            if (!int.TryParse(mac1, out _) && !int.TryParse(mac2, out _))
            {
                var char1 = mac1[0];
                var char2 = mac2[0];
                var numPart1 = int.Parse(mac1.Substring(1));
                var numPart2 = int.Parse(mac2.Substring(1));

                if (char1 == char2)
                {
                    if (numPart1 > numPart2) return -1;
                    if (numPart1 < numPart2) return 1;
                    return 1;
                }
                else
                {
                    if (char1 > char2) return -1;
                    if (char1 < char2) return 1;
                }
            }

            // Comparing numeric and alphanumeric machines
            return int.TryParse(mac1, out _) ? -1 : 1;
        }



        private DataTable AddIdleMachine(DataTable dt_Schedule)
        {
            string searching = txtSearch.Text;
            string cmbMacNameSelected = cmbMac.Text;

            if (cbRunning.Checked && cbPending.Checked && string.IsNullOrEmpty(searching))
            {
                //Idle machine adding
                foreach (var machine in MachineList)
                {
                    // Check if the machine id from MachineList is not found in dt_Schedule.
                    bool IdleMachineFound = !dt_Schedule.AsEnumerable().Any(row => !row.IsNull(text.Header_MacID) && row.Field<int>(text.Header_MacID) == machine.Item3);

                    if (IdleMachineFound && (machine.Item4 == cmbMacNameSelected || string.IsNullOrEmpty(cmbMacNameSelected)))
                    {
                        // Add a new row for this machine.
                        DataRow idleRow = dt_Schedule.NewRow();
                        DataRow row_Schedule;

                        // Populate the necessary fields for this row.
                        idleRow[text.Header_Status] = text.planning_status_idle;
                        idleRow[text.Header_FacID] = machine.Item1;
                        idleRow[text.Header_Fac] = machine.Item2;
                        idleRow[text.Header_MacID] = machine.Item3;
                        idleRow[text.Header_Mac] = machine.Item4;

                        // Find the rows with the same fac id.
                        var rows = dt_Schedule.AsEnumerable()
                            .Where(row => !row.IsNull(text.Header_FacID) && row.Field<int>(text.Header_FacID) == machine.Item1)
                            .ToList();

                        int rowToInsert = 0;

                        // If no such rows exist, simply add the row at the end.
                        if (!rows.Any())
                        {
                            rowToInsert = - 1;
                        }
                        else
                        {
                            string previousMacName = "";
                            int previousMacNameCompare = 0;

                            foreach (DataRow row in dt_Schedule.Rows)
                            {
                                string facID = row[text.Header_FacID].ToString();
                                string macName = row[text.Header_Mac].ToString();

                                if (facID == machine.Item1.ToString())
                                {
                                    int macNameCompare = CompareMachines(macName.ToUpper().Replace(" ", ""), machine.Item4.ToUpper().Replace(" ", ""));

                                    if(macNameCompare == -1)
                                    {
                                        rowToInsert = dt_Schedule.Rows.IndexOf(row);
                                        break;
                                    }
                                    else if (previousMacNameCompare == 1 && macNameCompare == -1)
                                    {
                                        break;
                                    }

                                    rowToInsert = dt_Schedule.Rows.IndexOf(row);

                                    if (macNameCompare == 1)
                                    {
                                        rowToInsert++;

                                    }
                                    previousMacNameCompare = macNameCompare;
                                    previousMacName = macName.ToUpper().Replace(" ", "");

                                    //int stringCompare = string.Compare(macName.ToUpper().Replace(" ", ""), machine.Item4, StringComparison.Ordinal);

                                    //if (stringCompare < 0)
                                    //{
                                    //    if (maxStringCompare == 0)
                                    //    {
                                    //        maxStringCompare = stringCompare;
                                    //    }

                                    //    if (maxStringCompare <= stringCompare)
                                    //    {
                                    //        rowToInsert = dt_Schedule.Rows.IndexOf(row);
                                    //        rowToInsert++;
                                    //        maxStringCompare = stringCompare;
                                    //    }
                                    //}
                                    //else if (stringCompare > 1)
                                    //{
                                    //    rowToInsert = dt_Schedule.Rows.IndexOf(row);
                                    //    rowToInsert++;
                                    //}
                                    //else if(stringCompare == 1)
                                    //{
                                    //    rowToInsert = dt_Schedule.Rows.IndexOf(row);
                                      
                                    //    break;
                                    //}
                                }
                            }
                        }

                        //Insert Idle Machine Row
                        if(rowToInsert == -1)
                        {
                            dt_Schedule.Rows.Add(idleRow);
                            rowToInsert = dt_Schedule.Rows.Count - 1;
                        }
                        else
                        {
                            dt_Schedule.Rows.InsertAt(idleRow, rowToInsert);

                        }

                        //dgvMacSchedule.Rows[prevRowIndex].Cells[text.Header_Status].Style.BackColor = Color.LightGray;

                        //check index -1 , if not empty, add divider
                        if (rowToInsert - 1 >= 0)
                        {
                            string facID = dt_Schedule.Rows[rowToInsert - 1][text.Header_FacID].ToString();
                            string macID = dt_Schedule.Rows[rowToInsert - 1][text.Header_MacID].ToString();

                            if (!string.IsNullOrEmpty(macID))
                            {
                                if (machine.Item1.ToString() == facID && machine.Item3.ToString() != macID)
                                {
                                    //add machine divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    row_Schedule[text.Header_ItemName] = "machineDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert);
                                    rowToInsert++;
                                    //dgvMacSchedule.Rows[prevRowIndex].Height = 6;
                                    //dgvMacSchedule.Rows[prevRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);

                                }
                                else if (machine.Item1.ToString() != facID)
                                {
                                    //add factory divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    row_Schedule[text.Header_ItemName] = "factoryDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert);
                                    rowToInsert++;

                                    //dgvMacSchedule.Rows[prevRowIndex].Height = 120;
                                    //dgvMacSchedule.Rows[prevRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                                }
                            }

                        }

                        //check index + 1, if not empty, add divider
                        if (dt_Schedule.Rows.Count > (rowToInsert + 1))
                        {
                            string facID = dt_Schedule.Rows[rowToInsert + 1][text.Header_FacID].ToString();
                            string macID = dt_Schedule.Rows[rowToInsert + 1][text.Header_MacID].ToString();

                            if (!string.IsNullOrEmpty(macID))
                            {
                                if (machine.Item1.ToString() == facID && machine.Item3.ToString() != macID)
                                {
                                    //add machine divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    row_Schedule[text.Header_ItemName] = "machineDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert + 1);
                                    //dgvMacSchedule.Rows[prevRowIndex + 1].Height = 6;
                                    //dgvMacSchedule.Rows[prevRowIndex + 1].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);

                                }
                                else if (machine.Item1.ToString() != facID)
                                {
                                    //add factory divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    row_Schedule[text.Header_ItemName] = "factoryDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert + 1);
                                    //dgvMacSchedule.Rows[prevRowIndex + 1].Height = 120;
                                    //dgvMacSchedule.Rows[prevRowIndex + 1].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                                }
                            }

                        }
                    }
                }
            }

            return dt_Schedule;
        }

        private void AddIdleMachineAndFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0 && cbRunning.Checked && cbPending.Checked)
            {
                DataTable dt_Schedule = (DataTable)dgv.DataSource;
                //Idle machine adding
                foreach (var machine in MachineList)
                {
                    // Check if the machine id from MachineList is not found in dt_Schedule.
                    bool IdleMachineFound = !dt_Schedule.AsEnumerable().Any(row => !row.IsNull(text.Header_MacID) && row.Field<int>(text.Header_MacID) == machine.Item3);

                    if (IdleMachineFound)
                    {
                        // Add a new row for this machine.
                        DataRow idleRow = dt_Schedule.NewRow();
                        DataRow row_Schedule;

                        // Populate the necessary fields for this row.
                        idleRow[text.Header_Status] = text.planning_status_idle;
                        idleRow[text.Header_FacID] = machine.Item1;
                        idleRow[text.Header_Fac] = machine.Item2;
                        idleRow[text.Header_MacID] = machine.Item3;
                        idleRow[text.Header_Mac] = machine.Item4;


                        // Find the rows with the same fac id.
                        var rows = dt_Schedule.AsEnumerable()
                            .Where(row => !row.IsNull(text.Header_FacID) && row.Field<int>(text.Header_FacID) == machine.Item1)
                            .ToList();

                        int rowToInsert = 0;

                        // If no such rows exist, simply add the row at the end.
                        if (!rows.Any())
                        {
                            rowToInsert = -1;
                        }
                        else
                        {

                            string previousMacName = "";
                            int previousMacNameCompare = 0;

                            foreach (DataRow row in dt_Schedule.Rows)
                            {
                                string facID = row[text.Header_FacID].ToString();
                                string macName = row[text.Header_Mac].ToString();

                                if (facID == machine.Item1.ToString())
                                {
                                    int macNameCompare = CompareMachines(macName.ToUpper().Replace(" ", ""), machine.Item4.ToUpper().Replace(" ", ""));

                                    if (macNameCompare == -1)
                                    {
                                        rowToInsert = dt_Schedule.Rows.IndexOf(row);
                                        break;
                                    }
                                    else if (previousMacNameCompare == 1 && macNameCompare == -1)
                                    {
                                        break;
                                    }

                                    rowToInsert = dt_Schedule.Rows.IndexOf(row);

                                    if (macNameCompare == 1)
                                    {
                                        rowToInsert++;

                                    }
                                    previousMacNameCompare = macNameCompare;
                                    previousMacName = macName.ToUpper().Replace(" ", "");
                                }
                            }

                           
                        }

                        //insert Idle Machine Row
                        if (rowToInsert == -1)
                        {
                            dt_Schedule.Rows.Add(idleRow);
                            rowToInsert = dt_Schedule.Rows.Count - 1;
                        }
                        else
                        {
                            dt_Schedule.Rows.InsertAt(idleRow, rowToInsert);

                        }
                        dgvMacSchedule.Rows[rowToInsert].Cells[text.Header_Status].Style.BackColor = Color.LightGray;

                        //check index -1 , if not empty, add divider
                        if (rowToInsert - 1 >= 0)
                        {
                            string facID = dt_Schedule.Rows[rowToInsert - 1][text.Header_FacID].ToString();
                            string macID = dt_Schedule.Rows[rowToInsert - 1][text.Header_MacID].ToString();

                            if (!string.IsNullOrEmpty(macID))
                            {
                                if (machine.Item1.ToString() == facID && machine.Item3.ToString() != macID)
                                {
                                    //add machine divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    //row_Schedule[text.Header_ItemName] = "machineDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert);
                                    dgvMacSchedule.Rows[rowToInsert].Height = 6;
                                    dgvMacSchedule.Rows[rowToInsert].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                                    rowToInsert++;
                                }
                                else if (machine.Item1.ToString() != facID)
                                {
                                    //add factory divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    //row_Schedule[text.Header_ItemName] = "factoryDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert);
                                    dgvMacSchedule.Rows[rowToInsert].Height = 120;
                                    dgvMacSchedule.Rows[rowToInsert].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                                    rowToInsert++;

                                }
                            }

                        }

                        //check index + 1, if not empty, add divider
                        if (dt_Schedule.Rows.Count > (rowToInsert + 1))
                        {
                            string facID = dt_Schedule.Rows[rowToInsert + 1][text.Header_FacID].ToString();
                            string macID = dt_Schedule.Rows[rowToInsert + 1][text.Header_MacID].ToString();

                            if (!string.IsNullOrEmpty(macID))
                            {
                                if (machine.Item1.ToString() == facID && machine.Item3.ToString() != macID)
                                {
                                    //add machine divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    //row_Schedule[text.Header_ItemName] = "machineDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert + 1);
                                    dgvMacSchedule.Rows[rowToInsert + 1].Height = 6;
                                    dgvMacSchedule.Rows[rowToInsert + 1].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);

                                }
                                else if (machine.Item1.ToString() != facID)
                                {
                                    //add factory divider
                                    row_Schedule = dt_Schedule.NewRow();
                                    //row_Schedule[text.Header_ItemName] = "factoryDivider";
                                    dt_Schedule.Rows.InsertAt(row_Schedule, rowToInsert + 1);
                                    dgvMacSchedule.Rows[rowToInsert + 1].Height = 120;
                                    dgvMacSchedule.Rows[rowToInsert + 1].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                                }
                            }
                            else
                            {
                                if (dt_Schedule.Rows.Count > (rowToInsert + 2))
                                {
                                     facID = dt_Schedule.Rows[rowToInsert + 2][text.Header_FacID].ToString();
                                     macID = dt_Schedule.Rows[rowToInsert + 2][text.Header_MacID].ToString();

                                    if (!string.IsNullOrEmpty(macID))
                                    {
                                        if (machine.Item1.ToString() == facID && machine.Item3.ToString() != macID)
                                        {
                                            dgvMacSchedule.Rows[rowToInsert + 1].Height = 6;
                                            dgvMacSchedule.Rows[rowToInsert + 1].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);

                                        }
                                        else if (machine.Item1.ToString() != facID)
                                        {
                                            dgvMacSchedule.Rows[rowToInsert + 1].Height = 120;
                                            dgvMacSchedule.Rows[rowToInsert + 1].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                                        }
                                    }


                                }
                            }

                        }
                    }
                }
            }
        }
        private void New_LoadMacSchedule()
        {
            lblSmallFontRow.Visible = true;
            if (DATE_COLLISION_FOUND || DATA_TO_UPDATE)
            {
                string message = "You have unsaved changes!\n\nAre you sure you want to process to load Machine Schedule ?";

                if (MessageBox.Show(message, "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes
                                                        )
                {
                    return;
                }
            }

            DATE_COLLISION_FOUND = false;
            DATA_TO_UPDATE = false;

            adjustAdjustCollisionDateButtonLayout();

            frmLoading.ShowLoadingScreen();

            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            if (DT_ITEM == null || DT_ITEM.Rows.Count <= 0)
            {
                DT_ITEM = dalItem.Select();
            }

            #region Search Filtering

            DataTable dt_JobPlan;
            DateTime startEarliest = DateTime.MinValue, endLatest = DateTime.MinValue;

            string Keywords = txtSearch.Text;

            bool GetCompletedPlanOnly = !cbDraft.Checked && !cbPending.Checked && !cbRunning.Checked && !cbWarning.Checked && !cbCancelled.Checked && cbCompleted.Checked;
            bool ShowAllActiveJob = cbPending.Checked && cbRunning.Checked;



            if (ChangePlanToAction)
            {
                txtSearch.Text = ITEM_CODE;

                Keywords = ITEM_CODE;

                cbItem.Checked = true;
                cbSearchByJobNo.Checked = false;
                cbPending.Checked = true;
                cbCancelled.Checked = true;
                cbCompleted.Checked = true;
                cbWarning.Checked = false;
                cbRunning.Checked = true;
            }

            if (!string.IsNullOrEmpty(Keywords))
            {
                if (cbItem.Checked)
                {
                    //search item
                    dt_JobPlan = dalPlanning.itemSearch(Keywords);
                }
                else
                {
                    //search id
                    dt_JobPlan = dalPlanning.idSearch(Keywords);
                }
            }
            else
            {
                dt_JobPlan = dalPlanning.Select();

            }

            dt_JobPlan = specialDataSort(dt_JobPlan);

            #endregion

            DataTable dt_Schedule = NewScheduleTable();

            DataRow row_Schedule;

            bool match = true;

            string previousLocationID = null;
            string previousMachineID = null;

            bool OneFactorySeaching = false;

            if (!cmbMacLocation.Text.Equals("All") && !string.IsNullOrEmpty(cmbMacLocation.Text) && string.IsNullOrEmpty(cmbMac.Text))
            {
                OneFactorySeaching = true;
            }

            string machineIDFilter = "";
            string machineNameFilter = cmbMac.Text;
            string factoryNameFilter = cmbMacLocation.Text;
            string factoryIDFilter = "";

            if (cmbMacLocation.SelectedIndex > -1)
            {
                DataRowView drv_Fac = (DataRowView)cmbMacLocation.SelectedItem;

                factoryIDFilter = drv_Fac[dalFac.FacID].ToString();
            }

            if (cmbMac.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cmbMac.SelectedItem;
                machineIDFilter = drv[dalMac.MacID].ToString();
            }
            //^379ms
            foreach (DataRow row in dt_JobPlan.Rows)
            {
                match = true;

                #region Status Filtering

                string status = row[dalPlanning.planStatus].ToString();

                if(status == text.planning_status_draft &&  userPermission < MainDashboard.ACTION_LVL_FOUR)
                {
                    match = false;
                    continue;
                }

                if (!(cbSearchByJobNo.Checked && !string.IsNullOrEmpty(Keywords)))
                {
                    if (string.IsNullOrEmpty(status))
                    {
                        match = false;
                        continue;
                    }

                    if (!cbPending.Checked && status.Equals(text.planning_status_pending))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbDraft.Checked && status.Equals(text.planning_status_draft))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbRunning.Checked && status.Equals(text.planning_status_running))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbWarning.Checked && status.Equals(text.planning_status_warning))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbCancelled.Checked && status.Equals(text.planning_status_cancelled))
                    {
                        match = false;
                        continue;

                    }

                    if (!cbCompleted.Checked && status.Equals(text.planning_status_completed))
                    {
                        match = false;
                        continue;

                    }
                }


                #endregion

                #region Period Filtering

                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                DateTime from = dtpFrom.Value;
                DateTime to = dtpTo.Value;

                if(ShowAllActiveJob && (status == text.planning_status_running || status == text.planning_status_pending))
                {

                }
                else
                {
                    if (!(cbSearchByJobNo.Checked && !string.IsNullOrEmpty(Keywords)))
                    {
                        if (!GetCompletedPlanOnly && !((start.Date >= from.Date && start.Date <= to.Date) || (end.Date >= from.Date && end.Date <= to.Date)))
                        {
                            match = false;
                            continue;

                        }
                        else if (GetCompletedPlanOnly && !(end.Date >= from.Date && end.Date <= to.Date))
                        {
                            match = false;
                            continue;

                        }
                    }
                }
               

                #endregion

                #region Location Filtering

                string factoryName = row[dalMac.MacLocationName].ToString();
                string factoryID = row[dalMac.MacLocationID].ToString();

                string machineID = row[dalMac.MacID].ToString();
                string machineName = row[dalMac.MacName].ToString();

                if (!factoryNameFilter.Equals("All") && !string.IsNullOrEmpty(factoryNameFilter))
                {
                    if (!factoryName.Equals(factoryNameFilter))
                    {
                        match = false;
                        continue;

                    }

                    if (!string.IsNullOrEmpty(machineIDFilter))
                    {
                        if (!machineID.Equals(machineIDFilter))
                        {
                            match = false;
                            continue;

                        }
                    }
                }

                #endregion

                if (match)
                {
                    #region Data Checking 

                    string factoryDivider = "";
                    string machineDivider = "";

                    if (startEarliest == DateTime.MinValue)
                    {
                        startEarliest = start;
                    }
                    else if (start < startEarliest)
                    {
                        startEarliest = start;
                    }

                    if (endLatest == DateTime.MinValue)
                    {
                        endLatest = end;
                    }
                    else if (endLatest < end)
                    {
                        endLatest = end;
                    }

                    if (previousLocationID == null)
                    {
                        previousLocationID = factoryID;
                    }
                    else if (!previousLocationID.Equals(factoryID))
                    {
                        factoryDivider = "factoryDivider";
                        previousLocationID = factoryID;
                    }

                    if (previousMachineID == null)
                    {
                        previousMachineID = machineID;
                    }
                    else if (!previousMachineID.Equals(machineID))
                    {
                        //if (OneFactorySeaching)
                        //{
                        //    dt_Schedule.Rows.Add(dt_Schedule.NewRow());
                        //}

                        machineDivider = "machineDivider";
                        previousMachineID = machineID;
                    }

                    if (!string.IsNullOrEmpty(machineDivider) || !string.IsNullOrEmpty(factoryDivider))
                    {
                        row_Schedule = dt_Schedule.NewRow();
                        row_Schedule[text.Header_ItemName] = factoryDivider + machineDivider;
                        dt_Schedule.Rows.Add(row_Schedule);
                    }


                    #endregion

                    #region Load to table 

                    row_Schedule = dt_Schedule.NewRow();

                    row_Schedule[text.Header_MouldCode] = row[dalPlanning.planMouldCode];
                    row_Schedule[text.Header_JobNo] = row[dalPlanning.jobNo];
                    row_Schedule[text.Header_DateStart] = row[dalPlanning.productionStartDate];
                    row_Schedule[text.Header_EstDateEnd] = row[dalPlanning.productionEndDate];
                    row_Schedule[text.Header_Ori_DateStart] = row[dalPlanning.productionStartDate];
                    row_Schedule[text.Header_Ori_EstDateEnd] = row[dalPlanning.productionEndDate];
                    row_Schedule[text.Header_Fac] = factoryName;
                    row_Schedule[text.Header_FacID] = int.TryParse(factoryID, out int x) ? x : 0;
                    row_Schedule[text.Header_Mac] = machineName;
                    row_Schedule[text.Header_Ori_Machine_Name] = machineName;
                    row_Schedule[text.Header_MacID] = int.TryParse(machineID, out x) ? x : 0;

                    string itemName = row[dalItem.ItemName].ToString();
                    string itemCode = row[dalItem.ItemCode].ToString();
                    string itemCodePresent = row[dalItem.ItemCodePresent].ToString();

                    if (string.IsNullOrEmpty(itemCodePresent))
                    {
                        itemCodePresent = itemCode;
                    }

                    string itemNameAndCodeString = tool.getItemNameAndCodeString( itemCodePresent,  itemName);
                   
                    row_Schedule[text.Header_ItemName] = itemName;
                    row_Schedule[text.Header_ItemCode] = itemCode;
                    row_Schedule[text.Header_ItemCode_Present] = itemCodePresent;

                    row_Schedule[text.Header_ItemNameAndCode] = itemNameAndCodeString;
                    int cycleTime = int.TryParse(row[dalPlanning.planCT].ToString(), out x) ? x : 0;
                    if (cycleTime == 0)
                    {
                        cycleTime = int.TryParse(row[dalItem.ItemProCTTo].ToString(), out x) ? x : 0;
                    }
                    row_Schedule[text.Header_ProCT] = cycleTime;
                    row_Schedule[text.Header_Job_Purpose] = row[dalPlanning.productionPurpose];

                    row_Schedule[text.Header_TargetQty] = row[dalPlanning.targetQty];
                    row_Schedule[text.Header_MaxOutput] = row[dalPlanning.ableQty];

                    if (row[dalPlanning.planProduced].ToString() != "0")
                        row_Schedule[text.Header_ProducedQty] = row[dalPlanning.planProduced];

                    string rawMat_1 = row[dalPlanning.materialCode].ToString();
                    string rawMat_2 = row[dalPlanning.materialCode2].ToString();

                    double rawMat_1_ratio = double.TryParse(row[dalPlanning.rawMatRatio_1].ToString(), out rawMat_1_ratio) ? rawMat_1_ratio : 0;
                    double rawMat_2_ratio = double.TryParse(row[dalPlanning.rawMatRatio_2].ToString(), out rawMat_2_ratio) ? rawMat_2_ratio : 0;

                    string rawMix = "";

                    rawMat_1_ratio = rawMat_1_ratio < 1 ? rawMat_1_ratio * 100 : rawMat_1_ratio;
                    rawMat_2_ratio = rawMat_2_ratio < 1 ? rawMat_2_ratio * 100 : rawMat_2_ratio;

                    if(rawMat_1_ratio == 0)
                    {
                        if (!string.IsNullOrEmpty(rawMat_1) && !string.IsNullOrEmpty(rawMat_2))
                        {
                            rawMat_1_ratio = 50;
                        }
                        else
                        {
                            rawMat_1_ratio = 100;

                        }
                    }

                    if (rawMat_2_ratio == 0)
                    {
                        if (!string.IsNullOrEmpty(rawMat_1) && !string.IsNullOrEmpty(rawMat_2))
                        {
                            rawMat_2_ratio = 50;
                        }
                    }

                    if (!string.IsNullOrEmpty(rawMat_1) && !string.IsNullOrEmpty(rawMat_2))
                    {
                        rawMix = rawMat_1 + "(" + rawMat_1_ratio + "%)" + " + " + rawMat_2 + "(" + rawMat_2_ratio + "%)";
                    }
                    else
                    {
                        rawMix = rawMat_1;
                    }

                    row_Schedule[text.Header_RawMat_1] = rawMat_1;
                    row_Schedule[text.Header_RawMat_2] = rawMat_2;
                    row_Schedule[text.Header_RawMat_String] = rawMix;
                    row_Schedule[text.Header_RawMat_1_Ratio] = rawMat_1_ratio;
                    row_Schedule[text.Header_RawMat_2_Ratio] = rawMat_2_ratio;

                    int rawBag_1 = int.TryParse(row[dalPlanning.materialBagQty_1].ToString(), out rawBag_1) ? rawBag_1 : 0;
                    int rawBag_2 = int.TryParse(row[dalPlanning.materialBagQty_2].ToString(), out rawBag_2) ? rawBag_2 : 0;

                    double rawKG_1 = double.TryParse(row[dalPlanning.rawMaterialQty_1].ToString(), out rawKG_1) ? rawKG_1 : 0;
                    double rawKG_2 = double.TryParse(row[dalPlanning.rawMaterialQty_2].ToString(), out rawKG_2) ? rawKG_2 : 0;

                    if (rawKG_1 == 0 && rawBag_1 > 0)
                    {
                        rawKG_1 = rawBag_1 * 25;
                    }

                    if (rawKG_2 == 0 && rawBag_2 > 0)
                    {
                        rawKG_2 = rawBag_2 * 25;

                    }

                    string rawMatQty = rawKG_1 + " KG (" + rawBag_1 + " bags)";

                    if (rawBag_1 == 1)
                    {
                        rawMatQty = rawKG_1 + " KG (" + rawBag_1 + " bag)";
                    }

                    if (rawKG_2 > 0)
                    {
                        double totalRawKG = rawKG_1 + rawKG_2;

                        rawMatQty = totalRawKG + " KG (" + rawKG_1 + " kg + " + rawKG_2 + " kg)";
                    }

                    row_Schedule[text.Header_RawMat_Bag_1] = rawBag_1;
                    row_Schedule[text.Header_RawMat_Bag_2] = rawBag_2;
                    row_Schedule[text.Header_RawMat_KG_1] = rawKG_1;
                    row_Schedule[text.Header_RawMat_KG_2] = rawKG_2;

                    row_Schedule[text.Header_RawMat_Qty] = rawMatQty;

                    row_Schedule[text.Header_Recycle_Qty] = row[dalPlanning.materialRecycleUse];

                    string colorMatCode = row[dalPlanning.colorMaterialCode].ToString();
                    string colorMatName = tool.getItemNameFromDataTable(DT_ITEM, colorMatCode);

                    colorMatName = string.IsNullOrEmpty(colorMatName) ? colorMatCode : colorMatName;

                    //row_Schedule[headerColor] = row[dalItem.ItemColor];
                    row_Schedule[text.Header_Color] = row[dalItem.ItemColor];
                    row_Schedule[text.Header_ColorMatCode] = colorMatCode;
                    row_Schedule[text.Header_ColorMat] = colorMatName;
                    row_Schedule[text.Header_ColorMat_KG] = row[dalPlanning.colorMaterialQty];
                    row_Schedule[text.Header_ColorRate] = row[dalPlanning.colorMaterialUsage];

                    row_Schedule[text.Header_Remark] = row[dalPlanning.planNote];
                    row_Schedule[text.Header_Status] = row[dalPlanning.planStatus];
                    row_Schedule[text.Header_Ori_Status] = row[dalPlanning.planStatus];

                    row_Schedule[text.Header_ProductionDay] = row[dalPlanning.productionDay];
                    row_Schedule[text.Header_ProductionHourPerDay] = row[dalPlanning.productionHourPerDay];
                    row_Schedule[text.Header_ProductionHour] = row[dalPlanning.productionHour];
                    row_Schedule[text.Header_Cavity] = row[dalPlanning.planCavity];
                    row_Schedule[text.Header_ProPwShot] = row[dalPlanning.planPW];
                    row_Schedule[text.Header_ProRwShot] = row[dalPlanning.planRW];
                    row_Schedule[text.Header_FamilyWithJobNo] = row[dalPlanning.familyWith];

                    dt_Schedule.Rows.Add(row_Schedule);

                    #endregion
                }

            }

            //^59ms
            dt_Schedule = AddIdleMachine(dt_Schedule);

            dgvMacSchedule.DataSource = null;

            if (dt_Schedule.Rows.Count > 0)
            {
                if (!GetCompletedPlanOnly)
                {
                    if (startEarliest != DateTime.MinValue)
                    {
                        dtpFrom.Value = startEarliest;
                    }

                    if (endLatest != DateTime.MinValue)
                    {
                        dtpTo.Value = endLatest;
                    }
                }
                //^462ms
                dgvMacSchedule.DataSource = dt_Schedule;
                dgvUIEdit(dgvMacSchedule);//1066ms
                MacScheduleListCellFormatting(dgvMacSchedule);//8546ms -> 2277ms
                dgvMacSchedule.ClearSelection();
            }

            frmLoading.CloseForm();

        }

        private void loadMaterialSummary(string Material)
        {
            float TotalMatPlannedToUse = 0;
            float TotalMatUsed = 0;
            float TotalMatToUse = 0;
            float stock = 0;

            bool rawType = false;
            bool colorType = false;

            int planCounter = 0;
            DataTable dt;


            var matSummary = tool.loadMaterialPlanningSummary(Material);
            planCounter = matSummary.Item1;
            TotalMatPlannedToUse = matSummary.Item2;
            TotalMatUsed = matSummary.Item3;
            TotalMatToUse = matSummary.Item4;
            stock = matSummary.Item5;
            rawType = matSummary.Item6;
            colorType = matSummary.Item7;

            lblTotalPlannedToUse.Text = "Total Planned ( " + planCounter + " ) To Use : " + TotalMatPlannedToUse.ToString("0.###") + " kg  ( " + (TotalMatPlannedToUse / 25).ToString("0.###") + " bag(s)  )";
            lblTotalUsed.Text = "Total Mat. Used : " + TotalMatUsed.ToString("0.###") + " kg  ( " + (TotalMatUsed / 25).ToString("0.###") + " bag(s)  )";
            lblTotalToUse.Text = "Total Mat. To Use : " + TotalMatToUse.ToString("0.###") + " kg  ( " + (TotalMatToUse / 25).ToString("0.###") + " bag(s)  )";

            //get stock qty

            lblMatStock.Text = "Stock : " + stock.ToString("0.###") + " kg  ( " + (stock / 25).ToString("0.###") + " bag(s)  )";

            if (stock < TotalMatToUse)
            {
                lblMatStock.ForeColor = Color.Red;
            }
            else
            {
                lblMatStock.ForeColor = Color.Green;

            }
            MatSummaryColoring(dgvMacSchedule, Material, rawType, colorType);
        }

        private void MatSummaryColoring(DataGridView dgv, string materialCode, bool rawMode, bool colorMode)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;

            if (rawMode && colorMode)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    if (string.IsNullOrEmpty(row[text.Header_ItemCode].ToString()) && row[text.Header_Status].ToString() != text.planning_status_idle)
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.FromArgb(1, 33, 71);
                        dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.FromArgb(1, 33, 71);
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.White;
                        dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.White;
                    }
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    string matCode_DGV = "";
                    string headerName = "";

                    if (rawMode)
                    {
                        headerName = text.Header_RawMat_1;

                    }
                    else if (colorMode)
                    {
                        headerName = text.Header_ColorMatCode;
                    }

                    matCode_DGV = row[headerName].ToString();

                    if (rawMode)
                    {
                        headerName = text.Header_RawMat_String;
                    }
                    else if (colorMode)
                    {
                        headerName = text.Header_ColorMat;
                    }

                    if (matCode_DGV == materialCode)
                    {
                        dgv.Rows[rowIndex].Cells[headerName].Style.BackColor = Color.OrangeRed;

                        if (rawMode)
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.White;
                        }
                        else if (colorMode)
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.White;

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(row[text.Header_ItemCode].ToString()) && row[text.Header_Status].ToString() != text.planning_status_idle)
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.FromArgb(1, 33, 71);
                            dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.FromArgb(1, 33, 71);
                        }
                        else
                        {
                            dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.White;
                            dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.White;
                        }
                    }
                }
            }


            dgv.ResumeLayout();
        }


        private bool checkIfFamilyMould(int jobNo, int jobNotoCompare)
        {
            string familyID_1 = "";
            string familyID_2 = "";
            bool result = false;

            DataTable dt_Plan = dalPlanning.Select();

            foreach (DataRow row in dt_Plan.Rows)
            {
                string jobNo_DB = row[dalPlanning.jobNo].ToString();

                if (jobNo_DB == jobNo.ToString())
                {
                    familyID_1 = row[dalPlanning.familyWith].ToString();
                }

                if (jobNo_DB == jobNotoCompare.ToString())
                {
                    familyID_2 = row[dalPlanning.familyWith].ToString();
                }
            }

            if (familyID_1 != "" && familyID_1 != "-1" && familyID_1 == familyID_2)
            {
                result = true;
            }

            return result;
        }


        private void frmMachineSchedule_Load(object sender, EventArgs e)
        {
            dgvMacSchedule.ClearSelection();

            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            loaded = true;
            //loadMachine();
            cmbMacLocation.SelectedIndex = 0;

            if (ItemProductionHistoryChecking)
            {
                cbCompleted.Checked = true;
                cbPending.Checked = true;

                dtpFrom.Value = new DateTime(2019, 01, 01);
                dtpTo.Value = DateTime.Today;
            }

            New_LoadMacSchedule();

            if (fromDailyRecord)
            {
                dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            else
            {
                dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

            }



        }

        #endregion

        #region Button Click to Plan Page

        private void AddNewJob()
        {
            frmJobAdding frm = new frmJobAdding();

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.ShowDialog();

            //get Job Number
            string NewJobNo = frmJobAdding.NEW_JOB_NO;
   
           
                

            if (!string.IsNullOrEmpty(NewJobNo))
            {
                    New_LoadMacSchedule();
                    dgvMacScheduleRowFirstDisplay(NewJobNo);
            }

        }
        private void btnNewJob_Click(object sender, EventArgs e)
        {
            AddNewJob();
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";
            string title = "Machine Schedule";
            DateTime currentDate = DateTime.Now;

            fileName = title + "_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (fromDailyRecord && MessageBox.Show("Are you sure you want to add this item to the list?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = false;
                foreach (DataGridViewRow row in dgvMacSchedule.SelectedRows)
                {
                    string jobNo = row.Cells[text.Header_JobNo].Value.ToString();
                    int jobNo_INT = -1;

                    if (!string.IsNullOrEmpty(jobNo) && int.TryParse(jobNo, out jobNo_INT))
                    {
                        uPlanning.plan_id = jobNo_INT;
                        uPlanning.recording = true;

                        success = dalPlanning.RecordingUpdate(uPlanning);
                    }
                }

                if (success)
                    MessageBox.Show("Item(s) added!");
                Close();
            }

            else if (buttionAction == text.DailyAction_ChangePlan && MessageBox.Show("Confirm to change plan ID?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                foreach (DataGridViewRow row in dgvMacSchedule.SelectedRows)
                {
                    string jobNo = row.Cells[text.Header_JobNo].Value.ToString();
                    string itemCode = row.Cells[text.Header_ItemCode].Value.ToString();

                    int jobNo_INT = -1;

                    if (!string.IsNullOrEmpty(jobNo) && int.TryParse(jobNo, out jobNo_INT))
                    {
                        if (jobNo_INT != -1 && itemCode == ITEM_CODE && jobNo_INT != PLAN_ID)
                        {
                            ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
                            ProductionRecordBLL uProRecord = new ProductionRecordBLL();

                            uProRecord.new_plan_id = jobNo_INT;
                            uProRecord.old_plan_id = PLAN_ID;
                            uProRecord.updated_date = DateTime.Now;
                            uProRecord.updated_by = MainDashboard.USER_ID;

                            if (dalProRecord.ChangePlanID(uProRecord))
                            {
                                //change transfer record plan id remark
                                ChangeTransferRecordProductionjobNoRemark(PLAN_ID.ToString(), jobNo_INT.ToString());

                                MessageBox.Show("Plan ID Changed!");

                                Close();
                            }

                        }
                        else if (jobNo_INT == PLAN_ID)
                        {
                            MessageBox.Show("Cannot select plan with same ID !");
                        }
                        else if (itemCode != ITEM_CODE)
                        {
                            MessageBox.Show("Item not match!");
                        }
                    }
                }

            }
            else
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                frmMacScheduleExcelDownload frm = new frmMacScheduleExcelDownload();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                Cursor = Cursors.Arrow; // change cursor to normal type

                if (frmMacScheduleExcelDownload.settingApplied)
                {
                    bool requestingMacSchedule = frmMacScheduleExcelDownload.RequestingMachineSchedule;

                    if (requestingMacSchedule)
                    {
                        try
                        {
                            Cursor = Cursors.WaitCursor; // change cursor to hourglass type


                            SaveFileDialog sfd = new SaveFileDialog();

                            string path = @"D:\StockAssistant\Document\MachineSchedule";

                            Directory.CreateDirectory(path);
                            sfd.InitialDirectory = path;
                            sfd.Filter = "Excel Documents (*.xls)|*.xls";
                            sfd.FileName = setFileName();

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                                // Copy DataGridView results to clipboard
                                copyAlltoClipboard();
                                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                object misValue = Missing.Value;
                                Excel.Application xlexcel = new Excel.Application();
                                xlexcel.PrintCommunication = false;
                                xlexcel.ScreenUpdating = false;
                                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                                xlexcel.Calculation = XlCalculation.xlCalculationManual;
                                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                                DateTime dateNow = DateTime.Now;
                                xlWorkSheet.Name = "Machine Schedule ";


                                #region Save data to Sheet

                                string title = "Machine Schedule " + dateNow.ToString("dd/MM/yyyy");

                                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 " + title;

                                string date = dateNow.ToString("dd/MM/yyyy hh:mm:ss");
                                //Header and Footer setup
                                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dateNow.ToString("dd/MM/yyyy");
                                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                                xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " At " + date;

                                //Page setup
                                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;


                                xlWorkSheet.PageSetup.Zoom = false;
                                xlWorkSheet.PageSetup.CenterHorizontally = true;

                                xlWorkSheet.PageSetup.FitToPagesWide = 1;
                                xlWorkSheet.PageSetup.FitToPagesTall = false;

                                double pointToCMRate = 0.035;
                                xlWorkSheet.PageSetup.TopMargin = 1.5 / pointToCMRate;
                                xlWorkSheet.PageSetup.BottomMargin = 1.0 / pointToCMRate;
                                xlWorkSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                                xlWorkSheet.PageSetup.FooterMargin = 1.0 / pointToCMRate;
                                xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                                xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                                xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                                xlexcel.PrintCommunication = true;
                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;
                                // Paste clipboard results to worksheet range
                                xlWorkSheet.Select();
                                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                                CR.Select();
                                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                                //content edit
                                Range tRange = xlWorkSheet.UsedRange;
                                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                                tRange.Borders.Weight = XlBorderWeight.xlMedium;
                                tRange.Font.Size = 10;
                                tRange.Font.Name = "Calibri";
                                tRange.EntireColumn.AutoFit();
                                tRange.EntireRow.AutoFit();

                                tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                                #endregion

                                DataTable dt_test = (DataTable)dgvMacSchedule.DataSource;

                                //int row = dgvSchedule.RowCount - 1;
                                //int col = dgvSchedule.ColumnCount - 1;

                                //MessageBox.Show("row: " + row + " col: " + col);
                                if (true)//cmbSubType.Text.Equals("PMMA")
                                {
                                    for (int i = 0; i <= dgvMacSchedule.RowCount - 1; i++)
                                    {
                                        string Mac = "";

                                        for (int j = 0; j <= dgvMacSchedule.ColumnCount - 1; j++)
                                        {
                                            string colName = dgvMacSchedule.Columns[j].Name;
                                           
                                            if(colName == text.Header_Mac)
                                            {
                                                Mac = dgvMacSchedule.Rows[i].Cells[j].Value.ToString();

                                                if(string.IsNullOrEmpty(Mac))
                                                {
                                                    break;
                                                }
                                            }
                                           
                                        }

                                        if (!string.IsNullOrEmpty(Mac))
                                        {
                                            Range range = (Range)xlWorkSheet.Cells[i + 2, 1];

                                            range.EntireRow.RowHeight = 40;
                                            range.EntireRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                            range.EntireRow.VerticalAlignment = XlVAlign.xlVAlignCenter;

                                            //range.Font.Size = 16;
                                            //range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                            ////range.Font.Color = ColorTranslator.ToOle(dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.ForeColor);

                                            //if (j == 3 || j == 6 || j == 7 || j == 9 || j == 12 || j == 15 || j == 11 || j == 14)
                                            //{
                                            //    range.Cells.Font.Bold = true;
                                            //    range.Font.Size = 16;
                                            //    range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                            //}

                                            //if (j == 4 || j == 5)
                                            //{
                                            //    range.ColumnWidth = 10;
                                            //}

                                            //if (j == 6 || j == 7 || j == 11 || j == 14 || j == 16)
                                            //{
                                            //    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                            //    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                            //}
                                            //else
                                            //{
                                            //    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                            //    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                            //}

                                            //if (dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Gainsboro)
                                            //{
                                            //    range.Interior.Color = Color.White;
                                            //    range.Rows.RowHeight = 40;
                                            //}
                                            //else if (dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.FromArgb(1, 33, 71))
                                            //{
                                            //    range.Rows.RowHeight = 4;
                                            //    range.Interior.Color = Color.Black;
                                            //}
                                            //else
                                            //{
                                            //    range.Interior.Color = ColorTranslator.ToOle(dgvMacSchedule.Rows[i].Cells[j].InheritedStyle.BackColor);
                                            //    range.Rows.RowHeight = 40;
                                            //}
                                        }
                                    }
                                }


                                for(int i = 1; i <= 17; i++)
                                {
                                    if(i == 8 || i == 12 || i == 17)
                                    {
                                        Range range2 = (Range)xlWorkSheet.Cells[1, i];

                                        range2.EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                        range2.EntireColumn.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                    }
                                  
                                }
                               

                                //Range header2 = (Range)xlWorkSheet.Cells[1, 1];
                                //header2.Interior.Color = Color.Gold;

                                //Save the excel file under the captured location from the SaveFileDialog
                                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                                    misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                xlexcel.DisplayAlerts = true;

                                xlWorkBook.Close(true, misValue, misValue);
                                xlexcel.Quit();

                                releaseObject(xlWorkSheet);
                                releaseObject(xlWorkBook);
                                releaseObject(xlexcel);

                                // Clear Clipboard and DataGridView selection
                                Clipboard.Clear();
                                dgvMacSchedule.ClearSelection();

                                // Open the newly saved excel file
                                if (File.Exists(sfd.FileName))
                                    System.Diagnostics.Process.Start(sfd.FileName);
                            }

                            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

                            Cursor = Cursors.Arrow; // change cursor to normal type
                        }
                        catch (Exception ex)
                        {
                            tool.saveToTextAndMessageToUser(ex);
                            Cursor = Cursors.Arrow; // change cursor to normal type
                        }


                    }
                    else//export Stocktake List
                    {
                        requestingStocktake_Part = frmMacScheduleExcelDownload.RequestingStocktake_Part;
                        requestingStocktake_RawMat = frmMacScheduleExcelDownload.RequestingStocktake_RawMat;
                        requestingStocktake_ColorMat = frmMacScheduleExcelDownload.RequestingStocktake_ColorMat;


                        DataTable dt_Main = (DataTable)dgvMacSchedule.DataSource;
                        GetStocktakeData(dt_Main);


                        //export to excel
                        NewExcel();

                    }

                }

            }

            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;


        }

        private void GetStocktakeData(DataTable dt_Main)
        {
            DT_STOCKTAKE_PART = NewStocktakeTable();
            DT_STOCKTAKE_RAWMAT = NewStocktakeTable();
            DT_STOCKTAKE_COLORMAT = NewStocktakeTable();

            if (dt_Main != null)
            {
                foreach (DataRow row in dt_Main.Rows)
                {
                    string ItemName = row[text.Header_ItemName].ToString();
                    string ItemCode = row[text.Header_ItemCode].ToString();
                    string RawMat = row[text.Header_RawMat_1].ToString();
                    string ColorMat = row[text.Header_ColorMatCode].ToString();

                    string Fac = row[text.Header_Fac].ToString();

                    //add Part to Table
                    if (requestingStocktake_Part && !string.IsNullOrEmpty(ItemName) && !string.IsNullOrEmpty(ItemCode))
                    {
                        bool itemDuplicated = false;

                        foreach (DataRow partRow in DT_STOCKTAKE_PART.Rows)
                        {
                            if ((ItemName + " (" + ItemCode + ")").Equals(partRow[text.Header_ItemDescription].ToString()) && Fac.Equals(partRow[text.Header_Fac].ToString()))
                            {
                                itemDuplicated = true;
                                break;
                            }
                        }

                        if (!itemDuplicated)
                        {
                            DataRow newPartRow = DT_STOCKTAKE_PART.NewRow();

                            newPartRow[text.Header_Fac] = Fac;
                            newPartRow[text.Header_ItemDescription] = ItemName + "\n" + " (" + ItemCode + ")";

                            DT_STOCKTAKE_PART.Rows.Add(newPartRow);
                        }



                    }

                    //TPE TSL H50 + H80 = TPE TSL H80 & TPE TSL H50
                    if (requestingStocktake_RawMat && !string.IsNullOrEmpty(RawMat))
                    {
                        bool itemDuplicated = false;

                        if (RawMat.Equals("TPE TSL H50 + H80"))
                        {
                            foreach (DataRow RawMatRaw in DT_STOCKTAKE_RAWMAT.Rows)
                            {
                                if ("TPE TSL H80".Equals(RawMatRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(RawMatRaw[text.Header_Fac].ToString()))
                                {
                                    itemDuplicated = true;
                                    break;
                                }
                            }

                            if (!itemDuplicated)
                            {
                                DataRow newRawMatRow = DT_STOCKTAKE_RAWMAT.NewRow();

                                newRawMatRow[text.Header_Fac] = Fac;
                                newRawMatRow[text.Header_ItemDescription] = "TPE TSL H80";

                                DT_STOCKTAKE_RAWMAT.Rows.Add(newRawMatRow);
                                itemDuplicated = false;
                            }


                            foreach (DataRow RawMatRaw in DT_STOCKTAKE_RAWMAT.Rows)
                            {
                                if ("TPE TSL H50".Equals(RawMatRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(RawMatRaw[text.Header_Fac].ToString()))
                                {
                                    itemDuplicated = true;
                                    break;
                                }
                            }

                            if (!itemDuplicated)
                            {
                                DataRow newRawMatRow = DT_STOCKTAKE_RAWMAT.NewRow();

                                newRawMatRow[text.Header_Fac] = Fac;
                                newRawMatRow[text.Header_ItemDescription] = "TPE TSL H50";

                                DT_STOCKTAKE_RAWMAT.Rows.Add(newRawMatRow);
                                itemDuplicated = false;
                            }

                        }
                        else
                        {
                            foreach (DataRow RawMatRaw in DT_STOCKTAKE_RAWMAT.Rows)
                            {
                                if (RawMat.Equals(RawMatRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(RawMatRaw[text.Header_Fac].ToString()))
                                {
                                    itemDuplicated = true;
                                    break;
                                }
                            }

                            if (!itemDuplicated)
                            {
                                DataRow newRawMatRow = DT_STOCKTAKE_RAWMAT.NewRow();

                                newRawMatRow[text.Header_Fac] = Fac;
                                newRawMatRow[text.Header_ItemDescription] = RawMat;

                                DT_STOCKTAKE_RAWMAT.Rows.Add(newRawMatRow);
                            }


                        }
                    }

                    //color filter Out: NATURAL, COMPOUND
                    if (requestingStocktake_ColorMat && !string.IsNullOrEmpty(ColorMat) && ColorMat != "NATURAL" && ColorMat != "COMPOUND")
                    {
                        bool itemDuplicated = false;

                        foreach (DataRow colorRaw in DT_STOCKTAKE_COLORMAT.Rows)
                        {
                            if (ColorMat.Equals(colorRaw[text.Header_ItemDescription].ToString()) && Fac.Equals(colorRaw[text.Header_Fac].ToString()))
                            {
                                itemDuplicated = true;
                                break;
                            }
                        }

                        if (!itemDuplicated)
                        {
                            DataRow newColorMatRow = DT_STOCKTAKE_COLORMAT.NewRow();

                            newColorMatRow[text.Header_Fac] = Fac;
                            newColorMatRow[text.Header_ItemDescription] = ColorMat;

                            DT_STOCKTAKE_COLORMAT.Rows.Add(newColorMatRow);
                        }

                    }
                }

                DT_STOCKTAKE_PART.DefaultView.Sort = text.Header_Fac + " ASC," + text.Header_ItemDescription + " ASC";
                DT_STOCKTAKE_PART = DT_STOCKTAKE_PART.DefaultView.ToTable();

                DT_STOCKTAKE_RAWMAT.DefaultView.Sort = text.Header_Fac + " ASC," + text.Header_ItemDescription + " ASC";
                DT_STOCKTAKE_RAWMAT = DT_STOCKTAKE_RAWMAT.DefaultView.ToTable();

                DT_STOCKTAKE_COLORMAT.DefaultView.Sort = text.Header_Fac + " ASC," + text.Header_ItemDescription + " ASC";
                DT_STOCKTAKE_COLORMAT = DT_STOCKTAKE_COLORMAT.DefaultView.ToTable();

                DT_STOCKTAKE_PART = tool.NEW_RearrangeIndex(DT_STOCKTAKE_PART, text.Header_Index);

                DT_STOCKTAKE_RAWMAT = tool.NEW_RearrangeIndex(DT_STOCKTAKE_RAWMAT, text.Header_Index);

                DT_STOCKTAKE_COLORMAT = tool.NEW_RearrangeIndex(DT_STOCKTAKE_COLORMAT, text.Header_Index);



            }
        }

        private void ExcelPageSetup(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = 1;
            xlWorkSheet.PageSetup.FitToPagesWide = 1;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            //xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

            xlWorkSheet.PageSetup.PrintArea = "a1:g25";


        }

        private void ExcelMergeandAlign(Worksheet xlWorkSheet, string RangeString, XlHAlign hl, XlVAlign va)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.Merge();

            range.HorizontalAlignment = hl;
            range.VerticalAlignment = va;
        }

        private void ExcelColumnWidth(Worksheet xlWorkSheet, string RangeString, double colWidth)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.ColumnWidth = colWidth;
        }

        private void ExcelRowHeight(Worksheet xlWorkSheet, string RangeString, double rowHeight)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.RowHeight = rowHeight;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, string value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;

            if (area == "a2:g2")
                DataInsertArea.Characters[34, 44].Font.Bold = 1;
        }

        private void NewInitialDOFormat(Worksheet xlWorkSheet)
        {
            #region Sheet Setting

            string Area_Sheet = "a1:g25";
            string Area_FormCode = "A1:C1";
            string Area_SheetTitle = "a2:g2";
            string Area_StockCountQty = "d3:e3";
            string Area_PageNo = "g1:g1";

            string Area_Data = "a4:g23";

            string Area_CountedInfo = "a25:c25";
            string Area_CountedBy = "a25:b25";
            string Area_CountedDate = "c25:c25";

            string Area_VerifiedInfo = "e25:g25";
            string Area_VerifiedBy = "e25:e25";
            string Area_VerifiedDate = "f25:g25";
            string Area_Signature = "a25:g25";

            string Row_FormCode = "a1";
            string Row_SheetTitle = "a2";
            string Row_Header = "a3";
            string Row_Data = "a4:g23";
            string Row_SpaceBetweenDataAndSignature = "a24";
            string Row_Signature = "a25";


            string Col_Index = "a1";
            string Col_Location = "b1";
            string Col_ItemDescription = "c1";
            string Col_StockCountQty_1 = "d1";
            string Col_StockCountQty_2 = "e1";
            string Col_SystemQty = "f1";
            string Col_Diffenrence = "g1";



            XlHAlign H_alignLeft = XlHAlign.xlHAlignLeft;
            XlHAlign H_alignRight = XlHAlign.xlHAlignRight;
            XlHAlign H_alignCenter = XlHAlign.xlHAlignCenter;

            XlVAlign V_alignTop = XlVAlign.xlVAlignTop;
            XlVAlign V_alignBottom = XlVAlign.xlVAlignBottom;
            XlVAlign V_alignCenter = XlVAlign.xlVAlignCenter;

            ExcelMergeandAlign(xlWorkSheet, "a3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "b3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "c3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "f3", H_alignCenter, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, "g3", H_alignCenter, V_alignCenter);


            ExcelMergeandAlign(xlWorkSheet, Area_FormCode, H_alignLeft, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, Area_PageNo, H_alignRight, V_alignCenter);
            ExcelMergeandAlign(xlWorkSheet, Area_SheetTitle, H_alignCenter, V_alignCenter);

            ExcelMergeandAlign(xlWorkSheet, Area_StockCountQty, H_alignCenter, V_alignCenter);

            ExcelMergeandAlign(xlWorkSheet, Area_CountedBy, H_alignLeft, V_alignTop);
            ExcelMergeandAlign(xlWorkSheet, Area_CountedDate, H_alignCenter, V_alignTop);

            ExcelMergeandAlign(xlWorkSheet, Area_VerifiedBy, H_alignLeft, V_alignTop);
            ExcelMergeandAlign(xlWorkSheet, Area_VerifiedDate, H_alignCenter, V_alignTop);

            ExcelRowHeight(xlWorkSheet, Row_FormCode, 19.6);
            ExcelRowHeight(xlWorkSheet, Row_SheetTitle, 31.6);
            ExcelRowHeight(xlWorkSheet, Row_Header, 30.4);
            ExcelRowHeight(xlWorkSheet, Row_Data, 31);
            ExcelRowHeight(xlWorkSheet, Row_SpaceBetweenDataAndSignature, 12.4);
            ExcelRowHeight(xlWorkSheet, Row_Signature, 73.6);

            ExcelColumnWidth(xlWorkSheet, Col_Index, 5.2);
            ExcelColumnWidth(xlWorkSheet, Col_Location, 9.63);
            ExcelColumnWidth(xlWorkSheet, Col_ItemDescription, 33.5);
            ExcelColumnWidth(xlWorkSheet, Col_StockCountQty_1, 30.38);
            ExcelColumnWidth(xlWorkSheet, Col_StockCountQty_2, 17.38);
            ExcelColumnWidth(xlWorkSheet, Col_SystemQty, 11.4);
            ExcelColumnWidth(xlWorkSheet, Col_Diffenrence, 10.2);

            Range SheetFormat = xlWorkSheet.get_Range(Area_Sheet).Cells;
            SheetFormat.Interior.Color = Color.White;
            SheetFormat.Font.Name = "Segoe UI";
            SheetFormat.Font.Size = 9;
            //SheetFormat.Font.Color = Color.DarkBlue;
            SheetFormat.Font.Color = Color.Black;
            xlWorkSheet.PageSetup.PrintArea = Area_Sheet;

            Color GrayLineColor = Color.DarkGray;




            SheetFormat = xlWorkSheet.get_Range(Area_CountedInfo).Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;


            SheetFormat = xlWorkSheet.get_Range(Area_VerifiedInfo).Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;


            SheetFormat = xlWorkSheet.get_Range(Area_SheetTitle).Cells;
            SheetFormat.Value = "PHYSICAL STOCK COUNT SHEET";
            SheetFormat.Font.Size = 12;

            SheetFormat = xlWorkSheet.get_Range(Area_FormCode).Cells;
            SheetFormat.Value = "FORM CODE : WHSC00101";
            SheetFormat.Font.Size = 8;
            SheetFormat.Font.Italic = true;

            SheetFormat = xlWorkSheet.get_Range("a3").Cells;
            SheetFormat.Value = "#";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("b3").Cells;
            SheetFormat.Value = "Location";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("c3").Cells;
            SheetFormat.Value = "Item Description";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("d3").Cells;
            SheetFormat.Value = "Stock Count Qty";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("f3").Cells;
            SheetFormat.Value = "System Qty";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range("g3").Cells;
            SheetFormat.Value = "Difference";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_CountedBy).Cells;
            SheetFormat.Value = "Counted By";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_CountedDate).Cells;
            SheetFormat.Value = "Date";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_VerifiedBy).Cells;
            SheetFormat.Value = "Verified By";
            SheetFormat.Font.Bold = true;

            SheetFormat = xlWorkSheet.get_Range(Area_VerifiedDate).Cells;
            SheetFormat.Value = "Date";
            SheetFormat.Font.Bold = true;


            #endregion

            #region Item List

            int TotalRows = 20;
            int itemRowOffset = 3;

            string indexColStart = "a";
            string indexColEnd = ":a";

            string LocationColStart = "b";
            string LocationColEnd = ":b";

            string ItemDescriptionColStart = "c";
            string ItemDescriptionColEnd = ":c";

            string StockCountQtyColStart = "d";
            string StockCountQtyColEnd = ":e";

            string SystemQtyColStart = "f";
            string SystemQtyColEnd = ":f";

            string DifferenceColStart = "g";
            string DifferenceColEnd = ":g";


            for (int i = 0; i <= TotalRows; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = LocationColStart + (itemRowOffset + i).ToString() + LocationColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = ItemDescriptionColStart + (itemRowOffset + i).ToString() + ItemDescriptionColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignLeft, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;


                area = StockCountQtyColStart + (itemRowOffset + i).ToString() + StockCountQtyColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = SystemQtyColStart + (itemRowOffset + i).ToString() + SystemQtyColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

                area = DifferenceColStart + (itemRowOffset + i).ToString() + DifferenceColEnd + (itemRowOffset + i).ToString();

                ExcelMergeandAlign(xlWorkSheet, area, H_alignCenter, V_alignCenter);

                SheetFormat = xlWorkSheet.get_Range(area).Cells;
                SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
                SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;

            }

            SheetFormat = xlWorkSheet.get_Range("a3:g3").Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;

            SheetFormat = xlWorkSheet.get_Range("a3:g23").Cells;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlMedium;

            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Color = GrayLineColor;
            SheetFormat.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlMedium;

            //CellStyle style = cell.Style;
            //style.ShrinkToFit = true;

            SheetFormat.ShrinkToFit = true;

            SheetFormat = xlWorkSheet.get_Range("c4:c23").Cells;
            SheetFormat.WrapText = true;

            #endregion
        }

        private void NewExcel()
        {
            #region export excel

            if (requestingStocktake_Part || requestingStocktake_RawMat || requestingStocktake_ColorMat)
            {
                #region Excel Saving Setting

                string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

                string path = @"D:\StockAssistant\Document\OUG Stock Count";

                Directory.CreateDirectory(path);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = path;

                DateTime dateFrom = dtpFrom.Value;
                DateTime dateTo = dtpTo.Value;

                string title = "StockCountSheet_ProductionEnd (" + dateFrom.Date.ToString("ddMM") + "~" + dateTo.Date.ToString("ddMM") + ")";
                DateTime currentDate = DateTime.Now;

                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = title + "_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

                #endregion

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    #region Excel Setting

                    frmLoading.ShowLoadingScreen();
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;

                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };

                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.StandardFont = "Segoe UI";
                    xlexcel.StandardFontSize = 9;

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    xlexcel.PrintCommunication = false;

                    int pageNo = 1;
                    int sheetNo = 0;
                    int rowNo = 0;

                    Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;
                    //xlWorkSheet.PageSetup.PrintArea

                    xlexcel.PrintCommunication = true;
                    ExcelPageSetup(xlWorkSheet);

                    xlexcel.PrintCommunication = true;

                    xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;


                    NewInitialDOFormat(xlWorkSheet);

                    #endregion

                    sheetNo = 0;
                    rowNo = 0;

                    int rowStart = 4;
                    int rowEnd = 23;
                    int rowMax = rowEnd - rowStart + 1;

                    string col_Index = "a";
                    string col_Location = "b";
                    string col_ItemDescription = "c";
                    string colStart_StockCountQty = "d";
                    string colEnd_StockCountQty = "e";
                    string col_SystemQty = "f";
                    string col_Difference = "g";

                    string Area_PageNo = "g1:g1";

                    string areaPageData = "t10:w10";

                    string Area_SheetTitle = "a2:g2";
                    int dataRowInsertedCount = 0;


                    string FormTitle = "STOCK COUNT SHEET_PRODUCTION END (" + dateFrom.Date.ToString("dd/MM") + "~" + dateTo.Date.ToString("dd/MM") + " )_";


                    //check if data can combine in one sheet
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //8467ms

                    if (requestingStocktake_Part)
                    {
                        sheetNo++;

                        pageNo = 1;
                        xlWorkSheet.Name = text.Cat_Part;
                        int index = 1;

                        InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());

                        InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_Part);


                        foreach (DataRow row in DT_STOCKTAKE_PART.Rows)
                        {
                            string fac = row[text.Header_Fac].ToString();
                            string itemDescription = row[text.Header_ItemDescription].ToString();

                            InsertToSheet(xlWorkSheet, col_Index + rowStart + ":" + col_Index + rowStart, index.ToString());
                            InsertToSheet(xlWorkSheet, col_Location + rowStart + ":" + col_Location + rowStart, fac);
                            InsertToSheet(xlWorkSheet, col_ItemDescription + rowStart + ":" + col_ItemDescription + rowStart, itemDescription);

                            rowStart++;
                            index++;
                            dataRowInsertedCount++;

                            if (dataRowInsertedCount == rowMax)
                            {
                                //reset and create new sheet
                                sheetNo++;

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                xlexcel.PrintCommunication = true;
                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;

                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                NewInitialDOFormat(xlWorkSheet);

                                pageNo++;
                                xlWorkSheet.Name = text.Cat_Part + " (" + pageNo + ")";
                                rowStart = 4;
                                dataRowInsertedCount = 0;

                                InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_Part);
                                InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());

                            }


                        }
                    }

                    if (requestingStocktake_RawMat)
                    {
                        sheetNo++;
                        rowStart = 4;
                        dataRowInsertedCount = 0;

                        if (sheetNo > 1)
                        {
                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                            xlexcel.PrintCommunication = true;
                            ExcelPageSetup(xlWorkSheet);

                            xlexcel.PrintCommunication = true;

                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            NewInitialDOFormat(xlWorkSheet);
                        }

                        pageNo = 1;
                        xlWorkSheet.Name = text.Cat_RawMat;
                        int index = 1;
                        InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                        InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_RawMat);


                        foreach (DataRow row in DT_STOCKTAKE_RAWMAT.Rows)
                        {
                            string fac = row[text.Header_Fac].ToString();
                            string itemDescription = row[text.Header_ItemDescription].ToString();

                            InsertToSheet(xlWorkSheet, col_Index + rowStart + ":" + col_Index + rowStart, index.ToString());
                            InsertToSheet(xlWorkSheet, col_Location + rowStart + ":" + col_Location + rowStart, fac);
                            InsertToSheet(xlWorkSheet, col_ItemDescription + rowStart + ":" + col_ItemDescription + rowStart, itemDescription);

                            rowStart++;
                            index++;
                            dataRowInsertedCount++;

                            if (dataRowInsertedCount == rowMax)
                            {
                                //reset and create new sheet
                                sheetNo++;

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                xlexcel.PrintCommunication = true;
                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;

                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                NewInitialDOFormat(xlWorkSheet);

                                pageNo++;
                                xlWorkSheet.Name = text.Cat_RawMat + " (" + pageNo + ")";
                                rowStart = 4;
                                dataRowInsertedCount = 0;
                                InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                                InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_RawMat);


                            }


                        }
                    }

                    if (requestingStocktake_ColorMat)
                    {
                        sheetNo++;
                        rowStart = 4;
                        dataRowInsertedCount = 0;

                        if (sheetNo > 1)
                        {
                            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                            xlexcel.PrintCommunication = true;
                            ExcelPageSetup(xlWorkSheet);

                            xlexcel.PrintCommunication = true;

                            xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                            NewInitialDOFormat(xlWorkSheet);
                        }

                        pageNo = 1;
                        xlWorkSheet.Name = text.Cat_ColorMat;
                        int index = 1;
                        InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                        InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_ColorMat);

                        foreach (DataRow row in DT_STOCKTAKE_COLORMAT.Rows)
                        {
                            string fac = row[text.Header_Fac].ToString();
                            string itemDescription = row[text.Header_ItemDescription].ToString();

                            InsertToSheet(xlWorkSheet, col_Index + rowStart + ":" + col_Index + rowStart, index.ToString());
                            InsertToSheet(xlWorkSheet, col_Location + rowStart + ":" + col_Location + rowStart, fac);
                            InsertToSheet(xlWorkSheet, col_ItemDescription + rowStart + ":" + col_ItemDescription + rowStart, itemDescription);

                            rowStart++;
                            index++;
                            dataRowInsertedCount++;

                            if (dataRowInsertedCount == rowMax)
                            {
                                //reset and create new sheet
                                sheetNo++;

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                xlexcel.PrintCommunication = true;
                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;

                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                NewInitialDOFormat(xlWorkSheet);

                                pageNo++;
                                xlWorkSheet.Name = text.Cat_ColorMat + " (" + pageNo + ")";
                                rowStart = 4;
                                dataRowInsertedCount = 0;
                                InsertToSheet(xlWorkSheet, Area_PageNo, "PG. " + pageNo.ToString());
                                InsertToSheet(xlWorkSheet, Area_SheetTitle, FormTitle + text.Cat_ColorMat);

                            }


                        }


                    }



                    if (sheetNo > 0)
                    {
                        xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                       misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    }

                    frmLoading.CloseForm();
                    Focus();

                    #region open saved file
                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                    {
                        Excel.Application excel = new Excel.Application();
                        excel.Visible = true;
                        excel.DisplayAlerts = false;
                        Workbook wb = excel.Workbooks.Open(sfd.FileName, ReadOnly: false, Notify: false);
                        excel.DisplayAlerts = true;
                    }


                    xlexcel.DisplayAlerts = true;

                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);
                    #endregion
                }

            }
            #endregion
        }

        private void ChangeTransferRecordProductionjobNoRemark(string jobNo, string newjobNo)
        {
            trfHistBLL uTrfHist = new trfHistBLL();
            uTrfHist.trf_hist_updated_by = MainDashboard.USER_ID;
            uTrfHist.trf_hist_updated_date = DateTime.Now;

            trfHistDAL dalTrf = new trfHistDAL();

            DataTable dt_Trf = dalTrf.SelectAll();
            dt_Trf.DefaultView.Sort = dalTrf.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();

            int trfTableCode = -1;

            foreach (DataRow row in dt_Trf.Rows)
            {
                DateTime trfDate = Convert.ToDateTime(row[dalTrf.TrfDate].ToString());

                string productionInfo = row[dalTrf.TrfNote].ToString();

                string _jobNoFound = "";
                string balanceClearType = "";
                bool startCopy = false;
                bool IDCopied = false;
                bool ShiftCopied = false;
                bool StopIDCopy = false;

                for (int i = 0; i < productionInfo.Length; i++)
                {
                    if (productionInfo[i].ToString() == "P")
                    {
                        startCopy = true;
                    }
                    else if (ShiftCopied && (productionInfo[i].ToString() == "O" || productionInfo[i].ToString() == "B"))
                    {
                        startCopy = true;
                    }
                    else if (ShiftCopied && productionInfo[i].ToString() == "]")
                    {
                        startCopy = false;
                        StopIDCopy = true;
                    }

                    if (startCopy)
                    {

                        if (char.IsDigit(productionInfo[i]) && !StopIDCopy)
                        {
                            IDCopied = true;
                            _jobNoFound += productionInfo[i];
                        }
                        else if (IDCopied && i + 1 < productionInfo.Length)
                        {
                            IDCopied = false;
                            ShiftCopied = true;
                            startCopy = false;
                        }
                        else if (ShiftCopied)
                        {
                            balanceClearType += productionInfo[i];
                        }

                    }
                }

                if (jobNo == _jobNoFound)
                {
                    trfTableCode = int.TryParse(row[dalTrf.TrfID].ToString(), out trfTableCode) ? trfTableCode : -1;

                    if (trfTableCode != -1)
                    {
                        uTrfHist.trf_hist_note = productionInfo.Replace(jobNo, newjobNo);
                        uTrfHist.trf_hist_id = trfTableCode;

                        if (!dalTrf.NoteUpdate(uTrfHist))
                        {
                            MessageBox.Show("Failed to update note!\nTable Code: " + trfTableCode);
                        }
                    }


                }

            }

        }

        private void copyAlltoClipboard()
        {
            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMacSchedule.MultiSelect = true;

            dgvMacSchedule.SelectAll();
            DataObject dataObj = dgvMacSchedule.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvMacSchedule.MultiSelect = false;

        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region Filter Data

        private void cbItem_CheckedChanged(object sender, EventArgs e)
        {
            if (cbItem.Checked)
            {
                cbSearchByJobNo.Checked = false;
            }
        }

        private void cbPlanningID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchByJobNo.Checked)
            {
                cbItem.Checked = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            DateTime DateFrom = dtpFrom.Value;

            bool GetCompletedPlanOnly = !cbPending.Checked && !cbRunning.Checked && !cbWarning.Checked && !cbCancelled.Checked && cbCompleted.Checked;
            bool isMonday = DateFrom.DayOfWeek == DayOfWeek.Monday;

            if (GetCompletedPlanOnly && isMonday)
            {
                //set date End on Saturday
                dtpTo.Value = DateFrom.AddDays(5);
            }


        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMacLocation.Text != null && loaded && ableLoadData)
            {
                ableLoadData = false;

                //loadMachine();
                loadMachine(cmbMac);
                
                ableLoadData = true;
            }
        }

        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMac.SelectedItem != null && ableLoadData)
            {
                ableLoadData = false;

                DataRowView drv = (DataRowView)cmbMac.SelectedItem;


                ableLoadData = true;

                cmbMacLocation.Text = tool.getFactoryNameFromMachineID(drv[dalMac.MacID].ToString());
            }
        }

        private void ResetStatusToDefault()
        {
            cbPending.Checked = true;
            cbRunning.Checked = true;
            cbWarning.Checked = true;
            cbCompleted.Checked = false;
            cbCancelled.Checked = false;
        }

        private void ResetData()
        {
            cmbMacLocation.SelectedIndex = 0;
            txtSearch.Clear();
            cbItem.Checked = true;
            ResetStatusToDefault();

            DateTime todayDate = DateTime.Today;
            dtpFrom.Value = todayDate.AddDays(-180);
            dtpTo.Value = todayDate.AddDays(180);
            ActiveControl = dgvMacSchedule;

        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            ResetData();

            dgvMacSchedule.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        public void StartForm()
        {
            System.Windows.Forms.Application.Run(new frmLoading());
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                New_LoadMacSchedule();
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchByJobNo.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region form close
        private void frmMachineSchedule_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NEWProductionFormOpen = false;
        }
        #endregion


        private void MacScheduleListCellFormatting(DataGridView dgv)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;

            lblTotalPlannedToUse.Text = "";
            lblTotalUsed.Text = "";
            lblTotalToUse.Text = "";
            lblMatStock.Text = "";

            dgv.Columns[text.Header_Status].DefaultCellStyle.ForeColor = Color.Black; 
            dgv.Columns[text.Header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);

            dgv.Columns[text.Header_RawMat_String].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns[text.Header_ColorMat].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns[text.Header_DateStart].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns[text.Header_EstDateEnd].DefaultCellStyle.BackColor = Color.White;

            dgv.Columns[text.Header_DateStart].DefaultCellStyle.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
            dgv.Columns[text.Header_EstDateEnd].DefaultCellStyle.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);

            dgv.Columns[text.Header_DateStart].DefaultCellStyle.ForeColor = Color.Black;
            dgv.Columns[text.Header_EstDateEnd].DefaultCellStyle.ForeColor = Color.Black;

            //49ms -> 156ms
            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                int familyJobNo = int.TryParse(row[text.Header_FamilyWithJobNo].ToString(), out familyJobNo) ? familyJobNo : -1;

                string PlanStatus = row[text.Header_Status].ToString();

                Color ColorSet = tool.GetColorSetFromPlanStatus(PlanStatus, dgv);

                dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = ColorSet;
                //dgv.Rows[rowIndex].Cells[text.Header_Status].Style.ForeColor = Color.Black;
                //dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);

                if (PlanStatus == text.planning_status_to_update)
                {
                    dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                }

                if (PlanStatus == "")
                {
                    string divider = row[text.Header_ItemName].ToString();

                    if (divider.Contains("factoryDivider"))
                    {
                        dgv.Rows[rowIndex].Height = 120;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Height = 6;
                    }

                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(1, 33, 71);
                    dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.FromArgb(1, 33, 71);
                    dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.FromArgb(1, 33, 71);

                    dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.FromArgb(1, 33, 71);
                    dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.FromArgb(1, 33, 71);

                    dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
                    dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
                    //if (!cmbMacLocation.Text.Equals("All") && !string.IsNullOrEmpty(cmbMacLocation.Text) && string.IsNullOrEmpty(cmbMac.Text))
                    //{
                    //    dgv.Rows[rowIndex].Height = 10;
                    //}

                }
                else
                {
                    //dgv.Rows[rowIndex].Cells[text.Header_RawMat_String].Style.BackColor = Color.White;
                    //dgv.Rows[rowIndex].Cells[text.Header_ColorMat].Style.BackColor = Color.White;

                    dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.BackColor = Color.White;
                    dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.BackColor = Color.White;

                    dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.ForeColor = Color.Black;
                    dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.ForeColor = Color.Black;

                    //dgv.Rows[rowIndex].Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
                    //dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);

                    // Color.Gainsboro;
                }

                bool activeJob = PlanStatus == text.planning_status_running;
                activeJob |= PlanStatus == text.planning_status_pending;
                activeJob |= PlanStatus == text.planning_status_draft;
                activeJob |= PlanStatus == text.planning_status_to_update;

                if (familyJobNo > 0 && activeJob)
                {
                    foreach(DataGridViewRow dgvRow in dgv.Rows)
                    {
                        if(familyJobNo.ToString() == dgvRow.Cells[text.Header_FamilyWithJobNo].Value.ToString())
                        {
                            dgvRow.Cells[text.Header_MouldCode].Style.BackColor = Color.Yellow;
                            dgvRow.Cells[text.Header_MouldCode].Value = "FAMILY";
                        }
                    }
                }

            }
            //5687ms -> 2268ms
            CheckMachineProductionDateCollision();
            //4ms
            dgv.ResumeLayout();
        }

        #region plan status change

        private bool planRunning(int rowIndex, string presentStatus)
        {
            bool statusChanged = false;

            uPlanning.plan_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_JobNo].Value;
            uPlanning.machine_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_MacID].Value;
            uPlanning.production_start_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value;
            uPlanning.production_end_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value;
            uPlanning.recording = true;

            if (tool.ifProductionDateAvailable(uPlanning.machine_id.ToString()))
            {
                frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, true);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmMachineScheduleAdjustFromMain.applied)
                {
                    //loadScheduleData();

                    statusChanged = true;
                    //inactive mat plan

                    uMatPlan.plan_id = uPlanning.plan_id;
                    uMatPlan.active = true;
                    uMatPlan.updated_date = DateTime.Now;
                    uMatPlan.updated_by = MainDashboard.USER_ID;

                    dalMatPlan.ActiveUpdate(uMatPlan);

                    if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                    {
                        //get habit data
                        float oldHourPerDay = 0;
                        float newHourPerDay = 0;

                        DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                        foreach (DataRow row in dt.Rows)
                        {
                            newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                        }

                        //get this plan hour per day data
                        DataTable dt_plan = dalPlanning.idSearch(uPlanning.plan_id.ToString());

                        foreach (DataRow row in dt_plan.Rows)
                        {
                            oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                            //compare
                            if (oldHourPerDay != newHourPerDay)
                            {
                                int oldProDay = 0, newProDay = 0;
                                float oldProHour = 0, newProHour = 0;

                                oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                                oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                                float totalHour = oldProDay * oldHourPerDay + oldProHour;

                                newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                                newProHour = totalHour - newProDay * newHourPerDay;

                                if (newProHour < 0)
                                {
                                    newProHour = 0;
                                }

                                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                                //change this plan data
                                //update
                                //change production day & hour data
                                uPlanning.plan_id = uPlanning.plan_id;
                                uPlanning.production_hour = newProHour.ToString();
                                uPlanning.production_day = newProDay.ToString();
                                uPlanning.production_hour_per_day = newHourPerDay.ToString();
                                uPlanning.production_start_date = start.Date;
                                uPlanning.production_end_date = end.Date;
                                uPlanning.plan_updated_date = DateTime.Now;
                                uPlanning.plan_updated_by = MainDashboard.USER_ID;
                                uPlanning.recording = true;

                                //update to db
                                dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                            }
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("Machine " + uPlanning.machine_id + " is running now, please stop it before start a new plan.");
            }



            Cursor = Cursors.Arrow; // change cursor to normal type
            return statusChanged;
        }

        private bool NEW_planRunning(int macID ,string macName, int rowIndex, string presentStatus)
        {
            bool JOB_UPDATED = false;

            if (tool.ifProductionDateAvailable(macID.ToString()))
            {
                int JOB_UPDATED_COUNT = 0;

                DateTime dateNow = DateTime.Now;

                DataGridView dgv = dgvMacSchedule;

                DataTable dt = (DataTable)dgv.DataSource;
                DataRow rowEditing = dt.NewRow();
                rowEditing.ItemArray = dt.Rows[rowIndex].ItemArray;

                if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                {
                    //get habit data
                    double newHourPerDay = (double) rowEditing[text.Header_ProductionHourPerDay];

                    dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                    foreach (DataRow row in dt.Rows)
                    {
                        newHourPerDay = double.TryParse(row[dalHabit.HabitData].ToString(), out double x)? x : newHourPerDay;
                        break;
                    }
                }

                frmChangeDate frm = new frmChangeDate(rowEditing);

                frm.StartPosition = FormStartPosition.CenterScreen;

                frm.ShowDialog();

                if(frmChangeDate.JOB_RUNNING_DATE_CONFIRMED)
                {
                    if (frmChangeDate.dateChanged)
                    {
                        uPlanning.production_start_date = frmChangeDate.start;
                        uPlanning.production_end_date = frmChangeDate.end;
                    }
                    else
                    {
                        uPlanning.production_start_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value;
                        uPlanning.production_end_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value;
                    }

                    string remark = rowEditing[text.Header_Remark].ToString();

                    DateTime oriStart = DateTime.TryParse(rowEditing[text.Header_Ori_DateStart].ToString(), out oriStart) ? oriStart : DateTime.MaxValue;
                    DateTime oriEnd = DateTime.TryParse(rowEditing[text.Header_Ori_EstDateEnd].ToString(), out oriEnd) ? oriEnd : DateTime.MaxValue;

                    int familyJobNo = int.TryParse(dgvMacSchedule.Rows[rowIndex].Cells[text.Header_FamilyWithJobNo].Value.ToString(), out familyJobNo) ? familyJobNo : -1;
                    int jobNo = int.TryParse(dgvMacSchedule.Rows[rowIndex].Cells[text.Header_JobNo].Value.ToString(), out jobNo) ? jobNo : -1;

                    uPlanning.plan_updated_date = dateNow;
                    uPlanning.plan_updated_by = MainDashboard.USER_ID;
                    uPlanning.recording = true;
                    uPlanning.plan_id = jobNo;
                    uPlanning.plan_status = text.planning_status_running;
                    uPlanning.family_with = familyJobNo;
                    uPlanning.plan_note = remark;
                    uPlanning.machine_id = macID;
                    uPlanning.machine_name = macName;

                    JOB_UPDATED = dalPlanningAction.JobDateRecordingAndMachineUpdate(uPlanning, macName, oriStart, oriEnd, familyJobNo);

                    if (JOB_UPDATED)
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Value = uPlanning.production_start_date;
                        dgv.Rows[rowIndex].Cells[text.Header_Ori_DateStart].Value = uPlanning.production_start_date;
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value = uPlanning.production_end_date;
                        dgv.Rows[rowIndex].Cells[text.Header_Ori_EstDateEnd].Value = uPlanning.production_end_date;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Value = text.planning_status_running;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = tool.GetColorSetFromPlanStatus(text.planning_status_running, dgv);

                        JOB_UPDATED_COUNT++;
                        tool.historyRecord(text.System, "Job Updated", dateNow, MainDashboard.USER_ID);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update Job data");
                        tool.historyRecord(text.System, "Failed to update job data", dateNow, MainDashboard.USER_ID);
                    }

                    //family mould check
                    if (familyJobNo > 0)
                    {
                        foreach (DataGridViewRow dgvRow in dgv.Rows)
                        {
                            if (familyJobNo.ToString() == dgvRow.Cells[text.Header_FamilyWithJobNo].Value.ToString() && jobNo.ToString() != dgvRow.Cells[text.Header_JobNo].Value.ToString())
                            {
                                remark = dgvRow.Cells[text.Header_Remark].Value.ToString();

                                oriStart = DateTime.TryParse(dgvRow.Cells[text.Header_Ori_DateStart].Value.ToString(), out oriStart) ? oriStart : DateTime.MaxValue;
                                oriEnd = DateTime.TryParse(dgvRow.Cells[text.Header_Ori_EstDateEnd].Value.ToString(), out oriEnd) ? oriEnd : DateTime.MaxValue;

                                int jobNo_FamilyMember = int.TryParse(dgvRow.Cells[text.Header_JobNo].Value.ToString(), out jobNo_FamilyMember) ? jobNo_FamilyMember : -1;

                                uPlanning.plan_id = jobNo_FamilyMember;
                                uPlanning.plan_note = remark;

                                JOB_UPDATED = dalPlanningAction.JobDateRecordingAndMachineUpdate(uPlanning, macName, oriStart, oriEnd, familyJobNo);

                                if (JOB_UPDATED)
                                {
                                    dgvRow.Cells[text.Header_DateStart].Value = uPlanning.production_start_date;
                                    dgvRow.Cells[text.Header_Ori_DateStart].Value = uPlanning.production_start_date;
                                    dgvRow.Cells[text.Header_EstDateEnd].Value = uPlanning.production_end_date;
                                    dgvRow.Cells[text.Header_Ori_EstDateEnd].Value = uPlanning.production_end_date;
                                    dgvRow.Cells[text.Header_Status].Value = text.planning_status_running;
                                    dgvRow.Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);
                                    dgvRow.Cells[text.Header_Status].Style.BackColor = tool.GetColorSetFromPlanStatus(text.planning_status_running, dgv);

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

                    if (JOB_UPDATED_COUNT > 0)
                    {
                        MessageBox.Show(JOB_UPDATED_COUNT + " Job(s) Updated.");

                        //date collision check
                        CheckMachineProductionDateCollision();
                    }
                }
               
            }
            
            else
            {
                MessageBox.Show("Machine " + macName + " is currently running.\n\nPlease stop it before starting a new job.");
            }

            return JOB_UPDATED;
        }
        private void planNoteUpdate(int jobNo)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            uPlanning.plan_id = jobNo;
            uPlanning.plan_note = CELL_EDITING_NEW_VALUE;
            uPlanning.plan_updated_date = DateTime.Now;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            dalPlanningAction.planningRemarkChange(uPlanning, CELL_EDITING_OLD_VALUE);

            CELL_EDITING_OLD_VALUE = "";
            CELL_EDITING_NEW_VALUE = "";

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private bool planPending(int jobNo, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = jobNo;
            uPlanning.plan_status = text.planning_status_pending;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;
            //uPlanning.recording = false;

            //bool success = dalPlanning.statusUpdate(uPlanning);
            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = jobNo;
                uMatPlan.active = true;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);

                if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                {
                    //get habit data
                    float oldHourPerDay = 0;
                    float newHourPerDay = 0;

                    DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                    foreach (DataRow row in dt.Rows)
                    {
                        newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                    }

                    //get this plan hour per day data
                    DataTable dt_plan = dalPlanning.idSearch(jobNo.ToString());

                    foreach (DataRow row in dt_plan.Rows)
                    {
                        oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                        //compare
                        if (oldHourPerDay != newHourPerDay)
                        {
                            int oldProDay = 0, newProDay = 0;
                            float oldProHour = 0, newProHour = 0;

                            oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                            oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                            float totalHour = oldProDay * oldHourPerDay + oldProHour;

                            newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                            newProHour = totalHour - newProDay * newHourPerDay;

                            if (newProHour < 0)
                            {
                                newProHour = 0;
                            }

                            DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                            DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                            //change this plan data
                            //update
                            //change production day & hour data
                            uPlanning.plan_id = jobNo;
                            uPlanning.production_hour = newProHour.ToString();
                            uPlanning.production_day = newProDay.ToString();
                            uPlanning.production_hour_per_day = newHourPerDay.ToString();
                            uPlanning.production_start_date = start.Date;
                            uPlanning.production_end_date = end.Date;
                            uPlanning.plan_updated_date = DateTime.Now;
                            uPlanning.plan_updated_by = MainDashboard.USER_ID;

                            //update to db
                            dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                        }
                    }
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }

        private bool planDraft(int jobNo, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = jobNo;
            uPlanning.plan_status = text.planning_status_draft;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;
            //uPlanning.recording = false;

            //bool success = dalPlanning.statusUpdate(uPlanning);
            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = jobNo;
                uMatPlan.active = true;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);

                if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                {
                    //get habit data
                    float oldHourPerDay = 0;
                    float newHourPerDay = 0;

                    DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                    foreach (DataRow row in dt.Rows)
                    {
                        newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                    }

                    //get this plan hour per day data
                    DataTable dt_plan = dalPlanning.idSearch(jobNo.ToString());

                    foreach (DataRow row in dt_plan.Rows)
                    {
                        oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                        //compare
                        if (oldHourPerDay != newHourPerDay)
                        {
                            int oldProDay = 0, newProDay = 0;
                            float oldProHour = 0, newProHour = 0;

                            oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                            oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                            float totalHour = oldProDay * oldHourPerDay + oldProHour;

                            newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                            newProHour = totalHour - newProDay * newHourPerDay;

                            if (newProHour < 0)
                            {
                                newProHour = 0;
                            }

                            DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                            DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                            //change this plan data
                            //update
                            //change production day & hour data
                            uPlanning.plan_id = jobNo;
                            uPlanning.production_hour = newProHour.ToString();
                            uPlanning.production_day = newProDay.ToString();
                            uPlanning.production_hour_per_day = newHourPerDay.ToString();
                            uPlanning.production_start_date = start.Date;
                            uPlanning.production_end_date = end.Date;
                            uPlanning.plan_updated_date = DateTime.Now;
                            uPlanning.plan_updated_by = MainDashboard.USER_ID;

                            //update to db
                            dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                        }
                    }
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }
        private bool planComplete(int jobNo, string presentStatus)
        {
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            uPlanning.plan_id = jobNo;
            uPlanning.plan_status = text.planning_status_completed;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = jobNo;
                uMatPlan.active = false;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);
            }
            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }

        private bool planCancel(int jobNo, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = jobNo;
            uPlanning.plan_status = text.planning_status_cancelled;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            uPlanning.recording = false;

            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = jobNo;
                uMatPlan.active = false;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);
            }


            Cursor = Cursors.Arrow; // change cursor to normal type
            return statusChanged;
        }

        private void CalculationMaterialSummary(string material)
        {
            loadMaterialSummary(material);
        }

        private void removeExtraSpace_MachineSchedule()
        {
            if (dgvMacSchedule?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                bool continueEmptySpace = false;

                foreach(DataGridViewRow row in dgvMacSchedule.Rows)
                {
                    string status = row.Cells[text.Header_Status].Value.ToString();

                    if (string.IsNullOrEmpty(status))
                    {
                        if (continueEmptySpace)
                        {
                            dgvMacSchedule.Rows.RemoveAt(row.Index);
                            continueEmptySpace = false;
                        }
                        else
                        {
                            continueEmptySpace = true;
                        }
                        
                    }
                    else
                    {
                        continueEmptySpace = false;
                    }
                }

                //dt.AcceptChanges();

                //foreach (DataRow row in dt.Rows)
                //{
                //    string status = row[text.Header_Status].ToString();

                //    if (string.IsNullOrEmpty(status))
                //    {
                //        if (continueEmptySpace)
                //        {
                //            row.Delete();
                //        }

                //        continueEmptySpace = true;
                //    }
                //    else
                //    {
                //        continueEmptySpace = false;
                //    }
                //}

                //dt.AcceptChanges();

            }
        }
        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            dgvMacSchedule.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMacSchedule;
            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
            int colIndex = dgv.CurrentCell.ColumnIndex;

            int jobNo = int.TryParse(dgv.Rows[rowIndex].Cells[text.Header_JobNo].Value.ToString(), out jobNo) ? jobNo : -1;

            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[text.Header_MacID].Value);
            string macName = dgv.Rows[rowIndex].Cells[text.Header_Mac].Value.ToString();

            string presentStatus = dgv.Rows[rowIndex].Cells[text.Header_Status].Value.ToString();

            string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();


            if (itemClicked.Equals(SCHEDULE_ACTION_JOB_EDIT))
            {
                if(DATE_COLLISION_FOUND || DATA_TO_UPDATE)
                {
                    string message = "You have unsaved changes!\n\nAre you sure you want to process to 'Job Edit' ?";

                    if(MessageBox.Show(message, "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes
                                                            )
                    {
                        return;
                    }
                }

                bool statusPass = presentStatus == text.planning_status_draft;
                statusPass |= presentStatus == text.planning_status_pending;
                statusPass |= presentStatus == text.planning_status_running;

                if (statusPass)
                {
                   

                    JobEditMode(rowIndex, colIndex);


                }
            }
            else if (itemClicked.Equals(SCHEDULE_ACTION_ADD_JOB))
            {
                if (DATE_COLLISION_FOUND || DATA_TO_UPDATE)
                {
                    string message = "You have unsaved changes!\n\nAre you sure you want to process to 'Job Edit' ?";

                    if (MessageBox.Show(message, "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes
                                                            )
                    {
                        return;
                    }
                }

                AddNewJob();


            }
            else if(itemClicked.Equals(SCHEDULE_ACTION_DATE_EDIT) && rowIndex > -1)
            {
                DataTable dt = (DataTable)dgv.DataSource;
                DataRow row = dt.NewRow();
                row.ItemArray = dt.Rows[rowIndex].ItemArray;

                frmChangeDate frm = new frmChangeDate(row);

                frm.StartPosition = FormStartPosition.CenterScreen;

                frm.ShowDialog();

                //get date
                if(frmChangeDate.dateChanged)
                {
                    MessageBox.Show("Date changed.");

                    DateTime start = frmChangeDate.start;
                    DateTime end = frmChangeDate.end;

                    int familyJobNo = int.TryParse(dgvMacSchedule.Rows[rowIndex].Cells[text.Header_FamilyWithJobNo].Value.ToString(), out familyJobNo)? familyJobNo : -1;

                    DATA_TO_UPDATE = true;

                    //family mould check
                    if(familyJobNo > 0)
                    {
                        foreach(DataGridViewRow dgvRow in dgv.Rows)
                        {
                            if(familyJobNo.ToString() == dgvRow.Cells[text.Header_FamilyWithJobNo].Value.ToString())
                            {
                                dgvRow.Cells[text.Header_DateStart].Value = start;
                                dgvRow.Cells[text.Header_EstDateEnd].Value = end;
                                dgvRow.Cells[text.Header_Status].Value = text.planning_status_to_update;

                                dgvRow.Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                                dgvRow.Cells[text.Header_Status].Style.BackColor = Color.White;

                            }
                        }
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_DateStart].Value = start;
                        dgv.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value = end;
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Value = text.planning_status_to_update;

                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                        dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = Color.White;
                    }
                    //date collision check
                    CheckMachineProductionDateCollision();
                }


            }
            else
            {
                bool rowRemoved = false;
                string newJobStatus = presentStatus;

                if (itemClicked.Equals(text.planning_status_pending))
                {
                    if (MessageBox.Show("Are you sure you want to switch this Job to PENDING status?", "Message",
                                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (planPending(jobNo, presentStatus))
                        {
                            //DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                            if (cbPending.Checked)
                            {
                                newJobStatus = text.planning_status_pending;
                               
                            }
                            else
                            {
                                dgv.Rows.RemoveAt(rowIndex);
                                rowRemoved = true;

                                //dt.Rows.RemoveAt(rowIndex);
                                //dt.AcceptChanges();
                            }
                        }
                    }

                }
                else if (itemClicked.Equals(text.planning_status_draft))
                {
                    if (MessageBox.Show("Are you sure you want to switch this Job to Draft status?", "Message",
                                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (planDraft(jobNo, presentStatus))
                        {
                            //DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                            if (cbDraft.Checked)
                            {
                                //dt.Rows[rowIndex][text.Header_Status] = text.planning_status_draft;
                                newJobStatus = text.planning_status_draft;
                            }
                            else
                            {
                                dgv.Rows.RemoveAt(rowIndex);
                                rowRemoved = true;
                            }
                        }
                    }

                }
                else if (itemClicked.Equals(text.planning_status_running))
                {
                    if (NEW_planRunning(macID, macName, rowIndex, presentStatus))
                    {
                        //DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                        if (cbRunning.Checked)
                        {
                            newJobStatus = text.planning_status_running;
                        }
                        else
                        {
                            dgv.Rows.RemoveAt(rowIndex);
                            rowRemoved = true;
                        }
                    }
                }
                else if (itemClicked.Equals(text.planning_status_completed))
                {
                    if (MessageBox.Show("Are you sure you want to switch this Job to COMPLETED status?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (planComplete(jobNo, presentStatus))
                        {
                           // DataTable dt = (DataTable)dgvMacSchedule.DataSource;
                            if (cbCompleted.Checked)
                            {
                                newJobStatus = text.planning_status_completed;
                            }
                            else
                            {
                                dgv.Rows.RemoveAt(rowIndex);
                                rowRemoved = true;
                            }

                            var w = new Form() { Size = new Size(0, 0) };
                            Task.Delay(TimeSpan.FromSeconds(2))
                                .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                            MessageBox.Show(w, "Job " + jobNo + " completed!", "SYSTEM");
                        }
                    }
                }
                else if (itemClicked.Equals(text.planning_status_cancelled))
                {
                    if (MessageBox.Show("Are you sure you want to CANCEL this job?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (planCancel(jobNo, presentStatus))
                        {
                            //DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                            if (cbCancelled.Checked)
                            {
                                newJobStatus = text.planning_status_cancelled;
                            }
                            else
                            {
                                dgv.Rows.RemoveAt(rowIndex);
                                rowRemoved = true;
                            }

                            var w = new Form() { Size = new Size(0, 0) };
                            Task.Delay(TimeSpan.FromSeconds(2))
                                .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                            MessageBox.Show(w, "Job " + jobNo + " cancelled!", "SYSTEM");
                        }
                    }

                }
                

                if(rowRemoved)
                {
                    removeExtraSpace_MachineSchedule();
                    AddIdleMachineAndFormatting(dgvMacSchedule);
                }
                else
                {
                    dgv.Rows[rowIndex].Cells[text.Header_Status].Value = newJobStatus;
                    dgv.Rows[rowIndex].Cells[text.Header_Status].Style.BackColor = tool.GetColorSetFromPlanStatus(newJobStatus, dgv);
                }

                //removeExtraSpace_MachineSchedule();

                //AddIdleMachine(dgvMacSchedule);

                //MacScheduleListCellFormatting(dgvMacSchedule);
            }

            if (itemClicked.Equals(text.planning_Material_Summary))
            {
                string headerName = dgv.Columns[colIndex].Name;

                if (headerName == text.Header_ColorMat)
                {
                    cellValue = dgv.Rows[rowIndex].Cells[text.Header_ColorMatCode].Value.ToString();

                }

                CalculationMaterialSummary(cellValue);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgvMacSchedule.ResumeLayout();
        }

        private void editSchedule(int rowIndex)
        {
            uPlanning.plan_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_JobNo].Value;
            uPlanning.machine_id = (int)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_MacID].Value;
            uPlanning.production_start_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value;
            uPlanning.production_end_date = (DateTime)dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value;

            frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frmMachineScheduleAdjustFromMain.applied)
            {
                New_LoadMacSchedule();
                dgvMacSchedule.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void JobEditMode(int rowIndex, int colIndex)
        {
            string EditingJobNo = dgvMacSchedule.Rows[rowIndex].Cells[text.Header_JobNo].Value.ToString();

            frmJobAdding frm = new frmJobAdding((DataTable)dgvMacSchedule.DataSource, rowIndex);

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.ShowDialog();

            if (frmJobAdding.JOB_ADDED || frmJobAdding.JOB_UPDATED || frmJobAdding.JOB_EDITING_UPDATED)
            {
                New_LoadMacSchedule();
                dgvMacScheduleRowFirstDisplay(EditingJobNo);
            }
               

        }

        public void dgvMacScheduleRowFirstDisplay(string reference)
        {
            DataGridView dgv = dgvMacSchedule;

            if (dgv.Rows.Count > 0 && dgv.Columns.Contains(text.Header_JobNo))
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ClearSelection();

                string mainFamilyJobNo = "-1";

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string data = dgv.Rows[i].Cells[text.Header_JobNo].Value.ToString();
                    string familyWith = dgv.Rows[i].Cells[text.Header_FamilyWithJobNo].Value.ToString();

                    if (data == reference)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = i;

                        dgv.Rows[i].Selected = true;

                        if(familyWith != "-1")
                        {
                            mainFamilyJobNo = familyWith;
                        }

                        break;
                    }
                }

                if(mainFamilyJobNo != "-1")
                {
                    dgv.MultiSelect = true;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        string data = dgv.Rows[i].Cells[text.Header_JobNo].Value.ToString();
                        string familyWith = dgv.Rows[i].Cells[text.Header_FamilyWithJobNo].Value.ToString();

                        if (data != reference && familyWith == mainFamilyJobNo)
                        {
                            dgv.Rows[i].Selected = true;
                        }
                    }
                }

            }
         
        }


        #endregion

        #region plan action history check
        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string jobNo = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_JobNo].Value.ToString();
                int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
                //handle the row selection on right click

                //MessageBox.Show("Plan ID: "+jobNo);
                if (jobNo != null && userPermission >= MainDashboard.ACTION_LVL_THREE)
                {
                    frmPlanningActionHistory frm = new frmPlanningActionHistory(Convert.ToInt32(jobNo));
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (!frmPlanningActionHistory.noData)
                    {
                        frm.ShowDialog();
                    }
                }

            }
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Thread t = null;
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                t = new Thread(new ThreadStart(StartForm));
                New_LoadMacSchedule();
            }
            catch (ThreadAbortException)
            {
                // ignore it
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                t.Abort();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (filterHide)
            {
                filterHide = false;
            }
            else
            {
                filterHide = true;
            }

            HideFilter(filterHide);
        }

        private void cbCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCompleted.Checked)
            {
                cbPending.Checked = false;
                cbRunning.Checked = false;
                cbWarning.Checked = false;
                cbCancelled.Checked = false;
                cbDraft.Checked = false;
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void dgvSchedule_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                CELL_VALUE_CHANGED = false;
                CELL_EDITING_OLD_VALUE = "";

                DataGridView dgv = dgvMacSchedule;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (dgv.Columns[colIndex].Name.Contains(text.Header_Remark)) //Desired Column
                {
                    CELL_EDITING_OLD_VALUE = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvSchedule_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (CELL_VALUE_CHANGED)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int jobNo = int.TryParse(dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_JobNo].Value.ToString(), out jobNo) ? jobNo : 0;

                    planNoteUpdate(jobNo);
                }
            }
        }

        private void dgvSchedule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CELL_VALUE_CHANGED = true;

            DataGridView dgv = dgvMacSchedule;

            CELL_EDITING_NEW_VALUE = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();


        }

        private void label6_Click(object sender, EventArgs e)
        {
            dtpTo.Value = DateTime.Now;
        }

        private void dgvSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string headerName = dgvMacSchedule.Columns[e.ColumnIndex].Name;

                if (headerName == text.Header_RawMat_String || headerName == text.Header_ColorMat || headerName == text.Header_ColorMatCode)
                {
                    string cellValue = dgvMacSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    if (headerName == text.Header_RawMat_String)
                    {
                        cellValue = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_RawMat_1].Value.ToString();

                        string rawMat2 = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_RawMat_2].Value.ToString();

                    }

                    if (headerName == text.Header_ColorMat)
                    {
                        cellValue = dgvMacSchedule.Rows[e.RowIndex].Cells[text.Header_ColorMatCode].Value.ToString();
                    }

                    if (!string.IsNullOrEmpty(cellValue))
                        CalculationMaterialSummary(cellValue);
                    else
                    {
                        MatSummaryReset();

                    }
                }
                else
                {
                    //reset
                    MatSummaryReset();
                }


            }
        }

        readonly private string SCHEDULE_ACTION_RUN = "Run";
        readonly private string SCHEDULE_ACTION_PENDING = "Pending";
        readonly private string SCHEDULE_ACTION_CANCEL = "Cancel";
        readonly private string SCHEDULE_ACTION_COMPLETE = "Complete";
        readonly private string SCHEDULE_ACTION_EDIT_SCHEUDLE = "Edit Schedule";
        readonly private string SCHEDULE_ACTION_JOB_EDIT = "Job Edit";
        readonly private string SCHEDULE_ACTION_ADD_JOB = "Add New Job";
        readonly private string SCHEDULE_ACTION_PUBLISH = "Publish";
        readonly private string SCHEDULE_ACTION_DRAFT = "Draft";
        readonly private string SCHEDULE_ACTION_DATE_EDIT = "Change Date";

        private void dgvSchedule_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvMacSchedule.CurrentCell = dgvMacSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvMacSchedule.Rows[e.RowIndex].Selected = true;
                dgvMacSchedule.Focus();
                int rowIndex = dgvMacSchedule.CurrentCell.RowIndex;
                int colIndex = dgvMacSchedule.CurrentCell.ColumnIndex;

                string currentHeader = dgvMacSchedule.Columns[colIndex].Name;

                try
                {
                    string result = dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Status].Value.ToString();

                    if (result.Equals(text.planning_status_pending))
                    {
                        my_menu.Items.Add(SCHEDULE_ACTION_RUN).Name = text.planning_status_running;
                        my_menu.Items.Add(SCHEDULE_ACTION_DRAFT).Name = text.planning_status_draft;
                        my_menu.Items.Add(SCHEDULE_ACTION_CANCEL).Name = text.planning_status_cancelled;
                        my_menu.Items.Add(SCHEDULE_ACTION_COMPLETE).Name = text.planning_status_completed;

                        my_menu.Items.Add(SCHEDULE_ACTION_JOB_EDIT).Name = SCHEDULE_ACTION_JOB_EDIT;

                    }
                    else if (result.Equals(text.planning_status_cancelled))
                    {

                        my_menu.Items.Add(SCHEDULE_ACTION_RUN).Name = text.planning_status_running;
                        my_menu.Items.Add(SCHEDULE_ACTION_DRAFT).Name = text.planning_status_draft;
                        my_menu.Items.Add(SCHEDULE_ACTION_PENDING).Name = text.planning_status_pending;
                    }
                    else if (result.Equals(text.planning_status_running))
                    {
                        my_menu.Items.Add(SCHEDULE_ACTION_PENDING).Name = text.planning_status_pending;
                        my_menu.Items.Add(SCHEDULE_ACTION_CANCEL).Name = text.planning_status_cancelled;
                        my_menu.Items.Add(SCHEDULE_ACTION_COMPLETE).Name = text.planning_status_completed;

                        my_menu.Items.Add(SCHEDULE_ACTION_JOB_EDIT).Name = SCHEDULE_ACTION_JOB_EDIT;
                    }
                    else if (result.Equals(text.planning_status_completed))
                    {
                        my_menu.Items.Add(SCHEDULE_ACTION_RUN).Name = text.planning_status_running;
                        my_menu.Items.Add(SCHEDULE_ACTION_PENDING).Name = text.planning_status_pending;
                    }
                    else if (result.Equals(text.planning_status_draft))
                    {
                        my_menu.Items.Add(SCHEDULE_ACTION_PUBLISH).Name = text.planning_status_pending;
                        my_menu.Items.Add(SCHEDULE_ACTION_CANCEL).Name = text.planning_status_cancelled;

                        my_menu.Items.Add(SCHEDULE_ACTION_JOB_EDIT).Name = SCHEDULE_ACTION_JOB_EDIT;
                    }
                    else if (result.Equals(text.planning_status_idle))
                    {
                        my_menu.Items.Add(SCHEDULE_ACTION_ADD_JOB).Name = SCHEDULE_ACTION_ADD_JOB;
                    }

                    if (result != text.planning_status_idle && result != text.planning_status_completed && result != text.planning_status_cancelled)
                        my_menu.Items.Add(SCHEDULE_ACTION_DATE_EDIT).Name = SCHEDULE_ACTION_DATE_EDIT;

                    //bool ableToEdit = currentHeader == text.Header_TargetQty;
                    //ableToEdit |= currentHeader == text.Header_Mac;
                    //ableToEdit |= currentHeader == text.Header_DateStart;
                    //ableToEdit |= currentHeader == text.Header_EstDateEnd;
                    //ableToEdit |= currentHeader == text.JobPurpose;
                    //ableToEdit |= currentHeader == text.Header_Remark;

                    //if(ableToEdit)
                    //    my_menu.Items.Add(SCHEDULE_ACTION_JOB_EDIT).Name = SCHEDULE_ACTION_JOB_EDIT;


                    if (currentHeader.Equals(text.Header_RawMat_String) || currentHeader.Equals(text.Header_ColorMat))
                        my_menu.Items.Add(text.planning_Material_Summary).Name = text.planning_Material_Summary;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }




        #region Drag and Drop

        Rectangle dragBoxFromMouseDown;

        private int rowIndexFromMouseDown = -1;

        private int rowIndexOfItemUnderMouseToDrop;

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {

                // If the mouse moves outside the rectangle, start the drag.

                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    if (rowIndexFromMouseDown > -1)
                    {
                        dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvMacSchedule.Rows[rowIndexFromMouseDown].Selected = true;
                    }

                    // Proceed with the drag and drop, passing in the list item.                   

                    DragDropEffects dropEffect = dgvMacSchedule.DoDragDrop(
                             dgvMacSchedule.Rows[rowIndexFromMouseDown],
                             DragDropEffects.Move);

                }

            }

        }


        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            CLEAR_SELECTION_AFTER_DROP = false;

            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            rowIndexFromMouseDown = dgvMacSchedule.HitTest(e.X, e.Y).RowIndex;


            if(userPermission > MainDashboard.ACTION_LVL_THREE && rowIndexFromMouseDown > -1)
            { 
                // Get the index of the item the mouse is below.

                string status = dgvMacSchedule.Rows[rowIndexFromMouseDown].Cells[text.Header_Ori_Status].Value.ToString();
                string item = dgvMacSchedule.Rows[rowIndexFromMouseDown].Cells[text.Header_ItemNameAndCode].Value.ToString();
                bool DragAble = status != text.planning_status_running;
                DragAble &= status != text.planning_status_completed;
                DragAble &= status != text.planning_status_cancelled;
                DragAble &= !string.IsNullOrEmpty(status);
                DragAble &= !string.IsNullOrEmpty(item);

                if (rowIndexFromMouseDown != -1 && DragAble)
                {
                    // Remember the point where the mouse down occurred.
                    // The DragSize indicates the size that the mouse can move
                    // before a drag event should be started.               

                    Size dragSize = SystemInformation.DragSize;


                    // Create a rectangle using the DragSize, with the mouse position being

                    // at the center of the rectangle.

                    dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),e.Y - (dragSize.Height / 2)),dragSize);

                }
                else
                {
                    // Reset the rectangle if the mouse is not over an item in the ListBox.
                    dragBoxFromMouseDown = Rectangle.Empty;
                }

            }


        }


        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            CLEAR_SELECTION_AFTER_DROP = false;
            e.Effect = DragDropEffects.Move;
        }

        bool CLEAR_SELECTION_AFTER_DROP = false;
        private DataRow MAC_SCHEDULE_ROW_TO_DROP;
        private string OLD_MACHINE_ID = null;


        private List<string> LIST_MAC_HISTORY = new List<string>();
        private List<int> MacHistoryList = new List<int>();

        private DataTable DT_MACHINE_SCHEDULE = null;
        private string MACHINE_HISTORY_REMARK = "";
        private void GetMachineHistory(string _ItemCode)
        {
            if(DT_MACHINE_SCHEDULE == null)
            {
                DT_MACHINE_SCHEDULE = dalPlanning.SelectCompletedOrRunningPlan();
            }

            foreach (DataRow row in DT_MACHINE_SCHEDULE.Rows)
            {
                string itemCode = row[dalPlanning.partCode].ToString();

                if (itemCode == _ItemCode)
                {
                    LIST_MAC_HISTORY.Add(row[dalPlanning.machineID].ToString());

                    int macID = int.TryParse(row[dalPlanning.machineID].ToString(), out macID) ? macID : 0;

                    MacHistoryList.Add(macID);
                }
            }

            LIST_MAC_HISTORY = LIST_MAC_HISTORY.Distinct().ToList();

            MACHINE_HISTORY_REMARK = " Machine History : ";

            List<int> noDupes = MacHistoryList.Distinct().ToList();

            noDupes.Sort();

            foreach (int i in noDupes)
            {
                var value = noDupes[noDupes.Count - 1];

                if (i == value)
                {
                    MACHINE_HISTORY_REMARK += i + " ;";

                }
                else
                {
                    MACHINE_HISTORY_REMARK += i + " , ";

                }
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            CLEAR_SELECTION_AFTER_DROP = false;

            Point clientPoint = dgvMacSchedule.PointToClient(new Point(e.X, e.Y));
            int rowIndexToDrop = dgvMacSchedule.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndexToDrop != -1 && e.Effect == DragDropEffects.Move)
            {
                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                DataRow draggedRow = dt.Rows[rowIndexFromMouseDown];
                string Drop_familyWithID = draggedRow[text.Header_FamilyWithJobNo].ToString();

                MAC_SCHEDULE_ROW_TO_DROP = dt.NewRow();
                MAC_SCHEDULE_ROW_TO_DROP.ItemArray = dt.Rows[rowIndexFromMouseDown].ItemArray;

                string MacID_NEW = null;
                string MacName_NEW = null;

                for (int i = rowIndexToDrop; i >= 0; i--)
                {
                    string macID = dt.Rows[i][text.Header_MacID].ToString();
                    string macName = dt.Rows[i][text.Header_Mac].ToString();

                    if ( !string.IsNullOrEmpty(macID))
                    {
                        MacID_NEW = macID;
                        MacName_NEW = macName;
                        break;
                    }

                }

                if(!string.IsNullOrEmpty(MacID_NEW))
                {
                    //check mac history
                    GetMachineHistory(MAC_SCHEDULE_ROW_TO_DROP[text.Header_ItemCode].ToString());

                    if(!LIST_MAC_HISTORY.Contains(MacID_NEW))
                    {
                        string message = "Item "+ MAC_SCHEDULE_ROW_TO_DROP[text.Header_ItemNameAndCode].ToString() + " has never run on machine " + MacName_NEW+ ". Are you sure you want to change the machine? \n\n\n" + MACHINE_HISTORY_REMARK;
                        if (MessageBox.Show(message, "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes
                                                           )
                        {
                            MessageBox.Show("Action Drag-and-Drop cancelled!");
                            return;

                        }
                       
                    }
                    
                }

                dt.Rows.RemoveAt(rowIndexFromMouseDown);

                string newFacID = "";
                string newMacID = "";
                string newFacName = "";
                string newMacName = "";

                int NEWrowIndexToDrop = 0;

                if (rowIndexFromMouseDown > rowIndexToDrop)
                {
                    NEWrowIndexToDrop = 1;
                }

                for (int i = rowIndexToDrop - 1 + NEWrowIndexToDrop; i >= 0; i--)
                {
                    string facID = dt.Rows[i][text.Header_FacID].ToString();
                    string facName = dt.Rows[i][text.Header_Fac].ToString();
                    string macID = dt.Rows[i][text.Header_MacID].ToString();
                    string macName = dt.Rows[i][text.Header_Mac].ToString();

                    if (!string.IsNullOrEmpty(facID) && !string.IsNullOrEmpty(macID))
                    {
                        bool dataChanged = false;

                        dataChanged |= MAC_SCHEDULE_ROW_TO_DROP[text.Header_FacID].ToString() != facID;
                        dataChanged |= MAC_SCHEDULE_ROW_TO_DROP[text.Header_MacID].ToString() != macID;

                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_FacID] = facID;
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_Fac] = facName;
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_MacID] = Convert.ToInt32(macID);
                        MAC_SCHEDULE_ROW_TO_DROP[text.Header_Mac] = macName;

                        if(dataChanged)
                        {
                            DATA_TO_UPDATE = true;
                            MAC_SCHEDULE_ROW_TO_DROP[text.Header_Status] = text.planning_status_to_update;
                        }


                        newFacID = facID;
                        newMacID = macID;
                        newFacName = facName;
                        newMacName = macName;

                        NEWrowIndexToDrop = i;
                        break;
                    }

                }

                rowIndexToDrop = NEWrowIndexToDrop + 1;

                string familyWithID = null;
                int rowIndexOffSet = 0;

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    int familyWith = int.TryParse(row[text.Header_FamilyWithJobNo].ToString(), out familyWith) ? familyWith : 0;
                    //string itemDescription = row[text.Header_ItemDescription].ToString();
                    string jobNo = row[text.Header_JobNo].ToString();

                    if (rowIndex == rowIndexToDrop && familyWith > 0)
                    {
                        if(rowIndex - 1 >= 0)
                        {
                            if (dt.Rows[rowIndex - 1][text.Header_FamilyWithJobNo].ToString() != familyWith.ToString())
                            {
                                break;
                            }
                        }
                        familyWithID = familyWith.ToString();

                        rowIndexOffSet++;
                    }
                    else if (familyWithID == familyWith.ToString() && familyWith > 0 && familyWithID != null)
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

                        rowIndexToDrop--;
                    }

                    // Insert rows at the new position.
                    foreach (DataRow row in rowsToMove)
                    {

                        bool dataChanged = false;

                        dataChanged |= row[text.Header_FacID].ToString() != newFacID;
                        dataChanged |= row[text.Header_MacID].ToString() != newMacID;

                        row[text.Header_FacID] = newFacID;
                        row[text.Header_MacID] = Convert.ToInt32(newMacID);

                        row[text.Header_Fac] = newFacName;
                        row[text.Header_Mac] = newMacName;

                        if(dataChanged)
                        {
                            row[text.Header_Status] = text.planning_status_to_update;
                            DATA_TO_UPDATE = true;
                        }

                        dt.Rows.InsertAt(row, rowIndexToDrop);
                        rowIndexToDrop++;
                    }
                }

                


                if (OLD_MACHINE_ID != null)
                    ResetOldMachineJobDate(OLD_MACHINE_ID);


                MacScheduleListCellFormatting(dgvMacSchedule);
                rowIndexOfItemUnderMouseToDrop = rowIndexToDrop;

                CLEAR_SELECTION_AFTER_DROP = true;

            }

        }

        private bool CheckMachineProductionDateCollision()
        {

            string currentMacID = null;
            string LastFamilyWith = null;
            DateTime LastRow_EndDate = DateTime.MaxValue;

            bool DateCollisionFound = false;

            foreach (DataGridViewRow row in dgvMacSchedule.Rows)
            {
                bool startCollision = false;
                bool endCollision = false;

                string MacID = row.Cells[text.Header_MacID].Value.ToString();
                string FamilyWith = row.Cells[text.Header_FamilyWithJobNo].Value.ToString();
                string status = row.Cells[text.Header_Status].Value.ToString();

                int familyJobNo = int.TryParse(FamilyWith, out familyJobNo) ? familyJobNo : -1;

                DateTime Date_Start = DateTime.TryParse(row.Cells[text.Header_DateStart].Value.ToString(), out Date_Start) ? Date_Start : DateTime.MaxValue;
                DateTime Date_End = DateTime.TryParse(row.Cells[text.Header_EstDateEnd].Value.ToString(), out Date_End) ? Date_End : DateTime.MaxValue;

                if (!string.IsNullOrEmpty(MacID) && status != text.planning_status_completed && status != text.planning_status_cancelled)
                {
                    if (currentMacID != MacID)
                    {
                        currentMacID = MacID;
                        LastRow_EndDate = Date_End;
                    }
                    else
                    {
                        if (Date_Start < LastRow_EndDate && LastRow_EndDate != DateTime.MaxValue && (LastFamilyWith != FamilyWith || FamilyWith == "-1" || string.IsNullOrEmpty(FamilyWith)))
                        {
                            row.Cells[text.Header_DateStart].Style.BackColor = Color.Red;
                            row.Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(254, 241, 154);
                            row.Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);
                            startCollision = true;
                            DateCollisionFound = true;


                            if (Date_End < LastRow_EndDate)
                            {
                                row.Cells[text.Header_EstDateEnd].Style.BackColor = Color.Red;
                                row.Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(254, 241, 154);
                                row.Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);

                                DATE_COLLISION_FOUND = true;
                                endCollision = true;

                            }
                        }
                        else
                        {
                            LastRow_EndDate = Date_End;
                        }

                    }

                    if (Date_End < Date_Start && Date_Start != DateTime.MaxValue)
                    {
                        startCollision = true;
                        endCollision = true;

                        row.Cells[text.Header_DateStart].Style.BackColor = Color.Red;
                        row.Cells[text.Header_EstDateEnd].Style.BackColor = Color.Red;

                        row.Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(254, 241, 154);
                        row.Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(254, 241, 154);

                        row.Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);
                        row.Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);

                        DateCollisionFound = true;


                    }


                    if(familyJobNo > 0 && (startCollision || endCollision))
                    {
                        foreach(DataGridViewRow dgvRow in dgvMacSchedule.Rows)
                        {
                            if(familyJobNo.ToString() == dgvRow.Cells[text.Header_FamilyWithJobNo].Value.ToString())
                            {
                                if(startCollision)
                                {
                                    dgvRow.Cells[text.Header_DateStart].Style.BackColor = Color.Red;
                                    dgvRow.Cells[text.Header_DateStart].Style.ForeColor = Color.FromArgb(254, 241, 154);
                                    dgvRow.Cells[text.Header_DateStart].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);
                                }

                                if (endCollision)
                                {
                                    dgvRow.Cells[text.Header_EstDateEnd].Style.BackColor = Color.Red;
                                    dgvRow.Cells[text.Header_EstDateEnd].Style.ForeColor = Color.FromArgb(254, 241, 154);
                                    dgvRow.Cells[text.Header_EstDateEnd].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold);
                                }
                            }
                        }
                    }

                    LastFamilyWith = FamilyWith;
                }

            }

            DATE_COLLISION_FOUND = DateCollisionFound;

            if (dgvMacSchedule?.Rows.Count > 0)
            {
                btnAdjustCollisionDateBySystem.Visible = DateCollisionFound;

            }
            else
            {
                btnAdjustCollisionDateBySystem.Visible = false;

            }

            adjustAdjustCollisionDateButtonLayout();

            return DateCollisionFound;
        }

        private void ResetOldMachineJobDate(string machineID)
        {
            if (dgvMacSchedule?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    string status = row[text.Header_Status].ToString();


                    if (row[text.Header_MacID].ToString() == machineID)
                    {
                        row[text.Header_DateStart] = row[text.Header_Ori_DateStart];
                        row[text.Header_EstDateEnd] = row[text.Header_Ori_EstDateEnd];
                        row[text.Header_Status] = row[text.Header_Ori_Status];
                    }
                }
            }
        }

        #endregion

        private void dgvMacSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (CLEAR_SELECTION_AFTER_DROP)
            {
                dgvMacSchedule.ClearSelection();

                if (rowIndexOfItemUnderMouseToDrop > -1 && rowIndexOfItemUnderMouseToDrop < dgvMacSchedule.Rows.Count)
                {
                    dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    dgvMacSchedule.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
                    CLEAR_SELECTION_AFTER_DROP = false;

                }

            }
        }

//         if (CLEAR_SELECTION_AFTER_DROP)
//            {
//                dgvMacSchedule.ClearSelection();

//                if (rowIndexOfItemUnderMouseToDrop > -1 && rowIndexOfItemUnderMouseToDrop<dgvMacSchedule.Rows.Count)
//                {
//                    dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

//                    dgvMacSchedule.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
//                    CLEAR_SELECTION_AFTER_DROP = false;

//                }

//}
private void dgvMacSchedule_MouseClick(object sender, MouseEventArgs e)
        {
            var hit = dgvMacSchedule.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.Cell && hit.RowIndex > -1)
            {
                dgvMacSchedule.ClearSelection();
                dgvMacSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvMacSchedule[hit.ColumnIndex, hit.RowIndex].Selected = true;
            }

        }

        private int TotalSundaysBetween(DateTime startDate, DateTime endDate)
        {
            int totalSundays = 0;

            if (endDate < startDate)
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

        private void AutoAdjustCollisionDate()
        {
            if (dgvMacSchedule?.Rows.Count > 0)
            {
                bool includedSunday = false;

                //if (MessageBox.Show("Do you want to include Sunday as a working day?", "Message",
                //                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                //                                            )
                //{
                //    includedSunday = true;
                //}

                string currentMacID = null;
                string LastFamilyWith = null;
                DateTime LastRow_EndDate = DateTime.MaxValue;

                DataTable dt = (DataTable)dgvMacSchedule.DataSource;

                bool dateAdjusted = false;

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
                            if (Date_Start < LastRow_EndDate && LastRow_EndDate != DateTime.MaxValue && (LastFamilyWith != FamilyWith || FamilyWith == "-1" || string.IsNullOrEmpty(FamilyWith)))
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

                        if (startError || endError)
                        {
                            int rowIndex = dt.Rows.IndexOf(row);

                            int proDay = int.TryParse(row[text.Header_ProductionDay].ToString(), out proDay) ? proDay : 0;
                            int proHourPerDay = int.TryParse(row[text.Header_ProductionHourPerDay].ToString(), out proHourPerDay) ? proHourPerDay : 0;
                            double proBalHour = double.TryParse(row[text.Header_ProductionHour].ToString(), out proBalHour) ? proBalHour : 0;

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
                            row[text.Header_DateStart] = Date_Start;
                            row[text.Header_EstDateEnd] = Date_End;

                            dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Status].Value = text.planning_status_to_update;
                            dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value = Date_Start;
                            dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value = Date_End;
                            

                            LastRow_EndDate = Date_End;

                            dateAdjusted = true;
                        }

                        LastFamilyWith = FamilyWith;
                    }

                }

                MacScheduleListCellFormatting(dgvMacSchedule);
                DATE_COLLISION_FOUND = false;
               

                if(dateAdjusted)
                {
                    DATA_TO_UPDATE = true;
                    MessageBox.Show("Collision Date adjusted by System.\n\nPlease click the save button below to update data.");
                }

                adjustAdjustCollisionDateButtonLayout();
            }
            else
            {
                MessageBox.Show("Data not found.");
            }
        }

        private void JobUpdate()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            bool JOB_UPDATED = false;
            int JOB_UPDATED_COUNT = 0;

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
                            string OriMachineName = row[text.Header_Ori_Machine_Name].ToString();
                            string remark = row[text.Header_Remark].ToString();

                            DateTime newStart = DateTime.TryParse(row[text.Header_DateStart].ToString(), out newStart) ? newStart : DateTime.MaxValue;
                            DateTime newEnd = DateTime.TryParse(row[text.Header_EstDateEnd].ToString(), out newEnd) ? newEnd : DateTime.MaxValue;

                            DateTime oriStart = DateTime.TryParse(row[text.Header_Ori_DateStart].ToString(), out oriStart) ? oriStart : DateTime.MaxValue;
                            DateTime oriEnd = DateTime.TryParse(row[text.Header_Ori_EstDateEnd].ToString(), out oriEnd) ? oriEnd : DateTime.MaxValue;

                            int familyWith = int.TryParse(row[text.Header_FamilyWithJobNo].ToString(), out familyWith) ? familyWith : -1;
                            int newMachineID = int.TryParse(row[text.Header_MacID].ToString(), out newMachineID) ? newMachineID : -1;

                            uPlanning.plan_id = jobNo;
                            uPlanning.plan_status = OriStatus;
                            uPlanning.production_start_date = newStart;
                            uPlanning.production_end_date = newEnd;
                            uPlanning.family_with = familyWith;
                            uPlanning.plan_note = remark;
                            uPlanning.machine_id = newMachineID;
                            uPlanning.machine_name = row[text.Header_Mac].ToString();
                            

                            JOB_UPDATED = dalPlanningAction.JobDateAndMachineUpdate(uPlanning, OriMachineName, oriStart, oriEnd, familyWith);

                            if (JOB_UPDATED)
                            {

                                int rowIndex = dt.Rows.IndexOf(row);

                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_DateStart].Value = newStart;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Ori_DateStart].Value = newStart;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_EstDateEnd].Value = newEnd;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Ori_EstDateEnd].Value = newEnd;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Status].Value = OriStatus;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_FamilyWithJobNo].Value = familyWith;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Ori_Machine_Name].Value = uPlanning.machine_name;
                                dgvMacSchedule.Rows[rowIndex].Cells[text.Header_Status].Style.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular);

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
                if (JOB_UPDATED_COUNT > 0)
                {
                    MessageBox.Show(JOB_UPDATED_COUNT + " Job(s) Updated.");
                }

            }

            DATA_TO_UPDATE = !JOB_UPDATED;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        private bool DATA_TO_UPDATE = false;
        private void btnAdjustCollisionDateBySystem_Click(object sender, EventArgs e)
        {
            if(DATE_COLLISION_FOUND)
            AutoAdjustCollisionDate();

            else if(DATA_TO_UPDATE)
            {
                JobUpdate();

                MacScheduleListCellFormatting(dgvMacSchedule);
            }
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                filterHide = true;
                HideFilter(filterHide);

                ResetData();
                New_LoadMacSchedule();
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

        private void dgvMacSchedule_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvSmallView(DataGridView dgv)
        {
            frmLoading.ShowLoadingScreen();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells[text.Header_ItemCode].Value.ToString()))
                    row.Height = 38;  
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 7.5F, FontStyle.Regular);

            //dgv.Columns[text.Header_Job_Purpose].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            //dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            //dgv.Columns[text.Header_Mac].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_TargetQty].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            //dgv.Columns[text.Header_RawMat_Qty].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_JobNo].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_ProducedQty].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_ColorMat_KG].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            frmLoading.CloseForm();

            lblSmallFontRow.Visible = false;
        }
        private void label7_Click(object sender, EventArgs e)
        {
            dgvSmallView(dgvMacSchedule);
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            MainDashboard.MACHINE_SCHEDULE_SWITCH_TO_OLD_VERSION = true;
            MainDashboard.OpenOldVersionMachineSchedule();
            Close();
        }
    }
}
