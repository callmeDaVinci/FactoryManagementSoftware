using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.ReportingServices.Interfaces;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Syncfusion.XlsIO.Parser.Biff_Records.PivotTable.PivotViewItemRecord;

namespace FactoryManagementSoftware.UI
{
    public partial class frmJobPurposeEdit : Form
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

        dataTrfBLL uData = new dataTrfBLL();

        private DataTable DT_ITEM;
        private DataTable DT_MOULD_ITEM;
        bool CMB_CODE_READY = false;
        static public string JOB_PURPOSE;
        private readonly string ITEM_TYPE;

        private string ITEM_CODE = null;
        private string ITEM_NAME = null;
        private string DGV_TITLE = ": Monthly Balance Estimate";
        private readonly string OTHER_INITIAL_CONTENT = "Fill in other planning purposes here.";
        private readonly string OTHER_PURPOSE_INITIAL = "OTHER PURPOSE";

        #endregion

      
        public frmJobPurposeEdit(string itemCode, string itemName)
        {
            InitializeComponent();
            ITEM_CODE = itemCode;
            ITEM_NAME = itemName;

            if(ITEM_CODE == ITEM_NAME)
            {
                lblItemDescription.Text = "Item: " + ITEM_NAME + DGV_TITLE;
            }
            else
            {
                lblItemDescription.Text = "Item: " + ITEM_NAME + " (" + ITEM_CODE + ")";
            }

            JOB_PURPOSE = "";
        }


        private DataTable NewMonthlyBalanceEsimateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Selection, typeof(bool));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(double));

            return dt;
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            int smallColumnWidth = 60;


            dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[text.Header_BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_Selection].Width = smallColumnWidth;
        }


        private void LoadMonthlyBalanceEstimate()
        {
            DataTable dt_MonthlyBalance = NewMonthlyBalanceEsimateTable();
            DataRow row_MonthlyBal;

            int month, year;

            if (!string.IsNullOrEmpty(ITEM_CODE))
            {

                float readyStock = dalItem.getStockQty(ITEM_CODE);

                row_MonthlyBal = dt_MonthlyBalance.NewRow();
                row_MonthlyBal[text.Header_Selection] = false;
                row_MonthlyBal[text.Header_Description] = text.Header_ReadyStock;
                row_MonthlyBal[text.Header_BalStock] = readyStock;

                dt_MonthlyBalance.Rows.Add(row_MonthlyBal);

                month = DateTime.Now.Month;
                year = DateTime.Now.Year;

                string monthName;
                monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);

                int monthStart = DateTime.Now.Month;
                int yearStart = DateTime.Now.Year;

                int totalMonth = 2;

                DateTime dateStart = new DateTime(yearStart, monthStart, 1);
                DateTime dateEnd = dateStart.AddMonths(totalMonth);

                int monthEnd = dateEnd.Month;
                int yearEnd = dateEnd.Year;

                dateEnd = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd));

                DataTable DT_Forecast_Summary = tool.MonthlyBalanceEstimate(ITEM_CODE);

                DateTime DateTmp = dateStart;

                #region new Method

                int rowIndex = -1;

                foreach (DataRow row in DT_Forecast_Summary.Rows)
                {
                    if (row[text.Header_PartCode].ToString() == ITEM_CODE)
                    {
                        rowIndex = DT_Forecast_Summary.Rows.IndexOf(row);
                        break;
                    }

                }

                if (rowIndex != -1)
                {
                    float balLastMonth = readyStock;

                    while (DateTmp < dateEnd)
                    {
                        bool monthBalAdded = false;

                        foreach (DataColumn col in DT_Forecast_Summary.Columns)
                        {
                            string colName = col.ColumnName;


                            string monthText = string.IsNullOrEmpty(DateTmp.Month.ToString()) ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) : CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTmp.Month);

                            monthText = tool.GetAbbreviatedFromFullName(monthText).ToUpper();

                            if (colName.Contains("BAL") && colName.Contains(monthText))
                            {

                                float estBalance = float.TryParse(DT_Forecast_Summary.Rows[rowIndex][colName].ToString(), out estBalance) ? estBalance : 0;

                                balLastMonth = estBalance;

                                monthName = new DateTime(DateTmp.Year, DateTmp.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);


                                row_MonthlyBal = dt_MonthlyBalance.NewRow();
                                row_MonthlyBal[text.Header_Selection] = false;
                                row_MonthlyBal[text.Header_Description] = monthName + " " + DateTmp.Year;
                                row_MonthlyBal[text.Header_BalStock] = estBalance;

                                dt_MonthlyBalance.Rows.Add(row_MonthlyBal);

                                monthBalAdded = true;
                                break;
                            }

                        }

                        if (!monthBalAdded)
                        {
                            monthName = new DateTime(DateTmp.Year, DateTmp.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);

                            row_MonthlyBal = dt_MonthlyBalance.NewRow();
                            row_MonthlyBal[text.Header_Selection] = false;
                            row_MonthlyBal[text.Header_Description] = monthName + " " + DateTmp.Year;
                            row_MonthlyBal[text.Header_BalStock] = balLastMonth;

                            dt_MonthlyBalance.Rows.Add(row_MonthlyBal);
                        }

                        DateTmp = DateTmp.AddMonths(1);

                    }
                }
                else
                {
                    while (DateTmp < dateEnd)
                    {
                        monthName = new DateTime(DateTmp.Year, DateTmp.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);

                        row_MonthlyBal = dt_MonthlyBalance.NewRow();
                        row_MonthlyBal[text.Header_Selection] = false;
                        row_MonthlyBal[text.Header_Description] = monthName + " " + DateTmp.Year;
                        row_MonthlyBal[text.Header_BalStock] = readyStock;

                        dt_MonthlyBalance.Rows.Add(row_MonthlyBal);

                        DateTmp = DateTmp.AddMonths(1);
                    }
                }
                #endregion

            }


            dgvMonthlyBalanceEstimate.DataSource = null;

            if (dt_MonthlyBalance.Rows.Count > 0)
            {
                dgvMonthlyBalanceEstimate.DataSource = dt_MonthlyBalance;

                dgvUIEdit(dgvMonthlyBalanceEstimate);

                dgvMonthlyBalanceEstimate.ClearSelection();
            }
        }

        private void btnCancelPartInfoEdit_Click(object sender, EventArgs e)
        {
            JOB_PURPOSE = "";

            Close();
        }

        private void JobPurposeConfirmed_Click(object sender, EventArgs e)
        {
            if(getPurpose())
            {
                Close();
            }
        }

        private void cbOtherPurpose_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOtherPurpose.Checked && dgvMonthlyBalanceEstimate?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvMonthlyBalanceEstimate.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    row[text.Header_Selection] = false;
                }

                dgvMonthlyBalanceEstimate.ClearSelection();
            }
            else
            {
                JOB_PURPOSE = "";
            }
        }

        private void txtOtherPurpose_Enter(object sender, EventArgs e)
        {
            if (txtOtherPurpose.Text == OTHER_INITIAL_CONTENT)
            {
                txtOtherPurpose.Text = "";
                txtOtherPurpose.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtOtherPurpose_Leave(object sender, EventArgs e)
        {
            if (txtOtherPurpose.Text.Length == 0)
            {
                txtOtherPurpose.Text = OTHER_INITIAL_CONTENT;
                txtOtherPurpose.ForeColor = SystemColors.GrayText;

                if(cbOtherPurpose.Checked)
                {
                    JOB_PURPOSE = OTHER_PURPOSE_INITIAL;
                }
            }
        }

        private void txtOtherPurpose_TextChanged(object sender, EventArgs e)
        {
            string JobPurpose = txtOtherPurpose.Text;

            if (!string.IsNullOrEmpty(JobPurpose) && !JobPurpose.Equals(OTHER_INITIAL_CONTENT))
            {
                cbOtherPurpose.Checked = true;
            }
        }

        private void frmJobPurposeEdit_Load(object sender, EventArgs e)
        {
            LoadMonthlyBalanceEstimate();
        }

        private void dgvMonthlyBalanceEstimate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvMonthlyBalanceEstimate.Columns[text.Header_Selection].Index &&
       e.RowIndex >= 0 &&
       dgvMonthlyBalanceEstimate.Rows[e.RowIndex].Cells[text.Header_Description].Value.ToString().Contains(text.Header_ReadyStock) == true)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Handled = true;
            }
        }

        private bool getPurpose()
        {
            JOB_PURPOSE = "";

            if(cbOtherPurpose.Checked)
            {
                string purpose = txtOtherPurpose.Text;

                if(string.IsNullOrEmpty(purpose) || purpose == OTHER_INITIAL_CONTENT)
                {
                    JOB_PURPOSE = OTHER_PURPOSE_INITIAL;
                }
                else
                {
                    JOB_PURPOSE = purpose;
                }
            }

            else if(dgvMonthlyBalanceEstimate?.Rows.Count > 0)
            {
                DataTable dt = (DataTable) dgvMonthlyBalanceEstimate.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    bool selection = bool.TryParse(row[text.Header_Selection].ToString(), out selection) ? selection : false;

                    if (selection)
                    {
                        string purpose = row[text.Header_Description].ToString();

                        if (!purpose.Equals(text.Header_ReadyStock))
                        {
                            if (JOB_PURPOSE.Equals(""))
                            {
                                JOB_PURPOSE = purpose.Replace("20", "");
                            }
                            else
                            {
                                JOB_PURPOSE += "/" + purpose.Replace("20", "");
                            }
                        }

                        
                    }
                }
            }

            if(string.IsNullOrEmpty(JOB_PURPOSE))
            {
                MessageBox.Show("Please select at least one purpose.");
                return false;
            }

            return true;

        }

        private void dgvMonthlyBalanceEstimate_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            DataGridView dgv = dgvMonthlyBalanceEstimate;

            if (row >= 0)
            {
                string colName = dgv.Columns[col].Name;

                if (colName == text.Header_Selection)
                {
                    bool selected = bool.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        cbOtherPurpose.Checked = false;
                    }
                }
            }

        }

        private void dgvMonthlyBalanceEstimate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            // If the clicked cell is in the 'Selection' column and it's a checkbox cell
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                senderGrid.Columns[e.ColumnIndex].Name == text.Header_Selection &&
                e.RowIndex >= 0)
            {
                // Uncheck the 'Other' checkbox
                cbOtherPurpose.Checked = false;
            }
        }
    }
}
