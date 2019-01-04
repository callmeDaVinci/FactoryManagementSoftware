﻿using FactoryManagementSoftware.DAL;
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

namespace FactoryManagementSoftware.UI
{
    public partial class frmPMMA : Form
    {
        public frmPMMA()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            InitializeComponent();
            dataSourceSetup();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #region Class Object
        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();
        joinDAL dalJoin = new joinDAL();
        custDAL dalCust = new custDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        CheckChildBLL uCheckChild = new CheckChildBLL();
        userDAL dalUser = new userDAL();
        pmmaDAL dalPMMA = new pmmaDAL();
        pmmaBLL uPMMA = new pmmaBLL();
        trfHistDAL dalTrfHist = new trfHistDAL();

        materialUsedBLL uMatUsed = new materialUsedBLL();
        materialUsedDAL dalMatUsed = new materialUsedDAL();

        Tool tool = new Tool();
        #endregion

        #region Variable
        readonly string cmbTypeActual = "Actual";
        readonly string cmbTypeForecast = "Forecast";
        readonly string IndexColumnName = "NO";
        readonly string IndexInName = "IN";
        readonly string IndexOutName = "OUT";
        readonly string IndexPercentageName = "%";
        readonly string IndexWastageName = "WASTAGE";
        readonly string IndexBalName = "BAL STOCK";
        // readonly string IndexBalWithWastageName = "BAL STOCK With Wastage";
        private int index = 0;

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        #endregion

        #region UI setting

        private void createColumnsForMonthlyReportDGV()
        {
            DataGridView dgv = dgvPMMA;

            dgv.Columns.Clear();        
          
            tool.AddTextBoxColumns(dgv, "NO", IndexColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "CODE", dalItem.ItemCode, Fill);
            tool.AddTextBoxColumns(dgv, "NAME", dalItem.ItemName, Fill);
            tool.AddTextBoxColumns(dgv, "OPENING STOCK", dalPMMA.OpenStock, DisplayedCells);

            tool.AddTextBoxColumns(dgv, IndexInName, IndexInName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexOutName, IndexOutName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexBalName, IndexBalName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexPercentageName, IndexPercentageName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexWastageName, IndexWastageName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexBalName, dalPMMA.BalStock, DisplayedCells);


            dgv.Columns[dalPMMA.OpenStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexInName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexOutName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexBalName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexPercentageName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexWastageName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[dalPMMA.BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dalUser.getPermissionLevel(MainDashboard.USER_ID) >= 3)
            {
                dgv.Columns[IndexColumnName].ReadOnly = true;
                dgv.Columns[dalItem.ItemCode].ReadOnly = true;
                dgv.Columns[dalItem.ItemName].ReadOnly = true;
                
                dgv.Columns[dalPMMA.OpenStock].ReadOnly = false;
                dgv.Columns[IndexInName].ReadOnly = false;
                dgv.Columns[IndexOutName].ReadOnly = false;

                dgv.Columns[IndexBalName].ReadOnly = true;
                dgv.Columns[IndexPercentageName].ReadOnly = false;
                dgv.Columns[IndexWastageName].ReadOnly = true;
                dgv.Columns[dalPMMA.BalStock].ReadOnly = true;
            }
            else
            {
                dgv.Columns[IndexColumnName].ReadOnly = true;
                dgv.Columns[dalItem.ItemCode].ReadOnly = true;
                dgv.Columns[dalItem.ItemName].ReadOnly = true;

                dgv.Columns[dalPMMA.OpenStock].ReadOnly = true;
                dgv.Columns[IndexInName].ReadOnly = true;
                dgv.Columns[IndexOutName].ReadOnly = true;

                dgv.Columns[IndexBalName].ReadOnly = true;
                dgv.Columns[IndexPercentageName].ReadOnly = true;
                dgv.Columns[IndexWastageName].ReadOnly = true;
                dgv.Columns[dalPMMA.BalStock].ReadOnly = true;
            }
        }

        #endregion

        #region load data

        private void dataSourceSetup()
        {
            // Set the Format type and the CustomFormat string.
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "MMMM yyyy";
            dtpDate.ShowUpDown = true;// to prevent the calendar from being displayed

            //Out Type
            cmbType.Items.Clear();
            cmbType.Items.Add(cmbTypeActual);
            cmbType.Items.Add(cmbTypeForecast);
            cmbType.SelectedIndex = 0;
        }

        private DataTable AddDuplicates(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        break;
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (dt.Rows[i]["item_code"].ToString() == dt.Rows[j]["item_code"].ToString())
                        {
                            float readyStock = dalItem.getStockQty(dt.Rows[i]["item_code"].ToString());

                            float jQty = Convert.ToSingle(dt.Rows[j]["quantity_order"].ToString());
                            float iQty = Convert.ToSingle(dt.Rows[i]["quantity_order"].ToString());

                            //float stillOne = readyStock - jQty;
                            //float stllTwo = readyStock - iQty;

                            //float actualBalance = readyStock - stillOne - stllTwo;
                            dt.Rows[j]["quantity_order"] = (jQty + iQty).ToString();
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        private string getCurrentForecastMonth()
        {
            string month = "";
            string custName = tool.getCustName(1);

            DataTable dt = dalItemCust.custSearch(custName);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    month = item["forecast_current_month"].ToString();
                }
            }
            return month;
        }

        private DataTable insertItemMaterialData()
        {
            string custName = tool.getCustName(1);
            DataTable dtMat = new DataTable();

            dtMat.Columns.Add(dalItem.ItemMaterial);
            
            DataTable dt = dalItemCust.custSearch(custName);

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                string itemCode;
                string itemMat;

                foreach (DataRow item in dt.Rows)
                {
                    itemCode = item["item_code"].ToString();
                    itemMat = item[dalItem.ItemMaterial].ToString();

                    if (tool.ifGotChild(itemCode))
                    {
                        DataTable dtJoin = dalJoin.parentCheck(itemCode);
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            dtMat.Rows.Add(dalItem.getMaterialType(Join["join_child_code"].ToString()));
                        }
                    }
                    else
                    {
                        dtMat.Rows.Add(itemMat);
                    }
                }
            }
            
            return dtMat;
        }

        private void getMonthlyStockData()
        {
            createColumnsForMonthlyReportDGV();
            dgvPMMA.Rows.Clear();
            dgvPMMA.Refresh();
            int index = 1;
            DataTable dt = insertItemMaterialData();
            dt = tool.RemoveDuplicates(dt, "item_material");
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            float openningStock = 0;
            float percentage = 0;
            string itemCode, month, year;

            month = Convert.ToDateTime(dtpDate.Text).Month.ToString();
            year = Convert.ToDateTime(dtpDate.Text).Year.ToString();

            foreach (DataRow item in dt.Rows)
            {
                openningStock = 0;
                itemCode = item["item_material"].ToString();

                DataTable searchdt = dalPMMA.Search(itemCode, month, year);

                //get last month bal
                openningStock = getLastMonthBal(itemCode, month, year);
                percentage = getLastMonthPercentage(itemCode, month, year);

                if (openningStock == -1)
                {
                    openningStock = 0;
                    foreach (DataRow pmma in searchdt.Rows)
                    {
                        if (float.TryParse(pmma[dalPMMA.OpenStock].ToString(), out float i))
                        {
                            openningStock += Convert.ToSingle(pmma[dalPMMA.OpenStock]);
                        }
                    }
                }

                if (searchdt.Rows.Count <= 0)
                {
                    //insert new data to table pmma
                    insertDataToPMMA(itemCode, Convert.ToDateTime(dtpDate.Text), openningStock);
                }
                
                loadDataToDGV(itemCode, openningStock, percentage, index, month, year);
                index++;
            }
        }

        private void loadDataToDGV(string itemCode, float openningStock, float percentage, int index, string month, string year)
        {
            DataGridView dgv = dgvPMMA;
            int n = dgv.Rows.Add();

            float inQty = calculateInRecord(itemCode, month, year);
            float outQty = calculateOutRecord(itemCode, month, year);
            float bal = openningStock + inQty - outQty;
                  
            float wastage = outQty * percentage;

            dgv.Rows[n].Cells[IndexColumnName].Value = index;
            dgv.Rows[n].Cells[dalItem.ItemCode].Value = itemCode;
            dgv.Rows[n].Cells[dalItem.ItemName].Value = dalItem.getMaterialName(itemCode);
            dgv.Rows[n].Cells[dalPMMA.OpenStock].Value = openningStock.ToString("0.000");
            dgv.Rows[n].Cells[IndexInName].Value = inQty.ToString("0.000");
            dgv.Rows[n].Cells[IndexOutName].Value = outQty.ToString("0.000");
            dgv.Rows[n].Cells[IndexBalName].Value = bal.ToString("0.000");

            if (outQty == 0)
            {
                percentage = 0;
            }

            dgv.Rows[n].Cells[IndexPercentageName].Value = percentage;
            dgv.Rows[n].Cells[IndexWastageName].Value = wastage;
            dgv.Rows[n].Cells[dalPMMA.BalStock].Value = (bal - wastage).ToString("0.000");

            //update balance stock to table pmma
            updateDataToPMMA( itemCode, Convert.ToDateTime(dtpDate.Text), openningStock,percentage, bal - wastage);
        }

        private float calculateInRecord(string itemCode, string month, string year)
        {
            DataTable dt = dalTrfHist.TrfSearch(itemCode, month, year);
            float inQty = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        string trfFrom = item[dalTrfHist.TrfFrom].ToString();
                        string trfTo = item[dalTrfHist.TrfTo].ToString();

                        //from non-factory to factory: IN
                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            if (float.TryParse(item[dalTrfHist.TrfQty].ToString(), out float i))
                            {
                                inQty += Convert.ToSingle(item[dalTrfHist.TrfQty]);
                            }
                            
                        } 
                        
                    }
                }
            }
         
            return inQty;
        }

        private float calculateOutRecord(string itemCode, string month, string year)
        {
            bool result = dalMatUsed.Delete();

            if(cmbType.Text.Equals(cmbTypeForecast))
            {
                insertItemForecastData(checkIfForecastExist());
            }
            else
            {
                insertItemQuantityOrderData(month, year);
            }
            
            int index = 1;
            string materialType = null;
            int OrderQty;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;

            DataTable dt = dalMatUsed.Select();
            dt = AddDuplicates(dt);
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow item in dt.Rows)
            {
                itemWeight = Convert.ToSingle(item["item_part_weight"].ToString());
                OrderQty = Convert.ToInt32(item["quantity_order"].ToString());
                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                materialUsed = OrderQty * itemWeight / 1000;

                wastageUsed = materialUsed * wastagePercetage;

                if (string.IsNullOrEmpty(materialType))
                {
                    materialType = item["item_material"].ToString();

                    totalMaterialUsed = materialUsed + wastageUsed;
                }
                else if (materialType == item["item_material"].ToString())
                {
                    //same data
                    totalMaterialUsed += materialUsed + wastageUsed;
                }
                else
                {
                    if(materialType == itemCode)
                    {
                        return totalMaterialUsed;
                    }
                    //first data
                    materialType = item["item_material"].ToString();
                    totalMaterialUsed = materialUsed + wastageUsed;
                }


                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    totalMaterialUsed = materialUsed + wastageUsed;
                    if (materialType == itemCode)
                    {
                        return totalMaterialUsed;
                    }
                }
                index++;
            }
            return totalMaterialUsed;
        }

        private void insertItemForecastData(int type)
        {
            int ForecastQty = 0;
            string forecast = "forecast_one";

            if (type == 2)
            {
                forecast = "forecast_two";
            }
            else if(type == 3)
            {
                forecast = "forecast_three";
            }

            string custName = tool.getCustName(1);

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    string itemCode;
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();
                        ForecastQty = Convert.ToInt32(item[forecast]);

                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();
                                uMatUsed.quantity_order = ForecastQty;

                                bool result = dalMatUsed.Insert(uMatUsed);
                                if (!result)
                                {
                                    MessageBox.Show("failed to insert material used data");
                                    return;
                                }
                                else
                                {
                                    forecastIndex++;
                                }
                            }
                        }
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = ForecastQty;

                            bool result = dalMatUsed.Insert(uMatUsed);
                            if (!result)
                            {
                                MessageBox.Show("failed to insert material used data");
                                return;
                            }
                            else
                            {
                                forecastIndex++;
                            }
                        }
                    }
                }
            }

        }

        private void insertItemQuantityOrderData(string month, string year)
        {
            string custName = tool.getCustName(1);

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int outStock = 0;
                    string itemCode;
                                 
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = dalTrfHist.ItemToCustomerDateSearch(custName, month, year, itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                if (outRecord["trf_result"].ToString().Equals("Passed"))
                                {
                                    outStock += Convert.ToInt32(outRecord["trf_hist_qty"]);
                                }
                            }
                        }

                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();
                                uMatUsed.quantity_order = outStock;

                                bool result = dalMatUsed.Insert(uMatUsed);
                                if (!result)
                                {
                                    MessageBox.Show("failed to insert material used data");
                                    return;
                                }
                                else
                                {
                                    forecastIndex++;
                                }
                            }
                        }
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = outStock;

                            bool result = dalMatUsed.Insert(uMatUsed);
                            if (!result)
                            {
                                MessageBox.Show("failed to insert material used data");
                                return;
                            }
                            else
                            {
                                forecastIndex++;
                            }
                        }
                    }
                }
            }

        }

        private float getLastMonthBal(string itemCode, string month, string year)
        {
            float lastMonthBal = 0;
            if(month.Equals("1"))
            {
                month = "12";
                year = (Convert.ToInt32(year) - 1).ToString();
            }
            else
            {
                month = (Convert.ToInt32(month) - 1).ToString();
            }

            DataTable dt = dalPMMA.Search(itemCode, month, year);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow pmma in dt.Rows)
                {
                    if (float.TryParse(pmma[dalPMMA.BalStock].ToString(), out float i))
                    {
                        lastMonthBal += Convert.ToSingle(pmma[dalPMMA.BalStock]);
                    }
                    
                }
            }
            else
            {
                lastMonthBal = -1;
            }

            return lastMonthBal;
        }

        private float getLastMonthPercentage(string itemCode, string month, string year)
        {
            float lastMonthPercentage = 0.5f;
            if (month.Equals("1"))
            {
                month = "12";
                year = (Convert.ToInt32(year) - 1).ToString();
            }
            else
            {
                month = (Convert.ToInt32(month) - 1).ToString();
            }

            DataTable dt = dalPMMA.Search(itemCode, month, year);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow pmma in dt.Rows)
                {
                    if (float.TryParse(pmma[dalPMMA.BalStock].ToString(), out float i))
                    {
                        lastMonthPercentage = Convert.ToSingle(pmma[dalPMMA.Percentage]);
                    }

                }
            }

            return lastMonthPercentage;
        }

        private void insertDataToPMMA(string itemCode,DateTime date, float openingStock)
        {
            uPMMA.pmma_item_code = itemCode;
            uPMMA.pmma_date = date;
            uPMMA.pmma_openning_stock = openingStock;
            uPMMA.pmma_added_date = DateTime.Now;
            uPMMA.pmma_added_by = MainDashboard.USER_ID;

            bool success = dalPMMA.insert(uPMMA);

            if(!success)
            {
                MessageBox.Show("Failed to insert data to PMMA");
            }
        }

        private void updateDataToPMMA(string itemCode, DateTime date, float openingStock, float percentage, float balStock)
        {
            uPMMA.pmma_item_code = itemCode;
            uPMMA.pmma_date = date;
            uPMMA.pmma_openning_stock = openingStock;
            uPMMA.pmma_percentage = percentage;
            uPMMA.pmma_bal_stock =  balStock;
            uPMMA.pmma_updated_date = DateTime.Now;
            uPMMA.pmma_updated_by = MainDashboard.USER_ID;

            bool success = dalPMMA.update(uPMMA);

            if (!success)
            {
                MessageBox.Show("Failed to insert data to PMMA");
            }
        }

        private void loadMaterialList(DataTable dt)
        {
            DataGridView dgv = dgvPMMA;
            index = 1;
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            string itemCode;
            dgv.Rows.Clear();
            dgv.Refresh();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgv.Rows.Add();

                itemCode = item[dalItem.ItemMaterial].ToString();

                dgv.Rows[n].Cells[IndexColumnName].Value = index;
                dgv.Rows[n].Cells[dalItem.ItemCode].Value = itemCode;
                dgv.Rows[n].Cells[dalItem.ItemName].Value = dalItem.getMaterialName(itemCode);
                dgv.Rows[n].Cells[dalItem.ItemLastPMMAQty].Value = dalItem.getLastPMMAQty(itemCode);
                dgv.Rows[n].Cells[dalItem.ItemPMMAQty].Value = dalItem.getPMMAQty(itemCode);
                dgv.Rows[n].Cells[dalItem.ItemUpdateDate].Value = dalItem.getUpdatedDate(itemCode);
                dgv.Rows[n].Cells[dalItem.ItemUpdateBy].Value = dalUser.getUsername(dalItem.getUpdatedBy(itemCode));
                index++;
            }
            tool.listPaintGreyHeader(dgv);
        }

        private void frmPMMA_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.PMMAFormOpen = false;
        }

        #endregion

        #region selected/text changed

        private void cmbType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataGridView dgv = dgvPMMA;

            dgv.Columns.Clear();

            if(cmbType.Text.Equals(cmbTypeActual))
            {
                //get actual out record
            }
            else if (cmbType.Text.Equals(cmbTypeForecast))
            {
                //get forecast record
            }
        }

        #endregion

        #region click/key press

        private void frmPMMA_Click(object sender, EventArgs e)
        {
            dgvPMMA.ClearSelection();
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&  e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        
        private void dgvPMMA_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);

            if (dgvPMMA.CurrentCell.ColumnIndex == dgvPMMA.Columns[IndexPercentageName].Index ) //Desired Column
            {
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null) 
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        
        private void dgvPMMA_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var datagridview = sender as DataGridView;
            DataGridView dgv = dgvPMMA;
            DateTime currentDate = DateTime.Now;

            int rowIndex = e.RowIndex;

            float openningStock = Convert.ToSingle(dgv.Rows[rowIndex].Cells[dalPMMA.OpenStock].Value.ToString());
            float inQty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexInName].Value.ToString());
            float outQty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexOutName].Value.ToString());
            float bal = openningStock + inQty - outQty;
            float percentage = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexPercentageName].Value.ToString());
            float wastage = outQty * percentage;

            dgv.Rows[rowIndex].Cells[IndexBalName].Value = bal.ToString("0.000");
            dgv.Rows[rowIndex].Cells[IndexWastageName].Value = wastage.ToString("0.000");
            dgv.Rows[rowIndex].Cells[dalPMMA.BalStock].Value = (bal - wastage).ToString("0.000");

            uPMMA.pmma_item_code = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();
            uPMMA.pmma_date = Convert.ToDateTime(dtpDate.Text);
            uPMMA.pmma_openning_stock = openningStock;
            uPMMA.pmma_percentage = percentage;
            uPMMA.pmma_bal_stock = bal - wastage;
            uPMMA.pmma_updated_date = DateTime.Now;
            uPMMA.pmma_updated_by = MainDashboard.USER_ID;


            bool success = dalPMMA.update(uPMMA);

            if (!success)
            {
                MessageBox.Show("Failed to updated item pmma qty");
            }

        }

        #endregion

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string outType = cmbType.Text;
            if (outType.Equals(cmbTypeActual))
            {
                getMonthlyStockData();
            }
            else if (outType.Equals(cmbTypeForecast))
            {
                if (checkIfForecastExist() != -1)
                {
                    getMonthlyStockData();
                }
                else
                {
                    MessageBox.Show("Forecast data for this month not exist.");
                }
            }  
        }

        private int getNextMonth(int currentMonth)
        {
            int nextMonth = 0;
            if (currentMonth == 12)
            {
                nextMonth = 1;
            }
            else
            {
                nextMonth = currentMonth + 1;
            }
            return nextMonth;
        }

        private int checkIfForecastExist()
        {
            bool forecastExist = false;
            int forecastType = -1;
          
            int forecastCurrentMonth = DateTime.Parse("1." + getCurrentForecastMonth() + " 2008").Month;
            int selectedMonth = Convert.ToDateTime(dtpDate.Text).Month;
            int selectedYear = Convert.ToDateTime(dtpDate.Text).Year;

            int forecastNextMonth = getNextMonth(forecastCurrentMonth);
            int forecastNextNextMonth = getNextMonth(forecastNextMonth);

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            if (selectedMonth == forecastCurrentMonth || selectedMonth == forecastNextMonth || selectedMonth == forecastNextNextMonth)
            {
                //current year + 1
                if (selectedMonth >= 1 && selectedMonth <= 3 && currentMonth <= 12 && currentMonth >= 10)
                {
                    if ((currentYear + 1) == selectedYear)
                    {
                        forecastExist = true;
                    }

                }
                //current year - 1
                else if (selectedMonth >= 10 && selectedMonth <= 12 && currentMonth >= 1 && currentMonth <= 2)
                {
                    if ((currentYear - 1) == selectedYear)
                    {
                        forecastExist = true;
                    }
                }
                else if ((currentYear) == selectedYear)
                {
                    forecastExist = true;
                }
            }

            if(forecastExist)
            {
                if (selectedMonth == forecastCurrentMonth)
                {
                    forecastType = 1;
                }
                else if (selectedMonth == forecastNextMonth)
                {
                    forecastType = 2;
                }
                else if (selectedMonth == forecastNextNextMonth)
                {
                    forecastType = 3;
                }
            }

            return forecastType;
        }

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";
            DateTime currentDate = DateTime.Now;
            fileName = "EndMonthStockReport(" + tool.getCustName(1) + "_" + dtpDate.Text +")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
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
                xlWorkSheet.Name = tool.getCustName(1);

                #region Save data to Sheet
                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + tool.getCustName(1) + ") END MONTH STOCK REPORT("+dtpDate.Text+")";
                

                //Header and Footer setup
                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

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
                dgvPMMA.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void copyAlltoClipboard()
        {
            dgvPMMA.SelectAll();
            DataObject dataObj = dgvPMMA.GetClipboardContent();
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
