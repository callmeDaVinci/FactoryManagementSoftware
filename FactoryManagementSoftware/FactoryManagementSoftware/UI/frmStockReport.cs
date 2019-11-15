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
using FactoryManagementSoftware.Properties;
using System.ComponentModel;
using System.Threading;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockReport : Form
    {

        #region Variable

        private string IndexColumnName = "NO";
        private string codeColumnName = "item_code";
        private string nameColumnName = "item_name";
        private string factoryColumnName = "";
        private string totalStockColumnName = "item_qty";
        private string unitColumnName = "stock_unit";
        readonly string CMBPartHeader = "Parts";
        readonly string CMBMaterialHeader = "Materials";

        readonly string headerIndex = "#";
        readonly string headerName = "NAME";
        readonly string headerCode = "CODE";
        readonly string headerTotal = "TOTAL";
        readonly string headerUnit = "UNIT";
        readonly string headerParentColor = "PARENT COLOR";
        readonly string headerRepeat = "REPEAT";

        readonly string AssemblyColor = "BLUE";
        readonly string ProductionColor = "GREEN";
        readonly string ProductionAndAssemblyColor = "Purple";

        private int index_Public = 0;
        private float public_IndexNO = 1;
        private float public_SubIndexNO = 0;
        private float public_SecondSubIndexNO = 0;
        private string editedOldValue, editedNewValue, editedFactory, editedItem, editedTotal, editedUnit;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        DataTable dt_Stock;
        DataTable dt_PublicItemInfo;
        #endregion

        #region Class Object
        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();

        trfHistDAL dalTrfHist = new trfHistDAL();

        joinDAL dalJoin = new joinDAL();
        custDAL dalCust = new custDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        CheckChildBLL uCheckChild = new CheckChildBLL();
        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();
        #endregion

        #region UI setting

        public frmStockReport()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            InitializeComponent();
            progressBar1.Hide();
            lblUpdatedTime.Text = DateTime.Now.ToLongTimeString();
            dt_Stock = NewStockTable();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237,237,237);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ClearSelection();
        }

        private DataTable NewStockTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(float));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));

            //add factory columns
            DataTable dt_Fac = dalFac.Select();

            string facName = string.Empty;

            if (dt_Fac.Rows.Count > 0)
            {
                foreach (DataRow stock in dt_Fac.Rows)
                {
                    facName = stock["fac_name"].ToString();
                    dt.Columns.Add(facName, typeof(float));
                }
            }

            dt.Columns.Add(headerTotal, typeof(float));
            dt.Columns.Add(headerUnit, typeof(string));

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerRepeat, typeof(int));

            return dt;
        }

        private void dgvStockUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[headerParentColor].Visible = false;
            //dgv.Columns[headerRepeat].Visible = false;
        }
        #endregion

        #region load data

        private void frmStockReport_Load(object sender, EventArgs e)
        {
            loadTypeData();
            tool.DoubleBuffered(dgvNewStock, true);
            dgvNewStock.ClearSelection();
        }

        private void frmStockReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.stockReportFormOpen = false;
        }

        private void loadTypeData()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(CMBPartHeader);
            cmbType.Items.Add(CMBMaterialHeader);
        }

        private void loadMaterialToComboBox(ComboBox cmb)
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

        private bool newLoadMaterialStockData()
        {
            public_IndexNO = 1;
            public_SubIndexNO = 0;
            public_SecondSubIndexNO = 0;

            bool gotData = true;
            //DataTable dt = dalItemCust.custSearch(cmbSubType.Text);//load customer's item list

            DataTable dt = dalItem.CatSearch(cmbSubType.Text);
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();


            dt_Stock.Clear();
            //DataRow dtStock_row;

            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dt_AllStockData = dalStock.Select();
            DataTable dt_JoinInfo = dalJoin.SelectAll();
            DataTable dt_StockData;

            dt_PublicItemInfo = dt_ItemInfo;
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();

            if (dt.Rows.Count <= 0)
            {
                gotData = false;
            }
            else
            {
                //load material data
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[dalItem.ItemCode].ToString();

                    if (item["item_cat"].ToString().Equals(cmbSubType.Text))
                    {
                        float readyStock = Convert.ToSingle(item["item_qty"]);

                        dt_StockData = tool.getStockDataTableFromDataTable(dt_AllStockData, itemCode);

                        addRowtoDataTable(dt_StockData, item["item_code"].ToString(), item["item_name"].ToString(), readyStock);
                        public_IndexNO++;
                    }
                }
            }

            dt_PublicItemInfo = dalItem.Select();
            dgvNewStock.DataSource = null;
            //DataTable sortedDt = dt.DefaultView.ToTable();
            //DataTable testDt = dt_Stock.ToTable(headerCode);

            //DataView dv = new DataView(dt_Stock);

            //DataTable dtTest = dv.ToTable(true, headerCode,headerName);

            //string abc = dt.Rows[0]["column name"].ToString();

            DataTable dt_StockRemovedFlag;

            if (dt_Stock.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(dtpEndDate.Value).Date;

                if (date < DateTime.Today)
                {
                    dt_Stock = calculateOldStock(dt_Stock);
                }

                dt_StockRemovedFlag = dt_Stock.Copy();
                dt_StockRemovedFlag.Columns.Remove(headerParentColor);
                dt_StockRemovedFlag.Columns.Remove(headerRepeat);

                //dt_Stock.DefaultView.Sort = headerIndex+" ASC";
                dgvNewStock.DataSource = dt_StockRemovedFlag;
                dgvStockUIEdit(dgvNewStock);
                dgvNewStock.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;
                //colorData(dgvNewStock, dt_Stock);
                dgvNewStock.ClearSelection();
            }

            return gotData;
        }

        private void dataInsertToStockDGV(DataGridView dgv, DataRow row, int n, string indexNo)
        {
            float readyStock = Convert.ToSingle(row["item_qty"]);
            dgv.Rows[n].Cells[IndexColumnName].Value = indexNo;
            dgv.Rows[n].Cells["item_code"].Value = row["item_code"].ToString();
            dgv.Rows[n].Cells["item_name"].Value = row["item_name"].ToString();
            dgv.Rows[n].Cells["item_qty"].Value = readyStock.ToString("0.00");
           
            if(readyStock < 0)
            {
                dgv.Rows[n].Cells["item_qty"].Style.ForeColor = Color.Red;

            }

            loadStockList(row["item_code"].ToString(), n);
        }

        private void loadStockList(string itemCode,int n)
        {
            DataTable dt = dalStock.Select(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    string factoryName = stock["fac_name"].ToString();
                    string qty = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");
                    dgvNewStock.Rows[n].Cells[factoryName].Value = qty;
                    
                    if(tool.ifGotChild(itemCode))
                    {
                        dgvNewStock.Rows[n].Cells[unitColumnName].Value = "set";
                    }
                    else
                    {
                        dgvNewStock.Rows[n].Cells[unitColumnName].Value = stock["stock_unit"].ToString();
                    }
                    
                }
            }
        }

        private bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        private bool ifRepeat(string itemCode)
        {
            foreach (DataRow row in dt_Stock.Rows)
            {
                if(row[headerCode].ToString().Equals(itemCode))
                {
                    return true;
                }
            }

            return false;
        }

        private void addRowtoDataTable(DataTable dt_StockData, string itemCode, string itemName, float stockQty)
        {          
            DataRow dtStock_row;

            dtStock_row = dt_Stock.NewRow();
            dtStock_row[headerIndex] = public_IndexNO + public_SubIndexNO/10 + public_SecondSubIndexNO/100;
            dtStock_row[headerName] = itemName;
            dtStock_row[headerCode] = itemCode;
            dtStock_row[headerTotal] = stockQty;

            if (dt_StockData.Rows.Count > 0)
            {
                foreach (DataRow stock in dt_StockData.Rows)
                {
                    string factoryName = stock[tool.headerFacName].ToString();

                    float qty = Convert.ToSingle(stock[tool.headerReadyStock]);

                    dtStock_row[factoryName] = qty;
                    dtStock_row[headerUnit] = stock[tool.headerUnit].ToString();
                }
            }

            if (ifGotChild(itemCode))
            {
                DataRow dt_Row = tool.getDataRowFromDataTable(dt_PublicItemInfo, itemCode);

                int assembly = Convert.ToInt16(dt_Row[dalItem.ItemAssemblyCheck]);
                int production = Convert.ToInt16(dt_Row[dalItem.ItemProductionCheck]);

                if (assembly == 1 && production == 1)
                {
                    dtStock_row[headerParentColor] = ProductionAndAssemblyColor;
                }
                else if (assembly != 1 && production == 1)
                {
                    dtStock_row[headerParentColor] = ProductionColor;
                }
                else if (assembly == 1 && production != 1)
                {
                    dtStock_row[headerParentColor] = AssemblyColor;
                }
            }

            //check if repeat
            if(ifRepeat(itemCode))
            {
                dtStock_row[headerRepeat] = 1;
            }
            else
            {
                dtStock_row[headerRepeat] = 0;
            }
        
            dt_Stock.Rows.Add(dtStock_row);
        }

        private bool newLoadPartStockData()
        {
            public_IndexNO = 1;
            public_SubIndexNO = 0;
            public_SecondSubIndexNO = 0;

            bool gotData = true;
            DataTable dt = dalItemCust.custSearch(cmbSubType.Text);//load customer's item list
            
            dt_Stock.Clear();
            DataRow dtStock_row;

            DataTable dt_ItemInfo = dalItem.Select();
            DataTable dt_AllStockData = dalStock.Select();
            DataTable dt_JoinInfo = dalJoin.SelectAll();
            DataTable dt_StockData;
            DataTable dt_Join = dalJoin.SelectAll();

            dt_PublicItemInfo = dt_ItemInfo;
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();

            if (dt.Rows.Count <= 0)
            {
                gotData = false;
            }
            else
            {
                //load single data
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[dalItem.ItemCode].ToString();

                    int assembly = item[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(item[dalItem.ItemAssemblyCheck]);
                    int production = item[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(item[dalItem.ItemProductionCheck]);

                    if (assembly == 0 && production == 0) //!dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode)
                    {
                        float readyStock = Convert.ToSingle(item["item_qty"]);
                       
                        dt_StockData =  tool.getStockDataTableFromDataTable(dt_AllStockData, itemCode);

                        addRowtoDataTable(dt_StockData, item["item_code"].ToString(), item["item_name"].ToString(), readyStock);
                        public_IndexNO++;
                    }
                }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                #region load parent and child data

                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[dalItem.ItemCode].ToString();

                    if (tool.ifGotChild2(itemCode, dt_Join))  //ifGotChild(itemCode)
                    {
                        //add empty space
                        dtStock_row = dt_Stock.NewRow();
                        dt_Stock.Rows.Add(dtStock_row);

                        //add parent item
                        float readyStock = Convert.ToSingle(item["item_qty"]);

                        dt_StockData = tool.getStockDataTableFromDataTable(dt_AllStockData, itemCode);

                        addRowtoDataTable(dt_StockData, item["item_code"].ToString(), item["item_name"].ToString(), readyStock);

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////

                        DataTable dtJoin_Child = tool.getJoinDataTableFromDataTable(dt_JoinInfo, itemCode);
                        if (dtJoin_Child.Rows.Count > 0)
                        {
                            public_SubIndexNO = 1;
                            foreach (DataRow Join in dtJoin_Child.Rows)
                            {
                                string childCode = Join[tool.headerChildCode].ToString();

                                if (tool.getCatNameFromDataTable(dt_ItemInfo, childCode).Equals("Part") || (cbIncludeSubMat.Checked && tool.getCatNameFromDataTable(dt_ItemInfo, childCode).Equals("Sub Material")))
                                {
                                    DataRow itemInfoRow = tool.getDataRowFromDataTable(dt_ItemInfo, childCode);

                                    string childName = itemInfoRow[dalItem.ItemName].ToString();
                                    float childStockQty = Convert.ToSingle(itemInfoRow[dalItem.ItemQty]);

                                    DataTable dt_ChildStockData = tool.getStockDataTableFromDataTable(dt_AllStockData, childCode);

                                    addRowtoDataTable(dt_ChildStockData, childCode, childName, childStockQty);

                                    //check if have child
                                    if (tool.ifGotChild2(childCode, dt_Join))  //ifGotChild(childCode)
                                    {
                                        DataTable dtJoin_SubChild = tool.getJoinDataTableFromDataTable(dt_JoinInfo, childCode);

                                        if (dtJoin_SubChild.Rows.Count > 0)
                                        {
                                            public_SecondSubIndexNO = 1;
                                            foreach (DataRow Join2 in dtJoin_SubChild.Rows)
                                            {
                                                string subChildCode = Join2[tool.headerChildCode].ToString();

                                                if (tool.getCatNameFromDataTable(dt_ItemInfo, subChildCode).Equals("Part"))
                                                {
                                                    DataRow itemInfoRow2 = tool.getDataRowFromDataTable(dt_ItemInfo, subChildCode);

                                                    string subChildName = itemInfoRow2[dalItem.ItemName].ToString();
                                                    float subChildStockQty = Convert.ToSingle(itemInfoRow2[dalItem.ItemQty]);

                                                    DataTable dt_SubChildStockData = tool.getStockDataTableFromDataTable(dt_AllStockData, subChildCode);

                                                    addRowtoDataTable(dt_SubChildStockData, subChildCode, subChildName, subChildStockQty);
                                                    public_SecondSubIndexNO++;

                                                }
                                            }
                                        }

                                    }

                                    public_SubIndexNO++;
                                    public_SecondSubIndexNO = 0;
                                }
                            }
                        }

                        public_SubIndexNO = 0;
                        
                        public_IndexNO++;
                    }

                    
                }

                #endregion
            }

            //dt_PublicItemInfo = dalItem.Select();

            dgvNewStock.DataSource = null;

            DataTable dt_StockRemovedFlag;
           
            if (dt_Stock.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(dtpEndDate.Value).Date;

                if (date < DateTime.Today)
                {
                    dt_Stock = calculateOldStock(dt_Stock);
                }

                dt_StockRemovedFlag = dt_Stock.Copy();
                dt_StockRemovedFlag.Columns.Remove(headerParentColor);
                dt_StockRemovedFlag.Columns.Remove(headerRepeat);

                //dt_Stock.DefaultView.Sort = headerIndex+" ASC";
                dgvNewStock.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                dgvNewStock.DataSource = dt_StockRemovedFlag;
                dgvStockUIEdit(dgvNewStock);
                colorData(dgvNewStock,dt_Stock);
                dgvNewStock.ClearSelection();
            }

            return gotData;
        }

        private DataTable calculateOldStock(DataTable dt)
        {
            DateTime now = DateTime.Now;
            string startDate = Convert.ToDateTime(dtpEndDate.Value).AddDays(1).ToString("yyyy/MM/dd");
            string endDate = DateTime.Now.ToString("yyyy/MM/dd");

            DataTable dt_TrfHist;

            if (!cbIncludeSubMat.Checked)
            {
                if (cmbType.Text.Equals(CMBPartHeader))
                {
                    dt_TrfHist = dalTrfHist.rangePartTrfSearch(startDate, endDate);
                }
                else
                {
                    dt_TrfHist = dalTrfHist.rangeTrfSearch(startDate, endDate);
                }
            }
            else
            {
                dt_TrfHist = dalTrfHist.rangeTrfSearch(startDate, endDate);
            }

            DataTable dt_Fac = dalFac.Select();

            foreach (DataRow row in dt.Rows)
            {
                string itemCode = row[headerCode].ToString();

                foreach (DataRow row2 in dt_TrfHist.Rows)
                {
                    if(row2[dalTrfHist.TrfItemCode].ToString() == itemCode)
                    {
                        string from = row2[dalTrfHist.TrfFrom].ToString();
                        string to = row2[dalTrfHist.TrfTo].ToString();

                        if(tool.IfFactoryExists(dt_Fac,from) && tool.IfFactoryExists(dt_Fac,to))
                        {
                            //from factory to factory
                            //from ++
                            //to --

                            if(row2[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                            {
                                float trfQty = row2[dalTrfHist.TrfQty] == null ? 0 : Convert.ToSingle(row2[dalTrfHist.TrfQty].ToString());
                                float oldQty = row[from] == null? 0 : Convert.ToSingle(row[from].ToString());
                                float newQty = oldQty + trfQty;

                                row[from] = newQty;

                                oldQty = row[to] == null ? 0 : Convert.ToSingle(row[to].ToString());
                                newQty = oldQty - trfQty;

                                row[to] = newQty;
                            }
                            
                            if(row[headerRepeat] != null)
                            {
                                int i = Convert.ToInt16(row[headerRepeat]);

                                if(i == 1)
                                {
                                    //row2.Delete();
                                }
                            }

                        }
                        else if (tool.IfFactoryExists(dt_Fac,from) && !tool.IfFactoryExists(dt_Fac,to))
                        {
                            //from factory to non factory
                            //from ++

                            if (row2[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                            {
                                float trfQty = row2[dalTrfHist.TrfQty] == null ? 0 : Convert.ToSingle(row2[dalTrfHist.TrfQty].ToString());
                                float oldQty = row[from] == null ? 0 : Convert.ToSingle(row[from].ToString());
                                float newQty = oldQty + trfQty;

                                row[from] = newQty;

                                float oldTotal = row[headerTotal] == null ? 0 : Convert.ToSingle(row[headerTotal].ToString());
                                row[headerTotal] = oldTotal + trfQty;

                            }

                            if (row[headerRepeat] != null)
                            {
                                int i = Convert.ToInt16(row[headerRepeat]);

                                if (i == 1)
                                {
                                    //row2.Delete();
                                }
                            }
                        }
                        else if (!tool.IfFactoryExists(dt_Fac,from) && tool.IfFactoryExists(dt_Fac,to))
                        {
                            //from non factory to factory
                            //to --

                            if (row2[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                            {
                                float trfQty = row2[dalTrfHist.TrfQty] == null ? 0 : Convert.ToSingle(row2[dalTrfHist.TrfQty].ToString());
                                float oldQty = row[to] == null ? 0 : Convert.ToSingle(row[to].ToString());
                                float newQty = oldQty - trfQty;

                                row[to] = newQty;

                                float oldTotal = row[headerTotal] == null ? 0 : Convert.ToSingle(row[headerTotal].ToString());
                                row[headerTotal] = oldTotal - trfQty;
                            }

                            if (row[headerRepeat] != null)
                            {
                                int i = Convert.ToInt16(row[headerRepeat]);

                                if (i == 1)
                                {
                                    //row2.Delete();
                                }
                            }
                        }
                    }
                }
                //dt_TrfHist.AcceptChanges();
            }

            return dt;
        }

        private void colorData(DataGridView dgv,DataTable dt)
        {
            int rowIndex = 0;
            string color = string.Empty;
 
            foreach(DataRow row in dt.Rows)
            {
                if(row[headerParentColor] != DBNull.Value)
                {
                    color = row[headerParentColor].ToString();

                    if (color.Equals(ProductionAndAssemblyColor))
                    {
                        dgv.Rows[rowIndex].Cells[headerCode].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[rowIndex].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                    }
                    else if (color.Equals(ProductionColor))
                    {
                        dgv.Rows[rowIndex].Cells[headerCode].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[rowIndex].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                    }
                    else if (color.Equals(AssemblyColor))
                    {
                        dgv.Rows[rowIndex].Cells[headerCode].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[rowIndex].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                    }
                    
                }

                if (row[headerRepeat] != DBNull.Value)
                {
                    int repeat = Convert.ToInt16(row[headerRepeat]);

                    if (repeat == 1)
                    {
                        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Lavender;
                    }

                }

                rowIndex ++;
            }
        }

        private void loadChildParStocktData(string itemCode, string no)
        {
            int index = 1;
            string childIndex = no+"."+index.ToString();

            DataGridView dgv = dgvNewStock;
            string parentItemCode = itemCode;
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    if(dalItem.getCatName(Join["join_child_code"].ToString()).Equals("Part"))
                    {
                        int n = dgv.Rows.Add();

                        dgv.Rows[n].Cells["item_code"].Value = Join["join_child_code"].ToString();

                        string childItemCode = Join["join_child_code"].ToString();

                        if (ifGotChild(childItemCode))
                        {
                            if(dalItem.checkIfAssembly(childItemCode) && dalItem.checkIfProduction(childItemCode))
                            {
                                dgv.Rows[n].Cells[codeColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[nameColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }
                            else if (dalItem.checkIfAssembly(childItemCode) && !dalItem.checkIfProduction(childItemCode))
                            {
                                dgv.Rows[n].Cells[codeColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[nameColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }
                            else if (!dalItem.checkIfAssembly(childItemCode) && dalItem.checkIfProduction(childItemCode))
                            {
                                dgv.Rows[n].Cells[codeColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[nameColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }

                            loadChildParStocktData(Join["join_child_code"].ToString(), childIndex);
                        }

                        DataTable dtItem = dalItem.codeSearch(Join["join_child_code"].ToString());

                        ifRepeat(Join["join_child_code"].ToString(), n);

                        if (dtItem.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtItem.Rows)
                            {
                                dataInsertToStockDGV(dgvNewStock, item, n, childIndex);
                            }
                            //alphbet++;
                            index++;
                            childIndex = no + "." + index.ToString();
                        }
                    }
                }
            }
        }

        #endregion

        #region click/index changed

        private void frmStockReport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            dgvNewStock.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                dgvNewStock.DataSource = null;
                //dgvNewStock.Rows.Clear();
                //dgvNewStock.Refresh();

                if (cmbType.Text.Equals(CMBPartHeader))
                {
                    tool.loadCustomerToComboBox(cmbSubType);
                    cbIncludeSubMat.Checked = false;
                    cbIncludeSubMat.Visible = true;
                    errorProvider1.Clear();
                }
                else if (cmbType.Text.Equals(CMBMaterialHeader))
                {
                    loadMaterialToComboBox(cmbSubType);
                    cbIncludeSubMat.Checked = false;
                    cbIncludeSubMat.Visible = false;
                    errorProvider1.Clear();
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }         
        }

        private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNewStock.DataSource = null;

            if(cmbSubType.Text != null)
            {
                errorProvider2.Clear();
            }
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "StockReport(" + cmbSubType.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //NOTE : DONT play with the UI thread here...
            // Do Whatever work you are doing and for which you need to show    progress bar
            //CopyLotsOfFiles() // This is the function which is being run in the background
            e.Result = true;// Tell that you are done
        }

        void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Access Main UI Thread here
            progressBar1.Value = e.ProgressPercentage;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            bgWorker.DoWork += BackgroundWorkerDoWork;
            bgWorker.ProgressChanged += BackgroundWorkerProgressChanged;

            if(dgvNewStock.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else
            {
                try
                {

                    dgvNewStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    SaveFileDialog sfd = new SaveFileDialog();

                    string path = @"D:\StockAssistant\Document\StockReport";
                    Directory.CreateDirectory(path);
                    sfd.InitialDirectory = path;

                    sfd.Filter = "Excel Documents (*.xls)|*.xls";
                    sfd.FileName = setFileName();

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        progressBar1.Show();
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
                        xlWorkSheet.Name = cmbSubType.Text;

                        #region Save data to Sheet

                        string subMaterialIncluded = "";

                        if (cbIncludeSubMat.Checked)
                        {
                            subMaterialIncluded = "_Sub Material Included";
                        }

                        DateTime date = dtpEndDate.Value.Date;
                        string dataDate = date.ToString("dd/MM/yyyy");

                        if (date == DateTime.Today)
                        {
                            dataDate += " " + lblUpdatedTime.Text;
                        }
                        //Header and Footer setup
                        xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + cmbSubType.Text + subMaterialIncluded + "_" + dataDate;
                        xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 STOCK LIST";
                        xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                        xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " On " + DateTime.Now;

                        //Page setup
                        xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                        xlWorkSheet.PageSetup.Zoom = false;
                        xlWorkSheet.PageSetup.CenterHorizontally = true;
                        xlWorkSheet.PageSetup.LeftMargin = 30;
                        xlWorkSheet.PageSetup.RightMargin = 30;
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
                        if (true)//cmbSubType.Text.Equals("PMMA")
                        {
                            for (int i = 0; i <= dgvNewStock.RowCount - 2; i++)
                            {
                                for (int j = 0; j <= dgvNewStock.ColumnCount - 1; j++)
                                {
                                    Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

                                    if (i == 0)
                                    {
                                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                        header.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor);

                                        header = (Range)xlWorkSheet.Cells[1, 10];
                                        header.Font.Color = Color.Blue;

                                        header = (Range)xlWorkSheet.Cells[1, 14];
                                        header.Font.Color = Color.Red;

                                        header = (Range)xlWorkSheet.Cells[1, 16];
                                        header.Font.Color = Color.Red;


                                    }

                                    if (dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                                    {
                                        range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                        if (i == 0)
                                        {
                                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                        }
                                    }
                                    else if (dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
                                    {
                                        range.Rows.RowHeight = 3;
                                        range.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor);
                                        if (i == 0)
                                        {
                                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor);
                                        }
                                    }
                                    else
                                    {
                                        range.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor);

                                        if (i == 0)
                                        {
                                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[i].Cells[j].InheritedStyle.BackColor);
                                        }
                                    }
                                    range.Font.Color = dgvNewStock.Rows[i].Cells[j].Style.ForeColor;
                                    if (dgvNewStock.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
                                    {
                                        Range header = (Range)xlWorkSheet.Cells[i + 2, 2];
                                        header.Font.Underline = true;

                                        header = (Range)xlWorkSheet.Cells[i + 2, 3];
                                        header.Font.Underline = true;
                                    }

                                }

                                Int32 percentage = ((i + 1) * 100) / (dgvNewStock.RowCount - 2);
                                if (percentage >= 100)
                                {
                                    percentage = 100;
                                }
                                bgWorker.ReportProgress(percentage);
                            }
                        }


                        bgWorker.ReportProgress(100);
                        System.Threading.Thread.Sleep(1000);

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
                        dgvNewStock.ClearSelection();

                        // Open the newly saved excel file
                        if (File.Exists(sfd.FileName))
                            System.Diagnostics.Process.Start(sfd.FileName);
                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type
                    dgvNewStock.SelectionMode = DataGridViewSelectionMode.CellSelect;
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    progressBar1.Visible = false;
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
            }
           
        }

        private void copyAlltoClipboard()
        {
            //dgvNewStock.Columns.Remove(headerParentColor);
            //dgvNewStock.Columns.Remove(headerRepeat);
            dgvNewStock.SelectAll();
            //dgvNewStock.sele
            DataObject dataObj = dgvNewStock.GetClipboardContent();
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

        private void btnExportAllToExcel_Click(object sender, EventArgs e)
        {
            bgWorker.DoWork += BackgroundWorkerDoWork;
            bgWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            try
            {
                dgvNewStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\StockReport";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "StockReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Show();
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
                    dgvNewStock.ClearSelection();
                }

                bgWorker.ReportProgress(100);
                System.Threading.Thread.Sleep(1000);
                dgvNewStock.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                progressBar1.Visible = false;
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


                for (int i = 0; i <= cmbType.Items.Count - 1; i++)
                {
                    cmbType.SelectedIndex = i;
                    for (int j = 0; j <= cmbSubType.Items.Count - 1; j++)
                    {
                        cmbSubType.SelectedIndex = j;
                        if (cmbType.Text.Equals(CMBPartHeader))
                        {
                            gotData = newLoadPartStockData();//if data != empty return true, else false
                        }
                        else if (cmbType.Text.Equals(CMBMaterialHeader))
                        {
                            gotData = newLoadMaterialStockData();//if data != empty return true, else false
                        }

                        if (gotData)//if datagridview have data
                        {
                            Worksheet addedSheet = null;

                            int count = g_Workbook.Worksheets.Count;

                            addedSheet = g_Workbook.Worksheets.Add(Type.Missing,
                                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                            addedSheet.Name = cmbSubType.Text;

                            addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                            addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") STOCK LIST";
                            addedSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                            addedSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                            //Page setup
                            addedSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                            addedSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                            addedSheet.PageSetup.Zoom = false;
                            addedSheet.PageSetup.CenterHorizontally = true;
                            addedSheet.PageSetup.LeftMargin = 1;
                            addedSheet.PageSetup.RightMargin = 1;
                            addedSheet.PageSetup.FitToPagesWide = 1;
                            addedSheet.PageSetup.FitToPagesTall = false;
                            addedSheet.PageSetup.PrintTitleRows = "$1:$1";

                            copyAlltoClipboard();
                            addedSheet.Select();
                            Range CR = (Range)addedSheet.Cells[1, 1];
                            CR.Select();
                            addedSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                            Range tRange = addedSheet.UsedRange;
                            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                            tRange.Borders.Weight = XlBorderWeight.xlThin;
                            tRange.Font.Size = 11;
                            tRange.Font.Name = "Calibri";
                            tRange.EntireRow.AutoFit();
                            tRange.EntireColumn.AutoFit();
                            tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                            if (true)//cmbSubType.Text.Equals("PMMA")
                            {
                                for (int x = 0; x <= dgvNewStock.RowCount - 2; x++)
                                {
                                    for (int y = 0; y <= dgvNewStock.ColumnCount - 1; y++)
                                    {
                                        Range range = (Range)addedSheet.Cells[x + 2, y + 1];

                                        if (x == 0)
                                        {
                                            Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor);

                                            header = (Range)addedSheet.Cells[1, 10];
                                            header.Font.Color = Color.Blue;

                                            header = (Range)addedSheet.Cells[1, 14];
                                            header.Font.Color = Color.Red;

                                            header = (Range)addedSheet.Cells[1, 16];
                                            header.Font.Color = Color.Red;


                                        }

                                        if (dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor == SystemColors.Window)
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                            }
                                        }
                                        else if (dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor == Color.Black)
                                        {
                                            range.Rows.RowHeight = 3;
                                            range.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            }
                                        }
                                        else
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor);

                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(dgvNewStock.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            }
                                        }
                                        range.Font.Color = dgvNewStock.Rows[x].Cells[y].Style.ForeColor;
                                        if (dgvNewStock.Rows[x].Cells[y].Style.ForeColor == Color.Blue)
                                        {
                                            Range header = (Range)addedSheet.Cells[x + 2, 2];
                                            header.Font.Underline = true;

                                            header = (Range)addedSheet.Cells[x + 2, 3];
                                            header.Font.Underline = true;
                                        }

                                    }

                                    Int32 percentage = ((x + 1) * 100) / (dgvNewStock.RowCount - 2);
                                    if (percentage >= 100)
                                    {
                                        percentage = 100;
                                    }
                                    bgWorker.ReportProgress(percentage);
                                }
                            }
                            
                            releaseObject(addedSheet);
                            Clipboard.Clear();
                            dgvNewStock.ClearSelection();
                        }
                    }
                }
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

        #endregion

        #region test

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbType.Text))
            {
                result = false;
                errorProvider1.SetError(lblType, "Item Type Required");
            }

            if (string.IsNullOrEmpty(cmbSubType.Text))
            {
                result = false;
                errorProvider2.SetError(lblSubType, "Item Sub Type Required");
            }

            return result;

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    frmLoading.ShowLoadingScreen();
                    lblUpdatedTime.Text = DateTime.Now.ToLongTimeString();
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                    if (cmbType.Text.Equals(CMBPartHeader) && !string.IsNullOrEmpty(cmbSubType.Text))
                    {
                        newLoadPartStockData();                     
                    }
                    else if (cmbType.Text.Equals(CMBMaterialHeader) && !string.IsNullOrEmpty(cmbSubType.Text))
                    {
                        newLoadMaterialStockData();
                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
               
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }

            frmLoading.CloseForm();
        }

        public void StartForm()
        {
            System.Windows.Forms.Application.Run(new frmLoading());
        }

        private void dgvNewStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvNewStock;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerCode)
            {
                //if (dgv.Rows[row].Cells[col].Value != DBNull.Value && dgv.Rows[row].Cells[headerParentColor].Value != DBNull.Value)
                //{
                //    string color = dgv.Rows[row].Cells[headerParentColor].Value.ToString();

                //    if(dgv.Rows[row].Cells[headerRepeat].Value != DBNull.Value)
                //    {
                //        int repeat = Convert.ToInt16(dgv.Rows[row].Cells[headerRepeat].Value);

                //        if (repeat == 1)
                //        {
                //            dgv.Rows[row].DefaultCellStyle.BackColor = Color.Lavender;
                //        }
                //    }
                    

                //    if (color.Equals(ProductionAndAssemblyColor))
                //    {
                //        dgv.Rows[row].Cells[col].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                //        dgv.Rows[row].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                //    }
                //    else if (color.Equals(ProductionColor))
                //    {
                //        dgv.Rows[row].Cells[col].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                //        dgv.Rows[row].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                //    }
                //    else if (color.Equals(AssemblyColor))
                //    {
                //        dgv.Rows[row].Cells[col].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                //        dgv.Rows[row].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                //    }
                //}

               
            }

            else if (dgv.Columns[col].Name != headerIndex && dgv.Columns[col].Name != headerName && dgv.Columns[col].Name != headerUnit)
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

      
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dgvNewStock.DataSource = null;
            DateTime date = Convert.ToDateTime(dtpEndDate.Value).Date;

            if(date != DateTime.Today)
            {
                lblInfo.Visible = false;
                lblUpdatedTime.Visible = false;
            }
            else
            {
                lblInfo.Visible = true;
                lblUpdatedTime.Visible = true;
                lblUpdatedTime.Text = DateTime.Now.ToLongTimeString();
            }
        }

        private void cbIncludeSubMat_CheckedChanged(object sender, EventArgs e)
        {
            dgvNewStock.DataSource = null;
        }

        private void dgvNewStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            string itemCode = dgvNewStock.Rows[rowIndex].Cells[headerCode].Value == DBNull.Value? null: dgvNewStock.Rows[rowIndex].Cells[headerCode].Value.ToString();

            if(itemCode != null)
            {
                DateTime now = DateTime.Now;
                string startDate = Convert.ToDateTime(dtpEndDate.Value).AddDays(1).ToString("yyyy/MM/dd");
                string endDate = DateTime.Now.ToString("yyyy/MM/dd");

                DataTable dt_TrfHist = dalTrfHist.rangeTrfSearch(itemCode, startDate, endDate);

                
                frmStockReverseDetail frm = new frmStockReverseDetail(dt_TrfHist,itemCode, dalItem.getStockQty(itemCode), startDate,endDate);
                frm.Show();
            }

           
        }

        private void ifRepeat(string itemCode, int index)
        {
            DataGridView dgv = dgvNewStock;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[codeColumnName].Value != null)
                {
                    if (row.Cells[codeColumnName].Value.ToString().Equals(itemCode) && row.Index < index)
                    {
                        dgv.Rows[index].DefaultCellStyle.BackColor = Color.Lavender;
                        return;
                    }
                }
            }
        }

        #endregion
    }
}
