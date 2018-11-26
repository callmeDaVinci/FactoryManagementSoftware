using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmReceiveConfirm : Form
    {
        public frmReceiveConfirm()
        {
            InitializeComponent();
        }

        private string stockInQty = frmOrder.selectedOrderQty;
        private string itemCode = frmOrder.selectedItemCode;
        private string unit = frmOrder.selectedUnit;

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facDAL dalFac = new facDAL();

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


            txtQty.Text = stockInQty;
        }

        private void button2_Click(object sender, EventArgs e)
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

            utrfHist.trf_hist_item_code = itemCode;
            utrfHist.trf_hist_from = locationFrom;
            utrfHist.trf_hist_to = locationTo;
            utrfHist.trf_hist_qty = Convert.ToSingle(stockInQty);
            utrfHist.trf_hist_unit = unit;
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

        private void button1_Click(object sender, EventArgs e)
        {
            uStock.stock_item_code = itemCode;
            uStock.stock_fac_id = Convert.ToInt32(getFactoryID(cmbTo.Text));
            uStock.stock_qty = getQty(itemCode, cmbTo.Text) + Convert.ToSingle(stockInQty);
            uStock.stock_unit = unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = 0;

            if (IfExists(itemCode, cmbTo.Text))
            {
                bool success = dalStock.Update(uStock);
           
                if(!success)
                {
                    MessageBox.Show("Failed to updated stock");
                }
                else
                {
                    frmOrder.receivedStockIn = true;
                    frmOrder.receivedLocation = cmbTo.Text;
                    transferRecord("Passed");
                }

            }
            else
            {
                bool success = dalStock.Insert(uStock);
                if(!success)
                {
                    MessageBox.Show("Failed to add new stock");
                }
                else
                {
                    frmOrder.receivedStockIn = true;
                    transferRecord("Passed");
                }
            }

            this.Close();
        }
    }  
}
