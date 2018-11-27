using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
        }

        #region variable declare
        private string presentValue = "";
        private int selectedOrderID = -1;
        static public string selectedOrderQty = "";
        static public string selectedItemCode = "";
        static public string selectedUnit = "";
        static public string selectedTo = "";
        static public string receivedLocation;
        static public bool receivedStockIn = false;
        static public bool receivedStockOut = false;
        #endregion

        #region create class object (database)
        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        #endregion

        #region Load or Reset Form

        private void frmOrder_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ordFormOpen = false;
        }

        private void loadOrderRecord()
        {
            DataTable dt = dalOrd.Select();
            dt.DefaultView.Sort = "ord_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();
            dgvOrd.Rows.Clear();
            ((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;

            foreach (DataRow ord in sortedDt.Rows)
            {
                int n = dgvOrd.Rows.Add();
                dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
                dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
                dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
                //dgvOrd.Rows[n].Cells["item_ord"].Value = ord["item_ord"].ToString();    
                dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
                dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
                dgvOrd.Rows[n].Cells["to"].Value = ord["ord_to"].ToString();
                dgvOrd.Rows[n].Cells["ord_forecast_date"].Value = Convert.ToDateTime(ord["ord_forecast_date"]).ToString("dd/MM/yyyy"); ;
                dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
                dgvOrd.Rows[n].Cells["ord_added_by"].Value = ord["ord_added_by"].ToString();
                dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();
            }
            listPaint(dgvOrd);
        }

        private void refreshOrderRecord(int orderID)
        {
            loadOrderRecord();

            dgvOrd.ClearSelection();
            if (selectedOrderID != -1)
            {
                foreach (DataGridViewRow row in dgvOrd.Rows)
                {
                    if (Convert.ToInt32(row.Cells["ord_id"].Value.ToString()).Equals(orderID))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }

        }

        private void resetForm()
        {
            loadOrderRecord();
            txtOrdSearch.Clear();
        }

        private void comboBoxPaint(DataGridView dgv, int rowIndex)
        {
            string value = dgv.Rows[rowIndex].Cells["ord_status"].Value.ToString();
            Color backColor = this.dgvOrd.DefaultCellStyle.BackColor;

            if (value.Equals("Requesting"))
            {
                //backColor = Color.FromArgb(210, 163, 30);
                backColor = Color.FromArgb(251, 188, 5);
            }
            else if (value.Equals("Cancelled"))
            {
                //backColor = Color.FromArgb(244, 67, 54);
                backColor = Color.FromArgb(234, 67, 53);
            }
            else if (value.Equals("Approved"))
            {
                //backColor = Color.FromArgb(104, 125, 222);
                backColor = Color.FromArgb(66, 133, 244);
            }
            else if (value.Equals("Received"))
            {
                //backColor = Color.FromArgb(104, 189, 101);
                backColor = Color.FromArgb(52, 168, 83)         ;
            }

            dgv.Rows[rowIndex].Cells["ord_status"].Style = new DataGridViewCellStyle { ForeColor = SystemColors.Control,BackColor = backColor };

        }

        private void listPaint(DataGridView dgv)
        {
            bool rowColorChange = true;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = SystemColors.Control;
                    rowColorChange = false;
                }
                else
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }
                if(dgv == dgvOrd)
                {
                    comboBoxPaint(dgv, n);
                }
                
            }
            dgv.ClearSelection();
        }

        private DataTable getOrderStatusTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status", typeof(string));
            dt.Rows.Add("Requesting");
            dt.Rows.Add("Cancelled");
            dt.Rows.Add("Approved");
            dt.Rows.Add("Received");
            return dt;
        }

        #endregion

        #region data Insert/Update/Search

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrderInput frm = new frmOrderInput();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(frmOrderInput.orderSuccess)
            {
                resetForm();
            }
        }

        private void dgvOrd_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combobox = e.Control as ComboBox;

            if (combobox != null)
            {
                combobox.SelectedIndexChanged -= new EventHandler(SelectedIndexChanged);

                combobox.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);

                 e.CellStyle.BackColor = this.dgvOrd.DefaultCellStyle.BackColor;
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        private void cancelReceive(string selectedItem)
        {
            frmReceiveCancel frm = new frmReceiveCancel();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//stock out

            if (receivedStockOut)
            {
                if (!dalItem.updateTotalStock(selectedItemCode))
                {
                    MessageBox.Show("Failed to update total stock");
                }
                else
                {
                    if (selectedItem.Equals("Approved"))
                    {
                        orderAdd(selectedItemCode, selectedOrderQty);
                    }

                    dalOrd.Update(uOrd);
                }
                receivedStockOut = false;
            } 
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string selectedItem = combo.SelectedItem.ToString();
            if (!presentValue.Equals(selectedItem))//if selected text change(before!=after)
            {
                uOrd.ord_id = selectedOrderID;
                uOrd.ord_status = selectedItem;

                if (selectedItem.Equals("Received"))
                {
                    frmReceiveConfirm frm = new frmReceiveConfirm();
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (presentValue.Equals("Requesting") || presentValue.Equals("Cancelled"))
                    {
                        MessageBox.Show("This order not approve yet!");
                        refreshOrderRecord(selectedOrderID);
                    }
                    else if (presentValue.Equals("Approved"))
                    {
                        frm.ShowDialog();//stock in

                        if(receivedStockIn)
                        {
                            if(!dalItem.updateTotalStock(selectedItemCode))
                            {
                                MessageBox.Show("Failed to update total stock");
                            }
                            else
                            {
                                uOrd.ord_to = receivedLocation;
                                dalOrd.Update(uOrd);
                                orderSubtract(selectedItemCode, selectedOrderQty);
                            }
                            receivedStockIn = false;
                            
                        }
                                        
                        refreshOrderRecord(selectedOrderID);
                    }
                    
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to change the order status?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        switch (selectedItem)
                        {
                            case "Cancelled":
                                if (presentValue.Equals("Approved"))
                                {
                                    orderSubtract(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    //dialog stock out
                                    cancelReceive(selectedItem);
                                }
                                else if(presentValue.Equals("Requesting"))
                                {
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                break;

                            case "Approved":
                                if (presentValue.Equals("Requesting"))
                                {
                                    orderAdd(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Cancelled"))
                                {
                                    orderAdd(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Received"))
                                {
                      
                                    cancelReceive(selectedItem);
                                    refreshOrderRecord(selectedOrderID);

                                }
                                break;

                            case "Requesting":
                                if (presentValue.Equals("Approved"))
                                {
                                    orderSubtract(selectedItemCode, selectedOrderQty);
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                else if (presentValue.Equals("Received"))
                                {
                                    cancelReceive(selectedItem);
                                }
                                else if (presentValue.Equals("Cancelled"))
                                {
                                    dalOrd.Update(uOrd);
                                    refreshOrderRecord(selectedOrderID);
                                }
                                break;

                            default:
                                break;
                        }
                        refreshOrderRecord(selectedOrderID);
                    }
                }
            }

        }

        private void dgvOrd_CellEnter(object sender, DataGridViewCellEventArgs e)//activate combobox on first click
        {

            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                int rowIndex = e.RowIndex;
                presentValue = dgvOrd.Rows[rowIndex].Cells["ord_status"].Value.ToString();
                selectedOrderID = Convert.ToInt32(dgvOrd.Rows[rowIndex].Cells["ord_id"].Value.ToString());
                selectedItemCode = dgvOrd.Rows[rowIndex].Cells["ord_item_code"].Value.ToString();
                selectedOrderQty = dgvOrd.Rows[rowIndex].Cells["ord_qty"].Value.ToString();
                selectedUnit = dgvOrd.Rows[rowIndex].Cells["ord_unit"].Value.ToString();

                selectedTo = dgvOrd.Rows[rowIndex].Cells["to"].Value == null? "": dgvOrd.Rows[rowIndex].Cells["to"].Value.ToString();
                receivedLocation = selectedTo;
                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;

                //MessageBox.Show(presentValue);

            }
        }

        private void txtOrdSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtOrdSearch.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                DataTable dt = dalOrd.Search(keywords);
                dt.DefaultView.Sort = "ord_added_date DESC";
                DataTable sortedDt = dt.DefaultView.ToTable();
                dgvOrd.Rows.Clear();
                ((DataGridViewComboBoxColumn)dgvOrd.Columns["ord_status"]).ReadOnly = false;

                foreach (DataRow ord in sortedDt.Rows)
                {
                    int n = dgvOrd.Rows.Add();
                    dgvOrd.Rows[n].Cells["ord_id"].Value = ord["ord_id"].ToString();
                    dgvOrd.Rows[n].Cells["ord_item_code"].Value = ord["ord_item_code"].ToString();
                    dgvOrd.Rows[n].Cells["item_name"].Value = ord["item_name"].ToString();
                    dgvOrd.Rows[n].Cells["item_ord"].Value = ord["item_ord"].ToString();
                    dgvOrd.Rows[n].Cells["ord_qty"].Value = ord["ord_qty"].ToString();
                    dgvOrd.Rows[n].Cells["ord_unit"].Value = ord["ord_unit"].ToString();
                    dgvOrd.Rows[n].Cells["ord_forecast_date"].Value = Convert.ToDateTime(ord["ord_forecast_date"]).ToString("dd/MM/yyyy"); ;
                    dgvOrd.Rows[n].Cells["ord_added_date"].Value = ord["ord_added_date"].ToString();
                    dgvOrd.Rows[n].Cells["ord_added_by"].Value = ord["ord_added_by"].ToString();
                    dgvOrd.Rows[n].Cells["ord_status"].Value = ord["ord_status"].ToString();
                }

            }
            else
            {
                //show all item from the database
                loadOrderRecord();
            }
            listPaint(dgvOrd);
        }

        private void orderAdd(string itemCode, string ordQty)
        {
            bool success = dalItem.orderAdd(itemCode, ordQty);//Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to updated item");//failed to update user
            }
        }

        private void orderSubtract(string itemCode, string ordQty)
        {
            bool success = dalItem.orderSubtract(itemCode, ordQty); //Updating data into database

            if (!success)
            {
                MessageBox.Show("Failed to updated item");//failed to update user
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void frmOrder_Click(object sender, EventArgs e)
        {
            dgvOrd.ClearSelection();
        }

        #endregion
    }
}
