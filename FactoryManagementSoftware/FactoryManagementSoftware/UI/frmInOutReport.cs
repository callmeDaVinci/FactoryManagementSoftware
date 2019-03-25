using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutReport : Form
    {
        #region Variable

        readonly string codeColumnName = "item_code";
        readonly string nameColumnName = "item_name";
        readonly string dateColumnName = "DATE";
        readonly string inQtyColumnName = "IN QTY";
        readonly string outQtyColumnName = "OUT QTY";

        readonly string CMBAllHeader = "All";
        readonly string CMBPartHeader = "Parts";
        readonly string CMBMaterialHeader = "Materials";

        readonly string cmbItemTypeDay = "Daily";
        readonly string cmbItemTypeMonth = "Monthly";
        readonly string cmbItemTypeYear = "Yearly";

        readonly string cmbItemSubTypeInOut = "In_Out";
        readonly string cmbItemSubTypeIn = "InOnly";
        readonly string cmbItemSubTypeOut = "OutOnly";

        readonly string lblDateStart = "START";
        readonly string lblDateEnd = "END";

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        #endregion

        #region Class Object
        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();
        joinDAL dalJoin = new joinDAL();
        custDAL dalCust = new custDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        CheckChildBLL uCheckChild = new CheckChildBLL();
        userDAL dalUser = new userDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        Text text = new Text();
        #endregion

        #region UI setting

        public frmInOutReport()
        {      
            InitializeComponent();            
        }

        private void createDailyDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();
            //add ID column
            tool.AddTextBoxColumns(dgv, "ID", dalTrfHist.TrfID, DisplayedCells);

            //add name column
            tool.AddTextBoxColumns(dgv, "CODE", dalTrfHist.TrfItemCode, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", dalTrfHist.TrfItemName, Fill);

            //add total qty column
            tool.AddTextBoxColumns(dgv, "TRANSFER", dalTrfHist.TrfDate, DisplayedCells);

            tool.AddTextBoxColumns(dgv, "FROM", dalTrfHist.TrfFrom, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "TO", dalTrfHist.TrfTo, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "QTY", dalTrfHist.TrfQty, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "UNIT", dalTrfHist.TrfUnit, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "NOTE", dalTrfHist.TrfNote, DisplayedCells);

            tool.AddTextBoxColumns(dgv, "ADDED DATE", dalTrfHist.TrfAddedDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "ADDED BY", dalTrfHist.TrfAddedBy, DisplayedCells);

            dgv.Columns[dalTrfHist.TrfQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            tool.listPaintGreyHeader(dgv);
        }

        private void createTotalDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();

            //add name column
            tool.AddTextBoxColumns(dgv, "CODE", dalTrfHist.TrfItemCode, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", dalTrfHist.TrfItemName, Fill);

            tool.AddTextBoxColumns(dgv, inQtyColumnName, inQtyColumnName, DisplayedCells);

            tool.AddTextBoxColumns(dgv, outQtyColumnName, outQtyColumnName, DisplayedCells);

            dgv.Columns[inQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[outQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tool.listPaintGreyHeader(dgv);

            tool.listPaintGreyHeader(dgv);
        }

        private void createTotalOutDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();

            //add name column
            tool.AddTextBoxColumns(dgv, "CODE", dalTrfHist.TrfItemCode, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", dalTrfHist.TrfItemName, Fill);

            tool.AddTextBoxColumns(dgv, outQtyColumnName, outQtyColumnName, DisplayedCells);

            dgv.Columns[outQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            tool.listPaintGreyHeader(dgv);
        }

        #endregion

        #region Load Data
        private DataTable RemoveNonOutToCustomerData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string trfFrom, trfTo;
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    trfFrom = dt.Rows[i][dalTrfHist.TrfFrom].ToString();
                    trfTo = dt.Rows[i][dalTrfHist.TrfTo].ToString();

                    if(dt.Rows[i][dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        if (tool.getFactoryID(trfFrom) == -1 || tool.getCustID(trfTo) == -1)
                        {
                            dt.Rows[i].Delete();
                        }
                    }
                    else
                    {
                        dt.Rows[i].Delete();
                    }
                     
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private DataTable SortByTrfDateAndRemoveFailedData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (!dt.Rows[i][dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {                     
                        dt.Rows[i].Delete();
                    }

                }
                dt.AcceptChanges();
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "trf_hist_trf_date desc";
            DataTable sortedDT = dv.ToTable();

            return sortedDT;
        }

        private DataTable SortByItemCodeAndRemoveFailedData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (!dt.Rows[i][dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        dt.Rows[i].Delete();
                    }

                }
                dt.AcceptChanges();
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "trf_hist_item_code asc";
            DataTable sortedDT = dv.ToTable();

            return sortedDT;
        }

        private DataTable TotalData(DataTable dt)
        {
            DataTable dtTotal = new DataTable();
            if (dt.Rows.Count > 0)
            {
                dt = SortByItemCodeAndRemoveFailedData(dt);
                dtTotal.Columns.Add("Item Code", typeof(string));
                dtTotal.Columns.Add("Item Name", typeof(string));
                dtTotal.Columns.Add("In", typeof(float));
                dtTotal.Columns.Add("Out", typeof(float));

                float inQty = 0, outQty = 0;
                string itemCode = null;
                string itemName = null;
                string trfFrom, trfTo;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    trfFrom = dt.Rows[i][dalTrfHist.TrfFrom].ToString();
                    trfTo = dt.Rows[i][dalTrfHist.TrfTo].ToString();

                    if (itemCode == null)
                    {
                        itemCode = dt.Rows[i][dalTrfHist.TrfItemCode].ToString();
                        itemName = dt.Rows[i][dalTrfHist.TrfItemName].ToString();
                    }

                    if(itemCode == dt.Rows[i][dalTrfHist.TrfItemCode].ToString())
                    {
                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            inQty += Convert.ToSingle(dt.Rows[i][dalTrfHist.TrfQty]);
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                        {
                            outQty += Convert.ToSingle(dt.Rows[i][dalTrfHist.TrfQty]);
                        }
                    }
                    else
                    {
                        dtTotal.Rows.Add(new Object[]{itemCode,itemName,inQty,outQty});

                        itemCode = dt.Rows[i][dalTrfHist.TrfItemCode].ToString();
                        itemName = dt.Rows[i][dalTrfHist.TrfItemName].ToString();

                        inQty = 0;
                        outQty = 0;
                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            inQty += Convert.ToSingle(dt.Rows[i][dalTrfHist.TrfQty]);
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                        {
                            outQty += Convert.ToSingle(dt.Rows[i][dalTrfHist.TrfQty]);
                        }
                    }

                    //last data
                    if(i == dt.Rows.Count - 1)
                    {
                        dtTotal.Rows.Add(new Object[] { itemCode, itemName, inQty, outQty });
                    }
                }
            }

            DataView dv = dtTotal.DefaultView;
            dv.Sort = "Item Code asc";
            DataTable sortedDT = dv.ToTable();

            return sortedDT;
        }


        #region Load Result
        //type: 2.in/out, 1.in, 0.out
        private void DailyInOutCalculate(DataTable dt, DataGridView dgv,int type)
        {
            float inQty = 0;
            float outQty = 0;
            string currentDate = null;
            
            int rowCount = dt.Rows.Count;
            int foreachCount = 0;
            if (rowCount > 0)
            {
                dgv.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    foreachCount++;
                    if (item[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        string trfFrom = item[dalTrfHist.TrfFrom].ToString();
                        string trfTo = item[dalTrfHist.TrfTo].ToString();
                        string trfDate = item[dalTrfHist.TrfDate].ToString();

                        if (currentDate == null)//first in data
                        {
                            currentDate = trfDate;
                        }

                        if (currentDate == trfDate && !string.IsNullOrEmpty(trfDate))//compare with last data record date
                        {
                            //from non-factory to factory: IN
                            if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1 && type != 0)
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                            //from factory to non-factory: OUT
                            else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1 && type != 1)
                            {
                                outQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                        }
                        else
                        {
                            //print data
                            if(inQty != 0 || outQty != 0)
                            {
                                int n = dgv.Rows.Add();
                                insertByItemDataToDGV(dgvInOutReport, n, currentDate, inQty, outQty, type);
                                inQty = 0;
                                outQty = 0;
                            }
                            currentDate = trfDate;

                            if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                            //from factory to non-factory: OUT
                            else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                            {
                                outQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }
                        }

                        if(foreachCount == dt.Rows.Count)
                        {
                            if (inQty != 0 || outQty != 0)
                            {
                                int n = dgv.Rows.Add();
                                insertByItemDataToDGV(dgvInOutReport, n, currentDate, inQty, outQty, type);
                                inQty = 0;
                                outQty = 0;
                            }
                        }
                        
                    }
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[nameColumnName].Value = "No data exist";
            }
        }

        //type: 2.in/out, 1.in, 0.out
        private void MonthlyInOutCalculate(DataTable dt, DataGridView dgv, int type)
        {
            float inQty = 0;
            float outQty = 0;
            string currentDate = null;
            string currentMonth = null;
            string currentYear = null;

            int rowCount = dt.Rows.Count;
            int foreachCount = 0;
            if (rowCount > 0)
            {
                dgv.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    foreachCount++;
                    if (item[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        string trfFrom = item[dalTrfHist.TrfFrom].ToString();
                        string trfTo = item[dalTrfHist.TrfTo].ToString();
                        string trfDate = item[dalTrfHist.TrfDate].ToString();
                        string trfMonth = Convert.ToDateTime(trfDate).Month.ToString();
                        string trfYear = Convert.ToDateTime(trfDate).Year.ToString();

                        if (currentDate == null)//first in data
                        {
                            currentDate = trfDate;
                            currentMonth = trfMonth;
                            currentYear = trfYear;
                        }

                        if (currentMonth == trfMonth && currentYear == trfYear && !string.IsNullOrEmpty(trfDate))//compare with last data record date
                        {
                            //from non-factory to factory: IN
                            if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1 && type != 0)
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                            //from factory to non-factory: OUT
                            else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1 && type != 1)
                            {
                                outQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                        }
                        else
                        {
                            //print data
                            if (inQty != 0 || outQty != 0)
                            {
                                int n = dgv.Rows.Add();
                                string date = Convert.ToDateTime(currentDate).ToString("MMMM") + Convert.ToDateTime(currentDate).ToString("yyyy");
                                insertByItemDataToDGV(dgvInOutReport, n, date, inQty, outQty, type);
                                inQty = 0;
                                outQty = 0;
                            }
                            currentDate = trfDate;
                            currentMonth = trfMonth;
                            currentYear = trfYear;

                            if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                            //from factory to non-factory: OUT
                            else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                            {
                                outQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }
                        }

                        if (foreachCount == dt.Rows.Count)
                        {
                            if (inQty != 0 || outQty != 0)
                            {
                                int n = dgv.Rows.Add();
                                string date = Convert.ToDateTime(currentDate).ToString("MMMM") + Convert.ToDateTime(currentDate).ToString("yyyy");
                                insertByItemDataToDGV(dgvInOutReport, n, date, inQty, outQty, type);
                                inQty = 0;
                                outQty = 0;
                            }
                        }

                    }
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[nameColumnName].Value = "No data exist";
            }
        }

        //type: 2.in/out, 1.in, 0.out
        private void YearlyInOutCalculate(DataTable dt, DataGridView dgv, int type)
        {
            float inQty = 0;
            float outQty = 0;
            string currentDate = null;
            string currentYear = null;

            int rowCount = dt.Rows.Count;
            int foreachCount = 0;
            if (rowCount > 0)
            {
                dgv.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    foreachCount++;
                    if (item[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        string trfFrom = item[dalTrfHist.TrfFrom].ToString();
                        string trfTo = item[dalTrfHist.TrfTo].ToString();
                        string trfDate = item[dalTrfHist.TrfDate].ToString();
                        string trfYear = Convert.ToDateTime(trfDate).Year.ToString();

                        if (currentDate == null)//first in data
                        {
                            currentDate = trfDate;
                            currentYear = trfYear;
                        }

                        if (currentYear == trfYear && !string.IsNullOrEmpty(trfDate))//compare with last data record date
                        {
                            //from non-factory to factory: IN
                            if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1 && type != 0)
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                            //from factory to non-factory: OUT
                            else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1 && type != 1)
                            {
                                outQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                        }
                        else
                        {
                            //print data
                            if (inQty != 0 || outQty != 0)
                            {
                                int n = dgv.Rows.Add();
                                string date = Convert.ToDateTime(currentDate).ToString("yyyy");
                                insertByItemDataToDGV(dgvInOutReport, n, date, inQty, outQty, type);
                                inQty = 0;
                                outQty = 0;
                            }
                            currentDate = trfDate;
                            currentYear = trfYear;

                            if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }

                            //from factory to non-factory: OUT
                            else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                            {
                                outQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }
                        }

                        if (foreachCount == dt.Rows.Count)
                        {
                            if (inQty != 0 || outQty != 0)
                            {
                                int n = dgv.Rows.Add();
                                string date = Convert.ToDateTime(currentDate).ToString("yyyy");
                                insertByItemDataToDGV(dgvInOutReport, n, date, inQty, outQty, type);
                                inQty = 0;
                                outQty = 0;
                            }
                        }

                    }
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[nameColumnName].Value = "No data exist";
            }
        }

        private void loadTrfHistData(DataGridView dgv)
        {
            //createByDateDatagridview(dgv);
            DataTable dt = null;
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");
            string Type = cmbType.Text;
            string subType = cmbType.Text;

            if (Type.Equals(CMBAllHeader))
            {
                //check type: all/part/material
                //if all: show all trf hist during selected period
                dt = dalTrfHist.rangeTrfSearch(start, end);
            }
            else if (Type.Equals(CMBPartHeader))
            {
                //if part: check sub type: all/customer
                //if all: show all part trf hist during selected period
                if (subType.Equals(CMBAllHeader))
                {
                    dt = dalTrfHist.rangePartTrfSearch(start, end);
                }
                else if (!string.IsNullOrEmpty(subType))
                {
                    //if customer: show selected customer's part only
                    dt = dalTrfHist.rangePartTrfSearch(start, end, tool.getCustID(subType));
                }
            }
            else if (Type.Equals(CMBMaterialHeader))
            {
                //show material transfer history during selected period
                dt = dalTrfHist.rangeMaterialTrfSearch(start, end, subType);
            }

            if (dt != null)
                insertTotalDataToDGV(dgv, dt);
        }

        private void loadItemTrfHistData(DataGridView dgv)
        {
            DataTable dt = null;
            string itemCode = "";
            string Type = cmbType.Text;
            string subType = cmbType.Text;
            int typeNO = 2;
            dt = dalTrfHist.codeSearch(itemCode);

            //check sub type and create datagridview
            if(subType.Equals(cmbItemSubTypeIn))
            {
                typeNO = 1;
                //createByItemInDatagridview(dgv);
            }
            else if(subType.Equals(cmbItemSubTypeOut))
            {
                typeNO = 0;
                //createByItemOutDatagridview(dgv);
            }
            else
            {
                //createByItemInOutDatagridview(dgv);
            }

            if (Type.Equals(cmbItemTypeDay))
            {
                //calculate daily data
                DailyInOutCalculate(dt,dgv,typeNO);
            }
            else if(Type.Equals(cmbItemTypeMonth))
            {
                //calculate monthly data
                MonthlyInOutCalculate(dt, dgv, typeNO);
            }
            else if(Type.Equals(cmbItemTypeYear))
            {
                //calculate yearly data
                YearlyInOutCalculate(dt, dgv, typeNO);
            }
        }

        private void insertByItemDataToDGV(DataGridView dgv, int n, string Date, float inQty, float outQty, int type)
        {
            dgv.Rows[n].Cells[codeColumnName].Value = "";
            dgv.Rows[n].Cells[nameColumnName].Value = "";

            if(cmbType.Text.Equals(cmbItemTypeDay))
            {
                dgv.Rows[n].Cells[dateColumnName].Value = Convert.ToDateTime(Date).ToString("dd/MM/yyyy");
            }
            else
            {
                dgv.Rows[n].Cells[dateColumnName].Value = Date;
            }
            
            
            if(type != 0)
            {
                dgv.Rows[n].Cells[inQtyColumnName].Value = inQty.ToString();
            }

            if (type != 1)
            {
                dgv.Rows[n].Cells[outQtyColumnName].Value = outQty.ToString();
            }
      
        }

        private void insertTotalDataToDGV(DataGridView dgv, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                createTotalDatagridview(dgv);
                dgv.Rows.Clear();

                dt = TotalData(dt);

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                        
                    dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Value = item["Item Code"].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = item["Item Name"].ToString();
                    dgv.Rows[n].Cells[inQtyColumnName].Value = item["In"].ToString();
                    dgv.Rows[n].Cells[outQtyColumnName].Value = item["Out"].ToString();
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = "No data exist";
            }
        }

        private void insertTotalDODataToDGV(DataGridView dgv, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                createTotalOutDatagridview(dgv);
                dgv.Rows.Clear();

                dt = TotalData(dt);

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();

                    dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Value = item["Item Code"].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = item["Item Name"].ToString();
                    dgv.Rows[n].Cells[outQtyColumnName].Value = item["Out"].ToString();
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = "No data exist";
            }
        }

        private void insertDailyDataToDGV(DataGridView dgv, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                createDailyDatagridview(dgv);
                dgv.Rows.Clear();

                if (cbSortByItem.Checked)
                {
                    dt = SortByItemCodeAndRemoveFailedData(dt);
                }
                else
                {
                    dt = SortByTrfDateAndRemoveFailedData(dt);
                }

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();

                    dgv.Rows[n].Cells[dalTrfHist.TrfID].Value = item[dalTrfHist.TrfID].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Value = item[dalTrfHist.TrfItemCode].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = item[dalTrfHist.TrfItemName].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfDate].Value = Convert.ToDateTime(item[dalTrfHist.TrfDate]).ToString("dd/MM/yyyy");
                    dgv.Rows[n].Cells[dalTrfHist.TrfFrom].Value = item[dalTrfHist.TrfFrom].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfTo].Value = item[dalTrfHist.TrfTo].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfQty].Value = item[dalTrfHist.TrfQty].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfUnit].Value = item[dalTrfHist.TrfUnit].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfNote].Value = item[dalTrfHist.TrfNote].ToString();
                    dgv.Rows[n].Cells[dalTrfHist.TrfAddedDate].Value = Convert.ToDateTime(item[dalTrfHist.TrfAddedDate]).ToString("dd/MM/yyyy");
                    dgv.Rows[n].Cells[dalTrfHist.TrfAddedBy].Value = dalUser.getUsername(Convert.ToInt32(item["trf_hist_added_by"])); ;
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = "No data exist";
            }
        }

        private void frmInOutReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.InOutReportFormOpen = false;
        }

        #endregion

        #endregion

        #region Text/Index Changed, Click

        private void frmInOutReport_Click(object sender, EventArgs e)
        {
            dgvInOutReport.ClearSelection();
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";
            string title = cmbType.Text;
            DateTime currentDate = DateTime.Now;

            if (cbDaily.Checked)
            {
                title += "_Daily";
            }
            else
            {
                title += "_Total";
            }

            if (cbDO.Checked)
            {
                title += "_OutReport";
            }
            else
            {
                title += "_InOutReport";
            }

            fileName = title+"(From_" + dtpStart.Text + "_To_"+dtpEnd.Text+")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            
            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path = @"D:\StockAssistant\Document\InOutReport";
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

                    if(string.IsNullOrEmpty(cmbType.Text))
                    {
                        if(!string.IsNullOrEmpty(cmbType.Text))
                        {
                            xlWorkSheet.Name = cmbType.Text;
                        }
                        else
                        {
                            xlWorkSheet.Name = "NAME NOT FOUND";
                        }
                        
                    }
                    else
                    {
                        xlWorkSheet.Name = cmbType.Text;
                    }


                    #region Save data to Sheet

                    string title = cmbType.Text;

                    if (cbDaily.Checked)
                    {
                        title += "_Daily";
                    }
                    else
                    {
                        title += "_Total";
                    }

                    if (cbDO.Checked)
                    {
                        title += "_OutReport";
                    }
                    else
                    {
                        title += "_InOutReport";
                    }

                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + dtpStart.Text + "-" + dtpEnd.Text + ")" +title;
                    
                    //Header and Footer setup
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                    //Page setup
                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;

                    if (cbDO.Checked)
                    {
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                    }
                    else
                    {
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    }
                    
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

        private void frmInOutReport_Load(object sender, EventArgs e)
        {
            tool.DoubleBuffered(dgvInOutReport, true);

            if (cbMat.Checked)
            {
                tool.loadMaterialAndAllToComboBox(cmbType);
            }
            else
            {
                tool.loadCustomerAndAllToComboBox(cmbType);
            }
        }

        #region backup

        //private void cmbBy_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

        //    if (cmbBy.Text.Equals(cmbByItem))
        //    {
        //        showItemInput();
        //        tool.loadItemCategoryDataToComboBox(cmbCat);
        //        addDataSourceToItemType();
        //        cmbCat.SelectedIndex = -1;
        //    }
        //    else if (cmbBy.Text.Equals(cmbByDate))
        //    {
        //        cmbCat.DataSource = null;
        //        cmbName.DataSource = null;
        //        cmbCode.DataSource = null;
        //        showDateInput();
          //      addDataSourceToDateType();
           //     loadTrfHistData(dgvInOutReport);
        //    }
        //    else
        //    {
        //        hideInput();
        //    }

        //    Cursor = Cursors.Arrow; // change cursor to normal type
        //}

        //private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    tool.loadItemNameDataToComboBox(cmbName, cmbCat.Text);
        //}

        //private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    tool.loadItemCodeDataToComboBox(cmbCode, cmbName.Text);
        //}

        //private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
        //    {
        //        loadItemTrfHistData(dgvInOutReport);
        //    }
        //}

        //private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbBy.Text.Equals(cmbByDate))
        //    {
        //        if (cmbType.Text.Equals(CMBAllHeader))
        //        {
        //            cmbSubType.DataSource = null;
        //        }
        //        else if (cmbType.Text.Equals(CMBPartHeader))
        //        {
        //            tool.loadCustomerAndAllToComboBox(cmbSubType);
        //        }
        //        else if (cmbType.Text.Equals(CMBMaterialHeader))
        //        {
        //            tool.loadMaterialAndAllToComboBox(cmbSubType);
        //        }
        //        loadTrfHistData(dgvInOutReport);
        //    }

        //    else if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
        //    {
        //        loadItemTrfHistData(dgvInOutReport);
        //    }
        //}

        //private void dtpStart_ValueChanged(object sender, EventArgs e)
        //{
        //    loadTrfHistData(dgvInOutReport);
        //}

        //private void dtpEnd_ValueChanged(object sender, EventArgs e)
        //{
        //    loadTrfHistData(dgvInOutReport);
        //}

        //private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
        //    {
        //        loadItemTrfHistData(dgvInOutReport);
        //    }
        //    else if (cmbBy.Text.Equals(cmbByDate))
        //    {
        //        loadTrfHistData(dgvInOutReport);
        //    }

        //    //try
        //    //{
        //    //    if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
        //    //    {
        //    //        loadItemTrfHistData(dgvInOutReport);
        //    //    }
        //    //    else if(cmbBy.Text.Equals(cmbByDate))
        //    //    {
        //    //        loadTrfHistData(dgvInOutReport);
        //    //    }

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    tool.saveToTextAndMessageToUser(ex);
        //    //}
        //}

        //private void frmInOutReport_Click(object sender, EventArgs e)
        //{
        //    dgvInOutReport.ClearSelection();
        //}

        #endregion

        private void cbMat_CheckedChanged(object sender, EventArgs e)
        {
            if(cbMat.Checked)
            {
                cbPart.Checked = false;
                tool.loadMaterialAndAllToComboBox(cmbType);
            }
        }

        private void cbPart_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPart.Checked)
            {
                cbMat.Checked = false;
                tool.loadCustomerAndAllToComboBox(cmbType);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvInOutReport;
            dgv.Rows.Clear();
            DataTable dt = null;
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");
            string Type = cmbType.Text;

            if (cbPart.Checked)
            {
                //if part: check sub type: all/customer
                //if all: show all part trf hist during selected period
                if (Type.Equals(CMBAllHeader))
                {
                    dt = dalTrfHist.rangePartTrfSearch(start, end);
                }
                else
                {
                    //if customer: show selected customer's part only
                    dt = dalTrfHist.rangePartTrfSearch(start, end, tool.getCustID(Type));
                }
            }
            else if (Type.Equals(CMBMaterialHeader))
            {
                //show material transfer history during selected period
                dt = dalTrfHist.rangeMaterialTrfSearch(start, end, Type);
            }

            if (dt != null)
            {
                if(cbDO.Checked)
                {
                    //show only out to customer's transfer data
                    dt = RemoveNonOutToCustomerData(dt);
                }

                if(cbDaily.Checked)
                {
                    insertDailyDataToDGV(dgv, dt);
                }
                else
                {
                    
                    if (cbDO.Checked)
                    {
                        //show only out to customer's transfer data
                        insertTotalDODataToDGV(dgv, dt);
                    }
                    else
                    {
                        insertTotalDataToDGV(dgv, dt);
                    }
                }               
            }               
        }

        private void cbDaily_CheckedChanged(object sender, EventArgs e)
        {
            if(cbDaily.Checked)
            {
                cbSortByItem.Checked = false;
            }
            else
            {
                cbSortByItem.Checked = true;
            }
        }
    }
}
