﻿using FactoryManagementSoftware.DAL;
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
        SPPDataDAL dalSPP = new SPPDataDAL();
        SPPDataBLL uSpp = new SPPDataBLL();

        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string text_CompleteDO = "Complete D/O";
        readonly string text_InCompleteDO = "Incomplete D/O";
        readonly string text_SelectDO = "SELECT D/O";
        readonly string text_Export= "Export >>";
        readonly string text_CancelSelectingMode = "CANCEL";
        readonly string text_RemoveDO = "REMOVE D/O";
        readonly string text_Excel = "EXCEL";

        readonly string header_POCode = "P/O CODE";
        readonly string header_PONo = "P/O NO";
        readonly string header_PODate = "P/O DATE";
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
        readonly string header_TrfTableCode = "TRANSFER CODE";
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
                
                btnRemove.Visible = true;
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
                    btnEdit.Visible = true;
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

            dgvDOList.ClearSelection();
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
            //int id =  dalTrfHist.GetLastInsertedID();

            //MessageBox.Show(id.ToString());
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

                    string trfID = row[dalSPP.TrfTableCode].ToString();
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

                        if(isDelivered && !string.IsNullOrEmpty(trfID))
                        {
                            dt_row[header_DeliveredDate] = tool.GetTransferDate(trfID);
                        }
                        
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
                        // int TrfTableCode = int.TryParse(row[dalSPP.TrfTableCode].ToString(), out TrfTableCode) ? TrfTableCode : -1;

                        dt_Row[header_DOTblCode] = DOTableCode;
                        //dt_Row[header_TrfTableCode] = TrfTableCode;

                        if (deliveryQty > 0)
                        {
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
                if(!DOSelectingMode)
                btnEdit.Visible = true;

                btnRemove.Visible = true;
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

        private void RemoveDO(int DONo)
        {
            uSpp.IsRemoved = true;
            uSpp.DO_no = DONo;

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;

            if (dalSPP.DORemove(uSpp))
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

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to REMOVE DO: " + DONo.ToString("D6") + " ?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        RemoveDO(DONo);

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

                    my_menu.Items.Add(text_CompleteDO).Name = text_CompleteDO;

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

            if (frmInOutEdit.TrfSuccess)
            {
                MessageBox.Show("transfer success");

                LoadDOList();
            }
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "SBB_Delivery_Order_" + DateTime.Now.ToString("dd-MM-yy_HHmmss") + ".xls";

            //DateTime currentDate = DateTime.Now;
            //fileName = "StockReport(" + cmbCustomer.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
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
            string DOInfo = "a10:a15";
            string Divider = "a17:a17";
            string tableHeader = "a18:a19";
            string itemList = "a20:a36";
            string signingArea = "a37:a44";
            string wholeSheetArea = "a1:x44";
            string littleSpace_1 = "a9:x9";
            string littleSpace_2 = "a16:x16";
            string companyName_CN = "F1:S2";
            string companyName_EN = "F3:S4";
            string company_Registration = "F5:S5";
            string company_AddressAndContact = "F6:S8";
            string DONoArea = "u12:x12";
            string PONoArea = "u13:x13";
            string DODateArea = "u14:x14";
            string CustFullNameArea = "c10:M10";
            string AddressArea_1 = "c11:M11";
            string AddressArea_2 = "c12:M12";
            string postalAndCity = "c13:M13";
            string state = "c14:M14";
            string contact = "c15:M15";
            string pageString = "v1:v1";
            string pageNo = "w1:x1";
            #endregion

            int itemRowOffset = 19;
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
            DOFormat.RowHeight = 4.2;

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

            string tempPath = Path.GetTempFileName();
            Resources.safety_logo.Save(tempPath + "safety-logo.png");
            string filePath = tempPath + "safety-logo.png";
            //var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 20, 88f, 49f);

            #endregion

            DOFormat = xlWorkSheet.get_Range(companyName_CN).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_Name_CN;
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
            DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_Registration).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10.5;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_AddressAndContact).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range("o9:x11").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY ORDER";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = "Arial Black";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o12:s12").Cells;
            DOFormat.Merge();
            DOFormat.Value = "No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o13:s13").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your Order No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o14:s14").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("t12:t12").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

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


            DOFormat = xlWorkSheet.get_Range("a10:b10").Cells;
            DOFormat.Merge();
            DOFormat.Value = "M/S:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range("a15:b15").Cells;
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
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = "Cambria";

            Color color = Color.Black;
            DOFormat = xlWorkSheet.get_Range("a17:w17").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("b18:l19").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("n18:q19").Cells;
            DOFormat.Merge();
            DOFormat.Value = "QUANTITY";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("r18:w19").Cells;
            DOFormat.Merge();
            DOFormat.Value = "REMARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("a19:w19").Cells;
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
                            int maxRow = 16;
                            int rowNo = 0;
                            int rowOffset = 19;

                            Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;
                            //xlWorkSheet.PageSetup.PrintArea
                            DataTable dt_PO = dalSPP.DOWithInfoSelect();

                            foreach (DataRow row in dt_DOList.Rows)
                            {
                                bool selected = bool.TryParse(row[header_Selected].ToString(), out selected) ? selected : false;

                                if(selected)
                                {
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


                                    
                                    string DONoArea = "u12:x12";
                                    string PONoArea = "u13:x13";
                                    string DODateArea = "u14:x14";
                                    string pageNoArea = "w1:x1";

                                    InsertToSheet(xlWorkSheet, pageNoArea, pageNo+"/"+pageNo);
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

                                    string CustFullNameArea = "c10:M10";
                                    string AddressArea_1 = "c11:M11";
                                    string AddressArea_2 = "c12:M12";
                                    string postalAndCity = "c13:M13";
                                    string state = "c14:M14";
                                    string contact = "c15:M15";

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
                                                if(rowNo + 1 > maxRow)
                                                {
                                                    rowNo = 1;
                                                    pageNo++;
                                                    //create new sheet
                                                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                                                    if(pageNo > 1)
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

                                                if(type == text.Type_EqualSocket)
                                                {
                                                    type_ShortName = text.EqualSocket_Short;
                                                }
                                                else if(type == text.Type_EqualTee)
                                                {
                                                    type_ShortName = text.EqualTee_Short;
                                                }

                                                string newItemCode = text.SPP_BrandName + type_ShortName + size;
                                                InsertToSheet(xlWorkSheet, descriptionRow, newItemCode + "     "+size+" "+unit+" "+type);

                                                descriptionRow = qtyColStart + (rowOffset + rowNo).ToString() + qtyColEnd + (rowOffset + rowNo).ToString();
                                                InsertToSheet(xlWorkSheet, descriptionRow, deliveryQty);

                                                descriptionRow = pcsColStart + (rowOffset + rowNo).ToString() + pcsColEnd + (rowOffset + rowNo).ToString();
                                                InsertToSheet(xlWorkSheet, descriptionRow, "PCS");

                                                descriptionRow = remarkColStart + (rowOffset + rowNo).ToString() + remarkColEnd + (rowOffset + rowNo).ToString();
                                                InsertToSheet(xlWorkSheet, descriptionRow, remark);
                                            }
                                                
                                        }
                                    }

                                    

                                    if(string.IsNullOrEmpty(shippingAddress_1))
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
                                    InsertToSheet(xlWorkSheet, postalAndCity, shippingPostalCode+" "+ shippingCity);
                                    InsertToSheet(xlWorkSheet, state, shippingState);
                                    InsertToSheet(xlWorkSheet, contact, Contact);

                            
                                    pageNo = 1;
                                    rowNo = 0;
                                   

                                }
                            }

                            if(sheetNo > 0)
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
                                    Workbook wb = excel.Workbooks.Open(sfd.FileName);

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
    }
}
