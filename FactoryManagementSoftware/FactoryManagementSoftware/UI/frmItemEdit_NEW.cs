using System;
using System.Data;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;
using System.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemEdit_NEW : Form
    {
        public frmItemEdit_NEW()
        {
            //item Adding

            InitializeComponent();

            Text = itemEdit_Add;
            MODE_ADDING = true;
            btnSave.Text = Button_AddItem;
            txtItemCode.Enabled = true;

            DataReset();
        }

        public frmItemEdit_NEW(bool ShowQuotation)
        {
            //item Adding

            InitializeComponent();

            Text = itemEdit_Add;
            MODE_ADDING = true;
            btnSave.Text = Button_AddItem;
            txtItemCode.Enabled = true;

            DataReset();

            if(ShowQuotation)
            {
                ShowQuotationData();
            }
        }

        public frmItemEdit_NEW(DataTable dt)
        {
            InitializeComponent();

            Text = itemEdit_Edit;
            DataReset();

            txtItemCode.Enabled = false;
            InitialData(dt);

            DT_DATA_SAVED = dt;

            inputDisable();
        }

        public frmItemEdit_NEW(DataTable dt, bool showQuotation)
        {
            InitializeComponent();

            Text = itemEdit_Edit;
            DataReset();

            txtItemCode.Enabled = false;

            InitialData(dt);

            DT_DATA_SAVED = dt;

            inputDisable();

            if (showQuotation)
            {
                ShowQuotationData();
            }
        }

        private void DataReset()
        {
            QuotationDataUIChanged(false);
            ShowSBBSetting(false);
            ShowProductionSetting(false);
            ShowGroupEditButton(false);

            tool.LoadCustomerToComboBoxSBB(cmbCust);
            loadItemCategoryData();
            loadMaterialTypeData();
            loadMasterBatchData();
            unitDataSource();
        }

        #region OBJECT DECLARE

        static public itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatDAL dalItemCat = new itemCatDAL();

        materialBLL uMaterial = new materialBLL();
        materialDAL dalMaterial = new materialDAL();

        SBBDataDAL dalSBBData = new SBBDataDAL();
        joinBLL uJoin = new joinBLL();

        Tool tool = new Tool();
        Text text = new Text();

        static public bool DATA_SAVED = false;
        static public DataTable DT_DATA_SAVED;

        private readonly string itemEdit_Add = "ITEM ADDING";
        private readonly string itemEdit_Edit = "ITEM EDITING";
        private readonly string Type_CommonParts = "COMMON PART";
        private readonly string Type_Products = "BODY OR PRODUCT";
        private readonly string Type_Line = "----------------------------";

        private readonly string Label_Type = "TYPE";
        private readonly string Label_Category = "CATEGORY";


        private readonly string header_Numerator = "NUMERATOR";
        private readonly string header_Denominator = "DENOMINATOR";
        private readonly string header_TblCode = "TBL CODE";
        private readonly string header_SizeString = "SIZE";
        private readonly string header_Unit = "UNIT";
        private readonly string header_SizeLevel = "SIZE LEVEL";

        private readonly string Button_AddItem = "ADD" ;
        private readonly string Button_UpdateItem= "UPDATE";

        private readonly string DEFAULT_SBB_CONTAINER_CODE = "CTR 003";
        private readonly string DEFAULT_SBB_PACKET_CODE = "PELDPEB1622";
        private readonly string DEFAULT_SBB_BAG_CODE = "PPWB2640";

        private readonly string MULTIPLE_CUSTOMER_FOUND = "Multiple Customers Found!";
        private bool QuotationDataVisible = false;
        private bool MODE_ADDING = false;

        private DataTable DT_SBB_TYPE;
        private DataTable DT_SBB_CATEGORY;
        private DataTable DT_SBB_SIZE;
        private DataTable DT_SBB_PACKING;
        #endregion

        #region LOAD DATA
        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";
            cmbCat.SelectedIndex = -1;

            cbZeroCost.Enabled = false;
        }

        private void loadMaterialTypeData()
        {
            DataTable dt = dalMaterial.catSearch("RAW Material");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            cmbRawMaterial.DataSource = distinctTable;
            cmbRawMaterial.DisplayMember = "material_code";
            cmbRawMaterial.SelectedIndex = -1;
        }

        private void loadMasterBatchData()
        {
            tool.loadMasterBatchToComboBox(cmbColorMaterial);
        }

        #endregion

        #region VALIDATION
        private bool Validation()
        {

            bool result = true;

            if (string.IsNullOrEmpty(cmbCat.Text))
            {
                result = false;
                errorProvider3.SetError(lblCategory, "Item Category Required");
            }

            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                result = false;

                errorProvider1.SetError(lblItemCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtItemName.Text))
            {
                result = false;

                errorProvider2.SetError(lblItemName, "Item Name Required");
            }

            if (cbSBB.Checked)
            {

            }

            return result;

        }

        private void InitialData()
        {
            cmbCat.SelectedIndex = -1;
            txtItemCode.Clear();
            txtItemName.Clear();
            cmbRawMaterial.SelectedIndex = -1;
            cmbColorMaterial.SelectedIndex = -1;
            txtColor.Clear();

            txtQuoTon.Clear();
            //txtBestTon.Clear();
            txtProTon.Clear();

            txtQuoCT.Clear();
            //txtProCTFrom.Clear();
            txtProCTTo.Clear();
            txtCavity.Clear();
            txtQuoPWPcs.Clear();
            txtQuoRWPcs.Clear();
            txtProPWPcs.Clear();
            txtProRWPcs.Clear();

            txtProPWShot.Clear();
            txtProRWShot.Clear();
            txtProCooling.Clear();
            txtWastageAllowed.Clear();
        }

        private void InitialData(DataTable dt)
        {
            if(dt !=  null && dt.Rows.Count >= 0)
            {
                MODE_ADDING = false;
                btnSave.Text = Button_UpdateItem;

                string itemCode = dt.Rows[0][dalItem.ItemCode].ToString();

                cmbCat.Text = dt.Rows[0][dalItem.ItemCat].ToString();
                txtItemCode.Text = itemCode;
                txtItemName.Text = dt.Rows[0][dalItem.ItemName].ToString();

                LoadPairedCustomer(itemCode);

                cbAssembly.Checked = bool.TryParse(dt.Rows[0][dalItem.ItemAssemblyCheck].ToString(), out bool checkTest) ? checkTest : false;
                cbProduction.Checked = bool.TryParse(dt.Rows[0][dalItem.ItemProductionCheck].ToString(), out  checkTest) ? checkTest : false;
                cbSBB.Checked = bool.TryParse(dt.Rows[0][dalItem.ItemSBBCheck].ToString(), out  checkTest) ? checkTest : false;

                cmbUnit.Text = dt.Rows[0][dalItem.ItemUnit].ToString();

                float wastage = float.TryParse(dt.Rows[0][dalItem.ItemWastage].ToString(), out wastage)? wastage * 100 : 0;

                txtWastageAllowed.Text = wastage.ToString();

                txtPCSRate.Text = dt.Rows[0][dalItem.ItemUnitToPCSRate].ToString();

                txtQuoTon.Text = dt.Rows[0][dalItem.ItemQuoTon].ToString();
                txtQuoCT.Text = dt.Rows[0][dalItem.ItemQuoCT].ToString();
                txtQuoPWPcs.Text = dt.Rows[0][dalItem.ItemQuoPWPcs].ToString();
                txtQuoRWPcs.Text = dt.Rows[0][dalItem.ItemQuoRWPcs].ToString();

                txtProTon.Text = dt.Rows[0][dalItem.ItemProTon].ToString();
                txtProCTTo.Text = dt.Rows[0][dalItem.ItemProCTTo].ToString();
                txtCavity.Text = dt.Rows[0][dalItem.ItemCavity].ToString();
                txtProCooling.Text = dt.Rows[0][dalItem.ItemProCooling].ToString();

                txtProPWShot.Text = dt.Rows[0][dalItem.ItemProPWShot].ToString();
                txtProRWShot.Text = dt.Rows[0][dalItem.ItemProRWShot].ToString();
                txtProPWPcs.Text = dt.Rows[0][dalItem.ItemProPWPcs].ToString();
                txtProRWPcs.Text = dt.Rows[0][dalItem.ItemProRWPcs].ToString();

                cmbRawMaterial.Text = dt.Rows[0][dalItem.ItemMaterial].ToString();
                cmbColorMaterial.Text = dt.Rows[0][dalItem.ItemMBatch].ToString();

                if(cmbColorMaterial.SelectedIndex < 0)
                {
                    //pigment
                    cbPigment.Checked = true;
                    cmbColorMaterial.Text = dt.Rows[0][dalItem.ItemMBatch].ToString();
                }

                txtColor.Text = dt.Rows[0][dalItem.ItemColor].ToString();
                float mbRate = float.TryParse(dt.Rows[0][dalItem.ItemMBRate].ToString(), out mbRate) ? mbRate * 100 : 0;
                txtMBRate.Text = mbRate.ToString();


                if (cbSBB.Checked)
                {
                   

                    string typeCode = dt.Rows[0][dalItem.TypeTblCode].ToString();
                    string categoryCode = dt.Rows[0][dalItem.CategoryTblCode].ToString();
                    string itemSizeCode_1 = dt.Rows[0][dalItem.ItemSize1].ToString();
                    string itemSizeCode_2 = dt.Rows[0][dalItem.ItemSize2].ToString();

                    if (DT_SBB_TYPE == null || DT_SBB_TYPE.Rows.Count < 0)
                    {
                        LoadSBBTypeData();
                    }

                    foreach(DataRow row in DT_SBB_TYPE.Rows)
                    {
                        if(typeCode.Equals(row[dalSBBData.TableCode].ToString()))
                        {
                            cmbSBBType.Text = row[dalSBBData.TypeName].ToString();
                            break;
                        }
                    }

                    LoadSBBCategoryDataToDataTable();

                    foreach (DataRow row in DT_SBB_CATEGORY.Rows)
                    {
                        if (categoryCode.Equals(row[dalSBBData.TableCode].ToString()))
                        {
                            cmbSBBCategory.Text = row[dalSBBData.CategoryName].ToString();
                            break;
                        }
                    }

                    DataTable dt_Size1 = (DataTable) cmbSBBSize1.DataSource;
                    DataTable dt_Size2 = (DataTable) cmbSBBSize2.DataSource;

                    if(dt_Size1 != null)
                    {
                        foreach(DataRow row in dt_Size1.Rows)
                        {
                            if(row[header_TblCode].ToString().Equals(itemSizeCode_1))
                            {
                                cmbSBBSize1.Text = row[header_SizeString].ToString();
                                break;
                            }
                        }
                    }

                    if (dt_Size2 != null)
                    {
                        foreach (DataRow row in dt_Size2.Rows)
                        {
                            if (row[header_TblCode].ToString().Equals(itemSizeCode_2))
                            {
                                cmbSBBSize2.Text = row[header_SizeString].ToString();
                                break;
                            }
                        }
                    }

                    DataTable dt_StdPacking = dalSBBData.StdPackingSelect();

                    if(dt_StdPacking != null)
                    {
                        LoadSBBPackagingItem();
                        foreach (DataRow row in dt_StdPacking.Rows)
                        {
                            if(itemCode.Equals(row[dalItem.ItemCode].ToString()))
                            {
                                txtPcsPerPacket.Text = row[dalSBBData.QtyPerPacket].ToString();
                                txtPcsPerBag.Text = row[dalSBBData.QtyPerBag].ToString();
                                txtPcsPerContainer.Text = row[dalSBBData.QtyPerContainer].ToString();


                            }
                        }
                    }
                }

                if(cmbRawMaterial.SelectedIndex != -1 && cmbCat.Text.Equals(text.Cat_Part))
                {
                    cbProduction.Checked = true;

                }
            }
            else
            {
                MessageBox.Show("Item not found!");

                InitialData();
            }
        }

        private bool IfProductsExists(String productCode)
        {
            DataTable dt;

            if(cmbCat.Text.Equals("Part"))
            {
                dt = dalItem.Search(productCode);
            }
            else
            {
                dt = dalMaterial.codeSearch(productCode);
            }
            

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region INSERT/UPDATE DATA

        private void GetData()
        {
            //General Data
            uItem.item_cat = cmbCat.Text;
            uItem.item_code = txtItemCode.Text;
            uItem.item_name = txtItemName.Text;

            uItem.item_unit = cmbUnit.Text;

            //item_wastage_allowed
            if (float.TryParse(txtWastageAllowed.Text, out float i))
            {
                if (i <= 0)
                {
                    uItem.item_wastage_allowed = 0.05f;
                }
                else
                {
                    uItem.item_wastage_allowed = i / 100;
                }
            }
            else
            {
                uItem.item_wastage_allowed = 0.05f;
            }

            //item_assembly
            if (cbAssembly.Checked)
            {
                uItem.item_assembly = 1;
            }
            else
            {
                uItem.item_assembly = 0;
            }

            //item_production
            if (cbProduction.Checked)
            {
                uItem.item_production = 1;
                
            }
            else
            {
                uItem.item_production = 0;
            }

            //item_sbb
            if (cbSBB.Checked)
            {
                uItem.item_sbb = 1;
            }
            else
            {
                uItem.item_sbb = 0;
            }


            //Quotation Data
            uItem.item_quo_ton = tool.Int_TryParse(txtQuoTon.Text);
            uItem.item_quo_ct = tool.Int_TryParse(txtQuoCT.Text);
            uItem.item_quo_pw_pcs = tool.Float_TryParse(txtQuoPWPcs.Text);
            uItem.item_quo_rw_pcs = tool.Float_TryParse(txtQuoRWPcs.Text);

            //Production Data
            uItem.item_best_ton = tool.Int_TryParse(txtProTon.Text);
            uItem.item_pro_ton = tool.Int_TryParse(txtProTon.Text);
            uItem.item_pro_ct_from = tool.Int_TryParse(txtProCTTo.Text);
            uItem.item_pro_ct_to = tool.Int_TryParse(txtProCTTo.Text);
            uItem.item_cavity = tool.Int_TryParse(txtCavity.Text);
            uItem.item_pro_cooling = tool.Int_TryParse(txtProCooling.Text);

            uItem.item_pro_pw_shot = tool.Float_TryParse(txtProPWShot.Text);
            uItem.item_pro_rw_shot = tool.Float_TryParse(txtProRWShot.Text);

            uItem.item_pro_pw_pcs = tool.Float_TryParse(txtProPWPcs.Text);
            uItem.item_pro_rw_pcs = tool.Float_TryParse(txtProRWPcs.Text);

            //Material Data
            uItem.item_material = cmbRawMaterial.Text;
            uItem.item_mb = cmbColorMaterial.Text;
            uItem.item_mb_rate = float.TryParse(txtMBRate.Text, out float colorRate) ? colorRate / 100 : 0;
            uItem.item_color = txtColor.Text;

            uItem.unit_to_pcs_rate = float.TryParse(txtPCSRate.Text, out float PcsRate) ? PcsRate : 1;

            //SBB Data
            GetSBBDataTableCode();

            DateTime now = DateTime.Now;

            uItem.item_added_date = now;
            uItem.item_added_by = MainDashboard.USER_ID;

            uItem.item_updtd_date = now;
            uItem.item_updtd_by = MainDashboard.USER_ID;

        }

        private void updateItem()
        {

            GetData();

            float i = uItem.item_mb_rate;
            string unit = uItem.item_unit;


            if (dalItem.ItemMasterList_ItemUpdate(uItem))
            {
                //data updated successfully
                MessageBox.Show("Item updated successfully!");

                if(cmbCust.SelectedIndex != -1 && cmbCat.Text.Equals(text.Cat_Part))
                {
                    pairCustomer();
                }

                //update packaging
                SBBDataBLL uData = new SBBDataBLL();

                int pcsPerPacket = int.TryParse(txtPcsPerPacket.Text, out int j) ? j : 0;
                int pcsPerBag = int.TryParse(txtPcsPerBag.Text, out j) ? j : 0;
                int pcsPerContainer = int.TryParse(txtPcsPerContainer.Text, out j) ? j : 0;

                uData.Max_Lvl = 0;

                uData.Updated_Date = DateTime.Now;
                uData.Updated_By = MainDashboard.USER_ID;

                uData.IsRemoved = false;

                uData.Qty_Per_Container = pcsPerContainer;
                uData.Qty_Per_Packet = pcsPerPacket;
                uData.Qty_Per_Bag = pcsPerBag;

                uData.Item_code = uItem.item_code;

                DataTable dt_Packaging = dalSBBData.StdPackingSelect();

                bool ItemFoundInStdPackaging = false;

                if (dt_Packaging != null)
                {
                    foreach(DataRow row in dt_Packaging.Rows)
                    {
                        if(row[dalSBBData.ItemCode].ToString().Equals(uItem.item_code))
                        {
                            ItemFoundInStdPackaging = true;

                            uData.Table_Code = int.TryParse(row[dalSBBData.TableCode].ToString(), out int tblCode)? tblCode : -1;

                            break;
                        }
                    }
                }

                if(ItemFoundInStdPackaging)
                {
                    if (!dalSBBData.StdPackingUpdate(uData))
                    {
                        MessageBox.Show("Failed to update standard packing data to DB.");
                    }
                }
                else
                {
                    if (pcsPerPacket > 0 || pcsPerBag > 0 || pcsPerContainer > 0)
                    {
                        if (!dalSBBData.InsertStdPacking(uData))
                        {
                            MessageBox.Show("Failed to insert standard packing data to DB.");
                        }
                    }
                }
             

                ReturnDataAfterDataSaved();

                Close();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated item");
            }
        }

        private void GetSBBDataTableCode()
        {
            uItem.Type_tbl_code = -1;
            uItem.Category_tbl_code = -1;
            uItem.Size_tbl_code_1 = -1;
            uItem.Size_tbl_code_2 = -1;

            if (cbSBB.Checked)
            {
                //type item code
                int SBBTypeTblCode = -1;

                if (cmbSBBType.SelectedIndex != -1)
                {
                    string type = cmbSBBType.Text;

                    bool invalidType = type == Type_CommonParts || type == Type_Products || type == Type_Line;

                    if (!invalidType)
                    {
                        int index = cmbSBBType.SelectedIndex;

                        if (index != -1 && DT_SBB_TYPE != null)
                        {
                            SBBTypeTblCode = int.TryParse(DT_SBB_TYPE.Rows[index][dalSBBData.TableCode].ToString(), out SBBTypeTblCode)? SBBTypeTblCode : -1;
                        }
                    }
                }

                uItem.Type_tbl_code = SBBTypeTblCode;

                //category item code
                int SBBCategoryTblCode = -1;

                if (cmbSBBCategory.SelectedIndex != -1)
                {
                    int index = cmbSBBCategory.SelectedIndex;

                    if (index != -1 && DT_SBB_CATEGORY != null)
                    {
                        SBBCategoryTblCode = int.TryParse(DT_SBB_CATEGORY.Rows[index][dalSBBData.TableCode].ToString(), out SBBCategoryTblCode) ? SBBCategoryTblCode : -1;
                    }
                }

                uItem.Category_tbl_code = SBBCategoryTblCode;

                //size 1 item code
                int SBBSizeblCode_1 = -1, SBBSizeblCode_2 = -1;

                if (cmbSBBSize1.SelectedIndex != -1)
                {
                    int index = cmbSBBSize1.SelectedIndex;

                    if (index != -1 && DT_SBB_SIZE != null)
                    {
                        SBBSizeblCode_1 = int.TryParse(DT_SBB_SIZE.Rows[index][dalSBBData.TableCode].ToString(), out SBBSizeblCode_1) ? SBBSizeblCode_1 : -1;
                    }
                }

                if (cmbSBBSize2.SelectedIndex != -1)
                {
                    int index = cmbSBBSize2.SelectedIndex;

                    if (index != -1 && DT_SBB_SIZE != null)
                    {
                        SBBSizeblCode_2 = int.TryParse(DT_SBB_SIZE.Rows[index][dalSBBData.TableCode].ToString(), out SBBSizeblCode_2) ? SBBSizeblCode_2 : -1;
                    }
                }


                uItem.Size_tbl_code_1 = SBBSizeblCode_1;
                uItem.Size_tbl_code_2 = SBBSizeblCode_2;
            }
        }

        private void ReturnDataAfterDataSaved()
        {
            DATA_SAVED = true;

            if(DT_DATA_SAVED != null && DT_DATA_SAVED.Rows.Count > 0)
            {
                DT_DATA_SAVED.Rows[0][dalItem.ItemCat] = cmbCat.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemName] = txtItemName.Text;

                DT_DATA_SAVED.Rows[0][dalItem.ItemAssemblyCheck] = cbAssembly.Checked;
                DT_DATA_SAVED.Rows[0][dalItem.ItemProductionCheck] = cbProduction.Checked;
                DT_DATA_SAVED.Rows[0][dalItem.ItemSBBCheck] = cbSBB.Checked;

                DT_DATA_SAVED.Rows[0][dalItem.ItemUnit] = cmbUnit.Text;

                DT_DATA_SAVED.Rows[0][dalItem.ItemWastage] = float.TryParse(txtWastageAllowed.Text, out float wastage) ? wastage / 100 : 0;

                DT_DATA_SAVED.Rows[0][dalItem.ItemUnitToPCSRate] = float.TryParse(txtPCSRate.Text, out float pcsrate) ? pcsrate : 0; 

                DT_DATA_SAVED.Rows[0][dalItem.ItemQuoTon] = txtQuoTon.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemQuoCT] = txtQuoCT.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemQuoPWPcs] = txtQuoPWPcs.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemQuoRWPcs] = txtQuoRWPcs.Text;

                DT_DATA_SAVED.Rows[0][dalItem.ItemProTon] = txtProTon.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemProCTTo] = txtProCTTo.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemCavity] = txtCavity.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemProCooling] = txtProCooling.Text;

                DT_DATA_SAVED.Rows[0][dalItem.ItemProPWShot] = txtProPWShot.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemProRWShot] = txtProRWShot.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemProPWPcs] = txtProPWPcs.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemProRWPcs] = txtProRWPcs.Text;

                DT_DATA_SAVED.Rows[0][dalItem.ItemMaterial] = cmbRawMaterial.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemMBatch] = cmbColorMaterial.Text;

                DT_DATA_SAVED.Rows[0][dalItem.ItemColor] = txtColor.Text;
                DT_DATA_SAVED.Rows[0][dalItem.ItemMBRate] = txtMBRate.Text;

                if (DT_SBB_TYPE == null || DT_SBB_TYPE.Rows.Count < 0)
                {
                    LoadSBBTypeData();
                }

                foreach (DataRow row in DT_SBB_TYPE.Rows)
                {
                    if (cmbSBBType.Text.Equals(row[dalSBBData.TypeName].ToString()))
                    {
                        DT_DATA_SAVED.Rows[0][dalItem.TypeTblCode] = int.TryParse(row[dalSBBData.TableCode].ToString(), out int i)? i : 0;
                        break;
                    }
                }

                LoadSBBCategoryDataToDataTable();

                foreach (DataRow row in DT_SBB_CATEGORY.Rows)
                {
                    if (cmbSBBCategory.Text.Equals(row[dalSBBData.CategoryName].ToString()))
                    {
                        DT_DATA_SAVED.Rows[0][dalItem.CategoryTblCode] = row[dalSBBData.TableCode].ToString();
                        break;
                    }
                }

                DataTable dt_Size1 = (DataTable)cmbSBBSize1.DataSource;
                DataTable dt_Size2 = (DataTable)cmbSBBSize2.DataSource;

                if (dt_Size1 != null)
                {
                    foreach (DataRow row in dt_Size1.Rows)
                    {
                        if (cmbSBBSize1.Text.Equals(row[header_SizeString].ToString()))
                        {
                            DT_DATA_SAVED.Rows[0][dalItem.ItemSize1] = row[header_TblCode].ToString();
                            break;
                        }
                    }
                }

                if (dt_Size2 != null)
                {
                    foreach (DataRow row in dt_Size2.Rows)
                    {
                        if (cmbSBBSize2.Text.Equals(row[header_SizeString].ToString()))
                        {
                            DT_DATA_SAVED.Rows[0][dalItem.ItemSize2] = row[header_TblCode].ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void insertItem()
        {
            GetData();

            //Inserting Data into Database
            bool success = dalItem.ItemMasterList_ItemAdd(uItem);
            //If the data is successfully inserted then the value of success will be true else false
            if (success)
            {
                //update packaging if SBB checked, and Customer pair if Product

                if (cmbCust.SelectedIndex != -1 && cmbCat.Text.Equals(text.Cat_Part))
                {
                    pairCustomer();
                }

                if (cbSBB.Checked)
                {
                    //update Customer
                    if(string.IsNullOrEmpty(cmbCust.Text) || cmbCust.SelectedIndex <= -1)
                    {
                        if(IfSBBProductItem())
                        {
                            pairCustomer(text.SPP_BrandName);

                        }
                    }
                    else
                    {
                        if (IfSBBProductItem())
                        {
                            pairCustomer();

                        }
                    }

                    //update packaging
                    int pcsPerPacket = int.TryParse(txtPcsPerPacket.Text, out int j) ? j : 0;
                    int pcsPerBag = int.TryParse(txtPcsPerBag.Text, out j) ? j : 0;
                    int pcsPerContainer = int.TryParse(txtPcsPerContainer.Text, out j) ? j : 0;

                    if (pcsPerPacket > 0 || pcsPerBag > 0 || pcsPerContainer > 0)
                    {
                        SBBDataBLL uData = new SBBDataBLL();
                        SBBDataDAL dalData = new SBBDataDAL();

                        uData.Max_Lvl = 0;

                        uData.Updated_Date = DateTime.Now;
                       uData.Updated_By = MainDashboard.USER_ID;

                        uData.Qty_Per_Container = pcsPerContainer;
                        uData.Qty_Per_Packet = pcsPerPacket;
                        uData.Qty_Per_Bag = pcsPerBag;

                        uData.Item_code = uItem.item_code;


                        if (!dalData.InsertStdPacking(uData))
                        {
                            MessageBox.Show("Failed to insert standard packing data to DB.");
                        }
                    }

                    //update price
                    if(IfSBBProductItem())
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to add a new  Price & Discount Rate for this product?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            //password
                            frmVerification frm = new frmVerification(text.PW_UnlockSBBCustomerDiscount)
                            {
                                StartPosition = FormStartPosition.CenterScreen
                            };


                            frm.ShowDialog();

                            if (frmVerification.PASSWORD_MATCHED)
                            {
                                frmSBBPrice frm2 = new frmSBBPrice
                                {
                                    StartPosition = FormStartPosition.CenterScreen
                                };

                                frm2.ShowDialog();
                            }
                        }

                        //update item group
                        dialogResult = MessageBox.Show("Do you want to add a new  Item Group for this product?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            uJoin.join_parent_code = txtItemCode.Text;

                            uJoin.join_child_code = "";

                            frmJoinEdit frm = new frmJoinEdit(uJoin, false);

                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();//Item Edit
                        }
                    }




                }
                MessageBox.Show("Item added successfully!");
                
                Close();
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new item");
            }
        }

        private bool IfExists(string itemCode, string custName)
        {
            itemCustDAL dalItemCust = new itemCustDAL();
            DataTable dt = dalItemCust.existsSearch(itemCode, tool.getCustID(custName).ToString());

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void LoadPairedCustomer(string itemCode)
        {
            itemCustDAL dalItemCust = new itemCustDAL();

            DataTable dt = dalItemCust.Select();

            string Customer = "";

            int customerCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                {
                    Customer = row["cust_name"].ToString();

                    customerCount++;
                }
            }


            if(customerCount < 2)
            {
                cmbCust.Enabled = true;

                cmbCust.Text = Customer;

                lblClearCustomer.Text = "CLEAR";

            }
            else
            {
                cmbCust.DataSource = null;
                cmbCust.Enabled = false;

                lblClearCustomer.Text = MULTIPLE_CUSTOMER_FOUND;
            }
            

        }

        private void pairCustomer()
        {
            itemCustBLL uItemCust = new itemCustBLL();
            itemCustDAL dalItemCust = new itemCustDAL();

            string cust = cmbCust.Text;

            bool SBBItemCheck = true;

            if(cust.Equals(text.SBB_BrandName) || cust.Equals(text.SPP_BrandName))
            {
                cust = text.SPP_BrandName;

                SBBItemCheck = IfSBBProductItem();
            }

            if (cmbCat.Text.Equals("Part") && !string.IsNullOrEmpty(cust) && SBBItemCheck)
            {
                if (!IfExists(txtItemCode.Text, cust))
                {
                    uItemCust.cust_id = Convert.ToInt32(tool.getCustID(cust));
                    uItemCust.item_code = txtItemCode.Text;
                    uItemCust.item_cust_added_date = DateTime.Now;
                    uItemCust.item_cust_added_by = MainDashboard.USER_ID;
                    uItemCust.forecast_one = 0;
                    uItemCust.forecast_two = 0;
                    uItemCust.forecast_three = 0;
                    uItemCust.forecast_current_month = DateTime.Now.ToString("MMMM");

                    bool success = dalItemCust.Insert(uItemCust);

                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new item_cust record");
                    }
                }
            }
        }

        private void pairCustomer(string cust)
        {
            itemCustBLL uItemCust = new itemCustBLL();
            itemCustDAL dalItemCust = new itemCustDAL();

            bool SBBItemCheck = true;

            if (cust.Equals(text.SBB_BrandName) || cust.Equals(text.SPP_BrandName))
            {
                cust = text.SPP_BrandName;

                SBBItemCheck = IfSBBProductItem();
            }

            if (cmbCat.Text.Equals("Part") && !string.IsNullOrEmpty(cust) && SBBItemCheck)
            {
                if (!IfExists(txtItemCode.Text, cust))
                {
                    uItemCust.cust_id = Convert.ToInt32(tool.getCustID(cust));
                    uItemCust.item_code = txtItemCode.Text;
                    uItemCust.item_cust_added_date = DateTime.Now;
                    uItemCust.item_cust_added_by = MainDashboard.USER_ID;
                    uItemCust.forecast_one = 0;
                    uItemCust.forecast_two = 0;
                    uItemCust.forecast_three = 0;
                    uItemCust.forecast_current_month = DateTime.Now.ToString("MMMM");

                    bool success = dalItemCust.Insert(uItemCust);

                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new item_cust record");
                    }
                }
            }
        }

        private void updateMaterial()
        {
            //Update data
            uMaterial.material_cat = cmbCat.Text;
            uMaterial.material_code = txtItemCode.Text;
            uMaterial.material_name = txtItemName.Text;

            if (cbZeroCost.Checked)
            {
                uMaterial.material_zero_cost = 1;
            }
            else
            {
                uMaterial.material_zero_cost = 0;
            }

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

        private void insertMaterial()
        {
            //Add data
            uMaterial.material_cat = cmbCat.Text;
            uMaterial.material_code = txtItemCode.Text;
            uMaterial.material_name = txtItemName.Text;

            if (cbZeroCost.Checked)
            {
                uMaterial.material_zero_cost = 1;
            }
            else
            {
                uMaterial.material_zero_cost = 0;
            }

            if (dalMaterial.Insert(uMaterial))
            {
                //Data Successfully Inserted
                //MessageBox.Show("Material successfully created");
                insertItem();
                Close();
            }
            else
            {
                //Failed to insert data
                dalMaterial.Delete(uMaterial);
                MessageBox.Show("Failed to add new material");
            }
        }
        #endregion

        #region BUTTON ACTION
        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (Validation())
            {
                string Message = "";

                bool ExistingItem = IfProductsExists(txtItemCode.Text);

                if(ExistingItem && btnSave.Text.Contains(Button_AddItem))
                {
                    errorProvider1.SetError(lblItemCode, "Item Code have been used!");
                }
                else
                {
                    if (!ExistingItem)
                    {
                        Message = "Confirm to add new Item?";
                    }
                    else
                    {
                        Message = "Confirm to update?";
                    }

                    DialogResult dialogResult = MessageBox.Show(Message, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                    if (dialogResult == DialogResult.Yes)
                    {
                        if (ExistingItem)
                        {
                            if (cmbCat.Text.Equals(text.Cat_Part))
                            {
                                updateItem();
                            }
                            else
                            {
                                updateMaterial();
                            }
                        }
                        else
                        {
                            //tool.historyRecord(text.LogIn, text.Failed, DateTime.Now, userID);

                            if (cmbCat.Text.Equals(text.Cat_Part))
                            {
                                insertItem();
                            }
                            else
                            {
                                insertMaterial();
                            }
                        }
                    }

                }

               
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region TEXT CHANGE/ INDEX CHANGE

        private void PartItemUI()
        {
            bool isPartItem = cmbCat.Text.Equals(text.Cat_Part);

            if(!isPartItem)
            {
                cbAssembly.Checked = false;
                cbProduction.Checked = false;
                cbSBB.Checked = false;
            }
            else
            {
                cbProduction.Checked = true;

            }

            cbZeroCost.Enabled = !isPartItem;
            cbAssembly.Enabled = isPartItem;
            cbProduction.Enabled = isPartItem;
            cbSBB.Enabled = isPartItem;

        }

        
        private void ShowGroupEditButton(bool show)
        {
            if (show)
            {
                tlpButton.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 100f);

            }
            else
            {
                tlpButton.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);

            }
        }

        private void ShowProductionSetting(bool show)
        {
            if (show)
            {
                if(QuotationDataVisible)
                {
                    tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 80f);

                }
                else
                {
                    tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 30f);

                }

                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 150f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 90f);

            }
            else
            {
                tlpMain.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowSBBSetting(bool show)
        {
            if (show)
            {
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 150f);

                LoadSBBTypeData();

            }
            else
            {
                cmbCust.Enabled = true;
                tlpMain.RowStyles[4] = new RowStyle(SizeType.Absolute, 0f);

            }
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCat.Text))
            {
                errorProvider3.Clear();
            }

            PartItemUI();
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                errorProvider1.Clear();
            }

            if (cmbCat.Text.Equals("RAW Material") || cmbCat.Text.Equals("Master Batch"))
            {
                txtItemName.Text = txtItemCode.Text;
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                errorProvider2.Clear();
            }
        }
        #endregion

        #region KEY PRESS

        private void txtPartWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRunnerWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMcTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        #endregion

        private void inputDisable()
        {
            cmbCat.Enabled = false;
            txtItemCode.Enabled = false;
            txtItemName.Enabled = true;

            cmbRawMaterial.Enabled = true;
            cmbColorMaterial.Enabled = true;
            txtColor.Enabled = true;
            txtQuoTon.Enabled = true;
            //txtBestTon.Enabled = true;
            txtProTon.Enabled = true;
            txtQuoCT.Enabled = true;
           // txtProCTFrom.Enabled = true;
            txtProCTTo.Enabled = true;
            txtCavity.Enabled = true;
            txtQuoPWPcs.Enabled = true;
            txtQuoRWPcs.Enabled = true;
            txtProPWPcs.Enabled = true;
            txtProRWPcs.Enabled = true;
            txtProPWShot.Enabled = true;
            txtProRWShot.Enabled = true;
            txtProCooling.Enabled = true;
            txtWastageAllowed.Enabled = true;

            if (string.IsNullOrEmpty(cmbCat.Text))
            {
                cmbCat.Enabled = true;
                txtItemCode.Enabled = true;
            }
            else if (!cmbCat.Text.Equals("Part"))
            {
                cmbRawMaterial.Enabled = false;
                cmbColorMaterial.Enabled = false;
                txtColor.Enabled = false;
                txtQuoTon.Enabled = false;
                //txtBestTon.Enabled = false;
                txtProTon.Enabled = false;
                txtQuoCT.Enabled = false;
                //txtProCTFrom.Enabled = false;
                txtProCTTo.Enabled = false;
                txtCavity.Enabled = false;
                txtQuoPWPcs.Enabled = false;
                txtQuoRWPcs.Enabled = false;
                txtProPWPcs.Enabled = false;
                txtProRWPcs.Enabled = false;
                txtProPWShot.Enabled = false;
                txtProRWShot.Enabled = false;
                txtProCooling.Enabled = false;
                txtWastageAllowed.Enabled = true;
            }
        }

        private void cbPigment_CheckedChanged(object sender, EventArgs e)
        {
            if(cbPigment.Checked)
            {
                cbMB.Checked = false;
                tool.loadPigmentToComboBox(cmbColorMaterial);
            }

        }

        private void cbMB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMB.Checked)
            {
                cbPigment.Checked = false;
                tool.loadMasterBatchToComboBox(cmbColorMaterial);
            }
        }

        private void tbMBRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void frmItemEdit_Load(object sender, EventArgs e)
        {
           
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbAssembly_CheckedChanged(object sender, EventArgs e)
        {
            //ShowGroupEditButton(cbAssembly.Checked);
        }

        private void unitDataSource()
        {
            string itemCat = cmbCat.Text;

            DataTable dt = new DataTable();
            dt.Columns.Add("item_unit");

            dt.Rows.Add(text.Unit_g);
            dt.Rows.Add(text.Unit_KG);

            dt.Rows.Add(text.Unit_Piece);
            dt.Rows.Add(text.Unit_Set);
            dt.Rows.Add(text.Unit_Packet);
            dt.Rows.Add(text.Unit_Bag);

            dt.Rows.Add(text.Unit_Meter);

            cmbUnit.DataSource = dt;
            cmbUnit.DisplayMember = "item_unit";
            cmbUnit.SelectedIndex = -1;

        }

        private void QuotationDataUIChanged(bool showQuotationData)
        {
            if(showQuotationData)
            {
                //request password
                frmVerification frm = new frmVerification(text.PW_TopManagement)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();

                if (frmVerification.PASSWORD_MATCHED)
                {
                    QuotationDataVisible = true;

                    tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 80f);
                    tlpQuotation.RowStyles[0] = new RowStyle(SizeType.Percent, 0f);
                    tlpQuotation.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);
                }
                else
                {
                    QuotationDataVisible = false;

                    tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 30f);

                    tlpQuotation.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                    tlpQuotation.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);
                }

            }
            else
            {
                
                tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 30f);

                tlpQuotation.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
                tlpQuotation.RowStyles[1] = new RowStyle(SizeType.Percent, 0f);
            }
        }

        private void ShowQuotationData()
        {
            QuotationDataVisible = true;

            tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 80f);
            tlpQuotation.RowStyles[0] = new RowStyle(SizeType.Percent, 0f);
            tlpQuotation.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);
        }

        private void label32_Click(object sender, EventArgs e)
        {
            QuotationDataUIChanged(true);
        }

        private void cbSBB_CheckedChanged(object sender, EventArgs e)
        {
            ShowSBBSetting(cbSBB.Checked);
        }

        private void lblClearRawMaterialData_Click(object sender, EventArgs e)
        {
            cmbRawMaterial.SelectedIndex = -1;
        }

        private void lblClearColorMaterial_Click(object sender, EventArgs e)
        {
            cmbColorMaterial.SelectedIndex = -1;

        }

        private void lblClearSBBTyple_Click(object sender, EventArgs e)
        {
            cmbSBBType.SelectedIndex = -1;

        }

        private void lblClearSize1_Click(object sender, EventArgs e)
        {
            cmbSBBSize1.SelectedIndex = -1;

        }

        private void lblClearSBBCategory_Click(object sender, EventArgs e)
        {
            cmbSBBCategory.SelectedIndex = -1;

        }

        private void lblClearSBBSize2_Click(object sender, EventArgs e)
        {
            cmbSBBSize2.SelectedIndex = -1;

        }

        private void lblClearSBBPacket_Click(object sender, EventArgs e)
        {
            cmbSBBPacket.SelectedIndex = -1;

        }

        private void lblClearSBBBag_Click(object sender, EventArgs e)
        {
            cmbSBBBag.SelectedIndex = -1;

        }

        private void LoadSBBTypeData()
        {

            #region Load Type

            if(DT_SBB_TYPE == null || DT_SBB_TYPE.Rows.Count < 0)
            {
                DT_SBB_TYPE = dalSBBData.TypeWithoutRemovedDataSelect();

                //DataTable distinctTable = dt.DefaultView.ToTable(true, dalSBBData.TypeName);

                DT_SBB_TYPE.DefaultView.Sort = dalSBBData.IsCommon + " DESC," + dalSBBData.TypeName + " ASC";

                DT_SBB_TYPE = DT_SBB_TYPE.DefaultView.ToTable();

                DataRow newRow = DT_SBB_TYPE.NewRow();
                newRow[dalSBBData.TypeName] = Type_CommonParts;
                newRow[dalSBBData.IsCommon] = true;
                DT_SBB_TYPE.Rows.InsertAt(newRow, 0);

                newRow = DT_SBB_TYPE.NewRow();
                newRow[dalSBBData.TypeName] = Type_Line;
                newRow[dalSBBData.IsCommon] = true;
                DT_SBB_TYPE.Rows.InsertAt(newRow, 1);

                foreach (DataRow row in DT_SBB_TYPE.Rows)
                {
                    bool commonPart = bool.TryParse(row[dalSBBData.IsCommon].ToString(), out commonPart) ? commonPart : false;

                    if (!commonPart)
                    {
                        int rowIndex = DT_SBB_TYPE.Rows.IndexOf(row);
                        DT_SBB_TYPE.Rows.InsertAt(DT_SBB_TYPE.NewRow(), rowIndex++);

                        newRow = DT_SBB_TYPE.NewRow();
                        newRow[dalSBBData.TypeName] = Type_Products;
                        DT_SBB_TYPE.Rows.InsertAt(newRow, rowIndex++);

                        newRow = DT_SBB_TYPE.NewRow();
                        newRow[dalSBBData.TypeName] = Type_Line;
                        DT_SBB_TYPE.Rows.InsertAt(newRow, rowIndex);

                        break;
                    }
                }
            }
           
            cmbSBBType.DataSource = DT_SBB_TYPE;
            cmbSBBType.DisplayMember = dalSBBData.TypeName;
            cmbSBBType.SelectedIndex = -1;

            #endregion

        }

        private void LoadSBBCategoryDataToCMB()
        {
            LoadSBBCategoryDataToDataTable();

            cmbSBBCategory.DataSource = DT_SBB_CATEGORY;
            cmbSBBCategory.DisplayMember = dalSBBData.CategoryName;
            cmbSBBCategory.SelectedIndex = -1;

        }

        private void LoadSBBCategoryDataToDataTable()
        {

            if (DT_SBB_CATEGORY == null || DT_SBB_CATEGORY.Rows.Count < 0)
            {
                DT_SBB_CATEGORY = dalSBBData.CategorySelect();

                DT_SBB_CATEGORY.AcceptChanges();

                foreach (DataRow row in DT_SBB_CATEGORY.Rows)
                {
                    string category = row[dalSBBData.CategoryName].ToString();

                    if (category.Equals(" READY GOODS"))
                    {
                        row[dalSBBData.CategoryName] = "PRODUCTS";
                    }
                    else if (category.Equals("ASSEMBLED"))
                    {
                        row.Delete();
                    }
                }

                DT_SBB_CATEGORY.AcceptChanges();

            }
        }

        private void LoadSBBCategoryData(bool isCommon)
        {
            #region Load Category

            DT_SBB_CATEGORY = dalSBBData.CategorySelect();

            DT_SBB_CATEGORY.AcceptChanges();

            foreach (DataRow row in DT_SBB_CATEGORY.Rows)
            {
                string category = row[dalSBBData.CategoryName].ToString();

                if (category.Equals(" READY GOODS"))
                {
                    row[dalSBBData.CategoryName] = "PRODUCTS";
                }
                else if (category.Equals("ASSEMBLED"))
                {
                    row.Delete();
                }
                else if (category.Equals("COMMON PART") && !isCommon)
                {
                    row.Delete();

                }
            }

            DT_SBB_CATEGORY.AcceptChanges();
            cmbSBBCategory.DataSource = DT_SBB_CATEGORY;
            cmbSBBCategory.DisplayMember = dalSBBData.CategoryName;
            cmbSBBCategory.SelectedIndex = -1;

            #endregion
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txtMBRate_TextChanged(object sender, EventArgs e)
        {
            decimal colorRate = decimal.TryParse(txtMBRate.Text, out colorRate) ? colorRate : 0;

            decimal exchange = colorRate / 100;

            lblColorRateExchange.Text = "= " + exchange;
        }

        private void txtWastageAllowed_TextChanged(object sender, EventArgs e)
        {
            decimal WastageRate = decimal.TryParse(txtWastageAllowed.Text, out WastageRate) ? WastageRate : 0;

            decimal exchange = WastageRate / 100;

            lblWastageExchange.Text = "= " + exchange;
        }

        private void cmbSBBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSBBCategory.Enabled = true;
            cmbSBBCategory.DataSource = null;
            cmbSBBSize1.DataSource = null;
            cmbSBBSize2.DataSource = null;

            if (cmbSBBType.SelectedIndex != -1)
            {
                string type = cmbSBBType.Text;


                bool invalidType = type == Type_CommonParts || type == Type_Products || type == Type_Line;

                if (invalidType)
                {
                    cmbSBBType.SelectedIndex = -1;
                    lblSBBCategory.Text = Label_Category;

                }
                else
                {
                    int index = cmbSBBType.SelectedIndex;

                    
                    

                    if (index != -1 && DT_SBB_TYPE != null)
                    {
                        if (btnSave.Text.Contains(Button_AddItem) && IfSBBProductItem())
                        {
                            AutoSetupSBBProductCodeAndName();
                        }
                            string typeName = DT_SBB_TYPE.Rows[index][dalSBBData.TypeName].ToString();

                        bool isCommon = bool.TryParse(DT_SBB_TYPE.Rows[index][dalSBBData.IsCommon].ToString(), out isCommon) ? isCommon : false;

                        LoadSizeData();
                        LoadSBBCategoryData(isCommon);

                        if (isCommon)
                        {
                            lblSBBCategory.Text = Label_Category;

                            cmbSBBCategory.Text = Type_CommonParts;

                            cmbSBBCategory.Enabled = false;
                        }
                        else
                        {
                            lblSBBCategory.Text = Label_Category + " (" + Type_Products + ")";
                             
                            if (cmbSBBCategory.Text.Equals(Type_CommonParts))
                            {
                                cmbSBBCategory.SelectedIndex = -1;
                            }

                        }
                    }
                    else
                    {
                        
                        lblSBBCategory.Text = Label_Category;

                    }
                }
            }
            
        }

        private void LoadSizeData()
        {
            string itemType = cmbSBBType.Text;

            if (DT_SBB_SIZE == null || DT_SBB_SIZE.Rows.Count < 0)
            {
                DT_SBB_SIZE = dalSBBData.SizeForReadyGoodsSelect();

                DT_SBB_SIZE.DefaultView.Sort = dalSBBData.SizeUnit + " DESC," + dalSBBData.SizeWeight + " ASC";
                DT_SBB_SIZE = DT_SBB_SIZE.DefaultView.ToTable();
            }

            DataTable dt_CMB = new DataTable();

            dt_CMB.Columns.Add(header_TblCode);
            dt_CMB.Columns.Add(header_SizeString);
            dt_CMB.Columns.Add(header_Numerator);
            dt_CMB.Columns.Add(header_Denominator);
            dt_CMB.Columns.Add(header_Unit);
            dt_CMB.Columns.Add(header_SizeLevel);

            foreach (DataRow row in DT_SBB_SIZE.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSBBData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    string tableCode = row[dalSBBData.TableCode].ToString();
                    int numerator = int.TryParse(row[dalSBBData.SizeNumerator].ToString(), out numerator) ? numerator : 0;
                    int denominator = int.TryParse(row[dalSBBData.SizeDenominator].ToString(), out denominator) ? denominator : 0;
                    string unit = row[dalSBBData.SizeUnit].ToString();

                    string sizeString = numerator.ToString();

                    if (denominator > 1)
                    {
                        if (numerator == 3 && denominator == 2)
                        {
                            sizeString = "1 1/2";
                        }
                        else
                        {
                            sizeString += "/" + denominator;
                        }

                    }

                    float sizeLevel = numerator / denominator;


                    bool sizeFilter = true;

                    if ((itemType.Contains(text.SBB_TYPE_EQUAL) || itemType.Contains(text.SBB_TYPE_ENDCAP) || itemType.Contains(text.SBB_TYPE_ENDCAP) || itemType.Contains(text.SBB_TYPE_REDUCING)) && numerator < 20)
                    {
                        sizeFilter = false;
                    }

                    if (sizeFilter)
                    {
                        DataRow newRow = dt_CMB.NewRow();

                        newRow[header_TblCode] = tableCode;
                        newRow[header_SizeString] = sizeString;
                        newRow[header_Numerator] = numerator;
                        newRow[header_Denominator] = denominator;
                        newRow[header_Unit] = unit;
                        newRow[header_SizeLevel] = sizeLevel;

                        dt_CMB.Rows.Add(newRow);
                    }

                }

            }

            cmbSBBSize1.DataSource = null;
            cmbSBBSize2.DataSource = null;

            if (dt_CMB.Rows.Count > 0)
            {
                //dt_CMB.DefaultView.Sort = header_Unit + " DESC," + header_SizeLevel + " ASC";
                //dt_CMB = dt_CMB.DefaultView.ToTable();

                cmbSBBSize1.DataSource = dt_CMB;
                cmbSBBSize1.DisplayMember = header_SizeString;
                cmbSBBSize1.SelectedIndex = -1;


                if (!itemType.Contains(text.SBB_TYPE_EQUAL) && !itemType.Contains(text.SBB_TYPE_ENDCAP) && !itemType.Contains(text.SBB_TYPE_POLYORING))
                {
                    cmbSBBSize2.Enabled = true;
                    DataTable dt_Size2 = dt_CMB.Copy();
                    //dt_CMB.DefaultView.Sort = header_SizeLevel + " ASC";
                    cmbSBBSize2.DataSource = dt_Size2;
                    cmbSBBSize2.DisplayMember = header_SizeString;
                    cmbSBBSize2.SelectedIndex = -1;

                }
                else
                {
                    cmbSBBSize2.Enabled = false;
                }

            }

        }

        private void LoadSizeData(bool reloadDB)
        {
            string itemType = cmbSBBType.Text;

            if (DT_SBB_SIZE == null || DT_SBB_SIZE.Rows.Count < 0 || reloadDB)
            {
                DT_SBB_SIZE = dalSBBData.SizeForReadyGoodsSelect();

                DT_SBB_SIZE.DefaultView.Sort = dalSBBData.SizeUnit + " DESC," + dalSBBData.SizeWeight + " ASC";
                DT_SBB_SIZE = DT_SBB_SIZE.DefaultView.ToTable();
            }

            DataTable dt_CMB = new DataTable();

            dt_CMB.Columns.Add(header_TblCode);
            dt_CMB.Columns.Add(header_SizeString);
            dt_CMB.Columns.Add(header_Numerator);
            dt_CMB.Columns.Add(header_Denominator);
            dt_CMB.Columns.Add(header_Unit);
            dt_CMB.Columns.Add(header_SizeLevel);

            foreach (DataRow row in DT_SBB_SIZE.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSBBData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    string tableCode = row[dalSBBData.TableCode].ToString();
                    int numerator = int.TryParse(row[dalSBBData.SizeNumerator].ToString(), out numerator) ? numerator : 0;
                    int denominator = int.TryParse(row[dalSBBData.SizeDenominator].ToString(), out denominator) ? denominator : 0;
                    string unit = row[dalSBBData.SizeUnit].ToString();

                    string sizeString = numerator.ToString();

                    if (denominator > 1)
                    {
                        if (numerator == 3 && denominator == 2)
                        {
                            sizeString = "1 1/2";
                        }
                        else
                        {
                            sizeString += "/" + denominator;
                        }

                    }

                    float sizeLevel = numerator / denominator;


                    bool sizeFilter = true;

                    if ((itemType.Contains(text.SBB_TYPE_EQUAL) || itemType.Contains(text.SBB_TYPE_ENDCAP) || itemType.Contains(text.SBB_TYPE_ENDCAP) || itemType.Contains(text.SBB_TYPE_REDUCING)) && numerator < 20)
                    {
                        sizeFilter = false;
                    }

                    if (sizeFilter)
                    {
                        DataRow newRow = dt_CMB.NewRow();

                        newRow[header_TblCode] = tableCode;
                        newRow[header_SizeString] = sizeString;
                        newRow[header_Numerator] = numerator;
                        newRow[header_Denominator] = denominator;
                        newRow[header_Unit] = unit;
                        newRow[header_SizeLevel] = sizeLevel;

                        dt_CMB.Rows.Add(newRow);
                    }

                }

            }

            cmbSBBSize1.DataSource = null;
            cmbSBBSize2.DataSource = null;

            if (dt_CMB.Rows.Count > 0)
            {
                //dt_CMB.DefaultView.Sort = header_Unit + " DESC," + header_SizeLevel + " ASC";
                //dt_CMB = dt_CMB.DefaultView.ToTable();

                cmbSBBSize1.DataSource = dt_CMB;
                cmbSBBSize1.DisplayMember = header_SizeString;
                cmbSBBSize1.SelectedIndex = -1;


                if (!itemType.Contains(text.SBB_TYPE_EQUAL) && !itemType.Contains(text.SBB_TYPE_ENDCAP) && !itemType.Contains(text.SBB_TYPE_POLYORING))
                {
                    cmbSBBSize2.Enabled = true;
                    DataTable dt_Size2 = dt_CMB.Copy();
                    //dt_CMB.DefaultView.Sort = header_SizeLevel + " ASC";
                    cmbSBBSize2.DataSource = dt_Size2;
                    cmbSBBSize2.DisplayMember = header_SizeString;
                    cmbSBBSize2.SelectedIndex = -1;

                }
                else
                {
                    cmbSBBSize2.Enabled = false;
                }

            }

        }

        private void WeightPerPcsCalculation()
        {
            int cavity = int.TryParse(txtCavity.Text, out cavity) ? cavity : 0;

            decimal PartWeightPerShot = decimal.TryParse(txtProPWShot.Text, out PartWeightPerShot) ? PartWeightPerShot : 0;
            decimal RunnerWeightPerShot = decimal.TryParse(txtProRWShot.Text, out RunnerWeightPerShot) ? RunnerWeightPerShot : 0;

            decimal PartWeightPerPcs = 0;
            decimal PartWeightPerRunner = 0;

            if(cavity > 0)
            {
                PartWeightPerPcs = PartWeightPerShot / cavity;
                PartWeightPerRunner = RunnerWeightPerShot / cavity;
            }

            txtProPWPcs.Text = PartWeightPerPcs.ToString();
            txtProRWPcs.Text = PartWeightPerRunner.ToString();


        }

        private void txtProPWShot_TextChanged(object sender, EventArgs e)
        {
            WeightPerPcsCalculation();
        }

        private void txtProRWShot_TextChanged(object sender, EventArgs e)
        {
            WeightPerPcsCalculation();
        }

        private void txtCavity_TextChanged(object sender, EventArgs e)
        {
            WeightPerPcsCalculation();

        }

        private void cbProduction_CheckedChanged(object sender, EventArgs e)
        {
            ShowProductionSetting(cbProduction.Checked);
        }

        private void cmbSBBCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Category = cmbSBBCategory.Text;

            if(true)//MODE_ADDING
            {
                if (!string.IsNullOrEmpty(Category))
                {
                    LoadSBBPackagingItem();

                }
                else
                {
                    txtPcsPerPacket.Text = "";
                    txtPcsPerBag.Text = "";
                    cmbSBBPacket.DataSource = null;
                    cmbSBBBag.DataSource = null;

                }
            }
             

        }

        private void LoadSBBPackagingItem()
        {
            cmbSBBContainer.DataSource = null;
            cmbSBBPacket.DataSource = null;
            cmbSBBBag.DataSource = null;

            bool ProductCategorySelected = IfSBBProductItem();

            if (cmbSBBCategory.SelectedIndex > -1)
            {
                DataTable dt_Carton = dalItem.CatSearch(text.Cat_Carton);

                cmbSBBContainer.DataSource = dt_Carton;
                cmbSBBContainer.DisplayMember = dalItem.ItemCode;

                cmbSBBContainer.Text = DEFAULT_SBB_CONTAINER_CODE;
            }

            if(ProductCategorySelected)
            {
                if(btnSave.Text.Contains(Button_AddItem))
                {
                    AutoSetupSBBProductCodeAndName();

                }

                cmbCust.Text = text.SBB_BrandName;
                cmbCust.Enabled = false;

                if (DT_SBB_PACKING == null || DT_SBB_PACKING.Rows.Count < 0)
                {
                    DT_SBB_PACKING = dalItem.CatSearch(text.Cat_Packaging);
                }


                if(DT_SBB_PACKING != null && DT_SBB_PACKING.Rows.Count >= 0)
                {
                    DataTable dt_Packet = DT_SBB_PACKING.Copy();

                    cmbSBBPacket.DataSource = dt_Packet;
                    cmbSBBPacket.DisplayMember = dalItem.ItemCode;

                    cmbSBBPacket.Text = DEFAULT_SBB_PACKET_CODE;


                    DataTable dt_Bag = DT_SBB_PACKING.Copy();

                    cmbSBBBag.DataSource = dt_Bag;
                    cmbSBBBag.DisplayMember = dalItem.ItemCode;

                    cmbSBBBag.Text = DEFAULT_SBB_BAG_CODE;

                }
            }
            else
            {
                cmbCust.Enabled = true;

            }

        }

        private void AutoSetupSBBProductCodeAndName()
        {
            string type = cmbSBBType.Text;
            string size1 = cmbSBBSize1.Text;
            string size2 = cmbSBBSize2.Text;

            string SBBStart = "(OK) CF";

            string TypefirstLetters = new String(type.Split(' ').Select(x => x[0]).ToArray());

            string itemCode = SBBStart + TypefirstLetters;
            string itemName = SBBStart + " " + type;

            if (!string.IsNullOrEmpty(size1))
            {
                itemCode += " " + size1;
                itemName += " " + size1;

                if (!string.IsNullOrEmpty(size2))
                {
                    itemCode += " " + size2;
                    itemName += " " + size2;
                }
            }

            if(txtItemCode.Enabled)
            {
                txtItemCode.Text = itemCode;
                txtItemName.Text = itemName;
            }
        }

        private bool IfSBBProductItem()
        {
            bool DBLoaded = false;

            if (DT_SBB_CATEGORY == null || DT_SBB_CATEGORY.Rows.Count < 0)
            {
                LoadSBBCategoryDataToDataTable();

                DBLoaded = true;
            }

            bool ProductCategorySelected = false;
            bool ProductTableCodeFound = false;

            foreach (DataRow row in DT_SBB_CATEGORY.Rows)
            {
                if (row[dalSBBData.TableCode].ToString().Equals("3"))
                {
                    ProductTableCodeFound = true;

                    if (row[dalSBBData.CategoryName].ToString().Equals(cmbSBBCategory.Text))
                    {
                        ProductCategorySelected = true;
                        break;
                    }
                }
            }

            if (!ProductCategorySelected && !ProductTableCodeFound && !DBLoaded)
            {
                LoadSBBCategoryDataToDataTable();

                foreach (DataRow row in DT_SBB_CATEGORY.Rows)
                {
                    if (row[dalSBBData.TableCode].ToString().Equals("3"))
                    {
                        ProductTableCodeFound = true;

                        if (row[dalSBBData.CategoryName].ToString().Equals(cmbSBBCategory.Text))
                        {
                            ProductCategorySelected = true;
                            break;
                        }
                    }
                }
            }

            return ProductCategorySelected;
        }

        private void cmbSBBSize1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnSave.Text.Contains(Button_AddItem) && IfSBBProductItem())
            {
                AutoSetupSBBProductCodeAndName();
            }
        }

        private void lblClearSBBContainer_Click(object sender, EventArgs e)
        {
            cmbSBBContainer.SelectedIndex = -1;
        }

        private void lblClearCustomer_Click(object sender, EventArgs e)
        {

            if(lblClearCustomer.Text.Equals(MULTIPLE_CUSTOMER_FOUND))
            {
                MessageBox.Show("Multipe Customers found!\nPlease edit customer on Item Master Page.");
            }
            else
            {
                cmbCust.SelectedIndex = -1;

            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Close without saving?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void cmbSBBSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnSave.Text.Contains(Button_AddItem) && IfSBBProductItem())
            {
                AutoSetupSBBProductCodeAndName();
            }
        }

        private void SizeEdit_Click(object sender, EventArgs e)
        {
            frmSBBDataSetting frm = new frmSBBDataSetting(frmSBBDataSetting.text_SizeDataList);

            frm.ShowDialog();

            //refresh size table
            if(cmbSBBSize1.DataSource != null)
            LoadSizeData(true);
        }

        private void tlpButton_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            string custName = cmbCust.Text;

            if(custName.Equals(text.SBB_BrandName) || custName.Equals(text.SPP_BrandName))
            {
                cbSBB.Checked = true;
            }
        }
    }
}
