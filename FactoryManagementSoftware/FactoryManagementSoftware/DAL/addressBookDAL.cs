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
        public string custTblCode { get; } = "cust_tbl_code";
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
                String sql = @"SELECT * FROM tbl_address_book";
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

        public bool Insert(AddressBookBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_address_book 
                            (" + custTblCode + ","
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
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cust_tbl_code", u.cust_tbl_code);
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

        public bool JobUpdate(AddressBookBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
             String sql = @"UPDATE tbl_address_book
                            SET "
                           + custTblCode + "=@cust_tbl_code,"
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
                           + isRemoved + "=@isRemoved,"
                           + updatedDate + "=@updated_date,"
                           + updatedBy + "=@updated_by" +
                           " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@cust_tbl_code", u.cust_tbl_code);
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


    }
}
