using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class itemCustDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public string CustID { get; } = "cust_id";
        public string CustName { get; } = "cust_name";


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
                String sql = @"SELECT * FROM ((tbl_item_cust 
                            INNER JOIN tbl_item 
                            ON tbl_item_cust.item_code = tbl_item.item_code)
                            INNER JOIN tbl_cust 
                            ON tbl_item_cust.cust_id = tbl_cust.cust_id) ORDER BY tbl_cust.cust_name ASC, tbl_item.item_name ASC";

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

        public DataTable SelectAll()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_item_cust";

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

        #region Insert Data in Database
        public bool Insert(itemCustBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_item_cust (item_code, cust_id, forecast_one, forecast_two, forecast_three, forecast_four, forecast_current_month, item_cust_added_date, item_cust_added_by) VALUES (@item_code, @cust_id, @forecast_one, @forecast_two, @forecast_three, @forecast_four, @forecast_current_month, @item_cust_added_date, @item_cust_added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);
                cmd.Parameters.AddWithValue("@forecast_one", u.forecast_one);
                cmd.Parameters.AddWithValue("@forecast_two", u.forecast_two);
                cmd.Parameters.AddWithValue("@forecast_three", u.forecast_three);
                cmd.Parameters.AddWithValue("@forecast_four", u.forecast_four);
                cmd.Parameters.AddWithValue("@forecast_current_month", u.forecast_current_month);
                cmd.Parameters.AddWithValue("@item_cust_added_date", u.item_cust_added_date);
                cmd.Parameters.AddWithValue("@item_cust_added_by", u.item_cust_added_by);

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

        #region Update
        public bool Update(itemCustBLL u)
        {
            bool isSuccess = false;
            Module.Tool tool = new Module.Tool();
            Module.Text text = new Module.Text();

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item_cust SET forecast_one=@forecast_one, forecast_two=@forecast_two, forecast_three=@forecast_three,forecast_four=@forecast_four, forecast_current_month=@forecast_current_month, forecast_updated_date=@forecast_updated_date, forecast_updated_by=@forecast_updated_by WHERE item_code=@item_code AND cust_id = @cust_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@forecast_one", u.forecast_one);
                cmd.Parameters.AddWithValue("@forecast_two", u.forecast_two);
                cmd.Parameters.AddWithValue("@forecast_three", u.forecast_three);
                cmd.Parameters.AddWithValue("@forecast_four", u.forecast_four);
                cmd.Parameters.AddWithValue("@forecast_current_month", u.forecast_current_month);
                cmd.Parameters.AddWithValue("@forecast_updated_date", u.forecast_updated_date);
                cmd.Parameters.AddWithValue("@forecast_updated_by", u.forecast_updated_by);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);

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
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();

            }
            return isSuccess;
        }

        public bool monthUpdate(itemCustBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item_cust SET forecast_current_month=@forecast_current_month, forecast_updated_date=@forecast_updated_date, forecast_updated_by=@forecast_updated_by WHERE item_code=@item_code AND cust_id = @cust_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@forecast_current_month", u.forecast_current_month);
                cmd.Parameters.AddWithValue("@forecast_updated_date", u.forecast_updated_date);
                cmd.Parameters.AddWithValue("@forecast_updated_by", u.forecast_updated_by);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);

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
        public bool Delete(itemCustBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_item_cust WHERE cust_id=@cust_id AND item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_id", u.cust_id);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);

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

        #region Search by customer on Database usingKeywords

        public DataTable SPPCustSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM 
                                ((tbl_item_cust 
                                INNER JOIN tbl_cust 
                                ON tbl_cust.cust_name=@keywords 
                                AND tbl_item_cust.cust_id = tbl_cust.cust_id) 
                                INNER JOIN tbl_item 
                                ON tbl_item_cust.item_code = tbl_item.item_code) 
                                FULL JOIN tbl_spp_stdpacking
                                ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                                ORDER BY tbl_item.item_name ASC";
                //
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
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

        public DataTable SPPCustSearchWithTypeAndSize(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM 
                                ((tbl_item_cust 
                                 INNER JOIN tbl_cust
                                ON tbl_cust.cust_name=@keywords 
                                AND tbl_item_cust.cust_id = tbl_cust.cust_id) 
                                 INNER JOIN tbl_item 
                                ON tbl_item_cust.item_code = tbl_item.item_code) 
                                 INNER JOIN tbl_spp_type
                                ON tbl_item.type_tbl_code = tbl_spp_type.tbl_code
                                 LEFT JOIN tbl_spp_size size1
                                ON tbl_item.size_tbl_code_1 = size1.tbl_code
                                LEFT JOIN tbl_spp_size size2
                                ON tbl_item.size_tbl_code_2 = size2.tbl_code
                                 INNER JOIN tbl_spp_stdpacking
                                ON tbl_item.item_code = tbl_spp_stdpacking.item_code
                                ORDER BY tbl_item.item_name ASC";

                

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
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

        public DataTable SBBItemSelect(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM 
                                tbl_item_cust 
                                 INNER JOIN tbl_cust
                                ON tbl_cust.cust_name=@keywords 
                                AND tbl_item_cust.cust_id = tbl_cust.cust_id
                                ORDER BY tbl_item_cust.item_code ASC";



                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
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

        public DataTable custSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM 
                                ((tbl_item_cust 
                                INNER JOIN tbl_cust 
                                ON tbl_cust.cust_name=@keywords 
                                AND tbl_item_cust.cust_id = tbl_cust.cust_id) 
                                INNER JOIN tbl_item 
                                ON tbl_item_cust.item_code = tbl_item.item_code) ORDER BY tbl_item.item_name ASC";
                //
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
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

        public DataTable forecastSearch(string keywords, string itemName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM ((tbl_item_cust INNER JOIN tbl_cust ON tbl_cust.cust_name LIKE '%" + keywords + "%'AND tbl_item_cust.cust_id = tbl_cust.cust_id) INNER JOIN tbl_item ON tbl_item_cust.item_code = tbl_item.item_code AND tbl_item.item_name LIKE '%" + itemName + "%')";

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

        public DataTable existsSearch(string itemCode, string custID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item_cust WHERE item_code = @itemCode AND cust_id = @custID";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@custID", custID);
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

        public DataTable nameSearch(string itemName, string custName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();


            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM ((tbl_item_cust INNER JOIN tbl_item ON tbl_item.item_name LIKE '%" + itemName + "%' AND tbl_item.item_code = tbl_item_cust.item_code) INNER JOIN tbl_cust ON tbl_cust.cust_name LIKE '%" + custName + "%' AND tbl_cust.cust_id = tbl_item_cust.cust_id)";

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

        public DataTable itemSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM ((tbl_item_cust INNER JOIN tbl_item ON tbl_item.item_name LIKE '%" + keywords + "%'AND tbl_item_cust.item_code = tbl_item.item_code) INNER JOIN tbl_cust ON tbl_item_cust.cust_id = tbl_cust.cust_id)";

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

        public DataTable itemCodeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM ((tbl_item_cust 
                                INNER JOIN tbl_item 
                                ON tbl_item.item_code = @keywords" +
                                " AND tbl_item_cust.item_code = tbl_item.item_code) " +
                                "INNER JOIN tbl_cust " +
                                "ON tbl_item_cust.cust_id = tbl_cust.cust_id)";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@keywords", keywords);
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

        public bool checkIfExistinItemCustTable(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item_cust WHERE item_code = @keywords";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keywords", keywords);
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

            if(dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable checkItemCustTable(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item_cust WHERE item_code = @keywords";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keywords", keywords);
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
        public bool ifItemUnderThisCustomer(string itemCode, string custName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();


            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM ((tbl_item_cust INNER JOIN tbl_item ON tbl_item.item_code=@itemCode AND tbl_item.item_code = tbl_item_cust.item_code) INNER JOIN tbl_cust ON tbl_cust.cust_name=@custName AND tbl_cust.cust_id = tbl_item_cust.cust_id)";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@custName", custName);

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

            if(dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
