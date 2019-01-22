using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutEdit : Form
    {
        public frmInOutEdit()
        {
            InitializeComponent();
            createDGV();
        }
        static public bool updateSuccess = false;

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

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        materialBLL uMaterial = new materialBLL();
        materialDAL dalMaterial = new materialDAL();

        childTrfHistBLL uChildTrfHist = new childTrfHistBLL();
        childTrfHistDAL dalChildTrfHist = new childTrfHistDAL();

        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();

        #endregion

        #region Variable

        readonly string IndexColumnName = "NO";
        readonly string DateColumnName = "DATE";
        readonly string CatColumnName = "CATEGORY";
        readonly string CodeColumnName = "CODE";
        readonly string NameColumnName = "NAME";
        readonly string FromCatColumnName = "FROM(CAT)";
        readonly string FromColumnName = "FROM";
        readonly string ToCatColumnName = "TO(CAT)";
        readonly string ToColumnName = "TO";
        readonly string QtyColumnName = "QTY";
        readonly string UnitColumnName = "UNIT";
        readonly string NoteColumnName = "NOTE";

        private string date = "!";
        private string category = "!";
        private string itemCode = "!";
        private string fromCat = "!";
        private string from = "!";
        private string toCat = "!";
        private string to = "!";
        private string unit = "!";
        private float qty = -1;
        private string note = "!";

        private int index = 0;
        private int selectedRow = -1;
        private bool dgvEdit = false;

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        #endregion

        #region UI setting

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dgvTransfer.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Segoe UI", 12F, GraphicsUnit.Pixel);
            }
        }

        private void createDGV()
        {
            DataGridView dgv = dgvTransfer;

            dgv.Columns.Clear();

            tool.AddTextBoxColumns(dgv, IndexColumnName, IndexColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, DateColumnName, DateColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, CatColumnName, CatColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, CodeColumnName, CodeColumnName, Fill);
            tool.AddTextBoxColumns(dgv, NameColumnName, NameColumnName, Fill);
            tool.AddTextBoxColumns(dgv, FromCatColumnName, FromCatColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, FromColumnName, FromColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, ToCatColumnName, ToCatColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, ToColumnName, ToColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, QtyColumnName, QtyColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, UnitColumnName, UnitColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, NoteColumnName, NoteColumnName, DisplayedCells);

            dgv.Columns[QtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            UpdateFont();
        }

        #endregion

        #region validation

        private float unitCheck()
        {
            float qty = Convert.ToSingle(txtTrfQty.Text);
            
            //TO-DO

            return qty;
        }

        private float unitCheck(float qty, string unit)
        {
            if(unit.Equals("g") || unit.Equals("G"))
            {
                qty = qty / 1000;
            }

            return qty;
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbTrfItemCat.Text))
            {
                result = false;
                errorProvider1.SetError(cmbTrfItemCat, "Item Category Required");
            }

            if (string.IsNullOrEmpty(cmbTrfItemName.Text))
            {
                result = false;
                errorProvider2.SetError(cmbTrfItemName, "Item Name Required");
            }

            if (string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {
                result = false;
                errorProvider3.SetError(cmbTrfItemCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(cmbTrfFromCategory.Text))
            {
                result = false;
                errorProvider4.SetError(cmbTrfFromCategory, "Item From Location Required");
            }

            if (string.IsNullOrEmpty(cmbTrfToCategory.Text))
            {
                result = false;
                errorProvider5.SetError(cmbTrfToCategory, "Item To Location Required");
            }

            if (string.IsNullOrEmpty(txtTrfQty.Text))
            {
                result = false;
                errorProvider6.SetError(txtTrfQty, "Item Transfer Qty Required");
            }

            if (string.IsNullOrEmpty(cmbTrfQtyUnit.Text))
            {
                result = false;
                errorProvider7.SetError(cmbTrfQtyUnit, "Item Qty Unit Required");
            }

            return result;

        }

        private void txtTrfQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
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

        private bool IfExists(string itemCode, string factoryName)
        {

            DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private float checkQty(float qtyInOut)
        {
            if(qtyInOut < 0)
            {
                qtyInOut = 0;
            }

            string unit = cmbTrfQtyUnit.Text;

            if (unit.Equals("g"))
            {
                qtyInOut = qtyInOut / 1000;
            }
            else if (string.IsNullOrEmpty(unit))
            {
                qtyInOut = 0;
            }

            return qtyInOut;
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

        private bool ifGotChild()
        {
            bool result = false;

            if(!string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {
                DataTable dtJoin = dalJoin.parentCheck(cmbTrfItemCode.Text);
                if (dtJoin.Rows.Count > 0)
                {
                    result = true;
                }
            }
            
            return result;
        }

        #endregion

        #region data proccessing

        //private float getQty(string itemCode, string factoryName)
        //{
        //    float qty = 0;
        //    if (IfExists(itemCode, factoryName))
        //    {
        //        DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

        //        qty = Convert.ToSingle(dt.Rows[0]["stock_qty"].ToString());
        //    }
        //    else
        //    {
        //        qty = 0;
        //    }

        //    return qty;
        //}

        #endregion

        #region load data

        private void frmInOutEdit_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            loadItemCategoryData();

            if(!string.IsNullOrEmpty(frmInOut.editingItemCode))
            {
                cmbTrfItemCat.Text = frmInOut.editingItemCat;
                cmbTrfItemName.Text = frmInOut.editingItemName;
                cmbTrfItemCode.Text = frmInOut.editingItemCode;
            }
            else
            {
                cmbTrfItemCat.SelectedIndex = -1;
            }

            loadLocationCategoryData();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbTrfItemCat.DataSource = distinctTable;
            cmbTrfItemCat.DisplayMember = "item_cat_name";
        }

        private void loadItemNameData()
        {
            string keywords = cmbTrfItemCat.Text;

            if(!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.catSearch(keywords);
                DataTable dtItemName = dt.DefaultView.ToTable(true, "item_name");

                dtItemName.DefaultView.Sort = "item_name ASC";
                cmbTrfItemName.DataSource = dtItemName;
                cmbTrfItemName.DisplayMember = "item_name";
            }
            else
            {
                cmbTrfItemName.DataSource = null;
            }
        }

        private void loadItemCodeData()
        {
            string keywords = cmbTrfItemName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                dtItemCode.DefaultView.Sort = "item_code ASC";
                cmbTrfItemCode.DataSource = dtItemCode;
                cmbTrfItemCode.DisplayMember = "item_code";
            }
            else
            {
                cmbTrfItemCode.DataSource = null;
            }
        }

        private void loadLocationCategoryData()
        {
            DataTable dtlocationCat = daltrfCat.Select();

            DataTable fromCatTable = dtlocationCat.DefaultView.ToTable(true, "trf_cat_name");
            //fromCatTable.DefaultView.Sort = "trf_cat_name ASC";
            cmbTrfFromCategory.DataSource = fromCatTable;
            cmbTrfFromCategory.DisplayMember = "trf_cat_name";

            DataTable toCatTable = dtlocationCat.DefaultView.ToTable(true, "trf_cat_name");
            //toCatTable.DefaultView.Sort = "trf_cat_name ASC";
            cmbTrfToCategory.DataSource = toCatTable;
            cmbTrfToCategory.DisplayMember = "trf_cat_name";
        }

        private void loadLocationData(DataTable dt, ComboBox cmb, string columnName)
        {
            DataTable lacationTable = dt.DefaultView.ToTable(true, columnName);

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = columnName;
        }

        private void unitDataSource()
        {
            string itemCat = cmbTrfItemCat.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("item_unit");

            if (itemCat.Equals("RAW Material") || itemCat.Equals("Master Batch") || itemCat.Equals("Pigment"))
            {
                dt.Clear();
                dt.Rows.Add("kg");
                dt.Rows.Add("g");

            }
            else if (itemCat.Equals("Part") || itemCat.Equals("Carton"))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                cmbTrfQtyUnit.DataSource = dt;
            }
            else if (!string.IsNullOrEmpty(itemCat))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                dt.Rows.Add("kg");
                dt.Rows.Add("g");

            }
            else
            {
                dt = null;
            }

            cmbTrfQtyUnit.DataSource = dt;
            cmbTrfQtyUnit.DisplayMember = "item_unit";
            cmbTrfQtyUnit.SelectedIndex = -1;

        }

        #endregion

        #region transfer action

        private void supplierRecord(string supplierName, string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void productionRecord(string machineName, string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void customerRecord(string customerName, string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void materialUsedRecord(string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }
       
        private int transferRecord(string stockResult)
        {
            string locationFrom = string.IsNullOrEmpty(from) ? fromCat : from;
            string locationTo = string.IsNullOrEmpty(to) ? toCat : to;

            utrfHist.trf_hist_item_code = itemCode;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = qty;
            utrfHist.trf_hist_unit = unit;
            utrfHist.trf_hist_trf_date = Convert.ToDateTime(date);
            utrfHist.trf_hist_note = note;
            utrfHist.trf_hist_added_date = DateTime.Now;
            utrfHist.trf_hist_added_by = MainDashboard.USER_ID;
            utrfHist.trf_result = stockResult;
            utrfHist.trf_hist_from_order = 0;

            //Inserting Data into Database
            bool success = daltrfHist.Insert(utrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new transfer record");
                tool.historyRecord(text.System, "Failed to add new transfer record (InOutEdit)", utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
            }
            else
            {
                tool.historyRecord(text.Transfer, text.getTransferDetailString(daltrfHist.getIndexNo(utrfHist), utrfHist.trf_hist_qty, utrfHist.trf_hist_unit, utrfHist.trf_hist_item_code, locationFrom, locationTo), utrfHist.trf_hist_added_date, MainDashboard.USER_ID);

            }
            return daltrfHist.getIndexNo(utrfHist);
        }

        private void childTransferRecord(string stockResult, int indexNo, string itemCode,float qty)
        {
            string locationFrom = string.IsNullOrEmpty(from) ? fromCat : from;
            string locationTo = string.IsNullOrEmpty(to) ? toCat : to;

            uChildTrfHist.child_trf_hist_code = itemCode;
            uChildTrfHist.child_trf_hist_from = locationTo;
            uChildTrfHist.child_trf_hist_to = locationFrom;
            uChildTrfHist.child_trf_hist_qty = qty;
            uChildTrfHist.child_trf_hist_unit = "piece";
            
            uChildTrfHist.child_trf_hist_result = stockResult;
            uChildTrfHist.child_trf_hist_id = indexNo;

            //Inserting Data into Database
            bool success = dalChildTrfHist.Insert(uChildTrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new child transfer record");
            }
        }

        private bool stockIn(string factoryName, string itemCode, float qty, string sub_unit)
        {
            bool successFacStockIn;

            successFacStockIn = dalStock.facStockIn(getFactoryID(factoryName), itemCode, qty, sub_unit);

            return successFacStockIn ;
        }

        private bool stockOut(string factoryName, string itemCode, float qty, string sub_unit)
        {
            bool successFacStockOut;

            successFacStockOut = dalStock.facStockOut(getFactoryID(factoryName), itemCode, qty, sub_unit);

            return successFacStockOut ;
        }

        private bool childStockOut(string factoryName, string parentItemCode, float qty, int indexNo)
        {
            bool success = true;
            string stockResult = "Passed";
  
            string childItemCode;
            DataTable dtJoin = dalJoin.parentCheck(parentItemCode);
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
                        if(!stockOut(factoryName, childItemCode, childQty, "piece"))
                        {
                            stockResult = "Falied";
                            success = false;
                        }
                        childTransferRecord(stockResult, indexNo, childItemCode, childQty);
                    }
                }
            }

            return success;
        }
    
        private void transferAction()
        {
            string result = "Passed";

            //part in out
            if (category.Equals("Part"))
            {
                if (fromCat.Equals("Production") && toCat.Equals("Factory"))
                {
                    //factory stock in (part)
                    if (stockIn(to, itemCode, qty,unit))
                    {
                        productionRecord(from, to, itemCode, qty);
                    }
                    else
                    {
                        result = "Failed";
                    }
                    transferRecord(result);
                }
                else if (fromCat.Equals("Factory"))
                {
                    if (toCat.Equals("Factory"))
                    {
                        if (!(stockIn(to, itemCode, qty, unit) && stockOut(from, itemCode, qty, unit)))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                    else if (toCat.Equals("Customer"))
                    {
                        if (stockOut(from, itemCode, qty, unit))
                        {
                            customerRecord(to, from, itemCode, qty);
                        }
                        else
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                    else
                    {
                        if (!stockOut(from, itemCode, qty, unit))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                }
                else if (fromCat.Equals("Assembly") && toCat.Equals("Factory"))
                {

                    if (ifGotChild())
                    {
                        if (!stockIn(to, itemCode, qty, unit))
                        {
                            result = "Failed";
                        }
                        childStockOut(to, itemCode, qty, transferRecord(result));
                    }
                    else
                    {
                        MessageBox.Show("This item not a assembly part");
                        result = "Failed";
                    }
                }
                else if (toCat.Equals("Factory"))
                {
                    if (!stockIn(to, itemCode, qty, unit))
                    {
                        result = "Failed";
                    }
                    transferRecord(result);
                }
                else
                {
                    MessageBox.Show("no action under (part category)");
                }
            }

            //material in out
            else
            {
                if (fromCat.Equals("Supplier") && toCat.Equals("Factory"))
                {
                    if (stockIn(to, itemCode, qty, unit))
                    {
                        supplierRecord(from, to, itemCode, qty);
                    }
                    else
                    {
                        result = "Failed";
                    }
                    transferRecord(result);
                }
                else if (fromCat.Equals("Factory"))
                {
                    if (toCat.Equals("Factory"))
                    {
                        if (!(stockIn(to, itemCode, qty, unit) && stockOut(from, itemCode, qty, unit)))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                    else if (toCat.Equals("Production"))
                    {
                        if (stockOut(from, itemCode, qty, unit))
                        {
                            materialUsedRecord(from, itemCode, qty);
                        }
                        else
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                    else
                    {
                        if (!stockOut(from, itemCode, qty, unit))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                }
                else if (toCat.Equals("Factory"))
                {
                    if (!stockIn(to, itemCode, qty, unit))
                    {
                        result = "Failed";
                    }
                    transferRecord(result);
                }
                else
                {
                    MessageBox.Show("no action under (material category)");
                }
            }

            if (result.Equals("Passed"))
            {
                updateSuccess = true;
                frmInOut.editingItemCode = itemCode;
                this.Close();
            }
            else
            {
                updateSuccess = false;
            }
        }

        #endregion

        #region function:transfer/cancel

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //foreach row and make transfer action

                    foreach (DataGridViewRow c in dgvTransfer.Rows)
                    {
                        selectedRow = c.Index;
                        getData();
                        transferAction();
                    }
                    
                }
            }    

            Cursor = Cursors.Arrow; // change cursor to normal type 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Index/Text Changed

        private void cmbTrfItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            loadItemNameData();
            unitDataSource();
        }

        private void cmbTrfItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            loadItemCodeData();
        }

        private void cmbTrfItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
            if (ifGotChild())
            {
                cmbTrfQtyUnit.Text = "set";
            }
            else
            {
                cmbTrfQtyUnit.Text = "piece";
            }
        }

        private void cmbTrfFromCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();

            if(cmbTrfFromCategory.Text.Equals("Factory"))
            {
                DataTable dt = dalFac.Select();
                loadLocationData(dt, cmbTrfFrom, "fac_name");

            }
            else if (cmbTrfFromCategory.Text.Equals("Customer"))
            {
                DataTable dt = dalCust.Select();
                loadLocationData(dt, cmbTrfFrom, "cust_name");

            }
            else
            {
                cmbTrfFrom.DataSource = null;
            }
        }

        private void cmbTrfToCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
            if (cmbTrfToCategory.Text.Equals("Factory"))
            {
                DataTable dt = dalFac.Select();
                loadLocationData(dt, cmbTrfTo, "fac_name");

            }
            else if (cmbTrfToCategory.Text.Equals("Customer"))
            {
                DataTable dt = dalCust.Select();
                loadLocationData(dt, cmbTrfTo, "cust_name");

            }
            else
            {
                cmbTrfTo.DataSource = null;
            }
        }

        private void txtTrfQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
        }

        private void cmbTrfQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider7.Clear();
        }



        #endregion

        private void addToDGV()
        {
            DataGridView dgv = dgvTransfer;
            int n;
            if(dgvEdit)
            {
                n = selectedRow;
            }
            else
            {
                n = dgv.Rows.Add();
                index++;
                dgv.Rows[n].Cells[IndexColumnName].Value = index;
            }
            
            dgv.Rows[n].Cells[DateColumnName].Value = dtpTrfDate.Text;
            dgv.Rows[n].Cells[CatColumnName].Value = cmbTrfItemCat.Text;
            dgv.Rows[n].Cells[CodeColumnName].Value = cmbTrfItemCode.Text;
            dgv.Rows[n].Cells[NameColumnName].Value = cmbTrfItemName.Text;
            dgv.Rows[n].Cells[FromCatColumnName].Value = cmbTrfFromCategory.Text;
            dgv.Rows[n].Cells[FromColumnName].Value = cmbTrfFrom.Text;
            dgv.Rows[n].Cells[ToCatColumnName].Value = cmbTrfToCategory.Text;
            dgv.Rows[n].Cells[ToColumnName].Value = cmbTrfTo.Text;
            dgv.Rows[n].Cells[QtyColumnName].Value = txtTrfQty.Text;
            dgv.Rows[n].Cells[UnitColumnName].Value = cmbTrfQtyUnit.Text;
            dgv.Rows[n].Cells[NoteColumnName].Value = txtTrfNote.Text;

            dgv.ClearSelection();
            dgv.Rows[n].Selected = true;
        }
        
        private string returnFrom()
        {
            if (string.IsNullOrEmpty(cmbTrfFrom.Text))
            {
                return cmbTrfFromCategory.Text;
            }
            else
            {
                return cmbTrfFrom.Text;
            }
        }

        private string returnTo()
        {
            if (string.IsNullOrEmpty(cmbTrfTo.Text))
            {
                return cmbTrfToCategory.Text;
            }
            else
            {
                return cmbTrfTo.Text;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (Validation())
            {
                addToDGV();
                
                dgvEdit = false;
            }
            changeButton();
            Cursor = Cursors.Arrow; // change cursor to normal type 
        }

        private void loadFromDGV()
        {
            DataGridView dgv = dgvTransfer;

            DateTime curDate;

            if (DateTime.TryParse(dgv.Rows[selectedRow].Cells[DateColumnName].Value.ToString(), out curDate))
            {
                dtpTrfDate.Value = curDate;
            }

            cmbTrfItemCat.Text = dgv.Rows[selectedRow].Cells[CatColumnName].Value.ToString();
            cmbTrfItemName.Text = dgv.Rows[selectedRow].Cells[NameColumnName].Value.ToString();
            cmbTrfItemCode.Text = dgv.Rows[selectedRow].Cells[CodeColumnName].Value.ToString();

            cmbTrfFromCategory.Text = dgv.Rows[selectedRow].Cells[FromCatColumnName].Value.ToString();
            cmbTrfFrom.Text = dgv.Rows[selectedRow].Cells[FromColumnName].Value.ToString();
            cmbTrfToCategory.Text = dgv.Rows[selectedRow].Cells[ToCatColumnName].Value.ToString();
            cmbTrfTo.Text = dgv.Rows[selectedRow].Cells[ToColumnName].Value.ToString();

            txtTrfQty.Text = dgv.Rows[selectedRow].Cells[QtyColumnName].Value.ToString();
            cmbTrfQtyUnit.Text = dgv.Rows[selectedRow].Cells[UnitColumnName].Value.ToString();
            txtTrfNote.Text = dgv.Rows[selectedRow].Cells[NoteColumnName].Value.ToString();
        }

        private void changeButton()
        {
            if(dgvEdit)
            {
                btnEdit.Text = "EDIT";
                btnDelete.Show();
            }
            else
            {
                btnEdit.Text = "ADD";
                btnDelete.Hide();
            }
        }

        private void dgvTransfer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            //load from dgv
            loadFromDGV();
            dgvEdit = true;
            changeButton();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            dgvTransfer.Rows.RemoveAt(selectedRow);
            dgvEdit = false;
            changeButton();
            Cursor = Cursors.Arrow; // change cursor to normal type 
        }

        private void getData()
        {
            int n = selectedRow;
            DataGridView dgv = dgvTransfer;

            //DateTime curDate;

            //if (DateTime.TryParse(dgv.Rows[selectedRow].Cells[DateColumnName].Value.ToString(), out curDate))
            //{
            //    date = curDate;
            //}

            date = dgv.Rows[n].Cells[DateColumnName].Value.ToString();
            category = dgv.Rows[n].Cells[CatColumnName].Value.ToString();
            itemCode = dgv.Rows[n].Cells[CodeColumnName].Value.ToString();
            fromCat = dgv.Rows[n].Cells[FromCatColumnName].Value.ToString();
            from = dgv.Rows[n].Cells[FromColumnName].Value.ToString();
            toCat = dgv.Rows[n].Cells[ToCatColumnName].Value.ToString();
            to = dgv.Rows[n].Cells[ToColumnName].Value.ToString();
            unit = dgv.Rows[n].Cells[UnitColumnName].Value.ToString();
            qty = Convert.ToSingle(dgv.Rows[n].Cells[QtyColumnName].Value);
            note = dgv.Rows[n].Cells[ToColumnName].Value.ToString();

            qty = unitCheck(qty, unit);
            unit = checkUnit(unit);

            if (qty < 0)
            {
                qty = 0;
            }
        }
    }
}
