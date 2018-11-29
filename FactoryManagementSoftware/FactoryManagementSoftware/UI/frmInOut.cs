using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOut : Form
    { 
        public frmInOut()
        {
            InitializeComponent();
        }

        #region variable declare

        static public string editingItemCat;
        static public string editingItemCode;
        static public string editingItemName;
        static public string editingFacName;
        static public int editingIndexNo = -1;

        #endregion

        #region create class object (database)

        facDAL dalFac = new facDAL();

        itemDAL dalItem = new itemDAL();

        itemCatDAL dalItemCat = new itemCatDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockDAL dalStock = new facStockDAL();
        
        joinDAL dalJoin = new joinDAL();

        childTrfHistDAL dalChildTrf = new childTrfHistDAL();
        childTrfHistBLL uChildTrfHist = new childTrfHistBLL();

        #endregion

        #region Load or Reset Form

        private void frmInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.inOutFormOpen = false;
        }

        private void frmInOut_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbSearchCat.DataSource = distinctTable;
            cmbSearchCat.DisplayMember = "item_cat_name";
            cmbSearchCat.SelectedIndex = 0;
        }

        private void resetSaveData()
        {
            editingItemCat = "";
            editingItemName = "";
            editingItemCode = "";
        }

        private void refreshDataList()//refresh/update stock qty and order qty
        {
            if (dgvItem.SelectedRows.Count > 0)//if item data selected,direct update item stock qty and order qty
            {
                int rowindex = dgvItem.CurrentCell.RowIndex;
                int columnindex = dgvItem.CurrentCell.ColumnIndex;
                string itemCode = dgvItem.Rows[rowindex].Cells["item_code"].Value.ToString();
                dgvItem.Rows[rowindex].Cells["item_qty"].Value = dalItem.getStockQty(itemCode).ToString("0.00");
                dgvItem.Rows[rowindex].Cells["item_ord"].Value = dalItem.getOrderQty(itemCode);
                loadStockList(itemCode);
                calTotalStock(itemCode);
            }
            else//if item data not selected, then search in item datagridview and update stock qty and order qty
            {
                foreach (DataGridViewRow row in dgvItem.Rows)
                {
                    int n = row.Index;

                    if (editingItemCode == dgvItem.Rows[n].Cells["item_code"].Value.ToString())
                    {
                        dgvItem.Rows[n].Cells["item_qty"].Value = dalItem.getStockQty(editingItemCode).ToString("0.00");
                        dgvItem.Rows[n].Cells["item_ord"].Value = dalItem.getOrderQty(editingItemCode);
                    }
                }
                //clear factory and total list since no item data is selected
                dgvFactoryStock.Rows.Clear();
                dgvTotal.Rows.Clear();
                resetSaveData();
            }
        }

        private void refreshDataList(string itemCode)
        {         
            foreach (DataGridViewRow row in dgvItem.Rows)
            {
                int n = row.Index;

                if (itemCode == dgvItem.Rows[n].Cells["item_code"].Value.ToString())
                {
                    dgvItem.Rows[n].Cells["item_qty"].Value = dalItem.getStockQty(itemCode).ToString("0.00");
                    dgvItem.Rows[n].Cells["item_ord"].Value = dalItem.getOrderQty(itemCode);
                }

            }
        }

        private void resetForm()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            resetSaveData();
            loadItemCategoryData();
            loadItemList();
            loadTransferList();
            txtSearch.Text = null;
            dgvFactoryStock.Rows.Clear();
            dgvTotal.Rows.Clear();
            
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void listPaint(DataGridView dgv)
        {
            //bool rowColorChange = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            //dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20,25,72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;                
                string itemCode = "";
                if (dgv == dgvItem)
                {
                    itemCode = dgv.Rows[n].Cells["item_code"].Value.ToString();
                    float qty = 0;

                    if (dgv.Rows[n].Cells["item_qty"] != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells["item_qty"].Value.ToString(), out (qty));
                    }
                    if (ifGotChild(itemCode))
                    {
                        dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }
                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells["item_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                    else
                    {
                        dgv.Rows[n].Cells["item_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                    }
                }
                else if (dgv == dgvTrf)
                {
                    itemCode = dgv.Rows[n].Cells["trf_hist_item_code"].Value.ToString();
                    if (ifGotChild(itemCode))
                    {
                        dgv.Rows[n].Cells["trf_hist_item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells["trf_hist_item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
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

        private void listPaintAndKeepSelected(DataGridView dgv)
        {
            //bool rowColorChange = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            //dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                string itemCode = "";
                if (dgv == dgvItem)
                {
                    itemCode = dgv.Rows[n].Cells["item_code"].Value.ToString();
                    float qty = 0;

                    if (dgv.Rows[n].Cells["item_qty"] != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells["item_qty"].Value.ToString(), out (qty));
                    }
                    if (ifGotChild(itemCode))
                    {
                        dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }
                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells["item_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                    else
                    {
                        dgv.Rows[n].Cells["item_qty"].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                    }
                }
                else if (dgv == dgvTrf)
                {
                    itemCode = dgv.Rows[n].Cells["trf_hist_item_code"].Value.ToString();
                    if (ifGotChild(itemCode))
                    {
                        dgv.Rows[n].Cells["trf_hist_item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells["trf_hist_item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
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
        }

        private void loadStockList(string itemCode)
        {
            DataTable dt = dalStock.Select(itemCode);

            dgvFactoryStock.Rows.Clear();

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    int n = dgvFactoryStock.Rows.Add();
                    dgvFactoryStock.Rows[n].Cells["fac_name"].Value = stock["fac_name"].ToString();
                    dgvFactoryStock.Rows[n].Cells["stock_qty"].Value = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");

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

        private void calTotalStock(string itemCode)
        {
            float totalStock = 0;

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                if(stock["stock_qty"] == null)
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
            dgvTotal.Rows[0].Cells["Total"].Value = totalStock.ToString("0.00");
            dgvTotal.ClearSelection();

        
        }

        private void loadItemList()
        {
            DataTable dtItem;

            if(string.IsNullOrEmpty(cmbSearchCat.Text) || cmbSearchCat.Text.Equals("All"))
            {
                //show all item from the database
                dtItem = dalItem.Select();
            }
            else
            {
                dtItem = dalItem.catSearch(cmbSearchCat.Text);
            }
           
            dgvItem.Rows.Clear();
            foreach (DataRow item in dtItem.Rows)
            {
                int n = dgvItem.Rows.Add();
                dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvItem.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(item["item_qty"]).ToString("0.00");
                dgvItem.Rows[n].Cells["item_ord"].Value = Convert.ToSingle(item["item_ord"]);
            }

            listPaint(dgvItem);
            
        }

        private void itemSearch()
        {
            //get keyword from text box
            string keywords = txtSearch.Text;
            DataTable dt;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                if(string.IsNullOrEmpty(cmbSearchBy.Text))
                {
                    dt = dalItem.Search(keywords);//search item code and item name
                }
                else if(cmbSearchBy.Text.Equals("Item Code"))
                {
                    dt = dalItem.itemCodeSearch(keywords);//search item code only
                }
                else
                {
                    dt = dalItem.nameSearch(keywords);//search item name only
                }
                

                dgvItem.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    if(item["item_cat"].ToString().Equals(cmbSearchCat.Text))//show data under choosen category
                    {
                        int n = dgvItem.Rows.Add();
                        dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                        dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                        dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvItem.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(item["item_qty"]).ToString("0.00");
                        dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
                    }
                    else if(cmbSearchCat.Text.Equals("All") || string.IsNullOrEmpty(cmbSearchCat.Text))//show all data
                    {
                        int n = dgvItem.Rows.Add();
                        dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                        dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                        dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                        dgvItem.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(item["item_qty"]).ToString("0.00");
                        dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
                    }                 
                }
            }
            else
            {
                loadItemList();//if keyword = null
            }
            listPaint(dgvItem);
        }

        private void loadTransferList(string itemCode)
        {
            DataTable dt;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(itemCode))
            {
                dt = daltrfHist.codeSearch(itemCode);
            }
            else
            {
                //show all transfer records from the database
                dt = daltrfHist.Select();
            }

            dgvTrf.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "trf_hist_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();
                foreach (DataRow trf in sortedDt.Rows)
                {
                    int n = dgvTrf.Rows.Add();
                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["item_name"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_note"].Value = trf["trf_hist_note"].ToString();
                    dgvTrf.Rows[n].Cells["trf_result"].Value = trf["trf_result"].ToString();
                }
            }
            else
            {
                int n = dgvTrf.Rows.Add();
                dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = "NO TRANSFER RECORD UNDER THIS DATA";
            }
            listPaint(dgvTrf);
        }

        private void loadTransferList()
        {         
            DataTable dt;
            //get keyword from text box
            string keywords = txtSearch.Text;

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                if (string.IsNullOrEmpty(cmbSearchBy.Text))
                {
                    dt = daltrfHist.Search(keywords);
                }
                else if (cmbSearchBy.Text.Equals("Item Code"))
                {
                    dt = daltrfHist.codeSearch(keywords);
                }
                else
                {
                    dt = daltrfHist.nameSearch(keywords);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cmbSearchCat.Text) || cmbSearchCat.Text.Equals("All"))
                {
                    //show all item from the database
                    dt = daltrfHist.Select();

                }
                else
                {
                    dt = daltrfHist.catSearch(cmbSearchCat.Text);
                }   
            }

            dgvTrf.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "trf_hist_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                foreach (DataRow trf in sortedDt.Rows)
                {
                    int n = dgvTrf.Rows.Add();
                    dgvTrf.Rows[n].Cells["trf_hist_id"].Value = trf["trf_hist_id"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_date"].Value = trf["trf_hist_added_date"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_trf_date"].Value = Convert.ToDateTime(trf["trf_hist_trf_date"]).ToString("dd/MM/yyyy");
                    dgvTrf.Rows[n].Cells["trf_hist_item_code"].Value = trf["trf_hist_item_code"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_item_name"].Value = trf["item_name"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_from"].Value = trf["trf_hist_from"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_to"].Value = trf["trf_hist_to"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_qty"].Value = Convert.ToSingle(trf["trf_hist_qty"]).ToString("0.00");
                    dgvTrf.Rows[n].Cells["trf_hist_unit"].Value = trf["trf_hist_unit"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_added_by"].Value = trf["trf_hist_added_by"].ToString();
                    dgvTrf.Rows[n].Cells["trf_hist_note"].Value = trf["trf_hist_note"].ToString();
                    dgvTrf.Rows[n].Cells["trf_result"].Value = trf["trf_result"].ToString();
                }
                listPaint(dgvTrf);
            }
        } 

        #endregion

        #region get data/validation

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

            if(dtFac.Rows.Count > 0)
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
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region text/index changed or click

        private void cmbSearchCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadItemList();
            loadTransferList();
            resetSaveData();
            dgvFactoryStock.Rows.Clear();
            dgvTotal.Rows.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            itemSearch();
            loadTransferList();
            Cursor = Cursors.Arrow; // change cursor to normal type
            
        }

        private void dgvItem_Sorted(object sender, EventArgs e)
        {
            listPaint((DataGridView)sender);
        }

        private void dgvItem_SelectionChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            int rowIndex = dgvItem.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                editingItemCat = dgvItem.Rows[rowIndex].Cells["item_cat"].Value == null ? "" : dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
                editingItemName = dgvItem.Rows[rowIndex].Cells["item_name"].Value == null ? "" : dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
                editingItemCode = dgvItem.Rows[rowIndex].Cells["item_code"].Value == null ? "" : dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();

                if (editingItemCat == null || editingItemName == null || editingItemCode == null)
                {
                    MessageBox.Show("empty value after selected");
                    dgvFactoryStock.DataSource = null;
                    dgvTotal.DataSource = null;
                }
                else
                {
                    dgvItem.Rows[rowIndex].Cells["item_qty"].Value = dalItem.getStockQty(editingItemCode).ToString("0.00");
                    dgvItem.Rows[rowIndex].Cells["item_ord"].Value = dalItem.getOrderQty(editingItemCode);

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

        #endregion

        #region Function

        //transfer stock
        private void transfer_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            frmInOutEdit frm = new frmInOutEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(frmInOutEdit.updateSuccess)
            {
                //txtSearch.Text = editingItemCode; // update date list      
            }

            refreshDataList();
            listPaintAndKeepSelected(dgvItem);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        
        //unselect data when click on empty space
        private void frmInOut_MouseClick(object sender, MouseEventArgs e)
        {
            dgvItem.ClearSelection();
            loadTransferList();
            refreshDataList();
            resetSaveData();
            txtSearch.Clear();
        }

        //reset
        private void btnReset_Click(object sender, EventArgs e)
        {
            
                resetForm();
       
        }

        //show undo or redo menustrip
        private void dgvTrf_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();

                try
                {
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

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception)
                {

                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        //undo/redo function
        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show(e.ClickedItem.Name.ToString());
            int rowIndex = dgvTrf.CurrentCell.RowIndex;
            bool fromOrder = daltrfHist.ifFromOrder(Convert.ToInt32(dgvTrf.Rows[rowIndex].Cells["trf_hist_id"].Value.ToString()));

            if (dgvItem.SelectedRows.Count <= 0)
            {
                editingItemCode = dgvTrf.Rows[rowIndex].Cells["trf_hist_item_code"].Value.ToString();
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
            refreshDataList();
            listPaintAndKeepSelected(dgvItem);
            Cursor = Cursors.Arrow; // change cursor to normal type
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

            if (ifFactory(locationFrom))
            {
                result = stockIn(locationFrom, itemCode, qty, unit);

                if (ifFactory(locationTo))
                {
                    result = stockOut(locationFrom, itemCode, qty, unit);
                }
            }
            else if (ifFactory(locationTo) && !locationFrom.Equals("Assembly"))
            {
                result = stockOut(locationTo, itemCode, qty, unit);
            }
            else if (locationFrom.Equals("Assembly") && ifFactory(locationTo))
            {
                result = stockOut(locationTo, itemCode, qty, unit);
                result = childStockIn(locationTo, itemCode, qty, id, unit);
            }

            if (result)
            {
                changeTransferRecord("Undo", rowIndex, id);
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
                result = childStockOut(locationTo, itemCode, qty, id, unit);
            }

            if (result)
            {
                changeTransferRecord("Passed", rowIndex, id);
            }

            return result;
        }

        private bool stockIn(string factoryName, string itemCode, float qty, string unit)
        {
            bool successFacStockIn;
            bool successStockAdd;

            successFacStockIn = dalStock.facStockIn(getFactoryID(factoryName), itemCode, qty, unit);

            successStockAdd = dalItem.stockAdd(itemCode, qty.ToString());

            return successFacStockIn && successStockAdd;
        }

        private bool stockOut(string factoryName, string itemCode, float qty, string unit)
        {
            bool successFacStockOut = false;
            bool successStockSubtract = false;

            successFacStockOut = dalStock.facStockOut(getFactoryID(factoryName), itemCode, qty, unit);

            successStockSubtract = dalItem.stockSubtract(itemCode, qty.ToString());

            return successFacStockOut && successStockSubtract;
        }

        private void changeTransferRecord(string stockResult, int rowIndex, int id)
        {
            utrfHist.trf_hist_updated_date = DateTime.Now;
            utrfHist.trf_hist_updated_by = 0;
            utrfHist.trf_hist_id = id;
            utrfHist.trf_result = stockResult;

            //Inserting Data into Database
            bool success = daltrfHist.Update(utrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to change transfer record");
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
            DataTable dtJoin = dalJoin.parentCheck(parentItemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);

                    if (dtItem.Rows.Count > 0)
                    {
                        if (!stockIn(factoryName, childItemCode, qty, unit))
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
            DataTable dtJoin = dalJoin.parentCheck(parentItemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);

                    if (dtItem.Rows.Count > 0)
                    {
                        if (!stockOut(factoryName, childItemCode, qty, unit))
                        {
                            success = false;
                        }
                        childTransferRecord(factoryName, indexNo, childItemCode, qty);
                        refreshDataList(childItemCode);
                    }
                }
            }

            return success;
        }

        #endregion

        private void dgvItem_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvItem.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvItem.ClearSelection();
                loadTransferList();
                refreshDataList();
                resetSaveData();
                txtSearch.Clear();
            }
        }

        private void dgvFactoryStock_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvFactoryStock.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvItem.ClearSelection();
                loadTransferList();
                refreshDataList();
                resetSaveData();
                txtSearch.Clear();
            }
        }

        private void dgvItem_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 4 || e.Column.Index == 3)
            {
                e.SortResult = float.Parse(e.CellValue1.ToString()).CompareTo(float.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }
    }
}

