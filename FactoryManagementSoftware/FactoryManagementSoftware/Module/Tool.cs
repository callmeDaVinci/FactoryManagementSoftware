using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using static Guna.UI2.Native.WinApi;
using static System.Net.Mime.MediaTypeNames;

namespace FactoryManagementSoftware.Module
{
    class Tool
    {
        #region Variable

        custDAL dalCust = new custDAL();
        facDAL dalFac = new facDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemDAL dalItem = new itemDAL();
        joinDAL dalJoin = new joinDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        materialDAL dalMaterial = new materialDAL();
        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        MacDAL dalMac = new MacDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        planningDAL dalPlanning = new planningDAL();
        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        dataTrfBLL uData = new dataTrfBLL();
        //Text text = new Text();
        SBBDataDAL dalSBB = new SBBDataDAL();
        public readonly string headerIndex = "#";

        readonly string headerParentColor = "PARENT COLOR";
        readonly string headerBackColor = "BACK COLOR";
        readonly string headerForecastType = "FORECAST TYPE";
        readonly string headerBalType = "BAL TYPE";
       
        readonly string headerType = "TYPE";
        readonly string headerRawMat = "RAW MATERIAL";
        readonly string headerPartName = "PART NAME";
        readonly string headerPartCode = "PART CODE";
        readonly string headerColorMat = "COLOR MATERIAL";
        readonly string headerPartWeight = "PART WEIGHT/G (RUNNER)";
        public readonly string headerReadyStock = "READY STOCK";
        readonly string headerEstimate = "ESTIMATE*";
        string headerForecast1 = "FCST/ NEEDED";
        string headerForecast2 = "FCST/ NEEDED";
        string headerForecast3 = "FCST/ NEEDED";
        string headerForecast4 = "FCST/ NEEDED";
        readonly string headerOut = "OUT";
        readonly string headerOutStd = "OUTSTD";
        string headerBal1 = "BAL";
        string headerBal2 = "BAL";
        string headerBal3 = "BAL";
        string headerBal4 = "BAL";

        readonly string headerMat = "MATERIAL";
        public readonly string headerCode = "CODE";
        public readonly string headerName = "NAME";
        public readonly string headerFacName = "FACTORY";
        readonly string headerMB = "MB";
        readonly string headerMBRate = "MB RATE";
        readonly string headerWeight = "WEIGHT";
        readonly string headerWastage = "WASTAGE RATE";
        //public readonly string headerReadyStock = "STOCK";
        public readonly string headerUnit = "UNIT";
        readonly string headerOutOne = "OUT 1";
        readonly string headerOutTwo = "OUT 2";
        readonly string headerOutThree = "OUT 3";
        readonly string headerOutFour = "OUT 4";

        readonly string balType_Total = "TOTAL";
        readonly string balType_Repeated = "REPEATED";
        readonly string balType_Unique = "UNIQUE";

        readonly string headerForecastOne = "Forecast 1";
        readonly string headerForecastTwo = "Forecast 2";
        readonly string headerForecastThree = "Forecast 3";
        readonly string headerForecastFour = "Forecast 4";

        //readonly string headerZeroCostStock = "ZERO COST STOCK";
        private string headerBalanceZero = "FORECAST BAL 0";
        private string headerBalanceOne = "FORECAST BAL 1";
        private string headerBalanceTwo = "FORECAST BAL 2";
        private string headerBalanceThree = "FORECAST BAL 3";
        private string headerBalanceFour = "FORECAST BALANCE 4";
        //readonly string headerPendingOrder = "PENDING ORDER";

        public readonly string headerParentCode = "PARENT CODE";
        public readonly string headerChildCode = "CHILD CODE";
        public readonly string headerJoinQty = "JOIN QTY";
        public readonly string headerJoinMax = "JOIN MAX";
        public readonly string headerJoinMin = "JOIN MIN";

        public readonly string Header_Index  = "#";
        public readonly string Header_MatCode = "MAT. CODE";
        public readonly string Header_MatName = "MAT. NAME";
        public readonly string Header_PartName = "PART NAME";
        public readonly string Header_PartCode = "PART CODE";
        public readonly string Header_Parent = "PARENT";
        public readonly string Header_Delivered = "DELIVERED";
        public readonly string Header_ItemWeight_G = "ITEM WEIGHT(g)";
        public readonly string Header_MaterialUsed_KG_Piece = "MAT. USED(KG/PIECE)";
        public readonly string Header_Wastage = "WASTAGE";
        public readonly string Header_MaterialUsedWithWastage = "MAT. USED WITH WASTAGE";
        public readonly string Header_TotalMaterialUsed_KG_Piece = "TOTAL MAT. USED(KG/PIECE)";

        readonly string header_POTblCode = "P/O TBL CODE";
        readonly string header_POCode = "P/O CODE";
        readonly string header_PONoString = "P/O NO";
        readonly string header_PODate = "P/O RECEIVED DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_Progress = "PROGRESS";
        readonly string header_Selected = "SELECTED";
        readonly string header_DataMode = "DATA MODE";
        readonly string header_DONoString = "D/O NO";

     
        readonly string header_PONo = "P/O NO";
       
        readonly string header_DeliveredDate = "DELIVERED";
       

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_OrderQty = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY";
        readonly string header_Note = "NOTE";
        readonly string header_StockCheck = "STOCK CHECK";
        readonly string header_DONo = "D/O #";
       
        readonly string header_DataType = "DATA TYPE";


        readonly string typeSingle = "SINGLE";
        readonly string typeParent = "PARENT";
        readonly string typeChild = "CHILD";

        readonly string forecastType_Forecast = "FORECAST";
        readonly string forecastType_Needed = "NEEDED";


        DataTable dt_Join = new DataTable();
        DataTable dt_Item = new DataTable();

        DataTable dt_OrginalData = new DataTable();
        #endregion

        #region UI design

        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DataType, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));

          
            return dt;
        }

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_PODate, typeof(DateTime));

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));



            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_Progress, typeof(string));

            dt.Columns.Add(header_DONoString, typeof(string));

            return dt;
        }

        private DataTable NewOrderAlertDataTable()
        {
            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";

            headerBal1 = "BAL";
            headerBal2 = "BAL";
            DataTable dt = new DataTable();

            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerIndex, typeof(float));
            dt.Columns.Add(headerRawMat, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerColorMat, typeof(string));
            dt.Columns.Add(headerPartWeight, typeof(string));
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerEstimate, typeof(float));

            string monthFrom = DateTime.Now.Month.ToString();

            string monthName = string.IsNullOrEmpty(monthFrom) ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) : monthFrom;
            int monthINT = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;

            for (int i = 1; i <= 3; i++)
            {
                if (i == 1)
                {
                    headerForecast1 = GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast1;
                    headerBal1 = GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal1;
                }

                else if (i == 2)
                {
                    headerForecast2 = GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast2;
                    headerBal2 = GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerBal2;
                }

                else if (i == 3)
                {
                    headerForecast3 = GetAbbreviatedFromFullName(monthName).ToUpper() + " " + headerForecast3;
                }

                monthINT++;

                if (monthINT > 12)
                {
                    monthINT -= 12;

                }

                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            }

            dt.Columns.Add(headerForecast1, typeof(float));
            dt.Columns.Add(headerOut, typeof(float));
            dt.Columns.Add(headerOutStd, typeof(float));
            dt.Columns.Add(headerBal1, typeof(float));
            dt.Columns.Add(headerForecast2, typeof(float));
            dt.Columns.Add(headerBal2, typeof(float));
            dt.Columns.Add(headerForecast3, typeof(float));
            return dt;
        }

        private DataTable NewMatUsedTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(Header_Index, typeof(int));
            dt.Columns.Add(Header_MatCode, typeof(string));
            dt.Columns.Add(Header_MatName, typeof(string));
            dt.Columns.Add(Header_PartName, typeof(string));
            dt.Columns.Add(Header_PartCode, typeof(string));
            dt.Columns.Add(Header_Parent, typeof(string));
            dt.Columns.Add(Header_Delivered, typeof(int));
            dt.Columns.Add(Header_ItemWeight_G, typeof(float));
            dt.Columns.Add(Header_MaterialUsed_KG_Piece, typeof(float));
            dt.Columns.Add(Header_Wastage, typeof(float));
            dt.Columns.Add(Header_MaterialUsedWithWastage, typeof(float));
            dt.Columns.Add(Header_TotalMaterialUsed_KG_Piece, typeof(float));


            return dt;
        }

        //without min width
        public void AddTextBoxColumns(DataGridView dgv,string HeaderText, string Name, DataGridViewAutoSizeColumnMode autoSize)
        {
            var col = new DataGridViewTextBoxColumn
            {
                HeaderText = HeaderText,
                Name = Name,
                AutoSizeMode = autoSize
            };

            dgv.Columns.Add(col);
        }

        //with min width
        public void AddTextBoxColumns(DataGridView dgv, string HeaderText, string Name, DataGridViewAutoSizeColumnMode autoSize, int minWidth)
        {
            var col = new DataGridViewTextBoxColumn
            {
                HeaderText = HeaderText,
                Name = Name,
                AutoSizeMode = autoSize,
                MinimumWidth = minWidth
            };

            dgv.Columns.Add(col);
        }

        public void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv.ClearSelection();
        }

        public void listPaintGreyHeader(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.ClearSelection();
        }
        #endregion

        #region Convert variable

        public int Int_TryParse(string value)
        {
            int result;
            int.TryParse(value, out result);
            return result;
        }

        public float Float_TryParse(string value)
        {
            float result;
            float.TryParse(value, out result);
            return result;
        }

        #endregion

        #region Load/Update Data

        public DateTime GetSBBMonthlyStartDate()
        {
            int dayNow = DateTime.Now.Day;

            DateTime StartDate;

            if (dayNow <= 22)
            {
                int month = DateTime.Now.Month - 1;
                int year = DateTime.Now.Year;

                if(month <= 0)
                {
                    month = 12;
                    year--;
                }
                StartDate = new DateTime(year, month, 23);

            }
            else
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 23);

            }

            return StartDate;
        }

        #region Get Item Forecast Data

        DataTable DT_PRODUCT_FORECAST_SUMMARY;
        DataTable DT_MATERIAL_FORECAST_SUMMARY;
        DataTable dt_ItemForecast;
        DataTable DT_PART_TRANSFER;
        DataTable DT_STOCK_LIST;

        BalForecastBLL balForecastBLL = new BalForecastBLL();


        public DataTable RemoveTerminatedProduct(DataTable dt_Product)
        {
            Text text = new Text();

            if (dt_Product != null)
            {
                dt_Product.AcceptChanges();
                foreach (DataRow row in dt_Product.Rows)
                {
                    string itemName = row[dalItem.ItemName].ToString();

                    if (itemName.ToUpper().Contains(text.Terminated.ToUpper()))
                    {
                        //remove item
                        row.Delete();
                    }
                }
                dt_Product.AcceptChanges();

            }
            return dt_Product;
        }

        public DataTable Product_SummaryDataTable()
        {
            DataTable dt = new DataTable();
            Text text = new Text();

            dt.Columns.Add(text.Header_IndexMarking, typeof(int));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ParentIndex, typeof(int));
            dt.Columns.Add(text.Header_GroupLevel, typeof(int));
            dt.Columns.Add(text.Header_Type, typeof(string));
            dt.Columns.Add(text.Header_PartWeight_G, typeof(float));
            dt.Columns.Add(text.Header_RunnerWeight_G, typeof(float));
            dt.Columns.Add(text.Header_WastageAllowed_Percentage, typeof(float));
            dt.Columns.Add(text.Header_ColorRate, typeof(float));
            dt.Columns.Add(text.Header_JoinQty, typeof(float));
            dt.Columns.Add(text.Header_JoinMax, typeof(float));
            dt.Columns.Add(text.Header_JoinMin, typeof(float));
            dt.Columns.Add(text.Header_JoinWastage, typeof(float));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));
            dt.Columns.Add(text.Header_Unit, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(float));

            // headerOutQty = cmbMonth.Text + "/" + cmbYear.Text + " FORECAST";

            string month = null;


            //check date
            DateTime dateStart = new DateTime(balForecastBLL.Year_From, balForecastBLL.Month_From, 1);
            DateTime dateEnd = new DateTime(balForecastBLL.Year_To, balForecastBLL.Month_To, 1);


            for (var date = dateStart; date <= dateEnd; date = date.AddMonths(1))
            {
                var i = date.Year;
                var j = date.Month;

                if (date == dateStart && j < balForecastBLL.Month_From)
                {
                    date = date.AddMonths(1);
                    i = date.Year;
                    j = date.Month;
                    date = new DateTime(i, j, 1);
                }

                month = j + "/" + i;

                dt.Columns.Add(month + text.str_Forecast, typeof(float));
                dt.Columns.Add(month + text.str_Delivered, typeof(float));
                dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
                dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));
                dt.Columns.Add(month + text.str_EstBalance, typeof(float));
            }

            dt.Columns.Add(text.Header_PendingOrder, typeof(string));

            return dt;
        }

        public DataTable Material_SummaryDataTable()
        {
            DataTable dt = new DataTable();
            Text text = new Text();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Type, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));

            #region Get Start Date & End Date


            //check date
            DateTime dateStart = new DateTime(balForecastBLL.Year_From, balForecastBLL.Month_From, 1);
            DateTime dateEnd = new DateTime(balForecastBLL.Year_To, balForecastBLL.Month_To, 1);

            #endregion

            for (var date = dateStart; date <= dateEnd; date = date.AddMonths(1))
            {
                var i = date.Year;
                var j = date.Month;

                if (date == dateStart && j < balForecastBLL.Month_From)
                {
                    date = date.AddMonths(1);
                    i = date.Year;
                    j = date.Month;
                    date = new DateTime(i, j, 1);
                }

                string month = j + "/" + i;
                dt.Columns.Add(month + text.str_Forecast, typeof(float));
                dt.Columns.Add(month + text.str_Delivered, typeof(float));
                dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
                dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));
                dt.Columns.Add(month + text.str_EstBalance, typeof(float));
            }

            dt.Columns.Add(text.Header_PendingOrder, typeof(string));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            return dt;
        }

        private DataTable New_StockList_DataTable()
        {
            Text text = new Text();
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(float));

            string month = null;

            DateTime dateStart = new DateTime(balForecastBLL.Year_From, balForecastBLL.Month_From, 1);
            DateTime dateEnd = new DateTime(balForecastBLL.Year_To, balForecastBLL.Month_To, 1);

            for (var date = dateStart; date <= dateEnd; date = date.AddMonths(1))
            {
                var i = date.Year;
                var j = date.Month;

                if (date == dateStart && j < balForecastBLL.Month_From)
                {
                    date = date.AddMonths(1);
                    i = date.Year;
                    j = date.Month;
                    date = new DateTime(i, j, 1);
                }

                month = j + "/" + i;

                dt.Columns.Add(month + text.str_EstBalance, typeof(float));

            }
            return dt;
        }

        private DataTable NEW_RearrangeIndex(DataTable dataTable)
        {
            Text text = new Text();
            int index = 1;

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    row[text.Header_Index] = index++;
                }
            }

            return dataTable;
        }

        public DataTable NEW_RearrangeIndex(DataTable dataTable, string headerName)
        {
            int index = 1;

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    row[headerName] = index ++;
                }
            }

            return dataTable;
        }

        private float DeliveredToCustomerQty(string itemCode, DateTime dateFrom, DateTime dateTo)
        {
            Text text = new Text();
            float DeliveredQty = 0;

            DT_PART_TRANSFER.AcceptChanges();

            bool itemFound = false;

            foreach (DataRow trfHistRow in DT_PART_TRANSFER.Rows)
            {
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();
                DateTime trfDate = DateTime.TryParse(trfHistRow[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                bool dateMatched = trfDate >= dateFrom;
                dateMatched &= trfDate <= dateTo;

                if (itemFound && itemCode != trfHistRow[dalTrfHist.TrfItemCode].ToString())
                {
                    break;
                }

                if (itemCode == trfHistRow[dalTrfHist.TrfItemCode].ToString() && result == text.Passed && dateMatched)
                {
                    itemFound = true;

                    int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                    DeliveredQty += qty;
                    trfHistRow.Delete();
                }
            }

            DT_PART_TRANSFER.AcceptChanges();

            return DeliveredQty;
        }

        private DataTable New_PrintForecastSummary()
        {
            Text text = new Text();
            DataTable dt_MaterialList = new DataTable();

            if (DT_MATERIAL_FORECAST_SUMMARY != null)
            {
                string Material_Type = balForecastBLL.Item_Type;

                dt_MaterialList = DT_MATERIAL_FORECAST_SUMMARY.Clone();

                if (Material_Type.ToUpper() != text.Cmb_All)
                {
                    foreach (DataRow row in DT_MATERIAL_FORECAST_SUMMARY.Rows)
                    {
                        string matType = row[text.Header_Type].ToString();
                        string stock = row[text.Header_ReadyStock].ToString();

                        if (matType == Material_Type && stock != "-1")
                        {
                            dt_MaterialList.ImportRow(row);
                        }
                    }
                }
                else
                {
                    dt_MaterialList = DT_MATERIAL_FORECAST_SUMMARY.Copy();

                }

                if (dt_MaterialList.Rows.Count > 0)
                {

                    dt_MaterialList.DefaultView.Sort = text.Header_PartName + " ASC";
                    dt_MaterialList = dt_MaterialList.DefaultView.ToTable();

                    dt_MaterialList = NEW_RearrangeIndex(dt_MaterialList);

                    //dgvAlertSummary.DataSource = dt_MaterialList;
                    //dgvUIEdit(dgvAlertSummary);
                    //dgvAlertSummary.ClearSelection();
                }
            }
            return dt_MaterialList;
        }

        private void GetChildFromGroup(DataTable dt_Join, DataTable dt_Item, string parentCode, int parentIndex, int groupLevel, int childIndex)
        {
            Text text = new Text();
            //search child

            foreach (DataRow join in dt_Join.Rows)
            {
                if (parentCode == join[dalJoin.ParentCode].ToString())
                {
                    string childCode = join[dalJoin.ChildCode].ToString();

                    float joinQty = float.TryParse(join[dalJoin.JoinQty].ToString(), out float i) ? i : 1;
                    float joinMax = float.TryParse(join[dalJoin.JoinMax].ToString(), out i) ? i : 1;
                    float joinMin = float.TryParse(join[dalJoin.JoinMin].ToString(), out i) ? i : 1;
                    float joinWastage = float.TryParse(join[dalJoin.JoinWastage].ToString(), out i) ? i : 1;

                    foreach (DataRow item in dt_Item.Rows)
                    {
                        if (childCode == item[dalItem.ItemCode].ToString())
                        {
                            #region add child to table

                            DataRow newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                            string itemCode = item[dalItem.ItemCode].ToString();
                            string itemName = item[dalItem.ItemName].ToString();
                            string itemType = item[dalItem.ItemCat].ToString();
                            float Child_Stock = float.TryParse(item[dalItem.ItemStock].ToString(), out Child_Stock) ? Child_Stock : 0;
                            float pendingOrder = float.TryParse(item[dalItem.ItemOrd].ToString(), out pendingOrder) ? pendingOrder : 0;
                            float Color_Rate = float.TryParse(item[dalItem.ItemMBRate].ToString(), out Color_Rate) ? Color_Rate : 0;
                            string itemUnit = item[dalItem.ItemUnit].ToString();

                            if (balForecastBLL.ZeroCost_Stock && itemType != text.Cat_Part)
                            {
                                Child_Stock = getPMMAQtyFromDataTable(dt_Item, itemCode);
                            }

                            newRow[text.Header_Index] = childIndex;
                            newRow[text.Header_ParentIndex] = parentIndex;
                            newRow[text.Header_GroupLevel] = groupLevel + 1;
                            newRow[text.Header_Type] = itemType;
                            newRow[text.Header_PartCode] = itemCode;
                            newRow[text.Header_PartName] = itemName;
                            newRow[text.Header_JoinQty] = joinQty;
                            newRow[text.Header_JoinMax] = joinMax;
                            newRow[text.Header_JoinMin] = joinMin;
                            newRow[text.Header_ReadyStock] = Child_Stock;
                            newRow[text.Header_PendingOrder] = pendingOrder;
                            newRow[text.Header_ColorRate] = Color_Rate;

                            #region add item weight info

                            float partWeight = 0;
                            float runnerWeight = 0;

                            if (balForecastBLL.ZeroCost_Material)
                            {
                                partWeight = float.TryParse(item[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                runnerWeight = float.TryParse(item[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                                if (partWeight <= 0)
                                {
                                    partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                    runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                                }

                            }
                            else
                            {
                                partWeight = float.TryParse(item[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                                runnerWeight = float.TryParse(item[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                            }

                            float itemWeight = partWeight + runnerWeight;

                            float wastage = item[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(item[dalItem.ItemWastage]);

                            newRow[text.Header_PartWeight_G] = partWeight;
                            newRow[text.Header_RunnerWeight_G] = runnerWeight;
                            newRow[text.Header_WastageAllowed_Percentage] = wastage;
                            newRow[text.Header_Unit] = itemUnit;

                            #endregion

                            DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                            ChildQtyCalculation(childIndex);

                            #endregion

                            int subChildIndex = childIndex * 100 + 1;

                            #region add material

                            string RawMaterial = item[dalItem.ItemMaterial].ToString();
                            string ColorMaterial = item[dalItem.ItemMBatch].ToString();

                            if (!string.IsNullOrEmpty(RawMaterial))
                            {
                                newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                                newRow[text.Header_Index] = subChildIndex++;
                                newRow[text.Header_ParentIndex] = childIndex;
                                newRow[text.Header_GroupLevel] = groupLevel + 2;
                                newRow[text.Header_Type] = text.Cat_RawMat;
                                newRow[text.Header_PartCode] = RawMaterial;
                                newRow[text.Header_PartName] = getItemNameFromDataTable(dt_Item, RawMaterial);

                                if (RawMaterial == "ABS 450Y MH1")
                                {
                                    float TEST = 0;
                                }

                                if (balForecastBLL.ZeroCost_Stock)
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);
                                }
                                else
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)getStockQtyFromDataTable(dt_Item, RawMaterial), 2);
                                }

                                newRow[text.Header_PendingOrder] = (float)Math.Round((double)getOrderQtyFromDataTable(dt_Item, RawMaterial), 2);
                                newRow[text.Header_Unit] = text.Unit_KG;

                                DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                                ChildQtyCalculation(subChildIndex - 1);

                            }

                            if (!string.IsNullOrEmpty(ColorMaterial))
                            {
                                newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                                newRow[text.Header_Index] = subChildIndex++;
                                newRow[text.Header_ParentIndex] = childIndex;
                                newRow[text.Header_GroupLevel] = groupLevel + 2;
                                newRow[text.Header_Type] = getCatNameFromDataTable(dt_Item, ColorMaterial);
                                newRow[text.Header_PartCode] = ColorMaterial;
                                newRow[text.Header_PartName] = getItemNameFromDataTable(dt_Item, ColorMaterial);

                                if (balForecastBLL.ZeroCost_Stock)
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                }
                                else
                                {
                                    newRow[text.Header_ReadyStock] = (float)Math.Round((double)getStockQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                }

                                newRow[text.Header_PendingOrder] = (float)Math.Round((double)getOrderQtyFromDataTable(dt_Item, ColorMaterial), 2);
                                newRow[text.Header_Unit] = text.Unit_KG;

                                DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                                ChildQtyCalculation(subChildIndex - 1);


                            }

                            #endregion

                            GetChildFromGroup(dt_Join, dt_Item, childCode, childIndex, groupLevel + 1, subChildIndex);

                            childIndex++;

                            break;
                        }


                    }

                }
            }

        }

        private float getItemForecast(string itemCode, int year, int month)
        {
            float forecast = -1;
            Text text = new Text();
            dt_ItemForecast.AcceptChanges();

            foreach (DataRow row in dt_ItemForecast.Rows)
            {
                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                bool itemMatch = code == itemCode && yearData == year && monthData == month;

                if (itemMatch)
                {
                    forecast = float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i) ? i : -1;
                    row.Delete();
                    break;
                }
            }

            dt_ItemForecast.AcceptChanges();

            return forecast;
        }

        private void ForecastDataFilter(int yearFrom, int yearTo)
        {
            dt_ItemForecast.AcceptChanges();

            foreach (DataRow row in dt_ItemForecast.Rows)
            {
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());

                if (yearData < yearFrom || yearData > yearTo)
                {
                    row.Delete();
                }
            }

            dt_ItemForecast.AcceptChanges();
        }

        private void ChildQtyCalculation(int childIndex)
        {
            Text text = new Text();
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
                foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                {
                    if (childIndex.ToString() == row[text.Header_Index].ToString())
                    {
                        string parentIndex = row[text.Header_ParentIndex].ToString();

                        float Ready_Stock = float.TryParse(row[text.Header_ReadyStock].ToString(), out Ready_Stock) ? Ready_Stock : 0;

                        for (int i = 0; i < DT_PRODUCT_FORECAST_SUMMARY.Rows.Count; i++)
                        {
                            if (DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_Index].ToString() == parentIndex)
                            {
                                for (int j = 0; j < DT_PRODUCT_FORECAST_SUMMARY.Columns.Count; j++)
                                {
                                    string colName = DT_PRODUCT_FORECAST_SUMMARY.Columns[j].ColumnName;

                                    bool colFound = colName.Contains(text.str_Forecast);
                                    colFound |= colName.Contains(text.str_Delivered);
                                    colFound |= colName.Contains(text.str_RequiredQty);
                                    colFound |= colName.Contains(text.str_InsufficientQty);

                                    if (colFound)
                                    {
                                        float parentQty = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][j].ToString(), out parentQty) ? parentQty : 0;
                                        float wastage = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_WastageAllowed_Percentage].ToString(), out wastage) ? wastage : 0;

                                        string childType = row[text.Header_Type].ToString();

                                        float childQty;

                                        if (childType == text.Cat_RawMat || childType == text.Cat_MB || childType == text.Cat_Pigment)
                                        {
                                            float partWeight = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                                            float runnerWeight = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;
                                            float colorRate = float.TryParse(DT_PRODUCT_FORECAST_SUMMARY.Rows[i][text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;

                                            float itemWeight = (partWeight + runnerWeight) / 1000;

                                            childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * (1 + wastage)), 3);

                                            if (childType == text.Cat_MB || childType == text.Cat_Pigment)
                                            {
                                                childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * colorRate * (1 + wastage)), 3);
                                            }
                                        }
                                        else
                                        {
                                            float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                                            float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 0;
                                            float joinWastage = float.TryParse(row[text.Header_JoinWastage].ToString(), out joinWastage) ? joinWastage : 0;

                                            joinMax = joinMax <= 0 ? 1 : joinMax;

                                            if (childType == text.Cat_Part)
                                            {
                                                childQty = parentQty / joinMax * joinQty;

                                            }
                                            else
                                            {
                                                childQty = (float)Math.Ceiling(parentQty / joinMax * joinQty * (1 + joinWastage));

                                            }
                                        }

                                        row[colName] = childQty;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }

                }
        }

        public Tuple<int,float,float,float,float,bool,bool> loadMaterialPlanningSummary(string Material)
        {
            DataTable dt;

            dt = dalPlanning.SelectActivePlanning();

            float TotalMatPlannedToUse = 0;
            float TotalMatUsed = 0;
            float TotalMatToUse = 0;
            int planCounter = 0;

            bool rawType = false;
            bool colorType = false;

            if (dt != null)
                foreach (DataRow row in dt.Rows)
                {
                    string rawMat = row[dalPlanning.materialCode].ToString();
                    string colorMat = row[dalPlanning.colorMaterialCode].ToString();
                    float matQty = 0;
                    float ableProduce = float.TryParse(row[dalPlanning.ableQty].ToString(), out float i) ? i : 0;
                    float ProducedQty = float.TryParse(row[dalPlanning.planProduced].ToString(), out i) ? i : 0;

                    if (Material.Equals(rawMat))
                    {
                        float MatBag = float.TryParse(row[dalPlanning.materialBagQty].ToString(), out i) ? i : 0;
                        matQty = MatBag * 25;



                        float toProduceQty = ableProduce - ProducedQty;

                        if (toProduceQty < 0)
                        {
                            toProduceQty = 0;
                        }

                        float matused_perPcs = matQty / ableProduce;

                        float matUsed = matused_perPcs * ProducedQty;
                        float matToUse = matused_perPcs * toProduceQty;

                        TotalMatUsed += matUsed;
                        TotalMatToUse += matToUse;

                        rawType = true;

                        planCounter++;

                    }
                    else if (Material.Equals(colorMat))
                    {
                        matQty = float.TryParse(row[dalPlanning.colorMaterialQty].ToString(), out i) ? i : 0;


                        float toProduceQty = ableProduce - ProducedQty;

                        if (toProduceQty < 0)
                        {
                            toProduceQty = 0;
                        }

                        float matused_perPcs = matQty / ableProduce;

                        float matUsed = matused_perPcs * ProducedQty;
                        float matToUse = matused_perPcs * toProduceQty;

                        TotalMatUsed += matUsed;
                        TotalMatToUse += matToUse;

                        colorType = true;

                        planCounter++;

                    }


                    TotalMatPlannedToUse += matQty;

                }

            //get stock qty
            float stock = getStockQtyFromDataTable(dalItem.Select(), Material);

            TotalMatPlannedToUse = (float)Math.Floor(TotalMatPlannedToUse * 1000) / 1000;
            TotalMatUsed = (float)Math.Floor(TotalMatUsed * 1000) / 1000;
            TotalMatToUse = (float)Math.Floor(TotalMatToUse * 1000) / 1000;
            stock = (float)Math.Floor(stock * 1000) / 1000;


            return Tuple.Create(planCounter, TotalMatPlannedToUse, TotalMatUsed, TotalMatToUse, stock,rawType,colorType);
        }

        public DataTable GetSummaryForecastMatUsedData(BalForecastBLL u)
        {
            #region data setting 

            Text text = new Text();

            DT_PRODUCT_FORECAST_SUMMARY = null;
            DT_MATERIAL_FORECAST_SUMMARY = null;
            dt_ItemForecast = null;
            DT_PART_TRANSFER = null;
            DT_STOCK_LIST = null;

            balForecastBLL = u;

            string from = u.Date_From.ToString("yyyy/MM/dd");
            string to = u.Date_To.ToString("yyyy/MM/dd");

            DataTable dt_PartTrfHist;
            DataTable dt_CustProduct;

            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();

            //get all item info
            dt_Item = dalItem.Select();

            bool PMMACustomer = false;

            if (u.Cust_Name.Equals("All"))
            {
                dt_CustProduct = dalItemCust.Select();
                dt_PartTrfHist = dalTrfHist.rangeItemToAllCustomerSearch(from, to);
                dt_ItemForecast = dalItemForecast.Select();
            }
            else
            {
                dt_CustProduct = dalItemCust.custSearch(u.Cust_Name);
                dt_PartTrfHist = dalTrfHist.rangeItemToCustomerSearch(u.Cust_Name, from, to);
                dt_ItemForecast = dalItemForecast.Select(u.Cust_ID.ToString());

                PMMACustomer = u.Cust_Name.Equals(getCustName(1));

            }

            DT_PART_TRANSFER = dt_PartTrfHist.Copy();

            dt_ItemForecast.DefaultView.Sort = dalItemForecast.ItemCode + " ASC, " + dalItemForecast.ForecastYear + " DESC, " + dalItemForecast.ForecastMonth + " DESC";
            dt_ItemForecast = dt_ItemForecast.DefaultView.ToTable();

            DT_PART_TRANSFER.DefaultView.Sort = dalTrfHist.TrfItemCode + " ASC, " + dalTrfHist.TrfDate + " ASC";
            DT_PART_TRANSFER = DT_PART_TRANSFER.DefaultView.ToTable();

            dt_CustProduct = RemoveTerminatedProduct(dt_CustProduct);
            dt_CustProduct.DefaultView.Sort = dalItem.ItemCode + " ASC";
            dt_CustProduct = dt_CustProduct.DefaultView.ToTable();


            DateTime dateStart = new DateTime(u.Year_From, u.Month_From, 1);
            DateTime dateEnd = new DateTime(u.Year_To, u.Month_To, 1);

            if (dateStart > dateEnd)
            {
                int tmp;

                tmp = u.Year_From;
                u.Year_From = u.Year_To;
                u.Year_To = tmp;

                tmp = u.Month_From;
                u.Month_From = u.Month_To;
                u.Month_To = tmp;

                balForecastBLL = u;
            }

            string MonthlyForecast;
            string MonthlyDelivered;
            string MonthlyRequired;
            string MonthlyInsufficient;
            string MonthlyBalance;

            DataTable dt_PMMA_Date = dalPmmaDate.Select();

            DT_PRODUCT_FORECAST_SUMMARY = Product_SummaryDataTable();

            if (PMMACustomer)
            {
                dateStart = GetPMMAStartDate(u.Month_From, u.Year_From, dt_PMMA_Date);
                dateEnd = GetPMMAEndDate(u.Month_To, u.Year_To, dt_PMMA_Date);
            }
            else
            {
                dateStart = new DateTime(u.Year_From, u.Month_From, 1);
                dateEnd = new DateTime(u.Year_To, u.Month_To, DateTime.DaysInMonth(u.Year_To, u.Month_To));
            }

            ForecastDataFilter(u.Year_From, u.Year_To);

            #endregion

            int index = 1;

            foreach (DataRow ProductRow in dt_CustProduct.Rows)
            {
                DataRow newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                string ProductCode = ProductRow[dalItem.ItemCode].ToString();
                string ProductName = ProductRow[dalItem.ItemName].ToString();
                string itemUnit = ProductRow[dalItem.ItemUnit].ToString();
                float Ready_Stock = float.TryParse(ProductRow[dalItem.ItemStock].ToString(), out Ready_Stock) ? Ready_Stock : 0;
                float Pending_Order = float.TryParse(ProductRow[dalItem.ItemOrd].ToString(), out Pending_Order) ? Pending_Order : 0;
                float Color_Rate = float.TryParse(ProductRow[dalItem.ItemMBRate].ToString(), out Color_Rate) ? Color_Rate : 0;
                float Forecast = 0;
                float Delivered = 0;
                float Required = 0;
                float Insufficient = 0;

                newRow[text.Header_Index] = index;
                newRow[text.Header_ParentIndex] = 0;
                newRow[text.Header_GroupLevel] = 1;
                newRow[text.Header_Type] = ProductRow[dalItem.ItemCat].ToString();
                newRow[text.Header_PartCode] = ProductCode;
                newRow[text.Header_PartName] = ProductName;
                newRow[text.Header_ReadyStock] = Ready_Stock;
                newRow[text.Header_PendingOrder] = Pending_Order;
                newRow[text.Header_ColorRate] = Color_Rate;

                for (var date = dateStart; date <= dateEnd; date = date.AddMonths(1))
                {
                    var i = date.Year;
                    var j = date.Month;

                    if (date == dateStart && j < u.Month_From)
                    {
                        date = date.AddMonths(1);
                        i = date.Year;
                        j = date.Month;
                        date = new DateTime(i, j, 1);
                    }

                    MonthlyForecast = j + "/" + i + text.str_Forecast;
                    MonthlyDelivered = j + "/" + i + text.str_Delivered;
                    MonthlyRequired = j + "/" + i + text.str_RequiredQty;
                    MonthlyInsufficient = j + "/" + i + text.str_InsufficientQty;
                    MonthlyBalance = j + "/" + i + text.str_EstBalance;

                    DateTime dateFrom, dateTo;

                    if (PMMACustomer)
                    {
                        dateFrom = GetPMMAStartDate(j, i, dt_PMMA_Date);
                        dateTo = GetPMMAEndDate(j, i, dt_PMMA_Date);
                    }
                    else
                    {
                        dateFrom = new DateTime(i, j, 1);
                        dateTo = new DateTime(i, j, DateTime.DaysInMonth(i, j));
                    }

                    bool colFound = DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyForecast);
                    colFound &= DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyDelivered);
                    colFound &= DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyRequired);
                    colFound &= DT_PRODUCT_FORECAST_SUMMARY.Columns.Contains(MonthlyInsufficient);

                    if (colFound)
                    {
                        Forecast = getItemForecast(ProductCode, i, j);

                        Forecast = Forecast < 0 ? 0 : Forecast;

                        if (u.Deduct_Delivered)
                        {
                            Delivered = DeliveredToCustomerQty(ProductCode, dateFrom, dateTo);
                            newRow[MonthlyDelivered] = Delivered;
                        }

                        Required = Forecast - Delivered;

                        Required = Required < 0 ? 0 : Required;

                        Insufficient = Ready_Stock - Required;



                        newRow[MonthlyForecast] = Forecast;
                        newRow[MonthlyRequired] = Required;

                        newRow[MonthlyBalance] = Insufficient;

                        Insufficient = Insufficient > 0 ? 0 : Insufficient;
                        newRow[MonthlyInsufficient] = Insufficient;

                    }
                }

                #region add item weight info

                float partWeight = 0;
                float runnerWeight = 0;

                if (u.ZeroCost_Material)
                {
                    partWeight = float.TryParse(ProductRow[dalItem.ItemQuoPWPcs].ToString(), out partWeight) ? partWeight : 0;
                    runnerWeight = float.TryParse(ProductRow[dalItem.ItemQuoRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;

                    if (partWeight <= 0)
                    {
                        partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                        runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                    }

                }
                else
                {
                    partWeight = float.TryParse(ProductRow[dalItem.ItemProPWPcs].ToString(), out partWeight) ? partWeight : 0;
                    runnerWeight = float.TryParse(ProductRow[dalItem.ItemProRWPcs].ToString(), out runnerWeight) ? runnerWeight : 0;
                }

                float itemWeight = partWeight + runnerWeight;

                float wastage = ProductRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(ProductRow[dalItem.ItemWastage]);

                newRow[text.Header_PartWeight_G] = partWeight;
                newRow[text.Header_RunnerWeight_G] = runnerWeight;
                newRow[text.Header_WastageAllowed_Percentage] = wastage;
                newRow[text.Header_Unit] = itemUnit;

                #endregion

                DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                #region add material

                string RawMaterial = ProductRow[dalItem.ItemMaterial].ToString();
                string ColorMaterial = ProductRow[dalItem.ItemMBatch].ToString();

                int childIndex = index * 1000 + 1;

                if (!string.IsNullOrEmpty(RawMaterial))
                {
                    newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                    newRow[text.Header_Index] = childIndex++;
                    newRow[text.Header_ParentIndex] = index;
                    newRow[text.Header_GroupLevel] = 2;
                    newRow[text.Header_Type] = text.Cat_RawMat;
                    newRow[text.Header_PartCode] = RawMaterial;
                    newRow[text.Header_PartName] = getItemNameFromDataTable(dt_Item, RawMaterial);


                    if (u.ZeroCost_Stock)
                    {
                        newRow[text.Header_ReadyStock] = (float)Math.Round((double)getPMMAQtyFromDataTable(dt_Item, RawMaterial), 2);
                    }
                    else
                    {
                        newRow[text.Header_ReadyStock] = (float)Math.Round((double)getStockQtyFromDataTable(dt_Item, RawMaterial), 2);
                    }

                    newRow[text.Header_PendingOrder] = (float)Math.Round((double)getOrderQtyFromDataTable(dt_Item, RawMaterial), 2);

                    newRow[text.Header_Unit] = text.Unit_KG;

                    DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);

                    ChildQtyCalculation(childIndex - 1);
                }

                if (!string.IsNullOrEmpty(ColorMaterial))
                {
                    newRow = DT_PRODUCT_FORECAST_SUMMARY.NewRow();

                    newRow[text.Header_Index] = childIndex++;
                    newRow[text.Header_ParentIndex] = index;
                    newRow[text.Header_GroupLevel] = 2;
                    newRow[text.Header_Type] = getCatNameFromDataTable(dt_Item, ColorMaterial);
                    newRow[text.Header_PartCode] = ColorMaterial;
                    newRow[text.Header_PartName] = getItemNameFromDataTable(dt_Item, ColorMaterial);

                    if (u.ZeroCost_Stock)
                    {
                        newRow[text.Header_ReadyStock] = (float)Math.Round((double)getPMMAQtyFromDataTable(dt_Item, ColorMaterial), 2);
                    }
                    else
                    {
                        newRow[text.Header_ReadyStock] = (float)Math.Round((double)getStockQtyFromDataTable(dt_Item, ColorMaterial), 2);
                    }

                    newRow[text.Header_PendingOrder] = (float)Math.Round((double)getOrderQtyFromDataTable(dt_Item, ColorMaterial), 2);
                    newRow[text.Header_Unit] = text.Unit_KG;


                    DT_PRODUCT_FORECAST_SUMMARY.Rows.Add(newRow);
                    ChildQtyCalculation(childIndex - 1);

                }

                #endregion

                #region add child group

                GetChildFromGroup(dt_Join, dt_Item, ProductCode, index, 1, childIndex);

                #endregion

                index++;
            }

            //index marking
            DT_PRODUCT_FORECAST_SUMMARY = IndexMarking(DT_PRODUCT_FORECAST_SUMMARY);

            //stock deduct
            if (u.Deduct_Stock)//1873ms
            {
                StockDeductCalculation();
                DT_PRODUCT_FORECAST_SUMMARY.DefaultView.Sort = text.Header_IndexMarking + " ASC";
                DT_PRODUCT_FORECAST_SUMMARY = DT_PRODUCT_FORECAST_SUMMARY.DefaultView.ToTable();

            }

            //material & child Item merge & balance calculation
            ChildItemMergeAndBalCalculation();

            return New_PrintForecastSummary();


        }

        private DataTable IndexMarking(DataTable dt)
        {
            Text text = new Text();
            if (dt != null && dt.Columns.Contains(text.Header_IndexMarking))
            {
                int indexMarking = 1;

                foreach (DataRow row in dt.Rows)
                {
                    row[text.Header_IndexMarking] = indexMarking++;
                }
            }

            return dt;
        }

        private float GetLatestBalStock(int CurrentLevel, string itemCode, float BalStock)
        {
            Text text = new Text();
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                if (CurrentLevel > 1)
                {
                    CurrentLevel--;
                }

                bool LevelItemFound = false;

                foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                {
                    string looping_ItemCode = row[text.Header_PartCode].ToString();
                    int looping_Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out int i) ? i : 0;

                    if (looping_Level == CurrentLevel && looping_ItemCode == itemCode)
                    {
                        LevelItemFound = true;
                        float StockTmp = float.TryParse(row[text.Header_BalStock].ToString(), out StockTmp) ? StockTmp : 0;

                        if (StockTmp < BalStock)
                        {
                            BalStock = StockTmp;
                        }

                    }
                }

                if (!LevelItemFound && CurrentLevel > 1)
                {
                    BalStock = GetLatestBalStock(CurrentLevel - 1, itemCode, BalStock);
                }
                else if (!LevelItemFound && CurrentLevel == 1)
                {
                    foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                    {
                        if (row[text.Header_PartCode].ToString() == itemCode)
                        {
                            BalStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out BalStock) ? BalStock : 0;

                            break;
                        }
                    }
                }
            }

            return BalStock;

        }

        private float GetLatestBalStock(string itemCode)
        {
            Text text = new Text();
            float balStock = 0;

            if (DT_STOCK_LIST != null)
            {

                foreach (DataRow row in DT_STOCK_LIST.Rows)
                {
                    if (itemCode == row[text.Header_PartCode].ToString())
                    {
                        return float.TryParse(row[text.Header_BalStock].ToString(), out balStock) ? balStock : 0;
                    }
                }

                //if item not found in stock list
                if (DT_PRODUCT_FORECAST_SUMMARY != null)
                    foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                    {
                        if (itemCode == row[text.Header_PartCode].ToString())
                        {
                            balStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out balStock) ? balStock : 0;
                            balStock = balStock == -1 ? 0 : balStock;

                            UpdateLatestBalStock(itemCode, balStock);
                            break;
                        }
                    }
            }

            return balStock;
        }

        private void UpdateLatestBalStock(string itemCode, float balStock)
        {
            Text text = new Text();
            if (DT_STOCK_LIST == null)
            {
                DT_STOCK_LIST = New_StockList_DataTable();
            }


            foreach (DataRow row in DT_STOCK_LIST.Rows)
            {
                if (itemCode == row[text.Header_PartCode].ToString())
                {
                    row[text.Header_BalStock] = balStock;
                    return;
                }
            }

            //if data not found
            DataRow NewRow = DT_STOCK_LIST.NewRow();

            NewRow[text.Header_PartCode] = itemCode;
            NewRow[text.Header_BalStock] = balStock;
            DT_STOCK_LIST.Rows.Add(NewRow);

        }

        private void UpdateMonthy(string itemCode, float balStock, string Month)
        {
            Text text = new Text();
            if (DT_STOCK_LIST == null)
            {
                DT_STOCK_LIST = New_StockList_DataTable();
            }


            foreach (DataRow row in DT_STOCK_LIST.Rows)
            {
                if (itemCode == row[text.Header_PartCode].ToString())
                {
                    row[text.Header_BalStock] = balStock;


                    row[Month + text.str_EstBalance] = balStock;

                    return;
                }
            }

            //if data not found
            DataRow NewRow = DT_STOCK_LIST.NewRow();

            NewRow[text.Header_PartCode] = itemCode;
            NewRow[text.Header_BalStock] = balStock;
            NewRow[Month + text.str_EstBalance] = balStock;
            DT_STOCK_LIST.Rows.Add(NewRow);

        }
        private void StockDeductCalculation()//Level 1 only
        {
            Text text = new Text();
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {

                if (DT_STOCK_LIST == null)
                {
                    DT_STOCK_LIST = New_StockList_DataTable();
                }

                float Bal_Stock = 0;
                string Last_PartCode = null;

                DT_PRODUCT_FORECAST_SUMMARY.DefaultView.Sort = text.Header_Type + " ASC," + text.Header_PartCode + " ASC," + text.Header_GroupLevel + " ASC";
                DT_PRODUCT_FORECAST_SUMMARY = DT_PRODUCT_FORECAST_SUMMARY.DefaultView.ToTable();
                int Level_Checking = 1;

                foreach (DataColumn col in DT_PRODUCT_FORECAST_SUMMARY.Columns)
                {
                    string ColName = col.ColumnName;

                    if (ColName.Contains(text.str_Forecast))
                    {
                        foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                        {
                            string itemType = row[text.Header_Type].ToString();

                            if (itemType == text.Cat_Part)
                            {
                                int Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out Level) ? Level : 0;
                                string itemCode = row[text.Header_PartCode].ToString();

                                if (Last_PartCode != itemCode)
                                {
                                    Last_PartCode = itemCode;
                                    Bal_Stock = GetLatestBalStock(itemCode);
                                }

                                if (Level == Level_Checking && Last_PartCode == itemCode)
                                {
                                    float Required = float.TryParse(row[ColName.Replace(text.str_Forecast, text.str_RequiredQty)].ToString(), out float j) ? j : 0;
                                    float Insufficient = 0;

                                    Bal_Stock -= Required;

                                    row[ColName.Replace(text.str_Forecast, text.str_EstBalance)] = Bal_Stock;

                                    if (Bal_Stock < 0)
                                    {
                                        Insufficient = (float)Bal_Stock;
                                        Bal_Stock = 0;
                                    }
                                    else
                                    {
                                        Insufficient = 0;
                                    }

                                    row[ColName.Replace(text.str_Forecast, text.str_InsufficientQty)] = Insufficient;



                                    row[text.Header_BalStock] = Bal_Stock;

                                    string Month = ColName.Replace(text.str_Forecast, "");
                                    UpdateLatestBalStock(itemCode, Bal_Stock);
                                }
                            }

                        }
                    }

                }

                //Level 2 and above
                StockDeductCalculation(Level_Checking + 1);
            }
        }

        private void StockDeductCalculation(int Level_Checking)// Level 2 above
        {
            Text text = new Text();
            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                float balStock = 0;
                string Last_PartCode = null;
                bool LevelFound = false;

                foreach (DataColumn col in DT_PRODUCT_FORECAST_SUMMARY.Columns)
                {
                    string ColName = col.ColumnName;

                    if (ColName.Contains(text.str_Forecast))
                    {
                        foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
                        {
                            string itemType = row[text.Header_Type].ToString();
                            string indexMarking = row[text.Header_Index].ToString();
                            int Level = int.TryParse(row[text.Header_GroupLevel].ToString(), out Level) ? Level : 0;
                            string itemCode = row[text.Header_PartCode].ToString();

                            if (Last_PartCode != itemCode)
                            {
                                Last_PartCode = itemCode;

                                balStock = GetLatestBalStock(itemCode);
                            }

                            if (Level == Level_Checking && Last_PartCode == itemCode)
                            {
                                LevelFound = true;

                                DataRow ParentRow = GetParentDatarow(row[text.Header_ParentIndex].ToString());

                                float Required = float.TryParse(ParentRow[ColName.Replace(text.str_Forecast, text.str_InsufficientQty)].ToString(), out float j) ? j * -1 : 0;

                                if (itemType == text.Cat_RawMat || itemType == text.Cat_MB || itemType == text.Cat_Pigment)
                                {
                                    float partWeight = float.TryParse(ParentRow[text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                                    float runnerWeight = float.TryParse(ParentRow[text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;
                                    float colorRate = float.TryParse(ParentRow[text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;
                                    float wastage = float.TryParse(ParentRow[text.Header_WastageAllowed_Percentage].ToString(), out wastage) ? wastage : 0;

                                    float itemWeight = (partWeight + runnerWeight) / 1000;

                                    if (itemType == text.Cat_MB || itemType == text.Cat_Pigment)
                                    {
                                        Required = (float)decimal.Round((decimal)(Required * itemWeight * colorRate * (1 + wastage)), 3);
                                    }
                                    else
                                    {
                                        Required = (float)decimal.Round((decimal)(Required * itemWeight * (1 + wastage)), 3);

                                    }
                                }
                                else
                                {
                                    float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                                    float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 0;
                                    float joinWastage = float.TryParse(row[text.Header_JoinWastage].ToString(), out joinWastage) ? joinWastage : 0;

                                    joinMax = joinMax <= 0 ? 1 : joinMax;

                                    if (itemType == text.Cat_Part)
                                    {
                                        Required = Required / joinMax * joinQty;

                                    }
                                    else
                                    {
                                        Required = (float)Math.Ceiling(Required / joinMax * joinQty * (1 + joinWastage));

                                    }
                                }


                                balStock -= Required;

                                row[ColName.Replace(text.str_Forecast, text.str_EstBalance)] = balStock;

                                float Insufficient = 0;


                                if (balStock < 0)
                                {
                                    Insufficient = (float)balStock;
                                    balStock = 0;
                                }
                                else
                                {
                                    Insufficient = 0;
                                }

                                string Month = ColName.Replace(text.str_Forecast, "");
                                UpdateLatestBalStock(itemCode, balStock);

                                row[ColName.Replace(text.str_Forecast, text.str_RequiredQty)] = Required;
                                row[ColName.Replace(text.str_Forecast, text.str_InsufficientQty)] = Insufficient;

                                row[text.Header_BalStock] = balStock;


                            }

                        }
                    }

                }



                if (LevelFound)
                {
                    StockDeductCalculation(Level_Checking + 1);
                }
            }
        }

        private DataRow GetParentDatarow(string ParentIndex)
        {
            Text text = new Text();
            foreach (DataRow row in DT_PRODUCT_FORECAST_SUMMARY.Rows)
            {
                if (ParentIndex == row[text.Header_Index].ToString())
                {
                    return row;
                }
            }
            return null;
        }

        private void ChildItemMergeAndBalCalculation()
        {
            Text text = new Text();
            DT_MATERIAL_FORECAST_SUMMARY = Material_SummaryDataTable();

            if (DT_PRODUCT_FORECAST_SUMMARY != null)
            {
                DataTable dt_Product = DT_PRODUCT_FORECAST_SUMMARY.Copy();

                dt_Product.DefaultView.Sort = text.Header_PartCode + " ASC";
                dt_Product = dt_Product.DefaultView.ToTable();

                string previousItemCode = null;

                DataRow newRow = DT_MATERIAL_FORECAST_SUMMARY.NewRow();

                int index = 1;

                foreach (DataRow productRow in dt_Product.Rows)
                {
                    string itemCode = productRow[text.Header_PartCode].ToString();
                    string itemName = productRow[text.Header_PartName].ToString();
                    string itemType = productRow[text.Header_Type].ToString();
                    string itemUnit = productRow[text.Header_Unit].ToString();

                    float readyStock = float.TryParse(productRow[text.Header_ReadyStock].ToString(), out readyStock) ? readyStock : 0;
                    float pendingOrder = float.TryParse(productRow[text.Header_PendingOrder].ToString(), out pendingOrder) ? pendingOrder : 0;

                    if (previousItemCode != itemCode)
                    {
                        if (previousItemCode != null)//next code found, save previous code's data to table
                        {
                            DT_MATERIAL_FORECAST_SUMMARY.Rows.Add(newRow);
                        }

                        previousItemCode = itemCode;

                        newRow = DT_MATERIAL_FORECAST_SUMMARY.NewRow();

                        newRow[text.Header_Index] = index++;
                        newRow[text.Header_Type] = itemType;
                        newRow[text.Header_ReadyStock] = readyStock;
                        newRow[text.Header_PartCode] = itemCode;
                        newRow[text.Header_PartName] = itemName;
                        newRow[text.Header_Unit] = itemUnit;
                        newRow[text.Header_PendingOrder] = pendingOrder;

                    }

                    foreach (DataColumn productCol in dt_Product.Columns)
                    {
                        string colName = productCol.ColumnName;
                        bool colFound = colName.Contains(text.str_Forecast);
                        colFound |= colName.Contains(text.str_Delivered);
                        colFound |= colName.Contains(text.str_RequiredQty);
                        colFound |= colName.Contains(text.str_InsufficientQty);

                        if (colFound)
                        {
                            float qty = float.TryParse(productRow[colName].ToString(), out qty) ? qty : 0;

                            float previous_qty = float.TryParse(newRow[colName].ToString(), out previous_qty) ? previous_qty : 0;

                            newRow[colName] = (float)decimal.Round((decimal)(qty + previous_qty), 3);

                            if (colName.Contains(text.str_RequiredQty))
                            {
                                string EstBalance_ColName = colName.Replace(text.str_RequiredQty, text.str_EstBalance);
                                float Required = qty + previous_qty;


                                readyStock = (float)decimal.Round((decimal)(readyStock - Required), 3);


                                newRow[EstBalance_ColName] = readyStock;
                            }

                        }

                    }
                }

                DT_MATERIAL_FORECAST_SUMMARY.Rows.Add(newRow);
            }
        }

        #endregion

        public DateTime GetSBBMonthlyEndDate()
        {
            int dayNow = DateTime.Now.Day;

            DateTime EndDate;

            if (dayNow <= 22)
            {
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 22);

            }
            else
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month + 1;

                if( month > 12)
                {
                    year++;
                    month = 1;
                }

                EndDate = new DateTime(year, month, 22);

            }

            return EndDate;
        }

        public float GetProductionDayNeeded(DataTable dt_Item, string itemCode, int TargetQty)
        {
            float ProDaysNeeded = 0;

            if (dt_Item != null && dt_Item.Rows.Count > 0 && !string.IsNullOrEmpty(itemCode) && TargetQty > 0)
            {
                foreach (DataRow row in dt_Item.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        int Production_cavity = int.TryParse(row[dalItem.ItemCavity].ToString(), out int k) ? k : 0;
                        int  Production_ct_sec = int.TryParse(row[dalItem.ItemProCTTo].ToString(), out k) ? k : 0;

                        if (Production_ct_sec == 0)
                        {
                            Production_ct_sec = int.TryParse(row[dalItem.ItemQuoCT].ToString(), out k) ? k : 0;
                        }

                        int TOTAL_PRODUCTION_SHOOT = (int)Math.Ceiling((double)TargetQty / Production_cavity);

                        int ADJUSTED_QTY = TOTAL_PRODUCTION_SHOOT * Production_cavity;

                        //calculate time needed
                        int TOTAL_PRODUCTION_SEC_NEEDED = TOTAL_PRODUCTION_SHOOT * Production_ct_sec;

                        int DaysNeeded = 0;
                        int balHoursNeeded = 0;

                        int totalHourNeeded = TOTAL_PRODUCTION_SEC_NEEDED / 3600;

                        int hoursPerDay = 24;

                        if (hoursPerDay > 0)
                        {
                            DaysNeeded = totalHourNeeded / hoursPerDay;
                            balHoursNeeded = totalHourNeeded % hoursPerDay;
                        }

                        ProDaysNeeded = DaysNeeded + (float)balHoursNeeded / 24;

                        ProDaysNeeded = (float)Math.Round(ProDaysNeeded, 2);

                      
                    }
                }
            }

            return ProDaysNeeded;
        }

        public float GetProductionDayNeeded(SBBDataBLL uSBB)
        {
            float ProductionDaysNeeded = 0;

            int TOTAL_PRODUCTION_SEC_NEEDED = 0;
            int TOTAL_PRODUCTION_SHOOT = 0;
            int ADJUSTED_QTY = 0;

            int Target_Qty = uSBB.Target_qty;
            int Production_CT = uSBB.Production_ct_sec;
            int Production_Cavity = uSBB.Production_cavity;

            if (Production_Cavity > 0)
            {
                double result = Math.Ceiling((double)Target_Qty / Production_Cavity);

                TOTAL_PRODUCTION_SHOOT = (int)Math.Ceiling((double)Target_Qty / Production_Cavity);

                ADJUSTED_QTY = TOTAL_PRODUCTION_SHOOT * Production_Cavity;

                uSBB.Production_TargetQty_Adjusted_BySystem = ADJUSTED_QTY;
            }

            //calculate time needed
            TOTAL_PRODUCTION_SEC_NEEDED = TOTAL_PRODUCTION_SHOOT * Production_CT;
          
            int DaysNeeded = 0;
            int balHoursNeeded = 0;

            int totalHourNeeded = TOTAL_PRODUCTION_SEC_NEEDED / 3600;

            int hoursPerDay = uSBB.Production_Hours_PerDay;

            if (hoursPerDay > 0)
            {
                DaysNeeded = totalHourNeeded / hoursPerDay;
                balHoursNeeded = totalHourNeeded % hoursPerDay;
            }

            ProductionDaysNeeded = DaysNeeded + (float)balHoursNeeded / 24;

            ProductionDaysNeeded = (float) Math.Round(ProductionDaysNeeded, 2);

            return ProductionDaysNeeded;
        }

        public void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        public bool IfPONoExist(string poNO, string custFullName)
        {
            SBBDataDAL dalSPP = new SBBDataDAL();
            DataTable dt_PO = dalSPP.POSelect();

            foreach (DataRow row in dt_PO.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    string pONoFromDB = row[dalSPP.PONo].ToString();
                    string custFullNameFromDB = row[dalSPP.FullName].ToString();

                    if (poNO == pONoFromDB && custFullName == custFullNameFromDB)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IfDONoExist(string doNO)
        {
            SBBDataDAL dalSPP = new SBBDataDAL();
            DataTable dt_DO = dalSPP.DOSelect();

            foreach(DataRow row in dt_DO.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(!isRemoved)
                {
                    string DONoFromDB = row[dalSPP.DONo].ToString();

                    if(doNO == DONoFromDB)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool IfDONoExistInRemovedDO(string doNO)
        {
            SBBDataDAL dalSPP = new SBBDataDAL();
            DataTable dt_DO = dalSPP.DOSelect();

            foreach (DataRow row in dt_DO.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(isRemoved)
                {
                    string DONoFromDB = row[dalSPP.DONo].ToString();

                    if (doNO == DONoFromDB)
                    {
                        return true;
                    }

                }
               
            }

            return false;

        }

        public int GetNewRemovedDONo()
        {
            int DoNo = -1;
            SBBDataDAL dalSPP = new SBBDataDAL();

            DataTable dt = dalSPP.DOSelect();
            dt.DefaultView.Sort = dalSPP.DONo + " ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                int number = int.TryParse(row[dalSPP.DONo].ToString(), out number) ? number : 0;

                if(number < 0)
                {
                    DoNo = number - 1;

                    if (!IfDONoExist(DoNo.ToString()))
                        return DoNo;
                }
                else
                {
                    return DoNo;
                }
            }

            return DoNo;
        }

        public int GetNewDONo()
        {
            int DoNo = 1;
            SBBDataDAL dalSPP = new SBBDataDAL();

            DataTable dt = dalSPP.DOSelect();
            dt.DefaultView.Sort = dalSPP.DONo + " DESC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    int number = int.TryParse(row[dalSPP.DONo].ToString(), out number) ? number : 0;

                    DoNo = number + 1;

                    return DoNo;
                }

            }

            return DoNo;
        }

        public int GetQtyPerBag(DataTable dt, string itemCode)
        {
            int qtyPerBag = 0;
            foreach (DataRow row in dt.Rows)
            {
                if(itemCode == row["CODE"].ToString())
                {
                    qtyPerBag = int.TryParse(row["STD_PACKING"].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                }
                
            }

            return qtyPerBag;
        }

        public Tuple<int,int> GetQtyPerBagAndPacket(DataTable dt, string itemCode)
        {
            int qtyPerBag = 0;
            int qtyPerPacket = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (itemCode == row["CODE"].ToString())
                {
                    qtyPerBag = int.TryParse(row["STD_PACKING"].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                    qtyPerPacket = int.TryParse(row["QTY_PACKET"].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                }

            }

            return Tuple.Create(qtyPerBag, qtyPerPacket);
        }

        public DateTime GetPMMAStartDate(int month, int year)
        {
            
            DataTable dt = dalPmmaDate.Select();

            DateTime date = new DateTime(year, month, 1);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateStart]);
                    }
                }
            }

            return date;
        }

        public DateTime GetPMMAMonthAndYear(DateTime trfDate)
        {
            DataTable dt = dalPmmaDate.Select();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime dateStart = Convert.ToDateTime(row[dalPmmaDate.dateStart].ToString());
                    DateTime dateEnd = Convert.ToDateTime(row[dalPmmaDate.dateEnd].ToString());

                    if(trfDate >= dateStart && trfDate <= dateEnd)
                    {
                        int month = Convert.ToInt32(row[dalPmmaDate.dateMonth].ToString());
                        int year = Convert.ToInt32(row[dalPmmaDate.dateYear].ToString());

                        return new DateTime(year, month, 1);
                    }
                   
                }
            }

            return DateTime.MaxValue;
        }

        public DateTime GetPMMAMonthAndYear(DateTime trfDate, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime dateStart = Convert.ToDateTime(row[dalPmmaDate.dateStart].ToString());
                    DateTime dateEnd = Convert.ToDateTime(row[dalPmmaDate.dateEnd].ToString());

                    if (trfDate >= dateStart && trfDate <= dateEnd)
                    {
                        int month = Convert.ToInt32(row[dalPmmaDate.dateMonth].ToString());
                        int year = Convert.ToInt32(row[dalPmmaDate.dateYear].ToString());

                        return new DateTime(year, month, 1);
                    }

                }
            }

            return DateTime.MaxValue;
        }

        public DateTime GetPMMAStartDate(int month, int year, DataTable dt)
        {
            
            if(month <= 0 || month > 12 || year <= 0)
            {
                return DateTime.MaxValue;
            }

            DateTime date = new DateTime(year, month, 1);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateStart]);
                    }
                }
            }

            return date;
        }

        public DateTime GetPMMAEndDate(int month, int year)
        {
            DataTable dt = dalPmmaDate.Select();

            var lastDayOfMonth = DateTime.DaysInMonth(year, month);

            DateTime date = new DateTime(year, month, lastDayOfMonth);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateEnd]);
                    }
                }
            }

            return date;
        }

        public DateTime GetPMMAEndDate(int month, int year, DataTable dt)
        {
            if (month <= 0 || month > 12 || year <= 0)
            {
                return DateTime.MaxValue;
            }
            var lastDayOfMonth = DateTime.DaysInMonth(year, month);

            DateTime date = new DateTime(year, month, lastDayOfMonth);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPmmaDate.dateYear].ToString() == year.ToString() && row[dalPmmaDate.dateMonth].ToString() == month.ToString())
                    {
                        date = Convert.ToDateTime(row[dalPmmaDate.dateEnd]);
                    }
                }
            }

            return date;
        }

        public int GetNextLotNo(DataTable dt, int macID)
        {
            int LotNo = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row[dalMac.MacID].ToString()) == macID)
                    {
                        LotNo = int.TryParse(row[dalMac.MacLotNo].ToString(), out LotNo)? LotNo : 1;

                        return LotNo + 1;
                    }

                }
            }

            return LotNo + 1;
        }

        public int GetPermissionLevel(int user)
        {
            userDAL dalUser = new userDAL();
            return dalUser.getPermissionLevel(user);
        }

        public string getFactoryNameFromMachineID(string machineID)
        {
            string factoryName = "";
            DataTable dt = dalMac.idSearch(machineID);

            if (dt.Rows.Count > 0)
            {
                factoryName = dt.Rows[0][dalMac.MacLocation].ToString();
            }

            return factoryName;
        }

        public string getFactoryNameFromMachineID(string machineID, DataTable dt)
        {
            string factoryName = "";

            foreach(DataRow row in dt.Rows)
            {
                if(row[dalMac.MacID].ToString() == machineID)
                {
                    factoryName = row[dalMac.MacLocation].ToString();
                }
            }
            
            return factoryName;
        }

        public void DoubleBuffered(DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public void loadItemCategoryDataToComboBox(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";

            distinctTable.AcceptChanges();
            foreach (DataRow row in distinctTable.Rows)
            {
                if (row["item_cat_name"].ToString().Equals("Mould"))
                {
                    row.Delete();
                }
            }
            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.SelectedIndex = -1;
        }

        public void loadItemCategoryDataToComboBox(ComboBox cmb,string catToShow)
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";

            distinctTable.AcceptChanges();
            foreach (DataRow row in distinctTable.Rows)
            {
                if (row["item_cat_name"].ToString().Equals("Mould"))
                {
                    row.Delete();
                }
            }
            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.Text = catToShow;

        }

        public void loadFactory(ComboBox cmb)
        {
            DataTable dt = dalFac.SelectDESC();
            DataTable lacationTable = dt.DefaultView.ToTable(true, "fac_name");

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = "fac_name";
        }

        public void loadFactoryAndAll(ComboBox cmb)
        {
            DataTable dt = dalFac.SelectDESC();
            DataTable lacationTable = dt.DefaultView.ToTable(true, "fac_name");
            lacationTable.Rows.Add("All");
            lacationTable.DefaultView.Sort = "fac_name ASC";

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = "fac_name";
            cmb.SelectedIndex = 0;

        }

        public void loadUnit(ComboBox cmb)
        {
            Text text = new Text();

            DataTable dt = new DataTable();

            string ColumnHeader = "UNIT";
            dt.Columns.Add(ColumnHeader, typeof(string));

            dt.Rows.Add(text.Unit_KG);
            dt.Rows.Add(text.Unit_g);
            dt.Rows.Add(text.Unit_Set);
            dt.Rows.Add(text.Unit_Piece);
            dt.Rows.Add(text.Unit_Meter);
            dt.Rows.Add(text.Unit_Millimetre);
            dt.Rows.Add(text.Unit_Inch);
            dt.Rows.Add(text.Unit_Bag);
            dt.Rows.Add(text.Unit_Roll);

            cmb.DataSource = dt;
            cmb.DisplayMember = ColumnHeader;
            cmb.SelectedIndex = -1;

        }

        public void loadFactoryAndAllExceptStore(ComboBox cmb)
        {
            DataTable dt = dalFac.SelectDESC();
            DataTable lacationTable = dt.DefaultView.ToTable(true, "fac_name");
            lacationTable.Rows.Add("All");
            lacationTable.DefaultView.Sort = "fac_name ASC";

            for (int i = lacationTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = lacationTable.Rows[i];
                if (dr["fac_name"].ToString() == "STORE" || dr["fac_name"].ToString() == "No.9")
                {
                    dr.Delete();
                }
                    
            }
            lacationTable.AcceptChanges();

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = "fac_name";
            cmb.SelectedIndex = 0;

        }

        public void loadOUGProductionFactory(ComboBox cmb)
        {
            DataTable dt = dalFac.SelectDESC();
            DataTable lacationTable = dt.DefaultView.ToTable(true, "fac_name", "oug_production");
           

            for (int i = lacationTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = lacationTable.Rows[i];
                bool productionFactory = bool.TryParse(dr["oug_production"].ToString(), out bool proFac) ? proFac : false;

                if (!productionFactory)
                {
                    dr.Delete();
                }

            }

            lacationTable.Rows.Add("All");
            lacationTable.DefaultView.Sort = "fac_name ASC";
            lacationTable.AcceptChanges();

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = "fac_name";
            cmb.SelectedIndex = 0;

        }
        public void loadMacIDByFactoryToComboBox(ComboBox cmb, string facName)
        {
            DataTable dt = dalMac.SelectByFactory(facName);
            DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
            cmb.SelectedIndex = -1;
        }

        public void loadOUGMacIDToComboBox(ComboBox cmb, string facName)
        {
            DataTable dt = dalMac.SelectByFactory(facName);
            DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
            cmb.SelectedIndex = -1;
        }

        public void loadSBBMacIDByFactoryToComboBox(ComboBox cmb, string facName)
        {
            DataTable dt = dalMac.SelectByFactory(facName);
            //DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            //distinctTable.DefaultView.Sort = "mac_id ASC";

            cmb.DataSource = dt;
            cmb.DisplayMember = "mac_name";
            cmb.ValueMember = "mac_id";

            cmb.SelectedIndex = -1;

        }

        public void loadMacIDToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMac.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
            cmb.SelectedIndex = -1;

        }

        public void loadOUGMacIDToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMac.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
            cmb.SelectedIndex = -1;

        }

        public void loadSBBMacIDToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMac.Select();
           
            cmb.DataSource = dt;
            cmb.DisplayMember = "mac_name";
            cmb.DisplayMember = "mac_id";
            cmb.SelectedIndex = -1;

        }

        public void loadMacIDAndAllToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMac.Select();

            DataTable distinctTable = new DataTable();
            distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
            cmb.SelectedIndex = 0;

        }

        public void loadItemNameDataToComboBox(ComboBox cmb, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.CatSearch(keywords);
                DataTable dtItemName = dt.DefaultView.ToTable(true, "item_name");

                dtItemName.DefaultView.Sort = "item_name ASC";
                cmb.DataSource = dtItemName;
                cmb.DisplayMember = "item_name";
            }
            else
            {
                cmb.DataSource = null;
            }
        }

        public void loadSPPItemNameDataToComboBox(ComboBox cmb)
        {
            DataTable dt = dalItem.SPPSearch();
            DataTable dtItemName = dt.DefaultView.ToTable(true, "item_name");

            dtItemName.DefaultView.Sort = "item_name ASC";
            cmb.DataSource = dtItemName;
            cmb.DisplayMember = "item_name";
        }

        public void loadItemCodeDataToComboBox(ComboBox cmb, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                dtItemCode.DefaultView.Sort = "item_code ASC";
                cmb.DataSource = dtItemCode;
                cmb.DisplayMember = "item_code";
            }
            else
            {
                cmb.DataSource = null;
            }
        }

        public void loadCustomerToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.FullSelect();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void loadMainCustomerAndAllToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.FullSelect();


            foreach (DataRow row in dt.Rows)
            {
                bool mainCustomer = bool.TryParse(row["cust_main"].ToString(), out bool i) ? i : false;

                if (!mainCustomer || row["cust_name"].ToString().Equals("OTHER"))
                {
                    row.Delete();
                }
            }

            dt.AcceptChanges();

            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");

            distinctTable.Rows.Add(new Text().Cmb_All);

            distinctTable.DefaultView.Sort = "cust_name ASC";

            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.Text = getCustName(1);
            //cmb.SelectedIndex = -1;
        }

        public void loadMainCustomerToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.FullSelect();
          

            foreach (DataRow row in dt.Rows)
            {
                bool mainCustomer = bool.TryParse(row["cust_main"].ToString(), out bool i) ? i : false;

                if (!mainCustomer || row["cust_name"].ToString().Equals("OTHER"))
                {
                    row.Delete();
                }
            }

            dt.AcceptChanges();

            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";

            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.Text = getCustName(1);
            //cmb.SelectedIndex = -1;
        }

        public void loadCustomerAndALLWithoutOtherToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.FullSelect();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");

            distinctTable.Rows.Add(new Text().Cmb_All);

            distinctTable.DefaultView.Sort = "cust_name ASC";

            distinctTable.AcceptChanges();
            foreach (DataRow row in distinctTable.Rows)
            {
                if (row["cust_name"].ToString().Equals("OTHER"))
                {
                    row.Delete();
                }
            }
            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void loadCustomerWithoutOtherToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.FullSelect();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";

            distinctTable.AcceptChanges();
            foreach (DataRow row in distinctTable.Rows)
            {
                if (row["cust_name"].ToString().Equals("OTHER"))
                {
                    row.Delete();
                }
            }
            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void loadRAWMaterialToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("RAW Material");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "material_code";
            cmb.SelectedIndex = -1;
        }

        public void loadMasterBatchToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("Master Batch");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            distinctTable.Rows.Add("COMPOUND");
            distinctTable.Rows.Add("NATURAL");
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "material_code";
            cmb.SelectedIndex = -1;
        }

        public void loadPigmentToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("Pigment");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            distinctTable.Rows.Add("COMPOUND");
            distinctTable.Rows.Add("NATURAL");
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "material_code";
            cmb.SelectedIndex = -1;
        }

        public void loadAllColorMatToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("Master Batch");
            DataTable dt2 = dalMaterial.catSearch("Pigment");

            DataTable dtAll = dt.Copy();
            dtAll.Merge(dt2);

            dtAll.DefaultView.Sort = "material_code ASC";

            dtAll.Rows.Add("COMPOUND");
            dtAll.Rows.Add("NATURAL");

            cmb.DataSource = dtAll;

            cmb.DisplayMember = "material_code";
            cmb.SelectedIndex = -1;
        }

        public void LoadCustomerAndAllToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.FullSelect();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = 0;
        }

        public void LoadCustomerAndAllToComboBoxSBB(ComboBox cmb)
        {
            Text text = new Text();
            DataTable dt = dalCust.FullSelect();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";

            foreach(DataRow row in distinctTable.Rows)
            {
                if(row["cust_name"].ToString().Equals(text.SPP_BrandName))
                {
                    row["cust_name"] = text.SBB_BrandName;
                }
            }
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void LoadCustomerToComboBoxSBB(ComboBox cmb)
        {
            Text text = new Text();
            DataTable dt = dalCust.FullSelect();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";

            foreach (DataRow row in distinctTable.Rows)
            {
                if (row["cust_name"].ToString().Equals(text.SPP_BrandName))
                {
                    row["cust_name"] = text.SBB_BrandName;
                }
            }
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public bool IfCustomer(DataTable dt, string keywords)
        {
            bool result = false;

            foreach (DataRow row in dt.Rows)
            {
                string customer = row["cust_name"].ToString();

                //keywords = keywords.ToUpper();
                //customer = customer.ToUpper();

                if (keywords.Equals(customer))
                {
                    return true;
                }
            }

            return result;
        }

        public bool IfCustomer(string keywords)
        {
            bool result = false;

            DataTable dt = dalCust.FullSelect();
           
            foreach(DataRow row in dt.Rows)
            {
                string customer = row["cust_name"].ToString();

                keywords = keywords.ToUpper();
                customer = customer.ToUpper();

                if (keywords.Equals(customer))
                {
                    return true;
                }
            }

            return result;
        }

        public bool IfSPPCustomer(string keywords)
        {
            bool result = false;

            SBBDataDAL dalSPP = new SBBDataDAL();

            DataTable dt = dalSPP.CustomerSelect();

            foreach (DataRow row in dt.Rows)
            {
                string customer = row[dalSPP.ShortName].ToString();

                keywords = keywords.ToUpper();
                customer = customer.ToUpper();

                if (keywords.Equals(customer))
                {
                    return true;
                }
            }

            return result;
        }

        public void LoadMaterialToComboBox(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");

            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            for (int i = distinctTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = distinctTable.Rows[i];
                if (dr["item_cat_name"].ToString() == "Part")
                {
                    distinctTable.Rows.Remove(dr);
                }
            }
            distinctTable.AcceptChanges();
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.SelectedIndex = -1;

        }

        public void LoadMaterialAndAllToComboBox(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            for (int i = distinctTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = distinctTable.Rows[i];
                if (dr["item_cat_name"].ToString() == "Part")
                {
                    distinctTable.Rows.Remove(dr);
                }
            }
            distinctTable.AcceptChanges();
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.SelectedIndex = -1;
        }

        public int min(int x, double y)
        {
            int z = Convert.ToInt32(y);

            if(x <= z)
            {
                return x;
            }
            else
            {
                return z;
            }
        }

        public float getJoinQty(string parentCode, string childCode)
        {
            float joinQty = 0;
            DataTable dt = dalJoin.loadChildList(parentCode);

            foreach(DataRow row in dt.Rows)
            {
                string itemCode = row[dalJoin.JoinChild].ToString();

                if(itemCode.Equals(childCode))
                {
                    joinQty = Convert.ToSingle(row[dalJoin.JoinQty]);
                }
            }
            return joinQty;
        }
        
        public int getFamilyWithData(int planID)
        {
            int familyWith = -1;
            DataTable dt = dalPlanning.idSearch(planID.ToString());

            foreach (DataRow row in dt.Rows)
            {
                familyWith = Convert.ToInt32(row[dalPlanning.familyWith]);
            }
            return familyWith;
        }

        public int getCustID(string custName)
        {
            string custID = "";

            DataTable dtCust = dalCust.nameSearch(custName);

            foreach (DataRow Cust in dtCust.Rows)
            {
                custID = Cust["cust_id"].ToString();
            }
            
            if(!string.IsNullOrEmpty(custID))
            {
                return Convert.ToInt32(custID);
            }
            else
            {
                return -1;
            }
            
        }

        public string getCustName(int custID)
        {
            string custName = "";

            DataTable dtCust = dalCust.idSearch(custID.ToString());

            foreach (DataRow Cust in dtCust.Rows)
            {
                custName = Cust["cust_name"].ToString();
            }

            return custName;
        }

        public string getCustName(int custID, DataTable dt_Cust)
        {
            string custName = "";

            foreach (DataRow Cust in dt_Cust.Rows)
            {
                if(custID.ToString().Equals(Cust["cust_id"].ToString()))
                {
                    custName = Cust["cust_name"].ToString();
                    return custName;
                }
            }

            return custName;
        }

        public string getItemName(string itemCode)
        {
            string itemName = "";

            DataTable dtItem = dalItem.codeSearch(itemCode);

            foreach (DataRow item in dtItem.Rows)
            {
                itemName = item["item_name"].ToString();
            }

            return itemName;
        }

        public string GetAbbreviatedFromFullName(string fullname)
        {
            DateTime month;
            return DateTime.TryParseExact(
                    fullname,
                    "MMMM",
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out month)
                ? month.ToString("MMM")
                : "ERROR";
        }

        public string getItemCat(string itemCode)
        {
            string itemCat = "";

            DataTable dtItem = dalItem.codeSearch(itemCode);

            foreach (DataRow item in dtItem.Rows)
            {
                itemCat = item["item_cat"].ToString();
            }

            return itemCat;
        }

        public DataRow getItemForecastDataRow(DataTable dt, string itemCode, int year, int month)
        {
            DataRow forecast = null;
            itemForecastDAL dalItemForecast = new itemForecastDAL();

            foreach (DataRow row in dt.Rows)
            {
                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                bool itemMatch = code == itemCode && yearData == year && monthData == month;

                if (itemMatch)
                {
                    return row;
                }
            }

            return forecast;
        }

        public int getNextMonth(int currentMonth)
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


        public Tuple<int, int,int,int,int> LoadPendingPOQty()//DataTable dt_DOList
        {
            int pendingPOQty = 0;
            int pendingCustQty = 0;

            int pendingBags = 0, pendingPkts = 0, pendingPcs = 0;

            SBBDataDAL dalSPP = new SBBDataDAL();

            DataTable dt = NewPOTable();

            //DataTable dt_POList = dalSPP.POSelect();
            DataTable dt_POList = dalSPP.POSelectWithSizeAndType();
            DataRow dt_row;

            int poCode = -1;
            int prePOCode = -1;
            int orderQty = 0;
            int deliveredQty = 0;
            int CustTblCode = -1;
            int progress = -1;
            bool dataMatched = true;
            string PONo = null, ShortName = null, FullName = null;
            string customer = null;

            DateTime PODate = DateTime.MaxValue;

            foreach (DataRow row in dt_POList.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;

                    int orderedQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderedQty) ? orderedQty : 0;
                    int delivered = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out delivered) ? delivered : 0;

                    int pendingDeliver = orderedQty - delivered;
                    int QtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out QtyPerBag) ? QtyPerBag : 1;
                    int QtyPerPkt = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out QtyPerPkt) ? QtyPerPkt : 1;

                    //if(QtyPerPkt == 0)
                    //{
                    //    QtyPerPkt = 1;

                    //}

               
                    pendingBags += pendingDeliver / QtyPerBag;

                    if (QtyPerPkt != 0)
                    {
                        pendingPkts += pendingDeliver % QtyPerBag / QtyPerPkt;
                        pendingPcs += pendingDeliver % QtyPerPkt;
                    }
                    else
                    {
                        pendingPcs += pendingDeliver % QtyPerBag;
                    }
                    

                    if (prePOCode == -1)
                    {
                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();
                        FullName = row[dalSPP.FullName].ToString();

                        CustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                    else if (prePOCode == poCode)
                    {
                        orderQty += int.TryParse(row[dalSPP.POQty].ToString(), out int temp) ? temp : 0;
                        deliveredQty += int.TryParse(row[dalSPP.DeliveredQty].ToString(), out temp) ? temp : 0;
                    }
                    else if (prePOCode != poCode)
                    {
                        progress = (int)((float)deliveredQty / orderQty * 100);
                        dataMatched = true;

                        #region PO TYPE

                        if (progress < 100 )
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        if (dataMatched)
                        {
                            dt_row = dt.NewRow();

                            dt_row[header_POCode] = prePOCode;
                            dt_row[header_PONoString] = PONo;
                            dt_row[header_PODate] = PODate;
                            dt_row[header_Customer] = ShortName;
                            dt_row[header_CustomerCode] = CustTblCode;
                            dt_row[header_Progress] = progress + "%";
                            dt_row[header_DONoString] = "";
                            dt.Rows.Add(dt_row);
                        }

                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();
                        FullName = row[dalSPP.FullName].ToString();
                        CustTblCode = int.TryParse(row[dalSPP.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                }

            }

            progress = (int)((float)deliveredQty / orderQty * 100);
            dataMatched = true;

            #region PO TYPE

            if (progress < 100)
            {
                dataMatched = dataMatched && true;
            }
            else
            {
                dataMatched = false;
            }

            #endregion

            if (dataMatched)
            {
                dt_row = dt.NewRow();

                dt_row[header_POCode] = prePOCode;
                dt_row[header_PONoString] = PONo;
                dt_row[header_PODate] = PODate;
                dt_row[header_Customer] = ShortName;
                dt_row[header_CustomerCode] = CustTblCode;
                dt_row[header_Progress] = progress + "%";
                dt_row[header_DONoString] = "";
                dt.Rows.Add(dt_row);
            }

            string previousCustCode = "";

            dt.DefaultView.Sort = header_CustomerCode + " ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                pendingPOQty++;
                string custCode = row[header_CustomerCode].ToString();
                if (previousCustCode == "")
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
                else if (previousCustCode != custCode)
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
            }

            return Tuple.Create(pendingPOQty, pendingCustQty,pendingBags,pendingPkts,pendingPcs);
        }

        public Tuple<int, int, int> LoadPendingDOQty()//DataTable dt_DOList
        {
            int pendingDOQty = 0;
            int pendingCustQty = 0;
            int totalBag = 0;
            SBBDataDAL dalSPP = new SBBDataDAL();

            DataTable dt_DOList = dalSPP.DOWithInfoSelect();

            DataTable dt = NewDOTable();
            DataRow dt_row;

            int preDOCode = -999999;

            foreach (DataRow row in dt_DOList.Rows)
            {
                int doCode = int.TryParse(row[dalSPP.DONo].ToString(), out doCode) ? doCode : -999999;

                int deliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                int bag = deliveryQty / stdPacking;

                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;

                if (deliveryQty > 0 && !isRemoved && !isDelivered)
                {
                    totalBag += bag;
                }

                if ((preDOCode == -999999 || preDOCode != doCode) && row[dalSPP.DONo] != DBNull.Value)
                {

                    if (deliveryQty > 0)
                    {
                        
                        string trfID = row[dalSPP.TrfTableCode].ToString();

                        bool dataMatched = true;
 
                        #region DO TYPE

                        if (!isDelivered )
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }
                        
                        if (isRemoved)
                        {
                            dataMatched = false;
                        }

                        #endregion

                        if (dataMatched)
                        {
                            preDOCode = doCode;
                            dt_row = dt.NewRow();

                            dt_row[header_CustomerCode] = row[dalSPP.CustTblCode];

                            dt.Rows.Add(dt_row);
                            
                        }
                    }


                }

            }


            string previousCustCode = "";

            dt.DefaultView.Sort = header_CustomerCode + " ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                pendingDOQty++;
                string custCode = row[header_CustomerCode].ToString();
                if (previousCustCode == "")
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
                else if (previousCustCode != custCode)
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
            }

            return Tuple.Create(pendingDOQty, pendingCustQty, totalBag);
        }

        public Tuple<int, int> GetPreviousBalanceClearRecord(DataTable dt_Trf, string itemCode, DateTime day, string planID, string shift, bool isBalanceIn)
        {
            dt_Trf.DefaultView.Sort = dalTrfHist.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();

            int trfTableCode = -1;
            int trfQty = -1;

            Text text = new Text();
            
            foreach (DataRow row in dt_Trf.Rows)
            {
                string status = row[dalTrfHist.TrfResult].ToString();

                if (status == "Passed")
                {
                    DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate].ToString());

                    if (trfDate < day)
                    {
                        return Tuple.Create(trfTableCode, trfQty);
                    }

                    string trfItem = row[dalTrfHist.TrfItemCode].ToString();

                    string productionInfo = row[dalTrfHist.TrfNote].ToString();
                    string _shift = "";
                    string _planID = "";
                    string balanceClearType = "";
                    bool startCopy = false;
                    bool IDCopied = false;
                    bool ShiftCopied = false;
                    

                    for (int i = 0; i < productionInfo.Length; i++)
                    {
                        if (productionInfo[i].ToString() == "P")
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && (productionInfo[i].ToString() == "O" || productionInfo[i].ToString() == "B"))
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && productionInfo[i].ToString() == "]")
                        {
                            startCopy = false;
                        }

                        if (startCopy)
                        {

                            if (char.IsDigit(productionInfo[i]))
                            {
                                IDCopied = true;
                                _planID += productionInfo[i];
                            }
                            else if (IDCopied)
                            {
                                _shift += productionInfo[i + 1];
                                IDCopied = false;
                                ShiftCopied = true;
                                startCopy = false;
                            }
                            else if(ShiftCopied)
                            {
                                balanceClearType += productionInfo[i];
                            }

                        }
                    }

                    if (_shift.ToUpper() == "M")
                    {
                        _shift = text.Shift_Morning;
                    }
                    else if (_shift.ToUpper() == "N")
                    {
                        _shift = text.Shift_Night;
                    }

                    string trfFrom = row[dalTrfHist.TrfFrom].ToString();
                    string trfTo = row[dalTrfHist.TrfTo].ToString();

                    bool dataMatch = trfDate == day && trfItem == itemCode && shift == _shift && planID == _planID;

                    if(isBalanceIn)
                    {
                        if(balanceClearType == text.Note_BalanceStockIn)
                        {
                            dataMatch &= true;

                            dataMatch &= trfFrom == text.Production || trfFrom == text.Assembly;

                            dataMatch &= IfFactoryExists(trfTo);
                        }
                        else
                        {
                            dataMatch = false;
                        }
                    }
                    else
                    {
                        if (balanceClearType == text.Note_OldBalanceStockOut)
                        {
                            dataMatch &= true;
                            dataMatch &= IfFactoryExists(trfFrom) && trfTo == text.Other;
                        }
                        else
                        {
                            dataMatch = false;
                        }
                    }

                    if (dataMatch)
                    {
                        trfTableCode = int.TryParse(row[dalTrfHist.TrfID].ToString(), out trfTableCode) ? trfTableCode : -1;
                        trfQty = int.TryParse(row[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : -1;
                    }
                }

            }

            return Tuple.Create(trfTableCode, trfQty);
        }

        public int TotalProductionStockInInOneDay(DataTable dt_Trf, string itemCode, DateTime day, string planID, string shift)
        {
            dt_Trf.DefaultView.Sort = dalTrfHist.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();

            int totalStockIn = -1;
            Text text = new Text();
            foreach(DataRow row in dt_Trf.Rows)
            {
                string status = row[dalTrfHist.TrfResult].ToString();

                if(status == "Passed")
                {
                    DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate].ToString());

                    if(trfDate < day)
                    {
                        return totalStockIn;
                    }

                    string trfItem = row[dalTrfHist.TrfItemCode].ToString();

                    #region old method
                    //string productionInfo = row[dalTrfHist.TrfNote].ToString();
                    //string _shift = "";
                    //string _planID = "";
                    //bool startCopy = false;
                    //bool IDCopied = false;

                    //for (int i = 0; i < productionInfo.Length; i++)
                    //{
                    //    if (productionInfo[i].ToString() == "P")
                    //    {
                    //        startCopy = true;
                    //    }

                    //    if (startCopy)
                    //    {

                    //        if (char.IsDigit(productionInfo[i]))
                    //        {
                    //            IDCopied = true;
                    //            _planID += productionInfo[i];
                    //        }
                    //        else if (IDCopied)
                    //        {
                    //            _shift += productionInfo[i + 1];
                    //            IDCopied = false;
                    //        }

                    //    }
                    //}

                   
                    #endregion

                    string productionInfo = row[dalTrfHist.TrfNote].ToString();
                    string _shift = "";
                    string _planID = "";
                    string balanceClearType = "";
                    bool startCopy = false;
                    bool IDCopied = false;
                    bool ShiftCopied = false;


                    for (int i = 0; i < productionInfo.Length; i++)
                    {
                        if (productionInfo[i].ToString() == "P")
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && (productionInfo[i].ToString() == "O" || productionInfo[i].ToString() == "B"))
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && productionInfo[i].ToString() == "]")
                        {
                            startCopy = false;
                        }

                        if (startCopy)
                        {

                            if (char.IsDigit(productionInfo[i]))
                            {
                                IDCopied = true;
                                _planID += productionInfo[i];
                            }
                            else if (IDCopied && i + 1 < productionInfo.Length)
                            {
                                _shift += productionInfo[i + 1];
                                IDCopied = false;
                                ShiftCopied = true;
                                startCopy = false;
                            }
                            else if (ShiftCopied)
                            {
                                balanceClearType += productionInfo[i];
                            }

                        }
                    }

                    if (_shift.ToUpper() == "M")
                    {
                        _shift = text.Shift_Morning;
                    }
                    else if (_shift.ToUpper() == "N")
                    {
                        _shift = text.Shift_Night;
                    }

                    if (trfDate == day && trfItem == itemCode && shift == _shift && planID == _planID)
                    {
                        
                        string trfFrom = row[dalTrfHist.TrfFrom].ToString();
                        string trfTo = row[dalTrfHist.TrfTo].ToString();

                        bool recordMatch = trfFrom == text.Production || trfFrom == text.Assembly;
                        recordMatch = recordMatch && IfFactoryExists(trfTo);

                        if (recordMatch)
                        {
                            if (totalStockIn == -1)
                            {
                                totalStockIn = 0;
                            }

                            int stockInQty = int.TryParse(row[dalTrfHist.TrfQty].ToString(), out stockInQty) ? stockInQty : 0;

                            totalStockIn += stockInQty;
                        }

                        if(IfFactoryExists(trfFrom) && !IfFactoryExists(trfTo) && balanceClearType == text.Note_OldBalanceStockOut)
                        {
                            if (totalStockIn == -1)
                            {
                                totalStockIn = 0;
                            }

                            totalStockIn -= int.TryParse(row[dalTrfHist.TrfQty].ToString(), out int outStock) ? outStock : 0;
                        }
                    }
                }
               
            }

            return totalStockIn;
        }

        public float getItemForecast(DataTable dt, string itemCode, int year, int month)
        {
            float forecast = -1;

            itemForecastDAL dalItemForecast = new itemForecastDAL();

            foreach(DataRow row in dt.Rows)
            {
                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                bool itemMatch = code == itemCode && yearData == year && monthData == month;

                if(itemMatch)
                {
                    return float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i)? i : -1;
                }
            }

            return forecast;
        }

        public Tuple<float,float,float> getItemForecast(DataTable dt, string itemCode, int year_1, int month_1, int year_2, int month_2, int year_3, int month_3)
        {
            float forecast_1 = -1;
            float forecast_2 = -1;
            float forecast_3 = -1;

            bool forecast_1_Found = false;
            bool forecast_2_Found = false;
            bool forecast_3_Found = false;

            itemForecastDAL dalItemForecast = new itemForecastDAL();

            foreach (DataRow row in dt.Rows)
            {
                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                float forecast = float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i) ? i : -1;

                if (code == itemCode && yearData == year_1 && monthData == month_1)
                {
                    forecast_1 = forecast;
                    forecast_1_Found = true;
                }
                else if (code == itemCode && yearData == year_2 && monthData == month_2)
                {
                    forecast_2 = forecast;
                    forecast_2_Found = true;
                }
                else if (code == itemCode && yearData == year_3 && monthData == month_3)
                {
                    forecast_3 = forecast;
                    forecast_3_Found = true;
                }

                if(forecast_1_Found && forecast_2_Found && forecast_3_Found)
                {
                    break;
                }
            }

            return Tuple.Create(forecast_1, forecast_2, forecast_3);

        }

        public Tuple<float, float, float> getCustomerItemForecast(DataTable dt, string CustomerID, string itemCode, int year_1, int month_1, int year_2, int month_2, int year_3, int month_3)
        {
            float forecast_1 = -1;
            float forecast_2 = -1;
            float forecast_3 = -1;

            bool forecast_1_Found = false;
            bool forecast_2_Found = false;
            bool forecast_3_Found = false;

            itemForecastDAL dalItemForecast = new itemForecastDAL();

            foreach (DataRow row in dt.Rows)
            {
                string DB_CustomerID = row[dalItemForecast.CustID].ToString();

                string code = row[dalItemForecast.ItemCode].ToString();
                int yearData = Convert.ToInt32(row[dalItemForecast.ForecastYear].ToString());
                int monthData = Convert.ToInt32(row[dalItemForecast.ForecastMonth].ToString());
                float forecast = float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i) ? i : -1;

                if (DB_CustomerID == CustomerID &&  code == itemCode && yearData == year_1 && monthData == month_1)
                {
                    forecast_1 = forecast;
                    forecast_1_Found = true;
                }
                else if (DB_CustomerID == CustomerID && code == itemCode && yearData == year_2 && monthData == month_2)
                {
                    forecast_2 = forecast;
                    forecast_2_Found = true;
                }
                else if (DB_CustomerID == CustomerID && code == itemCode && yearData == year_3 && monthData == month_3)
                {
                    forecast_3 = forecast;
                    forecast_3_Found = true;
                }

                if (forecast_1_Found && forecast_2_Found && forecast_3_Found)
                {
                    break;
                }
            }

            return Tuple.Create(forecast_1, forecast_2, forecast_3);

        }
        public int GetLatestTrfID()
        {
            int ID = -1;

            DataTable dt = dalTrfHist.Select();
            dt.DefaultView.Sort = dalTrfHist.TrfID + " DESC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                bool result = bool.TryParse(row[dalTrfHist.TrfResult].ToString(), out result) ? result : false;

                if (result)
                {
                    int number = int.TryParse(row[dalTrfHist.TrfID].ToString(), out number) ? number : 0;

                    ID = number;

                    return ID;
                }
            }

            return ID;
        }

        public int getFactoryID(string factoryName)
        {
            string factoryID = "";

            DataTable dtFac = dalFac.nameSearch(factoryName);

            if(dtFac.Rows.Count > 0)
            {
                foreach (DataRow fac in dtFac.Rows)
                {
                    factoryID = fac["fac_id"].ToString();
                }
                return Convert.ToInt32(factoryID);
            }
            else
            {
                return -1;
            }
        }

        public string getFactoryName(string factoryID)
        {
            string factoryName = "";

            DataTable dtFac = dalFac.idSearch(factoryID);

            if (dtFac.Rows.Count > 0)
            {
                foreach (DataRow fac in dtFac.Rows)
                {
                    factoryName = fac["fac_name"].ToString();
                }
            }

            return factoryName;
        }

        public string getCustomerName(string itemCode)
        {
            string CustomerName = "";
            
            DataTable dt = dalItemCust.checkItemCustTable(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow cust in dt.Rows)
                {
                    CustomerName = getCustName(Convert.ToInt32(cust["cust_id"].ToString()));
                }
            }

            return CustomerName;
        }

        public DataTable CopyDGVToDatatable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            return dt;
        }

        public DataTable RemoveDuplicates(DataTable dt, string columnName)
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
                        if (dt.Rows[i][columnName].ToString() == dt.Rows[j][columnName].ToString())
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

        public DataTable AddDuplicates(DataTable dt)
        {
            float jQty, iQty;
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
                        if (dt.Rows[i][headerCode].ToString() == dt.Rows[j][headerCode].ToString())
                        {
                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceZero].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceZero].ToString());
                            dt.Rows[j][headerBalanceZero] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceOne].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());
                            dt.Rows[j][headerBalanceOne] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceTwo].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceTwo].ToString());
                            dt.Rows[j][headerBalanceTwo] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceThree].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceThree].ToString());
                            dt.Rows[j][headerBalanceThree] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceFour].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceFour].ToString());
                            dt.Rows[j][headerBalanceFour] = (jQty + iQty).ToString();

                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        public float matPlanGetQty(DataTable matPlan_data, string matCode)
        {
            float qty = 0;
            Text text = new Text();

            if (matPlan_data.Rows.Count > 0)
            {
                foreach (DataRow row in matPlan_data.Rows)
                {
                    string mat_code = row[dalMatPlan.MatCode].ToString();
                    bool active = Convert.ToBoolean(row[dalMatPlan.Active]);

                    string planStatus = row[dalPlanning.planStatus].ToString();

                    active = planStatus.Equals(text.planning_status_running) || planStatus.Equals(text.planning_status_pending);

                    if (mat_code.Equals(matCode) && active)
                    {
                        float planToUse = row[dalMatPlan.PlanToUse] == null ? 0 : Convert.ToSingle(row[dalMatPlan.PlanToUse]);
                        float matUsed = row[dalMatPlan.MatUsed] == null ? 0 : Convert.ToSingle(row[dalMatPlan.MatUsed]);

                        planToUse -= matUsed;
                        if(planToUse < 0)
                        {
                            planToUse = 0;
                        }

                        qty += planToUse;
                    }
                }

            }

            return qty;
        }

        public bool matPlanAddQty(DataTable matPlan_data, string matCode, float qty)
        {
            bool successSave = false;
           
            //check if exist
            bool dataExist = false;

            foreach (DataRow row in matPlan_data.Rows)
            {
                string mat_code = row[dalMatPlan.MatCode].ToString();

                if (mat_code.Equals(matCode))
                {
                    dataExist = true;
                    float temp = row[dalMatPlan.PlanToUse] == null ? 0 : Convert.ToSingle(row[dalMatPlan.PlanToUse]);
                    qty += temp;
                }
            }

               
            uMatPlan.mat_code = matCode;
            uMatPlan.plan_to_use = qty;

            if (dataExist)
            {
                //update
                successSave = dalMatPlan.Update(uMatPlan);
            }
            else
            {
                //insert
                successSave = dalMatPlan.Insert(uMatPlan);
            }

            if (!successSave)
            {
                //Failed to insert data
                MessageBox.Show("Failed to save Material to use data");
                historyRecord("SYSTEM", "Failed to save Material to use data", DateTime.Now, MainDashboard.USER_ID);
            }
                
            

            return successSave;
        }

        public bool matPlanSubtractQty(DataTable matPlan_data, string matCode, float qty)
        {
            bool successSave = true;
            if (matPlan_data.Rows.Count > 0)
            {

                //check if exist
                bool dataExist = false;

                foreach (DataRow row in matPlan_data.Rows)
                {
                    string mat_code = row[dalMatPlan.MatCode].ToString();

                    if (mat_code.Equals(matCode))
                    {
                        dataExist = true;
                        float temp = row[dalMatPlan.PlanToUse] == null ? 0 : Convert.ToSingle(row[dalMatPlan.PlanToUse]);
                        qty = temp - qty;
                    }
                }

                if(qty < 0)
                {
                    qty = 0;
                }

                uMatPlan.mat_code = matCode;
                uMatPlan.plan_to_use = qty;

                if (dataExist)
                {
                    //update
                    successSave = dalMatPlan.Update(uMatPlan);
                }
               

                if (!successSave)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to save Material to use data");
                    historyRecord("SYSTEM", "Failed to save Material to use data", DateTime.Now, MainDashboard.USER_ID);
                }

            }

            return successSave;
        }

        public DataTable ZeroCostAddDuplicates(DataTable dt)
        {
            float jQty, iQty;
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        break;
                    }

                    string code = dt.Rows[i][headerCode].ToString();
                    if(code =="V95KUS000")
                    {
                        float test = 0;
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (dt.Rows[i][headerCode].ToString() == dt.Rows[j][headerCode].ToString())
                        {
                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceZero].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceZero].ToString());

                            if (jQty > 0 && iQty > 0)
                            {
                                if (jQty < iQty)
                                {
                                    jQty -= iQty;
                                    iQty = 0;
                                }
                                else
                                {
                                    iQty -= jQty;
                                    jQty = 0;
                                }
                            }
                            else
                            {
                                if (jQty > 0)
                                    jQty = 0;

                                if (iQty > 0)
                                    iQty = 0;
                            }

                            dt.Rows[j][headerBalanceZero] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceOne].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());

                            if (jQty > 0 && iQty > 0)
                            {
                                if (jQty < iQty)
                                {
                                    jQty -= iQty;
                                    iQty = 0;
                                }
                                else
                                {
                                    iQty -= jQty;
                                    jQty = 0;
                                }
                            }
                            else
                            {
                                if (jQty > 0)
                                    jQty = 0;

                                if (iQty > 0)
                                    iQty = 0;
                            }

                            dt.Rows[j][headerBalanceOne] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceTwo].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceTwo].ToString());

                            if(jQty > 0 && iQty > 0)
                            {
                                if(jQty < iQty)
                                {
                                    jQty -= iQty;
                                    iQty = 0;
                                }
                                else
                                {
                                    iQty -= jQty;
                                    jQty = 0;
                                }
                            }
                            else
                            {
                                if (jQty > 0)
                                    jQty = 0;

                                if (iQty > 0)
                                    iQty = 0;
                            }
                            
                            dt.Rows[j][headerBalanceTwo] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceThree].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceThree].ToString());

                            if (jQty > 0 && iQty > 0)
                            {
                                if (jQty < iQty)
                                {
                                    jQty -= iQty;
                                    iQty = 0;
                                }
                                else
                                {
                                    iQty -= jQty;
                                    jQty = 0;
                                }
                            }
                            else
                            {
                                if (jQty > 0)
                                    jQty = 0;

                                if (iQty > 0)
                                    iQty = 0;
                            }

                            dt.Rows[j][headerBalanceThree] = (jQty + iQty).ToString();

                            jQty = Convert.ToSingle(dt.Rows[j][headerBalanceFour].ToString());
                            iQty = Convert.ToSingle(dt.Rows[i][headerBalanceFour].ToString());

                            if (jQty > 0 && iQty > 0)
                            {
                                if (jQty < iQty)
                                {
                                    jQty -= iQty;
                                    iQty = 0;
                                }
                                else
                                {
                                    iQty -= jQty;
                                    jQty = 0;
                                }
                            }
                            else
                            {
                                if (jQty > 0)
                                    jQty = 0;

                                if (iQty > 0)
                                    iQty = 0;
                            }

                            dt.Rows[j][headerBalanceFour] = (jQty + iQty).ToString();

                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        public bool UndoTransferRecord(int trfID)
        {
            bool result = false;
            Text text = new Text();
            DataTable dt_Trf = dalTrfHist.SearchByID(trfID.ToString());

            if(dt_Trf.Rows.Count == 1)
            {
                string trfItemCode = dt_Trf.Rows[0][dalTrfHist.TrfItemCode].ToString();

                string trfFrom = dt_Trf.Rows[0][dalTrfHist.TrfFrom].ToString();

                string trfTo = dt_Trf.Rows[0][dalTrfHist.TrfTo].ToString();

                string unit = dt_Trf.Rows[0][dalTrfHist.TrfUnit].ToString();

                float trfQty = float.TryParse(dt_Trf.Rows[0][dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0 ;

                string note = dt_Trf.Rows[0][dalTrfHist.TrfNote].ToString();

                #region stock In/Out
                if (IfFactoryExists(trfFrom))
                {
                    result = stockIn(trfFrom, trfItemCode, trfQty, unit);

                    if (IfFactoryExists(trfTo))
                    {
                        result = stockOut(trfTo, trfItemCode, trfQty, unit);
                    }
                }
                else if (IfFactoryExists(trfTo))
                {
                    result = stockOut(trfTo, trfItemCode, trfQty, unit);
                }

                #endregion

                #region Undo transfer history
                if (result)
                {
                    utrfHist.trf_hist_updated_date = DateTime.Now;
                    utrfHist.trf_hist_updated_by = MainDashboard.USER_ID;
                    utrfHist.trf_hist_id = trfID;
                    utrfHist.trf_result = text.Undo;

                    //Inserting Data into Database
                    bool success = dalTrfHist.Update(utrfHist);
                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to change transfer record");
                        historyRecord("System", "Failed to change transfer record", utrfHist.trf_hist_updated_date, MainDashboard.USER_ID);
                    }
                    else
                    {
                        historyRecord(text.TransferUndoBySystem, text.getTransferDetailString(trfID, trfQty, unit, trfItemCode, trfFrom, trfTo), DateTime.Now, MainDashboard.USER_ID);
                    }

                }

                #endregion

            }
            else
            {
                MessageBox.Show("Transfer record not found or muliti records exist with same ID!");
            }

            return result;
        }

        private bool stockIn(string factoryName, string itemCode, float qty, string unit)
        {
            bool successFacStockIn;
    
            successFacStockIn = new facStockDAL().facStockIn(getFactoryID(factoryName).ToString(), itemCode, qty, unit);

            return successFacStockIn;
        }

        private bool stockOut(string factoryName, string itemCode, float qty, string unit)
        {
            bool successFacStockOut = false;

            successFacStockOut = new facStockDAL().facStockOut(getFactoryID(factoryName).ToString(), itemCode, qty, unit);

            return successFacStockOut;
        }

        public void changeTransferRecord(string stockResult, int id)
        {
            if(id == -1)
            {
                MessageBox.Show("Failed to change transfer record,id == -1, transfer record not found");
                historyRecord("System", "id == -1, transfer record not found", utrfHist.trf_hist_updated_date, MainDashboard.USER_ID);
            }
            else
            {
                utrfHist.trf_hist_updated_date = DateTime.Now;
                utrfHist.trf_hist_updated_by = MainDashboard.USER_ID;
                utrfHist.trf_hist_id = id;
                utrfHist.trf_result = stockResult;

                //Inserting Data into Database
                bool success = dalTrfHist.Update(utrfHist);
                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to change transfer record");
                    historyRecord("System", "Failed to change transfer record", utrfHist.trf_hist_updated_date, MainDashboard.USER_ID);
                }
            }
            
        }

        public string getCatNameFromDataTable(DataTable dt,string itemCode)
        {
            string catName = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if(row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        catName = row["item_cat"].ToString();
                        return catName;
                    }
                }
            }
            return catName;
        }

        


        public float getStockQtyFromDataTable(DataTable dt, string itemCode)
        {
            float stockQty = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        stockQty = Convert.ToSingle(row["item_qty"].ToString());
                        return stockQty;
                    }
                }
            }
            return stockQty;
        }

        public float getItemWastageAllowedFromDataTable(DataTable dt, string ItemCode)
        {
            float WastageAllowed = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(ItemCode))
                    {
                        WastageAllowed = Convert.ToSingle(row[dalItem.ItemWastage]);

                        return WastageAllowed;
                    }
                }
            }
            return WastageAllowed;
        }

        public string getItemName(DataTable dt, string ItemCode)
        {
            string ItemName = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(ItemCode))
                    {
                        ItemName = row["item_name"].ToString();

                        return ItemName;
                    }
                }
            }
            return ItemName;
        }

        public bool checkIfUsingOwnDOFrmDT(DataTable dt, string PONo)
        {
            bool usingOwnDO = false;


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalSBB.PONo].ToString().Equals(PONo))
                    {
                        return bool.TryParse(row[dalSBB.CustOwnDO].ToString(), out usingOwnDO) ? usingOwnDO : false ;
                    }
                }
            }

            return usingOwnDO;
        }

        public string getItemNameFromDataTable(DataTable dt, string ItemCode)
        {
            string ItemName = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(ItemCode))
                    {
                        ItemName = row["item_name"].ToString();

                        return ItemName;
                    }
                }
            }
            return ItemName;
        }

        public string getItemCatFromDataTable(DataTable dt, string ItemCode)
        {
            string catName = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(ItemCode))
                    {
                        catName = row["item_cat"].ToString();

                        return catName;
                    }
                }
            }
            return catName;
        }

        public DateTime GetTransferDate(string trfID)
        {
            DataTable dt = dalTrfHist.Select(trfID);

            return Convert.ToDateTime(dt.Rows[0][dalTrfHist.TrfDate].ToString()).Date;
        }

        public float GetZeroCostPendingOrder(DataTable dt,string itemCode)
        {
            string statusSearch = "PENDING";

            dt.DefaultView.Sort = "ord_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            float totalPending = 0;

            foreach (DataRow ord in sortedDt.Rows)
            {
                string orderType = ord["ord_type"].ToString();
                if (ord["ord_status"].ToString().Equals(statusSearch) && itemCode == ord[dalItem.ItemCode].ToString() && orderType == "ZERO COST")
                {

                    int orderID = Convert.ToInt32(ord["ord_id"].ToString());

                    if (orderID > 8)
                    {
                        float pendingOrder = float.TryParse(ord["ord_pending"].ToString(), out pendingOrder) ? pendingOrder : 0;
                        totalPending += pendingOrder;
                    }
                }
            }

            return totalPending;
        }

        public float GetPurchasePendingOrder(DataTable dt, string itemCode)
        {
            string statusSearch = "PENDING";

            dt.DefaultView.Sort = "ord_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            float totalPending = 0;

            foreach (DataRow ord in sortedDt.Rows)
            {
                string orderType = ord["ord_type"].ToString();
                if (ord["ord_status"].ToString().Equals(statusSearch) && itemCode == ord[dalItem.ItemCode].ToString() && orderType == "PURCHASE")
                {

                    int orderID = Convert.ToInt32(ord["ord_id"].ToString());

                    if (orderID > 8)
                    {
                        float pendingOrder = float.TryParse(ord["ord_pending"].ToString(), out pendingOrder) ? pendingOrder : 0;
                        totalPending += pendingOrder;
                    }
                }
            }

            return totalPending;
        }

        public float getOrderQtyFromDataTable(DataTable dt, string itemCode)
        {
            float orderQty = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        orderQty = Convert.ToSingle(row["item_ord"].ToString());

                        return orderQty;
                    }
                }
            }
           
            return orderQty;
        }

        public float getPMMAQtyFromDataTable(DataTable dt, string itemCode)
        {
            float PMMAQty = -1;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        PMMAQty = row[dalItem.ItemPMMAQty] == DBNull.Value? -1 : Convert.ToSingle(row[dalItem.ItemPMMAQty].ToString());

                        return PMMAQty;
                    }
                }
            }

            return PMMAQty;
        }

        public DataTable getStockDataTableFromDataTable(DataTable dt, string itemCode)
        {
            DataTable dt_Stock = NewStockTable();
            DataRow dtStock_row;
            int index = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        bool active = bool.TryParse(row[dalFac.FacActive].ToString(), out active) ? active : false;

                        if(active)
                        {
                            float stockQty = float.TryParse(row[dalFac.StockQty].ToString(), out stockQty) ? stockQty : 0;

                            dtStock_row = dt_Stock.NewRow();
                            dtStock_row[headerIndex] = index;
                            dtStock_row[headerName] = row[dalItem.ItemName].ToString();
                            dtStock_row[headerCode] = row[dalItem.ItemCode].ToString();
                            dtStock_row[headerFacName] = row[dalFac.FacName].ToString();
                            dtStock_row[headerReadyStock] = stockQty;
                            dtStock_row[headerUnit] = row[dalFac.StockUnit].ToString();

                            dt_Stock.Rows.Add(dtStock_row);
                            index++;
                        }
                    }
                }
            }

            return dt_Stock;
        }

        public DataTable getJoinDataTableFromDataTable(DataTable dt, string itemCode)
        {
            DataTable dt_Join = NewJoinTable();
            DataRow dtJoin_row;
            int index = 1;

            if (itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C")
            {
                foreach (DataRow row in dalJoin.SelectWithChildCat().Rows)
                {
                    if (itemCode == row[dalJoin.ParentCode].ToString())
                    {
                        string childCode = row[dalJoin.ChildCode].ToString();

                        if (childCode.Substring(0, 2) == "CF")
                        {
                            itemCode = childCode;

                            break;
                        }
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalJoin.JoinParent].ToString().Equals(itemCode))
                    {
                        dtJoin_row = dt_Join.NewRow();
                        dtJoin_row[headerIndex] = index;
                        dtJoin_row[headerParentCode] = row[dalJoin.JoinParent].ToString();
                        dtJoin_row[headerChildCode] = row[dalJoin.JoinChild].ToString();
                        dtJoin_row[headerJoinQty] = row[dalJoin.JoinQty];
                        dtJoin_row[headerJoinMax] = row[dalJoin.JoinMax];
                        dtJoin_row[headerJoinMin] = row[dalJoin.JoinMin];

                        dt_Join.Rows.Add(dtJoin_row);
                        index++;
                    }
                }
            }

            return dt_Join;
        }

        public DataRow getDataRowFromDataTable(DataTable dt, string itemCode)
        {
            DataRow dt_row = null;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {

                        dt_row = row;
                    }
                }
            }
            return dt_row;
        }

        public DataRow getDataRowFromDataTableByPlanID(DataTable dt, string PlanID)
        {
            DataRow dt_row = null;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPlanning.planID].ToString().Equals(PlanID))
                    {

                        dt_row = row;
                        return dt_row;
                    }
                }
            }
            return dt_row;
        }

        public int GetBalanceStock(string itemCode, int Month, int Year, int Stock)
        {
            DataTable dt_Item = dalItemCust.itemCodeSearch(itemCode);
            int balance = 0, readyStock = 0, forecast = 0, deliveredOut = 0;
            int qtyNeedFromParent = 0;
            int currentMonth = DateTime.Now.Month;

            readyStock = Stock;

            //get forecast
            if (dt_Item.Rows.Count > 0)
            {
                foreach (DataRow row in dt_Item.Rows)
                {
                    if (Month == currentMonth)
                    {
                        //foreacast 1
                        forecast += Convert.ToInt32(row["forecast_one"]);
                    }
                    else if (Month - currentMonth == 1)
                    {
                        //forecast 2
                        forecast += Convert.ToInt32(row["forecast_two"]);
                    }
                    else if (Month - currentMonth == 2)
                    {
                        //forecast 3
                        forecast += Convert.ToInt32(row["forecast_three"]);
                    }
                }
            }

            //calculate out
            string start = GetPMMAStartDate(Month, Year).ToString("yyyy/MM/dd");
            string end = GetPMMAEndDate(Month, Year).ToString("yyyy/MM/dd");

            DataTable dt_Out = dalTrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

            if (dt_Out.Rows.Count > 0)
            {
                foreach (DataRow row in dt_Out.Rows)
                {
                    if (row["trf_result"].ToString().Equals("Passed"))
                    {
                        deliveredOut += Convert.ToInt32(row["trf_hist_qty"]);
                    }
                }
            }

            forecast -= deliveredOut;

            if (forecast <= 0)
            {
                forecast = 0;
            }

            balance = readyStock - forecast;

            DataTable dt_Parent = dalJoin.loadParentList(itemCode);

            if (dt_Parent.Rows.Count > 0)
            {
                string parentCode;
                int temp = 0;
                foreach (DataRow row in dt_Parent.Rows)
                {
                    parentCode = row["join_parent_code"].ToString();
                    temp = GetBalanceStock(parentCode, Month, Year);

                    if (temp < 0)
                    {
                        int lastParentMonth = GetBalanceStock(parentCode, Month - 1, Year);
                        if (lastParentMonth < 0)
                        {
                            temp -= lastParentMonth;
                        }

                        qtyNeedFromParent += temp;
                        temp = 0;
                    }

                }
            }

            balance += qtyNeedFromParent;
            //DataTable dt3 = daltrfHist.rangeItemToCustomerSearch(cmbCust.Text, start, end, itemCode);
            return balance;
        }

        public int GetBalanceStock(string itemCode, int Month, int Year)
        {
            DataTable dt_Item = dalItemCust.itemCodeSearch(itemCode);
            int balance = 0, readyStock = 0, forecast = 0, deliveredOut = 0;
            int qtyNeedFromParent = 0;
            int currentMonth = DateTime.Now.Month;

            if (Month == currentMonth)
            {
                readyStock = Convert.ToInt32(dalItem.getStockQty(itemCode));
            }
            else if (Month - currentMonth == 1)
            {
                readyStock = GetBalanceStock(itemCode, Month-1, Year);
            }
            else if (Month - currentMonth == 2)
            {
                int test = GetBalanceStock(itemCode, Month - 2, Year);
                readyStock = GetBalanceStock(itemCode, Month - 1, Year, test);
            }

            //get forecast
            if (dt_Item.Rows.Count > 0)
            {
                foreach (DataRow row in dt_Item.Rows)
                {
                    if (Month == currentMonth)
                    {
                        //foreacast 1
                        forecast += Convert.ToInt32(row["forecast_one"]);
                    }
                    else if (Month - currentMonth == 1)
                    {
                        //forecast 2
                        forecast += Convert.ToInt32(row["forecast_two"]);
                    }
                    else if (Month - currentMonth == 2)
                    {
                        //forecast 3
                        forecast += Convert.ToInt32(row["forecast_three"]);
                    }
                }
            }

            //calculate out
            string start = GetPMMAStartDate(Month, Year).ToString("yyyy/MM/dd");
            string end = GetPMMAEndDate(Month, Year).ToString("yyyy/MM/dd");

            DataTable dt_Out = dalTrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

            if (dt_Out.Rows.Count > 0)
            {
                foreach (DataRow row in dt_Out.Rows)
                {
                    if (row["trf_result"].ToString().Equals("Passed") && !row["trf_hist_to"].ToString().Equals("OTHER"))
                    {
                        deliveredOut += Convert.ToInt32(row["trf_hist_qty"]);
                    }
                }
            }

            forecast -= deliveredOut;

            if (forecast <= 0)
            {
                forecast = 0;
            }

            balance = readyStock - forecast;

            DataTable dt_Parent = dalJoin.loadParentList(itemCode);

            if (dt_Parent.Rows.Count > 0)
            {
                string parentCode;
                int temp = 0;
                foreach (DataRow row in dt_Parent.Rows)
                {
                    parentCode = row["join_parent_code"].ToString();
                    temp = GetBalanceStock(parentCode, Month, Year);

                    if (temp < 0)
                    {
                        int lastParentMonth = GetBalanceStock(parentCode, Month - 1, Year);

                        if(lastParentMonth < 0)
                        {
                            temp -=lastParentMonth;
                        }

                        qtyNeedFromParent += temp;
                        temp = 0;
                    }

                }
            }

            balance += qtyNeedFromParent;
            //DataTable dt3 = daltrfHist.rangeItemToCustomerSearch(cmbCust.Text, start, end, itemCode);
            return balance;
        }

        public float getStockBalance(string itemCode,string facName, DataTable dt)
        {
            float balanceQty = 0;

            foreach (DataRow row in dt.Rows)
            {
                string code = row[dalItem.ItemCode].ToString();
                string fac = row["fac_name"].ToString();

                if(code == itemCode && fac == facName)
                {
                    balanceQty = row["stock_qty"] == DBNull.Value? 0 : Convert.ToSingle(row["stock_qty"]);
                }
            }

            return balanceQty;
        }

        public DataTable NewMatTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add(headerMat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerMB, typeof(string));
            dt.Columns.Add(headerMBRate, typeof(float));
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerWeight, typeof(float));
            dt.Columns.Add(headerWastage, typeof(float));
            dt.Columns.Add(headerBalanceZero, typeof(float));
            dt.Columns.Add(headerBalanceOne, typeof(float));
            dt.Columns.Add(headerBalanceTwo, typeof(float));
            dt.Columns.Add(headerBalanceThree, typeof(float));
            dt.Columns.Add(headerBalanceFour, typeof(float));

            dt.Columns.Add(headerOutOne, typeof(float));
            dt.Columns.Add(headerOutTwo, typeof(float));
            dt.Columns.Add(headerOutThree, typeof(float));
            dt.Columns.Add(headerOutFour, typeof(float));

            dt.Columns.Add(headerForecastOne, typeof(float));
            dt.Columns.Add(headerForecastTwo, typeof(float));
            dt.Columns.Add(headerForecastThree, typeof(float));
            dt.Columns.Add(headerForecastFour, typeof(float));


            return dt;
        }

        public DataTable NewStockTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));
           
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));

            dt.Columns.Add(headerFacName, typeof(string));
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerUnit, typeof(string));

            return dt;
        }

        public DataTable NewJoinTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));

            dt.Columns.Add(headerParentCode, typeof(string));
            dt.Columns.Add(headerChildCode, typeof(string));

            dt.Columns.Add(headerJoinQty, typeof(int));
            dt.Columns.Add(headerJoinMax, typeof(int));
            dt.Columns.Add(headerJoinMin, typeof(int));

            return dt;
        }

        public DataTable RemoveDuplicates(DataTable dt)
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

        public DataTable calStillNeed(DataTable dt)
        {
            float qty0, qty1, qty2, qty3, qty4, readyStock;
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    readyStock = dt.Rows[i][headerReadyStock] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerReadyStock].ToString());
                    qty0 = dt.Rows[i][headerBalanceZero] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceZero].ToString());
                    qty1 = dt.Rows[i][headerBalanceOne] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());
                    qty2 = dt.Rows[i][headerBalanceTwo] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceTwo].ToString());
                    qty3 = dt.Rows[i][headerBalanceThree] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceThree].ToString());
                    qty4 = dt.Rows[i][headerBalanceFour] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceFour].ToString());

                    if (qty1 >= 0 && qty1 == qty0)
                    {
                        dt.Rows[i][headerBalanceOne] = 0;
                    }
                    else
                    {
                        dt.Rows[i][headerBalanceOne] = qty1 - qty0;
                    }

                    if (qty2 >= 0 && qty2 == qty1)
                    {
                        dt.Rows[i][headerBalanceTwo] = 0;
                    }
                    else
                    {
                        dt.Rows[i][headerBalanceTwo] = qty2 - qty1;
                    }

                    if (qty3 >= 0 && qty3 == qty2)
                    {
                        dt.Rows[i][headerBalanceThree] = 0;
                    }
                    else
                    {
                        dt.Rows[i][headerBalanceThree] = qty3 - qty2;
                    }

                    if (qty4 >= 0 && qty4 == qty3)
                    {
                        dt.Rows[i][headerBalanceFour] = 0;
                    }
                    else
                    {
                        dt.Rows[i][headerBalanceFour] = qty4 - qty3;
                    }

                    qty1 = dt.Rows[i][headerBalanceOne] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());
                    qty2 = dt.Rows[i][headerBalanceTwo] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceTwo].ToString());
                    qty3 = dt.Rows[i][headerBalanceThree] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceThree].ToString());
                    qty4 = dt.Rows[i][headerBalanceFour] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceFour].ToString());

                    qty1 = qty1 == 0 ? readyStock : qty1 + readyStock;
                    qty2 = qty2 == 0 ? qty1 : qty2 + qty1;
                    qty3 = qty3 == 0 ? qty2 : qty3 + qty2;
                    qty4 = qty4 == 0 ? qty3 : qty4 + qty3;

                    dt.Rows[i][headerBalanceOne] = qty1;
                    dt.Rows[i][headerBalanceTwo] = qty2;
                    dt.Rows[i][headerBalanceThree] = qty3;
                    dt.Rows[i][headerBalanceFour] = qty4;

                }
                dt.AcceptChanges();
            }

            return dt;
        }

        public DataTable calCartonStillNeed(DataTable dt)
        {
            float qty0, qty1, qty2, qty3, qty4, readyStock;
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    readyStock = dt.Rows[i][headerReadyStock] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerReadyStock].ToString());
                    qty0 = dt.Rows[i][headerBalanceZero] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceZero].ToString());
                    qty1 = dt.Rows[i][headerBalanceOne] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());
                    qty2 = dt.Rows[i][headerBalanceTwo] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceTwo].ToString());
                    qty3 = dt.Rows[i][headerBalanceThree] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceThree].ToString());
                    qty4 = dt.Rows[i][headerBalanceFour] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceFour].ToString());

                    //if (qty1 >= 0 && qty1 == qty0)
                    //{
                    //    dt.Rows[i][headerBalanceOne] = 0;
                    //}
                    //else
                    //{
                    //    dt.Rows[i][headerBalanceOne] = qty1 - qty0;
                    //}

                    //if (qty2 >= 0 && qty2 == qty1)
                    //{
                    //    dt.Rows[i][headerBalanceTwo] = 0;
                    //}
                    //else
                    //{
                    //    dt.Rows[i][headerBalanceTwo] = qty2 - qty1;
                    //}

                    //if (qty3 >= 0 && qty3 == qty2)
                    //{
                    //    dt.Rows[i][headerBalanceThree] = 0;
                    //}
                    //else
                    //{
                    //    dt.Rows[i][headerBalanceThree] = qty3 - qty2;
                    //}

                    //if (qty4 >= 0 && qty4 == qty3)
                    //{
                    //    dt.Rows[i][headerBalanceFour] = 0;
                    //}
                    //else
                    //{
                    //    dt.Rows[i][headerBalanceFour] = qty4 - qty3;
                    //}

                    //qty1 = dt.Rows[i][headerBalanceOne] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());
                    //qty2 = dt.Rows[i][headerBalanceTwo] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceTwo].ToString());
                    //qty3 = dt.Rows[i][headerBalanceThree] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceThree].ToString());
                    //qty4 = dt.Rows[i][headerBalanceFour] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceFour].ToString());

                    //qty1 = qty1 == 0 ? readyStock : qty1 + readyStock;
                    //qty2 = qty2 == 0 ? qty1 : qty2 + qty1;
                    //qty3 = qty3 == 0 ? qty2 : qty3 + qty2;
                    //qty4 = qty4 == 0 ? qty3 : qty4 + qty3;

                    qty4 = qty4 - qty3;
                    qty3 = qty3 - qty2;
                    qty2 = qty2 - qty1;
                    qty1 = qty1 - qty0;

                    qty2 = qty1 < 0 ? qty2 + qty1: qty2;
                    qty3 = qty2 < 0 ? qty3 + qty2: qty3;
                    qty4 = qty3 < 0 ? qty4 + qty3: qty4;

                    qty1 = (qty1 + readyStock) >= 0 ? 0 : qty1 + readyStock;
                    qty2 = (qty2 + readyStock) >= 0 ? 0 : qty2 + readyStock;
                    qty3 = (qty3 + readyStock) >= 0 ? 0 : qty3 + readyStock;
                    qty4 = (qty4 + readyStock) >= 0 ? 0 : qty4 + readyStock;


                    dt.Rows[i][headerBalanceOne] = qty1;
                    dt.Rows[i][headerBalanceTwo] = qty2;
                    dt.Rows[i][headerBalanceThree] = qty3;
                    dt.Rows[i][headerBalanceFour] = qty4;

                }
                dt.AcceptChanges();
            }

            return dt;
        }

        public Tuple<int, int> GetNextMonthValue(int startMonth, int startYear, int nextmonthTime)
        {
            for(int i = 0; i < nextmonthTime; i++)
            {
                startMonth++;

                if(startMonth > 12)
                {
                    startMonth -= 12;
                    startYear++;
                }
            }

            return Tuple.Create(startMonth, startYear);
        }

        public DataTable GetItemBalanceValue(int custID, int startMonth, int startYear, int nextmonthTime, string itemType)
        {
            DataTable dt = GetCustItemWithForecast(custID, startMonth, startYear, nextmonthTime);

            //get child data if exist, out value and calculate balance for each month

            //
            return dt;
        }

        public DataTable GetCustItemWithForecast(int custID, int startMonth, int startYear, int nextmonthTime)
        {
            DataTable dt = new DataTable();

            //calculate month range
            var endDate = GetNextMonthValue(startMonth, startYear, nextmonthTime);
            int endMonth = endDate.Item1;
            int endYear = endDate.Item2;

            //custID: show all customer item if equal to 0
            if (custID > 0)
            {
                dt = dalItemForecast.SelectWithCustAndRange(custID, startMonth, startYear, endMonth, endYear);
            }
            else
            {
                dt = dalItemForecast.SelectWithRange(startMonth, startYear, endMonth, endYear);
            }

            //combine duplicate data

            return dt;
        }

        public float GetForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            month += forecastNum - 1;

            if (month > 12)
            {
                month -= 12;
                year++;

            }

            return getItemForecast(dt_ItemForecast, itemCode, year, month);

        }

        public float GetForecastQty(DataTable dt_ItemForecast, string itemCode, int forecastNum,string monthString)
        {
            int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;
            int year = DateTime.Now.Year;

            month += forecastNum - 1;

            if (month > 12)
            {
                month -= 12;
                year++;

            }
            return getItemForecast(dt_ItemForecast, itemCode, year, month);
        }


        public DataTable insertMaterialUsedData(string customer)
        {
            Text text = new Text();
            DataTable dt;
            DataTable dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());

            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
            }

            dt = RemoveDuplicates(dt);

            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";

            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock,child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1;
                float child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item[dalItem.ItemCode].ToString();

                    if(itemCode == "C84KXQ100")
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());
                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    forecast_1 = forecast_1 <= -1 ? 0 : forecast_1;
                    forecast_2 = forecast_2 <= -1 ? 0 : forecast_2;
                    forecast_3 = forecast_3 <= -1 ? 0 : forecast_3;
                    forecast_4 = forecast_4 <= -1 ? 0 : forecast_4;

                    itemPartWeight = item["item_quo_pw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_pw_pcs"].ToString());
                    itemRunnerWeight = item["item_quo_rw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_rw_pcs"].ToString());

                    if (itemPartWeight == 0)
                        itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());

                    if (itemRunnerWeight == 0)
                        itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());

                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    currentMonthOut = 0;
                    if (dt_currentMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_currentMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    //calculate still need how many qty
                    //forecastnum = balance
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = readyStock - forecast_1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else
                    {
                        bal_2 = bal_1 - forecast_2 + nextMonthOut;
                    }

                    bal_3 = bal_2 - forecast_3;
                    bal_4 = bal_3 - forecast_4;

                    #region child
                    if (ifGotChild(itemCode, dtJoin))
                    {
                        if (!item["item_assembly"].ToString().Equals("True") && item["item_production"].ToString().Equals("True"))
                        {
                            dtMat_row = dtMat.NewRow();
                            dtMat_row[headerIndex] = forecastIndex;
                            dtMat_row[headerMat] = item["item_material"].ToString();
                            dtMat_row[headerCode] = itemCode;
                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo,itemCode);
                            dtMat_row[headerMB] = MB;
                            dtMat_row[headerMBRate] = mbRate;

                            float totalWeight = itemPartWeight + itemRunnerWeight;
                            dtMat_row[headerWeight] = totalWeight;

                            dtMat_row[headerWastage] = wastageAllowed;
                            dtMat_row[headerReadyStock] = readyStock;
                            dtMat_row[headerBalanceZero] = readyStock;
                            dtMat_row[headerBalanceOne] = Convert.ToInt32(bal_1);
                            dtMat_row[headerBalanceTwo] = Convert.ToInt32(bal_2);
                            dtMat_row[headerBalanceThree] = Convert.ToInt32(bal_3);
                            dtMat_row[headerBalanceFour] = Convert.ToInt32(bal_4);

                            dtMat_row[headerOutOne] = currentMonthOut;
                            dtMat_row[headerOutTwo] = nextMonthOut;
                            dtMat_row[headerOutThree] = nextNextMonthOut;
                            dtMat_row[headerOutFour] = nextNextNextMonthOut;

                            dtMat_row[headerForecastOne] = forecast_1;
                            dtMat_row[headerForecastTwo] = forecast_2;
                            dtMat_row[headerForecastThree] = forecast_3;
                            dtMat_row[headerForecastFour] = forecast_4;

                            dtMat.Rows.Add(dtMat_row);
                            forecastIndex++;
                        }

                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join["parent_code"].ToString().Equals(itemCode))
                            {
                                if(itemCode == "V84KM4400")
                                {
                                    float test = 0;
                                }
       
                                if (Join["child_cat"].ToString().Equals("Part") || Join["child_cat"].ToString().Equals("Sub Material"))
                                {
                                    //if (!string.IsNullOrEmpty(Join["child_material"].ToString()) || Join["child_cat"].ToString().Equals("Sub Material"))
                                    
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join["join_qty"]);
                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());

                                    if (Join["child_cat"].ToString().Equals("Sub Material"))
                                    {
                                        child_Mat = "Sub Material";
                                    }
                                    else
                                    {
                                        child_Mat = Join["child_material"].ToString();
                                    }

                                    if (bal_1 < 0)
                                    {
                                        //if (Join["child_code"].ToString().Equals("V76KM4000 0.360"))
                                        //{
                                        //    childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                        //}
                                        //else
                                        //{
                                            childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                        //}
                                        
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = childBal_2 + bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = childBal_3 + bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }
                                   
                                    //if got child, then loop child here
                                    if (ifGotChild(Join["child_code"].ToString(), dtJoin))
                                    {
                                        if (!Join["child_assembly"].ToString().Equals("True") && Join["child_production"].ToString().Equals("True"))
                                        {
                                            dtMat_row = dtMat.NewRow();
                                            dtMat_row[headerIndex] = forecastIndex;
                                            dtMat_row[headerMat] = child_Mat;
                                            dtMat_row[headerCode] = Join["child_code"].ToString();
                                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                            dtMat_row[headerMB] = child_MB;
                                            dtMat_row[headerMBRate] = child_mbRate;

                                            float TotalWeight = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());

                                            if (TotalWeight == 0)
                                            {
                                                TotalWeight = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                            }

                                            dtMat_row[headerWeight] = TotalWeight;

                                           
                                            dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                            dtMat_row[headerReadyStock] = child_ReadyStock;
                                            dtMat_row[headerBalanceZero] = child_ReadyStock;
                                            dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                            dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                            dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                            dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                            dtMat_row[headerOutOne] = 0;
                                            dtMat_row[headerOutTwo] = 0;
                                            dtMat_row[headerOutThree] = 0;
                                            dtMat_row[headerOutFour] = 0;

                                            dtMat_row[headerForecastOne] = -1;
                                            dtMat_row[headerForecastTwo] = -1;
                                            dtMat_row[headerForecastThree] = -1;
                                            dtMat_row[headerForecastFour] = -1;

                                            dtMat.Rows.Add(dtMat_row);
                                            forecastIndex++;
                                        }

                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2["parent_code"].ToString().Equals(Join["child_code"].ToString()))
                                            {
                                                if (Join2["child_cat"].ToString().Equals("Part") || Join2["child_cat"].ToString().Equals("Sub Material"))
                                                {
                                                    if (true)
                                                    {
                                                        child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                        child_child_join_qty = Convert.ToSingle(Join2["join_qty"]);
                                                        child_child_MB = Join2["child_mb"] == DBNull.Value ? "NULL" : Join2["child_mb"].ToString();
                                                        child_child_mbRate = Join2["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join2["child_mb_rate"].ToString());

                                                        if (Join2["child_cat"].ToString().Equals("Sub Material"))
                                                        {
                                                            child_child_Mat = "Sub Material";
                                                        }
                                                        else
                                                        {
                                                            child_child_Mat = Join2["child_material"].ToString();
                                                        }

                                                        if (childBal_1 < 0)
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock + childBal_1 * child_child_join_qty;
                                                        }
                                                        else
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock;
                                                        }

                                                        if (childBal_2 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1;
                                                        }
                                                        else if (childBal_1 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1 + childBal_2 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                        }

                                                        if (childBal_3 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2;
                                                        }
                                                        else if (childBal_2 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2 + childBal_3 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_3 = child_childBal_2 - (childBal_2 - childBal_3) * child_child_join_qty;
                                                        }

                                                        if (childBal_4 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3;
                                                        }
                                                        else if (childBal_3 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3 + childBal_4 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_4 = child_childBal_3 - (childBal_3 - childBal_4) * child_child_join_qty;
                                                        }
                                                      
                                                        dtMat_row = dtMat.NewRow();
                                                        dtMat_row[headerIndex] = forecastIndex;
                                                        dtMat_row[headerMat] = child_child_Mat;
                                                        dtMat_row[headerCode] = Join2["child_code"].ToString();
                                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                        dtMat_row[headerMB] = child_child_MB;
                                                        dtMat_row[headerMBRate] = child_child_mbRate;

                                                        float TotalWeight = Convert.ToSingle(Join2["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join2["child_quo_runner_weight"].ToString());

                                                        if (TotalWeight == 0)
                                                        {
                                                            TotalWeight = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
                                                        }

                                                        dtMat_row[headerWeight] = TotalWeight;

                                                        dtMat_row[headerWastage] = Join2["child_wastage_allowed"].ToString();
                                                        dtMat_row[headerReadyStock] = child_child_ReadyStock;
                                                        dtMat_row[headerBalanceZero] = child_child_ReadyStock;
                                                        dtMat_row[headerBalanceOne] = Convert.ToSingle(child_childBal_1);
                                                        dtMat_row[headerBalanceTwo] = Convert.ToSingle(child_childBal_2);
                                                        dtMat_row[headerBalanceThree] = Convert.ToSingle(child_childBal_3);
                                                        dtMat_row[headerBalanceFour] = Convert.ToSingle(child_childBal_4);

                                                        dtMat_row[headerOutOne] = 0;
                                                        dtMat_row[headerOutTwo] = 0;
                                                        dtMat_row[headerOutThree] = 0;
                                                        dtMat_row[headerOutFour] = 0;

                                                        dtMat_row[headerForecastOne] = -1;
                                                        dtMat_row[headerForecastTwo] = -1;
                                                        dtMat_row[headerForecastThree] = -1;
                                                        dtMat_row[headerForecastFour] = -1;

                                                        dtMat.Rows.Add(dtMat_row);
                                                        forecastIndex++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dtMat_row = dtMat.NewRow();
                                        dtMat_row[headerIndex] = forecastIndex;
                                        dtMat_row[headerMat] = child_Mat;
                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                        dtMat_row[headerMB] = child_MB;
                                        dtMat_row[headerMBRate] = child_mbRate;

                                        float TotalWeight = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());

                                        if (TotalWeight == 0)
                                        {
                                            TotalWeight = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                        }

                                        dtMat_row[headerWeight] = TotalWeight;
             

                                        dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                        dtMat_row[headerReadyStock] = child_ReadyStock;
                                        dtMat_row[headerBalanceZero] = child_ReadyStock;
                                        dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                        dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                        dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                        dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                        dtMat_row[headerOutOne] = 0;
                                        dtMat_row[headerOutTwo] = 0;
                                        dtMat_row[headerOutThree] = 0;
                                        dtMat_row[headerOutFour] = 0;

                                        dtMat_row[headerForecastOne] = -1;
                                        dtMat_row[headerForecastTwo] = -1;
                                        dtMat_row[headerForecastThree] = -1;
                                        dtMat_row[headerForecastFour] = -1;

                                        dtMat.Rows.Add(dtMat_row);
                                        forecastIndex++;
                                    }
                                    
                                }

                                else if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join[dalJoin.JoinQty]);
                                    int child_join_max = int.TryParse(Join[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                    int child_join_min = int.TryParse(Join[dalJoin.JoinMin].ToString(), out  x) ? x : 0;

                                    child_join_max = child_join_max <= 0 ? 1 : child_join_max;
                                    child_join_min = child_join_min <= 0 ? 1 : child_join_min;

                                    int fullQty = 0;
                                    int notFullQty = 0;
                                    int childQty = 0;
                                    int ParentQty = 0;

                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());
                                    child_Mat = text.Cat_Carton;
                                   

                                    if (bal_1 < 0)
                                    {
                                        ParentQty = (int)bal_1 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }
                                        childBal_1 = child_ReadyStock - childQty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        ParentQty = (int)bal_2 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_2;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        ParentQty = (int)bal_3*-1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_3;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        ParentQty = (int)bal_4 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_4;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }
                                   
                                    //if got child, then loop child here
                                    if (ifGotChild(Join["child_code"].ToString(), dtJoin))
                                    {
                                        if (!Join["child_assembly"].ToString().Equals("True") && Join["child_production"].ToString().Equals("True"))
                                        {
                                            dtMat_row = dtMat.NewRow();
                                            dtMat_row[headerIndex] = forecastIndex;
                                            dtMat_row[headerMat] = child_Mat;
                                            dtMat_row[headerCode] = Join["child_code"].ToString();
                                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                            dtMat_row[headerMB] = child_MB;
                                            dtMat_row[headerMBRate] = child_mbRate;

                                            float TotalWeight = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());

                                            if (TotalWeight == 0)
                                            {
                                                TotalWeight = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                            }

                                            dtMat_row[headerWeight] = TotalWeight;


                                            dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                            dtMat_row[headerReadyStock] = child_ReadyStock;
                                            dtMat_row[headerBalanceZero] = child_ReadyStock;
                                            dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                            dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                            dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                            dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                            dtMat_row[headerOutOne] = 0;
                                            dtMat_row[headerOutTwo] = 0;
                                            dtMat_row[headerOutThree] = 0;
                                            dtMat_row[headerOutFour] = 0;

                                            dtMat_row[headerForecastOne] = -1;
                                            dtMat_row[headerForecastTwo] = -1;
                                            dtMat_row[headerForecastThree] = -1;
                                            dtMat_row[headerForecastFour] = -1;

                                            dtMat.Rows.Add(dtMat_row);
                                            forecastIndex++;
                                        }

                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2["parent_code"].ToString().Equals(Join["child_code"].ToString()))
                                            {
                                                if (Join2[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton))
                                                {
                                                    if (true)
                                                    {
                                                        if(Join2["parent_code"].ToString().Equals("V37KM4000"))
                                                        {
                                                            float test = 0;
                                                        }

                                                        child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                        child_child_join_qty = Convert.ToSingle(Join2["join_qty"]);
                                                        child_child_MB = Join2["child_mb"] == DBNull.Value ? "NULL" : Join2["child_mb"].ToString();
                                                        child_child_mbRate = Join2["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join2["child_mb_rate"].ToString());

                                                        if (Join2["child_cat"].ToString().Equals("Sub Material"))
                                                        {
                                                            child_child_Mat = "Sub Material";
                                                        }
                                                        else
                                                        {
                                                            child_child_Mat = Join2["child_material"].ToString();
                                                        }

                                                        if (childBal_1 < 0)
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock + childBal_1 * child_child_join_qty;
                                                        }
                                                        else
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock;
                                                        }

                                                        if (childBal_2 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1;
                                                        }
                                                        else if (childBal_1 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1 + childBal_2 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                        }

                                                        if (childBal_3 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2;
                                                        }
                                                        else if (childBal_2 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2 + childBal_3 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_3 = child_childBal_2 - (childBal_2 - childBal_3) * child_child_join_qty;
                                                        }

                                                        if (childBal_4 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3;
                                                        }
                                                        else if (childBal_3 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3 + childBal_4 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_4 = child_childBal_3 - (childBal_3 - childBal_4) * child_child_join_qty;
                                                        }
                                                      
                                                        dtMat_row = dtMat.NewRow();
                                                        dtMat_row[headerIndex] = forecastIndex;
                                                        dtMat_row[headerMat] = child_child_Mat;
                                                        dtMat_row[headerCode] = Join2["child_code"].ToString();
                                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                        dtMat_row[headerMB] = child_child_MB;
                                                        dtMat_row[headerMBRate] = child_child_mbRate;

                                                        float TotalWeight = Convert.ToSingle(Join2["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join2["child_quo_runner_weight"].ToString());

                                                        if (TotalWeight == 0)
                                                        {
                                                            TotalWeight = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
                                                        }

                                                        dtMat_row[headerWeight] = TotalWeight;

                                                        dtMat_row[headerWastage] = Join2["child_wastage_allowed"].ToString();
                                                        dtMat_row[headerReadyStock] = child_child_ReadyStock;
                                                        dtMat_row[headerBalanceZero] = child_child_ReadyStock;
                                                        dtMat_row[headerBalanceOne] = Convert.ToSingle(child_childBal_1);
                                                        dtMat_row[headerBalanceTwo] = Convert.ToSingle(child_childBal_2);
                                                        dtMat_row[headerBalanceThree] = Convert.ToSingle(child_childBal_3);
                                                        dtMat_row[headerBalanceFour] = Convert.ToSingle(child_childBal_4);

                                                        dtMat_row[headerOutOne] = 0;
                                                        dtMat_row[headerOutTwo] = 0;
                                                        dtMat_row[headerOutThree] = 0;
                                                        dtMat_row[headerOutFour] = 0;

                                                        dtMat_row[headerForecastOne] = -1;
                                                        dtMat_row[headerForecastTwo] = -1;
                                                        dtMat_row[headerForecastThree] = -1;
                                                        dtMat_row[headerForecastFour] = -1;

                                                        dtMat.Rows.Add(dtMat_row);
                                                        forecastIndex++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dtMat_row = dtMat.NewRow();
                                        dtMat_row[headerIndex] = forecastIndex;
                                        dtMat_row[headerMat] = child_Mat;
                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                        dtMat_row[headerMB] = child_MB;
                                        dtMat_row[headerMBRate] = child_mbRate;

                                        float TotalWeight = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());

                                        if (TotalWeight == 0)
                                        {
                                            TotalWeight = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                        }

                                        dtMat_row[headerWeight] = TotalWeight;

                                        dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                        dtMat_row[headerReadyStock] = child_ReadyStock;
                                        dtMat_row[headerBalanceZero] = child_ReadyStock;
                                        dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                        dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                        dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                        dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                        dtMat_row[headerOutOne] = 0;
                                        dtMat_row[headerOutTwo] = 0;
                                        dtMat_row[headerOutThree] = 0;
                                        dtMat_row[headerOutFour] = 0;

                                        dtMat_row[headerForecastOne] = -1;
                                        dtMat_row[headerForecastTwo] = -1;
                                        dtMat_row[headerForecastThree] = -1;
                                        dtMat_row[headerForecastFour] = -1;

                                        dtMat.Rows.Add(dtMat_row);
                                        forecastIndex++;
                                    }
                                    
                                }
                                
                            }

                        }
                    }
                    else
                    {
                        //save to database

                        if (!string.IsNullOrEmpty(item["item_material"].ToString()))
                        {
                            dtMat_row = dtMat.NewRow();
                            dtMat_row[headerIndex] = forecastIndex;
                            dtMat_row[headerMat] = item["item_material"].ToString();
                            dtMat_row[headerCode] = itemCode;
                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                            dtMat_row[headerMB] = MB;
                            dtMat_row[headerMBRate] = mbRate;

                            float totalWeight = itemPartWeight + itemRunnerWeight;
                            dtMat_row[headerWeight] = totalWeight;
                            dtMat_row[headerWastage] = wastageAllowed;
                            dtMat_row[headerReadyStock] = readyStock;
                            dtMat_row[headerBalanceZero] = readyStock;
                            dtMat_row[headerBalanceOne] = Convert.ToSingle(bal_1);
                            dtMat_row[headerBalanceTwo] = Convert.ToSingle(bal_2);
                            dtMat_row[headerBalanceThree] = Convert.ToSingle(bal_3);
                            dtMat_row[headerBalanceFour] = Convert.ToSingle(bal_4);

                            dtMat_row[headerOutOne] = currentMonthOut;
                            dtMat_row[headerOutTwo] = nextMonthOut;
                            dtMat_row[headerOutThree] = nextNextMonthOut;
                            dtMat_row[headerOutFour] = nextNextNextMonthOut;

                            dtMat_row[headerForecastOne] = forecast_1;
                            dtMat_row[headerForecastTwo] = forecast_2;
                            dtMat_row[headerForecastThree] = forecast_3;
                            dtMat_row[headerForecastFour] = forecast_4;

                            dtMat.Rows.Add(dtMat_row);
                            forecastIndex++;
                        }

                    }
                    #endregion
                }
            }

            dtMat = AddDuplicates(dtMat);
            dtMat = calStillNeed(dtMat);
            return dtMat;
        }

        

        public DataTable GetCartonBalanceData(string customer)
        {
            #region Get Data From Database
            Text text = new Text();
            DataTable dt;
            DataTable dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());

            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
            }

            dt = RemoveDuplicates(dt);

            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";

            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;
            #endregion

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock, child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1;
                float child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item[dalItem.ItemCode].ToString();

                    if(itemCode.Equals("V95KM4300"))
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());

                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    forecast_1 = forecast_1 <= -1 ? 0 : forecast_1;
                    forecast_2 = forecast_2 <= -1 ? 0 : forecast_2;
                    forecast_3 = forecast_3 <= -1 ? 0 : forecast_3;
                    forecast_4 = forecast_4 <= -1 ? 0 : forecast_4;

                    itemPartWeight = item["item_quo_pw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_pw_pcs"].ToString());
                    itemRunnerWeight = item["item_quo_rw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_rw_pcs"].ToString());

                    if(itemPartWeight == 0)
                    itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());

                    if(itemRunnerWeight == 0)
                    itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());

                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    currentMonthOut = 0;
                    if (dt_currentMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_currentMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    //calculate still need how many qty
                    //forecastnum = balance
                    #region Calculate Balance
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = readyStock - forecast_1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else
                    {
                        bal_2 = bal_1 - forecast_2 + nextMonthOut;
                    }

                    bal_3 = bal_2 - forecast_3;
                    bal_4 = bal_3 - forecast_4;
                    #endregion

                    #region child

                    if (ifGotChildIncludedPacking(itemCode, dtJoin))
                    {
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join[dalJoin.ParentCode].ToString().Equals(itemCode))
                            {
                                string childCat = Join[dalJoin.ChildCat].ToString();
                                if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Part))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join["join_qty"]);
                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());

                                    #region Calculate Balance
                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = childBal_2 + bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = childBal_3 + bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }
                                    #endregion


                                    //if(childBal_4 < 0)
                                    //{
                                    //    Join["child_qty"] = 0;
                                    //}
                                    //else
                                    //{
                                    //    Join["child_qty"] = childBal_4;

                                    //}

                                    if (Join["child_code"].ToString().Equals("V95KM4300"))
                                    {
                                        float test = 0;
                                    }

                                    //if got child, then loop child here
                                    if (ifGotChildIncludedPacking(Join["child_code"].ToString(), dtJoin))
                                    {
                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2[dalJoin.ParentCode].ToString().Equals(Join[dalJoin.ChildCode].ToString()) && Join2[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton))
                                            {
                                                child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                child_child_join_qty = Convert.ToSingle(Join2[dalJoin.JoinQty]);
                                                int child_child_join_max = int.TryParse(Join2[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                                int child_child_join_min = int.TryParse(Join2[dalJoin.JoinMin].ToString(), out x) ? x : 0;

                                                child_child_join_max = child_child_join_max <= 0 ? 1 : child_child_join_max;
                                                child_child_join_min = child_child_join_min <= 0 ? 1 : child_child_join_min;

                                                int fullQty = 0;
                                                int notFullQty = 0;
                                                int childQty = 0;
                                                int ParentQty = 0;

                                                child_child_MB = Join2["child_mb"] == DBNull.Value ? "NULL" : Join2["child_mb"].ToString();
                                                child_child_mbRate = Join2["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join2["child_mb_rate"].ToString());

                                                child_child_Mat = text.Cat_Carton;

                                                #region Calculate Balance
                                                if (childBal_1 < 0)
                                                {
                                                    ParentQty = (int)childBal_1 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_child_join_qty;

                                                    }
                                                    child_childBal_1 = child_child_ReadyStock - childQty;
                                                }
                                                else
                                                {
                                                    child_childBal_1 = child_child_ReadyStock;
                                                }

                                                if (childBal_2 > 0)
                                                {
                                                    child_childBal_2 = child_childBal_1;
                                                }
                                                else if (childBal_1 > 0)
                                                {
                                                    ParentQty = (int)childBal_2 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_child_join_qty;

                                                    }

                                                    child_childBal_2 = child_childBal_1 - childQty;
                                                }
                                                else
                                                {
                                                    //child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                    ParentQty = (int)(childBal_1 - childBal_2);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_2 = child_childBal_1 - childQty;
                                                }

                                                if (childBal_3 > 0)
                                                {
                                                    child_childBal_3 = child_childBal_2;
                                                }
                                                else if (childBal_2 > 0)
                                                {
                                                    ParentQty = (int)childBal_3 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_3 = child_childBal_2 - childQty;
                                                }
                                                else
                                                {
                                              
                                                    ParentQty = (int)(childBal_2 - childBal_3);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_3 = child_childBal_2 - childQty;
                                                }

                                                if (childBal_4 > 0)
                                                {
                                                    child_childBal_4 = child_childBal_3;
                                                }
                                                else if (childBal_3 > 0)
                                                {
                                                    ParentQty = (int)childBal_4 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_child_join_qty;

                                                    }

                                                    child_childBal_4 = child_childBal_3 - childQty;
                                                }
                                                else
                                                {
                                       
                                                    ParentQty = (int)(childBal_3 - childBal_4);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_child_join_qty;

                                                    }

                                                    child_childBal_4 = child_childBal_3 - childQty;
                                                }

                                                #endregion

                                                #region Add Data
                                                dtMat_row = dtMat.NewRow();


                                                if (forecastIndex == 63)
                                                {
                                                    float test = 0;

                                                }

                                                dtMat_row[headerIndex] = forecastIndex;
                                                dtMat_row[headerMat] = child_child_Mat;
                                                dtMat_row[headerCode] = Join2["child_code"].ToString();
                                                dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                dtMat_row[headerMB] = child_child_MB;
                                                dtMat_row[headerMBRate] = child_child_mbRate;

                                                float TotalWeight = Convert.ToSingle(Join2["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join2["child_quo_runner_weight"].ToString());

                                                if(TotalWeight == 0)
                                                {
                                                    TotalWeight = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
                                                }

                                                dtMat_row[headerWeight] = TotalWeight;

                                                dtMat_row[headerWastage] = Join2["child_wastage_allowed"].ToString();
                                                dtMat_row[headerReadyStock] = child_child_ReadyStock;
                                                dtMat_row[headerBalanceZero] = child_child_ReadyStock;
                                                dtMat_row[headerBalanceOne] = Convert.ToSingle(child_childBal_1);
                                                dtMat_row[headerBalanceTwo] = Convert.ToSingle(child_childBal_2);
                                                dtMat_row[headerBalanceThree] = Convert.ToSingle(child_childBal_3);
                                                dtMat_row[headerBalanceFour] = Convert.ToSingle(child_childBal_4);

                                                dtMat_row[headerOutOne] = 0;
                                                dtMat_row[headerOutTwo] = 0;
                                                dtMat_row[headerOutThree] = 0;
                                                dtMat_row[headerOutFour] = 0;

                                                dtMat_row[headerForecastOne] = -1;
                                                dtMat_row[headerForecastTwo] = -1;
                                                dtMat_row[headerForecastThree] = -1;
                                                dtMat_row[headerForecastFour] = -1;

                                                dtMat.Rows.Add(dtMat_row);
                                                forecastIndex++;
                                                #endregion
                                            }
                                        }
                                    }
                                }

                                else if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join[dalJoin.JoinQty]);
                                    int child_join_max = int.TryParse(Join[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                    int child_join_min = int.TryParse(Join[dalJoin.JoinMin].ToString(), out x) ? x : 0;

                                    child_join_max = child_join_max <= 0 ? 1 : child_join_max;
                                    child_join_min = child_join_min <= 0 ? 1 : child_join_min;

                                    int fullQty = 0;
                                    int notFullQty = 0;
                                    int childQty = 0;
                                    int ParentQty = 0;

                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());
                                    child_Mat = text.Cat_Carton;

                                    #region Calculate Balance
                                    if (bal_1 < 0)
                                    {
                                        ParentQty = (int)bal_1 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }
                                        childBal_1 = child_ReadyStock - childQty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        ParentQty = (int)bal_2 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_2;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        ParentQty = (int)bal_3 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_3;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        ParentQty = (int)bal_4 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_4;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }

                                    #endregion

                                    #region Add Data
                                    dtMat_row = dtMat.NewRow();

                                    if (forecastIndex == 63)
                                    {
                                        float test = 0;

                                    }

                                    dtMat_row[headerIndex] = forecastIndex;
                                    dtMat_row[headerMat] = child_Mat;
                                    dtMat_row[headerCode] = Join["child_code"].ToString();
                                    dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                    dtMat_row[headerMB] = child_MB;
                                    dtMat_row[headerMBRate] = child_mbRate;

                                    float TotalWeight = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());

                                    if (TotalWeight == 0)
                                    {
                                        TotalWeight = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                    }

                                    dtMat_row[headerWeight] = TotalWeight;

                                    dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                    dtMat_row[headerReadyStock] = child_ReadyStock;
                                    dtMat_row[headerBalanceZero] = child_ReadyStock;
                                    dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                    dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                    dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                    dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                    dtMat_row[headerOutOne] = 0;
                                    dtMat_row[headerOutTwo] = 0;
                                    dtMat_row[headerOutThree] = 0;
                                    dtMat_row[headerOutFour] = 0;

                                    dtMat_row[headerForecastOne] = -1;
                                    dtMat_row[headerForecastTwo] = -1;
                                    dtMat_row[headerForecastThree] = -1;
                                    dtMat_row[headerForecastFour] = -1;

                                    dtMat.Rows.Add(dtMat_row);
                                    forecastIndex++;
                                    #endregion
                                }

                            }

                        }
                    }
                    #endregion
                }
            }

            dtMat = AddDuplicates(dtMat);
            dtMat = calStillNeed(dtMat);

            dtMat = RecalculateCartonBalance(dtMat,customer);

            return dtMat;
        }

        private DataTable RecalculateCartonBalance(DataTable dt, string customer)
        {
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    string CartonCode = row[headerCode].ToString();
                    DataTable dt_Bal =  GetCartonBalanceData(customer, CartonCode);
                    float ReadyStock = float.TryParse(row[headerReadyStock].ToString(), out ReadyStock) ? ReadyStock : 0;

                    float bal_1 = 0;
                    float bal_2 = 0;
                    float bal_3 = 0;
                    float bal_4 = 0;
                    
                    foreach (DataRow bal in dt_Bal.Rows)
                    {
                        float MB = float.TryParse(bal[headerMB].ToString(), out MB) ? MB : 0;
                        float MBRate = float.TryParse(bal[headerMBRate].ToString(), out MBRate) ? MBRate : 0;

                        MB = MB == 0 ? 1 : MB;
                        MBRate = MBRate == 0 ? 1 : MBRate;

                        float CartonRate = MBRate / MB;

                        float Needed_1 = float.TryParse(bal[headerBalanceOne].ToString(), out Needed_1) ? Needed_1 : 0;
                        float Needed_2 = float.TryParse(bal[headerBalanceTwo].ToString(), out Needed_2) ? Needed_2 : 0;
                        float Needed_3 = float.TryParse(bal[headerBalanceThree].ToString(), out Needed_3) ? Needed_3 : 0;
                        float Needed_4 = float.TryParse(bal[headerBalanceFour].ToString(), out Needed_4) ? Needed_4 : 0;


                        bal_1 += Needed_1 * CartonRate;
                        bal_2 += Needed_2 * CartonRate;
                        bal_3 += Needed_3 * CartonRate;
                        bal_4 += Needed_4 * CartonRate;
                    }

                    row[headerBalanceOne] = ReadyStock + bal_1;
                    row[headerBalanceTwo] = ReadyStock + bal_2;
                    row[headerBalanceThree] = ReadyStock + bal_3;
                    row[headerBalanceFour] = ReadyStock + bal_4;
                }
            }
            return dt;
        }

        public DataTable NewGetCartonBalanceData(string customer)
        {
            #region Get Data From Database
            Text text = new Text();
            DataTable dt;
            DataTable dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());

            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
            }

            dt = RemoveDuplicates(dt);

            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";

            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;
            #endregion

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock, child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1;
                float child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item[dalItem.ItemCode].ToString();

                    if (itemCode.Equals("V84KM43RV"))
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());

                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    forecast_1 = forecast_1 <= -1 ? 0 : forecast_1;
                    forecast_2 = forecast_2 <= -1 ? 0 : forecast_2;
                    forecast_3 = forecast_3 <= -1 ? 0 : forecast_3;
                    forecast_4 = forecast_4 <= -1 ? 0 : forecast_4;

                    itemPartWeight = item["item_quo_pw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_pw_pcs"].ToString());
                    itemRunnerWeight = item["item_quo_rw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_rw_pcs"].ToString());

                    if (itemPartWeight == 0)
                        itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());

                    if (itemRunnerWeight == 0)
                        itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());

                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    currentMonthOut = 0;
                    if (dt_currentMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_currentMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    //calculate still need how many qty
                    //forecastnum = balance
                    #region Calculate Balance
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = readyStock - forecast_1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else
                    {
                        bal_2 = bal_1 - forecast_2 + nextMonthOut;
                    }

                    bal_3 = bal_2 - forecast_3;
                    bal_4 = bal_3 - forecast_4;
                    #endregion

                    #region child

                    if (ifGotChildIncludedPacking(itemCode, dtJoin))
                    {
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join[dalJoin.ParentCode].ToString().Equals(itemCode))
                            {
                                string childCat = Join[dalJoin.ChildCat].ToString();
                                if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Part))
                                {
                                    string childeCode = Join["child_code"].ToString();

                                    if (childeCode == "V37KM40RV")
                                    {
                                        float test = 0;
                                    }

                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join["join_qty"]);
                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());

                                    #region Calculate Balance
                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = childBal_2 + bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = childBal_3 + bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }
                                    #endregion

                                    //if got child, then loop child here
                                    if (ifGotChildIncludedPacking(Join["child_code"].ToString(), dtJoin))
                                    {
                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2[dalJoin.ParentCode].ToString().Equals(Join[dalJoin.ChildCode].ToString()) && Join2[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton))
                                            {
                                                child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                child_child_join_qty = Convert.ToSingle(Join2[dalJoin.JoinQty]);
                                                int child_child_join_max = int.TryParse(Join2[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                                int child_child_join_min = int.TryParse(Join2[dalJoin.JoinMin].ToString(), out x) ? x : 0;

                                                child_child_join_max = child_child_join_max <= 0 ? 1 : child_child_join_max;
                                                child_child_join_min = child_child_join_min <= 0 ? 1 : child_child_join_min;

                                                int fullQty = 0;
                                                int notFullQty = 0;
                                                int childQty = 0;
                                                int ParentQty = 0;

                                                child_child_MB = Join2["child_mb"] == DBNull.Value ? "NULL" : Join2["child_mb"].ToString();
                                                child_child_mbRate = Join2["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join2["child_mb_rate"].ToString());

                                                child_child_Mat = text.Cat_Carton;

                                                #region Calculate Balance
                                                if (childBal_1 < 0)
                                                {
                                                    ParentQty = (int)childBal_1 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }
                                                    child_childBal_1 = child_child_ReadyStock - childQty;
                                                }
                                                else
                                                {
                                                    child_childBal_1 = child_child_ReadyStock;
                                                }

                                                if (childBal_2 > 0)
                                                {
                                                    child_childBal_2 = child_childBal_1;
                                                }
                                                else if (childBal_1 > 0)
                                                {
                                                    ParentQty = (int)childBal_2 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_2 = child_childBal_1 - childQty;
                                                }
                                                else
                                                {
                                                    //child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                    ParentQty = (int)(childBal_1 - childBal_2);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_2 = child_childBal_1 - childQty;
                                                }

                                                if (childBal_3 > 0)
                                                {
                                                    child_childBal_3 = child_childBal_2;
                                                }
                                                else if (childBal_2 > 0)
                                                {
                                                    ParentQty = (int)childBal_3 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_3 = child_childBal_2 - childQty;
                                                }
                                                else
                                                {

                                                    ParentQty = (int)(childBal_2 - childBal_3);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_3 = child_childBal_2 - childQty;
                                                }

                                                if (childBal_4 > 0)
                                                {
                                                    child_childBal_4 = child_childBal_3;
                                                }
                                                else if (childBal_3 > 0)
                                                {
                                                    ParentQty = (int)childBal_4 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_4 = child_childBal_3 - childQty;
                                                }
                                                else
                                                {

                                                    ParentQty = (int)(childBal_3 - childBal_4);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_4 = child_childBal_3 - childQty;
                                                }

                                                #endregion

                                                #region Add Data

                                                if (forecastIndex == 5)
                                                {
                                                    float test = 0;

                                                }

                                                string addingMaterialCode = Join2["child_code"].ToString();
                                                string addingItemCode = Join[dalJoin.ChildCode].ToString();
                                                string addingItemName = getItemNameFromDataTable(dt_ItemInfo, Join[dalJoin.ChildCode].ToString());

                                                dtMat_row = dtMat.NewRow();
                                                dtMat_row[headerIndex] = forecastIndex;
                                                dtMat_row[headerMat] = addingMaterialCode;
                                                dtMat_row[headerCode] = addingItemCode;
                                                dtMat_row[headerName] = addingItemName;
                                                dtMat_row[headerMB] = child_child_join_max;
                                                dtMat_row[headerMBRate] = child_child_join_qty;
                                                //dtMat_row[headerWeight] = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
                                                //dtMat_row[headerWastage] = Join2["child_wastage_allowed"].ToString();
                                                dtMat_row[headerReadyStock] = child_ReadyStock;
                                                dtMat_row[headerBalanceZero] = child_ReadyStock;
                                                dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                                dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                                dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                                dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                                dtMat_row[headerOutOne] = 0;
                                                dtMat_row[headerOutTwo] = 0;
                                                dtMat_row[headerOutThree] = 0;
                                                dtMat_row[headerOutFour] = 0;

                                                dtMat_row[headerForecastOne] = -1;
                                                dtMat_row[headerForecastTwo] = -1;
                                                dtMat_row[headerForecastThree] = -1;
                                                dtMat_row[headerForecastFour] = -1;

                                                dtMat.Rows.Add(dtMat_row);
                                                forecastIndex++;
                                                #endregion
                                            }
                                        }
                                    }
                                }

                                else if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join[dalJoin.JoinQty]);
                                    int child_join_max = int.TryParse(Join[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                    int child_join_min = int.TryParse(Join[dalJoin.JoinMin].ToString(), out x) ? x : 0;

                                    child_join_max = child_join_max <= 0 ? 1 : child_join_max;
                                    child_join_min = child_join_min <= 0 ? 1 : child_join_min;

                                    int fullQty = 0;
                                    int notFullQty = 0;
                                    int childQty = 0;
                                    int ParentQty = 0;

                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());
                                    child_Mat = text.Cat_Carton;

                                    #region Calculate Balance
                                    if (bal_1 < 0)
                                    {
                                        ParentQty = (int)bal_1 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }
                                        childBal_1 = child_ReadyStock - childQty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        ParentQty = (int)bal_2 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_2;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        ParentQty = (int)bal_3 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_3;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        ParentQty = (int)bal_4 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_4;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }

                                    #endregion

                                    if (forecastIndex == 5)
                                    {
                                        float test = 0;

                                    }

                                    #region Add Data
                                    dtMat_row = dtMat.NewRow();
                                    dtMat_row[headerIndex] = forecastIndex;
                                    dtMat_row[headerMat] = Join["child_code"].ToString();
                                    dtMat_row[headerCode] = itemCode;
                                    dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                                    dtMat_row[headerMB] = child_join_max;
                                    dtMat_row[headerMBRate] = child_join_qty;
                                    //dtMat_row[headerWeight] = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                    // dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                    dtMat_row[headerReadyStock] = readyStock;
                                    dtMat_row[headerBalanceZero] = readyStock;
                                    dtMat_row[headerBalanceOne] = Convert.ToSingle(bal_1);
                                    dtMat_row[headerBalanceTwo] = Convert.ToSingle(bal_2);
                                    dtMat_row[headerBalanceThree] = Convert.ToSingle(bal_3);
                                    dtMat_row[headerBalanceFour] = Convert.ToSingle(bal_4);

                                    dtMat_row[headerOutOne] = 0;
                                    dtMat_row[headerOutTwo] = 0;
                                    dtMat_row[headerOutThree] = 0;
                                    dtMat_row[headerOutFour] = 0;

                                    dtMat_row[headerForecastOne] = -1;
                                    dtMat_row[headerForecastTwo] = -1;
                                    dtMat_row[headerForecastThree] = -1;
                                    dtMat_row[headerForecastFour] = -1;

                                    dtMat.Rows.Add(dtMat_row);
                                    forecastIndex++;
                                    #endregion
                                }

                            }

                        }
                    }
                    #endregion
                }
            }

            dtMat = AddDuplicates(dtMat);
            dtMat = calCartonStillNeed(dtMat);
            return dtMat;
        }

        public DataTable GetCartonBalanceData(string customer,string CartonCode)
        {
            #region Get Data From Database
            Text text = new Text();
            DataTable dt;
            DataTable dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());

            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
            }

            dt = RemoveDuplicates(dt);

            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";

            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;
            #endregion

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock, child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1;
                float child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item[dalItem.ItemCode].ToString();

                    if (itemCode.Equals("V02K81000"))
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());

                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    forecast_1 = forecast_1 <= -1 ? 0 : forecast_1;
                    forecast_2 = forecast_2 <= -1 ? 0 : forecast_2;
                    forecast_3 = forecast_3 <= -1 ? 0 : forecast_3;
                    forecast_4 = forecast_4 <= -1 ? 0 : forecast_4;

                    itemPartWeight = item["item_quo_pw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_pw_pcs"].ToString());
                    itemRunnerWeight = item["item_quo_rw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_rw_pcs"].ToString());

                    if (itemPartWeight == 0)
                        itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());

                    if (itemRunnerWeight == 0)
                        itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());

                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    currentMonthOut = 0;
                    if (dt_currentMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_currentMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                if (outRecord["trf_hist_to"].ToString().Equals(customer))
                                {
                                    currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);

                                }
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                if (outRecord["trf_hist_to"].ToString().Equals(customer))
                                {

                                    nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);

                                }
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_hist_to"].ToString().Equals(customer) && outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_hist_to"].ToString().Equals(customer) &&  outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    //calculate still need how many qty
                    //forecastnum = balance
                    #region Calculate Balance
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = readyStock - forecast_1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else
                    {
                        bal_2 = bal_1 - forecast_2 + nextMonthOut;
                    }

                    bal_3 = bal_2 - forecast_3;
                    bal_4 = bal_3 - forecast_4;
                    #endregion

                    #region child

                    if (ifGotChildIncludedPacking(itemCode, dtJoin))
                    {
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join[dalJoin.ParentCode].ToString().Equals(itemCode))
                            {
                                string childCat = Join[dalJoin.ChildCat].ToString();
                                if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Part))
                                {
                                    string childeCode = Join["child_code"].ToString();

                                    if(childeCode == "V37KM40RV")
                                    {
                                        float test = 0;
                                    }

                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join["join_qty"]);
                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());

                                    #region Calculate Balance
                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = childBal_2 + bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = childBal_3 + bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }
                                    #endregion

                                    //if got child, then loop child here
                                    if (ifGotChildIncludedPacking(Join["child_code"].ToString(), dtJoin))
                                    {
                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2[dalJoin.ParentCode].ToString().Equals(Join[dalJoin.ChildCode].ToString()) && Join2[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton) && Join2[dalJoin.ChildCode].ToString().Equals(CartonCode))
                                            {
                                                child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                child_child_join_qty = Convert.ToSingle(Join2[dalJoin.JoinQty]);
                                                int child_child_join_max = int.TryParse(Join2[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                                int child_child_join_min = int.TryParse(Join2[dalJoin.JoinMin].ToString(), out x) ? x : 0;

                                                child_child_join_max = child_child_join_max <= 0 ? 1 : child_child_join_max;
                                                child_child_join_min = child_child_join_min <= 0 ? 1 : child_child_join_min;

                                                int fullQty = 0;
                                                int notFullQty = 0;
                                                int childQty = 0;
                                                int ParentQty = 0;

                                                child_child_MB = Join2["child_mb"] == DBNull.Value ? "NULL" : Join2["child_mb"].ToString();
                                                child_child_mbRate = Join2["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join2["child_mb_rate"].ToString());

                                                child_child_Mat = text.Cat_Carton;

                                                #region Calculate Balance
                                                if (childBal_1 < 0)
                                                {
                                                    ParentQty = (int)childBal_1 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }
                                                    child_childBal_1 = child_child_ReadyStock - childQty;
                                                }
                                                else
                                                {
                                                    child_childBal_1 = child_child_ReadyStock;
                                                }

                                                if (childBal_2 > 0)
                                                {
                                                    child_childBal_2 = child_childBal_1;
                                                }
                                                else if (childBal_1 > 0)
                                                {
                                                    ParentQty = (int)childBal_2 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_2 = child_childBal_1 - childQty;
                                                }
                                                else
                                                {
                                                    //child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                    ParentQty = (int)(childBal_1 - childBal_2);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_2 = child_childBal_1 - childQty;
                                                }

                                                if (childBal_3 > 0)
                                                {
                                                    child_childBal_3 = child_childBal_2;
                                                }
                                                else if (childBal_2 > 0)
                                                {
                                                    ParentQty = (int)childBal_3 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_3 = child_childBal_2 - childQty;
                                                }
                                                else
                                                {

                                                    ParentQty = (int)(childBal_2 - childBal_3);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_3 = child_childBal_2 - childQty;
                                                }

                                                if (childBal_4 > 0)
                                                {
                                                    child_childBal_4 = child_childBal_3;
                                                }
                                                else if (childBal_3 > 0)
                                                {
                                                    ParentQty = (int)childBal_4 * -1;

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_4 = child_childBal_3 - childQty;
                                                }
                                                else
                                                {

                                                    ParentQty = (int)(childBal_3 - childBal_4);

                                                    fullQty = ParentQty / child_child_join_max;

                                                    notFullQty = ParentQty % child_child_join_max;

                                                    childQty = fullQty * (int)child_join_qty;

                                                    if (notFullQty >= child_child_join_min)
                                                    {
                                                        childQty += (int)child_join_qty;

                                                    }

                                                    child_childBal_4 = child_childBal_3 - childQty;
                                                }

                                                #endregion

                                                #region Add Data

                                                if(forecastIndex == 5)
                                                {
                                                    float test = 0;

                                                }

                                                string addingMaterialCode = Join2["child_code"].ToString();
                                                string addingItemCode = Join[dalJoin.ChildCode].ToString();
                                                string addingItemName = getItemNameFromDataTable(dt_ItemInfo, Join[dalJoin.ChildCode].ToString());

                                                dtMat_row = dtMat.NewRow();
                                                dtMat_row[headerIndex] = forecastIndex;
                                                dtMat_row[headerMat] = addingMaterialCode;
                                                dtMat_row[headerCode] = addingItemCode;
                                                dtMat_row[headerName] = addingItemName;
                                                dtMat_row[headerMB] = child_child_join_max;
                                                dtMat_row[headerMBRate] = child_child_join_qty;
                                                //dtMat_row[headerWeight] = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
                                                //dtMat_row[headerWastage] = Join2["child_wastage_allowed"].ToString();
                                                dtMat_row[headerReadyStock] = child_ReadyStock;
                                                dtMat_row[headerBalanceZero] = child_ReadyStock;
                                                dtMat_row[headerBalanceOne] = Convert.ToSingle(childBal_1);
                                                dtMat_row[headerBalanceTwo] = Convert.ToSingle(childBal_2);
                                                dtMat_row[headerBalanceThree] = Convert.ToSingle(childBal_3);
                                                dtMat_row[headerBalanceFour] = Convert.ToSingle(childBal_4);

                                                dtMat_row[headerOutOne] = 0;
                                                dtMat_row[headerOutTwo] = 0;
                                                dtMat_row[headerOutThree] = 0;
                                                dtMat_row[headerOutFour] = 0;

                                                dtMat_row[headerForecastOne] = -1;
                                                dtMat_row[headerForecastTwo] = -1;
                                                dtMat_row[headerForecastThree] = -1;
                                                dtMat_row[headerForecastFour] = -1;

                                                dtMat.Rows.Add(dtMat_row);
                                                forecastIndex++;
                                                #endregion
                                            }
                                        }
                                    }
                                }

                                else if (Join[dalJoin.ChildCat].ToString().Equals(text.Cat_Carton) && Join[dalJoin.ChildCode].ToString().Equals(CartonCode))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToSingle(Join[dalJoin.JoinQty]);
                                    int child_join_max = int.TryParse(Join[dalJoin.JoinMax].ToString(), out int x) ? x : 0;
                                    int child_join_min = int.TryParse(Join[dalJoin.JoinMin].ToString(), out x) ? x : 0;

                                    child_join_max = child_join_max <= 0 ? 1 : child_join_max;
                                    child_join_min = child_join_min <= 0 ? 1 : child_join_min;

                                    int fullQty = 0;
                                    int notFullQty = 0;
                                    int childQty = 0;
                                    int ParentQty = 0;

                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());
                                    child_Mat = text.Cat_Carton;

                                    #region Calculate Balance
                                    if (bal_1 < 0)
                                    {
                                        ParentQty = (int)bal_1 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }
                                        childBal_1 = child_ReadyStock - childQty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        ParentQty = (int)bal_2 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_2;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_2 = childBal_1 - childQty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        ParentQty = (int)bal_3 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_3;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_3 = childBal_2 - childQty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        ParentQty = (int)bal_4 * -1;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }
                                    else
                                    {
                                        ParentQty = (int)forecast_4;

                                        fullQty = ParentQty / child_join_max;

                                        notFullQty = ParentQty % child_join_max;

                                        childQty = fullQty * (int)child_join_qty;

                                        if (notFullQty >= child_join_min)
                                        {
                                            childQty += (int)child_join_qty;

                                        }

                                        childBal_4 = childBal_3 - childQty;
                                    }

                                    #endregion

                                    if (forecastIndex == 5)
                                    {
                                        float test = 0;

                                    }

                                    #region Add Data
                                    dtMat_row = dtMat.NewRow();
                                    dtMat_row[headerIndex] = forecastIndex;
                                    dtMat_row[headerMat] = Join["child_code"].ToString();
                                    dtMat_row[headerCode] = itemCode;
                                    dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                                    dtMat_row[headerMB] = child_join_max;
                                    dtMat_row[headerMBRate] = child_join_qty;
                                    //dtMat_row[headerWeight] = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                   // dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                    dtMat_row[headerReadyStock] = readyStock;
                                    dtMat_row[headerBalanceZero] = readyStock;
                                    dtMat_row[headerBalanceOne] = Convert.ToSingle(bal_1);
                                    dtMat_row[headerBalanceTwo] = Convert.ToSingle(bal_2);
                                    dtMat_row[headerBalanceThree] = Convert.ToSingle(bal_3);
                                    dtMat_row[headerBalanceFour] = Convert.ToSingle(bal_4);

                                    dtMat_row[headerOutOne] = 0;
                                    dtMat_row[headerOutTwo] = 0;
                                    dtMat_row[headerOutThree] = 0;
                                    dtMat_row[headerOutFour] = 0;

                                    dtMat_row[headerForecastOne] = -1;
                                    dtMat_row[headerForecastTwo] = -1;
                                    dtMat_row[headerForecastThree] = -1;
                                    dtMat_row[headerForecastFour] = -1;

                                    dtMat.Rows.Add(dtMat_row);
                                    forecastIndex++;
                                    #endregion
                                }

                            }

                        }
                    }
                    #endregion
                }
            }

            dtMat = AddDuplicates(dtMat);
            dtMat = calCartonStillNeed(dtMat);
            return dtMat;
        }

        private DataTable SummaryCalRepeatedData(DataTable dt)
        {
            double totalNeeded1 = 0;
            double totalNeeded2 = 0;
            double totalNeeded3 = 0;
            double totalNeeded4 = 0;

            bool colorChange = false;
            int colorOrder = 0;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                totalNeeded1 = 0;
                totalNeeded2 = 0;
                totalNeeded3 = 0;
                totalNeeded4 = 0;

                string rowReference = "(" + dt.Rows[i][headerIndex].ToString() + ")";
                int repeatedCount = 1;

                string firstItem = dt.Rows[i][headerPartCode].ToString();
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                double firstBal3 = double.TryParse(dt.Rows[i][headerBal3].ToString(), out firstBal3) ? firstBal3 : -0.001;
                double firstBal4 = double.TryParse(dt.Rows[i][headerBal4].ToString(), out firstBal4) ? firstBal4 : -0.001;

                string firstParentColor = dt.Rows[i][headerParentColor].ToString();

                string type = dt.Rows[i][headerType].ToString();

                if (firstItem.Equals("C84KXQ000"))
                {
                    float test = 0;
                }

                if (!(type.Equals(typeSingle) || type.Equals(typeParent)))
                {
                    dt.Rows[i][headerForecastType] = forecastType_Needed;
                }
                else
                {
                    dt.Rows[i][headerForecastType] = forecastType_Forecast;
                }

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();

                        if (firstItem.Equals(nextItem))
                        {
                            repeatedCount++;

                            rowReference += " (" + dt.Rows[j][headerIndex].ToString() + ")";

                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerForecast1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerForecast2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerForecast3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;
                            double nextNeededQty4 = double.TryParse(dt.Rows[j][headerForecast4].ToString(), out nextNeededQty4) ? nextNeededQty4 : 0;


                            nextNeededQty1 = nextNeededQty1 > 0 ? nextNeededQty1 : 0;
                            nextNeededQty2 = nextNeededQty2 > 0 ? nextNeededQty2 : 0;
                            nextNeededQty3 = nextNeededQty3 > 0 ? nextNeededQty3 : 0;
                            nextNeededQty4 = nextNeededQty4 > 0 ? nextNeededQty4 : 0;

                            dt.Rows[j][headerBal1] = DBNull.Value;
                            dt.Rows[j][headerBal2] = DBNull.Value;
                            dt.Rows[j][headerBal3] = DBNull.Value;
                            dt.Rows[j][headerBal4] = DBNull.Value;

                            totalNeeded1 += nextNeededQty1;
                            totalNeeded2 += nextNeededQty2;
                            totalNeeded3 += nextNeededQty3;
                            totalNeeded4 += nextNeededQty4;

                            //dt.Rows[i][headerBackColor] = (ColorSet)colorOrder;
                            //dt.Rows[j][headerBackColor] = (ColorSet)colorOrder;
                            dt.Rows[j][headerBalType] = balType_Repeated;
                            colorChange = true;
                        }
                    }



                    if (colorChange)
                    {
                        if (totalNeeded1 < 0)
                        {
                            totalNeeded1 = 0;
                        }

                        if (totalNeeded2 < 0)
                        {
                            totalNeeded2 = 0;
                        }

                        if (totalNeeded3 < 0)
                        {
                            totalNeeded3 = 0;
                        }

                        if (totalNeeded4 < 0)
                        {
                            totalNeeded4 = 0;
                        }
                        dt.Rows[i][headerBal1] = firstBal1 - totalNeeded1;
                        dt.Rows[i][headerBal2] = firstBal2 - totalNeeded1 - totalNeeded2;
                        dt.Rows[i][headerBal3] = firstBal3 - totalNeeded1 - totalNeeded2 - totalNeeded3;
                        dt.Rows[i][headerBal4] = firstBal4 - totalNeeded1 - totalNeeded2 - totalNeeded3 - totalNeeded4;

                        dt.Rows[i][headerBalType] = balType_Total;
                        dt.Rows[i][headerRowReference] = rowReference;
                        colorOrder++;

                        //if (colorOrder > Enum.GetValues(typeof(ColorSet)).Cast<int>().Max())
                        //{
                        //    colorOrder = 0;

                        //}
                        colorChange = false;
                    }



                }
            }

            dt = RepeatedRowReferenceRemark(dt);

            return ChildBalChecking(dt);


        }

        private DataTable RepeatedRowReferenceRemark(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                string firstItem = dt.Rows[i][headerPartCode].ToString();
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                double firstBal3 = double.TryParse(dt.Rows[i][headerBal3].ToString(), out firstBal3) ? firstBal3 : -0.001;
                double firstBal4 = double.TryParse(dt.Rows[i][headerBal4].ToString(), out firstBal4) ? firstBal4 : -0.001;

                string rowReference = dt.Rows[i][headerRowReference].ToString();

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();

                        if (firstItem.Equals(nextItem))
                        {
                            dt.Rows[j][headerRowReference] = rowReference;
                        }
                    }
                }
            }

            return dt;


        }

        private DataTable ChildBalChecking(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double forecast1 = double.TryParse(dt.Rows[i][headerForecast1].ToString(), out forecast1) ? forecast1 : 0;
                double forecast2 = double.TryParse(dt.Rows[i][headerForecast2].ToString(), out forecast2) ? forecast2 : 0;
                double forecast3 = double.TryParse(dt.Rows[i][headerForecast3].ToString(), out forecast3) ? forecast3 : 0;
                double forecast4 = double.TryParse(dt.Rows[i][headerForecast4].ToString(), out forecast4) ? forecast4 : 0;

                if (forecast1 < 0)
                {
                    dt.Rows[i][headerForecast1] = 0;
                }

                if (forecast2 < 0)
                {
                    dt.Rows[i][headerForecast2] = 0;
                }

                if (forecast3 < 0)
                {
                    dt.Rows[i][headerForecast3] = 0;
                }

                if (forecast4 < 0)
                {
                    dt.Rows[i][headerForecast4] = 0;
                }

                double totalNeeded1 = 0;
                double totalNeeded2 = 0;
                double totalNeeded3 = 0;
                double totalNeeded4 = 0;

                string itemCode = dt.Rows[i][headerPartCode].ToString();
                string itemType = dt.Rows[i][headerType].ToString();
                string balType = dt.Rows[i][headerBalType].ToString();
                string ForecastType = dt.Rows[i][headerForecastType].ToString();
                double itemStock = double.TryParse(dt.Rows[i][headerReadyStock].ToString(), out itemStock) ? itemStock : 0;

                //loop and find "TOTAL"  and not parent item
                if (itemType != typeParent && !ForecastType.Equals(forecastType_Forecast) && balType.Equals(balType_Total))
                {
                    bool itemBalToChange = false;

                    //load Joint Data Table and find Parent(s)
                    foreach (DataRow row in dt_Join.Rows)
                    {
                        string childCode = row[dalJoin.JoinChild].ToString();

                        if (childCode.Equals(itemCode))
                        {
                            string parentCode = row[dalJoin.JoinParent].ToString();

                            float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float f) ? f : 1;

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string seachingItemCode = dt.Rows[j][headerPartCode].ToString();
                                string seachingbalType = dt.Rows[j][headerBalType].ToString();
                                string seachingItemType = dt.Rows[j][headerType].ToString();


                                //Find Parent Item With "Total"
                                if (seachingItemCode.Equals(parentCode) && (seachingItemType.Equals(typeParent) || (seachingbalType.Equals(balType_Total) && seachingItemType.Equals(typeChild))))
                                {
                                    double nextNeededQty1 = double.TryParse(dt.Rows[j][headerBal1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                                    double nextNeededQty2 = double.TryParse(dt.Rows[j][headerBal2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                                    double nextNeededQty3 = double.TryParse(dt.Rows[j][headerBal3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;
                                    double nextNeededQty4 = double.TryParse(dt.Rows[j][headerBal4].ToString(), out nextNeededQty4) ? nextNeededQty4 : 0;

                                    //Sum Up balance, and calculate total needed for Child (if balance>=0, needed for child =0; if balance <0, needed for child = bal*-1)
                                    totalNeeded1 += nextNeededQty1 < 0 ? nextNeededQty1 * -1 * joinQty : 0;
                                    totalNeeded2 += nextNeededQty2 < 0 ? nextNeededQty2 * -1 * joinQty : 0;
                                    totalNeeded3 += nextNeededQty3 < 0 ? nextNeededQty3 * -1 * joinQty : 0;
                                    totalNeeded4 += nextNeededQty4 < 0 ? nextNeededQty4 * -1 * joinQty : 0;

                                    itemBalToChange = true;
                                    break;
                                }

                            }


                        }
                    }

                    if (itemBalToChange)
                    {
                        if (totalNeeded1 % 1 > 0)
                        {
                            totalNeeded1 = (float)Math.Round(totalNeeded1 * 100f) / 100f;
                        }

                        if (totalNeeded2 % 1 > 0)
                        {
                            totalNeeded2 = (float)Math.Round(totalNeeded2 * 100f) / 100f;

                        }

                        if (totalNeeded3 % 1 > 0)
                        {
                            totalNeeded3 = (float)Math.Round(totalNeeded3 * 100f) / 100f;

                        }

                        if (totalNeeded4 % 1 > 0)
                        {
                            totalNeeded4 = (float)Math.Round(totalNeeded4 * 100f) / 100f;

                        }

                        dt.Rows[i][headerBal1] = itemStock - totalNeeded1;
                        dt.Rows[i][headerBal2] = itemStock - totalNeeded2;
                        dt.Rows[i][headerBal3] = itemStock - totalNeeded3;
                        dt.Rows[i][headerBal4] = itemStock - totalNeeded4;
                    }

                }




            }

            return dt;
        }

        private DataTable CalRepeatedData(DataTable dt)
        {
            double totalNeeded1 = 0;
            double totalNeeded2 = 0;
            double totalNeeded3 = 0;

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                totalNeeded1 = 0;
                totalNeeded2 = 0;
                totalNeeded3 = 0;

                string firstItem = dt.Rows[i][headerPartCode].ToString();
                double firstBal1 = double.TryParse(dt.Rows[i][headerBal1].ToString(), out firstBal1) ? firstBal1 : -0.001;
                double firstBal2 = double.TryParse(dt.Rows[i][headerBal2].ToString(), out firstBal2) ? firstBal2 : -0.001;
                string firstParentColor = dt.Rows[i][headerParentColor].ToString();
                string type = dt.Rows[i][headerType].ToString();

                if (!(type.Equals(typeSingle) || type.Equals(typeParent)))
                {
                    dt.Rows[i][headerForecastType] = forecastType_Needed;
                }
                else
                {
                    dt.Rows[i][headerForecastType] = forecastType_Forecast;
                }

                if (!string.IsNullOrEmpty(firstItem) && firstBal1 != -0.001)
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string nextItem = dt.Rows[j][headerPartCode].ToString();

                        if (firstItem.Equals(nextItem))
                        {
                            double nextNeededQty1 = double.TryParse(dt.Rows[j][headerForecast1].ToString(), out nextNeededQty1) ? nextNeededQty1 : 0;
                            double nextNeededQty2 = double.TryParse(dt.Rows[j][headerForecast2].ToString(), out nextNeededQty2) ? nextNeededQty2 : 0;
                            double nextNeededQty3 = double.TryParse(dt.Rows[j][headerForecast3].ToString(), out nextNeededQty3) ? nextNeededQty3 : 0;

                            dt.Rows[j][headerBal1] = DBNull.Value;
                            dt.Rows[j][headerBal2] = DBNull.Value;

                            totalNeeded1 += nextNeededQty1;
                            totalNeeded2 += nextNeededQty2;
                            totalNeeded3 += nextNeededQty3;
                        }
                    }
                }
            }

            return dt;


        }

        private void LoadChild(DataTable dt_Data, dataTrfBLL uParentData, float subIndex)
        {
            DataRow dt_Row;
            Text text = new Text();
            dataTrfBLL uChildData = new dataTrfBLL();

            double index = uParentData.index + subIndex;

            foreach (DataRow row in dt_Join.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if (parentCode.Equals(uParentData.part_code))
                {
                    string childCode = row[dalJoin.JoinChild].ToString();

                    DataRow row_Item = getDataRowFromDataTable(dt_Item, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    if (itemMatch)
                    {

                        float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float i) ? Convert.ToSingle(row[dalJoin.JoinQty].ToString()) : 1;

                        dt_Row = dt_Data.NewRow();

                        uChildData.part_code = row_Item[dalItem.ItemCode].ToString();

                        uChildData.index = index;
                        uChildData.part_name = row_Item[dalItem.ItemName].ToString();
                        uChildData.color_mat = row_Item[dalItem.ItemMBatch].ToString();
                        uChildData.color = row_Item[dalItem.ItemColor].ToString();
                        uChildData.raw_mat = row_Item[dalItem.ItemMaterial].ToString();
                        uChildData.pw_per_shot = row_Item[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProPWShot]);
                        uChildData.rw_per_shot = row_Item[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProRWShot]);
                        uChildData.cavity = row_Item[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row_Item[dalItem.ItemCavity]);
                        uChildData.cavity = uChildData.cavity == 0 ? 1 : uChildData.cavity;
                        uChildData.ready_stock = row_Item[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemStock]);

                        if (uParentData.bal1 >= 0)
                        {
                            uChildData.forecast1 = 0;
                        }
                        else
                        {
                            //needed = parent bal1(if <0) * joinQty
                            uChildData.forecast1 = uParentData.bal1 * -1 * joinQty;
                        }

                        if (uParentData.bal2 >= 0)
                        {
                            uChildData.forecast2 = 0;

                            if (uParentData.bal2 - uParentData.forecast3 < 0)
                            {
                                uChildData.forecast3 = (uParentData.bal2 - uParentData.forecast3) * joinQty;
                            }
                            else
                            {
                                uChildData.forecast3 = 0;
                            }
                        }
                        else
                        {
                            if (uParentData.bal1 < 0)
                            {
                                uChildData.forecast2 = uParentData.forecast2 * joinQty;
                            }
                            else
                            {
                                uChildData.forecast2 = uParentData.bal2 * -1 * joinQty;
                            }

                            uChildData.forecast3 = uParentData.forecast3 * joinQty;
                        }

                        uChildData.bal1 = uChildData.ready_stock - uChildData.forecast1;

                        uChildData.bal2 = uChildData.bal1 - uChildData.forecast2;

                        if (!uChildData.color.Equals(uChildData.color_mat) && !string.IsNullOrEmpty(uChildData.color_mat))
                        {
                            uChildData.color_mat += " (" + uParentData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uChildData.index;
                        dt_Row[headerType] = typeChild;
                        dt_Row[headerRawMat] = uChildData.raw_mat;
                        dt_Row[headerPartCode] = uChildData.part_code;
                        dt_Row[headerPartName] = uChildData.part_name;
                        dt_Row[headerColorMat] = uChildData.color_mat;
                        dt_Row[headerPartWeight] = uChildData.pw_per_shot / uChildData.cavity + " (" + (uChildData.rw_per_shot / uChildData.cavity) + ")";
                        dt_Row[headerReadyStock] = uChildData.ready_stock;

                        dt_Row[headerForecast1] = uChildData.forecast1;
                        dt_Row[headerForecast2] = uChildData.forecast2;
                        dt_Row[headerForecast3] = uChildData.forecast3;

                        dt_Row[headerBal1] = uChildData.bal1;
                        dt_Row[headerBal2] = uChildData.bal2;

                        int assembly = row_Item[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemAssemblyCheck]);
                        int production = row_Item[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemProductionCheck]);

                        bool gotChild = ifGotChild2(uChildData.part_code, dt_Join);

                        dt_Data.Rows.Add(dt_Row);

                        index += subIndex;

                        //check if got child part also
                        if (gotChild)
                        {
                            LoadChild(dt_Data, uChildData, subIndex / 10);
                        }
                    }

                }

            }

        }

        private void SummaryLoadChild(DataTable dt_Item, DataTable dt_Join, DataTable dt_Data, dataTrfBLL uParentData, float subIndex)
        {
            Text text = new Text();

            DataRow dt_Row;

            dataTrfBLL uChildData = new dataTrfBLL();

            double index = uParentData.index + subIndex;

            foreach (DataRow row in dt_Join.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if (parentCode.Equals(uParentData.part_code))
                {
                    string childCode = row[dalJoin.JoinChild].ToString();


                    DataRow row_Item = getDataRowFromDataTable(dt_Item, childCode);

                    bool itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part);

                    itemMatch = row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_Part) || row_Item[dalItem.ItemCat].ToString().Equals(text.Cat_SubMat);

                    if (itemMatch)
                    {
                        float joinQty = float.TryParse(row[dalJoin.JoinQty].ToString(), out float i) ? Convert.ToSingle(row[dalJoin.JoinQty].ToString()) : 1;

                        dt_Row = dt_Data.NewRow();

                        uChildData.part_code = row_Item[dalItem.ItemCode].ToString();

                        uChildData.index = index;
                        uChildData.part_name = row_Item[dalItem.ItemName].ToString();
                        uChildData.color_mat = row_Item[dalItem.ItemMBatch].ToString();
                        uChildData.color = row_Item[dalItem.ItemColor].ToString();
                        uChildData.raw_mat = row_Item[dalItem.ItemMaterial].ToString();
                        uChildData.pw_per_shot = row_Item[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProPWShot]);
                        uChildData.rw_per_shot = row_Item[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemProRWShot]);
                        uChildData.cavity = row_Item[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row_Item[dalItem.ItemCavity]);
                        uChildData.cavity = uChildData.cavity == 0 ? 1 : uChildData.cavity;
                        uChildData.ready_stock = row_Item[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row_Item[dalItem.ItemStock]);
                        uParentData.estimate = 0;

                        float stock = uChildData.ready_stock;

                        if (stock % 1 > 0)
                        {
                            stock = (float)Math.Round(stock * 100f) / 100f;

                            uChildData.ready_stock = stock;
                        }


                        if (uParentData.bal1 >= 0)
                        {
                            uChildData.forecast1 = 0;
                        }
                        else
                        {
                            uChildData.forecast1 = uParentData.bal1 * -1 * joinQty;
                        }

                        if (uParentData.bal2 >= 0)
                        {
                            uChildData.forecast2 = 0;

                            if (uParentData.forecast3 <= -1)
                            {
                                if (uParentData.bal2 - uParentData.estimate < 0)
                                {
                                    uChildData.forecast3 = (uParentData.bal2 - uParentData.estimate) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = 0;
                                }
                            }
                            else
                            {
                                if (uParentData.bal2 - uParentData.forecast3 < 0)
                                {
                                    uChildData.forecast3 = (uParentData.bal2 - uParentData.forecast3) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = 0;
                                }
                            }


                        }
                        else
                        {
                            if (uParentData.bal1 < 0)
                            {
                                if (uParentData.forecast2 <= -1)
                                {
                                    uChildData.forecast2 = uParentData.estimate * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast2 = uParentData.forecast2 * joinQty;
                                }

                            }
                            else
                            {
                                uChildData.forecast2 = uParentData.bal2 * -1 * joinQty;
                            }

                            if (uParentData.forecast3 <= -1)
                            {
                                uChildData.forecast3 = uParentData.estimate * joinQty;
                            }
                            else
                            {
                                uChildData.forecast3 = uParentData.forecast3 * joinQty;
                            }


                        }

                        if (uParentData.bal3 >= 0)
                        {
                            uChildData.forecast3 = 0;

                            if (uParentData.forecast4 <= -1)
                            {
                                if (uParentData.bal3 - uParentData.estimate < 0)
                                {
                                    uChildData.forecast4 = (uParentData.bal3 - uParentData.estimate) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast4 = 0;
                                }
                            }
                            else
                            {
                                if (uParentData.bal3 - uParentData.forecast4 < 0)
                                {
                                    uChildData.forecast4 = (uParentData.bal3 - uParentData.forecast4) * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast4 = 0;
                                }
                            }


                        }
                        else
                        {
                            if (uParentData.bal2 < 0)
                            {
                                if (uParentData.forecast3 <= -1)
                                {
                                    uChildData.forecast3 = uParentData.estimate * joinQty;
                                }
                                else
                                {
                                    uChildData.forecast3 = uParentData.forecast3 * joinQty;
                                }

                            }
                            else
                            {
                                uChildData.forecast3 = uParentData.bal3 * -1 * joinQty;
                            }

                            if (uParentData.forecast4 <= -1)
                            {
                                uChildData.forecast4 = uParentData.estimate * joinQty;
                            }
                            else
                            {
                                uChildData.forecast4 = uParentData.forecast4 * joinQty;
                            }


                        }

                        uChildData.forecast1 = uChildData.forecast1 < 0 ? uChildData.forecast1 * -1 : uChildData.forecast1;
                        uChildData.forecast2 = uChildData.forecast2 < 0 ? uChildData.forecast2 * -1 : uChildData.forecast2;
                        uChildData.forecast3 = uChildData.forecast3 < 0 ? uChildData.forecast3 * -1 : uChildData.forecast3;
                        uChildData.forecast4 = uChildData.forecast4 < 0 ? uChildData.forecast4 * -1 : uChildData.forecast4;

                        uChildData.bal1 = uChildData.ready_stock - uChildData.forecast1;

                        uChildData.bal2 = uChildData.bal1 - uChildData.forecast2;

                        uChildData.bal3 = uChildData.bal2 - uChildData.forecast3;

                        uChildData.bal4 = uChildData.bal3 - uChildData.forecast4;


                        if (!uChildData.color.Equals(uChildData.color_mat) && !string.IsNullOrEmpty(uChildData.color_mat))
                        {
                            uChildData.color_mat += " (" + uParentData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uChildData.index;
                        dt_Row[headerType] = typeChild;
                        dt_Row[headerRawMat] = uChildData.raw_mat;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row_Item[dalItem.ItemCat].ToString();
                        dt_Row[headerPartCode] = uChildData.part_code;
                        dt_Row[headerPartName] = uChildData.part_name;
                        dt_Row[headerColorMat] = uChildData.color_mat;
                        dt_Row[headerPartWeight] = uChildData.pw_per_shot / uChildData.cavity + " (" + (uChildData.rw_per_shot / uChildData.cavity) + ")";



                        dt_Row[headerReadyStock] = uChildData.ready_stock;

                        dt_Row[headerForecast1] = uChildData.forecast1 % 1 > 0 ? (float)Math.Round(uChildData.forecast1 * 100f) / 100f : uChildData.forecast1;
                        dt_Row[headerForecast2] = uChildData.forecast2 % 1 > 0 ? (float)Math.Round(uChildData.forecast2 * 100f) / 100f : uChildData.forecast2;
                        dt_Row[headerForecast3] = uChildData.forecast3 % 1 > 0 ? (float)Math.Round(uChildData.forecast3 * 100f) / 100f : uChildData.forecast3;
                        dt_Row[headerForecast4] = uChildData.forecast4 % 1 > 0 ? (float)Math.Round(uChildData.forecast4 * 100f) / 100f : uChildData.forecast4;

                        dt_Row[headerBal1] = uChildData.bal1;
                        dt_Row[headerBal2] = uChildData.bal2;
                        dt_Row[headerBal3] = uChildData.bal3;
                        dt_Row[headerBal4] = uChildData.bal4;

                        int assembly = row_Item[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemAssemblyCheck]);
                        int production = row_Item[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row_Item[dalItem.ItemProductionCheck]);

                        bool gotChild = ifGotNotPackagingChild(uChildData.part_code, dt_Join, dt_Item);

                        if (assembly == 1 && production == 0 && gotChild)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = ProductionMarking;
                        }
                        else if (assembly == 1 && production == 1 && gotChild)
                        {
                            dt_Row[headerParentColor] = ProductionAndAssemblyMarking;
                        }

                        dt_Data.Rows.Add(dt_Row);

                        index += subIndex;

                        //check if got child part also
                        if (gotChild)
                        {
                            SummaryLoadChild(dt_Item,dt_Join,dt_Data, uChildData, subIndex / 10);
                        }
                    }

                }

            }
        }

        private float GetDeliveredQtyByMonth(string itemCode, string customer, int month,int year, DataTable dt_TrfHist, DataTable dt_PMMADate)
        {
            float deliveredQty = 0;

            DateTime start = GetPMMAStartDate(month, year, dt_PMMADate);
            DateTime end = GetPMMAEndDate(month, year, dt_PMMADate);

            if (!customer.Equals(getCustName(1)))
            {
                start = new DateTime(year, month, 1);
                end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
           
            foreach (DataRow row in dt_TrfHist.Rows)
            {
                string item = row[dalTrfHist.TrfItemCode].ToString();

                if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                {
                    DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                    string cust = row[dalTrfHist.TrfTo].ToString();

                    if (trfDate >= start && trfDate <= end && cust.Equals(customer))
                    {
                        deliveredQty += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                    }
                }
            }


            return deliveredQty;
        }

        public DataTable Test(string customer)
        {
            frmLoading.ShowLoadingScreen();
            DataTable dt_Data = NewOrderAlertDataTable();

            string keywords = getCustName(1);

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                
                DataRow dt_Row;

                DataTable dt = dalItemCust.custSearch(keywords);

                DataTable dt_ItemForecast = dalItemForecast.Select(getCustID(keywords).ToString());

                DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerSearch(keywords);

                DataTable dt_PMMADate = dalPmmaDate.Select();

                dt_Join = dalJoin.SelectAll();
                dt_Item = dalItem.Select();

                int index = 1;

                dt.DefaultView.Sort = "item_name ASC";

                dt = dt.DefaultView.ToTable();

                #region load single part
                //normal speed
                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    if (assembly == 0 && production == 0)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        uData.forecast1 = GetForecastQty(dt_ItemForecast, uData.part_code, 1);
                        uData.forecast2 = GetForecastQty(dt_ItemForecast, uData.part_code, 2);
                        uData.forecast3 = GetForecastQty(dt_ItemForecast, uData.part_code, 3);

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //uData.deliveredOut = GetMaxOut(uData.part_code, keywords, 0, dt_TrfHist, dt_PMMADate);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (uData.forecast1 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate;
                            }
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                        dt_Data.Rows.Add(dt_Row);
                        index++;
                        //loadChild(dt_Data, uData);
                    }
                }

                #endregion

                #region load assembly part
                //slow speed
                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    //check if got child part also
                    if ((assembly == 1 || production == 1) && ifGotChild2(uData.part_code, dt_Join))
                    {
                        dt_Row = dt_Data.NewRow();
                        dt_Data.Rows.Add(dt_Row);

                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        uData.forecast1 = GetForecastQty(dt_ItemForecast, uData.part_code, 1);
                        uData.forecast2 = GetForecastQty(dt_ItemForecast, uData.part_code, 2);
                        uData.forecast3 = GetForecastQty(dt_ItemForecast, uData.part_code, 3);

                        //waiting to fix///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //uData.deliveredOut = GetMaxOut(uData.part_code, keywords, 0, dt_TrfHist, dt_PMMADate);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (uData.forecast1 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate;
                            }
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (keywords.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;

                       

                        dt_Data.Rows.Add(dt_Row);
                        index++;

                        //load child
                        LoadChild(dt_Data, uData, 0.1f);
                    }
                }

                #endregion

                if (dt_Data.Rows.Count > 0)
                {
                    //check repeated data: normal-slow speed
                    dt_Data = CalRepeatedData(dt_Data);

                }
            }


            frmLoading.CloseForm();

            return dt_Data;
        }

        public DataTable InsertZeroCostMaterialUsedData(string customer)
        {
            DataTable dt;
            DataTable dt_ItemForecast;

            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
                dt_ItemForecast = dalItemForecast.Select();
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
                dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());
            }

            dt = RemoveDuplicates(dt);
            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";
            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                #region variable setting
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock, child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1, child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();

                string start = GetPMMAStartDate(DateTime.Now.Month, DateTime.Now.Year).ToString("yyyy/MM/dd");
                string end = GetPMMAEndDate(DateTime.Now.Month, DateTime.Now.Year).ToString("yyyy/MM/dd");
                

                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);

                int month = DateTime.ParseExact(nextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);

                month = DateTime.ParseExact(nextNextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);

                month = DateTime.ParseExact(nextNextNextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();
                #endregion

                foreach (DataRow item in dt.Rows)
                {
                    itemCode = item["item_code"].ToString();

                    if(itemCode == "A0LK190A0")
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());

                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    forecast_1 = forecast_1 <= -1 ? 0 : forecast_1;
                    forecast_2 = forecast_2 <= -1 ? 0 : forecast_2;
                    forecast_3 = forecast_3 <= -1 ? 0 : forecast_3;
                    forecast_4 = forecast_4 <= -1 ? 0 : forecast_4;

                    itemPartWeight = item[dalItem.ItemQuoPWPcs] == DBNull.Value ? item[dalItem.ItemProPWPcs] == DBNull.Value ? 0 : Convert.ToSingle(item[dalItem.ItemProPWPcs].ToString()) : Convert.ToSingle(item[dalItem.ItemQuoPWPcs].ToString());
                    itemRunnerWeight = item[dalItem.ItemQuoRWPcs] == DBNull.Value ? item[dalItem.ItemProRWPcs] == DBNull.Value ? 0 : Convert.ToSingle(item[dalItem.ItemProRWPcs].ToString()) : Convert.ToSingle(item[dalItem.ItemQuoRWPcs].ToString());
                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    DataTable dt_Out = dalTrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

                    currentMonthOut = 0;
                    // if (dt_currentMonthTrfOutHist.Rows.Count > 0)
                    if (dt_Out.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_Out.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    #region calculate still need how many qty
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = 0;//bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = forecast_1 * -1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else if(bal_1 < 0)
                    {
                        bal_2 = bal_1 + forecast_2*-1 + nextMonthOut;
                    }
                    else
                    {
                        bal_2 = forecast_2 * -1 + nextMonthOut;
                    }

                    if (nextNextMonthOut >= forecast_3)
                    {
                        bal_3 = bal_2;
                    }
                    else if (bal_2 < 0)
                    {
                        bal_3 = bal_2 + forecast_3 * -1 + nextNextMonthOut;
                    }
                    else
                    {
                        bal_3 = forecast_3 * -1 + nextNextMonthOut;
                    }

                    if (nextNextNextMonthOut >= forecast_4)
                    {
                        bal_4 = bal_3;
                    }
                    else if (bal_3 < 0)
                    {
                        bal_4 = bal_3 + forecast_4 * -1 + nextNextNextMonthOut;
                    }
                    else
                    {
                        bal_4 = forecast_4 * -1 + nextNextNextMonthOut;
                    }
                    #endregion

                    #region child
                    if (ifGotChild(itemCode, dtJoin))
                    {
                        //if production type item(injection), add parent also
                        if (!item["item_assembly"].ToString().Equals("True") && item["item_production"].ToString().Equals("True"))
                        {
                            dtMat_row = dtMat.NewRow();
                            dtMat_row[headerIndex] = forecastIndex;
                            dtMat_row[headerMat] = item["item_material"].ToString();
                            dtMat_row[headerCode] = itemCode;
                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                            dtMat_row[headerMB] = MB;
                            dtMat_row[headerMBRate] = mbRate;
                            dtMat_row[headerWeight] = itemPartWeight + itemRunnerWeight;
                            dtMat_row[headerWastage] = wastageAllowed;
                            dtMat_row[headerReadyStock] = readyStock;
                            dtMat_row[headerBalanceZero] = readyStock;
                            dtMat_row[headerBalanceOne] = Convert.ToInt32(bal_1);
                            dtMat_row[headerBalanceTwo] = Convert.ToInt32(bal_2);
                            dtMat_row[headerBalanceThree] = Convert.ToInt32(bal_3);
                            dtMat_row[headerBalanceFour] = Convert.ToInt32(bal_4);

                            dtMat_row[headerOutOne] = currentMonthOut;
                            dtMat_row[headerOutTwo] = nextMonthOut;
                            dtMat_row[headerOutThree] = nextNextMonthOut;
                            dtMat_row[headerOutFour] = nextNextNextMonthOut;

                            dtMat_row[headerForecastOne] = forecast_1;
                            dtMat_row[headerForecastTwo] = forecast_2;
                            dtMat_row[headerForecastThree] = forecast_3;
                            dtMat_row[headerForecastFour] = forecast_4;

                            dtMat.Rows.Add(dtMat_row);
                            forecastIndex++;
                        }

                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join["parent_code"].ToString().Equals(itemCode))
                            {
                                string childCode = Join["child_code"].ToString();

                                if (childCode == "V44KM4000-M")
                                {
                                    float test2 = 0;
                                }

                                if (Join["child_cat"].ToString().Equals("Part") || Join["child_cat"].ToString().Equals("Sub Material"))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToInt32(Join["join_qty"]);
                                    child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                    child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());

                                    if (Join["child_cat"].ToString().Equals("Sub Material"))
                                    {
                                        child_Mat = "Sub Material";
                                    }
                                    else
                                    {
                                        child_Mat = Join["child_material"].ToString();

                                        
                                    }

                                    #region CALCULATE BAL
                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = bal_1 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_1 = 0;
                                        //childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = bal_2 * child_join_qty;
                                        //childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }

                                    #endregion

                                    //if got child, then loop child here
                                    if (ifGotChild(Join["child_code"].ToString(), dtJoin))
                                    {
                                        if (!Join["child_assembly"].ToString().Equals("True") && Join["child_production"].ToString().Equals("True"))
                                        {
                                            //a.item_quo_pw_pcs as child_quo_part_weight ,
                                            //a.item_quo_rw_pcs as child_quo_runner_weight ,
                                            float childPartWeight = float.TryParse(Join["child_quo_part_weight"].ToString(), out float j)? j : 0;

                                            if(childPartWeight == 0)
                                            {
                                                childPartWeight = float.TryParse(Join["child_part_weight"].ToString(), out  j) ? j : 0;
                                            }

                                            float childRunerWeight = float.TryParse(Join["child_quo_runner_weight"].ToString(), out  j) ? j : 0;

                                            if (childRunerWeight == 0)
                                            {
                                                childRunerWeight = float.TryParse(Join["child_runner_weight"].ToString(), out  j) ? j : 0;
                                            }

                                            dtMat_row = dtMat.NewRow();
                                            dtMat_row[headerIndex] = forecastIndex;
                                            dtMat_row[headerMat] = child_Mat;
                                            dtMat_row[headerCode] = Join["child_code"].ToString();
                                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                            dtMat_row[headerMB] = child_MB;
                                            dtMat_row[headerMBRate] = child_mbRate;
                                            dtMat_row[headerWeight] = childPartWeight + childRunerWeight;
                                            dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                            dtMat_row[headerReadyStock] = child_ReadyStock;
                                            dtMat_row[headerBalanceZero] = child_ReadyStock;
                                            dtMat_row[headerBalanceOne] = Convert.ToInt32(childBal_1);
                                            dtMat_row[headerBalanceTwo] = Convert.ToInt32(childBal_2);
                                            dtMat_row[headerBalanceThree] = Convert.ToInt32(childBal_3);
                                            dtMat_row[headerBalanceFour] = Convert.ToInt32(childBal_4);

                                            dtMat_row[headerOutOne] = 0;
                                            dtMat_row[headerOutTwo] = 0;
                                            dtMat_row[headerOutThree] = 0;
                                            dtMat_row[headerOutFour] = 0;

                                            dtMat_row[headerForecastOne] = -1;
                                            dtMat_row[headerForecastTwo] = -1;
                                            dtMat_row[headerForecastThree] = -1;
                                            dtMat_row[headerForecastFour] = -1;

                                            dtMat.Rows.Add(dtMat_row);
                                            forecastIndex++;
                                        }

                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2["parent_code"].ToString().Equals(Join["child_code"].ToString()))
                                            {
                                                if (Join2["child_code"].ToString() == "V44KM4000-M")
                                                {
                                                    float test2 = 0;
                                                }

                                                if (Join2["child_cat"].ToString().Equals("Part") || Join2["child_cat"].ToString().Equals("Sub Material"))
                                                {
                                                    if (true)
                                                    {
                                                        child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                        child_child_join_qty = Convert.ToInt32(Join2["join_qty"]);
                                                        child_child_MB = Join2["child_mb"] == DBNull.Value ? "NULL" : Join2["child_mb"].ToString();
                                                        child_child_mbRate = Join2["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join2["child_mb_rate"].ToString());

                                                        if (Join2["child_cat"].ToString().Equals("Sub Material"))
                                                        {
                                                            child_child_Mat = "Sub Material";
                                                        }
                                                        else
                                                        {
                                                            child_child_Mat = Join2["child_material"].ToString();
                                                        }

                                                        if (childBal_1 < 0)
                                                        {
                                                            child_childBal_1 = childBal_1 * child_child_join_qty;
                                                        }
                                                        else
                                                        {
                                                            child_childBal_1 = 0; //child_childBal_1 = child_child_ReadyStock;
                                                        }

                                                        if (childBal_2 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1;
                                                        }
                                                        else if (childBal_1 > 0)
                                                        {
                                                            child_childBal_2 = childBal_2 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                        }

                                                        if (childBal_3 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2;
                                                        }
                                                        else if (childBal_2 > 0)
                                                        {
                                                            child_childBal_3 = childBal_3 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_3 = child_childBal_2 - (childBal_2 - childBal_3) * child_child_join_qty;
                                                        }

                                                        if (childBal_4 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3;
                                                        }
                                                        else if (childBal_3 > 0)
                                                        {
                                                            child_childBal_4 = childBal_4 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_4 = child_childBal_3 - (childBal_3 - childBal_4) * child_child_join_qty;
                                                        }

                                                        float childPartWeight = float.TryParse(Join2["child_quo_part_weight"].ToString(), out float j) ? j : 0;

                                                        if (childPartWeight == 0)
                                                        {
                                                            childPartWeight = float.TryParse(Join2["child_part_weight"].ToString(), out  j) ? j : 0;
                                                        }

                                                        float childRunerWeight = float.TryParse(Join2["child_quo_runner_weight"].ToString(), out j) ? j : 0;

                                                        if (childRunerWeight == 0)
                                                        {
                                                            childRunerWeight = float.TryParse(Join2["child_runner_weight"].ToString(), out j) ? j : 0;
                                                        }

                                                        dtMat_row = dtMat.NewRow();
                                                        dtMat_row[headerIndex] = forecastIndex;
                                                        dtMat_row[headerMat] = child_child_Mat;
                                                        dtMat_row[headerCode] = Join2["child_code"].ToString();
                                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                        dtMat_row[headerMB] = child_child_MB;
                                                        dtMat_row[headerMBRate] = child_child_mbRate;
                                                        dtMat_row[headerWeight] = childPartWeight + childRunerWeight;

                                                        dtMat_row[headerWastage] = Join2["child_wastage_allowed"].ToString();
                                                        dtMat_row[headerReadyStock] = child_child_ReadyStock;
                                                        dtMat_row[headerBalanceZero] = child_child_ReadyStock;
                                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(child_childBal_1);
                                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(child_childBal_2);
                                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(child_childBal_3);
                                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(child_childBal_4);

                                                        dtMat_row[headerOutOne] = 0;
                                                        dtMat_row[headerOutTwo] = 0;
                                                        dtMat_row[headerOutThree] = 0;
                                                        dtMat_row[headerOutFour] = 0;

                                                        dtMat_row[headerForecastOne] = -1;
                                                        dtMat_row[headerForecastTwo] = -1;
                                                        dtMat_row[headerForecastThree] = -1;
                                                        dtMat_row[headerForecastFour] = -1;

                                                        dtMat.Rows.Add(dtMat_row);
                                                        forecastIndex++;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else
                                    {
                                        dtMat_row = dtMat.NewRow();
                                        dtMat_row[headerIndex] = forecastIndex;
                                        dtMat_row[headerMat] = child_Mat;

                                        if (child_Mat.Equals("ABS 450Y MH1"))
                                        {
                                            string testCode = itemCode;
                                            float tes1t = bal_1;

                                            float tes2t = bal_2;
                                            float tes3t = bal_3;
                                            float tes4t = bal_4;

                                        }

                                        if (Join["child_code"].ToString().Equals("C92HZF100"))
                                        {
                                            string testCode = itemCode;
                                            float tes1t = bal_1;

                                            float tes2t = bal_2;
                                            float tes3t = bal_3;
                                            float tes4t = bal_4;

                                        }

                                       

                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                        dtMat_row[headerMB] = child_MB;
                                        dtMat_row[headerMBRate] = child_mbRate;

                                        float TotalWeight = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());

                                        if (TotalWeight == 0)
                                        {
                                            TotalWeight = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                        }

                                        dtMat_row[headerWeight] = TotalWeight;

                                        dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                        dtMat_row[headerReadyStock] = child_ReadyStock;
                                        dtMat_row[headerBalanceZero] = child_ReadyStock;
                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(bal_1);
                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(bal_2);
                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(bal_3);
                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(bal_4);

                                        dtMat_row[headerOutOne] = 0;
                                        dtMat_row[headerOutTwo] = 0;
                                        dtMat_row[headerOutThree] = 0;
                                        dtMat_row[headerOutFour] = 0;

                                        dtMat_row[headerForecastOne] = -1;
                                        dtMat_row[headerForecastTwo] = -1;
                                        dtMat_row[headerForecastThree] = -1;
                                        dtMat_row[headerForecastFour] = -1;

                                        dtMat.Rows.Add(dtMat_row);
                                        forecastIndex++;
                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        //save to database

                        if (!string.IsNullOrEmpty(item["item_material"].ToString()))
                        {
                            dtMat_row = dtMat.NewRow();
                            dtMat_row[headerIndex] = forecastIndex;
                            dtMat_row[headerMat] = item["item_material"].ToString();
                            dtMat_row[headerCode] = itemCode;
                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                            dtMat_row[headerMB] = MB;
                            dtMat_row[headerMBRate] = mbRate;
                            dtMat_row[headerWeight] = itemPartWeight + itemRunnerWeight;
                            dtMat_row[headerWastage] = wastageAllowed;
                            dtMat_row[headerReadyStock] = readyStock;
                            dtMat_row[headerBalanceZero] = readyStock;
                            dtMat_row[headerBalanceOne] = Convert.ToInt32(bal_1);
                            dtMat_row[headerBalanceTwo] = Convert.ToInt32(bal_2);
                            dtMat_row[headerBalanceThree] = Convert.ToInt32(bal_3);
                            dtMat_row[headerBalanceFour] = Convert.ToInt32(bal_4);

                            dtMat_row[headerOutOne] = currentMonthOut;
                            dtMat_row[headerOutTwo] = nextMonthOut;
                            dtMat_row[headerOutThree] = nextNextMonthOut;
                            dtMat_row[headerOutFour] = nextNextNextMonthOut;

                            dtMat_row[headerForecastOne] = forecast_1;
                            dtMat_row[headerForecastTwo] = forecast_2;
                            dtMat_row[headerForecastThree] = forecast_3;
                            dtMat_row[headerForecastFour] = forecast_4;

                            dtMat.Rows.Add(dtMat_row);
                            forecastIndex++;
                        }

                    }
                    #endregion
                }
            }

            dtMat = ZeroCostAddDuplicates(dtMat);
            dtMat = FilterNonZeroCostMaterial(dtMat);

            //dtMat = calStillNeed(dtMat);
            return dtMat;
        }

        public DataTable FilterNonZeroCostMaterial(DataTable dt)
        {
            DataTable dt_ZeroCostMat = new materialDAL().SelectZeroCostMaterial();

            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                //dtMat_row[headerMat] = item["item_material"].ToString();
                //dtMat_row[headerCode] = itemCode;
                //dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);

                string itemMat = row[headerMat].ToString();
                string itemCode = row[headerCode].ToString();

                if(itemMat != new Text().Cat_SubMat)
                {
                    itemCode = itemMat;
                }

                bool itemFoundInZeroCostList = false;

                foreach(DataRow rowZeroCost in dt_ZeroCostMat.Rows)
                {
                    string MatCode = rowZeroCost["material_code"].ToString();

                    if(MatCode == itemCode)
                    {
                        itemFoundInZeroCostList = true;
                        break;
                    }
                }

                if(!itemFoundInZeroCostList)
                {
                    row.Delete();
                }
               
            }
            dt.AcceptChanges();

            return dt;
        }
        public DataTable insertZeroCostSubMaterialUsedData(string customer, string SubMat)
        {
            DataTable dt;
            DataTable dt_ItemForecast;

            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
                dt_ItemForecast = dalItemForecast.Select();
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
                dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());
            }

            dt = RemoveDuplicates(dt);
            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";
            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock, child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1, child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                string start = GetPMMAStartDate(DateTime.Now.Month, DateTime.Now.Year).ToString("yyyy/MM/dd");
                string end = GetPMMAEndDate(DateTime.Now.Month, DateTime.Now.Year).ToString("yyyy/MM/dd");

                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);

                int month = DateTime.ParseExact(nextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);

                month = DateTime.ParseExact(nextNextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);

                month = DateTime.ParseExact(nextNextNextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item["item_code"].ToString();

                    if (itemCode.Equals("V96LAR000"))
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());

                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    //forecast_1 = item["forecast_one"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_one"]);
                    //forecast_2 = item["forecast_two"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_two"]);
                    //forecast_3 = item["forecast_three"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_three"]);
                    //forecast_4 = item["forecast_four"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_four"]);

                    itemPartWeight = item["item_quo_pw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_pw_pcs"].ToString());
                    itemRunnerWeight = item["item_quo_rw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_rw_pcs"].ToString());

                    if (itemPartWeight == 0)
                        itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());

                    if (itemRunnerWeight == 0)
                        itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());

                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    DataTable dt_Out = dalTrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

                    currentMonthOut = 0;
                    if (dt_Out.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_Out.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    #region cal bal qty
                    //calculate still need how many qty
                    //forecastnum = balance
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = forecast_1 * -1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else if (bal_1 < 0)
                    {
                        bal_2 = bal_1 + forecast_2 * -1 + nextMonthOut;
                    }
                    else
                    {
                        bal_2 = forecast_2 * -1 + nextMonthOut;
                    }

                    if (nextNextMonthOut >= forecast_3)
                    {
                        bal_3 = bal_2;
                    }
                    else if (bal_2 < 0)
                    {
                        bal_3 = bal_2 + forecast_3 * -1 + nextNextMonthOut;
                    }
                    else
                    {
                        bal_3 = forecast_3 * -1 + nextNextMonthOut;
                    }

                    if (nextNextNextMonthOut >= forecast_4)
                    {
                        bal_4 = bal_3;
                    }
                    else if (bal_3 < 0)
                    {
                        bal_4 = bal_3 + forecast_4 * -1 + nextNextNextMonthOut;
                    }
                    else
                    {
                        bal_4 = forecast_4 * -1 + nextNextNextMonthOut;
                    }

                    #endregion

                    #region child
                    if (ifGotChild(itemCode, dtJoin))
                    {
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join["parent_code"].ToString().Equals(itemCode))
                            {
                                if (Join["child_cat"].ToString().Equals("Sub Material") && Join["child_code"].ToString().Equals(SubMat))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToInt32(Join["join_qty"]);

                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = childBal_2 + bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = childBal_3 + bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }

                                    //if got child, then loop child here
                                    if (ifGotChild(Join["child_code"].ToString(), dtJoin))
                                    {
                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2["parent_code"].ToString().Equals(Join["child_code"].ToString()))
                                            {
                                                if (Join2["child_cat"].ToString().Equals("Sub Material") && Join2["child_code"].ToString().Equals(SubMat))
                                                {
                                                    if (true)
                                                    {
                                                        child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                        child_child_join_qty = Convert.ToInt32(Join2["join_qty"]);

                                                        if (Join2["child_cat"].ToString().Equals("Sub Material"))
                                                        {
                                                            child_child_Mat = "Sub Material";
                                                        }
                                                        else
                                                        {
                                                            child_child_Mat = Join2["child_material"].ToString();
                                                        }


                                                        if (childBal_1 < 0)
                                                        {
                                                            child_childBal_1 = childBal_1 * child_child_join_qty;
                                                        }
                                                        else
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock;
                                                        }

                                                        if (childBal_2 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1;
                                                        }
                                                        else if (childBal_1 > 0)
                                                        {
                                                            child_childBal_2 = childBal_2 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                        }

                                                        if (childBal_3 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2;
                                                        }
                                                        else if (childBal_2 > 0)
                                                        {
                                                            child_childBal_3 = childBal_3 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_3 = child_childBal_2 - (childBal_2 - childBal_3) * child_child_join_qty;
                                                        }

                                                        if (childBal_4 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3;
                                                        }
                                                        else if (childBal_3 > 0)
                                                        {
                                                            child_childBal_4 = childBal_4 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_4 = child_childBal_3 - (childBal_3 - childBal_4) * child_child_join_qty;
                                                        }

                                                        //if (childBal_1 < 0)
                                                        //{
                                                        //    child_childBal_1 = child_child_ReadyStock + childBal_1 * child_child_join_qty;
                                                        //}
                                                        //else
                                                        //{
                                                        //    child_childBal_1 = child_child_ReadyStock;
                                                        //}

                                                        //if (childBal_2 > 0)
                                                        //{
                                                        //    child_childBal_2 = child_childBal_1;
                                                        //}
                                                        //else if (childBal_1 > 0)
                                                        //{
                                                        //    child_childBal_2 = child_childBal_1 + childBal_2 * child_child_join_qty;
                                                        //}
                                                        //else
                                                        //{

                                                        //    child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                        //}

                                                        //if (childBal_3 > 0)
                                                        //{
                                                        //    child_childBal_3 = child_childBal_2;
                                                        //}
                                                        //else if (childBal_2 > 0)
                                                        //{
                                                        //    child_childBal_3 = child_childBal_2 + childBal_3 * child_child_join_qty;
                                                        //}
                                                        //else
                                                        //{

                                                        //    child_childBal_3 = child_childBal_2 - (childBal_2 - childBal_3) * child_child_join_qty;
                                                        //}

                                                        //if (childBal_4 > 0)
                                                        //{
                                                        //    child_childBal_4 = child_childBal_3;
                                                        //}
                                                        //else if (childBal_3 > 0)
                                                        //{
                                                        //    child_childBal_4 = child_childBal_3 + childBal_4 * child_child_join_qty;
                                                        //}
                                                        //else
                                                        //{

                                                        //    child_childBal_4 = child_childBal_3 - (childBal_3 - childBal_4) * child_child_join_qty;
                                                        //}

                                                        dtMat_row = dtMat.NewRow();
                                                        dtMat_row[headerIndex] = forecastIndex;
                                                        dtMat_row[headerMat] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                                        dtMat_row[headerMBRate] = child_child_join_qty * child_join_qty;
                                                        dtMat_row[headerReadyStock] = child_ReadyStock;
                                                        dtMat_row[headerBalanceZero] = child_ReadyStock;
                                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(childBal_1);
                                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(childBal_2);
                                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(childBal_3);
                                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(childBal_4);

                                                        dtMat.Rows.Add(dtMat_row);
                                                        forecastIndex++;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else
                                    {
                                        dtMat_row = dtMat.NewRow();
                                        dtMat_row[headerIndex] = forecastIndex;
                                        dtMat_row[headerMat] = Join["child_code"].ToString();
                                        dtMat_row[headerMBRate] = child_join_qty;
                                        dtMat_row[headerCode] = itemCode;
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                                        dtMat_row[headerReadyStock] = readyStock;
                                        dtMat_row[headerBalanceZero] = readyStock;
                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(bal_1);
                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(bal_2);
                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(bal_3);
                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(bal_4);

                                        dtMat.Rows.Add(dtMat_row);
                                        forecastIndex++;
                                    }

                                }
                            }

                        }
                    }

                    #endregion
                }
            }

            dtMat = AddDuplicates(dtMat);
            dtMat = calStillNeed(dtMat);

            return dtMat;
        }

        public string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public DataTable insertSubMaterialUsedData(string customer, string SubMat)
        {
            DataTable dt;
            DataTable dt_ItemForecast = dalItemForecast.Select(getCustID(customer).ToString());
            if (customer.Equals("All"))
            {
                dt = dalItemCust.Select();//load all customer's item list
            }
            else
            {
                dt = dalItemCust.custSearch(customer);
            }

            dt = RemoveDuplicates(dt);
            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";
            dt = dt.DefaultView.ToTable();
            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float nextNextMonthOut, nextNextNextMonthOut;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                float child_childBal_1, child_childBal_2, child_childBal_3, child_childBal_4, child_child_ReadyStock, child_child_mbRate;
                string itemCode, MB, child_MB, child_child_MB, child_Mat, child_child_Mat;
                int forecastIndex = 1, child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);

                string start = GetPMMAStartDate(DateTime.Now.Month, DateTime.Now.Year).ToString("yyyy/MM/dd");
                string end = GetPMMAEndDate(DateTime.Now.Month, DateTime.Now.Year).ToString("yyyy/MM/dd");

                

                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);

                int month = DateTime.ParseExact(nextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);

                month = DateTime.ParseExact(nextNextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);

                 month = DateTime.ParseExact(nextNextNextMonth, "MMMM", CultureInfo.CurrentCulture).Month;

                if (month == 1)
                {
                    year = DateTime.Now.AddYears(+1).Year.ToString();
                }

                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item["item_code"].ToString();

                    if (itemCode.Equals("V88K9J100"))
                    {
                        float test = 0;
                    }

                    DataTable dt_Out = dalTrfHist.rangeItemToAllCustomerSearch(start, end, itemCode);

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());
                    forecast_1 = GetForecastQty(dt_ItemForecast, itemCode, 1);
                    forecast_2 = GetForecastQty(dt_ItemForecast, itemCode, 2);
                    forecast_3 = GetForecastQty(dt_ItemForecast, itemCode, 3);
                    forecast_4 = GetForecastQty(dt_ItemForecast, itemCode, 4);

                    itemPartWeight = item["item_quo_pw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_pw_pcs"].ToString());
                    itemRunnerWeight = item["item_quo_rw_pcs"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_quo_rw_pcs"].ToString());

                    if (itemPartWeight == 0)
                        itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());

                    if (itemRunnerWeight == 0)
                        itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());

                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

                    #region cal out data

                    currentMonthOut = 0;
                    if (dt_Out.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_Out.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                currentMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextMonthOut = 0;
                    if (dt_nextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextMonthOut = 0;
                    if (dt_nextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    nextNextNextMonthOut = 0;
                    if (dt_nextNextNextMonthTrfOutHist.Rows.Count > 0)
                    {
                        foreach (DataRow outRecord in dt_nextNextNextMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextNextNextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

                    #endregion

                    #region cal bal qty
                    //calculate still need how many qty
                    //forecastnum = balance
                    if (currentMonthOut >= forecast_1)
                    {
                        bal_1 = readyStock;
                    }
                    else
                    {
                        bal_1 = readyStock - forecast_1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else
                    {
                        bal_2 = bal_1 - forecast_2 + nextMonthOut;
                    }

                    bal_3 = bal_2 - forecast_3;

                    if(forecast_4 != -1)
                    {
                        bal_4 = bal_3 - forecast_4;
                    }
                    else
                    {
                        bal_4 = bal_3;
                    }

                    #endregion

                    #region child
                    if (ifGotChild(itemCode, dtJoin))
                    {                   
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if (Join["parent_code"].ToString().Equals(itemCode))
                            {
                                string childCodeCheck = Join["child_code"].ToString();
                                string childCatCheck = Join["child_cat"].ToString();
                                if (Join["child_cat"].ToString().Equals("Sub Material") && Join["child_code"].ToString().Equals(SubMat) || Join["child_cat"].ToString().Equals("Part"))
                                {
                                    child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                    child_join_qty = Convert.ToInt32(Join["join_qty"]);

                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = child_ReadyStock + bal_1 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_1 = child_ReadyStock;
                                    }

                                    if (bal_2 > 0)
                                    {
                                        childBal_2 = childBal_1;
                                    }
                                    else if (bal_1 > 0)
                                    {
                                        childBal_2 = childBal_1 + bal_2 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_2 = childBal_1 - forecast_2 * child_join_qty;
                                    }

                                    if (bal_3 > 0)
                                    {
                                        childBal_3 = childBal_2;
                                    }
                                    else if (bal_2 > 0)
                                    {
                                        childBal_3 = childBal_2 + bal_3 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_3 = childBal_2 - forecast_3 * child_join_qty;
                                    }

                                    if (bal_4 > 0)
                                    {
                                        childBal_4 = childBal_3;
                                    }
                                    else if (bal_3 > 0)
                                    {
                                        childBal_4 = childBal_3 + bal_4 * child_join_qty;
                                    }
                                    else
                                    {
                                        childBal_4 = childBal_3 - forecast_4 * child_join_qty;
                                    }

                                    //if got child, then loop child here
                                    if (ifGotChild(Join["child_code"].ToString(), dtJoin))
                                    {
                                        //loop child child
                                        foreach (DataRow Join2 in dtJoin.Rows)
                                        {
                                            if (Join2["parent_code"].ToString().Equals(Join["child_code"].ToString()))
                                            {
                                                if (Join2["child_cat"].ToString().Equals("Sub Material") && Join2["child_code"].ToString().Equals(SubMat))
                                                {
                                                    if (true)
                                                    {
                                                        child_child_ReadyStock = Join2["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join2["child_qty"]);
                                                        child_child_join_qty = Convert.ToInt32(Join2["join_qty"]);

                                                        if (Join2["child_cat"].ToString().Equals("Sub Material"))
                                                        {
                                                            child_child_Mat = "Sub Material";
                                                        }
                                                        else
                                                        {
                                                            child_child_Mat = Join2["child_material"].ToString();
                                                        }

                                                        if (childBal_1 < 0)
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock + childBal_1 * child_child_join_qty;
                                                        }
                                                        else
                                                        {
                                                            child_childBal_1 = child_child_ReadyStock;
                                                        }

                                                        if (childBal_2 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1;
                                                        }
                                                        else if (childBal_1 > 0)
                                                        {
                                                            child_childBal_2 = child_childBal_1 + childBal_2 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_2 = child_childBal_1 - (childBal_1 - childBal_2) * child_child_join_qty;
                                                        }

                                                        if (childBal_3 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2;
                                                        }
                                                        else if (childBal_2 > 0)
                                                        {
                                                            child_childBal_3 = child_childBal_2 + childBal_3 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_3 = child_childBal_2 - (childBal_2 - childBal_3) * child_child_join_qty;
                                                        }

                                                        if (childBal_4 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3;
                                                        }
                                                        else if (childBal_3 > 0)
                                                        {
                                                            child_childBal_4 = child_childBal_3 + childBal_4 * child_child_join_qty;
                                                        }
                                                        else
                                                        {

                                                            child_childBal_4 = child_childBal_3 - (childBal_3 - childBal_4) * child_child_join_qty;
                                                        }

                                                        dtMat_row = dtMat.NewRow();
                                                        dtMat_row[headerIndex] = forecastIndex;
                                                        dtMat_row[headerMat] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
                                                        dtMat_row[headerMBRate] = child_child_join_qty * child_join_qty;
                                                        dtMat_row[headerReadyStock] = child_ReadyStock;
                                                        dtMat_row[headerBalanceZero] = child_ReadyStock;
                                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(childBal_1);
                                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(childBal_2);
                                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(childBal_3);
                                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(childBal_4);

                                                        dtMat.Rows.Add(dtMat_row);
                                                        forecastIndex++;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else
                                    {
                                        dtMat_row = dtMat.NewRow();
                                        dtMat_row[headerIndex] = forecastIndex;
                                        dtMat_row[headerMat] = Join["child_code"].ToString();
                                        dtMat_row[headerMBRate] = child_join_qty;
                                        dtMat_row[headerCode] = itemCode;
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, itemCode);
                                        dtMat_row[headerReadyStock] = readyStock;
                                        dtMat_row[headerBalanceZero] = readyStock;
                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(bal_1);
                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(bal_2);
                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(bal_3);
                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(bal_4);

                                        dtMat.Rows.Add(dtMat_row);
                                        forecastIndex++;
                                    }

                                }
                            }

                        }
                    }
                  
                    #endregion
                }
            }

            dtMat = AddDuplicates(dtMat);
            dtMat = calStillNeed(dtMat);
            return dtMat;
        }

      
        #endregion

        #region Validation

        public int getNumberOfProDay(int PlanID)
        {
            int proDay = 0;

            DataTable dt_planning = dalPlanning.idSearch(PlanID.ToString());

            foreach(DataRow row in dt_planning.Rows)
            {
                int proDayRequried = Convert.ToInt32(row[dalPlanning.productionDay]);
                float proHourRequried = Convert.ToSingle(row[dalPlanning.productionHour]);
                float prohourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                float totalHour = prohourPerDay * proDayRequried + proHourRequried;

                if(proHourRequried != 0)
                {
                    proDay = Convert.ToInt32(totalHour / prohourPerDay);

                    proHourRequried = totalHour - proDay * prohourPerDay;
                }

                if(proHourRequried >= 0)
                {
                    proDay++;
                }
            }

            return proDay;
        }

        public string GetAlphabet(int index)
        {
            const string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        public int getNumberOfDayBetweenTwoDate(DateTime startDate, DateTime endDate, bool includeSunday)
        {
            var dayDifference = (int)endDate.Date.Subtract(startDate.Date).TotalDays;

            if(includeSunday)
            {
                return Enumerable
                   .Range(1, dayDifference)
                   .Select(x => startDate.AddDays(x))
                   .Count() + 1;
            }
            else
            {
                return Enumerable
                                .Range(1, dayDifference)
                                .Select(x => startDate.AddDays(x))
                                .Count(x => x.DayOfWeek != DayOfWeek.Sunday) + 1;
            }
        }

        public bool checkIfSunday(DateTime date)
        {
            bool ifSunday = false;
            DayOfWeek day = date.DayOfWeek;
            if (day == DayOfWeek.Sunday)
            {
                ifSunday = true;
            }
            return ifSunday;
        }

        public DateTime AddDayIfSunday(DateTime date)
        {
            bool ifSunday = false;
            DayOfWeek day = date.DayOfWeek;
            if (day == DayOfWeek.Sunday)
            {
                ifSunday = true;
            }

            if(ifSunday)
            {
                date.AddDays(1);
            }

            return date;
        }

        public DateTime SubtractDayIfSunday(DateTime date)
        {
            DayOfWeek day = date.DayOfWeek;

            if (day == DayOfWeek.Sunday)
            {
                DateTime newDate = date.AddDays(-1);
                return newDate;
            }

            return date;
        }

        public DateTime EstimateEndDate(DateTime Start, int proDayRequired, bool IncludeSunday)
        {
            DateTime estimateEndDate = Start;

            for (int i = 1; i < proDayRequired; i++)
            {
                estimateEndDate = estimateEndDate.AddDays(1);

                if (!IncludeSunday)
                {
                    DayOfWeek day = estimateEndDate.DayOfWeek;
                    if (day == DayOfWeek.Sunday)
                    {
                        estimateEndDate = estimateEndDate.AddDays(1);
                    }
                }
            }

            return estimateEndDate;
        }

        public DateTime earlierAvailableDate(string macID, bool IncludeSunday, PlanningBLL u)
        {
            DateTime earlierStartDate = new DateTime();

            DataTable dt_planning = dalPlanning.macIDSearch(macID);

            int proDayRequired;

            float hours = Convert.ToSingle(u.production_hour);

            int day = Convert.ToInt32(u.production_day);

            if (hours > 0)
            {
                proDayRequired = day + 1;
            }
            else
            {
                proDayRequired = day;
            }

            if (dt_planning.Rows.Count > 0)
            {
                foreach (DataRow row in dt_planning.Rows)
                {
                    Text text = new Text();
                    string status = row[dalPlanning.planStatus].ToString();
                    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
                    {
                        DateTime plannedStart = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                        DateTime plannedEnd = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                        if(earlierStartDate == default(DateTime))
                        {
                            earlierStartDate = plannedEnd;
                        }
                       else
                        {
                            DateTime extimateEndDate = EstimateEndDate(earlierStartDate, proDayRequired, IncludeSunday);

                            if(extimateEndDate > plannedStart)
                            {
                                earlierStartDate = plannedEnd;
                            }
                        }
                    }
                }
            }

            else
            {
                earlierStartDate = DateTime.Today;

                if (!IncludeSunday)
                {
                    DayOfWeek weekDay = earlierStartDate.DayOfWeek;
                    if (weekDay == DayOfWeek.Sunday)
                    {
                        earlierStartDate = earlierStartDate.AddDays(1);
                    }
                }
            }
            return earlierStartDate;
        }

        public DateTime lastAvailableDate(string macID, bool IncludeSunday)
        {
            DateTime lastAvailableDate = DateTime.Today.AddDays(1);

            if(!IncludeSunday)
            {
                DayOfWeek day = lastAvailableDate.DayOfWeek;
                if (day == DayOfWeek.Sunday)
                {
                    lastAvailableDate = lastAvailableDate.AddDays(1);
                }
            }

            DataTable dt_planning = dalPlanning.macIDSearch(macID);

            if (dt_planning.Rows.Count > 0)
            {
                foreach (DataRow row in dt_planning.Rows)
                {
                    Text text = new Text();
                    string status = row[dalPlanning.planStatus].ToString();
                    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
                    {
                        DateTime plannedEnd = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                        if(plannedEnd >= lastAvailableDate)
                        {
                            lastAvailableDate = plannedEnd.AddDays(1);

                            if (!IncludeSunday)
                            {
                                DayOfWeek day = lastAvailableDate.DayOfWeek;
                                if (day == DayOfWeek.Sunday)
                                {
                                    lastAvailableDate = lastAvailableDate.AddDays(1);
                                }
                            }
                        }
                    }
                }
            }

            return lastAvailableDate;
        }

        public bool ifProductionDateAvailable(string macID)
        {
            bool available = true;
            DataTable dt_planning = dalPlanning.macIDSearch(macID);

            if (dt_planning.Rows.Count > 0)
            {
                foreach (DataRow row in dt_planning.Rows)
                {
                    Text text = new Text();
                    string status = row[dalPlanning.planStatus].ToString();
                    if (status.Equals(text.planning_status_running))
                    {
                        available = false;
                    }
                }
            }
            return available;
        }

        public bool ifProductionDateAvailable(string macID, DateTime start, DateTime end)
        {
            bool available = true;
            DataTable dt_planning = dalPlanning.macIDSearch(macID);

            if(dt_planning.Rows.Count > 0)
            {
                foreach (DataRow row in dt_planning.Rows)
                {
                    Text text = new Text();
                    string status = row[dalPlanning.planStatus].ToString();
                    if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed))
                    {
                        DateTime plannedStart = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                        DateTime plannedEnd = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                        if (plannedStart == null || end < plannedStart || end == plannedStart)
                        {
                            available = true;
                        }
                        else if (plannedEnd == null || start > plannedEnd || start == plannedEnd)
                        {
                            available = true;
                        }
                        else
                        {
                            return false;
                        }
                    } 
                }
            }
           
            return available;
        }

        public bool ifProductionDateAvailable(DateTime start, DateTime end, DateTime compareStart, DateTime compareEnd)
        {
            bool available = false;

            if (compareStart == null || end <= compareStart)
            {
                available = true;
            }

            if (compareEnd == null || start >= compareEnd)
            {
                available = true;
            }

            return available;
        }

        public bool ifGotChildIncludedPacking(string itemCode, DataTable dt)
        {
            bool result = false;
            Text text = new Text();
            foreach (DataRow join in dt.Rows)
            {
                if (join["parent_code"].ToString().Equals(itemCode) && (join["child_cat"].ToString().Equals(text.Cat_Carton) || join["child_cat"].ToString().Equals("Part") || join["child_cat"].ToString().Equals("Sub Material")))
                {
                    return true;
                }
            }
            return result;
      
        }

        public bool ifGotChildExcludedPacking(string itemCode, DataTable dt)
        {
            bool result = false;
            Text text = new Text();
            foreach (DataRow join in dt.Rows)
            {
                if (join["parent_code"].ToString().Equals(itemCode) && ( join["child_cat"].ToString().Equals("Part") || join["child_cat"].ToString().Equals("Sub Material")))
                {
                    return true;
                }
            }
            return result;

        }

        public bool ifGotChild(string itemCode, DataTable dt)
        {
            bool result = false;

            foreach (DataRow join in dt.Rows)
            {
                if(join["parent_code"].ToString().Equals(itemCode) && (join["child_cat"].ToString().Equals("Part") || join["child_cat"].ToString().Equals("Sub Material")))
                {
                    return true;
                }
            }
            return result;
        }

        public void AddIndexNumberToDGV(DataGridView dgv, string headerName)
        {
            if (dgv.Columns.Contains(headerName))
            {
                int index = 1;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Cells[headerName].Value = index++;
                }
            }

        }

        public Tuple<DataTable,bool> AddIndexNumber(DataTable dt, string headerName)
        {
            bool success = false;

            if(dt.Columns.Contains(headerName))
            {
                success = true;
                int index = 1;
                foreach(DataRow row in dt.Rows)
                {
                    row[headerName] = index++;
                }
            }

            return Tuple.Create(dt, success);
        }

        readonly string headerRowReference = "REPEATED ROW";
        readonly string headerItemType = "ITEM TYPE";
        readonly string AssemblyMarking = "Blue";
        readonly string InspectionMarking = "Peru";
        readonly string ProductionMarking = "Green";
        readonly string ProductionAndAssemblyMarking = "Purple";
        readonly string headerToDo = "TO DO";


        private DataTable NewForecastReportTable()
        {
            headerForecast1 = "FCST/ NEEDED";
            headerForecast2 = "FCST/ NEEDED";
            headerForecast3 = "FCST/ NEEDED";
            headerForecast4 = "FCST/ NEEDED";

            headerBal1 = "BAL";
            headerBal2 = "BAL";
            headerBal3 = "BAL";
            headerBal4 = "BAL";
            DataTable dt = new DataTable();

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerBackColor, typeof(string));
            dt.Columns.Add(headerForecastType, typeof(string));
            dt.Columns.Add(headerBalType, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerRowReference, typeof(string));
            dt.Columns.Add(headerItemType, typeof(string));
            dt.Columns.Add(headerIndex, typeof(float));
            dt.Columns.Add(headerRawMat, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerColorMat, typeof(string));
            dt.Columns.Add(headerPartWeight, typeof(string));

            //dt.Columns.Add(headerPlannedQty, typeof(float));
            //dt.Columns.Add(headerProduced, typeof(float));
            //dt.Columns.Add(headerToProduce, typeof(float));

            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerEstimate, typeof(float));

            string monthFrom = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

            int monthINT = DateTime.ParseExact(monthFrom, "MMMM", CultureInfo.CurrentCulture).Month;

            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                {
                    headerForecast1 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerForecast1;
                    headerBal1 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerBal1;
                }

                else if (i == 2)
                {
                    headerForecast2 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerForecast2;
                    headerBal2 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerBal2;
                }

                else if (i == 3)
                {
                    headerForecast3 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerForecast3;
                    headerBal3 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerBal3;

                }
                else if (i == 4)
                {
                    headerForecast4 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerForecast4;
                    headerBal4 = GetAbbreviatedFromFullName(monthFrom).ToUpper() + " " + headerBal4;

                }
                monthINT++;

                if (monthINT > 12)
                {
                    monthINT -= 12;

                }

                monthFrom = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthINT);

            }

            dt.Columns.Add(headerForecast1, typeof(float));
            dt.Columns.Add(headerOut, typeof(float));
            dt.Columns.Add(headerOutStd, typeof(float));
            dt.Columns.Add(headerBal1, typeof(float));
            dt.Columns.Add(headerForecast2, typeof(float));
            dt.Columns.Add(headerBal2, typeof(float));
            dt.Columns.Add(headerForecast3, typeof(float));
            dt.Columns.Add(headerBal3, typeof(float));
            dt.Columns.Add(headerForecast4, typeof(float));
            dt.Columns.Add(headerBal4, typeof(float));

            return dt;
        }

        private DataTable RemoveTerminatedItem(DataTable dt)
        {
            Text text = new Text();

            //"(TERMINATED)"
            DataTable dt_NEW = dt.Copy();

            dt_NEW.AcceptChanges();
            foreach (DataRow row in dt_NEW.Rows)
            {
                // If this row is offensive then
                string itemName = row[dalItem.ItemName].ToString();

                if (itemName.Contains(text.Cat_Terminated))
                {
                    row.Delete();
                }

            }
            dt_NEW.AcceptChanges();

            return dt_NEW;

        }

        private float GetMaxOut(string itemCode, string customer, int pastMonthQty, DataTable dt_TrfHist, DataTable dt_PMMADate,DateTime start, DateTime end)
        {
            float MaxOut = 0, tmp = 0;
            bool includeCurrentMonth = false;

            if (customer.Equals("PMMA"))
            {
                if (pastMonthQty <= 0)
                {
                    foreach (DataRow row in dt_TrfHist.Rows)
                    {
                        string item = row[dalTrfHist.TrfItemCode].ToString();
                        if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                        {
                            DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);
                            //string cust = row[dalTrfHist.TrfTo].ToString();


                            if (trfDate >= start && trfDate <= end)
                            {
                                MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                            }
                        }
                    }
                }
                else
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    if (DateTime.Today > GetPMMAEndDate(currentMonth, currentYear))
                    {
                        includeCurrentMonth = true;
                    }

                    for (int i = 0; i < pastMonthQty; i++)
                    {
                        tmp = 0;

                        if (!(i == 0 && includeCurrentMonth))
                        {
                            if (currentMonth == 1)
                            {
                                currentMonth = 12;
                                currentYear--;
                            }
                            else
                            {
                                currentMonth--;
                            }

                        }

                        start = GetPMMAStartDate(currentMonth, currentYear, dt_PMMADate);
                        end = GetPMMAEndDate(currentMonth, currentYear, dt_PMMADate);

                        //dtpOutFrom.Value = new DateTime(year, month, 1);
                        //dtpOutTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                        foreach (DataRow row in dt_TrfHist.Rows)
                        {
                            string item = row[dalTrfHist.TrfItemCode].ToString();
                            if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                            {
                                DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                                if (trfDate >= start && trfDate <= end)
                                {
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }

                        //get maxout
                        if (MaxOut < tmp)
                        {
                            MaxOut = tmp;
                        }


                    }
                }
            }
            else
            {
                if (pastMonthQty <= 0)
                {

                    foreach (DataRow row in dt_TrfHist.Rows)
                    {
                        string item = row[dalTrfHist.TrfItemCode].ToString();
                        if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                        {
                            DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                            if (trfDate >= start && trfDate <= end)
                            {
                                MaxOut += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                            }
                        }
                    }
                }
                else
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    for (int i = 0; i < pastMonthQty; i++)
                    {
                        tmp = 0;

                        if (currentMonth == 1)
                        {
                            currentMonth = 12;
                            currentYear--;
                        }
                        else
                        {
                            currentMonth--;
                        }

                        start = new DateTime(currentYear, currentMonth, 1);
                        end = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));

                        foreach (DataRow row in dt_TrfHist.Rows)
                        {
                            string item = row[dalTrfHist.TrfItemCode].ToString();
                            if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && item.Equals(itemCode))
                            {
                                DateTime trfDate = Convert.ToDateTime(row[dalTrfHist.TrfDate]);

                                if (trfDate >= start && trfDate <= end)
                                {
                                    tmp += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                                }
                            }
                        }

                        //get maxout
                        if (MaxOut < tmp)
                        {
                            MaxOut = tmp;
                        }
                    }
                }
            }



            return MaxOut;
        }

        readonly string ToDoType_ToAssembly = "(2) TO ASSEMBLY";
        readonly string ToDoType_ToProduce = "(1) TO PRODUCE";
        readonly string ToDoType_ToOrder = "(3) TO ORDER";

        public DataTable LoadSummaryList(DataTable dt)
        {
            DataTable dt_Copy = dt.Copy();
            Text text = new Text();

            dt_Copy.Columns.Add(headerToDo, typeof(string)).SetOrdinal(0);
            //dt_Copy.Columns.Add(headerBal3, typeof(float));
            //dt_Copy.Columns.Add(headerForecast4, typeof(float));
            //dt_Copy.Columns.Add(headerBal4, typeof(float));

            #region SUM Repeated row Total Needed

            for (int i = 0; i < dt_Copy.Rows.Count - 1; i++)
            {
                string balType = dt_Copy.Rows[i][headerBalType].ToString();
                decimal ThirdMonthNeeded = decimal.TryParse(dt_Copy.Rows[i][headerForecast3].ToString(), out decimal d) ? d : 0;
                decimal FouthMonthNeeded = decimal.TryParse(dt_Copy.Rows[i][headerForecast4].ToString(), out  d) ? d : 0;
                decimal bal_2 = decimal.TryParse(dt_Copy.Rows[i][headerBal2].ToString(), out d) ? d : 0;

                if (balType.Equals(balType_Total))
                {
                    string TotalItemCode = dt_Copy.Rows[i][headerPartCode].ToString();
                    decimal SecondMonthNeeded = decimal.TryParse(dt_Copy.Rows[i][headerForecast2].ToString(), out d) ? d : 0;
                    decimal FirstMonthNeeded = decimal.TryParse(dt_Copy.Rows[i][headerForecast1].ToString(), out d) ? d : 0;


                    for (int j = i + 1; j < dt_Copy.Rows.Count; j++)
                    {
                        string SeachingCode = dt_Copy.Rows[j][headerPartCode].ToString();

                        if (SeachingCode == TotalItemCode)
                        {
                            FouthMonthNeeded += decimal.TryParse(dt_Copy.Rows[j][headerForecast4].ToString(), out d) ? d : 0;
                            ThirdMonthNeeded += decimal.TryParse(dt_Copy.Rows[j][headerForecast3].ToString(), out d) ? d : 0;
                            SecondMonthNeeded += decimal.TryParse(dt_Copy.Rows[j][headerForecast2].ToString(), out d) ? d : 0;
                            FirstMonthNeeded += decimal.TryParse(dt_Copy.Rows[j][headerForecast1].ToString(), out d) ? d : 0;
                        }
                    }

                    dt_Copy.Rows[i][headerForecast4] = (float)FouthMonthNeeded;
                    dt_Copy.Rows[i][headerForecast3] = (float)ThirdMonthNeeded;
                    dt_Copy.Rows[i][headerForecast2] = (float)SecondMonthNeeded;
                    dt_Copy.Rows[i][headerForecast1] = (float)FirstMonthNeeded;


                }

                dt_Copy.Rows[i][headerBal3] = (float)(bal_2 - ThirdMonthNeeded);
                dt_Copy.Rows[i][headerBal4] = (float)(bal_2 - ThirdMonthNeeded - FouthMonthNeeded);
            }

            #endregion

            #region Filter Row

            if (dt_Copy.Rows.Count > 0)
            {
                List<DataRow> rowsToDelete = new List<DataRow>();
                foreach (DataRow row in dt_Copy.Rows)
                {

                    bool ToRemoveMatched = false;

                    string itemCode = row["PART CODE"].ToString();

                    string balType = row[headerBalType].ToString();
                    string itemType = row[headerItemType].ToString();
                    string ParentOrChild = row[headerType].ToString();
                    string RawMaterial = row[headerRawMat].ToString();
                    decimal bal_1 = decimal.TryParse(row[headerBal1].ToString(), out decimal d) ? d : 0;
                    decimal bal_2 = decimal.TryParse(row[headerBal2].ToString(), out d) ? d : 0;
                    decimal bal_3 = decimal.TryParse(row[headerBal3].ToString(), out d) ? d : 0;
                    decimal bal_4 = decimal.TryParse(row[headerBal4].ToString(), out d) ? d : 0;

                    string ToDOType = "";

                    if (balType != balType_Unique && balType != balType_Total)
                    {
                        ToRemoveMatched = true;
                    }

                    if (!string.IsNullOrEmpty(RawMaterial))
                    {
                        ToDOType = ToDoType_ToProduce;
                    }
                    else if (ParentOrChild.Equals(typeParent) || itemType.Equals(text.Cat_Part))
                    {
                        ToDOType = ToDoType_ToAssembly;
                    }
                    else if (itemType.Equals(text.Cat_SubMat))
                    {
                        ToDOType = ToDoType_ToOrder;
                    }



                    if (ToRemoveMatched)
                    {
                        // dt_Copy.Rows[i].Delete();
                        rowsToDelete.Add(row);
                    }
                    else
                        row[headerToDo] = ToDOType;


                }

                #region Old For Loop
                //for (int i = 0; i <= dt_Copy.Rows.Count - 1; i++)
                //{

                //    bool ToRemoveMatched = false;

                //    string itemCode = dt_Copy.Rows[i]["PART CODE"].ToString();

                //    if(itemCode == "V0KPBW100")
                //    {
                //        float checkpoint = 1;
                //    }

                //    string balType = dt_Copy.Rows[i][headerBalType].ToString();
                //    string itemType = dt_Copy.Rows[i][headerItemType].ToString();
                //    string ParentOrChild = dt_Copy.Rows[i][headerType].ToString();
                //    string RawMaterial = dt_Copy.Rows[i][headerRawMat].ToString();
                //    decimal bal_1 = decimal.TryParse(dt_Copy.Rows[i][headerBal1].ToString(), out decimal d) ? d : 0;
                //    decimal bal_2 = decimal.TryParse(dt_Copy.Rows[i][headerBal2].ToString(), out d) ? d : 0;
                //    decimal bal_3 = decimal.TryParse(dt_Copy.Rows[i][headerBal3].ToString(), out d) ? d : 0;

                //    string ToDOType = "";

                //    if (balType != balType_Unique && balType != balType_Total)
                //    {
                //        ToRemoveMatched = true;
                //    }

                //    if (!string.IsNullOrEmpty(RawMaterial))
                //    {
                //        ToDOType = ToDoType_ToProduce;
                //    }
                //    else if (ParentOrChild.Equals(typeParent) || itemType.Equals(text.Cat_Part))
                //    {
                //        ToDOType = ToDoType_ToAssembly;
                //    }
                //    else if (itemType.Equals(text.Cat_SubMat))
                //    {
                //        ToDOType = ToDoType_ToOrder;
                //    }



                //    if (ToRemoveMatched)
                //    {
                //       // dt_Copy.Rows[i].Delete();

                //    }
                //    else
                //        dt_Copy.Rows[i][headerToDo] = ToDOType;


                //}
                #endregion

                foreach (DataRow row in rowsToDelete)
                {
                    row.Delete();
                }
                dt_Copy.AcceptChanges();
            }

            #endregion

            #region Sorting

            dt_Copy.DefaultView.Sort = headerToDo + " ASC, " + headerBal4 + " ASC, "+ headerBal3 + " ASC, " + headerBal2 + " ASC, " + headerBal1 + " ASC";

            dt_Copy = dt_Copy.DefaultView.ToTable();

            #endregion

            return dt_Copy;
        }

        public DataTable GetForecastSummary(int CustID)
        {
            #region pre Setting

            string customer = getCustName(CustID);

            DataTable dt_Data = NewForecastReportTable();

            #endregion

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(customer))
            {
                #region Loading DB

                DataRow dt_Row;

                DataTable dt = dalItemCust.custSearch(customer);

                DataTable dt_ItemForecast = dalItemForecast.Select(CustID.ToString());

                DateTime dtpOutFrom = new DateTime();
                DateTime dtpOutTo = new DateTime();

                int year = DateTime.Now.Year;

                string monthString = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

                int month = DateTime.ParseExact(monthString, "MMMM", CultureInfo.CurrentCulture).Month;

                if (CustID == 1)
                {
                    DateTime outTo = GetPMMAEndDate(month, year);

                    if (DateTime.Today > outTo)
                    {
                        month++;

                        if (month > 12)
                        {
                            month -= 12;
                            year++;
                        }

                        monthString = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    }
                    else
                    {
                        dtpOutFrom = GetPMMAStartDate(month, year);
                        dtpOutTo = outTo;
                    }

                }
                else
                {
                    dtpOutFrom= new DateTime(year, month, 1);
                    dtpOutTo= new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }

                string from = dtpOutFrom.ToString("yyyy/MM/dd");
                string to = dtpOutTo.ToString("yyyy/MM/dd");
               
                DataTable dt_TrfHist = dalTrfHist.rangeItemToCustomerTransferDataOnlySearch(customer, from, to);

                DataTable dt_PMMADate = dalPmmaDate.Select();

                DataTable dt_Join = dalJoin.SelectAll();

                DataTable dt_Item = dalItem.Select();

                int index = 1;

                dt.DefaultView.Sort = "item_name ASC";


                dt = dt.DefaultView.ToTable();

                #endregion

                #region Load Single Item

                dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    bool gotNotPackagingChild = ifGotNotPackagingChild(uData.part_code, dt_Join, dt_Item);

                    #region load single part

                    if (!gotNotPackagingChild)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        //var result = GetProduceQty(uData.part_code, dt_MacSchedule);

                        //uData.toProduce = result.Item1;
                        //uData.Produced = result.Item2;

                        uData.forecast1 = GetForecastQty(dt_ItemForecast, uData.part_code, 1, monthString);
                        uData.forecast2 = GetForecastQty(dt_ItemForecast, uData.part_code, 2, monthString);
                        uData.forecast3 = GetForecastQty(dt_ItemForecast, uData.part_code, 3, monthString);
                        uData.forecast4 = GetForecastQty(dt_ItemForecast, uData.part_code, 4, monthString);

                        uData.estimate = 0;

                        uData.deliveredOut = GetMaxOut(uData.part_code, customer, 0, dt_TrfHist, dt_PMMADate,dtpOutFrom,dtpOutTo);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        if (!customer.Equals("PMMA"))
                        {
                            uData.outStd = uData.estimate - uData.deliveredOut;

                        }
                        if (uData.forecast1 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate - uData.deliveredOut;
                            }
                        }

                        else if (uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }


                        uData.bal3 = uData.bal2 - uData.forecast3;

                        if (uData.forecast3 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal3 = uData.bal2;
                            }
                            else
                            {
                                uData.bal3 = uData.bal2 - uData.estimate;
                            }
                        }
                        else if (uData.forecast3 > -1)
                        {
                            uData.bal3 = uData.bal2 - uData.forecast3;
                        }

                        uData.bal4 = uData.bal3 - uData.forecast4;

                        if (uData.forecast4 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal4 = uData.bal3;
                            }
                            else
                            {
                                uData.bal4 = uData.bal3 - uData.estimate;
                            }
                        }
                        else if (uData.forecast4 > -1)
                        {
                            uData.bal4 = uData.bal3 - uData.forecast4;
                        }



                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        //if (uData.toProduce > 0)
                        //{
                        //    dt_Row[headerToProduce] = uData.toProduce;
                        //}

                        //if (uData.Produced > 0)
                        //{
                        //    dt_Row[headerProduced] = uData.Produced;
                        //}

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerType] = typeSingle;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;
                        dt_Row[headerBal3] = uData.bal3;
                        dt_Row[headerBal4] = uData.bal4;

                        //uData.forecast4 = uData.forecast4 < 0 ? 0 : uData.forecast4;

                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;
                        dt_Row[headerForecast4] = uData.forecast4;

                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && uData.forecast4 <= 0))
                        {
                            dt_Data.Rows.Add(dt_Row);
                            index++;

                            row.Delete();
                        }

                    }

                    #endregion
                }
                dt.AcceptChanges();

                #endregion

                #region Load Group Item

                foreach (DataRow row in dt.Rows)
                {
                    uData.part_code = row[dalItem.ItemCode].ToString();

                    int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
                    int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

                    bool gotNotPackagingChild = ifGotNotPackagingChild(uData.part_code, dt_Join, dt_Item);


                    #region load assembly part

                    if (gotNotPackagingChild)
                    {
                        uData.index = index;
                        uData.part_name = row[dalItem.ItemName].ToString();
                        uData.color_mat = row[dalItem.ItemMBatch].ToString();
                        uData.color = row[dalItem.ItemColor].ToString();
                        uData.raw_mat = row[dalItem.ItemMaterial].ToString();
                        uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
                        uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
                        uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
                        uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
                        uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

                        //var result = GetProduceQty(uData.part_code, dt_MacSchedule);
                        //uData.toProduce = result.Item1;
                        //uData.Produced = result.Item2;
                        uData.estimate = 0;
                        uData.forecast1 = GetForecastQty(dt_ItemForecast, uData.part_code, 1,monthString);
                        uData.forecast2 = GetForecastQty(dt_ItemForecast, uData.part_code, 2, monthString);
                        uData.forecast3 = GetForecastQty(dt_ItemForecast, uData.part_code, 3, monthString);
                        uData.forecast4 = GetForecastQty(dt_ItemForecast, uData.part_code, 4, monthString);

                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && uData.forecast4 <= 0))
                        {
                            dt_Row = dt_Data.NewRow();
                            dt_Data.Rows.Add(dt_Row);
                        }


                        uData.deliveredOut = GetMaxOut(uData.part_code, customer, 0, dt_TrfHist, dt_PMMADate,dtpOutFrom,dtpOutTo);

                        uData.outStd = uData.forecast1 - uData.deliveredOut;

                        #region Balance Calculation

                        if (!customer.Equals("PMMA"))
                        {
                            uData.outStd = uData.estimate - uData.deliveredOut;
                        }

                        if (uData.forecast1 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.outStd = 0;
                            }
                            else
                            {
                                uData.outStd = uData.estimate - uData.deliveredOut;
                            }
                        }
                        else if (uData.forecast1 > -1)
                        {
                            uData.outStd = uData.forecast1 - uData.deliveredOut;
                        }

                        uData.bal1 = uData.ready_stock;

                        if (uData.outStd >= 0)
                        {
                            uData.bal1 = uData.ready_stock - uData.outStd;
                        }

                        uData.bal2 = uData.bal1 - uData.forecast2;

                        if (uData.forecast2 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal2 = uData.bal1;
                            }
                            else
                            {
                                uData.bal2 = uData.bal1 - uData.estimate;
                            }
                        }
                        else if (uData.forecast2 > -1)
                        {
                            uData.bal2 = uData.bal1 - uData.forecast2;
                        }

                        uData.bal3 = uData.bal2 - uData.forecast3;

                        if (uData.forecast3 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal3 = uData.bal2;
                            }
                            else
                            {
                                uData.bal3 = uData.bal2 - uData.estimate;
                            }
                        }
                        else if (uData.forecast3 > -1)
                        {
                            uData.bal3 = uData.bal2 - uData.forecast3;
                        }

                        uData.bal4 = uData.bal3 - uData.forecast4;

                        if (uData.forecast4 == -1)
                        {
                            if (customer.Equals("PMMA"))
                            {
                                uData.bal4 = uData.bal3;
                            }
                            else
                            {
                                uData.bal4 = uData.bal3 - uData.estimate;
                            }
                        }
                        else if (uData.forecast4 > -1)
                        {
                            uData.bal4 = uData.bal3 - uData.forecast4;
                        }

                        #endregion 

                        if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
                        {
                            uData.color_mat += " (" + uData.color + ")";
                        }

                        dt_Row = dt_Data.NewRow();

                        //if (uData.toProduce > 0)
                        //{
                        //    dt_Row[headerToProduce] = uData.toProduce;
                        //}

                        //if (uData.Produced > 0)
                        //{
                        //    dt_Row[headerProduced] = uData.Produced;
                        //}

                        dt_Row[headerIndex] = uData.index;
                        dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
                        dt_Row[headerType] = typeParent;
                        dt_Row[headerBalType] = balType_Unique;
                        dt_Row[headerRawMat] = uData.raw_mat;
                        dt_Row[headerPartCode] = uData.part_code;
                        dt_Row[headerPartName] = uData.part_name;
                        dt_Row[headerColorMat] = uData.color_mat;
                        dt_Row[headerPartWeight] = uData.pw_per_shot / uData.cavity + " (" + (uData.rw_per_shot / uData.cavity) + ")";
                        dt_Row[headerReadyStock] = uData.ready_stock;
                        dt_Row[headerEstimate] = uData.estimate;
                        dt_Row[headerOut] = uData.deliveredOut;
                        dt_Row[headerOutStd] = uData.outStd;

                        dt_Row[headerBal1] = uData.bal1;
                        dt_Row[headerBal2] = uData.bal2;
                        dt_Row[headerBal3] = uData.bal3;
                        dt_Row[headerBal4] = uData.bal4;

                        //uData.forecast4 = uData.forecast4 < 0 ? 0 : uData.forecast4;


                        dt_Row[headerForecast1] = uData.forecast1;
                        dt_Row[headerForecast2] = uData.forecast2;
                        dt_Row[headerForecast3] = uData.forecast3;
                        dt_Row[headerForecast4] = uData.forecast4;

                        if (assembly == 1 && production == 0)
                        {
                            dt_Row[headerParentColor] = AssemblyMarking;
                        }
                        else if (assembly == 0 && production == 1)
                        {
                            dt_Row[headerParentColor] = ProductionMarking;
                        }
                        else if (assembly == 1 && production == 1)
                        {
                            dt_Row[headerParentColor] = ProductionAndAssemblyMarking;
                        }


                        if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0))
                        {
                            dt_Data.Rows.Add(dt_Row);
                            index++;

                            //load child
                            SummaryLoadChild(dt_Item, dt_Join, dt_Data, uData, 0.1f);
                        }
                    }

                    #endregion

                }

                #endregion
            }

            if (dt_Data.Rows.Count > 0)
            {
                dt_Data = SummaryCalRepeatedData(dt_Data);
            }
            
            return LoadSummaryList(dt_Data);

        }

        public bool ifGotChild2(string itemCode, DataTable dt)
        {
            bool result = false;
            Text text = new Text();

            foreach (DataRow join in dt.Rows)
            {
                string childCode = join[dalJoin.JoinChild].ToString();

                if (join[dalJoin.JoinParent].ToString().Equals(itemCode))
                {
                    string childCat = getItemCat(childCode);

                    if(childCat != text.Cat_Packaging && childCat != text.Cat_Carton && childCat != text.Cat_PolyBag)
                    return true;
                }
            }
            return result;
        }


        public bool ifGotNotPackagingChild(string itemCode, DataTable dt_Join,DataTable dt_Item)
        {
            bool result = false;
            Text text = new Text();

            foreach (DataRow join in dt_Join.Rows)
            {
                if (join[dalJoin.JoinParent].ToString().Equals(itemCode))
                {
                    string childCode = join[dalJoin.JoinChild].ToString();

                    string childCat = "";

                    foreach (DataRow row in dt_Item.Rows)
                    {
                        if (row[dalItem.ItemCode].ToString().Equals(childCode))
                        {
                            childCat = row["item_cat"].ToString();
                            break;
                        }
                    }

                    if (childCat != text.Cat_Packaging && childCat != text.Cat_Carton && childCat != text.Cat_PolyBag)
                        return true;
                }
            }
            return result;
        }

        public bool ifGotParent(string ChildCode, DataTable dt)
        {
            bool result = false;

            foreach (DataRow join in dt.Rows)
            {
                if (join["child_code"].ToString().Equals(ChildCode))
                {
                    return true;
                }
            }
            return result;
        }

        public bool ifGotChildExcludePackaging(string itemCode)
        {
            bool result = false;
            Text text = new Text();
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            string itemCat = getItemCat(itemCode);

            if (itemCat == text.Cat_Part)
            {
                if (dtJoin.Rows.Count > 0)
                {
                    foreach (DataRow row in dtJoin.Rows)
                    {
                        string childCat = getItemCat(row[dalJoin.JoinChild].ToString());
                        if (childCat == text.Cat_Part || childCat == text.Cat_SubMat)
                        {
                            return true;
                        }
                    }
                }
            }


            return result;
        }

        public bool ifGotChildIncludedPackaging(string itemCode)
        {
            bool result = false;
            Text text = new Text();
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            string itemCat = getItemCat(itemCode);

            if (itemCat == text.Cat_Part)
            {
                if (dtJoin.Rows.Count > 0)
                {
                    foreach (DataRow row in dtJoin.Rows)
                    {
                        string childCat = getItemCat(row[dalJoin.JoinChild].ToString());
                        return true;
                    }
                }
            }


            return result;
        }


        public bool ifGotChild(string itemCode)
        {
            bool result = false;
            Text text = new Text();
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            string itemCat = getItemCat(itemCode);

            if(itemCat == text.Cat_Part)
            {
                if (dtJoin.Rows.Count > 0)
                {
                    foreach (DataRow row in dtJoin.Rows)
                    {
                        string childCat = getItemCat(row[dalJoin.JoinChild].ToString());
                        if (childCat == text.Cat_Part || childCat == text.Cat_SubMat) //childCat == text.Cat_Part || childCat == text.Cat_SubMat
                        {
                            return true;
                        }
                    }
                }
            }
           

            return result;
        }


        public bool ifGotParent(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.loadParentList(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        public bool IfExists(string itemCode, string custName)
        {
            DataTable dt = dalItemCust.existsSearch(itemCode, getCustID(custName).ToString());

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public float stillNeedCheck(float n)
        {
            if (n >= 0)
            {
                n = 0;
            }
            else
            {
                n *= -1;
            }
            return n;
        }

        public bool IfProductsExists(string productCode)
        {
            DataTable dt;

            dt = dalItem.codeSearch(productCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool IfMaterialExists(string itemCode)
        {
            DataTable dt;

            dt = dalMaterial.codeSearch(itemCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool IfFactoryExists(string facName)
        {
            DataTable dt;

            dt = dalFac.nameSearch(facName);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool IfFactoryExists(DataTable dt, string facName)
        {
            bool result = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["fac_name"].ToString().Equals(facName))
                {
                    return true;
                }
            }
            return result;
        }

        public bool IfAssembly(DataTable dt,string itemCode)
        {
            bool result = false;
            //DataTable dt = codeSearch(itemCode);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["item_assembly"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dt.Rows[0]["item_assembly"]) == 1)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        #endregion

        #region System

        public void saveToText(Exception ex)//error message
        {
            string errorMessage = ex.Message;
            Directory.CreateDirectory(@"D:\StockAssistant\SystemError");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemError\Error_" + today + ".txt";

            string str = "";

            if(File.Exists(filePath))
            {
                using (StreamReader sreader = new StreamReader(filePath))
                {
                    str = sreader.ReadToEnd();
                }

                File.Delete(filePath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine();
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine();
                    writer.WriteLine("StackTrace : ");
                    writer.WriteLine(ex.StackTrace);

                    ex = ex.InnerException;
                }

                writer.Write(str);
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            str = "";
            if (File.Exists(filePath))
            {
                using (StreamReader sreader = new StreamReader(filePath))
                {
                    str = sreader.ReadToEnd();
                }
                File.Delete(filePath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (errorMessage != null)
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    writer.WriteLine("Action : SYSTEM ERROR");
                    writer.WriteLine();
                    writer.WriteLine("Detail : " + errorMessage);
                    writer.WriteLine();
                }
                
                writer.Write(str);
            }
            
        }

        public void saveToTextAndMessageToUser(Exception ex)
        {
            string errorMessage = ex.Message;
            Directory.CreateDirectory(@"D:\StockAssistant\SystemError");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemError\Error_" + today + ".txt";

            string str = "";

            if (File.Exists(filePath))
            {
                using (StreamReader sreader = new StreamReader(filePath))
                {
                    str = sreader.ReadToEnd();
                }

                File.Delete(filePath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    MessageBox.Show("An unexpected error has occurred. Please contact your system administrator.\n\n"+ex.Message, "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine();
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine();
                    writer.WriteLine("StackTrace : ");
                    writer.WriteLine(ex.StackTrace);

                    ex = ex.InnerException;
                }
                writer.Write(str);
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            str = "";
            if (File.Exists(filePath))
            {
                using (StreamReader sreader = new StreamReader(filePath))
                {
                    str = sreader.ReadToEnd();
                }
                File.Delete(filePath);
            }


            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (errorMessage != null)
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    writer.WriteLine("Action : SYSTEM ERROR");
                    writer.WriteLine();
                    writer.WriteLine("Detail : " + errorMessage);
                    writer.WriteLine();
                }

                writer.Write(str);
            }
            
           

        }

        public void historyRecord(string action, string detail, DateTime date, int by)
        {
            string machineName = Environment.MachineName;
            string ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(1).ToString();

            //save history
            historyDAL dalHistory = new historyDAL();
            historyBLL uHistory = new historyBLL();

            userDAL dalUser = new userDAL();
            uHistory.history_date = date;
            uHistory.history_by = by;
            uHistory.history_action = "["+ dalUser.getUsername(by)+" ("+ machineName + " "+ip+" ) "+"] "+action;
            uHistory.history_detail = detail;

            bool result = dalHistory.insert(uHistory);

            if (!result)
            {
                MessageBox.Show("Failed to add new history");
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            string str = "";
            if (File.Exists(filePath))
            {
                using (StreamReader sreader = new StreamReader(filePath))
                {
                    str = sreader.ReadToEnd();
                }
                File.Delete(filePath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();
                writer.WriteLine("Action : "+action);
                writer.WriteLine();
                writer.WriteLine("Detail : " + detail);
                writer.WriteLine();
                writer.WriteLine("By : " + dalUser.getUsername(by));
                writer.WriteLine();

                writer.Write(str);
            }
        }

        public void historyRecord(string action, string detail, DateTime date, int by, string page_name, int data_id)
        {
            string machineName = Environment.MachineName;
            string ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(1).ToString();

            //save history
            historyDAL dalHistory = new historyDAL();
            historyBLL uHistory = new historyBLL();

            userDAL dalUser = new userDAL();
            uHistory.history_date = date;
            uHistory.history_by = by;
            uHistory.history_action = "[" + dalUser.getUsername(by) + " (" + machineName + " " + ip + " ) " + "] " + action;
            uHistory.history_detail = detail;
            uHistory.page_name = page_name;
            uHistory.data_id = data_id;

            bool result = dalHistory.insertWithDataID(uHistory);

            if (!result)
            {
                MessageBox.Show("Failed to add new history");
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            string str = "";
            if (File.Exists(filePath))
            {
                using (StreamReader sreader = new StreamReader(filePath))
                {
                    str = sreader.ReadToEnd();
                }
                File.Delete(filePath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();
                writer.WriteLine("Action : " + action);
                writer.WriteLine();
                writer.WriteLine("Detail : " + detail);
                writer.WriteLine();
                writer.WriteLine("By : " + dalUser.getUsername(by));
                writer.WriteLine();
                writer.WriteLine("Page: " + page_name+"; Data ID: "+ data_id);
                writer.WriteLine();

                writer.Write(str);
            }

        }

        #endregion
    }
}
