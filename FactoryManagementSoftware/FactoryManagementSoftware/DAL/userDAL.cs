using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class userDAL
    {
        #region data string name getter
        public string UserID { get; } = "user_id";
        public string Username { get; } = "user_name";
        public string Password { get; } = "user_password";
        public string Permission { get; } = "user_permission";

        public string AddedDate { get; } = "added_date";
        public string AddedBy { get; } = "added_by";
        public string UpdatedDate { get; } = "updated_date";
        public string UpdatedBy { get; } = "updated_by";
        #endregion

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select

        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_user";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

        #region Search Data from Database

        public DataTable userSearch(string username, string password)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_user WHERE user_name = @username AND user_password = @password";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable userIDSearch(int id)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_user WHERE user_id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }



        #endregion

        //login
        public int userLogin(string username, string password)
        {
            DataTable dt = userSearch(username, password);

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow user in dt.Rows)
                {
                    if(user[Username].ToString() == username && user[Password].ToString() == password)
                    {
                        return Convert.ToInt32(user[UserID]);
                    }
                }
            }
            return -1;
        }
        //signup
        //get username
        public string getUsername(int id)
        {
            DataTable dt = userIDSearch(id);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow user in dt.Rows)
                {
                    if (Convert.ToInt32(user[UserID]) == id)
                    {
                        return user[Username].ToString();
                    }
                }
            }
            return null;
        }

        public int getPermissionLevel(int id)
        {
            DataTable dt = userIDSearch(id);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow user in dt.Rows)
                {
                    if (Convert.ToInt32(user[UserID]) == id)
                    {
                        return Convert.ToInt32(user[Permission]);
                    }
                }
            }
            return -1;
        }
        //permission check

    }
}
