﻿using System;
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
using System.Globalization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBDeliveryPlanning : Form
    {
        public frmSBBDeliveryPlanning()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            LoadEditUnitCMB(cmbEditUnit);
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

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE INT";
        readonly string header_Unit = "UNIT";
        readonly string header_SizeString = "SIZE";
        readonly string header_Type = "TYPE";
        readonly string header_Code = "CODE";
        readonly string header_StockPcs = "STOCK (PCS)";
        readonly string header_StockBag = "STOCK (BAG)";
        readonly string header_StockString = "STOCK";
        readonly string header_QtyPerBag = "QTY PER BAG";
        readonly string header_BalAfterDelivery = "BAL. AFTER DELIVERY";
        readonly string header_BalAfterDeliveryInPcs = "BAL. AFTER DELIVERY IN PCS";

        readonly string header_Priority = "PO_";
        readonly string header_PriorityDelivered = "PRIORITY DELIVERED_";
        readonly string header_PriorityOrder = "PRIORITY ORDER_";
        readonly string header_PriorityTblCode = "PRIORITY TABLE CODE_";
        readonly string header_PriorityPOCode = "PRIORITY PO CODE_";
        readonly string header_PriorityPONo = "PRIORITY PO NO_";
        readonly string header_PriorityCustShortName = "PRIORITY CUST TBLCODE_";
        readonly string header_PriorityToDeliveryStatus = "PRIORITY DELIVERY STATUS_";
        readonly string header_PriorityToDeliveryQty = "PRIORITY TO DELIVERY QTY_";
        readonly string header_PriorityToDeliveryPlanningInPCS = "PRIORITY TO DELIVERY PLANNING IN PCS_";

        readonly string header_POTblCode = "PO TBL CODE";
        readonly string header_ToDeliveryPCSQty = "TO DELIVERY PCS QTY";
        readonly string header_ItemCode = "CODE";
        readonly string header_ItemName = "NAME";
        readonly string header_BalAfter = "BAL. AFTER";

        readonly string text_Unit = "UNIT";
        readonly string text_Bag = "BAG";
        readonly string text_Pcs = "PCS";

        readonly string text_DeliveryStatus_ToOpen = "TO OPEN";
        readonly string text_DeliveryStatus_Opened = "OPENED";
        readonly string text_DeliveryStatus_OpenedWithDiff = "OPENED WITH DIFF";

        private DataTable dt_POList;
        private DataTable dt_DOList;
        private DataTable dt_Product;
        private DataTable dt_Item;
        private DataTable dt_Mat;
        private DataTable dt_ToUpdate;

        private bool Loaded = false;
        private bool dataChanged = false;
        private int oldToDeliveryQty = 0;

        private DataTable NewToUpdateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POTblCode, typeof(string));
            dt.Columns.Add(header_ToDeliveryPCSQty, typeof(int));

            return dt;
        }

        private DataTable NewStockAlertTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));
            dt.Columns.Add(header_BalAfter, typeof(int));
        
            return dt;
        }

        private DataTable NewPlanningTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_SizeString, typeof(string));

            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_StockPcs, typeof(int));
            dt.Columns.Add(header_StockBag, typeof(int));
            dt.Columns.Add(header_StockString, typeof(string));
            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_BalAfterDelivery, typeof(string));
            dt.Columns.Add(header_BalAfterDeliveryInPcs, typeof(int));
            return dt;
        }

        private void AdjustDGVRowAlignment(DataGridView dgv)
        {
            for(int i = 0; i< dgv.Rows.Count; i++)
            {
                //row.AdjustRowHeaderBorderStyle
                string code = dgv.Rows[i].Cells[header_Code].Value.ToString();

                if(string.IsNullOrEmpty(code))
                {
                    dgv.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(64,64,64);
                    dgv.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                    dgv.Rows[i].Cells[header_Type].Style.Alignment = DataGridViewContentAlignment.BottomLeft;
                    dgv.Rows[i].Cells[header_StockString].Style.Alignment = DataGridViewContentAlignment.BottomRight;
                   
                }
                else
                {
                    //dgv.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    DataGridViewRow row = dgv.Rows[i + 1];
                    row.MinimumHeight = 2;

                    
                    dgv.Rows[i + 1].Height = 2;
                    dgv.Rows[i + 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dgv.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

                    dgv.Rows[i].Cells[header_BalAfterDelivery].Style.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

                    dgv.Rows[i].Cells[header_Type].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Rows[i].Cells[header_StockString].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Rows[i].Cells[header_BalAfterDelivery].Style.Alignment = DataGridViewContentAlignment.TopCenter;
                }
            } 
        }

        private void DgvUIEdit(DataGridView dgv, int priorityColNum)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_Code].Visible = false;
            dgv.Columns[header_Size].Visible = false;
            dgv.Columns[header_Unit].Visible = false;
            dgv.Columns[header_StockPcs].Visible = false;
            dgv.Columns[header_StockBag].Visible = false;
            dgv.Columns[header_QtyPerBag].Visible = false;
            dgv.Columns[header_BalAfterDeliveryInPcs].Visible = false;

            dgv.Columns[header_Code].DefaultCellStyle.ForeColor = Color.Gray;
            dgv.Columns[header_Code].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_StockString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            for (int i = 1; i <= priorityColNum; i++)
            {
                string colName = header_Priority + i;
                string colDeliveredName = header_PriorityDelivered + i;
                string colOrderName = header_PriorityOrder + i;
                string colTblCodeName = header_PriorityTblCode + i;
                string colPOCodeName = header_PriorityPOCode + i;
                string colPONoName = header_PriorityPONo + i;
                string colCustShortName = header_PriorityCustShortName + i;
                string colDeliveryStatusName = header_PriorityToDeliveryStatus + i;
                string colToDeliveryQtyName = header_PriorityToDeliveryQty + i;
                string colPlanningToDeliveryQtyName = header_PriorityToDeliveryPlanningInPCS + i;

                dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[colName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                dgv.Columns[colDeliveredName].Visible = false;
                dgv.Columns[colOrderName].Visible = false;
                dgv.Columns[colCustShortName].Visible = false;
                dgv.Columns[colTblCodeName].Visible = false;
                dgv.Columns[colPOCodeName].Visible = false;
                dgv.Columns[colPONoName].Visible = false;
                dgv.Columns[colDeliveryStatusName].Visible = false;
                dgv.Columns[colToDeliveryQtyName].Visible = false;
                dgv.Columns[colPlanningToDeliveryQtyName].Visible = false;

            }

            //dgv.Columns[header_BalAfter2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_BalAfter3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_ProduceTarget].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_Produced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_ProductionString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_PODate].DefaultCellStyle.ForeColor = Color.Gray;
            ////dgv.Columns[header_DONo].DefaultCellStyle.ForeColor = Color.Gray;
            //dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_PONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_Progress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_POCode].Visible = false;
            //dgv.Columns[header_CustomerCode].Visible = false;



        }

        private void AddToUpdateData(string POTblCode, int ToDeliveryQty)
        {
            if(dt_ToUpdate == null)
            {
                dt_ToUpdate = NewToUpdateTable();
            }

            bool dataExist = false;

            foreach(DataRow row in dt_ToUpdate.Rows)
            {
                if(POTblCode == row[header_POTblCode].ToString())
                {
                    dataExist = true;
                    row[header_ToDeliveryPCSQty] = ToDeliveryQty;
                }
            }

            if(!dataExist)
            {
                DataRow newRow = dt_ToUpdate.NewRow();

                newRow[header_POTblCode] = POTblCode;
                newRow[header_ToDeliveryPCSQty] = ToDeliveryQty;

                dt_ToUpdate.Rows.Add(newRow);
            }

        }

        private void LoadEditUnitCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(text_Unit);

            dt.Rows.Add(text_Bag);
            dt.Rows.Add(text_Pcs);

            cmb.DataSource = dt;
            cmb.DisplayMember = text_Unit;
        }

        private void LoadPOList()
        {
            Cursor = Cursors.WaitCursor;
            frmLoading.ShowLoadingScreen();

            #region Load data from database

            DataTable dt_Planning = NewPlanningTable();

            string itemCust = text.SPP_BrandName;
            dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);
            dt_Item = dalItem.Select();
            dt_POList = dalSPP.POSelect();
            dt_DOList = dalSPP.DOWithInfoSelect();

            dt_POList.DefaultView.Sort = dalSPP.PriorityLevel + " ASC," + dalSPP.PODate + " ASC," + dalSPP.POCode + " ASC," + dalSPP.CustomerTableCode + " ASC";
            dt_POList = dt_POList.DefaultView.ToTable();

            int index = 1;
            #endregion

            //load product & stock
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

                string typeName = row[dalSPP.TypeName].ToString();

                int pcsStock = readyStock;
                int bagStock = pcsStock / qtyPerBag;


                string sizeString = "";
                int size = 1;
                if(denominator == 1)
                {
                    size = numerator;
                    sizeString = numerator + " " + sizeUnit;
                    
                }
                else
                {
                    size = numerator/denominator;
                    sizeString = numerator +"/" + denominator + " " + sizeUnit;
                }


                DataRow planning_row = dt_Planning.NewRow();

                string stockString = pcsStock + " PCS";

                planning_row[header_Type] = itemCode;
                planning_row[header_StockString] = stockString;

                dt_Planning.Rows.Add(planning_row);


                //stockString = bagStock + " BAGS (" + pcsStock + ")";
                stockString = bagStock + " BAGS";

                planning_row = dt_Planning.NewRow();

                planning_row[header_Index] = index;

                planning_row[header_Size] = size;
                planning_row[header_Unit] = sizeUnit;
                planning_row[header_SizeString] = sizeString;

                planning_row[header_Type] = typeName;
                planning_row[header_Code] = itemCode;
                planning_row[header_StockPcs] = pcsStock;
                planning_row[header_StockBag] = bagStock;
                planning_row[header_StockString] = stockString;
                planning_row[header_QtyPerBag] = qtyPerBag;
                planning_row[header_BalAfterDelivery] = stockString;
                planning_row[header_BalAfterDeliveryInPcs] = pcsStock;

                dt_Planning.Rows.Add(planning_row);
                index++;

                dt_Planning.Rows.Add(dt_Planning.NewRow());
            }

            int prePOCode = -2, CustTblCode = -1;
            string PONo = "", ShortName = "";
            DateTime PODate;
            int colNum = 0;
            string colName = "";
            string colDeliveredName = "";
            string colOrderName = "";
            string colTblCodeName = "";
            string colPOCodeName = "";
            string colPONoName = "";
            string colCustShortName = "";
            string colDeliveryStatusName = "";
            string colToDeliveryQtyName = "";
            string colPlanningToDeliveryQtyName = "";

            //load PO data
            foreach (DataRow row in dt_POList.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                bool isFreeze = bool.TryParse(row[dalSPP.Freeze].ToString(), out isFreeze) ? isFreeze : false;

                int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;
                int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                int toDeliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out toDeliveryQty) ? toDeliveryQty : 0;
                string poTblCode = row[dalSPP.TableCode].ToString();
                string poCustTblCode = row[dalSPP.CustomerTableCode].ToString();

               //if(poTblCode == "727")
               // {
               //     float test = 0;
               // }

                if (!isRemoved && !isFreeze && deliveredQty < orderQty)
                {
                    int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;
                    string poItemCode = row[dalSPP.ItemCode].ToString();

                    //if (poCode.ToString() == "122")
                    //{
                    //    float trest = 0;
                    //}

                    if (prePOCode != poCode)
                    {
                        #region add column to table planning

                        colNum++;
                        colName = header_Priority + colNum;
                        dt_Planning.Columns.Add(colName, typeof(string));

                        colDeliveryStatusName = header_PriorityToDeliveryStatus + colNum;
                        dt_Planning.Columns.Add(colDeliveryStatusName, typeof(string));

                        colDeliveredName = header_PriorityDelivered + colNum;
                        dt_Planning.Columns.Add(colDeliveredName, typeof(int));

                        colOrderName = header_PriorityOrder + colNum;
                        dt_Planning.Columns.Add(colOrderName, typeof(int));

                        colTblCodeName = header_PriorityTblCode + colNum;
                        dt_Planning.Columns.Add(colTblCodeName, typeof(string));

                        colPOCodeName = header_PriorityPOCode + colNum;
                        dt_Planning.Columns.Add(colPOCodeName, typeof(string));

                        colPONoName = header_PriorityPONo + colNum;
                        dt_Planning.Columns.Add(colPONoName, typeof(string));

                        colCustShortName = header_PriorityCustShortName + colNum;
                        dt_Planning.Columns.Add(colCustShortName, typeof(string));

                        colToDeliveryQtyName = header_PriorityToDeliveryQty + colNum;
                        dt_Planning.Columns.Add(colToDeliveryQtyName, typeof(int));

                        colPlanningToDeliveryQtyName = header_PriorityToDeliveryPlanningInPCS + colNum;
                        dt_Planning.Columns.Add(colPlanningToDeliveryQtyName, typeof(int));

                        #endregion

                        prePOCode = poCode;
                        
                        PONo = row[dalSPP.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSPP.PODate]).Date;

                        ShortName = row[dalSPP.ShortName].ToString();

                    }

                    //add data
                    for(int i = 0; i < dt_Planning.Rows.Count; i++)
                    {
                        string planningCode = dt_Planning.Rows[i][header_Code].ToString();
                        int pcsPerBag = int.TryParse(dt_Planning.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : 1;

                        if (planningCode == poItemCode && i-1 >= 0)
                        {
                            DataColumnCollection columns = dt_Planning.Columns;

                            if (columns.Contains(colName) && columns.Contains(colDeliveredName) && columns.Contains(colOrderName))
                            {
                                int planningDelivered = int.TryParse(dt_Planning.Rows[i-1][colDeliveredName].ToString(), out planningDelivered) ? planningDelivered : 0;
                                int planningOrder = int.TryParse(dt_Planning.Rows[i - 1][colOrderName].ToString(), out planningOrder) ? planningOrder : 0;
                                //int planningToDelivery = int.TryParse(dt_Planning.Rows[i][colOrderName].ToString(), out planningToDelivery) ? planningToDelivery : 0;

                                planningDelivered += deliveredQty;
                                planningOrder += orderQty;

                                int divideBy = pcsPerBag;

                                if(cmbEditUnit.Text == text_Pcs)
                                {
                                    divideBy = 1;
                                }

                                //planningToDelivery =  toDeliveryQty;

                                //planningDelivered /= pcsPerBag;
                                //planningOrder /= pcsPerBag;

                               
                                dt_Planning.Rows[i - 1][colDeliveredName] = planningDelivered;
                                dt_Planning.Rows[i - 1][colOrderName] = planningOrder;

                                dt_Planning.Rows[i - 1][colTblCodeName] = poTblCode;
                                dt_Planning.Rows[i - 1][colPOCodeName] = poCode;
                                dt_Planning.Rows[i - 1][colPONoName] = PONo;
                                dt_Planning.Rows[i - 1][colCustShortName] = ShortName;

                                //get DO to delivery qty from tbl_DO, and compare to planning toDelivery
                                //input delivery status
                                int DO_ToDeliveryQty = 0;
                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                foreach (DataRow rowDO in dt_DOList.Rows)
                                {
                                    bool isDORemoved = bool.TryParse(rowDO[dalSPP.IsRemoved].ToString(), out isDORemoved) ? isDORemoved : false;
                                    bool isDODelivered = bool.TryParse(rowDO[dalSPP.IsDelivered].ToString(), out isDODelivered) ? isDODelivered : false;
                                    string DO_POTblCode = rowDO[dalSPP.POTableCode].ToString();

                                    if (!isDORemoved && !isDODelivered && DO_POTblCode == poTblCode)
                                    {
                                        //get to delivery qty

                                        //if(poTblCode == "681")
                                        //{
                                        //    string testt = rowDO[dalSPP.ToDeliveryQty].ToString();
                                        //}
                                        
                                        DO_ToDeliveryQty += int.TryParse(rowDO[dalSPP.ToDeliveryQty].ToString(), out DO_ToDeliveryQty) ? DO_ToDeliveryQty : 0;
                                       
                                    }
                                }

                                if(DO_ToDeliveryQty != 0)
                                {
                                    if(DO_ToDeliveryQty == toDeliveryQty)
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                    }
                                }

                                dt_Planning.Rows[i][colName] = toDeliveryQty / divideBy;
                                dt_Planning.Rows[i - 1][colPlanningToDeliveryQtyName] = toDeliveryQty;
                                dt_Planning.Rows[i - 1][colDeliveryStatusName] = deliveryStatus;
                                dt_Planning.Rows[i - 1][colToDeliveryQtyName] = DO_ToDeliveryQty;

                                string POData = " " + ShortName + " (" + PONo + "): " + DO_ToDeliveryQty / divideBy + "/" + planningDelivered / divideBy + "/" + planningOrder / divideBy + " ";
                                dt_Planning.Rows[i - 1][colName] = POData;

                            }
                            else
                            {
                                MessageBox.Show("Columns not exist!");
                            }

                            break;
                        }
                    }

                   
                }
                
            }

            dgvList.DataSource = dt_Planning;

            DgvUIEdit(dgvList, colNum);
            AdjustDGVRowAlignment(dgvList);

            AdjustToDeliveryForeColor();
            //UpdateBalAfterDelivery(int row);
            dgvList.ClearSelection();
           

            Cursor = Cursors.Arrow;

            frmLoading.CloseForm();

           
        }

        private void AdjustToDeliveryForeColor()
        {
            DataGridView dgv = dgvList;

            for(int i = 0; i< dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    int rowIndex = i;
                    int colIndex = j;

                    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
                    string colName = dgv.Columns[colIndex].Name;

                    if (colName.Contains(header_Priority) && int.TryParse(cellValue, out int x))
                    {
                        dgv.Rows[i].Cells[j].Style.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    }

                    // change to delivery qty fore color
                    if (cellValue == text_DeliveryStatus_ToOpen)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = Color.FromArgb(253, 203, 110);
                    }
                    else if (cellValue == text_DeliveryStatus_Opened)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = Color.FromArgb(0, 184, 148); 
                    }
                    else if (cellValue == text_DeliveryStatus_OpenedWithDiff)
                    {
                        dgv.Rows[rowIndex + 1].Cells[colIndex - 1].Style.BackColor = Color.FromArgb(52, 160, 225);
                    }
                }
            }
           

        }

        private void frmSBBDeliveryPlanning_Load(object sender, EventArgs e)
        {
            
     

            LoadPOList();


          
            Loaded = true;
            BringToFront();
            Activate();
        }

        private void SwitchUnit()
        {
            Cursor = Cursors.WaitCursor;

            DataGridView dgv = dgvList;

            DataTable dt = (DataTable)dgv.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;

                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        if (colName == searchingColumns)
                        {
                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;
                            int DOToDeliveryQtyInPcs = int.TryParse(dt.Rows[i - 1][header_PriorityToDeliveryQty + priorityLvl.ToString()].ToString(), out DOToDeliveryQtyInPcs) ? DOToDeliveryQtyInPcs : 0;

                            int planToDeliveryQty = int.TryParse(dt.Rows[i - 1][header_PriorityToDeliveryPlanningInPCS + priorityLvl].ToString(), out planToDeliveryQty) ? planToDeliveryQty : 0;
 
                            string PONo = dt.Rows[i - 1][header_PriorityPONo + priorityLvl].ToString();
                            string ShortName = dt.Rows[i - 1][header_PriorityCustShortName + priorityLvl].ToString();

                            if(!string.IsNullOrEmpty(PONo))
                            {
                                if (cmbEditUnit.Text == text_Bag)
                                {
                                    //pcs switch to bag
                                    DOToDeliveryQtyInPcs /= pcsPerBag;
                                    deliveredQty /= pcsPerBag;
                                    orderQty /= pcsPerBag;
                                    planToDeliveryQty /= pcsPerBag;
                                }
                                else
                                {
                                    //bag switch to pcs
                                    //planToDeliveryQty *= pcsPerBag;
                                }

                                string POData = " " + ShortName + " (" + PONo + "): " + DOToDeliveryQtyInPcs + "/" + deliveredQty + "/" + orderQty + " ";

                                dt.Rows[i - 1][searchingColumns] = POData;
                                dt.Rows[i][searchingColumns] = planToDeliveryQty;
                               
                            }

                            priorityLvl++;
                        }
                    }
                }
            }

            Cursor = Cursors.Arrow;


        }

        private void cmbEditUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                SwitchUnit();
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
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

        private void dgvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                int toDeliveryQty = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out toDeliveryQty) ? toDeliveryQty : -1;

                if (dgv.Columns[colIndex].Name.Contains(header_Priority) && toDeliveryQty >= 0) //Desired Column
                {
                    oldToDeliveryQty = toDeliveryQty;

                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
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

        private string GetChildCode(DataTable dt, string parentCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (parentCode == row[dalJoin.ParentCode].ToString())
                {
                    string childCode = row[dalJoin.ChildCode].ToString();

                    if (childCode.Substring(0, 2) == "CF")
                    {
                        parentCode = childCode;

                        break;
                    }
                }

            }

            return parentCode;
        }

        private DataTable LoadMatPartList(DataTable dt_Product)
        {
            DataTable dt_MatPart = NewStockAlertTable();
            DataTable dt_Plan = dalPlan.Select();

            int index = 1;
            DataTable dtJoin = dalJoin.Select();

            foreach (DataRow row in dt_Product.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_ItemCode].ToString());

                int stillNeed = int.TryParse(row[header_BalAfter].ToString(), out stillNeed) ? stillNeed : 0;
             

                stillNeed = stillNeed > 0 ? 0 : stillNeed * -1;
             
                foreach (DataRow rowJoin in dtJoin.Rows)
                {
                    if (rowJoin[dalJoin.ParentCode].ToString() == parentCode)
                    {
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

                        int readyStock = int.TryParse(rowJoin[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                        int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                        int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                        int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                        int child_StillNeed = 0;
                     
                        joinMax = joinMax <= 0 ? 1 : joinMax;
                        joinMin = joinMin <= 0 ? 1 : joinMin;

                        child_StillNeed = stillNeed / joinMax * joinQty;

                        child_StillNeed = stillNeed % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;

                        int bal = readyStock - child_StillNeed;

                        bool childInserted = false;

                        foreach (DataRow mat_row in dt_MatPart.Rows)
                        {
                            if (childCode == mat_row[header_ItemCode].ToString())
                            {
                                childInserted = true;

                                int previousBal = int.TryParse(mat_row[header_BalAfter].ToString(), out previousBal) ? previousBal : 0;

                                bal = previousBal - child_StillNeed;

                                mat_row[header_BalAfter] = bal;
                              
                                break;
                            }
                        }

                        if (!childInserted)//!childInserted
                        {
                            DataRow alert_row = dt_MatPart.NewRow();

                            alert_row[header_ItemCode] = childCode;
                            alert_row[header_ItemName] = childName;
                            alert_row[header_BalAfter] = bal;

                            dt_MatPart.Rows.Add(alert_row);
                            index++;
                        }


                    }
                }
            }

            return dt_MatPart;
        }

        private void CheckIfProductionNeeded()
        {
            bool productionNeeded = false;

            DataTable dt = (DataTable)dgvList.DataSource;

            DataTable dt_Product = NewStockAlertTable();

            //get to assembly product list
            foreach (DataRow row in dt.Rows)
            {
                int bal = int.TryParse(row[header_BalAfterDeliveryInPcs].ToString(), out bal) ? bal : 0;

                if (bal < 0)
                {
                    DataRow newRow = dt_Product.NewRow();

                    newRow[header_ItemCode] = row[header_Code].ToString();
                    newRow[header_BalAfter] = bal;

                    dt_Product.Rows.Add(newRow);
                }
            }

             dt_Mat = LoadMatPartList(dt_Product);

            foreach(DataRow row in dt_Mat.Rows)
            {
                int bal = int.TryParse(row[header_BalAfter].ToString(), out bal) ? bal : 0;

                if(bal < 0)
                {
                    productionNeeded = true;
                    break;
                }
            }
            
            if(productionNeeded)
            {
                lblProductionAlert.Visible = true;
            }
            else
            {
                lblProductionAlert.Visible = false;
            }

        }

        private void CheckIfAssemblyNeeded(DataTable dt)
        {
            bool assemblyNeeded = false;
            foreach(DataRow row in dt.Rows)
            {
                string balString = row[header_BalAfterDelivery].ToString().Replace(" BAGS", "");
                int bal = int.TryParse(balString, out bal) ? bal : 0;

                if(bal < 0)
                {
                    assemblyNeeded = true;
                    break;
                }
            }

            if(assemblyNeeded)
            {
                lblAssemblyNeeded.Visible = true;

                
            }
            else
            {
                lblAssemblyNeeded.Visible = false;
            }

            CheckIfProductionNeeded();
        }

        private void UpdateBalAfterDelivery(int row)
        {
            DataTable dt = (DataTable)dgvList.DataSource;

            int pcsPerBag = int.TryParse(dt.Rows[row][header_QtyPerBag].ToString(), out pcsPerBag)? pcsPerBag : 1;
            int stockInPcs = int.TryParse(dt.Rows[row][header_StockPcs].ToString(), out stockInPcs) ? stockInPcs : 0;

            int TotalToDeliveryQty = 0;

            foreach(DataColumn col in dt.Columns)
            {
                string colName = col.ColumnName;
                int toDelivery = int.TryParse(dt.Rows[row][colName].ToString(), out toDelivery) ? toDelivery : -1;

                if(colName.Contains(header_Priority) && toDelivery >= 0)
                {
                    TotalToDeliveryQty += toDelivery;
                }
            }

            int divideBy = pcsPerBag;

            if(cmbEditUnit.Text == text_Pcs)
            {
                divideBy = 1;
            }

            int balInPcs = stockInPcs - TotalToDeliveryQty * divideBy;

            dt.Rows[row][header_BalAfterDelivery] = balInPcs/pcsPerBag + " BAGS";
            dt.Rows[row][header_BalAfterDeliveryInPcs] = balInPcs;


            if (balInPcs < 0)
            {
                dgvList.Rows[row].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Red;
                
            }
            else
            {
                dgvList.Rows[row].Cells[header_BalAfterDelivery].Style.ForeColor = Color.Black;
            }

            CheckIfAssemblyNeeded(dt);
        }

        private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvList;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if(rowIndex - 1 >= 0)
            {
                string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                if (cellValue == oldToDeliveryQty.ToString())
                {

                }
                else
                {
                    dataChanged = true;
                    btnUpdate.Visible = true;

                    //update balance
                    UpdateBalAfterDelivery(rowIndex);

                    //get priority level
                    string colName = dgv.Columns[colIndex].Name;

                    string priorityLvl = colName.Replace(header_Priority, "");

                    //get DO to delivery qty
                    string DOToDeliveryQtyInPcs = dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();
                    string POTblCode = dgv.Rows[rowIndex - 1].Cells[header_PriorityTblCode + priorityLvl].Value.ToString();

                    string deliveryStatus = text_DeliveryStatus_ToOpen;

                    int pcsPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out pcsPerBag) ? pcsPerBag : 1;

                    int newToDeliveryQty = int.TryParse(cellValue, out newToDeliveryQty) ? newToDeliveryQty : 0;


                    
                    if (cmbEditUnit.Text == text_Bag)
                    {
                        newToDeliveryQty *= pcsPerBag;
                    }

                    dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryPlanningInPCS + priorityLvl].Value = newToDeliveryQty;

                    //change delivery status color
                    if (int.TryParse(DOToDeliveryQtyInPcs, out int i))
                    {
                        AddToUpdateData(POTblCode, newToDeliveryQty);

                        if (DOToDeliveryQtyInPcs != "0")
                        {
                            if (DOToDeliveryQtyInPcs == newToDeliveryQty.ToString())
                            {
                                deliveryStatus = text_DeliveryStatus_Opened;
                                dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(0, 184, 148);
                            }
                            else
                            {
                                deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(52, 160, 225);
                                //dgv.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.White;
                            }
                        }
                        else
                        {
                            dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(253, 203, 110);

                        }
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                    }

                    dgv.Rows[rowIndex - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;
                    //MessageBox.Show("Different");
                }
            }
           
            
        }

        private void btnFillALL_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DataGridView dgv = dgvList;

            DataTable dt = (DataTable)dgv.DataSource;

            for(int i =0; i < dt.Rows.Count; i++)
            {
                int pcsPerBag = int.TryParse(dt.Rows[i][header_QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : -1;
                
                if (pcsPerBag != -1)
                {
                    int priorityLvl = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dgv.Columns[j].Name;

                        string searchingColumns = header_Priority + priorityLvl.ToString();

                        if (colName == searchingColumns)
                        {
                            //get order, delivered, pcs
                            int orderQty = int.TryParse(dt.Rows[i - 1][header_PriorityOrder + priorityLvl].ToString(), out orderQty) ? orderQty : 0;
                            int deliveredQty = int.TryParse(dt.Rows[i - 1][header_PriorityDelivered + priorityLvl.ToString()].ToString(), out deliveredQty) ? deliveredQty : 0;

                            string POTblCode = dt.Rows[i - 1][header_PriorityTblCode + priorityLvl.ToString()].ToString();

                            //calculate still need
                            int stillNeed = orderQty - deliveredQty;

                            if (stillNeed > 0)
                            {
                                dataChanged = true;
                                btnUpdate.Visible = true;

                                AddToUpdateData(POTblCode, stillNeed);

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed / pcsPerBag : stillNeed;

                                dt.Rows[i][searchingColumns] = stillNeed;

                                stillNeed = cmbEditUnit.Text == text_Bag ? stillNeed * pcsPerBag : stillNeed;

                                //get DO to delivery qty
                                string DOToDeliveryQtyInPcs = dgv.Rows[i - 1].Cells[header_PriorityToDeliveryQty + priorityLvl].Value.ToString();

                                string deliveryStatus = text_DeliveryStatus_ToOpen;

                                //change delivery status color
                                if (DOToDeliveryQtyInPcs != "0")
                                {
                                    if (DOToDeliveryQtyInPcs == stillNeed.ToString())
                                    {
                                        deliveryStatus = text_DeliveryStatus_Opened;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(0, 184, 148);
                                    }
                                    else
                                    {
                                        deliveryStatus = text_DeliveryStatus_OpenedWithDiff;
                                        dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(52, 160, 225);
                                    }
                                }
                                else
                                {
                                    dgv.Rows[i].Cells[searchingColumns].Style.BackColor = Color.FromArgb(253, 203, 110); 
                                }

                                dgv.Rows[i - 1].Cells[header_PriorityToDeliveryStatus + priorityLvl].Value = deliveryStatus;

                            }
                            priorityLvl++;
                        }
                    }

                    UpdateBalAfterDelivery(i);
                }
               

               
            }

            Cursor = Cursors.Arrow;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool dataUpdated = true;

            if(dt_ToUpdate != null)
            { 
                uSpp.Updated_By = MainDashboard.USER_ID;
                uSpp.Updated_Date = DateTime.Now;

                foreach (DataRow row in dt_ToUpdate.Rows)
                {
                    uSpp.Table_Code = int.TryParse(row[header_POTblCode].ToString(), out int i) ? i : 0;
                    uSpp.To_delivery_qty = int.TryParse(row[header_ToDeliveryPCSQty].ToString(), out  i) ? i : 0;

                    dataUpdated = dalSPP.POToDeliveryDataUpdate(uSpp);
                    if (!dataUpdated)
                    {
                        MessageBox.Show("Failed to update to delivery qty.\nPO Table Code: "+ uSpp.Table_Code +"\nTo Delivery Qty: "+ uSpp.To_delivery_qty);
                    }
                }
             
                if(dataUpdated)
                {
                    MessageBox.Show("All changed data updated!");
                }

                dataChanged = false;
                btnUpdate.Visible = false;
            }
          
            
        }

        private void frmSBBDeliveryPlanning_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dataChanged)
            {
                DialogResult dialogResult = MessageBox.Show("You have unsaved changes.\n Are you sure you want to leave this page?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                   
                }
                else
                {
                    e.Cancel = true;
                }
                  
            }
            
        }

        private void lblProductionAlert_Click(object sender, EventArgs e)
        {
            if(dt_Mat != null)
            {
                frmShowDataTable frm = new frmShowDataTable(dt_Mat)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();
            }
           else
            {
                MessageBox.Show("no data");
            }
        }
    }
}
