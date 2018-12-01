using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmReceiveConfirm : Form
    {
        private string Unit;
        private int orderID;
        private float maxReceiveQty;
        private string orderQty;
        private string receivedQty;

        public frmReceiveConfirm(int id, string code,string name, float qty,float received, string unit)
        {
            InitializeComponent();
            txtItemCode.Text = code;
            txtItemName.Text = name;
            orderQty = qty.ToString();
            receivedQty = received.ToString();
            maxReceiveQty = qty - received;
            txtQty.Text = maxReceiveQty.ToString();
            Unit = unit;
            orderID = id;
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

        facDAL dalFac = new facDAL();

        #endregion 

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
                result = false;
                errorProvider1.SetError(txtQty, "Wrong receive qty."+"\nOrdered Qty: "+orderQty+"\nReceived Qty: "+receivedQty+"\nReceive qty cannot higher than "+maxReceiveQty);
            }

            if (receivedNumber <= 0)
            {
                result = false;
                errorProvider1.SetError(txtQty, "Receive qty cannot below the 1");
            }

            if (string.IsNullOrEmpty(txtLotNO.Text))
            {
                result = false;
                errorProvider2.SetError(txtLotNO, "Lot No Required");
            }

            return result;
        }

        private bool ifFullyReceived()
        {
            bool result = false;
            float receivedNumber = Convert.ToSingle(txtQty.Text);

            if(receivedNumber == maxReceiveQty)
            {
                result = true;             
            }

            return result;
        }

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

            cmbFrom.Text = "Supplier";

            DataTable dt = dalFac.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
            cmbTo.DataSource = distinctTable;
            cmbTo.DisplayMember = "fac_name";
        }

        private void button2_Click(object sender, EventArgs e)//close form
        {
            this.Close();
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

        private int transferRecord(string stockResult)
        {

            string locationFrom = cmbFrom.Text;

            string locationTo = cmbTo.Text;

            utrfHist.trf_hist_item_code = txtItemCode.Text;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = Convert.ToSingle(txtQty.Text);
            utrfHist.trf_hist_unit = Unit;
            utrfHist.trf_hist_trf_date = DateTime.Now;
            utrfHist.trf_hist_note = "Order: Received";
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

        private void addOrderAction(int id, string action, bool fullyReceived)
        {
            uOrderAction.ord_id = id;
            uOrderAction.added_date = DateTime.Now;
            uOrderAction.added_by = 0;
            uOrderAction.action = action;

            if(fullyReceived)
            {
                uOrderAction.note = "";
            }
            else
            {
                uOrderAction.note = "LOT NO: " + txtLotNO.Text;
            }

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

            if(!dalOrd.receivedUpdate(uOrd))
            {
                MessageBox.Show("Failed to update order record.");
            }

        }

        private void stockTransfer()
        {
            uStock.stock_item_code = txtItemCode.Text;
            uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTo.Text));
            uStock.stock_qty = getQty(txtItemCode.Text, cmbTo.Text) + Convert.ToSingle(txtQty.Text);
            uStock.stock_unit = Unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = 0;

            if (IfExists(txtItemCode.Text, cmbTo.Text))
            {
                bool success = dalStock.Update(uStock);

                if (!success)
                {
                    MessageBox.Show("Failed to updated stock");
                }
                else
                {
                    string action = "Stock In " + txtQty.Text + " To " + cmbTo.Text;
                    addOrderAction(orderID, action,false);

                    if (ifFullyReceived())
                    {
                        frmOrder.receivedStockIn = true;
                        action = "Order Fully Received";
                        addOrderAction(orderID, action,true);
                    }

                    frmOrder.receivedNumber = txtQty.Text;
                    
                    orderRecordUpdate();
                    
                    transferRecord("Passed");
                }
            }
            else
            {
                bool success = dalStock.Insert(uStock);
                if (!success)
                {
                    MessageBox.Show("Failed to add new stock");
                }
                else
                {
                    string action = "Stock In " + txtQty.Text + " To " + cmbTo.Text;
                    addOrderAction(orderID, action, false);
                    if (ifFullyReceived())
                    {
                        frmOrder.receivedStockIn = true;
                        action = "Order Fully Received";
                        addOrderAction(orderID, action,true);
                    }

                    frmOrder.receivedNumber = txtQty.Text;
                    
                    orderRecordUpdate();
                    
                    transferRecord("Passed");
                }
            }

            if (!dalItem.updateTotalStock(txtItemCode.Text))
            {
                MessageBox.Show("Failed to update total stock");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (validation())
            {
                stockTransfer();
                this.Close();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtLotNO_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }
    }  
}
