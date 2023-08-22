using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBDeliveredReport : Form
    {
        public frmSBBDeliveredReport()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            LoadDateTypeCMBData(cmbReportType);
            LoadMonthDataToCMB(cmbMonthFrom, DateTime.Now.Month);
            LoadYearDataToCMB(cmbYearFrom,DateTime.Now.Year);
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
        private readonly string dateType_Sales = "SALES";

        private readonly string header_Index = "#";
        private readonly string header_ItemSize_Numerator = "SIZE_NUMERATOR";
        private readonly string header_ItemSize_Denominator = "SIZE_DENOMINATOR";
        private readonly string header_SizeUnit = "SIZE UNIT";
        private readonly string header_ItemSizeString = "SIZE";
        private readonly string header_UnitPrice = "UNIT PRICE";


        private readonly string header_ItemType = "ITEM TYPE";
        private readonly string header_ItemCode = "ITEM CODE";
        private readonly string header_ItemName = "ITEM NAME";

        private readonly string header_ItemString = "ITEM";

        private readonly string header_StdPacking = "STD PACKING";

        private readonly string header_CustID = "CUST ID";
        private readonly string header_CustShortName = "CUSTOMER";

        private readonly string header_TotalBag = "TOTAL BAGS";
        private readonly string header_TotalPcs = "TOTAL PCS";
        private readonly string header_TotalSales = "TOTAL SALES (RM)";

        private readonly string header_TotalString = "TOTAL";

        private readonly string header_DeliveredPcs = "_DELIVERED PCS";
        private readonly string header_DeliveredBag = "_DELIVERED BAG";
        private readonly string header_DeliveredSales = "_SALES";

        DateTime LastTimeOfUnlockSalesReport = DateTime.MaxValue;


        private bool Loaded = false;

        private DataTable NewDeliveredTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemSize_Numerator, typeof(int));
            dt.Columns.Add(header_ItemSize_Denominator, typeof(int));
            dt.Columns.Add(header_SizeUnit, typeof(string));
            dt.Columns.Add(header_ItemSizeString, typeof(string));
            dt.Columns.Add(header_ItemType, typeof(string));
            dt.Columns.Add(header_ItemString, typeof(string));

            dt.Columns.Add(header_StdPacking, typeof(int));

            dt.Columns.Add(header_CustID, typeof(int));
            dt.Columns.Add(header_CustShortName, typeof(string));

            dt.Columns.Add(header_TotalPcs, typeof(int));
            dt.Columns.Add(header_TotalBag, typeof(int));
            dt.Columns.Add(header_TotalString, typeof(string));

            string dateType = cmbReportType.Text;

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

        private DataTable NewSalesTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemSize_Numerator, typeof(int));
            dt.Columns.Add(header_ItemSize_Denominator, typeof(int));
            dt.Columns.Add(header_SizeUnit, typeof(string));
            dt.Columns.Add(header_ItemSizeString, typeof(string));
            dt.Columns.Add(header_ItemType, typeof(string));
            dt.Columns.Add(header_ItemString, typeof(string));
            dt.Columns.Add(header_UnitPrice, typeof(string));

            dt.Columns.Add(header_StdPacking, typeof(int));

            dt.Columns.Add(header_CustID, typeof(int));
            dt.Columns.Add(header_CustShortName, typeof(string));

            dt.Columns.Add(header_TotalSales, typeof(float));
            dt.Columns.Add(header_TotalPcs, typeof(int));
            dt.Columns.Add(header_TotalBag, typeof(int));
            dt.Columns.Add(header_TotalString, typeof(string));

            return dt;
        }

        private void LoadDateTypeCMBData(ComboBox cmb)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text_DateType);

            dt.Rows.Add(dateType_Daily);
            dt.Rows.Add(dateType_Monthly);
            dt.Rows.Add(dateType_Yearly);


            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_FOUR)
            {
                dt.Rows.Add(dateType_Sales);
            }


            cmb.DataSource = dt;
            cmb.DisplayMember = text_DateType;
            cmb.SelectedIndex = 1;
        }

        private void DatePeriodSet()
        {
            if (Loaded)
            {
                #region get start and end date

                string dateType = cmbReportType.Text;

                DateTime start;
                DateTime end;

                int monthStart = -1, monthEnd = -1, yearStart = -1, yearEnd = -1;

                if (dateType == dateType_Sales)
                {
                    cbDeliveredUnitInSales.Visible = true;

                    cbDeliveredUnitInSales.Visible = true;

                    

                    cbMergeCustomer.Checked = false;
                    cbMergeCustomer.Visible = false;

                    cbSortByCustomer.Checked = false;
                    cbSortByCustomer.Visible = false;

                }
                else
                {
                    cbDeliveredUnitInSales.Visible = false;

                    cbMergeItem.Checked = true;

                    cbMergeCustomer.Checked = false;
                    cbMergeCustomer.Visible = true;

                    cbSortByCustomer.Checked = false;
                    cbSortByCustomer.Visible = true;

                }

                if (dateType == dateType_Daily || dateType == dateType_Monthly || dateType == dateType_Sales)
                {
                    

                    monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                    monthEnd = int.TryParse(cmbMonthTo.Text, out monthEnd) ? monthEnd : monthStart;

                    yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    start = new DateTime(yearStart, monthStart, 1);
                    end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd));

                    if (end < start)
                    {

                        int tmp = monthEnd;

                        monthEnd = monthStart;
                        monthStart = tmp;

                        tmp = yearEnd;
                        yearEnd = yearStart;
                        yearStart = tmp;

                        start = new DateTime(yearStart, monthStart, 1);
                        end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd));

                      
                    }

                    if (cb23to22.Checked)
                    {

                        monthStart--;

                        if (monthStart == 0)
                        {
                            monthStart = 12;
                            yearStart--;

                        }

                        start = new DateTime(yearStart, monthStart, 23);
                        end = new DateTime(yearEnd, monthEnd, 22);
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

                    if(cb23to22.Checked)
                    {
                        yearStart -= 1;

                        start = new DateTime(yearStart, 12, 23);
                        end = new DateTime(yearEnd, 12, 22);
                    }

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

            if (cmbReportType.Text.Equals(dateType_Sales))
            {
                for (int i = DateTime.Now.Year; i >= 2021; i--)
                {
                    dt_year.Rows.Add(i);
                }
            }
            else
            {
                for (int i = DateTime.Now.Year; i >= 2020; i--)
                {
                    dt_year.Rows.Add(i);
                }
            }

            

            cmb.DataSource = dt_year;
            cmb.DisplayMember = text_Year;
            cmb.SelectedIndex = -1;
        }

        private void LoadYearDataToCMB(ComboBox cmb, int initialYear)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add(text_Year);

            if(cmbReportType.Text.Equals(dateType_Sales))
            {
                for (int i = 2021; i <= DateTime.Now.Year; i++)
                {
                    dt_year.Rows.Add(i);
                }
            }
            else
            {
                for (int i = 2020; i <= DateTime.Now.Year; i++)
                {
                    dt_year.Rows.Add(i);
                }
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
            cmbMonthTo.Text = cmbMonthFrom.Text;

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
            string ReportType = cmbReportType.Text;

            if (ReportType.Equals(dateType_Sales))
            {
                LoadSalesData();
            }
            else

                LoadDeliveredData();
            //try
            //{
            //    string ReportType = cmbReportType.Text;

            //    if(ReportType.Equals(dateType_Sales))
            //    {
            //        LoadSalesData();
            //    }
            //    else
            //    LoadDeliveredData();
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
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
         
            if (cmbReportType.SelectedIndex == -1)
            {
                errorProvider1.SetError(lblDateType, "Please select a report type.");

                return false;
            }

            if(!cbDeliveredQtyInPcs.Checked && !cbDeliveredQtyInBag.Checked && (!cbDeliveredUnitInSales.Checked || !cmbReportType.Text.Equals(dateType_Sales)))
            {
                MessageBox.Show("Please select at least one DELIVERY UNIT.");

                return false;

            }


            if (cmbReportType.Text.Equals(dateType_Sales))
            {
             
                int YearFrom = dtpDateFrom.Value.Year;
                int MonthFrom = dtpDateFrom.Value.Month;

                if(YearFrom <= 2021 && MonthFrom < 4 || YearFrom < 2021)
                {
                    MessageBox.Show("Sales Report only available from April 2021 onward.");

                    return false;

                }

                TimeSpan duration = DateTime.Now - LastTimeOfUnlockSalesReport;

                if (LastTimeOfUnlockSalesReport == DateTime.MaxValue || duration.Minutes >= 1)
                {
                    //key in password
                    frmVerification frm = new frmVerification(text.PW_UnlockSBBSalesReport)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };


                    frm.ShowDialog();

                    if (!frmVerification.PASSWORD_MATCHED)
                    {
                        return false;
                    }
                    else
                    {
                        LastTimeOfUnlockSalesReport = DateTime.Now;
                    }
                }


            }

           

            return true;
        }

        private void old_LoadDeliveredData()
        {
            dgvList.DataSource = null;

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
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                string dateType = cmbReportType.Text;
                //string itemSize = cmbItemSize.Text;
                //string itemType = cmbItemType.Text;

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
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC,"+ dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }
                else
                {
                    dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";
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
     
                    //if (denominator == 1)
                    //{
                    //    size = numerator;
                    //    sizeString = numerator + " " + sizeUnit;
                    //}
                    //else
                    //{
                    //    size = numerator / denominator;
                    //    sizeString = numerator + "/" + denominator + " " + sizeUnit;
                    //}

                    #region Get Size Data

                    int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                    int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                    string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                    string sizeString_1 = "";
                    string sizeString_2 = "";

                    int size_1 = 1;
                    int size_2 = 1;

                    if (denominator == 1)
                    {
                        size_1 = numerator;
                        sizeString_1 = numerator.ToString();

                    }
                    else
                    {
                        size_1 = numerator / denominator;
                        sizeString_1 = numerator + "/" + denominator;
                    }

                    if (numerator_2 > 0)
                    {
                        if (denominator_2 == 1)
                        {
                            sizeString_2 += numerator_2;
                            size_2 = numerator_2;

                        }
                        else
                        {
                            size_2 = numerator_2 / denominator_2;

                            if (numerator_2 == 3 && denominator_2 == 2)
                            {
                                sizeString_2 += "1 1" + "/" + denominator_2;
                            }
                            else
                            {
                                sizeString_2 += numerator_2 + "/" + denominator_2;
                            }



                        }
                    }

                    if (size_1 >= size_2)
                    {
                        if (sizeString_2 != "")
                            sizeString = sizeString_1 + " x " + sizeString_2;

                        else
                        {
                            sizeString = sizeString_1;
                        }
                    }
                    else
                    {

                        if (sizeString_2 != "")
                            sizeString = sizeString_2 + " x " + sizeString_1;

                        else
                        {
                            sizeString = sizeString_2;
                        }
                    }
                    #endregion

                    DataRow newRow = dt_DeliveredReport.NewRow();

                    newRow[header_Index] = index++;

                    newRow[header_ItemCode] = itemCode;
                    newRow[header_ItemSize_Numerator] = numerator;
                    newRow[header_ItemSize_Denominator] = denominator;
                    newRow[header_SizeUnit] = sizeUnit;
                    newRow[header_ItemSizeString] = sizeString;
                    newRow[header_ItemType] = typeName;
                    //newRow[header_ItemString] = sizeString + " " + typeName;
                    newRow[header_ItemString] = itemName;

                    newRow[header_StdPacking] = qtyPerBag;

                    bool DOFound = false;

                    foreach(DataRow row_DO in dt_DOList.Rows)
                    {
                        string trfResult = row_DO[dalTrfHist.TrfResult].ToString();

                        if (trfResult == "Passed" && row_DO[dalSPP.ItemCode].ToString() == itemCode)
                        {
                            int custID = int.TryParse(row_DO[dalSPP.CustTblCode].ToString(), out custID) ? custID : -1;
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

                                        string bagString = " BAGS (";

                                        if(deliveredBag <= 1)
                                        {
                                            bagString = " BAG (";
                                        }

                                        string DeliveredQtyString = "";

                                        if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                        {
                                            DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                        }
                                        else if (cbDeliveredQtyInPcs.Checked)
                                        {
                                            DeliveredQtyString = deliveredPcs.ToString();
                                        }
                                        else if (cbDeliveredQtyInBag.Checked)
                                        {
                                            DeliveredQtyString = deliveredBag.ToString();
                                        }

                                        newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                        newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                        newRow[dateHeaderName] = DeliveredQtyString;

                                        //newRow[dateHeaderName] = deliveredPcs;
                                        //newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";


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
                                    //newRow[header_ItemString] = sizeString + " " + typeName;
                                    newRow[header_ItemString] = itemName;
                                    newRow[header_ItemSizeString] = sizeString;
                                    newRow[header_ItemType] = typeName;
                                    newRow[header_StdPacking] = qtyPerBag;

                                    newRow[header_CustID] = custID;
                                    newRow[header_CustShortName] = shortName;

                                    string dateHeaderName = GetDateHeaderName(trfDate);

                                    int deliveredBag = deliveredPcs / qtyPerBag;

                                    string bagString = " Bags (";

                                    if (deliveredBag <= 1)
                                    {
                                        bagString = " Bag (";
                                    }

                                    string DeliveredQtyString = "";

                                    if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                    {
                                        DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                    }
                                    else if (cbDeliveredQtyInPcs.Checked)
                                    {
                                        DeliveredQtyString = deliveredPcs.ToString();
                                    }
                                    else if (cbDeliveredQtyInBag.Checked)
                                    {
                                        DeliveredQtyString = deliveredBag.ToString();
                                    }

                                    newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                    newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                    newRow[dateHeaderName] = DeliveredQtyString;

                                   // newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                   // newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                   //// newRow[dateHeaderName] = deliveredPcs;
                                   // newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";

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

                                string bagString = " Bags (";

                                if (deliveredBag <= 1)
                                {
                                    bagString = " Bag (";
                                }

                                string DeliveredQtyString = "";

                                if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                {
                                    DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                }
                                else if (cbDeliveredQtyInPcs.Checked)
                                {
                                    DeliveredQtyString = deliveredPcs.ToString();
                                }
                                else if (cbDeliveredQtyInBag.Checked)
                                {
                                    DeliveredQtyString = deliveredBag.ToString();
                                }

                                newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                newRow[dateHeaderName] = DeliveredQtyString;

                                //newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                //newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                ////newRow[dateHeaderName] = deliveredPcs;
                                //newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";

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

                if(cbMergeItem.Checked)
                {
                    dt_DeliveredReport = MergeSameItem(dt_DeliveredReport);
                }
                else if(cbMergeCustomer.Checked)
                {
                    dt_DeliveredReport = MergeSameCustomer(dt_DeliveredReport);
                }
                else
                {
                    dt_DeliveredReport = CalculateRowTotal(dt_DeliveredReport);
                }


                ReallocateIndexAndSumUp(dt_DeliveredReport);

                AddDividerEmptyRow(dt_DeliveredReport);
                dt_DeliveredReport = AddDividerEmptyRow(dt_DeliveredReport);
                dgv.DataSource = dt_DeliveredReport;
                DgvUIEdit(dgv);

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

                dgv.ClearSelection();
                frmLoading.CloseForm();
            }
        }

        private void LoadDeliveredData()
        {
            dgvList.DataSource = null;

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
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                string dateType = cmbReportType.Text;
                //string itemSize = cmbItemSize.Text;
                //string itemType = cmbItemType.Text;

                #endregion

                #region get start and end date

                string start = dtpDateFrom.Value.ToString("yyyy/MM/dd");
                string end = dtpDateTo.Value.ToString("yyyy/MM/dd");

                #endregion

                #region Load data from database

                string itemCust = text.SPP_BrandName;
                DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);//19ms

                if (cbSortByType.Checked)
                {
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }
                else
                {
                    dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }

                DataTable dt_Item = dalItem.Select();//16ms

                //DataTable dt_DOList = dalSPP.DOWithTrfInfoSelect(start, end);//1897ms
                DataTable dt_DOList = dalSPP.NEW_DOWithTrfInfoSelect(start, end);//300

                dt_DOList.Columns.Add("To Delete", typeof(string));

                #endregion

                //^ 2600ms>>359ms

                #region load product list
                int index = 1;

                #region new Code


                Dictionary<string, List<DataRow>> doDict = new Dictionary<string, List<DataRow>>();

                foreach (DataRow row_DO in dt_DOList.Rows)
                {
                    string itemCode = row_DO[dalSPP.ItemCode].ToString();
                    if (!doDict.ContainsKey(itemCode))
                    {
                        doDict[itemCode] = new List<DataRow>();
                    }
                    doDict[itemCode].Add(row_DO);
                }

                foreach (DataRow row in dt_Product.Rows)
                {
                    //get product info
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

                    #region Get Size Data

                    int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                    int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                    string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                    string sizeString_1 = "";
                    string sizeString_2 = "";

                    int size_1 = 1;
                    int size_2 = 1;

                    if (denominator == 1)
                    {
                        size_1 = numerator;
                        sizeString_1 = numerator.ToString();

                    }
                    else
                    {
                        size_1 = numerator / denominator;
                        sizeString_1 = numerator + "/" + denominator;
                    }

                    if (numerator_2 > 0)
                    {
                        if (denominator_2 == 1)
                        {
                            sizeString_2 += numerator_2;
                            size_2 = numerator_2;

                        }
                        else
                        {
                            size_2 = numerator_2 / denominator_2;

                            if (numerator_2 == 3 && denominator_2 == 2)
                            {
                                sizeString_2 += "1 1" + "/" + denominator_2;
                            }
                            else
                            {
                                sizeString_2 += numerator_2 + "/" + denominator_2;
                            }



                        }
                    }

                    if (size_1 >= size_2)
                    {
                        if (sizeString_2 != "")
                            sizeString = sizeString_1 + " x " + sizeString_2;

                        else
                        {
                            sizeString = sizeString_1;
                        }
                    }
                    else
                    {

                        if (sizeString_2 != "")
                            sizeString = sizeString_2 + " x " + sizeString_1;

                        else
                        {
                            sizeString = sizeString_2;
                        }
                    }
                    #endregion

                    DataRow newRow = dt_DeliveredReport.NewRow();

                    newRow[header_Index] = index++;

                    newRow[header_ItemCode] = itemCode;
                    newRow[header_ItemSize_Numerator] = numerator;
                    newRow[header_ItemSize_Denominator] = denominator;
                    newRow[header_SizeUnit] = sizeUnit;
                    newRow[header_ItemSizeString] = sizeString;
                    newRow[header_ItemType] = typeName;
                    //newRow[header_ItemString] = sizeString + " " + typeName;
                    newRow[header_ItemString] = itemName;

                    newRow[header_StdPacking] = qtyPerBag;

                    bool DOFound = false;

                    if (doDict.ContainsKey(itemCode))
                    {
                        foreach (DataRow row_DO in doDict[itemCode])
                        {
                            string trfResult = row_DO[dalTrfHist.TrfResult].ToString();
                            string toDelete = row_DO["To Delete"].ToString();

                            if (toDelete != "1" && trfResult == "Passed" && row_DO[dalSPP.ItemCode].ToString() == itemCode)
                            {
                                int custID = int.TryParse(row_DO[dalSPP.CustTblCode].ToString(), out custID) ? custID : -1;
                                string shortName = row_DO[dalSPP.ShortName].ToString();

                                DateTime trfDate = DateTime.TryParse(row_DO[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                                int deliveredPcs = int.TryParse(row_DO[dalTrfHist.TrfQty].ToString(), out deliveredPcs) ? deliveredPcs : 0;

                                if (dt_DeliveredReport.Rows.Count > 0)
                                {
                                    bool custFound = false;

                                    foreach (DataRow delivered_Row in dt_DeliveredReport.Rows)
                                    {
                                        if (delivered_Row[header_ItemCode].ToString() == itemCode && delivered_Row[header_CustID].ToString() == custID.ToString())
                                        {
                                            //get delivered pcs qty
                                            string dateHeaderName = GetDateHeaderName(trfDate);

                                            int oldDeliveredPcs = int.TryParse(delivered_Row[dateHeaderName + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                                            deliveredPcs += oldDeliveredPcs;

                                            int deliveredBag = deliveredPcs / qtyPerBag;

                                            string bagString = " BAGS (";

                                            if (deliveredBag <= 1)
                                            {
                                                bagString = " BAG (";
                                            }

                                            string DeliveredQtyString = "";

                                            if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                            {
                                                DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                            }
                                            else if (cbDeliveredQtyInPcs.Checked)
                                            {
                                                DeliveredQtyString = deliveredPcs.ToString();
                                            }
                                            else if (cbDeliveredQtyInBag.Checked)
                                            {
                                                DeliveredQtyString = deliveredBag.ToString();
                                            }

                                            newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                            newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                            newRow[dateHeaderName] = DeliveredQtyString;

                                            //newRow[dateHeaderName] = deliveredPcs;
                                            //newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";


                                            DOFound = true;
                                            custFound = true;
                                            break;
                                        }
                                    }

                                    if (!custFound)
                                    {
                                        newRow = dt_DeliveredReport.NewRow();

                                        newRow[header_Index] = index++;

                                        newRow[header_ItemCode] = itemCode;
                                        newRow[header_ItemSize_Numerator] = numerator;
                                        newRow[header_ItemSize_Denominator] = denominator;
                                        newRow[header_SizeUnit] = sizeUnit;
                                        //newRow[header_ItemString] = sizeString + " " + typeName;
                                        newRow[header_ItemString] = itemName;
                                        newRow[header_ItemSizeString] = sizeString;
                                        newRow[header_ItemType] = typeName;
                                        newRow[header_StdPacking] = qtyPerBag;

                                        newRow[header_CustID] = custID;
                                        newRow[header_CustShortName] = shortName;

                                        string dateHeaderName = GetDateHeaderName(trfDate);

                                        int deliveredBag = deliveredPcs / qtyPerBag;

                                        string bagString = " Bags (";

                                        if (deliveredBag <= 1)
                                        {
                                            bagString = " Bag (";
                                        }

                                        string DeliveredQtyString = "";

                                        if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                        {
                                            DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                        }
                                        else if (cbDeliveredQtyInPcs.Checked)
                                        {
                                            DeliveredQtyString = deliveredPcs.ToString();
                                        }
                                        else if (cbDeliveredQtyInBag.Checked)
                                        {
                                            DeliveredQtyString = deliveredBag.ToString();
                                        }

                                        newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                        newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                        newRow[dateHeaderName] = DeliveredQtyString;

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

                                    string bagString = " Bags (";

                                    if (deliveredBag <= 1)
                                    {
                                        bagString = " Bag (";
                                    }

                                    string DeliveredQtyString = "";

                                    if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                    {
                                        DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                    }
                                    else if (cbDeliveredQtyInPcs.Checked)
                                    {
                                        DeliveredQtyString = deliveredPcs.ToString();
                                    }
                                    else if (cbDeliveredQtyInBag.Checked)
                                    {
                                        DeliveredQtyString = deliveredBag.ToString();
                                    }

                                    newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                    newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                    newRow[dateHeaderName] = DeliveredQtyString;

                                    //newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                                    //newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                                    ////newRow[dateHeaderName] = deliveredPcs;
                                    //newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";

                                    dt_DeliveredReport.Rows.Add(newRow);
                                    DOFound = true;
                                }

                                row_DO["To Delete"] = "1";


                            }
                        }
                        doDict.Remove(itemCode); // Once processed, no need to check again.
                    }
                    else
                    {
                        dt_DeliveredReport.Rows.Add(newRow);
                    }
                }

                #endregion

                #region Old Code

                //foreach (DataRow row in dt_Product.Rows)
                //{
                //    //get product info
                //    string itemCode = row[dalSPP.ItemCode].ToString();
                //    string itemName = row[dalSPP.ItemName].ToString();
                //    int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                //    int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                //    int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                //    int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;
                //    int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                //    int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                //    string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                //    string typeName = row[dalSPP.TypeName].ToString();

                //    int pcsStock = readyStock;
                //    int bagStock = pcsStock / qtyPerBag;


                //    string sizeString = "";

                //    #region Get Size Data

                //    int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                //    int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                //    string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                //    string sizeString_1 = "";
                //    string sizeString_2 = "";

                //    int size_1 = 1;
                //    int size_2 = 1;

                //    if (denominator == 1)
                //    {
                //        size_1 = numerator;
                //        sizeString_1 = numerator.ToString();

                //    }
                //    else
                //    {
                //        size_1 = numerator / denominator;
                //        sizeString_1 = numerator + "/" + denominator;
                //    }

                //    if (numerator_2 > 0)
                //    {
                //        if (denominator_2 == 1)
                //        {
                //            sizeString_2 += numerator_2;
                //            size_2 = numerator_2;

                //        }
                //        else
                //        {
                //            size_2 = numerator_2 / denominator_2;

                //            if (numerator_2 == 3 && denominator_2 == 2)
                //            {
                //                sizeString_2 += "1 1" + "/" + denominator_2;
                //            }
                //            else
                //            {
                //                sizeString_2 += numerator_2 + "/" + denominator_2;
                //            }



                //        }
                //    }

                //    if (size_1 >= size_2)
                //    {
                //        if (sizeString_2 != "")
                //            sizeString = sizeString_1 + " x " + sizeString_2;

                //        else
                //        {
                //            sizeString = sizeString_1;
                //        }
                //    }
                //    else
                //    {

                //        if (sizeString_2 != "")
                //            sizeString = sizeString_2 + " x " + sizeString_1;

                //        else
                //        {
                //            sizeString = sizeString_2;
                //        }
                //    }
                //    #endregion

                //    DataRow newRow = dt_DeliveredReport.NewRow();

                //    newRow[header_Index] = index++;

                //    newRow[header_ItemCode] = itemCode;
                //    newRow[header_ItemSize_Numerator] = numerator;
                //    newRow[header_ItemSize_Denominator] = denominator;
                //    newRow[header_SizeUnit] = sizeUnit;
                //    newRow[header_ItemSizeString] = sizeString;
                //    newRow[header_ItemType] = typeName;
                //    //newRow[header_ItemString] = sizeString + " " + typeName;
                //    newRow[header_ItemString] = itemName;

                //    newRow[header_StdPacking] = qtyPerBag;

                //    bool DOFound = false;

                //    //search product delivered record
                //    foreach (DataRow row_DO in dt_DOList.Rows)
                //    {
                //        string trfResult = row_DO[dalTrfHist.TrfResult].ToString();

                //        if (trfResult == "Passed" && row_DO[dalSPP.ItemCode].ToString() == itemCode)
                //        {
                //            int custID = int.TryParse(row_DO[dalSPP.CustTblCode].ToString(), out custID) ? custID : -1;
                //            string shortName = row_DO[dalSPP.ShortName].ToString();

                //            DateTime trfDate = DateTime.TryParse(row_DO[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                //            int deliveredPcs = int.TryParse(row_DO[dalTrfHist.TrfQty].ToString(), out deliveredPcs) ? deliveredPcs : 0;

                //            if (dt_DeliveredReport.Rows.Count > 0)
                //            {
                //                bool custFound = false;

                //                foreach (DataRow delivered_Row in dt_DeliveredReport.Rows)
                //                {
                //                    if (delivered_Row[header_ItemCode].ToString() == itemCode && delivered_Row[header_CustID].ToString() == custID.ToString())
                //                    {
                //                        //get delivered pcs qty
                //                        string dateHeaderName = GetDateHeaderName(trfDate);

                //                        int oldDeliveredPcs = int.TryParse(delivered_Row[dateHeaderName + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                //                        deliveredPcs += oldDeliveredPcs;

                //                        int deliveredBag = deliveredPcs / qtyPerBag;

                //                        string bagString = " BAGS (";

                //                        if (deliveredBag <= 1)
                //                        {
                //                            bagString = " BAG (";
                //                        }

                //                        string DeliveredQtyString = "";

                //                        if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                //                        {
                //                            DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                //                        }
                //                        else if (cbDeliveredQtyInPcs.Checked)
                //                        {
                //                            DeliveredQtyString = deliveredPcs.ToString();
                //                        }
                //                        else if (cbDeliveredQtyInBag.Checked)
                //                        {
                //                            DeliveredQtyString = deliveredBag.ToString();
                //                        }

                //                        newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                //                        newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                //                        newRow[dateHeaderName] = DeliveredQtyString;

                //                        //newRow[dateHeaderName] = deliveredPcs;
                //                        //newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";


                //                        DOFound = true;
                //                        custFound = true;
                //                        break;
                //                    }
                //                }

                //                if (!custFound)
                //                {
                //                    newRow = dt_DeliveredReport.NewRow();

                //                    newRow[header_Index] = index++;

                //                    newRow[header_ItemCode] = itemCode;
                //                    newRow[header_ItemSize_Numerator] = numerator;
                //                    newRow[header_ItemSize_Denominator] = denominator;
                //                    newRow[header_SizeUnit] = sizeUnit;
                //                    //newRow[header_ItemString] = sizeString + " " + typeName;
                //                    newRow[header_ItemString] = itemName;
                //                    newRow[header_ItemSizeString] = sizeString;
                //                    newRow[header_ItemType] = typeName;
                //                    newRow[header_StdPacking] = qtyPerBag;

                //                    newRow[header_CustID] = custID;
                //                    newRow[header_CustShortName] = shortName;

                //                    string dateHeaderName = GetDateHeaderName(trfDate);

                //                    int deliveredBag = deliveredPcs / qtyPerBag;

                //                    string bagString = " Bags (";

                //                    if (deliveredBag <= 1)
                //                    {
                //                        bagString = " Bag (";
                //                    }

                //                    string DeliveredQtyString = "";

                //                    if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                //                    {
                //                        DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                //                    }
                //                    else if (cbDeliveredQtyInPcs.Checked)
                //                    {
                //                        DeliveredQtyString = deliveredPcs.ToString();
                //                    }
                //                    else if (cbDeliveredQtyInBag.Checked)
                //                    {
                //                        DeliveredQtyString = deliveredBag.ToString();
                //                    }

                //                    newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                //                    newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                //                    newRow[dateHeaderName] = DeliveredQtyString;

                //                    dt_DeliveredReport.Rows.Add(newRow);
                //                    DOFound = true;
                //                }
                //            }
                //            else
                //            {
                //                // insert first data of the table
                //                newRow[header_CustID] = custID;
                //                newRow[header_CustShortName] = shortName;

                //                string dateHeaderName = GetDateHeaderName(trfDate);

                //                int deliveredBag = deliveredPcs / qtyPerBag;

                //                string bagString = " Bags (";

                //                if (deliveredBag <= 1)
                //                {
                //                    bagString = " Bag (";
                //                }

                //                string DeliveredQtyString = "";

                //                if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                //                {
                //                    DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                //                }
                //                else if (cbDeliveredQtyInPcs.Checked)
                //                {
                //                    DeliveredQtyString = deliveredPcs.ToString();
                //                }
                //                else if (cbDeliveredQtyInBag.Checked)
                //                {
                //                    DeliveredQtyString = deliveredBag.ToString();
                //                }

                //                newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                //                newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                //                newRow[dateHeaderName] = DeliveredQtyString;

                //                //newRow[dateHeaderName + header_DeliveredPcs] = deliveredPcs;
                //                //newRow[dateHeaderName + header_DeliveredBag] = deliveredBag;
                //                ////newRow[dateHeaderName] = deliveredPcs;
                //                //newRow[dateHeaderName] = deliveredBag + bagString + deliveredPcs + ")";

                //                dt_DeliveredReport.Rows.Add(newRow);
                //                DOFound = true;
                //            }
                //        }

                //    }


                //    if (!DOFound)
                //    {
                //        dt_DeliveredReport.Rows.Add(newRow);
                //    }


                //}

                #endregion

                #endregion

                //^ 2634ms>>2000ms
                if (cbSortByCustomer.Checked)
                {
                    dt_DeliveredReport.DefaultView.Sort = header_CustShortName + " DESC";

                    dt_DeliveredReport = dt_DeliveredReport.DefaultView.ToTable();


                }

                if (cbMergeItem.Checked)
                {
                    dt_DeliveredReport = MergeSameItem(dt_DeliveredReport);//3567MS>1867ms
                }
                else if (cbMergeCustomer.Checked)
                {
                    dt_DeliveredReport = MergeSameCustomer(dt_DeliveredReport);
                }
                else
                {
                    dt_DeliveredReport = CalculateRowTotal(dt_DeliveredReport);
                }

                //^ 3797ms >> 924ms
                ReallocateIndexAndSumUp(dt_DeliveredReport);

                AddDividerEmptyRow(dt_DeliveredReport);
                dt_DeliveredReport = AddDividerEmptyRow(dt_DeliveredReport);
          
                dgv.DataSource = dt_DeliveredReport;

                DgvUIEdit(dgv);

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

           
                dgv.ClearSelection();
                frmLoading.CloseForm();
            }
        }
        private void LoadSalesData()
        {
            dgvList.DataSource = null;

            lblTotalBag.Text ="RM "+ 0 +" : "+ 0 + " BAG(s) / " + 0 + " PCS " + text_Selected;

            if (Validation())
            {
                #region Pre Setting

                frmLoading.ShowLoadingScreen();

                DataGridView dgv = dgvList;
                DataTable dt_SalesReport = NewSalesTable();

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

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
                    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }
                else
                {
                    dt_Product.DefaultView.Sort = dalSPP.SizeNumerator + " ASC," + dalSPP.SizeWeight + "1 ASC";
                    dt_Product = dt_Product.DefaultView.ToTable();
                }

                DataTable dt_Item = dalItem.Select();

                DataTable dt_DOList = dalSPP.DOWithTrfInfoSelect(start, end);

                dt_DOList.DefaultView.Sort = dalSPP.DiscountRate + " ASC";
                dt_DOList = dt_DOList.DefaultView.ToTable();

                DataTable dt_PriceList = dalSPP.DiscountSelect();

                dt_PriceList.DefaultView.Sort = dalSPP.ItemTblCode + " ASC," + dalSPP.UnitPrice + " ASC," + dalSPP.DiscountRate + " ASC";
                dt_PriceList = dt_PriceList.DefaultView.ToTable();

                dt_PriceList = MergeSameItemAndPrice(dt_PriceList);
                int index = 1;

                #endregion

                #region load product list

                foreach (DataRow row in dt_Product.Rows)
                {
                    string itemCode = row[dalSPP.ItemCode].ToString();
                    string itemName = row[dalSPP.ItemName].ToString();

                    //int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;

                    int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                    int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                   // int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;
                    string typeName = row[dalSPP.TypeName].ToString();

                    //int pcsStock = readyStock;
                    //int bagStock = pcsStock / qtyPerBag;

                    #region Get Size Data

                    string sizeString = "";

                    int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                    int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                    string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                    int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                    int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                    string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                    string sizeString_1 = "";
                    string sizeString_2 = "";

                    int size_1 = 1;
                    int size_2 = 1;

                    if (denominator == 1)
                    {
                        size_1 = numerator;
                        sizeString_1 = numerator.ToString();

                    }
                    else
                    {
                        size_1 = numerator / denominator;
                        sizeString_1 = numerator + "/" + denominator;
                    }

                    if (numerator_2 > 0)
                    {
                        if (denominator_2 == 1)
                        {
                            sizeString_2 += numerator_2;
                            size_2 = numerator_2;

                        }
                        else
                        {
                            size_2 = numerator_2 / denominator_2;

                            if (numerator_2 == 3 && denominator_2 == 2)
                            {
                                sizeString_2 += "1 1" + "/" + denominator_2;
                            }
                            else
                            {
                                sizeString_2 += numerator_2 + "/" + denominator_2;
                            }



                        }
                    }

                    if (size_1 >= size_2)
                    {
                        if (sizeString_2 != "")
                            sizeString = sizeString_1 + " x " + sizeString_2;

                        else
                        {
                            sizeString = sizeString_1;
                        }
                    }
                    else
                    {

                        if (sizeString_2 != "")
                            sizeString = sizeString_2 + " x " + sizeString_1;

                        else
                        {
                            sizeString = sizeString_2;
                        }
                    }
                    #endregion

                    foreach(DataRow rowPrice in dt_PriceList.Rows)
                    {
                        DataRow newRow;

                        if (itemCode.Equals(rowPrice[dalSPP.ItemTblCode].ToString()))
                        {
                            string discountRate_String = rowPrice[dalSPP.DiscountRate].ToString();
                            string unitPrice_String = rowPrice[dalSPP.UnitPrice].ToString();

                            bool DOFound = false;

                            foreach (DataRow row_DO in dt_DOList.Rows)
                            {
                                string trfResult = row_DO[dalTrfHist.TrfResult].ToString();
                                string DO_ItemCode = row_DO[dalSPP.ItemCode].ToString();
                                string PO_UnitPrice = row_DO[dalSPP.UnitPrice].ToString();
                                string PO_DiscountRate = row_DO[dalSPP.DiscountRate].ToString();

                                if (!dt_SalesReport.Columns.Contains(PO_DiscountRate))
                                {


                                    dt_SalesReport.Columns.Add(PO_DiscountRate, typeof(string));

                                    if (!dt_SalesReport.Columns.Contains(PO_DiscountRate + header_DeliveredSales))
                                        dt_SalesReport.Columns.Add(PO_DiscountRate + header_DeliveredSales, typeof(string));

                                    if (!dt_SalesReport.Columns.Contains(PO_DiscountRate + header_DeliveredPcs))
                                        dt_SalesReport.Columns.Add(PO_DiscountRate + header_DeliveredPcs, typeof(string));

                                    if (!dt_SalesReport.Columns.Contains(PO_DiscountRate + header_DeliveredBag))
                                        dt_SalesReport.Columns.Add(PO_DiscountRate + header_DeliveredBag, typeof(string));

                                }

                                bool ItemAndPriceMatched = trfResult == "Passed" && DO_ItemCode == itemCode && PO_UnitPrice == unitPrice_String;

                                if (ItemAndPriceMatched)
                                {
                                    DOFound = true;

                                    int custID = int.TryParse(row_DO[dalSPP.CustTblCode].ToString(), out custID) ? custID : -1;
                                    string shortName = row_DO[dalSPP.ShortName].ToString();

                                    int deliveredPcs = int.TryParse(row_DO[dalTrfHist.TrfQty].ToString(), out deliveredPcs) ? deliveredPcs : 0;

                                    if (dt_SalesReport.Rows.Count > 0)
                                    {
                                        bool ItemWithSameUnitPriceFound = false;

                                        foreach (DataRow sales_Row in dt_SalesReport.Rows)
                                        {
                                            if (sales_Row[header_ItemCode].ToString() == itemCode && sales_Row[header_UnitPrice].ToString() == unitPrice_String)
                                            {

                                                ItemWithSameUnitPriceFound = true;

                                                int oldDeliveredPcs = int.TryParse(sales_Row[PO_DiscountRate + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                                                deliveredPcs += oldDeliveredPcs;

                                                int deliveredBag = deliveredPcs / qtyPerBag;

                                                float sales_subTotal = deliveredPcs * (float.TryParse(unitPrice_String, out float x)? x: 0);
                                                sales_subTotal = sales_subTotal * (100 - (float.TryParse(PO_DiscountRate, out float y) ? y : 0)) / 100;


                                                string bagString = " BAGS (";

                                                if (deliveredBag <= 1)
                                                {
                                                    bagString = " BAG (";
                                                }

                                                string DeliveredQtyString = "";

                                                if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                                {
                                                    DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                                }
                                                else if (cbDeliveredQtyInPcs.Checked)
                                                {
                                                    DeliveredQtyString = deliveredPcs.ToString();
                                                }
                                                else if (cbDeliveredQtyInBag.Checked)
                                                {
                                                    DeliveredQtyString = deliveredBag.ToString();
                                                }

                                                if(cbDeliveredUnitInSales.Checked)
                                                {
                                                    DeliveredQtyString = DeliveredQtyString == "" ? DeliveredQtyString : " : " + DeliveredQtyString;

                                                    DeliveredQtyString = "RM " + sales_subTotal.ToString("N2")  + DeliveredQtyString;
                                                }

                                                sales_Row[PO_DiscountRate + header_DeliveredPcs] = deliveredPcs;
                                                sales_Row[PO_DiscountRate + header_DeliveredBag] = deliveredBag;
                                                sales_Row[PO_DiscountRate + header_DeliveredSales] = sales_subTotal;
                                                sales_Row[PO_DiscountRate] = DeliveredQtyString;
                                          
                                                break;

                                            }
                                        }

                                        if (!ItemWithSameUnitPriceFound)
                                        {
                                            newRow = dt_SalesReport.NewRow();

                                            newRow[header_Index] = index++;

                                            newRow[header_ItemCode] = itemCode;
                                            newRow[header_ItemSize_Numerator] = numerator;
                                            newRow[header_ItemSize_Denominator] = denominator;
                                            newRow[header_SizeUnit] = sizeUnit;
                                            //newRow[header_ItemString] = sizeString + " " + typeName;
                                            newRow[header_ItemString] = itemName;
                                            newRow[header_ItemSizeString] = sizeString;
                                            newRow[header_ItemType] = typeName;
                                            newRow[header_StdPacking] = qtyPerBag;
                                            newRow[header_UnitPrice] = unitPrice_String;


                                            int deliveredBag = deliveredPcs / qtyPerBag;

                                            float sales_subTotal = deliveredPcs * (float.TryParse(unitPrice_String, out float x) ? x : 0);

                                            sales_subTotal = sales_subTotal * (100 - (float.TryParse(PO_DiscountRate, out float y) ? y : 0)) / 100;

                                            string bagString = " Bags (";

                                            if (deliveredBag <= 1)
                                            {
                                                bagString = " Bag (";
                                            }

                                            string DeliveredQtyString = "";

                                            if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                            {
                                                DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                            }
                                            else if (cbDeliveredQtyInPcs.Checked)
                                            {
                                                DeliveredQtyString = deliveredPcs.ToString();
                                            }
                                            else if (cbDeliveredQtyInBag.Checked)
                                            {
                                                DeliveredQtyString = deliveredBag.ToString();
                                            }

                                            if (cbDeliveredUnitInSales.Checked)
                                            {
                                                DeliveredQtyString = DeliveredQtyString == "" ? DeliveredQtyString : " : " + DeliveredQtyString;

                                                DeliveredQtyString = "RM " + sales_subTotal.ToString("N2")  + DeliveredQtyString;
                                            }

                                            newRow[PO_DiscountRate + header_DeliveredPcs] = deliveredPcs;
                                            newRow[PO_DiscountRate + header_DeliveredBag] = deliveredBag;
                                            newRow[PO_DiscountRate + header_DeliveredSales] = sales_subTotal;
                                            newRow[PO_DiscountRate] = DeliveredQtyString;

                                            dt_SalesReport.Rows.Add(newRow);

                                        }
                                    }
                                    else
                                    {
                                        newRow = dt_SalesReport.NewRow();

                                        newRow[header_ItemCode] = itemCode;
                                        newRow[header_ItemSize_Numerator] = numerator;
                                        newRow[header_ItemSize_Denominator] = denominator;
                                        newRow[header_SizeUnit] = sizeUnit;
                                        newRow[header_UnitPrice] = unitPrice_String;

                                        //newRow[header_ItemString] = sizeString + " " + typeName;
                                        newRow[header_ItemString] = itemName;
                                        newRow[header_ItemSizeString] = sizeString;
                                        newRow[header_ItemType] = typeName;
                                        newRow[header_StdPacking] = qtyPerBag;

                                        int deliveredBag = deliveredPcs / qtyPerBag;
                                        float sales_subTotal = deliveredPcs * (float.TryParse(unitPrice_String, out float x) ? x : 0);
                                        sales_subTotal = sales_subTotal * (100 - (float.TryParse(PO_DiscountRate, out float y) ? y : 0)) /100;

                                        string bagString = " Bags (";

                                        if (deliveredBag <= 1)
                                        {
                                            bagString = " Bag (";
                                        }

                                        string DeliveredQtyString = "";

                                        if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && deliveredBag != 0)
                                        {
                                            DeliveredQtyString = deliveredBag + bagString + deliveredPcs + ")";
                                        }
                                        else if (cbDeliveredQtyInPcs.Checked)
                                        {
                                            DeliveredQtyString = deliveredPcs.ToString();
                                        }
                                        else if (cbDeliveredQtyInBag.Checked)
                                        {
                                            DeliveredQtyString = deliveredBag.ToString();
                                        }

                                        if (cbDeliveredUnitInSales.Checked)
                                        {
                                            DeliveredQtyString = DeliveredQtyString == "" ? DeliveredQtyString : " : " + DeliveredQtyString;

                                            DeliveredQtyString = "RM " + sales_subTotal.ToString("N2") + DeliveredQtyString;
                                        }

                                        newRow[PO_DiscountRate + header_DeliveredPcs] = deliveredPcs;
                                        newRow[PO_DiscountRate + header_DeliveredBag] = deliveredBag;
                                        newRow[PO_DiscountRate + header_DeliveredSales] = sales_subTotal;
                                        newRow[PO_DiscountRate] = DeliveredQtyString;

                                        dt_SalesReport.Rows.Add(newRow);
                                    }
                                }

                            }

                            if(!DOFound)
                            {
                                newRow = dt_SalesReport.NewRow();

                                newRow[header_ItemCode] = itemCode;
                                newRow[header_UnitPrice] = unitPrice_String;
                                newRow[header_ItemSize_Numerator] = numerator;
                                newRow[header_ItemSize_Denominator] = denominator;
                                newRow[header_SizeUnit] = sizeUnit;
                                newRow[header_ItemSizeString] = sizeString;
                                newRow[header_ItemType] = typeName;
                                newRow[header_ItemString] = itemName;


                                newRow[header_StdPacking] = qtyPerBag;
                                dt_SalesReport.Rows.Add(newRow);

                            }
                        }

                        

                    }

                }

                #endregion

                if (cbMergeItem.Checked)
                {
                    dt_SalesReport = MergeSameItemSales(dt_SalesReport);
                }

                ReallocateIndexAndSumUp(dt_SalesReport);

                //dDividerEmptyRow(dt_SalesReport);
                //_SalesReport = AddDividerEmptyRow(dt_SalesReport);

                AddDividerEmptyRow(dt_SalesReport);
                dt_SalesReport = AddDividerEmptyRow(dt_SalesReport);

                dgv.DataSource = dt_SalesReport;
                DgvUIEdit(dgv);

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

                dgv.ClearSelection();
                frmLoading.CloseForm();
            }
        }

        private DataTable CalculateRowTotal(DataTable dt)
        {
            DataTable dt_SubTotalCalculate = dt.Copy();

            for (int i = 0; i < dt_SubTotalCalculate.Rows.Count; i++)
            {
                int rowTotalBag = 0;
                int rowTotalPcs = 0;

                for (int j = 0; j < dt_SubTotalCalculate.Columns.Count; j++)
                {
                    string colHeaderName = dt_SubTotalCalculate.Columns[j].ColumnName;

                    int newDeliveredBag = 0;
                    int newOeliveredPcs = 0;

                    if (colHeaderName.Contains(header_DeliveredPcs))
                    {
                        string DateColHeaderName = dt_SubTotalCalculate.Columns[j - 1].ColumnName;

                        newOeliveredPcs = int.TryParse(dt_SubTotalCalculate.Rows[i][DateColHeaderName + header_DeliveredPcs].ToString(), out newOeliveredPcs) ? newOeliveredPcs : 0;

                        newDeliveredBag = int.TryParse(dt_SubTotalCalculate.Rows[i][DateColHeaderName + header_DeliveredBag].ToString(), out newDeliveredBag) ? newDeliveredBag : 0;

                        rowTotalBag += newDeliveredBag;
                        rowTotalPcs += newOeliveredPcs;
                    }
                }

                dt_SubTotalCalculate.Rows[i][header_TotalPcs] = rowTotalPcs;
                dt_SubTotalCalculate.Rows[i][header_TotalBag] = rowTotalBag;
                dt_SubTotalCalculate.Rows[i][header_TotalString] = rowTotalBag + " Bags (" + rowTotalPcs + ")";
            }

            dt_SubTotalCalculate.AcceptChanges();

            return dt_SubTotalCalculate;
        }

        private DataTable MergeSameItemAndPrice(DataTable dt)
        {
            DataTable dt_Merge = dt.Copy();

            string header_Removed = "REMOVED";

            dt_Merge.Columns.Add(header_Removed, typeof(string));

            string preItemCode = null;
            string preUnitPrice = null;

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string itemCode = dt_Merge.Rows[i][dalSPP.ItemTblCode].ToString();

                string unitPrice = dt_Merge.Rows[i][dalSPP.UnitPrice].ToString();


                if (preItemCode == null || preItemCode != itemCode)
                {
                    preItemCode = itemCode;
                    preUnitPrice = unitPrice;
                }
                else if (preItemCode == itemCode && i > 0)
                {
                    if (preUnitPrice == unitPrice)
                    {
                        dt_Merge.Rows[i-1][header_Removed] = 1.ToString();
                    }
                    else
                    {
                        preUnitPrice = unitPrice;
                    }

                }

            }

            dt_Merge.AcceptChanges();
            foreach (DataRow row in dt_Merge.Rows)
            {
                string removed = row[header_Removed].ToString();

                if (removed == "1")
                    row.Delete();
            }

            dt_Merge.Columns.Remove(header_Removed);
            dt_Merge.AcceptChanges();

            return dt_Merge;
        }

        private DataTable MergeSameItem(DataTable dt)
        {
            DataTable dt_Merge = dt.Copy();
            string header_Removed = "REMOVED";
            dt_Merge.Columns.Add(header_Removed, typeof(string));

            bool isPcsChecked = cbDeliveredQtyInPcs.Checked;
            bool isBagChecked = cbDeliveredQtyInBag.Checked;

            string preItemCode = null;
            int totalCustomer = 1;

            #region new code

            // Cache the column indices for efficiency
            List<int> deliveredPcsIndices = new List<int>();
            List<int> deliveredBagIndices = new List<int>();

            for (int j = 0; j < dt_Merge.Columns.Count; j++)
            {
                string colHeaderName = dt_Merge.Columns[j].ColumnName;
                if (colHeaderName.Contains(header_DeliveredPcs))
                {
                    string DateColHeaderName = dt_Merge.Columns[j - 1].ColumnName;
                    deliveredPcsIndices.Add(dt_Merge.Columns.IndexOf(DateColHeaderName + header_DeliveredPcs));
                    deliveredBagIndices.Add(dt_Merge.Columns.IndexOf(DateColHeaderName + header_DeliveredBag));
                }
            }

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string itemCode = dt_Merge.Rows[i][header_ItemCode].ToString();

                if (preItemCode == null || preItemCode != itemCode)
                {
                    preItemCode = itemCode;
                    totalCustomer = 1;
                }
                else if (preItemCode == itemCode)
                {
                    dt_Merge.Rows[i][header_CustShortName] = ++totalCustomer;

                    int rowTotalBag = 0;
                    int rowTotalPcs = 0;

                    for (int j = 0; j < deliveredPcsIndices.Count; j++)
                    {
                        int oldDeliveredPcs = int.TryParse(dt_Merge.Rows[i - 1][deliveredPcsIndices[j]].ToString(), out int tempVal) ? tempVal : 0;
                        int newDeliveredPcs = int.TryParse(dt_Merge.Rows[i][deliveredPcsIndices[j]].ToString(), out tempVal) ? tempVal : 0;
                        newDeliveredPcs += oldDeliveredPcs;

                        int oldDeliveredBag = int.TryParse(dt_Merge.Rows[i - 1][deliveredBagIndices[j]].ToString(), out tempVal) ? tempVal : 0;
                        int newDeliveredBag = int.TryParse(dt_Merge.Rows[i][deliveredBagIndices[j]].ToString(), out tempVal) ? tempVal : 0;
                        newDeliveredBag += oldDeliveredBag;

                        rowTotalBag += newDeliveredBag;
                        rowTotalPcs += newDeliveredPcs;

                        dt_Merge.Rows[i][deliveredPcsIndices[j]] = newDeliveredPcs;
                        dt_Merge.Rows[i][deliveredBagIndices[j]] = newDeliveredBag;

                        string DateColHeaderName = dt_Merge.Columns[deliveredPcsIndices[j] - 1].ColumnName;
                        string DeliveredQtyString = GetDeliveredQtyString(isPcsChecked, isBagChecked, newDeliveredBag, newDeliveredPcs);
                        dt_Merge.Rows[i][DateColHeaderName] = DeliveredQtyString;
                    }

                    dt_Merge.Rows[i][header_TotalPcs] = rowTotalPcs;
                    dt_Merge.Rows[i][header_TotalBag] = rowTotalBag;
                    dt_Merge.Rows[i][header_TotalString] = $"{rowTotalBag} Bags ({rowTotalPcs})";

                    dt_Merge.Rows[i - 1][header_Removed] = "1";
                }
            }


            #endregion


            #region old code



            //for (int i = 0; i < dt_Merge.Rows.Count; i++)
            //{
            //    string itemCode = dt_Merge.Rows[i][header_ItemCode].ToString();

            //    if (preItemCode == null || preItemCode != itemCode)
            //    {
            //        preItemCode = itemCode;
            //        totalCustomer = 1;
            //    }
            //    else if (preItemCode == itemCode)
            //    {
            //        dt_Merge.Rows[i][header_CustShortName] = ++totalCustomer;

            //        int rowTotalBag = 0;
            //        int rowTotalPcs = 0;

            //        for (int j = 0; j < dt_Merge.Columns.Count; j++)
            //        {
            //            string colHeaderName = dt_Merge.Columns[j].ColumnName;

            //            if (colHeaderName.Contains(header_DeliveredPcs))
            //            {
            //                string DateColHeaderName = dt_Merge.Columns[j - 1].ColumnName;

            //                int columnDeliverdPcsIndex = dt_Merge.Columns.IndexOf(DateColHeaderName + header_DeliveredPcs);
            //                int columnDeliverdBagIndex = dt_Merge.Columns.IndexOf(DateColHeaderName + header_DeliveredBag);

            //                int oldDeliveredPcs = int.TryParse(dt_Merge.Rows[i - 1][columnDeliverdPcsIndex].ToString(), out int tempVal) ? tempVal : 0;
            //                int newDeliveredPcs = int.TryParse(dt_Merge.Rows[i][columnDeliverdPcsIndex].ToString(), out tempVal) ? tempVal : 0;
            //                newDeliveredPcs += oldDeliveredPcs;

            //                int oldDeliveredBag = int.TryParse(dt_Merge.Rows[i - 1][columnDeliverdBagIndex].ToString(), out tempVal) ? tempVal : 0;
            //                int newDeliveredBag = int.TryParse(dt_Merge.Rows[i][columnDeliverdBagIndex].ToString(), out tempVal) ? tempVal : 0;
            //                newDeliveredBag += oldDeliveredBag;

            //                string DeliveredQtyString = GetDeliveredQtyString(isPcsChecked, isBagChecked, newDeliveredBag, newDeliveredPcs);


            //                rowTotalBag += newDeliveredBag;
            //                rowTotalPcs += newDeliveredPcs;

            //                dt_Merge.Rows[i][columnDeliverdPcsIndex] = newDeliveredPcs;
            //                dt_Merge.Rows[i][columnDeliverdBagIndex] = newDeliveredBag;
            //                dt_Merge.Rows[i][DateColHeaderName] = DeliveredQtyString;
            //                dt_Merge.Rows[i - 1][header_Removed] = "1";

            //                dt_Merge.Rows[i][header_TotalPcs] = rowTotalPcs;
            //                dt_Merge.Rows[i][header_TotalBag] = rowTotalBag;
            //                dt_Merge.Rows[i][header_TotalString] = $"{rowTotalBag} Bags ({rowTotalPcs})";
            //            }
            //        }
            //    }
            //}

            #endregion

            List<DataRow> rowsToDelete = new List<DataRow>();

            foreach (DataRow row in dt_Merge.Rows)
            {
                if (row[header_Removed].ToString() == "1")
                {
                    rowsToDelete.Add(row);
                }
            }

            foreach (DataRow row in rowsToDelete)
            {
                dt_Merge.Rows.Remove(row);
            }

            dt_Merge.Columns.Remove(header_Removed);
            return dt_Merge;
        }


        private string GetDeliveredQtyString(bool isPcsChecked, bool isBagChecked, int newDeliveredBag, int newOeliveredPcs)
        {
            string bagString = newDeliveredBag <= 1 ? " Bag (" : " Bags (";
            if (isPcsChecked && isBagChecked && newDeliveredBag != 0)
            {
                return newDeliveredBag + bagString + newOeliveredPcs + ")";
            }
            if (isPcsChecked)
            {
                return newOeliveredPcs.ToString();
            }
            if (isBagChecked)
            {
                return newDeliveredBag.ToString();
            }
            return string.Empty;
        }


        private DataTable Old_MergeSameItem(DataTable dt)
        {
            DataTable dt_Merge = dt.Copy();

            string header_Removed = "REMOVED";

            dt_Merge.Columns.Add(header_Removed, typeof(string));

            string preItemCode = null;

            int totalCustomer = 1;

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string itemCode = dt_Merge.Rows[i][header_ItemCode].ToString();
                int qtyPerBag = int.TryParse(dt_Merge.Rows[i][header_StdPacking].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                if(itemCode == "(OK) CFRE 25 20")
                {
                    float searching = 0;
                }

                int rowTotalBag = 0;
                int rowTotalPcs = 0;

                if (preItemCode == null || preItemCode != itemCode)
                {
                    preItemCode = itemCode;
                    totalCustomer = 1;

                }
                else if (preItemCode == itemCode && i > 0)
                {
                    dt_Merge.Rows[i][header_CustShortName] = ++totalCustomer;

                    for (int j = 0; j < dt_Merge.Columns.Count; j++)
                    {
                        string colHeaderName = dt_Merge.Columns[j].ColumnName;

                        int newDeliveredBag = 0;
                        int newOeliveredPcs = 0;

                        if (colHeaderName.Contains(header_DeliveredPcs))
                        {
                            string DateColHeaderName = dt_Merge.Columns[j - 1].ColumnName;

                            int oldDeliveredPcs = int.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                            newOeliveredPcs = int.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredPcs].ToString(), out newOeliveredPcs) ? newOeliveredPcs : 0;

                            newOeliveredPcs += oldDeliveredPcs;

                            int oldDeliveredBag = int.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredBag].ToString(), out oldDeliveredBag) ? oldDeliveredBag : 0;

                            newDeliveredBag = int.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredBag].ToString(), out newDeliveredBag) ? newDeliveredBag : 0;

                            newDeliveredBag += oldDeliveredBag;

                            string bagString = " Bags (";

                            if (newDeliveredBag <= 1)
                            {
                                bagString = " Bag (";
                            }

                            string DeliveredQtyString = "";

                            if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && newDeliveredBag != 0)
                            {
                                DeliveredQtyString = newDeliveredBag + bagString + newOeliveredPcs + ")";
                            }
                            else if (cbDeliveredQtyInPcs.Checked)
                            {
                                DeliveredQtyString = newOeliveredPcs.ToString();
                            }
                            else if (cbDeliveredQtyInBag.Checked)
                            {
                                DeliveredQtyString = newDeliveredBag.ToString();
                            }

                           
                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredPcs] = newOeliveredPcs;
                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredBag] = newDeliveredBag;
                            dt_Merge.Rows[i][DateColHeaderName] = DeliveredQtyString;

                            //if (newDeliveredBag != 0)
                            //    dt_Merge.Rows[i][DateColHeaderName] = newDeliveredBag + bagString + newOeliveredPcs + ")";

                            dt_Merge.Rows[i - 1][header_Removed] = 1.ToString();


                        }

                        rowTotalBag += newDeliveredBag;
                        rowTotalPcs += newOeliveredPcs;

                        dt_Merge.Rows[i][header_TotalPcs] = rowTotalPcs;
                        dt_Merge.Rows[i][header_TotalBag] = rowTotalBag;
                        dt_Merge.Rows[i][header_TotalString] = rowTotalBag + " Bags (" + rowTotalPcs + ")";
                    }

                }
                
            }

            //^^3999MS
            dt_Merge.AcceptChanges();
            foreach (DataRow row in dt_Merge.Rows)
            {
                string removed = row[header_Removed].ToString();

                if(removed == "1")
                row.Delete();
            }

            dt_Merge.Columns.Remove(header_Removed);
            dt_Merge.AcceptChanges();

            return dt_Merge;
        }

        private DataTable MergeSameItemSales(DataTable dt)
        {
            DataTable dt_Merge = dt.Copy();

            string header_Removed = "REMOVED";

            dt_Merge.Columns.Add(header_Removed, typeof(string));

            string preItemCode = null;



            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string itemCode = dt_Merge.Rows[i][header_ItemCode].ToString();
                int qtyPerBag = int.TryParse(dt_Merge.Rows[i][header_StdPacking].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                int rowTotalBag = 0;
                int rowTotalPcs = 0;
                float rowTotalSales = 0;

                if (preItemCode == null || preItemCode != itemCode)
                {
                    preItemCode = itemCode;
                }
                else if (preItemCode == itemCode && i > 0)
                {
                    for (int j = 0; j < dt_Merge.Columns.Count; j++)
                    {
                        string colHeaderName = dt_Merge.Columns[j].ColumnName;

                        int newDeliveredBag = 0;
                        int newDeliveredPcs = 0;
                        float newDeliveredSales = 0;

                        if (colHeaderName.Contains(header_DeliveredSales))
                        {
                            string DateColHeaderName = dt_Merge.Columns[j - 1].ColumnName;

                            int oldDeliveredPcs = int.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                            newDeliveredPcs = int.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredPcs].ToString(), out newDeliveredPcs) ? newDeliveredPcs : 0;

                            newDeliveredPcs += oldDeliveredPcs;

                            int oldDeliveredBag = int.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredBag].ToString(), out oldDeliveredBag) ? oldDeliveredBag : 0;

                            newDeliveredBag = int.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredBag].ToString(), out newDeliveredBag) ? newDeliveredBag : 0;

                            newDeliveredBag += oldDeliveredBag;

                            float oldDeliveredSales = float.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredSales].ToString(), out oldDeliveredSales) ? oldDeliveredSales : 0;

                            newDeliveredSales = float.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredSales].ToString(), out newDeliveredSales) ? newDeliveredSales : 0;

                            newDeliveredSales += oldDeliveredSales;

                            string bagString = " Bags (";

                            if (newDeliveredBag <= 1)
                            {
                                bagString = " Bag (";
                            }

                            string DeliveredQtyString = "";

                            if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && newDeliveredBag != 0)
                            {
                                DeliveredQtyString = newDeliveredBag + bagString + newDeliveredPcs + ")";
                            }
                            else if (cbDeliveredQtyInPcs.Checked)
                            {
                                DeliveredQtyString = newDeliveredPcs.ToString();
                            }
                            else if (cbDeliveredQtyInBag.Checked)
                            {
                                DeliveredQtyString = newDeliveredBag.ToString();
                            }

                            if (cbDeliveredUnitInSales.Checked)
                            {
                                DeliveredQtyString = DeliveredQtyString == "" ? DeliveredQtyString : " : " + DeliveredQtyString;

                                DeliveredQtyString = "RM " + newDeliveredSales.ToString("N2") + DeliveredQtyString;
                            }

                            if(newDeliveredBag <= 0)
                            {
                                DeliveredQtyString = "";
                            }

                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredPcs] = newDeliveredPcs;
                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredBag] = newDeliveredBag;
                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredSales] = newDeliveredSales;

                            dt_Merge.Rows[i][DateColHeaderName] = DeliveredQtyString;


                            //if (newDeliveredBag != 0)
                            //    dt_Merge.Rows[i][DateColHeaderName] = newDeliveredBag + bagString + newOeliveredPcs + ")";

                            dt_Merge.Rows[i - 1][header_Removed] = 1.ToString();


                        }

                        //rowTotalBag += newDeliveredBag;
                        //rowTotalPcs += newDeliveredPcs;

                        //dt_Merge.Rows[i][header_TotalPcs] = rowTotalPcs;
                        //dt_Merge.Rows[i][header_TotalBag] = rowTotalBag;
                        //dt_Merge.Rows[i][header_TotalString] = rowTotalBag + " Bags (" + rowTotalPcs + ")";
                    }

                }

            }

            dt_Merge.AcceptChanges();
            foreach (DataRow row in dt_Merge.Rows)
            {
                string removed = row[header_Removed].ToString();

                if (removed == "1")
                    row.Delete();
            }

            dt_Merge.Columns.Remove(header_Removed);
            dt_Merge.AcceptChanges();

            return dt_Merge;
        }

        private DataTable MergeSameCustomer(DataTable dt)
        {
            DataTable dt_Merge = dt.Copy();

            string header_Removed = "REMOVED";

            dt_Merge.Columns.Add(header_Removed, typeof(string));

            string preCustID = null;

            int totalItem = 1;

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string custID = dt_Merge.Rows[i][header_CustID].ToString();
                int qtyPerBag = int.TryParse(dt_Merge.Rows[i][header_StdPacking].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                int rowTotalBag = 0;
                int rowTotalPcs = 0;

                if (preCustID == null || preCustID != custID)
                {
                    preCustID = custID;
                    totalItem = 1;
                }
                else if (preCustID == custID && i > 0)
                {
                    dt_Merge.Rows[i][header_ItemString] = ++totalItem;

                    for (int j = 0; j < dt_Merge.Columns.Count; j++)
                    {
                        string colHeaderName = dt_Merge.Columns[j].ColumnName;

                        int newDeliveredBag = 0;
                        int newDeliveredPcs = 0;

                        if (colHeaderName.Contains(header_DeliveredPcs))
                        {
                            string DateColHeaderName = dt_Merge.Columns[j - 1].ColumnName;

                            int oldDeliveredPcs = int.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                            newDeliveredPcs = int.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredPcs].ToString(), out newDeliveredPcs) ? newDeliveredPcs : 0;

                            newDeliveredPcs += oldDeliveredPcs;

                            int oldDeliveredBag = int.TryParse(dt_Merge.Rows[i - 1][DateColHeaderName + header_DeliveredBag].ToString(), out oldDeliveredBag) ? oldDeliveredBag : 0;

                            newDeliveredBag = int.TryParse(dt_Merge.Rows[i][DateColHeaderName + header_DeliveredBag].ToString(), out newDeliveredBag) ? newDeliveredBag : 0;

                            newDeliveredBag += oldDeliveredBag;

                            string bagString = " Bags (";

                            if (newDeliveredBag <= 1)
                            {
                                bagString = " Bag (";
                            }

                            string DeliveredQtyString = "";

                            if(cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && newDeliveredBag != 0)
                            {
                                DeliveredQtyString = newDeliveredBag + bagString + newDeliveredPcs + ")";
                            }
                            else if(cbDeliveredQtyInPcs.Checked)
                            {
                                DeliveredQtyString = newDeliveredPcs.ToString();
                            }
                            else if(cbDeliveredQtyInBag.Checked)
                            {
                                DeliveredQtyString = newDeliveredBag.ToString();
                            }

                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredPcs] = newDeliveredPcs;
                            dt_Merge.Rows[i][DateColHeaderName + header_DeliveredBag] = newDeliveredBag;

                            dt_Merge.Rows[i][DateColHeaderName] = DeliveredQtyString;

                            //dt_Merge.Rows[i][DateColHeaderName] = newDeliveredBag + bagString + newDeliveredPcs + ")";

                            dt_Merge.Rows[i - 1][header_Removed] = 1.ToString();


                        }

                        rowTotalBag += newDeliveredBag;
                        rowTotalPcs += newDeliveredPcs;

                        dt_Merge.Rows[i][header_TotalPcs] = rowTotalPcs;
                        dt_Merge.Rows[i][header_TotalBag] = rowTotalBag;
                        dt_Merge.Rows[i][header_TotalString] = rowTotalBag + " Bags (" + rowTotalPcs + ")";

                    }
                }

            }

            dt_Merge.AcceptChanges();

            dt_Merge.DefaultView.Sort = header_TotalBag + " DESC";
            dt_Merge = dt_Merge.DefaultView.ToTable();
            dt_Merge.AcceptChanges();
            foreach (DataRow row in dt_Merge.Rows)
            {
                string removed = row[header_Removed].ToString();

                if (removed == "1")
                    row.Delete();
            }

            dt_Merge.Columns.Remove(header_Removed);
            dt_Merge.AcceptChanges();

            return dt_Merge;
        }

        private DataTable AddDividerEmptyRow(DataTable dt)
        {
            DataTable dt_Divider = dt.Copy();

            string sortingHeaderName = null;

            
            if(cbSortByCustomer.Checked)
            {
                sortingHeaderName = header_CustID;
            }
            else if(cbSortBySize.Checked)
            {
                sortingHeaderName = header_ItemSize_Numerator;
            }
            else if(cbSortByType.Checked)
            {
                sortingHeaderName = header_ItemType;
            }

            if(sortingHeaderName != null)
            {
                string preData = null;

                for(int i = 0; i < dt_Divider.Rows.Count; i++)
                {
                    string sortingData = dt_Divider.Rows[i][sortingHeaderName].ToString();

                    if(preData == null)
                    {
                        preData = sortingData;
                    }
                    else if(preData != sortingData)
                    {
                        dt_Divider.Rows.InsertAt(dt_Divider.NewRow(), i);
                        preData = sortingData;
                    }
                }

                dt_Divider.AcceptChanges();
            }
            else
            {
                MessageBox.Show("Divider Error!");
            }

            return dt_Divider;
        }

        private void ReallocateIndexAndSumUp(DataTable dt)
        {
            int index = 1;
            foreach(DataRow row in dt.Rows)
            {
                row[header_Index] = index++;

                string itemCode = row[header_ItemCode].ToString();
                string totalDeliveredPcs_String = row[header_TotalPcs].ToString();

                if (!string.IsNullOrEmpty(itemCode) && string.IsNullOrEmpty(totalDeliveredPcs_String))
                {

                    int TotalDeliveredBag = 0;
                    int TotalDeliveredPCS = 0;
                    float TotalDeliveredSales = 0;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.Contains(header_DeliveredBag))
                            TotalDeliveredBag += int.TryParse(dt.Rows[dt.Rows.IndexOf(row)][j].ToString(), out int x) ? x : 0;

                        if (dt.Columns[j].ColumnName.Contains(header_DeliveredPcs))
                            TotalDeliveredPCS += int.TryParse(dt.Rows[dt.Rows.IndexOf(row)][j].ToString(), out int x) ? x : 0;

                        if (dt.Columns[j].ColumnName.Contains(header_DeliveredSales))
                            TotalDeliveredSales += float.TryParse(dt.Rows[dt.Rows.IndexOf(row)][j].ToString(), out float x) ? x : 0;
                    }

                    row[header_TotalPcs] = TotalDeliveredPCS;
                    row[header_TotalBag] = TotalDeliveredBag;

                    if(dt.Columns.Contains(header_TotalSales))
                    row[header_TotalSales] = TotalDeliveredSales;

                    string DeliveredQtyString = "";

                    string bagString = " Bags (";

                    if (TotalDeliveredBag <= 1)
                    {
                        bagString = " Bag (";
                    }

                    if (cbDeliveredQtyInPcs.Checked && cbDeliveredQtyInBag.Checked && TotalDeliveredBag != 0)
                    {
                        DeliveredQtyString = TotalDeliveredBag + bagString + TotalDeliveredPCS + ")";
                    }
                    else if (cbDeliveredQtyInPcs.Checked)
                    {
                        DeliveredQtyString = TotalDeliveredPCS.ToString();
                    }
                    else if (cbDeliveredQtyInBag.Checked)
                    {
                        DeliveredQtyString = TotalDeliveredBag.ToString();
                    }

                    if (cbDeliveredUnitInSales.Checked)
                    {
                        DeliveredQtyString = DeliveredQtyString == "" ? DeliveredQtyString : " : " + DeliveredQtyString;

                        DeliveredQtyString = "RM " + TotalDeliveredSales.ToString("N2") + DeliveredQtyString;
                    }

                    if(TotalDeliveredBag >= 1)
                    row[header_TotalString] = DeliveredQtyString;


                }


            }
        }

       

        private string GetDateHeaderName(DateTime trfDate)
        {
            string headerName = "";

            string dateType = cmbReportType.Text;

            if (dateType == dateType_Daily)
            {
                headerName = trfDate.ToString("dd/MM yy");
            }

            else if (dateType == dateType_Monthly)
            {
                if(cb23to22.Checked)
                {
                    int day = trfDate.Day;
                    int month = trfDate.Month;
                    int year = trfDate.Year;

                    if(day >= 23)
                    {
                        month++;
                        if(month == 13)
                        {
                            month = 1;
                            year++;
                        }

                    }
                    headerName = month + "/" + year;

                }
                else
                {
                    headerName = trfDate.Month + "/" + trfDate.Year;

                }

            }

            else if (dateType == dateType_Yearly)
            {
                if (cb23to22.Checked)
                {
                    int day = trfDate.Day;
                    int month = trfDate.Month;
                    int year = trfDate.Year;

                    if(month == 12 && day >= 23)
                    {
                        year++;
                    }

                    headerName = year.ToString();
                }
                else
                {
                    headerName = trfDate.Year.ToString();


                }

            }

            return headerName;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[header_ItemCode].Visible = false;
            dgv.Columns[header_ItemSize_Numerator].Visible = false;
            dgv.Columns[header_ItemSize_Denominator].Visible = false;
            dgv.Columns[header_SizeUnit].Visible = false;
            dgv.Columns[header_ItemString].Visible = false;

            dgv.Columns[header_StdPacking].Visible = false;

            dgv.Columns[header_CustID].Visible = false;

            if (cmbReportType.Text.Equals(dateType_Sales))
            {
                dgv.Columns[header_CustShortName].Visible = false;
            }

            if (cbMergeItem.Checked && cmbReportType.Text.Equals(dateType_Sales))
            {
                if (dgv.Columns.Contains(header_UnitPrice))
                    dgv.Columns[header_UnitPrice].Visible = false;
            }

            dgv.Columns[header_TotalString].Visible = false;


            if (cbDeliveredQtyInBag.Checked && !cbDeliveredQtyInPcs.Checked && !cbDeliveredUnitInSales.Checked)
            {
                dgv.Columns[header_TotalPcs].Visible = false;

                if (dgv.Columns.Contains(header_TotalSales))
                    dgv.Columns[header_TotalSales].Visible = false;

                dgv.Columns[header_TotalBag].Frozen = true;

            }
            else if (!cbDeliveredQtyInBag.Checked && cbDeliveredQtyInPcs.Checked && !cbDeliveredUnitInSales.Checked)
            {
                dgv.Columns[header_TotalBag].Visible = false;

                if (dgv.Columns.Contains(header_TotalSales))
                    dgv.Columns[header_TotalSales].Visible = false;

                dgv.Columns[header_TotalPcs].Frozen = true;

            }
            else if (!cbDeliveredQtyInBag.Checked && !cbDeliveredQtyInPcs.Checked && cbDeliveredUnitInSales.Checked && cmbReportType.Text.Equals(dateType_Sales))
            {
                dgv.Columns[header_TotalBag].Visible = false;
                dgv.Columns[header_TotalPcs].Visible = false;

                dgv.Columns[header_TotalSales].Frozen = true;
            }
            else
            {
                dgv.Columns[header_TotalString].Visible = true;

                dgv.Columns[header_TotalString].Frozen = true;

                dgv.Columns[header_TotalBag].Visible = false;
                dgv.Columns[header_TotalPcs].Visible = false;

                if (dgv.Columns.Contains(header_TotalSales))
                    dgv.Columns[header_TotalSales].Visible = false;
            }

            //if (cbDeliveredQtyInBag.Checked && cbDeliveredQtyInPcs.Checked && (cbDeliveredUnitInSales.Checked || !cmbReportType.Text.Equals(dateType_Sales)))
            //{
            //    dgv.Columns[header_TotalPcs].Visible = false;

            //    dgv.Columns[header_TotalBag].Visible = false;

            //    if(dgv.Columns.Contains(header_TotalSales))
            //    dgv.Columns[header_TotalSales].Visible = false;

            //}
            //else if(cbDeliveredQtyInBag.Checked)
            //{
            //    dgv.Columns[header_TotalString].Visible = false;
            //    dgv.Columns[header_TotalPcs].Visible = false;
            //}
            //else
            //{
            //    dgv.Columns[header_TotalBag].Visible = false;
            //    dgv.Columns[header_TotalString].Visible = false;
            //}

            

            DataTable dt = (DataTable)dgv.DataSource;


            for(int i = 0; i < dt.Columns.Count; i++)
            {
                string colName = dgv.Columns[i].Name;

                if (colName.Contains(header_DeliveredPcs))
                {
                    dgv.Columns[i].Visible = false;
                }
                else if (colName.Contains(header_DeliveredBag))
                {
                    dgv.Columns[i].Visible = false;
                }
                else if (colName.Contains(header_DeliveredSales) && colName != header_TotalSales)
                {
                    dgv.Columns[i].Visible = false;
                }
            }

            dgv.Columns[header_ItemSizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ItemType].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            if (cbMergeItem.Checked)
            {
                dgv.Columns[header_CustShortName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                dgv.Columns[header_CustShortName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (cbMergeCustomer.Checked)
            {
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemType].Visible = false;
                dgv.Columns[header_ItemSizeString].Visible = false;
            }
            else
            {
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_ItemType].Visible = true;
                dgv.Columns[header_ItemSizeString].Visible = true;

            }
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
            dgvList.DataSource = null;

            if(cmbReportType.Text.Equals(dateType_Sales))
            {
                cbMergeItem.Checked = false;
            }

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
            int totalOringBag = 0;
            int totalPcs = 0;
            float totalSales = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_StdPacking].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                string itemCode = dt.Rows[i][header_ItemCode].ToString();

                bool OringFound = itemCode.Contains("CFPOR");

                if (pcsPerBag != -1)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        bool colSelected = false;
                        bool totalSelected = false;

                        if (colName.Contains(header_DeliveredPcs) || colName.Contains(header_DeliveredSales))
                            colSelected = dgv.Rows[i].Cells[j - 1].Selected;

                        else if(colName.Contains(header_TotalString) || colName.Contains(header_TotalBag) || colName.Contains(header_TotalPcs))
                        {
                            colSelected = dgv.Rows[i].Cells[j].Selected;
                            totalSelected = true;

                        }
                       
                        if (colSelected)
                        {
                            if(!totalSelected)
                            {
                                if (colName.Contains(header_DeliveredSales))
                                {
                                    float deliveredSales = float.TryParse(dt.Rows[i][j].ToString(), out deliveredSales) ? deliveredSales : 0;
                                    int deliveredPcs = int.TryParse(dt.Rows[i][j+1].ToString(), out deliveredPcs) ? deliveredPcs : 0;
                                    int deliveredBag = int.TryParse(dt.Rows[i][j + 2].ToString(), out deliveredBag) ? deliveredBag : 0;

                                    if (deliveredPcs > 0)
                                    {
                                        totalSales += deliveredSales;

                                        totalBag += deliveredBag;
                                        totalPcs += deliveredPcs;

                                        if(OringFound)
                                        {
                                            totalOringBag += deliveredBag;
                                        }
                                    }
                                }
                                else
                                {
                                    int deliveredPcs = int.TryParse(dt.Rows[i][j].ToString(), out deliveredPcs) ? deliveredPcs : 0;
                                    int deliveredBag = int.TryParse(dt.Rows[i][j + 1].ToString(), out deliveredBag) ? deliveredBag : 0;

                                    if (deliveredPcs > 0)
                                    {
                                        totalBag += deliveredBag;
                                        totalPcs += deliveredPcs;

                                        if (OringFound)
                                        {
                                            totalOringBag += deliveredBag;
                                        }
                                    }
                                }
                              
                            }
                            else
                            {
                                float TotalDeliveredSales = 0;
                                if (dt.Columns.Contains(header_TotalSales))
                                {
                                    TotalDeliveredSales = float.TryParse(dt.Rows[i][header_TotalSales].ToString(), out TotalDeliveredSales) ? TotalDeliveredSales : 0;

                                }

                                int TotalDeliveredPcs = int.TryParse(dt.Rows[i][header_TotalPcs].ToString(), out TotalDeliveredPcs) ? TotalDeliveredPcs : 0;
                                int TotalDeliveredBag = int.TryParse(dt.Rows[i][header_TotalBag].ToString(), out TotalDeliveredBag) ? TotalDeliveredBag : 0;

                                if (TotalDeliveredPcs > 0)
                                {
                                    totalBag += TotalDeliveredBag;
                                    totalPcs += TotalDeliveredPcs;
                                    totalSales += TotalDeliveredSales;

                                    if (OringFound)
                                    {
                                        totalOringBag += TotalDeliveredBag;
                                    }
                                }
                            }
                           
                        }

                    }
                }
            }

            totalBag -= totalOringBag;

            if (cmbReportType.Text.Equals(dateType_Sales))
            {
                lblTotalBag.Text = "RM " +totalSales.ToString("N2") + " : "+ totalBag + " BAG(s) / " + totalPcs + " PCS " + text_Selected;

            }
            else
            {
                lblTotalBag.Text = totalBag + " BAG(s) / " + totalPcs + " PCS " + text_Selected;

            }

            if(totalOringBag > 0)
            {
                lblTotalBag.Text = lblTotalBag.Text.Replace(text_Selected, " + ORING " + totalOringBag + " PACKET(s) " + text_Selected);

            }
             
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

        private void cbMergeCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if(cbMergeCustomer.Checked)
            {
                cbSortByCustomer.Checked = true;
                cbMergeItem.Checked = false;
            }
        }

        private void cbMergeItem_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMergeItem.Checked)
            {
                cbSortByCustomer.Checked = false;
                cbMergeCustomer.Checked = false;
            }
            


        }

        private void cbSortByCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSortByCustomer.Checked)
            {
                cbMergeItem.Checked = false;
            }
            else if(cbMergeCustomer.Checked)
            {
                cbSortByCustomer.Checked = true;
            }
        }

        private void dgvList_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;
        }

        private void cbDeliveredQtyInPcs_CheckedChanged(object sender, EventArgs e)
        {
            //if(!cbDeliveredQtyInPcs.Checked && !cbDeliveredQtyInBag.Checked)
            //{
            //    if(!cmbReportType.Text.Equals(dateType_Sales))
            //    cbDeliveredQtyInBag.Checked = true;
            //}
        }

        private void cbDeliveredQtyInBag_CheckedChanged(object sender, EventArgs e)
        {
            //if (!cbDeliveredQtyInBag.Checked && !cbDeliveredQtyInPcs.Checked)
            //{
            //    cbDeliveredQtyInPcs.Checked = true;
            //}
        }

        private void cb1to31_CheckedChanged(object sender, EventArgs e)
        {
            if(cb1to31.Checked)
            {
                cb23to22.Checked = false;
                DatePeriodSet();

            }
            else
            {
                cb23to22.Checked = true;
            }

        }

        private void cb23to22_CheckedChanged(object sender, EventArgs e)
        {
            if (cb23to22.Checked)
            {
                cb1to31.Checked = false;
                DatePeriodSet();

            }
            else
            {
                cb1to31.Checked = true;
            }
        }
    }
}
