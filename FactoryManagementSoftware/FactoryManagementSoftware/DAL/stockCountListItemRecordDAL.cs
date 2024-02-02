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
    class stockCountListItemRecordDAL
    {
        #region data string name getter
        public string TblCode { get; } = "tbl_code";
        public string ListItemTblCode { get; } = "list_item_tbl_code";
        public string TotalUnitQty { get; } = "total_unit_qty";
        public string CountUnit { get; } = "count_unit";
        public string UnitConversionRate { get; } = "unit_conversion_rate";
        public string TotalPcs { get; } = "total_pcs";
        public string Remark { get; } = "remark";
        public string UpdatedBy { get; } = "updated_by";
        public string UpdatedDate { get; } = "updated_date";
        public string StockCountDate { get; } = "stock_count_date";


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
                String sql = @"SELECT * FROM tbl_stock_count_list_item_record";
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

        public bool Insert(stockCountListItemRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = $@"INSERT INTO tbl_stock_count_list_item_record (
                        {ListItemTblCode}, 
                        {TotalUnitQty},                         
                        {CountUnit}, 
                        {UnitConversionRate}, 
                        {TotalPcs}, 
                        {Remark}, 
                        {UpdatedBy}, 
                        {UpdatedDate}, 
                        {StockCountDate}
                        ) VALUES (
                        @list_item_tbl_code, 
                        @total_unit_qty,                         
                        @count_unit, 
                        @unit_conversion_rate, 
                        @total_pcs, 
                        @remark, 
                        @updated_by, 
                        @updated_date, 
                        @stock_count_date
                        )";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@list_item_tbl_code", u.list_item_tbl_code);
                cmd.Parameters.AddWithValue("@total_unit_qty", u.total_unit_qty);
                cmd.Parameters.AddWithValue("@count_unit", u.count_unit);
                cmd.Parameters.AddWithValue("@unit_conversion_rate", u.unit_conversion_rate);
                cmd.Parameters.AddWithValue("@total_pcs", u.total_pcs);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@stock_count_date", u.stock_count_date);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0;
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

            return isSuccess;
        }



        #endregion

        #region Update data in Database

        public bool Update(stockCountListItemRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = $@"UPDATE tbl_stock_count_list_item_record
                        SET 
                            {TotalUnitQty} = @total_unit_qty,                             
                            {CountUnit} = @count_unit, 
                            {UnitConversionRate} = @unit_conversion_rate, 
                            {TotalPcs} = @total_pcs, 
                            {Remark} = @remark, 
                            {UpdatedBy} = @updated_by, 
                            {UpdatedDate} = @updated_date, 
                            {StockCountDate} = @stock_count_date
                        WHERE 
                            {TblCode} = @tbl_code";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code); // Primary key to identify the record
                cmd.Parameters.AddWithValue("@total_unit_qty", u.total_unit_qty);
                cmd.Parameters.AddWithValue("@count_unit", u.count_unit);
                cmd.Parameters.AddWithValue("@unit_conversion_rate", u.unit_conversion_rate);
                cmd.Parameters.AddWithValue("@total_pcs", u.total_pcs);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@stock_count_date", u.stock_count_date);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0;
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

            return isSuccess;
        }



        #endregion

        #region Remove

        //public bool SoftRemove(stockCountListItemRecordBLL u)
        //{
        //    bool isSuccess = false;
        //    SqlConnection conn = new SqlConnection(myconnstrng);

        //    try
        //    {
        //        String sql = @"UPDATE tbl_stock_count_list_item_record
        //                    SET "
        //                      + isRemoved + "=@isRemoved,"
        //                      + updatedDate + "=@updated_date,"
        //                      + updatedBy + "=@updated_by" +
        //                      " WHERE tbl_code = @tbl_code";


        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@tbl_code", u.tbl_code);
        //        cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
        //        cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
        //        cmd.Parameters.AddWithValue("@updated_by", u.updated_by);

        //        conn.Open();

        //        int rows = cmd.ExecuteNonQuery();

        //        //if the query is executed successfully then the rows' value = 0
        //        if (rows > 0)
        //        {
        //            //query successful
        //            isSuccess = true;
        //        }
        //        else
        //        {
        //            //Query falled
        //            isSuccess = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return isSuccess;

        //}

        public bool RemovePermanently(stockCountListItemRecordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_stock_count_list_item_record WHERE tbl_code=@tbl_code";
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
                Tool tool = new Tool();
                tool.saveToTextAndMessageToUser(ex);
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
                               WHERE TABLE_NAME = 'tbl_stock_count_list_item_record')
                BEGIN
                    CREATE TABLE tbl_stock_count_list_item_record(
                        {TblCode} INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                        {ListItemTblCode} INT NOT NULL,
                        {TotalUnitQty} INT NOT NULL,
                        {CountUnit} VARCHAR(50) NOT NULL,
                        {UnitConversionRate} DECIMAL(10, 2) NOT NULL,
                        {TotalPcs} DECIMAL(18, 2) NOT NULL,
                        {Remark} VARCHAR(255),
                        {UpdatedBy} INT NOT NULL,
                        {UpdatedDate} DATETIME NOT NULL,
                        {StockCountDate} DATETIME NOT NULL
                    );
                END";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    // Consider implementing success notification
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
