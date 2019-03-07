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
using System.Globalization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderAlertDetail : Form
    {
        itemDAL dalItem = new itemDAL();
        materialUsedDAL dalMatUsed = new materialUsedDAL();
        materialUsedBLL uMatUsed = new materialUsedBLL();
        Tool tool = new Tool();
        itemCustDAL dalItemCust = new itemCustDAL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();

        readonly string indexItemCode = "Code";
        readonly string indexitemName = "Name";
        readonly string indexReadyStock = "Ready Stock";
        readonly string indexForecast = "Forecast";
        readonly string indexStillNeed = "Still Need";
        readonly string indexOut = "Out";
        readonly string indexWeight = "Item Weight (Grams)";
        readonly string indexMaterialUsed = "Material Used (Kg)";
        readonly string indexWastageAllow = "Wastage Allowed (%)";
        readonly string indexMaterialUsedIncludeWastage = "Material Used (Wastage Included)";
        readonly string indexTotalMaterialUsed = "Total Material Used (Kg)";
        private string forecastCurrentMonth;
        private string forecastNextMonth;
        private string forecastNextNextMonth;
        private string MaterialCode;

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;
        DataGridViewAutoSizeColumnMode DisplayedCellsExceptHeader = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        public frmOrderAlertDetail(string itemCode)
        {
            MaterialCode = itemCode;
            InitializeComponent();
            addDataToForecastCMB();
            createDGV();
        }

        #region UI setting

        private void createDGV()
        {
            DataGridView dgv = dgvMaterialUsedForecast;

            dgv.Columns.Clear();

            tool.AddTextBoxColumns(dgv, indexItemCode, indexItemCode, Fill);
            tool.AddTextBoxColumns(dgv, indexitemName, indexitemName, Fill);
            tool.AddTextBoxColumns(dgv, indexReadyStock, indexReadyStock, DisplayedCells);
            tool.AddTextBoxColumns(dgv, indexForecast, indexForecast, DisplayedCells);
            tool.AddTextBoxColumns(dgv, indexOut, indexOut, DisplayedCells);
            tool.AddTextBoxColumns(dgv, indexStillNeed, indexStillNeed, DisplayedCells);
            tool.AddTextBoxColumns(dgv, indexWeight, indexWeight, DisplayedCells);
            tool.AddTextBoxColumns(dgv, indexMaterialUsed, indexMaterialUsed, DisplayedCellsExceptHeader);
            tool.AddTextBoxColumns(dgv, indexWastageAllow, indexWastageAllow, DisplayedCellsExceptHeader);
            tool.AddTextBoxColumns(dgv, indexMaterialUsedIncludeWastage, indexMaterialUsedIncludeWastage, DisplayedCellsExceptHeader);
            tool.AddTextBoxColumns(dgv, indexTotalMaterialUsed, indexTotalMaterialUsed, DisplayedCells);

            dgv.Columns[indexReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexForecast].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexOut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexStillNeed].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexWeight].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexMaterialUsed].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexWastageAllow].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexMaterialUsedIncludeWastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[indexTotalMaterialUsed].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void addDataToForecastCMB()
        {
            forecastCurrentMonth = getForecastMonth(1);
            forecastNextMonth = getForecastMonth(2);
            forecastNextNextMonth = getForecastMonth(3);

            cmbForecast.Items.Clear();
            cmbForecast.Items.Add(forecastCurrentMonth);
            cmbForecast.Items.Add(forecastNextMonth);
            cmbForecast.Items.Add(forecastNextNextMonth);
            cmbForecast.SelectedIndex = 0;
        }

        #endregion

        private DataTable AddDuplicates(DataTable dt)
        {
            float jQty,iQty;
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
                            jQty = Convert.ToSingle(dt.Rows[j]["quantity_order"].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i]["quantity_order"].ToString());
                            dt.Rows[j]["quantity_order"] = (jQty  +   iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j]["quantity_order_two"].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i]["quantity_order_two"].ToString());
                            dt.Rows[j]["quantity_order_two"] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j]["quantity_order_three"].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i]["quantity_order_three"].ToString());
                            dt.Rows[j]["quantity_order_three"] = (jQty + iQty).ToString();

                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        private DataTable removeNonSelectedMaterial(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {                  
                    string materialType = dt.Rows[i]["item_material"].ToString();
                    if (materialType != MaterialCode && !string.IsNullOrEmpty(materialType))
                    {
                        dt.Rows[i].Delete();
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private DataTable removeDuplicates(DataTable dt)
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

        private string getForecastMonth(int type)
        {
            string monthName = "";

            int forecastCurrentMonthNum = DateTime.Parse("1." + getCurrentForecastMonth() + " 2008").Month;
            monthName = new DateTimeFormatInfo().GetMonthName(forecastCurrentMonthNum).ToUpper().ToString();

            if (type == 2)
            {
                int forecastNextMonthNum = getNextMonth(forecastCurrentMonthNum);
                monthName = new DateTimeFormatInfo().GetMonthName(forecastNextMonthNum).ToUpper().ToString();
            }
            else if (type == 3)
            {
                int forecastNextMonthNum = getNextMonth(forecastCurrentMonthNum);
                int forecastNextNextMonthNum = getNextMonth(forecastNextMonthNum);
                monthName = new DateTimeFormatInfo().GetMonthName(forecastNextNextMonthNum).ToUpper().ToString();
            }
            return monthName;
        }

        private void insertAllItemForecastData()
        {
            dalMatUsed.Delete();

            DataTable dt = dalItemCust.Select();//load all customer's item list

            dt = removeDuplicates(dt);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float Bal1Forecast, Bal2Forecast, Bal3Forecast, readyStock;
                string itemCode;
                int forecastIndex = 1;

                foreach (DataRow item in dt.Rows)
                {
                    itemCode = item["item_code"].ToString();
                    readyStock = dalItem.getStockQty(itemCode);
                    Bal1Forecast = 0;
                    Bal2Forecast = 0;
                    Bal3Forecast = 0;

                    string currentMonth = DateTime.ParseExact(item["forecast_current_month"].ToString(), "MMMM", CultureInfo.CurrentCulture).Month.ToString();
                    string year = DateTime.Now.Year.ToString();
                    float outStock = 0;


                    DataTable dt2 = dalTrfHist.rangeItemToAllCustomerSearchByMonth(currentMonth, year, itemCode);

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt2.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed"))
                            {
                                outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    DataTable dt3 = dalItemCust.itemCodeSearch(itemCode);
                    if (dt3.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt3.Rows)
                        {
                            Bal1Forecast += Convert.ToInt32(outRecord["forecast_one"]);
                            Bal2Forecast += Convert.ToInt32(outRecord["forecast_two"]);
                            Bal3Forecast += Convert.ToInt32(outRecord["forecast_three"]);
                        }
                    }
                    if (tool.ifGotChild(itemCode))
                    {
                        //calculate still need how many qty

                        if (outStock >= Bal1Forecast)
                        {
                            Bal1Forecast = readyStock;
                        }
                        else
                        {
                            Bal1Forecast = readyStock - Bal1Forecast + outStock;
                        }

                        Bal2Forecast = Bal1Forecast - Bal2Forecast;
                        Bal3Forecast = Bal2Forecast - Bal3Forecast;

                        DataTable dtJoin = dalJoin.parentCheck(itemCode);//get children list
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = Join["join_child_code"].ToString();
                            uMatUsed.quantity_order = Convert.ToInt32(Bal1Forecast);
                            uMatUsed.quantity_order_two = Convert.ToInt32(Bal2Forecast);
                            uMatUsed.quantity_order_three = Convert.ToInt32(Bal3Forecast);
                            uMatUsed.item_out = outStock;

                            bool resultChild = dalMatUsed.Insert(uMatUsed);
                            if (!resultChild)
                            {
                                MessageBox.Show("failed to insert child forecast data for order alert detail.");
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
                        //save to database
                        uMatUsed.no = forecastIndex;
                        uMatUsed.item_code = itemCode;
                        uMatUsed.quantity_order = Convert.ToInt32(Bal1Forecast);
                        uMatUsed.quantity_order_two = Convert.ToInt32(Bal2Forecast);
                        uMatUsed.quantity_order_three = Convert.ToInt32(Bal3Forecast);
                        uMatUsed.item_out = outStock;

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

        private void loadMaterialUsedList()
        {
            int index = 1;
            string materialType = null;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;
            float readyStock = 0;
            float stillNeed1, stillNeed2, stillNeed3;
            float shot2, shot3;
            float forecast1, forecast2, forecast3;
            float childForecast1, childForecast2, childForecast3;
            float childShot1, childShot2, childShot3;
            float outStock = 0;
            float readyStockShow = 0, forecastShow = 0, stillNeedShow = 0;
            int n = -1;
            int type = 1;

            if(cmbForecast.Text.Equals(forecastNextMonth))
            {
                type = 2;
            }
            else if (cmbForecast.Text.Equals(forecastNextNextMonth))
            {
                type = 3;
            }
       
            DataTable dt = dalMatUsed.Select();
            dt = AddDuplicates(dt);
            dt = removeNonSelectedMaterial(dt);

            dgvMaterialUsedForecast.Rows.Clear();
        
            foreach (DataRow item in dt.Rows)
            {
                string itemCode = item["item_code"].ToString();
                materialType = item["item_material"].ToString();
                forecast1 = Convert.ToSingle(item["quantity_order"].ToString());
                forecast2 = Convert.ToSingle(item["quantity_order_two"].ToString());
                forecast3 = Convert.ToSingle(item["quantity_order_three"].ToString());
                outStock = Convert.ToSingle(item["item_out"].ToString());

                if (string.IsNullOrEmpty(materialType))//parent part
                {
                    //n = dgvMaterialUsedForecast.Rows.Add();
                    //readyStock = dalItem.getStockQty(item[dalItem.ItemCode].ToString());

                    //if (type == 1)
                    //{
                    //    forecast = readyStock - stillNeed;
                    //}
                    //else if (type == 2)
                    //{
                    //    forecast = Convert.ToSingle(item["quantity_order"].ToString()) - stillNeed;
                    //    readyStock = Convert.ToSingle(item["quantity_order"].ToString());
                    //}
                    //else
                    //{
                    //    forecast = Convert.ToSingle(item["quantity_order_two"].ToString()) - stillNeed;
                    //    readyStock = Convert.ToSingle(item["quantity_order_two"].ToString());
                    //}

                    //if(stillNeed > 0)
                    //{
                    //    stillNeed = 0;
                    //}
                    //else
                    //{
                    //    stillNeed *= -1;
                    //}

                    //dgvMaterialUsedForecast.Rows[n].Cells[indexitemName].Value = item[dalItem.ItemName].ToString();
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexItemCode].Value = item[dalItem.ItemCode].ToString();
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexReadyStock].Value = readyStock;
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexForecast].Value = forecast;
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexStillNeed].Value = stillNeed;
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexWeight].Value = 0;

                    //dgvMaterialUsedForecast.Rows[n].Cells[indexWastageAllow].Value = 0;
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsed].Value = 0;
                    //dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsedIncludeWastage].Value = 0;
                    //index++;
                }

                else
                {
                    n = dgvMaterialUsedForecast.Rows.Add();
                    readyStock = dalItem.getStockQty(item[dalItem.ItemCode].ToString());
                    itemWeight = Convert.ToSingle(item["item_part_weight"].ToString());
                    wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    if (dalItemCust.checkIfExistinItemCustTable(item[dalItem.ItemCode].ToString()))//non child part
                    {

                        if(outStock >= forecast1)
                        {
                            stillNeed1 = 0;
                            shot2 = readyStock;
                        }
                        else
                        {
                            stillNeed1 = readyStock - forecast1 + outStock;
                            shot2 = stillNeed1;
                        }

                        stillNeed2 = shot2 - forecast2;
                        shot3 = stillNeed2;
                        stillNeed3 = shot3 - forecast3;

                        stillNeed1 = tool.stillNeedCheck(stillNeed1);
                        stillNeed2 = tool.stillNeedCheck(stillNeed2);
                        stillNeed3 = tool.stillNeedCheck(stillNeed3);

                        if (type == 1)
                        {
                            readyStockShow = readyStock;
                            forecastShow = forecast1;
                            stillNeedShow = stillNeed1;
                        }
                        else if (type == 2)
                        {
                            readyStockShow = shot2;
                            forecastShow = forecast2;
                            stillNeedShow = stillNeed2;
                            outStock = 0;
                        }
                        else
                        {
                            readyStockShow = shot3;
                            forecastShow = forecast3;
                            stillNeedShow = stillNeed3;
                            outStock = 0;
                        }

                    }
                    else//child part
                    {
                        if(Convert.ToSingle(item["quantity_order"].ToString()) >= 0)
                        {
                            childForecast1 = 0;
                        }
                        else
                        {
                            childForecast1 = Convert.ToSingle(item["quantity_order"].ToString());
                        }

                        childForecast1 = Math.Abs(childForecast1);
                        childShot1 = readyStock - childForecast1;

                        if (Convert.ToSingle(item["quantity_order_two"].ToString()) >= 0)
                        {
                            childForecast2 = 0;
                        }
                        else
                        {
                            if(Convert.ToSingle(item["quantity_order"].ToString()) < 0)
                            {
                                childForecast2 = Convert.ToSingle(item["quantity_order"].ToString()) - Convert.ToSingle(item["quantity_order_two"].ToString());
                            }
                            else
                            {
                                childForecast2 = Convert.ToSingle(item["quantity_order_two"].ToString());
                            }
                        }

                        childForecast2 = Math.Abs(childForecast2);
                        childShot2 = childShot1 - childForecast2;

                        if (Convert.ToSingle(item["quantity_order_three"].ToString()) >= 0)
                        {
                            childForecast3 = 0;
                        }
                        else
                        {
                            if (Convert.ToSingle(item["quantity_order_two"].ToString()) < 0)
                            {
                                childForecast3 = Convert.ToSingle(item["quantity_order_two"].ToString()) - Convert.ToSingle(item["quantity_order_three"].ToString());
                            }
                            else
                            {
                                childForecast3 = Convert.ToSingle(item["quantity_order_three"].ToString());
                            }
                        }

                        childForecast3 = Math.Abs(childForecast3);
                        childShot3 = childShot2 - childForecast3;

                        if (type == 1)
                        {
                            forecastShow = childForecast1;
                            stillNeedShow = childShot1;
                            readyStockShow = readyStock;
                        }
                        else if(type == 2)
                        {
                            readyStockShow = childShot1;
                            forecastShow = childForecast2;
                            stillNeedShow = childShot2;
                            outStock = 0;
                        }
                        else
                        {
                            readyStockShow = childShot2;
                            forecastShow = childForecast3;
                            stillNeedShow = childShot3;
                            outStock = 0;
                        }
                        stillNeedShow = tool.stillNeedCheck(stillNeedShow);
                    }

                    //stillNeedShow -= outStock;
                    materialUsed = stillNeedShow * itemWeight / 1000;
                    wastageUsed = materialUsed * wastagePercetage;
                    totalMaterialUsed += materialUsed + wastageUsed;

                    dgvMaterialUsedForecast.Rows[n].Cells[indexitemName].Value = item[dalItem.ItemName].ToString();
                    dgvMaterialUsedForecast.Rows[n].Cells[indexItemCode].Value = item[dalItem.ItemCode].ToString();
                    dgvMaterialUsedForecast.Rows[n].Cells[indexReadyStock].Value = readyStockShow;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexForecast].Value = forecastShow;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexOut].Value = outStock;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexStillNeed].Value = stillNeedShow;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexWeight].Value = itemWeight;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexWastageAllow].Value = item[dalItem.ItemWastage].ToString();
                    dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsed].Value = (materialUsed).ToString("0.00");
                    dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsedIncludeWastage].Value = (materialUsed + wastageUsed).ToString("0.00");
                    index++;
                }
            }

            if(n >= 0)
            {
                dgvMaterialUsedForecast.Rows[n].Cells[indexTotalMaterialUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedForecast.Font, FontStyle.Bold);
                dgvMaterialUsedForecast.Rows[n].Cells[indexTotalMaterialUsed].Value = totalMaterialUsed.ToString("0.00");
            }            
            tool.listPaintGreyHeader(dgvMaterialUsedForecast);
        }

        private void cmbForecast_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblDGV.Text = cmbForecast.Text + " MATERIAL USED FORECAST";
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            insertAllItemForecastData();
            loadMaterialUsedList();
        }

        #region backup
        private void loadMaterialUsedList2()
        {
            int index = 1;
            string materialType = null;
            int OrderQty;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;
            //float readyStock = 0;
            //float stillNeed = 0;

            DataTable dt = dalMatUsed.Select();
            dt = AddDuplicates(dt);
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            dgvMaterialUsedForecast.Rows.Clear();
            dgvMaterialUsedForecast.Refresh();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvMaterialUsedForecast.Rows.Add();

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
                    //first data

                    dgvMaterialUsedForecast.Rows[n - 1].Cells[indexTotalMaterialUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedForecast.Font, FontStyle.Bold);
                    dgvMaterialUsedForecast.Rows[n - 1].Cells[indexTotalMaterialUsed].Value = totalMaterialUsed;

                    materialType = item["item_material"].ToString();
                    totalMaterialUsed = materialUsed + wastageUsed;
                    n = dgvMaterialUsedForecast.Rows.Add();
                }

                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    totalMaterialUsed = materialUsed + wastageUsed;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexTotalMaterialUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedForecast.Font, FontStyle.Bold);
                    dgvMaterialUsedForecast.Rows[n].Cells[indexTotalMaterialUsed].Value = totalMaterialUsed;
                }

                dgvMaterialUsedForecast.Rows[n].Cells[indexitemName].Value = item[dalItem.ItemName].ToString();
                dgvMaterialUsedForecast.Rows[n].Cells[indexItemCode].Value = item[dalItem.ItemCode].ToString();

                dgvMaterialUsedForecast.Rows[n].Cells[indexForecast].Value = item["quantity_order"].ToString();

                dgvMaterialUsedForecast.Rows[n].Cells[indexWeight].Value = itemWeight;

                dgvMaterialUsedForecast.Rows[n].Cells[indexWastageAllow].Value = item[dalItem.ItemWastage].ToString();
                dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsed].Value = materialUsed;
                dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsedIncludeWastage].Value = materialUsed + wastageUsed;
                index++;
            }
            tool.listPaintGreyHeader(dgvMaterialUsedForecast);
        }

        private void loadMaterialUsedList3()
        {
            int index = 1;
            string materialType = null;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;
            float readyStock = 0;
            float stillNeed = 0;
            float forecast = 0;
            int n = -1;

            DataTable dt = dalMatUsed.Select();
            //dt = AddDuplicates(dt);
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            dgvMaterialUsedForecast.Rows.Clear();
            dgvMaterialUsedForecast.Refresh();
            foreach (DataRow item in dt.Rows)
            {
                materialType = item["item_material"].ToString();
                if (materialType == MaterialCode)
                {
                    n = dgvMaterialUsedForecast.Rows.Add();
                    itemWeight = Convert.ToSingle(item["item_part_weight"].ToString());

                    readyStock = Convert.ToSingle(item["quantity_order"].ToString());
                    forecast = Convert.ToSingle(item["quantity_order_two"].ToString());
                    //stillNeed = Convert.ToSingle(item["quantity_order_three"].ToString());

                    wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    if (forecast < 0)
                    {
                        forecast *= -1;
                    }

                    stillNeed = readyStock - forecast;

                    if (stillNeed >= 0)
                    {
                        stillNeed = 0;
                    }
                    else
                    {
                        stillNeed *= -1;
                    }

                    materialUsed = stillNeed * itemWeight / 1000;
                    wastageUsed = materialUsed * wastagePercetage;
                    totalMaterialUsed += materialUsed + wastageUsed;

                    dgvMaterialUsedForecast.Rows[n].Cells[indexitemName].Value = item[dalItem.ItemName].ToString();
                    dgvMaterialUsedForecast.Rows[n].Cells[indexItemCode].Value = item[dalItem.ItemCode].ToString();
                    dgvMaterialUsedForecast.Rows[n].Cells[indexReadyStock].Value = readyStock;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexForecast].Value = forecast;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexStillNeed].Value = stillNeed;
                    dgvMaterialUsedForecast.Rows[n].Cells[indexWeight].Value = itemWeight;

                    dgvMaterialUsedForecast.Rows[n].Cells[indexWastageAllow].Value = item[dalItem.ItemWastage].ToString();
                    dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsed].Value = (materialUsed).ToString("0.00");
                    dgvMaterialUsedForecast.Rows[n].Cells[indexMaterialUsedIncludeWastage].Value = (materialUsed + wastageUsed).ToString("0.00");
                    index++;
                }
            }


            if (n > 0)
            {
                dgvMaterialUsedForecast.Rows[n].Cells[indexTotalMaterialUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedForecast.Font, FontStyle.Bold);
                dgvMaterialUsedForecast.Rows[n].Cells[indexTotalMaterialUsed].Value = totalMaterialUsed.ToString("0.00");
            }


            tool.listPaintGreyHeader(dgvMaterialUsedForecast);
        }

        private void insertAllItemForecastData2()
        {
            dalMatUsed.Delete();
            int type = 1;

            if (cmbForecast.Text.Equals(forecastNextMonth))
            {
                type = 2;
            }
            else if (cmbForecast.Text.Equals(forecastNextNextMonth))
            {
                type = 3;
            }

            DataTable dt = dalItemCust.Select();
            dt = dt.DefaultView.ToTable();

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float Forecast1Num = 0;
                float Forecast2Num = 0;
                float Forecast3Num = 0;
                float shot1 = 0;
                float shot2 = 0;
                float shot3 = 0;
                float shotSave = 0;
                float forecastSave = 0;
                float balSave = 0;

                string itemCode;
                float readyStock = 0;
                // string forecastNO = "";
                int forecastIndex = 1;

                foreach (DataRow item in dt.Rows)
                {

                    itemCode = item["item_code"].ToString();
                    readyStock = dalItem.getStockQty(itemCode);

                    DataTable dt3 = dalItemCust.itemCodeSearch(itemCode);

                    Forecast1Num = 0;
                    Forecast2Num = 0;
                    Forecast3Num = 0;

                    if (dt3.Rows.Count > 0)
                    {

                        foreach (DataRow outRecord in dt3.Rows)
                        {
                            Forecast1Num += Convert.ToInt32(outRecord["forecast_one"]);
                            Forecast2Num += Convert.ToInt32(outRecord["forecast_two"]);
                            Forecast3Num += Convert.ToInt32(outRecord["forecast_three"]);
                        }
                    }

                    shot1 = readyStock - Forecast1Num;
                    shot2 = shot1 - Forecast2Num;
                    shot3 = shot3 - Forecast3Num;

                    if (tool.ifGotChild(itemCode))
                    {
                        //show parent
                        //check if got child when foreach item in matused table list
                        DataTable dtJoin = dalJoin.parentCheck(itemCode);
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            float childReadyStock = dalItem.getStockQty(Join["join_child_code"].ToString());
                            float childForecast1 = 0;
                            float childForecast2 = 0;
                            float childForecast3 = 0;
                            float childShot1 = 0;
                            float childShot2 = 0;
                            float childShot3 = 0;

                            if (shot1 >= 0)
                            {
                                childForecast1 = 0;

                            }
                            else
                            {
                                childForecast1 = shot1;
                            }

                            childShot1 = childReadyStock + childForecast1;

                            if (shot2 >= 0)
                            {
                                childForecast2 = 0;
                            }
                            else if (shot1 > 0)
                            {
                                childForecast2 = shot2;
                            }
                            else
                            {
                                childForecast2 = Forecast2Num * -1;
                            }

                            childShot2 = childShot1 + childForecast2;

                            if (shot3 >= 0)
                            {
                                childForecast3 = 0;
                            }
                            else if (shot2 > 0)
                            {
                                childForecast3 = shot3;
                            }
                            else
                            {
                                childForecast3 = Forecast3Num * -1;
                            }

                            childShot3 = childShot2 + childForecast3;

                            if (type == 1)
                            {
                                balSave = childReadyStock;

                                if (childForecast1 < 0)
                                {
                                    childForecast1 *= -1;
                                }

                                forecastSave = childForecast1;
                                shotSave = childShot1;
                            }
                            else if (type == 2)
                            {
                                balSave = childShot1;

                                if (childForecast2 < 0)
                                {
                                    childForecast2 *= -1;
                                }

                                forecastSave = childForecast2;
                                shotSave = childShot2;
                            }
                            else if (type == 3)
                            {
                                if (childForecast3 < 0)
                                {
                                    childForecast3 *= -1;
                                }

                                balSave = childShot2;
                                forecastSave = childForecast3;
                                shotSave = childShot3;
                            }

                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = Join["join_child_code"].ToString();
                            uMatUsed.quantity_order = Convert.ToInt32(balSave);
                            uMatUsed.quantity_order_two = Convert.ToInt32(forecastSave);
                            uMatUsed.quantity_order_three = Convert.ToInt32(shotSave);

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
                        if (type == 1)
                        {
                            balSave = readyStock;
                            forecastSave = Forecast1Num;
                            shotSave = shot1;
                        }
                        else if (type == 2)
                        {
                            balSave = shot1;
                            forecastSave = Forecast2Num;
                            shotSave = shot2;
                        }
                        else
                        {
                            balSave = shot2;
                            forecastSave = Forecast3Num;
                            shotSave = shot3;
                        }

                        uMatUsed.no = forecastIndex;
                        uMatUsed.item_code = itemCode;
                        uMatUsed.quantity_order = Convert.ToInt32(balSave);
                        uMatUsed.quantity_order_two = Convert.ToInt32(forecastSave);
                        uMatUsed.quantity_order_three = Convert.ToInt32(shotSave);

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
        #endregion
    }
}
