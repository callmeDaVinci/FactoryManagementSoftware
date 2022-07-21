using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.IO;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;

namespace FactoryManagementSoftware.UI
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();

           

        }


        private void ShowSystemVersion()
        {
            string GetVersion = ApplicationDeployment.IsNetworkDeployed ? $"Version: {ApplicationDeployment.CurrentDeployment.CurrentVersion}" : $"Version: {Application.ProductVersion}";

            lblVersion.Text = GetVersion;
        }

        public frmLogIn(string userName)
        {
            InitializeComponent();
            gtxtUsername.Text = userName;
        }

        userDAL dalUser = new userDAL();

        Text text = new Text();
        Tool tool = new Tool();

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        joinBLL uJoin = new joinBLL();

        SBBDataDAL dalSBB = new SBBDataDAL();
        SBBDataBLL uSBB = new SBBDataBLL();

        planningDAL dalPlan = new planningDAL();

        joinDAL dalJoin = new joinDAL();

        static public DataTable dt_DOWithTrfInfoSelectedPeriod;
        static public DataTable dt_Item;
        static public DataTable dt_SBBCustSearchWithTypeAndSize;
        static public DataTable dt_SBBItemSelect;
        static public DataTable dt_JoinSelectWithChildCat;
        static public DataTable dt_TrfRangeUsageSearch;
        static public DataTable dt_POSelectWithSizeAndType;
        static public DataTable dt_PendingPOSelect;
        static public DataTable dt_SBBCustWithoutRemovedDataSelect;
        static public DataTable dt_SBBItemUpload;
        static public DataTable dt_DOWithTrfInfoSelect;

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

            if (validation())
            {
                //if (username.ToUpper() == "EMMELINE" && password == "ep668")
                //{
                //    frmLoading.ShowLoadingScreen();
                //    //tool.historyRecord(text.LogIn, text.Success, DateTime.Now, userID);

                //    FinexDataUpload frm = new FinexDataUpload();
                //    frm.Show();
                //    frmLoading.CloseForm();
                //    Hide();
                //}

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
                        //frmLoading.ShowLoadingScreen();
                        tool.historyRecord(text.LogIn, text.Success, DateTime.Now, userID);
                        MainDashboard frm = new MainDashboard(userID);
                        frm.Show();
                        //frmLoading.CloseForm();
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

        private void SBBPageDataPreLoad()
        {
            DateTime MonthlyDateStart = tool.GetSBBMonthlyStartDate();
            DateTime MonthlyDateEnd = tool.GetSBBMonthlyEndDate();

            string start;
            string end;
            string itemCust = text.SPP_BrandName;

            DateTime now = DateTime.Now;

            start = MonthlyDateStart.ToString("yyyy/MM/dd");
            end = MonthlyDateEnd.ToString("yyyy/MM/dd");

            dt_SBBCustWithoutRemovedDataSelect = dalSBB.CustomerWithoutRemovedDataSelect();//84

            dt_PendingPOSelect = dalSBB.SBBPagePendingPOSelect();//456


            dt_JoinSelectWithChildCat = dalJoin.SelectWithChildCat();//98
            dt_SBBItemSelect = dalItemCust.SBBItemSelect(itemCust);//49
            dt_POSelectWithSizeAndType = dalSBB.SBBPagePOSelectWithSizeAndType();//60

            dt_DOWithTrfInfoSelectedPeriod = dalSBB.SBBPageOWithTrfInfoSelect(start, end);//765

            dt_SBBCustSearchWithTypeAndSize = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);//26
            dt_SBBCustSearchWithTypeAndSize.DefaultView.Sort = dalSBB.TypeName + " ASC," + dalSBB.SizeNumerator + " ASC," + dalSBB.SizeWeight + "1 ASC";
            dt_SBBCustSearchWithTypeAndSize = dt_SBBCustSearchWithTypeAndSize.DefaultView.ToTable();

            dt_Item = dalItem.Select();

            dt_TrfRangeUsageSearch = dalTrfHist.SBBPageRangeUsageSearch(start, end);

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
            else if(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString.Equals(text.DB_Semenyih) || ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString.Equals(text.DB_JunPC))
            {
                //SBBPageDataPreLoad();
            }

            ShowSystemVersion();

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
