using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;
using Accord;

namespace FactoryManagementSoftware.DAL
{
    class addressBookDAL
    {
        #region data string name getter
        public string tblCode { get; } = "tbl_code";
        public string companyTblCode { get; } = "company_tbl_code";
        public string fullName { get; } = "full_name";
        public string shortName { get; } = "short_name";
        public string registrationNo { get; } = "registration_no";
        public string addressLine1 { get; } = "address_1";
        public string addressLine2 { get; } = "address_2";
        public string addressLine3 { get; } = "address_3";
        public string addressState { get; } = "address_state";
        public string addressPostalCode { get; } = "address_postal_code";
        public string addressCountry { get; } = "address_country";
        public string faxNo { get; } = "fax_no";
        public string contactNo1 { get; } = "contact_number_1";
        public string contactName1 { get; } = "contact_name_1";
        public string contactNo2 { get; } = "contact_number_2";
        public string contactName2 { get; } = "contact_name_2";
        public string emailAddress { get; } = "email_address";
        public string website { get; } = "website";
        public string routeTblCode { get; } = "route_tbl_code";
        public string remark { get; } = "remark";
        public string isRemoved { get; } = "isRemoved";
        public string updatedDate { get; } = "updated_date";
        public string updatedBy { get; } = "updated_by";

        public string deliveryMethodRemark { get; } = "delivery_method_remark";


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
                string checkTableAndDataSql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbl_address_book';
                                        SELECT COUNT(*) FROM tbl_address_book;";
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
                        AddressBookBLL sampleData = new AddressBookBLL()
                        {
                            company_tbl_code = 1, // Adjust as necessary
                            route_tbl_code = -1, // Adjust as necessary
                            full_name = "SAFETY PLASTICS SDN BHD (SEMENYIH FAC.)",
                            short_name = "Semenyih",
                            registration_no = "198901011676 (188981-U)",
                            address_1 = "NO.17, PT 2507, JLN HI-TECH 2,",
                            address_2 = "KAW. PERIND. HI.TECH,",
                            address_3 = "JALAN SG. LALANG,",
                            address_state = "SEMENYIH, SELANGOR.",
                            address_postal_code = "43500",
                            address_country = "Malaysia",
                            fax_no = "03 - 7782 0399",
                            contact_number_1 = "016 - 282 8195",
                            contact_name_1 = "Vincent",
                            contact_number_2 = "",
                            contact_name_2 = "",
                            email_address = "sales@safetyplastics.com.my",
                            website = "www.safetyplastic.com.my",
                            delivery_method_remark = "In-House Delivery",
                            remark = "",
                            isRemoved = false,
                            updated_by = 1, // Assume an admin or system ID
                            updated_date = DateTime.Now
                        };

                        // Insert sample data using the Insert function
                        Insert(sampleData);

                        sampleData = new AddressBookBLL()
                        {
                            company_tbl_code = 1, // Adjust as necessary
                            route_tbl_code = -1, // Adjust as necessary
                            full_name = "SAFETY PLASTICS SDN BHD (OUG FAC.)",
                            short_name = "OUG",
                            registration_no = "198901011676 (188981-U)",
                            address_1 = "NO.2, JALAN 10/152,",
                            address_2 = "TAMAN PERINDUSTRIAN O.U.G,",
                            address_3 = "BATU 6, JALAN PUCHONG,",
                            address_state = "KUALA LUMPUR.",
                            address_postal_code = "58200",
                            address_country = "MALAYSIA",
                            fax_no = "03 - 7782 0399",
                            contact_number_1 = "03 - 7785 5278",
                            contact_name_1 = "Ms.Yong",
                            contact_number_2 = "",
                            contact_name_2 = "",
                            email_address = "safety_plastic@yahoo.com",
                            website = "www.safetyplastic.com.my",
                            delivery_method_remark = "In-House Delivery",
                            remark = "",
                            isRemoved = false,
                            updated_by = 1, // Assume an admin or system ID
                            updated_date = DateTime.Now
                        };

                        // Insert sample data using the Insert function
                        Insert(sampleData);
                    }
                }

                // Re-fetch data to return
                if (conn.State != ConnectionState.Open) conn.Open();
                string sql = "SELECT * FROM tbl_address_book";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter newAdapter = new SqlDataAdapter(cmd);
                newAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
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

        public string SearchShortName(string tblCode)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // SQL query to get tbl_code from database where do_no matches
                String sql = @"SELECT tbl_address_book FROM tbl_internal_do WHERE tbl_code = @tblCode";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tblCode", tblCode);

                conn.Open();

                // Execute the query and get the tbl_code
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    tblCode = result.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }

            return tblCode;
        }

        public string SearchShortName(DataTable dt, string addressTblCode)
        {
            string name = "";

            if(dt?.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if(addressTblCode == row[tblCode].ToString())
                    {
                        name = row[shortName].ToString();   
                        break;
                    }
                }
            }
            return name;
        }

        public string SearchFullName(DataTable dt, string addressTblCode)
        {
            string name = "";

            if (dt?.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (addressTblCode == row[tblCode].ToString())
                    {
                        name = row[fullName].ToString();
                        break;
                    }
                }
            }
            return name;
        }

        public AddressBookBLL GetAddressInfo(DataTable dt, string addressTblCode)
        {
            AddressBookBLL uAddressBook = new AddressBookBLL();

            if (dt?.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (addressTblCode == row[tblCode].ToString())
                    {
                        // Assuming AddressBookBLL properties are public and have setters.
                        uAddressBook.full_name = row[fullName].ToString();
                        uAddressBook.short_name = row[shortName].ToString();
                        uAddressBook.registration_no = row[registrationNo].ToString();
                        uAddressBook.address_1 = row[addressLine1].ToString();
                        uAddressBook.address_2 = row[addressLine2].ToString();
                        uAddressBook.address_3 = row[addressLine3].ToString();
                        uAddressBook.address_state = row[addressState].ToString();
                        uAddressBook.address_postal_code = row[addressPostalCode].ToString();
                        uAddressBook.address_country = row[addressCountry].ToString();
                        uAddressBook.fax_no = row[faxNo].ToString();
                        uAddressBook.contact_number_1 = row[contactNo1].ToString();
                        uAddressBook.contact_name_1 = row[contactName1].ToString();
                        uAddressBook.contact_number_2 = row[contactNo2].ToString();
                        uAddressBook.contact_name_2 = row[contactName2].ToString();
                        uAddressBook.email_address = row[emailAddress].ToString();
                        uAddressBook.website = row[website].ToString();
                        uAddressBook.remark = row[remark].ToString();
                        // Assuming isRemoved is a boolean in your BLL and string "true"/"false" in DataTable.
                        uAddressBook.isRemoved = bool.Parse(row[isRemoved].ToString());
                        // Assuming updated_by is an integer in your BLL.
                        // Handle parsing since DataTable might return a string representation.
                        if (int.TryParse(row[updatedBy].ToString(), out int updatedByValue))
                        {
                            uAddressBook.updated_by = updatedByValue;
                        }
                        break; // Break the loop once the matching row is found and processed.
                    }
                }
            }

            return uAddressBook;
        }


        #endregion

        #region Insert Data in Database

        public bool Insert(AddressBookBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_address_book 
                            (" + companyTblCode + ","
                            + fullName + ","
                            + shortName + ","
                            + registrationNo + ","
                            + addressLine1 + ","
                            + addressLine2 + ","
                            + addressLine3 + ","
                            + addressState + ","
                            + addressPostalCode + ","
                            + addressCountry + ","
                            + faxNo + ","
                            + contactNo1 + ","
                            + contactName1 + ","
                            + contactNo2 + ","
                            + contactName2 + ","
                            + emailAddress + ","
                            + website + ","
                            + routeTblCode + ","
                            + remark + ","
                            + deliveryMethodRemark + ","
                            + isRemoved + ","
                            + updatedDate + ","
                            + updatedBy + ") VALUES" +
                            "(@cust_tbl_code," +
                            "@full_name," +
                            "@short_name," +
                            "@registration_no," +
                            "@address_1," +
                            "@address_2," +
                            "@address_3," +
                            "@address_state," +
                            "@address_postal_code," +
                            "@address_country," +
                            "@fax_no," +
                            "@contact_number_1," +
                            "@contact_name_1," +
                            "@contact_number_2," +
                            "@contact_name_2," +
                            "@email_address," +
                            "@website," +
                            "@route_tbl_code," +
                            "@remark," +
                            "@delivery_method_remark," +
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_tbl_code", u.company_tbl_code);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@short_name", u.short_name);
                cmd.Parameters.AddWithValue("@registration_no", u.registration_no);
                cmd.Parameters.AddWithValue("@address_1", u.address_1);
                cmd.Parameters.AddWithValue("@address_2", u.address_2);
                cmd.Parameters.AddWithValue("@address_3", u.address_3);
                cmd.Parameters.AddWithValue("@address_state", u.address_state);
                cmd.Parameters.AddWithValue("@address_postal_code", u.address_postal_code);
                cmd.Parameters.AddWithValue("@address_country", u.address_country);
                cmd.Parameters.AddWithValue("@fax_no", u.fax_no);
                cmd.Parameters.AddWithValue("@contact_number_1", u.contact_number_1);
                cmd.Parameters.AddWithValue("@contact_name_1", u.contact_name_1);
                cmd.Parameters.AddWithValue("@contact_number_2", u.contact_number_2);
                cmd.Parameters.AddWithValue("@contact_name_2", u.contact_name_2);
                cmd.Parameters.AddWithValue("@email_address", u.email_address);
                cmd.Parameters.AddWithValue("@website", u.website);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.route_tbl_code);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@delivery_method_remark", u.delivery_method_remark);
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

        public bool Update(AddressBookBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
             String sql = @"UPDATE tbl_address_book
                            SET "
                           + companyTblCode + "=@cust_tbl_code,"
                           + fullName + "=@full_name,"
                           + shortName + "=@short_name,"
                           + registrationNo + "=@registration_no,"
                           + addressLine1 + "=@address_1,"
                           + addressLine2 + "=@address_2,"
                           + addressLine3 + "=@address_3,"
                           + addressState + "=@address_state,"
                           + addressPostalCode + "=@address_postal_code,"
                           + addressCountry + "=@address_country,"
                           + faxNo + "=@fax_no,"
                           + contactNo1 + "=@contact_number_1,"
                           + contactName1 + "=@contact_name_1,"
                           + contactNo2 + "=@contact_number_2,"
                           + contactName2 + "=@contact_name_2,"
                           + emailAddress + "=@email_address,"
                           + website + "=@website,"
                           + routeTblCode + "=@route_tbl_code,"
                           + remark + "=@remark,"
                           + deliveryMethodRemark + "=@delivery_method_remark,"
                           + isRemoved + "=@isRemoved,"
                           + updatedDate + "=@updated_date,"
                           + updatedBy + "=@updated_by" +
                           " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@cust_tbl_code", u.company_tbl_code);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@short_name", u.short_name);
                cmd.Parameters.AddWithValue("@registration_no", u.registration_no);
                cmd.Parameters.AddWithValue("@address_1", u.address_1);
                cmd.Parameters.AddWithValue("@address_2", u.address_2);
                cmd.Parameters.AddWithValue("@address_3", u.address_3);
                cmd.Parameters.AddWithValue("@address_state", u.address_state);
                cmd.Parameters.AddWithValue("@address_postal_code", u.address_postal_code);
                cmd.Parameters.AddWithValue("@address_country", u.address_country);
                cmd.Parameters.AddWithValue("@fax_no", u.fax_no);
                cmd.Parameters.AddWithValue("@contact_number_1", u.contact_number_1);
                cmd.Parameters.AddWithValue("@contact_name_1", u.contact_name_1);
                cmd.Parameters.AddWithValue("@contact_number_2", u.contact_number_2);
                cmd.Parameters.AddWithValue("@contact_name_2", u.contact_name_2);
                cmd.Parameters.AddWithValue("@email_address", u.email_address);
                cmd.Parameters.AddWithValue("@website", u.website);
                cmd.Parameters.AddWithValue("@route_tbl_code", u.route_tbl_code);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@delivery_method_remark", u.delivery_method_remark);
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
                Tool tool = new Tool(); 
                tool.saveToText(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        #endregion

        #region Remove

        public bool SoftRemove(AddressBookBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_address_book
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

        public bool RemovePermanently(AddressBookBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_address_book WHERE tbl_code=@tbl_code";
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
                    // Command to create the table if it does not exist
                    string createTableSql = @"
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbl_address_book')
BEGIN
    CREATE TABLE tbl_address_book (
        tbl_code INT IDENTITY(1,1) PRIMARY KEY,
        company_tbl_code INT,
        route_tbl_code INT,
        full_name VARCHAR(255),
        short_name VARCHAR(255),
        registration_no VARCHAR(100),
        address_1 VARCHAR(255),
        address_2 VARCHAR(255),
        address_3 VARCHAR(255),
        address_state VARCHAR(100),
        address_postal_code VARCHAR(20),
        address_country VARCHAR(100),
        fax_no VARCHAR(20),
        contact_number_1 VARCHAR(20),
        contact_name_1 VARCHAR(255),
        contact_number_2 VARCHAR(20),
        contact_name_2 VARCHAR(255),
        email_address VARCHAR(255),
        website VARCHAR(255),
        delivery_method_remark VARCHAR(255),
        remark VARCHAR(255),
        isRemoved BIT,
        updated_date DATETIME,
        updated_by INT
    );
END";
                    SqlCommand createTableCmd = new SqlCommand(createTableSql, conn);
                    createTableCmd.ExecuteNonQuery();

                    // Commands to add missing columns if the table already exists
                    string[] columns = new string[] {
                "company_tbl_code INT",
                "route_tbl_code INT",
                "full_name VARCHAR(255)",
                "short_name VARCHAR(255)",
                "registration_no VARCHAR(100)",
                "address_1 VARCHAR(255)",
                "address_2 VARCHAR(255)",
                "address_3 VARCHAR(255)",
                "address_state VARCHAR(100)",
                "address_postal_code VARCHAR(20)",
                "address_country VARCHAR(100)",
                "fax_no VARCHAR(20)",
                "contact_number_1 VARCHAR(20)",
                "contact_name_1 VARCHAR(255)",
                "contact_number_2 VARCHAR(20)",
                "contact_name_2 VARCHAR(255)",
                "email_address VARCHAR(255)",
                "website VARCHAR(255)",
                "delivery_method_remark VARCHAR(255)",
                "remark VARCHAR(255)",
                "isRemoved BIT",
                "updated_date DATETIME",
                "updated_by INT"
            };

                    foreach (string column in columns)
                    {
                        string columnName = column.Split(' ')[0];
                        string alterTableSql = $@"
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_address_book' AND COLUMN_NAME = '{columnName}')
BEGIN
    ALTER TABLE tbl_address_book ADD {column};
END";
                        SqlCommand cmd = new SqlCommand(alterTableSql, conn);
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
