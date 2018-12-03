using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class facStockDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        facStockBLL uStock = new facStockBLL();
        itemDAL dalItem = new itemDAL();

        #region Select Data from Database
        public DataTable Select(string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @" SELECT tbl_fac.fac_name, tbl_stock.stock_qty 
                                FROM((tbl_stock 
                                INNER JOIN tbl_item 
                                ON tbl_item.item_code =@itemCode 
                                AND  tbl_item.item_code = tbl_stock.stock_item_code ) 
                                INNER JOIN tbl_fac 
                                ON tbl_stock.stock_fac_id = tbl_fac.fac_id)";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);
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
        public bool Insert( facStockBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_stock (stock_item_code, stock_fac_id, stock_qty, stock_unit, stock_updtd_date, stock_updtd_by) VALUES (@stock_item_code, @stock_fac_id, @stock_qty, @stock_unit, @stock_updtd_date, @stock_updtd_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@stock_item_code", u.stock_item_code);
                cmd.Parameters.AddWithValue("@stock_fac_id", u.stock_fac_id);
                cmd.Parameters.AddWithValue("@stock_qty", u.stock_qty);
                cmd.Parameters.AddWithValue("@stock_unit", u.stock_unit);
                cmd.Parameters.AddWithValue("@stock_updtd_date", u.stock_updtd_date);
                cmd.Parameters.AddWithValue("@stock_updtd_by", u.stock_updtd_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                    dalItem.updateTotalStock(u.stock_item_code);
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
        public bool Update(facStockBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_stock SET stock_qty=@stock_qty, stock_unit=@stock_unit, stock_updtd_date=@stock_updtd_date, stock_updtd_by=@stock_updtd_by WHERE stock_item_code=@stock_item_code AND stock_fac_id=@stock_fac_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@stock_item_code", u.stock_item_code);
                cmd.Parameters.AddWithValue("@stock_fac_id", u.stock_fac_id);
                cmd.Parameters.AddWithValue("@stock_qty", u.stock_qty);
                cmd.Parameters.AddWithValue("@stock_unit", u.stock_unit);
                cmd.Parameters.AddWithValue("@stock_updtd_date", u.stock_updtd_date);
                cmd.Parameters.AddWithValue("@stock_updtd_by", u.stock_updtd_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the rows' value = 0
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                    dalItem.updateTotalStock(u.stock_item_code);

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

        #region Search stock record
        public DataTable Search(string itemCode, string factoryID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_stock WHERE stock_item_code LIKE '%" + itemCode + "%' AND stock_fac_id LIKE '%" + factoryID + "%'";

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

        private bool IfExists(string itemCode, string facID)
        {
            DataTable dt = Search(itemCode, facID);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private float getQty(string itemCode, string facID)
        {
            float qty = 0;
            if (IfExists(itemCode, facID))
            {
                DataTable dt = Search(itemCode, facID);

                qty = Convert.ToSingle(dt.Rows[0]["stock_qty"].ToString());
            }
            else
            {
                qty = 0;
            }

            return qty;
        }

        public bool facStockIn(string facID, string itemCode, float qty, string unit)
        {
            bool result = true;
            uStock.stock_item_code = itemCode;
            uStock.stock_fac_id = Convert.ToInt32(facID);
            uStock.stock_qty = getQty(itemCode, facID) + qty;
            uStock.stock_unit = unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = 0;

            if (IfExists(itemCode, facID))
            {
                //Updating data into database
                bool success = Update(uStock);

                if(!success)
                {
                    //failed to update user
                    result = false;
                    MessageBox.Show("Failed to updated stock");
                }
            }
            else
            {
                //Inserting Data into Database
                bool success = Insert(uStock);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    //Failed to insert data
                    result = false;
                    MessageBox.Show("Failed to add new stock");
                }
            }
            return result;
        }

        public bool facStockOut(string facID, string itemCode, float qty,string unit)
        {
            bool result = true;
            uStock.stock_item_code = itemCode;
            uStock.stock_fac_id = Convert.ToInt32(facID);
            uStock.stock_qty = getQty(itemCode, facID) - qty;
            uStock.stock_unit = unit;
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = 0;

            if (IfExists(itemCode, facID))
            {
                //Updating data into database
                bool success = Update(uStock);

                if (!success)
                {
                    //failed to update user
                    result = false;
                    MessageBox.Show("Failed to updated stock");
                }
            }
            else
            {
                //Inserting Data into Database
                bool success = Insert(uStock);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    //Failed to insert data
                    result = false;
                    MessageBox.Show("Failed to add new stock");
                }
            }

            return result;
        }
    }
}
