using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBPrice : Form
    {
        public frmSBBPrice()
        {
            InitializeComponent();
            LoadCustomerList();
        }

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();
        planningDAL dalPlan = new planningDAL();

        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();
        private DataTable dt_Product;


        readonly string header_Index = "#";
        readonly string header_Size = "SIZE INT";
        readonly string header_Unit = "UNIT";
        readonly string header_SizeString = "SIZE";
        readonly string header_Type = "TYPE";
        readonly string header_Code = "CODE";

        readonly string header_DefaultPrice = "DEFAULT UNIT PRICE(RM)";
        readonly string header_UnitPrice = "UNIT PRICE(RM)";
        readonly string header_DefaultDiscount = "DEFAULT DISCOUNT(%)";
        readonly string header_Discount = "DISCOUNT(%)";

        readonly string header_OldPrice = "Old UNIT PRICE(RM)";
        readonly string header_OldDefaultPrice = "Old DEFAULT UNIT PRICE(RM)";
        readonly string header_OldDefaultDiscount = "Old DEFAULT DISCOUNT(%)";
        readonly string header_OldDiscount = "Old DISCOUNT(%)";

        readonly string header_Amount = "AMOUNTS(RM)";


        private string oldCellValue = "";
        private string preSelectedCustomer = "";
        private bool dataChanged = false;
        private bool isItemDefaultPriceMode = true;
        private bool Loaded = false;

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_SizeString, typeof(string));

            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));

            dt.Columns.Add(header_OldDefaultPrice, typeof(decimal));
            dt.Columns.Add(header_DefaultPrice, typeof(decimal));
            dt.Columns.Add(header_OldDefaultDiscount, typeof(decimal));
            dt.Columns.Add(header_DefaultDiscount, typeof(decimal));

            dt.Columns.Add(header_OldPrice, typeof(decimal));
            dt.Columns.Add(header_UnitPrice, typeof(decimal));
            dt.Columns.Add(header_OldDiscount, typeof(decimal));
            dt.Columns.Add(header_Discount, typeof(decimal));

            dt.Columns.Add(header_Amount, typeof(decimal));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_Code].Visible = false;
            dgv.Columns[header_Size].Visible = false;
            dgv.Columns[header_Unit].Visible = false;

            dgv.Columns[header_OldPrice].Visible = false;
            dgv.Columns[header_OldDefaultPrice].Visible = false;
            dgv.Columns[header_OldDefaultDiscount].Visible = false;
            dgv.Columns[header_OldDiscount].Visible = false;

            //dgv.Columns[header_Amount].Visible = false;

            dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_SizeString].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[header_UnitPrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_DefaultPrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_DefaultDiscount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Discount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Amount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void frmSBBPrice_Load(object sender, EventArgs e)
        {
            LoadItemList();
            SwitchToItemPrice();
            
        }

        private void LoadCustomerList()
        {
            DataTable dt = dalSPP.CustomerWithoutRemovedDataSelect();

            DataTable custTable = dt.DefaultView.ToTable();//true, dalSPP.FullName

            //DataRow dr;
            //dr = locationTable.NewRow();
            //dr[dalSPP.FullName] = "ALL";

            //locationTable.Rows.InsertAt(dr, 0);

            custTable.DefaultView.Sort = dalSPP.FullName + " ASC";

            cmbCustomer.DataSource = custTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;

            cmbCustomer.SelectedIndex = -1;
        }

        private void btnItemPrice_Click(object sender, EventArgs e)
        {
            SwitchToItemPrice();
        }

        private void btnCustomerDiscount_Click(object sender, EventArgs e)
        {
            SwitchToCustomerDiscount();
        }

        private void LoadItemList()
        {
            string itemCust = text.SPP_BrandName;
            dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);

            dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC";
            dt_Product = dt_Product.DefaultView.ToTable();

            DataTable dt_ItemList = NewItemTable();

            int index = 1;

            DataTable dt_ItemPrice = dalSPP.PriceSelect();

            foreach (DataRow row in dt_Product.Rows)
            {
                string itemCode = row[dalSPP.ItemCode].ToString();
                string itemName = row[dalSPP.ItemName].ToString();
                int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;
                int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                int numerator_2 = int.TryParse(row[dalSPP.SizeNumerator+"1"].ToString(), out numerator_2) ? numerator_2 : 0;
                int denominator_2 = int.TryParse(row[dalSPP.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;
                string sizeUnit_2 = row[dalSPP.SizeUnit + "1"].ToString().ToUpper();

                string typeName = row[dalSPP.TypeName].ToString();

                int pcsStock = readyStock;
                int bagStock = pcsStock / qtyPerBag;


                string sizeString = "";
                int size = 1;
                if (denominator == 1)
                {
                    size = numerator;
                    sizeString = numerator.ToString();

                }
                else
                {
                    size = numerator / denominator;
                    sizeString = numerator + "/" + denominator ;
                }

                if(numerator_2 > 0)
                {
                    if (denominator_2 == 1)
                    {
                        sizeString += " x "+numerator_2 ;

                    }
                    else
                    {
                        sizeString += " x " + numerator_2 + "/" + denominator_2 ;
                    }
                }

                DataRow item_row = dt_ItemList.NewRow();

                string stockString = pcsStock + " PCS";

                item_row[header_Index] = index;
                item_row[header_Code] = itemCode;
                item_row[header_Size] = size;
                item_row[header_Unit] = sizeUnit;
                item_row[header_SizeString] = sizeString;
                item_row[header_Type] = typeName;

                //get item price from database
                if(dt_ItemPrice != null && dt_ItemPrice.Rows.Count > 0)
                {
                    foreach(DataRow priceRow in dt_ItemPrice.Rows)
                    {
                        if(itemCode == priceRow[dalSPP.ItemTblCode].ToString())
                        {
                            item_row[header_DefaultPrice] = priceRow[dalSPP.DefaultPrice];
                            item_row[header_DefaultDiscount] = priceRow[dalSPP.DefaultDiscount];

                            item_row[header_OldDefaultPrice] = priceRow[dalSPP.DefaultPrice];
                            item_row[header_OldDefaultDiscount] = priceRow[dalSPP.DefaultDiscount];

                            break;
                        }
                    }
                }

                //item_row[header_DefaultPrice] = "1.20";
                ////item_row[header_Price] = "1.20";
                //item_row[header_DefaultDiscount] = "78";
                ////item_row[header_Discount] = "82";

                ////item_row[header_OldPrice] = "1.20";
                //item_row[header_OldDefaultDiscount] = "78";
                //item_row[header_OldDiscount] = "82";

                dt_ItemList.Rows.Add(item_row);
                index++;
            }

            dgvItemList.DataSource = dt_ItemList;
            DgvUIEdit(dgvItemList);

            dgvItemList.ClearSelection();

            //if (cmbProductSortBy.Text == text_Type)
            //{
            //    dt_Product.DefaultView.Sort = dalSPP.TypeName + " ASC";
            //    dt_Product = dt_Product.DefaultView.ToTable();
            //}
        }

        private void ResetToSavedData()
        {
            DataGridView dgv = dgvItemList;
            DataTable dt = (DataTable)dgv.DataSource;

            foreach(DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                if(isItemDefaultPriceMode)
                {
                    row[header_DefaultPrice] = row[header_OldDefaultPrice];
                    row[header_DefaultDiscount] = row[header_OldDefaultDiscount];

                    dgv.Rows[rowIndex].Cells[header_DefaultPrice].Style.BackColor = Color.White;
                    dgv.Rows[rowIndex].Cells[header_DefaultDiscount].Style.BackColor = Color.White;
                }
                else
                {
                    row[header_UnitPrice] = row[header_OldPrice];
                    row[header_Discount] = row[header_OldDiscount];

                    dgv.Rows[rowIndex].Cells[header_UnitPrice].Style.BackColor = Color.White;
                    dgv.Rows[rowIndex].Cells[header_Discount].Style.BackColor = Color.White;
                }
            }
        }

        private void SwitchToCustomerDiscount()
        {
            bool permissionToProcess = true;

            if (dataChanged)
            {

                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to switch to discount page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult != DialogResult.Yes)
                {
                    permissionToProcess = false;
                }
                else
                {
                    ResetToSavedData();
                }

            }

            if(permissionToProcess)
            {
                isItemDefaultPriceMode = false;
                btnSave.Visible = false;
                dataChanged = false;
                cmbCustomer.SelectedIndex = -1;
                btnSetAll.Visible = true;
                lblDiscountRate.Visible = true;

                txtSetAll.Clear();
                txtSetAll.Visible = true;

                DataGridView dgv = dgvItemList;
                btnItemPrice.ForeColor = Color.Black;
                btnCustomerDiscount.ForeColor = Color.FromArgb(52, 139, 209);

                lblCustomer.Visible = true;
                cmbCustomer.Visible = true;

                dgv.Columns[header_UnitPrice].Visible = true;
                dgv.Columns[header_DefaultPrice].Visible = false;

                dgv.Columns[header_DefaultDiscount].Visible = false;
                dgv.Columns[header_Discount].Visible = true;
                dgv.Columns[header_Amount].Visible = true;
            }
          
        }

        private void SwitchToItemPrice()
        {
            bool permissionToSwitch = true;

            if (dataChanged)
            {

                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to switch to price page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult != DialogResult.Yes)
                {
                    permissionToSwitch = false;
                }
                else
                {
                    ResetToSavedData();
                }
            }

            if (permissionToSwitch)
            {
                isItemDefaultPriceMode = true;
                btnSave.Visible = false;
                dataChanged = false;
                DataGridView dgv = dgvItemList;

                btnSetAll.Visible = false;
                lblDiscountRate.Visible = false;

                txtSetAll.Visible = false;
                btnCustomerDiscount.ForeColor = Color.Black;
                btnItemPrice.ForeColor = Color.FromArgb(52, 139, 209);

                lblCustomer.Visible = false;
                cmbCustomer.Visible = false;

                dgv.Columns[header_UnitPrice].Visible = false;
                dgv.Columns[header_DefaultPrice].Visible = true;

                dgv.Columns[header_DefaultDiscount].Visible = true;
                dgv.Columns[header_Discount].Visible = false;
                dgv.Columns[header_Amount].Visible = false;
            }
        }

        private void LoadCustomerPrice()
        {
            DataGridView dgv = dgvItemList;

            DataTable dt_ItemList = (DataTable)dgv.DataSource;

            DataTable dt_customerList = (DataTable)cmbCustomer.DataSource;

            string custFullName = cmbCustomer.Text;

            int custTblCode = -1;

            foreach(DataRow row in dt_customerList.Rows)
            {
                if(row[dalSPP.FullName].ToString() == custFullName)
                {
                    custTblCode = int.TryParse(row[dalSPP.TableCode].ToString(), out custTblCode) ? custTblCode : -1;
                    break;
                }
            }

            if(custTblCode != -1)
            {
                DataTable dt_Discount = dalSPP.DiscountSelect();

                if(dt_Discount != null && dt_Discount.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ItemList.Rows)
                    {
                        string itemCode = row[header_Code].ToString();

                        foreach (DataRow discountRow in dt_Discount.Rows)
                        {
                            bool dataMatched = discountRow[dalSPP.CustTblCode].ToString() == custTblCode.ToString();
                            dataMatched &= discountRow[dalSPP.ItemTblCode].ToString() == itemCode;

                            if (dataMatched)
                            {
                                //get unit price
                                //get discount rate
                                //calculate amount
                                //insert data to table

                                decimal unitPrice = decimal.TryParse(discountRow[dalSPP.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                decimal discountRate = decimal.TryParse(discountRow[dalSPP.DiscountRate].ToString(), out discountRate) ? discountRate : 0;

                                decimal amount = (1 - discountRate / 100) * unitPrice;

                                row[header_UnitPrice] = unitPrice;
                                row[header_OldPrice] = unitPrice;
                                row[header_Discount] = discountRate;
                                row[header_OldDiscount] = discountRate;

                                row[header_Amount] = amount;

                                break;
                            }
                        }
                }
                }
              
            }
            else
            {
                MessageBox.Show("Failed to get customer's table code!");
            }
        }

        private void LoadDefaultData()
        {
            DataGridView dgv = dgvItemList;

            DataTable dt = (DataTable)dgv.DataSource;
            bool dataModified = false;

             if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int index = dt.Rows.IndexOf(row);

                    string unitPrice = row[header_UnitPrice].ToString();

                    if (string.IsNullOrEmpty(unitPrice) && !string.IsNullOrEmpty(row[header_DefaultPrice].ToString()))
                    {
                        dataModified = true;
                        row[header_UnitPrice] = row[header_DefaultPrice];
                        dgv.Rows[index].Cells[header_UnitPrice].Style.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        dgv.Rows[index].Cells[header_UnitPrice].Style.BackColor = Color.White;
                    }

                    string priceDiscount = row[header_Discount].ToString();

                    if (string.IsNullOrEmpty(priceDiscount) && !string.IsNullOrEmpty(row[header_DefaultDiscount].ToString()))
                    {
                        dataModified = true;
                        row[header_Discount] = row[header_DefaultDiscount];
                        dgv.Rows[index].Cells[header_Discount].Style.BackColor = Color.LightYellow;

                    }
                    else
                    {
                        dgv.Rows[index].Cells[header_Discount].Style.BackColor = Color.White;
                    }

                    string amount = row[header_Amount].ToString();

                    if (string.IsNullOrEmpty(amount))
                    {

                        decimal unitPrice_Decimal = decimal.TryParse(row[header_UnitPrice].ToString(), out unitPrice_Decimal) ? unitPrice_Decimal : 0;
                        decimal discount_Decimal = decimal.TryParse(row[header_Discount].ToString(), out discount_Decimal) ? discount_Decimal : 0;

                        decimal amount_Decimal = (1 - discount_Decimal / 100) * unitPrice_Decimal;

                        row[header_Amount] = amount_Decimal;

                        //dgv.Rows[index].Cells[header_Amount].Style.BackColor = Color.LightYellow;

                    }
                    else
                    {
                        dgv.Rows[index].Cells[header_Amount].Style.BackColor = Color.White;
                    }
                   
                }
            }

             if(dataModified)
            {
                DataChanged();
                
            }
           
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                if (dataChanged)
                {
                    DialogResult dialogResult = MessageBox.Show("You have unsaved changes for " + preSelectedCustomer + ".\n Do you want to save your changes?", "Message",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        DataTable dt_customerList = (DataTable)cmbCustomer.DataSource;

                        string custFullName = preSelectedCustomer;

                        int custTblCode = -1;

                        foreach (DataRow row in dt_customerList.Rows)
                        {
                            if (row[dalSPP.FullName].ToString() == custFullName)
                            {
                                custTblCode = int.TryParse(row[dalSPP.TableCode].ToString(), out custTblCode) ? custTblCode : -1;
                                break;
                            }
                        }

                        if (custTblCode != -1)
                        {
                            if(SaveCustomerPriceData(custTblCode))
                            {
                                
                                MessageBox.Show("Data saved!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to get customer table code!");
                        }
                    }

                    DataSaved();
                }

                if (cmbCustomer.SelectedIndex != -1)
                {
                    btnSave.Visible = false;

                    ClearData();
                    LoadCustomerPrice();
                    LoadDefaultData();
                }
                else
                {
                    //clear data
                    ClearData();

                }

                preSelectedCustomer = cmbCustomer.Text;
            }
          
        }

        private void ClearData()
        {
            DataGridView dgv = dgvItemList;

            DataTable dt = (DataTable)dgv.DataSource;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[header_UnitPrice] = DBNull.Value;
                    row[header_Discount] = DBNull.Value;
                    row[header_Amount] = DBNull.Value;

                    int rowIndex = dt.Rows.IndexOf(row);
                    dgv.Rows[rowIndex].Cells[header_UnitPrice].Style.BackColor = Color.White;
                    dgv.Rows[rowIndex].Cells[header_Discount].Style.BackColor = Color.White;
                    dgv.Rows[rowIndex].Cells[header_Amount].Style.BackColor = Color.White;

                }
            }
        }

        private bool SaveItemDefaultPriceData()
        {
            DataGridView dgv = dgvItemList;
            //insert or update
            DataTable dt_ItemPrice = dalSPP.PriceSelect();
            DataTable dt = (DataTable) dgv.DataSource;

            if (dt != null && dt.Rows.Count > 0)
            {
                uSpp.Updated_Date = DateTime.Now;
                uSpp.Updated_By = MainDashboard.USER_ID;

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    bool rowDataChanged = row[header_DefaultPrice] != DBNull.Value && row[header_DefaultPrice] != row[header_OldDefaultPrice];
                    rowDataChanged |= row[header_DefaultDiscount] != DBNull.Value && row[header_DefaultDiscount] != row[header_OldDefaultDiscount];

                    if(rowDataChanged)
                    {
                        //get data

                        decimal defaultPrice = decimal.TryParse(row[header_DefaultPrice].ToString(), out defaultPrice) ? defaultPrice : 0;
                        decimal defaultDiscount = decimal.TryParse(row[header_DefaultDiscount].ToString(), out defaultDiscount) ? defaultDiscount : 0;

                        uSpp.Item_tbl_code = row[header_Code].ToString();
                        uSpp.Default_price = defaultPrice;
                        uSpp.Default_discount = defaultDiscount;

                        //update or insert
                        bool dataSaved = false;
                        
                        if(dt_ItemPrice != null && dt_ItemPrice.Rows.Count > 0)
                        {
                            foreach(DataRow priceRow in dt_ItemPrice.Rows)
                            {
                                if (uSpp.Item_tbl_code == priceRow[dalSPP.ItemTblCode].ToString())
                                {
                                    //get table code
                                    int tableCode = int.TryParse(priceRow[dalSPP.TableCode].ToString(), out tableCode) ? tableCode : -1;
                                    uSpp.Table_Code = tableCode;
                                    if(tableCode != -1)
                                    {
                                        if (!dalSPP.PriceUpdate(uSpp))
                                        {
                                            MessageBox.Show("Failed to update item default price info!");
                                            return false;
                                        }
                                        else
                                        {
                                           
                                            dataSaved = true;
                                        }

                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to get table code of item default price table!");
                                        break;
                                    }
                                    //updated data

                                   
                                }
                            }
                        }

                        if(!dataSaved)
                        {
                            //insert data
                            if(!dalSPP.InsertPrice(uSpp))
                            {
                                MessageBox.Show("Failed to insert item default price info!");
                                return false;
                            }
                            else
                            {
                                dataSaved = true;
                            }
                        }

                        if(dataSaved)
                        {
                            row[header_OldDefaultPrice] = defaultPrice;
                            row[header_OldDefaultDiscount] = defaultDiscount;

                            dgv.Rows[rowIndex].Cells[header_DefaultPrice].Style.BackColor = Color.White;
                            dgv.Rows[rowIndex].Cells[header_DefaultDiscount].Style.BackColor = Color.White;
                        }

                    }
                }
            }

            return true;
        }

        private bool SaveCustomerPriceData(int custTblCode)
        {
            DataGridView dgv = dgvItemList;
            //insert or update
            DataTable dt_CustPrice = dalSPP.DiscountSelect();

            DataTable dt = (DataTable)dgv.DataSource;

            if (dt != null && dt.Rows.Count > 0)
            {
                uSpp.Updated_Date = DateTime.Now;
                uSpp.Updated_By = MainDashboard.USER_ID;
                uSpp.Customer_tbl_code = custTblCode;

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    bool rowDataChanged = row[header_UnitPrice] != DBNull.Value && row[header_UnitPrice] != row[header_OldPrice];
                    rowDataChanged |= row[header_Discount] != DBNull.Value && row[header_Discount] != row[header_OldDiscount];


                    if (rowDataChanged)
                    {
                        //get data

                        decimal unitPrice = decimal.TryParse(row[header_UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                        decimal Discount = decimal.TryParse(row[header_Discount].ToString(), out Discount) ? Discount : 0;

                        
                        uSpp.Item_tbl_code = row[header_Code].ToString();
                        uSpp.Unit_price = unitPrice;
                        uSpp.Discount_rate = Discount;

                        //update or insert
                        bool dataSaved = false;

                        if (dt_CustPrice != null && dt_CustPrice.Rows.Count > 0)
                        {
                            foreach (DataRow priceRow in dt_CustPrice.Rows)
                            {
                                int db_CustTblCode = int.TryParse(priceRow[dalSPP.CustTblCode].ToString(), out db_CustTblCode) ? db_CustTblCode : -2;
                                if (uSpp.Item_tbl_code == priceRow[dalSPP.ItemTblCode].ToString() && db_CustTblCode == custTblCode)
                                {
                                    //get table code
                                    int tableCode = int.TryParse(priceRow[dalSPP.TableCode].ToString(), out tableCode) ? tableCode : -1;
                                    uSpp.Table_Code = tableCode;

                                    if (tableCode != -1)
                                    {
                                        if (!dalSPP.DiscountUpdate(uSpp))
                                        {
                                            MessageBox.Show("Failed to update customer discount info!");
                                            return false;
                                        }
                                        else
                                        {

                                            dataSaved = true;
                                        }

                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to get table code of customer discount table!");
                                        break;
                                    }
                                    //updated data


                                }
                            }
                        }

                        if (!dataSaved)
                        {
                            //insert data
                            if (!dalSPP.InsertDiscount(uSpp))
                            {
                                MessageBox.Show("Failed to insert customer discount info!");
                                return false;
                            }
                            else
                            {
                                dataSaved = true;
                            }
                        }

                        if (dataSaved)
                        {
                            row[header_OldPrice] = unitPrice;
                            row[header_OldDiscount] = Discount;

                            dgv.Rows[rowIndex].Cells[header_UnitPrice].Style.BackColor = Color.White;
                            dgv.Rows[rowIndex].Cells[header_Discount].Style.BackColor = Color.White;
                        }

                    }
                }
            }

            return true;
        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView) sender;

            if (e.RowIndex != -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                string selectedHeader = dgv.Columns[col].Name;

                if(selectedHeader == header_DefaultPrice || selectedHeader == header_UnitPrice || selectedHeader == header_DefaultDiscount || selectedHeader == header_Discount )
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.ReadOnly = false;

                    dgv.Rows[row].Cells[col].Selected = true;
                }
                else
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.ReadOnly = true;

                    dgv.Rows[row].Selected = true;

                }
                
               
            }
        }

        private void OnlyDecimalAllowed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }

        private void OnlyIntegerAllowed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void NotAllowedEdit(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void dgvItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView) sender;

                e.Control.KeyPress -= new KeyPressEventHandler(OnlyDecimalAllowed);
                e.Control.KeyPress -= new KeyPressEventHandler(OnlyIntegerAllowed);
                e.Control.KeyPress -= new KeyPressEventHandler(NotAllowedEdit);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                string selectedColName = dgv.Columns[colIndex].Name;

                if (selectedColName == header_DefaultPrice || selectedColName == header_UnitPrice || selectedColName == header_DefaultDiscount || selectedColName == header_Discount) //Desired Column
                {
                    oldCellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(OnlyDecimalAllowed);
                    }


                }
               
                else
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(NotAllowedEdit);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void DataChanged()
        {
            dataChanged = true;
            btnSave.Visible = true;
        }

        private void DataSaved()
        {
            txtSetAll.Clear();
            dataChanged = false;
            btnSave.Visible = false;
        }

        private void dgvItemList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = (DataGridView) sender;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (rowIndex >= 0)
            {
                string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
                string cellHeader = dgv.Columns[colIndex].Name;

                decimal valueInDecimal = decimal.TryParse(cellValue, out valueInDecimal) ? valueInDecimal : -1;

                if (valueInDecimal != -1)
                {
                    valueInDecimal = decimal.Round(valueInDecimal, 2, MidpointRounding.AwayFromZero);

                    if (cellHeader == header_UnitPrice)
                        valueInDecimal = Convert.ToDecimal(string.Format("{0:F2}", valueInDecimal));

                    dgv.Rows[rowIndex].Cells[colIndex].Value = valueInDecimal;

                    if(valueInDecimal.ToString() != oldCellValue)
                    {
                        DataChanged();

                        if (cellHeader == header_Discount)
                        {
                            decimal unitPrice = decimal.TryParse(dgv.Rows[rowIndex].Cells[header_UnitPrice].Value.ToString(), out unitPrice) ? unitPrice : 0;

                            decimal amount = (1 - valueInDecimal / 100) * unitPrice;

                            dgv.Rows[rowIndex].Cells[header_Amount].Value = amount;
                            dgv.Rows[rowIndex].Cells[header_Amount].Style.BackColor = Color.LightYellow;
                        }

                        if (cellHeader == header_UnitPrice)
                        {
                            decimal priceDiscount = decimal.TryParse(dgv.Rows[rowIndex].Cells[header_Discount].Value.ToString(), out priceDiscount) ? priceDiscount : 0;

                            decimal amount = (1 - priceDiscount / 100) * valueInDecimal;

                            dgv.Rows[rowIndex].Cells[header_Amount].Value = amount;
                            dgv.Rows[rowIndex].Cells[header_Amount].Style.BackColor = Color.LightYellow;
                        }

                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.LightYellow;
                    }
           
                }

                if (cellHeader == header_Discount && cellValue != dgv.Rows[rowIndex].Cells[header_OldDiscount].Value.ToString())
                {
                    dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.LightYellow;
                }

                else if (cellHeader == header_UnitPrice && cellValue != dgv.Rows[rowIndex].Cells[header_OldPrice].Value.ToString())
                {
                    dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.LightYellow;
                }
                else if (cellHeader == header_DefaultPrice && cellValue != dgv.Rows[rowIndex].Cells[header_OldDefaultPrice].Value.ToString())
                {
                    dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.LightYellow;
                    DataChanged();
                }
                else if (cellHeader == header_DefaultDiscount && cellValue != dgv.Rows[rowIndex].Cells[header_OldDefaultDiscount].Value.ToString())
                {
                    dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.LightYellow;
                    DataChanged();
                }
                else
                {
                    dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                }

                if (!isItemDefaultPriceMode)
                    CustomerPriceChangedChecking();

                else
                    DefaultPriceChangedChecking();
            }
        }

        private void CustomerPriceChangedChecking()
        {
            DataGridView dgv = dgvItemList;
            DataTable dt = (DataTable)dgv.DataSource;

            foreach(DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                if(row[header_UnitPrice] != row[header_OldPrice])
                {
                    //MessageBox.Show("Data changed found!");
                    return;
                }

                if (row[header_Discount] != row[header_OldDiscount])
                {
                    //MessageBox.Show("Data changed found!");
                    return;
                }
            }

            //no data changed found
            dataChanged = false;
            btnSave.Visible = false;
        }

        private void DefaultPriceChangedChecking()
        {
            DataGridView dgv = dgvItemList;
            DataTable dt = (DataTable)dgv.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                if (row[header_DefaultPrice] != row[header_OldDefaultPrice])
                {
                    //MessageBox.Show("Data changed found!");
                    return;
                }

                if (row[header_DefaultDiscount] != row[header_OldDefaultDiscount])
                {
                    //MessageBox.Show("Data changed found!");
                    return;
                }
            }

            //no data changed found
            dataChanged = false;
            btnSave.Visible = false;
        }
        private void frmSBBPrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataChanged)
            {
                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
               
            }
        }

        private void cmbCustomer_DropDown(object sender, EventArgs e)
        {
           
        }

        private void cmbCustomer_Click(object sender, EventArgs e)
        {

            //if (dataChanged)
            //{
            //    DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to leave this page?", "Message",
            //                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (dialogResult != DialogResult.Yes)
            //    {
            //        cmbCustomer.Enabled = true;
            //        cmbCustomer.DroppedDown = true;
            //    }

            //}
        }

        private void cmbCustomer_Enter(object sender, EventArgs e)
        {
            
        }

        private void cmbCustomer_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("yes");
        }

        private void cmbCustomer_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void cmbCustomer_MouseEnter_1(object sender, EventArgs e)
        {
          
        }

        private void cmbCustomer_Enter_1(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool dataSaved = false;

            if(isItemDefaultPriceMode)
            {
                dataSaved = SaveItemDefaultPriceData();
            }
            else
            {
                DataTable dt_customerList = (DataTable)cmbCustomer.DataSource;

                string custFullName = cmbCustomer.Text;

                int custTblCode = -1;

                foreach (DataRow row in dt_customerList.Rows)
                {
                    if (row[dalSPP.FullName].ToString() == custFullName)
                    {
                        custTblCode = int.TryParse(row[dalSPP.TableCode].ToString(), out custTblCode) ? custTblCode : -1;
                        break;
                    }
                }

                if(custTblCode != -1)
                dataSaved = SaveCustomerPriceData(custTblCode);

                else
                {
                    MessageBox.Show("Failed to get customer table code!");
                }
            }

            if(dataSaved)
            {
                DataSaved();
                MessageBox.Show("Data saved!");
            }
        }

        private void frmSBBPrice_Shown(object sender, EventArgs e)
        {
            Loaded = true;
        }

        private void txtSetAll_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnSetAll_Click(object sender, EventArgs e)
        {
            string discount = txtSetAll.Text;

            if(cmbCustomer.SelectedIndex != -1 && decimal.TryParse(discount, out decimal i))
            {
                DataTable dt = (DataTable)dgvItemList.DataSource;

                foreach(DataRow row in dt.Rows)
                {
                    string itemCode = row[header_Code].ToString();

                    if(!string.IsNullOrEmpty(itemCode))
                    {
                        row[header_Discount] = discount;
                        DataChanged();
                    }
                }
            }
        }
    }
}
