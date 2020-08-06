using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBDeliveredReport : Form
    {
        public frmSBBDeliveredReport()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            LoadDateTypeCMBData(cmbDateType);
            LoadMonthDataToCMB(cmbMonthFrom,1);
            LoadYearDataToCMB(cmbYearFrom,2020);
            LoadMonthDataToCMB(cmbMonthTo, DateTime.Now.Month);
            LoadYearDataToCMB(cmbYearTo, DateTime.Now.Year);
        }

        #region Initital Data Setting

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();

        Text text = new Text();
        Tool tool = new Tool();

        readonly string text_Selected = " SELECTED";

        private readonly string text_MoreFilters = "SHOW FILTERS";
        private readonly string text_HideFilters = "HIDE FILTERS";

        private readonly string text_Year = "YEAR";
        private readonly string text_Month = "MONTH";

        private readonly string text_DateType = "DATE TYPE";
        private readonly string dateType_Daily = "DAILY";
        private readonly string dateType_Monthly = "MONTHLY";
        private readonly string dateType_Yearly = "YEARLY";

        private readonly string header_Index = "#";
        private readonly string header_ItemSize_Numerator = "SIZE_NUMERATOR";
        private readonly string header_ItemSize_Denominator = "SIZE_DENOMINATOR";
        private readonly string header_SizeUnit = "SIZE UNIT";
        private readonly string header_ItemType = "ITEM TYPE";
        private readonly string header_ItemCode = "ITEM CODE";
        private readonly string header_ItemString = "ITEM";

        private readonly string header_StdPacking = "STD PACKING";

        private readonly string header_CustID = "CUST ID";
        private readonly string header_CustShortName = "CUSTOMER";

        private readonly string header_TotalPcs = "TOTAL PCS";
        private readonly string header_TotalString = "TOTAL";

        private readonly string header_DeliveredPcs = "_DELIVERED PCS";
        private readonly string header_DeliveredBag = "_DELIVERED BAG";

        private bool Loaded = false;

        private DataTable NewDeliveredTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemSize_Numerator, typeof(int));
            dt.Columns.Add(header_ItemSize_Denominator, typeof(int));
            dt.Columns.Add(header_SizeUnit, typeof(string));
            dt.Columns.Add(header_ItemType, typeof(string));
            dt.Columns.Add(header_ItemString, typeof(string));

            dt.Columns.Add(header_StdPacking, typeof(int));

            dt.Columns.Add(header_CustID, typeof(int));
            dt.Columns.Add(header_CustShortName, typeof(string));

            dt.Columns.Add(header_TotalPcs, typeof(int));
            dt.Columns.Add(header_TotalString, typeof(string));

            string dateType = cmbDateType.Text;

            if (dateType == dateType_Daily)
            {
                string day = null;
                DateTime start = dtpDateFrom.Value.Date;
                DateTime end = dtpDateTo.Value.Date;

                if (end < start)
                {
                    MessageBox.Show("Date invalid.\nPlease pick a correct date period.");
                }
                else
                {
                    for (DateTime current = start; current <= end; current = current.AddDays(1))
                    {
                        Console.WriteLine(current.Day);
                        day = current.ToString("dd/MM yy");
                        dt.Columns.Add(day, typeof(string));
                        dt.Columns.Add(day + header_DeliveredPcs, typeof(int));
                        dt.Columns.Add(day + header_DeliveredBag, typeof(int));
                    }
                }
            }

            else if (dateType == dateType_Monthly)
            {
                string month = null;

                int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                int monthEnd = int.TryParse(cmbMonthTo.Text, out monthEnd) ? monthEnd : monthStart;

                int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                int yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                for (int i = yearStart; i <= yearEnd; i++)
                {
                    if (yearStart == yearEnd)
                    {
                        for (int j = monthStart; j <= monthEnd; j++)
                        {
                            month = j + "/" + i;
                            dt.Columns.Add(month, typeof(string));
                            dt.Columns.Add(month + header_DeliveredPcs, typeof(int));
                            dt.Columns.Add(month + header_DeliveredBag, typeof(int));
                        }
                    }
                    else
                    {
                        if (i == yearStart)
                        {
                            for (int j = monthStart; j <= 12; j++)
                            {
                                month = j + "/" + i;
                                dt.Columns.Add(month, typeof(string));
                                dt.Columns.Add(month + header_DeliveredPcs, typeof(int));
                                dt.Columns.Add(month + header_DeliveredBag, typeof(int));
                            }
                        }
                        else if (i == yearEnd)
                        {
                            for (int j = 1; j <= monthEnd; j++)
                            {
                                month = j + "/" + i;
                                dt.Columns.Add(month, typeof(string));
                                dt.Columns.Add(month + header_DeliveredPcs, typeof(int));
                                dt.Columns.Add(month + header_DeliveredBag, typeof(int));
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                month = j + "/" + i;
                                dt.Columns.Add(month, typeof(string));
                                dt.Columns.Add(month + header_DeliveredPcs, typeof(int));
                                dt.Columns.Add(month + header_DeliveredBag, typeof(int));
                            }
                        }
                    }
                }
            }

            else if (dateType == dateType_Yearly)
            {
                int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                int yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                for (int i = yearStart; i <= yearEnd; i++)
                {
                    dt.Columns.Add(i.ToString(), typeof(string));
                    dt.Columns.Add(i.ToString() + header_DeliveredPcs, typeof(int));
                    dt.Columns.Add(i.ToString() + header_DeliveredBag, typeof(int));
                }
            }

            return dt;
        }

        private void LoadDateTypeCMBData(ComboBox cmb)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text_DateType);

            dt.Rows.Add(dateType_Daily);
            dt.Rows.Add(dateType_Monthly);
            dt.Rows.Add(dateType_Yearly);

            cmb.DataSource = dt;
            cmb.DisplayMember = text_DateType;
            cmb.SelectedIndex = 1;
        }

        private void DatePeriodSet()
        {
            if (Loaded)
            {
                #region get start and end date

                string dateType = cmbDateType.Text;

                DateTime start;
                DateTime end;
                int monthStart = -1, monthEnd = -1, yearStart = -1, yearEnd = -1;

                if (dateType == dateType_Daily || dateType == dateType_Monthly)
                {
                    monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                    monthEnd = int.TryParse(cmbMonthTo.Text, out monthEnd) ? monthEnd : monthStart;

                    yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    start = new DateTime(yearStart, monthStart, 1);
                    end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd));

                    if (end < start)
                    {
                        //DateFrom = end.ToString("yyyy/MM/dd");
                        //DateTo = start.ToString("yyyy/MM/dd");

                        start = new DateTime(yearEnd, monthEnd, 1);
                        end = new DateTime(yearStart, monthStart, DateTime.DaysInMonth(yearStart, monthStart));



                    }

                    dtpDateFrom.Value = start;
                    dtpDateTo.Value = end;
                }

                else if (dateType == dateType_Yearly)
                {
                    yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    if(yearStart > yearEnd)
                    {
                        int temp = yearStart;
                        yearStart = yearEnd;
                        yearEnd = yearStart;
                    }

                    start = new DateTime(yearStart, 1, 1);
                    end = new DateTime(yearEnd, 12, 31);

                    dtpDateFrom.Value = start;
                    dtpDateTo.Value = end;
                }
                else
                {
                    MessageBox.Show("Date invalid!");
                    return;
                }
                #endregion

            
            }

        }

        private void LoadMonthDataToCMB(ComboBox cmb)
        {
            DataTable dt_month = new DataTable();

            dt_month.Columns.Add(text_Month);

            for (int i = 1; i <= 12; i++)
            {
                dt_month.Rows.Add(i);
            }

            cmb.DataSource = dt_month;
            cmb.DisplayMember = text_Month;
            cmb.SelectedIndex = -1;
        }

        private void LoadMonthDataToCMB(ComboBox cmb, int InitialMonth)
        {
            DataTable dt_month = new DataTable();

            dt_month.Columns.Add(text_Month);

            for (int i = 1; i <= 12; i++)
            {
                dt_month.Rows.Add(i);
            }

            cmb.DataSource = dt_month;
            cmb.DisplayMember = text_Month;
            cmb.SelectedIndex = InitialMonth - 1;
        }

        private void LoadYearDataToCMB(ComboBox cmb)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add(text_Year);

            for (int i = DateTime.Now.Year; i >= 2020; i--)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = text_Year;
            cmb.SelectedIndex = -1;
        }

        private void LoadYearDataToCMB(ComboBox cmb, int initialYear)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add(text_Year);

            for (int i = 2020; i <= DateTime.Now.Year; i++)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = text_Year;
            cmb.Text = initialYear.ToString();
        }

        #endregion

        #region Form Load/ Closed

        private void frmSBBDeliveredReport_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();
            Loaded = true;

            DatePeriodSet();
        }

        private void frmSBBDeliveredReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.SBBDeliveredFormOpen = false;
        }

        #endregion

        #region Date Changed Action

        private void cmbMonthFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePeriodSet();
        }

        private void cmbYearFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePeriodSet();
        }

        private void cmbMonthTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePeriodSet();
        }

        private void cmbYearTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePeriodSet();
        }

        #endregion

        #region Load Data

        private void ShowOrHideFilter()
        {
            DataGridView dgv = dgvList;

            dgv.SuspendLayout();

            string text = btnFilter.Text;

            if (text == text_MoreFilters)
            {
                tlpReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 175f);

                dgv.ResumeLayout();

                btnFilter.Text = text_HideFilters;
            }
            else if (text == text_HideFilters)
            {
                tlpReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgv.ResumeLayout();

                btnFilter.Text = text_MoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDeliveredData();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void ClearAllErrorSign()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
        }

        private bool Validation()
        {
            ClearAllErrorSign();

            bool passed = true;
         
            if (cmbDateType.SelectedIndex == -1)
            {
                errorProvider1.SetError(lblDateType, "Please select a date type.");

                passed = false;
            }

         
            return passed;
        }

        private void LoadDeliveredData()
        {
            lblTotalBag.Text = 0 + " BAG(s) / " + 0 + " PCS " + text_Selected;
            if (Validation())
            {
                #region Pre Setting

                frmLoading.ShowLoadingScreen();

                DataGridView dgv = dgvList;
                DataTable dt_DeliveredReport = NewDeliveredTable();
                //DataTable dt_ItemList;
                //DataTable dt_TrfHist;

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                string dateType = cmbDateType.Text;
                //string itemSize = cmbItemSize.Text;
                //string itemType = cmbItemType.Text;

                #endregion

                #region load item list

                //dt_ItemList = dalItemCust.SPPCustSearch(itemType);

                #endregion

                #region get start and end date

                string start = dtpDateFrom.Value.ToString("yyyy/MM/dd");
                string end = dtpDateTo.Value.ToString("yyyy/MM/dd");

                #endregion

                #region Load data from database

                string itemCust = text.SPP_BrandName;
                DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);

                if (cbSortByType.Checked)
                {
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }
                else
                {
                    dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }

                DataTable dt_Item = dalItem.Select();

                DataTable dt_DOList = dalSPP.DOWithTrfInfoSelect(start, end);


                int index = 1;
                #endregion

                #region load product list

                foreach (DataRow row in dt_Product.Rows)
                {
                    string itemCode = row[dalSPP.ItemCode].ToString();
                    string itemName = row[dalSPP.ItemName].ToString();
                    int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                    int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                    int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                    int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;
                    int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                    int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                    string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                    string typeName = row[dalSPP.TypeName].ToString();

                    int pcsStock = readyStock;
                    int bagStock = pcsStock / qtyPerBag;


                    string sizeString = "";
                    int size = 1;

                    if (denominator == 1)
                    {
                        size = numerator;
                        sizeString = numerator + " " + sizeUnit;
                    }
                    else
                    {
                        size = numerator / denominator;
                        sizeString = numerator + "/" + denominator + " " + sizeUnit;
                    }

                    DataRow newRow = dt_DeliveredReport.NewRow();

                    newRow[header_Index] = index++;

                    newRow[header_ItemCode] = itemCode;
                    newRow[header_ItemSize_Numerator] = numerator;
                    newRow[header_ItemSize_Denominator] = denominator;
                    newRow[header_SizeUnit] = sizeUnit;
                    newRow[header_ItemString] = sizeString + " " + typeName;

                    newRow[header_StdPacking] = qtyPerBag;

                    bool DOFound = false;

                    foreach(DataRow row_DO in dt_DOList.Rows)
                    {
                        if(row_DO[dalSPP.ItemCode].ToString() == itemCode)
                        {
                            int custID = int.TryParse(row_DO[dalSPP.CustomerTableCode].ToString(), out custID) ? custID : -1;
                            string shortName = row_DO[dalSPP.ShortName].ToString();

                            DateTime trfDate = DateTime.TryParse(row_DO[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                            int deliveredPcs = int.TryParse(row_DO[dalTrfHist.TrfQty].ToString(), out deliveredPcs) ? deliveredPcs : 0;

                            if(dt_DeliveredReport.Rows.Count > 0)
                            {
                                bool custFound = false;

                                foreach(DataRow delivered_Row in dt_DeliveredReport.Rows)
                                {
                                    if (delivered_Row[header_ItemCode].ToString() == itemCode && delivered_Row[header_CustID].ToString() == custID.ToString())
                                    {
                                        //get delivered pcs qty
                                        string dateHeaderName = GetDateHeaderName(trfDate);

                                        int oldDeliveredPcs = int.TryParse(delivered_Row[dateHeaderName + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                                        deliveredPcs += oldDeliveredPcs;

                                        int deliveredBag = deliveredPcs / qtyPerBag;

                                        newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                        newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                        newRow[dateHeaderName] = deliveredBag + " BAGS ("+deliveredPcs+")";

                                        DOFound = true;
                                        custFound = true;
                                        break;
                                    }
                                }

                                if(!custFound)
                                {
                                    newRow = dt_DeliveredReport.NewRow();

                                    newRow[header_Index] = index++;

                                    newRow[header_ItemCode] = itemCode;
                                    newRow[header_ItemSize_Numerator] = numerator;
                                    newRow[header_ItemSize_Denominator] = denominator;
                                    newRow[header_SizeUnit] = sizeUnit;
                                    newRow[header_ItemString] = sizeString + " " + typeName;

                                    newRow[header_StdPacking] = qtyPerBag;

                                    newRow[header_CustID] = custID;
                                    newRow[header_CustShortName] = shortName;

                                    string dateHeaderName = GetDateHeaderName(trfDate);

                                    int deliveredBag = deliveredPcs / qtyPerBag;

                                    newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                    newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                    newRow[dateHeaderName] = deliveredBag + " BAGS (" + deliveredPcs + ")";

                                    dt_DeliveredReport.Rows.Add(newRow);
                                    DOFound = true;
                                }
                            }
                            else
                            {
                                // insert first data of the table
                                newRow[header_CustID] = custID;
                                newRow[header_CustShortName] = shortName;

                                string dateHeaderName = GetDateHeaderName(trfDate);

                                int deliveredBag = deliveredPcs / qtyPerBag;

                                newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                newRow[dateHeaderName] = deliveredBag + " BAGS (" + deliveredPcs + ")";

                                dt_DeliveredReport.Rows.Add(newRow);
                                DOFound = true;
                            }
                        }

                    }

                    
                    if(!DOFound)
                    {
                        dt_DeliveredReport.Rows.Add(newRow);
                    }


                }

                #endregion


                #region print data

                //int index = 1;
                //int qtyPerBag = 0;
                //int deliveredPcsQty = 0;
                //int totalDeliveredPcsQty = 0;

                //bool dataPrinted = false;

                //bool itemFound = false;


                //TO-DO:

                
                #endregion

                if(cbSortByCustomer.Checked)
                {
                    dt_DeliveredReport.DefaultView.Sort = header_CustShortName + " DESC";

                    dt_DeliveredReport = dt_DeliveredReport.DefaultView.ToTable();


                }

                ReallocateIndex(dt_DeliveredReport);

                dgv.DataSource = dt_DeliveredReport;
                DgvUIEdit(dgv);

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

                dgv.ClearSelection();
                frmLoading.CloseForm();
            }
        }

        private void ReallocateIndex(DataTable dt)
        {
            int index = 1;
            foreach(DataRow row in dt.Rows)
            {
                row[header_Index] = index++;
            }
        }

        private string GetDateHeaderName(DateTime trfDate)
        {
            string headerName = "";

            string dateType = cmbDateType.Text;

            if (dateType == dateType_Daily)
            {
                headerName = trfDate.ToString("dd/MM yy");
            }

            else if (dateType == dateType_Monthly)
            {
                headerName = trfDate.Month + "/" + trfDate.Year;
            }

            else if (dateType == dateType_Yearly)
            {
                headerName = trfDate.Year.ToString();
            }

            return headerName;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_ItemCode].Visible = false;
            dgv.Columns[header_ItemSize_Numerator].Visible = false;
            dgv.Columns[header_ItemSize_Denominator].Visible = false;
            dgv.Columns[header_SizeUnit].Visible = false;
            dgv.Columns[header_ItemType].Visible = false;

            dgv.Columns[header_StdPacking].Visible = false;

            dgv.Columns[header_CustID].Visible = false;

            dgv.Columns[header_TotalPcs].Visible = false;

            DataTable dt = (DataTable)dgv.DataSource;

            for(int i = 0; i < dt.Columns.Count; i++)
            {
                string colName = dgv.Columns[i].Name;

                if(colName.Contains(header_DeliveredPcs))
                {
                    dgv.Columns[i].Visible = false;
                }
                else if (colName.Contains(header_DeliveredBag))
                {
                    dgv.Columns[i].Visible = false;
                }
            }

            dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_CustShortName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_Code].DefaultCellStyle.ForeColor = Color.Gray;
            //dgv.Columns[header_Code].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            //dgv.Columns[header_Route].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            //dgv.Columns[header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.Columns[header_Total].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            //dgv.Columns[header_Bags].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_TripNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_StopNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_DeliveryDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Route].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;


        }
        #endregion

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDeliveredData();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void cmbDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePeriodSet();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvList;

            if (e.RowIndex != -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv.Rows[row].Cells[col].Selected = true;
            }
        }

        private void dgvList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                DataGridView dgv = dgvList;
                int col = e.ColumnIndex;

                dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                dgv.Columns[col].Selected = true;

                CalculateTotalBag(dgv);
            }
        }

        private void dgvList_MouseClick(object sender, MouseEventArgs e)
        {
            CalculateTotalBag(dgvList);
        }

        private void CalculateTotalBag(DataGridView dgv)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            int totalBag = 0;
            int totalPcs = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_StdPacking].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        bool colSelected = false;

                        if (colName.Contains(header_DeliveredPcs))
                            colSelected = dgv.Rows[i].Cells[j - 1].Selected;

                        if (colSelected)
                        {
                            int deliveredPcs = int.TryParse(dt.Rows[i][j].ToString(), out deliveredPcs) ? deliveredPcs : 0;
                            int deliveredBag = int.TryParse(dt.Rows[i][j + 1].ToString(), out deliveredBag) ? deliveredBag : 0;

                            if (deliveredPcs > 0)
                            {
                                totalBag += deliveredBag;
                                totalPcs += deliveredPcs;
                            }
                        }

                    }
                }
            }

            lblTotalBag.Text = totalBag + " BAG(s) / " + totalPcs + " PCS " + text_Selected;

        }

        private void cbSortBySize_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSortBySize.Checked)
            {
                cbSortByType.Checked = false;
            }
            else
            {
                cbSortByType.Checked = true;
            }
        }

        private void cbSortByType_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSortByType.Checked)
            {
                cbSortBySize.Checked = false;
            }
            else
            {
                cbSortBySize.Checked = true;
            }
        }
    }
}
