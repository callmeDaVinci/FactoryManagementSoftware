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
        public string Wastage { get; } = "pmma_wastage";
        public string Adjust { get; } = "pmma_adjust";
        public string Note { get; } = "pmma_note";
        public string DirectOut { get; } = "pmma_direct_out";

        public string PMMAFrom { get; } = "pmma_from";
        public string PMMATo { get; } = "pmma_to";

        public string PMMAIn { get; } = "pmma_in";
        public string PMMAOut { get; } = "pmma_out";

        public string Month { get; } = "pmma_month";
        public string Year { get; } = "pmma_year";

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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable Select(pmmaBLL u)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_pmma WHERE pmma_item_code=@pmma_item_code AND pmma_month=@pmma_month AND pmma_year=@pmma_year";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
                cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);

                conn.Open();
                adapter.Fill(dt);
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
                            + Adjust + ","
                            + Note + ","
                            + AddedDate + ","
                            + AddedBy + ") VALUES " +
                            "(@pmma_item_code," +
                            "@pmma_openning_stock," +
                            "@pmma_bal_stock," +
                            "@pmma_date," +
                             "@pmma_adjust," +
                            "@pmma_note," +
                            "@pmma_added_date," +
                            "@pmma_added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);
                cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);
                cmd.Parameters.AddWithValue("@pmma_adjust", u.pmma_adjust);
                cmd.Parameters.AddWithValue("@pmma_note", u.pmma_note);
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool InsertOrUpdate(pmmaBLL u)
        {
            bool isSuccess = false;
            DataTable dt = Select(u);
            
            if (dt.Rows.Count > 0)
            {
                //update data
                isSuccess = NewUpdate(u);
            }
            else
            {
                SqlConnection conn = new SqlConnection(myconnstrng);

                try
                {

                    String sql = @"INSERT INTO tbl_pmma 
                            (" + itemCode + ","
                                + OpenStock + ","
                                + Adjust + ","
                                + Note + ","
                                + BalStock + ","
                                + Month + ","
                                + Year + ","                            
                                + AddedDate + ","
                                + AddedBy + ") VALUES " +
                                "(@pmma_item_code," +
                                "@pmma_openning_stock," +
                                 "@pmma_adjust," +
                                "@pmma_note," +
                                 "@pmma_bal_stock," +
                                "@pmma_month," +
                                "@pmma_year," +
                                "@pmma_added_date," +
                                "@pmma_added_by)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                    cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);

                    cmd.Parameters.AddWithValue("@pmma_adjust", u.pmma_adjust);
                    cmd.Parameters.AddWithValue("@pmma_note", u.pmma_note);
                    cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);

                    cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
                    cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);
                   

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
                    Module.Tool tool = new Module.Tool();
                    tool.saveToText(ex);
                }
                finally
                {
                    conn.Close();
                }
            }

            
           
            return isSuccess;
        }

        public bool InsertOrUpdateNextMonthOpenStock(pmmaBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_pmma SET "
                           + OpenStock + "=@pmma_openning_stock,"
                           + UpdatedDate + "=@pmma_added_date,"
                           + UpdatedBy + "=@pmma_added_by" +
                           " WHERE pmma_item_code=@pmma_item_code " +
                           "AND pmma_month=@pmma_month AND pmma_year=@pmma_year " +
                           "IF @@ROWCOUNT = 0 " +
                           "INSERT INTO tbl_pmma " +
                           "(" + itemCode + ", "
                            + OpenStock + ","
                            + Month + ","
                            + Year + ","
                            + AddedDate + ","
                            + AddedBy + ") VALUES " +
                            "(@pmma_item_code," +
                            "@pmma_openning_stock," +
                            "@pmma_month," +
                             "@pmma_year," +
                            "@pmma_added_date," +
                            "@pmma_added_by)  ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);

                cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
                cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);
                cmd.Parameters.AddWithValue("@pmma_added_date", u.pmma_added_date);
                cmd.Parameters.AddWithValue("@pmma_added_by", u.pmma_added_by);
                cmd.Parameters.AddWithValue("@pmma_updated_date", u.pmma_added_date);
                cmd.Parameters.AddWithValue("@pmma_updated_by", u.pmma_added_by);

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

        #region update

        public void MatDirectOut(pmmaBLL u)
        {

            //select data check if exist
            DataTable dt = Select(u);

            if (dt.Rows.Count > 0)
            {
                float adjustQty = 0;
                float outQty = u.pmma_adjust;
                string note = "";
                if (outQty < 0)
                {
                    outQty *= -1;
                }


                //get old data
                foreach (DataRow row in dt.Rows)
                {
                    adjustQty = float.TryParse(row[Adjust].ToString(), out adjustQty) ? adjustQty : 0;
                    note = row[Note].ToString();
                }

                adjustQty -= outQty;
                note += "Direct out " + outQty;

                //updata adjust & note
                // uPMMA.pmma_openning_stock = openStock;
                //uPMMA.pmma_in = inQty;
                //uPMMA.pmma_out = Out;
                //uPMMA.pmma_wastage = wastage;
                u.pmma_adjust = adjustQty;
                u.pmma_note = note;
                //uPMMA.pmma_bal_stock = balance;

                UpdateAdjustAndNote(u);
            }

        }

        public bool UpdateAdjustAndNote(pmmaBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_pmma SET "
                            + Adjust + "=@pmma_adjust,"
                            + Note + "=@pmma_note,"
                            + UpdatedDate + "=@pmma_updated_date,"
                            + UpdatedBy + "=@pmma_updated_by" +
                            " WHERE pmma_item_code=@pmma_item_code " +
                            "AND pmma_month=@pmma_month AND pmma_year=@pmma_year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
            
                cmd.Parameters.AddWithValue("@pmma_adjust", u.pmma_adjust);
                cmd.Parameters.AddWithValue("@pmma_note", u.pmma_note);
               
                cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
                cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool NewUpdate(pmmaBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            string month = u.pmma_date.Month.ToString();
            string year = u.pmma_date.Year.ToString();

            try
            {
                String sql = @"UPDATE tbl_pmma SET "
                            + OpenStock + "=@pmma_openning_stock,"
                            + PMMAIn + "=@pmma_in,"
                            + PMMAOut + "=@pmma_out,"
                            + Wastage + "=@pmma_wastage,"
                            + Adjust + "=@pmma_adjust,"
                            + Note + "=@pmma_note,"
                            + BalStock + "=@pmma_bal_stock,"
                            + UpdatedDate + "=@pmma_updated_date,"
                            + UpdatedBy + "=@pmma_updated_by" +
                            " WHERE pmma_item_code=@pmma_item_code " +
                            "AND pmma_month=@pmma_month AND pmma_year=@pmma_year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);
                cmd.Parameters.AddWithValue("@pmma_in", u.pmma_in);
                cmd.Parameters.AddWithValue("@pmma_out", u.pmma_out);
                cmd.Parameters.AddWithValue("@pmma_wastage", u.pmma_wastage);

                cmd.Parameters.AddWithValue("@pmma_adjust", u.pmma_adjust);
                cmd.Parameters.AddWithValue("@pmma_note", u.pmma_note);
                cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);

                cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
                cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //public bool update(pmmaBLL u)
        //{
        //    bool isSuccess = false;
        //    SqlConnection conn = new SqlConnection(myconnstrng);
        //    string month = u.pmma_date.Month.ToString();
        //    string year = u.pmma_date.Year.ToString();
            
        //    try
        //    {
        //        String sql = @"UPDATE tbl_pmma SET "
        //                    + OpenStock + "=@pmma_openning_stock,"
        //                     + BalStock + "=@pmma_bal_stock,"
        //                      + Wastage + "=@pmma_percentage,"
        //                       + Adjust + "=@pmma_adjust,"
        //                        + Note + "=@pmma_note,"
        //                    + UpdatedDate + "=@pmma_updated_by,"
        //                    + UpdatedBy + "=@pmma_updated_by" +
        //                    " WHERE pmma_item_code=@pmma_item_code " +
        //                    "AND MONTH(pmma_date)=@month AND YEAR(pmma_date)=@year";

        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
        //        cmd.Parameters.AddWithValue("@pmma_openning_stock", u.pmma_openning_stock);
        //        cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);
        //        cmd.Parameters.AddWithValue("@pmma_adjust", u.pmma_adjust);
        //        cmd.Parameters.AddWithValue("@pmma_note", u.pmma_note);
        //        cmd.Parameters.AddWithValue("@pmma_percentage", u.pmma_wastage);
        //        cmd.Parameters.AddWithValue("@month", month);
        //        cmd.Parameters.AddWithValue("@year", year);
        //        cmd.Parameters.AddWithValue("@pmma_updated_date", u.pmma_updated_date);
        //        cmd.Parameters.AddWithValue("@pmma_updated_by", u.pmma_updated_by);

        //        conn.Open();

        //        int rows = cmd.ExecuteNonQuery();

        //        //if the query is executed successfully then the rows' value = 0
        //        if (rows > 0)
        //        {
        //            //query successful
        //            isSuccess = true;
        //        }
        //        else
        //        {
        //            //Query falled
        //            isSuccess = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return isSuccess;
        //}

        //public bool updateMonthAndYear(pmmaBLL u)
        //{
        //    bool isSuccess = false;
        //    SqlConnection conn = new SqlConnection(myconnstrng);

        //    try
        //    {
        //        u.pmma_date = new DateTime(Convert.ToInt32(u.pmma_year), Convert.ToInt32(u.pmma_month), 1);

        //        String sql = @"UPDATE tbl_pmma SET "
        //                    + Month + "=@pmma_month,"
        //                     + Year + "=@pmma_year,"
        //                    + UpdatedDate + "=@pmma_updated_by,"
        //                    + UpdatedBy + "=@pmma_updated_by" +
        //                    " WHERE tbl_pmma.pmma_item_code = @pmma_item_code AND MONTH(tbl_pmma.pmma_date) = @pmma_month AND YEAR(tbl_pmma.pmma_date) = @pmma_year";

        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);
        //        cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
        //        cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);
        //        cmd.Parameters.AddWithValue("@pmma_updated_date", u.pmma_updated_date);
        //        cmd.Parameters.AddWithValue("@pmma_updated_by", u.pmma_updated_by);

        //        conn.Open();

        //        int rows = cmd.ExecuteNonQuery();

        //        //if the query is executed successfully then the rows' value = 0
        //        if (rows > 0)
        //        {
        //            //query successful
        //            isSuccess = true;
        //        }
        //        else
        //        {
        //            //Query falled
        //            isSuccess = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return isSuccess;
        //}

        //public bool updateAdjustedBal(pmmaBLL u)
        //{
        //    bool isSuccess = false;
        //    SqlConnection conn = new SqlConnection(myconnstrng);

        //    try
        //    {
        //        String sql = @"UPDATE tbl_pmma SET "
        //                    + Adjust + "=@pmma_adjust,"
        //                     + Note + "=@pmma_note,"
        //                      + BalStock + "=@pmma_bal_stock,"
        //                    + UpdatedDate + "=@pmma_updated_by,"
        //                    + UpdatedBy + "=@pmma_updated_by" +
        //                    " WHERE tbl_pmma.pmma_item_code = @pmma_item_code AND tbl_pmma.pmma_month = @pmma_month AND tbl_pmma.pmma_year = @pmma_year";

        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@pmma_item_code", u.pmma_item_code);

        //        cmd.Parameters.AddWithValue("@pmma_adjust", u.pmma_adjust);
        //        cmd.Parameters.AddWithValue("@pmma_note", u.pmma_note);
        //        cmd.Parameters.AddWithValue("@pmma_bal_stock", u.pmma_bal_stock);

        //        cmd.Parameters.AddWithValue("@pmma_month", u.pmma_month);
        //        cmd.Parameters.AddWithValue("@pmma_year", u.pmma_year);
        //        cmd.Parameters.AddWithValue("@pmma_updated_date", u.pmma_updated_date);
        //        cmd.Parameters.AddWithValue("@pmma_updated_by", u.pmma_updated_by);

        //        conn.Open();

        //        int rows = cmd.ExecuteNonQuery();

        //        //if the query is executed successfully then the rows' value = 0
        //        if (rows > 0)
        //        {
        //            //query successful
        //            isSuccess = true;
        //        }
        //        else
        //        {
        //            //Query falled
        //            isSuccess = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return isSuccess;
        //}
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

        public DataTable TestSearch(pmmaBLL u)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * FROM tbl_pmma INNER JOIN tbl_item 
                                ON tbl_pmma.pmma_item_code = tbl_item.item_code 
                                AND tbl_pmma.pmma_item_code = @itemCode AND MONTH(tbl_pmma.pmma_date) = @month AND YEAR(tbl_pmma.pmma_date) = @year";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", u.pmma_item_code);
                cmd.Parameters.AddWithValue("@month", u.pmma_month);
                cmd.Parameters.AddWithValue("@year", u.pmma_year);

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
            return dt;
        }

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable SearchByOldDate(string month, string year)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * FROM tbl_pmma INNER JOIN tbl_item 
                                ON tbl_pmma.pmma_item_code = tbl_item.item_code 
                                AND MONTH(tbl_pmma.pmma_date) = @month AND YEAR(tbl_pmma.pmma_date) = @year";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable SearchByNewDate(string month, string year)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = @"SELECT * FROM tbl_pmma INNER JOIN tbl_item 
                                ON tbl_pmma.pmma_item_code = tbl_item.item_code 
                                AND tbl_pmma.pmma_month = @month AND tbl_pmma.pmma_year = @year";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
