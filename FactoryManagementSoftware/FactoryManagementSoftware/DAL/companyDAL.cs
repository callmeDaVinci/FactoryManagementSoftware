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
    class companyDAL
    {
        #region data string name getter
        public string tblCode { get; } = "tbl_code";
        public string fullName { get; } = "full_name";
        public string shortName { get; } = "short_name";
        public string registrationNo { get; } = "registration_no";
        public string primaryBillingAddressNo { get; } = "primary_billing_address_code";
        public string primaryShippingAddressNo { get; } = "primary_shipping_address_code";
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
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                // Check if table exists and has data
                string checkTableAndDataSql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbl_company';
                                        SELECT COUNT(*) FROM tbl_company;";
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
                        // Assuming there's an Insert method for companyBLL similar to the Insert function provided before
                        companyBLL sampleData = new companyBLL()
                        {
                            // Initialize your sampleData object with appropriate values
                            full_name = "Safety Plastics Sdn Bhd",
                            short_name = "Safety Plastics",
                            registration_no = "198901011676 (188981-U)",
                            primary_billing_address_code = -1,
                            primary_shipping_address_code = -1,
                            isInternal = true,
                            remark = "",
                            isRemoved = false,
                            updated_by = 1,
                            updated_date = DateTime.Now
                        };
                        Insert(sampleData); // Insert sample data
                    }
                }

                // Re-fetch data to return
                if (conn.State != ConnectionState.Open) conn.Open();
                string sql = @"SELECT * FROM tbl_company";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter newAdapter = new SqlDataAdapter(cmd);
                newAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle the exception
                Tool tool = new Tool();
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


        #endregion

        #region Insert Data in Database

        public bool Insert(companyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_company 
                            (" + fullName + ","
                            + shortName + ","
                            + registrationNo + ","
                            + primaryBillingAddressNo + ","
                            + primaryShippingAddressNo + ","
                            + isInternal + ","
                            + remark + ","
                            + isRemoved + ","
                            + updatedDate + ","
                            + updatedBy + ") VALUES" +
                            "(@full_name," +
                            "@short_name," +
                            "@registration_no," +
                            "@primary_billing_address_code," +
                            "@primary_shipping_address_code," +
                            "@isInternal," +
                            "@remark," +
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@short_name", u.short_name);
                cmd.Parameters.AddWithValue("@registration_no", u.registration_no);
                cmd.Parameters.AddWithValue("@primary_billing_address_code", u.primary_billing_address_code);
                cmd.Parameters.AddWithValue("@primary_shipping_address_code", u.primary_shipping_address_code);
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

        public bool Update(companyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
             String sql = @"UPDATE tbl_company
                            SET "
                           + fullName + "=@full_name,"
                           + shortName + "=@short_name,"
                           + registrationNo + "=@registration_no,"
                           + primaryBillingAddressNo + "=@primary_billing_address_code,"
                           + primaryShippingAddressNo + "=@primary_shipping_address_code,"
                           + isInternal + "=@isInternal,"
                           + remark + "=@remark,"
                           + isRemoved + "=@isRemoved,"
                           + updatedDate + "=@updated_date,"
                           + updatedBy + "=@updated_by" +
                           " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@short_name", u.short_name);
                cmd.Parameters.AddWithValue("@registration_no", u.registration_no);
                cmd.Parameters.AddWithValue("@primary_billing_address_code", u.primary_billing_address_code);
                cmd.Parameters.AddWithValue("@primary_shipping_address_code", u.primary_shipping_address_code);
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

        public bool SoftRemove(companyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_company
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

        public bool RemovePermanently(companyBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_company WHERE tbl_code=@tbl_code";
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

        public void CreateTableOrUpdate()
        {
            using (SqlConnection conn = new SqlConnection(myconnstrng))
            {
                try
                {
                    conn.Open();
                    // Command to check if the table exists and create it if it does not
                    string createTableSql = @"
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbl_company')
BEGIN
    CREATE TABLE tbl_company(
        tbl_code INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
        primary_billing_address_code INT,
        primary_shipping_address_code INT,
        full_name VARCHAR(100),
        short_name VARCHAR(50),
        registration_no VARCHAR(50),
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
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'primary_billing_address_code') BEGIN ALTER TABLE tbl_company ADD primary_billing_address_code INT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'primary_shipping_address_code') BEGIN ALTER TABLE tbl_company ADD primary_shipping_address_code INT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'full_name') BEGIN ALTER TABLE tbl_company ADD full_name VARCHAR(100) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'short_name') BEGIN ALTER TABLE tbl_company ADD short_name VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'registration_no') BEGIN ALTER TABLE tbl_company ADD registration_no VARCHAR(50) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'isInternal') BEGIN ALTER TABLE tbl_company ADD isInternal BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'remark') BEGIN ALTER TABLE tbl_company ADD remark VARCHAR(200) END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'isRemoved') BEGIN ALTER TABLE tbl_company ADD isRemoved BIT END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'updated_date') BEGIN ALTER TABLE tbl_company ADD updated_date DATETIME NOT NULL END",
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_company' AND COLUMN_NAME = 'updated_by') BEGIN ALTER TABLE tbl_company ADD updated_by INT NOT NULL END"
                    };

                    foreach (var sql in alterTableSql)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    // Assuming Tool class has a method saveToText for logging exceptions
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
