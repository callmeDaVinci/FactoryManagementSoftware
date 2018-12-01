using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
        }

        #region variable declare

        private int selectedOrderID = -1;
        static public string finalOrderNumber;
        static public string receivedNumber;
        static public bool receivedStockIn = false;
        static public bool receivedStockOut = false;
        static public bool orderApproved = false;

        #endregion

        #region create class object (database)
        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        #endregion

        #region Load or Reset Form

        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ordFormOpen = false;
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            resetForm();
            cmbStatusSearch.SelectedIndex = 0;
        }

        private void resetForm()
        {
            loadOrderRecord();
            txtOrdSearch.Clear();
        }
        
        private void loadOrderRecord()
        {

            string keywords = txtOrdSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                string statusSearch = cmbStatusSearch.Text;
                DataTable dt = dalOrd.Search(keywords);
                dt.DefaultView.Sort = "ord_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();
                dgvOrd.Rows.Clear();

                foreach (DataRow ord in sortedDt.Rows)
                {
                    if (statusSearch.Equals("ALL") || ord["ord_status"].ToString().Equals(statusSearch))
                    {
                        int n = dgvOrd.Rows.Add();
                        dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
                        dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
                        dgvOrd.Rows[n].Cells["ord_required_date"].Value = Convert.ToDateTime(ord["ord_required_date"]).ToString("dd/MM/yyyy"); ;
                        dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
                        dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
                        dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
                        dgvOrd.Rows[n].Cells["ord_pending"].Value = ord["ord_pending"].ToString();
                        dgvOrd.Rows[n].Cells["ord_received"].Value = ord["ord_received"].ToString();
                        dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
                        dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();
                    }
                }
            }
            else
            {
                //show all item from the database
                loadOrderRecord();
            }
            listPaint(dgvOrd);

        }

        private void refreshOrderRecord(int orderID)
        {
            loadOrderRecord();
            dgvOrd.ClearSelection();
            if (selectedOrderID != -1)
            {
                foreach (DataGridViewRow row in dgvOrd.Rows)
                {
                    if (Convert.ToInt32(row.Cells["ord_id"].Value.ToString()).Equals(orderID))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private void statusColorSet(DataGridView dgv, int rowIndex)
        {
            string value = dgv.Rows[rowIndex].Cells["ord_status"].Value.ToString();
            Color foreColor = this.dgvOrd.DefaultCellStyle.ForeColor;

            if (value.Equals("REQUESTING"))
            {
                foreColor = Color.FromArgb(251, 188, 5);
            }
            else if (value.Equals("CANCELLED"))
            {
                foreColor = Color.FromArgb(234, 67, 53);
            }
            else if (value.Equals("PENDING"))
            {
                foreColor = Color.FromArgb(66, 133, 244);
            }
            else if (value.Equals("RECEIVED"))
            {
                foreColor = Color.FromArgb(52, 168, 83)         ;
            }

            //dgv.Rows[rowIndex].Cells["ord_status"].Style = new DataGridViewCellStyle { ForeColor = SystemColors.Control,BackColor = foreColor };
            dgv.Rows[rowIndex].Cells["ord_status"].Style.ForeColor = foreColor;
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //dgv.RowTemplate.Height = 40;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if(dgv == dgvOrd)
                {
                    statusColorSet(dgv, n);
                }
                
            }
            dgv.ClearSelection();
        }

        private DataTable getOrderStatusTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status", typeof(string));
            dt.Rows.Add("Requesting");
            dt.Rows.Add("Cancelled");
            dt.Rows.Add("Approved");
            dt.Rows.Add("Received");
            return dt;
        }

        #endregion

        #region data Insert/Update/Search

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrderRequest frm = new frmOrderRequest();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//create new order

            if(frmOrderRequest.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                resetForm();
                frmOrderRequest.orderSuccess = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }   
        }

        private void txtOrdSearch_TextChanged(object sender, EventArgs e)
        {
            loadOrderRecord();
        }

        private void addOrderAction(string action, int orderID)
        {
            if(orderID < 0)//create a new action record after new order has created
            {
                //get the last record from tbl_ord
                DataTable lastRecord = dalOrd.lastRecordSelect();

                foreach (DataRow ord in lastRecord.Rows)
                {
                    uOrderAction.ord_id = Convert.ToInt32(ord["ord_id"]);
                    uOrderAction.added_date = Convert.ToDateTime(ord["ord_added_date"]);
                    uOrderAction.added_by = Convert.ToInt32(ord["ord_added_by"]); ;
                    uOrderAction.action = action;
                    uOrderAction.note = "";

                    if (!dalOrderAction.Insert(uOrderAction))
                    {
                        MessageBox.Show("Failed to create new action");
                    }
                }
            }
           else//action record exist,add another action
            {
                uOrderAction.ord_id = orderID;
                uOrderAction.added_date = DateTime.Now;
                uOrderAction.added_by = 0;
                uOrderAction.action = action;
                uOrderAction.note = "";

                if (!dalOrderAction.Insert(uOrderAction))
                {
                    MessageBox.Show("Failed to add new action");
                }
            }
        }

        #endregion

        #region order status change

        private void orderRequest(int orderID)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //get data from datagridview
            // item category, item code, item name, order id, 
            string id = "";
            string cat = "";
            string itemCode = "";
            string itemName = "";
            string qty = "";
            string unit = "";
            string date = "";
            DataTable dt = dalOrd.Select(orderID);

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow ord in dt.Rows)
                {
                    id = ord["ord_id"].ToString();
                    cat = ord["item_cat"].ToString();
                    itemCode = ord["ord_item_code"].ToString();
                    itemName = ord["item_name"].ToString();
                    qty = ord["ord_qty"].ToString();
                    unit = ord["ord_unit"].ToString();
                    date = ord["ord_required_date"].ToString();
                }
            }
            
            frmOrderRequest frm = new frmOrderRequest(id, cat, itemCode, itemName, qty, unit, date);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//order request

            if (frmOrderRequest.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                frmOrderRequest.orderSuccess = false;
                resetForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }

            refreshOrderRecord(selectedOrderID);

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void orderAprove(int rowIndex, int orderID)
        {
            //get data from datagridview
            string itemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
            string itemName = dgvOrd.Rows[rowIndex].Cells["item_name"].Value.ToString();
            string requiredDate = dgvOrd.Rows[rowIndex].Cells["ord_required_date"].Value.ToString();
            string qty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
            string unit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();

            //approve form
            frmOrderApprove frm = new frmOrderApprove(orderID.ToString(), requiredDate, itemName, itemCode, qty, unit);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(orderApproved)//if order approved from approve form, then change order status from requesting to pending
            {
                uOrd.ord_id = orderID;
                uOrd.ord_status = "PENDING";
                dalOrd.statusUpdate(uOrd);

                dalItem.orderAdd(itemCode, finalOrderNumber);//add order qty to item
                refreshOrderRecord(selectedOrderID);
                orderApproved = false;
            }
        }

        private void orderReceive(int rowIndex, int orderID)
        {
            //get data from datagridview
            string itemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
            string itemName = dgvOrd.Rows[rowIndex].Cells["item_name"].Value.ToString();
            string requiredDate = dgvOrd.Rows[rowIndex].Cells["ord_required_date"].Value.ToString();
            string qty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
            string received = dgvOrd.Rows[rowIndex].Cells["ord_received"].Value.ToString();
            string unit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();

            frmReceiveConfirm frm = new frmReceiveConfirm(orderID,itemCode,itemName,Convert.ToSingle(qty),Convert.ToSingle(received), unit);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//stock in

            if (receivedStockIn)//if order qty fully received
            {
                uOrd.ord_id = orderID;
                uOrd.ord_status = "RECEIVED";
                dalOrd.statusUpdate(uOrd);
                receivedStockIn = false;
            }

            if(!string.IsNullOrEmpty(receivedNumber))
            {
                dalItem.orderSubtract(itemCode, receivedNumber); //Updating data into database
            }

            refreshOrderRecord(selectedOrderID);
        }

        private void orderCancel(int rowIndex, int orderID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            bool receivedClear = true;//received qty = 0
            DialogResult dialogResult = MessageBox.Show("Are you sure want to cancel this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string itemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
                string itemName = dgvOrd.Rows[rowIndex].Cells["item_name"].Value.ToString();
                string requiredDate = dgvOrd.Rows[rowIndex].Cells["ord_required_date"].Value.ToString();
                string qty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
                string unit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();
                float received = Convert.ToSingle(dgvOrd.Rows[rowIndex].Cells["ord_received"].Value);
                string pending = dgvOrd.Rows[rowIndex].Cells["ord_pending"].Value.ToString();

                if (received > 0)//if have received record under this order ,then need to return this item from stock before cancel this order
                {
                    receivedClear = false;
                    dialogResult = MessageBox.Show(@"There is a received record under this order. please return this item from stock before canceling this order.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.OK)
                    {
                        frmReceiveCancel frm = new frmReceiveCancel(orderID, itemCode, itemName, received.ToString(), unit);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();//return item from stock

                        if (receivedStockOut)
                        {
                            if (!dalItem.updateTotalStock(itemCode))
                            {
                                MessageBox.Show("Failed to update total stock");
                            }
                            else
                            {
                                receivedClear = true;
                            }

                            receivedStockOut = false;
                        }
                    }
                }

                if (receivedClear)
                {
                    uOrd.ord_id = orderID;
                    uOrd.ord_required_date = Convert.ToDateTime(requiredDate);
                    uOrd.ord_qty = Convert.ToSingle(qty);
                    uOrd.ord_pending = 0;
                    uOrd.ord_received = 0;
                    uOrd.ord_updated_date = DateTime.Now;
                    uOrd.ord_updated_by = 0;
                    uOrd.ord_item_code = itemCode;
                    uOrd.ord_note = "";
                    uOrd.ord_unit = unit;

                    if (dalOrd.Update(uOrd))
                    {
                        if (!presentStatus.Equals("REQUESTING"))
                        {
                            dalItem.orderSubtract(itemCode, pending); //Updating data into database
                        }

                        dalOrderAction.orderCancel(orderID, "");

                        uOrd.ord_id = orderID;
                        uOrd.ord_status = "CANCELLED";
                        dalOrd.statusUpdate(uOrd);
                    }
                    refreshOrderRecord(selectedOrderID);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region  click / selected index changed action

        //handle the row selection on right click
        private void dgvOrd_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvOrd.CurrentCell = dgvOrd.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvOrd.Rows[e.RowIndex].Selected = true;
                dgvOrd.Focus();
                int rowIndex = dgvOrd.CurrentCell.RowIndex;

                try
                {
                    string result = dgvOrd.Rows[rowIndex].Cells["ord_status"].Value.ToString();

                    if (result.Equals("REQUESTING"))
                    {
                        my_menu.Items.Add("Approve").Name = "Approve";
                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }
                    else if (result.Equals("CANCELLED"))
                    {
                        my_menu.Items.Add("Request").Name = "Request";
                    }
                    else if (result.Equals("PENDING"))
                    {
                        my_menu.Items.Add("Receive").Name = "Receive";
                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }
                    else if (result.Equals("RECEIVED"))
                    {
                        my_menu.Items.Add("Cancel").Name = "Cancel";
                    }

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        //handle what will happen when item clicked after right click on datagridview
        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            DataGridView dgv = dgvOrd;
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int orderID = Convert.ToInt32(dgv.Rows[rowIndex].Cells["ord_id"].Value);
            string presentStatus = dgv.Rows[rowIndex].Cells["ord_status"].Value.ToString();

            contextMenuStrip1.Hide();
            if(itemClicked.Equals("Request"))
            {
                orderRequest(orderID);
                //from order request form
            }
            else if (itemClicked.Equals("Approve"))
            {
                orderAprove(rowIndex, orderID);
            }
            else if (itemClicked.Equals("Cancel"))
            {
                orderCancel(rowIndex, orderID, presentStatus);
            }
            else if (itemClicked.Equals("Receive"))
            {
                orderReceive(rowIndex, orderID);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        //show record action record when double click on datagridview
        private void dgvOrd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgvOrd.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                int orderID = Convert.ToInt32(dgvOrd.Rows[rowIndex].Cells["ord_id"].Value);

                DataTable dt = dalOrderAction.Select(orderID);
                if (dt.Rows.Count > 0)
                {
                    frmOrderActionHistory frm = new frmOrderActionHistory(dt);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit
                }
                else
                {
                    MessageBox.Show("No action record under this order yet.");
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        //clear selection when mouse click on empty space
        private void dgvOrd_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvOrd.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvOrd.ClearSelection();
            }
        }
        
        //clear selection when mouse click on empty space
        private void frmOrder_Click(object sender, EventArgs e)
        {
            dgvOrd.ClearSelection();
        }

        //input text search
        private void cmbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadOrderRecord();
        }

        #endregion


    }
}
