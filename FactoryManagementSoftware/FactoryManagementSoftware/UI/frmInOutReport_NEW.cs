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
            tool.loadCustomerToComboBox(cmbItemType);
            LoadInOutTypeCMBData(cmbInOutType);
            LoadDateTypeCMBData(cmbDateType);
        }

        #region Variable, Object, Class

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";

        private readonly string inOutType_In = "IN";
        private readonly string inOutType_Out = "OUT";
        private readonly string inOutType_InOut = "IN & OUT";

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
        Tool tool = new Tool();

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

            dt.Columns.Add(header_Total, typeof(string));


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
                        day = current.ToString("dd/MM/yy");
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
            dgv.Columns[header_ItemName].MinimumWidth = 300;
            dgv.Columns[header_Total].Frozen = true;
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
                            dtpFrom.Value = tool.GetStartDate(month, year);
                            dtpTo.Value = tool.GetEndDate(month, year);
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

        private void LoadInOutData()
        {
            if(Validation())
            {
                #region Pre Setting
                frmLoading.ShowLoadingScreen();
                DataGridView dgv = dgvInOutReport;
                DataTable dt = NewInOutTable();
                DataTable dt_ItemList;
                DataTable dt_TrfHist;
                string inOutType = cmbInOutType.Text;
                string dateType = cmbDateType.Text;
                string itemType = cmbItemType.Text;
                bool isMaterialItem = cbMaterial.Checked;
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
                    if(itemType.ToUpper().Equals("ALL"))
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
                if (dateType == dateType_Daily)
                {
                    start = dtpFrom.Value.ToString("yyyy/MM/dd");
                    end = dtpTo.Value.ToString("yyyy/MM/dd");
                }
                else if (dateType == dateType_Monthly)
                {
                    int monthStart = Convert.ToInt32(cmbMonthFrom.Text);
                    int monthEnd = int.TryParse(cmbMonthTo.Text, out monthEnd) ? monthEnd : monthStart;

                    int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    int yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    start = new DateTime(yearStart, monthStart, 1).ToString("yyyy/MM/dd");
                    end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd)).ToString("yyyy/MM/dd");

                }
                else if (dateType == dateType_Yearly)
                {
                    int yearStart = Convert.ToInt32(cmbYearFrom.Text);
                    int yearEnd = int.TryParse(cmbYearTo.Text, out yearEnd) ? yearEnd : yearStart;

                    start = new DateTime(yearStart, 1, 1).ToString("yyyy/MM/dd");
                    end = new DateTime(yearEnd, 12, 31).ToString("yyyy/MM/dd");
                }
                else
                {
                    MessageBox.Show("Date invalid!");
                    return;
                }
                #endregion

                #region load transfer history

                if(isMaterialItem)
                {
                    //load material item
                    if(inOutType == inOutType_Out)
                    {
                        dt_TrfHist = dalTrfHist.MaterialInOutSearch(start, end, itemType);
                    }
                    else if (inOutType == inOutType_In || inOutType == inOutType_InOut)
                    {
                        dt_TrfHist = dalTrfHist.MaterialOutSearch(start, end, itemType);
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
                        dt_TrfHist = dalTrfHist.ItemInSearch(start, end);
                    }
                    else if (inOutType == inOutType_Out)
                    {
                        dt_TrfHist = dalTrfHist.ItemToCustomerSearch(start, end, itemType);
                    }
                    else if (inOutType == inOutType_InOut)
                    {
                        dt_TrfHist = dalTrfHist.ItemInOutSearch(start, end);
                    }
                    else
                    {
                        MessageBox.Show("In Out type invaild.");
                        return;
                    }
                }
                #endregion

                #region print data

                //To-Do: daily

                //To-Do: monthly

                //To-Do: yearly

                #endregion
                //bind data to datagridview
                dgv.DataSource = dt;
                frmLoading.CloseForm();
            }
        }

        #endregion

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

        private void dgvInOutReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvInOutUIEdit(dgvInOutReport);
        }
    }
}
