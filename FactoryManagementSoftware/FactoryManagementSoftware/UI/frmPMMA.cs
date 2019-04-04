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
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
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
        Text text = new Text();
        #endregion

        #region Variable
        readonly string cmbTypeActual = "Actual";
        readonly string cmbTypeForecast = "Forecast";
        readonly string IndexColumnName = "NO";
        readonly string IndexInName = "IN";
        readonly string IndexOutName = "OUT";
        readonly string IndexPercentageName = "%";
        readonly string IndexWastageName = "WASTAGE";
        readonly string IndexAdjustName = "ADJUST";
        readonly string IndexNoteName = "NOTE";
        readonly string IndexBalName = "BAL STOCK";
        private string editedOldValue;
        private string editedNewValue;
        private string editedHeaderText;
        private bool firstForecastChecked = false;
        private bool secondForecastChecked = false;
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
            tool.AddTextBoxColumns(dgv, IndexAdjustName, dalPMMA.Adjust, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexNoteName, dalPMMA.Note, DisplayedCells);
            tool.AddTextBoxColumns(dgv, IndexBalName, dalPMMA.BalStock, DisplayedCells);


            dgv.Columns[dalPMMA.OpenStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexInName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexOutName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexBalName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexPercentageName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[IndexWastageName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[dalPMMA.Adjust].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[dalPMMA.BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[IndexColumnName].ReadOnly = true;
            dgv.Columns[dalItem.ItemCode].ReadOnly = true;
            dgv.Columns[dalItem.ItemName].ReadOnly = true;

            dgv.Columns[dalPMMA.OpenStock].ReadOnly = true;
            dgv.Columns[IndexInName].ReadOnly = true;
            dgv.Columns[IndexOutName].ReadOnly = true;
            dgv.Columns[IndexBalName].ReadOnly = true;
            dgv.Columns[IndexWastageName].ReadOnly = true;
            dgv.Columns[dalPMMA.BalStock].ReadOnly = true;

            if (dalUser.getPermissionLevel(MainDashboard.USER_ID) >= 2)
            {
                dgv.Columns[IndexPercentageName].ReadOnly = false;
                dgv.Columns[dalPMMA.Adjust].ReadOnly = false;
                dgv.Columns[dalPMMA.Note].ReadOnly = false;
            }
            else
            {
                dgv.Columns[IndexPercentageName].ReadOnly = true;
                dgv.Columns[dalPMMA.Adjust].ReadOnly = true;
                dgv.Columns[dalPMMA.Note].ReadOnly = true;
            }
        }

        #endregion

        #region load data

        private void dataSourceSetup()
        {
            // Set the Format type and the CustomFormat string.
            //DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            //dtpDate.Value = date;
            
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
            //int typeNo = 0;
            dtMat.Columns.Add(dalItem.ItemMaterial);
            dtMat.Columns.Add("no");

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
                    //typeNo = 0;
                    itemCode = item["item_code"].ToString();
                    itemMat = item[dalItem.ItemMaterial].ToString();

                    if (tool.ifGotChild(itemCode))
                    {
                        DataTable dtJoin = dalJoin.parentCheck(itemCode);
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (tool.ifGotChild(Join["join_child_code"].ToString()))
                            {
                                DataTable dtJoin2 = dalJoin.parentCheck(Join["join_child_code"].ToString());
                                foreach (DataRow Join2 in dtJoin2.Rows)
                                {
                                    //typeNo = 0;
                                    if (dalItem.getCatName(Join2["join_child_code"].ToString()).Equals("Part"))
                                    {
                                        itemMat = dalItem.getMaterialType(Join2["join_child_code"].ToString());

                                        if (!string.IsNullOrEmpty(itemMat))
                                        {
                                            dtMat.Rows.Add(itemMat, "1");
                                        }
                                    }
                                    else if (dalItem.getCatName(Join2["join_child_code"].ToString()).Equals("Sub Material"))
                                    {
                                        itemMat = Join2["join_child_code"].ToString();

                                        if (!string.IsNullOrEmpty(itemMat))
                                        {
                                            dtMat.Rows.Add(itemMat, "2");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (dalItem.getCatName(Join["join_child_code"].ToString()).Equals("Part"))
                                {
                                    itemMat = dalItem.getMaterialType(Join["join_child_code"].ToString());

                                    if (!string.IsNullOrEmpty(itemMat))
                                    {
                                        dtMat.Rows.Add(itemMat, "1");
                                    }
                                }
                                else if (dalItem.getCatName(Join["join_child_code"].ToString()).Equals("Sub Material"))
                                {
                                    itemMat = Join["join_child_code"].ToString();

                                   
                                    if (!string.IsNullOrEmpty(itemMat))
                                    {
                                        dtMat.Rows.Add(itemMat, "2");
                                    }
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        //typeNo = 0;
                        if (!string.IsNullOrEmpty(itemMat))
                        {
                            dtMat.Rows.Add(itemMat, "1");
                        }
                    }
                }
            }
            
            return dtMat;
        }

        private DataTable RemoveNonZeroCost(DataTable dt)
        {
            materialDAL dalMat = new materialDAL();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (!dalMat.checkIfZeroCost(dt.Rows[i]["item_material"].ToString()))
                    {
                        dt.Rows[i].Delete();
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private DataTable RemoveSubMat(DataTable dt)
        {
            materialDAL dalMat = new materialDAL();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["item_cat"].ToString().Equals("Sub Material"))
                    {
                        dt.Rows[i].Delete();
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private DataTable RemoveEmpty(DataTable dt)
        {
            materialDAL dalMat = new materialDAL();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["item_cat"].ToString().Equals("Part") && string.IsNullOrEmpty(dt.Rows[i]["item_material"].ToString()))
                    {
                        dt.Rows[i].Delete();
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private DataTable SeperateMatAndSubMat(DataTable dt)
        {
            materialDAL dalMat = new materialDAL();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 2; i++)
                {
                    string a = dt.Rows[i]["no"].ToString();

                    if(a.Equals("2"))
                    {
                        for (int j = i + 1; j <= dt.Rows.Count - 1; j++)
                        {
                            string b = dt.Rows[j]["no"].ToString();
                            if (b.Equals("1"))
                            {
                                //swap row[i] and row[j]
                                break;
                            }
                        }
                    }
                }
                dt.AcceptChanges();
            }

            return dt;
        }


        private void getMonthlyStockData()
        {
            createColumnsForMonthlyReportDGV();
            dgvPMMA.Rows.Clear();
            dgvPMMA.Refresh();
            int index = 1;
            DataTable dt = insertItemMaterialData();//23S,2S
            dt = tool.RemoveDuplicates(dt, "item_material");
            dt = RemoveNonZeroCost(dt);

            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            dt.DefaultView.Sort = "no ASC";
            dt = dt.DefaultView.ToTable();

            dt.Columns.Remove("no");

            float openningStock = 0;
            float percentage = 0;
            float adjust = 0;
            string itemCode, month, year,note="";

            month = Convert.ToDateTime(dtpDate.Text).Month.ToString();
            year = Convert.ToDateTime(dtpDate.Text).Year.ToString();

            foreach (DataRow item in dt.Rows)
            {
                openningStock = 0;
                percentage = 0;
                adjust = 0;
                note = "";
                itemCode = item["item_material"].ToString();

                DataTable searchdt = dalPMMA.Search(itemCode, month, year);

                //get last month bal
                openningStock = getLastMonthBal(itemCode, month, year);
                
                foreach (DataRow pmma in searchdt.Rows)
                {
                    if (openningStock == -1)
                    {
                        openningStock = 0;
                        if (float.TryParse(pmma[dalPMMA.OpenStock].ToString(), out float i))
                        {
                            openningStock += Convert.ToSingle(pmma[dalPMMA.OpenStock]);
                        }
                    }

                    if (float.TryParse(pmma[dalPMMA.Percentage].ToString(), out float j))
                    {
                        percentage += Convert.ToSingle(pmma[dalPMMA.Percentage]);
                    }

                if (float.TryParse(pmma[dalPMMA.Adjust].ToString(), out float k))
                    {
                            adjust += Convert.ToSingle(pmma[dalPMMA.Adjust]);
                    }
                    note = pmma[dalPMMA.Note].ToString();
                }

                if (searchdt.Rows.Count <= 0)
                {
                    //insert new data to table pmma
                    percentage = getLastMonthPercentage(itemCode, month, year);
                    insertDataToPMMA(itemCode, Convert.ToDateTime(dtpDate.Text), openningStock);
                }
                
                loadDataToDGV(itemCode, openningStock, percentage, index, month, year,adjust,note);
                index++;
            }
        }

        private void loadDataToDGV(string itemCode, float openningStock, float percentage, int index, string month, string year, float adjust, string note)
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
            dgv.Rows[n].Cells[dalPMMA.OpenStock].Value = openningStock.ToString("0.00");
            dgv.Rows[n].Cells[IndexInName].Value = inQty.ToString("0.00");
            dgv.Rows[n].Cells[IndexOutName].Value = outQty.ToString("0.00");
            dgv.Rows[n].Cells[dalPMMA.Adjust].Value = adjust.ToString("0.00");
            dgv.Rows[n].Cells[dalPMMA.Note].Value = note;
            dgv.Rows[n].Cells[IndexBalName].Value = (bal+adjust).ToString("0.00");

            if (outQty == 0)
            {
                percentage = 0;
            }

            dgv.Rows[n].Cells[IndexPercentageName].Value = percentage;
            dgv.Rows[n].Cells[IndexWastageName].Value = wastage.ToString("0.00");
            dgv.Rows[n].Cells[dalPMMA.BalStock].Value = (bal - wastage + adjust).ToString("0.00");

            //update balance stock to table pmma
            updateDataToPMMA( itemCode, Convert.ToDateTime(dtpDate.Text), openningStock,percentage, bal - wastage, adjust, note);
        }

        private float calculateInRecord(string itemCode, string month, string year)
        {
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");

            DataTable dt = dalTrfHist.rangeTrfSearch(itemCode, start, end);
            float inQty = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                    {
                        string trfFrom = item[dalTrfHist.TrfFrom].ToString();
                        string trfTo = item[dalTrfHist.TrfTo].ToString();

                        //from PMMA to factory: IN
                        if (tool.getCustName(1) == trfFrom && tool.getFactoryID(trfTo) != -1)
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
            //string materialType = null;
            int OrderQty;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;
            DataTable dt;
            string test = dalItem.getCatName(itemCode);
            if (dalItem.getCatName(itemCode).Equals("RAW Material"))
            {
                dt = dalMatUsed.matSearch(itemCode);
            }
            else
            {
                dt = dalMatUsed.codeSearch(itemCode);
            }

            dt = AddDuplicates(dt);
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow item in dt.Rows)
            {
                itemWeight = Convert.ToSingle(item[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(item[dalItem.ItemQuoRWPcs].ToString());

                if (itemWeight <= 0)
                {
                    itemWeight = Convert.ToSingle(item[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(item[dalItem.ItemProRWPcs].ToString());
                }

                OrderQty = Convert.ToInt32(item["quantity_order"].ToString());
                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                if (item[dalItem.ItemCat].ToString().Equals("Part"))
                {
                    materialUsed = OrderQty * itemWeight / 1000;
                    wastageUsed = materialUsed * wastagePercetage;
                    totalMaterialUsed += materialUsed + wastageUsed;
                }
                else
                {
                    materialUsed = OrderQty;
                    wastageUsed = materialUsed * wastagePercetage;
                    totalMaterialUsed = materialUsed + wastageUsed;
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
            else if (type == 3)
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

            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");

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

                        DataTable dt3 = dalTrfHist.rangeItemToCustomerSearch(custName, start, end, itemCode);

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
                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            if (dalItem.checkIfProduction(itemCode))
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = itemCode;
                                uMatUsed.quantity_order = outStock;

                                result = dalMatUsed.Insert(uMatUsed);
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

                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                if (tool.ifGotChild(Join["join_child_code"].ToString()))
                                {
                                    if (dalItem.checkIfProduction(Join["join_child_code"].ToString()))
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join["join_child_code"].ToString();
                                        uMatUsed.quantity_order = outStock;

                                        result = dalMatUsed.Insert(uMatUsed);
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

                                    DataTable dtJoin2 = dalJoin.parentCheck(Join["join_child_code"].ToString());
                                    foreach (DataRow Join2 in dtJoin2.Rows)
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join2["join_child_code"].ToString();
                                        uMatUsed.quantity_order = outStock;

                                        result = dalMatUsed.Insert(uMatUsed);
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
                                    uMatUsed.item_code = Join["join_child_code"].ToString();
                                    uMatUsed.quantity_order = outStock;

                                    result = dalMatUsed.Insert(uMatUsed);
                                    if (!result)
                                    {
                                        MessageBox.Show("failed to insert material used data" + itemCode + Join["join_child_code"].ToString());
                                        return;
                                    }
                                    else
                                    {
                                        forecastIndex++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = outStock;

                            result = dalMatUsed.Insert(uMatUsed);
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
            uPMMA.pmma_adjust = 0;
            uPMMA.pmma_note = "";
            uPMMA.pmma_added_date = DateTime.Now;
            uPMMA.pmma_added_by = MainDashboard.USER_ID;

            bool success = dalPMMA.insert(uPMMA);

            if(!success)
            {
                MessageBox.Show("Failed to insert data to PMMA");
            }
        }

        private void updateDataToPMMA(string itemCode, DateTime date, float openingStock, float percentage, float balStock, float adjust, string note)
        {
            uPMMA.pmma_item_code = itemCode;
            uPMMA.pmma_date = date;
            uPMMA.pmma_openning_stock = openingStock;
            uPMMA.pmma_percentage = percentage;
            uPMMA.pmma_adjust = adjust;
            uPMMA.pmma_note = note;
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&  e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
        
        private void dgvPMMA_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                
                if (dgvPMMA.CurrentCell.ColumnIndex == dgvPMMA.Columns[IndexPercentageName].Index || dgvPMMA.CurrentCell.ColumnIndex == dgvPMMA.Columns[dalPMMA.Adjust].Index) //Desired Column
                {
                    System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            
        }

        private void dgvPMMA_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                var oldValue = dgvPMMA[e.ColumnIndex, e.RowIndex].Value;
                var newValue = e.FormattedValue;
                int columnIndex = dgvPMMA.CurrentCell.ColumnIndex;
                string columnName = dgvPMMA.Columns[columnIndex].HeaderText;

                if (!oldValue.ToString().Equals(newValue.ToString()))
                {
                    editedOldValue = oldValue.ToString();
                    editedNewValue = newValue.ToString();
                    editedHeaderText = columnName;
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvPMMA_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var datagridview = sender as DataGridView;
            DataGridView dgv = dgvPMMA;
            DateTime currentDate = DateTime.Now;

            int rowIndex = e.RowIndex;
            try
            {
                float openningStock = Convert.ToSingle(dgv.Rows[rowIndex].Cells[dalPMMA.OpenStock].Value.ToString());
                float inQty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexInName].Value.ToString());
                float outQty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexOutName].Value.ToString());
                float bal = openningStock + inQty - outQty;
                float percentage = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexPercentageName].Value.ToString());
                float wastage = outQty * percentage;
                string itemCode = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();
                float adjust = 0;
                DateTime date = Convert.ToDateTime(dtpDate.Text).Date;
                if (dgv.Rows[rowIndex].Cells[dalPMMA.Adjust].Value != null)
                {
                    adjust = Convert.ToSingle(dgv.Rows[rowIndex].Cells[dalPMMA.Adjust].Value.ToString());
                }

                string note = "";
                if (dgv.Rows[rowIndex].Cells[dalPMMA.Note].Value != null)
                {
                    note = dgv.Rows[rowIndex].Cells[dalPMMA.Note].Value.ToString();
                }


                dgv.Rows[rowIndex].Cells[IndexBalName].Value = bal.ToString("0.00");
                dgv.Rows[rowIndex].Cells[IndexWastageName].Value = wastage.ToString("0.00");
                dgv.Rows[rowIndex].Cells[dalPMMA.BalStock].Value = (bal - wastage + adjust).ToString("0.00");


                uPMMA.pmma_item_code = itemCode;
                uPMMA.pmma_date = date;
                uPMMA.pmma_openning_stock = openningStock;
                uPMMA.pmma_percentage = percentage;
                uPMMA.pmma_adjust = adjust;
                uPMMA.pmma_note = note;
                uPMMA.pmma_bal_stock = bal - wastage + adjust;
                uPMMA.pmma_updated_date = DateTime.Now;
                uPMMA.pmma_updated_by = MainDashboard.USER_ID;


                bool success = dalPMMA.update(uPMMA);

                if (!success)
                {
                    MessageBox.Show("Failed to updated PMMA item");
                    tool.historyRecord(text.System, "Failed to updated PMMA item(frmPMMA)", DateTime.Now, MainDashboard.USER_ID);
                }
                else
                {
                    tool.historyRecord(text.PMMAEdit, text.getPMMAEditString(itemCode, date.ToString("MMMMyy"), editedHeaderText, editedOldValue, editedNewValue), DateTime.Now, MainDashboard.USER_ID);
                }

            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        #endregion

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                string outType = cmbType.Text;
                if (outType.Equals(cmbTypeActual))
                {
                    getMonthlyStockData();//143S
                    firstForecastChecked = false;
                    secondForecastChecked = false;
                }
                else if (outType.Equals(cmbTypeForecast))
                {
                    int n = checkIfForecastExist();

                    if (n == 1)
                    {
                        getMonthlyStockData(); 
                        firstForecastChecked = true;
                    }
                    else if (n == 2)
                    {
                        if (firstForecastChecked)
                        {
                            getMonthlyStockData();
                            secondForecastChecked = true;
                        }
                        else
                        {
                            MessageBox.Show("Please check the data of the forecast 1 month before checking the data of the forecast 2 month.");
                        }
                    }
                    else if (n == 3)
                    {
                        if (secondForecastChecked)
                        {
                            getMonthlyStockData();
                            secondForecastChecked = true;
                        }
                        else
                        {
                            MessageBox.Show("Please check the data of the forecast 2 month before checking the data of the forecast 3 month.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Forecast data for this month not exist.");
                    }
                }

                foreach (DataGridViewRow row in dgvPMMA.Rows)
                {

                    float openningStock = 0;

                    if (row.Cells[dalPMMA.OpenStock].Value != null)
                    {
                        openningStock = Convert.ToSingle(row.Cells[dalPMMA.OpenStock].Value.ToString());
                    }

                    float inQty = 0;
                    float outQty = 0;

                    if (row.Cells[IndexInName].Value != null)
                    {
                        inQty = Convert.ToSingle(row.Cells[IndexInName].Value.ToString());
                    }

                    if (row.Cells[IndexOutName].Value != null)
                    {
                        outQty = Convert.ToSingle(row.Cells[IndexOutName].Value.ToString());
                    }

                    float bal = openningStock + inQty - outQty;

                    float percentage = 0;
                    float adjust = 0;

                    if (row.Cells[IndexPercentageName].Value != null)
                    {
                        percentage = Convert.ToSingle(row.Cells[IndexPercentageName].Value.ToString());
                    }

                    float wastage = outQty * percentage;

                    if (row.Cells[dalPMMA.Adjust].Value != null)
                    {
                        adjust = Convert.ToSingle(row.Cells[dalPMMA.Adjust].Value.ToString());
                    }

                    string note = "";
                    if (row.Cells[dalPMMA.Note].Value != null)
                    {
                        note = row.Cells[dalPMMA.Note].Value.ToString();
                    }

                    uPMMA.pmma_item_code = row.Cells[dalItem.ItemCode].Value.ToString();
                    uPMMA.pmma_date = Convert.ToDateTime(dtpDate.Text);
                    uPMMA.pmma_openning_stock = openningStock;
                    uPMMA.pmma_percentage = percentage;
                    uPMMA.pmma_adjust = adjust;
                    uPMMA.pmma_note = note;
                    uPMMA.pmma_bal_stock = bal - wastage + adjust;
                    uPMMA.pmma_updated_date = DateTime.Now;
                    uPMMA.pmma_updated_by = MainDashboard.USER_ID;

                    bool success = dalPMMA.update(uPMMA);

                    if (!success)
                    {
                        MessageBox.Show("Failed to updated item pmma qty");
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type 
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
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

        private bool checkIfActualExist()
        {
            bool actualExist = false;
            DateTime selectedDate = Convert.ToDateTime(dtpDate.Text);
            //DateTime selectedDate = Convert.ToDateTime(dtpDate.Text,System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

            int CurrentMonth = DateTime.Now.Month;
            int CurrentYear = DateTime.Now.Year;
            int selectedMonth = selectedDate.Month;
            int selectedYear = selectedDate.Year;

            if(selectedYear > CurrentYear)
            {
                actualExist = false;
            }
            else if(selectedYear == CurrentYear)
            {
                if(CurrentMonth == 12)
                {
                    actualExist = true;
                }
                else if(selectedMonth > CurrentMonth)
                {
                    actualExist = false;
                }
                else
                {
                    actualExist = true;
                }
            }
            else
            {
                actualExist = true;
            }
            
            return actualExist;
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
            try
            {
                dgvPMMA.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();
                string path = @"D:\StockAssistant\Document\PMMAMaterialUsedReport";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(setFileName()), DateTime.Now, MainDashboard.USER_ID);
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
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + tool.getCustName(1) + ") END MONTH STOCK REPORT(" + dtpDate.Text + ")";


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
                    dgvPMMA.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                dgvPMMA.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
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

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dgvPMMA.Rows.Clear();

            if(checkIfActualExist())//checkIfActualExist()
            {
                cmbType.Items.Clear();
                cmbType.Items.Add(cmbTypeActual);
                cmbType.Items.Add(cmbTypeForecast);
                cmbType.SelectedIndex = 0;
            }
            else
            {
                cmbType.Items.Clear();
                cmbType.Items.Add(cmbTypeForecast);
                cmbType.SelectedIndex = 0;
            }
        }

        private void cmbType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dgvPMMA.Rows.Clear();

            if(cmbType.Text.Equals(cmbTypeActual))
            {
                lblStart.Text = "IN/OUT START";
                lblEnd.Text = "IN/OUT END";
                //lblStart.Show();
                //lblEnd.Show();
                //dtpStart.Show();
                //dtpEnd.Show();
            }
            else
            {
                lblStart.Text = "IN START";
                lblEnd.Text = "IN END";
                //lblStart.Hide();
                //lblEnd.Hide();
                //dtpStart.Hide();
                //dtpEnd.Hide();
            }
        }

        private void frmPMMA_Load(object sender, EventArgs e)
        {
            tool.DoubleBuffered(dgvPMMA, true);
        }
    }



}


