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
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_company";
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

        public bool JobUpdate(companyBLL u)
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


    }
}
