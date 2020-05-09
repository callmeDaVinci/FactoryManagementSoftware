using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmTargetQtyConfirm : Form
    {
        static public string targetQty;
        static public bool confirm = false;

        public frmTargetQtyConfirm()
        {
            InitializeComponent();
        }

        public frmTargetQtyConfirm(string ableToProduce, string targetQty)
        {
            InitializeComponent();
            txtAbleToProduceQty.Text = ableToProduce;
            txtTargetQty.Text = targetQty;
        }
        private void frmTargetQtyConfirm_Load(object sender, EventArgs e)
        {

        }

        private void txtTargetQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtAbleToProduceQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            float Qty = Convert.ToSingle(txtTargetQty.Text);
            float ableQty = Convert.ToSingle(txtAbleToProduceQty.Text);

            if (string.IsNullOrEmpty(txtTargetQty.Text))
            {
                errorProvider1.SetError(txtTargetQty, "Target Qty Required");
            }
            else if(Qty > ableQty)
            {
                MessageBox.Show("Warning!!!\nTarget quantity is greater than the quantity able to produce!");
            }
            else
            {
                confirm = true;
                targetQty = txtTargetQty.Text;
                Close();
            }
        }

        private void txtTargetQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtTargetQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCheck_Click(sender, e);
            }
        }
    }
}
