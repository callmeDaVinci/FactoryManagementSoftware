using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class orderActionDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database

        public DataTable Select()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_order_action";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                //throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable Select(int orderID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_order_action WHERE ord_id = @orderID";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@orderID", orderID);

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                //throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }
        
        #endregion

        #region Insert Data in Database

        public bool Insert(orderActionBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_order_action (ord_id, added_date, added_by, action,note) VALUES (@ord_id, @added_date, @added_by, @action, @note)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@action", u.action);
                cmd.Parameters.AddWithValue("@note", u.note);

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        
        #endregion
    }
}
