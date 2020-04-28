using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport_NEW : Form
    {
        public frmForecastReport_NEW()
        {
            InitializeComponent();

            //tool.GetCustItemWithForecast(1, 1, 2020, 3);
            tool.DoubleBuffered(dgvForecastReport, true);

            tool.loadCustomerWithoutOtherToComboBox(cmbCustomer);

            InitializeFilterData();
        }

        #region variable declare

        enum ColorSet
        {
            Gold = 0,
            Cyan,
            Aquamarine,
            Orange,
            Yellow,
            //MediumSpringGreen,
            Lavender,
            LightGray,
            LightBlue
            //Bisque,
            //Chartreuse,
            //CornflowerBlue,
            //MediumTurquoise,
            //MediumAquamarine,
            //LightSalmon,
            //DeepSkyBlue,
            //SkyBlue,
            //Tan,
            //Thistle,
        }

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";

        readonly string headerParentColor = "PARENT COLOR";
        readonly string headerBackColor = "BACK COLOR";
        readonly string headerForecastType = "FORECAST TYPE";
        readonly string headerBalType = "BAL TYPE";
        readonly string headerToProduce = "TO PRODUCE";
        readonly string headerProduced = "PRODUCED*";
        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerRawMat = "RAW MATERIAL";
        readonly string headerPartName = "PART NAME";
        readonly string headerPartCode = "PART CODE";
        readonly string headerColorMat = "COLOR MATERIAL";
        readonly string headerPartWeight = "PART WEIGHT/G (RUNNER)";
        readonly string headerPlannedQty = "PLANNED QTY";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerEstimate = "ESTIMATE";
        string headerForecast1 = "FCST/ NEEDED";
        string headerForecast2 = "FCST/ NEEDED";
        string headerForecast3 = "FCST/ NEEDED";
        readonly string headerOut = "OUT";
        readonly string headerOutStd = "OUTSTD";
        string headerBal1 = "BAL";
        string headerBal2 = "BAL";

        readonly Color AssemblyColor = Color.Blue;
        readonly Color ProductionColor = Color.Green;
        readonly Color ProductionAndAssemblyColor = Color.Purple;
        readonly Color InspectionColor = Color.Peru;

        readonly string AssemblyMarking = "Blue";
        readonly string InspectionMarking = "Peru";
        readonly string ProductionMarking = "Green";
        readonly string ProductionAndAssemblyMarking = "Purple";
        readonly string typeSingle = "SINGLE";
        readonly string typeParent= "PARENT";
        readonly string typeChild = "CHILD";

        readonly string forecastType_Forecast = "FORECAST";
        readonly string forecastType_Needed = "NEEDED";

        readonly string balType_Total = "TOTAL";

        readonly string sortBy_Stock = "Stock (Finished Goods)";
        readonly string sortBy_Estimate = "Estimate";
        string sortBy_Forecast1 = "Forecast/Needed";
        string sortBy_Forecast2 = "Forecast/Needed";
        string sortBy_Forecast3 = "Forecast/Needed";

        readonly string sortBy_Out = "Delivered Out";
        readonly string sortBy_OutStd = "Out Standing";
        string sortBy_Bal1 = "Balance";
        string sortBy_Bal2 = "Balance";


        private bool loaded = false;
        private bool custChanging = false;

        private float alertLevel = 0;

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();
        planningDAL dalPlanning = new planningDAL();
        dataTrfBLL uData = new dataTrfBLL();

        Tool tool = new Tool();

        Text text = new Text();

        DataTable dt_Join = new DataTable();
        DataTable dt_Item = new DataTable();
        DataTable dt_OrginalData = new DataTable();
        DataTable dt_MacSchedule = new DataTable();

        #endregion

        #region UI Design

        private DataTable NewForecastReportTable()
        {
            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";
          
            headerBal1 = "BAL";
            headerBal2 = "BAL";
            DataTable dt = new DataTable();

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerBackColor, typeof(string));
            dt.Columns.Add(headerForecastType, typeof(string));
            dt.Columns.Add(headerBalType, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerIndex, typeof(float));
            dt.Columns.Add(headerRawMat, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerColorMat, typeof(string));
            dt.Columns.Add(headerPartWeight, typeof(string));
            //dt.Columns.Add(headerPlannedQty, typeof(float));
            dt.Columns.Add(headerProduced, typeof(float));
            dt.Columns.Add(headerToProduce, typeof(float));
           
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerEstimate, typeof(float));

            string monthFrom = cmbForecastFrom.Text;

            string monthName = string.IsNullOrEmpty(monthFrom) || cmbForecastFrom.SelectedIndex == -1 ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) : monthFrom;
            int monthINT = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;

            for (int i = 1; i <= 3; i++)
            {
                if(i == 1)
                {
                    headerForecast1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast1;
                    headerBal1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal1;
                }
               
                else if(i == 2)
                {
                    headerForecast2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast2;
                    headerBal2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal2;
                }

                else if (i == 3)
                {
                    headerForecast3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast3;
                }

                monthINT++;

                if(monthINT > 12)
                {
                    monthINT -= 12;

                }

                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            }

            dt.Columns.Add(headerForecast1, typeof(float));
            dt.Columns.Add(headerOut, typeof(float));
            dt.Columns.Add(headerOutStd, typeof(float));
            dt.Columns.Add(headerBal1, typeof(float));
            dt.Columns.Add(headerForecast2, typeof(float));
            dt.Columns.Add(headerBal2, typeof(float));
            dt.Columns.Add(headerForecast3, typeof(float));
            return dt;
        }

        private void DgvForecastReportUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPartWeight].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[headerPlannedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerToProduce].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerEstimate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerForecast1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerOut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerOutStd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBal1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerForecast2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBal2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerForecast3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[headerPartCode].Frozen = true;

            dgv.Columns[headerReadyStock].HeaderCell.Style.BackColor = Color.Pink;
            dgv.Columns[headerReadyStock].DefaultCellStyle.BackColor = Color.Pink;

            dgv.Columns[headerEstimate].HeaderCell.Style.BackColor = Color.LightCyan;
            dgv.Columns[headerEstimate].DefaultCellStyle.BackColor = Color.LightCyan;

            dgv.Columns[headerForecast1].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[headerForecast1].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[headerOut].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[headerOut].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[headerOutStd].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[headerOutStd].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[headerBal1].HeaderCell.Style.BackColor = Color.LightYellow;
            dgv.Columns[headerBal1].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[headerBal1].HeaderCell.Style.ForeColor = Color.Red;

            dgv.Columns[headerForecast2].HeaderCell.Style.BackColor = Color.PeachPuff;
            dgv.Columns[headerForecast2].DefaultCellStyle.BackColor = Color.PeachPuff;

            dgv.Columns[headerBal2].HeaderCell.Style.BackColor = Color.PeachPuff;
            dgv.Columns[headerBal2].DefaultCellStyle.BackColor = Color.PeachPuff;
            dgv.Columns[headerBal2].HeaderCell.Style.ForeColor = Color.Red;

            dgv.Columns[headerBal1].HeaderCell.Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[headerBal1].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[headerBal2].HeaderCell.Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[headerBal2].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[headerProduced].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Strikeout);

            dgv.EnableHeadersVisualStyles = false;
        }

        private void ColorData()
        {
            DataTable dt = (DataTable)dgvForecastReport.DataSource;
            DataGridView dgv = dgvForecastReport;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            Font _ParentFont = new Font(dgvForecastReport.Font, FontStyle.Underline);
            Font _BalFont = new Font(dgvForecastReport.Font, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold);
            Font _NeededFont = new Font(dgvForecastReport.Font, FontStyle.Italic);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parentColor = dt.Rows[i][headerParentColor].ToString();
                string backColor = dt.Rows[i][headerBackColor].ToString();
                string type = dt.Rows[i][headerType].ToString();
                string balType = dt.Rows[i][headerBalType].ToString();

                //change parent color
                if (parentColor.Equals(AssemblyMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = Color.FromName(parentColor);
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = AssemblyColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }
                else if (parentColor.Equals(ProductionMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = ProductionColor;
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = ProductionColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }
                else if (parentColor.Equals(ProductionAndAssemblyMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = ProductionAndAssemblyColor;
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = ProductionAndAssemblyColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }
                else if (parentColor.Equals(InspectionMarking))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.ForeColor = InspectionColor;
                    dgv.Rows[i].Cells[headerPartName].Style.Font = _ParentFont;

                    dgv.Rows[i].Cells[headerPartCode].Style.ForeColor = InspectionColor;
                    dgv.Rows[i].Cells[headerPartCode].Style.Font = _ParentFont;
                }

                if (!string.IsNullOrEmpty(backColor))
                {
                    dgv.Rows[i].Cells[headerPartName].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerPartCode].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerForecast1].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerForecast2].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerForecast3].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerOut].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerOutStd].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerBal1].Style.BackColor = Color.FromName(backColor);
                    dgv.Rows[i].Cells[headerBal2].Style.BackColor = Color.FromName(backColor);

                }

                if (type.Equals(typeChild))
                {
                    dgv.Rows[i].Cells[headerForecast1].Style.Font = _NeededFont;
                    dgv.Rows[i].Cells[headerForecast2].Style.Font = _NeededFont;
                    dgv.Rows[i].Cells[headerForecast3].Style.Font = _NeededFont;
                }

                if (balType.Equals(balType_Total))
                {
                    dgv.Rows[i].Cells[headerBal1].Style.Font = _BalFont;
                    dgv.Rows[i].Cells[headerBal2].Style.Font = _BalFont;
                }
            }

        }

        private void dgvForecastReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvForecastReport;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerPartCode)
            {
                string partCode = dgv.Rows[row].Cells[headerPartCode].Value.ToString();

                if (string.IsNullOrEmpty(partCode))
                {
                    dgv.Rows[row].Height = 4;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
                }
            }
            else if (dgv.Columns[col].Name == headerBal1)
            {
                float bal = dgv.Rows[row].Cells[headerBal1].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal1].Value);

                if (bal < alertLevel)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
            else if (dgv.Columns[col].Name == headerBal2)
            {
                float bal = dgv.Rows[row].Cells[headerBal2].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal2].Value);

                if (bal < alertLevel)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
        }

        #endregion

        #region UI Function

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();
            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 175f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvForecastReport.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void cmbForecastFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbForecastFrom.SelectedIndex != -1)
            {
                dgvForecastReport.DataSource = null;
                cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).AddMonths(2).Month);
                getStartandEndDate();
                LoadSortByComboBoxData();
            }
        }

        private void lblIncludeSubMat_Click(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            if (cbWithSubMat.Checked)
            {
                cbWithSubMat.Checked = false;
            }
            else
            {
                cbWithSubMat.Checked = true;
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cust = cmbCustomer.Text;
            custChanging = true;
            dgvForecastReport.DataSource = null;
            if (cmbCustomer.SelectedIndex != -1)
            {
                getStartandEndDate();

                if (cust.Equals(tool.getCustName(1)))
                {
                    lblChangeDate.Visible = true;
                }
                else
                {
                    lblChangeDate.Visible = false;
                }
            }
            custChanging = false;

        }

        private void lblChangeDate_Click(object sender, EventArgs e)
        {
            lblChangeDate.ForeColor = Color.Purple;
            frmPMMADateEdit frm = new frmPMMADateEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            lblChangeDate.ForeColor = Color.Blue;

            if (frmPMMADateEdit.dateChanged)
            {
                getStartandEndDate();
            }
        }

        private void cmbForecastTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbForecastFrom.SelectedIndex != -1 && cmbForecastTo.SelectedIndex != -1 && loaded)
            {
                int monthFrom = DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).Month;
                int monthTo = DateTime.ParseExact(cmbForecastTo.Text, "MMMM", CultureInfo.CurrentCulture).Month;

                monthFrom += 2;

                if (monthFrom > 12)
                {
                    monthFrom -= 12;
                }

                if (monthFrom != monthTo)
                {
                    MessageBox.Show("Total forecast months must be exactly 3!\nSystem will correct it after this message.");

                    cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthFrom);
                }
            }
        }

        private void lblCurrentMonth_Click(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
            if (cmbForecastFrom.SelectedIndex != -1)
            {
                cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                LoadForecastData();
            }
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                LoadForecastData();
            }
        }

        private void dtpOutFrom_ValueChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cmbSoryBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void txtAlertLevel_TextChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void dtpOutTo_ValueChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cbWithSubMat_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void cbDescending_CheckedChanged(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            dgvForecastReport.DataSource = null;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                LoadForecastData();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            dgvForecastReport.DataSource = null;

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        #endregion

        #region Loading Data

        private void frmForecastReport_NEW_Load(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();

            tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvForecastReport.ResumeLayout();

            btnFilter.Text = textMoreFilters;

            cmbCustomer.Focus();
            loaded = true;
        }

        private void InitializeFilterData()
        {
            loaded = false;

            cmbForecastFrom.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                     .MonthNames.Take(12).ToList();

            cmbForecastTo.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                    .MonthNames.Take(12).ToList();

            cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(2).Month);

            LoadSortByComboBoxData();

            loaded = true;
        }

        private void LoadSortByComboBoxData()
        {
            ComboBox cmb = cmbSoryBy;
            cmb.DataSource = null;

            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";

            headerBal1 = "BAL";
            headerBal2 = "BAL";

            sortBy_Forecast1 = "Forecast/Needed";
            sortBy_Forecast2 = "Forecast/Needed";
            sortBy_Forecast3 = "Forecast/Needed";

            sortBy_Bal1 = "Balance";
            sortBy_Bal2 = "Balance";

            string monthFrom = cmbForecastFrom.Text;

            string monthName = string.IsNullOrEmpty(monthFrom) || cmbForecastFrom.SelectedIndex == -1 ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) : monthFrom;
            int monthINT = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;

            //for (int i = 1; i <= 3; i++)
            //{
            //    if (i == 1)
            //    {
            //        sortBy_Forecast1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + sortBy_Forecast1;
            //        sortBy_Bal1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + sortBy_Bal1;
            //    }

            //    else if (i == 2)
            //    {
            //        sortBy_Forecast2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + sortBy_Forecast2;
            //        sortBy_Bal2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + sortBy_Bal2;
            //    }

            //    else if (i == 3)
            //    {
            //        sortBy_Forecast3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + sortBy_Forecast3;
            //    }

            //    monthINT++;

            //    if (monthINT > 12)
            //    {
            //        monthINT -= 12;

            //    }

            //    monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            //}

            for (int i = 1; i <= 3; i++)
            {
                if (i == 1)
                {
                    headerForecast1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast1;
                    headerBal1 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal1;
                }

                else if (i == 2)
                {
                    headerForecast2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast2;
                    headerBal2 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal2;
                }

                else if (i == 3)
                {
                    headerForecast3 = tool.GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast3;
                }

                monthINT++;

                if (monthINT > 12)
                {
                    monthINT -= 12;

                }

                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            }

            DataTable dt = new DataTable();
            dt.Columns.Add("SORT BY");

            //dt.Rows.Add(sortBy_Stock);
            //dt.Rows.Add(sortBy_Estimate);
            //dt.Rows.Add("");
            //dt.Rows.Add(sortBy_Forecast1);
            //dt.Rows.Add(sortBy_Forecast2);
            //dt.Rows.Add(sortBy_Forecast3);
            //dt.Rows.Add("");
            //dt.Rows.Add(sortBy_Out);
            //dt.Rows.Add(sortBy_OutStd);
            //dt.Rows.Add("");
            //dt.Rows.Add(sortBy_Bal1);
            //dt.Rows.Add(sortBy_Bal2);

            dt.Rows.Add(headerBal1);
            dt.Rows.Add(headerBal2);
            dt.Rows.Add("");
            dt.Rows.Add(headerForecast1);
            dt.Rows.Add(headerForecast2);
            dt.Rows.Add(headerForecast3);
            dt.Rows.Add("");
            dt.Rows.Add(headerReadyStock);
            dt.Rows.Add(headerEstimate);
            dt.Rows.Add("");
            dt.Rows.Add(headerOut);
            dt.Rows.Add(headerOutStd);

            cmb.DataSource = dt;
            cmb.DisplayMember = "SORT BY";
            cmb.SelectedIndex = -1;
        }

        
        private int CalculateEstimateOrder(DataTable dt_PMMADate, DataTable dt_trfToCustomer, string _ItemCode, string _Customer)
        {
            int estimateOrder = 0;

            int preMonth = 0, preYear = 0, dividedQty = 0;
            double singleOutQty = 0, totalTrfOutQty = 0;

            int monthNow = DateTime.Now.Month;
            int yearNow = DateTime.Now.Year;

            foreach (DataRow row in dt_trfToCustomer.Rows)
            {
                string trfResult = row[dalTrfHist.TrfResult].ToString();
                string itemCode = row[dalTrfHist.TrfItemCode].ToString();

                if (trfResult == "Passed" && _ItemCode == itemCode)
                {
                    double trfQty = double.TryParse(row[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;
                    DateTime trfDate = DateTime.TryParse(row[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                    int month = 0;
                    int year = 0;

                    if (trfDate == DateTime.MaxValue)
                    {
                        continue;
                    }
                    else
                    {
                        if (_Customer == "PMMA")
                        {
                            //day = trfDate.Day;
                            //month = trfDate.Month;
                            //year = trfDate.Year;

                            DateTime pmmaDate = tool.GetPMMAMonthAndYear(trfDate, dt_PMMADate);

                            if (pmmaDate != DateTime.MaxValue)
                            {
                                month = pmmaDate.Month;
                                year = pmmaDate.Year;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            month = trfDate.Month;
                            year = trfDate.Year;
                        }

                        if (month != monthNow || year != yearNow)
                        {
                            totalTrfOutQty += trfQty;

                        }
                        
                        if (preMonth != 0 && preYear != 0 && preMonth == month && preYear == year)
                        {
                            singleOutQty += trfQty;
                        }
                        else
                        {
                            singleOutQty = trfQty;
                            preMonth = month;
                            preYear = year;

                            if (month != monthNow || year != yearNow)
                            {
                                dividedQty++;
                            }
                            
                        }

                    }
                }
            }

            if(totalTrfOutQty == 0 || dividedQty == 0)
            {
                estimateOrder = 0;
            }
            else
            {
                estimateOrder = (int)Math.Round(totalTrfOutQty / dividedQty/100, 0) * 100; // round up to nearest 100
            }

            return estimateOrder;
        }

        private Tuple<int, int> GetProduceQty(string _ItemCode, DataTable dt_MacSechedule)
        {
            int toProduce = 0, produced = 0;

            DateTime start = dtpOutFrom.Value;
            DateTime end = dtpOutTo.Value;

            foreach(DataRow row in dt_MacSechedule.Rows)
            {
                string itemCode = row[dalPlanning.partCode].ToString();
                DateTime produceStart = DateTime.TryParse(row[dalPlanning.productionStartDate].ToString(), out produceStart) ? produceStart : DateTime.MaxValue;

                if(produceStart >= start && produceStart <= end && itemCode == _ItemCode)
                {
                    string status = row[dalPlanning.planStatus].ToString();
                    int produceQty = int.TryParse(row[dalPlanning.targetQty].ToString(), out produceQty) ? produceQty : 0;

                    if(status == text.planning_status_running || status == text.planning_status_pending)
                    {
                        toProduce += produceQty;
                    }
                    else if (status == text.planning_status_completed)
                    {
                        produced += produceQty;
                    }

                }
            }
            return Tuple.Create(toProduce, produced);
        }

        private void LoadForecastData()
        {

            Cursor = Cursors.WaitCursor;

            btnSearch.Enabled = false;
            btnFilterApply.Enabled = false;
            btnRefresh.Enabled = false;

            frmLoading.ShowLoadingScreen();

            DataGridView dgv = dgvForecastReport;

            alertLevel = float.TryParse(txtAlertLevel.Text, out alertLevel) ? alertLevel : 0;
            dgvForecastReport.SuspendLayout();
            lblLastUpdated.Visible = true;
            lblUpdatedTime.Visible = true;
            lblNote.Visible = true;

            lblUpdatedTime.Text = DateTime.Now.ToString();
            dgvForecastReport.DataSource = null;


            string customer = cmbCustomer.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(customer))
            {
                DataTable dt_Data = NewForecastReportTable();
                DataRow dt_Row;

                DataTable dt = dalItemCust.custSearch(customer);

                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(customer).ToString());

                DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerSearch(customer);

                DataTable dt_PMMADate = dalPmmaDate.Select();

                DataTable dt_Estimate = dalTrfHist.ItemToCustomerAllTimeSearch(cmbCustomer.Text);

                dt_MacSchedule = dalPlanning.Select(); 

                dt_Join = dalJoin.SelectAll();
                dt_Item = dalItem.Select();

                int index = 1;

                dt.DefaultView.Sort = "item_name ASC";

                dt = dt.DefaultView.ToTable();

                #region load single part
                //normal speed
                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    if (assembly == 0 && production == 0)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        var result = GetProduceQty(uData.part_code, dt_MacSchedule);
                        uData.toProduce = result.Item1;
                        uData.Produced = result.Item2;

                        uData.forecast1 = GetForecastQty(dt_ItemForecast, uData.part_code, 1);
                        uData.forecast2 = GetForecastQty(dt_ItemForecast, uData.part_code, 2);
                        uData.forecast3 = GetForecastQty(dt_ItemForecast, uData.part_code, 3);

                        //uData.estimate = GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, dt_PMMADate);
                        uData.estimate = CalculateEstimateOrder(dt_PMMADate, dt_Estimate, uData.part_code, customer);

                        if (GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, dt_PMMADate) == 0)
                        {
                            uData.estimate = 0;
                        }

                        uData.deliveredOut = GetMaxOut(uData.part_code, customer, 0, dt_TrfHist, dt_PMMADate);
                        
                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (!customer.Equals("PMMA"))
                        {
                            uData.outStd = uData.estimate - uData.deliveredOut;

                        }
                        if (uData.forecast1 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate - uData.deliveredOut;
                            }
                        }
                        else if(uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }


                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        if(uData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uData.toProduce;
                        }

                        if (uData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uData.Produced;
                        }

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        dt_Data.Rows.Add(dt_Row);
                        index++;
        
                    }
                }

                #endregion

                #region load assembly part

                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    //check if got child part also
                    if ((assembly == 1 || production == 1) && tool.ifGotChild2(uData.part_code, dt_Join))
                    {
                        dt_Row = dt_Data.NewRow();
                        dt_Data.Rows.Add(dt_Row);

                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        var result = GetProduceQty(uData.part_code, dt_MacSchedule);
                        uData.toProduce = result.Item1;
                        uData.Produced = result.Item2;

                        uData.forecast1 = GetForecastQty(dt_ItemForecast, uData.part_code, 1);
                        uData.forecast2 = GetForecastQty(dt_ItemForecast, uData.part_code, 2);
                        uData.forecast3 = GetForecastQty(dt_ItemForecast, uData.part_code, 3);

                        //uData.estimate = GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, dt_PMMADate);
                        uData.estimate = CalculateEstimateOrder(dt_PMMADate, dt_Estimate, uData.part_code, customer);

                        if(GetMaxOut(uData.part_code, customer, 6, dt_TrfHist, dt_PMMADate) == 0)
                        {
                            uData.estimate = 0;
                        }

                        uData.deliveredOut = GetMaxOut(uData.part_code, customer, 0, dt_TrfHist, dt_PMMADate);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (!customer.Equals("PMMA"))
                        {
                            uData.outStd = uData.estimate - uData.deliveredOut;
                        }

                        if (uData.forecast1 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate - uData.deliveredOut;
                            }
                        }
                        else if(uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        if (uData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uData.toProduce;
                        }

                        if (uData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uData.Produced;
                        }


                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        if (assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = ProductionMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = ProductionAndAssemblyMarking;
                        }

                        if (uData.part_code.Substring(1, 2) == text.Inspection_Pass)
                        {
                            dt_Row[headerParentColor] = InspectionMarking;
                        }

                        dt_Data.Rows.Add(dt_Row);
                        index++;

                        //load child
                        LoadChild(dt_Data, uData, 0.1f);
                    }
                }

                #endregion

                if (dt_Data.Rows.Count > 0)
                {
                    //check repeated data: normal-slow speed
                    dt_Data = CalRepeatedData(dt_Data);

                    //item search:fast speed
                    dt_Data = ItemSearch(dt_Data);

                    dt_OrginalData = Sorting(dt_Data);
                    //Sorting:fast speed
                    dgvForecastReport.DataSource = dt_OrginalData;

                    //color change: slow speed
                    ColorData();

                    //delete column
                    dgvForecastReport.Columns.Remove(headerParentColor);
                    dgvForecastReport.Columns.Remove(headerType);
                    dgvForecastReport.Columns.Remove(headerBackColor);
                    dgvForecastReport.Columns.Remove(headerBalType);
                    dgvForecastReport.Columns.Remove(headerForecastType);

                    DgvForecastReportUIEdit(dgvForecastReport);
                    dgvForecastReport.ClearSelection();
                    dgvForecastReport.ResumeLayout();

                    

                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                }
            }


            frmLoading.CloseForm();
            
            btnSearch.Enabled = true;
            btnFilterApply.Enabled = true;
            btnRefresh.Enabled = true;

            Cursor = Cursors.Arrow;
        }

        private DataTable Sorting(DataTable dt)
        {
            string keywords = cmbSoryBy.Text;
            DataTable dt_Copy;
            bool dataInserted = false;
            bool singleToParent = false;
            bool spaceAdded = false;
            if (cmbSoryBy.SelectedIndex == -1 || keywords.Equals("") || string.IsNullOrEmpty(keywords))
            {
                dt_Copy = dt.Copy();
            }
            else
            {
                dt_Copy = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataInserted = false;
                    double sortingTarget_1 = double.TryParse(dt.Rows[i][keywords].ToString(), out sortingTarget_1) ? sortingTarget_1 : 0;
                    string itemType_1 = dt.Rows[i][headerType].ToString();
                    string itemCode = dt.Rows[i][headerPartCode].ToString();

                    //if(itemCode.Equals("V96LAR000"))
                    //{
                    //    float test = 0;
                    //}

                    if (itemType_1.Equals(typeParent) && !spaceAdded)
                    {
                        singleToParent = true;
                    }

                    if (singleToParent && !spaceAdded)
                    {
                        //add space
                        dt_Copy.Rows.Add(dt_Copy.NewRow());
                        spaceAdded = true;
                    }

                    if (!string.IsNullOrEmpty(itemType_1) && (itemType_1.Equals(typeSingle) || itemType_1.Equals(typeParent)))
                    {
                        for (int j = 0; j < dt_Copy.Rows.Count; j++)
                        {
                            bool waitingToInsert = false;
                            string itemType_2 = dt_Copy.Rows[j][headerType].ToString();
                            double sortingTarget_2 = double.TryParse(dt_Copy.Rows[j][keywords].ToString(), out sortingTarget_2) ? sortingTarget_2 : 0;
                            if (cbDescending.Checked)
                            {
                                waitingToInsert = sortingTarget_1 > sortingTarget_2;
                            }
                            else
                            {
                                waitingToInsert = sortingTarget_1 < sortingTarget_2;
                            }

                            if (itemType_1.Equals(itemType_2) && itemType_1.Equals(typeSingle))
                            {
                                if (waitingToInsert)
                                {
                                    //insert
                                    DataRow insertingRow = dt_Copy.NewRow();

                                    insertingRow.ItemArray = dt.Rows[i].ItemArray;

                                    dt_Copy.Rows.InsertAt(insertingRow, j);
                                    dataInserted = true;
                                    break;
                                }
                            }

                            else if (itemType_1.Equals(itemType_2) && itemType_1.Equals(typeParent))
                            {
                                if (waitingToInsert)
                                {
                                    //insert
                                    DataRow insertingRow = dt_Copy.NewRow();

                                    insertingRow.ItemArray = dt.Rows[i].ItemArray;

                                    dt_Copy.Rows.InsertAt(insertingRow, j);

                                    int tempIndex = j;

                                    for (int k = i + 1; k < dt.Rows.Count; k++)
                                    {
                                        tempIndex++;
                                        string _type = dt.Rows[k][headerType].ToString();

                                        if (_type.Equals(typeParent))
                                        {
                                            //dt_Copy.Rows.Add(dt_Copy.NewRow());
                                            break;
                                        }
                                        else
                                        {
                                            insertingRow = dt_Copy.NewRow();

                                            insertingRow.ItemArray = dt.Rows[k].ItemArray;

                                            dt_Copy.Rows.InsertAt(insertingRow, tempIndex);

                                        }
                                    }


                                    dataInserted = true;
                                    break;
                                }
                            }
                        }

                        if (!dataInserted)
                        {
                            //add data
                            if (itemType_1.Equals(typeSingle))
                            {
                                dt_Copy.ImportRow(dt.Rows[i]);
                            }

                            else if (itemType_1.Equals(typeParent))
                            {
                                dt_Copy.ImportRow(dt.Rows[i]);

                                for (int k = i + 1; k < dt.Rows.Count; k++)
                                {
                                    string _type = dt.Rows[k][headerType].ToString();

                                    if (_type.Equals(typeParent))
                                    {
                                        //dt_Copy.Rows.Add(dt_Copy.NewRow());
                                        break;
                                    }
                                    else
                                    {
                                        dt_Copy.ImportRow(dt.Rows[k]);

                                    }
                                }


                            }

                        }
                    }
                }
            }

            //remove last row if it is empty
            if (dt_Copy.Rows.Count > 0)
            {
                string itemCode = dt_Copy.Rows[dt_Copy.Rows.Count - 1][headerPartCode].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    dt_Copy.Rows.Remove(dt_Copy.Rows[dt_Copy.Rows.Count - 1]);
                }
                dt_Copy.AcceptChanges();
            }

            return dt_Copy;

        }

        private DataTable ItemSearch(DataTable dt)
        {
            string keywords = txtNameSearch.Text;
            DataTable dt_Copy;
            int parentIndex = -1;

            if (string.IsNullOrEmpty(keywords))
            {
                dt_Copy = dt.Copy();
            }
            else
            {
                dt_Copy = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string itemName = dt.Rows[i][headerPartName].ToString();
                    string itemCode = dt.Rows[i][headerPartCode].ToString();
                    string itemType = dt.Rows[i][headerType].ToString();

                    bool itemMatch = itemName.Contains(keywords.ToUpper()) || itemCode.Contains(keywords.ToUpper());

                    if (itemMatch && dt.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (itemType.Equals(typeSingle))
                        {
                            dt_Copy.ImportRow(dt.Rows[i]);
                            //dt.Rows.Remove(dt.Rows[i]);
                            dt.Rows[i][headerType] = DBNull.Value;

                            dt_Copy.Rows.Add(dt_Copy.NewRow());
                        }
                        else if (itemType.Equals(typeParent))
                        {
                            parentIndex = i;

                            dt_Copy.ImportRow(dt.Rows[i]);
                            //dt.Rows.Remove(dt.Rows[i]);
                            dt.Rows[i][headerType] = DBNull.Value;

                            for (int j = i + 1; j < dt.Rows.Count; j++)
                            {
                                string _type = dt.Rows[j][headerType].ToString();

                                if (_type.Equals(typeParent))
                                {
                                    break;
                                }
                                else if (dt.Rows[j].RowState != DataRowState.Deleted)
                                {
                                    dt_Copy.ImportRow(dt.Rows[j]);
                                    //dt.Rows.Remove(dt.Rows[j]);
                                    dt.Rows[j][headerType] = DBNull.Value;
                                }
                            }
                        }

                        else if (itemType.Equals(typeChild))
                        {
                            for (int j = i; j >= 0; j--)
                            {
                                string _type = dt.Rows[j][headerType].ToString();

                                if (_type.Equals(typeParent))
                                {
                                    parentIndex = j;
                                    break;
                                }
                            }

                            if (parentIndex >= 0)
                            {
                                dt_Copy.ImportRow(dt.Rows[parentIndex]);
                                //dt.Rows.Remove(dt.Rows[parentIndex]);
                                dt.Rows[parentIndex][headerType] = DBNull.Value;
                                for (int k = parentIndex + 1; k < dt.Rows.Count; k++)
                                {
                                    string _type = dt.Rows[k][headerType].ToString();

                                    if (_type.Equals(typeParent))
                                    {
                                        break;
                                    }
                                    else if (dt.Rows[k].RowState != DataRowState.Deleted)
                                    {
                                        dt_Copy.ImportRow(dt.Rows[k]);
                                        //dt.Rows.Remove(dt.Rows[k]);
                                        dt.Rows[k][headerType] = DBNull.Value;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Parent not found. (ItemSearch method error)");
                            }

                        }
                    }
                }
            }

            if (dt_Copy.Rows.Count > 0)
            {
                string itemCode = dt_Copy.Rows[dt_Copy.Rows.Count - 1][headerPartCode].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    dt_Copy.Rows.Remove(dt_Copy.Rows[dt_Copy.Rows.Count - 1]);
                }
                dt_Copy.AcceptChanges();
            }

            return dt_Copy;

        }

        private DataTable CalRepeatedData(DataTable dt)
        {
            double totalNeeded1 = 0;
            double totalNeeded2 = 0;
            double totalNeeded3 = 0;

            bool colorChange = false;
            int colorOrder = 0;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                totalNeeded1 = 0;
                totalNeeded2 = 0;
                totalNeeded3 = 0;

                string firstItem = dt.Rows[i][headerPartCode].ToString();
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                string firstParentColor = dt.Rows[i][headerParentColor].ToString();
                string type = dt.Rows[i][headerType].ToString();

                if (!(type.Equals(typeSingle) || type.Equals(typeParent)))
                {
                    dt.Rows[i][headerForecastType] = forecastType_Needed;
                }
                else
                {
                    dt.Rows[i][headerForecastType] = forecastType_Forecast;
                }

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();

                        if (firstItem.Equals(nextItem))
                        {
                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerForecast1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerForecast2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerForecast3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                            dt.Rows[j][headerBal1] = DBNull.Value;
                            dt.Rows[j][headerBal2] = DBNull.Value;

                            totalNeeded1 += nextNeededQty1;
                            totalNeeded2 += nextNeededQty2;
                            totalNeeded3 += nextNeededQty3;

                            dt.Rows[i][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBackColor] = (ColorSet)colorOrder;

                            colorChange = true;
                        }
                    }



                    if (colorChange)
                    {
                        dt.Rows[i][headerBal1] = firstBal1 - totalNeeded1;
                        dt.Rows[i][headerBal2] = firstBal2 - totalNeeded1 - totalNeeded2;

                        dt.Rows[i][headerBalType] = balType_Total;
                        colorOrder++;

                        if (colorOrder > Enum.GetValues(typeof(ColorSet)).Cast<int>().Max())
                        {
                            colorOrder = 0;

                        }
                        colorChange = false;
                    }



                }
            }

            return dt;


        }

        private void LoadChild(DataTable dt_Data, dataTrfBLL uParentData, float subIndex)
        {
            DataRow dt_Row;
            dataTrfBLL uChildData = new dataTrfBLL();

            double index = uParentData.index + subIndex;

            foreach (DataRow row in dt_Join.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if (parentCode.Equals(uParentData.part_code))
                {
                    string childCode = row[dalJoin.JoinChild].ToString();

                    if(parentCode == "CF ET 20MM 20MM")
                    {
                        float test = 0;
                    }

                    DataRow row_Item = tool.getDataRowFromDataTable(dt_Item, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    if (cbWithSubMat.Checked)
                    {
                        itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part) || row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_SubMat);
                    }

                    if (itemMatch)
                    {
                        //float childQty = uData.bal1;

                        float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float i) ? Convert.ToSingle(row[dalJoin.JoinQty].ToString()) : 1;
                        //int joinMax = int.TryParse(row[dalJoin.JoinMax].ToString(), out int j) ? Convert.ToInt32(row[dalJoin.JoinMax].ToString()) : 1;
                        //int JoinMin = int.TryParse(row[dalJoin.JoinMin].ToString(), out int k) ? Convert.ToInt32(row[dalJoin.JoinMin].ToString()) : 1;

                        //joinMax = joinMax <= 0 ? 1 : joinMax;
                        //JoinMin = JoinMin <= 0 ? 1 : JoinMin;

                        //int ParentQty = Convert.ToInt32(uData.bal1);

                        //int fullQty = ParentQty / joinMax;

                        //int notFullQty = ParentQty % joinMax;

                        //childQty = fullQty * joinQty;

                        //if (notFullQty >= JoinMin)
                        //{
                        //    childQty += joinQty;
                        //}

                        dt_Row = dt_Data.NewRow();

                        uChildData.part_code = row_Item[dalItem.ItemCode].ToString();

                        uChildData.index = index;
                        uChildData.part_name = row_Item[dalItem.ItemName].ToString();
                        uChildData.color_mat = row_Item[dalItem.ItemMBatch].ToString();
                        uChildData.color = row_Item[dalItem.ItemColor].ToString();
                        uChildData.raw_mat = row_Item[dalItem.ItemMaterial].ToString();
                        uChildData.pw_per_shot = row_Item[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProPWShot]);
                        uChildData.rw_per_shot = row_Item[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProRWShot]);
                        uChildData.cavity = row_Item[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row_Item[dalItem.ItemCavity]);
                        uChildData.cavity = uChildData.cavity == 0 ? 1 : uChildData.cavity;
                        uChildData.ready_stock = row_Item[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemStock]);

                        if (uParentData.bal1 >= 0)
                        {
                            uChildData.forecast1 = 0;
                        }
                        else
                        {
                            //needed = parent bal1(if <0) * joinQty
                            uChildData.forecast1 = uParentData.bal1 * -1 * joinQty;
                        }

                        if (uParentData.bal2 >= 0)
                        {
                            uChildData.forecast2 = 0;

                            if (uParentData.forecast3 <= -1)
                            {
                                if (uParentData.bal2 - uParentData.estimate < 0)
                                {
                                    uChildData.forecast3 = (uParentData.bal2 - uParentData.estimate) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = 0;
                                }
                            }
                            else
                            {
                                if (uParentData.bal2 - uParentData.forecast3 < 0)
                                {
                                    uChildData.forecast3 = (uParentData.bal2 - uParentData.forecast3) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = 0;
                                }
                            }

                            
                        }
                        else
                        {
                            if (uParentData.bal1 < 0)
                            {
                                if (uParentData.forecast2 <= -1)
                                {
                                    uChildData.forecast2 = uParentData.estimate * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast2 = uParentData.forecast2 * joinQty;
                                }
                               
                            }
                            else
                            {
                                uChildData.forecast2 = uParentData.bal2 * -1 * joinQty;
                            }

                            if (uParentData.forecast3 <= -1)
                            {
                                uChildData.forecast3 = uParentData.estimate * joinQty;
                            }
                            else
                            {
                                uChildData.forecast3 = uParentData.forecast3 * joinQty;
                            }

                            
                        }

                        

                        uChildData.forecast1 = uChildData.forecast1 < 0 ? uChildData.forecast1 * -1 : uChildData.forecast1;
                        uChildData.forecast2 = uChildData.forecast2 < 0 ? uChildData.forecast2 * -1 : uChildData.forecast2;
                        uChildData.forecast3 = uChildData.forecast3 < 0 ? uChildData.forecast3 * -1 : uChildData.forecast3;

                        uChildData.bal1 = uChildData.ready_stock - uChildData.forecast1;

                        uChildData.bal2 = uChildData.bal1 - uChildData.forecast2;

                        if (!uChildData.color.Equals(uChildData.color_mat) && !string.IsNullOrEmpty(uChildData.color_mat))
                        {
                            uChildData.color_mat += " (" + uParentData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        var result = GetProduceQty(uChildData.part_code, dt_MacSchedule);
                        uChildData.toProduce = result.Item1;
                        uChildData.Produced = result.Item2;

                        if (uChildData.toProduce > 0)
                        {
                            dt_Row[headerToProduce] = uChildData.toProduce;
                        }

                        if (uChildData.Produced > 0)
                        {
                            dt_Row[headerProduced] = uChildData.Produced;
                        }

                        dt_Row[headerIndex] = uChildData.index;
                        dt_Row[headerType] = typeChild;
                        dt_Row[headerRawMat] = uChildData.raw_mat;
                        dt_Row[headerPartCode] = uChildData.part_code;
                        dt_Row[headerPartName] = uChildData.part_name;
                        dt_Row[headerColorMat] = uChildData.color_mat;
                        dt_Row[headerPartWeight] = uChildData.pw_per_shot / uChildData.cavity + " (" + (uChildData.rw_per_shot / uChildData.cavity) + ")";
                        dt_Row[headerReadyStock] = uChildData.ready_stock;

                        dt_Row[headerForecast1] = uChildData.forecast1;
                        dt_Row[headerForecast2] = uChildData.forecast2;
                        dt_Row[headerForecast3] = uChildData.forecast3;

                        dt_Row[headerBal1] = uChildData.bal1;
                        dt_Row[headerBal2] = uChildData.bal2;

                        int assembly = row_Item[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemAssemblyCheck]);
                        int production = row_Item[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemProductionCheck]);

                        bool gotChild = tool.ifGotChild2(uChildData.part_code, dt_Join);

                        if (assembly == 1 && production == 0 && gotChild)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = ProductionMarking;
                        }
                        else if (assembly == 1 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = ProductionAndAssemblyMarking;
                        }

                        dt_Data.Rows.Add(dt_Row);

                        index += subIndex;

                        //check if got child part also
                        if (gotChild)
                        {
                            LoadChild(dt_Data, uChildData, subIndex / 10);
                        }
                    }

                }

            }


        }

        private float GetForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum)
        {
            string monthString = cmbForecastFrom.Text;

            int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year;

            if (month < DateTime.Now.Month)
            {
                year = DateTime.Now.Year + 1;
            }
            else
            {
                year = DateTime.Now.Year;
            }

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
            float MaxOut = 0, tmp = 0;
            bool includeCurrentMonth = false;

            if(customer.Equals("PMMA"))
            {
                if (pastMonthQty <= 0)
                {
                    DateTime start = dtpOutFrom.Value;
                    DateTime end = dtpOutTo.Value;

                    foreach (DataRow row in dt_TrfHist.Rows)
                    {
                        string item = row[dalTrfHist.TrfItemCode].ToString();
                        if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                        {
                            DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                            //string cust = row[dalTrfHist.TrfTo].ToString();


                            if (trfDate >= start && trfDate <= end)
                            {
                                MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                            }
                        }
                    }
                }
                else
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    if (DateTime.Today > tool.GetPMMAEndDate(currentMonth, currentYear))
                    {
                        includeCurrentMonth = true;
                    }

                    for (int i = 0; i < pastMonthQty; i++)
                    {
                        tmp = 0;

                        if (!(i == 0 && includeCurrentMonth))
                        {
                            if (currentMonth == 1)
                            {
                                currentMonth = 12;
                                currentYear--;
                            }
                            else
                            {
                                currentMonth--;
                            }

                        }

                        DateTime start = tool.GetPMMAStartDate(currentMonth, currentYear, dt_PMMADate);
                        DateTime end = tool.GetPMMAEndDate(currentMonth, currentYear, dt_PMMADate);

                        //dtpOutFrom.Value = new DateTime(year, month, 1);
                        //dtpOutTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                        foreach (DataRow row in dt_TrfHist.Rows)
                        {
                            string item = row[dalTrfHist.TrfItemCode].ToString();
                            if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                            {
                                DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                                if (trfDate >= start && trfDate <= end)
                                {
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }

                        //get maxout
                        if (MaxOut < tmp)
                        {
                            MaxOut = tmp;
                        }


                    }
                }
            }
            else
            {
                if (pastMonthQty <= 0)
                {
                    DateTime start = dtpOutFrom.Value;
                    DateTime end = dtpOutTo.Value;

                    foreach (DataRow row in dt_TrfHist.Rows)
                    {
                        string item = row[dalTrfHist.TrfItemCode].ToString();
                        if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                        {
                            DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                            if (trfDate >= start && trfDate <= end)
                            {
                                MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                            }
                        }
                    }
                }
                else
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    for (int i = 0; i < pastMonthQty; i++)
                    {
                        tmp = 0;

                        if (currentMonth == 1)
                        {
                            currentMonth = 12;
                            currentYear--;
                        }
                        else
                        {
                            currentMonth--;
                        }

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
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }

                        //get maxout
                        if (MaxOut < tmp)
                        {
                            MaxOut = tmp;
                        }
                    }
                }
            }

          

            return MaxOut;
        }

        private float Get6MonthsMaxOut(string itemCode, string customer)
        {
            float MaxOut = 0, tmp = 0;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            for (int i = 0; i < 6; i++)
            {
                tmp = 0;

                if (currentMonth == 1)
                {
                    currentMonth = 12;
                    currentYear--;
                }
                else
                {
                    currentMonth--;
                }

                //get transfer out record
                DataTable dt = dalTrfHist.ItemToCustomerDateSearch(customer, currentMonth.ToString(), currentYear.ToString(), itemCode);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow outRecord in dt.Rows)
                    {
                        if (outRecord["trf_result"].ToString().Equals("Passed"))
                        {
                            tmp += Convert.ToSingle(outRecord["trf_hist_qty"]);
                        }
                    }
                }

                //get maxout
                if (MaxOut < tmp)
                {
                    MaxOut = tmp;
                }


            }
            return MaxOut;
        }

        private void getStartandEndDate()
        {
            if (loaded && cmbForecastFrom.SelectedIndex != -1 && cmbCustomer.SelectedIndex != -1)
            {
                string monthString = cmbForecastFrom.Text;

                int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
                int year;

                if (month < DateTime.Now.Month)
                {
                    year = DateTime.Now.Year + 1;
                }
                else
                {
                    year = DateTime.Now.Year;
                }


                if (cmbCustomer.Text.Equals(tool.getCustName(1)))
                {

                    //if (cmbForecastFrom.SelectedIndex != -1)
                    //{
                    //    cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                    //}
                    DateTime outTo = tool.GetPMMAEndDate(month, year);

                    if (DateTime.Today > outTo && custChanging)
                    {
                        month++;

                        if (month > 12)
                        {
                            month -= 12;
                            year++;
                        }

                        cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    }
                    else
                    {
                        dtpOutFrom.Value = tool.GetPMMAStartDate(month, year);
                        dtpOutTo.Value = outTo;
                    }

                }
                else
                {
                    dtpOutFrom.Value = new DateTime(year, month, 1);
                    dtpOutTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }

            }
        }

        #endregion

        #region export to excel

        static void OpenCSVWithExcel(string path)
        {
            var ExcelApp = new Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);

            ExcelApp.Visible = true;
        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "ForecastReport(" + cmbCustomer.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                if(dgvForecastReport.DataSource == null)
                {
                    MessageBox.Show("No data.");
                }
                else
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        btnExcel.Enabled = false;

                        btnSearch.Enabled = false;
                        btnFilterApply.Enabled = false;
                        btnRefresh.Enabled = false;



                        SaveFileDialog sfd = new SaveFileDialog();
                        string path = @"D:\StockAssistant\Document\ForecastReport";
                        Directory.CreateDirectory(path);
                        sfd.InitialDirectory = path;
                        sfd.Filter = "Excel Documents (*.xls)|*.xls";
                        sfd.FileName = setFileName();

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                            // Copy DataGridView results to clipboard
                            copyAlltoClipboard();

                            object misValue = Missing.Value;
                            Excel.Application xlexcel = new Excel.Application
                            {
                                DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                            };

                            frmLoading.ShowLoadingScreen();

                            Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                            Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                            xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"); ;
                            xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") READY STOCK VERSUS FORECAST";
                            xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                            xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + ", OUT PEROID FROM:" + dtpOutFrom.Text + " TO:" + dtpOutTo.Text;
                            xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&8 *Produced: was produced within this month.\n-1: No data found in database.";


                            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                            xlWorkSheet.PageSetup.Zoom = false;

                            xlWorkSheet.PageSetup.FitToPagesWide = 1;
                            xlWorkSheet.PageSetup.FitToPagesTall = false;

                            double pointToCMRate = 0.035;
                            xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                            xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                            xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                            xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                            xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                            xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                            // Paste clipboard results to worksheet range
                            Range CR = (Range)xlWorkSheet.Cells[1, 1];
                            CR.Select();
                            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                            Range tRange = xlWorkSheet.UsedRange;
                            tRange.Font.Size = 8;
                            tRange.RowHeight = 15;
                            tRange.Font.Name = "Calibri";
                            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                            tRange.Borders.Weight = XlBorderWeight.xlThin;

                            DataGridView dgv = dgvForecastReport;

                            int forecast3Index = dgv.Columns[headerForecast3].Index;

                            //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                            Range FirstRow = xlWorkSheet.get_Range("a1:o1").Cells;
                            FirstRow.WrapText = true;
                            FirstRow.Font.Size = 6;
                            FirstRow.Font.Name = "Calibri";
                            FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                            FirstRow.RowHeight = 25;
                            FirstRow.Interior.Color = Color.WhiteSmoke;

                            

                            DataTable dt = (DataTable)dgv.DataSource;

                            int Index = dgv.Columns[headerIndex].Index;
                            int weightIndex = dgv.Columns[headerPartWeight].Index;

                            int bal1Index = dgv.Columns[headerBal1].Index;
                            int bal2Index = dgv.Columns[headerBal2].Index;

                            int nameIndex = dgv.Columns[headerPartName].Index;
                            int codeIndex = dgv.Columns[headerPartCode].Index;

                            int stockIndex = dgv.Columns[headerReadyStock].Index;
                            int estimateIndex = dgv.Columns[headerEstimate].Index;

                            int forecast1Index = dgv.Columns[headerForecast1].Index;
                            int forecast2Index = dgv.Columns[headerForecast2].Index;


                            int outIndex = dgv.Columns[headerOut].Index;
                            int outStdIndex = dgv.Columns[headerOutStd].Index;

                            xlWorkSheet.Cells[1, weightIndex + 1].ColumnWidth = 10;
                            xlWorkSheet.Cells[1, forecast1Index + 1].ColumnWidth = 8;
                            xlWorkSheet.Cells[1, forecast2Index + 1].ColumnWidth = 8;
                            xlWorkSheet.Cells[1, forecast3Index + 1].ColumnWidth = 8;
                            xlWorkSheet.Cells[1, bal1Index + 1].ColumnWidth = 8;
                            xlWorkSheet.Cells[1, bal2Index + 1].ColumnWidth = 8;
                            tRange.EntireColumn.AutoFit();

                            xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[stockIndex].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, estimateIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[estimateIndex].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, outIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[outIndex].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, outStdIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[outStdIndex].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, forecast1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast1Index].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, forecast2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[forecast2Index].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, bal1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal1Index].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, bal2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[1].Cells[bal2Index].InheritedStyle.BackColor);
                            xlWorkSheet.Cells[1, bal1Index + 1].Font.Color = Color.Red;
                            xlWorkSheet.Cells[1, bal2Index + 1].Font.Color = Color.Red;
                            xlWorkSheet.Cells[1, bal1Index + 1].Font.Bold = true;
                            xlWorkSheet.Cells[1, bal2Index + 1].Font.Bold = true;

                            for (int i = 0; i <= dt_OrginalData.Rows.Count - 1; i++)
                            {
                                Range rangeIndex = (Range)xlWorkSheet.Cells[i + 2, Index + 1];
                                Range rangeWeight = (Range)xlWorkSheet.Cells[i + 2, weightIndex + 1];

                                Range rangeName = (Range)xlWorkSheet.Cells[i + 2, nameIndex + 1];
                                Range rangeCode = (Range)xlWorkSheet.Cells[i + 2, codeIndex + 1];

                                Range rangeStock = (Range)xlWorkSheet.Cells[i + 2, stockIndex + 1];
                                Range rangeEstimate = (Range)xlWorkSheet.Cells[i + 2, estimateIndex + 1];

                                Range rangeForecast1 = (Range)xlWorkSheet.Cells[i + 2, forecast1Index + 1];
                                Range rangeForecast2 = (Range)xlWorkSheet.Cells[i + 2, forecast2Index + 1];
                                Range rangeForecast3 = (Range)xlWorkSheet.Cells[i + 2, forecast3Index + 1];

                                Range rangeOut = (Range)xlWorkSheet.Cells[i + 2, outIndex + 1];
                                Range rangeOutStd = (Range)xlWorkSheet.Cells[i + 2, outStdIndex + 1];

                                Range rangeRow = (Range)xlWorkSheet.Rows[i + 2];
                                //rangeRow.Rows.RowHeight = 40;
                                rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                                rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                                rangeWeight.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                                //color bal font
                                Range rangeBal1 = (Range)xlWorkSheet.Cells[i + 2, bal1Index + 1];
                                rangeBal1.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal1Index].InheritedStyle.ForeColor);

                                Range rangeBal2 = (Range)xlWorkSheet.Cells[i + 2, bal2Index + 1];
                                rangeBal2.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal2Index].InheritedStyle.ForeColor);

                                string itemCode = dgv.Rows[i].Cells[headerPartCode].Value.ToString();

                                //color empty space
                                if (string.IsNullOrEmpty(itemCode))
                                {
                                    rangeRow.Rows.RowHeight = 2;
                                    Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[i + 2, 1], xlWorkSheet.Cells[i + 2, xlWorkSheet.UsedRange.Columns.Count]];
                                    //Range rng2 = xlWorkSheet.Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, xlWorkSheet.UsedRange.Columns.Count]);
                                    //Range changeRowBackColor = xlWorkSheet.get_Range(i+2, last);
                                    rng.Interior.Color = Color.Black;
                                }
                                else
                                {
                                    rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.ForeColor);
                                    rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.ForeColor);

                                    rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[nameIndex].InheritedStyle.BackColor);
                                    rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[codeIndex].InheritedStyle.BackColor);

                                    rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[stockIndex].InheritedStyle.BackColor);
                                    rangeEstimate.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[estimateIndex].InheritedStyle.BackColor);

                                    rangeForecast1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[forecast1Index].InheritedStyle.BackColor);
                                    rangeForecast2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[forecast2Index].InheritedStyle.BackColor);
                                    rangeForecast3.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[forecast3Index].InheritedStyle.BackColor);

                                    rangeBal1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal1Index].InheritedStyle.BackColor);
                                    rangeBal2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[bal2Index].InheritedStyle.BackColor);

                                    rangeOut.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[outIndex].InheritedStyle.BackColor);
                                    rangeOutStd.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[outStdIndex].InheritedStyle.BackColor);
                                }

                                string parentColor = dt.Rows[i][headerParentColor].ToString();
                                string type = dt.Rows[i][headerType].ToString();
                                string balType = dt.Rows[i][headerBalType].ToString();

                                //change parent color
                                if (parentColor.Equals(AssemblyMarking))
                                {
                                    rangeName.Font.Underline = true;
                                    rangeCode.Font.Underline = true;
                                }
                                else if (parentColor.Equals(ProductionMarking))
                                {
                                    rangeName.Font.Underline = true;
                                    rangeCode.Font.Underline = true;
                                }
                                else if (parentColor.Equals(ProductionAndAssemblyMarking))
                                {
                                    rangeName.Font.Underline = true;
                                    rangeCode.Font.Underline = true;
                                }

                                if (type.Equals(typeChild))
                                {
                                    rangeForecast1.Font.Italic = true;
                                    rangeForecast2.Font.Italic = true;
                                    rangeForecast3.Font.Italic = true;
                                }

                                if (balType.Equals(balType_Total))
                                {
                                    rangeBal1.Font.Bold = true;
                                    rangeBal1.Font.Italic = true;
                                    rangeBal1.Font.Underline = true;

                                    rangeBal2.Font.Bold = true;
                                    rangeBal2.Font.Italic = true;
                                    rangeBal2.Font.Underline = true;
                                    //dgv.Rows[i].Cells[headerBal1].Style.Font = _BalFont;
                                    //dgv.Rows[i].Cells[headerBal2].Style.Font = _BalFont;
                                }
                                else
                                {
                                    rangeBal1.Font.Bold = true;
                                    rangeBal2.Font.Bold = true;
                                }

                            }

                            // Save the excel file under the captured location from the SaveFileDialog
                            xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                            xlexcel.DisplayAlerts = true;
                            xlWorkBook.Close(true, misValue, misValue);
                            xlexcel.Quit();

                            releaseObject(xlWorkSheet);
                            releaseObject(xlWorkBook);
                            releaseObject(xlexcel);

                            // Clear Clipboard and DataGridView selection
                            Clipboard.Clear();
                            dgvForecastReport.ClearSelection();

                            // Open the newly saved excel file
                            //if (File.Exists(sfd.FileName))
                            //    System.Diagnostics.Process.Start(sfd.FileName);
                            OpenCSVWithExcel(sfd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        tool.saveToTextAndMessageToUser(ex);
                    }
                    finally
                    {
                        frmLoading.CloseForm();

                        btnExcel.Enabled = true;
                        btnSearch.Enabled = true;
                        btnFilterApply.Enabled = true;
                        btnRefresh.Enabled = true;

                        Cursor = Cursors.Arrow; // change cursor to normal type
                    }
                }
               
            }
          
        }

        private void copyAlltoClipboard()
        {
            dgvForecastReport.SelectAll();
            DataObject dataObj = dgvForecastReport.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
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

        private void btnExcelAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\ForecastReport";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "ForecastReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;
                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    insertAllDataToSheet(path, sfd.FileName);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvForecastReport.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void insertAllDataToSheet(string path, string fileName)
        {
            string custName = tool.getCustName(1);

            int totalCust = cmbCustomer.Items.Count;

            Excel.Application excelApp = new Excel.Application
            {
                Visible = true
            };

            Workbook g_Workbook = excelApp.Workbooks.Open(
               path,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing);

            object misValue = Missing.Value;

            //frmLoading.ShowLoadingScreen();
            for (int i = 0; i <= cmbCustomer.Items.Count - 1; i++)
            {
                cmbCustomer.SelectedIndex = i;
                LoadForecastData();

                string cust = cmbCustomer.Text;

                DataTable dt = NewForecastReportTable();

                if (dgvForecastReport.DataSource != null)
                {
                    dt = (DataTable)dgvForecastReport.DataSource;
                }

                if (dt.Rows.Count > 0)//if datagridview have data
                {
                    Worksheet xlWorkSheet = null;

                    int count = g_Workbook.Worksheets.Count;

                    xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                            g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                    xlWorkSheet.Name = cmbCustomer.Text;

                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&12 (" + cmbCustomer.Text + ") READY STOCK VERSUS FORECAST";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + ", OUT PEROID FROM:" + dtpOutFrom.Text + " TO:" + dtpOutTo.Text;
                    xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&8 *Produced: was produced within this month.\n-1: No data found in database.";


                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;
                    
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                    // Paste clipboard results to worksheet range
                    copyAlltoClipboard();
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);



                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Font.Size = 8;
                    tRange.RowHeight = 15;
                    tRange.Font.Name = "Calibri";
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;

                    DataGridView dgv = dgvForecastReport;

                    int forecast3Index = dgv.Columns[headerForecast3].Index;

                    //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                    Range FirstRow = xlWorkSheet.get_Range("a1:o1").Cells;
                    FirstRow.WrapText = true;
                    FirstRow.Font.Size = 6;
                    FirstRow.Font.Name = "Calibri";
                    FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    FirstRow.RowHeight = 25;
                    FirstRow.Interior.Color = Color.WhiteSmoke;



                    //DataTable dt = (DataTable)dgv.DataSource;

                    int Index = dgv.Columns[headerIndex].Index;
                    int weightIndex = dgv.Columns[headerPartWeight].Index;

                    int bal1Index = dgv.Columns[headerBal1].Index;
                    int bal2Index = dgv.Columns[headerBal2].Index;

                    int nameIndex = dgv.Columns[headerPartName].Index;
                    int codeIndex = dgv.Columns[headerPartCode].Index;

                    int stockIndex = dgv.Columns[headerReadyStock].Index;
                    int estimateIndex = dgv.Columns[headerEstimate].Index;

                    int forecast1Index = dgv.Columns[headerForecast1].Index;
                    int forecast2Index = dgv.Columns[headerForecast2].Index;


                    int outIndex = dgv.Columns[headerOut].Index;
                    int outStdIndex = dgv.Columns[headerOutStd].Index;

                    xlWorkSheet.Cells[1, weightIndex + 1].ColumnWidth = 10;
                    xlWorkSheet.Cells[1, forecast1Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, forecast2Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, forecast3Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, bal1Index + 1].ColumnWidth = 8;
                    xlWorkSheet.Cells[1, bal2Index + 1].ColumnWidth = 8;
                    tRange.EntireColumn.AutoFit();

                    xlWorkSheet.Cells[1, stockIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[stockIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, estimateIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[estimateIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, outIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[outIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, outStdIndex + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[outStdIndex].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, forecast1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[forecast1Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, forecast2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[forecast2Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, bal1Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[bal1Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, bal2Index + 1].Interior.Color = ColorTranslator.ToOle(dgv.Rows[0].Cells[bal2Index].InheritedStyle.BackColor);
                    xlWorkSheet.Cells[1, bal1Index + 1].Font.Color = Color.Red;
                    xlWorkSheet.Cells[1, bal2Index + 1].Font.Color = Color.Red;
                    xlWorkSheet.Cells[1, bal1Index + 1].Font.Bold = true;
                    xlWorkSheet.Cells[1, bal2Index + 1].Font.Bold = true;

                    for (int j = 0; j <= dt_OrginalData.Rows.Count - 1; j++)
                    {
                        Range rangeIndex = (Range)xlWorkSheet.Cells[j + 2, Index + 1];
                        Range rangeWeight = (Range)xlWorkSheet.Cells[j + 2, weightIndex + 1];

                        Range rangeName = (Range)xlWorkSheet.Cells[j + 2, nameIndex + 1];
                        Range rangeCode = (Range)xlWorkSheet.Cells[j + 2, codeIndex + 1];

                        Range rangeStock = (Range)xlWorkSheet.Cells[j + 2, stockIndex + 1];
                        Range rangeEstimate = (Range)xlWorkSheet.Cells[j + 2, estimateIndex + 1];

                        Range rangeForecast1 = (Range)xlWorkSheet.Cells[j + 2, forecast1Index + 1];
                        Range rangeForecast2 = (Range)xlWorkSheet.Cells[j + 2, forecast2Index + 1];
                        Range rangeForecast3 = (Range)xlWorkSheet.Cells[j + 2, forecast3Index + 1];

                        Range rangeOut = (Range)xlWorkSheet.Cells[j + 2, outIndex + 1];
                        Range rangeOutStd = (Range)xlWorkSheet.Cells[j + 2, outStdIndex + 1];

                        Range rangeRow = (Range)xlWorkSheet.Rows[j + 2];
                        //rangeRow.Rows.RowHeight = 40;
                        rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                        rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                        rangeWeight.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                        //color bal font
                        Range rangeBal1 = (Range)xlWorkSheet.Cells[j + 2, bal1Index + 1];
                        rangeBal1.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal1Index].InheritedStyle.ForeColor);

                        Range rangeBal2 = (Range)xlWorkSheet.Cells[j + 2, bal2Index + 1];
                        rangeBal2.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal2Index].InheritedStyle.ForeColor);

                        string itemCode = dgv.Rows[j].Cells[headerPartCode].Value.ToString();

                        //color empty space
                        if (string.IsNullOrEmpty(itemCode))
                        {
                            rangeRow.Rows.RowHeight = 2;
                            Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[j + 2, 1], xlWorkSheet.Cells[j + 2, xlWorkSheet.UsedRange.Columns.Count]];
                            //Range rng2 = xlWorkSheet.Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, xlWorkSheet.UsedRange.Columns.Count]);
                            //Range changeRowBackColor = xlWorkSheet.get_Range(i+2, last);
                            rng.Interior.Color = Color.Black;
                        }
                        else
                        {
                            rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.ForeColor);
                            rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.ForeColor);

                            rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.BackColor);
                            rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.BackColor);

                            rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[stockIndex].InheritedStyle.BackColor);
                            rangeEstimate.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[estimateIndex].InheritedStyle.BackColor);

                            rangeForecast1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast1Index].InheritedStyle.BackColor);
                            rangeForecast2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast2Index].InheritedStyle.BackColor);
                            rangeForecast3.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast3Index].InheritedStyle.BackColor);

                            rangeBal1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal1Index].InheritedStyle.BackColor);
                            rangeBal2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal2Index].InheritedStyle.BackColor);

                            rangeOut.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[outIndex].InheritedStyle.BackColor);
                            rangeOutStd.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[outStdIndex].InheritedStyle.BackColor);
                        }

                        string parentColor = dt.Rows[j][headerParentColor].ToString();
                        string type = dt.Rows[j][headerType].ToString();
                        string balType = dt.Rows[j][headerBalType].ToString();

                        //change parent color
                        if (parentColor.Equals(AssemblyMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }
                        else if (parentColor.Equals(ProductionMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }
                        else if (parentColor.Equals(ProductionAndAssemblyMarking))
                        {
                            rangeName.Font.Underline = true;
                            rangeCode.Font.Underline = true;
                        }

                        if (type.Equals(typeChild))
                        {
                            rangeForecast1.Font.Italic = true;
                            rangeForecast2.Font.Italic = true;
                            rangeForecast3.Font.Italic = true;
                        }

                        if (balType.Equals(balType_Total))
                        {
                            rangeBal1.Font.Bold = true;
                            rangeBal1.Font.Italic = true;
                            rangeBal1.Font.Underline = true;

                            rangeBal2.Font.Bold = true;
                            rangeBal2.Font.Italic = true;
                            rangeBal2.Font.Underline = true;
                            //dgv.Rows[i].Cells[headerBal1].Style.Font = _BalFont;
                            //dgv.Rows[i].Cells[headerBal2].Style.Font = _BalFont;
                        }
                        else
                        {
                            rangeBal1.Font.Bold = true;
                            rangeBal2.Font.Bold = true;
                        }

                    }
                   

                    releaseObject(xlWorkSheet);
                    Clipboard.Clear();
                    dgvForecastReport.ClearSelection();
                }

            }

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        private void frmForecastReport_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastReportInputFormOpen = false;
        }

        private void frmForecastReport_NEW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbCustomer.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a customer.");
                }
                else
                {
                    LoadForecastData();
                }
            }
        }

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbCustomer.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a customer.");
                }
                else
                {
                    LoadForecastData();
                }
            }
        }

        private void txtNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbCustomer.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a customer.");
                }
                else
                {
                    LoadForecastData();
                }
            }
        }
    }

}

