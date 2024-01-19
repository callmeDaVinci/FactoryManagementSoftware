using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutVer2 : Form
    {
        private int userPermission = -1;

        public frmInOutVer2()
        {
            try
            {
                InitializeComponent();
                dt_Fac = dalFac.SelectDESC();
                dt_Cust = dalCust.CustSelectAll();
                addDataToTrfHistDateCMB();
                userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
                btnTransfer.Show();
                //if (userPermission >= MainDashboard.ACTION_LVL_TWO)
                //{
                //    btnTransfer.Show();
                //}
                //else
                //{
                //    btnTransfer.Hide();
                //}

                dt_Plan = dalPlan.Select();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        #region variable declare
        private Button btn = new Button();
        static public string editingItemCat;
        static public string editingItemCode;
        static public string editingItemName;
        static public string editingFacName;
        static public DataTable editingItemTransferRecord;
        static public int editingIndexNo = -1;
        private bool formLoaded = false;
        private bool itemListLoaded = false;
        private int lastPastDay = 3;
        readonly string past3Days = "3";
        readonly string pastWeek = "7";
        readonly string pastMonth = "30";
        readonly string past2Months = "60";
        readonly string past3Months = "90";
        readonly string past6Months = "180";
        readonly string pastYear = "365";
        readonly string All = "ALL";

        DataTable dt_Fac;
        DataTable dt_Cust;
        DataTable dtJoin;
        DataTable dt_Plan;

        private DateTime updatedTime;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        #endregion

        #region create class object (database)


        facDAL dalFac = new facDAL();

        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();

        custSupplierDAL dalCust = new custSupplierDAL();

        itemCatDAL dalItemCat = new itemCatDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockDAL dalStock = new facStockDAL();

        joinDAL dalJoin = new joinDAL();

        userDAL dalUser = new userDAL();

        planningDAL dalPlan = new planningDAL();

        PlanningBLL uPlan = new PlanningBLL();
        childTrfHistDAL dalChildTrf = new childTrfHistDAL();
        childTrfHistBLL uChildTrfHist = new childTrfHistBLL();

        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();

        MacDAL dalMac = new MacDAL();

        Tool tool = new Tool();
        Text text = new Text();

        #endregion

        #region Load or Reset Form

        private void frmInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.inOutFormOpen = false;
        }


        private void frmInOut_Load(object sender, EventArgs e)
        {
            try
            {
                tool.DoubleBuffered(dgvItem, true);
                tool.DoubleBuffered(dgvTrf, true);
                tool.DoubleBuffered(dgvFactoryStock, true);
                resetForm();
                dataUpdatedTime();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                //itemListLoaded = true;
                ActiveControl = label1;
            }
        }

        private void dataUpdatedTime()
        {
            updatedTime = DateTime.Now;
            lblUpdatedTime.Text = updatedTime.ToString();
        }

        private void addDataToTrfHistDateCMB()
        {
            ComboBox cmb = cmbTransHistDate;
            cmb.Items.Clear();
            cmb.Items.Add(past3Days);
            cmb.Items.Add(pastWeek);
            cmb.Items.Add(pastMonth);
            cmb.Items.Add(past2Months);
            cmb.Items.Add(past3Months);
            cmb.Items.Add(past6Months);
            cmb.Items.Add(pastYear);
            cmb.Items.Add(All);

            cmb.SelectedIndex = 2;
        }

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbSearchCat.DataSource = distinctTable;
            cmbSearchCat.DisplayMember = "item_cat_name";


            //cmbSearchCat.SelectedIndex = 1;

            if (MainDashboard.myconnstrng == text.DB_Semenyih || MainDashboard.myconnstrng == text.DB_JunPC)
            {
                //Semenyih
                cmbSearchCat.Text = text.Cat_Part;
            }
            else
            {
                cmbSearchCat.SelectedIndex = 0;
            }

        }

        private void resetSaveData()
        {
            editingItemCat = "";
            editingItemName = "";
            editingItemCode = "";
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
        }

        private void refreshDataList()//refresh/update stock qty and order qty
        {
            DataGridView dgv = dgvItem;
            dataUpdatedTime();

            if (dgv.SelectedRows.Count > 0)//if item data selected,direct update item stock qty and order qty
            {
                int rowindex = dgv.CurrentCell.RowIndex;
                int columnindex = dgv.CurrentCell.ColumnIndex;
                string itemCode = dgv.Rows[rowindex].Cells[dalItem.ItemCode].Value.ToString();
                dgv.Rows[rowindex].Cells[dalItem.ItemStock].Value = dalItem.getStockQty(itemCode).ToString("0.00");
                dgv.Rows[rowindex].Cells[dalItem.ItemOrd].Value = dalItem.getOrderQty(itemCode);
                loadStockList(itemCode);
                calTotalStock(itemCode);
                loadTransferList(itemCode);
            }
            else//if item data not selected, then search in item datagridview and update stock qty and order qty
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    int n = row.Index;

                    if (editingItemCode == dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString())
                    {
                        dgv.Rows[n].Cells[dalItem.ItemStock].Value = dalItem.getStockQty(editingItemCode).ToString("0.00");
                        dgv.Rows[n].Cells[dalItem.ItemOrd].Value = dalItem.getOrderQty(editingItemCode);
                    }
                }
                //clear factory and total list since no item data is selected
                resetSaveData();
                loadItemList();
                loadTransferList();
                dgvFactoryStock.Rows.Clear();
                dgvTotal.Rows.Clear();
            }

        }

        private void refreshDataList(string itemCode)
        {
            DataGridView dgv = dgvItem;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;

                if (itemCode == dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString())
                {
                    dgv.Rows[n].Cells[dalItem.ItemStock].Value = dalItem.getStockQty(itemCode).ToString("0.00");
                    dgv.Rows[n].Cells[dalItem.ItemOrd].Value = dalItem.getOrderQty(itemCode);
                }

            }
            loadTransferList();
        }

        private void resetForm()
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                dt_Fac = dalFac.SelectDESC();
                dt_Cust = dalCust.CustSelectAll();
                dtJoin = dalJoin.SelectwithChildInfo();
                loadItemCategoryData();
                itemListLoaded = false;
                loadItemList();
                resetSaveData();
                loadTransferList();
                dgvFactoryStock.Rows.Clear();
                dgvTotal.Rows.Clear();


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

        private void listPaint(DataGridView dgv)
        {
            //tool.DoubleBuffered(dgv, true);

            //bool rowColorChange = true;
            //dgv.BorderStyle = BorderStyle.None;
            ////dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv.BackgroundColor = Color.White;

            //dgv.EnableHeadersVisualStyles = false;
            ////dgv.RowTemplate.Height = 41;
            //dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20,25,72);
            //dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                string itemCode = "";
                if (dgv == dgvItem)
                {
                    if (dgv.Rows[n].Cells[dalItem.ItemCode].Value != null)
                    {
                        itemCode = dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString();
                    }


                    float qty = 0;

                    if (dgv.Rows[n].Cells[dalItem.ItemStock] != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells[dalItem.ItemStock].Value.ToString(), out (qty));
                    }
                    if (ifGotChild(itemCode))
                    {
                        if (dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells[dalItem.ItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                            dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                        }
                        else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells[dalItem.ItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                            dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                        }
                        else
                        {
                            dgv.Rows[n].Cells[dalItem.ItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                            dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        }
                    }
                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells[dalItem.ItemStock].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                    else
                    {
                        dgv.Rows[n].Cells[dalItem.ItemStock].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                    }
                }
                else if (dgv == dgvTrf)
                {
                    itemCode = dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Value.ToString();

                    if (ifGotChild(itemCode))
                    {
                        if (dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                            dgv.Rows[n].Cells[daltrfHist.TrfItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };

                        }
                        else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                            dgv.Rows[n].Cells[daltrfHist.TrfItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };

                        }
                        else
                        {
                            dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                            dgv.Rows[n].Cells[daltrfHist.TrfItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        }
                    }

                    if (dgv.Rows[n].Cells[daltrfHist.TrfResult].Value != null)
                    {
                        if (dgv.Rows[n].Cells[daltrfHist.TrfResult].Value.ToString().Equals("Undo"))
                        {
                            dgv.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
                            dgv.Rows[n].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Strikeout);
                        }
                    }
                }
                else if (dgv == dgvFactoryStock)
                {
                    float qty = 0;

                    if (dgv.Rows[n].Cells["stock_qty"].Value.ToString() != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells["stock_qty"].Value.ToString(), out (qty));
                    }

                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells["stock_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                }
                else if (dgv == dgvTotal)
                {
                    float qty = 0;

                    if (dgv.Rows[n].Cells["Total"] != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells["Total"].Value.ToString(), out (qty));
                    }


                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells["Total"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                }
            }

            dgv.ClearSelection();
        }

        private void loadStockList(string itemCode)
        {
            if (formLoaded)
            {
                DataTable dt = dalStock.Select(itemCode);

                bool isRawMaterial =  tool.getItemCat(itemCode) == text.Cat_RawMat;

                dgvFactoryStock.Columns["Remark"].Visible = isRawMaterial;

                dgvFactoryStock.Rows.Clear();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow stock in dt.Rows)
                    {
                        int n = dgvFactoryStock.Rows.Add();
                        dgvFactoryStock.Rows[n].Cells["fac_name"].Value = stock["fac_name"].ToString();

                        float facStock = Convert.ToSingle(stock["stock_qty"].ToString());
                        facStock = (float)Math.Truncate(facStock * 1000) / 1000;

                        string facStock_string = facStock.ToString("0.##");

                        dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = facStock_string;


                        string remark = "";


                        if(isRawMaterial)
                        {
                            int bagQty = (int) facStock / 25;

                            if(bagQty == 1)
                            {
                                remark = bagQty + " bag";

                            }
                            else if(bagQty > 1)
                            {
                                remark = bagQty + " bags";

                            }
                        }

                        dgvFactoryStock.Rows[n].Cells["Remark"].Value = remark;

                    }

                }
                else
                {
                    int n = dgvFactoryStock.Rows.Add();
                    dgvFactoryStock.Rows[n].Cells["fac_name"].Value = "null";
                    dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = "null";

                }
                listPaint(dgvFactoryStock);
            }
            else
            {
                dgvFactoryStock.Rows.Clear();
                dgvFactoryStock.Refresh();
            }

        }

        private void calTotalStock(string itemCode)
        {
            float totalStock = 0;

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                if (stock["stock_qty"] == null)
                {
                    MessageBox.Show("empty stock data");
                }
                else
                {
                    totalStock += Convert.ToSingle(stock["stock_qty"].ToString());
                }

            }

            dgvTotal.Rows.Clear();
            dgvTotal.Rows.Add();
            dgvTotal.Rows[0].Cells["Total"].Value = totalStock.ToString("0.##");
            dgvTotal.ClearSelection();


        }

        private void loadItemList()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            DataGridView dgv = dgvItem;
            dataUpdatedTime();

            DataTable dtItem;
            string keywords = txtSearch.Text;

            if (!string.IsNullOrEmpty(keywords) && keywords != "Search")
            {
                if (cmbSearchCat.Text.Equals("All"))//string.IsNullOrEmpty(cmbSearchCat.Text) || 
                {
                    //show all item from the database
                    dtItem = dalItem.InOutSearch(keywords);//search item code and item name
                }
                else
                {
                    if (MainDashboard.myconnstrng == text.DB_Semenyih && cmbSearchCat.Text == text.Cat_Part)
                    {
                        dtItem = dalItem.InOutCatSBBItemSearch(keywords);
                    }
                    else
                    {
                        dtItem = dalItem.InOutCatItemSearch(keywords, cmbSearchCat.Text);

                    }
                }
            }
            else
            {
                if (cmbSearchCat.Text.Equals("All"))//string.IsNullOrEmpty(cmbSearchCat.Text) || 
                {
                    //show all item from the database
                    dtItem = dalItem.InOutSelect();
                }
                else
                {
                    if (MainDashboard.myconnstrng == text.DB_Semenyih && cmbSearchCat.Text == text.Cat_Part)
                    {
                        dtItem = dalItem.InOutCatSBBItemSearch();
                    }
                    else
                    {
                        dtItem = dalItem.catInOutSearch(cmbSearchCat.Text);
                    }
                }

            }

            dgv.DataSource = null;

            if (dtItem.Rows.Count > 0)
            {
                dgv.DataSource = dtItem;
                dgvItemUIEdit(dgv);
                dgv.ClearSelection();
            }
            itemListLoaded = true;
        }

        private void loadTransferList(string itemCode)
        {
            DataTable dt;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(itemCode))
            {

                if (!cmbTransHistDate.Text.Equals(All))
                {
                    //MessageBox.Show(currenteDate.ToString("yyyy/MM/dd"));
                    dt = daltrfHist.codeRangeSearch(itemCode, Convert.ToInt32(cmbTransHistDate.Text));
                }
                else
                {

                    //dt = daltrfHist.codeLikeSearch(itemCode);
                    dt = daltrfHist.codeSearch(itemCode);
                }

            }
            else
            {
                if (!cmbTransHistDate.Text.Equals(All))
                {
                    //MessageBox.Show(currenteDate.ToString("yyyy/MM/dd"));
                    dt = daltrfHist.pastAddedSearch(Convert.ToInt32(cmbTransHistDate.Text));
                }
                else
                {
                    dt = daltrfHist.Select();
                }
            }

            dgvTrf.DataSource = null;
            dgvTrf.Rows.Clear();

            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "trf_hist_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                sortedDt.Columns.Add(text.Header_ItemDescription, typeof(string)).SetOrdinal(dt.Columns[daltrfHist.TrfItemCode].Ordinal);

                foreach (DataRow row in sortedDt.Rows)
                {
                    string itemName = row[daltrfHist.TrfItemName].ToString();
                    itemCode = row[daltrfHist.TrfItemCode].ToString();

                    string itemDescription = tool.getItemNameAndCodeString(itemCode, itemName);
                    row[text.Header_ItemDescription] = itemDescription;

                    if(sortedDt.Columns.Contains(daltrfHist.Balance))
                    {
                        float balance = float.TryParse(row[daltrfHist.Balance].ToString(), out balance) ? balance : 0;

                        balance = (float)Math.Truncate(balance * 1000) / 1000;

                        row[text.Header_Balance] = balance.ToString("0.##");
                    }

                    if (sortedDt.Columns.Contains(daltrfHist.TrfQty))
                    {
                        float trfqty = Convert.ToSingle(row[daltrfHist.TrfQty].ToString());

                        trfqty = (float)Math.Truncate(trfqty * 1000) / 1000;

                        row[daltrfHist.TrfQty] = trfqty.ToString("0.##");
                    }

                }

                dgvTrf.DataSource = sortedDt;
                dgvTrfUIEdit(dgvTrf);
                dgvTrf.ClearSelection();
                //listPaint(dgvTrf);
                #region old version
                //foreach (DataRow trf in sortedDt.Rows)
                //{
                //    int n = dgvTrf.Rows.Add();
                //    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
                //    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["item_name"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
                //    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_hist_note"].Value = trf["trf_hist_note"].ToString();
                //    dgvTrf.Rows[n].Cells["trf_result"].Value = trf["trf_result"].ToString();



                //    if (Convert.ToInt32(trf["trf_hist_added_by"]) <= 0)
                //    {
                //        dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = "ADMIN";
                //    }
                //    else
                //    {
                //        dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = dalUser.getUsername(Convert.ToInt32(trf["trf_hist_added_by"]));
                //    }
                //}
                #endregion
            }
        }

        private string getBetween(string strSource, string strStart, string strEnd1, string strEnd2)
        {



            if (strSource.Contains(strStart) && (strSource.Contains(strEnd1) || strSource.Contains(strEnd2)))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd1, Start);

                if (End <= -1)
                {
                    End = strSource.IndexOf(strEnd2, Start);

                    if (End <= -1)
                    {
                        return "Failed to get Plan ID";
                    }
                }



                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private string GetPlanID(string note)
        {
            note = note.ToUpper();

            string planID = "";

            if (note.Contains("PLAN"))
                planID = getBetween(note, "PLAN ", "(", "]");


            return planID;

        }

        private string GetPlanItem(string PlanID)
        {
            string planItem = "";


            foreach (DataRow row in dt_Plan.Rows)
            {
                if (PlanID == row[dalPlan.jobNo].ToString())
                {
                    string itemCode = row[dalPlan.partCode].ToString();
                    string itemName = tool.getItemName(itemCode);

                    if (itemName != itemCode)
                        planItem = itemCode + "_" + itemName;
                    else
                    {
                        planItem = itemName;

                    }
                    break;
                }
            }

            return planItem;

        }

        private void loadTransferList()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            DataTable dt;
            dataUpdatedTime();

            //get keyword from text box
            string keywords = txtSearch.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords) && keywords != "Search")
            {
                if (!cmbTransHistDate.Text.Equals(All))
                {
                    dt = daltrfHist.keywordRangeSearch(keywords, Convert.ToInt32(cmbTransHistDate.Text));
                }
                else
                {
                    dt = daltrfHist.Search(keywords);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cmbSearchCat.Text) || cmbSearchCat.Text.Equals("All"))
                {
                    //show all item from the database
                    if (!cmbTransHistDate.Text.Equals(All) && !string.IsNullOrEmpty(cmbTransHistDate.Text))
                    {
                        dt = daltrfHist.pastAddedSearch(Convert.ToInt32(cmbTransHistDate.Text));
                    }
                    else
                    {
                        dt = daltrfHist.Select();
                    }
                }
                else
                {
                    if (!cmbTransHistDate.Text.Equals(All))
                    {
                        dt = daltrfHist.catTrfRangeAddSearch(cmbSearchCat.Text, Convert.ToInt32(cmbTransHistDate.Text));
                    }
                    else
                    {
                        dt = daltrfHist.catSearch(cmbSearchCat.Text);
                    }
                }
            }

            dgvTrf.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(text.Header_ItemDescription, typeof(string)).SetOrdinal(dt.Columns[daltrfHist.TrfItemCode].Ordinal);

                foreach (DataRow row in dt.Rows)
                {
                    string itemName = row[daltrfHist.TrfItemName].ToString();
                    string itemCode = row[daltrfHist.TrfItemCode].ToString();

                    string itemDescription = tool.getItemNameAndCodeString(itemCode, itemName);
                    row[text.Header_ItemDescription] = itemDescription;

                    if(dt.Columns.Contains(daltrfHist.Balance))
                    {
                        float balance = Convert.ToSingle(row[daltrfHist.Balance].ToString());

                        balance = (float)Math.Truncate(balance * 1000) / 1000;

                        row[text.Header_Balance] = balance.ToString("0.##");
                    }
                  

                }

                dgvTrf.DataSource = dt;
                dgvTrfUIEdit(dgvTrf);
                dgvTrf.ClearSelection();
            }
            //itemListLoaded = true;
        }


        private void LoadPlanItem()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataTable dt = (DataTable)dgvTrf.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                //get note
                string note = row[daltrfHist.TrfNote].ToString();
                string itemName = row[daltrfHist.TrfItemName].ToString();
                string itemCode = row[daltrfHist.TrfItemCode].ToString();

                string PlanID = GetPlanID(note);

                string PlanItem = GetPlanItem(PlanID);

                if (!string.IsNullOrEmpty(PlanID) && !PlanItem.Contains(itemCode) && !PlanItem.Contains(itemName))
                    row[daltrfHist.TrfNote] = note + " : " + PlanItem;
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void dgvItemUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[dalItem.ItemCat].HeaderText = "Category";
            dgv.Columns[dalItem.ItemCode].HeaderText = "Code";
            dgv.Columns[dalItem.ItemName].HeaderText = "Name";
            dgv.Columns[dalItem.ItemOrd].HeaderText = "Order Pending";
            dgv.Columns[dalItem.ItemStock].HeaderText = "Stock";

            dgv.Columns[dalItem.ItemCat].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalItem.ItemCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[dalItem.ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[dalItem.ItemOrd].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalItem.ItemStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalItem.ItemOrd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[dalItem.ItemStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[dalItem.ItemStock].DefaultCellStyle.Format = "0.##";
            dgv.Columns[dalItem.ItemOrd].DefaultCellStyle.Format = "0.##";
        }

        private void dgvTrfUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[daltrfHist.TrfID].HeaderText = "ID";
            dgv.Columns[daltrfHist.TrfAddedDate].HeaderText = "Added_Date";
            dgv.Columns[daltrfHist.TrfDate].HeaderText = "Trf_Date";
            dgv.Columns[daltrfHist.TrfItemCode].HeaderText = "Code";
            dgv.Columns[daltrfHist.TrfItemName].HeaderText = "Name";
            dgv.Columns[daltrfHist.TrfFrom].HeaderText = "From";
            dgv.Columns[daltrfHist.TrfTo].HeaderText = "To";
            dgv.Columns[daltrfHist.TrfQty].HeaderText = "Qty";
            dgv.Columns[daltrfHist.TrfUnit].HeaderText = "Unit";
            dgv.Columns[daltrfHist.TrfNote].HeaderText = "Note";
            dgv.Columns[daltrfHist.TrfAddedBy].HeaderText = "By";
            dgv.Columns[daltrfHist.TrfResult].HeaderText = "Result";

            dgv.Columns[daltrfHist.TrfID].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            dgv.Columns[daltrfHist.TrfAddedDate].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            dgv.Columns[daltrfHist.TrfUnit].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            dgv.Columns[daltrfHist.TrfNote].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[daltrfHist.TrfNote].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            dgv.Columns[daltrfHist.TrfID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfAddedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[daltrfHist.TrfItemCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[daltrfHist.TrfItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[daltrfHist.TrfFrom].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfTo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfUnit].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[daltrfHist.TrfAddedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dgv.Columns[daltrfHist.TrfResult].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[daltrfHist.TrfNote].MinimumWidth = 200;
            dgv.Columns[daltrfHist.TrfQty].MinimumWidth = 80;
            dgv.Columns[daltrfHist.TrfFrom].MinimumWidth = 100;
            dgv.Columns[daltrfHist.TrfTo].MinimumWidth = 100;
            dgv.Columns[daltrfHist.TrfDate].MinimumWidth = 100;
            dgv.Columns[daltrfHist.TrfItemCode].Width = 0;
            dgv.Columns[daltrfHist.TrfItemName].Width = 0;
            dgv.Columns[daltrfHist.TrfResult].Width = 0;
            dgv.Columns[daltrfHist.TrfQty].DefaultCellStyle.Format = "0.##";

            dgv.Columns[daltrfHist.TrfAddedBy].Visible = false;
            //dgv.Columns[daltrfHist.TrfItemCode].Visible = false;
            //dgv.Columns[daltrfHist.TrfItemName].Visible = false;
            //dgv.Columns[daltrfHist.TrfResult].Visible = false;
            //dgv.Columns[daltrfHist.TrfResult].Visible = false;

            //dgv.Columns[header_ItemCode].DefaultCellStyle.ForeColor = Color.Gray;

        }
        #endregion

        #region get data/validation

        private bool CheckIfTrfOutToCustomer(string trfFrom, string trfTo)
        {
            bool fromFactory = tool.IfFactoryExists(dt_Fac, trfFrom);
            bool toCustomer = tool.IfCustomer(dt_Cust, trfTo);

            if (fromFactory && toCustomer)
            {
                return true;
            }

            return false;
        }

        private float getQty(string itemCode, string factoryName)
        {
            float qty = 0;
            if (IfExists(itemCode, factoryName))
            {
                DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

                qty = Convert.ToSingle(dt.Rows[0]["stock_qty"].ToString());
                //MessageBox.Show("get qty= "+qty);
            }
            else
            {
                qty = 0;
            }

            return qty;
        }

        private string getFactoryID(string factoryName)
        {
            string factoryID = "";

            DataTable dtFac = dalFac.nameSearch(factoryName);

            foreach (DataRow fac in dtFac.Rows)
            {
                factoryID = fac["fac_id"].ToString();
            }
            return factoryID;
        }

        private bool ifFactory(string factoryName)
        {
            bool result = false;

            DataTable dtFac = dalFac.nameSearch(factoryName);

            if (dtFac.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        private bool IfExists(string itemCode, string factoryName)
        {

            DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private string checkUnit(string keyword)
        {
            string unit = keyword;
            if (unit.Equals("g"))
            {
                unit = "kg";
            }
            return unit;
        }

        private bool ifGotChild(string itemCode)
        {
            DataTable dtJoin = dalJoin.loadChildList(itemCode);

            if (tool.getItemCat(itemCode) == text.Cat_Part)
            {
                if (dtJoin.Rows.Count > 0)
                {
                    foreach (DataRow row in dtJoin.Rows)
                    {
                        string ChildCode = row[dalJoin.JoinChild].ToString();

                        string ChildCat = tool.getItemCat(ChildCode);

                        if (ChildCat == text.Cat_Part || ChildCat == text.Cat_SubMat)
                        {
                            return true;
                        }
                    }

                }
            }


            return false;
        }

        #endregion

        #region text/index changed or click

        private void cmbSearchCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formLoaded)
            {
                try
                {
                    itemListLoaded = false;
                    Application.UseWaitCursor = true;
                    loadItemList();
                    resetSaveData();
                    loadTransferList();
                    dgvFactoryStock.Rows.Clear();
                    dgvTotal.Rows.Clear();
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    Application.UseWaitCursor = false;
                    itemListLoaded = true;
                    ActiveControl = label1;
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (formLoaded)
            {
                timer1.Stop();
                timer1.Start();
            }


            //if (!string.IsNullOrEmpty(txtSearch.Text))
            //{
            //    Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //    btn.Visible = true;
            //    loadItemList();
            //    loadTransferList();
            //    Cursor = Cursors.Arrow; // change cursor to normal type
            //}
            //else
            //{
            //    btn.Visible = false;
            //}

        }


        private void dgvItem_Sorted(object sender, EventArgs e)
        {
            dgvItem.ClearSelection();
            loadTransferList();
            dgvFactoryStock.Rows.Clear();
            dgvTotal.Rows.Clear();
            //listPaint((DataGridView)sender);
            Application.UseWaitCursor = false;
            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void dgvItem_SelectionChanged(object sender, EventArgs e)
        {

            if (formLoaded && dgvItem.SelectedRows.Count > 0)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvItem;
                int rowIndex;
                if (dgv.CurrentCell != null)
                {
                    rowIndex = dgv.CurrentCell.RowIndex;
                }
                else
                {
                    rowIndex = -1;
                }


                if (rowIndex >= 0)
                {
                    editingItemCat = dgv.Rows[rowIndex].Cells[dalItem.ItemCat].Value == null ? "" : dgv.Rows[rowIndex].Cells[dalItem.ItemCat].Value.ToString();
                    editingItemName = dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value == null ? "" : dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value.ToString();
                    editingItemCode = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value == null ? "" : dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();

                    if (editingItemCat == null || editingItemName == null || editingItemCode == null)
                    {
                        MessageBox.Show("empty value after selected");
                        dgvFactoryStock.DataSource = null;
                        dgvTotal.DataSource = null;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[dalItem.ItemStock].Value = dalItem.getStockQty(editingItemCode).ToString("0.00");
                        dgv.Rows[rowIndex].Cells[dalItem.ItemOrd].Value = dalItem.getOrderQty(editingItemCode);

                        loadStockList(editingItemCode);
                        calTotalStock(editingItemCode);
                        loadTransferList(editingItemCode);
                    }
                }
                else
                {
                    resetSaveData();
                    dgvFactoryStock.DataSource = null;
                    dgvTotal.DataSource = null;
                }
                Cursor = Cursors.Arrow; // change cursor to normal type 
            }

        }

        private void dgvTrf_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgvTrf.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                if (dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value != null)
                {
                    int.TryParse(dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value.ToString(), out (editingIndexNo));
                }
                if (editingIndexNo != -1)
                {
                    DataTable dt = dalChildTrf.indexSearch(editingIndexNo);

                    if (dt.Rows.Count > 0)
                    {
                        frmChildStockOutRecord frm = new frmChildStockOutRecord();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();//Item Edit
                    }

                }
            }
            else
            {
                editingIndexNo = -1;
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvFactoryStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgvFactoryStock.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                editingFacName = dgvFactoryStock.Rows[rowIndex].Cells["fac_name"].Value.ToString();

                if (!string.IsNullOrEmpty(editingFacName) || editingFacName != "null")
                {
                    DataTable dt = daltrfHist.facSearch(editingItemCode, editingFacName);
                    if (dt.Rows.Count > 0)
                    {
                        frmFactoryTrfRecord frm = new frmFactoryTrfRecord();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();//Item Edit
                    }
                }
            }
            else
            {
                editingIndexNo = -1;
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvItem_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void dgvFactoryStock_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvFactoryStock.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvItem.ClearSelection();
                refreshDataList();
                txtSearch.Clear();
            }
        }

        private void dgvItem_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            Application.UseWaitCursor = true;
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


        
        private void dgvItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = dgvItem;
            int Permission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            int rowIndex = dgv.CurrentCell.RowIndex;
            //&& Permission >= MainDashboard.ACTION_LVL_TWO
            if (rowIndex >= 0 )
            {
                try
                {
                    editingItemCat = dgv.Rows[rowIndex].Cells["item_cat"].Value == null ? "" : dgv.Rows[rowIndex].Cells["item_cat"].Value.ToString();
                    editingItemName = dgv.Rows[rowIndex].Cells["item_name"].Value == null ? "" : dgv.Rows[rowIndex].Cells["item_name"].Value.ToString();
                    editingItemCode = dgv.Rows[rowIndex].Cells["item_code"].Value == null ? "" : dgv.Rows[rowIndex].Cells["item_code"].Value.ToString();
                    editingItemTransferRecord = (DataTable)dgvTrf.DataSource;

                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    frmInOutEdit frm = new frmInOutEdit();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit

                    if (frmInOutEdit.updateSuccess)
                    {
                        //txtSearch.Text = editingItemCode; // update date list      
                    }

                    refreshDataList();
                    //listPaintAndKeepSelected(dgv);
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
            }
        }

        #endregion

        #region Function

        //transfer stock
        private void transfer_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                frmInOutEdit frm = new frmInOutEdit();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();//Item Edit

                if (frmInOutEdit.updateSuccess)
                {
                    //txtSearch.Text = editingItemCode; // update date list      
                }

                refreshDataList();
                //listPaintAndKeepSelected(dgvItem);

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

        //unselect data when click on empty space
        private void frmInOut_MouseClick(object sender, MouseEventArgs e)
        {

        }

        //reset
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MainDashboard.stockReportFormOpen)
                {
                    frmStockReport frm = new frmStockReport();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    MainDashboard.stockReportFormOpen = true;
                }
                else
                {
                    if (Application.OpenForms.OfType<frmStockReport>().Count() == 1)
                    {
                        Application.OpenForms.OfType<frmStockReport>().First().BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        //show undo or redo menustrip
        private void dgvTrf_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && userPermission >= MainDashboard.ACTION_LVL_TWO)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();

                    dgvTrf.CurrentCell = dgvTrf.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgvTrf.Rows[e.RowIndex].Selected = true;
                    dgvTrf.Focus();
                    int rowIndex = dgvTrf.CurrentCell.RowIndex;

                    string result = dgvTrf.Rows[rowIndex].Cells["trf_result"].Value.ToString();
                    if (result.Equals("Passed"))
                    {
                        my_menu.Items.Add("Undo").Name = "Undo";
                    }
                    else if (result.Equals("Undo"))
                    {
                        my_menu.Items.Add("Redo").Name = "Redo";
                    }

                    my_menu.Items.Add(text.Jump).Name = text.Jump;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        //undo/redo function
        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                             //MessageBox.Show(e.ClickedItem.Name.ToString());
                int rowIndex = dgvTrf.CurrentCell.RowIndex;
                bool fromOrder = daltrfHist.ifFromOrder(Convert.ToInt32(dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfID].Value.ToString()));


                if (e.ClickedItem.Name.ToString().Equals(text.Jump))
                {
                    string trfID = dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfID].Value.ToString();

                    txtJumpID.Text = trfID;

                    formLoaded = false;

                    txtSearch.Text = "Search";
                    txtSearch.ForeColor = SystemColors.GrayText;

                    cmbSearchCat.Text = text.Cmb_All;

                    refreshDataList();

                    formLoaded = true;

                    JumpToSelectedTrfIDRow();
                }
                else
                {
                    if (dgvItem.SelectedRows.Count <= 0)
                    {
                        editingItemCode = dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfItemCode].Value.ToString();
                    }

                    if (!fromOrder)
                    {
                        if (rowIndex >= 0 && e.ClickedItem.Name.ToString().Equals("Undo"))
                        {
                            //MessageBox.Show(dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value.ToString());
                            if (!undo(rowIndex))
                            {
                                MessageBox.Show("Failed to undo");
                            }
                        }
                        else if (rowIndex >= 0 && e.ClickedItem.Name.ToString().Equals("Redo"))
                        {
                            //MessageBox.Show(dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value.ToString());
                            if (!redo(rowIndex))
                            {
                                MessageBox.Show("Failed to redo");
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please go to the ORDER PAGE to change the record");
                    }



                    string itemCode = dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfItemCode].Value.ToString();

                    loadStockList(itemCode);
                    calTotalStock(itemCode);
                }

                //listPaintAndKeepSelected(dgvItem);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvTrf_MouseClick(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    var ht = dgvTrf.HitTest(e.X, e.Y);

            //    if (ht.Type == DataGridViewHitTestType.None)
            //    {
            //        //clicked on grey area
            //        dgvItem.ClearSelection();
            //        refreshDataList();
            //        txtSearch.Clear();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tool.saveToTextAndMessageToUser(ex);
            //}
        }

        #endregion

        #region data processing

        private bool undo(int rowIndex)
        {
            bool result = false;

            int id = Convert.ToInt32(dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value.ToString());

            string itemCode = dgvTrf.Rows[rowIndex].Cells["trf_hist_item_code"].Value.ToString();

            string locationFrom = dgvTrf.Rows[rowIndex].Cells["trf_hist_from"].Value.ToString();

            string locationTo = dgvTrf.Rows[rowIndex].Cells["trf_hist_to"].Value.ToString();

            string unit = dgvTrf.Rows[rowIndex].Cells["trf_hist_unit"].Value.ToString();

            float qty = Convert.ToSingle(dgvTrf.Rows[rowIndex].Cells["trf_hist_qty"].Value.ToString());

            string note = dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfNote].Value.ToString();

            if (ifFactory(locationFrom))
            {
                result = stockIn(locationFrom, itemCode, qty, unit);

                if (ifFactory(locationTo))
                {
                    result = stockOut(locationTo, itemCode, qty, unit);
                }
            }
            else if (ifFactory(locationTo) && !locationFrom.Equals("Assembly"))
            {
                result = stockOut(locationTo, itemCode, qty, unit);
            }
            else if (locationFrom.Equals("Assembly") && ifFactory(locationTo))
            {
                result = stockOut(locationTo, itemCode, qty, unit);
                //result = childStockIn(locationTo, itemCode, qty, id, unit);
            }

            if (result)
            {
                changeTransferRecord("Undo", rowIndex, id);
                tool.historyRecord(text.TransferUndo, text.getTransferDetailString(id, qty, unit, itemCode, locationFrom, locationTo), DateTime.Now, MainDashboard.USER_ID);

                matPlanDAL dalMatPlan = new matPlanDAL();

                string textFind = tool.getBetween(note, "[For Plan ", "]");
                int planID = -1;
                if (textFind != "" && int.TryParse(textFind, out planID))
                {
                    if (planID != -1)
                    {
                        //update transferred qty
                        DataTable dt_MatPlan = dalMatPlan.Select();

                        foreach (DataRow mat in dt_MatPlan.Rows)
                        {
                            bool active = Convert.ToBoolean(mat[dalMatPlan.Active]);

                            if (active)
                            {
                                float checkingQty = float.TryParse(mat[dalMatPlan.Prepare].ToString(), out float j) ? Convert.ToSingle(mat[dalMatPlan.Prepare].ToString()) : 0;
                                string orgFrom = mat[dalMatPlan.MatFrom].ToString();
                                bool match = true;
                                int checkingPlanID = int.TryParse(mat[dalMatPlan.PlanID].ToString(), out int k) ? Convert.ToInt32(mat[dalMatPlan.PlanID]) : 0;

                                if (itemCode != mat[dalMatPlan.MatCode].ToString())
                                {
                                    match = false;
                                }

                                else if (planID != checkingPlanID)
                                {
                                    match = false;
                                }

                                if (match)
                                {
                                    //get plan id

                                    float Transferred = float.TryParse(mat[dalMatPlan.Transferred].ToString(), out float l) ? Convert.ToSingle(mat[dalMatPlan.Transferred].ToString()) : 0;

                                    uMatPlan.mat_code = itemCode;
                                    uMatPlan.plan_id = planID;
                                    uMatPlan.mat_transferred = Transferred - qty < 0 ? 0 : Transferred - qty;
                                    uMatPlan.mat_preparing = checkingQty;
                                    uMatPlan.mat_from = orgFrom;
                                    uMatPlan.updated_date = DateTime.Now;
                                    uMatPlan.updated_by = MainDashboard.USER_ID;

                                    if (!dalMatPlan.MatPrepareUpdate(uMatPlan))
                                    {
                                        MessageBox.Show("Failed to update material preparing data.");
                                    }
                                }
                            }


                        }
                    }
                }
            }

            return result;
        }

        private bool redo(int rowIndex)
        {
            bool result = false;

            int id = Convert.ToInt32(dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value.ToString());

            string itemCode = dgvTrf.Rows[rowIndex].Cells["trf_hist_item_code"].Value.ToString();

            string locationFrom = dgvTrf.Rows[rowIndex].Cells["trf_hist_from"].Value.ToString();

            string locationTo = dgvTrf.Rows[rowIndex].Cells["trf_hist_to"].Value.ToString();

            string unit = dgvTrf.Rows[rowIndex].Cells["trf_hist_unit"].Value.ToString();

            float qty = Convert.ToSingle(dgvTrf.Rows[rowIndex].Cells["trf_hist_qty"].Value.ToString());

            string note = dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfNote].Value.ToString();

            if (ifFactory(locationFrom))
            {
                result = stockOut(locationFrom, itemCode, qty, unit);

                if (ifFactory(locationTo))
                {
                    result = stockIn(locationTo, itemCode, qty, unit);
                }
            }
            else if (ifFactory(locationTo) && !locationFrom.Equals("Assembly"))
            {
                result = stockIn(locationTo, itemCode, qty, unit);
            }
            else if (locationFrom.Equals("Assembly") && ifFactory(locationTo))
            {
                result = stockIn(locationTo, itemCode, qty, unit);
                //result = childStockOut(locationTo, itemCode, qty, id, unit);
            }

            if (result)
            {
                changeTransferRecord("Passed", rowIndex, id);
                tool.historyRecord(text.TransferRedo, text.getTransferDetailString(id, qty, unit, itemCode, locationFrom, locationTo), DateTime.Now, MainDashboard.USER_ID);

                matPlanDAL dalMatPlan = new matPlanDAL();

                string textFind = tool.getBetween(note, "[For Plan ", "]");
                int planID = -1;
                if (textFind != "" && int.TryParse(textFind, out planID))
                {
                    if (planID != -1)
                    {
                        //update transferred qty
                        DataTable dt_MatPlan = dalMatPlan.Select();

                        foreach (DataRow mat in dt_MatPlan.Rows)
                        {
                            bool active = Convert.ToBoolean(mat[dalMatPlan.Active]);

                            if (active)
                            {
                                float checkingQty = float.TryParse(mat[dalMatPlan.Prepare].ToString(), out float j) ? Convert.ToSingle(mat[dalMatPlan.Prepare].ToString()) : 0;
                                string orgFrom = mat[dalMatPlan.MatFrom].ToString();
                                bool match = true;
                                int checkingPlanID = int.TryParse(mat[dalMatPlan.PlanID].ToString(), out int k) ? Convert.ToInt32(mat[dalMatPlan.PlanID]) : 0;

                                if (itemCode != mat[dalMatPlan.MatCode].ToString())
                                {
                                    match = false;
                                }

                                else if (planID != checkingPlanID)
                                {
                                    match = false;
                                }

                                if (match)
                                {
                                    //get plan id

                                    float Transferred = float.TryParse(mat[dalMatPlan.Transferred].ToString(), out float l) ? Convert.ToSingle(mat[dalMatPlan.Transferred].ToString()) : 0;

                                    uMatPlan.mat_code = itemCode;
                                    uMatPlan.plan_id = planID;
                                    uMatPlan.mat_transferred = Transferred + qty;
                                    uMatPlan.mat_preparing = checkingQty;
                                    uMatPlan.mat_from = orgFrom;
                                    uMatPlan.updated_date = DateTime.Now;
                                    uMatPlan.updated_by = MainDashboard.USER_ID;

                                    if (!dalMatPlan.MatPrepareUpdate(uMatPlan))
                                    {
                                        MessageBox.Show("Failed to update material preparing data.");
                                    }
                                }
                            }


                        }
                    }
                }
            }

            return result;
        }

        private bool stockIn(string factoryName, string itemCode, float qty, string unit)
        {
            bool successFacStockIn;

            successFacStockIn = dalStock.facStockIn(getFactoryID(factoryName), itemCode, qty, unit);

            return successFacStockIn;
        }

        private bool stockOut(string factoryName, string itemCode, float qty, string unit)
        {
            bool successFacStockOut = false;

            successFacStockOut = dalStock.facStockOut(getFactoryID(factoryName), itemCode, qty, unit);

            return successFacStockOut;
        }

        private void changeTransferRecord(string stockResult, int rowIndex, int id)
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
                tool.historyRecord(text.System, "Failed to change transfer record", utrfHist.trf_hist_updated_date, MainDashboard.USER_ID);
            }
            else
            {
                dgvTrf.Rows[rowIndex].Cells["trf_result"].Value = stockResult;
            }
        }

        private void deleteChildTransferRecord(int indexNo, string itemCode)
        {
            uChildTrfHist.child_trf_hist_code = itemCode;
            uChildTrfHist.child_trf_hist_id = indexNo;

            //Inserting Data into Database
            bool success = dalChildTrf.Delete(uChildTrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to delete child transfer record");
            }
        }

        private void childTransferRecord(string factoryName, int indexNo, string itemCode, float qty)
        {
            uChildTrfHist.child_trf_hist_code = itemCode;
            uChildTrfHist.child_trf_hist_from = factoryName;
            uChildTrfHist.child_trf_hist_to = "Assembly";
            uChildTrfHist.child_trf_hist_qty = qty;
            uChildTrfHist.child_trf_hist_unit = "piece";
            uChildTrfHist.child_trf_hist_result = "Passed";
            uChildTrfHist.child_trf_hist_id = indexNo;

            //Inserting Data into Database
            bool success = dalChildTrf.Insert(uChildTrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new child transfer record");
            }
        }

        private bool childStockIn(string factoryName, string parentItemCode, float qty, int indexNo, string unit)
        {
            bool success = true;

            string childItemCode;
            DataTable dtJoin = dalJoin.loadChildList(parentItemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    float childQty = qty;
                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);
                    childQty = childQty * Convert.ToSingle(Join["join_qty"].ToString());
                    if (dtItem.Rows.Count > 0)
                    {
                        if (!stockIn(factoryName, childItemCode, childQty, unit))
                        {
                            success = false;
                        }
                        deleteChildTransferRecord(indexNo, childItemCode);
                        refreshDataList(childItemCode);
                    }
                }
            }

            return success;
        }

        private bool childStockOut(string factoryName, string parentItemCode, float qty, int indexNo, string unit)
        {
            bool success = true;

            string childItemCode;
            DataTable dtJoin = dalJoin.loadChildList(parentItemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    float childQty = qty;
                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);
                    childQty = childQty * Convert.ToSingle(Join["join_qty"].ToString());

                    if (dtItem.Rows.Count > 0)
                    {
                        if (!stockOut(factoryName, childItemCode, childQty, unit))
                        {
                            success = false;
                        }
                        childTransferRecord(factoryName, indexNo, childItemCode, childQty);
                        refreshDataList(childItemCode);
                    }
                }
            }

            return success;
        }


        #endregion

        private void dgvTrf_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = dgvTrf.CurrentCell.RowIndex;

            string itemCode = dgvTrf.Rows[rowIndex].Cells["trf_hist_item_code"].Value.ToString();

            loadStockList(itemCode);
            calTotalStock(itemCode);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            ////itemSearch();
            //loadItemList();
            //loadTransferList();
            //Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbTransHistDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formLoaded)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvItem;
                int rowIndex;
                if (dgv.CurrentCell != null)
                {
                    rowIndex = dgv.CurrentCell.RowIndex;
                }
                else
                {
                    rowIndex = -1;
                }

                if (rowIndex > 0)
                {
                    editingItemCode = dgv.Rows[rowIndex].Cells["item_code"].Value == null ? "" : dgv.Rows[rowIndex].Cells["item_code"].Value.ToString();

                    if (editingItemCat == null || editingItemName == null || editingItemCode == null)
                    {
                        MessageBox.Show("empty value after selected");
                        dgvFactoryStock.DataSource = null;
                        dgvTotal.DataSource = null;
                    }
                    else
                    {
                        loadTransferList(editingItemCode);
                    }
                }
                else
                {
                    loadTransferList();
                }

                if (cmbTransHistDate.Text.Equals(All))
                {

                    lastPastDay = -1;
                }
                else if (Convert.ToInt32(cmbTransHistDate.Text) != lastPastDay)
                {

                    lastPastDay = Convert.ToInt32(cmbTransHistDate.Text);
                }
                ActiveControl = label1;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                dgvItem.ClearSelection();

                txtSearch.Text = "Search";
                txtSearch.ForeColor = SystemColors.GrayText;



                refreshDataList();

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

        private void frmInOut_Shown(object sender, EventArgs e)
        {

            dgvItem.ClearSelection();
            dgvTrf.ClearSelection();
            formLoaded = true;
            btn.Location = new Point(txtSearch.ClientSize.Width - btn.Width, (txtSearch.ClientSize.Height - btn.Height) / 2);

            if (MainDashboard.myconnstrng == text.DB_Semenyih || MainDashboard.myconnstrng == text.DB_JunPC)//|| myconnstrng == text.DB_JunPC
            {

            }
            else
            {

                frmMaterialAlertNotice frm2 = new frmMaterialAlertNotice();
                //frm2.StartPosition = FormStartPosition.CenterScreen;
                //frm2.Show();

                // Check if frm2 exists and is not disposed
                if (frm2 != null && !frm2.IsDisposed)
                {
                    // The form exists and is not disposed, so just show it
                    frm2.StartPosition = FormStartPosition.CenterScreen;
                    frm2.Show();
                }
               
            }
        }

        private void dgvTrf_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //listPaint(dgvTrf);
        }

        private void dgvTrf_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView dgv = dgvTrf;
            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            string itemCode = null;


            if (dgv.Columns[col].Name == daltrfHist.TrfItemCode)
            {
                itemCode = dgv.Rows[row].Cells[daltrfHist.TrfItemCode].Value.ToString();

                if (tool.ifGotChild(itemCode, dtJoin))
                {
                    if (dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                    {
                        //dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[row].Cells[text.Header_ItemDescription].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };

                    }
                    else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                    {
                        //dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[row].Cells[text.Header_ItemDescription].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };

                    }
                    else
                    {
                        //dgv.Rows[n].Cells[daltrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[row].Cells[text.Header_ItemDescription].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }

                    if (itemCode.Substring(0, 3) == text.Inspection_Pass)
                    {
                        dgv.Rows[row].Cells[text.Header_ItemDescription].Style = new DataGridViewCellStyle { ForeColor = Color.Peru, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }
                }
            }
            else if (dgv.Columns[col].Name == daltrfHist.TrfResult)
            {
                if (dgv.Rows[row].Cells[daltrfHist.TrfResult].Value != null)
                {
                    if (dgv.Rows[row].Cells[daltrfHist.TrfResult].Value.ToString().Equals("Undo"))
                    {
                        dgv.Rows[row].DefaultCellStyle.ForeColor = Color.Red;

                        dgv.Rows[row].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Strikeout);
                        dgv.Rows[row].Cells[text.Header_ItemDescription].Style = new DataGridViewCellStyle { ForeColor = Color.Red, Font = new Font("Segoe UI", 7F, FontStyle.Strikeout) };

                    }
                    else if (dgv.Rows[row].Cells[daltrfHist.TrfResult].Value.ToString().Equals("Failed"))
                    {
                        dgv.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                        //dgv.Rows[n].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
                    }
                    else
                    {
                        //dgv.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
                        //dgv.Rows[row].DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);

                        string from = dgv.Rows[row].Cells[daltrfHist.TrfFrom].Value.ToString();
                        string to = dgv.Rows[row].Cells[daltrfHist.TrfTo].Value.ToString();

                        if (CheckIfTrfOutToCustomer(from, to))
                        {
                            dgv.Rows[row].Cells[daltrfHist.TrfQty].Style.BackColor = Color.FromArgb(255, 118, 117);
                        }
                        else
                        {
                            dgv.Rows[row].Cells[daltrfHist.TrfQty].Style.BackColor = dgv.Rows[row].Cells[daltrfHist.TrfItemCode].Style.BackColor;
                        }

                    }
                }
            }
            else if (dgv.Columns[col].Name == daltrfHist.TrfQty)
            {
                double d = Convert.ToDouble(dgv.Rows[row].Cells[daltrfHist.TrfQty].Value);
                if ((d % 1) > 0)
                {
                    //is decimal
                    //d = Math.Round(d,2);

                    //DataTable dt = (DataTable)dgv.DataSource;

                    //dt.Rows[n][e.ColumnIndex] = Convert.ToSingle(dt.Rows[n][e.ColumnIndex]).ToString("F2");



                    //dt.Rows[n][e.ColumnIndex] = Convert.ToSingle(d);
                }
            }

            string colName = dgv.Columns[col].Name;
            //if (colName == daltrfHist.TrfItemCode || colName == daltrfHist.TrfAddedDate || colName == daltrfHist.TrfDate || colName == daltrfHist.TrfID || colName == daltrfHist.TrfResult || colName == daltrfHist.TrfNote)
            //{


            //}

            //dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.ResumeLayout();
        }

        private void dgvItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvItem;
            dgv.SuspendLayout();
            int n = e.RowIndex;
            string itemCode = null;

            if (dgv.Columns[e.ColumnIndex].Name == dalItem.ItemCode)
            {
                if (dgv.Rows[n].Cells[dalItem.ItemCode].Value != null)
                {
                    itemCode = dgv.Rows[n].Cells[dalItem.ItemCode].Value.ToString();
                }

                if (ifGotChild(itemCode))
                {
                    if (dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                    {
                        //dgv.Rows[n].Cells[dalItem.ItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }
                    else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                    {
                        //dgv.Rows[n].Cells[dalItem.ItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }
                    else if (dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))
                    {
                        //dgv.Rows[n].Cells[dalItem.ItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }

                    if (itemCode.Length > 3)
                    {
                        string test = itemCode.Substring(0, 3);
                        if (itemCode.Substring(0, 3) == text.Inspection_Pass)
                        {
                            dgv.Rows[n].Cells[dalItem.ItemName].Style = new DataGridViewCellStyle { ForeColor = Color.Peru, Font = new Font(dgv.Font, FontStyle.Underline) };
                        }
                    }

                }
            }

            else if (dgv.Columns[e.ColumnIndex].Name == dalItem.ItemStock)
            {
                float qty = 0;

                if (dgv.Rows[n].Cells[dalItem.ItemStock] != null)
                {
                    float.TryParse(dgv.Rows[n].Cells[dalItem.ItemStock].Value.ToString(), out (qty));
                }

                if (qty < 0)
                {
                    dgv.Rows[n].Cells[dalItem.ItemStock].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                }
                else
                {
                    dgv.Rows[n].Cells[dalItem.ItemStock].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                }
            }

            dgv.ResumeLayout();
        }

        private void MyButtonHandler(object sender, EventArgs e)
        {


            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                dgvItem.ClearSelection();
                txtSearch.Clear();
                refreshDataList();

            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                btn.Visible = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            btn.Visible = false;
            btn.Click += new EventHandler(MyButtonHandler);
            btn.Size = new Size(txtSearch.ClientSize.Height - 10, txtSearch.ClientSize.Height - 10);
            btn.Location = new Point(txtSearch.ClientSize.Width - btn.Width, (txtSearch.ClientSize.Height - btn.Height) / 2);
            btn.Cursor = Cursors.Default;
            btn.BackgroundImage = Properties.Resources.icons8_delete_filled_500;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            txtSearch.Controls.Add(btn);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(txtSearch.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            base.OnLoad(e);
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (formLoaded && itemListLoaded)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                dgvTrf.ClearSelection();
                DataGridView dgv = dgvItem;
                int rowIndex;
                if (dgv.CurrentCell != null)
                {
                    rowIndex = dgv.CurrentCell.RowIndex;
                }
                else
                {
                    rowIndex = -1;
                }


                if (rowIndex >= 0)
                {
                    editingItemCat = dgv.Rows[rowIndex].Cells[dalItem.ItemCat].Value == null ? "" : dgv.Rows[rowIndex].Cells[dalItem.ItemCat].Value.ToString();
                    editingItemName = dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value == null ? "" : dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value.ToString();
                    editingItemCode = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value == null ? "" : dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();

                    if (editingItemCat == null || editingItemName == null || editingItemCode == null)
                    {
                        MessageBox.Show("empty value after selected");
                        dgvFactoryStock.DataSource = null;
                        dgvTotal.DataSource = null;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[dalItem.ItemStock].Value = dalItem.getStockQty(editingItemCode).ToString("0.00#");
                        dgv.Rows[rowIndex].Cells[dalItem.ItemOrd].Value = dalItem.getOrderQty(editingItemCode);

                        loadStockList(editingItemCode);
                        calTotalStock(editingItemCode);
                        loadTransferList(editingItemCode);
                    }
                }
                else
                {
                    resetSaveData();
                    dgvFactoryStock.DataSource = null;
                    dgvTotal.DataSource = null;
                }
                Cursor = Cursors.Arrow; // change cursor to normal type 
            }
        }

        private void lblUpdatedTime_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnTrfHistSearch_Click(object sender, EventArgs e)
        {
            frmTransferHistory frm = new frmTransferHistory
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            formLoaded = false;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (string.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text == "Search")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }

            loadItemList();
            loadTransferList();


            formLoaded = true;
            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void JumpToSelectedTrfIDRow()
        {


            int searchingID = int.TryParse(txtJumpID.Text, out searchingID) ? searchingID : -1;

            if (searchingID > 0)
            {
                bool IDFound = false;
                DataTable dt = (DataTable)dgvTrf.DataSource;


                foreach (DataGridViewRow row in dgvTrf.Rows)
                {
                    string listID = row.Cells[daltrfHist.TrfID].Value.ToString();

                    if (listID == searchingID.ToString())
                    {
                        //dgvTrf.FirstDisplayedScrollingRowIndex = dgvTrf.SelectedRows[i].Index;

                        int rowIndex = row.Index;
                        dgvTrf.CurrentCell = dgvTrf.Rows[rowIndex].Cells[daltrfHist.TrfID];

                        IDFound = true;
                    }
                }


                if (!IDFound)
                {
                    MessageBox.Show("ID not found!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JumpToSelectedTrfIDRow();
        }

        private void txtJumpID_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                JumpToSelectedTrfIDRow();
            }
        }

        private void txtJumpID_Enter(object sender, EventArgs e)
        {
            if (txtJumpID.Text == "transfer id")
            {
                txtJumpID.Text = "";
                txtJumpID.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtJumpID_Leave(object sender, EventArgs e)
        {
            if (txtJumpID.Text.Length == 0)
            {
                txtJumpID.Text = "transfer id";
                txtJumpID.ForeColor = SystemColors.GrayText;
            }
        }

        private void dgvItem_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvItem;

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && userPermission >= MainDashboard.ACTION_LVL_TWO)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();

                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.Focus();
                    int rowIndex = dgv.CurrentCell.RowIndex;

                    string itemCode = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();
                    string itemName = dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value.ToString();

                    if (itemName.Contains(text.Terminated) || itemName.Contains(text.Terminated.ToUpper()))
                    {
                        my_menu.Items.Add(text.Activate).Name = text.Activate;
                    }
                    else
                    {
                        my_menu.Items.Add(text.Terminated).Name = text.Terminated;
                    }

                    my_menu.Items.Add(text.TransferHistory).Name = text.TransferHistory;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(Item_my_menu_ItemClicked);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private bool ItemTermination(string itemCode, string itemName)
        {
            bool result = false;
            uItem.item_code = itemCode;
            uItem.item_name = "(" + text.Terminated.ToUpper() + ")" + itemName;

            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

            result = dalItem.ItemNameUpdate(uItem);
            if (!result)
            {
                MessageBox.Show("Failed to terminated item");
            }

            return result;
        }

        private bool ItemActivation(string itemCode, string itemName)
        {
            bool result = false;

            if (itemName.Contains("(" + text.Terminated.ToUpper() + ")"))
            {
                itemName = itemName.Replace("(" + text.Terminated.ToUpper() + ")", "");

                uItem.item_code = itemCode;
                uItem.item_name = itemName;

                uItem.item_updtd_date = DateTime.Now;
                uItem.item_updtd_by = MainDashboard.USER_ID;

                result = dalItem.ItemNameUpdate(uItem);

                if (!result)
                {
                    MessageBox.Show("Failed to activate item");
                }

            }
            else
            {
                MessageBox.Show("Not terminated item!");
            }

            return result;
        }

        private void Item_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                             //MessageBox.Show(e.ClickedItem.Name.ToString());

                DataGridView dgv = dgvItem;

                int rowIndex = dgvItem.CurrentCell.RowIndex;

                string itemCode = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();
                string itemName = dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value.ToString();

                string clickedItem = e.ClickedItem.Name.ToString();

                if (dgv.SelectedRows.Count >= 0 && rowIndex >= 0)
                {
                    if (clickedItem.Equals(text.Terminated))
                    {
                        if (ItemTermination(itemCode, itemName))
                        {
                            dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value = "(" + text.Terminated.ToUpper() + ")" + itemName;
                        }
                    }
                    else if (clickedItem.Equals(text.Activate))
                    {
                        if (ItemActivation(itemCode, itemName))
                        {
                            dgv.Rows[rowIndex].Cells[dalItem.ItemName].Value = itemName.Replace("(" + text.Terminated.ToUpper() + ")", "");
                        }
                    }
                    else if (clickedItem.Equals(text.TransferHistory))
                    {
                        frmTransferHistory frm = new frmTransferHistory(itemCode)
                        {
                            StartPosition = FormStartPosition.CenterScreen
                        };

                        frm.Show();
                    }

                }

                //listPaintAndKeepSelected(dgvItem);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void cbPlanDetail_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmLoading.ShowLoadingScreen();

            LoadPlanItem();

            frmLoading.CloseForm();

        }
    }
}

