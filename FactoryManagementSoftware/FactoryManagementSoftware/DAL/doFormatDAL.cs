using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;
using Accord;
using System.Data.Entity.Core.Mapping;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                // Check if table exists and has data
                string checkTableAndDataSql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbl_do_format';
                                        SELECT COUNT(*) FROM tbl_do_format;";
                SqlCommand checkCmd = new SqlCommand(checkTableAndDataSql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(checkCmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                conn.Close();

                int tableExists = (int)ds.Tables[0].Rows[0][0];
                int dataExists = ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0 ? (int)ds.Tables[1].Rows[0][0] : 0;

                // If table doesn't exist or is empty, create/update table and add sample data
                if (tableExists == 0 || dataExists == 0)
                {
                    CreateTableOrUpdate(); // Ensure table is created and columns are updated

                    // Insert sample data only if there is no data
                    if (dataExists == 0)
                    {
                        doFormatBLL sampleDataOUG = new doFormatBLL()
                        {
                            letter_head_tbl_code = -1,
                            sheet_format_tbl_code = -1,
                            do_type = "Internal Transfer Note (From OUG)",
                            no_format = "IT[YY]/000",
                            prefix = "IT",
                            date_format = "[yy]/",
                            suffix = "",
                            running_number_length = 3,
                            last_number = 0,
                            reset_running_number = "False",
                            isInternal = true,
                            remark = "",
                            isRemoved = false,
                            updated_by = 1,
                            updated_date = DateTime.Now,
                            isMonthlyReset = false,
                            isYearlyReset = true,
                            last_reset_date = DateTime.Now
                        };
                        Insert(sampleDataOUG); // Insert sample data

                        doFormatBLL sampleDataSMY = new doFormatBLL()
                        {
                            letter_head_tbl_code = -1,
                            sheet_format_tbl_code = -1,
                            do_type = "Internal Transfer Note (From SMY)",
                            no_format = "ITN[YY]/S000",
                            prefix = "ITN",
                            date_format = "[yy]/S",
                            suffix = "",
                            running_number_length = 3,
                            last_number = 0,
                            reset_running_number = "False",
                            isInternal = true,
                            remark = "",
                            isRemoved = false,
                            updated_by = 1,
                            updated_date = DateTime.Now,
                            isMonthlyReset = false,
                            isYearlyReset = true,
                            last_reset_date = DateTime.Now
                        };
                        Insert(sampleDataSMY); // Insert sample data
                    }
                }

                // Re-fetch data to return
                if (conn.State != ConnectionState.Open) conn.Open();
                string sql = @"SELECT * FROM tbl_do_format";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter newAdapter = new SqlDataAdapter(cmd);
                newAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle the exception
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
                            "@last_number," +
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
                cmd.Parameters.AddWithValue("@last_number", u.last_number);
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

        public void CreateTableOrUpdate()
        {
            using (SqlConnection conn = new SqlConnection(myconnstrng))
            {
                try
                {
                    conn.Open();
                    // Command to check if the table exists and create it if it does not
                    string createTableSql = @"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbl_do_format')
            BEGIN
                CREATE TABLE tbl_do_format(
                    tbl_code INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                    letter_head_tbl_code INT,
                    sheet_format_tbl_code INT,
                    do_type VARCHAR(50),
                    no_format VARCHAR(50),
                    prefix VARCHAR(50),
                    date_format VARCHAR(50),
                    suffix VARCHAR(50),
                    running_number_length INT,
                    last_number INT,
                    reset_running_number BIT,
                    isMonthlyReset BIT,
                    isYearlyReset BIT,
                    last_reset_date DATETIME,
                    isInternal BIT,
                    remark VARCHAR(200),
                    isRemoved BIT,
                    updated_date DATETIME NOT NULL,
                    updated_by INT NOT NULL
                );
            END";
                    SqlCommand createTableCmd = new SqlCommand(createTableSql, conn);
                    createTableCmd.ExecuteNonQuery();

                    // Commands to add missing columns if the table already exists
                    string[] alterTableSql = new string[]
                    {
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'letter_head_tbl_code') BEGIN ALTER TABLE tbl_do_format ADD letter_head_tbl_code INT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'sheet_format_tbl_code') BEGIN ALTER TABLE tbl_do_format ADD sheet_format_tbl_code INT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'do_type') BEGIN ALTER TABLE tbl_do_format ADD do_type VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'no_format') BEGIN ALTER TABLE tbl_do_format ADD no_format VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'prefix') BEGIN ALTER TABLE tbl_do_format ADD prefix VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'date_format') BEGIN ALTER TABLE tbl_do_format ADD date_format VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'suffix') BEGIN ALTER TABLE tbl_do_format ADD suffix VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'running_number_length') BEGIN ALTER TABLE tbl_do_format ADD running_number_length INT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'last_number') BEGIN ALTER TABLE tbl_do_format ADD last_number INT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'reset_running_number') BEGIN ALTER TABLE tbl_do_format ADD reset_running_number BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'isMonthlyReset') BEGIN ALTER TABLE tbl_do_format ADD isMonthlyReset BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'isYearlyReset') BEGIN ALTER TABLE tbl_do_format ADD isYearlyReset BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'last_reset_date') BEGIN ALTER TABLE tbl_do_format ADD last_reset_date DATETIME END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'isInternal') BEGIN ALTER TABLE tbl_do_format ADD isInternal BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'remark') BEGIN ALTER TABLE tbl_do_format ADD remark VARCHAR(200) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'isRemoved') BEGIN ALTER TABLE tbl_do_format ADD isRemoved BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'updated_date') BEGIN ALTER TABLE tbl_do_format ADD updated_date DATETIME NOT NULL END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_do_format' AND COLUMN_NAME = 'updated_by') BEGIN ALTER TABLE tbl_do_format ADD updated_by INT NOT NULL END"
                    };

                    foreach (var sql in alterTableSql)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Tool tool = new Tool(); 
                    tool.saveToText(ex);

                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
