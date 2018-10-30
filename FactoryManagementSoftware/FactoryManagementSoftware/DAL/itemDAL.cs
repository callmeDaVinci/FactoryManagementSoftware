using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class itemDAL
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
                String sql = "SELECT * FROM tbl_item";
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
        public bool Insert(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_item (item_code, item_name, item_cat, item_color, item_weight, item_added_date, item_added_by) VALUES (@item_code, @item_name, @item_cat, @item_color, @item_weight, @item_added_date, @item_added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_weight", u.item_weight);
                cmd.Parameters.AddWithValue("@item_added_date", u.item_added_date);
                cmd.Parameters.AddWithValue("@item_added_by", u.item_added_by);

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
        public bool Update(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item SET item_name=@item_name, item_cat=@item_cat, item_color=@item_color, item_weight=@item_weight, item_updtd_date=@item_updtd_date, item_updtd_by=@item_updtd_by WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_name", u.item_name);
                cmd.Parameters.AddWithValue("@item_cat", u.item_cat);
                cmd.Parameters.AddWithValue("@item_color", u.item_color);
                cmd.Parameters.AddWithValue("@item_weight", u.item_weight);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);
                
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

        public bool qtyUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item SET item_qty=@item_qty, item_updtd_date=@item_updtd_date, item_updtd_by=@item_updtd_by WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_qty", u.item_qty);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);

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

        public bool ordUpdate(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_item SET item_ord=@item_ord, item_updtd_date=@item_updtd_date, item_updtd_by=@item_updtd_by WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@item_ord", u.item_ord);
                cmd.Parameters.AddWithValue("@item_updtd_date", u.item_updtd_date);
                cmd.Parameters.AddWithValue("@item_updtd_by", u.item_updtd_by);

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
        public bool Delete(itemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_item WHERE item_code=@item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

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
                String sql = "SELECT * FROM tbl_item WHERE item_code LIKE '%" + keywords + "%'OR item_name LIKE '%" + keywords + "%' OR item_cat LIKE '%" + keywords + "%'";

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

        public DataTable catSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_cat LIKE '%" + keywords + "%'";

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

        public DataTable nameSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_name LIKE '%" + keywords + "%'";

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

        public DataTable codeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_item WHERE item_code LIKE '%" + keywords + "%'";

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

        public float getOrderQty(string itemCode)
        {
            float orderQty = 0;
            DataTable dt = codeSearch(itemCode);

            orderQty = Convert.ToSingle(dt.Rows[0]["item_ord"].ToString());
            //MessageBox.Show("get qty= "+qty);


            return orderQty;
        }

        public bool orderAdd(string itemCode, string ordQty)
        {
            itemBLL uItem = new itemBLL();

            float number = Convert.ToSingle(ordQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_ord = getOrderQty(itemCode) + number;

            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;

        }

        public bool orderSubtract(string itemCode, string ordQty)
        {
            itemBLL uItem = new itemBLL();
            float number = Convert.ToSingle(ordQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_ord = getOrderQty(itemCode) - number;

            //Updating data into database
            bool success = ordUpdate(uItem);

            return success;

        }

        public float getStockQty(string itemCode)
        {
            float stockQty = 0;
            DataTable dt = codeSearch(itemCode);

            stockQty = Convert.ToSingle(dt.Rows[0]["item_qty"].ToString());
            //MessageBox.Show("get qty= "+qty);


            return stockQty;
        }

        public bool stockAdd(string itemCode, string stockQty)
        {
            itemBLL uItem = new itemBLL();

            float number = Convert.ToSingle(stockQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_qty = getStockQty(itemCode) + number;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

        public bool stockSubtract(string itemCode, string stockQty)
        {
            itemBLL uItem = new itemBLL();
            float number = Convert.ToSingle(stockQty);
            uItem.item_code = itemCode;
            uItem.item_updtd_date = DateTime.Now;
            uItem.item_updtd_by = 0;
            uItem.item_qty = getStockQty(itemCode) - number;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;

        }

        public bool updateTotalStock(string itemCode)
        {
            float totalStock = 0;
            stockDAL dalStock = new stockDAL();
            itemBLL uItem = new itemBLL();

            DataTable dtStock = dalStock.Select(itemCode);

            foreach (DataRow stock in dtStock.Rows)
            {
                totalStock += Convert.ToSingle(stock["stock_qty"].ToString());
            }

                //Update data
                uItem.item_code = itemCode;
                uItem.item_qty = totalStock;
                uItem.item_updtd_date = DateTime.Now;
                uItem.item_updtd_by = 0;

            //Updating data into database
            bool success = qtyUpdate(uItem);

            return success;
        }

    }
}
