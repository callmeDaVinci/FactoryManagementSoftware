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
        public string lastNumber { get; } = "last_number";
        public string resetRunningNumber { get; } = "reset_running_number";
        public string isInternal { get; } = "isInternal";
        public string remark { get; } = "remark";
        public string isRemoved { get; } = "isRemoved";
        public string updatedDate { get; } = "updated_date";
        public string updatedBy { get; } = "updated_by";

        public string lastResetDate { get; } = "last_reset_date";
        public string isMonthlyReset { get; } = "isMonthlyReset";
        public string isYearlyReset { get; } = "isYearlyReset";


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

        public doFormatBLL GetDOFormatByID(string id)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            doFormatBLL doFormat = new doFormatBLL();
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_do_format WHERE tbl_code = @tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    doFormat.tbl_code = row[tblCode] != DBNull.Value ? Convert.ToInt32(row[tblCode]) : -1;
                    doFormat.letter_head_tbl_code = row[letterHeadTblCode] != DBNull.Value ? Convert.ToInt32(row[letterHeadTblCode]) : -1;
                    doFormat.sheet_format_tbl_code = row[sheetFormatTblCode] != DBNull.Value ? Convert.ToInt32(row[sheetFormatTblCode]) : -1;
                    doFormat.do_type = row[doType] != DBNull.Value ? row[doType].ToString() : "";
                    doFormat.no_format = row[noFormat] != DBNull.Value ? row[noFormat].ToString() : "";
                    doFormat.prefix = row[prefix] != DBNull.Value ? row[prefix].ToString() : "";
                    doFormat.date_format = row[dateFormat] != DBNull.Value ? row[dateFormat].ToString() : "";
                    doFormat.suffix = row[suffix] != DBNull.Value ? row[suffix].ToString() : "";
                    doFormat.running_number_length = row[runningNumberLength] != DBNull.Value ? Convert.ToInt32(row[runningNumberLength]) : -1;
                    doFormat.last_number = row[lastNumber] != DBNull.Value ? Convert.ToInt32(row[lastNumber]) : -1;
                    doFormat.reset_running_number = row[resetRunningNumber] != DBNull.Value ? row[resetRunningNumber].ToString() : "";
                    doFormat.isInternal = row[isInternal] != DBNull.Value ? Convert.ToBoolean(row[isInternal]) : false;
                    doFormat.remark = row[remark] != DBNull.Value ? row[remark].ToString() : "";
                    doFormat.isRemoved = row[isRemoved] != DBNull.Value ? Convert.ToBoolean(row[isRemoved]) : false;
                    doFormat.updated_by = row[updatedBy] != DBNull.Value ? Convert.ToInt32(row[updatedBy]) : -1;
                    doFormat.updated_date = row[updatedDate] != DBNull.Value ? Convert.ToDateTime(row[updatedDate]) : DateTime.MinValue;
                    doFormat.isMonthlyReset = row[isMonthlyReset] != DBNull.Value ? Convert.ToBoolean(row[isMonthlyReset]) : false;
                    doFormat.isYearlyReset = row[isYearlyReset] != DBNull.Value ? Convert.ToBoolean(row[isYearlyReset]) : false;
                    doFormat.last_reset_date = row[lastResetDate] != DBNull.Value ? Convert.ToDateTime(row[lastResetDate]) : DateTime.MinValue;



                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                conn.Close();
            }
            return doFormat;
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
                            + lastNumber + ","
                            + resetRunningNumber + ","
                            + isMonthlyReset + ","
                            + isYearlyReset + ","
                            + lastResetDate + ","
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
                            "@isMonthlyReset," +
                            "@isYearlyReset," +
                            "@last_reset_date," +
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
                cmd.Parameters.AddWithValue("@isMonthlyReset", u.isMonthlyReset);
                cmd.Parameters.AddWithValue("@isYearlyReset", u.isYearlyReset);
                cmd.Parameters.AddWithValue("@last_reset_date", u.last_reset_date);
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

        public bool Update(doFormatBLL u)
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
                           + isMonthlyReset + "=@isMonthlyReset,"
                           + isYearlyReset + "=@isYearlyReset,"
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
                cmd.Parameters.AddWithValue("@isMonthlyReset", u.isMonthlyReset);
                cmd.Parameters.AddWithValue("@isYearlyReset", u.isYearlyReset);
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

        // Method to update the Last Number and Last Reset Date
        public bool UpdateLastNumberAndResetDate(doFormatBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_do_format SET "
                            + lastNumber + "=@last_number, "
                            + lastResetDate + "=@last_reset_date "
                            + "WHERE " + tblCode + "=@tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@last_number", u.last_number);
                cmd.Parameters.AddWithValue("@last_reset_date", u.last_reset_date);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                // If the query is executed successfully then the rows value > 0
                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                // Close the connection
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

        public int GenerateDONumber(doFormatBLL format)
        {
            // Extract details from the format object
            string prefix = format.prefix;
            string dateFormat = format.date_format;
            string suffix = format.suffix;
            int runningNumberLength = format.running_number_length;
            int lastNumber = format.last_number;
            DateTime lastResetDate = format.last_reset_date;
            bool isMonthlyReset = format.isMonthlyReset;
            bool isYearlyReset = format.isYearlyReset;

            // Get the current date
            DateTime currentDate = DateTime.Now;

            if (format.last_reset_date == DateTime.MinValue)
            {
                lastNumber = 0;
                lastResetDate = currentDate;

            }
            // Check if a reset is needed
            else if ((isYearlyReset && currentDate.Year != lastResetDate.Year) ||
                (isMonthlyReset && (currentDate.Year != lastResetDate.Year || currentDate.Month != lastResetDate.Month)))
            {
                lastNumber = 0; // Reset the last number
                lastResetDate = currentDate;
            }

            // Increment the last number
            lastNumber++;

            //// Apply date formatting
            //string formattedDate = ApplyDateFormat(dateFormat, currentDate);

            //// Format the running number with leading zeros
            //string formattedRunningNumber = lastNumber.ToString().PadLeft(runningNumberLength, '0');

            //// Concatenate to form the DO number
            //string newDONumber = $"{prefix}{formattedDate}{formattedRunningNumber}{suffix}";

            // Update the table with the new last number and reset date
            format.last_number = lastNumber;
            format.last_reset_date = lastResetDate;

            UpdateLastNumberAndResetDate(format);

            return lastNumber;
        }

        public string ApplyDOFormat(doFormatBLL format, int newNumber)
        {
            // Extract details from the format object
            string prefix = format.prefix;
            string dateFormat = format.date_format;
            string suffix = format.suffix;
            int runningNumberLength = format.running_number_length;

            if(string.IsNullOrEmpty(prefix))
            {
                prefix = string.Empty;
            }

            if (string.IsNullOrEmpty(suffix))
            {
                suffix = string.Empty;
            }

            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Apply date formatting
            string formattedDate = ApplyDateFormat(dateFormat, currentDate);

            // Format the running number with leading zeros
            string formattedRunningNumber = newNumber.ToString().PadLeft(runningNumberLength, '0');

            // Concatenate to form the DO number
            string newDONo = $"{prefix}{formattedDate}{formattedRunningNumber}{suffix}";

            return newDONo;
        }

        private string ApplyDateFormat(string dateFormat, DateTime date)
        {
            // Replace date format placeholders with actual date values
            return dateFormat.Replace("[yyyy]", date.ToString("yyyy"))
                             .Replace("[yy]", date.ToString("yy"))
                             .Replace("[mm]", date.ToString("MM"))
                             .Replace("[dd]", date.ToString("dd"))
                             .Replace("[MMM]", date.ToString("MMM").ToUpper())
                             .Replace("[MMMM]", date.ToString("MMMM"));
        }

    }
}
