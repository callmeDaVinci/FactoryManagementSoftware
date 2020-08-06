using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDeliverySchedule : Form
    {
        public frmDeliverySchedule()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvMainList, true);
            tool.DoubleBuffered(dgvSubList, true);

            btnFilter.Text = text_HideFilter;


         
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

        readonly string text_EditDeliveryDate = "EDIT DELIVERY DATE";
       
        readonly string text_RemoveTrip = "TRIP REMOVE";
        readonly string text_UndoRemoved = "UNDO REMOVED";
        readonly string text_TripConfirm = "TRIP CONFIRM";
        readonly string text_CancelTrip = "CANCEL TRIP";
        readonly string text_OpenDO = "OPEN D/O";

        readonly string text_EditTrip = "TRIP # EDIT";
        readonly string text_EditStop = "STOP # EDIT";

        readonly string text_OrderDelivered = "ORDER DELIVERED";
        readonly string text_UndoDelivered = "UNDO DELIVERED";

        readonly string text_SelectDeliveryDate = "SELECT DELIVERY DATE";
        //readonly string text_CancelTrip = "CANCEL TRIP";

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string text_TotalBagString = " BAG(s) SELECTED";

        readonly string header_Status = "STATUS";
        readonly string header_TripNo = "TRIP #";
        readonly string header_StopNo = "STOP #";
        readonly string header_DeliveryDate = "DELIVERY DATE";
        readonly string header_Route = "ROUTE";
        readonly string header_RouteName = "ROUTE NAME";
        readonly string header_RouteTblCode = "ROUTE TBL CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_CustomerName = "CUSTOMER NAME";
        readonly string header_PONo = "P/O NO";
        readonly string header_POTableCode = "P/O TBL CODE";
        readonly string header_POCode = "P/O CODE";
        readonly string header_Pcs = "PCS";
        readonly string header_Bags = "BAG(S)";
        readonly string header_Total = "TOTAL";
        readonly string header_PlanningNo = "PLANNING NO.";

        readonly string header_Size = "SIZE";
        readonly string header_SizeString = "SIZE STRING";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_ItemName = "ITEM NAME";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_PcsPerBag = "PCS PER BAG";

        readonly string header_DeliveredPcs = "DELIVERED PCS";
        readonly string header_OrderedPcs = "ORDERED PCS";

        private bool Loaded = false;
        #endregion

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Status, typeof(string));
            dt.Columns.Add(header_TripNo, typeof(int));
            dt.Columns.Add(header_StopNo, typeof(int));
            dt.Columns.Add(header_DeliveryDate, typeof(DateTime));

            dt.Columns.Add(header_RouteName, typeof(string));
            dt.Columns.Add(header_RouteTblCode, typeof(int));
            dt.Columns.Add(header_Route, typeof(string));

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));
            dt.Columns.Add(header_POTableCode, typeof(int));
            dt.Columns.Add(header_PlanningNo, typeof(int));

            dt.Columns.Add(header_CustomerName, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));

            dt.Columns.Add(header_Bags, typeof(int));
            dt.Columns.Add(header_Pcs, typeof(int));
            dt.Columns.Add(header_Total, typeof(int));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Unit, typeof(int));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_PcsPerBag, typeof(int));

            dt.Columns.Add(header_DeliveredPcs, typeof(int));
            dt.Columns.Add(header_OrderedPcs, typeof(int));
            return dt;
        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpDeliveryList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpDeliveryList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_RouteName].Visible = false;
            dgv.Columns[header_RouteTblCode].Visible = false;

            dgv.Columns[header_POCode].Visible = false;
            dgv.Columns[header_PONo].Visible = false;
            dgv.Columns[header_POTableCode].Visible = false;

            dgv.Columns[header_CustomerName].Visible = false;
            dgv.Columns[header_Pcs].Visible = false;

            dgv.Columns[header_Size].Visible = false;
            dgv.Columns[header_SizeString].Visible = false;
            dgv.Columns[header_Unit].Visible = false;
            dgv.Columns[header_Type].Visible = false;
            dgv.Columns[header_PlanningNo].Visible = false;
            dgv.Columns[header_ItemName].Visible = false;
            dgv.Columns[header_ItemCode].Visible = false;

            dgv.Columns[header_DeliveredPcs].Visible = false;
            dgv.Columns[header_OrderedPcs].Visible = false;
            dgv.Columns[header_PcsPerBag].Visible = false;

            //dgv.Columns[header_Code].DefaultCellStyle.ForeColor = Color.Gray;
            //dgv.Columns[header_Code].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[header_Route].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            dgv.Columns[header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.Columns[header_Total].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[header_Bags].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_TripNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_StopNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_DeliveryDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Route].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;


        }

        

        private void TripConfirm()
        {
            AllocateTripNumber();
        }

        private void AllocateTripNumber()
        {
            DataTable dt = (DataTable)dgvMainList.DataSource;

            //get max trip number
            DataTable dt_Copy = dt.Copy();

            int maxTripNo = 0;

            foreach(DataRow row in dt_Copy.Rows)
            {
                int tripNo = int.TryParse(row[header_TripNo].ToString(), out tripNo) ? tripNo : -1;

                if(tripNo > maxTripNo)
                {
                    maxTripNo = tripNo;
                }
            }

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dgvMainList.Rows[i].Selected)
                {
                    DateTime deliveryDate = DateTime.TryParse(dt.Rows[i][header_DeliveryDate].ToString(), out deliveryDate) ? deliveryDate.Date : DateTime.MaxValue;
                    int tripData = int.TryParse(dt.Rows[i][header_TripNo].ToString(), out tripData) ? tripData : -1;

                    if(deliveryDate != DateTime.MaxValue)
                    {
                        dt.Rows[i][header_Status] = text.Delivery_DOPending;
                        dgvMainList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        dgvMainList.Rows[i].Cells[header_Status].Style.BackColor = Color.FromArgb(253, 203, 110);

                        if (tripData == -1)
                        {
                            maxTripNo++;
                            dt.Rows[i][header_TripNo] = maxTripNo;
                            int planningNo = int.TryParse(dt.Rows[i][header_PlanningNo].ToString(), out planningNo) ? planningNo : -1;
                            int poCode = int.TryParse(dt.Rows[i][header_POCode].ToString(), out poCode) ? poCode : -1;

                            uSpp.planning_no = planningNo;
                            uSpp.PO_code = poCode;
                            uSpp.trip_no = maxTripNo;
                            uSpp.delivery_status = text.Delivery_DOPending;

                            if (!dalSPP.DeliveryTripNumberAndStatusUpdate(uSpp))
                            {
                                MessageBox.Show("Failed to update trip number data!");
                                break;
                            }
                        }
                       
                    }
                }
            }
        }

        private bool CheckIfTripReadyToConfirm()
        {
            DataTable dt = (DataTable)dgvMainList.DataSource;

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (dgvMainList.Rows[i].Selected)
                {
                    if(!DateTime.TryParse(dt.Rows[i][header_DeliveryDate].ToString(), out DateTime test))
                        return false;

                    if (dt.Rows[i][header_Status].ToString() != text.Delivery_Processing)
                        return false;
                }
               
            }
            return true;
        }

        private void LoadDeliverySchedule()
        {
            Loaded = false;
            //get data from DB
            DataTable dt_DeliveryData = dalSPP.DeliveryWithInfoSelect();
            DataTable dt_Schedule = NewScheduleTable();

            //filter data
            foreach(DataRow row in dt_DeliveryData.Rows)
            {
                string deliveryTblCode = row[dalSPP.TableCode].ToString();

                if(!string.IsNullOrEmpty(deliveryTblCode))
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                    bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;

                    int pcsPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : 1;
                    int toDeliverQty = int.TryParse(row[dalSPP.DeliverPcs].ToString(), out toDeliverQty) ? toDeliverQty : 0;
                    int planningNo = int.TryParse(row[dalSPP.PlanningNo].ToString(), out planningNo) ? planningNo : 0;

                    int tripNo = int.TryParse(row[dalSPP.TripNo].ToString(), out tripNo) ? tripNo : 0;

                    int deliverBag = toDeliverQty / pcsPerBag;

                    DateTime deliveryDate = DateTime.TryParse(row[dalSPP.DeliveryDate].ToString(), out deliveryDate) ? deliveryDate : DateTime.MaxValue;

                    string custShortName = row[dalSPP.ShortName].ToString();
                    string poNo = row[dalSPP.PONo].ToString();
                    int poCode = Convert.ToInt32(row[dalSPP.POCode].ToString());
                    int poTblCode = Convert.ToInt32(row[dalSPP.POTableCode].ToString());
                    int OrderedQty = Convert.ToInt32(row[dalSPP.POQty].ToString());
                    int deliveredQty = Convert.ToInt32(row[dalSPP.DeliveredQty].ToString());
                    int routeTblCode = Convert.ToInt32(row[dalSPP.RouteTblCode].ToString());
                    string itemCode = row[dalSPP.ItemCode].ToString();
                    string itemName = row[dalSPP.ItemName].ToString();
                    string routeName = row[dalSPP.RouteName].ToString();

                    DataRow scheduleRow = dt_Schedule.NewRow();

                    scheduleRow[header_Status] = row[dalSPP.DeliveryStatus].ToString();
                    scheduleRow[header_Customer] = "   "+custShortName + "(" + poNo + ")   ";
                    scheduleRow[header_CustomerName] = custShortName;
                    scheduleRow[header_Pcs] = toDeliverQty;
                    scheduleRow[header_PcsPerBag] = pcsPerBag;
                    scheduleRow[header_Bags] = deliverBag;

                    scheduleRow[header_PONo] = poNo;
                    scheduleRow[header_POCode] = poCode;
                    scheduleRow[header_POTableCode] = poTblCode;
                    scheduleRow[header_PlanningNo] = planningNo;

                    scheduleRow[header_OrderedPcs] = OrderedQty;
                    scheduleRow[header_DeliveredPcs] = deliveredQty;
                    scheduleRow[header_ItemCode] = itemCode;
                    scheduleRow[header_ItemName] = itemName;

                    scheduleRow[header_RouteTblCode] = routeTblCode;
                    scheduleRow[header_RouteName] = routeName;
                    scheduleRow[header_Route] = "   ("+ routeTblCode+")"+routeName+"   ";

                    if(deliveryDate != DateTime.MaxValue)
                    {
                        scheduleRow[header_DeliveryDate] = deliveryDate.Date;
                    }
                    if (tripNo != 0 && tripNo != 9999)
                    {
                        scheduleRow[header_TripNo] = tripNo;
                    }

                    dt_Schedule.Rows.Add(scheduleRow);

                }

            }
            //merge data

            //set data source to dgv
            //edit dgv style

            dgvMainList.DataSource = MergeSamePOSamePlanningNo(dt_Schedule);
            DgvUIEdit(dgvMainList);
            MainListUIEdit();
            dgvMainList.ClearSelection();

            Loaded = true;
        }

        private DataTable MergeSamePOSamePlanningNo(DataTable dt)
        {
            DataTable dt_Merge = NewScheduleTable();

            int totalBags = 0;
            int mergeRowIndex = -1;
            
            string prePOCode = null;
            string prePlanningNo = null;

            foreach(DataRow row in dt.Rows)
            {
                string POCode = row[header_POCode].ToString();
                string PlanningNo = row[header_PlanningNo].ToString();
                int deliverBag = int.TryParse(row[header_Bags].ToString(), out deliverBag) ? deliverBag : 0;

                //if(prePOCode == null && prePlanningNo == null)
                //{
                //    prePOCode = POCode;
                //    prePlanningNo = PlanningNo;
                //}

                if(prePlanningNo == PlanningNo)
                {
                    if(prePOCode == POCode)
                    {
                        totalBags += deliverBag;
                        dt_Merge.Rows[mergeRowIndex][header_Bags] = totalBags;

                    }
                    else
                    {
                        prePOCode = POCode;
                        totalBags = deliverBag;

                        dt_Merge.ImportRow(row);
                        mergeRowIndex++;
                    }
                }
                else
                {
                    prePOCode = POCode;
                    prePlanningNo = PlanningNo;

                    totalBags = deliverBag;
                    dt_Merge.ImportRow(row);
                    mergeRowIndex++;
                }

            }

            //DataView dv = dt_Merge.DefaultView;
            //dv.Sort = header_RouteTblCode+" ASC";
            //DataTable sortedDT = dv.ToTable();

            return dt_Merge;
        }

        private void LoadSalesReport()
        {
            //DataTable dt_DO = dalSPP.DOWithTrfInfoSelect();
            //start = dtpFrom.Value.ToString("yyyy/MM/dd");
            //end = dtpTo.Value.ToString("yyyy/MM/dd");

        }

        private void frmDeliverySchedule_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();

            if (dalUser.getPermissionLevel(MainDashboard.USER_ID) <= 3)
            {
                tlpDeliveryList.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
            }

            LoadDeliverySchedule();
            LoadSalesReport();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainListUIEdit()
        {
            DataGridView dgv = dgvMainList;
            dgv.SuspendLayout();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Columns[j].Name == header_Status)
                    {
                        if (dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_Processing)
                        {
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                        }
                        else
                        {
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            if (dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_DOOpened)
                            {
                                dgv.Rows[i].Cells[header_Status].Style.BackColor = Color.White;

                            }
                            else if (dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_DOPending)
                            {
                                dgv.Rows[i].Cells[header_Status].Style.BackColor = Color.FromArgb(253, 203, 110);
                            }
                            else if (dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_ToDeliver)
                            {
                                dgv.Rows[i].Cells[header_Status].Style.BackColor = Color.FromArgb(52, 139, 209);
                            }
                        }

                    }
                }
            }

            dgv.ResumeLayout();

            dgv.ClearSelection();
        }

        private void dgvMainList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           
        }

        private void CalculateTotalBag(DataGridView dgv)
        {

            DataTable dt = (DataTable)dgv.DataSource;

            int totalBag = 0;

            if(dt.Rows.Count > 0 && Loaded)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Selected)
                    {
                        int selectedBag = int.TryParse(dt.Rows[i][header_Bags].ToString(), out selectedBag) ? selectedBag : 0;

                        totalBag += selectedBag;
                    }
                }
            }
          

            lblTotalBagSelected.Text = totalBag + text_TotalBagString;

        }

        private void dgvMainList_MouseClick(object sender, MouseEventArgs e)
        {
            CalculateTotalBag(dgvMainList);
        }

        private void EditDeliveryDate()
        {
            frmDeliveryDate frm = new frmDeliveryDate(text_SelectDeliveryDate)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();//Item Edit

            if (frmDeliveryDate.selectedDate != DateTime.MaxValue)
            {
                //DateTime selectedDate = DateTime.Now;
                DateTime selectedDate = frmDeliveryDate.selectedDate;
                DataGridView dgv = dgvMainList;

                DataTable dt = (DataTable)dgv.DataSource;

                uSpp.Updated_Date = DateTime.Now;
                uSpp.Updated_By = MainDashboard.USER_ID;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool rowSelected = dgv.Rows[i].Selected;

                    if(rowSelected)
                    {
                        if (selectedDate == DateTimePicker.MinimumDateTime || selectedDate == null)
                        {
                            dgv.Rows[i].Cells[header_DeliveryDate].Value = "";
                            uSpp.delivery_date = null;
                        }
                        else
                        {
                            dgv.Rows[i].Cells[header_DeliveryDate].Value = selectedDate.Date;
                            uSpp.delivery_date = selectedDate.Date;
                        }

                        //update
                        int planningNo = int.TryParse(dt.Rows[i][header_PlanningNo].ToString(), out planningNo) ? planningNo : -1;
                        int poCode = int.TryParse(dt.Rows[i][header_POCode].ToString(), out poCode) ? poCode : -1;

                        uSpp.planning_no = planningNo;
                        uSpp.PO_code = poCode;

                        if(!dalSPP.DeliveryTripDateUpdate(uSpp))
                        {
                            MessageBox.Show("Failed to update delivery data!");
                            break;
                        }
                    }
                }
            }
           
        }

        private void AddOptionToMenu(ContextMenuStrip my_menu)
        {
            DataGridView dgv = dgvMainList;

            DataTable dt = (DataTable)dgv.DataSource;

            //bool multiSelected = false;
            bool allSameStatus = true;

            string preStatus = null;

            int selectedRowNo = 0;

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowSelected = dgv.Rows[i].Selected;

                if(rowSelected)
                {
                    selectedRowNo++;

                    string status = dt.Rows[i][header_Status].ToString();

                    if (preStatus == null)
                    {
                        preStatus = status;
                    }

                    if (preStatus != status)
                    {
                        allSameStatus = false;
                        break;
                    }

                    
                }
            }

            my_menu.Items.Add(text_EditDeliveryDate).Name = text_EditDeliveryDate;

            if (allSameStatus)
            {
                if(preStatus == text.Delivery_Processing)
                {
                    if (CheckIfTripReadyToConfirm())
                    {
                        my_menu.Items.Add(text_TripConfirm).Name = text_TripConfirm;
                    }

                    if(selectedRowNo == 1)
                    {
                        my_menu.Items.Add(text_RemoveTrip).Name = text_RemoveTrip;
                    }
                }
                else if (preStatus == text.Delivery_DOPending)
                {
                    my_menu.Items.Add(text_OpenDO).Name = text_OpenDO;

                    if (selectedRowNo == 1)
                    {
                        my_menu.Items.Add(text_EditTrip).Name = text_EditTrip;
                        my_menu.Items.Add(text_EditStop).Name = text_EditStop;
                        my_menu.Items.Add(text_CancelTrip).Name = text_CancelTrip;
                    }
                }
                else if (preStatus == text.Delivery_DOOpened || preStatus == text.Delivery_ToDeliver)
                {
                    my_menu.Items.Add(text_OrderDelivered).Name = text_OrderDelivered;

                    if (selectedRowNo == 1)
                    {
                        my_menu.Items.Add(text_EditTrip).Name = text_EditTrip;
                        my_menu.Items.Add(text_EditStop).Name = text_EditStop;
                        my_menu.Items.Add(text_CancelTrip).Name = text_CancelTrip;
                    }
                }
                else if (preStatus == text.Delivery_Delivered)
                {
                    my_menu.Items.Add(text_UndoDelivered).Name = text_UndoDelivered;
                }
                else if (preStatus == text.Delivery_Removed)
                {
                    my_menu.Items.Add(text_UndoRemoved).Name = text_UndoRemoved;
                }
            }
            

            

            
        }

        private void dgvMainList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = dgvMainList;

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();

                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //Can leave these here -doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;

                    dgv.Focus();

                    AddOptionToMenu(my_menu);

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(List_ItemClicked);

                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void List_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvMainList;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();

                    if (ClickedItem.Equals(text_EditDeliveryDate))
                    {
                        EditDeliveryDate();
                    }
                    else if(ClickedItem.Equals(text_TripConfirm))
                    {
                        TripConfirm();
                    }
                    else if (ClickedItem.Equals(text_RemoveTrip))
                    {
                        TripConfirm();
                    }
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void TripRemove(int rowIndex)
        {
            //change status
            //dgvMainList.Rows[rowIndex].Cells[header_Status].Value;
        }

        private void dgvMainList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = dgvMainList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if(row >= 0 )
            {
                if(dgv.Columns[col].Name == header_TripNo)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.Rows[row].Cells[col].Selected = true;

                    if(int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out int x))
                    {
                        dgv.ReadOnly = false;
                    }
                    else
                    {
                        dgv.ReadOnly = true;
                    }
                    
                }
                else
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = true;
                }
                
            }
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

        private void dgvMainList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int col = dgvMainList.CurrentCell.ColumnIndex;
                int row = dgvMainList.CurrentCell.RowIndex;

                int tripData = int.TryParse(dgvMainList.Rows[row].Cells[col].Value.ToString(), out tripData) ? tripData : -1;

                if (dgvMainList.Columns[col].Name == header_TripNo && tripData != -1) //Desired Column
                {
                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    if (e.Control is TextBox tb)
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

        private void dgvMainList_CurrentCellChanged(object sender, EventArgs e)
        {

            DataGridView dgv = dgvMainList;

            if(dgv.CurrentCell != null)
            {
                int row = dgv.CurrentCell.RowIndex;
                int col = dgv.CurrentCell.ColumnIndex;

                if (row >= 0)
                {
                    if (dgv.Columns[col].Name == header_TripNo)
                    {
                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.Rows[row].Cells[col].Selected = true;

                        if (int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out int x))
                        {
                            dgv.ReadOnly = false;
                        }
                        else
                        {
                            dgv.ReadOnly = true;
                        }

                    }
                    else
                    {
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.Rows[row].Cells[col].Selected = true;
                        dgv.ReadOnly = true;
                    }

                }

                CalculateTotalBag(dgvMainList);
            }
            
        }

        private void dgvMainList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMainList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            int tripData = int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out tripData) ? tripData : -1;

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;

            if (tripData != -1)
            {
                int planningNo = int.TryParse(dgv.Rows[row].Cells[header_PlanningNo].Value.ToString(), out planningNo) ? planningNo : -1;
                int poCode = int.TryParse(dgv.Rows[row].Cells[header_POCode].Value.ToString(), out poCode) ? poCode : -1;

                uSpp.planning_no = planningNo;
                uSpp.PO_code = poCode;
                uSpp.trip_no = tripData;

                if (!dalSPP.DeliveryTripNumberUpdate(uSpp))
                {
                    MessageBox.Show("Failed to update trip number data!");
                }
            }
            
           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDeliverySchedule();
        }
    }
}
