using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.UI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FactoryManagementSoftware.DAL
{
    class pmmaDateDAL
    {
        #region data string name getter

        public string dateMonth { get; } = "month";
        public string dateYear { get; } = "year";
        public string dateStart { get; } = "date_start";
        public string dateEnd { get; } = "date_end";
        public string dateUpdatedDate { get; } = "updated_date";
        public string dateUpdatedBy { get; } = "updated_by";

        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

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
                String sql = "SELECT * FROM tbl_pmma_date";
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
      
        public bool Insert(pmmaDateBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_pmma_date 
                            (" + dateMonth + "," + dateYear + "," + dateStart + "," + dateEnd + "," + dateUpdatedDate + "," + dateUpdatedBy + ") VALUES (@month,@year," +
                            "@date_start," +
                            "@date_end," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@month", u.month);
                cmd.Parameters.AddWithValue("@year", u.year);
                cmd.Parameters.AddWithValue("@date_start", u.date_start);
                cmd.Parameters.AddWithValue("@date_end", u.date_end);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);


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

        public bool Update(pmmaDateBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_pmma_date 
                            SET "
                            + dateStart + "=@date_start,"
                            + dateEnd + "=@date_end,"
                            + dateUpdatedDate + "=@updated_date,"
                            + dateUpdatedBy + "=@updated_by" +
                            " WHERE month=@month AND year=@year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@month", u.month);
                cmd.Parameters.AddWithValue("@year", u.year);
                cmd.Parameters.AddWithValue("@date_start", u.date_start);
                cmd.Parameters.AddWithValue("@date_end", u.date_end);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

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
                String sql = "SELECT * FROM tbl_pmma_date WHERE month LIKE '%" + keywords + "%'";

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
    }
}
