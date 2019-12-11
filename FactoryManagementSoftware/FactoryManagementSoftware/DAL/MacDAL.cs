using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.UI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FactoryManagementSoftware.DAL
{
    class MacDAL
    {
        #region data string name getter

        public string MacID { get; } = "mac_id";
        public string MacName { get; } = "mac_name";
        public string MacTon { get; } = "mac_ton";
        public string MacLocation { get; } = "mac_location";

        public string MacAddedBy { get; } = "mac_added_by";
        public string MacAddedDate { get; } = "mac_added_date";

        public string MacUpdatedBy { get; } = "mac_updated_by";
        public string MacUpdatedDate { get; } = "mac_updated_date";

        public string MacLotNo { get; } = "mac_lot_no";

        #endregion

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
                String sql = "SELECT * FROM tbl_mac ORDER BY mac_id ASC";
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

        public DataTable SelectByFactory(string FacName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_mac WHERE mac_location = @FacName ORDER BY mac_id ASC ";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FacName", FacName);
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

        public bool Insert(MacBLL u)
        {

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_mac
                            (" + MacName + ","
                            + MacTon + ","
                            + MacLocation + ","
                            + MacLotNo + ","
                            + MacAddedBy + ","
                            + MacAddedDate + ") VALUES" +
                            "(@mac_name," +
                            "@mac_ton," +
                            "@mac_location," +
                            "@mac_lot_no," +
                            "@mac_added_by," +
                            "@mac_added_date)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mac_name", u.mac_name);
                cmd.Parameters.AddWithValue("@mac_ton", u.mac_ton);
                cmd.Parameters.AddWithValue("@mac_location", u.mac_location);
                cmd.Parameters.AddWithValue("@mac_lot_no", u.mac_lot_no);
                cmd.Parameters.AddWithValue("@mac_added_by", u.mac_added_by);
                cmd.Parameters.AddWithValue("@mac_added_date", u.mac_added_date);

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
                Module.Tool tool = new Module.Tool(); tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Update data in Database

        public bool Update(MacBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_mac 
                            SET "
                            + MacName + "=@mac_name,"
                            + MacTon + "=@mac_ton,"
                            + MacLocation + "=@mac_location,"
                            + MacUpdatedBy + "=@mac_updated_by,"
                            + MacUpdatedDate + "=@mac_updated_date" +
                            " WHERE mac_id=@mac_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mac_id", u.mac_id);
                cmd.Parameters.AddWithValue("@mac_name", u.mac_name);
                cmd.Parameters.AddWithValue("@mac_ton", u.mac_ton);
                cmd.Parameters.AddWithValue("@mac_location", u.mac_location);
                cmd.Parameters.AddWithValue("@mac_updated_by", u.mac_updated_by);
                cmd.Parameters.AddWithValue("@mac_updated_date", u.mac_updated_date);
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

        public bool UpdateLotNo(MacBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_mac 
                            SET "
                            + MacLotNo + "=@mac_lot_no,"
                            + MacUpdatedBy + "=@mac_updated_by,"
                            + MacUpdatedDate + "=@mac_updated_date" +
                            " WHERE mac_id=@mac_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mac_id", u.mac_id);
                cmd.Parameters.AddWithValue("@mac_lot_no", u.mac_lot_no);
                cmd.Parameters.AddWithValue("@mac_updated_by", u.mac_updated_by);
                cmd.Parameters.AddWithValue("@mac_updated_date", u.mac_updated_date);
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

        public bool Delete(MacBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_mac WHERE mac_id=@mac_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mac_id", u.mac_id);

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

        #region Search Data

        public DataTable idSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                int id = -1;

                bool successfullyParsed = int.TryParse(keywords, out id);

                if (!successfullyParsed)
                {
                    id = -1;
                }

                //sql query to get data from database
                String sql = "SELECT * FROM tbl_mac WHERE mac_id=@keywords";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keywords", id);
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
    }
}
