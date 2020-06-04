using System;
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
        readonly string headerOut = "OUT";
        readonly string headerOutStd = "OUTSTD";
        string headerBal1 = "BAL";
        string headerBal2 = "BAL";

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

        readonly string typeSingle = "SINGLE";
        readonly string typeParent = "PARENT";
        readonly string typeChild = "CHILD";

        readonly string forecastType_Forecast = "FORECAST";
        readonly string forecastType_Needed = "NEEDED";


        readonly string balType_Total = "TOTAL";
        DataTable dt_Join = new DataTable();
        DataTable dt_Item = new DataTable();

        DataTable dt_OrginalData = new DataTable();
        #endregion

        #region UI design

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

        public void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        public bool IfDONoExist(string doNO)
        {
            SPPDataDAL dalSPP = new SPPDataDAL();
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

        public int GetNewDONo()
        {
            int DoNo = 1;
            SPPDataDAL dalSPP = new SPPDataDAL();

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

        public void loadFactory(ComboBox cmb)
        {
            DataTable dt = dalFac.Select();
            DataTable lacationTable = dt.DefaultView.ToTable(true, "fac_name");

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = "fac_name";
        }

        public void loadFactoryAndAll(ComboBox cmb)
        {
            DataTable dt = dalFac.Select();
            DataTable lacationTable = dt.DefaultView.ToTable(true, "fac_name");
            lacationTable.Rows.Add("All");
            lacationTable.DefaultView.Sort = "fac_name ASC";

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = "fac_name";
            cmb.SelectedIndex = 0;

        }

        public void loadFactoryAndAllExceptStore(ComboBox cmb)
        {
            DataTable dt = dalFac.Select();
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

        public void loadMacIDByFactoryToComboBox(ComboBox cmb, string facName)
        {
            DataTable dt = dalMac.SelectByFactory(facName);
            DataTable distinctTable = dt.DefaultView.ToTable(true, "mac_id");
            distinctTable.DefaultView.Sort = "mac_id ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "mac_id";
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
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = 0;
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

            DataTable dt = dalCust.Select();
           
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

            SPPDataDAL dalSPP = new SPPDataDAL();

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
                    return float.TryParse(row[dalItemForecast.ForecastQty].ToString(), out float i) ? Convert.ToSingle(row[dalItemForecast.ForecastQty].ToString()) : -1;
                }
            }

            return forecast;
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
            if (matPlan_data.Rows.Count > 0)
            {
                foreach (DataRow row in matPlan_data.Rows)
                {
                    string mat_code = row[dalMatPlan.MatCode].ToString();
                    bool active = Convert.ToBoolean(row[dalMatPlan.Active]);

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
                        float stockQty = 0;

                        if(!(row["stock_qty"] is null))
                        {
                            stockQty = Convert.ToSingle(row["stock_qty"]);
                        }
                        
                        dtStock_row = dt_Stock.NewRow();
                        dtStock_row[headerIndex] = index;
                        dtStock_row[headerName] = row["item_name"].ToString();
                        dtStock_row[headerCode] = row["item_code"].ToString();
                        dtStock_row[headerFacName] = row["fac_name"].ToString();
                        dtStock_row[headerReadyStock] = stockQty;
                        dtStock_row[headerUnit] = row["stock_unit"].ToString();

                        dt_Stock.Rows.Add(dtStock_row);
                        index++;
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

        public DataTable insertMaterialUsedData(string customer)
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
                    itemCode = item["item_code"].ToString();

                    if(itemCode.Equals("V02K81000"))
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
                                            dtMat_row[headerWeight] = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
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
                                                        dtMat_row[headerWeight] = Convert.ToSingle(Join2["child_part_weight"].ToString()) + Convert.ToSingle(Join2["child_runner_weight"].ToString());
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
                                        dtMat_row[headerWeight] = Convert.ToSingle(Join["child_part_weight"].ToString()) + Convert.ToSingle(Join["child_runner_weight"].ToString());
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
                            dtMat_row[headerWeight] = itemPartWeight + itemRunnerWeight;
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
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
                DataTable dt_nextNextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextNextMonth, year);

                DataTable dtJoin = dalJoin.SelectwithChildInfo();
                #endregion

                foreach (DataRow item in dt.Rows)
                {
                    itemCode = item["item_code"].ToString();

                    if(itemCode == "V84KUU0V0")
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

                    #region calculate still need how many qty
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
                                if (childCode == "V95KUS000")
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

                                                        dtMat_row = dtMat.NewRow();
                                                        dtMat_row[headerIndex] = forecastIndex;
                                                        dtMat_row[headerMat] = child_child_Mat;
                                                        dtMat_row[headerCode] = Join2["child_code"].ToString();
                                                        dtMat_row[headerName] = getItemNameFromDataTable(dt_ItemInfo, Join2["child_code"].ToString());
                                                        dtMat_row[headerMB] = child_child_MB;
                                                        dtMat_row[headerMBRate] = child_child_mbRate;
                                                        dtMat_row[headerWeight] = Convert.ToSingle(Join2["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join2["child_quo_runner_weight"].ToString());
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
                                        dtMat_row[headerWeight] = Convert.ToSingle(Join["child_quo_part_weight"].ToString()) + Convert.ToSingle(Join["child_quo_runner_weight"].ToString());
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
            //dtMat = calStillNeed(dtMat);
            return dtMat;
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
                //MessageBox.Show(nextMonth);
                DataTable dt_currentMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(currentMonth, year);
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
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
                DataTable dt_nextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextMonth, year);
                DataTable dt_nextNextMonthTrfOutHist = dalTrfHist.rangeToAllCustomerSearchByMonth(nextNextMonth, year);
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
                    itemPartWeight = item["item_part_weight"] == DBNull.Value ? 0 : Convert.ToSingle(item["item_part_weight"].ToString());
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
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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

        public bool ifGotChild2(string itemCode, DataTable dt)
        {
            bool result = false;

            foreach (DataRow join in dt.Rows)
            {
                if (join[dalJoin.JoinParent].ToString().Equals(itemCode))
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
            Text text = new Text();
            DataTable dtJoin = dalJoin.loadChildList(itemCode);

            if (dtJoin.Rows.Count > 0)
            {
                foreach(DataRow row in dtJoin.Rows)
                {
                    string childCat = getItemCat(row[dalJoin.JoinChild].ToString());
                    if (childCat == text.Cat_Part || childCat == text.Cat_SubMat)
                    {
                        return true;
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
