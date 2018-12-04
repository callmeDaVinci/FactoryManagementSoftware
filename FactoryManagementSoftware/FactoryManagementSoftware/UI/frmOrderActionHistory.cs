using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderActionHistory : Form
    {
        public frmOrderActionHistory(int orderID)
        {
            InitializeComponent();
            loadActionHistory(orderID);
            dgvAction.ClearSelection();
        }

        static public bool editSuccess = false;
        #region class object

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facDAL dalFac = new facDAL();

        ordBLL uOrder = new ordBLL();
        ordDAL dalOrder = new ordDAL();

        itemDAL dalItem = new itemDAL();

        #endregion

        private int transferRecord(string stockResult,string itemCode, string from, string qty, string unit)
        {

            utrfHist.trf_hist_item_code = itemCode;
            utrfHist.trf_hist_from = from;
            utrfHist.trf_hist_to = "Other";
            utrfHist.trf_hist_qty = Convert.ToSingle(qty);
            utrfHist.trf_hist_unit = unit;
            utrfHist.trf_hist_trf_date = DateTime.Now;
            utrfHist.trf_hist_note = "Order: Received Undo";
            utrfHist.trf_hist_added_date = DateTime.Now;
            utrfHist.trf_hist_added_by = 0;
            utrfHist.trf_result = stockResult;
            utrfHist.trf_hist_from_order = 1;
            //Inserting Data into Database
            bool success = daltrfHist.Insert(utrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new transfer record");
            }
            return daltrfHist.getIndexNo(utrfHist);
        }

        private string getFactoryID(string factoryName)
        {
            string factoryID = "";

            DataTable dtFac = dalFac.nameSearch(factoryName);

            foreach (DataRow fac in dtFac.Rows)
            {
                factoryID = fac["fac_id"].ToString();
            }
            return factoryID;
        }

        private bool IfExists(string itemCode, string factoryName)
        {

            DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private float getQty(string itemCode, string factoryName)
        {
            float qty = 0;
            if (IfExists(itemCode, factoryName))
            {
                DataTable dt = dalStock.Search(itemCode, getFactoryID(factoryName));
                qty = Convert.ToSingle(dt.Rows[0]["stock_qty"].ToString());
            }
            else
            {
                qty = 0;
            }

            return qty;
        }

        private void loadActionHistory(int orderID)
        {
            DataTable dt = dalOrderAction.Select(orderID);
            dt.DefaultView.Sort = "order_action_id DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            dgvAction.Rows.Clear();
            foreach (DataRow action in sortedDt.Rows)
            {
                int n = dgvAction.Rows.Add();
                if(!Convert.ToBoolean(action["active"]))
                {
                    dgvAction.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
                    dgvAction.Rows[n].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Strikeout);
                }
                else if(action["action"].ToString().Equals("RECEIVE"))
                {
                    dgvAction.Rows[n].DefaultCellStyle.ForeColor = Color.FromArgb(52, 168, 83);
                }

                dgvAction.Rows[n].Cells["order_action_id"].Value = action["order_action_id"].ToString();
                dgvAction.Rows[n].Cells["order_id"].Value = action["ord_id"].ToString();
                dgvAction.Rows[n].Cells["added_date"].Value = action["added_date"].ToString();
                dgvAction.Rows[n].Cells["added_by"].Value = action["added_by"].ToString();
                dgvAction.Rows[n].Cells["action"].Value = action["action"].ToString();
                dgvAction.Rows[n].Cells["action_detail"].Value = action["action_detail"].ToString();
                dgvAction.Rows[n].Cells["action_from"].Value = action["action_from"].ToString();
                dgvAction.Rows[n].Cells["action_to"].Value = action["action_to"].ToString();
                dgvAction.Rows[n].Cells["note"].Value = action["note"].ToString();
            }
            listPaint(dgvAction);
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

            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ClearSelection();
        }

        private void dgvAction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                dgvAction.ClearSelection();
            }
        }

        private void dgvAction_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvAction.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvAction.ClearSelection();
            }
        }

        private void frmOrderActionHistory_Load(object sender, EventArgs e)
        {
            dgvAction.ClearSelection();
        }

        //handle the row selection on right click
        private void dgvAction_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvAction.CurrentCell = dgvAction.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvAction.Rows[e.RowIndex].Selected = true;
                dgvAction.Focus();
                int rowIndex = dgvAction.CurrentCell.RowIndex;

                try
                {
                   
                    my_menu.Items.Add("Edit").Name = "Edit";
                    my_menu.Items.Add("Undo").Name = "Undo";

                    string action = dgvAction.Rows[rowIndex].Cells["action"].Value.ToString();
                    int actionID = Convert.ToInt32(dgvAction.Rows[rowIndex].Cells["order_action_id"].Value);
                    bool active = dalOrderAction.checkIfActive(actionID);

                    if(active && action.Equals("RECEIVE"))
                    {
                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                        contextMenuStrip1 = my_menu;
                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    }
                    

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

            DataGridView dgv = dgvAction;
            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;

            int actionID = Convert.ToInt32(dgv.Rows[rowIndex].Cells["order_action_id"].Value);
            int orderID = Convert.ToInt32(dgv.Rows[rowIndex].Cells["order_id"].Value);

            contextMenuStrip1.Hide();

            if (itemClicked.Equals("Edit"))
            {
                actionEdit(actionID, orderID);
            }
            else if (itemClicked.Equals("Undo"))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to undo this action?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    actionUndo(actionID, orderID);
                }         
            }
          

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void orderRecordUpdate(int orderID, float orderQty, float receivedQty)
        {
            float pending = orderQty - receivedQty;

            uOrder.ord_pending = pending;
            uOrder.ord_received = receivedQty;
            uOrder.ord_id = orderID;

            if (!dalOrder.receivedUpdate(uOrder))
            {
                MessageBox.Show("Failed to update order record.");
            }

        }

        private void actionEdit(int actionID, int orderID)
        {
            string to = "";
            string itemCode = "";
            string itemName = "";
            string unit = "";
            float returnQty = 0;
            float orderQty = 0;
            float receivedQty = 0;
            //string trfDate = "";

            DataTable dt = dalOrderAction.SelectByActionID(actionID);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow action in dt.Rows)
                {
                    to = action["action_to"].ToString();

                    if (float.TryParse(action["action_detail"].ToString(), out returnQty))
                    {
                        returnQty = Convert.ToSingle(action["action_detail"]);
                    }
                    //trfDate = Convert.ToDateTime(action["action_detail"])
                }
            }

            DataTable dtOrder = dalOrder.Select(orderID);

            if (dtOrder.Rows.Count > 0)
            {
                foreach (DataRow order in dtOrder.Rows)
                {
                    itemCode = order["ord_item_code"].ToString();
                    itemName = order["item_name"].ToString();
                    unit = order["ord_unit"].ToString();
                    orderQty = Convert.ToSingle(order["ord_qty"]);
                    receivedQty = Convert.ToSingle(order["ord_received"]);
                }
            }
            //call receive form
            frmOrderReceive frm = new frmOrderReceive(orderID, itemCode, itemName, orderQty, returnQty, receivedQty, unit);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//stock in

            if(editSuccess)
            {
                editSuccess = false;
                actionUndo(actionID, orderID);
            }
        }

        private void actionUndo(int actionID, int orderID)
        {
            //stock out,deactivate
            //reload list
            bool success = false;
            string to = "";
            string itemCode = "";
            string unit = "";
            float returnQty = 0;
            float orderQty = 0;
            float receivedQty = 0;

            DataTable dt = dalOrderAction.SelectByActionID(actionID);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow action in dt.Rows)
                {  
                    to = action["action_to"].ToString();

                    if (float.TryParse(action["action_detail"].ToString(), out returnQty))
                    {
                        returnQty = Convert.ToSingle(action["action_detail"]);
                    }
                }
            }

            DataTable dtOrder = dalOrder.Select(orderID);

            if (dtOrder.Rows.Count > 0)
            {
                foreach (DataRow order in dtOrder.Rows)
                {
                    itemCode = order["ord_item_code"].ToString();
                    unit = order["ord_unit"].ToString();
                    orderQty = Convert.ToSingle(order["ord_qty"]);
                    receivedQty = Convert.ToSingle(order["ord_received"]);
                }
            }

            uStock.stock_item_code = itemCode;
            uStock.stock_fac_id = Convert.ToInt32(getFactoryID(to));
            uStock.stock_qty = getQty(itemCode, to) - returnQty;
            uStock.stock_unit = unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = 0;

            if (IfExists(itemCode, to))
            {
                success = dalStock.Update(uStock);
            }
            else
            {
                success = dalStock.Insert(uStock);
            }

            if (!success)
            {
                MessageBox.Show("Failed to updated stock");
            }
            else
            {
                frmOrder.receivedReturn = true;
                dalItem.orderAdd(itemCode, returnQty.ToString());//add order qty to item
                uOrder.ord_id = orderID;
                uOrder.ord_status = "PENDING";
                dalOrder.statusUpdate(uOrder);
                orderRecordUpdate(orderID, orderQty, receivedQty - returnQty);
                dalOrderAction.deactivate(actionID);
                transferRecord("Passed", itemCode, to, returnQty.ToString(), unit);
                loadActionHistory(orderID);
            }
        }
    }
}
