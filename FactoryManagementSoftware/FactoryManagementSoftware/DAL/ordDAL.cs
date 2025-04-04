﻿using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class ordDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
    

        #region Select Data from Database
        public DataTable Select()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_item_code = tbl_item.item_code";
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable Select(int orderID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_item_code = tbl_item.item_code WHERE ord_id=@ord_id";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", orderID);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable PendingOrderSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_item_code = tbl_item.item_code WHERE ord_status=@ord_status";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_status", "PENDING");
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable RequestingOrderSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_item_code = tbl_item.item_code WHERE ord_status=@ord_status";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_status", "REQUESTING");
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }
        public DataTable PendingOrderSelect(string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_item_code = tbl_item.item_code WHERE tbl_item.item_code =@itemCode AND tbl_ord.ord_status=@ord_status";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_status", "PENDING");
                cmd.Parameters.AddWithValue("@itemCode", itemCode);
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable lastRecordSelect()
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT TOP 1 * FROM tbl_ord ORDER BY ord_id DESC";
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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
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
        public bool Insert(ordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_ord (ord_item_code, ord_qty, ord_unit, ord_status, ord_required_date, ord_added_date, ord_added_by, ord_note,ord_type,ord_po_no) VALUES ( @ord_item_code, @ord_qty, @ord_unit, @ord_status, @ord_required_date, @ord_added_date, @ord_added_by, @ord_note, @ord_type,@ord_po_no)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_item_code", u.ord_item_code);
                cmd.Parameters.AddWithValue("@ord_qty", u.ord_qty);
                cmd.Parameters.AddWithValue("@ord_unit", u.ord_unit);
                cmd.Parameters.AddWithValue("@ord_status", u.ord_status);
                cmd.Parameters.AddWithValue("@ord_required_date", u.ord_required_date);
                cmd.Parameters.AddWithValue("@ord_added_date", u.ord_added_date);
                cmd.Parameters.AddWithValue("@ord_added_by", u.ord_added_by);
                cmd.Parameters.AddWithValue("@ord_note", u.ord_note);
                cmd.Parameters.AddWithValue("@ord_type", u.ord_type);
                cmd.Parameters.AddWithValue("@ord_po_no", u.ord_po_no);

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

        public bool DataMerge(ordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_ord (ord_item_code, ord_qty, ord_unit, ord_status, ord_required_date, ord_added_date, ord_added_by, ord_note,ord_type,ord_po_no,ord_pending,ord_received) VALUES ( @ord_item_code, @ord_qty, @ord_unit, @ord_status, @ord_required_date, @ord_added_date, @ord_added_by, @ord_note, @ord_type,@ord_po_no,@ord_pending,@ord_received)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_item_code", u.ord_item_code);
                cmd.Parameters.AddWithValue("@ord_qty", u.ord_qty);
                cmd.Parameters.AddWithValue("@ord_unit", u.ord_unit);
                cmd.Parameters.AddWithValue("@ord_status", u.ord_status);
                cmd.Parameters.AddWithValue("@ord_required_date", u.ord_required_date);
                cmd.Parameters.AddWithValue("@ord_added_date", u.ord_added_date);
                cmd.Parameters.AddWithValue("@ord_added_by", u.ord_added_by);
                cmd.Parameters.AddWithValue("@ord_note", u.ord_note);
                cmd.Parameters.AddWithValue("@ord_type", u.ord_type);
                cmd.Parameters.AddWithValue("@ord_po_no", u.ord_po_no);
                cmd.Parameters.AddWithValue("@ord_pending", u.ord_pending);
                cmd.Parameters.AddWithValue("@ord_received", u.ord_received);



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

        #region Update data in Database

        public bool Update(ordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_ord SET 
                                ord_item_code=@ord_item_code,
                                ord_required_date=@ord_required_date,
                                ord_status=@ord_status,
                                ord_qty=@ord_qty,
                                ord_pending=@ord_pending,
                                ord_received=@ord_received,
                                ord_updated_date=@ord_updated_date,
                                ord_type=@ord_type,
                                ord_po_no=@ord_po_no,
                                ord_updated_by=@ord_updated_by
                                WHERE ord_id=@ord_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@ord_item_code", u.ord_item_code);
                cmd.Parameters.AddWithValue("@ord_status", u.ord_status);
                cmd.Parameters.AddWithValue("@ord_required_date", u.ord_required_date);
                cmd.Parameters.AddWithValue("@ord_qty", u.ord_qty);
                cmd.Parameters.AddWithValue("@ord_pending", u.ord_pending);
                cmd.Parameters.AddWithValue("@ord_received", u.ord_received);
                cmd.Parameters.AddWithValue("@ord_updated_date", u.ord_updated_date);
                cmd.Parameters.AddWithValue("@ord_updated_by", u.ord_updated_by);
                cmd.Parameters.AddWithValue("@ord_type", u.ord_type);
                cmd.Parameters.AddWithValue("@ord_po_no", u.ord_po_no);

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

        public bool POUpdate(ordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_ord SET 
                                ord_updated_date=@ord_updated_date,
                                ord_po_no=@ord_po_no,
                                ord_updated_by=@ord_updated_by
                                WHERE ord_id=@ord_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@ord_updated_date", u.ord_updated_date);
                cmd.Parameters.AddWithValue("@ord_updated_by", u.ord_updated_by);
                cmd.Parameters.AddWithValue("@ord_po_no", u.ord_po_no);

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

        public bool receivedUpdate(ordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_ord SET 
                                ord_pending=@ord_pending,
                                ord_received=@ord_received
                                WHERE ord_id=@ord_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@ord_pending", u.ord_pending);
                cmd.Parameters.AddWithValue("@ord_received", u.ord_received);


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

        public bool statusUpdate(ordBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = @"UPDATE tbl_ord SET
                                ord_status=@ord_status
                                WHERE ord_id=@ord_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ord_id", u.ord_id);
                cmd.Parameters.AddWithValue("@ord_status", u.ord_status);

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

        #region Search item category on Database usingKeywords

        public DataTable Search(string keyword)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON (tbl_item.item_name LIKE '%" + keyword + "%' OR tbl_item.item_code LIKE '%" + keyword + "%')AND  tbl_item.item_code = tbl_ord.ord_item_code ";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable PONOSearch(string keyword)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_po_no LIKE '%" + keyword + "%' AND  tbl_item.item_code = tbl_ord.ord_item_code ";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable IDSearch(string keyword)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_ord.ord_id LIKE '%" + keyword + "%' AND  tbl_item.item_code = tbl_ord.ord_item_code ";

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
                Module.Tool tool = new Module.Tool(); tool.saveToText(ex);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable StatusSearch(string keyword)
        {
            // Static method to connect to the database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // To hold the data from the database
            DataTable dt = new DataTable();
            try
            {
                // SQL query to get data from database with ord_status matching the keyword
                String sql = "SELECT * FROM tbl_ord INNER JOIN tbl_item ON tbl_item.item_code = tbl_ord.ord_item_code " +
                             "WHERE tbl_ord.ord_status LIKE '%' + @keyword + '%'";

                // For executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keyword", keyword);

                // Getting data from the database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Open database connection
                conn.Open();
                // Fill data in our DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw message if any error occurs
                Module.Tool tool = new Module.Tool();
                tool.saveToText(ex);
            }
            finally
            {
                // Closing connection
                conn.Close();
            }
            return dt;
        }

        #endregion




    }
}
