using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Syncfusion.XlsIO;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBNewPO : Form
    {
        public frmSBBNewPO()
        {
            InitializeComponent();
            dt_CustomerList = dalData.CustomerWithoutRemovedDataSelect();
            dt_TypeList = dalData.TypeForReadyGoodsSelect();
            dt_SizeList = dalData.SizeForReadyGoodsSelect();
            //dt_ReadyGoods = dalItem.SPPReadyGoodsSelect();

            RearrangeGoodsList(dalItem.SPPReadyGoodsSelect());

            dt_OrderList = NewOrderTable();
            customerTblCode = "";
        }

        private void RearrangeGoodsList(DataTable dt)
        {
            dt.Columns.Add(header_Size_1);
            dt.Columns.Add(header_Size_2);

            DataTable dt_NewGoodsList = dt.Clone();


            foreach (DataRow row in dt.Rows)
            {
                string itemCode = row["CODE"].ToString();
                string itemType = row["TYPE"].ToString();

                bool itemFound = false;

                int sizeTableCode = int.TryParse(row[dalData.TableCode].ToString(), out sizeTableCode) ? sizeTableCode : 0;

                foreach(DataRow row_New in dt_NewGoodsList.Rows)
                {
                    string itemCode_Seaching = row_New["CODE"].ToString();

                    if(itemCode_Seaching == itemCode)
                    {
                        itemFound = true;
                        //int size_1 = int.TryParse(row_New["SIZE"].ToString(), out size_1) ? size_1 : 0;

      
                        row_New[header_Size_2] = sizeTableCode;
                    }
                }

                if(!itemFound)
                {
                    
                    row[header_Size_1] = sizeTableCode;

                    if(itemType.Equals(text.Type_PolyNipple))
                    {
                        row[header_Size_2] = sizeTableCode;
                    }

                    dt_NewGoodsList.ImportRow(row);

                }
            }

         
            dt_ReadyGoods = dt_NewGoodsList;
        }

        public frmSBBNewPO(string poCode)//edit po
        {
            InitializeComponent();
            customerTblCode = "";
            dt_CustomerList = dalData.CustomerWithoutRemovedDataSelect();
            dt_TypeList = dalData.TypeForReadyGoodsSelect();
            dt_SizeList = dalData.SizeForReadyGoodsSelect();
            
            RearrangeGoodsList(dalItem.SPPReadyGoodsSelect());
            dt_OrderList = NewOrderTable();

            btnAdd.Text = text_Update;
            btnRemove.Visible = true;
            poEditing = true;

            EditingPOCode = poCode;

            LoadExistingPO(poCode);
            UpdateTotalPrice();

        }

        #region variable/object declare

        SBBDataDAL dalData = new SBBDataDAL();
        SBBDataBLL uData = new SBBDataBLL();

        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        DataTable dt_CustomerList;
        DataTable dt_OrderList;
        DataTable dt_TypeList;
        DataTable dt_SizeList;
        DataTable dt_ReadyGoods;
        DataTable dt_OriginalToEditList;

        private bool loaded = false;
        private bool ConvertFromPcs = false;
        private bool ConvertFromBags = false;

        private readonly string header_Index = "#";
        private readonly string header_Code = "CODE";
        private readonly string header_Size_1 = "SIZE_1";
        private readonly string header_Unit_1 = "UNIT_1";
        private readonly string header_Size_2 = "SIZE_2";
        private readonly string header_Unit_2 = "UNIT_2";
        private readonly string header_Numerator = "NUMERATOR";
        private readonly string header_Denominator = "DENOMINATOR";
        private readonly string header_SizeString = "SIZE";
        private readonly string header_Unit = "UNIT";
        private readonly string header_SizeLevel = "SIZE LEVEL";
        private readonly string header_SizeTblCode_1 = "SIZE TABLE CODE 1";
        private readonly string header_SizeTblCode_2 = "SIZE TABLE CODE 2";
        private readonly string header_UnitPrice = "UNIT PRICE(RM)";
        private readonly string header_Discount = "DISC.";
        private readonly string header_Total = "SUB TOTAL(RM)";


        private readonly string header_Type = "TYPE";
        private readonly string header_TblCode = "TBL CODE";
        private readonly string header_DataMode = "DATA MODE";
        private readonly string header_OrderQty = "ORDER QTY(PCS)";
        private readonly string header_Note = "NOTE";
        private readonly string text_New = "NEW";
        private readonly string text_ToRemove = "TO REMOVE";
        private readonly string text_ToEdit = "TO EDIT";
        private readonly string text_ToUpdate = "TO UPDATE";
        private readonly string text_Remove = "REMOVE";
        private readonly string text_UndoRemove = "UNDO REMOVE";
        private readonly string text_Update = "UPDATE";
        private readonly string text_AddItem = "ADD ITEM";
        private readonly string text_UpdateItem = "UPDATE ITEM";
        private readonly string text_TotalPrice = "Total Price: ";

        private string EditingPOCode;
        private string EditingPOTableCode = "";
        private string POBeginingNo = "";
        static public bool poEdited = false;
        static public bool poRemoved = false;
        private bool poEditing = false;
        private string customerTblCode = "";
        private int stdPackingPerBag = 0;
        private int stdPackingPerPacket = 0;

        #endregion

        private DataTable NewOrderTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_TblCode, typeof(string));
            dt.Columns.Add(header_DataMode, typeof(string));

            dt.Columns.Add(header_Size_1, typeof(string));
            dt.Columns.Add(header_Unit_1, typeof(string));

            dt.Columns.Add(header_Size_2, typeof(string));
            dt.Columns.Add(header_Unit_2, typeof(string));

            dt.Columns.Add(header_SizeString, typeof(string));

            dt.Columns.Add(header_SizeTblCode_1, typeof(string));
            dt.Columns.Add(header_SizeTblCode_2, typeof(string));

            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));

            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_UnitPrice, typeof(decimal));
            dt.Columns.Add(header_Discount, typeof(decimal));
            dt.Columns.Add(header_Total, typeof(decimal));

            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[header_Unit_1].Visible = false;
            dgv.Columns[header_SizeTblCode_1].Visible = false;
            dgv.Columns[header_SizeTblCode_2].Visible = false;
            dgv.Columns[header_Size_1].Visible = false;
            dgv.Columns[header_Size_2].Visible = false;
            dgv.Columns[header_Unit_2].Visible = false;
            dgv.Columns[header_TblCode].Visible = false;

            dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_DataMode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_UnitPrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Discount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
           
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_SizeString].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

        }


        private void LoadExistingPO(string poCode)
        {

            DataTable dt = dalData.POSelectWithSizeAndType();

            loadLocationData(dt_CustomerList, cmbCustomer, dalData.FullName);

            foreach (DataRow row in dt.Rows)
            {
                if(poCode == row[dalData.POCode].ToString())
                {
                    cbUseBillingAddress.Checked = bool.TryParse(row[dalData.DefaultShippingAddress].ToString(), out bool billAddress) ? billAddress : true;

                    customerTblCode = row[dalData.CustTblCode].ToString();
                    cmbCustomer.Text = row[dalData.FullName].ToString();
                    dtpPODate.Text = row[dalData.PODate].ToString();
                    txtPONo.Text = row[dalData.PONo].ToString();

                    txtAddress1.Text = row[dalData.Address1].ToString();
                    txtAddress2.Text = row[dalData.Address2].ToString();
                    txtAddress3.Text = row[dalData.Address3].ToString();

                    txtCity.Text = row[dalData.AddressCity].ToString();
                    txtState.Text = row[dalData.AddressState].ToString();
                    txtCountry.Text = row[dalData.AddressCountry].ToString();
                    txtPostalCode.Text = row[dalData.AddressPostalCode].ToString();

                    txtFullName.Text = row[dalData.ShippingFullName].ToString();
                    txtShortName.Text = row[dalData.ShippingShortName].ToString();
                    txtTransporterName.Text = row[dalData.ShippingTransporter].ToString();
                    txtTel.Text = row[dalData.Phone1].ToString();

                    txtShowInDO.Text = row[dalData.RemarkInDO].ToString();

                    int priorityLevel = int.TryParse(row[dalData.PriorityLevel].ToString(), out priorityLevel) ? priorityLevel :-1;

                    if(priorityLevel > 0)
                    {
                        cbUrgent.Checked = true;
                        DateTime TargetDeliveryDate = DateTime.TryParse(row[dalData.TargetDeliveryDate].ToString(), out TargetDeliveryDate)? TargetDeliveryDate : DateTime.MinValue;
                        dtpTargetDeliveryDate.Value = TargetDeliveryDate;
                    }

                    DataRow dt_Row = dt_OrderList.NewRow();

                    int numerator = int.TryParse(row[dalData.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                    int denominator = int.TryParse(row[dalData.SizeDenominator].ToString(), out denominator) ? denominator : 1;
              

                    int numerator_2 = int.TryParse(row[dalData.SizeNumerator + "1"].ToString(), out numerator_2) ? numerator_2 : 0;
                    int denominator_2 = int.TryParse(row[dalData.SizeDenominator + "1"].ToString(), out denominator_2) ? denominator_2 : 1;


                    string sizeString_1 = "";
                    string sizeString_2 = "";

                    int size_1 = 1;
                    int size_2 = 1;

                    if (denominator == 1)
                    {
                        size_1 = numerator;
                        sizeString_1 = numerator.ToString();

                    }
                    else
                    {
                        size_1 = numerator / denominator;
                        sizeString_1 = numerator + "/" + denominator;
                    }

                    if (numerator_2 > 0)
                    {
                        if (denominator_2 == 1)
                        {
                            sizeString_2 += numerator_2.ToString();
                            size_2 = numerator_2;

                        }
                        else
                        {
                            size_2 = numerator_2 / denominator_2;

                            if (numerator_2 == 3 && denominator_2 == 2)
                            {
                                sizeString_2 +=  "1 1" + "/" + denominator_2;
                            }
                            else
                            {
                                sizeString_2 +=  numerator_2 + "/" + denominator_2;
                            }



                        }
                    }

                    string sizeString = "";

                    if (!string.IsNullOrEmpty(sizeString_2))
                    {
                        sizeString = sizeString_1 + " x " + sizeString_2;

                   
                    }
                    else
                    {
                        sizeString = size_1.ToString();

                    }

                    dt_Row[header_TblCode] = row[dalData.TableCode];
                    dt_Row[header_DataMode] = text_ToUpdate;
                    dt_Row[header_SizeString] = sizeString;
                    dt_Row[header_Size_1] = sizeString_1;
                    dt_Row[header_Unit_1] = row[dalData.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Size_2] = sizeString_2;
                    dt_Row[header_Unit_2] = row[dalData.SizeUnit+"1"].ToString().ToUpper();
                    dt_Row[header_Code] = row[dalData.ItemCode];
                    dt_Row[header_Type] = row[dalData.TypeName];
                    dt_Row[header_OrderQty] = row[dalData.POQty];
                    dt_Row[header_Note] = row[dalData.PONote];

                    dt_Row[header_UnitPrice] = row[dalData.UnitPrice];
                    dt_Row[header_Discount] = row[dalData.DiscountRate];
                    dt_Row[header_Total] = row[dalData.SubTotal];

                    dt_OrderList.Rows.Add(dt_Row);

                }
            }

            dt_OrderList = AddTypeDivideSpace(dt_OrderList);
            AddIndexToOrderTable();

            dgvPOItemList.DataSource = dt_OrderList;

            if(poEditing)
            {
                dt_OriginalToEditList = dt_OrderList.Copy();
            }

            DgvUIEdit(dgvPOItemList);
            dgvPOItemList.ClearSelection();
        }


        private void ShowShippingInfo()
        {
            tlpDataInput.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            tlpDataInput.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
        }

        private void ShowAddItemField()
        {
            tlpDataInput.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
            tlpDataInput.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);

            btnAddItem.Width = 200;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvPOItemList;

            int rowIndex = dgv.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                string dataMode = dgv.Rows[rowIndex].Cells[header_DataMode].Value.ToString();

                DialogResult dialogResult;

                if (dataMode == text_ToRemove)
                {
                    dialogResult = MessageBox.Show("Confirm to undo remove this item from order list?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    dialogResult = MessageBox.Show("Confirm to remove this item from order list?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                
                if (dialogResult == DialogResult.Yes)
                {
                    btnAddItem.Text = text_AddItem;
                    btnEdit.Visible = false;
                    //dt_OrderList.Rows.RemoveAt(rowIndex);

                    

                    if(dataMode == text_ToRemove)
                    {
                        dgv.Rows[rowIndex].Cells[header_DataMode].Value = dgv.Rows[rowIndex].Cells[header_TblCode].Value.ToString();
                        dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if(!string.IsNullOrEmpty(dataMode))
                    {
                        dgv.Rows[rowIndex].Cells[header_DataMode].Value = text_ToRemove;
                        dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                    }

                    BigSprayJetPriceSetBaseOnQty();
                    SmallSprayJetPriceSetBaseOnQty();

                    //AddIndexToOrderTable();
                    // dgv.DataSource = dt_OrderList;

                    //DgvUIEdit(dgv);
                    dgv.ClearSelection();
                    btnRemoveItem.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
            UpdateTotalPrice();
            #region old add item
            ////btnEdit.Visible = false;
            //frmSPPAddItem frm = new frmSPPAddItem(true)
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};


            //frm.ShowDialog();

            //if (frmSPPAddItem.itemAdded)
            //{
            //    DataRow dt_Row = dt_OrderList.NewRow();

            //    dt_Row[header_Size] = frmSPPAddItem.item_Size;
            //    dt_Row[header_Unit] = "MM";
            //    dt_Row[header_Code] = frmSPPAddItem.item_Code;
            //    dt_Row[header_Type] = frmSPPAddItem.item_Type;
            //    dt_Row[header_OrderQty] = frmSPPAddItem.item_Target_Pcs;
            //    dt_Row[header_Note] = "("+frmSPPAddItem.item_Target_Bags+" BAGS) " + frmSPPAddItem.item_Note;

            //    dt_OrderList.Rows.Add(dt_Row);

            //    AddIndexToOrderTable();
            //    dgvPOItemList.DataSource = dt_OrderList;
            //    DgvUIEdit(dgvPOItemList);
            //    dgvPOItemList.ClearSelection();

            //    frmSPPAddItem.itemAdded = false;
            //}
            #endregion
        }

        private void AddIndexToOrderTable()
        {
            int index = 1;
            foreach(DataRow row in dt_OrderList.Rows)
            {
                if(!string.IsNullOrEmpty(row[header_Type].ToString()))
                {
                    row[header_Index] = index;
                    index++;
                }
               
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void loadLocationData(DataTable dt, ComboBox cmb, string columnName)
        {
            DataTable locationTable = dt.DefaultView.ToTable(true, columnName);

            locationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = locationTable;
            cmb.DisplayMember = columnName;

        }

        private void LoadSizeData(DataTable dt_Size)
        {
            DataTable dt_CMB = new DataTable();

            dt_CMB.Columns.Add(header_TblCode);
            dt_CMB.Columns.Add(header_SizeString);
            dt_CMB.Columns.Add(header_Numerator);
            dt_CMB.Columns.Add(header_Denominator);
            dt_CMB.Columns.Add(header_Unit);
            dt_CMB.Columns.Add(header_SizeLevel);

            // dt_CMB.Columns.Add(header_Unit_2cmcmdc);

            foreach (DataRow row in dt_Size.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(!isRemoved)
                {
                    string tableCode = row[dalData.TableCode].ToString();
                    int numerator = int.TryParse(row[dalData.SizeNumerator].ToString(), out numerator) ? numerator : 0;
                    int denominator = int.TryParse(row[dalData.SizeDenominator].ToString(), out denominator) ? denominator : 0;
                    string unit = row[dalData.SizeUnit].ToString();

                    string sizeString = numerator.ToString();

                    if(denominator > 1)
                    {
                        sizeString += "/" + denominator;
                    }

                    float sizeLevel = numerator / denominator;

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

            cmbSize_1.DataSource = null;
            cmbSize_2.DataSource = null;

            if (dt_CMB.Rows.Count > 0)
            {


                dt_CMB.DefaultView.Sort = header_SizeLevel + " ASC";
                cmbSize_1.DataSource = dt_CMB;
                cmbSize_1.DisplayMember = header_SizeString;

                cmbSize_2.DataSource = dt_CMB;
                cmbSize_2.DisplayMember = header_SizeString;
            }

        }

        private void LoadSizeData(DataTable dt_Size, string itemType)
        {
            DataTable dt_CMB = new DataTable();

            dt_CMB.Columns.Add(header_TblCode);
            dt_CMB.Columns.Add(header_SizeString);
            dt_CMB.Columns.Add(header_Numerator);
            dt_CMB.Columns.Add(header_Denominator);
            dt_CMB.Columns.Add(header_Unit);
            dt_CMB.Columns.Add(header_SizeLevel);

            // dt_CMB.Columns.Add(header_Unit_2cmcmdc);

            foreach (DataRow row in dt_Size.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    string tableCode = row[dalData.TableCode].ToString();
                    int numerator = int.TryParse(row[dalData.SizeNumerator].ToString(), out numerator) ? numerator : 0;
                    int denominator = int.TryParse(row[dalData.SizeDenominator].ToString(), out denominator) ? denominator : 0;
                    string unit = row[dalData.SizeUnit].ToString();

                    string sizeString = numerator.ToString();

                    if (denominator > 1)
                    {
                        if(numerator == 3 && denominator == 2)
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

                    //if (numerator >= 20)
                    //{
                    //    sizeFilter = itemType.Contains(text.SBB_TYPE_EQUAL) || itemType.Contains(text.SBB_TYPE_REDUCING);
                    //}
                    //else
                    //{
                    //    sizeFilter = itemType.Contains(text.SBB_TYPE_MALE) || itemType.Contains(text.SBB_TYPE_FEMALE);
                    //}



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

            cmbSize_1.DataSource = null;
            cmbSize_2.DataSource = null;

            if (dt_CMB.Rows.Count > 0)
            {


                //dt_CMB.DefaultView.Sort = header_SizeLevel + " ASC";
                cmbSize_1.DataSource = dt_CMB;
                cmbSize_1.DisplayMember = header_SizeString;


                if(!itemType.Contains(text.SBB_TYPE_SPRINKLER) && !itemType.Contains(text.SBB_TYPE_SPRAYJET) && !itemType.Contains(text.SBB_TYPE_EQUAL) && !itemType.Contains(text.SBB_TYPE_ENDCAP) && !itemType.Contains(text.SBB_TYPE_POLYORING))
                {
                    cmbSize_2.Enabled = true;
                    DataTable dt_Size2 = dt_CMB.Copy();
                    //dt_CMB.DefaultView.Sort = header_SizeLevel + " ASC";
                    cmbSize_2.DataSource = dt_Size2;
                    cmbSize_2.DisplayMember = header_SizeString;
                }
                else
                {
                    cmbSize_2.Enabled = false;
                }
               
            }


            if(itemType.Contains(text.SBB_TYPE_SPRAYJET))
            {
                cmbSize_1.Text = "360";
            }
            else if (itemType.Contains(text.SBB_TYPE_SPRINKLER))
            {
                cmbSize_1.Text = "323";

            }
        }

        private void frmSPPNewPO_Load(object sender, EventArgs e)
        {
            
            loadLocationData(dt_TypeList, cmbType, dalData.TypeName);
            //loadLocationData(dt_SizeList, cmbSize_1, dalData.SizeNumerator);
            //LoadSizeData(dt_SizeList);

            if (!poEditing)
            {
                cmbCustomer.SelectedIndex = -1;
                loadLocationData(dt_CustomerList, cmbCustomer, dalData.FullName);
                cmbCustomer.SelectedIndex = -1;
            }
            else
            {
                dgvPOItemList.ClearSelection();
            }

            ShowShippingInfo();

            lblTel.Visible = true;
            txtTel.Visible = true;
            loaded = true;
        }

        private void ClearField()
        {
            txtFullName.Clear();

            txtAddress1.Clear();
            txtAddress2.Clear();
            txtAddress3.Clear();
            txtShortName.Clear();
            txtFullName.Clear();
            txtTransporterName.Clear();
            txtTel.Clear();

            txtCity.Clear();
            txtState.Clear();
            txtCountry.Text = "MALAYSIA";
            txtPostalCode.Clear();

            txtAddress2.Enabled = false;
            txtAddress3.Enabled = false;
        }

        private void GetHabitPONumber(string custTblCode)
        {
            //get po data with same customer table code
            DataTable dt = dalData.POSelectWithCustCode(custTblCode);

            string beginningNumber = "", PONumber_1 = "", PONumber_2 = "", PONumber_3 = "", previousPONO = "Initial";
            int NumberOfPoToCompare = 3;

            //get latest not removed po code
            foreach(DataRow row in dt.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(!isRemoved)
                {
                    string poNumber = row[dalData.PONo].ToString();

                    if(previousPONO != poNumber || previousPONO == "Initial")
                    {
                        previousPONO = poNumber;

                        if (NumberOfPoToCompare == 3)
                        {
                            PONumber_1 = poNumber;
                            NumberOfPoToCompare--;
                        }
                        else if (NumberOfPoToCompare == 2)
                        {
                            PONumber_2 = poNumber;
                            NumberOfPoToCompare--;
                        }
                        else if (NumberOfPoToCompare == 1)
                        {
                            PONumber_3 = poNumber;
                            NumberOfPoToCompare--;
                            break;
                        }

                    }
                  
                }
            }

            //compare string
            if(PONumber_1 != "" && PONumber_2 != "" && PONumber_3 != "")
            {
                for(int index = 0; index < PONumber_1.Length; index ++)
                {
                    bool ableToCompare = index < PONumber_1.Length;
                    ableToCompare &= index < PONumber_2.Length;
                    ableToCompare &= index < PONumber_3.Length;

                    if (ableToCompare)
                    {
                        bool charMatched = PONumber_1[index].ToString() == PONumber_2[index].ToString();
                        charMatched &= PONumber_2[index].ToString() == PONumber_3[index].ToString();

                        if (charMatched)
                        {
                            beginningNumber += PONumber_1[index].ToString();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                
            }
            
            if(!string.IsNullOrEmpty(beginningNumber))
            {
                POBeginingNo = beginningNumber;
                txtPONo.Text = beginningNumber;
                txtPONo.ForeColor = SystemColors.GrayText;
            }
            else
            {
                POBeginingNo = "";
                txtPONo.Text = "";
            }
        }

        private void ShowDataToField(string customerName)
        {
            ClearField();

            if(cbUseBillingAddress.Checked)
            {
                foreach (DataRow row in dt_CustomerList.Rows)
                {
                    string fullName = row[dalData.FullName] == null ? string.Empty : row[dalData.FullName].ToString();

                    if (fullName == customerName)
                    {
                        string registrationNO = row[dalData.RegistrationNo] == null ? string.Empty : row[dalData.RegistrationNo].ToString();
                        string address1 = row[dalData.Address1] == null ? string.Empty : row[dalData.Address1].ToString();
                        string address2 = row[dalData.Address2] == null ? string.Empty : row[dalData.Address2].ToString();
                        string address3 = row[dalData.Address3] == null ? string.Empty : row[dalData.Address3].ToString();
                        string city = row[dalData.AddressCity] == null ? string.Empty : row[dalData.AddressCity].ToString();
                        string state = row[dalData.AddressState] == null ? string.Empty : row[dalData.AddressState].ToString();
                        string country = row[dalData.AddressCountry] == null ? string.Empty : row[dalData.AddressCountry].ToString();
                        string postalCode = row[dalData.AddressPostalCode] == null ? string.Empty : row[dalData.AddressPostalCode].ToString();
                        string ShortName = row[dalData.ShortName] == null ? string.Empty : row[dalData.ShortName].ToString();
                        string phone1 = row[dalData.Phone1] == null ? string.Empty : row[dalData.Phone1].ToString();

                        txtFullName.Text = fullName;
                        txtShortName.Text = ShortName;
                        txtAddress1.Text = address1;
                        txtAddress2.Text = address2;
                        txtAddress3.Text = address3;
                        txtCity.Text = city;
                        txtState.Text = state;
                        txtCountry.Text = country;
                        txtPostalCode.Text = postalCode;
                        txtTel.Text = phone1;
                        cbOwnDO.Checked =  false;

                        //LOAD HABIT P/O NUMBER
                        //string custTblCode = row[dalData.TableCode].ToString();

                        customerTblCode = row[dalData.TableCode].ToString();
                        GetHabitPONumber(customerTblCode);
                        break;
                    }
                }

                txtAddress2.Enabled = false;
                txtAddress3.Enabled = false;
            }
            
            
        }

        private void LoadShippingDataToField()
        {
            ClearField();

            if (!string.IsNullOrEmpty(uData.Shipping_Full_Name) && frmSBBShippingData.dataSelected)
            {
                txtFullName.Text = uData.Shipping_Full_Name;
                txtShortName.Text = uData.Shipping_Short_Name;
                txtAddress1.Text = uData.Address_1;
                txtAddress2.Text = uData.Address_2;
                txtAddress3.Text = uData.Address_3;
                txtCity.Text = uData.Address_City;
                txtState.Text = uData.Address_State;
                txtCountry.Text = uData.Address_Country;
                txtPostalCode.Text = uData.Address_Postal_Code;
                txtTel.Text = uData.Phone_1;
                txtTransporterName.Text = uData.Shipping_Transporter;

                 cbOwnDO.Checked = uData.Cust_Own_DO;

            }

        }

        private void AddNewCustomer_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSPPCustomerEdit frm = new frmSPPCustomerEdit
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            dt_CustomerList = dalData.CustomerWithoutRemovedDataSelect();
            loadLocationData(dt_CustomerList, cmbCustomer, dalData.FullName);
            cmbCustomer.SelectedIndex = -1;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                ClearError();
                cbUseBillingAddress.Checked = true;
                ShowDataToField(cmbCustomer.Text);
                
            }
        }

        private void ClearError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            errorProvider8.Clear();
            errorProvider9.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to cancel PO edting?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void ShippingInfoEdit(bool Enabled)
        {
            txtFullName.Enabled = false;
            txtShortName.Enabled = false;

            txtAddress1.Enabled = false;

            txtPostalCode.Enabled = false;
            txtCity.Enabled = false;

            txtState.Enabled = false;
            txtTransporterName.Enabled = true;
            txtTel.Enabled = false;
          


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShippingInfoEdit(!cbUseBillingAddress.Checked);


            if (cbUseBillingAddress.Checked)
            {
                //lblTel.Visible = false;
                //txtTel.Visible = false;

                lblTel.Visible = true;
                txtTel.Visible = true;

                ShowDataToField(cmbCustomer.Text);
                lblShippingList.Visible = false;

            }
            else
            {
                ClearField();

                lblTel.Visible = true;
                txtTel.Visible = true;
                lblShippingList.Visible = true;
            }
        }

        private void dgvPOItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Visible = true;
            btnRemoveItem.Visible = true;

            DataGridView dgv = dgvPOItemList;
            btnRemoveItem.Text = text_Remove;
            if (dgv.SelectedRows.Count > 0)
            {
                int rowIndex = dgv.CurrentCell.RowIndex;
                string dataMode = dgv.Rows[rowIndex].Cells[header_DataMode].Value.ToString();

                if(dataMode == text_ToRemove)
                {
                    btnRemoveItem.Text = text_UndoRemove;

                }
                
            }
    
        }

        

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(poEditing)
            {
                ShowAddItemField();
            }

           
            LoadItemToField();

            UpdateTotalPrice();

        }

        private void dgvPOItemList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (poEditing)
            {
                ShowAddItemField();

            }

            btnAddItem.Text = text_UpdateItem;

            LoadItemToField();
            UpdateTotalPrice();
        }

        private bool Validation()
        {
            ClearError();
            bool result = true;

            if (string.IsNullOrEmpty(cmbCustomer.Text))
            {
                result = false;
                errorProvider1.SetError(lblCustomer, "Customer Required");
            }

            if (string.IsNullOrEmpty(dtpPODate.Text))
            {
                result = false;
                errorProvider2.SetError(lblPODate, "PO Date Required");
            }

            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                result = false;
                errorProvider3.SetError(lblShippingAddress, "Shipping Address Required");
            }

            if (string.IsNullOrEmpty(txtCity.Text))
            {
                result = false;
                errorProvider4.SetError(lblCity, "Address City Required");
            }

            if (string.IsNullOrEmpty(txtState.Text))
            {
                result = false;
                errorProvider5.SetError(lblState, "Address State Required");
            }

            if (string.IsNullOrEmpty(txtPostalCode.Text))
            {
                result = false;
                errorProvider6.SetError(lblPostalCode, "Address Postal Code Required");
            }

            if (string.IsNullOrEmpty(txtCountry.Text))
            {
                result = false;
                errorProvider7.SetError(lblCountry, "Address Country Required");
            }

            
            if (dt_OrderList.Rows.Count <= 0)
            {
                result = false;
                errorProvider8.SetError(lblOrderList, "Order Item Required");
            }

            if (string.IsNullOrEmpty(txtPONo.Text))
            {
                result = false;
                errorProvider9.SetError(lblPONo, "PO NO Required");
            }

            return result;

        }

        private bool ShippingInfoValidation()
        {
            ClearError();
            bool result = true;

            if (string.IsNullOrEmpty(cmbCustomer.Text))
            {
                result = false;
                errorProvider1.SetError(lblCustomer, "Customer Required");
            }

            if (string.IsNullOrEmpty(dtpPODate.Text))
            {
                result = false;
                errorProvider2.SetError(lblPODate, "PO Date Required");
            }

            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                result = false;
                errorProvider3.SetError(lblAddress1, "Shipping Address Required");
            }

            if (string.IsNullOrEmpty(txtCity.Text))
            {
                result = false;
                errorProvider4.SetError(lblCity, "Address City Required");
            }

            if (string.IsNullOrEmpty(txtState.Text))
            {
                result = false;
                errorProvider5.SetError(lblState, "Address State Required");
            }

            if (string.IsNullOrEmpty(txtPostalCode.Text))
            {
                result = false;
                errorProvider6.SetError(lblPostalCode, "Address Postal Code Required");
            }

            if (string.IsNullOrEmpty(txtCountry.Text))
            {
                result = false;
                errorProvider7.SetError(lblCountry, "Address Country Required");
            }

            if (string.IsNullOrEmpty(txtPONo.Text))
            {
                result = false;
                errorProvider9.SetError(lblPONo, "PO NO Required");
            }

            if (string.IsNullOrEmpty(txtFullName.Text) && !cbUseBillingAddress.Checked)
            {
                result = false;
                errorProvider10.SetError(lblFullName, "FullName Required");
            }

            if (string.IsNullOrEmpty(txtShortName.Text) && !cbUseBillingAddress.Checked)
            {
                result = false;
                errorProvider8.SetError(lblShortName, "ShortName Required");
            }

            if (string.IsNullOrEmpty(txtTel.Text) && !cbUseBillingAddress.Checked)
            {
                result = false;
                errorProvider11.SetError(lblTel, "Tel. Required");
            }

            else if (!poEditing)
            {
                //check if number exist in db
                if(tool.IfPONoExist(txtPONo.Text, cmbCustomer.Text))
                {
                    lblAvailableResult.Visible = true;
                    result = false;
                    errorProvider9.SetError(lblPONo, "PO NO is USED!");
                }
                else
                {
                    lblAvailableResult.Visible = false;
                }

                if(txtPONo.Text == POBeginingNo)
                {
                    result = false;
                    errorProvider9.SetError(lblPONo, "PO NO invalid!");
                }
            }

            return result;

        }

        private bool ItemInfoValidation()
        {
            ClearError();
            bool result = true;

            if (string.IsNullOrEmpty(cmbType.Text))
            {
                result = false;
                errorProvider1.SetError(lblType, "Type Required");
            }

            if (string.IsNullOrEmpty(cmbSize_1.Text))
            {
                result = false;
                errorProvider2.SetError(lblSize, "Size Required");
            }

            if (string.IsNullOrEmpty(lblCode.Text))
            {
                result = false;
                errorProvider3.SetError(label9, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtPCS.Text))
            {
                result = false;
                errorProvider4.SetError(lblPCS, "Order Pcs Required");
            }

            if (string.IsNullOrEmpty(txtBag.Text))
            {
                result = false;
                errorProvider5.SetError(lblBag, "Order Bag Required");
            }


            return result;

        }

      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validation() && cbTotalPrice.Checked)
            {
                //get customer table code
                int customer_tbl_code = 0;
                string customerFullName = cmbCustomer.Text;
                string shortName = "";
                string PONo = txtPONo.Text;
                int po_code = 0;

                DateTime updatedDate = DateTime.Now;
                int userID = MainDashboard.USER_ID;

                foreach (DataRow row in dt_CustomerList.Rows)
                {
                    if (row[dalData.FullName].ToString() == customerFullName)
                    {
                        customer_tbl_code = Convert.ToInt32(row[dalData.TableCode].ToString());
                        shortName = row[dalData.ShortName].ToString();
                        break;
                    }
                }

                uData.PO_date = dtpPODate.Value;
                uData.Customer_tbl_code = customer_tbl_code;
                uData.PO_no = txtPONo.Text;
                uData.Address_1 = txtAddress1.Text.ToUpper();
                uData.Address_2 = txtAddress2.Text.ToUpper();
                uData.Address_3 = txtAddress3.Text.ToUpper();
                uData.Phone_1 = txtTel.Text;

                uData.Address_City = txtCity.Text.ToUpper();
                uData.Address_State = txtState.Text.ToUpper();
                uData.Address_Country = txtCountry.Text.ToUpper();
                uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();

                uData.Shipping_Full_Name = txtFullName.Text.ToUpper();
                uData.Shipping_Short_Name = txtShortName.Text.ToUpper();
                uData.Shipping_Transporter = txtTransporterName.Text.ToUpper();

                uData.remark_in_do = txtShowInDO.Text.ToUpper();

                uData.UseBillingAddress = cbUseBillingAddress.Checked;
                uData.Cust_Own_DO = cbOwnDO.Checked;

                uData.IsRemoved = false;
                uData.Updated_Date = updatedDate;
                uData.Updated_By = userID;


                if (cbUrgent.Checked)
                {
                    uData.Target_Delivery_Date = dtpTargetDeliveryDate.Value;
                    uData.Priority_level = 1;
                }
                else
                {
                    uData.Priority_level = -1;
                    uData.Target_Delivery_Date = DateTime.MaxValue;
                }

                if (poEditing)
                {
                    uData.PO_code = Convert.ToInt32(EditingPOCode);
                    po_code = Convert.ToInt32(EditingPOCode);
                    //dalData.PODelete(uData);
                    //remove old one
                }
                else
                {
                    //get po code
                    DataTable dt = dalData.POSelect();

                    if (dt.Rows.Count <= 0)
                    {
                        po_code = -1;
                    }
                    else
                    {
                        DataView dv = dt.DefaultView;
                        dv.Sort = dalData.POCode + " desc";
                        DataTable sortedDT = dv.ToTable();

                        po_code = Convert.ToInt32(sortedDT.Rows[0][dalData.POCode].ToString()) + 1;
                    }

                    uData.PO_code = po_code;
                }

                if(po_code != -1)
                {
                    bool failedToInsert = false;

                    foreach (DataRow row in dt_OrderList.Rows)
                    {
                        string itemCode = row[header_Code].ToString();
                        uData.IsRemoved = false;

                        if (!string.IsNullOrEmpty(itemCode))
                        {
                            int po_qty = Convert.ToInt32(row[header_OrderQty]);
                            string note = row[header_Note].ToString();
                            string dataMode = row[header_DataMode].ToString();
                            string tblCode = row[header_TblCode].ToString();

                            decimal unitPrice = decimal.TryParse(row[header_UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                            decimal discountRate = decimal.TryParse(row[header_Discount].ToString(), out discountRate) ? discountRate : 0;
                            decimal subTotal = decimal.TryParse(row[header_Total].ToString(), out subTotal) ? subTotal : 0;

                            uData.Item_code = itemCode;
                            uData.PO_qty = po_qty;
                            uData.PO_note = note;

                            uData.Unit_price = unitPrice;
                            uData.Discount_rate = discountRate;
                            uData.Sub_total = subTotal;

                            if (dataMode == text_New)
                            {
                                if (!dalData.InsertPO(uData))
                                {
                                    MessageBox.Show("Unable to insert/update " + itemCode + " PO data!");
                                    failedToInsert = true;
                                }

                            }
                            else if (dataMode == text_ToRemove && int.TryParse(tblCode, out int i))
                            {
                                uData.IsRemoved = true;
                                uData.Table_Code = Convert.ToInt32(tblCode);

                                if (!dalData.PORemove(uData))
                                {

                                    MessageBox.Show("Unable to remove " + itemCode + " PO data!");
                                    failedToInsert = true;
                                }
                            }
                            else if (dataMode == text_ToUpdate && int.TryParse(tblCode, out i))
                            {
                                uData.Table_Code = Convert.ToInt32(tblCode);
                                if (!dalData.POUpdate(uData))
                                {

                                    MessageBox.Show("Unable to update " + itemCode + " PO data!");
                                    failedToInsert = true;
                                }
                            }

                        }

                    }

                    if (!failedToInsert)
                    {
                        MessageBox.Show("PO inserted/updated!");

                        if (poEditing)
                        {
                            poEdited = true;
                            tool.historyRecord(text.PO_Edited, text.GetPONumberAndCustomer(PONo, shortName), updatedDate, userID, dalData.POTableName, po_code);
                        }
                        else
                        {
                            historyDAL dalHistory = new historyDAL();
                            historyBLL uHistory = new historyBLL();

                            uHistory.page_name = dalData.POTableName;
                            uHistory.data_id = -1;

                            dalHistory.ChangeDataID(uHistory, po_code.ToString());
                            tool.historyRecord(text.PO_Added, text.GetPONumberAndCustomer(PONo, shortName), updatedDate, userID, dalData.POTableName, po_code);
                        }

                        Close();

                    }
                }
                else
                {
                    MessageBox.Show("P/O code error(-1), please try again.");
                }
            }
            else if(!cbTotalPrice.Checked)
            {
                MessageBox.Show("Please check the total price.");
            }
        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress1.Text) && loaded)
            {
                txtAddress2.Enabled = false;

            }
            else
            {
                txtAddress2.Text = "";

                txtAddress2.Enabled = false;
            }
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to remove this PO?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                uData.PO_code = Convert.ToInt32(EditingPOCode);

               
                if (dalData.PODelete(uData))
                {
                    poRemoved = true;
                    MessageBox.Show("PO removed!");
                    string customerFullName = cmbCustomer.Text;
                    string shortName = "";
                    string PONo = txtPONo.Text;

                    foreach (DataRow row in dt_CustomerList.Rows)
                    {
                        if (row[dalData.FullName].ToString() == customerFullName)
                        {
                            shortName = row[dalData.ShortName].ToString();
                            break;
                        }
                    }

                    tool.historyRecord(text.PO_Removed, text.GetPONumberAndCustomer(PONo, shortName), DateTime.Now, MainDashboard.USER_ID, dalData.POTableName, uData.PO_code);
                }
                    
               
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgvPOItemList.ClearSelection();
            btnEdit.Visible = false;
            btnRemoveItem.Visible = false;
            ShowShippingInfo();
        }

        private void btnGoToAddItem_Click(object sender, EventArgs e)
        {
            ClearError();

            dgvPOItemList.ClearSelection();
            btnEdit.Visible = false;
            btnRemoveItem.Visible = false;

            if (ShippingInfoValidation())
            ShowAddItemField();
        }

        private string GetSizeTableCodeFromCMB(ComboBox cmb)
        {
            DataTable dt_Size = (DataTable)cmb.DataSource;

            string size_Tbl_Code = "";

            if (dt_Size != null && cmb.SelectedIndex != -1)
            {
                size_Tbl_Code = dt_Size.Rows[cmb.SelectedIndex][header_TblCode].ToString();
            }


            return size_Tbl_Code;
        }

        private void GetItemCodeAndStock()
        {
            txtPCS.Clear();

            string itemType = cmbType.Text;

            string size_Tbl_Code_1 = GetSizeTableCodeFromCMB(cmbSize_1);
            string size_Tbl_Code_2 = GetSizeTableCodeFromCMB(cmbSize_2);

            string itemCode = "";
            string itemStock = "";
            string stdPacking = "";


            if (!string.IsNullOrEmpty(itemType) && !string.IsNullOrEmpty(size_Tbl_Code_1))
            {
                foreach (DataRow row in dt_ReadyGoods.Rows)
                {
                    bool dataMatched = true;

                    string sizeDB_1 = row[header_Size_1].ToString();
                    string sizeDB_2 = row[header_Size_2].ToString();

                    
                    string typeDB = row["TYPE"].ToString();

                    if (typeDB == text.Type_Sprinkler)
                    {
                        sizeDB_2 = "";
                    }
                    if (row["TYPE"].ToString()!= itemType)
                    {

                        dataMatched = false;

                    }
                    else
                    {
                        float test = 0;
                    }


                    if(size_Tbl_Code_2 == "")
                    {
                        if(sizeDB_2 != "" || size_Tbl_Code_1 != sizeDB_1)
                        {
                            dataMatched = false;
                        }
                    }
                    else 
                    {
                        if (sizeDB_2 == "")
                        {
                            dataMatched = false;

                        }
                        else if(size_Tbl_Code_1 != sizeDB_1 || size_Tbl_Code_2 != sizeDB_2)
                        {
                            if(size_Tbl_Code_1 != sizeDB_2 || size_Tbl_Code_2 != sizeDB_1)
                            {
                                dataMatched = false;

                            }
                        }
                        
                    }
                    

                    //if (sizeDB_1 == "" || (sizeDB_1 != size_Tbl_Code_1 && sizeDB_1 != size_Tbl_Code_2))
                    //{

                    //    dataMatched = false;

                    //}

                   
                    //if(size_Tbl_Code_2 != "")
                    //{
                    //    if (sizeDB_2 == "" || (sizeDB_2 != size_Tbl_Code_1 && sizeDB_2 != size_Tbl_Code_2))
                    //    {

                    //        dataMatched = false;

                    //    }
                    //}
                  

                    if (dataMatched)
                    {
                        itemCode = row["CODE"].ToString();

                        int qtyPerPacket = int.TryParse(row["QTY_PACKET"].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                        int qtyPerBag = int.TryParse(row["STD_PACKING"].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int stockQty = int.TryParse(row["QUANTITY"].ToString(), out stockQty) ? stockQty : 0;

                        stdPacking = qtyPerPacket + "/PACKET, " + qtyPerBag.ToString() + "/BAG";
                        stdPackingPerBag = qtyPerBag;
                        stdPackingPerPacket = qtyPerPacket;
                        itemStock = stockQty + " pcs";

                        if (qtyPerBag > 0)
                        {

                            int bagQty = stockQty / qtyPerBag;

                            itemStock += " (" + bagQty + " bags)";
                        }
                    }
                }

            }
            lblCode.Text = itemCode;
            lblStock.Text = itemStock;
            lblStdPacking.Text = stdPacking;
            //lblStdPacking.Text = stdPackingPerBag + "/BAG";

            ConvertFromBags = true;
            txtPCS.Clear();
            ConvertFromBags = false;

            ConvertFromPcs = true;
            txtBag.Clear();
            ConvertFromPcs = false;
        }


        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSizeData(dt_SizeList, cmbType.Text);
            GetItemCodeAndStock();
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItemCodeAndStock();
        }

        private void CalculateBagQty()
        {
            errorProvider2.Clear();
            int PcsQty = int.TryParse(txtPCS.Text, out PcsQty) ? PcsQty : 0;

            double balance = 0;
            if (stdPackingPerBag > 0)
            {
                txtBag.Text = (PcsQty / stdPackingPerBag).ToString();
                balance = PcsQty % stdPackingPerBag;
            }
            else
            {
                txtBag.Clear();
            }

            if (balance > 0)
            {
                lblBalance.Visible = true;
                lblBalQty.Visible = true;


                int packetBal = 0;
                int pcsBal = 0;

                if(stdPackingPerPacket > 1)
                {
                    packetBal = (int)balance / stdPackingPerPacket;
                    pcsBal = (int)balance % stdPackingPerPacket;
                }
                else
                {
                    pcsBal = (int)balance;

                }

                string packetUnitString = " pkts + ";
                string pcsUnitString = " pcs";

                if(packetBal == 1)
                {
                    packetUnitString = " pkt + ";
                }

                if (pcsBal == 1)
                {
                    pcsUnitString = " pc";
                }

                string balString = packetBal + packetUnitString + pcsBal + pcsUnitString;

                lblBalQty.Text = balString;
                //lblBalQty.Text = balance.ToString();
                
            }
            else
            {
                lblBalance.Visible = false;
                lblBalQty.Visible = false;
            }
        }

        private void CalculatePcsQty()
        {
            lblBalance.Visible = false;
            lblBalQty.Visible = false;

            errorProvider2.Clear();
            int bagQty = int.TryParse(txtBag.Text, out bagQty) ? bagQty : 0;

            txtPCS.Text = (bagQty * stdPackingPerBag).ToString();
        }

        private void txtPCS_TextChanged(object sender, EventArgs e)
        {
            if (!ConvertFromBags)
            {
                ConvertFromPcs = true;
            }

            if (ConvertFromPcs)
                CalculateBagQty();

            ConvertFromPcs = false;
        }

        private void txtBag_TextChanged(object sender, EventArgs e)
        {
            if (!ConvertFromPcs)
            {
                ConvertFromBags = true;
            }

            if (ConvertFromBags)
                CalculatePcsQty();

            ConvertFromBags = false;
        }

        private void btnAutoAdjust_Click(object sender, EventArgs e)
        {
            lblBalance.Visible = false;
            lblBalQty.Visible = false;

            ConvertFromBags = true;
            CalculatePcsQty();
            ConvertFromBags = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            int targetBags = int.TryParse(txtBag.Text, out targetBags) ? targetBags : 0;

            txtBag.Text = (targetBags + 10).ToString();
        }

        private void AddItemToList()
        {
            if (ItemInfoValidation())
            {
                DataRow dt_Row = dt_OrderList.NewRow();

                string sizeTblCode_1 = GetSizeTableCodeFromCMB(cmbSize_1);
                string sizeTblCode_2 = GetSizeTableCodeFromCMB(cmbSize_2);
                string itemCode = lblCode.Text;

                dt_Row[header_TblCode] = text_New;
                dt_Row[header_DataMode] = text_New;

                if(!string.IsNullOrEmpty(EditingPOTableCode) && poEditing)
                {
                    dt_Row[header_TblCode] = EditingPOTableCode;
                    dt_Row[header_DataMode] = text_ToUpdate;

                    EditingPOTableCode = "";
                }

                //dt_Row[header_Size_1] = cmbSize_1.Text;
                dt_Row[header_SizeTblCode_1] = sizeTblCode_1;
                dt_Row[header_SizeTblCode_2] = sizeTblCode_2;

                string sizeString = "";

                int size_1 = int.TryParse(cmbSize_1.Text, out size_1)? size_1 : 0;
                int size_2 = int.TryParse(cmbSize_2.Text, out size_2) ? size_2 : 0;
                int orderQty = int.TryParse(txtPCS.Text, out orderQty) ? orderQty : 0;

                if (sizeTblCode_1 != "" && sizeTblCode_2 != "")
                {
                    sizeString = cmbSize_1.Text + " x "+ cmbSize_2.Text;

                    if(size_2 > size_1)
                    {
                        sizeString = cmbSize_2.Text + " x " + cmbSize_1.Text;
                    }
                }
                else if(sizeTblCode_1 != "")
                {
                    sizeString = size_1.ToString();

                }

                dt_Row[header_SizeString] = sizeString;

                dt_Row[header_Size_1] = cmbSize_1.Text;
                dt_Row[header_Size_2] = cmbSize_2.Text;
                //dt_Row[header_Unit_1] = "MM";
                dt_Row[header_Code] = itemCode;
                dt_Row[header_Type] = cmbType.Text;
                dt_Row[header_OrderQty] = orderQty.ToString();

             

                if(lblBalQty.Visible)
                {
                    dt_Row[header_Note] = "(" + txtBag.Text + " BAGS +"+ lblBalQty.Text + ") " + txtNote.Text;
                }
                else
                {
                    dt_Row[header_Note] = "(" + txtBag.Text + " BAGS) " + txtNote.Text;
                }
                

                if (customerTblCode != "")
                {
                    DataTable dt_Discount = dalData.DiscountSelect();

                    if (dt_Discount != null && dt_Discount.Rows.Count > 0)
                    {
                        foreach (DataRow discountRow in dt_Discount.Rows)
                        {
                            bool dataMatched = discountRow[dalData.CustTblCode].ToString() == customerTblCode;
                            dataMatched &= discountRow[dalData.ItemTblCode].ToString() == itemCode;

                            if (dataMatched)
                            {
                                //get unit price
                                //get discount rate
                                //calculate amount
                                //insert data to table

                                decimal unitPrice = decimal.TryParse(discountRow[dalData.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                decimal discountRate = decimal.TryParse(discountRow[dalData.DiscountRate].ToString(), out discountRate) ? discountRate : 0;

                                decimal amount = (1 - discountRate / 100) * unitPrice;

                                dt_Row[header_UnitPrice] = unitPrice;

                                dt_Row[header_Discount] = discountRate;


                                dt_Row[header_Total] = decimal.Round(amount * orderQty, 2, MidpointRounding.AwayFromZero);

                                break;
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Failed to get customer's table code!");
                }

                dt_OrderList.Rows.Add(dt_Row);

                //dt_OrderList = AddTypeDivideSpace(dt_OrderList);
                AddIndexToOrderTable();

                dgvPOItemList.DataSource = dt_OrderList;
                DgvUIEdit(dgvPOItemList);
                dgvPOItemList.ClearSelection();

                dgvPOItemList.Rows[dgvPOItemList.Rows.Count - 1].Selected = true;
            }
            
        }

        private void DiscountRatePlusOne()
        {
            if (customerTblCode != "")
            {
                DataTable dt_Item = (DataTable)dgvPOItemList.DataSource;
                DataTable dt_Discount = dalData.DiscountSelect();

                if (dt_Discount != null && dt_Discount.Rows.Count > 0 && dt_Item != null && dt_Item.Rows.Count > 0)
                {
                    foreach(DataRow row in dt_Item.Rows)
                    {
                        string itemCode = row[header_Code].ToString();
                        int orderQty = int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;
                        decimal discountRate = decimal.TryParse(row[header_Discount].ToString(), out decimal j) ? j : 0;
                        decimal unitPrice = decimal.TryParse(row[header_UnitPrice].ToString(), out  j) ? j : 0;

                        if(discountRate > 0)
                        {
                            if(cbZeroPointOne.Checked)
                            {
                                discountRate += (decimal) 0.1;

                            }
                            else
                            {
                                discountRate++;

                            }

                            decimal amount = (1 - discountRate / 100) * unitPrice;

                            row[header_UnitPrice] = unitPrice;

                            row[header_Discount] = discountRate;

                            row[header_Total] = decimal.Round(amount * orderQty, 2, MidpointRounding.AwayFromZero);
                        }

                       
                    }

                    
                }

                UpdateTotalPrice();

            }
          
        }

        private void DiscountRateMinusOne()
        {
            if (customerTblCode != "")
            {
                DataTable dt_Item = (DataTable)dgvPOItemList.DataSource;
                DataTable dt_Discount = dalData.DiscountSelect();

                if (dt_Discount != null && dt_Discount.Rows.Count > 0 && dt_Item != null && dt_Item.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_Item.Rows)
                    {
                        string itemCode = row[header_Code].ToString();
                        int orderQty = int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;
                        decimal discountRate = decimal.TryParse(row[header_Discount].ToString(), out decimal j) ? j : 0;
                        decimal unitPrice = decimal.TryParse(row[header_UnitPrice].ToString(), out j) ? j : 0;

                        if (discountRate > 0)
                        {
                            if (cbZeroPointOne.Checked)
                            {
                                discountRate -= (decimal)0.1;

                            }
                            else
                            {
                                discountRate--;

                            }

                            decimal amount = (1 - discountRate / 100) * unitPrice;

                            row[header_UnitPrice] = unitPrice;

                            row[header_Discount] = discountRate;

                            row[header_Total] = decimal.Round(amount * orderQty, 2, MidpointRounding.AwayFromZero);

                        }

                       
                    }


                }

                UpdateTotalPrice();

            }

        }

        private void DiscountRateReset()
        {
            if (customerTblCode != "")
            {
                DataTable dt_Item = (DataTable)dgvPOItemList.DataSource;
                DataTable dt_Discount = dalData.DiscountSelect();

                if (dt_Discount != null && dt_Discount.Rows.Count > 0 && dt_Item != null && dt_Item.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_Item.Rows)
                    {
                        string itemCode = row[header_Code].ToString();
                        int orderQty = int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;

                        foreach (DataRow discountRow in dt_Discount.Rows)
                        {
                            bool dataMatched = discountRow[dalData.CustTblCode].ToString() == customerTblCode;
                            dataMatched &= discountRow[dalData.ItemTblCode].ToString() == itemCode;

                            if (dataMatched)
                            {
                                //get unit price
                                //get discount rate
                                //calculate amount
                                //insert data to table

                                decimal unitPrice = decimal.TryParse(discountRow[dalData.UnitPrice].ToString(), out unitPrice) ? unitPrice : 0;
                                decimal discountRate = decimal.TryParse(discountRow[dalData.DiscountRate].ToString(), out discountRate) ? discountRate : 0;

                                decimal amount = (1 - discountRate / 100) * unitPrice;

                                row[header_UnitPrice] = unitPrice;

                                row[header_Discount] = discountRate;


                                row[header_Total] = decimal.Round(amount * orderQty, 2, MidpointRounding.AwayFromZero);

                                break;
                            }
                        }


                       
                    }


                }

                UpdateTotalPrice();

            }

        }

        private void LoadItemToField()
        {
            //get item info
            btnAddItem.Text = text_UpdateItem;
            DataGridView dgv = dgvPOItemList;
            DataTable dt = (DataTable)dgv.DataSource;
            int rowIndex = dgv.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                //string code, string type, string size, string unit
                string type = dt.Rows[rowIndex][header_Type].ToString();
                string size = dt.Rows[rowIndex][header_Size_1].ToString();
                string unit = dt.Rows[rowIndex][header_Unit_1].ToString();

                string size2 = dt.Rows[rowIndex][header_Size_2].ToString();
                string unit2 = dt.Rows[rowIndex][header_Unit_2].ToString();

                string orderQty = dt.Rows[rowIndex][header_OrderQty].ToString();

                EditingPOTableCode = dt.Rows[rowIndex][header_TblCode].ToString();

                cmbType.Text = type;

                //DataTable dt232 = (DataTable)cmbSize_2.DataSource;

                cmbSize_1.SelectedIndex = cmbSize_1.FindStringExact(size);
                cmbSize_2.SelectedIndex = cmbSize_2.FindStringExact(size2);
                //if (size == "3/2")
                //{
                //    cmbSize_1.Text = "1 1/2";
                //}
                //else
                //{
                //    cmbSize_1.Text = size;
                //}

                //if (size2 == "3/2")
                //{
                //    cmbSize_2.Text = "1 1/2";
                //    cmbSize_2.SelectedIndex = cmbSize_2.FindStringExact(size2);
                //}
                //else
                //{
                //    cmbSize_2.Text = size2;
                //    cmbSize_2.SelectedIndex = cmbSize_2.FindStringExact("1 1/2");
                //}
                ////cmbSize_2.SelectedIndex = 10;
               
                txtPCS.Text = orderQty;
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private void UpdateItemToList()
        {
            //get item info
            DataGridView dgv = dgvPOItemList;
            DataTable dt = (DataTable)dgv.DataSource;
            int rowIndex = dgv.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                btnEdit.Visible = false;
                btnAddItem.Text = text_AddItem;

                dt_OrderList.Rows.RemoveAt(rowIndex);
                AddItemToList();
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private DataTable AddTypeDivideSpace(DataTable dt)
        {
            string preType = null;

            DataView dv = dt.DefaultView;
            dv.Sort = header_Type + " ASC," + header_Size_1 + " ASC,"+ header_OrderQty + " ASC";
            DataTable sortedDT = dv.ToTable();

            DataTable dt_Copy = dt.Clone();
            DataRow dt_Row;
            foreach (DataRow row in sortedDT.Rows)
            {
                string type = row[header_Type].ToString();

                if(!string.IsNullOrEmpty(type))
                {
                    if (preType == null)
                    {
                        preType = type;
                    }
                    else if (preType != type)
                    {
                        preType = type;
                        dt_Row = dt_Copy.NewRow();
                        dt_Copy.Rows.Add(dt_Row);
                    }


                    dt_Copy.ImportRow(row);
                    //index++;
                }

            }

            return dt_Copy;
        }

        private void UpdateTotalPrice()
        {
            DataTable dt = (DataTable)dgvPOItemList.DataSource;
            decimal totalPrice = 0;

            if(dt != null)
            foreach(DataRow row in dt.Rows)
            {
                string dataMode = row[header_DataMode].ToString();

                if(dataMode != text_ToRemove)
                {
                    totalPrice += decimal.TryParse(row[header_Total].ToString(), out decimal subTotal) ? subTotal : 0;
                }
               
            }

            totalPrice = decimal.Round(totalPrice, 2, MidpointRounding.AwayFromZero);

            btnTotalPrice.Text = "RM " + totalPrice;
            cbTotalPrice.Checked = false;

        }

        private void BigSprayJetPriceSetBaseOnQty()
        {
            DataTable dt = (DataTable)dgvPOItemList.DataSource;
            int totalQty = 0;
            decimal unitPrice = (decimal) 0.35;

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string dataMode = row[header_DataMode].ToString();
                    string itemCode = row[header_Code].ToString();

                    if (dataMode != text_ToRemove && itemCode.Contains("SJ360B"))
                    {
                        totalQty += int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;
                    }

                }

                if(totalQty >= 10000 && totalQty < 20000)
                {
                    unitPrice = (decimal)0.3;
                }
                else if (totalQty >= 20000 && totalQty < 40000)
                {
                    unitPrice = (decimal) 0.25;
                }
                else if (totalQty >= 40000 )
                {
                    unitPrice = (decimal) 0.2;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string dataMode = row[header_DataMode].ToString();
                    string itemCode = row[header_Code].ToString();

                    if (dataMode != text_ToRemove && itemCode.Contains("SJ360B"))
                    {
                        int orderQty = int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;
                        decimal discountRate = decimal.TryParse(row[header_Discount].ToString(), out discountRate) ? discountRate :  80;

                        row[header_UnitPrice] = unitPrice;

                        decimal amount = (1 - discountRate / 100) * unitPrice;

                        row[header_Total] = decimal.Round(amount * orderQty, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }
       
        }
        private void SmallSprayJetPriceSetBaseOnQty()
        {
            DataTable dt = (DataTable)dgvPOItemList.DataSource;
            int totalQty = 0;
            decimal unitPrice = (decimal)0.3;

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string dataMode = row[header_DataMode].ToString();
                    string itemCode = row[header_Code].ToString();

                    if (dataMode != text_ToRemove && itemCode.Contains("SJ360S"))
                    {
                        totalQty += int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;
                    }

                }

                if (totalQty >= 10000 && totalQty < 20000)
                {
                    unitPrice = (decimal)0.25;
                }
                else if (totalQty >= 20000 && totalQty < 40000)
                {
                    unitPrice = (decimal)0.2;
                }
                else if (totalQty >= 40000)
                {
                    unitPrice = (decimal)0.15;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string dataMode = row[header_DataMode].ToString();
                    string itemCode = row[header_Code].ToString();

                    if (dataMode != text_ToRemove && itemCode.Contains("SJ360S"))
                    {
                        int orderQty = int.TryParse(row[header_OrderQty].ToString(), out int i) ? i : 0;
                        decimal discountRate = decimal.TryParse(row[header_Discount].ToString(), out discountRate) ? discountRate : 80;

                        row[header_UnitPrice] = unitPrice;

                        decimal amount = (1 - discountRate / 100) * unitPrice;

                        row[header_Total] = decimal.Round(amount * orderQty, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }

        }

        private bool checkIfItemAdded(string itemCode)
        {
            DataTable dt = (DataTable)dgvPOItemList.DataSource;

            if(dt == null || dt.Rows.Count <= 0 || btnAddItem.Text == text_UpdateItem)
            {
                return false;
            }

            foreach(DataRow row in dt.Rows)
            {
                if(row[header_Code].ToString() == itemCode)
                {
                    MessageBox.Show("Item have been added to the list.\nDouble click the row to update item quantity.");
                    return true;
                }
            }

            return false;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string itemCode = lblCode.Text;

            bool isBigSprayJet = itemCode.Contains("SJ360B");
            bool isSmallSprayJet = itemCode.Contains("SJ360S");

            if (!checkIfItemAdded(itemCode))
            {
                if (!string.IsNullOrEmpty(itemCode))
                {
                    btnEdit.Visible = false;
                    btnRemoveItem.Visible = false;

                    if (btnAddItem.Text == text_AddItem)
                    {
                        AddItemToList();
                    }
                    else if (btnAddItem.Text == text_UpdateItem)
                    {
                        UpdateItemToList();
                    }

                    if(isBigSprayJet)
                    {
                        BigSprayJetPriceSetBaseOnQty();
                    }

                    if (isSmallSprayJet)
                    {
                        SmallSprayJetPriceSetBaseOnQty();
                    }

                    UpdateTotalPrice();
                    ConvertFromBags = true;
                    txtPCS.Clear();
                    ConvertFromBags = false;

                    ConvertFromPcs = true;
                    txtBag.Clear();
                    ConvertFromPcs = false;
                }
                else
                {
                    errorProvider3.SetError(label9, "Item Code Required");

                    MessageBox.Show("Item Code invalid or not found.");
                }
            }
           

           
        }

        private void dgvPOItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DataGridView dgv = dgvPOItemList;
            //dgv.SuspendLayout();

            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;

            //if (dgv.Columns[e.ColumnIndex].Name == header_Type)
            //{
            //    if (dgv.Rows[rowIndex].Cells[header_Type].Value == DBNull.Value)
            //    {
            //        dgv.Rows[rowIndex].Height = 5;
            //        dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            //    }
            //    else
            //    {
            //        dgv.Rows[rowIndex].Height = 50;

            //    }
            //}
        }

        private void txtPONo_TextChanged(object sender, EventArgs e)
        {
            lblAvailableResult.Visible = false;
            if (txtPONo.Text.Length == 0)
            {
                POBeginingNo = "";
            }

        }

        private void txtPONo_Enter(object sender, EventArgs e)
        {
            if (txtPONo.Text == POBeginingNo)
            {
                txtPONo.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtPONo_Leave(object sender, EventArgs e)
        {
            if (txtPONo.Text == POBeginingNo)
            {
                txtPONo.ForeColor = SystemColors.GrayText;
            }
        }

        private void lblAddFive_Click(object sender, EventArgs e)
        {
            int targetBags = int.TryParse(txtBag.Text, out targetBags) ? targetBags : 0;

            txtBag.Text = (targetBags + 5).ToString();
        }

        private void lblAddOne_Click(object sender, EventArgs e)
        {
            int targetBags = int.TryParse(txtBag.Text, out targetBags) ? targetBags : 0;

            txtBag.Text = (targetBags + 1).ToString();
        }

        private void cmbSize_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItemCodeAndStock();
        }

        private void cbTotalPrice_CheckedChanged(object sender, EventArgs e)
        {
            if(cbTotalPrice.Checked)
            {
                btnTotalPrice.BackColor = Color.FromArgb(0, 184, 148);
            }
            else
            {
                btnTotalPrice.BackColor = Color.FromArgb(253, 203, 110);
            }
        }

        private void btnAdd_MouseClick(object sender, MouseEventArgs e)
        {
            if (!btnAdd.Enabled)
            {
                MessageBox.Show("Please check the total price.");
            }
        }

        private void btnAdd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!btnAdd.Enabled)
            {
                MessageBox.Show("Please check the total price.");
            }
        }

        private void btnTotalPrice_Click(object sender, EventArgs e)
        {
            if(cbTotalPrice.Checked)
            {
                cbTotalPrice.Checked = false;
            }
            else
            {
                cbTotalPrice.Checked = true;
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtAddress1.CreateGraphics().MeasureString(txtAddress1.Text, txtAddress1.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtAddress1.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtAddress1.Font).Width;
            }

            int textboxWidth = txtAddress1.Width;

            if (stringWidth > 320 && (Keys)e.KeyChar != Keys.Back )
            {
                e.Handled = true;
            }
        }

        private void txtAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtAddress2.CreateGraphics().MeasureString(txtAddress2.Text, txtAddress2.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtAddress2.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtAddress2.Font).Width;
            }

            int textboxWidth = txtAddress2.Width;

            if (stringWidth > 320 && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtAddress3.CreateGraphics().MeasureString(txtAddress3.Text, txtAddress3.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtAddress3.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtAddress3.Font).Width;
            }

            int textboxWidth = txtAddress3.Width;

            if (stringWidth > 320 && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtFullName.CreateGraphics().MeasureString(txtFullName.Text, txtFullName.Font).Width;
            
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtFullName.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtFullName.Font).Width;
            }

            int maxWidth = (int)txtFullName.CreateGraphics().MeasureString("SIA THAI YEW HARDWARE TRADING SDN BHDaaaaabbbbbbcef", txtFullName.Font).Width;

            if (stringWidth > maxWidth && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFullName.Text))
            {
                string shortName = txtFullName.Text.Split(' ').First().ToUpper();

                int stringWidth = (int)txtFullName.CreateGraphics().MeasureString(shortName, txtFullName.Font).Width;

                if (stringWidth > 140)
                {
                    string newShortName = "";

                    int newStringWidth = (int)txtFullName.CreateGraphics().MeasureString(newShortName, txtFullName.Font).Width;

                    int i = 0;

                    while (newStringWidth < 130 && i < shortName.Length)
                    {
                        newShortName += shortName[i].ToString();

                        i++;

                        newStringWidth = (int)txtFullName.CreateGraphics().MeasureString(newShortName, txtFullName.Font).Width;
                    }

                    shortName = newShortName;
                }

                txtShortName.Text = shortName;
               

            }
            else
            {
                txtShortName.Text = "";
            }
        }

        private void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress2.Text))
            {
                txtAddress3.Enabled = false;

            }
            else
            {
                txtAddress3.Text = "";
                txtAddress3.Enabled = false;
            }
        }

        private void lblShippingList_Click(object sender, EventArgs e)
        {
            uData.Customer_tbl_code = int.TryParse(customerTblCode, out int i) ? i : 0;

            frmSBBShippingData frm = new frmSBBShippingData(uData)
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            //validation
            //add data to field
            LoadShippingDataToField();

        }

        private void btnAddDiscountRate_Click(object sender, EventArgs e)
        {
            DiscountRatePlusOne();
        }

        private void btnMinusDiscountRate_Click(object sender, EventArgs e)
        {
            DiscountRateMinusOne();
        }

        private void btnResetDiscountRate_Click(object sender, EventArgs e)
        {
            DiscountRateReset();
        }

        private void cbUrgent_CheckedChanged(object sender, EventArgs e)
        {
            lblTargetDeliveryDate.Visible = cbUrgent.Checked;
            dtpTargetDeliveryDate.Visible = cbUrgent.Checked;
        }
    }
}
