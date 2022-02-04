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
    public partial class frmSBBShippingData : Form
    {
        public frmSBBShippingData()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvShippingList, true);
            //dt_ShippingList = dalData.ShippingDataSelectWithCustCode();
        }

        public frmSBBShippingData(string custID)
        {
            InitializeComponent();

            CUST_TBL_ID = custID;

            tool.DoubleBuffered(dgvShippingList, true);

            dt_ShippingList = dalData.ShippingDataSelectWithCustCode(CUST_TBL_ID);
        }

        public frmSBBShippingData(SBBDataBLL POData)
        {
            InitializeComponent();
            uData = POData;

            CUST_TBL_ID = POData.Customer_tbl_code.ToString();

            tool.DoubleBuffered(dgvShippingList, true);

            dt_ShippingList = dalData.ShippingDataSelectWithCustCode(CUST_TBL_ID);
        }

        SBBDataDAL dalData = new SBBDataDAL();
        SBBDataBLL uData = new SBBDataBLL();

        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        joinDAL dalJoin = new joinDAL();

        DataTable dt_ShippingList;

        int tableID = -1;

        private readonly string header_ID = "ID";
        private readonly string header_Name = "NAME";

        private readonly string TEXT_ROUTE = "ROUTE";
        private readonly string BUTTON_ADD = "ADD";
        private readonly string BUTTON_SELECT = "SELECT";
        private readonly string BUTTON_UPDATE = "UPDATE";
        private string CUST_TBL_ID = "";

        private bool showRemoved = false;

        static public bool dataSelected = false;
        private bool Loaded = false;

        private void frmShippingList_Load(object sender, EventArgs e)
        {
            LoadShippingList();
            CMBLoadRoute(cmbRoute);
            txtShortName.Clear();
            Loaded = true;

            btnAdd.Text = BUTTON_ADD;
        }

        private void CMBLoadRoute(ComboBox cmb)
        {
            DataTable RouteTable = dalData.RouteWithoutRemovedDataSelect();

            RouteTable.DefaultView.Sort = dalData.RouteName + " ASC";

            RouteTable = RouteTable.DefaultView.ToTable();

            cmb.DataSource = RouteTable;
            cmb.ValueMember = dalData.TableCode;
            cmb.DisplayMember = dalData.RouteName;
            cmb.SelectedIndex = -1;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 139, 209);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[header_ID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_ID].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[header_Name].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
           
        }

        private DataTable NewShippingTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_ID, typeof(int));
            dt.Columns.Add(header_Name, typeof(string));

            return dt;
        }

        private void LoadShippingList()
        {

            ClearField();
            ClearError();
            btnAdd.Text = BUTTON_ADD;

            btnCancel.Visible = false;
            btnRemove.Visible = false;

            DataTable dt = NewShippingTable();

            DataRow dt_Row;

            foreach (DataRow row in dt_ShippingList.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(isRemoved == showRemoved)
                {
                    int id = int.TryParse(row[dalData.TableCode].ToString(), out id) ? id : -1;
                    string name = row[dalData.ShippingFullName].ToString();

                    if(id != -1)
                    {
                        dt_Row = dt.NewRow();

                        dt_Row[header_ID] = id;
                        dt_Row[header_Name] = name;
                      
                        dt.Rows.Add(dt_Row);
                    }
                }
            }

            DataGridView dgv = dgvShippingList;
            dgv.DataSource = dt;
            DgvUIEdit(dgv);
            dgv.ClearSelection();

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
            errorProvider9.Clear();

          

        }

        private bool Validation()
        {
            bool result = true;

            string EditMode = btnAdd.Text;

            if (string.IsNullOrEmpty(CUST_TBL_ID))
            {
                result = false;
                MessageBox.Show("Customer ID Invalid!");
            }

            //if (EditMode == "ADD")
            //{
            //    result = !IfNameExist();
            //}
            //else if (EditMode == "EDIT")
            //{
            //    int id = Convert.ToInt32(dgvShippingList.Rows[dgvShippingList.CurrentCell.RowIndex].Cells[header_ID].Value.ToString());
            //    result = !IfNameExist(id.ToString());

            //}

            if (string.IsNullOrEmpty(txtShippingFullName.Text))
            {
                result = false;
                errorProvider1.SetError(lblName, "Customer Name Required");
            }

            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                result = false;
                errorProvider2.SetError(lblAddress1, "Customer Address Required");
            }

            if (string.IsNullOrEmpty(txtCity.Text))
            {
                result = false;
                errorProvider3.SetError(lblCity, "Address City Required");
            }

            if (string.IsNullOrEmpty(txtState.Text))
            {
                result = false;
                errorProvider4.SetError(lblState, "Address State Required");
            }

            if (string.IsNullOrEmpty(txtPostalCode.Text))
            {
                result = false;
                errorProvider5.SetError(lblPostalCode, "Address Postal Code Required");
            }

            if (string.IsNullOrEmpty(txtCountry.Text))
            {
                result = false;
                errorProvider6.SetError(lblCountry, "Address Country Required");
            }


            if (string.IsNullOrEmpty(cmbRoute.Text))
            {
                result = false;
                errorProvider9.SetError(lblRoute, "Route Required");
            }

          
                
            return result;

        }

        //private bool IfNameExist()
        //{
        //    bool result = false;
        //    errorProvider1.Clear();
        //    errorProvider8.Clear();
        //    dt_ShippingList = dalData.CustomerSelect();
        //    string fullName = txtShippingFullName.Text.ToUpper();
        //    string shortName = txtShortName.Text.ToUpper();

        //    foreach(DataRow row in dt_ShippingList.Rows)
        //    {
        //        string DB_FullName = row[dalData.FullName] == null ? string.Empty : row[dalData.FullName].ToString();
        //        string DB_ShortName = row[dalData.ShortName] == null ? string.Empty : row[dalData.ShortName].ToString();

        //        if(fullName == DB_FullName)
        //        {
        //            errorProvider1.SetError(lblName, "full name used!");
        //            result = true;
        //        }

        //        if (shortName == DB_ShortName)
        //        {
        //            errorProvider8.SetError(lblShortName, "short name used!");
        //            result = true;
        //        }

        //        if(result)
        //        {
        //            return true;
        //        }
        //    }

           
        //    return result;
        //}

        //private bool IfNameExist(string customerID)
        //{
        //    bool result = false;

        //    dt_ShippingList = dalData.CustomerSelect();
        //    string fullName = txtShippingFullName.Text;
        //    string shortName = txtShortName.Text;

        //    foreach (DataRow row in dt_ShippingList.Rows)
        //    {
        //        int id = int.TryParse(row[dalData.TableCode].ToString(), out id) ? id : -1;

        //        if(id != -1 && id.ToString() != customerID)
        //        {
        //            string DB_FullName = row[dalData.FullName] == null ? string.Empty : row[dalData.FullName].ToString();
        //            string DB_ShortName = row[dalData.ShortName] == null ? string.Empty : row[dalData.ShortName].ToString();

        //            if (fullName == DB_FullName)
        //            {
        //                errorProvider1.SetError(lblName, "full name used!");
        //                result = true;
        //            }

        //            if (shortName == DB_ShortName)
        //            {
        //                errorProvider8.SetError(lblName, "short name used!");
        //                result = true;
        //            }

        //            if (result)
        //            {
        //                return true;
        //            }
        //        }
                
        //    }

        //    return result;
        //}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                string EditMode = btnAdd.Text;

                if(EditMode == BUTTON_ADD)
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm to add new shipping data to the list?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        uData.Customer_tbl_code = int.TryParse(CUST_TBL_ID, out int custID)? custID : 0 ;

                        uData.Shipping_Full_Name = txtShippingFullName.Text.ToUpper();
                        uData.Shipping_Short_Name = txtShortName.Text.ToUpper();
                        uData.Address_1 = txtAddress1.Text.ToUpper();
                        uData.Address_2 = txtAddress2.Text.ToUpper();
                        uData.Address_3 = txtAddress3.Text.ToUpper();
                        uData.Address_City = txtCity.Text.ToUpper();
                        uData.Address_State = txtState.Text.ToUpper();
                        uData.Address_Country = txtCountry.Text.ToUpper();
                        uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();
                        uData.Phone_1 = txtPhone1.Text.ToUpper();
                        uData.Shipping_Transporter = txtTransporter.Text.ToUpper();
                        uData.Cust_Own_DO = cbCustOwnDO.Checked;

                        uData.Updated_Date = DateTime.Now;
                        uData.Updated_By = MainDashboard.USER_ID;

                        DataTable dt = (DataTable)cmbRoute.DataSource;

                        int index = cmbRoute.SelectedIndex;

                        
                        int tableCode = int.TryParse(dt.Rows[index][dalData.TableCode].ToString(), out tableCode)? tableCode : -1;

                        if(tableCode > 0)
                        {
                            uData.Route_tbl_code = tableCode;

                        }
                        else
                        {
                            uData.Route_tbl_code = 0;
                        }

                        if (dalData.InsertCustomerShippingData(uData))
                        {
                            MessageBox.Show("New shipping data inserted!");
                            dt_ShippingList = dalData.ShippingDataSelectWithCustCode(CUST_TBL_ID);
                            LoadShippingList();
                            ClearField();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert new shipping data!");
                        }
                    }
                        
                }
                else if (EditMode == BUTTON_UPDATE)
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm to update shipping info?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes && tableID != -1)
                    {
                        uData.Table_Code = tableID;


                        uData.Customer_tbl_code = int.TryParse(CUST_TBL_ID, out int custID) ? custID : 0;

                        uData.Shipping_Full_Name = txtShippingFullName.Text.ToUpper();
                        uData.Shipping_Short_Name = txtShortName.Text.ToUpper();
                        uData.Address_1 = txtAddress1.Text.ToUpper();
                        uData.Address_2 = txtAddress2.Text.ToUpper();
                        uData.Address_3 = txtAddress3.Text.ToUpper();
                        uData.Address_City = txtCity.Text.ToUpper();
                        uData.Address_State = txtState.Text.ToUpper();
                        uData.Address_Country = txtCountry.Text.ToUpper();
                        uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();
                        uData.Phone_1 = txtPhone1.Text.ToUpper();
                        uData.Shipping_Transporter = txtTransporter.Text.ToUpper();
                        uData.Cust_Own_DO = cbCustOwnDO.Checked;

                        uData.IsRemoved = false;
                        uData.Updated_Date = DateTime.Now;
                        uData.Updated_By = MainDashboard.USER_ID;

                        DataTable dt = (DataTable)cmbRoute.DataSource;

                        int index = cmbRoute.SelectedIndex;
                        int tableCode = int.TryParse(dt.Rows[index][dalData.TableCode].ToString(), out tableCode) ? tableCode : -1;

                        if (tableCode > 0)
                        {
                            uData.Route_tbl_code = tableCode;

                        }
                        else
                        {
                            uData.Route_tbl_code = 0;
                        }

                        if (dalData.ShippingDataUpdate(uData))
                        {
                            MessageBox.Show("Shipping data Updated!");
                            dt_ShippingList = dalData.ShippingDataSelectWithCustCode(CUST_TBL_ID);
                            LoadShippingList();
                            ClearField();
                            tableID = -1;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update shipping data!");
                        }
                    }
                }
                else if (EditMode == BUTTON_SELECT)
                {
                    uData.Shipping_Full_Name = txtShippingFullName.Text.ToUpper();
                    uData.Shipping_Short_Name = txtShortName.Text.ToUpper();
                    uData.Address_1 = txtAddress1.Text.ToUpper();
                    uData.Address_2 = txtAddress2.Text.ToUpper();
                    uData.Address_3 = txtAddress3.Text.ToUpper();
                    uData.Address_City = txtCity.Text.ToUpper();
                    uData.Address_State = txtState.Text.ToUpper();
                    uData.Address_Country = txtCountry.Text.ToUpper();
                    uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();
                    uData.Phone_1 = txtPhone1.Text.ToUpper();
                    uData.Shipping_Transporter = txtTransporter.Text.ToUpper();
                    uData.Cust_Own_DO = cbCustOwnDO.Checked;

                    dataSelected = true;

                    Close();
                }
            }
        }

        private void ClearField()
        {
            txtShippingFullName.Clear();
            txtShortName.Clear();
  
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtAddress3.Clear();
            txtShortName.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtCountry.Text = "MALAYSIA";
            txtPostalCode.Clear();
            txtPhone1.Clear();

            txtTransporter.Clear();
            cmbRoute.SelectedIndex = -1;

        }

        private void ShowDataToField(int selectedID)
        {
            ClearField();

            foreach (DataRow row in dt_ShippingList.Rows)
            {
                int id = int.TryParse(row[dalData.TableCode].ToString(), out id) ? id : -1;

                if(id == selectedID)
                {
                    string name = row[dalData.ShippingFullName] == null ? string.Empty : row[dalData.ShippingFullName].ToString();
                    string shortName = row[dalData.ShippingShortName] == null ? string.Empty : row[dalData.ShippingShortName].ToString();
                 
                    string address1 = row[dalData.Address1] == null ? string.Empty : row[dalData.Address1].ToString();
                    string address2 =  row[dalData.Address2] == null ? string.Empty : row[dalData.Address2].ToString();
                    string address3 =  row[dalData.Address3] == null ? string.Empty : row[dalData.Address3].ToString();
                    string city =  row[dalData.AddressCity] == null ? string.Empty : row[dalData.AddressCity].ToString();
                    string state =  row[dalData.AddressState] == null ? string.Empty : row[dalData.AddressState].ToString();
                    string country =  row[dalData.AddressCountry] == null ? string.Empty : row[dalData.AddressCountry].ToString();
                    string postalCode =  row[dalData.AddressPostalCode] == null ? string.Empty : row[dalData.AddressPostalCode].ToString();
                    string phone1 =  row[dalData.Phone1] == null ? string.Empty : row[dalData.Phone1].ToString();
                    string transporter =  row[dalData.ShippingTransporter] == null ? string.Empty : row[dalData.ShippingTransporter].ToString();

                    bool custOwnDO = bool.TryParse(row[dalData.CustOwnDO].ToString(), out custOwnDO) ? custOwnDO : false;



                    string routeTableCode = row[dalData.RouteTblCode] == null ? string.Empty : row[dalData.RouteTblCode].ToString();

                    DataTable dt = dalData.RouteWithoutRemovedDataSelect();

                    string routeName = "";

                    foreach(DataRow routeRow in dt.Rows)
                    {
                        if(routeTableCode == routeRow[dalData.TableCode].ToString())
                        {
                            routeName = routeRow[dalData.RouteName].ToString();
                            break;
                        }
                    }

                    cmbRoute.Text = routeName;
                    txtShippingFullName.Text = name;
                    txtShortName.Text = shortName;
                   
                    txtAddress1.Text = address1;
                    txtAddress2.Text = address2;
                    txtAddress3.Text = address3;
                    txtCity.Text = city;
                    txtState.Text = state;
                    txtCountry.Text = country;
                    txtPostalCode.Text = postalCode;
                    txtPhone1.Text = phone1;
                    txtTransporter.Text = transporter;

                    cbCustOwnDO.Checked = custOwnDO;

                    break;
                }
               
              
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvShippingList;

            if(rowIndex >= 0)
            {
                //get id
                int id = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_ID].Value.ToString());
                tableID = id;
                ShowDataToField(id);

                btnAdd.Text = BUTTON_SELECT;
                btnCancel.Visible = true;
                btnRemove.Visible = true;
           
            }
            else
            {
                ClearField();

                btnAdd.Text = "ADD";
                btnCancel.Visible = false;
                btnRemove.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if(btnAdd.Text == BUTTON_UPDATE)
            {
                DialogResult dialogResult = MessageBox.Show("Confirm to cancel customer info editing?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    ClearField();
                    ClearError();
                    btnAdd.Text = "ADD";
                    btnCancel.Visible = false;
                    btnRemove.Visible = false;
                    dgvShippingList.ClearSelection();
                    tableID = -1;
                }


            }
            else if(btnAdd.Text == BUTTON_ADD)
            {
             
                Close();
            }
            else if (btnAdd.Text == BUTTON_SELECT)
            {
                ClearField();
                ClearError();
                btnAdd.Text = "ADD";
                btnCancel.Visible = false;
                btnRemove.Visible = false;
                dgvShippingList.ClearSelection();
                tableID = -1;
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to delete?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes && tableID != -1)
            {
                uData.Table_Code = tableID;
                uData.IsRemoved = true;
                uData.Updated_Date = DateTime.Now;
                uData.Updated_By = MainDashboard.USER_ID;

                if (dalData.ShippingRemove(uData))
                {
                    MessageBox.Show("Deleted!");
                    dt_ShippingList = dalData.ShippingDataSelectWithCustCode(CUST_TBL_ID);
                    LoadShippingList();
                    ClearField();
                    tableID = -1;
                }
                else
                {
                    MessageBox.Show("Failed to delete data!");
                }
            }
        }

        private void cbShowRemovedDataOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowRemovedDataOnly.Checked)
            {
                showRemoved = true;
            }
            else
            {
                showRemoved = false;
            }
            LoadShippingList();
        }

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgvShippingList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
            }

            if (rowIndex >= 0)
            {
                //get id
                int id = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_ID].Value.ToString());
                tableID = id;
                ShowDataToField(id);

                btnAdd.Text = "EDIT";
                btnCancel.Visible = true;
                btnRemove.Visible = true;

            }
            else
            {
                ClearField();

                btnAdd.Text = "ADD";
                btnCancel.Visible = false;
                btnRemove.Visible = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ClearError();

            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

            if(!string.IsNullOrEmpty(txtShippingFullName.Text))
            {
                string shortName = txtShippingFullName.Text.Split(' ').First().ToUpper(); 

                int stringWidth = (int)txtShippingFullName.CreateGraphics().MeasureString(shortName, txtShippingFullName.Font).Width;

                if(stringWidth > 140)
                {
                    string newShortName = "";

                    int newStringWidth = (int)txtShippingFullName.CreateGraphics().MeasureString(newShortName, txtShippingFullName.Font).Width;

                    int i = 0;

                    while(newStringWidth < 130 && i < shortName.Length)
                    {
                        newShortName += shortName[i].ToString();

                        i++;

                        newStringWidth = (int)txtShippingFullName.CreateGraphics().MeasureString(newShortName, txtShippingFullName.Font).Width;
                    }

                    shortName = newShortName;
                }

                txtShortName.Text = shortName;

                string EditMode = btnAdd.Text;

               
            }
            else
            {
                txtShortName.Text = "";
            }
            

        }

        private void txtShortName_TextChanged(object sender, EventArgs e)
        {

            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;


        }

        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError();
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

            //if(Loaded)
            //{
            //    DataTable dt = (DataTable)cmbRoute.DataSource;

            //    int index = cmbRoute.SelectedIndex;

            //    string name = dt.Rows[index][dalData.RouteName].ToString();
            //    string tableCode = dt.Rows[index][dalData.TableCode].ToString();
            //    MessageBox.Show("("+tableCode+")"+name);
            //}

        }

        private void txtAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtAddress1.CreateGraphics().MeasureString(txtAddress1.Text, txtAddress1.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtAddress1.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtAddress1.Font).Width;
            }

            int maxWidth = 320;

            if (stringWidth > maxWidth && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtAddress2.CreateGraphics().MeasureString(txtAddress2.Text, txtAddress2.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtAddress2.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtAddress2.Font).Width;
            }

            int maxWidth = 320;

            if (stringWidth > maxWidth && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtaddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtAddress3.CreateGraphics().MeasureString(txtAddress3.Text, txtAddress3.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtAddress3.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtAddress3.Font).Width;
            }

            int maxWidth = 320;

            if (stringWidth > maxWidth && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtShippingFullName.CreateGraphics().MeasureString(txtShippingFullName.Text, txtShippingFullName.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtShippingFullName.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtShippingFullName.Font).Width;
            }

            int maxWidth = 420;

            if (stringWidth > maxWidth && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            int stringWidth = (int)txtShortName.CreateGraphics().MeasureString(txtShortName.Text, txtShortName.Font).Width;

            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                stringWidth += (int)txtShortName.CreateGraphics().MeasureString(e.KeyChar.ToString(), txtShortName.Font).Width;
            }

            int maxWidth = 140;

            if (stringWidth > maxWidth && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtAddress3_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtPostalCode_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtPhone1_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void txtTransporter_TextChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;

        }

        private void frmSBBShippingData_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void cbCustOwnDO_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAdd.Text == BUTTON_SELECT)
                btnAdd.Text = BUTTON_UPDATE;
        }
    }

}
