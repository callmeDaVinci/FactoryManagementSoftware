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
using Microsoft.Office.Interop.Word;
using System.Web.UI.WebControls;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmProductionReportV2 : Form
    {
        public frmProductionReportV2()
        {
            InitializeComponent();
            InitializeFilterData();
            tool.DoubleBuffered(dgvMainList, true);
            tool.DoubleBuffered(dgvSubList, true);
        }

        #region variable declare
        itemDAL dalItem = new itemDAL();
        MacDAL dalMac = new MacDAL();
        planningDAL dalPlan = new planningDAL();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
        Tool tool = new Tool();
        Text text = new Text();
        ExcelTool excelTool = new ExcelTool();

        private string textMoreFilters = "MORE FILTERS";
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

        //private readonly string header_Index = "#";
        //private readonly string header_ProDate = "PRO. DATE";
        //private readonly string header_ProDateFrom = "PRO. DATE FROM";
        //private readonly string header_ProDateTo = "PRO. DATE TO";
        //private readonly string header_PartName = "NAME";
        //private readonly string header_PartCode = "CODE";
        //private readonly string header_Mac = "MAC.";
        //private readonly string header_Fac = "FAC.";
        //private readonly string header_PlanID = "PLAN ID";
        //private readonly string header_SheetID = "SHEET ID";
        //private readonly string header_Shift = "SHIFT";
        //private readonly string header_Produced = "PRODUCED";
        //private readonly string header_RawMat = "RAW MAT.";
        //private readonly string header_ColorMat = "COLOR MAT.";
        //private readonly string header_Data= "DATA";
        //private readonly string header_Description = "DESCRIPTION";



        private bool formLoaded = false;
        private bool recordLoaded = false;
        private bool ableLoadData = true;
        private bool ableLoadCodeData = false;

        DataTable DT_JOBSHEETRECORD;
        DataTable DT_METERRECORD;

        #endregion

        private void InitializeFilterData()
        {
            loadReportType(cmbReportType);
            tool.loadOUGProductionFactory(cmbFac);

            loadMachine(cmbMac);

            //tool.loadMacIDToComboBox(cmbMac);
            cmbMac.SelectedIndex = -1;

            ableLoadCodeData = false;
            //tool.loadItemNameDataToComboBox(cmbPartName, text.Cat_Part);
            //cmbPartName.SelectedIndex = -1;

            ableLoadCodeData = true;

            dtpFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            //ResetData();
            //loadSubListType(cmbSubListType);

        }

        private void loadReportType(ComboBox cmb)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(text.Header_Type);
            dt.Rows.Add(text.ReportType_ByJobNo);
            dt.Rows.Add(text.ReportType_ByDateAndShift);

            cmb.DataSource = dt;
            cmb.DisplayMember = text.Header_Type;
            cmb.SelectedIndex = 0;
        }

        private void loadSubListType(ComboBox cmb)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(text.Header_Type);
            dt.Rows.Add(text.ReportType_NoShow);

            if (cmbReportType.Text == text.ReportType_ByJobNo)
            {
                dt.Rows.Add(text.ReportType_SheetList);

               
            }
            else if (cmbReportType.Text == text.ReportType_ByDateAndShift)
            {
                dt.Rows.Add(text.ReportType_SheetData);
            }

            cmb.DataSource = dt;
            cmb.DisplayMember = text.Header_Type;
            cmb.SelectedIndex = 0;
        }

        private DataTable NewProductionRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));

            dt.Columns.Add(text.Header_JobNo, typeof(int));

            dt.Columns.Add(text.Header_Fac, typeof(string));
            dt.Columns.Add(text.Header_Mac, typeof(int));

            if (cmbReportType.Text == text.ReportType_ByJobNo)
            {
                dt.Columns.Add(text.Header_DateFrom, typeof(DateTime));
                dt.Columns.Add(text.Header_DateTo, typeof(DateTime));
            }
            else
            {
                dt.Columns.Add(text.Header_Date, typeof(DateTime));
                dt.Columns.Add(text.Header_SheetID, typeof(int));
                dt.Columns.Add(text.Header_Shift, typeof(string));
            }
            
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));

            dt.Columns.Add(text.Header_MaxOutput, typeof(int));

            dt.Columns.Add(text.Header_TotalProduced, typeof(int));
           //dt.Columns.Add(text.Header_TotalStockIn, typeof(int));
            
            dt.Columns.Add(text.Header_TotalReject, typeof(int));
            dt.Columns.Add(text.Header_RejectRate, typeof(decimal));

            if (cbShowRawMat.Checked)
            {
                dt.Columns.Add(text.Header_RawMat, typeof(string));
            }

            if (cbShowColorMat.Checked)
            {
                dt.Columns.Add(text.Header_ColorMat, typeof(string));
            }

            dt.Columns.Add(text.Header_YieldRate, typeof(decimal));

            dt.Columns.Add(text.Header_IdealHourlyShot, typeof(int));
            dt.Columns.Add(text.Header_AvgHourlyShot, typeof(decimal));

            dt.Columns.Add(text.Header_EfficiencyRate, typeof(decimal));
            dt.Columns.Add(text.Header_Note, typeof(string));

            return dt;
        }

        private DataTable NewMoreDetailTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Data, typeof(string));
            dt.Columns.Add(text.Header_Description, typeof(string));

            return dt;
        }

        private void loadMachine(ComboBox cmb)
        {
            DataRowView drv = (DataRowView)cmbFac.SelectedItem;

            String valueOfItem = drv["fac_name"].ToString();

            string fac = valueOfItem;

            DataTable dt_Mac = dalMac.Select();

            DataTable dt = new DataTable();
            dt.Columns.Add(dalMac.MacID, typeof(int));

            if (string.IsNullOrEmpty(fac) || fac.ToUpper().Equals(text.Cmb_All.ToUpper()))
            {
                //load All active machine
                facDAL dalFac = new facDAL();

                DataTable dt_Fac = (DataTable)cmbFac.DataSource;

                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocation].ToString();

                    foreach (DataRow facRow in dt_Fac.Rows)
                    {
                        string facName = facRow[dalFac.FacName].ToString();

                        if(macLocation == facName)
                        {
                            //add machine id to table
                            dt.Rows.Add(row[dalMac.MacID]);

                        }
                    }
                }

            }
            else
            {
                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocation].ToString();

                    if (macLocation == fac)
                    {
                        //add machine id to table
                        dt.Rows.Add(row[dalMac.MacID]);

                    }
                }
            }

            DataTable distinctTable = dt.DefaultView.ToTable(true, dalMac.MacID);
            distinctTable.DefaultView.Sort = dalMac.MacID + " ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = dalMac.MacID;
            cmb.SelectedIndex = -1;

            //if (cmbFac.SelectedIndex != -1)
            //{
            //    string fac = cmbFac.Text;

            //    if (string.IsNullOrEmpty(fac))
            //    {
            //        tool.loadMacIDToComboBox(cmbMac);
            //        cmbMac.SelectedIndex = -1;
            //    }
            //    else if (fac.Equals("All"))
            //    {
            //        tool.loadMacIDToComboBox(cmbMac);
            //        cmbMac.SelectedIndex = -1;
            //    }
            //    else
            //    {
            //        tool.loadMacIDByFactoryToComboBox(cmbMac, fac);
            //    }
            //}

        }

        private void loadMachine()
        {
            ComboBox cmb = cmbMac;

            string fac = cmbFac.Text;
            DataTable dt_Mac = dalMac.Select();
            DataTable dt = new DataTable();
            dt.Columns.Add(dalMac.MacID);

            if (cmbFac.DataSource == null || string.IsNullOrEmpty(fac) || fac.ToUpper().Equals(text.Cmb_All.ToUpper()))
            {
                //load active machine
                facDAL dalFac = new facDAL();

                DataTable dt_Fac = (DataTable)cmbFac.DataSource;

                foreach (DataRow row in dt_Mac.Rows)
                {
                    string macLocation = row[dalMac.MacLocation].ToString();

                    foreach (DataRow facRow in dt_Fac.Rows)
                    {
                        string facName = row[dalFac.FacName].ToString();

                        if (macLocation == facName)
                        {
                            //add machine id to table
                            string macID = row[dalMac.MacID].ToString();
                            dt.Rows.Add(macID);

                        }
                    }
                }

            }
            else if (cmbFac.SelectedIndex != -1)
            {

            }

            DataTable distinctTable = dt.DefaultView.ToTable(true, dalMac.MacID);
            distinctTable.DefaultView.Sort = dalMac.MacID + " ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = dalMac.MacID;
            cmb.SelectedIndex = -1;

            //if (cmbFac.SelectedIndex != -1)
            //{
            //    string fac = cmbFac.Text;

            //    if (string.IsNullOrEmpty(fac))
            //    {
            //        tool.loadMacIDToComboBox(cmbMac);
            //        cmbMac.SelectedIndex = -1;
            //    }
            //    else if (fac.Equals("All"))
            //    {
            //        tool.loadMacIDToComboBox(cmbMac);
            //        cmbMac.SelectedIndex = -1;
            //    }
            //    else
            //    {
            //        tool.loadMacIDByFactoryToComboBox(cmbMac, fac);
            //    }
            //}

        }

        private void ShowOrHideFilterOption()
        {
            dgvMainList.SuspendLayout();
            dgvSubList.SuspendLayout();

            string text = btnFilter.Text;

            if (text == textMoreFilters)
            {
                tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 210f);

                dgvMainList.ResumeLayout();
                dgvSubList.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvMainList.ResumeLayout();
                dgvSubList.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void ShowOrHideSubList(bool Show)
        {
            if(Show)
            {
                tlpListTitle.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 10f);
                tlpListTitle.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 400f);

                tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 10f);
                tlpList.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 400f);
            }
            else
            {
                tlpListTitle.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpListTitle.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);

                tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpList.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
            }


        }

        private void ShowFilterOption(bool showFilterOption)
        {
            dgvMainList.SuspendLayout();
            dgvSubList.SuspendLayout();

            if (showFilterOption)
            {
                tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 210f);

                dgvMainList.ResumeLayout();
                dgvSubList.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else
            {
                tlpProductionReport.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvMainList.ResumeLayout();
                dgvSubList.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
        }

        private void LoadMoreDetail(int rowIndex)
        {
            if(rowIndex >= 0 && dgvMainList.Rows.Count >= 1)
            {
                DataTable dt = NewMoreDetailTable();
                DataRow dt_Row;
                //need code,cavity,meter start, meter end, total shot, weight per shot, raw mat. code, raw mat used. color mat. code, color usage, color mat used
                if (cmbReportType.Text == text.ReportType_ByJobNo)
                {
                    //get data by plan id
                    string JobNo = dgvMainList.Rows[rowIndex].Cells[text.Header_JobNo].Value.ToString();

                    bool dataFound = false;
                    int previousMeterEnd = 0;

                    foreach (DataRow row in DT_JOBSHEETRECORD.Rows)
                    {
                        string _planID = row[dalPlan.planID].ToString();

                        if (JobNo == _planID)
                        {
                            if (!dataFound)
                            {
                                dataFound = true;

                                dt_Row = dt.NewRow();

                                string itemCode = row[dalItem.ItemCode].ToString();
                                dt_Row[text.Header_Data] = data_Code;
                                dt_Row[text.Header_Description] = itemCode;
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                string cavity = row[dalItem.ItemCavity].ToString();
                                dt_Row[text.Header_Data] = data_Cavity;
                                dt_Row[text.Header_Description] = cavity;
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                int meterStart = Convert.ToInt32(row[dalProRecord.MeterStart].ToString());
                                dt_Row[text.Header_Data] = data_MeterStart;
                                dt_Row[text.Header_Description] = meterStart;
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                int meterEnd = Convert.ToInt32(row[dalProRecord.MeterEnd].ToString());
                                previousMeterEnd = meterEnd;
                                dt_Row[text.Header_Data] = data_MeterEnd;
                                dt_Row[text.Header_Description] = row[dalProRecord.MeterEnd].ToString();
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                int totalShot = meterEnd - meterStart;
                                dt_Row[text.Header_Data] = data_TotalShot;
                                dt_Row[text.Header_Description] = totalShot;
                                dt.Rows.Add(dt_Row);



                                dt_Row = dt.NewRow();

                                float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : 0;
                                float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : 0;

                                dt_Row[text.Header_Data] = data_Weight;
                                dt_Row[text.Header_Description] = partWeight.ToString("0.##") + "(" + runnerWeight.ToString("0.##") + ")" + " G";
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                dt_Row[text.Header_Data] = data_RawMat;
                                dt_Row[text.Header_Description] = row[dalItem.ItemMaterial].ToString();
                                dt.Rows.Add(dt_Row);

                                //dt_Row = dt.NewRow();
                                //dt_Row[header_Data] = data_RawMatUsed;
                                //dt_Row[header_Description] = "0";
                                //dt.Rows.Add(dt_Row);

                                if (!string.IsNullOrEmpty(row[dalItem.ItemMBatch].ToString()))
                                {
                                    dt_Row = dt.NewRow();
                                    dt_Row[text.Header_Data] = data_ColorMat;
                                    dt_Row[text.Header_Description] = row[dalItem.ItemMBatch].ToString();
                                    dt.Rows.Add(dt_Row);

                                    double colorUsage = double.TryParse(row[dalItem.ItemMBRate].ToString(), out double d) ? d : 0;
                                    dt_Row = dt.NewRow();
                                    dt_Row[text.Header_Data] = data_ColorUsage;
                                    dt_Row[text.Header_Description] = colorUsage.ToString("0.##");
                                    dt.Rows.Add(dt_Row);

                                    //dt_Row = dt.NewRow();
                                    //dt_Row[header_Data] = data_ColorMatUsed;
                                    //dt_Row[header_Description] = "0";
                                    //dt.Rows.Add(dt_Row);
                                }
                                else
                                {
                                    dt_Row = dt.NewRow();
                                    dt_Row[text.Header_Data] = data_ColorMat;
                                    dt_Row[text.Header_Description] = "NO COLOR MATERIAL";
                                    dt.Rows.Add(dt_Row);

                                }


                            }
                            else
                            {
                                int meterStart = Convert.ToInt32(row[dalProRecord.MeterStart].ToString());

                                dt.Rows[2][text.Header_Description] = meterStart;

                                int totalShot = previousMeterEnd - meterStart;

                                dt.Rows[4][text.Header_Description] = totalShot;

                            }



                        }
                    }

                    dgvSubList.DataSource = dt;
                    dgvSubList.ClearSelection();
                }
                else
                {
                    //get data by sheet id
                    string sheetID = dgvMainList.Rows[rowIndex].Cells[text.Header_SheetID].Value.ToString();


                    foreach(DataRow row in DT_JOBSHEETRECORD.Rows)
                    {
                        string _sheetID = row[dalProRecord.SheetID].ToString();

                        if(sheetID == _sheetID)
                        {
                            dt_Row = dt.NewRow();

                            string itemCode = row[dalItem.ItemCode].ToString();
                            dt_Row[text.Header_Data] = data_Code;
                            dt_Row[text.Header_Description] = itemCode;
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            string cavity = row[dalItem.ItemCavity].ToString();
                            dt_Row[text.Header_Data] = data_Cavity;
                            dt_Row[text.Header_Description] = cavity;
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            int meterStart = Convert.ToInt32(row[dalProRecord.MeterStart].ToString());
                            dt_Row[text.Header_Data] = data_MeterStart;
                            dt_Row[text.Header_Description] = meterStart;
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            int meterEnd = Convert.ToInt32(row[dalProRecord.MeterEnd].ToString());
                            dt_Row[text.Header_Data] = data_MeterEnd;
                            dt_Row[text.Header_Description] = row[dalProRecord.MeterEnd].ToString();
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            int totalShot = meterEnd - meterStart;
                            dt_Row[text.Header_Data] = data_TotalShot;
                            dt_Row[text.Header_Description] = totalShot;
                            dt.Rows.Add(dt_Row);



                            dt_Row = dt.NewRow();

                            float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : 0;
                            float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : 0;

                            dt_Row[text.Header_Data] = data_Weight;
                            dt_Row[text.Header_Description] = partWeight.ToString("0.##") + "(" + runnerWeight.ToString("0.##") + ")" + " G";
                            dt.Rows.Add(dt_Row);

                            dt_Row = dt.NewRow();
                            dt_Row[text.Header_Data] = data_RawMat;
                            dt_Row[text.Header_Description] = row[dalItem.ItemMaterial].ToString();
                            dt.Rows.Add(dt_Row);

                            //dt_Row = dt.NewRow();
                            //dt_Row[header_Data] = data_RawMatUsed;
                            //dt_Row[header_Description] = "0";
                            //dt.Rows.Add(dt_Row);

                            if (!string.IsNullOrEmpty(row[dalItem.ItemMBatch].ToString()))
                            {
                                dt_Row = dt.NewRow();
                                dt_Row[text.Header_Data] = data_ColorMat;
                                dt_Row[text.Header_Description] = row[dalItem.ItemMBatch].ToString();
                                dt.Rows.Add(dt_Row);

                                dt_Row = dt.NewRow();
                                dt_Row[text.Header_Data] = data_ColorUsage;
                                dt_Row[text.Header_Description] = row[dalItem.ItemMBRate].ToString();
                                dt.Rows.Add(dt_Row);

                                //dt_Row = dt.NewRow();
                                //dt_Row[header_Data] = data_ColorMatUsed;
                                //dt_Row[header_Description] = "0";
                                //dt.Rows.Add(dt_Row);
                            }
                            else
                            {
                                dt_Row = dt.NewRow();
                                dt_Row[text.Header_Data] = data_ColorMat;
                                dt_Row[text.Header_Description] = "NO COLOR MATERIAL";
                                dt.Rows.Add(dt_Row);

                            }

                            break;
                        }
                    }

                    dgvSubList.DataSource = dt;
                    dgvSubList.ClearSelection();
                }
            }
        }

        //private void LoadProductionRecord()
        //{
        //    Cursor = Cursors.WaitCursor;

        //    recordLoaded = false;


        //    dgvMoreDetail.DataSource = null;


        //    string keywords = txtItemSearch.Text;

        //    if(keywords == text.Search_DefaultText)
        //    {
        //        keywords = "";
        //    }

        //    keywords = keywords.ToUpper();

        //    if (!string.IsNullOrEmpty(keywords) && cbKeywordSearchByJobNo.Checked)
        //    {
        //        cbAllTime.Checked = true;
        //    }

        //    DataTable dt = NewProductionRecordTable();
        //    dt_ProRecord = dalProRecord.SelectWithItemInfo();
        //    DataTable dt_Mac = dalMac.Select();

        //    int index = 1;

        //    foreach(DataRow row in dt_ProRecord.Rows)
        //    {
        //        int macID = int.TryParse(row[dalPlan.machineID].ToString(), out macID) ? macID : 0;

        //        string fac = tool.getFactoryNameFromMachineID(macID.ToString(), dt_Mac);

        //        DateTime proDate = Convert.ToDateTime(row[dalProRecord.ProDate]).Date;

        //        int jobNo = int.TryParse(row[dalPlan.planID].ToString(), out jobNo) ? jobNo : 0;

        //        int sheetID = int.TryParse(row[dalProRecord.SheetID].ToString(), out sheetID) ? sheetID : 0;

        //        string shift = row[dalProRecord.Shift].ToString();

        //        string itemCode = row[dalItem.ItemCode].ToString();
        //        string itemName = row[dalItem.ItemName].ToString();

        //        int totalProduced = int.TryParse(row[dalProRecord.TotalProduced].ToString(), out totalProduced) ? totalProduced : 0;

        //        string rawMat = row[dalItem.ItemMaterial].ToString();
        //        string colorMat = row[dalItem.ItemMBatch].ToString();

        //        bool dataMatched = true;

        //        #region filter date
        //        DateTime dateFrom = dtpFrom.Value.Date;
        //        DateTime dateTo = dtpTo.Value.Date;

        //        if(proDate < dateFrom || proDate > dateTo)
        //        {
        //            dataMatched = false;
        //        }
        //        #endregion

        //        #region filter item

        //        if(cbKeywordSearchByItem.Checked)
        //        {
        //            if (!string.IsNullOrEmpty(keywords))
        //            {
        //                if(!itemName.ToUpper().Contains(keywords) && !itemCode.ToUpper().Contains(keywords))
        //                {
        //                    dataMatched = false;
        //                }
        //            }


        //        }


        //        #endregion

        //        #region filter factory

        //        string filterFac = cmbFac.Text;

        //        if(filterFac != "All" && !string.IsNullOrEmpty(filterFac) && filterFac != fac)
        //        {
        //            dataMatched = false;
        //        }

        //        #endregion

        //        #region filter machine

        //        string filterMac = cmbMac.Text;

        //        if (!string.IsNullOrEmpty(filterMac) && filterMac != macID.ToString())
        //        {
        //            dataMatched = false;
        //        }

        //        #endregion

        //        if (dataMatched)
        //        {
        //            DataRow dt_Row = dt.NewRow();

        //            dt_Row[header_Index] = index;
        //            dt_Row[header_Fac] = fac;
        //            dt_Row[header_Mac] = macID;

        //            if(cmbReportType.Text == text.ReportType_ByJobNo)
        //            {
        //                dt_Row[header_ProDateTo] = proDate;
        //                dt_Row[header_ProDateFrom] = proDate;
        //            }
        //            else
        //            {
        //                dt_Row[header_ProDate] = proDate;
        //                dt_Row[header_SheetID] = sheetID;
        //                dt_Row[header_Shift] = shift;
        //            }

        //            dt_Row[header_PlanID] = jobNo;
        //            dt_Row[header_PartName] = itemName;
        //            dt_Row[header_PartCode] = itemCode;
        //            dt_Row[header_Produced] = totalProduced;

        //            if (cbShowRawMat.Checked)
        //            {
        //                dt_Row[header_RawMat] = rawMat;
        //            }

        //            if (cbShowColorMat.Checked)
        //            {
        //                dt_Row[header_ColorMat] = colorMat;
        //            }


        //            dt.Rows.Add(dt_Row);
        //            index++;
        //        }
        //    }

        //    #region show only 1 row for each plan

        //    if(cmbReportType.Text == text.ReportType_ByJobNo)
        //    {
        //        DataTable dt_FilterDuplicatePlan = NewProductionRecordTable();

        //        string previousPlanID = null;
        //        int totalStockIn = 0;

        //        //sorting dt by planID and date
        //        dt.DefaultView.Sort = header_PlanID+" ASC";
        //        dt = dt.DefaultView.ToTable();

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            DateTime proDate = Convert.ToDateTime(row[header_ProDateTo]);

        //            if (previousPlanID == row[header_PlanID].ToString())
        //            {
        //                totalStockIn += Convert.ToInt32(row[header_Produced].ToString());

        //                dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][header_ProDateFrom] = proDate;
        //                dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][header_Produced] = totalStockIn;
        //            }
        //            else
        //            {
        //                previousPlanID = row[header_PlanID].ToString();
        //                totalStockIn = Convert.ToInt32(row[header_Produced].ToString());

        //                dt_FilterDuplicatePlan.ImportRow(row);
        //            }

        //            //if(previousPlanID == null)
        //            //{
        //            //    previousPlanID = row[header_PlanID].ToString();
        //            //    totalStockIn = Convert.ToInt32(row[header_StockIn].ToString());

        //            //    dt_FilterDuplicatePlan.Rows.Add(row);
        //            //}
        //            //else if(previousPlanID == row[header_PlanID].ToString())
        //            //{
        //            //    totalStockIn += Convert.ToInt32(row[header_StockIn].ToString());
        //            //    dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][header_StockIn] = totalStockIn;
        //            //}
        //            //else
        //            //{
        //            //    previousPlanID = row[header_PlanID].ToString();
        //            //    totalStockIn = Convert.ToInt32(row[header_StockIn].ToString());

        //            //    dt_FilterDuplicatePlan.Rows.Add(row);
        //            //}

        //        }

        //        dt_FilterDuplicatePlan.DefaultView.Sort = header_Mac + " ASC, " + header_ProDateFrom + " ASC";
        //        dt_FilterDuplicatePlan = dt_FilterDuplicatePlan.DefaultView.ToTable();

        //        int indexNo = 1;
        //        foreach (DataRow row in dt_FilterDuplicatePlan.Rows)
        //        {
        //            row[header_Index] = indexNo;
        //            indexNo++;

        //        }

        //        dt = dt_FilterDuplicatePlan.Copy();
        //    }

        //    #endregion

        //    dgvProductionRecord.DataSource = dt;
        //    dgvProductionRecord.ClearSelection();

        //    recordLoaded = true;

        //    ShowFilterOption(false);
        //    Cursor = Cursors.Arrow;
        //}

        private decimal OldcalculateAvgHourlyShot(int sheetID, int totalMeterRun)
        {
            decimal avgHourlyShot = 0;
            int productionHour = 0;
                
            int rowCount = DT_METERRECORD != null ?  DT_METERRECORD.Rows.Count : 0;

            if (DT_METERRECORD != null && rowCount > 0)
            {
                bool sheetFound = false;

                foreach (DataRow row in DT_METERRECORD.Rows)
                {
                    int sheetID_DB = int.TryParse(row[dalProRecord.SheetID].ToString(), out sheetID_DB) ? sheetID_DB : 0;

                    if (sheetID_DB == sheetID )
                    {
                        int proMeter = int.TryParse(row[dalProRecord.ProMeterReading].ToString(), out proMeter) ? proMeter : 0;

                        if (proMeter > 0)
                            productionHour++;


                        if (!sheetFound)
                        {
                            sheetFound = true;
                        }

                    }
                    else if(sheetFound)
                    {
                        break;
                    }
                }
            }

            avgHourlyShot = productionHour > 0 ? totalMeterRun / productionHour : 0;

            return avgHourlyShot;
        }

        private int BinarySearch(DataTable sortedTable, int target)
        {
            int left = 0;
            int right = sortedTable.Rows.Count - 1;
            int firstOccurrence = -1;

            while (left <= right)
            {
                int middle = left + (right - left) / 2;
                int middleValue = Convert.ToInt32(sortedTable.Rows[middle][dalProRecord.SheetID]);

                if (middleValue == target)
                {
                    firstOccurrence = middle;
                    right = middle - 1; // Continue searching to the left for the first occurrence
                }
                else if (middleValue < target)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }

            return firstOccurrence;
        }

        private decimal calculateAvgHourlyShot(int sheetID, int totalMeterRun)
        {
            decimal avgHourlyShot = 0;
            int productionHour = 0;

            int rowCount = DT_METERRECORD != null ? DT_METERRECORD.Rows.Count : 0;

            if (DT_METERRECORD != null && rowCount > 0)
            {
                bool sheetFound = false;

                int rowIndex = BinarySearch(DT_METERRECORD, sheetID);

                if(rowIndex > -1)
                {
                    for (int i = rowIndex; i < rowCount; i++)
                    {
                        int sheetID_DB = int.TryParse(DT_METERRECORD.Rows[i][dalProRecord.SheetID].ToString(), out sheetID_DB) ? sheetID_DB : 0;

                        if (sheetID_DB == sheetID)
                        {
                            int proMeter = int.TryParse(DT_METERRECORD.Rows[i][dalProRecord.ProMeterReading].ToString(), out proMeter) ? proMeter : 0;

                            if (proMeter > 0)
                                productionHour++;


                            if (!sheetFound)
                            {
                                sheetFound = true;
                            }

                        }
                        else if (sheetFound)
                        {
                            break;
                        }
                    }
                }
               
            }

            avgHourlyShot = productionHour > 0 ? totalMeterRun / productionHour : 0;

            return avgHourlyShot;
        }

        private void NewLoadProductionRecord()
        {
            #region Setting

            Cursor = Cursors.WaitCursor;

            frmLoading.ShowLoadingScreen();

            recordLoaded = false;


            dgvMainList.DataSource = null;
            dgvSubList.DataSource = null;


            string keywords = txtItemSearch.Text;

            if (keywords == text.Search_DefaultText)
            {
                keywords = "";
            }

            keywords = keywords.ToUpper();

            if (!string.IsNullOrEmpty(keywords) && cbKeywordSearchByJobNo.Checked)
            {
                cbAllTime.Checked = true;
            }

            DataTable dt = NewProductionRecordTable();

            DT_JOBSHEETRECORD = dalProRecord.SelectWithItemInfo();//1483ms

            DT_METERRECORD = dalProRecord.MeterRecordSelect();

            DT_METERRECORD.DefaultView.Sort = dalProRecord.SheetID + " ASC";
            DT_METERRECORD = DT_METERRECORD.DefaultView.ToTable();

            DataTable dt_Mac = dalMac.Select();

           

            int index = 1;

            #endregion

            foreach (DataRow row in DT_JOBSHEETRECORD.Rows)
            {
                int macID = int.TryParse(row[dalPlan.machineID].ToString(), out macID) ? macID : 0;

                string fac = tool.getFactoryNameFromMachineID(macID.ToString(), dt_Mac);

                DateTime proDate = Convert.ToDateTime(row[dalProRecord.ProDate]).Date;

                int jobNo = int.TryParse(row[dalPlan.planID].ToString(), out jobNo) ? jobNo : 0;

                int sheetID = int.TryParse(row[dalProRecord.SheetID].ToString(), out sheetID) ? sheetID : 0;
                int meterStart = int.TryParse(row[dalProRecord.MeterStart].ToString(), out meterStart) ? meterStart : 0;
                int meterEnd = int.TryParse(row[dalProRecord.MeterEnd].ToString(), out meterEnd) ? meterEnd : 0;

                int cavity = int.TryParse(row[dalPlan.planCavity].ToString(), out cavity) ? cavity : 0;
                int cycleTime = int.TryParse(row[dalPlan.planCT].ToString(), out cycleTime) ? cycleTime : 0;

                if(cycleTime == 0)
                {
                    cycleTime = int.TryParse(row[dalItem.ItemProCTTo].ToString(), out cycleTime) ? cycleTime : 0;

                    if (cycleTime == 0)
                    {
                        cycleTime = int.TryParse(row[dalItem.ItemQuoCT].ToString(), out cycleTime) ? cycleTime : 0;
                    }
                }

                int IdealHourlyShot = cycleTime > 0 ? 3600 / cycleTime : 0;

                

                int MaxOutput = (meterEnd - meterStart) * cavity;

                string shift = row[dalProRecord.Shift].ToString();

                string itemCode = row[dalItem.ItemCode].ToString();
                string itemName = row[dalItem.ItemName].ToString();

                string note = row[dalProRecord.Note].ToString();

                int totalProduced = int.TryParse(row[dalProRecord.TotalProduced].ToString(), out totalProduced) ? totalProduced : 0;

                int totalReject = int.TryParse(row[dalProRecord.TotalActualReject].ToString(), out totalReject) ? totalReject : 0;

                if(totalReject == 0)
                {
                    totalReject = int.TryParse(row[dalProRecord.TotalReject].ToString(), out totalReject) ? totalReject : 0;
                }

                if(totalReject < 0)
                {
                    totalReject = 0;
                }

                decimal rejectRate =  decimal.Round( (decimal) totalReject / (decimal) MaxOutput * 100,2);
                var yieldRate = 100 - rejectRate;

                string rawMat = row[dalItem.ItemMaterial].ToString();
                string colorMat = row[dalItem.ItemMBatch].ToString();

                bool dataMatched = true;

                #region filter date
                DateTime dateFrom = dtpFrom.Value.Date;
                DateTime dateTo = dtpTo.Value.Date;

                if (proDate < dateFrom || proDate > dateTo)
                {
                    dataMatched = false;
                }
                #endregion

                #region filter item

                if (cbKeywordSearchByItem.Checked)
                {
                    if (!string.IsNullOrEmpty(keywords))
                    {
                        if (!itemName.ToUpper().Contains(keywords) && !itemCode.ToUpper().Contains(keywords))
                        {
                            dataMatched = false;
                        }
                    }


                }


                #endregion

                #region filter factory

                string filterFac = cmbFac.Text;

                if (filterFac != "All" && !string.IsNullOrEmpty(filterFac) && filterFac != fac)
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

                    //decimal ActualAvgHourlyShot = 0;
                    decimal ActualAvgHourlyShot = decimal.Round(calculateAvgHourlyShot(sheetID, meterEnd - meterStart), 2);
                    var efficiencyRate = IdealHourlyShot > 0 ? decimal.Round( ActualAvgHourlyShot / IdealHourlyShot * 100, 2) : -1 ;

                    dt_Row[text.Header_Index] = index;
                    dt_Row[text.Header_Fac] = fac;
                    dt_Row[text.Header_Mac] = macID;

                    if (cmbReportType.Text == text.ReportType_ByJobNo)
                    {
                        dt_Row[text.Header_DateTo] = proDate;
                        dt_Row[text.Header_DateFrom] = proDate;
                    }
                    else
                    {
                        dt_Row[text.Header_Date] = proDate;
                        dt_Row[text.Header_SheetID] = sheetID;
                        dt_Row[text.Header_Shift] = shift;
                    }

                    dt_Row[text.Header_JobNo] = jobNo;
                    dt_Row[text.Header_PartName] = itemName;
                    dt_Row[text.Header_PartCode] = itemCode;

                    dt_Row[text.Header_MaxOutput] = MaxOutput;
                    dt_Row[text.Header_TotalProduced] = totalProduced;

                    dt_Row[text.Header_TotalReject] = totalReject;
                    dt_Row[text.Header_RejectRate] = rejectRate;

                    dt_Row[text.Header_YieldRate] = yieldRate;
                    dt_Row[text.Header_EfficiencyRate] = efficiencyRate;

                    dt_Row[text.Header_IdealHourlyShot] = IdealHourlyShot;
                    dt_Row[text.Header_AvgHourlyShot] = ActualAvgHourlyShot;

                    if(efficiencyRate == -1)
                    {
                        note = "(SYSTEM: Cycle Time not Found!) ;" + note;
                    }
                    dt_Row[text.Header_Note] = note;



                    if (cbShowRawMat.Checked)
                    {
                        dt_Row[text.Header_Mat] = rawMat;
                    }

                    if (cbShowColorMat.Checked)
                    {
                        dt_Row[text.Header_ColorMat] = colorMat;
                    }


                    dt.Rows.Add(dt_Row);
                    index++;
                }
            }

            #region show only 1 row for each plan

            if (cmbReportType.Text == text.ReportType_ByJobNo)
            {
                DataTable dt_FilterDuplicatePlan = NewProductionRecordTable();

                string previousJobNo = null;
                int totalStockIn = 0;

                //sorting dt by planID and date
                dt.DefaultView.Sort = text.Header_JobNo + " ASC";
                dt = dt.DefaultView.ToTable();

                foreach (DataRow row in dt.Rows)
                {
                    DateTime proDate = Convert.ToDateTime(row[text.Header_DateTo]);

                    if (previousJobNo == row[text.Header_JobNo].ToString())
                    {
                        totalStockIn += Convert.ToInt32(row[text.Header_TotalProduced].ToString());

                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_DateFrom] = proDate;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_TotalProduced] = totalStockIn;
                    }
                    else
                    {
                        previousJobNo = row[text.Header_JobNo].ToString();
                        totalStockIn = Convert.ToInt32(row[text.Header_TotalProduced].ToString());

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

                dt_FilterDuplicatePlan.DefaultView.Sort = text.Header_Mac + " ASC, " + text.Header_DateFrom + " ASC";
                dt_FilterDuplicatePlan = dt_FilterDuplicatePlan.DefaultView.ToTable();

                int indexNo = 1;
                foreach (DataRow row in dt_FilterDuplicatePlan.Rows)
                {
                    row[text.Header_Index] = indexNo;
                    indexNo++;

                }

                dt = dt_FilterDuplicatePlan.Copy();
            }

            #endregion

            dgvMainList.DataSource = dt;
            dgvMainList.ClearSelection();

            recordLoaded = true;

            ShowFilterOption(false);


            frmLoading.CloseForm();

            Cursor = Cursors.Arrow;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilterOption();
        }

        private void frmProductionReport_Load(object sender, EventArgs e)
        {
            ShowFilterOption(false);
            ShowOrHideSubList(false);

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

                loadMachine(cmbMac);

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
            dgvMainList.DataSource = null;
            dgvSubList.DataSource = null;
        }

        private void cmbPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllDGVData();
            if (ableLoadCodeData)
            {
                string keywords = txtItemSearch.Text;

                if (!string.IsNullOrEmpty(keywords))
                {
                    DataTable dt = dalItem.nameSearch(keywords);
                    DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                    dtItemCode.DefaultView.Sort = "item_code ASC";
                    //cmbPartCode.DataSource = dtItemCode;
                    //cmbPartCode.DisplayMember = "item_code";

                    if(dtItemCode.Rows.Count > 1)
                    {
                        //cmbPartCode.SelectedIndex = -1;
                    }
                    
                }
                else
                {
                    //cmbPartCode.DataSource = null;

                }
            }
        }

        private void lblPartNameReset_Click(object sender, EventArgs e)
        {
            //cmbPartName.SelectedIndex = -1;
        }

        private void lblPartCodeReset_Click(object sender, EventArgs e)
        {
            //cmbPartCode.SelectedIndex = -1;
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
            NewLoadProductionRecord();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            NewLoadProductionRecord();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            NewLoadProductionRecord();
        }

        private void dgvProductionRecord_SelectionChanged(object sender, EventArgs e)
        {
            if(recordLoaded)
            {
                if(dgvMainList.CurrentCell != null)
                {
                    int rowIndex = dgvMainList.CurrentCell.RowIndex;

                    
                    LoadMoreDetail(rowIndex);
                }
               
            }
            
        }

        private void dgvProductionRecord_Click(object sender, EventArgs e)
        {
            if (recordLoaded && dgvMainList.CurrentCell != null)
            {
                int rowIndex = dgvMainList.CurrentCell.RowIndex;

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



            dgvMainList.SelectAll();
            excelTool.ExportToExcel(text.Report_Type_Production, (DataTable) dgvMainList.DataSource, dgvMainList.GetClipboardContent());


            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        bool SearchByChanging = false;

        private void cbKeywordSearchByJobNo_CheckedChanged(object sender, EventArgs e)
        {
            if(!SearchByChanging)
            {
                SearchByChanging = true;

                cbKeywordSearchByItem.Checked = !cbKeywordSearchByJobNo.Checked;
               
                SearchByChanging = false;
            }


        }

        private void cbKeywordSearchByItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!SearchByChanging)
            {
                SearchByChanging = true;

                cbKeywordSearchByJobNo.Checked = !cbKeywordSearchByItem.Checked;

                SearchByChanging = false;
            }
        }

        private void cbAllTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = !cbAllTime.Checked;
            dtpTo.Enabled = !cbAllTime.Checked;
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubListType(cmbSubListType);
        }

        private void cmbSubListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrHideSubList(cmbSubListType.Text != text.ReportType_NoShow);
        }

        private void txtItemSearch_Enter(object sender, EventArgs e)
        {
            if (txtItemSearch.Text == text.Search_DefaultText)
            {
                txtItemSearch.Text = "";
                txtItemSearch.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtItemSearch_Leave(object sender, EventArgs e)
        {
            if (txtItemSearch.Text.Length == 0)
            {
                txtItemSearch.Text = text.Search_DefaultText;
                txtItemSearch.ForeColor = SystemColors.GrayText;
            }
        }

        private void dgvProductionRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMainList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == text.Header_YieldRate)
            {
                decimal yieldAlert = decimal.TryParse(txtYieldAlert.Text, out yieldAlert) ? yieldAlert : 0;
                decimal yieldRate = decimal.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out yieldRate) ? yieldRate : 0;

                if (yieldRate < yieldAlert)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
         
        }
    }
}
