using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderApprove : Form
    {
        private string messageToUser = "Are you sure you want to approve this order?";
        private string date;
        private string orderQty;
        private string orderUnit;
        private string orderType;
        private string orderReceived = 0.ToString();
        private int orderPoNO;

        public frmOrderApprove(string orderID, DateTime requiredDate, string itemName, string itemCode, string qty, string unit, string type, int pono, string received)
        {
            //DateTime date = DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null);
            date = requiredDate.ToString();
            orderQty = qty;
            orderUnit = unit;
            orderType = type;
            orderPoNO = pono;
            orderReceived = received;
            InitializeComponent();
            txtOrderID.Text = orderID;

            dtpDateRequired.Value = requiredDate;
            txtItemName.Text = itemName;
            txtItemCode.Text = itemCode;
            txtQty.Text = qty;
            cmbQtyUnit.Text = unit;
            txtPONO.Text = orderPoNO.ToString();

            btnApprove.Text = "EDIT";
            messageToUser = "Are you sure you want to edit this order?";
            if (orderType.Equals("PURCHASE"))
            {
                cbZeroCost.Checked = false;
            }
            else
            {
                cbZeroCost.Checked = true;
            }

        }


        public frmOrderApprove(string orderID,DateTime requiredDate,string itemName,string itemCode,string qty,string unit, string type, int pono)
        {
            //DateTime date = DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null);
            date = requiredDate.ToString();
            orderQty = qty;
            orderUnit = unit;
            orderType = type;
            orderPoNO = pono;

            InitializeComponent();
            txtOrderID.Text = orderID;

            dtpDateRequired.Value = requiredDate;//DateTime.ParseExact(requiredDate, "dd/MM/yyyy", null)
            txtItemName.Text = itemName;
            txtItemCode.Text = itemCode;
            txtQty.Text = qty;
            cmbQtyUnit.Text = unit;
            txtPONO.Text = orderPoNO.ToString();

            if(orderType.Equals("PURCHASE"))
            {
                cbZeroCost.Checked = false;
            }
            else
            {
                cbZeroCost.Checked = true;
            }

        }

        userDAL dalUser = new userDAL();

        ordBLL uOrder = new ordBLL();
        ordDAL dalOrder = new ordDAL();

        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();

        private void checkEdit(int id)
        {
            string oldDate = Convert.ToDateTime(date).Date.ToString("dd/MM/yyyy");
            if (!oldDate.Equals(dtpDateRequired.Text))
            {
                dalOrderAction.orderEdit(id, -1,-1, "Required Date", date, dtpDateRequired.Text, txtNote.Text);
            }
            if (!orderQty.Equals(txtQty.Text))
            {
                dalOrderAction.orderEdit(id, -1,-1, "Qty", orderQty, txtQty.Text, txtNote.Text);
            }

            if (!orderUnit.Equals(cmbQtyUnit.Text))
            {
                dalOrderAction.orderEdit(id, -1, -1, "Unit", orderUnit, cmbQtyUnit.Text, txtNote.Text);
            }

            if (!orderPoNO.ToString().Equals(txtPONO.Text))
            {
                dalOrderAction.orderEdit(id, -1, -1, "P/O NO", orderPoNO.ToString(), txtPONO.Text, txtNote.Text);
            }

            string type;

            if(cbZeroCost.Checked)
            {
                type = "ZERO COST";
            }
            else
            {
                type = "PURCHASE";
            }
            
            if (!orderType.Equals(type))
            {
                dalOrderAction.orderEdit(id, -1, -1, "Type", orderType, type, txtNote.Text);
            }

            dalOrderAction.orderApprove(id, txtNote.Text);
        }

        private void orderAppoveUpdate()
        {
            uOrder.ord_id = Convert.ToInt32(txtOrderID.Text);
            uOrder.ord_required_date = dtpDateRequired.Value.Date;
            uOrder.ord_qty = Convert.ToSingle(txtQty.Text);
            uOrder.ord_pending = uOrder.ord_qty - Convert.ToSingle(orderReceived);
            uOrder.ord_received = Convert.ToSingle(orderReceived);
            uOrder.ord_item_code = txtItemCode.Text;
            uOrder.ord_status = "PENDING";
            uOrder.ord_po_no = Convert.ToInt32(txtPONO.Text);
            uOrder.ord_updated_date = DateTime.Now;
            uOrder.ord_updated_by = MainDashboard.USER_ID;

            if(cbZeroCost.Checked)
            {
                uOrder.ord_type = "ZERO COST";
            }
            else
            {
                uOrder.ord_type = "PURCHASE";

            }
            if (dalOrder.Update(uOrder))
            {
                frmOrder.orderApproved = true;
                frmOrder.finalOrderNumber = txtQty.Text;
                checkEdit(uOrder.ord_id);
                Close();

            }
        }

        #region button function

        private void btnApprove_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show(messageToUser, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

            materialDAL dalMat = new materialDAL();
            if (dalMat.checkIfZeroCost(txtItemCode.Text))
            {
                if (!cbZeroCost.Checked)
                {
                    errorProvider2.SetError(cbZeroCost, txtItemCode.Text + " is a zero cost item");
                    DialogResult dialogResult = MessageBox.Show(txtItemCode.Text + " is a zero cost item, are you sure you want to make this a PURCHASE order?\n(You can change it by check/uncheck the ZERO COST section)", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult != DialogResult.Yes)
                    {
                        result = false;
                    }
                }
            }
            else
            {
                if (cbZeroCost.Checked)
                {
                    errorProvider2.SetError(cbZeroCost, txtItemCode.Text + " NOT a zero cost item");
                    DialogResult dialogResult = MessageBox.Show(txtItemCode.Text + " NOT a zero cost item, are you sure you want to make this a ZERO COST order?\n(You can change it by check/uncheck the ZERO COST section)", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult != DialogResult.Yes)
                    {
                        result = false;
                    }
                }
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

        private void txtPONO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cbZeroCost_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        private void frmOrderApprove_Load(object sender, EventArgs e)
        {

        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
