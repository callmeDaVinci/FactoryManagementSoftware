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

        #endregion

        #region Search Data from Database

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

        #endregion
    }
}
