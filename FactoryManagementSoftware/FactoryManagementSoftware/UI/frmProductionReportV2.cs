using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using System.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmProductionReportV2 : Form
    {
        public frmProductionReportV2()
        {
            InitializeComponent();
            InitializeFilterData();
        }

        itemDAL dalItem = new itemDAL();
        MacDAL dalMac = new MacDAL();
        planningDAL dalPlan = new planningDAL();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
        Tool tool = new Tool();
        Text text = new Text();
        ExcelTool excelTool = new ExcelTool();

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";

        private readonly string data_Code = "CODE";
        private readonly string data_Cavity = "CAVITY";
        private readonly string data_MeterStart = "METER START";
        private readonly string data_MeterEnd = "METER END";
        private readonly string data_TotalShot = "TOTAL SHOT";
        private readonly string data_Weight = "PW/SHOT (RW)";
        private readonly string data_RawMat = "RAW MAT.";
        private readonly string data_RawMatUsed = "RAW MAT. USED";
        private readonly string data_ColorMat = "COLOR MAT.";
        private readonly string data_ColorUsage = "COLOR USAGE";
        private readonly string data_ColorMatUsed = "COLOR MAT. USED";

        private readonly string header_Index = "#";
        private readonly string header_ProDate = "PRO. DATE";
        private readonly string header_ProDateFrom = "PRO. DATE FROM";
        private readonly string header_ProDateTo = "PRO. DATE TO";
        private readonly string header_PartName = "NAME";
        private readonly string header_PartCode = "CODE";
        private readonly string header_Mac = "MAC.";
        private readonly string header_Fac = "FAC.";
        private readonly string header_PlanID = "PLAN ID";
        private readonly string header_SheetID = "SHEET ID";
        private readonly string header_Shift = "SHIFT";
        private readonly string header_Produced = "PRODUCED";
        private readonly string header_RawMat = "RAW MAT.";
        private readonly string header_ColorMat = "COLOR MAT.";
        private readonly string header_Data= "DATA";
        private readonly string header_Description = "DESCRIPTION";



        private bool formLoaded = false;
        private bool recordLoaded = false;
        private bool ableLoadData = true;
        private bool ableLoadCodeData = false;

        DataTable dt_ProRecord;

        private void InitializeFilterData()
        {
            tool.loadFactoryAndAllExceptStore(cmbFac);
            
            tool.loadMacIDToComboBox(cmbMac);
            cmbMac.SelectedIndex = -1;

            ableLoadCodeData = false;
            tool.loadItemNameDataToComboBox(cmbPartName, text.Cat_Part);
            cmbPartName.SelectedIndex = -1;

            ableLoadCodeData = true;


            //ResetData();
        }

        private DataTable NewProductionRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_Fac, typeof(string));
            dt.Columns.Add(header_Mac, typeof(int));

            if (cbShowOnlyOneRowForEachPlan.Checked)
            {
                dt.Columns.Add(header_ProDateFrom, typeof(DateTime));
                dt.Columns.Add(header_ProDateTo, typeof(DateTime));
            }
            else
            {
                dt.Columns.Add(header_ProDate, typeof(DateTime));
            }
            
            dt.Columns.Add(header_PlanID, typeof(int));
            dt.Columns.Add(header_PartName, typeof(string));
            dt.Columns.Add(header_PartCode, typeof(string));

            if (!cbShowOnlyOneRowForEachPlan.Checked)
            {
                dt.Columns.Add(header_SheetID, typeof(int));
                dt.Columns.Add(header_Shift, typeof(string));
            }

            dt.Columns.Add(header_Produced, typeof(int));

            if(cbShowRawMat.Checked)
            {
                dt.Columns.Add(header_RawMat, typeof(string));
            }

            if (cbShowColorMat.Checked)
            {
                dt.Columns.Add(header_ColorMat, typeof(string));
            }

            return dt;
        }

        private DataTable NewMoreDetailTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Data, typeof(string));
            dt.Columns.Add(header_Description, typeof(string));

            return dt;
        }

        private void loadMachine()
        {
            if (cmbFac.SelectedIndex != -1)
            {
                string fac = cmbFac.Text;

                if (string.IsNullOrEmpty(fac))
                {
                    tool.loadMacIDToComboBox(cmbMac);
                    cmbMac.SelectedIndex = -1;
                }
                else if (fac.Equals("All"))
                {
                    tool.loadMacIDToComboBox(cmbMac);
                    cmbMac.SelectedIndex = -1;
                }
                else
                {
                    tool.loadMacIDByFactoryToComboBox(cmbMac, fac);
                }
            }

        }

        private void ShowOrHideFilterOption()
        {
            dgvProductionRecord.SuspendLayout();
            dgvMoreDetail.SuspendLayout();

            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 165f);

                dgvProductionRecord.ResumeLayout();
                dgvMoreDetail.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvProductionRecord.ResumeLayout();
                dgvMoreDetail.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void ShowOrHideFilterOption(bool showFilterOption)
        {
            dgvProductionRecord.SuspendLayout();
            dgvMoreDetail.SuspendLayout();

            tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvProductionRecord.ResumeLayout();
            dgvMoreDetail.ResumeLayout();

            btnFilter.Text = textMoreFilters;
        }

        private void LoadMoreDetail(int rowIndex)
        {
            if(rowIndex >= 0 && dgvProductionRecord.Rows.Count >= 1)
            {
                DataTable dt = NewMoreDetailTable();
                DataRow dt_Row;
                //need code,cavity,meter start, meter end, total shot, weight per shot, raw mat. code, raw mat used. color mat. code, color usage, color mat used
                if (cbShowOnlyOneRowForEachPlan.Checked)
                {
                    //get data by plan id
                    string planID = dgvProductionRecord.Rows[rowIndex].Cells[header_PlanID].Value.ToString();

                    bool dataFound = false;
                    int previousMeterEnd = 0;

                    foreach (DataRow row in dt_ProRecord.Rows)
                    {
                        string _planID = row[dalPlan.planID].ToString();

                        if (planID == _planID)
                        {
                            if (!dataFound)
                            {
                                dataFound = true;

                                dt_Row = dt.NewRow();

                                string itemCode = row[dalItem.ItemCode].ToString();
                                dt_Row[header_Data] = data_Code;
                                dt_Row[header_Description] = itemCode;
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                string cavity = row[dalItem.ItemCavity].ToString();
                                dt_Row[header_Data] = data_Cavity;
                                dt_Row[header_Description] = cavity;
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                int meterStart = Convert.ToInt32(row[dalProRecord.MeterStart].ToString());
                                dt_Row[header_Data] = data_MeterStart;
                                dt_Row[header_Description] = meterStart;
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                int meterEnd = Convert.ToInt32(row[dalProRecord.MeterEnd].ToString());
                                previousMeterEnd = meterEnd;
                                dt_Row[header_Data] = data_MeterEnd;
                                dt_Row[header_Description] = row[dalProRecord.MeterEnd].ToString();
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                int totalShot = meterEnd - meterStart;
                                dt_Row[header_Data] = data_TotalShot;
                                dt_Row[header_Description] = totalShot;
                                dt.Rows.Add(dt_Row);



                                dt_Row = dt.NewRow();

                                float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : 0;
                                float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : 0;

                                dt_Row[header_Data] = data_Weight;
                                dt_Row[header_Description] = partWeight.ToString("0.##") + "(" + runnerWeight.ToString("0.##") + ")" + " G";
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                dt_Row[header_Data] = data_RawMat;
                                dt_Row[header_Description] = row[dalItem.ItemMaterial].ToString();
                                dt.Rows.Add(dt_Row);

                                //dt_Row = dt.NewRow();
                                //dt_Row[header_Data] = data_RawMatUsed;
                                //dt_Row[header_Description] = "0";
                                //dt.Rows.Add(dt_Row);

                                if (!string.IsNullOrEmpty(row[dalItem.ItemMBatch].ToString()))
                                {
                                    dt_Row = dt.NewRow();
                                    dt_Row[header_Data] = data_ColorMat;
                                    dt_Row[header_Description] = row[dalItem.ItemMBatch].ToString();
                                    dt.Rows.Add(dt_Row);

                                    double colorUsage = double.TryParse(row[dalItem.ItemMBRate].ToString(), out double d) ? d : 0;
                                    dt_Row = dt.NewRow();
                                    dt_Row[header_Data] = data_ColorUsage;
                                    dt_Row[header_Description] = colorUsage.ToString("0.##");
                                    dt.Rows.Add(dt_Row);

                                    //dt_Row = dt.NewRow();
                                    //dt_Row[header_Data] = data_ColorMatUsed;
                                    //dt_Row[header_Description] = "0";
                                    //dt.Rows.Add(dt_Row);
                                }
                                else
                                {
                                    dt_Row = dt.NewRow();
                                    dt_Row[header_Data] = data_ColorMat;
                                    dt_Row[header_Description] = "NO COLOR MATERIAL";
                                    dt.Rows.Add(dt_Row);

                                }


                            }
                            else
                            {
                                int meterStart = Convert.ToInt32(row[dalProRecord.MeterStart].ToString());

                                dt.Rows[2][header_Description] = meterStart;

                                int totalShot = previousMeterEnd - meterStart;

                                dt.Rows[4][header_Description] = totalShot;

                            }



                        }
                    }

                    dgvMoreDetail.DataSource = dt;
                    dgvMoreDetail.ClearSelection();
                }
                else
                {
                    //get data by sheet id
                    string sheetID = dgvProductionRecord.Rows[rowIndex].Cells[header_SheetID].Value.ToString();


                    foreach(DataRow row in dt_ProRecord.Rows)
                    {
                        string _sheetID = row[dalProRecord.SheetID].ToString();

                        if(sheetID == _sheetID)
                        {
                            dt_Row = dt.NewRow();

                            string itemCode = row[dalItem.ItemCode].ToString();
                            dt_Row[header_Data] = data_Code;
                            dt_Row[header_Description] = itemCode;
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            string cavity = row[dalItem.ItemCavity].ToString();
                            dt_Row[header_Data] = data_Cavity;
                            dt_Row[header_Description] = cavity;
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            int meterStart = Convert.ToInt32(row[dalProRecord.MeterStart].ToString());
                            dt_Row[header_Data] = data_MeterStart;
                            dt_Row[header_Description] = meterStart;
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            int meterEnd = Convert.ToInt32(row[dalProRecord.MeterEnd].ToString());
                            dt_Row[header_Data] = data_MeterEnd;
                            dt_Row[header_Description] = row[dalProRecord.MeterEnd].ToString();
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            int totalShot = meterEnd - meterStart;
                            dt_Row[header_Data] = data_TotalShot;
                            dt_Row[header_Description] = totalShot;
                            dt.Rows.Add(dt_Row);



                            dt_Row = dt.NewRow();

                            float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : 0;
                            float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : 0;

                            dt_Row[header_Data] = data_Weight;
                            dt_Row[header_Description] = partWeight.ToString("0.##") + "(" + runnerWeight.ToString("0.##") + ")" + " G";
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            dt_Row[header_Data] = data_RawMat;
                            dt_Row[header_Description] = row[dalItem.ItemMaterial].ToString();
                            dt.Rows.Add(dt_Row);

                            //dt_Row = dt.NewRow();
                            //dt_Row[header_Data] = data_RawMatUsed;
                            //dt_Row[header_Description] = "0";
                            //dt.Rows.Add(dt_Row);

                            if (!string.IsNullOrEmpty(row[dalItem.ItemMBatch].ToString()))
                            {
                                dt_Row = dt.NewRow();
                                dt_Row[header_Data] = data_ColorMat;
                                dt_Row[header_Description] = row[dalItem.ItemMBatch].ToString();
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                dt_Row[header_Data] = data_ColorUsage;
                                dt_Row[header_Description] = row[dalItem.ItemMBRate].ToString();
                                dt.Rows.Add(dt_Row);

                                //dt_Row = dt.NewRow();
                                //dt_Row[header_Data] = data_ColorMatUsed;
                                //dt_Row[header_Description] = "0";
                                //dt.Rows.Add(dt_Row);
                            }
                            else
                            {
                                dt_Row = dt.NewRow();
                                dt_Row[header_Data] = data_ColorMat;
                                dt_Row[header_Description] = "NO COLOR MATERIAL";
                                dt.Rows.Add(dt_Row);

                            }

                            break;
                        }
                    }

                    dgvMoreDetail.DataSource = dt;
                    dgvMoreDetail.ClearSelection();
                }
            }
        }

        private void LoadProductionRecord()
        {
            Cursor = Cursors.WaitCursor;

            recordLoaded = false;

            dgvMoreDetail.DataSource = null;

            DataTable dt = NewProductionRecordTable();
            dt_ProRecord = dalProRecord.SelectWithItemInfo();
            DataTable dt_Mac = dalMac.Select();

            int index = 1;

            foreach(DataRow row in dt_ProRecord.Rows)
            {
                int macID = int.TryParse(row[dalPlan.machineID].ToString(), out macID) ? macID : 0;

                string fac = tool.getFactoryNameFromMachineID(macID.ToString(), dt_Mac);

                DateTime proDate = Convert.ToDateTime(row[dalProRecord.ProDate]).Date;

                int planID = int.TryParse(row[dalPlan.planID].ToString(), out planID) ? planID : 0;

                int sheetID = int.TryParse(row[dalProRecord.SheetID].ToString(), out sheetID) ? sheetID : 0;

                string shift = row[dalProRecord.Shift].ToString();

                string itemCode = row[dalItem.ItemCode].ToString();
                string itemName = row[dalItem.ItemName].ToString();

                int totalProduced = int.TryParse(row[dalProRecord.TotalProduced].ToString(), out totalProduced) ? totalProduced : 0;

                string rawMat = row[dalItem.ItemMaterial].ToString();
                string colorMat = row[dalItem.ItemMBatch].ToString();

                bool dataMatched = true;

                #region filter date
                DateTime dateFrom = dtpFrom.Value.Date;
                DateTime dateTo = dtpTo.Value.Date;

                if(proDate < dateFrom || proDate > dateTo)
                {
                    dataMatched = false;
                }
                #endregion

                #region filter item

                string filterName = cmbPartName.Text;
                string filterCode = cmbPartCode.Text;

                if(!string.IsNullOrEmpty(filterName) && filterName != itemName)
                {
                    dataMatched = false;
                }

                if (!string.IsNullOrEmpty(filterCode) && filterCode != itemCode)
                {
                    dataMatched = false;
                }

                #endregion

                #region filter factory

                string filterFac = cmbFac.Text;

                if(filterFac != "All" && !string.IsNullOrEmpty(filterFac) && filterFac != fac)
                {
                    dataMatched = false;
                }

                #endregion

                #region filter machine

                string filterMac = cmbMac.Text;

                if (!string.IsNullOrEmpty(filterMac) && filterMac != macID.ToString())
                {
                    dataMatched = false;
                }

                #endregion

                if (dataMatched)
                {
                    DataRow dt_Row = dt.NewRow();

                    dt_Row[header_Index] = index;
                    dt_Row[header_Fac] = fac;
                    dt_Row[header_Mac] = macID;

                    if(cbShowOnlyOneRowForEachPlan.Checked)
                    {
                        dt_Row[header_ProDateTo] = proDate;
                        dt_Row[header_ProDateFrom] = proDate;
                    }
                    else
                    {
                        dt_Row[header_ProDate] = proDate;
                        dt_Row[header_SheetID] = sheetID;
                        dt_Row[header_Shift] = shift;
                    }
                   
                    dt_Row[header_PlanID] = planID;
                    dt_Row[header_PartName] = itemName;
                    dt_Row[header_PartCode] = itemCode;
                    dt_Row[header_Produced] = totalProduced;

                    if (cbShowRawMat.Checked)
                    {
                        dt_Row[header_RawMat] = rawMat;
                    }

                    if (cbShowColorMat.Checked)
                    {
                        dt_Row[header_ColorMat] = colorMat;
                    }


                    dt.Rows.Add(dt_Row);
                    index++;
                }
            }

            #region show only 1 row for each plan

            if(cbShowOnlyOneRowForEachPlan.Checked)
            {
                DataTable dt_FilterDuplicatePlan = NewProductionRecordTable();

                string previousPlanID = null;
                int totalStockIn = 0;

                //sorting dt by planID and date
                dt.DefaultView.Sort = header_PlanID+" ASC";
                dt = dt.DefaultView.ToTable();

                foreach (DataRow row in dt.Rows)
                {
                    DateTime proDate = Convert.ToDateTime(row[header_ProDateTo]);

                    if (previousPlanID == row[header_PlanID].ToString())
                    {
                        totalStockIn += Convert.ToInt32(row[header_Produced].ToString());

                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][header_ProDateFrom] = proDate;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][header_Produced] = totalStockIn;
                    }
                    else
                    {
                        previousPlanID = row[header_PlanID].ToString();
                        totalStockIn = Convert.ToInt32(row[header_Produced].ToString());

                        dt_FilterDuplicatePlan.ImportRow(row);
                    }

                    //if(previousPlanID == null)
                    //{
                    //    previousPlanID = row[header_PlanID].ToString();
                    //    totalStockIn = Convert.ToInt32(row[header_StockIn].ToString());

                    //    dt_FilterDuplicatePlan.Rows.Add(row);
                    //}
                    //else if(previousPlanID == row[header_PlanID].ToString())
                    //{
                    //    totalStockIn += Convert.ToInt32(row[header_StockIn].ToString());
                    //    dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][header_StockIn] = totalStockIn;
                    //}
                    //else
                    //{
                    //    previousPlanID = row[header_PlanID].ToString();
                    //    totalStockIn = Convert.ToInt32(row[header_StockIn].ToString());

                    //    dt_FilterDuplicatePlan.Rows.Add(row);
                    //}

                }

                dt_FilterDuplicatePlan.DefaultView.Sort = header_Mac + " ASC, " + header_ProDateFrom + " ASC";
                dt_FilterDuplicatePlan = dt_FilterDuplicatePlan.DefaultView.ToTable();

                int indexNo = 1;
                foreach (DataRow row in dt_FilterDuplicatePlan.Rows)
                {
                    row[header_Index] = indexNo;
                    indexNo++;

                }

                dt = dt_FilterDuplicatePlan.Copy();
            }

            #endregion

            dgvProductionRecord.DataSource = dt;
            dgvProductionRecord.ClearSelection();

            recordLoaded = true;

            Cursor = Cursors.Arrow;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilterOption();
        }

        private void frmProductionReport_Load(object sender, EventArgs e)
        {
            ShowOrHideFilterOption(false);
            formLoaded = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
        }

        private void cmbFac_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
            if (cmbFac.Text != null && formLoaded && ableLoadData)
            {
                ableLoadData = false;

                loadMachine();

                ableLoadData = true;
            }
        }

        private void cmbMac_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
            if (cmbMac.Text != null && ableLoadData)
            {
                ableLoadData = false;
                cmbFac.Text = tool.getFactoryNameFromMachineID(cmbMac.Text);
                ableLoadData = true;
            }
        }

        private void ClearAllDGVData()
        {
            recordLoaded = false;
            dgvProductionRecord.DataSource = null;
            dgvMoreDetail.DataSource = null;
        }

        private void cmbPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
            if (ableLoadCodeData)
            {
                string keywords = cmbPartName.Text;

                if (!string.IsNullOrEmpty(keywords))
                {
                    DataTable dt = dalItem.nameSearch(keywords);
                    DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                    dtItemCode.DefaultView.Sort = "item_code ASC";
                    cmbPartCode.DataSource = dtItemCode;
                    cmbPartCode.DisplayMember = "item_code";

                    if(dtItemCode.Rows.Count > 1)
                    {
                        cmbPartCode.SelectedIndex = -1;
                    }
                    
                }
                else
                {
                    cmbPartCode.DataSource = null;

                }
            }
        }

        private void lblPartNameReset_Click(object sender, EventArgs e)
        {
            cmbPartName.SelectedIndex = -1;
        }

        private void lblPartCodeReset_Click(object sender, EventArgs e)
        {
            cmbPartCode.SelectedIndex = -1;
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
            DateTime from = dtpFrom.Value;
            DateTime to = dtpTo.Value;

            if(from > to)
            {
                MessageBox.Show("date from cannot later than date to.");
                dtpFrom.Value = to;
            }
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
            DateTime from = dtpFrom.Value;
            DateTime to = dtpTo.Value;

            if (to < from)
            {
                MessageBox.Show("date to cannot earlier than date from.");
                dtpTo.Value = from;
            }
        }

        private void cbShowRawMat_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
        }

        private void cbShowColorMat_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProductionRecord();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadProductionRecord();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProductionRecord();
        }

        private void dgvProductionRecord_SelectionChanged(object sender, EventArgs e)
        {
            if(recordLoaded)
            {
                if(dgvProductionRecord.CurrentCell != null)
                {
                    int rowIndex = dgvProductionRecord.CurrentCell.RowIndex;

                    
                    LoadMoreDetail(rowIndex);
                }
               
            }
            
        }

        private void dgvProductionRecord_Click(object sender, EventArgs e)
        {
            if (recordLoaded && dgvProductionRecord.CurrentCell != null)
            {
                int rowIndex = dgvProductionRecord.CurrentCell.RowIndex;

                LoadMoreDetail(rowIndex);
            }
        }

        private void frmProductionReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ProductionReportFormOpen = false;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type



            dgvProductionRecord.SelectAll();
            excelTool.ExportToExcel(text.Report_Type_Production, (DataTable) dgvProductionRecord.DataSource, dgvProductionRecord.GetClipboardContent());


            Cursor = Cursors.Arrow; // change cursor to normal type

        }
    }
}
