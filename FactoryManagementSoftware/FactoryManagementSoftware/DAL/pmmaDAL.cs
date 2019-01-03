using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class pmmaDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region data string name getter

        public string itemCode { get; } = "pmma_item_code";
        public string OpenStock { get; } = "pmma_openning_stock";
        public string BalStock { get; } = "pmma_bal_stock";
        public string Date { get; } = "pmma_date";

        public string AddedDate { get; } = "pmma_added_date";
        public string AddedBy { get; } = "pmma_added_by";
        public string UpdatedDate { get; } = "pmma_updated_date";
        public string UpdatedBy { get; } = "pmma_updated_by";

        #endregion

        #region Select

        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_pmma INNER JOIN tbl_item ON tbl_pmma.pmma_item_code = tbl_item.item_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

        #region insert

        public bool insert(pmmaBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_pmma 
                            (" + itemCode + ","
                            + OpenStock + ","
                            + BalStock + ","
                            + Date + ","
                            + AddedDate + ","
                            + AddedBy + ") VALUES " +
                            "(@pmma_item_code," +
                            "@pmma_openning_stock," +
                            "@pmma_bal_stock," +
                            "@pmma_date," +
                            "@pmma_added_date," +
                            "@pmma_added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);
                cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);
                cmd.Parameters.AddWithValue("@pmma_date", u.pmma_date);
                cmd.Parameters.AddWithValue("@pmma_added_date", u.pmma_added_date);
                cmd.Parameters.AddWithValue("@pmma_added_by", u.pmma_added_by);

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

        #region update

        public bool update(pmmaBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            string month = u.pmma_date.Month.ToString();
            string year = u.pmma_date.Year.ToString();
            
            try
            {
                String sql = @"UPDATE tbl_pmma SET "
                            + OpenStock + "=@pmma_openning_stock,"
                             + BalStock + "=@pmma_bal_stock,"
                            + UpdatedDate + "=@pmma_updated_by,"
                            + UpdatedBy + "=@pmma_updated_by" +
                            " WHERE pmma_item_code=@pmma_item_code " +
                            "AND MONTH(pmma_date)=@month AND YEAR(pmma_date)=@year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);
                cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@pmma_updated_date", u.pmma_updated_date);
                cmd.Parameters.AddWithValue("@pmma_updated_by", u.pmma_updated_by);

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

        #region delete

        public bool Delete(pmmaBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_pmma WHERE pmma_item_code=@pmma_item_code AND pmma_date=@pmma_date";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_date", u.pmma_date);

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

        #region Search Data from Database

        public DataTable Search(string itemCode, string month, string year)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * FROM tbl_pmma INNER JOIN tbl_item 
                                ON tbl_pmma.pmma_item_code = tbl_item.item_code 
                                AND tbl_pmma.pmma_item_code = @itemCode AND MONTH(tbl_pmma.pmma_date) = @month AND YEAR(tbl_pmma.pmma_date) = @year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion
    }
}
