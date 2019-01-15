using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderRequest : Form
    {
        private string initialCat = "";
        private string initialItemCode = "";
        private string initialItemName = "";
        private string initialQty= "";
        private string initialUnit = "";
        private string initialDate = "";
        private bool edit = false;

        public frmOrderRequest()
        {
            InitializeComponent();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            loadItemCategoryData();
            cmbItemCat.SelectedIndex = -1;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        public frmOrderRequest(string orderID, string category, string itemCode, string itemName, string qty, string unit, string requiredDate)
        {
            InitializeComponent();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            loadItemCategoryData();

            initialCat = category;
            initialItemCode = itemCode;
            initialItemName = itemName;
            initialQty = qty;
            initialUnit = unit;
            initialDate = requiredDate;
            edit = true;

            txtOrderID.Text = orderID;
            cmbItemCat.Text = initialCat;
            cmbItemName.Text = initialItemName;
            cmbItemCode.Text = initialItemCode;
            txtQty.Text = initialQty;
            cmbQtyUnit.Text = initialUnit;
            dtpRequiredDate.Text = initialDate;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        static public bool orderSuccess = false;

        #region create class object (database)

        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        orderActionDAL dalOrderAction = new orderActionDAL();

        Tool tool = new Tool();
        Text text = new Text();

        #endregion

        #region load data

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbItemCat.DataSource = distinctTable;
            cmbItemCat.DisplayMember = "item_cat_name";
        }

        private void loadItemNameData()
        {
            string keywords = cmbItemCat.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.catSearch(keywords);
                DataTable dtItemName = dt.DefaultView.ToTable(true, "item_name");

                dtItemName.DefaultView.Sort = "item_name ASC";
                cmbItemName.DataSource = dtItemName;
                cmbItemName.DisplayMember = "item_name";
            }
            else
            {
                cmbItemName.DataSource = null;
            }
        }

        private void loadItemCodeData()
        {
            string keywords = cmbItemName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                dtItemCode.DefaultView.Sort = "item_code ASC";
                cmbItemCode.DataSource = dtItemCode;
                cmbItemCode.DisplayMember = "item_code";
            }
            else
            {
                cmbItemCode.DataSource = null;
            }
        }

        private void unitDataSource()
        {
            string itemCat = cmbItemCat.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("item_unit");

            if (itemCat.Equals("RAW Material") || itemCat.Equals("Master Batch") || itemCat.Equals("Pigment"))
            {
                dt.Clear();
                dt.Rows.Add("kg");
                dt.Rows.Add("g");

            }
            else if (itemCat.Equals("Part") || itemCat.Equals("Carton"))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                cmbQtyUnit.DataSource = dt;
            }
            else if (!string.IsNullOrEmpty(itemCat))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                dt.Rows.Add("kg");
                dt.Rows.Add("g");

            }
            else
            {
                dt = null;
            }

            cmbQtyUnit.DataSource = dt;
            cmbQtyUnit.DisplayMember = "item_unit";
            cmbQtyUnit.SelectedIndex = -1;

        }

        #endregion

        #region validation

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbItemCat.Text))
            {
                result = false;
                errorProvider1.SetError(cmbItemCat, "Item Category Required");
            }

            if (string.IsNullOrEmpty(cmbItemName.Text))
            {
                result = false;
                errorProvider2.SetError(cmbItemName, "Item Name Required");
            }

            if (string.IsNullOrEmpty(cmbItemCode.Text))
            {
                result = false;
                errorProvider3.SetError(cmbItemCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                result = false;
                errorProvider4.SetError(txtQty, "Item order qty Required");
            }

            if (string.IsNullOrEmpty(cmbQtyUnit.Text))
            {
                result = false;
                errorProvider5.SetError(cmbQtyUnit, "Item order unit Required");
            }

            if (string.IsNullOrEmpty(dtpRequiredDate.Text))
            {
                result = false;
                errorProvider6.SetError(dtpRequiredDate, "Item forecast date Required");
            }

            return result;
        }

        private void getDataFromUser()
        {
            int id = Convert.ToInt32(txtOrderID.Text);

            if(id == -1)//create new order record
            {
                uOrd.ord_added_date = DateTime.Now;
                uOrd.ord_added_by = MainDashboard.USER_ID;
            }
            else//update order record
            {  
                uOrd.ord_id = id;
                uOrd.ord_updated_date = DateTime.Now;
                uOrd.ord_updated_by = MainDashboard.USER_ID;
            }

            uOrd.ord_item_code = cmbItemCode.Text;
            uOrd.ord_qty = Convert.ToInt32(txtQty.Text);
            uOrd.ord_pending = 0;
            uOrd.ord_received = 0;

            uOrd.ord_required_date = dtpRequiredDate.Value.Date;
            uOrd.ord_note = txtNote.Text;
            uOrd.ord_unit = cmbQtyUnit.Text;
            uOrd.ord_status = "REQUESTING";
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        #endregion

        #region selected index/text changed

        private void cmbItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            loadItemNameData();
            unitDataSource();
        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            loadItemCodeData();
        }

        private void cmbItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
        }

        private void cmbQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
        }

        private void dtpForecastDate_ValueChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
        }

        #endregion

        #region function: order/cancel

        private void checkEdit(int id)
        {
            if(initialCat != cmbItemCat.Text)
            {
                dalOrderAction.orderEdit(id, -1, "Category", initialCat, cmbItemCat.Text, txtNote.Text);
            }

            if (initialItemCode != cmbItemCode.Text)
            {
                dalOrderAction.orderEdit(id, -1, "Item Code", initialItemCode, cmbItemCode.Text, txtNote.Text);
            }

            if (initialItemName != cmbItemName.Text)
            {
                dalOrderAction.orderEdit(id, -1, "Item Name", initialItemName, cmbItemName.Text, txtNote.Text);
            }

            if (initialQty != txtQty.Text)
            {
                dalOrderAction.orderEdit(id, -1, "Qty", initialQty, txtQty.Text, txtNote.Text);
            }

            if (initialUnit != cmbQtyUnit.Text)
            {
                dalOrderAction.orderEdit(id, -1, "Unit", initialUnit, cmbQtyUnit.Text, txtNote.Text);
            }

            if (initialDate != dtpRequiredDate.Text)
            {
                dalOrderAction.orderEdit(id, -1, "Required Date", initialDate, dtpRequiredDate.Text, txtNote.Text);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            int id = Convert.ToInt32(txtOrderID.Text);
            bool success;
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    getDataFromUser();

                    if(id == -1)
                    {
                        success = dalOrd.Insert(uOrd);
                    }
                    else
                    {
                        success = dalOrd.Update(uOrd);
                    }
                    
                    if(!success)
                    {
                        //Failed to insert data
                        MessageBox.Show("Failed to add new order record");
                        tool.historyRecord(text.System, "Failed to add new order record(frmOrderRequest)", DateTime.Now, MainDashboard.USER_ID);
                    }
                    else
                    {
                        orderSuccess = true;

                        if(edit)
                        {
                            checkEdit(id);
                        }
                        
                        if (dalOrderAction.orderRequest(id, txtNote.Text))
                        {
                            MessageBox.Show("New order is requesting..."); 
                            this.Close();
                        }
                        else
                        {
                            //delete last add order record?
                        }
                        
                    }
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
