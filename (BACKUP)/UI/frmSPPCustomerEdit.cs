﻿using FactoryManagementSoftware.DAL;
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
    public partial class frmSPPCustomerEdit : Form
    {
        public frmSPPCustomerEdit()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvCustomer, true);
            dt_Customer = dalData.CustomerSelect();
        }

        SPPDataDAL dalData = new SPPDataDAL();
        SPPDataBLL uData = new SPPDataBLL();

        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        joinDAL dalJoin = new joinDAL();

        DataTable dt_Customer;
        int tableID = -1;

        private readonly string header_ID = "ID";
        private readonly string header_Name = "NAME";
        private bool showRemoved = false;

        private void frmSPPCustomerEdit_Load(object sender, EventArgs e)
        {
            LoadCustomer();
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

        private DataTable NewCustomerTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_ID, typeof(int));
            dt.Columns.Add(header_Name, typeof(string));

            return dt;
        }

        private void LoadCustomer()
        {

            ClearField();
            ClearError();
            btnEdit.Text = "ADD";
            btnCancel.Visible = false;
            btnRemove.Visible = false;

            DataTable dt = NewCustomerTable();
            DataRow dt_Row;
            foreach (DataRow row in dt_Customer.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(isRemoved == showRemoved)
                {
                    int id = int.TryParse(row[dalData.TableCode].ToString(), out id) ? id : -1;
                    string name = row[dalData.FullName].ToString();

                    if(id != -1)
                    {
                        dt_Row = dt.NewRow();

                        dt_Row[header_ID] = id;
                        dt_Row[header_Name] = name;
                      
                        dt.Rows.Add(dt_Row);
                    }
                }
            }

            DataGridView dgv = dgvCustomer;
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
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                result = false;
                errorProvider1.SetError(lblName, "Customer Name Required");
            }

            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                result = false;
                errorProvider2.SetError(lblAddress, "Customer Address Required");
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

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                result = false;
                errorProvider7.SetError(lblEmail, "Customer Email Required");
            }

            return result;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                string EditMode = btnEdit.Text;

                if(EditMode == "ADD")
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm to add new customer to the list?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        uData.Full_Name = txtName.Text.ToUpper();
                        uData.Short_Name = uData.Full_Name.Split(' ').First().ToUpper();
                        uData.Registration_No = txtRegistrationNo.Text.ToUpper();
                        uData.Address_1 = txtAddress1.Text.ToUpper();
                        uData.Address_2 = txtAddress2.Text.ToUpper();
                        uData.Address_City = txtCity.Text.ToUpper();
                        uData.Address_State = txtState.Text.ToUpper();
                        uData.Address_Country = txtCountry.Text.ToUpper();
                        uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();
                        uData.Phone_1 = txtPhone1.Text.ToUpper();
                        uData.Phone_2 = txtPhone2.Text.ToUpper();
                        uData.Fax = txtFax.Text.ToUpper();
                        uData.Email = txtEmail.Text.ToUpper();
                        uData.Website = txtWebsite.Text.ToUpper();
                        uData.Updated_Date = DateTime.Now;
                        uData.Updated_By = MainDashboard.USER_ID;

                        if (dalData.InsertCustomer(uData))
                        {
                            MessageBox.Show("New Customer Inserted!");
                            dt_Customer = dalData.CustomerSelect();
                            LoadCustomer();
                            ClearField();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert new customer!");
                        }
                    }
                        
                }
                else if (EditMode == "EDIT")
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm to update customer info?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes && tableID != -1)
                    {
                        uData.Table_Code = tableID;
                        uData.Full_Name = txtName.Text.ToUpper();
                        uData.Short_Name = uData.Full_Name.Split(' ').First().ToUpper();
                        uData.Registration_No = txtRegistrationNo.Text.ToUpper();
                        uData.Address_1 = txtAddress1.Text.ToUpper();
                        uData.Address_2 = txtAddress2.Text.ToUpper();
                        uData.Address_City = txtCity.Text.ToUpper();
                        uData.Address_State = txtState.Text.ToUpper();
                        uData.Address_Country = txtCountry.Text.ToUpper();
                        uData.Address_Postal_Code = txtPostalCode.Text.ToUpper();
                        uData.Phone_1 = txtPhone1.Text.ToUpper();
                        uData.Phone_2 = txtPhone2.Text.ToUpper();
                        uData.Fax = txtFax.Text.ToUpper();
                        uData.Email = txtEmail.Text.ToUpper();
                        uData.Website = txtWebsite.Text.ToUpper();
                        uData.IsRemoved = false;
                        uData.Updated_Date = DateTime.Now;
                        uData.Updated_By = MainDashboard.USER_ID;

                        if (dalData.CustomerUpdate(uData))
                        {
                            MessageBox.Show("Customer data Updated!");
                            dt_Customer = dalData.CustomerSelect();
                            LoadCustomer();
                            ClearField();
                            tableID = -1;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update customer data!");
                        }
                    }
                }
            }
        }

        private void ClearField()
        {
            txtName.Clear();
            txtRegistrationNo.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtCountry.Text = "MALAYSIA";
            txtPostalCode.Clear();
            txtPhone1.Clear();
            txtPhone2.Clear();
            txtFax.Clear();
            txtEmail.Clear();
            txtWebsite.Clear();
        }

        private void ShowDataToField(int selectedID)
        {
            ClearField();

            foreach (DataRow row in dt_Customer.Rows)
            {
                int id = int.TryParse(row[dalData.TableCode].ToString(), out id) ? id : -1;

                if(id == selectedID)
                {
                    string name = row[dalData.FullName] == null ? string.Empty : row[dalData.FullName].ToString();
                    string registrationNO = row[dalData.RegistrationNo] == null ? string.Empty : row[dalData.RegistrationNo].ToString();
                    string address1 = row[dalData.Address1] == null ? string.Empty : row[dalData.Address1].ToString();
                    string address2 =  row[dalData.Address2] == null ? string.Empty : row[dalData.Address2].ToString();
                    string city =  row[dalData.AddressCity] == null ? string.Empty : row[dalData.AddressCity].ToString();
                    string state =  row[dalData.AddressState] == null ? string.Empty : row[dalData.AddressState].ToString();
                    string country =  row[dalData.AddressCountry] == null ? string.Empty : row[dalData.AddressCountry].ToString();
                    string postalCode =  row[dalData.AddressPostalCode] == null ? string.Empty : row[dalData.AddressPostalCode].ToString();
                    string phone1 =  row[dalData.Phone1] == null ? string.Empty : row[dalData.Phone1].ToString();
                    string phone2 =  row[dalData.Phone2] == null ? string.Empty : row[dalData.Phone2].ToString();
                    string fax =  row[dalData.Fax] == null ? string.Empty : row[dalData.Fax].ToString();
                    string email =  row[dalData.Email] == null ? string.Empty : row[dalData.Email].ToString();
                    string website =  row[dalData.Website] == null ? string.Empty : row[dalData.Website].ToString();

                    txtName.Text = name;
                    txtRegistrationNo.Text = registrationNO;
                    txtAddress1.Text = address1;
                    txtAddress2.Text = address2;
                    txtCity.Text = city;
                    txtState.Text = state;
                    txtCountry.Text = country;
                    txtPostalCode.Text = postalCode;
                    txtPhone1.Text = phone1;
                    txtPhone2.Text = phone2;
                    txtFax.Text = fax;
                    txtEmail.Text = email;
                    txtWebsite.Text = website;

                    break;
                }
               
              
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvCustomer;

            if(rowIndex >= 0)
            {
                //get id
                int id = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_ID].Value.ToString());
                tableID = id;
                ShowDataToField(id);

                btnEdit.Text = "EDIT";
                btnCancel.Visible = true;
                btnRemove.Visible = true;
           
            }
            else
            {
                ClearField();

                btnEdit.Text = "ADD";
                btnCancel.Visible = false;
                btnRemove.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to cancel customer info editing?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                ClearField();
                ClearError();
                btnEdit.Text = "ADD";
                btnCancel.Visible = false;
                btnRemove.Visible = false;
                dgvCustomer.ClearSelection();
                tableID = -1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to remove this customer?", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes && tableID != -1)
            {
                uData.Table_Code = tableID;
                uData.IsRemoved = true;
                uData.Updated_Date = DateTime.Now;
                uData.Updated_By = MainDashboard.USER_ID;

                if (dalData.CustomerRemove(uData))
                {
                    MessageBox.Show("Customer Removed!");
                    dt_Customer = dalData.CustomerSelect();
                    LoadCustomer();
                    ClearField();
                    tableID = -1;
                }
                else
                {
                    MessageBox.Show("Failed to remove customer!");
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
            LoadCustomer();
        }

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgvCustomer;
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

                btnEdit.Text = "EDIT";
                btnCancel.Visible = true;
                btnRemove.Visible = true;

            }
            else
            {
                ClearField();

                btnEdit.Text = "ADD";
                btnCancel.Visible = false;
                btnRemove.Visible = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
