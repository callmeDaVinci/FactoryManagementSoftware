using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using Org.BouncyCastle.Asn1.Ocsp;

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
        trfHistDAL daltrfHist = new trfHistDAL();

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

        readonly string headerOutOne = "OUT 1";
        readonly string headerOutTwo = "OUT 2";
        readonly string headerOutThree = "OUT 3";
        readonly string headerOutFour = "OUT 4";

        readonly string headerForecastOne = "Forecast 1";
        readonly string headerForecastTwo = "Forecast 2";
        readonly string headerForecastThree = "Forecast 3";
        readonly string headerForecastFour = "Forecast 4";

        readonly string headerZeroCostStock = "ZERO COST STOCK";
        private string headerBalanceZero = "FORECAST BAL 0";
        private string headerBalanceOne = "FORECAST BAL 1";
        private string headerBalanceTwo = "FORECAST BAL 2";
        private string headerBalanceThree = "FORECAST BAL 3";
        private string headerBalanceFour = "FORECAST BALANCE 4";
        readonly string headerPendingOrder = "PENDING ORDER";

        #endregion

        #region UI design

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
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
        }

        public void loadItemNameDataToComboBox(ComboBox cmb, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.catSearch(keywords);
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
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void loadCustomerWithoutOtherToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.Select();
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

        public void loadCustomerAndAllToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = 0;
        }

        public void loadMaterialAndAllToComboBox(ComboBox cmb)
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
            cmb.SelectedIndex = 0;
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
                bool success = daltrfHist.Update(utrfHist);
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
            dt.Columns.Add(headerBalanceZero, typeof(int));
            dt.Columns.Add(headerBalanceOne, typeof(int));
            dt.Columns.Add(headerBalanceTwo, typeof(int));
            dt.Columns.Add(headerBalanceThree, typeof(int));
            dt.Columns.Add(headerBalanceFour, typeof(int));

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

        public DataTable insertMaterialUsedData(string customer)
        {
            DataTable dt;
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
                int forecastIndex = 1, child_join_qty, child_child_join_qty;

                string currentMonth = DateTime.Now.Month.ToString();
                string nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM");
                string nextNextMonth = DateTime.Now.AddMonths(+2).ToString("MMMM");
                string nextNextNextMonth = DateTime.Now.AddMonths(+3).ToString("MMMM");

                string year = DateTime.Now.Year.ToString();
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item["item_code"].ToString();

                    if(itemCode.Equals("V96LAR000"))
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());
                    forecast_1 = item["forecast_one"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_one"]);
                    forecast_2 = item["forecast_two"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_two"]);
                    forecast_3 = item["forecast_three"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_three"]);
                    forecast_4 = item["forecast_four"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_four"]);
                    itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());
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
                                if (Join["child_cat"].ToString().Equals("Part") || Join["child_cat"].ToString().Equals("Sub Material"))
                                {
                                    //if (!string.IsNullOrEmpty(Join["child_material"].ToString()) || Join["child_cat"].ToString().Equals("Sub Material"))
                                    
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
                                        if (!Join["child_assembly"].ToString().Equals("True") && Join["child_production"].ToString().Equals("True"))
                                        {
                                            dtMat_row = dtMat.NewRow();
                                            dtMat_row[headerIndex] = forecastIndex;
                                            dtMat_row[headerMat] = child_Mat;
                                            dtMat_row[headerCode] = Join["child_code"].ToString();
                                            dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
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
                                                        dtMat_row[headerWeight] = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
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
                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
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

            dtMat = AddDuplicates(dtMat);
            dtMat = calStillNeed(dtMat);
            return dtMat;
        }

        public DataTable insertZeroCostMaterialUsedData(string customer)
        {
            DataTable dt;
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
                DataTable dt_currentMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();

                foreach (DataRow item in dt.Rows)
                {
                    //counter++;
                    itemCode = item["item_code"].ToString();

                    if (itemCode.Equals("V88K9J100"))
                    {
                        float test = 0;
                    }

                    MB = item["item_mb"] == DBNull.Value ? "NULL" : item["item_mb"].ToString();
                    mbRate = item["item_mb_rate"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_mb_rate"].ToString());
                    readyStock = item["item_qty"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_qty"].ToString());
                    forecast_1 = item["forecast_one"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_one"]);
                    forecast_2 = item["forecast_two"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_two"]);
                    forecast_3 = item["forecast_three"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_three"]);
                    forecast_4 = item["forecast_four"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_four"]);
                    itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());
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
                        bal_1 = forecast_1 * -1 + currentMonthOut;
                    }

                    if (nextMonthOut >= forecast_2)
                    {
                        bal_2 = bal_1;
                    }
                    else
                    {
                        bal_2 = forecast_2*-1 + nextMonthOut;
                    }

                    bal_3 = forecast_3 * -1;
                    bal_4 = forecast_4 * -1;

                    #region child
                    if (ifGotChild(itemCode, dtJoin))
                    {
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
                                if (Join["child_cat"].ToString().Equals("Part") || Join["child_cat"].ToString().Equals("Sub Material"))
                                {
                                    //if (!string.IsNullOrEmpty(Join["child_material"].ToString()) || Join["child_cat"].ToString().Equals("Sub Material"))

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

                                    if (bal_1 < 0)
                                    {
                                        childBal_1 = bal_1 * child_join_qty;
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
                                            dtMat_row[headerWeight] = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
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
                                                        dtMat_row[headerWeight] = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
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
                                        dtMat_row[headerCode] = Join["child_code"].ToString();
                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join["child_code"].ToString());
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

            dtMat = AddDuplicates(dtMat);
            dtMat = calStillNeed(dtMat);
            return dtMat;
        }

        public DataTable insertSubMaterialUsedData(string customer, string SubMat)
        {
            DataTable dt;
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
                DataTable dt_currentMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = daltrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

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
                    forecast_1 = item["forecast_one"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_one"]);
                    forecast_2 = item["forecast_two"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_two"]);
                    forecast_3 = item["forecast_three"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_three"]);
                    forecast_4 = item["forecast_four"] == DBNull.Value ? 0 : Convert.ToInt32(item["forecast_four"]);
                    itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());
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
                    bal_4 = bal_3 - forecast_4;

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

        public bool ifGotChild(string itemCode, DataTable dt)
        {
            bool result = false;

            foreach (DataRow join in dt.Rows)
            {
                if(join["parent_code"].ToString().Equals(itemCode))
                {
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

        public bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        public bool ifGotParent(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.childCheck(itemCode);
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

        #endregion

        #region System

        public void saveToText(Exception ex)//error message
        {
            
            Directory.CreateDirectory(@"D:\StockAssistant\SystemError");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemError\Error_" + today + ".txt";

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
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            if (ex != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    writer.WriteLine("Action : SYSTEM ERROR");
                    writer.WriteLine();
                    writer.WriteLine("Detail : " + ex.Message);
                    writer.WriteLine();
                }
            }
        }

        public void saveToTextAndMessageToUser(Exception ex)
        {
            Directory.CreateDirectory(@"D:\StockAssistant\SystemError");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemError\Error_" + today + ".txt";

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
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            if(ex != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    writer.WriteLine("Action : SYSTEM ERROR");
                    writer.WriteLine();
                    writer.WriteLine("Detail : " + ex.Message);
                    writer.WriteLine();
                }
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
            }
        }

        #endregion
    }
}
