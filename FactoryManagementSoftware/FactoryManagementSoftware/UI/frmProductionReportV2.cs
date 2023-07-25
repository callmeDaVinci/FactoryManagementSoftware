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
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using iTextSharp.text.pdf;
using MathNet.Numerics.Distributions;
using System.Collections.Generic;

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
        joinDAL dalJoin = new joinDAL();
        itemCustDAL dalItemCust = new itemCustDAL();

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
        DataTable DT_ITEM_CUST;
        DataTable DT_JOIN;

        #endregion

        private void InitializeFilterData()
        {
            loadReportType(cmbReportType);
            tool.loadProductionFactory(cmbFac);

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
            cmb.SelectedIndex = 1;
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

            dt.Columns.Add(text.Header_RawMat_1, typeof(string));
            dt.Columns.Add(text.Header_ColorMat, typeof(string));

            dt.Columns.Add(text.Header_MaxOutput, typeof(int));

            dt.Columns.Add(text.Header_QCPassedQty, typeof(int));
           //dt.Columns.Add(text.Header_TotalStockIn, typeof(int));
            
            dt.Columns.Add(text.Header_TotalReject, typeof(int));
            dt.Columns.Add(text.Header_RejectRate, typeof(decimal));

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
                    string macLocation = row[dalMac.MacLocationName].ToString();

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
                    string macLocation = row[dalMac.MacLocationName].ToString();

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
                    string macLocation = row[dalMac.MacLocationName].ToString();

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
                        string _planID = row[dalPlan.jobNo].ToString();

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

        private int BinarySearch(DataTable sortedTable, string targetColumn , int target)
        {
            int left = 0;
            int right = sortedTable.Rows.Count - 1;
            int firstOccurrence = -1;

            //sortedTable.DefaultView.Sort = targetColumn + " ASC";
            //sortedTable = sortedTable.DefaultView.ToTable();

            while (left <= right)
            {
                int middle = left + (right - left) / 2;
                int middleValue = Convert.ToInt32(sortedTable.Rows[middle][targetColumn]);

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
            DateTime EarliestTime = DateTime.MaxValue;
            DateTime LatestTime = DateTime.MaxValue;

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
                            DateTime proDate = DateTime.TryParse(DT_METERRECORD.Rows[i][dalProRecord.ProTime].ToString(), out proDate) ? proDate : DateTime.MaxValue;
                            int proMeter = int.TryParse(DT_METERRECORD.Rows[i][dalProRecord.ProMeterReading].ToString(), out proMeter) ? proMeter : 0;

                            if (proMeter > 0)
                                productionHour++;

                            if(LatestTime == DateTime.MaxValue || proDate > LatestTime)
                            {
                                LatestTime = proDate;
                            }

                            if (EarliestTime == DateTime.MaxValue || proDate < EarliestTime)
                            {
                                EarliestTime = proDate;
                            }

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

        private bool ifJobProductionDateFallWithinTargetPeriod(int jobNo, DateTime dateFrom, DateTime dateTo)
        {
            bool withinTargetPeriod = false;

            int rowCount = DT_JOBSHEETRECORD != null ? DT_JOBSHEETRECORD.Rows.Count : 0;

            if (DT_JOBSHEETRECORD != null && rowCount > 0)
            {
                bool jobFound = false;

                int rowIndex = BinarySearch(DT_JOBSHEETRECORD, dalPlan.jobNo, jobNo);

                if (rowIndex > -1)
                {
                    for (int i = rowIndex; i < rowCount; i++)
                    {
                        int jobNo_DB = int.TryParse(DT_JOBSHEETRECORD.Rows[i][dalPlan.jobNo].ToString(), out jobNo_DB) ? jobNo_DB : -1;

                        if (jobNo_DB == jobNo)
                        {
                            DateTime proDate = DateTime.TryParse(DT_JOBSHEETRECORD.Rows[i][dalProRecord.ProDate].ToString(), out proDate) ? proDate : DateTime.MaxValue;
                            if (proDate >= dateFrom && proDate <= dateTo)
                            {
                                return true;
                            }

                            if (!jobFound)
                            {
                                jobFound = true;
                            }

                        }
                        else if (jobFound)
                        {
                            break;
                        }
                    }
                }

            }

            return withinTargetPeriod;
        }
        private void dgvUIEdit(DataGridView dgv)
        {
            if (dgv == dgvMainList)
            {

                //header,List
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                #region default columns
                dgv.Columns[text.Header_PartName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgv.Columns[text.Header_PartCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_PartCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[text.Header_PartCode].Frozen = true;


                if (dgv.Columns.Contains(text.Header_Shift))
                {
                    dgv.Columns[text.Header_Shift].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                }

                if (dgv.Columns.Contains(text.Header_SheetID))
                {
                    dgv.Columns[text.Header_SheetID].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                }

                if (dgv.Columns.Contains(text.Header_DateFrom))
                {
                    dgv.Columns[text.Header_DateFrom].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                }

                if (dgv.Columns.Contains(text.Header_DateTo))
                {
                    dgv.Columns[text.Header_DateTo].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                }

                if (dgv.Columns.Contains(text.Header_Date))
                {
                    dgv.Columns[text.Header_Date].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

                }

                dgv.Columns[text.Header_JobNo].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                dgv.Columns[text.Header_Index].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                #endregion

                #region Optional Columns

                if (dgv.Columns.Contains(text.Header_Mac))
                {
                    if(cbMachine.Checked)
                    {
                        dgv.Columns[text.Header_Mac].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    }
                    else
                    {
                        dgv.Columns[text.Header_Mac].Visible = cbMachine.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_Fac))
                {
                    if (cbFactory.Checked)
                    {
                        dgv.Columns[text.Header_Fac].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    }
                    else
                    {
                        dgv.Columns[text.Header_Fac].Visible = cbFactory.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_RawMat_1))
                {
                    if (cbShowRawMat.Checked)
                    {
                        dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgv.Columns[text.Header_RawMat_1].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    }
                    else
                    {
                        dgv.Columns[text.Header_RawMat_1].Visible = cbShowRawMat.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_ColorMat))
                {
                    if (cbShowColorMat.Checked)
                    {
                        dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgv.Columns[text.Header_ColorMat].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    }
                    else
                    {
                        dgv.Columns[text.Header_ColorMat].Visible = cbShowColorMat.Checked;
                    }

                }
                if (dgv.Columns.Contains(text.Header_MaxOutput))
                {
                    if (cbMaxOutput.Checked)
                    {

                    }
                    else
                    {
                        dgv.Columns[text.Header_MaxOutput].Visible = cbMaxOutput.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_QCPassedQty))
                {
                    if (!cbQCPassedQty.Checked)
                    {
                        dgv.Columns[text.Header_QCPassedQty].Visible = cbQCPassedQty.Checked;
                    }
                    else
                    {
                        
                    }

                }

                if (dgv.Columns.Contains(text.Header_TotalReject))
                {
                    if (!cbRejectedQty.Checked)
                    {
                        dgv.Columns[text.Header_TotalReject].Visible = cbRejectedQty.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_RejectRate))
                {
                    if (!cbRejectedRate.Checked)
                    {
                        dgv.Columns[text.Header_RejectRate].Visible = cbRejectedRate.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_YieldRate))
                {
                    if (!cbYieldRate.Checked)
                    {
                        dgv.Columns[text.Header_YieldRate].Visible = cbYieldRate.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_IdealHourlyShot))
                {
                    if (!cbIdealHourlyShot.Checked)
                    {
                        dgv.Columns[text.Header_IdealHourlyShot].Visible = cbIdealHourlyShot.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_AvgHourlyShot))
                {
                    if (!cbAVGHourlyShot.Checked)
                    {
                        dgv.Columns[text.Header_AvgHourlyShot].Visible = cbAVGHourlyShot.Checked;
                    }

                }

                if (dgv.Columns.Contains(text.Header_EfficiencyRate))
                {
                    if (!cbEfficiencyRate.Checked)
                    {
                        dgv.Columns[text.Header_EfficiencyRate].Visible = cbEfficiencyRate.Checked;
                    }

                }


                if (cmbReportType.Text == text.ReportType_ByJobNo || !cbNote.Checked)
                {
                    dgv.Columns[text.Header_Note].Visible = false;
                }
                else
                {
                    dgv.Columns[text.Header_Note].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    dgv.Columns[text.Header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[text.Header_Note].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                #endregion


            }

        }

        private void MainListColumnsToDisplaySetting(DataGridView dgv)
        {
            if(dgv != null )
            {
                if (dgv.Columns.Contains(text.Header_Mac))
                {
                    dgv.Columns[text.Header_Mac].Visible = cbMachine.Checked;
                }

                if (dgv.Columns.Contains(text.Header_Fac))
                {
                    dgv.Columns[text.Header_Fac].Visible = cbFactory.Checked;
                }

                if (dgv.Columns.Contains(text.Header_RawMat_1))
                {
                    dgv.Columns[text.Header_RawMat_1].Visible = cbShowRawMat.Checked;
                }

                if (dgv.Columns.Contains(text.Header_ColorMat))
                {
                    dgv.Columns[text.Header_ColorMat].Visible = cbShowColorMat.Checked;

                }
                if (dgv.Columns.Contains(text.Header_MaxOutput))
                {
                    dgv.Columns[text.Header_MaxOutput].Visible = cbMaxOutput.Checked;

                }

                if (dgv.Columns.Contains(text.Header_QCPassedQty))
                {
                    dgv.Columns[text.Header_QCPassedQty].Visible = cbQCPassedQty.Checked;

                }

                if (dgv.Columns.Contains(text.Header_TotalReject))
                {
                    dgv.Columns[text.Header_TotalReject].Visible = cbRejectedQty.Checked;
                }

                if (dgv.Columns.Contains(text.Header_RejectRate))
                {
                    dgv.Columns[text.Header_RejectRate].Visible = cbRejectedRate.Checked;
                }

                if (dgv.Columns.Contains(text.Header_YieldRate))
                {
                    dgv.Columns[text.Header_YieldRate].Visible = cbYieldRate.Checked;
                }

                if (dgv.Columns.Contains(text.Header_IdealHourlyShot))
                {
                    dgv.Columns[text.Header_IdealHourlyShot].Visible = cbIdealHourlyShot.Checked;
                }

                if (dgv.Columns.Contains(text.Header_AvgHourlyShot))
                {
                    dgv.Columns[text.Header_AvgHourlyShot].Visible = cbAVGHourlyShot.Checked;
                }

                if (dgv.Columns.Contains(text.Header_EfficiencyRate))
                {
                    dgv.Columns[text.Header_EfficiencyRate].Visible = cbEfficiencyRate.Checked;
                }

                if (dgv.Columns.Contains(text.Header_Note))
                {
                    dgv.Columns[text.Header_Note].Visible = cbNote.Checked;
                }
            }
        
        }

        private void CellSelectedSumUp(DataGridView dgv)
        {
            decimal Average = 0;
            decimal Sum = 0;
            int Count = 0;

            if (dgv != null)
            {
                for(int i = 0; i < dgv.Rows.Count; i++)
                {
                    for(int j = 0; j < dgv.Columns.Count; j++)
                    {
                        bool cellSelected = dgv.Rows[i].Cells[j].Selected;

                        //if (cellSelected)
                        //{
                        //    decimal data = decimal.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out data) ? data : 0;

                        //    Sum += decimal.Round(data, 2);

                        //    Count++;

                        //}

                        if (dgv.SelectionMode != DataGridViewSelectionMode.FullColumnSelect)
                        {
                            cellSelected = dgv.Rows[i].Cells[j].Selected;

                            if (cellSelected)
                            {
                                decimal data = decimal.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out data) ? data : 0;

                                Sum += decimal.Round(data, 2);

                                Count++;

                            }
                        }
                        else if (dgv.SelectionMode == DataGridViewSelectionMode.FullColumnSelect)
                        {
                            cellSelected = dgv.Columns[j].Selected;

                            if (cellSelected)
                            {
                                decimal data = decimal.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out data) ? data : 0;

                                Sum += decimal.Round(data, 2);

                                Count++;

                            }
                        }


                    }
                }
            }

            if(Count > 0)
            Average = decimal.Round(Sum/Count, 2);

            lblSelectedCellSum.Text = "Average: " + Average.ToString() + "   Count: " + Count.ToString() + "   Sum: " + Sum.ToString();
        }

        private bool ItemOrParentBelongCustomer(string childCode, string custID)
        {
            bool customerMatched = false;

            if (itemBelongsToCustomer(custID, childCode))
            {
                return true;
            }

            foreach (DataRow row in DT_JOIN.Rows)
            {
                string childCode_DB = row[dalJoin.JoinChild].ToString();

                if(childCode_DB == childCode)
                {
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    if(ItemOrParentBelongCustomer(parentCode, custID))
                    {
                        return true;
                    }
                    
                }
            }

            return customerMatched;
        }

        private bool itemBelongsToCustomer(string custID, string itemCode)
        {
            //int custID_INT = int.TryParse(custID, out custID_INT) ? custID_INT : -1;

            // Assuming "CustomerId" and "ItemCode" are column names in your DataTable
            string expression = $"cust_id = '{custID}' AND item_code = '{itemCode}'";

            DataRow[] foundRows = DT_ITEM_CUST.Select(expression);

            // If any rows were found, the item belongs to the customer
            return foundRows.Length > 0;
        }

        private void CustomerFilter(string custID)
        {
            DT_JOBSHEETRECORD.DefaultView.Sort = dalPlan.partCode + " ASC";
            DT_JOBSHEETRECORD = DT_JOBSHEETRECORD.DefaultView.ToTable();

            string previousItemCode = "";

            bool itemToKeep = false;

            DT_JOBSHEETRECORD.AcceptChanges();
            foreach (DataRow row in DT_JOBSHEETRECORD.Rows)
            {
                string itemCode = row[dalPlan.partCode].ToString();

                if(itemCode.Equals("V76PBW000"))
                {
                    var checkpoint = 1;
                }

                if(previousItemCode != itemCode)
                {
                    previousItemCode = itemCode;

                    itemToKeep = ItemOrParentBelongCustomer(itemCode, custID);

                }
               
                if(!itemToKeep)
                {
                    row.Delete();
                }
              
            }
            DT_JOBSHEETRECORD.AcceptChanges();

        }


        private void NewLoadProductionRecord()
        {
            #region Setting

            Cursor = Cursors.WaitCursor;

            frmLoading.ShowLoadingScreen();

            ClearAllDGVData();

            string keywords = txtItemSearch.Text;

            if (keywords == text.Search_DefaultText)
            {
                keywords = "";
            }

            keywords = keywords.ToUpper();

            if (!string.IsNullOrEmpty(keywords) && cbKeywordSearchByJobNo.Checked && !cbDateFromStrict.Checked && !cbDateToStrict.Checked)
            {
                cbAllTime.Checked = true;
            }

            DataTable dt = NewProductionRecordTable();

            DT_JOBSHEETRECORD = dalProRecord.SelectWithItemInfo();//1483ms

           

            DT_METERRECORD = dalProRecord.MeterRecordSelect();

            DT_METERRECORD.DefaultView.Sort = dalProRecord.SheetID + " ASC";
            DT_METERRECORD = DT_METERRECORD.DefaultView.ToTable();

            DataTable dt_Mac = dalMac.Select();

            //customer filter
            string customer = cmbCustomer.Text;

            if(!string.IsNullOrEmpty(customer) && customer.ToUpper() != text.Cmb_All)
            {
                int customerSelectedIdex = cmbCustomer.SelectedIndex;
                DataTable dt_CMB_Customer = (DataTable)cmbCustomer.DataSource;

                string custID = "";

                if(dt_CMB_Customer.Columns.Contains(dalItemCust.CustID))
                {
                    custID = dt_CMB_Customer.Rows[customerSelectedIdex][dalItemCust.CustID].ToString();
                }


                DT_JOIN = dalJoin.SelectAll();
                DT_ITEM_CUST = dalItemCust.SelectAll();

                CustomerFilter(custID);

            }


            DT_JOBSHEETRECORD.DefaultView.Sort = dalPlan.jobNo + " ASC, " + dalProRecord.SheetID + " ASC";
            DT_JOBSHEETRECORD = DT_JOBSHEETRECORD.DefaultView.ToTable();

            int index = 1;

            #endregion

            foreach (DataRow row in DT_JOBSHEETRECORD.Rows)
            {
                int macID = int.TryParse(row[dalPlan.machineID].ToString(), out macID) ? macID : 0;

                string fac = tool.getFactoryNameFromMachineID(macID.ToString(), dt_Mac);

                DateTime proDate = Convert.ToDateTime(row[dalProRecord.ProDate]).Date;

                int jobNo = int.TryParse(row[dalPlan.jobNo].ToString(), out jobNo) ? jobNo : 0;

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

                string rawMat = row[dalPlan.materialCode].ToString();
                string colorMat = row[dalPlan.colorMaterialCode].ToString();

                bool dataMatched = true;

                #region filter date

                if (!cbAllTime.Checked)
                {
                    DateTime dateFrom = dtpFrom.Value.Date;
                    DateTime dateTo = dtpTo.Value.Date;

                    if (proDate < dateFrom || proDate > dateTo)
                    {
                        if(cbDateFromStrict.Checked && proDate < dateFrom)
                        {
                            dataMatched = false;
                        }
                        else if (cbDateToStrict.Checked && proDate > dateTo)
                        {
                            dataMatched = false;
                        }
                        else
                        {
                            int jobIndexCheck = BinarySearch(dt, text.Header_JobNo, jobNo);

                            if (jobIndexCheck == -1)
                            {
                                if (!ifJobProductionDateFallWithinTargetPeriod(jobNo, dateFrom, dateTo))
                                {
                                    dataMatched = false;

                                }
                            }
                        }
                           
                    }
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

                #region filter Job No

                if (cbKeywordSearchByJobNo.Checked && !string.IsNullOrEmpty(keywords))
                {
                    if (keywords != jobNo.ToString())
                    {
                        dataMatched = false;
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
                        dt_Row[text.Header_DateFrom] = proDate;
                        dt_Row[text.Header_DateTo] = proDate;
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
                    dt_Row[text.Header_QCPassedQty] = totalProduced;

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

                    dt_Row[text.Header_RawMat_1] = rawMat;

                    dt_Row[text.Header_ColorMat] = colorMat;

                    dt.Rows.Add(dt_Row);
                    index++;
                }
            }

            #region view by Job No

            if (cmbReportType.Text == text.ReportType_ByJobNo)
            {
                DataTable dt_FilterDuplicatePlan = NewProductionRecordTable();

                string previousJobNo = null;
                int qcPassedQty = 0;
                int totalReject = 0;
                int maxOutput = 0;
                decimal avgHourlyShot = 0;
                int IdealHourlyShot = 0;

                //sorting dt by planID and date
                //dt.DefaultView.Sort = text.Header_JobNo + " ASC";
                //dt = dt.DefaultView.ToTable();

                int avgCount = 1;

                foreach (DataRow row in dt.Rows)
                {
                    DateTime proDate = Convert.ToDateTime(row[text.Header_DateFrom]);

                    if (previousJobNo == row[text.Header_JobNo].ToString())
                    {
                        qcPassedQty += Convert.ToInt32(row[text.Header_QCPassedQty].ToString());
                        totalReject += Convert.ToInt32(row[text.Header_TotalReject].ToString());
                        maxOutput += Convert.ToInt32(row[text.Header_MaxOutput].ToString());
                        avgHourlyShot += Convert.ToDecimal(row[text.Header_AvgHourlyShot].ToString());
                        avgCount++;

                        IdealHourlyShot = Convert.ToInt32(row[text.Header_IdealHourlyShot].ToString());

                        decimal rejectRate = decimal.Round((decimal)totalReject / (decimal)maxOutput * 100, 2);
                        var yieldRate = 100 - rejectRate;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_DateTo] = proDate;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_QCPassedQty] = qcPassedQty;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_TotalReject] = totalReject;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_RejectRate] = rejectRate;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_YieldRate] = yieldRate;

                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_MaxOutput] = maxOutput;

                        decimal ActualAvgHourlyShot = decimal.Round(avgHourlyShot / avgCount,0);

                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_AvgHourlyShot] = ActualAvgHourlyShot;
                        var efficiencyRate = IdealHourlyShot > 0 ? decimal.Round(ActualAvgHourlyShot / IdealHourlyShot * 100, 2) : -1;
                        dt_FilterDuplicatePlan.Rows[dt_FilterDuplicatePlan.Rows.Count - 1][text.Header_EfficiencyRate] = efficiencyRate;

                    }
                    else
                    {
                       

                        avgCount = 1;

                        previousJobNo = row[text.Header_JobNo].ToString();
                        qcPassedQty = Convert.ToInt32(row[text.Header_QCPassedQty].ToString());
                        totalReject = Convert.ToInt32(row[text.Header_TotalReject].ToString());
                        maxOutput = Convert.ToInt32(row[text.Header_MaxOutput].ToString());
                        avgHourlyShot = Convert.ToDecimal(row[text.Header_AvgHourlyShot].ToString());

                        dt_FilterDuplicatePlan.ImportRow(row);
                    }

                  

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
            dgvMainList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvMainList.MultiSelect = true;
            dgvMainList.DataSource = dt;
            dgvUIEdit(dgvMainList);

            dgvMainList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            dgvMainList.ClearSelection();
            lblSelectedCellSum.Text = "";
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

            LoadCustomerSelection(cmbCustomer);
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
            lblSelectedCellSum.Text = "";
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

            ClearAllDGVData();

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


            dgvMainList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvMainList.SelectAll();
            excelTool.ExportToExcel(text.Report_Type_Production, (DataTable) dgvMainList.DataSource, dgvMainList.GetClipboardContent());


            Cursor = Cursors.Arrow; // change cursor to normal type
            dgvMainList.SelectionMode = DataGridViewSelectionMode.CellSelect;

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

                if(cbKeywordSearchByItem.Checked)
                {
                    cbAllTime.Checked = false;
                }

                SearchByChanging = false;
            }
        }

        private void cbAllTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = !cbAllTime.Checked;
            dtpTo.Enabled = !cbAllTime.Checked;
            ClearAllDGVData();

        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool byDateandShiftType = cmbReportType.Text == text.ReportType_ByDateAndShift;
            cbNote.Visible = byDateandShiftType;

            //cbDateFromStrict.Enabled = byDateandShiftType;
            //cbDateToStrict.Enabled = byDateandShiftType;

            if (!byDateandShiftType)
            {
                cbDateFromStrict.Checked = false;
                cbDateToStrict.Checked = false;


            }

            loadSubListType(cmbSubListType);

            if (formLoaded)
                ClearAllDGVData();
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
            else if (dgv.Columns[col].Name == text.Header_EfficiencyRate)
            {
                decimal efficiencyAlert = decimal.TryParse(txtEfficiencyAlert.Text, out efficiencyAlert) ? efficiencyAlert : 0;
                decimal efficiencyRate = decimal.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out efficiencyRate) ? efficiencyRate : 0;

                if (efficiencyRate < efficiencyAlert)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }

        }

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvMainList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridView dgv = dgvMainList;
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                if (dgvMainList.SelectionMode != DataGridViewSelectionMode.CellSelect)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgvMainList.MultiSelect = true;
                }

                dgv.Rows[row].Cells[col].Selected = true;

                CellSelectedSumUp(dgv);
            }

        }

        private void dgvMainList_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvMainList.SelectionMode != DataGridViewSelectionMode.FullColumnSelect)
            {
                CellSelectedSumUp(dgvMainList);
            }


        }

        private void dgvMainList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMainList;
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string currentHeader = dgv.Columns[colIndex].Name;

                try
                {
                    my_menu.Items.Add(text.ViewJobSheetRecord).Name = text.ViewJobSheetRecord;
                    
                    //my_menu.Items.Add(text.StockLocation).Name = text.StockLocation;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvMainList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            string itemCode = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_PartCode].Value.ToString();
            string jobNo = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();
            string sheetNo = "";

            if(dgv.Columns.Contains(text.Header_SheetID))
                 sheetNo = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_SheetID].Value.ToString();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.ViewJobSheetRecord))
            {
                frmJobSheetViewMode frm = new frmJobSheetViewMode(jobNo, sheetNo, itemCode);

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                frm.Size = new Size(1650, 900);
                frm.ShowDialog();

                frmLoading.CloseForm();
            }
          
            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void MainListColumnToDisplaySettingChanged(object sender, EventArgs e)
        {
            MainListColumnsToDisplaySetting(dgvMainList);
        }

       
        private void dgvMainList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                DataGridView dgv = dgvMainList;
                int col = e.ColumnIndex;

                if(dgvMainList.SelectionMode != DataGridViewSelectionMode.FullColumnSelect)
                {
                    //dgv.Rows[1].Cells[col].Selected = true;

                    dgvMainList.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                }

                dgv.Columns[col].Selected = true;

                CellSelectedSumUp(dgv);
            }
        }

        private void dgvMainList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dgvSorting(DataGridView dgv, string columns)
        {
            if(dgv != null)
            {
                DataTable dt_DGV = (DataTable)dgv.DataSource;

                DataTable dt_Sorted = dt_DGV.Copy();

                int totalRow = dt_DGV.Rows.Count;

                dt_Sorted.DefaultView.Sort = columns + " ASC";
                dt_Sorted = dt_Sorted.DefaultView.ToTable();

                if(dt_Sorted.Rows.Count > 1)
                {
                    string data_Original = dt_DGV.Rows[0][columns].ToString(); 
                    string data_Sorted = dt_Sorted.Rows[0][columns].ToString();

                    if(data_Original == data_Sorted)
                    {
                        dt_Sorted.DefaultView.Sort = columns + " DESC";
                        dt_Sorted = dt_Sorted.DefaultView.ToTable();
                    }

                }

                dgv.DataSource = dt_Sorted;
                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }
        }

        private void dgvMainList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                DataGridView dgv = dgvMainList;
                int col = e.ColumnIndex;

                dgvSorting(dgv, dgv.Columns[col].Name);

                //dgv.Rows[1].Cells[col].Selected = true;

                //if (dgvMainList.SelectionMode != DataGridViewSelectionMode.FullColumnSelect)
                //{
                //    dgvMainList.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                //}

                dgv.ClearSelection();
                dgv.Columns[col].Selected = true;

                //CellSelectedSumUp(dgv);
            }

        }

        private void cbDateFromStrict_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAllTime.Checked && cbDateFromStrict.Checked)
            {
                cbAllTime.Checked = false;
            }
        }

        private void cbDateToStrict_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllTime.Checked && cbDateToStrict.Checked)
            {
                cbAllTime.Checked = false;
            }
        }

        private void LoadCustomerSelection(ComboBox cmb)
        {

            if (formLoaded)
            {
                if (cbMainCustomerOnly.Checked)
                {
                    tool.loadMainCustomerAndAllToComboBox(cmb);
                }
                else
                {
                    tool.loadCustomerAndALLWithoutOtherToComboBox(cmb);

                }

                cmb.Text = text.Cmb_All;
            }

        }
        private void cbMainCustomerOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadCustomerSelection(cmbCustomer);
        }
    }
}
