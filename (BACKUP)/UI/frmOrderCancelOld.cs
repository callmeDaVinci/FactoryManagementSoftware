using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderCancelOld : Form
    {
        public frmOrderCancelOld()
        {
            InitializeComponent();
        }

        private int orderID;
        private string stockInQty;
        private string itemCode;
        private string itemName;
        private string Unit;

        public frmOrderCancelOld(int id, string itemcode, string itemname, string qty, string unit)
        {
            InitializeComponent();
            orderID = id;
            itemCode = itemcode;
            itemName = itemname;
            stockInQty = qty;
            Unit = unit;

            txtItemCode.Text = itemcode;
            txtItemName.Text = itemname;
            txtQty.Text = qty;
        }

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

        #endregion

        private void frmReceiveCancel_Load(object sender, EventArgs e)
        {
            DataTable dt = dalFac.SelectDESC();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "fac_name");
            distinctTable.DefaultView.Sort = "fac_name ASC";
            cmbFrom.DataSource = distinctTable;
            cmbFrom.DisplayMember = "fac_name";
            txtQty.Text = stockInQty;
        }

        private void addOrderAction(int id, string action)
        {
            uOrderAction.ord_id = id;
            uOrderAction.added_date = DateTime.Now;
            uOrderAction.added_by = 0;
            uOrderAction.action = action;
            uOrderAction.note = "";

            if (!dalOrderAction.Insert(uOrderAction))
            {
                MessageBox.Show("Failed to add new action");
            }
        }

        private int transferRecord(string stockResult)
        {
            string locationFrom = cmbFrom.Text;
            string locationTo = "Other";
            utrfHist.trf_hist_item_code = itemCode;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = Convert.ToSingle(txtQty.Text);
            utrfHist.trf_hist_unit = Unit;
            utrfHist.trf_hist_trf_date = DateTime.Now;
            utrfHist.trf_hist_note = "Order: Cancel Received";
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

        private void button1_Click(object sender, EventArgs e)
        {

            uStock.stock_item_code = itemCode;
            uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbFrom.Text));
            uStock.stock_qty = getQty(itemCode, cmbFrom.Text) - Convert.ToSingle(stockInQty);
            uStock.stock_unit = Unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = 0;

            if (IfExists(itemCode, cmbFrom.Text))
            {
                bool success = dalStock.Update(uStock);

                if (!success)
                {
                    MessageBox.Show("Failed to updated stock");
                }
                else
                {
                    frmOrder.receivedReturn = true;
                    string action = "Stock out "+stockInQty+"from "+cmbFrom.Text;
                    addOrderAction(orderID,action);
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
                    frmOrder.receivedReturn = true;
                    string action = "Stock out " + stockInQty + " from " + cmbFrom.Text;
                    addOrderAction(orderID, action);
                    transferRecord("Passed");
                }
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
