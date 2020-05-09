using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPAddItem : Form
    {
        public frmSPPAddItem()
        {
            InitializeComponent();
            dt_ReadyGoods = dalItem.SPPReadyGoodsSelect();
        }

        public frmSPPAddItem(bool mode)
        {
            InitializeComponent();
            dt_ReadyGoods = dalItem.SPPReadyGoodsSelect();
            lblNote.Visible = true;
            txtNote.Visible = true;
            CallFromNewPO = true;
        }

        public frmSPPAddItem( string type, string size, string unit, string targetPcs)
        {
            InitializeComponent();
            dt_ReadyGoods = dalItem.SPPReadyGoodsSelect();

            itemEditing = true;
            lblSize.Text = size;
            lblUnit.Text = unit;
            lblType.Text = type;
            editingTargetPcs = targetPcs;
            lblNote.Visible = true;
            txtNote.Visible = true;
            CallFromNewPO = true;
        }

        public frmSPPAddItem(string code, string type, string size, string unit, string targetPcs)
        {
            InitializeComponent();
            dt_ReadyGoods = dalItem.SPPReadyGoodsSelect();

            itemEditing = true;
            lblSize.Text = size;
            lblUnit.Text = unit;
            lblType.Text = type;
            lblCode.Text = code;
            editingTargetPcs = targetPcs;

        }

        SPPDataDAL dalData = new SPPDataDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        DataTable dt_ReadyGoods;

        bool CallFromNewPO = false;
        bool ConvertFromPcs = false;
        bool ConvertFromBags = false;
        bool itemEditing = false;

        private int stdPackingPerBag = 0;
        private string editingTargetPcs;
        static public bool itemAdded = false;
        static public bool itemEdited = false;
        static public bool itemRemoved = false;

        static public string item_Size;
        static public string item_Type;
        static public string item_Code;
        static public string item_Target_Pcs;
        static public string item_Target_Bags;
        static public string item_Note;

        private void txtQtyPerPacket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnEqualSocket_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_EqualSocket;
            GetItemCodeAndStock();
        }

        private void btnEqualElbow_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_EqualElbow;
            GetItemCodeAndStock();
        }

        private void btnEqualTee_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_EqualTee;
            GetItemCodeAndStock();
        }

        private void GetItemCodeAndStock()
        {
            txtPcsQty.Clear();
            errorProvider1.Clear();
            errorProvider2.Clear();

            string itemType = lblType.Text;
            string itemSize = lblSize.Text;
            string itemCode = "";
            string itemStock = "";
           

            if (!string.IsNullOrEmpty(itemType) && !string.IsNullOrEmpty(itemSize))
            {
                foreach(DataRow row in dt_ReadyGoods.Rows)
                {
                    if(row["TYPE"].ToString() == itemType && row["SIZE"].ToString() == itemSize)
                    {
                        itemCode = row["CODE"].ToString();

                        int qtyPerBag = int.TryParse(row["STD_PACKING"].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int stockQty = int.TryParse(row["QUANTITY"].ToString(), out stockQty) ? stockQty : 0;

                        stdPackingPerBag = qtyPerBag;
                        itemStock = stockQty+" pcs";

                        if (qtyPerBag > 0)
                        {

                            int bagQty = stockQty / qtyPerBag;

                            itemStock += " ("+bagQty+" bags)";
                        }

                    }
                }

            }
            lblCode.Text = itemCode;
            lblStock.Text = itemStock;
            lblQtyPerBag.Text = stdPackingPerBag + "/BAG";
        }

        private void btn20MM_Click(object sender, EventArgs e)
        {
            lblSize.Text = "20";
            GetItemCodeAndStock();
        }

        private void btn25MM_Click(object sender, EventArgs e)
        {
            lblSize.Text = "25";
            GetItemCodeAndStock();
        }

        private void btn32MM_Click(object sender, EventArgs e)
        {
            lblSize.Text = "32";
            GetItemCodeAndStock();
        }

        private void btn50MM_Click(object sender, EventArgs e)
        {
            lblSize.Text = "50";
            GetItemCodeAndStock();
        }

        private void btn63MM_Click(object sender, EventArgs e)
        {
            lblSize.Text = "63";
            GetItemCodeAndStock();
        }

        private void btn90MM_Click(object sender, EventArgs e)
        {
            //lblSize.Text = "90";
            //GetItemCode();
        }

        private void frmSPPAddItem_Load(object sender, EventArgs e)
        {
            GetItemCodeAndStock();

            if(itemEditing)
            {
                txtPcsQty.Text = editingTargetPcs;
                btnRemoveFromList.Visible = true;
                btnAdd.Text = "EDIT";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtQtyPerPacket_TextChanged(object sender, EventArgs e)
        {
            if(!ConvertFromBags)
            {
                ConvertFromPcs = true;
            }

            if(ConvertFromPcs)
            CalculateBagQty();

            ConvertFromPcs = false;
        }

        private void CalculateBagQty()
        {
            errorProvider2.Clear();
            int PcsQty = int.TryParse(txtPcsQty.Text, out PcsQty) ? PcsQty : 0;

            double balance = 0;
            if (stdPackingPerBag >0)
            {
                txtBagQty.Text = (PcsQty / stdPackingPerBag).ToString();
                balance = PcsQty % stdPackingPerBag;
            }
            else
            {
                txtBagQty.Clear();
            }

            if (balance > 0)
            {
                lblPlusBalance.Visible = true;
                lblBalance.Visible = true;

                lblBalance.Text = balance.ToString();
            }
            else
            {
                lblPlusBalance.Visible = false;
                lblBalance.Visible = false;
            }
        }

        private void CalculatePcsQty()
        {
            

            errorProvider2.Clear();
            int bagQty = int.TryParse(txtBagQty.Text, out bagQty) ? bagQty : 0;

            txtPcsQty.Text = (bagQty * stdPackingPerBag).ToString();

           
        }

        private void txtBagQty_TextChanged(object sender, EventArgs e)
        {
            if (!ConvertFromPcs)
            {
                ConvertFromBags = true;
            }

            if (ConvertFromBags)
                CalculatePcsQty();

            ConvertFromBags = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lblPlusBalance.Visible = false;
            lblBalance.Visible = false;

            ConvertFromBags = true;
            CalculatePcsQty();
            ConvertFromBags = false;
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(lblCode.Text))
            {
                result = false;
                errorProvider1.SetError(lblCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtPcsQty.Text) || string.IsNullOrEmpty(txtBagQty.Text))
            {
                result = false;
                errorProvider2.SetError(gbTargetQty, "Item Target Qty Required");
            }

            return result;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           if(Validation())
            {
                item_Type = lblType.Text;
                item_Size = lblSize.Text;
                item_Code = lblCode.Text;
                item_Target_Pcs = txtPcsQty.Text;
                item_Target_Bags = txtBagQty.Text;
                
                if(CallFromNewPO)
                {
                    item_Note = txtNote.Text;
                }

                if (itemEditing)
                {
                    string newTargetPcs = txtPcsQty.Text;

                    if(newTargetPcs != editingTargetPcs)
                    {
                        itemEdited = true;
                        MessageBox.Show("Item edited!");
                    }
                    
                }
                else
                {
                    itemAdded = true;
                    

                    MessageBox.Show("Item added!");
                }
                
                Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            int targetBags = int.TryParse(txtBagQty.Text, out targetBags) ? targetBags : 0;

            txtBagQty.Text = (targetBags + 10).ToString();
        }

        private void btnRemoveFromList_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to remove this item from target list?", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                itemRemoved = true;
                MessageBox.Show("Item removed!");
                Close();
            }
        }

        private void lblSize_Click(object sender, EventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }
    }
}
