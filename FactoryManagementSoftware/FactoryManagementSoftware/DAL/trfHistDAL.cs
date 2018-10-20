using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class trfHistDAL
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
                String sql = "SELECT * FROM tbl_trf_hist";
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
        #endregion

        #region Insert Data in Database
        public bool Insert(trfHistBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_trf_hist (trf_hist_item_code, trf_hist_item_name, trf_hist_from, trf_hist_to, trf_hist_qty, trf_hist_unit, trf_hist_trf_date, trf_hist_note, trf_hist_added_date, trf_hist_added_by) VALUES (@trf_hist_item_code, @trf_hist_item_name, @trf_hist_from, @trf_hist_to, @trf_hist_qty, @trf_hist_unit, @trf_hist_trf_date, @trf_hist_note, @trf_hist_added_date, @trf_hist_added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trf_hist_item_code", u.trf_hist_item_code);
                cmd.Parameters.AddWithValue("@trf_hist_item_name", u.trf_hist_item_name);
                cmd.Parameters.AddWithValue("@trf_hist_from", u.trf_hist_from);
                cmd.Parameters.AddWithValue("@trf_hist_to", u.trf_hist_to);
                cmd.Parameters.AddWithValue("@trf_hist_qty", u.trf_hist_qty);
                cmd.Parameters.AddWithValue("@trf_hist_unit", u.trf_hist_unit);
                cmd.Parameters.AddWithValue("@trf_hist_trf_date", u.trf_hist_trf_date);
                cmd.Parameters.AddWithValue("@trf_hist_note", u.trf_hist_note);
                cmd.Parameters.AddWithValue("@trf_hist_added_date", u.trf_hist_added_date);
                cmd.Parameters.AddWithValue("@trf_hist_added_by", u.trf_hist_added_by);

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
        
        #region Search User on Database usingKeywords

        public DataTable Search(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist WHERE trf_hist_item_code LIKE '%" + keywords + "%'OR trf_hist_item_name LIKE '%" + keywords + "%'";

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

        #endregion
    }
}
