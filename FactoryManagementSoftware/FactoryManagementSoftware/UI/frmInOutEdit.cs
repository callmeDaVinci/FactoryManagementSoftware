using System;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutEdit : Form
    {
        public frmInOutEdit()
        {
            InitializeComponent();
        }

        #region validation

        private float unitCheck()
        {
            float qty = Convert.ToSingle(txtTrfQty.Text);
            
            //TO-DO

            return qty;
        }

        #endregion

        #region transfer action

        private void supplierRecord(string supplierName, string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void customerRecord(string customerName, string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void materialUsedRecord(string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void stockIn(string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void stockOut(string factoryName, string itemCode, float qty)
        {
            //TO-DO
        }

        private void childStockOut(string factoryName, string parentItemCode, float qty)
        {
            //TO-DO

            //LOOP
            string childItemCode = parentItemCode;//get child itemcode from parent
            stockOut(factoryName, childItemCode, qty);
            //LOOP
        }

        private void transferAction()
        {
            string category = cmbTrfItemCat.Text;
            string fromCat = cmbTrfFromCategory.Text;
            string from = cmbTrfFrom.Text;
            string toCat = cmbTrfToCategory.Text;
            string to = cmbTrfTo.Text;
            string itemCode = cmbTrfItemCode.Text;
            float qty = unitCheck();

            if(category.Equals("Part"))//part 
            {
                if(fromCat.Equals("Production") && toCat.Equals("Factory"))
                {
                    //factory stock in (part)
                    stockIn(to,itemCode,qty);
                }
                else if(fromCat.Equals("Factory"))
                {
                    if(toCat.Equals("Factory"))
                    {
                        //factory stock out (part) from
                        stockOut(from, itemCode, qty);
                        //factory stock in (part) to
                        stockIn(to, itemCode, qty);
                    }
                    else if(toCat.Equals("Customer"))
                    {
                        //customer record update (daily delivery record)
                        customerRecord(to,from,itemCode,qty);
                        //factory stock out (part)
                        stockOut(from, itemCode, qty);
                    }
                    else
                    {
                        MessageBox.Show("no action under (from factory record)");
                    }
                }
                else if(fromCat.Equals("Assembly") && toCat.Equals("Factory"))
                {
                    //factory stock out (child part) from (to factory)
                    childStockOut(to, itemCode, qty);
                    //factory stock in (parent part) to
                    stockIn(to, itemCode, qty);
                }
                else
                {
                    MessageBox.Show("no action under (part category)");
                }
            }
            else//material
            {
                if (fromCat.Equals("Supplier") && toCat.Equals("Factory"))
                {
                    //Supplier Record
                    supplierRecord(from,to, itemCode, qty);
                    //Factory stock in (material) to
                    stockIn(to, itemCode, qty);
                }
                else if (fromCat.Equals("Factory"))
                {
                    if (toCat.Equals("Factory"))
                    {
                        //factory stock out (material) from
                        stockOut(from, itemCode, qty);
                        //factory stock in (material) to
                        stockIn(to, itemCode, qty);
                    }
                    else if (toCat.Equals("Production"))
                    {
                        //material used record update (daily delivery record)
                        materialUsedRecord(from, itemCode, qty);
                        //factory stock out (material)
                        stockOut(from, itemCode, qty);
                    }
                    else
                    {
                        MessageBox.Show("no action under (from factory record)");
                    }
                }
                else
                {
                    MessageBox.Show("no action under (material category)");
                }
            }

        }

        #endregion

        #region function:transfer/cancel

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            Cursor = Cursors.Arrow; // change cursor to normal type

            //string locationFrom = "";
            //string locationTo = "";

            //if (Validation())
            //{
            //    DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        if (!string.IsNullOrEmpty(cmbTrfFrom.Text))
            //        {
            //            locationFrom = cmbTrfFrom.Text;
            //        }
            //        else
            //        {
            //            locationFrom = cmbTrfFromCategory.Text;
            //        }

            //        if (!string.IsNullOrEmpty(cmbTrfTo.Text))
            //        {
            //            locationTo = cmbTrfTo.Text;
            //        }
            //        else
            //        {
            //            locationTo = cmbTrfToCategory.Text;
            //        }

            //        utrfHist.trf_hist_item_code = cmbTrfItemCode.Text;
            //        utrfHist.trf_hist_item_name = cmbTrfItemName.Text;
            //        utrfHist.trf_hist_from = locationFrom;
            //        utrfHist.trf_hist_to = locationTo;
            //        utrfHist.trf_hist_qty = checkQty(txtTrfQty.Text);
            //        utrfHist.trf_hist_unit = checkUnit(cmbTrfQtyUnit.Text);
            //        utrfHist.trf_hist_trf_date = dtpTrfDate.Value.Date;
            //        utrfHist.trf_hist_note = txtTrfNote.Text;
            //        utrfHist.trf_hist_added_date = DateTime.Now;
            //        utrfHist.trf_hist_added_by = 0;

            //        //Inserting Data into Database
            //        bool success = daltrfHist.Insert(utrfHist);
            //        //If the data is successfully inserted then the value of success will be true else false
            //        if (success == true)
            //        {
            //            //Data Successfully Inserted
            //            //MessageBox.Show("Transfer record successfully created");

            //            stockInandOut();
            //            cmbTrfQtyUnit.SelectedIndex = -1;

            //            if (!string.IsNullOrEmpty(cmbTrfItemCode.Text))
            //            {
            //                refreshList(cmbTrfItemCode.Text);
            //            }


            //        }
            //        else
            //        {
            //            //Failed to insert data
            //            MessageBox.Show("Failed to add new transfer record");
            //        }

            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
