using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanning : Form
    {
        #region Variable Declare
        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        Text text = new Text();
        matPlanDAL dalmatPlan = new matPlanDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        habitDAL dalHabit = new habitDAL();
        habitBLL uHabit = new habitBLL();

        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();

        planningActionDAL dalPlanAction = new planningActionDAL();
        itemForecastDAL dalItemForecast = new itemForecastDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();

        readonly string headerCheck = "FOR";
        readonly string headerDescription = "DESCRIPTION";
        readonly string headerBalance = "ESTIMATE CLOSING BALANCE";

        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        static public readonly string headerCode = "CODE";
        static public readonly string headerName = "NAME";
        static public readonly string headerShort = "SHORT";
        readonly string headerStock = "CURRENT STOCK";
        readonly string headerPlanningUsed = "PLANNED TO USE";
        readonly string headerAvaiableQty = "AVAIABLE QTY";
        static public  readonly string headerQtyNeedForThisPlanning = "QTY NEED FOR THIS PLANNING";
        
        readonly string OtherPurpose = "FOR OTHER PURPOSE";

        static public bool planEditing = false;
    
        private bool ableToCalculateQty = true;
        private bool ableToLoadData = false;
        private bool materialChecked = false;
        private bool partInfoEdited = false;
        private bool loaded = false;

        private string purpose = "FOR OTHER PURPOSE";
        private string name = null;
        private string code = null;

        private static frmLoading splashForm;
        #endregion

        public frmPlanning()
        {
            InitializeComponent();

            InitialData();

            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                cbEditMode.Visible = true;
            }
        }

        public frmPlanning(string itemName, string itemCode)
        {
            InitializeComponent();

            InitialData();

            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                cbEditMode.Visible = true;
            }

            name = itemName;
            code = itemCode;
        }

        public frmPlanning(int planID)
        {
            InitializeComponent();

            InitialData();

            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                cbEditMode.Visible = true;
            }
        }

        private void reIndex(DataTable dt)
        {
            int index = 1;

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    row[headerIndex] = index;

                    index++;
                }
            }
        }

        private void InitialData()
        {
            string keywords = "Part";

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.CatSearch(keywords);
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbPartName.DataSource = distinctTable;
                cmbPartName.DisplayMember = "item_name";
                cmbPartName.ValueMember = "item_name";

                if (string.IsNullOrEmpty(cmbPartName.Text))
                {
                    cmbPartCode.DataSource = null;
                }
            }
            else
            {
                cmbPartName.DataSource = null;
            }

            cmbPartName.SelectedIndex = -1;

            loadHabitData();

        }

        private void InitialData(int PlanID)
        {
            string keywords = "Part";

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.CatSearch(keywords);
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbPartName.DataSource = distinctTable;
                cmbPartName.DisplayMember = "item_name";
                cmbPartName.ValueMember = "item_name";

                if (string.IsNullOrEmpty(cmbPartName.Text))
                {
                    cmbPartCode.DataSource = null;
                }
            }
            else
            {
                cmbPartName.DataSource = null;
            }

            cmbPartName.SelectedIndex = -1;

            loadHabitData();

            //load planing db data, search planid
            DataTable dt_plan = dalPlan.idSearch(PlanID.ToString());

            foreach(DataRow row in dt_plan.Rows)
            {
                string itemCode = row[dalPlan.partCode].ToString();
                string itemName = tool.getItemName(itemCode);

            }
            //set item name
            //set item code
            //set purpose
            //set raw material code (get from material plan list)
            //set color material code ( get from material plan list)
            //
            //set

        }

        private void loadHabitData()
        {
            string belongTo = text.habit_belongTo_PlanningPage;
            string HourPerDayHabitData = "23";
            string WastageHabitData = "5";

            DataTable dt = dalHabit.HabitSearch(belongTo);

            foreach(DataRow row in dt.Rows)
            {
                string name = row[dalHabit.HabitName].ToString();

                if(name.Equals(text.habit_planning_HourPerDay))
                {
                    if (float.TryParse(row[dalHabit.HabitData].ToString(), out float result))
                    {
                        HourPerDayHabitData = result.ToString();
                    }

                }

                if (name.Equals(text.habit_planning_Wastage))
                {
                    if (float.TryParse(row[dalHabit.HabitData].ToString(), out float result))
                    {
                        WastageHabitData = result.ToString();
                    }

                }

            }

            txtHoursPerDay.Text = HourPerDayHabitData;
            txtMatWastage.Text = WastageHabitData;
        }

        private DataTable NewForecastTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerCheck, typeof(bool));
            dt.Columns.Add(headerDescription, typeof(string));
            dt.Columns.Add(headerBalance, typeof(float));

            return dt;
        }

        private DataTable NewMatCheckTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));

            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerStock, typeof(float));
            dt.Columns.Add(headerPlanningUsed, typeof(float));

            dt.Columns.Add(headerAvaiableQty, typeof(float));
            dt.Columns.Add(headerQtyNeedForThisPlanning, typeof(float));
            dt.Columns.Add(headerShort, typeof(float));

            return dt;
        }

        private void dgvForecastUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerCheck].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerBalance].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerDescription].ReadOnly = true;
            dgv.Columns[headerBalance].ReadOnly = true;
        }

        private void dgvCheckListUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPlanningUsed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAvaiableQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerQtyNeedForThisPlanning].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerShort].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerQtyNeedForThisPlanning].DefaultCellStyle.Format = "0.##";
        }

        private void LoadPartInfo()
        {
            string itemCode = cmbPartCode.Text;

            DataTable dt = dalItem.codeSearch(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCat].ToString().Equals("Part"))
                    {
                        float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : 0;
                        float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : 0;

                        txtPartWeight.Text = partWeight.ToString("0.##");
                        txtRunnerWeight.Text = runnerWeight.ToString("0.##");
                        txtCavity.Text = row[dalItem.ItemCavity] == DBNull.Value ? "" : row[dalItem.ItemCavity].ToString();
                        txtProCT.Text = row[dalItem.ItemProCTTo] == DBNull.Value ? "" : row[dalItem.ItemProCTTo].ToString();
                        txtQuoCT.Text = row[dalItem.ItemQuoCT] == DBNull.Value ? "" : row[dalItem.ItemQuoCT].ToString();

                        if (string.IsNullOrEmpty(txtProCT.Text))
                        {
                            txtProCT.Text = row[dalItem.ItemQuoCT] == DBNull.Value ? "" : row[dalItem.ItemQuoCT].ToString();
                        }

                        txtQuoTon.Text = row[dalItem.ItemQuoTon] == DBNull.Value ? "" : row[dalItem.ItemQuoTon].ToString();
                        txtProTon.Text = row[dalItem.ItemProTon] == DBNull.Value ? "" : row[dalItem.ItemProTon].ToString();

                        string rawMaterial = row[dalItem.ItemMaterial] == DBNull.Value ? "" : row[dalItem.ItemMaterial].ToString();
                        LoadRawMaterial(rawMaterial);

                        string colorMaterial = row[dalItem.ItemMBatch] == DBNull.Value ? "" : row[dalItem.ItemMBatch].ToString();
                        LoadColorMaterial(colorMaterial);

                        float colorRate = row[dalItem.ItemMBRate] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemMBRate]);
                        txtColorUsage.Text = (colorRate * 100).ToString();

                        string colorName = row[dalItem.ItemColor] == DBNull.Value ? "" : row[dalItem.ItemColor].ToString();
                        txtColorName.Text = colorName;
                    }
                }
            }

        }

        private void LoadColorMaterial(string matCode)
        {
            tool.loadAllColorMatToComboBox(cmbColorMatCode);

            if (dalItem.getCatName(matCode).Equals("Pigment"))
            {
                //tool.loadPigmentToComboBox(cmbColorMatCode);
                cmbColorMatCode.Text = matCode;
            }
            else
            {
                //tool.loadMasterBatchToComboBox(cmbColorMatCode);
                cmbColorMatCode.Text = matCode;
            }
        }

        private void LoadRawMaterial(string matName)
        {
            tool.loadRAWMaterialToComboBox(cmbMatCode);
            cmbMatCode.Text = matName;
        }

        private void CalTotalMatAfterWastage()
        {
            float totalMaterial = 0;

            if (txtMatAfterWastage.Text != "")
            {
                totalMaterial = Convert.ToSingle(txtMatAfterWastage.Text);
            }

            if (cbRecycleUse.Checked && txtRecycleKG.Text != "")
            {
                totalMaterial += Convert.ToSingle(txtRecycleKG.Text);
            }


            txtTotalMat.Text = totalMaterial.ToString("0.###");

            ////calculate total hours need
            //CalculateProductionDaysAndHours();
        }

        private void CalculateProductionDaysAndHours()
        {
            //CalTotalRecycleMat();

            float totalRawMat = txtMatBeforeWastage.Text == "" ? 0 : Convert.ToSingle(txtMatBeforeWastage.Text);
            float totalRecycleMat = txtRecycleKG.Text == "" ? 0 : Convert.ToSingle(txtRecycleKG.Text);

            float totalMatBeforeWastage = totalRawMat + totalRecycleMat;

            float partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);
            int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);

            if (partWeightPerShot == 0)
            {
                partWeightPerShot = 1;
            }

            if (runnerWeightPerShot == 0)
            {
                runnerWeightPerShot = 1;
            }

            int TotalShot = Convert.ToInt32(Math.Floor(totalMatBeforeWastage * 1000 / (partWeightPerShot + runnerWeightPerShot)));

            //calculate total hours need
            int cycleTime = txtProCT.Text == "" ? 0 : Convert.ToInt32(txtProCT.Text);
            int totalSec = cycleTime * TotalShot;

            float totalHrs = Convert.ToSingle(totalSec) / 3600;

            int hoursPerDay = txtHoursPerDay.Text == "" ? 0 : Convert.ToInt32(txtHoursPerDay.Text);

            int totalDay = 0;

            if (hoursPerDay != 0)
            {
                totalDay = Convert.ToInt32(Math.Floor(totalHrs / Convert.ToSingle(hoursPerDay)));
            }

            txtProDays.Text = totalDay.ToString();

            txtProHours.Text = (totalHrs - Convert.ToSingle(totalDay * hoursPerDay)).ToString("0.##");
        }

        private void CalTotalRecycleMat()
        {
            float totalRecycleMat = txtMatAfterWastage.Text == "" ? 0 : Convert.ToSingle(txtMatAfterWastage.Text);
            totalRecycleMat *= 1000;

            float partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);

            float totalWeightPerShot = partWeightPerShot + runnerWeightPerShot;

            if (totalWeightPerShot == 0)
            {
                totalWeightPerShot = 1;
            }

            if (partWeightPerShot != 0 && runnerWeightPerShot != 0)
            {
                totalRecycleMat = runnerRecycleCalculate(totalRecycleMat / (totalWeightPerShot) * runnerWeightPerShot);
                
            }
            else
            {
                totalRecycleMat = 0;
            }

            if(totalRecycleMat != 0)
            {
                totalRecycleMat /= 1000;
            }

            txtRecycleKG.Text = totalRecycleMat.ToString("0.###");
        }

        private float runnerRecycleCalculate(float runnerLeft)
        {
            float partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);

            if (runnerLeft > (partWeightPerShot+runnerWeightPerShot))
            {
                return Convert.ToSingle(Math.Floor(runnerRecycleCalculate(runnerLeft / (partWeightPerShot + runnerWeightPerShot) * runnerWeightPerShot) + runnerLeft));
            }
            else
            {
                return 0;
            }
            
        }

        private void CalColorMatAndTotalRawMatBeforeWastage()
        {
            materialChecked = false;
            dgvCheckList.DataSource = null;
            float totalRawMat = 0;
            float totalColorMat = 0;

            if (txtMatBagQty.Text != "" && txtMatBagKG.Text != "" && txtColorUsage.Text != "")
            {
                totalRawMat = Convert.ToSingle(txtMatBagQty.Text) * Convert.ToSingle(txtMatBagKG.Text);
                totalColorMat = totalRawMat * Convert.ToSingle(txtColorUsage.Text) / 100;
            }

            totalColorMat *= 1000;
            totalColorMat = (float)Math.Floor(totalColorMat);

            totalColorMat /= 1000;

            txtColorMatPlannedQty.Text = totalColorMat.ToString("0.###");
            txtMatBeforeWastage.Text = (totalRawMat + totalColorMat).ToString("0.###");
        }

        private float GetForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            month += forecastNum - 1;

            if (month > 12)
            {
                month -= 12;
                year++;

            }
            return tool.getItemForecast(dt_ItemForecast, itemCode, year, month);
        }

        private float GetMaxOut(string itemCode, string customer, int pastMonthQty, DataTable dt_TrfHist, DataTable dt_PMMADate)
        {
            float deliveredOut = 0;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            if (customer.Equals("PMMA"))
            {
                DateTime start = tool.GetPMMAStartDate(currentMonth, currentYear, dt_PMMADate);
                DateTime end = tool.GetPMMAEndDate(currentMonth, currentYear, dt_PMMADate);

                foreach (DataRow row in dt_TrfHist.Rows)
                {
                    string item = row[dalTrfHist.TrfItemCode].ToString();
                    if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                    {
                        DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                        //string cust = row[dalTrfHist.TrfTo].ToString();


                        if (trfDate >= start && trfDate <= end)
                        {
                            deliveredOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                        }
                    }
                }

            }
            else
            {
                DateTime start = new DateTime(currentYear, currentMonth, 1);
                DateTime end = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));

                foreach (DataRow row in dt_TrfHist.Rows)
                {
                    string item = row[dalTrfHist.TrfItemCode].ToString();
                    if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                    {
                        DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                        if (trfDate >= start && trfDate <= end)
                        {
                            deliveredOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                        }
                    }
                }

            }
            return deliveredOut;
        }

        private void AddDataToForecastTable()
        {
            DataTable dt_Forecast = NewForecastTable();
            DataRow row_dtForecast;
            DataTable dt_ItemForecast = dalItemForecast.Select();

            int month, year;
            string itemCode = cmbPartCode.Text;

            if (!string.IsNullOrEmpty(itemCode))
            {
                float readyStock = dalItem.getStockQty(itemCode);
                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = "READY STOCK";
                row_dtForecast[headerBalance] = readyStock;

                dt_Forecast.Rows.Add(row_dtForecast);

                month = DateTime.Now.Month;
                year = DateTime.Now.Year;

                string monthName;
                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);

                float forecast1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                float forecast2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                float forecast3 = GetForecastQty(dt_ItemForecast, itemCode, 3);

                forecast1 = forecast1 == -1 ? 0 : forecast1;
                forecast2 = forecast2 == -1 ? 0 : forecast2;
                forecast3 = forecast3 == -1 ? 0 : forecast3;

                string customer = tool.getCustomerName(itemCode);
                DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerSearch(customer);
                DataTable dt_PMMADate = dalPmmaDate.Select();

                float deliveredOut = GetMaxOut(itemCode, customer, 0, dt_TrfHist, dt_PMMADate);

                float balance1 = readyStock - forecast1 + deliveredOut;
                float balance2 = balance1 - forecast2;
                float balance3 = balance2 - forecast3;

                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = monthName+" "+year;
                row_dtForecast[headerBalance] = balance1;

                dt_Forecast.Rows.Add(row_dtForecast);

                if (month != 12)
                {
                    month++;
                }
                else
                {
                    month = 1;
                    year++;
                }

                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = monthName + " " + year;
                row_dtForecast[headerBalance] = balance2; /*tool.GetBalanceStock(itemCode, month, year);*/

                dt_Forecast.Rows.Add(row_dtForecast);

                if (month != 12)
                {
                    month++;
                }
                else
                {
                    month = 1;
                    year++;
                }

                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = monthName + " " + year;
                row_dtForecast[headerBalance] = balance3;/* tool.GetBalanceStock(itemCode, month, year);*/

                dt_Forecast.Rows.Add(row_dtForecast);
            }
            dgvCheckList.DataSource = null;
            if (dt_Forecast.Rows.Count > 0)
            {
                dgvForecast.DataSource = dt_Forecast;
                dgvForecastUIEdit(dgvForecast);
                dgvForecast.ClearSelection();
            }
        }

        #region KeyPress

        private void txtMatBagQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMatBagKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRecycleWastage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtColorUsage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;

        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTargetQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtCavity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
                e.Handled = true;
        }

        private void txtPartWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            else
                e.Handled = true;
        }

        private void txtQuoTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
                e.Handled = true;
        }

        private void txtRunnerWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            else
                e.Handled = true;
        }

        private void txtProTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
                e.Handled = true;
        }

        private void txtCycleTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
                
        }

        private void txtRecycleKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTotalMatKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtColorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void txtColorMatPlannedQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtAbleToProduceQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        private void frmPlanning_Load(object sender, EventArgs e)
        {
            CalTotalRecycleMat();
            ActiveControl = cmbPartName;

            if(name != null)
            {
                cmbPartName.Text = name;
                cmbPartCode.Text = code;
            }
            loaded = true;
        }

        private void cmbPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            errorProvider1.Clear();
            string keywords = cmbPartName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                ableToLoadData = false;
                DataTable dt = dalItem.nameSearch(keywords);

                foreach(DataRow row in dt.Rows)
                {
                    string itemCat = row[dalItem.ItemCat].ToString();

                    if(!itemCat.Equals(text.Cat_Part))
                    {
                        row.Delete();
                    }
                }

                dt.AcceptChanges();

                cmbPartCode.DataSource = dt;
                cmbPartCode.DisplayMember = "item_code";
                cmbPartCode.ValueMember = "item_code";
                cmbPartCode.SelectedIndex = -1;
                ableToLoadData = true;

                int count = cmbPartCode.Items.Count;

                if(count == 1)
                {
                    cmbPartCode.SelectedIndex = 0;
                }
            }
            else
            {
                cmbPartCode.DataSource = null;
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            materialChecked = false;
            dgvCheckList.DataSource = null;
            errorProvider2.Clear();
            
            if (ableToLoadData)
            {
                try
                {
                    dgvForecast.DataSource = null;
                    cmbMatCode.SelectedIndex = -1;
                    txtTargetQty.Clear();
                    txtMatBagQty.Clear();
                    cmbColorMatCode.SelectedIndex = -1;

                 
                    if (dalItem.checkIfAssembly(cmbPartCode.Text) && !dalItem.checkIfProduction(cmbPartCode.Text) && tool.ifGotChild(cmbPartCode.Text))
                    {
                        MessageBox.Show(cmbPartCode.Text + " " + cmbPartName.Text + " is a assembly part!");
                    }
                    else
                    {
                        frmLoading.ShowLoadingScreen();
                        LoadPartInfo();
                        partInfoEdited = false;
                      
                        //CalTotalMatAfterWastage();
                    }
                    AddDataToForecastTable();
                }
             
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    frmLoading.CloseForm();
                    txtMatBagQty.Focus();
                }
            }
            if(!cbEditMode.Checked)
            {
                btnPartInfoSave.Visible = false;
            }

            btnRawMatUpdate.Visible = false;
            btnColorMatUpdate.Visible = false;
        }

        private void cbRecycleUse_CheckedChanged(object sender, EventArgs e)
        {
            CalTotalMatAfterWastage();
        }

        private void CalRawMatAfterWastage()
        {
            materialChecked = false;
            dgvCheckList.DataSource = null;
            float totalMaterialAfterWastage = txtMatBeforeWastage.Text == "" ? 0 : Convert.ToSingle(txtMatBeforeWastage.Text);

            float wastage = txtMatWastage.Text == "" ? 0 : Convert.ToSingle(txtMatWastage.Text);

            totalMaterialAfterWastage = totalMaterialAfterWastage * (1 - wastage / 100);

            totalMaterialAfterWastage = (float)Math.Floor(totalMaterialAfterWastage * 100) / 100;
            txtMatAfterWastage.Text = totalMaterialAfterWastage.ToString("0.##");

           
        }

        private void txtMatBagQty_TextChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                errorProvider4.Clear();

                CalColorMatAndTotalRawMatBeforeWastage();
                CalRawMatAfterWastage();
                CalTotalRecycleMat();
                CalTotalMatAfterWastage();

                //give warning if qty have decimal point
                double d = txtMatBagQty.Text.Equals("") ? 0 : Convert.ToDouble(txtMatBagQty.Text);

                if (!(Math.Abs(d % 1) <= (double.Epsilon * 100)))
                {
                    errorProvider4.SetError(txtMatBagQty, "Decimal Point Exist");
                }
            }
            
        }

        private void txtMatBagKG_TextChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                CalColorMatAndTotalRawMatBeforeWastage();
                CalRawMatAfterWastage();
                CalTotalRecycleMat();
                CalTotalMatAfterWastage();
            }  
        }

        private void txtRecycleKG_TextChanged(object sender, EventArgs e)
        {
            //CalculateTotalRawMaterial();
        }

        private void txtRawMatWastage_TextChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                CalRawMatAfterWastage();
                CalTotalRecycleMat();
                CalTotalMatAfterWastage();
            }

            string habitData = txtMatWastage.Text;
            //save habit
            uHabit.belong_to = text.habit_belongTo_PlanningPage;
            uHabit.habit_name = text.habit_planning_Wastage;
            uHabit.habit_data = habitData;
            uHabit.added_date = DateTime.Now;
            uHabit.added_by = MainDashboard.USER_ID;

            dalHabit.HabitInsertAndHistoryRecord(uHabit);
        }

        private void txtTotalMatKG_TextChanged(object sender, EventArgs e)
        {
            //materialChecked = false;
            //CalculateTotalColorMaterial();
            //dgvCheckList.DataSource = null;

            //float txttotalMat = txtTotalRawMatKG.Text.Equals("") ? 0 : Convert.ToSingle(txtTotalRawMatKG.Text);
            //float txtcolorMat = txtColorMat.Text.Equals("")? 0 : Convert.ToSingle(txtColorMat.Text);

            //txtTotalMat.Text = (txttotalMat + txtcolorMat).ToString();
        }

        private void txtColorUsage_TextChanged(object sender, EventArgs e)
        {
            btnColorMatUpdate.Visible = true;
            if (loaded)
            {
                CalColorMatAndTotalRawMatBeforeWastage();
                CalTotalMatAfterWastage();
            }
           
        }

        private void txtHoursPerDay_TextChanged(object sender, EventArgs e)
        {
            CalculateProductionDaysAndHours();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private int MAXQtyPerBagCanProduce()
        {
            materialChecked = false;
            dgvCheckList.DataSource = null;
            float totalRawMat = 0;
            float totalColorMat = 0;

            if (txtMatBagKG.Text != "")
            {
                totalRawMat = 1 * Convert.ToSingle(txtMatBagKG.Text);
            }

            if (txtColorUsage.Text != "")
            {
                totalColorMat = totalRawMat * Convert.ToSingle(txtColorUsage.Text) / 100;
            }

            totalColorMat *= 1000;
            totalColorMat = (float)Math.Floor(totalColorMat);

            totalColorMat /= 1000;

            float TotalMatBeforeWastage = totalRawMat + totalColorMat;

            float wastage = txtMatWastage.Text == "" ? 0 : Convert.ToSingle(txtMatWastage.Text);

            float totalMaterialAfterWastage = TotalMatBeforeWastage * (1 - wastage / 100);
            totalMaterialAfterWastage = (float)Math.Floor(totalMaterialAfterWastage * 100) / 100;

            float totalRecycleMat = 0;

            float partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);
            float totalWeightPerShot = partWeightPerShot + runnerWeightPerShot;

            if (totalWeightPerShot == 0)
            {
                totalWeightPerShot = 1;
            }

            if (cbRecycleUse.Checked)
            {
                totalRecycleMat = totalMaterialAfterWastage;
                totalRecycleMat *= 1000;

                

                

                if (partWeightPerShot != 0 && runnerWeightPerShot != 0)
                {
                    totalRecycleMat = runnerRecycleCalculate(totalRecycleMat / (totalWeightPerShot) * runnerWeightPerShot);

                }
                else
                {
                    totalRecycleMat = 0;
                }

                if (totalRecycleMat != 0)
                {
                    totalRecycleMat /= 1000;
                }

            }

            float TotalMat = totalMaterialAfterWastage + totalRecycleMat;

            int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);

            int TotalShot = Convert.ToInt32(Math.Floor(TotalMat * 1000 / (totalWeightPerShot)));

            return TotalShot * cavity;
        }

        private int MAXQtyPerBagCanProduce(int bagQty)
        {
            materialChecked = false;
            dgvCheckList.DataSource = null;
            float totalRawMat = 0;
            float totalColorMat = 0;

            if (txtMatBagKG.Text != "")
            {
                totalRawMat = bagQty * Convert.ToSingle(txtMatBagKG.Text);
            }

            if (txtColorUsage.Text != "")
            {
                totalColorMat = totalRawMat * Convert.ToSingle(txtColorUsage.Text) / 100;
            }

            totalColorMat *= 1000;
            totalColorMat = (float)Math.Floor(totalColorMat);

            totalColorMat /= 1000;

            float TotalMatBeforeWastage = totalRawMat + totalColorMat;

            float wastage = txtMatWastage.Text == "" ? 0 : Convert.ToSingle(txtMatWastage.Text);

            float totalMaterialAfterWastage = TotalMatBeforeWastage * (1 - wastage / 100);
            totalMaterialAfterWastage = (float)Math.Floor(totalMaterialAfterWastage * 100) / 100;

            float totalRecycleMat = 0;

            float partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);

            if (cbRecycleUse.Checked)
            {
                totalRecycleMat = totalMaterialAfterWastage;
                totalRecycleMat *= 1000;

                float totalWeightPerShot = partWeightPerShot + runnerWeightPerShot;

                if (totalWeightPerShot == 0)
                {
                    totalWeightPerShot = 1;
                }

                if (partWeightPerShot != 0 && runnerWeightPerShot != 0)
                {
                    totalRecycleMat = runnerRecycleCalculate(totalRecycleMat / (totalWeightPerShot) * runnerWeightPerShot);

                }
                else
                {
                    totalRecycleMat = 0;
                }

                if (totalRecycleMat != 0)
                {
                    totalRecycleMat /= 1000;
                }

            }

            float TotalMat = totalMaterialAfterWastage + totalRecycleMat;

            int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);

            int TotalShot = Convert.ToInt32(Math.Floor(TotalMat * 1000 / (partWeightPerShot + runnerWeightPerShot)));

            return TotalShot * cavity;
        }

        private void txtTargetQty_TextChanged(object sender, EventArgs e)
        {
            materialChecked = false;
            dgvCheckList.DataSource = null;
            if (ableToCalculateQty)
            {
                int MaxQtyAbleToProducePerBag = MAXQtyPerBagCanProduce();

                int TargetQty = txtTargetQty.Text == "" ? 0 : Convert.ToInt32(txtTargetQty.Text);


                int TotalRawMatBagQty = TargetQty/ MaxQtyAbleToProducePerBag;

                MaxQtyAbleToProducePerBag = MAXQtyPerBagCanProduce(TotalRawMatBagQty);

                if(MaxQtyAbleToProducePerBag < TargetQty)
                {
                    TotalRawMatBagQty++;
                }


                txtMatBagQty.Text = TotalRawMatBagQty.ToString();
            }
        }

        private void txtProDays_TextChanged(object sender, EventArgs e)
        {
            //double matBagKG = txtMatBagKG.Text == "" ? 0 : Convert.ToDouble(txtMatBagKG.Text);
            //double partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToDouble(txtPartWeight.Text);
            //double runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToDouble(txtRunnerWeight.Text);

            //double proDay = txtProDays.Text == "" ? 0 : Convert.ToDouble(txtProDays.Text);
            //double proHours = txtProHours.Text == "" ? 0 : Convert.ToDouble(txtProHours.Text);
            //double hoursPerDay = txtHoursPerDay.Text == "" ? 0 : Convert.ToDouble(txtHoursPerDay.Text);

            //double totalHours = proDay * hoursPerDay + proHours;

            //double cycleTime = txtCycleTime.Text == "" ? 0 : Convert.ToInt32(txtCycleTime.Text);

            //double totalShot = 0;

            //if (cycleTime != 0)
            //{
            //    totalShot = Math.Floor(Convert.ToDouble(totalHours) / Convert.ToDouble(cycleTime));
            //}

            //txtMatBagQty.Text = (totalShot * (partWeightPerShot + runnerWeightPerShot) / 1000 / matBagKG).ToString("0.##");
        }

        private void txtProHours_TextChanged(object sender, EventArgs e)
        {
            //double matBagKG = txtMatBagKG.Text == "" ? 0 : Convert.ToDouble(txtMatBagKG.Text);
            //double partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToDouble(txtPartWeight.Text);
            //double runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToDouble(txtRunnerWeight.Text);

            //double proDay = txtProDays.Text == "" ? 0 : Convert.ToDouble(txtProDays.Text);
            //double proHours = txtProHours.Text == "" ? 0 : Convert.ToDouble(txtProHours.Text);
            //double hoursPerDay = txtHoursPerDay.Text == "" ? 0 : Convert.ToDouble(txtHoursPerDay.Text);

            //double totalHours = proDay * hoursPerDay + proHours;

            //double cycleTime = txtCycleTime.Text == "" ? 0 : Convert.ToInt32(txtCycleTime.Text);

            //double totalShot = 0;

            //if (cycleTime != 0)
            //{
            //    totalShot = Math.Floor(Convert.ToDouble(totalHours) / Convert.ToDouble(cycleTime));
            //}

            //txtMatBagQty.Text = (totalShot * (partWeightPerShot + runnerWeightPerShot) / 1000 / matBagKG).ToString("0.##");
        }

        private void dgvForecast_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            groupBox1.Focus();
            bool check = Convert.ToBoolean(dgvForecast.Rows[rowIndex].Cells[headerCheck].Value);

            if (dgvForecast.Columns[colIndex].Name == headerCheck)
            {
                getPurpose();
                if (check)
                {
                    cbOtherPurpose.Checked = false;

                }


            }

        }

        private void getPurpose()
        {
            purpose = "";
            DataTable dt = (DataTable)dgvForecast.DataSource;
            foreach (DataRow row in dt.Rows)
            {

                if (Convert.ToBoolean(row[headerCheck]))
                {
                    string text = row[headerDescription].ToString();

                    if(text.Equals("READY STOCK"))
                    {
                        text = "STOCK";
                    }

                    if (purpose.Equals(""))
                    {
                        purpose = text;
                    }
                    else
                    {
                        purpose += "/ " + text;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOtherPurpose.Checked)
            {
                string text = txtOtherPurpose.Text;

                if (string.IsNullOrEmpty(text) || text.Equals("Fill in planning purpose here"))
                {
                    cbOtherPurpose.BackColor = Color.Red;
                }

                txtOtherPurpose.BackColor = SystemColors.Info;
                dgvForecast.ClearSelection();
                purpose = OtherPurpose;
                txtOtherPurpose.Enabled = true;
                DataTable dt = (DataTable)dgvForecast.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    row[headerCheck] = false;
                }
            }
            else
            {
                txtOtherPurpose.BackColor = Color.White;
                cbOtherPurpose.BackColor = Color.White;

                bool ableToUnCheck = false;
                
                foreach (DataGridViewRow rows in dgvForecast.Rows)
                {
                    if (Convert.ToBoolean(rows.Cells[headerCheck].Value))
                    {
                        ableToUnCheck = true;
                    }
                }

                if (ableToUnCheck)
                {
                    txtOtherPurpose.Enabled = false;
                }
                else
                {
                    cbOtherPurpose.Checked = true;
                    txtOtherPurpose.Enabled = true;
                    //MessageBox
                }
            }
        }

        private void dgvForecast_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvForecast;
            //dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerBalance)
            {
                if (dgv.Rows[row].Cells[col].Value != null)
                {
                    if (!string.IsNullOrEmpty(dgv.Rows[row].Cells[col].Value.ToString()))
                    {
                        float num = dgv.Rows[row].Cells[col].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[col].Value.ToString());
                        if (num < 0)
                        {
                            dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private int GetBalanceStock(string itemCode, int Month, int Year)
        {
            DataTable dt_Item = dalItemCust.itemCodeSearch(itemCode);
            int balance = 0, readyStock, forecast = 0, deliveredOut = 0;
            int qtyNeedFromParent = 0;
            readyStock = Convert.ToInt32(dalItem.getStockQty(itemCode));

            int currentMonth = DateTime.Now.Month;

            //get forecast
            if (dt_Item.Rows.Count > 0)
            {
                foreach (DataRow row in dt_Item.Rows)
                {
                    if (Month == currentMonth)
                    {
                        //foreacast 1
                        forecast += Convert.ToInt32(row["forecast_one"]);
                    }
                    else if (Month - currentMonth == 1)
                    {
                        //forecast 2
                        forecast += Convert.ToInt32(row["forecast_two"]);
                    }
                    else if (Month - currentMonth == 2)
                    {
                        //forecast 3
                        forecast += Convert.ToInt32(row["forecast_three"]);
                    }
                    else if (Month - currentMonth == 3)
                    {
                        //forecast 4
                        forecast += Convert.ToInt32(row["forecast_four"]);
                    }
                }
            }

            //calculate out
            string start = tool.GetPMMAStartDate(Month, Year).ToString("yyyy/MM/dd");
            string end = tool.GetPMMAEndDate(Month, Year).ToString("yyyy/MM/dd");

            DataTable dt_Out = dalTrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

            if (dt_Out.Rows.Count > 0)
            {
                foreach (DataRow row in dt_Out.Rows)
                {
                    if (row["trf_result"].ToString().Equals("Passed"))
                    {
                        deliveredOut += Convert.ToInt32(row["trf_hist_qty"]);
                    }
                }
            }

            forecast -= deliveredOut;

            if (forecast <= 0)
            {
                forecast = 0;
            }

            balance = readyStock - forecast;

            DataTable dt_Parent = dalJoin.loadParentList(itemCode);

            if (dt_Parent.Rows.Count > 0)
            {
                string parentCode;
                int temp = 0;
                foreach (DataRow row in dt_Parent.Rows)
                {
                    parentCode = row["join_parent_code"].ToString();
                    temp = GetBalanceStock(parentCode, Month, Year);

                    if (temp < 0)
                    {
                        qtyNeedFromParent += temp;
                        temp = 0;
                    }

                }
            }

            balance += qtyNeedFromParent;
            //DataTable dt3 = daltrfHist.rangeItemToCustomerSearch(cmbCust.Text, start, end, itemCode);
            return balance;
        }

        private void dgvForecast_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvForecast;
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerCheck && row != -1) 
            {
                dgvForecast.Rows[row].Cells[headerCheck].Value = true;
            }
        }

        private void dgvForecast_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridView dgv = dgvForecast;
            //int row = e.RowIndex;
            //int col = e.ColumnIndex;

            //if (dgv.Columns[col].Name == headerCheck && row != -1)
            //{
            //    dgvForecast.Rows[row].Cells[headerCheck].Value = true;
            //    dgvForecast.EndEdit();
            //    //for (int i = 0; i <= dgvForecast.Rows.Count - 1; i++)
            //    //{
            //    //    if (i != row)
            //    //    {
            //    //        dgvForecast.Rows[i].Cells[headerCheck].Value = false;
            //    //    }
            //    //}
            //}
        }

        public void StartForm()
        {
            try
            {
                Application.Run(new frmLoading());
            }
            catch (ThreadAbortException)
            {
                
            }

           
        }

        private void btnMaterialCheck_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            errorProvider3.Clear();
            if(Validation())
            {
                frmTargetQtyConfirm frm = new frmTargetQtyConfirm(txtAbleToProduceQty.Text, txtTargetQty.Text);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmTargetQtyConfirm.confirm)
                {
                    materialChecked = true;
                    ableToCalculateQty = false;

                    txtTargetQty.Text = frmTargetQtyConfirm.targetQty;

                    ableToCalculateQty = true;

                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    LoadMatCheckList();
                    
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void LoadMatCheckList()
        {
            DataTable dt_MAT = NewMatCheckTable();
            DataTable dt = dalmatPlan.Select();
            DataRow row_dtMat;

            int index = 1;
            float currentStock, planningUsed, availableQty,totalMaterial = 0, qtyShort = 0;
            string rawMaterialCode = cmbMatCode.Text;
            string colorMaterialCode = cmbColorMatCode.Text;
            string itemCode = cmbPartCode.Text;

            //add raw material to list
            if (!string.IsNullOrEmpty(rawMaterialCode))
            {
                currentStock = dalItem.getStockQty(rawMaterialCode);


                planningUsed = tool.matPlanGetQty(dt, rawMaterialCode);
                availableQty = currentStock - planningUsed;

                if (txtMatBagQty.Text != "" && txtMatBagKG.Text != "")
                {
                    totalMaterial = Convert.ToSingle(txtMatBagQty.Text) * Convert.ToSingle(txtMatBagKG.Text);
                }

                qtyShort = availableQty - totalMaterial;

                if(qtyShort > 0)
                {
                    qtyShort = 0;
                }
                row_dtMat = dt_MAT.NewRow();

                row_dtMat[headerIndex] = index;

                row_dtMat[headerType] = dalItem.getCatName(rawMaterialCode);
                row_dtMat[headerCode] = rawMaterialCode;
                row_dtMat[headerName] = dalItem.getItemName(rawMaterialCode);

                row_dtMat[headerStock] = currentStock;
                row_dtMat[headerPlanningUsed] = planningUsed;
                row_dtMat[headerAvaiableQty] = availableQty;

                row_dtMat[headerQtyNeedForThisPlanning] = totalMaterial;
                row_dtMat[headerShort] = qtyShort;


                dt_MAT.Rows.Add(row_dtMat);
                index++;

                //if (dt_MAT.Rows.Count > 0)
                //{
                //    dgvCheckList.DataSource = dt_MAT;
                //    dgvCheckListUIEdit(dgvCheckList);
                //    dgvCheckList.ClearSelection();
                //}
            }

            //add color material to list
            if (!string.IsNullOrEmpty(colorMaterialCode))
            {
                currentStock = dalItem.getStockQty(colorMaterialCode);

                planningUsed = tool.matPlanGetQty(dt, colorMaterialCode);

                availableQty = currentStock - planningUsed;


                if (txtColorMatPlannedQty.Text != "")
                {
                    totalMaterial = Convert.ToSingle(txtColorMatPlannedQty.Text);
                }

                qtyShort = availableQty - totalMaterial;

                if (qtyShort > 0)
                {
                    qtyShort = 0;
                }
                row_dtMat = dt_MAT.NewRow();

                row_dtMat[headerIndex] = index;

                row_dtMat[headerType] = dalItem.getCatName(colorMaterialCode);
                row_dtMat[headerCode] = colorMaterialCode;
                row_dtMat[headerName] = dalItem.getItemName(colorMaterialCode);

                row_dtMat[headerStock] = currentStock;
                row_dtMat[headerPlanningUsed] = planningUsed;
                row_dtMat[headerAvaiableQty] = availableQty;

                row_dtMat[headerQtyNeedForThisPlanning] = totalMaterial;
                row_dtMat[headerShort] = qtyShort;


                dt_MAT.Rows.Add(row_dtMat);
                index++;
            }

            //add child material
            if (!string.IsNullOrEmpty(itemCode))
            {
                //check if got child
                if (tool.ifGotChild(itemCode))
                {
                    //loop child list
                    DataTable dtJoin = dalJoin.loadChildList(itemCode);
                    string childCode;
                    float targetQty = txtAbleToProduceQty.Text.Equals("")? 0 : Convert.ToSingle(txtAbleToProduceQty.Text);
                    float joinQty = 0;
                    
                    foreach(DataRow row in dtJoin.Rows)
                    {
                        childCode = row[dalJoin.JoinChild].ToString();
                        joinQty = row[dalJoin.JoinQty] == DBNull.Value? 0 : Convert.ToSingle(row[dalJoin.JoinQty]);

                        currentStock = dalItem.getStockQty(childCode);
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        //waiting to get planning used qty from database
                        planningUsed = tool.matPlanGetQty(dt, childCode);
                        availableQty = currentStock - planningUsed;

                        //calculate total material need
                        totalMaterial = joinQty * targetQty;

                        qtyShort = availableQty - totalMaterial;

                        if (qtyShort > 0)
                        {
                            qtyShort = 0;
                        }
                        row_dtMat = dt_MAT.NewRow();

                        row_dtMat[headerIndex] = index;

                        row_dtMat[headerType] = dalItem.getCatName(childCode);
                        row_dtMat[headerCode] = childCode;
                        row_dtMat[headerName] = dalItem.getItemName(childCode);

                        row_dtMat[headerStock] = currentStock;
                        row_dtMat[headerPlanningUsed] = planningUsed;
                        row_dtMat[headerAvaiableQty] = availableQty;

                        row_dtMat[headerQtyNeedForThisPlanning] = totalMaterial;
                        row_dtMat[headerShort] = qtyShort;


                        dt_MAT.Rows.Add(row_dtMat);
                        index++;
                    }
                   
                }
            }
            
            //add datatable to datagridview if got data
            if (dt_MAT.Rows.Count > 0)
            {
                btnAdd.Visible = true;
                dgvCheckList.DataSource = dt_MAT;
                dgvCheckListUIEdit(dgvCheckList);
                dgvCheckList.ClearSelection();
            }
            else
            {
                MessageBox.Show("No data for material check list.\n Please check the material for this planning or continue process to next step.");
            }

        }

        private void dgvCheckList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            DataGridView dgv = dgvCheckList;

            if (dgvCheckList.Columns[col].Name == headerShort)
            {
                if (dgv.Rows[row].Cells[col].Value != null)
                {
                    if (!string.IsNullOrEmpty(dgv.Rows[row].Cells[col].Value.ToString()))
                    {
                        float num = dgv.Rows[row].Cells[col].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[col].Value.ToString());
                        if (num < 0)
                        {
                            dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbPartName.Text))
            {
                result = false;
                errorProvider1.SetError(lblPartName, "Part Name Required");
            }

            if (string.IsNullOrEmpty(cmbPartCode.Text))
            {
                result = false;
                errorProvider2.SetError(lblPartCode, "Part Code Required");
            }

            //double d = txtMatBagQty.Text.Equals("") ? 0 : Convert.ToDouble(txtMatBagQty.Text);
            //if (!(Math.Abs(d % 1) <= (double.Epsilon * 100)))
            //{
            //    errorProvider4.Clear();
            //    result = false;
            //    errorProvider4.SetError(txtMatBagQty, "Decimal Point Exist");
            //}

            return result;

        }

        private void txtOtherPurpose_Enter(object sender, EventArgs e)
        {
            if (txtOtherPurpose.Text == "Fill in planning purpose here")
            {
                txtOtherPurpose.Text = "";
                txtOtherPurpose.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtOtherPurpose_Leave(object sender, EventArgs e)
        {
            if (txtOtherPurpose.Text.Length == 0)
            {
                txtOtherPurpose.Text = "Fill in planning purpose here";
                txtOtherPurpose.ForeColor = SystemColors.GrayText;
            }
        }

        private void getPlanningData()
        {
            uPlanning.plan_id = -1;
            uPlanning.part_name = cmbPartName.Text;
            uPlanning.part_code = cmbPartCode.Text;

            uPlanning.plan_cavity = txtCavity.Text;
            if (string.IsNullOrEmpty(txtCavity.Text))
            {
                uPlanning.plan_cavity = "0";
            }

            uPlanning.quo_ton = txtQuoTon.Text;
            if (string.IsNullOrEmpty(txtQuoTon.Text))
            {
                uPlanning.quo_ton = "0";
            }

            uPlanning.plan_ct = txtProCT.Text;
            if(string.IsNullOrEmpty(txtProCT.Text))
            {
                uPlanning.plan_ct = "0";
            }

            uPlanning.plan_pw = txtPartWeight.Text;
            if (string.IsNullOrEmpty(txtPartWeight.Text))
            {
                uPlanning.plan_pw = "0";
            }

            uPlanning.plan_rw = txtRunnerWeight.Text;
            if (string.IsNullOrEmpty(txtRunnerWeight.Text))
            {
                uPlanning.plan_rw = "0";
            }

            uPlanning.production_purpose = purpose;

            uPlanning.material_code = cmbMatCode.Text;
            uPlanning.material_bag_kg = txtMatBagKG.Text;
            uPlanning.material_bag_qty = txtMatBagQty.Text;
            uPlanning.material_recycle_use = txtRecycleKG.Text;

            uPlanning.part_color = txtColorName.Text;
            uPlanning.color_material_code = cmbColorMatCode.Text;
            uPlanning.color_material_usage = txtColorUsage.Text;
            uPlanning.color_material_qty = txtColorMatPlannedQty.Text;

            uPlanning.production_target_qty = txtTargetQty.Text;
            uPlanning.production_able_produce_qty = txtAbleToProduceQty.Text;

            float day = txtProDays.Text == ""? 0: Convert.ToSingle(txtProDays.Text);
            float hourPerDay = txtHoursPerDay.Text == "" ? 0 : Convert.ToSingle(txtHoursPerDay.Text);
            float hour = txtProHours.Text == "" ? 0 : Convert.ToSingle(txtProHours.Text);

            uPlanning.production_day = day.ToString();
            uPlanning.production_hour_per_day = hourPerDay.ToString();
            uPlanning.production_hour = hour.ToString();
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (Validation())
            {
                if(materialChecked)
                {
                    getPlanningData();
                    DataTable dt_mat = (DataTable)dgvCheckList.DataSource;

                    frmPlanningApply frm = new frmPlanningApply(uPlanning, dt_mat);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                    if(frmPlanningApply.dataSaved)
                    {
                        Cursor = Cursors.Arrow; // change cursor to normal type

                        //Hide();
                        //frmCustomMessageBox frm2 = new frmCustomMessageBox();
                        //frm2.StartPosition = FormStartPosition.CenterScreen;
                        //frm2.Closed += (s, args) => Close();
                        //frm2.Show();
                        Close();
                    }
                }
                else
                {
                    errorProvider3.SetError(btnMaterialCheck, "Material Stock Check Required");

                }
                
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void dgvCheckList_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvCheckList.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvCheckList.ClearSelection();
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
        }

        private void dgvForecast_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvForecast.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvForecast.ClearSelection();
                
            }

        }

        private void frmPlanning_Click(object sender, EventArgs e)
        {
            dgvCheckList.ClearSelection();
            dgvForecast.ClearSelection();
            btnDelete.Visible = false;
            btnEdit.Visible = false;
        }

        private void dgvCheckList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

            if(row != -1)
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
        }

        private void dgvCheckList_DataSourceChanged(object sender, EventArgs e)
        {
            btnDelete.Visible = false;
            btnEdit.Visible = false;

            if(dgvCheckList.DataSource == null)
            {
                btnAdd.Visible = false;
            }
        }

        private void txtCavity_TextChanged(object sender, EventArgs e)
        {
            string cavity = txtCavity.Text;

            if(string.IsNullOrEmpty(cavity) || cavity.Equals("0"))
            {
                lblCavity.BackColor = Color.Red;
            }
            else
            {
                lblCavity.BackColor = Color.White;                
            }

            CalColorMatAndTotalRawMatBeforeWastage();
            CalTotalRecycleMat();
            CalTotalMatAfterWastage();
            partInfoEdited = true;
        }

        private void txtCycleTime_TextChanged(object sender, EventArgs e)
        {
            string cycleTime = txtProCT.Text;

            if (string.IsNullOrEmpty(cycleTime) || cycleTime.Equals("0"))
            {
                lblProCT.BackColor = Color.Red;
            }
            else
            {
                lblProCT.BackColor = Color.White;
                
            }

            CalColorMatAndTotalRawMatBeforeWastage();
            CalTotalRecycleMat();
            CalTotalMatAfterWastage();

            CalculateProductionDaysAndHours();
            partInfoEdited = true;
        }

        private void txtPartWeight_TextChanged(object sender, EventArgs e)
        {
            string partWeightPerShot = txtPartWeight.Text;

            if (string.IsNullOrEmpty(partWeightPerShot) || partWeightPerShot.Equals("0"))
            {
                lblPW.BackColor = Color.Red;
            }
            else
            {
                lblPW.BackColor = Color.White;
                
            }

            CalColorMatAndTotalRawMatBeforeWastage();
            CalTotalRecycleMat();
            CalTotalMatAfterWastage();
            partInfoEdited = true;
        }

        private void txtRunnerWeight_TextChanged(object sender, EventArgs e)
        {
            string runnerWeightPerShot = txtRunnerWeight.Text;

            if (string.IsNullOrEmpty(runnerWeightPerShot) || runnerWeightPerShot.Equals("0"))
            {
                lblRW.BackColor = Color.Red;
            }
            else
            {
                lblRW.BackColor = Color.White;
                
            }

            CalColorMatAndTotalRawMatBeforeWastage();
            CalTotalRecycleMat();
            CalTotalMatAfterWastage();
            partInfoEdited = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            int rowIndex = dgvCheckList.CurrentCell.RowIndex;
            string itemName= null;
            if(rowIndex != -1)
            {
                int indexNo = (int)dgvCheckList.Rows[rowIndex].Cells[headerIndex].Value;
                DataTable dt = (DataTable)dgvCheckList.DataSource;
                dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    if(row[headerIndex].ToString() == indexNo.ToString())
                    {
                        itemName = row[headerName].ToString();
                        row.Delete();
                    }
                }
                dt.AcceptChanges();
                reIndex(dt);
                MessageBox.Show("Item ("+itemName+") deleted successfully.");
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvCheckList_Sorted(object sender, EventArgs e)
        {
            dgvCheckList.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            frmPlanningAddSubItem frm = new frmPlanningAddSubItem(txtAbleToProduceQty.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(frmPlanningAddSubItem.dataSaved)
            {
                DataTable dt_MAT = (DataTable)dgvCheckList.DataSource;

                DataRow row_dtMat;

                dt_MAT.AcceptChanges();

                
                row_dtMat = dt_MAT.NewRow();

                row_dtMat[headerIndex] = 100;

                row_dtMat[headerType] = frmPlanningAddSubItem.itemCat;
                row_dtMat[headerCode] = frmPlanningAddSubItem.itemCode;
                row_dtMat[headerName] = frmPlanningAddSubItem.itemName;

                row_dtMat[headerStock] = frmPlanningAddSubItem.itemStock;
                row_dtMat[headerPlanningUsed] = frmPlanningAddSubItem.itemPlannedUse;
                row_dtMat[headerAvaiableQty] = frmPlanningAddSubItem.itemAvailableQty;

                row_dtMat[headerQtyNeedForThisPlanning] = frmPlanningAddSubItem.itemRequiredQty;
                row_dtMat[headerShort] = frmPlanningAddSubItem.itemShort;


                dt_MAT.Rows.Add(row_dtMat);

                dt_MAT.AcceptChanges();

                reIndex(dt_MAT);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            

            int rowIndex = dgvCheckList.CurrentCell.RowIndex;

            if (rowIndex != -1)
            {
                int indexNo = (int)dgvCheckList.Rows[rowIndex].Cells[headerIndex].Value;
                string itemCat = (string)dgvCheckList.Rows[rowIndex].Cells[headerType].Value;
                string itemCode = (string)dgvCheckList.Rows[rowIndex].Cells[headerCode].Value;
                string itemName = (string)dgvCheckList.Rows[rowIndex].Cells[headerName].Value;
                string requiredQty = dgvCheckList.Rows[rowIndex].Cells[headerQtyNeedForThisPlanning].Value.ToString();

                frmPlanningAddSubItem frm = new frmPlanningAddSubItem(itemCat, itemCode, itemName, requiredQty);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmPlanningAddSubItem.dataSaved)
                {
                    DataTable dt = (DataTable)dgvCheckList.DataSource;
                    dt.AcceptChanges();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[headerIndex].ToString() == indexNo.ToString())
                        {
                            row[headerQtyNeedForThisPlanning] = frmPlanningAddSubItem.itemRequiredQty;
                            row[headerShort] = frmPlanningAddSubItem.itemShort;
                        }
                    }
                    dt.AcceptChanges();
                    reIndex(dt);
                }

            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtOtherPurpose_TextChanged(object sender, EventArgs e)
        {
            string text = txtOtherPurpose.Text;

            if(!string.IsNullOrEmpty(text) && !text.Equals("Fill in planning purpose here"))
            {
                purpose = text;
                cbOtherPurpose.BackColor = Color.White;

            }
            else
            {
                purpose = OtherPurpose;

                if(cbOtherPurpose.Checked)
                cbOtherPurpose.BackColor = Color.Red;
            }
        }

        private void txtColorMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtColorMatPlannedQty_TextChanged(object sender, EventArgs e)
        {
            //string colorMat = txtColorMatPlannedQty.Text;
            //txtColorMat.Text = colorMat;
        }

        private void txtColorMat_TextChanged(object sender, EventArgs e)
        {
            //float txttotalMat = txtTotalRawMatKG.Text.Equals("") ? 0 : Convert.ToSingle(txtTotalRawMatKG.Text);
            //float txtcolorMat = txtColorMat.Text.Equals("") ? 0 : Convert.ToSingle(txtColorMat.Text);

            //txtTotalMat.Text = (txttotalMat + txtcolorMat).ToString();
        }

        private void calculateAbleToProduce()
        {
            float totalMaterial = txtTotalMat.Text.Equals("") ? 0 : Convert.ToSingle(txtTotalMat.Text);
            float partWeightPerShot = txtPartWeight.Text == "" || txtPartWeight.Text == "0" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" || txtRunnerWeight.Text == "0" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);
            int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);


            int TotalShot = Convert.ToInt32(Math.Floor(totalMaterial * 1000 / (partWeightPerShot + runnerWeightPerShot)));

            txtAbleToProduceQty.Text = (TotalShot * cavity).ToString();
        }

        private void txtTotalMat_TextChanged(object sender, EventArgs e)
        {
            calculateAbleToProduce();

            //calculate total hours need
            CalculateProductionDaysAndHours();
        }

        private void txtQuoTon_TextChanged(object sender, EventArgs e)
        {
            partInfoEdited = true;
        }

        private void cbEditMode_CheckedChanged(object sender, EventArgs e)
        {
            if(cbEditMode.Checked)
            {
                btnPartInfoSave.Visible = true;

                txtQuoTon.BackColor = SystemColors.Info;
                txtProTon.BackColor = SystemColors.Info;
                txtProCT.BackColor = SystemColors.Info;
                txtCavity.BackColor = SystemColors.Info;
                txtPartWeight.BackColor = SystemColors.Info;
                txtRunnerWeight.BackColor = SystemColors.Info;
                txtQuoCT.BackColor = SystemColors.Info;
            }
            else
            {
                btnPartInfoSave.Visible = false;

                txtQuoTon.BackColor = Color.White;
                txtProTon.BackColor = Color.White;
                txtProCT.BackColor = Color.White;
                txtCavity.BackColor = Color.White;
                txtPartWeight.BackColor = Color.White;
                txtRunnerWeight.BackColor = Color.White;
                txtQuoCT.BackColor = Color.White;
            }
        }

        private void txtProTon_TextChanged(object sender, EventArgs e)
        {
            partInfoEdited = true;
        }

        private void btnPartInfoSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to save these data?", "Message",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes && partInfoEdited)
            {
               
                itemBLL uItem = new itemBLL();

                string itemCode = cmbPartCode.Text;

                if(!string.IsNullOrEmpty(itemCode))
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    uItem.item_code = itemCode;
                    uItem.item_quo_ton = string.IsNullOrEmpty(txtQuoTon.Text)? 0 : Convert.ToInt32(txtQuoTon.Text);
                    uItem.item_pro_ton = string.IsNullOrEmpty(txtProTon.Text) ? 0 : Convert.ToInt32(txtProTon.Text);
                    uItem.item_capacity = string.IsNullOrEmpty(txtCavity.Text) ? 0 : Convert.ToInt32(txtCavity.Text);
                    uItem.item_quo_ct = string.IsNullOrEmpty(txtQuoCT.Text) ? 0 : Convert.ToInt32(txtQuoCT.Text);
                    uItem.item_pro_ct_to = string.IsNullOrEmpty(txtProCT.Text) ? 0 : Convert.ToInt32(txtProCT.Text);
                    uItem.item_pro_pw_shot = string.IsNullOrEmpty(txtPartWeight.Text) ? 0 : Convert.ToSingle(txtPartWeight.Text);
                    uItem.item_pro_rw_shot = string.IsNullOrEmpty(txtRunnerWeight.Text) ? 0 : Convert.ToSingle(txtRunnerWeight.Text);
                    uItem.item_updtd_date = DateTime.Now;
                    uItem.item_updtd_by = MainDashboard.USER_ID;

                    if(dalItem.updateAndHistoryRecord(uItem))
                    {
                        MessageBox.Show("Data saved!");
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void txtTotalRawAfterWastage_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtColorName_TextChanged(object sender, EventArgs e)
        {
            btnColorMatUpdate.Visible = true;
        }

        private void txtTotalRawAfterWastage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dgvForecast_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("cell click" + e.RowIndex);
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            groupBox1.Focus();
            bool check = Convert.ToBoolean(dgvForecast.Rows[rowIndex].Cells[headerCheck].Value);

            if (check)
            {
                dgvForecast.Rows[rowIndex].Cells[headerCheck].Value = false;

                bool purposeChecked = false;

                DataTable dt = (DataTable)dgvForecast.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    if(Convert.ToBoolean(row[headerCheck]))
                    {
                        purposeChecked = true;
                    }
                }

                if(!purposeChecked)
                {
                    cbOtherPurpose.Checked = true;
                }
            }
            else
            {
                dgvForecast.Rows[rowIndex].Cells[headerCheck].Value = true;
                cbOtherPurpose.Checked = false;
            }

            getPurpose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRawMatUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update raw material info for this item?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {

                itemBLL uItem = new itemBLL();

                string itemCode = cmbPartCode.Text;
                string rawMat = cmbMatCode.Text;

                if (!string.IsNullOrEmpty(itemCode))
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    uItem.item_code = itemCode;
                    uItem.item_material = rawMat;

                    uItem.item_updtd_date = DateTime.Now;
                    uItem.item_updtd_by = MainDashboard.USER_ID;

                    if (dalItem.rawMatUpdateAndHistoryRecord(uItem))
                    {
                        MessageBox.Show("Data saved!");
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void cmbMatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRawMatUpdate.Visible = true;
        }

        private void cmbColorMatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColorMatUpdate.Visible = true;
        }

        private void btnColorMatUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update color material info for this item?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {

                itemBLL uItem = new itemBLL();

                string itemCode = cmbPartCode.Text;
                string colorMat = cmbColorMatCode.Text;
                string color = txtColorName.Text;
                float colorUsage = float.TryParse(txtColorUsage.Text, out colorUsage)? colorUsage/100 : 0;

                
                if (!string.IsNullOrEmpty(itemCode))
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    uItem.item_code = itemCode;
                    uItem.item_mb = colorMat;
                    uItem.item_mb_rate = Convert.ToSingle(colorUsage);
                    uItem.item_color = color;

                    uItem.item_updtd_date = DateTime.Now;
                    uItem.item_updtd_by = MainDashboard.USER_ID;

                    if (dalItem.colorMatUpdateAndHistoryRecord(uItem))
                    {
                        MessageBox.Show("Data saved!");
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void txtQuoCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbEditMode.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtQuoCT_TextChanged(object sender, EventArgs e)
        {
            partInfoEdited = true;
        }

        private void txtHoursPerDay_Leave(object sender, EventArgs e)
        {
            string habitData = txtHoursPerDay.Text;

            //check old data
            string oldHabitData = "";

            DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

            foreach(DataRow row in dt.Rows)
            {
                oldHabitData = row[dalHabit.HabitData].ToString();
            }

            if(oldHabitData != habitData)
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
                    Thread t = null;
                    bool aborted = false;
                    //if yes then change
                    //loading
                    try
                    {
                        t = new Thread(new ThreadStart(StartForm));
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        
                        //get machine data
                        DataTable dt_plan = dalPlan.Select();
                        int previousMacID = -1, previousFamilyWith = -1;
                        DateTime previousStart = DateTime.Today;
                        DateTime previousEnd = DateTime.Today;

                        foreach(DataRow row in dt_plan.Rows)
                        {
                            string status = row[dalPlan.planStatus].ToString();

                            if(status != text.planning_status_cancelled && status != text.planning_status_completed)
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

                                if(habitData != oldProHourPerDay.ToString())
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

                                                if(totalProHourLeft > 0)
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
                                            if(previousFamilyWith == currentFamilyWith && previousFamilyWith != -1)
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

                                        if(newProHour < 0)
                                        {
                                            newProHour = 0;
                                        }

                                        previousStart = oldStart;
                                        previousFamilyWith = currentFamilyWith;
                                        //change production day & hour data
                                        uPlan.plan_id = Convert.ToInt32(row[dalPlan.planID]);
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

                                        if(newProHour > 0)
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
                                        else if(previousMacID == currentMacID)
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
                                            if(previousEnd < newEnd)
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
                                        uPlan.plan_id = Convert.ToInt32(row[dalPlan.planID]);
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
                    catch (ThreadAbortException)
                    {
                        // ignore it
                        aborted = true;
                    }
                    catch (Exception ex)
                    {
                        tool.saveToTextAndMessageToUser(ex);
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow; // change cursor to normal type
                        //finish changed
                        if (!aborted)
                            t.Abort();
                    }
                   

                }
            }
        }
    }
}
