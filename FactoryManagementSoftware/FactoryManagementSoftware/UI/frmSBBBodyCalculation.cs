using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBBodyCalculation : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        itemBLL BODY_INFO = new itemBLL();
        itemBLL GOODS_INFO = new itemBLL();

        private int BODY_QTY_PER_GOODS = 1;

        public frmSBBBodyCalculation()
        {
            InitializeComponent();
        }

        public frmSBBBodyCalculation(itemBLL BODY, itemBLL GOODS, int TotalbodyQtyPerGoods)
        {
            InitializeComponent();
            BODY_INFO = BODY;
            GOODS_INFO = GOODS;
            BODY_QTY_PER_GOODS = TotalbodyQtyPerGoods;

            LoadPackagingData();
        }

        private bool DATA_UPDATING = false;

        private void LoadPackagingData()
        {
            DATA_UPDATING = true;

            txtContainerQty.Text = "1";

            lblBodyDescription.Text = BODY_INFO.item_name + " (" +BODY_INFO.item_code + ")";

            txtBodyPcsPerContainer.Text = BODY_INFO.standard_packing_qty.ToString();
            txtBodyStockPcsQty.Text = BODY_INFO.item_ready_stock.ToString();

            lblGoodsDescription.Text = GOODS_INFO.item_name + " (" + GOODS_INFO.item_code + ")";
            txtGoodsPcsPerBag.Text = GOODS_INFO.standard_packing_qty.ToString();
            txtGoodsStockPcsQty.Text = GOODS_INFO.item_ready_stock.ToString();

            DATA_UPDATING = false;

            bodyContainerStockUpdate();
            goodsBagStockUpdate();

            ContainerToBagConvert();

        }

        private void bodyContainerStockUpdate()
        {
            if(!DATA_UPDATING)
            {
                DATA_UPDATING = true;
                int itemPerPackaging = int.TryParse(txtBodyPcsPerContainer.Text, out int i) ? i : 0;
                int itemStock = int.TryParse(txtBodyStockPcsQty.Text, out i) ? i : 0;

                int packagingQty = 0;

                if (itemPerPackaging > 0)
                {
                    packagingQty = itemStock / itemPerPackaging;
                }

                txtBodyStockContainerQty.Text = packagingQty.ToString();
                DATA_UPDATING = false;

            }

        }

        private void goodsBagStockUpdate()
        {
            if(!DATA_UPDATING)
            {
                DATA_UPDATING = true;

                int itemPerPackaging = int.TryParse(txtGoodsPcsPerBag.Text, out int i) ? i : 0;
                int itemStock = int.TryParse(txtGoodsStockPcsQty.Text, out i) ? i : 0;

                int packagingQty = 0;

                if (itemPerPackaging > 0)
                {
                    packagingQty = itemStock / itemPerPackaging;
                }

                txtGoodsStockBagQty.Text = packagingQty.ToString();

                DATA_UPDATING = false;
            }

        }

        private void balQtyUpdate()
        {
            double balqty = (double.TryParse(txtContainerQty.Text, out double i) ? i : 0) % 1;

            balqty = Math.Round(balqty, 2);

            if (balqty > 0)
            {
                int containerBalQty = (int)(balqty * (double.TryParse(txtBodyPcsPerContainer.Text, out i) ? i : 0));

                lblMaxBody.Text = balqty + " = " + containerBalQty.ToString() + " pcs";
            }
            else
            {
                lblMaxBody.Text = "-";
            }

            balqty = (double.TryParse(txtBagQty.Text, out  i) ? i : 0) % 1;

            balqty = Math.Round(balqty, 2);

            if (balqty > 0)
            {
                int bagBalQty = (int)(balqty * (double.TryParse(txtGoodsPcsPerBag.Text, out i) ? i : 0));

                lblBagBalQty.Text = balqty + " = " + bagBalQty.ToString() + " pcs";
            }
            else
            {
                lblBagBalQty.Text = "-";
            }
        }

        private void ContainerToBagConvert()
        {
            if(!DATA_UPDATING)
            {
                DATA_UPDATING = true;

                double bodyPerContainer = double.TryParse(txtBodyPcsPerContainer.Text, out double i) ? i : 0;
                double goodsPerBag = double.TryParse(txtGoodsPcsPerBag.Text, out i) ? i : 0;

                double containerQty = double.TryParse(txtContainerQty.Text, out double x) ? x : 0;
                double bodyPcs = double.TryParse(txtBodyPcs.Text, out x) ? x : 0;

                double totalBodyPcs = bodyPerContainer * containerQty + bodyPcs;

                // bag qty = pcs/ctn x ctn qty / ratio / pcs/bag
                double bagQty = totalBodyPcs / BODY_QTY_PER_GOODS / goodsPerBag;

                double goodsPcs = totalBodyPcs / BODY_QTY_PER_GOODS;

                double balGoodsQty = goodsPcs - (int)bagQty* goodsPerBag;

                txtBagQty.Text = bagQty.ToString("0");
                txtGoodsPcs.Text = balGoodsQty.ToString("0");

                //balQtyUpdate();


                DATA_UPDATING = false;
            }

            // ctn qty = bag qty x ratio x pcs/bag / pcs/ctn
        }

      
        private void BagToContainerConvert()
        {
            if (!DATA_UPDATING)
            {
                DATA_UPDATING = true;

                double bodyPerContainer = double.TryParse(txtBodyPcsPerContainer.Text, out double i) ? i : 0;
                double goodsPerBag = double.TryParse(txtGoodsPcsPerBag.Text, out i) ? i : 0;

                double bagQty = double.TryParse(txtBagQty.Text, out i) ? i : 0;
                double goodsPCs = double.TryParse(txtGoodsPcs.Text, out i) ? i : 0;

                double totalGoodsQty = bagQty * goodsPerBag + goodsPCs;

                // ctn qty = bag qty x ratio x pcs/bag / pcs/ctn
                double containerQty = totalGoodsQty * BODY_QTY_PER_GOODS / bodyPerContainer;
                double bodyPcs = totalGoodsQty * BODY_QTY_PER_GOODS;

                double balBodyQty = bodyPcs - (int)containerQty * bodyPerContainer;

                txtContainerQty.Text = containerQty.ToString("0");
                txtBodyPcs.Text = balBodyQty.ToString("0");

                double balqty = containerQty % 1;

                balqty = Math.Round(balqty, 2);

                if (balqty > 0)
                {
                    int containerBalQty = (int)(balqty * (double.TryParse(txtBodyPcsPerContainer.Text, out  i) ? i : 0));

                    lblMaxBody.Text = balqty + " = " +containerBalQty.ToString() + " pcs";
                }
                else
                {
                    lblMaxBody.Text = "-";
                }

                //balQtyUpdate();


                DATA_UPDATING = false;
            }
        }

        private void frmChangeDate_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmChangeDate_Load(object sender, EventArgs e)
        {
            
        }

        private void txtContainerQty_TextChanged(object sender, EventArgs e)
        {
            ContainerToBagConvert();
        }

        private void txtBagQty_TextChanged(object sender, EventArgs e)
        {
            BagToContainerConvert();
        }

        private void txtBodyPcsPerContainer_TextChanged(object sender, EventArgs e)
        {
            bodyContainerStockUpdate();
            ContainerToBagConvert();
        }

        private void txtGoodsPcsPerBag_TextChanged(object sender, EventArgs e)
        {
            goodsBagStockUpdate();
            ContainerToBagConvert();
        }

        static public bool PACKING_INFO_CHANGED = false;

        private void btnBodyPcsPerContainerSave_Click(object sender, EventArgs e)
        {
            joinBLL uJoin = new joinBLL();

            uJoin.join_parent_code = BODY_INFO.item_code;
            uJoin.join_parent_name = BODY_INFO.item_name;
            uJoin.join_child_code = BODY_INFO.packaging_code;
            uJoin.join_child_name = BODY_INFO.packaging_name;

            int perPerPackaging = int.TryParse(txtBodyPcsPerContainer.Text, out int i) ? i : 0;

            uJoin.join_qty = 1;
            uJoin.join_max = perPerPackaging;
            uJoin.join_max = perPerPackaging;

            uJoin.join_main_carton = true;
            uJoin.join_stock_out = true;
          
            frmJoinEdit frm = new frmJoinEdit(uJoin, true,1);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(frmJoinEdit.DATA_UPDATED)
            {
                BODY_INFO.packaging_code = frmJoinEdit.SAVED_JOIN_INFO.join_child_code;
                BODY_INFO.packaging_name = frmJoinEdit.SAVED_JOIN_INFO.join_child_name;

                BODY_INFO.standard_packing_qty = frmJoinEdit.SAVED_JOIN_INFO.join_max;
                txtBodyPcsPerContainer.Text = BODY_INFO.standard_packing_qty.ToString();
                PACKING_INFO_CHANGED = true;
                frmSBBPOVSStock.DB_JOIN_ALL = new joinDAL().SelectAll();

            }
        }

        private void btnGoodsPcsPerBagSave_Click(object sender, EventArgs e)
        {
            joinBLL uJoin = new joinBLL();

            uJoin.join_parent_code = GOODS_INFO.item_code;
            uJoin.join_parent_name = GOODS_INFO.item_name;

            uJoin.join_child_code = GOODS_INFO.packaging_code;
            uJoin.join_child_name = GOODS_INFO.packaging_name;

            int perPerPackaging = int.TryParse(txtGoodsPcsPerBag.Text, out int i) ? i : 0;

            uJoin.join_qty = 1;
            uJoin.join_max = perPerPackaging;
            uJoin.join_max = perPerPackaging;

            uJoin.join_main_carton = true;
            uJoin.join_stock_out = true;

            frmJoinEdit frm = new frmJoinEdit(uJoin, true, 1);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if (frmJoinEdit.DATA_UPDATED)
            {
                GOODS_INFO.packaging_code = frmJoinEdit.SAVED_JOIN_INFO.join_child_code;
                GOODS_INFO.packaging_name = frmJoinEdit.SAVED_JOIN_INFO.join_child_name;

                GOODS_INFO.standard_packing_qty = frmJoinEdit.SAVED_JOIN_INFO.join_max;
                txtGoodsPcsPerBag.Text = GOODS_INFO.standard_packing_qty.ToString();
                PACKING_INFO_CHANGED = true;

                frmSBBPOVSStock.DB_JOIN_ALL = new joinDAL().SelectAll();
            }
        }

        private void isNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtBodyPcs_TextChanged(object sender, EventArgs e)
        {
            ContainerToBagConvert();
        }

        private void txtGoodsPcs_TextChanged(object sender, EventArgs e)
        {
            BagToContainerConvert();

        }

        private void lblMaxBody_Click(object sender, EventArgs e)
        {
            DATA_UPDATING = true;

            double bodyPerContainer = double.TryParse(txtBodyPcsPerContainer.Text, out double i) ? i : 0;
            double bodyStock = double.TryParse(txtBodyStockPcsQty.Text, out i) ? i : 0;

            int ContainerQty = (int) (bodyStock / bodyPerContainer);

            int balBodyPcs = (int)bodyStock - ContainerQty * (int)bodyPerContainer;

            txtContainerQty.Text = ContainerQty.ToString("0");

            txtBodyPcs.Text = balBodyPcs.ToString("0");
            DATA_UPDATING = false;
            ContainerToBagConvert();

        }

        private void lblBagBalQty_Click(object sender, EventArgs e)
        {
            
        }
    }
}
