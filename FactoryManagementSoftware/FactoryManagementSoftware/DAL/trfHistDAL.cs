﻿using FactoryManagementSoftware.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    class trfHistDAL
    {

        #region data string name getter
        public string TrfID { get; } = "trf_hist_id";
        public string TrfItemCat { get; } = "item_cat";
        public string TrfItemName { get; } = "item_name";
        public string TrfItemCode { get; } = "trf_hist_item_code";

        public string TrfFrom { get; } = "trf_hist_from";
        public string TrfTo { get; } = "trf_hist_to";
        public string TrfQty { get; } = "trf_hist_qty";
        public string TrfUnit { get; } = "trf_hist_unit";
        public string TrfDate { get; } = "trf_hist_trf_date";
        public string TrfNote { get; } = "trf_hist_note";

        public string TrfAddedDate { get; } = "trf_hist_added_date";
        public string TrfAddedBy { get; } = "trf_hist_added_by";
        public string TrfResult { get; } = "trf_result";
        public string TrfUpdatedDate { get; } = "trf_hist_updated_date";
        public string TrfUpdatedBy { get; } = "trf_hist_updated_by";
        public string TrfFromOrder { get; } = "trf_hist_from_order";

        #endregion

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
                String sql = "SELECT * FROM tbl_trf_hist INNER JOIN tbl_item ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code";
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
                MessageBox.Show(ex.Message);
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

        public bool Insert(trfHistBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_trf_hist (trf_hist_item_code, trf_hist_from, trf_hist_to, trf_hist_qty, trf_hist_unit, trf_hist_trf_date, trf_hist_note, trf_hist_added_date, trf_hist_added_by, trf_result, trf_hist_from_order) VALUES (@trf_hist_item_code,  @trf_hist_from, @trf_hist_to, @trf_hist_qty, @trf_hist_unit, @trf_hist_trf_date, @trf_hist_note, @trf_hist_added_date, @trf_hist_added_by, @trf_result, @trf_hist_from_order)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trf_hist_item_code", u.trf_hist_item_code);
                cmd.Parameters.AddWithValue("@trf_hist_from", u.trf_hist_from);
                cmd.Parameters.AddWithValue("@trf_hist_to", u.trf_hist_to);
                cmd.Parameters.AddWithValue("@trf_hist_qty", u.trf_hist_qty);
                cmd.Parameters.AddWithValue("@trf_hist_unit", u.trf_hist_unit);
                cmd.Parameters.AddWithValue("@trf_hist_trf_date", u.trf_hist_trf_date);
                cmd.Parameters.AddWithValue("@trf_hist_note", u.trf_hist_note);
                cmd.Parameters.AddWithValue("@trf_hist_added_date", u.trf_hist_added_date);
                cmd.Parameters.AddWithValue("@trf_hist_added_by", u.trf_hist_added_by);
                cmd.Parameters.AddWithValue("@trf_result", u.trf_result);
                cmd.Parameters.AddWithValue("@trf_hist_from_order", u.trf_hist_from_order);

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region Update data in Database

        public bool Update(trfHistBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE tbl_trf_hist SET trf_result=@trf_result, trf_hist_updated_date=@trf_hist_updated_date, trf_hist_updated_by=@trf_hist_updated_by WHERE trf_hist_id=@trf_hist_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@trf_hist_id", u.trf_hist_id);
                cmd.Parameters.AddWithValue("@trf_result", u.trf_result);
                cmd.Parameters.AddWithValue("@trf_hist_updated_date", u.trf_hist_updated_date);
                cmd.Parameters.AddWithValue("@trf_hist_updated_by", u.trf_hist_updated_by);

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region Search User on Database usingKeywords

        public DataTable Search(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist  INNER JOIN tbl_item ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code WHERE tbl_item.item_code LIKE '%" + keywords + "%'OR tbl_item.item_name LIKE '%" + keywords + "%'";

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable nameSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist INNER JOIN tbl_item ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code  WHERE tbl_item.item_name LIKE '%" + keywords + "%'";

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable codeSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist INNER JOIN tbl_item ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code WHERE tbl_item.item_code LIKE '%" + keywords + "%'";

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable outSearch(string customer, int month, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist WHERE MONTH(trf_hist_trf_date) = @month AND trf_hist_item_code=@itemCode AND trf_hist_to=@customer";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@customer", customer);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable rangeItemToCustomerSearch(string customer, string start, string end, string itemCode)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist WHERE trf_hist_trf_date BETWEEN @start AND @end AND trf_hist_item_code=@itemCode AND trf_hist_to=@customer";

                SqlCommand cmd = new SqlCommand(sql, conn);
               
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@customer", customer);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable rangeMaterialToProductionSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            string to = "Production";

            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND trf_hist_to=@to
                                AND tbl_item.item_cat != @cat";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@to", to);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable rangePartTrfSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            String sql = null;
            try
            {
                //sql query to get data from database
                sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_item 
                            ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                            WHERE 
                            trf_hist_trf_date 
                            BETWEEN @start AND @end 
                            AND tbl_item.item_cat = @cat";
          

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable rangePartTrfSearch(string start, string end, int custID)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            String sql = null;
            try
            {
                //sql query to get data from database
                sql = @"SELECT * FROM tbl_trf_hist 
                            INNER JOIN tbl_item_cust 
                            ON tbl_trf_hist.trf_hist_item_code = tbl_item_cust.item_code 
                            INNER JOIN tbl_item 
                            ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                            WHERE 
                            trf_hist_trf_date 
                            BETWEEN @start AND @end 
                            AND tbl_item_cust.cust_id = @custID";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@custID", custID);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable rangeMaterialTrfSearch(string start, string end, string material)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            string cat = "Part";
            String sql = null;
            try
            {
                if(material.Equals("All"))
                {
                    //sql query to get data from database
                    sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat != @cat";
                }
                else
                {
                    //sql query to get data from database
                    sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end 
                                AND tbl_item.item_cat = @material";
                }
               

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@material", material);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@cat", cat);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable rangeTrfSearch(string start, string end)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();

            String sql = null;
            try
            {
                
                    //sql query to get data from database
                    sql = @"SELECT * FROM tbl_trf_hist 
                                INNER JOIN tbl_item 
                                ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE 
                                trf_hist_trf_date 
                                BETWEEN @start AND @end";
                


                SqlCommand cmd = new SqlCommand(sql, conn);

   
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);


                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            dt.DefaultView.Sort = "trf_hist_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            return sortedDt;
        }

        public DataTable facSearch(string itemCode, string facName)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = @"SELECT * FROM tbl_trf_hist INNER JOIN tbl_item ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code 
                                WHERE tbl_item.item_code=@itemCode AND (tbl_trf_hist.trf_hist_from = @facName OR tbl_trf_hist.trf_hist_to = @facName)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@facName", facName);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        public DataTable catSearch(string keywords)
        {
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist INNER JOIN tbl_item ON tbl_trf_hist.trf_hist_item_code = tbl_item.item_code WHERE tbl_item.item_cat=@category";

                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@category", keywords);
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return dt;
        }

        #endregion

        public bool ifFromOrder(int indexNo)
        {
            bool result = false;

            int fromOrder = -1;
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist WHERE trf_hist_id = @trf_hist_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@trf_hist_id", indexNo);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    if(i["trf_hist_from_order"] is DBNull)
                    {
                        fromOrder = 0;
                    }
                    else
                    {
                        fromOrder = Convert.ToInt32(i["trf_hist_from_order"]);
                    }
                    
                }
            }

            if(fromOrder > 0)
            {
                result = true;
            }

            return result;
        }

        public int getIndexNo(trfHistBLL u)
        {
            int indexNo = -1;
            //static methodd to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql query to get data from database
                String sql = "SELECT * FROM tbl_trf_hist WHERE trf_hist_added_date = @dateTime AND trf_hist_item_code=@itemCode";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@itemCode", u.trf_hist_item_code);
                cmd.Parameters.AddWithValue("@dateTime", u.trf_hist_added_date);

                //for executing command
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow i in dt.Rows)
                {
                    indexNo = Convert.ToInt32(i["trf_hist_id"]);
                }
            }
            return indexNo;
        }
    }
}
