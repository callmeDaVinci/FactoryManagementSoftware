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
                    Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                    String sql = "SELECT * FROM tbl_forecast INNER JOIN tbl_item ON tbl_forecast.item_code = tbl_item.item_code ORDER BY " + sortSQL + " " + orderSQL;

                    //for executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    //cmd.Parameters.AddWithValue("@sortSQL", sortSQL);
                    //cmd.Parameters.AddWithValue("@orderSQL", orderSQL);

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
                    Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Search

        public DataTable Search(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();


            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_forecast INNER JOIN tbl_item ON tbl_forecast.item_code = tbl_item.item_code AND (tbl_item.item_code LIKE '%" + keywords + "%' OR tbl_item.item_name LIKE '%" + keywords + "%')";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable testSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();


            try
            {
                //sql query to get data from database
                String sql = @"SELECT 
	                                tbl_forecast.forecast_no,
	                                tbl_forecast.item_code,
	                                tbl_forecast.forecast_ready_stock,
	                                tbl_forecast.forecast_current_month,
	                                tbl_forecast.forecast_one,
	                                tbl_forecast.forecast_two,
	                                tbl_forecast.forecast_three,
	                                tbl_forecast.forecast_out_stock,
	                                tbl_forecast.forecast_osant,
	                                tbl_forecast.forecast_shot_one,
	                                tbl_forecast.forecast_shot_two,
	                                tbl_item.item_name,
	                                tbl_item.item_material,
	                                tbl_item.item_mb,
	                                tbl_item.item_mc,
	                                tbl_item.item_color,
	                                tbl_item.item_part_weight,
	                                tbl_item.item_runner_weight,
	                                tbl_join.join_child_code
                                INTO
	                                #temp
                                FROM 
	                                tbl_forecast 
	                                LEFT JOIN tbl_join 
	                                ON tbl_forecast.item_code = tbl_join.join_parent_code
	                                INNER JOIN tbl_item ON tbl_forecast.item_code = tbl_item.item_code ;
                                SELECT 
	                                #temp.forecast_no,
	                                #temp.item_code,
	                                #temp.forecast_ready_stock,
	                                #temp.forecast_current_month,
	                                #temp.forecast_one,
	                                #temp.forecast_two,
	                                #temp.forecast_three,
	                                #temp.forecast_out_stock,
	                                #temp.forecast_osant,
	                                #temp.forecast_shot_one,
	                                #temp.forecast_shot_two,
	                                #temp.item_name,
	                                #temp.item_material,
	                                #temp.item_mb,
	                                #temp.item_mc,
	                                #temp.item_color,
	                                #temp.item_part_weight,
	                                #temp.item_runner_weight,
	                                #temp.join_child_code,
	                                tbl_item.item_name as child_name
                                FROM 
	                                #temp 
	                                LEFT JOIN tbl_item
	                                ON #temp.join_child_code = tbl_item.item_code
                                WHERE #temp.item_name LIKE '%" + keywords + "%' OR tbl_item.item_name LIKE '%" + keywords + "%' " +
                                "DROP TABLE #temp";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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

        //        USE[Factory]
        //GO


        //SELECT
        //    tbl_forecast.item_code as item_code,
        //    tbl_item.item_code as item_itemcode,
        //    tbl_item.item_name,
        //    tbl_join.join_parent_code,
        //    tbl_join.join_child_code
        //FROM

        //    tbl_forecast
        //    LEFT JOIN tbl_join ON tbl_forecast.item_code = tbl_join.join_parent_code

        //    INNER JOIN tbl_item ON tbl_forecast.item_code = tbl_item.item_code

        //WHERE
        //    tbl_item.item_name LIKE '%sl%'
        //UNION

        //SELECT

        //    tbl_forecast.item_code as item_code,
        //    tbl_item.item_code,
        //    tbl_item.item_name,
        //    tbl_join.join_parent_code,
        //    tbl_join.join_child_code
        //FROM

        //    tbl_join
        //    INNER JOIN tbl_item ON tbl_join.join_child_code = tbl_item.item_code

        //    INNER JOIN tbl_forecast ON tbl_join.join_parent_code = tbl_forecast.item_code

        //WHERE
        //    tbl_item.item_name LIKE '%sl%'
        //GO




    }
}
