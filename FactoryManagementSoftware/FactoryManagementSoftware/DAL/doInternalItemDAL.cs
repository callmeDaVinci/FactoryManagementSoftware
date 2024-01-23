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
    class doInternalItemDAL
    {
        #region data string name getter
        public string TblCode { get; } = "tbl_code";
        public string InternalDoTblCode { get; } = "internal_do_tbl_code";
        public string ItemCode { get; } = "item_code";
        public string TotalQty { get; } = "total_qty";
        public string QtyUnit { get; } = "qty_unit";
        public string QtyPerBox { get; } = "qty_per_box";
        public string BoxQty { get; } = "box_qty";
        public string BoxUnit { get; } = "box_unit";
        public string BalanceQty { get; } = "balance_qty";
        public string SearchMode { get; } = "search_mode";
        public string ItemDesription { get; } = "item_description";
        public string Description { get; } = "description";
        public string DescriptionPacking { get; } = "description_packing";
        public string DescriptionCategory { get; } = "description_category";
        public string DescriptionRemark { get; } = "description_remark";
        public string IsRemoved { get; } = "isRemoved";
        public string Remark { get; } = "remark";
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
                String sql = @"SELECT * FROM tbl_internal_do_item";
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

        public DataTable Select(string doTblCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_internal_do_item WHERE internal_do_tbl_code = @doTblCode";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@doTblCode", doTblCode);

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

        public bool Insert(internalDOItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"INSERT INTO tbl_internal_do_item 
                            (" + InternalDoTblCode + ","
                            + ItemCode + ","
                            + TotalQty + ","
                            + QtyUnit + ","
                            + QtyPerBox + ","
                            + BoxQty + ","
                            + BoxUnit + ","
                            + BalanceQty + ","
                            + Remark + ","
                            + SearchMode + ","
                            + ItemDesription + ","
                            + Description + ","
                            + DescriptionPacking + ","
                            + DescriptionCategory + ","
                            + DescriptionRemark + ","
                            + IsRemoved + ","
                            + updatedDate + ","
                            + updatedBy + ") VALUES" +
                            "(@internal_do_tbl_code," +
                            "@item_code," +
                            "@total_qty," +
                            "@qty_unit," +
                            "@qty_per_box," +
                            "@box_qty," +
                            "@box_unit," +
                            "@balance_qty," +
                            "@remark," +
                            "@search_mode," +
                            "@item_description," +
                            "@description," +
                            "@description_packing," +
                            "@description_category," +
                            "@description_remark," +
                            "@isRemoved," +
                            "@updated_date," +
                            "@updated_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@internal_do_tbl_code", u.internal_do_tbl_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@total_qty", u.total_qty);
                cmd.Parameters.AddWithValue("@qty_unit", u.qty_unit);
                cmd.Parameters.AddWithValue("@qty_per_box", u.qty_per_box);
                cmd.Parameters.AddWithValue("@box_qty", u.box_qty);
                cmd.Parameters.AddWithValue("@box_unit", u.box_unit);
                cmd.Parameters.AddWithValue("@balance_qty", u.balance_qty);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@search_mode", u.search_mode);
                cmd.Parameters.AddWithValue("@item_description", u.item_description);
                cmd.Parameters.AddWithValue("@description", u.description);
                cmd.Parameters.AddWithValue("@description_packing", u.description_packing);
                cmd.Parameters.AddWithValue("@description_category", u.description_category);
                cmd.Parameters.AddWithValue("@description_remark", u.description_remark);
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

        public bool Update(internalDOItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
             String sql = @"UPDATE tbl_internal_do_item
                            SET "
                           + InternalDoTblCode + "=@internal_do_tbl_code,"
                           + ItemCode + "=@item_code,"
                           + TotalQty + "=@total_qty,"
                           + QtyUnit + "=@qty_unit,"
                           + QtyPerBox + "=@qty_per_box,"
                           + BoxQty + "=@box_qty,"
                           + BoxUnit + "=@box_unit,"
                           + BalanceQty + "=@balance_qty,"
                           + Remark + "=@remark,"
                           + SearchMode + "=@search_mode,"
                           + ItemDesription + "=@item_description,"
                           + Description + "=@description,"
                           + DescriptionPacking + "=@description_packing,"
                           + DescriptionCategory + "=@description_category,"
                           + DescriptionRemark + "=@description_remark,"
                           + IsRemoved + "=@isRemoved,"
                           + updatedDate + "=@updated_date,"
                           + updatedBy + "=@updated_by" +
                           " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@internal_do_tbl_code", u.internal_do_tbl_code);
                cmd.Parameters.AddWithValue("@item_code", u.item_code);
                cmd.Parameters.AddWithValue("@total_qty", u.total_qty);
                cmd.Parameters.AddWithValue("@qty_unit", u.qty_unit);
                cmd.Parameters.AddWithValue("@qty_per_box", u.qty_per_box);
                cmd.Parameters.AddWithValue("@box_qty", u.box_qty);
                cmd.Parameters.AddWithValue("@box_unit", u.box_unit);
                cmd.Parameters.AddWithValue("@balance_qty", u.balance_qty);
                cmd.Parameters.AddWithValue("@remark", u.remark);
                cmd.Parameters.AddWithValue("@search_mode", u.search_mode);
                cmd.Parameters.AddWithValue("@item_description", u.item_description);
                cmd.Parameters.AddWithValue("@description", u.description);
                cmd.Parameters.AddWithValue("@description_packing", u.description_packing);
                cmd.Parameters.AddWithValue("@description_category", u.description_category);
                cmd.Parameters.AddWithValue("@description_remark", u.description_remark);
                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
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

        #region Remove

        public bool SoftRemove(internalDOItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_internal_do_item
                            SET "
                              + IsRemoved + "=@isRemoved,"
                              + updatedDate + "=@updated_date,"
                              + updatedBy + "=@updated_by" +
                              " WHERE tbl_code = @tbl_code";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@isRemoved", u.isRemoved);
                cmd.Parameters.AddWithValue("@updated_date", u.updated_date);
                cmd.Parameters.AddWithValue("@updated_by", u.updated_by);
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

        public bool RemovePermanently(internalDOItemBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM tbl_internal_do_item WHERE tbl_code=@tbl_code";
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
