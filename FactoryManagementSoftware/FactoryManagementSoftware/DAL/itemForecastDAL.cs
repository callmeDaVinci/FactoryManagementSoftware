using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using FactoryManagementSoftware.UI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class itemForecastDAL
    {
        #region data string name getter
        public string CustID { get; } = "cust_id";
        public string ItemCode { get; } = "item_code";

        public string ForecastMonth { get; } = "forecast_month";
        public string ForecastYear { get; } = "forecast_year";
        public string ForecastQty { get; } = "forecast_qty";

        public string UpdatedBy { get; } = "updated_by";
        public string UpdatedDate { get; } = "updated_date";

        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        //Tool tool = new Tool();
        //Text text = new Text();
        #endregion

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
                String sql = "SELECT * FROM tbl_item_forecast ORDER BY cust_id ASC, item_code ASC, forecast_year ASC, forecast_month ASC";

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

        public DataTable Select(string custID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_item_forecast WHERE cust_id = @custID ORDER BY item_code ASC, forecast_year ASC, forecast_month ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@custID", custID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


        public DataTable SelectWithCustAndRange(int custID, int startMonth, int startYear, int endMonth, int endYear)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * 
                             FROM tbl_item_forecast 
                             INNER JOIN tbl_item 
                             ON tbl_item_forecast.item_code = tbl_item.item_code
                             WHERE cust_id = @custID 
                             AND tbl_item_forecast.forecast_year >= @startYear
                             AND tbl_item_forecast.forecast_year <= @endYear
                             AND (tbl_item_forecast.forecast_month >= @startMonth OR tbl_item_forecast.forecast_month <= @endMonth)
                             ORDER BY tbl_item_forecast.item_code ASC, tbl_item_forecast.forecast_year ASC, tbl_item_forecast.forecast_month ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //AND tbl_item_forecast.forecast_year <= @endMonth
                //             AND tbl_item_forecast.forecast_month <= @startMonth
                //             AND tbl_item_forecast.forecast_month >= @endMonth

                cmd.Parameters.AddWithValue("@custID", custID);
                cmd.Parameters.AddWithValue("@startMonth", startMonth);
                cmd.Parameters.AddWithValue("@startYear", startYear);
                cmd.Parameters.AddWithValue("@endMonth", endMonth);
                cmd.Parameters.AddWithValue("@endYear", endYear);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable SelectWithRange(int startMonth, int startYear, int endMonth, int endYear)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * 
                             FROM tbl_item_forecast 
                             INNER JOIN tbl_item 
                             ON tbl_item_forecast.item_code = tbl_item.item_code
                             WHERE tbl_item_forecast.forecast_year >= @startYear
                             AND tbl_item_forecast.forecast_year <= @endYear
                             AND (tbl_item_forecast.forecast_month >= @startMonth OR tbl_item_forecast.forecast_month <= @endMonth)
                             ORDER BY tbl_item_forecast.item_code ASC, tbl_item_forecast.forecast_year ASC, tbl_item_forecast.forecast_month ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //AND tbl_item_forecast.forecast_year <= @endMonth
                //             AND tbl_item_forecast.forecast_month <= @startMonth
                //             AND tbl_item_forecast.forecast_month >= @endMonth

                cmd.Parameters.AddWithValue("@startMonth", startMonth);
                cmd.Parameters.AddWithValue("@startYear", startYear);
                cmd.Parameters.AddWithValue("@endMonth", endMonth);
                cmd.Parameters.AddWithValue("@endYear", endYear);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ItemSelect(string itemCode)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_item_forecast WHERE item_code = @itemCode ORDER BY cust_id ASC, forecast_year ASC, forecast_month ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ItemSelect(string itemCode, string custID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_item_forecast WHERE item_code =@itemCode AND cust_id = @custID ORDER BY forecast_year ASC, forecast_month ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@custID", custID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ItemSelect(string itemCode, string custID, string year)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_item_forecast WHERE item_code =@itemCode AND cust_id = @custID AND forecast_year =@year ORDER BY forecast_month ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@custID", custID);
                cmd.Parameters.AddWithValue("@year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ItemSelect(string itemCode, string custID, string year, string month)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_item_forecast WHERE item_code =@itemCode AND cust_id = @custID AND forecast_year =@year AND forecast_month =@month";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@custID", custID);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@month", month);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Tool tool = new Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

        #region Insert Data in Database
   
        public bool Insert(itemForecastBLL u)
        {
            bool isSuccess = false;
            Tool tool = new Tool();
            Text text = new Text();
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_item_forecast 
                            (" + CustID + ","
                            + ItemCode + ","
                            + ForecastYear + ","
                            + ForecastMonth + ","
                            + ForecastQty + ","
                            + UpdatedDate + ","
                            + UpdatedBy + ") VALUES" +
                            "(@cust_id," +
                            "@item_code," +
                            "@forecast_year," +
                            "@forecast_month," +
                            "@forecast_qty," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@forecast_year", u.forecast_year);
                cmd.Parameters.AddWithValue("@forecast_month", u.forecast_month);
                cmd.Parameters.AddWithValue("@forecast_qty", u.forecast_qty);
                
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                    tool.historyRecord(text.ForecastInsert, text.getForecastInsertString(u), u.updated_date, u.updated_by);
                }
                else
                {
                    //Query failed
                    isSuccess = false;
                    tool.historyRecord(text.System, "Failed to insert forecast(frmItemForecastDAL) " + u.item_code, u.updated_date, u.updated_by);
                }

            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region Update data in Database

        public bool Update(itemForecastBLL u)
        {
            bool isSuccess = false;
            Tool tool = new Tool();
            Text text = new Text();

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_item_forecast 
                            SET "                    
                            + ForecastQty + "=@forecast_qty,"
                            + UpdatedDate + "=@updated_date,"
                            + UpdatedBy + "=@updated_by" +
                            " WHERE item_code=@item_code AND cust_id =@cust_id AND forecast_year = @forecast_year AND forecast_month = @forecast_month";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@forecast_year", u.forecast_year);
                cmd.Parameters.AddWithValue("@forecast_month", u.forecast_month);
                cmd.Parameters.AddWithValue("@forecast_qty", u.forecast_qty);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
           
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                    tool.historyRecord(text.ForecastEdit, text.getForecastEditString(u), u.updated_date, u.updated_by);
                }
                else
                {
                    //Query failed
                    isSuccess = false;
                    tool.historyRecord(text.System, "Failed to updated forecast(frmItemForecastDAL) " + u.item_code, u.updated_date, u.updated_by);
                }
            }
            catch (Exception ex)
            {
               
                tool.saveToText(ex);
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
