using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }

        public frmLogIn(string userName)
        {
            InitializeComponent();
            gtxtUsername.Text = userName;
        }

        userDAL dalUser = new userDAL();

        Text text = new Text();
        Tool tool = new Tool(); 

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validation()
        {
            bool result = true;

            if(string.IsNullOrEmpty(gtxtUsername.Text))
            {
                errorProvider1.SetError(gtxtUsername, "Username required");
                result = false;
            }

            if (string.IsNullOrEmpty(gtxtPassword.Text))
            {
                errorProvider2.SetError(gtxtPassword, "Password required");
                result = false;
            }

            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = gtxtUsername.Text;
            string password = gtxtPassword.Text;

            //string username = textBox1.Text;
            //string password = textBox2.Text;

            if (validation())//validation()
            {
               
                int userID = dalUser.userLogin(username, password);
                if (userID != -1)
                {
                    int userPermission = dalUser.getPermissionLevel(userID);

                    if (userPermission < MainDashboard.ACTION_LVL_ONE)
                    {   
                        MessageBox.Show("Access denied. Contact your administrator.");
                        tool.historyRecord(text.LogIn, text.Failed, DateTime.Now, userID);
                    }   
                    else
                    {
                        frmLoading.ShowLoadingScreen();
                        tool.historyRecord(text.LogIn, text.Success, DateTime.Now, userID);
                        MainDashboard frm = new MainDashboard(userID);
                        frm.Show();
                        frmLoading.CloseForm();
                        Hide();
                    }  
                }
                else
                {
                    MessageBox.Show("username or password invalid. please try again.");
                }
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(gtxtUsername.Text))
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gtxtPassword.Text))
            {
                errorProvider2.Clear();
            }
        }

        private void frmLogIn_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("haha");
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("enter");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            //checking whether database exist or not
            if (!CheckDatabaseExist())
            {
                
                MessageBox.Show("Database not exist");
                //GenerateDatabase();
            }
            
        }

        private bool CheckDatabaseExist()
        {
            bool result;
            string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
            //Sql connection for user defined database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                conn.Open();
                result = true;             
            }
            catch (Exception ex)
            {
                result = false;
                tool.saveToText(ex);
            }

            conn.Close();
            return result;
        }

        private void GenerateDatabase()
        {
            List<string> cmds = new List<string>();
            if (File.Exists(Application.StartupPath + "\\script.sql"))
            {
                TextReader tr = new StreamReader(Application.StartupPath + "\\script.sql");
                string line = "";
                string cmd = "";
                while((line = tr.ReadLine()) != null)
                {
                    if(line.Trim().ToUpper() == "GO")
                    {
                        cmds.Add(cmd);
                        cmd = "";
                    }
                    else
                    {
                        cmd += line + "\r\n";
                    }
                }

                if(cmd.Length > 0)
                {
                    cmds.Add(cmd);
                    cmd = "";
                }

                tr.Close();
            }

            if(cmds.Count > 0)
            {
                SqlCommand command = new SqlCommand();

                command.Connection = new SqlConnection(@"Data Source=
           .\SQLEXPRESS;Initial Catalog=Factory;Integrated Security=True");

                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                
                for(int i = 0; i < cmds.Count; i++)
                {
                    command.CommandText = cmds[i];
                    command.ExecuteNonQuery();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmLogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
    
}
