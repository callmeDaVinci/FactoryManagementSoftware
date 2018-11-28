using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderApprove : Form
    {
        private string date;
        private string orderQty;
        private string orderUnit;

        public frmOrderApprove(string orderID,string requiredDate,string itemName,string itemCode,string qty,string unit)
        {
            date = requiredDate;
            orderQty = qty;
            orderUnit = unit;

            InitializeComponent();
            txtOrderID.Text = orderID;
            dtpDateRequired.Text = requiredDate;
            txtItemName.Text = itemName;
            txtItemCode.Text = itemCode;
            txtQty.Text = qty;
            cmbQtyUnit.Text = unit;
        }

        ordBLL uOrder = new ordBLL();
        ordDAL dalOrder = new ordDAL();

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();

        private void checkOrderChange()
        {
            string action = "";
            if(!date.Equals(dtpDateRequired.Text))
            {
                action = "Date Required: " + date + " ---> " + dtpDateRequired.Text;
                addOrderAction(action);
            }

            if (!orderQty.Equals(txtQty.Text))
            {
                action = "Order Qty: " + orderQty + " ---> " + txtQty.Text;
                addOrderAction(action);
            }

            if (!orderUnit.Equals(cmbQtyUnit.Text))
            {
                action = "Order Unit: " + orderUnit + " ---> " + cmbQtyUnit.Text;
                addOrderAction(action);
            }

            action = "Order Approved";
            addOrderAction(action);
        }

        private void addOrderAction(string action)
        {
            uOrderAction.ord_id = Convert.ToInt32(txtOrderID.Text);
            uOrderAction.added_date = DateTime.Now;
            uOrderAction.added_by = 0;
            uOrderAction.action = action;
            uOrderAction.note = "";

            if(!dalOrderAction.Insert(uOrderAction))
            {
                MessageBox.Show("Failed to add new action");
            }
        }

        private void orderAppoveUpdate()
        {
            uOrder.ord_id = Convert.ToInt32(txtOrderID.Text);
            uOrder.ord_required_date = Convert.ToDateTime(dtpDateRequired.Text);
            uOrder.ord_qty = Convert.ToSingle(txtQty.Text);
            uOrder.ord_pending = uOrder.ord_qty;
            uOrder.ord_received = 0;

            if(dalOrder.Update(uOrder))
            {
                frmOrder.orderApproved = true;
                checkOrderChange();
                this.Close();

            }
        }

        #region button function

        private void btnApprove_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to approve this order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    orderAppoveUpdate();
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region validation

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                result = false;
                errorProvider1.SetError(txtQty, "Order Qty Required");
            }
            return result;
        }

        #endregion

        #region text change

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        #endregion
    }
}
