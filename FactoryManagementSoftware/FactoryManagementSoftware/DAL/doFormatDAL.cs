using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;
using Accord;
using System.Data.Entity.Core.Mapping;

namespace FactoryManagementSoftware.DAL
{
    class doFormatDAL
    {
        #region data string name getter
        public string tblCode { get; } = "tbl_code";
        public string letterHeadTblCode { get; } = "letter_head_tbl_code";
        public string sheetFormatTblCode { get; } = "sheet_format_tbl_code";
        public string doType { get; } = "do_type";
        public string noFormat { get; } = "no_format";
        public string prefix { get; } = "prefix";
        public string dateFormat { get; } = "date_format";
        public string suffix { get; } = "suffix";
        public string runningNumberLength { get; } = "running_number_length";
        public string nextStartNumber { get; } = "next_start_number";
        public string resetRunningNumber { get; } = "reset_running_number";
        public string isInternal { get; } = "isInternal";
        public string remark { get; } = "remark";
        public string isRemoved { get; } = "isRemoved";
        public string updatedDate { get; } = "updated_date";
        public string updatedBy { get; } = "updated_by";

        #endregion

        #region variable/class object declare

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #endregion

        #region Select Data from Database

        public DataTable SelectAll()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_do_format";
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
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
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

        public bool Insert(doFormatBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_do_format 
                            (" + letterHeadTblCode + ","
                            + sheetFormatTblCode + ","
                            + doType + ","
                            + noFormat + ","
                            + prefix + ","
                            + dateFormat + ","
                            + suffix + ","
                            + runningNumberLength + ","
                            + nextStartNumber + ","
                            + resetRunningNumber + ","
                            + isInternal + ","
                            + remark + ","
                            + isRemoved + ","
                            + updatedDate + ","
                            + updatedBy + ") VALUES" +
                            "(@letter_head_tbl_code," +
                            "@sheet_format_tbl_code," +
                            "@do_type," +
                            "@no_format," +
                            "@prefix," +
                            "@date_format," +
                            "@suffix," +
                            "@running_number_length," +
                            "@reset_running_number," +
                            "@isInternal," +
                            "@remark," +
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@letter_head_tbl_code", u.letter_head_tbl_code);
                cmd.Parameters.AddWithValue("@sheet_format_tbl_code", u.sheet_format_tbl_code);
                cmd.Parameters.AddWithValue("@do_type", u.do_type);
                cmd.Parameters.AddWithValue("@no_format", u.no_format);
                cmd.Parameters.AddWithValue("@prefix", u.prefix);
                cmd.Parameters.AddWithValue("@date_format", u.date_format);
                cmd.Parameters.AddWithValue("@suffix", u.suffix);
                cmd.Parameters.AddWithValue("@running_number_length", u.running_number_length);
                cmd.Parameters.AddWithValue("@reset_running_number", u.reset_running_number);
                cmd.Parameters.AddWithValue("@isInternal", u.isInternal);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
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

        public bool JobUpdate(doFormatBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
             String sql = @"UPDATE tbl_do_format
                            SET "
                           + letterHeadTblCode + "=@letter_head_tbl_code,"
                           + sheetFormatTblCode + "=@sheet_format_tbl_code,"
                           + doType + "=@do_type,"
                           + noFormat + "=@no_format,"
                           + prefix + "=@prefix,"
                           + dateFormat + "=@date_format,"
                           + suffix + "=@suffix,"
                           + runningNumberLength + "=@running_number_length,"
                           + resetRunningNumber + "=@reset_running_number,"
                           + isInternal + "=@isInternal,"
                           + remark + "=@remark,"
                           + isRemoved + "=@isRemoved,"
                           + updatedDate + "=@updated_date,"
                           + updatedBy + "=@updated_by" +
                           " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@letter_head_tbl_code", u.letter_head_tbl_code);
                cmd.Parameters.AddWithValue("@sheet_format_tbl_code", u.sheet_format_tbl_code);
                cmd.Parameters.AddWithValue("@do_type", u.do_type);
                cmd.Parameters.AddWithValue("@no_format", u.no_format);
                cmd.Parameters.AddWithValue("@prefix", u.prefix);
                cmd.Parameters.AddWithValue("@date_format", u.date_format);
                cmd.Parameters.AddWithValue("@suffix", u.suffix);
                cmd.Parameters.AddWithValue("@running_number_length", u.running_number_length);
                cmd.Parameters.AddWithValue("@reset_running_number", u.reset_running_number);
                cmd.Parameters.AddWithValue("@isInternal", u.isInternal);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
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

        #region Remove

        public bool SoftRemove(doFormatBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_do_format
                            SET "
                              + isRemoved + "=@isRemoved,"
                              + updatedDate + "=@updated_date,"
                              + updatedBy + "=@updated_by" +
                              " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
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

        public bool RemovePermanently(doFormatBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_do_format WHERE tbl_code=@tbl_code";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);

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


    }
}
