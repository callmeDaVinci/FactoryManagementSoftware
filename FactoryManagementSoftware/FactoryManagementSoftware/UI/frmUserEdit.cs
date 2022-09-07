using System;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System.Windows.Forms;
using System.Data;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmUserEdit : Form
    {

        userBLL uUser = new userBLL();
        userDAL dalUser = new userDAL();
        Text text = new Text();

        private readonly string BTN_SIGN_UP = "SIGN UP";
        private readonly string BTN_UPDATED = "UPDATE";
        private readonly string FORM_NEW_USER = "NEW USER";
        private readonly string FORM_USER_UPDATE = "USER UPDATE";

        private int userID = -1;

        
        public frmUserEdit()
        {
            InitializeComponent();

            NewUserModeUIChange();
        }

        public frmUserEdit(bool accountSignUpbyUser)
        {
            InitializeComponent();

            NewUserModeUIChange();
        }

        public frmUserEdit(userBLL u)
        {
            InitializeComponent();
            userID = u.user_id;
            txtUsername.Text = u.user_name;
            //txtNewPassword.Text = u.user_password;
            cmbPermissions.SelectedIndex = u.user_permissions;

            uUser = u;

            int EditorID = MainDashboard.USER_ID;

            int PermissionLevel = u.user_permissions;

            if (EditorID != -1)
            {
                int EditorPermissions = dalUser.getPermissionLevel(EditorID);

                PermissionLevel = EditorPermissions;
            }


            if (PermissionLevel < 5)
            {
                cmbPermissions.Enabled = false;
            }
            else
            {
                cmbPermissions.Enabled = true;
            }

            UserEditModeUIChange();

        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username Required!");
                result = false;
            }

            if (userID > 0)
            {
                if (string.IsNullOrEmpty(txtOldPassword.Text))
                {
                    result = false;
                    errorProvider5.SetError(lblOldPassword, "Password Required!");
                }
                else if(txtOldPassword.Text != uUser.user_password)
                {
                    result = false;
                    errorProvider5.SetError(lblOldPassword, "Old password do not match!");
                }

                if (txtPasswordConfirm.Text != txtNewPassword.Text)
                {
                    result = false;
                    errorProvider2.SetError(lblNewPassword, "Password do not match!");
                    errorProvider4.SetError(lblPasswordConfirm, "Password do not match!");
                }

                if (cmbPermissions.SelectedIndex <= -1)
                {
                    result = false;
                    errorProvider3.SetError(lblPermissions, "Permissions Required");
                }
            }
            else
            {

                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    result = false;
                    errorProvider2.SetError(lblNewPassword, "Password Required");
                }

                if (string.IsNullOrEmpty(txtPasswordConfirm.Text))
                {
                    result = false;
                    errorProvider4.SetError(lblPasswordConfirm, "Please confirm your password again.");
                }

                if (txtPasswordConfirm.Text != txtNewPassword.Text)
                {
                    result = false;
                    errorProvider2.SetError(lblNewPassword, "Password do not match");
                    errorProvider4.SetError(lblPasswordConfirm, "Password do not match");
                }

                if (cmbPermissions.SelectedIndex <= -1)
                {
                    result = false;
                    errorProvider3.SetError(lblPermissions, "Permissions Required");
                }
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

            if (userID <= 0)
            {
                uUser.user_password = txtNewPassword.Text;

            }
            else if(!string.IsNullOrEmpty(txtNewPassword.Text) && !string.IsNullOrEmpty(txtPasswordConfirm.Text))
            {
                uUser.user_password = txtNewPassword.Text;

            }
            else
            {
                uUser.user_password = txtOldPassword.Text;
            }

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
                DialogResult dialogResult = MessageBox.Show("Confrim to save user's data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (userID <= 0)
                    {
                        if (IfUsernameExists(txtUsername.Text))
                        {
                            string errorText = "An account with this username already exists.";
                            MessageBox.Show(errorText);

                            errorProvider1.SetError(lblUserName, errorText);

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
                        else
                        {
                            MessageBox.Show("USER ID INVALID!");
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(cbPasswordConfirm.Checked)
            {
                txtPasswordConfirm.PasswordChar = '\0';
            }
            else
            {
                txtPasswordConfirm.PasswordChar = '*';
            }
        }

        private void frmUserEdit_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNewPass.Checked)
            {
                txtNewPassword.PasswordChar = '\0';
            }
            else
            {
                txtNewPassword.PasswordChar = '*';
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NewUserModeUIChange()
        {
            tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            if (MainDashboard.USER_ID > -1 && dalUser.getPermissionLevel(MainDashboard.USER_ID) >= 5)
            {
                cmbPermissions.SelectedIndex = -1;
                cmbPermissions.Enabled = true;
            }
            else
            {
                cmbPermissions.SelectedIndex = 1;
                cmbPermissions.Enabled = false;
            }
            

            btnSave.Text = BTN_SIGN_UP;
            Text = FORM_NEW_USER;

            //txtUsername.TabIndex = 101;

            //txtNewPassword.TabIndex = 102;

            //txtPasswordConfirm.TabIndex = 103;

            //cmbPermissions.TabIndex = 104;

            //btnSave.TabIndex = 105;

            //txtOldPassword.TabIndex = 1;
            txtOldPassword.TabStop = false;
        }

        private void UserEditModeUIChange()
        {
            tlpMain.RowStyles[1] = new RowStyle(SizeType.Absolute, 80f);

            btnSave.Text = BTN_UPDATED;
            Text = FORM_USER_UPDATE;

            //txtUsername.TabIndex = 101;

            txtOldPassword.TabStop = true;
            //txtOldPassword.TabIndex = 102;

            //txtNewPassword.TabIndex = 103;

            //txtPasswordConfirm.TabIndex = 104;

            //cmbPermissions.TabIndex = 105;

            //btnSave.TabIndex = 106;
        }

        private void cbOldPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOldPassword.Checked)
            {
                txtOldPassword.PasswordChar = '\0';
            }
            else
            {
                txtOldPassword.PasswordChar = '*';
            }
        }

        private void lblPermissions_Click(object sender, EventArgs e)
        {

            frmVerification frm = new frmVerification(text.PW_TopManagement);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frmVerification.PASSWORD_MATCHED)
            {
                cmbPermissions.Enabled = true;
            }
        }
    }
}
