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
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPDOList : Form
    {
        public frmSPPDOList()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvDOList, true);
            tool.DoubleBuffered(dgvItemList, true);

            btnFilter.Text = text_HideFilter;
           
            LoadCustomerList();
        }

        #region variable/object declare

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SPPDataDAL dalSPP = new SPPDataDAL();
        SPPDataBLL uSpp = new SPPDataBLL();

        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string text_SelectPO = "PLEASE SELECT PO";
        readonly string text_AddDo = "ADD DO";
        readonly string text_NextStep = "NEXT STEP";
        readonly string text_SelectAll = "SELECT ALL";
        readonly string text_AddNewPO = "+ NEW PO";
        readonly string text_POList = "PO LIST";
        readonly string text_POItemList = "PO ITEM LIST";
        readonly string text_CombineDO = "COMBINE DO";
        readonly string text_Cancel = "CANCEL";
        readonly string text_CombineConfirm = "COMBINE CONFIRM";

        readonly string text_DOList = "DO TO ADD";
        readonly string text_DOItemList = "DO ITEM LIST";

        readonly string text_AvailableStock = "AVAILABLE";
        readonly string text_InsufficientStock = "INSUFFICIENT";

        readonly string text_DB = "DB";
        readonly string text_ToAdd = "TO ADD";
        readonly string text_ToEdit = "TO EDIT";
        readonly string text_ToUpdate = "TO UPDATE";

        readonly string header_POTblCode = "PO TBL CODE";
        readonly string header_POCode = "PO CODE";
        readonly string header_PONo = "PO NO";
        readonly string header_PODate = "PO DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_Progress = "PROGRESS";
        readonly string header_Selected = "SELECTED";
        readonly string header_DataMode = "DATA MODE";

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_OrderQty = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY";
        readonly string header_Note = "NOTE";
        readonly string header_StockCheck = "STOCK CHECK";
        readonly string header_DONo = "DO #";
        readonly string header_DONoString = "DO NO";
        readonly string header_CombinedCode = "COMBINED CODE";
        readonly string header_Stock = "STOCK QTY";
        readonly string header_StockString = "STOCK";
        readonly string header_DeliveryPCS = "DELIVERY PCS";
        readonly string header_DeliveryBAG = "DELIVERY BAG";
        readonly string header_DeliveryQTY = "DELIVERY QTY";
        readonly string header_Balance = "BAL No";
        readonly string header_StdPacking = "StdPacking";
        readonly string header_BalanceString = "BAL.";

        private DataTable dt_DOList;
        private DataTable dt_DOItemList_1;

        private bool addingDOMode = false;
        private bool ableToNextStep = false;
        private bool addingDOStep1 = false;
        private bool POMode = true;
        private bool addingDOStep2 = false;
        private bool combineDO = false;
        private bool combiningDO = false;

        private int selectedDO = 0;
        private int oldData = 0;
        private int adjustRow = -1;
        private int adjustCol = -1;
        #endregion

        #region data table 

        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));

            return dt;
        }

        private DataTable NewDOItemTable()
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add(header_POTblCode, typeof(int));
            //dt.Columns.Add(header_DataMode, typeof(string));
            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));

            //dt.Columns.Add(header_Stock, typeof(int));
            //dt.Columns.Add(header_StockString, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            //dt.Columns.Add(header_DONo, typeof(int));
            //dt.Columns.Add(header_DONoString, typeof(string));
            //dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));
            //dt.Columns.Add(header_Balance, typeof(int));
            //dt.Columns.Add(header_BalanceString, typeof(string));
            //dt.Columns.Add(header_Note, typeof(string));
            dt.Columns.Add(header_StdPacking, typeof(int));
            return dt;
        }



        #endregion

        #region UI

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            if (dgv == dgvDOList)
            {
                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PONo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_DONo].Visible = false;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
            }
            else if(dgv == dgvItemList)
            {
                dgv.Columns[header_DeliveryPCS].Visible = false;
                dgv.Columns[header_DeliveryBAG].Visible = false;
                dgv.Columns[header_StdPacking].Visible = false;

                dgv.Columns[header_DeliveryQTY].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_DeliveryPCS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_DeliveryBAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }

        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowFilter(bool showMoreFilter)
        {
            if (showMoreFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadDOList();
        }

        private void dgvDOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvDOList;

            if (rowIndex >= 0)
            {
                btnEdit.Visible = true;
                btnRemove.Visible = true;
                string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
                ShowDOItem(code);
            }
            else
            {
                dgvItemList.DataSource = null;
                btnEdit.Visible = false;
                btnRemove.Visible = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Load Data

        private void LoadCustomerList()
        {
            DataTable dt = dalSPP.CustomerWithoutRemovedDataSelect();

            DataTable locationTable = dt.DefaultView.ToTable(true, dalSPP.FullName);

            DataRow dr;
            dr = locationTable.NewRow();
            dr[dalSPP.FullName] = "ALL";

            locationTable.Rows.InsertAt(dr, 0);

            cmbCustomer.DataSource = locationTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;

        }

        private void frmSPPDOList_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();
            LoadDOList();
           
        }

        private void LoadDOList()
        {
            btnEdit.Visible = false;
            btnRemove.Visible = false;
            dgvItemList.DataSource = null;

            dt_DOList = dalSPP.DOWithInfoSelect();

            DataTable dt = NewDOTable();
            DataRow dt_row;

            int preDOCode = -1;

            foreach (DataRow row in dt_DOList.Rows)
            {
                int doCode = int.TryParse(row[dalSPP.DONo].ToString(), out doCode) ? doCode : -1;

                if ((preDOCode == -1 || preDOCode != doCode) && row[dalSPP.DONo] != DBNull.Value)
                {
                    preDOCode = doCode;

                    int deliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                    bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;

                    bool dataMatched = !isRemoved;

                    #region DO TYPE

                    if (!isDelivered && cbInProgress.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if (isDelivered && cbCompleted.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else
                    {
                        dataMatched = false;
                    }

                    #endregion

                    #region Customer Filter

                    string customer = cmbCustomer.Text;

                    if (string.IsNullOrEmpty(customer) || customer == "ALL")
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if (row[dalSPP.FullName].ToString() == customer)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else
                    {
                        dataMatched = false;
                    }

                    #endregion

                    if (dataMatched)
                    {
                        dt_row = dt.NewRow();

                        dt_row[header_DONo] = doCode;
                        dt_row[header_DONoString] = doCode.ToString("D6");
                        dt_row[header_PONo] = row[dalSPP.PONo];
                        dt_row[header_POCode] = row[dalSPP.POCode];
                        dt_row[header_PODate] = Convert.ToDateTime(row[dalSPP.PODate]).Date;
                        dt_row[header_Customer] = row[dalSPP.ShortName];
                        dt_row[header_CustomerCode] = row[dalSPP.CustomerTableCode];
                        dt.Rows.Add(dt_row);
                    }

                }

            }


            dgvDOList.DataSource = dt;
            DgvUIEdit(dgvDOList);
            dgvDOList.ClearSelection();

        }

        private void ShowDOItem(string doCode)
        {
            DataTable dt = dalSPP.DOWithInfoSelect();

            dt.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
            dt = dt.DefaultView.ToTable();

            DataTable dt_DOItemList = NewDOItemTable();

            DataRow dt_Row;
            int index = 1;
            string preType = null;

            foreach (DataRow row in dt.Rows)
            {
                if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value)
                {
                    int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if (preType == null)
                    {
                        preType = type;
                    }
                    else if (preType != type)
                    {
                        preType = type;
                        dt_Row = dt_DOItemList.NewRow();
                        dt_DOItemList.Rows.Add(dt_Row);
                    }

                    dt_Row = dt_DOItemList.NewRow();

                    int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;

                    int bag = deliveryQty / stdPacking;

                    dt_Row[header_Index] = index;
                    dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                    dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Type] = type;
                    dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                    dt_Row[header_StdPacking] = stdPacking;
                    dt_Row[header_DeliveryPCS] = deliveryQty;
                    dt_Row[header_DeliveryBAG] = bag;
                    dt_Row[header_DeliveryQTY] = deliveryQty + " (" + bag + "bags)";

                    dt_DOItemList.Rows.Add(dt_Row);
                    index++;
                }
            }

            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvItemList.DataSource = dt_DOItemList;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();
        }


        #endregion

        private void dgvDOList_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Visible = true;
            btnRemove.Visible = true;

            DataGridView dgv = dgvDOList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
            }


            DataTable dt = (DataTable)dgvDOList.DataSource;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_DONo].ToString();
                ShowDOItem(code);
            }
            else
            {
                dgvItemList.DataSource = null;
                btnEdit.Visible = false;
                btnRemove.Visible = false;
            }
        }

        private void RemoveDO(int DONo)
        {
            uSpp.IsRemoved = true;
            uSpp.DO_no = DONo;

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;

            if(dalSPP.DORemove(uSpp))
            {
                MessageBox.Show("DO Removed!");
            }
            else
            {
                MessageBox.Show("Unable to delete DO!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvDOList;

            int rowIndex = dgv.CurrentCell.RowIndex;

            if(rowIndex >= 0)
            {
                DataTable dt = NewDOTable();

                DataRow dt_Row = dt.NewRow();

                dt_Row[header_DONo] = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString());
                dt_Row[header_DONoString] = dgv.Rows[rowIndex].Cells[header_DONoString].Value.ToString();
                dt_Row[header_PONo] = dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString();
                dt_Row[header_POCode] = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString());
                dt_Row[header_PODate] = dgv.Rows[rowIndex].Cells[header_PODate].Value.ToString();
                dt_Row[header_Customer] = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
                dt_Row[header_CustomerCode] = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString());

                dt.Rows.Add(dt_Row);

                frmSPPPOList frm = new frmSPPPOList(dt)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();
            }
           
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvDOList;

            int rowIndex = dgv.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                int DONo = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString());

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to REMOVE DO: "+DONo.ToString("D6")+" ?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    RemoveDO(DONo);

                    LoadDOList();
                }
            }
        }
    }
}
