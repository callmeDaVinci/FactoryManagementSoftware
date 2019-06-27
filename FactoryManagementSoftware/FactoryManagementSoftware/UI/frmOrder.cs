using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using FactoryManagementSoftware.Module;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Tulpep.NotificationWindow;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_TWO)
            {
                btnOrder.Show();
            }
            else
            {
                btnOrder.Hide();
            }
            tool.loadMaterialAndAllToComboBox(cmbType);
        }


        #region variable declare

        private int selectedOrderID = -1;
        static public string finalOrderNumber;
        static public string receivedNumber;
        static public string note;
        static public bool cancel;
        static public bool receivedReturn = false;
        static public bool orderApproved = false;

        private int userPermission = -1;

        readonly string status_Received = "RECEIVED";
        readonly string status_Requesting = "REQUESTING";
        readonly string status_Pending = "PENDING";
        readonly string status_Cancelled = "CANCELLED";
        readonly string headerPONO = "P/O NO";
        readonly string headerID = "ID";
        readonly string headerDateRequired = "DATE REQUIRED";
        readonly string headerCat = "CATEGORY";
        readonly string headerOrdered = "ORDERED";
        readonly string headerPending = "PENDING / OVER";
        readonly string headerReceived = "RECEIVED";
        readonly string headerUnit = "UNIT";
        readonly string headerStatus = "STATUS";
        readonly string headerIndex = "#";
        readonly string headerType = "TYPE";
        readonly string headerMat = "MATERIAL";
        readonly string headerCode = "CODE";
        readonly string headerName = "NAME";
        readonly string headerMB = "MB";
        readonly string headerMBRate = "MB RATE";
        readonly string headerWeight = "WEIGHT";
        readonly string headerWastage = "WASTAGE %";
        readonly string headerReadyStock = "READY STOCK";
        readonly string headerZeroCostStock = "ZERO COST STOCK";
        private string headerBalanceZero = "FORECAST BAL 0";
        private string headerBalanceOne = "FORECAST BAL 1";
        private string headerBalanceTwo = "FORECAST BAL 2";
        private string headerBalanceThree = "FORECAST BAL 3";
        private string headerBalanceFour = "FORECAST BALANCE 4";
        readonly string headerPendingOrder = "PENDING ORDER";

        readonly string headerForecast1 = "FORECAST 1";
        readonly string headerForecast2 = "FORECAST 2";
        readonly string headerForecast3 = "FORECAST 3";
        readonly string headerForecast4 = "FORECAST 4";
        //readonly string headerOutStock = "OUT STOCK";

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;
        #endregion

        #region create class object (database)
        custBLL uCust = new custBLL();
        custDAL dalCust = new custDAL();

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();
        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

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
        Text text = new Text();
        #endregion

        #region UI setting

        private DataTable NewOrderRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerDateRequired, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerCat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerPONO, typeof(int));
            dt.Columns.Add(headerOrdered, typeof(float));
            dt.Columns.Add(headerPending, typeof(float));
            dt.Columns.Add(headerReceived, typeof(float));
            dt.Columns.Add(headerUnit, typeof(string));
            dt.Columns.Add(headerStatus, typeof(string));

            return dt;
        }

        private DataTable NewOrderAlertTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerBalanceOne, typeof(float));
            dt.Columns.Add(headerBalanceTwo, typeof(float));
            dt.Columns.Add(headerBalanceThree, typeof(float));
            dt.Columns.Add(headerBalanceFour, typeof(float));
            dt.Columns.Add(headerPendingOrder, typeof(float));

            return dt;
        }

        private DataTable NewMatTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add(headerMat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerMB, typeof(string));
            dt.Columns.Add(headerMBRate, typeof(float));
            dt.Columns.Add(headerReadyStock, typeof(float));
            dt.Columns.Add(headerWeight, typeof(float));
            dt.Columns.Add(headerWastage, typeof(float));
            dt.Columns.Add(headerBalanceZero, typeof(int));
            dt.Columns.Add(headerBalanceOne, typeof(int));
            dt.Columns.Add(headerBalanceTwo, typeof(int));
            dt.Columns.Add(headerBalanceThree, typeof(int));
            dt.Columns.Add(headerBalanceFour, typeof(int));
            //dt.Columns.Add(headerOutStock, typeof(float));

            return dt;
        }

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

            dgv.Columns[headerBalanceOne].HeaderText = "AFTER "+balanceOneName;
            dgv.Columns[headerBalanceTwo].HeaderText = "AFTER " + balanceTwoName;
            dgv.Columns[headerBalanceThree].HeaderText = "AFTER " + balanceThreeName;
            dgv.Columns[headerBalanceFour].HeaderText = "AFTER " + balanceFourName;

            if(cbZeroCost.Checked)
            {
                dgv.Columns[headerReadyStock].HeaderText = headerZeroCostStock;
            }
            else
            {
                dgv.Columns[headerReadyStock].HeaderText = headerReadyStock;
            }
        }

        private void dgvOrderUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerCat].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerDateRequired].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerOrdered].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPending].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerReceived].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerUnit].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStatus].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerOrdered].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerPending].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerReceived].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void createOrderAlertDatagridview()
        {
            DataGridView dgv = dgvOrderAlert;
            dgv.Columns.Clear();

            int forecastCurrentMonth = DateTime.Parse("1." + getCurrentForecastMonth() + " 2008").Month;
            int forecastNextMonth = getNextMonth(forecastCurrentMonth);
            int forecastNextNextMonth = getNextMonth(forecastNextMonth);
            int forecastNextNextNextMonth = getNextMonth(forecastNextNextMonth);

            string balanceOneName = new DateTimeFormatInfo().GetMonthName(forecastCurrentMonth).ToUpper().ToString() + " BAL";
            string balanceTwoName = new DateTimeFormatInfo().GetMonthName(forecastNextMonth).ToUpper().ToString() + " BAL";
            string balanceThreeName = new DateTimeFormatInfo().GetMonthName(forecastNextNextMonth).ToUpper().ToString() + " BAL";
            string balanceFourName = new DateTimeFormatInfo().GetMonthName(forecastNextNextNextMonth).ToUpper().ToString() + " BAL";

            tool.AddTextBoxColumns(dgv, headerIndex, headerIndex, DisplayedCells);
            tool.AddTextBoxColumns(dgv, headerCode, dalItem.ItemCode, Fill);
            tool.AddTextBoxColumns(dgv, headerName, dalItem.ItemName, Fill);
            tool.AddTextBoxColumns(dgv, headerReadyStock, dalItem.ItemQty, DisplayedCells);
            tool.AddTextBoxColumns(dgv, balanceOneName, headerBalanceOne, DisplayedCells);
            tool.AddTextBoxColumns(dgv, balanceTwoName, headerBalanceTwo, DisplayedCells);
            tool.AddTextBoxColumns(dgv, balanceThreeName, headerBalanceThree, DisplayedCells);
            tool.AddTextBoxColumns(dgv, balanceFourName, headerBalanceFour, DisplayedCells);

            dgv.Columns[dalItem.ItemQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBalanceOne].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBalanceTwo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBalanceThree].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[headerBalanceFour].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        #endregion

        #region Load or Reset Form


        private DataTable AddDuplicates(DataTable dt)
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

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        if (i == 0)
            //        {
            //            break;
            //        }
            //        for (int j = i - 1; j >= 0; j--)
            //        {
            //            if (dt.Rows[i]["item_code"].ToString() == dt.Rows[j]["item_code"].ToString())
            //            {
            //                float readyStock = dalItem.getStockQty(dt.Rows[i]["item_code"].ToString());

            //                float jQty = Convert.ToSingle(dt.Rows[j]["quantity_order"].ToString());
            //                float iQty = Convert.ToSingle(dt.Rows[i]["quantity_order"].ToString());

            //                float stillOne = readyStock - jQty;
            //                float stllTwo = readyStock - iQty;

            //                float actualBalance = readyStock - stillOne - stllTwo;
            //                dt.Rows[j]["quantity_order"] = actualBalance.ToString();
            //                dt.Rows[i].Delete();
            //                break;
            //            }
            //        }
            //    }
            //    dt.AcceptChanges();

            //}
            //return dt;
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

        private DataTable calStillNeed(DataTable dt)
        {
            float qty0,qty1,qty2,qty3,qty4,readyStock;
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    readyStock = dt.Rows[i][headerReadyStock] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerReadyStock].ToString());
                    qty0 = dt.Rows[i][headerBalanceZero] == DBNull.Value ? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceZero].ToString());
                    qty1 = dt.Rows[i][headerBalanceOne] == DBNull.Value? 0 : Convert.ToSingle(dt.Rows[i][headerBalanceOne].ToString());
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
                        dt.Rows[i][headerBalanceTwo] = qty2 - qty1 ;
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

                    qty1 = qty1 == 0? readyStock : qty1 + readyStock;
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

        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ordFormOpen = false;
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            tool.DoubleBuffered(dgvOrder, true);
            tool.DoubleBuffered(dgvOrderAlert, true);
            resetForm();
            cmbStatusSearch.SelectedIndex = 0;
        }


        private void resetForm()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadOrderRecord();
            txtOrdSearch.Clear();

            loadOrderAlertData();
            dgvOrderAlert.ClearSelection();
            lblUpdatedTime.Text = DateTime.Now.ToString();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        
        private void loadOrderRecord()
        {
            System.Data.DataTable dtOrder = NewOrderRecordTable();
            DataRow dtOrder_row;
            DataTable dt_itemInfo = dalItem.Select();

            string keywords = txtOrdSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                string statusSearch = cmbStatusSearch.Text;
                DataTable dt = dalOrd.Search(keywords);
                dt.DefaultView.Sort = "ord_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                foreach (DataRow ord in sortedDt.Rows)
                {
                    if (statusSearch.Equals("ALL") || ord["ord_status"].ToString().Equals(statusSearch))
                    {

                        int orderID = Convert.ToInt32(ord["ord_id"].ToString());

                        if(orderID > 8)
                        {
                            dtOrder_row = dtOrder.NewRow();

                            string itemCode = ord["ord_item_code"].ToString();
                            dtOrder_row[headerID] = ord["ord_id"].ToString();
                            dtOrder_row[headerDateRequired] = Convert.ToDateTime(ord["ord_required_date"]).ToString("dd/MM/yyyy");
                            dtOrder_row[headerType] = ord["ord_type"].ToString();
                            dtOrder_row[headerCat] = tool.getCatNameFromDataTable(dt_itemInfo, itemCode);
                            dtOrder_row[headerCode] = itemCode;
                            dtOrder_row[headerName] = ord["item_name"].ToString();
                            dtOrder_row[headerPONO] = ord["ord_po_no"] == DBNull.Value ? -1 : Convert.ToInt32(ord["ord_po_no"].ToString());
                            dtOrder_row[headerOrdered] = ord["ord_qty"].ToString();
                            dtOrder_row[headerPending] = ord["ord_pending"].ToString();
                            dtOrder_row[headerReceived] = ord["ord_received"].ToString();
                            dtOrder_row[headerUnit] = ord["ord_unit"].ToString();
                            dtOrder_row[headerStatus] = ord["ord_status"].ToString();

                            dtOrder.Rows.Add(dtOrder_row);
                        }
                    }
                }
            }

            dgvOrder.DataSource = null;

            if (dtOrder.Rows.Count > 0)
            {
                dtOrder.DefaultView.Sort = "TYPE DESC";
                dgvOrder.DataSource = dtOrder;
                dgvOrderUIEdit(dgvOrder);
                dgvOrder.ClearSelection();
            }
        }

        private void refreshOrderRecord(int orderID)
        {
            //dgvOrderAlert.Rows.Clear();
            loadOrderRecord();
            dgvOrder.ClearSelection();
            if (orderID != -1)
            {
                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    if (Convert.ToInt32(row.Cells[headerID].Value.ToString()).Equals(orderID))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

   

     

        private DataTable getOrderStatusTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status", typeof(string));
            dt.Rows.Add("Requesting");
            dt.Rows.Add("Cancelled");
            dt.Rows.Add("Approved");
            dt.Rows.Add("Received");
            return dt;
        }

        private DataTable insertMaterialUsedData()
        {
            //DataTable dt = dalItemCust.Select();//load all customer's item list

            DataTable dt = dalItemCust.custSearch(tool.getCustName(1));
            dt = RemoveDuplicates(dt);
            dt.DefaultView.Sort = "cust_name ASC, item_name ASC, item_code ASC";
            dt = dt.DefaultView.ToTable();

            DataTable dtMat = NewMatTable();
            DataRow dtMat_row;

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("no data under this record.");
            }
            else
            {
                float bal_1, bal_2, bal_3, bal_4, readyStock, currentMonthOut, nextMonthOut, itemPartWeight, itemRunnerWeight, wastageAllowed;
                float forecast_1, forecast_2, forecast_3, forecast_4;
                float childBal_1, childBal_2, childBal_3, childBal_4, child_ReadyStock, mbRate, child_mbRate;
                string itemCode,MB, child_MB;
                int forecastIndex = 1, child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item["item_code"].ToString();
                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());
                    forecast_1 = item["forecast_one"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_one"]);
                    forecast_2 = item["forecast_two"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_two"]);
                    forecast_3 = item["forecast_three"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_three"]);
                    forecast_4 = item["forecast_four"] == DBNull.Value? 0 : Convert.ToInt32(item["forecast_four"]);
                    itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());
                    itemRunnerWeight = item["item_runner_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_runner_weight"].ToString());
                    wastageAllowed = item["item_wastage_allowed"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_wastage_allowed"].ToString());

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
                        foreach (DataRow outRecord in dt_currentMonthTrfOutHist.Rows)
                        {
                            if (outRecord["trf_result"].ToString().Equals("Passed") && outRecord["trf_hist_item_code"].ToString().Equals(itemCode))
                            {
                                nextMonthOut += Convert.ToSingle(outRecord["trf_hist_qty"]);
                            }
                        }
                    }

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
                    if (tool.ifGotChild(itemCode,dtJoin))
                    {
                        if (!item["item_assembly"].ToString().Equals("True") && item["item_production"].ToString().Equals("True"))
                        {
                            dtMat_row = dtMat.NewRow();
                            dtMat_row[headerIndex] = forecastIndex;
                            dtMat_row[headerMat] = item["item_material"].ToString();
                            dtMat_row[headerCode] = itemCode;
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
                            //dtMat_row[headerOutStock] = currentMonthOut;

                            dtMat.Rows.Add(dtMat_row);
                            forecastIndex++;
                        }

                        //DataTable dtJoin = dalJoin.parentCheck(itemCode);//get children list
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            if(Join["parent_code"].ToString().Equals(itemCode))
                            {
                                if (Join["child_cat"].ToString().Equals("Part") || Join["child_cat"].ToString().Equals("Sub Material"))
                                {
                                    if (!string.IsNullOrEmpty(Join["child_material"].ToString()) || Join["child_cat"].ToString().Equals("Sub Material"))
                                    {
                                        child_ReadyStock = Join["child_qty"] == DBNull.Value ? 0 : Convert.ToInt32(Join["child_qty"]);
                                        child_join_qty = Convert.ToInt32(Join["join_qty"]);
                                        if (bal_1 < 0)
                                        {
                                            childBal_1 = child_ReadyStock + bal_1* child_join_qty;
                                        }
                                        else
                                        {
                                            childBal_1 = child_ReadyStock;
                                        }

                                        if (bal_2 < 0)
                                        {
                                            childBal_2 = childBal_1 + bal_2* child_join_qty;
                                        }
                                        else
                                        {
                                            childBal_2 = childBal_1;
                                        }

                                        if (bal_3 < 0)
                                        {
                                            childBal_3 = childBal_2 + bal_3* child_join_qty;
                                        }
                                        else
                                        {
                                            childBal_3 = childBal_2;
                                        }

                                        if (bal_4 < 0)
                                        {
                                            childBal_4 = childBal_3 + bal_4* child_join_qty;
                                        }
                                        else
                                        {
                                            childBal_4 = childBal_3;
                                        }

                                        dtMat_row = dtMat.NewRow();
                                        dtMat_row[headerIndex] = forecastIndex;

                                        if(Join["child_cat"].ToString().Equals("Sub Material"))
                                        {
                                            dtMat_row[headerMat] = "Sub Material";
                                        }
                                        else
                                        {
                                            dtMat_row[headerMat] = Join["child_material"].ToString();
                                        }

                                        child_MB = Join["child_mb"] == DBNull.Value ? "NULL" : Join["child_mb"].ToString();
                                        child_mbRate = Join["child_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(Join["child_mb_rate"].ToString());
                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                        dtMat_row[headerMB] = child_MB;
                                        dtMat_row[headerMBRate] = child_mbRate;
                                        dtMat_row[headerWeight] = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
                                        dtMat_row[headerWastage] = Join["child_wastage_allowed"].ToString();
                                        dtMat_row[headerReadyStock] = child_ReadyStock;
                                        dtMat_row[headerBalanceZero] = child_ReadyStock;
                                        dtMat_row[headerBalanceOne] = Convert.ToInt32(childBal_1);
                                        dtMat_row[headerBalanceTwo] = Convert.ToInt32(childBal_2);
                                        dtMat_row[headerBalanceThree] = Convert.ToInt32(childBal_3);
                                        dtMat_row[headerBalanceFour] = Convert.ToInt32(childBal_4);
                                        //dtMat_row[headerOutStock] = currentMonthOut;

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

                        if(!string.IsNullOrEmpty(item["item_material"].ToString()))
                        {
                            dtMat_row = dtMat.NewRow();
                            dtMat_row[headerIndex] = forecastIndex;
                            dtMat_row[headerMat] = item["item_material"].ToString();
                            dtMat_row[headerCode] = itemCode;
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
                            //dtMat_row[headerOutStock] = currentMonthOut;

                            dtMat.Rows.Add(dtMat_row);
                            forecastIndex++;
                        }
                       
                    }
                    #endregion
                } 
            }

            return dtMat;
        }

        private void loadOrderAlertData()
        {
            #region insert material data to datatable

            //DataTable dtMat = insertMaterialUsedData();
            DataTable dtMat;

            if(cbZeroCost.Checked)
            {
                dtMat = tool.insertZeroCostMaterialUsedData(tool.getCustName(1));
            }
            else
            {
                dtMat = tool.insertMaterialUsedData(tool.getCustName(1));
            }
            

            //dtMat = tool.calStillNeed(dtMat);

            dtMat.DefaultView.Sort = "MATERIAL ASC";
            dtMat = dtMat.DefaultView.ToTable();

            #endregion

            #region VARIABLE
            int index = 1;

            string typeCheck = cmbType.Text;  
            string material, itemCode, itemMB, itemType;
            float MB_rate, item_weight, item_wastage;
            float bal1, bal2, bal3, bal4;
            float mat_Ready_Stock;
            float temp;
            DataTable dt_itemInfo = dalItem.Select();

            DataTable dtAlert = NewOrderAlertTable();
            DataRow dtAlert_row;

            #endregion

            #region data proccessing

            foreach (DataRow item in dtMat.Rows)
            {
                itemCode = (item[headerCode] == DBNull.Value) ? null : item[headerCode].ToString();
                material = (item[headerMat] == DBNull.Value) ? null : item[headerMat].ToString();
                itemMB = (item[headerMB] == DBNull.Value) ? null : item[headerMB].ToString();
                MB_rate = Convert.ToSingle(item[headerMBRate].ToString());

                bal1 = Convert.ToSingle(item[headerBalanceOne].ToString());
                bal2 = Convert.ToSingle(item[headerBalanceTwo].ToString());
                bal3 = Convert.ToSingle(item[headerBalanceThree].ToString());
                bal4 = Convert.ToSingle(item[headerBalanceFour].ToString());

                item_weight = Convert.ToSingle(item[headerWeight].ToString());
                item_wastage = Convert.ToSingle(item[headerWastage].ToString());

                if (material != null)
                {
                    if(material.Equals("Sub Material"))
                    {
                        
                        #region add Sub Material to datatable

                        itemType = tool.getCatNameFromDataTable(dt_itemInfo, itemCode);


                        if (typeCheck.Equals("All") || typeCheck.Equals(itemType))
                        {
                            if(cbZeroCost.Checked)
                            {
                                float mat_Zero_Stock = tool.getPMMAQtyFromDataTable(dt_itemInfo, itemCode);
                                mat_Ready_Stock = tool.getStockQtyFromDataTable(dt_itemInfo, itemCode);

                                bal1 = mat_Ready_Stock - bal1;
                                bal1 = mat_Zero_Stock - bal1;

                                bal2 = mat_Ready_Stock - bal2;
                                bal2 = mat_Zero_Stock - bal2;

                                bal3 = mat_Ready_Stock - bal3;
                                bal3 = mat_Zero_Stock - bal3;

                                bal4 = mat_Ready_Stock - bal4;
                                bal4 = mat_Zero_Stock - bal4;

                                mat_Ready_Stock = mat_Zero_Stock;
                            }
                            else
                            {
                                mat_Ready_Stock = tool.getStockQtyFromDataTable(dt_itemInfo, itemCode);
                            }

                            if (mat_Ready_Stock != -1)
                            {
                                var rows = dtAlert.Select(string.Format("CODE ='{0}'", itemCode.Replace(@"'", "''"), headerCode));
                                if (rows.Length == 0)
                                {
                                    dtAlert_row = dtAlert.NewRow();

                                    dtAlert_row[headerIndex] = index;
                                    dtAlert_row[headerType] = itemType;
                                    dtAlert_row[headerCode] = itemCode;
                                    dtAlert_row[headerName] = tool.getItemNameFromDataTable(dt_itemInfo, itemCode);
                                    dtAlert_row[headerReadyStock] = Math.Round(mat_Ready_Stock , 2);
                                    dtAlert_row[headerBalanceOne] = Math.Round(bal1, 2);
                                    dtAlert_row[headerBalanceTwo] = Math.Round(bal2, 2);
                                    dtAlert_row[headerBalanceThree] = Math.Round(bal3, 2);
                                    dtAlert_row[headerBalanceFour] = Math.Round(bal4, 2);
                                    dtAlert_row[headerPendingOrder] = tool.getOrderQtyFromDataTable(dt_itemInfo, itemCode);

                                    dtAlert.Rows.Add(dtAlert_row);
                                    index++;
                                }
                                else
                                {
                                    rows[0][headerBalanceOne] = Math.Round(Convert.ToSingle(rows[0][headerBalanceOne]) + bal1, 2);
                                    rows[0][headerBalanceTwo] = Math.Round(Convert.ToSingle(rows[0][headerBalanceTwo]) + bal2, 2);
                                    rows[0][headerBalanceThree] = Math.Round(Convert.ToSingle(rows[0][headerBalanceThree]) + bal3, 2);
                                    rows[0][headerBalanceFour] = Math.Round(Convert.ToSingle(rows[0][headerBalanceFour]) + bal4, 2);
                                }
                            }
                               
                        }

                        #endregion
                    }
                    else
                    {
                        #region cal material used weight

                        temp = Convert.ToSingle(item[headerBalanceOne].ToString());
                        bal1 = temp >= 0 ? 0 : temp;

                        temp = Convert.ToSingle(item[headerBalanceTwo].ToString());
                        bal2 = temp >= 0 ? 0 : temp;

                        temp = Convert.ToSingle(item[headerBalanceThree].ToString());
                        bal3 = temp >= 0 ? 0 : temp;

                        temp = Convert.ToSingle(item[headerBalanceFour].ToString());
                        bal4 = temp >= 0 ? 0 : temp;

                        bal1 *= item_weight / 1000 * (1 + item_wastage);
                        bal2 *= item_weight / 1000 * (1 + item_wastage);
                        bal3 *= item_weight / 1000 * (1 + item_wastage);
                        bal4 *= item_weight / 1000 * (1 + item_wastage);

                        #endregion

                        #region add RAW Material to datatable

                        itemType = tool.getCatNameFromDataTable(dt_itemInfo,material);

                       

                        var rows = dtAlert.Select(string.Format("CODE ='{0}'", material.Replace(@"'", "''"), headerCode));
                        if (typeCheck.Equals("All") || typeCheck.Equals(itemType))
                        {
                            if (cbZeroCost.Checked)
                            {
                                mat_Ready_Stock = tool.getPMMAQtyFromDataTable(dt_itemInfo, material);
                            }
                            else
                            {
                                mat_Ready_Stock = tool.getStockQtyFromDataTable(dt_itemInfo, material);
                            }

                            if (mat_Ready_Stock != -1)
                            {
                                if (rows.Length == 0)
                                {
                                    dtAlert_row = dtAlert.NewRow();

                                    dtAlert_row[headerIndex] = index;
                                    dtAlert_row[headerType] = itemType;
                                    dtAlert_row[headerCode] = material;
                                    dtAlert_row[headerName] = tool.getItemNameFromDataTable(dt_itemInfo, material);
                                    dtAlert_row[headerReadyStock] = Math.Round(mat_Ready_Stock, 2);
                                    dtAlert_row[headerBalanceOne] = Math.Round(mat_Ready_Stock + bal1, 2);
                                    dtAlert_row[headerBalanceTwo] = Math.Round(mat_Ready_Stock + bal2, 2);
                                    dtAlert_row[headerBalanceThree] = Math.Round(mat_Ready_Stock + bal3, 2);
                                    dtAlert_row[headerBalanceFour] = Math.Round(mat_Ready_Stock + bal4, 2);
                                    dtAlert_row[headerPendingOrder] = tool.getOrderQtyFromDataTable(dt_itemInfo, material);

                                    dtAlert.Rows.Add(dtAlert_row);
                                    index++;
                                }
                                else
                                {
                                    rows[0][headerBalanceOne] = Math.Round(Convert.ToSingle(rows[0][headerBalanceOne]) + bal1, 2);
                                    rows[0][headerBalanceTwo] = Math.Round(Convert.ToSingle(rows[0][headerBalanceTwo]) + bal2, 2);
                                    rows[0][headerBalanceThree] = Math.Round(Convert.ToSingle(rows[0][headerBalanceThree]) + bal3, 2);
                                    rows[0][headerBalanceFour] = Math.Round(Convert.ToSingle(rows[0][headerBalanceFour]) +  bal4, 2);
                                }
                            }
                               

                        }

                        #endregion

                        #region add MB OR Pigment to datatable

                        if (itemMB != null && itemMB != "" && MB_rate > 0)
                        {
                            itemType = tool.getCatNameFromDataTable(dt_itemInfo, itemMB);

                            if (typeCheck.Equals("All") || typeCheck.Equals(itemType))
                            {

                                if (cbZeroCost.Checked)
                                {
                                    mat_Ready_Stock = tool.getPMMAQtyFromDataTable(dt_itemInfo, itemMB);
                                }
                                else
                                {
                                    mat_Ready_Stock = tool.getStockQtyFromDataTable(dt_itemInfo, itemMB);
                                }

                                if(mat_Ready_Stock != -1)
                                {
                                    rows = dtAlert.Select(string.Format("CODE ='{0}'", itemMB.Replace(@"'", "''"), headerCode));
                                    if (rows.Length == 0)
                                    {
                                        dtAlert_row = dtAlert.NewRow();

                                        dtAlert_row[headerIndex] = index;
                                        dtAlert_row[headerType] = itemType;
                                        dtAlert_row[headerCode] = itemMB;
                                        dtAlert_row[headerName] = tool.getItemNameFromDataTable(dt_itemInfo, itemMB);
                                        dtAlert_row[headerReadyStock] = Math.Round(mat_Ready_Stock,2);
                                        dtAlert_row[headerBalanceOne] = Math.Round(mat_Ready_Stock + bal1 * MB_rate, 2);
                                        dtAlert_row[headerBalanceTwo] = Math.Round(mat_Ready_Stock +  bal2 * MB_rate, 2);
                                        dtAlert_row[headerBalanceThree] = Math.Round(mat_Ready_Stock + bal3 * MB_rate, 2);
                                        dtAlert_row[headerBalanceFour] = Math.Round(mat_Ready_Stock +  bal4 * MB_rate, 2);
                                        dtAlert_row[headerPendingOrder] = tool.getOrderQtyFromDataTable(dt_itemInfo, itemMB);

                                        dtAlert.Rows.Add(dtAlert_row);
                                        index++;
                                    }
                                    else
                                    {
                                        rows[0][headerBalanceOne] = Math.Round(Convert.ToSingle(rows[0][headerBalanceOne]) + bal1 * MB_rate, 2);
                                        rows[0][headerBalanceTwo] = Math.Round(Convert.ToSingle(rows[0][headerBalanceTwo]) +  bal2 * MB_rate, 2);
                                        rows[0][headerBalanceThree] = Math.Round(Convert.ToSingle(rows[0][headerBalanceThree]) + bal3 * MB_rate, 2);
                                        rows[0][headerBalanceFour] = Math.Round(Convert.ToSingle(rows[0][headerBalanceFour])  + bal4 * MB_rate, 2);
                                    }
                                }
                                
                            }
                              
                        }

                        #endregion
                    }
                }

                #region old way
                //currentMaterial = item[headerMat].ToString();
                //if (lastMaterial != null && lastMaterial != currentMaterial)
                //{
                //    //readyStock = dalItem.getStockQty(lastMaterial);

                //    balanceOne = readyStock - totalMaterialUsed;
                //    balanceTwo = readyStock - totalMaterialUsedTwo;
                //    balanceThree = readyStock - totalMaterialUsedThree;

                //    if (!string.IsNullOrEmpty(lastMaterial))
                //    {
                //        dtAlert_row = dtAlert.NewRow();
                //        dtAlert_row[headerIndex] = index;
                //        dtAlert_row[headerCode] = lastMaterial;
                //        dtAlert_row[headerName] = dalItem.getMaterialName(lastMaterial);
                //        dtAlert_row[headerReadyStock] = readyStock;
                //        dtAlert_row[headerBalanceOne] = balanceOne;
                //        dtAlert_row[headerBalanceTwo] = balanceTwo;
                //        dtAlert_row[headerBalanceThree] = balanceThree;
                //        dtAlert_row[headerPendingOrder] = 0;

                //        dtAlert.Rows.Add(dtAlert_row);
                //        index++;
                //    }

                //    totalMaterialUsed = 0;
                //    totalMaterialUsedTwo = 0;
                //    totalMaterialUsedThree = 0;
                //}

                //lastMaterial = currentMaterial;

                ////readyStock = dalItem.getStockQty(item[headerCode].ToString());

                //itemWeight = (item[headerWeight] == DBNull.Value) ? 0 : Convert.ToSingle(item[headerWeight].ToString());
                //wastagePercetage = (item[headerWastage] == DBNull.Value) ? 0 : Convert.ToSingle(item[headerWastage].ToString());


                //OrderQty = Convert.ToSingle(item[headerBalanceOne].ToString());
                //OrderQtyTwo = Convert.ToSingle(item[headerBalanceTwo].ToString());
                //OrderQtyThree = Convert.ToSingle(item[headerBalanceThree].ToString());

                //if (true)//non child part, dalItemCust.checkIfExistinItemCustTable(item[headerCode].ToString())
                //{
                //    stillNeed1 = OrderQty;

                //    stillNeed2 = OrderQtyTwo;

                //    stillNeed3 = OrderQtyThree;
                //}
                //else//child part
                //{
                //    if (Convert.ToSingle(item[headerForecast1].ToString()) >= 0)
                //    {
                //        childForecast1 = 0;
                //    }
                //    else
                //    {
                //        childForecast1 = Convert.ToSingle(item[headerForecast1].ToString());
                //    }

                //    childShot1 =  Math.Abs(childForecast1);

                //    if (Convert.ToSingle(item[headerForecast2].ToString()) >= 0)
                //    {
                //        childForecast2 = 0;
                //    }
                //    else
                //    {
                //        if (Convert.ToSingle(item[headerForecast1].ToString()) < 0)
                //        {
                //            childForecast2 = Convert.ToSingle(item[headerForecast1].ToString()) - Convert.ToSingle(item[headerForecast2].ToString());
                //        }
                //        else
                //        {
                //            childForecast2 = Convert.ToSingle(item[headerForecast2].ToString());
                //        }
                //    }

                //    childShot2 = childShot1 - Math.Abs(childForecast2);

                //    if (Convert.ToSingle(item[headerForecast3].ToString()) >= 0)
                //    {
                //        childForecast3 = 0;
                //    }
                //    else
                //    {
                //        if (Convert.ToSingle(item[headerForecast2].ToString()) < 0)
                //        {
                //            childForecast3 = Convert.ToSingle(item[headerForecast2].ToString()) - Convert.ToSingle(item[headerForecast3].ToString());
                //        }
                //        else
                //        {
                //            childForecast3 = Convert.ToSingle(item[headerForecast3].ToString());
                //        }
                //    }

                //    childShot3 = childShot2 - Math.Abs(childForecast3);

                //    stillNeed1 = childShot1;

                //    stillNeed2 = childShot2;

                //    stillNeed3 = childShot3;
                //}

                //stillNeed1 = tool.stillNeedCheck(stillNeed1);
                //stillNeed2 = tool.stillNeedCheck(stillNeed2);
                //stillNeed3 = tool.stillNeedCheck(stillNeed3);

                //materialUsed = stillNeed1 * itemWeight / 1000;
                //wastageUsed = materialUsed * wastagePercetage;
                //totalMaterialUsed += materialUsed + wastageUsed;

                //materialUsedTwo = stillNeed2 * itemWeight / 1000;
                //wastageUsedTwo = materialUsedTwo * wastagePercetage;
                //totalMaterialUsedTwo += materialUsedTwo + wastageUsedTwo;

                //materialUsedThree = stillNeed3 * itemWeight / 1000;
                //wastageUsedThree = materialUsedThree * wastagePercetage;
                //totalMaterialUsedThree += materialUsedThree + wastageUsedThree;

                #endregion
            }

            #endregion

            #region load data to datagridview

            dgvOrderAlert.DataSource = null;

            if (dtAlert.Rows.Count > 0)
            {
                dtAlert.DefaultView.Sort = "TYPE ASC";
                dgvOrderAlert.DataSource = dtAlert;
                dgvAlertUIEdit(dgvOrderAlert);
                dgvOrderAlert.ClearSelection();
            }

            #endregion
        }

        #endregion


        #region data Insert/Update/Search

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            frmOrderRequest frm = new frmOrderRequest();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//create new order

            if(frmOrderRequest.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                resetForm();
                frmOrderRequest.orderSuccess = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtOrdSearch_TextChanged(object sender, EventArgs e)
        {
            loadOrderRecord();
        }

        private void addOrderAction(string action, int orderID)
        {
            if(orderID < 0)//create a new action record after new order has created
            {
                //get the last record from tbl_ord
                DataTable lastRecord = dalOrd.lastRecordSelect();

                foreach (DataRow ord in lastRecord.Rows)
                {
                    uOrderAction.ord_id = Convert.ToInt32(ord["ord_id"]);
                    uOrderAction.added_date = Convert.ToDateTime(ord["ord_added_date"]);
                    uOrderAction.added_by = Convert.ToInt32(ord["ord_added_by"]); ;
                    uOrderAction.action = action;
                    uOrderAction.note = "";

                    if (!dalOrderAction.Insert(uOrderAction))
                    {
                        MessageBox.Show("Failed to create new action");
                        tool.historyRecord(text.System, "Failed to create new action(frmOrder)", DateTime.Now, MainDashboard.USER_ID);
                    }
                }
            }
           else//action record exist,add another action
            {
                uOrderAction.ord_id = orderID;
                uOrderAction.added_date = DateTime.Now;
                uOrderAction.added_by = MainDashboard.USER_ID;
                uOrderAction.action = action;
                uOrderAction.note = "";

                if (!dalOrderAction.Insert(uOrderAction))
                {
                    MessageBox.Show("Failed to add new action");
                    tool.historyRecord(text.System, "Failed to add new action(frmOrder)", DateTime.Now, MainDashboard.USER_ID);

                }
            }
        }

        #endregion

        #region order status change

        private void orderRequest(int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //get data from datagridview
            // item category, item code, item name, order id, 
            string id = "";
            string type = "";
            string cat = "";
            string itemCode = "";
            string itemName = "";
            string qty = "";
            string unit = "";
            string date = "";
            DataTable dt = dalOrd.Select(orderID);

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow ord in dt.Rows)
                {
                    id = ord["ord_id"].ToString();
                    cat = ord["item_cat"].ToString();
                    itemCode = ord["ord_item_code"].ToString();
                    itemName = ord["item_name"].ToString();
                    type = ord["ord_type"].ToString();
                    qty = ord["ord_qty"].ToString();
                    unit = ord["ord_unit"].ToString();
                    date = Convert.ToDateTime(ord["ord_required_date"].ToString()).ToString("dd/MM/yyyy");

                }
            }
 
            frmOrderRequest frm = new frmOrderRequest(id, cat, itemCode, itemName, qty, unit, date, type);
  
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//order request

            if (frmOrderRequest.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                frmOrderRequest.orderSuccess = false;
                resetForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }

            refreshOrderRecord(selectedOrderID);

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderAprove(int rowIndex, int orderID)
        {
            try
            {
                if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                {
                    string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
                    string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
                    string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
                    string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
                    string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
                    string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();
                    int po_no = dgvOrder.Rows[rowIndex].Cells[headerPONO].Value == DBNull.Value ? -1 : Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerPONO].Value.ToString());


                    //approve form
                    frmOrderApprove frm = new frmOrderApprove(orderID.ToString(), requiredDate, itemName, itemCode, qty, unit, type, po_no);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                    if (orderApproved)//if order approved from approve form, then change order status from requesting to pending
                    {
                        dalItem.orderAdd(itemCode, finalOrderNumber);//add order qty to item
                        refreshOrderRecord(selectedOrderID);
                        orderApproved = false;
                    }
                }
                else
                {
                    MessageBox.Show("Action denied. Please contact admin for this action.");
                    tool.historyRecord(text.System, "Action denied. Please contact admin for this action.(frmOrder)", DateTime.Now, MainDashboard.USER_ID);

                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }


        }

        private void orderEdit(int rowIndex, int orderID)
        {
            try
            {
                if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                {
                    string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
                    string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
                    string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
                    string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
                    string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
                    string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();
                    int po_no = dgvOrder.Rows[rowIndex].Cells[headerPONO].Value == DBNull.Value ? -1 : Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerPONO].Value.ToString());


                    //approve form
                    frmOrderApprove frm = new frmOrderApprove(orderID.ToString(), requiredDate, itemName, itemCode, qty, unit, type, po_no);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                    if (orderApproved)//if order approved from approve form, then change order status from requesting to pending
                    {
                        dalItem.orderAdd(itemCode, Convert.ToSingle(finalOrderNumber) -Convert.ToSingle(qty));//add order qty to item
                        refreshOrderRecord(selectedOrderID);
                        orderApproved = false;
                    }
                }
                else
                {
                    MessageBox.Show("Action denied. Please contact admin for this action.");
                    tool.historyRecord(text.System, "Action denied. Please contact admin for this action.(frmOrder)", DateTime.Now, MainDashboard.USER_ID);

                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }


        }

        private void orderReceive(int rowIndex, int orderID)
        {
            //get data from datagridview
            string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
            string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
            string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
            string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
            string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
            float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
            string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

            frmOrderReceive frm = new frmOrderReceive(orderID,itemCode,itemName,Convert.ToSingle(qty),Convert.ToSingle(received), unit, type);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//stock in

            refreshOrderRecord(orderID);

        }

        private void orderCancel(int rowIndex, int orderID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //DialogResult dialogResult = MessageBox.Show("Are you sure want to cancel this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
            string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
            string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
            string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
            string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
            float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
            string pending = dgvOrder.Rows[rowIndex].Cells[headerPending].Value.ToString();
            string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

            if (received > 0)//if have received record under this order ,then need to return this item from stock before cancel this order
            {
            DialogResult dialogResult = MessageBox.Show(@"There is an existing received record under this order. Please undo this record before canceling.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.OK)
                {     
                    frmOrderActionHistory frm = new frmOrderActionHistory(orderID);
                   
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//return item from stock
                }
            }
            else
            {
                frmOrderCancel frm = new frmOrderCancel();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if(cancel)
                {
                    uOrd.ord_id = orderID;
                    DateTime date = DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null);
                    uOrd.ord_required_date = date;
                    uOrd.ord_qty = Convert.ToSingle(qty);
                    uOrd.ord_pending = 0;
                    uOrd.ord_received = 0;
                    uOrd.ord_updated_date = DateTime.Now;
                    uOrd.ord_updated_by = MainDashboard.USER_ID;
                    uOrd.ord_item_code = itemCode;
                    uOrd.ord_note = note;
                    uOrd.ord_unit = unit;
                    uOrd.ord_status = status_Cancelled;

                    uOrd.ord_type = type;

                    if (dalOrd.Update(uOrd))
                    {
                        if (!presentStatus.Equals(status_Requesting))
                        {
                            dalItem.orderSubtract(itemCode, pending); //subtract order qty
                        }

                        dalOrderAction.orderCancel(orderID, -1, note);
                    }
                }

            }
            refreshOrderRecord(selectedOrderID);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderComplete(int rowIndex, int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
            string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
            string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
            string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
            string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
            float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
            string pending = dgvOrder.Rows[rowIndex].Cells[headerPending].Value.ToString();
            string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

            frmOrderComplete frm = new frmOrderComplete();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            uOrd.ord_id = orderID;
            DateTime date = DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null);
            uOrd.ord_required_date = date;
            uOrd.ord_qty = Convert.ToSingle(qty);
            uOrd.ord_pending = 0;
            uOrd.ord_received = received;
            uOrd.ord_updated_date = DateTime.Now;
            uOrd.ord_updated_by = MainDashboard.USER_ID;
            uOrd.ord_item_code = itemCode;
            uOrd.ord_note = note;
            uOrd.ord_unit = unit;
            uOrd.ord_status = status_Received;
            uOrd.ord_type = type;

            if (dalOrd.Update(uOrd))
            {
                dalItem.orderSubtract(itemCode, pending); //subtract order qty

                dalOrderAction.orderClose(orderID, note);
            }
            
            refreshOrderRecord(selectedOrderID);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderIncomplete(int rowIndex, int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to reopen this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {

                string itemCode = dgvOrder.Rows[rowIndex].Cells[headerCode].Value.ToString();
                string itemName = dgvOrder.Rows[rowIndex].Cells[headerName].Value.ToString();
                string requiredDate = dgvOrder.Rows[rowIndex].Cells[headerDateRequired].Value.ToString();
                string qty = dgvOrder.Rows[rowIndex].Cells[headerOrdered].Value.ToString();
                string unit = dgvOrder.Rows[rowIndex].Cells[headerUnit].Value.ToString();
                float received = Convert.ToSingle(dgvOrder.Rows[rowIndex].Cells[headerReceived].Value);
                string pending = dgvOrder.Rows[rowIndex].Cells[headerPending].Value.ToString();
                string type = dgvOrder.Rows[rowIndex].Cells[headerType].Value == DBNull.Value ? "PURCHASE" : dgvOrder.Rows[rowIndex].Cells[headerType].Value.ToString();

                uOrd.ord_id = orderID;
                DateTime date = DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null);
                uOrd.ord_required_date = date;
                uOrd.ord_qty = Convert.ToSingle(qty);
                uOrd.ord_pending = uOrd.ord_qty - received;
                uOrd.ord_received = received;
                uOrd.ord_updated_date = DateTime.Now;
                uOrd.ord_updated_by = MainDashboard.USER_ID;
                uOrd.ord_item_code = itemCode;
                uOrd.ord_note = "";
                uOrd.ord_unit = unit;
                uOrd.ord_status = status_Pending;
                uOrd.ord_type = type;

                if (dalOrd.Update(uOrd))
                {
                    dalItem.orderAdd(itemCode, uOrd.ord_qty - received); //subtract order qty

                    dalOrderAction.orderReOpen(orderID, "");
                }

                refreshOrderRecord(selectedOrderID);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }


        #endregion

        #region  click / selected index changed action

       
        //handle what will happen when item clicked after right click on datagridview
        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            DataGridView dgv = dgvOrder;
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int orderID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);
            string presentStatus = dgv.Rows[rowIndex].Cells[headerStatus].Value.ToString();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();
            if(itemClicked.Equals("Request"))
            {
                orderRequest(orderID);
            }
            else if (itemClicked.Equals("Approve"))
            {
                orderAprove(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Edit"))
            {
                orderEdit(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Cancel"))
            {
                orderCancel(rowIndex, orderID, presentStatus);
            }
            else if (itemClicked.Equals("Receive"))
            {
                orderReceive(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Complete"))
            {
                orderComplete(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Incomplete"))
            {
                orderIncomplete(rowIndex, orderID);
            }

            loadOrderAlertData();
            dgvOrderAlert.ClearSelection();
            lblUpdatedTime.Text = DateTime.Now.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

    

       
        
        //clear selection when mouse click on empty space
        private void frmOrder_Click(object sender, EventArgs e)
        {
            dgvOrder.ClearSelection();
            dgvOrderAlert.ClearSelection();
        }

        //input text search
        private void cmbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadOrderRecord();
        }

        //sort data according number
        private void dgvOrd_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
           
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
        }

        #endregion

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

        private void dgvOrderAlert_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                float bal1, bal2, bal3, bal4, readyStock;
                string itemCode = dgvOrderAlert.Rows[rowIndex].Cells[headerCode].Value.ToString();

                readyStock = Convert.ToSingle(dgvOrderAlert.Rows[rowIndex].Cells[headerReadyStock].Value.ToString());
                bal1 = Convert.ToSingle(dgvOrderAlert.Rows[rowIndex].Cells[headerBalanceOne].Value.ToString());
                bal2 = Convert.ToSingle(dgvOrderAlert.Rows[rowIndex].Cells[headerBalanceTwo].Value.ToString());
                bal3 = Convert.ToSingle(dgvOrderAlert.Rows[rowIndex].Cells[headerBalanceThree].Value.ToString());
                bal4 = Convert.ToSingle(dgvOrderAlert.Rows[rowIndex].Cells[headerBalanceFour].Value.ToString());

                DataTable dt = NewOrderAlertTable();
                DataRow dt_row;  

                
                dt_row = dt.NewRow();

                dt_row[headerIndex] = 1;
                dt_row[headerType] = dgvOrderAlert.Rows[rowIndex].Cells[headerType].Value.ToString();
                dt_row[headerCode] = itemCode;
                dt_row[headerName] = dgvOrderAlert.Rows[rowIndex].Cells[headerName].Value.ToString();
                dt_row[headerReadyStock] = readyStock;
                dt_row[headerBalanceOne] = bal1;
                dt_row[headerBalanceTwo] = bal2;
                dt_row[headerBalanceThree] = bal3;
                dt_row[headerBalanceFour] = bal4;
                dt_row[headerPendingOrder] = dgvOrderAlert.Rows[rowIndex].Cells[headerPendingOrder].Value.ToString();

                dt.Rows.Add(dt_row);

                frmOrderAlertDetail frm = new frmOrderAlertDetail(itemCode,dt);

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
        }

        private void dgvOrderAlert_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
            
        }

        private void dgvOrderAlert_Sorted(object sender, EventArgs e)
        {
            dgvOrderAlert.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadOrderAlertData();
            dgvOrderAlert.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
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
                        float num = dgv.Rows[row].Cells[col].Value == DBNull.Value? 0 : Convert.ToSingle(dgv.Rows[row].Cells[col].Value.ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {

            resetForm();
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

    
        private void dgvOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvOrder;
            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerOrdered)
            {
                dgv.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(232, 244, 66);
            }
            else if (dgv.Columns[col].Name == headerPending)
            {
                dgv.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(66, 191, 244);
            }
            else if (dgv.Columns[col].Name == headerReceived)
            {
                dgv.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(66, 244, 161);
            }
            else if (dgv.Columns[col].Name == headerStatus)
            {
                string value = dgv.Rows[row].Cells[headerStatus].Value.ToString();
                Color foreColor = dgvOrder.DefaultCellStyle.ForeColor;

                if (value.Equals(status_Requesting))
                {
                    foreColor = Color.FromArgb(244, 170, 66);
                }
                else if (value.Equals(status_Cancelled))
                {
                    foreColor = Color.FromArgb(234, 67, 53);
                }
                else if (value.Equals(status_Pending))
                {
                    foreColor = Color.FromArgb(66, 133, 244);
                }
                else if (value.Equals(status_Received))
                {
                    foreColor = Color.FromArgb(52, 168, 83);
                }
                dgv.Rows[row].Cells[headerStatus].Style.ForeColor = foreColor;
            }

            dgv.ResumeLayout();
        }

        private void dgvOrder_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
        }

        private void dgvOrder_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvOrder.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvOrder.ClearSelection();
            }
        }

        private void dgvOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgvOrder.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                int orderID = Convert.ToInt32(dgvOrder.Rows[rowIndex].Cells[headerID].Value);

                DataTable dt = dalOrderAction.Select(orderID);
                if (dt.Rows.Count > 0)
                {
                    frmOrderActionHistory frm = new frmOrderActionHistory(orderID);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit
                }
                else
                {
                    MessageBox.Show("No action record under this order yet.");
                }
            }

            if (receivedReturn)//if order approved from approve form, then change order status from requesting to pending
            {
                refreshOrderRecord(selectedOrderID);
                receivedReturn = false;
            }

            loadOrderAlertData();
            dgvOrderAlert.ClearSelection();
            lblUpdatedTime.Text = DateTime.Now.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvOrder_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && userPermission >= MainDashboard.ACTION_LVL_TWO)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvOrder.CurrentCell = dgvOrder.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvOrder.Rows[e.RowIndex].Selected = true;
                dgvOrder.Focus();
                int rowIndex = dgvOrder.CurrentCell.RowIndex;

                try
                {
                    string result = dgvOrder.Rows[rowIndex].Cells[headerStatus].Value.ToString();

                    if (result.Equals(status_Requesting))
                    {
                        if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                        {
                            my_menu.Items.Add("Approve").Name = "Approve";
                        }

                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }
                    else if (result.Equals(status_Cancelled))
                    {
                        my_menu.Items.Add("Request").Name = "Request";
                    }
                    else if (result.Equals(status_Pending))
                    {
                        my_menu.Items.Add("Receive").Name = "Receive";

                        if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                        {
                            my_menu.Items.Add("Edit").Name = "Edit";
                            my_menu.Items.Add("Complete").Name = "Complete";
                        }

                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }
                    else if (result.Equals(status_Received))
                    {
                        if (userPermission >= MainDashboard.ACTION_LVL_THREE)
                        {
                            my_menu.Items.Add("Incomplete").Name = "Incomplete";
                        }

                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }

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

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "OrderReport(" + cmbStatusSearch.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void copyAlltoClipboard()
        {
            dgvOrder.SelectAll();
            DataObject dataObj = dgvOrder.GetClipboardContent();
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {

                dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();

                string path = @"D:\StockAssistant\Document\OrderReport";
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
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Name = cmbStatusSearch.Text;

                    #region Save data to Sheet

                    //Header and Footer setup
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&16 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 ORDERING RECORD";
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&16 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                    //Page setup

                    


                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.Zoom = false;
                    xlWorkSheet.PageSetup.CenterHorizontally = true;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.4 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 1.4 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

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
                    dgvOrder.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                dgvOrder.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }
    }
}
