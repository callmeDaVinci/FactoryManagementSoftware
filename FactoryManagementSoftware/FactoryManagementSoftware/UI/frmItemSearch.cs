using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Guna.UI2.WinForms.Suite;
using Syncfusion.XlsIO.Implementation.XmlSerialization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemSearch : Form
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

        //private DataTable DT_ITEM;
        private DataTable DT_SELECTED_ITEM;
        private DataTable DT_ITEM_SOURCE;

        private DataTable DT_MOULD_ITEM;
        bool CMB_CODE_READY = false;
        static public string ITEM_CODE_SELECTED;
        private string ITEM_TYPE;
        static public string JOB_PURPOSE;
        static public int JOB_TARGET_QTY;
        private string COLOR_CODE;

        private bool COLOR_MAT_MODE = false;

        private readonly string JOB_PURPOSE_INITIAL_CONTENT = "Please fill in the job purposes here, or select the month above.";

        #endregion

        public frmItemSearch()
        {
            ITEM_CODE_SELECTED = "";
            InitializeComponent();
            InitialSetting();
        }

        public frmItemSearch(string itemType)
        {
            ITEM_TYPE = itemType;
            //DT_ITEM = dalItem.CatSearch(itemType);

            ITEM_CODE_SELECTED = "";

            InitializeComponent();
            InitialSetting();
        }

        public frmItemSearch(DataTable dt, string itemType)
        {
            InitializeComponent();
           
            JOB_PURPOSE = "";
            JOB_TARGET_QTY = 0;

            ITEM_TYPE = itemType;
            DT_SELECTED_ITEM = dt;
            ITEM_CODE_SELECTED = "";

            InitialSetting();
        }

        public frmItemSearch(DataTable dt, string itemType, bool ShowPurposeAndTargetQty)
        {
            InitializeComponent();
            DT_SELECTED_ITEM = dt;

            JOB_PURPOSE = "";
            JOB_TARGET_QTY = 0;

            ITEM_TYPE = itemType;
            ITEM_CODE_SELECTED = "";

            ShowPurposeAndTargetQtyUI(ShowPurposeAndTargetQty);

            InitialSetting();
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
            if(dgv == dgvMonthlyBalanceEstimate)
            {
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //dgv.Columns[text.Header_Selection].Width = smallColumnWidth;
                dgv.Columns[text.Header_Selection].Visible = false;
            }
        }

        private void MonthCheckBoxNameIntitial()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

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


            DateTime DateTmp = dateStart;

            int count = 1;

            while (DateTmp < dateEnd && count < 4)
            {
                monthName = new DateTime(DateTmp.Year, DateTmp.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);

                if (count == 1)
                {
                    cbPurposeMonth1.Text = monthName + " " + DateTmp.Year.ToString().Substring(2);

                    count++;
                }
                else if (count == 2)
                {
                    cbPurposeMonth2.Text = monthName + " " + DateTmp.Year.ToString().Substring(2);

                    count++;
                }
                else if (count == 3)
                {
                    cbPurposeMonth3.Text = monthName + " " + DateTmp.Year.ToString().Substring(2);
                    count++;

                }

                DateTmp = DateTmp.AddMonths(1);
            }
        }

        private void LoadMonthlyBalanceEstimate()
        {
            frmLoading.ShowLoadingScreen();

            dgvMonthlyBalanceEstimate.DataSource = null;
            string itemCode = cmbPartCode.Text;

            if(!string.IsNullOrEmpty(itemCode))
            {
                DataTable dt_MonthlyBalance = NewMonthlyBalanceEsimateTable();
                DataRow row_MonthlyBal;

                int month, year;

                float readyStock = dalItem.getStockQty(itemCode);

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

                DataTable DT_Forecast_Summary = tool.NEW_MonthlyBalanceEstimate(itemCode);
                //DataTable DT_Forecast_Summary = tool.MonthlyBalanceEstimate(itemCode);
                
                DateTime DateTmp = dateStart;

                #region new Method

                int rowIndex = -1;

                foreach (DataRow row in DT_Forecast_Summary.Rows)
                {
                    if (row[text.Header_PartCode].ToString() == itemCode)
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

                if (dt_MonthlyBalance.Rows.Count > 0)
                {
                    dgvMonthlyBalanceEstimate.DataSource = dt_MonthlyBalance;

                    dgvUIEdit(dgvMonthlyBalanceEstimate);

                    //MonthlyEstimateBalanceCellFormatting(dgvMonthlyBalanceEstimate);

                    dgvMonthlyBalanceEstimate.ClearSelection();
                }
            }

            frmLoading.CloseForm();
            
        }

        private void MonthlyEstimateBalanceCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    decimal num = decimal.TryParse(dgv.Rows[rowIndex].Cells[text.Header_BalStock].Value.ToString(), out num) ? num : 0;

                    if (num < 0)
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_BalStock].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[text.Header_BalStock].Style.ForeColor = Color.Black;
                    }


                }

                dgv.ResumeLayout();
            }
        }

        private void ShowPurposeAndTargetQtyUI(bool show)
        {
            if(show)
            {
                //form side
                Size = new Size(1100, 700);

                //column
                tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 400);
                tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);

                MonthCheckBoxNameIntitial();

            }
            else
            {
                //form side
                Size = new Size(500,460);

                //column
                tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
            }
        }
        private void frmPlanning_Load(object sender, EventArgs e)
        {
            
        }

        itemCatDAL dalItemCat = new itemCatDAL();
        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbCategory.DataSource = distinctTable;
            cmbCategory.DisplayMember = "item_cat_name";
            cmbCategory.Text = text.Cat_Part;
        }

        private void loadItemCategoryData(string category)
        {
            if(!CAT_NAME_DATA_LOADING)
            {
                CAT_NAME_DATA_LOADING = true;
                DataTable dtItemCat = dalItemCat.Select();

                DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
                distinctTable.Rows.Add("All");
                distinctTable.DefaultView.Sort = "item_cat_name ASC";
                cmbCategory.DataSource = distinctTable;
                cmbCategory.DisplayMember = "item_cat_name";
                cmbCategory.Text = category;

                CAT_NAME_DATA_LOADING = false;

            }

        }

        private void ctbPartName_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            CMB_CODE_READY = false;
            cmbPartCode.DataSource = null;

            errorProvider1.Clear();
            string keywords = txtPartName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = DT_ITEM_SOURCE.Copy();

                // Only call AcceptChanges once, improving performance
                dt.AcceptChanges();

                var rows = dt.AsEnumerable()
                    .Where(r => r.Field<string>(dalItem.ItemName) == keywords);

                if(rows.Any())
                {
                    // Convert filtered enumerable back to DataTable
                    dt = rows.CopyToDataTable();

                    foreach (DataRow row in dt.Rows)
                    {
                        string itemCat = row[dalItem.ItemCat].ToString();

                        if (!itemCat.Equals(ITEM_TYPE))
                        {
                            row.Delete();
                        }
                    }

                    dt.AcceptChanges();

                    cmbPartCode.DataSource = dt;
                    cmbPartCode.DisplayMember = "item_code";
                    cmbPartCode.ValueMember = "item_code";



                    int count = cmbPartCode.Items.Count;
                    cmbPartCode.SelectedIndex = -1;

                    if (count == 1)
                    {
                        cmbPartCode.SelectedIndex = 0;

                    }
                    else
                    {
                        cmbPartCode.DroppedDown = true;
                    }

                    CMB_CODE_READY = true;
                }
               

            }
         

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
     
        #region UI/UX


        private void ReferenceUIUpdate(int buttonNo)
        {
            if(buttonNo == 0)
            {
                tlpReferencesList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tlpReferencesList.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
                tlpReferencesList.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);
                tlpReferencesList.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
                tlpReferencesList.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);


                dgvMonthlyBalanceEstimate.Visible = false;
                dgvProductionHistory.Visible = false;
                dgvUsageRecord.Visible = false;

                btnLoadMonthlyEstimateBalance.Visible = true;
                btnLoadProductionHistory.Visible = true;
                btnLoadUsageRecord.Visible = true;

                tlpReferencesMonthlyBalEstimate.RowStyles[0] = new RowStyle(SizeType.Absolute, 50);
                tlpReferencesMonthlyBalEstimate.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesMonthlyBalEstimate.RowStyles[2] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesMonthlyBalEstimate.RowStyles[3] = new RowStyle(SizeType.Percent, 100);

                tlpReferencesProductionHistory.RowStyles[0] = new RowStyle(SizeType.Absolute, 50);
                tlpReferencesProductionHistory.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesProductionHistory.RowStyles[2] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesProductionHistory.RowStyles[3] = new RowStyle(SizeType.Percent, 100);

                tlpReferencesUsageRecord.RowStyles[0] = new RowStyle(SizeType.Absolute, 50);
                tlpReferencesUsageRecord.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesUsageRecord.RowStyles[2] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesUsageRecord.RowStyles[3] = new RowStyle(SizeType.Percent, 100);

            }
            else if(buttonNo == 1)
            {
                dgvMonthlyBalanceEstimate.Visible = true;
                btnLoadMonthlyEstimateBalance.Visible = false;


                tlpReferencesMonthlyBalEstimate.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesMonthlyBalEstimate.RowStyles[1] = new RowStyle(SizeType.Absolute, 30);
                tlpReferencesMonthlyBalEstimate.RowStyles[2] = new RowStyle(SizeType.Percent, 100);
                tlpReferencesMonthlyBalEstimate.RowStyles[3] = new RowStyle(SizeType.Absolute, 0);

            }
            else if (buttonNo == 2)
            {
                dgvProductionHistory.Visible = true;
                btnLoadProductionHistory.Visible = false;

                tlpReferencesProductionHistory.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesProductionHistory.RowStyles[1] = new RowStyle(SizeType.Absolute, 30);
                tlpReferencesProductionHistory.RowStyles[2] = new RowStyle(SizeType.Percent, 100);
                tlpReferencesProductionHistory.RowStyles[3] = new RowStyle(SizeType.Absolute, 0);

            }
            else if (buttonNo == 3)
            {
                dgvUsageRecord.Visible = true;
                btnLoadUsageRecord.Visible = false;

                tlpReferencesUsageRecord.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
                tlpReferencesUsageRecord.RowStyles[1] = new RowStyle(SizeType.Absolute, 30);
                tlpReferencesUsageRecord.RowStyles[2] = new RowStyle(SizeType.Percent, 100);
                tlpReferencesUsageRecord.RowStyles[3] = new RowStyle(SizeType.Absolute, 0);

            }
        }

        private void InitialSetting()
        {
            loadItemCategoryData(ITEM_TYPE);

            InitialNameTextBox();
            ReferenceUIUpdate(0);

        }

        #endregion

        #region Data Loading

        private DataTable RemoveSelectedItem(DataTable dt)
        {
            List<string> itemsToRemove = new List<string>();

            //remove terminated item
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

            //remove selected item
            if(DT_SELECTED_ITEM?.Rows.Count > 0)
            foreach (DataRow row in DT_SELECTED_ITEM.Rows)
            {
                string itemCode = row[text.Header_ItemCode].ToString();
                itemsToRemove.Add(itemCode);
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

            return dt;
        }

        private void InitialNameTextBox()
        {
            if(!CAT_NAME_DATA_LOADING)
            {
                CAT_NAME_DATA_LOADING = true;

                string category = cmbCategory.Text;

                if (!string.IsNullOrEmpty(category))
                {
                    DT_ITEM_SOURCE = dalItem.CatSearch(category);

                    DT_ITEM_SOURCE = RemoveSelectedItem(DT_ITEM_SOURCE);

                    DataTable dt = DT_ITEM_SOURCE.Copy();

                    dt = DT_ITEM_SOURCE.DefaultView.ToTable(true, "item_name");
                    dt.DefaultView.Sort = "item_name ASC";

                    string[] stringArray = new string[DT_ITEM_SOURCE.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        stringArray[i] = dt.Rows[i][0].ToString();
                    }

                    txtPartName.Values = stringArray;
                }

                CAT_NAME_DATA_LOADING = false;
            }
           
        }

        #endregion

        private void frmItemSearch_Shown(object sender, EventArgs e)
        {
            txtPartName.Focus();
            FORM_LOADED = true;
        }

        private void txtPartName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                //cmbPartCode.DroppedDown = true;
                //cmbPartCode.Focus();

                //CMB_CODE_READY = true;

            }
        }

        private void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }

        }
        private void btnCancelPartInfoEdit_Click(object sender, EventArgs e)
        {
            ITEM_CODE_SELECTED = "";

            Close();
        }

        private void btnMouldSelected_Click(object sender, EventArgs e)
        {
            ITEM_CODE_SELECTED = cmbPartCode.Text;
            JOB_TARGET_QTY = int.TryParse(txtTargetQty.Text, out int i) ? i : 0;

            Close();
        }

        bool CAT_NAME_DATA_LOADING = false;
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITEM_TYPE = cmbCategory.Text;

            if(!string.IsNullOrEmpty(ITEM_TYPE))
            {
                lblItemSelectionTitle.Text = "Item (" + ITEM_TYPE +") Selection";
            }
            else
            {
                lblItemSelectionTitle.Text = "Item Selection";

            }

            InitialNameTextBox();

            ShowPurposeAndTargetQtyUI(ITEM_TYPE == text.Cat_Part);
        }

        private void dgvMonthlyBalanceEstimate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
      //      if (e.ColumnIndex == dgvMonthlyBalanceEstimate.Columns[text.Header_Selection].Index &&
      //e.RowIndex >= 0 &&
      //dgvMonthlyBalanceEstimate.Rows[e.RowIndex].Cells[text.Header_Description].Value.ToString().Contains(text.Header_ReadyStock) == true)
      //      {
      //          e.PaintBackground(e.CellBounds, true);
      //          e.Handled = true;
      //      }
        }

        private void txtJobPurpose_Enter(object sender, EventArgs e)
        {
            if (txtJobPurpose.Text == JOB_PURPOSE_INITIAL_CONTENT)
            {
                txtJobPurpose.Text = "";
               
            }
            txtJobPurpose.ForeColor = SystemColors.WindowText;
        }

        private void txtJobPurpose_Leave(object sender, EventArgs e)
        {
            if (txtJobPurpose.Text.Length == 0)
            {
                txtJobPurpose.Text = JOB_PURPOSE_INITIAL_CONTENT;
                txtJobPurpose.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtJobPurpose_TextChanged(object sender, EventArgs e)
        {
           
        }

        private string JOB_PURPOSE_MONTH = "";

        private bool JOB_PURPOSE_UPDATING = false;

        private void PurposeUpdate()
        {

            if(JOB_PURPOSE_UPDATING)
            {
                return;
            }

            JOB_PURPOSE_UPDATING = true;

            JOB_PURPOSE = txtJobPurpose.Text;
            JOB_PURPOSE_MONTH = "";

            if (JOB_PURPOSE == JOB_PURPOSE_INITIAL_CONTENT)
            {
                JOB_PURPOSE = "";
            }

            JOB_PURPOSE = JOB_PURPOSE.Replace(" /" + cbPurposeMonth1.Text, "");
            JOB_PURPOSE = JOB_PURPOSE.Replace(cbPurposeMonth1.Text, "");

            JOB_PURPOSE = JOB_PURPOSE.Replace(" /" + cbPurposeMonth2.Text, "");
            JOB_PURPOSE = JOB_PURPOSE.Replace(cbPurposeMonth2.Text, "");

            JOB_PURPOSE = JOB_PURPOSE.Replace(" /" + cbPurposeMonth3.Text, "");
            JOB_PURPOSE = JOB_PURPOSE.Replace(cbPurposeMonth3.Text, "");

            if (cbPurposeMonth1.Checked)
            {
                if (JOB_PURPOSE_MONTH.Equals(""))
                {
                    JOB_PURPOSE_MONTH = cbPurposeMonth1.Text;
                }
                else
                {
                    JOB_PURPOSE_MONTH += " /" + cbPurposeMonth1.Text;
                }
            }

            if (cbPurposeMonth2.Checked)
            {

                if (JOB_PURPOSE_MONTH.Equals(""))
                {
                    JOB_PURPOSE_MONTH = cbPurposeMonth2.Text;
                }
                else
                {
                    JOB_PURPOSE_MONTH += " /" + cbPurposeMonth2.Text;
                }
            }

            if (cbPurposeMonth3.Checked)
            {
                if (JOB_PURPOSE_MONTH.Equals(""))
                {
                    JOB_PURPOSE_MONTH = cbPurposeMonth3.Text.Replace("20", "");
                }
                else
                {
                    JOB_PURPOSE_MONTH += " /" + cbPurposeMonth3.Text.Replace("20", "");

                }
            }
           

            if (!JOB_PURPOSE_MONTH.Equals(""))
            {
                if (JOB_PURPOSE.Equals(""))
                {
                    JOB_PURPOSE = JOB_PURPOSE_MONTH;
                }
                else
                {
                    JOB_PURPOSE += " /" + JOB_PURPOSE_MONTH;
                }
            }

            if(string.IsNullOrEmpty(JOB_PURPOSE))
            {
                txtJobPurpose.Text = JOB_PURPOSE_INITIAL_CONTENT;
                txtJobPurpose.ForeColor = SystemColors.GrayText;
            }
            else
            {
                txtJobPurpose.Text = JOB_PURPOSE;
                txtJobPurpose.ForeColor = SystemColors.WindowText;

            }

            JOB_PURPOSE_UPDATING = false;

        }

        private bool FORM_LOADED = false;

        private void JobPurposeSettingReset()
        {
            JOB_PURPOSE_UPDATING = true;
            cbPurposeMonth1.Checked = false;
            cbPurposeMonth2.Checked = false;
            cbPurposeMonth3.Checked = false;
            txtJobPurpose.Text = JOB_PURPOSE_INITIAL_CONTENT;

            JOB_PURPOSE = "";

            txtJobPurpose.ForeColor = SystemColors.GrayText;
            JOB_PURPOSE_UPDATING = false;


        }

        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReferenceUIUpdate(0);


            JobPurposeSettingReset();
        }

        private void cbOtherPurpose_CheckedChanged(object sender, EventArgs e)
        {
            PurposeUpdate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            FORM_LOADED = false;
            string itemCode = cmbPartCode.Text;
            string itemType = cmbCategory.Text;

            dgvMonthlyBalanceEstimate.DataSource = null; 

            if (!string.IsNullOrEmpty(itemCode) && itemType == text.Cat_Part)
            {
                LoadMonthlyBalanceEstimate();
            }
            

            FORM_LOADED = true;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnLoadMonthlyEstimateBalance_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            FORM_LOADED = false;
            string itemCode = cmbPartCode.Text;
            string itemType = cmbCategory.Text;

            dgvMonthlyBalanceEstimate.DataSource = null;

            if (!string.IsNullOrEmpty(itemCode) && itemType == text.Cat_Part)
            {
                LoadMonthlyBalanceEstimate();

                ReferenceUIUpdate(1);
            }
            else
            {
                if(string.IsNullOrEmpty(itemCode))
                {
                    MessageBox.Show("Item Code not found!");

                }
                else if(itemType != text.Cat_Part)
                {
                    MessageBox.Show("This function is temporarily available for part items only.");
                }
            }
           


            FORM_LOADED = true;

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void btnLoadProductionHistory_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            FORM_LOADED = false;
            string itemCode = cmbPartCode.Text;
            string itemType = cmbCategory.Text;

            dgvMonthlyBalanceEstimate.DataSource = null;

            if (!string.IsNullOrEmpty(itemCode) && itemType == text.Cat_Part)
            {
                //LoadMonthlyBalanceEstimate();
                ReferenceUIUpdate(2);
            }
            else
            {
                if (string.IsNullOrEmpty(itemCode))
                {
                    MessageBox.Show("Item Code not found!");

                }
                else if (itemType != text.Cat_Part)
                {
                    MessageBox.Show("This function is temporarily available for part items only.");
                }
            }
            


            FORM_LOADED = true;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvUsageRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            FORM_LOADED = false;
            string itemCode = cmbPartCode.Text;
            string itemType = cmbCategory.Text;

            dgvMonthlyBalanceEstimate.DataSource = null;

            if (!string.IsNullOrEmpty(itemCode) && itemType == text.Cat_Part)
            {
                //LoadMonthlyBalanceEstimate();
                ReferenceUIUpdate(3);

            }
            else
            {
                if (string.IsNullOrEmpty(itemCode))
                {
                    MessageBox.Show("Item Code not found!");

                }
                else if (itemType != text.Cat_Part)
                {
                    MessageBox.Show("This function is temporarily available for part items only.");
                }
            }


            FORM_LOADED = true;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvProductionHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbPurposeMonth2_CheckedChanged(object sender, EventArgs e)
        {
            PurposeUpdate();
        }

        private void cbPurposeMonth3_CheckedChanged(object sender, EventArgs e)
        {
            PurposeUpdate();
        }

        private void txtTargetQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnMouldSelected_Click(sender, e);
            }
        }

        private void dgvMonthlyBalanceEstimate_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMonthlyBalanceEstimate;

            string colName = dgv.Columns[e.ColumnIndex].Name;

            if(colName == text.Header_BalStock)
            {
                decimal num = decimal.TryParse(dgv.Rows[e.RowIndex].Cells[text.Header_BalStock].Value.ToString(), out num) ? num : 0;

                if (num < 0)
                {
                    dgv.Rows[e.RowIndex].Cells[text.Header_BalStock].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[e.RowIndex].Cells[text.Header_BalStock].Style.ForeColor = Color.Black;
                }
            }
            
        }
    }
}
