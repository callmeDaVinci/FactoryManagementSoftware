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

        private readonly string OTHER_INITIAL_CONTENT = "Fill in other planning purposes here.";
        private readonly string OTHER_PURPOSE_INITIAL = "OTHER PURPOSE";
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
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            int smallColumnWidth = 60;


            dgv.Columns[text.Header_Description].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_Description].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[text.Header_BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_Selection].Width = smallColumnWidth;

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
                    MonthlyEstimateBalanceCellFormatting(dgvMonthlyBalanceEstimate);
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


                    foreach (DataColumn col in dt.Columns)
                    {
                        string colName = col.ColumnName;


                        bool isNumColumn = colName == text.Header_BalStock;
                     

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

        private void ShowPurposeAndTargetQtyUI(bool show)
        {
            if(show)
            {
                //form side
                Size = new Size(1000, 700);

                //column
                tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 400);
                tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);

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


        private void InitialSetting()
        {
            loadItemCategoryData(ITEM_TYPE);

            InitialNameTextBox();

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

            getPurpose();

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
            if (e.ColumnIndex == dgvMonthlyBalanceEstimate.Columns[text.Header_Selection].Index &&
      e.RowIndex >= 0 &&
      dgvMonthlyBalanceEstimate.Rows[e.RowIndex].Cells[text.Header_Description].Value.ToString().Contains(text.Header_ReadyStock) == true)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Handled = true;
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

                if (cbOtherPurpose.Checked)
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

        private bool getPurpose()
        {
            JOB_PURPOSE = "";

            if (cbOtherPurpose.Checked)
            {
                string purpose = txtOtherPurpose.Text;

                if (string.IsNullOrEmpty(purpose) || purpose == OTHER_INITIAL_CONTENT)
                {
                    JOB_PURPOSE = OTHER_PURPOSE_INITIAL;
                }
                else
                {
                    JOB_PURPOSE = purpose;
                }
            }

            else if (dgvMonthlyBalanceEstimate?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvMonthlyBalanceEstimate.DataSource;

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

            if (string.IsNullOrEmpty(JOB_PURPOSE))
            {
                MessageBox.Show("Please select at least one purpose.");
                return false;
            }

            return true;

        }

        private bool FORM_LOADED = false;
        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FORM_LOADED)
            {
                timer1.Stop();
                timer1.Start();
            }

          
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
    }
}
