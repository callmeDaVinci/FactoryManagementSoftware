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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

        #region insert

        public bool insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_user 
                            (" + Username + ","
                            + Password + ","
                            + Permission + ","
                            + AddedDate + ","
                            + AddedBy + ") VALUES " +
                            "(@user_name," +
                            "@user_password," +
                            "@user_permissions," +
                            "@added_date," +
                            "@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@user_name", u.user_name);
                cmd.Parameters.AddWithValue("@user_password", u.user_password);
                cmd.Parameters.AddWithValue("@user_permissions", u.user_permissions);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                }
                else
                {
                    //Query falled
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region update

        public bool update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_user SET "
                            + Username + "=@user_name,"
                            + Password + "=@user_password,"
                            + Permission + "=@user_permissions,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE user_id=@user_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@user_id", u.user_id);
                cmd.Parameters.AddWithValue("@user_name", u.user_name);
                cmd.Parameters.AddWithValue("@user_password", u.user_password);
                cmd.Parameters.AddWithValue("@user_permissions", u.user_permissions);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                }
                else
                {
                    //Query falled
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region delete

        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_user WHERE user_id=@user_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                }
                else
                {
                    //Query falled
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable userNameSearch(string username)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_user WHERE user_name = @username";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", username);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

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

        public string getPassword(int id)
        {
            DataTable dt = userIDSearch(id);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow user in dt.Rows)
                {
                    if (Convert.ToInt32(user[UserID]) == id)
                    {
                        return user[Password].ToString();
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


    }
}
