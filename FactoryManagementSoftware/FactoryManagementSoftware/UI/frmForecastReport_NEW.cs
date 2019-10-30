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

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecastReport_NEW : Form
    {
        public frmForecastReport_NEW()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvForecastReport, true);
          

            tool.loadCustomerWithoutOtherToComboBox(cmbCustomer);

            InitializeFilterData();
        }

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";

        readonly string headerParentColor = "COLOR";
        readonly string headerIndex = "#";
        readonly string headerRawMat = "RAW MATERIAL";
        readonly string headerPartName = "PART NAME";
        readonly string headerPartCode = "PART CODE";
        readonly string headerColorMat = "COLOR MATERIAL";
        readonly string headerPartWeight = "PART WEIGHT* (RUNNER)";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerEstimate = "ESTIMATE**";
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

        readonly string AssemblyMarking = "BLUE";
        readonly string ProductionMarking = "GREEN";
        readonly string ProductionAndAssemblyMarking = "PURPLE";

        private bool loaded = false;
        private bool custChanging = false;

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();

        dataTrfBLL uData = new dataTrfBLL();

        Tool tool = new Tool();

        Text text = new Text();

        DataTable dt_Join = new DataTable();
        DataTable dt_Item = new DataTable();

        private DataTable NewForecastReportTable()
        {
            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";
          
            headerBal1 = "BAL";
            headerBal2 = "BAL";
            DataTable dt = new DataTable();

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerIndex, typeof(float));
            dt.Columns.Add(headerRawMat, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerColorMat, typeof(string));
            dt.Columns.Add(headerPartWeight, typeof(string));
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
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPartWeight].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

            dgv.Columns[headerBal1].HeaderCell.Style.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[headerBal1].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[headerBal2].HeaderCell.Style.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[headerBal2].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.EnableHeadersVisualStyles = false;
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
            loaded = true;
        }

        private void LoadForecastData()
        {
            Cursor = Cursors.WaitCursor;

            lblLastUpdated.Visible = true;
            lblUpdatedTime.Visible = true;
            lblNote.Visible = true;

            lblUpdatedTime.Text = DateTime.Now.ToString();
            dgvForecastReport.DataSource = null;
            

            string keywords = cmbCustomer.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt_Data = NewForecastReportTable();
                DataRow dt_Row;

                DataTable dt = dalItemCust.custSearch(keywords);

                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(keywords).ToString());

                DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerSearch(keywords);

                DataTable dt_PMMADate = dalPmmaDate.Select();

                dt_Join = dalJoin.SelectAll();
                dt_Item = dalItem.Select();

                int index = 1;

                dt.DefaultView.Sort = "item_name ASC";

                dt = dt.DefaultView.ToTable();

                //load single part
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
                        uData.ready_stock = row[dalItem.ItemQty] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemQty]);

                        uData.forecast1 = getForecastQty(dt_ItemForecast, uData.part_code, 1);
                        uData.forecast2 = getForecastQty(dt_ItemForecast, uData.part_code, 2);
                        uData.forecast3 = getForecastQty(dt_ItemForecast, uData.part_code, 3);

                        uData.estimate = getMaxOut(uData.part_code, keywords, 6, dt_TrfHist, dt_PMMADate);
                        uData.deliveredOut = getMaxOut(uData.part_code, keywords, 0, dt_TrfHist, dt_PMMADate);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if(uData.forecast1 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate;
                            }
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " ("+(uData.rw_per_shot / uData.cavity) + ")";
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
                        //loadChild(dt_Data, uData);
                    }
                }

                //load assembly part
                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    //check if got child part also
                    if (assembly == 1 || production == 1)
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
                        uData.ready_stock = row[dalItem.ItemQty] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemQty]);

                        uData.forecast1 = getForecastQty(dt_ItemForecast, uData.part_code, 1);
                        uData.forecast2 = getForecastQty(dt_ItemForecast, uData.part_code, 2);
                        uData.forecast3 = getForecastQty(dt_ItemForecast, uData.part_code, 3);

                        uData.estimate = getMaxOut(uData.part_code, keywords, 6, dt_TrfHist, dt_PMMADate);
                        uData.deliveredOut = getMaxOut(uData.part_code, keywords, 0, dt_TrfHist, dt_PMMADate);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (uData.forecast1 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate;
                            }
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uData.index;
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

                        if(assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if(assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = ProductionMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = ProductionAndAssemblyMarking;
                        }

                        dt_Data.Rows.Add(dt_Row);
                        index++;

                        //load child
                        loadChild(dt_Data,uData, 0.1f);
                    }
                }

                if (dt_Data.Rows.Count > 0)
                {
                    dgvForecastReport.DataSource = dt_Data;
                    DgvForecastReportUIEdit(dgvForecastReport);

                    dgvForecastReport.ClearSelection();
                }
            }

            Cursor = Cursors.Arrow;
            

        }

        private void loadChild(DataTable dt_Data, dataTrfBLL uParentData, float subIndex)
        {
            DataRow dt_Row;
            dataTrfBLL uChildData = new dataTrfBLL();

            double index = uParentData.index + subIndex;

            foreach(DataRow row in dt_Join.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if(parentCode.Equals(uParentData.part_code))
                {
                    string childCode = row[dalJoin.JoinChild].ToString();

                    DataRow row_Item = tool.getDataRowFromDataTable(dt_Item, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    if(cbWithSubMat.Checked)
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
                        uChildData.ready_stock = row_Item[dalItem.ItemQty] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemQty]);

                        if(uParentData.bal1 >= 0)
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

                            if(uParentData.bal2 - uParentData.forecast3 < 0)
                            {
                                uChildData.forecast3 = (uParentData.bal2 - uParentData.forecast3) * joinQty;
                            }
                            else
                            {
                                uChildData.forecast3 = 0;
                            }
                        }
                        else
                        {
                            if(uParentData.bal1 < 0)
                            {
                                uChildData.forecast2 = uParentData.forecast2 * joinQty;
                            }
                            else
                            {
                                uChildData.forecast2 = uParentData.bal2 * -1 * joinQty;
                            }

                            uChildData.forecast3 = uParentData.forecast3 * joinQty;
                        }

                        uChildData.bal1 = uChildData.ready_stock - uChildData.forecast1;

                        uChildData.bal2 = uChildData.bal1 - uChildData.forecast2;

                        if (!uChildData.color.Equals(uChildData.color_mat) && !string.IsNullOrEmpty(uChildData.color_mat))
                        {
                            uChildData.color_mat += " (" + uParentData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uChildData.index;
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
                            loadChild(dt_Data, uChildData, subIndex/10);
                        }
                    }
                   
                }
                
            }

            
        }

        private float getForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum)
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

            if(forecastNum == 2)
            {
                month++;

                if(month > 12)
                {
                    month -= 12;
                    year++;
                }
            }
            else if(forecastNum == 3)
            {
                month += 2;

                if (month > 12)
                {
                    month -= 12;
                    year++;
                }
            }

            return tool.getItemForecast(dt_ItemForecast, itemCode, year, month);
        }

        private float getMaxOut(string itemCode, string customer, int pastMonthQty, DataTable dt_TrfHist, DataTable dt_PMMADate)
        {
            float MaxOut = 0, tmp = 0;
            bool includeCurrentMonth = false;
            if(pastMonthQty <= 0)
            {
                DateTime start = dtpOutFrom.Value;
                DateTime end = dtpOutTo.Value;

                foreach(DataRow row in dt_TrfHist.Rows)
                {
                    string item = row[dalTrfHist.TrfItemCode].ToString();
                    if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                    {
                        DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                        //string cust = row[dalTrfHist.TrfTo].ToString();
                        

                        if(trfDate >= start && trfDate <= end)
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

                if (DateTime.Today > tool.GetEndDate(currentMonth, currentYear))
                {
                    includeCurrentMonth = true;
                }

                for (int i = 0; i < pastMonthQty; i++)
                {
                    tmp = 0;

                    if(!(i == 0 && includeCurrentMonth))
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
                 
                    


                    DateTime start = tool.GetStartDate(currentMonth, currentYear, dt_PMMADate);
                    DateTime end = tool.GetEndDate(currentMonth, currentYear, dt_PMMADate);

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

            return MaxOut;
        }

        private float get6MonthsMaxOut(string itemCode, string customer)
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

        private void tlpForecast_Paint(object sender, PaintEventArgs e)
        {

        }

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

        private void frmForecastReport_NEW_Load(object sender, EventArgs e)
        {
            dgvForecastReport.SuspendLayout();

            tlpForecastReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvForecastReport.ResumeLayout();

            btnFilter.Text = textMoreFilters;


            loaded = true;
        }

        private void cmbForecastFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbForecastFrom.SelectedIndex != -1)
            {
                dgvForecastReport.DataSource = null;
                cmbForecastTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).AddMonths(2).Month);
                getStartandEndDate();
            }
        }

        private void label5_Click(object sender, EventArgs e)
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
            if(cmbCustomer.SelectedIndex != -1)
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

        private void getStartandEndDate()
        {
            if (loaded && cmbForecastFrom.SelectedIndex != -1 && cmbCustomer.SelectedIndex != -1)
            {
                string monthString = cmbForecastFrom.Text;

                int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
                int year;

                if(month < DateTime.Now.Month)
                {
                    year = DateTime.Now.Year + 1;
                }
                else
                {
                    year = DateTime.Now.Year;
                }

               
                if(cmbCustomer.Text.Equals(tool.getCustName(1)))
                {

                    //if (cmbForecastFrom.SelectedIndex != -1)
                    //{
                    //    cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                    //}
                    DateTime outTo = tool.GetEndDate(month, year);
                    
                    if(DateTime.Today > outTo && custChanging)
                    {
                        month++;

                        if(month > 12)
                        {
                            month -= 12;
                            year++;
                        }

                        cmbForecastFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    }
                    else
                    {
                        dtpOutFrom.Value = tool.GetStartDate(month, year);
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
            if(cmbForecastFrom.SelectedIndex != -1 && cmbForecastTo.SelectedIndex != -1 && loaded)
            {
                int monthFrom = DateTime.ParseExact(cmbForecastFrom.Text, "MMMM", CultureInfo.CurrentCulture).Month;
                int monthTo = DateTime.ParseExact(cmbForecastTo.Text, "MMMM", CultureInfo.CurrentCulture).Month;

                monthFrom += 2;

                if(monthFrom > 12)
                {
                    monthFrom -= 12;
                }

                if(monthFrom != monthTo)
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
            if(cmbCustomer.SelectedIndex == -1)
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
            dgvForecastReport.DataSource = null;
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
                }
            }
            else if (dgv.Columns[col].Name == headerBal1)
            {
                float bal = dgv.Rows[row].Cells[headerBal1].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[headerBal1].Value);

                if (bal < 0)
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

                if (bal < 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
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
    }

}

