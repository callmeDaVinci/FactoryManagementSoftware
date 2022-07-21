using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.ComponentModel;
using System.Threading;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using Excel = Microsoft.Office.Interop.Excel;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemReport : BaseForm
    {

        #region Variable
        private string path = null;
        private string excelName = null;
        private int rowCount = 0;

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
        readonly string headerQuoTon = "QUO TON";
        readonly string headerProTon = "PRO TON";
        readonly string headerCavity = "CAVITY";
        readonly string headerCycleTime = "CYCLE TIME(SEC)";
        readonly string headerPWPerShot = "PW PER SHOT(G)";
        readonly string headerRWPerShot = "RW PER SHOT(G)";

        //readonly string headerTotal = "TOTAL";
        //readonly string headerUnit = "UNIT";
        readonly string headerParentColor = "PARENT COLOR";
        readonly string headerRepeat = "REPEAT";

        readonly string AssemblyColor = "BLUE";
        readonly string ProductionColor = "GREEN";
        readonly string ProductionAndAssemblyColor = "Purple";

        private int index_Public = 0;
        private float public_IndexNO = 1;
        private float public_SubIndexNO = 0;
        private float public_SecondSubIndexNO = 0;

        DataTable dt_Item;
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

        materialBLL uMaterial = new materialBLL();
        materialDAL dalMaterial = new materialDAL();

        itemBLL uItem = new itemBLL();

        Tool tool = new Tool();
        Text text = new Text();
        #endregion

        #region UI setting

        public frmItemReport()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            InitializeComponent();

            progressBar1.Hide();
            dt_Item = NewItemTable();

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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ClearSelection();
        }

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(float));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));

            dt.Columns.Add(headerQuoTon, typeof(int));
            dt.Columns.Add(headerProTon, typeof(int));
            dt.Columns.Add(headerCavity, typeof(int));
            dt.Columns.Add(headerCycleTime, typeof(int));
            dt.Columns.Add(headerPWPerShot, typeof(float));
            dt.Columns.Add(headerRWPerShot, typeof(float));
            ////add factory columns
            //DataTable dt_Fac = dalFac.Select();

            //string facName = string.Empty;

            //if (dt_Fac.Rows.Count > 0)
            //{
            //    foreach (DataRow stock in dt_Fac.Rows)
            //    {
            //        facName = stock["fac_name"].ToString();
            //        dt.Columns.Add(facName, typeof(float));
            //    }
            //}

            //dt.Columns.Add(headerTotal, typeof(float));
            //dt.Columns.Add(headerUnit, typeof(string));

            dt.Columns.Add(headerParentColor, typeof(string));
            dt.Columns.Add(headerRepeat, typeof(int));

            return dt;
        }

        private void dgvItemUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[headerParentColor].Visible = false;
            //dgv.Columns[headerRepeat].Visible = false;
        }

        #endregion

        #region load data

        private void frmItemReport_Load(object sender, EventArgs e)
        {
            loadTypeData();
            tool.DoubleBuffered(dgvItemList, true);
            dgvItemList.ClearSelection();
        }

        private void frmItemReport_FormClosed(object sender, FormClosedEventArgs e)
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

        private bool loadMaterialStockData()
        {
            bool gotData = true;
            index_Public = 1;
            DataTable dt;
            dt = dalItem.CatSearch(cmbSubType.Text);
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();

            dgvItemList.Rows.Clear();
            dgvItemList.Refresh();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["item_cat"].ToString().Equals(cmbSubType.Text))//show data under choosen category
                    {
                        int n = dgvItemList.Rows.Add();
                        dataInsertToStockDGV(dgvItemList, item, n, index_Public.ToString());
                        index_Public++;
                    }
                }
            }
            else
            {
                gotData = false;
            }

            //listPaint(dgvNewStock);
            tool.listPaint(dgvItemList);
            return gotData;
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


            dt_Item.Clear();
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
            dgvItemList.DataSource = null;
            //DataTable sortedDt = dt.DefaultView.ToTable();
            //DataTable testDt = dt_Stock.ToTable(headerCode);

            //DataView dv = new DataView(dt_Stock);

            //DataTable dtTest = dv.ToTable(true, headerCode,headerName);

            //string abc = dt.Rows[0]["column name"].ToString();

            DataTable dt_StockRemovedFlag;

            if (dt_Item.Rows.Count > 0)
            {
                dt_StockRemovedFlag = dt_Item.Copy();
                dt_StockRemovedFlag.Columns.Remove(headerParentColor);
                dt_StockRemovedFlag.Columns.Remove(headerRepeat);

                //dt_Stock.DefaultView.Sort = headerIndex+" ASC";
                dgvItemList.DataSource = dt_StockRemovedFlag;
                dgvItemUIEdit(dgvItemList);
                dgvItemList.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;
                //colorData(dgvNewStock, dt_Stock);
                dgvItemList.ClearSelection();
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

            if (readyStock < 0)
            {
                dgv.Rows[n].Cells["item_qty"].Style.ForeColor = Color.Red;

            }

            loadStockList(row["item_code"].ToString(), n);
        }

        private void loadStockList(string itemCode, int n)
        {
            DataTable dt = dalStock.Select(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    string factoryName = stock["fac_name"].ToString();
                    string qty = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");
                    dgvItemList.Rows[n].Cells[factoryName].Value = qty;

                    if (tool.ifGotChild(itemCode))
                    {
                        dgvItemList.Rows[n].Cells[unitColumnName].Value = "set";
                    }
                    else
                    {
                        dgvItemList.Rows[n].Cells[unitColumnName].Value = stock["stock_unit"].ToString();
                    }

                }
            }
        }

        private void dgvStockReport_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            string factoryName = "";

            DataTable dt = dalFac.SelectDESC();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    factoryName = stock["fac_name"].ToString();
                    e.Row.Cells[factoryName].Value = "0";
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

        private bool loadPartStockData()
        {
            bool gotData = true;

            DataGridView dgv = dgvItemList;
            DataTable dt = dalItemCust.custSearch(cmbSubType.Text);//load customer's item list

            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();
            dgv.Rows.Clear();
            dgv.Refresh();
            if (dt.Rows.Count <= 0)
            {
                //MessageBox.Show("no data under this record.");
                gotData = false;
            }
            else
            {
                //load single data
                index_Public = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[codeColumnName].ToString();

                    if (!dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))//!ifGotChild(itemCode)
                    {
                        int n = dgv.Rows.Add();

                        dataInsertToStockDGV(dgvItemList, item, n, index_Public.ToString());
                        index_Public++;
                    }
                }

                //load parent and child data
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[codeColumnName].ToString();

                    if (ifGotChild(itemCode))
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].DefaultCellStyle.BackColor = Color.Black;
                        dgv.Rows[n].Height = 3;

                        n = dgv.Rows.Add();

                        if (dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                        }
                        else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                        }
                        else if (dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        }

                        dataInsertToStockDGV(dgvItemList, item, n, index_Public.ToString());
                        loadChildParStocktData(itemCode, index_Public.ToString());
                        index_Public++;

                    }
                    //alphbet = 65;
                }
            }

            listPaint(dgvItemList);
            dgv.ClearSelection();

            return gotData;
        }

        private bool ifRepeat(string itemCode)
        {
            foreach (DataRow row in dt_Item.Rows)
            {
                if (row[headerCode].ToString().Equals(itemCode))
                {
                    return true;
                }
            }

            return false;
        }

        private void addRowtoDataTable(DataTable dt_StockData, string itemCode, string itemName, float stockQty)
        {
            DataRow dtStock_row;

            dtStock_row = dt_Item.NewRow();
            dtStock_row[headerIndex] = public_IndexNO + public_SubIndexNO / 10 + public_SecondSubIndexNO / 100;
            dtStock_row[headerName] = itemName;
            dtStock_row[headerCode] = itemCode;
            //dtStock_row[headerTotal] = stockQty;

            if (dt_StockData.Rows.Count > 0)
            {
                foreach (DataRow stock in dt_StockData.Rows)
                {
                    string factoryName = stock[tool.headerFacName].ToString();

                    float qty = Convert.ToSingle(stock[tool.headerReadyStock]);

                    dtStock_row[factoryName] = qty;
                    //dtStock_row[headerUnit] = stock[tool.headerUnit].ToString();
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
            if (ifRepeat(itemCode))
            {
                dtStock_row[headerRepeat] = 1;
            }
            else
            {
                dtStock_row[headerRepeat] = 0;
            }

            dt_Item.Rows.Add(dtStock_row);
        }

        private void addRowtoDataTable(DataRow item)
        {
            DataRow dtStock_row;
            string itemCode = item[dalItem.ItemCode].ToString();
            dtStock_row = dt_Item.NewRow();
            dtStock_row[headerIndex] = public_IndexNO + public_SubIndexNO / 10 + public_SecondSubIndexNO / 100;
            dtStock_row[headerName] = item[dalItem.ItemName].ToString();
            dtStock_row[headerCode] = itemCode;

            dtStock_row[headerQuoTon] = item[dalItem.ItemQuoTon] == DBNull.Value? 0 : Convert.ToInt32(item[dalItem.ItemQuoTon]);
            dtStock_row[headerProTon] = item[dalItem.ItemProTon] == DBNull.Value ? 0 : Convert.ToInt32(item[dalItem.ItemProTon]);
            dtStock_row[headerCavity] = item[dalItem.ItemCavity] == DBNull.Value ? 0 : Convert.ToInt32(item[dalItem.ItemCavity]);

            int ct = item[dalItem.ItemProCTTo] == DBNull.Value? item[dalItem.ItemProCTFrom] == DBNull.Value ? 
                                                    item[dalItem.ItemQuoCT] == DBNull.Value ? 
                                                    0 : Convert.ToInt32(item[dalItem.ItemQuoCT]) : 
                                                    Convert.ToInt32(item[dalItem.ItemProCTFrom]) :Convert.ToInt32(item[dalItem.ItemProCTTo]);

           
            dtStock_row[headerCycleTime] = ct;
            dtStock_row[headerPWPerShot] = item[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToInt32(item[dalItem.ItemProPWShot]);
            dtStock_row[headerRWPerShot] = item[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToInt32(item[dalItem.ItemProRWShot]);

            //dtStock_row[headerTotal] = Convert.ToSingle(item[dalItem.ItemQty]);

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
            if (ifRepeat(itemCode))
            {
                dtStock_row[headerRepeat] = 1;
            }
            else
            {
                dtStock_row[headerRepeat] = 0;
            }

            dt_Item.Rows.Add(dtStock_row);
        }

        private bool newLoadPartStockData()
        {
            public_IndexNO = 1;
            public_SubIndexNO = 0;
            public_SecondSubIndexNO = 0;

            bool gotData = true;
            DataTable dt = dalItemCust.custSearch(cmbSubType.Text);//load customer's item list

            dt_Item.Clear();
            DataRow dtStock_row;

            DataTable dt_ItemInfo = dalItem.Select();
            //DataTable dt_AllStockData = dalStock.Select();
            DataTable dt_JoinInfo = dalJoin.SelectAll();
            //DataTable dt_StockData;

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

                    if (!dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))
                    {
                        //float readyStock = Convert.ToSingle(item["item_qty"]);

                        //dt_StockData = tool.getStockDataTableFromDataTable(dt_AllStockData, itemCode);

                        //addRowtoDataTable(dt_StockData, item["item_code"].ToString(), item["item_name"].ToString(), readyStock);
                        addRowtoDataTable(item);
                        public_IndexNO++;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                #region load parent and child data

                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[dalItem.ItemCode].ToString();

                    if (ifGotChild(itemCode))
                    {
                        //add empty space
                        dtStock_row = dt_Item.NewRow();
                        dt_Item.Rows.Add(dtStock_row);

                        //add parent item
                        //float readyStock = Convert.ToSingle(item["item_qty"]);

                        //dt_StockData = tool.getStockDataTableFromDataTable(dt_AllStockData, itemCode);

                        //addRowtoDataTable(dt_StockData, item["item_code"].ToString(), item["item_name"].ToString(), readyStock);
                        addRowtoDataTable(item);
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
                                    float childStockQty = Convert.ToSingle(itemInfoRow[dalItem.ItemStock]);

                                    //DataTable dt_ChildStockData = tool.getStockDataTableFromDataTable(dt_AllStockData, childCode);

                                    //addRowtoDataTable(dt_ChildStockData, childCode, childName, childStockQty);
                                    addRowtoDataTable(itemInfoRow);
                                    //check if have child
                                    if (ifGotChild(childCode))
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
                                                    float subChildStockQty = Convert.ToSingle(itemInfoRow2[dalItem.ItemStock]);

                                                    //DataTable dt_SubChildStockData = tool.getStockDataTableFromDataTable(dt_AllStockData, subChildCode);

                                                    //addRowtoDataTable(dt_SubChildStockData, subChildCode, subChildName, subChildStockQty);
                                                    addRowtoDataTable(itemInfoRow2);
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

            dgvItemList.DataSource = null;

            DataTable dt_StockRemovedFlag;

            if (dt_Item.Rows.Count > 0)
            {
                
                dt_StockRemovedFlag = dt_Item.Copy();
                dt_StockRemovedFlag.Columns.Remove(headerParentColor);
                dt_StockRemovedFlag.Columns.Remove(headerRepeat);

                //dt_Stock.DefaultView.Sort = headerIndex+" ASC";
                dgvItemList.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                dgvItemList.DataSource = dt_StockRemovedFlag;
                dgvItemUIEdit(dgvItemList);
                colorData(dgvItemList, dt_Item);
                dgvItemList.ClearSelection();
            }

            return gotData;
        }

        private void colorData(DataGridView dgv, DataTable dt)
        {
            int rowIndex = 0;
            string color = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                if (row[headerParentColor] != DBNull.Value)
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

                rowIndex++;
            }
        }

        private void loadChildParStocktData(string itemCode, string no)
        {
            int index = 1;
            string childIndex = no + "." + index.ToString();

            DataGridView dgv = dgvItemList;
            string parentItemCode = itemCode;
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    if (dalItem.getCatName(Join["join_child_code"].ToString()).Equals("Part"))
                    {
                        int n = dgv.Rows.Add();

                        dgv.Rows[n].Cells["item_code"].Value = Join["join_child_code"].ToString();

                        string childItemCode = Join["join_child_code"].ToString();

                        if (ifGotChild(childItemCode))
                        {
                            if (dalItem.checkIfAssembly(childItemCode) && dalItem.checkIfProduction(childItemCode))
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
                                dataInsertToStockDGV(dgvItemList, item, n, childIndex);
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
            dgvItemList.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                dgvItemList.DataSource = null;
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

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadMaterialStockData();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvStockReport_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
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

        private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvItemList.DataSource = null;

            if (cmbSubType.Text != null)
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

            if (dgvItemList.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else
            {
                try
                {

                    dgvItemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

                        DateTime date = DateTime.Now;
                        string dataDate = date.ToString("dd/MM/yyyy");

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
                            for (int i = 0; i <= dgvItemList.RowCount - 2; i++)
                            {
                                for (int j = 0; j <= dgvItemList.ColumnCount - 1; j++)
                                {
                                    Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

                                    if (i == 0)
                                    {
                                        Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                        header.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor);

                                        header = (Range)xlWorkSheet.Cells[1, 10];
                                        header.Font.Color = Color.Blue;

                                        header = (Range)xlWorkSheet.Cells[1, 14];
                                        header.Font.Color = Color.Red;

                                        header = (Range)xlWorkSheet.Cells[1, 16];
                                        header.Font.Color = Color.Red;


                                    }

                                    if (dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                                    {
                                        range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                        if (i == 0)
                                        {
                                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                        }
                                    }
                                    else if (dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
                                    {
                                        range.Rows.RowHeight = 3;
                                        range.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor);
                                        if (i == 0)
                                        {
                                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor);
                                        }
                                    }
                                    else
                                    {
                                        range.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor);

                                        if (i == 0)
                                        {
                                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[i].Cells[j].InheritedStyle.BackColor);
                                        }
                                    }
                                    range.Font.Color = dgvItemList.Rows[i].Cells[j].Style.ForeColor;
                                    if (dgvItemList.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
                                    {
                                        Range header = (Range)xlWorkSheet.Cells[i + 2, 2];
                                        header.Font.Underline = true;

                                        header = (Range)xlWorkSheet.Cells[i + 2, 3];
                                        header.Font.Underline = true;
                                    }

                                }

                                Int32 percentage = ((i + 1) * 100) / (dgvItemList.RowCount - 2);
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
                        dgvItemList.ClearSelection();

                        // Open the newly saved excel file
                        if (File.Exists(sfd.FileName))
                            System.Diagnostics.Process.Start(sfd.FileName);
                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type
                    dgvItemList.SelectionMode = DataGridViewSelectionMode.CellSelect;
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
            dgvItemList.SelectAll();
            //dgvNewStock.sele
            DataObject dataObj = dgvItemList.GetClipboardContent();
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
                dgvItemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                    dgvItemList.ClearSelection();
                }

                bgWorker.ReportProgress(100);
                System.Threading.Thread.Sleep(1000);
                dgvItemList.SelectionMode = DataGridViewSelectionMode.CellSelect;
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
                                for (int x = 0; x <= dgvItemList.RowCount - 2; x++)
                                {
                                    for (int y = 0; y <= dgvItemList.ColumnCount - 1; y++)
                                    {
                                        Range range = (Range)addedSheet.Cells[x + 2, y + 1];

                                        if (x == 0)
                                        {
                                            Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor);

                                            header = (Range)addedSheet.Cells[1, 10];
                                            header.Font.Color = Color.Blue;

                                            header = (Range)addedSheet.Cells[1, 14];
                                            header.Font.Color = Color.Red;

                                            header = (Range)addedSheet.Cells[1, 16];
                                            header.Font.Color = Color.Red;


                                        }

                                        if (dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor == SystemColors.Window)
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                            }
                                        }
                                        else if (dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor == Color.Black)
                                        {
                                            range.Rows.RowHeight = 3;
                                            range.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            }
                                        }
                                        else
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor);

                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(dgvItemList.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            }
                                        }
                                        range.Font.Color = dgvItemList.Rows[x].Cells[y].Style.ForeColor;
                                        if (dgvItemList.Rows[x].Cells[y].Style.ForeColor == Color.Blue)
                                        {
                                            Range header = (Range)addedSheet.Cells[x + 2, 2];
                                            header.Font.Underline = true;

                                            header = (Range)addedSheet.Cells[x + 2, 3];
                                            header.Font.Underline = true;
                                        }

                                    }

                                    Int32 percentage = ((x + 1) * 100) / (dgvItemList.RowCount - 2);
                                    if (percentage >= 100)
                                    {
                                        percentage = 100;
                                    }
                                    bgWorker.ReportProgress(percentage);
                                }
                            }

                            releaseObject(addedSheet);
                            Clipboard.Clear();
                            dgvItemList.ClearSelection();
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

        private void dgvStockReport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            int columnIndex = dgvItemList.CurrentCell.ColumnIndex;
            string columnName = dgvItemList.Columns[columnIndex].Name;
            if (tool.getFactoryID(columnName) != -1) //Desired Column
            {
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
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
            Thread t = new Thread(new ThreadStart(StartForm));
            try
            {
                if (Validation())
                {
                    t.Start();
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
            catch (ThreadAbortException)
            {
                // ignore it
                Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                t.Abort();
            }
        }

        public void StartForm()
        {
            System.Windows.Forms.Application.Run(new frmLoading());
        }

        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvItemList;
            //dgv.SuspendLayout();
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

            else if (dgv.Columns[col].Name != headerIndex && dgv.Columns[col].Name != headerName)
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

            //dgv.ResumeLayout();
        }

        private void cbIncludeSubMat_CheckedChanged(object sender, EventArgs e)
        {
            dgvItemList.DataSource = null;
        }

        #endregion

        private void ifRepeat(string itemCode, int index)
        {
            DataGridView dgv = dgvItemList;

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

        private void cbGetItemReport_CheckedChanged(object sender, EventArgs e)
        {
            if(cbGetItemReport.Checked)
            {
                btnUpdateData.Visible = false;
                cbImportItemData.Checked = false;
                lblTotalRowCount.Visible = false;
                lblType.Visible = true;
                lblSubType.Visible = true;
                cmbSubType.Visible = true;
                cmbType.Visible = true;
                btnCheck.Visible = true;

                if (cmbType.Text.Equals(CMBPartHeader))
                {
                    cbIncludeSubMat.Checked = false;
                    cbIncludeSubMat.Visible = true;
                }

                gbImportData.Visible = false;
            } 
            else
            {
                if(!cbImportItemData.Checked)
                {
                    cbGetItemReport.Checked = true;
                }
            }

        }

        private void cbImportItemData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImportItemData.Checked)
            {
                dgvItemList.DataSource = null;
                cbGetItemReport.Checked = false;
                lblTotalRowCount.Visible = true;
                gbImportData.Visible = true;

                lblType.Visible = false;
                lblSubType.Visible = false;
                cmbSubType.Visible = false;
                cmbType.Visible = false;
                cbIncludeSubMat.Visible = false;
                btnCheck.Visible = false;
            }
            else
            {
                if (!cbGetItemReport.Checked)
                {
                    cbImportItemData.Checked = true;
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtPath.BackColor = SystemColors.Window;
            txtPath.Clear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File Excel|*.xlsx";

            DialogResult re = fd.ShowDialog();
            excelName = fd.SafeFileName;

            if (re == DialogResult.OK)
            {
                path = fd.FileName;

                //string extension = System.IO.Path.GetExtension(path);
                //MessageBox.Show(extension);
                //if (".csv".Equals(extension))

                txtPath.Text = path;
                txtPath.BackColor = Color.DarkSeaGreen;
            }
        }

        private void gbImportData_Enter(object sender, EventArgs e)
        {

        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            btnUpdateData.Visible = false;
            dgvItemList.DataSource = null;
            if (path != null)
            {
                ReadExcel(path);

                if(dgvItemList.DataSource != null)
                {
                    btnUpdateData.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("path not found");
            }
        }

        public void ReadExcel(string Path)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            rowCount = 0;
            DataTable dt = NewItemTable();

            Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            Range range;

            int rCnt;
            int rw = 0;
            int cl = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            for (rCnt = 2; rCnt <= rw; rCnt++)
            {
                string index = (range.Cells[rCnt, 1] as Range).Value2 == null ? "-1" : Convert.ToString((range.Cells[rCnt, 1] as Range).Value2);
                string itemName = (range.Cells[rCnt, 2] as Range).Value2 == null ? "-1" : Convert.ToString((range.Cells[rCnt, 2] as Range).Value2);
                string itemCode = (range.Cells[rCnt, 3] as Range).Value2 == null ? "-1" : Convert.ToString((range.Cells[rCnt, 3] as Range).Value2);
                string quoTon = (range.Cells[rCnt, 4] as Range).Value2 == null ? "0" : Convert.ToString((range.Cells[rCnt, 4] as Range).Value2);
                string proTon = (range.Cells[rCnt, 5] as Range).Value2 == null ? "0" : Convert.ToString((range.Cells[rCnt, 5] as Range).Value2);
                string cavity = (range.Cells[rCnt, 6] as Range).Value2 == null ? "0" : Convert.ToString((range.Cells[rCnt, 6] as Range).Value2);
                string cycleTime = (range.Cells[rCnt, 7] as Range).Value2  == null ? "0" : Convert.ToString((range.Cells[rCnt, 7] as Range).Value2);
                string pwPerShot = (range.Cells[rCnt, 8] as Range).Value2 == null ? "0" : Convert.ToString((range.Cells[rCnt, 8] as Range).Value2);
                string rwPerShot = (range.Cells[rCnt, 9] as Range).Value2 == null ? "0" : Convert.ToString((range.Cells[rCnt, 9] as Range).Value2);

                if (!string.IsNullOrEmpty(itemCode))
                {
                    DataRow row = dt.NewRow();

                    row[headerIndex] = index;
                    row[headerCode] = itemCode;
                    row[headerName] = itemName;

                    row[headerQuoTon] = quoTon;
                    row[headerProTon] = proTon;
                    row[headerCavity] = cavity;
                    row[headerCycleTime] = cycleTime;
                    row[headerPWPerShot] = pwPerShot;
                    row[headerRWPerShot] = rwPerShot;

                    dt.Rows.Add(row);
                    rowCount++;
                    lblTotalRowCount.Text = rowCount.ToString();
                    itemCode = null;
                }
            }

            //dgvItemList.DataSource = dt;
            //dgvItemUIEdit(dgvItemList);

            dgvItemList.DataSource = null;

            DataTable dt_StockRemovedFlag;

            if (dt.Rows.Count > 0)
            {

                dt_StockRemovedFlag = dt.Copy();
                dt_StockRemovedFlag.Columns.Remove(headerParentColor);
                dt_StockRemovedFlag.Columns.Remove(headerRepeat);

                dgvItemList.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                dgvItemList.DataSource = dt_StockRemovedFlag;
                //dgvItemList.DataSource = dt;
                dgvItemUIEdit(dgvItemList);
                colorData(dgvItemList, dt);
                dgvItemList.ClearSelection();
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            lblTotalRowCount.Text = rowCount.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvItemList.DataSource;
            int rowIndex = 0;
            DataTable dt_Item = dalItem.Select();
            foreach(DataRow row in dt.Rows)
            {
                string itemCode = row[headerCode].ToString();
                if (tool.IfProductsExists(itemCode))
                {
                    uItem.item_code = itemCode;
                    uItem.item_quo_ton = Convert.ToInt32(row[headerQuoTon]);
                    uItem.item_pro_ton = Convert.ToInt32(row[headerProTon]);
                    uItem.item_cavity = Convert.ToInt32(row[headerCavity]);
                    uItem.item_pro_ct_to = Convert.ToInt32(row[headerCycleTime]);
                    uItem.item_pro_pw_shot = Convert.ToInt32(row[headerPWPerShot]);
                    uItem.item_pro_rw_shot = Convert.ToInt32(row[headerRWPerShot]);

                    if (tool.getCatNameFromDataTable(dt_Item,itemCode).Equals("Part"))
                    {
                        if(!updateItem())
                        {
                            //red color row
                            dgvItemList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {
                            dgvItemList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 184, 148);
                        }
                        
                    }
                    else
                    {
                        //updateMaterial();
                    }
                }
                else // item not exist in system, show yellow color
                {
                    //yellow color row
                    dgvItemList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                rowIndex++;
            }

        }

        private bool updateItem()
        {
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            //Updating data into database
            return dalItem.UpdateFromExcelImport(uItem);
        }

        private void updateMaterial()
        {


            bool success = dalMaterial.Update(uMaterial);
            if (success == true)
            {
                //data updated successfully
                //MessageBox.Show("Material successfully updated ");
                updateItem();
                this.Close();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated material");
            }
        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

            //MessageBox.Show(row.ToString());
        }
    }

}
