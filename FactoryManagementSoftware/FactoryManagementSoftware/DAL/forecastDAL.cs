using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class forecastDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Data in Database
        public bool Insert(forecastBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_forecast (cust_id, item_code, forecast_one, forecast_two, forecast_three, forecast_current_month, forecast_updtd_date, forecast_updtd_by) VALUES (@cust_id, @item_code, @forecast_one, @forecast_two, @forecast_three, @forecast_current_month, @forecast_updtd_date, @forecast_updtd_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@forecast_one", u.forecast_one);
                cmd.Parameters.AddWithValue("@forecast_two", u.forecast_two);
                cmd.Parameters.AddWithValue("@forecast_three", u.forecast_three);
                cmd.Parameters.AddWithValue("@forecast_current_month", u.forecast_current_month);
                cmd.Parameters.AddWithValue("@forecast_updtd_date", u.forecast_updtd_date);
                cmd.Parameters.AddWithValue("@forecast_updtd_by", u.forecast_updtd_by);

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

        #region Update data in Database
        public bool Update(forecastBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_forecast SET forecast_one=@forecast_one, forecast_two=@forecast_two, forecast_three=@forecast_three, forecast_current_month=@forecast_current_month, forecast_updtd_date=@forecast_updtd_date, forecast_updtd_by=@forecast_updtd_by WHERE cust_id=@cust_id AND item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@forecast_one", u.forecast_one);
                cmd.Parameters.AddWithValue("@forecast_two", u.forecast_two);
                cmd.Parameters.AddWithValue("@forecast_three", u.forecast_three);
                cmd.Parameters.AddWithValue("@forecast_current_month", u.forecast_current_month);
                cmd.Parameters.AddWithValue("@forecast_updtd_date", u.forecast_updtd_date);
                cmd.Parameters.AddWithValue("@forecast_updtd_by", u.forecast_updtd_by);
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

        public DataTable existsSearch(string itemCode, string custID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();


            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_forecast WHERE item_code LIKE '%" + itemCode + "%' AND cust_id LIKE '%" + custID + "%'";

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

        public DataTable nameSearch(string itemName, string custName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();


            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM ((tbl_forecast INNER JOIN tbl_item ON tbl_item.item_name LIKE '%" + itemName + "%' AND tbl_item.item_code = tbl_forecast.item_code) INNER JOIN tbl_cust ON tbl_cust.cust_name LIKE '%" + custName + "%' AND tbl_cust.cust_id = tbl_forecast.cust_id)";

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
    }
}
