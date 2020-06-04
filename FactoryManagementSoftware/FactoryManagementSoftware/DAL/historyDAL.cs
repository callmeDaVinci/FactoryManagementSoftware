using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class historyDAL
    {
        #region data string name getter

        public string HistoryID { get; } = "history_id";
        public string HistoryDate { get; } = "history_date";
        public string HistoryBy { get; } = "history_by";
        public string HistoryAction { get; } = "history_action";
        public string HistoryDetail { get; } = "history_detail";
        public string HistoryPageName { get; } = "page_name";
        public string HistoryDataID { get; } = "data_id";

        #endregion

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select

        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_history";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }

            dt.DefaultView.Sort = "history_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        #endregion

        #region insert

        public bool insert(historyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_history 
                            (" + HistoryDate + ","
                            + HistoryBy + ","
                            + HistoryAction + ","
                            + HistoryDetail + ") VALUES " +
                            "(@history_date," +
                            "@history_by," +
                            "@history_action," +
                            "@history_detail)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@history_date", u.history_date);
                cmd.Parameters.AddWithValue("@history_by", u.history_by);
                cmd.Parameters.AddWithValue("@history_action", u.history_action);
                cmd.Parameters.AddWithValue("@history_detail", u.history_detail);

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

        public bool insertWithDataID(historyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_history 
                            (" + HistoryDate + ","
                            + HistoryBy + ","
                            + HistoryAction + ","
                            + HistoryDetail + ","
                            + HistoryPageName + ","
                            + HistoryDataID + ") VALUES " +
                            "(@history_date," +
                            "@history_by," +
                            "@history_action," +
                            "@history_detail," +
                            "@page_name," +
                            "@data_id)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@history_date", u.history_date);
                cmd.Parameters.AddWithValue("@history_by", u.history_by);
                cmd.Parameters.AddWithValue("@history_action", u.history_action);
                cmd.Parameters.AddWithValue("@history_detail", u.history_detail);
                cmd.Parameters.AddWithValue("@page_name", u.page_name);
                cmd.Parameters.AddWithValue("@data_id", u.data_id);

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

        public bool update(historyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_history 
                            SET "
                           + HistoryDate + "=@history_date,"
                           + HistoryDetail + "=@history_detail"+
                           " WHERE history_id=@history_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@history_date", u.history_date);
                cmd.Parameters.AddWithValue("@history_id", 8607);
                cmd.Parameters.AddWithValue("@history_detail", "***SAVE DATE FOR FORECAST REPORT OUT START DATE***");

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region Search Data from Database

        public DataTable LogSearch(string tblName, string dataID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_history WHERE page_name = @tblName AND data_id =@dataID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tblName", tblName);
                cmd.Parameters.AddWithValue("@dataID", dataID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            dt.DefaultView.Sort = "history_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable userSearch(int userID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_history WHERE history_by = @userID ";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@userID", userID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            dt.DefaultView.Sort = "history_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DateTime GetForecastReportOutStartDate()
        {
            int id = 8607;//<---FIX ID for out start date data storing
            DateTime startDate = new DateTime();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT history_date FROM tbl_history WHERE history_id = @id ";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        startDate = Convert.ToDateTime(row["history_date"].ToString()).Date;
                    }
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

            return startDate;
        }

        #endregion
    }
}
