using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderReceive : Form
    {
        private string Unit;
        private int orderID;
        private float maxReceiveQty;
        private string orderQty;
        private string receivedQty;
        private float returnQty;
        private float overReceivedQty = 0;
        private string orderType ="";
        private bool actionEdit = false;
        custDAL dalCust = new custDAL();
        userDAL dalUser = new userDAL();
        Tool tool = new Tool();

        public frmOrderReceive(int id, string code,string name, float qty,float received, string unit, string type)
        {
            InitializeComponent();
            orderType = type;
            txtItemCode.Text = code;
            txtItemName.Text = name;
            txtUnit.Text = unit;
            orderQty = qty.ToString();
            receivedQty = received.ToString();
            maxReceiveQty = qty - received;
            txtQty.Text = maxReceiveQty.ToString();
            Unit = unit;
            orderID = id;
        }

        public frmOrderReceive(int id, string code, string name, float orderedqty, float returnqty, float received, string unit)
        {
            InitializeComponent();
            txtItemCode.Text = code;
            txtItemName.Text = name;
            txtUnit.Text = unit;
            returnQty = returnqty;
            orderQty = orderedqty.ToString();
            receivedQty = received.ToString();
            maxReceiveQty = orderedqty - received + returnqty;
            txtQty.Text = returnQty.ToString();
            Unit = unit;
            orderID = id;
            actionEdit = true;
        }

        #region class object declare

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();

        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();

        facDAL dalFac = new facDAL();

        #endregion

        #region load

        private void frmReceiveConfirm_Load(object sender, EventArgs e)
        {
            //select category list from category database
            DataTable dtTrfCatFrm = daltrfCat.Select();
            //remove repeating name in trf_cat_name
            DataTable distinctTable3 = dtTrfCatFrm.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according trf_cat_name
            distinctTable3.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbFrom.DataSource = distinctTable3;
            //show trf_cat_name data from table only
            cmbFrom.DisplayMember = "trf_cat_name";

            if(orderType.Equals("ZERO COST"))
            {
                cmbFrom.Text = "Customer";
                cmbSubFrom.Text = "PMMA";
            }
            else
            {
                cmbFrom.Text = "Supplier";
                cmbSubFrom.SelectedIndex = -1;
            }

            DataTable dt = dalFac.SelectDESC();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
            cmbTo.DataSource = distinctTable;
            cmbTo.DisplayMember = "fac_name";
        }
        
        #endregion

        #region validation

        private bool validation()
        {
            bool result = true;
            float receivedNumber = Convert.ToSingle(txtQty.Text);

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                result = false;
                errorProvider1.SetError(txtQty, "Receive qty Required");
            }

            if (receivedNumber > maxReceiveQty)
            {
                float actualReceivedQty = 0;
                result = false;
                if(actionEdit)
                {
                    actualReceivedQty = Convert.ToSingle(receivedQty) - returnQty;
                }
                else
                {
                    actualReceivedQty = Convert.ToSingle(receivedQty);
                }
                errorProvider1.SetError(txtQty, "Wrong receive qty." + "\nOrdered Qty: " + orderQty + "\nReceived Qty: " + actualReceivedQty + "\nReceive qty cannot higher than " + maxReceiveQty);

                DialogResult dialogResult = MessageBox.Show("Ordered qty: "+orderQty+"\nPending qty: "+(Convert.ToSingle(orderQty) - actualReceivedQty)+"\nReceive  qty: "+ receivedNumber+"\nThe receive qty has exceeded the pending qty, are you sure want to proccess this action?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    overReceivedQty = receivedNumber - maxReceiveQty;
                    result = true;
                }
            }

            if (receivedNumber <= 0)
            {
                result = false;
                errorProvider1.SetError(txtQty, "Receive qty cannot below the 1");
            }

            if (string.IsNullOrEmpty(txtLotNO.Text))
            {
                result = false;
                errorProvider2.SetError(txtLotNO, "Lot Number Required");
            }

            return result;
        }

        private bool ifFullyReceived()
        {
            bool result = false;
            float receivedNumber = Convert.ToSingle(txtQty.Text);

            if (receivedNumber >= maxReceiveQty)
            {
                result = true;
            }

            return result;
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

        #endregion

        #region database proccessing

        public float GetTotalBalAfterTransfer(DataTable dtStock)
        {
            float afterTotalQty = 0;

            foreach (DataRow stock in dtStock.Rows)
            {
                float stockQty = float.TryParse(stock["stock_qty"].ToString(), out stockQty) ? stockQty : 0;

                afterTotalQty += stockQty;
            }

            return afterTotalQty;

        }
        private int transferRecord(string stockResult)
        {
            string locationFrom;

            if (string.IsNullOrEmpty(cmbSubFrom.Text))
            {
                locationFrom = cmbFrom.Text;
            }
            else
            {
                locationFrom = cmbSubFrom.Text;
            }

            string locationTo = cmbTo.Text;

            utrfHist.trf_hist_item_code = txtItemCode.Text;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = Convert.ToSingle(txtQty.Text);
            utrfHist.trf_hist_unit = Unit;
            utrfHist.trf_hist_trf_date = Convert.ToDateTime(dtpTrfDate.Text);
            utrfHist.trf_hist_note = "[" + dalUser.getUsername(MainDashboard.USER_ID) + "] Order: Received";
            utrfHist.trf_hist_added_date = DateTime.Now;
            utrfHist.trf_hist_added_by = MainDashboard.USER_ID;
            utrfHist.trf_result = stockResult;
            utrfHist.trf_hist_from_order = 1;


            utrfHist.balance = GetTotalBalAfterTransfer(dalStock.Select(txtItemCode.Text));

            //Inserting Data into Database
            bool success = daltrfHist.Insert(utrfHist);
            if (!success)
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new transfer record");
            }
            return daltrfHist.getIndexNo(utrfHist);
        }

        private void orderRecordUpdate()
        {
            float orderedqty = Convert.ToSingle(orderQty);
            float oldreceivedNumber = Convert.ToSingle(receivedQty);
            float receivedNumber = Convert.ToSingle(txtQty.Text) + oldreceivedNumber;
            float pending = orderedqty - receivedNumber;

            uOrd.ord_pending = pending;
            uOrd.ord_received = receivedNumber;
            uOrd.ord_id = orderID;

            if (!dalOrd.receivedUpdate(uOrd))
            {
                MessageBox.Show("Failed to update order record.");
            }

        }

        private void stockTransfer()
        {
            bool success = false;
            uStock.stock_item_code = txtItemCode.Text;
            uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTo.Text));
            uStock.stock_qty = getQty(txtItemCode.Text, cmbTo.Text) + Convert.ToSingle(txtQty.Text);
            uStock.stock_unit = Unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = MainDashboard.USER_ID;


            if (IfExists(txtItemCode.Text, cmbTo.Text))
            {
                success = dalStock.Update(uStock);
            }
            else
            {
                success = dalStock.Insert(uStock);
            }

            if (!success)
            {
                MessageBox.Show("Failed to transfer stock");
            }
            else
            {
                float temp = Convert.ToSingle(txtQty.Text) - overReceivedQty;
                dalItem.orderSubtract(txtItemCode.Text, temp.ToString()); //subtract order qty

                //change pmma zero cost item qty
                if (cmbSubFrom.Text.Equals(tool.getCustName(1)))
                {
                    //dalItem.orderSubtract(txtItemCode.Text, txtQty.Text);

                    uItem.item_code = uStock.stock_item_code;
                    uItem.item_last_pmma_qty = dalItem.getLastPMMAQty(uItem.item_code);
                    uItem.item_pmma_qty = dalItem.getPMMAQty(uItem.item_code) + Convert.ToSingle(txtQty.Text);
                    uItem.item_updtd_date = uStock.stock_updtd_date;
                    uItem.item_updtd_by = MainDashboard.USER_ID;

                    bool itemPMMMAQtyUpdateSuccess = dalItem.UpdatePMMAQty(uItem);

                    if (!itemPMMMAQtyUpdateSuccess)
                    {
                        MessageBox.Show("Failed to updated item pmma qty(@item dal)");
                    }

                }

                if (actionEdit)
                {
                    
                    actionEdit = false;
                    frmOrderActionHistory.editSuccess = true;
                }
                if (ifFullyReceived())
                {
                    uOrd.ord_id = orderID;
                    uOrd.ord_status = "RECEIVED";
                    dalOrd.statusUpdate(uOrd);
                }
                frmOrder.receivedNumber = txtQty.Text;

                orderRecordUpdate();

                string from;

                if(string.IsNullOrEmpty(cmbSubFrom.Text))
                {
                    from = cmbFrom.Text;
                }
                else
                {
                    from = cmbSubFrom.Text;
                }

                dalOrderAction.orderReceive(orderID, transferRecord("Passed"),txtQty.Text, from, cmbTo.Text, txtLotNO.Text);
                
            }

            if (!dalItem.updateTotalStock(txtItemCode.Text))
            {
                MessageBox.Show("Failed to update total stock");
            }
        }
        #endregion

        #region text changed

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtLotNO_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        #endregion

        #region function: order/cancel

        private void cancel_Click(object sender, EventArgs e)//close form
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this action?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void stockIn_Clock(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (validation())
            {
                stockTransfer();
                Close();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }


        #endregion

        private void loadLocationData(DataTable dt, ComboBox cmb, string columnName)
        {
            DataTable lacationTable = dt.DefaultView.ToTable(true, columnName);

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = columnName;
        }

        private void cmbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //errorProvider4.Clear();
            if (cmbFrom.Text.Equals("Customer"))
            {
                DataTable dt = dalCust.FullSelect();
                loadLocationData(dt, cmbSubFrom, "cust_name");

            }
            else
            {
                cmbSubFrom.DataSource = null;
            }
        }
    }
}
