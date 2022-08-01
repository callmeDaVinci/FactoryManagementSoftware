﻿using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Collections.Generic;
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
//#############################################################################################################################################
            dtpTrfDate.Value = DateTime.Today.AddDays(-1);
            //dtpTrfDate.Value = DateTime.Today.AddDays(-30);
            createDGV();
        }

        public frmInOutEdit(DataTable dt)
        {
            InitializeComponent();
            //#############################################################################################################################################
            dtpTrfDate.Value = DateTime.Today.AddDays(-1);
            //dtpTrfDate.Value = DateTime.Today.AddDays(-30);
            createDGV();

            dt_MatChecklist = dt;
            callFromMatChecklist = true;
        }

        //CALL FROM D/O LIST
        public frmInOutEdit(DataTable dt, DateTime date)
        {
            InitializeComponent();
            //#############################################################################################################################################
            //dtpTrfDate.Value = date;
            dtpTrfDate.Value = DateTime.Today.AddDays(-1);
            //dtpTrfDate.Value = DateTime.Today.AddDays(-30);
            createDGV();

            dt_DOItem = dt;
            delivered_date = date;
            callFromDOlist = true;
        }

        public frmInOutEdit(DataTable dt, bool _fromProductionRecord)
        {
            InitializeComponent();
            //#############################################################################################################################################
            dtpTrfDate.Value = DateTime.Today.AddDays(-1);
            //dtpTrfDate.Value = DateTime.Today.AddDays(-30);
            createDGV();

            dt_ProductionRecord = dt;
            callFromProductionRecord = true;
            callFromMatChecklist = false;
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

        matPlanDAL dalMatPlan = new matPlanDAL();

        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();

        SPPDataDAL dalData = new SPPDataDAL();
        SPPDataBLL uData = new SPPDataBLL();

        #endregion

        #region Variable

        readonly string headerPlanID = "PLAN ID";
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

        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerIndex = "#";
        readonly string headerFrom = "FROM";
        readonly string headerTo = "TO";
        readonly string headerQty = "QTY (KG/PIECE)";

        readonly private string header_Shift = "SHIFT";
        readonly string header_Cat = "Cat";
        readonly string header_ProDate = "DATE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_From = "FROM";
        readonly string header_To = "TO";
        readonly string header_Qty = "QTY";
        readonly private string header_PlanID = "PLAN ID";

        readonly string header_DOTblCode = "D/O TABLE CODE";
        readonly string header_DONo = "D/O #";
        readonly string header_DeliveryPCS = "DELIVERY PCS";
        readonly string header_DeliveryBAG = "DELIVERY BAG";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_Customer = "CUSTOMER";

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
        private string failedNote = "!";
        private int index = 0;
        private int selectedRow = -1;
        private bool dgvEdit = false;
        private bool callFromMatChecklist = false;
        private bool callFromDOlist = false;
        private bool callFromProductionRecord = false;
        private bool isInpectionItem = false;
        static public bool TrfSuccess = false;

        static public DataTable dt_MatChecklist;
        private DataTable dt_DOItem;
        static public DataTable dt_ProductionRecord;

        private DateTime delivered_date;

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
            tool.AddTextBoxColumns(dgv, CodeColumnName, CodeColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, NameColumnName, NameColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, FromCatColumnName, FromCatColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, FromColumnName, FromColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, ToCatColumnName, ToCatColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, ToColumnName, ToColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, QtyColumnName, QtyColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, UnitColumnName, UnitColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, NoteColumnName, NoteColumnName, DisplayedCells);

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[QtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[FromCatColumnName].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[FromColumnName].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns[ToCatColumnName].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.Columns[ToColumnName].DefaultCellStyle.BackColor = Color.LightSteelBlue;

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
                errorProvider1.SetError(lblCategoryError, "Item Category Required");
            }

            if (string.IsNullOrEmpty(cmbTrfItemName.Text))
            {
                result = false;
                errorProvider2.SetError(lblNameError, "Item Name Required");
            }

            if (string.IsNullOrEmpty(cmbTrfItemCode.Text))
            {
                result = false;
                errorProvider3.SetError(lblCodeError, "Item Code Required");
            }   

            if (string.IsNullOrEmpty(cmbTrfFromCategory.Text))
            {
                result = false;
                errorProvider4.SetError(lblLocationFromError, "Item From Location Required");
            }
            else if (cmbTrfFromCategory.Text.Equals(text.Factory) || cmbTrfFromCategory.Text.Equals(text.Customer))
            {
                if (string.IsNullOrEmpty(cmbTrfFrom.Text))
                {
                    result = false;
                    errorProvider5.SetError(lblLocationFromError, "Item From Location Required");
                }
            }

            if (string.IsNullOrEmpty(cmbTrfToCategory.Text))
            {
                result = false;
                errorProvider5.SetError(lblLocationToError, "Item To Location Required");
            }
            else if(cmbTrfToCategory.Text.Equals(text.Factory) || cmbTrfToCategory.Text.Equals(text.Customer))
            {
                if (string.IsNullOrEmpty(cmbTrfTo.Text))
                {
                    result = false;
                    errorProvider5.SetError(lblLocationToError, "Item To Location Required");
                }
            }

            if (string.IsNullOrEmpty(txtTrfQty.Text))
            {
                result = false;
                errorProvider6.SetError(lblQuantityError, "Item Transfer Qty Required");
            }

            if (string.IsNullOrEmpty(cmbTrfQtyUnit.Text))
            {
                result = false;
                errorProvider7.SetError(lblUnitError, "Item Qty Unit Required");
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
                DataTable dtJoin = dalJoin.loadChildList(cmbTrfItemCode.Text);
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

            txtTrfQty.Focus();

            if (callFromMatChecklist)
            {
                addMatToDGV(dt_MatChecklist);
            }
            else if (callFromProductionRecord)
            {
                addToDGV(dt_ProductionRecord,callFromProductionRecord);
            }
            else if(callFromDOlist)
            {
                addPartToDGV(dt_DOItem);
            }

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
                DataTable dt = dalItem.CatSearch(keywords);
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

            string itemCode = cmbTrfItemCode.Text;

            DataTable dt_Item = dalItem.codeSearch(itemCode);

            bool assembly = dalItem.checkIfAssembly(itemCode, dt_Item);
            bool production = dalItem.checkIfProduction(itemCode, dt_Item);

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (tool.ifGotChild(itemCode))
                {
                    if (itemCode.Substring(1, 2) == text.Inspection_Pass)
                    {
                        //cmbTrfFromCategory.Text = text.Inspection;
                        cmbTrfFromCategory.Text = text.Assembly;

                    }
                    else if(assembly && !production)
                    {
                        cmbTrfFromCategory.Text = text.Assembly;
                    }
                    else
                    {
                        cmbTrfFromCategory.Text = text.Production;
                    }
                }
                else if(dalItem.getCatName(itemCode).Equals(text.Cat_Part))
                {
                    cmbTrfFromCategory.Text = text.Production;
                }
                else
                {
                    cmbTrfFromCategory.Text = text.Factory;
                }
            }
            

            DataTable toCatTable = dtlocationCat.DefaultView.ToTable(true, "trf_cat_name");
            //toCatTable.DefaultView.Sort = "trf_cat_name ASC";
            cmbTrfToCategory.DataSource = toCatTable;
            cmbTrfToCategory.DisplayMember = "trf_cat_name";

            if (cmbTrfFromCategory.Text.Equals(text.Assembly))
            {
                cmbTrfToCategory.SelectedIndex = 0;

                if (cmbTrfItemCode.Text.Substring(1, 2) == text.Inspection_Pass)
                {
                    cmbTrfTo.Text = text.Factory_Store;
                    //cmbTrfTo.SelectedIndex = 5;
                }
              
                else
                {
                    cmbTrfTo.Text = text.Factory_9;
                }
            }
            else if(cmbTrfFromCategory.Text.Equals(text.Inspection))
            {
                cmbTrfToCategory.Text = text.Factory;
                cmbTrfTo.Text = text.Factory_Store;
                cmbTrfQtyUnit.Text = text.Unit_Piece;
            }

            //#############################################################################################################################################
            //cmbTrfFromCategory.Text = "Other";
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

            if (itemCat.Equals(text.Cat_RawMat) || itemCat.Equals(text.Cat_MB) || itemCat.Equals(text.Cat_Pigment))
            {
                dt.Clear();
                dt.Rows.Add(text.Unit_KG);
                dt.Rows.Add(text.Unit_g);

            }
            else if (itemCat.Equals(text.Cat_Part) || itemCat.Equals(text.Cat_Carton))
            {
                dt.Clear();
                dt.Rows.Add(text.Unit_Set);
                dt.Rows.Add(text.Unit_Piece);
                dt.Rows.Add(text.Unit_Meter);
                // cmbTrfQtyUnit.DataSource = dt;
            }
            else if (itemCat.Equals(text.Cat_PolyBag))
            {
                dt.Clear();
                dt.Rows.Add(text.Unit_KG);
            }
            else if (!string.IsNullOrEmpty(itemCat))
            {
                dt.Clear();
                dt.Rows.Add(text.Unit_Set);
                dt.Rows.Add(text.Unit_Piece);
                dt.Rows.Add(text.Unit_KG);
                dt.Rows.Add(text.Unit_g);
                dt.Rows.Add(text.Unit_Meter);
            }
            else
            {
                dt = null;
            }

            cmbTrfQtyUnit.DataSource = dt;
            cmbTrfQtyUnit.DisplayMember = "item_unit";
            cmbTrfQtyUnit.SelectedIndex = -1;

            if ( !string.IsNullOrEmpty(cmbTrfItemCode.Text) && cmbTrfItemCode.Text.Substring(1, 2) == text.Inspection_Pass)
            {
                cmbTrfQtyUnit.Text = "set";
            }

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
       
        private int TransferRecord(string stockResult)
        {
            string locationFrom = string.IsNullOrEmpty(from) ? fromCat : from;
            string locationTo = string.IsNullOrEmpty(to) ? toCat : to;

            utrfHist.trf_hist_item_code = itemCode;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = qty;
            utrfHist.trf_hist_unit = unit;
            utrfHist.trf_hist_trf_date = Convert.ToDateTime(date);
            utrfHist.trf_hist_note = "[" + dalUser.getUsername(MainDashboard.USER_ID) + "] " + note;
            utrfHist.trf_hist_added_date = DateTime.Now;
            utrfHist.trf_hist_added_by = MainDashboard.USER_ID;
            utrfHist.trf_result = stockResult;
            utrfHist.trf_hist_from_order = 0;

            if(stockResult == "Failed")
            {
                utrfHist.trf_hist_note = failedNote;
            }

            //Inserting Data into Database
            int trfID = daltrfHist.InsertAndGetPrimaryKey(utrfHist);
                     
            if (trfID == -1)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new transfer record");
                tool.historyRecord(text.System, "Failed to add new transfer record (InOutEdit)", utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
            }
            else
            {
                //tool.historyRecord(text.Transfer, text.getTransferDetailString(daltrfHist.getIndexNo(utrfHist), utrfHist.trf_hist_qty, utrfHist.trf_hist_unit, utrfHist.trf_hist_item_code, locationFrom, locationTo), utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
                tool.historyRecord(text.Transfer, text.getTransferDetailString(trfID, utrfHist.trf_hist_qty, utrfHist.trf_hist_unit, utrfHist.trf_hist_item_code, locationFrom, locationTo), utrfHist.trf_hist_added_date, MainDashboard.USER_ID);
            }
            return trfID;
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

            return successFacStockOut;
        }

        private bool productionChildStockOut(string factoryName, string parentItemCode, float qty, int indexNo)
        {
            bool success = true;
            int n;
            string childItemCode;
            DataTable dtJoin = dalJoin.loadChildList(parentItemCode);

            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    float childQty = 0;

                    float joinQty = float.TryParse(Join["join_qty"].ToString(), out float i) ? Convert.ToSingle(Join["join_qty"].ToString()) : 1;
                    int joinMax = int.TryParse(Join[dalJoin.JoinMax].ToString(), out int j) ? Convert.ToInt32(Join[dalJoin.JoinMax].ToString()) : 1;
                    int JoinMin = int.TryParse(Join[dalJoin.JoinMin].ToString(), out int k) ? Convert.ToInt32(Join[dalJoin.JoinMin].ToString()) : 1;

                    joinMax = joinMax <= 0 ? 1 : joinMax;
                    JoinMin = JoinMin <= 0 ? 1 : JoinMin;

                    int ParentQty = Convert.ToInt32(qty);

                    int fullQty = ParentQty / joinMax;

                    int notFullQty = ParentQty % joinMax;

                    childQty = fullQty * joinQty;

                    if (notFullQty >= JoinMin)
                    {
                        childQty += joinQty;

                    }
                    //childQty = childQty * Convert.ToSingle(Join["join_qty"].ToString());
                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);
                    //childQty = childQty * Convert.ToSingle(Join["join_qty"].ToString());

                    if (dtItem.Rows.Count > 0)
                    {
                        DataGridView dgv = dgvTransfer;

                        string childItemCat = dtItem.Rows[0][dalItem.ItemCat].ToString();

                        if(!callFromProductionRecord || (childItemCat != text.Cat_Carton && childItemCat != text.Cat_Packaging))
                        {
                            if (dalItem.checkIfAssembly(childItemCode) && dalItem.checkIfProduction(childItemCode))
                            {
                                if (dalItem.checkIfAssembly(parentItemCode) && dalItem.checkIfProduction(parentItemCode))
                                {
                                    n = dgv.Rows.Add();
                                    index++;

                                    dgv.Rows[n].Cells[IndexColumnName].Value = index;
                                    dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.FromArgb(0, 192, 0);
                                    dgv.Rows[n].Cells[DateColumnName].Value = dtpTrfDate.Text;
                                    dgv.Rows[n].Cells[CatColumnName].Value = dtItem.Rows[0][dalItem.ItemCat].ToString();
                                    dgv.Rows[n].Cells[CodeColumnName].Value = childItemCode;
                                    dgv.Rows[n].Cells[NameColumnName].Value = dtItem.Rows[0][dalItem.ItemName].ToString();
                                    dgv.Rows[n].Cells[FromCatColumnName].Value = "Production";
                                    dgv.Rows[n].Cells[FromColumnName].Value = "";
                                    dgv.Rows[n].Cells[ToCatColumnName].Value = "Factory";
                                    dgv.Rows[n].Cells[ToColumnName].Value = factoryName;
                                    dgv.Rows[n].Cells[QtyColumnName].Value = childQty;
                                    dgv.Rows[n].Cells[UnitColumnName].Value = "piece";
                                    dgv.Rows[n].Cells[NoteColumnName].Value = "Production Sub Part in";
                                }

                            }

                            n = dgv.Rows.Add();
                            index++;

                            dgv.Rows[n].Cells[IndexColumnName].Value = index;
                            dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
                            dgv.Rows[n].Cells[DateColumnName].Value = dtpTrfDate.Text;
                            dgv.Rows[n].Cells[CatColumnName].Value = dtItem.Rows[0][dalItem.ItemCat].ToString();
                            dgv.Rows[n].Cells[CodeColumnName].Value = childItemCode;
                            dgv.Rows[n].Cells[NameColumnName].Value = dtItem.Rows[0][dalItem.ItemName].ToString();
                            dgv.Rows[n].Cells[FromCatColumnName].Value = "Factory";
                            dgv.Rows[n].Cells[FromColumnName].Value = factoryName;
                            dgv.Rows[n].Cells[ToCatColumnName].Value = "Production";
                            dgv.Rows[n].Cells[ToColumnName].Value = "";
                            dgv.Rows[n].Cells[QtyColumnName].Value = childQty;
                            dgv.Rows[n].Cells[UnitColumnName].Value = "piece";
                            dgv.Rows[n].Cells[NoteColumnName].Value = "Production Sub Part out";

                            if (childItemCode.Equals("V76KM4000 0.360"))
                            {
                                //dgv.Rows[n].Cells[QtyColumnName].Value = childQty * 0.36;
                                dgv.Rows[n].Cells[UnitColumnName].Value = "meter";
                            }

                            facStockDAL dalFacStock = new facStockDAL();
                            float facStock = dalFacStock.getQty(childItemCode, tool.getFactoryID(factoryName).ToString());

                            float transferQty = 0;
                            if (callFromProductionRecord)
                            {
                                transferQty = qty;
                            }
                            else
                            {
                                transferQty = Convert.ToSingle(txtTrfQty.Text);
                            }

                            if (facStock - transferQty < 0)
                            {
                                dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                                dgv.Rows[n].Cells[NoteColumnName].Value = "AFTER BAL=" + (facStock - transferQty);
                            }
                        }
                      
                    }
                }
            }

            return success;
        }

        private void addSubCartonToDGV(string factoryName, string ItemCode, int qty)
        {
            DataGridView dgv = dgvTransfer;
            int n = dgv.Rows.Add();
            index++;

            dgv.Rows[n].Cells[IndexColumnName].Value = index;
            dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
            dgv.Rows[n].Cells[DateColumnName].Value = dtpTrfDate.Text;
            dgv.Rows[n].Cells[CatColumnName].Value = dalItem.getCatName(ItemCode);
            dgv.Rows[n].Cells[CodeColumnName].Value = ItemCode;
            dgv.Rows[n].Cells[NameColumnName].Value = dalItem.getItemName(ItemCode);
            dgv.Rows[n].Cells[FromCatColumnName].Value = "Factory";
            dgv.Rows[n].Cells[FromColumnName].Value = factoryName;
            dgv.Rows[n].Cells[ToCatColumnName].Value = "Production";
            dgv.Rows[n].Cells[ToColumnName].Value = "";
            dgv.Rows[n].Cells[QtyColumnName].Value = qty;
            dgv.Rows[n].Cells[UnitColumnName].Value = "piece";
            dgv.Rows[n].Cells[NoteColumnName].Value = "Production Carton out";

            facStockDAL dalFacStock = new facStockDAL();
            float facStock = dalFacStock.getQty(ItemCode, tool.getFactoryID(factoryName).ToString());
            float transferQty = Convert.ToSingle(qty);

            if (facStock - transferQty < 0)
            {
                dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                dgv.Rows[n].Cells[NoteColumnName].Value = "AFTER BAL=" + (facStock - transferQty);
            }
        }

        private void addSubItemToDGV(string factoryName, string ItemCode, int qty, string fromCat, string toCat, string to, string unit, string note)
        {
            DataGridView dgv = dgvTransfer;
            int n = dgv.Rows.Add();
            index++;

            dgv.Rows[n].Cells[IndexColumnName].Value = index;
            dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
            dgv.Rows[n].Cells[DateColumnName].Value = dtpTrfDate.Text;
            dgv.Rows[n].Cells[CatColumnName].Value = dalItem.getCatName(ItemCode);
            dgv.Rows[n].Cells[CodeColumnName].Value = ItemCode;
            dgv.Rows[n].Cells[NameColumnName].Value = dalItem.getItemName(ItemCode);
            dgv.Rows[n].Cells[FromCatColumnName].Value = fromCat;
            dgv.Rows[n].Cells[FromColumnName].Value = factoryName;
            dgv.Rows[n].Cells[ToCatColumnName].Value = toCat;
            dgv.Rows[n].Cells[ToColumnName].Value = to;
            dgv.Rows[n].Cells[QtyColumnName].Value = qty;
            dgv.Rows[n].Cells[UnitColumnName].Value = unit;
            dgv.Rows[n].Cells[NoteColumnName].Value = note;

            facStockDAL dalFacStock = new facStockDAL();
            float facStock = dalFacStock.getQty(ItemCode, tool.getFactoryID(factoryName).ToString());
            float transferQty = Convert.ToSingle(qty);

            if (facStock - transferQty < 0)
            {
                dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                dgv.Rows[n].Cells[NoteColumnName].Value = note+ " /AFTER BAL=" + (facStock - transferQty);
            }
        }

        private void productionAutoOut(string factoryName, string parentItemCode, float qty)
        {
            if(parentItemCode.Equals("V66K9J000") || parentItemCode.Equals("V66K9J0K0"))//max=80
            {
                addSubCartonToDGV(factoryName, "CREASING PAD 570 X 190", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 4);
                addSubCartonToDGV(factoryName, "CTN 590 X 515 X 360", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 1);
                addSubCartonToDGV(factoryName, "LYR PAD 560 X 500", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 1);
                addSubCartonToDGV(factoryName, "NESTING 496 X 170 X 570 X 170", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 2);
            }
            else if(parentItemCode.Equals("V66KAQ100"))//max=100
            {
                addSubCartonToDGV(factoryName, "CREASING PAD 570 X 190", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 100)) * 4);
                addSubCartonToDGV(factoryName, "CTN 590 X 515 X 360", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 100)) * 1);
                addSubCartonToDGV(factoryName, "LYR PAD 560 X 500", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 100)) * 1);
                addSubCartonToDGV(factoryName, "NESTING 496 X 170 X 570 X 170", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 100)) * 2);
            }
            else if(parentItemCode.Equals("V66KDN100") || parentItemCode.Equals("V73KBW100"))//max=80
            {
                addSubCartonToDGV(factoryName, "CREASING PAD 400 X 305", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 4);
                addSubCartonToDGV(factoryName, "CTN 608 X 413 X 455", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 1);
                addSubCartonToDGV(factoryName, "LYR PAD 590 X 370", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 1);
                addSubCartonToDGV(factoryName, "NESTING 405  X 218 X 601 X  218", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 80)) * 2);
            }
            else if(parentItemCode.Equals("V02K81000"))//max=50
            {
                addSubCartonToDGV(factoryName, "CTN 613 X 312 X 265", Convert.ToInt32(Math.Truncate(Convert.ToDecimal(qty) / 50)) * 1);
            }
            else if(parentItemCode.Equals("R 060 641 050 50"))
            {
                int maxQty = 100;
                int minQty = 50;
                int ParentQty = Convert.ToInt32(qty);

                int fullCartonQty = ParentQty / maxQty;

                int notFullCartonQty = ParentQty % maxQty / minQty;

                int ChildQty = fullCartonQty + notFullCartonQty;
                
                //addSubItemToDGV(factoryName, "CTN 910 X 250 X 220", ChildQty, "Factory", "Production", "", "piece", "Auto out");
                
            }
            else if (parentItemCode.Equals("R 060 641 385 60"))
            {
                int maxQty = 100;
                int minQty = 50;
                int ParentQty = Convert.ToInt32(qty);

                int fullCartonQty = ParentQty / maxQty;

                int notFullCartonQty = ParentQty % maxQty / minQty;

                int ChildQty = fullCartonQty + notFullCartonQty;

                //addSubItemToDGV(factoryName, "CTN 630 X 250 X 220 MM", ChildQty, "Factory", "Production", "", "piece", "Auto out");
                
            }
        }

        private string DeliveryAutoOut(string factoryName, string parentItemCode, float qty)
        {
            string note = "";
            if(parentItemCode.Equals("R 060 641 050 50/385 60"))
            {
                frmDialog frm = new frmDialog();
                frm.ShowDialog();

                int maxQty = 100;
                int minQty = 50;
                int ParentQty = Convert.ToInt32(qty);

                int fullCartonQty = ParentQty / maxQty;

                int notFullCartonQty = ParentQty % maxQty / minQty;

                int ChildQty = fullCartonQty + notFullCartonQty;

                if (frmDialog.InsType)//ins type
                {
                    addSubItemToDGV(factoryName, "CTN 910 X 250 X 220",ChildQty,"Factory","Production","","piece","Auto out(Delivery)");
                    note = "( INS )";
                }
                else//tube type
                {
                    addSubItemToDGV(factoryName, "CTN 630 X 250 X 220 MM", ChildQty, "Factory", "Production", "", "piece", "Auto out(Delivery)");
                    note = "( TUBE )";
                }
                
            }

            return note;
        }

        private bool childStockOut(string factoryName, string parentItemCode, float qty, int indexNo)
        {
            bool success = true;
  
            string childItemCode;

            if (parentItemCode.Substring(1, 2) == text.Inspection_Pass && cmbTrfFromCategory.Text.Equals(text.Assembly))
            {
                DataTable dtSPP = dalJoin.loadChildList(parentItemCode);

                foreach (DataRow row in dtSPP.Rows)
                {
                    //string text = row["join_child_code"].ToString().Substring(0, 2);
                    if (row["join_child_code"].ToString().Substring(0, 2) == "CF")
                    parentItemCode = row["join_child_code"].ToString();
                }
            }

            DataTable dtJoin = dalJoin.loadChildList(parentItemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    float childQty = qty;

                    float joinQty = float.TryParse(Join["join_qty"].ToString(), out float i) ? Convert.ToSingle(Join["join_qty"].ToString()): 1;
                    int joinMax = int.TryParse(Join[dalJoin.JoinMax].ToString(), out int j) ? Convert.ToInt32(Join[dalJoin.JoinMax].ToString()) : 1;
                    int JoinMin = int.TryParse(Join[dalJoin.JoinMin].ToString(), out int k) ? Convert.ToInt32(Join[dalJoin.JoinMin].ToString()) :1;

                    joinMax = joinMax <= 0 ? 1 : joinMax;
                    JoinMin = JoinMin <= 0 ? 1 : JoinMin;

                    int ParentQty = Convert.ToInt32(qty);

                    int fullQty = ParentQty / joinMax;

                    int notFullQty = ParentQty % joinMax;

                    childQty = fullQty * joinQty;

                    if(notFullQty >= JoinMin)
                    {
                        childQty += joinQty;
                    }

                    //childQty = childQty * Convert.ToSingle(Join["join_qty"].ToString());

                    childItemCode = Join["join_child_code"].ToString();
                    DataTable dtItem = dalItem.codeSearch(childItemCode);
                    if (dtItem.Rows.Count > 0)
                    {
                        DataGridView dgv = dgvTransfer;
                        int n = dgv.Rows.Add();
                        index++;
                        dgv.Rows[n].Cells[IndexColumnName].Value = index;
                        dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
                        dgv.Rows[n].Cells[DateColumnName].Value = dtpTrfDate.Text;
                        dgv.Rows[n].Cells[CatColumnName].Value = dtItem.Rows[0][dalItem.ItemCat].ToString();
                        dgv.Rows[n].Cells[CodeColumnName].Value = childItemCode;
                        dgv.Rows[n].Cells[NameColumnName].Value = dtItem.Rows[0][dalItem.ItemName].ToString();

                        if(isInpectionItem)
                        {
                            dgv.Rows[n].Cells[ToCatColumnName].Value = text.Inspection;
                        }
                        else
                        {
                            dgv.Rows[n].Cells[ToCatColumnName].Value = text.Assembly;
                            dgv.Rows[n].Cells[NoteColumnName].Value = text.Assembly + dalItem.getCatName(childItemCode);
                        }
                        dgv.Rows[n].Cells[FromCatColumnName].Value = text.Factory;
                        dgv.Rows[n].Cells[FromColumnName].Value = factoryName;
                        
                        dgv.Rows[n].Cells[ToColumnName].Value = "";
                        dgv.Rows[n].Cells[QtyColumnName].Value = childQty;
                        dgv.Rows[n].Cells[UnitColumnName].Value = text.Unit_Piece;
                        

                        if (childItemCode.Equals("V76KM4000 0.360"))
                        {
                            //dgv.Rows[n].Cells[QtyColumnName].Value = childQty * 0.36;
                            dgv.Rows[n].Cells[UnitColumnName].Value = text.Unit_Meter;
                        }

                        facStockDAL dalFacStock = new facStockDAL();
                        float facStock = dalFacStock.getQty(childItemCode, tool.getFactoryID(factoryName).ToString());
                        float transferQty = Convert.ToSingle(txtTrfQty.Text);

                        if (facStock - transferQty < 0)
                        {
                            dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                            dgv.Rows[n].Cells[NoteColumnName].Value = "AFTER BAL=" + (facStock - transferQty);
                        }
                        else
                        {
                            dgv.Rows[n].Cells[NoteColumnName].Value = "";
                        }
                    }
                }
            }

            return success;
        }
    
        private int TransferAction()
        {
            string result = "Passed";
            int TrfID = -1;

            //part in out
            if (category.Equals("Part"))
            {
                if (fromCat.Equals("Production") && toCat.Equals("Factory"))
                {
                    if(tool.ifGotChild(itemCode))
                    {
                        if(dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))
                        {
                            MessageBox.Show("Assembly parts cannot be production.");
                            result = "Failed";
                        }
                        else
                        {
                            //factory stock in (part)
                            if (stockIn(to, itemCode, qty, "set"))
                            {
                                productionRecord(from, to, itemCode, qty);
                            }
                            else
                            {
                                result = "Failed";
                            }
                        }
                    }
                    else
                    {
                        //factory stock in (part)
                        if (stockIn(to, itemCode, qty, "piece"))
                        {
                            productionRecord(from, to, itemCode, qty);
                        }
                        else
                        {
                            result = "Failed";
                        }
                    }
                    TrfID = TransferRecord(result);
                }

                else if (fromCat.Equals("Factory"))
                {
                    if (toCat.Equals("Factory"))
                    {
                        if (!(stockIn(to, itemCode, qty, unit) && stockOut(from, itemCode, qty, unit)))
                        {
                            result = "Failed";
                        }
                        TrfID = TransferRecord(result);
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
                        TrfID = TransferRecord(result);
                    }
                    else
                    {
                        if (!stockOut(from, itemCode, qty, unit))
                        {
                            result = "Failed";
                        }
                        TrfID = TransferRecord(result);
                    }
                }

                else if (fromCat.Equals("Assembly") && toCat.Equals("Factory"))
                {

                    if (tool.ifGotChild(itemCode))
                    {
                        if (!stockIn(to, itemCode, qty, unit))
                        {
                            result = "Failed";
                        }
                        //childStockOut(to, itemCode, qty, transferRecord(result));
                    }
                    else
                    {
                        MessageBox.Show("This item not a assembly part");
                        result = "Failed";
                    }

                    failedNote = "not a assembly part";
                    TrfID = TransferRecord(result);
                }

                else if (toCat.Equals("Factory"))
                {
                    if (!stockIn(to, itemCode, qty, unit))
                    {
                        result = "Failed";
                    }
                    TrfID = TransferRecord(result);
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
                    TrfID = TransferRecord(result);
                }
                else if (fromCat.Equals("Factory"))
                {
                    if (toCat.Equals("Factory"))
                    {
                        if (!(stockIn(to, itemCode, qty, unit) && stockOut(from, itemCode, qty, unit)))
                        {
                            result = "Failed";
                        }
                        TrfID = TransferRecord(result);
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
                        TrfID = TransferRecord(result);
                    }
                    else
                    {
                        if (!stockOut(from, itemCode, qty, unit))
                        {
                            result = "Failed";
                        }
                        TrfID = TransferRecord(result);
                    }
                }
                else if (toCat.Equals("Factory"))
                {
                    if (!stockIn(to, itemCode, qty, unit))
                    {
                        result = "Failed";
                    }
                    TrfID = TransferRecord(result);
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
            }
            else
            {
                TrfID = -1;
            }

            return TrfID;
        }

        #endregion

        #region function:transfer/cancel

        private bool UpdateDOandPO(DataTable dt_DO, DataTable dt_PO, string trfItemCode, int trfID, string trfQty)
        {
            bool success = false;
            DateTime updatedDate = DateTime.Now;
            int userID = MainDashboard.USER_ID;

            uData.Updated_Date = updatedDate;
            uData.Updated_By = userID;

            if(!string.IsNullOrEmpty(itemCode) && dt_DOItem.Rows.Count > 0 )
            {
                //GET D/O TABLE CODE BY ITEM CODE SEARCHING
                foreach (DataRow row in dt_DOItem.Rows)
                {
                    //GET TRANSFER QTY
                    string itemCode = row[header_ItemCode].ToString();
                    string pcsQty = row[header_DeliveryPCS].ToString();

                    if (trfItemCode == itemCode && pcsQty == trfQty)
                    {
                        string DOTableCode = row[header_DOTblCode].ToString();

                        //update D/O
                        uData.Table_Code = Convert.ToInt32(DOTableCode);
                        uData.IsDelivered = true;
                        uData.Trf_tbl_code = trfID;

                        success = dalData.DODelivered(uData);

                        if(!success)
                        {
                            MessageBox.Show("Failed to update D/O\nTransfer ID: " + trfID);
                            return success;
                        }

                        //SEARCHING AT D/O DATABASE TABLE
                        foreach (DataRow rowDODatabase in dt_DO.Rows)
                        {
                            string DB_DOTableCode = rowDODatabase[dalData.TableCode].ToString();

                            if(DB_DOTableCode == DOTableCode)
                            {
                                //GET P/O TABLE CODE
                                string POTableCode = rowDODatabase[dalData.POTableCode].ToString();

                                //get P/O delivered qty
                                foreach(DataRow rowPODatabase in dt_PO.Rows)
                                {
                                    string DB_POTableCode = rowPODatabase[dalData.TableCode].ToString();

                                    if(POTableCode == DB_POTableCode)
                                    {
                                        int deliveredQty = int.TryParse(rowPODatabase[dalData.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                                        deliveredQty += int.TryParse(trfQty, out int i) ? i : 0;

                                        //update P/O

                                        uData.Table_Code = Convert.ToInt32(DB_POTableCode);
                                        uData.Delivered_qty = deliveredQty;

                                        success = dalData.PODeliveredDataUpdate(uData);

                                        if (!success)
                                        {
                                            MessageBox.Show("Failed to update P/O\nP/O Table Code: " + DB_POTableCode);
                                           
                                        }

                                        return success;
                                    }
                                }
                                
                            }
                        }
                        
                    }


                }
            }
            

            return success;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //foreach row and make transfer action
                    DataTable dt_DODataBase = dalData.DOSelect();
                    DataTable dt_PODataBase = dalData.POSelect();

                    foreach (DataGridViewRow c in dgvTransfer.Rows)
                    {
                        selectedRow = c.Index;
                        getData();
                        int trfID = TransferAction();

                        if (trfID == -1)
                        {
                            MessageBox.Show("TRANSFER CANCEL: Transfer Error Occur, please contact your system admin. ");
                            Cursor = Cursors.Arrow; // change cursor to normal type 
                            return;
                        }
                        else
                        {
                            TrfSuccess = true;

                            if (callFromMatChecklist)
                            {
                                dt_MatChecklist = tool.CopyDGVToDatatable(dgvTransfer);
                            }
                            else if(callFromDOlist)
                            {
                                string itemCode = c.Cells[CodeColumnName].Value.ToString();
                                string trfQty = c.Cells[QtyColumnName].Value.ToString();

                                if (!UpdateDOandPO(dt_DODataBase, dt_PODataBase, itemCode, trfID, trfQty))
                                {
                                    //undo transfer record
                                    MessageBox.Show("Undo Action");
                                }
                            }

                            Close();
                        }
                    }
                }
                

                Cursor = Cursors.Arrow; // change cursor to normal type 
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
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
            loadLocationCategoryData();


            if (ifGotChild() || !string.IsNullOrEmpty(cmbTrfItemCode.Text ) && cmbTrfItemCode.Text.Substring(1, 2) == text.Inspection_Pass)
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
                cmbTrfFrom.Text = tool.getCustomerName(cmbTrfItemCode.Text);
            }
            else if(cmbTrfFromCategory.Text.Equals("Assembly"))
            {
                cmbTrfFrom.DataSource = null;
                cmbTrfToCategory.SelectedIndex = 0;

                if (cmbTrfItemCode.Text.Substring(1, 2) == text.Inspection_Pass)
                {
                    cmbTrfTo.Text = text.Factory_Store;
                    //cmbTrfTo.SelectedIndex = 5;
                }
                else
                {
                    cmbTrfTo.SelectedIndex = 4;
                }
                
                
            }
            else if (cmbTrfFromCategory.Text.Equals("Inspection"))
            {
                cmbTrfFrom.DataSource = null;
                cmbTrfToCategory.SelectedIndex = 0;
                cmbTrfTo.Text = text.Factory_Store;
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
                if (cmbTrfItemCode.Text.Substring(1, 2) == text.Inspection_Pass)
                {
                    DataTable dt = dalData.CustomerWithoutRemovedDataSelect();
                    loadLocationData(dt, cmbTrfTo, dalData.ShortName);
                }
                else
                {
                    DataTable dt = dalCust.Select();
                    loadLocationData(dt, cmbTrfTo, "cust_name");

                    cmbTrfTo.Text = tool.getCustomerName(cmbTrfItemCode.Text);
                }
               

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
                if (!(dgv.Rows != null && dgv.Rows.Count != 0))
                {
                    n = dgv.Rows.Add();
                    index = n + 1;
                    dgv.Rows[n].Cells[IndexColumnName].Value = index;
                }

                //if (dgv.DataSource == null)
                //{
                //    n = dgv.Rows.Add();
                //    index = n + 1;
                //    dgv.Rows[n].Cells[IndexColumnName].Value = index;
                //}
            }
            else
            {
                n = dgv.Rows.Add();
                //index++;
                index = n+1;
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

            string trfFromCat = cmbTrfFromCategory.Text;

            if (cmbTrfFromCategory.Text.Equals(text.Factory))
            {
                facStockDAL dalFacStock = new facStockDAL();
                float facStock = dalFacStock.getQty(cmbTrfItemCode.Text, tool.getFactoryID(cmbTrfFrom.Text).ToString());
                float transferQty = Convert.ToSingle(txtTrfQty.Text);
                string factoryName = "";
                if (string.IsNullOrEmpty(cmbTrfTo.Text))
                {
                    factoryName = cmbTrfToCategory.Text;
                }
                else
                {
                    factoryName = cmbTrfTo.Text;
                }

                if (facStock - transferQty < 0 && cmbTrfFrom.Text != cmbTrfTo.Text)
                {
                    //#############################################################################################################################################
                    dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                    dgv.Rows[n].Cells[NoteColumnName].Value += " (AFTER BAL:"+(facStock - transferQty).ToString()+")";
                }

                if (!cmbTrfToCategory.Text.Equals("Factory"))
                {
                    dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
                }

                if (cmbTrfToCategory.Text.Equals("Customer"))
                {
                    //dgv.Rows[n].Cells[NoteColumnName].Value += DeliveryAutoOut(cmbTrfFrom.Text, cmbTrfItemCode.Text, Convert.ToSingle(txtTrfQty.Text));
                }
            }

            else if ((trfFromCat.Equals(text.Assembly) || trfFromCat.Equals(text.Inspection)) && cmbTrfToCategory.Text.Equals(text.Factory))
            {
                if(trfFromCat.Equals(text.Inspection))
                {
                    isInpectionItem = true;
                }

                 dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.FromArgb(0, 192, 0);
                string factoryName = "";
                if (tool.ifGotChild(cmbTrfItemCode.Text))
                {
                    
                    
                    if (string.IsNullOrEmpty(cmbTrfTo.Text))
                    {
                        factoryName = cmbTrfToCategory.Text;
                    }
                    else
                    {
                        factoryName = cmbTrfTo.Text;
                    }
                    if(!dgvEdit)
                    childStockOut(factoryName, cmbTrfItemCode.Text, Convert.ToSingle(txtTrfQty.Text), -1);

                }
                isInpectionItem = false;
                //productionAutoOut(factoryName, cmbTrfItemCode.Text, Convert.ToSingle(txtTrfQty.Text));
            }

            else if (cmbTrfFromCategory.Text.Equals(text.Production) && cmbTrfToCategory.Text.Equals(text.Factory))
            {
                dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.FromArgb(0, 192, 0);
                string factoryName = "";
                if (string.IsNullOrEmpty(cmbTrfTo.Text))
                {
                    factoryName = cmbTrfToCategory.Text;
                }
                else
                {
                    factoryName = cmbTrfTo.Text;
                }

                if (tool.ifGotChild(cmbTrfItemCode.Text)) //&& dalItem.checkIfProduction(cmbTrfItemCode.Text)
                {
                    if (!dgvEdit)
                        productionChildStockOut(factoryName, cmbTrfItemCode.Text, Convert.ToSingle(txtTrfQty.Text), -1);
                }
                //productionAutoOut(factoryName, cmbTrfItemCode.Text, Convert.ToSingle(txtTrfQty.Text));
            }

            else if (cmbTrfToCategory.Text.Equals("Factory"))
            {
                dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.FromArgb(0, 192, 0);
            }

            dgv.ClearSelection();
            dgv.Rows[n].Selected = true;
        }

        private void addToDGV(DataTable dt, bool fromProductionRecord)
        {
            DataGridView dgv = dgvTransfer;
            DataTable dt_ItemInfo = dalItem.Select();
            int n;

            //if (dgvEdit)
            //{
            //    n = selectedRow;
            //}
            //else
            //{
            //    n = dgv.Rows.Add();
            //    //index++;
            //    index = n + 1;
            //    dgv.Rows[n].Cells[IndexColumnName].Value = index;
            //}

            string ProDate = null, itemCat = null, itemCode = null, itemName = null, fromCat = null, from = null, to = null, proQty = null, planID = null, unit = null, shift = null;

            foreach (DataRow row in dt.Rows)
            {
                n = dgv.Rows.Add();
                //index++;
                index = n + 1;
                dgv.Rows[n].Cells[IndexColumnName].Value = index;

                ProDate = row[header_ProDate].ToString();
                itemCat = row[header_Cat].ToString();
                itemCode = row[header_ItemCode].ToString();
                itemName = tool.getItemNameFromDataTable(dt_ItemInfo, itemCode);
                from = row[header_From].ToString();
                to = row[header_To].ToString();
                proQty = row[header_Qty].ToString();
                planID = row[header_PlanID].ToString();
                shift = row[header_Shift].ToString();

                dtpTrfDate.Text = ProDate;
                if (itemCat == text.Cat_SubMat || itemCat == text.Cat_Part || itemCat == text.Cat_Carton || itemCat == text.Cat_Packaging)
                {
                    unit = text.Unit_Piece;
                }
                else
                {
                    unit = text.Unit_KG;
                }

                dgv.Rows[n].Cells[DateColumnName].Value = ProDate;
                dgv.Rows[n].Cells[CatColumnName].Value = itemCat;
                dgv.Rows[n].Cells[CodeColumnName].Value = itemCode;
                dgv.Rows[n].Cells[NameColumnName].Value = itemName;

                bool fromFac = false, toFac = false;
                if(tool.IfFactoryExists(from))
                {
                    fromFac = true;
                    dgv.Rows[n].Cells[FromCatColumnName].Value = text.Factory;
                    dgv.Rows[n].Cells[FromColumnName].Value = from;

                }
                else
                {
                    dgv.Rows[n].Cells[FromCatColumnName].Value =from;
                    dgv.Rows[n].Cells[FromColumnName].Value = "";
                }

                if (tool.IfFactoryExists(to))
                {
                    toFac = true;
                    dgv.Rows[n].Cells[ToCatColumnName].Value = text.Factory;
                    dgv.Rows[n].Cells[ToColumnName].Value = to;
                }
                else
                {
                    dgv.Rows[n].Cells[ToCatColumnName].Value = to;
                    dgv.Rows[n].Cells[ToColumnName].Value = "";
                }

                if(fromFac && !toFac)
                {
                    dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
                }
                else if(!fromFac && toFac)
                {
                    dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.FromArgb(0, 192, 0);
                }

                dgv.Rows[n].Cells[QtyColumnName].Value = proQty;
                dgv.Rows[n].Cells[UnitColumnName].Value = unit;


                if(shift == "M" || shift == "N")
                {
                    dgv.Rows[n].Cells[NoteColumnName].Value = "[Plan " + planID + "(" + shift + ")]";
                }
                else if (shift == "MB" || shift == "NB")
                {

                    dgv.Rows[n].Cells[NoteColumnName].Value = "[Plan " + planID + "(" + shift.Substring(0, 1) + ") Balance Stock In]";
                }
                else if (shift == "MBO" || shift == "NBO")
                {

                    dgv.Rows[n].Cells[NoteColumnName].Value = "[Plan " + planID + "(" + shift.Substring(0, 1) + ") Old Balance Stock Out]";
                }



                if (tool.ifGotChild(itemCode))
                {
                    productionChildStockOut(to, itemCode, Convert.ToSingle(proQty), -1);
                }
            }

            

            dgv.ClearSelection();
            //dgv.Rows[n].Selected = true;
        }

        private void addMatToDGV(DataTable dt)
        {
            DataGridView dgv = dgvTransfer;
            DataTable dt_ItemInfo = dalItem.Select();
            int n;
            

            foreach (DataRow row in dt.Rows)
            {
                n = dgv.Rows.Add();
                index = Convert.ToInt32(row[headerIndex].ToString());
                string itemCode = row[headerMatCode].ToString();
                string itemCat = row[headerType].ToString();
                string from = row[headerFrom].ToString();
                string to = row[headerTo].ToString();
                string qty = row[headerQty].ToString();
                string unit;
                string planID = row[headerPlanID].ToString();
                if (itemCat == text.Cat_SubMat || itemCat == text.Cat_Part)
                {
                    unit = text.Unit_Piece;
                }
                else
                {
                    unit = text.Unit_KG;
                }

                dgv.Rows[n].Cells[IndexColumnName].Value = index;
                dgv.Rows[n].Cells[DateColumnName].Value = frmMatCheckList.MatTrfDate;
                dgv.Rows[n].Cells[CatColumnName].Value = itemCat;
                dgv.Rows[n].Cells[CodeColumnName].Value = itemCode;
                dgv.Rows[n].Cells[NameColumnName].Value = tool.getItemNameFromDataTable(dt_ItemInfo,itemCode);
                dgv.Rows[n].Cells[FromCatColumnName].Value = text.Factory;
                dgv.Rows[n].Cells[FromColumnName].Value = from;

                if(tool.IfFactoryExists(to))
                {
                    dgv.Rows[n].Cells[ToCatColumnName].Value = text.Factory;
                    dgv.Rows[n].Cells[ToColumnName].Value = to;
                }
                else
                {
                    dgv.Rows[n].Cells[ToCatColumnName].Value = to;
                }

                dgv.Rows[n].Cells[QtyColumnName].Value = qty;


                dgv.Rows[n].Cells[UnitColumnName].Value = unit;
                dgv.Rows[n].Cells[NoteColumnName].Value = "[For Plan "+ planID+"]";

                if (cmbTrfFromCategory.Text.Equals("Factory"))
                {
                    facStockDAL dalFacStock = new facStockDAL();
                    float facStock = dalFacStock.getQty(itemCode, tool.getFactoryID(from).ToString());
                    float transferQty = Convert.ToSingle(qty);

                    if (facStock - transferQty < 0 && from != to)
                    {
                        //#############################################################################################################################################
                        dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                        dgv.Rows[n].Cells[NoteColumnName].Value += " (AFTER BAL:" + (facStock - transferQty).ToString() + ")";
                    }
                }
            }
            dgv.ClearSelection();
        }

        private void addPartToDGV(DataTable dt)
        {
            DataGridView dgv = dgvTransfer;
            DataTable dt_ItemInfo = dalItem.Select();
            int n;


            foreach (DataRow row in dt.Rows)
            {
                n = dgv.Rows.Add();
                index = n+1;
                string itemCode = row[header_ItemCode].ToString();
                string itemCat = text.Cat_Part;
                string from = text.Factory_Store;
                string to = row[header_Customer].ToString();
                string pcsQty = row[header_DeliveryPCS].ToString();
                string bagQty = row[header_DeliveryBAG].ToString();
                string unit = text.Unit_Set;
                string DOCode = row[header_DONo].ToString();

                
                dgv.Rows[n].Cells[IndexColumnName].Value = index;
                dgv.Rows[n].Cells[IndexColumnName].Style.BackColor = Color.Red;
                dgv.Rows[n].Cells[DateColumnName].Value = delivered_date.ToShortDateString();
                dgv.Rows[n].Cells[CatColumnName].Value = itemCat;
                dgv.Rows[n].Cells[CodeColumnName].Value = itemCode;
                dgv.Rows[n].Cells[NameColumnName].Value = tool.getItemNameFromDataTable(dt_ItemInfo, itemCode);
                dgv.Rows[n].Cells[FromCatColumnName].Value = text.Factory;
                dgv.Rows[n].Cells[FromColumnName].Value = from;

                dgv.Rows[n].Cells[ToCatColumnName].Value = text.Customer;
                dgv.Rows[n].Cells[ToColumnName].Value = to;
                dgv.Rows[n].Cells[QtyColumnName].Value = pcsQty;

                int doCode = int.TryParse(DOCode, out doCode) ? doCode : 0;
                
                dgv.Rows[n].Cells[UnitColumnName].Value = unit;
                dgv.Rows[n].Cells[NoteColumnName].Value = "[DO " + doCode.ToString("D6") + ": "+bagQty +" Bags]";

                if (cmbTrfFromCategory.Text.Equals("Factory"))
                {
                    facStockDAL dalFacStock = new facStockDAL();
                    float facStock = dalFacStock.getQty(itemCode, tool.getFactoryID(from).ToString());
                    float transferQty = Convert.ToSingle(pcsQty);

                    if (facStock - transferQty < 0 && from != to)
                    {
                        //#############################################################################################################################################
                        dgv.Rows[n].Cells[NoteColumnName].Style.ForeColor = Color.Red;
                        dgv.Rows[n].Cells[NoteColumnName].Value += " (AFTER BAL:" + (facStock - transferQty).ToString() + ")";
                    }
                }
            }
            dgv.ClearSelection();
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
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                if (Validation())
                {
                    if(callFromDOlist)
                    {
                        MessageBox.Show("Unable to ADD or EDIT D/O transfer data here.\nPlease return to D/O list to MODIFY.");  
                    }
                    else
                    {
                        addToDGV();                
                    }

                    dgvEdit = false;

                    txtTrfQty.Clear();
                    cmbTrfFrom.SelectedIndex = -1;
                    cmbTrfTo.SelectedIndex = -1;

                }

                changeButton();
                Cursor = Cursors.Arrow; // change cursor to normal type 
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void loadFromDGV()
        {
            DataGridView dgv = dgvTransfer;

            DateTime curDate;

            if(selectedRow >= 0)
            {
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
            
        }

        private void changeButton()
        {
            if(dgvEdit && selectedRow >= 0)
            {
                btnEdit.Text = "EDIT";
                btnDelete.Show();
            }
            else
            {
                btnEdit.Text = "ADD";
                btnDelete.Hide();
                dgvEdit = false;
            } 
        }

        private void dgvTransfer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                //load from dgv
                loadFromDGV();


                dgvEdit = true;
                changeButton();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                dgvTransfer.Rows.RemoveAt(selectedRow);
                dgvEdit = false;
                changeButton();
                Cursor = Cursors.Arrow; // change cursor to normal type 
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
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
            note = dgv.Rows[n].Cells[NoteColumnName].Value == null ?string.Empty : dgv.Rows[n].Cells[NoteColumnName].Value.ToString();

            qty = unitCheck(qty, unit);
            unit = checkUnit(unit);

            if (qty < 0)
            {
                qty = 0;
            }
        }

        private void frmInOutEdit_MouseClick(object sender, MouseEventArgs e)
        {
            dgvTransfer.ClearSelection();
        }

        private void dgvTransfer_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var ht = dgvTransfer.HitTest(e.X, e.Y);

                if (ht.Type == DataGridViewHitTestType.None)
                {
                    //clicked on grey area
                    dgvTransfer.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }  
        }

        private void cmbTrfItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //SortedDictionary<int, ListInfo> dict = new SortedDictionary<int, ListInfo>();

            //int found = -1;
            //int current = cmbTrfItemName.SelectedIndex;

            //// collect all items that match:
            //for (int i = 0; i < comboBox1.Items.Count; i++)
            //    if (((ListInfo)comboBox1.Items[i]).Name.ToLower().IndexOf(e.KeyChar.ToString().ToLower()) >= 0)
            //        // case sensitive version:
            //        // if (((ListInfo)comboBox1.Items[i]).Name.IndexOf(e.KeyChar.ToString()) >= 0)
            //        dict.Add(i, (ListInfo)comboBox1.Items[i]);

            //// find the one after the current position:
            //foreach (KeyValuePair<int, ListInfo> kv in dict)
            //    if (kv.Key > current) { found = kv.Key; break; }

            //// or take the first one:
            //if (dict.Keys.Count > 0 && found < 0) found = dict.Keys.First();

            //if (found >= 0) comboBox1.SelectedIndex = found;

            //e.Handled = true;
        }

        private void dgvTransfer_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();

                    dgvTransfer.CurrentCell = dgvTransfer.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    string itemCode = dgvTransfer.Rows[e.RowIndex].Cells[CodeColumnName].Value.ToString();
                    
                    string from = dgvTransfer.Rows[e.RowIndex].Cells[FromCatColumnName].Value.ToString();
                    string to = dgvTransfer.Rows[e.RowIndex].Cells[ToCatColumnName].Value.ToString();
                    float trfQty = Convert.ToSingle(dgvTransfer.Rows[e.RowIndex].Cells[QtyColumnName].Value);

                    if (from.Equals("Factory"))
                    {
                        from = dgvTransfer.Rows[e.RowIndex].Cells[FromColumnName].Value.ToString();
                    }
                    else
                    {
                        from = "";
                    }

                    if (to.Equals("Factory"))
                    {
                        to = dgvTransfer.Rows[e.RowIndex].Cells[ToColumnName].Value.ToString();
                    }
                    else
                    {
                        to = "";
                    }

                    // Can leave these here - doesn't hurt
                    dgvTransfer.Rows[e.RowIndex].Selected = true;
                    dgvTransfer.Focus();
                    int rowIndex = dgvTransfer.CurrentCell.RowIndex;

                    my_menu.Items.Add(itemCode + "     " + dalItem.getItemName(itemCode)).Name = itemCode;
                    my_menu.Items.Add("").Name = itemCode;

                    DataTable dt = dalStock.Select(itemCode);

                    if (dt.Rows.Count > 0)
                    {
                        float beforeTotalQty = 0;
                        float afterTotalQty = 0;
                        foreach (DataRow stock in dt.Rows)
                        {
                            string fac = stock["fac_name"].ToString();
                            string qty = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");
                            beforeTotalQty += Convert.ToSingle(qty);
                            afterTotalQty += Convert.ToSingle(qty);

                            if (from.Equals(fac) && to.Equals(fac))
                            {
                                qty =  qty + " --> " +qty;
                                from = "";
                                to = "";
                            }

                            else if (from.Equals(fac))
                            {
                                afterTotalQty -= trfQty;
                                qty = qty + " --> " + (Convert.ToSingle(qty) - trfQty).ToString();
                                from = "";
                            }

                            else if (to.Equals(fac))
                            {
                                afterTotalQty += trfQty;
                                qty = qty + " --> " + (Convert.ToSingle(qty) + trfQty).ToString();
                                to = "";
                            }


                            int length = fac.Length;
                            if((10 - length) >=0)
                            {
                                for (int i = 1; i <= (10 - length); i++)
                                {
                                    fac += " ";
                                }
                            }

                            my_menu.Items.Add(fac+" "+qty).Name = fac;                          
                        }

                        if(from != "" && to != "")
                        {
                            string fac = "";
                            string stock = "";
                            if (from.Equals(to))
                            {
                                fac = from;
                                stock = "0 --> 0";
                                int length = fac.Length;
                                if ((10 - length) >= 0)
                                {
                                    for (int i = 1; i <= (10 - length); i++)
                                    {
                                        fac += " ";
                                    }
                                }
                                my_menu.Items.Add(fac + " " + stock).Name = fac;
                            }
                            else
                            {
                                fac = from;
                                stock = "0 --> " + (0 - trfQty);
                                int length = fac.Length;
                                if ((10 - length) >= 0)
                                {
                                    for (int i = 1; i <= (10 - length); i++)
                                    {
                                        fac += " ";
                                    }
                                }
                                my_menu.Items.Add(fac + " " + stock).Name = fac;

                                fac = to;
                                stock = "0 -- > " + (trfQty);

                                length = fac.Length;
                                if ((10 - length) >= 0)
                                {
                                    for (int i = 1; i <= (10 - length); i++)
                                    {
                                        fac += " ";
                                    }
                                }
                                my_menu.Items.Add(fac + " " + stock).Name = fac;
                            }
                            
                            

                        }
                        else if (from != "")
                        {
                            string fac = from;
                            string stock = "0 --> " + (0 - trfQty);
                            int length = fac.Length;
                            afterTotalQty -= trfQty;

                            if ((10 - length) >= 0)
                            {
                                for (int i = 1; i <= (10 - length); i++)
                                {
                                    fac += " ";
                                }
                            }
                            my_menu.Items.Add(fac + " " + stock).Name = fac;
                        }
                        else if (to != "")
                        {
                            string fac = to;
                            string stock = "0 --> " +trfQty;
                            int length = fac.Length;
                            afterTotalQty += trfQty;

                            if ((10 - length) >= 0)
                            {
                                for (int i = 1; i <= (10 - length); i++)
                                {
                                    fac += " ";
                                }
                            }
                            my_menu.Items.Add(fac + " " + stock).Name = fac;
                        }

                        string total = null;

                        if(beforeTotalQty == afterTotalQty)
                        {
                            total = beforeTotalQty.ToString("0.00");
                        }
                        else
                        {
                            total = beforeTotalQty + " --> " + afterTotalQty;
                        }

                        my_menu.Items.Add("---------------------").Name = "Line1";
                        my_menu.Items.Add("Total" + "       " + total).Name = "Total";
                        my_menu.Items.Add("---------------------").Name = "Line2";                         
                    }
                    else
                    {
                        string fac = "null";
                        string qty = "null";
                        my_menu.Items.Add(fac + "   " + qty).Name = fac;
                    }

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dtpTrfDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DialogResult dialogResult = MessageBox.Show("Are you sure want to clear all row's data?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dgvTransfer.Rows.Clear();
                }


                Cursor = Cursors.Arrow; // change cursor to normal type 
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void cmbTrfTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvTransfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnIN_Click(object sender, EventArgs e)
        {
            string itemCode = cmbTrfItemCode.Text;

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (tool.ifGotChild(itemCode))
                {
                    if (dalItem.checkIfAssembly(itemCode) && !dalItem.checkIfProduction(itemCode))//assembly
                    {
                        cmbTrfFromCategory.Text = "Assembly";
                        cmbTrfToCategory.Text = "Factory";
                    }
                    else//production
                    {
                        cmbTrfFromCategory.Text = "Production";
                        cmbTrfToCategory.Text = "Factory";
                    }
                }
                else if (dalItem.getCatName(itemCode).Equals("Part"))
                {
                    cmbTrfFromCategory.Text = "Production";
                    cmbTrfToCategory.Text = "Factory";
                }
                else
                {
                    if(dalMaterial.checkIfZeroCost(itemCode))
                    {
                        cmbTrfFromCategory.Text = "Customer";
                        cmbTrfTo.Text = "PMMA";
                        cmbTrfToCategory.Text = "Factory";
                    }
                    else
                    {
                        cmbTrfFromCategory.Text = "Supplier";
                        cmbTrfToCategory.Text = "Factory";
                    }
                }
                cmbTrfTo.SelectedIndex = -1;

            }
            else
            {
                cmbTrfFromCategory.SelectedIndex = -1;
                cmbTrfToCategory.SelectedIndex = -1;
            }
        }

        private void btnOUT_Click(object sender, EventArgs e)
        {
            string itemCode = cmbTrfItemCode.Text;

            if (!string.IsNullOrEmpty(itemCode))
            {
                cmbTrfFromCategory.Text = "Factory";
                cmbTrfFrom.SelectedIndex = -1;

                if (dalItem.getCatName(itemCode).Equals("Part"))
                {
                    string customer = tool.getCustomerName(cmbTrfItemCode.Text);

                    if (!customer.Equals(""))
                    {
                        cmbTrfToCategory.Text = "Customer";

                        //DataTable dt = dalCust.Select();
                        //loadLocationData(dt, cmbTrfTo, "cust_name");

                        cmbTrfTo.Text = customer;
                    }
                    else
                    {
                        cmbTrfToCategory.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbTrfFromCategory.Text = "Factory";
                    cmbTrfFrom.SelectedIndex = -1;
                    cmbTrfToCategory.Text = "Production";
                    cmbTrfTo.SelectedIndex = -1;
                }
                    

            }
            else
            {
                cmbTrfFromCategory.SelectedIndex = -1;
                cmbTrfToCategory.SelectedIndex = -1;
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string itemCode = cmbTrfItemCode.Text;

            if (!string.IsNullOrEmpty(itemCode))
            {
                cmbTrfFromCategory.Text = "Factory";
                cmbTrfToCategory.Text = "Factory";

                cmbTrfFrom.SelectedIndex = -1;
                cmbTrfTo.SelectedIndex = -1;
            }
            else
            {
                cmbTrfFromCategory.SelectedIndex = -1;
                cmbTrfToCategory.SelectedIndex = -1;
            }
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbTrfFrom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblPartCodeReset_Click(object sender, EventArgs e)
        {
            dtpTrfDate.Text = DateTime.Now.Date.ToShortDateString();
        }
    }
}