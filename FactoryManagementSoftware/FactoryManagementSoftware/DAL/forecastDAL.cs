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

        #region Select Data from Database
        public DataTable Select(string sort, string order)
        {
            string orderSQL = "ASC";
            string sortSQL = "";

            if (order.Equals("Descending"))
            {
                orderSQL = "DESC";
            }

            switch (sort)
            {
                case "Ready Stock":
                    sortSQL = "tbl_forecast.forecast_ready_stock";
                    break;

                case "Forecast One":
                    sortSQL = "tbl_forecast.forecast_one";
                    break;

                case "Forecast Two":
                    sortSQL = "tbl_forecast.forecast_two";
                    break;

                case "Forecast Three":
                    sortSQL = "tbl_forecast.forecast_three";
                    break;

                case "Shot One":
                    sortSQL = "tbl_forecast.forecast_shot_one";
                    break;

                case "Shot Two":
                    sortSQL = "tbl_forecast.forecast_shot_two";
                    break;

                default:
                    break;
                    
            }
               
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            
            if(string.IsNullOrEmpty(sortSQL))
            {
                try
                {
                    //sql query to get data from database
                    String sql = "SELECT * FROM tbl_forecast INNER JOIN tbl_item ON tbl_forecast.item_code = tbl_item.item_code";
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
            }

            else
            {
                try
                {
                    //sql query to get data from database
                    String sql = "SELECT * FROM tbl_forecast INNER JOIN tbl_item ON tbl_forecast.item_code = tbl_item.item_code ORDER BY "+sortSQL+" "+orderSQL;
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
            }

            return dt;
        }
        #endregion

        #region Insert Data in Database
        public bool Insert(forecastBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_forecast (forecast_no, item_code, forecast_ready_stock, forecast_current_month, forecast_one, forecast_two, forecast_three, forecast_out_stock, forecast_osant, forecast_shot_one, forecast_shot_two, forecast_updtd_date, forecast_updtd_by) VALUES (@forecast_no, @item_code, @forecast_ready_stock, @forecast_current_month, @forecast_one, @forecast_two, @forecast_three, @forecast_out_stock, @forecast_osant, @forecast_shot_one, @forecast_shot_two, @forecast_updtd_date, @forecast_updtd_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@forecast_no", u.forecast_no);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@forecast_ready_stock", u.forecast_ready_stock);
                cmd.Parameters.AddWithValue("@forecast_current_month", u.forecast_current_month);
                cmd.Parameters.AddWithValue("@forecast_one", u.forecast_one);
                cmd.Parameters.AddWithValue("@forecast_two", u.forecast_two);
                cmd.Parameters.AddWithValue("@forecast_three", u.forecast_three);
                cmd.Parameters.AddWithValue("@forecast_out_stock", u.forecast_out_stock);
                cmd.Parameters.AddWithValue("@forecast_osant", u.forecast_osant);
                cmd.Parameters.AddWithValue("@forecast_shot_one", u.forecast_shot_one);
                cmd.Parameters.AddWithValue("@forecast_shot_two", u.forecast_shot_two);
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

        #region Delete data from Database
        public bool Delete()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_forecast";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows >= 0)
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

        #region Search

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

        #endregion

        #region Sort

        #endregion

    }
}
