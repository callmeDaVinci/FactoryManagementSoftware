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
        readonly string cmbByItem = "Item";
        readonly string cmbByDate = "Date";

        readonly string lblItemCat = "CATEGORY";
        readonly string lblItemName = "NAME";
        readonly string lblItemCode = "CODE";

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
        #endregion

        #region UI setting

        public frmInOutReport()
        {      
            InitializeComponent();
            addDataSourceTocmbBy();
            
        }

        private void showItemInput()
        {
            dtpStart.Hide();
            dtpEnd.Hide();

            lblStartOrCat.Text = lblItemCat;
            lblEndOrName.Text = lblItemName;
            lblCode.Text = lblItemCode;

            lblStartOrCat.Show();
            lblEndOrName.Show();
            lblCode.Show();

            cmbCat.Show();
            cmbName.Show();
            cmbCode.Show();
        }

        private void showDateInput()
        {           
            cmbCat.Hide();
            cmbName.Hide();
            cmbCode.Hide();
            lblCode.Hide();

            lblStartOrCat.Text = lblDateStart;
            lblEndOrName.Text = lblDateEnd;

            lblStartOrCat.Show();
            lblEndOrName.Show();

            dtpStart.Show();
            dtpEnd.Show();
        }

        private void hideInput()
        {
            cmbCat.Hide();
            cmbName.Hide();
            cmbCode.Hide();
            dtpStart.Hide();
            dtpEnd.Hide();

            lblStartOrCat.Hide();
            lblEndOrName.Hide();
            lblCode.Hide();
        }

        private void createByDateDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();
            //add ID column
            tool.AddTextBoxColumns(dgv, "ID", dalTrfHist.TrfID, DisplayedCells);

            //add category column
            tool.AddTextBoxColumns(dgv, "CATEGORY", dalTrfHist.TrfItemCat, DisplayedCells);

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

            tool.AddTextBoxColumns(dgv, "UPDATED DATE", dalTrfHist.TrfUpdatedDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "UPDATED BY", dalTrfHist.TrfUpdatedBy, DisplayedCells);

            dgv.Columns[dalTrfHist.TrfQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            tool.listPaintGreyHeader(dgv);
        }

        private void createByItemInOutDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();
           
            tool.AddTextBoxColumns(dgv, "CODE", codeColumnName, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", nameColumnName, Fill);

            //add total qty column
            tool.AddTextBoxColumns(dgv, dateColumnName, dateColumnName, DisplayedCells);

            tool.AddTextBoxColumns(dgv, inQtyColumnName, inQtyColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, outQtyColumnName, outQtyColumnName, DisplayedCells);

            dgv.Columns[inQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[outQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tool.listPaintGreyHeader(dgv);
        }

        private void createByItemInDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();

            tool.AddTextBoxColumns(dgv, "CODE", codeColumnName, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", nameColumnName, Fill);

            //add total qty column
            tool.AddTextBoxColumns(dgv, dateColumnName, dateColumnName, DisplayedCells);

            tool.AddTextBoxColumns(dgv, inQtyColumnName, inQtyColumnName, DisplayedCells);

            dgv.Columns[inQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tool.listPaintGreyHeader(dgv);
        }

        private void createByItemOutDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();

            tool.AddTextBoxColumns(dgv, "CODE", codeColumnName, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", nameColumnName, Fill);

            //add total qty column
            tool.AddTextBoxColumns(dgv, dateColumnName, dateColumnName, DisplayedCells);

            tool.AddTextBoxColumns(dgv, outQtyColumnName, outQtyColumnName, DisplayedCells);

            dgv.Columns[outQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tool.listPaintGreyHeader(dgv);
        }
        #endregion

        #region Load Data

        #region ComboBox Resource
        private void addDataSourceTocmbBy()
        {
            cmbBy.Items.Clear();
            cmbBy.Items.Add(cmbByDate);
            cmbBy.Items.Add(cmbByItem);
            cmbBy.SelectedIndex = -1;
            hideInput();
        }

        private void addDataSourceToItemType()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(cmbItemTypeDay);
            cmbType.Items.Add(cmbItemTypeMonth);
            cmbType.Items.Add(cmbItemTypeYear);
            cmbType.SelectedIndex = -1;

            cmbSubType.DataSource = null;
            cmbSubType.Items.Add(cmbItemSubTypeInOut);
            cmbSubType.Items.Add(cmbItemSubTypeIn);
            cmbSubType.Items.Add(cmbItemSubTypeOut);
            cmbSubType.SelectedIndex = -1;
        }

        private void addDataSourceToDateType()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(CMBAllHeader);
            cmbType.Items.Add(CMBPartHeader);
            cmbType.Items.Add(CMBMaterialHeader);
            cmbType.SelectedIndex = 0;

            cmbSubType.Items.Clear();
        }
        #endregion

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
            createByDateDatagridview(dgv);
            DataTable dt = null;
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");
            string Type = cmbType.Text;
            string subType = cmbSubType.Text;

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
                insertByDateDataToDGV(dgv, dt);
        }

        private void loadItemTrfHistData(DataGridView dgv)
        {
            DataTable dt = null;
            string itemCode = cmbCode.Text;
            string Type = cmbType.Text;
            string subType = cmbSubType.Text;
            int typeNO = 2;
            dt = dalTrfHist.codeSearch(itemCode);

            //check sub type and create datagridview
            if(subType.Equals(cmbItemSubTypeIn))
            {
                typeNO = 1;
                createByItemInDatagridview(dgv);
            }
            else if(subType.Equals(cmbItemSubTypeOut))
            {
                typeNO = 0;
                createByItemOutDatagridview(dgv);
            }
            else
            {
                createByItemInOutDatagridview(dgv);
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
            dgv.Rows[n].Cells[codeColumnName].Value = cmbCode.Text;
            dgv.Rows[n].Cells[nameColumnName].Value = cmbName.Text;

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

        private void insertByDateDataToDGV(DataGridView dgv, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgv.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    if (item["trf_result"].ToString().Equals("Passed"))
                    {
                        int n = dgv.Rows.Add();

                        if(cmbType.Text.Equals(CMBPartHeader) && cmbSubType.Text != CMBAllHeader)
                        {
                            dgv.Rows[n].Cells[dalTrfHist.TrfItemCat].Value = cmbSubType.Text;
                        }
                        else
                        {
                            dgv.Rows[n].Cells[dalTrfHist.TrfItemCat].Value = item[dalTrfHist.TrfItemCat].ToString();
                        }
                        dgv.Rows[n].Cells[dalTrfHist.TrfID].Value = item[dalTrfHist.TrfID].ToString();
                        
                        dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Value = item[dalTrfHist.TrfItemCode].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = item[dalTrfHist.TrfItemName].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfDate].Value = Convert.ToDateTime(item[dalTrfHist.TrfDate]).ToString("dd/MM/yyyy");
                        dgv.Rows[n].Cells[dalTrfHist.TrfFrom].Value = item[dalTrfHist.TrfFrom].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfTo].Value = item[dalTrfHist.TrfTo].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfQty].Value = item[dalTrfHist.TrfQty].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfUnit].Value = item[dalTrfHist.TrfUnit].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfNote].Value = item[dalTrfHist.TrfNote].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfAddedDate].Value = item[dalTrfHist.TrfAddedDate].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfUpdatedDate].Value = item[dalTrfHist.TrfUpdatedDate].ToString();


                        if (int.TryParse(item[dalTrfHist.TrfAddedBy].ToString(), out int i))
                        {
                            dgv.Rows[n].Cells[dalTrfHist.TrfAddedBy].Value = dalUser.getUsername(Convert.ToInt32(item[dalTrfHist.TrfAddedBy]));
                        }
                        
                        if(int.TryParse(item[dalTrfHist.TrfUpdatedBy].ToString(), out int j))
                        {
                            dgv.Rows[n].Cells[dalTrfHist.TrfUpdatedBy].Value = dalUser.getUsername(Convert.ToInt32(item[dalTrfHist.TrfUpdatedBy]));
                        }
                    } 
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

        private void cmbBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (cmbBy.Text.Equals(cmbByItem))
            {
                showItemInput();
                tool.loadItemCategoryDataToComboBox(cmbCat);
                addDataSourceToItemType();
                cmbCat.SelectedIndex = -1;  
            }
            else if(cmbBy.Text.Equals(cmbByDate))
            {
                cmbCat.DataSource = null;
                cmbName.DataSource = null;
                cmbCode.DataSource = null;
                showDateInput();
                addDataSourceToDateType();
                loadTrfHistData(dgvInOutReport);
            }
            else
            {
                hideInput();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            tool.loadItemNameDataToComboBox(cmbName, cmbCat.Text);
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tool.loadItemCodeDataToComboBox(cmbCode, cmbName.Text);
        }

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
            {
                loadItemTrfHistData(dgvInOutReport);
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBy.Text.Equals(cmbByDate))
            {
                if(cmbType.Text.Equals(CMBAllHeader))
                {
                    cmbSubType.DataSource = null;
                }
                else if (cmbType.Text.Equals(CMBPartHeader))
                {
                    tool.loadCustomerAndAllToComboBox(cmbSubType);
                }
                else if (cmbType.Text.Equals(CMBMaterialHeader))
                {
                    tool.loadMaterialAndAllToComboBox(cmbSubType);
                }
                loadTrfHistData(dgvInOutReport);
            }

            else if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
            {
                loadItemTrfHistData(dgvInOutReport);
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            loadTrfHistData(dgvInOutReport);
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            loadTrfHistData(dgvInOutReport);
        }

        private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBy.Text.Equals(cmbByItem) && cmbCode.SelectedIndex != -1 && cmbType.SelectedIndex != -1 && cmbSubType.SelectedIndex != -1)
            {
                loadItemTrfHistData(dgvInOutReport);
            }
        }

        private void frmInOutReport_Click(object sender, EventArgs e)
        {
            dgvInOutReport.ClearSelection();
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            if (cmbBy.Text.Equals(cmbByItem))
            {
                fileName = "TransferReport(" + cmbName.Text +"_"+cmbType.Text+"_"+cmbSubType.Text+ ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
            else if (cmbBy.Text.Equals(cmbByDate))
            {
                fileName = "TransferReport(From:" + dtpStart.Text + "To: "+dtpEnd.Text+"_"+cmbType.Text+"_"+cmbSubType.Text+")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
            
            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
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
                xlWorkSheet.Name = cmbSubType.Text;

                #region Save data to Sheet

                if (cmbBy.Text.Equals(cmbByItem))
                {
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbName.Text + "_" + cmbType.Text + "_" + cmbSubType.Text + ") TRANSFER LIST";
                }
                else if (cmbBy.Text.Equals(cmbByDate))
                {
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (From:" + dtpStart.Text + "To: " + dtpEnd.Text + "_" + cmbType.Text + "_" + cmbSubType.Text + ") TRANSFER LIST";
                }

                //Header and Footer setup
                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                //Page setup
                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
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
                tRange.EntireColumn.AutoFit();
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
    }
}
