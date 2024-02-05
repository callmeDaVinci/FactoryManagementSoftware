using System;
using FactoryManagementSoftware.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FactoryManagementSoftware.Module;
using Accord;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class stockCountListDAL
    {
        #region data string name getter
        public string TblCode { get; } = "tbl_code";
        public string ListDescription { get; } = "list_description";
        public string DefaultFactoryTblCode { get; } = "default_factory_tbl_code";
        public string Remark { get; } = "remark";
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
                String sql = @"SELECT * FROM tbl_stock_count_list";
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

        public bool Insert(stockCountListBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // Correctly using the data string names for the column names in the SQL statement
                String sql = $@"INSERT INTO tbl_stock_count_list 
                            ({ListDescription}, 
                            {DefaultFactoryTblCode}, 
                            {Remark}, 
                            {isRemoved}, 
                            {updatedDate}, 
                            {updatedBy}) VALUES 
                            (@list_description, 
                            @default_factory_tbl_code, 
                            @remark, 
                            @isRemoved, 
                            @updated_date, 
                            @updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                // Mapping the BLL properties to the SQL parameters
                cmd.Parameters.AddWithValue("@list_description", u.list_description);
                cmd.Parameters.AddWithValue("@default_factory_tbl_code", u.default_factory_tbl_code);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                // Determining success based on the number of rows affected
                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                Module.Tool tool = new Module.Tool();
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Update data in Database

        public bool Update(stockCountListBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // Using data string names for column names in the SQL update statement
                String sql = $@"UPDATE tbl_stock_count_list
                        SET {ListDescription}=@list_description,
                        {DefaultFactoryTblCode}=@default_factory_tbl_code,
                        {Remark}=@remark,
                        {isRemoved}=@isRemoved,
                        {updatedDate}=@updated_date,
                        {updatedBy}=@updated_by
                        WHERE {TblCode} = @tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);
                // Assigning values to parameters
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code); // Assuming you have this property for identifying the record
                cmd.Parameters.AddWithValue("@list_description", u.list_description);
                cmd.Parameters.AddWithValue("@default_factory_tbl_code", u.default_factory_tbl_code);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                // If the update is successful, rows will be greater than 0
                isSuccess = rows > 0;
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

        #region Remove

        public bool SoftRemove(stockCountListBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_stock_count_list
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

        public bool RemovePermanently(stockCountListBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_stock_count_list WHERE tbl_code=@tbl_code";
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


        public void CreateTable()
        {
            using (SqlConnection conn = new SqlConnection(myconnstrng))
            {
                try
                {
                    // Command to check if the table exists and create it if it does not
                    string sql = $@"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                                   WHERE TABLE_NAME = 'tbl_stock_count_list')
                    BEGIN
                        CREATE TABLE tbl_stock_count_list(
                            {TblCode} INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                            {ListDescription} VARCHAR(100) NOT NULL,
                            {DefaultFactoryTblCode} INT,
                            {Remark} VARCHAR(200),
                            {isRemoved} BIT,
                            {updatedDate} DATETIME NOT NULL,
                            {updatedBy} INT NOT NULL
                        );
                    END";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();

                    int result = cmd.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (result != -1)
                    {
                        // Show a message box indicating success
                        //MessageBox.Show("Table 'tbl_stock_count_list' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Tool tool = new Tool();
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
