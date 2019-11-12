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
using System.Globalization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMaterialUsedReport : Form
    {
        public frmMaterialUsedReport()
        {
            InitializeComponent();

            tool.LoadCustomerAndAllToComboBox(cmbCust);
            cbZeroCost.Visible = false;
            addDataToTypeCMB();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            dtpStart.Value = tool.GetStartDate(month, year);
            dtpEnd.Value = tool.GetEndDate(month, year);
        }

        #region Valiable Declare

        readonly string headerMatUsed = "material_used";
        readonly string headerMatUsedAndWastage = "material_used_include_wastage";
        readonly string headerTotalMatUsed = "total_material_used";

        readonly string cmbTypeActualUsed = "ACTUAL USED";
        readonly string cmbTypeActualStore = "ACTUAL STOCK";
        private string cmbTypeCurrentMonth = "Current Month Forecast";
        private string cmbTypeNextMonth = "Next Month Forecast";
        private string cmbTypeNextNextMonth = "Next Next Month Forecast";
        private string cmbTypeNextNextNextMonth = "Next Next Next Month Forecast";

        private string forecastMonth = "!";

        private bool loaded = false;
        private bool startDateUpdated = false;
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

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        Tool tool = new Tool();
        Text text = new Text();
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

        private DataTable RemoveNonZeroCost(DataTable dt)
        {
            materialDAL dalMat = new materialDAL();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if(dt.Rows[i]["item_cat"].ToString().Equals("Part"))
                    {
                        if (!dalMat.checkIfZeroCost(dt.Rows[i]["item_material"].ToString()))
                        {
                            dt.Rows[i].Delete();
                        }
                    }
                    else
                    {
                        if (!dalMat.checkIfZeroCost(dt.Rows[i]["item_code"].ToString()))
                        {
                            dt.Rows[i].Delete();
                        }
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

        private void addDataToTypeCMB()
        {
            cmbType.Items.Clear();

            int currentMonth = Convert.ToInt32(DateTime.Now.Month.ToString("00"));
            cmbTypeCurrentMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth);

            if (currentMonth + 1 > 12)
            {
                cmbTypeNextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth + 1 - 12);
            }
            else
            {
                cmbTypeNextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth + 1);
            }

            if (currentMonth + 2 > 12)
            {
                cmbTypeNextNextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth + 2 - 12);
            }
            else
            {
                cmbTypeNextNextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth + 2);
            }

            if (currentMonth + 3 > 12)
            {
                cmbTypeNextNextNextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth + 3 - 12);
            }
            else
            {
                cmbTypeNextNextNextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth + 3);
            }

            cmbTypeCurrentMonth = cmbTypeCurrentMonth.ToUpper() + " FORECAST";
            cmbTypeNextMonth = cmbTypeNextMonth.ToUpper() + " FORECAST";
            cmbTypeNextNextMonth = cmbTypeNextNextMonth.ToUpper() + " FORECAST";
            cmbTypeNextNextNextMonth = cmbTypeNextNextNextMonth.ToUpper() + " FORECAST";

            cmbType.Items.Add(cmbTypeActualUsed);
            cmbType.Items.Add(cmbTypeActualStore);
            cmbType.Items.Add(cmbTypeCurrentMonth);
            cmbType.Items.Add(cmbTypeNextMonth);
            cmbType.Items.Add(cmbTypeNextNextMonth);
            cmbType.Items.Add(cmbTypeNextNextNextMonth);
            cmbType.SelectedIndex = 0;
        }

        private void insertAllItemForecastData()
        {
            int forecastNum = 1;

            if (cmbType.Text.Equals(cmbTypeNextMonth))
            {

                forecastNum = 2;
            }
            else if (cmbType.Text.Equals(cmbTypeNextNextMonth))
            {

                forecastNum = 3;
            }
            else if (cmbType.Text.Equals(cmbTypeNextNextNextMonth))
            {

                forecastNum = 4;
            }

            if (!string.IsNullOrEmpty(cmbCust.Text))
            {
                DataTable dt = dalItemCust.Select();
                DataTable dt_ItemForecast = dalItemForecast.Select();
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    float ForecastQty = 0;
                    string itemCode;

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        ForecastQty = tool.GetForecastQty(dt_ItemForecast, itemCode, forecastNum);

                        if (ForecastQty < 0)
                        {
                            ForecastQty = 0;
                        }

                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                if (tool.ifGotChild(Join["join_child_code"].ToString()))
                                {
                                    DataTable dtJoin2 = dalJoin.loadChildList(Join["join_child_code"].ToString());
                                    foreach (DataRow Join2 in dtJoin2.Rows)
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join2["join_child_code"].ToString();
                                        uMatUsed.quantity_order = ForecastQty;

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
                                    uMatUsed.quantity_order = ForecastQty;

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
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = ForecastQty;

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

        private void insertItemForecastData()
        {
            float ForecastQty = 0;
            int forecastNum = 1;
 
            if (cmbType.Text.Equals(cmbTypeNextMonth))
            {
    
                forecastNum = 2;
            }
            else if (cmbType.Text.Equals(cmbTypeNextNextMonth))
            {
      
                forecastNum = 3;
            }
            else if (cmbType.Text.Equals(cmbTypeNextNextNextMonth))
            {
      
                forecastNum = 4;
            }

            string custName = cmbCust.Text;

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);
                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(custName).ToString());
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
                        //ForecastQty = Convert.ToInt32(item[forecast]);
                        ForecastQty = tool.GetForecastQty(dt_ItemForecast, itemCode, forecastNum);

                        if(ForecastQty < 0)
                        {
                            ForecastQty = 0;
                        }

                        //ForecastQty = item[forecast] == DBNull.Value? 0: Convert.ToInt32(item[forecast]);
                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = itemCode;
                                uMatUsed.quantity_order = ForecastQty;

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

                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                if (tool.ifGotChild(Join["join_child_code"].ToString()))
                                {
                                    if (dalItem.checkIfProduction(Join["join_child_code"].ToString()) && !dalItem.checkIfAssembly(Join["join_child_code"].ToString()))
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join["join_child_code"].ToString();
                                        uMatUsed.quantity_order = ForecastQty;

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

                                    DataTable dtJoin2 = dalJoin.loadChildList(Join["join_child_code"].ToString());
                                    foreach (DataRow Join2 in dtJoin2.Rows)
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join2["join_child_code"].ToString();
                                        uMatUsed.quantity_order = ForecastQty;

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
                                    uMatUsed.quantity_order = ForecastQty;

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
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = ForecastQty;

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

        private void insertItemActualStoreData()
        {
            int actualStock = 0;
            
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
                    string itemCode;
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();
                        actualStock = Convert.ToInt32(dalItem.getStockQty(itemCode));

                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = itemCode;
                                uMatUsed.quantity_order = actualStock;

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

                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                if (tool.ifGotChild(Join["join_child_code"].ToString()))
                                {
                                    if (dalItem.checkIfProduction(Join["join_child_code"].ToString()) && !dalItem.checkIfAssembly(Join["join_child_code"].ToString()))
                                    {
                                        int joinQty = Convert.ToInt16(Join["join_qty"].ToString());
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join["join_child_code"].ToString();
                                        uMatUsed.quantity_order = Convert.ToInt32(dalItem.getStockQty(Join["join_child_code"].ToString())) + actualStock*joinQty;

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

                                    DataTable dtJoin2 = dalJoin.loadChildList(Join["join_child_code"].ToString());
                                    foreach (DataRow Join2 in dtJoin2.Rows)
                                    {
                                        int joinQty = Convert.ToInt16(Join2["join_qty"].ToString());
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join2["join_child_code"].ToString();
                                        uMatUsed.quantity_order = Convert.ToInt32(dalItem.getStockQty(Join2["join_child_code"].ToString())) + Convert.ToInt32(dalItem.getStockQty(Join["join_child_code"].ToString()))*joinQty + actualStock;

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
                                    float joinQty = Convert.ToSingle(Join["join_qty"].ToString());
                                    uMatUsed.no = forecastIndex;
                                    uMatUsed.item_code = Join["join_child_code"].ToString();
                                    uMatUsed.quantity_order = Convert.ToSingle(dalItem.getStockQty(Join["join_child_code"].ToString())) + actualStock*joinQty;

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
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = actualStock;

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

        private void insertAllItemStoreData()
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
                    float storeNum = 0;
                    string itemCode;

                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();


                        storeNum = dalItem.getStockQty(itemCode);

                       
                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = itemCode;
                                uMatUsed.quantity_order = Convert.ToInt32(storeNum);
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

                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                if (tool.ifGotChild(Join["join_child_code"].ToString()))
                                {
                                    if (dalItem.checkIfProduction(Join["join_child_code"].ToString()) && !dalItem.checkIfAssembly(Join["join_child_code"].ToString()))
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join["join_child_code"].ToString();
                                        uMatUsed.quantity_order = Convert.ToInt32(dalItem.getStockQty(Join["join_child_code"].ToString()));
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

                                    DataTable dtJoin2 = dalJoin.loadChildList(Join["join_child_code"].ToString());
                                    foreach (DataRow Join2 in dtJoin2.Rows)
                                    {
                                        uMatUsed.no = forecastIndex;
                                        uMatUsed.item_code = Join2["join_child_code"].ToString();
                                        uMatUsed.quantity_order = Convert.ToInt32(storeNum);

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
                                    uMatUsed.quantity_order = Convert.ToInt32(storeNum);

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
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = Convert.ToInt32(storeNum);

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
                                    outStock += Convert.ToInt32(outRecord["trf_hist_qty"]);
                                }
                            }
                        }


                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                if (tool.ifGotChild(Join["join_child_code"].ToString()))
                                {
                                    DataTable dtJoin2 = dalJoin.loadChildList(Join["join_child_code"].ToString());
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

        private void insertSearchItemStoreData()
        {
            string custName = cmbCust.Text;
            string keyword = txtSearch.Text;

            if (!string.IsNullOrEmpty(keyword))
            {
                DataTable dt = dalItem.Search(keyword);

                if (dt.Rows.Count <= 0)
                {
                    lblSearchError.Show();
                }
                else
                {
                    lblSearchError.Hide();

                    int storeNum = 0;
                    string itemCode;
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        storeNum = Convert.ToInt32(dalItem.getStockQty(itemCode));
                       
                        bool result = false;

                        uMatUsed.no = forecastIndex;
                        uMatUsed.item_code = itemCode;
                        uMatUsed.quantity_order = storeNum;

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
            else
            {
                lblSearchError.Show();
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
                                    outStock += Convert.ToInt32(outRecord["trf_hist_qty"]);
                                }
                            }
                        }
                        bool result = false;
                        if (tool.ifGotChild(itemCode))
                        {
                            if(dalItem.checkIfProduction(itemCode))
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

                            DataTable dtJoin = dalJoin.loadChildList(itemCode);
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

                                    DataTable dtJoin2 = dalJoin.loadChildList(Join["join_child_code"].ToString());
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
                                        MessageBox.Show("failed to insert material used data"+itemCode+ Join["join_child_code"].ToString());
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

        private void InsertDataIfGotChild(string itemCode, int forecastIndex, int qty)
        {
            bool result = false;
            if (tool.ifGotChild(itemCode))
            {
                DataTable dtJoin = dalJoin.loadChildList(itemCode);
                foreach (DataRow Join in dtJoin.Rows)
                {
                    if (tool.ifGotChild(Join["join_child_code"].ToString()))
                    {
                       
                    }
                    else
                    {
                        uMatUsed.no = forecastIndex;
                        uMatUsed.item_code = Join["join_child_code"].ToString();
                        uMatUsed.quantity_order = qty;

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
                uMatUsed.quantity_order = qty;

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
            DataTable dt_itemInfo = dalItem.Select();

            if (cmbType.Text.Equals(cmbTypeActualStore))
            {
                dt = RemoveDuplicates(dt);
            }
            else
            {
                dt = AddDuplicates(dt);
            }
            
            dt = RemoveEmpty(dt);

            if (cbZeroCost.Checked)
            {
                dt = RemoveNonZeroCost(dt);
            }

            if(!cbSubMat.Checked)
            {
                dt = RemoveSubMat(dt);

            }


            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();


            DataTable dtJoin = dalJoin.SelectwithChildInfo();

            dgvMaterialUsedRecord.Rows.Clear();
            dgvMaterialUsedRecord.Refresh();

            foreach (DataRow item in dt.Rows)
            {
                int n = dgvMaterialUsedRecord.Rows.Add();

                itemWeight = Convert.ToSingle(item[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(item[dalItem.ItemQuoRWPcs].ToString());

                if(itemWeight <= 0)
                {
                    itemWeight = Convert.ToSingle(item[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(item[dalItem.ItemProRWPcs].ToString());
                }
                ///////////////////////////////////////////////////////////////////////

                string itemCode = item[dalItem.ItemCode].ToString();
                
                OrderQty = Convert.ToInt32(item["quantity_order"]);

                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                if(item[dalItem.ItemCat].ToString().Equals("Part"))
                {
                    materialUsed = OrderQty * itemWeight / 1000;

                }
                else
                {
                    materialUsed = OrderQty;
                }

                wastageUsed = materialUsed * wastagePercetage;

                if (item[dalItem.ItemCat].ToString().Equals("Part"))
                {
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
                }

                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    // = materialUsed + wastageUsed;
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Value = totalMaterialUsed;
                }
                
                dgvMaterialUsedRecord.Rows[n].Cells["no"].Value = index;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemMaterial].Value = item[dalItem.ItemMaterial].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemColor].Value = item[dalItem.ItemColor].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemOrd].Value = item["quantity_order"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemProPWPcs].Value = itemWeight;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemWastage].Value = Convert.ToSingle(item[dalItem.ItemWastage]).ToString("0.00");
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsed].Value = materialUsed;
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsedAndWastage].Value = materialUsed + wastageUsed;

                //SUB MATERIAL
                if (!item[dalItem.ItemCat].ToString().Equals("Part"))
                {
                    dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemMaterial].Value = tool.getCatNameFromDataTable(dt_itemInfo,itemCode);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Value = materialUsed + wastageUsed;
                }
                    

                index++;
            }
            tool.listPaintGreyHeader(dgvMaterialUsedRecord);
        }

        private void loadActualStockList()
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
            DataTable dt_itemInfo = dalItem.Select();

            if (cmbType.Text.Equals(cmbTypeActualStore))
            {
                dt = RemoveDuplicates(dt);
            }
            else
            {
                dt = AddDuplicates(dt);
            }

            dt = RemoveEmpty(dt);

            if (cbZeroCost.Checked)
            {
                dt = RemoveNonZeroCost(dt);
            }

            if (!cbSubMat.Checked)
            {
                dt = RemoveSubMat(dt);

            }


            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();


            DataTable dtJoin = dalJoin.SelectwithChildInfo();

            dgvMaterialUsedRecord.Rows.Clear();
            dgvMaterialUsedRecord.Refresh();

            foreach (DataRow item in dt.Rows)
            {
                int n = dgvMaterialUsedRecord.Rows.Add();

                itemWeight = Convert.ToSingle(item[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(item[dalItem.ItemQuoRWPcs].ToString());

                if (itemWeight <= 0)
                {
                    itemWeight = Convert.ToSingle(item[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(item[dalItem.ItemProRWPcs].ToString());
                }
                ///////////////////////////////////////////////////////////////////////

                string itemCode = item[dalItem.ItemCode].ToString();

                OrderQty = Convert.ToInt32(tool.getStockQtyFromDataTable(dt_itemInfo, itemCode));

                if (tool.ifGotParent(itemCode, dtJoin))
                {
                    string ParentCode;
                    int joinQty;
                    DataTable dt_Parent = dalJoin.loadParentList(itemCode);

                    foreach (DataRow row in dt_Parent.Rows)
                    {
                        if (row["join_child_code"].ToString().Equals(itemCode))
                        {
                            ParentCode = row["join_parent_code"].ToString();
                            joinQty = Convert.ToInt32(row["join_qty"].ToString());
                            OrderQty += Convert.ToInt32(tool.getStockQtyFromDataTable(dt_itemInfo, ParentCode)) * joinQty;

                            if (tool.ifGotParent(ParentCode, dtJoin))
                            {
                                string GrandparentCode;
                                DataTable dt_Grandparent = dalJoin.loadParentList(ParentCode);

                                foreach (DataRow row2 in dt_Grandparent.Rows)
                                {
                                    if (row2["join_child_code"].ToString().Equals(ParentCode))
                                    {
                                        GrandparentCode = row2["join_parent_code"].ToString();
                                        joinQty = Convert.ToInt32(row2["join_qty"].ToString());
                                        OrderQty += Convert.ToInt32(tool.getStockQtyFromDataTable(dt_itemInfo, GrandparentCode)) * joinQty;
                                    }
                                }
                            }
                        }
                    }
                }

                item["quantity_order"] = OrderQty;

                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                if (item[dalItem.ItemCat].ToString().Equals("Part"))
                {
                    materialUsed = OrderQty * itemWeight / 1000;

                }
                else
                {
                    materialUsed = OrderQty;
                }

                wastageUsed = materialUsed * wastagePercetage;

                if (item[dalItem.ItemCat].ToString().Equals("Part"))
                {
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
                }

                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    // = materialUsed + wastageUsed;
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Value = totalMaterialUsed;
                }

                dgvMaterialUsedRecord.Rows[n].Cells["no"].Value = index;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemMaterial].Value = item[dalItem.ItemMaterial].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemName].Value = item[dalItem.ItemName].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemCode].Value = item[dalItem.ItemCode].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemColor].Value = item[dalItem.ItemColor].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemOrd].Value = item["quantity_order"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemProPWPcs].Value = itemWeight;
                dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemWastage].Value = Convert.ToSingle(item[dalItem.ItemWastage]).ToString("0.00");
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsed].Value = materialUsed;
                dgvMaterialUsedRecord.Rows[n].Cells[headerMatUsedAndWastage].Value = materialUsed + wastageUsed;

                //SUB MATERIAL
                if (!item[dalItem.ItemCat].ToString().Equals("Part"))
                {
                    dgvMaterialUsedRecord.Rows[n].Cells[dalItem.ItemMaterial].Value = tool.getCatNameFromDataTable(dt_itemInfo, itemCode);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells[headerTotalMatUsed].Value = materialUsed + wastageUsed;
                }


                index++;
            }
            tool.listPaintGreyHeader(dgvMaterialUsedRecord);
        }

        private void frmMaterialUsedReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MaterialUsedReportFormOpen = false;
        }

        private void frmMaterialUsedReport_Load(object sender, EventArgs e)
        {
            //loadCustomerList();
            tool.DoubleBuffered(dgvMaterialUsedRecord, true);
            loaded = true;
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
                    if(cmbType.Text.Equals(cmbTypeActualUsed))
                    {
                        insertAllItemQuantityOrderData();
                    }
                    else if(cmbType.Text.Equals(cmbTypeActualStore))
                    {
                        insertAllItemStoreData();
                    }
                    else
                    {
                        insertAllItemForecastData();
                    }
                    loadMaterialUsedList();
                }
                else
                {
                    if (cmbType.Text.Equals(cmbTypeActualUsed))
                    {
                        insertItemQuantityOrderData();
                        loadMaterialUsedList();
                    }
                    else if (cmbType.Text.Equals(cmbTypeActualStore))
                    {
                        if(cbBySearch.Checked)
                        {
                            insertSearchItemStoreData();
                        }
                        else
                        {
                            insertItemActualStoreData();
                        }
                        loadActualStockList();
                    }
                    else
                    {
                        insertItemForecastData();
                        loadMaterialUsedList();
                    }
                    
                }
                
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

        private string setFileName()
        {
            int forecastCurrentMonth = DateTime.Parse("1." + getCurrentForecastMonth() + " 2008").Month;
            string fileName = "Test.xls";
            string title = "";
            string date = dtpStart.Text + " - " + dtpEnd.Text;
            DateTime currentDate = DateTime.Now;
            if (cmbType.Text.Equals(cmbTypeActualUsed))
            {
                date = dtpStart.Text + " - " + dtpEnd.Text;
                title = "ActualMaterialUsedReport";
            }
            else if (cmbType.Text.Equals(cmbTypeCurrentMonth))
            {
                date = new DateTimeFormatInfo().GetMonthName(forecastCurrentMonth).ToUpper().ToString();
                title = "ForecastMaterialUsedReport";
            }
            else if (cmbType.Text.Equals(cmbTypeNextMonth))
            {
                int forecastNextMonth = getNextMonth(forecastCurrentMonth);
                date = new DateTimeFormatInfo().GetMonthName(forecastNextMonth).ToUpper().ToString();
                title = "ForecastMaterialUsedReport";

            }
            else if (cmbType.Text.Equals(cmbTypeNextNextMonth))
            {
                int forecastNextMonth = getNextMonth(forecastCurrentMonth);
                int forecastNextNextMonth = getNextMonth(forecastNextMonth);
                date = new DateTimeFormatInfo().GetMonthName(forecastNextNextMonth).ToUpper().ToString();
                title = "ForecastMaterialUsedReport";
            }
            else
            {
                title = "MaterialUsedReport";
            }
            

            fileName = title+"(" + cmbCust.Text + "_"+ date + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string path = @"D:\StockAssistant\Document\MaterialUsedReport";
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
                xlWorkSheet.Name = cmbCust.Text;

                #region Save data to Sheet

                //Header and Footer setup

                if (cmbType.Text.Equals(cmbTypeActualUsed))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dtpStart.Text + ">" + dtpEnd.Text;
                }
                else if (cmbType.Text.Equals(cmbTypeCurrentMonth))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + forecastMonth + " FORECAST";
                }
                else if (cmbType.Text.Equals(cmbTypeNextMonth))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + forecastMonth + " FORECAST";

                }
                else if (cmbType.Text.Equals(cmbTypeNextNextMonth))
                {
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + forecastMonth + " FORECAST";
                }
                else
                {  
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + forecastMonth;
                }

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

            if (!string.IsNullOrEmpty(cmbCust.Text))
            {
                if (cmbCust.Text.Equals(tool.getCustName(1)))
                {
                    cbZeroCost.Visible = true;
                }
                else
                {
                    cbZeroCost.Visible = false;
                }
            }
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

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMaterialUsedRecord.Rows.Clear();

            int forecastCurrentMonth = DateTime.Parse("1." + getCurrentForecastMonth() + " 2008").Month;

            if (cmbType.Text.Equals(cmbTypeActualUsed))
            {
                lblMonth.Text = new DateTimeFormatInfo().GetMonthName(forecastCurrentMonth).ToUpper().ToString() + " ACTUAL MATERIAL USED REPORT";
                dtpStart.Show();
                dtpEnd.Show();
                label1.Show();
                label3.Show();
                cbBySearch.Hide();
                lblSearch.Hide();
                txtSearch.Hide();
                cbSubMat.Visible = true;
                cmbCust.Enabled = true;
            }
            else if (cmbType.Text.Equals(cmbTypeActualStore))
            {
                cbBySearch.Show();
                lblMonth.Text = "QTY ORDER = ACTUAL STORE QTY";
                //lblSearch.Show();
                //txtSearch.Show();
                //dtpStart.Hide();
                //dtpEnd.Hide();
                //label1.Hide();
                //label3.Hide();
                //cbSubMat.Visible = false;
                //cmbCust.SelectedIndex = -1;
                //cmbCust.Enabled = false;
                dtpStart.Hide();
                dtpEnd.Hide();
                label1.Hide();
                label3.Hide();
                lblSearch.Hide();
                txtSearch.Hide();
                cbSubMat.Visible = true;
                cmbCust.Enabled = true;
            }
            else
            {
                cbBySearch.Hide();
                dtpStart.Hide();
                dtpEnd.Hide();
                label1.Hide();
                label3.Hide();
                lblSearch.Hide();
                txtSearch.Hide();
                cbSubMat.Visible = true;
                cmbCust.Enabled = true;

                if (cmbType.Text.Equals(cmbTypeCurrentMonth))
                {
                    forecastMonth = new DateTimeFormatInfo().GetMonthName(forecastCurrentMonth).ToUpper().ToString();
                    lblMonth.Text = forecastMonth  + " MATERIAL USED REPORT";
                }
                else if (cmbType.Text.Equals(cmbTypeNextMonth))
                {
                    int forecastNextMonth = getNextMonth(forecastCurrentMonth);
                    forecastMonth = new DateTimeFormatInfo().GetMonthName(forecastNextMonth).ToUpper().ToString();
                    lblMonth.Text = forecastMonth + " FORECAST MATERIAL USED REPORT";
                }
                else if (cmbType.Text.Equals(cmbTypeNextNextMonth))
                {
                    int forecastNextMonth = getNextMonth(forecastCurrentMonth);
                    int forecastNextNextMonth = getNextMonth(forecastNextMonth);
                    forecastMonth = new DateTimeFormatInfo().GetMonthName(forecastNextNextMonth).ToUpper().ToString();
                    lblMonth.Text = forecastMonth + " FORECAST MATERIAL USED REPORT";
                }
                else if (cmbType.Text.Equals(cmbTypeNextNextNextMonth))
                {
                    int forecastNextMonth = getNextMonth(forecastCurrentMonth);
                    int forecastNextNextMonth = getNextMonth(forecastNextMonth);
                    int forecastNextNextNextMonth = getNextMonth(forecastNextNextMonth);
                    forecastMonth = new DateTimeFormatInfo().GetMonthName(forecastNextNextNextMonth).ToUpper().ToString();
                    lblMonth.Text = forecastMonth + " FORECAST MATERIAL USED REPORT";
                }
                else
                {
                    lblMonth.Text = "MATERIAL USED REPORT";
                }
            }
            
        }

        #endregion

        private void btnExportAllToExcel_Click(object sender, EventArgs e)
        {

            //bgWorker.DoWork += BackgroundWorkerDoWork;
            //bgWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            try
            {
                dgvMaterialUsedRecord.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\StockReport";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //progressBar1.Show();
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                    xlexcel.PrintCommunication = false;
                    xlexcel.ScreenUpdating = false;
                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    insertDataToSheet(path, sfd.FileName);

                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvMaterialUsedRecord.ClearSelection();
                }

                //bgWorker.ReportProgress(100);
                //System.Threading.Thread.Sleep(1000);
                dgvMaterialUsedRecord.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                //progressBar1.Visible = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void insertDataToSheet(string path, string fileName)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application
                {
                    Visible = true
                };

                Workbook g_Workbook = excelApp.Workbooks.Open(
                    path,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

                bool gotData = false;
                object misValue = System.Reflection.Missing.Value;

                //load different data list to datagridview but changing the comboBox selected index


                //for (int i = 0; i <= cmbType.Items.Count - 1; i++)
                //{
                //    cmbType.SelectedIndex = i;
                //    for (int j = 0; j <= cmbSubType.Items.Count - 1; j++)
                //    {
                //        cmbSubType.SelectedIndex = j;
                //        if (cmbType.Text.Equals(CMBPartHeader))
                //        {
                //            gotData = loadPartStockData();//if data != empty return true, else false
                //        }
                //        else if (cmbType.Text.Equals(CMBMaterialHeader))
                //        {
                //            gotData = loadMaterialStockData();//if data != empty return true, else false
                //        }

                //        if (gotData)//if datagridview have data
                //        {
                //            Worksheet addedSheet = null;

                //            int count = g_Workbook.Worksheets.Count;

                //            addedSheet = g_Workbook.Worksheets.Add(Type.Missing,
                //                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                //            addedSheet.Name = cmbSubType.Text;

                //            addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                //            addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") STOCK LIST";
                //            addedSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                //            addedSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                //            //Page setup
                //            addedSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                //            addedSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                //            addedSheet.PageSetup.Zoom = false;
                //            addedSheet.PageSetup.CenterHorizontally = true;
                //            addedSheet.PageSetup.LeftMargin = 1;
                //            addedSheet.PageSetup.RightMargin = 1;
                //            addedSheet.PageSetup.FitToPagesWide = 1;
                //            addedSheet.PageSetup.FitToPagesTall = false;
                //            addedSheet.PageSetup.PrintTitleRows = "$1:$1";

                //            copyAlltoClipboard();
                //            addedSheet.Select();
                //            Range CR = (Range)addedSheet.Cells[1, 1];
                //            CR.Select();
                //            addedSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                //            Range tRange = addedSheet.UsedRange;
                //            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                //            tRange.Borders.Weight = XlBorderWeight.xlThin;
                //            tRange.Font.Size = 11;
                //            tRange.Font.Name = "Calibri";
                //            tRange.EntireRow.AutoFit();
                //            tRange.EntireColumn.AutoFit();
                //            tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                //            if (true)//cmbSubType.Text.Equals("PMMA")
                //            {
                //                for (int x = 0; x <= dgvStockReport.RowCount - 2; x++)
                //                {
                //                    for (int y = 0; y <= dgvStockReport.ColumnCount - 1; y++)
                //                    {
                //                        Range range = (Range)addedSheet.Cells[x + 2, y + 1];

                //                        if (x == 0)
                //                        {
                //                            Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                //                            header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);

                //                            header = (Range)addedSheet.Cells[1, 10];
                //                            header.Font.Color = Color.Blue;

                //                            header = (Range)addedSheet.Cells[1, 14];
                //                            header.Font.Color = Color.Red;

                //                            header = (Range)addedSheet.Cells[1, 16];
                //                            header.Font.Color = Color.Red;


                //                        }

                //                        if (dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor == SystemColors.Window)
                //                        {
                //                            range.Interior.Color = ColorTranslator.ToOle(Color.White);
                //                            if (x == 0)
                //                            {
                //                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                //                                header.Interior.Color = ColorTranslator.ToOle(Color.White);
                //                            }
                //                        }
                //                        else if (dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor == Color.Black)
                //                        {
                //                            range.Rows.RowHeight = 3;
                //                            range.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);
                //                            if (x == 0)
                //                            {
                //                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                //                                header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);
                //                            }
                //                        }
                //                        else
                //                        {
                //                            range.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);

                //                            if (x == 0)
                //                            {
                //                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                //                                header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);
                //                            }
                //                        }
                //                        range.Font.Color = dgvStockReport.Rows[x].Cells[y].Style.ForeColor;
                //                        if (dgvStockReport.Rows[x].Cells[y].Style.ForeColor == Color.Blue)
                //                        {
                //                            Range header = (Range)addedSheet.Cells[x + 2, 2];
                //                            header.Font.Underline = true;

                //                            header = (Range)addedSheet.Cells[x + 2, 3];
                //                            header.Font.Underline = true;
                //                        }

                //                    }

                //                    Int32 percentage = ((x + 1) * 100) / (dgvStockReport.RowCount - 2);
                //                    if (percentage >= 100)
                //                    {
                //                        percentage = 100;
                //                    }
                //                    bgWorker.ReportProgress(percentage);
                //                }
                //            }

                //            releaseObject(addedSheet);
                //            Clipboard.Clear();
                //            dgvStockReport.ClearSelection();
                //        }
                //    }
                //}
                g_Workbook.Worksheets.Item[1].Delete();
                g_Workbook.Save();
                releaseObject(g_Workbook);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                tool.historyRecord(text.System, e.Message, DateTime.Now, MainDashboard.USER_ID);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvMaterialUsedRecord.Rows.Clear();
        }

        private void cbBySearch_CheckedChanged(object sender, EventArgs e)
        {
            if(cbBySearch.Checked)
            {
                txtSearch.Show();
                cmbCust.Enabled = false;
            }
            else
            {
                txtSearch.Hide();
                cmbCust.Enabled = true;
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !startDateUpdated)
            {
                int year = dtpStart.Value.Year;
                int month = dtpStart.Value.Month;

                startDateUpdated = true;
                dtpStart.Value = tool.GetStartDate(month, year);
                dtpEnd.Value = tool.GetEndDate(month, year);
            }
            else
            {
                startDateUpdated = false;
            }
        }
    }
}
