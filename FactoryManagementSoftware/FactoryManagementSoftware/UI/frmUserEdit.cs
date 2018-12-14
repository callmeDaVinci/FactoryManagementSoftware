using System;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System.Windows.Forms;
using System.Data;

namespace FactoryManagementSoftware.UI
{
    public partial class frmUserEdit : Form
    {

        userBLL uUser = new userBLL();
        userDAL dalUser = new userDAL();

        private int userID = -1;

        public frmUserEdit()
        {
            InitializeComponent();
        }

        public frmUserEdit(userBLL u)
        {
            InitializeComponent();
            userID = u.user_id;
            txtUsername.Text = u.user_name;
            txtPassword.Text = u.user_password;
            cmbPermissions.SelectedIndex = u.user_permissions;
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username Required");
                result = false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                result = false;
                errorProvider2.SetError(txtPassword, "Password Required");
            }

            if (cmbPermissions.SelectedIndex <= -1)
            {
                result = false;
                errorProvider3.SetError(cmbPermissions, "Permissions Required");
            }
            return result;
        }

        private bool IfUsernameExists(String username)
        {
            DataTable dt;
            dt = dalUser.userNameSearch(username);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private bool IfUserIDExists()
        {
            DataTable dt;
            dt = dalUser.userIDSearch(userID);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void getDataFromInput()
        {
            uUser.user_name = txtUsername.Text;
            uUser.user_password = txtPassword.Text;
            uUser.user_permissions = cmbPermissions.SelectedIndex;
        }

        private void createNewUser()
        {
            getDataFromInput();
            uUser.added_date = DateTime.Now;
            uUser.added_by = MainDashboard.USER_ID;

            bool success = dalUser.insert(uUser);
            if (success == true)
            {
                //Data Successfully Inserted
                MessageBox.Show("User successfully created");
                Close();
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new user");
            }
        }

        private void updateUser()
        {
            getDataFromInput();
            uUser.user_id = userID;
            uUser.updated_date = DateTime.Now;
            uUser.updated_by = MainDashboard.USER_ID;

            bool success = dalUser.update(uUser);
            if (success)
            {
                //Data Successfully Inserted
                MessageBox.Show("User successfully updated");
                Close();
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to update user");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (userID <= 0)
                    {
                        if (IfUsernameExists(txtUsername.Text))
                        {
                            MessageBox.Show("Username already exist. Please use another one.");
                        }
                        else
                        {
                            createNewUser();
                        }
                    }
                    else
                    {
                        if(IfUserIDExists())
                        {
                            updateUser();
                        }
                    }
                    
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void cmbPermissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }
    }
}
