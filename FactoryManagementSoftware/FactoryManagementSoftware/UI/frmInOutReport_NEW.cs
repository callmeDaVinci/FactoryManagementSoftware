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
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutReport_NEW : Form
    {
        public frmInOutReport_NEW()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvInOutReport, true);
            tool.loadCustomerToComboBox(cmbItemType);
            LoadInOutTypeCMBData(cmbInOutType);
            LoadDateTypeCMBData(cmbDateType);
        }

        #region Variable, Object, Class

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";

        private readonly string inOutType_In = "IN";
        private readonly string inOutType_Out = "OUT";
        private readonly string inOutType_InOut = "IN AND OUT";

        private readonly string dateType_Daily = "DAILY";
        private readonly string dateType_Monthly = "MONTHLY";
        private readonly string dateType_Yearly = "YEARLY";

        private readonly string header_Index = "#";
        private readonly string header_Customer = "CUSTOMER";
        private readonly string header_ItemCode = "CODE";
        private readonly string header_ItemName = "NAME";
        private readonly string header_Total = "TOTAL";
        private readonly string header_Type = "TYPE";
        private readonly string text_In = "IN";
        private readonly string text_Out = "OUT";

        bool loaded = false;

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();
        DataTable dt_Fac;
        DataTable dt_PMMADate;
        #endregion

        #region UI Design

        private DataTable NewInOutTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));

            if (cmbItemType.Text.Equals("All"))
            {
                dt.Columns.Add(header_Customer, typeof(string));
            }

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));

            string inOutType = cmbInOutType.Text;
            if(inOutType == inOutType_InOut)
            {
                dt.Columns.Add(header_Type, typeof(string));
            }

            dt.Columns.Add(header_Total, typeof(double));


            string dateType = cmbDateType.Text;

            if(dateType == dateType_Daily)
            {
                string day = null;
                DateTime start = dtpFrom.Value.Date;
                DateTime end = dtpTo.Value.Date;

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

                for(int i = yearStart; i <= yearEnd; i++)
                {
                    if(yearStart == yearEnd)
                    {
                        for (int j = monthStart; j <= monthEnd; j++)
                        {
                            month = j +"/" +i;
                            dt.Columns.Add(month, typeof(string));
                        }
                    }
                    else
                    {
                        if(i == yearStart)
                        {
                            for (int j = monthStart; j <= 12; j++)
                            {
                                month = j + "/" + i;
                                dt.Columns.Add(month, typeof(string));
                            }
                        }
                        else if (i == yearEnd)
                        {
                            for (int j = 1; j <= monthEnd; j++)
                            {
                                month = j + "/" + i;
                                dt.Columns.Add(month, typeof(string));
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                month = j + "/" + i;
                                dt.Columns.Add(month, typeof(string));
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
                }
            }

            return dt;
        }

        private void dgvInOutUIEdit(DataGridView dgv)
        {

            //dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_ItemName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if(cmbItemType.Text.ToUpper() == "ALL")
            {
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if(cmbInOutType.Text == inOutType_InOut)
            {
                dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgv.Columns[header_ItemName].MinimumWidth = 300;
            dgv.Columns[header_Total].Frozen = true;
        }

        private void dgvInOutReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvInOutReport;
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            //color in or out qty
            string colName = dgv.Columns[col].Name;

            bool matched = colName != header_Index && colName != header_ItemCode && colName != header_ItemName && colName != header_Total;
            string inOutType = cmbInOutType.Text;

            if (cmbItemType.Text.ToUpper() == "ALL")
            {
                matched = matched && colName != header_Customer;
            }

            if (inOutType == inOutType_InOut && dgv.Columns.Contains(header_Type))
            {
                matched = matched && colName != header_Type;
                inOutType = dgv.Rows[row].Cells[header_Type].Value.ToString();

                if (inOutType == inOutType_In)
                {
                    //dgv.Rows[row].Cells[header_Type].Style.BackColor = Color.LightGreen;
                }
                else if (inOutType == inOutType_Out)
                {
                    //dgv.Rows[row].Cells[header_Type].Style.BackColor = Color.LightPink;
                }
                else
                {
                    //dgv.Rows[row].Cells[header_Type].Style.BackColor = Color.White;
                }
            }

            if (matched)
            {
                double qty = double.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out qty) ? qty : -1;

                if (qty > 0)
                {
                    if (inOutType == inOutType_In)
                    {
                        dgv.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;
                    }
                    else if (inOutType == inOutType_Out)
                    {
                        dgv.Rows[row].Cells[col].Style.BackColor = Color.LightPink;
                    }
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.BackColor = Color.White;
                }
            }


        }

        private void dgvInOutReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvInOutUIEdit(dgvInOutReport);
        }

        #endregion

        #region Load Data

        private bool Validation()
        {
            ClearAllErrorSign();

            bool passed = true;
            if (cmbItemType.SelectedIndex == -1)
            {
                if(cbPart.Checked)
                errorProvider1.SetError(cbPart, "Please select a item type.");

                else
                    errorProvider1.SetError(cbMaterial, "Please select a item type.");

                passed = false;
            }

            if (cmbInOutType.SelectedIndex == -1)
            {
                errorProvider2.SetError(lblInOutType, "Please select a in/out type.");

                passed = false;
            }

            if (cmbDateType.SelectedIndex == -1)
            {
                errorProvider3.SetError(lblDateType, "Please select a date type.");

                passed = false;
            }
            else
            {
                string dateType = cmbDateType.Text;

                if(dateType == dateType_Daily)
                {
                    DateTime from, to;

                    if (!DateTime.TryParse(dtpFrom.Value.ToString(), out from))
                    {
                        OpenMoreFilter();
                        errorProvider8.SetError(lblDateFrom, "Invalid Date.");
                        passed = false;
                    }

                    if (!DateTime.TryParse(dtpTo.Value.ToString(), out to))
                    {
                        OpenMoreFilter();
                        errorProvider9.SetError(lblDateTo, "Invalid Date.");
                        passed = false;
                    }

                    if(passed)
                    {
                        if(to < from)
                        {
                            OpenMoreFilter();
                            errorProvider9.SetError(lblDateTo, "Invalid Date.");
                            passed = false;
                        }
                    }

                }
                else if (dateType == dateType_Monthly)
                {
                    if (cmbMonthFrom.SelectedIndex == -1)
                    {
                        OpenMoreFilter();
                        errorProvider4.SetError(lblMonthFrom, "Please select a start month.");
                        passed = false;
                    }

                    if (cmbYearFrom.SelectedIndex == -1)
                    {
                        OpenMoreFilter();
                        errorProvider5.SetError(lblYearFrom, "Please select a start year.");
                        passed = false;
                    }
                }
                else if (dateType == dateType_Yearly)
                {
                    if (cmbYearFrom.SelectedIndex == -1)
                    {
                        OpenMoreFilter();
                        errorProvider5.SetError(lblYearFrom, "Please select a start year.");
                        passed = false;
                    }
                }

            }

            return passed;
        }

        private void LoadInOutTypeCMBData(ComboBox cmb)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("In Out Type");

            dt.Rows.Add(inOutType_In);
            dt.Rows.Add(inOutType_Out);
            dt.Rows.Add(inOutType_InOut);

            cmb.DataSource = dt;
            cmb.DisplayMember = "In Out Type";
            cmb.SelectedIndex = -1;
        }

        private void LoadDateTypeCMBData(ComboBox cmb)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Date Type");

            dt.Rows.Add(dateType_Daily);
            dt.Rows.Add(dateType_Monthly);
            dt.Rows.Add(dateType_Yearly);

            cmb.DataSource = dt;
            cmb.DisplayMember = "Date Type";
            cmb.SelectedIndex = -1;
        }

        private void LoadMonthDataToCMB(ComboBox cmb)
        {
            DataTable dt_month = new DataTable();

            dt_month.Columns.Add("month");

            for (int i = 1; i <= 12; i++)
            {
                dt_month.Rows.Add(i);
            }

            cmb.DataSource = dt_month;
            cmb.DisplayMember = "month";
            cmb.SelectedIndex = -1;
        }

        private void LoadYearDataToCMB(ComboBox cmb)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add("year");

            for (int i = DateTime.Now.Year; i >= 2018; i--)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = "year";
            cmb.SelectedIndex = -1;
        }

        private void LoadYearDataToCMB(ComboBox cmb, int year)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add("year");

            for (int i =year; i <= 2019; i++)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = "year";
            cmb.SelectedIndex = -1;
        }

        private void getStartandEndDate()
        {
            
            if (cmbItemType.SelectedIndex != -1 && cmbDateType.Text == dateType_Daily)
            {
                if(cmbMonthFrom.SelectedIndex >= 0 && cmbYearFrom.SelectedIndex >= 0)
                {
                    int month = int.TryParse(cmbMonthFrom.Text, out month) ? month : DateTime.Now.Month;
                    int year = int.TryParse(cmbYearFrom.Text, out year) ? year : DateTime.Now.Year;

                    if (cbPart.Checked)
                    {
                        if (cmbItemType.Text.Equals(tool.getCustName(1)))
                        {
                            dt_PMMADate = dalPMMADate.Select();
                            dtpFrom.Value = tool.GetPMMAStartDate(month, year, dt_PMMADate);
                            dtpTo.Value = tool.GetPMMAEndDate(month, year, dt_PMMADate);
                        }
                        else
                        {
                            dtpFrom.Value = new DateTime(year, month, 1);
                            dtpTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                        }
                    }
                    else
                    {
                        dtpFrom.Value = new DateTime(year, month, 1);
                        dtpTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    }
                }
            }
        }

        private void frmInOutReport_NEW_Load(object sender, EventArgs e)
        {
            dgvInOutReport.SuspendLayout();

            tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvInOutReport.ResumeLayout();

            btnFilter.Text = textMoreFilters;

            cmbItemType.Text = tool.getCustName(1);

            loaded = true;
        }

        private void LoadInOutData()
        {
            if (Validation())
            {
                #region Pre Setting
                frmLoading.ShowLoadingScreen();
                DataGridView dgv = dgvInOutReport;
                DataTable dt = NewInOutTable();
                DataTable dt_ItemList;
                DataTable dt_TrfHist;
                dt_Fac = dalFac.Select();
                dt_PMMADate = dalPMMADate.Select();
                string inOutType = cmbInOutType.Text;
                string dateType = cmbDateType.Text;
                string itemType = cmbItemType.Text;
                string itemSearch = txtItemSearch.Text;
                bool isMaterialItem = cbMaterial.Checked;

                btnRefresh.Visible = true;
                lblLastUpdated.Visible = true;
                lblUpdatedTime.Visible = true;

                lblUpdatedTime.Text = DateTime.Now.ToString();
                #endregion

                #region load item list
                if (isMaterialItem)
                {
                    //load material item
                    dt_ItemList = dalItem.MatSearch(itemType);
                }
                else
                {
                    //load customer's item
                    if (itemType.ToUpper().Equals("ALL"))
                    {
                        dt_ItemList = dalItemCust.Select();//load all customer's item list
                    }
                    else
                    {
                        dt_ItemList = dalItemCust.custSearch(itemType);
                    }
                }
                #endregion

                #region get start and end date
                string start;
                string end;
                int monthStart = -1, monthEnd = -1, yearStart = -1, yearEnd = -1;

                if (dateType == dateType_Daily)
                {
                    start = dtpFrom.Value.ToString("yyyy/MM/dd");
                    end = dtpTo.Value.ToString("yyyy/MM/dd");
                }
                else if (dateType == dateType_Monthly)
                {
                    monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                    monthEnd = int.TryParse(cmbMonthTo.Text, out monthEnd) ? monthEnd : monthStart;

                    yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    if (itemType == tool.getCustName(1))
                    {
                        start = tool.GetPMMAStartDate(monthStart, yearStart, dt_PMMADate).ToString("yyyy/MM/dd");
                        end = tool.GetPMMAEndDate(monthEnd, yearEnd, dt_PMMADate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        start = new DateTime(yearStart, monthStart, 1).ToString("yyyy/MM/dd");
                        end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd)).ToString("yyyy/MM/dd");
                    }
                }
                else if (dateType == dateType_Yearly)
                {
                    yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    start = new DateTime(yearStart, 1, 1).ToString("yyyy/MM/dd");
                    end = new DateTime(yearEnd, 12, 31).ToString("yyyy/MM/dd");

                    if (itemType == tool.getCustName(1))
                    {
                        start = tool.GetPMMAStartDate(1, yearStart, dt_PMMADate).ToString("yyyy/MM/dd");
                        end = tool.GetPMMAEndDate(12, yearEnd, dt_PMMADate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        start = new DateTime(yearStart, 1, 1).ToString("yyyy/MM/dd");
                        end = new DateTime(yearEnd, 12, 31).ToString("yyyy/MM/dd");
                    }
                }
                else
                {
                    MessageBox.Show("Date invalid!");
                    return;
                }
                #endregion

                #region load transfer history

                if (isMaterialItem)
                {
                    //load material item
                    if (inOutType == inOutType_Out)
                    {
                        dt_TrfHist = dalTrfHist.MaterialInOutSearch(start, end, itemType);
                    }
                    else if (inOutType == inOutType_In || inOutType == inOutType_InOut)
                    {
                        dt_TrfHist = dalTrfHist.MaterialInOutSearch(start, end, itemType);
                    }
                    else
                    {
                        MessageBox.Show("In Out type invaild.");
                        return;
                    }
                }
                else
                {
                    //load part item
                    if (inOutType == inOutType_In)
                    {
                        //dt_TrfHist = dalTrfHist.ItemInSearch(start, end);
                        dt_TrfHist = dalTrfHist.ItemInSearch(start, end, itemType);
                    }
                    else if (inOutType == inOutType_Out)
                    {
                        dt_TrfHist = dalTrfHist.ItemToCustomerSearch(start, end, itemType);
                    }
                    else if (inOutType == inOutType_InOut)
                    {
                        dt_TrfHist = dalTrfHist.ItemInOutSearch(start, end, itemType);
                    }
                    else
                    {
                        MessageBox.Show("In Out type invaild.");
                        return;
                    }

                }
                #endregion

                #region print data
                int index = 1;
                double singleTrfQty = 0;
                double totalTrfInQty = 0;
                double totalTrfOutQty = 0;
                double singleInQty = 0;
                double singleOutQty = 0;
                bool trfIn = false;
                bool trfOut = false;
                bool dataPrinted = false;
                DataRow row_In = dt.NewRow(), row_Out = dt.NewRow();

                bool itemFound = false;

                foreach (DataRow itemRow in dt_ItemList.Rows)
                {
                    itemFound = false;
                    dataPrinted = false;
                    row_In = dt.NewRow();
                    row_Out = dt.NewRow();
                    bool itemSearchMatched = false;
                    //singleTrfQty = 0;

                    singleInQty = 0;
                    singleOutQty = 0;

                    totalTrfInQty = 0;
                    totalTrfOutQty = 0;

                    string itemCode = itemRow[dalItem.ItemCode].ToString();
                    string itemName = itemRow[dalItem.ItemName].ToString();

                    int preDay = 0;
                    int preMonth = 0;
                    int preYear = 0;

                    string itemCustOrCat = null;

                    if (cbPart.Checked)
                    {
                        itemCustOrCat = itemRow["cust_name"].ToString();
                    }
                    else
                    {
                        itemCustOrCat = itemRow[dalItem.ItemCat].ToString();
                    }

                    //set datarow info

                    foreach (DataRow trfRow in dt_TrfHist.Rows)
                    {
                        string trfItemCode = trfRow[dalItem.ItemCode].ToString();
                        string passed = trfRow[dalTrfHist.TrfResult].ToString();

                        itemSearchMatched = itemName.Contains(itemSearch.ToUpper()) || itemCode.Contains(itemSearch.ToUpper());


                        if (string.IsNullOrEmpty(itemSearch))
                        {
                            itemSearchMatched = true;

                        }

                        if (passed != "Passed")
                        {
                            itemSearchMatched = false;
                            trfRow.Delete();
                            continue;
                        }

                        if (trfItemCode == itemCode && itemSearchMatched)
                        {
                            itemFound = true;
                            string trfFrom = trfRow[dalTrfHist.TrfFrom].ToString();
                            string trfTo = trfRow[dalTrfHist.TrfTo].ToString();
                            double trfQty = double.TryParse(trfRow[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;
                            DateTime trfDate = DateTime.TryParse(trfRow[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;
                            //bool matched = false;

                            int day = 0;
                            int month = 0;
                            int year = 0;

                            if (trfDate == DateTime.MaxValue)
                            {
                                continue;
                            }
                            else
                            {
                                if (itemType == tool.getCustName(1) && dateType != dateType_Daily)
                                {
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
                                    day = trfDate.Day;
                                    month = trfDate.Month;
                                    year = trfDate.Year;
                                }

                            }

                            //check in out type
                            trfIn = CheckIfTrfIn(trfFrom, trfTo);
                            trfOut = CheckIfTrfOut(trfFrom, trfTo);

                            if ((inOutType == inOutType_In || inOutType == inOutType_InOut) && trfIn)
                            {
                                dataPrinted = true;
                                totalTrfInQty += trfQty;

                                if (dateType == dateType_Daily)
                                {
                                    if (preDay != 0 && preMonth != 0 && preYear != 0 && preDay == day && preMonth == month && preYear == year)
                                    {
                                        singleInQty += trfQty;
                                    }
                                    else
                                    {
                                        singleInQty = trfQty;
                                        singleOutQty = 0;
                                        preDay = day;
                                        preMonth = month;
                                        preYear = year;
                                    }


                                    row_In[trfDate.ToString("dd/MM yy")] = singleInQty;
                                }

                                else if (dateType == dateType_Monthly)
                                {
                                    if (preMonth != 0 && preYear != 0 && preMonth == month && preYear == year)
                                    {
                                        singleInQty += trfQty;
                                    }
                                    else
                                    {
                                        singleInQty = trfQty;

                                        singleOutQty = 0;
                                        preMonth = month;
                                        preYear = year;
                                    }

                                    row_In[month + "/" + year] = singleInQty;
                                }

                                else if (dateType == dateType_Yearly)
                                {
                                    if (preYear != 0 && preYear == year)
                                    {
                                        singleInQty += trfQty;
                                    }
                                    else
                                    {
                                        singleInQty = trfQty;
                                        singleOutQty = 0;
                                        preYear = year;
                                    }

                                    row_In[year.ToString()] = singleInQty;
                                }

                            }
                            else if ((inOutType == inOutType_Out || inOutType == inOutType_InOut) && trfOut)
                            {
                                dataPrinted = true;
                                totalTrfOutQty += trfQty;

                                if (dateType == dateType_Daily)
                                {
                                    if (preDay != 0 && preMonth != 0 && preYear != 0 && preDay == day && preMonth == month && preYear == year)
                                    {
                                        singleOutQty += trfQty;

                                    }
                                    else
                                    {
                                        singleOutQty = trfQty;
                                        singleInQty = 0;
                                        preDay = day;
                                        preMonth = month;
                                        preYear = year;
                                    }


                                    row_Out[trfDate.ToString("dd/MM yy")] = singleOutQty;
                                }

                                else if (dateType == dateType_Monthly)
                                {
                                    if (preMonth != 0 && preYear != 0 && preMonth == month && preYear == year)
                                    {
                                        singleOutQty += trfQty;
                                    }
                                    else
                                    {
                                        singleOutQty = trfQty;
                                        singleInQty = 0;
                                        preMonth = month;
                                        preYear = year;
                                    }

                                    row_Out[month + "/" + year] = singleOutQty;
                                }

                                else if (dateType == dateType_Yearly)
                                {
                                    if (preYear != 0 && preYear == year)
                                    {
                                        singleOutQty += trfQty;
                                    }
                                    else
                                    {
                                        singleOutQty = trfQty;
                                        singleInQty = 0;
                                        preYear = year;
                                    }

                                    row_Out[year.ToString()] = singleOutQty;
                                }
                            }

                            if (dataPrinted)
                            {
                                trfRow.Delete();

                            }
                        }
                        else
                        {
                            if (trfItemCode != itemCode && itemFound)
                            {
                                break;
                            }
                        }
                    }

                    dt_TrfHist.AcceptChanges();

                    if (true)
                    {
                        //print datarow to datatable
                        if (inOutType == inOutType_In)
                        {
                            if (itemType.ToUpper().Equals("ALL"))
                            {
                                row_In[header_Customer] = itemCustOrCat;
                            }

                            row_In[header_Index] = index;
                            row_In[header_ItemCode] = itemCode;
                            row_In[header_ItemName] = itemName;
                            row_In[header_Total] = totalTrfInQty;
                            dt.Rows.Add(row_In);
                            index++;
                        }
                        else if (inOutType == inOutType_Out)
                        {
                            if (itemType.ToUpper().Equals("ALL"))
                            {
                                row_Out[header_Customer] = itemCustOrCat;
                            }

                            row_Out[header_Index] = index;
                            row_Out[header_ItemCode] = itemCode;
                            row_Out[header_ItemName] = itemName;
                            row_Out[header_Total] = totalTrfOutQty;
                            dt.Rows.Add(row_Out);
                            index++;
                        }
                        else if (inOutType == inOutType_InOut)
                        {
                            if (itemType.ToUpper().Equals("ALL"))
                            {
                                row_In[header_Customer] = itemCustOrCat;
                                row_Out[header_Customer] = itemCustOrCat;
                            }

                            row_In[header_Index] = index;
                            row_In[header_ItemCode] = itemCode;
                            row_In[header_ItemName] = itemName;
                            row_In[header_Total] = totalTrfInQty;

                            row_Out[header_Index] = index;
                            row_Out[header_ItemCode] = itemCode;
                            row_Out[header_ItemName] = itemName;
                            row_Out[header_Total] = totalTrfOutQty;

                            row_In[header_Type] = inOutType_In;
                            row_Out[header_Type] = inOutType_Out;
                            dt.Rows.Add(row_In);
                            dt.Rows.Add(row_Out);
                            index++;

                            dt.Rows.Add(dt.NewRow());
                        }
                    }

                }
                #endregion

                //bind data to datagridview
                if (dt.Rows.Count > 0)
                {
                    string itemCode = dt.Rows[dt.Rows.Count - 1][header_ItemCode].ToString();

                    if (string.IsNullOrEmpty(itemCode))
                    {
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                    }
                    dt.AcceptChanges();


                }

                dgv.DataSource = dt;
                dgv.ClearSelection();
                frmLoading.CloseForm();
            }
        }

        #endregion

        #region UI Function

        private void ClearAllErrorSign()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            errorProvider8.Clear();
            errorProvider9.Clear();
        }

        private void OpenMoreFilter()
        {
            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 175f);

                dgvInOutReport.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvInOutReport.SuspendLayout();
            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 175f);

                dgvInOutReport.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvInOutReport.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void ResetUI()
        {
            dgvInOutReport.DataSource = null;

            lblReportTitle.Text = cmbInOutType.Text + " REPORT";
            ClearAllErrorSign();
        }

        private void cbMaterial_CheckedChanged(object sender, EventArgs e)
        {
            ResetUI();
            cmbItemType.DataSource = null;

            if (cbMaterial.Checked)
            {
                cbPart.Checked = false;
                tool.LoadMaterialToComboBox(cmbItemType);
            }
            else
            {
                cbPart.Checked = true;
            }
        }

        private void cbPart_CheckedChanged(object sender, EventArgs e)
        {
            ResetUI();
            cmbItemType.DataSource = null;

            if (cbPart.Checked)
            {
                cbMaterial.Checked = false;
                tool.loadCustomerToComboBox(cmbItemType);
            }
            else
            {
                cbMaterial.Checked = true;
            }
        }

        private void cmbDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded && cmbDateType.SelectedIndex >= 0)
            {
                ResetUI();
                loaded = false;
                errorProvider6.Clear();
                cmbMonthFrom.SelectedIndex = -1;
                cmbYearFrom.SelectedIndex = -1;
                cmbMonthTo.SelectedIndex = -1;
                cmbYearTo.SelectedIndex = -1;

                string dateType = cmbDateType.Text;

                if(dateType == dateType_Daily)
                {
                    cmbYearFrom.Enabled = true;
                    cmbYearTo.Enabled = false;
                    LoadYearDataToCMB(cmbYearFrom);
                    cmbYearFrom.Text = DateTime.Now.Year.ToString();

                    cmbMonthFrom.Enabled = true;
                    cmbMonthTo.Enabled = false;
                    LoadMonthDataToCMB(cmbMonthFrom);
                    cmbMonthFrom.Text = DateTime.Now.Month.ToString();

                    dtpFrom.Format = DateTimePickerFormat.Short;
                    dtpTo.Format = DateTimePickerFormat.Short;

                    dtpFrom.Enabled = true;
                    dtpTo.Enabled = true;

                    getStartandEndDate();

                }
                else if (dateType == dateType_Monthly)
                {
                    cmbYearFrom.Enabled = true;
                    cmbYearTo.Enabled = false;
                    LoadYearDataToCMB(cmbYearFrom);
                    //cmbYearFrom.Text = DateTime.Now.Year.ToString();

                    cmbMonthFrom.Enabled = true;
                    cmbMonthTo.Enabled = false;
                    LoadMonthDataToCMB(cmbMonthFrom);
                    //cmbMonthFrom.Text = DateTime.Now.Month.ToString();

                    dtpFrom.CustomFormat = " ";
                    dtpFrom.Format = DateTimePickerFormat.Custom;
                    dtpFrom.Enabled = false;

                    dtpTo.CustomFormat = " ";
                    dtpTo.Format = DateTimePickerFormat.Custom;
                    dtpTo.Enabled = false;

                }
                else if (dateType == dateType_Yearly)
                {
                    cmbYearFrom.Enabled = true;
                    cmbYearTo.Enabled = false;
                    LoadYearDataToCMB(cmbYearFrom);
                    //cmbYearFrom.Text = DateTime.Now.Year.ToString();

                    cmbMonthFrom.SelectedIndex = -1;
                    cmbMonthFrom.Enabled = false;

                    cmbMonthTo.SelectedIndex = -1;
                    cmbMonthTo.Enabled = false;

                    dtpFrom.CustomFormat = " ";
                    dtpFrom.Format = DateTimePickerFormat.Custom;
                    dtpFrom.Enabled = false;

                    dtpTo.CustomFormat = " ";
                    dtpTo.Format = DateTimePickerFormat.Custom;
                    dtpTo.Enabled = false;
                }

                loaded = true;
            }
        }

        private void cmbYearFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ResetUI();
                if (cmbDateType.Text == dateType_Monthly)
                {
                    if (cmbMonthFrom.SelectedIndex >= 0 && cmbYearFrom.SelectedIndex >= 0)
                    {
                        cmbMonthTo.Enabled = true;
                        cmbYearTo.Enabled = true;

                        LoadMonthDataToCMB(cmbMonthTo);
                        LoadYearDataToCMB(cmbYearTo, Convert.ToInt32(cmbYearFrom.Text));
                    }
                    else
                    {
                        cmbMonthTo.Enabled = false;
                        cmbYearTo.Enabled = false;

                        cmbMonthTo.DataSource = null;
                        cmbYearTo.DataSource = null;
                    }
                }
                else if (cmbDateType.Text == dateType_Yearly)
                {
                    if (cmbYearFrom.SelectedIndex >= 0)
                    {
                        cmbYearTo.Enabled = true;
                        LoadYearDataToCMB(cmbYearTo, Convert.ToInt32(cmbYearFrom.Text));
                    }
                    else
                    {
                        cmbYearTo.Enabled = false;

                        cmbYearTo.DataSource = null;
                    }
                }
                else if (cmbDateType.Text == dateType_Daily)
                {
                    getStartandEndDate();
                }
            }

        }

        private void cmbMonthFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                ResetUI();
                if(cmbDateType.Text == dateType_Monthly)
                {
                    if (cmbMonthFrom.SelectedIndex >= 0 && cmbYearFrom.SelectedIndex >= 0)
                    {
                        cmbMonthTo.Enabled = true;
                        cmbYearTo.Enabled = true;

                        LoadMonthDataToCMB(cmbMonthTo);
                        LoadYearDataToCMB(cmbYearTo, Convert.ToInt32(cmbYearFrom.Text));
                    }
                    else
                    {
                        cmbMonthTo.Enabled = false;
                        cmbYearTo.Enabled = false;
                    }
                }
                else if(cmbDateType.Text == dateType_Daily)
                {
                    getStartandEndDate();
                }
            }
        }

        private void lblMonthFromReset_Click(object sender, EventArgs e)
        {
            cmbYearTo.SelectedIndex = -1;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            cmbMonthTo.SelectedIndex = -1;
        }

        private void cmbMonthTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ResetUI();
                if (cmbDateType.Text == dateType_Monthly)
                {
                    if (cmbMonthTo.SelectedIndex >= 0 && cmbYearTo.SelectedIndex >= 0)
                    {
                        int monthFrom = Convert.ToInt32(cmbMonthFrom.Text);
                        int yearFrom = Convert.ToInt32(cmbYearFrom.Text);

                        int monthTo = Convert.ToInt32(cmbMonthTo.Text);
                        int yearTo = Convert.ToInt32(cmbYearTo.Text);

                        if(yearFrom == yearTo && monthTo < monthFrom)
                        {
                            errorProvider6.SetError(lblMonthTo, "Invalid Month!\nMonth to must greater than month from.");

                        }
                        else
                        {
                            errorProvider6.Clear();
                        }
                    }
                }
               
            }
        }

        private void cmbYearTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ResetUI();
                if (cmbDateType.Text == dateType_Monthly)
                {
                    if (cmbMonthTo.SelectedIndex >= 0 && cmbYearTo.SelectedIndex >= 0)
                    {
                        int monthFrom = Convert.ToInt32(cmbMonthFrom.Text);
                        int yearFrom = Convert.ToInt32(cmbYearFrom.Text);

                        int monthTo = Convert.ToInt32(cmbMonthTo.Text);
                        int yearTo = Convert.ToInt32(cmbYearTo.Text);

                        if (yearFrom == yearTo && monthTo < monthFrom)
                        {
                            errorProvider6.SetError(lblMonthTo, "Invalid Month!\nMonth to must greater than month from.");

                        }
                        else
                        {
                            errorProvider6.Clear();
                        }
                    }
                }

            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            ResetUI();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            ResetUI();
        }

        private void cmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetUI();
            getStartandEndDate();
            if(loaded && cbPart.Checked)
            {
                if (cmbItemType.Text.Equals(tool.getCustName(1)))
                {
                    lblChangeDate.Visible = true;
                }
                else
                {
                    lblChangeDate.Visible = false;
                }
            }
        }

        private bool CheckIfTrfIn(string trfFrom, string trfTo)
        {
            bool fromFactory = tool.IfFactoryExists(dt_Fac, trfFrom);
            bool toFactory = tool.IfFactoryExists(dt_Fac, trfTo);

            if (!fromFactory && toFactory)
            {
                return true;
            }

            return false;

        }

        private bool CheckIfTrfOut(string trfFrom, string trfTo)
        {
            bool fromFactory = tool.IfFactoryExists(dt_Fac, trfFrom);
            bool toFactory = tool.IfFactoryExists(dt_Fac, trfTo);

            if (fromFactory && !toFactory)
            {
                return true;
            }

            return false;

        }

        private bool CheckTrfInOut(string inOutType, string trfFrom, string trfTo)
        {
            bool matched = false;

            bool fromFactory = tool.IfFactoryExists(dt_Fac, trfFrom);
            bool toFactory = tool.IfFactoryExists(dt_Fac, trfTo);

            if (inOutType == inOutType_In)
            {
                if(!fromFactory && toFactory)
                {
                    matched = true;
                }
            }
            else if (inOutType == inOutType_Out)
            {
                if (fromFactory && !toFactory)
                {
                    matched = true;
                }
            }
            else if (inOutType == inOutType_InOut)
            {
                if (fromFactory && !toFactory)
                {
                    matched = true;
                }
                else if (!fromFactory && toFactory)
                {
                    matched = true;
                }
            }

            return matched;
        }

        private void lblCurrentMonth_Click(object sender, EventArgs e)
        {
            string dateType = cmbDateType.Text;

            if (dateType == dateType_Daily)
            {
                cmbMonthFrom.Text = DateTime.Now.Month.ToString();
                cmbYearFrom.Text = DateTime.Now.Year.ToString();
                getStartandEndDate();
            }
            else if (dateType == dateType_Monthly)
            {
                cmbMonthFrom.Text = DateTime.Now.Month.ToString();
                cmbYearFrom.Text = DateTime.Now.Year.ToString();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInOutData();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void cmbInOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetUI();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInOutData();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvInOutReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("hello, have a nice day! ");
        }

        private void frmInOutReport_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.InOutReportFormOpen = false;
        }


        #endregion


        #region export to excel

        private string setFileName(string start, string end)
        {
            string dateType = cmbDateType.Text;
            string itemType = cmbItemType.Text;

            return itemType + "_"+ dateType + "_"+cmbInOutType.Text+"_REPORT(From " + start + " To " + end + ")_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvInOutReport.DataSource == null)
            {
                MessageBox.Show("No data found.");
            }
            else if (Validation())
            {
                try
                {
                    dgvInOutReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    string start = null;
                    string end = null;
                    string dateType = cmbDateType.Text;
                    string itemType = cmbItemType.Text;
                    int monthStart = -1, monthEnd = -1, yearStart = -1, yearEnd = -1;

                    if (dateType == dateType_Daily)
                    {
                        start = dtpFrom.Value.ToString("yyyy_MM_dd");
                        end = dtpTo.Value.ToString("yyyy_MM_dd");
                    }
                    else if (dateType == dateType_Monthly)
                    {
                        monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                        monthEnd = int.TryParse(cmbMonthTo.Text, out monthEnd) ? monthEnd : monthStart;

                        yearStart = Convert.ToInt32(cmbYearFrom.Text);
                        yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                        if (itemType == tool.getCustName(1))
                        {
                            start = tool.GetPMMAStartDate(monthStart, yearStart, dt_PMMADate).ToString("yyyy_MM_dd");
                            end = tool.GetPMMAEndDate(monthEnd, yearEnd, dt_PMMADate).ToString("yyyy_MM_dd");
                        }
                        else
                        {
                            start = new DateTime(yearStart, monthStart, 1).ToString("yyyy_MM_dd");
                            end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd)).ToString("yyyy_MM_dd");
                        }
                    }
                    else if (dateType == dateType_Yearly)
                    {
                        yearStart = Convert.ToInt32(cmbYearFrom.Text);
                        yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                        start = new DateTime(yearStart, 1, 1).ToString("yyyy_MM_dd");
                        end = new DateTime(yearEnd, 12, 31).ToString("yyyy_MM_dd");

                        if (itemType == tool.getCustName(1))
                        {
                            start = tool.GetPMMAStartDate(1, yearStart, dt_PMMADate).ToString("yyyy_MM_dd");
                            end = tool.GetPMMAEndDate(12, yearEnd, dt_PMMADate).ToString("yyyy_MM_dd");
                        }
                        else
                        {
                            start = new DateTime(yearStart, 1, 1).ToString("yyyy_MM_dd");
                            end = new DateTime(yearEnd, 12, 31).ToString("yyyy_MM_dd");
                        }
                    }


                    SaveFileDialog sfd = new SaveFileDialog();
                    string path = @"D:\StockAssistant\Document\InOutReport";
                    Directory.CreateDirectory(path);
                    sfd.InitialDirectory = path;
                    sfd.Filter = "Excel Documents (*.xls)|*.xls";
                    sfd.FileName = setFileName(start, end);

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

                        string title = cmbItemType.Text + " " + cmbDateType.Text + " " + cmbInOutType.Text;
                        xlWorkSheet.Name = title;
                        


                        #region Save data to Sheet

                        xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&14 (" + start + "-" + end + ")" + title + " REPORT";

                        //Header and Footer setup
                        xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                        xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                        xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri,Bold\"&11 PG -&P" + "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " "+ DateTime.Now;

                        //Page setup
                        xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;


                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                        

                        xlWorkSheet.PageSetup.Zoom = false;
                        xlWorkSheet.PageSetup.CenterHorizontally = true;
                        xlWorkSheet.PageSetup.LeftMargin = 1;
                        xlWorkSheet.PageSetup.RightMargin = 1;
                        xlWorkSheet.PageSetup.FitToPagesWide = 1;
                        xlWorkSheet.PageSetup.FitToPagesTall = false;
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
                        tRange.Borders.Weight = XlBorderWeight.xlThin;
                        tRange.Font.Size = 11;
                        tRange.Font.Name = "Calibri";
                        tRange.EntireColumn.AutoFit();
                        tRange.EntireRow.AutoFit();
                        tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                        #endregion

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
                        dgvInOutReport.ClearSelection();

                        // Open the newly saved excel file
                        if (File.Exists(sfd.FileName))
                            System.Diagnostics.Process.Start(sfd.FileName);
                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }

                dgvInOutReport.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            
            
        }

        private void copyAlltoClipboard()
        {
            dgvInOutReport.SelectAll();
            DataObject dataObj = dgvInOutReport.GetClipboardContent();
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
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInOutData();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }
    }
}
