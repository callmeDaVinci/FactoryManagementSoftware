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
        private string forecastNextNextNextMonth;
        private string MaterialCode;

        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerMat = "MATERIAL";
        readonly string headerCode = "CODE";
        readonly string headerName = "NAME";
        readonly string headerMB = "MB";
        readonly string headerMBRate = "MB RATE";
        readonly string headerSubMatRate = "RATE";
        readonly string headerWeight = "WEIGHT";
        readonly string headerWastage = "WASTAGE %";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerOut = "OUT";
        readonly string headerMatUsed = "MAT USED";
        readonly string headerSubMatUsed = "SUB MAT USED";
        readonly string headerMBUsed = "MB/PIGMENT USED";
        readonly string headerStillNeed = "STILL NEED";
        readonly string headerTotal = "TOTAL";

        readonly string headerOutOne = "OUT 1";
        readonly string headerOutTwo = "OUT 2";
        readonly string headerOutThree = "OUT 3";
        readonly string headerOutFour = "OUT 4";

        readonly string headerForecastOne = "Forecast 1";
        readonly string headerForecastTwo = "Forecast 2";
        readonly string headerForecastThree = "Forecast 3";
        readonly string headerForecastFour = "Forecast 4";

        private string headerBalanceZero = "FORECAST BAL 0";
        private string headerBalanceOne = "FORECAST BAL 1";
        private string headerBalanceTwo = "FORECAST BAL 2";
        private string headerBalanceThree = "FORECAST BAL 3";
        private string headerBalanceFour = "FORECAST BALANCE 4";
        readonly string headerPendingOrder = "PENDING ORDER";
        readonly string headerForecast = "FORECAST";


        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;
        DataGridViewAutoSizeColumnMode DisplayedCellsExceptHeader = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

        public frmOrderAlertDetail(string itemCode,DataTable dt)
        {
            MaterialCode = itemCode;
            InitializeComponent();
            addDataToForecastCMB();

            dgvOrderAlert.DataSource = dt;
            dgvAlertUIEdit(dgvOrderAlert);

            tool.DoubleBuffered(dgvOrderAlert, true);
            tool.DoubleBuffered(dgvMaterialUsedForecast,true);
        }

        #region UI setting

        private void dgvAlertUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerReadyStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerBalanceOne].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerBalanceTwo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerBalanceThree].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerBalanceFour].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPendingOrder].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            int forecastCurrentMonth = DateTime.Parse("1." + getCurrentForecastMonth() + " 2008").Month;
            int forecastNextMonth = getNextMonth(forecastCurrentMonth);
            int forecastNextNextMonth = getNextMonth(forecastNextMonth);
            int forecastNextNextNextMonth = getNextMonth(forecastNextNextMonth);

            string balanceOneName = new DateTimeFormatInfo().GetMonthName(forecastCurrentMonth).ToUpper().ToString() + " BAL";
            string balanceTwoName = new DateTimeFormatInfo().GetMonthName(forecastNextMonth).ToUpper().ToString() + " BAL";
            string balanceThreeName = new DateTimeFormatInfo().GetMonthName(forecastNextNextMonth).ToUpper().ToString() + " BAL";
            string balanceFourName = new DateTimeFormatInfo().GetMonthName(forecastNextNextNextMonth).ToUpper().ToString() + " BAL";

            dgv.Columns[headerBalanceOne].HeaderText = "AFTER " + balanceOneName;
            dgv.Columns[headerBalanceTwo].HeaderText = "AFTER " + balanceTwoName;
            dgv.Columns[headerBalanceThree].HeaderText = "AFTER " + balanceThreeName;
            dgv.Columns[headerBalanceFour].HeaderText = "AFTER " + balanceFourName;
        }

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
            forecastNextNextNextMonth = getForecastMonth(4);

            cmbForecast.Items.Clear();
            cmbForecast.Items.Add("UP TO "+forecastCurrentMonth);
            cmbForecast.Items.Add("UP TO " + forecastNextMonth);
            cmbForecast.Items.Add("UP TO " + forecastNextNextMonth);
            cmbForecast.Items.Add("UP TO " + forecastNextNextNextMonth);

            cmbForecast.SelectedIndex = 0;
        }

        public DataTable NewRAWMatUsedForeacastTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add(headerMat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));

            //dt.Columns.Add(headerReadyStock, typeof(float));
            //dt.Columns.Add(headerForecast, typeof(float));
            //dt.Columns.Add(headerOut, typeof(float));
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerWeight, typeof(float));
            dt.Columns.Add(headerWastage, typeof(float));
            dt.Columns.Add(headerMatUsed, typeof(float));
            dt.Columns.Add(headerTotal, typeof(float));

            return dt;
        }

        public DataTable NewMBUsedForeacastTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add(headerMat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerWeight, typeof(float));
            dt.Columns.Add(headerWastage, typeof(float));
            dt.Columns.Add(headerMatUsed, typeof(float));
            dt.Columns.Add(headerMBRate, typeof(float));
            dt.Columns.Add(headerMBUsed, typeof(float));
            dt.Columns.Add(headerTotal, typeof(float));

            return dt;
        }

        public DataTable NewSubMatForeacastTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add(headerMat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerSubMatRate, typeof(float));
            dt.Columns.Add(headerSubMatUsed, typeof(float));
            dt.Columns.Add(headerTotal, typeof(float));

            return dt;
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
            else if (type == 4)
            {
                int forecastNextMonthNum = getNextMonth(forecastCurrentMonthNum);
                int forecastNextNextMonthNum = getNextMonth(forecastNextMonthNum);
                int forecastNextNextNextMonthNum = getNextMonth(forecastNextNextMonthNum);

                monthName = new DateTimeFormatInfo().GetMonthName(forecastNextNextNextMonthNum).ToUpper().ToString();
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

        private void dgvMatUsedForecastUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMat].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[headerReadyStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerForecast].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerOut].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStillNeed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerWeight].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerWastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatUsed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTotal].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

           // dgv.Columns[headerReadyStock].HeaderText = "STOCK FORECAST";
        }

        private void dgvMBUsedForecastUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMat].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerStillNeed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerWeight].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerWastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatUsed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMBRate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMBUsed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTotal].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void dgvSubMatUsedForecastUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMat].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerStillNeed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTotal].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void loadSubMatUsedData(DataTable dtSubMat)
        {
            dgvMaterialUsedForecast.DataSource = null;

            DataTable dt = NewSubMatForeacastTable();
            DataRow dt_row;

            DataTable dt_itemInfo = dalItem.Select();

            int index = 1;
            float stillNeed = 0, bal1, bal2, bal3, bal4;
            string itemCode;
            foreach (DataRow row in dtSubMat.Rows)
            {
                if (row[headerMat].ToString().Equals(MaterialCode))
                {
                    bal1 = Convert.ToSingle(row[headerBalanceOne]);
                    bal2 = Convert.ToSingle(row[headerBalanceTwo]);
                    bal3 = Convert.ToSingle(row[headerBalanceThree]);
                    bal4 = Convert.ToSingle(row[headerBalanceFour]);

                    dt_row = dt.NewRow();
                    dt_row[headerIndex] = index;
                    dt_row[headerMat] = MaterialCode;
                    dt_row[headerCode] = row[headerCode];
                    dt_row[headerName] = row[headerName];

                    if (cmbForecast.SelectedIndex == 0)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceOne]);
                    }
                    else if (cmbForecast.SelectedIndex == 1)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceTwo]);
                    }
                    else if (cmbForecast.SelectedIndex == 2)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceThree]);
                    }
                    else if (cmbForecast.SelectedIndex == 3)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceFour]);
                    }

                    if (stillNeed >= 0)
                    {
                        stillNeed = 0;
                    }

                    stillNeed *= -1;
                    dt_row[headerStillNeed] = stillNeed;

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///get join qty data
                    dt_row[headerSubMatRate] = row[headerMBRate];
                    dt_row[headerSubMatUsed] = Math.Round(stillNeed * Convert.ToSingle(dt_row[headerSubMatRate]), 2);

                    dt.Rows.Add(dt_row);
                    index++;
                }
            }

            if (dt.Rows.Count > 0)
            {
                float totalSubMatUsed = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalSubMatUsed += Convert.ToSingle(row[headerSubMatUsed]);
                }

                dt.Rows[dt.Rows.Count - 1][headerTotal] = Math.Round(totalSubMatUsed, 2);

                dgvMaterialUsedForecast.DataSource = dt;
                dgvSubMatUsedForecastUIEdit(dgvMaterialUsedForecast);
                dgvMaterialUsedForecast.ClearSelection();
            }
        }

        private void loadMBUsedData(DataTable dtMB)
        {
            dgvMaterialUsedForecast.DataSource = null;

            DataTable dt = NewMBUsedForeacastTable();
            DataRow dt_row;

            DataTable dt_itemInfo = dalItem.Select();

            int index = 1;
            float stillNeed = 0,bal1,bal2,bal3,bal4;
            string itemCode;
            foreach(DataRow row in dtMB.Rows)
            {      
                if (row[headerMB].ToString().Equals(MaterialCode))
                {
                    itemCode = row[headerCode].ToString();
                    float itemWeight = Convert.ToSingle(row[headerWeight]);
                    float wastage = Convert.ToSingle(row[headerWastage]);
                    float mbRate = Convert.ToSingle(row[headerMBRate]);
                    bal1 = Convert.ToSingle(row[headerBalanceOne]);
                    bal2 = Convert.ToSingle(row[headerBalanceTwo]);
                    bal3 = Convert.ToSingle(row[headerBalanceThree]);
                    bal4 = Convert.ToSingle(row[headerBalanceFour]);

                    dt_row = dt.NewRow();
                    dt_row[headerIndex] = index;
                    dt_row[headerMat] = MaterialCode;
                    dt_row[headerCode] = row[headerCode];
                    dt_row[headerName] = row[headerName];
                    dt_row[headerMBRate] = row[headerMBRate];
                    dt_row[headerWeight] = row[headerWeight];
                    dt_row[headerWastage] = row[headerWastage];

                    if (cmbForecast.SelectedIndex == 0)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceOne]);
                    }
                    else if (cmbForecast.SelectedIndex == 1)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceTwo]);
                    }
                    else if (cmbForecast.SelectedIndex == 2)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceThree]);
                    }
                    else if (cmbForecast.SelectedIndex == 3)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceFour]);
                    }

                    if (stillNeed >= 0)
                    {
                        stillNeed = 0;
                    }

                    stillNeed *= -1;
                    dt_row[headerStillNeed] = stillNeed;
                    dt_row[headerMatUsed] = Math.Round(stillNeed * itemWeight / 1000 * (1 + wastage), 2);
                    dt_row[headerMBUsed] = Math.Round(Convert.ToSingle(dt_row[headerMatUsed])*mbRate, 2);
                    dt.Rows.Add(dt_row);
                    index++;
                }
            }

            if (dt.Rows.Count > 0)
            {
                float totalMBUsed = 0;
                foreach(DataRow row in dt.Rows)
                {
                    totalMBUsed += Convert.ToSingle(row[headerMBUsed]);
                }

                dt.Rows[dt.Rows.Count - 1][headerTotal] = Math.Round(totalMBUsed, 2);

                dgvMaterialUsedForecast.DataSource = dt;
                dgvMBUsedForecastUIEdit(dgvMaterialUsedForecast);
                dgvMaterialUsedForecast.ClearSelection();
            }
        }

        private void loadMatUsedData(DataTable dtMat)
        {
            dgvMaterialUsedForecast.DataSource = null;

            DataTable dt = NewRAWMatUsedForeacastTable();
            DataRow dt_row;

            DataTable dt_itemInfo = dalItem.Select();

            int index = 1;
            float stillNeed = 0, bal1, bal2, bal3, bal4;
            string itemCode;
            foreach (DataRow row in dtMat.Rows)
            {
                if (row[headerMat].ToString().Equals(MaterialCode))
                {
                    itemCode = row[headerCode].ToString();
                    float itemWeight = Convert.ToSingle(row[headerWeight]);
                    float wastage = Convert.ToSingle(row[headerWastage]);
                    float mbRate = Convert.ToSingle(row[headerMBRate]);
                    bal1 = Convert.ToSingle(row[headerBalanceOne]);
                    bal2 = Convert.ToSingle(row[headerBalanceTwo]);
                    bal3 = Convert.ToSingle(row[headerBalanceThree]);
                    bal4 = Convert.ToSingle(row[headerBalanceFour]);

                    dt_row = dt.NewRow();
                    dt_row[headerIndex] = index;
                    dt_row[headerMat] = MaterialCode;
                    dt_row[headerCode] = row[headerCode];
                    dt_row[headerName] = row[headerName];
                    dt_row[headerWeight] = row[headerWeight];
                    dt_row[headerWastage] = row[headerWastage];

                    if (cmbForecast.SelectedIndex == 0)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceOne]);
                        //dt_row[headerReadyStock] = row[headerBalanceOne];
                        //dt_row[headerForecast] = row[headerForecastOne];
                        //dt_row[headerOut] = row[headerOutOne];
                    }
                    else if (cmbForecast.SelectedIndex == 1)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceTwo]);
                        //dt_row[headerReadyStock] = row[headerBalanceTwo];
                        //dt_row[headerForecast] = row[headerForecastTwo];
                        //dt_row[headerOut] = row[headerOutTwo];
                    }
                    else if (cmbForecast.SelectedIndex == 2)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceThree]);
                        //dt_row[headerReadyStock] = row[headerBalanceThree];
                        //dt_row[headerForecast] = row[headerForecastThree];
                        //dt_row[headerOut] = row[headerOutThree];
                    }
                    else if (cmbForecast.SelectedIndex == 3)
                    {
                        stillNeed = Convert.ToSingle(row[headerBalanceFour]);
                        //dt_row[headerReadyStock] = row[headerBalanceFour];
                        //dt_row[headerForecast] = row[headerForecastFour];
                        //dt_row[headerOut] = row[headerOutFour];
                    }

                    if (stillNeed >= 0)
                    {
                        stillNeed = 0;
                    }

                    stillNeed *= -1;
                    dt_row[headerStillNeed] = stillNeed;
                    dt_row[headerMatUsed] = Math.Round(stillNeed * itemWeight / 1000 * (1 + wastage), 2);

                    dt.Rows.Add(dt_row);
                    index++;
                }
            }

            if (dt.Rows.Count > 0)
            {
                float totalMatUsed = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalMatUsed += Convert.ToSingle(row[headerMatUsed]);
                }

                dt.Rows[dt.Rows.Count - 1][headerTotal] = totalMatUsed;

                dgvMaterialUsedForecast.DataSource = dt;
                dgvMatUsedForecastUIEdit(dgvMaterialUsedForecast);
                dgvMaterialUsedForecast.ClearSelection();
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            DataTable dt_itemInfo = dalItem.Select();
            string type = tool.getCatNameFromDataTable(dt_itemInfo, MaterialCode);
            if (type.Equals("RAW Material"))
            {
                loadMatUsedData(tool.insertMaterialUsedData(tool.getCustName(1)));
            }
            else if(type.Equals("Master Batch") || type.Equals("Pigment"))
            {
                loadMBUsedData(tool.insertMaterialUsedData(tool.getCustName(1)));
            }
            else if (type.Equals("Sub Material"))
            {
                loadSubMatUsedData(tool.insertSubMaterialUsedData(tool.getCustName(1), MaterialCode));
            }
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

        private void dgvOrderAlert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmOrderAlertDetail_Load(object sender, EventArgs e)
        {
            dgvOrderAlert.ClearSelection();

        }

        private void dgvOrderAlert_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvOrderAlert;
            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name != "#" && dgv.Columns[col].Name != "TYPE" && dgv.Columns[col].Name != headerCode && dgv.Columns[col].Name != headerName)
            {
                if (dgv.Rows[row].Cells[col].Value != null)
                {
                    if (!string.IsNullOrEmpty(dgv.Rows[row].Cells[col].Value.ToString()))
                    {
                        float num = dgv.Rows[row].Cells[col].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[col].Value.ToString());
                        if (num < 0)
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

            dgv.ResumeLayout();
        }
    }
}
