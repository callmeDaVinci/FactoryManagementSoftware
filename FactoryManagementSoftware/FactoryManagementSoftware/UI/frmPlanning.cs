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
        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        matPlanDAL dalmatPlan = new matPlanDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        readonly string headerCheck = "FOR";
        readonly string headerDescription = "DESCRIPTION";
        readonly string headerBalance = "ESTIMATE CLOSING BALANCE";

        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        static public readonly string headerCode = "CODE";
        readonly string headerName = "NAME";
        readonly string headerStock = "CURRENT STOCK";
        readonly string headerPlanningUsed = "PLANNED TO USE";
        readonly string headerAvaiableQty = "AVAIABLE QTY";
        static public  readonly string headerQtyNeedForThisPlanning = "QTY NEED FOR THIS PLANNING";
        readonly string headerShort = "SHORT";
        readonly string OtherPurpose = "FOR OTHER PURPOSE";
        private bool ableToCalculateQty = true;
        private bool ableToLoadData = false;
        private bool materialChecked = false;

        private string purpose = "FOR OTHER PURPOSE";
        private string name = null;
        private string code = null;

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
                DataTable dt = dalItem.catSearch(keywords);
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
                        txtPartWeight.Text = row[dalItem.ItemProPWShot] == DBNull.Value ? "" : row[dalItem.ItemProPWShot].ToString();
                        txtRunnerWeight.Text = row[dalItem.ItemProRWShot] == DBNull.Value ? "" : row[dalItem.ItemProRWShot].ToString();
                        txtCavity.Text = row[dalItem.ItemCapacity] == DBNull.Value ? "" : row[dalItem.ItemCapacity].ToString();
                        txtCycleTime.Text = row[dalItem.ItemProCTTo] == DBNull.Value ? "" : row[dalItem.ItemProCTTo].ToString();

                        if(string.IsNullOrEmpty(txtCycleTime.Text))
                        {
                            txtCycleTime.Text = row[dalItem.ItemQuoCT] == DBNull.Value ? "" : row[dalItem.ItemQuoCT].ToString();
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
            if (dalItem.getCatName(matCode).Equals("Pigment"))
            {
                tool.loadPigmentToComboBox(cmbColorMatCode);
                cmbColorMatCode.Text = matCode;
            }
            else
            {
                tool.loadMasterBatchToComboBox(cmbColorMatCode);
                cmbColorMatCode.Text = matCode;
            }
        }

        private void LoadRawMaterial(string matName)
        {
            tool.loadRAWMaterialToComboBox(cmbMatCode);
            cmbMatCode.Text = matName;
        }

        private void CalculateTotalRawMateral()
        {
            float totalMaterial = 0;

            if (txtMatBagQty.Text != "" && txtMatBagKG.Text != "")
            {
                totalMaterial = Convert.ToSingle(txtMatBagQty.Text) * Convert.ToSingle(txtMatBagKG.Text);
            }

            if (cbRecycleUse.Checked && txtRecycleKG.Text != "")
            {
                totalMaterial += Convert.ToSingle(txtRecycleKG.Text);
            }

            txtTotalRawMatKG.Text = totalMaterial.ToString();

            //label30.Text = Convert.ToInt32(Math.Floor(totalMaterial)).ToString();
            //calculate total qty able to produce

            //float partWeightPerShot = txtPartWeight.Text == "" || txtPartWeight.Text == "0" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            //float runnerWeightPerShot = txtRunnerWeight.Text == "" || txtRunnerWeight.Text == "0" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);
            //int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);


            //int TotalShot = Convert.ToInt32(Math.Floor(totalMaterial * 1000 / (partWeightPerShot + runnerWeightPerShot)));

            //txtAbleToProduceQty.Text = (TotalShot * cavity).ToString();

            //calculate total hours need
            CalculateProductionDaysAndHours();

        }

        private void CalculateProductionDaysAndHours()
        {
            CalculateTotalRecycleMaterial();
            float totalMaterial = txtTotalMat.Text == "" ? 0 : Convert.ToSingle(txtTotalMat.Text);
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

            int TotalShot = Convert.ToInt32(Math.Floor(totalMaterial * 1000 / (partWeightPerShot + runnerWeightPerShot)));

            txtAbleToProduceQty.Text = (TotalShot * cavity).ToString();

            //calculate total hours need
            int cycleTime = txtCycleTime.Text == "" ? 0 : Convert.ToInt32(txtCycleTime.Text);
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

        private void CalculateTotalRecycleMaterial()
        {
            //recycle material = runner weight per shot * roundup( targetQTY/cavity ) * ( 1 - wastage )
            float totalMaterial = 0;

            if (txtMatBagQty.Text != "" && txtMatBagKG.Text != "")
            {
                totalMaterial = Convert.ToSingle(txtMatBagQty.Text) * Convert.ToSingle(txtMatBagKG.Text) * 1000;
            }

            float partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);

            float totalWeightPerShot = partWeightPerShot + runnerWeightPerShot;

            if (totalWeightPerShot == 0)
            {
                totalWeightPerShot = 1;
            }

            int totalShot = Convert.ToInt32(Math.Floor(totalMaterial / totalWeightPerShot));
            totalMaterial = Convert.ToSingle(totalShot) * runnerWeightPerShot;

            float wastage = txtRecycleWastage.Text == "" ? 0 : Convert.ToSingle(txtRecycleWastage.Text);

            totalMaterial = totalMaterial * (1 - wastage / 100) / 1000;
            //totalMaterial = Convert.ToSingle(Math.Floor((totalMaterial * (1 - wastage / 100) / 1000)));

            txtRecycleKG.Text = totalMaterial.ToString("0.###");

        }

        private void CalculateTotalColorMaterial()
        {
            materialChecked = false;
            float totalMaterial = 0;

            if (txtMatBagKG.Text != "" && txtMatBagQty.Text != "" && txtColorUsage.Text != "")
            {
                totalMaterial = Convert.ToSingle(txtMatBagKG.Text) * Convert.ToSingle(txtMatBagQty.Text) * Convert.ToSingle(txtColorUsage.Text) / 100;
            }

            txtColorMatPlannedQty.Text = (totalMaterial).ToString("0.##");
        }

        private void AddDataToForecastTable()
        {
            DataTable dt_Forecast = NewForecastTable();
            DataRow row_dtForecast;
            int month, year;
            string itemCode = cmbPartCode.Text;

            if (!string.IsNullOrEmpty(itemCode))
            {
                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = "READY STOCK";
                row_dtForecast[headerBalance] = dalItem.getStockQty(itemCode);

                dt_Forecast.Rows.Add(row_dtForecast);

                month = DateTime.Now.Month;
                year = DateTime.Now.Year;

                string monthName;
                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);

                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = monthName+" "+year;
                row_dtForecast[headerBalance] = tool.GetBalanceStock(itemCode, month, year);

                dt_Forecast.Rows.Add(row_dtForecast);

                if (month != 12)
                {
                    month++;
                }
                else
                {
                    month = 1;
                }

                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = monthName + " " + year;
                row_dtForecast[headerBalance] = tool.GetBalanceStock(itemCode, month, year);

                dt_Forecast.Rows.Add(row_dtForecast);

                if (month != 12)
                {
                    month++;
                }
                else
                {
                    month = 1;
                }

                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                row_dtForecast = dt_Forecast.NewRow();
                row_dtForecast[headerCheck] = false;
                row_dtForecast[headerDescription] = monthName + " " + year;
                row_dtForecast[headerBalance] = tool.GetBalanceStock(itemCode, month, year);

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
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
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
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
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
                e.Handled = true;
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
            e.Handled = true;
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
            CalculateTotalRecycleMaterial();
            ActiveControl = cmbPartName;

            if(name != null)
            {
                cmbPartName.Text = name;
                cmbPartCode.Text = code;
            }
        }

        private void cmbPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string keywords = cmbPartName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                ableToLoadData = false;
                DataTable dt = dalItem.Search(keywords);
                cmbPartCode.DataSource = dt;
                cmbPartCode.DisplayMember = "item_code";
                cmbPartCode.ValueMember = "item_code";
                cmbPartCode.SelectedIndex = -1;
                ableToLoadData = true;
            }
            else
            {
                cmbPartCode.DataSource = null;
            }
        }

        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            materialChecked = false;
            errorProvider2.Clear();
            Thread t = null;
            if (ableToLoadData)
            {
                try
                {
                    dgvForecast.DataSource = null;
                    cmbMatCode.SelectedIndex = -1;
                    txtTargetQty.Clear();
                    txtMatBagQty.Clear();
                    cmbColorMatCode.SelectedIndex = -1;

                    t = new Thread(new ThreadStart(StartForm));
                    if (dalItem.checkIfAssembly(cmbPartCode.Text) && !dalItem.checkIfProduction(cmbPartCode.Text) && tool.ifGotChild(cmbPartCode.Text))
                    {
                        MessageBox.Show(cmbPartCode.Text + " " + cmbPartName.Text + " is a assembly part!");
                    }
                    else
                    {
                        LoadPartInfo();
                        t.Start();
                        CalculateTotalRawMateral();
                    }
                    AddDataToForecastTable();
                }
                catch (ThreadAbortException)
                {
                    // ignore it
                    Thread.ResetAbort();
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    t.Abort();
                }
            }
            
        }

        private void cbRecycleUse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRecycleUse.Checked)
            {
                txtRecycleWastage.Enabled = true;
            }
            else
            {
                txtRecycleWastage.Enabled = false;
            }

            CalculateTotalRawMateral();
        }

        private void txtMatBagQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();

            double d = txtMatBagQty.Text.Equals("") ? 0 : Convert.ToDouble(txtMatBagQty.Text);

            if( !(Math.Abs(d % 1) <= (double.Epsilon * 100)))
            {
                errorProvider4.SetError(txtMatBagQty, "Decimal Point Exist");
            }
        }

        private void txtMatBagKG_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();
        }

        private void txtRecycleKG_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRawMateral();
        }

        private void txtRecycleWastage_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();
        }

        private void txtTotalMatKG_TextChanged(object sender, EventArgs e)
        {
            materialChecked = false;
            CalculateTotalColorMaterial();
            dgvCheckList.DataSource = null;

            float txttotalMat = txtTotalRawMatKG.Text.Equals("") ? 0 : Convert.ToSingle(txtTotalRawMatKG.Text);
            float txtcolorMat = txtColorMat.Text.Equals("")? 0 : Convert.ToSingle(txtColorMat.Text);

            txtTotalMat.Text = (txttotalMat + txtcolorMat).ToString();
        }

        private void txtColorUsage_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalColorMaterial();
        }

        private void txtHoursPerDay_TextChanged(object sender, EventArgs e)
        {
            CalculateProductionDaysAndHours();
            
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtTargetQty_TextChanged(object sender, EventArgs e)
        {
            materialChecked = false;
            if (ableToCalculateQty)
            {
                int targetQty = txtTargetQty.Text == "" ? 0 : Convert.ToInt32(txtTargetQty.Text);
                int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);
                double totalShot = 0;

                double matBagKG = txtMatBagKG.Text == "" ? 0 : Convert.ToDouble(txtMatBagKG.Text);
                double partWeightPerShot = txtPartWeight.Text == "" ? 1 : Convert.ToDouble(txtPartWeight.Text);
                double runnerWeightPerShot = txtRunnerWeight.Text == "" ? 1 : Convert.ToDouble(txtRunnerWeight.Text);

                if (cavity != 0)
                {
                    totalShot = Math.Ceiling(Convert.ToDouble(targetQty) / Convert.ToDouble(cavity));
                }

                txtMatBagQty.Text = (totalShot * (partWeightPerShot + runnerWeightPerShot) / 1000 / matBagKG).ToString("0.##");
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
                    //MessageBox.Show("check at " + row);
                    cbOtherPurpose.Checked = false;


                    //if (purpose == OtherPurpose || string.IsNullOrEmpty(purpose))
                    //{
                    //    purpose = text;
                    //}
                    //else
                    //{
                    //    purpose += "/ "+text;
                    //}
                    
                    //purpose = "";
                    //DataTable dt = (DataTable)dgvForecast.DataSource;
                    //foreach (DataRow row in dt.Rows)
                    //{
                        
                    //    if(Convert.ToBoolean(row[headerCheck]))
                    //    {
                    //        string text = dgvForecast.Rows[rowIndex].Cells[headerDescription].Value.ToString();

                    //        if(purpose.Equals(""))
                    //        {
                    //            purpose = text;
                    //        }
                    //        else
                    //        {
                    //            purpose += "/ " + text;
                    //        }
                    //    }
                    //}

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
            string start = tool.GetStartDate(Month, Year).ToString("yyyy/MM/dd");
            string end = tool.GetEndDate(Month, Year).ToString("yyyy/MM/dd");

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
                // ignore it
                Thread.ResetAbort();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    btnAdd.Visible = true;
                }
            }
           
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
                dgvCheckList.DataSource = dt_MAT;
                dgvCheckListUIEdit(dgvCheckList);
                dgvCheckList.ClearSelection();
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
                errorProvider1.SetError(cmbPartName, "Part Name Required");
            }

            if (string.IsNullOrEmpty(cmbPartCode.Text))
            {
                result = false;
                errorProvider2.SetError(cmbPartCode, "Part Code Required");
            }

            double d = txtMatBagQty.Text.Equals("") ? 0 : Convert.ToDouble(txtMatBagQty.Text);
            if (!(Math.Abs(d % 1) <= (double.Epsilon * 100)))
            {
                errorProvider4.Clear();
                result = false;
                errorProvider4.SetError(txtMatBagQty, "Decimal Point Exist");
            }

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
            uPlanning.part_name = cmbPartName.Text;
            uPlanning.part_code = cmbPartCode.Text;
            uPlanning.quo_ton = txtQuoTon.Text;
            uPlanning.cycle_time = txtCycleTime.Text;
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
                        Close();
                    }
                }
                else
                {
                    errorProvider3.SetError(btnMaterialCheck, "Material Stock Check Required");

                }
                
            }
                
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
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();
        }

        private void txtCycleTime_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();
        }

        private void txtPartWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();
        }

        private void txtRunnerWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRawMateral();
            CalculateTotalRecycleMaterial();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
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
            

        }

        private void dgvCheckList_Sorted(object sender, EventArgs e)
        {
            dgvCheckList.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
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

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtOtherPurpose_TextChanged(object sender, EventArgs e)
        {
            string text = txtOtherPurpose.Text;

            if(!string.IsNullOrEmpty(text))
            {
                purpose = text;
            }
            else
            {
                purpose = OtherPurpose;
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
            string colorMat = txtColorMatPlannedQty.Text;
            txtColorMat.Text = colorMat;
        }

        private void txtColorMat_TextChanged(object sender, EventArgs e)
        {
            float txttotalMat = txtTotalRawMatKG.Text.Equals("") ? 0 : Convert.ToSingle(txtTotalRawMatKG.Text);
            float txtcolorMat = txtColorMat.Text.Equals("") ? 0 : Convert.ToSingle(txtColorMat.Text);

            txtTotalMat.Text = (txttotalMat + txtcolorMat).ToString();
        }

        private void txtTotalMat_TextChanged(object sender, EventArgs e)
        {
            float totalMaterial = txtTotalMat.Text.Equals("") ? 0 : Convert.ToSingle(txtTotalMat.Text);
            float partWeightPerShot = txtPartWeight.Text == "" || txtPartWeight.Text == "0" ? 1 : Convert.ToSingle(txtPartWeight.Text);
            float runnerWeightPerShot = txtRunnerWeight.Text == "" || txtRunnerWeight.Text == "0" ? 1 : Convert.ToSingle(txtRunnerWeight.Text);
            int cavity = txtCavity.Text == "" ? 0 : Convert.ToInt32(txtCavity.Text);


            int TotalShot = Convert.ToInt32(Math.Floor(totalMaterial * 1000 / (partWeightPerShot + runnerWeightPerShot)));

            txtAbleToProduceQty.Text = (TotalShot * cavity).ToString();
        }
    }
}
