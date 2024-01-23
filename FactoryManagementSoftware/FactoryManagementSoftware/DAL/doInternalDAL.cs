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
    class doInternalDAL
    {
        #region data string name getter
        public string tblCode { get; } = "tbl_code";
        public string DOFormatTblCode { get; } = "do_format_tbl_code";
        public string RunningNo { get; } = "running_no";
        public string DoNoString { get; } = "do_no";
        public string CompanyTblCode { get; } = "company_tbl_code";
        public string ShippingAddressTblCode { get; } = "shipping_address_tbl_code";
        public string BillingAddressTblCode { get; } = "billing_address_tbl_code";
        public string ShippingMethod { get; } = "shipping_method";
        public string DeliveryDate { get; } = "delivery_date";
        public string IsDraft { get; } = "isDraft";
        public string IsProcessing { get; } = "isProcessing";
        public string IsCompleted { get; } = "isCompleted";
        public string IsCancelled { get; } = "isCancelled";
        public string remark { get; } = "remark";
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
                String sql = @"SELECT * FROM tbl_internal_do";
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

        public DataTable Select(string doNo)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_internal_do WHERE do_no = @doNo";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@doNo", doNo);

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

        public string SelectTblCodeByRandomCode(string RandomCode)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            string tblCode = "-1";

            try
            {
                // SQL query to get tbl_code from database where do_no matches
                String sql = @"SELECT tbl_code FROM tbl_internal_do WHERE do_no = @RandomCode";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RandomCode", RandomCode);

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


        #region Insert Data in Database

        public bool Insert(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_internal_do 
                            (" + DOFormatTblCode + ","
                            + RunningNo + ","
                            + DoNoString + ","
                            + CompanyTblCode + ","
                            + ShippingAddressTblCode + ","
                            + BillingAddressTblCode + ","
                            + ShippingMethod + ","
                            + DeliveryDate + ","
                            + IsDraft + ","
                            + IsProcessing + ","
                            + IsCompleted + ","
                            + IsCancelled + ","
                            + remark + ","
                            + updatedDate + ","
                            + updatedBy + ") VALUES" +
                            "(@do_format_tbl_code," +
                            "@running_no," +
                            "@do_no_string," +
                            "@company_tbl_code," +
                            "@shipping_address_tbl_code," +
                            "@billing_address_tbl_code," +
                            "@shipping_method," +
                            "@delivery_date," +
                            "@isDraft," +
                            "@isProcessing," +
                            "@isCompleted," +
                            "@isCancelled," +
                            "@remark," +
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@do_format_tbl_code", u.do_format_tbl_code);
                cmd.Parameters.AddWithValue("@company_tbl_code", u.company_tbl_code);
                cmd.Parameters.AddWithValue("@shipping_address_tbl_code", u.shipping_address_tbl_code);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@shipping_method", u.shipping_method);

                cmd.Parameters.AddWithValue("@running_no", u.running_no);
                cmd.Parameters.AddWithValue("@do_no_string", u.do_no_string);
                cmd.Parameters.AddWithValue("@billing_address_tbl_code", u.billing_address_tbl_code);
                cmd.Parameters.AddWithValue("@delivery_date", u.delivery_date);
                cmd.Parameters.AddWithValue("@isDraft", u.isDraft);
                cmd.Parameters.AddWithValue("@isProcessing", u.isProcessing);
                cmd.Parameters.AddWithValue("@isCompleted", u.isCompleted);
                cmd.Parameters.AddWithValue("@isCancelled", u.isCancelled);
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
        public bool SaveDraft(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_internal_do 
                            (" + DOFormatTblCode + ","
                            + RunningNo + ","
                            + DoNoString + ","
                            + CompanyTblCode + ","
                            + ShippingAddressTblCode + ","
                            + BillingAddressTblCode + ","
                            + ShippingMethod + ","
                            + DeliveryDate + ","
                            + IsDraft + ","
                            + IsProcessing + ","
                            + IsCompleted + ","
                            + IsCancelled + ","
                            + remark + ","
                            + updatedDate + ","
                            + updatedBy + ") VALUES" +
                            "(@do_format_tbl_code," +
                            "@running_no," +
                            "@do_no_string," +
                            "@company_tbl_code," +
                            "@shipping_address_tbl_code," +
                            "@billing_address_tbl_code," +
                            "@shipping_method," +
                            "@delivery_date," +
                            "@isDraft," +
                            "@isProcessing," +
                            "@isCompleted," +
                            "@isCancelled," +
                            "@remark," +
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@do_format_tbl_code", u.do_format_tbl_code);
                cmd.Parameters.AddWithValue("@running_no", -1);
                cmd.Parameters.AddWithValue("@do_no_string", "DRAFT");
                cmd.Parameters.AddWithValue("@company_tbl_code", u.company_tbl_code);
                cmd.Parameters.AddWithValue("@shipping_address_tbl_code", u.shipping_address_tbl_code);
                cmd.Parameters.AddWithValue("@billing_address_tbl_code", u.billing_address_tbl_code);
                cmd.Parameters.AddWithValue("@shipping_method", u.shipping_method);
                cmd.Parameters.AddWithValue("@delivery_date", u.delivery_date);
                cmd.Parameters.AddWithValue("@isDraft", true);
                cmd.Parameters.AddWithValue("@isProcessing", false);
                cmd.Parameters.AddWithValue("@isCompleted", false);
                cmd.Parameters.AddWithValue("@isCancelled", false);
                cmd.Parameters.AddWithValue("@remark", u.remark);
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

        public bool Update(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
             String sql = @"UPDATE tbl_internal_do
                            SET "
                           + DOFormatTblCode + "=@do_format_tbl_code,"
                           + RunningNo + "=@running_no,"
                           + DoNoString + "=@do_no_string,"
                           + CompanyTblCode + "=@company_tbl_code,"
                           + ShippingAddressTblCode + "=@shipping_address_tbl_code,"
                           + BillingAddressTblCode + "=@billing_address_tbl_code,"
                           + ShippingMethod + "=@shipping_method,"
                           + DeliveryDate + "=@delivery_date,"
                           + IsDraft + "=@isDraft,"
                           + IsProcessing + "=@isProcessing,"
                           + IsCompleted + "=@isCompleted,"
                           + IsCancelled + "=@isCancelled,"
                           + remark + "=@remark,"
                           + updatedDate + "=@updated_date,"
                           + updatedBy + "=@updated_by" +
                           " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@do_format_tbl_code", u.do_format_tbl_code);
                cmd.Parameters.AddWithValue("@running_no", u.running_no);
                cmd.Parameters.AddWithValue("@do_no_string", u.do_no_string);
                cmd.Parameters.AddWithValue("@company_tbl_code", u.company_tbl_code);
                cmd.Parameters.AddWithValue("@shipping_address_tbl_code", u.shipping_address_tbl_code);
                cmd.Parameters.AddWithValue("@billing_address_tbl_code", u.billing_address_tbl_code);
                cmd.Parameters.AddWithValue("@shipping_method", u.shipping_method);
                cmd.Parameters.AddWithValue("@delivery_date", u.delivery_date);
                cmd.Parameters.AddWithValue("@isDraft", u.isDraft);
                cmd.Parameters.AddWithValue("@isProcessing", u.isProcessing);
                cmd.Parameters.AddWithValue("@isCompleted", u.isCompleted);
                cmd.Parameters.AddWithValue("@isCancelled", u.isCancelled);
                cmd.Parameters.AddWithValue("@remark", u.remark);
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

        public bool UpdateDoNo(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_internal_do
                            SET "
                              + RunningNo + "=@running_no,"
                              + DoNoString + "=@do_no_string,"
                              + IsDraft + "=@isDraft,"
                              + IsProcessing + "=@isProcessing,"
                              + IsCompleted + "=@isCompleted,"
                              + IsCancelled + "=@isCancelled,"
                              + updatedDate + "=@updated_date,"
                              + updatedBy + "=@updated_by" +
                              " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);

             
                cmd.Parameters.AddWithValue("@running_no", u.running_no);
                cmd.Parameters.AddWithValue("@do_no_string", u.do_no_string);
                cmd.Parameters.AddWithValue("@isDraft", u.isDraft);
                cmd.Parameters.AddWithValue("@isProcessing", u.isProcessing);
                cmd.Parameters.AddWithValue("@isCompleted", u.isCompleted);
                cmd.Parameters.AddWithValue("@isCancelled", u.isCancelled);
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

        public bool UpdateDeliveryDate(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_internal_do
                            SET "
                              + DeliveryDate + "=@do_no_string,"
                              + updatedDate + "=@updated_date,"
                              + updatedBy + "=@updated_by" +
                              " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@delivery_date", u.delivery_date);
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

        public bool DOStatusUpdate(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_internal_do
                            SET "
                              + IsDraft + "=@isDraft,"
                              + IsProcessing + "=@isProcessing,"
                              + IsCompleted + "=@isCompleted,"
                              + IsCancelled + "=@isCancelled,"
                              + updatedDate + "=@updated_date,"
                              + updatedBy + "=@updated_by" +
                              " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@isDraft", u.isDraft);
                cmd.Parameters.AddWithValue("@isProcessing", u.isProcessing);
                cmd.Parameters.AddWithValue("@isCompleted", u.isCompleted);
                cmd.Parameters.AddWithValue("@isCancelled", u.isCancelled);
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

        public bool RemovePermanently(internalDOBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_internal_do WHERE tbl_code=@tbl_code";
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
