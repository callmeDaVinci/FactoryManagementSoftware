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
        private string presentValue = "";
        private int selectedOrderID = -1;
        static public string selectedOrderQty = "";
        static public string selectedItemCode = "";
        static public string selectedUnit = "";
        static public string selectedTo = "";
        static public string receivedLocation;
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
        }

        private void resetForm()
        {
            loadOrderRecord();
            txtOrdSearch.Clear();
        }
        
        private void loadOrderRecord()
        {
            DataTable dt = dalOrd.Select();
            dt.DefaultView.Sort = "ord_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();
            dgvOrd.Rows.Clear();
            //((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;

            foreach (DataRow ord in sortedDt.Rows)
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
            frmOrderInput frm = new frmOrderInput();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(frmOrderInput.orderSuccess)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                resetForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }   
        }

        private void dgvOrd_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combobox = e.Control as ComboBox;

            if (combobox != null)
            {
                combobox.SelectedIndexChanged -= new EventHandler(SelectedIndexChanged);

                combobox.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);

                e.CellStyle.BackColor = this.dgvOrd.DefaultCellStyle.BackColor;
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            bool orderStatusUpdate = false;
            ComboBox combo = sender as ComboBox;
            string selectedItem = combo.SelectedItem.ToString();

            if (!presentValue.Equals(selectedItem))//if selected text change(before!=after)
            {

                uOrd.ord_id = selectedOrderID;
                uOrd.ord_status = selectedItem;

                if (selectedItem.Equals("Received"))
                {
                    frmReceiveConfirm frm = new frmReceiveConfirm();
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (presentValue.Equals("Requesting") || presentValue.Equals("Cancelled"))
                    {
                        MessageBox.Show("This order not approve yet!");                     
                    }
                    else if (presentValue.Equals("Pending"))
                    {
                        frm.ShowDialog();//stock in

                        if(receivedStockIn)
                        {
                            if(!dalItem.updateTotalStock(selectedItemCode))
                            {
                                MessageBox.Show("Failed to update total stock");
                            }
                            else
                            {
                                orderStatusUpdate = true;
                                orderSubtract(selectedItemCode, selectedOrderQty);
                            }
                            receivedStockIn = false;
                        }               
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to change the order status?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        switch (selectedItem)
                        {
                            case "Cancelled":
                                if (presentValue.Equals("Pending"))
                                {
                                    orderSubtract(selectedItemCode, selectedOrderQty);
                                    //dalOrd.Update(uOrd);
                                    orderStatusUpdate = true;
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    //dialog stock out
                                    cancelReceive(selectedItem);
                                }
                                else if(presentValue.Equals("Requesting"))
                                {
                                    //dalOrd.Update(uOrd);
                                    orderStatusUpdate = true;
                                }
                                break;

                            case "Pending":
                                if (presentValue.Equals("Requesting"))
                                {
                                    orderAdd(selectedItemCode, selectedOrderQty);
                                    //dalOrd.Update(uOrd);
                                    orderStatusUpdate = true;
                                }
                                else if (presentValue.Equals("Cancelled"))
                                {
                                    orderAdd(selectedItemCode, selectedOrderQty);
                                    //dalOrd.Update(uOrd);
                                    orderStatusUpdate = true;
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    cancelReceive(selectedItem);
                                }
                                break;

                            case "Requesting":
                                if (presentValue.Equals("Pending"))
                                {
                                    orderSubtract(selectedItemCode, selectedOrderQty);
                                    //dalOrd.Update(uOrd);
                                    orderStatusUpdate = true;
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    cancelReceive(selectedItem);
                                }
                                else if (presentValue.Equals("Cancelled"))
                                {
                                    //dalOrd.Update(uOrd);
                                    orderStatusUpdate = true;
                                }
                                break;

                            default:
                                break;
                        }
                        
                    }
                }

                if(orderStatusUpdate)
                {
                    dalOrd.statusUpdate(uOrd);
                    orderStatusUpdate = false;
                }
                refreshOrderRecord(selectedOrderID);
            }
        }

        private void dgvOrd_CellEnter(object sender, DataGridViewCellEventArgs e)//activate combobox on first click
        {

            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                int rowIndex = e.RowIndex;
                presentValue = dgvOrd.Rows[rowIndex].Cells["ord_status"].Value.ToString();
                selectedOrderID = Convert.ToInt32(dgvOrd.Rows[rowIndex].Cells["ord_id"].Value.ToString());
                selectedItemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
                selectedOrderQty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
                selectedUnit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();

                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;

                //MessageBox.Show(presentValue);

            }
        }

        private void txtOrdSearch_TextChanged(object sender, EventArgs e)
        {
            //string keywords = txtOrdSearch.Text;

            ////check if the keywords has value or not
            //if (keywords != null)
            //{
            //    DataTable dt = dalOrd.Search(keywords);
            //    dt.DefaultView.Sort = "ord_added_date DESC";
            //    DataTable sortedDt = dt.DefaultView.ToTable();
            //    dgvOrd.Rows.Clear();
            //    ((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;

            //    foreach (DataRow ord in sortedDt.Rows)
            //    {
            //        int n = dgvOrd.Rows.Add();
            //        dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
            //        dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
            //        dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
            //        dgvOrd.Rows[n].Cells["item_ord"].Value = ord["item_ord"].ToString();
            //        dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
            //        dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
            //        dgvOrd.Rows[n].Cells["ord_forecast_date"].Value = Convert.ToDateTime(ord["ord_forecast_date"]).ToString("dd/MM/yyyy"); ;
            //        dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
            //        dgvOrd.Rows[n].Cells["ord_added_by"].Value = ord["ord_added_by"].ToString();
            //        dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();
            //    }

            //}
            //else
            //{
            //    //show all item from the database
            //    loadOrderRecord();
            //}
            //listPaint(dgvOrd);
        }

        private void cancelReceive(string selectedItem)
        {
           
        }

        private void orderAdd(string itemCode, string ordQty)
        {
            bool success = dalItem.orderAdd(itemCode, ordQty);//Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to updated item");//failed to update user
            }
        }

        private void orderSubtract(string itemCode, string ordQty)
        {
            bool success = dalItem.orderSubtract(itemCode, ordQty); //Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to updated item");//failed to update user
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void addOrderAction(string action, int orderID)
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

        #endregion

        #region order status change

        private void orderRequest(int orderID, string presentStatus)
        {
            uOrd.ord_id = orderID;
            uOrd.ord_status = "REQUESTING";
            dalOrd.statusUpdate(uOrd);
            refreshOrderRecord(selectedOrderID);
        }

        private void orderAprove(int rowIndex, int orderID, string presentStatus)
        {
            string itemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
            string itemName = dgvOrd.Rows[rowIndex].Cells["item_name"].Value.ToString();
            string requiredDate = dgvOrd.Rows[rowIndex].Cells["ord_required_date"].Value.ToString();
            string qty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
            string unit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();

            frmOrderApprove frm = new frmOrderApprove(orderID.ToString(), requiredDate, itemName, itemCode, qty, unit);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(orderApproved)
            {
                uOrd.ord_id = orderID;
                uOrd.ord_status = "PENDING";
                dalOrd.statusUpdate(uOrd);

                orderAdd(itemCode, qty);
                refreshOrderRecord(selectedOrderID);
            }
           
        }

        private void orderCancel(int rowIndex, int orderID, string presentStatus)
        {
            bool receivedClear = true;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to cancel this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string itemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
                string itemName = dgvOrd.Rows[rowIndex].Cells["item_name"].Value.ToString();
                string requiredDate = dgvOrd.Rows[rowIndex].Cells["ord_required_date"].Value.ToString();
                string qty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
                string unit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();
                float received = Convert.ToSingle(dgvOrd.Rows[rowIndex].Cells["ord_received"].Value);

                if (received > 0)
                {
                    receivedClear = false;
                    dialogResult = MessageBox.Show(@"There is a received record under this order. please stock it out before canceling this order.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.OK)
                    {
                        frmReceiveCancel frm = new frmReceiveCancel(orderID,itemCode,itemName,received.ToString(),unit);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();//stock out

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

                if(receivedClear)
                {
                    uOrd.ord_id = orderID;
                    uOrd.ord_required_date = Convert.ToDateTime(requiredDate);
                    uOrd.ord_qty = Convert.ToSingle(qty);
                    uOrd.ord_pending = 0;
                    uOrd.ord_received = 0;

                    if (dalOrd.Update(uOrd))
                    {
                        orderSubtract(itemCode, qty);
                        string action = "Order Cancel";
                        addOrderAction(action, orderID);
                        uOrd.ord_id = orderID;
                        uOrd.ord_status = "CANCELLED";
                        dalOrd.statusUpdate(uOrd);
                    }
                    refreshOrderRecord(selectedOrderID);
                }
                
            }
                
        }

        private void orderReceive(int orderID, string presentStatus)
        {
            uOrd.ord_id = orderID;
            uOrd.ord_status = "RECEIVED";
            dalOrd.statusUpdate(uOrd);
            refreshOrderRecord(selectedOrderID);
        }

        #endregion

        #region  click action

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

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvOrd;
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int orderID = Convert.ToInt32(dgv.Rows[rowIndex].Cells["ord_id"].Value);
            string presentStatus = dgv.Rows[rowIndex].Cells["ord_status"].Value.ToString();
            
            if(itemClicked.Equals("Request"))
            {
                orderRequest(orderID, presentStatus);
            }
            else if (itemClicked.Equals("Approve"))
            {
                orderAprove(rowIndex, orderID, presentStatus);
            }
            else if (itemClicked.Equals("Cancel"))
            {
                orderCancel(rowIndex, orderID, presentStatus);
            }
            else if (itemClicked.Equals("Receive"))
            {
                orderReceive(orderID, presentStatus);
            }

        }

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

        private void dgvOrd_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvOrd.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvOrd.ClearSelection();
            }
        }

        private void frmOrder_Click(object sender, EventArgs e)
        {
            dgvOrd.ClearSelection();
        }

        #endregion
    }
}
