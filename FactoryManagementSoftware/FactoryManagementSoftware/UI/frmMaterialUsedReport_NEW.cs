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
    public partial class frmMaterialUsedReport_NEW : Form
    {
        public frmMaterialUsedReport_NEW()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvMatUsedReport, true);

            tool.LoadCustomerAndAllToComboBox(cmbCustomer);
            cmbCustomer.Text = tool.getCustName(1);

            LoadDataToReportTypeCMB(cmbReportType);

            loadMonthDataToCMB(cmbMonth);
            loadYearDataToCMB(cmbYear);

            cmbMonth.Text = DateTime.Now.Month.ToString();
            cmbYear.Text = DateTime.Now.Year.ToString();

            dt_MatUsed = NewMatUsedTable();
            dt_DeliveredData = NewDeliveredDataTable();

            headerOutQty = text.Header_Delivered;
        }

        #region variable declare

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";
        private string headerOutQty = "DELIVERED";
        readonly string reportType_Delivered = "DELIVERED";
        readonly string reportType_ReadyStock = "READY STOCK";
        readonly string reportType_Forecast = "FORECAST";

        readonly string reportTitle_ActualUsed = "MATERIAL USED REPORT";
        readonly string reportTitle_Forecast = "MATERIAL USED REPORT";
        readonly string reportTitle_ReadyStock = "MATERIAL USED REPORT";

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();

        custDAL dalCust = new custDAL();
        materialDAL dalMat = new materialDAL();
        dataTrfBLL uData = new dataTrfBLL();

        Tool tool = new Tool();

        Text text = new Text();

        DataTable dt_Join = new DataTable();
        DataTable dt_Item = new DataTable();
        DataTable dt_OrginalData = new DataTable();

        private DataTable dt_MatUsed;
        private DataTable dt_MatStock;
        private DataTable dt_Mat;
        private DataTable dt_PMMA;
        private DataTable dt_DeliveredData;
        private DataTable dt_ItemForecast;
        bool loaded = false;
        bool custChanging = false;
        #endregion

        #region UI Design

        private DataTable NewDeliveredDataTable()
        {
            DataTable dt = new DataTable();

            string reportType = cmbReportType.Text;
            if (reportType.Equals(reportType_Delivered))
            {
                headerOutQty = "DELIVERED";
            }
            else if (reportType.Equals(reportType_ReadyStock))
            {
                headerOutQty = "STOCK";
            }
            else if (reportType.Equals(reportType_Forecast))
            {
                headerOutQty = cmbMonth.Text + "/" + cmbYear.Text + " FORECAST";
            }

            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(headerOutQty, typeof(float));

            return dt;
        }

        private DataTable NewMatUsedTable()
        {
            DataTable dt = new DataTable();

            string reportType = cmbReportType.Text;

            if(reportType.Equals(reportType_Delivered))
            {
                headerOutQty = "DELIVERED";
            }
            else if (reportType.Equals(reportType_ReadyStock))
            {
                headerOutQty = "STOCK";
            }
            else if (reportType.Equals(reportType_Forecast))
            {
                headerOutQty = cmbMonth.Text + "/"+cmbYear.Text+" FORECAST";
            }

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MatType, typeof(string));
            dt.Columns.Add(text.Header_MatCode, typeof(string));
            dt.Columns.Add(text.Header_MatName, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_Parent, typeof(string));
            dt.Columns.Add(headerOutQty, typeof(int));
            dt.Columns.Add(text.Header_ItemWeight_G, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsed_KG_Piece, typeof(float));
            dt.Columns.Add(text.Header_Wastage, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsedWithWastage, typeof(float));
            dt.Columns.Add(text.Header_TotalMaterialUsed_KG_Piece, typeof(float));


            return dt;
        }

        private void dgvMatUsedUIEdit(DataGridView dgv)
        {
            //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[text.Header_Parent].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            dgv.Columns[text.Header_MatType].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);


            dgv.Columns[text.Header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_PartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MatName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Parent].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerOutQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_ItemWeight_G].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Wastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MaterialUsedWithWastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerOutQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ItemWeight_G].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_Wastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_MaterialUsedWithWastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_MatName].Frozen = true;

            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 6F, FontStyle.Regular);
        }

        private void dgvMatUsedReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView dgv = dgvMatUsedReport;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == text.Header_MatCode)
            {
                string matCode = dgv.Rows[row].Cells[text.Header_MatCode].Value.ToString();

                if (matCode == "")
                {
                    dgv.Rows[row].Height = 3;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[row].Height = 50;
                }
            }

            dgv.ResumeLayout();
        }

        private void dgvMatUsedReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvMatUsedUIEdit(dgvMatUsedReport);
            dgvMatUsedReport.AutoResizeColumns();
        }

        #endregion

        #region Load Data/Validation

        private void frmMaterialUsedReport_NEW_Load(object sender, EventArgs e)
        {
            dgvMatUsedReport.SuspendLayout();

            tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvMatUsedReport.ResumeLayout();

            btnFilter.Text = textMoreFilters;


            loaded = true;
        }

        private void loadMonthDataToCMB(ComboBox cmb)
        {
            DataTable dt_month = new DataTable();

            dt_month.Columns.Add("month");

            for (int i = 1; i <= 12; i++)
            {
                dt_month.Rows.Add(i);
            }

            cmb.DataSource = dt_month;
            cmb.DisplayMember = "month";
        }

        private void loadYearDataToCMB(ComboBox cmb)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add("year");

            for (int i = 2018; i <= 2018 + 100; i++)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = "year";
        }

        private void LoadDataToReportTypeCMB(ComboBox cmb)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TYPE");
            dt.Rows.Add(reportType_Delivered);
            dt.Rows.Add(reportType_ReadyStock);
            dt.Rows.Add(reportType_Forecast);

            cmb.DataSource = dt;
            cmb.DisplayMember = "TYPE";
            cmb.SelectedIndex = 0;
        }

        private void getStartandEndDate()
        {
            if (cmbCustomer.SelectedIndex != -1)
            {
                int month = int.TryParse(cmbMonth.Text, out month) ? month : DateTime.Now.Month;
                int year = int.TryParse(cmbYear.Text, out year) ? year : DateTime.Now.Year;

                if (cmbCustomer.Text.Equals(tool.getCustName(1)))
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
        }

        private bool ifCustomerItem(string itemCode, DataTable dt_Join, DataTable dt_CustItem)
        {
            bool isCustomerItem = false;

            foreach (DataRow row in dt_CustItem.Rows)
            {
                if (itemCode == row[dalItem.ItemCode].ToString())
                {
                    return true;
                }
            }

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    string parentCode = join["parent_code"].ToString();
                    isCustomerItem = ifCustomerItem(parentCode, dt_Join, dt_CustItem);
                }
            }

            return isCustomerItem;
        }

        private float DeliveredToCustomerQty(string itemCode, DataTable dt_Join, DataTable dt_ToCustomer)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;
            row_Delivered = dt_DeliveredData.NewRow();

            foreach (DataRow trfHistRow in dt_ToCustomer.Rows)
            {
                dbItemCode = trfHistRow[dalTrfHist.TrfItemCode].ToString();
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();
                //get single out
                if (itemCode == dbItemCode && result == "Passed")
                {
                    dbItemName = trfHistRow[dalItem.ItemName].ToString();
                    int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                    DeliveredQty += qty;
                }

            }

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * DeliveredToCustomerQty(parentCode, dt_Join, dt_ToCustomer);
                }
            }

            return DeliveredQty;

        }

        private float ReadyStock(string customer, string itemCode, DataTable dt_Join, DataTable dt_ToCustomer)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;

            DataTable dt_Cust = dalCust.Select();

            row_Delivered = dt_DeliveredData.NewRow();

            foreach (DataRow trfHistRow in dt_ToCustomer.Rows)
            {
                dbItemCode = trfHistRow[dalTrfHist.TrfItemCode].ToString();
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();

                string trf_To = trfHistRow[dalTrfHist.TrfTo].ToString();

                bool matched = itemCode == dbItemCode && result == "Passed" && tool.IfCustomer(dt_Cust, trf_To);

                if (!customer.Equals("All"))
                {
                    matched = matched && trf_To.ToUpper().Equals(customer.ToUpper());
                }

                //get single out
                if (matched)
                {
                    dbItemName = trfHistRow[dalItem.ItemName].ToString();
                    int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                    DeliveredQty += qty;
                }

            }

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * DeliveredToCustomerQty(customer, parentCode, dt_Join, dt_ToCustomer);
                }
            }

            return DeliveredQty;

        }

        private float DeliveredToCustomerQty(string customer, string itemCode, DataTable dt_Join, DataTable dt_ToCustomer)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;

            DataTable dt_Cust = dalCust.Select();

            row_Delivered = dt_DeliveredData.NewRow();

            if (cmbReportType.Text.Equals(reportType_Delivered))
            {
                foreach (DataRow trfHistRow in dt_ToCustomer.Rows)
                {
                    dbItemCode = trfHistRow[dalTrfHist.TrfItemCode].ToString();
                    string result = trfHistRow[dalTrfHist.TrfResult].ToString();

                    string trf_To = trfHistRow[dalTrfHist.TrfTo].ToString();

                    bool matched = itemCode == dbItemCode && result == "Passed" && tool.IfCustomer(dt_Cust, trf_To);

                    if (!customer.Equals("All"))
                    {
                        matched = matched && trf_To.ToUpper().Equals(customer.ToUpper());
                    }

                    //get single out
                    if (matched)
                    {
                        dbItemName = trfHistRow[dalItem.ItemName].ToString();
                        int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                        DeliveredQty += qty;

                    }

                }
            }

            else if (cmbReportType.Text.Equals(reportType_ReadyStock))
            {
                DeliveredQty = tool.getStockQtyFromDataTable(dt_Item, itemCode);
                dbItemName = tool.getItemNameFromDataTable(dt_Item, itemCode);
            }
            else if (cmbReportType.Text.Equals(reportType_Forecast))
            {
                DeliveredQty = tool.getItemForecast(dt_ItemForecast, itemCode, Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.Text));
                dbItemName = tool.getItemNameFromDataTable(dt_Item, itemCode);

                if (DeliveredQty < 0)
                    DeliveredQty = 0;
            }


            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[headerOutQty] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * DeliveredToCustomerQty(customer, parentCode, dt_Join, dt_ToCustomer);
                }
            }

            return DeliveredQty;
        }

        private bool ifZeroCostMat(string matCode, DataTable dt_ZeroCostMat)
        {
            foreach (DataRow row in dt_ZeroCostMat.Rows)
            {
                if (matCode == row[dalMat.MatCode].ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private void LoadMatUsedData_2()
        {
            frmLoading.ShowLoadingScreen();
            dgvMatUsedReport.DataSource = null;

            #region Datatable Data

            //get all pmma item
            string custName = cmbCustomer.Text;

            //get all transfer history(for in out)
            string from = dtpFrom.Value.ToString("yyyy/MM/dd");
            string to = dtpTo.Value.ToString("yyyy/MM/dd");
            DataTable dt_PartTrfHist;
            DataTable dt_CustItem;

            if (custName.Equals("All"))
            {
                dt_CustItem = dalItemCust.Select();
                dt_PartTrfHist = dalTrfHist.rangeItemToAllCustomerSearch(from, to);
                dt_ItemForecast = dalItemForecast.Select();
            }
            else
            {
                dt_CustItem = dalItemCust.custSearch(custName);
                dt_PartTrfHist = dalTrfHist.rangeItemToCustomerSearch(custName, from, to);
                dt_ItemForecast = dalItemForecast.Select(tool.getCustID(custName).ToString());
            }

            if (cbZeroCostOnly.Checked)
            {
                //get zero cost material list
                dt_Mat = dalMat.SelectZeroCostMaterial();
            }
            else
            {
                dt_Mat = dalMat.Select();
            }
            //create datatable
            dt_MatUsed = NewMatUsedTable();
            DataRow row_DGVSouce;

            //get all item info
            dt_Item = dalItem.Select();

            //get all part list
            DataTable dt_Part = dalItem.catSelect(text.Cat_Part);
            DataTable dt_PartForChecking = dt_Part.Copy();

            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();
            DataTable dt_JoinforChecking = dt_Join.Copy();

            #endregion

            #region variable

            int index = 1;
            int dgvRowIndex = 0;
            string previousMatCode = null;
            float totalMatUsed = 0;
            bool printed = false;

            #endregion

            //foreach material list
            foreach (DataRow matRow in dt_Mat.Rows)
            {
                string matCode = matRow[dalMat.MatCode].ToString();
                string matName = matRow[dalMat.MatName].ToString();
                string matCat = matRow[dalMat.MatCat].ToString();

                bool itemCatMatched = matCat == text.Cat_RawMat || matCat == text.Cat_MB || matCat == text.Cat_Pigment || matCat == text.Cat_SubMat;

                if (itemCatMatched)
                {
                    if (previousMatCode == null)
                    {
                        previousMatCode = matCode;
                        totalMatUsed = 0;
                    }

                    else if (previousMatCode != matCode && printed)
                    {
                        string printedMatCode = dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_MatCode].ToString();

                        if (dgvRowIndex - 1 >= 0 && !string.IsNullOrEmpty(printedMatCode))
                        {
                            dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);
                            row_DGVSouce = dt_MatUsed.NewRow();
                            dt_MatUsed.Rows.Add(row_DGVSouce);
                            totalMatUsed = 0;
                            previousMatCode = matCode;
                            dgvRowIndex++;
                        }

                    }

                    bool itemMatch = false;

                    if (matCat == text.Cat_RawMat && cbRawMat.Checked)
                    {
                        itemMatch = true;
                    }
                    else if (matCat == text.Cat_MB && cbMasterBatch.Checked)
                    {
                        itemMatch = true;
                    }
                    else if (matCat == text.Cat_Pigment && cbPigment.Checked)
                    {
                        itemMatch = true;
                    }


                    if (matCat == text.Cat_SubMat && cbSubMat.Checked)
                    {
                        //foreach all join list (for sub material)
                        foreach (DataRow joinRow in dt_Join.Rows)
                        {
                            if (joinRow.RowState != DataRowState.Deleted)
                            {
                                string childCode = joinRow["child_code"].ToString();

                                if (childCode == matCode)
                                {
                                    string parentCode = joinRow["parent_code"].ToString();

                                    if (ifCustomerItem(parentCode, dt_JoinforChecking, dt_CustItem))
                                    {
                                        string parentName = joinRow["parent_name"].ToString();


                                        //Get delivered out data
                                        dt_DeliveredData.Rows.Clear();
                                        float reference_Value = DeliveredToCustomerQty(custName, parentCode, dt_JoinforChecking, dt_PartTrfHist);

                                        #region get weight data
                                        float partWeight = 0;
                                        float runnerWeight = 0;

                                        if (cbZeroCostOnly.Checked)
                                        {
                                            partWeight = float.TryParse(joinRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(joinRow[dalItem.ItemQuoRWPcs].ToString(), out partWeight) ? partWeight : 0;
                                        }
                                        else
                                        {
                                            partWeight = float.TryParse(joinRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(joinRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                        }

                                        float itemWeight = partWeight + runnerWeight;
                                        #endregion

                                        float wastage = Convert.ToSingle(matRow[dalItem.ItemWastage]); // tool.getItemWastageAllowedFromDataTable(dt_Item, matCode);

                                        float wastageAdd = reference_Value * wastage;
                                        int MatUsedWithWastage = (int)Math.Ceiling(reference_Value + wastageAdd);
                                        totalMatUsed += MatUsedWithWastage;

                                        #region New Test: with parent code/name
                                        foreach (DataRow row in dt_DeliveredData.Rows)
                                        {
                                            string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();
                                            string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();

                                            float qty = Convert.ToSingle(row[headerOutQty]);
                                            string parent = DeliveredName + "(" + DeliveredCode + ")";

                                            if ((DeliveredName != null && DeliveredCode != null) || (reference_Value == 0 && parentCode == DeliveredCode))
                                            {

                                                if (DeliveredCode == parentCode)
                                                {
                                                    parent = "";
                                                }

                                                //matUsedInKG = qty * itemWeight / 1000;

                                                wastageAdd = qty * wastage;
                                                MatUsedWithWastage = (int)Math.Ceiling(qty + wastageAdd);

                                                row_DGVSouce = dt_MatUsed.NewRow();
                                                row_DGVSouce[text.Header_MatType] = matCat;
                                                row_DGVSouce[text.Header_Index] = index;
                                                row_DGVSouce[text.Header_MatCode] = matCode;
                                                row_DGVSouce[text.Header_MatName] = matName;
                                                row_DGVSouce[text.Header_PartName] = parentName;
                                                row_DGVSouce[text.Header_PartCode] = parentCode;
                                                row_DGVSouce[text.Header_Parent] = parent;
                                                row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                                row_DGVSouce[text.Header_Wastage] = wastage;
                                                row_DGVSouce[headerOutQty] = Math.Round(qty, 2);
                                                row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = qty;
                                                row_DGVSouce[text.Header_MaterialUsedWithWastage] = MatUsedWithWastage;

                                                dt_MatUsed.Rows.Add(row_DGVSouce);
                                                dgvRowIndex++;
                                                index++;
                                                printed = true;
                                            }
                                        }
                                        //joinRow.Delete();

                                        #endregion



                                    }
                                }
                            }
                        }
                        dt_Join.AcceptChanges();
                    }

                    else if (itemMatch)
                    {
                        //foreach all part list(for raw material)
                        foreach (DataRow partRow in dt_Part.Rows)
                        {
                            if (partRow.RowState != DataRowState.Deleted)
                            {
                                string itemMatCode = partRow[dalItem.ItemMaterial].ToString();

                                if (matCat != text.Cat_RawMat)
                                {
                                    itemMatCode = partRow[dalItem.ItemMBatch].ToString();
                                }

                                if (itemMatCode == matCode)
                                {
                                    string itemCode = partRow[dalItem.ItemCode].ToString();


                                    if (ifCustomerItem(itemCode, dt_JoinforChecking, dt_CustItem))
                                    {
                                        string itemName = partRow[dalItem.ItemName].ToString();

                                        float partWeight = 0;
                                        float runnerWeight = 0;

                                        if (cbZeroCostOnly.Checked)
                                        {
                                            partWeight = float.TryParse(partRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(partRow[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                                            if (partWeight <= 0)
                                            {
                                                partWeight = float.TryParse(partRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                                runnerWeight = float.TryParse(partRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                                            }

                                        }
                                        else
                                        {
                                            partWeight = float.TryParse(partRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                            runnerWeight = float.TryParse(partRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                                        }

                                        float itemWeight = partWeight + runnerWeight;

                                        float wastage = partRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(partRow[dalItem.ItemWastage]);

                                        //Get delivered out data
                                        dt_DeliveredData.Rows.Clear();
                                        float deliveredQty = DeliveredToCustomerQty(custName, itemCode, dt_JoinforChecking, dt_PartTrfHist);
                                        float matUsedInKG = deliveredQty * itemWeight / 1000;
                                        totalMatUsed += (float)Math.Round(matUsedInKG + matUsedInKG * wastage, 2);


                                        #region New Test: with parent code/name
                                        foreach (DataRow row in dt_DeliveredData.Rows)
                                        {
                                            string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();

                                            //if(itemCode == "V99CJP0M0")
                                            //{
                                            //    float TEST = 0;
                                            //}
                                            string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();
                                            float qty = Convert.ToSingle(row[headerOutQty]);
                                            string parent = DeliveredName + "(" + DeliveredCode + ")";

                                            if ((DeliveredName != null && DeliveredCode != null) || (deliveredQty == 0 && itemCode == DeliveredCode))
                                            {
                                                row_DGVSouce = dt_MatUsed.NewRow();

                                                if (DeliveredCode == itemCode)
                                                {
                                                    parent = "";
                                                }

                                                matUsedInKG = qty * itemWeight / 1000;

                                                row_DGVSouce[text.Header_Index] = index;
                                                row_DGVSouce[text.Header_MatType] = matCat;
                                                row_DGVSouce[text.Header_MatCode] = matCode;
                                                row_DGVSouce[text.Header_MatName] = matName;
                                                row_DGVSouce[text.Header_PartName] = itemName;
                                                row_DGVSouce[text.Header_PartCode] = itemCode;
                                                row_DGVSouce[text.Header_Parent] = parent;
                                                row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                                row_DGVSouce[text.Header_Wastage] = wastage;
                                                row_DGVSouce[headerOutQty] = Math.Round(qty, 2);
                                                row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = Math.Round(matUsedInKG, 2);
                                                row_DGVSouce[text.Header_MaterialUsedWithWastage] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

                                                dt_MatUsed.Rows.Add(row_DGVSouce);
                                                index++;
                                                dgvRowIndex++;
                                                printed = true;
                                            }
                                        }

                                        //partRow.Delete();
                                        #endregion
                                    }
                                }

                                if (!ifZeroCostMat(itemMatCode, dt_Mat))
                                {
                                    //partRow.Delete();
                                }
                            }
                        }
                        //dt_Part.AcceptChanges();
                    }
                }

            }

            //last row 
            string printedMatCode2 = dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_MatCode].ToString();
            if (dgvRowIndex - 1 >= 0 && !string.IsNullOrEmpty(printedMatCode2))
            {
                dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);
            }

            //set data source to datagridview
            if (dt_MatUsed.Rows.Count > 0)
            {
                dgvMatUsedReport.DataSource = dt_MatUsed;
                dgvMatUsedReport.ClearSelection();
            }

            frmLoading.CloseForm();
        }

        private void LoadMatUsedData()
        {
            #region Datatable Data

            //get all pmma item
            string PMMA2 = tool.getCustName(1);
            DataTable dt_PMMAItem = dalItemCust.custSearch(PMMA2);

            //create datatable
            dt_MatUsed = NewMatUsedTable();
            DataRow row_DGVSouce;

            //get all item info
            dt_Item = dalItem.Select();

            //get zero cost material list
            dt_Mat = dalMat.SelectZeroCostMaterial();

            //get all part list
            DataTable dt_Part = dalItem.catSelect(text.Cat_Part);
            DataTable dt_PartForChecking = dt_Part.Copy();

            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();
            DataTable dt_JoinforChecking = dt_Join.Copy();

            //get all transfer history(for in out)
            string from = dtpFrom.Value.ToString("yyyy/MM/dd");
            string to = dtpTo.Value.ToString("yyyy/MM/dd");
            string PMMA = tool.getCustName(1);

            DataTable dt_PartTrfToPMMAHist = dalTrfHist.rangeItemToCustomerSearch(PMMA, from, to);
            #endregion

            int index = 1;
            int dgvRowIndex = 0;
            string previousMatCode = null;
            float totalMatUsed = 0;
            //foreach material list
            foreach (DataRow matRow in dt_Mat.Rows)
            {
                string matCode = matRow[dalMat.MatCode].ToString();
                string matName = matRow[dalMat.MatName].ToString();
                string matCat = matRow[dalMat.MatCat].ToString();

                if (previousMatCode == null)
                {
                    previousMatCode = matCode;
                    totalMatUsed = 0;
                }

                else if (previousMatCode != matCode)
                {
                    if (dgvRowIndex - 1 >= 0)
                        dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);

                    totalMatUsed = 0;

                    previousMatCode = matCode;
                    row_DGVSouce = dt_MatUsed.NewRow();
                    dt_MatUsed.Rows.Add(row_DGVSouce);
                    dgvRowIndex++;
                }

                if (matCat == text.Cat_SubMat)
                {
                    //foreach all join list (for sub material)
                    foreach (DataRow joinRow in dt_Join.Rows)
                    {
                        if (joinRow.RowState != DataRowState.Deleted)
                        {
                            string childCode = joinRow["child_code"].ToString();

                            if (childCode == matCode)
                            {
                                string parentCode = joinRow["parent_code"].ToString();

                                if (ifCustomerItem(parentCode, dt_JoinforChecking, dt_PMMAItem))
                                {

                                    if (matCode == "V44K61000")
                                    {
                                        float TEST = 0;
                                    }

                                    string parentName = joinRow["parent_name"].ToString();

                                    //Get delivered out data
                                    dt_DeliveredData.Rows.Clear();
                                    float deliveredQty = DeliveredToCustomerQty(parentCode, dt_JoinforChecking, dt_PartTrfToPMMAHist);

                                    float itemWeight = Convert.ToSingle(joinRow[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(joinRow[dalItem.ItemQuoRWPcs].ToString());

                                    if (itemWeight <= 0)
                                    {
                                        itemWeight = Convert.ToSingle(joinRow[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(joinRow[dalItem.ItemProRWPcs].ToString());
                                    }

                                    //float matUsedInKG = deliveredQty * itemWeight / 1000;
                                    float wastage = tool.getItemWastageAllowedFromDataTable(dt_Item, matCode);
                                    //int MatUsedWithWastage = (int)Math.Ceiling(deliveredQty + deliveredQty * wastage);

                                    float wastageAdd = deliveredQty * wastage;
                                    int MatUsedWithWastage = (int)Math.Ceiling(deliveredQty + wastageAdd);
                                    totalMatUsed += MatUsedWithWastage;

                                    #region New Test: with parent code/name
                                    foreach (DataRow row in dt_DeliveredData.Rows)
                                    {
                                        string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();
                                        string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();

                                        float qty = Convert.ToSingle(row[headerOutQty]);
                                        string parent = DeliveredName + "(" + DeliveredCode + ")";

                                        if ((DeliveredName != null && DeliveredCode != null) || (deliveredQty == 0 && parentCode == DeliveredCode))
                                        {

                                            if (DeliveredCode == parentCode)
                                            {
                                                parent = "";
                                            }

                                            //matUsedInKG = qty * itemWeight / 1000;

                                            wastageAdd = qty * wastage;
                                            MatUsedWithWastage = (int)Math.Ceiling(qty + wastageAdd);

                                            row_DGVSouce = dt_MatUsed.NewRow();
                                            row_DGVSouce[text.Header_Index] = index;
                                            row_DGVSouce[text.Header_MatCode] = matCode;
                                            row_DGVSouce[text.Header_MatName] = matName;
                                            row_DGVSouce[text.Header_PartName] = parentName;
                                            row_DGVSouce[text.Header_PartCode] = parentCode;
                                            row_DGVSouce[text.Header_Parent] = parent;
                                            row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                            row_DGVSouce[text.Header_Wastage] = wastage;
                                            row_DGVSouce[headerOutQty] = Math.Round(qty, 2);
                                            row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = qty;
                                            row_DGVSouce[text.Header_MaterialUsedWithWastage] = MatUsedWithWastage;

                                            dt_MatUsed.Rows.Add(row_DGVSouce);
                                            dgvRowIndex++;
                                            index++;
                                        }
                                    }
                                    //joinRow.Delete();

                                    #endregion



                                }
                            }
                        }
                    }
                    dt_Join.AcceptChanges();
                }
                else
                {
                    //foreach all part list(for raw material)
                    foreach (DataRow partRow in dt_Part.Rows)
                    {
                        if (partRow.RowState != DataRowState.Deleted)
                        {
                            string itemMatCode = partRow[dalItem.ItemMaterial].ToString();

                            if (itemMatCode == matCode)
                            {
                                string itemCode = partRow[dalItem.ItemCode].ToString();


                                //check if item belong to PMMA
                                if (ifCustomerItem(itemCode, dt_JoinforChecking, dt_PMMAItem))
                                {
                                    string itemName = partRow[dalItem.ItemName].ToString();

                                    float itemWeight = Convert.ToSingle(partRow[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(partRow[dalItem.ItemQuoRWPcs].ToString());

                                    if (itemWeight <= 0)
                                    {
                                        itemWeight = Convert.ToSingle(partRow[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(partRow[dalItem.ItemProRWPcs].ToString());
                                    }

                                    float wastage = partRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(partRow[dalItem.ItemWastage]);

                                    //Get delivered out data
                                    dt_DeliveredData.Rows.Clear();
                                    float deliveredQty = DeliveredToCustomerQty(itemCode, dt_JoinforChecking, dt_PartTrfToPMMAHist);
                                    float matUsedInKG = deliveredQty * itemWeight / 1000;
                                    totalMatUsed += (float)Math.Round(matUsedInKG + matUsedInKG * wastage, 2);


                                    #region New Test: with parent code/name
                                    foreach (DataRow row in dt_DeliveredData.Rows)
                                    {
                                        string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();

                                        //if(itemCode == "V99CJP0M0")
                                        //{
                                        //    float TEST = 0;
                                        //}
                                        string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();
                                        float qty = Convert.ToSingle(row[headerOutQty]);
                                        string parent = DeliveredName + "(" + DeliveredCode + ")";

                                        if ((DeliveredName != null && DeliveredCode != null) || (deliveredQty == 0 && itemCode == DeliveredCode))
                                        {
                                            row_DGVSouce = dt_MatUsed.NewRow();

                                            if (DeliveredCode == itemCode)
                                            {
                                                parent = "";
                                            }

                                            matUsedInKG = qty * itemWeight / 1000;

                                            row_DGVSouce[text.Header_Index] = index;
                                            row_DGVSouce[text.Header_MatCode] = matCode;
                                            row_DGVSouce[text.Header_MatName] = matName;
                                            row_DGVSouce[text.Header_PartName] = itemName;
                                            row_DGVSouce[text.Header_PartCode] = itemCode;
                                            row_DGVSouce[text.Header_Parent] = parent;
                                            row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                            row_DGVSouce[text.Header_Wastage] = wastage;
                                            row_DGVSouce[headerOutQty] = Math.Round(qty, 2);
                                            row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = Math.Round(matUsedInKG, 2);
                                            row_DGVSouce[text.Header_MaterialUsedWithWastage] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

                                            dt_MatUsed.Rows.Add(row_DGVSouce);
                                            index++;
                                            dgvRowIndex++;
                                        }
                                    }

                                    //partRow.Delete();
                                    #endregion
                                }
                            }

                            if (!ifZeroCostMat(itemMatCode, dt_Mat))
                            {
                                //partRow.Delete();
                            }
                        }
                    }
                    //dt_Part.AcceptChanges();
                }
            }

            if (dgvRowIndex - 1 >= 0)
                dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);

            if (dt_MatUsed.Rows.Count > 0)
            {
                dgvMatUsedReport.DataSource = dt_MatUsed;
                dgvMatUsedReport.ClearSelection();
            }

        }

        private void frmMaterialUsedReport_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MaterialUsedReportFormOpen = false;
        }

        #endregion

        #region UI Action

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvMatUsedReport.SuspendLayout();
            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 175f);

                dgvMatUsedReport.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvMatUsedReport.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cust = cmbCustomer.Text;

            if (cust.Equals(tool.getCustName(1)))
            {
                cbZeroCostOnly.Checked = true;
            }
            else
            {
                cbZeroCostOnly.Checked = false;
            }

            custChanging = true;
            dgvMatUsedReport.DataSource = null;

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

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reportType = cmbReportType.Text;
            dt_DeliveredData = NewDeliveredDataTable();
            if (reportType.Equals(reportType_Delivered))
            {
                gbMonthYear.Enabled = true;
                gbDatePeriod.Enabled = true;

                dtpFrom.Format = DateTimePickerFormat.Short;
                dtpTo.Format = DateTimePickerFormat.Short;

                cmbMonth.Text = DateTime.Now.Month.ToString();
                cmbYear.Text = DateTime.Now.Year.ToString();
                //getStartandEndDate();

                lblReportTitle.Text = reportTitle_ActualUsed;
            }
            else if (reportType.Equals(reportType_ReadyStock))
            {
                gbMonthYear.Enabled = false;
                gbDatePeriod.Enabled = false;

                cmbMonth.SelectedIndex = -1;
                cmbYear.SelectedIndex = -1;

                dtpFrom.CustomFormat = " ";
                dtpFrom.Format = DateTimePickerFormat.Custom;

                dtpTo.CustomFormat = " ";
                dtpTo.Format = DateTimePickerFormat.Custom;

                lblReportTitle.Text = reportTitle_ReadyStock;
            }
            else if (reportType.Equals(reportType_Forecast))
            {
                gbMonthYear.Enabled = true;
                gbDatePeriod.Enabled = false;
                cmbMonth.Text = DateTime.Now.Month.ToString();
                cmbYear.Text = DateTime.Now.Year.ToString();

                dtpFrom.CustomFormat = " ";
                dtpFrom.Format = DateTimePickerFormat.Custom;

                dtpTo.CustomFormat = " ";
                dtpTo.Format = DateTimePickerFormat.Custom;

                lblReportTitle.Text = reportTitle_Forecast;
            }
        }

        private void lblCurrentMonth_Click(object sender, EventArgs e)
        {
            cmbMonth.Text = DateTime.Now.Month.ToString();
            cmbYear.Text = DateTime.Now.Year.ToString();
            getStartandEndDate();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedIndex != -1)
            {
                dgvMatUsedReport.DataSource = null;
                getStartandEndDate();
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.SelectedIndex != -1)
            {
                dgvMatUsedReport.DataSource = null;
                getStartandEndDate();
            }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else
            {
                LoadMatUsedData_2();
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
                LoadMatUsedData_2();
            }
        }

        private void lblZeroCostOnly_Click(object sender, EventArgs e)
        {
            dgvMatUsedReport.DataSource = null;
            if (cbZeroCostOnly.Checked)
            {
                cbZeroCostOnly.Checked = false;
            }
            else
            {
                cbZeroCostOnly.Checked = true;
            }
        }

        private void cbRawMat_CheckedChanged(object sender, EventArgs e)
        {
            dgvMatUsedReport.DataSource = null;
        }

        private void cbMasterBatch_CheckedChanged(object sender, EventArgs e)
        {
            dgvMatUsedReport.DataSource = null;
        }

        private void cbPigment_CheckedChanged(object sender, EventArgs e)
        {
            dgvMatUsedReport.DataSource = null;
        }

        private void cbSubMat_CheckedChanged(object sender, EventArgs e)
        {
            dgvMatUsedReport.DataSource = null;
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            if (cmbReportType.Text.Equals(reportType_Delivered))
            {
                return cmbReportType.Text + "_MaterialUsedReport " + "(" + cmbCustomer.Text + "_" + cmbMonth.Text + "_" + cmbYear.Text + ")_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

            }
            else if (cmbReportType.Text.Equals(reportType_ReadyStock))
            {
                return cmbReportType.Text + "_MaterialUsedReport " + "(" + cmbCustomer.Text + ")_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

            }
            else
            {
                return cmbReportType.Text + "_MaterialUsedReport " + "(" + cmbCustomer.Text + "_" + cmbMonth.Text + "_" + cmbYear.Text + ")_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string path = @"D:\StockAssistant\Document\MaterialUsedReport";
            Directory.CreateDirectory(path);
            sfd.InitialDirectory = path;
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                // Copy DataGridView results to clipboard
                copyAlltoClipboard();
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                xlexcel.PrintCommunication = false;
                xlexcel.ScreenUpdating = false;
                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                xlexcel.Calculation = XlCalculation.xlCalculationManual;
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Name = cmbCustomer.Text;

                #region Save data to Sheet

                //Header and Footer setup

                if (cmbReportType.Text.Equals(reportType_Delivered))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dtpFrom.Text + ">" + dtpTo.Text;
                }
                else if (cmbReportType.Text.Equals(reportType_ReadyStock))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                }
                else if (cmbReportType.Text.Equals(reportType_Forecast))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + cmbMonth.Text +"/"+cmbYear.Text + " FORECAST";
                }

                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCustomer.Text + ")" + cmbReportType.Text + "_MATERIAL USED REPORT";
                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

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
                //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                //tRange.Borders.Weight = XlBorderWeight.xlThin;
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
                dgvMatUsedReport.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void copyAlltoClipboard()
        {
            dgvMatUsedReport.SelectAll();
            DataObject dataObj = dgvMatUsedReport.GetClipboardContent();
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

       
    }
}
