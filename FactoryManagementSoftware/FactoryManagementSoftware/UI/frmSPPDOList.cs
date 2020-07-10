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
using FactoryManagementSoftware.Properties;


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
        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();


        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string text_CompleteDO = "Complete D/O";
        readonly string text_InCompleteDO = "D/O Incomplete";
        readonly string text_UndoRemove = "Undo Remove";
        readonly string text_ChangeDONumber = "Change D/O Number";
        readonly string text_SelectDO = "SELECT D/O";
        readonly string text_Export= "Export >>";
        readonly string text_CancelSelectingMode = "CANCEL";
        readonly string text_RemoveDO = "REMOVE D/O";
        readonly string text_Excel = "EXCEL";
        readonly string text_DOItemList = "D/O ITEM LIST";

        readonly string header_POCode = "P/O CODE";
        readonly string header_PONo = "P/O NO";
        readonly string header_PODate = "P/O RECEIVED DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_DeliveredDate = "DELIVERED";
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
        readonly string header_DONo = "D/O #";
        readonly string header_DONoString = "D/O NO";
        readonly string header_DataType = "DATA TYPE";

        readonly string header_POTblCode = "P/O TABLE CODE";
        readonly string header_DOTblCode = "D/O TABLE CODE";
        readonly string header_CombinedCode = "COMBINED CODE";
        readonly string header_Stock = "STOCK QTY";
        readonly string header_StockString = "STOCK";
        readonly string header_DeliveryPCS = "DELIVERY PCS";
        readonly string header_DeliveryBAG = "DELIVERY BAG";
        readonly string header_DeliveryQTY = "DELIVERY QTY";
        readonly string header_Balance = "BAL No";
        readonly string header_StdPacking = "StdPacking";
        readonly string header_BalanceString = "BAL.";
        readonly string header_TrfTableCode = "TRANSFER ID";
        readonly string headerIndex = "#";
        readonly string headerPlanID = "PLAN ID";
        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerItem = "PLAN FOR";

        readonly string headerCollectBag = "COLLECT BAG";
        readonly string headerDeliverBag = "DELIVER BAG";
        readonly string headerFrom = "FROM";
        readonly string headerTo = "TO";
        readonly string headerReceivedBy = "RECEIVED BY";
        readonly string headerCheck = "CHECK";
        readonly string headerQty = "QTY (KG/PIECE)";

        readonly string DataType_Removed = "REMOVED";
        readonly string DataType_InProgress = "IN PROGRESS";
        readonly string DataType_Delivered = "DELIVERED";

        private DataTable dt_DOList;
        private DataTable dt_DOItemList_1;

        //private bool addingDOMode = false;
        //private bool ableToNextStep = false;
        private bool DOSelectingMode = false;
        //private bool POMode = true;
        //private bool addingDOStep2 = false;
        //private bool combineDO = false;
        //private bool combiningDO = false;

        //private int selectedDO = 0;
        //private int oldData = 0;
        //private int adjustRow = -1;
        //private int adjustCol = -1;
        #endregion

        #region data table 

        private DataTable NewDeliverTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(headerTo, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerPlanID, typeof(int));
            dt.Columns.Add(headerItem, typeof(string));
            dt.Columns.Add(headerQty, typeof(float));
            dt.Columns.Add(headerDeliverBag, typeof(string));
            dt.Columns.Add(headerReceivedBy, typeof(string));
            dt.Columns.Add(headerCheck, typeof(bool));

            return dt;

        }

        private DataTable NewCompleteTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DOTblCode, typeof(string));
            dt.Columns.Add(header_DONo, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveryPCS, typeof(string));
            dt.Columns.Add(header_DeliveryBAG, typeof(string));

            return dt;

        }
       
        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DataType, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));

            if(DOSelectingMode)
            {
                dt.Columns.Add(header_Selected, typeof(bool));
            }
            return dt;
        }

        private DataTable NewDOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(int));
            dt.Columns.Add(header_DOTblCode, typeof(int));
            dt.Columns.Add(header_TrfTableCode, typeof(int));
            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));

            dt.Columns.Add(header_ItemCode, typeof(string));

            dt.Columns.Add(header_DeliveryPCS, typeof(int));
            dt.Columns.Add(header_DeliveryBAG, typeof(int));
            dt.Columns.Add(header_DeliveryQTY, typeof(string));

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
                dgv.Columns[header_DataType].Visible = false;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
            }
            else if(dgv == dgvItemList)
            {
                dgv.Columns[header_DeliveryPCS].Visible = false;
                dgv.Columns[header_DeliveryBAG].Visible = false;
                dgv.Columns[header_StdPacking].Visible = false;
                dgv.Columns[header_POTblCode].Visible = false;
                dgv.Columns[header_DOTblCode].Visible = false;

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
                string dataType = dgv.Rows[rowIndex].Cells[header_DataType].Value.ToString();

                if(dataType == DataType_InProgress)
                    btnRemove.Visible = true;
                else
                    btnRemove.Visible = false;

                string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
                ShowDOItem(code);

                if(DOSelectingMode)
                {
                    bool selected = bool.TryParse(dgv.Rows[rowIndex].Cells[header_Selected].Value.ToString(), out selected) ? selected : false;

                    if (!selected)
                    {
                        dgv.Rows[rowIndex].Cells[header_Selected].Value = true;

                        btnExcel.Text = text_Export;
                        btnExcel.Enabled = true;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[header_Selected].Value = false;

                        if (!ifDOSelected())
                        {
                            btnExcel.Text = text_SelectDO;
                            btnExcel.Enabled = false;
                        }
                        else
                        {
                            btnExcel.Text = text_Export;
                            btnExcel.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (dataType == DataType_InProgress)
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;
                }
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

        private bool ifDOSelected()
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

        private void DOSelectingUI()
        {
            dgvItemList.DataSource = null;

            DOSelectingMode = true;

            btnAddNewDO.Visible = false;
            btnEdit.Visible = false;
            btnRemove.Text = text_CancelSelectingMode;
            btnRemove.Visible = true;

            DataTable dt = (DataTable)dgvDOList.DataSource;

            if (!dt.Columns.Contains(header_Selected))
            {
                DataColumn dc = new DataColumn(header_Selected, typeof(bool));

                dt.Columns.Add(dc);
                btnExcel.Enabled = false;
                btnExcel.Text = text_SelectDO;
            }
            else if (ifDOSelected())
            {
                btnExcel.Enabled = true;
                btnExcel.Text = text_Export;
            }
            else
            {
                btnExcel.Enabled = false;
                btnExcel.Text = text_SelectDO;
            }

            //dgvDOList.ClearSelection();
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

            locationTable.DefaultView.Sort = dalSPP.FullName + " ASC";


            cmbCustomer.DataSource = locationTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;
        }

        private void frmSPPDOList_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();
            LoadDOList();
            //int id =  dalTrfHist.GetLastInsertedID();
           
            //MessageBox.Show(id.ToString());
        }

        private void LoadDOList()
        {
            btnEdit.Visible = false;
            btnRemove.Visible = false;
            dgvItemList.DataSource = null;

            //dt_DOList = dalSPP.DOSelect();
            dt_DOList = dalSPP.DOWithInfoSelect();

            DataTable dt = NewDOTable();
            DataRow dt_row;

            int preDOCode = -999999;

             foreach (DataRow row in dt_DOList.Rows)
            {
                int doCode = int.TryParse(row[dalSPP.DONo].ToString(), out doCode) ? doCode : -999999;

                if ((preDOCode == -999999 || preDOCode != doCode) && row[dalSPP.DONo] != DBNull.Value)
                {
                    int deliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                    if(deliveryQty > 0)
                    {
                        
                        bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                        bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;
                        string trfID = row[dalSPP.TrfTableCode].ToString();

                        bool dataMatched = true;
                        string dataType = "";
                        #region DO TYPE

                        if (!isDelivered && cbInProgress.Checked)
                        {
                            dataMatched = dataMatched && true;
                            dataType = DataType_InProgress;
                        }
                        else if (isDelivered && cbCompleted.Checked)
                        {
                            dataMatched = dataMatched && true;
                            dataType = DataType_Delivered;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        if (isRemoved && cbRemoved.Checked)
                        {
                            dataMatched = true;
                            dataType = DataType_Removed;
                        }
                        else if (isRemoved)
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
                            preDOCode = doCode;
                            dt_row = dt.NewRow();

                            dt_row[header_DataType] = dataType;
                            dt_row[header_DONo] = doCode;
                            dt_row[header_DONoString] = doCode.ToString("D6");
                            dt_row[header_PONo] = row[dalSPP.PONo];
                            dt_row[header_POCode] = row[dalSPP.POCode];
                            dt_row[header_PODate] = Convert.ToDateTime(row[dalSPP.PODate]).Date;
                            dt_row[header_Customer] = row[dalSPP.ShortName];
                            dt_row[header_CustomerCode] = row[dalSPP.CustomerTableCode];

                            if (isDelivered && !string.IsNullOrEmpty(trfID))
                            {
                                dt_row[header_DeliveredDate] = tool.GetTransferDate(trfID);
                            }

                            dt.Rows.Add(dt_row);
                        }
                    }
                    

                }

            }

            
            dgvDOList.DataSource = dt;
            DgvUIEdit(dgvDOList);
            dgvDOList.ClearSelection();
            lblSubList.Text = text_DOItemList;
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
            int totalBag = 0;

            foreach (DataRow row in dt.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ?isRemoved : false;

                if (doCode == row[dalSPP.DONo].ToString() && row[dalSPP.DONo] != DBNull.Value && !isRemoved)
                {
                    int deliveryQty = row[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.ToDeliveryQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if (!string.IsNullOrEmpty(type) &&  (deliveryQty > 0))
                    {
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

                        int DOTableCode = int.TryParse(row[dalSPP.TableCode].ToString(), out DOTableCode) ? DOTableCode : -1;
                        int POTableCode = int.TryParse(row[dalSPP.POTableCode].ToString(), out POTableCode) ? POTableCode : -1;
                        int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                        dt_Row[header_POTblCode] = POTableCode;
                        dt_Row[header_DOTblCode] = DOTableCode;

                        if(TrfTableCode != -1)
                        {
                            dt_Row[header_TrfTableCode] = TrfTableCode;
                        }
                        

                        if (deliveryQty > 0)
                        {
                            totalBag += bag;
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
                }
            }

            lblSubList.Text = text_DOItemList + " (Total Bags: " + totalBag + ")";
            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvItemList.DataSource = dt_DOItemList;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();
        }

        private void dgvDOList_SelectionChanged(object sender, EventArgs e)
        {
            

            DataGridView dgv = dgvDOList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0 || dgv.CurrentRow == null)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
                string dataType = dgv.Rows[rowIndex].Cells[header_DataType].Value.ToString();

                if (!DOSelectingMode)
                {
                    if (dataType == DataType_InProgress)
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;
                }
                

               

                if (dataType == DataType_InProgress)
                    btnRemove.Visible = true;
                else
                    btnRemove.Visible = false;
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

                if(!DOSelectingMode)
                btnRemove.Visible = false;
            }
        }

        private void RemoveDO(int DONo, string customer)
        {
            uSpp.IsRemoved = true;
            uSpp.DO_no = DONo;

            int userID = MainDashboard.USER_ID;
            DateTime dateNow = DateTime.Now;
            uSpp.Updated_Date = dateNow;
            uSpp.Updated_By = userID;

            if (dalSPP.DORemove(uSpp))
            {
                MessageBox.Show("DO Removed!");
                tool.historyRecord(text.DO_Removed, text.GetDONumberAndCustomer(DONo, customer), dateNow, userID, dalSPP.DOTableName, DONo);
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

            if (rowIndex >= 0)
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

                LoadDOList();
            }

        }

        private void DOListUI()
        {
            DataGridView dgv = dgvDOList;
            DataTable dt = (DataTable)dgv.DataSource;

            if (DOSelectingMode)
            {
                if (dt != null && dt.Columns.Contains(header_Selected))
                {
                    dt.Columns.Remove(header_Selected);
                }

                dgvItemList.DataSource = null;

                DOSelectingMode = true;

                btnAddNewDO.Visible = true;
                btnEdit.Visible = false;
                btnRemove.Text = text_RemoveDO;
                btnRemove.Visible = false;
                btnExcel.Enabled = true;
                btnExcel.Text = text_Excel;
               

                dgvDOList.ClearSelection();

                DOSelectingMode = false;
            }
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvDOList;

            if(DOSelectingMode)
            {
                DOListUI();
            }
            else
            {
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    int DONo = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString());
                    string Customer = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to REMOVE DO: " + DONo.ToString("D6") + " ?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        RemoveDO(DONo, Customer);

                        LoadDOList();
                    }
                }
            }
          
        }

        private void btnAddNewDO_Click(object sender, EventArgs e)
        {
            frmSPPPOList frm = new frmSPPPOList(true)
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
            LoadDOList();
        }

        private void dgvDOList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvDOList;


                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();


                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.Focus();
                    int rowIndex = dgv.CurrentCell.RowIndex;

                    string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
                    ShowDOItem(code);

                    string dataType = dgv.Rows[rowIndex].Cells[header_DataType].Value.ToString();

                    string showingText = "";

                    bool ableToChangeDONo = false;
                    if(dataType == DataType_Removed)
                    {
                        showingText = text_UndoRemove;
                    }
                    else if (dataType == DataType_Delivered)
                    {
                        showingText = text_InCompleteDO;
                        ableToChangeDONo = true;
                    }
                    else if (dataType == DataType_InProgress)
                    {
                        showingText = text_CompleteDO;
                        ableToChangeDONo = true;
                    }

                    my_menu.Items.Add(showingText).Name = showingText;

                    if(ableToChangeDONo)
                    {
                        my_menu.Items.Add(text_ChangeDONumber).Name = text_ChangeDONumber;
                    }

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(DOList_ItemClicked);

                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void DOList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvDOList;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();
                    if (ClickedItem.Equals(text_CompleteDO))
                    {
                        CompleteDO(rowIndex);
                    }
                    else if (ClickedItem.Equals(text_InCompleteDO))
                    {
                        IncompleteDO(rowIndex);
                        //MessageBox.Show("incomplete do");
                    }
                    else if (ClickedItem.Equals(text_UndoRemove))
                    {
                        DOUndoRemove(rowIndex);
                        //MessageBox.Show("undo remove");
                    }
                    else if (ClickedItem.Equals(text_ChangeDONumber))
                    {
                        ChangeDONumber(rowIndex);
                        //MessageBox.Show("Change D/O Number");
                    }
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void CompleteDO(int rowIndex)
        {
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;
            DataGridView dgv = dgvDOList;


            string doCode = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            DataTable dt = NewCompleteTable();
            DataRow dt_Row;


            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string itemCode = row[header_ItemCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    dt_Row = dt.NewRow();

                    dt_Row[header_DOTblCode] = doTableCode;
                    dt_Row[header_DONo] = doCode;
                    dt_Row[header_Customer] = customerShortName;
                    dt_Row[header_CustomerCode] = customerCode;

                    dt_Row[header_ItemCode] = itemCode;
                    dt_Row[header_DeliveryPCS] = PcsQty;
                    dt_Row[header_DeliveryBAG] = BagQty;

                    dt.Rows.Add(dt_Row);
                }


            }


            frmDeliveryDate frm = new frmDeliveryDate(dt)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            frm.ShowDialog();//Item Edit

            if (frmDeliveryDate.transferred)
            {
                MessageBox.Show("Transfer success!");

                LoadDOList();
                tool.historyRecord(text.DO_Delivered, text.GetDONumberAndCustomer(Convert.ToInt32(doCode), customerShortName), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(doCode));
            }
            else
            {
                MessageBox.Show("Transfer cancelled!");
            }
        }

        private bool UpdateDOandPO(DataTable dt_DO, DataTable dt_PO, string trfItemCode, int trfID, string trfQty)
        {
            bool success = false;
            //DateTime updatedDate = DateTime.Now;
            //int userID = MainDashboard.USER_ID;

            //uSpp.Updated_Date = updatedDate;
            //uSpp.Updated_By = userID;

            //if (!string.IsNullOrEmpty(trfItemCode) && dt_DO.Rows.Count > 0)
            //{
            //    //GET D/O TABLE CODE BY ITEM CODE SEARCHING
            //    foreach (DataRow row in dt_DOItem.Rows)
            //    {
            //        //GET TRANSFER QTY
            //        string itemCode = row[header_ItemCode].ToString();
            //        string pcsQty = row[header_DeliveryPCS].ToString();

            //        if (trfItemCode == itemCode && pcsQty == trfQty)
            //        {
            //            string DOTableCode = row[header_DOTblCode].ToString();

            //            //update D/O
            //            uData.Table_Code = Convert.ToInt32(DOTableCode);
            //            uData.IsDelivered = true;
            //            uData.Trf_tbl_code = trfID;

            //            success = dalData.DODelivered(uData);

            //            if (!success)
            //            {
            //                MessageBox.Show("Failed to update D/O\nTransfer ID: " + trfID);
            //                return success;
            //            }

            //            //SEARCHING AT D/O DATABASE TABLE
            //            foreach (DataRow rowDODatabase in dt_DO.Rows)
            //            {
            //                string DB_DOTableCode = rowDODatabase[dalData.TableCode].ToString();

            //                if (DB_DOTableCode == DOTableCode)
            //                {
            //                    //GET P/O TABLE CODE
            //                    string POTableCode = rowDODatabase[dalData.POTableCode].ToString();

            //                    //get P/O delivered qty
            //                    foreach (DataRow rowPODatabase in dt_PO.Rows)
            //                    {
            //                        string DB_POTableCode = rowPODatabase[dalData.TableCode].ToString();

            //                        if (POTableCode == DB_POTableCode)
            //                        {
            //                            int deliveredQty = int.TryParse(rowPODatabase[dalData.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

            //                            deliveredQty += int.TryParse(trfQty, out int i) ? i : 0;

            //                            //update P/O

            //                            uData.Table_Code = Convert.ToInt32(DB_POTableCode);
            //                            uData.Delivered_qty = deliveredQty;

            //                            success = dalData.PODeliveredDataUpdate(uData);

            //                            if (!success)
            //                            {
            //                                MessageBox.Show("Failed to update P/O\nP/O Table Code: " + DB_POTableCode);

            //                            }

            //                            return success;
            //                        }
            //                    }

            //                }
            //            }

            //        }
            //    }
            //}


            return success;
        }

        private void CompleteDOWithoutTransfer(int rowIndex)
        {
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;
            DataGridView dgv = dgvDOList;

            string doCode = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            DataTable dt_DODataBase = dalSPP.DOSelect();
            DataTable dt_PODataBase = dalSPP.POSelect();

            bool result = true;
            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string itemCode = row[header_ItemCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

               if( !UpdateDOandPO(dt_DODataBase, dt_PODataBase, itemCode,-1, PcsQty))
                {
                    result = false;
                    MessageBox.Show("Failed to transfer!");
                }
            }

            if (result)
            {
                MessageBox.Show("transfer success");

                LoadDOList();
                tool.historyRecord(text.DO_Delivered, text.GetDONumberAndCustomer(Convert.ToInt32(doCode), customerShortName), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(doCode));
            }
        }

        private int GetPODeliveredQty(DataTable dt_PO, string poTableCode)
        {
            int deliveredQty = 0;

            foreach(DataRow row in dt_PO.Rows)
            {
                if(poTableCode == row[dalSPP.TableCode].ToString())
                {
                    deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                }
            }

            return deliveredQty;
        }

        private void IncompleteDO(int rowIndex)
        {
            bool success = true;
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;
            DataGridView dgv = dgvDOList;

            string doCode = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string poNoString = dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            //get po data from DB by po no string
            DataTable dt_POData = dalSPP.POSelect(poNoString);
            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;

            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string poTableCode = row[header_POTblCode].ToString();
                string trfTableCode = row[header_TrfTableCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();

                string itemCode = row[header_ItemCode].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = updatedBy;

                    #region  update isDelivered to false to DB by do table code

                    uSpp.Table_Code = Convert.ToInt32(doTableCode);
                    uSpp.IsDelivered = false;
                    uSpp.Trf_tbl_code = Convert.ToInt32(trfTableCode);

                    success = dalSPP.DODelivered(uSpp);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update D/O\nTransfer ID: " + trfTableCode);
                    }
                    #endregion

                    #region update delivered qty to DB by po table code

                    //get delivered qty by po table code
                    int deliveredQty = GetPODeliveredQty(dt_POData, poTableCode);

                    //dedute delivered qty by to delivery qty
                    int toDeliveryPcs = int.TryParse(PcsQty, out toDeliveryPcs) ? toDeliveryPcs : 0;

                    deliveredQty -= toDeliveryPcs;
                    uSpp.Table_Code = Convert.ToInt32(poTableCode);
                    uSpp.Delivered_qty = deliveredQty;
              

                    success = dalSPP.PODeliveredDataUpdate(uSpp);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update delivered qty!");

                    }

                    #endregion

                    tool.UndoTransferRecord(Convert.ToInt32(trfTableCode));

                }


            }

            //message
            if (success)
            {
                MessageBox.Show("D/O: " + Convert.ToInt32(doCode).ToString("D6") + " Incomplete Successful!");
                dgv.Rows[rowIndex].Cells[header_DataType].Value = DataType_InProgress;
            }
          

            //change row color
            ColorDONoByStatus();

            //record history
            tool.historyRecord(text.DO_Incomplete, text.GetDONumberAndCustomer(Convert.ToInt32(doCode), customerShortName), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(doCode));


            //LoadDOList();
        }

        private void DOUndoRemove(int rowIndex)
        {
            bool success = true;
            
            DataGridView dgv = dgvDOList;

            string doNo = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string poNoString = dgv.Rows[rowIndex].Cells[header_PONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();
            string customerCode = dgv.Rows[rowIndex].Cells[header_CustomerCode].Value.ToString();

            DataTable dt_Item = (DataTable)dgvItemList.DataSource;

            string newDONo = doNo;
            if(tool.IfDONoExist(doNo))
            {
                newDONo = tool.GetNewDONo().ToString();
            }


            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;


            foreach (DataRow row in dt_Item.Rows)
            {
                string doTableCode = row[header_DOTblCode].ToString();
                string poTableCode = row[header_POTblCode].ToString();
                string trfTableCode = row[header_TrfTableCode].ToString();
                string PcsQty = row[header_DeliveryPCS].ToString();

                string itemCode = row[header_ItemCode].ToString();
                string BagQty = row[header_DeliveryBAG].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    uSpp.Updated_Date = updatedDate;
                    uSpp.Updated_By = updatedBy;

                    #region  update isRemoved to false and DONo to DB by do table code

                    uSpp.Table_Code = Convert.ToInt32(doTableCode);
                    uSpp.IsRemoved = false;
                    uSpp.DO_no = Convert.ToInt32(newDONo);

                    success = dalSPP.DORemovedStatusAndDoNoUpdate(uSpp);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update D/O's number or isRemoved status!");
                    }
                    #endregion
                }


            }

            //message
            if(success)
            {
                MessageBox.Show("D/O: " + Convert.ToInt32(newDONo).ToString("D6") + " remove undo successful!");
                dgv.Rows[rowIndex].Cells[header_DataType].Value = DataType_InProgress;
            }

            //change row color
            ColorDONoByStatus();

            //record history
            tool.historyRecord(text.DO_UndoRemove, text.GetDONumberAndCustomer(Convert.ToInt32(newDONo), customerShortName), updatedDate, updatedBy, dalSPP.DOTableName, Convert.ToInt32(newDONo));
        }

        private void ChangeDONumber(int rowIndex)
        {
            bool success = true;
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;
            DataGridView dgv = dgvDOList;
            DateTime updatedDate = DateTime.Now;
            int updatedBy = MainDashboard.USER_ID;

            uSpp.Updated_Date = updatedDate;
            uSpp.Updated_By = updatedBy;

            string oldDONo = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();
            string customerShortName = dgv.Rows[rowIndex].Cells[header_Customer].Value.ToString();

            frmDONumberChange frm = new frmDONumberChange(oldDONo)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            string newDONo = frmDONumberChange.NewDONumber;

            if(frmDONumberChange.numberSelected)
            {
                #region Update Removed D/O

                //To-Do: 
                //check if removed do have same newDONo
                if (tool.IfDONoExistInRemovedDO(newDONo))
                {
                    //if have, generate new do no for removed do
                    int newRemovedDONo = tool.GetNewRemovedDONo();

                    //change removed do old no to new do no in do table
                    uSpp.DO_no = Convert.ToInt32(newRemovedDONo);
                    success = dalSPP.DONoChange(uSpp, newDONo);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update new Removed D/O's number!");
                    }

                    //change removed do old no to new do no in history table
                    uHistory.page_name = dalSPP.DOTableName;
                    uHistory.data_id = newRemovedDONo;

                    success = dalHistory.ChangeDataID(uHistory, newDONo);

                    //removed d/o change number
                    tool.historyRecord(text.DO_ChangeDONumber, text.ChangeDONumber(Convert.ToInt32(newRemovedDONo), "", newDONo, newRemovedDONo.ToString()), updatedDate, updatedBy, dalSPP.DOTableName, Convert.ToInt32(newRemovedDONo));
                }

                #endregion

                #region change to new D/O no.

                if (!string.IsNullOrEmpty(newDONo) && newDONo != "")
                {
                    uSpp.DO_no = Convert.ToInt32(newDONo);

                    success = dalSPP.DONoChange(uSpp, oldDONo);

                    if (!success)
                    {
                        MessageBox.Show("Failed to update new D/O's number!");
                    }

                    LoadDOList();

                    //record history
                    tool.historyRecord(text.DO_ChangeDONumber, text.ChangeDONumber(Convert.ToInt32(newDONo), customerShortName, oldDONo, newDONo), updatedDate, updatedBy, dalSPP.DOTableName, Convert.ToInt32(newDONo));
                }
                else
                {
                    MessageBox.Show("Error! New DO No is null or invalid data.");
                }

                #endregion

                #region  change old data id to new data id in history record

                uHistory.page_name = dalSPP.DOTableName;
                uHistory.data_id = Convert.ToInt32(newDONo);

                success = dalHistory.ChangeDataID(uHistory, oldDONo);

                #endregion
            }
        }


        #endregion

        #region export to excel

        private string setFileName()
        {
            int count = 0;
            string firstDONumber = "DO", lastDONumber = DateTime.Now.ToShortDateString();

            DataGridView dgv = dgvDOList;
            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dt_DOList.Columns.Contains(header_Selected))
            {
                firstDONumber = "";
                lastDONumber = "";
                foreach (DataRow row in dt_DOList.Rows)
                {
                    bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                    if (selected)
                    {
                        string DONoString = row[header_DONoString].ToString();

                        if (count == 0)
                        {
                            firstDONumber = DONoString;
                        }
                        else
                        {
                            lastDONumber = DONoString;
                        }

                        count++;
                    }
                }
            }

           

            if(lastDONumber != "")
            {
                lastDONumber = "_" + lastDONumber;
            }

            return firstDONumber + lastDONumber + ".xls";
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if(btnExcel.Text == text_Export)
            {
                ExportToExcel();
            }
            else
            {
                DOSelectingUI();
            }
           
        }

        private void ExcelPageSetup(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;
            xlWorkSheet.PageSetup.FitToPagesWide = false;
            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 0.5 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";


        }

        private void InitialDOFormat(Worksheet xlWorkSheet)
        {
            #region Sheet Area

            string sheetWidth = "a1:x1";
            string letterHeadArea = "a1:a16";
            string pageString = "v1:v1";
            string pageNo = "w1:x1";

            string DOInfo = "a11:a16";
            string Divider = "a18:a18";
            string tableHeader = "a19:a20";
            string itemList = "a21:a36";
            string signingArea = "a37:a44";
            string wholeSheetArea = "a1:x44";
            string littleSpace_1 = "a10:x10";
            string littleSpace_2 = "a17:x17";
            string companyName_CN = "F1:S2";
            string companyName_EN = "F3:S4";
            string company_Registration = "F5:S5";
            string company_AddressAndContact = "F6:S8";
            string DONoArea = "u13:x13";
            string PONoArea = "u14:x14";
            string DODateArea = "u15:x15";
            string CustFullNameArea = "c11:M11";
            string AddressArea_1 = "c12:M12";
            string AddressArea_2 = "c13:M13";
            string postalAndCity = "c14:M14";
            string state = "c15:M15";
            string contact = "c16:M16";
           
            #endregion

            int itemRowOffset = 20;
            int maxRow = 16;
            int itemFontSize = 11;
            string descriptionColStart = "b";
            string descriptionColEnd = ":l";

            string qtyColStart = "n";
            string qtyColEnd = ":o";

            string pcsColStart = "p";
            string pcsColEnd = ":p";

            string remarkColStart = "t";
            string remarkColEnd = ":w";
            #region sheet size adjustment

            Range DOFormat = xlWorkSheet.get_Range(sheetWidth).Cells;
            DOFormat.ColumnWidth = 3;

            DOFormat = xlWorkSheet.get_Range(letterHeadArea).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(DOInfo).Cells;
            DOFormat.RowHeight = 18.5;

            DOFormat = xlWorkSheet.get_Range(littleSpace_1).Cells;
            DOFormat.RowHeight = 8;

            DOFormat = xlWorkSheet.get_Range(littleSpace_2).Cells;
            DOFormat.RowHeight = 4.2;

            DOFormat = xlWorkSheet.get_Range(Divider).Cells;
            DOFormat.RowHeight = 5.4;

            DOFormat = xlWorkSheet.get_Range(tableHeader).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(itemList).Cells;
            DOFormat.RowHeight = 24.6;

            DOFormat = xlWorkSheet.get_Range(signingArea).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 12;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;
            #endregion

            #region LOGO

            //string tempPath = Path.GetTempFileName();
            //Resources.safety_logo.Save(tempPath + "safety-logo.png");
            //string filePath = tempPath + "safety-logo.png";
            ////var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo.png";
            //xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 20, 88f, 49f);

            #endregion

            DOFormat = xlWorkSheet.get_Range(companyName_CN).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 22;
            DOFormat.Font.Name = text.Font_Type_KaiTi;
            DOFormat.Font.Color = Color.Red;

            DOFormat = xlWorkSheet.get_Range(pageString).Cells;
            DOFormat.Value = "PG.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(pageNo).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;

            DOFormat.Font.Name = "Cambria";
            DOFormat = xlWorkSheet.get_Range(companyName_EN).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_Registration).Cells;
            DOFormat.Merge();
           // DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10.5;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_AddressAndContact).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range("o10:x12").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY ORDER";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = "Arial Black";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o13:s13").Cells;
            DOFormat.Merge();
            DOFormat.Value = "No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o14:s14").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your Order No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o15:s15").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("t13:t13").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t14:t14").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t15:t15").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(DONoArea).Cells;
            DOFormat.Merge();
            //DOFormat.NumberFormat = 
            // DOFormat.Value = "14245";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(PONoArea).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "44946";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            //DO DATE//////////////////////////////////////////////////////////////////////////////////////////////////////
            DOFormat = xlWorkSheet.get_Range(DODateArea).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DODate.ToOADate();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("a11:b11").Cells;
            DOFormat.Merge();
            DOFormat.Value = "M/S:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range("a16:b16").Cells;
            DOFormat.Merge();
            DOFormat.Value = "TEL:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range(CustFullNameArea).Cells;
            DOFormat.Merge();
           // DOFormat.Value = "PERMABONN SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(AddressArea_1).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "NO.2, JALAN MERANTI JAYA";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(AddressArea_2).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "MERANTI JAYA INDUSTRIAL PARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(postalAndCity).Cells;
            DOFormat.Merge();
           // DOFormat.Value = "47120 PUCHONG";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(state).Cells;
            DOFormat.Merge();
           // DOFormat.Value = "SELANGOR";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(contact).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "03-87259657";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            Color color = Color.Black;
            DOFormat = xlWorkSheet.get_Range("a18:w18").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("b19:l20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("n19:q20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "QUANTITY";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("r19:w20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "REMARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("a20:w20").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

           

            for(int i = 1; i <= maxRow; i++)
            {
                string area = descriptionColStart + (itemRowOffset + i).ToString() + descriptionColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                // DOFormat.Value = "SBBEE032   25 MM EQUAL ELBOW";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = qtyColStart + (itemRowOffset + i).ToString() + qtyColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "1200";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = pcsColStart + (itemRowOffset + i).ToString() + pcsColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                //DOFormat.Value = "PCS";
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = remarkColStart + (itemRowOffset + i).ToString() + remarkColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "3 BAGS";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";
            }

            DOFormat = xlWorkSheet.get_Range("a36:w36").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a38:h38").Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("p38:x38").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Goods checked and received by";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("a44:c44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Issued By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("f44:h44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Checked By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("p44:w44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Customer's Chop & Signature";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, string value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
          
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, double value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, DateTime value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
        }

        private void AllInOneExcel()
        {
            DateTime DODate = frmExportSetting.DODate;
            bool openFile = frmExportSetting.openFileAfterExport;
            bool printFile = frmExportSetting.printFileAfterExport;
            bool printPreview = frmExportSetting.printPreview;
            string DODate_String = DODate.ToString("dd/MM/yyyy");

            #region export excel
            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                try
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    SaveFileDialog sfd = new SaveFileDialog();

                    string path = @"D:\StockAssistant\Document\SBB DO Report";
                    Directory.CreateDirectory(path);
                    sfd.InitialDirectory = path;

                    sfd.Filter = "Excel Documents (*.xls)|*.xls";
                    sfd.FileName = setFileName();

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        frmLoading.ShowLoadingScreen();
                        tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                        // Copy DataGridView results to clipboard
                        copyAlltoClipboard();
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        object misValue = Missing.Value;

                        Excel.Application xlexcel = new Excel.Application
                        {
                            PrintCommunication = false,
                            ScreenUpdating = false,
                            DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                        };

                        Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);


                        xlexcel.StandardFont = "Calibri";
                        xlexcel.StandardFontSize = 12;

                        xlexcel.Calculation = XlCalculation.xlCalculationManual;
                        xlexcel.PrintCommunication = false;

                        int sheetNo = 0;
                        int pageNo = 1;
                        int maxRow = 15;
                        int rowNo = 0;
                        int rowOffset = 20;

                        Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;
                        //xlWorkSheet.PageSetup.PrintArea
                        DataTable dt_PO = dalSPP.DOWithInfoSelect();
                        dt_PO.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
                        dt_PO = dt_PO.DefaultView.ToTable();

                        foreach (DataRow row in dt_DOList.Rows)
                        {
                            

                            bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                            if (selected)
                            {
                                string deliveredDate = row[header_DeliveredDate].ToString();

                                DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate; 
                                if (sheetNo > 0)
                                {
                                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                }

                                sheetNo++;

                                string customerName = row[header_Customer].ToString();
                                string POTableCode = row[header_POCode].ToString();
                                string PONo = row[header_PONo].ToString();
                                string DONo = row[header_DONo].ToString();
                                string DONoString = row[header_DONoString].ToString();

                                xlWorkSheet.Name = customerName + "_" + DONoString;

                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;
                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                InitialDOFormat(xlWorkSheet);



                                string DONoArea = "u13:x13";
                                string PONoArea = "u14:x14";
                                string DODateArea = "u15:x15";
                                string pageNoArea = "w1:x1";

                                InsertToSheet(xlWorkSheet, pageNoArea, pageNo + "/" + pageNo);
                                InsertToSheet(xlWorkSheet, DONoArea, DONoString);
                                InsertToSheet(xlWorkSheet, PONoArea, PONo);
                                InsertToSheet(xlWorkSheet, DODateArea, DODate.ToOADate());

                                string custFullName = null;
                                string shippingAddress_1 = null;
                                string shippingAddress_2 = null;
                                string shippingPostalCode = null;
                                string shippingCity = null;
                                string shippingState = null;
                                string Contact = null;

                                //string itemList = "a20:a36";

                                string descriptionRowStart = "b";
                                string descriptionRowEnd = ":l";

                                string qtyColStart = "n";
                                string qtyColEnd = ":o";

                                string pcsColStart = "p";
                                string pcsColEnd = ":p";

                                string remarkColStart = "t";
                                string remarkColEnd = ":w";

                                string CustFullNameArea = "c11:M11";
                                string AddressArea_1 = "c12:M12";
                                string AddressArea_2 = "c13:M13";
                                string postalAndCity = "c14:M14";
                                string state = "c15:M15";
                                string contact = "c16:M16";

                                foreach (DataRow row2 in dt_PO.Rows)
                                {
                                    if (DONo == row2[dalSPP.DONo].ToString() && row2[dalSPP.DONo] != DBNull.Value)
                                    {
                                        int deliveryQty = row2[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row2[dalSPP.ToDeliveryQty].ToString());

                                        custFullName = row2[dalSPP.FullName].ToString();
                                        shippingAddress_1 = row2[dalSPP.Address1].ToString();
                                        shippingAddress_2 = row2[dalSPP.Address2].ToString();
                                        shippingPostalCode = row2[dalSPP.AddressPostalCode].ToString();
                                        shippingCity = row2[dalSPP.AddressCity].ToString();
                                        shippingState = row2[dalSPP.AddressState].ToString();
                                        Contact = row2[dalSPP.Phone1].ToString();

                                        if (deliveryQty > 0)
                                        {
                                            string type = row2[dalSPP.TypeName].ToString();
                                            int stdPacking = int.TryParse(row2[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;
                                            int bag = deliveryQty / stdPacking;
                                            string size = row2[dalSPP.SizeNumerator].ToString();
                                            string unit = row2[dalSPP.SizeUnit].ToString().ToUpper();
                                            string itemCode = row2[dalSPP.ItemCode].ToString();
                                            string remark = bag + " BAG(S)";

                                            //check current row vs max row
                                            if (rowNo + 1 > maxRow)
                                            {
                                                rowNo = 1;
                                                pageNo++;
                                                //create new sheet
                                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                                if (pageNo > 1)
                                                    xlWorkSheet.Name = customerName + "_" + DONoString + "_" + pageNo;

                                                ExcelPageSetup(xlWorkSheet);
                                                InitialDOFormat(xlWorkSheet);
                                                InsertToSheet(xlWorkSheet, DONoArea, DONoString);
                                                InsertToSheet(xlWorkSheet, PONoArea, PONo);
                                                InsertToSheet(xlWorkSheet, pageNoArea, pageNo + "/" + pageNo);

                                                InsertToSheet(xlWorkSheet, DODateArea, DODate.ToOADate());
                                                Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString];

                                                InsertToSheet(previousSheet, pageNoArea, 1 + "/" + pageNo);
                                                InsertToSheet(previousSheet, CustFullNameArea, custFullName);
                                                InsertToSheet(previousSheet, AddressArea_1, shippingAddress_1);
                                                InsertToSheet(previousSheet, AddressArea_2, shippingAddress_2);
                                                InsertToSheet(previousSheet, postalAndCity, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, state, shippingState);
                                                InsertToSheet(previousSheet, contact, Contact);

                                                //change total page number in previous sheet(s)
                                                for (int i = 2; i < pageNo; i++)
                                                {
                                                    previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString + "_" + i];

                                                    InsertToSheet(previousSheet, pageNoArea, i + "/" + pageNo);
                                                    InsertToSheet(previousSheet, CustFullNameArea, custFullName);
                                                    InsertToSheet(previousSheet, AddressArea_1, shippingAddress_1);
                                                    InsertToSheet(previousSheet, AddressArea_2, shippingAddress_2);
                                                    InsertToSheet(previousSheet, postalAndCity, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, state, shippingState);
                                                    InsertToSheet(previousSheet, contact, Contact);

                                                }

                                                sheetNo++;

                                            }
                                            else
                                            {
                                                rowNo++;
                                            }
                                            //insert data
                                            string descriptionRow = descriptionRowStart + (rowOffset + rowNo).ToString() + descriptionRowEnd + (rowOffset + rowNo).ToString();

                                            string type_ShortName = text.EqualElbow_Short;

                                            if (type == text.Type_EqualSocket)
                                            {
                                                type_ShortName = text.EqualSocket_Short;
                                            }
                                            else if (type == text.Type_EqualTee)
                                            {
                                                type_ShortName = text.EqualTee_Short;
                                            }

                                            string newItemCode = text.SPP_BrandName + type_ShortName + size;
                                            InsertToSheet(xlWorkSheet, descriptionRow, newItemCode + "     " + size + " " + unit + " " + type);

                                            descriptionRow = qtyColStart + (rowOffset + rowNo).ToString() + qtyColEnd + (rowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, descriptionRow, deliveryQty);

                                            descriptionRow = pcsColStart + (rowOffset + rowNo).ToString() + pcsColEnd + (rowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, descriptionRow, "PCS");

                                            descriptionRow = remarkColStart + (rowOffset + rowNo).ToString() + remarkColEnd + (rowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, descriptionRow, remark);


                                        }

                                    }
                                }



                                if (string.IsNullOrEmpty(shippingAddress_1))
                                {
                                    AddressArea_2 = "c11:M11";
                                    postalAndCity = "c12:M12";
                                    state = "c13:M13";
                                }

                                if (string.IsNullOrEmpty(shippingAddress_2))
                                {
                                    postalAndCity = "c12:M12";
                                    state = "c13:M13";
                                }

                                InsertToSheet(xlWorkSheet, CustFullNameArea, custFullName);
                                InsertToSheet(xlWorkSheet, AddressArea_1, shippingAddress_1);
                                InsertToSheet(xlWorkSheet, AddressArea_2, shippingAddress_2);
                                InsertToSheet(xlWorkSheet, postalAndCity, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, state, shippingState);
                                InsertToSheet(xlWorkSheet, contact, Contact);


                                tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));

                                pageNo = 1;
                                rowNo = 0;


                            }
                        }

                        if (sheetNo > 0)
                        {
                            xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                           misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        }

                        // Clear Clipboard and DataGridView selection
                        Clipboard.Clear();
                        dgv.ClearSelection();

                        frmLoading.CloseForm();

                        // Open the newly saved excel file
                        if (File.Exists(sfd.FileName))
                        {

                            if (openFile)
                            {
                                Excel.Application excel = new Excel.Application();
                                excel.Visible = true;
                                excel.DisplayAlerts = false;
                                Workbook wb = excel.Workbooks.Open(sfd.FileName, ReadOnly: false, Notify: false);
                                excel.DisplayAlerts = true;

                                //System.Diagnostics.Process.Start(sfd.FileName);
                            }
                            else
                            {
                                MessageBox.Show("D/O export successful!");
                            }

                            if (printFile)
                            {
                                xlexcel.Visible = printPreview;
                                xlWorkBook.PrintOut(Preview: printPreview, Collate: true);
                                //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, printPreview, Type.Missing, Type.Missing, true, Type.Missing);
                            }

                        }

                        xlexcel.DisplayAlerts = true;

                        xlWorkBook.Close(true, misValue, misValue);
                        xlexcel.Quit();

                        releaseObject(xlWorkSheet);

                        releaseObject(xlWorkBook);
                        releaseObject(xlexcel);
                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type

                }
                catch (Exception ex)
                {
                    frmLoading.CloseForm();
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
            }
            #endregion
        }

        private void SeparateExcel()
        {
            DateTime DODate = frmExportSetting.DODate;
            bool openFile = frmExportSetting.openFileAfterExport;
            bool printFile = frmExportSetting.printFileAfterExport;
            bool printPreview = frmExportSetting.printPreview;
            string DODate_String = DODate.ToString("dd/MM/yyyy");

            #region export excel
            DataGridView dgv = dgvDOList;

            DataTable dt_DOList = (DataTable)dgv.DataSource;

            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dt_DOList.Columns.Contains(header_Selected))
            {
                try
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DataTable dt_PO = dalSPP.DOWithInfoSelect();
                    dt_PO.DefaultView.Sort = dalSPP.TypeName + " ASC," + dalSPP.SizeNumerator + " ASC";
                    dt_PO = dt_PO.DefaultView.ToTable();
                    string path = @"D:\StockAssistant\Document\SBB DO Report";
                    Directory.CreateDirectory(path);

                    foreach (DataRow row in dt_DOList.Rows)
                    {
                        bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                        if (selected)
                        {
                            string deliveredDate = row[header_DeliveredDate].ToString();

                            DODate = DateTime.TryParse(deliveredDate, out DateTime test) ? test : frmExportSetting.DODate;

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = path;

                            string customerName = row[header_Customer].ToString();
                            string POTableCode = row[header_POCode].ToString();
                            string PONo = row[header_PONo].ToString();
                            string DONo = row[header_DONo].ToString();
                            string DONoString = row[header_DONoString].ToString();


                            sfd.Filter = "Excel Documents (*.xls)|*.xls";
                            sfd.FileName = DONoString + ".xls";

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                frmLoading.ShowLoadingScreen();
                                tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                                // Copy DataGridView results to clipboard
                                copyAlltoClipboard();
                                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                object misValue = Missing.Value;

                                Excel.Application xlexcel = new Excel.Application
                                {
                                    PrintCommunication = false,
                                    ScreenUpdating = false,
                                    DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                                };

                                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);


                                xlexcel.StandardFont = "Calibri";
                                xlexcel.StandardFontSize = 12;

                                xlexcel.Calculation = XlCalculation.xlCalculationManual;
                                xlexcel.PrintCommunication = false;

                                int sheetNo = 0;
                                int pageNo = 1;
                                int maxRow = 15;
                                int rowNo = 0;
                                int rowOffset = 20;

                                Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;
                                //xlWorkSheet.PageSetup.PrintArea

                                if (sheetNo > 0)
                                {
                                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                }

                                sheetNo++;


                                xlWorkSheet.Name = customerName + "_" + DONoString;

                                ExcelPageSetup(xlWorkSheet);

                                xlexcel.PrintCommunication = true;
                                xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                                InitialDOFormat(xlWorkSheet);



                                string DONoArea = "u13:x13";
                                string PONoArea = "u14:x14";
                                string DODateArea = "u15:x15";
                                string pageNoArea = "w1:x1";

                                InsertToSheet(xlWorkSheet, pageNoArea, pageNo + "/" + pageNo);
                                InsertToSheet(xlWorkSheet, DONoArea, DONoString);
                                InsertToSheet(xlWorkSheet, PONoArea, PONo);
                                InsertToSheet(xlWorkSheet, DODateArea, DODate.ToOADate());

                                string custFullName = null;
                                string shippingAddress_1 = null;
                                string shippingAddress_2 = null;
                                string shippingPostalCode = null;
                                string shippingCity = null;
                                string shippingState = null;
                                string Contact = null;

                                //string itemList = "a20:a36";

                                string descriptionRowStart = "b";
                                string descriptionRowEnd = ":l";

                                string qtyColStart = "n";
                                string qtyColEnd = ":o";

                                string pcsColStart = "p";
                                string pcsColEnd = ":p";

                                string remarkColStart = "t";
                                string remarkColEnd = ":w";

                                string CustFullNameArea = "c11:M11";
                                string AddressArea_1 = "c12:M12";
                                string AddressArea_2 = "c13:M13";
                                string postalAndCity = "c14:M14";
                                string state = "c15:M15";
                                string contact = "c16:M16";

                                foreach (DataRow row2 in dt_PO.Rows)
                                {
                                    if (DONo == row2[dalSPP.DONo].ToString() && row2[dalSPP.DONo] != DBNull.Value)
                                    {
                                        int deliveryQty = row2[dalSPP.ToDeliveryQty] == DBNull.Value ? 0 : Convert.ToInt32(row2[dalSPP.ToDeliveryQty].ToString());

                                        custFullName = row2[dalSPP.FullName].ToString();
                                        shippingAddress_1 = row2[dalSPP.Address1].ToString();
                                        shippingAddress_2 = row2[dalSPP.Address2].ToString();
                                        shippingPostalCode = row2[dalSPP.AddressPostalCode].ToString();
                                        shippingCity = row2[dalSPP.AddressCity].ToString();
                                        shippingState = row2[dalSPP.AddressState].ToString();
                                        Contact = row2[dalSPP.Phone1].ToString();

                                        if (deliveryQty > 0)
                                        {
                                            string type = row2[dalSPP.TypeName].ToString();
                                            int stdPacking = int.TryParse(row2[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;
                                            int bag = deliveryQty / stdPacking;
                                            string size = row2[dalSPP.SizeNumerator].ToString();
                                            string unit = row2[dalSPP.SizeUnit].ToString().ToUpper();
                                            string itemCode = row2[dalSPP.ItemCode].ToString();
                                            string remark = bag + " BAG(S)";

                                            //check current row vs max row
                                            if (rowNo + 1 > maxRow)
                                            {
                                                rowNo = 1;
                                                pageNo++;
                                                //create new sheet
                                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                                if (pageNo > 1)
                                                    xlWorkSheet.Name = customerName + "_" + DONoString + "_" + pageNo;

                                                ExcelPageSetup(xlWorkSheet);
                                                InitialDOFormat(xlWorkSheet);
                                                InsertToSheet(xlWorkSheet, DONoArea, DONoString);
                                                InsertToSheet(xlWorkSheet, PONoArea, PONo);
                                                InsertToSheet(xlWorkSheet, pageNoArea, pageNo + "/" + pageNo);

                                                InsertToSheet(xlWorkSheet, DODateArea, DODate.ToOADate());
                                                Worksheet previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString];

                                                InsertToSheet(previousSheet, pageNoArea, 1 + "/" + pageNo);
                                                InsertToSheet(previousSheet, CustFullNameArea, custFullName);
                                                InsertToSheet(previousSheet, AddressArea_1, shippingAddress_1);
                                                InsertToSheet(previousSheet, AddressArea_2, shippingAddress_2);
                                                InsertToSheet(previousSheet, postalAndCity, shippingPostalCode + " " + shippingCity);
                                                InsertToSheet(previousSheet, state, shippingState);
                                                InsertToSheet(previousSheet, contact, Contact);

                                                //change total page number in previous sheet(s)
                                                for (int i = 2; i < pageNo; i++)
                                                {
                                                    previousSheet = (Worksheet)xlWorkBook.Worksheets[customerName + "_" + DONoString + "_" + i];

                                                    InsertToSheet(previousSheet, pageNoArea, i + "/" + pageNo);
                                                    InsertToSheet(previousSheet, CustFullNameArea, custFullName);
                                                    InsertToSheet(previousSheet, AddressArea_1, shippingAddress_1);
                                                    InsertToSheet(previousSheet, AddressArea_2, shippingAddress_2);
                                                    InsertToSheet(previousSheet, postalAndCity, shippingPostalCode + " " + shippingCity);
                                                    InsertToSheet(previousSheet, state, shippingState);
                                                    InsertToSheet(previousSheet, contact, Contact);

                                                }

                                                sheetNo++;

                                            }
                                            else
                                            {
                                                rowNo++;
                                            }
                                            //insert data
                                            string descriptionRow = descriptionRowStart + (rowOffset + rowNo).ToString() + descriptionRowEnd + (rowOffset + rowNo).ToString();

                                            string type_ShortName = text.EqualElbow_Short;

                                            if (type == text.Type_EqualSocket)
                                            {
                                                type_ShortName = text.EqualSocket_Short;
                                            }
                                            else if (type == text.Type_EqualTee)
                                            {
                                                type_ShortName = text.EqualTee_Short;
                                            }

                                            string newItemCode = text.SPP_BrandName + type_ShortName + size;
                                            InsertToSheet(xlWorkSheet, descriptionRow, newItemCode + "     " + size + " " + unit + " " + type);

                                            descriptionRow = qtyColStart + (rowOffset + rowNo).ToString() + qtyColEnd + (rowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, descriptionRow, deliveryQty);

                                            descriptionRow = pcsColStart + (rowOffset + rowNo).ToString() + pcsColEnd + (rowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, descriptionRow, "PCS");

                                            descriptionRow = remarkColStart + (rowOffset + rowNo).ToString() + remarkColEnd + (rowOffset + rowNo).ToString();
                                            InsertToSheet(xlWorkSheet, descriptionRow, remark);


                                        }

                                    }
                                }

                                if (string.IsNullOrEmpty(shippingAddress_1))
                                {
                                    AddressArea_2 = "c11:M11";
                                    postalAndCity = "c12:M12";
                                    state = "c13:M13";
                                }

                                if (string.IsNullOrEmpty(shippingAddress_2))
                                {
                                    postalAndCity = "c12:M12";
                                    state = "c13:M13";
                                }

                                InsertToSheet(xlWorkSheet, CustFullNameArea, custFullName);
                                InsertToSheet(xlWorkSheet, AddressArea_1, shippingAddress_1);
                                InsertToSheet(xlWorkSheet, AddressArea_2, shippingAddress_2);
                                InsertToSheet(xlWorkSheet, postalAndCity, shippingPostalCode + " " + shippingCity);
                                InsertToSheet(xlWorkSheet, state, shippingState);
                                InsertToSheet(xlWorkSheet, contact, Contact);


                                tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));

                                pageNo = 1;
                                rowNo = 0;

                                if (sheetNo > 0)
                                {
                                    xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                                   misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                }

                                // Clear Clipboard and DataGridView selection
                                Clipboard.Clear();
                                dgv.ClearSelection();

                                frmLoading.CloseForm();

                                // Open the newly saved excel file
                                if (File.Exists(sfd.FileName))
                                {

                                    if (openFile)
                                    {
                                        Excel.Application excel = new Excel.Application();
                                        excel.Visible = true;
                                        excel.DisplayAlerts = false;
                                        Workbook wb = excel.Workbooks.Open(sfd.FileName,ReadOnly: false, Notify: false);
                                        excel.DisplayAlerts = true;
                                        //System.Diagnostics.Process.Start(sfd.FileName);
                                    }
                                    else
                                    {
                                        MessageBox.Show("D/O export successful!");
                                    }

                                    if (printFile)
                                    {
                                        xlexcel.Visible = printPreview;
                                        xlWorkBook.PrintOut(Preview: printPreview, Collate: true);
                                        //xlWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, printPreview, Type.Missing, Type.Missing, true, Type.Missing);
                                    }

                                }

                                xlexcel.DisplayAlerts = true;

                                xlWorkBook.Close(true, misValue, misValue);
                                xlexcel.Quit();

                                releaseObject(xlWorkSheet);

                                releaseObject(xlWorkBook);
                                releaseObject(xlexcel);
                            }

                        }
                    }

                   

                    Cursor = Cursors.Arrow; // change cursor to normal type

                }
                catch (Exception ex)
                {
                    frmLoading.CloseForm();
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
            }
            #endregion
        }

        private void ExportToExcel()
        {
            //show export setting
            frmExportSetting frm = new frmExportSetting()
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            frm.ShowDialog();

            //get return data
            if(frmExportSetting.settingApplied)
            {
               
                bool allInOne = frmExportSetting.allInOne;

                if(allInOne)
                {
                    AllInOneExcel();
                }
                else
                {
                    SeparateExcel();
                }
               
            }

            DOListUI();
        }

        private void copyAlltoClipboard()
        {
            //dgvNewStock.Columns.Remove(headerParentColor);
            //dgvNewStock.Columns.Remove(headerRepeat);
            dgvDOList.SelectAll();
            //dgvNewStock.sele
            DataObject dataObj = dgvDOList.GetClipboardContent();
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

        #endregion

        private void dgvDOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvDOList;

            if (rowIndex >= 0 && dgv.SelectedRows.Count > 0)
            {
                string tblName = dalSPP.DOTableName;
                string code = dgv.Rows[rowIndex].Cells[header_DONo].Value.ToString();

                frmActionLog frm = new frmActionLog(tblName, code)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();
            }

        }

        private void ColorDONoByStatus()
        {
            DataGridView dgv = dgvDOList;
            dgv.SuspendLayout();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Columns[j].Name == header_DataType)
                    {
                        if (dgv.Rows[i].Cells[j].Value.ToString() == DataType_Delivered)
                        {
                            dgv.Rows[i].Cells[header_DONoString].Style.ForeColor = Color.Green;
                        }
                        else if (dgv.Rows[i].Cells[j].Value.ToString() == DataType_Removed)
                        {
                            dgv.Rows[i].Cells[header_DONoString].Style.ForeColor = Color.Red;

                        }
                        else
                        {
                            dgv.Rows[i].Cells[header_DONoString].Style.ForeColor = Color.Black;

                        }
                    }
                }
            }

            dgv.ResumeLayout();

            dgv.ClearSelection();
        }

        private void dgvDOList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorDONoByStatus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDOList();
        }

        private void frmSPPDOList_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmSPP.Reload();
        }
    }
}
