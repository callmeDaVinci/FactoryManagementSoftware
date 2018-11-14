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
                String sql = "INSERT INTO tbl_stock (stock_item_code, stock_fac_id, stock_qty, stock_updtd_date, stock_updtd_by) VALUES (@stock_item_code, @stock_fac_id, @stock_qty, @stock_updtd_date, @stock_updtd_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@stock_item_code", u.stock_item_code);
                cmd.Parameters.AddWithValue("@stock_fac_id", u.stock_fac_id);
                cmd.Parameters.AddWithValue("@stock_qty", u.stock_qty);
                cmd.Parameters.AddWithValue("@stock_updtd_date", u.stock_updtd_date);
                cmd.Parameters.AddWithValue("@stock_updtd_by", u.stock_updtd_by);

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
        public bool Update(facStockBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_stock SET stock_qty=@stock_qty,stock_updtd_date=@stock_updtd_date, stock_updtd_by=@stock_updtd_by WHERE stock_item_code=@stock_item_code AND stock_fac_id=@stock_fac_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@stock_item_code", u.stock_item_code);
                cmd.Parameters.AddWithValue("@stock_fac_id", u.stock_fac_id);
                cmd.Parameters.AddWithValue("@stock_qty", u.stock_qty);
                cmd.Parameters.AddWithValue("@stock_updtd_date", u.stock_updtd_date);
                cmd.Parameters.AddWithValue("@stock_updtd_by", u.stock_updtd_by);

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

        
    }
}
