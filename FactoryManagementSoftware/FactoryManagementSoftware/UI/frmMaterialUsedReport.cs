﻿using FactoryManagementSoftware.DAL;
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
using System.Globalization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMaterialUsedReport : Form
    {
        public frmMaterialUsedReport()
        {
            InitializeComponent();
            tool.loadCustomerAndAllToComboBox(cmbCust);
            addDataToTypeCMB();
        }

        #region Valiable Declare

        readonly string headerMatUsed = "material_used";
        readonly string headerMatUsedAndWastage = "material_used_include_wastage";
        readonly string headerTotalMatUsed = "total_material_used";

        readonly string cmbTypeActual = "Actual Used";
        readonly string cmbTypeNextMonth = "Next Month Forecast";
        readonly string cmbTypeNextNextMonth = "Next Next Month Forecast";

        #endregion

        #region create class object (database)

        custBLL uCust = new custBLL();
        custDAL dalCust = new custDAL();

        facBLL uFac = new facBLL();
        facDAL dalFac = new facDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        itemCustBLL uItemCust = new itemCustBLL();
        itemCustDAL dalItemCust = new itemCustDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        forecastBLL uForecast = new forecastBLL();
        forecastDAL dalForecast = new forecastDAL();

        materialUsedBLL uMatUsed = new materialUsedBLL();
        materialUsedDAL dalMatUsed = new materialUsedDAL();

        userDAL dalUser = new userDAL();

        Tool tool = new Tool();
        #endregion

        #region load/close data

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

                            float stillOne = readyStock - jQty;
                            float stllTwo = readyStock - iQty;

                            float actualBalance = readyStock - stillOne - stllTwo;
                            dt.Rows[j]["quantity_order"] = actualBalance.ToString();
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        private DataTable RemoveDuplicates(DataTable dt)
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

        private void addDataToTypeCMB()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(cmbTypeActual);
            cmbType.Items.Add(cmbTypeNextMonth);
            cmbType.Items.Add(cmbTypeNextNextMonth);
            cmbType.SelectedIndex = 0;
        }

        private void insertAllItemForecastData()
        {
            string forecast = "forecast_two";
            if (cmbType.Text.Equals(cmbTypeNextNextMonth))
            {
                forecast = "forecast_three";
            }

            if (!string.IsNullOrEmpty(cmbCust.Text))
            {
                DataTable dt = dalItemCust.Select();

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int Forecast2 = 0;
                    string itemCode;

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = dalItemCust.itemCodeSearch(itemCode);

                        Forecast2 = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                Forecast2 += Convert.ToInt32(outRecord[forecast]);
                            }
                        }

                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();
                                uMatUsed.quantity_order = Forecast2;

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
                            uMatUsed.quantity_order = Forecast2;

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

        private void insertItemForecastData()
        {
            string forecast = "forecast_two";
            if (cmbType.Text.Equals(cmbTypeNextNextMonth))
            {
                forecast = "forecast_three";
            }

            string custName = cmbCust.Text;

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int Forecast2 = 0;
                    string itemCode;

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = dalItemCust.itemCodeSearch(itemCode);

                        Forecast2 = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                Forecast2 += Convert.ToInt32(outRecord[forecast]);
                            }
                        }

                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();
                                uMatUsed.quantity_order = Forecast2;

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
                            uMatUsed.quantity_order = Forecast2;

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

        private void insertAllItemQuantityOrderData()
        {
            if (!string.IsNullOrEmpty(cmbCust.Text))
            {
                DataTable dt = dalItemCust.Select();

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int outStock = 0;
                    string itemCode;
                    string start = dtpStart.Value.ToString("yyyy/MM/dd");
                    string end = dtpEnd.Value.ToString("yyyy/MM/dd");

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = daltrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                if (outRecord["trf_result"].ToString().Equals("Passed"))
                                {
                                    //outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
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

        private void insertItemQuantityOrderData()
        {
            string custName = cmbCust.Text;

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
                    string start = dtpStart.Value.ToString("yyyy/MM/dd");
                    string end = dtpEnd.Value.ToString("yyyy/MM/dd");

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = daltrfHist.rangeItemToCustomerSearch(cmbCust.Text, start, end, itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                if (outRecord["trf_result"].ToString().Equals("Passed"))
                                {
                                    //outStock += Convert.ToSingle(outRecord["trf_hist_qty"]);
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

        private void insertOrderAlertOneData()
        {
            string forecast = "forecast_one";

            if (!string.IsNullOrEmpty(cmbCust.Text))
            {
                DataTable dt = dalItemCust.Select();

                dt = RemoveDuplicates(dt);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int ForecastQty = 0;
                    int outStock = 0;
                    int readyStock = 0;
                    int stillNeed = 0;
                    string itemCode;
                    string currentMonth;

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        readyStock = 0;
                        outStock = 0;
                        stillNeed = 0;
                        ForecastQty = 0;
                        
                        itemCode = item["item_code"].ToString();
                        currentMonth = item["forecast_current_month"].ToString();
                        currentMonth = DateTime.ParseExact(currentMonth, "MMMM", CultureInfo.CurrentCulture).Month.ToString();
                        string year = DateTime.Now.Year.ToString();

                        readyStock = Convert.ToInt32(dalItem.getStockQty(itemCode));

                        DataTable dt3 = daltrfHist.rangeItemToAllCustomerSearchByMonth(currentMonth, year, itemCode);

                        

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

                        DataTable dt4 = dalItemCust.itemCodeSearch(itemCode);

                        if (dt4.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt4.Rows)
                            {
                                ForecastQty += Convert.ToInt32(outRecord[forecast]);
                            }
                        }

                        stillNeed = readyStock - ForecastQty + outStock;
                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();



                                if (stillNeed < 0)
                                {
                                    stillNeed *= -1;
                                }

                                uMatUsed.quantity_order = Convert.ToInt32(dalItem.getStockQty(uMatUsed.item_code)) - stillNeed;

                                

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
                            uMatUsed.quantity_order = stillNeed;

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

        private void insertOrderAlertForecastData()
        {
            string forecast = "forecast_two";
            if (cmbType.Text.Equals(cmbTypeNextNextMonth))
            {
                forecast = "forecast_three";
            }

            if (!string.IsNullOrEmpty(cmbCust.Text))
            {
                DataTable dt = dalItemCust.Select();

                dt = RemoveDuplicates(dt);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int ForecastQty = 0;
                    int readyStock = 0;
                    int stillNeed = 0;
                    string itemCode;
    

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        readyStock = 0;
                        stillNeed = 0;
                        ForecastQty = 0;

                        itemCode = item["item_code"].ToString();
                      
                        string year = DateTime.Now.Year.ToString();

                        readyStock = Convert.ToInt32(dalItem.getStockQty(itemCode));

                       

                        DataTable dt4 = dalItemCust.itemCodeSearch(itemCode);

                        if (dt4.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt4.Rows)
                            {
                                ForecastQty += Convert.ToInt32(outRecord[forecast]);
                            }
                        }

                        stillNeed = readyStock - ForecastQty;
                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();

                                if (stillNeed < 0)
                                {
                                    stillNeed *= -1;
                                }

                                uMatUsed.quantity_order = Convert.ToInt32(dalItem.getStockQty(uMatUsed.item_code)) - stillNeed;



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
                            uMatUsed.quantity_order = stillNeed;

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

        private void loadOrderAlertData()
        {
            int n = 0;
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

            dgvMaterialUsedRecord.Rows.Clear();
            dgvMaterialUsedRecord.Refresh();
            foreach (DataRow item in dt.Rows)
            {

                n = dgvMaterialUsedRecord.Rows.Add();

                itemWeight = Convert.ToSingle(item["item_part_weight"].ToString());
                OrderQty = Convert.ToInt32(item["quantity_order"].ToString());
                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                if(OrderQty < 0)
                {
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

                        dgvMaterialUsedRecord.Rows[n-1].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                        dgvMaterialUsedRecord.Rows[n-1].Cells[headerTotalMatUsed].Value = totalMaterialUsed;

                        materialType = item["item_material"].ToString();
                        totalMaterialUsed = materialUsed + wastageUsed;
                        n = dgvMaterialUsedRecord.Rows.Add();
                       
                        index++;

                    }
                }
                else
                {
                    materialUsed = 0;
                    wastageUsed = 0;
                    
                }

                dgvMaterialUsedRecord.Rows[n].Cells["no"].Value = index;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemMaterial].Value = item[dalItem.ItemMaterial].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsedAndWastage].Value = materialUsed + wastageUsed;
                index++;

                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemColor].Value = item[dalItem.ItemCode].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemOrd].Value = item["quantity_order"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemProPWPcs].Value = itemWeight;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemWastage].Value = item[dalItem.ItemWastage].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsed].Value = materialUsed;



            }
            
                // this is the last item
                totalMaterialUsed += materialUsed + wastageUsed;
                dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Value = totalMaterialUsed;
       
            //listPaint(dgvMaterialUsedRecord);
            //tool.listPaint(dgvMaterialUsedRecord);
            tool.listPaintGreyHeader(dgvMaterialUsedRecord);

        }

        private void loadMaterialUsedList()
        {
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

            dgvMaterialUsedRecord.Rows.Clear();
            dgvMaterialUsedRecord.Refresh();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvMaterialUsedRecord.Rows.Add();

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
                    
                    dgvMaterialUsedRecord.Rows[n - 1].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n - 1].Cells[headerTotalMatUsed].Value = totalMaterialUsed;

                    materialType = item["item_material"].ToString();
                    totalMaterialUsed = materialUsed + wastageUsed;
                    n = dgvMaterialUsedRecord.Rows.Add();
                }

                
                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    totalMaterialUsed = materialUsed + wastageUsed;
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Value = totalMaterialUsed;
                }

                dgvMaterialUsedRecord.Rows[n].Cells["no"].Value = index;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemMaterial].Value = item[dalItem.ItemMaterial].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemColor].Value = item[dalItem.ItemCode].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemOrd].Value = item["quantity_order"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemProPWPcs].Value = itemWeight;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemWastage].Value = item[dalItem.ItemWastage].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsed].Value = materialUsed;
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsedAndWastage].Value = materialUsed + wastageUsed;
                index++;

            }

            //listPaint(dgvMaterialUsedRecord);
            //tool.listPaint(dgvMaterialUsedRecord);
            tool.listPaintGreyHeader(dgvMaterialUsedRecord);

        }

        private void frmMaterialUsedReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MaterialUsedReportFormOpen = false;
        }

        private void frmMaterialUsedReport_Load(object sender, EventArgs e)
        {
            //loadCustomerList();
        }

        private void loadCustomerList()
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmbCust.DataSource = distinctTable;
            cmbCust.DisplayMember = "cust_name";
            cmbCust.SelectedIndex = -1;
        }

        #endregion

        #region function

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
          
            bool result = dalMatUsed.Delete();

            if (!result)
            {
                MessageBox.Show("Failed to reset material used data");
            }
            else
            {
                if(cmbCust.Text.Equals("All"))
                {
                    if(cmbType.Text.Equals(cmbTypeActual))
                    {
                        //insertAllItemQuantityOrderData();
                        insertOrderAlertOneData();
                    }
                    else
                    {
                        //insertAllItemForecastData();
                        insertOrderAlertForecastData();
                    }
                    
                }
                else
                {
                    if (cmbType.Text.Equals(cmbTypeActual))
                    {
                        insertItemQuantityOrderData();
                    }
                    else
                    {
                        insertItemForecastData();
                    }
                    
                }
                //loadMaterialUsedList();        
                loadOrderAlertData();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void frmMaterialUsedReport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            dgvMaterialUsedRecord.ClearSelection();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region export to excel

        //private string getNextFileName(string fileName)
        //{
        //    string extension = Path.GetExtension(fileName);

        //    int i = 0;
        //    while (File.Exists(fileName))
        //    {
        //        if (i == 0)
        //            fileName = fileName.Replace(extension, "(" + ++i + ")" + extension);
        //        else
        //            fileName = fileName.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
        //    }

        //    return fileName;
        //}

        private string setFileName()
        {
            string fileName = "Test.xls";
            DateTime currentDate = DateTime.Now;
            fileName = "MaterialUsedReport(" + cmbCust.Text + "_"+dtpStart.Text+"-"+dtpEnd.Text+")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
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
                xlWorkSheet.Name = cmbCust.Text;

                #region Save data to Sheet

                //Header and Footer setup
                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dtpStart.Text + ">" + dtpEnd.Text; 
                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbCust.Text + ") MATERIAL USED REPORT";
                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet.PageSetup.CenterFooter = DateTime.Now.Date.ToString("dd/MM/yyyy")+" Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

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
                dgvMaterialUsedRecord.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void copyAlltoClipboard()
        {
            dgvMaterialUsedRecord.SelectAll();
            DataObject dataObj = dgvMaterialUsedRecord.GetClipboardContent();
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

        #region slected index changed

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMaterialUsedRecord.Rows.Clear();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMaterialUsedRecord.Rows.Clear();
        }

        #endregion
    }
}