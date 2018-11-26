using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutEdit : Form
    {
        public frmInOutEdit()
        {
            InitializeComponent();
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


        #endregion

        #region validation

        private float unitCheck()
        {
            float qty = Convert.ToSingle(txtTrfQty.Text);
            
            //TO-DO

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
            DataTable dtJoin = dalJoin.parentCheck(cmbTrfItemCode.Text);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
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
            
            string locationFrom = string.IsNullOrEmpty(cmbTrfFrom.Text) ? cmbTrfFromCategory.Text: cmbTrfFrom.Text;

            string locationTo = string.IsNullOrEmpty(cmbTrfTo.Text) ? cmbTrfToCategory.Text : cmbTrfTo.Text;

            utrfHist.trf_hist_item_code = cmbTrfItemCode.Text;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = checkQty(Convert.ToSingle(txtTrfQty.Text));
            utrfHist.trf_hist_unit = checkUnit(cmbTrfQtyUnit.Text);
            utrfHist.trf_hist_trf_date = dtpTrfDate.Value.Date;
            utrfHist.trf_hist_note = txtTrfNote.Text;
            utrfHist.trf_hist_added_date = DateTime.Now;
            utrfHist.trf_hist_added_by = 0;
            utrfHist.trf_result = stockResult;
            utrfHist.trf_hist_from_order = 0;

            //Inserting Data into Database
            bool success = daltrfHist.Insert(utrfHist);
            if(!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new transfer record");
            }
            return daltrfHist.getIndexNo(utrfHist);
        }

        private void childTransferRecord(string stockResult, int indexNo, string itemCode)
        {

            string locationFrom = string.IsNullOrEmpty(cmbTrfFrom.Text)? cmbTrfFromCategory.Text : cmbTrfFrom.Text;

            string locationTo = string.IsNullOrEmpty(cmbTrfTo.Text) ? cmbTrfToCategory.Text : cmbTrfTo.Text;

            uChildTrfHist.child_trf_hist_code = itemCode;
            uChildTrfHist.child_trf_hist_from = locationTo;
            uChildTrfHist.child_trf_hist_to = locationFrom;
            uChildTrfHist.child_trf_hist_qty = checkQty(Convert.ToSingle(txtTrfQty.Text));
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

        private bool stockIn(string factoryName, string itemCode, float qty)
        {
            bool successFacStockIn;
            bool successStockAdd;
            qty = checkQty(qty);

            successFacStockIn = dalStock.facStockIn(getFactoryID(factoryName), itemCode, qty, cmbTrfQtyUnit.Text);

            successStockAdd = dalItem.stockAdd(itemCode, qty.ToString());

            return successFacStockIn && successStockAdd;
        }

        private bool stockOut(string factoryName, string itemCode, float qty)
        {
            bool successFacStockOut;
            bool successStockSubtract;
            qty = checkQty(qty);

            successFacStockOut = dalStock.facStockOut(getFactoryID(factoryName), itemCode, qty, cmbTrfQtyUnit.Text);

            successStockSubtract = dalItem.stockSubtract(itemCode, qty.ToString());

            return successFacStockOut && successStockSubtract;
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
                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);

                    if (dtItem.Rows.Count > 0)
                    {
                        if(!stockOut(factoryName, childItemCode, qty))
                        {
                            stockResult = "Falied";
                            success = false;
                        }
                        childTransferRecord(stockResult, indexNo, childItemCode);
                    }
                }
            }

            return success;
        }

        private void transferAction()
        {
            string category = cmbTrfItemCat.Text;
            string fromCat = cmbTrfFromCategory.Text;
            string from = cmbTrfFrom.Text;
            string toCat = cmbTrfToCategory.Text;
            string to = cmbTrfTo.Text;
            string itemCode = cmbTrfItemCode.Text;
            float qty = unitCheck();
            string result = "Passed";
            
            //part in out
            if(category.Equals("Part"))
            {
                if(fromCat.Equals("Production") && toCat.Equals("Factory"))
                {
                    //factory stock in (part)
                    if(stockIn(to,itemCode,qty))
                    {
                        productionRecord(from, to, itemCode, qty);
                    }
                    else
                    {
                        result = "Failed";
                    }
                    transferRecord(result);
                }
                else if(fromCat.Equals("Factory"))
                {
                    if(toCat.Equals("Factory"))
                    {
                        if (!(stockIn(to, itemCode, qty) && stockOut(from, itemCode, qty)))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                    else if(toCat.Equals("Customer"))
                    {
                        if (stockOut(from, itemCode, qty))
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
                        if (!stockOut(from, itemCode, qty))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                }
                else if(fromCat.Equals("Assembly") && toCat.Equals("Factory"))
                {

                    if (ifGotChild())
                    {
                        if (!stockIn(to, itemCode, qty))
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
                else if(toCat.Equals("Factory"))
                {
                    if (!stockIn(to, itemCode, qty))
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
                    if (stockIn(to, itemCode, qty))
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
                        if (!(stockIn(to, itemCode, qty) && stockOut(from, itemCode, qty)))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                    else if (toCat.Equals("Production"))
                    {
                        if (stockOut(from, itemCode, qty))
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
                        if (!stockOut(from, itemCode, qty))
                        {
                            result = "Failed";
                        }
                        transferRecord(result);
                    }
                }
                else if (toCat.Equals("Factory"))
                {
                    if (!stockIn(to, itemCode, qty))
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

            if(result.Equals("Passed"))
            {
                updateSuccess = true;
                frmInOut.editingItemCode = cmbTrfItemCode.Text;
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
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    transferAction();
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

       


    }
}
