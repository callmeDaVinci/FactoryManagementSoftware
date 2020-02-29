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
    public partial class frmSPPPOList : Form
    {
        public frmSPPPOList()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvPOList, true);
            tool.DoubleBuffered(dgvDOList, true);
            tool.DoubleBuffered(dgvItemList, true);


            btnFilter.Text = text_HideFilter;
            dt_POList = dalSPP.POSelect();
            LoadCustomerList();

        }

        public frmSPPPOList(DataTable dt)
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvPOList, true);
            tool.DoubleBuffered(dgvDOList, true);
            tool.DoubleBuffered(dgvItemList, true);


            btnFilter.Text = text_HideFilter;
            dt_POList = dalSPP.POSelect();
            LoadCustomerList();

            EditMode = true;

            AddingDOStep2();

            POToDO(dt);

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
        readonly string text_AddDo= "ADD DO";
        readonly string text_NextStep = "NEXT STEP";
        readonly string text_SelectAll = "SELECT ALL";
        readonly string text_AddNewPO = "+ NEW PO";
        readonly string text_POList = "PO LIST";
        readonly string text_POItemList = "PO ITEM LIST";
        readonly string text_CombineDO = "COMBINE DO";
        readonly string text_DOUpdate = "DO UPDATE";
        readonly string text_Cancel = "CANCEL";
        readonly string text_CombineConfirm = "COMBINE CONFIRM";

        readonly string text_DOList = "DO TO ADD";
        readonly string text_EditDOList = "DO TO EDIT";
        readonly string text_DOItemList = "DO ITEM LIST";

        readonly string text_AvailableStock = "AVAILABLE";
        readonly string text_InsufficientStock = "INSUFFICIENT";

        readonly string text_DB = "DB";
        readonly string text_ToAdd = "TO ADD";
        readonly string text_ToEdit = "TO EDIT";
        readonly string text_ToUpdate = "TO UPDATE";

        readonly string header_POTblCode = "PO TBL CODE";
        readonly string header_POCode = "PO CODE";
        readonly string header_PONoString = "PO NO";
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
        readonly string header_DOTblCode = "DO TBL CODE";
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

        private DataTable dt_POList;
        private DataTable dt_DOItemList_1;
        private DataTable dt_DOEditList;

        private bool addingDOMode = false;
        private bool ableToNextStep = false;
        private bool addingDOStep1 = false;
        private bool POMode = true;
        private bool addingDOStep2 = false;
        private bool combineDO = false;
        private bool combiningDO = false;
        private bool EditMode = false;

        private int selectedDO = 0;
        private int oldData = 0;
        private int adjustRow = -1;
        private int adjustCol = -1;
        #endregion

        private DataTable NewExcelTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add(header_PODate, typeof(DateTime));
            
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));

            

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_Progress, typeof(string));

            dt.Columns.Add(header_DONoString, typeof(string));

            return dt;
        }

        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));
            
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_StockCheck, typeof(string));
            dt.Columns.Add(header_CombinedCode, typeof(string));
            return dt;
        }

        private DataTable NewPOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
         
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private DataTable NewDOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DataMode, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            
            dt.Columns.Add(header_Stock, typeof(int));
            dt.Columns.Add(header_StockString, typeof(string));
            dt.Columns.Add(header_POCode, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));
            dt.Columns.Add(header_Balance, typeof(int));
            dt.Columns.Add(header_BalanceString, typeof(string));
            dt.Columns.Add(header_Note, typeof(string));
            dt.Columns.Add(header_StdPacking, typeof(int));
            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            if (dgv == dgvPOList)
            {
                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Progress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
            }
            else if (dgv == dgvDOList)
            {
                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_DONo].Visible = false;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
                dgv.Columns[header_CombinedCode].Visible = false;
            }
            else if (dgv == dgvItemList)
            {
                if(POMode)
                {
                    dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    dgv.Columns[header_DeliveredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_OrderQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else if(addingDOStep2 || EditMode)
                {
                    dgv.Columns[header_DONo].Visible = false;
                    dgv.Columns[header_Stock].Visible = false;
                    dgv.Columns[header_DeliveryPCS].Visible = false;
                    dgv.Columns[header_DeliveryBAG].Visible = false;
                    dgv.Columns[header_POCode].Visible = false;
                    dgv.Columns[header_Note].Visible = false;
                    dgv.Columns[header_StdPacking].Visible = false;
                    dgv.Columns[header_Balance].Visible = false;
                    dgv.Columns[header_DataMode].Visible = false;
                    dgv.Columns[header_POTblCode].Visible = false;
                    dgv.Columns[header_DOTblCode].Visible = false;

                    dgv.Columns[header_DONoString].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    //dgv.Columns[header_StockString].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    dgv.Columns[header_DeliveryQTY].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    //dgv.Columns[header_Size].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    //dgv.Columns[header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                    dgv.Columns[header_DONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_DeliveryPCS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_DeliveryBAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }

                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            }
           
        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if(filterText == text_ShowFilter)
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

        private void ShowDOList()
        {
            lblMainList.Text = text_DOList;
            lblSubList.Text = text_DOItemList;

            if(EditMode)
            {
                lblMainList.Text = text_EditDOList;
            }

            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 45);
        }

        private void ShowPOList()
        {
            lblMainList.Text = text_POList;
            lblSubList.Text = text_POItemList;

           
            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 45);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void frmSPPPOList_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();

            if (!EditMode)
            {
                ShowPOList();
                LoadPOList();
            }
            else
            {
                dgvDOList.ClearSelection();
                dgvItemList.ClearSelection();
            }
          
        }

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

        private DataTable AddSpaceToList(DataTable dt)
        {
            string preType = null;
            DataTable dt_Copy = dt.Copy();

            foreach(DataRow row in dt.Rows)
            {
                string type = row[header_Type].ToString();

                if(preType == null)
                {
                    preType = type;
                }
                else if(preType != type)
                {
                    preType = type;
                    dt.Rows.Add();
                    dt.AcceptChanges();
                }
            }

            return dt;
        }

        private string GetDONo(DataTable dt_DO, string poNo)
        {
            string DONo = "";

            string preDoNo = null;
            foreach(DataRow row in dt_DO.Rows)
            {
                string currentDO = row[dalSPP.DONo].ToString();
                string PONo = row[dalSPP.PONo].ToString();
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (poNo == PONo && !isRemoved)
                {
                    if(preDoNo == null || preDoNo != currentDO)
                    {

                        if(preDoNo != null)
                        {
                            DONo += "/";
                        }

                        DONo += Convert.ToInt16(currentDO).ToString("D6");
                        preDoNo = currentDO;
                    }
                   
                }
            }

            return DONo;
        }

        private void LoadPOList()
        {
            btnEdit.Visible = false;
            dgvItemList.DataSource = null;

            DataTable dt = NewPOTable();
            DataTable dt_DOList = dalSPP.DOWithInfoSelect();

            DataRow dt_row;

            int prePOCode = -1;
            foreach(DataRow row in dt_POList.Rows)
            {
                int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;

                if(prePOCode == -1 || prePOCode != poCode)
                {
                    prePOCode = poCode;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                    int progress = deliveredQty / orderQty * 100;

                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                    bool dataMatched = !isRemoved;

                    #region PO TYPE

                    if (progress < 100 && cbInProgressPO.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if(progress >= 100 && cbCompletedPO.Checked)
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

                    if(string.IsNullOrEmpty(customer) || customer == "ALL")
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if(row[dalSPP.FullName].ToString() == customer)
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

                        
                        dt_row[header_POCode] = poCode;
                        dt_row[header_PONoString] = row[dalSPP.PONo];
                        dt_row[header_PODate] = Convert.ToDateTime(row[dalSPP.PODate]).Date;
                        dt_row[header_Customer] = row[dalSPP.ShortName];
                        dt_row[header_CustomerCode] = row[dalSPP.CustomerTableCode];
                        dt_row[header_Progress] = progress + "%";
                        dt_row[header_DONoString] = GetDONo(dt_DOList, row[dalSPP.PONo].ToString()) ;
                        dt.Rows.Add(dt_row);
                    }
                   
                }
               
            }

            
            dgvPOList.DataSource = dt;
            DgvUIEdit(dgvPOList);
            dgvPOList.ClearSelection();

        }

        private void btnAddNewPO_Click(object sender, EventArgs e)
        {
            if(addingDOStep1)
            {
                //select all
                DataTable dt = (DataTable) dgvPOList.DataSource;

                if(dt.Columns.Contains(header_Selected))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row[header_Selected] = true;
                    }
                }

                btnAddDO.Text = text_NextStep;
                btnAddDO.Enabled = true;
               
            }
            else if(POMode)
            {
                btnEdit.Visible = false;
                frmSPPNewPO frm = new frmSPPNewPO
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();
                dt_POList = dalSPP.POSelect();
                LoadPOList();
            }
            else if(addingDOStep2)
            {
                if(combiningDO)
                {
                    SetCombinedCode();
                    MessageBox.Show("DO Combined!");
                    btnConfirmToAddDO.Visible = true;
                    btnBackToPOList.Visible = true;
                    btnEdit.Visible = false;

                    DataTable dt = (DataTable)dgvDOList.DataSource;

                    if (dt.Columns.Contains(header_Selected))
                    {
                        dt.Columns.Remove(header_Selected);
                    }

                    dgvDOList.Columns[header_StockCheck].Visible = true;

                    btnAddNewPO.Text = text_CombineDO;
                    btnAddNewPO.Enabled = true;

                    combiningDO = false;

                    dgvDOList.DataSource = RefreshDOList();


                    DgvUIEdit(dgvDOList);
                    dgvDOList.ClearSelection();
                }
                else
                {
                    selectedDO = 0;
                    combiningDO = true;
                    btnConfirmToAddDO.Visible = false;
                    btnBackToPOList.Visible = false;
                    DataTable dt = (DataTable)dgvDOList.DataSource;

                    dt = AddSelectedColumn(dt);

                    dgvDOList.Columns[header_StockCheck].Visible = false;

                    btnEdit.Text = text_Cancel;
                    btnEdit.Visible = true;

                    btnAddNewPO.Text = text_CombineConfirm;
                    btnAddNewPO.Enabled = false;

                }
               
            }
           
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadPOList();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvItemList.DataSource = null;
        }

        private void cbInProgressPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvItemList.DataSource = null;
        }

        private void cbCompletedPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvItemList.DataSource = null;
        }

        private void dgvPOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
           // DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvPOList;

            if (rowIndex >= 0)
            {
                if(addingDOStep1)
                {
                    bool selected = bool.TryParse(dgv.Rows[rowIndex].Cells[header_Selected].Value.ToString(), out selected) ? selected : false;

                    if(!selected)
                    {
                        dgv.Rows[rowIndex].Cells[header_Selected].Value = true;

                        btnAddDO.Text = text_NextStep;
                        btnAddDO.Enabled = true;
                        ableToNextStep = true;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[header_Selected].Value = false;

                        if(!ifPOSelected())
                        {
                            btnAddDO.Text = text_SelectPO;
                            btnAddDO.Enabled = false;
                        }
                    }
                }
                else if(POMode)
                {
                    btnEdit.Visible = true;
                    string code = dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString();
                    ShowPOItem(code);

                }
            }
            else
            {
                dgvItemList.DataSource = null;
                btnEdit.Visible = false;
            }
        }

        private void ShowPOItem(string poCode)
        {
            DataTable dt = dalSPP.POSelectWithSizeAndType();

            DataTable dt_POItemList = NewPOItemTable();
            DataRow dt_Row;
            int index = 1;
            string preType = null;
            foreach (DataRow row in dt.Rows)
            {
                if (poCode == row[dalSPP.POCode].ToString())
                {

                    int deliveredQty = row[dalSPP.DeliveredQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.DeliveredQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if(preType == null)
                    {
                        preType = type;
                    }
                    else if(preType != type)
                    {
                        preType = type;
                        dt_Row = dt_POItemList.NewRow();
                        dt_POItemList.Rows.Add(dt_Row);
                    }

                    dt_Row = dt_POItemList.NewRow();
                    dt_Row[header_Index] = index;
                    dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                    dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Type] = type;
                    dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                    dt_Row[header_DeliveredQty] = deliveredQty;
                    dt_Row[header_OrderQty] = row[dalSPP.POQty];
                    dt_Row[header_Note] = row[dalSPP.PONote];

                    dt_POItemList.Rows.Add(dt_Row);
                    index++;
                }
            }

            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvItemList.DataSource = dt_POItemList;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();
        }

        private void EditPO()
        {
            //get item info
            DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = dgvPOList.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();

                frmSPPNewPO frm = new frmSPPNewPO(code)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPNewPO.poEdited || frmSPPNewPO.poRemoved)
                {
                    btnEdit.Visible = false;
                    dt_POList = dalSPP.POSelect();
                    LoadPOList();
                }

            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private DataTable GetDOItem(DataTable dt_DBSource , string poCode, int DONo, string customerName)
        {
            

            DataTable dt_DOItemList = NewDOItemTable();
            DataRow dt_Row;

            string preType = null;
            foreach (DataRow row in dt_DBSource.Rows)
            {
                if (poCode == row[dalSPP.POCode].ToString())
                {
                    string type = row[dalSPP.TypeName].ToString();

                    if (preType == null)
                    {
                        preType = type;
                    }
                    else if (preType != type)
                    {
                        preType = type;
                        //dt_Row = dt_DOItemList.NewRow();
                        //dt_DOItemList.Rows.Add(dt_Row);
                    }

                    int stockQty = int.TryParse(row[dalItem.ItemQty].ToString(), out stockQty) ? stockQty : 0;
                    int deliveryQty = int.TryParse(row[dalSPP.POQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                    if(EditMode)
                    {
                        deliveryQty = int.TryParse(row[dalSPP.DOToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;
                    }

                    int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                    int deliveryBag = deliveryQty / qtyPerBag;
                    dt_Row = dt_DOItemList.NewRow();

                    dt_Row[header_POTblCode] = row[dalSPP.TableCode];
                    dt_Row[header_DataMode] = text_ToAdd;
                    dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                    dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Type] = type;
                    dt_Row[header_Stock] = row[dalItem.ItemQty];
                    dt_Row[header_StockString] = row[dalItem.ItemQty] + " ("+stockQty/qtyPerBag + "bags)";
                    dt_Row[header_POCode] = row[dalSPP.POCode];
                    dt_Row[header_DONo] = DONo;
                    dt_Row[header_DONoString] = DONo.ToString("D6") + "-NEW";

                    if (EditMode)
                    {
                        dt_Row[header_DOTblCode] = row[dalSPP.TableCode];
                        dt_Row[header_DataMode] = text_ToEdit;
                        dt_Row[header_DONoString] = DONo.ToString("D6") + "-EDIT";
                    }

                    dt_Row[header_Customer] = customerName;
                    dt_Row[header_DeliveryPCS] = deliveryQty;
                    dt_Row[header_DeliveryQTY] = deliveryQty + " (" + deliveryBag + "bags)";
                    dt_Row[header_DeliveryBAG] = deliveryBag;
                    dt_Row[header_StdPacking] = qtyPerBag;
                    dt_DOItemList.Rows.Add(dt_Row);
                }
            }

            return dt_DOItemList;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (POMode)
            {
                EditPO();
            }
            else if(addingDOStep2)
            {
                if(combiningDO)
                {
                    btnEdit.Visible = false;
                    btnConfirmToAddDO.Visible = true;
                    btnBackToPOList.Visible = true;
                    DataTable dt = (DataTable)dgvDOList.DataSource;

                    if (dt.Columns.Contains(header_Selected))
                    {
                        dt.Columns.Remove(header_Selected);
                    }

                    dgvDOList.Columns[header_StockCheck].Visible = true;

                    btnAddNewPO.Text = text_CombineDO;
                    btnAddNewPO.Enabled = true;

                    combiningDO = false;

                }
                
            }
        }

        private void dgvPOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!addingDOMode)
            EditPO();
        }

        private bool ifPOSelected()
        {
            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (dt.Columns.Contains(header_Selected))
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                    if(selected)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ifDoSelected()
        {
            DataTable dt = (DataTable)dgvDOList.DataSource;

            if (dt.Columns.Contains(header_Selected))
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void dgvPOList_SelectionChanged(object sender, EventArgs e)
        {
            if(POMode)
            {
                btnEdit.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
            }

            DataGridView dgv = dgvPOList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
            }

            
            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvItemList.DataSource = null;
                btnEdit.Visible = false;
            }

        }

        private void CombineCustomerData()
        {
            DataGridView dgv = dgvPOList;

            DataTable dt_TargetPO = (DataTable)dgv.DataSource;
            DataTable dt_DBSource = dalSPP.POSelectWithSizeAndType();

            dt_TargetPO.DefaultView.Sort = header_CustomerCode + " ASC, " + header_PODate + " DESC," + header_POCode + " DESC";
            dt_TargetPO = dt_TargetPO.DefaultView.ToTable();

            dt_DBSource.DefaultView.Sort = dalSPP.CustomerTableCode + " ASC, " + dalSPP.PODate + " DESC, " + dalSPP.POCode + " DESC";
            dt_DBSource = dt_DBSource.DefaultView.ToTable();

            int preCustomerCode = -1;

            DataTable dt_Excel = NewExcelTable();
            DataRow dt_Row;

            foreach (DataRow row in dt_TargetPO.Rows)
            {
                int poCode = Convert.ToInt32(row[header_POCode].ToString());

                foreach (DataRow db in dt_DBSource.Rows)
                {
                    int dbCode = Convert.ToInt32(db[dalSPP.POCode].ToString());

                    if (dbCode == poCode)
                    {
                        int customerCode = Convert.ToInt32(db[dalSPP.CustomerTableCode].ToString());
                        string customerShortName = db[dalSPP.ShortName].ToString();

                        if (preCustomerCode == customerCode)
                        {
                            //combine data in same sheet

                        }
                        else
                        {

                            if (preCustomerCode != -1)
                            {
                                dt_Row = dt_Excel.NewRow();
                                dt_Excel.Rows.Add(dt_Row);
                            }

                            preCustomerCode = customerCode;
                            //save data in new sheet
                        }

                        dt_Row = dt_Excel.NewRow();

                        dt_Row[header_POCode] = db[dalSPP.POCode];
                        dt_Row[header_PODate] = db[dalSPP.PODate];
                        dt_Row[header_Size] = db[dalSPP.SizeNumerator];
                        dt_Row[header_Unit] = db[dalSPP.SizeUnit] + " " + customerShortName;
                        dt_Row[header_Type] = db[dalSPP.TypeName];
                        dt_Row[header_ItemCode] = db[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = db[dalSPP.DeliveredQty];
                        dt_Row[header_OrderQty] = db[dalSPP.POQty];
                        dt_Row[header_Note] = db[dalSPP.PONote];

                        dt_Excel.Rows.Add(dt_Row);

                    }
                }
            }
            dgvItemList.DataSource = null;
            dgvItemList.DataSource = dt_Excel;
            dgvItemList.ClearSelection();
        }

        #region export to excel

        static void OpenCSVWithExcel(string path)
        {
            var ExcelApp = new Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);

            ExcelApp.Visible = true;
        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "SPP PO REPORT_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void copyDGVtoClipboard(DataGridView dgv)
        {
            dgv.SelectAll();
            DataObject dataObj = dgv.GetClipboardContent();
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

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\SPP PO Report";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SPP PO Report_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;
                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    InsertAllDataToSheet(path, sfd.FileName);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    LoadPOList();
                    dgvPOList.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void InsertAllDataToSheet(string path, string fileName)
        {
            Excel.Application excelApp = new Excel.Application
            {
                Visible = true
            };

            Workbook g_Workbook = excelApp.Workbooks.Open(
               path,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing);

            object misValue = Missing.Value;


            DataGridView dgv = dgvPOList;

            DataTable dt_TargetPO = (DataTable)dgv.DataSource;
            DataTable dt_DBSource = dalSPP.POSelectWithSizeAndType();

            dt_TargetPO.DefaultView.Sort = header_CustomerCode + " ASC, " + header_PODate + " DESC," + header_POCode + " DESC";
            dt_TargetPO = dt_TargetPO.DefaultView.ToTable();

            dt_DBSource.DefaultView.Sort = dalSPP.CustomerTableCode + " ASC, " + dalSPP.PODate + " DESC, " + dalSPP.POCode + " DESC";
            dt_DBSource = dt_DBSource.DefaultView.ToTable();

            int preCustomerCode = -1;
            string customerShortName = "";
            DataTable dt_Excel = NewExcelTable();
            DataRow dt_Row;

            int preCode = -1;

            foreach (DataRow row in dt_TargetPO.Rows)
            {
                int poCode = Convert.ToInt32(row[header_POCode].ToString());

                foreach (DataRow db in dt_DBSource.Rows)
                {
                    int dbCode = Convert.ToInt32(db[dalSPP.POCode].ToString());

                    if (dbCode == poCode)
                    {
                        int customerCode = Convert.ToInt32(db[dalSPP.CustomerTableCode].ToString());


                        if (preCustomerCode == customerCode)
                        {
                            //combine data in same sheet
                            if (preCode != dbCode)
                            {
                                dt_Excel.Rows.Add(dt_Excel.NewRow());
                                preCode = dbCode;
                            }
                            
                            //if(preCode == -1)
                            //{
                            //    preCode = dbCode;
                            //}
                            //else if(preCode != dbCode)
                            //{
                            //    dt_Excel.Rows.Add(dt_Excel.NewRow());
                            //    preCode = dbCode;
                            //}
                        }
                        else
                        {
                            if (preCustomerCode != -1)
                            {
                                dgvItemList.DataSource = null;
                                dgvItemList.DataSource = dt_Excel;
                                dgvItemList.ClearSelection();

                                MoveUniqueDataToSheet(g_Workbook, customerShortName);

                                dgvItemList.DataSource = null;
                                dt_Excel = NewExcelTable();
                                preCode = -1;

                            }

                            preCode = dbCode;
                            preCustomerCode = customerCode;
                            //save data in new sheet
                        }


                        customerShortName = db[dalSPP.ShortName].ToString();
                        dt_Row = dt_Excel.NewRow();

                        dt_Row[header_PONoString] = db[dalSPP.PONo];
                        dt_Row[header_PODate] = Convert.ToDateTime(db[dalSPP.PODate]).Date;
                        dt_Row[header_Size] = db[dalSPP.SizeNumerator];
                        dt_Row[header_Unit] = db[dalSPP.SizeUnit] ;
                        dt_Row[header_Type] = db[dalSPP.TypeName];
                        dt_Row[header_ItemCode] = db[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = db[dalSPP.DeliveredQty];
                        dt_Row[header_OrderQty] = db[dalSPP.POQty];
                        dt_Row[header_Note] = db[dalSPP.PONote];

                        dt_Excel.Rows.Add(dt_Row);
                        

                    }
                }
            }
         
            if(dt_Excel.Rows.Count > 0)
            {
                dgvItemList.DataSource = null;
                dgvItemList.DataSource = dt_Excel;
                dgvItemList.ClearSelection();

                MoveUniqueDataToSheet(g_Workbook, customerShortName);

                dgvItemList.DataSource = null;
                dt_Excel = NewExcelTable();
            }

            
            #endregion

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void MoveUniqueDataToSheet(Workbook g_Workbook, string CustomerName)
        {
            DataGridView dgv = dgvItemList;

            #region move data to sheet

            Worksheet xlWorkSheet = null;

            int count = g_Workbook.Worksheets.Count;

            xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

            xlWorkSheet.Name = CustomerName;

            xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&10 SPP PO REPORT: " + CustomerName;
            xlWorkSheet.PageSetup.RightHeader = "&\"Courier New\"&8 PG -&P";
            xlWorkSheet.PageSetup.CenterFooter = "&\"Courier New\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

            xlWorkSheet.PageSetup.CenterHorizontally = true;
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;

            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



            // Paste clipboard results to worksheet range
            copyDGVtoClipboard(dgvItemList);

            xlWorkSheet.Select();
            Range CR = (Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            #region old format setting

            Range tRange = xlWorkSheet.UsedRange;
            tRange.Font.Size = 11;
            tRange.RowHeight = 30;
            tRange.VerticalAlignment = XlHAlign.xlHAlignCenter;
            tRange.Font.Name = "Courier New";

            tRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

            //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            //tRange.Borders.Weight = XlBorderWeight.xlThin;

            //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
            Range FirstRow = xlWorkSheet.get_Range("a1:i1").Cells;
            FirstRow.WrapText = true;
            FirstRow.Font.Size = 8;

            FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
            FirstRow.RowHeight = 20;
            FirstRow.Interior.Color = Color.WhiteSmoke;
            FirstRow.Borders.LineStyle = XlLineStyle.xlContinuous;
            FirstRow.Borders.Weight = XlBorderWeight.xlThin;

            tRange.EntireColumn.AutoFit();


            DataTable dt = (DataTable)dgvItemList.DataSource;

            int PONOIndex = dgv.Columns[header_PONoString].Index;
            int PODateIndex = dgv.Columns[header_PODate].Index;
            int SizeIndex = dgv.Columns[header_Size].Index;
            int UnitIndex = dgv.Columns[header_Unit].Index;
            int TypeIndex = dgv.Columns[header_Type].Index;
            int CodeIndex = dgv.Columns[header_ItemCode].Index;
            int DeliveredQtyIndex = dgv.Columns[header_DeliveredQty].Index;
            int OrderQtyIndex = dgv.Columns[header_OrderQty].Index;
            int NoteIndex = dgv.Columns[header_Note].Index;

            xlWorkSheet.Cells[1, PONOIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, PODateIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, CodeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, DeliveredQtyIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, OrderQtyIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, NoteIndex + 1].ColumnWidth = 10;

            xlWorkSheet.Cells[1, PONOIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, PODateIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[1, SizeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, UnitIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, TypeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, CodeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, DeliveredQtyIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, OrderQtyIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, NoteIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;

            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                Range rangePODate = (Range)xlWorkSheet.Cells[j + 2, PODateIndex + 1];
                rangePODate.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeType = (Range)xlWorkSheet.Cells[j + 2, TypeIndex + 1];

                Range rangeDelievered = (Range)xlWorkSheet.Cells[j + 2, DeliveredQtyIndex + 1];
                rangeDelievered.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeNote = (Range)xlWorkSheet.Cells[j + 2, NoteIndex + 1];


                Color color = Color.Black;
                rangePODate.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangePODate.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                

                rangeType.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeType.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeDelievered.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeDelievered.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
                

                rangeNote.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeNote.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

            }

            #endregion

            releaseObject(xlWorkSheet);
            Clipboard.Clear();

            dgvItemList.ClearSelection();
            #endregion
        }

        private int GetNewDONo()
        {
            int DoNo = 1;

            DataTable dt = dalSPP.DOSelect();
            dt.DefaultView.Sort = dalSPP.DONo + " DESC";
            dt = dt.DefaultView.ToTable();

            foreach(DataRow row in dt.Rows)
            {
                int number = int.TryParse(row[dalSPP.DONo].ToString(), out number) ? number : 0;

                DoNo = number + 1;

                return DoNo;
            }

            return DoNo;
        }


        private void POToDO()
        {
            DataTable dt = (DataTable)dgvPOList.DataSource;

            DataTable dt_DO = NewDOTable();
            DataRow dt_Row;

            int doNo = GetNewDONo();

            foreach (DataRow row in dt.Rows)
            {
                bool selected = false;

                if (dt.Columns.Contains(header_Selected))
                {
                    selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;
                }

                if (selected)
                {
                    dt_Row = dt_DO.NewRow();

                    dt_Row[header_DONo] = doNo;
                    dt_Row[header_DONoString] = doNo.ToString("D6") + "-NEW";
                    dt_Row[header_PONoString] = row[header_PONoString];
                    dt_Row[header_POCode] = row[header_POCode];
                    dt_Row[header_PODate] = row[header_PODate];
                    dt_Row[header_Customer] = row[header_Customer];
                    dt_Row[header_CustomerCode] = row[header_CustomerCode];
                    dt_Row[header_StockCheck] = text_AvailableStock;
                    dt_Row[header_CombinedCode] = DBNull.Value;
                    dt_DO.Rows.Add(dt_Row);
                    doNo++;
                }
            }

            if (IfCustomerDuplicated(dt_DO.Copy()))
            {
                btnAddNewPO.Visible = true;
            }
            else
            {
                btnAddNewPO.Visible = false;
            }


            CombineDOItemList(dt_DO);
            DOItemStockChecking(dt_DO);

            dgvItemList.DataSource = dt_DOItemList_1;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();

            dgvDOList.DataSource = dt_DO;
            DgvUIEdit(dgvDOList);
            dgvDOList.ClearSelection();
        }

        private void POToDO(DataTable dt)
        {
            DataTable dt_DO = NewDOTable();
            DataRow dt_Row;

            foreach (DataRow row in dt.Rows)
            {
                dt_Row = dt_DO.NewRow();

                dt_Row[header_DONo] = row[header_DONo];
                dt_Row[header_DONoString] = row[header_DONoString];
                dt_Row[header_PONoString] = row[header_PONoString];
                dt_Row[header_POCode] = row[header_POCode];
                dt_Row[header_PODate] = row[header_PODate];
                dt_Row[header_Customer] = row[header_Customer];
                dt_Row[header_CustomerCode] = row[header_CustomerCode];
                dt_Row[header_StockCheck] = text_AvailableStock;
                dt_Row[header_CombinedCode] = DBNull.Value;
                dt_DO.Rows.Add(dt_Row);
            }

            btnAddNewPO.Visible = false;

            CombineDOItemList(dt_DO);
            DOItemStockChecking(dt_DO);

            dgvItemList.DataSource = dt_DOItemList_1;
            DgvUIEdit(dgvItemList);

            dgvDOList.DataSource = dt_DO;
            DgvUIEdit(dgvDOList);

            dgvDOList.ClearSelection();
            dgvItemList.ClearSelection();
        }

        private void CombineDOItemList(DataTable dt_DOMainList)
        {
            dt_DOItemList_1 = NewDOItemTable();

            DataTable dt_DBSource;
            if (EditMode)
            {
                dt_DBSource = dalSPP.DOWithInfoSelect();
            }
            else
            {
                dt_DBSource = dalSPP.POSelectWithSizeAndType();
            }

            foreach (DataRow row in dt_DOMainList.Rows)
            {
                int DONo = int.TryParse(row[header_DONo].ToString(), out DONo) ? DONo : -1;

                if(DONo == -1)
                {
                    MessageBox.Show("DO No Error!");
                    break;
                }
                else
                {
                    string poCode = row[header_POCode].ToString();
                    string customerName = row[header_Customer].ToString();

                    //get all "to add" data
                    
                    dt_DOItemList_1.Merge(GetDOItem(dt_DBSource, poCode, DONo,customerName));
                }
            }

            dt_DOItemList_1.DefaultView.Sort = header_Type + " ASC," + header_Size + " ASC";
            dt_DOItemList_1 = dt_DOItemList_1.DefaultView.ToTable();

            DataTable DODataList = dalSPP.DOWithInfoSelect();

            string preType = null;
            string preSize = null;

            DataTable dtDOItemList_2 = dt_DOItemList_1.Clone();
            DataRow dt_Row;

            //get same type data from db
            foreach(DataRow row in dt_DOItemList_1.Rows)
            {
                dtDOItemList_2.ImportRow(row);

                string type = row[header_Type].ToString();
                string size = row[header_Size].ToString();
                string doNo = row[header_DONo].ToString();

                if (preType == null || (preType != type || preSize != size) )
                {
                    preType = type;
                    preSize = size;

                    foreach(DataRow row2 in DODataList.Rows)
                    {
                        bool isDelivered = bool.TryParse(row2[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;
                        bool isRemoved = bool.TryParse(row2[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                        string doNo2 = row2[dalSPP.DONo].ToString();

                        if (!isDelivered && doNo2 != doNo && !isRemoved)
                        {
                            string DBType = row2[dalSPP.TypeName].ToString();
                            string DBSize = row2[dalSPP.SizeNumerator].ToString();

                            if(DBType == type && DBSize == size)
                            {
                                int stockQty = int.TryParse(row2[dalItem.ItemQty].ToString(), out stockQty) ? stockQty : 0;
                                int deliveryQty = int.TryParse(row2[dalSPP.DOToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                                int qtyPerBag = int.TryParse(row2[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                                int deliveryBag = deliveryQty / qtyPerBag;
                                dt_Row = dtDOItemList_2.NewRow();

                                dt_Row[header_POTblCode] = row2["tbl_code1"];
                                
                                dt_Row[header_DataMode] = text_DB;
                                dt_Row[header_DOTblCode] = row2[dalSPP.TableCode];
                                dt_Row[header_Size] = size;
                                dt_Row[header_Unit] = row[header_Unit];
                                dt_Row[header_Type] = type;
                                dt_Row[header_Stock] = row[header_Stock];
                                dt_Row[header_StockString] = row[header_StockString];
                                dt_Row[header_DONo] = row2[dalSPP.DONo];
                                dt_Row[header_POCode] = row2[dalSPP.POCode];
                                dt_Row[header_DONoString] = Convert.ToInt16(row2[dalSPP.DONo]).ToString("D6");
                                dt_Row[header_Customer] = row2[dalSPP.ShortName];
                                dt_Row[header_DeliveryPCS] = row2[dalSPP.DOToDeliveryQty];
                                dt_Row[header_DeliveryQTY] = row2[dalSPP.DOToDeliveryQty] + " (" + deliveryBag + "bags)";
                                dt_Row[header_DeliveryBAG] = deliveryBag;
                                dt_Row[header_StdPacking] = qtyPerBag;
                                dtDOItemList_2.Rows.Add(dt_Row);
                            }
                        }

                    }
                }
            }

            dt_DOItemList_1 = dtDOItemList_2.Copy();

            dt_DOItemList_1.DefaultView.Sort = header_Type + " ASC," + header_Size + " ASC," + header_DONo + " ASC";
            dt_DOItemList_1 = dt_DOItemList_1.DefaultView.ToTable();

        }

        private void DOItemStockChecking(DataTable dt_DO)
        {
            DataTable dt = dt_DOItemList_1.Clone();

            string preType = null, preSize = null;
            int balStock = 0;

            foreach(DataRow row in dt_DOItemList_1.Rows)
            {
                string type = row[header_Type].ToString();
                string size = row[header_Size].ToString();
                int stock = int.TryParse(row[header_Balance].ToString(), out stock) ? stock : int.TryParse(row[header_Stock].ToString(), out stock) ? stock : 0;

                if (preType == null)
                {
                    balStock = stock;
                    preType = type;
                    preSize = size;
                }
                else if(type != preType)
                {
                    balStock = stock;
                    preType = type;
                    preSize = size;
                    dt.Rows.Add(dt.NewRow());
                    dt.Rows.Add(dt.NewRow());
                }
                else if (preSize != size)
                {
                    balStock = stock;
                    preSize = size;
                    dt.Rows.Add(dt.NewRow());
                }
                
                int deliveryQty = int.TryParse(row[header_DeliveryPCS].ToString(), out deliveryQty) ? deliveryQty : 0;
                int qtyPerBag = int.TryParse(row[header_StdPacking].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                int afterBal = balStock - deliveryQty;

                balStock = afterBal;

                row[header_Balance] = afterBal;
                row[header_BalanceString] = afterBal +" ("+afterBal/qtyPerBag+" bags)";

                dt.ImportRow(row);

                if(afterBal < 0)
                {
                    string poCode = row[header_POCode].ToString();
                    string dONo = row[header_DONo].ToString();

                    foreach(DataRow row2 in dt_DO.Rows)
                    {
                        string poCodeMain = row2[header_POCode].ToString();
                        string dONoMain = row2[header_DONo].ToString();

                        if(poCodeMain == poCode && dONoMain == dONo)
                        {
                            row2[header_StockCheck] = text_InsufficientStock;
                        }
                    }
                }
                
            }

            dt_DOItemList_1 = dt.Copy();
        }

        private void DOItemStockChecking(string DONo, string POCode)
        {
            DataTable dt_DO = (DataTable)dgvDOList.DataSource;

            DataTable dt_DOItemList_2 = (DataTable)dgvItemList.DataSource;

            foreach (DataRow rowMain in dt_DO.Rows)
            {
                string poCodeMain = rowMain[header_POCode].ToString();
                string dONoMain = rowMain[header_DONo].ToString();

                if (poCodeMain == POCode && dONoMain == DONo)
                {
                    rowMain[header_StockCheck] = text_AvailableStock;
                }
            }

            foreach (DataRow rowSub in dt_DOItemList_2.Rows)
            {
                string poCodeSub = rowSub[header_POCode].ToString();
                string dONoSub = rowSub[header_DONo].ToString();

                if (poCodeSub == POCode && dONoSub == DONo)
                {
                    int stock = int.TryParse(rowSub[header_Balance].ToString(), out stock) ? stock : int.TryParse(rowSub[header_Stock].ToString(), out stock) ? stock : 0;

                    if (stock < 0)
                    {
                        foreach (DataRow rowMain in dt_DO.Rows)
                        {
                            string poCodeMain = rowMain[header_POCode].ToString();
                            string dONoMain = rowMain[header_DONo].ToString();

                            if (poCodeMain == POCode && dONoMain == DONo)
                            {
                                rowMain[header_StockCheck] = text_InsufficientStock;
                            }
                        }

                        break;
                    }
                }
            }

        }

        private DataTable RefreshDOList()
        {
            DataTable dt = (DataTable)dgvDOList.DataSource;

            dt.DefaultView.Sort = header_PODate + " ASC," + header_POCode + " ASC";

            dt = dt.DefaultView.ToTable();

            DataTable dt_List = dt.Clone();
            DataRow dt_Row;

            int DoNo = -1;

            foreach(DataRow row in dt.Rows)
            {
                if(DoNo == -1)
                {
                    DoNo = int.TryParse(row[header_DONo].ToString(), out DoNo) ? DoNo : -2;

                    if(DoNo == -2)
                    {
                        MessageBox.Show("DO No Error!");

                        return null;
                    }
                }

                bool dataFound = false;
                string poCode = row[header_POCode].ToString();

                foreach(DataRow row2 in dt_List.Rows)
                {
                    if(poCode == row2[header_POCode].ToString())
                    {
                        dataFound = true;
                    }
                }

                if(!dataFound)
                {
                    dt_Row = dt_List.NewRow();

                    string combinedCode = row[header_CombinedCode].ToString();
                    dt_Row[header_DONo] = DoNo;
                    dt_Row[header_DONoString] = DoNo.ToString("D6") + "-NEW";
                    dt_Row[header_PONoString] = row[header_PONoString];
                    dt_Row[header_POCode] = row[header_POCode];
                    dt_Row[header_PODate] = row[header_PODate];
                    dt_Row[header_Customer] = row[header_Customer];
                    dt_Row[header_CustomerCode] = row[header_CustomerCode];
                    //dt_Row[header_StockCheck] = row[header_PONo];
                    dt_Row[header_CombinedCode] = combinedCode;
                    dt_List.Rows.Add(dt_Row);

                    if(!string.IsNullOrEmpty(combinedCode))
                    {
                        foreach (DataRow row3 in dt.Rows)
                        {
                            if(combinedCode == row3[header_CombinedCode].ToString() && poCode != row3[header_POCode].ToString())
                            {
                                dt_Row = dt_List.NewRow();

                                dt_Row[header_DONo] = DoNo;
                                dt_Row[header_DONoString] = "";
                                dt_Row[header_PONoString] = row3[header_PONoString];
                                dt_Row[header_POCode] = row3[header_POCode];
                                dt_Row[header_PODate] = row3[header_PODate];
                                dt_Row[header_Customer] = row3[header_Customer];
                                dt_Row[header_CustomerCode] = row3[header_CustomerCode];
                                //dt_Row[header_StockCheck] = row[header_PONo];
                                dt_Row[header_CombinedCode] = combinedCode;
                                dt_List.Rows.Add(dt_Row);
                            }
                        }
                    }

                    DoNo++;
                }



            }

            
            CombineDOItemList(dt_List);
            DOItemStockChecking(dt_List);

            dgvItemList.DataSource = dt_DOItemList_1;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();

            return dt_List;
        }

        private bool IfCustomerDuplicated(DataTable dt)
        {
            dt.DefaultView.Sort = header_CustomerCode + " ASC";
            dt = dt.DefaultView.ToTable();

            string preCustomer = null;

            foreach(DataRow row in dt.Rows)
            {
                string customer = row[header_CustomerCode].ToString();

                if(preCustomer == null || preCustomer != customer)
                {
                    preCustomer = customer;
                }
                else if(preCustomer == customer)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnAddDO_Click(object sender, EventArgs e)
        {
            frmLoading.ShowLoadingScreen();
            if (POMode)
            {
                AddingDOStep1();
            }
            else if(addingDOStep1)
            {
                
                AddingDOStep2();
                POToDO();
                //stock check
            }

            frmLoading.CloseForm();

        }

        private void btnCancelDOMode_Click(object sender, EventArgs e)
        {
            POListMode();

        }

        private void btnBackToPOList_Click(object sender, EventArgs e)
        {
            if (addingDOStep2)
            {
                if(EditMode)
                {
                    Close();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("All your edited data will be lost.\nAre you sure you want to back to the PO List?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        AddingDOStep1();
                    }
                }
               
            }
            
                
        }

        private void POListMode()
        {
            dgvItemList.DataSource = null;

            addingDOStep1 = false;
            addingDOStep2 = false;
            POMode = true;
            dgvPOList.ClearSelection();

            dgvItemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnExcel.Visible = true;

            cbEditInPcsUnit.Visible = false;
            cbEditInBagUnit.Visible = false;

            btnCancelDOMode.Visible = false;
            btnAddNewPO.Visible = true;
            btnFilter.Visible = true;

            lblMainList.Text = text_POList;
            lblSubList.Text = text_POItemList;
            btnAddNewPO.Text = text_AddNewPO;
            btnAddDO.Text = text_AddDo;

            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (dt != null && dt.Columns.Contains(header_Selected))
            {
                dt.Columns.Remove(header_Selected);
            }

            btnAddDO.Enabled = true;

            LoadPOList();

            dgvPOList.ClearSelection();
            ShowPOList();

        }

        private DataTable AddSelectedColumn(DataTable dt)
        {
            if (!dt.Columns.Contains(header_Selected))
            {
                DataColumn dc = new DataColumn(header_Selected, typeof(bool));

                dt.Columns.Add(dc);
          
            }

            return dt;
        }

        private void AddingDOStep1()
        {
            dgvItemList.DataSource = null;

            addingDOStep1 = true;
            addingDOStep2 = false;
            POMode = false;

            btnExcel.Visible = false;

            cbEditInPcsUnit.Visible = false;
            cbEditInBagUnit.Visible = false;

            lblMainList.Text = text_POList;
            lblSubList.Text = text_POItemList;
            btnAddNewPO.Text = text_SelectAll;

            ShowFilter(false);

            btnFilter.Visible = false;
            btnCancelDOMode.Visible = true;
            btnAddNewPO.Visible = true;

            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (!dt.Columns.Contains(header_Selected))
            {
                DataColumn dc = new DataColumn(header_Selected, typeof(bool));

                dt.Columns.Add(dc);
                btnAddDO.Enabled = false;
                btnAddDO.Text = text_SelectPO;
            }
            else if(ifPOSelected())
            {
                btnAddDO.Enabled = true;
                btnAddDO.Text = text_NextStep;
            }
            else
            {
                btnAddDO.Enabled = false;
                btnAddDO.Text = text_SelectPO;
            }

            dgvPOList.ClearSelection();
           
            ShowPOList();
        }

        private void AddingDOStep2()
        {
            dgvItemList.DataSource = null;
            addingDOStep1 = false;
            addingDOStep2 = true;
            POMode = false;

            btnExcel.Visible = false;
            btnFilter.Visible = false;

            cbEditInPcsUnit.Visible = true;
            cbEditInBagUnit.Visible = true;

            //cbEditInPcsUnit.Checked = false;
            //cbEditInBagUnit.Checked = false;

            

            lblMainList.Text = text_DOList;
            lblSubList.Text = text_DOItemList;
            btnAddNewPO.Text = text_CombineDO;

            btnCancelDOMode.Visible = true;
            btnConfirmToAddDO.Visible = true;
            btnBackToPOList.Visible = true;

            if(EditMode)
            {
                //change back to po list: cancel text, white back color, black fore color, align to right
                btnBackToPOList.Text = text_Cancel;
                btnBackToPOList.BackColor = Color.White;
                btnBackToPOList.ForeColor = Color.Black;
                btnBackToPOList.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                btnBackToPOList.FlatStyle = FlatStyle.Flat;

                btnBack.Visible = false;

                btnConfirmToAddDO.Text = text_DOUpdate;

                lblMainList.Text = text_EditDOList;
            }

            ShowDOList();

            dgvPOList.ClearSelection();
        }

        private void DeliveryQtyEditMode()
        {
            DataGridView dgv = dgvItemList;

            if (cbEditInBagUnit.Checked || cbEditInPcsUnit.Checked)
            {
                string headerName = "";
                if(cbEditInBagUnit.Checked)
                {
                    headerName = header_DeliveryBAG;
                    dgv.Columns[header_DeliveryBAG].Visible = true;
                    dgv.Columns[header_DeliveryPCS].Visible = false;
                }
                else if(cbEditInPcsUnit.Checked)
                {
                    headerName = header_DeliveryPCS;
                    dgv.Columns[header_DeliveryPCS].Visible = true;
                    dgv.Columns[header_DeliveryBAG].Visible = false;
                }

                if (dgv.DataSource != null)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.ReadOnly = false;
                    dgv.Columns[header_DeliveryQTY].Visible = false;
                    dgv.Columns[headerName].DefaultCellStyle.BackColor = SystemColors.Info;
                }
                else
                {
                    MessageBox.Show("No data.");
                    cbEditInBagUnit.Checked = false;
                    cbEditInPcsUnit.Checked = false;
                    dgv.Columns[header_DeliveryBAG].Visible = false;
                    dgv.Columns[header_DeliveryPCS].Visible = false;
                    dgv.Columns[header_DeliveryQTY].Visible = true;
                    dgv.Columns[header_DeliveryQTY].DefaultCellStyle.BackColor = Color.White;
                }


            }
            else
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ReadOnly = true;

                if (dgv.DataSource != null)
                {
                    dgv.Columns[header_DeliveryBAG].Visible = false;
                    dgv.Columns[header_DeliveryPCS].Visible = false;
                    dgv.Columns[header_DeliveryQTY].Visible = true;
                    dgv.Columns[header_DeliveryQTY].DefaultCellStyle.BackColor = Color.White;
                }

            }
        }

        private void SetCombinedCode()
        {
            DataTable dt = (DataTable)dgvDOList.DataSource;

            string combinedCode = null;
            foreach(DataRow row in dt.Rows)
            {
                bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                if(selected)
                {
                    combinedCode += row[header_PONoString].ToString();
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                if (selected)
                {
                    if(selectedDO == 1)
                    {
                        row[header_CombinedCode] = DBNull.Value;
                    }
                    else
                    {
                        row[header_CombinedCode] = combinedCode;
                    }
                    
                }
            }
        }

        private void dgvDOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvDOList;

            if (rowIndex >= 0)
            {
                if (addingDOStep2)
                {
                    DataTable dt = (DataTable)dgvDOList.DataSource;

                    if (dt.Columns.Contains(header_Selected))
                    {
                        bool selected = bool.TryParse(dgv.Rows[rowIndex].Cells[header_Selected].Value.ToString(), out selected) ? selected : false;

                        string customerID = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

                        if (!selected && IfSameCustomerSelected(customerID))
                        {
                            dgv.Rows[rowIndex].Cells[header_Selected].Value = true;

                            //dgv.Rows[rowIndex].Cells[header_DOCombined].Value = true;
                            selectedDO++;

                            if(selectedDO >= 1)
                            {
                                btnAddNewPO.Enabled = true;
                            }
                            else
                            {
                                btnAddNewPO.Enabled = false;
                            }
                        }
                        else if(selected)
                        {
                            dgv.Rows[rowIndex].Cells[header_Selected].Value = false;
                            //dgv.Rows[rowIndex].Cells[header_DOCombined].Value = false;
                            selectedDO--;

                            if(selectedDO < 0)
                            {
                                selectedDO = 0;
                            }

                            if (selectedDO >= 2)
                            {
                                btnAddNewPO.Enabled = true;
                            }
                            else
                            {
                                btnAddNewPO.Enabled = false;

                            }
                            if (!ifDoSelected())
                            {
                                btnAddNewPO.Enabled = false;
                            }
                        }
                    }

                   
                }
            }
        }

        private bool IfSameCustomerSelected(string customerID)
        {
            bool result = true;

            DataTable dt = (DataTable)dgvDOList.DataSource;

            if (dt.Columns.Contains(header_Selected))
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                    if(selected)
                    {
                        string custID = row[header_CustomerCode].ToString();

                        if (custID != customerID)
                        {
                            MessageBox.Show("Only DO with same customer can be combine!");
                            return false;
                        }
                    }
                    
                }
            }

            return result;

        }

        private void dgvDOList_DataSourceChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvItemList_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        private void cbEditInPcsUnit_CheckedChanged(object sender, EventArgs e)
        {
            if(cbEditInPcsUnit.Checked)
            {
                if(cbEditInBagUnit.Checked)
                {
                    cbEditInBagUnit.Checked = false;
                }


            }
            DeliveryQtyEditMode();
        }

        private void cbEditInBagUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEditInBagUnit.Checked)
            {
                if (cbEditInPcsUnit.Checked)
                {
                    cbEditInPcsUnit.Checked = false;
                }
            }
            DeliveryQtyEditMode();
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void dgvItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
               
                DataGridView dgv = dgvItemList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int currentColumn = dgv.CurrentCell.ColumnIndex;
                int currentRow = dgv.CurrentCell.RowIndex;

                string headerName = header_DeliveryPCS;

                if(cbEditInBagUnit.Checked)
                {
                    headerName = header_DeliveryBAG;
                }

                if (dgv.Columns[currentColumn].Name == headerName && dgv.CurrentCell.Value != DBNull.Value) //Desired Column
                {

                    oldData = int.TryParse(dgv.CurrentCell.Value.ToString(), out oldData)? oldData : 0;

                    if (e.Control is System.Windows.Forms.TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {

                    if (e.Control is System.Windows.Forms.TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvItemList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvItemList;

            int currentColumn = dgv.CurrentCell.ColumnIndex;
            int currentRow = dgv.CurrentCell.RowIndex;

            int newData = int.TryParse(dgv.Rows[currentRow].Cells[currentColumn].Value.ToString(), out newData)? newData : 0;

            //get data
            if (oldData != newData)
            {
                int stdPacking = int.TryParse(dgv.Rows[currentRow].Cells[header_StdPacking].Value.ToString(), out stdPacking) ? stdPacking : 0;

                if(stdPacking > 0)
                {
                    int remainder = 1;
                    int pcs = 0;
                    int bag = 0;

                    if (cbEditInPcsUnit.Checked)
                    {
                        pcs = newData;
                        remainder = pcs % stdPacking;
                        bag = pcs / stdPacking;
                    }
                    else if(cbEditInBagUnit.Checked)
                    {
                        remainder = 0;
                        bag = newData;
                        pcs = bag * stdPacking;
                    }

                    if (remainder > 0)
                    {
                        //wrong std packing
                        //message box
                        MessageBox.Show("Delivery quantity does not match with standard packaging quantity, \nplease adjust.\n\nStandard Packaging: "+stdPacking+" /Bag");
                        dgv.Rows[currentRow].Cells[header_DeliveryPCS].Value = oldData;

                        adjustRow = currentRow;
                        adjustCol = currentColumn;

                        dgv.ClearSelection();
                        dgv.Rows[currentRow].Cells[currentColumn].Selected = true;

                        //dgv.CurrentCell = dgv.Rows[currentRow].Cells[currentColumn];
                        //dgv.BeginEdit(true);
                    }
                    else
                    {
                        //save
                        //change delivery qty data
                        dgv.Rows[currentRow].Cells[header_DeliveryQTY].Value = pcs + " (" + bag + "bags)";

                        dgv.Rows[currentRow].Cells[header_DeliveryPCS].Value = pcs;
                        dgv.Rows[currentRow].Cells[header_DeliveryBAG].Value = bag;

                        //change data mode
                        string dataMode = dgv.Rows[currentRow].Cells[header_DataMode].Value.ToString();

                        if(dataMode == text_ToEdit || dataMode == text_DB)
                        {
                            dgv.Rows[currentRow].Cells[header_DataMode].Value = text_ToUpdate;
                        }

                        UpdateBalance(currentRow);

                        string DONo = dgv.Rows[currentRow].Cells[header_DONo].Value.ToString();
                        string POCode = dgv.Rows[currentRow].Cells[header_POCode].Value.ToString();

                        DOItemStockChecking(DONo, POCode);
                    }

                }
                
            }
        }

        private void UpdateBalance(int currentRow)
        {
            DataGridView dgv = dgvItemList;

            int balance = int.TryParse(dgv.Rows[currentRow].Cells[header_Balance].Value.ToString(), out balance) ? balance : 0;
            int stdPacking = int.TryParse(dgv.Rows[currentRow].Cells[header_StdPacking].Value.ToString(), out stdPacking) ? stdPacking : 0;

            string type = dgv.Rows[currentRow].Cells[header_Type].Value.ToString();
            string size = dgv.Rows[currentRow].Cells[header_Size].Value.ToString();

            if (cbEditInPcsUnit.Checked)
            {
                balance += oldData;
            }
            else if (cbEditInBagUnit.Checked)
            {
                balance += oldData * stdPacking;
            }

            for (int i = currentRow; i < dgv.Rows.Count; i++)
            {
                string loopType = dgv.Rows[i].Cells[header_Type].Value.ToString();
                string loopSize = dgv.Rows[i].Cells[header_Size].Value.ToString();
                
                if (loopType == type && loopSize == size)
                {
                    int pcs = int.TryParse(dgv.Rows[i].Cells[header_DeliveryPCS].Value.ToString(), out pcs) ? pcs : 0;

                    balance -= pcs;

                    dgv.Rows[i].Cells[header_Balance].Value = balance;
                    dgv.Rows[i].Cells[header_BalanceString].Value = balance + " (" + balance/stdPacking + "bags)";

                    if(balance < 0)
                    {
                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Black;
                    }

                }
                else
                {
                    break;
                }
                
            }
        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (adjustRow != -1 && adjustCol != -1)
            {
                
                int row = adjustRow;
                int col = adjustCol;
                adjustRow = -1;
                adjustCol = -1;
                dgvItemList.ClearSelection();
                dgvItemList.Rows[row].Cells[col].Selected = true;

                dgvItemList.CurrentCell = dgvItemList.Rows[row].Cells[col];
                //dgvItemList.BeginEdit(true);

            }
        }

        private bool InsertDODataToDB()
        {
            bool success = true;
            DataTable dt = (DataTable)dgvItemList.DataSource;

            cbEditInBagUnit.Checked = false;
            cbEditInPcsUnit.Checked = false;

            DateTime updatedDate = DateTime.Now;
            int userID = MainDashboard.USER_ID;

            foreach(DataRow row in dt.Rows)
            {
                int DONo = int.TryParse(row[header_DONo].ToString(), out DONo) ? DONo : -1;

                if(DONo != -1)
                {
                    uSpp.DO_no = DONo;
                    
                    uSpp.PO_tbl_code = Convert.ToInt32(row[header_POTblCode]);
                    uSpp.DO_to_delivery_qty = Convert.ToInt32(row[header_DeliveryPCS]);
                    uSpp.DO_date = updatedDate;
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = userID;

                    string dataMode = row[header_DataMode].ToString();

                   

                    if(dataMode == text_ToAdd)
                    {
                        success = dalSPP.InsertDO(uSpp);

                        if(success)
                        {
                            //update po and item table
                        }
                    }
                    else if (dataMode == text_ToUpdate)
                    {
                        uSpp.Table_Code = Convert.ToInt32(row[header_DOTblCode]);
                        success = dalSPP.DOUpdate(uSpp);
                    }
                   

                    if (!success)
                    {
                        MessageBox.Show("DO insert failed!");
                    }
                }
            }

            return success;
        }

        private void btnConfirmToAddDO_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to insert Delivery Order to database?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if(InsertDODataToDB())
                {
                    string message = "";
                    if(EditMode)
                    {
                        message = "DO updated!";
                    }
                    else
                    {
                        message = "DO added!";

                    }
                    MessageBox.Show(message);
                    POListMode();
                }
            }
            
        }

        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (addingDOStep2 || EditMode)
            //{
            //    bool twoEmptyInaRow = false;

            //    DataGridView dgv = dgvItemList;
            //    dgv.SuspendLayout();

            //    for (int i = 0; i < dgv.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < dgv.Columns.Count; j++)
            //        {
            //            string colName = dgv.Columns[j].Name;

            //            if (dgv.Columns[j].Name == header_Balance)
            //            {
            //                if (dgv.Rows[i].Cells[j].Value == DBNull.Value)
            //                {
            //                    if (twoEmptyInaRow)
            //                    {
            //                        dgv.Rows[i].Height = 3;
            //                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Black;
            //                        dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.Black;
            //                    }
            //                    else
            //                    {
            //                        dgv.Rows[i].Height = 25;
            //                        twoEmptyInaRow = true;
            //                    }


            //                }
            //                else
            //                {
            //                    twoEmptyInaRow = false;
            //                    dgv.Rows[i].Height = 50;

            //                    int bal = int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out bal) ? bal : 0;

            //                    if (bal < 0)
            //                    {
            //                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Red;
            //                    }
            //                    else
            //                    {
            //                        dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Black;
            //                    }
            //                }

            //            }

            //            else if (dgv.Columns[j].Name == header_DataMode)
            //            {
            //                if (dgv.Rows[i].Cells[j].Value != DBNull.Value)
            //                {
            //                    string text = dgv.Rows[i].Cells[j].Value.ToString();
            //                    if (dgv.Rows[i].Cells[j].Value.ToString() != text_DB)
            //                    {
            //                        dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.FromArgb(253, 203, 110);
            //                    }
            //                    else
            //                    {
            //                        dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.White;
            //                    }
            //                }
            //                else
            //                {
            //                    dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.White;
            //                }

            //            }
            //        }
            //    }

            //    dgv.ResumeLayout();
            //}
        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (addingDOStep2 || EditMode)
            {
                bool twoEmptyInaRow = false;

                DataGridView dgv = dgvItemList;
                dgv.SuspendLayout();

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        if (dgv.Columns[j].Name == header_Balance)
                        {
                            if (dgv.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                if (twoEmptyInaRow)
                                {
                                    dgv.Rows[i].Height = 3;
                                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                                }
                                else
                                {
                                    dgv.Rows[i].Height = 25;
                                    twoEmptyInaRow = true;
                                }


                            }
                            else
                            {
                                twoEmptyInaRow = false;
                                dgv.Rows[i].Height = 50;

                                int bal = int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out bal) ? bal : 0;

                                if (bal < 0)
                                {
                                    dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Red;
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[header_BalanceString].Style.ForeColor = Color.Black;
                                }
                            }

                        }
                        else if (dgv.Columns[j].Name == header_DataMode)
                        {
                            if (dgv.Rows[i].Cells[j].Value != DBNull.Value)
                            {
                                string text = dgv.Rows[i].Cells[j].Value.ToString();
                                if (dgv.Rows[i].Cells[j].Value.ToString() != text_DB)
                                {
                                    dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.FromArgb(253, 203, 110);
                                }
                            }
                            else
                            {
                                //dgv.Rows[i].Cells[header_DONoString].Style.BackColor = Color.White;
                            }
                        }
                    }
                }

                dgv.ResumeLayout();
            }
        }

        private void dgvDOList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = dgvDOList;
            dgv.SuspendLayout();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Columns[j].Name == header_StockCheck)
                    {
                        if (dgv.Rows[i].Cells[j].Value.ToString() == text_AvailableStock)
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.Red;

                        }
                    }
                }
            }

            dgv.ResumeLayout();

            dgv.ClearSelection();
        }
    }
}
