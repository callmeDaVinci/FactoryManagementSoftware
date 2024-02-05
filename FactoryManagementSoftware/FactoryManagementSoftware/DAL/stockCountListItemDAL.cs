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
    class stockCountListItemDAL
    {
        #region data string name getter
        public string TblCode { get; } = "tbl_code";
        public string ListTblCode { get; } = "list_tbl_code";
        public string ItemCode { get; } = "item_code";
        public string IndexLevel { get; } = "index_level";
        public string FactoryTblCode { get; } = "factory_tbl_code";
        public string DefaultOutCatTblCode { get; } = "default_out_cat_tbl_code";
      
        public string DefaultInCatTblCode { get; } = "default_in_cat_tbl_code";
      
        public string CountUnit { get; } = "count_unit";
        public string UnitConversionRate { get; } = "unit_conversion_rate";
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
                String sql = @"SELECT * FROM tbl_stock_count_list_item";
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

        public DataTable SelectListItem(int listTblCode)
        {
            //static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            try
            {
                string sql = $@"SELECT * FROM tbl_stock_count_list_item
                        WHERE 
                            list_tbl_code = @list_tbl_code AND isRemoved != 1";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@list_tbl_code", listTblCode);

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

        public bool Insert(stockCountListItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // INSERT statement with one column per line for clarity
                string sql = $@"INSERT INTO tbl_stock_count_list_item (
                        {ListTblCode}, 
                        {ItemCode}, 
                        {FactoryTblCode}, 
                        {DefaultOutCatTblCode},
                        {DefaultInCatTblCode},
                        {CountUnit}, 
                        {UnitConversionRate},                        
                        {IndexLevel}, 
                        {Remark}, 
                        {isRemoved}, 
                        {updatedDate}, 
                        {updatedBy}
                        ) VALUES (
                        @list_tbl_code, 
                        @item_code, 
                        @factory_tbl_code, 
                        @default_out_cat_tbl_code,
                        @default_int_cat_tbl_code, 
                        @count_unit, 
                        @unit_conversion_rate,                      
                        @index_level, 
                        @remark, 
                        @isRemoved, 
                        @updated_date, 
                        @updated_by
                        )";

                SqlCommand cmd = new SqlCommand(sql, conn);

                // Mapping the BLL properties to the SQL parameters
                cmd.Parameters.AddWithValue("@list_tbl_code", u.list_tbl_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@factory_tbl_code", u.factory_tbl_code);
                cmd.Parameters.AddWithValue("@index_level", u.index_level);
                cmd.Parameters.AddWithValue("@default_out_cat_tbl_code", u.default_out_cat_tbl_code);
                cmd.Parameters.AddWithValue("@default_int_cat_tbl_code", u.default_int_cat_tbl_code);
                cmd.Parameters.AddWithValue("@count_unit", u.count_unit);
                cmd.Parameters.AddWithValue("@unit_conversion_rate", u.unit_conversion_rate);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

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

        public bool Update(stockCountListItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = $@"UPDATE tbl_stock_count_list_item
                        SET 
                            {ListTblCode} = @list_tbl_code, 
                            {ItemCode} = @item_code, 
                            {FactoryTblCode} = @factory_tbl_code, 
                            {DefaultOutCatTblCode} = @default_out_cat_tbl_code, 
                            {DefaultInCatTblCode} = @default_int_cat_tbl_code, 
                            {CountUnit} = @count_unit, 
                            {UnitConversionRate} = @unit_conversion_rate,                          
                            {IndexLevel} = @index_level, 
                            {Remark} = @remark, 
                            {isRemoved} = @isRemoved, 
                            {updatedDate} = @updated_date, 
                            {updatedBy} = @updated_by
                        WHERE 
                            {TblCode} = @tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
                cmd.Parameters.AddWithValue("@list_tbl_code", u.list_tbl_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@index_level", u.index_level);
                cmd.Parameters.AddWithValue("@factory_tbl_code", u.factory_tbl_code);
                cmd.Parameters.AddWithValue("@default_out_cat_tbl_code", u.default_out_cat_tbl_code);
                cmd.Parameters.AddWithValue("@default_int_cat_tbl_code", u.default_int_cat_tbl_code);
                cmd.Parameters.AddWithValue("@count_unit", u.count_unit);
                cmd.Parameters.AddWithValue("@unit_conversion_rate", u.unit_conversion_rate);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
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

        public bool SoftRemove(stockCountListItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_stock_count_list_item
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

        public bool RemovePermanently(stockCountListItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_stock_count_list_item WHERE tbl_code=@tbl_code";
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
                    string sql = $@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                               WHERE TABLE_NAME = 'tbl_stock_count_list_item')
                BEGIN
                    CREATE TABLE tbl_stock_count_list_item(
                        {TblCode} INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                        {ListTblCode} INT NOT NULL,
                        {ItemCode} VARCHAR(255) NOT NULL,
                        {FactoryTblCode} INT NOT NULL,
                        {DefaultOutCatTblCode} INT,
                        {DefaultInCatTblCode} INT,
                        {CountUnit} VARCHAR(50) NOT NULL,
                        {UnitConversionRate} DECIMAL(10, 2) NOT NULL,
                        {IndexLevel} INT,
                        {Remark} VARCHAR(255),
                        {isRemoved} BIT NOT NULL,
                        {updatedDate} DATETIME NOT NULL,
                        {updatedBy} INT NOT NULL
                    );
                END";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Table 'tbl_stock_count_list_item' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }


    }
}
