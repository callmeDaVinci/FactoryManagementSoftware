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
    public partial class frmSPPNewPO : Form
    {
        public frmSPPNewPO()
        {
            InitializeComponent();
            dt_CustomerList = dalData.CustomerWithoutRemovedDataSelect();
            dt_OrderList = NewOrderTable();
        }

        public frmSPPNewPO(string poCode)//edit po
        {
            InitializeComponent();
            dt_CustomerList = dalData.CustomerWithoutRemovedDataSelect();
            dt_OrderList = NewOrderTable();

            btnAdd.Text = "EDIT";
            btnRemove.Visible = true;
            poEditing = true;

            EditingPOCode = poCode;

            LoadExistingPO(poCode);
        }

        #region variable/object declare

        SPPDataDAL dalData = new SPPDataDAL();
        SPPDataBLL uData = new SPPDataBLL();

        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        DataTable dt_CustomerList;
        DataTable dt_OrderList;

        private bool loaded = false;

        private readonly string header_Index = "#";
        private readonly string header_Code = "CODE";
        private readonly string header_Size = "SIZE";
        private readonly string header_Unit = "UNIT";
        private readonly string header_Type = "TYPE";
        private readonly string header_OrderQty = "ORDER QTY(PCS)";
        private readonly string header_Note = "NOTE";

        private string EditingPOCode;

        static public bool poEdited = false;
        static public bool poRemoved = false;
        private bool poEditing = false;

        #endregion

        private DataTable NewOrderTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
           
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));

            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[header_Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }


        private void LoadExistingPO(string poCode)
        {

            DataTable dt = dalData.POSelectWithSizeAndType();
            loadLocationData(dt_CustomerList, cmbCustomer, dalData.FullName);
            foreach (DataRow row in dt.Rows)
            {
                if(poCode == row[dalData.POCode].ToString())
                {
                    cmbCustomer.Text = row[dalData.FullName].ToString();
                    dtpPODate.Text = row[dalData.PODate].ToString();

                    txtAddress1.Text = row[dalData.Address1].ToString();
                    txtAddress2.Text = row[dalData.Address2].ToString();
                    
                    txtCity.Text = row[dalData.AddressCity].ToString();
                    txtState.Text = row[dalData.AddressState].ToString();
                    txtCountry.Text = row[dalData.AddressCountry].ToString();
                    txtPostalCode.Text = row[dalData.AddressPostalCode].ToString();

                    DataRow dt_Row = dt_OrderList.NewRow();

                    dt_Row[header_Size] = row[dalData.SizeNumerator];
                    dt_Row[header_Unit] = row[dalData.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Code] = row[dalData.ItemCode];
                    dt_Row[header_Type] = row[dalData.TypeName];
                    dt_Row[header_OrderQty] = row[dalData.POQty];
                    dt_Row[header_Note] = row[dalData.PONote];

                    dt_OrderList.Rows.Add(dt_Row);

                }
            }

            AddIndexToOrderTable();
            dgvPOItemList.DataSource = dt_OrderList;
            DgvUIEdit(dgvPOItemList);
            dgvPOItemList.ClearSelection();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //btnEdit.Visible = false;
            frmSPPAddItem frm = new frmSPPAddItem(true)
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            if (frmSPPAddItem.itemAdded)
            {
                DataRow dt_Row = dt_OrderList.NewRow();

                dt_Row[header_Size] = frmSPPAddItem.item_Size;
                dt_Row[header_Unit] = "MM";
                dt_Row[header_Code] = frmSPPAddItem.item_Code;
                dt_Row[header_Type] = frmSPPAddItem.item_Type;
                dt_Row[header_OrderQty] = frmSPPAddItem.item_Target_Pcs;
                dt_Row[header_Note] = "("+frmSPPAddItem.item_Target_Bags+" BAGS) " + frmSPPAddItem.item_Note;

                dt_OrderList.Rows.Add(dt_Row);

                AddIndexToOrderTable();
                dgvPOItemList.DataSource = dt_OrderList;
                DgvUIEdit(dgvPOItemList);
                dgvPOItemList.ClearSelection();

                frmSPPAddItem.itemAdded = false;
            }
        }

        private void AddIndexToOrderTable()
        {
            int index = 1;
            foreach(DataRow row in dt_OrderList.Rows)
            {
                row[header_Index] = index;
                index++;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void loadLocationData(DataTable dt, ComboBox cmb, string columnName)
        {
            DataTable lacationTable = dt.DefaultView.ToTable(true, columnName);

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = columnName;

        }
        private void frmSPPNewPO_Load(object sender, EventArgs e)
        {
            if(!poEditing)
            {
                loadLocationData(dt_CustomerList, cmbCustomer, dalData.FullName);
                cmbCustomer.SelectedIndex = -1;
            }
           

            loaded = true;
        }

        private void ClearField()
        {
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtCountry.Text = "MALAYSIA";
            txtPostalCode.Clear();
        }

        private void ShowDataToField(string customerName)
        {
            ClearField();

            if(cbUseDefaultAddress.Checked)
            {
                foreach (DataRow row in dt_CustomerList.Rows)
                {
                    string fullName = row[dalData.FullName] == null ? string.Empty : row[dalData.FullName].ToString();

                    if (fullName == customerName)
                    {
                        string registrationNO = row[dalData.RegistrationNo] == null ? string.Empty : row[dalData.RegistrationNo].ToString();
                        string address1 = row[dalData.Address1] == null ? string.Empty : row[dalData.Address1].ToString();
                        string address2 = row[dalData.Address2] == null ? string.Empty : row[dalData.Address2].ToString();
                        string city = row[dalData.AddressCity] == null ? string.Empty : row[dalData.AddressCity].ToString();
                        string state = row[dalData.AddressState] == null ? string.Empty : row[dalData.AddressState].ToString();
                        string country = row[dalData.AddressCountry] == null ? string.Empty : row[dalData.AddressCountry].ToString();
                        string postalCode = row[dalData.AddressPostalCode] == null ? string.Empty : row[dalData.AddressPostalCode].ToString();


                        txtAddress1.Text = address1;
                        txtAddress2.Text = address2;
                        txtCity.Text = city;
                        txtState.Text = state;
                        txtCountry.Text = country;
                        txtPostalCode.Text = postalCode;

                        break;
                    }
                }
            }
            
        }

        private void AddNewCustomer_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSPPCustomerEdit frm = new frmSPPCustomerEdit
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            dt_CustomerList = dalData.CustomerWithoutRemovedDataSelect();
            loadLocationData(dt_CustomerList, cmbCustomer, dalData.FullName);
            cmbCustomer.SelectedIndex = -1;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
            ShowDataToField(cmbCustomer.Text);
        }

        private void ClearError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            errorProvider8.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to cancel PO edting?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(cbUseDefaultAddress.Checked)
            {
                ShowDataToField(cmbCustomer.Text);
            }
            else
            {
                ClearField();
            }
        }

        private void dgvPOItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Visible = true;
        }

        private void EditOrderItem()
        {
            //get item info
            DataGridView dgv = dgvPOItemList;
            DataTable dt = (DataTable)dgv.DataSource;
            int rowIndex = dgv.CurrentRow.Index;

            //string code, string type, string size, string unit
            string type = dt.Rows[rowIndex][header_Type].ToString();
            string size = dt.Rows[rowIndex][header_Size].ToString();
            string unit = dt.Rows[rowIndex][header_Unit].ToString();
            string orderQty = dt.Rows[rowIndex][header_OrderQty].ToString();

            if (rowIndex >= 0)
            {
                frmSPPAddItem frm = new frmSPPAddItem(type, size, unit, orderQty)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();

                if (frmSPPAddItem.itemEdited)
                {
                    btnEdit.Visible = false;
                    //add data to datarow
                    //add datarow to table
                    dt_OrderList.Rows.RemoveAt(rowIndex);
                    DataRow dt_Row = dt_OrderList.NewRow();

                    dt_Row[header_Size] = frmSPPAddItem.item_Size;
                    dt_Row[header_Unit] = "MM";
                    dt_Row[header_Code] = frmSPPAddItem.item_Code;
                    dt_Row[header_Type] = frmSPPAddItem.item_Type;
                    dt_Row[header_OrderQty] = frmSPPAddItem.item_Target_Pcs;
                    dt_Row[header_Note] = "(" + frmSPPAddItem.item_Target_Bags + " BAGS) " + frmSPPAddItem.item_Note;

                    dt_OrderList.Rows.Add(dt_Row);

                    AddIndexToOrderTable();
                    dgvPOItemList.DataSource = dt_OrderList;
                    DgvUIEdit(dgvPOItemList);
                    dgvPOItemList.ClearSelection();

                    frmSPPAddItem.itemEdited = false;
                }
                else if (frmSPPAddItem.itemRemoved)
                {
                    btnEdit.Visible = false;
                    dt_OrderList.Rows.RemoveAt(rowIndex);
                    dgv.DataSource = dt_OrderList;

                    AddIndexToOrderTable();
                    dgvPOItemList.DataSource = dt_OrderList;
                    DgvUIEdit(dgvPOItemList);
                    dgvPOItemList.ClearSelection();

                    frmSPPAddItem.itemRemoved = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditOrderItem();
        }

        private void dgvPOItemList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditOrderItem();
        }

        private bool Validation()
        {
            ClearError();
            bool result = true;

            if (string.IsNullOrEmpty(cmbCustomer.Text))
            {
                result = false;
                errorProvider1.SetError(lblCustomer, "Customer Required");
            }

            if (string.IsNullOrEmpty(dtpPODate.Text))
            {
                result = false;
                errorProvider2.SetError(lblPODate, "PO Date Required");
            }

            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                result = false;
                errorProvider3.SetError(lblShippingAddress, "Shipping Address Required");
            }

            if (string.IsNullOrEmpty(txtCity.Text))
            {
                result = false;
                errorProvider4.SetError(lblCity, "Address City Required");
            }

            if (string.IsNullOrEmpty(txtState.Text))
            {
                result = false;
                errorProvider5.SetError(lblState, "Address State Required");
            }

            if (string.IsNullOrEmpty(txtPostalCode.Text))
            {
                result = false;
                errorProvider6.SetError(lblPostalCode, "Address Postal Code Required");
            }

            if (string.IsNullOrEmpty(txtCountry.Text))
            {
                result = false;
                errorProvider7.SetError(lblCountry, "Address Country Required");
            }

            
            if (dt_OrderList.Rows.Count <= 0)
            {
                result = false;
                errorProvider8.SetError(lblOrderList, "Order Item Required");
            }

            return result;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                //get customer table code
                int customer_tbl_code = 0;
                string customerFullName = cmbCustomer.Text;

                foreach (DataRow row in dt_CustomerList.Rows)
                {
                    if (row[dalData.FullName].ToString() == customerFullName)
                    {
                        customer_tbl_code = Convert.ToInt32(row[dalData.TableCode].ToString());
                        break;
                    }
                }

                uData.PO_date = dtpPODate.Value;
                uData.Customer_tbl_code = customer_tbl_code;
                uData.Address_1 = txtAddress1.Text.ToUpper();
                uData.Address_2 = txtAddress2.Text.ToUpper();
                uData.Address_City = txtCity.Text.ToUpper();
                uData.Address_State = txtState.Text.ToUpper();
                uData.Address_Country = txtCountry.Text.ToUpper();
                uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();

                uData.IsRemoved = false;
                uData.Updated_Date = DateTime.Now;
                uData.Updated_By = MainDashboard.USER_ID;

                if (poEditing)
                {

                    uData.PO_code = Convert.ToInt32(EditingPOCode);
                    dalData.PODelete(uData);
                }
                else
                {
                    //get po code
                    DataTable dt = dalData.POSelect();

                    int po_code = 0;
                    if (dt.Rows.Count <= 0)
                    {
                        po_code = 1;
                    }
                    else
                    {
                        DataView dv = dt.DefaultView;
                        dv.Sort = dalData.POCode + " desc";
                        DataTable sortedDT = dv.ToTable();

                        po_code = Convert.ToInt32(sortedDT.Rows[0][dalData.POCode].ToString()) + 1;
                    }

                    uData.PO_code = po_code;

                   
                }

                bool failedToInsert = false;
                foreach (DataRow row in dt_OrderList.Rows)
                {
                    string itemCode = row[header_Code].ToString();
                    int po_qty = Convert.ToInt32(row[header_OrderQty]);
                    string note = row[header_Note].ToString();

                    uData.Item_code = itemCode;
                    uData.PO_qty = po_qty;
                    uData.PO_note = note;

                    if (!dalData.InsertPO(uData))
                    {
                        
                        MessageBox.Show("Unable to insert/update " + itemCode + " PO data!");
                        failedToInsert = true;
                    }
                }

                if (!failedToInsert)
                {
                    MessageBox.Show("PO inserted/updated!");

                    if(poEditing)
                    {
                        poEdited = true;
                    }
                    
                    Close();

                }

            }
        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to remove this PO?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                uData.PO_code = Convert.ToInt32(EditingPOCode);

               
                if (dalData.PODelete(uData))
                {
                    poRemoved = true;
                    MessageBox.Show("PO removed!");
                }
                    
               
                Close();
            }
        }
    }
}
